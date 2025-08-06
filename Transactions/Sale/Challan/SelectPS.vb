
Imports BL

Public Class SelectPS

    Public DT As DataTable
    Public DTBALE As New DataTable
    Public QUALITY As String = ""
    Public MAINLINENO, FROMNO As Integer

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub SelectPS_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub SelectPS_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Top = 100
        fillgrid()
    End Sub

    Sub fillgrid(Optional ByVal where As String = "")
        Try
            If QUALITY <> "" Then where = where & " AND GREY_NAME = '" & QUALITY & "'"
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.search(" CAST(0 AS BIT) AS CHK, PACKINGSLIP.PS_NO AS PSNO, PACKINGSLIP.PS_DATE AS DATE, ISNULL(LEDGERS.Acc_cmpname,'') AS NAME, ISNULL(FROMLEDGERS.Acc_cmpname,'') AS FROMNAME, ISNULL(PACKINGLEDGERS.Acc_cmpname,'') AS PACKERNAME, GREYQUALITYMASTER.GREY_NAME AS QUALITY, PACKINGSLIP.PS_TOTALTAKA AS TOTALTAKA, PACKINGSLIP.PS_TOTALMTRS AS TOTALMTRS, PACKINGSLIP.PS_TOTALTP AS TOTALTP ", "", " PACKINGSLIP INNER JOIN GREYQUALITYMASTER ON PACKINGSLIP.PS_QUALITYID = GREYQUALITYMASTER.GREY_ID LEFT OUTER JOIN LEDGERS ON PACKINGSLIP.PS_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS FROMLEDGERS ON PACKINGSLIP.PS_FROMLEDGERID = FROMLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS PACKINGLEDGERS ON PACKINGSLIP.PS_PACKERLEDGERID = PACKINGLEDGERS.Acc_id ", " AND PS_DONE = 'FALSE' AND PACKINGSLIP.PS_YEARID = " & YearId & where)
            gridbilldetails.DataSource = DT
            If DT.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try

            'CODE FOR SELECTING ONLY SINGLE BALE
            'Dim COUNT As Integer
            'For i As Integer = 0 To gridbill.RowCount - 1
            '    Dim dtrow As DataRow = gridbill.GetDataRow(i)
            '    If Convert.ToBoolean(dtrow("CHK")) = True Then
            '        COUNT = COUNT + 1
            '    End If
            'Next

            'If COUNT > 1 Then
            '    MsgBox("You Can Select Only One Order")
            '    Exit Sub
            'End If

            If DT.Columns.Count = 0 Then
                DT.Columns.Add("SRNO")
                DT.Columns.Add("MTRS")
                DT.Columns.Add("TP")
                DT.Columns.Add("MAINLINENO")
            End If

            DTBALE.Columns.Add("PSNO")
            DTBALE.Columns.Add("QUALITY")
            DTBALE.Columns.Add("TOTALTAKA")
            DTBALE.Columns.Add("TOTALMTRS")
            DTBALE.Columns.Add("TOTALTP")

            Dim GRIDSRNO As Integer = 1

            For i As Integer = 0 To gridbill.RowCount - 1
                Dim ROW As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(ROW("CHK")) = True Then
                    DTBALE.Rows.Add(ROW("PSNO"), ROW("QUALITY"), Val(ROW("TOTALTAKA")), Val(ROW("TOTALMTRS")), ROW("TOTALTP"))


                    'WE NEED TO ADD PCS DETAILS OF SELECTED BALES IN DTPCS
                    'FIRST REMOVE LINES FROM DT IF PRESENT FOR THE SAME MAINLINENO, THEN ADD AGAIN
LINE1:
                    For Each DTROW As DataRow In DT.Rows
                        If DTROW("MAINLINENO") = MAINLINENO Then
                            DT.Rows.Remove(DTROW)
                            GoTo LINE1
                        End If
                    Next


                    Dim OBJCMN As New ClsCommon
                    Dim DTPS As DataTable = OBJCMN.search("PS_GRIDSRNO AS GRIDSRNO, PS_MTRS AS MTRS, PS_TP AS TP", "", " PACKINGSLIP_DESC ", " AND PS_NO = " & ROW("PSNO") & " AND PS_YEARID = " & YearId & " ORDER BY PS_GRIDSRNO")
                    For Each ROWPS As DataRow In DTPS.Rows
                        DT.Rows.Add(Val(GRIDSRNO), Val(ROWPS("MTRS")), Val(ROWPS("TP")), MAINLINENO)
                    Next
                    GRIDSRNO += 1
                    MAINLINENO += 1
                End If
            Next

            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class