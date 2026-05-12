namespace MultiSupplierMTPlugin.Forms
{
    partial class OpenAICompatibleProvider
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.comboBoxProviders = new System.Windows.Forms.ComboBox();
            this.labelProviders = new System.Windows.Forms.Label();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.labelAction = new System.Windows.Forms.Label();
            this.textBoxApiKeyLink = new System.Windows.Forms.TextBox();
            this.textBoxBaseURL = new System.Windows.Forms.TextBox();
            this.textBoxDocLink = new System.Windows.Forms.TextBox();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.textBoxModelList = new System.Windows.Forms.TextBox();
            this.textBoxModelsLink = new System.Windows.Forms.TextBox();
            this.textBoxDefaultModel = new System.Windows.Forms.TextBox();
            this.labelBaseURL = new System.Windows.Forms.Label();
            this.labelPath = new System.Windows.Forms.Label();
            this.labelDefaultModel = new System.Windows.Forms.Label();
            this.labelModelList = new System.Windows.Forms.Label();
            this.labelModelsLink = new System.Windows.Forms.Label();
            this.labelApiKeyLink = new System.Windows.Forms.Label();
            this.labelDocLink = new System.Windows.Forms.Label();
            this.groupBoxOptional = new System.Windows.Forms.GroupBox();
            this.groupBoxRecommended = new System.Windows.Forms.GroupBox();
            this.groupBoxRequired = new System.Windows.Forms.GroupBox();
            this.labelDisplayName = new System.Windows.Forms.Label();
            this.textBoxDisplayName = new System.Windows.Forms.TextBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBoxOptional.SuspendLayout();
            this.groupBoxRecommended.SuspendLayout();
            this.groupBoxRequired.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(425, 582);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(103, 27);
            this.buttonCancel.TabIndex = 25;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(316, 582);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(100, 27);
            this.buttonOK.TabIndex = 24;
            this.buttonOK.Text = "&OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // comboBoxProviders
            // 
            this.comboBoxProviders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProviders.FormattingEnabled = true;
            this.comboBoxProviders.Location = new System.Drawing.Point(152, 55);
            this.comboBoxProviders.Name = "comboBoxProviders";
            this.comboBoxProviders.Size = new System.Drawing.Size(364, 23);
            this.comboBoxProviders.TabIndex = 4;
            this.comboBoxProviders.SelectedIndexChanged += new System.EventHandler(this.comboBoxProviders_SelectedIndexChanged);
            // 
            // labelProviders
            // 
            this.labelProviders.Location = new System.Drawing.Point(23, 57);
            this.labelProviders.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelProviders.Name = "labelProviders";
            this.labelProviders.Size = new System.Drawing.Size(120, 18);
            this.labelProviders.TabIndex = 3;
            this.labelProviders.Text = "Providers";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(152, 10);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(76, 27);
            this.buttonAdd.TabIndex = 1;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(441, 10);
            this.buttonDelete.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(74, 27);
            this.buttonDelete.TabIndex = 2;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // labelAction
            // 
            this.labelAction.Location = new System.Drawing.Point(23, 14);
            this.labelAction.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAction.Name = "labelAction";
            this.labelAction.Size = new System.Drawing.Size(120, 18);
            this.labelAction.TabIndex = 0;
            this.labelAction.Text = "Action";
            // 
            // textBoxApiKeyLink
            // 
            this.textBoxApiKeyLink.Location = new System.Drawing.Point(136, 29);
            this.textBoxApiKeyLink.Name = "textBoxApiKeyLink";
            this.textBoxApiKeyLink.Size = new System.Drawing.Size(364, 25);
            this.textBoxApiKeyLink.TabIndex = 19;
            this.textBoxApiKeyLink.TextChanged += new System.EventHandler(this.textBoxApiKeyURL_TextChanged);
            // 
            // textBoxBaseURL
            // 
            this.textBoxBaseURL.Location = new System.Drawing.Point(136, 73);
            this.textBoxBaseURL.Name = "textBoxBaseURL";
            this.textBoxBaseURL.Size = new System.Drawing.Size(364, 25);
            this.textBoxBaseURL.TabIndex = 9;
            this.textBoxBaseURL.TextChanged += new System.EventHandler(this.textBoxBaseURL_TextChanged);
            // 
            // textBoxDocLink
            // 
            this.textBoxDocLink.Location = new System.Drawing.Point(136, 120);
            this.textBoxDocLink.Name = "textBoxDocLink";
            this.textBoxDocLink.Size = new System.Drawing.Size(364, 25);
            this.textBoxDocLink.TabIndex = 23;
            this.textBoxDocLink.TextChanged += new System.EventHandler(this.textBoxHelpURL_TextChanged);
            // 
            // textBoxPath
            // 
            this.textBoxPath.Location = new System.Drawing.Point(136, 116);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(364, 25);
            this.textBoxPath.TabIndex = 11;
            this.textBoxPath.TextChanged += new System.EventHandler(this.textBoxPath_TextChanged);
            // 
            // textBoxModelList
            // 
            this.textBoxModelList.Location = new System.Drawing.Point(136, 76);
            this.textBoxModelList.Name = "textBoxModelList";
            this.textBoxModelList.Size = new System.Drawing.Size(364, 25);
            this.textBoxModelList.TabIndex = 16;
            this.textBoxModelList.TextChanged += new System.EventHandler(this.textBoxModels_TextChanged);
            // 
            // textBoxModelsLink
            // 
            this.textBoxModelsLink.Location = new System.Drawing.Point(136, 75);
            this.textBoxModelsLink.Name = "textBoxModelsLink";
            this.textBoxModelsLink.Size = new System.Drawing.Size(364, 25);
            this.textBoxModelsLink.TabIndex = 21;
            this.textBoxModelsLink.TextChanged += new System.EventHandler(this.textBoxModelsURL_TextChanged);
            // 
            // textBoxDefaultModel
            // 
            this.textBoxDefaultModel.Location = new System.Drawing.Point(136, 32);
            this.textBoxDefaultModel.Name = "textBoxDefaultModel";
            this.textBoxDefaultModel.Size = new System.Drawing.Size(364, 25);
            this.textBoxDefaultModel.TabIndex = 14;
            this.textBoxDefaultModel.TextChanged += new System.EventHandler(this.textBoxModel_TextChanged);
            // 
            // labelBaseURL
            // 
            this.labelBaseURL.Location = new System.Drawing.Point(9, 76);
            this.labelBaseURL.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelBaseURL.Name = "labelBaseURL";
            this.labelBaseURL.Size = new System.Drawing.Size(120, 18);
            this.labelBaseURL.TabIndex = 8;
            this.labelBaseURL.Text = "Base URL";
            // 
            // labelPath
            // 
            this.labelPath.Location = new System.Drawing.Point(9, 119);
            this.labelPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(120, 18);
            this.labelPath.TabIndex = 10;
            this.labelPath.Text = "Path";
            // 
            // labelDefaultModel
            // 
            this.labelDefaultModel.Location = new System.Drawing.Point(9, 35);
            this.labelDefaultModel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDefaultModel.Name = "labelDefaultModel";
            this.labelDefaultModel.Size = new System.Drawing.Size(120, 18);
            this.labelDefaultModel.TabIndex = 13;
            this.labelDefaultModel.Text = "Default Model";
            // 
            // labelModelList
            // 
            this.labelModelList.Location = new System.Drawing.Point(9, 79);
            this.labelModelList.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelModelList.Name = "labelModelList";
            this.labelModelList.Size = new System.Drawing.Size(120, 18);
            this.labelModelList.TabIndex = 15;
            this.labelModelList.Text = "Model List";
            // 
            // labelModelsLink
            // 
            this.labelModelsLink.Location = new System.Drawing.Point(9, 78);
            this.labelModelsLink.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelModelsLink.Name = "labelModelsLink";
            this.labelModelsLink.Size = new System.Drawing.Size(120, 18);
            this.labelModelsLink.TabIndex = 20;
            this.labelModelsLink.Text = "Models Link";
            // 
            // labelApiKeyLink
            // 
            this.labelApiKeyLink.Location = new System.Drawing.Point(9, 32);
            this.labelApiKeyLink.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelApiKeyLink.Name = "labelApiKeyLink";
            this.labelApiKeyLink.Size = new System.Drawing.Size(120, 18);
            this.labelApiKeyLink.TabIndex = 18;
            this.labelApiKeyLink.Text = "Api Key Link";
            // 
            // labelDocLink
            // 
            this.labelDocLink.Location = new System.Drawing.Point(9, 123);
            this.labelDocLink.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDocLink.Name = "labelDocLink";
            this.labelDocLink.Size = new System.Drawing.Size(120, 18);
            this.labelDocLink.TabIndex = 22;
            this.labelDocLink.Text = "Doc Link";
            // 
            // groupBoxOptional
            // 
            this.groupBoxOptional.Controls.Add(this.labelApiKeyLink);
            this.groupBoxOptional.Controls.Add(this.labelDocLink);
            this.groupBoxOptional.Controls.Add(this.textBoxApiKeyLink);
            this.groupBoxOptional.Controls.Add(this.textBoxDocLink);
            this.groupBoxOptional.Controls.Add(this.labelModelsLink);
            this.groupBoxOptional.Controls.Add(this.textBoxModelsLink);
            this.groupBoxOptional.Location = new System.Drawing.Point(16, 398);
            this.groupBoxOptional.Name = "groupBoxOptional";
            this.groupBoxOptional.Size = new System.Drawing.Size(512, 165);
            this.groupBoxOptional.TabIndex = 17;
            this.groupBoxOptional.TabStop = false;
            this.groupBoxOptional.Text = "Optional option(s)";
            // 
            // groupBoxRecommended
            // 
            this.groupBoxRecommended.Controls.Add(this.labelDefaultModel);
            this.groupBoxRecommended.Controls.Add(this.textBoxModelList);
            this.groupBoxRecommended.Controls.Add(this.labelModelList);
            this.groupBoxRecommended.Controls.Add(this.textBoxDefaultModel);
            this.groupBoxRecommended.Location = new System.Drawing.Point(16, 266);
            this.groupBoxRecommended.Name = "groupBoxRecommended";
            this.groupBoxRecommended.Size = new System.Drawing.Size(512, 122);
            this.groupBoxRecommended.TabIndex = 12;
            this.groupBoxRecommended.TabStop = false;
            this.groupBoxRecommended.Text = "Recommended option(s)";
            // 
            // groupBoxRequired
            // 
            this.groupBoxRequired.Controls.Add(this.labelDisplayName);
            this.groupBoxRequired.Controls.Add(this.textBoxDisplayName);
            this.groupBoxRequired.Controls.Add(this.labelBaseURL);
            this.groupBoxRequired.Controls.Add(this.textBoxBaseURL);
            this.groupBoxRequired.Controls.Add(this.textBoxPath);
            this.groupBoxRequired.Controls.Add(this.labelPath);
            this.groupBoxRequired.Location = new System.Drawing.Point(16, 95);
            this.groupBoxRequired.Name = "groupBoxRequired";
            this.groupBoxRequired.Size = new System.Drawing.Size(512, 161);
            this.groupBoxRequired.TabIndex = 5;
            this.groupBoxRequired.TabStop = false;
            this.groupBoxRequired.Text = "Required option(s)";
            // 
            // labelDisplayName
            // 
            this.labelDisplayName.Location = new System.Drawing.Point(9, 33);
            this.labelDisplayName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDisplayName.Name = "labelDisplayName";
            this.labelDisplayName.Size = new System.Drawing.Size(120, 18);
            this.labelDisplayName.TabIndex = 6;
            this.labelDisplayName.Text = "Name";
            // 
            // textBoxDisplayName
            // 
            this.textBoxDisplayName.Location = new System.Drawing.Point(136, 30);
            this.textBoxDisplayName.Name = "textBoxDisplayName";
            this.textBoxDisplayName.Size = new System.Drawing.Size(364, 25);
            this.textBoxDisplayName.TabIndex = 7;
            this.textBoxDisplayName.LostFocus += new System.EventHandler(this.textBoxDisplayName_LostFocus);
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 5000;
            this.toolTip.InitialDelay = 100;
            this.toolTip.ReshowDelay = 100;
            // 
            // OpenAICompatibleProvider
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(540, 621);
            this.Controls.Add(this.groupBoxRequired);
            this.Controls.Add(this.groupBoxRecommended);
            this.Controls.Add(this.groupBoxOptional);
            this.Controls.Add(this.labelAction);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.labelProviders);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.comboBoxProviders);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OpenAICompatibleProvider";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Custom OpenAI Compatible Providers Manage";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OpenAICompatibleProvider_FormClosing);
            this.groupBoxOptional.ResumeLayout(false);
            this.groupBoxOptional.PerformLayout();
            this.groupBoxRecommended.ResumeLayout(false);
            this.groupBoxRecommended.PerformLayout();
            this.groupBoxRequired.ResumeLayout(false);
            this.groupBoxRequired.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.ComboBox comboBoxProviders;
        private System.Windows.Forms.Label labelProviders;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Label labelAction;
        private System.Windows.Forms.TextBox textBoxApiKeyLink;
        private System.Windows.Forms.TextBox textBoxBaseURL;
        private System.Windows.Forms.TextBox textBoxDocLink;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.TextBox textBoxModelList;
        private System.Windows.Forms.TextBox textBoxModelsLink;
        private System.Windows.Forms.TextBox textBoxDefaultModel;
        private System.Windows.Forms.Label labelBaseURL;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.Label labelDefaultModel;
        private System.Windows.Forms.Label labelModelList;
        private System.Windows.Forms.Label labelModelsLink;
        private System.Windows.Forms.Label labelApiKeyLink;
        private System.Windows.Forms.Label labelDocLink;
        private System.Windows.Forms.GroupBox groupBoxOptional;
        private System.Windows.Forms.GroupBox groupBoxRecommended;
        private System.Windows.Forms.GroupBox groupBoxRequired;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TextBox textBoxDisplayName;
        private System.Windows.Forms.Label labelDisplayName;
    }
}