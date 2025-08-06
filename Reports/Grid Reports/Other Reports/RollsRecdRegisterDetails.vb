Imports BL

Public Class RollsRecdRegisterDetails
    Public WHERECLAUSE As String
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT


    Private Sub ExcelExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExcelExport.Click
        Try
            Dim PATH As String = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "Rolls Received Register Details.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True

            For Each Proc As System.Diagnostics.Process In System.Diagnostics.Process.GetProcessesByName("Excel")
                Proc.Kill()
            Next

            Dim PERIOD As String = AccFrom & " - " & AccTo

            opti.SheetName = "Rolls Received Register Details"
            gridbilldetails.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Rolls Received Register Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RollsRecvRegisterDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid()
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Dim OBJCMN As New ClsCommonMaster
            Dim dt As DataTable = OBJCMN.search(" ISNULL(ROLLRECEIVED.ROLLRECD_NO, 0) AS SRNO, ISNULL(ROLLRECEIVED.ROLLRECD_CHALLANNO, '0') AS CHALLANNO, ROLLRECEIVED.ROLLRECD_DATE AS DATE, ISNULL(MILLLEDGERS.Acc_cmpname, '') AS MILLNAME, ISNULL(ROLLRECEIVED.ROLLRECD_FRESH, '') AS FRESH, ISNULL(ROLLRECEIVED.ROLLRECD_FRESHWT, 0) AS FRESHWT, ISNULL(ROLLRECEIVED.ROLLRECD_WINDING, '') AS WINDING, ISNULL(ROLLRECEIVED.ROLLRECD_WINDINGWT, 0) AS WINDINGWT, ISNULL(ROLLRECEIVED.ROLLRECD_FIRKA, '') AS FIRKA, ISNULL(ROLLRECEIVED.ROLLRECD_FIRKAWT, 0) AS FIRKAWT, ISNULL(ROLLRECEIVED.ROLLRECD_TOTALWT, 0) AS ROLLWT, ISNULL(ROLLRECEIVED.ROLLRECD_LENGTH, 0) AS LENGTH, ISNULL(ROLLRECEIVED.ROLLRECD_TOTALENDS, 0) AS TOTALENDS, ISNULL(ROLLRECEIVED.ROLLRECD_TAPLINE, 0) AS TL, ISNULL(ROLLRECEIVED.ROLLRECD_CUT, 0) AS CUT, 0 AS CUTWEIGHT, ISNULL(ROLLRECEIVED.ROLLRECD_RETFRESHWT + ROLLRECEIVED.ROLLRECD_RETWINDINGWT + ROLLRECEIVED.ROLLRECD_RETFIRKAWT, 0) AS RETURNWT, ISNULL(ROLLRECEIVED.ROLLRECD_TOTALGROSSWT, 0) AS TOTALWEIGHT, 0 AS LONGATION, ROUND(ISNULL(ROLLRECEIVED.ROLLRECD_COUNT, 0), 2) AS COUNT, ISNULL(WARPERLEDGERS.Acc_cmpname, '') AS WARPER, ISNULL(QUALITYMASTER.QUALITY_NAME, '') AS QUALITY, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN ", " ", "  LEDGERS AS MILLLEDGERS INNER JOIN ROLLRECEIVED_DESC ON MILLLEDGERS.Acc_id = ROLLRECEIVED_DESC.ROLLRECD_MILLID INNER JOIN ROLLRECEIVED ON ROLLRECEIVED_DESC.ROLLRECD_NO = ROLLRECEIVED.ROLLRECD_NO AND ROLLRECEIVED_DESC.ROLLRECD_YEARID = ROLLRECEIVED.ROLLRECD_YEARID INNER JOIN LEDGERS AS WARPERLEDGERS ON ROLLRECEIVED.ROLLRECD_WARPERID = WARPERLEDGERS.Acc_id INNER JOIN QUALITYMASTER ON ROLLRECEIVED_DESC.ROLLRECD_QUALITYID = QUALITYMASTER.QUALITY_ID AND ROLLRECEIVED_DESC.ROLLRECD_YEARID = QUALITYMASTER.QUALITY_YEARID INNER JOIN GODOWNMASTER ON ROLLRECEIVED.ROLLRECD_GODOWNID = GODOWNMASTER.GODOWN_ID AND ROLLRECEIVED.ROLLRECD_YEARID = GODOWNMASTER.GODOWN_YEARID ", WHERECLAUSE & " AND (ROLLRECEIVED.ROLLRECD_YEARID = '" & YearId & "') ORDER BY ROLLRECEIVED.ROLLRECD_NO ")
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RollsRecvRegisterDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'MFG'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDREFRESH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDREFRESH.Click
        Try
            fillgrid()
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
End Class