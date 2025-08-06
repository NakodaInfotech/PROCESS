Imports BL
Imports DevExpress.XtraGrid.Views.Grid

Public Class JobInDetails
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub CMDEXIT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub JobInDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.Alt = True And e.KeyCode = Keys.R Then
            Call TOOLREFRESH_Click(sender, e)
        ElseIf e.Alt = True And e.KeyCode = Keys.P Then
            Call TOOLEXCEL_Click(sender, e)
        ElseIf e.KeyCode = Keys.OemQuotes Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub JobInDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'MFG'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal EDITVAL As Boolean, ByVal JOBNO As Integer)
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Dim OBJJOBOUT As New JobIn
            OBJJOBOUT.EDIT = EDITVAL
            OBJJOBOUT.MdiParent = MDIMain
            OBJJOBOUT.TEMPJOBNO = JOBNO
            OBJJOBOUT.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Sub fillgrid()
    '    Try
    '        Dim OBJJOBOUT As New ClsJobIn
    '        OBJJOBOUT.alParaval.Add(0)
    '        OBJJOBOUT.alParaval.Add(YearId)
    '        Dim dttable As DataTable = OBJJOBOUT.selectJOBIN()
    '        gridbilldetails.DataSource = dttable
    '        If dttable.Rows.Count > 0 Then
    '            gridbill.FocusedRowHandle = gridbill.RowCount - 1
    '            gridbill.TopRowIndex = gridbill.RowCount - 15
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Sub fillgrid()
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As New DataTable
            'dt = objclsCMST.search(" JOBIN.JI_NO AS JOBNO, JOBIN.JI_DATE AS DATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN,  ISNULL(QUALITYMASTER.QUALITY_NAME, '') AS QUALITY, ISNULL(JOBIN.JI_TOTALWT, 0) AS TOTALWT, ISNULL(JOBIN.JI_TOTALWINDING, 0) AS TOTALWINDING,  ISNULL(JOBIN.JI_TOTALFIRKA, 0) AS TOTALFIRKA, ISNULL(JOBIN.JI_TOTALNALI, 0) AS TOTALNALI, ISNULL(JOBIN.JI_REMARKS, '') AS REMARKS", "", " JOBIN INNER JOIN JOBIN_DESC ON JOBIN.JI_NO = JOBIN_DESC.JI_NO AND JOBIN.JI_YEARID = JOBIN_DESC.JI_YEARID INNER JOIN GODOWNMASTER ON JOBIN.JI_GODOWNID = GODOWNMASTER.GODOWN_ID INNER JOIN QUALITYMASTER ON JOBIN_DESC.JI_QUALITYID = QUALITYMASTER.QUALITY_ID INNER JOIN LEDGERS ON JOBIN.JI_LEDGERID = LEDGERS.Acc_id", "AND JOBIN.JI_YEARID=" & YearId)
            dt = objclsCMST.search(" JOBIN.JI_NO AS JOBNO, JOBIN.JI_DATE AS DATE, CASE WHEN JI_NAMETYPE = 'SELF' THEN ISNULL(GODOWNMASTER.GODOWN_NAME, '') ELSE ISNULL(NAMELEDGERS.ACC_CMPNAME, '') END AS NAME,  ISNULL(QUALITYMASTER.QUALITY_NAME, '') AS QUALITY, ISNULL(JOBIN.JI_TOTALWT, 0) AS TOTALWT, ISNULL(JOBIN.JI_TOTALWINDING, 0) AS TOTALWINDING,  ISNULL(JOBIN.JI_TOTALFIRKA, 0) AS TOTALFIRKA, ISNULL(JOBIN.JI_TOTALNALI, 0) AS TOTALNALI, ISNULL(JOBIN.JI_REMARKS, '') AS REMARKS", "", " JOBIN INNER JOIN JOBIN_DESC ON JOBIN.JI_NO = JOBIN_DESC.JI_NO AND JOBIN.JI_YEARID = JOBIN_DESC.JI_YEARID LEFT OUTER JOIN LEDGERS AS NAMELEDGERS ON JOBIN.JI_NAMEID = NAMELEDGERS.Acc_id  LEFT OUTER JOIN GODOWNMASTER ON JOBIN.JI_NAMEID = GODOWNMASTER.GODOWN_ID INNER JOIN QUALITYMASTER ON JOBIN_DESC.JI_QUALITYID = QUALITYMASTER.QUALITY_ID ", "AND JOBIN.JI_YEARID=" & YearId)
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDEDIT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMDEDIT.Click
        Try
            If USEREDIT = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            showform(True, gridbill.GetFocusedRowCellValue("JOBNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
        Try
            If USEREDIT = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            showform(True, gridbill.GetFocusedRowCellValue("JOBNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDADD_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMDADD.Click
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

    Private Sub TOOLREFRESH_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TOOLREFRESH.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLEXCEL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TOOLEXCEL.Click
        Try
            Dim PATH As String = "" = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\Job In Details.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True

            Dim workbook As String = PATH
            If FileIO.FileSystem.FileExists(PATH) = True Then Interaction.GetObject(workbook).close(False)
            GC.Collect()

            Dim PERIOD As String = AccFrom & " - " & AccTo

            opti.SheetName = "Job In Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Job In Details", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class