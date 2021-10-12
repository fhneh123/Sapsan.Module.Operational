namespace Sapsan.Modules.Operational.Controls
{
	partial class StatusEditorControl
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
			this.components = new System.ComponentModel.Container();
			this.dataGridControlAddBtn = new SUVPP.UI.Controls.DataGridControlAddBtn();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.добавитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.редактироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.историяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// dataGridControlAddBtn
			// 
			this.dataGridControlAddBtn.ColumnPanelRowHeight = -1;
			this.dataGridControlAddBtn.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridControlAddBtn.HeaderPanelWordWrap = DevExpress.Utils.WordWrap.Default;
			this.dataGridControlAddBtn.IsBtnAddVisible = true;
			this.dataGridControlAddBtn.IsBtnExportVisible = false;
			this.dataGridControlAddBtn.Location = new System.Drawing.Point(0, 0);
			this.dataGridControlAddBtn.Margin = new System.Windows.Forms.Padding(2);
			this.dataGridControlAddBtn.Name = "dataGridControlAddBtn";
			this.dataGridControlAddBtn.OptionsBehavior_Editable = true;
			this.dataGridControlAddBtn.OptionsClipboard_AllowExcelFormat = DevExpress.Utils.DefaultBoolean.Default;
			this.dataGridControlAddBtn.OptionsClipboard_AllowHtmlFormat = DevExpress.Utils.DefaultBoolean.Default;
			this.dataGridControlAddBtn.OptionsClipboard_AllowRtfFormat = DevExpress.Utils.DefaultBoolean.Default;
			this.dataGridControlAddBtn.OptionsClipboard_ClipboardMode = DevExpress.Export.ClipboardMode.Default;
			this.dataGridControlAddBtn.OptionsClipboard_CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.Default;
			this.dataGridControlAddBtn.OptionsFind_Always_Visible = false;
			this.dataGridControlAddBtn.OptionsMenu_EnableFooterMenu = true;
			this.dataGridControlAddBtn.OptionsMenu_EnableGroupPanelMenu = true;
			this.dataGridControlAddBtn.OptionsView_ShowGroupPanel = true;
			this.dataGridControlAddBtn.Size = new System.Drawing.Size(522, 371);
			this.dataGridControlAddBtn.TabIndex = 3;
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьToolStripMenuItem,
            this.редактироватьToolStripMenuItem,
            this.удалитьToolStripMenuItem,
            this.историяToolStripMenuItem});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(181, 114);
			this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
			// 
			// добавитьToolStripMenuItem
			// 
			this.добавитьToolStripMenuItem.Name = "добавитьToolStripMenuItem";
			this.добавитьToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.добавитьToolStripMenuItem.Text = "Добавить";
			this.добавитьToolStripMenuItem.Click += new System.EventHandler(this.добавитьToolStripMenuItem_Click);
			// 
			// редактироватьToolStripMenuItem
			// 
			this.редактироватьToolStripMenuItem.Name = "редактироватьToolStripMenuItem";
			this.редактироватьToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.редактироватьToolStripMenuItem.Text = "Редактировать";
			this.редактироватьToolStripMenuItem.Click += new System.EventHandler(this.редактироватьToolStripMenuItem_Click);
			// 
			// удалитьToolStripMenuItem
			// 
			this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
			this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.удалитьToolStripMenuItem.Text = "Удалить";
			this.удалитьToolStripMenuItem.Click += new System.EventHandler(this.удалитьToolStripMenuItem_Click);
			// 
			// историяToolStripMenuItem
			// 
			this.историяToolStripMenuItem.Name = "историяToolStripMenuItem";
			this.историяToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.историяToolStripMenuItem.Text = "История";
			this.историяToolStripMenuItem.Click += new System.EventHandler(this.историяToolStripMenuItem_Click);
			// 
			// StatusEditorControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.dataGridControlAddBtn);
			this.Name = "StatusEditorControl";
			this.Size = new System.Drawing.Size(522, 371);
			this.contextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private SUVPP.UI.Controls.DataGridControlAddBtn dataGridControlAddBtn;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem добавитьToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem редактироватьToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem историяToolStripMenuItem;
	}
}
