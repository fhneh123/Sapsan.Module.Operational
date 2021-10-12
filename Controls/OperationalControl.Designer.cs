namespace Sapsan.Modules.Operational.Controls
{
	partial class OperationalControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OperationalControl));
			this.gridControl = new DevExpress.XtraGrid.GridControl();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.добавитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.редактироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.восстановитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.историяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.gridView = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
			this.panel1 = new System.Windows.Forms.Panel();
			this.checkBoxAnnul = new System.Windows.Forms.CheckBox();
			this.simpleButtonEditWork = new DevExpress.XtraEditors.SimpleButton();
			this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
			this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
			this.label1 = new System.Windows.Forms.Label();
			this.comboBoxOperational = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
			this.contextMenuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// gridControl
			// 
			this.gridControl.ContextMenuStrip = this.contextMenuStrip;
			this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControl.Location = new System.Drawing.Point(0, 32);
			this.gridControl.MainView = this.gridView;
			this.gridControl.Name = "gridControl";
			this.gridControl.Size = new System.Drawing.Size(911, 481);
			this.gridControl.TabIndex = 0;
			this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьToolStripMenuItem,
            this.редактироватьToolStripMenuItem,
            this.удалитьToolStripMenuItem,
            this.восстановитьToolStripMenuItem,
            this.историяToolStripMenuItem});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(155, 114);
			this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
			// 
			// добавитьToolStripMenuItem
			// 
			this.добавитьToolStripMenuItem.Name = "добавитьToolStripMenuItem";
			this.добавитьToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
			this.добавитьToolStripMenuItem.Text = "Добавить";
			this.добавитьToolStripMenuItem.Click += new System.EventHandler(this.добавитьToolStripMenuItem_Click);
			// 
			// редактироватьToolStripMenuItem
			// 
			this.редактироватьToolStripMenuItem.Name = "редактироватьToolStripMenuItem";
			this.редактироватьToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
			this.редактироватьToolStripMenuItem.Text = "Редактировать";
			this.редактироватьToolStripMenuItem.Click += new System.EventHandler(this.редактироватьToolStripMenuItem_Click);
			// 
			// удалитьToolStripMenuItem
			// 
			this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
			this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
			this.удалитьToolStripMenuItem.Text = "Удалить";
			this.удалитьToolStripMenuItem.Click += new System.EventHandler(this.удалитьToolStripMenuItem_Click);
			// 
			// восстановитьToolStripMenuItem
			// 
			this.восстановитьToolStripMenuItem.Name = "восстановитьToolStripMenuItem";
			this.восстановитьToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
			this.восстановитьToolStripMenuItem.Text = "Восстановить";
			this.восстановитьToolStripMenuItem.Click += new System.EventHandler(this.восстановитьToolStripMenuItem_Click);
			// 
			// историяToolStripMenuItem
			// 
			this.историяToolStripMenuItem.Name = "историяToolStripMenuItem";
			this.историяToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
			this.историяToolStripMenuItem.Text = "История";
			this.историяToolStripMenuItem.Click += new System.EventHandler(this.историяToolStripMenuItem_Click);
			// 
			// gridView
			// 
			this.gridView.Appearance.BandPanel.Options.UseTextOptions = true;
			this.gridView.Appearance.BandPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gridView.Appearance.BandPanelBackground.Options.UseTextOptions = true;
			this.gridView.Appearance.BandPanelBackground.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.gridView.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gridView.BandPanelRowHeight = 60;
			this.gridView.ColumnPanelRowHeight = 60;
			this.gridView.GridControl = this.gridControl;
			this.gridView.Name = "gridView";
			this.gridView.OptionsMenu.EnableFooterMenu = false;
			this.gridView.OptionsMenu.EnableGroupPanelMenu = false;
			this.gridView.OptionsView.ColumnAutoWidth = false;
			this.gridView.OptionsView.HeaderFilterButtonShowMode = DevExpress.XtraEditors.Controls.FilterButtonShowMode.Button;
			this.gridView.OptionsView.ShowAutoFilterRow = true;
			this.gridView.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways;
			this.gridView.OptionsView.ShowGroupPanel = false;
			this.gridView.CustomDrawBandHeader += new DevExpress.XtraGrid.Views.BandedGrid.BandHeaderCustomDrawEventHandler(this.gridView_CustomDrawBandHeader);
			this.gridView.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView_RowCellStyle);
			this.gridView.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gridView_CustomRowCellEdit);
			this.gridView.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView_CellValueChanged);
			this.gridView.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.gridView_CustomUnboundColumnData_1);
			this.gridView.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gridView_CustomColumnDisplayText);
			this.gridView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridView_MouseUp);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.checkBoxAnnul);
			this.panel1.Controls.Add(this.simpleButtonEditWork);
			this.panel1.Controls.Add(this.btnPrint);
			this.panel1.Controls.Add(this.btnAdd);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.comboBoxOperational);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(911, 32);
			this.panel1.TabIndex = 1;
			// 
			// checkBoxAnnul
			// 
			this.checkBoxAnnul.AutoSize = true;
			this.checkBoxAnnul.Location = new System.Drawing.Point(453, 7);
			this.checkBoxAnnul.Name = "checkBoxAnnul";
			this.checkBoxAnnul.Size = new System.Drawing.Size(177, 17);
			this.checkBoxAnnul.TabIndex = 11;
			this.checkBoxAnnul.Text = "Показывать аннулированные";
			this.checkBoxAnnul.UseVisualStyleBackColor = true;
			this.checkBoxAnnul.CheckedChanged += new System.EventHandler(this.checkBoxAnnul_CheckedChanged);
			// 
			// simpleButtonEditWork
			// 
			this.simpleButtonEditWork.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.simpleButtonEditWork.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonEditWork.ImageOptions.Image")));
			this.simpleButtonEditWork.Location = new System.Drawing.Point(688, 3);
			this.simpleButtonEditWork.Name = "simpleButtonEditWork";
			this.simpleButtonEditWork.Size = new System.Drawing.Size(139, 23);
			this.simpleButtonEditWork.TabIndex = 10;
			this.simpleButtonEditWork.Text = "Редактор оперативок";
			this.simpleButtonEditWork.Click += new System.EventHandler(this.simpleButtonEditWork_Click);
			// 
			// btnPrint
			// 
			this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPrint.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.ImageOptions.Image")));
			this.btnPrint.Location = new System.Drawing.Point(833, 3);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(75, 23);
			this.btnPrint.TabIndex = 9;
			this.btnPrint.Text = "Печать";
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.ImageOptions.Image")));
			this.btnAdd.Location = new System.Drawing.Point(297, 3);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(150, 23);
			this.btnAdd.TabIndex = 8;
			this.btnAdd.Text = "Добавить оперативку";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(69, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Отображать";
			// 
			// comboBoxOperational
			// 
			this.comboBoxOperational.FormattingEnabled = true;
			this.comboBoxOperational.Location = new System.Drawing.Point(78, 5);
			this.comboBoxOperational.Name = "comboBoxOperational";
			this.comboBoxOperational.Size = new System.Drawing.Size(213, 21);
			this.comboBoxOperational.TabIndex = 0;
			// 
			// OperationalControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gridControl);
			this.Controls.Add(this.panel1);
			this.Name = "OperationalControl";
			this.Size = new System.Drawing.Size(911, 513);
			((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
			this.contextMenuStrip.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraGrid.GridControl gridControl;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboBoxOperational;
		private DevExpress.XtraEditors.SimpleButton btnAdd;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem добавитьToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem редактироватьToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem историяToolStripMenuItem;
		private DevExpress.XtraEditors.SimpleButton btnPrint;
		private DevExpress.XtraEditors.SimpleButton simpleButtonEditWork;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView gridView;
		private System.Windows.Forms.CheckBox checkBoxAnnul;
		private System.Windows.Forms.ToolStripMenuItem восстановитьToolStripMenuItem;
	}
}
