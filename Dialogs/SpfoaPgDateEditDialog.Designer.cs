namespace Sapsan.Modules.Operational.Dialogs
{
	partial class SpfoaPgDateEditDialog
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
			this.comboBoxOperationals = new System.Windows.Forms.ComboBox();
			this.labelControl2 = new SUVPP.UI.Controls.Common.LabelControl();
			this.labelControl1 = new SUVPP.UI.Controls.Common.LabelControl();
			this.comboBoxWorks = new System.Windows.Forms.ComboBox();
			this.labelControl3 = new SUVPP.UI.Controls.Common.LabelControl();
			this.label1 = new System.Windows.Forms.Label();
			this.comboBoxDateType = new System.Windows.Forms.ComboBox();
			this.labelControl4 = new SUVPP.UI.Controls.Common.LabelControl();
			this.comboBoxStatusII = new System.Windows.Forms.ComboBox();
			this.orderNumUpd = new System.Windows.Forms.NumericUpDown();
			this.labelControl5 = new SUVPP.UI.Controls.Common.LabelControl();
			this.buttonSaveCancelControl1 = new SUVPP.UI.Controls.Common.ButtonSaveCancelControl();
			this.comboBoxStatusOperational = new DevExpress.XtraEditors.SearchLookUpEdit();
			this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnName = new DevExpress.XtraGrid.Columns.GridColumn();
			((System.ComponentModel.ISupportInitialize)(this.orderNumUpd)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxStatusOperational.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
			this.SuspendLayout();
			// 
			// comboBoxOperationals
			// 
			this.comboBoxOperationals.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxOperationals.FormattingEnabled = true;
			this.comboBoxOperationals.Location = new System.Drawing.Point(12, 23);
			this.comboBoxOperationals.Name = "comboBoxOperationals";
			this.comboBoxOperationals.Size = new System.Drawing.Size(577, 21);
			this.comboBoxOperationals.TabIndex = 0;
			this.comboBoxOperationals.SelectedValueChanged += new System.EventHandler(this.comboBoxOperationals_SelectedValueChanged);
			// 
			// labelControl2
			// 
			this.labelControl2.AutoSize = true;
			this.labelControl2.IsRequired = true;
			this.labelControl2.Location = new System.Drawing.Point(12, 7);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(68, 13);
			this.labelControl2.TabIndex = 18;
			this.labelControl2.Text = "Оперативка";
			// 
			// labelControl1
			// 
			this.labelControl1.AutoSize = true;
			this.labelControl1.IsRequired = true;
			this.labelControl1.Location = new System.Drawing.Point(12, 47);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(43, 13);
			this.labelControl1.TabIndex = 19;
			this.labelControl1.Text = "Работа";
			// 
			// comboBoxWorks
			// 
			this.comboBoxWorks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxWorks.FormattingEnabled = true;
			this.comboBoxWorks.Location = new System.Drawing.Point(12, 63);
			this.comboBoxWorks.Name = "comboBoxWorks";
			this.comboBoxWorks.Size = new System.Drawing.Size(577, 21);
			this.comboBoxWorks.TabIndex = 20;
			this.comboBoxWorks.SelectedValueChanged += new System.EventHandler(this.comboBoxWorks_SelectedValueChanged);
			// 
			// labelControl3
			// 
			this.labelControl3.AutoSize = true;
			this.labelControl3.IsRequired = true;
			this.labelControl3.Location = new System.Drawing.Point(12, 127);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Size = new System.Drawing.Size(54, 13);
			this.labelControl3.TabIndex = 21;
			this.labelControl3.Text = "Тип даты";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 87);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 13);
			this.label1.TabIndex = 22;
			this.label1.Text = "Статус в оперативке";
			// 
			// comboBoxDateType
			// 
			this.comboBoxDateType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxDateType.FormattingEnabled = true;
			this.comboBoxDateType.Location = new System.Drawing.Point(12, 143);
			this.comboBoxDateType.Name = "comboBoxDateType";
			this.comboBoxDateType.Size = new System.Drawing.Size(577, 21);
			this.comboBoxDateType.TabIndex = 24;
			// 
			// labelControl4
			// 
			this.labelControl4.AutoSize = true;
			this.labelControl4.IsRequired = true;
			this.labelControl4.Location = new System.Drawing.Point(12, 167);
			this.labelControl4.Name = "labelControl4";
			this.labelControl4.Size = new System.Drawing.Size(113, 13);
			this.labelControl4.TabIndex = 25;
			this.labelControl4.Text = "Статус в СПФОА ИИ";
			// 
			// comboBoxStatusII
			// 
			this.comboBoxStatusII.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxStatusII.FormattingEnabled = true;
			this.comboBoxStatusII.Location = new System.Drawing.Point(12, 183);
			this.comboBoxStatusII.Name = "comboBoxStatusII";
			this.comboBoxStatusII.Size = new System.Drawing.Size(577, 21);
			this.comboBoxStatusII.TabIndex = 26;
			// 
			// orderNumUpd
			// 
			this.orderNumUpd.Location = new System.Drawing.Point(12, 223);
			this.orderNumUpd.Name = "orderNumUpd";
			this.orderNumUpd.Size = new System.Drawing.Size(120, 20);
			this.orderNumUpd.TabIndex = 27;
			// 
			// labelControl5
			// 
			this.labelControl5.AutoSize = true;
			this.labelControl5.IsRequired = true;
			this.labelControl5.Location = new System.Drawing.Point(12, 207);
			this.labelControl5.Name = "labelControl5";
			this.labelControl5.Size = new System.Drawing.Size(106, 13);
			this.labelControl5.TabIndex = 28;
			this.labelControl5.Text = "Порядковый номер";
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
			this.buttonSaveCancelControl1.Location = new System.Drawing.Point(0, 257);
			this.buttonSaveCancelControl1.Name = "buttonSaveCancelControl1";
			this.buttonSaveCancelControl1.Size = new System.Drawing.Size(601, 42);
			this.buttonSaveCancelControl1.TabIndex = 29;
			this.buttonSaveCancelControl1.ButtonSaveClick += new System.EventHandler(this.buttonSaveCancelControl1_ButtonSaveClick);
			this.buttonSaveCancelControl1.ButtonCancelClick += new System.EventHandler(this.buttonSaveCancelControl1_ButtonCancelClick);
			// 
			// comboBoxStatusOperational
			// 
			this.comboBoxStatusOperational.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxStatusOperational.EditValue = "";
			this.comboBoxStatusOperational.Location = new System.Drawing.Point(12, 104);
			this.comboBoxStatusOperational.Name = "comboBoxStatusOperational";
			this.comboBoxStatusOperational.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxStatusOperational.Properties.NullText = "";
			this.comboBoxStatusOperational.Properties.PopupView = this.searchLookUpEdit1View;
			this.comboBoxStatusOperational.Size = new System.Drawing.Size(577, 20);
			this.comboBoxStatusOperational.TabIndex = 30;
			// 
			// searchLookUpEdit1View
			// 
			this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnName});
			this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
			this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
			// 
			// gridColumnName
			// 
			this.gridColumnName.Caption = "Название";
			this.gridColumnName.FieldName = "Name";
			this.gridColumnName.Name = "gridColumnName";
			this.gridColumnName.Visible = true;
			this.gridColumnName.VisibleIndex = 0;
			// 
			// SpfoaPgDateEditDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(601, 299);
			this.Controls.Add(this.comboBoxStatusOperational);
			this.Controls.Add(this.buttonSaveCancelControl1);
			this.Controls.Add(this.labelControl5);
			this.Controls.Add(this.orderNumUpd);
			this.Controls.Add(this.comboBoxStatusII);
			this.Controls.Add(this.labelControl4);
			this.Controls.Add(this.comboBoxDateType);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.labelControl3);
			this.Controls.Add(this.comboBoxWorks);
			this.Controls.Add(this.labelControl1);
			this.Controls.Add(this.labelControl2);
			this.Controls.Add(this.comboBoxOperationals);
			this.Name = "SpfoaPgDateEditDialog";
			this.Text = "Добавление связи с план-графиком";
			((System.ComponentModel.ISupportInitialize)(this.orderNumUpd)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxStatusOperational.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox comboBoxOperationals;
		private SUVPP.UI.Controls.Common.LabelControl labelControl2;
		private SUVPP.UI.Controls.Common.LabelControl labelControl1;
		private System.Windows.Forms.ComboBox comboBoxWorks;
		private SUVPP.UI.Controls.Common.LabelControl labelControl3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboBoxDateType;
		private SUVPP.UI.Controls.Common.LabelControl labelControl4;
		private System.Windows.Forms.ComboBox comboBoxStatusII;
		private System.Windows.Forms.NumericUpDown orderNumUpd;
		private SUVPP.UI.Controls.Common.LabelControl labelControl5;
		private SUVPP.UI.Controls.Common.ButtonSaveCancelControl buttonSaveCancelControl1;
		private DevExpress.XtraEditors.SearchLookUpEdit comboBoxStatusOperational;
		private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnName;
	}
}