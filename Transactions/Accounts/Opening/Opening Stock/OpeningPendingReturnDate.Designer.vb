<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OpeningPendingReturnDate
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
        Me.components = New System.ComponentModel.Container
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.BlendPanel1 = New VbPowerPack.BlendPanel
        Me.TXTADD = New System.Windows.Forms.TextBox
        Me.cmbcode = New System.Windows.Forms.ComboBox
        Me.CMBDYEINGFILTER = New System.Windows.Forms.ComboBox
        Me.LBLTOTALTAKA = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.LBLTOTALMTRS = New System.Windows.Forms.Label
        Me.TXTOPRETNO = New System.Windows.Forms.TextBox
        Me.LBLTOTAL = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.CMBOURGODOWN = New System.Windows.Forms.ComboBox
        Me.TXTRETDONO = New System.Windows.Forms.TextBox
        Me.TXTRETTAKA = New System.Windows.Forms.TextBox
        Me.TXTRETMTRS = New System.Windows.Forms.TextBox
        Me.CMBTRANS = New System.Windows.Forms.ComboBox
        Me.DTRETDODATE = New System.Windows.Forms.MaskedTextBox
        Me.CMBDYEING = New System.Windows.Forms.ComboBox
        Me.DTINVDATE = New System.Windows.Forms.MaskedTextBox
        Me.TXTGRNO = New System.Windows.Forms.TextBox
        Me.TXTINVNO = New System.Windows.Forms.TextBox
        Me.TXTLOTNO = New System.Windows.Forms.TextBox
        Me.CMBGREYQUALITY = New System.Windows.Forms.ComboBox
        Me.GRIDSTOCK = New System.Windows.Forms.DataGridView
        Me.cmdexit = New System.Windows.Forms.Button
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.GOPRETNO = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GGODOWN = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GOPGRNO = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GOPINVNO = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GOPDATE = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GNAME = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GOPDYEING = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GOPLOTNO = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GOPGREYQUALITY = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GOPRETTAKA = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GOPRETMTRS = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GOPRETDO = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GOPRETDATE = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GOPTRANSPORT = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CMBNAME = New System.Windows.Forms.ComboBox
        Me.BlendPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.GRIDSTOCK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.AutoSize = True
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.TXTADD)
        Me.BlendPanel1.Controls.Add(Me.cmbcode)
        Me.BlendPanel1.Controls.Add(Me.CMBDYEINGFILTER)
        Me.BlendPanel1.Controls.Add(Me.LBLTOTALTAKA)
        Me.BlendPanel1.Controls.Add(Me.Label1)
        Me.BlendPanel1.Controls.Add(Me.LBLTOTALMTRS)
        Me.BlendPanel1.Controls.Add(Me.TXTOPRETNO)
        Me.BlendPanel1.Controls.Add(Me.LBLTOTAL)
        Me.BlendPanel1.Controls.Add(Me.Panel1)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1284, 586)
        Me.BlendPanel1.TabIndex = 0
        '
        'TXTADD
        '
        Me.TXTADD.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTADD.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTADD.Location = New System.Drawing.Point(672, 12)
        Me.TXTADD.Name = "TXTADD"
        Me.TXTADD.Size = New System.Drawing.Size(29, 21)
        Me.TXTADD.TabIndex = 807
        Me.TXTADD.TabStop = False
        Me.TXTADD.Visible = False
        '
        'cmbcode
        '
        Me.cmbcode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbcode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbcode.FormattingEnabled = True
        Me.cmbcode.Location = New System.Drawing.Point(707, 9)
        Me.cmbcode.Name = "cmbcode"
        Me.cmbcode.Size = New System.Drawing.Size(25, 22)
        Me.cmbcode.TabIndex = 717
        Me.cmbcode.Visible = False
        '
        'CMBDYEINGFILTER
        '
        Me.CMBDYEINGFILTER.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBDYEINGFILTER.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBDYEINGFILTER.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBDYEINGFILTER.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBDYEINGFILTER.FormattingEnabled = True
        Me.CMBDYEINGFILTER.Location = New System.Drawing.Point(111, 20)
        Me.CMBDYEINGFILTER.Name = "CMBDYEINGFILTER"
        Me.CMBDYEINGFILTER.Size = New System.Drawing.Size(230, 23)
        Me.CMBDYEINGFILTER.TabIndex = 0
        '
        'LBLTOTALTAKA
        '
        Me.LBLTOTALTAKA.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALTAKA.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALTAKA.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALTAKA.Location = New System.Drawing.Point(712, 554)
        Me.LBLTOTALTAKA.Name = "LBLTOTALTAKA"
        Me.LBLTOTALTAKA.Size = New System.Drawing.Size(60, 15)
        Me.LBLTOTALTAKA.TabIndex = 833
        Me.LBLTOTALTAKA.Text = "0"
        Me.LBLTOTALTAKA.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(29, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 14)
        Me.Label1.TabIndex = 716
        Me.Label1.Text = "Dyeing Name"
        '
        'LBLTOTALMTRS
        '
        Me.LBLTOTALMTRS.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALMTRS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALMTRS.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALMTRS.Location = New System.Drawing.Point(807, 554)
        Me.LBLTOTALMTRS.Name = "LBLTOTALMTRS"
        Me.LBLTOTALMTRS.Size = New System.Drawing.Size(65, 15)
        Me.LBLTOTALMTRS.TabIndex = 832
        Me.LBLTOTALMTRS.Text = "0.00"
        Me.LBLTOTALMTRS.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TXTOPRETNO
        '
        Me.TXTOPRETNO.BackColor = System.Drawing.Color.White
        Me.TXTOPRETNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTOPRETNO.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TXTOPRETNO.Location = New System.Drawing.Point(738, 9)
        Me.TXTOPRETNO.Name = "TXTOPRETNO"
        Me.TXTOPRETNO.ReadOnly = True
        Me.TXTOPRETNO.Size = New System.Drawing.Size(30, 23)
        Me.TXTOPRETNO.TabIndex = 715
        Me.TXTOPRETNO.Text = " "
        Me.TXTOPRETNO.Visible = False
        '
        'LBLTOTAL
        '
        Me.LBLTOTAL.AutoSize = True
        Me.LBLTOTAL.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTAL.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTAL.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTAL.Location = New System.Drawing.Point(669, 554)
        Me.LBLTOTAL.Name = "LBLTOTAL"
        Me.LBLTOTAL.Size = New System.Drawing.Size(33, 15)
        Me.LBLTOTAL.TabIndex = 831
        Me.LBLTOTAL.Text = "Total"
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel1.Controls.Add(Me.CMBNAME)
        Me.Panel1.Controls.Add(Me.CMBOURGODOWN)
        Me.Panel1.Controls.Add(Me.TXTRETDONO)
        Me.Panel1.Controls.Add(Me.TXTRETTAKA)
        Me.Panel1.Controls.Add(Me.TXTRETMTRS)
        Me.Panel1.Controls.Add(Me.CMBTRANS)
        Me.Panel1.Controls.Add(Me.DTRETDODATE)
        Me.Panel1.Controls.Add(Me.CMBDYEING)
        Me.Panel1.Controls.Add(Me.DTINVDATE)
        Me.Panel1.Controls.Add(Me.TXTGRNO)
        Me.Panel1.Controls.Add(Me.TXTINVNO)
        Me.Panel1.Controls.Add(Me.TXTLOTNO)
        Me.Panel1.Controls.Add(Me.CMBGREYQUALITY)
        Me.Panel1.Controls.Add(Me.GRIDSTOCK)
        Me.Panel1.Location = New System.Drawing.Point(22, 49)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1250, 492)
        Me.Panel1.TabIndex = 1
        '
        'CMBOURGODOWN
        '
        Me.CMBOURGODOWN.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBOURGODOWN.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBOURGODOWN.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBOURGODOWN.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBOURGODOWN.FormattingEnabled = True
        Me.CMBOURGODOWN.Location = New System.Drawing.Point(3, 3)
        Me.CMBOURGODOWN.Name = "CMBOURGODOWN"
        Me.CMBOURGODOWN.Size = New System.Drawing.Size(100, 23)
        Me.CMBOURGODOWN.TabIndex = 0
        '
        'TXTRETDONO
        '
        Me.TXTRETDONO.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTRETDONO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTRETDONO.Location = New System.Drawing.Point(1153, 3)
        Me.TXTRETDONO.MaxLength = 10
        Me.TXTRETDONO.Name = "TXTRETDONO"
        Me.TXTRETDONO.Size = New System.Drawing.Size(80, 23)
        Me.TXTRETDONO.TabIndex = 10
        Me.TXTRETDONO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTRETTAKA
        '
        Me.TXTRETTAKA.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTRETTAKA.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTRETTAKA.Location = New System.Drawing.Point(1013, 3)
        Me.TXTRETTAKA.MaxLength = 10
        Me.TXTRETTAKA.Name = "TXTRETTAKA"
        Me.TXTRETTAKA.Size = New System.Drawing.Size(70, 23)
        Me.TXTRETTAKA.TabIndex = 8
        Me.TXTRETTAKA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTRETMTRS
        '
        Me.TXTRETMTRS.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTRETMTRS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTRETMTRS.Location = New System.Drawing.Point(1083, 3)
        Me.TXTRETMTRS.MaxLength = 10
        Me.TXTRETMTRS.Name = "TXTRETMTRS"
        Me.TXTRETMTRS.Size = New System.Drawing.Size(70, 23)
        Me.TXTRETMTRS.TabIndex = 9
        Me.TXTRETMTRS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CMBTRANS
        '
        Me.CMBTRANS.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBTRANS.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBTRANS.BackColor = System.Drawing.Color.White
        Me.CMBTRANS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBTRANS.FormattingEnabled = True
        Me.CMBTRANS.Location = New System.Drawing.Point(1303, 3)
        Me.CMBTRANS.Name = "CMBTRANS"
        Me.CMBTRANS.Size = New System.Drawing.Size(120, 23)
        Me.CMBTRANS.TabIndex = 12
        '
        'DTRETDODATE
        '
        Me.DTRETDODATE.AsciiOnly = True
        Me.DTRETDODATE.BackColor = System.Drawing.Color.LemonChiffon
        Me.DTRETDODATE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTRETDODATE.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite
        Me.DTRETDODATE.Location = New System.Drawing.Point(1233, 3)
        Me.DTRETDODATE.Mask = "00/00/0000"
        Me.DTRETDODATE.Name = "DTRETDODATE"
        Me.DTRETDODATE.Size = New System.Drawing.Size(70, 23)
        Me.DTRETDODATE.TabIndex = 11
        Me.DTRETDODATE.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals
        Me.DTRETDODATE.ValidatingType = GetType(Date)
        '
        'CMBDYEING
        '
        Me.CMBDYEING.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBDYEING.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBDYEING.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBDYEING.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBDYEING.FormattingEnabled = True
        Me.CMBDYEING.Location = New System.Drawing.Point(513, 3)
        Me.CMBDYEING.Name = "CMBDYEING"
        Me.CMBDYEING.Size = New System.Drawing.Size(200, 23)
        Me.CMBDYEING.TabIndex = 5
        '
        'DTINVDATE
        '
        Me.DTINVDATE.AsciiOnly = True
        Me.DTINVDATE.BackColor = System.Drawing.Color.LemonChiffon
        Me.DTINVDATE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTINVDATE.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite
        Me.DTINVDATE.Location = New System.Drawing.Point(243, 3)
        Me.DTINVDATE.Mask = "00/00/0000"
        Me.DTINVDATE.Name = "DTINVDATE"
        Me.DTINVDATE.Size = New System.Drawing.Size(70, 23)
        Me.DTINVDATE.TabIndex = 3
        Me.DTINVDATE.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals
        Me.DTINVDATE.ValidatingType = GetType(Date)
        '
        'TXTGRNO
        '
        Me.TXTGRNO.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTGRNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTGRNO.Location = New System.Drawing.Point(103, 3)
        Me.TXTGRNO.MaxLength = 10
        Me.TXTGRNO.Name = "TXTGRNO"
        Me.TXTGRNO.Size = New System.Drawing.Size(70, 23)
        Me.TXTGRNO.TabIndex = 1
        Me.TXTGRNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTINVNO
        '
        Me.TXTINVNO.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTINVNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTINVNO.Location = New System.Drawing.Point(173, 3)
        Me.TXTINVNO.MaxLength = 10
        Me.TXTINVNO.Name = "TXTINVNO"
        Me.TXTINVNO.Size = New System.Drawing.Size(70, 23)
        Me.TXTINVNO.TabIndex = 2
        Me.TXTINVNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTLOTNO
        '
        Me.TXTLOTNO.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTLOTNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTLOTNO.Location = New System.Drawing.Point(713, 3)
        Me.TXTLOTNO.MaxLength = 10
        Me.TXTLOTNO.Name = "TXTLOTNO"
        Me.TXTLOTNO.Size = New System.Drawing.Size(100, 23)
        Me.TXTLOTNO.TabIndex = 6
        Me.TXTLOTNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CMBGREYQUALITY
        '
        Me.CMBGREYQUALITY.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBGREYQUALITY.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBGREYQUALITY.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBGREYQUALITY.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBGREYQUALITY.FormattingEnabled = True
        Me.CMBGREYQUALITY.Location = New System.Drawing.Point(813, 3)
        Me.CMBGREYQUALITY.Name = "CMBGREYQUALITY"
        Me.CMBGREYQUALITY.Size = New System.Drawing.Size(200, 23)
        Me.CMBGREYQUALITY.TabIndex = 7
        '
        'GRIDSTOCK
        '
        Me.GRIDSTOCK.AllowUserToAddRows = False
        Me.GRIDSTOCK.AllowUserToDeleteRows = False
        Me.GRIDSTOCK.AllowUserToResizeColumns = False
        Me.GRIDSTOCK.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(248, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black
        Me.GRIDSTOCK.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.GRIDSTOCK.BackgroundColor = System.Drawing.Color.White
        Me.GRIDSTOCK.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GRIDSTOCK.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.GRIDSTOCK.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.GRIDSTOCK.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GRIDSTOCK.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GOPRETNO, Me.GGODOWN, Me.GOPGRNO, Me.GOPINVNO, Me.GOPDATE, Me.GNAME, Me.GOPDYEING, Me.GOPLOTNO, Me.GOPGREYQUALITY, Me.GOPRETTAKA, Me.GOPRETMTRS, Me.GOPRETDO, Me.GOPRETDATE, Me.GOPTRANSPORT})
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDSTOCK.DefaultCellStyle = DataGridViewCellStyle10
        Me.GRIDSTOCK.GridColor = System.Drawing.SystemColors.Control
        Me.GRIDSTOCK.Location = New System.Drawing.Point(3, 27)
        Me.GRIDSTOCK.MultiSelect = False
        Me.GRIDSTOCK.Name = "GRIDSTOCK"
        Me.GRIDSTOCK.ReadOnly = True
        Me.GRIDSTOCK.RowHeadersVisible = False
        Me.GRIDSTOCK.RowHeadersWidth = 30
        Me.GRIDSTOCK.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.Black
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.White
        Me.GRIDSTOCK.RowsDefaultCellStyle = DataGridViewCellStyle11
        Me.GRIDSTOCK.RowTemplate.Height = 20
        Me.GRIDSTOCK.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDSTOCK.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.GRIDSTOCK.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.GRIDSTOCK.Size = New System.Drawing.Size(1459, 448)
        Me.GRIDSTOCK.TabIndex = 7
        Me.GRIDSTOCK.TabStop = False
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.FlatAppearance.BorderSize = 0
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.Black
        Me.cmdexit.Location = New System.Drawing.Point(556, 547)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 28)
        Me.cmdexit.TabIndex = 2
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'EP
        '
        Me.EP.BlinkRate = 0
        Me.EP.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.EP.ContainerControl = Me
        '
        'GOPRETNO
        '
        Me.GOPRETNO.HeaderText = "Sr."
        Me.GOPRETNO.Name = "GOPRETNO"
        Me.GOPRETNO.ReadOnly = True
        Me.GOPRETNO.Visible = False
        '
        'GGODOWN
        '
        Me.GGODOWN.HeaderText = "Godown"
        Me.GGODOWN.Name = "GGODOWN"
        Me.GGODOWN.ReadOnly = True
        Me.GGODOWN.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GGODOWN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GOPGRNO
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GOPGRNO.DefaultCellStyle = DataGridViewCellStyle3
        Me.GOPGRNO.HeaderText = "GR. No."
        Me.GOPGRNO.Name = "GOPGRNO"
        Me.GOPGRNO.ReadOnly = True
        Me.GOPGRNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GOPGRNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GOPGRNO.Width = 70
        '
        'GOPINVNO
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GOPINVNO.DefaultCellStyle = DataGridViewCellStyle4
        Me.GOPINVNO.HeaderText = "Inv No"
        Me.GOPINVNO.Name = "GOPINVNO"
        Me.GOPINVNO.ReadOnly = True
        Me.GOPINVNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GOPINVNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GOPINVNO.Width = 70
        '
        'GOPDATE
        '
        Me.GOPDATE.HeaderText = "Inv. Date"
        Me.GOPDATE.Name = "GOPDATE"
        Me.GOPDATE.ReadOnly = True
        Me.GOPDATE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GOPDATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GOPDATE.Width = 70
        '
        'GNAME
        '
        Me.GNAME.HeaderText = "Name"
        Me.GNAME.Name = "GNAME"
        Me.GNAME.ReadOnly = True
        Me.GNAME.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GNAME.Width = 200
        '
        'GOPDYEING
        '
        Me.GOPDYEING.HeaderText = "Dyeing Name"
        Me.GOPDYEING.Name = "GOPDYEING"
        Me.GOPDYEING.ReadOnly = True
        Me.GOPDYEING.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GOPDYEING.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GOPDYEING.Width = 200
        '
        'GOPLOTNO
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GOPLOTNO.DefaultCellStyle = DataGridViewCellStyle5
        Me.GOPLOTNO.HeaderText = "Lot No."
        Me.GOPLOTNO.Name = "GOPLOTNO"
        Me.GOPLOTNO.ReadOnly = True
        Me.GOPLOTNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GOPLOTNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GOPGREYQUALITY
        '
        Me.GOPGREYQUALITY.HeaderText = "Grey Quality"
        Me.GOPGREYQUALITY.Name = "GOPGREYQUALITY"
        Me.GOPGREYQUALITY.ReadOnly = True
        Me.GOPGREYQUALITY.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GOPGREYQUALITY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GOPGREYQUALITY.Width = 200
        '
        'GOPRETTAKA
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GOPRETTAKA.DefaultCellStyle = DataGridViewCellStyle6
        Me.GOPRETTAKA.HeaderText = "Ret. Taka"
        Me.GOPRETTAKA.Name = "GOPRETTAKA"
        Me.GOPRETTAKA.ReadOnly = True
        Me.GOPRETTAKA.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GOPRETTAKA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GOPRETTAKA.Width = 70
        '
        'GOPRETMTRS
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GOPRETMTRS.DefaultCellStyle = DataGridViewCellStyle7
        Me.GOPRETMTRS.HeaderText = "Ret. Mtrs."
        Me.GOPRETMTRS.Name = "GOPRETMTRS"
        Me.GOPRETMTRS.ReadOnly = True
        Me.GOPRETMTRS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GOPRETMTRS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GOPRETMTRS.Width = 70
        '
        'GOPRETDO
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GOPRETDO.DefaultCellStyle = DataGridViewCellStyle8
        Me.GOPRETDO.HeaderText = "Ret. DO. No."
        Me.GOPRETDO.Name = "GOPRETDO"
        Me.GOPRETDO.ReadOnly = True
        Me.GOPRETDO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GOPRETDO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GOPRETDO.Width = 80
        '
        'GOPRETDATE
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GOPRETDATE.DefaultCellStyle = DataGridViewCellStyle9
        Me.GOPRETDATE.HeaderText = "Ret. Date"
        Me.GOPRETDATE.Name = "GOPRETDATE"
        Me.GOPRETDATE.ReadOnly = True
        Me.GOPRETDATE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GOPRETDATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GOPRETDATE.Width = 70
        '
        'GOPTRANSPORT
        '
        Me.GOPTRANSPORT.HeaderText = "Transport"
        Me.GOPTRANSPORT.Name = "GOPTRANSPORT"
        Me.GOPTRANSPORT.ReadOnly = True
        Me.GOPTRANSPORT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GOPTRANSPORT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GOPTRANSPORT.Width = 120
        '
        'CMBNAME
        '
        Me.CMBNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBNAME.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBNAME.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBNAME.FormattingEnabled = True
        Me.CMBNAME.Location = New System.Drawing.Point(313, 3)
        Me.CMBNAME.Name = "CMBNAME"
        Me.CMBNAME.Size = New System.Drawing.Size(200, 23)
        Me.CMBNAME.TabIndex = 4
        '
        'OpeningPendingReturnDate
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1284, 586)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "OpeningPendingReturnDate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Opening Pending Return Date"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.GRIDSTOCK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents TXTADD As System.Windows.Forms.TextBox
    Friend WithEvents cmbcode As System.Windows.Forms.ComboBox
    Friend WithEvents CMBDYEINGFILTER As System.Windows.Forms.ComboBox
    Friend WithEvents LBLTOTALTAKA As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LBLTOTALMTRS As System.Windows.Forms.Label
    Friend WithEvents TXTOPRETNO As System.Windows.Forms.TextBox
    Friend WithEvents LBLTOTAL As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents TXTLOTNO As System.Windows.Forms.TextBox
    Friend WithEvents CMBGREYQUALITY As System.Windows.Forms.ComboBox
    Friend WithEvents GRIDSTOCK As System.Windows.Forms.DataGridView
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents TXTGRNO As System.Windows.Forms.TextBox
    Friend WithEvents TXTINVNO As System.Windows.Forms.TextBox
    Friend WithEvents CMBTRANS As System.Windows.Forms.ComboBox
    Friend WithEvents DTRETDODATE As System.Windows.Forms.MaskedTextBox
    Friend WithEvents CMBDYEING As System.Windows.Forms.ComboBox
    Friend WithEvents DTINVDATE As System.Windows.Forms.MaskedTextBox
    Friend WithEvents TXTRETDONO As System.Windows.Forms.TextBox
    Friend WithEvents TXTRETTAKA As System.Windows.Forms.TextBox
    Friend WithEvents TXTRETMTRS As System.Windows.Forms.TextBox
    Friend WithEvents EP As System.Windows.Forms.ErrorProvider
    Friend WithEvents CMBOURGODOWN As System.Windows.Forms.ComboBox
    Friend WithEvents CMBNAME As System.Windows.Forms.ComboBox
    Friend WithEvents GOPRETNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GGODOWN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GOPGRNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GOPINVNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GOPDATE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GNAME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GOPDYEING As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GOPLOTNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GOPGREYQUALITY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GOPRETTAKA As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GOPRETMTRS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GOPRETDO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GOPRETDATE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GOPTRANSPORT As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
