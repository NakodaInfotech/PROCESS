
Imports BL

Public Class PipeTransfer

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public EDIT As Boolean
    Public TEMPTRANSFERNO As Integer

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        Try
            clear()
            EDIT = False
            TRANSFERDATE.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub clear()
        Try
            EP.Clear()
            TRANSFERDATE.Text = Mydate
            tstxtbillno.Clear()

            CMBGODOWNFROM.Text = ""
            CMBJOBBERFROM.Text = ""
            CMBGODOWNTO.Text = ""
            CMBJOBBERTO.Text = ""
            CMBTRANS.Text = ""
            TXTQTY.Clear()
            TXTCHALLANNO.Clear()
            CHALLANDATE.Clear()
            TXTREMARKS.Clear()

            getmax_BILL_no()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub getmax_BILL_no()
        Try
            Dim DTTABLE As DataTable = getmax(" isnull(max(PIPE_NO),0) + 1 ", " PIPETRANSFER ", " AND PIPE_YEARID = " & YearId)
            If DTTABLE.Rows.Count > 0 Then TXTTRANSFERNO.Text = DTTABLE.Rows(0).Item(0)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PipeTransfer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If errorvalid() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then cmdok_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.KeyCode = Keys.Oemcomma Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for Delete
                tstxtbillno.Focus()
                tstxtbillno.SelectAll()
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Keys.Left And e.Alt = True Then
                Call toolprevious_Click(sender, e)
            ElseIf e.KeyCode = Keys.Right And e.Alt = True Then
                Call toolnext_Click(sender, e)
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.WaitCursor
        End Try
    End Sub

    Sub FILLCMB()
        Try
            If CMBJOBBERFROM.Text.Trim = "" Then fillname(CMBJOBBERFROM, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS' AND (ACC_SUBTYPE = 'SIZER' OR ACC_SUBTYPE = 'WEAVER') ")
            If CMBJOBBERTO.Text.Trim = "" Then fillname(CMBJOBBERTO, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS' AND (ACC_SUBTYPE = 'SIZER' OR ACC_SUBTYPE = 'WEAVER') ")
            If CMBTRANS.Text.Trim = "" Then fillname(CMBTRANS, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'TRANSPORT'")
            If CMBGODOWNFROM.Text.Trim = "" Then fillGODOWN(CMBGODOWNFROM, False, " AND GODOWN_ISOUR = 'True'")
            If CMBGODOWNTO.Text.Trim = "" Then fillGODOWN(CMBGODOWNTO, False, " AND GODOWN_ISOUR = 'True'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PipeTRANSFER_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'STORES'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor

            FILLCMB()
            clear()

            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim OBJTRANSFER As New ClsPipeTransfer
                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(TEMPTRANSFERNO)
                ALPARAVAL.Add(YearId)
                OBJTRANSFER.alParaval = ALPARAVAL
                Dim dttable As DataTable = OBJTRANSFER.SELECTTRANSFER()

                If dttable.Rows.Count > 0 Then
                    For Each dr As DataRow In dttable.Rows

                        TXTTRANSFERNO.Text = TEMPTRANSFERNO
                        TRANSFERDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")

                        CMBJOBBERFROM.Text = Convert.ToString(dr("NAMEFROM").ToString)
                        CMBGODOWNFROM.Text = dr("GODOWNFROM").ToString

                        CMBJOBBERTO.Text = Convert.ToString(dr("NAMETO").ToString)
                        CMBGODOWNTO.Text = dr("GODOWNTO").ToString

                        TXTQTY.Text = Val(dr("QTY").ToString)
                        CMBTRANS.Text = dr("TRANSNAME").ToString

                        TXTCHALLANNO.Text = dr("CHALLANNO")
                        If dr("CHALLANDATE") <> "01/01/1900" Then CHALLANDATE.Text = Format(Convert.ToDateTime(dr("CHALLANDATE")).Date, "dd/MM/yyyy")
                        TXTREMARKS.Text = Convert.ToString(dr("REMARKS").ToString)
                    Next
                Else
                    EDIT = False
                    clear()
                End If
            End If

        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Dim IntResult As Integer
        Try
            Cursor.Current = Cursors.WaitCursor
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList

            alParaval.Add(Format(Convert.ToDateTime(TRANSFERDATE.Text).Date, "MM/dd/yyyy"))

            alParaval.Add(CMBJOBBERFROM.Text.Trim)
            alParaval.Add(CMBGODOWNFROM.Text.Trim)

            alParaval.Add(CMBJOBBERTO.Text.Trim)
            alParaval.Add(CMBGODOWNTO.Text.Trim)

            alParaval.Add(Val(TXTQTY.Text.Trim))
            alParaval.Add(CMBTRANS.Text.Trim)

            alParaval.Add(TXTCHALLANNO.Text)
            If CHALLANDATE.Text <> "__/__/____" Then alParaval.Add(Format(Convert.ToDateTime(CHALLANDATE.Text).Date, "MM/dd/yyyy")) Else alParaval.Add("")
            alParaval.Add(TXTREMARKS.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)

            Dim OBJTRANSFER As New ClsPipeTransfer
            OBJTRANSFER.alParaval = alParaval
            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DTTABLE As DataTable = OBJTRANSFER.SAVE()
                TEMPTRANSFERNO = DTTABLE.Rows(0).Item(0)
                MessageBox.Show("Details Added")

            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPTRANSFERNO)
                IntResult = OBJTRANSFER.UPDATE()
                MessageBox.Show("Details Updated")
                EDIT = False
            End If

            'clear()
            'SHOW NEXT BILL ON EDIT MODE DONT CLEAR
            Call toolnext_Click(sender, e)
            TRANSFERDATE.Focus()

        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try

    End Sub

    Private Function errorvalid() As Boolean
        Try
            Dim bln As Boolean = True

            If TRANSFERDATE.Text = "__/__/____" Then
                EP.SetError(TRANSFERDATE, " Please Enter Proper Date")
                bln = False
            Else
                If Not datecheck(TRANSFERDATE.Text) Then
                    EP.SetError(TRANSFERDATE, "Date not in Accounting Year")
                    bln = False
                End If
            End If

            If CHALLANDATE.Text <> "__/__/____" Then
                If Not datecheck(CHALLANDATE.Text) Then
                    EP.SetError(CHALLANDATE, "Date not in Accounting Year")
                    bln = False
                End If

                If Convert.ToDateTime(TRANSFERDATE.Text).Date < Convert.ToDateTime(CHALLANDATE.Text).Date Then
                    EP.SetError(CHALLANDATE, " Please Enter Proper Challan Date")
                    bln = False
                End If
            End If

            If CMBGODOWNFROM.Text.Trim.Length = 0 And CMBJOBBERFROM.Text.Trim.Length = 0 Then
                EP.SetError(CMBGODOWNFROM, " Please Fill Godown / Jobber Name ")
                bln = False
            End If

            If CMBGODOWNFROM.Text.Trim.Length > 0 And CMBJOBBERFROM.Text.Trim.Length > 0 Then
                EP.SetError(CMBGODOWNFROM, " Please Fill Either Godown / Jobber Name, Both are not allowed ")
                bln = False
            End If

            If CMBGODOWNTO.Text.Trim.Length = 0 And CMBJOBBERTO.Text.Trim.Length = 0 Then
                EP.SetError(CMBGODOWNTO, " Please Fill Godown / Jobber Name ")
                bln = False
            End If

            If CMBGODOWNTO.Text.Trim.Length > 0 And CMBJOBBERTO.Text.Trim.Length > 0 Then
                EP.SetError(CMBGODOWNTO, " Please Fill Either Godown / Jobber Name, Both are not allowed ")
                bln = False
            End If

            If CMBJOBBERFROM.Text.Trim.Length > 0 Then
                If CMBJOBBERFROM.Text.Trim = CMBJOBBERTO.Text.Trim Then
                    EP.SetError(CMBJOBBERFROM, " Jobber Name cannot be same ")
                    bln = False
                End If
            End If

            If CMBGODOWNFROM.Text.Trim.Length > 0 Then
                If CMBGODOWNFROM.Text.Trim = CMBGODOWNTO.Text.Trim Then
                    EP.SetError(CMBGODOWNFROM, " Godown cannot be same ")
                    bln = False
                End If
            End If

            If Val(TXTQTY.Text.Trim) = 0 Then
                EP.SetError(TXTQTY, " Please Fill Quantity ")
                bln = False
            Else
                If Not CHECKQTY() Then bln = False
            End If

            Return bln
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub cmbtrans_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBTRANS.Enter
        Try
            If CMBTRANS.Text.Trim = "" Then fillname(CMBTRANS, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBTRANS.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBTRANS.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBTRANS.Validating
        Try
            If CMBTRANS.Text.Trim <> "" Then namevalidate(CMBTRANS, cmbcode, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT' ", "Sundry Creditors", "TRANSPORT")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBJOBBERTO_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBJOBBERTO.Enter
        Try
            If CMBJOBBERTO.Text.Trim = "" Then fillname(CMBJOBBERTO, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'WARPER' OR LEDGERS.ACC_SUBTYPE = 'SIZER' OR LEDGERS.ACC_SUBTYPE = 'WEAVER')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBJOBBERTO_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBJOBBERTO.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'WARPER' OR LEDGERS.ACC_SUBTYPE = 'SIZER' OR LEDGERS.ACC_SUBTYPE = 'WEAVER')"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBJOBBERTO.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBJOBBERTO_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBJOBBERTO.Validating
        Try
            If CMBJOBBERTO.Text.Trim = "" Then fillname(CMBJOBBERTO, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'WARPER' OR LEDGERS.ACC_SUBTYPE = 'SIZER' OR LEDGERS.ACC_SUBTYPE = 'WEAVER')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBJOBBERFROM_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBJOBBERFROM.Enter
        Try
            If CMBJOBBERFROM.Text.Trim = "" Then fillname(CMBJOBBERFROM, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'WARPER' OR LEDGERS.ACC_SUBTYPE = 'SIZER' OR LEDGERS.ACC_SUBTYPE = 'WEAVER')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBJOBBERFROM_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBJOBBERFROM.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'WARPER' OR LEDGERS.ACC_SUBTYPE = 'SIZER' OR LEDGERS.ACC_SUBTYPE = 'WEAVER')"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBJOBBERFROM.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBJOBBERFROM_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBJOBBERFROM.Validating
        Try
            If CMBJOBBERFROM.Text.Trim <> "" Then namevalidate(CMBJOBBERFROM, cmbcode, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'WARPER' OR LEDGERS.ACC_SUBTYPE = 'SIZER' OR LEDGERS.ACC_SUBTYPE = 'WEAVER')", "Sundry Creditors", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
LINE1:
            TEMPTRANSFERNO = Val(TXTTRANSFERNO.Text) - 1
Line2:
            If TEMPTRANSFERNO > 0 Then

                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" PIPE_NO ", "", " PIPETRANSFER ", " AND PIPE_NO = '" & TEMPTRANSFERNO & "' AND PIPETRANSFER.PIPE_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    EDIT = True
                    PipeTRANSFER_Load(sender, e)
                Else
                    TEMPTRANSFERNO = Val(TEMPTRANSFERNO - 1)
                    GoTo Line2
                End If
            Else
                clear()
                EDIT = False
            End If

            If Val(TXTQTY.Text.Trim) = 0 And TEMPTRANSFERNO > 1 Then
                TXTTRANSFERNO.Text = TEMPTRANSFERNO
                GoTo LINE1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
LINE1:
            TEMPTRANSFERNO = Val(TXTTRANSFERNO.Text) + 1
            getmax_BILL_no()
            Dim MAXNO As Integer = TXTTRANSFERNO.Text.Trim
            clear()
            If Val(TXTTRANSFERNO.Text) - 1 >= TEMPTRANSFERNO Then
                EDIT = True
                PipeTRANSFER_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
            If Val(TXTQTY.Text.Trim) = 0 And TEMPTRANSFERNO < MAXNO Then
                TXTTRANSFERNO.Text = TEMPTRANSFERNO
                GoTo LINE1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                TEMPTRANSFERNO = Val(tstxtbillno.Text)
                If TEMPTRANSFERNO > 0 Then
                    EDIT = True
                    PipeTRANSFER_Load(sender, e)
                Else
                    clear()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Dim OBJDTLS As New PipeTransferDetails
            OBJDTLS.MdiParent = MDIMain
            OBJDTLS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Call cmdok_Click(sender, e)
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Try
            If EDIT = True Then
                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If MsgBox("Delete Entry?", MsgBoxStyle.YesNo) = vbYes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(TXTTRANSFERNO.Text.Trim)
                    alParaval.Add(YearId)

                    Dim ClsDO As New ClsPipeTransfer
                    ClsDO.alParaval = alParaval
                    Dim IntResult As Integer = ClsDO.DELETE()
                    MsgBox("Entry Deleted Successfully")
                    clear()
                    EDIT = False
                End If
            Else
                MsgBox("Delete is only in Edit Mode")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tooldelete.Click
        Call cmddelete_Click(sender, e)
    End Sub

    Private Sub CMBGODOWNFROM_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBGODOWNFROM.Enter
        Try
            If CMBGODOWNFROM.Text.Trim = "" Then fillGODOWN(CMBGODOWNFROM, EDIT, " AND GODOWN_ISOUR = 'True'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWNTO_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBGODOWNTO.Enter
        Try
            If CMBGODOWNTO.Text.Trim = "" Then fillGODOWN(CMBGODOWNTO, EDIT, " AND GODOWN_ISOUR = 'True'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWNFROM_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBGODOWNFROM.Validating
        Try
            If CMBGODOWNFROM.Text.Trim <> "" Then GODOWNVALIDATE(CMBGODOWNFROM, e, Me, " AND GODOWN_ISOUR = 'True'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWNTO_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBGODOWNTO.Validating
        Try
            If CMBGODOWNTO.Text.Trim <> "" Then GODOWNVALIDATE(CMBGODOWNTO, e, Me, " AND GODOWN_ISOUR = 'True'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWNFROM_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBGODOWNFROM.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJGODOWN As New SelectGodown
                OBJGODOWN.FRMSTRING = "GODOWN"
                OBJGODOWN.SEARCH = " AND GODOWN_ISOUR = 'True'"
                OBJGODOWN.ShowDialog()
                If OBJGODOWN.TEMPNAME <> "" Then CMBGODOWNFROM.Text = OBJGODOWN.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWNTO_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBGODOWNTO.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJGODOWN As New SelectGodown
                OBJGODOWN.FRMSTRING = "GODOWN"
                OBJGODOWN.SEARCH = " AND GODOWN_ISOUR = 'True'"
                OBJGODOWN.ShowDialog()
                If OBJGODOWN.TEMPNAME <> "" Then CMBGODOWNTO.Text = OBJGODOWN.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TRANSFERDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TRANSFERDATE.GotFocus
        Try
            TRANSFERDATE.SelectAll()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TRANSFERDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TRANSFERDATE.Validating
        Try
            If TRANSFERDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(TRANSFERDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHALLANDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CHALLANDATE.GotFocus
        Try
            CHALLANDATE.SelectAll()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHALLANDATE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CHALLANDATE.Validating
        Try
            If CHALLANDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(CHALLANDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTQTY_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTQTY.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTQTY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTQTY.Validating
        Try
            If CMBGODOWNFROM.Text.Length = 0 And CMBJOBBERFROM.Text.Length = 0 And Val(TXTQTY.Text.Trim) > 0 Then
                MsgBox("Please Fill Either From Godown / Jobber Name", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If CMBGODOWNTO.Text.Length = 0 And CMBJOBBERTO.Text.Length = 0 And Val(TXTQTY.Text.Trim) > 0 Then
                MsgBox("Please Fill Either To Godown / Jobber Name", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If (CMBJOBBERFROM.Text.Trim.Length <> 0 Or CMBGODOWNFROM.Text.Trim.Length <> 0) And (CMBJOBBERTO.Text.Trim.Length <> 0 Or CMBGODOWNTO.Text.Trim.Length <> 0) And Val(TXTQTY.Text.Trim) > 0 Then

                If CMBGODOWNFROM.Text.Trim.Length > 0 And CMBJOBBERFROM.Text.Trim.Length > 0 Then
                    MsgBox("Please Fill Either From Godown / Jobber Name, Both are not allowed", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                If CMBGODOWNTO.Text.Trim.Length > 0 And CMBJOBBERTO.Text.Trim.Length > 0 Then
                    MsgBox("Please Fill Either To Godown / Jobber Name, Both are not allowed", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                If CMBGODOWNFROM.Text.Length <> 0 Then
                    If CMBGODOWNFROM.Text.Trim = CMBGODOWNTO.Text.Trim Then
                        MsgBox("From Godown and To Godown cannot be same", MsgBoxStyle.Critical)
                        Exit Sub
                    End If
                End If
                
                If CMBJOBBERFROM.Text.Length <> 0 Then
                    If CMBJOBBERFROM.Text.Trim = CMBJOBBERTO.Text.Trim Then
                        MsgBox("From Jobber and To Jobber cannot be same", MsgBoxStyle.Critical)
                        Exit Sub
                    End If
                End If

                If Not CHECKQTY() Then
                    Exit Sub
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function CHECKQTY() As Boolean
        Try
            Dim BLN As Boolean = True
            Dim STOCK As Double = 0
            Dim OBJCMN As New ClsCommonMaster
            Dim dt As DataTable

            'STOCK VALIDATION FOR JOBBER
            If CMBJOBBERFROM.Text.Trim <> "" Then
                STOCK = 0
                dt = OBJCMN.search(" ISNULL(SUM(PIPESTOCK.QTY),0) AS QTY ", "", " PIPESTOCK ", " AND PIPESTOCK.NAME = '" & CMBJOBBERFROM.Text.Trim & "' AND PIPESTOCK.CMPID = " & CmpId & " AND PIPESTOCK.YEARID = " & YearId)
                If dt.Rows.Count > 0 Then STOCK = Val(dt.Rows(0).Item("QTY"))

                'IF ENTRY IS IN EDIT MODE ADD ALREADY SAVED(SUBTRACTED) QTY AND THEN CHECK...
                If EDIT = True Then
                    Dim OBJtransfer As New ClsPipeTransfer
                    Dim ALPARAVAL As New ArrayList
                    ALPARAVAL.Add(TEMPTRANSFERNO)
                    ALPARAVAL.Add(YearId)
                    OBJtransfer.alParaval = ALPARAVAL
                    Dim dttable As DataTable = OBJtransfer.SELECTTRANSFER()
                    If dttable.Rows.Count > 0 Then STOCK += Val(dttable.Rows(0).Item("QTY"))
                Else
                    If STOCK = 0 Then
                        MsgBox("Stock not Found")
                        BLN = False
                    End If
                End If
            End If


            'STOCK VALIDATION FOR GODOWN
            If CMBGODOWNFROM.Text.Trim <> "" Then
                STOCK = 0
                dt = OBJCMN.search(" ISNULL(SUM(PIPESTOCK_OURODOWN.QTY),0) as QTY ", "", " PIPESTOCK_OURODOWN ", " AND PIPESTOCK_OURODOWN.GODOWN = '" & CMBGODOWNFROM.Text.Trim & "' AND PIPESTOCK_OURODOWN.CMPID = " & CmpId & " AND PIPESTOCK_OURODOWN.YEARID = " & YearId)
                If dt.Rows.Count > 0 Then STOCK = Val(dt.Rows(0).Item("QTY"))

                'IF ENTRY IS IN EDIT MODE ADD ALREADY SAVED(SUBTRACTED) QTY AND THEN CHECK...
                If EDIT = True Then
                    Dim OBJtransfer As New ClsPipeTransfer
                    Dim ALPARAVAL As New ArrayList
                    ALPARAVAL.Add(TEMPTRANSFERNO)
                    ALPARAVAL.Add(YearId)
                    OBJtransfer.alParaval = ALPARAVAL
                    Dim dttable As DataTable = OBJtransfer.SELECTTRANSFER()
                    If dttable.Rows.Count > 0 Then STOCK += Val(dttable.Rows(0).Item("QTY"))
                Else
                    If STOCK = 0 Then
                        MsgBox("Stock not Found")
                        BLN = False
                    End If
                End If
            End If

            If Val(TXTQTY.Text.Trim) > STOCK Then
                MsgBox("Quantity cannot be more than stock quantity")
                BLN = False
            End If

            Return BLN
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class