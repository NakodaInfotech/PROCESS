﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OpeningStockRollsSizer
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.BlendPanel1 = New VbPowerPack.BlendPanel
        Me.TXTADD = New System.Windows.Forms.TextBox
        Me.cmbcode = New System.Windows.Forms.ComboBox
        Me.LBLTOTALWT = New System.Windows.Forms.Label
        Me.CMBSIZERFILTER = New System.Windows.Forms.ComboBox
        Me.LBLTOTALROLLS = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.LBLTOTAL = New System.Windows.Forms.Label
        Me.TXTOPROLLSSTOCKNOSIZER = New System.Windows.Forms.TextBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.TXTPROGRAMNO = New System.Windows.Forms.TextBox
        Me.CMBMILL = New System.Windows.Forms.ComboBox
        Me.CMBQUALITY = New System.Windows.Forms.ComboBox
        Me.TXTREMARKS = New System.Windows.Forms.TextBox
        Me.TXTWT = New System.Windows.Forms.TextBox
        Me.TXTROLLS = New System.Windows.Forms.TextBox
        Me.TXTTOTALENDS = New System.Windows.Forms.TextBox
        Me.CMBSIZER = New System.Windows.Forms.ComboBox
        Me.GRIDSTOCK = New System.Windows.Forms.DataGridView
        Me.GOPROLLSTOCKNOSIZER = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GSIZER = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GPROGRAMNO = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GQUALITY = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GMILL = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GENDS = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GROLLS = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GWT = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GREMARKS = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GOUTROLLS = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GOUTWT = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cmdexit = New System.Windows.Forms.Button
        Me.BlendPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.GRIDSTOCK, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.AutoSize = True
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.TXTADD)
        Me.BlendPanel1.Controls.Add(Me.cmbcode)
        Me.BlendPanel1.Controls.Add(Me.LBLTOTALWT)
        Me.BlendPanel1.Controls.Add(Me.CMBSIZERFILTER)
        Me.BlendPanel1.Controls.Add(Me.LBLTOTALROLLS)
        Me.BlendPanel1.Controls.Add(Me.Label1)
        Me.BlendPanel1.Controls.Add(Me.LBLTOTAL)
        Me.BlendPanel1.Controls.Add(Me.TXTOPROLLSSTOCKNOSIZER)
        Me.BlendPanel1.Controls.Add(Me.Panel1)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1096, 596)
        Me.BlendPanel1.TabIndex = 0
        '
        'TXTADD
        '
        Me.TXTADD.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTADD.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTADD.Location = New System.Drawing.Point(712, 12)
        Me.TXTADD.Name = "TXTADD"
        Me.TXTADD.Size = New System.Drawing.Size(29, 21)
        Me.TXTADD.TabIndex = 807
        Me.TXTADD.TabStop = False
        Me.TXTADD.Visible = False
        '
        'cmbcode
        '
        Me.cmbcode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbcode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbcode.FormattingEnabled = True
        Me.cmbcode.Location = New System.Drawing.Point(747, 9)
        Me.cmbcode.Name = "cmbcode"
        Me.cmbcode.Size = New System.Drawing.Size(25, 22)
        Me.cmbcode.TabIndex = 717
        Me.cmbcode.Visible = False
        '
        'LBLTOTALWT
        '
        Me.LBLTOTALWT.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALWT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALWT.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALWT.Location = New System.Drawing.Point(712, 560)
        Me.LBLTOTALWT.Name = "LBLTOTALWT"
        Me.LBLTOTALWT.Size = New System.Drawing.Size(65, 15)
        Me.LBLTOTALWT.TabIndex = 834
        Me.LBLTOTALWT.Text = "0.000"
        Me.LBLTOTALWT.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CMBSIZERFILTER
        '
        Me.CMBSIZERFILTER.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBSIZERFILTER.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBSIZERFILTER.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBSIZERFILTER.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBSIZERFILTER.FormattingEnabled = True
        Me.CMBSIZERFILTER.Location = New System.Drawing.Point(85, 15)
        Me.CMBSIZERFILTER.Name = "CMBSIZERFILTER"
        Me.CMBSIZERFILTER.Size = New System.Drawing.Size(197, 23)
        Me.CMBSIZERFILTER.TabIndex = 0
        '
        'LBLTOTALROLLS
        '
        Me.LBLTOTALROLLS.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALROLLS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALROLLS.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALROLLS.Location = New System.Drawing.Point(633, 560)
        Me.LBLTOTALROLLS.Name = "LBLTOTALROLLS"
        Me.LBLTOTALROLLS.Size = New System.Drawing.Size(65, 15)
        Me.LBLTOTALROLLS.TabIndex = 832
        Me.LBLTOTALROLLS.Text = "0"
        Me.LBLTOTALROLLS.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(15, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 14)
        Me.Label1.TabIndex = 716
        Me.Label1.Text = "Sizer Name"
        '
        'LBLTOTAL
        '
        Me.LBLTOTAL.AutoSize = True
        Me.LBLTOTAL.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTAL.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTAL.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTAL.Location = New System.Drawing.Point(585, 560)
        Me.LBLTOTAL.Name = "LBLTOTAL"
        Me.LBLTOTAL.Size = New System.Drawing.Size(33, 15)
        Me.LBLTOTAL.TabIndex = 831
        Me.LBLTOTAL.Text = "Total"
        '
        'TXTOPROLLSSTOCKNOSIZER
        '
        Me.TXTOPROLLSSTOCKNOSIZER.BackColor = System.Drawing.Color.White
        Me.TXTOPROLLSSTOCKNOSIZER.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTOPROLLSSTOCKNOSIZER.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TXTOPROLLSSTOCKNOSIZER.Location = New System.Drawing.Point(778, 9)
        Me.TXTOPROLLSSTOCKNOSIZER.Name = "TXTOPROLLSSTOCKNOSIZER"
        Me.TXTOPROLLSSTOCKNOSIZER.ReadOnly = True
        Me.TXTOPROLLSSTOCKNOSIZER.Size = New System.Drawing.Size(30, 23)
        Me.TXTOPROLLSSTOCKNOSIZER.TabIndex = 715
        Me.TXTOPROLLSSTOCKNOSIZER.Text = " "
        Me.TXTOPROLLSSTOCKNOSIZER.Visible = False
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel1.Controls.Add(Me.TXTPROGRAMNO)
        Me.Panel1.Controls.Add(Me.CMBMILL)
        Me.Panel1.Controls.Add(Me.CMBQUALITY)
        Me.Panel1.Controls.Add(Me.TXTREMARKS)
        Me.Panel1.Controls.Add(Me.TXTWT)
        Me.Panel1.Controls.Add(Me.TXTROLLS)
        Me.Panel1.Controls.Add(Me.TXTTOTALENDS)
        Me.Panel1.Controls.Add(Me.CMBSIZER)
        Me.Panel1.Controls.Add(Me.GRIDSTOCK)
        Me.Panel1.Location = New System.Drawing.Point(13, 49)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1070, 505)
        Me.Panel1.TabIndex = 1
        '
        'TXTPROGRAMNO
        '
        Me.TXTPROGRAMNO.BackColor = System.Drawing.Color.Linen
        Me.TXTPROGRAMNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTPROGRAMNO.Location = New System.Drawing.Point(204, 3)
        Me.TXTPROGRAMNO.MaxLength = 10
        Me.TXTPROGRAMNO.Name = "TXTPROGRAMNO"
        Me.TXTPROGRAMNO.ReadOnly = True
        Me.TXTPROGRAMNO.Size = New System.Drawing.Size(80, 23)
        Me.TXTPROGRAMNO.TabIndex = 1
        Me.TXTPROGRAMNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CMBMILL
        '
        Me.CMBMILL.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBMILL.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBMILL.BackColor = System.Drawing.Color.Linen
        Me.CMBMILL.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBMILL.FormattingEnabled = True
        Me.CMBMILL.Location = New System.Drawing.Point(404, 3)
        Me.CMBMILL.Name = "CMBMILL"
        Me.CMBMILL.Size = New System.Drawing.Size(200, 23)
        Me.CMBMILL.TabIndex = 3
        Me.CMBMILL.TabStop = False
        '
        'CMBQUALITY
        '
        Me.CMBQUALITY.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBQUALITY.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBQUALITY.BackColor = System.Drawing.Color.Linen
        Me.CMBQUALITY.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBQUALITY.FormattingEnabled = True
        Me.CMBQUALITY.Location = New System.Drawing.Point(284, 3)
        Me.CMBQUALITY.Name = "CMBQUALITY"
        Me.CMBQUALITY.Size = New System.Drawing.Size(120, 23)
        Me.CMBQUALITY.TabIndex = 2
        Me.CMBQUALITY.TabStop = False
        '
        'TXTREMARKS
        '
        Me.TXTREMARKS.BackColor = System.Drawing.Color.White
        Me.TXTREMARKS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTREMARKS.Location = New System.Drawing.Point(844, 3)
        Me.TXTREMARKS.MaxLength = 200
        Me.TXTREMARKS.Name = "TXTREMARKS"
        Me.TXTREMARKS.Size = New System.Drawing.Size(200, 23)
        Me.TXTREMARKS.TabIndex = 7
        '
        'TXTWT
        '
        Me.TXTWT.BackColor = System.Drawing.Color.LemonChiffon
        Me.TXTWT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTWT.Location = New System.Drawing.Point(764, 3)
        Me.TXTWT.MaxLength = 10
        Me.TXTWT.Name = "TXTWT"
        Me.TXTWT.Size = New System.Drawing.Size(80, 23)
        Me.TXTWT.TabIndex = 6
        Me.TXTWT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTROLLS
        '
        Me.TXTROLLS.BackColor = System.Drawing.Color.Linen
        Me.TXTROLLS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTROLLS.Location = New System.Drawing.Point(684, 3)
        Me.TXTROLLS.MaxLength = 10
        Me.TXTROLLS.Name = "TXTROLLS"
        Me.TXTROLLS.Size = New System.Drawing.Size(80, 23)
        Me.TXTROLLS.TabIndex = 5
        Me.TXTROLLS.TabStop = False
        Me.TXTROLLS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTTOTALENDS
        '
        Me.TXTTOTALENDS.BackColor = System.Drawing.Color.Linen
        Me.TXTTOTALENDS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTTOTALENDS.Location = New System.Drawing.Point(604, 3)
        Me.TXTTOTALENDS.MaxLength = 10
        Me.TXTTOTALENDS.Name = "TXTTOTALENDS"
        Me.TXTTOTALENDS.Size = New System.Drawing.Size(80, 23)
        Me.TXTTOTALENDS.TabIndex = 4
        Me.TXTTOTALENDS.TabStop = False
        Me.TXTTOTALENDS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CMBSIZER
        '
        Me.CMBSIZER.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBSIZER.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBSIZER.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBSIZER.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBSIZER.FormattingEnabled = True
        Me.CMBSIZER.Location = New System.Drawing.Point(4, 3)
        Me.CMBSIZER.Name = "CMBSIZER"
        Me.CMBSIZER.Size = New System.Drawing.Size(200, 23)
        Me.CMBSIZER.TabIndex = 0
        '
        'GRIDSTOCK
        '
        Me.GRIDSTOCK.AllowUserToAddRows = False
        Me.GRIDSTOCK.AllowUserToDeleteRows = False
        Me.GRIDSTOCK.AllowUserToResizeColumns = False
        Me.GRIDSTOCK.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(248, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black
        Me.GRIDSTOCK.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.GRIDSTOCK.BackgroundColor = System.Drawing.Color.White
        Me.GRIDSTOCK.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GRIDSTOCK.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.GRIDSTOCK.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.GRIDSTOCK.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GRIDSTOCK.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GOPROLLSTOCKNOSIZER, Me.GSIZER, Me.GPROGRAMNO, Me.GQUALITY, Me.GMILL, Me.GENDS, Me.GROLLS, Me.GWT, Me.GREMARKS, Me.GOUTROLLS, Me.GOUTWT})
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDSTOCK.DefaultCellStyle = DataGridViewCellStyle6
        Me.GRIDSTOCK.GridColor = System.Drawing.SystemColors.Control
        Me.GRIDSTOCK.Location = New System.Drawing.Point(3, 28)
        Me.GRIDSTOCK.MultiSelect = False
        Me.GRIDSTOCK.Name = "GRIDSTOCK"
        Me.GRIDSTOCK.ReadOnly = True
        Me.GRIDSTOCK.RowHeadersVisible = False
        Me.GRIDSTOCK.RowHeadersWidth = 30
        Me.GRIDSTOCK.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Black
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White
        Me.GRIDSTOCK.RowsDefaultCellStyle = DataGridViewCellStyle7
        Me.GRIDSTOCK.RowTemplate.Height = 20
        Me.GRIDSTOCK.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDSTOCK.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.GRIDSTOCK.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.GRIDSTOCK.Size = New System.Drawing.Size(1067, 474)
        Me.GRIDSTOCK.TabIndex = 7
        Me.GRIDSTOCK.TabStop = False
        '
        'GOPROLLSTOCKNOSIZER
        '
        Me.GOPROLLSTOCKNOSIZER.HeaderText = "Sr."
        Me.GOPROLLSTOCKNOSIZER.Name = "GOPROLLSTOCKNOSIZER"
        Me.GOPROLLSTOCKNOSIZER.ReadOnly = True
        Me.GOPROLLSTOCKNOSIZER.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GOPROLLSTOCKNOSIZER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GOPROLLSTOCKNOSIZER.Visible = False
        Me.GOPROLLSTOCKNOSIZER.Width = 40
        '
        'GSIZER
        '
        Me.GSIZER.HeaderText = "Sizer"
        Me.GSIZER.Name = "GSIZER"
        Me.GSIZER.ReadOnly = True
        Me.GSIZER.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GSIZER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GSIZER.Width = 200
        '
        'GPROGRAMNO
        '
        Me.GPROGRAMNO.HeaderText = "Program No"
        Me.GPROGRAMNO.Name = "GPROGRAMNO"
        Me.GPROGRAMNO.ReadOnly = True
        Me.GPROGRAMNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GPROGRAMNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GPROGRAMNO.Width = 80
        '
        'GQUALITY
        '
        Me.GQUALITY.HeaderText = "Quality"
        Me.GQUALITY.Name = "GQUALITY"
        Me.GQUALITY.ReadOnly = True
        Me.GQUALITY.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GQUALITY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GQUALITY.Width = 120
        '
        'GMILL
        '
        Me.GMILL.HeaderText = "Mill Name"
        Me.GMILL.Name = "GMILL"
        Me.GMILL.ReadOnly = True
        Me.GMILL.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GMILL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GMILL.Width = 200
        '
        'GENDS
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GENDS.DefaultCellStyle = DataGridViewCellStyle3
        Me.GENDS.HeaderText = "ENDS"
        Me.GENDS.Name = "GENDS"
        Me.GENDS.ReadOnly = True
        Me.GENDS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GENDS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GENDS.Width = 80
        '
        'GROLLS
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GROLLS.DefaultCellStyle = DataGridViewCellStyle4
        Me.GROLLS.HeaderText = "Rolls"
        Me.GROLLS.Name = "GROLLS"
        Me.GROLLS.ReadOnly = True
        Me.GROLLS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GROLLS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GROLLS.Width = 80
        '
        'GWT
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GWT.DefaultCellStyle = DataGridViewCellStyle5
        Me.GWT.HeaderText = "Weight"
        Me.GWT.Name = "GWT"
        Me.GWT.ReadOnly = True
        Me.GWT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GWT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GWT.Width = 80
        '
        'GREMARKS
        '
        Me.GREMARKS.HeaderText = "Remarks"
        Me.GREMARKS.Name = "GREMARKS"
        Me.GREMARKS.ReadOnly = True
        Me.GREMARKS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GREMARKS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GREMARKS.Width = 200
        '
        'GOUTROLLS
        '
        Me.GOUTROLLS.HeaderText = "OUTROLLS"
        Me.GOUTROLLS.Name = "GOUTROLLS"
        Me.GOUTROLLS.ReadOnly = True
        Me.GOUTROLLS.Visible = False
        '
        'GOUTWT
        '
        Me.GOUTWT.HeaderText = "OUTWT"
        Me.GOUTWT.Name = "GOUTWT"
        Me.GOUTWT.ReadOnly = True
        Me.GOUTWT.Visible = False
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.FlatAppearance.BorderSize = 0
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.Black
        Me.cmdexit.Location = New System.Drawing.Point(468, 560)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 28)
        Me.cmdexit.TabIndex = 2
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'OpeningStockRollsSizer
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1096, 596)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "OpeningStockRollsSizer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Opening Stock Rolls With Sizer"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.GRIDSTOCK, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents TXTADD As System.Windows.Forms.TextBox
    Friend WithEvents cmbcode As System.Windows.Forms.ComboBox
    Friend WithEvents LBLTOTALWT As System.Windows.Forms.Label
    Friend WithEvents CMBSIZERFILTER As System.Windows.Forms.ComboBox
    Friend WithEvents LBLTOTALROLLS As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LBLTOTAL As System.Windows.Forms.Label
    Friend WithEvents TXTOPROLLSSTOCKNOSIZER As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents CMBMILL As System.Windows.Forms.ComboBox
    Friend WithEvents CMBQUALITY As System.Windows.Forms.ComboBox
    Friend WithEvents TXTREMARKS As System.Windows.Forms.TextBox
    Friend WithEvents TXTWT As System.Windows.Forms.TextBox
    Friend WithEvents TXTROLLS As System.Windows.Forms.TextBox
    Friend WithEvents TXTTOTALENDS As System.Windows.Forms.TextBox
    Friend WithEvents CMBSIZER As System.Windows.Forms.ComboBox
    Friend WithEvents GRIDSTOCK As System.Windows.Forms.DataGridView
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents TXTPROGRAMNO As System.Windows.Forms.TextBox
    Friend WithEvents GOPROLLSTOCKNOSIZER As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GSIZER As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GPROGRAMNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GQUALITY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GMILL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GENDS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GROLLS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GWT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GREMARKS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GOUTROLLS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GOUTWT As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
