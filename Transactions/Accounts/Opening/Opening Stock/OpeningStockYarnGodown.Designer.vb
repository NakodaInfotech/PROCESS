<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OpeningStockYarnGodown
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
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.LBLTOTALNALI = New System.Windows.Forms.Label()
        Me.LBLTOTALFIRKA = New System.Windows.Forms.Label()
        Me.LBLTOTALWINDING = New System.Windows.Forms.Label()
        Me.LBLTOTALFRESH = New System.Windows.Forms.Label()
        Me.LBLTOTALBAG = New System.Windows.Forms.Label()
        Me.TXTADD = New System.Windows.Forms.TextBox()
        Me.LBLTOTALWT = New System.Windows.Forms.Label()
        Me.cmbcode = New System.Windows.Forms.ComboBox()
        Me.LBLTOTAL = New System.Windows.Forms.Label()
        Me.CMBQUALITYFILTER = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TXTOPYARNSTOCKNO = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CMBCOLOR = New System.Windows.Forms.ComboBox()
        Me.gridstock = New System.Windows.Forms.DataGridView()
        Me.GOPYARNSTOCKNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GQUALITY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GSUPPLIERNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GMILLNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GGODOWN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GGODRECNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GGODRECDATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GTRANSPORT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GLOTNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GCOLOR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GBAGS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GWT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GFRESH = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GWINDING = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GFIRKA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GNALI = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GLRNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GLRDATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GREMARKS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GOUTBAGS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GOUTWT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GOUTFRESH = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GOUTWINDING = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GOUTFIRKA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GOUTNALI = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TXTLOTNO = New System.Windows.Forms.TextBox()
        Me.TXTNALI = New System.Windows.Forms.TextBox()
        Me.TXTFIRKA = New System.Windows.Forms.TextBox()
        Me.TXTWINDING = New System.Windows.Forms.TextBox()
        Me.TXTFRESH = New System.Windows.Forms.TextBox()
        Me.DTLRDATE = New System.Windows.Forms.MaskedTextBox()
        Me.DTGODRECDATE = New System.Windows.Forms.MaskedTextBox()
        Me.CMBMILLNAME = New System.Windows.Forms.ComboBox()
        Me.txtbags = New System.Windows.Forms.TextBox()
        Me.TXTLRNO = New System.Windows.Forms.TextBox()
        Me.CMBQUALITY = New System.Windows.Forms.ComboBox()
        Me.TXTWT = New System.Windows.Forms.TextBox()
        Me.CMBTRANS = New System.Windows.Forms.ComboBox()
        Me.TXTGODRECNO = New System.Windows.Forms.TextBox()
        Me.TXTREMARKS = New System.Windows.Forms.TextBox()
        Me.CMBSUPPLIER = New System.Windows.Forms.ComboBox()
        Me.CMBOURGODOWN = New System.Windows.Forms.ComboBox()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.BlendPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.gridstock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.AutoSize = True
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.LBLTOTALNALI)
        Me.BlendPanel1.Controls.Add(Me.LBLTOTALFIRKA)
        Me.BlendPanel1.Controls.Add(Me.LBLTOTALWINDING)
        Me.BlendPanel1.Controls.Add(Me.LBLTOTALFRESH)
        Me.BlendPanel1.Controls.Add(Me.LBLTOTALBAG)
        Me.BlendPanel1.Controls.Add(Me.TXTADD)
        Me.BlendPanel1.Controls.Add(Me.LBLTOTALWT)
        Me.BlendPanel1.Controls.Add(Me.cmbcode)
        Me.BlendPanel1.Controls.Add(Me.LBLTOTAL)
        Me.BlendPanel1.Controls.Add(Me.CMBQUALITYFILTER)
        Me.BlendPanel1.Controls.Add(Me.Label1)
        Me.BlendPanel1.Controls.Add(Me.TXTOPYARNSTOCKNO)
        Me.BlendPanel1.Controls.Add(Me.Panel1)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1146, 596)
        Me.BlendPanel1.TabIndex = 0
        '
        'LBLTOTALNALI
        '
        Me.LBLTOTALNALI.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALNALI.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALNALI.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALNALI.Location = New System.Drawing.Point(1014, 568)
        Me.LBLTOTALNALI.Name = "LBLTOTALNALI"
        Me.LBLTOTALNALI.Size = New System.Drawing.Size(58, 15)
        Me.LBLTOTALNALI.TabIndex = 834
        Me.LBLTOTALNALI.Text = "0"
        Me.LBLTOTALNALI.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LBLTOTALFIRKA
        '
        Me.LBLTOTALFIRKA.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALFIRKA.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALFIRKA.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALFIRKA.Location = New System.Drawing.Point(959, 568)
        Me.LBLTOTALFIRKA.Name = "LBLTOTALFIRKA"
        Me.LBLTOTALFIRKA.Size = New System.Drawing.Size(58, 15)
        Me.LBLTOTALFIRKA.TabIndex = 833
        Me.LBLTOTALFIRKA.Text = "0"
        Me.LBLTOTALFIRKA.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LBLTOTALWINDING
        '
        Me.LBLTOTALWINDING.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALWINDING.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALWINDING.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALWINDING.Location = New System.Drawing.Point(889, 568)
        Me.LBLTOTALWINDING.Name = "LBLTOTALWINDING"
        Me.LBLTOTALWINDING.Size = New System.Drawing.Size(73, 15)
        Me.LBLTOTALWINDING.TabIndex = 832
        Me.LBLTOTALWINDING.Text = "0"
        Me.LBLTOTALWINDING.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LBLTOTALFRESH
        '
        Me.LBLTOTALFRESH.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALFRESH.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALFRESH.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALFRESH.Location = New System.Drawing.Point(834, 568)
        Me.LBLTOTALFRESH.Name = "LBLTOTALFRESH"
        Me.LBLTOTALFRESH.Size = New System.Drawing.Size(58, 15)
        Me.LBLTOTALFRESH.TabIndex = 831
        Me.LBLTOTALFRESH.Text = "0"
        Me.LBLTOTALFRESH.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LBLTOTALBAG
        '
        Me.LBLTOTALBAG.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALBAG.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALBAG.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALBAG.Location = New System.Drawing.Point(709, 568)
        Me.LBLTOTALBAG.Name = "LBLTOTALBAG"
        Me.LBLTOTALBAG.Size = New System.Drawing.Size(63, 15)
        Me.LBLTOTALBAG.TabIndex = 830
        Me.LBLTOTALBAG.Text = "0"
        Me.LBLTOTALBAG.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TXTADD
        '
        Me.TXTADD.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTADD.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTADD.Location = New System.Drawing.Point(937, 12)
        Me.TXTADD.Name = "TXTADD"
        Me.TXTADD.Size = New System.Drawing.Size(29, 21)
        Me.TXTADD.TabIndex = 807
        Me.TXTADD.TabStop = False
        Me.TXTADD.Visible = False
        '
        'LBLTOTALWT
        '
        Me.LBLTOTALWT.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALWT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALWT.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALWT.Location = New System.Drawing.Point(769, 568)
        Me.LBLTOTALWT.Name = "LBLTOTALWT"
        Me.LBLTOTALWT.Size = New System.Drawing.Size(68, 15)
        Me.LBLTOTALWT.TabIndex = 829
        Me.LBLTOTALWT.Text = "0.000"
        Me.LBLTOTALWT.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbcode
        '
        Me.cmbcode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbcode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbcode.FormattingEnabled = True
        Me.cmbcode.Location = New System.Drawing.Point(972, 11)
        Me.cmbcode.Name = "cmbcode"
        Me.cmbcode.Size = New System.Drawing.Size(25, 22)
        Me.cmbcode.TabIndex = 717
        Me.cmbcode.Visible = False
        '
        'LBLTOTAL
        '
        Me.LBLTOTAL.AutoSize = True
        Me.LBLTOTAL.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTAL.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTAL.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTAL.Location = New System.Drawing.Point(641, 568)
        Me.LBLTOTAL.Name = "LBLTOTAL"
        Me.LBLTOTAL.Size = New System.Drawing.Size(33, 15)
        Me.LBLTOTAL.TabIndex = 828
        Me.LBLTOTAL.Text = "Total"
        '
        'CMBQUALITYFILTER
        '
        Me.CMBQUALITYFILTER.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBQUALITYFILTER.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBQUALITYFILTER.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBQUALITYFILTER.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBQUALITYFILTER.FormattingEnabled = True
        Me.CMBQUALITYFILTER.Location = New System.Drawing.Point(102, 11)
        Me.CMBQUALITYFILTER.Name = "CMBQUALITYFILTER"
        Me.CMBQUALITYFILTER.Size = New System.Drawing.Size(155, 23)
        Me.CMBQUALITYFILTER.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(18, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 15)
        Me.Label1.TabIndex = 716
        Me.Label1.Text = "Select Quality"
        '
        'TXTOPYARNSTOCKNO
        '
        Me.TXTOPYARNSTOCKNO.BackColor = System.Drawing.Color.White
        Me.TXTOPYARNSTOCKNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTOPYARNSTOCKNO.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TXTOPYARNSTOCKNO.Location = New System.Drawing.Point(1003, 11)
        Me.TXTOPYARNSTOCKNO.Name = "TXTOPYARNSTOCKNO"
        Me.TXTOPYARNSTOCKNO.ReadOnly = True
        Me.TXTOPYARNSTOCKNO.Size = New System.Drawing.Size(30, 23)
        Me.TXTOPYARNSTOCKNO.TabIndex = 715
        Me.TXTOPYARNSTOCKNO.Text = " "
        Me.TXTOPYARNSTOCKNO.Visible = False
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel1.Controls.Add(Me.CMBCOLOR)
        Me.Panel1.Controls.Add(Me.gridstock)
        Me.Panel1.Controls.Add(Me.TXTLOTNO)
        Me.Panel1.Controls.Add(Me.TXTNALI)
        Me.Panel1.Controls.Add(Me.TXTFIRKA)
        Me.Panel1.Controls.Add(Me.TXTWINDING)
        Me.Panel1.Controls.Add(Me.TXTFRESH)
        Me.Panel1.Controls.Add(Me.DTLRDATE)
        Me.Panel1.Controls.Add(Me.DTGODRECDATE)
        Me.Panel1.Controls.Add(Me.CMBMILLNAME)
        Me.Panel1.Controls.Add(Me.txtbags)
        Me.Panel1.Controls.Add(Me.TXTLRNO)
        Me.Panel1.Controls.Add(Me.CMBQUALITY)
        Me.Panel1.Controls.Add(Me.TXTWT)
        Me.Panel1.Controls.Add(Me.CMBTRANS)
        Me.Panel1.Controls.Add(Me.TXTGODRECNO)
        Me.Panel1.Controls.Add(Me.TXTREMARKS)
        Me.Panel1.Controls.Add(Me.CMBSUPPLIER)
        Me.Panel1.Controls.Add(Me.CMBOURGODOWN)
        Me.Panel1.Location = New System.Drawing.Point(14, 38)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1122, 517)
        Me.Panel1.TabIndex = 1
        '
        'CMBCOLOR
        '
        Me.CMBCOLOR.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBCOLOR.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBCOLOR.BackColor = System.Drawing.Color.White
        Me.CMBCOLOR.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBCOLOR.FormattingEnabled = True
        Me.CMBCOLOR.Location = New System.Drawing.Point(998, 4)
        Me.CMBCOLOR.MaxDropDownItems = 14
        Me.CMBCOLOR.Name = "CMBCOLOR"
        Me.CMBCOLOR.Size = New System.Drawing.Size(100, 23)
        Me.CMBCOLOR.TabIndex = 8
        '
        'gridstock
        '
        Me.gridstock.AllowUserToAddRows = False
        Me.gridstock.AllowUserToDeleteRows = False
        Me.gridstock.AllowUserToResizeColumns = False
        Me.gridstock.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(248, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black
        Me.gridstock.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.gridstock.BackgroundColor = System.Drawing.Color.White
        Me.gridstock.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.gridstock.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.gridstock.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.gridstock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gridstock.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GOPYARNSTOCKNO, Me.GQUALITY, Me.GSUPPLIERNAME, Me.GMILLNAME, Me.GGODOWN, Me.GGODRECNO, Me.GGODRECDATE, Me.GTRANSPORT, Me.GLOTNO, Me.GCOLOR, Me.GBAGS, Me.GWT, Me.GFRESH, Me.GWINDING, Me.GFIRKA, Me.GNALI, Me.GLRNO, Me.GLRDATE, Me.GREMARKS, Me.GOUTBAGS, Me.GOUTWT, Me.GOUTFRESH, Me.GOUTWINDING, Me.GOUTFIRKA, Me.GOUTNALI})
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gridstock.DefaultCellStyle = DataGridViewCellStyle9
        Me.gridstock.GridColor = System.Drawing.SystemColors.Control
        Me.gridstock.Location = New System.Drawing.Point(3, 28)
        Me.gridstock.MultiSelect = False
        Me.gridstock.Name = "gridstock"
        Me.gridstock.ReadOnly = True
        Me.gridstock.RowHeadersVisible = False
        Me.gridstock.RowHeadersWidth = 30
        Me.gridstock.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.Black
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.White
        Me.gridstock.RowsDefaultCellStyle = DataGridViewCellStyle10
        Me.gridstock.RowTemplate.Height = 20
        Me.gridstock.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gridstock.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.gridstock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.gridstock.Size = New System.Drawing.Size(1759, 472)
        Me.gridstock.TabIndex = 16
        Me.gridstock.TabStop = False
        '
        'GOPYARNSTOCKNO
        '
        Me.GOPYARNSTOCKNO.HeaderText = "Sr."
        Me.GOPYARNSTOCKNO.Name = "GOPYARNSTOCKNO"
        Me.GOPYARNSTOCKNO.ReadOnly = True
        Me.GOPYARNSTOCKNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GOPYARNSTOCKNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GOPYARNSTOCKNO.Visible = False
        Me.GOPYARNSTOCKNO.Width = 40
        '
        'GQUALITY
        '
        Me.GQUALITY.HeaderText = "Quality"
        Me.GQUALITY.Name = "GQUALITY"
        Me.GQUALITY.ReadOnly = True
        Me.GQUALITY.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GQUALITY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GQUALITY.Width = 120
        '
        'GSUPPLIERNAME
        '
        Me.GSUPPLIERNAME.HeaderText = "Supplier Name"
        Me.GSUPPLIERNAME.Name = "GSUPPLIERNAME"
        Me.GSUPPLIERNAME.ReadOnly = True
        Me.GSUPPLIERNAME.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GSUPPLIERNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GSUPPLIERNAME.Width = 180
        '
        'GMILLNAME
        '
        Me.GMILLNAME.HeaderText = "Mill Name"
        Me.GMILLNAME.Name = "GMILLNAME"
        Me.GMILLNAME.ReadOnly = True
        Me.GMILLNAME.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GMILLNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GMILLNAME.Width = 180
        '
        'GGODOWN
        '
        Me.GGODOWN.HeaderText = "Godown"
        Me.GGODOWN.Name = "GGODOWN"
        Me.GGODOWN.ReadOnly = True
        Me.GGODOWN.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GGODOWN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GGODOWN.Width = 90
        '
        'GGODRECNO
        '
        Me.GGODRECNO.HeaderText = "Rec No."
        Me.GGODRECNO.Name = "GGODRECNO"
        Me.GGODRECNO.ReadOnly = True
        Me.GGODRECNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GGODRECNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GGODRECNO.Width = 65
        '
        'GGODRECDATE
        '
        Me.GGODRECDATE.HeaderText = "Rec Date"
        Me.GGODRECDATE.Name = "GGODRECDATE"
        Me.GGODRECDATE.ReadOnly = True
        Me.GGODRECDATE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GGODRECDATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GGODRECDATE.Width = 80
        '
        'GTRANSPORT
        '
        Me.GTRANSPORT.HeaderText = "Transport"
        Me.GTRANSPORT.Name = "GTRANSPORT"
        Me.GTRANSPORT.ReadOnly = True
        Me.GTRANSPORT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GTRANSPORT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GTRANSPORT.Width = 180
        '
        'GLOTNO
        '
        Me.GLOTNO.HeaderText = "Lot No."
        Me.GLOTNO.Name = "GLOTNO"
        Me.GLOTNO.ReadOnly = True
        Me.GLOTNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GLOTNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GCOLOR
        '
        Me.GCOLOR.HeaderText = "Shade"
        Me.GCOLOR.Name = "GCOLOR"
        Me.GCOLOR.ReadOnly = True
        Me.GCOLOR.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GCOLOR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GBAGS
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GBAGS.DefaultCellStyle = DataGridViewCellStyle3
        Me.GBAGS.HeaderText = "Bags"
        Me.GBAGS.Name = "GBAGS"
        Me.GBAGS.ReadOnly = True
        Me.GBAGS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GBAGS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GBAGS.Width = 60
        '
        'GWT
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GWT.DefaultCellStyle = DataGridViewCellStyle4
        Me.GWT.HeaderText = "Wt."
        Me.GWT.Name = "GWT"
        Me.GWT.ReadOnly = True
        Me.GWT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GWT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GWT.Width = 65
        '
        'GFRESH
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GFRESH.DefaultCellStyle = DataGridViewCellStyle5
        Me.GFRESH.HeaderText = "Fresh"
        Me.GFRESH.Name = "GFRESH"
        Me.GFRESH.ReadOnly = True
        Me.GFRESH.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GFRESH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GFRESH.Width = 55
        '
        'GWINDING
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GWINDING.DefaultCellStyle = DataGridViewCellStyle6
        Me.GWINDING.HeaderText = "Winding"
        Me.GWINDING.Name = "GWINDING"
        Me.GWINDING.ReadOnly = True
        Me.GWINDING.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GWINDING.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GWINDING.Width = 70
        '
        'GFIRKA
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GFIRKA.DefaultCellStyle = DataGridViewCellStyle7
        Me.GFIRKA.HeaderText = "Firka"
        Me.GFIRKA.Name = "GFIRKA"
        Me.GFIRKA.ReadOnly = True
        Me.GFIRKA.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GFIRKA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GFIRKA.Width = 55
        '
        'GNALI
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GNALI.DefaultCellStyle = DataGridViewCellStyle8
        Me.GNALI.HeaderText = "Nali"
        Me.GNALI.Name = "GNALI"
        Me.GNALI.ReadOnly = True
        Me.GNALI.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GNALI.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GNALI.Width = 55
        '
        'GLRNO
        '
        Me.GLRNO.HeaderText = "LR/DO No."
        Me.GLRNO.Name = "GLRNO"
        Me.GLRNO.ReadOnly = True
        Me.GLRNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GLRNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GLRNO.Width = 70
        '
        'GLRDATE
        '
        Me.GLRDATE.HeaderText = "LR/DO Date"
        Me.GLRDATE.Name = "GLRDATE"
        Me.GLRDATE.ReadOnly = True
        Me.GLRDATE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GLRDATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GLRDATE.Width = 80
        '
        'GREMARKS
        '
        Me.GREMARKS.HeaderText = "Remarks"
        Me.GREMARKS.Name = "GREMARKS"
        Me.GREMARKS.ReadOnly = True
        Me.GREMARKS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GREMARKS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GREMARKS.Width = 120
        '
        'GOUTBAGS
        '
        Me.GOUTBAGS.HeaderText = "OUTBAGS"
        Me.GOUTBAGS.Name = "GOUTBAGS"
        Me.GOUTBAGS.ReadOnly = True
        Me.GOUTBAGS.Visible = False
        '
        'GOUTWT
        '
        Me.GOUTWT.HeaderText = "OUTWT"
        Me.GOUTWT.Name = "GOUTWT"
        Me.GOUTWT.ReadOnly = True
        Me.GOUTWT.Visible = False
        '
        'GOUTFRESH
        '
        Me.GOUTFRESH.HeaderText = "OUTFRESH"
        Me.GOUTFRESH.Name = "GOUTFRESH"
        Me.GOUTFRESH.ReadOnly = True
        Me.GOUTFRESH.Visible = False
        '
        'GOUTWINDING
        '
        Me.GOUTWINDING.HeaderText = "OUTWINDING"
        Me.GOUTWINDING.Name = "GOUTWINDING"
        Me.GOUTWINDING.ReadOnly = True
        Me.GOUTWINDING.Visible = False
        '
        'GOUTFIRKA
        '
        Me.GOUTFIRKA.HeaderText = "OUTFIRKA"
        Me.GOUTFIRKA.Name = "GOUTFIRKA"
        Me.GOUTFIRKA.ReadOnly = True
        Me.GOUTFIRKA.Visible = False
        '
        'GOUTNALI
        '
        Me.GOUTNALI.HeaderText = "OUTNALI"
        Me.GOUTNALI.Name = "GOUTNALI"
        Me.GOUTNALI.ReadOnly = True
        Me.GOUTNALI.Visible = False
        '
        'TXTLOTNO
        '
        Me.TXTLOTNO.BackColor = System.Drawing.Color.White
        Me.TXTLOTNO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTLOTNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTLOTNO.Location = New System.Drawing.Point(898, 4)
        Me.TXTLOTNO.Name = "TXTLOTNO"
        Me.TXTLOTNO.Size = New System.Drawing.Size(100, 23)
        Me.TXTLOTNO.TabIndex = 7
        '
        'TXTNALI
        '
        Me.TXTNALI.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTNALI.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTNALI.Location = New System.Drawing.Point(1403, 4)
        Me.TXTNALI.Name = "TXTNALI"
        Me.TXTNALI.Size = New System.Drawing.Size(55, 23)
        Me.TXTNALI.TabIndex = 14
        Me.TXTNALI.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTFIRKA
        '
        Me.TXTFIRKA.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTFIRKA.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTFIRKA.Location = New System.Drawing.Point(1348, 4)
        Me.TXTFIRKA.Name = "TXTFIRKA"
        Me.TXTFIRKA.Size = New System.Drawing.Size(55, 23)
        Me.TXTFIRKA.TabIndex = 13
        Me.TXTFIRKA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTWINDING
        '
        Me.TXTWINDING.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTWINDING.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTWINDING.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTWINDING.Location = New System.Drawing.Point(1278, 4)
        Me.TXTWINDING.Name = "TXTWINDING"
        Me.TXTWINDING.Size = New System.Drawing.Size(70, 23)
        Me.TXTWINDING.TabIndex = 12
        Me.TXTWINDING.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTFRESH
        '
        Me.TXTFRESH.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTFRESH.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTFRESH.Location = New System.Drawing.Point(1223, 4)
        Me.TXTFRESH.Name = "TXTFRESH"
        Me.TXTFRESH.Size = New System.Drawing.Size(55, 23)
        Me.TXTFRESH.TabIndex = 11
        Me.TXTFRESH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DTLRDATE
        '
        Me.DTLRDATE.AsciiOnly = True
        Me.DTLRDATE.BackColor = System.Drawing.Color.White
        Me.DTLRDATE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTLRDATE.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite
        Me.DTLRDATE.Location = New System.Drawing.Point(1528, 4)
        Me.DTLRDATE.Mask = "00/00/0000"
        Me.DTLRDATE.Name = "DTLRDATE"
        Me.DTLRDATE.Size = New System.Drawing.Size(79, 23)
        Me.DTLRDATE.TabIndex = 16
        Me.DTLRDATE.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals
        Me.DTLRDATE.ValidatingType = GetType(Date)
        '
        'DTGODRECDATE
        '
        Me.DTGODRECDATE.AsciiOnly = True
        Me.DTGODRECDATE.BackColor = System.Drawing.Color.White
        Me.DTGODRECDATE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTGODRECDATE.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite
        Me.DTGODRECDATE.Location = New System.Drawing.Point(639, 4)
        Me.DTGODRECDATE.Mask = "00/00/0000"
        Me.DTGODRECDATE.Name = "DTGODRECDATE"
        Me.DTGODRECDATE.Size = New System.Drawing.Size(79, 23)
        Me.DTGODRECDATE.TabIndex = 5
        Me.DTGODRECDATE.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals
        Me.DTGODRECDATE.ValidatingType = GetType(Date)
        '
        'CMBMILLNAME
        '
        Me.CMBMILLNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBMILLNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBMILLNAME.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBMILLNAME.DropDownWidth = 400
        Me.CMBMILLNAME.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBMILLNAME.FormattingEnabled = True
        Me.CMBMILLNAME.Location = New System.Drawing.Point(303, 4)
        Me.CMBMILLNAME.Name = "CMBMILLNAME"
        Me.CMBMILLNAME.Size = New System.Drawing.Size(180, 23)
        Me.CMBMILLNAME.TabIndex = 2
        '
        'txtbags
        '
        Me.txtbags.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtbags.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbags.Location = New System.Drawing.Point(1098, 4)
        Me.txtbags.Name = "txtbags"
        Me.txtbags.Size = New System.Drawing.Size(60, 23)
        Me.txtbags.TabIndex = 9
        Me.txtbags.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTLRNO
        '
        Me.TXTLRNO.BackColor = System.Drawing.Color.White
        Me.TXTLRNO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTLRNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTLRNO.Location = New System.Drawing.Point(1458, 4)
        Me.TXTLRNO.Name = "TXTLRNO"
        Me.TXTLRNO.Size = New System.Drawing.Size(70, 23)
        Me.TXTLRNO.TabIndex = 15
        '
        'CMBQUALITY
        '
        Me.CMBQUALITY.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBQUALITY.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBQUALITY.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBQUALITY.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBQUALITY.FormattingEnabled = True
        Me.CMBQUALITY.Location = New System.Drawing.Point(3, 4)
        Me.CMBQUALITY.Name = "CMBQUALITY"
        Me.CMBQUALITY.Size = New System.Drawing.Size(120, 23)
        Me.CMBQUALITY.TabIndex = 0
        '
        'TXTWT
        '
        Me.TXTWT.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTWT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTWT.Location = New System.Drawing.Point(1158, 4)
        Me.TXTWT.Name = "TXTWT"
        Me.TXTWT.Size = New System.Drawing.Size(65, 23)
        Me.TXTWT.TabIndex = 10
        Me.TXTWT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CMBTRANS
        '
        Me.CMBTRANS.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBTRANS.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBTRANS.BackColor = System.Drawing.Color.White
        Me.CMBTRANS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBTRANS.FormattingEnabled = True
        Me.CMBTRANS.Location = New System.Drawing.Point(718, 4)
        Me.CMBTRANS.MaxDropDownItems = 14
        Me.CMBTRANS.Name = "CMBTRANS"
        Me.CMBTRANS.Size = New System.Drawing.Size(180, 23)
        Me.CMBTRANS.TabIndex = 6
        '
        'TXTGODRECNO
        '
        Me.TXTGODRECNO.BackColor = System.Drawing.Color.White
        Me.TXTGODRECNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTGODRECNO.Location = New System.Drawing.Point(574, 4)
        Me.TXTGODRECNO.Name = "TXTGODRECNO"
        Me.TXTGODRECNO.Size = New System.Drawing.Size(65, 23)
        Me.TXTGODRECNO.TabIndex = 4
        Me.TXTGODRECNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTREMARKS
        '
        Me.TXTREMARKS.BackColor = System.Drawing.Color.White
        Me.TXTREMARKS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTREMARKS.Location = New System.Drawing.Point(1607, 4)
        Me.TXTREMARKS.Name = "TXTREMARKS"
        Me.TXTREMARKS.Size = New System.Drawing.Size(120, 23)
        Me.TXTREMARKS.TabIndex = 17
        '
        'CMBSUPPLIER
        '
        Me.CMBSUPPLIER.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBSUPPLIER.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBSUPPLIER.BackColor = System.Drawing.Color.White
        Me.CMBSUPPLIER.DropDownWidth = 400
        Me.CMBSUPPLIER.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBSUPPLIER.FormattingEnabled = True
        Me.CMBSUPPLIER.Location = New System.Drawing.Point(123, 4)
        Me.CMBSUPPLIER.Name = "CMBSUPPLIER"
        Me.CMBSUPPLIER.Size = New System.Drawing.Size(180, 23)
        Me.CMBSUPPLIER.TabIndex = 1
        '
        'CMBOURGODOWN
        '
        Me.CMBOURGODOWN.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBOURGODOWN.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBOURGODOWN.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBOURGODOWN.DropDownWidth = 400
        Me.CMBOURGODOWN.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBOURGODOWN.FormattingEnabled = True
        Me.CMBOURGODOWN.Location = New System.Drawing.Point(483, 4)
        Me.CMBOURGODOWN.Name = "CMBOURGODOWN"
        Me.CMBOURGODOWN.Size = New System.Drawing.Size(90, 23)
        Me.CMBOURGODOWN.TabIndex = 3
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.FlatAppearance.BorderSize = 0
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.Black
        Me.cmdexit.Location = New System.Drawing.Point(533, 561)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 28)
        Me.cmdexit.TabIndex = 1
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'EP
        '
        Me.EP.BlinkRate = 0
        Me.EP.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.EP.ContainerControl = Me
        '
        'OpeningStockYarnGodown
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1146, 596)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "OpeningStockYarnGodown"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Opening Stock Yarn (Godown)"
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.gridstock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents LBLTOTALNALI As System.Windows.Forms.Label
    Friend WithEvents LBLTOTALFIRKA As System.Windows.Forms.Label
    Friend WithEvents LBLTOTALWINDING As System.Windows.Forms.Label
    Friend WithEvents LBLTOTALFRESH As System.Windows.Forms.Label
    Friend WithEvents LBLTOTALBAG As System.Windows.Forms.Label
    Friend WithEvents TXTADD As System.Windows.Forms.TextBox
    Friend WithEvents LBLTOTALWT As System.Windows.Forms.Label
    Friend WithEvents cmbcode As System.Windows.Forms.ComboBox
    Friend WithEvents LBLTOTAL As System.Windows.Forms.Label
    Friend WithEvents CMBQUALITYFILTER As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TXTOPYARNSTOCKNO As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents TXTNALI As System.Windows.Forms.TextBox
    Friend WithEvents TXTFIRKA As System.Windows.Forms.TextBox
    Friend WithEvents TXTWINDING As System.Windows.Forms.TextBox
    Friend WithEvents TXTFRESH As System.Windows.Forms.TextBox
    Friend WithEvents DTLRDATE As System.Windows.Forms.MaskedTextBox
    Friend WithEvents DTGODRECDATE As System.Windows.Forms.MaskedTextBox
    Friend WithEvents CMBMILLNAME As System.Windows.Forms.ComboBox
    Friend WithEvents txtbags As System.Windows.Forms.TextBox
    Friend WithEvents TXTLRNO As System.Windows.Forms.TextBox
    Friend WithEvents CMBQUALITY As System.Windows.Forms.ComboBox
    Friend WithEvents TXTWT As System.Windows.Forms.TextBox
    Friend WithEvents CMBTRANS As System.Windows.Forms.ComboBox
    Friend WithEvents TXTGODRECNO As System.Windows.Forms.TextBox
    Friend WithEvents TXTREMARKS As System.Windows.Forms.TextBox
    Friend WithEvents CMBSUPPLIER As System.Windows.Forms.ComboBox
    Friend WithEvents CMBOURGODOWN As System.Windows.Forms.ComboBox
    Friend WithEvents gridstock As System.Windows.Forms.DataGridView
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents EP As System.Windows.Forms.ErrorProvider
    Friend WithEvents TXTLOTNO As System.Windows.Forms.TextBox
    Friend WithEvents GOPYARNSTOCKNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GQUALITY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GSUPPLIERNAME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GMILLNAME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GGODOWN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GGODRECNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GGODRECDATE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GTRANSPORT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GLOTNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GCOLOR As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GBAGS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GWT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GFRESH As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GWINDING As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GFIRKA As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GNALI As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GLRNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GLRDATE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GREMARKS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GOUTBAGS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GOUTWT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GOUTFRESH As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GOUTWINDING As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GOUTFIRKA As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GOUTNALI As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CMBCOLOR As System.Windows.Forms.ComboBox
End Class
