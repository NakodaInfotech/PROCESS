<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectGrey
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
        Dim RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
        Me.BlendPanel2 = New VbPowerPack.BlendPanel
        Me.Label1 = New System.Windows.Forms.Label
        Me.CMDEXIT = New System.Windows.Forms.Button
        Me.CMDOK = New System.Windows.Forms.Button
        Me.GridGreyRecd = New DevExpress.XtraGrid.GridControl
        Me.GridGrey = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GGREYRECNO = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GGREYRECDATE = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GWEAVER = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GGODOWN = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GTRANS = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GCHALLANNO = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GTOTALPCS = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GTOTALMTRS = New DevExpress.XtraGrid.Columns.GridColumn
        Me.BlendPanel2.SuspendLayout()
        CType(Me.GridGreyRecd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridGrey, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel2
        '
        Me.BlendPanel2.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.Color.White)
        Me.BlendPanel2.Controls.Add(Me.Label1)
        Me.BlendPanel2.Controls.Add(Me.CMDEXIT)
        Me.BlendPanel2.Controls.Add(Me.CMDOK)
        Me.BlendPanel2.Controls.Add(Me.GridGreyRecd)
        Me.BlendPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel2.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel2.Name = "BlendPanel2"
        Me.BlendPanel2.Size = New System.Drawing.Size(1075, 567)
        Me.BlendPanel2.TabIndex = 661
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(21, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(178, 23)
        Me.Label1.TabIndex = 659
        Me.Label1.Text = "Select Grey Received"
        '
        'CMDEXIT
        '
        Me.CMDEXIT.BackColor = System.Drawing.Color.Transparent
        Me.CMDEXIT.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDEXIT.FlatAppearance.BorderSize = 0
        Me.CMDEXIT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDEXIT.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.CMDEXIT.Location = New System.Drawing.Point(529, 525)
        Me.CMDEXIT.Name = "CMDEXIT"
        Me.CMDEXIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEXIT.TabIndex = 658
        Me.CMDEXIT.Text = "E&xit"
        Me.CMDEXIT.UseVisualStyleBackColor = False
        '
        'CMDOK
        '
        Me.CMDOK.BackColor = System.Drawing.Color.Transparent
        Me.CMDOK.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDOK.FlatAppearance.BorderSize = 0
        Me.CMDOK.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDOK.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.CMDOK.Location = New System.Drawing.Point(444, 525)
        Me.CMDOK.Name = "CMDOK"
        Me.CMDOK.Size = New System.Drawing.Size(80, 28)
        Me.CMDOK.TabIndex = 657
        Me.CMDOK.Text = "&Ok"
        Me.CMDOK.UseVisualStyleBackColor = False
        '
        'GridGreyRecd
        '
        Me.GridGreyRecd.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridGreyRecd.Location = New System.Drawing.Point(10, 36)
        Me.GridGreyRecd.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GridGreyRecd.MainView = Me.GridGrey
        Me.GridGreyRecd.Name = "GridGreyRecd"
        Me.GridGreyRecd.Size = New System.Drawing.Size(1033, 483)
        Me.GridGreyRecd.TabIndex = 656
        Me.GridGreyRecd.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridGrey})
        '
        'GridGrey
        '
        Me.GridGrey.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridGrey.Appearance.Row.Options.UseFont = True
        Me.GridGrey.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GGREYRECNO, Me.GGREYRECDATE, Me.GWEAVER, Me.GGODOWN, Me.GTRANS, Me.GCHALLANNO, Me.GTOTALPCS, Me.GTOTALMTRS})
        Me.GridGrey.GridControl = Me.GridGreyRecd
        Me.GridGrey.Name = "GridGrey"
        Me.GridGrey.OptionsBehavior.AllowIncrementalSearch = True
        Me.GridGrey.OptionsCustomization.AllowColumnMoving = False
        Me.GridGrey.OptionsCustomization.AllowGroup = False
        Me.GridGrey.OptionsCustomization.AllowQuickHideColumns = False
        Me.GridGrey.OptionsView.ColumnAutoWidth = False
        Me.GridGrey.OptionsView.ShowAutoFilterRow = True
        Me.GridGrey.OptionsView.ShowFooter = True
        Me.GridGrey.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        RepositoryItemCheckEdit1.AutoHeight = False
        RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        RepositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        Me.GridColumn1.ColumnEdit = RepositoryItemCheckEdit1
        Me.GridColumn1.FieldName = "CHK"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.ShowCaption = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 41
        '
        'GGREYRECNO
        '
        Me.GGREYRECNO.Caption = "Grey Recd. No."
        Me.GGREYRECNO.FieldName = "GREYRECNO"
        Me.GGREYRECNO.Name = "GGREYRECNO"
        Me.GGREYRECNO.OptionsColumn.AllowEdit = False
        Me.GGREYRECNO.Visible = True
        Me.GGREYRECNO.VisibleIndex = 1
        Me.GGREYRECNO.Width = 100
        '
        'GGREYRECDATE
        '
        Me.GGREYRECDATE.Caption = "Date"
        Me.GGREYRECDATE.FieldName = "GREYRECDATE"
        Me.GGREYRECDATE.Name = "GGREYRECDATE"
        Me.GGREYRECDATE.OptionsColumn.AllowEdit = False
        Me.GGREYRECDATE.Visible = True
        Me.GGREYRECDATE.VisibleIndex = 2
        Me.GGREYRECDATE.Width = 100
        '
        'GWEAVER
        '
        Me.GWEAVER.Caption = "Weaver Name"
        Me.GWEAVER.FieldName = "WEAVER"
        Me.GWEAVER.Name = "GWEAVER"
        Me.GWEAVER.OptionsColumn.AllowEdit = False
        Me.GWEAVER.Visible = True
        Me.GWEAVER.VisibleIndex = 3
        Me.GWEAVER.Width = 200
        '
        'GGODOWN
        '
        Me.GGODOWN.Caption = "Godown"
        Me.GGODOWN.FieldName = "GODOWN"
        Me.GGODOWN.Name = "GGODOWN"
        Me.GGODOWN.OptionsColumn.AllowEdit = False
        Me.GGODOWN.Visible = True
        Me.GGODOWN.VisibleIndex = 4
        Me.GGODOWN.Width = 150
        '
        'GTRANS
        '
        Me.GTRANS.Caption = "Transport"
        Me.GTRANS.FieldName = "TRANS"
        Me.GTRANS.Name = "GTRANS"
        Me.GTRANS.Visible = True
        Me.GTRANS.VisibleIndex = 5
        Me.GTRANS.Width = 150
        '
        'GCHALLANNO
        '
        Me.GCHALLANNO.Caption = "Challan No."
        Me.GCHALLANNO.FieldName = "CHALLANNO"
        Me.GCHALLANNO.Name = "GCHALLANNO"
        Me.GCHALLANNO.Visible = True
        Me.GCHALLANNO.VisibleIndex = 6
        Me.GCHALLANNO.Width = 100
        '
        'GTOTALPCS
        '
        Me.GTOTALPCS.Caption = "Total Pcs."
        Me.GTOTALPCS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTOTALPCS.FieldName = "TOTALPCS"
        Me.GTOTALPCS.Name = "GTOTALPCS"
        Me.GTOTALPCS.Visible = True
        Me.GTOTALPCS.VisibleIndex = 7
        '
        'GTOTALMTRS
        '
        Me.GTOTALMTRS.Caption = "Total Mtrs."
        Me.GTOTALMTRS.DisplayFormat.FormatString = "0.00"
        Me.GTOTALMTRS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTOTALMTRS.FieldName = "TOTALMTRS"
        Me.GTOTALMTRS.Name = "GTOTALMTRS"
        Me.GTOTALMTRS.Visible = True
        Me.GTOTALMTRS.VisibleIndex = 8
        '
        'SelectGrey
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1075, 567)
        Me.Controls.Add(Me.BlendPanel2)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "SelectGrey"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Select Grey Received"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel2.ResumeLayout(False)
        Me.BlendPanel2.PerformLayout()
        CType(Me.GridGreyRecd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridGrey, System.ComponentModel.ISupportInitialize).EndInit()
        CType(RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BlendPanel2 As VbPowerPack.BlendPanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CMDEXIT As System.Windows.Forms.Button
    Friend WithEvents CMDOK As System.Windows.Forms.Button
    Private WithEvents GridGreyRecd As DevExpress.XtraGrid.GridControl
    Private WithEvents GridGrey As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGREYRECNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGREYRECDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GWEAVER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGODOWN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTRANS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCHALLANNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTOTALPCS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTOTALMTRS As DevExpress.XtraGrid.Columns.GridColumn
End Class
