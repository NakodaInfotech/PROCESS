<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GoodsReturnDetails
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GoodsReturnDetails))
        Me.BlendPanel1 = New VbPowerPack.BlendPanel
        Me.gridbilldetails = New DevExpress.XtraGrid.GridControl
        Me.gridbill = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.GGRNO = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GDATE = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GGODOWN = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GNAME = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GLOTNO = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GTRANSNAME = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GGREYQUALITY = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GCHALLANTAKA = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GCHALLANMTRS = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GRETURNTAKA = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GRETURNMTRS = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GRETURNDONO = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GDODATE = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GDAMARAGE = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GRETURNDATE = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GSHORTAGE = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GNETTTAKA = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GNETTMTRS = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GREMARKS = New DevExpress.XtraGrid.Columns.GridColumn
        Me.CHKBLOCKED = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
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
        CType(Me.CHKBLOCKED, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.BlendPanel1.Size = New System.Drawing.Size(1106, 577)
        Me.BlendPanel1.TabIndex = 9
        '
        'gridbilldetails
        '
        Me.gridbilldetails.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbilldetails.Location = New System.Drawing.Point(20, 42)
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.CHKBLOCKED})
        Me.gridbilldetails.Size = New System.Drawing.Size(1068, 495)
        Me.gridbilldetails.TabIndex = 321
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill})
        '
        'gridbill
        '
        Me.gridbill.Appearance.HeaderPanel.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.gridbill.Appearance.HeaderPanel.Options.UseFont = True
        Me.gridbill.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GGRNO, Me.GDATE, Me.GGODOWN, Me.GNAME, Me.GLOTNO, Me.GTRANSNAME, Me.GGREYQUALITY, Me.GCHALLANTAKA, Me.GCHALLANMTRS, Me.GRETURNTAKA, Me.GRETURNMTRS, Me.GRETURNDONO, Me.GDODATE, Me.GDAMARAGE, Me.GRETURNDATE, Me.GSHORTAGE, Me.GNETTTAKA, Me.GNETTMTRS, Me.GREMARKS})
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
        'GGRNO
        '
        Me.GGRNO.Caption = "GR. No."
        Me.GGRNO.FieldName = "GRNO"
        Me.GGRNO.Name = "GGRNO"
        Me.GGRNO.OptionsColumn.AllowEdit = False
        Me.GGRNO.Visible = True
        Me.GGRNO.VisibleIndex = 0
        Me.GGRNO.Width = 60
        '
        'GDATE
        '
        Me.GDATE.Caption = "Date"
        Me.GDATE.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GDATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GDATE.FieldName = "GRDATE"
        Me.GDATE.Name = "GDATE"
        Me.GDATE.OptionsColumn.AllowEdit = False
        Me.GDATE.Visible = True
        Me.GDATE.VisibleIndex = 1
        '
        'GGODOWN
        '
        Me.GGODOWN.Caption = "Godown"
        Me.GGODOWN.FieldName = "GODOWN"
        Me.GGODOWN.Name = "GGODOWN"
        Me.GGODOWN.OptionsColumn.AllowEdit = False
        Me.GGODOWN.Visible = True
        Me.GGODOWN.VisibleIndex = 2
        Me.GGODOWN.Width = 160
        '
        'GNAME
        '
        Me.GNAME.Caption = "Dyeing"
        Me.GNAME.FieldName = "NAME"
        Me.GNAME.Name = "GNAME"
        Me.GNAME.OptionsColumn.AllowEdit = False
        Me.GNAME.Visible = True
        Me.GNAME.VisibleIndex = 3
        Me.GNAME.Width = 250
        '
        'GLOTNO
        '
        Me.GLOTNO.Caption = "Lot No."
        Me.GLOTNO.DisplayFormat.FormatString = "0"
        Me.GLOTNO.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GLOTNO.FieldName = "LOTNO"
        Me.GLOTNO.Name = "GLOTNO"
        Me.GLOTNO.OptionsColumn.AllowEdit = False
        Me.GLOTNO.Visible = True
        Me.GLOTNO.VisibleIndex = 4
        Me.GLOTNO.Width = 60
        '
        'GTRANSNAME
        '
        Me.GTRANSNAME.Caption = "Transport"
        Me.GTRANSNAME.FieldName = "TRANSPORT"
        Me.GTRANSNAME.Name = "GTRANSNAME"
        Me.GTRANSNAME.Visible = True
        Me.GTRANSNAME.VisibleIndex = 5
        Me.GTRANSNAME.Width = 200
        '
        'GGREYQUALITY
        '
        Me.GGREYQUALITY.Caption = "Grey Quality"
        Me.GGREYQUALITY.FieldName = "GREYQUALITY"
        Me.GGREYQUALITY.Name = "GGREYQUALITY"
        Me.GGREYQUALITY.OptionsColumn.AllowEdit = False
        Me.GGREYQUALITY.Visible = True
        Me.GGREYQUALITY.VisibleIndex = 6
        Me.GGREYQUALITY.Width = 125
        '
        'GCHALLANTAKA
        '
        Me.GCHALLANTAKA.Caption = "Challan Taka"
        Me.GCHALLANTAKA.DisplayFormat.FormatString = "0"
        Me.GCHALLANTAKA.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GCHALLANTAKA.FieldName = "CHALLANTAKA"
        Me.GCHALLANTAKA.Name = "GCHALLANTAKA"
        Me.GCHALLANTAKA.OptionsColumn.AllowEdit = False
        Me.GCHALLANTAKA.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GCHALLANTAKA.Visible = True
        Me.GCHALLANTAKA.VisibleIndex = 7
        Me.GCHALLANTAKA.Width = 90
        '
        'GCHALLANMTRS
        '
        Me.GCHALLANMTRS.Caption = "Challan Mtrs."
        Me.GCHALLANMTRS.DisplayFormat.FormatString = "0.00"
        Me.GCHALLANMTRS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GCHALLANMTRS.FieldName = "CHALLANMTRS"
        Me.GCHALLANMTRS.Name = "GCHALLANMTRS"
        Me.GCHALLANMTRS.OptionsColumn.AllowEdit = False
        Me.GCHALLANMTRS.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GCHALLANMTRS.Visible = True
        Me.GCHALLANMTRS.VisibleIndex = 8
        Me.GCHALLANMTRS.Width = 90
        '
        'GRETURNTAKA
        '
        Me.GRETURNTAKA.Caption = "Return Taka"
        Me.GRETURNTAKA.DisplayFormat.FormatString = "0"
        Me.GRETURNTAKA.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GRETURNTAKA.FieldName = "RETURNTAKA"
        Me.GRETURNTAKA.Name = "GRETURNTAKA"
        Me.GRETURNTAKA.OptionsColumn.AllowEdit = False
        Me.GRETURNTAKA.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GRETURNTAKA.Visible = True
        Me.GRETURNTAKA.VisibleIndex = 9
        Me.GRETURNTAKA.Width = 90
        '
        'GRETURNMTRS
        '
        Me.GRETURNMTRS.Caption = "Return Mtrs."
        Me.GRETURNMTRS.DisplayFormat.FormatString = "0.00"
        Me.GRETURNMTRS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GRETURNMTRS.FieldName = "RETURNMTRS"
        Me.GRETURNMTRS.Name = "GRETURNMTRS"
        Me.GRETURNMTRS.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GRETURNMTRS.Visible = True
        Me.GRETURNMTRS.VisibleIndex = 10
        Me.GRETURNMTRS.Width = 90
        '
        'GRETURNDONO
        '
        Me.GRETURNDONO.Caption = "Return DO. No."
        Me.GRETURNDONO.FieldName = "RETURNDONO"
        Me.GRETURNDONO.Name = "GRETURNDONO"
        Me.GRETURNDONO.Visible = True
        Me.GRETURNDONO.VisibleIndex = 11
        Me.GRETURNDONO.Width = 90
        '
        'GDODATE
        '
        Me.GDODATE.Caption = "DO Date"
        Me.GDODATE.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GDODATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GDODATE.FieldName = "DODATE"
        Me.GDODATE.Name = "GDODATE"
        Me.GDODATE.Visible = True
        Me.GDODATE.VisibleIndex = 12
        Me.GDODATE.Width = 90
        '
        'GDAMARAGE
        '
        Me.GDAMARAGE.Caption = "Damarage"
        Me.GDAMARAGE.DisplayFormat.FormatString = "0.00"
        Me.GDAMARAGE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GDAMARAGE.FieldName = "DAMARAGE"
        Me.GDAMARAGE.Name = "GDAMARAGE"
        Me.GDAMARAGE.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GDAMARAGE.Visible = True
        Me.GDAMARAGE.VisibleIndex = 13
        Me.GDAMARAGE.Width = 60
        '
        'GRETURNDATE
        '
        Me.GRETURNDATE.Caption = "Return Date"
        Me.GRETURNDATE.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GRETURNDATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GRETURNDATE.FieldName = "RETURNDATE"
        Me.GRETURNDATE.Name = "GRETURNDATE"
        Me.GRETURNDATE.OptionsColumn.AllowEdit = False
        Me.GRETURNDATE.Visible = True
        Me.GRETURNDATE.VisibleIndex = 14
        Me.GRETURNDATE.Width = 90
        '
        'GSHORTAGE
        '
        Me.GSHORTAGE.Caption = "Shortage"
        Me.GSHORTAGE.DisplayFormat.FormatString = "0.00"
        Me.GSHORTAGE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GSHORTAGE.FieldName = "SHORTAGE"
        Me.GSHORTAGE.Name = "GSHORTAGE"
        Me.GSHORTAGE.OptionsColumn.AllowEdit = False
        Me.GSHORTAGE.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GSHORTAGE.Visible = True
        Me.GSHORTAGE.VisibleIndex = 15
        Me.GSHORTAGE.Width = 90
        '
        'GNETTTAKA
        '
        Me.GNETTTAKA.Caption = "Nett Taka"
        Me.GNETTTAKA.DisplayFormat.FormatString = "0"
        Me.GNETTTAKA.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GNETTTAKA.FieldName = "NETTTAKA"
        Me.GNETTTAKA.Name = "GNETTTAKA"
        Me.GNETTTAKA.OptionsColumn.AllowEdit = False
        Me.GNETTTAKA.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GNETTTAKA.Visible = True
        Me.GNETTTAKA.VisibleIndex = 17
        Me.GNETTTAKA.Width = 80
        '
        'GNETTMTRS
        '
        Me.GNETTMTRS.Caption = "Nett Mtrs."
        Me.GNETTMTRS.DisplayFormat.FormatString = "0.00"
        Me.GNETTMTRS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GNETTMTRS.FieldName = "NETTMTRS"
        Me.GNETTMTRS.Name = "GNETTMTRS"
        Me.GNETTMTRS.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GNETTMTRS.Visible = True
        Me.GNETTMTRS.VisibleIndex = 16
        '
        'GREMARKS
        '
        Me.GREMARKS.Caption = "Remarks"
        Me.GREMARKS.FieldName = "REMARKS"
        Me.GREMARKS.Name = "GREMARKS"
        Me.GREMARKS.OptionsColumn.AllowEdit = False
        Me.GREMARKS.Visible = True
        Me.GREMARKS.VisibleIndex = 18
        Me.GREMARKS.Width = 100
        '
        'CHKBLOCKED
        '
        Me.CHKBLOCKED.AutoHeight = False
        Me.CHKBLOCKED.Name = "CHKBLOCKED"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TOOLEXCEL, Me.ToolStripSeparator2, Me.TOOLREFRESH, Me.ToolStripSeparator1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1106, 25)
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
        Me.TOOLREFRESH.Image = CType(resources.GetObject("TOOLREFRESH.Image"), System.Drawing.Image)
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
        Me.CMDEXIT.Location = New System.Drawing.Point(598, 543)
        Me.CMDEXIT.Name = "CMDEXIT"
        Me.CMDEXIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEXIT.TabIndex = 2
        Me.CMDEXIT.Text = "E&xit"
        Me.CMDEXIT.UseVisualStyleBackColor = True
        '
        'CMDEDIT
        '
        Me.CMDEDIT.Location = New System.Drawing.Point(513, 543)
        Me.CMDEDIT.Name = "CMDEDIT"
        Me.CMDEDIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEDIT.TabIndex = 1
        Me.CMDEDIT.Text = "&Edit"
        Me.CMDEDIT.UseVisualStyleBackColor = True
        '
        'CMDADD
        '
        Me.CMDADD.Location = New System.Drawing.Point(428, 543)
        Me.CMDADD.Name = "CMDADD"
        Me.CMDADD.Size = New System.Drawing.Size(80, 28)
        Me.CMDADD.TabIndex = 0
        Me.CMDADD.Text = "&Add New"
        Me.CMDADD.UseVisualStyleBackColor = True
        '
        'GoodsReturnDetails
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1106, 577)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "GoodsReturnDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Goods Return Details"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CHKBLOCKED, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GGRNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGODOWN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GLOTNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGREYQUALITY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCHALLANTAKA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCHALLANMTRS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRETURNTAKA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRETURNDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSHORTAGE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GNETTTAKA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GREMARKS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CHKBLOCKED As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents TOOLEXCEL As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TOOLREFRESH As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CMDEXIT As System.Windows.Forms.Button
    Friend WithEvents CMDEDIT As System.Windows.Forms.Button
    Friend WithEvents CMDADD As System.Windows.Forms.Button
    Friend WithEvents GRETURNMTRS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRETURNDONO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GNETTMTRS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTRANSNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDODATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDAMARAGE As DevExpress.XtraGrid.Columns.GridColumn
End Class
