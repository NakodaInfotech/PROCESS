Imports System.Windows.Forms
Imports BL


Public Class UpdateDetails

    Sub FILLGRID()
        Try
            Dim Objcls As New ClsCommonMaster
            Dim dt As DataTable
            dt = Objcls.search(" UPDATE_LOGS.UPDATE_ID AS ID,UPDATE_LOGS.UPDATE_TABLE AS [TABLE], UPDATE_LOGS.UPDATE_DATE AS DATE, UPDATE_LOGS.UPDATE_REMARKS AS REMARKS, USERMASTER.User_Name AS [USER], CMPMASTER.cmp_name AS CMPNAME", " ", "  UPDATE_LOGS INNER JOIN USERMASTER ON UPDATE_LOGS.UPDATE_USERID = USERMASTER.User_id INNER JOIN CMPMASTER ON UPDATE_LOGS.UPDATE_CMPID = CMPMASTER.cmp_id", " and UPDATE_LOGS.UPDATE_CMPID = " & CmpId & " AND UPDATE_LOGS.UPDATE_YEARID = " & YearId)
            griddetails.DataSource = dt

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UpdateDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            fromdate.Value = Mydate
            Todate.Value = Mydate
            FILLGRID()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex

        End Try
    End Sub

    Private Sub UpdateDetails_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If (e.Alt = True And e.KeyCode = Windows.Forms.Keys.X) Or (e.KeyCode = Windows.Forms.Keys.Escape) Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Oemcomma Then
            e.SuppressKeyPress = True
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub ExcelExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExcelExport.Click
        Try
            Dim PATH As String = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "Update logs.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True

            For Each Proc As System.Diagnostics.Process In System.Diagnostics.Process.GetProcessesByName("Excel")
                Proc.Kill()
            Next

            Dim PERIOD As String = AccFrom & " - " & AccTo

            opti.SheetName = "Update Details"
            griddetails.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Update Details", gridpayment.VisibleColumns.Count + gridpayment.GroupCount)


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub griddetails_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles griddetails.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Delete Then
                Dim TEMPMSG As Integer = MsgBox("Delete Log " & gridpayment.GetFocusedRowCellValue("TABLE"), MsgBoxStyle.YesNo)

                If TEMPMSG = vbYes Then
                    Dim OBJOP As New Clsupdate
                    Dim ALPARAVAL As New ArrayList

                    ALPARAVAL.Add(gridpayment.GetFocusedRowCellValue("ID").ToString)
                    ALPARAVAL.Add(CmpId)
                    ALPARAVAL.Add(YearId)
                    ALPARAVAL.Add(Userid)

                    OBJOP.alParaval = ALPARAVAL
                    Dim INTRES As Integer = OBJOP.Delete
                    FILLGRID()

                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub fromdate_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fromdate.Validated
        Try
            If Todate.Value.Date < fromdate.Value.Date Then

                MsgBox("Invalid Check In Date")
                Exit Sub

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Todate_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Todate.Validated
        Try
            If fromdate.Value.Date > Todate.Value.Date Then

                MsgBox("Invalid date")
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex

        End Try
    End Sub

    Private Sub cmddel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddel.Click
        If fromdate.Value.Date <= Todate.Value.Date Then
            Dim TEMPMSG As Integer = MsgBox("Wish To Delete Logs From " & Format(fromdate.Value, "dd/MM/yyyy") & " to " & Format(Todate.Value, "dd/MM/yyyy") & " ", MsgBoxStyle.YesNo)

            If TEMPMSG = vbYes Then
                Dim OBJOP As New Clsupdate
                Dim ALPARAVAL As New ArrayList

                ALPARAVAL.Add(fromdate.Value.Date)
                ALPARAVAL.Add(Todate.Value.Date)

                OBJOP.alParaval = ALPARAVAL
                Dim INTRES As Integer = OBJOP.Delete_DATE
                FILLGRID()

            End If
        End If
    End Sub

End Class