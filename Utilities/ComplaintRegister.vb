
Imports BL
Imports System.Windows.Forms
Imports System.IO

Public Class ComplaintRegister

    Dim IntResult As Integer
    Public edit As Boolean          'used for editing
    Public TEMPCOMPLAINTNO As Integer     'used for poation no while editing
    Dim TEMPMSG As Integer

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub clear()
        Try
            EP.Clear()
            CMBNAME.Text = ""
            TXTCOMPLAINT.Clear()
            TXTSOLUTION.Clear()
            CMBSTATUS.SelectedIndex = 0
            TXTRECEIVEDBY.Clear()

            txtadd.Clear()
            tstxtbillno.Clear()

            getmaxno()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdclear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        Try
            clear()
            edit = False
            CMBNAME.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub getmaxno()
        Try
            Dim DTTABLE As New DataTable
            DTTABLE = getmax(" isnull(max(COM_NO),0) + 1 ", " COMPLAINTREGISTER ", " AND COM_YEARID = " & YearId)
            If DTTABLE.Rows.Count > 0 Then TXTCOMPLAINTNO.Text = DTTABLE.Rows(0).Item(0)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function errorvalid() As Boolean
        Try
            Dim bln As Boolean = True

            If CMBNAME.Text.Trim.Length = 0 Then
                EP.SetError(CMBNAME, " Please Fill Party Name ")
                bln = False
            End If

            If TXTCOMPLAINT.Text.Trim.Length = 0 Then
                EP.SetError(TXTCOMPLAINT, " Please Fill Complaint ")
                bln = False
            End If

            If CMBSTATUS.Text.Trim.Length = 0 Then
                EP.SetError(CMBSTATUS, " Please Select Status ")
                bln = False
            End If

            If TXTRECEIVEDBY.Text.Trim.Length = 0 Then
                EP.SetError(TXTRECEIVEDBY, " Please Fill Complaint Received By ")
                bln = False
            End If

            Return bln
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Function

    Private Sub cmdok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            Cursor.Current = Cursors.WaitCursor
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList
            alParaval.Add(DTCOMPLAINT.Value.Date)
            alParaval.Add(CMBNAME.Text.Trim)
            alParaval.Add(TXTCOMPLAINT.Text.Trim)
            alParaval.Add(TXTSOLUTION.Text.Trim)
            alParaval.Add(CMBSTATUS.Text.Trim)
            alParaval.Add(TXTRECEIVEDBY.Text.Trim)


            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)

            Dim OBJCOM As New ClsComplaintRegister()
            OBJCOM.alParaval = alParaval
            If edit = False Then
                Dim DTTABLE As DataTable = OBJCOM.SAVE()
                TEMPCOMPLAINTNO = DTTABLE.Rows(0).Item(0)
                MsgBox("Details Added")
            ElseIf edit = True Then
                alParaval.Add(TEMPCOMPLAINTNO)

                IntResult = OBJCOM.UPDATE()
                MsgBox("Details Updated")
            End If

            edit = False
            'clear()
            'SHOW NEXT BILL ON EDIT MODE DONT CLEAR
            Call toolnext_Click(sender, e)

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub ComplaintRegister_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for Delete
            tstxtbillno.Focus()
            tstxtbillno.SelectAll()
        ElseIf e.KeyCode = Keys.OemQuotes Then
            e.SuppressKeyPress = True
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.KeyCode = Keys.Left And e.Alt = True Then
            Call toolprevious_Click(sender, e)
        ElseIf e.KeyCode = Keys.Right And e.Alt = True Then
            Call toolnext_Click(sender, e)
        End If
    End Sub

    Private Sub ComplaintRegister_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            fillcmb()
            clear()

            If edit = True Then

                Dim OBJCOM As New ClsComplaintRegister()
                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(TEMPCOMPLAINTNO)
                ALPARAVAL.Add(YearId)
                OBJCOM.alParaval = ALPARAVAL
                Dim dttable As DataTable = OBJCOM.SELECTCOMPLAINT()

                If dttable.Rows.Count > 0 Then

                    For Each dr As DataRow In dttable.Rows

                        TXTCOMPLAINTNO.Text = TEMPCOMPLAINTNO
                        TEMPCOMPLAINTNO = Convert.ToString(dr("COMPLAINTNO").ToString)
                        DTCOMPLAINT.Value = Convert.ToDateTime(dr("DATE"))
                        CMBNAME.Text = Convert.ToString(dr("NAME").ToString)
                        TXTCOMPLAINT.Text = Convert.ToString(dr("COMPLAINT").ToString)
                        TXTSOLUTION.Text = Convert.ToString(dr("SOLUTION").ToString)
                        CMBSTATUS.Text = Convert.ToString(dr("STATUS").ToString)
                        TXTRECEIVEDBY.Text = Convert.ToString(dr("RECEIVEDBY").ToString)
                    Next

                    CMBNAME.Focus()
                Else
                    edit = False
                    clear()
                End If
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try

    End Sub

    Sub fillcmb()
        Try
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, edit, " and (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' OR GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS') AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try

            Dim OBJCOM As New ComplaintRegisterDetails
            OBJCOM.MdiParent = MDIMain
            OBJCOM.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                TEMPCOMPLAINTNO = Val(tstxtbillno.Text)
                If TEMPCOMPLAINTNO > 0 Then
                    edit = True
                    ComplaintRegister_Load(sender, e)
                Else
                    clear()
                    edit = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
