namespace Sapsan.Modules.Operational.Dialogs
{
	partial class OperationalWorkEditDialog
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
			this.labelControl1 = new SUVPP.UI.Controls.Common.LabelControl();
			this.buttonSaveCancelControl1 = new SUVPP.UI.Controls.Common.ButtonSaveCancelControl();
			this.labelControl2 = new SUVPP.UI.Controls.Common.LabelControl();
			this.orderNumUpd = new System.Windows.Forms.NumericUpDown();
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.labelControl3 = new SUVPP.UI.Controls.Common.LabelControl();
			this.comboBoxOperational = new System.Windows.Forms.ComboBox();
			this.gridControl = new DevExpress.XtraGrid.GridControl();
			this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.labelControl4 = new SUVPP.UI.Controls.Common.LabelControl();
			this.label1 = new System.Windows.Forms.Label();
			this.comboBoxFromOperationalWork = new DevExpress.XtraEditors.SearchLookUpEdit();
			((System.ComponentModel.ISupportInitialize)(this.orderNumUpd)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxFromOperationalWork.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// labelControl1
			// 
			this.labelControl1.AutoSize = true;
			this.labelControl1.IsRequired = true;
			this.labelControl1.Location = new System.Drawing.Point(12, 9);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(88, 13);
			this.labelControl1.TabIndex = 7;
			this.labelControl1.Text = "Вид оперативки";
			// 
			// buttonSaveCancelControl1
			// 
			this.buttonSaveCancelControl1.ButtonCancelImage = null;
			this.buttonSaveCancelControl1.ButtonCancelText = "Отмена";
			this.buttonSaveCancelControl1.ButtonSaveEnabled = true;
			this.buttonSaveCancelControl1.ButtonSavelImage = null;
			this.buttonSaveCancelControl1.ButtonSaveText = "Сохранить";
			this.buttonSaveCancelControl1.ButtonSaveVisible = true;
			this.buttonSaveCancelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.buttonSaveCancelControl1.Location = new System.Drawing.Point(0, 296);
			this.buttonSaveCancelControl1.Name = "buttonSaveCancelControl1";
			this.buttonSaveCancelControl1.Size = new System.Drawing.Size(648, 42);
			this.buttonSaveCancelControl1.TabIndex = 10;
			this.buttonSaveCancelControl1.ButtonSaveClick += new System.EventHandler(this.buttonSaveCancelControl1_ButtonSaveClick);
			this.buttonSaveCancelControl1.ButtonCancelClick += new System.EventHandler(this.buttonSaveCancelControl1_ButtonCancelClick);
			// 
			// labelControl2
			// 
			this.labelControl2.AutoSize = true;
			this.labelControl2.IsRequired = true;
			this.labelControl2.Location = new System.Drawing.Point(12, 128);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(106, 13);
			this.labelControl2.TabIndex = 12;
			this.labelControl2.Text = "Порядковый номер";
			// 
			// orderNumUpd
			// 
			this.orderNumUpd.Location = new System.Drawing.Point(12, 144);
			this.orderNumUpd.Name = "orderNumUpd";
			this.orderNumUpd.Size = new System.Drawing.Size(120, 20);
			this.orderNumUpd.TabIndex = 11;
			// 
			// textBoxName
			// 
			this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxName.Location = new System.Drawing.Point(12, 64);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(624, 20);
			this.textBoxName.TabIndex = 14;
			// 
			// labelControl3
			// 
			this.labelControl3.AutoSize = true;
			this.labelControl3.IsRequired = true;
			this.labelControl3.Location = new System.Drawing.Point(12, 48);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Size = new System.Drawing.Size(97, 13);
			this.labelControl3.TabIndex = 13;
			this.labelControl3.Text = "Название работы";
			// 
			// comboBoxOperational
			// 
			this.comboBoxOperational.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxOperational.FormattingEnabled = true;
			this.comboBoxOperational.Location = new System.Drawing.Point(12, 25);
			this.comboBoxOperational.Name = "comboBoxOperational";
			this.comboBoxOperational.Size = new System.Drawing.Size(624, 21);
			this.comboBoxOperational.TabIndex = 19;
			// 
			// gridControl
			// 
			this.gridControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridControl.Location = new System.Drawing.Point(12, 183);
			this.gridControl.MainView = this.gridView;
			this.gridControl.Name = "gridControl";
			this.gridControl.Size = new System.Drawing.Size(624, 109);
			this.gridControl.TabIndex = 20;
			this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
			this.gridControl.DataSourceChanged += new System.EventHandler(this.gridControl_DataSourceChanged);
			// 
			// gridView
			// 
			this.gridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.gridView.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gridView.ColumnPanelRowHeight = 60;
			this.gridView.GridControl = this.gridControl;
			this.gridView.Name = "gridView";
			this.gridView.OptionsView.ColumnAutoWidth = false;
			this.gridView.OptionsView.ShowGroupPanel = false;
			// 
			// labelControl4
			// 
			this.labelControl4.AutoSize = true;
			this.labelControl4.IsRequired = true;
			this.labelControl4.Location = new System.Drawing.Point(12, 167);
			this.labelControl4.Name = "labelControl4";
			this.labelControl4.Size = new System.Drawing.Size(50, 13);
			this.labelControl4.TabIndex = 21;
			this.labelControl4.Text = "Колонки";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 87);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(184, 13);
			this.label1.TabIndex = 23;
			this.label1.Text = "Соответствует работе оперативки:";
			// 
			// comboBoxFromOperationalWork
			// 
			this.comboBoxFromOperationalWork.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxFromOperationalWork.Location = new System.Drawing.Point(12, 103);
			this.comboBoxFromOperationalWork.Name = "comboBoxFromOperationalWork";
			this.comboBoxFromOperationalWork.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxFromOperationalWork.Properties.DisplayMember = "Name";
			this.comboBoxFromOperationalWork.Properties.NullText = "";
			this.comboBoxFromOperationalWork.Properties.ValueMember = "Id";
			this.comboBoxFromOperationalWork.Size = new System.Drawing.Size(624, 20);
			this.comboBoxFromOperationalWork.TabIndex = 26;
			// 
			// OperationalWorkEditDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(648, 338);
			this.Controls.Add(this.comboBoxFromOperationalWork);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.labelControl4);
			this.Controls.Add(this.gridControl);
			this.Controls.Add(this.comboBoxOperational);
			this.Controls.Add(this.textBoxName);
			this.Controls.Add(this.labelControl3);
			this.Controls.Add(this.labelControl2);
			this.Controls.Add(this.orderNumUpd);
			this.Controls.Add(this.buttonSaveCancelControl1);
			this.Controls.Add(this.labelControl1);
			this.Name = "OperationalWorkEditDialog";
			this.Text = "Добавление работы";
			((System.ComponentModel.ISupportInitialize)(this.orderNumUpd)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxFromOperationalWork.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private SUVPP.UI.Controls.Common.LabelControl labelControl1;
		private SUVPP.UI.Controls.Common.ButtonSaveCancelControl buttonSaveCancelControl1;
		private SUVPP.UI.Controls.Common.LabelControl labelControl2;
		private System.Windows.Forms.NumericUpDown orderNumUpd;
		private System.Windows.Forms.TextBox textBoxName;
		private SUVPP.UI.Controls.Common.LabelControl labelControl3;
		private System.Windows.Forms.ComboBox comboBoxOperational;
		private DevExpress.XtraGrid.GridControl gridControl;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView;
		private SUVPP.UI.Controls.Common.LabelControl labelControl4;
		private System.Windows.Forms.Label label1;
		private DevExpress.XtraEditors.SearchLookUpEdit comboBoxFromOperationalWork;
	}
}