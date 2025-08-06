<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PurchaseInvoiceDetails
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PurchaseInvoiceDetails))
        Me.cmbregister = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.TOOLWHATSAPP = New System.Windows.Forms.ToolStripButton()
        Me.TOOLGRIDDETAILS = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.gridbilldetails = New DevExpress.XtraGrid.GridControl()
        Me.gridbill = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GSRNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gdate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gname = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGSTIN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPARTYBILLNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBROKERNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GMILLNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTOTALBAGS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTOTALWT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTAXABLEAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCGSTAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSGSTAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GIGSTAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GAPPLYTCS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTCSPER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTCSAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTOTALAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRETURN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GAMTPAID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBALAMT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRCM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CHKRCM = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GBILLDISPUTE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBILLCHECKED = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGRNNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPURTYPE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GREMARKS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPENDINGREPORT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GEWAYBILLNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TXTCOPIES = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TXTTO = New System.Windows.Forms.TextBox()
        Me.TXTFROM = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.cmdok = New System.Windows.Forms.Button()
        Me.lbl = New System.Windows.Forms.Label()
        Me.PRINTDIALOG = New System.Windows.Forms.PrintDialog()
        Me.PRINTDOC = New System.Drawing.Printing.PrintDocument()
        Me.GCHK = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CHKRCM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BlendPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbregister
        '
        Me.cmbregister.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbregister.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbregister.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbregister.FormattingEnabled = True
        Me.cmbregister.Items.AddRange(New Object() {""})
        Me.cmbregister.Location = New System.Drawing.Point(687, 35)
        Me.cmbregister.Name = "cmbregister"
        Me.cmbregister.Size = New System.Drawing.Size(255, 22)
        Me.cmbregister.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(630, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 14)
        Me.Label1.TabIndex = 313
        Me.Label1.Text = "Register"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(59, 22)
        Me.ToolStripButton1.Text = "Add New"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.toolStripSeparator, Me.PrintToolStripButton, Me.ToolStripSeparator1, Me.TOOLWHATSAPP, Me.TOOLGRIDDETAILS, Me.ToolStripSeparator2})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1184, 25)
        Me.ToolStrip1.TabIndex = 4
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'PrintToolStripButton
        '
        Me.PrintToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PrintToolStripButton.Image = Global.PROCESS.My.Resources.Resources.Excel_icon
        Me.PrintToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintToolStripButton.Name = "PrintToolStripButton"
        Me.PrintToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.PrintToolStripButton.Text = "&Print"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'TOOLWHATSAPP
        '
        Me.TOOLWHATSAPP.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TOOLWHATSAPP.Image = Global.PROCESS.My.Resources.Resources.WHATSAPP
        Me.TOOLWHATSAPP.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TOOLWHATSAPP.Name = "TOOLWHATSAPP"
        Me.TOOLWHATSAPP.Size = New System.Drawing.Size(23, 22)
        Me.TOOLWHATSAPP.Text = "Whatsapp Challan Directly"
        '
        'TOOLGRIDDETAILS
        '
        Me.TOOLGRIDDETAILS.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TOOLGRIDDETAILS.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TOOLGRIDDETAILS.Name = "TOOLGRIDDETAILS"
        Me.TOOLGRIDDETAILS.Size = New System.Drawing.Size(77, 22)
        Me.TOOLGRIDDETAILS.Text = "Grid Details"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'gridbilldetails
        '
        Me.gridbilldetails.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbilldetails.Location = New System.Drawing.Point(13, 61)
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.CHKRCM})
        Me.gridbilldetails.Size = New System.Drawing.Size(1158, 454)
        Me.gridbilldetails.TabIndex = 318
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill})
        '
        'gridbill
        '
        Me.gridbill.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GCHK, Me.GSRNO, Me.gdate, Me.gname, Me.GGSTIN, Me.GPARTYBILLNO, Me.GBROKERNAME, Me.GMILLNAME, Me.GTOTALBAGS, Me.GTOTALWT, Me.GTAXABLEAMT, Me.GCGSTAMT, Me.GSGSTAMT, Me.GIGSTAMT, Me.GAPPLYTCS, Me.GTCSPER, Me.GTCSAMT, Me.GTOTALAMT, Me.GRETURN, Me.GAMTPAID, Me.GBALAMT, Me.GRCM, Me.GBILLDISPUTE, Me.GBILLCHECKED, Me.GGRNNO, Me.GPURTYPE, Me.GREMARKS, Me.GPENDINGREPORT, Me.GEWAYBILLNO})
        Me.gridbill.GridControl = Me.gridbilldetails
        Me.gridbill.Images = Me.imageList1
        Me.gridbill.Name = "gridbill"
        Me.gridbill.OptionsBehavior.AllowIncrementalSearch = True
        Me.gridbill.OptionsCustomization.AllowGroup = False
        Me.gridbill.OptionsView.ColumnAutoWidth = False
        Me.gridbill.OptionsView.ShowAutoFilterRow = True
        Me.gridbill.OptionsView.ShowFooter = True
        '
        'GSRNO
        '
        Me.GSRNO.Caption = "Sr. No"
        Me.GSRNO.FieldName = "SRNO"
        Me.GSRNO.Name = "GSRNO"
        Me.GSRNO.OptionsColumn.AllowEdit = False
        Me.GSRNO.Visible = True
        Me.GSRNO.VisibleIndex = 1
        Me.GSRNO.Width = 50
        '
        'gdate
        '
        Me.gdate.Caption = "Date"
        Me.gdate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.gdate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.gdate.FieldName = "Date"
        Me.gdate.Name = "gdate"
        Me.gdate.OptionsColumn.AllowEdit = False
        Me.gdate.Visible = True
        Me.gdate.VisibleIndex = 2
        Me.gdate.Width = 80
        '
        'gname
        '
        Me.gname.Caption = "Vendor Name"
        Me.gname.FieldName = "PARTYNAME"
        Me.gname.ImageIndex = 0
        Me.gname.Name = "gname"
        Me.gname.OptionsColumn.AllowEdit = False
        Me.gname.Visible = True
        Me.gname.VisibleIndex = 3
        Me.gname.Width = 230
        '
        'GGSTIN
        '
        Me.GGSTIN.Caption = "GSTIN"
        Me.GGSTIN.FieldName = "GSTIN"
        Me.GGSTIN.Name = "GGSTIN"
        Me.GGSTIN.OptionsColumn.AllowEdit = False
        Me.GGSTIN.Visible = True
        Me.GGSTIN.VisibleIndex = 4
        Me.GGSTIN.Width = 100
        '
        'GPARTYBILLNO
        '
        Me.GPARTYBILLNO.Caption = "Party Bill No"
        Me.GPARTYBILLNO.FieldName = "PARTYBILLNO"
        Me.GPARTYBILLNO.Name = "GPARTYBILLNO"
        Me.GPARTYBILLNO.OptionsColumn.AllowEdit = False
        Me.GPARTYBILLNO.Visible = True
        Me.GPARTYBILLNO.VisibleIndex = 5
        Me.GPARTYBILLNO.Width = 80
        '
        'GBROKERNAME
        '
        Me.GBROKERNAME.Caption = "Broker Name"
        Me.GBROKERNAME.FieldName = "BROKERNAME"
        Me.GBROKERNAME.Name = "GBROKERNAME"
        Me.GBROKERNAME.OptionsColumn.AllowEdit = False
        Me.GBROKERNAME.Visible = True
        Me.GBROKERNAME.VisibleIndex = 6
        Me.GBROKERNAME.Width = 130
        '
        'GMILLNAME
        '
        Me.GMILLNAME.Caption = "Mill Name"
        Me.GMILLNAME.FieldName = "MILLNAME"
        Me.GMILLNAME.Name = "GMILLNAME"
        Me.GMILLNAME.OptionsColumn.AllowEdit = False
        Me.GMILLNAME.Visible = True
        Me.GMILLNAME.VisibleIndex = 7
        Me.GMILLNAME.Width = 150
        '
        'GTOTALBAGS
        '
        Me.GTOTALBAGS.Caption = "Total Bags"
        Me.GTOTALBAGS.DisplayFormat.FormatString = "0"
        Me.GTOTALBAGS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTOTALBAGS.FieldName = "TOTALBAGS"
        Me.GTOTALBAGS.Name = "GTOTALBAGS"
        Me.GTOTALBAGS.OptionsColumn.AllowEdit = False
        Me.GTOTALBAGS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GTOTALBAGS.Visible = True
        Me.GTOTALBAGS.VisibleIndex = 8
        Me.GTOTALBAGS.Width = 70
        '
        'GTOTALWT
        '
        Me.GTOTALWT.Caption = "Total Wt"
        Me.GTOTALWT.DisplayFormat.FormatString = "0.00"
        Me.GTOTALWT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTOTALWT.FieldName = "TOTALWT"
        Me.GTOTALWT.Name = "GTOTALWT"
        Me.GTOTALWT.OptionsColumn.AllowEdit = False
        Me.GTOTALWT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GTOTALWT.Visible = True
        Me.GTOTALWT.VisibleIndex = 9
        '
        'GTAXABLEAMT
        '
        Me.GTAXABLEAMT.Caption = "Taxable Amt"
        Me.GTAXABLEAMT.DisplayFormat.FormatString = "0.00"
        Me.GTAXABLEAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTAXABLEAMT.FieldName = "TAXABLEAMT"
        Me.GTAXABLEAMT.Name = "GTAXABLEAMT"
        Me.GTAXABLEAMT.OptionsColumn.AllowEdit = False
        Me.GTAXABLEAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GTAXABLEAMT.Visible = True
        Me.GTAXABLEAMT.VisibleIndex = 10
        Me.GTAXABLEAMT.Width = 90
        '
        'GCGSTAMT
        '
        Me.GCGSTAMT.Caption = "CGST Amt"
        Me.GCGSTAMT.DisplayFormat.FormatString = "0.00"
        Me.GCGSTAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GCGSTAMT.FieldName = "CGSTAMT"
        Me.GCGSTAMT.Name = "GCGSTAMT"
        Me.GCGSTAMT.OptionsColumn.AllowEdit = False
        Me.GCGSTAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GCGSTAMT.Visible = True
        Me.GCGSTAMT.VisibleIndex = 11
        '
        'GSGSTAMT
        '
        Me.GSGSTAMT.Caption = "SGST Amt"
        Me.GSGSTAMT.DisplayFormat.FormatString = "0.00"
        Me.GSGSTAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GSGSTAMT.FieldName = "SGSTAMT"
        Me.GSGSTAMT.Name = "GSGSTAMT"
        Me.GSGSTAMT.OptionsColumn.AllowEdit = False
        Me.GSGSTAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GSGSTAMT.Visible = True
        Me.GSGSTAMT.VisibleIndex = 12
        '
        'GIGSTAMT
        '
        Me.GIGSTAMT.Caption = "IGST Amt"
        Me.GIGSTAMT.DisplayFormat.FormatString = "0.00"
        Me.GIGSTAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GIGSTAMT.FieldName = "IGSTAMT"
        Me.GIGSTAMT.Name = "GIGSTAMT"
        Me.GIGSTAMT.OptionsColumn.AllowEdit = False
        Me.GIGSTAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GIGSTAMT.Visible = True
        Me.GIGSTAMT.VisibleIndex = 13
        '
        'GAPPLYTCS
        '
        Me.GAPPLYTCS.Caption = "Apply TCS"
        Me.GAPPLYTCS.FieldName = "APPLYTCS"
        Me.GAPPLYTCS.Name = "GAPPLYTCS"
        Me.GAPPLYTCS.OptionsColumn.AllowEdit = False
        Me.GAPPLYTCS.Visible = True
        Me.GAPPLYTCS.VisibleIndex = 14
        '
        'GTCSPER
        '
        Me.GTCSPER.Caption = "TCS %"
        Me.GTCSPER.DisplayFormat.FormatString = "0.000"
        Me.GTCSPER.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTCSPER.FieldName = "TCSPER"
        Me.GTCSPER.Name = "GTCSPER"
        Me.GTCSPER.OptionsColumn.AllowEdit = False
        Me.GTCSPER.Visible = True
        Me.GTCSPER.VisibleIndex = 15
        '
        'GTCSAMT
        '
        Me.GTCSAMT.Caption = "TCS Amt"
        Me.GTCSAMT.DisplayFormat.FormatString = "0.00"
        Me.GTCSAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTCSAMT.FieldName = "TCSAMT"
        Me.GTCSAMT.Name = "GTCSAMT"
        Me.GTCSAMT.OptionsColumn.AllowEdit = False
        Me.GTCSAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GTCSAMT.Visible = True
        Me.GTCSAMT.VisibleIndex = 16
        '
        'GTOTALAMT
        '
        Me.GTOTALAMT.Caption = "Total Amt"
        Me.GTOTALAMT.DisplayFormat.FormatString = "0.00"
        Me.GTOTALAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTOTALAMT.FieldName = "GRANDTOTAL"
        Me.GTOTALAMT.Name = "GTOTALAMT"
        Me.GTOTALAMT.OptionsColumn.AllowEdit = False
        Me.GTOTALAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GTOTALAMT.Visible = True
        Me.GTOTALAMT.VisibleIndex = 17
        Me.GTOTALAMT.Width = 80
        '
        'GRETURN
        '
        Me.GRETURN.Caption = "Pur Return"
        Me.GRETURN.DisplayFormat.FormatString = "0.00"
        Me.GRETURN.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GRETURN.FieldName = "PURRETURN"
        Me.GRETURN.Name = "GRETURN"
        Me.GRETURN.OptionsColumn.AllowEdit = False
        Me.GRETURN.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GRETURN.Visible = True
        Me.GRETURN.VisibleIndex = 18
        Me.GRETURN.Width = 80
        '
        'GAMTPAID
        '
        Me.GAMTPAID.Caption = "Amt Paid"
        Me.GAMTPAID.DisplayFormat.FormatString = "0.00"
        Me.GAMTPAID.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GAMTPAID.FieldName = "AMTPAID"
        Me.GAMTPAID.Name = "GAMTPAID"
        Me.GAMTPAID.OptionsColumn.AllowEdit = False
        Me.GAMTPAID.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GAMTPAID.Visible = True
        Me.GAMTPAID.VisibleIndex = 19
        Me.GAMTPAID.Width = 80
        '
        'GBALAMT
        '
        Me.GBALAMT.Caption = "Bal Amt"
        Me.GBALAMT.DisplayFormat.FormatString = "0.00"
        Me.GBALAMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GBALAMT.FieldName = "BALANCE"
        Me.GBALAMT.Name = "GBALAMT"
        Me.GBALAMT.OptionsColumn.AllowEdit = False
        Me.GBALAMT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GBALAMT.Visible = True
        Me.GBALAMT.VisibleIndex = 20
        Me.GBALAMT.Width = 80
        '
        'GRCM
        '
        Me.GRCM.Caption = "RCM"
        Me.GRCM.ColumnEdit = Me.CHKRCM
        Me.GRCM.FieldName = "RCM"
        Me.GRCM.Name = "GRCM"
        Me.GRCM.OptionsColumn.AllowEdit = False
        Me.GRCM.Visible = True
        Me.GRCM.VisibleIndex = 21
        Me.GRCM.Width = 40
        '
        'CHKRCM
        '
        Me.CHKRCM.AutoHeight = False
        Me.CHKRCM.Name = "CHKRCM"
        Me.CHKRCM.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'GBILLDISPUTE
        '
        Me.GBILLDISPUTE.Caption = "Bill Disputed"
        Me.GBILLDISPUTE.FieldName = "DISPUTED"
        Me.GBILLDISPUTE.Name = "GBILLDISPUTE"
        Me.GBILLDISPUTE.OptionsColumn.AllowEdit = False
        Me.GBILLDISPUTE.Visible = True
        Me.GBILLDISPUTE.VisibleIndex = 22
        Me.GBILLDISPUTE.Width = 70
        '
        'GBILLCHECKED
        '
        Me.GBILLCHECKED.Caption = "Bill Checked"
        Me.GBILLCHECKED.FieldName = "CHECKED"
        Me.GBILLCHECKED.Name = "GBILLCHECKED"
        Me.GBILLCHECKED.OptionsColumn.AllowEdit = False
        Me.GBILLCHECKED.Visible = True
        Me.GBILLCHECKED.VisibleIndex = 23
        Me.GBILLCHECKED.Width = 70
        '
        'GGRNNO
        '
        Me.GGRNNO.Caption = "GRN No"
        Me.GGRNNO.FieldName = "GRNNO"
        Me.GGRNNO.Name = "GGRNNO"
        Me.GGRNNO.OptionsColumn.AllowEdit = False
        Me.GGRNNO.Visible = True
        Me.GGRNNO.VisibleIndex = 24
        '
        'GPURTYPE
        '
        Me.GPURTYPE.Caption = "Screen Type"
        Me.GPURTYPE.FieldName = "PURTYPE"
        Me.GPURTYPE.Name = "GPURTYPE"
        Me.GPURTYPE.OptionsColumn.AllowEdit = False
        Me.GPURTYPE.Visible = True
        Me.GPURTYPE.VisibleIndex = 26
        Me.GPURTYPE.Width = 100
        '
        'GREMARKS
        '
        Me.GREMARKS.Caption = "Remarks"
        Me.GREMARKS.FieldName = "REMARKS"
        Me.GREMARKS.Name = "GREMARKS"
        Me.GREMARKS.OptionsColumn.AllowEdit = False
        Me.GREMARKS.Visible = True
        Me.GREMARKS.VisibleIndex = 25
        Me.GREMARKS.Width = 145
        '
        'GPENDINGREPORT
        '
        Me.GPENDINGREPORT.Caption = "Pending Report"
        Me.GPENDINGREPORT.FieldName = "PENDINGREPORT"
        Me.GPENDINGREPORT.Name = "GPENDINGREPORT"
        Me.GPENDINGREPORT.OptionsColumn.AllowEdit = False
        Me.GPENDINGREPORT.Visible = True
        Me.GPENDINGREPORT.VisibleIndex = 27
        '
        'GEWAYBILLNO
        '
        Me.GEWAYBILLNO.Caption = "E-Way Bill No."
        Me.GEWAYBILLNO.FieldName = "EWAYBILLNO"
        Me.GEWAYBILLNO.Name = "GEWAYBILLNO"
        Me.GEWAYBILLNO.OptionsColumn.AllowEdit = False
        Me.GEWAYBILLNO.Visible = True
        Me.GEWAYBILLNO.VisibleIndex = 28
        '
        'imageList1
        '
        Me.imageList1.ImageStream = CType(resources.GetObject("imageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imageList1.TransparentColor = System.Drawing.Color.Magenta
        Me.imageList1.Images.SetKeyName(0, "")
        Me.imageList1.Images.SetKeyName(1, "")
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.Label6)
        Me.BlendPanel1.Controls.Add(Me.TXTCOPIES)
        Me.BlendPanel1.Controls.Add(Me.Label9)
        Me.BlendPanel1.Controls.Add(Me.Label10)
        Me.BlendPanel1.Controls.Add(Me.TXTTO)
        Me.BlendPanel1.Controls.Add(Me.TXTFROM)
        Me.BlendPanel1.Controls.Add(Me.Label4)
        Me.BlendPanel1.Controls.Add(Me.Label5)
        Me.BlendPanel1.Controls.Add(Me.Label2)
        Me.BlendPanel1.Controls.Add(Me.Label3)
        Me.BlendPanel1.Controls.Add(Me.Label21)
        Me.BlendPanel1.Controls.Add(Me.Label22)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Controls.Add(Me.cmdok)
        Me.BlendPanel1.Controls.Add(Me.gridbilldetails)
        Me.BlendPanel1.Controls.Add(Me.cmbregister)
        Me.BlendPanel1.Controls.Add(Me.Label1)
        Me.BlendPanel1.Controls.Add(Me.ToolStrip1)
        Me.BlendPanel1.Controls.Add(Me.lbl)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1184, 562)
        Me.BlendPanel1.TabIndex = 4
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.White
        Me.Label6.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(403, 7)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(44, 14)
        Me.Label6.TabIndex = 801
        Me.Label6.Text = "Copies"
        '
        'TXTCOPIES
        '
        Me.TXTCOPIES.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTCOPIES.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTCOPIES.Location = New System.Drawing.Point(448, 3)
        Me.TXTCOPIES.Name = "TXTCOPIES"
        Me.TXTCOPIES.Size = New System.Drawing.Size(29, 22)
        Me.TXTCOPIES.TabIndex = 798
        Me.TXTCOPIES.Text = "1"
        Me.TXTCOPIES.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.White
        Me.Label9.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(319, 7)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(19, 14)
        Me.Label9.TabIndex = 800
        Me.Label9.Text = "To"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.White
        Me.Label10.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(227, 7)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(34, 14)
        Me.Label10.TabIndex = 799
        Me.Label10.Text = "From"
        '
        'TXTTO
        '
        Me.TXTTO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTTO.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTTO.Location = New System.Drawing.Point(340, 3)
        Me.TXTTO.Name = "TXTTO"
        Me.TXTTO.Size = New System.Drawing.Size(52, 22)
        Me.TXTTO.TabIndex = 797
        Me.TXTTO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTFROM
        '
        Me.TXTFROM.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTFROM.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTFROM.Location = New System.Drawing.Point(264, 3)
        Me.TXTFROM.Name = "TXTFROM"
        Me.TXTFROM.Size = New System.Drawing.Size(50, 22)
        Me.TXTFROM.TabIndex = 796
        Me.TXTFROM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Plum
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Location = New System.Drawing.Point(388, 38)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(18, 16)
        Me.Label4.TabIndex = 667
        Me.Label4.Text = "   "
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Red
        Me.Label5.Location = New System.Drawing.Point(409, 39)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(61, 14)
        Me.Label5.TabIndex = 666
        Me.Label5.Text = "Cancelled"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Red
        Me.Label2.Location = New System.Drawing.Point(246, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 14)
        Me.Label2.TabIndex = 665
        Me.Label2.Text = "Disputed"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.LightGreen
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Location = New System.Drawing.Point(309, 38)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(18, 16)
        Me.Label3.TabIndex = 664
        Me.Label3.Text = "   "
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Red
        Me.Label21.Location = New System.Drawing.Point(330, 39)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(52, 14)
        Me.Label21.TabIndex = 663
        Me.Label21.Text = "Checked"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Label22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label22.Location = New System.Drawing.Point(224, 38)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(18, 16)
        Me.Label22.TabIndex = 662
        Me.Label22.Text = "   "
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdexit.FlatAppearance.BorderSize = 0
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.Black
        Me.cmdexit.Location = New System.Drawing.Point(595, 523)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 28)
        Me.cmdexit.TabIndex = 661
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'cmdok
        '
        Me.cmdok.BackColor = System.Drawing.Color.Transparent
        Me.cmdok.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdok.FlatAppearance.BorderSize = 0
        Me.cmdok.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdok.ForeColor = System.Drawing.Color.Black
        Me.cmdok.Location = New System.Drawing.Point(509, 523)
        Me.cmdok.Name = "cmdok"
        Me.cmdok.Size = New System.Drawing.Size(80, 28)
        Me.cmdok.TabIndex = 660
        Me.cmdok.Text = "&Ok"
        Me.cmdok.UseVisualStyleBackColor = False
        '
        'lbl
        '
        Me.lbl.AutoSize = True
        Me.lbl.BackColor = System.Drawing.Color.Transparent
        Me.lbl.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lbl.Location = New System.Drawing.Point(20, 44)
        Me.lbl.Name = "lbl"
        Me.lbl.Size = New System.Drawing.Size(156, 14)
        Me.lbl.TabIndex = 251
        Me.lbl.Text = "Select an Invoice to Change"
        '
        'PRINTDIALOG
        '
        Me.PRINTDIALOG.AllowSelection = True
        Me.PRINTDIALOG.AllowSomePages = True
        Me.PRINTDIALOG.ShowHelp = True
        Me.PRINTDIALOG.UseEXDialog = True
        '
        'GCHK
        '
        Me.GCHK.FieldName = "CHK"
        Me.GCHK.Name = "GCHK"
        Me.GCHK.Visible = True
        Me.GCHK.VisibleIndex = 0
        Me.GCHK.Width = 30
        '
        'PurchaseInvoiceDetails
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1184, 562)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "PurchaseInvoiceDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Purchase Details"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CHKRCM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmbregister As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PrintToolStripButton As System.Windows.Forms.ToolStripButton
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Private WithEvents GSRNO As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents gdate As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents gname As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents imageList1 As System.Windows.Forms.ImageList
    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents lbl As System.Windows.Forms.Label
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents cmdok As System.Windows.Forms.Button
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents GTOTALBAGS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GREMARKS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBILLDISPUTE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBILLCHECKED As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTOTALAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents GPARTYBILLNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GRETURN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBROKERNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPURTYPE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGRNNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRCM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CHKRCM As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GTAXABLEAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCGSTAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSGSTAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GIGSTAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GEWAYBILLNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTOTALWT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPENDINGREPORT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GAPPLYTCS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTCSPER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTCSAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TOOLGRIDDETAILS As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents GGSTIN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GAMTPAID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBALAMT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GMILLNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TOOLWHATSAPP As ToolStripButton
    Friend WithEvents Label6 As Label
    Friend WithEvents TXTCOPIES As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents TXTTO As TextBox
    Friend WithEvents TXTFROM As TextBox
    Friend WithEvents PRINTDIALOG As PrintDialog
    Friend WithEvents PRINTDOC As Printing.PrintDocument
    Friend WithEvents GCHK As DevExpress.XtraGrid.Columns.GridColumn
End Class
