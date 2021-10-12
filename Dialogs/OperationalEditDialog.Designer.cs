namespace Sapsan.Modules.Operational.Dialogs
{
	partial class OperationalEditDialog
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
			this.labelControl2 = new SUVPP.UI.Controls.Common.LabelControl();
			this.orderNumUpd = new System.Windows.Forms.NumericUpDown();
			this.labelControl1 = new SUVPP.UI.Controls.Common.LabelControl();
			this.tbName = new System.Windows.Forms.TextBox();
			this.buttonSaveCancelControl1 = new SUVPP.UI.Controls.Common.ButtonSaveCancelControl();
			this.workStructureSprLookUpControl = new Sapsan.Modules.ProjectScheduler.Controls.Shifr.WorkStructureSprLookUpControl();
			this.labelControl3 = new SUVPP.UI.Controls.Common.LabelControl();
			((System.ComponentModel.ISupportInitialize)(this.orderNumUpd)).BeginInit();
			this.SuspendLayout();
			// 
			// labelControl2
			// 
			this.labelControl2.AutoSize = true;
			this.labelControl2.IsRequired = true;
			this.labelControl2.Location = new System.Drawing.Point(12, 88);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(106, 13);
			this.labelControl2.TabIndex = 8;
			this.labelControl2.Text = "Порядковый номер";
			// 
			// orderNumUpd
			// 
			this.orderNumUpd.Location = new System.Drawing.Point(12, 104);
			this.orderNumUpd.Name = "orderNumUpd";
			this.orderNumUpd.Size = new System.Drawing.Size(120, 20);
			this.orderNumUpd.TabIndex = 7;
			// 
			// labelControl1
			// 
			this.labelControl1.AutoSize = true;
			this.labelControl1.IsRequired = true;
			this.labelControl1.Location = new System.Drawing.Point(12, 9);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(57, 13);
			this.labelControl1.TabIndex = 6;
			this.labelControl1.Text = "Название";
			// 
			// tbName
			// 
			this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbName.Location = new System.Drawing.Point(12, 25);
			this.tbName.Name = "tbName";
			this.tbName.Size = new System.Drawing.Size(421, 20);
			this.tbName.TabIndex = 5;
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
			this.buttonSaveCancelControl1.Location = new System.Drawing.Point(0, 137);
			this.buttonSaveCancelControl1.Name = "buttonSaveCancelControl1";
			this.buttonSaveCancelControl1.Size = new System.Drawing.Size(445, 42);
			this.buttonSaveCancelControl1.TabIndex = 9;
			this.buttonSaveCancelControl1.ButtonSaveClick += new System.EventHandler(this.buttonSaveCancelControl1_ButtonSaveClick);
			this.buttonSaveCancelControl1.ButtonCancelClick += new System.EventHandler(this.buttonSaveCancelControl1_ButtonCancelClick);
			// 
			// workStructureSprLookUpControl
			// 
			this.workStructureSprLookUpControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.workStructureSprLookUpControl.Location = new System.Drawing.Point(12, 64);
			this.workStructureSprLookUpControl.Name = "workStructureSprLookUpControl";
			this.workStructureSprLookUpControl.NullText = "";
			this.workStructureSprLookUpControl.SelectedValue = null;
			this.workStructureSprLookUpControl.Size = new System.Drawing.Size(421, 21);
			this.workStructureSprLookUpControl.TabIndex = 10;
			// 
			// labelControl3
			// 
			this.labelControl3.AutoSize = true;
			this.labelControl3.IsRequired = true;
			this.labelControl3.Location = new System.Drawing.Point(12, 48);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Size = new System.Drawing.Size(118, 13);
			this.labelControl3.TabIndex = 11;
			this.labelControl3.Text = "Соответствует группе";
			// 
			// OperationalEditDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(445, 179);
			this.Controls.Add(this.labelControl3);
			this.Controls.Add(this.workStructureSprLookUpControl);
			this.Controls.Add(this.buttonSaveCancelControl1);
			this.Controls.Add(this.labelControl2);
			this.Controls.Add(this.orderNumUpd);
			this.Controls.Add(this.labelControl1);
			this.Controls.Add(this.tbName);
			this.Name = "OperationalEditDialog";
			this.Text = "Добавление оперативки";
			((System.ComponentModel.ISupportInitialize)(this.orderNumUpd)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private SUVPP.UI.Controls.Common.LabelControl labelControl2;
		private System.Windows.Forms.NumericUpDown orderNumUpd;
		private SUVPP.UI.Controls.Common.LabelControl labelControl1;
		private System.Windows.Forms.TextBox tbName;
		private SUVPP.UI.Controls.Common.ButtonSaveCancelControl buttonSaveCancelControl1;
		private ProjectScheduler.Controls.Shifr.WorkStructureSprLookUpControl workStructureSprLookUpControl;
		private SUVPP.UI.Controls.Common.LabelControl labelControl3;
	}
}