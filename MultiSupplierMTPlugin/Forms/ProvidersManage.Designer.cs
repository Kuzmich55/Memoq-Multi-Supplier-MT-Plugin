using System.Drawing;
using System.Windows.Forms;

namespace MultiSupplierMTPlugin.Forms
{
    partial class ProvidersManage
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.dataGridViewEnable = new System.Windows.Forms.DataGridView();
            this.buttonEnable = new System.Windows.Forms.Button();
            this.buttonDisable = new System.Windows.Forms.Button();
            this.dataGridViewDisable = new System.Windows.Forms.DataGridView();
            this.labelNoDisableTip = new System.Windows.Forms.Label();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.labelSearch = new System.Windows.Forms.Label();
            this.linkLabelMore = new System.Windows.Forms.LinkLabel();
            this.groupBoxEnableList = new System.Windows.Forms.GroupBox();
            this.groupBoxDisableList = new System.Windows.Forms.GroupBox();
            this.labelDisabledProviders = new System.Windows.Forms.Label();
            this.labelEnabledProviders = new System.Windows.Forms.Label();
            this.linkLabelReset = new System.Windows.Forms.LinkLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEnable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDisable)).BeginInit();
            this.groupBoxEnableList.SuspendLayout();
            this.groupBoxDisableList.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.AutoSize = true;
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(600, 600);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(100, 27);
            this.buttonOK.TabIndex = 14;
            this.buttonOK.Text = "&OK";
            // 
            // buttonCancel
            // 
            this.buttonCancel.AutoSize = true;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(705, 600);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 27);
            this.buttonCancel.TabIndex = 15;
            this.buttonCancel.Text = "&Cancel";
            // 
            // dataGridViewEnable
            // 
            this.dataGridViewEnable.AllowUserToAddRows = false;
            this.dataGridViewEnable.AllowUserToDeleteRows = false;
            this.dataGridViewEnable.AllowUserToOrderColumns = true;
            this.dataGridViewEnable.AllowUserToResizeRows = false;
            this.dataGridViewEnable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewEnable.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewEnable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewEnable.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridViewEnable.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dataGridViewEnable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewEnable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEnable.ColumnHeadersVisible = false;
            this.dataGridViewEnable.EnableHeadersVisualStyles = false;
            this.dataGridViewEnable.GridColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewEnable.Location = new System.Drawing.Point(2, 11);
            this.dataGridViewEnable.Name = "dataGridViewEnable";
            this.dataGridViewEnable.ReadOnly = true;
            this.dataGridViewEnable.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridViewEnable.RowHeadersVisible = false;
            this.dataGridViewEnable.RowHeadersWidth = 51;
            this.dataGridViewEnable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEnable.Size = new System.Drawing.Size(360, 486);
            this.dataGridViewEnable.TabIndex = 9;
            this.dataGridViewEnable.TabStop = false;
            // 
            // buttonEnable
            // 
            this.buttonEnable.Location = new System.Drawing.Point(383, 263);
            this.buttonEnable.Name = "buttonEnable";
            this.buttonEnable.Size = new System.Drawing.Size(51, 23);
            this.buttonEnable.TabIndex = 7;
            this.buttonEnable.Text = "->";
            this.buttonEnable.UseVisualStyleBackColor = true;
            this.buttonEnable.Click += new System.EventHandler(this.buttonEnable_Click);
            // 
            // buttonDisable
            // 
            this.buttonDisable.Location = new System.Drawing.Point(383, 328);
            this.buttonDisable.Name = "buttonDisable";
            this.buttonDisable.Size = new System.Drawing.Size(51, 23);
            this.buttonDisable.TabIndex = 8;
            this.buttonDisable.Text = "<-";
            this.buttonDisable.UseVisualStyleBackColor = true;
            this.buttonDisable.Click += new System.EventHandler(this.buttonDisable_Click);
            // 
            // dataGridViewDisable
            // 
            this.dataGridViewDisable.AllowUserToAddRows = false;
            this.dataGridViewDisable.AllowUserToDeleteRows = false;
            this.dataGridViewDisable.AllowUserToOrderColumns = true;
            this.dataGridViewDisable.AllowUserToResizeRows = false;
            this.dataGridViewDisable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewDisable.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewDisable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewDisable.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridViewDisable.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dataGridViewDisable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewDisable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDisable.ColumnHeadersVisible = false;
            this.dataGridViewDisable.EnableHeadersVisualStyles = false;
            this.dataGridViewDisable.GridColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewDisable.Location = new System.Drawing.Point(2, 11);
            this.dataGridViewDisable.Name = "dataGridViewDisable";
            this.dataGridViewDisable.ReadOnly = true;
            this.dataGridViewDisable.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridViewDisable.RowHeadersVisible = false;
            this.dataGridViewDisable.RowHeadersWidth = 51;
            this.dataGridViewDisable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewDisable.Size = new System.Drawing.Size(360, 486);
            this.dataGridViewDisable.TabIndex = 5;
            this.dataGridViewDisable.TabStop = false;
            // 
            // labelNoDisableTip
            // 
            this.labelNoDisableTip.ForeColor = System.Drawing.Color.Red;
            this.labelNoDisableTip.Location = new System.Drawing.Point(249, 606);
            this.labelNoDisableTip.Name = "labelNoDisableTip";
            this.labelNoDisableTip.Size = new System.Drawing.Size(318, 15);
            this.labelNoDisableTip.TabIndex = 13;
            this.labelNoDisableTip.Text = "Can not disable the provider in used !";
            this.labelNoDisableTip.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.labelNoDisableTip.Visible = false;
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(93, 19);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(711, 25);
            this.textBoxSearch.TabIndex = 2;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // labelSearch
            // 
            this.labelSearch.AutoSize = true;
            this.labelSearch.Location = new System.Drawing.Point(16, 23);
            this.labelSearch.Name = "labelSearch";
            this.labelSearch.Size = new System.Drawing.Size(71, 15);
            this.labelSearch.TabIndex = 1;
            this.labelSearch.Text = "Search: ";
            // 
            // linkLabelMore
            // 
            this.linkLabelMore.AutoSize = true;
            this.linkLabelMore.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabelMore.Location = new System.Drawing.Point(12, 606);
            this.linkLabelMore.Name = "linkLabelMore";
            this.linkLabelMore.Size = new System.Drawing.Size(55, 15);
            this.linkLabelMore.TabIndex = 12;
            this.linkLabelMore.TabStop = true;
            this.linkLabelMore.Text = "More ?";
            this.linkLabelMore.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelMore_LinkClicked);
            // 
            // groupBoxEnableList
            // 
            this.groupBoxEnableList.Controls.Add(this.dataGridViewEnable);
            this.groupBoxEnableList.Location = new System.Drawing.Point(442, 49);
            this.groupBoxEnableList.Name = "groupBoxEnableList";
            this.groupBoxEnableList.Size = new System.Drawing.Size(364, 500);
            this.groupBoxEnableList.TabIndex = 0;
            this.groupBoxEnableList.TabStop = false;
            // 
            // groupBoxDisableList
            // 
            this.groupBoxDisableList.Controls.Add(this.dataGridViewDisable);
            this.groupBoxDisableList.Location = new System.Drawing.Point(12, 50);
            this.groupBoxDisableList.Name = "groupBoxDisableList";
            this.groupBoxDisableList.Size = new System.Drawing.Size(364, 500);
            this.groupBoxDisableList.TabIndex = 0;
            this.groupBoxDisableList.TabStop = false;
            // 
            // labelDisabledProviders
            // 
            this.labelDisabledProviders.AutoSize = true;
            this.labelDisabledProviders.Location = new System.Drawing.Point(119, 558);
            this.labelDisabledProviders.Name = "labelDisabledProviders";
            this.labelDisabledProviders.Size = new System.Drawing.Size(151, 15);
            this.labelDisabledProviders.TabIndex = 10;
            this.labelDisabledProviders.Text = "Disabled Providers";
            // 
            // labelEnabledProviders
            // 
            this.labelEnabledProviders.AutoSize = true;
            this.labelEnabledProviders.Location = new System.Drawing.Point(553, 558);
            this.labelEnabledProviders.Name = "labelEnabledProviders";
            this.labelEnabledProviders.Size = new System.Drawing.Size(143, 15);
            this.labelEnabledProviders.TabIndex = 11;
            this.labelEnabledProviders.Text = "Enabled Providers";
            // 
            // linkLabelReset
            // 
            this.linkLabelReset.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLabelReset.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabelReset.Location = new System.Drawing.Point(383, 102);
            this.linkLabelReset.Name = "linkLabelReset";
            this.linkLabelReset.Size = new System.Drawing.Size(51, 23);
            this.linkLabelReset.TabIndex = 6;
            this.linkLabelReset.TabStop = true;
            this.linkLabelReset.Text = "↻";
            this.linkLabelReset.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.linkLabelReset.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelResort_LinkClicked);
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 5000;
            this.toolTip.InitialDelay = 100;
            this.toolTip.ReshowDelay = 100;
            // 
            // ProvidersManage
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(819, 639);
            this.Controls.Add(this.linkLabelReset);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.labelSearch);
            this.Controls.Add(this.labelEnabledProviders);
            this.Controls.Add(this.labelDisabledProviders);
            this.Controls.Add(this.groupBoxDisableList);
            this.Controls.Add(this.groupBoxEnableList);
            this.Controls.Add(this.linkLabelMore);
            this.Controls.Add(this.labelNoDisableTip);
            this.Controls.Add(this.buttonDisable);
            this.Controls.Add(this.buttonEnable);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProvidersManage";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Providers Manage";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProvidersManage_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEnable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDisable)).EndInit();
            this.groupBoxEnableList.ResumeLayout(false);
            this.groupBoxDisableList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private DataGridView dataGridViewEnable;
        private Button buttonEnable;
        private Button buttonDisable;
        private DataGridView dataGridViewDisable;
        private Label labelNoDisableTip;
        private TextBox textBoxSearch;
        private Label labelSearch;
        private LinkLabel linkLabelMore;
        private GroupBox groupBoxEnableList;
        private GroupBox groupBoxDisableList;
        private Label labelDisabledProviders;
        private Label labelEnabledProviders;
        private LinkLabel linkLabelReset;
        private ToolTip toolTip;
    }
}