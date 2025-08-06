
Imports BL
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Grid

Public Class PendingLotno
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public DT As New DataTable

    Private Sub PendingLotno_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.KeyCode = Keys.OemQuotes Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub PendingLotno_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'GDN'")
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
            If RBPENDING.Checked = True Then WHERECLAUSE = " AND ISNULL(CHALLANMASTER.CHALLAN_LOTNO,0) = 0 " Else WHERECLAUSE = " AND ISNULL(CHALLANMASTER.CHALLAN_LOTNO,0) <> 0"

            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.search("ISNULL(CHALLANMASTER.CHALLAN_NO, 0) AS CHALLANNO, ISNULL(MILLLEDGERS.Acc_cmpname, '') AS MILLNAME, CHALLANMASTER.CHALLAN_DATE AS DATE, ISNULL(LEDGERS.ACC_CMPNAME,'') AS NAME, ISNULL(INVOICEMASTER_DESC.INVOICE_NO,0) AS INVOICENO, ISNULL(CHALLANMASTER_DESC.CHALLAN_GRIDSRNO, 0) AS GRIDSRNO, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN, ISNULL(DELIVERYATMASTER.ACC_CMPNAME, '') AS DELAT, (CASE WHEN CHALLANMASTER.CHALLAN_TYPE = 'GREY' THEN ISNULL(GREYQUALITYMASTER.GREY_NAME, '') ELSE ISNULL(QUALITYMASTER.QUALITY_NAME, '') END) AS QUALITY, ISNULL(CHALLANMASTER_DESC.CHALLAN_TAKA, 0) AS TAKA, ISNULL(CHALLANMASTER_DESC.CHALLAN_MTRS, 0) AS MTRS, ISNULL(CHALLANMASTER.CHALLAN_LOTNO, 0) AS LOTNO, ISNULL(CHALLAN_DONE,0) DONE", "", " CHALLANMASTER INNER JOIN CHALLANMASTER_DESC ON CHALLANMASTER.CHALLAN_NO = CHALLANMASTER_DESC.CHALLAN_NO AND CHALLANMASTER.CHALLAN_YEARID = CHALLANMASTER_DESC.CHALLAN_YEARID INNER JOIN GODOWNMASTER ON CHALLANMASTER.CHALLAN_GODOWNID = GODOWNMASTER.GODOWN_ID INNER JOIN LEDGERS ON CHALLANMASTER.CHALLAN_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS MILLLEDGERS ON CHALLANMASTER_DESC.CHALLAN_MILLID = MILLLEDGERS.Acc_id LEFT OUTER JOIN QUALITYMASTER ON CHALLANMASTER_DESC.CHALLAN_QUALITYID = QUALITYMASTER.QUALITY_ID LEFT OUTER JOIN GREYQUALITYMASTER ON CHALLANMASTER_DESC.CHALLAN_QUALITYID = GREYQUALITYMASTER.GREY_ID LEFT OUTER JOIN LEDGERS AS DELIVERYATMASTER ON CHALLANMASTER.CHALLAN_DELIVERYID = DELIVERYATMASTER.Acc_id LEFT OUTER JOIN INVOICEMASTER_DESC ON CHALLANMASTER_DESC.CHALLAN_NO = INVOICEMASTER_DESC.INVOICE_FROMNO AND CHALLANMASTER_DESC.CHALLAN_YEARID = INVOICEMASTER_DESC.INVOICE_YEARID AND INVOICEMASTER_DESC.INVOICE_GRIDTYPE = 'CHALLAN' ", WHERECLAUSE & " AND ISNULL(CHALLANMASTER.CHALLAN_NOGR,0) = 'FALSE' AND CHALLANMASTER.CHALLAN_YEARID = " & YearId & " ORDER BY CHALLANMASTER.CHALLAN_NO")
            gridbilldetails.DataSource = DT
            If DT.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal CHALLANNO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim OBJCHALLAN As New Challan
                OBJCHALLAN.MdiParent = MDIMain
                OBJCHALLAN.EDIT = editval
                OBJCHALLAN.TEMPCHALLANNO = CHALLANNO
                OBJCHALLAN.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        Me.Close()
    End Sub

    Private Sub gridbilldetails_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gridbilldetails.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("CHALLANNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        Try
            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If dtrow("LOTNO") <> 0 Then
                    Dim OBJCMN As New ClsCommon
                    DT = OBJCMN.Execute_Any_String("UPDATE CHALLANMASTER SET CHALLANMASTER.CHALLAN_LOTNO = " & dtrow("LOTNO") & " WHERE CHALLANMASTER.CHALLAN_NO = " & dtrow("CHALLANNO") & "  AND CHALLANMASTER.CHALLAN_YEARID = " & YearId, "", "")
                    DT = OBJCMN.Execute_Any_String("UPDATE INVOICEMASTER_DESC SET INVOICEMASTER_DESC.INVOICE_LOTNO = " & dtrow("LOTNO") & " WHERE INVOICEMASTER_DESC.INVOICE_FROMNO = " & dtrow("CHALLANNO") & " AND INVOICEMASTER_DESC.INVOICE_GRIDTYPE = 'CHALLAN' AND INVOICEMASTER_DESC.INVOICE_YEARID = " & YearId, "", "")
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
            If Val(gridbill.GetRowCellValue(e.RowHandle, "LOTNO")) > 0 Then
                If MsgBox("Save Entry?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then Call CMDOK_Click(sender, e)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Private Sub gridbill_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gridbill.RowStyle
    '    Try
    '        If e.RowHandle >= 0 Then
    '            Dim View As GridView = sender
    '            If View.GetRowCellDisplayText(e.RowHandle, View.Columns("DONE")) = "Checked" Then
    '                e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Bold)
    '                e.Appearance.BackColor = Color.Yellow
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

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
            DT = OBJCMN.Execute_Any_String("UPDATE CHALLANMASTER SET CHALLAN_LOTNO= 0  WHERE CHALLAN_NO = " & ROW("CHALLANNO") & " AND CHALLAN_YEARID = " & YearId, "", "")
            fillgrid()
            gridbill.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


End Class