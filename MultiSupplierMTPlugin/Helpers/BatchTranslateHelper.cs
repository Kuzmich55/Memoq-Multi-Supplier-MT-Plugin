using MultiSupplierMTPlugin.ProvidersCommon.Options.LLM;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiSupplierMTPlugin.Helpers
{
    class BatchTranslateHelper
    {
        private static Dictionary<string, object> _jsonSchemaShorter = new Dictionary<string, object>
        {
            ["name"] = "translation_result_map",
            ["description"] = "Map of sentence IDs to their translations.",
            ["strict"] = true,
            ["schema"] = new Dictionary<string, object>
            {
                ["type"] = "object",
                ["properties"] = new Dictionary<string, object>(),
                ["additionalProperties"] = new Dictionary<string, object> { ["type"] = "string" },
            }
        };

        private static Dictionary<string, object> _jsonSchemaLonger = new Dictionary<string, object>
        {
            ["name"] = "translation_result_list",
            ["description"] = "List of original texts with IDs and their translations.",
            ["strict"] = true,
            ["schema"] = new Dictionary<string, object>
            {
                ["type"] = "object",
                ["properties"] = new Dictionary<string, object>
                {
                    ["texts"] = new Dictionary<string, object>
                    {
                        ["type"] = "array",
                        ["items"] = new Dictionary<string, object>
                        {
                            ["type"] = "object",
                            ["properties"] = new Dictionary<string, object>
                            {
                                ["id"] = new Dictionary<string, object> { ["type"] = "integer" },
                                ["text"] = new Dictionary<string, object> { ["type"] = "string" }
                            },
                            ["required"] = new[] { "id", "text" },
                            ["additionalProperties"] = false
                        }
                        //["minItems"] = 1
                        //["maxItems"] = 
                    }
                },
                ["required"] = new[] { "texts" },
                ["additionalProperties"] = false
            }
        };


        public static Dictionary<string, object> GetJsonScheme(BatchTranslateSchema schema)
        {
            return schema == BatchTranslateSchema.Longer ? _jsonSchemaLonger : _jsonSchemaShorter;
        }

        public static string Serialize(BatchTranslateSchema schema, List<string> texts)
        {
            if (schema == BatchTranslateSchema.Longer)
            {
                var textEntities = new SchemaLongerEntity
                {
                    Texts = texts.Select((text, index) => new SchemaLongerItem { Id = index + 1, Text = text })
                    .ToArray()
                };
                return JsonConvert.SerializeObject(textEntities);
            }

            var textMap = texts.Select((text, index) => new { Key = (index + 1).ToString(), Value = text })
                   .ToDictionary(pair => pair.Key, pair => pair.Value);
            return JsonConvert.SerializeObject(textMap);
        }

        public static List<string> Deserialize(BatchTranslateSchema schema, BatchTranslateResponseFormat format, int count, string content)
        {
            if (format == BatchTranslateResponseFormat.Text)
            {
                int firstBrace = content.IndexOf('{');
                int lastBrace = content.LastIndexOf('}');

                bool hasBraces = firstBrace != -1 && lastBrace > firstBrace;
                bool needsTrim = firstBrace > 0 || lastBrace < content.Length - 1;

                if (hasBraces && needsTrim)
                    content = content.Substring(firstBrace, lastBrace - firstBrace + 1);
            }

            object deserialized;
            try
            {
                if (schema == BatchTranslateSchema.Longer)
                    deserialized = JsonConvert.DeserializeObject<SchemaLongerEntity>(content);
                else
                    deserialized = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
            }
            catch (Exception ex)
            {
                throw new Exception($"Invalid JSON format in batch translation response. Please check your prompt or batch translation settings. Original error: {ex.Message}", ex);
            }

            string[] results = new string[count];

            if (schema == BatchTranslateSchema.Longer)
            {
                var items = ((SchemaLongerEntity)deserialized).Texts;
                
                if (items == null)
                    throw new Exception("Missing 'texts' array in batch translation response.");

                if (items.Length != count)
                    throw new Exception($"The number of batch translation items is incorrect. Expected {count} but got {items.Length}.");

                foreach (var item in items)
                {
                    int index = item.Id - 1;

                    if (index < 0 || index >= count || results[index] != null)
                        throw new Exception($"Invalid item ID in batch translation response. must be 1 to {count} and unique.");

                    results[index] = item.Text;
                }
            }
            else
            {
                var map = (Dictionary<string, string>)deserialized;

                if (map.Count != count)
                    throw new Exception($"The number of batch translation items is incorrect. Expected {count} but got {map.Count}.");

                foreach (var kv in map)
                {
                    if (!int.TryParse(kv.Key, out int id))
                        throw new Exception($"Invalid item ID in batch translation response. '{kv.Key}' is not a valid number.");

                    int index = id - 1;

                    if (index < 0 || index >= count || results[index] != null)
                        throw new Exception($"Invalid item ID in batch translation response. must be 1 to {count} and unique.");

                    results[index] = kv.Value;
                }
            }

            return results.ToList();
        }


        private class SchemaLongerEntity
        {
            [JsonProperty("texts")]
            public SchemaLongerItem[] Texts { get; set; }
        }

        private class SchemaLongerItem
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("text")]
            public string Text { get; set; }
        }
    }
}
