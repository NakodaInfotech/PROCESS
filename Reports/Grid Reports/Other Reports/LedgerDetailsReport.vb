
Imports BL

Public Class LedgerDetailsReport

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub LedgerDetailsReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Or (e.KeyCode = Keys.X And e.Alt = True) Then
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid(ByVal WHERECLAUSE As String)
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable
            dt = objclsCMST.search("  ISNULL(LEDGERS.Acc_cmpname,'') AS CMPNAME, ISNULL(LEDGERS.Acc_name,'') AS NAME, ISNULL(LEDGERS.Acc_code,'') AS CODE, ISNULL(GROUPMASTER.group_name,'') AS GROUPNAME,ISNULL(GROUPMASTER.group_secondary, '') AS SECONDARY,ISNULL(LEDGERS.ACC_TYPE,'') AS TYPE, ISNULL(LEDGERS.Acc_opbal,0) AS OPBAL, ISNULL(LEDGERS.Acc_drcr,'') AS DRCR, ISNULL(AREAMASTER.area_name,'') AS AREA, ISNULL(CITYMASTER.city_name,'') AS CITY, ISNULL(STATEMASTER.state_name,'') AS STATE, ISNULL(COUNTRYMASTER.country_name,'') AS COUNTRY, ISNULL(LEDGERS.Acc_resino,'') AS RESINO, ISNULL(LEDGERS.Acc_altno,'') AS ALTNO, ISNULL(LEDGERS.Acc_phone,'') AS PHONENO, ISNULL(LEDGERS.Acc_mobile,'') AS MOBILE, ISNULL(LEDGERS.Acc_fax,'') AS FAX, ISNULL(LEDGERS.Acc_website,'') AS WEBSITE, ISNULL(LEDGERS.Acc_email,'') AS EMAIL, ISNULL(LEDGERS.Acc_panno,'') AS PANNO, ISNULL(LEDGERS.Acc_add,'') AS [ADD], ISNULL(LEDGERS.Acc_BOSSMOBILE,'') AS BOSSMOBILE, ISNULL(LEDGERS.Acc_GSTIN,'') AS GSTIN ", "", " LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id AND LEDGERS.Acc_cmpid = GROUPMASTER.group_cmpid AND LEDGERS.Acc_locationid = GROUPMASTER.group_locationid AND LEDGERS.Acc_yearid = GROUPMASTER.group_yearid LEFT OUTER JOIN CITYMASTER ON LEDGERS.Acc_yearid = CITYMASTER.city_yearid AND LEDGERS.Acc_locationid = CITYMASTER.city_locationid AND LEDGERS.Acc_cmpid = CITYMASTER.city_cmpid AND LEDGERS.Acc_cityid = CITYMASTER.city_id LEFT OUTER JOIN AREAMASTER ON LEDGERS.Acc_yearid = AREAMASTER.area_yearid AND LEDGERS.Acc_locationid = AREAMASTER.area_locationid AND LEDGERS.Acc_cmpid = AREAMASTER.area_cmpid AND LEDGERS.Acc_areaid = AREAMASTER.area_id LEFT OUTER JOIN STATEMASTER ON LEDGERS.Acc_yearid = STATEMASTER.state_yearid AND LEDGERS.Acc_locationid = STATEMASTER.state_locationid AND LEDGERS.Acc_cmpid = STATEMASTER.state_cmpid AND LEDGERS.Acc_stateid = STATEMASTER.state_id LEFT OUTER JOIN COUNTRYMASTER ON LEDGERS.Acc_yearid = COUNTRYMASTER.country_yearid AND LEDGERS.Acc_locationid = COUNTRYMASTER.country_locationid AND LEDGERS.Acc_cmpid = COUNTRYMASTER.country_cmpid AND LEDGERS.Acc_countryid = COUNTRYMASTER.country_id ", WHERECLAUSE & " AND ACC_CMPID =" & CmpId & " AND ACC_LOCATIONID = " & Locationid & " AND ACC_YEARID = " & YearId & " ORDER BY ACC_CMPNAME")
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then gridbill.FocusedRowHandle = gridbill.RowCount - 1
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub CMDPRINT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDPRINT.Click
        Try
            Dim PATH As String = "" = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\Ledger Details.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True

            Dim workbook As String = PATH
            If FileIO.FileSystem.FileExists(PATH) = True Then Interaction.GetObject(workbook).close(False)
            GC.Collect()

            Dim PERIOD As String = AccFrom & " - " & AccTo
            
            opti.SheetName = "Ledger Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Ledger Details", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LedgerDetailsReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            fillgrid("")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSAVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSAVE.Click
        Try
            fillgrid("")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class