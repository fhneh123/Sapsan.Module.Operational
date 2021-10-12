namespace Sapsan.Modules.Operational.Dialogs
{
	partial class OperationalTypeTaskDialog
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
			this.tbName = new System.Windows.Forms.TextBox();
			this.labelControl2 = new SUVPP.UI.Controls.Common.LabelControl();
			this.orderNumUpd = new System.Windows.Forms.NumericUpDown();
			this.buttonSaveCancelControl = new SUVPP.UI.Controls.Common.ButtonSaveCancelControl();
			((System.ComponentModel.ISupportInitialize)(this.orderNumUpd)).BeginInit();
			this.SuspendLayout();
			// 
			// labelControl1
			// 
			this.labelControl1.AutoSize = true;
			this.labelControl1.IsRequired = true;
			this.labelControl1.Location = new System.Drawing.Point(12, 9);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(57, 13);
			this.labelControl1.TabIndex = 8;
			this.labelControl1.Text = "Название";
			// 
			// tbName
			// 
			this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbName.Location = new System.Drawing.Point(12, 25);
			this.tbName.Name = "tbName";
			this.tbName.Size = new System.Drawing.Size(380, 20);
			this.tbName.TabIndex = 7;
			// 
			// labelControl2
			// 
			this.labelControl2.AutoSize = true;
			this.labelControl2.IsRequired = true;
			this.labelControl2.Location = new System.Drawing.Point(12, 48);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(106, 13);
			this.labelControl2.TabIndex = 10;
			this.labelControl2.Text = "Порядковый номер";
			// 
			// orderNumUpd
			// 
			this.orderNumUpd.Location = new System.Drawing.Point(12, 64);
			this.orderNumUpd.Name = "orderNumUpd";
			this.orderNumUpd.Size = new System.Drawing.Size(120, 20);
			this.orderNumUpd.TabIndex = 9;
			// 
			// buttonSaveCancelControl
			// 
			this.buttonSaveCancelControl.ButtonCancelImage = null;
			this.buttonSaveCancelControl.ButtonCancelText = "Отмена";
			this.buttonSaveCancelControl.ButtonSaveEnabled = true;
			this.buttonSaveCancelControl.ButtonSavelImage = null;
			this.buttonSaveCancelControl.ButtonSaveText = "Сохранить";
			this.buttonSaveCancelControl.ButtonSaveVisible = true;
			this.buttonSaveCancelControl.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.buttonSaveCancelControl.Location = new System.Drawing.Point(0, 109);
			this.buttonSaveCancelControl.Name = "buttonSaveCancelControl";
			this.buttonSaveCancelControl.Size = new System.Drawing.Size(404, 42);
			this.buttonSaveCancelControl.TabIndex = 11;
			this.buttonSaveCancelControl.ButtonSaveClick += new System.EventHandler(this.buttonSaveCancelControl_ButtonSaveClick);
			this.buttonSaveCancelControl.ButtonCancelClick += new System.EventHandler(this.buttonSaveCancelControl_ButtonCancelClick);
			// 
			// OperationalTypeTaskDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(404, 151);
			this.Controls.Add(this.buttonSaveCancelControl);
			this.Controls.Add(this.labelControl2);
			this.Controls.Add(this.orderNumUpd);
			this.Controls.Add(this.labelControl1);
			this.Controls.Add(this.tbName);
			this.Name = "OperationalTypeTaskDialog";
			this.Text = "Добавление типа задания";
			((System.ComponentModel.ISupportInitialize)(this.orderNumUpd)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private SUVPP.UI.Controls.Common.LabelControl labelControl1;
		private System.Windows.Forms.TextBox tbName;
		private SUVPP.UI.Controls.Common.LabelControl labelControl2;
		private System.Windows.Forms.NumericUpDown orderNumUpd;
		private SUVPP.UI.Controls.Common.ButtonSaveCancelControl buttonSaveCancelControl;
	}
}