﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RollsRecdDetails
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RollsRecdDetails))
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.TXTCOPIES = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TXTTO = New System.Windows.Forms.TextBox()
        Me.TXTFROM = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.gridbilldetails = New DevExpress.XtraGrid.GridControl()
        Me.gridbill = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GROLLRECDNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGODOWN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GWARPER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCHALLANNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gprogramno = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPROGRAMSRNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GENDS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GLENGTH = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCUT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GQUALITY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GMILLNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GROLLS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GUSEDWT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRETWT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GWT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GNARR = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GOUTROLLS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GOUTWT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCOUNT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GLONGATION = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCUTWT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CHKDONE = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.TOOLEXCEL = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.TOOLREFRESH = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.CMDEXIT = New System.Windows.Forms.Button()
        Me.CMDEDIT = New System.Windows.Forms.Button()
        Me.CMDADD = New System.Windows.Forms.Button()
        Me.PRINTDIALOG = New System.Windows.Forms.PrintDialog()
        Me.PRINTDOC = New System.Drawing.Printing.PrintDocument()
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
        Me.BlendPanel1.Controls.Add(Me.TXTCOPIES)
        Me.BlendPanel1.Controls.Add(Me.Label5)
        Me.BlendPanel1.Controls.Add(Me.Label9)
        Me.BlendPanel1.Controls.Add(Me.Label10)
        Me.BlendPanel1.Controls.Add(Me.TXTTO)
        Me.BlendPanel1.Controls.Add(Me.TXTFROM)
        Me.BlendPanel1.Controls.Add(Me.Label14)
        Me.BlendPanel1.Controls.Add(Me.Label15)
        Me.BlendPanel1.Controls.Add(Me.gridbilldetails)
        Me.BlendPanel1.Controls.Add(Me.ToolStrip1)
        Me.BlendPanel1.Controls.Add(Me.CMDEXIT)
        Me.BlendPanel1.Controls.Add(Me.CMDEDIT)
        Me.BlendPanel1.Controls.Add(Me.CMDADD)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1234, 586)
        Me.BlendPanel1.TabIndex = 9
        '
        'TXTCOPIES
        '
        Me.TXTCOPIES.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTCOPIES.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTCOPIES.Location = New System.Drawing.Point(318, 2)
        Me.TXTCOPIES.Name = "TXTCOPIES"
        Me.TXTCOPIES.Size = New System.Drawing.Size(29, 22)
        Me.TXTCOPIES.TabIndex = 804
        Me.TXTCOPIES.Text = "1"
        Me.TXTCOPIES.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.White
        Me.Label5.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(273, 6)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 14)
        Me.Label5.TabIndex = 807
        Me.Label5.Text = "Copies"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.White
        Me.Label9.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(184, 6)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(18, 14)
        Me.Label9.TabIndex = 806
        Me.Label9.Text = "To"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.White
        Me.Label10.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(94, 6)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(33, 14)
        Me.Label10.TabIndex = 805
        Me.Label10.Text = "From"
        '
        'TXTTO
        '
        Me.TXTTO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTTO.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTTO.Location = New System.Drawing.Point(205, 2)
        Me.TXTTO.Name = "TXTTO"
        Me.TXTTO.Size = New System.Drawing.Size(52, 22)
        Me.TXTTO.TabIndex = 803
        Me.TXTTO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTFROM
        '
        Me.TXTFROM.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTFROM.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTFROM.Location = New System.Drawing.Point(129, 2)
        Me.TXTFROM.Name = "TXTFROM"
        Me.TXTFROM.Size = New System.Drawing.Size(50, 22)
        Me.TXTFROM.TabIndex = 802
        Me.TXTFROM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(37, 551)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(143, 14)
        Me.Label14.TabIndex = 770
        Me.Label14.Text = "Locked (Used in Next Form)"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label15.Location = New System.Drawing.Point(15, 550)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(18, 17)
        Me.Label15.TabIndex = 769
        Me.Label15.Text = "   "
        '
        'gridbilldetails
        '
        Me.gridbilldetails.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbilldetails.Location = New System.Drawing.Point(13, 42)
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.CHKDONE})
        Me.gridbilldetails.Size = New System.Drawing.Size(1208, 495)
        Me.gridbilldetails.TabIndex = 321
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill})
        '
        'gridbill
        '
        Me.gridbill.Appearance.HeaderPanel.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.gridbill.Appearance.HeaderPanel.Options.UseFont = True
        Me.gridbill.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GROLLRECDNO, Me.GDATE, Me.GGODOWN, Me.GWARPER, Me.GCHALLANNO, Me.gprogramno, Me.GPROGRAMSRNO, Me.GQUALITY, Me.GMILLNAME, Me.GENDS, Me.GLENGTH, Me.GTL, Me.GCUT, Me.GROLLS, Me.GWT, Me.GRETWT, Me.GUSEDWT, Me.GCOUNT, Me.GNARR, Me.GOUTROLLS, Me.GOUTWT, Me.GLONGATION, Me.GCUTWT})
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
        'GROLLRECDNO
        '
        Me.GROLLRECDNO.Caption = "Sr. No."
        Me.GROLLRECDNO.FieldName = "ROLLRECDNO"
        Me.GROLLRECDNO.Name = "GROLLRECDNO"
        Me.GROLLRECDNO.OptionsColumn.AllowEdit = False
        Me.GROLLRECDNO.Visible = True
        Me.GROLLRECDNO.VisibleIndex = 0
        Me.GROLLRECDNO.Width = 60
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
        'GGODOWN
        '
        Me.GGODOWN.Caption = "Godown"
        Me.GGODOWN.FieldName = "GODOWN"
        Me.GGODOWN.Name = "GGODOWN"
        Me.GGODOWN.OptionsColumn.AllowEdit = False
        Me.GGODOWN.Width = 100
        '
        'GWARPER
        '
        Me.GWARPER.Caption = "Warper Name"
        Me.GWARPER.FieldName = "WARPER"
        Me.GWARPER.Name = "GWARPER"
        Me.GWARPER.OptionsColumn.AllowEdit = False
        Me.GWARPER.Visible = True
        Me.GWARPER.VisibleIndex = 2
        Me.GWARPER.Width = 220
        '
        'GCHALLANNO
        '
        Me.GCHALLANNO.Caption = "Challan No."
        Me.GCHALLANNO.FieldName = "CHALLANNO"
        Me.GCHALLANNO.Name = "GCHALLANNO"
        Me.GCHALLANNO.OptionsColumn.AllowEdit = False
        Me.GCHALLANNO.Visible = True
        Me.GCHALLANNO.VisibleIndex = 3
        '
        'gprogramno
        '
        Me.gprogramno.Caption = "Prog No"
        Me.gprogramno.FieldName = "PROGRAMNO"
        Me.gprogramno.Name = "gprogramno"
        Me.gprogramno.OptionsColumn.AllowEdit = False
        Me.gprogramno.Width = 60
        '
        'GPROGRAMSRNO
        '
        Me.GPROGRAMSRNO.Caption = "Prog No."
        Me.GPROGRAMSRNO.FieldName = "PROGRAMSRNO"
        Me.GPROGRAMSRNO.Name = "GPROGRAMSRNO"
        Me.GPROGRAMSRNO.Visible = True
        Me.GPROGRAMSRNO.VisibleIndex = 4
        Me.GPROGRAMSRNO.Width = 60
        '
        'GENDS
        '
        Me.GENDS.Caption = "Total Ends"
        Me.GENDS.DisplayFormat.FormatString = "0"
        Me.GENDS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GENDS.FieldName = "TOTALENDS"
        Me.GENDS.Name = "GENDS"
        Me.GENDS.OptionsColumn.AllowEdit = False
        Me.GENDS.Visible = True
        Me.GENDS.VisibleIndex = 7
        '
        'GLENGTH
        '
        Me.GLENGTH.Caption = "Length"
        Me.GLENGTH.FieldName = "LENGTH"
        Me.GLENGTH.Name = "GLENGTH"
        Me.GLENGTH.Visible = True
        Me.GLENGTH.VisibleIndex = 8
        '
        'GTL
        '
        Me.GTL.Caption = "TL"
        Me.GTL.FieldName = "TAPLINE"
        Me.GTL.Name = "GTL"
        Me.GTL.Visible = True
        Me.GTL.VisibleIndex = 9
        '
        'GCUT
        '
        Me.GCUT.Caption = "Cut"
        Me.GCUT.DisplayFormat.FormatString = "0.00"
        Me.GCUT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GCUT.FieldName = "CUT"
        Me.GCUT.Name = "GCUT"
        Me.GCUT.Visible = True
        Me.GCUT.VisibleIndex = 10
        '
        'GQUALITY
        '
        Me.GQUALITY.Caption = "Quality Name"
        Me.GQUALITY.FieldName = "QUALITY"
        Me.GQUALITY.Name = "GQUALITY"
        Me.GQUALITY.OptionsColumn.AllowEdit = False
        Me.GQUALITY.Visible = True
        Me.GQUALITY.VisibleIndex = 5
        Me.GQUALITY.Width = 150
        '
        'GMILLNAME
        '
        Me.GMILLNAME.Caption = "Mill Name"
        Me.GMILLNAME.FieldName = "MILLNAME"
        Me.GMILLNAME.Name = "GMILLNAME"
        Me.GMILLNAME.OptionsColumn.AllowEdit = False
        Me.GMILLNAME.Visible = True
        Me.GMILLNAME.VisibleIndex = 6
        Me.GMILLNAME.Width = 200
        '
        'GROLLS
        '
        Me.GROLLS.Caption = "Rolls"
        Me.GROLLS.FieldName = "TOTALROLLS"
        Me.GROLLS.Name = "GROLLS"
        Me.GROLLS.OptionsColumn.AllowEdit = False
        Me.GROLLS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GROLLS.Visible = True
        Me.GROLLS.VisibleIndex = 11
        Me.GROLLS.Width = 65
        '
        'GUSEDWT
        '
        Me.GUSEDWT.Caption = "Used Wt"
        Me.GUSEDWT.DisplayFormat.FormatString = "0.000"
        Me.GUSEDWT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GUSEDWT.FieldName = "USEDWT"
        Me.GUSEDWT.Name = "GUSEDWT"
        Me.GUSEDWT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GUSEDWT.Visible = True
        Me.GUSEDWT.VisibleIndex = 14
        '
        'GRETWT
        '
        Me.GRETWT.Caption = "Ret Wt"
        Me.GRETWT.FieldName = "RETWT"
        Me.GRETWT.Name = "GRETWT"
        Me.GRETWT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GRETWT.Visible = True
        Me.GRETWT.VisibleIndex = 13
        '
        'GWT
        '
        Me.GWT.Caption = "Roll Wt"
        Me.GWT.DisplayFormat.FormatString = "0.000"
        Me.GWT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GWT.FieldName = "TOTALWT"
        Me.GWT.Name = "GWT"
        Me.GWT.OptionsColumn.AllowEdit = False
        Me.GWT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GWT.Visible = True
        Me.GWT.VisibleIndex = 12
        '
        'GNARR
        '
        Me.GNARR.Caption = "Narration"
        Me.GNARR.FieldName = "NARR"
        Me.GNARR.Name = "GNARR"
        Me.GNARR.OptionsColumn.AllowEdit = False
        Me.GNARR.Width = 180
        '
        'GOUTROLLS
        '
        Me.GOUTROLLS.Caption = "Issued Rolls"
        Me.GOUTROLLS.DisplayFormat.FormatString = "0"
        Me.GOUTROLLS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GOUTROLLS.FieldName = "OUTROLLS"
        Me.GOUTROLLS.Name = "GOUTROLLS"
        Me.GOUTROLLS.OptionsColumn.AllowEdit = False
        '
        'GOUTWT
        '
        Me.GOUTWT.Caption = "Issued Wt"
        Me.GOUTWT.DisplayFormat.FormatString = "0.000"
        Me.GOUTWT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GOUTWT.FieldName = "OUTWT"
        Me.GOUTWT.Name = "GOUTWT"
        Me.GOUTWT.OptionsColumn.AllowEdit = False
        '
        'GCOUNT
        '
        Me.GCOUNT.Caption = "Count"
        Me.GCOUNT.DisplayFormat.FormatString = "0.00"
        Me.GCOUNT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GCOUNT.FieldName = "COUNT"
        Me.GCOUNT.Name = "GCOUNT"
        Me.GCOUNT.Visible = True
        Me.GCOUNT.VisibleIndex = 15
        '
        'GLONGATION
        '
        Me.GLONGATION.Caption = "Longation"
        Me.GLONGATION.DisplayFormat.FormatString = "0.00"
        Me.GLONGATION.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GLONGATION.FieldName = "LONGATION"
        Me.GLONGATION.Name = "GLONGATION"
        Me.GLONGATION.Visible = True
        Me.GLONGATION.VisibleIndex = 16
        '
        'GCUTWT
        '
        Me.GCUTWT.Caption = "Cut Wt"
        Me.GCUTWT.DisplayFormat.FormatString = "0.000"
        Me.GCUTWT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GCUTWT.FieldName = "CUTWT"
        Me.GCUTWT.Name = "GCUTWT"
        Me.GCUTWT.Visible = True
        Me.GCUTWT.VisibleIndex = 17
        '
        'CHKDONE
        '
        Me.CHKDONE.AutoHeight = False
        Me.CHKDONE.Name = "CHKDONE"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TOOLEXCEL, Me.ToolStripSeparator2, Me.TOOLREFRESH, Me.ToolStripSeparator1, Me.PrintToolStripButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1234, 25)
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
        'PrintToolStripButton
        '
        Me.PrintToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PrintToolStripButton.Image = CType(resources.GetObject("PrintToolStripButton.Image"), System.Drawing.Image)
        Me.PrintToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintToolStripButton.Name = "PrintToolStripButton"
        Me.PrintToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.PrintToolStripButton.Text = "&Print"
        '
        'CMDEXIT
        '
        Me.CMDEXIT.Location = New System.Drawing.Point(662, 543)
        Me.CMDEXIT.Name = "CMDEXIT"
        Me.CMDEXIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEXIT.TabIndex = 2
        Me.CMDEXIT.Text = "E&xit"
        Me.CMDEXIT.UseVisualStyleBackColor = True
        '
        'CMDEDIT
        '
        Me.CMDEDIT.Location = New System.Drawing.Point(577, 543)
        Me.CMDEDIT.Name = "CMDEDIT"
        Me.CMDEDIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEDIT.TabIndex = 1
        Me.CMDEDIT.Text = "&Edit"
        Me.CMDEDIT.UseVisualStyleBackColor = True
        '
        'CMDADD
        '
        Me.CMDADD.Location = New System.Drawing.Point(492, 543)
        Me.CMDADD.Name = "CMDADD"
        Me.CMDADD.Size = New System.Drawing.Size(80, 28)
        Me.CMDADD.TabIndex = 0
        Me.CMDADD.Text = "&Add New"
        Me.CMDADD.UseVisualStyleBackColor = True
        '
        'PRINTDIALOG
        '
        Me.PRINTDIALOG.AllowSelection = True
        Me.PRINTDIALOG.AllowSomePages = True
        Me.PRINTDIALOG.ShowHelp = True
        Me.PRINTDIALOG.UseEXDialog = True
        '
        'RollsRecdDetails
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1234, 586)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "RollsRecdDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Rolls Recd Details"
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
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GROLLRECDNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GWARPER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GMILLNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGODOWN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCHALLANNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GROLLS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GQUALITY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GWT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GNARR As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CHKDONE As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents TOOLEXCEL As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TOOLREFRESH As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CMDEXIT As System.Windows.Forms.Button
    Friend WithEvents CMDEDIT As System.Windows.Forms.Button
    Friend WithEvents CMDADD As System.Windows.Forms.Button
    Friend WithEvents gprogramno As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GENDS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GOUTROLLS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GOUTWT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents GPROGRAMSRNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TXTCOPIES As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TXTTO As System.Windows.Forms.TextBox
    Friend WithEvents TXTFROM As System.Windows.Forms.TextBox
    Friend WithEvents PrintToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents PRINTDIALOG As System.Windows.Forms.PrintDialog
    Friend WithEvents PRINTDOC As System.Drawing.Printing.PrintDocument
    Friend WithEvents GUSEDWT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRETWT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCOUNT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GLONGATION As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCUTWT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GLENGTH As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCUT As DevExpress.XtraGrid.Columns.GridColumn
End Class
