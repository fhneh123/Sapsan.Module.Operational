namespace Sapsan.Modules.Operational.Controls
{
	partial class OperationalMainControl
	{
		/// <summary> 
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором компонентов

		/// <summary> 
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPageOperationalLines = new System.Windows.Forms.TabPage();
			this.tabPageSpfoa = new System.Windows.Forms.TabPage();
			this.tabControl.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabPageOperationalLines);
			this.tabControl.Controls.Add(this.tabPageSpfoa);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(683, 533);
			this.tabControl.TabIndex = 0;
			this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
			// 
			// tabPageOperationalLines
			// 
			this.tabPageOperationalLines.Location = new System.Drawing.Point(4, 22);
			this.tabPageOperationalLines.Name = "tabPageOperationalLines";
			this.tabPageOperationalLines.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageOperationalLines.Size = new System.Drawing.Size(675, 507);
			this.tabPageOperationalLines.TabIndex = 0;
			this.tabPageOperationalLines.Text = "Оперативки";
			this.tabPageOperationalLines.UseVisualStyleBackColor = true;
			// 
			// tabPageSpfoa
			// 
			this.tabPageSpfoa.Location = new System.Drawing.Point(4, 22);
			this.tabPageSpfoa.Name = "tabPageSpfoa";
			this.tabPageSpfoa.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageSpfoa.Size = new System.Drawing.Size(675, 507);
			this.tabPageSpfoa.TabIndex = 1;
			this.tabPageSpfoa.Text = "СПФОА";
			this.tabPageSpfoa.UseVisualStyleBackColor = true;
			// 
			// OperationalMainControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabControl);
			this.Name = "OperationalMainControl";
			this.Size = new System.Drawing.Size(683, 533);
			this.tabControl.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPageOperationalLines;
		private System.Windows.Forms.TabPage tabPageSpfoa;
	}
}
