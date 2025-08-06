<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PurchaseConfig
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
        Me.CMBTYPE = New System.Windows.Forms.ComboBox
        Me.cmbregister = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.cmdok = New System.Windows.Forms.Button
        Me.cmdexit = New System.Windows.Forms.Button
        Me.lblgroup = New System.Windows.Forms.Label
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.PBWEAVING = New System.Windows.Forms.PictureBox
        Me.PBSIZING = New System.Windows.Forms.PictureBox
        Me.PBPROCESS = New System.Windows.Forms.PictureBox
        Me.PBGREY = New System.Windows.Forms.PictureBox
        Me.PBFINISHED = New System.Windows.Forms.PictureBox
        Me.PBCOMMON = New System.Windows.Forms.PictureBox
        Me.PBYARN = New System.Windows.Forms.PictureBox
        Me.BlendPanel1.SuspendLayout()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PBWEAVING, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PBSIZING, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PBPROCESS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PBGREY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PBFINISHED, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PBCOMMON, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PBYARN, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.PBWEAVING)
        Me.BlendPanel1.Controls.Add(Me.PBSIZING)
        Me.BlendPanel1.Controls.Add(Me.PBPROCESS)
        Me.BlendPanel1.Controls.Add(Me.PBGREY)
        Me.BlendPanel1.Controls.Add(Me.PBFINISHED)
        Me.BlendPanel1.Controls.Add(Me.PBCOMMON)
        Me.BlendPanel1.Controls.Add(Me.PBYARN)
        Me.BlendPanel1.Controls.Add(Me.CMBTYPE)
        Me.BlendPanel1.Controls.Add(Me.cmbregister)
        Me.BlendPanel1.Controls.Add(Me.Label5)
        Me.BlendPanel1.Controls.Add(Me.cmdok)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Controls.Add(Me.lblgroup)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1004, 562)
        Me.BlendPanel1.TabIndex = 0
        '
        'CMBTYPE
        '
        Me.CMBTYPE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBTYPE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBTYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CMBTYPE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBTYPE.FormattingEnabled = True
        Me.CMBTYPE.Items.AddRange(New Object() {"COMMON PURCHASE", "FINISHED PURCHASE", "GREY PURCHASE", "PROCESS CHGS", "SIZING CHGS", "TRANSPORT CHGS", "WARPER CHGS", "WEAVING CHGS", "YARN PURCHASE"})
        Me.CMBTYPE.Location = New System.Drawing.Point(102, 41)
        Me.CMBTYPE.Name = "CMBTYPE"
        Me.CMBTYPE.Size = New System.Drawing.Size(169, 23)
        Me.CMBTYPE.TabIndex = 1
        '
        'cmbregister
        '
        Me.cmbregister.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbregister.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbregister.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbregister.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbregister.FormattingEnabled = True
        Me.cmbregister.Items.AddRange(New Object() {""})
        Me.cmbregister.Location = New System.Drawing.Point(102, 12)
        Me.cmbregister.Name = "cmbregister"
        Me.cmbregister.Size = New System.Drawing.Size(169, 23)
        Me.cmbregister.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(48, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 15)
        Me.Label5.TabIndex = 715
        Me.Label5.Text = "Register"
        '
        'cmdok
        '
        Me.cmdok.BackColor = System.Drawing.Color.Transparent
        Me.cmdok.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdok.FlatAppearance.BorderSize = 0
        Me.cmdok.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdok.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmdok.Location = New System.Drawing.Point(277, 38)
        Me.cmdok.Name = "cmdok"
        Me.cmdok.Size = New System.Drawing.Size(80, 28)
        Me.cmdok.TabIndex = 2
        Me.cmdok.Text = "Update"
        Me.cmdok.UseVisualStyleBackColor = False
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.FlatAppearance.BorderSize = 0
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmdexit.Location = New System.Drawing.Point(363, 38)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 28)
        Me.cmdexit.TabIndex = 3
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'lblgroup
        '
        Me.lblgroup.BackColor = System.Drawing.Color.Transparent
        Me.lblgroup.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblgroup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblgroup.Location = New System.Drawing.Point(29, 42)
        Me.lblgroup.Name = "lblgroup"
        Me.lblgroup.Size = New System.Drawing.Size(70, 22)
        Me.lblgroup.TabIndex = 149
        Me.lblgroup.Text = "Screen Type"
        Me.lblgroup.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'EP
        '
        Me.EP.BlinkRate = 0
        Me.EP.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.EP.ContainerControl = Me
        '
        'PBWEAVING
        '
        Me.PBWEAVING.BackColor = System.Drawing.Color.Transparent
        Me.PBWEAVING.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PBWEAVING.Image = Global.PROCESS.My.Resources.Resources.WEAVING_CHARGES_SCREEN
        Me.PBWEAVING.Location = New System.Drawing.Point(35, 86)
        Me.PBWEAVING.Name = "PBWEAVING"
        Me.PBWEAVING.Size = New System.Drawing.Size(800, 460)
        Me.PBWEAVING.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PBWEAVING.TabIndex = 722
        Me.PBWEAVING.TabStop = False
        Me.PBWEAVING.Visible = False
        '
        'PBSIZING
        '
        Me.PBSIZING.BackColor = System.Drawing.Color.Transparent
        Me.PBSIZING.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PBSIZING.Image = Global.PROCESS.My.Resources.Resources.SIZING_CHARGES_SCREEN
        Me.PBSIZING.Location = New System.Drawing.Point(35, 86)
        Me.PBSIZING.Name = "PBSIZING"
        Me.PBSIZING.Size = New System.Drawing.Size(800, 460)
        Me.PBSIZING.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PBSIZING.TabIndex = 721
        Me.PBSIZING.TabStop = False
        Me.PBSIZING.Visible = False
        '
        'PBPROCESS
        '
        Me.PBPROCESS.BackColor = System.Drawing.Color.Transparent
        Me.PBPROCESS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PBPROCESS.Image = Global.PROCESS.My.Resources.Resources.PROCESS_CHARGES_SCREEN
        Me.PBPROCESS.Location = New System.Drawing.Point(35, 86)
        Me.PBPROCESS.Name = "PBPROCESS"
        Me.PBPROCESS.Size = New System.Drawing.Size(800, 460)
        Me.PBPROCESS.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PBPROCESS.TabIndex = 720
        Me.PBPROCESS.TabStop = False
        Me.PBPROCESS.Visible = False
        '
        'PBGREY
        '
        Me.PBGREY.BackColor = System.Drawing.Color.Transparent
        Me.PBGREY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PBGREY.Image = Global.PROCESS.My.Resources.Resources.GREY_PURCHASE_SCREEN
        Me.PBGREY.Location = New System.Drawing.Point(35, 86)
        Me.PBGREY.Name = "PBGREY"
        Me.PBGREY.Size = New System.Drawing.Size(800, 460)
        Me.PBGREY.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PBGREY.TabIndex = 718
        Me.PBGREY.TabStop = False
        Me.PBGREY.Visible = False
        '
        'PBFINISHED
        '
        Me.PBFINISHED.BackColor = System.Drawing.Color.Transparent
        Me.PBFINISHED.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PBFINISHED.Image = Global.PROCESS.My.Resources.Resources.FINISHED_PURCHASE_SCREEN
        Me.PBFINISHED.Location = New System.Drawing.Point(35, 86)
        Me.PBFINISHED.Name = "PBFINISHED"
        Me.PBFINISHED.Size = New System.Drawing.Size(800, 460)
        Me.PBFINISHED.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PBFINISHED.TabIndex = 719
        Me.PBFINISHED.TabStop = False
        Me.PBFINISHED.Visible = False
        '
        'PBCOMMON
        '
        Me.PBCOMMON.BackColor = System.Drawing.Color.Transparent
        Me.PBCOMMON.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PBCOMMON.Image = Global.PROCESS.My.Resources.Resources.OTHER_PURCHASE_SCREEN
        Me.PBCOMMON.Location = New System.Drawing.Point(35, 86)
        Me.PBCOMMON.Name = "PBCOMMON"
        Me.PBCOMMON.Size = New System.Drawing.Size(800, 460)
        Me.PBCOMMON.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PBCOMMON.TabIndex = 717
        Me.PBCOMMON.TabStop = False
        Me.PBCOMMON.Visible = False
        '
        'PBYARN
        '
        Me.PBYARN.BackColor = System.Drawing.Color.Transparent
        Me.PBYARN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PBYARN.Image = Global.PROCESS.My.Resources.Resources.YARN_PURCHASE_SCREEN
        Me.PBYARN.Location = New System.Drawing.Point(35, 86)
        Me.PBYARN.Name = "PBYARN"
        Me.PBYARN.Size = New System.Drawing.Size(800, 460)
        Me.PBYARN.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PBYARN.TabIndex = 716
        Me.PBYARN.TabStop = False
        Me.PBYARN.Visible = False
        '
        'PurchaseConfig
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1004, 562)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "PurchaseConfig"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Purchase Config"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PBWEAVING, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PBSIZING, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PBPROCESS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PBGREY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PBFINISHED, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PBCOMMON, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PBYARN, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents CMBTYPE As System.Windows.Forms.ComboBox
    Friend WithEvents cmbregister As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmdok As System.Windows.Forms.Button
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents lblgroup As System.Windows.Forms.Label
    Friend WithEvents PBYARN As System.Windows.Forms.PictureBox
    Friend WithEvents EP As System.Windows.Forms.ErrorProvider
    Friend WithEvents PBGREY As System.Windows.Forms.PictureBox
    Friend WithEvents PBCOMMON As System.Windows.Forms.PictureBox
    Friend WithEvents PBFINISHED As System.Windows.Forms.PictureBox
    Friend WithEvents PBPROCESS As System.Windows.Forms.PictureBox
    Friend WithEvents PBSIZING As System.Windows.Forms.PictureBox
    Friend WithEvents PBWEAVING As System.Windows.Forms.PictureBox
End Class
