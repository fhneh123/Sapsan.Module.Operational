namespace Sapsan.Modules.Operational.Dialogs
{
	partial class OperationalEntityStatusEditorDialog
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
			this.statusEditorControl = new Sapsan.Modules.Operational.Controls.StatusEditorControl();
			this.SuspendLayout();
			// 
			// statusEditorControl
			// 
			this.statusEditorControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.statusEditorControl.Location = new System.Drawing.Point(0, 0);
			this.statusEditorControl.Name = "statusEditorControl";
			this.statusEditorControl.Size = new System.Drawing.Size(557, 450);
			this.statusEditorControl.TabIndex = 0;
			// 
			// OperationalEntityStatusEditorDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(557, 450);
			this.Controls.Add(this.statusEditorControl);
			this.Name = "OperationalEntityStatusEditorDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Редактор статусов";
			this.ResumeLayout(false);

		}

		#endregion

		private Controls.StatusEditorControl statusEditorControl;
	}
}