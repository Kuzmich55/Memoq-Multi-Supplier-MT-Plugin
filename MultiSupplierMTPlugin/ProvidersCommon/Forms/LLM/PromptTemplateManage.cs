using MultiSupplierMTPlugin.Helpers;
using MultiSupplierMTPlugin.Localized;
using MultiSupplierMTPlugin.ProvidersCommon.Options.LLM;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using LLH = MultiSupplierMTPlugin.Localized.LocalizedHelper;
using LLK = MultiSupplierMTPlugin.ProvidersCommon.Forms.LLM.PromptTemplateManageLocalizedKey;
using LLKC = MultiSupplierMTPlugin.Localized.LocalizedKeyCommon;

namespace MultiSupplierMTPlugin.ProvidersCommon.Forms.LLM
{
    partial class PromptTemplateManage : Form
    {
        private MultiSupplierMTGeneralSettings _mtGeneralSettings;

        private MultiSupplierMTSecureSettings _mtSecureSettings;

        private BindingList<PromptTemplate> _promptTemplates;

        private string _currentPromptId;


        public PromptTemplateManage(MultiSupplierMTGeneralSettings mtGeneralSettings, MultiSupplierMTSecureSettings mtSecureSettings, string currentPromptId)
        {
            InitializeComponent();

            this._mtGeneralSettings = mtGeneralSettings;
            this._mtSecureSettings = mtSecureSettings;

            this._currentPromptId = currentPromptId;
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Localized();

            LoadOptions();
        }


        private void Localized()
        {
            var textBoxPromptMenu = PromptHelper.CreateTextBoxContextMenu();

            Text = LLH.G(LLK.Form);

            labelAction.Text = LLH.G(LLK.LabelAction);
            buttonAdd.Text = LLH.G(LLK.ButtonAdd);
            buttonDelete.Text = LLH.G(LLK.ButtonDelete);

            labelTemplates.Text = LLH.G(LLK.LabelTemplates);

            labelName.Text = LLH.G(LLK.LabelName);

            groupBoxSingleTranslate.Text = LLH.G(LLK.GroupBoxSingleTranslate);
            labelSystemPrompt.Text = LLH.G(LLK.LabelSystemPrompt);
            labelUserPrompt.Text = LLH.G(LLK.LabelUserPrompt);

            toolTip.SetToolTip(labelSystemPrompt, LLH.G(LLKC.ToolTip_LLMPromptTip));
            toolTip.SetToolTip(labelUserPrompt, LLH.G(LLKC.ToolTip_LLMPromptTip));

            textBoxSystemPrompt.ContextMenuStrip = textBoxPromptMenu;
            textBoxUserPrompt.ContextMenuStrip = textBoxPromptMenu;

            groupBoxBatchTranslate.Text = LLH.G(LLK.GroupBoxBatchTranslate);
            labelBatchTranslateSystemPrompt.Text = LLH.G(LLK.LabelBatchTranslateSystemPrompt);
            labelBatchTranslateUserPrompt.Text = LLH.G(LLK.LabelBatchTranslateUserPrompt);

            toolTip.SetToolTip(labelBatchTranslateSystemPrompt, LLH.G(LLKC.ToolTip_LLMPromptTip));
            toolTip.SetToolTip(labelBatchTranslateUserPrompt, LLH.G(LLKC.ToolTip_LLMPromptTip));

            textBoxBatchTranslateSystemPrompt.ContextMenuStrip = textBoxPromptMenu;
            textBoxBatchTranslateUserPrompt.ContextMenuStrip = textBoxPromptMenu;

            buttonOK.Text = LLH.G(LLKC.ButtonOK);
            buttonCancel.Text = LLH.G(LLKC.ButtonCancel);
        }

        private void LoadOptions()
        {
            _promptTemplates = new BindingList<PromptTemplate>(
                _mtGeneralSettings.LLMCommon.PromptTemplates
                .Select(p => p.Clone())
                .OrderBy(p => p.Name, new NaturalSortComparer())
                .ToList());

            comboBoxTemplates.DataSource = _promptTemplates;
            comboBoxTemplates.DisplayMember = "Name";

            var promptTemplate = _promptTemplates.Where(p => p.ID == _currentPromptId).FirstOrDefault();
            if (promptTemplate != null)
            {
                comboBoxTemplates.SelectedItem = promptTemplate;
            }

            UpdateControlState();
        }

