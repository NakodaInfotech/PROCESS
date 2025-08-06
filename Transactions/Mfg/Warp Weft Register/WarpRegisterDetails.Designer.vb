<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WarpRegisterDetails
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
        Me.BlendPanel1 = New VbPowerPack.BlendPanel
        Me.gridbilldetails = New DevExpress.XtraGrid.GridControl
        Me.gridbill = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.GISSUENO = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GDATE = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GMILLNAME = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GSIZERNAME = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GTOTALWT = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GLENGTH = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GENDS = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GTL = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GCUT = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GLONGATION = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GCOUNT = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GPROGRAMSRNO = New DevExpress.XtraGrid.Columns.GridColumn
        Me.PBPHOTO = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
        Me.CHKDONE = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.TOOLEXCEL = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.TOOLREFRESH = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.CMDEXIT = New System.Windows.Forms.Button
        Me.CMDEDIT = New System.Windows.Forms.Button
        Me.CMDADD = New System.Windows.Forms.Button
        Me.GROLLRECDNO = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GWARPINGNO = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GRETURNED = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GCALCCOUNT = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GCONSUMED = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GREMARKS = New DevExpress.XtraGrid.Columns.GridColumn
        Me.BlendPanel1.SuspendLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PBPHOTO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CHKDONE, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.gridbilldetails)
        Me.BlendPanel1.Controls.Add(Me.ToolStrip1)
        Me.BlendPanel1.Controls.Add(Me.CMDEXIT)
        Me.BlendPanel1.Controls.Add(Me.CMDEDIT)
        Me.BlendPanel1.Controls.Add(Me.CMDADD)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1184, 570)
        Me.BlendPanel1.TabIndex = 10
        '
        'gridbilldetails
        '
        Me.gridbilldetails.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbilldetails.Location = New System.Drawing.Point(27, 40)
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.PBPHOTO, Me.CHKDONE})
        Me.gridbilldetails.Size = New System.Drawing.Size(1145, 486)
        Me.gridbilldetails.TabIndex = 321
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill})
        '
        'gridbill
        '
        Me.gridbill.Appearance.HeaderPanel.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.gridbill.Appearance.HeaderPanel.Options.UseFont = True
        Me.gridbill.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GISSUENO, Me.GDATE, Me.GMILLNAME, Me.GSIZERNAME, Me.GTOTALWT, Me.GLENGTH, Me.GENDS, Me.GTL, Me.GCUT, Me.GLONGATION, Me.GCOUNT, Me.GPROGRAMSRNO, Me.GROLLRECDNO, Me.GWARPINGNO, Me.GRETURNED, Me.GCALCCOUNT, Me.GCONSUMED, Me.GREMARKS})
        Me.gridbill.GridControl = Me.gridbilldetails
        Me.gridbill.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.gridbill.Name = "gridbill"
        Me.gridbill.OptionsBehavior.AllowIncrementalSearch = True
        Me.gridbill.OptionsBehavior.AutoExpandAllGroups = True
        Me.gridbill.OptionsBehavior.Editable = False
        Me.gridbill.OptionsCustomization.AllowRowSizing = True
        Me.gridbill.OptionsView.ColumnAutoWidth = False
        Me.gridbill.OptionsView.ShowAutoFilterRow = True
        Me.gridbill.OptionsView.ShowFooter = True
        '
        'GISSUENO
        '
        Me.GISSUENO.Caption = "Sr. No."
        Me.GISSUENO.FieldName = "SRNO"
        Me.GISSUENO.Name = "GISSUENO"
        Me.GISSUENO.OptionsColumn.AllowEdit = False
        Me.GISSUENO.Visible = True
        Me.GISSUENO.VisibleIndex = 0
        Me.GISSUENO.Width = 60
        '
        'GDATE
        '
        Me.GDATE.Caption = "Date"
        Me.GDATE.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GDATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GDATE.FieldName = "DATE"
        Me.GDATE.Name = "GDATE"
        Me.GDATE.OptionsColumn.AllowEdit = False
        Me.GDATE.Visible = True
        Me.GDATE.VisibleIndex = 1
        '
        'GMILLNAME
        '
        Me.GMILLNAME.Caption = "Mill Name"
        Me.GMILLNAME.FieldName = "MILLNAME"
        Me.GMILLNAME.Name = "GMILLNAME"
        Me.GMILLNAME.OptionsColumn.AllowEdit = False
        Me.GMILLNAME.Visible = True
        Me.GMILLNAME.VisibleIndex = 2
        Me.GMILLNAME.Width = 220
        '
        'GSIZERNAME
        '
        Me.GSIZERNAME.Caption = "Sizer Name"
        Me.GSIZERNAME.FieldName = "SIZERNAME"
        Me.GSIZERNAME.Name = "GSIZERNAME"
        Me.GSIZERNAME.Visible = True
        Me.GSIZERNAME.VisibleIndex = 3
        Me.GSIZERNAME.Width = 200
        '
        'GTOTALWT
        '
        Me.GTOTALWT.Caption = "Total Wt."
        Me.GTOTALWT.DisplayFormat.FormatString = "0.000"
        Me.GTOTALWT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTOTALWT.FieldName = "TOTALWT"
        Me.GTOTALWT.Name = "GTOTALWT"
        Me.GTOTALWT.OptionsColumn.AllowEdit = False
        Me.GTOTALWT.Visible = True
        Me.GTOTALWT.VisibleIndex = 4
        '
        'GLENGTH
        '
        Me.GLENGTH.Caption = "Length"
        Me.GLENGTH.FieldName = "LENGTH"
        Me.GLENGTH.Name = "GLENGTH"
        Me.GLENGTH.Visible = True
        Me.GLENGTH.VisibleIndex = 5
        '
        'GENDS
        '
        Me.GENDS.Caption = "Ends"
        Me.GENDS.FieldName = "ENDS"
        Me.GENDS.Name = "GENDS"
        Me.GENDS.Visible = True
        Me.GENDS.VisibleIndex = 6
        '
        'GTL
        '
        Me.GTL.Caption = "Tapline"
        Me.GTL.FieldName = "TAPLINE"
        Me.GTL.Name = "GTL"
        Me.GTL.Visible = True
        Me.GTL.VisibleIndex = 7
        '
        'GCUT
        '
        Me.GCUT.Caption = "Cut"
        Me.GCUT.FieldName = "CUT"
        Me.GCUT.Name = "GCUT"
        Me.GCUT.Visible = True
        Me.GCUT.VisibleIndex = 8
        '
        'GLONGATION
        '
        Me.GLONGATION.Caption = "Longation"
        Me.GLONGATION.FieldName = "LONGATION"
        Me.GLONGATION.Name = "GLONGATION"
        Me.GLONGATION.Visible = True
        Me.GLONGATION.VisibleIndex = 9
        '
        'GCOUNT
        '
        Me.GCOUNT.Caption = "Count"
        Me.GCOUNT.FieldName = "COUNT"
        Me.GCOUNT.Name = "GCOUNT"
        Me.GCOUNT.Visible = True
        Me.GCOUNT.VisibleIndex = 10
        '
        'GPROGRAMSRNO
        '
        Me.GPROGRAMSRNO.Caption = "Program No"
        Me.GPROGRAMSRNO.FieldName = "PROGRAMSRNO"
        Me.GPROGRAMSRNO.Name = "GPROGRAMSRNO"
        Me.GPROGRAMSRNO.Visible = True
        Me.GPROGRAMSRNO.VisibleIndex = 11
        '
        'PBPHOTO
        '
        Me.PBPHOTO.Name = "PBPHOTO"
        Me.PBPHOTO.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        '
        'CHKDONE
        '
        Me.CHKDONE.AutoHeight = False
        Me.CHKDONE.Name = "CHKDONE"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TOOLEXCEL, Me.ToolStripSeparator2, Me.TOOLREFRESH, Me.ToolStripSeparator1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1184, 25)
        Me.ToolStrip1.TabIndex = 3
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'TOOLEXCEL
        '
        Me.TOOLEXCEL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TOOLEXCEL.Image = Global.PROCESS.My.Resources.Resources.Excel_icon
        Me.TOOLEXCEL.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TOOLEXCEL.Name = "TOOLEXCEL"
        Me.TOOLEXCEL.Size = New System.Drawing.Size(23, 22)
        Me.TOOLEXCEL.Text = "Print"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'TOOLREFRESH
        '
        Me.TOOLREFRESH.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TOOLREFRESH.Image = Global.PROCESS.My.Resources.Resources.refresh1
        Me.TOOLREFRESH.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TOOLREFRESH.Name = "TOOLREFRESH"
        Me.TOOLREFRESH.Size = New System.Drawing.Size(23, 22)
        Me.TOOLREFRESH.Text = "ToolStripButton1"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'CMDEXIT
        '
        Me.CMDEXIT.Location = New System.Drawing.Point(637, 532)
        Me.CMDEXIT.Name = "CMDEXIT"
        Me.CMDEXIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEXIT.TabIndex = 2
        Me.CMDEXIT.Text = "E&xit"
        Me.CMDEXIT.UseVisualStyleBackColor = True
        '
        'CMDEDIT
        '
        Me.CMDEDIT.Location = New System.Drawing.Point(552, 532)
        Me.CMDEDIT.Name = "CMDEDIT"
        Me.CMDEDIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEDIT.TabIndex = 1
        Me.CMDEDIT.Text = "&Edit"
        Me.CMDEDIT.UseVisualStyleBackColor = True
        '
        'CMDADD
        '
        Me.CMDADD.Location = New System.Drawing.Point(467, 532)
        Me.CMDADD.Name = "CMDADD"
        Me.CMDADD.Size = New System.Drawing.Size(80, 28)
        Me.CMDADD.TabIndex = 0
        Me.CMDADD.Text = "&Add New"
        Me.CMDADD.UseVisualStyleBackColor = True
        '
        'GROLLRECDNO
        '
        Me.GROLLRECDNO.Caption = "Roll Recd No"
        Me.GROLLRECDNO.FieldName = "ROLLRECDNO"
        Me.GROLLRECDNO.Name = "GROLLRECDNO"
        Me.GROLLRECDNO.Visible = True
        Me.GROLLRECDNO.VisibleIndex = 12
        '
        'GWARPINGNO
        '
        Me.GWARPINGNO.Caption = "Warping No"
        Me.GWARPINGNO.FieldName = "WARPINGNO"
        Me.GWARPINGNO.Name = "GWARPINGNO"
        Me.GWARPINGNO.Visible = True
        Me.GWARPINGNO.VisibleIndex = 13
        '
        'GRETURNED
        '
        Me.GRETURNED.Caption = "Returned"
        Me.GRETURNED.FieldName = "RETURNED"
        Me.GRETURNED.Name = "GRETURNED"
        Me.GRETURNED.Visible = True
        Me.GRETURNED.VisibleIndex = 14
        '
        'GCALCCOUNT
        '
        Me.GCALCCOUNT.Caption = "Calc Count"
        Me.GCALCCOUNT.FieldName = "CALCCOUNT"
        Me.GCALCCOUNT.Name = "GCALCCOUNT"
        Me.GCALCCOUNT.Visible = True
        Me.GCALCCOUNT.VisibleIndex = 15
        '
        'GCONSUMED
        '
        Me.GCONSUMED.Caption = "Consumed"
        Me.GCONSUMED.FieldName = "CONSUMED"
        Me.GCONSUMED.Name = "GCONSUMED"
        Me.GCONSUMED.Visible = True
        Me.GCONSUMED.VisibleIndex = 16
        '
        'GREMARKS
        '
        Me.GREMARKS.Caption = "Remarks"
        Me.GREMARKS.FieldName = "REMARKS"
        Me.GREMARKS.Name = "GREMARKS"
        Me.GREMARKS.Visible = True
        Me.GREMARKS.VisibleIndex = 17
        '
        'WarpRegisterDetails
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1184, 570)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "WarpRegisterDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Warp Register Details"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PBPHOTO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CHKDONE, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GISSUENO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GMILLNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTOTALWT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PBPHOTO As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents CHKDONE As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents TOOLEXCEL As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TOOLREFRESH As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CMDEXIT As System.Windows.Forms.Button
    Friend WithEvents CMDEDIT As System.Windows.Forms.Button
    Friend WithEvents CMDADD As System.Windows.Forms.Button
    Friend WithEvents GLENGTH As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GENDS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCUT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GLONGATION As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCOUNT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSIZERNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPROGRAMSRNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GROLLRECDNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GWARPINGNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRETURNED As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCALCCOUNT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCONSUMED As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GREMARKS As DevExpress.XtraGrid.Columns.GridColumn
End Class
