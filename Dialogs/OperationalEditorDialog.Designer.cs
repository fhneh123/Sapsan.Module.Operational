namespace Sapsan.Modules.Operational.Dialogs
{
	partial class OperationalEditorDialog
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
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPageWorkEditor = new System.Windows.Forms.TabPage();
			this.operationalWorkControl = new Sapsan.Modules.Operational.Controls.Admin.OperationalWorkControl();
			this.tabPageTypeTask = new System.Windows.Forms.TabPage();
			this.dataGridControlAddBtn = new SUVPP.UI.Controls.DataGridControlAddBtn();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.добавитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.редактироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tabControl.SuspendLayout();
			this.tabPageWorkEditor.SuspendLayout();
			this.tabPageTypeTask.SuspendLayout();
			this.contextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabPageWorkEditor);
			this.tabControl.Controls.Add(this.tabPageTypeTask);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(1036, 640);
			this.tabControl.TabIndex = 1;
			// 
			// tabPageWorkEditor
			// 
			this.tabPageWorkEditor.Controls.Add(this.operationalWorkControl);
			this.tabPageWorkEditor.Location = new System.Drawing.Point(4, 22);
			this.tabPageWorkEditor.Name = "tabPageWorkEditor";
			this.tabPageWorkEditor.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageWorkEditor.Size = new System.Drawing.Size(1028, 614);
			this.tabPageWorkEditor.TabIndex = 0;
			this.tabPageWorkEditor.Text = "Работы";
			this.tabPageWorkEditor.UseVisualStyleBackColor = true;
			// 
			// operationalWorkControl
			// 
			this.operationalWorkControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.operationalWorkControl.Location = new System.Drawing.Point(3, 3);
			this.operationalWorkControl.Name = "operationalWorkControl";
			this.operationalWorkControl.Size = new System.Drawing.Size(1022, 608);
			this.operationalWorkControl.TabIndex = 0;
			// 
			// tabPageTypeTask
			// 
			this.tabPageTypeTask.Controls.Add(this.dataGridControlAddBtn);
			this.tabPageTypeTask.Location = new System.Drawing.Point(4, 22);
			this.tabPageTypeTask.Name = "tabPageTypeTask";
			this.tabPageTypeTask.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageTypeTask.Size = new System.Drawing.Size(1028, 614);
			this.tabPageTypeTask.TabIndex = 1;
			this.tabPageTypeTask.Text = "Типы заданий";
			this.tabPageTypeTask.UseVisualStyleBackColor = true;
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
			this.dataGridControlAddBtn.Size = new System.Drawing.Size(1022, 608);
			this.dataGridControlAddBtn.TabIndex = 0;
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьToolStripMenuItem,
            this.редактироватьToolStripMenuItem,
            this.удалитьToolStripMenuItem});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(181, 92);
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
			this.удалитьToolStripMenuItem.Text = "Удалить...";
			this.удалитьToolStripMenuItem.Click += new System.EventHandler(this.удалитьToolStripMenuItem_Click);
			// 
			// OperationalEditorDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1036, 640);
			this.Controls.Add(this.tabControl);
			this.Name = "OperationalEditorDialog";
			this.Text = "Редактор оперативок";
			this.tabControl.ResumeLayout(false);
			this.tabPageWorkEditor.ResumeLayout(false);
			this.tabPageTypeTask.ResumeLayout(false);
			this.contextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Controls.Admin.OperationalWorkControl operationalWorkControl;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPageWorkEditor;
		private System.Windows.Forms.TabPage tabPageTypeTask;
		private SUVPP.UI.Controls.DataGridControlAddBtn dataGridControlAddBtn;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem добавитьToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem редактироватьToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
	}
}