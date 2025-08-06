<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectPurRetScreen
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.GRIDREGISTER = New System.Windows.Forms.DataGridView
        Me.GREGNAME = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GFRMSTRING = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.GRIDREGISTER, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GRIDREGISTER
        '
        Me.GRIDREGISTER.AllowUserToAddRows = False
        Me.GRIDREGISTER.AllowUserToDeleteRows = False
        Me.GRIDREGISTER.AllowUserToResizeColumns = False
        Me.GRIDREGISTER.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(248, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black
        Me.GRIDREGISTER.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.GRIDREGISTER.BackgroundColor = System.Drawing.Color.White
        Me.GRIDREGISTER.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.GRIDREGISTER.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.GRIDREGISTER.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.Transparent
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(248, Byte), Integer))
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GRIDREGISTER.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.GRIDREGISTER.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GRIDREGISTER.ColumnHeadersVisible = False
        Me.GRIDREGISTER.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GREGNAME, Me.GFRMSTRING})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(248, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDREGISTER.DefaultCellStyle = DataGridViewCellStyle3
        Me.GRIDREGISTER.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GRIDREGISTER.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.GRIDREGISTER.GridColor = System.Drawing.SystemColors.Control
        Me.GRIDREGISTER.Location = New System.Drawing.Point(0, 0)
        Me.GRIDREGISTER.MultiSelect = False
        Me.GRIDREGISTER.Name = "GRIDREGISTER"
        Me.GRIDREGISTER.RowHeadersVisible = False
        Me.GRIDREGISTER.RowHeadersWidth = 30
        Me.GRIDREGISTER.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White
        Me.GRIDREGISTER.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.GRIDREGISTER.RowTemplate.Height = 20
        Me.GRIDREGISTER.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDREGISTER.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.GRIDREGISTER.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GRIDREGISTER.Size = New System.Drawing.Size(334, 262)
        Me.GRIDREGISTER.TabIndex = 2
        '
        'GREGNAME
        '
        Me.GREGNAME.HeaderText = "REGNAME"
        Me.GREGNAME.Name = "GREGNAME"
        Me.GREGNAME.ReadOnly = True
        Me.GREGNAME.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GREGNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GREGNAME.Width = 300
        '
        'GFRMSTRING
        '
        Me.GFRMSTRING.HeaderText = "FRMSTRING"
        Me.GFRMSTRING.Name = "GFRMSTRING"
        Me.GFRMSTRING.Visible = False
        '
        'SelectPurRetScreen
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(334, 262)
        Me.Controls.Add(Me.GRIDREGISTER)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "SelectPurRetScreen"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Select Purchase Return Screen"
        CType(Me.GRIDREGISTER, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GRIDREGISTER As System.Windows.Forms.DataGridView
    Friend WithEvents GREGNAME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GFRMSTRING As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
