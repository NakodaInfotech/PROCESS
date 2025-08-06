<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OutstandingFilter
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
        Me.CMBBROKERNAME = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.CHKSELECTALL = New System.Windows.Forms.CheckBox
        Me.gridbilldetails = New DevExpress.XtraGrid.GridControl
        Me.gridbill = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.GCHK = New DevExpress.XtraGrid.Columns.GridColumn
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
        Me.GNAME = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GUNDER = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GCITY = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.RBSELECTED = New System.Windows.Forms.RadioButton
        Me.RadioButton1 = New System.Windows.Forms.RadioButton
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.RBGROUP = New System.Windows.Forms.RadioButton
        Me.RBACCOUNT = New System.Windows.Forms.RadioButton
        Me.CHKPRINTDATE = New System.Windows.Forms.CheckBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.TXTDAYS = New System.Windows.Forms.TextBox
        Me.TXTINTEREST = New System.Windows.Forms.TextBox
        Me.CMBGROUP = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.CHKGROUPONNEWPG = New System.Windows.Forms.CheckBox
        Me.CHKADDRESS = New System.Windows.Forms.CheckBox
        Me.CHKSUMMARY = New System.Windows.Forms.CheckBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.RBDUEDATE = New System.Windows.Forms.RadioButton
        Me.RBBILLDATE = New System.Windows.Forms.RadioButton
        Me.RBPAYABLE = New System.Windows.Forms.RadioButton
        Me.RBREC = New System.Windows.Forms.RadioButton
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.RBBROKEROUTSTANDING = New System.Windows.Forms.RadioButton
        Me.RBREMINDER = New System.Windows.Forms.RadioButton
        Me.RBOUTSTANDINGBILLS = New System.Windows.Forms.RadioButton
        Me.RBALLBILLS = New System.Windows.Forms.RadioButton
        Me.RBOUTSTANDING = New System.Windows.Forms.RadioButton
        Me.RBINVENTORY = New System.Windows.Forms.RadioButton
        Me.RBINT = New System.Windows.Forms.RadioButton
        Me.RBALL = New System.Windows.Forms.RadioButton
        Me.cmbacccode = New System.Windows.Forms.ComboBox
        Me.txtadd = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.dtto = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.dtfrom = New System.Windows.Forms.DateTimePicker
        Me.Label7 = New System.Windows.Forms.Label
        Me.chkdate = New System.Windows.Forms.CheckBox
        Me.CMBPARTYNAME = New System.Windows.Forms.ComboBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.cmdshow = New System.Windows.Forms.Button
        Me.cmdexit = New System.Windows.Forms.Button
        Me.RBONLYPAYMENT = New System.Windows.Forms.RadioButton
        Me.Label6 = New System.Windows.Forms.Label
        Me.TXTDUDAYS = New System.Windows.Forms.TextBox
        Me.BlendPanel2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BlendPanel2
        '
        Me.BlendPanel2.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel2.Controls.Add(Me.Label6)
        Me.BlendPanel2.Controls.Add(Me.TXTDUDAYS)
        Me.BlendPanel2.Controls.Add(Me.CMBBROKERNAME)
        Me.BlendPanel2.Controls.Add(Me.Label5)
        Me.BlendPanel2.Controls.Add(Me.GroupBox4)
        Me.BlendPanel2.Controls.Add(Me.CHKPRINTDATE)
        Me.BlendPanel2.Controls.Add(Me.Label4)
        Me.BlendPanel2.Controls.Add(Me.Label3)
        Me.BlendPanel2.Controls.Add(Me.TXTDAYS)
        Me.BlendPanel2.Controls.Add(Me.TXTINTEREST)
        Me.BlendPanel2.Controls.Add(Me.CMBGROUP)
        Me.BlendPanel2.Controls.Add(Me.Label2)
        Me.BlendPanel2.Controls.Add(Me.CHKGROUPONNEWPG)
        Me.BlendPanel2.Controls.Add(Me.CHKADDRESS)
        Me.BlendPanel2.Controls.Add(Me.CHKSUMMARY)
        Me.BlendPanel2.Controls.Add(Me.GroupBox2)
        Me.BlendPanel2.Controls.Add(Me.RBPAYABLE)
        Me.BlendPanel2.Controls.Add(Me.RBREC)
        Me.BlendPanel2.Controls.Add(Me.GroupBox3)
        Me.BlendPanel2.Controls.Add(Me.cmbacccode)
        Me.BlendPanel2.Controls.Add(Me.txtadd)
        Me.BlendPanel2.Controls.Add(Me.GroupBox1)
        Me.BlendPanel2.Controls.Add(Me.CMBPARTYNAME)
        Me.BlendPanel2.Controls.Add(Me.Label9)
        Me.BlendPanel2.Controls.Add(Me.cmdshow)
        Me.BlendPanel2.Controls.Add(Me.cmdexit)
        Me.BlendPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel2.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel2.Name = "BlendPanel2"
        Me.BlendPanel2.Size = New System.Drawing.Size(905, 586)
        Me.BlendPanel2.TabIndex = 0
        '
        'CMBBROKERNAME
        '
        Me.CMBBROKERNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBBROKERNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBBROKERNAME.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBBROKERNAME.FormattingEnabled = True
        Me.CMBBROKERNAME.Location = New System.Drawing.Point(136, 65)
        Me.CMBBROKERNAME.MaxDropDownItems = 14
        Me.CMBBROKERNAME.Name = "CMBBROKERNAME"
        Me.CMBBROKERNAME.Size = New System.Drawing.Size(260, 22)
        Me.CMBBROKERNAME.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(57, 69)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 14)
        Me.Label5.TabIndex = 669
        Me.Label5.Text = "Broker Name"
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox4.Controls.Add(Me.CHKSELECTALL)
        Me.GroupBox4.Controls.Add(Me.gridbilldetails)
        Me.GroupBox4.Controls.Add(Me.GroupBox5)
        Me.GroupBox4.Controls.Add(Me.GroupBox6)
        Me.GroupBox4.Location = New System.Drawing.Point(31, 198)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(692, 359)
        Me.GroupBox4.TabIndex = 11
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
        Me.CHKSELECTALL.TabIndex = 2
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
        Me.GCHK.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.GCHK.FieldName = "CHK"
        Me.GCHK.Name = "GCHK"
        Me.GCHK.OptionsColumn.ShowCaption = False
        Me.GCHK.Visible = True
        Me.GCHK.VisibleIndex = 0
        Me.GCHK.Width = 50
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
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
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox5.Controls.Add(Me.RBSELECTED)
        Me.GroupBox5.Controls.Add(Me.RadioButton1)
        Me.GroupBox5.Location = New System.Drawing.Point(350, 4)
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
        'GroupBox6
        '
        Me.GroupBox6.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox6.Controls.Add(Me.RBGROUP)
        Me.GroupBox6.Controls.Add(Me.RBACCOUNT)
        Me.GroupBox6.Location = New System.Drawing.Point(186, 4)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(157, 38)
        Me.GroupBox6.TabIndex = 0
        Me.GroupBox6.TabStop = False
        '
        'RBGROUP
        '
        Me.RBGROUP.AutoSize = True
        Me.RBGROUP.BackColor = System.Drawing.Color.Transparent
        Me.RBGROUP.Location = New System.Drawing.Point(88, 14)
        Me.RBGROUP.Name = "RBGROUP"
        Me.RBGROUP.Size = New System.Drawing.Size(59, 19)
        Me.RBGROUP.TabIndex = 1
        Me.RBGROUP.Text = "Group"
        Me.RBGROUP.UseVisualStyleBackColor = False
        '
        'RBACCOUNT
        '
        Me.RBACCOUNT.AutoSize = True
        Me.RBACCOUNT.BackColor = System.Drawing.Color.Transparent
        Me.RBACCOUNT.Checked = True
        Me.RBACCOUNT.Location = New System.Drawing.Point(6, 14)
        Me.RBACCOUNT.Name = "RBACCOUNT"
        Me.RBACCOUNT.Size = New System.Drawing.Size(69, 19)
        Me.RBACCOUNT.TabIndex = 0
        Me.RBACCOUNT.TabStop = True
        Me.RBACCOUNT.Text = "Account"
        Me.RBACCOUNT.UseVisualStyleBackColor = False
        '
        'CHKPRINTDATE
        '
        Me.CHKPRINTDATE.AutoSize = True
        Me.CHKPRINTDATE.BackColor = System.Drawing.Color.Transparent
        Me.CHKPRINTDATE.Checked = True
        Me.CHKPRINTDATE.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CHKPRINTDATE.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHKPRINTDATE.ForeColor = System.Drawing.Color.Black
        Me.CHKPRINTDATE.Location = New System.Drawing.Point(163, 173)
        Me.CHKPRINTDATE.Name = "CHKPRINTDATE"
        Me.CHKPRINTDATE.Size = New System.Drawing.Size(112, 18)
        Me.CHKPRINTDATE.TabIndex = 8
        Me.CHKPRINTDATE.Text = "Show Print Date"
        Me.CHKPRINTDATE.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(748, 270)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(33, 14)
        Me.Label4.TabIndex = 667
        Me.Label4.Text = "Days"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(731, 240)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 14)
        Me.Label3.TabIndex = 666
        Me.Label3.Text = "Interest"
        '
        'TXTDAYS
        '
        Me.TXTDAYS.Location = New System.Drawing.Point(784, 266)
        Me.TXTDAYS.Name = "TXTDAYS"
        Me.TXTDAYS.Size = New System.Drawing.Size(31, 23)
        Me.TXTDAYS.TabIndex = 13
        Me.TXTDAYS.Text = "365"
        Me.TXTDAYS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTINTEREST
        '
        Me.TXTINTEREST.Location = New System.Drawing.Point(784, 236)
        Me.TXTINTEREST.Name = "TXTINTEREST"
        Me.TXTINTEREST.Size = New System.Drawing.Size(31, 23)
        Me.TXTINTEREST.TabIndex = 12
        Me.TXTINTEREST.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CMBGROUP
        '
        Me.CMBGROUP.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBGROUP.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBGROUP.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBGROUP.FormattingEnabled = True
        Me.CMBGROUP.Location = New System.Drawing.Point(136, 93)
        Me.CMBGROUP.MaxDropDownItems = 14
        Me.CMBGROUP.Name = "CMBGROUP"
        Me.CMBGROUP.Size = New System.Drawing.Size(260, 22)
        Me.CMBGROUP.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(94, 97)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 14)
        Me.Label2.TabIndex = 663
        Me.Label2.Text = "Group"
        '
        'CHKGROUPONNEWPG
        '
        Me.CHKGROUPONNEWPG.AutoSize = True
        Me.CHKGROUPONNEWPG.BackColor = System.Drawing.Color.Transparent
        Me.CHKGROUPONNEWPG.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHKGROUPONNEWPG.ForeColor = System.Drawing.Color.Black
        Me.CHKGROUPONNEWPG.Location = New System.Drawing.Point(85, 125)
        Me.CHKGROUPONNEWPG.Name = "CHKGROUPONNEWPG"
        Me.CHKGROUPONNEWPG.Size = New System.Drawing.Size(193, 18)
        Me.CHKGROUPONNEWPG.TabIndex = 5
        Me.CHKGROUPONNEWPG.Text = "Show Each Group On New Page"
        Me.CHKGROUPONNEWPG.UseVisualStyleBackColor = False
        '
        'CHKADDRESS
        '
        Me.CHKADDRESS.AutoSize = True
        Me.CHKADDRESS.BackColor = System.Drawing.Color.Transparent
        Me.CHKADDRESS.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHKADDRESS.ForeColor = System.Drawing.Color.Black
        Me.CHKADDRESS.Location = New System.Drawing.Point(85, 149)
        Me.CHKADDRESS.Name = "CHKADDRESS"
        Me.CHKADDRESS.Size = New System.Drawing.Size(102, 18)
        Me.CHKADDRESS.TabIndex = 6
        Me.CHKADDRESS.Text = "Show Address"
        Me.CHKADDRESS.UseVisualStyleBackColor = False
        '
        'CHKSUMMARY
        '
        Me.CHKSUMMARY.AutoSize = True
        Me.CHKSUMMARY.BackColor = System.Drawing.Color.Transparent
        Me.CHKSUMMARY.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHKSUMMARY.ForeColor = System.Drawing.Color.Black
        Me.CHKSUMMARY.Location = New System.Drawing.Point(85, 173)
        Me.CHKSUMMARY.Name = "CHKSUMMARY"
        Me.CHKSUMMARY.Size = New System.Drawing.Size(75, 18)
        Me.CHKSUMMARY.TabIndex = 7
        Me.CHKSUMMARY.Text = "Summary"
        Me.CHKSUMMARY.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.RBDUEDATE)
        Me.GroupBox2.Controls.Add(Me.RBBILLDATE)
        Me.GroupBox2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(309, 120)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(96, 71)
        Me.GroupBox2.TabIndex = 9
        Me.GroupBox2.TabStop = False
        '
        'RBDUEDATE
        '
        Me.RBDUEDATE.AutoSize = True
        Me.RBDUEDATE.Checked = True
        Me.RBDUEDATE.Location = New System.Drawing.Point(11, 43)
        Me.RBDUEDATE.Name = "RBDUEDATE"
        Me.RBDUEDATE.Size = New System.Drawing.Size(76, 18)
        Me.RBDUEDATE.TabIndex = 1
        Me.RBDUEDATE.TabStop = True
        Me.RBDUEDATE.Text = "Due Date"
        Me.RBDUEDATE.UseVisualStyleBackColor = True
        '
        'RBBILLDATE
        '
        Me.RBBILLDATE.AutoSize = True
        Me.RBBILLDATE.Location = New System.Drawing.Point(11, 19)
        Me.RBBILLDATE.Name = "RBBILLDATE"
        Me.RBBILLDATE.Size = New System.Drawing.Size(73, 18)
        Me.RBBILLDATE.TabIndex = 0
        Me.RBBILLDATE.Text = "Bill Date"
        Me.RBBILLDATE.UseVisualStyleBackColor = True
        '
        'RBPAYABLE
        '
        Me.RBPAYABLE.AutoSize = True
        Me.RBPAYABLE.BackColor = System.Drawing.Color.Transparent
        Me.RBPAYABLE.Location = New System.Drawing.Point(255, 13)
        Me.RBPAYABLE.Name = "RBPAYABLE"
        Me.RBPAYABLE.Size = New System.Drawing.Size(75, 19)
        Me.RBPAYABLE.TabIndex = 1
        Me.RBPAYABLE.Text = "Payables"
        Me.RBPAYABLE.UseVisualStyleBackColor = False
        '
        'RBREC
        '
        Me.RBREC.AutoSize = True
        Me.RBREC.BackColor = System.Drawing.Color.Transparent
        Me.RBREC.Checked = True
        Me.RBREC.Location = New System.Drawing.Point(161, 13)
        Me.RBREC.Name = "RBREC"
        Me.RBREC.Size = New System.Drawing.Size(90, 19)
        Me.RBREC.TabIndex = 0
        Me.RBREC.TabStop = True
        Me.RBREC.Text = "Receivables"
        Me.RBREC.UseVisualStyleBackColor = False
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.RBONLYPAYMENT)
        Me.GroupBox3.Controls.Add(Me.RBBROKEROUTSTANDING)
        Me.GroupBox3.Controls.Add(Me.RBREMINDER)
        Me.GroupBox3.Controls.Add(Me.RBOUTSTANDINGBILLS)
        Me.GroupBox3.Controls.Add(Me.RBALLBILLS)
        Me.GroupBox3.Controls.Add(Me.RBOUTSTANDING)
        Me.GroupBox3.Controls.Add(Me.RBINVENTORY)
        Me.GroupBox3.Controls.Add(Me.RBINT)
        Me.GroupBox3.Controls.Add(Me.RBALL)
        Me.GroupBox3.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(411, 37)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(409, 158)
        Me.GroupBox3.TabIndex = 10
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Reports"
        '
        'RBBROKEROUTSTANDING
        '
        Me.RBBROKEROUTSTANDING.AutoSize = True
        Me.RBBROKEROUTSTANDING.Location = New System.Drawing.Point(229, 97)
        Me.RBBROKEROUTSTANDING.Name = "RBBROKEROUTSTANDING"
        Me.RBBROKEROUTSTANDING.Size = New System.Drawing.Size(161, 18)
        Me.RBBROKEROUTSTANDING.TabIndex = 7
        Me.RBBROKEROUTSTANDING.Text = "Broker Wise Outstanding"
        Me.RBBROKEROUTSTANDING.UseVisualStyleBackColor = True
        '
        'RBREMINDER
        '
        Me.RBREMINDER.AutoSize = True
        Me.RBREMINDER.Location = New System.Drawing.Point(229, 73)
        Me.RBREMINDER.Name = "RBREMINDER"
        Me.RBREMINDER.Size = New System.Drawing.Size(112, 18)
        Me.RBREMINDER.TabIndex = 6
        Me.RBREMINDER.Text = "Reminder Letter"
        Me.RBREMINDER.UseVisualStyleBackColor = True
        '
        'RBOUTSTANDINGBILLS
        '
        Me.RBOUTSTANDINGBILLS.AutoSize = True
        Me.RBOUTSTANDINGBILLS.Location = New System.Drawing.Point(229, 49)
        Me.RBOUTSTANDINGBILLS.Name = "RBOUTSTANDINGBILLS"
        Me.RBOUTSTANDINGBILLS.Size = New System.Drawing.Size(147, 18)
        Me.RBOUTSTANDINGBILLS.TabIndex = 5
        Me.RBOUTSTANDINGBILLS.Text = "Only Outstanding Bills"
        Me.RBOUTSTANDINGBILLS.UseVisualStyleBackColor = True
        '
        'RBALLBILLS
        '
        Me.RBALLBILLS.AutoSize = True
        Me.RBALLBILLS.Location = New System.Drawing.Point(229, 25)
        Me.RBALLBILLS.Name = "RBALLBILLS"
        Me.RBALLBILLS.Size = New System.Drawing.Size(68, 18)
        Me.RBALLBILLS.TabIndex = 4
        Me.RBALLBILLS.Text = "All Bills"
        Me.RBALLBILLS.UseVisualStyleBackColor = True
        '
        'RBOUTSTANDING
        '
        Me.RBOUTSTANDING.AutoSize = True
        Me.RBOUTSTANDING.Location = New System.Drawing.Point(26, 49)
        Me.RBOUTSTANDING.Name = "RBOUTSTANDING"
        Me.RBOUTSTANDING.Size = New System.Drawing.Size(119, 18)
        Me.RBOUTSTANDING.TabIndex = 1
        Me.RBOUTSTANDING.Text = "Only Outstanding"
        Me.RBOUTSTANDING.UseVisualStyleBackColor = True
        '
        'RBINVENTORY
        '
        Me.RBINVENTORY.AutoSize = True
        Me.RBINVENTORY.Location = New System.Drawing.Point(26, 73)
        Me.RBINVENTORY.Name = "RBINVENTORY"
        Me.RBINVENTORY.Size = New System.Drawing.Size(174, 18)
        Me.RBINVENTORY.TabIndex = 2
        Me.RBINVENTORY.Text = "Outstanding With Inventory"
        Me.RBINVENTORY.UseVisualStyleBackColor = True
        '
        'RBINT
        '
        Me.RBINT.AutoSize = True
        Me.RBINT.Location = New System.Drawing.Point(26, 97)
        Me.RBINT.Name = "RBINT"
        Me.RBINT.Size = New System.Drawing.Size(160, 18)
        Me.RBINT.TabIndex = 3
        Me.RBINT.Text = "Outstanding With Intrest"
        Me.RBINT.UseVisualStyleBackColor = True
        '
        'RBALL
        '
        Me.RBALL.AutoSize = True
        Me.RBALL.Checked = True
        Me.RBALL.Location = New System.Drawing.Point(26, 25)
        Me.RBALL.Name = "RBALL"
        Me.RBALL.Size = New System.Drawing.Size(111, 18)
        Me.RBALL.TabIndex = 0
        Me.RBALL.TabStop = True
        Me.RBALL.Text = "All Transactions"
        Me.RBALL.UseVisualStyleBackColor = True
        '
        'cmbacccode
        '
        Me.cmbacccode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbacccode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbacccode.BackColor = System.Drawing.Color.White
        Me.cmbacccode.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbacccode.FormattingEnabled = True
        Me.cmbacccode.Location = New System.Drawing.Point(31, 153)
        Me.cmbacccode.MaxDropDownItems = 14
        Me.cmbacccode.Name = "cmbacccode"
        Me.cmbacccode.Size = New System.Drawing.Size(30, 22)
        Me.cmbacccode.TabIndex = 650
        Me.cmbacccode.Visible = False
        '
        'txtadd
        '
        Me.txtadd.Location = New System.Drawing.Point(31, 153)
        Me.txtadd.Name = "txtadd"
        Me.txtadd.Size = New System.Drawing.Size(30, 23)
        Me.txtadd.TabIndex = 649
        Me.txtadd.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.dtto)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.dtfrom)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.chkdate)
        Me.GroupBox1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(736, 304)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(148, 86)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.TabStop = False
        '
        'dtto
        '
        Me.dtto.Font = New System.Drawing.Font("Calibri", 9.0!)
        Me.dtto.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtto.Location = New System.Drawing.Point(50, 48)
        Me.dtto.Name = "dtto"
        Me.dtto.Size = New System.Drawing.Size(83, 22)
        Me.dtto.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.0!)
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(23, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(25, 14)
        Me.Label1.TabIndex = 106
        Me.Label1.Text = "To :"
        '
        'dtfrom
        '
        Me.dtfrom.Font = New System.Drawing.Font("Calibri", 9.0!)
        Me.dtfrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtfrom.Location = New System.Drawing.Point(50, 20)
        Me.dtfrom.Name = "dtfrom"
        Me.dtfrom.Size = New System.Drawing.Size(83, 22)
        Me.dtfrom.TabIndex = 1
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
        Me.Label7.TabIndex = 104
        Me.Label7.Text = "From :"
        '
        'chkdate
        '
        Me.chkdate.AutoSize = True
        Me.chkdate.BackColor = System.Drawing.Color.Transparent
        Me.chkdate.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkdate.ForeColor = System.Drawing.Color.Black
        Me.chkdate.Location = New System.Drawing.Point(6, 0)
        Me.chkdate.Name = "chkdate"
        Me.chkdate.Size = New System.Drawing.Size(52, 18)
        Me.chkdate.TabIndex = 0
        Me.chkdate.Text = "Date"
        Me.chkdate.UseVisualStyleBackColor = False
        '
        'CMBPARTYNAME
        '
        Me.CMBPARTYNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBPARTYNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBPARTYNAME.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBPARTYNAME.FormattingEnabled = True
        Me.CMBPARTYNAME.Location = New System.Drawing.Point(136, 37)
        Me.CMBPARTYNAME.MaxDropDownItems = 14
        Me.CMBPARTYNAME.Name = "CMBPARTYNAME"
        Me.CMBPARTYNAME.Size = New System.Drawing.Size(260, 22)
        Me.CMBPARTYNAME.TabIndex = 2
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(95, 41)
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
        Me.cmdshow.ForeColor = System.Drawing.Color.Black
        Me.cmdshow.Location = New System.Drawing.Point(736, 491)
        Me.cmdshow.Name = "cmdshow"
        Me.cmdshow.Size = New System.Drawing.Size(85, 28)
        Me.cmdshow.TabIndex = 15
        Me.cmdshow.Text = "Show Report"
        Me.cmdshow.UseVisualStyleBackColor = False
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdexit.FlatAppearance.BorderSize = 0
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.Black
        Me.cmdexit.Location = New System.Drawing.Point(736, 523)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(85, 28)
        Me.cmdexit.TabIndex = 16
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'RBONLYPAYMENT
        '
        Me.RBONLYPAYMENT.AutoSize = True
        Me.RBONLYPAYMENT.Location = New System.Drawing.Point(26, 121)
        Me.RBONLYPAYMENT.Name = "RBONLYPAYMENT"
        Me.RBONLYPAYMENT.Size = New System.Drawing.Size(175, 18)
        Me.RBONLYPAYMENT.TabIndex = 9
        Me.RBONLYPAYMENT.Text = "Only Payment Made Details"
        Me.RBONLYPAYMENT.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(724, 211)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(58, 14)
        Me.Label6.TabIndex = 673
        Me.Label6.Text = "Due Days"
        '
        'TXTDUDAYS
        '
        Me.TXTDUDAYS.Location = New System.Drawing.Point(784, 207)
        Me.TXTDUDAYS.Name = "TXTDUDAYS"
        Me.TXTDUDAYS.Size = New System.Drawing.Size(31, 23)
        Me.TXTDUDAYS.TabIndex = 672
        Me.TXTDUDAYS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'OutstandingFilter
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(905, 586)
        Me.Controls.Add(Me.BlendPanel2)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "OutstandingFilter"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Outstanding Filter"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel2.ResumeLayout(False)
        Me.BlendPanel2.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BlendPanel2 As VbPowerPack.BlendPanel
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents RBOUTSTANDING As System.Windows.Forms.RadioButton
    Friend WithEvents RBINVENTORY As System.Windows.Forms.RadioButton
    Friend WithEvents RBINT As System.Windows.Forms.RadioButton
    Friend WithEvents RBALL As System.Windows.Forms.RadioButton
    Friend WithEvents cmbacccode As System.Windows.Forms.ComboBox
    Friend WithEvents txtadd As System.Windows.Forms.TextBox
    Friend WithEvents chkdate As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dtto As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtfrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents CMBPARTYNAME As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmdshow As System.Windows.Forms.Button
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents RBPAYABLE As System.Windows.Forms.RadioButton
    Friend WithEvents RBREC As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents RBDUEDATE As System.Windows.Forms.RadioButton
    Friend WithEvents RBBILLDATE As System.Windows.Forms.RadioButton
    Friend WithEvents CHKADDRESS As System.Windows.Forms.CheckBox
    Friend WithEvents CHKSUMMARY As System.Windows.Forms.CheckBox
    Friend WithEvents CHKGROUPONNEWPG As System.Windows.Forms.CheckBox
    Friend WithEvents CMBGROUP As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents RBOUTSTANDINGBILLS As System.Windows.Forms.RadioButton
    Friend WithEvents RBALLBILLS As System.Windows.Forms.RadioButton
    Friend WithEvents TXTDAYS As System.Windows.Forms.TextBox
    Friend WithEvents TXTINTEREST As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents RBREMINDER As System.Windows.Forms.RadioButton
    Friend WithEvents CHKPRINTDATE As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GCHK As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Private WithEvents GNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GUNDER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCITY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents RBSELECTED As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents RBGROUP As System.Windows.Forms.RadioButton
    Friend WithEvents RBACCOUNT As System.Windows.Forms.RadioButton
    Friend WithEvents CHKSELECTALL As System.Windows.Forms.CheckBox
    Friend WithEvents RBBROKEROUTSTANDING As System.Windows.Forms.RadioButton
    Friend WithEvents CMBBROKERNAME As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents RBONLYPAYMENT As System.Windows.Forms.RadioButton
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TXTDUDAYS As System.Windows.Forms.TextBox
End Class
