<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OpeningStockBeam
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
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.LBLTOTALBEAMS = New System.Windows.Forms.Label()
        Me.TXTADD = New System.Windows.Forms.TextBox()
        Me.cmbcode = New System.Windows.Forms.ComboBox()
        Me.LBLTOTALWTCUT = New System.Windows.Forms.Label()
        Me.CMBBEAMFILTER = New System.Windows.Forms.ComboBox()
        Me.LBLTOTALCUT = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LBLTOTALWT = New System.Windows.Forms.Label()
        Me.TXTOPBEAMSTOCKNO = New System.Windows.Forms.TextBox()
        Me.LBLTOTAL = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CMBSIZER = New System.Windows.Forms.ComboBox()
        Me.CMBOURGODOWN = New System.Windows.Forms.ComboBox()
        Me.TXTREMARKS = New System.Windows.Forms.TextBox()
        Me.TXTWTCUT = New System.Windows.Forms.TextBox()
        Me.TXTWT = New System.Windows.Forms.TextBox()
        Me.TXTCUT = New System.Windows.Forms.TextBox()
        Me.TXTTAPLINE = New System.Windows.Forms.TextBox()
        Me.TXTENDS = New System.Windows.Forms.TextBox()
        Me.TXTBEAMNO = New System.Windows.Forms.TextBox()
        Me.CMBBEAM = New System.Windows.Forms.ComboBox()
        Me.GRIDSTOCK = New System.Windows.Forms.DataGridView()
        Me.GOPBEAMSTOCKNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GSIZER = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GGODOWN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GBEAM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GBEAMNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GENDS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GTAPLINE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GCUT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GWT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GWTCUT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GREMARKS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GDONE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
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
        Me.BlendPanel1.Controls.Add(Me.LBLTOTALBEAMS)
        Me.BlendPanel1.Controls.Add(Me.TXTADD)
        Me.BlendPanel1.Controls.Add(Me.cmbcode)
        Me.BlendPanel1.Controls.Add(Me.LBLTOTALWTCUT)
        Me.BlendPanel1.Controls.Add(Me.CMBBEAMFILTER)
        Me.BlendPanel1.Controls.Add(Me.LBLTOTALCUT)
        Me.BlendPanel1.Controls.Add(Me.Label1)
        Me.BlendPanel1.Controls.Add(Me.LBLTOTALWT)
        Me.BlendPanel1.Controls.Add(Me.TXTOPBEAMSTOCKNO)
        Me.BlendPanel1.Controls.Add(Me.LBLTOTAL)
        Me.BlendPanel1.Controls.Add(Me.Panel1)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1234, 591)
        Me.BlendPanel1.TabIndex = 0
        '
        'LBLTOTALBEAMS
        '
        Me.LBLTOTALBEAMS.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALBEAMS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALBEAMS.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALBEAMS.Location = New System.Drawing.Point(764, 566)
        Me.LBLTOTALBEAMS.Name = "LBLTOTALBEAMS"
        Me.LBLTOTALBEAMS.Size = New System.Drawing.Size(60, 15)
        Me.LBLTOTALBEAMS.TabIndex = 835
        Me.LBLTOTALBEAMS.Text = "0"
        Me.LBLTOTALBEAMS.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TXTADD
        '
        Me.TXTADD.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTADD.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTADD.Location = New System.Drawing.Point(712, 12)
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
        Me.cmbcode.Location = New System.Drawing.Point(747, 9)
        Me.cmbcode.Name = "cmbcode"
        Me.cmbcode.Size = New System.Drawing.Size(25, 22)
        Me.cmbcode.TabIndex = 717
        Me.cmbcode.Visible = False
        '
        'LBLTOTALWTCUT
        '
        Me.LBLTOTALWTCUT.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALWTCUT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALWTCUT.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALWTCUT.Location = New System.Drawing.Point(999, 566)
        Me.LBLTOTALWTCUT.Name = "LBLTOTALWTCUT"
        Me.LBLTOTALWTCUT.Size = New System.Drawing.Size(65, 15)
        Me.LBLTOTALWTCUT.TabIndex = 834
        Me.LBLTOTALWTCUT.Text = "0.000"
        Me.LBLTOTALWTCUT.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CMBBEAMFILTER
        '
        Me.CMBBEAMFILTER.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBBEAMFILTER.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBBEAMFILTER.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBBEAMFILTER.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBBEAMFILTER.FormattingEnabled = True
        Me.CMBBEAMFILTER.Location = New System.Drawing.Point(87, 15)
        Me.CMBBEAMFILTER.Name = "CMBBEAMFILTER"
        Me.CMBBEAMFILTER.Size = New System.Drawing.Size(200, 23)
        Me.CMBBEAMFILTER.TabIndex = 0
        '
        'LBLTOTALCUT
        '
        Me.LBLTOTALCUT.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALCUT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALCUT.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALCUT.Location = New System.Drawing.Point(844, 566)
        Me.LBLTOTALCUT.Name = "LBLTOTALCUT"
        Me.LBLTOTALCUT.Size = New System.Drawing.Size(60, 15)
        Me.LBLTOTALCUT.TabIndex = 833
        Me.LBLTOTALCUT.Text = "0"
        Me.LBLTOTALCUT.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(12, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 14)
        Me.Label1.TabIndex = 716
        Me.Label1.Text = "Beam Name"
        '
        'LBLTOTALWT
        '
        Me.LBLTOTALWT.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALWT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALWT.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALWT.Location = New System.Drawing.Point(919, 566)
        Me.LBLTOTALWT.Name = "LBLTOTALWT"
        Me.LBLTOTALWT.Size = New System.Drawing.Size(65, 15)
        Me.LBLTOTALWT.TabIndex = 832
        Me.LBLTOTALWT.Text = "0.000"
        Me.LBLTOTALWT.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TXTOPBEAMSTOCKNO
        '
        Me.TXTOPBEAMSTOCKNO.BackColor = System.Drawing.Color.White
        Me.TXTOPBEAMSTOCKNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTOPBEAMSTOCKNO.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TXTOPBEAMSTOCKNO.Location = New System.Drawing.Point(778, 9)
        Me.TXTOPBEAMSTOCKNO.Name = "TXTOPBEAMSTOCKNO"
        Me.TXTOPBEAMSTOCKNO.ReadOnly = True
        Me.TXTOPBEAMSTOCKNO.Size = New System.Drawing.Size(30, 23)
        Me.TXTOPBEAMSTOCKNO.TabIndex = 715
        Me.TXTOPBEAMSTOCKNO.Text = " "
        Me.TXTOPBEAMSTOCKNO.Visible = False
        '
        'LBLTOTAL
        '
        Me.LBLTOTAL.AutoSize = True
        Me.LBLTOTAL.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTAL.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTAL.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTAL.Location = New System.Drawing.Point(709, 566)
        Me.LBLTOTAL.Name = "LBLTOTAL"
        Me.LBLTOTAL.Size = New System.Drawing.Size(33, 15)
        Me.LBLTOTAL.TabIndex = 831
        Me.LBLTOTAL.Text = "Total"
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel1.Controls.Add(Me.CMBSIZER)
        Me.Panel1.Controls.Add(Me.CMBOURGODOWN)
        Me.Panel1.Controls.Add(Me.TXTREMARKS)
        Me.Panel1.Controls.Add(Me.TXTWTCUT)
        Me.Panel1.Controls.Add(Me.TXTWT)
        Me.Panel1.Controls.Add(Me.TXTCUT)
        Me.Panel1.Controls.Add(Me.TXTTAPLINE)
        Me.Panel1.Controls.Add(Me.TXTENDS)
        Me.Panel1.Controls.Add(Me.TXTBEAMNO)
        Me.Panel1.Controls.Add(Me.CMBBEAM)
        Me.Panel1.Controls.Add(Me.GRIDSTOCK)
        Me.Panel1.Location = New System.Drawing.Point(11, 49)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1211, 503)
        Me.Panel1.TabIndex = 1
        '
        'CMBSIZER
        '
        Me.CMBSIZER.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBSIZER.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBSIZER.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBSIZER.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBSIZER.FormattingEnabled = True
        Me.CMBSIZER.Location = New System.Drawing.Point(3, 3)
        Me.CMBSIZER.Name = "CMBSIZER"
        Me.CMBSIZER.Size = New System.Drawing.Size(200, 23)
        Me.CMBSIZER.TabIndex = 0
        '
        'CMBOURGODOWN
        '
        Me.CMBOURGODOWN.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBOURGODOWN.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBOURGODOWN.BackColor = System.Drawing.Color.White
        Me.CMBOURGODOWN.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBOURGODOWN.FormattingEnabled = True
        Me.CMBOURGODOWN.Location = New System.Drawing.Point(203, 3)
        Me.CMBOURGODOWN.Name = "CMBOURGODOWN"
        Me.CMBOURGODOWN.Size = New System.Drawing.Size(150, 23)
        Me.CMBOURGODOWN.TabIndex = 1
        '
        'TXTREMARKS
        '
        Me.TXTREMARKS.BackColor = System.Drawing.Color.White
        Me.TXTREMARKS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTREMARKS.Location = New System.Drawing.Point(1053, 3)
        Me.TXTREMARKS.MaxLength = 200
        Me.TXTREMARKS.Name = "TXTREMARKS"
        Me.TXTREMARKS.Size = New System.Drawing.Size(200, 23)
        Me.TXTREMARKS.TabIndex = 7
        '
        'TXTWTCUT
        '
        Me.TXTWTCUT.BackColor = System.Drawing.Color.Linen
        Me.TXTWTCUT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTWTCUT.Location = New System.Drawing.Point(973, 3)
        Me.TXTWTCUT.Name = "TXTWTCUT"
        Me.TXTWTCUT.ReadOnly = True
        Me.TXTWTCUT.Size = New System.Drawing.Size(80, 23)
        Me.TXTWTCUT.TabIndex = 7
        Me.TXTWTCUT.TabStop = False
        Me.TXTWTCUT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTWT
        '
        Me.TXTWT.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTWT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTWT.Location = New System.Drawing.Point(893, 3)
        Me.TXTWT.MaxLength = 10
        Me.TXTWT.Name = "TXTWT"
        Me.TXTWT.Size = New System.Drawing.Size(80, 23)
        Me.TXTWT.TabIndex = 6
        Me.TXTWT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTCUT
        '
        Me.TXTCUT.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTCUT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTCUT.Location = New System.Drawing.Point(813, 3)
        Me.TXTCUT.MaxLength = 10
        Me.TXTCUT.Name = "TXTCUT"
        Me.TXTCUT.Size = New System.Drawing.Size(80, 23)
        Me.TXTCUT.TabIndex = 5
        Me.TXTCUT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTTAPLINE
        '
        Me.TXTTAPLINE.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTTAPLINE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTTAPLINE.Location = New System.Drawing.Point(733, 3)
        Me.TXTTAPLINE.Name = "TXTTAPLINE"
        Me.TXTTAPLINE.Size = New System.Drawing.Size(80, 23)
        Me.TXTTAPLINE.TabIndex = 4
        Me.TXTTAPLINE.TabStop = False
        Me.TXTTAPLINE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTENDS
        '
        Me.TXTENDS.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTENDS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTENDS.Location = New System.Drawing.Point(653, 3)
        Me.TXTENDS.Name = "TXTENDS"
        Me.TXTENDS.Size = New System.Drawing.Size(80, 23)
        Me.TXTENDS.TabIndex = 4
        Me.TXTENDS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTBEAMNO
        '
        Me.TXTBEAMNO.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTBEAMNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTBEAMNO.Location = New System.Drawing.Point(553, 3)
        Me.TXTBEAMNO.MaxLength = 10
        Me.TXTBEAMNO.Name = "TXTBEAMNO"
        Me.TXTBEAMNO.Size = New System.Drawing.Size(100, 23)
        Me.TXTBEAMNO.TabIndex = 3
        '
        'CMBBEAM
        '
        Me.CMBBEAM.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBBEAM.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBBEAM.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBBEAM.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBBEAM.FormattingEnabled = True
        Me.CMBBEAM.Location = New System.Drawing.Point(353, 3)
        Me.CMBBEAM.Name = "CMBBEAM"
        Me.CMBBEAM.Size = New System.Drawing.Size(200, 23)
        Me.CMBBEAM.TabIndex = 2
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
        Me.GRIDSTOCK.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GOPBEAMSTOCKNO, Me.GSIZER, Me.GGODOWN, Me.GBEAM, Me.GBEAMNO, Me.GENDS, Me.GTAPLINE, Me.GCUT, Me.GWT, Me.GWTCUT, Me.GREMARKS, Me.GDONE})
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDSTOCK.DefaultCellStyle = DataGridViewCellStyle8
        Me.GRIDSTOCK.GridColor = System.Drawing.SystemColors.Control
        Me.GRIDSTOCK.Location = New System.Drawing.Point(3, 27)
        Me.GRIDSTOCK.MultiSelect = False
        Me.GRIDSTOCK.Name = "GRIDSTOCK"
        Me.GRIDSTOCK.ReadOnly = True
        Me.GRIDSTOCK.RowHeadersVisible = False
        Me.GRIDSTOCK.RowHeadersWidth = 30
        Me.GRIDSTOCK.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.Black
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.White
        Me.GRIDSTOCK.RowsDefaultCellStyle = DataGridViewCellStyle9
        Me.GRIDSTOCK.RowTemplate.Height = 20
        Me.GRIDSTOCK.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDSTOCK.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.GRIDSTOCK.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.GRIDSTOCK.Size = New System.Drawing.Size(1284, 456)
        Me.GRIDSTOCK.TabIndex = 8
        Me.GRIDSTOCK.TabStop = False
        '
        'GOPBEAMSTOCKNO
        '
        Me.GOPBEAMSTOCKNO.HeaderText = "Sr."
        Me.GOPBEAMSTOCKNO.Name = "GOPBEAMSTOCKNO"
        Me.GOPBEAMSTOCKNO.ReadOnly = True
        Me.GOPBEAMSTOCKNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GOPBEAMSTOCKNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GOPBEAMSTOCKNO.Visible = False
        Me.GOPBEAMSTOCKNO.Width = 40
        '
        'GSIZER
        '
        Me.GSIZER.HeaderText = "Sizer Name"
        Me.GSIZER.Name = "GSIZER"
        Me.GSIZER.ReadOnly = True
        Me.GSIZER.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GSIZER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GSIZER.Width = 200
        '
        'GGODOWN
        '
        Me.GGODOWN.HeaderText = "Godown"
        Me.GGODOWN.Name = "GGODOWN"
        Me.GGODOWN.ReadOnly = True
        Me.GGODOWN.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GGODOWN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GGODOWN.Width = 150
        '
        'GBEAM
        '
        Me.GBEAM.HeaderText = "Beam Name"
        Me.GBEAM.Name = "GBEAM"
        Me.GBEAM.ReadOnly = True
        Me.GBEAM.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GBEAM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GBEAM.Width = 200
        '
        'GBEAMNO
        '
        Me.GBEAMNO.HeaderText = "Beam No."
        Me.GBEAMNO.Name = "GBEAMNO"
        Me.GBEAMNO.ReadOnly = True
        Me.GBEAMNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GBEAMNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GENDS
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GENDS.DefaultCellStyle = DataGridViewCellStyle3
        Me.GENDS.HeaderText = "ENDS"
        Me.GENDS.Name = "GENDS"
        Me.GENDS.ReadOnly = True
        Me.GENDS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GENDS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GENDS.Width = 80
        '
        'GTAPLINE
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GTAPLINE.DefaultCellStyle = DataGridViewCellStyle4
        Me.GTAPLINE.HeaderText = "Tapline"
        Me.GTAPLINE.Name = "GTAPLINE"
        Me.GTAPLINE.ReadOnly = True
        Me.GTAPLINE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GTAPLINE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GTAPLINE.Width = 80
        '
        'GCUT
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight
        Me.GCUT.DefaultCellStyle = DataGridViewCellStyle5
        Me.GCUT.HeaderText = "Cut"
        Me.GCUT.Name = "GCUT"
        Me.GCUT.ReadOnly = True
        Me.GCUT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GCUT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GCUT.Width = 80
        '
        'GWT
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GWT.DefaultCellStyle = DataGridViewCellStyle6
        Me.GWT.HeaderText = "Weight"
        Me.GWT.Name = "GWT"
        Me.GWT.ReadOnly = True
        Me.GWT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GWT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GWT.Width = 80
        '
        'GWTCUT
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GWTCUT.DefaultCellStyle = DataGridViewCellStyle7
        Me.GWTCUT.HeaderText = "Wt./Cut"
        Me.GWTCUT.Name = "GWTCUT"
        Me.GWTCUT.ReadOnly = True
        Me.GWTCUT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GWTCUT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GWTCUT.Width = 80
        '
        'GREMARKS
        '
        Me.GREMARKS.HeaderText = "Remarks"
        Me.GREMARKS.Name = "GREMARKS"
        Me.GREMARKS.ReadOnly = True
        Me.GREMARKS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GREMARKS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GREMARKS.Width = 200
        '
        'GDONE
        '
        Me.GDONE.HeaderText = "DONE"
        Me.GDONE.Name = "GDONE"
        Me.GDONE.ReadOnly = True
        Me.GDONE.Visible = False
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.FlatAppearance.BorderSize = 0
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.Black
        Me.cmdexit.Location = New System.Drawing.Point(511, 555)
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
        'OpeningStockBeam
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1234, 591)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "OpeningStockBeam"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Opening Stock Beam"
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
    Friend WithEvents CMBBEAMFILTER As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TXTOPBEAMSTOCKNO As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents CMBBEAM As System.Windows.Forms.ComboBox
    Friend WithEvents GRIDSTOCK As System.Windows.Forms.DataGridView
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents TXTBEAMNO As System.Windows.Forms.TextBox
    Friend WithEvents TXTWTCUT As System.Windows.Forms.TextBox
    Friend WithEvents TXTWT As System.Windows.Forms.TextBox
    Friend WithEvents TXTCUT As System.Windows.Forms.TextBox
    Friend WithEvents TXTTAPLINE As System.Windows.Forms.TextBox
    Friend WithEvents TXTENDS As System.Windows.Forms.TextBox
    Friend WithEvents TXTREMARKS As System.Windows.Forms.TextBox
    Friend WithEvents EP As System.Windows.Forms.ErrorProvider
    Friend WithEvents LBLTOTALWTCUT As System.Windows.Forms.Label
    Friend WithEvents LBLTOTALCUT As System.Windows.Forms.Label
    Friend WithEvents LBLTOTALWT As System.Windows.Forms.Label
    Friend WithEvents LBLTOTAL As System.Windows.Forms.Label
    Friend WithEvents CMBOURGODOWN As System.Windows.Forms.ComboBox
    Friend WithEvents CMBSIZER As System.Windows.Forms.ComboBox
    Friend WithEvents GOPBEAMSTOCKNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GSIZER As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GGODOWN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GBEAM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GBEAMNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GENDS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GTAPLINE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GCUT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GWT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GWTCUT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GREMARKS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GDONE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LBLTOTALBEAMS As System.Windows.Forms.Label
End Class
