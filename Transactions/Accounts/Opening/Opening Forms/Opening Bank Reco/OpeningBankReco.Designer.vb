<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OpeningBankReco
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OpeningBankReco))
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.TXTGRIDSRNO = New System.Windows.Forms.TextBox()
        Me.TXTAMOUNT = New System.Windows.Forms.TextBox()
        Me.CMBACCCODE = New System.Windows.Forms.ComboBox()
        Me.TXTCHQNO = New System.Windows.Forms.TextBox()
        Me.TXTADD = New System.Windows.Forms.TextBox()
        Me.CMBPARTYNAME = New System.Windows.Forms.ComboBox()
        Me.TXTSRNO = New System.Windows.Forms.TextBox()
        Me.TXTENTRYNO = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CMBREGISTER = New System.Windows.Forms.ComboBox()
        Me.TXTCHQISSUED = New System.Windows.Forms.TextBox()
        Me.CMBCHEQUE = New System.Windows.Forms.ComboBox()
        Me.TXTCHQDEPOSITED = New System.Windows.Forms.TextBox()
        Me.GRIDBANKRECO = New System.Windows.Forms.DataGridView()
        Me.GSRNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GTYPE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GENTRYNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GREGISTER = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GPARTYNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GCHQNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GAMOUNT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GRECODATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DTDATE = New System.Windows.Forms.MaskedTextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CMBOPENBANK = New System.Windows.Forms.ComboBox()
        Me.CMDDELETE = New System.Windows.Forms.Button()
        Me.CMDCLEAR = New System.Windows.Forms.Button()
        Me.CMDEXIT = New System.Windows.Forms.Button()
        Me.CMDOK = New System.Windows.Forms.Button()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.OpenToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.SaveToolStrip = New System.Windows.Forms.ToolStripButton()
        Me.PrintToolStrip = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripdelete = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripPrevious = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripNext = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.BlendPanel1.SuspendLayout()
        CType(Me.GRIDBANKRECO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.TXTGRIDSRNO)
        Me.BlendPanel1.Controls.Add(Me.TXTAMOUNT)
        Me.BlendPanel1.Controls.Add(Me.CMBACCCODE)
        Me.BlendPanel1.Controls.Add(Me.TXTCHQNO)
        Me.BlendPanel1.Controls.Add(Me.TXTADD)
        Me.BlendPanel1.Controls.Add(Me.CMBPARTYNAME)
        Me.BlendPanel1.Controls.Add(Me.TXTSRNO)
        Me.BlendPanel1.Controls.Add(Me.TXTENTRYNO)
        Me.BlendPanel1.Controls.Add(Me.Label1)
        Me.BlendPanel1.Controls.Add(Me.CMBREGISTER)
        Me.BlendPanel1.Controls.Add(Me.TXTCHQISSUED)
        Me.BlendPanel1.Controls.Add(Me.CMBCHEQUE)
        Me.BlendPanel1.Controls.Add(Me.TXTCHQDEPOSITED)
        Me.BlendPanel1.Controls.Add(Me.GRIDBANKRECO)
        Me.BlendPanel1.Controls.Add(Me.Label2)
        Me.BlendPanel1.Controls.Add(Me.Label3)
        Me.BlendPanel1.Controls.Add(Me.DTDATE)
        Me.BlendPanel1.Controls.Add(Me.Label4)
        Me.BlendPanel1.Controls.Add(Me.Label5)
        Me.BlendPanel1.Controls.Add(Me.CMBOPENBANK)
        Me.BlendPanel1.Controls.Add(Me.CMDDELETE)
        Me.BlendPanel1.Controls.Add(Me.CMDCLEAR)
        Me.BlendPanel1.Controls.Add(Me.CMDEXIT)
        Me.BlendPanel1.Controls.Add(Me.CMDOK)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1234, 582)
        Me.BlendPanel1.TabIndex = 1
        '
        'TXTGRIDSRNO
        '
        Me.TXTGRIDSRNO.BackColor = System.Drawing.Color.Linen
        Me.TXTGRIDSRNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTGRIDSRNO.ForeColor = System.Drawing.Color.Black
        Me.TXTGRIDSRNO.Location = New System.Drawing.Point(24, 119)
        Me.TXTGRIDSRNO.Name = "TXTGRIDSRNO"
        Me.TXTGRIDSRNO.ReadOnly = True
        Me.TXTGRIDSRNO.Size = New System.Drawing.Size(40, 23)
        Me.TXTGRIDSRNO.TabIndex = 0
        Me.TXTGRIDSRNO.TabStop = False
        '
        'TXTAMOUNT
        '
        Me.TXTAMOUNT.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTAMOUNT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTAMOUNT.ForeColor = System.Drawing.Color.Black
        Me.TXTAMOUNT.Location = New System.Drawing.Point(814, 119)
        Me.TXTAMOUNT.Name = "TXTAMOUNT"
        Me.TXTAMOUNT.Size = New System.Drawing.Size(100, 23)
        Me.TXTAMOUNT.TabIndex = 7
        Me.TXTAMOUNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CMBACCCODE
        '
        Me.CMBACCCODE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBACCCODE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBACCCODE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBACCCODE.FormattingEnabled = True
        Me.CMBACCCODE.Items.AddRange(New Object() {""})
        Me.CMBACCCODE.Location = New System.Drawing.Point(881, 45)
        Me.CMBACCCODE.Name = "CMBACCCODE"
        Me.CMBACCCODE.Size = New System.Drawing.Size(74, 23)
        Me.CMBACCCODE.TabIndex = 641
        Me.CMBACCCODE.Visible = False
        '
        'TXTCHQNO
        '
        Me.TXTCHQNO.BackColor = System.Drawing.Color.White
        Me.TXTCHQNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTCHQNO.ForeColor = System.Drawing.Color.Black
        Me.TXTCHQNO.Location = New System.Drawing.Point(714, 119)
        Me.TXTCHQNO.Name = "TXTCHQNO"
        Me.TXTCHQNO.Size = New System.Drawing.Size(100, 23)
        Me.TXTCHQNO.TabIndex = 6
        '
        'TXTADD
        '
        Me.TXTADD.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTADD.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTADD.Location = New System.Drawing.Point(845, 45)
        Me.TXTADD.MaxLength = 10
        Me.TXTADD.Name = "TXTADD"
        Me.TXTADD.Size = New System.Drawing.Size(30, 23)
        Me.TXTADD.TabIndex = 650
        Me.TXTADD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TXTADD.Visible = False
        '
        'CMBPARTYNAME
        '
        Me.CMBPARTYNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBPARTYNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBPARTYNAME.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBPARTYNAME.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBPARTYNAME.FormattingEnabled = True
        Me.CMBPARTYNAME.Location = New System.Drawing.Point(514, 119)
        Me.CMBPARTYNAME.MaxDropDownItems = 14
        Me.CMBPARTYNAME.Name = "CMBPARTYNAME"
        Me.CMBPARTYNAME.Size = New System.Drawing.Size(200, 23)
        Me.CMBPARTYNAME.TabIndex = 5
        '
        'TXTSRNO
        '
        Me.TXTSRNO.BackColor = System.Drawing.Color.Linen
        Me.TXTSRNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTSRNO.ForeColor = System.Drawing.Color.Black
        Me.TXTSRNO.Location = New System.Drawing.Point(720, 49)
        Me.TXTSRNO.Name = "TXTSRNO"
        Me.TXTSRNO.ReadOnly = True
        Me.TXTSRNO.Size = New System.Drawing.Size(88, 23)
        Me.TXTSRNO.TabIndex = 390
        Me.TXTSRNO.TabStop = False
        '
        'TXTENTRYNO
        '
        Me.TXTENTRYNO.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTENTRYNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTENTRYNO.ForeColor = System.Drawing.Color.Black
        Me.TXTENTRYNO.Location = New System.Drawing.Point(214, 119)
        Me.TXTENTRYNO.Name = "TXTENTRYNO"
        Me.TXTENTRYNO.Size = New System.Drawing.Size(100, 23)
        Me.TXTENTRYNO.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(678, 53)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 15)
        Me.Label1.TabIndex = 392
        Me.Label1.Text = "Sr No."
        '
        'CMBREGISTER
        '
        Me.CMBREGISTER.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBREGISTER.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBREGISTER.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CMBREGISTER.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBREGISTER.FormattingEnabled = True
        Me.CMBREGISTER.Location = New System.Drawing.Point(314, 119)
        Me.CMBREGISTER.MaxDropDownItems = 14
        Me.CMBREGISTER.Name = "CMBREGISTER"
        Me.CMBREGISTER.Size = New System.Drawing.Size(200, 23)
        Me.CMBREGISTER.TabIndex = 4
        '
        'TXTCHQISSUED
        '
        Me.TXTCHQISSUED.BackColor = System.Drawing.Color.Linen
        Me.TXTCHQISSUED.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTCHQISSUED.ForeColor = System.Drawing.Color.Black
        Me.TXTCHQISSUED.Location = New System.Drawing.Point(814, 406)
        Me.TXTCHQISSUED.Name = "TXTCHQISSUED"
        Me.TXTCHQISSUED.ReadOnly = True
        Me.TXTCHQISSUED.Size = New System.Drawing.Size(100, 23)
        Me.TXTCHQISSUED.TabIndex = 391
        Me.TXTCHQISSUED.TabStop = False
        Me.TXTCHQISSUED.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CMBCHEQUE
        '
        Me.CMBCHEQUE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CMBCHEQUE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBCHEQUE.FormattingEnabled = True
        Me.CMBCHEQUE.Items.AddRange(New Object() {"Cheque Deposited", "Cheque Issued"})
        Me.CMBCHEQUE.Location = New System.Drawing.Point(64, 119)
        Me.CMBCHEQUE.MaxDropDownItems = 14
        Me.CMBCHEQUE.Name = "CMBCHEQUE"
        Me.CMBCHEQUE.Size = New System.Drawing.Size(150, 23)
        Me.CMBCHEQUE.TabIndex = 2
        '
        'TXTCHQDEPOSITED
        '
        Me.TXTCHQDEPOSITED.BackColor = System.Drawing.Color.Linen
        Me.TXTCHQDEPOSITED.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTCHQDEPOSITED.ForeColor = System.Drawing.Color.Black
        Me.TXTCHQDEPOSITED.Location = New System.Drawing.Point(814, 378)
        Me.TXTCHQDEPOSITED.Name = "TXTCHQDEPOSITED"
        Me.TXTCHQDEPOSITED.ReadOnly = True
        Me.TXTCHQDEPOSITED.Size = New System.Drawing.Size(100, 23)
        Me.TXTCHQDEPOSITED.TabIndex = 389
        Me.TXTCHQDEPOSITED.TabStop = False
        Me.TXTCHQDEPOSITED.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'GRIDBANKRECO
        '
        Me.GRIDBANKRECO.AllowUserToAddRows = False
        Me.GRIDBANKRECO.AllowUserToDeleteRows = False
        Me.GRIDBANKRECO.AllowUserToResizeColumns = False
        Me.GRIDBANKRECO.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White
        Me.GRIDBANKRECO.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.GRIDBANKRECO.BackgroundColor = System.Drawing.Color.White
        Me.GRIDBANKRECO.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GRIDBANKRECO.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GRIDBANKRECO.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.GRIDBANKRECO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GRIDBANKRECO.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GSRNO, Me.GTYPE, Me.GENTRYNO, Me.GREGISTER, Me.GPARTYNAME, Me.GCHQNO, Me.GAMOUNT, Me.GRECODATE})
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDBANKRECO.DefaultCellStyle = DataGridViewCellStyle4
        Me.GRIDBANKRECO.GridColor = System.Drawing.SystemColors.ControlText
        Me.GRIDBANKRECO.Location = New System.Drawing.Point(24, 142)
        Me.GRIDBANKRECO.Margin = New System.Windows.Forms.Padding(2)
        Me.GRIDBANKRECO.MultiSelect = False
        Me.GRIDBANKRECO.Name = "GRIDBANKRECO"
        Me.GRIDBANKRECO.RowHeadersVisible = False
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Black
        Me.GRIDBANKRECO.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.GRIDBANKRECO.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.GRIDBANKRECO.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.GRIDBANKRECO.Size = New System.Drawing.Size(921, 231)
        Me.GRIDBANKRECO.TabIndex = 8
        '
        'GSRNO
        '
        Me.GSRNO.FillWeight = 50.0!
        Me.GSRNO.HeaderText = "Sr"
        Me.GSRNO.Name = "GSRNO"
        Me.GSRNO.ReadOnly = True
        Me.GSRNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GSRNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GSRNO.Width = 40
        '
        'GTYPE
        '
        Me.GTYPE.FillWeight = 50.0!
        Me.GTYPE.HeaderText = "Type"
        Me.GTYPE.Name = "GTYPE"
        Me.GTYPE.ReadOnly = True
        Me.GTYPE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GTYPE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GTYPE.Width = 150
        '
        'GENTRYNO
        '
        Me.GENTRYNO.HeaderText = "Entry No."
        Me.GENTRYNO.Name = "GENTRYNO"
        Me.GENTRYNO.ReadOnly = True
        Me.GENTRYNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GENTRYNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GREGISTER
        '
        Me.GREGISTER.HeaderText = "Register"
        Me.GREGISTER.Name = "GREGISTER"
        Me.GREGISTER.ReadOnly = True
        Me.GREGISTER.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GREGISTER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GREGISTER.Width = 200
        '
        'GPARTYNAME
        '
        Me.GPARTYNAME.HeaderText = "Party Name "
        Me.GPARTYNAME.Name = "GPARTYNAME"
        Me.GPARTYNAME.ReadOnly = True
        Me.GPARTYNAME.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GPARTYNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GPARTYNAME.Width = 200
        '
        'GCHQNO
        '
        Me.GCHQNO.HeaderText = "Chq No"
        Me.GCHQNO.Name = "GCHQNO"
        Me.GCHQNO.ReadOnly = True
        Me.GCHQNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GCHQNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GAMOUNT
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GAMOUNT.DefaultCellStyle = DataGridViewCellStyle3
        Me.GAMOUNT.HeaderText = "Amount"
        Me.GAMOUNT.Name = "GAMOUNT"
        Me.GAMOUNT.ReadOnly = True
        Me.GAMOUNT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GAMOUNT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GRECODATE
        '
        Me.GRECODATE.HeaderText = "Reco Date"
        Me.GRECODATE.Name = "GRECODATE"
        Me.GRECODATE.ReadOnly = True
        Me.GRECODATE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRECODATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GRECODATE.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(745, 411)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 15)
        Me.Label2.TabIndex = 388
        Me.Label2.Text = "Chq Issued"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(725, 381)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(86, 15)
        Me.Label3.TabIndex = 387
        Me.Label3.Text = "Chq Deposited"
        '
        'DTDATE
        '
        Me.DTDATE.AsciiOnly = True
        Me.DTDATE.BackColor = System.Drawing.Color.LemonChiffon
        Me.DTDATE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTDATE.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite
        Me.DTDATE.Location = New System.Drawing.Point(720, 77)
        Me.DTDATE.Mask = "00/00/0000"
        Me.DTDATE.Name = "DTDATE"
        Me.DTDATE.Size = New System.Drawing.Size(88, 23)
        Me.DTDATE.TabIndex = 1
        Me.DTDATE.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals
        Me.DTDATE.ValidatingType = GetType(Date)
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(685, 81)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(32, 15)
        Me.Label4.TabIndex = 386
        Me.Label4.Text = "Date"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(34, 53)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(68, 15)
        Me.Label5.TabIndex = 385
        Me.Label5.Text = "Bank Name"
        '
        'CMBOPENBANK
        '
        Me.CMBOPENBANK.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBOPENBANK.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBOPENBANK.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBOPENBANK.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBOPENBANK.FormattingEnabled = True
        Me.CMBOPENBANK.Location = New System.Drawing.Point(105, 49)
        Me.CMBOPENBANK.Name = "CMBOPENBANK"
        Me.CMBOPENBANK.Size = New System.Drawing.Size(293, 23)
        Me.CMBOPENBANK.TabIndex = 0
        '
        'CMDDELETE
        '
        Me.CMDDELETE.BackColor = System.Drawing.Color.Transparent
        Me.CMDDELETE.FlatAppearance.BorderSize = 0
        Me.CMDDELETE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDDELETE.ForeColor = System.Drawing.Color.Black
        Me.CMDDELETE.Location = New System.Drawing.Point(488, 406)
        Me.CMDDELETE.Name = "CMDDELETE"
        Me.CMDDELETE.Size = New System.Drawing.Size(80, 28)
        Me.CMDDELETE.TabIndex = 11
        Me.CMDDELETE.Text = "&Delete"
        Me.CMDDELETE.UseVisualStyleBackColor = False
        '
        'CMDCLEAR
        '
        Me.CMDCLEAR.BackColor = System.Drawing.Color.Transparent
        Me.CMDCLEAR.FlatAppearance.BorderSize = 0
        Me.CMDCLEAR.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDCLEAR.ForeColor = System.Drawing.Color.Black
        Me.CMDCLEAR.Location = New System.Drawing.Point(402, 406)
        Me.CMDCLEAR.Name = "CMDCLEAR"
        Me.CMDCLEAR.Size = New System.Drawing.Size(80, 28)
        Me.CMDCLEAR.TabIndex = 10
        Me.CMDCLEAR.Text = "&Clear"
        Me.CMDCLEAR.UseVisualStyleBackColor = False
        '
        'CMDEXIT
        '
        Me.CMDEXIT.BackColor = System.Drawing.Color.Transparent
        Me.CMDEXIT.FlatAppearance.BorderSize = 0
        Me.CMDEXIT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDEXIT.ForeColor = System.Drawing.Color.Black
        Me.CMDEXIT.Location = New System.Drawing.Point(574, 406)
        Me.CMDEXIT.Name = "CMDEXIT"
        Me.CMDEXIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEXIT.TabIndex = 12
        Me.CMDEXIT.Text = "E&xit"
        Me.CMDEXIT.UseVisualStyleBackColor = False
        '
        'CMDOK
        '
        Me.CMDOK.BackColor = System.Drawing.Color.Transparent
        Me.CMDOK.FlatAppearance.BorderSize = 0
        Me.CMDOK.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDOK.ForeColor = System.Drawing.Color.Black
        Me.CMDOK.Location = New System.Drawing.Point(316, 406)
        Me.CMDOK.Name = "CMDOK"
        Me.CMDOK.Size = New System.Drawing.Size(80, 28)
        Me.CMDOK.TabIndex = 9
        Me.CMDOK.Text = "&Save"
        Me.CMDOK.UseVisualStyleBackColor = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ToolStrip1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenToolStripButton, Me.SaveToolStrip, Me.PrintToolStrip, Me.ToolStripdelete, Me.toolStripSeparator, Me.ToolStripPrevious, Me.ToolStripNext, Me.ToolStripSeparator1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1234, 25)
        Me.ToolStrip1.TabIndex = 293
        Me.ToolStrip1.Text = "ToolStrip1"
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
        'SaveToolStrip
        '
        Me.SaveToolStrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.SaveToolStrip.Image = CType(resources.GetObject("SaveToolStrip.Image"), System.Drawing.Image)
        Me.SaveToolStrip.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.SaveToolStrip.Name = "SaveToolStrip"
        Me.SaveToolStrip.Size = New System.Drawing.Size(23, 22)
        Me.SaveToolStrip.Text = "&Save"
        '
        'PrintToolStrip
        '
        Me.PrintToolStrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PrintToolStrip.Image = CType(resources.GetObject("PrintToolStrip.Image"), System.Drawing.Image)
        Me.PrintToolStrip.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintToolStrip.Name = "PrintToolStrip"
        Me.PrintToolStrip.Size = New System.Drawing.Size(23, 22)
        Me.PrintToolStrip.Text = "&Print"
        '
        'ToolStripdelete
        '
        Me.ToolStripdelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripdelete.Image = CType(resources.GetObject("ToolStripdelete.Image"), System.Drawing.Image)
        Me.ToolStripdelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripdelete.Name = "ToolStripdelete"
        Me.ToolStripdelete.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripdelete.Text = "&Delete Receipt"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripPrevious
        '
        Me.ToolStripPrevious.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripPrevious.Image = Global.PROCESS.My.Resources.Resources.POINT02
        Me.ToolStripPrevious.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripPrevious.Name = "ToolStripPrevious"
        Me.ToolStripPrevious.Size = New System.Drawing.Size(73, 22)
        Me.ToolStripPrevious.Text = "Previous"
        '
        'ToolStripNext
        '
        Me.ToolStripNext.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripNext.Image = Global.PROCESS.My.Resources.Resources.POINT04
        Me.ToolStripNext.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripNext.Name = "ToolStripNext"
        Me.ToolStripNext.Size = New System.Drawing.Size(51, 22)
        Me.ToolStripNext.Text = "Next"
        Me.ToolStripNext.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'EP
        '
        Me.EP.BlinkRate = 0
        Me.EP.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.EP.ContainerControl = Me
        '
        'OpeningBankReco
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1234, 582)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.BlendPanel1)
        Me.KeyPreview = True
        Me.Name = "OpeningBankReco"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "OpeningBankReco"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.GRIDBANKRECO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BlendPanel1 As BlendPanel
    Friend WithEvents TXTGRIDSRNO As TextBox
    Friend WithEvents TXTAMOUNT As TextBox
    Friend WithEvents CMBACCCODE As ComboBox
    Friend WithEvents TXTCHQNO As TextBox
    Friend WithEvents TXTADD As TextBox
    Friend WithEvents CMBPARTYNAME As ComboBox
    Friend WithEvents TXTSRNO As TextBox
    Friend WithEvents TXTENTRYNO As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents CMBREGISTER As ComboBox
    Friend WithEvents TXTCHQISSUED As TextBox
    Friend WithEvents CMBCHEQUE As ComboBox
    Friend WithEvents TXTCHQDEPOSITED As TextBox
    Friend WithEvents GRIDBANKRECO As DataGridView
    Friend WithEvents GSRNO As DataGridViewTextBoxColumn
    Friend WithEvents GTYPE As DataGridViewTextBoxColumn
    Friend WithEvents GENTRYNO As DataGridViewTextBoxColumn
    Friend WithEvents GREGISTER As DataGridViewTextBoxColumn
    Friend WithEvents GPARTYNAME As DataGridViewTextBoxColumn
    Friend WithEvents GCHQNO As DataGridViewTextBoxColumn
    Friend WithEvents GAMOUNT As DataGridViewTextBoxColumn
    Friend WithEvents GRECODATE As DataGridViewTextBoxColumn
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents DTDATE As MaskedTextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents CMBOPENBANK As ComboBox
    Friend WithEvents CMDDELETE As Button
    Friend WithEvents CMDCLEAR As Button
    Friend WithEvents CMDEXIT As Button
    Friend WithEvents CMDOK As Button
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents OpenToolStripButton As ToolStripButton
    Friend WithEvents SaveToolStrip As ToolStripButton
    Friend WithEvents PrintToolStrip As ToolStripButton
    Friend WithEvents ToolStripdelete As ToolStripButton
    Friend WithEvents toolStripSeparator As ToolStripSeparator
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents EP As ErrorProvider
    Friend WithEvents ToolStripPrevious As ToolStripButton
    Friend WithEvents ToolStripNext As ToolStripButton
End Class
