<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class QualityMaster
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Ep = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbQuality = New System.Windows.Forms.ComboBox()
        Me.txtremarks = New System.Windows.Forms.TextBox()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.cmdok = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmddelete = New System.Windows.Forms.Button()
        Me.CMBYARNQUALITY = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TXTRATE = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TXTHSNCODE = New System.Windows.Forms.TextBox()
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TXTDENIER = New System.Windows.Forms.TextBox()
        Me.GRPCOMPOSITION = New System.Windows.Forms.GroupBox()
        Me.TXTTOTALPER = New System.Windows.Forms.TextBox()
        Me.CMBYARNCOMPOSITION = New System.Windows.Forms.ComboBox()
        Me.TXTPER = New System.Windows.Forms.TextBox()
        Me.GRIDCOMP = New System.Windows.Forms.DataGridView()
        Me.GYARNQUALITY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GPER = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.Ep, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BlendPanel1.SuspendLayout()
        Me.GRPCOMPOSITION.SuspendLayout()
        CType(Me.GRIDCOMP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Ep
        '
        Me.Ep.BlinkRate = 0
        Me.Ep.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.Ep.ContainerControl = Me
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(20, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 15)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Yarn Quality"
        '
        'cmbQuality
        '
        Me.cmbQuality.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbQuality.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbQuality.BackColor = System.Drawing.Color.LemonChiffon
        Me.cmbQuality.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbQuality.FormattingEnabled = True
        Me.cmbQuality.Location = New System.Drawing.Point(95, 19)
        Me.cmbQuality.MaxDropDownItems = 14
        Me.cmbQuality.Name = "cmbQuality"
        Me.cmbQuality.Size = New System.Drawing.Size(272, 23)
        Me.cmbQuality.TabIndex = 0
        '
        'txtremarks
        '
        Me.txtremarks.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtremarks.ForeColor = System.Drawing.Color.DimGray
        Me.txtremarks.Location = New System.Drawing.Point(95, 135)
        Me.txtremarks.Multiline = True
        Me.txtremarks.Name = "txtremarks"
        Me.txtremarks.Size = New System.Drawing.Size(272, 42)
        Me.txtremarks.TabIndex = 5
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.FlatAppearance.BorderSize = 0
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.Black
        Me.cmdexit.Location = New System.Drawing.Point(237, 186)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 28)
        Me.cmdexit.TabIndex = 9
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
        Me.cmdok.Location = New System.Drawing.Point(65, 186)
        Me.cmdok.Name = "cmdok"
        Me.cmdok.Size = New System.Drawing.Size(80, 28)
        Me.cmdok.TabIndex = 7
        Me.cmdok.Text = "&Save"
        Me.cmdok.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(40, 140)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 15)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Remarks"
        '
        'cmddelete
        '
        Me.cmddelete.BackColor = System.Drawing.Color.Transparent
        Me.cmddelete.FlatAppearance.BorderSize = 0
        Me.cmddelete.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmddelete.ForeColor = System.Drawing.Color.Black
        Me.cmddelete.Location = New System.Drawing.Point(151, 186)
        Me.cmddelete.Name = "cmddelete"
        Me.cmddelete.Size = New System.Drawing.Size(80, 28)
        Me.cmddelete.TabIndex = 8
        Me.cmddelete.Text = "&Delete"
        Me.cmddelete.UseVisualStyleBackColor = False
        '
        'CMBYARNQUALITY
        '
        Me.CMBYARNQUALITY.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBYARNQUALITY.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBYARNQUALITY.FormattingEnabled = True
        Me.CMBYARNQUALITY.Location = New System.Drawing.Point(95, 48)
        Me.CMBYARNQUALITY.Name = "CMBYARNQUALITY"
        Me.CMBYARNQUALITY.Size = New System.Drawing.Size(272, 23)
        Me.CMBYARNQUALITY.TabIndex = 1
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(31, 51)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(60, 15)
        Me.Label9.TabIndex = 10
        Me.Label9.Text = "Effect Into"
        '
        'TXTRATE
        '
        Me.TXTRATE.BackColor = System.Drawing.Color.White
        Me.TXTRATE.Location = New System.Drawing.Point(95, 77)
        Me.TXTRATE.Name = "TXTRATE"
        Me.TXTRATE.Size = New System.Drawing.Size(74, 23)
        Me.TXTRATE.TabIndex = 2
        Me.TXTRATE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(20, 81)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(71, 15)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "Default Rate"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(199, 81)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(91, 15)
        Me.Label11.TabIndex = 13
        Me.Label11.Text = "HSN / SAC Code"
        '
        'TXTHSNCODE
        '
        Me.TXTHSNCODE.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTHSNCODE.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTHSNCODE.Location = New System.Drawing.Point(293, 77)
        Me.TXTHSNCODE.Name = "TXTHSNCODE"
        Me.TXTHSNCODE.ReadOnly = True
        Me.TXTHSNCODE.Size = New System.Drawing.Size(74, 22)
        Me.TXTHSNCODE.TabIndex = 3
        Me.TXTHSNCODE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.Label12)
        Me.BlendPanel1.Controls.Add(Me.TXTDENIER)
        Me.BlendPanel1.Controls.Add(Me.GRPCOMPOSITION)
        Me.BlendPanel1.Controls.Add(Me.TXTHSNCODE)
        Me.BlendPanel1.Controls.Add(Me.Label11)
        Me.BlendPanel1.Controls.Add(Me.Label10)
        Me.BlendPanel1.Controls.Add(Me.TXTRATE)
        Me.BlendPanel1.Controls.Add(Me.Label9)
        Me.BlendPanel1.Controls.Add(Me.CMBYARNQUALITY)
        Me.BlendPanel1.Controls.Add(Me.cmddelete)
        Me.BlendPanel1.Controls.Add(Me.Label1)
        Me.BlendPanel1.Controls.Add(Me.cmdok)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Controls.Add(Me.txtremarks)
        Me.BlendPanel1.Controls.Add(Me.cmbQuality)
        Me.BlendPanel1.Controls.Add(Me.Label3)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(686, 243)
        Me.BlendPanel1.TabIndex = 0
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(12, 110)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(79, 15)
        Me.Label12.TabIndex = 15
        Me.Label12.Text = "Denier/Count"
        '
        'TXTDENIER
        '
        Me.TXTDENIER.BackColor = System.Drawing.Color.White
        Me.TXTDENIER.Location = New System.Drawing.Point(95, 106)
        Me.TXTDENIER.Name = "TXTDENIER"
        Me.TXTDENIER.Size = New System.Drawing.Size(74, 23)
        Me.TXTDENIER.TabIndex = 4
        Me.TXTDENIER.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'GRPCOMPOSITION
        '
        Me.GRPCOMPOSITION.BackColor = System.Drawing.Color.Transparent
        Me.GRPCOMPOSITION.Controls.Add(Me.TXTTOTALPER)
        Me.GRPCOMPOSITION.Controls.Add(Me.CMBYARNCOMPOSITION)
        Me.GRPCOMPOSITION.Controls.Add(Me.TXTPER)
        Me.GRPCOMPOSITION.Controls.Add(Me.GRIDCOMP)
        Me.GRPCOMPOSITION.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRPCOMPOSITION.ForeColor = System.Drawing.Color.Black
        Me.GRPCOMPOSITION.Location = New System.Drawing.Point(380, 21)
        Me.GRPCOMPOSITION.Name = "GRPCOMPOSITION"
        Me.GRPCOMPOSITION.Size = New System.Drawing.Size(292, 173)
        Me.GRPCOMPOSITION.TabIndex = 6
        Me.GRPCOMPOSITION.TabStop = False
        Me.GRPCOMPOSITION.Text = "Yarn Composition"
        '
        'TXTTOTALPER
        '
        Me.TXTTOTALPER.BackColor = System.Drawing.Color.Linen
        Me.TXTTOTALPER.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTTOTALPER.Location = New System.Drawing.Point(211, 146)
        Me.TXTTOTALPER.Name = "TXTTOTALPER"
        Me.TXTTOTALPER.ReadOnly = True
        Me.TXTTOTALPER.Size = New System.Drawing.Size(40, 22)
        Me.TXTTOTALPER.TabIndex = 3
        Me.TXTTOTALPER.TabStop = False
        Me.TXTTOTALPER.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CMBYARNCOMPOSITION
        '
        Me.CMBYARNCOMPOSITION.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBYARNCOMPOSITION.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBYARNCOMPOSITION.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBYARNCOMPOSITION.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBYARNCOMPOSITION.FormattingEnabled = True
        Me.CMBYARNCOMPOSITION.Location = New System.Drawing.Point(11, 21)
        Me.CMBYARNCOMPOSITION.MaxDropDownItems = 14
        Me.CMBYARNCOMPOSITION.Name = "CMBYARNCOMPOSITION"
        Me.CMBYARNCOMPOSITION.Size = New System.Drawing.Size(200, 22)
        Me.CMBYARNCOMPOSITION.TabIndex = 0
        '
        'TXTPER
        '
        Me.TXTPER.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTPER.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTPER.Location = New System.Drawing.Point(211, 21)
        Me.TXTPER.Name = "TXTPER"
        Me.TXTPER.Size = New System.Drawing.Size(40, 22)
        Me.TXTPER.TabIndex = 1
        Me.TXTPER.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'GRIDCOMP
        '
        Me.GRIDCOMP.AllowUserToAddRows = False
        Me.GRIDCOMP.AllowUserToDeleteRows = False
        Me.GRIDCOMP.AllowUserToResizeColumns = False
        Me.GRIDCOMP.AllowUserToResizeRows = False
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.GRIDCOMP.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle6
        Me.GRIDCOMP.BackgroundColor = System.Drawing.Color.White
        Me.GRIDCOMP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GRIDCOMP.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GRIDCOMP.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.GRIDCOMP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GRIDCOMP.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GYARNQUALITY, Me.GPER})
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDCOMP.DefaultCellStyle = DataGridViewCellStyle9
        Me.GRIDCOMP.GridColor = System.Drawing.SystemColors.ControlText
        Me.GRIDCOMP.Location = New System.Drawing.Point(11, 43)
        Me.GRIDCOMP.Margin = New System.Windows.Forms.Padding(2)
        Me.GRIDCOMP.MultiSelect = False
        Me.GRIDCOMP.Name = "GRIDCOMP"
        Me.GRIDCOMP.ReadOnly = True
        Me.GRIDCOMP.RowHeadersVisible = False
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.Black
        Me.GRIDCOMP.RowsDefaultCellStyle = DataGridViewCellStyle10
        Me.GRIDCOMP.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.GRIDCOMP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GRIDCOMP.Size = New System.Drawing.Size(270, 100)
        Me.GRIDCOMP.TabIndex = 2
        '
        'GYARNQUALITY
        '
        Me.GYARNQUALITY.HeaderText = "Yarn Quality"
        Me.GYARNQUALITY.Name = "GYARNQUALITY"
        Me.GYARNQUALITY.ReadOnly = True
        Me.GYARNQUALITY.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GYARNQUALITY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GYARNQUALITY.Width = 200
        '
        'GPER
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GPER.DefaultCellStyle = DataGridViewCellStyle8
        Me.GPER.HeaderText = "%"
        Me.GPER.Name = "GPER"
        Me.GPER.ReadOnly = True
        Me.GPER.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GPER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GPER.Width = 40
        '
        'QualityMaster
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(686, 243)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "QualityMaster"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Yarn Quality Master"
        CType(Me.Ep, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        Me.GRPCOMPOSITION.ResumeLayout(False)
        Me.GRPCOMPOSITION.PerformLayout()
        CType(Me.GRIDCOMP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmbunit As System.Windows.Forms.ComboBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Ep As System.Windows.Forms.ErrorProvider
    Friend WithEvents cmbprocessname As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbitemname As System.Windows.Forms.ComboBox
    Friend WithEvents TXTWIDTH As System.Windows.Forms.TextBox
    Friend WithEvents TXTPICK As System.Windows.Forms.TextBox
    Friend WithEvents TXTCOUNT As System.Windows.Forms.TextBox
    Friend WithEvents TXTREED As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents BlendPanel1 As BlendPanel
    Friend WithEvents GRPCOMPOSITION As GroupBox
    Friend WithEvents TXTTOTALPER As TextBox
    Friend WithEvents TXTPER As TextBox
    Friend WithEvents GRIDCOMP As DataGridView
    Friend WithEvents GYARNQUALITY As DataGridViewTextBoxColumn
    Friend WithEvents GPER As DataGridViewTextBoxColumn
    Friend WithEvents TXTHSNCODE As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents TXTRATE As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents CMBYARNQUALITY As ComboBox
    Friend WithEvents cmddelete As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents cmdok As Button
    Friend WithEvents cmdexit As Button
    Friend WithEvents txtremarks As TextBox
    Friend WithEvents cmbQuality As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents CMBYARNCOMPOSITION As ComboBox
    Friend WithEvents Label12 As Label
    Friend WithEvents TXTDENIER As TextBox
End Class
