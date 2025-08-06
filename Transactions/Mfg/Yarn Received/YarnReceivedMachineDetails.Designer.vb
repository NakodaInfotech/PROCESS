<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class YarnReceivedMachineDetails
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
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.gridbilldetails = New DevExpress.XtraGrid.GridControl()
        Me.gridbill = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GMACRECNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GMACHINENO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GISSUENO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GQUALITY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GLOTNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSHADE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCONES = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGODOWN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGROSSWT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTAREWT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GNETTWT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTOTALGROSSWT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTARENETTWT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTOTALNETTWT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PBPHOTO = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.CHKDONE = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.TOOLEXCEL = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.TOOLREFRESH = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.CMDEXIT = New System.Windows.Forms.Button()
        Me.CMDEDIT = New System.Windows.Forms.Button()
        Me.CMDADD = New System.Windows.Forms.Button()
        Me.GLABOURNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BlendPanel1.SuspendLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PBPHOTO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CHKDONE, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.gridbilldetails)
        Me.BlendPanel1.Controls.Add(Me.ToolStrip1)
        Me.BlendPanel1.Controls.Add(Me.CMDEXIT)
        Me.BlendPanel1.Controls.Add(Me.CMDEDIT)
        Me.BlendPanel1.Controls.Add(Me.CMDADD)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1052, 578)
        Me.BlendPanel1.TabIndex = 11
        '
        'gridbilldetails
        '
        Me.gridbilldetails.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbilldetails.Location = New System.Drawing.Point(15, 40)
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.PBPHOTO, Me.CHKDONE})
        Me.gridbilldetails.Size = New System.Drawing.Size(1034, 486)
        Me.gridbilldetails.TabIndex = 321
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill})
        '
        'gridbill
        '
        Me.gridbill.Appearance.HeaderPanel.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.gridbill.Appearance.HeaderPanel.Options.UseFont = True
        Me.gridbill.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GMACRECNO, Me.GDATE, Me.GMACHINENO, Me.GISSUENO, Me.GQUALITY, Me.GLOTNO, Me.GSHADE, Me.GCONES, Me.GGODOWN, Me.GGROSSWT, Me.GTAREWT, Me.GNETTWT, Me.GTOTALGROSSWT, Me.GTARENETTWT, Me.GTOTALNETTWT, Me.GLABOURNAME})
        Me.gridbill.GridControl = Me.gridbilldetails
        Me.gridbill.Name = "gridbill"
        Me.gridbill.OptionsBehavior.AllowIncrementalSearch = True
        Me.gridbill.OptionsBehavior.AutoExpandAllGroups = True
        Me.gridbill.OptionsBehavior.Editable = False
        Me.gridbill.OptionsCustomization.AllowRowSizing = True
        Me.gridbill.OptionsView.ColumnAutoWidth = False
        Me.gridbill.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.gridbill.OptionsView.ShowAutoFilterRow = True
        Me.gridbill.OptionsView.ShowFooter = True
        '
        'GMACRECNO
        '
        Me.GMACRECNO.Caption = "Sr. No."
        Me.GMACRECNO.FieldName = "MACRECNO"
        Me.GMACRECNO.Name = "GMACRECNO"
        Me.GMACRECNO.OptionsColumn.AllowEdit = False
        Me.GMACRECNO.Visible = True
        Me.GMACRECNO.VisibleIndex = 0
        Me.GMACRECNO.Width = 60
        '
        'GDATE
        '
        Me.GDATE.Caption = "Date"
        Me.GDATE.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GDATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GDATE.FieldName = "DATE"
        Me.GDATE.Name = "GDATE"
        Me.GDATE.OptionsColumn.AllowEdit = False
        Me.GDATE.Visible = True
        Me.GDATE.VisibleIndex = 1
        '
        'GMACHINENO
        '
        Me.GMACHINENO.Caption = "Machine No"
        Me.GMACHINENO.FieldName = "MACHINENAME"
        Me.GMACHINENO.Name = "GMACHINENO"
        Me.GMACHINENO.OptionsColumn.AllowEdit = False
        Me.GMACHINENO.Visible = True
        Me.GMACHINENO.VisibleIndex = 2
        Me.GMACHINENO.Width = 120
        '
        'GISSUENO
        '
        Me.GISSUENO.Caption = "Issue No"
        Me.GISSUENO.FieldName = "ISSUENO"
        Me.GISSUENO.Name = "GISSUENO"
        Me.GISSUENO.Visible = True
        Me.GISSUENO.VisibleIndex = 3
        '
        'GQUALITY
        '
        Me.GQUALITY.Caption = "Quality"
        Me.GQUALITY.FieldName = "QUALITY"
        Me.GQUALITY.Name = "GQUALITY"
        Me.GQUALITY.Visible = True
        Me.GQUALITY.VisibleIndex = 4
        Me.GQUALITY.Width = 150
        '
        'GLOTNO
        '
        Me.GLOTNO.Caption = "Lot No."
        Me.GLOTNO.FieldName = "LOTNO"
        Me.GLOTNO.Name = "GLOTNO"
        Me.GLOTNO.Visible = True
        Me.GLOTNO.VisibleIndex = 5
        Me.GLOTNO.Width = 100
        '
        'GSHADE
        '
        Me.GSHADE.Caption = "Shade"
        Me.GSHADE.FieldName = "SHADE"
        Me.GSHADE.Name = "GSHADE"
        Me.GSHADE.Visible = True
        Me.GSHADE.VisibleIndex = 6
        '
        'GCONES
        '
        Me.GCONES.Caption = "Cones"
        Me.GCONES.FieldName = "CONES"
        Me.GCONES.Name = "GCONES"
        Me.GCONES.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GCONES.Visible = True
        Me.GCONES.VisibleIndex = 7
        '
        'GGODOWN
        '
        Me.GGODOWN.Caption = "Godown"
        Me.GGODOWN.FieldName = "GODOWN"
        Me.GGODOWN.Name = "GGODOWN"
        Me.GGODOWN.OptionsColumn.AllowEdit = False
        Me.GGODOWN.Width = 150
        '
        'GGROSSWT
        '
        Me.GGROSSWT.Caption = "Gross Wt"
        Me.GGROSSWT.DisplayFormat.FormatString = "0.00"
        Me.GGROSSWT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GGROSSWT.FieldName = "GROSSWT"
        Me.GGROSSWT.Name = "GGROSSWT"
        Me.GGROSSWT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GGROSSWT.Visible = True
        Me.GGROSSWT.VisibleIndex = 9
        '
        'GTAREWT
        '
        Me.GTAREWT.Caption = "Tare Wt"
        Me.GTAREWT.DisplayFormat.FormatString = "0.00"
        Me.GTAREWT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTAREWT.FieldName = "TAREWT"
        Me.GTAREWT.Name = "GTAREWT"
        Me.GTAREWT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GTAREWT.Visible = True
        Me.GTAREWT.VisibleIndex = 10
        '
        'GNETTWT
        '
        Me.GNETTWT.Caption = "Nett Wt"
        Me.GNETTWT.DisplayFormat.FormatString = "0.00"
        Me.GNETTWT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GNETTWT.FieldName = "NETTWT"
        Me.GNETTWT.Name = "GNETTWT"
        Me.GNETTWT.OptionsColumn.AllowEdit = False
        Me.GNETTWT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GNETTWT.Visible = True
        Me.GNETTWT.VisibleIndex = 8
        '
        'GTOTALGROSSWT
        '
        Me.GTOTALGROSSWT.Caption = "Total Gross Wt"
        Me.GTOTALGROSSWT.FieldName = "TOTALGROSSWT"
        Me.GTOTALGROSSWT.Name = "GTOTALGROSSWT"
        Me.GTOTALGROSSWT.Visible = True
        Me.GTOTALGROSSWT.VisibleIndex = 11
        '
        'GTARENETTWT
        '
        Me.GTARENETTWT.Caption = "Total Tare wt"
        Me.GTARENETTWT.FieldName = "TOTALTAREWT"
        Me.GTARENETTWT.Name = "GTARENETTWT"
        Me.GTARENETTWT.Visible = True
        Me.GTARENETTWT.VisibleIndex = 12
        '
        'GTOTALNETTWT
        '
        Me.GTOTALNETTWT.Caption = "Total Nett Wt"
        Me.GTOTALNETTWT.FieldName = "TOTALNETTWT"
        Me.GTOTALNETTWT.Name = "GTOTALNETTWT"
        Me.GTOTALNETTWT.Visible = True
        Me.GTOTALNETTWT.VisibleIndex = 13
        '
        'PBPHOTO
        '
        Me.PBPHOTO.Name = "PBPHOTO"
        Me.PBPHOTO.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        '
        'CHKDONE
        '
        Me.CHKDONE.AutoHeight = False
        Me.CHKDONE.Name = "CHKDONE"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TOOLEXCEL, Me.ToolStripSeparator2, Me.TOOLREFRESH, Me.ToolStripSeparator1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1052, 25)
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
        Me.CMDEXIT.Location = New System.Drawing.Point(414, 532)
        Me.CMDEXIT.Name = "CMDEXIT"
        Me.CMDEXIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEXIT.TabIndex = 2
        Me.CMDEXIT.Text = "E&xit"
        Me.CMDEXIT.UseVisualStyleBackColor = True
        '
        'CMDEDIT
        '
        Me.CMDEDIT.Location = New System.Drawing.Point(329, 532)
        Me.CMDEDIT.Name = "CMDEDIT"
        Me.CMDEDIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEDIT.TabIndex = 1
        Me.CMDEDIT.Text = "&Edit"
        Me.CMDEDIT.UseVisualStyleBackColor = True
        '
        'CMDADD
        '
        Me.CMDADD.Location = New System.Drawing.Point(244, 532)
        Me.CMDADD.Name = "CMDADD"
        Me.CMDADD.Size = New System.Drawing.Size(80, 28)
        Me.CMDADD.TabIndex = 0
        Me.CMDADD.Text = "&Add New"
        Me.CMDADD.UseVisualStyleBackColor = True
        '
        'GLABOURNAME
        '
        Me.GLABOURNAME.Caption = "Labour Name"
        Me.GLABOURNAME.FieldName = "LABOURNAME"
        Me.GLABOURNAME.Name = "GLABOURNAME"
        Me.GLABOURNAME.Visible = True
        Me.GLABOURNAME.VisibleIndex = 14
        Me.GLABOURNAME.Width = 150
        '
        'YarnReceivedMachineDetails
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1052, 578)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "YarnReceivedMachineDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Yarn Received Machine Details"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PBPHOTO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CHKDONE, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As BlendPanel
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Private WithEvents GMACRECNO As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GDATE As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GMACHINENO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GISSUENO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GLOTNO As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GGODOWN As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GNETTWT As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents PBPHOTO As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Private WithEvents CHKDONE As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents TOOLEXCEL As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents TOOLREFRESH As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents CMDEXIT As Button
    Friend WithEvents CMDEDIT As Button
    Friend WithEvents CMDADD As Button
    Friend WithEvents GTOTALGROSSWT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTARENETTWT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTOTALNETTWT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCONES As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSHADE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GQUALITY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGROSSWT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTAREWT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GLABOURNAME As DevExpress.XtraGrid.Columns.GridColumn
End Class
