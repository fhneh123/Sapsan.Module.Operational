namespace Sapsan.Modules.Operational.Dialogs
{
	partial class StatusEditDialog
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
			this.buttonSaveCancelControl1 = new SUVPP.UI.Controls.Common.ButtonSaveCancelControl();
			this.labelControl2 = new SUVPP.UI.Controls.Common.LabelControl();
			this.orderNumUpd = new System.Windows.Forms.NumericUpDown();
			this.labelControl1 = new SUVPP.UI.Controls.Common.LabelControl();
			this.tbName = new System.Windows.Forms.TextBox();
			this.btnSelectColor = new System.Windows.Forms.Button();
			this.panelColor = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.orderNumUpd)).BeginInit();
			this.SuspendLayout();
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
			this.buttonSaveCancelControl1.Location = new System.Drawing.Point(0, 132);
			this.buttonSaveCancelControl1.Name = "buttonSaveCancelControl1";
			this.buttonSaveCancelControl1.Size = new System.Drawing.Size(439, 42);
			this.buttonSaveCancelControl1.TabIndex = 16;
			this.buttonSaveCancelControl1.ButtonSaveClick += new System.EventHandler(this.buttonSaveCancelControl1_ButtonSaveClick);
			this.buttonSaveCancelControl1.ButtonCancelClick += new System.EventHandler(this.buttonSaveCancelControl1_ButtonCancelClick);
			// 
			// labelControl2
			// 
			this.labelControl2.AutoSize = true;
			this.labelControl2.IsRequired = true;
			this.labelControl2.Location = new System.Drawing.Point(12, 89);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(106, 13);
			this.labelControl2.TabIndex = 15;
			this.labelControl2.Text = "Порядковый номер";
			// 
			// orderNumUpd
			// 
			this.orderNumUpd.Location = new System.Drawing.Point(12, 105);
			this.orderNumUpd.Name = "orderNumUpd";
			this.orderNumUpd.Size = new System.Drawing.Size(120, 20);
			this.orderNumUpd.TabIndex = 14;
			// 
			// labelControl1
			// 
			this.labelControl1.AutoSize = true;
			this.labelControl1.IsRequired = true;
			this.labelControl1.Location = new System.Drawing.Point(12, 5);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(57, 13);
			this.labelControl1.TabIndex = 13;
			this.labelControl1.Text = "Название";
			// 
			// tbName
			// 
			this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbName.Location = new System.Drawing.Point(12, 21);
			this.tbName.Name = "tbName";
			this.tbName.Size = new System.Drawing.Size(421, 20);
			this.tbName.TabIndex = 12;
			// 
			// btnSelectColor
			// 
			this.btnSelectColor.Location = new System.Drawing.Point(56, 60);
			this.btnSelectColor.Name = "btnSelectColor";
			this.btnSelectColor.Size = new System.Drawing.Size(121, 23);
			this.btnSelectColor.TabIndex = 19;
			this.btnSelectColor.Text = "Выбрать цвет";
			this.btnSelectColor.UseVisualStyleBackColor = true;
			this.btnSelectColor.Click += new System.EventHandler(this.btnSelectColor_Click);
			// 
			// panelColor
			// 
			this.panelColor.BackColor = System.Drawing.SystemColors.Window;
			this.panelColor.Location = new System.Drawing.Point(12, 60);
			this.panelColor.Name = "panelColor";
			this.panelColor.Size = new System.Drawing.Size(38, 23);
			this.panelColor.TabIndex = 18;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 44);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 13);
			this.label2.TabIndex = 17;
			this.label2.Text = "Цвет:";
			// 
			// StatusEditDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(439, 174);
			this.Controls.Add(this.btnSelectColor);
			this.Controls.Add(this.panelColor);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.buttonSaveCancelControl1);
			this.Controls.Add(this.labelControl2);
			this.Controls.Add(this.orderNumUpd);
			this.Controls.Add(this.labelControl1);
			this.Controls.Add(this.tbName);
			this.Name = "StatusEditDialog";
			this.Text = "Добавление статуса";
			((System.ComponentModel.ISupportInitialize)(this.orderNumUpd)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private SUVPP.UI.Controls.Common.ButtonSaveCancelControl buttonSaveCancelControl1;
		private SUVPP.UI.Controls.Common.LabelControl labelControl2;
		private System.Windows.Forms.NumericUpDown orderNumUpd;
		private SUVPP.UI.Controls.Common.LabelControl labelControl1;
		private System.Windows.Forms.TextBox tbName;
		private System.Windows.Forms.Button btnSelectColor;
		private System.Windows.Forms.Panel panelColor;
		private System.Windows.Forms.Label label2;
	}
}