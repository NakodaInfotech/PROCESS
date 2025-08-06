<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GSTTaxFilter
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
        Me.BlendPanel2 = New VbPowerPack.BlendPanel()
        Me.CMBTYPE = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CMBCODE = New System.Windows.Forms.ComboBox()
        Me.txtDeliveryadd = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.RBGSTOUTWAREDSERIES = New System.Windows.Forms.RadioButton()
        Me.RBGSTHSNPURDETAILS = New System.Windows.Forms.RadioButton()
        Me.RBGSTHSNDETAILS = New System.Windows.Forms.RadioButton()
        Me.RBGSTSALEHSNSUMM = New System.Windows.Forms.RadioButton()
        Me.RBGSTPURHSNSUMM = New System.Windows.Forms.RadioButton()
        Me.GSTITCRCM = New System.Windows.Forms.RadioButton()
        Me.RBGSTITC = New System.Windows.Forms.RadioButton()
        Me.RBGSTPURCHASEEXCEL = New System.Windows.Forms.RadioButton()
        Me.RBGSTSALEDETAILS = New System.Windows.Forms.RadioButton()
        Me.RBGSTSUMMARY = New System.Windows.Forms.RadioButton()
        Me.cmbacccode = New System.Windows.Forms.ComboBox()
        Me.txtadd = New System.Windows.Forms.TextBox()
        Me.TXTTEMP = New System.Windows.Forms.TextBox()
        Me.chkdate = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.dtto = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtfrom = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.CMBREGISTER = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmdshow = New System.Windows.Forms.Button()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.BlendPanel2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BlendPanel2
        '
        Me.BlendPanel2.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel2.Controls.Add(Me.CMBTYPE)
        Me.BlendPanel2.Controls.Add(Me.Label2)
        Me.BlendPanel2.Controls.Add(Me.CMBCODE)
        Me.BlendPanel2.Controls.Add(Me.txtDeliveryadd)
        Me.BlendPanel2.Controls.Add(Me.GroupBox3)
        Me.BlendPanel2.Controls.Add(Me.cmbacccode)
        Me.BlendPanel2.Controls.Add(Me.txtadd)
        Me.BlendPanel2.Controls.Add(Me.TXTTEMP)
        Me.BlendPanel2.Controls.Add(Me.chkdate)
        Me.BlendPanel2.Controls.Add(Me.GroupBox1)
        Me.BlendPanel2.Controls.Add(Me.CMBREGISTER)
        Me.BlendPanel2.Controls.Add(Me.Label9)
        Me.BlendPanel2.Controls.Add(Me.cmdshow)
        Me.BlendPanel2.Controls.Add(Me.cmdexit)
        Me.BlendPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel2.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel2.Name = "BlendPanel2"
        Me.BlendPanel2.Size = New System.Drawing.Size(505, 406)
        Me.BlendPanel2.TabIndex = 0
        '
        'CMBTYPE
        '
        Me.CMBTYPE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBTYPE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBTYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CMBTYPE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBTYPE.FormattingEnabled = True
        Me.CMBTYPE.Items.AddRange(New Object() {"", "COMMON PURCHASE", "FINISHED PURCHASE", "GREY PURCHASE", "PROCESS CHGS", "SIZING CHGS", "TRANSPORT CHGS", "WARPER CHGS", "WEAVING CHGS", "YARN PURCHASE"})
        Me.CMBTYPE.Location = New System.Drawing.Point(125, 44)
        Me.CMBTYPE.Name = "CMBTYPE"
        Me.CMBTYPE.Size = New System.Drawing.Size(230, 23)
        Me.CMBTYPE.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(28, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(87, 14)
        Me.Label2.TabIndex = 740
        Me.Label2.Text = "Purchase Screen"
        '
        'CMBCODE
        '
        Me.CMBCODE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBCODE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBCODE.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBCODE.FormattingEnabled = True
        Me.CMBCODE.Items.AddRange(New Object() {"C/R", "O/R"})
        Me.CMBCODE.Location = New System.Drawing.Point(37, 365)
        Me.CMBCODE.Name = "CMBCODE"
        Me.CMBCODE.Size = New System.Drawing.Size(28, 22)
        Me.CMBCODE.TabIndex = 738
        Me.CMBCODE.Visible = False
        '
        'txtDeliveryadd
        '
        Me.txtDeliveryadd.BackColor = System.Drawing.Color.White
        Me.txtDeliveryadd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDeliveryadd.Enabled = False
        Me.txtDeliveryadd.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeliveryadd.Location = New System.Drawing.Point(35, 365)
        Me.txtDeliveryadd.Name = "txtDeliveryadd"
        Me.txtDeliveryadd.ReadOnly = True
        Me.txtDeliveryadd.Size = New System.Drawing.Size(34, 22)
        Me.txtDeliveryadd.TabIndex = 737
        Me.txtDeliveryadd.TabStop = False
        Me.txtDeliveryadd.Visible = False
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.RBGSTOUTWAREDSERIES)
        Me.GroupBox3.Controls.Add(Me.RBGSTHSNPURDETAILS)
        Me.GroupBox3.Controls.Add(Me.RBGSTHSNDETAILS)
        Me.GroupBox3.Controls.Add(Me.RBGSTSALEHSNSUMM)
        Me.GroupBox3.Controls.Add(Me.RBGSTPURHSNSUMM)
        Me.GroupBox3.Controls.Add(Me.GSTITCRCM)
        Me.GroupBox3.Controls.Add(Me.RBGSTITC)
        Me.GroupBox3.Controls.Add(Me.RBGSTPURCHASEEXCEL)
        Me.GroupBox3.Controls.Add(Me.RBGSTSALEDETAILS)
        Me.GroupBox3.Controls.Add(Me.RBGSTSUMMARY)
        Me.GroupBox3.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(34, 85)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(428, 199)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Excel Formats"
        '
        'RBGSTOUTWAREDSERIES
        '
        Me.RBGSTOUTWAREDSERIES.AutoSize = True
        Me.RBGSTOUTWAREDSERIES.Location = New System.Drawing.Point(18, 148)
        Me.RBGSTOUTWAREDSERIES.Name = "RBGSTOUTWAREDSERIES"
        Me.RBGSTOUTWAREDSERIES.Size = New System.Drawing.Size(124, 18)
        Me.RBGSTOUTWAREDSERIES.TabIndex = 9
        Me.RBGSTOUTWAREDSERIES.Text = "GST Outward Series"
        Me.RBGSTOUTWAREDSERIES.UseVisualStyleBackColor = True
        '
        'RBGSTHSNPURDETAILS
        '
        Me.RBGSTHSNPURDETAILS.AutoSize = True
        Me.RBGSTHSNPURDETAILS.Location = New System.Drawing.Point(194, 124)
        Me.RBGSTHSNPURDETAILS.Name = "RBGSTHSNPURDETAILS"
        Me.RBGSTHSNPURDETAILS.Size = New System.Drawing.Size(181, 18)
        Me.RBGSTHSNPURDETAILS.TabIndex = 8
        Me.RBGSTHSNPURDETAILS.Text = "GST HSN Wise Purchase Details"
        Me.RBGSTHSNPURDETAILS.UseVisualStyleBackColor = True
        '
        'RBGSTHSNDETAILS
        '
        Me.RBGSTHSNDETAILS.AutoSize = True
        Me.RBGSTHSNDETAILS.Location = New System.Drawing.Point(18, 124)
        Me.RBGSTHSNDETAILS.Name = "RBGSTHSNDETAILS"
        Me.RBGSTHSNDETAILS.Size = New System.Drawing.Size(158, 18)
        Me.RBGSTHSNDETAILS.TabIndex = 4
        Me.RBGSTHSNDETAILS.Text = "GST HSN Wise Sale Details"
        Me.RBGSTHSNDETAILS.UseVisualStyleBackColor = True
        '
        'RBGSTSALEHSNSUMM
        '
        Me.RBGSTSALEHSNSUMM.AutoSize = True
        Me.RBGSTSALEHSNSUMM.Location = New System.Drawing.Point(18, 100)
        Me.RBGSTSALEHSNSUMM.Name = "RBGSTSALEHSNSUMM"
        Me.RBGSTSALEHSNSUMM.Size = New System.Drawing.Size(143, 18)
        Me.RBGSTSALEHSNSUMM.TabIndex = 3
        Me.RBGSTSALEHSNSUMM.Text = "GST Sale HSN Summary"
        Me.RBGSTSALEHSNSUMM.UseVisualStyleBackColor = True
        '
        'RBGSTPURHSNSUMM
        '
        Me.RBGSTPURHSNSUMM.AutoSize = True
        Me.RBGSTPURHSNSUMM.Location = New System.Drawing.Point(194, 100)
        Me.RBGSTPURHSNSUMM.Name = "RBGSTPURHSNSUMM"
        Me.RBGSTPURHSNSUMM.Size = New System.Drawing.Size(166, 18)
        Me.RBGSTPURHSNSUMM.TabIndex = 7
        Me.RBGSTPURHSNSUMM.Text = "GST Purchase HSN Summary"
        Me.RBGSTPURHSNSUMM.UseVisualStyleBackColor = True
        '
        'GSTITCRCM
        '
        Me.GSTITCRCM.AutoSize = True
        Me.GSTITCRCM.Location = New System.Drawing.Point(194, 52)
        Me.GSTITCRCM.Name = "GSTITCRCM"
        Me.GSTITCRCM.Size = New System.Drawing.Size(182, 18)
        Me.GSTITCRCM.TabIndex = 6
        Me.GSTITCRCM.Text = "GST Input Tax credit (ITC - RCM)"
        Me.GSTITCRCM.UseVisualStyleBackColor = True
        '
        'RBGSTITC
        '
        Me.RBGSTITC.AutoSize = True
        Me.RBGSTITC.Location = New System.Drawing.Point(194, 28)
        Me.RBGSTITC.Name = "RBGSTITC"
        Me.RBGSTITC.Size = New System.Drawing.Size(213, 18)
        Me.RBGSTITC.TabIndex = 5
        Me.RBGSTITC.Text = "GST Input Tax Credit (ITC - Registered)"
        Me.RBGSTITC.UseVisualStyleBackColor = True
        '
        'RBGSTPURCHASEEXCEL
        '
        Me.RBGSTPURCHASEEXCEL.AutoSize = True
        Me.RBGSTPURCHASEEXCEL.Location = New System.Drawing.Point(18, 76)
        Me.RBGSTPURCHASEEXCEL.Name = "RBGSTPURCHASEEXCEL"
        Me.RBGSTPURCHASEEXCEL.Size = New System.Drawing.Size(128, 18)
        Me.RBGSTPURCHASEEXCEL.TabIndex = 2
        Me.RBGSTPURCHASEEXCEL.Text = "GST Purchase (Excel)"
        Me.RBGSTPURCHASEEXCEL.UseVisualStyleBackColor = True
        '
        'RBGSTSALEDETAILS
        '
        Me.RBGSTSALEDETAILS.AutoSize = True
        Me.RBGSTSALEDETAILS.Location = New System.Drawing.Point(18, 52)
        Me.RBGSTSALEDETAILS.Name = "RBGSTSALEDETAILS"
        Me.RBGSTSALEDETAILS.Size = New System.Drawing.Size(106, 18)
        Me.RBGSTSALEDETAILS.TabIndex = 1
        Me.RBGSTSALEDETAILS.Text = "GST Sale Details"
        Me.RBGSTSALEDETAILS.UseVisualStyleBackColor = True
        '
        'RBGSTSUMMARY
        '
        Me.RBGSTSUMMARY.AutoSize = True
        Me.RBGSTSUMMARY.Location = New System.Drawing.Point(18, 28)
        Me.RBGSTSUMMARY.Name = "RBGSTSUMMARY"
        Me.RBGSTSUMMARY.Size = New System.Drawing.Size(95, 18)
        Me.RBGSTSUMMARY.TabIndex = 0
        Me.RBGSTSUMMARY.Text = "GST Summary"
        Me.RBGSTSUMMARY.UseVisualStyleBackColor = True
        '
        'cmbacccode
        '
        Me.cmbacccode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbacccode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbacccode.BackColor = System.Drawing.Color.White
        Me.cmbacccode.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbacccode.FormattingEnabled = True
        Me.cmbacccode.Location = New System.Drawing.Point(37, 365)
        Me.cmbacccode.MaxDropDownItems = 14
        Me.cmbacccode.Name = "cmbacccode"
        Me.cmbacccode.Size = New System.Drawing.Size(30, 22)
        Me.cmbacccode.TabIndex = 650
        Me.cmbacccode.Visible = False
        '
        'txtadd
        '
        Me.txtadd.Location = New System.Drawing.Point(37, 366)
        Me.txtadd.Name = "txtadd"
        Me.txtadd.Size = New System.Drawing.Size(30, 20)
        Me.txtadd.TabIndex = 649
        Me.txtadd.Visible = False
        '
        'TXTTEMP
        '
        Me.TXTTEMP.Location = New System.Drawing.Point(34, 368)
        Me.TXTTEMP.Name = "TXTTEMP"
        Me.TXTTEMP.Size = New System.Drawing.Size(30, 20)
        Me.TXTTEMP.TabIndex = 646
        Me.TXTTEMP.Visible = False
        '
        'chkdate
        '
        Me.chkdate.AutoSize = True
        Me.chkdate.BackColor = System.Drawing.Color.Transparent
        Me.chkdate.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkdate.ForeColor = System.Drawing.Color.Black
        Me.chkdate.Location = New System.Drawing.Point(117, 290)
        Me.chkdate.Name = "chkdate"
        Me.chkdate.Size = New System.Drawing.Size(49, 18)
        Me.chkdate.TabIndex = 3
        Me.chkdate.Text = "Date"
        Me.chkdate.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.dtto)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.dtfrom)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(112, 290)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(280, 53)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        '
        'dtto
        '
        Me.dtto.Font = New System.Drawing.Font("Calibri", 9.0!)
        Me.dtto.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtto.Location = New System.Drawing.Point(189, 20)
        Me.dtto.Name = "dtto"
        Me.dtto.Size = New System.Drawing.Size(83, 22)
        Me.dtto.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.0!)
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(161, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(24, 14)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "To :"
        '
        'dtfrom
        '
        Me.dtfrom.Font = New System.Drawing.Font("Calibri", 9.0!)
        Me.dtfrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtfrom.Location = New System.Drawing.Point(50, 20)
        Me.dtfrom.Name = "dtfrom"
        Me.dtfrom.Size = New System.Drawing.Size(83, 22)
        Me.dtfrom.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Calibri", 9.0!)
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(9, 24)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(39, 14)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "From :"
        '
        'CMBREGISTER
        '
        Me.CMBREGISTER.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBREGISTER.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBREGISTER.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBREGISTER.FormattingEnabled = True
        Me.CMBREGISTER.Location = New System.Drawing.Point(125, 16)
        Me.CMBREGISTER.MaxDropDownItems = 14
        Me.CMBREGISTER.Name = "CMBREGISTER"
        Me.CMBREGISTER.Size = New System.Drawing.Size(230, 22)
        Me.CMBREGISTER.TabIndex = 0
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(71, 20)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(48, 14)
        Me.Label9.TabIndex = 419
        Me.Label9.Text = "Register"
        '
        'cmdshow
        '
        Me.cmdshow.BackColor = System.Drawing.Color.Transparent
        Me.cmdshow.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdshow.FlatAppearance.BorderSize = 0
        Me.cmdshow.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdshow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmdshow.Location = New System.Drawing.Point(161, 355)
        Me.cmdshow.Name = "cmdshow"
        Me.cmdshow.Size = New System.Drawing.Size(88, 28)
        Me.cmdshow.TabIndex = 5
        Me.cmdshow.Text = "&Show Details"
        Me.cmdshow.UseVisualStyleBackColor = False
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdexit.FlatAppearance.BorderSize = 0
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmdexit.Location = New System.Drawing.Point(255, 355)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(88, 28)
        Me.cmdexit.TabIndex = 6
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'GSTTaxFilter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(505, 406)
        Me.Controls.Add(Me.BlendPanel2)
        Me.Name = "GSTTaxFilter"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "GST Tax Filter"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel2.ResumeLayout(False)
        Me.BlendPanel2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BlendPanel2 As VbPowerPack.BlendPanel
    Friend WithEvents CMBCODE As System.Windows.Forms.ComboBox
    Friend WithEvents txtDeliveryadd As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents RBGSTSALEDETAILS As System.Windows.Forms.RadioButton
    Friend WithEvents RBGSTSUMMARY As System.Windows.Forms.RadioButton
    Friend WithEvents cmbacccode As System.Windows.Forms.ComboBox
    Friend WithEvents txtadd As System.Windows.Forms.TextBox
    Friend WithEvents TXTTEMP As System.Windows.Forms.TextBox
    Friend WithEvents chkdate As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dtto As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtfrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents CMBREGISTER As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmdshow As System.Windows.Forms.Button
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents RBGSTPURCHASEEXCEL As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CMBTYPE As System.Windows.Forms.ComboBox
    Friend WithEvents RBGSTSALEHSNSUMM As RadioButton
    Friend WithEvents RBGSTPURHSNSUMM As RadioButton
    Friend WithEvents GSTITCRCM As RadioButton
    Friend WithEvents RBGSTITC As RadioButton
    Friend WithEvents RBGSTHSNDETAILS As RadioButton
    Friend WithEvents RBGSTHSNPURDETAILS As RadioButton
    Friend WithEvents RBGSTOUTWAREDSERIES As RadioButton
End Class
