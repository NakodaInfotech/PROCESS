
Imports System.ComponentModel
Imports BL

Public Class BeamUpload

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean   'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public EDIT As Boolean
    Public TEMPBEAMUPLOADNO As Integer

    Private Sub CMDEXIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub CMDCLEAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDCLEAR.Click
        CLEAR()
        EDIT = False
        DTBEAMUPLOADDATE.Focus()
    End Sub

    Sub CLEAR()

        tstxtbillno.Clear()
        TXTBEAMUPLOADNO.Clear()
        DTBEAMUPLOADDATE.Text = Mydate
        CMBNAME.Text = ""
        CMBNAME.Enabled = True

        TXTSRNO.Clear()
        CMBLOOMNO.Text = ""
        LOOMUPLOADDATE.Clear()
        TXTBEAMNAME.Clear()
        TXTBEAMNO.Clear()
        TXTBALANCECUT.Clear()
        TXTWT.Clear()
        GRIDBEAMUPLOAD.RowCount = 0

        EP.Clear()
        GRIDBEAMUPLOAD.RowCount = 0
        GETMAX_BEAMUPLOAD_NO()

    End Sub

    Sub GETMAX_BEAMUPLOAD_NO()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax("ISNULL(MAX(BEAMUPLOAD_NO),0)+1", "BEAMUPLOADMASTER", "AND BEAMUPLOAD_YEARID=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTBEAMUPLOADNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub BeamUpload_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If ERRORVALID() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then CMDSAVE_Click(sender, e)
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
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.WaitCursor
        End Try
    End Sub

    Sub FILLCMB()
        If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'")
    End Sub

    Private Sub BeamUpload_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'MFG'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor

            FILLCMB()
            CLEAR()


            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim dttable As New DataTable
                Dim OBJEAMUPLOAD As New ClsBeamUpload

                OBJEAMUPLOAD.alParaval.Add(TEMPBEAMUPLOADNO)
                OBJEAMUPLOAD.alParaval.Add(YearId)
                dttable = OBJEAMUPLOAD.SELECTBEAM()

                If dttable.Rows.Count > 0 Then
                    TXTBEAMUPLOADNO.Text = TEMPBEAMUPLOADNO
                    DTBEAMUPLOADDATE.Text = dttable.Rows(0).Item("DATE")
                    CMBNAME.Text = dttable.Rows(0).Item("NAME").ToString

                    'ITEM GRID
                    For Each ROW As DataRow In dttable.Rows
                        GRIDBEAMUPLOAD.Rows.Add(Val(ROW("SRNO")), ROW("LOOMNO"), ROW("UPLOADDATE"), ROW("BEAMNAME"), ROW("BEAMNO"), Val(ROW("BALANCEBEAM")), Format(Val(ROW("WT")), "0.000"), ROW("FROMNO"), ROW("FROMSRNO"), ROW("TYPE"))
                    Next

                    getsrno(GRIDBEAMUPLOAD)
                    CMBNAME.Enabled = False
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

            alParaval.Add(Format(Convert.ToDateTime(DTBEAMUPLOADDATE.Text.Trim).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBNAME.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)

            Dim SRNO As String = ""
            Dim LOOMNO As String = ""
            Dim UPLOADDATE As String = ""
            Dim BEAMNAME As String = ""
            Dim BEAMNO As String = ""
            Dim BALANCEBEAM As String = ""
            Dim WT As String = ""
            Dim FROMNO As String = ""
            Dim FROMSRNO As String = ""
            Dim TYPE As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDBEAMUPLOAD.Rows
                If row.Cells(GLOOMNO.Index).Value.ToString <> "" Then
                    If SRNO = "" Then
                        SRNO = row.Cells(GSRNO.Index).Value
                        LOOMNO = Val(row.Cells(GLOOMNO.Index).Value)
                        UPLOADDATE = Format(Convert.ToDateTime(row.Cells(GDATE.Index).Value).Date, "MM/dd/yyyy")
                        BEAMNAME = row.Cells(GBEAMNAME.Index).Value.ToString
                        BEAMNO = row.Cells(GBEAMNO.Index).Value.ToString
                        BALANCEBEAM = Val(row.Cells(GBALANCE.Index).Value)
                        WT = Val(row.Cells(GWT.Index).Value)
                        FROMNO = row.Cells(GFROMNO.Index).Value.ToString
                        FROMSRNO = row.Cells(GFROMSRNO.Index).Value.ToString
                        TYPE = row.Cells(GTYPE.Index).Value.ToString
                    Else
                        SRNO = SRNO & "|" & row.Cells(GSRNO.Index).Value
                        LOOMNO = LOOMNO & "|" & Val(row.Cells(GLOOMNO.Index).Value)
                        UPLOADDATE = UPLOADDATE & "|" & Format(Convert.ToDateTime(row.Cells(GDATE.Index).Value).Date, "MM/dd/yyyy")
                        BEAMNAME = BEAMNAME & "|" & row.Cells(GBEAMNAME.Index).Value.ToString
                        BEAMNO = BEAMNO & "|" & row.Cells(GBEAMNO.Index).Value.ToString
                        BALANCEBEAM = BALANCEBEAM & "|" & Val(row.Cells(GBALANCE.Index).Value)
                        WT = WT & "|" & Val(row.Cells(GWT.Index).Value)
                        FROMNO = FROMNO & "|" & row.Cells(GFROMNO.Index).Value.ToString
                        FROMSRNO = FROMSRNO & "|" & row.Cells(GFROMSRNO.Index).Value.ToString
                        TYPE = TYPE & "|" & row.Cells(GTYPE.Index).Value.ToString
                    End If
                End If
            Next

            alParaval.Add(SRNO)
            alParaval.Add(LOOMNO)
            alParaval.Add(UPLOADDATE)
            alParaval.Add(BEAMNAME)
            alParaval.Add(BEAMNO)
            alParaval.Add(BALANCEBEAM)
            alParaval.Add(WT)
            alParaval.Add(FROMNO)
            alParaval.Add(FROMSRNO)
            alParaval.Add(TYPE)

            Dim OBJEAMUPLOAD As New ClsBeamUpload
            OBJEAMUPLOAD.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DT As DataTable = OBJEAMUPLOAD.SAVE()
                TEMPBEAMUPLOADNO = DT.Rows(0).Item(0)
                MsgBox("Details Added")

            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPBEAMUPLOADNO)
                IntResult = OBJEAMUPLOAD.UPDATE()
                EDIT = False
                MsgBox("Details Updated")
            End If

            'CLEAR()
            'SHOW NEXT BILL ON EDIT MODE DONT CLEAR
            Call toolnext_Click(sender, e)
            DTBEAMUPLOADDATE.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Function ERRORVALID() As Boolean
        Dim bln As Boolean = True


        If DTBEAMUPLOADDATE.Text = "__/__/____" Then
            EP.SetError(DTBEAMUPLOADDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(DTBEAMUPLOADDATE.Text) Then
                EP.SetError(DTBEAMUPLOADDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        If CMBNAME.Text.Trim.Length = 0 Then
            EP.SetError(CMBNAME, "Please Fill Jobber Name")
            bln = False
        End If

        If GRIDBEAMUPLOAD.RowCount = 0 Then
            EP.SetError(CMBNAME, "Enter Proper Details")
            bln = False
        End If


        'DONE TEMPORARILY
        If lbllocked.Visible = True Then
            EP.SetError(lbllocked, "Unable to Modify, Entry Locked")
            bln = False
        End If

        Return bln
    End Function

    Private Sub DTGREYRECDDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTBEAMUPLOADDATE.GotFocus
        DTBEAMUPLOADDATE.SelectAll()
    End Sub

    Sub getsrno(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            For Each row As DataGridViewRow In grid.Rows
                row.Cells(0).Value = row.Index + 1
            Next
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
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

    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            GRIDBEAMUPLOAD.RowCount = 0
LINE1:
            TEMPBEAMUPLOADNO = Val(TXTBEAMUPLOADNO.Text) - 1
Line2:
            If TEMPBEAMUPLOADNO > 0 Then

                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" BEAMUPLOAD_NO ", "", "  BEAMUPLOADMASTER", " AND BEAMUPLOAD_NO = '" & TEMPBEAMUPLOADNO & "' AND BEAMUPLOADMASTER.BEAMUPLOAD_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    EDIT = True
                    BeamUpload_Load(sender, e)
                Else
                    TEMPBEAMUPLOADNO = Val(TEMPBEAMUPLOADNO - 1)
                    GoTo Line2
                End If
            Else
                CLEAR()
                EDIT = False
            End If

            If GRIDBEAMUPLOAD.RowCount = 0 And TEMPBEAMUPLOADNO > 1 Then
                TXTBEAMUPLOADNO.Text = TEMPBEAMUPLOADNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            GRIDBEAMUPLOAD.RowCount = 0
LINE1:
            TEMPBEAMUPLOADNO = Val(TXTBEAMUPLOADNO.Text) + 1
            GETMAX_BEAMUPLOAD_NO()
            Dim MAXNO As Integer = TXTBEAMUPLOADNO.Text.Trim
            CLEAR()
            If Val(TXTBEAMUPLOADNO.Text) - 1 >= TEMPBEAMUPLOADNO Then
                EDIT = True
                BeamUpload_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDBEAMUPLOAD.RowCount = 0 And TEMPBEAMUPLOADNO < MAXNO Then
                TXTBEAMUPLOADNO.Text = TEMPBEAMUPLOADNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tstxtbillno.KeyPress
        numkeypress(e, tstxtbillno, Me)
    End Sub

    Private Sub tstxtbillno_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tstxtbillno.Validated
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDBEAMUPLOAD.RowCount = 0
                TEMPBEAMUPLOADNO = Val(tstxtbillno.Text)
                If TEMPBEAMUPLOADNO > 0 Then
                    EDIT = True
                    BeamUpload_Load(sender, e)
                Else
                    CLEAR()
                    EDIT = False
                End If
            End If
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

                If MsgBox("Delete Entry?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                Dim alParaval As New ArrayList
                alParaval.Add(TXTBEAMUPLOADNO.Text.Trim)
                alParaval.Add(YearId)

                Dim OBJDEL As New ClsBeamUpload
                OBJDEL.alParaval = alParaval
                IntResult = OBJDEL.DELETE()
                MsgBox("Entry Deleted")
                CLEAR()
                EDIT = False
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim OBJGREY As New BeamUploadDetails
            OBJGREY.MdiParent = MDIMain
            OBJGREY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBNAME.Validated
        Try
            If CMBNAME.Text.Trim <> "" And EDIT = False Then
                FILLLOOMNO(CMBLOOMNO, CMBNAME.Text.Trim, False, "")
                FILLPENINGBEAM()
                CMBNAME.Enabled = False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLPENINGBEAM()
        Try
            'GET THOSE BEAMS WHICH ARE ISSUES TO SELECTED WEAVER
            'ALSO FETCH DATA FROM OPENINGSTOCKBEAM WITH WEAVER WHERE LOOM NO IS NOT PRESENT AND DONE = 0
            'AND WHICH ARE YET TO BE UPLOADED ON LOOM, IF BEAM IS UPLOADED THEN DO NOT SHOW THEM HERE
            GRIDBEAMUPLOAD.RowCount = 0
            Dim OBJCMN As New ClsCommonMaster
            Dim dt As DataTable = OBJCMN.search("T.BEAMNO, T.BEAMNAME, T.BALANCECUT, T.BEAMWT, T.FROMNO, T.FROMSRNO, T.FROMTYPE", "", "  (SELECT BEAMISSUE_BEAMNO AS BEAMNO, BEAMMASTER.BEAM_NAME AS BEAMNAME, (BEAMISSUETOWEAVER_DESC.BEAMISSUE_CUT - BEAMISSUETOWEAVER_DESC.BEAMISSUE_OUTCUT) AS BALANCECUT, ISNULL(BEAMISSUE_WT,0) AS BEAMWT, BEAMISSUETOWEAVER.BEAMISSUE_NO AS FROMNO, BEAMISSUETOWEAVER_DESC.BEAMISSUE_GRIDSRNO AS FROMSRNO, 'BEAMISSUE' AS FROMTYPE FROM LEDGERS INNER JOIN BEAMISSUETOWEAVER ON LEDGERS.Acc_id = BEAMISSUETOWEAVER.BEAMISSUE_LEDGERID INNER JOIN BEAMISSUETOWEAVER_DESC ON BEAMISSUETOWEAVER.BEAMISSUE_NO = BEAMISSUETOWEAVER_DESC.BEAMISSUE_NO AND BEAMISSUETOWEAVER.BEAMISSUE_YEARID = BEAMISSUETOWEAVER_DESC.BEAMISSUE_YEARID INNER JOIN BEAMMASTER ON BEAMISSUE_BEAMID = BEAMMASTER.BEAM_ID WHERE LEDGERS.ACC_CMPNAME= '" & CMBNAME.Text.Trim & "' AND ISNULL(BEAMISSUE_LOOMNO,0) = 0 AND (BEAMISSUETOWEAVER_DESC.BEAMISSUE_CUT - BEAMISSUETOWEAVER_DESC.BEAMISSUE_OUTCUT) > 0 AND  BEAMISSUETOWEAVER_DESC.BEAMISSUE_YEARID = " & YearId & " UNION ALL SELECT SMBEAMWEAVER_BEAMNO AS BEAMNO, BEAMMASTER.BEAM_NAME AS BEAMNAME, (SMBEAMWEAVER_CUT - SMBEAMWEAVER_OUTCUT) AS BALANCECUT, ISNULL(SMBEAMWEAVER_WT,0) AS BEAMWT, SMBEAMWEAVER_NO AS FROMNO, SMBEAMWEAVER_NO AS FROMSRNO, 'OPENING' AS FROMTYPE FROM  LEDGERS INNER JOIN STOCKMASTER_BEAMWEAVER  ON LEDGERS.Acc_id = SMBEAMWEAVER_WEAVERID INNER JOIN BEAMMASTER ON SMBEAMWEAVER_BEAMID = BEAMMASTER.BEAM_ID WHERE LEDGERS.ACC_CMPNAME= '" & CMBNAME.Text.Trim & "' AND ISNULL(SMBEAMWEAVER_LOOMNO,0) = 0 AND (SMBEAMWEAVER_CUT - SMBEAMWEAVER_OUTCUT) > 0 AND  SMBEAMWEAVER_YEARID = " & YearId & " ) AS T", "")
            For Each DTROW As DataRow In dt.Rows
                GRIDBEAMUPLOAD.Rows.Add(0, "", "", DTROW("BEAMNAME"), DTROW("BEAMNO"), Val(DTROW("BALANCECUT")), Val(DTROW("BEAMWT")), Val(DTROW("FROMNO")), Val(DTROW("FROMSRNO")), DTROW("FROMTYPE"))
            Next
            getsrno(GRIDBEAMUPLOAD)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DTGREYRECDDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DTBEAMUPLOADDATE.Validating
        Try
            If DTBEAMUPLOADDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(DTBEAMUPLOADDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GreyRecdFromWeaver_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            If ALLOWMFG = False Then Exit Sub
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try
            If GRIDDOUBLECLICK = True Then
                GRIDBEAMUPLOAD.Item(GLOOMNO.Index, TEMPROW).Value = Val(CMBLOOMNO.Text.Trim)
                GRIDBEAMUPLOAD.Item(GDATE.Index, TEMPROW).Value = LOOMUPLOADDATE.Text
                GRIDDOUBLECLICK = False
                TEMPROW = 0
            End If

            LOOMUPLOADDATE.Clear()
            TXTBEAMNAME.Clear()
            TXTBEAMNO.Text = ""
            TXTBALANCECUT.Clear()
            TXTWT.Clear()
            getsrno(GRIDBEAMUPLOAD)
            TXTSRNO.Text = Val(GRIDBEAMUPLOAD.RowCount) + 1

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDBEAMUPLOAD_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDBEAMUPLOAD.CellDoubleClick
        Try
            If e.RowIndex = -1 Then Exit Sub

            If e.RowIndex >= 0 And GRIDBEAMUPLOAD.Item(GSRNO.Index, e.RowIndex).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                TXTSRNO.Text = GRIDBEAMUPLOAD.Item(GSRNO.Index, e.RowIndex).Value
                CMBLOOMNO.Text = GRIDBEAMUPLOAD.Item(GLOOMNO.Index, e.RowIndex).Value
                LOOMUPLOADDATE.Text = GRIDBEAMUPLOAD.Item(GDATE.Index, e.RowIndex).Value
                TXTBEAMNAME.Text = GRIDBEAMUPLOAD.Item(GBEAMNAME.Index, e.RowIndex).Value
                TXTBEAMNO.Text = GRIDBEAMUPLOAD.Item(GBEAMNO.Index, e.RowIndex).Value
                TXTBALANCECUT.Text = Val(GRIDBEAMUPLOAD.Item(GBALANCE.Index, e.RowIndex).Value)
                TXTWT.Text = Val(GRIDBEAMUPLOAD.Item(GWT.Index, e.RowIndex).Value)

                TEMPROW = e.RowIndex
                CMBLOOMNO.Focus()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDGREY_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDBEAMUPLOAD.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDBEAMUPLOAD.RowCount > 0 Then
                GRIDBEAMUPLOAD.CurrentRow.Cells(GLOOMNO.Index).Value = ""
                GRIDBEAMUPLOAD.CurrentRow.Cells(GDATE.Index).Value = ""
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub LOOMUPLOADDATE_Validated(sender As Object, e As EventArgs) Handles LOOMUPLOADDATE.Validated
        Try
            If Val(CMBLOOMNO.Text.Trim) > 0 And LOOMUPLOADDATE.Text <> "__/__/____" Then FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LOOMUPLOADDATE_Validating(sender As Object, e As CancelEventArgs) Handles LOOMUPLOADDATE.Validating
        Try
            If LOOMUPLOADDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(LOOMUPLOADDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class