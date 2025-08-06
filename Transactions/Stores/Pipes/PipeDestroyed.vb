
Imports BL

Public Class PipeDestroyed

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public edit As Boolean
    Public TEMPDESTROYEDNO As Integer

    Sub CLEAR()
        Try
            EP.Clear()
            tstxtbillno.Clear()
            DESTROYEDDATE.Text = Mydate
            CMBJOBBERNAME.Text = ""
            CMBOURGODOWN.Text = ""
            TXTOPSTOCKNO.Clear()
            TXTQTY.Clear()
            TXTREMARKS.Clear()
            TXTDESTROYEDNO.Clear()
            GETMAX_DESTROYED_NO()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETMAX_DESTROYED_NO()
        Try
            Dim DTTABLE As DataTable = getmax(" isnull(max(PIPE_NO),0) + 1 ", "  PIPEDESTROYED ", " AND PIPE_YEARID = " & YearId)
            If DTTABLE.Rows.Count > 0 Then TXTDESTROYEDNO.Text = DTTABLE.Rows(0).Item(0)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PipeDestroyed_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then
                Me.Close()
            ElseIf e.KeyCode = Keys.OemQuotes Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        Try
            fillGODOWN(CMBOURGODOWN, edit, "AND GODOWN_ISOUR='TRUE'")
            fillname(CMBJOBBERNAME, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS' AND (ACC_SUBTYPE = 'SIZER' OR ACC_SUBTYPE = 'WEAVER') ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTQTY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTQTY.Validating
        Try
            If CMBOURGODOWN.Text.Trim = "" And CMBJOBBERNAME.Text.Trim = "" And Val(TXTQTY.Text.Trim) > 0 Then
                MsgBox("Please Fill Godown / Jobber Name", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If CMBOURGODOWN.Text.Trim.Length = 0 And CMBJOBBERNAME.Text.Trim.Length = 0 Then
                MsgBox(" Please Fill Either Godown / Jobber Name ")
                Exit Sub
            End If

            If (CMBJOBBERNAME.Text.Trim.Length <> 0 Or CMBOURGODOWN.Text.Trim.Length <> 0) And Val(TXTQTY.Text.Trim) > 0 Then
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
            Dim BALANCESTOCK As Double = 0
            Dim OBJCMN As New ClsCommonMaster
            Dim dt As DataTable

            'STOCK VALIDATION FOR JOBBER
            If CMBJOBBERNAME.Text.Trim <> "" Then
                BALANCESTOCK = 0
                dt = OBJCMN.search(" ISNULL(SUM(PIPESTOCK.QTY),0) AS QTY ", "", " PIPESTOCK ", " AND PIPESTOCK.NAME = '" & CMBJOBBERNAME.Text.Trim & "' AND PIPESTOCK.CMPID = " & CmpId & " AND PIPESTOCK.YEARID = " & YearId)
                If dt.Rows.Count > 0 Then BALANCESTOCK = Val(dt.Rows(0).Item("QTY"))

                'IF ENTRY IS IN EDIT MODE ADD ALREADY SAVED(SUBTRACTED) QTY AND THEN CHECK...
                If edit = True Then
                    Dim OBJDESTROYED As New ClsPipeDestroyed
                    Dim ALPARAVAL As New ArrayList
                    ALPARAVAL.Add(TEMPDESTROYEDNO)
                    ALPARAVAL.Add(YearId)
                    OBJDESTROYED.alParaval = ALPARAVAL
                    Dim dttable As DataTable = OBJDESTROYED.SELECTDESTROYED()
                    If dttable.Rows.Count > 0 Then BALANCESTOCK += Val(dttable.Rows(0).Item("QTY"))
                Else
                    If BALANCESTOCK = 0 Then
                        MsgBox("Stock not Found")
                        BLN = False
                        Exit Function
                    End If
                End If
            End If

            'STOCK VALIDATION FOR GODOWN
            If CMBOURGODOWN.Text.Trim <> "" Then
                BALANCESTOCK = 0
                dt = OBJCMN.search(" ISNULL(SUM(PIPESTOCK_OURODOWN.QTY),0) as QTY ", "", " PIPESTOCK_OURODOWN ", " AND PIPESTOCK_OURODOWN.GODOWN = '" & CMBOURGODOWN.Text.Trim & "' AND PIPESTOCK_OURODOWN.CMPID = " & CmpId & " AND PIPESTOCK_OURODOWN.YEARID = " & YearId)
                If dt.Rows.Count > 0 Then BALANCESTOCK = Val(dt.Rows(0).Item("QTY"))

                'IF ENTRY IS IN EDIT MODE ADD ALREADY SAVED(SUBTRACTED) QTY AND THEN CHECK...
                If edit = True Then
                    Dim OBJDESTROYED As New ClsPipeDestroyed
                    Dim ALPARAVAL As New ArrayList
                    ALPARAVAL.Add(TEMPDESTROYEDNO)
                    ALPARAVAL.Add(YearId)
                    OBJDESTROYED.alParaval = ALPARAVAL
                    Dim dttable As DataTable = OBJDESTROYED.SELECTDESTROYED()
                    If dttable.Rows.Count > 0 Then BALANCESTOCK += Val(dttable.Rows(0).Item("QTY"))
                Else
                    If BALANCESTOCK = 0 Then
                        MsgBox("Stock not Found")
                        BLN = False
                        Exit Function
                    End If
                End If
            End If

            If Val(TXTQTY.Text.Trim) > BALANCESTOCK Then
                MsgBox("Quantity cannot be more than stock quantity")
                BLN = False
            End If

            Return BLN
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub PipeDestroyed_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'OPENING'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor

            FILLCMB()
            CLEAR()

            If edit = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim OBJDESTROYED As New ClsPipeDestroyed
                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(TEMPDESTROYEDNO)
                ALPARAVAL.Add(YearId)
                OBJDESTROYED.alParaval = ALPARAVAL
                Dim dttable As DataTable = OBJDESTROYED.SELECTDESTROYED()

                If dttable.Rows.Count > 0 Then
                    For Each dr As DataRow In dttable.Rows

                        TXTDESTROYEDNO.Text = TEMPDESTROYEDNO
                        DESTROYEDDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")

                        CMBJOBBERNAME.Text = Convert.ToString(dr("NAME").ToString)
                        CMBOURGODOWN.Text = dr("GODOWNNAME").ToString
                        TXTQTY.Text = Val(dr("QTY").ToString)
                        TXTREMARKS.Text = Convert.ToString(dr("REMARKS").ToString)
                    Next
                Else
                    edit = False
                    CLEAR()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBJOBBERNAME.Enter
        Try
            If CMBJOBBERNAME.Text.Trim = "" Then fillname(CMBJOBBERNAME, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS' and (LEDGERS.ACC_SUBTYPE = 'SIZER' OR  LEDGERS.ACC_SUBTYPE = 'WEAVER')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBJOBBERNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' and (LEDGERS.ACC_SUBTYPE = 'SIZER' OR  LEDGERS.ACC_SUBTYPE = 'WEAVER')"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then cmbcode.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then CMBJOBBERNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBJOBBERNAME.Validating
        Try
            If CMBJOBBERNAME.Text.Trim <> "" Then namevalidate(CMBJOBBERNAME, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' and (LEDGERS.ACC_SUBTYPE = 'SIZER' OR  LEDGERS.ACC_SUBTYPE = 'WEAVER')", "SUNDRY CREDITORS", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBOURGODOWN_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBOURGODOWN.Enter
        Try
            If CMBOURGODOWN.Text.Trim = "" Then fillGODOWN(CMBOURGODOWN, edit, "AND GODOWN_ISOUR='TRUE'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBOURGODOWN_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBOURGODOWN.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJGODOWN As New SelectGodown
                OBJGODOWN.FRMSTRING = "GODOWN"
                OBJGODOWN.SEARCH = "AND GODOWN_ISOUR='TRUE'"
                OBJGODOWN.ShowDialog()
                If OBJGODOWN.TEMPNAME <> "" Then CMBOURGODOWN.Text = OBJGODOWN.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBOURGODOWN_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBOURGODOWN.Validating
        Try
            If CMBOURGODOWN.Text.Trim <> "" Then GODOWNVALIDATE(CMBOURGODOWN, e, Me, "AND GODOWN_ISOUR='TRUE'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTQTY_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTQTY.KeyPress
        numkeypress(e, TXTQTY, Me)
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True

        If DESTROYEDDATE.Text = "__/__/____" Then
            EP.SetError(DESTROYEDDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(DESTROYEDDATE.Text) Then
                EP.SetError(DESTROYEDDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        If CMBOURGODOWN.Text.Trim.Length = 0 And CMBJOBBERNAME.Text.Trim.Length = 0 Then
            EP.SetError(CMBOURGODOWN, " Please Fill Either Godown / Jobber Name ")
            bln = False
        End If

        If Val(TXTQTY.Text.Trim) = 0 Then
            EP.SetError(TXTQTY, " Please Fill Quantity ")
            bln = False
        Else
            If Not CHECKQTY() Then bln = False
        End If

        Return bln
    End Function

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        Dim IntResult As Integer
        Try
            Cursor.Current = Cursors.WaitCursor
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList

            alParaval.Add(Format(Convert.ToDateTime(DESTROYEDDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBJOBBERNAME.Text.Trim)
            alParaval.Add(CMBOURGODOWN.Text.Trim)
            alParaval.Add(Val(TXTQTY.Text.Trim))
            alParaval.Add(TXTREMARKS.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)

            Dim OBJDEST As New ClsPipeDestroyed
            OBJDEST.alParaval = alParaval
            If edit = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DTTABLE As DataTable = OBJDEST.SAVE()
                TEMPDESTROYEDNO = DTTABLE.Rows(0).Item(0)
                MessageBox.Show("Details Added")

            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPDESTROYEDNO)
                IntResult = OBJDEST.UPDATE()
                MessageBox.Show("Details Updated")
                edit = False
            End If

            'CLEAR()
            'SHOW NEXT BILL ON EDIT MODE DONT CLEAR
            Call toolnext_Click(sender, e)
            DESTROYEDDATE.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDCLEAR.Click
        Try
            CLEAR()
            edit = False
            DESTROYEDDATE.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDDELETE.Click
        Try
            If edit = True Then
                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If MsgBox("Delete Entry?", MsgBoxStyle.YesNo) = vbYes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(TEMPDESTROYEDNO)
                    alParaval.Add(YearId)

                    Dim ClsDO As New ClsPipeDestroyed
                    ClsDO.alParaval = alParaval
                    Dim IntResult As Integer = ClsDO.DELETE()
                    MsgBox("Entry Deleted Successfully")
                    CLEAR()
                    edit = False
                End If
            Else
                MsgBox("Delete is only in Edit Mode")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DESTROYEDDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DESTROYEDDATE.GotFocus
        Try
            DESTROYEDDATE.SelectAll()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DESTROYEDDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DESTROYEDDATE.Validating
        Try
            If DESTROYEDDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(DESTROYEDDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                TEMPDESTROYEDNO = Val(tstxtbillno.Text)
                If TEMPDESTROYEDNO > 0 Then
                    edit = True
                    PipeDestroyed_Load(sender, e)
                Else
                    CLEAR()
                    edit = False
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
LINE1:
            TEMPDESTROYEDNO = Val(TXTDESTROYEDNO.Text) - 1
Line2:
            If TEMPDESTROYEDNO > 0 Then

                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" PIPE_NO ", "", " PIPEDESTROYED ", " AND PIPE_NO = '" & TEMPDESTROYEDNO & "' AND PIPEDESTROYED.PIPE_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    edit = True
                    PipeDestroyed_Load(sender, e)
                Else
                    TEMPDESTROYEDNO = Val(TEMPDESTROYEDNO - 1)
                    GoTo Line2
                End If
            Else
                CLEAR()
                edit = False
            End If

            If Val(TXTQTY.Text.Trim) = 0 And TEMPDESTROYEDNO > 1 Then
                TXTDESTROYEDNO.Text = TEMPDESTROYEDNO
                GoTo LINE1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
LINE1:
            TEMPDESTROYEDNO = Val(TXTDESTROYEDNO.Text) + 1
            GETMAX_DESTROYED_NO()
            Dim MAXNO As Integer = TXTDESTROYEDNO.Text.Trim
            CLEAR()
            If Val(TXTDESTROYEDNO.Text) - 1 >= TEMPDESTROYEDNO Then
                edit = True
                PipeDestroyed_Load(sender, e)
            Else
                CLEAR()
                edit = False
            End If
            If Val(TXTQTY.Text.Trim) = 0 And TEMPDESTROYEDNO < MAXNO Then
                TXTDESTROYEDNO.Text = TEMPDESTROYEDNO
                GoTo LINE1
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
            Dim OBJDTLS As New PipeDestroyedDetails
            OBJDTLS.MdiParent = MDIMain
            OBJDTLS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Call cmdok_Click(sender, e)
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tooldelete.Click
        Try
            If edit = True Then
                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If MsgBox("Delete Delivery Order?", MsgBoxStyle.YesNo) = vbYes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(TXTDESTROYEDNO.Text.Trim)
                    alParaval.Add(YearId)

                    Dim ClsDO As New ClsPipeDestroyed
                    ClsDO.alParaval = alParaval
                    Dim IntResult As Integer = ClsDO.DELETE()
                    MsgBox("Entry Deleted Successfully")
                    CLEAR()
                    edit = False
                End If
            Else
                MsgBox("Delete is only in Edit Mode")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class