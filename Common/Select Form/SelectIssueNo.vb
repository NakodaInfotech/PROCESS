Imports BL

Public Class SelectIssueNo
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public TEMPISSUENO, FRMSTRING, MACHINENAME As String
    Dim ADDCOL As Boolean = False
    Dim col As New DataGridViewCheckBoxColumn
    Public DT As New DataTable

    Private Sub CMDEXIT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SelectIssueNo_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Alt = True And e.KeyCode = Windows.Forms.Keys.O Then       'for Saving
                Call CMDOK_Click(sender, e)
            ElseIf (e.Alt = True And e.KeyCode = Windows.Forms.Keys.X) Or (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                Me.Close()
            ElseIf e.KeyCode = Keys.Oemcomma Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub



    Private Sub CMDOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        Try
            If GRIDISSUENO.SelectedRows.Count > 0 Then
                TEMPISSUENO = GRIDISSUENO.Rows(GRIDISSUENO.SelectedRows(0).Index).Cells(0).Value
            Else
                TEMPISSUENO = ""
            End If
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try

        'Try

        '    Dim objclspreq As New ClsCommon()
        '    DT.Columns.Add("YISSUEMACNO")
        '    DT.Columns.Add("SRNO")
        '    DT.Columns.Add("PIECETYPE")
        '    DT.Columns.Add("BALENO")
        '    DT.Columns.Add("ITEMNAME")
        '    DT.Columns.Add("QUALITY")
        '    DT.Columns.Add("DESIGN")
        '    DT.Columns.Add("COLOR")
        '    DT.Columns.Add("PCS")
        '    DT.Columns.Add("MTRS")
        '    DT.Columns.Add("FROMTYPE")

        '    For Each ROW As DataGridViewRow In GRIDYARNISSUEMACHINE.Rows
        '        If ROW.Cells(0).Value = True Then
        '            DT.Rows.Add(ROW.Cells(1).Value, ROW.Cells(3).Value, ROW.Cells(4).Value, ROW.Cells(5).Value, ROW.Cells(6).Value, ROW.Cells(7).Value, ROW.Cells(8).Value, ROW.Cells(9).Value, ROW.Cells(10).Value, ROW.Cells(11).Value, ROW.Cells(12).Value)
        '        End If
        '    Next
        '    Me.Close()
        'Catch ex As Exception
        '    If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        'End Try
    End Sub

    Private Sub SelectIssueNo_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'MFG'")

            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            FILLGRID(" AND MACHINEMASTER.MACHINE_NAME = '" & MACHINENAME & "' AND YARNISSUEMACHINE.YISSUEMAC_CMPID = " & CmpId & " AND YARNISSUEMACHINE.YISSUEMAC_YEARID = " & YearId & " order by YARNISSUEMACHINE.YISSUEMAC_NO")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID(ByVal WHERECLAUSE As String)
        Try
            Dim OBJCMN As New ClsCommon
            'YARNISSUEMACHINE.YISSUEMAC_no AS YISSUEMACNO ", "", "  YARNISSUEMACHINE INNER YISSUEMACIN LEDGERS ON YARNISSUEMACHINE.YISSUEMAC_ledgerid = LEDGERS.Acc_id AND YARNISSUEMACHINE.YISSUEMAC_cmpid = LEDGERS.Acc_cmpid AND YARNISSUEMACHINE.YISSUEMAC_locationid = LEDGERS.Acc_locationid AND YARNISSUEMACHINE.YISSUEMAC_yearid = LEDGERS.Acc_yearid ", " AND LEDGERS.Acc_CMPNAME='" & cmbname.Text.Trim & "' AND (YARNISSUEMACHINE.YISSUEMAC_TOTALMTRS - YARNISSUEMACHINE.YISSUEMAC_RECDMTRS) > 0 "
            Dim DT As DataTable = OBJCMN.search(" YARNISSUEMACHINE.YISSUEMAC_no AS YISSUEMACNO, YARNISSUEMACHINE.YISSUEMAC_date AS YISSUEMACDATE, YARNISSUEMACHINE.YISSUEMAC_TOTALMTRS AS TOTALMTRS, ISNULL(YARNISSUEMACHINE.YISSUEMAC_TOTALMTRS - YARNISSUEMACHINE.YISSUEMAC_RECDMTRS, 0) AS PENDINGMTRS ", "", "  YARNISSUEMACHINE INNER YISSUEMACIN LEDGERS ON YARNISSUEMACHINE.YISSUEMAC_ledgerid = LEDGERS.Acc_id AND YARNISSUEMACHINE.YISSUEMAC_cmpid = LEDGERS.Acc_cmpid AND YARNISSUEMACHINE.YISSUEMAC_locationid = LEDGERS.Acc_locationid AND YARNISSUEMACHINE.YISSUEMAC_yearid = LEDGERS.Acc_yearid", " AND LEDGERS.Acc_CMPNAME='" & MACHINENAME & "' AND (YARNISSUEMACHINE.YISSUEMAC_TOTALMTRS - YARNISSUEMACHINE.YISSUEMAC_RECDMTRS) > 0 AND YARNISSUEMACHINE.YISSUEMAC_CLOSE=0 ")

            GRIDISSUENO.DataSource = DT

            GRIDISSUENO.Columns(0).HeaderText = "YISSUEMAC No"
            GRIDISSUENO.Columns(0).Width = 80
            GRIDISSUENO.Columns(1).HeaderText = "Date"
            GRIDISSUENO.Columns(1).Width = 80
            GRIDISSUENO.Columns(2).HeaderText = "Total Mtrs"
            GRIDISSUENO.Columns(2).Width = 100
            GRIDISSUENO.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            GRIDISSUENO.Columns(3).HeaderText = "Pending Mtrs"
            GRIDISSUENO.Columns(3).Width = 120
            GRIDISSUENO.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GRIDISSUENO_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDISSUENO.CellDoubleClick
        Try
            If GRIDISSUENO.Rows.Count > 0 Then
                CMDOK_Click(sender, e)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDISSUENO_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDISSUENO.KeyDown
        Try
            If e.KeyCode = Keys.Enter And GRIDISSUENO.RowCount > 0 Then
                If GRIDISSUENO.SelectedRows.Count > 0 Then
                    TEMPISSUENO = GRIDISSUENO.Rows(GRIDISSUENO.SelectedRows(0).Index).Cells(0).Value
                Else
                    TEMPISSUENO = ""
                End If
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDISSUENO_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDISSUENO.CellClick
        'If e.RowIndex >= 0 Then
        '    With GRIDYARNISSUEMACHINE.Rows(e.RowIndex).Cells(0)
        '        If .Value = True Then
        '            .Value = False
        '        Else
        '            .Value = True
        '        End If
        '    End With
        'End If
    End Sub

End Class