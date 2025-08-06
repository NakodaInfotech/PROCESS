<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OpeningBills
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OpeningBills))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.CMBREGISTER = New System.Windows.Forms.ComboBox()
        Me.BILLDATE = New System.Windows.Forms.MaskedTextBox()
        Me.CMBCODE = New System.Windows.Forms.ComboBox()
        Me.CMBAGENT = New System.Windows.Forms.ComboBox()
        Me.TXTREMARKS = New System.Windows.Forms.TextBox()
        Me.TXTCRDAYS = New System.Windows.Forms.TextBox()
        Me.CHKDISPUTE = New System.Windows.Forms.CheckBox()
        Me.DUEDATE = New System.Windows.Forms.DateTimePicker()
        Me.lbldrcropening = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.OpenToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.SaveToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripdelete = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.txtopening = New System.Windows.Forms.TextBox()
        Me.lblopbal = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TXTADD = New System.Windows.Forms.TextBox()
        Me.LBLTOTAL = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.CMBTYPE = New System.Windows.Forms.ComboBox()
        Me.TXTYEAR = New System.Windows.Forms.TextBox()
        Me.TXTBILLNO = New System.Windows.Forms.TextBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.CMBACCCODE = New System.Windows.Forms.ComboBox()
        Me.CMBNAME = New System.Windows.Forms.ComboBox()
        Me.cmdclear = New System.Windows.Forms.Button()
        Me.cmdok = New System.Windows.Forms.Button()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.lbl = New System.Windows.Forms.Label()
        Me.TXTSRNO = New System.Windows.Forms.TextBox()
        Me.GRIDOPENING = New System.Windows.Forms.DataGridView()
        Me.TXTAMT = New System.Windows.Forms.TextBox()
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.GSRNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GBILLTYPE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GREGISTER = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GBILLNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GYEAR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GBILLDATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GCRDAYS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GDUEDATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GAGENT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GNARRATION = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GDISPUTE = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.GAMT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GAMTPAIDREC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GEXTRAAMT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GRETURN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GBALANCE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GPRINTINITIALS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BlendPanel1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.GRIDOPENING, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.CMBREGISTER)
        Me.BlendPanel1.Controls.Add(Me.BILLDATE)
        Me.BlendPanel1.Controls.Add(Me.CMBCODE)
        Me.BlendPanel1.Controls.Add(Me.CMBAGENT)
        Me.BlendPanel1.Controls.Add(Me.TXTREMARKS)
        Me.BlendPanel1.Controls.Add(Me.TXTCRDAYS)
        Me.BlendPanel1.Controls.Add(Me.CHKDISPUTE)
        Me.BlendPanel1.Controls.Add(Me.DUEDATE)
        Me.BlendPanel1.Controls.Add(Me.lbldrcropening)
        Me.BlendPanel1.Controls.Add(Me.ToolStrip1)
        Me.BlendPanel1.Controls.Add(Me.txtopening)
        Me.BlendPanel1.Controls.Add(Me.lblopbal)
        Me.BlendPanel1.Controls.Add(Me.Label7)
        Me.BlendPanel1.Controls.Add(Me.Label4)
        Me.BlendPanel1.Controls.Add(Me.TXTADD)
        Me.BlendPanel1.Controls.Add(Me.LBLTOTAL)
        Me.BlendPanel1.Controls.Add(Me.Label23)
        Me.BlendPanel1.Controls.Add(Me.CMBTYPE)
        Me.BlendPanel1.Controls.Add(Me.TXTYEAR)
        Me.BlendPanel1.Controls.Add(Me.TXTBILLNO)
        Me.BlendPanel1.Controls.Add(Me.Label31)
        Me.BlendPanel1.Controls.Add(Me.Label33)
        Me.BlendPanel1.Controls.Add(Me.CMBACCCODE)
        Me.BlendPanel1.Controls.Add(Me.CMBNAME)
        Me.BlendPanel1.Controls.Add(Me.cmdclear)
        Me.BlendPanel1.Controls.Add(Me.cmdok)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Controls.Add(Me.lbl)
        Me.BlendPanel1.Controls.Add(Me.TXTSRNO)
        Me.BlendPanel1.Controls.Add(Me.GRIDOPENING)
        Me.BlendPanel1.Controls.Add(Me.TXTAMT)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1134, 562)
        Me.BlendPanel1.TabIndex = 0
        '
        'CMBREGISTER
        '
        Me.CMBREGISTER.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBREGISTER.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBREGISTER.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBREGISTER.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBREGISTER.FormattingEnabled = True
        Me.CMBREGISTER.Items.AddRange(New Object() {""})
        Me.CMBREGISTER.Location = New System.Drawing.Point(150, 84)
        Me.CMBREGISTER.Name = "CMBREGISTER"
        Me.CMBREGISTER.Size = New System.Drawing.Size(150, 23)
        Me.CMBREGISTER.TabIndex = 2
        '
        'BILLDATE
        '
        Me.BILLDATE.AsciiOnly = True
        Me.BILLDATE.BackColor = System.Drawing.Color.LemonChiffon
        Me.BILLDATE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BILLDATE.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite
        Me.BILLDATE.Location = New System.Drawing.Point(430, 84)
        Me.BILLDATE.Mask = "00/00/0000"
        Me.BILLDATE.Name = "BILLDATE"
        Me.BILLDATE.Size = New System.Drawing.Size(81, 23)
        Me.BILLDATE.TabIndex = 5
        Me.BILLDATE.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals
        Me.BILLDATE.ValidatingType = GetType(Date)
        '
        'CMBCODE
        '
        Me.CMBCODE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBCODE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBCODE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBCODE.FormattingEnabled = True
        Me.CMBCODE.Items.AddRange(New Object() {""})
        Me.CMBCODE.Location = New System.Drawing.Point(608, 54)
        Me.CMBCODE.Name = "CMBCODE"
        Me.CMBCODE.Size = New System.Drawing.Size(74, 23)
        Me.CMBCODE.TabIndex = 653
        Me.CMBCODE.Visible = False
        '
        'CMBAGENT
        '
        Me.CMBAGENT.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBAGENT.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBAGENT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBAGENT.FormattingEnabled = True
        Me.CMBAGENT.Items.AddRange(New Object() {""})
        Me.CMBAGENT.Location = New System.Drawing.Point(656, 84)
        Me.CMBAGENT.Name = "CMBAGENT"
        Me.CMBAGENT.Size = New System.Drawing.Size(200, 23)
        Me.CMBAGENT.TabIndex = 8
        '
        'TXTREMARKS
        '
        Me.TXTREMARKS.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTREMARKS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTREMARKS.Location = New System.Drawing.Point(856, 84)
        Me.TXTREMARKS.MaxLength = 200
        Me.TXTREMARKS.Name = "TXTREMARKS"
        Me.TXTREMARKS.Size = New System.Drawing.Size(150, 23)
        Me.TXTREMARKS.TabIndex = 9
        '
        'TXTCRDAYS
        '
        Me.TXTCRDAYS.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTCRDAYS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTCRDAYS.Location = New System.Drawing.Point(511, 84)
        Me.TXTCRDAYS.MaxLength = 10
        Me.TXTCRDAYS.Name = "TXTCRDAYS"
        Me.TXTCRDAYS.Size = New System.Drawing.Size(60, 23)
        Me.TXTCRDAYS.TabIndex = 6
        Me.TXTCRDAYS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CHKDISPUTE
        '
        Me.CHKDISPUTE.AutoSize = True
        Me.CHKDISPUTE.BackColor = System.Drawing.Color.Transparent
        Me.CHKDISPUTE.Location = New System.Drawing.Point(759, 34)
        Me.CHKDISPUTE.Name = "CHKDISPUTE"
        Me.CHKDISPUTE.Size = New System.Drawing.Size(82, 18)
        Me.CHKDISPUTE.TabIndex = 9
        Me.CHKDISPUTE.Text = "Bill Dispute"
        Me.CHKDISPUTE.UseVisualStyleBackColor = False
        Me.CHKDISPUTE.Visible = False
        '
        'DUEDATE
        '
        Me.DUEDATE.CustomFormat = "dd/MM/yyyy"
        Me.DUEDATE.Enabled = False
        Me.DUEDATE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DUEDATE.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DUEDATE.Location = New System.Drawing.Point(571, 84)
        Me.DUEDATE.Name = "DUEDATE"
        Me.DUEDATE.Size = New System.Drawing.Size(85, 23)
        Me.DUEDATE.TabIndex = 7
        '
        'lbldrcropening
        '
        Me.lbldrcropening.AutoSize = True
        Me.lbldrcropening.BackColor = System.Drawing.Color.Transparent
        Me.lbldrcropening.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldrcropening.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lbldrcropening.Location = New System.Drawing.Point(972, 61)
        Me.lbldrcropening.Name = "lbldrcropening"
        Me.lbldrcropening.Size = New System.Drawing.Size(0, 15)
        Me.lbldrcropening.TabIndex = 490
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenToolStripButton, Me.SaveToolStripButton, Me.ToolStripSeparator1, Me.ToolStripdelete, Me.ToolStripSeparator2})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1134, 25)
        Me.ToolStrip1.TabIndex = 652
        Me.ToolStrip1.Text = "v"
        '
        'OpenToolStripButton
        '
        Me.OpenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.OpenToolStripButton.Image = CType(resources.GetObject("OpenToolStripButton.Image"), System.Drawing.Image)
        Me.OpenToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.OpenToolStripButton.Name = "OpenToolStripButton"
        Me.OpenToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.OpenToolStripButton.Text = "&Open"
        '
        'SaveToolStripButton
        '
        Me.SaveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.SaveToolStripButton.Image = CType(resources.GetObject("SaveToolStripButton.Image"), System.Drawing.Image)
        Me.SaveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.SaveToolStripButton.Name = "SaveToolStripButton"
        Me.SaveToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.SaveToolStripButton.Text = "&Save"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripdelete
        '
        Me.ToolStripdelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripdelete.Image = CType(resources.GetObject("ToolStripdelete.Image"), System.Drawing.Image)
        Me.ToolStripdelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripdelete.Name = "ToolStripdelete"
        Me.ToolStripdelete.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripdelete.Text = "&Delete All"
        Me.ToolStripdelete.ToolTipText = "Delete Contra"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'txtopening
        '
        Me.txtopening.BackColor = System.Drawing.Color.White
        Me.txtopening.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtopening.ForeColor = System.Drawing.Color.Black
        Me.txtopening.Location = New System.Drawing.Point(870, 57)
        Me.txtopening.Name = "txtopening"
        Me.txtopening.ReadOnly = True
        Me.txtopening.Size = New System.Drawing.Size(100, 23)
        Me.txtopening.TabIndex = 489
        Me.txtopening.TabStop = False
        Me.txtopening.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblopbal
        '
        Me.lblopbal.AutoSize = True
        Me.lblopbal.BackColor = System.Drawing.Color.Transparent
        Me.lblopbal.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblopbal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblopbal.Location = New System.Drawing.Point(820, 61)
        Me.lblopbal.Name = "lblopbal"
        Me.lblopbal.Size = New System.Drawing.Size(47, 15)
        Me.lblopbal.TabIndex = 488
        Me.lblopbal.Text = "O/P Bal"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Red
        Me.Label7.Location = New System.Drawing.Point(43, 531)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(71, 14)
        Me.Label7.TabIndex = 651
        Me.Label7.Text = "Items Locked"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Location = New System.Drawing.Point(24, 530)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(18, 16)
        Me.Label4.TabIndex = 650
        Me.Label4.Text = "   "
        '
        'TXTADD
        '
        Me.TXTADD.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTADD.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTADD.Location = New System.Drawing.Point(306, 30)
        Me.TXTADD.MaxLength = 10
        Me.TXTADD.Name = "TXTADD"
        Me.TXTADD.Size = New System.Drawing.Size(30, 22)
        Me.TXTADD.TabIndex = 649
        Me.TXTADD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TXTADD.Visible = False
        '
        'LBLTOTAL
        '
        Me.LBLTOTAL.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTAL.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTAL.Location = New System.Drawing.Point(832, 530)
        Me.LBLTOTAL.Name = "LBLTOTAL"
        Me.LBLTOTAL.Size = New System.Drawing.Size(113, 14)
        Me.LBLTOTAL.TabIndex = 648
        Me.LBLTOTAL.Text = "0.00"
        Me.LBLTOTAL.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(803, 530)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(31, 14)
        Me.Label23.TabIndex = 647
        Me.Label23.Text = "Total"
        '
        'CMBTYPE
        '
        Me.CMBTYPE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBTYPE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBTYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CMBTYPE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBTYPE.FormattingEnabled = True
        Me.CMBTYPE.Items.AddRange(New Object() {"PURCHASE", "SALE", "PAYMENT", "RECEIPT", "DEBIT", "CREDIT"})
        Me.CMBTYPE.Location = New System.Drawing.Point(50, 84)
        Me.CMBTYPE.Name = "CMBTYPE"
        Me.CMBTYPE.Size = New System.Drawing.Size(100, 23)
        Me.CMBTYPE.TabIndex = 1
        '
        'TXTYEAR
        '
        Me.TXTYEAR.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTYEAR.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTYEAR.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTYEAR.Location = New System.Drawing.Point(380, 84)
        Me.TXTYEAR.MaxLength = 10
        Me.TXTYEAR.Name = "TXTYEAR"
        Me.TXTYEAR.Size = New System.Drawing.Size(50, 23)
        Me.TXTYEAR.TabIndex = 4
        Me.TXTYEAR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTBILLNO
        '
        Me.TXTBILLNO.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTBILLNO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTBILLNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTBILLNO.Location = New System.Drawing.Point(300, 84)
        Me.TXTBILLNO.MaxLength = 10
        Me.TXTBILLNO.Name = "TXTBILLNO"
        Me.TXTBILLNO.Size = New System.Drawing.Size(80, 23)
        Me.TXTBILLNO.TabIndex = 3
        Me.TXTBILLNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.BackColor = System.Drawing.Color.Transparent
        Me.Label31.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(25, 62)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(88, 15)
        Me.Label31.TabIndex = 643
        Me.Label31.Text = "Party A/C Code"
        Me.Label31.Visible = False
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.BackColor = System.Drawing.Color.Transparent
        Me.Label33.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(192, 62)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(91, 15)
        Me.Label33.TabIndex = 642
        Me.Label33.Text = "Party A/C Name"
        '
        'CMBACCCODE
        '
        Me.CMBACCCODE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBACCCODE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBACCCODE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBACCCODE.FormattingEnabled = True
        Me.CMBACCCODE.Items.AddRange(New Object() {""})
        Me.CMBACCCODE.Location = New System.Drawing.Point(110, 58)
        Me.CMBACCCODE.Name = "CMBACCCODE"
        Me.CMBACCCODE.Size = New System.Drawing.Size(74, 23)
        Me.CMBACCCODE.TabIndex = 640
        Me.CMBACCCODE.Visible = False
        '
        'CMBNAME
        '
        Me.CMBNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBNAME.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBNAME.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBNAME.FormattingEnabled = True
        Me.CMBNAME.Items.AddRange(New Object() {""})
        Me.CMBNAME.Location = New System.Drawing.Point(285, 58)
        Me.CMBNAME.Name = "CMBNAME"
        Me.CMBNAME.Size = New System.Drawing.Size(269, 23)
        Me.CMBNAME.TabIndex = 0
        '
        'cmdclear
        '
        Me.cmdclear.BackColor = System.Drawing.Color.Transparent
        Me.cmdclear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdclear.FlatAppearance.BorderSize = 0
        Me.cmdclear.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdclear.ForeColor = System.Drawing.Color.Black
        Me.cmdclear.Location = New System.Drawing.Point(462, 529)
        Me.cmdclear.Name = "cmdclear"
        Me.cmdclear.Size = New System.Drawing.Size(80, 28)
        Me.cmdclear.TabIndex = 13
        Me.cmdclear.Text = "&Clear"
        Me.cmdclear.UseVisualStyleBackColor = False
        '
        'cmdok
        '
        Me.cmdok.BackColor = System.Drawing.Color.Transparent
        Me.cmdok.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdok.FlatAppearance.BorderSize = 0
        Me.cmdok.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdok.ForeColor = System.Drawing.Color.Black
        Me.cmdok.Location = New System.Drawing.Point(376, 529)
        Me.cmdok.Name = "cmdok"
        Me.cmdok.Size = New System.Drawing.Size(80, 28)
        Me.cmdok.TabIndex = 12
        Me.cmdok.Text = "&Save"
        Me.cmdok.UseVisualStyleBackColor = False
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdexit.FlatAppearance.BorderSize = 0
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.Black
        Me.cmdexit.Location = New System.Drawing.Point(548, 529)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 28)
        Me.cmdexit.TabIndex = 14
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'lbl
        '
        Me.lbl.AutoSize = True
        Me.lbl.BackColor = System.Drawing.Color.Transparent
        Me.lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lbl.Location = New System.Drawing.Point(10, 29)
        Me.lbl.Name = "lbl"
        Me.lbl.Size = New System.Drawing.Size(105, 20)
        Me.lbl.TabIndex = 313
        Me.lbl.Text = "Opening Bill"
        '
        'TXTSRNO
        '
        Me.TXTSRNO.BackColor = System.Drawing.Color.Linen
        Me.TXTSRNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTSRNO.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TXTSRNO.Location = New System.Drawing.Point(10, 84)
        Me.TXTSRNO.Name = "TXTSRNO"
        Me.TXTSRNO.ReadOnly = True
        Me.TXTSRNO.Size = New System.Drawing.Size(40, 23)
        Me.TXTSRNO.TabIndex = 1
        Me.TXTSRNO.TabStop = False
        '
        'GRIDOPENING
        '
        Me.GRIDOPENING.AllowUserToAddRows = False
        Me.GRIDOPENING.AllowUserToDeleteRows = False
        Me.GRIDOPENING.AllowUserToResizeColumns = False
        Me.GRIDOPENING.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White
        Me.GRIDOPENING.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.GRIDOPENING.BackgroundColor = System.Drawing.Color.White
        Me.GRIDOPENING.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GRIDOPENING.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GRIDOPENING.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.GRIDOPENING.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.GRIDOPENING.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GSRNO, Me.GBILLTYPE, Me.GREGISTER, Me.GBILLNO, Me.GYEAR, Me.GBILLDATE, Me.GCRDAYS, Me.GDUEDATE, Me.GAGENT, Me.GNARRATION, Me.GDISPUTE, Me.GAMT, Me.GAMTPAIDREC, Me.GEXTRAAMT, Me.GRETURN, Me.GBALANCE, Me.GPRINTINITIALS})
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDOPENING.DefaultCellStyle = DataGridViewCellStyle5
        Me.GRIDOPENING.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.GRIDOPENING.GridColor = System.Drawing.SystemColors.Control
        Me.GRIDOPENING.Location = New System.Drawing.Point(10, 106)
        Me.GRIDOPENING.MultiSelect = False
        Me.GRIDOPENING.Name = "GRIDOPENING"
        Me.GRIDOPENING.ReadOnly = True
        Me.GRIDOPENING.RowHeadersVisible = False
        Me.GRIDOPENING.RowHeadersWidth = 30
        Me.GRIDOPENING.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Black
        Me.GRIDOPENING.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.GRIDOPENING.RowTemplate.Height = 20
        Me.GRIDOPENING.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDOPENING.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.GRIDOPENING.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GRIDOPENING.Size = New System.Drawing.Size(1114, 418)
        Me.GRIDOPENING.TabIndex = 11
        Me.GRIDOPENING.TabStop = False
        '
        'TXTAMT
        '
        Me.TXTAMT.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTAMT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTAMT.Location = New System.Drawing.Point(1006, 84)
        Me.TXTAMT.Name = "TXTAMT"
        Me.TXTAMT.Size = New System.Drawing.Size(90, 23)
        Me.TXTAMT.TabIndex = 10
        Me.TXTAMT.Text = "0.00"
        Me.TXTAMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'EP
        '
        Me.EP.BlinkRate = 0
        Me.EP.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.EP.ContainerControl = Me
        '
        'GSRNO
        '
        Me.GSRNO.HeaderText = "Sr."
        Me.GSRNO.Name = "GSRNO"
        Me.GSRNO.ReadOnly = True
        Me.GSRNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GSRNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GSRNO.Width = 40
        '
        'GBILLTYPE
        '
        Me.GBILLTYPE.HeaderText = "Bill Type"
        Me.GBILLTYPE.Name = "GBILLTYPE"
        Me.GBILLTYPE.ReadOnly = True
        Me.GBILLTYPE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GBILLTYPE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GREGISTER
        '
        Me.GREGISTER.HeaderText = "Register"
        Me.GREGISTER.Name = "GREGISTER"
        Me.GREGISTER.ReadOnly = True
        Me.GREGISTER.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GREGISTER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GREGISTER.Width = 150
        '
        'GBILLNO
        '
        Me.GBILLNO.HeaderText = "Bill No"
        Me.GBILLNO.Name = "GBILLNO"
        Me.GBILLNO.ReadOnly = True
        Me.GBILLNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GBILLNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GBILLNO.Width = 80
        '
        'GYEAR
        '
        Me.GYEAR.HeaderText = "Year"
        Me.GYEAR.Name = "GYEAR"
        Me.GYEAR.ReadOnly = True
        Me.GYEAR.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GYEAR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GYEAR.Width = 50
        '
        'GBILLDATE
        '
        Me.GBILLDATE.HeaderText = "Date"
        Me.GBILLDATE.Name = "GBILLDATE"
        Me.GBILLDATE.ReadOnly = True
        Me.GBILLDATE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GBILLDATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GBILLDATE.Width = 80
        '
        'GCRDAYS
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GCRDAYS.DefaultCellStyle = DataGridViewCellStyle3
        Me.GCRDAYS.HeaderText = "Cr Days"
        Me.GCRDAYS.Name = "GCRDAYS"
        Me.GCRDAYS.ReadOnly = True
        Me.GCRDAYS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GCRDAYS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GCRDAYS.Width = 60
        '
        'GDUEDATE
        '
        Me.GDUEDATE.HeaderText = "Due Date"
        Me.GDUEDATE.Name = "GDUEDATE"
        Me.GDUEDATE.ReadOnly = True
        Me.GDUEDATE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GDUEDATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GDUEDATE.Width = 85
        '
        'GAGENT
        '
        Me.GAGENT.HeaderText = "Agent"
        Me.GAGENT.Name = "GAGENT"
        Me.GAGENT.ReadOnly = True
        Me.GAGENT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GAGENT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GAGENT.Width = 200
        '
        'GNARRATION
        '
        Me.GNARRATION.HeaderText = "Remarks"
        Me.GNARRATION.Name = "GNARRATION"
        Me.GNARRATION.ReadOnly = True
        Me.GNARRATION.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GNARRATION.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GNARRATION.Width = 150
        '
        'GDISPUTE
        '
        Me.GDISPUTE.HeaderText = "Dispute"
        Me.GDISPUTE.Name = "GDISPUTE"
        Me.GDISPUTE.ReadOnly = True
        Me.GDISPUTE.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GDISPUTE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.GDISPUTE.Visible = False
        '
        'GAMT
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N2"
        Me.GAMT.DefaultCellStyle = DataGridViewCellStyle4
        Me.GAMT.HeaderText = "Amt"
        Me.GAMT.Name = "GAMT"
        Me.GAMT.ReadOnly = True
        Me.GAMT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GAMT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GAMT.Width = 90
        '
        'GAMTPAIDREC
        '
        Me.GAMTPAIDREC.HeaderText = "AMT PAID"
        Me.GAMTPAIDREC.Name = "GAMTPAIDREC"
        Me.GAMTPAIDREC.ReadOnly = True
        Me.GAMTPAIDREC.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GAMTPAIDREC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GAMTPAIDREC.Visible = False
        '
        'GEXTRAAMT
        '
        Me.GEXTRAAMT.HeaderText = "EXTRA AMT"
        Me.GEXTRAAMT.Name = "GEXTRAAMT"
        Me.GEXTRAAMT.ReadOnly = True
        Me.GEXTRAAMT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GEXTRAAMT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GEXTRAAMT.Visible = False
        '
        'GRETURN
        '
        Me.GRETURN.HeaderText = "RETURN"
        Me.GRETURN.Name = "GRETURN"
        Me.GRETURN.ReadOnly = True
        Me.GRETURN.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRETURN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GRETURN.Visible = False
        '
        'GBALANCE
        '
        Me.GBALANCE.HeaderText = "BALANCE"
        Me.GBALANCE.Name = "GBALANCE"
        Me.GBALANCE.ReadOnly = True
        Me.GBALANCE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GBALANCE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GBALANCE.Visible = False
        '
        'GPRINTINITIALS
        '
        Me.GPRINTINITIALS.HeaderText = "PRINTINITIALS"
        Me.GPRINTINITIALS.Name = "GPRINTINITIALS"
        Me.GPRINTINITIALS.ReadOnly = True
        Me.GPRINTINITIALS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GPRINTINITIALS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GPRINTINITIALS.Visible = False
        '
        'OpeningBills
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1134, 562)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "OpeningBills"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Opening Bills"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.GRIDOPENING, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents cmdclear As System.Windows.Forms.Button
    Friend WithEvents cmdok As System.Windows.Forms.Button
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents lbl As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents CMBACCCODE As System.Windows.Forms.ComboBox
    Friend WithEvents CMBNAME As System.Windows.Forms.ComboBox
    Friend WithEvents TXTAMT As System.Windows.Forms.TextBox
    Friend WithEvents TXTSRNO As System.Windows.Forms.TextBox
    Friend WithEvents GRIDOPENING As System.Windows.Forms.DataGridView
    Friend WithEvents TXTYEAR As System.Windows.Forms.TextBox
    Friend WithEvents TXTBILLNO As System.Windows.Forms.TextBox
    Friend WithEvents CMBTYPE As System.Windows.Forms.ComboBox
    Friend WithEvents GPURSRNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EP As System.Windows.Forms.ErrorProvider
    Friend WithEvents LBLTOTAL As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents TXTADD As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripdelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents OpenToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents lbldrcropening As System.Windows.Forms.Label
    Friend WithEvents txtopening As System.Windows.Forms.TextBox
    Friend WithEvents lblopbal As System.Windows.Forms.Label
    Friend WithEvents DUEDATE As System.Windows.Forms.DateTimePicker
    Friend WithEvents CHKDISPUTE As System.Windows.Forms.CheckBox
    Friend WithEvents TXTREMARKS As System.Windows.Forms.TextBox
    Friend WithEvents TXTCRDAYS As System.Windows.Forms.TextBox
    Friend WithEvents CMBAGENT As System.Windows.Forms.ComboBox
    Friend WithEvents CMBCODE As System.Windows.Forms.ComboBox
    Friend WithEvents BILLDATE As System.Windows.Forms.MaskedTextBox
    Friend WithEvents CMBREGISTER As System.Windows.Forms.ComboBox
    Friend WithEvents GSRNO As DataGridViewTextBoxColumn
    Friend WithEvents GBILLTYPE As DataGridViewTextBoxColumn
    Friend WithEvents GREGISTER As DataGridViewTextBoxColumn
    Friend WithEvents GBILLNO As DataGridViewTextBoxColumn
    Friend WithEvents GYEAR As DataGridViewTextBoxColumn
    Friend WithEvents GBILLDATE As DataGridViewTextBoxColumn
    Friend WithEvents GCRDAYS As DataGridViewTextBoxColumn
    Friend WithEvents GDUEDATE As DataGridViewTextBoxColumn
    Friend WithEvents GAGENT As DataGridViewTextBoxColumn
    Friend WithEvents GNARRATION As DataGridViewTextBoxColumn
    Friend WithEvents GDISPUTE As DataGridViewCheckBoxColumn
    Friend WithEvents GAMT As DataGridViewTextBoxColumn
    Friend WithEvents GAMTPAIDREC As DataGridViewTextBoxColumn
    Friend WithEvents GEXTRAAMT As DataGridViewTextBoxColumn
    Friend WithEvents GRETURN As DataGridViewTextBoxColumn
    Friend WithEvents GBALANCE As DataGridViewTextBoxColumn
    Friend WithEvents GPRINTINITIALS As DataGridViewTextBoxColumn
End Class
