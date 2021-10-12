namespace Sapsan.Modules.Operational.Dialogs
{
	partial class PreProjectDialog
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
			this.luSubdivision = new SUVPP.UI.Controls.ShifrControl.SubdivisionLookUpEditControl();
			this.labelControl4 = new SUVPP.UI.Controls.Common.LabelControl();
			this.labelControl6 = new SUVPP.UI.Controls.Common.LabelControl();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.secondLevelControl = new Sapsan.Modules.ProjectScheduler.Controls.SecondLevelControl();
			this.workStructureLookUpControl = new Sapsan.Modules.ProjectScheduler.Controls.Shifr.WorkStructureLookUpControl();
			this.groupBox2.SuspendLayout();
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
			this.buttonSaveCancelControl1.Location = new System.Drawing.Point(0, 337);
			this.buttonSaveCancelControl1.Name = "buttonSaveCancelControl1";
			this.buttonSaveCancelControl1.Size = new System.Drawing.Size(535, 42);
			this.buttonSaveCancelControl1.TabIndex = 0;
			this.buttonSaveCancelControl1.ButtonSaveClick += new System.EventHandler(this.ButtonSaveCancelControl1_ButtonSaveClick);
			this.buttonSaveCancelControl1.ButtonCancelClick += new System.EventHandler(this.ButtonSaveCancelControl1_ButtonCancelClick);
			// 
			// luSubdivision
			// 
			this.luSubdivision.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.luSubdivision.Location = new System.Drawing.Point(9, 23);
			this.luSubdivision.Name = "luSubdivision";
			this.luSubdivision.NullText = "";
			this.luSubdivision.SelectedValue = null;
			this.luSubdivision.Size = new System.Drawing.Size(514, 21);
			this.luSubdivision.TabIndex = 20;
			// 
			// labelControl4
			// 
			this.labelControl4.AutoSize = true;
			this.labelControl4.IsRequired = true;
			this.labelControl4.Location = new System.Drawing.Point(6, 7);
			this.labelControl4.Name = "labelControl4";
			this.labelControl4.Size = new System.Drawing.Size(84, 13);
			this.labelControl4.TabIndex = 22;
			this.labelControl4.Text = "Ведущий отдел";
			// 
			// labelControl6
			// 
			this.labelControl6.AutoSize = true;
			this.labelControl6.IsRequired = true;
			this.labelControl6.Location = new System.Drawing.Point(6, 23);
			this.labelControl6.Name = "labelControl6";
			this.labelControl6.Size = new System.Drawing.Size(211, 13);
			this.labelControl6.TabIndex = 25;
			this.labelControl6.Text = "Группа работ из сетевого план-графика";
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.secondLevelControl);
			this.groupBox2.Controls.Add(this.labelControl6);
			this.groupBox2.Controls.Add(this.workStructureLookUpControl);
			this.groupBox2.Location = new System.Drawing.Point(9, 50);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(514, 295);
			this.groupBox2.TabIndex = 31;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "График передачи заданий";
			// 
			// secondLevelControl
			// 
			this.secondLevelControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.secondLevelControl.Location = new System.Drawing.Point(9, 67);
			this.secondLevelControl.Name = "secondLevelControl";
			this.secondLevelControl.Size = new System.Drawing.Size(499, 222);
			this.secondLevelControl.TabIndex = 26;
			// 
			// workStructureLookUpControl
			// 
			this.workStructureLookUpControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.workStructureLookUpControl.Location = new System.Drawing.Point(9, 39);
			this.workStructureLookUpControl.Name = "workStructureLookUpControl";
			this.workStructureLookUpControl.NullText = "";
			this.workStructureLookUpControl.SelectedValue = null;
			this.workStructureLookUpControl.Size = new System.Drawing.Size(499, 21);
			this.workStructureLookUpControl.TabIndex = 24;
			// 
			// PreProjectDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(535, 379);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.labelControl4);
			this.Controls.Add(this.luSubdivision);
			this.Controls.Add(this.buttonSaveCancelControl1);
			this.MinimumSize = new System.Drawing.Size(500, 415);
			this.Name = "PreProjectDialog";
			this.Text = "Предпроект";
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private SUVPP.UI.Controls.Common.ButtonSaveCancelControl buttonSaveCancelControl1;
		private SUVPP.UI.Controls.ShifrControl.SubdivisionLookUpEditControl luSubdivision;
		private SUVPP.UI.Controls.Common.LabelControl labelControl4;
		private ProjectScheduler.Controls.Shifr.WorkStructureLookUpControl workStructureLookUpControl;
		private SUVPP.UI.Controls.Common.LabelControl labelControl6;
		private System.Windows.Forms.GroupBox groupBox2;
        private ProjectScheduler.Controls.SecondLevelControl secondLevelControl;
    }
}