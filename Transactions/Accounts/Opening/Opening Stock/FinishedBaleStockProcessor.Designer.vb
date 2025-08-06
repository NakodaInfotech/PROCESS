<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FinishedBaleStockProcessor
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.BlendPanel1 = New VbPowerPack.BlendPanel
        Me.CMBPROCESSOR = New System.Windows.Forms.ComboBox
        Me.TXTBALENO = New System.Windows.Forms.TextBox
        Me.TXTLOTNO = New System.Windows.Forms.TextBox
        Me.LBLTOTALTAKA = New System.Windows.Forms.Label
        Me.TXTREMARKS = New System.Windows.Forms.TextBox
        Me.TXTMTRS = New System.Windows.Forms.TextBox
        Me.LBLTOTALMTRS = New System.Windows.Forms.Label
        Me.TXTTAKA = New System.Windows.Forms.TextBox
        Me.LBLTOTAL = New System.Windows.Forms.Label
        Me.CMBGREYQUALITY = New System.Windows.Forms.ComboBox
        Me.TXTADD = New System.Windows.Forms.TextBox
        Me.GRIDSTOCK = New System.Windows.Forms.DataGridView
        Me.GOPFINISHEDGREYSTOCK = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GPROCESSOR = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GGREY = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GLOTNO = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GBALENO = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GTAKA = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GMTRS = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GREMARKS = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GDONE = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cmbcode = New System.Windows.Forms.ComboBox
        Me.CMBGREYFILTER = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TXTFINISHEDBALESTOCK = New System.Windows.Forms.TextBox
        Me.cmdexit = New System.Windows.Forms.Button
        Me.BlendPanel1.SuspendLayout()
        CType(Me.GRIDSTOCK, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.AutoSize = True
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.CMBPROCESSOR)
        Me.BlendPanel1.Controls.Add(Me.TXTBALENO)
        Me.BlendPanel1.Controls.Add(Me.TXTLOTNO)
        Me.BlendPanel1.Controls.Add(Me.LBLTOTALTAKA)
        Me.BlendPanel1.Controls.Add(Me.TXTREMARKS)
        Me.BlendPanel1.Controls.Add(Me.TXTMTRS)
        Me.BlendPanel1.Controls.Add(Me.LBLTOTALMTRS)
        Me.BlendPanel1.Controls.Add(Me.TXTTAKA)
        Me.BlendPanel1.Controls.Add(Me.LBLTOTAL)
        Me.BlendPanel1.Controls.Add(Me.CMBGREYQUALITY)
        Me.BlendPanel1.Controls.Add(Me.TXTADD)
        Me.BlendPanel1.Controls.Add(Me.GRIDSTOCK)
        Me.BlendPanel1.Controls.Add(Me.cmbcode)
        Me.BlendPanel1.Controls.Add(Me.CMBGREYFILTER)
        Me.BlendPanel1.Controls.Add(Me.Label1)
        Me.BlendPanel1.Controls.Add(Me.TXTFINISHEDBALESTOCK)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1084, 587)
        Me.BlendPanel1.TabIndex = 0
        '
        'CMBPROCESSOR
        '
        Me.CMBPROCESSOR.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBPROCESSOR.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBPROCESSOR.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBPROCESSOR.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBPROCESSOR.FormattingEnabled = True
        Me.CMBPROCESSOR.Location = New System.Drawing.Point(19, 53)
        Me.CMBPROCESSOR.Name = "CMBPROCESSOR"
        Me.CMBPROCESSOR.Size = New System.Drawing.Size(250, 23)
        Me.CMBPROCESSOR.TabIndex = 1
        '
        'TXTBALENO
        '
        Me.TXTBALENO.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTBALENO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTBALENO.Location = New System.Drawing.Point(609, 53)
        Me.TXTBALENO.MaxLength = 10
        Me.TXTBALENO.Name = "TXTBALENO"
        Me.TXTBALENO.Size = New System.Drawing.Size(90, 23)
        Me.TXTBALENO.TabIndex = 4
        Me.TXTBALENO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTLOTNO
        '
        Me.TXTLOTNO.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTLOTNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTLOTNO.Location = New System.Drawing.Point(519, 53)
        Me.TXTLOTNO.MaxLength = 10
        Me.TXTLOTNO.Name = "TXTLOTNO"
        Me.TXTLOTNO.Size = New System.Drawing.Size(90, 23)
        Me.TXTLOTNO.TabIndex = 3
        Me.TXTLOTNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LBLTOTALTAKA
        '
        Me.LBLTOTALTAKA.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALTAKA.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALTAKA.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALTAKA.Location = New System.Drawing.Point(699, 550)
        Me.LBLTOTALTAKA.Name = "LBLTOTALTAKA"
        Me.LBLTOTALTAKA.Size = New System.Drawing.Size(60, 15)
        Me.LBLTOTALTAKA.TabIndex = 838
        Me.LBLTOTALTAKA.Text = "0"
        Me.LBLTOTALTAKA.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TXTREMARKS
        '
        Me.TXTREMARKS.BackColor = System.Drawing.Color.White
        Me.TXTREMARKS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTREMARKS.Location = New System.Drawing.Point(820, 53)
        Me.TXTREMARKS.MaxLength = 200
        Me.TXTREMARKS.Name = "TXTREMARKS"
        Me.TXTREMARKS.Size = New System.Drawing.Size(200, 23)
        Me.TXTREMARKS.TabIndex = 7
        '
        'TXTMTRS
        '
        Me.TXTMTRS.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTMTRS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTMTRS.Location = New System.Drawing.Point(759, 53)
        Me.TXTMTRS.MaxLength = 10
        Me.TXTMTRS.Name = "TXTMTRS"
        Me.TXTMTRS.Size = New System.Drawing.Size(60, 23)
        Me.TXTMTRS.TabIndex = 6
        Me.TXTMTRS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LBLTOTALMTRS
        '
        Me.LBLTOTALMTRS.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALMTRS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALMTRS.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALMTRS.Location = New System.Drawing.Point(754, 550)
        Me.LBLTOTALMTRS.Name = "LBLTOTALMTRS"
        Me.LBLTOTALMTRS.Size = New System.Drawing.Size(92, 15)
        Me.LBLTOTALMTRS.TabIndex = 837
        Me.LBLTOTALMTRS.Text = "0.00"
        Me.LBLTOTALMTRS.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TXTTAKA
        '
        Me.TXTTAKA.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTTAKA.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTTAKA.Location = New System.Drawing.Point(699, 53)
        Me.TXTTAKA.MaxLength = 10
        Me.TXTTAKA.Name = "TXTTAKA"
        Me.TXTTAKA.Size = New System.Drawing.Size(60, 23)
        Me.TXTTAKA.TabIndex = 5
        Me.TXTTAKA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LBLTOTAL
        '
        Me.LBLTOTAL.AutoSize = True
        Me.LBLTOTAL.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTAL.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTAL.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTAL.Location = New System.Drawing.Point(666, 550)
        Me.LBLTOTAL.Name = "LBLTOTAL"
        Me.LBLTOTAL.Size = New System.Drawing.Size(33, 15)
        Me.LBLTOTAL.TabIndex = 836
        Me.LBLTOTAL.Text = "Total"
        '
        'CMBGREYQUALITY
        '
        Me.CMBGREYQUALITY.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBGREYQUALITY.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBGREYQUALITY.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBGREYQUALITY.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBGREYQUALITY.FormattingEnabled = True
        Me.CMBGREYQUALITY.Location = New System.Drawing.Point(269, 53)
        Me.CMBGREYQUALITY.Name = "CMBGREYQUALITY"
        Me.CMBGREYQUALITY.Size = New System.Drawing.Size(250, 23)
        Me.CMBGREYQUALITY.TabIndex = 2
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
        Me.GRIDSTOCK.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GOPFINISHEDGREYSTOCK, Me.GPROCESSOR, Me.GGREY, Me.GLOTNO, Me.GBALENO, Me.GTAKA, Me.GMTRS, Me.GREMARKS, Me.GDONE})
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDSTOCK.DefaultCellStyle = DataGridViewCellStyle5
        Me.GRIDSTOCK.GridColor = System.Drawing.SystemColors.Control
        Me.GRIDSTOCK.Location = New System.Drawing.Point(19, 77)
        Me.GRIDSTOCK.MultiSelect = False
        Me.GRIDSTOCK.Name = "GRIDSTOCK"
        Me.GRIDSTOCK.ReadOnly = True
        Me.GRIDSTOCK.RowHeadersVisible = False
        Me.GRIDSTOCK.RowHeadersWidth = 30
        Me.GRIDSTOCK.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White
        Me.GRIDSTOCK.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.GRIDSTOCK.RowTemplate.Height = 20
        Me.GRIDSTOCK.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDSTOCK.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.GRIDSTOCK.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.GRIDSTOCK.Size = New System.Drawing.Size(1038, 464)
        Me.GRIDSTOCK.TabIndex = 8
        Me.GRIDSTOCK.TabStop = False
        '
        'GOPFINISHEDGREYSTOCK
        '
        Me.GOPFINISHEDGREYSTOCK.HeaderText = "Sr."
        Me.GOPFINISHEDGREYSTOCK.Name = "GOPFINISHEDGREYSTOCK"
        Me.GOPFINISHEDGREYSTOCK.ReadOnly = True
        Me.GOPFINISHEDGREYSTOCK.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GOPFINISHEDGREYSTOCK.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GOPFINISHEDGREYSTOCK.Visible = False
        Me.GOPFINISHEDGREYSTOCK.Width = 40
        '
        'GPROCESSOR
        '
        Me.GPROCESSOR.HeaderText = "Processor Name"
        Me.GPROCESSOR.Name = "GPROCESSOR"
        Me.GPROCESSOR.ReadOnly = True
        Me.GPROCESSOR.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GPROCESSOR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GPROCESSOR.Width = 250
        '
        'GGREY
        '
        Me.GGREY.HeaderText = "Item Name"
        Me.GGREY.Name = "GGREY"
        Me.GGREY.ReadOnly = True
        Me.GGREY.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GGREY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GGREY.Width = 250
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
        'GBALENO
        '
        Me.GBALENO.HeaderText = "Bale No."
        Me.GBALENO.Name = "GBALENO"
        Me.GBALENO.ReadOnly = True
        Me.GBALENO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GBALENO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GBALENO.Width = 90
        '
        'GTAKA
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GTAKA.DefaultCellStyle = DataGridViewCellStyle3
        Me.GTAKA.HeaderText = "Taka"
        Me.GTAKA.Name = "GTAKA"
        Me.GTAKA.ReadOnly = True
        Me.GTAKA.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GTAKA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GTAKA.Width = 60
        '
        'GMTRS
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GMTRS.DefaultCellStyle = DataGridViewCellStyle4
        Me.GMTRS.HeaderText = "Mtrs."
        Me.GMTRS.Name = "GMTRS"
        Me.GMTRS.ReadOnly = True
        Me.GMTRS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GMTRS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GMTRS.Width = 60
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
        'CMBGREYFILTER
        '
        Me.CMBGREYFILTER.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBGREYFILTER.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBGREYFILTER.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBGREYFILTER.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBGREYFILTER.FormattingEnabled = True
        Me.CMBGREYFILTER.Location = New System.Drawing.Point(91, 20)
        Me.CMBGREYFILTER.Name = "CMBGREYFILTER"
        Me.CMBGREYFILTER.Size = New System.Drawing.Size(250, 23)
        Me.CMBGREYFILTER.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(22, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 14)
        Me.Label1.TabIndex = 716
        Me.Label1.Text = "Item Name"
        '
        'TXTFINISHEDBALESTOCK
        '
        Me.TXTFINISHEDBALESTOCK.BackColor = System.Drawing.Color.White
        Me.TXTFINISHEDBALESTOCK.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTFINISHEDBALESTOCK.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TXTFINISHEDBALESTOCK.Location = New System.Drawing.Point(738, 9)
        Me.TXTFINISHEDBALESTOCK.Name = "TXTFINISHEDBALESTOCK"
        Me.TXTFINISHEDBALESTOCK.ReadOnly = True
        Me.TXTFINISHEDBALESTOCK.Size = New System.Drawing.Size(30, 23)
        Me.TXTFINISHEDBALESTOCK.TabIndex = 715
        Me.TXTFINISHEDBALESTOCK.Text = " "
        Me.TXTFINISHEDBALESTOCK.Visible = False
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.FlatAppearance.BorderSize = 0
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.Black
        Me.cmdexit.Location = New System.Drawing.Point(502, 550)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 28)
        Me.cmdexit.TabIndex = 9
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'FinishedBaleStockProcessor
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1084, 587)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "FinishedBaleStockProcessor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Finished Bale Stock Processor"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.GRIDSTOCK, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents LBLTOTALTAKA As System.Windows.Forms.Label
    Friend WithEvents LBLTOTALMTRS As System.Windows.Forms.Label
    Friend WithEvents LBLTOTAL As System.Windows.Forms.Label
    Friend WithEvents TXTADD As System.Windows.Forms.TextBox
    Friend WithEvents cmbcode As System.Windows.Forms.ComboBox
    Friend WithEvents CMBGREYFILTER As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TXTFINISHEDBALESTOCK As System.Windows.Forms.TextBox
    Friend WithEvents CMBPROCESSOR As System.Windows.Forms.ComboBox
    Friend WithEvents TXTBALENO As System.Windows.Forms.TextBox
    Friend WithEvents TXTLOTNO As System.Windows.Forms.TextBox
    Friend WithEvents TXTREMARKS As System.Windows.Forms.TextBox
    Friend WithEvents TXTMTRS As System.Windows.Forms.TextBox
    Friend WithEvents TXTTAKA As System.Windows.Forms.TextBox
    Friend WithEvents CMBGREYQUALITY As System.Windows.Forms.ComboBox
    Friend WithEvents GRIDSTOCK As System.Windows.Forms.DataGridView
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents GOPFINISHEDGREYSTOCK As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GPROCESSOR As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GGREY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GLOTNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GBALENO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GTAKA As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GMTRS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GREMARKS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GDONE As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
