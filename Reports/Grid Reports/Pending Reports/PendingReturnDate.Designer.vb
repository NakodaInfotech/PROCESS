<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PendingReturnDate
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
        Me.gridbilldetails = New DevExpress.XtraGrid.GridControl()
        Me.gridbill = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GGRNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTYPE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GINVOICENO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGODOWN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDYEINGNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GLOTNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GQUALITY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCTAKA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCMTRS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRTAKA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRMTRS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRDONO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSHORTAGE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GNTAKA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GNMTRS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTRANSPORT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CMBTRANSPORT = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.CHKEDIT = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.APPROXDATE = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.RBENTERED = New System.Windows.Forms.RadioButton()
        Me.RBPENDING = New System.Windows.Forms.RadioButton()
        Me.CMDDELETE = New System.Windows.Forms.Button()
        Me.CMDOK = New System.Windows.Forms.Button()
        Me.CMDREFRESH = New System.Windows.Forms.Button()
        Me.cmdcancel = New System.Windows.Forms.Button()
        Me.BlendPanel1.SuspendLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CMBTRANSPORT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CHKEDIT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.APPROXDATE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.APPROXDATE.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.gridbilldetails)
        Me.BlendPanel1.Controls.Add(Me.RBENTERED)
        Me.BlendPanel1.Controls.Add(Me.RBPENDING)
        Me.BlendPanel1.Controls.Add(Me.CMDDELETE)
        Me.BlendPanel1.Controls.Add(Me.CMDOK)
        Me.BlendPanel1.Controls.Add(Me.CMDREFRESH)
        Me.BlendPanel1.Controls.Add(Me.cmdcancel)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1234, 570)
        Me.BlendPanel1.TabIndex = 10
        '
        'gridbilldetails
        '
        Me.gridbilldetails.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbilldetails.Location = New System.Drawing.Point(15, 35)
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.CHKEDIT, Me.APPROXDATE, Me.CMBTRANSPORT})
        Me.gridbilldetails.Size = New System.Drawing.Size(1207, 481)
        Me.gridbilldetails.TabIndex = 655
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill})
        '
        'gridbill
        '
        Me.gridbill.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GGRNO, Me.GTYPE, Me.GINVOICENO, Me.GDATE, Me.GGODOWN, Me.GNAME, Me.GDYEINGNAME, Me.GLOTNO, Me.GQUALITY, Me.GCTAKA, Me.GCMTRS, Me.GRTAKA, Me.GRMTRS, Me.GRDONO, Me.GSHORTAGE, Me.GNTAKA, Me.GNMTRS, Me.GRDATE, Me.GTRANSPORT})
        Me.gridbill.GridControl = Me.gridbilldetails
        Me.gridbill.Name = "gridbill"
        Me.gridbill.OptionsBehavior.AllowIncrementalSearch = True
        Me.gridbill.OptionsCustomization.AllowColumnMoving = False
        Me.gridbill.OptionsCustomization.AllowGroup = False
        Me.gridbill.OptionsCustomization.AllowQuickHideColumns = False
        Me.gridbill.OptionsView.ColumnAutoWidth = False
        Me.gridbill.OptionsView.ShowAutoFilterRow = True
        Me.gridbill.OptionsView.ShowGroupPanel = False
        '
        'GGRNO
        '
        Me.GGRNO.Caption = "Sr No."
        Me.GGRNO.DisplayFormat.FormatString = "0"
        Me.GGRNO.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GGRNO.FieldName = "GRNO"
        Me.GGRNO.Name = "GGRNO"
        Me.GGRNO.OptionsColumn.AllowEdit = False
        Me.GGRNO.Visible = True
        Me.GGRNO.VisibleIndex = 0
        Me.GGRNO.Width = 60
        '
        'GTYPE
        '
        Me.GTYPE.Caption = "Type"
        Me.GTYPE.FieldName = "TYPE"
        Me.GTYPE.Name = "GTYPE"
        Me.GTYPE.OptionsColumn.AllowEdit = False
        '
        'GINVOICENO
        '
        Me.GINVOICENO.Caption = "Invoice No"
        Me.GINVOICENO.FieldName = "INVOICENO"
        Me.GINVOICENO.Name = "GINVOICENO"
        Me.GINVOICENO.OptionsColumn.AllowEdit = False
        Me.GINVOICENO.Visible = True
        Me.GINVOICENO.VisibleIndex = 1
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
        Me.GDATE.Width = 80
        '
        'GGODOWN
        '
        Me.GGODOWN.Caption = "Godown"
        Me.GGODOWN.FieldName = "GODOWN"
        Me.GGODOWN.Name = "GGODOWN"
        Me.GGODOWN.OptionsColumn.AllowEdit = False
        Me.GGODOWN.Width = 150
        '
        'GNAME
        '
        Me.GNAME.Caption = "Name"
        Me.GNAME.FieldName = "NAME"
        Me.GNAME.Name = "GNAME"
        Me.GNAME.OptionsColumn.AllowEdit = False
        Me.GNAME.Visible = True
        Me.GNAME.VisibleIndex = 3
        Me.GNAME.Width = 180
        '
        'GDYEINGNAME
        '
        Me.GDYEINGNAME.Caption = "Dyeing Name"
        Me.GDYEINGNAME.FieldName = "DYEINGNAME"
        Me.GDYEINGNAME.Name = "GDYEINGNAME"
        Me.GDYEINGNAME.OptionsColumn.AllowEdit = False
        Me.GDYEINGNAME.Visible = True
        Me.GDYEINGNAME.VisibleIndex = 4
        Me.GDYEINGNAME.Width = 180
        '
        'GLOTNO
        '
        Me.GLOTNO.Caption = "Lot No."
        Me.GLOTNO.DisplayFormat.FormatString = "0"
        Me.GLOTNO.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GLOTNO.FieldName = "LOTNO"
        Me.GLOTNO.Name = "GLOTNO"
        Me.GLOTNO.OptionsColumn.AllowEdit = False
        Me.GLOTNO.Visible = True
        Me.GLOTNO.VisibleIndex = 5
        Me.GLOTNO.Width = 80
        '
        'GQUALITY
        '
        Me.GQUALITY.Caption = "Grey Quality"
        Me.GQUALITY.FieldName = "QUALITY"
        Me.GQUALITY.Name = "GQUALITY"
        Me.GQUALITY.OptionsColumn.AllowEdit = False
        Me.GQUALITY.Visible = True
        Me.GQUALITY.VisibleIndex = 6
        Me.GQUALITY.Width = 180
        '
        'GCTAKA
        '
        Me.GCTAKA.Caption = "Challan Taka"
        Me.GCTAKA.DisplayFormat.FormatString = "0"
        Me.GCTAKA.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GCTAKA.FieldName = "CTAKA"
        Me.GCTAKA.Name = "GCTAKA"
        Me.GCTAKA.OptionsColumn.AllowEdit = False
        '
        'GCMTRS
        '
        Me.GCMTRS.Caption = "Challan Mtrs."
        Me.GCMTRS.DisplayFormat.FormatString = "0.00"
        Me.GCMTRS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GCMTRS.FieldName = "CMTRS"
        Me.GCMTRS.Name = "GCMTRS"
        Me.GCMTRS.OptionsColumn.AllowEdit = False
        '
        'GRTAKA
        '
        Me.GRTAKA.Caption = "Return Taka"
        Me.GRTAKA.DisplayFormat.FormatString = "0"
        Me.GRTAKA.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GRTAKA.FieldName = "RTAKA"
        Me.GRTAKA.Name = "GRTAKA"
        Me.GRTAKA.OptionsColumn.AllowEdit = False
        Me.GRTAKA.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "BAGS", "")})
        Me.GRTAKA.Visible = True
        Me.GRTAKA.VisibleIndex = 7
        Me.GRTAKA.Width = 80
        '
        'GRMTRS
        '
        Me.GRMTRS.Caption = "Return Mtrs."
        Me.GRMTRS.DisplayFormat.FormatString = "0.00"
        Me.GRMTRS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GRMTRS.FieldName = "RMTRS"
        Me.GRMTRS.Name = "GRMTRS"
        Me.GRMTRS.OptionsColumn.AllowEdit = False
        Me.GRMTRS.Visible = True
        Me.GRMTRS.VisibleIndex = 8
        Me.GRMTRS.Width = 80
        '
        'GRDONO
        '
        Me.GRDONO.Caption = "Return DO. No."
        Me.GRDONO.FieldName = "RDONO"
        Me.GRDONO.Name = "GRDONO"
        Me.GRDONO.OptionsColumn.AllowEdit = False
        Me.GRDONO.Visible = True
        Me.GRDONO.VisibleIndex = 9
        Me.GRDONO.Width = 80
        '
        'GSHORTAGE
        '
        Me.GSHORTAGE.Caption = "Shortage"
        Me.GSHORTAGE.DisplayFormat.FormatString = "0.00"
        Me.GSHORTAGE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GSHORTAGE.FieldName = "SHORTAGE"
        Me.GSHORTAGE.Name = "GSHORTAGE"
        Me.GSHORTAGE.OptionsColumn.AllowEdit = False
        '
        'GNTAKA
        '
        Me.GNTAKA.Caption = "Nett Taka"
        Me.GNTAKA.DisplayFormat.FormatString = "0"
        Me.GNTAKA.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GNTAKA.FieldName = "NTAKA"
        Me.GNTAKA.Name = "GNTAKA"
        Me.GNTAKA.OptionsColumn.AllowEdit = False
        '
        'GNMTRS
        '
        Me.GNMTRS.Caption = "Nett Mtrs."
        Me.GNMTRS.DisplayFormat.FormatString = "0.00"
        Me.GNMTRS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GNMTRS.FieldName = "NMTRS"
        Me.GNMTRS.Name = "GNMTRS"
        Me.GNMTRS.OptionsColumn.AllowEdit = False
        '
        'GRDATE
        '
        Me.GRDATE.Caption = "Return Date"
        Me.GRDATE.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GRDATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GRDATE.FieldName = "RDATE"
        Me.GRDATE.Name = "GRDATE"
        Me.GRDATE.Visible = True
        Me.GRDATE.VisibleIndex = 10
        Me.GRDATE.Width = 80
        '
        'GTRANSPORT
        '
        Me.GTRANSPORT.Caption = "Transport"
        Me.GTRANSPORT.ColumnEdit = Me.CMBTRANSPORT
        Me.GTRANSPORT.FieldName = "TRANSNAME"
        Me.GTRANSPORT.Name = "GTRANSPORT"
        Me.GTRANSPORT.Visible = True
        Me.GTRANSPORT.VisibleIndex = 11
        Me.GTRANSPORT.Width = 200
        '
        'CMBTRANSPORT
        '
        Me.CMBTRANSPORT.AutoHeight = False
        Me.CMBTRANSPORT.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CMBTRANSPORT.Name = "CMBTRANSPORT"
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
        Me.APPROXDATE.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.APPROXDATE.Name = "APPROXDATE"
        '
        'RBENTERED
        '
        Me.RBENTERED.AutoSize = True
        Me.RBENTERED.BackColor = System.Drawing.Color.Transparent
        Me.RBENTERED.Location = New System.Drawing.Point(93, 10)
        Me.RBENTERED.Name = "RBENTERED"
        Me.RBENTERED.Size = New System.Drawing.Size(66, 19)
        Me.RBENTERED.TabIndex = 798
        Me.RBENTERED.Text = "Entered"
        Me.RBENTERED.UseVisualStyleBackColor = False
        '
        'RBPENDING
        '
        Me.RBPENDING.AutoSize = True
        Me.RBPENDING.BackColor = System.Drawing.Color.Transparent
        Me.RBPENDING.Checked = True
        Me.RBPENDING.Location = New System.Drawing.Point(18, 10)
        Me.RBPENDING.Name = "RBPENDING"
        Me.RBPENDING.Size = New System.Drawing.Size(69, 19)
        Me.RBPENDING.TabIndex = 797
        Me.RBPENDING.TabStop = True
        Me.RBPENDING.Text = "Pending"
        Me.RBPENDING.UseVisualStyleBackColor = False
        '
        'CMDDELETE
        '
        Me.CMDDELETE.BackColor = System.Drawing.Color.Transparent
        Me.CMDDELETE.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDDELETE.FlatAppearance.BorderSize = 0
        Me.CMDDELETE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDDELETE.ForeColor = System.Drawing.Color.Black
        Me.CMDDELETE.Location = New System.Drawing.Point(535, 522)
        Me.CMDDELETE.Name = "CMDDELETE"
        Me.CMDDELETE.Size = New System.Drawing.Size(80, 28)
        Me.CMDDELETE.TabIndex = 796
        Me.CMDDELETE.Text = "&Delete"
        Me.CMDDELETE.UseVisualStyleBackColor = False
        '
        'CMDOK
        '
        Me.CMDOK.BackColor = System.Drawing.Color.Transparent
        Me.CMDOK.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDOK.FlatAppearance.BorderSize = 0
        Me.CMDOK.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDOK.ForeColor = System.Drawing.Color.Black
        Me.CMDOK.Location = New System.Drawing.Point(450, 522)
        Me.CMDOK.Name = "CMDOK"
        Me.CMDOK.Size = New System.Drawing.Size(80, 28)
        Me.CMDOK.TabIndex = 793
        Me.CMDOK.Text = "&Save"
        Me.CMDOK.UseVisualStyleBackColor = False
        '
        'CMDREFRESH
        '
        Me.CMDREFRESH.BackColor = System.Drawing.Color.Transparent
        Me.CMDREFRESH.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDREFRESH.FlatAppearance.BorderSize = 0
        Me.CMDREFRESH.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDREFRESH.ForeColor = System.Drawing.Color.Black
        Me.CMDREFRESH.Location = New System.Drawing.Point(620, 522)
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
        Me.cmdcancel.Location = New System.Drawing.Point(705, 522)
        Me.cmdcancel.Name = "cmdcancel"
        Me.cmdcancel.Size = New System.Drawing.Size(80, 28)
        Me.cmdcancel.TabIndex = 795
        Me.cmdcancel.Text = "E&xit"
        Me.cmdcancel.UseVisualStyleBackColor = False
        '
        'PendingReturnDate
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1234, 570)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "PendingReturnDate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Pending Goods Return Date"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CMBTRANSPORT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CHKEDIT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.APPROXDATE.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.APPROXDATE, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents RBENTERED As System.Windows.Forms.RadioButton
    Friend WithEvents RBPENDING As System.Windows.Forms.RadioButton
    Friend WithEvents CMDDELETE As System.Windows.Forms.Button
    Friend WithEvents CMDOK As System.Windows.Forms.Button
    Friend WithEvents CMDREFRESH As System.Windows.Forms.Button
    Friend WithEvents cmdcancel As System.Windows.Forms.Button
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Private WithEvents GGRNO As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GDATE As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GGODOWN As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GDYEINGNAME As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GLOTNO As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GQUALITY As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GCTAKA As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GCMTRS As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GRTAKA As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GRMTRS As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GRDONO As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GSHORTAGE As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GNTAKA As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GNMTRS As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GRDATE As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents CHKEDIT As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Private WithEvents APPROXDATE As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Private WithEvents GTRANSPORT As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents CMBTRANSPORT As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Private WithEvents GNAME As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GINVOICENO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTYPE As DevExpress.XtraGrid.Columns.GridColumn
End Class
