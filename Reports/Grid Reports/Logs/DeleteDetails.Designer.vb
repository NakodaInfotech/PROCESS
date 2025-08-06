<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DeleteDetails
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
        Me.CHKDATE = New System.Windows.Forms.CheckBox
        Me.Todate = New System.Windows.Forms.DateTimePicker
        Me.fromdate = New System.Windows.Forms.DateTimePicker
        Me.LBLTO = New System.Windows.Forms.Label
        Me.LBLFROM = New System.Windows.Forms.Label
        Me.CMDDELETE = New System.Windows.Forms.Button
        Me.CMDREFRESH = New System.Windows.Forms.Button
        Me.lbl = New System.Windows.Forms.Label
        Me.CMDEXIT = New System.Windows.Forms.Button
        Me.griddetails = New DevExpress.XtraGrid.GridControl
        Me.gridpayment = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.Gid = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GSRNO = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GTABLE = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GNAME = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GUSER = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GCMP = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GDATE = New DevExpress.XtraGrid.Columns.GridColumn
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.ExcelExport = New System.Windows.Forms.ToolStripButton
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator
        Me.BlendPanel1.SuspendLayout()
        CType(Me.griddetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridpayment, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.CHKDATE)
        Me.BlendPanel1.Controls.Add(Me.Todate)
        Me.BlendPanel1.Controls.Add(Me.fromdate)
        Me.BlendPanel1.Controls.Add(Me.LBLTO)
        Me.BlendPanel1.Controls.Add(Me.LBLFROM)
        Me.BlendPanel1.Controls.Add(Me.CMDDELETE)
        Me.BlendPanel1.Controls.Add(Me.CMDREFRESH)
        Me.BlendPanel1.Controls.Add(Me.lbl)
        Me.BlendPanel1.Controls.Add(Me.CMDEXIT)
        Me.BlendPanel1.Controls.Add(Me.griddetails)
        Me.BlendPanel1.Controls.Add(Me.ToolStrip1)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1194, 561)
        Me.BlendPanel1.TabIndex = 5
        '
        'CHKDATE
        '
        Me.CHKDATE.AutoSize = True
        Me.CHKDATE.BackColor = System.Drawing.Color.Transparent
        Me.CHKDATE.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHKDATE.Location = New System.Drawing.Point(213, 487)
        Me.CHKDATE.Name = "CHKDATE"
        Me.CHKDATE.Size = New System.Drawing.Size(52, 18)
        Me.CHKDATE.TabIndex = 496
        Me.CHKDATE.Text = "Date"
        Me.CHKDATE.UseVisualStyleBackColor = False
        '
        'Todate
        '
        Me.Todate.CustomFormat = "dd/MM/yyyy"
        Me.Todate.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Todate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Todate.Location = New System.Drawing.Point(304, 514)
        Me.Todate.Name = "Todate"
        Me.Todate.Size = New System.Drawing.Size(86, 23)
        Me.Todate.TabIndex = 328
        '
        'fromdate
        '
        Me.fromdate.CustomFormat = "dd/MM/yyyy"
        Me.fromdate.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.fromdate.Location = New System.Drawing.Point(304, 485)
        Me.fromdate.Name = "fromdate"
        Me.fromdate.Size = New System.Drawing.Size(86, 23)
        Me.fromdate.TabIndex = 327
        Me.fromdate.Value = New Date(2014, 3, 11, 0, 0, 0, 0)
        '
        'LBLTO
        '
        Me.LBLTO.AutoSize = True
        Me.LBLTO.BackColor = System.Drawing.Color.Transparent
        Me.LBLTO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTO.Location = New System.Drawing.Point(280, 517)
        Me.LBLTO.Name = "LBLTO"
        Me.LBLTO.Size = New System.Drawing.Size(19, 15)
        Me.LBLTO.TabIndex = 325
        Me.LBLTO.Text = "To"
        '
        'LBLFROM
        '
        Me.LBLFROM.AutoSize = True
        Me.LBLFROM.BackColor = System.Drawing.Color.Transparent
        Me.LBLFROM.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLFROM.Location = New System.Drawing.Point(265, 488)
        Me.LBLFROM.Name = "LBLFROM"
        Me.LBLFROM.Size = New System.Drawing.Size(35, 15)
        Me.LBLFROM.TabIndex = 324
        Me.LBLFROM.Text = "From"
        '
        'CMDDELETE
        '
        Me.CMDDELETE.BackColor = System.Drawing.Color.Transparent
        Me.CMDDELETE.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDDELETE.FlatAppearance.BorderSize = 0
        Me.CMDDELETE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDDELETE.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.CMDDELETE.Location = New System.Drawing.Point(448, 491)
        Me.CMDDELETE.Name = "CMDDELETE"
        Me.CMDDELETE.Size = New System.Drawing.Size(80, 28)
        Me.CMDDELETE.TabIndex = 323
        Me.CMDDELETE.Text = "&Delete"
        Me.CMDDELETE.UseVisualStyleBackColor = False
        '
        'CMDREFRESH
        '
        Me.CMDREFRESH.BackColor = System.Drawing.Color.Transparent
        Me.CMDREFRESH.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDREFRESH.FlatAppearance.BorderSize = 0
        Me.CMDREFRESH.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDREFRESH.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.CMDREFRESH.Location = New System.Drawing.Point(620, 491)
        Me.CMDREFRESH.Name = "CMDREFRESH"
        Me.CMDREFRESH.Size = New System.Drawing.Size(80, 28)
        Me.CMDREFRESH.TabIndex = 322
        Me.CMDREFRESH.Text = "Refresh"
        Me.CMDREFRESH.UseVisualStyleBackColor = False
        '
        'lbl
        '
        Me.lbl.AutoSize = True
        Me.lbl.BackColor = System.Drawing.Color.Transparent
        Me.lbl.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lbl.Location = New System.Drawing.Point(27, 28)
        Me.lbl.Name = "lbl"
        Me.lbl.Size = New System.Drawing.Size(103, 23)
        Me.lbl.TabIndex = 321
        Me.lbl.Text = "Delete Logs"
        '
        'CMDEXIT
        '
        Me.CMDEXIT.BackColor = System.Drawing.Color.Transparent
        Me.CMDEXIT.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDEXIT.FlatAppearance.BorderSize = 0
        Me.CMDEXIT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDEXIT.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.CMDEXIT.Location = New System.Drawing.Point(534, 491)
        Me.CMDEXIT.Name = "CMDEXIT"
        Me.CMDEXIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEXIT.TabIndex = 320
        Me.CMDEXIT.Text = "E&xit"
        Me.CMDEXIT.UseVisualStyleBackColor = False
        '
        'griddetails
        '
        Me.griddetails.Location = New System.Drawing.Point(18, 54)
        Me.griddetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.griddetails.MainView = Me.gridpayment
        Me.griddetails.Name = "griddetails"
        Me.griddetails.Size = New System.Drawing.Size(1173, 410)
        Me.griddetails.TabIndex = 1
        Me.griddetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridpayment})
        '
        'gridpayment
        '
        Me.gridpayment.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridpayment.Appearance.Row.Options.UseFont = True
        Me.gridpayment.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.Gid, Me.GSRNO, Me.GTABLE, Me.GNAME, Me.GUSER, Me.GCMP, Me.GDATE})
        Me.gridpayment.GridControl = Me.griddetails
        Me.gridpayment.Name = "gridpayment"
        Me.gridpayment.OptionsBehavior.Editable = False
        Me.gridpayment.OptionsCustomization.AllowColumnMoving = False
        Me.gridpayment.OptionsCustomization.AllowGroup = False
        Me.gridpayment.OptionsCustomization.AllowQuickHideColumns = False
        Me.gridpayment.OptionsView.ColumnAutoWidth = False
        Me.gridpayment.OptionsView.ShowGroupPanel = False
        '
        'Gid
        '
        Me.Gid.Caption = "Id"
        Me.Gid.FieldName = "ID"
        Me.Gid.Name = "Gid"
        '
        'GSRNO
        '
        Me.GSRNO.Caption = "Sr No"
        Me.GSRNO.FieldName = "DELETE_NO"
        Me.GSRNO.ImageIndex = 1
        Me.GSRNO.Name = "GSRNO"
        Me.GSRNO.Width = 50
        '
        'GTABLE
        '
        Me.GTABLE.Caption = "Table"
        Me.GTABLE.FieldName = "TABLE"
        Me.GTABLE.Name = "GTABLE"
        Me.GTABLE.Visible = True
        Me.GTABLE.VisibleIndex = 0
        Me.GTABLE.Width = 100
        '
        'GNAME
        '
        Me.GNAME.Caption = "Remarks"
        Me.GNAME.FieldName = "REMARKS"
        Me.GNAME.ImageIndex = 0
        Me.GNAME.Name = "GNAME"
        Me.GNAME.Visible = True
        Me.GNAME.VisibleIndex = 1
        Me.GNAME.Width = 1000
        '
        'GUSER
        '
        Me.GUSER.Caption = "User"
        Me.GUSER.FieldName = "USER"
        Me.GUSER.Name = "GUSER"
        Me.GUSER.Visible = True
        Me.GUSER.VisibleIndex = 3
        Me.GUSER.Width = 90
        '
        'GCMP
        '
        Me.GCMP.Caption = "Company"
        Me.GCMP.FieldName = "CMPNAME"
        Me.GCMP.Name = "GCMP"
        Me.GCMP.Visible = True
        Me.GCMP.VisibleIndex = 4
        Me.GCMP.Width = 100
        '
        'GDATE
        '
        Me.GDATE.Caption = "Date"
        Me.GDATE.FieldName = "DATE"
        Me.GDATE.Name = "GDATE"
        Me.GDATE.Visible = True
        Me.GDATE.VisibleIndex = 2
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExcelExport, Me.toolStripSeparator})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1194, 25)
        Me.ToolStrip1.TabIndex = 4
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
        'DeleteDetails
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1194, 561)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "DeleteDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Delete Details"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.griddetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridpayment, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents CHKDATE As System.Windows.Forms.CheckBox
    Friend WithEvents Todate As System.Windows.Forms.DateTimePicker
    Friend WithEvents fromdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents LBLTO As System.Windows.Forms.Label
    Friend WithEvents LBLFROM As System.Windows.Forms.Label
    Friend WithEvents CMDDELETE As System.Windows.Forms.Button
    Friend WithEvents CMDREFRESH As System.Windows.Forms.Button
    Friend WithEvents lbl As System.Windows.Forms.Label
    Friend WithEvents CMDEXIT As System.Windows.Forms.Button
    Friend WithEvents griddetails As DevExpress.XtraGrid.GridControl
    Friend WithEvents gridpayment As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Gid As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSRNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTABLE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GUSER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCMP As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ExcelExport As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
End Class
