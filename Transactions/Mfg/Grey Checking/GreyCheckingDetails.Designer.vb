<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GreyCheckingDetails
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.BlendPanel1 = New VbPowerPack.BlendPanel
        Me.Label14 = New System.Windows.Forms.Label
        Me.gridbilldetails = New DevExpress.XtraGrid.GridControl
        Me.gridbill = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.GGREYCHKNO = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GGREYCHKDATE = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GGREYRECDNO = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GREYRECDDATE = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GWEAVER = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GLOOMNO = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GMTRS = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GWT = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GTP = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GPICK = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GPANNA = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GNARR = New DevExpress.XtraGrid.Columns.GridColumn
        Me.CHKDONE = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
        Me.Label15 = New System.Windows.Forms.Label
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.TOOLEXCEL = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.TOOLREFRESH = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.CMDEXIT = New System.Windows.Forms.Button
        Me.CMDEDIT = New System.Windows.Forms.Button
        Me.CMDADD = New System.Windows.Forms.Button
        Me.BlendPanel1.SuspendLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CHKDONE, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.Label14)
        Me.BlendPanel1.Controls.Add(Me.gridbilldetails)
        Me.BlendPanel1.Controls.Add(Me.Label15)
        Me.BlendPanel1.Controls.Add(Me.ToolStrip1)
        Me.BlendPanel1.Controls.Add(Me.CMDEXIT)
        Me.BlendPanel1.Controls.Add(Me.CMDEDIT)
        Me.BlendPanel1.Controls.Add(Me.CMDADD)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1274, 590)
        Me.BlendPanel1.TabIndex = 11
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(42, 544)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(154, 14)
        Me.Label14.TabIndex = 768
        Me.Label14.Text = "Locked (Used in Next Form)"
        '
        'gridbilldetails
        '
        Me.gridbilldetails.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbilldetails.Location = New System.Drawing.Point(22, 41)
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.CHKDONE})
        Me.gridbilldetails.Size = New System.Drawing.Size(1230, 474)
        Me.gridbilldetails.TabIndex = 321
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill})
        '
        'gridbill
        '
        Me.gridbill.Appearance.HeaderPanel.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.gridbill.Appearance.HeaderPanel.Options.UseFont = True
        Me.gridbill.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GGREYCHKNO, Me.GGREYCHKDATE, Me.GGREYRECDNO, Me.GREYRECDDATE, Me.GWEAVER, Me.GLOOMNO, Me.GMTRS, Me.GWT, Me.GTP, Me.GPICK, Me.GPANNA, Me.GNARR})
        Me.gridbill.GridControl = Me.gridbilldetails
        Me.gridbill.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.gridbill.Name = "gridbill"
        Me.gridbill.OptionsBehavior.AllowIncrementalSearch = True
        Me.gridbill.OptionsBehavior.AutoExpandAllGroups = True
        Me.gridbill.OptionsBehavior.Editable = False
        Me.gridbill.OptionsCustomization.AllowRowSizing = True
        Me.gridbill.OptionsView.ColumnAutoWidth = False
        Me.gridbill.OptionsView.ShowAutoFilterRow = True
        Me.gridbill.OptionsView.ShowFooter = True
        '
        'GGREYCHKNO
        '
        Me.GGREYCHKNO.Caption = "Sr. No."
        Me.GGREYCHKNO.FieldName = "GREYCHKNO"
        Me.GGREYCHKNO.Name = "GGREYCHKNO"
        Me.GGREYCHKNO.OptionsColumn.AllowEdit = False
        Me.GGREYCHKNO.Visible = True
        Me.GGREYCHKNO.VisibleIndex = 0
        Me.GGREYCHKNO.Width = 60
        '
        'GGREYCHKDATE
        '
        Me.GGREYCHKDATE.Caption = "Date"
        Me.GGREYCHKDATE.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GGREYCHKDATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GGREYCHKDATE.FieldName = "GREYCHKDATE"
        Me.GGREYCHKDATE.Name = "GGREYCHKDATE"
        Me.GGREYCHKDATE.OptionsColumn.AllowEdit = False
        Me.GGREYCHKDATE.Visible = True
        Me.GGREYCHKDATE.VisibleIndex = 1
        '
        'GGREYRECDNO
        '
        Me.GGREYRECDNO.Caption = "Grey Recd. No."
        Me.GGREYRECDNO.FieldName = "GREYRECDNO"
        Me.GGREYRECDNO.Name = "GGREYRECDNO"
        Me.GGREYRECDNO.OptionsColumn.AllowEdit = False
        Me.GGREYRECDNO.Visible = True
        Me.GGREYRECDNO.VisibleIndex = 2
        Me.GGREYRECDNO.Width = 100
        '
        'GREYRECDDATE
        '
        Me.GREYRECDDATE.Caption = "Grey Recd. Date"
        Me.GREYRECDDATE.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GREYRECDDATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GREYRECDDATE.FieldName = "GREYRECDNO"
        Me.GREYRECDDATE.Name = "GREYRECDDATE"
        Me.GREYRECDDATE.Visible = True
        Me.GREYRECDDATE.VisibleIndex = 3
        Me.GREYRECDDATE.Width = 100
        '
        'GWEAVER
        '
        Me.GWEAVER.Caption = "Weaver Name"
        Me.GWEAVER.FieldName = "WEAVER"
        Me.GWEAVER.Name = "GWEAVER"
        Me.GWEAVER.OptionsColumn.AllowEdit = False
        Me.GWEAVER.Visible = True
        Me.GWEAVER.VisibleIndex = 4
        Me.GWEAVER.Width = 200
        '
        'GLOOMNO
        '
        Me.GLOOMNO.Caption = "Loom No."
        Me.GLOOMNO.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GLOOMNO.FieldName = "LOOMNO"
        Me.GLOOMNO.Name = "GLOOMNO"
        Me.GLOOMNO.OptionsColumn.AllowEdit = False
        Me.GLOOMNO.Visible = True
        Me.GLOOMNO.VisibleIndex = 5
        '
        'GMTRS
        '
        Me.GMTRS.Caption = "Mtrs"
        Me.GMTRS.DisplayFormat.FormatString = "0.00"
        Me.GMTRS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GMTRS.FieldName = "MTRS"
        Me.GMTRS.Name = "GMTRS"
        Me.GMTRS.OptionsColumn.AllowEdit = False
        Me.GMTRS.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GMTRS.Visible = True
        Me.GMTRS.VisibleIndex = 6
        '
        'GWT
        '
        Me.GWT.Caption = "Weight"
        Me.GWT.DisplayFormat.FormatString = "0.000"
        Me.GWT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GWT.FieldName = "WT"
        Me.GWT.Name = "GWT"
        Me.GWT.OptionsColumn.AllowEdit = False
        Me.GWT.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GWT.Visible = True
        Me.GWT.VisibleIndex = 7
        '
        'GTP
        '
        Me.GTP.Caption = "TP"
        Me.GTP.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTP.FieldName = "TP"
        Me.GTP.Name = "GTP"
        Me.GTP.OptionsColumn.AllowEdit = False
        Me.GTP.Visible = True
        Me.GTP.VisibleIndex = 8
        '
        'GPICK
        '
        Me.GPICK.Caption = "Pick"
        Me.GPICK.DisplayFormat.FormatString = "0.00"
        Me.GPICK.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GPICK.FieldName = "PICK"
        Me.GPICK.Name = "GPICK"
        Me.GPICK.OptionsColumn.AllowEdit = False
        Me.GPICK.Visible = True
        Me.GPICK.VisibleIndex = 9
        '
        'GPANNA
        '
        Me.GPANNA.Caption = "Panna"
        Me.GPANNA.DisplayFormat.FormatString = "0.00"
        Me.GPANNA.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GPANNA.FieldName = "PANNA"
        Me.GPANNA.Name = "GPANNA"
        Me.GPANNA.OptionsColumn.AllowEdit = False
        Me.GPANNA.Visible = True
        Me.GPANNA.VisibleIndex = 10
        '
        'GNARR
        '
        Me.GNARR.Caption = "Report"
        Me.GNARR.FieldName = "NARR"
        Me.GNARR.Name = "GNARR"
        Me.GNARR.OptionsColumn.AllowEdit = False
        Me.GNARR.Visible = True
        Me.GNARR.VisibleIndex = 11
        Me.GNARR.Width = 200
        '
        'CHKDONE
        '
        Me.CHKDONE.AutoHeight = False
        Me.CHKDONE.Name = "CHKDONE"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label15.Location = New System.Drawing.Point(20, 543)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(18, 17)
        Me.Label15.TabIndex = 767
        Me.Label15.Text = "   "
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TOOLEXCEL, Me.ToolStripSeparator2, Me.TOOLREFRESH, Me.ToolStripSeparator1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1274, 25)
        Me.ToolStrip1.TabIndex = 3
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'TOOLEXCEL
        '
        Me.TOOLEXCEL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TOOLEXCEL.Image = Global.PROCESS.My.Resources.Resources.Excel_icon
        Me.TOOLEXCEL.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TOOLEXCEL.Name = "TOOLEXCEL"
        Me.TOOLEXCEL.Size = New System.Drawing.Size(23, 22)
        Me.TOOLEXCEL.Text = "Print"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'TOOLREFRESH
        '
        Me.TOOLREFRESH.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TOOLREFRESH.Image = Global.PROCESS.My.Resources.Resources.refresh1
        Me.TOOLREFRESH.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TOOLREFRESH.Name = "TOOLREFRESH"
        Me.TOOLREFRESH.Size = New System.Drawing.Size(23, 22)
        Me.TOOLREFRESH.Text = "ToolStripButton1"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'CMDEXIT
        '
        Me.CMDEXIT.Location = New System.Drawing.Point(682, 543)
        Me.CMDEXIT.Name = "CMDEXIT"
        Me.CMDEXIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEXIT.TabIndex = 2
        Me.CMDEXIT.Text = "E&xit"
        Me.CMDEXIT.UseVisualStyleBackColor = True
        '
        'CMDEDIT
        '
        Me.CMDEDIT.Location = New System.Drawing.Point(597, 543)
        Me.CMDEDIT.Name = "CMDEDIT"
        Me.CMDEDIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEDIT.TabIndex = 1
        Me.CMDEDIT.Text = "&Edit"
        Me.CMDEDIT.UseVisualStyleBackColor = True
        '
        'CMDADD
        '
        Me.CMDADD.Location = New System.Drawing.Point(512, 543)
        Me.CMDADD.Name = "CMDADD"
        Me.CMDADD.Size = New System.Drawing.Size(80, 28)
        Me.CMDADD.TabIndex = 0
        Me.CMDADD.Text = "&Add New"
        Me.CMDADD.UseVisualStyleBackColor = True
        '
        'GreyCheckingDetails
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1274, 590)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "GreyCheckingDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Grey Checking Details"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CHKDONE, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GGREYCHKNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGREYCHKDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGREYRECDNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GWEAVER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GLOOMNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTP As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPICK As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPANNA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GMTRS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GWT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GNARR As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CHKDONE As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents TOOLEXCEL As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TOOLREFRESH As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CMDEXIT As System.Windows.Forms.Button
    Friend WithEvents CMDEDIT As System.Windows.Forms.Button
    Friend WithEvents CMDADD As System.Windows.Forms.Button
    Friend WithEvents GREYRECDDATE As DevExpress.XtraGrid.Columns.GridColumn
End Class