LINE1:
            TEMPCOMPLAINTNO = Val(TXTCOMPLAINTNO.Text) - 1
            If TEMPCOMPLAINTNO > 0 Then
                edit = True
                ComplaintRegister_Load(sender, e)
            Else
                clear()
                edit = False
            End If
            If CMBNAME.Text = "" And TEMPCOMPLAINTNO > 1 Then
                TXTCOMPLAINTNO.Text = TEMPCOMPLAINTNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
LINE1:
            TEMPCOMPLAINTNO = Val(TXTCOMPLAINTNO.Text) + 1
            getmaxno()
            Dim MAXNO As Integer = TXTCOMPLAINTNO.Text.Trim
            clear()
            If Val(TXTCOMPLAINTNO.Text) - 1 >= TEMPCOMPLAINTNO Then
                edit = True
                ComplaintRegister_Load(sender, e)
            Else
                clear()
                edit = False
            End If
            If CMBNAME.Text = "" And TEMPCOMPLAINTNO < MAXNO Then
                TXTCOMPLAINTNO.Text = TEMPCOMPLAINTNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tooldelete.Click
        Try
            Call cmddelete_Click(sender, e)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub DTCOMPLAINT_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DTCOMPLAINT.Validating
        If Not datecheck(DTCOMPLAINT.Value) Then
            MsgBox("Date Not in Current Accounting Year")
            e.Cancel = True
        End If
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Dim IntResult As Integer
        Try

            If edit = True Then

                TEMPMSG = MsgBox("Delete Complaint?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbYes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(TXTCOMPLAINTNO.Text.Trim)
                    alParaval.Add(YearId)
                    alParaval.Add(ClientName)

                    Dim Clsgrn As New ClsComplaintRegister()
                    Clsgrn.alParaval = alParaval
                    IntResult = Clsgrn.Delete()
                    MsgBox("Complaint Deleted")
                    clear()
                    edit = False
                End If
            Else
                MsgBox("Delete is only in Edit Mode")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbname_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, edit, " AND (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' OR GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS') and ACC_TYPE = 'ACCOUNTS' ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, CMBCODE, e, Me, txtadd, " AND (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' OR GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS') AND LEDGERS.ACC_TYPE = 'ACCOUNTS' ", "Sundry Creditors OR SUNDRY DEBTORS", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' OR GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS') AND LEDGERS.ACC_TYPE = 'ACCOUNTS' "
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then CMBNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class