
Imports BL

Public Class TDSDeductedReport

    Private Sub TDSDeductedReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
            Dim OBJCMN As New ClsCommon
            Dim dt As DataTable
            Dim WHERECLAUSE As String = ""
            If cmbregister.Text.Trim <> "" Then WHERECLAUSE = " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "'"
            If RBNOTDEDUCTED.Checked = True Then
                dt = OBJCMN.search(" * ", "", " (SELECT BILL_NO AS BILLNO, BILL_PARTYBILLNO AS PARTYBILLNO, BILL_PARTYBILLDATE AS PARTYBILLDATE, LEDGERS.Acc_cmpname AS NAME, BILL_GRANDTOTAL AS GRANDTOTAL, ISNULL(TDSCHALLANVIEW.TDSAMT,0) AS TDSAMT FROM PURCHASEMASTER INNER JOIN LEDGERS ON Acc_id = BILL_LEDGERID LEFT OUTER JOIN TDSCHALLANVIEW ON TDSCHALLANVIEW.BILLINITIALS = BILL_INITIALS AND TDSCHALLANVIEW.YEARID = BILL_YEARID INNER JOIN REGISTERMASTER ON REGISTER_ID = BILL_REGISTERID WHERE BILL_INITIALS NOT IN (SELECT DISTINCT BILL_INITIALS FROM PURCHASEMASTER INNER JOIN JOURNALMASTER ON BILL_INITIALS = journal_refno AND BILL_YEARID = journal_yearid INNER JOIN LEDGERS ON journal_ledgerid = LEDGERS.ACC_ID WHERE LEDGERS.ACC_TDSAC = 'TRUE' AND BILL_YEARID = " & YearId & ") AND BILL_YEARID = " & YearId & WHERECLAUSE & " UNION  ALL SELECT NP_NO AS BILLNO, NP_PARTYBILLNO AS PARTYBILLNO, NP_PARTYBILLDATE AS PARTYBILLDATE, LEDGERS.Acc_cmpname AS NAME, NP_GRANDTOTAL AS GRANDTOTAL, ISNULL(TDSCHALLANVIEW.TDSAMT,0) AS TDSAMT FROM NONPURCHASE INNER JOIN LEDGERS ON Acc_id = NP_LEDGERID LEFT OUTER JOIN TDSCHALLANVIEW ON TDSCHALLANVIEW.BILLINITIALS = NP_INITIALS AND TDSCHALLANVIEW.YEARID = NP_YEARID INNER JOIN REGISTERMASTER ON REGISTER_ID = NP_REGISTERID WHERE NP_INITIALS NOT IN (SELECT DISTINCT NP_INITIALS FROM NONPURCHASE INNER JOIN JOURNALMASTER ON NP_INITIALS = journal_refno AND NP_YEARID = journal_yearid INNER JOIN LEDGERS ON journal_ledgerid = LEDGERS.ACC_ID WHERE LEDGERS.ACC_TDSAC = 'TRUE' AND NP_YEARID = " & YearId & ") AND NP_YEARID = " & YearId & WHERECLAUSE & ") AS T", " ORDER BY T.BILLNO ")
            Else
                dt = OBJCMN.search(" * ", "", " (SELECT DISTINCT BILL_NO AS BILLNO, BILL_PARTYBILLNO AS PARTYBILLNO, BILL_PARTYBILLDATE AS PARTYBILLDATE, LEDGERS.Acc_cmpname AS NAME, BILL_GRANDTOTAL AS GRANDTOTAL, ISNULL(TDSCHALLANVIEW.TDSAMT,0) AS TDSAMT FROM PURCHASEMASTER INNER JOIN JOURNALMASTER ON BILL_INITIALS = journal_refno AND BILL_YEARID = journal_yearid INNER JOIN LEDGERS AS JOURNALLEDGERS ON journal_ledgerid = JOURNALLEDGERS.ACC_ID INNER JOIN LEDGERS ON LEDGERS.Acc_id = BILL_LEDGERID LEFT OUTER JOIN TDSCHALLANVIEW ON TDSCHALLANVIEW.BILLINITIALS = BILL_INITIALS AND TDSCHALLANVIEW.YEARID = BILL_YEARID INNER JOIN REGISTERMASTER ON REGISTER_ID = BILL_REGISTERID WHERE JOURNALLEDGERS.ACC_TDSAC = 'TRUE' AND BILL_YEARID = " & YearId & WHERECLAUSE & " AND TDSLEDGER <> LEDGERS.ACC_CMPNAME UNION ALL SELECT DISTINCT NP_NO AS BILLNO, NP_PARTYBILLNO AS PARTYBILLNO, NP_PARTYBILLDATE AS PARTYBILLDATE, LEDGERS.Acc_cmpname AS NAME, NP_GRANDTOTAL AS GRANDTOTAL, ISNULL(TDSCHALLANVIEW.TDSAMT,0) AS TDSAMT FROM NONPURCHASE INNER JOIN JOURNALMASTER ON NP_INITIALS = journal_refno AND NP_YEARID = journal_yearid INNER JOIN LEDGERS AS JOURNALLEDGERS ON journal_ledgerid = JOURNALLEDGERS.ACC_ID INNER JOIN LEDGERS ON LEDGERS.Acc_id = NP_LEDGERID LEFT OUTER JOIN TDSCHALLANVIEW ON TDSCHALLANVIEW.BILLINITIALS = NP_INITIALS AND TDSCHALLANVIEW.YEARID = NP_YEARID INNER JOIN REGISTERMASTER ON REGISTER_ID = NP_REGISTERID WHERE JOURNALLEDGERS.ACC_TDSAC = 'TRUE' AND NP_YEARID = " & YearId & WHERECLAUSE & " AND TDSLEDGER <> LEDGERS.ACC_CMPNAME ) AS T", " ORDER BY T.BILLNO ")
            End If
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then gridbill.FocusedRowHandle = gridbill.RowCount - 1
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
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
            'For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
            '    proc.Kill()
            'Next

            Dim PERIOD As String = AccFrom & " - " & AccTo

            opti.SheetName = "Ledger Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Ledger Details", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TDSDeductedReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            fillregister(cmbregister, " and (register_type = 'PURCHASE' OR register_type = 'EXPENSE')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbregister_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbregister.Validating
        Try
            If cmbregister.Text.Trim.Length > 0 Then
                cmbregister.Text = UCase(cmbregister.Text)
                Dim clscommon As New ClsCommon
                Dim dt As DataTable
                dt = clscommon.search(" register_abbr, register_initials, register_id", "", " RegisterMaster", " and register_name ='" & cmbregister.Text.Trim & "' and (register_type = 'PURCHASE' OR register_type = 'EXPENSE') and register_cmpid = " & CmpId & " AND REGISTER_LOCATIONID = " & Locationid & " AND REGISTER_YEARID = " & YearId)
                If dt.Rows.Count <= 0 Then
                    MsgBox("Register Not Present, Add New from Master Module")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMDREFRESH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDREFRESH.Click
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class