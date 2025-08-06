<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RollsRecdRegisterDetails
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
        Me.CMDREFRESH = New System.Windows.Forms.Button
        Me.cmdcancel = New System.Windows.Forms.Button
        Me.gridbilldetails = New DevExpress.XtraGrid.GridControl
        Me.gridbill = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.GSRNO = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GCHALLANNO = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GDATE = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GMILLNAME = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GFRESH = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GFRESHWT = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GWINDING = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GWINDINGWT = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GFIRKA = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GFIRKAWT = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GROLLWT = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GLENGTH = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GTOTALENDS = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GTL = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GCUT = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GCUTWEIGHT = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GRETURNWT = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GTOTALWEIGHT = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GLONGATION = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GCOUNT = New DevExpress.XtraGrid.Columns.GridColumn
        Me.CHKEDIT = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
        Me.APPROXDATE = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.ExcelExport = New System.Windows.Forms.ToolStripButton
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator
        Me.BlendPanel1.SuspendLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CHKEDIT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.APPROXDATE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.APPROXDATE.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.CMDREFRESH)
        Me.BlendPanel1.Controls.Add(Me.cmdcancel)
        Me.BlendPanel1.Controls.Add(Me.gridbilldetails)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1135, 562)
        Me.BlendPanel1.TabIndex = 11
        '
        'CMDREFRESH
        '
        Me.CMDREFRESH.BackColor = System.Drawing.Color.Transparent
        Me.CMDREFRESH.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDREFRESH.FlatAppearance.BorderSize = 0
        Me.CMDREFRESH.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDREFRESH.ForeColor = System.Drawing.Color.Black
        Me.CMDREFRESH.Location = New System.Drawing.Point(485, 522)
        Me.CMDREFRESH.Name = "CMDREFRESH"
        Me.CMDREFRESH.Size = New System.Drawing.Size(80, 28)
        Me.CMDREFRESH.TabIndex = 794
        Me.CMDREFRESH.Text = "&Refresh"
        Me.CMDREFRESH.UseVisualStyleBackColor = False
        '
        'cmdcancel
        '
        Me.cmdcancel.BackColor = System.Drawing.Color.Transparent
        Me.cmdcancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdcancel.FlatAppearance.BorderSize = 0
        Me.cmdcancel.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdcancel.ForeColor = System.Drawing.Color.Black
        Me.cmdcancel.Location = New System.Drawing.Point(570, 522)
        Me.cmdcancel.Name = "cmdcancel"
        Me.cmdcancel.Size = New System.Drawing.Size(80, 28)
        Me.cmdcancel.TabIndex = 795
        Me.cmdcancel.Text = "E&xit"
        Me.cmdcancel.UseVisualStyleBackColor = False
        '
        'gridbilldetails
        '
        Me.gridbilldetails.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbilldetails.Location = New System.Drawing.Point(11, 35)
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.CHKEDIT, Me.APPROXDATE})
        Me.gridbilldetails.Size = New System.Drawing.Size(1112, 481)
        Me.gridbilldetails.TabIndex = 655
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill})
        '
        'gridbill
        '
        Me.gridbill.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GSRNO, Me.GCHALLANNO, Me.GDATE, Me.GMILLNAME, Me.GFRESH, Me.GFRESHWT, Me.GWINDING, Me.GWINDINGWT, Me.GFIRKA, Me.GFIRKAWT, Me.GROLLWT, Me.GLENGTH, Me.GTOTALENDS, Me.GTL, Me.GCUT, Me.GCUTWEIGHT, Me.GRETURNWT, Me.GTOTALWEIGHT, Me.GLONGATION, Me.GCOUNT})
        Me.gridbill.GridControl = Me.gridbilldetails
        Me.gridbill.Name = "gridbill"
        Me.gridbill.OptionsBehavior.AllowIncrementalSearch = True
        Me.gridbill.OptionsBehavior.Editable = False
        Me.gridbill.OptionsCustomization.AllowColumnMoving = False
        Me.gridbill.OptionsCustomization.AllowGroup = False
        Me.gridbill.OptionsCustomization.AllowQuickHideColumns = False
        Me.gridbill.OptionsView.ColumnAutoWidth = False
        Me.gridbill.OptionsView.ShowAutoFilterRow = True
        Me.gridbill.OptionsView.ShowFooter = True
        Me.gridbill.OptionsView.ShowGroupPanel = False
        '
        'GSRNO
        '
        Me.GSRNO.Caption = "Sr No"
        Me.GSRNO.FieldName = "SRNO"
        Me.GSRNO.Name = "GSRNO"
        Me.GSRNO.Visible = True
        Me.GSRNO.VisibleIndex = 0
        Me.GSRNO.Width = 50
        '
        'GCHALLANNO
        '
        Me.GCHALLANNO.Caption = "Challan No"
        Me.GCHALLANNO.FieldName = "CHALLANNO"
        Me.GCHALLANNO.Name = "GCHALLANNO"
        Me.GCHALLANNO.Visible = True
        Me.GCHALLANNO.VisibleIndex = 1
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
        Me.GDATE.VisibleIndex = 2
        '
        'GMILLNAME
        '
        Me.GMILLNAME.Caption = "Mill Name"
        Me.GMILLNAME.FieldName = "MILLNAME"
        Me.GMILLNAME.Name = "GMILLNAME"
        Me.GMILLNAME.OptionsColumn.AllowEdit = False
        Me.GMILLNAME.Visible = True
        Me.GMILLNAME.VisibleIndex = 3
        Me.GMILLNAME.Width = 190
        '
        'GFRESH
        '
        Me.GFRESH.Caption = "Fresh"
        Me.GFRESH.DisplayFormat.FormatString = "0"
        Me.GFRESH.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GFRESH.FieldName = "FRESH"
        Me.GFRESH.Name = "GFRESH"
        Me.GFRESH.Visible = True
        Me.GFRESH.VisibleIndex = 4
        '
        'GFRESHWT
        '
        Me.GFRESHWT.Caption = "Fresh Wt"
        Me.GFRESHWT.DisplayFormat.FormatString = "0.00"
        Me.GFRESHWT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GFRESHWT.FieldName = "FRESHWT"
        Me.GFRESHWT.Name = "GFRESHWT"
        Me.GFRESHWT.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GFRESHWT.Visible = True
        Me.GFRESHWT.VisibleIndex = 5
        '
        'GWINDING
        '
        Me.GWINDING.Caption = "Winding"
        Me.GWINDING.DisplayFormat.FormatString = "0"
        Me.GWINDING.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GWINDING.FieldName = "WINDING"
        Me.GWINDING.Name = "GWINDING"
        Me.GWINDING.Visible = True
        Me.GWINDING.VisibleIndex = 6
        '
        'GWINDINGWT
        '
        Me.GWINDINGWT.Caption = "Winding Wt"
        Me.GWINDINGWT.DisplayFormat.FormatString = "0.00"
        Me.GWINDINGWT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GWINDINGWT.FieldName = "WINDINGWT"
        Me.GWINDINGWT.Name = "GWINDINGWT"
        Me.GWINDINGWT.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GWINDINGWT.Visible = True
        Me.GWINDINGWT.VisibleIndex = 7
        '
        'GFIRKA
        '
        Me.GFIRKA.Caption = "Firka"
        Me.GFIRKA.FieldName = "FIRKA"
        Me.GFIRKA.Name = "GFIRKA"
        Me.GFIRKA.Visible = True
        Me.GFIRKA.VisibleIndex = 8
        '
        'GFIRKAWT
        '
        Me.GFIRKAWT.Caption = "Firka Wt"
        Me.GFIRKAWT.DisplayFormat.FormatString = "0.00"
        Me.GFIRKAWT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GFIRKAWT.FieldName = "FIRKAWT"
        Me.GFIRKAWT.Name = "GFIRKAWT"
        Me.GFIRKAWT.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GFIRKAWT.Visible = True
        Me.GFIRKAWT.VisibleIndex = 9
        '
        'GROLLWT
        '
        Me.GROLLWT.Caption = "Roll Weight"
        Me.GROLLWT.DisplayFormat.FormatString = "0.00"
        Me.GROLLWT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GROLLWT.FieldName = "ROLLWT"
        Me.GROLLWT.Name = "GROLLWT"
        Me.GROLLWT.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GROLLWT.Visible = True
        Me.GROLLWT.VisibleIndex = 10
        Me.GROLLWT.Width = 85
        '
        'GLENGTH
        '
        Me.GLENGTH.Caption = "Length"
        Me.GLENGTH.DisplayFormat.FormatString = "0"
        Me.GLENGTH.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GLENGTH.FieldName = "LENGTH"
        Me.GLENGTH.Name = "GLENGTH"
        Me.GLENGTH.Visible = True
        Me.GLENGTH.VisibleIndex = 11
        '
        'GTOTALENDS
        '
        Me.GTOTALENDS.Caption = "Total Ends"
        Me.GTOTALENDS.DisplayFormat.FormatString = "0"
        Me.GTOTALENDS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTOTALENDS.FieldName = "TOTALENDS"
        Me.GTOTALENDS.Name = "GTOTALENDS"
        Me.GTOTALENDS.Visible = True
        Me.GTOTALENDS.VisibleIndex = 12
        '
        'GTL
        '
        Me.GTL.Caption = "TL"
        Me.GTL.DisplayFormat.FormatString = "0"
        Me.GTL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTL.FieldName = "TL"
        Me.GTL.Name = "GTL"
        Me.GTL.Visible = True
        Me.GTL.VisibleIndex = 13
        '
        'GCUT
        '
        Me.GCUT.Caption = "Cut"
        Me.GCUT.DisplayFormat.FormatString = "0.00"
        Me.GCUT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GCUT.FieldName = "CUT"
        Me.GCUT.Name = "GCUT"
        Me.GCUT.Visible = True
        Me.GCUT.VisibleIndex = 14
        '
        'GCUTWEIGHT
        '
        Me.GCUTWEIGHT.Caption = "Cut Weight"
        Me.GCUTWEIGHT.DisplayFormat.FormatString = "0.00"
        Me.GCUTWEIGHT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GCUTWEIGHT.FieldName = "CUTWEIGHT"
        Me.GCUTWEIGHT.Name = "GCUTWEIGHT"
        Me.GCUTWEIGHT.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GCUTWEIGHT.Visible = True
        Me.GCUTWEIGHT.VisibleIndex = 15
        '
        'GRETURNWT
        '
        Me.GRETURNWT.Caption = "Return Wt"
        Me.GRETURNWT.DisplayFormat.FormatString = "0.00"
        Me.GRETURNWT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GRETURNWT.FieldName = "RETURNWT"
        Me.GRETURNWT.Name = "GRETURNWT"
        Me.GRETURNWT.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GRETURNWT.Visible = True
        Me.GRETURNWT.VisibleIndex = 16
        '
        'GTOTALWEIGHT
        '
        Me.GTOTALWEIGHT.Caption = "Total Weight"
        Me.GTOTALWEIGHT.DisplayFormat.FormatString = "0.00"
        Me.GTOTALWEIGHT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTOTALWEIGHT.FieldName = "TOTALWEIGHT"
        Me.GTOTALWEIGHT.Name = "GTOTALWEIGHT"
        Me.GTOTALWEIGHT.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GTOTALWEIGHT.Visible = True
        Me.GTOTALWEIGHT.VisibleIndex = 17
        Me.GTOTALWEIGHT.Width = 85
        '
        'GLONGATION
        '
        Me.GLONGATION.Caption = "Longation"
        Me.GLONGATION.DisplayFormat.FormatString = "0.00"
        Me.GLONGATION.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GLONGATION.FieldName = "LONGATION"
        Me.GLONGATION.Name = "GLONGATION"
        Me.GLONGATION.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GLONGATION.Visible = True
        Me.GLONGATION.VisibleIndex = 18
        '
        'GCOUNT
        '
        Me.GCOUNT.Caption = "Count"
        Me.GCOUNT.DisplayFormat.FormatString = "0.00"
        Me.GCOUNT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GCOUNT.FieldName = "COUNT"
        Me.GCOUNT.Name = "GCOUNT"
        Me.GCOUNT.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GCOUNT.Visible = True
        Me.GCOUNT.VisibleIndex = 19
        '
        'CHKEDIT
        '
        Me.CHKEDIT.AutoHeight = False
        Me.CHKEDIT.Name = "CHKEDIT"
        Me.CHKEDIT.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'APPROXDATE
        '
        Me.APPROXDATE.AutoHeight = False
        Me.APPROXDATE.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.APPROXDATE.Name = "APPROXDATE"
        Me.APPROXDATE.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExcelExport, Me.toolStripSeparator})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1135, 25)
        Me.ToolStrip1.TabIndex = 12
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ExcelExport
        '
        Me.ExcelExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ExcelExport.Image = Global.PROCESS.My.Resources.Resources.Excel_icon
        Me.ExcelExport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ExcelExport.Name = "ExcelExport"
        Me.ExcelExport.Size = New System.Drawing.Size(23, 22)
        Me.ExcelExport.Text = "&Export to Excel"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'RollsRecdRegisterDetails
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1135, 562)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "RollsRecdRegisterDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Rolls Received Register Details"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CHKEDIT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.APPROXDATE.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.APPROXDATE, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents CMDREFRESH As System.Windows.Forms.Button
    Friend WithEvents cmdcancel As System.Windows.Forms.Button
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GMILLNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CHKEDIT As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents APPROXDATE As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ExcelExport As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents GSRNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCHALLANNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFRESH As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFRESHWT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GWINDING As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GWINDINGWT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFIRKA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFIRKAWT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GROLLWT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GLENGTH As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTOTALENDS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCUT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCUTWEIGHT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCOUNT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRETURNWT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTOTALWEIGHT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GLONGATION As DevExpress.XtraGrid.Columns.GridColumn
End Class
