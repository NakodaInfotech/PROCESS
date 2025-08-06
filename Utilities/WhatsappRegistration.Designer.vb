<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WhatsappRegistration
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
        Me.CMDRESTART = New System.Windows.Forms.Button()
        Me.CMDEXIT = New System.Windows.Forms.Button()
        Me.CMDDISCONNECT = New System.Windows.Forms.Button()
        Me.CMDSENDDOC = New System.Windows.Forms.Button()
        Me.CMDSTATUS = New System.Windows.Forms.Button()
        Me.TXTRESPONSE = New System.Windows.Forms.TextBox()
        Me.CMDGETQRCODE = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CMDRESTART
        '
        Me.CMDRESTART.Location = New System.Drawing.Point(29, 136)
        Me.CMDRESTART.Name = "CMDRESTART"
        Me.CMDRESTART.Size = New System.Drawing.Size(80, 28)
        Me.CMDRESTART.TabIndex = 15
        Me.CMDRESTART.Text = "Restart Serv."
        Me.CMDRESTART.UseVisualStyleBackColor = True
        Me.CMDRESTART.Visible = False
        '
        'CMDEXIT
        '
        Me.CMDEXIT.Location = New System.Drawing.Point(347, 283)
        Me.CMDEXIT.Name = "CMDEXIT"
        Me.CMDEXIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEXIT.TabIndex = 14
        Me.CMDEXIT.Text = "E&xit"
        Me.CMDEXIT.UseVisualStyleBackColor = True
        '
        'CMDDISCONNECT
        '
        Me.CMDDISCONNECT.Location = New System.Drawing.Point(29, 34)
        Me.CMDDISCONNECT.Name = "CMDDISCONNECT"
        Me.CMDDISCONNECT.Size = New System.Drawing.Size(80, 28)
        Me.CMDDISCONNECT.TabIndex = 13
        Me.CMDDISCONNECT.Text = "Disconnect"
        Me.CMDDISCONNECT.UseVisualStyleBackColor = True
        '
        'CMDSENDDOC
        '
        Me.CMDSENDDOC.Location = New System.Drawing.Point(29, 170)
        Me.CMDSENDDOC.Name = "CMDSENDDOC"
        Me.CMDSENDDOC.Size = New System.Drawing.Size(80, 28)
        Me.CMDSENDDOC.TabIndex = 12
        Me.CMDSENDDOC.Text = "Send Doc"
        Me.CMDSENDDOC.UseVisualStyleBackColor = True
        Me.CMDSENDDOC.Visible = False
        '
        'CMDSTATUS
        '
        Me.CMDSTATUS.Location = New System.Drawing.Point(29, 102)
        Me.CMDSTATUS.Name = "CMDSTATUS"
        Me.CMDSTATUS.Size = New System.Drawing.Size(80, 28)
        Me.CMDSTATUS.TabIndex = 11
        Me.CMDSTATUS.Text = "Get Status"
        Me.CMDSTATUS.UseVisualStyleBackColor = True
        '
        'TXTRESPONSE
        '
        Me.TXTRESPONSE.Location = New System.Drawing.Point(134, 25)
        Me.TXTRESPONSE.Multiline = True
        Me.TXTRESPONSE.Name = "TXTRESPONSE"
        Me.TXTRESPONSE.Size = New System.Drawing.Size(330, 252)
        Me.TXTRESPONSE.TabIndex = 10
        '
        'CMDGETQRCODE
        '
        Me.CMDGETQRCODE.Location = New System.Drawing.Point(29, 68)
        Me.CMDGETQRCODE.Name = "CMDGETQRCODE"
        Me.CMDGETQRCODE.Size = New System.Drawing.Size(80, 28)
        Me.CMDGETQRCODE.TabIndex = 9
        Me.CMDGETQRCODE.Text = "Get QRCode"
        Me.CMDGETQRCODE.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(470, 25)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(550, 380)
        Me.PictureBox1.TabIndex = 8
        Me.PictureBox1.TabStop = False
        '
        'WhatsappRegistration
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1049, 431)
        Me.Controls.Add(Me.CMDRESTART)
        Me.Controls.Add(Me.CMDEXIT)
        Me.Controls.Add(Me.CMDDISCONNECT)
        Me.Controls.Add(Me.CMDSENDDOC)
        Me.Controls.Add(Me.CMDSTATUS)
        Me.Controls.Add(Me.TXTRESPONSE)
        Me.Controls.Add(Me.CMDGETQRCODE)
        Me.Controls.Add(Me.PictureBox1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "WhatsappRegistration"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Whatsapp Registration"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents CMDRESTART As Button
    Friend WithEvents CMDEXIT As Button
    Friend WithEvents CMDDISCONNECT As Button
    Friend WithEvents CMDSENDDOC As Button
    Friend WithEvents CMDSTATUS As Button
    Friend WithEvents TXTRESPONSE As TextBox
    Friend WithEvents CMDGETQRCODE As Button
    Friend WithEvents PictureBox1 As PictureBox
End Class
