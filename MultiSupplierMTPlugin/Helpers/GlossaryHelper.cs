using Bbieniek.Uax29;
using NReco.Text;
using Snowball;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MultiSupplierMTPlugin.Helpers
{
    class GlossaryHelper
    {
        private const string _SOURCE_TERM_FIELD = "SourceTerm";
        private const string _TARGET_TERM_FIELD = "TargetTerm";
        private const string _SOURCE_LANGUAGE_FIELD = "SourceLanguage";
        private const string _TARGET_LANGUAGE_FIELD = "TargetLanguage";

        private const string _ANY_SOURCE_LANGUAGE = "AnySourceLanguage";
        private const string _ANY_TARGET_LANGUAGE = "AnyTargetLanguage";

        private static readonly ConcurrentDictionary<string, Matcher> _matcherCache
            = new ConcurrentDictionary<string, Matcher>();

        #region Public Methods

        public static string ReadGlossaryString(List<string> plainTexts, string path, string srcLang, string tgtLang,
            string delimiter = ",", string charset = "utf-8", bool enableCache = true)
        {
            var termPairs = ReadGlossaryPairs(plainTexts, path, srcLang, tgtLang, delimiter, charset, enableCache);
            return string.Join(Environment.NewLine, termPairs.Select(tp => $"{tp.Key}\t{tp.Value}"));
        }

        public static HashSet<KeyValuePair<string, string>> ReadGlossaryPairs(List<string> plainTexts, string path,
            string srcLang, string tgtLang, string delimiter = ",", string charset = "utf-8", bool enableCache = true)
        {
            var matcher = GetOrCreateMatcher(path, srcLang, tgtLang, delimiter, charset, enableCache);

            string matchString = string.Join("", plainTexts);
            if (matcher.Stemmer != null && matcher.WordEncoder != null)
            {
                var words = Tokenize(matchString, matcher.Stemmer);
                matchString = matcher.WordEncoder.EncodeForQuery(words);
            }

            var result = new HashSet<KeyValuePair<string, string>>();
            matcher.Automata.ParseText(matchString, hit =>
            {
                var concept = hit.Value.Concept;

                foreach (var langCode in new[] { tgtLang, _ANY_TARGET_LANGUAGE })
                {
                    if (!concept.TermDic.ContainsKey(langCode)) continue;

                    foreach (var targetTerm in concept.TermDic[langCode])
                    {
                        result.Add(new KeyValuePair<string, string>(hit.Value.SourceTerm, targetTerm));
                    }
                }
            });

            //LoggingHelper.Info($"Matched {result.Count} term(s) from the glossary");

            return result;
        }

        #endregion

        #region Tokenize

        private static List<string> Tokenize(string text, Stemmer stemmer = null)
        {
            var tokens = new List<string>();

            foreach (var token in WordBreakTokenizer.Tokenize(text))
            {
                var tokenValue = text.Substring(token.Start, token.Length);

                tokens.Add(stemmer != null && token.IsWord ? stemmer.Stem(tokenValue) : tokenValue);
            }

            return tokens;
        }

        #endregion

        #region Matcher

        private static string GetMatcherCacheKey(string path, string srcLang, string tgtLang, string delimiter, string charset)
        {
            string modificationTime;
            try
            {
                modificationTime = File.GetLastWriteTimeUtc(path).ToString("o");
            }
            catch
            {
                throw new Exception($"Glossary file does not exist: {path}");
            }
            return $"{path}|{modificationTime}|{srcLang}|{tgtLang}|{delimiter}|{charset}";
        }

        private static Matcher GetOrCreateMatcher(string path, string srcLang, string tgtLang,
           string delimiter = ",", string charset = "utf-8", bool enableCache = true)
        {
            string key = GetMatcherCacheKey(path, srcLang, tgtLang, delimiter, charset);

            if (enableCache && _matcherCache.TryGetValue(key, out var cachedMatcher))
            {
                LoggingHelper.Info("Glossary load from cache done");
                return cachedMatcher;
            }

            bool hasStemmer = StemmerFactory.TryGet(srcLang, out var stemmer);
            WordEncoder wordEncoder = hasStemmer ? new WordEncoder(true) : null;

            var entries = new List<KeyValuePair<string, TermHit>>();
            var concepts = ReadGlossaryConceptsFromFile(path, delimiter, charset, new HashSet<string>() { srcLang, tgtLang });
            foreach (var concept in concepts)
            {
                foreach (var langCode in new[] { srcLang, _ANY_SOURCE_LANGUAGE })
                {
                    if (!concept.TermDic.ContainsKey(langCode)) continue;

                    foreach (var srcTerm in concept.TermDic[langCode])
                    {
                        string matchString = srcTerm;
                        if (stemmer != null && wordEncoder != null)
                        {
                            var words = Tokenize(srcTerm, stemmer);
                            matchString = wordEncoder.EncodeForBuild(words);
                        }

                        var termHit = new TermHit { SourceTerm = srcTerm, Concept = concept };

                        entries.Add(new KeyValuePair<string, TermHit>(matchString, termHit));
                    }
                }
            }

            var matcher = new Matcher()
            {
                Stemmer = stemmer,
                WordEncoder = wordEncoder,
                Automata = new AhoCorasickDoubleArrayTrie<TermHit>(entries),
            };

            if (enableCache)
            {
                _matcherCache[key] = matcher;
            }

            LoggingHelper.Info("Glossary load from disk done");
            return matcher;
        }

        #endregion

        #region File Parsing

        private static List<Concept> ReadGlossaryConceptsFromFile(string path, string delimiter = ",", string charset = "utf-8", HashSet<string> includeLangs = null)
        {
            using (var parser = new CsvTextFieldParser(path, Encoding.GetEncoding(charset)))
            {
                parser.SetDelimiter(delimiter[0]);
                parser.SetQuoteCharacter('"');
                parser.SetQuoteEscapeCharacter('"');
                parser.HasFieldsEnclosedInQuotes = true;
                parser.TrimWhiteSpace = true;

                if (parser.EndOfData)
                    throw new Exception("Empty glossary file");

                string[] firstLineFields = parser.ReadFields();
                var format = DetectFormat(firstLineFields);

                LoggingHelper.Info($"Detected glossary format is: {format.ToString()}");

                var concepts = format == GlossaryFormat.MemoQCsv
                    ? ParseMemoQFormat(parser, firstLineFields, includeLangs)
                    : ParseDeepLFormat(parser, firstLineFields, includeLangs);

                LoggingHelper.Info($"Obtain {concepts.Count} concept(s) from the glossary file");                

                return concepts;
            }
        }

        private static GlossaryFormat DetectFormat(string[] firstLineFields)
        {
            int memoQLangCount = firstLineFields
                .Where(field => LanguageHelper.GlossaryFieldNameToCodeDic.ContainsKey(field))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .Count();

            return memoQLangCount >= 2 ? GlossaryFormat.MemoQCsv : GlossaryFormat.DeepLCsv;
        }

        #endregion

        #region MemoQ Format

        private static List<Concept> ParseMemoQFormat(CsvTextFieldParser parser, string[] firstLineFields, HashSet<string> includeLangs = null)
        {
            var concepts = new List<Concept>();

            // 收集语言列
            var langIndicesDic = new Dictionary<string, List<int>>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < firstLineFields.Length; i++)
            {
                if (LanguageHelper.GlossaryFieldNameToCodeDic.TryGetValue(firstLineFields[i], out string langCode))
                {
                    // 过滤掉不需要的语言
                    if (includeLangs != null && !includeLangs.Contains(langCode))
                        continue;

                    LoggingHelper.Info($"Detected '{firstLineFields[i]}' in column {i + 1} from the glossary file");

                    if (!langIndicesDic.ContainsKey(langCode))
                        langIndicesDic[langCode] = new List<int>();

                    langIndicesDic[langCode].Add(i);
                }
            }

            while (!parser.EndOfData)
            {
                string[] fields = parser.ReadFields();
                var concept = new Concept { TermDic = new Dictionary<string, HashSet<string>>() };

                foreach (var kv in langIndicesDic)
                {
                    string langCode = kv.Key;
                    List<int> indices = kv.Value;

                    var terms = indices
                        .Where(idx => idx < fields.Length && !string.IsNullOrEmpty(fields[idx]))
                        .Select(idx => fields[idx])
                        .ToHashSet();

                    if (terms.Any())
                        concept.TermDic[langCode] = terms;
                }

                if (concept.TermDic.Any())
                    concepts.Add(concept);
            }

            return concepts;
        }

        #endregion

        #region DeepL Format

        private static List<Concept> ParseDeepLFormat(CsvTextFieldParser parser, string[] firstLineFields, HashSet<string> includeLangs = null)
        {
            var indices = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < firstLineFields.Length; i++)
            {
                string f = firstLineFields[i];
                if (f.Equals(_SOURCE_TERM_FIELD, StringComparison.OrdinalIgnoreCase)) indices[_SOURCE_TERM_FIELD] = i;
                else if (f.Equals(_TARGET_TERM_FIELD, StringComparison.OrdinalIgnoreCase)) indices[_TARGET_TERM_FIELD] = i;
                else if (f.Equals(_SOURCE_LANGUAGE_FIELD, StringComparison.OrdinalIgnoreCase)) indices[_SOURCE_LANGUAGE_FIELD] = i;
                else if (f.Equals(_TARGET_LANGUAGE_FIELD, StringComparison.OrdinalIgnoreCase)) indices[_TARGET_LANGUAGE_FIELD] = i;
            }

            bool hasHeader = indices.ContainsKey(_SOURCE_TERM_FIELD) && indices.ContainsKey(_TARGET_TERM_FIELD);

            LoggingHelper.Info($"Glossary file contains headers: {hasHeader}");

            int srcTermIdx = hasHeader ? indices[_SOURCE_TERM_FIELD] : 0;
            int tgtTermIdx = hasHeader ? indices[_TARGET_TERM_FIELD] : 1;
            int srcLangIdx = hasHeader ? (indices.ContainsKey(_SOURCE_LANGUAGE_FIELD) ? indices[_SOURCE_LANGUAGE_FIELD] : -1) : 2;
            int tgtLangIdx = hasHeader ? (indices.ContainsKey(_TARGET_LANGUAGE_FIELD) ? indices[_TARGET_LANGUAGE_FIELD] : -1) : 3;

            LoggingHelper.Info($"Glossary field index: " +
                $"{_SOURCE_TERM_FIELD}:{srcTermIdx + 1} {_TARGET_TERM_FIELD}:{tgtTermIdx + 1} " +
                $"{_SOURCE_LANGUAGE_FIELD}:{srcLangIdx + 1} {_TARGET_LANGUAGE_FIELD}:{tgtLangIdx + 1}");

            var concepts = new List<Concept>();

            if (!hasHeader)
            {
                ProcessDeepLRow(srcTermIdx, tgtTermIdx, srcLangIdx, tgtLangIdx, firstLineFields, concepts);
            }

            while (!parser.EndOfData)
            {
                ProcessDeepLRow(srcTermIdx, tgtTermIdx, srcLangIdx, tgtLangIdx, parser.ReadFields(), concepts);
            }

            if (includeLangs != null)
            {
                FilterConceptLanguages(includeLangs, concepts);
            }

            return concepts;
        }

        private static void ProcessDeepLRow(int srcTermIdx, int tgtTermIdx, int srcLangIdx, int tgtLangIdx, string[] fields, List<Concept> concepts)
        {
            string srcTerm = GetTermOrNull(srcTermIdx, fields);
            string tgtTerm = GetTermOrNull(tgtTermIdx, fields);

            string srcLang = GetLanguageOrAny(srcLangIdx, fields, true);
            string tgtLang = GetLanguageOrAny(tgtLangIdx, fields, false);

            // 源术语、目标术语都不存在
            if (srcTerm == null && tgtTerm == null)
            {
                return;
            }

            // 只有源术语不存在（只存在目标术语）
            if (srcTerm == null)
            {
                if (!IsInAnyConcept(tgtLang, tgtTerm, concepts))
                {
                    concepts.Add(CreateConcept(tgtLang, tgtTerm));
                }
                return;
            }

            // 只有目标术语不存在（只存在源术语）
            if (tgtTerm == null)
            {
                if (!IsInAnyConcept(srcLang, srcTerm, concepts))
                {
                    concepts.Add(CreateConcept(srcLang, srcTerm));
                }
                return;
            }

            // 同时存在源术语和目标术语，但至少其中一者的语言不存在（语言是 Any*）
            if (srcLang == _ANY_SOURCE_LANGUAGE || tgtLang == _ANY_TARGET_LANGUAGE)
            {
                if (!IsInAnyConcept(srcLang, srcTerm, tgtLang, tgtTerm, concepts))
                {
                    concepts.Add(CreateConcept(srcLang, srcTerm, tgtLang, tgtTerm));
                }
                return;
            }

            // 同时存在源术语和目标术语，且两者的语言都存在
            var conceptContainSrc = FindConceptExcludeAnyLanguage(srcLang, srcTerm, concepts);
            var conceptContainTgt = FindConceptExcludeAnyLanguage(tgtLang, tgtTerm, concepts);
            if (conceptContainSrc == null && conceptContainTgt == null)
            {
                concepts.Add(CreateConcept(srcLang, srcTerm, tgtLang, tgtTerm));
            }
            else if (conceptContainSrc == null && conceptContainTgt != null)
            {
                AddTermToConcept(srcLang, srcTerm, conceptContainTgt);
            }
            else if (conceptContainSrc != null && conceptContainTgt == null)
            {
                AddTermToConcept(tgtLang, tgtTerm, conceptContainSrc);
            }
            else if (conceptContainSrc != conceptContainTgt)
            {
                MergeConcepts(conceptContainSrc, conceptContainTgt);
                concepts.Remove(conceptContainSrc);
            }
        }

        private static string GetTermOrNull(int index, string[] fields)
        {
            return (index < 0 || index >= fields.Length || string.IsNullOrEmpty(fields[index]))
                ? null
                : fields[index];
        }

        private static string GetLanguageOrAny(int index, string[] fields, bool isSource)
        {
            return (index < 0 || index >= fields.Length || string.IsNullOrEmpty(fields[index]))
                    ? (isSource ? _ANY_SOURCE_LANGUAGE : _ANY_TARGET_LANGUAGE)
                    : fields[index];
        }

        private static bool IsInAnyConcept(string lang, string term, List<Concept> concepts)
        {
            return concepts.Any(c =>
                   c.TermDic.ContainsKey(lang)
                && c.TermDic[lang].Contains(term));
        }

        private static bool IsInAnyConcept(string srcLang, string srcTerm, string tgtLang, string tgtTerm, List<Concept> concepts)
        {
            return concepts.Any(c =>
                   c.TermDic.ContainsKey(srcLang)
                && c.TermDic.ContainsKey(tgtLang)
                && c.TermDic[srcLang].Contains(srcTerm)
                && c.TermDic[tgtLang].Contains(tgtTerm));
        }

        private static Concept CreateConcept(string lang, string term)
        {
            var termDic = new Dictionary<string, HashSet<string>>();
            termDic[lang] = new HashSet<string> { term };

            return new Concept { TermDic = termDic };
        }

        private static Concept CreateConcept(string srcLang, string srcTerm, string tgtLang, string tgtTerm)
        {
            var termDic = new Dictionary<string, HashSet<string>>();
            termDic[srcLang] = new HashSet<string> { srcTerm };
            termDic[tgtLang] = new HashSet<string> { tgtTerm };

            return new Concept { TermDic = termDic };
        }

        private static Concept FindConceptExcludeAnyLanguage(string lang, string term, List<Concept> concepts)
        {
            return concepts.FirstOrDefault(c =>
                   !c.TermDic.ContainsKey(_ANY_SOURCE_LANGUAGE)
                && !c.TermDic.ContainsKey(_ANY_TARGET_LANGUAGE)
                && c.TermDic.ContainsKey(lang) && c.TermDic[lang].Contains(term));
        }

        private static void AddTermToConcept(string lang, string term, Concept concept)
        {
            if (!concept.TermDic.ContainsKey(lang))
                concept.TermDic[lang] = new HashSet<string>();

            concept.TermDic[lang].Add(term);
        }

        private static void MergeConcepts(Concept from, Concept to)
        {
            foreach (var kv in from.TermDic)
            {
                if (!to.TermDic.ContainsKey(kv.Key))
                    to.TermDic[kv.Key] = new HashSet<string>();

                foreach (var term in kv.Value)
                    to.TermDic[kv.Key].Add(term);
            }
        }

        private static void FilterConceptLanguages(HashSet<string> includeLangs, List<Concept> concepts)
        {
            foreach (var concept in concepts)
            {
                var toRemove = concept.TermDic.Keys
                    .Where(lang => lang != _ANY_SOURCE_LANGUAGE
                                && lang != _ANY_TARGET_LANGUAGE
                                && !includeLangs.Contains(lang))
                    .ToList();

                foreach (var lang in toRemove)
                    concept.TermDic.Remove(lang);
            }
        }

        #endregion

        #region Data Structures

        private class Matcher
        {
            public Stemmer Stemmer { get; set; }

            public WordEncoder WordEncoder { get; set; }

            public AhoCorasickDoubleArrayTrie<TermHit> Automata { get; set; }
        }

        private class WordEncoder
        {
            private int _nextId = 1; // 0 保留给未知词
            private readonly Dictionary<string, char> _wordIdDic;

            public WordEncoder(bool ignoreCase = false)
            {
                _wordIdDic = ignoreCase
                    ? new Dictionary<string, char>(StringComparer.OrdinalIgnoreCase)
                    : new Dictionary<string, char>();
            }

            public string EncodeForBuild(List<string> words)
            {
                var sb = new StringBuilder();

                foreach (var word in words)
                {
                    if (_wordIdDic.TryGetValue(word, out char id))
                    {
                        sb.Append(id);
                        continue;
                    }

                    _nextId++;
                    if (_nextId > 65535)
                        throw new Exception("WordEncoder exceed the value, range 0 ~ 65535");

                    id = (char)_nextId;
                    _wordIdDic[word] = id;

                    sb.Append(id);
                }

                return sb.ToString();
            }

            public string EncodeForQuery(List<string> words)
            {
                var sb = new StringBuilder();

                foreach (var word in words)
                {
                    if (_wordIdDic.TryGetValue(word, out char id))
                    {
                        sb.Append(id);
                        continue;
                    }

                    sb.Append((char)0);
                }

                return sb.ToString();
            }
        }

        private struct TermHit
        {
            public string SourceTerm;

            public Concept Concept;
        }

        private class Concept
        {
            // key: 语言代码 value: 该语言下的术语列表
            public Dictionary<string, HashSet<string>> TermDic { get; set; }
        }

        private enum GlossaryFormat
        {
            MemoQCsv,   // 多语言列格式：English, English, Chinese_PRC, Chinese_PRC
            DeepLCsv    // 扁平格式：SourceTerm, TargetTerm, SourceLanguage, TargetLanguage
        }

        private class StemmerFactory
        {
            public static bool TryGet(string langCode, out Stemmer stemmer)
            {
                switch (langCode)
                {
                    //case "ara":
                    //case "ara-AE":
                    //case "ara-BH":
                    //case "ara-DZ":
                    //case "ara-EG":
                    //case "ara-IQ":
                    //case "ara-JO":
                    //case "ara-KW":
                    //case "ara-LB":
                    //case "ara-LY":
                    //case "ara-MA":
                    //case "ara-OM":
                    //case "ara-QA":
                    //case "ara-SA":
                    //case "ara-SY":
                    //case "ara-TN":
                    //case "ara-YE": stemmer = new ArabicStemmer(); return true; // UAX29 分词不完善
                    case "hye": stemmer = new ArmenianStemmer(); return true;
                    case "baq": stemmer = new BasqueStemmer(); return true;
                    case "cat": stemmer = new CatalanStemmer(); return true;
                    //case "cze": stemmer = new CzechStemmer(); return true; //缺少 Stemmer 实现
                    case "dan": stemmer = new DanishStemmer(); return true;
                    case "dut":
                    case "dut-BE":
                    case "dut-NL": stemmer = new DutchStemmer(); return true;
                    case "eng":
                    case "eng-AU":
                    case "eng-BZ":
                    case "eng-CA":
                    case "eng-CB":
                    case "eng-GB":
                    case "eng-IE":
                    case "eng-JM":
                    case "eng-NZ":
                    case "eng-PH":
                    case "eng-TT":
                    case "eng-US":
                    case "eng-ZA":
                    case "eng-ZW": stemmer = new EnglishStemmer(); return true;
                    case "epo": stemmer = new EsperantoStemmer(); return true;
                    case "est": stemmer = new EstonianStemmer(); return true;
                    case "fin": stemmer = new FinnishStemmer(); return true;
                    case "fre":
                    case "fre-02":
                    case "fre-BE":
                    case "fre-CA":
                    case "fre-CH":
                    case "fre-FR":
                    case "fre-LU":
                    case "fre-MA":
                    case "fre-MC": stemmer = new FrenchStemmer(); return true;
                    case "ger":
                    case "ger-AT":
                    case "ger-CH":
                    case "ger-DE":
                    case "ger-LI":
                    case "ger-LU": stemmer = new GermanStemmer(); return true;
                    case "gre": stemmer = new GreekStemmer(); return true;
                    //case "hin": stemmer = new HindiStemmer(); return true; // UAX29 分词不完善
                    case "hun": stemmer = new HungarianStemmer(); return true;
                    case "ind": stemmer = new IndonesianStemmer(); return true;
                    case "gle": stemmer = new IrishStemmer(); return true;
                    case "ita":
                    case "ita-CH":
                    case "ita-IT": stemmer = new ItalianStemmer(); return true;
                    case "lit": stemmer = new LithuanianStemmer(); return true;
                    //case "nep": stemmer = new NepaliStemmer(); return true; // UAX29 分词不完善
                    case "nnb":
                    case "nno":
                    case "nor": stemmer = new NorwegianStemmer(); return true;
                    //case "pol": stemmer = new PolishStemmer(); return true; // 缺少 Stemmer 实现
                    //case "fas": stemmer = new PersianStemmer(); return true; // 缺少 Stemmer 实现、UAX29 分词不完善
                    case "por":
                    case "por-BR":
                    case "por-PT": stemmer = new PortugueseStemmer(); return true;
                    case "rum": stemmer = new RomanianStemmer(); return true;
                    case "rus": stemmer = new RussianStemmer(); return true;
                    case "scc":
                    case "scr": stemmer = new SerbianStemmer(); return true;
                    case "spa":
                    case "spa-AR":
                    case "spa-BO":
                    case "spa-CL":
                    case "spa-CO":
                    case "spa-CR":
                    case "spa-DO":
                    case "spa-EC":
                    case "spa-ES":
                    case "spa-GT":
                    case "spa-HN":
                    case "spa-M9":
                    case "spa-MX":
                    case "spa-NI":
                    case "spa-PA":
                    case "spa-PE":
                    case "spa-PR":
                    case "spa-PY":
                    case "spa-SV":
                    case "spa-US":
                    case "spa-UY":
                    case "spa-VE": stemmer = new SpanishStemmer(); return true;
                    case "swe":
                    case "swe-FI":
                    case "swe-SE": stemmer = new SwedishStemmer(); return true;
                    //case "tam": stemmer = new TamilStemmer(); return true; // UAX29 分词不完善
                    case "tur": stemmer = new TurkishStemmer(); return true;
                    case "yid": stemmer = new YiddishStemmer(); return true;
                    default: stemmer = null; return false;
                }
            }
        }
        #endregion
    }

}
