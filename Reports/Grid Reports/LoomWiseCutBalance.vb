
Imports BL
Imports DevExpress.XtraEditors.Controls

Public Class LoomWiseCutBalance

    Public DT As New DataTable

    Private Sub LoomWiseCutBalance_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.KeyCode = Keys.OemQuotes Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub LoomWiseCutBalance_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid()
        Try
            'FETCH ALL THE BALANCECUT IN TEMPBALANCECUT TABLE 
            Dim OBJCMN As New ClsCommon
            Dim dt As DataTable = OBJCMN.Execute_Any_String("DELETE FROM TEMPBALANCECUT WHERE YEARID = " & YearId, "", "")

            Dim DTWEAVER As DataTable = OBJCMN.Execute_Any_String("SELECT DISTINCT BEAMUPLOAD_WEAVERID AS WEAVERID FROM BEAMUPLOAD WHERE BEAMUPLOAD_YEARID = " & YearId, "", "")
            For Each DTROWWEAVER As DataRow In DTWEAVER.Rows
                Dim DTBEAM As DataTable = OBJCMN.Execute_Any_String("SELECT LEDGERS.Acc_cmpname AS WEAVERNAME, BEAMUPLOAD.BEAMUPLOAD_LOOMNO AS LOOMNO, BEAMUPLOAD.BEAMUPLOAD_BEAMNO AS BEAMNO, BEAMUPLOAD.BEAMUPLOAD_FROMNO AS FROMNO, BEAMUPLOAD.BEAMUPLOAD_FROMSRNO AS FROMSRNO, BEAMUPLOAD.BEAMUPLOAD_TYPE AS TYPE FROM BEAMUPLOAD INNER JOIN LEDGERS ON BEAMUPLOAD.BEAMUPLOAD_WEAVERID = LEDGERS.Acc_id   WHERE ACC_ID = " & Val(DTROWWEAVER("WEAVERID")) & " AND BEAMUPLOAD_YEARID = " & YearId & " AND BEAMUPLOAD_GREYRECNO = (SELECT TOP 1 BEAMUPLOAD_GREYRECNO FROM BEAMUPLOAD INNER JOIN LEDGERS ON BEAMUPLOAD_WEAVERID = LEDGERS.ACC_ID  WHERE LEDGERS.ACC_ID = " & Val(DTROWWEAVER("WEAVERID")) & " AND BEAMUPLOAD_YEARID = " & YearId & " ORDER BY BEAMUPLOAD_GREYRECNO DESC) ORDER BY BEAMUPLOAD_LOOMNO ", "", "")
                For Each DTROWBEAM As DataRow In DTBEAM.Rows
                    Dim DTLOOM As DataTable = OBJCMN.Execute_Any_String(" SELECT T.BEAMNAME, T.TOTALCUT, T.BALANCECUT, T.SIZER, T.BEAMWT FROM (SELECT ISNULL(BEAMMASTER.BEAM_NAME,'') AS BEAMNAME, BEAMISSUETOWEAVER_DESC.BEAMISSUE_CUT as TOTALCUT, (BEAMISSUETOWEAVER_DESC.BEAMISSUE_CUT - BEAMISSUETOWEAVER_DESC.BEAMISSUE_OUTCUT) AS BALANCECUT, BEAMISSUETOWEAVER.BEAMISSUE_NO AS FROMNO, BEAMISSUETOWEAVER_DESC.BEAMISSUE_GRIDSRNO AS FROMSRNO, 'BEAMISSUE' AS TYPE, ISNULL(SIZERLEDGERS.Acc_cmpname,'') AS SIZER, BEAMISSUE_WT AS BEAMWT FROM  BEAMISSUETOWEAVER INNER JOIN BEAMISSUETOWEAVER_DESC ON BEAMISSUETOWEAVER.BEAMISSUE_NO = BEAMISSUETOWEAVER_DESC.BEAMISSUE_NO AND BEAMISSUETOWEAVER.BEAMISSUE_YEARID = BEAMISSUETOWEAVER_DESC.BEAMISSUE_YEARID INNER JOIN LEDGERS ON BEAMISSUETOWEAVER.BEAMISSUE_LEDGERID = LEDGERS.Acc_id INNER JOIN BEAMMASTER ON BEAMISSUETOWEAVER_DESC.BEAMISSUE_BEAMID = BEAMMASTER.BEAM_ID  LEFT OUTER JOIN LEDGERS AS SIZERLEDGERS ON BEAMISSUETOWEAVER_DESC.BEAMISSUE_SIZERID = SIZERLEDGERS.ACC_ID WHERE BEAMISSUETOWEAVER_DESC.BEAMISSUE_YEARID = " & YearId & " UNION ALL SELECT ISNULL(BEAMMASTER.BEAM_NAME,'') AS BEAMNAME, SMBEAMWEAVER_CUT as TOTALCUT, (SMBEAMWEAVER_CUT - SMBEAMWEAVER_OUTCUT) AS BALANCECUT, SMBEAMWEAVER_NO AS FROMNO, SMBEAMWEAVER_NO AS FROMSRNO, 'OPENING' AS TYPE, '' AS SIZER, SMBEAMWEAVER_WT AS BEAMWT FROM STOCKMASTER_BEAMWEAVER  INNER JOIN LEDGERS ON SMBEAMWEAVER_WEAVERID= LEDGERS.Acc_id INNER JOIN BEAMMASTER ON SMBEAMWEAVER_BEAMID = BEAMMASTER.BEAM_ID WHERE SMBEAMWEAVER_YEARID  = " & YearId & ") AS T WHERE T.FROMNO = " & Val(DTROWBEAM("FROMNO")) & " AND T.FROMSRNO = " & Val(DTROWBEAM("FROMSRNO")) & " AND T.TYPE = '" & DTROWBEAM("TYPE") & "'", "", "")
                    For Each DTROWLOOM As DataRow In DTLOOM.Rows
                        'ADD IN TEMPTABLE
                        Dim DTDATA As DataTable = OBJCMN.Execute_Any_String("INSERT INTO TEMPBALANCECUT VALUES (" & Val(DTROWBEAM("FROMNO")) & "," & Val(DTROWBEAM("FROMSRNO")) & ",'" & DTROWBEAM("TYPE") & "','" & DTROWBEAM("WEAVERNAME") & "','" & DTROWLOOM("BEAMNAME") & "','" & DTROWBEAM("BEAMNO") & "'," & Val(DTROWLOOM("TOTALCUT")) & "," & Val(DTROWLOOM("BALANCECUT")) & "," & Val(DTROWLOOM("BEAMWT")) & ",'" & DTROWLOOM("SIZER") & "'," & Val(DTROWBEAM("LOOMNO")) & "," & CmpId & "," & YearId & ")", "", "")
                    Next
                Next
            Next

            DT = OBJCMN.Execute_Any_String("SELECT * FROM TEMPBALANCECUT WHERE YEARID = " & YearId, "", "")
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

    Private Sub CMDREFRESH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDREFRESH.Click
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDEXCEL_Click(sender As Object, e As EventArgs) Handles CMDEXCEL.Click
        Try
            Dim PATH As String = Application.StartupPath & "\Loom Wise Cut Balance.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True

            For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                proc.Kill()
            Next
            opti.SheetName = "Loom Wise Cut Balance"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Loom Wise Cut Balance", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class