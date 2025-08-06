
Imports BL

Public Class GRDyeingUpdate
    Public EDIT As Boolean

    Private Sub CMDEXIT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub clear()
        Try
            TXTGRNO.Clear()
            CMBNAME.DataSource = Nothing
            TXTDYEINGNAME.Clear()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTGRNO_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTGRNO.KeyPress
        Try
            numkeypress(e, sender, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE ='PROCESSOR'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, cmbcode, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "PROCESSOR")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTGRNO_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTGRNO.Validating
        Try
            'TXTDYEINGNAME.Clear()
            'CMBNAME.Text = Nothing


            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable

            DT = OBJCMN.search("  ISNULL(LEDGERS.Acc_cmpname, '') AS DYEING ", " ", " GOODSRETURNMASTER INNER JOIN LEDGERS ON GOODSRETURNMASTER.GR_DYEINGID = LEDGERS.Acc_id ", " AND GR_NO = '" & TXTGRNO.Text.Trim & "' AND GR_YEARID = " & YearId)
            
            If DT.Rows.Count > 0 Then
                For Each DR As DataRow In DT.Rows
                    TXTDYEINGNAME.Text = Convert.ToString(DR("DYEING"))
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND ACC_SUBTYPE = 'PROCESSOR'")
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable
            If CMBNAME.Text.Trim <> "" And Val(TXTGRNO.Text.Trim) > 0 Then
                'ordermaster

                DT = OBJCMN.Execute_Any_String(" UPDATE GOODSRETURNMASTER SET GR_DYEINGID = (SELECT LEDGERS.Acc_id FROM LEDGERS where LEDGERS.Acc_cmpname='" & CMBNAME.Text.Trim & "' and LEDGERS.Acc_yearid= " & YearId & ") WHERE GOODSRETURNMASTER.GR_NO = " & Val(TXTGRNO.Text.Trim) & " AND GR_YEARID = " & YearId, "", "")
                MsgBox("Dyeing Updated Successfully")
                clear()
            End If
            Exit Sub

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDCLEAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDCLEAR.Click
        Try
            clear()
            TXTGRNO.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRDyeingUpdate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt = True And e.KeyCode = Windows.Forms.Keys.S Then       'for Saving
            Call cmdok_Click(sender, e)
        ElseIf (e.Alt = True And e.KeyCode = Windows.Forms.Keys.X) Or (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf e.KeyCode = Keys.Oemcomma Then
            e.SuppressKeyPress = True
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub GRDyeingUpdate_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        clear()
        FILLCMB()
    End Sub
End Class