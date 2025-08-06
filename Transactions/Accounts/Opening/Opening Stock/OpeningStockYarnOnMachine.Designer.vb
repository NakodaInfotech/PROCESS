<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OpeningStockYarnOnMachine
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.LBLTOTALCONES = New System.Windows.Forms.Label()
        Me.DTLRDATE = New System.Windows.Forms.MaskedTextBox()
        Me.DTGODRECDATE = New System.Windows.Forms.MaskedTextBox()
        Me.TXTADD = New System.Windows.Forms.TextBox()
        Me.TXTLRNO = New System.Windows.Forms.TextBox()
        Me.cmbcode = New System.Windows.Forms.ComboBox()
        Me.CMBMACHINEFILTER = New System.Windows.Forms.ComboBox()
        Me.LBLNAME = New System.Windows.Forms.Label()
        Me.CMBTRANS = New System.Windows.Forms.ComboBox()
        Me.LBLTOTALBAG = New System.Windows.Forms.Label()
        Me.TXTOPYARNSTOCKNO = New System.Windows.Forms.TextBox()
        Me.TXTGODRECNO = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TXTCONES = New System.Windows.Forms.TextBox()
        Me.TXTWEIGHT = New System.Windows.Forms.TextBox()
        Me.TXTBAGS = New System.Windows.Forms.TextBox()
        Me.CMBMACHINE = New System.Windows.Forms.ComboBox()
        Me.CMBSHADE = New System.Windows.Forms.ComboBox()
        Me.TXTLOTNO = New System.Windows.Forms.TextBox()
        Me.CMBQUALITY = New System.Windows.Forms.ComboBox()
        Me.TXTREMARKS = New System.Windows.Forms.TextBox()
        Me.CMBMILLNAME = New System.Windows.Forms.ComboBox()
        Me.gridstock = New System.Windows.Forms.DataGridView()
        Me.GOPYARNSTOCKNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GMACHINE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GQUALITY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GMILLNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GLOTNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GSHADE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GBAGS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GWT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GCONES = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GREMARKS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GRECDWT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.LBLTOTALWT = New System.Windows.Forms.Label()
        Me.CMBGODOWN = New System.Windows.Forms.ComboBox()
        Me.LBLTOTAL = New System.Windows.Forms.Label()
        Me.BlendPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.gridstock, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.AutoSize = True
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.LBLTOTALCONES)
        Me.BlendPanel1.Controls.Add(Me.DTLRDATE)
        Me.BlendPanel1.Controls.Add(Me.DTGODRECDATE)
        Me.BlendPanel1.Controls.Add(Me.TXTADD)
        Me.BlendPanel1.Controls.Add(Me.TXTLRNO)
        Me.BlendPanel1.Controls.Add(Me.cmbcode)
        Me.BlendPanel1.Controls.Add(Me.CMBMACHINEFILTER)
        Me.BlendPanel1.Controls.Add(Me.LBLNAME)
        Me.BlendPanel1.Controls.Add(Me.CMBTRANS)
        Me.BlendPanel1.Controls.Add(Me.LBLTOTALBAG)
        Me.BlendPanel1.Controls.Add(Me.TXTOPYARNSTOCKNO)
        Me.BlendPanel1.Controls.Add(Me.TXTGODRECNO)
        Me.BlendPanel1.Controls.Add(Me.Panel1)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Controls.Add(Me.LBLTOTALWT)
        Me.BlendPanel1.Controls.Add(Me.CMBGODOWN)
        Me.BlendPanel1.Controls.Add(Me.LBLTOTAL)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1184, 606)
        Me.BlendPanel1.TabIndex = 1
        '
        'LBLTOTALCONES
        '
        Me.LBLTOTALCONES.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALCONES.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALCONES.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALCONES.Location = New System.Drawing.Point(947, 554)
        Me.LBLTOTALCONES.Name = "LBLTOTALCONES"
        Me.LBLTOTALCONES.Size = New System.Drawing.Size(58, 15)
        Me.LBLTOTALCONES.TabIndex = 835
        Me.LBLTOTALCONES.Text = "0"
        Me.LBLTOTALCONES.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DTLRDATE
        '
        Me.DTLRDATE.AsciiOnly = True
        Me.DTLRDATE.BackColor = System.Drawing.Color.White
        Me.DTLRDATE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTLRDATE.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite
        Me.DTLRDATE.Location = New System.Drawing.Point(630, 19)
        Me.DTLRDATE.Mask = "00/00/0000"
        Me.DTLRDATE.Name = "DTLRDATE"
        Me.DTLRDATE.Size = New System.Drawing.Size(79, 23)
        Me.DTLRDATE.TabIndex = 15
        Me.DTLRDATE.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals
        Me.DTLRDATE.ValidatingType = GetType(Date)
        Me.DTLRDATE.Visible = False
        '
        'DTGODRECDATE
        '
        Me.DTGODRECDATE.AsciiOnly = True
        Me.DTGODRECDATE.BackColor = System.Drawing.Color.White
        Me.DTGODRECDATE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTGODRECDATE.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite
        Me.DTGODRECDATE.Location = New System.Drawing.Point(630, 19)
        Me.DTGODRECDATE.Mask = "00/00/0000"
        Me.DTGODRECDATE.Name = "DTGODRECDATE"
        Me.DTGODRECDATE.Size = New System.Drawing.Size(79, 23)
        Me.DTGODRECDATE.TabIndex = 6
        Me.DTGODRECDATE.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals
        Me.DTGODRECDATE.ValidatingType = GetType(Date)
        Me.DTGODRECDATE.Visible = False
        '
        'TXTADD
        '
        Me.TXTADD.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTADD.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTADD.Location = New System.Drawing.Point(889, 18)
        Me.TXTADD.Name = "TXTADD"
        Me.TXTADD.Size = New System.Drawing.Size(29, 21)
        Me.TXTADD.TabIndex = 809
        Me.TXTADD.TabStop = False
        Me.TXTADD.Visible = False
        '
        'TXTLRNO
        '
        Me.TXTLRNO.BackColor = System.Drawing.Color.White
        Me.TXTLRNO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTLRNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTLRNO.Location = New System.Drawing.Point(634, 19)
        Me.TXTLRNO.Name = "TXTLRNO"
        Me.TXTLRNO.Size = New System.Drawing.Size(70, 23)
        Me.TXTLRNO.TabIndex = 14
        Me.TXTLRNO.Visible = False
        '
        'cmbcode
        '
        Me.cmbcode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbcode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbcode.FormattingEnabled = True
        Me.cmbcode.Location = New System.Drawing.Point(924, 15)
        Me.cmbcode.Name = "cmbcode"
        Me.cmbcode.Size = New System.Drawing.Size(25, 22)
        Me.cmbcode.TabIndex = 808
        Me.cmbcode.Visible = False
        '
        'CMBMACHINEFILTER
        '
        Me.CMBMACHINEFILTER.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBMACHINEFILTER.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBMACHINEFILTER.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBMACHINEFILTER.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBMACHINEFILTER.FormattingEnabled = True
        Me.CMBMACHINEFILTER.Location = New System.Drawing.Point(120, 20)
        Me.CMBMACHINEFILTER.Name = "CMBMACHINEFILTER"
        Me.CMBMACHINEFILTER.Size = New System.Drawing.Size(276, 23)
        Me.CMBMACHINEFILTER.TabIndex = 0
        '
        'LBLNAME
        '
        Me.LBLNAME.AutoSize = True
        Me.LBLNAME.BackColor = System.Drawing.Color.Transparent
        Me.LBLNAME.Location = New System.Drawing.Point(35, 24)
        Me.LBLNAME.Name = "LBLNAME"
        Me.LBLNAME.Size = New System.Drawing.Size(82, 14)
        Me.LBLNAME.TabIndex = 716
        Me.LBLNAME.Text = "Select Machine"
        '
        'CMBTRANS
        '
        Me.CMBTRANS.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBTRANS.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBTRANS.BackColor = System.Drawing.Color.White
        Me.CMBTRANS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBTRANS.FormattingEnabled = True
        Me.CMBTRANS.Location = New System.Drawing.Point(619, 19)
        Me.CMBTRANS.MaxDropDownItems = 14
        Me.CMBTRANS.Name = "CMBTRANS"
        Me.CMBTRANS.Size = New System.Drawing.Size(100, 23)
        Me.CMBTRANS.TabIndex = 7
        Me.CMBTRANS.Visible = False
        '
        'LBLTOTALBAG
        '
        Me.LBLTOTALBAG.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALBAG.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALBAG.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALBAG.Location = New System.Drawing.Point(836, 556)
        Me.LBLTOTALBAG.Name = "LBLTOTALBAG"
        Me.LBLTOTALBAG.Size = New System.Drawing.Size(60, 15)
        Me.LBLTOTALBAG.TabIndex = 833
        Me.LBLTOTALBAG.Text = "0"
        Me.LBLTOTALBAG.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TXTOPYARNSTOCKNO
        '
        Me.TXTOPYARNSTOCKNO.BackColor = System.Drawing.Color.White
        Me.TXTOPYARNSTOCKNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTOPYARNSTOCKNO.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TXTOPYARNSTOCKNO.Location = New System.Drawing.Point(1003, 19)
        Me.TXTOPYARNSTOCKNO.Name = "TXTOPYARNSTOCKNO"
        Me.TXTOPYARNSTOCKNO.ReadOnly = True
        Me.TXTOPYARNSTOCKNO.Size = New System.Drawing.Size(30, 23)
        Me.TXTOPYARNSTOCKNO.TabIndex = 715
        Me.TXTOPYARNSTOCKNO.Text = " "
        Me.TXTOPYARNSTOCKNO.Visible = False
        '
        'TXTGODRECNO
        '
        Me.TXTGODRECNO.BackColor = System.Drawing.Color.White
        Me.TXTGODRECNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTGODRECNO.Location = New System.Drawing.Point(637, 19)
        Me.TXTGODRECNO.Name = "TXTGODRECNO"
        Me.TXTGODRECNO.Size = New System.Drawing.Size(65, 23)
        Me.TXTGODRECNO.TabIndex = 5
        Me.TXTGODRECNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TXTGODRECNO.Visible = False
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.BackColor = System.Drawing.Color.Linen
        Me.Panel1.Controls.Add(Me.TXTCONES)
        Me.Panel1.Controls.Add(Me.TXTWEIGHT)
        Me.Panel1.Controls.Add(Me.TXTBAGS)
        Me.Panel1.Controls.Add(Me.CMBMACHINE)
        Me.Panel1.Controls.Add(Me.CMBSHADE)
        Me.Panel1.Controls.Add(Me.TXTLOTNO)
        Me.Panel1.Controls.Add(Me.CMBQUALITY)
        Me.Panel1.Controls.Add(Me.TXTREMARKS)
        Me.Panel1.Controls.Add(Me.CMBMILLNAME)
        Me.Panel1.Controls.Add(Me.gridstock)
        Me.Panel1.Location = New System.Drawing.Point(15, 49)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1157, 489)
        Me.Panel1.TabIndex = 1
        '
        'TXTCONES
        '
        Me.TXTCONES.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTCONES.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTCONES.Location = New System.Drawing.Point(940, 4)
        Me.TXTCONES.Name = "TXTCONES"
        Me.TXTCONES.Size = New System.Drawing.Size(55, 23)
        Me.TXTCONES.TabIndex = 7
        Me.TXTCONES.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTWEIGHT
        '
        Me.TXTWEIGHT.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTWEIGHT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTWEIGHT.Location = New System.Drawing.Point(875, 4)
        Me.TXTWEIGHT.Name = "TXTWEIGHT"
        Me.TXTWEIGHT.Size = New System.Drawing.Size(65, 23)
        Me.TXTWEIGHT.TabIndex = 6
        Me.TXTWEIGHT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTBAGS
        '
        Me.TXTBAGS.BackColor = System.Drawing.Color.White
        Me.TXTBAGS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTBAGS.Location = New System.Drawing.Point(815, 4)
        Me.TXTBAGS.Name = "TXTBAGS"
        Me.TXTBAGS.Size = New System.Drawing.Size(60, 23)
        Me.TXTBAGS.TabIndex = 5
        Me.TXTBAGS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CMBMACHINE
        '
        Me.CMBMACHINE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBMACHINE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBMACHINE.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBMACHINE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBMACHINE.FormattingEnabled = True
        Me.CMBMACHINE.Location = New System.Drawing.Point(4, 4)
        Me.CMBMACHINE.Name = "CMBMACHINE"
        Me.CMBMACHINE.Size = New System.Drawing.Size(200, 23)
        Me.CMBMACHINE.TabIndex = 0
        '
        'CMBSHADE
        '
        Me.CMBSHADE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBSHADE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBSHADE.BackColor = System.Drawing.Color.White
        Me.CMBSHADE.DropDownWidth = 400
        Me.CMBSHADE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBSHADE.FormattingEnabled = True
        Me.CMBSHADE.Location = New System.Drawing.Point(615, 4)
        Me.CMBSHADE.Name = "CMBSHADE"
        Me.CMBSHADE.Size = New System.Drawing.Size(200, 23)
        Me.CMBSHADE.TabIndex = 4
        '
        'TXTLOTNO
        '
        Me.TXTLOTNO.BackColor = System.Drawing.Color.White
        Me.TXTLOTNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTLOTNO.Location = New System.Drawing.Point(524, 4)
        Me.TXTLOTNO.Name = "TXTLOTNO"
        Me.TXTLOTNO.Size = New System.Drawing.Size(91, 23)
        Me.TXTLOTNO.TabIndex = 3
        Me.TXTLOTNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CMBQUALITY
        '
        Me.CMBQUALITY.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBQUALITY.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBQUALITY.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBQUALITY.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBQUALITY.FormattingEnabled = True
        Me.CMBQUALITY.Location = New System.Drawing.Point(204, 4)
        Me.CMBQUALITY.Name = "CMBQUALITY"
        Me.CMBQUALITY.Size = New System.Drawing.Size(120, 23)
        Me.CMBQUALITY.TabIndex = 1
        '
        'TXTREMARKS
        '
        Me.TXTREMARKS.BackColor = System.Drawing.Color.White
        Me.TXTREMARKS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTREMARKS.Location = New System.Drawing.Point(995, 4)
        Me.TXTREMARKS.Name = "TXTREMARKS"
        Me.TXTREMARKS.Size = New System.Drawing.Size(120, 23)
        Me.TXTREMARKS.TabIndex = 8
        Me.TXTREMARKS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CMBMILLNAME
        '
        Me.CMBMILLNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBMILLNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBMILLNAME.BackColor = System.Drawing.Color.White
        Me.CMBMILLNAME.DropDownWidth = 400
        Me.CMBMILLNAME.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBMILLNAME.FormattingEnabled = True
        Me.CMBMILLNAME.Location = New System.Drawing.Point(324, 4)
        Me.CMBMILLNAME.Name = "CMBMILLNAME"
        Me.CMBMILLNAME.Size = New System.Drawing.Size(200, 23)
        Me.CMBMILLNAME.TabIndex = 2
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
        Me.gridstock.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GOPYARNSTOCKNO, Me.GMACHINE, Me.GQUALITY, Me.GMILLNAME, Me.GLOTNO, Me.GSHADE, Me.GBAGS, Me.GWT, Me.GCONES, Me.GREMARKS, Me.GRECDWT})
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gridstock.DefaultCellStyle = DataGridViewCellStyle6
        Me.gridstock.GridColor = System.Drawing.SystemColors.Control
        Me.gridstock.Location = New System.Drawing.Point(4, 27)
        Me.gridstock.MultiSelect = False
        Me.gridstock.Name = "gridstock"
        Me.gridstock.ReadOnly = True
        Me.gridstock.RowHeadersVisible = False
        Me.gridstock.RowHeadersWidth = 30
        Me.gridstock.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Black
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White
        Me.gridstock.RowsDefaultCellStyle = DataGridViewCellStyle7
        Me.gridstock.RowTemplate.Height = 20
        Me.gridstock.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gridstock.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.gridstock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.gridstock.Size = New System.Drawing.Size(1152, 439)
        Me.gridstock.TabIndex = 13
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
        'GMACHINE
        '
        Me.GMACHINE.HeaderText = "Machine Name"
        Me.GMACHINE.Name = "GMACHINE"
        Me.GMACHINE.ReadOnly = True
        Me.GMACHINE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GMACHINE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GMACHINE.Width = 200
        '
        'GQUALITY
        '
        Me.GQUALITY.HeaderText = "Yarn Quality"
        Me.GQUALITY.Name = "GQUALITY"
        Me.GQUALITY.ReadOnly = True
        Me.GQUALITY.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GQUALITY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GQUALITY.Width = 120
        '
        'GMILLNAME
        '
        Me.GMILLNAME.HeaderText = "Mill Name"
        Me.GMILLNAME.Name = "GMILLNAME"
        Me.GMILLNAME.ReadOnly = True
        Me.GMILLNAME.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GMILLNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GMILLNAME.Width = 200
        '
        'GLOTNO
        '
        Me.GLOTNO.HeaderText = "Lot No."
        Me.GLOTNO.Name = "GLOTNO"
        Me.GLOTNO.ReadOnly = True
        Me.GLOTNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GLOTNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GLOTNO.Width = 90
        '
        'GSHADE
        '
        Me.GSHADE.HeaderText = "Shade"
        Me.GSHADE.Name = "GSHADE"
        Me.GSHADE.ReadOnly = True
        Me.GSHADE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GSHADE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GSHADE.Width = 200
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
        'GCONES
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GCONES.DefaultCellStyle = DataGridViewCellStyle5
        Me.GCONES.HeaderText = "CONES"
        Me.GCONES.Name = "GCONES"
        Me.GCONES.ReadOnly = True
        Me.GCONES.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GCONES.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GCONES.Width = 55
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
        'GRECDWT
        '
        Me.GRECDWT.HeaderText = "RECDWT"
        Me.GRECDWT.Name = "GRECDWT"
        Me.GRECDWT.ReadOnly = True
        Me.GRECDWT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRECDWT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GRECDWT.Visible = False
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.FlatAppearance.BorderSize = 0
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.Black
        Me.cmdexit.Location = New System.Drawing.Point(502, 542)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 28)
        Me.cmdexit.TabIndex = 2
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'LBLTOTALWT
        '
        Me.LBLTOTALWT.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALWT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALWT.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALWT.Location = New System.Drawing.Point(891, 555)
        Me.LBLTOTALWT.Name = "LBLTOTALWT"
        Me.LBLTOTALWT.Size = New System.Drawing.Size(66, 15)
        Me.LBLTOTALWT.TabIndex = 832
        Me.LBLTOTALWT.Text = "0.000"
        Me.LBLTOTALWT.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CMBGODOWN
        '
        Me.CMBGODOWN.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBGODOWN.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBGODOWN.BackColor = System.Drawing.Color.White
        Me.CMBGODOWN.DropDownWidth = 400
        Me.CMBGODOWN.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBGODOWN.FormattingEnabled = True
        Me.CMBGODOWN.Location = New System.Drawing.Point(624, 19)
        Me.CMBGODOWN.Name = "CMBGODOWN"
        Me.CMBGODOWN.Size = New System.Drawing.Size(90, 23)
        Me.CMBGODOWN.TabIndex = 4
        Me.CMBGODOWN.Visible = False
        '
        'LBLTOTAL
        '
        Me.LBLTOTAL.AutoSize = True
        Me.LBLTOTAL.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTAL.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTAL.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTAL.Location = New System.Drawing.Point(669, 552)
        Me.LBLTOTAL.Name = "LBLTOTAL"
        Me.LBLTOTAL.Size = New System.Drawing.Size(33, 15)
        Me.LBLTOTAL.TabIndex = 831
        Me.LBLTOTAL.Text = "Total"
        '
        'OpeningStockYarnOnMachine
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1184, 606)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "OpeningStockYarnOnMachine"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "OpeningStockYarnOnMachine"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.gridstock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BlendPanel1 As BlendPanel
    Friend WithEvents LBLTOTALCONES As Label
    Friend WithEvents DTLRDATE As MaskedTextBox
    Friend WithEvents DTGODRECDATE As MaskedTextBox
    Friend WithEvents TXTADD As TextBox
    Friend WithEvents TXTLRNO As TextBox
    Friend WithEvents cmbcode As ComboBox
    Friend WithEvents CMBMACHINEFILTER As ComboBox
    Friend WithEvents LBLNAME As Label
    Friend WithEvents CMBTRANS As ComboBox
    Friend WithEvents LBLTOTALBAG As Label
    Friend WithEvents TXTOPYARNSTOCKNO As TextBox
    Friend WithEvents TXTGODRECNO As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents TXTCONES As TextBox
    Friend WithEvents TXTWEIGHT As TextBox
    Friend WithEvents TXTBAGS As TextBox
    Friend WithEvents CMBMACHINE As ComboBox
    Friend WithEvents TXTLOTNO As TextBox
    Friend WithEvents CMBQUALITY As ComboBox
    Friend WithEvents TXTREMARKS As TextBox
    Friend WithEvents CMBMILLNAME As ComboBox
    Friend WithEvents gridstock As DataGridView
    Friend WithEvents cmdexit As Button
    Friend WithEvents LBLTOTALWT As Label
    Friend WithEvents CMBGODOWN As ComboBox
    Friend WithEvents LBLTOTAL As Label
    Friend WithEvents CMBSHADE As ComboBox
    Friend WithEvents GOPYARNSTOCKNO As DataGridViewTextBoxColumn
    Friend WithEvents GMACHINE As DataGridViewTextBoxColumn
    Friend WithEvents GQUALITY As DataGridViewTextBoxColumn
    Friend WithEvents GMILLNAME As DataGridViewTextBoxColumn
    Friend WithEvents GLOTNO As DataGridViewTextBoxColumn
    Friend WithEvents GSHADE As DataGridViewTextBoxColumn
    Friend WithEvents GBAGS As DataGridViewTextBoxColumn
    Friend WithEvents GWT As DataGridViewTextBoxColumn
    Friend WithEvents GCONES As DataGridViewTextBoxColumn
    Friend WithEvents GREMARKS As DataGridViewTextBoxColumn
    Friend WithEvents GRECDWT As DataGridViewTextBoxColumn
End Class
