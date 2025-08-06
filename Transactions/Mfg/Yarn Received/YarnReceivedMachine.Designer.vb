<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class YarnReceivedMachine
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(YarnReceivedMachine))
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.TXTLABOURNAME = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GRIDYARNCOMP = New System.Windows.Forms.DataGridView()
        Me.CYARNQUALITY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CWT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TXTTYPE = New System.Windows.Forms.TextBox()
        Me.TXTFROMSRNO = New System.Windows.Forms.TextBox()
        Me.TXTFROMTYPE = New System.Windows.Forms.TextBox()
        Me.TXTFROMNO = New System.Windows.Forms.TextBox()
        Me.TXTRUNNINGBAL = New System.Windows.Forms.TextBox()
        Me.CMBISSUENO = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LBLLOTNO = New System.Windows.Forms.Label()
        Me.TXTACTUALISSUEWT = New System.Windows.Forms.TextBox()
        Me.CMBOURGODOWN = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TXTLOTNO = New System.Windows.Forms.TextBox()
        Me.TXTSRNO = New System.Windows.Forms.TextBox()
        Me.TXTTAREWT = New System.Windows.Forms.TextBox()
        Me.TXTBAGS = New System.Windows.Forms.TextBox()
        Me.TXTBAGNO = New System.Windows.Forms.TextBox()
        Me.TXTGROSSWT = New System.Windows.Forms.TextBox()
        Me.TXTNETTWT = New System.Windows.Forms.TextBox()
        Me.CMBCOLOR = New System.Windows.Forms.ComboBox()
        Me.CMBQUALITY = New System.Windows.Forms.ComboBox()
        Me.LBLTOTALTAREWT = New System.Windows.Forms.Label()
        Me.LBLTOTALGROSSWT = New System.Windows.Forms.Label()
        Me.GRIDYARN = New System.Windows.Forms.DataGridView()
        Me.gsrno = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GYARNQUALITY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GLOTNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GSHADE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GBAGNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GBAGS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GGROSSWT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GTAREWT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GNETTWT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GOUTWT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GFROMNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GFROMSRNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GFROMTYPE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LBLTOTALNETTWT = New System.Windows.Forms.Label()
        Me.LBLTOTALCONES = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.tbupload = New System.Windows.Forms.TabPage()
        Me.CMDUPLOAD = New System.Windows.Forms.Button()
        Me.CMDVIEW = New System.Windows.Forms.Button()
        Me.CMDREMOVE = New System.Windows.Forms.Button()
        Me.TXTNEWIMGPATH = New System.Windows.Forms.TextBox()
        Me.TXTFILENAME = New System.Windows.Forms.TextBox()
        Me.txtimgpath = New System.Windows.Forms.TextBox()
        Me.txtuploadname = New System.Windows.Forms.TextBox()
        Me.txtuploadsrno = New System.Windows.Forms.TextBox()
        Me.txtuploadremarks = New System.Windows.Forms.TextBox()
        Me.gridupload = New System.Windows.Forms.DataGridView()
        Me.GUSRNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GUREMARKS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GUNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GUIMGPATH = New System.Windows.Forms.DataGridViewImageColumn()
        Me.GUNEWIMGPATH = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GUFILENAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.PBSoftCopy = New System.Windows.Forms.PictureBox()
        Me.TXTBALWT = New System.Windows.Forms.TextBox()
        Me.LBLPROCESS = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CMBMACHINE = New System.Windows.Forms.ComboBox()
        Me.RECEIVEDDATE = New System.Windows.Forms.MaskedTextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.PBlock = New System.Windows.Forms.PictureBox()
        Me.TXTRECNO = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmddelete = New System.Windows.Forms.Button()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.txtremarks = New System.Windows.Forms.TextBox()
        Me.cmdclear = New System.Windows.Forms.Button()
        Me.CMDOK = New System.Windows.Forms.Button()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.tstxtbillno = New System.Windows.Forms.TextBox()
        Me.lbllocked = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.OpenToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.SaveToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.tooldelete = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.toolprevious = New System.Windows.Forms.ToolStripButton()
        Me.toolnext = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BlendPanel1.SuspendLayout()
        CType(Me.GRIDYARNCOMP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.GRIDYARN, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbupload.SuspendLayout()
        CType(Me.gridupload, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PBSoftCopy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PBlock, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'EP
        '
        Me.EP.BlinkRate = 0
        Me.EP.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.EP.ContainerControl = Me
        '
        'BlendPanel1
        '
        Me.BlendPanel1.AutoSize = True
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.TXTLABOURNAME)
        Me.BlendPanel1.Controls.Add(Me.Label1)
        Me.BlendPanel1.Controls.Add(Me.GRIDYARNCOMP)
        Me.BlendPanel1.Controls.Add(Me.TXTTYPE)
        Me.BlendPanel1.Controls.Add(Me.TXTFROMSRNO)
        Me.BlendPanel1.Controls.Add(Me.TXTFROMTYPE)
        Me.BlendPanel1.Controls.Add(Me.TXTFROMNO)
        Me.BlendPanel1.Controls.Add(Me.TXTRUNNINGBAL)
        Me.BlendPanel1.Controls.Add(Me.CMBISSUENO)
        Me.BlendPanel1.Controls.Add(Me.Label2)
        Me.BlendPanel1.Controls.Add(Me.LBLLOTNO)
        Me.BlendPanel1.Controls.Add(Me.TXTACTUALISSUEWT)
        Me.BlendPanel1.Controls.Add(Me.CMBOURGODOWN)
        Me.BlendPanel1.Controls.Add(Me.Label7)
        Me.BlendPanel1.Controls.Add(Me.TabControl1)
        Me.BlendPanel1.Controls.Add(Me.TXTBALWT)
        Me.BlendPanel1.Controls.Add(Me.LBLPROCESS)
        Me.BlendPanel1.Controls.Add(Me.Label4)
        Me.BlendPanel1.Controls.Add(Me.CMBMACHINE)
        Me.BlendPanel1.Controls.Add(Me.RECEIVEDDATE)
        Me.BlendPanel1.Controls.Add(Me.Label15)
        Me.BlendPanel1.Controls.Add(Me.Label23)
        Me.BlendPanel1.Controls.Add(Me.PBlock)
        Me.BlendPanel1.Controls.Add(Me.TXTRECNO)
        Me.BlendPanel1.Controls.Add(Me.Label12)
        Me.BlendPanel1.Controls.Add(Me.Label9)
        Me.BlendPanel1.Controls.Add(Me.cmddelete)
        Me.BlendPanel1.Controls.Add(Me.GroupBox5)
        Me.BlendPanel1.Controls.Add(Me.cmdclear)
        Me.BlendPanel1.Controls.Add(Me.CMDOK)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Controls.Add(Me.tstxtbillno)
        Me.BlendPanel1.Controls.Add(Me.lbllocked)
        Me.BlendPanel1.Controls.Add(Me.Label6)
        Me.BlendPanel1.Controls.Add(Me.ToolStrip1)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1010, 562)
        Me.BlendPanel1.TabIndex = 0
        '
        'TXTLABOURNAME
        '
        Me.TXTLABOURNAME.BackColor = System.Drawing.Color.White
        Me.TXTLABOURNAME.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTLABOURNAME.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTLABOURNAME.Location = New System.Drawing.Point(417, 90)
        Me.TXTLABOURNAME.MaxLength = 100
        Me.TXTLABOURNAME.Name = "TXTLABOURNAME"
        Me.TXTLABOURNAME.Size = New System.Drawing.Size(220, 23)
        Me.TXTLABOURNAME.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(336, 94)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 15)
        Me.Label1.TabIndex = 429
        Me.Label1.Text = "Labour Name"
        '
        'GRIDYARNCOMP
        '
        Me.GRIDYARNCOMP.AllowUserToAddRows = False
        Me.GRIDYARNCOMP.AllowUserToDeleteRows = False
        Me.GRIDYARNCOMP.AllowUserToResizeColumns = False
        Me.GRIDYARNCOMP.AllowUserToResizeRows = False
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black
        Me.GRIDYARNCOMP.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.GRIDYARNCOMP.BackgroundColor = System.Drawing.Color.White
        Me.GRIDYARNCOMP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GRIDYARNCOMP.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.GRIDYARNCOMP.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.GRIDYARNCOMP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GRIDYARNCOMP.ColumnHeadersVisible = False
        Me.GRIDYARNCOMP.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CYARNQUALITY, Me.CWT})
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDYARNCOMP.DefaultCellStyle = DataGridViewCellStyle4
        Me.GRIDYARNCOMP.GridColor = System.Drawing.SystemColors.Control
        Me.GRIDYARNCOMP.Location = New System.Drawing.Point(971, 154)
        Me.GRIDYARNCOMP.MultiSelect = False
        Me.GRIDYARNCOMP.Name = "GRIDYARNCOMP"
        Me.GRIDYARNCOMP.RowHeadersVisible = False
        Me.GRIDYARNCOMP.RowHeadersWidth = 30
        Me.GRIDYARNCOMP.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White
        Me.GRIDYARNCOMP.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.GRIDYARNCOMP.RowTemplate.Height = 20
        Me.GRIDYARNCOMP.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDYARNCOMP.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.GRIDYARNCOMP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.GRIDYARNCOMP.Size = New System.Drawing.Size(204, 307)
        Me.GRIDYARNCOMP.TabIndex = 902
        Me.GRIDYARNCOMP.TabStop = False
        Me.GRIDYARNCOMP.Visible = False
        '
        'CYARNQUALITY
        '
        Me.CYARNQUALITY.HeaderText = "Yarn Quality"
        Me.CYARNQUALITY.Name = "CYARNQUALITY"
        Me.CYARNQUALITY.ReadOnly = True
        Me.CYARNQUALITY.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.CYARNQUALITY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CYARNQUALITY.Width = 140
        '
        'CWT
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.CWT.DefaultCellStyle = DataGridViewCellStyle3
        Me.CWT.HeaderText = "Wt"
        Me.CWT.Name = "CWT"
        Me.CWT.ReadOnly = True
        Me.CWT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.CWT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CWT.Width = 50
        '
        'TXTTYPE
        '
        Me.TXTTYPE.BackColor = System.Drawing.Color.White
        Me.TXTTYPE.ForeColor = System.Drawing.Color.DimGray
        Me.TXTTYPE.Location = New System.Drawing.Point(382, 28)
        Me.TXTTYPE.Multiline = True
        Me.TXTTYPE.Name = "TXTTYPE"
        Me.TXTTYPE.ReadOnly = True
        Me.TXTTYPE.Size = New System.Drawing.Size(55, 19)
        Me.TXTTYPE.TabIndex = 685
        Me.TXTTYPE.Visible = False
        '
        'TXTFROMSRNO
        '
        Me.TXTFROMSRNO.BackColor = System.Drawing.Color.White
        Me.TXTFROMSRNO.ForeColor = System.Drawing.Color.DimGray
        Me.TXTFROMSRNO.Location = New System.Drawing.Point(598, 28)
        Me.TXTFROMSRNO.Multiline = True
        Me.TXTFROMSRNO.Name = "TXTFROMSRNO"
        Me.TXTFROMSRNO.ReadOnly = True
        Me.TXTFROMSRNO.Size = New System.Drawing.Size(10, 19)
        Me.TXTFROMSRNO.TabIndex = 684
        Me.TXTFROMSRNO.Visible = False
        '
        'TXTFROMTYPE
        '
        Me.TXTFROMTYPE.BackColor = System.Drawing.Color.White
        Me.TXTFROMTYPE.ForeColor = System.Drawing.Color.DimGray
        Me.TXTFROMTYPE.Location = New System.Drawing.Point(582, 28)
        Me.TXTFROMTYPE.Multiline = True
        Me.TXTFROMTYPE.Name = "TXTFROMTYPE"
        Me.TXTFROMTYPE.ReadOnly = True
        Me.TXTFROMTYPE.Size = New System.Drawing.Size(10, 19)
        Me.TXTFROMTYPE.TabIndex = 683
        Me.TXTFROMTYPE.Visible = False
        '
        'TXTFROMNO
        '
        Me.TXTFROMNO.BackColor = System.Drawing.Color.White
        Me.TXTFROMNO.ForeColor = System.Drawing.Color.DimGray
        Me.TXTFROMNO.Location = New System.Drawing.Point(556, 28)
        Me.TXTFROMNO.Multiline = True
        Me.TXTFROMNO.Name = "TXTFROMNO"
        Me.TXTFROMNO.ReadOnly = True
        Me.TXTFROMNO.Size = New System.Drawing.Size(14, 19)
        Me.TXTFROMNO.TabIndex = 682
        Me.TXTFROMNO.Visible = False
        '
        'TXTRUNNINGBAL
        '
        Me.TXTRUNNINGBAL.BackColor = System.Drawing.Color.Linen
        Me.TXTRUNNINGBAL.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTRUNNINGBAL.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTRUNNINGBAL.Location = New System.Drawing.Point(753, 119)
        Me.TXTRUNNINGBAL.Name = "TXTRUNNINGBAL"
        Me.TXTRUNNINGBAL.ReadOnly = True
        Me.TXTRUNNINGBAL.Size = New System.Drawing.Size(65, 23)
        Me.TXTRUNNINGBAL.TabIndex = 680
        Me.TXTRUNNINGBAL.TabStop = False
        Me.TXTRUNNINGBAL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CMBISSUENO
        '
        Me.CMBISSUENO.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBISSUENO.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBISSUENO.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBISSUENO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBISSUENO.FormattingEnabled = True
        Me.CMBISSUENO.Location = New System.Drawing.Point(417, 61)
        Me.CMBISSUENO.MaxDropDownItems = 14
        Me.CMBISSUENO.Name = "CMBISSUENO"
        Me.CMBISSUENO.Size = New System.Drawing.Size(79, 23)
        Me.CMBISSUENO.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(652, 123)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(99, 15)
        Me.Label2.TabIndex = 681
        Me.Label2.Text = "Running Balance"
        '
        'LBLLOTNO
        '
        Me.LBLLOTNO.AutoSize = True
        Me.LBLLOTNO.BackColor = System.Drawing.Color.Transparent
        Me.LBLLOTNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLLOTNO.ForeColor = System.Drawing.Color.Black
        Me.LBLLOTNO.Location = New System.Drawing.Point(364, 65)
        Me.LBLLOTNO.Name = "LBLLOTNO"
        Me.LBLLOTNO.Size = New System.Drawing.Size(54, 15)
        Me.LBLLOTNO.TabIndex = 15
        Me.LBLLOTNO.Text = "Issue No"
        '
        'TXTACTUALISSUEWT
        '
        Me.TXTACTUALISSUEWT.BackColor = System.Drawing.Color.Linen
        Me.TXTACTUALISSUEWT.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTACTUALISSUEWT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTACTUALISSUEWT.Location = New System.Drawing.Point(753, 90)
        Me.TXTACTUALISSUEWT.Name = "TXTACTUALISSUEWT"
        Me.TXTACTUALISSUEWT.ReadOnly = True
        Me.TXTACTUALISSUEWT.Size = New System.Drawing.Size(65, 23)
        Me.TXTACTUALISSUEWT.TabIndex = 678
        Me.TXTACTUALISSUEWT.TabStop = False
        Me.TXTACTUALISSUEWT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CMBOURGODOWN
        '
        Me.CMBOURGODOWN.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBOURGODOWN.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBOURGODOWN.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBOURGODOWN.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBOURGODOWN.FormattingEnabled = True
        Me.CMBOURGODOWN.Location = New System.Drawing.Point(100, 61)
        Me.CMBOURGODOWN.MaxDropDownItems = 14
        Me.CMBOURGODOWN.Name = "CMBOURGODOWN"
        Me.CMBOURGODOWN.Size = New System.Drawing.Size(224, 23)
        Me.CMBOURGODOWN.TabIndex = 0
        Me.CMBOURGODOWN.TabStop = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(655, 94)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(96, 15)
        Me.Label7.TabIndex = 679
        Me.Label7.Text = "Acutal Issue Wt."
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.tbupload)
        Me.TabControl1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(19, 137)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(954, 306)
        Me.TabControl1.TabIndex = 5
        '
        'TabPage1
        '
        Me.TabPage1.AutoScroll = True
        Me.TabPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.TabPage1.Controls.Add(Me.TXTLOTNO)
        Me.TabPage1.Controls.Add(Me.TXTSRNO)
        Me.TabPage1.Controls.Add(Me.TXTTAREWT)
        Me.TabPage1.Controls.Add(Me.TXTBAGS)
        Me.TabPage1.Controls.Add(Me.TXTBAGNO)
        Me.TabPage1.Controls.Add(Me.TXTGROSSWT)
        Me.TabPage1.Controls.Add(Me.TXTNETTWT)
        Me.TabPage1.Controls.Add(Me.CMBCOLOR)
        Me.TabPage1.Controls.Add(Me.CMBQUALITY)
        Me.TabPage1.Controls.Add(Me.LBLTOTALTAREWT)
        Me.TabPage1.Controls.Add(Me.LBLTOTALGROSSWT)
        Me.TabPage1.Controls.Add(Me.GRIDYARN)
        Me.TabPage1.Controls.Add(Me.LBLTOTALNETTWT)
        Me.TabPage1.Controls.Add(Me.LBLTOTALCONES)
        Me.TabPage1.Controls.Add(Me.Label10)
        Me.TabPage1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPage1.Location = New System.Drawing.Point(4, 24)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(946, 278)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "1. Item Details"
        '
        'TXTLOTNO
        '
        Me.TXTLOTNO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTLOTNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTLOTNO.Location = New System.Drawing.Point(214, 3)
        Me.TXTLOTNO.MaxLength = 50
        Me.TXTLOTNO.Name = "TXTLOTNO"
        Me.TXTLOTNO.Size = New System.Drawing.Size(100, 23)
        Me.TXTLOTNO.TabIndex = 1
        Me.TXTLOTNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTSRNO
        '
        Me.TXTSRNO.BackColor = System.Drawing.Color.Linen
        Me.TXTSRNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTSRNO.Location = New System.Drawing.Point(6, 3)
        Me.TXTSRNO.Name = "TXTSRNO"
        Me.TXTSRNO.ReadOnly = True
        Me.TXTSRNO.Size = New System.Drawing.Size(30, 23)
        Me.TXTSRNO.TabIndex = 0
        Me.TXTSRNO.TabStop = False
        Me.TXTSRNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTTAREWT
        '
        Me.TXTTAREWT.BackColor = System.Drawing.Color.Linen
        Me.TXTTAREWT.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTTAREWT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTTAREWT.Location = New System.Drawing.Point(714, 3)
        Me.TXTTAREWT.MaxLength = 50
        Me.TXTTAREWT.Name = "TXTTAREWT"
        Me.TXTTAREWT.ReadOnly = True
        Me.TXTTAREWT.Size = New System.Drawing.Size(100, 23)
        Me.TXTTAREWT.TabIndex = 6
        Me.TXTTAREWT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTBAGS
        '
        Me.TXTBAGS.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTBAGS.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTBAGS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTBAGS.Location = New System.Drawing.Point(514, 3)
        Me.TXTBAGS.MaxLength = 50
        Me.TXTBAGS.Name = "TXTBAGS"
        Me.TXTBAGS.Size = New System.Drawing.Size(100, 23)
        Me.TXTBAGS.TabIndex = 4
        Me.TXTBAGS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTBAGNO
        '
        Me.TXTBAGNO.BackColor = System.Drawing.Color.White
        Me.TXTBAGNO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTBAGNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTBAGNO.Location = New System.Drawing.Point(414, 3)
        Me.TXTBAGNO.MaxLength = 50
        Me.TXTBAGNO.Name = "TXTBAGNO"
        Me.TXTBAGNO.Size = New System.Drawing.Size(100, 23)
        Me.TXTBAGNO.TabIndex = 3
        Me.TXTBAGNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTGROSSWT
        '
        Me.TXTGROSSWT.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTGROSSWT.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTGROSSWT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTGROSSWT.Location = New System.Drawing.Point(614, 3)
        Me.TXTGROSSWT.MaxLength = 50
        Me.TXTGROSSWT.Name = "TXTGROSSWT"
        Me.TXTGROSSWT.Size = New System.Drawing.Size(100, 23)
        Me.TXTGROSSWT.TabIndex = 5
        Me.TXTGROSSWT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTNETTWT
        '
        Me.TXTNETTWT.BackColor = System.Drawing.Color.Linen
        Me.TXTNETTWT.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTNETTWT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTNETTWT.Location = New System.Drawing.Point(814, 3)
        Me.TXTNETTWT.MaxLength = 50
        Me.TXTNETTWT.Name = "TXTNETTWT"
        Me.TXTNETTWT.ReadOnly = True
        Me.TXTNETTWT.Size = New System.Drawing.Size(100, 23)
        Me.TXTNETTWT.TabIndex = 7
        Me.TXTNETTWT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CMBCOLOR
        '
        Me.CMBCOLOR.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBCOLOR.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBCOLOR.BackColor = System.Drawing.Color.White
        Me.CMBCOLOR.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBCOLOR.FormattingEnabled = True
        Me.CMBCOLOR.Location = New System.Drawing.Point(314, 3)
        Me.CMBCOLOR.MaxDropDownItems = 14
        Me.CMBCOLOR.Name = "CMBCOLOR"
        Me.CMBCOLOR.Size = New System.Drawing.Size(100, 23)
        Me.CMBCOLOR.TabIndex = 2
        '
        'CMBQUALITY
        '
        Me.CMBQUALITY.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBQUALITY.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBQUALITY.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBQUALITY.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBQUALITY.FormattingEnabled = True
        Me.CMBQUALITY.Location = New System.Drawing.Point(34, 3)
        Me.CMBQUALITY.MaxDropDownItems = 14
        Me.CMBQUALITY.Name = "CMBQUALITY"
        Me.CMBQUALITY.Size = New System.Drawing.Size(180, 23)
        Me.CMBQUALITY.TabIndex = 0
        '
        'LBLTOTALTAREWT
        '
        Me.LBLTOTALTAREWT.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALTAREWT.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALTAREWT.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALTAREWT.Location = New System.Drawing.Point(755, 253)
        Me.LBLTOTALTAREWT.Name = "LBLTOTALTAREWT"
        Me.LBLTOTALTAREWT.Size = New System.Drawing.Size(58, 15)
        Me.LBLTOTALTAREWT.TabIndex = 13
        Me.LBLTOTALTAREWT.Text = "0"
        Me.LBLTOTALTAREWT.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LBLTOTALGROSSWT
        '
        Me.LBLTOTALGROSSWT.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALGROSSWT.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALGROSSWT.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALGROSSWT.Location = New System.Drawing.Point(628, 253)
        Me.LBLTOTALGROSSWT.Name = "LBLTOTALGROSSWT"
        Me.LBLTOTALGROSSWT.Size = New System.Drawing.Size(85, 15)
        Me.LBLTOTALGROSSWT.TabIndex = 12
        Me.LBLTOTALGROSSWT.Text = "0"
        Me.LBLTOTALGROSSWT.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GRIDYARN
        '
        Me.GRIDYARN.AllowUserToAddRows = False
        Me.GRIDYARN.AllowUserToDeleteRows = False
        Me.GRIDYARN.AllowUserToResizeColumns = False
        Me.GRIDYARN.AllowUserToResizeRows = False
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(248, Byte), Integer))
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Black
        Me.GRIDYARN.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle6
        Me.GRIDYARN.BackgroundColor = System.Drawing.Color.White
        Me.GRIDYARN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GRIDYARN.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.GRIDYARN.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.GRIDYARN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GRIDYARN.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.gsrno, Me.GYARNQUALITY, Me.GLOTNO, Me.GSHADE, Me.GBAGNO, Me.GBAGS, Me.GGROSSWT, Me.GTAREWT, Me.GNETTWT, Me.GOUTWT, Me.GFROMNO, Me.GFROMSRNO, Me.GFROMTYPE})
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle13.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDYARN.DefaultCellStyle = DataGridViewCellStyle13
        Me.GRIDYARN.GridColor = System.Drawing.SystemColors.Control
        Me.GRIDYARN.Location = New System.Drawing.Point(3, 26)
        Me.GRIDYARN.MultiSelect = False
        Me.GRIDYARN.Name = "GRIDYARN"
        Me.GRIDYARN.RowHeadersVisible = False
        Me.GRIDYARN.RowHeadersWidth = 30
        Me.GRIDYARN.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.Black
        DataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.White
        Me.GRIDYARN.RowsDefaultCellStyle = DataGridViewCellStyle14
        Me.GRIDYARN.RowTemplate.Height = 20
        Me.GRIDYARN.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDYARN.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.GRIDYARN.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.GRIDYARN.Size = New System.Drawing.Size(939, 224)
        Me.GRIDYARN.TabIndex = 8
        Me.GRIDYARN.TabStop = False
        '
        'gsrno
        '
        Me.gsrno.HeaderText = "Sr."
        Me.gsrno.Name = "gsrno"
        Me.gsrno.ReadOnly = True
        Me.gsrno.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gsrno.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.gsrno.Width = 30
        '
        'GYARNQUALITY
        '
        Me.GYARNQUALITY.HeaderText = "Yarn Quality"
        Me.GYARNQUALITY.Name = "GYARNQUALITY"
        Me.GYARNQUALITY.ReadOnly = True
        Me.GYARNQUALITY.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GYARNQUALITY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GYARNQUALITY.Width = 180
        '
        'GLOTNO
        '
        Me.GLOTNO.HeaderText = "Lot No"
        Me.GLOTNO.Name = "GLOTNO"
        Me.GLOTNO.ReadOnly = True
        Me.GLOTNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GLOTNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GSHADE
        '
        Me.GSHADE.HeaderText = "Shade"
        Me.GSHADE.Name = "GSHADE"
        Me.GSHADE.ReadOnly = True
        Me.GSHADE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GSHADE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GBAGNO
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.NullValue = Nothing
        Me.GBAGNO.DefaultCellStyle = DataGridViewCellStyle8
        Me.GBAGNO.HeaderText = "Bag No"
        Me.GBAGNO.Name = "GBAGNO"
        Me.GBAGNO.ReadOnly = True
        Me.GBAGNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GBAGNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GBAGS
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GBAGS.DefaultCellStyle = DataGridViewCellStyle9
        Me.GBAGS.HeaderText = "Bags"
        Me.GBAGS.Name = "GBAGS"
        Me.GBAGS.ReadOnly = True
        Me.GBAGS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GBAGS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GGROSSWT
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Calibri", 8.25!)
        Me.GGROSSWT.DefaultCellStyle = DataGridViewCellStyle10
        Me.GGROSSWT.HeaderText = "Gross Wt"
        Me.GGROSSWT.Name = "GGROSSWT"
        Me.GGROSSWT.ReadOnly = True
        Me.GGROSSWT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GGROSSWT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GTAREWT
        '
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GTAREWT.DefaultCellStyle = DataGridViewCellStyle11
        Me.GTAREWT.HeaderText = "Tare Wt."
        Me.GTAREWT.Name = "GTAREWT"
        Me.GTAREWT.ReadOnly = True
        Me.GTAREWT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GTAREWT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GNETTWT
        '
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GNETTWT.DefaultCellStyle = DataGridViewCellStyle12
        Me.GNETTWT.HeaderText = "Nett Wt."
        Me.GNETTWT.Name = "GNETTWT"
        Me.GNETTWT.ReadOnly = True
        Me.GNETTWT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GNETTWT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GOUTWT
        '
        Me.GOUTWT.HeaderText = "Out Wt"
        Me.GOUTWT.Name = "GOUTWT"
        Me.GOUTWT.Visible = False
        '
        'GFROMNO
        '
        Me.GFROMNO.HeaderText = "FROMNO"
        Me.GFROMNO.Name = "GFROMNO"
        Me.GFROMNO.Visible = False
        '
        'GFROMSRNO
        '
        Me.GFROMSRNO.HeaderText = "FROMSRNO"
        Me.GFROMSRNO.Name = "GFROMSRNO"
        Me.GFROMSRNO.Visible = False
        '
        'GFROMTYPE
        '
        Me.GFROMTYPE.HeaderText = "TYPE"
        Me.GFROMTYPE.Name = "GFROMTYPE"
        Me.GFROMTYPE.Visible = False
        '
        'LBLTOTALNETTWT
        '
        Me.LBLTOTALNETTWT.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALNETTWT.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALNETTWT.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALNETTWT.Location = New System.Drawing.Point(855, 250)
        Me.LBLTOTALNETTWT.Name = "LBLTOTALNETTWT"
        Me.LBLTOTALNETTWT.Size = New System.Drawing.Size(58, 15)
        Me.LBLTOTALNETTWT.TabIndex = 14
        Me.LBLTOTALNETTWT.Text = "0"
        Me.LBLTOTALNETTWT.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LBLTOTALCONES
        '
        Me.LBLTOTALCONES.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALCONES.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALCONES.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALCONES.Location = New System.Drawing.Point(556, 253)
        Me.LBLTOTALCONES.Name = "LBLTOTALCONES"
        Me.LBLTOTALCONES.Size = New System.Drawing.Size(58, 15)
        Me.LBLTOTALCONES.TabIndex = 11
        Me.LBLTOTALCONES.Text = "0"
        Me.LBLTOTALCONES.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(454, 253)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(31, 14)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "Total"
        '
        'tbupload
        '
        Me.tbupload.BackColor = System.Drawing.Color.Linen
        Me.tbupload.Controls.Add(Me.CMDUPLOAD)
        Me.tbupload.Controls.Add(Me.CMDVIEW)
        Me.tbupload.Controls.Add(Me.CMDREMOVE)
        Me.tbupload.Controls.Add(Me.TXTNEWIMGPATH)
        Me.tbupload.Controls.Add(Me.TXTFILENAME)
        Me.tbupload.Controls.Add(Me.txtimgpath)
        Me.tbupload.Controls.Add(Me.txtuploadname)
        Me.tbupload.Controls.Add(Me.txtuploadsrno)
        Me.tbupload.Controls.Add(Me.txtuploadremarks)
        Me.tbupload.Controls.Add(Me.gridupload)
        Me.tbupload.Controls.Add(Me.Label8)
        Me.tbupload.Controls.Add(Me.PBSoftCopy)
        Me.tbupload.Location = New System.Drawing.Point(4, 24)
        Me.tbupload.Name = "tbupload"
        Me.tbupload.Padding = New System.Windows.Forms.Padding(3)
        Me.tbupload.Size = New System.Drawing.Size(946, 278)
        Me.tbupload.TabIndex = 2
        Me.tbupload.Text = "2. Scan Documents"
        '
        'CMDUPLOAD
        '
        Me.CMDUPLOAD.BackColor = System.Drawing.Color.Transparent
        Me.CMDUPLOAD.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDUPLOAD.FlatAppearance.BorderSize = 0
        Me.CMDUPLOAD.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDUPLOAD.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.CMDUPLOAD.Location = New System.Drawing.Point(682, 24)
        Me.CMDUPLOAD.Name = "CMDUPLOAD"
        Me.CMDUPLOAD.Size = New System.Drawing.Size(80, 28)
        Me.CMDUPLOAD.TabIndex = 758
        Me.CMDUPLOAD.Text = "&Upload"
        Me.CMDUPLOAD.UseVisualStyleBackColor = False
        '
        'CMDVIEW
        '
        Me.CMDVIEW.BackColor = System.Drawing.Color.Transparent
        Me.CMDVIEW.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDVIEW.FlatAppearance.BorderSize = 0
        Me.CMDVIEW.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDVIEW.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.CMDVIEW.Location = New System.Drawing.Point(596, 24)
        Me.CMDVIEW.Name = "CMDVIEW"
        Me.CMDVIEW.Size = New System.Drawing.Size(80, 28)
        Me.CMDVIEW.TabIndex = 757
        Me.CMDVIEW.Text = "&View"
        Me.CMDVIEW.UseVisualStyleBackColor = False
        '
        'CMDREMOVE
        '
        Me.CMDREMOVE.BackColor = System.Drawing.Color.Transparent
        Me.CMDREMOVE.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDREMOVE.FlatAppearance.BorderSize = 0
        Me.CMDREMOVE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDREMOVE.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.CMDREMOVE.Location = New System.Drawing.Point(510, 24)
        Me.CMDREMOVE.Name = "CMDREMOVE"
        Me.CMDREMOVE.Size = New System.Drawing.Size(80, 28)
        Me.CMDREMOVE.TabIndex = 756
        Me.CMDREMOVE.Text = "&Remove"
        Me.CMDREMOVE.UseVisualStyleBackColor = False
        '
        'TXTNEWIMGPATH
        '
        Me.TXTNEWIMGPATH.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTNEWIMGPATH.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TXTNEWIMGPATH.Location = New System.Drawing.Point(474, 3)
        Me.TXTNEWIMGPATH.Multiline = True
        Me.TXTNEWIMGPATH.Name = "TXTNEWIMGPATH"
        Me.TXTNEWIMGPATH.Size = New System.Drawing.Size(27, 22)
        Me.TXTNEWIMGPATH.TabIndex = 445
        Me.TXTNEWIMGPATH.Visible = False
        '
        'TXTFILENAME
        '
        Me.TXTFILENAME.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTFILENAME.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TXTFILENAME.Location = New System.Drawing.Point(456, 4)
        Me.TXTFILENAME.Multiline = True
        Me.TXTFILENAME.Name = "TXTFILENAME"
        Me.TXTFILENAME.Size = New System.Drawing.Size(10, 22)
        Me.TXTFILENAME.TabIndex = 444
        Me.TXTFILENAME.Visible = False
        '
        'txtimgpath
        '
        Me.txtimgpath.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtimgpath.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtimgpath.Location = New System.Drawing.Point(464, 6)
        Me.txtimgpath.Multiline = True
        Me.txtimgpath.Name = "txtimgpath"
        Me.txtimgpath.Size = New System.Drawing.Size(12, 22)
        Me.txtimgpath.TabIndex = 443
        Me.txtimgpath.Visible = False
        '
        'txtuploadname
        '
        Me.txtuploadname.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtuploadname.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtuploadname.Location = New System.Drawing.Point(250, 1)
        Me.txtuploadname.Name = "txtuploadname"
        Me.txtuploadname.Size = New System.Drawing.Size(200, 22)
        Me.txtuploadname.TabIndex = 3
        '
        'txtuploadsrno
        '
        Me.txtuploadsrno.BackColor = System.Drawing.Color.Linen
        Me.txtuploadsrno.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtuploadsrno.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtuploadsrno.Location = New System.Drawing.Point(2, 1)
        Me.txtuploadsrno.Name = "txtuploadsrno"
        Me.txtuploadsrno.ReadOnly = True
        Me.txtuploadsrno.Size = New System.Drawing.Size(50, 22)
        Me.txtuploadsrno.TabIndex = 0
        Me.txtuploadsrno.TabStop = False
        '
        'txtuploadremarks
        '
        Me.txtuploadremarks.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtuploadremarks.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtuploadremarks.Location = New System.Drawing.Point(52, 1)
        Me.txtuploadremarks.Name = "txtuploadremarks"
        Me.txtuploadremarks.Size = New System.Drawing.Size(200, 22)
        Me.txtuploadremarks.TabIndex = 1
        '
        'gridupload
        '
        Me.gridupload.AllowUserToAddRows = False
        Me.gridupload.AllowUserToDeleteRows = False
        Me.gridupload.AllowUserToResizeColumns = False
        Me.gridupload.AllowUserToResizeRows = False
        DataGridViewCellStyle15.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(248, Byte), Integer))
        DataGridViewCellStyle15.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.Black
        Me.gridupload.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle15
        Me.gridupload.BackgroundColor = System.Drawing.Color.White
        Me.gridupload.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.gridupload.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle16.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.gridupload.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle16
        Me.gridupload.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gridupload.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GUSRNO, Me.GUREMARKS, Me.GUNAME, Me.GUIMGPATH, Me.GUNEWIMGPATH, Me.GUFILENAME})
        DataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle17.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gridupload.DefaultCellStyle = DataGridViewCellStyle17
        Me.gridupload.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.gridupload.GridColor = System.Drawing.SystemColors.Control
        Me.gridupload.Location = New System.Drawing.Point(2, 23)
        Me.gridupload.MultiSelect = False
        Me.gridupload.Name = "gridupload"
        Me.gridupload.RowHeadersVisible = False
        Me.gridupload.RowHeadersWidth = 30
        Me.gridupload.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle18.SelectionBackColor = System.Drawing.Color.Black
        DataGridViewCellStyle18.SelectionForeColor = System.Drawing.Color.White
        Me.gridupload.RowsDefaultCellStyle = DataGridViewCellStyle18
        Me.gridupload.RowTemplate.Height = 20
        Me.gridupload.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gridupload.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.gridupload.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.gridupload.Size = New System.Drawing.Size(477, 189)
        Me.gridupload.TabIndex = 4
        Me.gridupload.TabStop = False
        '
        'GUSRNO
        '
        Me.GUSRNO.HeaderText = "Sr."
        Me.GUSRNO.Name = "GUSRNO"
        Me.GUSRNO.ReadOnly = True
        Me.GUSRNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GUSRNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GUSRNO.Width = 50
        '
        'GUREMARKS
        '
        Me.GUREMARKS.HeaderText = "Remarks"
        Me.GUREMARKS.Name = "GUREMARKS"
        Me.GUREMARKS.ReadOnly = True
        Me.GUREMARKS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GUREMARKS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GUREMARKS.Width = 200
        '
        'GUNAME
        '
        Me.GUNAME.HeaderText = "Name"
        Me.GUNAME.Name = "GUNAME"
        Me.GUNAME.ReadOnly = True
        Me.GUNAME.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GUNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GUNAME.Width = 200
        '
        'GUIMGPATH
        '
        Me.GUIMGPATH.HeaderText = "ImgPath"
        Me.GUIMGPATH.Name = "GUIMGPATH"
        Me.GUIMGPATH.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GUIMGPATH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.GUIMGPATH.Visible = False
        '
        'GUNEWIMGPATH
        '
        Me.GUNEWIMGPATH.HeaderText = "New Img Path"
        Me.GUNEWIMGPATH.Name = "GUNEWIMGPATH"
        Me.GUNEWIMGPATH.Visible = False
        '
        'GUFILENAME
        '
        Me.GUFILENAME.HeaderText = "File Name"
        Me.GUFILENAME.Name = "GUFILENAME"
        Me.GUFILENAME.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(507, 4)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(110, 14)
        Me.Label8.TabIndex = 442
        Me.Label8.Text = "Upload Soft Copies"
        '
        'PBSoftCopy
        '
        Me.PBSoftCopy.BackColor = System.Drawing.Color.Transparent
        Me.PBSoftCopy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PBSoftCopy.Location = New System.Drawing.Point(479, 59)
        Me.PBSoftCopy.Name = "PBSoftCopy"
        Me.PBSoftCopy.Size = New System.Drawing.Size(156, 153)
        Me.PBSoftCopy.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PBSoftCopy.TabIndex = 441
        Me.PBSoftCopy.TabStop = False
        '
        'TXTBALWT
        '
        Me.TXTBALWT.BackColor = System.Drawing.Color.Linen
        Me.TXTBALWT.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTBALWT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTBALWT.Location = New System.Drawing.Point(753, 61)
        Me.TXTBALWT.Name = "TXTBALWT"
        Me.TXTBALWT.ReadOnly = True
        Me.TXTBALWT.Size = New System.Drawing.Size(65, 23)
        Me.TXTBALWT.TabIndex = 676
        Me.TXTBALWT.TabStop = False
        Me.TXTBALWT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LBLPROCESS
        '
        Me.LBLPROCESS.AutoSize = True
        Me.LBLPROCESS.BackColor = System.Drawing.Color.Transparent
        Me.LBLPROCESS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLPROCESS.ForeColor = System.Drawing.Color.Black
        Me.LBLPROCESS.Location = New System.Drawing.Point(8, 94)
        Me.LBLPROCESS.Name = "LBLPROCESS"
        Me.LBLPROCESS.Size = New System.Drawing.Size(89, 15)
        Me.LBLPROCESS.TabIndex = 14
        Me.LBLPROCESS.Text = "Machine Name"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(678, 65)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 15)
        Me.Label4.TabIndex = 677
        Me.Label4.Text = "Balance Wt."
        '
        'CMBMACHINE
        '
        Me.CMBMACHINE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBMACHINE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBMACHINE.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBMACHINE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBMACHINE.FormattingEnabled = True
        Me.CMBMACHINE.Location = New System.Drawing.Point(100, 90)
        Me.CMBMACHINE.MaxDropDownItems = 14
        Me.CMBMACHINE.Name = "CMBMACHINE"
        Me.CMBMACHINE.Size = New System.Drawing.Size(224, 23)
        Me.CMBMACHINE.TabIndex = 2
        '
        'RECEIVEDDATE
        '
        Me.RECEIVEDDATE.AsciiOnly = True
        Me.RECEIVEDDATE.BackColor = System.Drawing.Color.LemonChiffon
        Me.RECEIVEDDATE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RECEIVEDDATE.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite
        Me.RECEIVEDDATE.Location = New System.Drawing.Point(891, 90)
        Me.RECEIVEDDATE.Mask = "00/00/0000"
        Me.RECEIVEDDATE.Name = "RECEIVEDDATE"
        Me.RECEIVEDDATE.Size = New System.Drawing.Size(82, 23)
        Me.RECEIVEDDATE.TabIndex = 1
        Me.RECEIVEDDATE.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals
        Me.RECEIVEDDATE.ValidatingType = GetType(Date)
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Red
        Me.Label15.Location = New System.Drawing.Point(780, 446)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(44, 15)
        Me.Label15.TabIndex = 20
        Me.Label15.Text = "Locked"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Label23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label23.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(756, 446)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(18, 17)
        Me.Label23.TabIndex = 19
        Me.Label23.Text = "   "
        '
        'PBlock
        '
        Me.PBlock.BackColor = System.Drawing.Color.Transparent
        Me.PBlock.Image = Global.PROCESS.My.Resources.Resources.lock_copy
        Me.PBlock.Location = New System.Drawing.Point(756, 466)
        Me.PBlock.Name = "PBlock"
        Me.PBlock.Size = New System.Drawing.Size(60, 60)
        Me.PBlock.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PBlock.TabIndex = 446
        Me.PBlock.TabStop = False
        Me.PBlock.Visible = False
        '
        'TXTRECNO
        '
        Me.TXTRECNO.BackColor = System.Drawing.Color.Linen
        Me.TXTRECNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTRECNO.Location = New System.Drawing.Point(891, 61)
        Me.TXTRECNO.Name = "TXTRECNO"
        Me.TXTRECNO.ReadOnly = True
        Me.TXTRECNO.Size = New System.Drawing.Size(82, 23)
        Me.TXTRECNO.TabIndex = 17
        Me.TXTRECNO.TabStop = False
        Me.TXTRECNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(849, 65)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(38, 15)
        Me.Label12.TabIndex = 16
        Me.Label12.Text = "Sr. No"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(856, 94)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(32, 15)
        Me.Label9.TabIndex = 18
        Me.Label9.Text = "Date"
        '
        'cmddelete
        '
        Me.cmddelete.BackColor = System.Drawing.Color.Transparent
        Me.cmddelete.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmddelete.FlatAppearance.BorderSize = 0
        Me.cmddelete.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmddelete.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmddelete.Location = New System.Drawing.Point(434, 500)
        Me.cmddelete.Name = "cmddelete"
        Me.cmddelete.Size = New System.Drawing.Size(80, 28)
        Me.cmddelete.TabIndex = 9
        Me.cmddelete.Text = "&Delete"
        Me.cmddelete.UseVisualStyleBackColor = False
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox5.Controls.Add(Me.txtremarks)
        Me.GroupBox5.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.ForeColor = System.Drawing.Color.Black
        Me.GroupBox5.Location = New System.Drawing.Point(28, 455)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(264, 95)
        Me.GroupBox5.TabIndex = 6
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Remarks"
        '
        'txtremarks
        '
        Me.txtremarks.ForeColor = System.Drawing.Color.DimGray
        Me.txtremarks.Location = New System.Drawing.Point(5, 21)
        Me.txtremarks.Multiline = True
        Me.txtremarks.Name = "txtremarks"
        Me.txtremarks.Size = New System.Drawing.Size(248, 66)
        Me.txtremarks.TabIndex = 0
        '
        'cmdclear
        '
        Me.cmdclear.BackColor = System.Drawing.Color.Transparent
        Me.cmdclear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdclear.FlatAppearance.BorderSize = 0
        Me.cmdclear.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdclear.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmdclear.Location = New System.Drawing.Point(520, 466)
        Me.cmdclear.Name = "cmdclear"
        Me.cmdclear.Size = New System.Drawing.Size(80, 28)
        Me.cmdclear.TabIndex = 8
        Me.cmdclear.Text = "&Clear"
        Me.cmdclear.UseVisualStyleBackColor = False
        '
        'CMDOK
        '
        Me.CMDOK.BackColor = System.Drawing.Color.Transparent
        Me.CMDOK.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDOK.FlatAppearance.BorderSize = 0
        Me.CMDOK.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDOK.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.CMDOK.Location = New System.Drawing.Point(434, 466)
        Me.CMDOK.Name = "CMDOK"
        Me.CMDOK.Size = New System.Drawing.Size(80, 28)
        Me.CMDOK.TabIndex = 7
        Me.CMDOK.Text = "&Save"
        Me.CMDOK.UseVisualStyleBackColor = False
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdexit.FlatAppearance.BorderSize = 0
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmdexit.Location = New System.Drawing.Point(520, 500)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 28)
        Me.cmdexit.TabIndex = 10
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'tstxtbillno
        '
        Me.tstxtbillno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tstxtbillno.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstxtbillno.Location = New System.Drawing.Point(239, 1)
        Me.tstxtbillno.Name = "tstxtbillno"
        Me.tstxtbillno.Size = New System.Drawing.Size(74, 22)
        Me.tstxtbillno.TabIndex = 11
        Me.tstxtbillno.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbllocked
        '
        Me.lbllocked.AutoSize = True
        Me.lbllocked.BackColor = System.Drawing.Color.Transparent
        Me.lbllocked.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbllocked.ForeColor = System.Drawing.Color.Red
        Me.lbllocked.Location = New System.Drawing.Point(822, 461)
        Me.lbllocked.Name = "lbllocked"
        Me.lbllocked.Size = New System.Drawing.Size(82, 29)
        Me.lbllocked.TabIndex = 21
        Me.lbllocked.Text = "Locked"
        Me.lbllocked.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(46, 65)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(52, 15)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Godown"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenToolStripButton, Me.SaveToolStripButton, Me.PrintToolStripButton, Me.tooldelete, Me.toolStripSeparator, Me.toolprevious, Me.toolnext, Me.ToolStripSeparator1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1010, 25)
        Me.ToolStrip1.TabIndex = 12
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
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'YarnReceivedMachine
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1010, 562)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "YarnReceivedMachine"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Yarn Received Machine"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.GRIDYARNCOMP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.GRIDYARN, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbupload.ResumeLayout(False)
        Me.tbupload.PerformLayout()
        CType(Me.gridupload, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PBSoftCopy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PBlock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents EP As ErrorProvider
    Friend WithEvents BlendPanel1 As BlendPanel
    Friend WithEvents CMBOURGODOWN As ComboBox
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents GRIDYARN As DataGridView
    Friend WithEvents LBLTOTALNETTWT As Label
    Friend WithEvents LBLTOTALCONES As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents tbupload As TabPage
    Friend WithEvents TXTNEWIMGPATH As TextBox
    Friend WithEvents TXTFILENAME As TextBox
    Friend WithEvents txtimgpath As TextBox
    Friend WithEvents txtuploadname As TextBox
    Friend WithEvents txtuploadsrno As TextBox
    Friend WithEvents txtuploadremarks As TextBox
    Friend WithEvents gridupload As DataGridView
    Friend WithEvents GUSRNO As DataGridViewTextBoxColumn
    Friend WithEvents GUREMARKS As DataGridViewTextBoxColumn
    Friend WithEvents GUNAME As DataGridViewTextBoxColumn
    Friend WithEvents GUIMGPATH As DataGridViewImageColumn
    Friend WithEvents GUNEWIMGPATH As DataGridViewTextBoxColumn
    Friend WithEvents GUFILENAME As DataGridViewTextBoxColumn
    Friend WithEvents Label8 As Label
    Friend WithEvents PBSoftCopy As PictureBox
    Friend WithEvents LBLPROCESS As Label
    Friend WithEvents CMBMACHINE As ComboBox
    Friend WithEvents RECEIVEDDATE As MaskedTextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents Label23 As Label
    Friend WithEvents PBlock As PictureBox
    Friend WithEvents TXTRECNO As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents cmddelete As Button
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents txtremarks As TextBox
    Friend WithEvents cmdclear As Button
    Friend WithEvents CMDOK As Button
    Friend WithEvents cmdexit As Button
    Friend WithEvents tstxtbillno As TextBox
    Friend WithEvents lbllocked As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents OpenToolStripButton As ToolStripButton
    Friend WithEvents SaveToolStripButton As ToolStripButton
    Friend WithEvents PrintToolStripButton As ToolStripButton
    Friend WithEvents tooldelete As ToolStripButton
    Friend WithEvents toolStripSeparator As ToolStripSeparator
    Friend WithEvents toolprevious As ToolStripButton
    Friend WithEvents toolnext As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents LBLLOTNO As Label
    Friend WithEvents LBLTOTALTAREWT As Label
    Friend WithEvents LBLTOTALGROSSWT As Label
    Friend WithEvents CMBQUALITY As ComboBox
    Friend WithEvents CMBCOLOR As ComboBox
    Friend WithEvents TXTNETTWT As TextBox
    Friend WithEvents TXTTAREWT As TextBox
    Friend WithEvents TXTBAGS As TextBox
    Friend WithEvents TXTBAGNO As TextBox
    Friend WithEvents TXTGROSSWT As TextBox
    Friend WithEvents TXTSRNO As TextBox
    Friend WithEvents CMDREMOVE As Button
    Friend WithEvents CMDVIEW As Button
    Friend WithEvents CMDUPLOAD As Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents CMBISSUENO As ComboBox
    Friend WithEvents TXTLOTNO As TextBox
    Friend WithEvents TXTRUNNINGBAL As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TXTACTUALISSUEWT As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TXTBALWT As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TXTFROMNO As TextBox
    Friend WithEvents TXTFROMTYPE As TextBox
    Friend WithEvents TXTFROMSRNO As TextBox
    Friend WithEvents TXTTYPE As TextBox
    Friend WithEvents GRIDYARNCOMP As DataGridView
    Friend WithEvents CYARNQUALITY As DataGridViewTextBoxColumn
    Friend WithEvents CWT As DataGridViewTextBoxColumn
    Friend WithEvents TXTLABOURNAME As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents gsrno As DataGridViewTextBoxColumn
    Friend WithEvents GYARNQUALITY As DataGridViewTextBoxColumn
    Friend WithEvents GLOTNO As DataGridViewTextBoxColumn
    Friend WithEvents GSHADE As DataGridViewTextBoxColumn
    Friend WithEvents GBAGNO As DataGridViewTextBoxColumn
    Friend WithEvents GBAGS As DataGridViewTextBoxColumn
    Friend WithEvents GGROSSWT As DataGridViewTextBoxColumn
    Friend WithEvents GTAREWT As DataGridViewTextBoxColumn
    Friend WithEvents GNETTWT As DataGridViewTextBoxColumn
    Friend WithEvents GOUTWT As DataGridViewTextBoxColumn
    Friend WithEvents GFROMNO As DataGridViewTextBoxColumn
    Friend WithEvents GFROMSRNO As DataGridViewTextBoxColumn
    Friend WithEvents GFROMTYPE As DataGridViewTextBoxColumn
End Class
