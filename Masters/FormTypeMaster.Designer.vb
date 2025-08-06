<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormTypeMaster
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
        Me.BlendPanel1 = New VbPowerPack.BlendPanel
        Me.CHKBREAK = New System.Windows.Forms.CheckBox
        Me.RBDO = New System.Windows.Forms.RadioButton
        Me.RBLR = New System.Windows.Forms.RadioButton
        Me.CHKINV = New System.Windows.Forms.CheckBox
        Me.cmddelete = New System.Windows.Forms.Button
        Me.cmdok = New System.Windows.Forms.Button
        Me.lblgroup = New System.Windows.Forms.Label
        Me.TXTFORMTYPE = New System.Windows.Forms.TextBox
        Me.cmdexit = New System.Windows.Forms.Button
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.BlendPanel1.SuspendLayout()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.CHKBREAK)
        Me.BlendPanel1.Controls.Add(Me.RBDO)
        Me.BlendPanel1.Controls.Add(Me.RBLR)
        Me.BlendPanel1.Controls.Add(Me.CHKINV)
        Me.BlendPanel1.Controls.Add(Me.cmddelete)
        Me.BlendPanel1.Controls.Add(Me.cmdok)
        Me.BlendPanel1.Controls.Add(Me.lblgroup)
        Me.BlendPanel1.Controls.Add(Me.TXTFORMTYPE)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(414, 162)
        Me.BlendPanel1.TabIndex = 0
        '
        'CHKBREAK
        '
        Me.CHKBREAK.AutoSize = True
        Me.CHKBREAK.BackColor = System.Drawing.Color.Transparent
        Me.CHKBREAK.Location = New System.Drawing.Point(110, 84)
        Me.CHKBREAK.Name = "CHKBREAK"
        Me.CHKBREAK.Size = New System.Drawing.Size(112, 19)
        Me.CHKBREAK.TabIndex = 2
        Me.CHKBREAK.Text = "Break L.R. / D.O."
        Me.CHKBREAK.UseVisualStyleBackColor = False
        '
        'RBDO
        '
        Me.RBDO.AutoSize = True
        Me.RBDO.BackColor = System.Drawing.Color.Transparent
        Me.RBDO.Location = New System.Drawing.Point(256, 84)
        Me.RBDO.Name = "RBDO"
        Me.RBDO.Size = New System.Drawing.Size(48, 19)
        Me.RBDO.TabIndex = 4
        Me.RBDO.Text = "D.O."
        Me.RBDO.UseVisualStyleBackColor = False
        '
        'RBLR
        '
        Me.RBLR.AutoSize = True
        Me.RBLR.BackColor = System.Drawing.Color.Transparent
        Me.RBLR.Checked = True
        Me.RBLR.Location = New System.Drawing.Point(256, 59)
        Me.RBLR.Name = "RBLR"
        Me.RBLR.Size = New System.Drawing.Size(43, 19)
        Me.RBLR.TabIndex = 3
        Me.RBLR.TabStop = True
        Me.RBLR.Text = "L.R."
        Me.RBLR.UseVisualStyleBackColor = False
        '
        'CHKINV
        '
        Me.CHKINV.AutoSize = True
        Me.CHKINV.BackColor = System.Drawing.Color.Transparent
        Me.CHKINV.Location = New System.Drawing.Point(110, 59)
        Me.CHKINV.Name = "CHKINV"
        Me.CHKINV.Size = New System.Drawing.Size(94, 19)
        Me.CHKINV.TabIndex = 1
        Me.CHKINV.Text = "Auto Invoice"
        Me.CHKINV.UseVisualStyleBackColor = False
        '
        'cmddelete
        '
        Me.cmddelete.BackColor = System.Drawing.Color.Transparent
        Me.cmddelete.FlatAppearance.BorderSize = 0
        Me.cmddelete.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmddelete.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmddelete.Location = New System.Drawing.Point(167, 122)
        Me.cmddelete.Name = "cmddelete"
        Me.cmddelete.Size = New System.Drawing.Size(80, 28)
        Me.cmddelete.TabIndex = 6
        Me.cmddelete.Text = "&Delete"
        Me.cmddelete.UseVisualStyleBackColor = False
        '
        'cmdok
        '
        Me.cmdok.BackColor = System.Drawing.Color.Transparent
        Me.cmdok.FlatAppearance.BorderSize = 0
        Me.cmdok.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdok.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmdok.Location = New System.Drawing.Point(81, 122)
        Me.cmdok.Name = "cmdok"
        Me.cmdok.Size = New System.Drawing.Size(80, 28)
        Me.cmdok.TabIndex = 5
        Me.cmdok.Text = "Save"
        Me.cmdok.UseVisualStyleBackColor = False
        '
        'lblgroup
        '
        Me.lblgroup.AutoSize = True
        Me.lblgroup.BackColor = System.Drawing.Color.Transparent
        Me.lblgroup.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblgroup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblgroup.Location = New System.Drawing.Point(73, 22)
        Me.lblgroup.Name = "lblgroup"
        Me.lblgroup.Size = New System.Drawing.Size(62, 15)
        Me.lblgroup.TabIndex = 149
        Me.lblgroup.Text = "Form Type"
        Me.lblgroup.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TXTFORMTYPE
        '
        Me.TXTFORMTYPE.BackColor = System.Drawing.Color.Linen
        Me.TXTFORMTYPE.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTFORMTYPE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTFORMTYPE.Location = New System.Drawing.Point(137, 19)
        Me.TXTFORMTYPE.MaxLength = 100
        Me.TXTFORMTYPE.Name = "TXTFORMTYPE"
        Me.TXTFORMTYPE.Size = New System.Drawing.Size(205, 23)
        Me.TXTFORMTYPE.TabIndex = 0
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.FlatAppearance.BorderSize = 0
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmdexit.Location = New System.Drawing.Point(253, 122)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 28)
        Me.cmdexit.TabIndex = 7
        Me.cmdexit.Text = "Exit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'EP
        '
        Me.EP.BlinkRate = 0
        Me.EP.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.EP.ContainerControl = Me
        '
        'FormTypeMaster
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(414, 162)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "FormTypeMaster"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Form Type Master"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents cmddelete As System.Windows.Forms.Button
    Friend WithEvents cmdok As System.Windows.Forms.Button
    Friend WithEvents lblgroup As System.Windows.Forms.Label
    Friend WithEvents TXTFORMTYPE As System.Windows.Forms.TextBox
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents CHKINV As System.Windows.Forms.CheckBox
    Friend WithEvents CHKBREAK As System.Windows.Forms.CheckBox
    Friend WithEvents RBDO As System.Windows.Forms.RadioButton
    Friend WithEvents RBLR As System.Windows.Forms.RadioButton
    Friend WithEvents EP As System.Windows.Forms.ErrorProvider
End Class
