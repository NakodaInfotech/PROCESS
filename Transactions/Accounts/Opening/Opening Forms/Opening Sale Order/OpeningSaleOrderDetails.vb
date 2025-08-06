Imports BL

Public Class OpeningSaleOrderDetails

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub OpeningSaleOrderDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.N And e.Control = True Then
                showform(False, 0)
            ElseIf e.KeyCode = Keys.P And e.Alt = True Then
                PrintToolStripButton_Click(sender, e)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub OpeningSaleOrderDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid()
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search(" OPENINGSALEORDER.OPSO_NO AS SRNO, OPENINGSALEORDER.OPSO_date AS DATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(AGENT.Acc_cmpname,'') AS AGENT, ISNULL(OPENINGSALEORDER.OPSO_TOTALPCS, 0) AS TOTALPCS, ISNULL(OPENINGSALEORDER.OPSO_TOTALMTRS, 0) AS TOTALMTRS, ISNULL(OPENINGSALEORDER.OPSO_TOTALAMT, 0) AS TOTALAMT, ISNULL(TRANSPORT.Acc_cmpname,'') AS TRANS, ISNULL(OPENINGSALEORDER.OPSO_REMARKS, '') AS REMARKS, ISNULL(OPENINGSALEORDER.OPSO_VERIFIED, 0) AS VERIFIED, ISNULL(OPENINGSALEORDER.OPSO_TYPE, 'GREY') AS [TYPE]", "", " OPENINGSALEORDER INNER JOIN LEDGERS ON OPENINGSALEORDER.OPSO_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS TRANSPORT ON OPENINGSALEORDER.OPSO_transid = TRANSPORT.Acc_id LEFT OUTER JOIN LEDGERS AS AGENT ON OPENINGSALEORDER.OPSO_Agentid = AGENT.Acc_id ", " and dbo.OPENINGSALEORDER.OPSO_yearid=" & YearId & " order by dbo.OPENINGSALEORDER.OPSO_no")
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal OPSONO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim OBJOPSO As New OpeningSaleOrder
                OBJOPSO.MdiParent = MDIMain
                OBJOPSO.edit = editval
                OBJOPSO.TEMPOPSONO = OPSONO
                OBJOPSO.Show()
                Me.Close()
            End If
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

    Private Sub gridbilldetails_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gridbilldetails.DoubleClick
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

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EXCELTOOL.Click
        Try

            Dim PATH As String = Application.StartupPath & "\Opening Sale Order Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True

            For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                proc.Kill()
            Next
            opti.SheetName = "Sale Order Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Opening Sale Order Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
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
End Class