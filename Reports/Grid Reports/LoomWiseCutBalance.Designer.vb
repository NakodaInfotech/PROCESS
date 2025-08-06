<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LoomWiseCutBalance
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
        Me.CMDREFRESH = New System.Windows.Forms.Button()
        Me.cmdcancel = New System.Windows.Forms.Button()
        Me.gridbilldetails = New DevExpress.XtraGrid.GridControl()
        Me.gridbill = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GISSUENO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBEAMNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBEAMNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCUTS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBALANCE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GWT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSIZERNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GLOOMNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGRIDSRNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTYPE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CHKDONE = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.CMDEXCEL = New System.Windows.Forms.Button()
        Me.BlendPanel1.SuspendLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CHKDONE, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.CMDEXCEL)
        Me.BlendPanel1.Controls.Add(Me.CMDREFRESH)
        Me.BlendPanel1.Controls.Add(Me.cmdcancel)
        Me.BlendPanel1.Controls.Add(Me.gridbilldetails)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1184, 592)
        Me.BlendPanel1.TabIndex = 13
        '
        'CMDREFRESH
        '
        Me.CMDREFRESH.BackColor = System.Drawing.Color.Transparent
        Me.CMDREFRESH.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDREFRESH.FlatAppearance.BorderSize = 0
        Me.CMDREFRESH.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDREFRESH.ForeColor = System.Drawing.Color.Black
        Me.CMDREFRESH.Location = New System.Drawing.Point(553, 551)
        Me.CMDREFRESH.Name = "CMDREFRESH"
        Me.CMDREFRESH.Size = New System.Drawing.Size(80, 28)
        Me.CMDREFRESH.TabIndex = 802
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
        Me.cmdcancel.Location = New System.Drawing.Point(638, 551)
        Me.cmdcancel.Name = "cmdcancel"
        Me.cmdcancel.Size = New System.Drawing.Size(80, 28)
        Me.cmdcancel.TabIndex = 803
        Me.cmdcancel.Text = "E&xit"
        Me.cmdcancel.UseVisualStyleBackColor = False
        '
        'gridbilldetails
        '
        Me.gridbilldetails.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbilldetails.Location = New System.Drawing.Point(23, 24)
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.CHKDONE})
        Me.gridbilldetails.Size = New System.Drawing.Size(1134, 511)
        Me.gridbilldetails.TabIndex = 257
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill})
        '
        'gridbill
        '
        Me.gridbill.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!)
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GISSUENO, Me.GNAME, Me.GBEAMNAME, Me.GBEAMNO, Me.GCUTS, Me.GBALANCE, Me.GWT, Me.GSIZERNAME, Me.GLOOMNO, Me.GGRIDSRNO, Me.GTYPE})
        Me.gridbill.CustomizationFormBounds = New System.Drawing.Rectangle(688, 311, 208, 184)
        Me.gridbill.GridControl = Me.gridbilldetails
        Me.gridbill.Name = "gridbill"
        Me.gridbill.OptionsBehavior.AutoExpandAllGroups = True
        Me.gridbill.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click
        Me.gridbill.OptionsView.ColumnAutoWidth = False
        Me.gridbill.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.gridbill.OptionsView.ShowAutoFilterRow = True
        Me.gridbill.OptionsView.ShowFooter = True
        Me.gridbill.OptionsView.ShowGroupPanel = False
        '
        'GISSUENO
        '
        Me.GISSUENO.Caption = "Entry No"
        Me.GISSUENO.FieldName = "FROMNO"
        Me.GISSUENO.Name = "GISSUENO"
        Me.GISSUENO.OptionsColumn.AllowEdit = False
        Me.GISSUENO.OptionsColumn.ReadOnly = True
        Me.GISSUENO.Visible = True
        Me.GISSUENO.VisibleIndex = 0
        '
        'GNAME
        '
        Me.GNAME.Caption = "Weaver Name"
        Me.GNAME.FieldName = "WEAVERNAME"
        Me.GNAME.Name = "GNAME"
        Me.GNAME.OptionsColumn.AllowEdit = False
        Me.GNAME.Visible = True
        Me.GNAME.VisibleIndex = 1
        Me.GNAME.Width = 200
        '
        'GBEAMNAME
        '
        Me.GBEAMNAME.Caption = "Beam Name"
        Me.GBEAMNAME.FieldName = "BEAMNAME"
        Me.GBEAMNAME.Name = "GBEAMNAME"
        Me.GBEAMNAME.OptionsColumn.AllowEdit = False
        Me.GBEAMNAME.Visible = True
        Me.GBEAMNAME.VisibleIndex = 2
        Me.GBEAMNAME.Width = 150
        '
        'GBEAMNO
        '
        Me.GBEAMNO.Caption = "Beam No"
        Me.GBEAMNO.FieldName = "BEAMNO"
        Me.GBEAMNO.Name = "GBEAMNO"
        Me.GBEAMNO.OptionsColumn.AllowEdit = False
        Me.GBEAMNO.OptionsColumn.ReadOnly = True
        Me.GBEAMNO.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)})
        Me.GBEAMNO.Visible = True
        Me.GBEAMNO.VisibleIndex = 3
        Me.GBEAMNO.Width = 80
        '
        'GCUTS
        '
        Me.GCUTS.Caption = "Cuts"
        Me.GCUTS.DisplayFormat.FormatString = "0.00"
        Me.GCUTS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GCUTS.FieldName = "CUT"
        Me.GCUTS.Name = "GCUTS"
        Me.GCUTS.OptionsColumn.AllowEdit = False
        Me.GCUTS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GCUTS.Visible = True
        Me.GCUTS.VisibleIndex = 4
        '
        'GBALANCE
        '
        Me.GBALANCE.Caption = "Balance Cut"
        Me.GBALANCE.DisplayFormat.FormatString = "0.00"
        Me.GBALANCE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GBALANCE.FieldName = "BALANCECUT"
        Me.GBALANCE.Name = "GBALANCE"
        Me.GBALANCE.OptionsColumn.AllowEdit = False
        Me.GBALANCE.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GBALANCE.Visible = True
        Me.GBALANCE.VisibleIndex = 5
        '
        'GWT
        '
        Me.GWT.Caption = "Wt"
        Me.GWT.DisplayFormat.FormatString = "0.00"
        Me.GWT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GWT.FieldName = "WT"
        Me.GWT.Name = "GWT"
        Me.GWT.OptionsColumn.AllowEdit = False
        Me.GWT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GWT.Visible = True
        Me.GWT.VisibleIndex = 6
        '
        'GSIZERNAME
        '
        Me.GSIZERNAME.Caption = "Sizer Name"
        Me.GSIZERNAME.FieldName = "SIZERNAME"
        Me.GSIZERNAME.Name = "GSIZERNAME"
        Me.GSIZERNAME.OptionsColumn.AllowEdit = False
        Me.GSIZERNAME.Visible = True
        Me.GSIZERNAME.VisibleIndex = 7
        Me.GSIZERNAME.Width = 200
        '
        'GLOOMNO
        '
        Me.GLOOMNO.Caption = "Loom No"
        Me.GLOOMNO.FieldName = "LOOMNO"
        Me.GLOOMNO.Name = "GLOOMNO"
        Me.GLOOMNO.Visible = True
        Me.GLOOMNO.VisibleIndex = 8
        '
        'GGRIDSRNO
        '
        Me.GGRIDSRNO.Caption = "Grid Sr No"
        Me.GGRIDSRNO.FieldName = "FROMSRNO"
        Me.GGRIDSRNO.Name = "GGRIDSRNO"
        Me.GGRIDSRNO.OptionsColumn.AllowEdit = False
        '
        'GTYPE
        '
        Me.GTYPE.Caption = "Type"
        Me.GTYPE.FieldName = "TYPE"
        Me.GTYPE.Name = "GTYPE"
        Me.GTYPE.OptionsColumn.AllowEdit = False
        '
        'CHKDONE
        '
        Me.CHKDONE.AutoHeight = False
        Me.CHKDONE.Name = "CHKDONE"
        Me.CHKDONE.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'CMDEXCEL
        '
        Me.CMDEXCEL.BackColor = System.Drawing.Color.Transparent
        Me.CMDEXCEL.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDEXCEL.FlatAppearance.BorderSize = 0
        Me.CMDEXCEL.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDEXCEL.ForeColor = System.Drawing.Color.Black
        Me.CMDEXCEL.Location = New System.Drawing.Point(467, 551)
        Me.CMDEXCEL.Name = "CMDEXCEL"
        Me.CMDEXCEL.Size = New System.Drawing.Size(80, 28)
        Me.CMDEXCEL.TabIndex = 804
        Me.CMDEXCEL.Text = "&Export"
        Me.CMDEXCEL.UseVisualStyleBackColor = False
        '
        'LoomWiseCutBalance
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1184, 592)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "LoomWiseCutBalance"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Loom Wise Cut Balance"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CHKDONE, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents CMDREFRESH As System.Windows.Forms.Button
    Friend WithEvents cmdcancel As System.Windows.Forms.Button
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GISSUENO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBEAMNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBEAMNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCUTS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBALANCE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GWT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSIZERNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CHKDONE As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GGRIDSRNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTYPE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GLOOMNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CMDEXCEL As Button
End Class