        private void UpdateControlState()
        {
            bool hasSel = comboBoxTemplates.SelectedIndex != -1;
            bool noEmpty = _promptTemplates.Count > 0;

            buttonDelete.Enabled = hasSel;

            comboBoxTemplates.Enabled = noEmpty;

            textBoxName.Enabled = hasSel;

            textBoxSystemPrompt.Enabled = hasSel;
            textBoxUserPrompt.Enabled = hasSel;

            textBoxBatchTranslateSystemPrompt.Enabled = hasSel;
            textBoxBatchTranslateUserPrompt.Enabled = hasSel;
        }

        private void comboBoxTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            var prompt = comboBoxTemplates.SelectedItem as PromptTemplate;

            textBoxName.Text = prompt?.Name ?? "";

            textBoxSystemPrompt.Text = prompt?.SystemPrompt
              .Replace(Environment.NewLine, "\n").Replace("\n", Environment.NewLine) ?? ""; // 解决 xml 反序列化后换行符总是变成 \n
            textBoxUserPrompt.Text = prompt?.UserPrompt
                .Replace(Environment.NewLine, "\n").Replace("\n", Environment.NewLine) ?? ""; // 解决 xml 反序列化后换行符总是变成 \n;

            textBoxBatchTranslateSystemPrompt.Text = prompt?.BathTranslateSystemPrompt
             .Replace(Environment.NewLine, "\n").Replace("\n", Environment.NewLine) ?? ""; // 解决 xml 反序列化后换行符总是变成 \n
            textBoxBatchTranslateUserPrompt.Text = prompt?.BathTranslateUserPrompt
                .Replace(Environment.NewLine, "\n").Replace("\n", Environment.NewLine) ?? ""; // 解决 xml 反序列化后换行符总是变成 \n;
        }


        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string newName = Enumerable.Range(1, int.MaxValue)
               .Select(i => $"New {i}")
               .First(name => !_promptTemplates.Any(p => p.Name == name));

            bool firstNewTpl = _promptTemplates.Count == 0;
            var defaultTpl = PromptTemplate.GetDefault();
            _promptTemplates.Add(new PromptTemplate()
            {
                ID = Guid.NewGuid().ToString(),
                Name = newName,
                SystemPrompt = firstNewTpl ? defaultTpl.SystemPrompt : "",
                UserPrompt = firstNewTpl ? defaultTpl.UserPrompt : "",
                BathTranslateSystemPrompt = firstNewTpl ? defaultTpl.BathTranslateSystemPrompt : "",
                BathTranslateUserPrompt = firstNewTpl ? defaultTpl.BathTranslateUserPrompt : ""
            });
            UpdateControlState();

            if (comboBoxTemplates.Items.Count > 0)
                comboBoxTemplates.SelectedIndex = comboBoxTemplates.Items.Count - 1;

