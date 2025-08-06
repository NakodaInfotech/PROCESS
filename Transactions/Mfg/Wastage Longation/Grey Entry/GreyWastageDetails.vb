
Imports BL

Public Class GreyWastageDetails

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public FRMSTRING As String

    Private Sub CMDEXIT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YarnISSUETOWARPERDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Or (e.KeyCode = Keys.X And e.Alt = True) Then
                Me.Close()
            ElseIf e.KeyCode = Keys.N And e.Control = True Then
                showform(False, 0)
            ElseIf e.KeyCode = Keys.O And e.Alt = True Then
                CMDEDIT_Click(sender, e)
            ElseIf e.KeyCode = Keys.P And e.Alt = True Then
                TOOLEXCEL_Click(sender, e)
            ElseIf e.KeyCode = Keys.R And e.Alt = True Then
                TOOLREFRESH_Click(sender, e)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub YarnISSUETOWARPERDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

    Sub fillgrid()
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As New DataTable
            dt = objclsCMST.search(" ISNULL(GREYWASTAGE.GWASTAGE_NO, 0) AS SRNO, GREYWASTAGE.GWASTAGE_DATE AS DATE, ISNULL(GREYWASTAGE.GWASTAGE_REMARKS, '') AS REMARKS, ISNULL(GREYWASTAGE_DESC.GWASTAGE_GRIDSRNO, 0) AS SRNO, ISNULL(GREYWASTAGE_DESC.GWASTAGE_TYPE, '') AS TYPE, ISNULL(GREYWASTAGE_DESC.GWASTAGE_PCS, 0) AS PCS, ISNULL(GREYWASTAGE_DESC.GWASTAGE_MTRS, 0) AS MTRS, ISNULL(GREYWASTAGE_DESC.GWASTAGE_NARRATION, '') AS NARRATION, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN, ISNULL(GREYWASTAGE.GWASTAGE_TOTALPCS, 0) AS TOTALPCS, ISNULL(GREYWASTAGE.GWASTAGE_TOTALMTRS, 0) AS TOTALMTRS, ISNULL(GREYQUALITYMASTER.GREY_NAME, '') AS QUALITY, ISNULL(GREYWASTAGE_DESC.GWASTAGE_TAKANO, 0) AS TAKANO ", "", " GREYWASTAGE INNER JOIN GREYWASTAGE_DESC ON GREYWASTAGE.GWASTAGE_NO = GREYWASTAGE_DESC.GWASTAGE_NO AND GREYWASTAGE.GWASTAGE_YEARID = GREYWASTAGE_DESC.GWASTAGE_YEARID INNER JOIN GREYQUALITYMASTER ON GREYWASTAGE_DESC.GWASTAGE_GREYID = GREYQUALITYMASTER.GREY_ID INNER JOIN GODOWNMASTER ON GREYWASTAGE.GWASTAGE_GODOWNID = GODOWNMASTER.GODOWN_ID ", " AND GREYWASTAGE.GWASTAGE_YEARID = " & YearId & " ORDER BY GREYWASTAGE.GWASTAGE_NO, GREYWASTAGE_DESC.GWASTAGE_GRIDSRNO ")
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal WASTAGENO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim objDO As New GreyWastage
                objDO.MdiParent = MDIMain
                objDO.EDIT = editval
                objDO.TEMPGWASTAGENO = WASTAGENO
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

    Private Sub gridbilldetails_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbilldetails.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEDIT.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLEXCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLEXCEL.Click
        Try
            Dim PATH As String = Application.StartupPath & "\Grey Wastage Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True

            For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                proc.Kill()
            Next
            opti.SheetName = "Grey Wastage Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Grey Wastage Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
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