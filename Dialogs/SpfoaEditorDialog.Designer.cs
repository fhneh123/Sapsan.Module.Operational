namespace Sapsan.Modules.Operational.Dialogs
{
	partial class SpfoaEditorDialog
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
			this.components = new System.ComponentModel.Container();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.добавитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.редактироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.историяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPagePgDateEditor = new System.Windows.Forms.TabPage();
			this.dataGridControlAddBtn = new SUVPP.UI.Controls.DataGridControlAddBtn();
			this.tabPageStatusII = new System.Windows.Forms.TabPage();
			this.tabPageStatusExecute = new System.Windows.Forms.TabPage();
			this.contextMenuStrip.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.tabPagePgDateEditor.SuspendLayout();
			this.SuspendLayout();
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
			this.добавитьToolStripMenuItem.Text = "Добавить...";
			this.добавитьToolStripMenuItem.Click += new System.EventHandler(this.добавитьToolStripMenuItem_Click);
			// 
			// редактироватьToolStripMenuItem
			// 
			this.редактироватьToolStripMenuItem.Name = "редактироватьToolStripMenuItem";
			this.редактироватьToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.редактироватьToolStripMenuItem.Text = "Редактировать...";
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
			this.историяToolStripMenuItem.Text = "История...";
			this.историяToolStripMenuItem.Click += new System.EventHandler(this.историяToolStripMenuItem_Click);
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabPagePgDateEditor);
			this.tabControl.Controls.Add(this.tabPageStatusII);
			this.tabControl.Controls.Add(this.tabPageStatusExecute);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(800, 450);
			this.tabControl.TabIndex = 2;
			this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
			// 
			// tabPagePgDateEditor
			// 
			this.tabPagePgDateEditor.Controls.Add(this.dataGridControlAddBtn);
			this.tabPagePgDateEditor.Location = new System.Drawing.Point(4, 22);
			this.tabPagePgDateEditor.Name = "tabPagePgDateEditor";
			this.tabPagePgDateEditor.Padding = new System.Windows.Forms.Padding(3);
			this.tabPagePgDateEditor.Size = new System.Drawing.Size(792, 424);
			this.tabPagePgDateEditor.TabIndex = 0;
			this.tabPagePgDateEditor.Text = "Связи с план-графиком";
			this.tabPagePgDateEditor.UseVisualStyleBackColor = true;
			// 
			// dataGridControlAddBtn
			// 
			this.dataGridControlAddBtn.ColumnPanelRowHeight = -1;
			this.dataGridControlAddBtn.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridControlAddBtn.HeaderPanelWordWrap = DevExpress.Utils.WordWrap.Default;
			this.dataGridControlAddBtn.IsBtnAddVisible = true;
			this.dataGridControlAddBtn.IsBtnExportVisible = false;
			this.dataGridControlAddBtn.Location = new System.Drawing.Point(3, 3);
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
			this.dataGridControlAddBtn.Size = new System.Drawing.Size(786, 418);
			this.dataGridControlAddBtn.TabIndex = 0;
			// 
			// tabPageStatusII
			// 
			this.tabPageStatusII.Location = new System.Drawing.Point(4, 22);
			this.tabPageStatusII.Name = "tabPageStatusII";
			this.tabPageStatusII.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageStatusII.Size = new System.Drawing.Size(792, 424);
			this.tabPageStatusII.TabIndex = 1;
			this.tabPageStatusII.Text = "Статусы СПФОА ИИ";
			this.tabPageStatusII.UseVisualStyleBackColor = true;
			// 
			// tabPageStatusExecute
			// 
			this.tabPageStatusExecute.Location = new System.Drawing.Point(4, 22);
			this.tabPageStatusExecute.Name = "tabPageStatusExecute";
			this.tabPageStatusExecute.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageStatusExecute.Size = new System.Drawing.Size(792, 424);
			this.tabPageStatusExecute.TabIndex = 2;
			this.tabPageStatusExecute.Text = "Статусы выполнения";
			this.tabPageStatusExecute.UseVisualStyleBackColor = true;
			// 
			// SpfoaEditorDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.tabControl);
			this.Name = "SpfoaEditorDialog";
			this.Text = "Редактор СПФОА";
			this.contextMenuStrip.ResumeLayout(false);
			this.tabControl.ResumeLayout(false);
			this.tabPagePgDateEditor.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem добавитьToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem редактироватьToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem историяToolStripMenuItem;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPagePgDateEditor;
		private System.Windows.Forms.TabPage tabPageStatusII;
		private System.Windows.Forms.TabPage tabPageStatusExecute;
		private SUVPP.UI.Controls.DataGridControlAddBtn dataGridControlAddBtn;
	}
}