            comboBoxTemplates_SelectedIndexChanged(comboBoxTemplates, EventArgs.Empty);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int index = comboBoxTemplates.SelectedIndex;
            if (index >= 0 && index < _promptTemplates.Count)
            {
                _promptTemplates.RemoveAt(index);
                UpdateControlState();
                comboBoxTemplates_SelectedIndexChanged(comboBoxTemplates, EventArgs.Empty);
            }
        }

        private void textBoxName_LostFocus(object sender, EventArgs e)
        {
            if (comboBoxTemplates.SelectedItem is PromptTemplate prompt)
            {
                var name = textBoxName.Text;

                if (_promptTemplates.Any(p => p.Name == name && prompt.Name != name))
                {
                    MessageBox.Show(LLH.G(LLK.MessageAlreadyExists), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxName.Text = prompt.Name;
                    return;
                }

                prompt.Name = name;
                ((CurrencyManager)BindingContext[comboBoxTemplates.DataSource]).Refresh();
            }
        }

        private void textBoxSystemPrompt_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxTemplates.SelectedItem is PromptTemplate prompt)
            {
                prompt.SystemPrompt = textBoxSystemPrompt.Text;
                textBoxSystemPrompt.ScrollBars = textBoxSystemPrompt.Lines.Length > 6 ? ScrollBars.Vertical : ScrollBars.None;
            }
        }

        private void textBoxUserPrompt_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxTemplates.SelectedItem is PromptTemplate prompt)
            {
                prompt.UserPrompt = textBoxUserPrompt.Text;
                textBoxUserPrompt.ScrollBars = textBoxUserPrompt.Lines.Length > 6 ? ScrollBars.Vertical : ScrollBars.None;
            }
        }


        private void textBoxBatchTranslateSystemPrompt_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxTemplates.SelectedItem is PromptTemplate prompt)
            {
                prompt.BathTranslateSystemPrompt = textBoxBatchTranslateSystemPrompt.Text;
                textBoxBatchTranslateSystemPrompt.ScrollBars = textBoxBatchTranslateSystemPrompt.Lines.Length > 6 ? ScrollBars.Vertical : ScrollBars.None;
            }
        }

        private void textBoxBatchTranslateUserPrompt_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxTemplates.SelectedItem is PromptTemplate prompt)
            {
                prompt.BathTranslateUserPrompt = textBoxBatchTranslateUserPrompt.Text;
                textBoxBatchTranslateUserPrompt.ScrollBars = textBoxBatchTranslateUserPrompt.Lines.Length > 6 ? ScrollBars.Vertical : ScrollBars.None;
            }
        }


        private void buttonHelp_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("https://github.com/JuchiaLu/Multi-Supplier-MT-Plugin");
            }
            catch
            {
                // do nothing
            }
        }


        private void PromptTemplateManage_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
               _mtGeneralSettings.LLMCommon.PromptTemplates = _promptTemplates.ToArray();
            }
        }
    }

    class PromptTemplateManageLocalizedKey : LocalizedKeyBase
    {
        public PromptTemplateManageLocalizedKey(string name) : base(name)
        {
        }

        static PromptTemplateManageLocalizedKey()
        {
            AutoInit<PromptTemplateManageLocalizedKey>();
        }

        [LocalizedValue("ffbeb939-9b25-4b44-bd30-604ad2c0b2ba", "Prompt Templates (Shared by all LLM providers)", "提示词模板（所有 LLM 提供商共用）")]
        public static PromptTemplateManageLocalizedKey Form { get; private set; }

        [LocalizedValue("227804b2-297d-4c7c-9f16-bcb4306ca076", "Action", "动作")]
        public static PromptTemplateManageLocalizedKey LabelAction { get; private set; }

        [LocalizedValue("776f2388-a89a-4e10-a6ce-11d9f6ec82cf", "Add", "新添")]
        public static PromptTemplateManageLocalizedKey ButtonAdd { get; private set; }

        [LocalizedValue("7e313363-cc3d-47e7-bd78-3428ce73fd7e", "Delete", "删除")]
        public static PromptTemplateManageLocalizedKey ButtonDelete { get; private set; }

        [LocalizedValue("c6be6ebf-5421-4640-84c6-e06f9f38a74e", "Templates", "模板")]
        public static PromptTemplateManageLocalizedKey LabelTemplates { get; private set; }

        [LocalizedValue("4c2687ea-c162-4a4f-8dc2-d7bd3770683c", "Name", "名称")]
        public static PromptTemplateManageLocalizedKey LabelName { get; private set; }

        [LocalizedValue("7261c57b-b57a-4c99-864a-d469e973cb28", "Use For Single Translate", "用于单段翻译")]
        public static PromptTemplateManageLocalizedKey GroupBoxSingleTranslate { get; private set; }

        [LocalizedValue("58afc667-1e31-407a-a0c0-6a47118ce089", "System Prompt^", "系统提示词^")]
        public static PromptTemplateManageLocalizedKey LabelSystemPrompt { get; private set; }

        [LocalizedValue("aa1943dd-abdc-4a13-b6f6-293144219aaf", "User Prompt^", "用户提示词^")]
        public static PromptTemplateManageLocalizedKey LabelUserPrompt { get; private set; }

        [LocalizedValue("86643d6d-60d7-4657-ade2-8a8929892dac", "Use For Batch Translate", "用于批量翻译")]
        public static PromptTemplateManageLocalizedKey GroupBoxBatchTranslate { get; private set; }

        [LocalizedValue("87c5b641-ad6e-4e26-899b-9ef7317ac40d", "System Prompt^", "系统提示词^")]
        public static PromptTemplateManageLocalizedKey LabelBatchTranslateSystemPrompt { get; private set; }

        [LocalizedValue("51da8b07-42b8-459e-adc7-853fe2ebb782", "User Prompt^", "用户提示词^")]
        public static PromptTemplateManageLocalizedKey LabelBatchTranslateUserPrompt { get; private set; }

        [LocalizedValue("f7e49fcd-990c-4166-999d-c0dee50fd85e", "Name already exists", "名称已存在")]
        public static PromptTemplateManageLocalizedKey MessageAlreadyExists { get; private set; }
    }
}
