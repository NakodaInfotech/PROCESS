
Imports BL

Public Class ProgramMaster

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public edit As Boolean
    Public TEMPPROGRAMNO, TEMPPROGRAMSRNO As String

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub CALC()
        TXTTOTALENDS.Text = Format(Val(TXTENDS.Text.Trim) * Val(TXTROLLS.Text.Trim))
    End Sub

    Private Sub TXTENDS_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTENDS.Validating
        CALC()
    End Sub

    Private Sub TXTROLLS_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTROLLS.Validating
        CALC()
    End Sub

    Private Sub TXTENDS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTENDS.KeyPress, TXTROLLS.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTLENGTH_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTLENGTH.KeyPress
        numkeypress(e, TXTLENGTH, Me)
    End Sub

    Sub GETMAX_PROGRAM_NO()
        Dim DTTABLE As DataTable = getmax("ISNULL(MAX(PROGRAM_NO),0)+ 1", "PROGRAMMASTER", "")
        If DTTABLE.Rows.Count > 0 Then TXTPROGNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Sub GETMAX_PROGRAM_SRNO()
        Dim DTTABLE As DataTable = getmax("ISNULL(MAX(PROGRAM_SRNO),0)+ 1", "PROGRAMMASTER", " AND PROGRAM_YEARID = " & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTSRNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try

            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim IntResult As Integer
            Dim alParaval As New ArrayList

            alParaval.Add(Format(Convert.ToDateTime(PROGRAMDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(Val(TXTENDS.Text.Trim))
            alParaval.Add(Val(TXTROLLS.Text.Trim))
            alParaval.Add(Val(TXTLENGTH.Text.Trim))
            alParaval.Add(Val(TXTTOTALENDS.Text.Trim))
            alParaval.Add(CMBQUALITY.Text.Trim)
            alParaval.Add(CMBMILL.Text.Trim)
            alParaval.Add(CMBNAME.Text.Trim)
            alParaval.Add(TXTGIVENBY.Text.Trim)
            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)

            Dim OBJPROGRAM As New ClsProgramMaster
            OBJPROGRAM.alParaval = alParaval

            If edit = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DT As DataTable = OBJPROGRAM.SAVE()
                MsgBox("Details Added")
                TEMPPROGRAMNO = DT.Rows(0).Item(0)

            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPPROGRAMSRNO)
                alParaval.Add(YearId)
                IntResult = OBJPROGRAM.Update()
                MsgBox("Details Updated")
                edit = False

            End If
            PRINTREPORT(TEMPPROGRAMNO)
            clear()
            PROGRAMDATE.Focus()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try

    End Sub

    Sub PRINTREPORT(ByVal PROGRAMNO As Integer)
        Try
            If MsgBox("Wish to Print Program?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim OBJDO As New ProgramDesign
                OBJDO.MdiParent = MDIMain
                OBJDO.WHERECLAUSE = "{PROGRAMMASTER.PROGRAM_SRNO}=" & Val(TEMPPROGRAMSRNO)
                OBJDO.FRMSTRING = "PROGRAMREPORT"
                OBJDO.PROGRAMNO = PROGRAMNO
                OBJDO.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True

        If PROGRAMDATE.Text = "__/__/____" Then
            EP.SetError(PROGRAMDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(PROGRAMDATE.Text) Then
                EP.SetError(PROGRAMDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        If Val(TXTENDS.Text.Trim) = 0 Then
            EP.SetError(TXTENDS, "Enter Ends")
            bln = False
        End If

        If Val(TXTROLLS.Text.Trim) = 0 Then
            EP.SetError(TXTROLLS, "Enter Rolls")
            bln = False
        End If

        If Val(TXTLENGTH.Text.Trim) = 0 Then
            EP.SetError(TXTLENGTH, "Enter Length")
            bln = False
        End If

        If CMBQUALITY.Text.Trim.Length = 0 Then
            EP.SetError(CMBQUALITY, "Enter Quality Name")
            bln = False
        End If

        If TXTGIVENBY.Text.Trim.Length = 0 Then
            EP.SetError(TXTGIVENBY, "Enter Given By")
            bln = False
        End If

        'LOCKED OPEN AS PER REQUIREMENT OF JASHOK
        'If lbllocked.Visible = True Then
        '    EP.SetError(lbllocked, "Unable to Mpdify, Program Already Used")
        '    bln = False
        'End If

        Return bln
    End Function

    Sub clear()
        Try
            GETMAX_PROGRAM_SRNO()
            GETMAX_PROGRAM_NO()
            lbllocked.Visible = False
            PBlock.Visible = False
            PROGRAMDATE.Text = Mydate
            TXTENDS.Clear()
            TXTROLLS.Clear()
            TXTLENGTH.Clear()
            TXTTOTALENDS.Clear()
            TXTGIVENBY.Text = UserName
            CMBQUALITY.Text = ""
            CMBMILL.Text = ""
            CMBNAME.Text = ""
            txtremarks.Clear()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ProgramMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.KeyCode = Keys.OemQuotes Then
            e.SuppressKeyPress = True
        ElseIf e.KeyCode = Keys.Left And e.Alt = True Then
            Call toolprevious_Click(sender, e)
        ElseIf e.KeyCode = Keys.Right And e.Alt = True Then
            Call toolnext_Click(sender, e)
        ElseIf e.KeyCode = Keys.P And e.Alt = True Then
            Call PrintToolStripButton_Click(sender, e)
        End If
    End Sub

    Private Sub ProgramMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'MFG'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            fillQUALITY(CMBQUALITY, edit)

            If ClientName = "SASHWINKUMAR" Then fillname(CMBNAME, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'WARPER' OR LEDGERS.ACC_SUBTYPE = 'SIZER')") Else fillname(CMBNAME, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WARPER'")
            fillname(CMBMILL, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'MILL'")
            clear()

            If edit = True Then
                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If


                Dim objCommon As New ClsCommonMaster
                Dim dttable As DataTable = objCommon.search(" PROGRAM_NO AS PROGRAMNO,PROGRAM_SRNO AS PROGRAMSRNO, PROGRAM_DATE AS DATE, PROGRAM_ENDS AS ENDS, PROGRAM_ROLLS AS ROLLS, PROGRAM_LENGTH AS LENGTH, PROGRAM_TOTALENDS AS TOTALENDS, QUALITY_NAME AS QUALITY, PROGRAM_GIVENBY AS GIVENBY, PROGRAM_REMARKS AS REMARKS, PROGRAM_DONE AS DONE, ISNULL(MILLLEDGERS.ACC_CMPNAME ,'') AS MILLNAME, ISNULL(WARPERLEDGERS.ACC_CMPNAME ,'') AS WARPERNAME   ", "", " PROGRAMMASTER INNER JOIN QUALITYMASTER ON PROGRAM_QUALITYID = QUALITY_ID LEFT OUTER JOIN LEDGERS AS MILLLEDGERS ON PROGRAM_MILLID = MILLLEDGERS.ACC_ID LEFT OUTER JOIN LEDGERS AS WARPERLEDGERS ON PROGRAM_LEDGERID = WARPERLEDGERS.ACC_ID ", " and PROGRAM_SRNO = " & TEMPPROGRAMSRNO & " AND PROGRAM_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then

                    TXTPROGNO.Text = dttable.Rows(0).Item("PROGRAMNO")
                    TXTSRNO.Text = dttable.Rows(0).Item("PROGRAMSRNO")
                    PROGRAMDATE.Text = dttable.Rows(0).Item("DATE")
                    TXTENDS.Text = Val(dttable.Rows(0).Item("ENDS"))
                    TXTROLLS.Text = Val(dttable.Rows(0).Item("ROLLS"))
                    TXTLENGTH.Text = Val(dttable.Rows(0).Item("LENGTH"))
                    TXTTOTALENDS.Text = Val(dttable.Rows(0).Item("TOTALENDS"))
                    CMBQUALITY.Text = dttable.Rows(0).Item("YARNQUALITY").ToString
                    CMBMILL.Text = dttable.Rows(0).Item("MILLNAME").ToString
                    CMBNAME.Text = dttable.Rows(0).Item("JOBBERNAME").ToString
                    TXTGIVENBY.Text = dttable.Rows(0).Item("GIVENBY").ToString
                    txtremarks.Text = dttable.Rows(0).Item("REMARKS").ToString

                    CALC()
                    If Convert.ToBoolean(dttable.Rows(0).Item("DONE")) = True Then
                        lbllocked.Visible = True
                        PBlock.Visible = True
                    End If

                End If
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try

    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Try
            If USERDELETE = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If edit = True Then

                If lbllocked.Visible = True Then
                    MsgBox("Unable to Modify, Program Already Used", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                Dim intresult As Integer
                Dim objcls As New ClsProgramMaster
                Dim TEMPMSG As Integer = MsgBox("Wish To Delete?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbYes Then

                    Dim alParaval As New ArrayList
                    alParaval.Add(TEMPPROGRAMSRNO)
                    alParaval.Add(YearId)
                    objcls.alParaval = alParaval
                    intresult = objcls.DELETE()
                    MsgBox("Program Delete Successfully")
                    clear()
                    edit = False
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PROGRAMDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles PROGRAMDATE.GotFocus
        PROGRAMDATE.SelectAll()
    End Sub

    Private Sub CMBQUALITY_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBQUALITY.Enter
        Try
            If CMBQUALITY.Text.Trim = "" Then fillQUALITY(CMBQUALITY, edit)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBQUALITY.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJROLLS As New SelectQuality
                OBJROLLS.ShowDialog()
                If OBJROLLS.TEMPNAME <> "" Then CMBQUALITY.Text = OBJROLLS.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBQUALITY.Validating
        Try
            If CMBQUALITY.Text.Trim <> "" Then QUALITYVALIDATE(CMBQUALITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PROGRAMDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles PROGRAMDATE.Validating
        Try
            If PROGRAMDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(PROGRAMDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILL_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBMILL.Enter
        Try
            If CMBMILL.Text.Trim = "" Then fillname(CMBMILL, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'MILL'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILL_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBMILL.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'MILL'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBMILL.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILL_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBMILL.Validating
        Try
            If CMBMILL.Text.Trim <> "" Then namevalidate(CMBMILL, cmbcode, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'MILL'", "Sundry Creditors", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then
                If ClientName = "JASHOK" Then fillname(CMBNAME, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WARPER'") Else fillname(CMBNAME, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'WARPER' OR LEDGERS.ACC_SUBTYPE = 'SIZER')")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBNAME.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                If ClientName = "JASHOK" Then OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WARPER' " Else OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'WARPER' OR LEDGERS.ACC_SUBTYPE = 'SIZER')"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then
                If ClientName = "JASHOK" Then namevalidate(CMBNAME, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WARPER'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "WARPER") Else namevalidate(CMBNAME, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'WARPER' OR LEDGERS.ACC_SUBTYPE = 'SIZER')", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "WARPER")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDPRINT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDPRINT.Click
        Try
            If edit = True Then PRINTREPORT(TEMPPROGRAMNO)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
LINE1:
            TEMPPROGRAMSRNO = Val(TXTSRNO.Text) - 1
Line2:
            If TEMPPROGRAMSRNO > 0 Then

                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" PROGRAM_SRNO ", "", "  PROGRAMMASTER ", " AND PROGRAM_SRNO = " & TEMPPROGRAMSRNO & " AND PROGRAM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    edit = True
                    ProgramMaster_Load(sender, e)
                Else
                    TEMPPROGRAMSRNO = Val(TEMPPROGRAMSRNO - 1)
                    GoTo Line2
                End If
            Else
                clear()
                edit = False
            End If

            If CMBQUALITY.Text = "" And TEMPPROGRAMSRNO > 1 Then
                TXTSRNO.Text = TEMPPROGRAMSRNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
LINE1:
            TEMPPROGRAMSRNO = Val(TXTSRNO.Text) + 1
            GETMAX_PROGRAM_SRNO()
            Dim MAXNO As Integer = TXTSRNO.Text.Trim
            clear()
            If Val(TXTSRNO.Text) - 1 >= TEMPPROGRAMSRNO Then
                edit = True
                ProgramMaster_Load(sender, e)
            Else
                clear()
                edit = False
            End If
            If CMBQUALITY.Text.Trim = "" And TEMPPROGRAMSRNO < MAXNO Then
                TXTSRNO.Text = TEMPPROGRAMSRNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Call CMDPRINT_Click(sender, e)
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Call cmdok_Click(sender, e)
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tooldelete.Click
        Call cmddelete_Click(sender, e)
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim OBJPROGRAM As New ProgramDetails
            OBJPROGRAM.MdiParent = MDIMain
            OBJPROGRAM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tstxtbillno.Validated
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                TEMPPROGRAMSRNO = Val(tstxtbillno.Text)
                If TEMPPROGRAMSRNO > 0 Then
                    edit = True
                    ProgramMaster_Load(sender, e)
                Else
                    clear()
                    edit = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ProgramMaster_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If ALLOWMFG = False Then Exit Sub
    End Sub
End Class