<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PurchaseInvoiceFilter
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
        Me.BlendPanel2 = New VbPowerPack.BlendPanel
        Me.CMBTYPE = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.CHKRSELECTALL = New System.Windows.Forms.CheckBox
        Me.GRIDBILLDETAILSR = New DevExpress.XtraGrid.GridControl
        Me.GRIDBILLR = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.RDBRSELECTED = New System.Windows.Forms.RadioButton
        Me.RadioButton3 = New System.Windows.Forms.RadioButton
        Me.txtadd = New System.Windows.Forms.TextBox
        Me.CMBEFFECTQUALITY = New System.Windows.Forms.ComboBox
        Me.LBLEFFECTQTY = New System.Windows.Forms.Label
        Me.CHKPRINTDATE = New System.Windows.Forms.CheckBox
        Me.cmbregister = New System.Windows.Forms.ComboBox
        Me.Label37 = New System.Windows.Forms.Label
        Me.CHKADDRESS = New System.Windows.Forms.CheckBox
        Me.CHKHEADER = New System.Windows.Forms.CheckBox
        Me.CHKGROUPONNEWPG = New System.Windows.Forms.CheckBox
        Me.LBLNAME = New System.Windows.Forms.Label
        Me.CMBMILL = New System.Windows.Forms.ComboBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.CHKSELECTALL = New System.Windows.Forms.CheckBox
        Me.gridbilldetails = New DevExpress.XtraGrid.GridControl
        Me.gridbill = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.GCHK = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GNAME = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GUNDER = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GCITY = New DevExpress.XtraGrid.Columns.GridColumn
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.RBSELECTED = New System.Windows.Forms.RadioButton
        Me.RadioButton1 = New System.Windows.Forms.RadioButton
        Me.LBLTRANS = New System.Windows.Forms.Label
        Me.cmbtrans = New System.Windows.Forms.ComboBox
        Me.CHKSUMMARY = New System.Windows.Forms.CheckBox
        Me.CMBCODE = New System.Windows.Forms.ComboBox
        Me.txtDeliveryadd = New System.Windows.Forms.TextBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.RDBGSTREGISTER = New System.Windows.Forms.RadioButton
        Me.LBLRATE = New System.Windows.Forms.Label
        Me.TXTRATE = New System.Windows.Forms.TextBox
        Me.RDBREGISTER = New System.Windows.Forms.RadioButton
        Me.RDBDETAILS = New System.Windows.Forms.RadioButton
        Me.RDAVGPURQUALITY = New System.Windows.Forms.RadioButton
        Me.RDAVGPURMONTHLY = New System.Windows.Forms.RadioButton
        Me.RDBNAME = New System.Windows.Forms.RadioButton
        Me.CMBSIGN = New System.Windows.Forms.ComboBox
        Me.LBLAMT = New System.Windows.Forms.Label
        Me.TXTAMT = New System.Windows.Forms.TextBox
        Me.RDBTRANS = New System.Windows.Forms.RadioButton
        Me.RDBMONTHLY = New System.Windows.Forms.RadioButton
        Me.RDQUALITY = New System.Windows.Forms.RadioButton
        Me.RDBAGENT = New System.Windows.Forms.RadioButton
        Me.RDBPARTY = New System.Windows.Forms.RadioButton
        Me.LBLAGENT = New System.Windows.Forms.Label
        Me.CMBAGENT = New System.Windows.Forms.ComboBox
        Me.chkdate = New System.Windows.Forms.CheckBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.dtto = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.dtfrom = New System.Windows.Forms.DateTimePicker
        Me.Label7 = New System.Windows.Forms.Label
        Me.CMBQUALITY = New System.Windows.Forms.ComboBox
        Me.LBLQUALITY = New System.Windows.Forms.Label
        Me.CMBNAME = New System.Windows.Forms.ComboBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.cmdshow = New System.Windows.Forms.Button
        Me.cmdexit = New System.Windows.Forms.Button
        Me.BlendPanel2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.GRIDBILLDETAILSR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRIDBILLR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BlendPanel2
        '
        Me.BlendPanel2.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel2.Controls.Add(Me.CMBTYPE)
        Me.BlendPanel2.Controls.Add(Me.Label2)
        Me.BlendPanel2.Controls.Add(Me.GroupBox2)
        Me.BlendPanel2.Controls.Add(Me.txtadd)
        Me.BlendPanel2.Controls.Add(Me.CMBEFFECTQUALITY)
        Me.BlendPanel2.Controls.Add(Me.LBLEFFECTQTY)
        Me.BlendPanel2.Controls.Add(Me.CHKPRINTDATE)
        Me.BlendPanel2.Controls.Add(Me.cmbregister)
        Me.BlendPanel2.Controls.Add(Me.Label37)
        Me.BlendPanel2.Controls.Add(Me.CHKADDRESS)
        Me.BlendPanel2.Controls.Add(Me.CHKHEADER)
        Me.BlendPanel2.Controls.Add(Me.CHKGROUPONNEWPG)
        Me.BlendPanel2.Controls.Add(Me.LBLNAME)
        Me.BlendPanel2.Controls.Add(Me.CMBMILL)
        Me.BlendPanel2.Controls.Add(Me.GroupBox4)
        Me.BlendPanel2.Controls.Add(Me.LBLTRANS)
        Me.BlendPanel2.Controls.Add(Me.cmbtrans)
        Me.BlendPanel2.Controls.Add(Me.CHKSUMMARY)
        Me.BlendPanel2.Controls.Add(Me.CMBCODE)
        Me.BlendPanel2.Controls.Add(Me.txtDeliveryadd)
        Me.BlendPanel2.Controls.Add(Me.GroupBox3)
        Me.BlendPanel2.Controls.Add(Me.LBLAGENT)
        Me.BlendPanel2.Controls.Add(Me.CMBAGENT)
        Me.BlendPanel2.Controls.Add(Me.chkdate)
        Me.BlendPanel2.Controls.Add(Me.GroupBox1)
        Me.BlendPanel2.Controls.Add(Me.CMBQUALITY)
        Me.BlendPanel2.Controls.Add(Me.LBLQUALITY)
        Me.BlendPanel2.Controls.Add(Me.CMBNAME)
        Me.BlendPanel2.Controls.Add(Me.Label9)
        Me.BlendPanel2.Controls.Add(Me.cmdshow)
        Me.BlendPanel2.Controls.Add(Me.cmdexit)
        Me.BlendPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel2.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel2.Name = "BlendPanel2"
        Me.BlendPanel2.Size = New System.Drawing.Size(1037, 723)
        Me.BlendPanel2.TabIndex = 0
        '
        'CMBTYPE
        '
        Me.CMBTYPE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBTYPE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBTYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CMBTYPE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBTYPE.FormattingEnabled = True
        Me.CMBTYPE.Items.AddRange(New Object() {"", "COMMON PURCHASE", "FINISHED PURCHASE", "GREY PURCHASE", "PROCESS CHGS", "SIZING CHGS", "TRANSPORT CHGS", "WARPER CHGS", "WEAVING CHGS", "YARN PURCHASE"})
        Me.CMBTYPE.Location = New System.Drawing.Point(411, 78)
        Me.CMBTYPE.Name = "CMBTYPE"
        Me.CMBTYPE.Size = New System.Drawing.Size(230, 23)
        Me.CMBTYPE.TabIndex = 761
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(346, 82)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 14)
        Me.Label2.TabIndex = 760
        Me.Label2.Text = "Pur Screen"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.CHKRSELECTALL)
        Me.GroupBox2.Controls.Add(Me.GRIDBILLDETAILSR)
        Me.GroupBox2.Controls.Add(Me.GroupBox6)
        Me.GroupBox2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(54, 163)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(355, 194)
        Me.GroupBox2.TabIndex = 759
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Register Selection"
        '
        'CHKRSELECTALL
        '
        Me.CHKRSELECTALL.AutoSize = True
        Me.CHKRSELECTALL.BackColor = System.Drawing.Color.Transparent
        Me.CHKRSELECTALL.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHKRSELECTALL.ForeColor = System.Drawing.Color.Black
        Me.CHKRSELECTALL.Location = New System.Drawing.Point(18, 22)
        Me.CHKRSELECTALL.Name = "CHKRSELECTALL"
        Me.CHKRSELECTALL.Size = New System.Drawing.Size(77, 18)
        Me.CHKRSELECTALL.TabIndex = 0
        Me.CHKRSELECTALL.Text = "Select All"
        Me.CHKRSELECTALL.UseVisualStyleBackColor = False
        '
        'GRIDBILLDETAILSR
        '
        Me.GRIDBILLDETAILSR.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDBILLDETAILSR.Location = New System.Drawing.Point(18, 43)
        Me.GRIDBILLDETAILSR.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GRIDBILLDETAILSR.MainView = Me.GRIDBILLR
        Me.GRIDBILLDETAILSR.Name = "GRIDBILLDETAILSR"
        Me.GRIDBILLDETAILSR.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit2})
        Me.GRIDBILLDETAILSR.Size = New System.Drawing.Size(321, 142)
        Me.GRIDBILLDETAILSR.TabIndex = 3
        Me.GRIDBILLDETAILSR.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GRIDBILLR})
        '
        'GRIDBILLR
        '
        Me.GRIDBILLR.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRIDBILLR.Appearance.Row.Options.UseFont = True
        Me.GRIDBILLR.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.GRIDBILLR.GridControl = Me.GRIDBILLDETAILSR
        Me.GRIDBILLR.Name = "GRIDBILLR"
        Me.GRIDBILLR.OptionsBehavior.AllowIncrementalSearch = True
        Me.GRIDBILLR.OptionsView.ColumnAutoWidth = False
        Me.GRIDBILLR.OptionsView.ShowAutoFilterRow = True
        Me.GRIDBILLR.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.FieldName = "CHKR"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.ShowCaption = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 50
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Register"
        Me.GridColumn2.FieldName = "REGISTER"
        Me.GridColumn2.ImageIndex = 0
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 230
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        Me.RepositoryItemCheckEdit2.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'GroupBox6
        '
        Me.GroupBox6.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox6.Controls.Add(Me.RDBRSELECTED)
        Me.GroupBox6.Controls.Add(Me.RadioButton3)
        Me.GroupBox6.Location = New System.Drawing.Point(162, 4)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(157, 38)
        Me.GroupBox6.TabIndex = 1
        Me.GroupBox6.TabStop = False
        '
        'RDBRSELECTED
        '
        Me.RDBRSELECTED.AutoSize = True
        Me.RDBRSELECTED.BackColor = System.Drawing.Color.Transparent
        Me.RDBRSELECTED.Location = New System.Drawing.Point(66, 14)
        Me.RDBRSELECTED.Name = "RDBRSELECTED"
        Me.RDBRSELECTED.Size = New System.Drawing.Size(70, 19)
        Me.RDBRSELECTED.TabIndex = 1
        Me.RDBRSELECTED.Text = "Selected"
        Me.RDBRSELECTED.UseVisualStyleBackColor = False
        '
        'RadioButton3
        '
        Me.RadioButton3.AutoSize = True
        Me.RadioButton3.BackColor = System.Drawing.Color.Transparent
        Me.RadioButton3.Checked = True
        Me.RadioButton3.Location = New System.Drawing.Point(6, 14)
        Me.RadioButton3.Name = "RadioButton3"
        Me.RadioButton3.Size = New System.Drawing.Size(40, 19)
        Me.RadioButton3.TabIndex = 0
        Me.RadioButton3.TabStop = True
        Me.RadioButton3.Text = "All"
        Me.RadioButton3.UseVisualStyleBackColor = False
        '
        'txtadd
        '
        Me.txtadd.BackColor = System.Drawing.Color.White
        Me.txtadd.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtadd.ForeColor = System.Drawing.Color.DimGray
        Me.txtadd.Location = New System.Drawing.Point(959, 652)
        Me.txtadd.Multiline = True
        Me.txtadd.Name = "txtadd"
        Me.txtadd.ReadOnly = True
        Me.txtadd.Size = New System.Drawing.Size(10, 19)
        Me.txtadd.TabIndex = 758
        Me.txtadd.Visible = False
        '
        'CMBEFFECTQUALITY
        '
        Me.CMBEFFECTQUALITY.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBEFFECTQUALITY.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBEFFECTQUALITY.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBEFFECTQUALITY.FormattingEnabled = True
        Me.CMBEFFECTQUALITY.Location = New System.Drawing.Point(411, 50)
        Me.CMBEFFECTQUALITY.MaxDropDownItems = 14
        Me.CMBEFFECTQUALITY.Name = "CMBEFFECTQUALITY"
        Me.CMBEFFECTQUALITY.Size = New System.Drawing.Size(230, 22)
        Me.CMBEFFECTQUALITY.TabIndex = 6
        '
        'LBLEFFECTQTY
        '
        Me.LBLEFFECTQTY.AutoSize = True
        Me.LBLEFFECTQTY.BackColor = System.Drawing.Color.Transparent
        Me.LBLEFFECTQTY.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLEFFECTQTY.ForeColor = System.Drawing.Color.Black
        Me.LBLEFFECTQTY.Location = New System.Drawing.Point(331, 54)
        Me.LBLEFFECTQTY.Name = "LBLEFFECTQTY"
        Me.LBLEFFECTQTY.Size = New System.Drawing.Size(78, 14)
        Me.LBLEFFECTQTY.TabIndex = 757
        Me.LBLEFFECTQTY.Text = "Effect Quality"
        '
        'CHKPRINTDATE
        '
        Me.CHKPRINTDATE.AutoSize = True
        Me.CHKPRINTDATE.BackColor = System.Drawing.Color.Transparent
        Me.CHKPRINTDATE.Checked = True
        Me.CHKPRINTDATE.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CHKPRINTDATE.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHKPRINTDATE.ForeColor = System.Drawing.Color.Black
        Me.CHKPRINTDATE.Location = New System.Drawing.Point(785, 115)
        Me.CHKPRINTDATE.Name = "CHKPRINTDATE"
        Me.CHKPRINTDATE.Size = New System.Drawing.Size(112, 18)
        Me.CHKPRINTDATE.TabIndex = 13
        Me.CHKPRINTDATE.Text = "Show Print Date"
        Me.CHKPRINTDATE.UseVisualStyleBackColor = False
        '
        'cmbregister
        '
        Me.cmbregister.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbregister.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbregister.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbregister.FormattingEnabled = True
        Me.cmbregister.Items.AddRange(New Object() {""})
        Me.cmbregister.Location = New System.Drawing.Point(411, 21)
        Me.cmbregister.Name = "cmbregister"
        Me.cmbregister.Size = New System.Drawing.Size(230, 23)
        Me.cmbregister.TabIndex = 5
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.BackColor = System.Drawing.Color.Transparent
        Me.Label37.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(358, 25)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(51, 15)
        Me.Label37.TabIndex = 754
        Me.Label37.Text = "Register"
        '
        'CHKADDRESS
        '
        Me.CHKADDRESS.AutoSize = True
        Me.CHKADDRESS.BackColor = System.Drawing.Color.Transparent
        Me.CHKADDRESS.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHKADDRESS.ForeColor = System.Drawing.Color.Black
        Me.CHKADDRESS.Location = New System.Drawing.Point(658, 115)
        Me.CHKADDRESS.Name = "CHKADDRESS"
        Me.CHKADDRESS.Size = New System.Drawing.Size(102, 18)
        Me.CHKADDRESS.TabIndex = 10
        Me.CHKADDRESS.Text = "Show Address"
        Me.CHKADDRESS.UseVisualStyleBackColor = False
        '
        'CHKHEADER
        '
        Me.CHKHEADER.AutoSize = True
        Me.CHKHEADER.BackColor = System.Drawing.Color.Transparent
        Me.CHKHEADER.Checked = True
        Me.CHKHEADER.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CHKHEADER.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHKHEADER.ForeColor = System.Drawing.Color.Black
        Me.CHKHEADER.Location = New System.Drawing.Point(658, 139)
        Me.CHKHEADER.Name = "CHKHEADER"
        Me.CHKHEADER.Size = New System.Drawing.Size(98, 18)
        Me.CHKHEADER.TabIndex = 11
        Me.CHKHEADER.Text = "Show Header"
        Me.CHKHEADER.UseVisualStyleBackColor = False
        '
        'CHKGROUPONNEWPG
        '
        Me.CHKGROUPONNEWPG.AutoSize = True
        Me.CHKGROUPONNEWPG.BackColor = System.Drawing.Color.Transparent
        Me.CHKGROUPONNEWPG.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHKGROUPONNEWPG.ForeColor = System.Drawing.Color.Black
        Me.CHKGROUPONNEWPG.Location = New System.Drawing.Point(785, 93)
        Me.CHKGROUPONNEWPG.Name = "CHKGROUPONNEWPG"
        Me.CHKGROUPONNEWPG.Size = New System.Drawing.Size(193, 18)
        Me.CHKGROUPONNEWPG.TabIndex = 12
        Me.CHKGROUPONNEWPG.Text = "Show Each Group On New Page"
        Me.CHKGROUPONNEWPG.UseVisualStyleBackColor = False
        '
        'LBLNAME
        '
        Me.LBLNAME.AutoSize = True
        Me.LBLNAME.BackColor = System.Drawing.Color.Transparent
        Me.LBLNAME.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLNAME.Location = New System.Drawing.Point(25, 108)
        Me.LBLNAME.Name = "LBLNAME"
        Me.LBLNAME.Size = New System.Drawing.Size(64, 15)
        Me.LBLNAME.TabIndex = 746
        Me.LBLNAME.Text = "Mill Name"
        '
        'CMBMILL
        '
        Me.CMBMILL.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBMILL.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBMILL.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBMILL.FormattingEnabled = True
        Me.CMBMILL.Location = New System.Drawing.Point(91, 105)
        Me.CMBMILL.MaxDropDownItems = 14
        Me.CMBMILL.Name = "CMBMILL"
        Me.CMBMILL.Size = New System.Drawing.Size(230, 23)
        Me.CMBMILL.TabIndex = 3
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox4.Controls.Add(Me.CHKSELECTALL)
        Me.GroupBox4.Controls.Add(Me.gridbilldetails)
        Me.GroupBox4.Controls.Add(Me.GroupBox5)
        Me.GroupBox4.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(50, 354)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(692, 359)
        Me.GroupBox4.TabIndex = 15
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Selection"
        '
        'CHKSELECTALL
        '
        Me.CHKSELECTALL.AutoSize = True
        Me.CHKSELECTALL.BackColor = System.Drawing.Color.Transparent
        Me.CHKSELECTALL.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHKSELECTALL.ForeColor = System.Drawing.Color.Black
        Me.CHKSELECTALL.Location = New System.Drawing.Point(18, 22)
        Me.CHKSELECTALL.Name = "CHKSELECTALL"
        Me.CHKSELECTALL.Size = New System.Drawing.Size(77, 18)
        Me.CHKSELECTALL.TabIndex = 0
        Me.CHKSELECTALL.Text = "Select All"
        Me.CHKSELECTALL.UseVisualStyleBackColor = False
        '
        'gridbilldetails
        '
        Me.gridbilldetails.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbilldetails.Location = New System.Drawing.Point(18, 43)
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.gridbilldetails.Size = New System.Drawing.Size(656, 310)
        Me.gridbilldetails.TabIndex = 3
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill})
        '
        'gridbill
        '
        Me.gridbill.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GCHK, Me.GNAME, Me.GUNDER, Me.GCITY})
        Me.gridbill.GridControl = Me.gridbilldetails
        Me.gridbill.Name = "gridbill"
        Me.gridbill.OptionsBehavior.AllowIncrementalSearch = True
        Me.gridbill.OptionsView.ColumnAutoWidth = False
        Me.gridbill.OptionsView.ShowAutoFilterRow = True
        Me.gridbill.OptionsView.ShowFooter = True
        '
        'GCHK
        '
        Me.GCHK.FieldName = "CHK"
        Me.GCHK.Name = "GCHK"
        Me.GCHK.OptionsColumn.ShowCaption = False
        Me.GCHK.Visible = True
        Me.GCHK.VisibleIndex = 0
        Me.GCHK.Width = 50
        '
        'GNAME
        '
        Me.GNAME.Caption = "Name"
        Me.GNAME.FieldName = "NAME"
        Me.GNAME.ImageIndex = 0
        Me.GNAME.Name = "GNAME"
        Me.GNAME.OptionsColumn.AllowEdit = False
        Me.GNAME.Visible = True
        Me.GNAME.VisibleIndex = 1
        Me.GNAME.Width = 230
        '
        'GUNDER
        '
        Me.GUNDER.Caption = "Under"
        Me.GUNDER.FieldName = "UNDER"
        Me.GUNDER.Name = "GUNDER"
        Me.GUNDER.OptionsColumn.AllowEdit = False
        Me.GUNDER.Visible = True
        Me.GUNDER.VisibleIndex = 2
        Me.GUNDER.Width = 180
        '
        'GCITY
        '
        Me.GCITY.Caption = "City"
        Me.GCITY.FieldName = "CITY"
        Me.GCITY.Name = "GCITY"
        Me.GCITY.OptionsColumn.AllowEdit = False
        Me.GCITY.Visible = True
        Me.GCITY.VisibleIndex = 3
        Me.GCITY.Width = 150
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox5.Controls.Add(Me.RBSELECTED)
        Me.GroupBox5.Controls.Add(Me.RadioButton1)
        Me.GroupBox5.Location = New System.Drawing.Point(268, 4)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(157, 38)
        Me.GroupBox5.TabIndex = 1
        Me.GroupBox5.TabStop = False
        '
        'RBSELECTED
        '
        Me.RBSELECTED.AutoSize = True
        Me.RBSELECTED.BackColor = System.Drawing.Color.Transparent
        Me.RBSELECTED.Location = New System.Drawing.Point(66, 14)
        Me.RBSELECTED.Name = "RBSELECTED"
        Me.RBSELECTED.Size = New System.Drawing.Size(70, 19)
        Me.RBSELECTED.TabIndex = 1
        Me.RBSELECTED.Text = "Selected"
        Me.RBSELECTED.UseVisualStyleBackColor = False
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.BackColor = System.Drawing.Color.Transparent
        Me.RadioButton1.Checked = True
        Me.RadioButton1.Location = New System.Drawing.Point(6, 14)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(40, 19)
        Me.RadioButton1.TabIndex = 0
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "All"
        Me.RadioButton1.UseVisualStyleBackColor = False
        '
        'LBLTRANS
        '
        Me.LBLTRANS.AutoSize = True
        Me.LBLTRANS.BackColor = System.Drawing.Color.Transparent
        Me.LBLTRANS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTRANS.Location = New System.Drawing.Point(28, 137)
        Me.LBLTRANS.Name = "LBLTRANS"
        Me.LBLTRANS.Size = New System.Drawing.Size(60, 15)
        Me.LBLTRANS.TabIndex = 743
        Me.LBLTRANS.Text = "Transport"
        '
        'cmbtrans
        '
        Me.cmbtrans.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbtrans.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbtrans.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbtrans.FormattingEnabled = True
        Me.cmbtrans.Location = New System.Drawing.Point(91, 134)
        Me.cmbtrans.MaxDropDownItems = 14
        Me.cmbtrans.Name = "cmbtrans"
        Me.cmbtrans.Size = New System.Drawing.Size(230, 23)
        Me.cmbtrans.TabIndex = 4
        '
        'CHKSUMMARY
        '
        Me.CHKSUMMARY.AutoSize = True
        Me.CHKSUMMARY.BackColor = System.Drawing.Color.Transparent
        Me.CHKSUMMARY.Location = New System.Drawing.Point(658, 92)
        Me.CHKSUMMARY.Name = "CHKSUMMARY"
        Me.CHKSUMMARY.Size = New System.Drawing.Size(77, 19)
        Me.CHKSUMMARY.TabIndex = 9
        Me.CHKSUMMARY.Text = "Summary"
        Me.CHKSUMMARY.UseVisualStyleBackColor = False
        '
        'CMBCODE
        '
        Me.CMBCODE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBCODE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBCODE.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBCODE.FormattingEnabled = True
        Me.CMBCODE.Items.AddRange(New Object() {"C/R", "O/R"})
        Me.CMBCODE.Location = New System.Drawing.Point(975, 655)
        Me.CMBCODE.Name = "CMBCODE"
        Me.CMBCODE.Size = New System.Drawing.Size(28, 22)
        Me.CMBCODE.TabIndex = 738
        Me.CMBCODE.Visible = False
        '
        'txtDeliveryadd
        '
        Me.txtDeliveryadd.BackColor = System.Drawing.Color.White
        Me.txtDeliveryadd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDeliveryadd.Enabled = False
        Me.txtDeliveryadd.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeliveryadd.Location = New System.Drawing.Point(975, 649)
        Me.txtDeliveryadd.Name = "txtDeliveryadd"
        Me.txtDeliveryadd.ReadOnly = True
        Me.txtDeliveryadd.Size = New System.Drawing.Size(34, 22)
        Me.txtDeliveryadd.TabIndex = 737
        Me.txtDeliveryadd.TabStop = False
        Me.txtDeliveryadd.Visible = False
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.RDBGSTREGISTER)
        Me.GroupBox3.Controls.Add(Me.LBLRATE)
        Me.GroupBox3.Controls.Add(Me.TXTRATE)
        Me.GroupBox3.Controls.Add(Me.RDBREGISTER)
        Me.GroupBox3.Controls.Add(Me.RDBDETAILS)
        Me.GroupBox3.Controls.Add(Me.RDAVGPURQUALITY)
        Me.GroupBox3.Controls.Add(Me.RDAVGPURMONTHLY)
        Me.GroupBox3.Controls.Add(Me.RDBNAME)
        Me.GroupBox3.Controls.Add(Me.CMBSIGN)
        Me.GroupBox3.Controls.Add(Me.LBLAMT)
        Me.GroupBox3.Controls.Add(Me.TXTAMT)
        Me.GroupBox3.Controls.Add(Me.RDBTRANS)
        Me.GroupBox3.Controls.Add(Me.RDBMONTHLY)
        Me.GroupBox3.Controls.Add(Me.RDQUALITY)
        Me.GroupBox3.Controls.Add(Me.RDBAGENT)
        Me.GroupBox3.Controls.Add(Me.RDBPARTY)
        Me.GroupBox3.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(748, 175)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(193, 352)
        Me.GroupBox3.TabIndex = 14
        Me.GroupBox3.TabStop = False
        '
        'RDBGSTREGISTER
        '
        Me.RDBGSTREGISTER.AutoSize = True
        Me.RDBGSTREGISTER.Location = New System.Drawing.Point(26, 187)
        Me.RDBGSTREGISTER.Name = "RDBGSTREGISTER"
        Me.RDBGSTREGISTER.Size = New System.Drawing.Size(131, 18)
        Me.RDBGSTREGISTER.TabIndex = 17
        Me.RDBGSTREGISTER.Text = "Register Wise - GST"
        Me.RDBGSTREGISTER.UseVisualStyleBackColor = True
        '
        'LBLRATE
        '
        Me.LBLRATE.AutoSize = True
        Me.LBLRATE.BackColor = System.Drawing.Color.Transparent
        Me.LBLRATE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLRATE.Location = New System.Drawing.Point(17, 294)
        Me.LBLRATE.Name = "LBLRATE"
        Me.LBLRATE.Size = New System.Drawing.Size(31, 15)
        Me.LBLRATE.TabIndex = 16
        Me.LBLRATE.Text = "Rate"
        '
        'TXTRATE
        '
        Me.TXTRATE.Location = New System.Drawing.Point(50, 290)
        Me.TXTRATE.Name = "TXTRATE"
        Me.TXTRATE.Size = New System.Drawing.Size(71, 22)
        Me.TXTRATE.TabIndex = 10
        Me.TXTRATE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'RDBREGISTER
        '
        Me.RDBREGISTER.AutoSize = True
        Me.RDBREGISTER.Location = New System.Drawing.Point(26, 164)
        Me.RDBREGISTER.Name = "RDBREGISTER"
        Me.RDBREGISTER.Size = New System.Drawing.Size(101, 18)
        Me.RDBREGISTER.TabIndex = 6
        Me.RDBREGISTER.Text = "Register Wise"
        Me.RDBREGISTER.UseVisualStyleBackColor = True
        '
        'RDBDETAILS
        '
        Me.RDBDETAILS.AutoSize = True
        Me.RDBDETAILS.Checked = True
        Me.RDBDETAILS.Location = New System.Drawing.Point(26, 26)
        Me.RDBDETAILS.Name = "RDBDETAILS"
        Me.RDBDETAILS.Size = New System.Drawing.Size(69, 18)
        Me.RDBDETAILS.TabIndex = 0
        Me.RDBDETAILS.TabStop = True
        Me.RDBDETAILS.Text = "All Data"
        Me.RDBDETAILS.UseVisualStyleBackColor = True
        '
        'RDAVGPURQUALITY
        '
        Me.RDAVGPURQUALITY.AutoSize = True
        Me.RDAVGPURQUALITY.Location = New System.Drawing.Point(26, 233)
        Me.RDAVGPURQUALITY.Name = "RDAVGPURQUALITY"
        Me.RDAVGPURQUALITY.Size = New System.Drawing.Size(136, 18)
        Me.RDAVGPURQUALITY.TabIndex = 8
        Me.RDAVGPURQUALITY.Text = "Avg Pur Quality Wise"
        Me.RDAVGPURQUALITY.UseVisualStyleBackColor = True
        '
        'RDAVGPURMONTHLY
        '
        Me.RDAVGPURMONTHLY.AutoSize = True
        Me.RDAVGPURMONTHLY.Location = New System.Drawing.Point(26, 256)
        Me.RDAVGPURMONTHLY.Name = "RDAVGPURMONTHLY"
        Me.RDAVGPURMONTHLY.Size = New System.Drawing.Size(142, 18)
        Me.RDAVGPURMONTHLY.TabIndex = 9
        Me.RDAVGPURMONTHLY.Text = "Avg Purchase Monthly"
        Me.RDAVGPURMONTHLY.UseVisualStyleBackColor = True
        '
        'RDBNAME
        '
        Me.RDBNAME.AutoSize = True
        Me.RDBNAME.Location = New System.Drawing.Point(26, 118)
        Me.RDBNAME.Name = "RDBNAME"
        Me.RDBNAME.Size = New System.Drawing.Size(78, 18)
        Me.RDBNAME.TabIndex = 4
        Me.RDBNAME.Text = "Mill Wise"
        Me.RDBNAME.UseVisualStyleBackColor = True
        '
        'CMBSIGN
        '
        Me.CMBSIGN.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBSIGN.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBSIGN.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBSIGN.FormattingEnabled = True
        Me.CMBSIGN.Items.AddRange(New Object() {"", "=", "<", ">", ">=", "<=", "<>"})
        Me.CMBSIGN.Location = New System.Drawing.Point(123, 318)
        Me.CMBSIGN.MaxDropDownItems = 14
        Me.CMBSIGN.Name = "CMBSIGN"
        Me.CMBSIGN.Size = New System.Drawing.Size(52, 23)
        Me.CMBSIGN.TabIndex = 12
        '
        'LBLAMT
        '
        Me.LBLAMT.AutoSize = True
        Me.LBLAMT.BackColor = System.Drawing.Color.Transparent
        Me.LBLAMT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLAMT.Location = New System.Drawing.Point(17, 322)
        Me.LBLAMT.Name = "LBLAMT"
        Me.LBLAMT.Size = New System.Drawing.Size(31, 15)
        Me.LBLAMT.TabIndex = 8
        Me.LBLAMT.Text = "Amt."
        '
        'TXTAMT
        '
        Me.TXTAMT.Location = New System.Drawing.Point(50, 318)
        Me.TXTAMT.Name = "TXTAMT"
        Me.TXTAMT.Size = New System.Drawing.Size(71, 22)
        Me.TXTAMT.TabIndex = 11
        '
        'RDBTRANS
        '
        Me.RDBTRANS.AutoSize = True
        Me.RDBTRANS.Location = New System.Drawing.Point(26, 141)
        Me.RDBTRANS.Name = "RDBTRANS"
        Me.RDBTRANS.Size = New System.Drawing.Size(107, 18)
        Me.RDBTRANS.TabIndex = 5
        Me.RDBTRANS.Text = "Transport Wise"
        Me.RDBTRANS.UseVisualStyleBackColor = True
        '
        'RDBMONTHLY
        '
        Me.RDBMONTHLY.AutoSize = True
        Me.RDBMONTHLY.Location = New System.Drawing.Point(26, 210)
        Me.RDBMONTHLY.Name = "RDBMONTHLY"
        Me.RDBMONTHLY.Size = New System.Drawing.Size(69, 18)
        Me.RDBMONTHLY.TabIndex = 7
        Me.RDBMONTHLY.Text = "Monthly"
        Me.RDBMONTHLY.UseVisualStyleBackColor = True
        '
        'RDQUALITY
        '
        Me.RDQUALITY.AutoSize = True
        Me.RDQUALITY.Location = New System.Drawing.Point(26, 95)
        Me.RDQUALITY.Name = "RDQUALITY"
        Me.RDQUALITY.Size = New System.Drawing.Size(95, 18)
        Me.RDQUALITY.TabIndex = 3
        Me.RDQUALITY.Text = "Quality Wise"
        Me.RDQUALITY.UseVisualStyleBackColor = True
        '
        'RDBAGENT
        '
        Me.RDBAGENT.AutoSize = True
        Me.RDBAGENT.Location = New System.Drawing.Point(26, 72)
        Me.RDBAGENT.Name = "RDBAGENT"
        Me.RDBAGENT.Size = New System.Drawing.Size(87, 18)
        Me.RDBAGENT.TabIndex = 2
        Me.RDBAGENT.Text = "Agent Wise"
        Me.RDBAGENT.UseVisualStyleBackColor = True
        '
        'RDBPARTY
        '
        Me.RDBPARTY.AutoSize = True
        Me.RDBPARTY.Location = New System.Drawing.Point(26, 49)
        Me.RDBPARTY.Name = "RDBPARTY"
        Me.RDBPARTY.Size = New System.Drawing.Size(82, 18)
        Me.RDBPARTY.TabIndex = 1
        Me.RDBPARTY.Text = "Party Wise"
        Me.RDBPARTY.UseVisualStyleBackColor = True
        '
        'LBLAGENT
        '
        Me.LBLAGENT.AutoSize = True
        Me.LBLAGENT.BackColor = System.Drawing.Color.Transparent
        Me.LBLAGENT.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLAGENT.ForeColor = System.Drawing.Color.Black
        Me.LBLAGENT.Location = New System.Drawing.Point(51, 53)
        Me.LBLAGENT.Name = "LBLAGENT"
        Me.LBLAGENT.Size = New System.Drawing.Size(38, 14)
        Me.LBLAGENT.TabIndex = 652
        Me.LBLAGENT.Text = "Agent"
        '
        'CMBAGENT
        '
        Me.CMBAGENT.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBAGENT.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBAGENT.BackColor = System.Drawing.Color.White
        Me.CMBAGENT.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBAGENT.FormattingEnabled = True
        Me.CMBAGENT.Location = New System.Drawing.Point(91, 49)
        Me.CMBAGENT.MaxDropDownItems = 14
        Me.CMBAGENT.Name = "CMBAGENT"
        Me.CMBAGENT.Size = New System.Drawing.Size(230, 22)
        Me.CMBAGENT.TabIndex = 1
        '
        'chkdate
        '
        Me.chkdate.AutoSize = True
        Me.chkdate.BackColor = System.Drawing.Color.Transparent
        Me.chkdate.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkdate.ForeColor = System.Drawing.Color.Black
        Me.chkdate.Location = New System.Drawing.Point(661, 22)
        Me.chkdate.Name = "chkdate"
        Me.chkdate.Size = New System.Drawing.Size(52, 18)
        Me.chkdate.TabIndex = 7
        Me.chkdate.Text = "Date"
        Me.chkdate.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.dtto)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.dtfrom)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(653, 27)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(280, 53)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        '
        'dtto
        '
        Me.dtto.Font = New System.Drawing.Font("Calibri", 9.0!)
        Me.dtto.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtto.Location = New System.Drawing.Point(189, 20)
        Me.dtto.Name = "dtto"
        Me.dtto.Size = New System.Drawing.Size(83, 22)
        Me.dtto.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.0!)
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(161, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(25, 14)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "To :"
        '
        'dtfrom
        '
        Me.dtfrom.Font = New System.Drawing.Font("Calibri", 9.0!)
        Me.dtfrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtfrom.Location = New System.Drawing.Point(50, 20)
        Me.dtfrom.Name = "dtfrom"
        Me.dtfrom.Size = New System.Drawing.Size(83, 22)
        Me.dtfrom.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Calibri", 9.0!)
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(9, 24)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(40, 14)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "From :"
        '
        'CMBQUALITY
        '
        Me.CMBQUALITY.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBQUALITY.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBQUALITY.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBQUALITY.FormattingEnabled = True
        Me.CMBQUALITY.Location = New System.Drawing.Point(91, 77)
        Me.CMBQUALITY.MaxDropDownItems = 14
        Me.CMBQUALITY.Name = "CMBQUALITY"
        Me.CMBQUALITY.Size = New System.Drawing.Size(230, 22)
        Me.CMBQUALITY.TabIndex = 2
        '
        'LBLQUALITY
        '
        Me.LBLQUALITY.BackColor = System.Drawing.Color.Transparent
        Me.LBLQUALITY.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLQUALITY.ForeColor = System.Drawing.Color.Black
        Me.LBLQUALITY.Location = New System.Drawing.Point(0, 81)
        Me.LBLQUALITY.Name = "LBLQUALITY"
        Me.LBLQUALITY.Size = New System.Drawing.Size(89, 14)
        Me.LBLQUALITY.TabIndex = 439
        Me.LBLQUALITY.Text = "Quality"
        Me.LBLQUALITY.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CMBNAME
        '
        Me.CMBNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBNAME.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBNAME.FormattingEnabled = True
        Me.CMBNAME.Location = New System.Drawing.Point(91, 21)
        Me.CMBNAME.MaxDropDownItems = 14
        Me.CMBNAME.Name = "CMBNAME"
        Me.CMBNAME.Size = New System.Drawing.Size(230, 22)
        Me.CMBNAME.TabIndex = 0
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(50, 25)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(39, 14)
        Me.Label9.TabIndex = 419
        Me.Label9.Text = "Name"
        '
        'cmdshow
        '
        Me.cmdshow.BackColor = System.Drawing.Color.Transparent
        Me.cmdshow.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdshow.FlatAppearance.BorderSize = 0
        Me.cmdshow.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdshow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmdshow.Location = New System.Drawing.Point(759, 533)
        Me.cmdshow.Name = "cmdshow"
        Me.cmdshow.Size = New System.Drawing.Size(88, 28)
        Me.cmdshow.TabIndex = 16
        Me.cmdshow.Text = "&Show Details"
        Me.cmdshow.UseVisualStyleBackColor = False
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdexit.FlatAppearance.BorderSize = 0
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmdexit.Location = New System.Drawing.Point(853, 533)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(88, 28)
        Me.cmdexit.TabIndex = 17
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'PurchaseInvoiceFilter
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1037, 723)
        Me.Controls.Add(Me.BlendPanel2)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "PurchaseInvoiceFilter"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Purchase Invoice Filter"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel2.ResumeLayout(False)
        Me.BlendPanel2.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.GRIDBILLDETAILSR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRIDBILLR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BlendPanel2 As VbPowerPack.BlendPanel
    Friend WithEvents CHKPRINTDATE As System.Windows.Forms.CheckBox
    Friend WithEvents cmbregister As System.Windows.Forms.ComboBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents CHKADDRESS As System.Windows.Forms.CheckBox
    Friend WithEvents CHKHEADER As System.Windows.Forms.CheckBox
    Friend WithEvents CHKGROUPONNEWPG As System.Windows.Forms.CheckBox
    Friend WithEvents LBLNAME As System.Windows.Forms.Label
    Friend WithEvents CMBMILL As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents CHKSELECTALL As System.Windows.Forms.CheckBox
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Private WithEvents GNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents RBSELECTED As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents LBLTRANS As System.Windows.Forms.Label
    Friend WithEvents cmbtrans As System.Windows.Forms.ComboBox
    Friend WithEvents CHKSUMMARY As System.Windows.Forms.CheckBox
    Friend WithEvents CMBCODE As System.Windows.Forms.ComboBox
    Friend WithEvents txtDeliveryadd As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents LBLRATE As System.Windows.Forms.Label
    Friend WithEvents TXTRATE As System.Windows.Forms.TextBox
    Friend WithEvents RDBREGISTER As System.Windows.Forms.RadioButton
    Friend WithEvents RDBDETAILS As System.Windows.Forms.RadioButton
    Friend WithEvents RDAVGPURQUALITY As System.Windows.Forms.RadioButton
    Friend WithEvents RDAVGPURMONTHLY As System.Windows.Forms.RadioButton
    Friend WithEvents RDBNAME As System.Windows.Forms.RadioButton
    Friend WithEvents CMBSIGN As System.Windows.Forms.ComboBox
    Friend WithEvents LBLAMT As System.Windows.Forms.Label
    Friend WithEvents TXTAMT As System.Windows.Forms.TextBox
    Friend WithEvents RDBTRANS As System.Windows.Forms.RadioButton
    Friend WithEvents RDBMONTHLY As System.Windows.Forms.RadioButton
    Friend WithEvents RDQUALITY As System.Windows.Forms.RadioButton
    Friend WithEvents RDBAGENT As System.Windows.Forms.RadioButton
    Friend WithEvents RDBPARTY As System.Windows.Forms.RadioButton
    Friend WithEvents LBLAGENT As System.Windows.Forms.Label
    Friend WithEvents CMBAGENT As System.Windows.Forms.ComboBox
    Friend WithEvents chkdate As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dtto As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtfrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents CMBQUALITY As System.Windows.Forms.ComboBox
    Friend WithEvents LBLQUALITY As System.Windows.Forms.Label
    Friend WithEvents CMBNAME As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmdshow As System.Windows.Forms.Button
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents CMBEFFECTQUALITY As System.Windows.Forms.ComboBox
    Friend WithEvents LBLEFFECTQTY As System.Windows.Forms.Label
    Private WithEvents GCHK As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Private WithEvents GUNDER As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GCITY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtadd As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents CHKRSELECTALL As System.Windows.Forms.CheckBox
    Private WithEvents GRIDBILLDETAILSR As DevExpress.XtraGrid.GridControl
    Private WithEvents GRIDBILLR As DevExpress.XtraGrid.Views.Grid.GridView
    Private WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents RDBRSELECTED As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton3 As System.Windows.Forms.RadioButton
    Friend WithEvents CMBTYPE As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents RDBGSTREGISTER As System.Windows.Forms.RadioButton
End Class
