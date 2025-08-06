
Imports BL
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid

Public Class OpeningPurchaseOrderDetails

    Public edit As Boolean
    Dim temppreqno As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub OpeningPurchaseOrderDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.N And e.Control = True Then
                showform(False, 0)
            ElseIf e.KeyCode = Keys.Enter Then
                cmdok_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.R Then
                Call TOOLREFRESH_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.P Then
                Call TOOLEXCEL_Click(sender, e)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub OpeningPurchaseOrderDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'OPENING'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            fillgrid(" and dbo.OPENINGPURCHASEORDER.OPPO_yearid=" & YearId & " order by dbo.OPENINGPURCHASEORDER.OPPO_no ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid(ByVal tepmcondition)
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable
            dt = objclsCMST.search(" OPENINGPURCHASEORDER.OPPO_NO AS SRNO,OPENINGPURCHASEORDER.OPPO_date AS DATE, ISNULL(LEDGERS.Acc_cmpname,'') AS NAME, ISNULL(MILLLEDGERS.Acc_cmpname,'') AS MILLNAME,OPENINGPURCHASEORDER.OPPO_TOTALBAGS AS TOTALBAGS, (OPENINGPURCHASEORDER_DESC.OPPO_BAGS - OPENINGPURCHASEORDER_DESC.OPPO_OUTBAGS) AS PENDINGBAGS, OPENINGPURCHASEORDER.OPPO_GRANDTOTAL AS TOTALAMT, CAST(OPENINGPURCHASEORDER.OPPO_REMARKS AS VARCHAR(MAX)) AS REMARKS,OPENINGPURCHASEORDER.OPPO_DONE  AS DONE,OPENINGPURCHASEORDER.OPPO_CLOSE AS CLOSED ", "", "OPENINGPURCHASEORDER LEFT OUTER JOIN LEDGERS ON OPENINGPURCHASEORDER.OPPO_LEDGERID = LEDGERS.Acc_id INNER JOIN LEDGERS AS MILLLEDGERS ON OPENINGPURCHASEORDER.OPPO_MILLID = MILLLEDGERS.Acc_id INNER JOIN OPENINGPURCHASEORDER_DESC ON OPENINGPURCHASEORDER.OPPO_NO =OPENINGPURCHASEORDER_DESC.OPPO_NO AND OPENINGPURCHASEORDER.OPPO_CMPID =OPENINGPURCHASEORDER_DESC.OPPO_CMPID AND OPENINGPURCHASEORDER.OPPO_LOCATIONID =OPENINGPURCHASEORDER_DESC.OPPO_LOCATIONID AND OPENINGPURCHASEORDER.OPPO_YEARID =OPENINGPURCHASEORDER_DESC.OPPO_YEARID ", tepmcondition)
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal OPPONO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim objOPPO As New OpeningPurchaseOrder
                objOPPO.MdiParent = MDIMain
                objOPPO.edit = editval
                objOPPO.TEMPOPPONO = OPPONO
                objOPPO.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
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

    Private Sub gridpayment_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gridbill.RowStyle
        Try
            If e.RowHandle >= 0 Then
                Dim View As GridView = sender
                If View.GetRowCellDisplayText(e.RowHandle, View.Columns("CLOSED")) = "Checked" Then
                    e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Bold)
                    e.Appearance.BackColor = Color.LightSkyBlue
                ElseIf View.GetRowCellDisplayText(e.RowHandle, View.Columns("DONE")) = "Checked" Then
                    e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Bold)
                    e.Appearance.BackColor = Color.Yellow
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLEXCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try

            Dim PATH As String = Application.StartupPath & "\Opening Purchase Order Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True

            For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                proc.Kill()
            Next
            opti.SheetName = "Opening Purchase Order Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Opening Purchase Order Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLREFRESH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLREFRESH.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            fillgrid(" and dbo.OPENINGPURCHASEORDER.OPPO_yearid=" & YearId & " order by dbo.OPENINGPURCHASEORDER.OPPO_no")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class