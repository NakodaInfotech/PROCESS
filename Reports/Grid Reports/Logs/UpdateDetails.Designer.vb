<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UpdateDetails
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
        Me.Todate = New System.Windows.Forms.DateTimePicker
        Me.fromdate = New System.Windows.Forms.DateTimePicker
        Me.LBLTO = New System.Windows.Forms.Label
        Me.LBLFROM = New System.Windows.Forms.Label
        Me.cmddel = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.lbl = New System.Windows.Forms.Label
        Me.cmdcancel = New System.Windows.Forms.Button
        Me.griddetails = New DevExpress.XtraGrid.GridControl
        Me.gridpayment = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.GID = New DevExpress.XtraGrid.Columns.GridColumn
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
        Me.BlendPanel1.Controls.Add(Me.Todate)
        Me.BlendPanel1.Controls.Add(Me.fromdate)
        Me.BlendPanel1.Controls.Add(Me.LBLTO)
        Me.BlendPanel1.Controls.Add(Me.LBLFROM)
        Me.BlendPanel1.Controls.Add(Me.cmddel)
        Me.BlendPanel1.Controls.Add(Me.Button1)
        Me.BlendPanel1.Controls.Add(Me.lbl)
        Me.BlendPanel1.Controls.Add(Me.cmdcancel)
        Me.BlendPanel1.Controls.Add(Me.griddetails)
        Me.BlendPanel1.Controls.Add(Me.ToolStrip1)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1188, 562)
        Me.BlendPanel1.TabIndex = 5
        '
        'Todate
        '
        Me.Todate.CustomFormat = "dd/MM/yyyy"
        Me.Todate.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Todate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Todate.Location = New System.Drawing.Point(271, 473)
        Me.Todate.Name = "Todate"
        Me.Todate.Size = New System.Drawing.Size(88, 23)
        Me.Todate.TabIndex = 333
        '
        'fromdate
        '
        Me.fromdate.CustomFormat = "dd/MM/yyyy"
        Me.fromdate.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.fromdate.Location = New System.Drawing.Point(145, 473)
        Me.fromdate.Name = "fromdate"
        Me.fromdate.Size = New System.Drawing.Size(86, 23)
        Me.fromdate.TabIndex = 332
        Me.fromdate.Value = New Date(2014, 3, 11, 0, 0, 0, 0)
        '
        'LBLTO
        '
        Me.LBLTO.AutoSize = True
        Me.LBLTO.BackColor = System.Drawing.Color.Transparent
        Me.LBLTO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTO.Location = New System.Drawing.Point(239, 477)
        Me.LBLTO.Name = "LBLTO"
        Me.LBLTO.Size = New System.Drawing.Size(19, 15)
        Me.LBLTO.TabIndex = 331
        Me.LBLTO.Text = "To"
        '
        'LBLFROM
        '
        Me.LBLFROM.AutoSize = True
        Me.LBLFROM.BackColor = System.Drawing.Color.Transparent
        Me.LBLFROM.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLFROM.Location = New System.Drawing.Point(109, 477)
        Me.LBLFROM.Name = "LBLFROM"
        Me.LBLFROM.Size = New System.Drawing.Size(35, 15)
        Me.LBLFROM.TabIndex = 330
        Me.LBLFROM.Text = "From"
        '
        'cmddel
        '
        Me.cmddel.BackColor = System.Drawing.Color.Transparent
        Me.cmddel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmddel.FlatAppearance.BorderSize = 0
        Me.cmddel.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmddel.ForeColor = System.Drawing.Color.Black
        Me.cmddel.Location = New System.Drawing.Point(408, 470)
        Me.cmddel.Name = "cmddel"
        Me.cmddel.Size = New System.Drawing.Size(80, 28)
        Me.cmddel.TabIndex = 329
        Me.cmddel.Text = "&Delete"
        Me.cmddel.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Transparent
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.Black
        Me.Button1.Location = New System.Drawing.Point(580, 470)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(80, 28)
        Me.Button1.TabIndex = 322
        Me.Button1.Text = "&Refresh"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'lbl
        '
        Me.lbl.AutoSize = True
        Me.lbl.BackColor = System.Drawing.Color.Transparent
        Me.lbl.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lbl.Location = New System.Drawing.Point(19, 30)
        Me.lbl.Name = "lbl"
        Me.lbl.Size = New System.Drawing.Size(119, 26)
        Me.lbl.TabIndex = 321
        Me.lbl.Text = "Update Logs"
        '
        'cmdcancel
        '
        Me.cmdcancel.BackColor = System.Drawing.Color.Transparent
        Me.cmdcancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdcancel.FlatAppearance.BorderSize = 0
        Me.cmdcancel.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdcancel.ForeColor = System.Drawing.Color.Black
        Me.cmdcancel.Location = New System.Drawing.Point(494, 470)
        Me.cmdcancel.Name = "cmdcancel"
        Me.cmdcancel.Size = New System.Drawing.Size(80, 28)
        Me.cmdcancel.TabIndex = 320
        Me.cmdcancel.Text = "E&xit"
        Me.cmdcancel.UseVisualStyleBackColor = False
        '
        'griddetails
        '
        Me.griddetails.Location = New System.Drawing.Point(16, 59)
        Me.griddetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.griddetails.MainView = Me.gridpayment
        Me.griddetails.Name = "griddetails"
        Me.griddetails.Size = New System.Drawing.Size(1157, 394)
        Me.griddetails.TabIndex = 1
        Me.griddetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridpayment})
        '
        'gridpayment
        '
        Me.gridpayment.Appearance.Row.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridpayment.Appearance.Row.Options.UseFont = True
        Me.gridpayment.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GID, Me.GSRNO, Me.GTABLE, Me.GNAME, Me.GUSER, Me.GCMP, Me.GDATE})
        Me.gridpayment.GridControl = Me.griddetails
        Me.gridpayment.Name = "gridpayment"
        Me.gridpayment.OptionsBehavior.Editable = False
        Me.gridpayment.OptionsCustomization.AllowColumnMoving = False
        Me.gridpayment.OptionsCustomization.AllowGroup = False
        Me.gridpayment.OptionsCustomization.AllowQuickHideColumns = False
        Me.gridpayment.OptionsView.ColumnAutoWidth = False
        Me.gridpayment.OptionsView.ShowGroupPanel = False
        '
        'GID
        '
        Me.GID.AppearanceCell.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GID.AppearanceCell.Options.UseFont = True
        Me.GID.AppearanceHeader.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GID.AppearanceHeader.Options.UseFont = True
        Me.GID.Caption = "ID"
        Me.GID.FieldName = "ID"
        Me.GID.Name = "GID"
        '
        'GSRNO
        '
        Me.GSRNO.AppearanceCell.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GSRNO.AppearanceCell.Options.UseFont = True
        Me.GSRNO.AppearanceHeader.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GSRNO.AppearanceHeader.Options.UseFont = True
        Me.GSRNO.Caption = "Sr No"
        Me.GSRNO.FieldName = "DELETE_NO"
        Me.GSRNO.ImageIndex = 1
        Me.GSRNO.Name = "GSRNO"
        Me.GSRNO.Width = 50
        '
        'GTABLE
        '
        Me.GTABLE.AppearanceCell.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GTABLE.AppearanceCell.Options.UseFont = True
        Me.GTABLE.AppearanceHeader.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GTABLE.AppearanceHeader.Options.UseFont = True
        Me.GTABLE.Caption = "Table"
        Me.GTABLE.FieldName = "TABLE"
        Me.GTABLE.Name = "GTABLE"
        Me.GTABLE.Visible = True
        Me.GTABLE.VisibleIndex = 0
        Me.GTABLE.Width = 100
        '
        'GNAME
        '
        Me.GNAME.AppearanceCell.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GNAME.AppearanceCell.Options.UseFont = True
        Me.GNAME.AppearanceHeader.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GNAME.AppearanceHeader.Options.UseFont = True
        Me.GNAME.Caption = "Remarks"
        Me.GNAME.FieldName = "REMARKS"
        Me.GNAME.ImageIndex = 0
        Me.GNAME.Name = "GNAME"
        Me.GNAME.Visible = True
        Me.GNAME.VisibleIndex = 1
        Me.GNAME.Width = 710
        '
        'GUSER
        '
        Me.GUSER.AppearanceCell.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GUSER.AppearanceCell.Options.UseFont = True
        Me.GUSER.AppearanceHeader.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GUSER.AppearanceHeader.Options.UseFont = True
        Me.GUSER.Caption = "User"
        Me.GUSER.FieldName = "USER"
        Me.GUSER.Name = "GUSER"
        Me.GUSER.Visible = True
        Me.GUSER.VisibleIndex = 3
        Me.GUSER.Width = 100
        '
        'GCMP
        '
        Me.GCMP.AppearanceCell.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GCMP.AppearanceCell.Options.UseFont = True
        Me.GCMP.AppearanceHeader.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GCMP.AppearanceHeader.Options.UseFont = True
        Me.GCMP.Caption = "Company"
        Me.GCMP.FieldName = "CMPNAME"
        Me.GCMP.Name = "GCMP"
        Me.GCMP.Visible = True
        Me.GCMP.VisibleIndex = 4
        Me.GCMP.Width = 100
        '
        'GDATE
        '
        Me.GDATE.AppearanceCell.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GDATE.AppearanceCell.Options.UseFont = True
        Me.GDATE.AppearanceHeader.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GDATE.AppearanceHeader.Options.UseFont = True
        Me.GDATE.Caption = "Date"
        Me.GDATE.FieldName = "DATE"
        Me.GDATE.Name = "GDATE"
        Me.GDATE.Visible = True
        Me.GDATE.VisibleIndex = 2
        Me.GDATE.Width = 100
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExcelExport, Me.toolStripSeparator})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1188, 25)
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
        'UpdateDetails
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1188, 562)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "UpdateDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Update Logs"
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
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents lbl As System.Windows.Forms.Label
    Friend WithEvents cmdcancel As System.Windows.Forms.Button
    Friend WithEvents griddetails As DevExpress.XtraGrid.GridControl
    Friend WithEvents gridpayment As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GSRNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTABLE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GUSER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCMP As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ExcelExport As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents GID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Todate As System.Windows.Forms.DateTimePicker
    Friend WithEvents fromdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents LBLTO As System.Windows.Forms.Label
    Friend WithEvents LBLFROM As System.Windows.Forms.Label
    Friend WithEvents cmddel As System.Windows.Forms.Button
End Class
