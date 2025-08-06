<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WarpRegister
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WarpRegister))
        Me.BlendPanel1 = New VbPowerPack.BlendPanel
        Me.Label10 = New System.Windows.Forms.Label
        Me.TXTCALCCOUNT = New System.Windows.Forms.TextBox
        Me.CMDSELECTROLLRECD = New System.Windows.Forms.Button
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.TXTREMARKS = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.TXTCOUNT = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.TXTLONGATION = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.TXTCONSUMED = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.TXTWTRETURNED = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.TXTWTGIVEN = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.TXTMILLNAME = New System.Windows.Forms.TextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.TXTWARPINGNO = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.TXTTL = New System.Windows.Forms.TextBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.TXTLENGTH = New System.Windows.Forms.TextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.TXTCUT = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.TXTTOTALENDS = New System.Windows.Forms.TextBox
        Me.TXTWARPNO = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.WARPDATE = New System.Windows.Forms.MaskedTextBox
        Me.TXTROLLSRECDNO = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.lblgrndate = New System.Windows.Forms.Label
        Me.CMDDELETE = New System.Windows.Forms.Button
        Me.CMDSAVE = New System.Windows.Forms.Button
        Me.CMDCLEAR = New System.Windows.Forms.Button
        Me.cmdcancel = New System.Windows.Forms.Button
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.OpenToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.SaveToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.tooldelete = New System.Windows.Forms.ToolStripButton
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator
        Me.toolprevious = New System.Windows.Forms.ToolStripButton
        Me.toolnext = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.tstxtbillno = New System.Windows.Forms.TextBox
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Label11 = New System.Windows.Forms.Label
        Me.TXTPROGRAMSRNO = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.TXTSIZERNAME = New System.Windows.Forms.TextBox
        Me.BlendPanel1.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.Label13)
        Me.BlendPanel1.Controls.Add(Me.TXTSIZERNAME)
        Me.BlendPanel1.Controls.Add(Me.Label11)
        Me.BlendPanel1.Controls.Add(Me.TXTPROGRAMSRNO)
        Me.BlendPanel1.Controls.Add(Me.Label10)
        Me.BlendPanel1.Controls.Add(Me.TXTCALCCOUNT)
        Me.BlendPanel1.Controls.Add(Me.CMDSELECTROLLRECD)
        Me.BlendPanel1.Controls.Add(Me.GroupBox5)
        Me.BlendPanel1.Controls.Add(Me.Label9)
        Me.BlendPanel1.Controls.Add(Me.TXTCOUNT)
        Me.BlendPanel1.Controls.Add(Me.Label8)
        Me.BlendPanel1.Controls.Add(Me.TXTLONGATION)
        Me.BlendPanel1.Controls.Add(Me.Label6)
        Me.BlendPanel1.Controls.Add(Me.TXTCONSUMED)
        Me.BlendPanel1.Controls.Add(Me.Label5)
        Me.BlendPanel1.Controls.Add(Me.TXTWTRETURNED)
        Me.BlendPanel1.Controls.Add(Me.Label4)
        Me.BlendPanel1.Controls.Add(Me.TXTWTGIVEN)
        Me.BlendPanel1.Controls.Add(Me.Label3)
        Me.BlendPanel1.Controls.Add(Me.TXTMILLNAME)
        Me.BlendPanel1.Controls.Add(Me.Label23)
        Me.BlendPanel1.Controls.Add(Me.TXTWARPINGNO)
        Me.BlendPanel1.Controls.Add(Me.Label2)
        Me.BlendPanel1.Controls.Add(Me.TXTTL)
        Me.BlendPanel1.Controls.Add(Me.Label26)
        Me.BlendPanel1.Controls.Add(Me.TXTLENGTH)
        Me.BlendPanel1.Controls.Add(Me.Label24)
        Me.BlendPanel1.Controls.Add(Me.TXTCUT)
        Me.BlendPanel1.Controls.Add(Me.Label7)
        Me.BlendPanel1.Controls.Add(Me.TXTTOTALENDS)
        Me.BlendPanel1.Controls.Add(Me.TXTWARPNO)
        Me.BlendPanel1.Controls.Add(Me.Label1)
        Me.BlendPanel1.Controls.Add(Me.WARPDATE)
        Me.BlendPanel1.Controls.Add(Me.TXTROLLSRECDNO)
        Me.BlendPanel1.Controls.Add(Me.Label12)
        Me.BlendPanel1.Controls.Add(Me.lblgrndate)
        Me.BlendPanel1.Controls.Add(Me.CMDDELETE)
        Me.BlendPanel1.Controls.Add(Me.CMDSAVE)
        Me.BlendPanel1.Controls.Add(Me.CMDCLEAR)
        Me.BlendPanel1.Controls.Add(Me.cmdcancel)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 25)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(784, 314)
        Me.BlendPanel1.TabIndex = 0
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(50, 138)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(66, 15)
        Me.Label10.TabIndex = 919
        Me.Label10.Text = "Calc Count"
        '
        'TXTCALCCOUNT
        '
        Me.TXTCALCCOUNT.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTCALCCOUNT.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTCALCCOUNT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTCALCCOUNT.Location = New System.Drawing.Point(117, 134)
        Me.TXTCALCCOUNT.MaxLength = 10
        Me.TXTCALCCOUNT.Name = "TXTCALCCOUNT"
        Me.TXTCALCCOUNT.Size = New System.Drawing.Size(79, 23)
        Me.TXTCALCCOUNT.TabIndex = 2
        Me.TXTCALCCOUNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CMDSELECTROLLRECD
        '
        Me.CMDSELECTROLLRECD.BackColor = System.Drawing.Color.Transparent
        Me.CMDSELECTROLLRECD.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDSELECTROLLRECD.FlatAppearance.BorderSize = 0
        Me.CMDSELECTROLLRECD.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDSELECTROLLRECD.ForeColor = System.Drawing.Color.Black
        Me.CMDSELECTROLLRECD.Location = New System.Drawing.Point(514, 187)
        Me.CMDSELECTROLLRECD.Name = "CMDSELECTROLLRECD"
        Me.CMDSELECTROLLRECD.Size = New System.Drawing.Size(100, 28)
        Me.CMDSELECTROLLRECD.TabIndex = 0
        Me.CMDSELECTROLLRECD.Text = "&Select Roll Rec"
        Me.CMDSELECTROLLRECD.UseVisualStyleBackColor = False
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox5.Controls.Add(Me.TXTREMARKS)
        Me.GroupBox5.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.ForeColor = System.Drawing.Color.Black
        Me.GroupBox5.Location = New System.Drawing.Point(68, 171)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(268, 89)
        Me.GroupBox5.TabIndex = 4
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Remarks"
        '
        'TXTREMARKS
        '
        Me.TXTREMARKS.ForeColor = System.Drawing.Color.Black
        Me.TXTREMARKS.Location = New System.Drawing.Point(5, 16)
        Me.TXTREMARKS.MaxLength = 200
        Me.TXTREMARKS.Multiline = True
        Me.TXTREMARKS.Name = "TXTREMARKS"
        Me.TXTREMARKS.Size = New System.Drawing.Size(257, 65)
        Me.TXTREMARKS.TabIndex = 0
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(260, 138)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(39, 15)
        Me.Label9.TabIndex = 917
        Me.Label9.Text = "Count"
        '
        'TXTCOUNT
        '
        Me.TXTCOUNT.BackColor = System.Drawing.Color.Linen
        Me.TXTCOUNT.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTCOUNT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTCOUNT.Location = New System.Drawing.Point(301, 134)
        Me.TXTCOUNT.MaxLength = 10
        Me.TXTCOUNT.Name = "TXTCOUNT"
        Me.TXTCOUNT.ReadOnly = True
        Me.TXTCOUNT.Size = New System.Drawing.Size(79, 23)
        Me.TXTCOUNT.TabIndex = 916
        Me.TXTCOUNT.TabStop = False
        Me.TXTCOUNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(239, 109)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(60, 15)
        Me.Label8.TabIndex = 915
        Me.Label8.Text = "Longation"
        '
        'TXTLONGATION
        '
        Me.TXTLONGATION.BackColor = System.Drawing.Color.Linen
        Me.TXTLONGATION.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTLONGATION.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTLONGATION.Location = New System.Drawing.Point(301, 105)
        Me.TXTLONGATION.MaxLength = 10
        Me.TXTLONGATION.Name = "TXTLONGATION"
        Me.TXTLONGATION.ReadOnly = True
        Me.TXTLONGATION.Size = New System.Drawing.Size(79, 23)
        Me.TXTLONGATION.TabIndex = 914
        Me.TXTLONGATION.TabStop = False
        Me.TXTLONGATION.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(216, 80)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(83, 15)
        Me.Label6.TabIndex = 913
        Me.Label6.Text = "Wt Consumed"
        '
        'TXTCONSUMED
        '
        Me.TXTCONSUMED.BackColor = System.Drawing.Color.Linen
        Me.TXTCONSUMED.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTCONSUMED.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTCONSUMED.Location = New System.Drawing.Point(301, 76)
        Me.TXTCONSUMED.MaxLength = 10
        Me.TXTCONSUMED.Name = "TXTCONSUMED"
        Me.TXTCONSUMED.ReadOnly = True
        Me.TXTCONSUMED.Size = New System.Drawing.Size(79, 23)
        Me.TXTCONSUMED.TabIndex = 912
        Me.TXTCONSUMED.TabStop = False
        Me.TXTCONSUMED.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(41, 109)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(75, 15)
        Me.Label5.TabIndex = 911
        Me.Label5.Text = "Wt Returned"
        '
        'TXTWTRETURNED
        '
        Me.TXTWTRETURNED.BackColor = System.Drawing.Color.Linen
        Me.TXTWTRETURNED.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTWTRETURNED.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTWTRETURNED.Location = New System.Drawing.Point(117, 105)
        Me.TXTWTRETURNED.MaxLength = 10
        Me.TXTWTRETURNED.Name = "TXTWTRETURNED"
        Me.TXTWTRETURNED.ReadOnly = True
        Me.TXTWTRETURNED.Size = New System.Drawing.Size(79, 23)
        Me.TXTWTRETURNED.TabIndex = 2
        Me.TXTWTRETURNED.TabStop = False
        Me.TXTWTRETURNED.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(29, 80)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(87, 15)
        Me.Label4.TabIndex = 909
        Me.Label4.Text = "Total Wt Given"
        '
        'TXTWTGIVEN
        '
        Me.TXTWTGIVEN.BackColor = System.Drawing.Color.Linen
        Me.TXTWTGIVEN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTWTGIVEN.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTWTGIVEN.Location = New System.Drawing.Point(117, 76)
        Me.TXTWTGIVEN.MaxLength = 10
        Me.TXTWTGIVEN.Name = "TXTWTGIVEN"
        Me.TXTWTGIVEN.ReadOnly = True
        Me.TXTWTGIVEN.Size = New System.Drawing.Size(79, 23)
        Me.TXTWTGIVEN.TabIndex = 1
        Me.TXTWTGIVEN.TabStop = False
        Me.TXTWTGIVEN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(52, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 15)
        Me.Label3.TabIndex = 907
        Me.Label3.Text = "Mill Name"
        '
        'TXTMILLNAME
        '
        Me.TXTMILLNAME.BackColor = System.Drawing.Color.Linen
        Me.TXTMILLNAME.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTMILLNAME.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTMILLNAME.Location = New System.Drawing.Point(117, 18)
        Me.TXTMILLNAME.MaxLength = 10
        Me.TXTMILLNAME.Name = "TXTMILLNAME"
        Me.TXTMILLNAME.ReadOnly = True
        Me.TXTMILLNAME.Size = New System.Drawing.Size(263, 23)
        Me.TXTMILLNAME.TabIndex = 906
        Me.TXTMILLNAME.TabStop = False
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.Black
        Me.Label23.Location = New System.Drawing.Point(584, 138)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(76, 15)
        Me.Label23.TabIndex = 905
        Me.Label23.Text = "Warping No."
        '
        'TXTWARPINGNO
        '
        Me.TXTWARPINGNO.BackColor = System.Drawing.Color.Linen
        Me.TXTWARPINGNO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTWARPINGNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTWARPINGNO.Location = New System.Drawing.Point(662, 134)
        Me.TXTWARPINGNO.MaxLength = 10
        Me.TXTWARPINGNO.Name = "TXTWARPINGNO"
        Me.TXTWARPINGNO.ReadOnly = True
        Me.TXTWARPINGNO.Size = New System.Drawing.Size(79, 23)
        Me.TXTWARPINGNO.TabIndex = 3
        Me.TXTWARPINGNO.TabStop = False
        Me.TXTWARPINGNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(414, 109)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 15)
        Me.Label2.TabIndex = 903
        Me.Label2.Text = "Tapline"
        '
        'TXTTL
        '
        Me.TXTTL.BackColor = System.Drawing.Color.Linen
        Me.TXTTL.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTTL.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTTL.Location = New System.Drawing.Point(463, 105)
        Me.TXTTL.MaxLength = 10
        Me.TXTTL.Name = "TXTTL"
        Me.TXTTL.ReadOnly = True
        Me.TXTTL.Size = New System.Drawing.Size(79, 23)
        Me.TXTTL.TabIndex = 902
        Me.TXTTL.TabStop = False
        Me.TXTTL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.Black
        Me.Label26.Location = New System.Drawing.Point(419, 80)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(42, 15)
        Me.Label26.TabIndex = 901
        Me.Label26.Text = "Length"
        '
        'TXTLENGTH
        '
        Me.TXTLENGTH.BackColor = System.Drawing.Color.Linen
        Me.TXTLENGTH.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTLENGTH.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTLENGTH.Location = New System.Drawing.Point(463, 76)
        Me.TXTLENGTH.MaxLength = 10
        Me.TXTLENGTH.Name = "TXTLENGTH"
        Me.TXTLENGTH.ReadOnly = True
        Me.TXTLENGTH.Size = New System.Drawing.Size(79, 23)
        Me.TXTLENGTH.TabIndex = 900
        Me.TXTLENGTH.TabStop = False
        Me.TXTLENGTH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.Black
        Me.Label24.Location = New System.Drawing.Point(436, 51)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(25, 15)
        Me.Label24.TabIndex = 899
        Me.Label24.Text = "Cut"
        '
        'TXTCUT
        '
        Me.TXTCUT.BackColor = System.Drawing.Color.Linen
        Me.TXTCUT.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTCUT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTCUT.Location = New System.Drawing.Point(463, 47)
        Me.TXTCUT.MaxLength = 10
        Me.TXTCUT.Name = "TXTCUT"
        Me.TXTCUT.ReadOnly = True
        Me.TXTCUT.Size = New System.Drawing.Size(79, 23)
        Me.TXTCUT.TabIndex = 898
        Me.TXTCUT.TabStop = False
        Me.TXTCUT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(398, 22)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(63, 15)
        Me.Label7.TabIndex = 897
        Me.Label7.Text = "Total Ends"
        '
        'TXTTOTALENDS
        '
        Me.TXTTOTALENDS.BackColor = System.Drawing.Color.Linen
        Me.TXTTOTALENDS.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTTOTALENDS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTTOTALENDS.Location = New System.Drawing.Point(463, 18)
        Me.TXTTOTALENDS.MaxLength = 10
        Me.TXTTOTALENDS.Name = "TXTTOTALENDS"
        Me.TXTTOTALENDS.ReadOnly = True
        Me.TXTTOTALENDS.Size = New System.Drawing.Size(79, 23)
        Me.TXTTOTALENDS.TabIndex = 896
        Me.TXTTOTALENDS.TabStop = False
        Me.TXTTOTALENDS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTWARPNO
        '
        Me.TXTWARPNO.BackColor = System.Drawing.Color.Linen
        Me.TXTWARPNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTWARPNO.Location = New System.Drawing.Point(662, 18)
        Me.TXTWARPNO.Name = "TXTWARPNO"
        Me.TXTWARPNO.ReadOnly = True
        Me.TXTWARPNO.Size = New System.Drawing.Size(79, 23)
        Me.TXTWARPNO.TabIndex = 801
        Me.TXTWARPNO.TabStop = False
        Me.TXTWARPNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(607, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 15)
        Me.Label1.TabIndex = 802
        Me.Label1.Text = "Entry No"
        '
        'WARPDATE
        '
        Me.WARPDATE.AsciiOnly = True
        Me.WARPDATE.BackColor = System.Drawing.Color.LemonChiffon
        Me.WARPDATE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WARPDATE.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite
        Me.WARPDATE.Location = New System.Drawing.Point(662, 47)
        Me.WARPDATE.Mask = "00/00/0000"
        Me.WARPDATE.Name = "WARPDATE"
        Me.WARPDATE.Size = New System.Drawing.Size(79, 23)
        Me.WARPDATE.TabIndex = 1
        Me.WARPDATE.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals
        Me.WARPDATE.ValidatingType = GetType(Date)
        '
        'TXTROLLSRECDNO
        '
        Me.TXTROLLSRECDNO.BackColor = System.Drawing.Color.Linen
        Me.TXTROLLSRECDNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTROLLSRECDNO.Location = New System.Drawing.Point(662, 76)
        Me.TXTROLLSRECDNO.Name = "TXTROLLSRECDNO"
        Me.TXTROLLSRECDNO.ReadOnly = True
        Me.TXTROLLSRECDNO.Size = New System.Drawing.Size(79, 23)
        Me.TXTROLLSRECDNO.TabIndex = 799
        Me.TXTROLLSRECDNO.TabStop = False
        Me.TXTROLLSRECDNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(582, 80)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(78, 15)
        Me.Label12.TabIndex = 800
        Me.Label12.Text = "Rolls Rec. No"
        '
        'lblgrndate
        '
        Me.lblgrndate.AutoSize = True
        Me.lblgrndate.BackColor = System.Drawing.Color.Transparent
        Me.lblgrndate.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblgrndate.ForeColor = System.Drawing.Color.Black
        Me.lblgrndate.Location = New System.Drawing.Point(628, 51)
        Me.lblgrndate.Name = "lblgrndate"
        Me.lblgrndate.Size = New System.Drawing.Size(32, 15)
        Me.lblgrndate.TabIndex = 798
        Me.lblgrndate.Text = "Date"
        '
        'CMDDELETE
        '
        Me.CMDDELETE.BackColor = System.Drawing.Color.Transparent
        Me.CMDDELETE.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDDELETE.FlatAppearance.BorderSize = 0
        Me.CMDDELETE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDDELETE.ForeColor = System.Drawing.Color.Black
        Me.CMDDELETE.Location = New System.Drawing.Point(570, 221)
        Me.CMDDELETE.Name = "CMDDELETE"
        Me.CMDDELETE.Size = New System.Drawing.Size(80, 28)
        Me.CMDDELETE.TabIndex = 7
        Me.CMDDELETE.Text = "&Delete"
        Me.CMDDELETE.UseVisualStyleBackColor = False
        '
        'CMDSAVE
        '
        Me.CMDSAVE.BackColor = System.Drawing.Color.Transparent
        Me.CMDSAVE.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDSAVE.FlatAppearance.BorderSize = 0
        Me.CMDSAVE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDSAVE.ForeColor = System.Drawing.Color.Black
        Me.CMDSAVE.Location = New System.Drawing.Point(398, 221)
        Me.CMDSAVE.Name = "CMDSAVE"
        Me.CMDSAVE.Size = New System.Drawing.Size(80, 28)
        Me.CMDSAVE.TabIndex = 5
        Me.CMDSAVE.Text = "&Save"
        Me.CMDSAVE.UseVisualStyleBackColor = False
        '
        'CMDCLEAR
        '
        Me.CMDCLEAR.BackColor = System.Drawing.Color.Transparent
        Me.CMDCLEAR.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDCLEAR.FlatAppearance.BorderSize = 0
        Me.CMDCLEAR.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDCLEAR.ForeColor = System.Drawing.Color.Black
        Me.CMDCLEAR.Location = New System.Drawing.Point(484, 221)
        Me.CMDCLEAR.Name = "CMDCLEAR"
        Me.CMDCLEAR.Size = New System.Drawing.Size(80, 28)
        Me.CMDCLEAR.TabIndex = 6
        Me.CMDCLEAR.Text = "&Clear"
        Me.CMDCLEAR.UseVisualStyleBackColor = False
        '
        'cmdcancel
        '
        Me.cmdcancel.BackColor = System.Drawing.Color.Transparent
        Me.cmdcancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdcancel.FlatAppearance.BorderSize = 0
        Me.cmdcancel.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdcancel.ForeColor = System.Drawing.Color.Black
        Me.cmdcancel.Location = New System.Drawing.Point(656, 221)
        Me.cmdcancel.Name = "cmdcancel"
        Me.cmdcancel.Size = New System.Drawing.Size(80, 28)
        Me.cmdcancel.TabIndex = 8
        Me.cmdcancel.Text = "E&xit"
        Me.cmdcancel.UseVisualStyleBackColor = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenToolStripButton, Me.SaveToolStripButton, Me.PrintToolStripButton, Me.tooldelete, Me.toolStripSeparator, Me.toolprevious, Me.toolnext, Me.ToolStripSeparator1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(784, 25)
        Me.ToolStrip1.TabIndex = 647
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'OpenToolStripButton
        '
        Me.OpenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.OpenToolStripButton.Image = CType(resources.GetObject("OpenToolStripButton.Image"), System.Drawing.Image)
        Me.OpenToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.OpenToolStripButton.Name = "OpenToolStripButton"
        Me.OpenToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.OpenToolStripButton.Text = "&Open"
        '
        'SaveToolStripButton
        '
        Me.SaveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.SaveToolStripButton.Image = CType(resources.GetObject("SaveToolStripButton.Image"), System.Drawing.Image)
        Me.SaveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.SaveToolStripButton.Name = "SaveToolStripButton"
        Me.SaveToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.SaveToolStripButton.Text = "&Save"
        '
        'PrintToolStripButton
        '
        Me.PrintToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PrintToolStripButton.Image = CType(resources.GetObject("PrintToolStripButton.Image"), System.Drawing.Image)
        Me.PrintToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintToolStripButton.Name = "PrintToolStripButton"
        Me.PrintToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.PrintToolStripButton.Text = "&Print"
        '
        'tooldelete
        '
        Me.tooldelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tooldelete.Image = CType(resources.GetObject("tooldelete.Image"), System.Drawing.Image)
        Me.tooldelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tooldelete.Name = "tooldelete"
        Me.tooldelete.Size = New System.Drawing.Size(23, 22)
        Me.tooldelete.Text = "&Delete"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'toolprevious
        '
        Me.toolprevious.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.toolprevious.Image = Global.PROCESS.My.Resources.Resources.POINT02
        Me.toolprevious.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.toolprevious.Name = "toolprevious"
        Me.toolprevious.Size = New System.Drawing.Size(73, 22)
        Me.toolprevious.Text = "Previous"
        '
        'toolnext
        '
        Me.toolnext.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.toolnext.Image = Global.PROCESS.My.Resources.Resources.POINT04
        Me.toolnext.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.toolnext.Name = "toolnext"
        Me.toolnext.Size = New System.Drawing.Size(51, 22)
        Me.toolnext.Text = "Next"
        Me.toolnext.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'tstxtbillno
        '
        Me.tstxtbillno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tstxtbillno.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstxtbillno.Location = New System.Drawing.Point(240, 1)
        Me.tstxtbillno.Name = "tstxtbillno"
        Me.tstxtbillno.Size = New System.Drawing.Size(61, 22)
        Me.tstxtbillno.TabIndex = 1
        Me.tstxtbillno.TabStop = False
        Me.tstxtbillno.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'EP
        '
        Me.EP.BlinkRate = 0
        Me.EP.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.EP.ContainerControl = Me
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(584, 109)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(75, 15)
        Me.Label11.TabIndex = 921
        Me.Label11.Text = "Program No."
        '
        'TXTPROGRAMSRNO
        '
        Me.TXTPROGRAMSRNO.BackColor = System.Drawing.Color.Linen
        Me.TXTPROGRAMSRNO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTPROGRAMSRNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTPROGRAMSRNO.Location = New System.Drawing.Point(662, 105)
        Me.TXTPROGRAMSRNO.MaxLength = 10
        Me.TXTPROGRAMSRNO.Name = "TXTPROGRAMSRNO"
        Me.TXTPROGRAMSRNO.ReadOnly = True
        Me.TXTPROGRAMSRNO.Size = New System.Drawing.Size(79, 23)
        Me.TXTPROGRAMSRNO.TabIndex = 920
        Me.TXTPROGRAMSRNO.TabStop = False
        Me.TXTPROGRAMSRNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(49, 51)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(67, 15)
        Me.Label13.TabIndex = 923
        Me.Label13.Text = "Sizer Name"
        '
        'TXTSIZERNAME
        '
        Me.TXTSIZERNAME.BackColor = System.Drawing.Color.Linen
        Me.TXTSIZERNAME.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTSIZERNAME.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTSIZERNAME.Location = New System.Drawing.Point(117, 47)
        Me.TXTSIZERNAME.MaxLength = 10
        Me.TXTSIZERNAME.Name = "TXTSIZERNAME"
        Me.TXTSIZERNAME.ReadOnly = True
        Me.TXTSIZERNAME.Size = New System.Drawing.Size(263, 23)
        Me.TXTSIZERNAME.TabIndex = 922
        Me.TXTSIZERNAME.TabStop = False
        '
        'WarpRegister
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(784, 339)
        Me.Controls.Add(Me.tstxtbillno)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "WarpRegister"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Warp Register"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents CMDDELETE As System.Windows.Forms.Button
    Friend WithEvents CMDSAVE As System.Windows.Forms.Button
    Friend WithEvents CMDCLEAR As System.Windows.Forms.Button
    Friend WithEvents cmdcancel As System.Windows.Forms.Button
    Friend WithEvents WARPDATE As System.Windows.Forms.MaskedTextBox
    Friend WithEvents TXTROLLSRECDNO As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblgrndate As System.Windows.Forms.Label
    Friend WithEvents TXTWARPNO As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TXTTL As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents TXTLENGTH As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents TXTCUT As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TXTTOTALENDS As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents TXTWARPINGNO As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TXTCOUNT As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TXTLONGATION As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TXTCONSUMED As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TXTWTRETURNED As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TXTWTGIVEN As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TXTMILLNAME As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents TXTREMARKS As System.Windows.Forms.TextBox
    Friend WithEvents CMDSELECTROLLRECD As System.Windows.Forms.Button
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents OpenToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents PrintToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents tooldelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents toolprevious As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolnext As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tstxtbillno As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TXTCALCCOUNT As System.Windows.Forms.TextBox
    Friend WithEvents EP As System.Windows.Forms.ErrorProvider
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents TXTSIZERNAME As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TXTPROGRAMSRNO As System.Windows.Forms.TextBox
End Class
