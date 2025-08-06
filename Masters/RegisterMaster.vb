
Imports BL
Imports System.Windows.Forms

Public Class RegisterMaster

    Public edit As Boolean
    Public TempRegisterName As String
    Dim TempRegisterId As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public frmstring As String        'Used from Displaying Purchase, Sale, Receipt, Payment, Journal, Contra Master

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True
        If txtname.Text.Trim.Length = 0 Then
            EP.SetError(txtname, "Fill Register Name")
            bln = False
        End If

        If txtabbr.Text.Trim.Length = 0 Then
            EP.SetError(txtabbr, "Fill Abbr.")
            bln = False
        End If

        If txtinitials.Text.Trim.Length = 0 Then
            EP.SetError(txtinitials, "Fill Initials")
            bln = False
        End If
        Return bln
    End Function

    Sub clear()
        txtname.Clear()
        txtabbr.Clear()
        txtinitials.Clear()
        cmbname.Text = ""
        edit = False
    End Sub

    Private Sub txtname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtname.Validating
        Try
            uppercase(txtname)
            If (edit = False) Or (edit = True And tempregistername <> txtname.Text.Trim) Then
                'for search
                Dim OBJCMN As New ClsCommon
                Dim dt As DataTable
                dt = OBJCMN.search("register_name", "", "RegisterMaster", " and register_name = '" & txtname.Text.Trim & "' and REGISTER_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    MsgBox("Register Name. Already Exists", MsgBoxStyle.Critical, CmpName)
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RegisterMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Alt = True And e.KeyCode = Windows.Forms.Keys.S Then       'for Saving
                Call cmdok_Click(sender, e)
            ElseIf (e.Alt = True And e.KeyCode = Windows.Forms.Keys.X) Or (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If errorvalid() = True Then
                    Dim tempmsg As Integer = MsgBox("Save Changes?", MsgBoxStyle.YesNo)
                    If tempmsg = vbYes Then cmdok_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Keys.Oemcomma Then
                e.SuppressKeyPress = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RegisterMaster_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        Try
            If AscW(e.KeyChar) <> 33 Then
                chkchange.CheckState = CheckState.Checked
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RegisterMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'REGISTER MASTER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            If frmstring = "PURCHASE" Then
                Me.Text = "Purchase Register"
                lbl.Text = "Purchase Register"
            ElseIf frmstring = "PURCHASERETURN" Then
                Me.Text = "Purchase Return Register"
                lbl.Text = "Purchase Return Register"
            ElseIf frmstring = "SALE" Then
                Me.Text = "Sale Register"
                lbl.Text = "Sale Register"
            ElseIf frmstring = "SALERETURN" Then
                Me.Text = "Sale Return Register"
                lbl.Text = "Sale Return Register"
            ElseIf frmstring = "JOURNAL" Then
                Me.Text = "Journal Register"
                lbl.Text = "Journal Register"
            ElseIf frmstring = "EXPENSE" Then
                Me.Text = "Expense Register"
                lbl.Text = "Expense Register"
            ElseIf frmstring = "CONTRA" Then
                Me.Text = "Contra"
                lbl.Text = "Contra"
                lbldefault.Visible = True
                cmbname.Visible = True
            ElseIf frmstring = "PAYMENT" Then
                Me.Text = "Payment"
                lbl.Text = "Payment"
                lbldefault.Visible = True
                cmbname.Visible = True
            ElseIf frmstring = "RECEIPT" Then
                Me.Text = "Receipt"
                lbl.Text = "Receipt"
                lbldefault.Visible = True
                cmbname.Visible = True
            ElseIf frmstring = "DEBITNOTE" Then
                Me.Text = "Debit Note Register"
                lbl.Text = "Debit Note Register"
            ElseIf frmstring = "CREDITNOTE" Then
                Me.Text = "Credit Note Register"
                lbl.Text = "Credit Note Register"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtabbr_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtabbr.Validating
        Try
            uppercase(txtabbr)
            If (edit = False) Then
                'for search
                Dim OBJCMN As New ClsCommon
                Dim dt As DataTable
                dt = OBJCMN.search("register_abbr", "", "RegisterMaster", " and register_abbr = '" & txtabbr.Text.Trim & "' AND REGISTER_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    MsgBox("Register Abbr. Already Exists", MsgBoxStyle.Critical, CmpName)
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtinitials_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtinitials.Validating
        Try
            uppercase(txtinitials)
            If (edit = False) Then
                'for search
                Dim OBJCMN As New ClsCommon
                Dim dt As DataTable
                dt = OBJCMN.search("register_initials", "", "RegisterMaster", " and register_initials = '" & txtinitials.Text.Trim & "' and REGISTER_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    MsgBox("Register Initials Already Exists", MsgBoxStyle.Critical, CmpName)
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbname.Enter
        Try
            If cmbname.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable

                dt = OBJCMN.search(" acc_cmpname ", "", "AccountsMaster inner join GroupMaster on AccountsMaster.Acc_groupid = Groupmaster.group_id ", " and groupmaster.group_secondary ='Bank A/C' or group_secondary = 'Bank OD A/C' or group_secondary = 'Cash In Hand' AND ACC_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "Acc_cmpname"
                    cmbname.DataSource = dt
                    cmbname.DisplayMember = "Acc_cmpname"
                    If edit = False Then cmbname.Text = ""
                End If
                cmbname.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbname.Validating
        Try
            If cmbname.Text.Trim <> "" Then
                uppercase(cmbname)
                Dim OBJCMN As New ClsCommon
                Dim dt As DataTable
                dt = OBJCMN.search("Acc_add", "", "AccountsMaster inner join GroupMaster on groupmaster.group_id = AccountsMaster.Acc_groupid ", " and groupmaster.group_secondary ='Bank A/C' or group_secondary = 'Bank OD A/C' or group_secondary = 'Cash In Hand' and Acc_cmpname = '" & cmbname.Text.Trim & "' and ACC_YEARID = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("Accounts not present, Add New?", MsgBoxStyle.YesNo, "")
                    If tempmsg = vbYes Then
                        Dim objVendormaster As New AccountsMaster
                        objVendormaster.frmstring = "ACCOUNTS"
                        objVendormaster.cmbcmpname.Text = cmbname.Text.Trim()
                        objVendormaster.ShowDialog()
                        dt = OBJCMN.search("Acc_cmpname", "", "AccountsMaster inner join GroupMaster on groupmaster.group_id = AccountsMaster.Acc_groupid ", " and groupmaster.group_secondary ='Bank A/C' or group_secondary = 'Bank OD A/C' or group_secondary = 'Cash In Hand' and Acc_cmpname = '" & cmbname.Text.Trim & "' and ACC_YEARID = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            Dim a As String = cmbname.Text.Trim
                            dt1 = cmbname.DataSource
                            If cmbname.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(cmbname.Text.Trim)
                                    cmbname.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try

            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim IntResult As Integer
            Dim alParaval As New ArrayList

            alParaval.Add(txtname.Text.Trim)
            alParaval.Add(txtabbr.Text.Trim)
            alParaval.Add(txtinitials.Text.Trim)
            alParaval.Add(frmstring)
            alParaval.Add(0)
            alParaval.Add(cmbname.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)

            Dim objclsRegisterMaster As New ClsRegisterMaster
            objclsRegisterMaster.alParaval = alParaval

            If edit = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                IntResult = objclsRegisterMaster.save()
                MsgBox("Details Added")
            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TempRegisterId)
                IntResult = objclsRegisterMaster.update()
                MsgBox("Details Updated")
                edit = False

            End If

            clear()
            txtname.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Try
            If USERDELETE = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            If edit = True Then

                Dim tempmsg As Integer = MsgBox("Delete Register Permanently?", MsgBoxStyle.YesNo, "Process")
                If tempmsg = vbYes Then

                    Dim OBJREGISTER As New ClsRegisterMaster
                    Dim ALPARAVAL As New ArrayList

                    ALPARAVAL.Add(txtname.Text.Trim)
                    ALPARAVAL.Add(CmpId)

                    OBJREGISTER.alParaval = ALPARAVAL
                    Dim DT As DataTable = OBJREGISTER.DELETE()
                    MsgBox(DT.Rows(0).Item(0).ToString)

                    clear()

                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class