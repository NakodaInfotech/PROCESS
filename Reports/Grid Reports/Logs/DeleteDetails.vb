
Imports BL
Imports System.IO

Public Class DeleteDetails
    Public TEMPID As Integer

    Sub FILLGRID()
        Try
            Dim Objcls As New ClsCommonMaster
            Dim dt As DataTable
            dt = Objcls.search("   DELETE_LOGS.DELETE_ID AS ID,DELETE_LOGS.DELETE_TABLE AS [TABLE], DELETE_LOGS.DELETE_REMARKS + ' ' + cast(DELETE_LOGS.DELETE_DATE as varchar(100)) AS REMARKS, USERMASTER.User_Name AS [USER], CMPMASTER.cmp_name AS CMPNAME,DELETE_LOGS.DELETE_DATE as DATE ", " ", "  DELETE_LOGS INNER JOIN CMPMASTER ON DELETE_LOGS.DELETE_CMPID = CMPMASTER.cmp_id INNER JOIN USERMASTER ON DELETE_LOGS.DELETE_USERID = USERMASTER.User_id ", " and DELETE_LOGS.DELETE_CMPID = " & CmpId & " AND DELETE_LOGS.DELETE_YEARID = " & YearId)
            griddetails.DataSource = dt

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDEXIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub DeleteDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ExcelExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExcelExport.Click
        Try
            Dim PATH As String = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "Delete logs.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True

            For Each Proc As System.Diagnostics.Process In System.Diagnostics.Process.GetProcessesByName("Excel")
                Proc.Kill()
            Next

            Dim PERIOD As String = AccFrom & " - " & AccTo

            opti.SheetName = "Delete Details"
            griddetails.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Delete Details", gridpayment.VisibleColumns.Count + gridpayment.GroupCount)


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDREFRESH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDREFRESH.Click
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DeleteDetails_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Oemcomma Then
            e.SuppressKeyPress = True
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub gridpayment_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gridpayment.KeyDown
        Try
            Dim TEMPMSG As Integer = MsgBox("Delete Log? " & gridpayment.GetFocusedRowCellValue("TABLE"), MsgBoxStyle.YesNo)

            If TEMPMSG = vbYes Then
                Dim OBJOP As New ClSDELETE
                Dim ALPARAVAL As New ArrayList

                ALPARAVAL.Add(gridpayment.GetFocusedRowCellValue("ID").ToString)
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(YearId)
                ALPARAVAL.Add(Userid)

                OBJOP.alParaval = ALPARAVAL
                Dim INTRES As Integer = OBJOP.DELETE
                FILLGRID()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub fromdate_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fromdate.Validated
        Try
            If fromdate.Value.Date > Todate.Value.Date Then
                MsgBox("Enter valid date")
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Todate_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Todate.Validating
        Try
            If Todate.Value.Date < fromdate.Value.Date Then
                MsgBox("Enter valid date")
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDDELETE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDDELETE.Click
        If CHKDATE.Checked = True And fromdate.Value.Date <= Todate.Value.Date Then
            Dim TEMPMSG As Integer = MsgBox("Delete Logs? ", MsgBoxStyle.YesNo)

            If TEMPMSG = vbYes Then
                Dim OBJOP As New ClsDelete
                Dim ALPARAVAL As New ArrayList

                ALPARAVAL.Add(fromdate.Value.Date)
                ALPARAVAL.Add(Todate.Value.Date)

                OBJOP.alParaval = ALPARAVAL
                Dim INTRES As Integer = OBJOP.Delete_DATE
                FILLGRID()

            End If
        End If
    End Sub

    Private Sub CHKDATE_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHKDATE.CheckedChanged
        fromdate.Enabled = CHKDATE.CheckState
        Todate.Enabled = CHKDATE.CheckState
    End Sub
End Class