<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HSNMaster
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
        Me.CMDEXIT = New System.Windows.Forms.Button
        Me.CMDDELETE = New System.Windows.Forms.Button
        Me.CMDCLEAR = New System.Windows.Forms.Button
        Me.CMDSAVE = New System.Windows.Forms.Button
        Me.TXTIGST = New System.Windows.Forms.TextBox
        Me.TXTSGST = New System.Windows.Forms.TextBox
        Me.TXTCGST = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.TXTDESC = New System.Windows.Forms.TextBox
        Me.TXTITEMDESC = New System.Windows.Forms.TextBox
        Me.LBLITEMDESC = New System.Windows.Forms.Label
        Me.CMBTYPE = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.TXTHSNCODE = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.TXTHSNNO = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.CHKNOITC = New System.Windows.Forms.CheckBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.BlendPanel1.SuspendLayout()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.Label4)
        Me.BlendPanel1.Controls.Add(Me.CHKNOITC)
        Me.BlendPanel1.Controls.Add(Me.CMDEXIT)
        Me.BlendPanel1.Controls.Add(Me.CMDDELETE)
        Me.BlendPanel1.Controls.Add(Me.CMDCLEAR)
        Me.BlendPanel1.Controls.Add(Me.CMDSAVE)
        Me.BlendPanel1.Controls.Add(Me.TXTIGST)
        Me.BlendPanel1.Controls.Add(Me.TXTSGST)
        Me.BlendPanel1.Controls.Add(Me.TXTCGST)
        Me.BlendPanel1.Controls.Add(Me.Label8)
        Me.BlendPanel1.Controls.Add(Me.Label7)
        Me.BlendPanel1.Controls.Add(Me.Label6)
        Me.BlendPanel1.Controls.Add(Me.Label5)
        Me.BlendPanel1.Controls.Add(Me.TXTDESC)
        Me.BlendPanel1.Controls.Add(Me.TXTITEMDESC)
        Me.BlendPanel1.Controls.Add(Me.LBLITEMDESC)
        Me.BlendPanel1.Controls.Add(Me.CMBTYPE)
        Me.BlendPanel1.Controls.Add(Me.Label3)
        Me.BlendPanel1.Controls.Add(Me.TXTHSNCODE)
        Me.BlendPanel1.Controls.Add(Me.Label2)
        Me.BlendPanel1.Controls.Add(Me.TXTHSNNO)
        Me.BlendPanel1.Controls.Add(Me.Label1)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(597, 351)
        Me.BlendPanel1.TabIndex = 0
        '
        'CMDEXIT
        '
        Me.CMDEXIT.Location = New System.Drawing.Point(387, 285)
        Me.CMDEXIT.Name = "CMDEXIT"
        Me.CMDEXIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEXIT.TabIndex = 11
        Me.CMDEXIT.Text = "&Exit"
        Me.CMDEXIT.UseVisualStyleBackColor = True
        '
        'CMDDELETE
        '
        Me.CMDDELETE.Location = New System.Drawing.Point(301, 285)
        Me.CMDDELETE.Name = "CMDDELETE"
        Me.CMDDELETE.Size = New System.Drawing.Size(80, 28)
        Me.CMDDELETE.TabIndex = 10
        Me.CMDDELETE.Text = "&Delete"
        Me.CMDDELETE.UseVisualStyleBackColor = True
        '
        'CMDCLEAR
        '
        Me.CMDCLEAR.Location = New System.Drawing.Point(215, 285)
        Me.CMDCLEAR.Name = "CMDCLEAR"
        Me.CMDCLEAR.Size = New System.Drawing.Size(80, 28)
        Me.CMDCLEAR.TabIndex = 9
        Me.CMDCLEAR.Text = "&Clear"
        Me.CMDCLEAR.UseVisualStyleBackColor = True
        '
        'CMDSAVE
        '
        Me.CMDSAVE.Location = New System.Drawing.Point(129, 285)
        Me.CMDSAVE.Name = "CMDSAVE"
        Me.CMDSAVE.Size = New System.Drawing.Size(80, 28)
        Me.CMDSAVE.TabIndex = 8
        Me.CMDSAVE.Text = "&Save"
        Me.CMDSAVE.UseVisualStyleBackColor = True
        '
        'TXTIGST
        '
        Me.TXTIGST.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTIGST.Location = New System.Drawing.Point(139, 225)
        Me.TXTIGST.Name = "TXTIGST"
        Me.TXTIGST.Size = New System.Drawing.Size(60, 23)
        Me.TXTIGST.TabIndex = 6
        Me.TXTIGST.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTSGST
        '
        Me.TXTSGST.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTSGST.Location = New System.Drawing.Point(139, 196)
        Me.TXTSGST.Name = "TXTSGST"
        Me.TXTSGST.Size = New System.Drawing.Size(60, 23)
        Me.TXTSGST.TabIndex = 5
        Me.TXTSGST.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTCGST
        '
        Me.TXTCGST.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTCGST.Location = New System.Drawing.Point(139, 167)
        Me.TXTCGST.Name = "TXTCGST"
        Me.TXTCGST.Size = New System.Drawing.Size(60, 23)
        Me.TXTCGST.TabIndex = 4
        Me.TXTCGST.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(94, 229)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(43, 15)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "IGST %"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(92, 200)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(45, 15)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "SGST %"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(91, 171)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(46, 15)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "CGST %"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(67, 118)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(70, 15)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Description"
        '
        'TXTDESC
        '
        Me.TXTDESC.BackColor = System.Drawing.Color.White
        Me.TXTDESC.Location = New System.Drawing.Point(139, 114)
        Me.TXTDESC.MaxLength = 500
        Me.TXTDESC.Multiline = True
        Me.TXTDESC.Name = "TXTDESC"
        Me.TXTDESC.Size = New System.Drawing.Size(365, 47)
        Me.TXTDESC.TabIndex = 3
        '
        'TXTITEMDESC
        '
        Me.TXTITEMDESC.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTITEMDESC.Location = New System.Drawing.Point(139, 85)
        Me.TXTITEMDESC.MaxLength = 100
        Me.TXTITEMDESC.Name = "TXTITEMDESC"
        Me.TXTITEMDESC.Size = New System.Drawing.Size(365, 23)
        Me.TXTITEMDESC.TabIndex = 2
        '
        'LBLITEMDESC
        '
        Me.LBLITEMDESC.BackColor = System.Drawing.Color.Transparent
        Me.LBLITEMDESC.Location = New System.Drawing.Point(15, 89)
        Me.LBLITEMDESC.Name = "LBLITEMDESC"
        Me.LBLITEMDESC.Size = New System.Drawing.Size(122, 15)
        Me.LBLITEMDESC.TabIndex = 7
        Me.LBLITEMDESC.Text = "Item Description"
        Me.LBLITEMDESC.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CMBTYPE
        '
        Me.CMBTYPE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBTYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CMBTYPE.FormattingEnabled = True
        Me.CMBTYPE.Items.AddRange(New Object() {"Goods", "Services"})
        Me.CMBTYPE.Location = New System.Drawing.Point(139, 27)
        Me.CMBTYPE.Name = "CMBTYPE"
        Me.CMBTYPE.Size = New System.Drawing.Size(105, 23)
        Me.CMBTYPE.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(47, 60)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(90, 15)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "HSN / SAC Code"
        '
        'TXTHSNCODE
        '
        Me.TXTHSNCODE.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTHSNCODE.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTHSNCODE.Location = New System.Drawing.Point(139, 56)
        Me.TXTHSNCODE.MaxLength = 10
        Me.TXTHSNCODE.Name = "TXTHSNCODE"
        Me.TXTHSNCODE.Size = New System.Drawing.Size(105, 23)
        Me.TXTHSNCODE.TabIndex = 1
        Me.TXTHSNCODE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(106, 31)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(31, 15)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Type"
        '
        'TXTHSNNO
        '
        Me.TXTHSNNO.BackColor = System.Drawing.Color.Linen
        Me.TXTHSNNO.Location = New System.Drawing.Point(303, 3)
        Me.TXTHSNNO.Name = "TXTHSNNO"
        Me.TXTHSNNO.Size = New System.Drawing.Size(105, 23)
        Me.TXTHSNNO.TabIndex = 1
        Me.TXTHSNNO.TabStop = False
        Me.TXTHSNNO.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(257, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "HSN ID"
        Me.Label1.Visible = False
        '
        'EP
        '
        Me.EP.BlinkRate = 0
        Me.EP.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.EP.ContainerControl = Me
        '
        'CHKNOITC
        '
        Me.CHKNOITC.AutoSize = True
        Me.CHKNOITC.BackColor = System.Drawing.Color.Transparent
        Me.CHKNOITC.Location = New System.Drawing.Point(139, 254)
        Me.CHKNOITC.Name = "CHKNOITC"
        Me.CHKNOITC.Size = New System.Drawing.Size(130, 19)
        Me.CHKNOITC.TabIndex = 7
        Me.CHKNOITC.Text = "No Input Tax Credit"
        Me.CHKNOITC.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Red
        Me.Label4.Location = New System.Drawing.Point(12, 324)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(576, 15)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Note : If we are not going to receive Input Tax credit of GST then click on ""No I" & _
            "nput Tax Credit"" check box"
        '
        'HSNMaster
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(597, 351)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "HSNMaster"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "HSN Master"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TXTHSNCODE As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TXTHSNNO As System.Windows.Forms.TextBox
    Friend WithEvents CMBTYPE As System.Windows.Forms.ComboBox
    Friend WithEvents TXTIGST As System.Windows.Forms.TextBox
    Friend WithEvents TXTSGST As System.Windows.Forms.TextBox
    Friend WithEvents TXTCGST As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TXTDESC As System.Windows.Forms.TextBox
    Friend WithEvents TXTITEMDESC As System.Windows.Forms.TextBox
    Friend WithEvents LBLITEMDESC As System.Windows.Forms.Label
    Friend WithEvents EP As System.Windows.Forms.ErrorProvider
    Friend WithEvents CMDSAVE As System.Windows.Forms.Button
    Friend WithEvents CMDEXIT As System.Windows.Forms.Button
    Friend WithEvents CMDDELETE As System.Windows.Forms.Button
    Friend WithEvents CMDCLEAR As System.Windows.Forms.Button
    Friend WithEvents CHKNOITC As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
