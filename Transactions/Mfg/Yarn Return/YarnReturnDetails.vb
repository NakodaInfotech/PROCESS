Imports BL

Public Class YarnReturnDetails
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public FRMSTRING As String


    Private Sub CMDEXIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub YarnReturnDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Or (e.KeyCode = Keys.X And e.Alt = True) Then
                Me.Close()
            ElseIf e.KeyCode = Keys.N And e.Control = True Then
                showform(False, 0)
            ElseIf e.KeyCode = Keys.O And e.Alt = True Then
                CMDEDIT_Click(sender, e)
            ElseIf e.KeyCode = Keys.E And e.Alt = True Then
                TOOLEXCEL_Click(sender, e)
            ElseIf e.KeyCode = Keys.R And e.Alt = True Then
                TOOLREFRESH_Click(sender, e)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub YarnReturnDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'MFG'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            If FRMSTRING = "RETURNFROMWARPER" Then
                Me.Text = "Yarn Return From Warper Details"
            ElseIf FRMSTRING = "RETURNFROMADDA" Then
                Me.Text = "Yarn Return From Adda Details"
                GADDANO.Visible = True
            ElseIf FRMSTRING = "RETURNFROMSIZER" Then
                Me.Text = "Yarn Return From Sizer Details"
            ElseIf FRMSTRING = "RETURNFROMWEAVER" Then
                Me.Text = "Yarn Return From Weaver Details"
            ElseIf FRMSTRING = "RETURNFROMDYEING" Then
                Me.Text = "Yarn Return From Dyeing Details"
            ElseIf FRMSTRING = "RETURNFROMJOBBER" Then
                Me.Text = "Yarn Return From Jobber Details"
            ElseIf FRMSTRING = "RETURNFROMMACHINE" Then
                Me.Text = "Yarn Return From Machine Details"
            End If

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            fillgrid()


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid()

        Try
            If FRMSTRING = "RETURNFROMWARPER" Then

                Dim OBJRETURN As New ClsYarnReturnFromWarper
                OBJRETURN.alParaval.Add(0)
                OBJRETURN.alParaval.Add(YearId)
                Dim dttable As DataTable = OBJRETURN.selectYARNRETURN()
                gridbilldetails.DataSource = dttable
                If dttable.Rows.Count > 0 Then
                    gridbill.FocusedRowHandle = gridbill.RowCount - 1
                    gridbill.TopRowIndex = gridbill.RowCount - 15
                End If

            ElseIf FRMSTRING = "RETURNFROMADDA" Then

                Dim OBJRETURN As New ClsYarnReturnFromADDA
                OBJRETURN.alParaval.Add(0)
                OBJRETURN.alParaval.Add(YearId)
                Dim dttable As DataTable = OBJRETURN.selectYARNRETURN()
                gridbilldetails.DataSource = dttable
                If dttable.Rows.Count > 0 Then
                    gridbill.FocusedRowHandle = gridbill.RowCount - 1
                    gridbill.TopRowIndex = gridbill.RowCount - 15
                End If

            ElseIf FRMSTRING = "RETURNFROMSIZER" Then

                Dim OBJRETURN As New ClsYarnReturnFromSizer
                OBJRETURN.alParaval.Add(0)
                OBJRETURN.alParaval.Add(YearId)
                Dim dttable As DataTable = OBJRETURN.selectYARNRETURN()
                gridbilldetails.DataSource = dttable
                If dttable.Rows.Count > 0 Then
                    gridbill.FocusedRowHandle = gridbill.RowCount - 1
                    gridbill.TopRowIndex = gridbill.RowCount - 15
                End If

            ElseIf FRMSTRING = "RETURNFROMWEAVER" Then
                Dim OBJRETURN As New ClsYarnReturnFromWeaver
                OBJRETURN.alParaval.Add(0)
                OBJRETURN.alParaval.Add(YearId)
                Dim dttable As DataTable = OBJRETURN.selectYARNRETURN()
                gridbilldetails.DataSource = dttable
                If dttable.Rows.Count > 0 Then
                    gridbill.FocusedRowHandle = gridbill.RowCount - 1
                    gridbill.TopRowIndex = gridbill.RowCount - 15
                End If

            ElseIf FRMSTRING = "RETURNFROMDYEING" Then
                Dim OBJRETURN As New ClsYarnReturnFromDyeing
                OBJRETURN.alParaval.Add(0)
                OBJRETURN.alParaval.Add(YearId)
                Dim dttable As DataTable = OBJRETURN.selectYARNRETURN()
                gridbilldetails.DataSource = dttable
                If dttable.Rows.Count > 0 Then
                    gridbill.FocusedRowHandle = gridbill.RowCount - 1
                    gridbill.TopRowIndex = gridbill.RowCount - 15
                End If

            ElseIf FRMSTRING = "RETURNFROMJOBBER" Then
                Dim OBJRETURN As New ClsYarnReturnFromJobber
                OBJRETURN.alParaval.Add(0)
                OBJRETURN.alParaval.Add(YearId)
                Dim dttable As DataTable = OBJRETURN.selectYARNRETURN()
                gridbilldetails.DataSource = dttable
                If dttable.Rows.Count > 0 Then
                    gridbill.FocusedRowHandle = gridbill.RowCount - 1
                    gridbill.TopRowIndex = gridbill.RowCount - 15
                End If

            ElseIf FRMSTRING = "RETURNFROMMACHINE" Then
                Dim OBJRETURN As New ClsYarnReturnFromMachine
                OBJRETURN.alParaval.Add(0)
                OBJRETURN.alParaval.Add(YearId)
                Dim dttable As DataTable = OBJRETURN.selectYARNRETURN()
                gridbilldetails.DataSource = dttable
                If dttable.Rows.Count > 0 Then
                    gridbill.FocusedRowHandle = gridbill.RowCount - 1
                    gridbill.TopRowIndex = gridbill.RowCount - 15
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try

       
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal NO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim objDO As New YarnReturn
                objDO.MdiParent = MDIMain
                objDO.EDIT = editval
                objDO.TEMPRETURNNO = NO
                objDO.FRMSTRING = FRMSTRING
                objDO.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub CMDADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDADD.Click
        Try
            If USERADD = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            showform(False, 0)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub gridbill_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("NO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEDIT.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("NO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLEXCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLEXCEL.Click
        Try
            If FRMSTRING = "RETURNFROMWARPER" Then
                Dim PATH As String = Application.StartupPath & "\Yarn Return From Warper Details.XLS"
                Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
                opti.ShowGridLines = True

                For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                    proc.Kill()
                Next
                opti.SheetName = "Yarn Return From Warper Details"
                gridbill.ExportToXls(PATH, opti)
                EXCELCMPHEADER(PATH, "Yarn Return From Warper Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)

            ElseIf FRMSTRING = "RETURNFROMADDA" Then
                Dim PATH As String = Application.StartupPath & "\Yarn Return From ADDA Details.XLS"
                Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
                opti.ShowGridLines = True

                For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                    proc.Kill()
                Next
                opti.SheetName = "Yarn Return From ADDA Details"
                gridbill.ExportToXls(PATH, opti)
                EXCELCMPHEADER(PATH, "Yarn Return From ADDA Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)

            ElseIf FRMSTRING = "RETURNFROMSIZER" Then
                Dim PATH As String = Application.StartupPath & "\Yarn Return From Sizer Details.XLS"
                Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
                opti.ShowGridLines = True

                For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                    proc.Kill()
                Next
                opti.SheetName = "Yarn Return From Sizer Details"
                gridbill.ExportToXls(PATH, opti)
                EXCELCMPHEADER(PATH, "Yarn IReturn From Sizer Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)

            ElseIf FRMSTRING = "RETURNFROMWEAVER" Then

                Dim PATH As String = Application.StartupPath & "\Yarn Return From Weaver Details.XLS"
                Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
                opti.ShowGridLines = True

                For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                    proc.Kill()
                Next
                opti.SheetName = "Yarn Return From Weaver Details"
                gridbill.ExportToXls(PATH, opti)
                EXCELCMPHEADER(PATH, "Yarn Return From Weaver Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)

            ElseIf FRMSTRING = "RETURNFROMDYEING" Then

                Dim PATH As String = Application.StartupPath & "\Yarn Return From Dyeing Details.XLS"
                Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
                opti.ShowGridLines = True

                For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                    proc.Kill()
                Next
                opti.SheetName = "Yarn Return From Dyeing Details"
                gridbill.ExportToXls(PATH, opti)
                EXCELCMPHEADER(PATH, "Yarn Return From Dyeing Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)

            ElseIf FRMSTRING = "RETURNFROMJOBBER" Then

                Dim PATH As String = Application.StartupPath & "\Yarn Return From Jobber Details.XLS"
                Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
                opti.ShowGridLines = True

                For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                    proc.Kill()
                Next
                opti.SheetName = "Yarn Return From Jobber Details"
                gridbill.ExportToXls(PATH, opti)
                EXCELCMPHEADER(PATH, "Yarn Return From Jobber Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)

            ElseIf FRMSTRING = "RETURNFROMMACHINE" Then

                Dim PATH As String = Application.StartupPath & "\Yarn Return From Machine Details.XLS"
                Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
                opti.ShowGridLines = True

                For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                    proc.Kill()
                Next
                opti.SheetName = "Yarn Return From Machine Details"
                gridbill.ExportToXls(PATH, opti)
                EXCELCMPHEADER(PATH, "Yarn Return From Machine Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)



            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLREFRESH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLREFRESH.Click
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class