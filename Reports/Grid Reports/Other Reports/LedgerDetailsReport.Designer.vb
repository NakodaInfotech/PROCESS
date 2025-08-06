<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LedgerDetailsReport
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
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.CMDSAVE = New System.Windows.Forms.Button()
        Me.CMDPRINT = New System.Windows.Forms.Button()
        Me.gridbilldetails = New DevExpress.XtraGrid.GridControl()
        Me.gridbill = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GCMPNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGROUP = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GOPBAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDRCR = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GAREA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCITY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSTATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCOUNTRY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRESINO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GALTNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPHONE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GMOBILENO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBOSSMOBILE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFAX = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GWEBSITE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GEMAIL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPANNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGSTIN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GADD = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTYPE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSECONDARY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CMDEXIT = New System.Windows.Forms.Button()
        Me.BlendPanel1.SuspendLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.CMDSAVE)
        Me.BlendPanel1.Controls.Add(Me.CMDPRINT)
        Me.BlendPanel1.Controls.Add(Me.gridbilldetails)
        Me.BlendPanel1.Controls.Add(Me.CMDEXIT)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1184, 581)
        Me.BlendPanel1.TabIndex = 5
        '
        'CMDSAVE
        '
        Me.CMDSAVE.Location = New System.Drawing.Point(67, 7)
        Me.CMDSAVE.Name = "CMDSAVE"
        Me.CMDSAVE.Size = New System.Drawing.Size(75, 23)
        Me.CMDSAVE.TabIndex = 502
        Me.CMDSAVE.Text = "&Refresh"
        Me.CMDSAVE.UseVisualStyleBackColor = True
        '
        'CMDPRINT
        '
        Me.CMDPRINT.BackColor = System.Drawing.Color.Transparent
        Me.CMDPRINT.BackgroundImage = Global.PROCESS.My.Resources.Resources.Excel_icon
        Me.CMDPRINT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CMDPRINT.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDPRINT.FlatAppearance.BorderSize = 0
        Me.CMDPRINT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CMDPRINT.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDPRINT.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.CMDPRINT.Location = New System.Drawing.Point(25, 9)
        Me.CMDPRINT.Name = "CMDPRINT"
        Me.CMDPRINT.Size = New System.Drawing.Size(25, 21)
        Me.CMDPRINT.TabIndex = 501
        Me.CMDPRINT.UseVisualStyleBackColor = False
        '
        'gridbilldetails
        '
        Me.gridbilldetails.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbilldetails.Location = New System.Drawing.Point(19, 36)
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.Size = New System.Drawing.Size(1153, 503)
        Me.gridbilldetails.TabIndex = 494
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill})
        '
        'gridbill
        '
        Me.gridbill.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GCMPNAME, Me.GGROUP, Me.GOPBAL, Me.GDRCR, Me.GAREA, Me.GCITY, Me.GSTATE, Me.GCOUNTRY, Me.GRESINO, Me.GALTNO, Me.GPHONE, Me.GMOBILENO, Me.GBOSSMOBILE, Me.GFAX, Me.GWEBSITE, Me.GEMAIL, Me.GPANNO, Me.GGSTIN, Me.GADD, Me.GTYPE, Me.GSECONDARY})
        Me.gridbill.GridControl = Me.gridbilldetails
        Me.gridbill.Name = "gridbill"
        Me.gridbill.OptionsBehavior.AllowIncrementalSearch = True
        Me.gridbill.OptionsBehavior.Editable = False
        Me.gridbill.OptionsView.ColumnAutoWidth = False
        Me.gridbill.OptionsView.ShowAutoFilterRow = True
        '
        'GCMPNAME
        '
        Me.GCMPNAME.Caption = "Cmp Name"
        Me.GCMPNAME.FieldName = "CMPNAME"
        Me.GCMPNAME.Name = "GCMPNAME"
        Me.GCMPNAME.Visible = True
        Me.GCMPNAME.VisibleIndex = 0
        Me.GCMPNAME.Width = 180
        '
        'GGROUP
        '
        Me.GGROUP.Caption = "Group Name"
        Me.GGROUP.FieldName = "GROUPNAME"
        Me.GGROUP.Name = "GGROUP"
        Me.GGROUP.Visible = True
        Me.GGROUP.VisibleIndex = 1
        Me.GGROUP.Width = 100
        '
        'GOPBAL
        '
        Me.GOPBAL.Caption = "O/P Bal"
        Me.GOPBAL.FieldName = "OPBAL"
        Me.GOPBAL.Name = "GOPBAL"
        Me.GOPBAL.Visible = True
        Me.GOPBAL.VisibleIndex = 2
        '
        'GDRCR
        '
        Me.GDRCR.Caption = "Dr/Cr"
        Me.GDRCR.FieldName = "DRCR"
        Me.GDRCR.Name = "GDRCR"
        Me.GDRCR.Visible = True
        Me.GDRCR.VisibleIndex = 3
        Me.GDRCR.Width = 40
        '
        'GAREA
        '
        Me.GAREA.Caption = "Area"
        Me.GAREA.FieldName = "AREA"
        Me.GAREA.Name = "GAREA"
        Me.GAREA.Visible = True
        Me.GAREA.VisibleIndex = 4
        Me.GAREA.Width = 100
        '
        'GCITY
        '
        Me.GCITY.Caption = "City"
        Me.GCITY.FieldName = "CITY"
        Me.GCITY.Name = "GCITY"
        Me.GCITY.Visible = True
        Me.GCITY.VisibleIndex = 5
        Me.GCITY.Width = 100
        '
        'GSTATE
        '
        Me.GSTATE.Caption = "State"
        Me.GSTATE.FieldName = "STATE"
        Me.GSTATE.Name = "GSTATE"
        Me.GSTATE.Visible = True
        Me.GSTATE.VisibleIndex = 6
        Me.GSTATE.Width = 100
        '
        'GCOUNTRY
        '
        Me.GCOUNTRY.Caption = "Country"
        Me.GCOUNTRY.FieldName = "COUNTRY"
        Me.GCOUNTRY.Name = "GCOUNTRY"
        Me.GCOUNTRY.Visible = True
        Me.GCOUNTRY.VisibleIndex = 7
        '
        'GRESINO
        '
        Me.GRESINO.Caption = "Resi No"
        Me.GRESINO.FieldName = "RESINO"
        Me.GRESINO.Name = "GRESINO"
        Me.GRESINO.Visible = True
        Me.GRESINO.VisibleIndex = 8
        Me.GRESINO.Width = 90
        '
        'GALTNO
        '
        Me.GALTNO.Caption = "Alt No"
        Me.GALTNO.FieldName = "ALTNO"
        Me.GALTNO.Name = "GALTNO"
        Me.GALTNO.Visible = True
        Me.GALTNO.VisibleIndex = 9
        '
        'GPHONE
        '
        Me.GPHONE.Caption = "Tel No"
        Me.GPHONE.FieldName = "PHONENO"
        Me.GPHONE.Name = "GPHONE"
        Me.GPHONE.Visible = True
        Me.GPHONE.VisibleIndex = 10
        Me.GPHONE.Width = 90
        '
        'GMOBILENO
        '
        Me.GMOBILENO.Caption = "Mobile No"
        Me.GMOBILENO.FieldName = "MOBILE"
        Me.GMOBILENO.Name = "GMOBILENO"
        Me.GMOBILENO.Visible = True
        Me.GMOBILENO.VisibleIndex = 11
        Me.GMOBILENO.Width = 90
        '
        'GBOSSMOBILE
        '
        Me.GBOSSMOBILE.Caption = "Boss Mobile"
        Me.GBOSSMOBILE.FieldName = "BOSSMOBILE"
        Me.GBOSSMOBILE.Name = "GBOSSMOBILE"
        Me.GBOSSMOBILE.Visible = True
        Me.GBOSSMOBILE.VisibleIndex = 12
        Me.GBOSSMOBILE.Width = 90
        '
        'GFAX
        '
        Me.GFAX.Caption = "Fax"
        Me.GFAX.FieldName = "FAX"
        Me.GFAX.Name = "GFAX"
        Me.GFAX.Visible = True
        Me.GFAX.VisibleIndex = 13
        Me.GFAX.Width = 90
        '
        'GWEBSITE
        '
        Me.GWEBSITE.Caption = "Website"
        Me.GWEBSITE.FieldName = "WEBSITE"
        Me.GWEBSITE.Name = "GWEBSITE"
        Me.GWEBSITE.Visible = True
        Me.GWEBSITE.VisibleIndex = 14
        Me.GWEBSITE.Width = 150
        '
        'GEMAIL
        '
        Me.GEMAIL.Caption = "Email"
        Me.GEMAIL.FieldName = "EMAIL"
        Me.GEMAIL.Name = "GEMAIL"
        Me.GEMAIL.Visible = True
        Me.GEMAIL.VisibleIndex = 15
        Me.GEMAIL.Width = 150
        '
        'GPANNO
        '
        Me.GPANNO.Caption = "PAN No"
        Me.GPANNO.FieldName = "PANNO"
        Me.GPANNO.Name = "GPANNO"
        Me.GPANNO.Visible = True
        Me.GPANNO.VisibleIndex = 16
        Me.GPANNO.Width = 100
        '
        'GGSTIN
        '
        Me.GGSTIN.Caption = "GSTIN No"
        Me.GGSTIN.FieldName = "GSTIN"
        Me.GGSTIN.Name = "GGSTIN"
        Me.GGSTIN.Visible = True
        Me.GGSTIN.VisibleIndex = 17
        '
        'GADD
        '
        Me.GADD.Caption = "Address"
        Me.GADD.FieldName = "ADD"
        Me.GADD.Name = "GADD"
        Me.GADD.Visible = True
        Me.GADD.VisibleIndex = 18
        Me.GADD.Width = 200
        '
        'GTYPE
        '
        Me.GTYPE.Caption = "Type"
        Me.GTYPE.FieldName = "TYPE"
        Me.GTYPE.Name = "GTYPE"
        Me.GTYPE.Visible = True
        Me.GTYPE.VisibleIndex = 19
        Me.GTYPE.Width = 90
        '
        'GSECONDARY
        '
        Me.GSECONDARY.Caption = "Secondary"
        Me.GSECONDARY.FieldName = "SECONDARY"
        Me.GSECONDARY.Name = "GSECONDARY"
        Me.GSECONDARY.Visible = True
        Me.GSECONDARY.VisibleIndex = 20
        Me.GSECONDARY.Width = 100
        '
        'CMDEXIT
        '
        Me.CMDEXIT.BackColor = System.Drawing.Color.Transparent
        Me.CMDEXIT.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDEXIT.FlatAppearance.BorderSize = 0
        Me.CMDEXIT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDEXIT.ForeColor = System.Drawing.Color.Black
        Me.CMDEXIT.Location = New System.Drawing.Point(552, 545)
        Me.CMDEXIT.Name = "CMDEXIT"
        Me.CMDEXIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEXIT.TabIndex = 316
        Me.CMDEXIT.Text = "E&xit"
        Me.CMDEXIT.UseVisualStyleBackColor = False
        '
        'LedgerDetailsReport
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1184, 581)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "LedgerDetailsReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Ledger Details"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents CMDPRINT As System.Windows.Forms.Button
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CMDEXIT As System.Windows.Forms.Button
    Friend WithEvents GCMPNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGROUP As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GOPBAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDRCR As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GAREA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCITY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSTATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCOUNTRY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRESINO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GALTNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPHONE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GMOBILENO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFAX As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GWEBSITE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GEMAIL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPANNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GADD As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTYPE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSECONDARY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CMDSAVE As System.Windows.Forms.Button
    Friend WithEvents GBOSSMOBILE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGSTIN As DevExpress.XtraGrid.Columns.GridColumn
End Class
