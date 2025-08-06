
Imports BL
Imports DevExpress.XtraEditors.Controls

Public Class LockPendingBeams

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public DT As New DataTable

    Private Sub LockPendingBeams_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.KeyCode = Keys.OemQuotes Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub LockPendingBeams_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim WHERECLAUSE As String = ""
            If RBPENDING.Checked = True Then WHERECLAUSE = " AND T.DONE = 'FALSE'" Else WHERECLAUSE = " AND T.DONE = 'TRUE'"

            Dim OBJCMN As New ClsCommon
            Dim dt As DataTable = OBJCMN.search("*", "", "  (SELECT BEAMISSUETOWEAVER.BEAMISSUE_NO AS SRNO, BEAMISSUETOWEAVER_DESC.BEAMISSUE_GRIDSRNO AS GRIDSRNO, 'BEAMISSUE' AS TYPE, BEAMISSUETOWEAVER.BEAMISSUE_DATE AS DATE, LEDGERS.Acc_cmpname AS NAME, BEAMMASTER.BEAM_NAME AS BEAMNAME, BEAMISSUETOWEAVER_DESC.BEAMISSUE_BEAMNO AS BEAMNO, BEAMISSUETOWEAVER_DESC.BEAMISSUE_CUT AS CUTS, BEAMISSUETOWEAVER_DESC.BEAMISSUE_WT AS WT, ISNULL(SIZERLEDGERS.Acc_cmpname,'') AS SIZERNAME, BEAMISSUETOWEAVER_DESC.BEAMISSUE_DONE AS DONE, (BEAMISSUETOWEAVER_DESC.BEAMISSUE_CUT - BEAMISSUETOWEAVER_DESC.BEAMISSUE_OUTCUT) AS BALANCECUT, ISNULL(BEAMISSUETOWEAVER_DESC.BEAMISSUE_LOOMNO,0) AS LOOMNO FROM LEDGERS INNER JOIN BEAMISSUETOWEAVER ON LEDGERS.Acc_id = BEAMISSUETOWEAVER.BEAMISSUE_LEDGERID INNER JOIN BEAMISSUETOWEAVER_DESC ON BEAMISSUETOWEAVER.BEAMISSUE_NO = BEAMISSUETOWEAVER_DESC.BEAMISSUE_NO AND BEAMISSUETOWEAVER.BEAMISSUE_YEARID = BEAMISSUETOWEAVER_DESC.BEAMISSUE_YEARID INNER JOIN BEAMMASTER ON BEAMISSUETOWEAVER_DESC.BEAMISSUE_BEAMID = BEAMMASTER.BEAM_ID LEFT OUTER JOIN LEDGERS AS SIZERLEDGERS ON BEAMISSUETOWEAVER_DESC.BEAMISSUE_SIZERID = SIZERLEDGERS.Acc_id WHERE (BEAMISSUETOWEAVER_DESC.BEAMISSUE_CUT - BEAMISSUETOWEAVER_DESC.BEAMISSUE_OUTCUT > 0) AND  BEAMISSUETOWEAVER_DESC.BEAMISSUE_YEARID = " & YearId & " UNION ALL SELECT STOCKMASTER_BEAMWEAVER.SMBEAMWEAVER_NO AS SRNO, 0 AS GRIDSRNO, 'OPENING' AS TYPE, YEARMASTER.year_startdate AS DATE, LEDGERS.Acc_cmpname AS WEAVERNAME,  BEAMMASTER.BEAM_NAME AS BEAMNAME, STOCKMASTER_BEAMWEAVER.SMBEAMWEAVER_BEAMNO AS BEAMNO, STOCKMASTER_BEAMWEAVER.SMBEAMWEAVER_CUT AS CUTS, STOCKMASTER_BEAMWEAVER.SMBEAMWEAVER_WT AS WT, '' AS SIZERNAME, STOCKMASTER_BEAMWEAVER.SMBEAMWEAVER_DONE AS DONE, (STOCKMASTER_BEAMWEAVER.SMBEAMWEAVER_CUT - STOCKMASTER_BEAMWEAVER.SMBEAMWEAVER_OUTCUT) AS BALANCECUT, ISNULL(SMBEAMWEAVER_LOOMNO,0) AS LOOMNO FROM LEDGERS INNER JOIN STOCKMASTER_BEAMWEAVER ON LEDGERS.Acc_id = STOCKMASTER_BEAMWEAVER.SMBEAMWEAVER_WEAVERID INNER JOIN BEAMMASTER ON STOCKMASTER_BEAMWEAVER.SMBEAMWEAVER_BEAMID = BEAMMASTER.BEAM_ID INNER JOIN YEARMASTER ON STOCKMASTER_BEAMWEAVER.SMBEAMWEAVER_YEARID = YEARMASTER.year_id WHERE (STOCKMASTER_BEAMWEAVER.SMBEAMWEAVER_CUT - STOCKMASTER_BEAMWEAVER.SMBEAMWEAVER_OUTCUT > 0)  AND  SMBEAMWEAVER_YEARID = " & YearId & " ) AS T", WHERECLAUSE)
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        Me.Close()
    End Sub

    Private Sub CMDOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        Try
            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("DONE")) = True Then
                    Dim OBJCMN As New ClsCommon
                    If dtrow("TYPE") = "BEAMISSUE" Then DT = OBJCMN.Execute_Any_String("UPDATE BEAMISSUETOWEAVER_DESC SET BEAMISSUETOWEAVER_DESC.BEAMISSUE_DONE = 'TRUE' WHERE BEAMISSUETOWEAVER_DESC.BEAMISSUE_NO = " & dtrow("SRNO") & " AND BEAMISSUETOWEAVER_DESC.BEAMISSUE_GRIDSRNO = " & dtrow("GRIDSRNO") & " AND BEAMISSUETOWEAVER_DESC.BEAMISSUE_YEARID = " & YearId, "", "") Else DT = OBJCMN.Execute_Any_String("UPDATE STOCKMASTER_BEAMWEAVER SET STOCKMASTER_BEAMWEAVER.SMBEAMWEAVER_DONE = 'TRUE' WHERE STOCKMASTER_BEAMWEAVER.SMBEAMWEAVER_NO = " & dtrow("SRNO") & " AND STOCKMASTER_BEAMWEAVER.SMBEAMWEAVER_YEARID = " & YearId, "", "")
                End If
            Next
            fillgrid()
            gridbill.Focus()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub gridbill_InvalidRowException(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs) Handles gridbill.InvalidRowException
        e.ExceptionMode = ExceptionMode.NoAction
    End Sub

    Private Sub gridbill_ValidateRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs) Handles gridbill.ValidateRow
        Try
            If (Convert.ToBoolean(gridbill.GetRowCellValue(e.RowHandle, "DONE")) = True And RBPENDING.Checked = True) Then
                If MsgBox("Save Entry?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then Call CMDOK_Click(sender, e)
            End If
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

    Private Sub CMDDELETE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDDELETE.Click
        Try

            Dim ROW As DataRow = gridbill.GetFocusedDataRow
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable

            If MsgBox("Delete Data?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            If ROW("TYPE") = "BEAMISSUE" Then DT = OBJCMN.Execute_Any_String("UPDATE BEAMISSUETOWEAVER_DESC SET BEAMISSUETOWEAVER_DESC.BEAMISSUE_DONE = 'FALSE' WHERE BEAMISSUETOWEAVER_DESC.BEAMISSUE_NO = " & ROW("SRNO") & " AND BEAMISSUETOWEAVER_DESC.BEAMISSUE_GRIDSRNO = " & ROW("GRIDSRNO") & " AND BEAMISSUETOWEAVER_DESC.BEAMISSUE_YEARID = " & YearId, "", "") Else DT = OBJCMN.Execute_Any_String("UPDATE STOCKMASTER_BEAMWEAVER SET STOCKMASTER_BEAMWEAVER.SMBEAMWEAVER_DONE = 'FALSE' WHERE STOCKMASTER_BEAMWEAVER.SMBEAMWEAVER_NO = " & ROW("SRNO") & " AND STOCKMASTER_BEAMWEAVER.SMBEAMWEAVER_YEARID = " & YearId, "", "")
            fillgrid()
            gridbill.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class