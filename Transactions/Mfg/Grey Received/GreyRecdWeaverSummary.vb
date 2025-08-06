
Imports BL
Imports System.Windows.Forms
Imports System.IO

Public Class GreyRecdWeaverSummary

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean   'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public EDIT As Boolean
    Public TEMPGREYRECDSUMMNO As Integer

    Private Sub CMDEXIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub CMDCLEAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDCLEAR.Click
        CLEAR()
        EDIT = False
        CMBNAME.Focus()
    End Sub

    Sub TOTAL()
        Try
            LBLTOTALTAKA.Text = 0.0
            LBLTOTALMTRS.Text = 0.0
            LBLTOTALWT.Text = 0.0

            For Each ROW As DataGridViewRow In GRIDGREY.Rows
                If ROW.Cells(GSRNO.Index).Value <> Nothing Then
                    LBLTOTALTAKA.Text = Format(Val(LBLTOTALTAKA.Text) + Val(ROW.Cells(GTAKA.Index).EditedFormattedValue), "0.00")
                    LBLTOTALMTRS.Text = Format(Val(LBLTOTALMTRS.Text) + Val(ROW.Cells(GMTRS.Index).EditedFormattedValue), "0.000")
                    LBLTOTALWT.Text = Format(Val(LBLTOTALWT.Text) + Val(ROW.Cells(GQUALITYWT.Index).EditedFormattedValue), "0.000")
                End If
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()

        tstxtbillno.Clear()
        TXTGREYRECDSUMMNO.Clear()
        DTGREYRECDSUMMDATE.Text = Mydate
        CMBGODOWN.Text = GETDEFAULTGODOWN()
        CMBNAME.Text = ""
        CMBNAME.Enabled = True
        cmbtrans.Text = ""
        TXTCHALLANNO.Clear()

        TXTSRNO.Clear()
        CMBBEAMNO.Items.Clear()
        CMBBEAMNO.Text = ""
        TXTBEAMNAME.Clear()
        TXTBALANCECUT.Clear()
        TXTTAKA.Clear()
        CMBGREYQUALITY.Text = ""
        TXTMTRS.Clear()
        TXTQUALITYWT.Clear()
        TXTFROMNO.Clear()
        TXTFROMSRNO.Clear()
        TXTTYPE.Clear()

        EP.Clear()
        lbllocked.Visible = False
        PBlock.Visible = False
        GRIDGREY.RowCount = 0

        LBLTOTALTAKA.Text = 0
        LBLTOTALMTRS.Text = 0.0
        LBLTOTALWT.Text = 0.0

        TXTSRNO.Text = 1

        GETMAX_GREYRECDSUMM_NO()
        GRIDDOUBLECLICK = False

    End Sub

    Sub GETMAX_GREYRECDSUMM_NO()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax("ISNULL(MAX(GREYRECDSUMM_NO),0)+1", "GREYRECDWEAVERSUMMARY", "AND GREYRECDSUMM_YEARID=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTGREYRECDSUMMNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub GreyRecdFromWeaver_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If ERRORVALID() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then CMDSAVE_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.KeyCode = Keys.OemPipe Then
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
                'ElseIf e.KeyCode = Keys.P And e.Alt = True Then
                '    Call PrintToolStripButton_Click(sender, e)
            ElseIf e.KeyCode = Windows.Forms.Keys.F5 Then       'for grid foucs
                GRIDGREY.Focus()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.WaitCursor
        End Try
    End Sub

    Sub FILLCMB()
        If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'")
        If cmbtrans.Text.Trim = "" Then fillname(cmbtrans, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'TRANSPORT'")
        If CMBGODOWN.Text.Trim = "" Then fillGODOWN(CMBGODOWN, EDIT, " and GODOWN_ISOUR = 'True'")

        'DONE BY GULKIT.. DO NOT OPEN THIS CODE.. IT WILL GIVE ERROR ON ENTER EVENT OF CMBGREY
        'If CMBGREYQUALITY.Text.Trim = "" Then FILLGREY(CMBGREYQUALITY, EDIT)
    End Sub

    Private Sub GreyRecdWeaverSummary_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'MFG'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor

            FILLCMB()
            CLEAR()
            CMBGODOWN.Text = GETDEFAULTGODOWN()

            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim dttable As New DataTable
                Dim OBJGREYREC As New ClsGreyRecdWeaverSummary

                OBJGREYREC.alParaval.Add(TEMPGREYRECDSUMMNO)
                OBJGREYREC.alParaval.Add(YearId)
                dttable = OBJGREYREC.SELECTGREY()

                If dttable.Rows.Count > 0 Then
                    CMBNAME.Focus()
                    TXTGREYRECDSUMMNO.Text = TEMPGREYRECDSUMMNO
                    DTGREYRECDSUMMDATE.Text = dttable.Rows(0).Item("DATE")
                    CMBGODOWN.Text = dttable.Rows(0).Item("GODOWN").ToString
                    CMBNAME.Text = dttable.Rows(0).Item("NAME").ToString
                    cmbtrans.Text = dttable.Rows(0).Item("TRANSNAME").ToString
                    TXTCHALLANNO.Text = dttable.Rows(0).Item("CHALLANNO").ToString
                    TXTREMARKS.Text = dttable.Rows(0).Item("REMARKS").ToString

                    'ITEM GRID
                    For Each ROW As DataRow In dttable.Rows
                        GRIDGREY.Rows.Add(Val(ROW("SRNO")), ROW("BEAMNO"), ROW("BEAMNAME"), Val(ROW("BALANCECUT")), Format(Val(ROW("TAKA")), "0.00"), ROW("GREYQUALITY"), Format(Val(ROW("MTRS")), "0.000"), Format(Val(ROW("QUALITYWT")), "0.000"), ROW("FROMNO"), ROW("FROMSRNO"), ROW("TYPE"))
                    Next

                    TOTAL()
                    getsrno(GRIDGREY)

                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSAVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSAVE.Click
        Try
            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If

            Dim IntResult As Integer
            Dim alParaval As New ArrayList

            alParaval.Add(Format(Convert.ToDateTime(DTGREYRECDSUMMDATE.Text.Trim).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBGODOWN.Text.Trim)
            alParaval.Add(CMBNAME.Text.Trim)
            alParaval.Add(cmbtrans.Text.Trim)
            alParaval.Add(TXTCHALLANNO.Text.Trim)
            alParaval.Add(Val(LBLTOTALTAKA.Text.Trim))
            alParaval.Add(Val(LBLTOTALMTRS.Text.Trim))
            alParaval.Add(Val(LBLTOTALWT.Text.Trim))
            alParaval.Add(TXTREMARKS.Text.Trim)


            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)

            Dim SRNO As String = ""
            Dim BEAMNO As String = ""
            Dim BEAMNAME As String = ""
            Dim BALANCECUT As String = ""
            Dim TAKA As String = ""
            Dim GREYQUALITY As String = ""
            Dim MTRS As String = ""
            Dim QUALITYWT As String = ""
            Dim FROMNO As String = ""
            Dim FROMSRNO As String = ""
            Dim TYPE As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDGREY.Rows
                If row.Cells(GSRNO.Index).Value <> Nothing Then
                    If SRNO = "" Then
                        SRNO = Val(row.Cells(GSRNO.Index).Value)
                        BEAMNO = row.Cells(GBEAMNO.Index).Value.ToString
                        BEAMNAME = row.Cells(GBEAMNAME.Index).Value.ToString
                        BALANCECUT = Val(row.Cells(GBALANCECUT.Index).Value)
                        TAKA = Val(row.Cells(GTAKA.Index).Value)
                        GREYQUALITY = row.Cells(GGREYQUALITY.Index).Value.ToString
                        MTRS = Format(Val(row.Cells(GMTRS.Index).Value), "0.00")
                        QUALITYWT = Val(row.Cells(GQUALITYWT.Index).Value)
                        FROMNO = Val(row.Cells(GFROMNO.Index).Value)
                        FROMSRNO = Val(row.Cells(GFROMSRNO.Index).Value)
                        TYPE = row.Cells(GTYPE.Index).Value

                    Else

                        SRNO = SRNO & "|" & row.Cells(GSRNO.Index).Value
                        BEAMNO = BEAMNO & "|" & row.Cells(GBEAMNO.Index).Value.ToString
                        BEAMNAME = BEAMNAME & "|" & row.Cells(GBEAMNAME.Index).Value.ToString
                        BALANCECUT = BALANCECUT & "|" & Val(row.Cells(GBALANCECUT.Index).Value)
                        TAKA = TAKA & "|" & Val(row.Cells(GTAKA.Index).Value)
                        GREYQUALITY = GREYQUALITY & "|" & row.Cells(GGREYQUALITY.Index).Value.ToString
                        MTRS = MTRS & "|" & Format(Val(row.Cells(GMTRS.Index).Value), "0.00")
                        QUALITYWT = QUALITYWT & "|" & Val(row.Cells(GQUALITYWT.Index).Value)
                        FROMNO = FROMNO & "|" & Val(row.Cells(GFROMNO.Index).Value)
                        FROMSRNO = FROMSRNO & "|" & Val(row.Cells(GFROMSRNO.Index).Value)
                        TYPE = TYPE & "|" & row.Cells(GTYPE.Index).Value
                    End If
                End If
            Next

            alParaval.Add(SRNO)
            alParaval.Add(BEAMNO)
            alParaval.Add(BEAMNAME)
            alParaval.Add(BALANCECUT)
            alParaval.Add(TAKA)
            alParaval.Add(GREYQUALITY)
            alParaval.Add(MTRS)
            alParaval.Add(QUALITYWT)
            alParaval.Add(FROMNO)
            alParaval.Add(FROMSRNO)
            alParaval.Add(TYPE)

            Dim OBJGREYREC As New ClsGreyRecdWeaverSummary
            OBJGREYREC.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DT As DataTable = OBJGREYREC.SAVE()
                TEMPGREYRECDSUMMNO = DT.Rows(0).Item(0)
                MsgBox("Details Added")
            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPGREYRECDSUMMNO)
                IntResult = OBJGREYREC.UPDATE()
                EDIT = False
                MsgBox("Details Updated")
            End If

            'CLEAR()
            'SHOW NEXT BILL ON EDIT MODE DONT CLEAR
            Call toolnext_Click(sender, e)
            DTGREYRECDSUMMDATE.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Function ERRORVALID() As Boolean
        Dim bln As Boolean = True

        If DTGREYRECDSUMMDATE.Text = "__/__/____" Then
            EP.SetError(DTGREYRECDSUMMDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(DTGREYRECDSUMMDATE.Text) Then
                EP.SetError(DTGREYRECDSUMMDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        If ClientName = "JASHOK" And TXTCHALLANNO.Text.Trim.Length = 0 Then
            EP.SetError(TXTCHALLANNO, "Please Fill Challan No")
            bln = False
        End If

        If CMBNAME.Text.Trim.Length = 0 Then
            EP.SetError(CMBNAME, "Please Fill Jobber Name")
            bln = False
        End If

        If cmbtrans.Text.Trim.Length = 0 Then
            EP.SetError(cmbtrans, "Please Fill Transport Name")
            bln = False
        End If

        If CMBGODOWN.Text.Trim.Length = 0 Then
            EP.SetError(CMBGODOWN, " Please Fill Godown ")
            bln = False
        End If

        If GRIDGREY.RowCount = 0 Then
            EP.SetError(CMBNAME, "Enter Proper Details")
            bln = False
        End If

        If lbllocked.Visible = True Then
            EP.SetError(lbllocked, "Unable to Modify, Entry Locked")
            bln = False
        End If

        Return bln
    End Function

    Sub getsrno(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            For Each row As DataGridViewRow In grid.Rows
                row.Cells(0).Value = row.Index + 1
            Next
            TXTSRNO.Text = GRIDGREY.RowCount + 1
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Call CMDSAVE_Click(sender, e)
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tooldelete.Click
        Call CMDDELETE_Click(sender, e)
    End Sub

    Private Sub CMDDELETE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDDELETE.Click
        Dim IntResult As Integer
        Try

            If EDIT = True Then
                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If lbllocked.Visible = True Then
                    MsgBox("Unable to Delete, Entry Locked", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                If MsgBox("Delete Entry?", MsgBoxStyle.YesNo) = vbYes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(TXTGREYRECDSUMMNO.Text.Trim)
                    alParaval.Add(YearId)

                    Dim OBJDEL As New ClsGreyRecdWeaverSummary
                    OBJDEL.alParaval = alParaval
                    IntResult = OBJDEL.Delete()
                    MsgBox("Entry Deleted")
                    CLEAR()
                    EDIT = False
                End If
            Else
                MsgBox("Delete is only in Edit Mode")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    'Sub FILLGRID()
    '    Try
    '        If GRIDDOUBLECLICK = False Then
    '            GRIDGREY.Rows.Add(Val(TXTSRNO.Text.Trim), CMBBEAMNO.Text.Trim, TXTBEAMNAME.Text.Trim, CMBGREYQUALITY.Text.Trim, Format(Val(TXTTAKA.Text.Trim), "0"), Format(Val(TXTMTRS.Text.Trim), "0.00"), Format(Val(TXTQUALITYWT.Text.Trim), "0.00"), Val(TXTFROMNO.Text.Trim), Val(TXTFROMSRNO.Text.Trim))
    '        Else
    '            GRIDGREY.Item(GSRNO.Index, TEMPROW).Value = TXTSRNO.Text.Trim
    '            GRIDGREY.Item(GBEAMNO.Index, TEMPROW).Value = CMBBEAMNO.Text.Trim
    '            GRIDGREY.Item(GBEAMNAME.Index, TEMPROW).Value = TXTBEAMNAME.Text.Trim
    '            GRIDGREY.Item(GGREYQUALITY.Index, TEMPROW).Value = CMBGREYQUALITY.Text.Trim
    '            GRIDGREY.Item(GTAKA.Index, TEMPROW).Value = Format(Val(TXTTAKA.Text.Trim), "0")
    '            GRIDGREY.Item(GMTRS.Index, TEMPROW).Value = Format(Val(TXTMTRS.Text.Trim), "0.00")
    '            GRIDGREY.Item(GQUALITYWT.Index, TEMPROW).Value = Format(Val(TXTQUALITYWT.Text.Trim), "0.00")
    '            GRIDDOUBLECLICK = False
    '        End If

    '        CMBBEAMNO.Text = ""
    '        TXTBEAMNAME.Clear()
    '        CMBGREYQUALITY.Text = ""
    '        TXTTAKA.Clear()
    '        TXTMTRS.Clear()
    '        TXTQUALITYWT.Clear()
    '        TXTFROMNO.Clear()
    '        TXTFROMSRNO.Clear()

    '        getsrno(GRIDGREY)
    '        TOTAL()
    '        CMBBEAMNO.Focus()
    '        TXTSRNO.Text = Val(GRIDGREY.RowCount) + 1

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub GRIDGREY_DoubleClick(sender As Object, e As EventArgs) Handles GRIDGREY.DoubleClick
        Try
            EDITROW()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub EDITROW()
        Try
            If GRIDGREY.CurrentRow.Index >= 0 And GRIDGREY.Item(GSRNO.Index, GRIDGREY.CurrentRow.Index).Value <> Nothing Then
                GRIDDOUBLECLICK = True

                TXTSRNO.Text = GRIDGREY.Item(GSRNO.Index, GRIDGREY.CurrentRow.Index).Value.ToString
                CMBBEAMNO.Text = GRIDGREY.Item(GBEAMNO.Index, GRIDGREY.CurrentRow.Index).Value.ToString
                TXTBEAMNAME.Text = GRIDGREY.Item(GBEAMNAME.Index, GRIDGREY.CurrentRow.Index).Value.ToString
                CMBGREYQUALITY.Text = GRIDGREY.Item(GGREYQUALITY.Index, GRIDGREY.CurrentRow.Index).Value.ToString
                TXTTAKA.Text = GRIDGREY.Item(GTAKA.Index, GRIDGREY.CurrentRow.Index).Value.ToString
                TXTMTRS.Text = GRIDGREY.Item(GMTRS.Index, GRIDGREY.CurrentRow.Index).Value.ToString
                TXTQUALITYWT.Text = GRIDGREY.Item(GQUALITYWT.Index, GRIDGREY.CurrentRow.Index).Value.ToString

                TEMPROW = GRIDGREY.CurrentRow.Index
                CMBBEAMNO.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLBEAMNO()
        Try
            CMBBEAMNO.Items.Clear()
            CMBBEAMNO.Text = ""
            Dim OBJCMN As New ClsCommonMaster
            Dim dt As DataTable = OBJCMN.search("T.BEAMNO", "", "  (SELECT BEAMISSUE_BEAMNO AS BEAMNO FROM LEDGERS INNER JOIN BEAMISSUETOWEAVER ON LEDGERS.Acc_id = BEAMISSUETOWEAVER.BEAMISSUE_LEDGERID INNER JOIN BEAMISSUETOWEAVER_DESC ON BEAMISSUETOWEAVER.BEAMISSUE_NO = BEAMISSUETOWEAVER_DESC.BEAMISSUE_NO AND BEAMISSUETOWEAVER.BEAMISSUE_YEARID = BEAMISSUETOWEAVER_DESC.BEAMISSUE_YEARID WHERE LEDGERS.ACC_CMPNAME= '" & CMBNAME.Text.Trim & "' AND  BEAMISSUE_DONE = 'FALSE' AND (BEAMISSUETOWEAVER_DESC.BEAMISSUE_CUT - BEAMISSUETOWEAVER_DESC.BEAMISSUE_OUTCUT) > 0 AND  BEAMISSUETOWEAVER_DESC.BEAMISSUE_YEARID = " & YearId & " UNION ALL SELECT SMBEAMWEAVER_BEAMNO AS BEAMNO FROM  LEDGERS INNER JOIN STOCKMASTER_BEAMWEAVER  ON LEDGERS.Acc_id = SMBEAMWEAVER_WEAVERID WHERE LEDGERS.ACC_CMPNAME= '" & CMBNAME.Text.Trim & "' AND  SMBEAMWEAVER_DONE = 'FALSE' AND (SMBEAMWEAVER_CUT - SMBEAMWEAVER_OUTCUT) > 0 AND  SMBEAMWEAVER_YEARID = " & YearId & " ) AS T", "")
            If dt.Rows.Count > 0 Then
                For Each DTROW As DataRow In dt.Rows
                    CMBBEAMNO.Items.Add(DTROW("BEAMNO"))
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDGREY_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDGREY.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDGREY.RowCount > 0 Then
                'dont allow user if any of the grid line is in edit mode.....
                'cmbitemname.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block
                GRIDGREY.Rows.RemoveAt(GRIDGREY.CurrentRow.Index)
                getsrno(GRIDGREY)
                TXTSRNO.Text = GRIDGREY.RowCount + 1
                TOTAL()
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            GRIDGREY.RowCount = 0
LINE1:
            TEMPGREYRECDSUMMNO = Val(TXTGREYRECDSUMMNO.Text) - 1
Line2:
            If TEMPGREYRECDSUMMNO > 0 Then

                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" GREYRECDSUMM_NO ", "", " GREYRECDWEAVERSUMMARY", " AND GREYRECDSUMM_NO = '" & TEMPGREYRECDSUMMNO & "' AND GREYRECDWEAVERSUMMARY.GREYRECDSUMM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    EDIT = True
                    GreyRecdWeaverSummary_Load(sender, e)
                Else
                    TEMPGREYRECDSUMMNO = Val(TEMPGREYRECDSUMMNO - 1)
                    GoTo Line2
                End If
            Else
                CLEAR()
                EDIT = False
            End If

            If GRIDGREY.RowCount = 0 And TEMPGREYRECDSUMMNO > 1 Then
                TXTGREYRECDSUMMNO.Text = TEMPGREYRECDSUMMNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            GRIDGREY.RowCount = 0
LINE1:
            TEMPGREYRECDSUMMNO = Val(TXTGREYRECDSUMMNO.Text) + 1
            GETMAX_GREYRECDSUMM_NO()
            Dim MAXNO As Integer = TXTGREYRECDSUMMNO.Text.Trim
            CLEAR()
            If Val(TXTGREYRECDSUMMNO.Text) - 1 >= TEMPGREYRECDSUMMNO Then
                EDIT = True
                GreyRecdWeaverSummary_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDGREY.RowCount = 0 And TEMPGREYRECDSUMMNO < MAXNO Then
                TXTGREYRECDSUMMNO.Text = TEMPGREYRECDSUMMNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim OBJGREY As New GreyRecdWeaverSummaryDetails
            OBJGREY.MdiParent = MDIMain
            OBJGREY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tstxtbillno.KeyPress
        numkeypress(e, tstxtbillno, Me)
    End Sub

    Private Sub tstxtbillno_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tstxtbillno.Validated
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDGREY.RowCount = 0
                TEMPGREYRECDSUMMNO = Val(tstxtbillno.Text)
                If TEMPGREYRECDSUMMNO > 0 Then
                    EDIT = True
                    GreyRecdWeaverSummary_Load(sender, e)
                Else
                    CLEAR()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub GreyRecdFromWeaver_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            If ClientName = "JASHOK" Then TXTCHALLANNO.BackColor = Color.LemonChiffon
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbtrans.Enter
        Try
            If cmbtrans.Text.Trim = "" Then fillname(cmbtrans, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbtrans.Validating
        Try
            If cmbtrans.Text.Trim <> "" Then namevalidate(cmbtrans, cmbcode, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'", "Sundry Creditors", "TRANSPORT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBBEAMNO_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBBEAMNO.Validated
        Try
            If CMBBEAMNO.Text.Trim <> "" And CMBNAME.Text.Trim <> "" Then
                'GET BEAMNAME FROM BEAMNO
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" T.BEAMNAME, T.TOTALCUT, T.BALANCECUT, T.FROMNO, T.FROMSRNO,T.TYPE ", "", " (SELECT ISNULL(BEAMMASTER.BEAM_NAME,'') AS BEAMNAME, BEAMISSUETOWEAVER_DESC.BEAMISSUE_CUT as TOTALCUT, (BEAMISSUETOWEAVER_DESC.BEAMISSUE_CUT - BEAMISSUETOWEAVER_DESC.BEAMISSUE_OUTCUT) AS BALANCECUT, BEAMISSUETOWEAVER.BEAMISSUE_NO AS FROMNO, BEAMISSUETOWEAVER_DESC.BEAMISSUE_GRIDSRNO AS FROMSRNO, 'BEAMISSUE' AS TYPE FROM  BEAMISSUETOWEAVER INNER JOIN BEAMISSUETOWEAVER_DESC ON BEAMISSUETOWEAVER.BEAMISSUE_NO = BEAMISSUETOWEAVER_DESC.BEAMISSUE_NO AND BEAMISSUETOWEAVER.BEAMISSUE_YEARID = BEAMISSUETOWEAVER_DESC.BEAMISSUE_YEARID INNER JOIN LEDGERS ON BEAMISSUETOWEAVER.BEAMISSUE_LEDGERID = LEDGERS.Acc_id INNER JOIN BEAMMASTER ON BEAMISSUETOWEAVER_DESC.BEAMISSUE_BEAMID = BEAMMASTER.BEAM_ID WHERE LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND BEAMISSUE_BEAMNO = '" & CMBBEAMNO.Text.Trim & "' AND BEAMISSUE_DONE = 0 AND (BEAMISSUETOWEAVER_DESC.BEAMISSUE_CUT - BEAMISSUETOWEAVER_DESC.BEAMISSUE_OUTCUT) > 0 AND BEAMISSUETOWEAVER_DESC.BEAMISSUE_YEARID = " & YearId & " UNION ALL SELECT ISNULL(BEAMMASTER.BEAM_NAME,'') AS BEAMNAME, SMBEAMWEAVER_CUT as TOTALCUT, (SMBEAMWEAVER_CUT - SMBEAMWEAVER_OUTCUT) AS BALANCECUT, SMBEAMWEAVER_NO AS FROMNO, SMBEAMWEAVER_NO AS FROMSRNO, 'OPENING' AS TYPE  FROM  STOCKMASTER_BEAMWEAVER  INNER JOIN LEDGERS ON SMBEAMWEAVER_WEAVERID= LEDGERS.Acc_id INNER JOIN BEAMMASTER ON SMBEAMWEAVER_BEAMID = BEAMMASTER.BEAM_ID WHERE LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND SMBEAMWEAVER_BEAMNO = '" & CMBBEAMNO.Text.Trim & "' AND SMBEAMWEAVER_DONE = 0 AND (SMBEAMWEAVER_CUT - SMBEAMWEAVER_OUTCUT) > 0 AND SMBEAMWEAVER_YEARID = " & YearId & ") AS T", "")
                If DT.Rows.Count > 0 Then
                    TXTBEAMNAME.Text = DT.Rows(0).Item("BEAMNAME")
                    TXTBALANCECUT.Text = Val(DT.Rows(0).Item("BALANCECUT"))
                    TXTFROMNO.Text = Val(DT.Rows(0).Item("FROMNO"))
                    TXTFROMSRNO.Text = Val(DT.Rows(0).Item("FROMSRNO"))
                    TXTTYPE.Text = DT.Rows(0).Item("TYPE")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CALC(ByVal QUALITYNAME As String)
        Try
            If QUALITYNAME <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("ISNULL(GREY_QUALITYWT, 0) AS QUALITYWT, ISNULL(GREY_MTRS, 0) AS MTRS", "", "GREYQUALITYMASTER", "AND GREYQUALITYMASTER.GREY_NAME = '" & QUALITYNAME & "' AND GREY_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TXTMTRS.Text = Val(TXTTAKA.Text.Trim) * Val(DT.Rows(0).Item("MTRS"))
                    TXTQUALITYWT.Text = Format(Val(DT.Rows(0).Item("QUALITYWT")), "0.00")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGREYQUALITY_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBGREYQUALITY.Enter
        Try
            CMBGREYQUALITY.Items.Clear()
            If CMBGREYQUALITY.Text.Trim = "" And CMBBEAMNO.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("GREYQUALITYMASTER.GREY_NAME AS GREYNAME", "", " GREYQUALITYMASTER INNER JOIN BEAMMASTER ON GREYQUALITYMASTER.GREY_BEAMID = BEAMMASTER.BEAM_ID ", " AND BEAM_NAME ='" & TXTBEAMNAME.Text.Trim & "' AND GREY_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    CMBGREYQUALITY.Items.Clear()
                    CMBGREYQUALITY.Text = ""
                    For Each DTROW As DataRow In DT.Rows
                        CMBGREYQUALITY.Items.Add(DTROW("GREYNAME"))
                    Next
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGREYQUALITY_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBGREYQUALITY.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJGREY As New SelectGreyQuality
                OBJGREY.ShowDialog()
                If OBJGREY.TEMPNAME <> "" Then CMBGREYQUALITY.Text = OBJGREY.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGREYQUALITY_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBGREYQUALITY.Validated
        Try
            CALC(CMBGREYQUALITY.Text.Trim)
            TOTAL()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGREYQUALITY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBGREYQUALITY.Validating
        Try
            If CMBGREYQUALITY.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("GREYQUALITYMASTER.GREY_NAME AS GREYNAME, GREY_MTRS AS MTRS, GREY_QUALITYWT AS QUALITYWT", "", " GREYQUALITYMASTER ", " AND GREY_NAME ='" & CMBGREYQUALITY.Text.Trim & "' AND GREY_YEARID = " & YearId)
                If DT.Rows.Count = 0 Then
                    If MsgBox("Quality Not Present, Add New?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                        e.Cancel = True
                    Else
                        Dim OBJGREY As New GreyQualityMaster
                        OBJGREY.MdiParent = MDIMain
                        OBJGREY.TEMPQUALITYNAME = CMBGREYQUALITY.Text.Trim
                        OBJGREY.Show()
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBNAME.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "WEAVER")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBGODOWN.Enter
        Try
            If CMBGODOWN.Text.Trim <> "" Then fillGODOWN(CMBGODOWN, EDIT, " and GODOWN_ISOUR = 'True'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBGODOWN.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJGODOWN As New SelectGodown
                OBJGODOWN.FRMSTRING = "GODOWN"
                OBJGODOWN.SEARCH = " AND GODOWN_ISOUR = 'True'"
                OBJGODOWN.ShowDialog()
                If OBJGODOWN.TEMPNAME <> "" Then CMBGODOWN.Text = OBJGODOWN.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBGODOWN.Validating
        Try
            If CMBGODOWN.Text.Trim <> "" Then GODOWNVALIDATE(CMBGODOWN, e, Me, " and GODOWN_ISOUR = 'True'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DTGREYRECDSUMMDATE_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles DTGREYRECDSUMMDATE.Validating
        Try
            If DTGREYRECDSUMMDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(DTGREYRECDSUMMDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DTGREYRECDSUMMDATE_GotFocus(sender As Object, e As EventArgs) Handles DTGREYRECDSUMMDATE.GotFocus
        DTGREYRECDSUMMDATE.Select(0, 0)
    End Sub

    Private Sub GRIDGREY_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDGREY.CellEnter
        'Try
        '    If e.ColumnIndex = GGREYQUALITY.Index And GRIDGREY.CurrentRow.Cells(GBEAMNO.Index).Value.ToString <> "" Then
        '        CMBGREYQUALITY.Top = 31 + ((e.RowIndex - GRIDGREY.FirstDisplayedScrollingRowIndex) * 20)
        '        CMBGREYQUALITY.Visible = True

        '        CMBGREYQUALITY.Text = ""
        '        CMBGREYQUALITY.BringToFront()
        '        CMBGREYQUALITY.Focus()
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

    Private Sub CMBNAME_Validated(sender As Object, e As EventArgs) Handles CMBNAME.Validated
        Try
            'GET THOSE BEAMS WHICH ARE ISSUES TO SELECTED WEAVER
            'ALSO FETCH DATA FROM OPENINGSTOCKBEAM WITH WEAVER WHERE LOOM NO IS NOT PRESENT AND DONE = 0
            'AND WHICH ARE YET TO BE UPLOADED ON LOOM, IF BEAM IS UPLOADED THEN DO NOT SHOW THEM HERE
            CMBBEAMNO.Items.Clear()
            CMBBEAMNO.Text = ""
            TXTBEAMNAME.Clear()
            TXTBALANCECUT.Clear()
            Dim OBJCMN As New ClsCommonMaster
            Dim dt As DataTable = OBJCMN.search("T.BEAMNO", "", "  (SELECT BEAMISSUE_BEAMNO AS BEAMNO FROM LEDGERS INNER JOIN BEAMISSUETOWEAVER ON LEDGERS.Acc_id = BEAMISSUETOWEAVER.BEAMISSUE_LEDGERID INNER JOIN BEAMISSUETOWEAVER_DESC ON BEAMISSUETOWEAVER.BEAMISSUE_NO = BEAMISSUETOWEAVER_DESC.BEAMISSUE_NO AND BEAMISSUETOWEAVER.BEAMISSUE_YEARID = BEAMISSUETOWEAVER_DESC.BEAMISSUE_YEARID WHERE LEDGERS.ACC_CMPNAME= '" & CMBNAME.Text.Trim & "' AND  BEAMISSUE_DONE = 'FALSE' AND (BEAMISSUETOWEAVER_DESC.BEAMISSUE_CUT - BEAMISSUETOWEAVER_DESC.BEAMISSUE_OUTCUT) > 0 AND  BEAMISSUETOWEAVER_DESC.BEAMISSUE_YEARID = " & YearId & " UNION ALL SELECT SMBEAMWEAVER_BEAMNO AS BEAMNO FROM  LEDGERS INNER JOIN STOCKMASTER_BEAMWEAVER  ON LEDGERS.Acc_id = SMBEAMWEAVER_WEAVERID WHERE LEDGERS.ACC_CMPNAME= '" & CMBNAME.Text.Trim & "' AND  SMBEAMWEAVER_DONE = 'FALSE' AND (SMBEAMWEAVER_CUT - SMBEAMWEAVER_OUTCUT) > 0 AND  SMBEAMWEAVER_YEARID = " & YearId & " ) AS T", "")
            If dt.Rows.Count > 0 Then
                For Each DTROW As DataRow In dt.Rows
                    CMBBEAMNO.Items.Add(DTROW("BEAMNO"))
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try
            If GRIDDOUBLECLICK = False Then
                GRIDGREY.Rows.Add(0, CMBBEAMNO.Text.Trim, TXTBEAMNAME.Text.Trim, Val(TXTBALANCECUT.Text.Trim), Val(TXTTAKA.Text.Trim), CMBGREYQUALITY.Text.Trim, Val(TXTMTRS.Text.Trim), Val(TXTQUALITYWT.Text.Trim), Val(TXTFROMNO.Text.Trim), Val(TXTFROMSRNO.Text.Trim), TXTTYPE.Text.Trim)
            Else
                GRIDGREY.Item(GBEAMNO.Index, TEMPROW).Value = CMBBEAMNO.Text.Trim
                GRIDGREY.Item(GBEAMNAME.Index, TEMPROW).Value = TXTBEAMNAME.Text.Trim
                GRIDGREY.Item(GBALANCECUT.Index, TEMPROW).Value = Val(TXTBALANCECUT.Text.Trim)
                GRIDGREY.Item(GTAKA.Index, TEMPROW).Value = Val(TXTTAKA.Text.Trim)
                GRIDGREY.Item(GGREYQUALITY.Index, TEMPROW).Value = CMBGREYQUALITY.Text.Trim
                GRIDGREY.Item(GMTRS.Index, TEMPROW).Value = Val(TXTMTRS.Text.Trim)
                GRIDGREY.Item(GQUALITYWT.Index, TEMPROW).Value = Val(TXTQUALITYWT.Text.Trim)
                GRIDDOUBLECLICK = False
                TEMPROW = 0
            End If

            CMBBEAMNO.Text = ""
            TXTBEAMNAME.Clear()
            TXTBALANCECUT.Clear()
            TXTTAKA.Clear()
            CMBGREYQUALITY.Text = ""
            TXTMTRS.Clear()
            TXTQUALITYWT.Clear()
            TXTFROMNO.Clear()
            TXTFROMSRNO.Clear()
            TXTTYPE.Clear()

            getsrno(GRIDGREY)
            CMBBEAMNO.Focus()
            TXTSRNO.Text = Val(GRIDGREY.RowCount) + 1
            TOTAL()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTQUALITYWT_Validated(sender As Object, e As EventArgs) Handles TXTQUALITYWT.Validated
        Try
            If CMBBEAMNO.Text.Trim <> "" And TXTBEAMNAME.Text.Trim <> "" And Val(TXTTAKA.Text.Trim) > 0 And CMBGREYQUALITY.Text.Trim <> "" And Val(TXTMTRS.Text.Trim) > 0 Then FILLGRID() Else MsgBox("Enter Proper Details", MsgBoxStyle.Critical)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDGREY_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDGREY.CellDoubleClick
        Try
            If e.RowIndex = -1 Then Exit Sub

            If e.RowIndex >= 0 And GRIDGREY.Item(GSRNO.Index, e.RowIndex).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                TXTSRNO.Text = GRIDGREY.Item(GSRNO.Index, e.RowIndex).Value
                CMBBEAMNO.Text = GRIDGREY.Item(GBEAMNO.Index, e.RowIndex).Value
                TXTBEAMNAME.Text = GRIDGREY.Item(GBEAMNAME.Index, e.RowIndex).Value
                TXTBALANCECUT.Text = Val(GRIDGREY.Item(GBALANCECUT.Index, e.RowIndex).Value)
                TXTTAKA.Text = Val(GRIDGREY.Item(GTAKA.Index, e.RowIndex).Value)
                CMBGREYQUALITY.Text = GRIDGREY.Item(GGREYQUALITY.Index, e.RowIndex).Value
                TXTMTRS.Text = Val(GRIDGREY.Item(GMTRS.Index, e.RowIndex).Value)
                TXTQUALITYWT.Text = Val(GRIDGREY.Item(GQUALITYWT.Index, e.RowIndex).Value)
                TXTFROMNO.Text = Val(GRIDGREY.Item(GFROMNO.Index, e.RowIndex).Value)
                TXTFROMSRNO.Text = Val(GRIDGREY.Item(GFROMSRNO.Index, e.RowIndex).Value)
                TXTTYPE.Text = GRIDGREY.Item(GTYPE.Index, e.RowIndex).Value

                TEMPROW = e.RowIndex
                CMBBEAMNO.Focus()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class