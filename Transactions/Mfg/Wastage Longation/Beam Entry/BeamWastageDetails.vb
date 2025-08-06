
Imports BL

Public Class BeamWastageDetails

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public FRMSTRING As String = ""

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
            If FRMSTRING = "WASTAGEGODOWN" Then
                dt = objclsCMST.search("  ISNULL(BEAMWASTAGEGODOWN.BWASGODOWN_DATE, GETDATE()) AS DATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN, ISNULL(BEAMMASTER.BEAM_NAME, '') AS BEAMNAME,  ISNULL(BEAMWASTAGEGODOWN_DESC.BWASGODOWN_TYPE, '') AS TYPE, ISNULL(BEAMWASTAGEGODOWN_DESC.BWASGODOWN_BEAMNO, '') AS BEAMNO, ISNULL(BEAMWASTAGEGODOWN.BWASGODOWN_NO, 0) AS SRNO, ISNULL(BEAMWASTAGEGODOWN_DESC.BWASGODOWN_CUT, 0) AS CUT ", " ", "  BEAMWASTAGEGODOWN INNER JOIN BEAMWASTAGEGODOWN_DESC ON BEAMWASTAGEGODOWN.BWASGODOWN_NO = BEAMWASTAGEGODOWN_DESC.BWASGODOWN_NO AND  BEAMWASTAGEGODOWN.BWASGODOWN_YEARID = BEAMWASTAGEGODOWN_DESC.BWASGODOWN_YEARID INNER JOIN LEDGERS ON BEAMWASTAGEGODOWN.BWASGODOWN_LEDGERID = LEDGERS.Acc_id INNER JOIN GODOWNMASTER ON BEAMWASTAGEGODOWN.BWASGODOWN_GODOWNID = GODOWNMASTER.GODOWN_ID INNER JOIN BEAMMASTER ON BEAMWASTAGEGODOWN_DESC.BWASGODOWN_BEAMID = BEAMMASTER.BEAM_ID ", " AND dbo.BEAMWASTAGEGODOWN.BWASGODOWN_YEARID =" & YearId & " ORDER by dbo.BEAMWASTAGEGODOWN.BWASGODOWN_NO,BEAMWASTAGEGODOWN_DESC.BWASGODOWN_GRIDSRNO ")
            ElseIf FRMSTRING = "WASTAGEWEAVER" Then
                dt = objclsCMST.search("  ISNULL(BEAMWASTAGEWEAVER.BWASWEAVER_NO, 0) AS SRNO, ISNULL(BEAMWASTAGEWEAVER.BWASWEAVER_DATE, GETDATE()) AS DATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN,  ISNULL(BEAMWASTAGEWEAVER_DESC.BWASWEAVER_CUT, 0) AS CUT, ISNULL(BEAMMASTER.BEAM_NAME, '') AS BEAMNAME,  ISNULL(BEAMWASTAGEWEAVER_DESC.BWASWEAVER_BEAMNO, '') AS BEAMNO, ISNULL(BEAMWASTAGEWEAVER_DESC.BWASWEAVER_TYPE, '') AS TYPE ", "", " BEAMWASTAGEWEAVER INNER JOIN BEAMWASTAGEWEAVER_DESC ON BEAMWASTAGEWEAVER.BWASWEAVER_NO = BEAMWASTAGEWEAVER_DESC.BWASWEAVER_NO AND  BEAMWASTAGEWEAVER.BWASWEAVER_YEARID = BEAMWASTAGEWEAVER_DESC.BWASWEAVER_YEARID INNER JOIN LEDGERS ON BEAMWASTAGEWEAVER.BWASWEAVER_LEDGERID = LEDGERS.Acc_id INNER JOIN BEAMMASTER ON BEAMWASTAGEWEAVER_DESC.BWASWEAVER_BEAMID = BEAMMASTER.BEAM_ID LEFT OUTER JOIN  GODOWNMASTER ON BEAMWASTAGEWEAVER.BWASWEAVER_GODOWNID = GODOWNMASTER.GODOWN_ID ", "  AND dbo.BEAMWASTAGEWEAVER.BWASWEAVER_YEARID =" & YearId & " ORDER by dbo.BEAMWASTAGEWEAVER.BWASWEAVER_NO,BEAMWASTAGEWEAVER_DESC.BWASWEAVER_GRIDSRNO ")
            ElseIf FRMSTRING = "WASTAGESIZER" Then
                dt = objclsCMST.search("  ISNULL(BEAMWASTAGESIZER.BWASSIZER_NO, 0) AS SRNO, ISNULL(BEAMWASTAGESIZER.BWASSIZER_DATE, GETDATE()) AS DATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN,  ISNULL(BEAMWASTAGESIZER_DESC.BWASSIZER_CUT, 0) AS CUT, ISNULL(BEAMMASTER.BEAM_NAME, '') AS BEAMNAME,  ISNULL(BEAMWASTAGESIZER_DESC.BWASSIZER_BEAMNO, '') AS BEAMNO, ISNULL(BEAMWASTAGESIZER_DESC.BWASSIZER_TYPE, '') AS TYPE ", "", " BEAMWASTAGESIZER INNER JOIN BEAMWASTAGESIZER_DESC ON BEAMWASTAGESIZER.BWASSIZER_NO = BEAMWASTAGESIZER_DESC.BWASSIZER_NO AND  BEAMWASTAGESIZER.BWASSIZER_YEARID = BEAMWASTAGESIZER_DESC.BWASSIZER_YEARID INNER JOIN LEDGERS ON BEAMWASTAGESIZER.BWASSIZER_LEDGERID = LEDGERS.Acc_id INNER JOIN BEAMMASTER ON BEAMWASTAGESIZER_DESC.BWASSIZER_BEAMID = BEAMMASTER.BEAM_ID LEFT OUTER JOIN  GODOWNMASTER ON BEAMWASTAGESIZER.BWASSIZER_GODOWNID = GODOWNMASTER.GODOWN_ID ", "  AND dbo.BEAMWASTAGESIZER.BWASSIZER_YEARID =" & YearId & " ORDER by dbo.BEAMWASTAGESIZER.BWASSIZER_NO,BEAMWASTAGESIZER_DESC.BWASSIZER_GRIDSRNO ")
            End If
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
                Dim objDO As New BeamWastage
                objDO.MdiParent = MDIMain
                objDO.EDIT = editval
                objDO.TEMPWASTAGENO = WASTAGENO
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
            Dim PATH As String = Application.StartupPath & "\Beam Wastage Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True

            For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                proc.Kill()
            Next
            opti.SheetName = "Beam Wastage Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Beam Wastage Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
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