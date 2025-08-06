<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PayOutstanding
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PayOutstanding))
        Me.GAMTPAID = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GBALANCE = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GGTOTAL = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GEXTRA = New DevExpress.XtraGrid.Columns.GridColumn
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator
        Me.GAGENT = New DevExpress.XtraGrid.Columns.GridColumn
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.GBILLDATE = New DevExpress.XtraGrid.Columns.GridColumn
        Me.CMBSEARCH = New System.Windows.Forms.ComboBox
        Me.GBILL = New DevExpress.XtraGrid.Columns.GridColumn
        Me.BlendPanel1 = New VbPowerPack.BlendPanel
        Me.cmdexit = New System.Windows.Forms.Button
        Me.cmdok = New System.Windows.Forms.Button
        Me.gridbilldetails = New DevExpress.XtraGrid.GridControl
        Me.gridbill = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.GREGISTER = New DevExpress.XtraGrid.Columns.GridColumn
        Me.gsrno = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GPUNO = New DevExpress.XtraGrid.Columns.GridColumn
        Me.gdate = New DevExpress.XtraGrid.Columns.GridColumn
        Me.gname = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GRETURN = New DevExpress.XtraGrid.Columns.GridColumn
        Me.lbl = New System.Windows.Forms.Label
        Me.ToolStrip1.SuspendLayout()
        Me.BlendPanel1.SuspendLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GAMTPAID
        '
        Me.GAMTPAID.Caption = "Amt. Paid"
        Me.GAMTPAID.DisplayFormat.FormatString = "0.00"
        Me.GAMTPAID.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GAMTPAID.FieldName = "AMTREC"
        Me.GAMTPAID.Name = "GAMTPAID"
        Me.GAMTPAID.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GAMTPAID.Visible = True
        Me.GAMTPAID.VisibleIndex = 7
        '
        'GBALANCE
        '
        Me.GBALANCE.Caption = "Balance"
        Me.GBALANCE.DisplayFormat.FormatString = "0.00"
        Me.GBALANCE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GBALANCE.FieldName = "BALANCE"
        Me.GBALANCE.Name = "GBALANCE"
        Me.GBALANCE.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GBALANCE.Visible = True
        Me.GBALANCE.VisibleIndex = 10
        Me.GBALANCE.Width = 85
        '
        'GGTOTAL
        '
        Me.GGTOTAL.Caption = "Grand Total"
        Me.GGTOTAL.DisplayFormat.FormatString = "0.00"
        Me.GGTOTAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GGTOTAL.FieldName = "GRANDTOTAL"
        Me.GGTOTAL.Name = "GGTOTAL"
        Me.GGTOTAL.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GGTOTAL.Visible = True
        Me.GGTOTAL.VisibleIndex = 6
        '
        'GEXTRA
        '
        Me.GEXTRA.Caption = "Extra Amt."
        Me.GEXTRA.DisplayFormat.FormatString = "0.00"
        Me.GEXTRA.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GEXTRA.FieldName = "EXTRAAMT"
        Me.GEXTRA.Name = "GEXTRA"
        Me.GEXTRA.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GEXTRA.Visible = True
        Me.GEXTRA.VisibleIndex = 8
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
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'GAGENT
        '
        Me.GAGENT.Caption = "Agent"
        Me.GAGENT.FieldName = "AGENT"
        Me.GAGENT.Name = "GAGENT"
        Me.GAGENT.Visible = True
        Me.GAGENT.VisibleIndex = 5
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripSeparator, Me.PrintToolStripButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1043, 25)
        Me.ToolStrip1.TabIndex = 4
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'GBILLDATE
        '
        Me.GBILLDATE.Caption = "Bill Date"
        Me.GBILLDATE.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GBILLDATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GBILLDATE.FieldName = "GDNDATE"
        Me.GBILLDATE.Name = "GBILLDATE"
        Me.GBILLDATE.Visible = True
        Me.GBILLDATE.VisibleIndex = 4
        '
        'CMBSEARCH
        '
        Me.CMBSEARCH.FormattingEnabled = True
        Me.CMBSEARCH.Items.AddRange(New Object() {"COMPANY", "ALL"})
        Me.CMBSEARCH.Location = New System.Drawing.Point(899, 33)
        Me.CMBSEARCH.Name = "CMBSEARCH"
        Me.CMBSEARCH.Size = New System.Drawing.Size(121, 23)
        Me.CMBSEARCH.TabIndex = 320
        '
        'GBILL
        '
        Me.GBILL.Caption = "Party Bill No."
        Me.GBILL.FieldName = "GDN"
        Me.GBILL.Name = "GBILL"
        Me.GBILL.Visible = True
        Me.GBILL.VisibleIndex = 3
        Me.GBILL.Width = 60
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Controls.Add(Me.cmdok)
        Me.BlendPanel1.Controls.Add(Me.CMBSEARCH)
        Me.BlendPanel1.Controls.Add(Me.gridbilldetails)
        Me.BlendPanel1.Controls.Add(Me.ToolStrip1)
        Me.BlendPanel1.Controls.Add(Me.lbl)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1043, 582)
        Me.BlendPanel1.TabIndex = 8
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdexit.FlatAppearance.BorderSize = 0
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.Black
        Me.cmdexit.Location = New System.Drawing.Point(533, 541)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 28)
        Me.cmdexit.TabIndex = 323
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'cmdok
        '
        Me.cmdok.BackColor = System.Drawing.Color.Transparent
        Me.cmdok.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdok.FlatAppearance.BorderSize = 0
        Me.cmdok.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdok.ForeColor = System.Drawing.Color.Black
        Me.cmdok.Location = New System.Drawing.Point(447, 541)
        Me.cmdok.Name = "cmdok"
        Me.cmdok.Size = New System.Drawing.Size(80, 28)
        Me.cmdok.TabIndex = 322
        Me.cmdok.Text = "&Ok"
        Me.cmdok.UseVisualStyleBackColor = False
        '
        'gridbilldetails
        '
        Me.gridbilldetails.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbilldetails.Location = New System.Drawing.Point(22, 61)
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.Size = New System.Drawing.Size(998, 467)
        Me.gridbilldetails.TabIndex = 318
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill})
        '
        'gridbill
        '
        Me.gridbill.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GREGISTER, Me.gsrno, Me.GPUNO, Me.gdate, Me.gname, Me.GBILL, Me.GBILLDATE, Me.GAGENT, Me.GGTOTAL, Me.GAMTPAID, Me.GEXTRA, Me.GRETURN, Me.GBALANCE})
        Me.gridbill.GridControl = Me.gridbilldetails
        Me.gridbill.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.gridbill.Name = "gridbill"
        Me.gridbill.OptionsBehavior.AllowIncrementalSearch = True
        Me.gridbill.OptionsBehavior.Editable = False
        Me.gridbill.OptionsView.ColumnAutoWidth = False
        Me.gridbill.OptionsView.ShowAutoFilterRow = True
        Me.gridbill.OptionsView.ShowFooter = True
        '
        'GREGISTER
        '
        Me.GREGISTER.Caption = "Register"
        Me.GREGISTER.FieldName = "REGTYPE"
        Me.GREGISTER.Name = "GREGISTER"
        '
        'gsrno
        '
        Me.gsrno.Caption = "Sr. No"
        Me.gsrno.FieldName = "BILLINITIALS"
        Me.gsrno.ImageIndex = 1
        Me.gsrno.Name = "gsrno"
        Me.gsrno.Visible = True
        Me.gsrno.VisibleIndex = 0
        Me.gsrno.Width = 60
        '
        'GPUNO
        '
        Me.GPUNO.Caption = "Purchase no."
        Me.GPUNO.FieldName = "BILL"
        Me.GPUNO.Name = "GPUNO"
        '
        'gdate
        '
        Me.gdate.Caption = "Date"
        Me.gdate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.gdate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.gdate.FieldName = "DATE"
        Me.gdate.Name = "gdate"
        Me.gdate.Visible = True
        Me.gdate.VisibleIndex = 1
        '
        'gname
        '
        Me.gname.Caption = "Name"
        Me.gname.FieldName = "NAME"
        Me.gname.ImageIndex = 0
        Me.gname.Name = "gname"
        Me.gname.Visible = True
        Me.gname.VisibleIndex = 2
        Me.gname.Width = 220
        '
        'GRETURN
        '
        Me.GRETURN.Caption = "Return"
        Me.GRETURN.FieldName = "RETURN"
        Me.GRETURN.Name = "GRETURN"
        Me.GRETURN.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GRETURN.Visible = True
        Me.GRETURN.VisibleIndex = 9
        '
        'lbl
        '
        Me.lbl.AutoSize = True
        Me.lbl.BackColor = System.Drawing.Color.Transparent
        Me.lbl.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lbl.Location = New System.Drawing.Point(20, 44)
        Me.lbl.Name = "lbl"
        Me.lbl.Size = New System.Drawing.Size(156, 14)
        Me.lbl.TabIndex = 251
        Me.lbl.Text = "Select an Invoice to Change"
        '
        'PayOutstanding
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1043, 582)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "PayOutstanding"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Payable Outstanding Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GAMTPAID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBALANCE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGTOTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GEXTRA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PrintToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents GAGENT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents GBILLDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CMBSEARCH As System.Windows.Forms.ComboBox
    Friend WithEvents GBILL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GREGISTER As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents gsrno As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPUNO As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents gdate As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents gname As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lbl As System.Windows.Forms.Label
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents cmdok As System.Windows.Forms.Button
    Friend WithEvents GRETURN As DevExpress.XtraGrid.Columns.GridColumn
End Class
