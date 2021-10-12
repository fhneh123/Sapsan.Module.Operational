namespace Sapsan.Modules.Operational.Dialogs
{
	partial class OperationalLineEditDialog
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
			this.textBoxPriority = new System.Windows.Forms.TextBox();
			this.comboBoxGroup = new System.Windows.Forms.ComboBox();
			this.comboBoxTaskType = new System.Windows.Forms.ComboBox();
			this.labelControl6 = new SUVPP.UI.Controls.Common.LabelControl();
			this.labelControl4 = new SUVPP.UI.Controls.Common.LabelControl();
			this.labelControl3 = new SUVPP.UI.Controls.Common.LabelControl();
			this.labelControl2 = new SUVPP.UI.Controls.Common.LabelControl();
			this.labelControl1 = new SUVPP.UI.Controls.Common.LabelControl();
			this.contractLookUpEditControl = new SUVPP.UI.Controls.ShifrControl.ContractLookUpEditControl();
			this.buttonSaveCancelControl1 = new SUVPP.UI.Controls.Common.ButtonSaveCancelControl();
			this.label1 = new System.Windows.Forms.Label();
			this.planLookUpControl = new Sapsan.Modules.ProjectScheduler.Controls.Shifr.PlanLookUpControl();
			this.subdivisionLookUpEditControl = new SUVPP.UI.Controls.ShifrControl.SubdivisionLookUpEditControl();
			this.labelControl5 = new SUVPP.UI.Controls.Common.LabelControl();
			this.SuspendLayout();
			// 
			// comboBoxOperationals
			// 
			this.comboBoxOperationals.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxOperationals.FormattingEnabled = true;
			this.comboBoxOperationals.Location = new System.Drawing.Point(12, 25);
			this.comboBoxOperationals.Name = "comboBoxOperationals";
			this.comboBoxOperationals.Size = new System.Drawing.Size(553, 21);
			this.comboBoxOperationals.TabIndex = 0;
			this.comboBoxOperationals.SelectedValueChanged += new System.EventHandler(this.comboBoxOperationals_SelectedValueChanged);
			// 
			// textBoxPriority
			// 
			this.textBoxPriority.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxPriority.Location = new System.Drawing.Point(12, 265);
			this.textBoxPriority.Name = "textBoxPriority";
			this.textBoxPriority.Size = new System.Drawing.Size(553, 20);
			this.textBoxPriority.TabIndex = 7;
			// 
			// comboBoxGroup
			// 
			this.comboBoxGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxGroup.FormattingEnabled = true;
			this.comboBoxGroup.Location = new System.Drawing.Point(12, 144);
			this.comboBoxGroup.Name = "comboBoxGroup";
			this.comboBoxGroup.Size = new System.Drawing.Size(553, 21);
			this.comboBoxGroup.TabIndex = 19;
			// 
			// comboBoxTaskType
			// 
			this.comboBoxTaskType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxTaskType.FormattingEnabled = true;
			this.comboBoxTaskType.Location = new System.Drawing.Point(12, 184);
			this.comboBoxTaskType.Name = "comboBoxTaskType";
			this.comboBoxTaskType.Size = new System.Drawing.Size(553, 21);
			this.comboBoxTaskType.TabIndex = 22;
			// 
			// labelControl6
			// 
			this.labelControl6.AutoSize = true;
			this.labelControl6.IsRequired = true;
			this.labelControl6.Location = new System.Drawing.Point(12, 168);
			this.labelControl6.Name = "labelControl6";
			this.labelControl6.Size = new System.Drawing.Size(71, 13);
			this.labelControl6.TabIndex = 23;
			this.labelControl6.Text = "Тип задания";
			// 
			// labelControl4
			// 
			this.labelControl4.AutoSize = true;
			this.labelControl4.IsRequired = true;
			this.labelControl4.Location = new System.Drawing.Point(12, 128);
			this.labelControl4.Name = "labelControl4";
			this.labelControl4.Size = new System.Drawing.Size(42, 13);
			this.labelControl4.TabIndex = 20;
			this.labelControl4.Text = "Группа";
			// 
			// labelControl3
			// 
			this.labelControl3.AutoSize = true;
			this.labelControl3.IsRequired = true;
			this.labelControl3.Location = new System.Drawing.Point(12, 88);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Size = new System.Drawing.Size(73, 13);
			this.labelControl3.TabIndex = 18;
			this.labelControl3.Text = "План-график";
			// 
			// labelControl2
			// 
			this.labelControl2.AutoSize = true;
			this.labelControl2.IsRequired = true;
			this.labelControl2.Location = new System.Drawing.Point(12, 9);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(88, 13);
			this.labelControl2.TabIndex = 17;
			this.labelControl2.Text = "Вид оперативки";
			// 
			// labelControl1
			// 
			this.labelControl1.AutoSize = true;
			this.labelControl1.IsRequired = true;
			this.labelControl1.Location = new System.Drawing.Point(12, 49);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(44, 13);
			this.labelControl1.TabIndex = 16;
			this.labelControl1.Text = "Проект";
			// 
			// contractLookUpEditControl
			// 
			this.contractLookUpEditControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.contractLookUpEditControl.Location = new System.Drawing.Point(12, 65);
			this.contractLookUpEditControl.Name = "contractLookUpEditControl";
			this.contractLookUpEditControl.NullText = "";
			this.contractLookUpEditControl.SelectedValue = null;
			this.contractLookUpEditControl.Size = new System.Drawing.Size(553, 20);
			this.contractLookUpEditControl.TabIndex = 15;
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
			this.buttonSaveCancelControl1.Location = new System.Drawing.Point(0, 291);
			this.buttonSaveCancelControl1.Name = "buttonSaveCancelControl1";
			this.buttonSaveCancelControl1.Size = new System.Drawing.Size(577, 42);
			this.buttonSaveCancelControl1.TabIndex = 14;
			this.buttonSaveCancelControl1.ButtonSaveClick += new System.EventHandler(this.buttonSaveCancelControl1_ButtonSaveClick);
			this.buttonSaveCancelControl1.ButtonCancelClick += new System.EventHandler(this.buttonSaveCancelControl1_ButtonCancelClick);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 249);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(61, 13);
			this.label1.TabIndex = 25;
			this.label1.Text = "Приоритет";
			// 
			// planLookUpControl
			// 
			this.planLookUpControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.planLookUpControl.Location = new System.Drawing.Point(12, 104);
			this.planLookUpControl.Name = "planLookUpControl";
			this.planLookUpControl.NullText = "";
			this.planLookUpControl.SelectedValue = null;
			this.planLookUpControl.Size = new System.Drawing.Size(553, 21);
			this.planLookUpControl.TabIndex = 26;
			// 
			// subdivisionLookUpEditControl
			// 
			this.subdivisionLookUpEditControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.subdivisionLookUpEditControl.Location = new System.Drawing.Point(12, 224);
			this.subdivisionLookUpEditControl.Name = "subdivisionLookUpEditControl";
			this.subdivisionLookUpEditControl.NullText = "";
			this.subdivisionLookUpEditControl.SelectedValue = null;
			this.subdivisionLookUpEditControl.Size = new System.Drawing.Size(553, 21);
			this.subdivisionLookUpEditControl.TabIndex = 27;
			// 
			// labelControl5
			// 
			this.labelControl5.AutoSize = true;
			this.labelControl5.IsRequired = true;
			this.labelControl5.Location = new System.Drawing.Point(12, 208);
			this.labelControl5.Name = "labelControl5";
			this.labelControl5.Size = new System.Drawing.Size(87, 13);
			this.labelControl5.TabIndex = 28;
			this.labelControl5.Text = "Подразделение";
			// 
			// OperationalLineEditDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(577, 333);
			this.Controls.Add(this.labelControl5);
			this.Controls.Add(this.subdivisionLookUpEditControl);
			this.Controls.Add(this.planLookUpControl);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.labelControl6);
			this.Controls.Add(this.comboBoxTaskType);
			this.Controls.Add(this.labelControl4);
			this.Controls.Add(this.comboBoxGroup);
			this.Controls.Add(this.labelControl3);
			this.Controls.Add(this.labelControl2);
			this.Controls.Add(this.labelControl1);
			this.Controls.Add(this.contractLookUpEditControl);
			this.Controls.Add(this.buttonSaveCancelControl1);
			this.Controls.Add(this.textBoxPriority);
			this.Controls.Add(this.comboBoxOperationals);
			this.Name = "OperationalLineEditDialog";
			this.Text = "Добавление строки оперативки";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox comboBoxOperationals;
		private System.Windows.Forms.TextBox textBoxPriority;
		private SUVPP.UI.Controls.Common.ButtonSaveCancelControl buttonSaveCancelControl1;
		private SUVPP.UI.Controls.ShifrControl.ContractLookUpEditControl contractLookUpEditControl;
		private SUVPP.UI.Controls.Common.LabelControl labelControl1;
		private SUVPP.UI.Controls.Common.LabelControl labelControl2;
		private SUVPP.UI.Controls.Common.LabelControl labelControl3;
		private SUVPP.UI.Controls.Common.LabelControl labelControl4;
		private System.Windows.Forms.ComboBox comboBoxGroup;
		private SUVPP.UI.Controls.Common.LabelControl labelControl6;
		private System.Windows.Forms.ComboBox comboBoxTaskType;
		private System.Windows.Forms.Label label1;
		private ProjectScheduler.Controls.Shifr.PlanLookUpControl planLookUpControl;
		private SUVPP.UI.Controls.ShifrControl.SubdivisionLookUpEditControl subdivisionLookUpEditControl;
		private SUVPP.UI.Controls.Common.LabelControl labelControl5;
	}
}