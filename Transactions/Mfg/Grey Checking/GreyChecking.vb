Imports BL

Public Class GreyChecking
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean   'USED FOR RIGHT MANAGEMAENT
    Dim GRIDUPLOADDOUBLECLICK As Boolean
    Dim TEMPUPLOADROW As Integer
    Public EDIT As Boolean
    Public TEMPGREYCHKNO As Integer


    Private Sub CMDEXIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub CMDCLEAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDCLEAR.Click
        CLEAR()
        EDIT = False
        DTGREYCHKDATE.Focus()
    End Sub

    Sub TOTAL()
        Try

            LBLTOTALMTRS.Text = 0.0
            LBLTOTALWT.Text = 0.0
            For Each ROW As DataGridViewRow In GRIDGREYCHK.Rows
                If ROW.Cells(GLOOMNO.Index).Value <> Nothing Then
                    LBLTOTALMTRS.Text = Format(Val(LBLTOTALMTRS.Text) + Val(ROW.Cells(GMTRS.Index).EditedFormattedValue), "0.000")
                    LBLTOTALWT.Text = Format(Val(LBLTOTALWT.Text) + Val(ROW.Cells(GWT.Index).EditedFormattedValue), "0.000")
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()

        tstxtbillno.Clear()

        TXTGREYCHKNO.Clear()
        DTGREYCHKDATE.Text = Mydate

        CMBNAME.Text = ""
        TXTGREYRECDNO.Clear()
        DTGREYRECDDATE.Text = ""

        EP.Clear()
        lbllocked.Visible = False
        PBlock.Visible = False

        CMDSELECTGREY.Enabled = True
        CMBNAME.Enabled = True

        GRIDGREYCHK.RowCount = 0

        LBLTOTALMTRS.Text = 0.0
        LBLTOTALWT.Text = 0.0

        GETMAX_GREYCHK_NO()

        GRIDUPLOADDOUBLECLICK = False

        TabControl1.SelectedIndex = 0

        PBSOFTCOPY.Image = Nothing
        TXTUPLOADSRNO.Clear()
        txtuploadname.Clear()
        txtuploadremarks.Clear()
        TXTIMGPATH.Clear()
        gridupload.RowCount = 0

        TXTUPLOADSRNO.Text = 1
    End Sub

    Sub GETMAX_GREYCHK_NO()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax("ISNULL(MAX(GREYCHK_NO),0)+1", "GREYCHECKING", "AND GREYCHK_YEARID=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTGREYCHKNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub GreyChecking_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If errorvalid() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then CMDSAVE_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.KeyCode = Keys.Oemcomma Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for Delete
                tstxtbillno.Focus()
                tstxtbillno.SelectAll()
            ElseIf (e.Alt = True And e.KeyCode = Windows.Forms.Keys.D1) Then       'for CLEAR
                TabControl1.SelectedIndex = (0)
            ElseIf (e.Alt = True And e.KeyCode = Windows.Forms.Keys.D2) Then       'for CLEAR
                TabControl1.SelectedIndex = (1)
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

    Private Sub GreyChecking_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
                Dim OBJGREYCHK As New ClsGreyChecking

                OBJGREYCHK.alParaval.Add(TEMPGREYCHKNO)
                OBJGREYCHK.alParaval.Add(YearId)
                dttable = OBJGREYCHK.selectGREYCHK()

                If dttable.Rows.Count > 0 Then

                    CMDSELECTGREY.Enabled = False
                    CMBNAME.Enabled = False

                    TXTGREYCHKNO.Text = TEMPGREYCHKNO
                    DTGREYCHKDATE.Text = dttable.Rows(0).Item("GREYCHKDATE")

                    CMBNAME.Text = dttable.Rows(0).Item("WEAVER").ToString
                    TXTGREYRECDNO.Text = dttable.Rows(0).Item("GREYRECDNO").ToString
                    DTGREYRECDDATE.Text = dttable.Rows(0).Item("GREYRECDDATE")

                    'ITEM GRID
                    For Each ROW As DataRow In dttable.Rows
                        GRIDGREYCHK.Rows.Add(Val(ROW("LOOMNO")), Format(Val(ROW("MTRS")), "0.00"), Format(Val(ROW("WT")), "0.00"), Val(ROW("TP")), Format(Val(ROW("PICK")), "0.00"), Format(Val(ROW("PANNA")), "0.00"), ROW("NARR"))
                    Next

                    'UPLOAD(GRID)
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.search(" GREYCHECKING_UPLOAD.GREYCHK_SRNO AS GRIDSRNO, GREYCHECKING_UPLOAD.GREYCHK_REMARKS AS REMARKS, GREYCHECKING_UPLOAD.GREYCHK_NAME AS NAME, GREYCHECKING_UPLOAD.GREYCHK_PHOTO AS IMGPATH ", "", " GREYCHECKING_UPLOAD ", " AND GREYCHECKING_UPLOAD.GREYCHK_NO = " & TEMPGREYCHKNO & " AND GREYCHK_YEARID = " & YearId & " ORDER BY GREYCHECKING_UPLOAD.GREYCHK_SRNO")
                    If DT.Rows.Count > 0 Then
                        For Each DTR As DataRow In DT.Rows
                            gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                        Next
                    End If

                    TOTAL()
                    DTGREYCHKDATE.Focus()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSAVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSAVE.Click
        Try
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim IntResult As Integer
            Dim alParaval As New ArrayList

            alParaval.Add(Format(Convert.ToDateTime(DTGREYCHKDATE.Text.Trim).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBNAME.Text.Trim)
            alParaval.Add(Val(TXTGREYRECDNO.Text.Trim))
            alParaval.Add(DTGREYRECDDATE.Text.Trim)
            alParaval.Add(Format(Val(LBLTOTALMTRS.Text), "0.00"))
            alParaval.Add(Format(Val(LBLTOTALWT.Text), "0.00"))

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)

            Dim LOOMNO As String = ""
            Dim MTRS As String = ""
            Dim WT As String = ""
            Dim TP As String = ""
            Dim PICK As String = ""
            Dim PANNA As String = ""
            Dim REPORT As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDGREYCHK.Rows
                If LOOMNO = "" Then
                    LOOMNO = Val(row.Cells(GLOOMNO.Index).Value)
                    MTRS = Format(Val(row.Cells(GMTRS.Index).Value), "0.00")
                    WT = Format(Val(row.Cells(GWT.Index).Value), "0.00")
                    TP = Val(row.Cells(GTP.Index).Value)
                    PICK = Format(Val(row.Cells(GPICK.Index).Value), "0.00")
                    PANNA = Format(Val(row.Cells(GPANNA.Index).Value), "0.00")
                    If row.Cells(GNARR.Index).Value <> Nothing Then REPORT = row.Cells(GNARR.Index).Value.ToString Else REPORT = ""

                Else

                    LOOMNO = LOOMNO & "|" & Val(row.Cells(GLOOMNO.Index).Value)
                    MTRS = MTRS & "|" & Format(Val(row.Cells(GMTRS.Index).Value), "0.00")
                    WT = WT & "|" & Format(Val(row.Cells(GWT.Index).Value), "0.00")
                    TP = TP & "|" & Val(row.Cells(GTP.Index).Value)
                    PICK = PICK & "|" & Format(Val(row.Cells(GPICK.Index).Value), "0.00")
                    PANNA = PANNA & "|" & Format(Val(row.Cells(GPANNA.Index).Value), "0.00")
                    If row.Cells(GNARR.Index).Value <> Nothing Then REPORT = REPORT & "|" & row.Cells(GNARR.Index).Value.ToString Else REPORT = REPORT & "|" & ""

                End If
            Next

            alParaval.Add(LOOMNO)
            alParaval.Add(MTRS)
            alParaval.Add(WT)
            alParaval.Add(TP)
            alParaval.Add(PICK)
            alParaval.Add(PANNA)
            alParaval.Add(REPORT)

            Dim OBJGREYCHK As New ClsGreyChecking
            OBJGREYCHK.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DT As DataTable = OBJGREYCHK.save()
                TEMPGREYCHKNO = DT.Rows(0).Item(0)
                MsgBox("Details Added")

            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPGREYCHKNO)
                IntResult = OBJGREYCHK.Update()
                EDIT = False
                MsgBox("Details Updated")

            End If

            If gridupload.RowCount > 0 Then SAVEUPLOAD()
            'CLEAR()
            'SHOW NEXT BILL ON EDIT MODE DONT CLEAR
            Call toolnext_Click(sender, e)
            DTGREYCHKDATE.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub SAVEUPLOAD()

        Try
            Dim OBJGREYCHK As New ClsGreyChecking
            For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                Dim MS As New IO.MemoryStream
                Dim ALPARAVAL As New ArrayList
                If row.Cells(GUSRNO.Index).Value <> Nothing Then
                    ALPARAVAL.Add(TEMPGREYCHKNO)
                    ALPARAVAL.Add(row.Cells(GUSRNO.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUREMARKS.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUNAME.Index).Value)

                    PBSOFTCOPY.Image = row.Cells(GUIMGPATH.Index).Value
                    PBSOFTCOPY.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                    ALPARAVAL.Add(MS.ToArray)
                    ALPARAVAL.Add(YearId)

                    OBJGREYCHK.alParaval = ALPARAVAL
                    Dim INTRES As Integer = OBJGREYCHK.SAVEUPLOAD()
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLUPLOAD()

        If GRIDUPLOADDOUBLECLICK = False Then
            gridupload.Rows.Add(Val(TXTUPLOADSRNO.Text.Trim), txtuploadremarks.Text.Trim, txtuploadname.Text.Trim, PBSOFTCOPY.Image)
            getsrno(gridupload)
        ElseIf GRIDUPLOADDOUBLECLICK = True Then

            gridupload.Item(GUSRNO.Index, TEMPUPLOADROW).Value = TXTUPLOADSRNO.Text.Trim
            gridupload.Item(GUREMARKS.Index, TEMPUPLOADROW).Value = txtuploadremarks.Text.Trim
            gridupload.Item(GUNAME.Index, TEMPUPLOADROW).Value = txtuploadname.Text.Trim
            gridupload.Item(GUIMGPATH.Index, TEMPUPLOADROW).Value = PBSOFTCOPY.Image

            GRIDUPLOADDOUBLECLICK = False

        End If
        gridupload.FirstDisplayedScrollingRowIndex = gridupload.RowCount - 1

        TXTUPLOADSRNO.Clear()
        txtuploadremarks.Clear()
        txtuploadname.Clear()
        PBSOFTCOPY.Image = Nothing
        TXTIMGPATH.Clear()

        txtuploadremarks.Focus()

    End Sub

    Private Function ERRORVALID() As Boolean
        Dim bln As Boolean = True


        If DTGREYCHKDATE.Text = "__/__/____" Then
            EP.SetError(DTGREYCHKDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(DTGREYCHKDATE.Text) Then
                EP.SetError(DTGREYCHKDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If


        If TXTGREYRECDNO.Text.Trim.Length = 0 Then
            EP.SetError(TXTGREYRECDNO, "Please Fill Grey Recd. No")
            bln = False
        End If

        If CMBNAME.Text.Trim.Length = 0 Then
            EP.SetError(CMBNAME, "Please Fill Jobber Name")
            bln = False
        End If

        If GRIDGREYCHK.RowCount = 0 Then
            EP.SetError(CMBNAME, "Enter Proper Details")
            bln = False
        Else
            For Each ROW As DataGridViewRow In GRIDGREYCHK.Rows
                If Val(ROW.Cells(GMTRS.Index).Value) = 0 Then
                    EP.SetError(CMBNAME, "Meters cannot be 0")
                    bln = False
                End If
            Next
        End If

        If lbllocked.Visible = True Then
            EP.SetError(lbllocked, "Unable to Modify, Entry Locked")
            bln = False
        End If

        Return bln
    End Function

    Private Sub DTGREYCHKDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTGREYCHKDATE.GotFocus
        DTGREYCHKDATE.SelectAll()
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
            GRIDGREYCHK.RowCount = 0
LINE1:
            TEMPGREYCHKNO = Val(TXTGREYCHKNO.Text) - 1
Line2:
            If TEMPGREYCHKNO > 0 Then

                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" GREYCHK_NO ", "", "  GREYCHECKING", " AND GREYCHK_NO = '" & TEMPGREYCHKNO & "' AND GREYCHECKING.GREYCHK_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    EDIT = True
                    GreyChecking_Load(sender, e)
                Else
                    TEMPGREYCHKNO = Val(TEMPGREYCHKNO - 1)
                    GoTo Line2
                End If
            Else
                CLEAR()
                EDIT = False
            End If

            If GRIDGREYCHK.RowCount = 0 And TEMPGREYCHKNO > 1 Then
                TXTGREYCHKNO.Text = TEMPGREYCHKNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            GRIDGREYCHK.RowCount = 0
LINE1:
            TEMPGREYCHKNO = Val(TXTGREYRECDNO.Text) + 1
            GETMAX_GREYCHK_NO()
            Dim MAXNO As Integer = TXTGREYCHKNO.Text.Trim
            CLEAR()
            If Val(TXTGREYCHKNO.Text) - 1 >= TEMPGREYCHKNO Then
                EDIT = True
                GreyChecking_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDGREYCHK.RowCount = 0 And TEMPGREYCHKNO < MAXNO Then
                TXTGREYCHKNO.Text = TEMPGREYCHKNO
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
                GRIDGREYCHK.RowCount = 0
                TEMPGREYCHKNO = Val(tstxtbillno.Text)
                If TEMPGREYCHKNO > 0 Then
                    EDIT = True
                    GreyChecking_Load(sender, e)
                Else
                    CLEAR()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub gridupload_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridupload.CellDoubleClick
        Try
            If e.RowIndex = -1 Then Exit Sub

            If e.RowIndex >= 0 And gridupload.Item(GUSRNO.Index, e.RowIndex).Value <> Nothing Then

                GRIDUPLOADDOUBLECLICK = True
                TXTUPLOADSRNO.Text = gridupload.Item(GUSRNO.Index, e.RowIndex).Value
                txtuploadremarks.Text = gridupload.Item(GUREMARKS.Index, e.RowIndex).Value
                txtuploadname.Text = gridupload.Item(GUNAME.Index, e.RowIndex).Value
                PBSOFTCOPY.Image = gridupload.Item(GUIMGPATH.Index, e.RowIndex).Value

                TEMPUPLOADROW = e.RowIndex
                txtuploadremarks.Focus()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridupload_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gridupload.KeyDown
        Try
            If e.KeyCode = Keys.Delete And gridupload.RowCount > 0 Then
                'dont allow user if any of the grid line is in edit mode.....
                If GRIDUPLOADDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block

                gridupload.Rows.RemoveAt(gridupload.CurrentRow.Index)
                getsrno(gridupload)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtuploadname_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtuploadname.Validating
        Try
            If txtuploadremarks.Text.Trim <> "" And txtuploadname.Text.Trim <> "" And PBSOFTCOPY.ImageLocation <> "" Then
                FILLUPLOAD()
            Else
                MsgBox("Enter Proper Details")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTUPLOADSRNO_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTUPLOADSRNO.GotFocus
        If GRIDUPLOADDOUBLECLICK = False Then
            If gridupload.RowCount > 0 Then
                TXTUPLOADSRNO.Text = Val(gridupload.Rows(gridupload.RowCount - 1).Cells(0).Value) + 1
            Else
                TXTUPLOADSRNO.Text = 1
            End If
        End If
    End Sub

    Private Sub CMDUPLOAD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDUPLOAD.Click
        OpenFileDialog1.Filter = "Pictures (*.bmp;*.jpeg;*.png)|*.bmp;*.jpg;*.png"
        OpenFileDialog1.ShowDialog()
        TXTIMGPATH.Text = OpenFileDialog1.FileName
        On Error Resume Next
        If TXTIMGPATH.Text.Trim.Length <> 0 Then PBSOFTCOPY.ImageLocation = TXTIMGPATH.Text.Trim
    End Sub

    Private Sub CMDREMOVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDREMOVE.Click
        Try
            PBSOFTCOPY.Image = Nothing
            TXTIMGPATH.Clear()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDVIEW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDVIEW.Click
        Try
            If gridupload.SelectedRows.Count > 0 Then
                Dim objVIEW As New ViewImage
                objVIEW.pbsoftcopy.Image = PBSOFTCOPY.Image
                objVIEW.ShowDialog()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridupload_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridupload.RowEnter
        Try
            If e.RowIndex >= 0 Then PBSOFTCOPY.Image = gridupload.Rows(e.RowIndex).Cells(GUIMGPATH.Index).Value
        Catch ex As Exception
            Throw ex
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
                alParaval.Add(TXTGREYCHKNO.Text.Trim)
                alParaval.Add(TXTGREYRECDNO.Text.Trim)
                alParaval.Add(YearId)

                Dim OBJDEL As New ClsGreyChecking
                OBJDEL.alParaval = alParaval
                IntResult = OBJDEL.Delete()
                MsgBox("Entry Deleted")
                CLEAR()
                EDIT = False
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMDSELECTGREY_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSELECTGREY.Click
        Try

            If (EDIT = True And USEREDIT = False And USERVIEW = False) Or (EDIT = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If CMBNAME.Text = "" Then
                MsgBox("Select Weaver Name First !", MsgBoxStyle.Critical)
                Exit Sub
            End If

            Dim DTINV As New DataTable
            Dim OBJSELECTGREY As New SelectGrey
            OBJSELECTGREY.WEAVERNAME = CMBNAME.Text.Trim
            OBJSELECTGREY.ShowDialog()
            DTINV = OBJSELECTGREY.DT
            If DTINV.Rows.Count > 0 Then

                CMDSELECTGREY.Enabled = False

                TXTGREYRECDNO.Text = DTINV.Rows(0).Item("GREYRECNO")

                Dim objclscmn As New ClsCommon()
                'OPEN ALL LOOMS AS PER CLIENTS REQUIREMENT
                Dim DT As New DataTable
                If ClientName = "JASHOK" Then
                    DT = objclscmn.search(" DISTINCT CAST(ISNULL(LOOMMASTER_DESC.LOOM_NO, 0) AS INT) AS LOOMNO, ISNULL(GREYRECEIVEDWEAVER.GRECDWEAVER_NO, 0) AS GREYRECDNO, GREYRECEIVEDWEAVER.GRECDWEAVER_DATE AS GREYRECDDATE, ISNULL(GREYRECEIVEDWEAVER_DESC.GRECDWEAVER_PCS, 0) AS PCS", "", " GREYRECEIVEDWEAVER INNER JOIN GREYRECEIVEDWEAVER_DESC ON GREYRECEIVEDWEAVER.GRECDWEAVER_NO = GREYRECEIVEDWEAVER_DESC.GRECDWEAVER_NO AND GREYRECEIVEDWEAVER.GRECDWEAVER_YEARID = GREYRECEIVEDWEAVER_DESC.GRECDWEAVER_YEARID INNER JOIN LOOMMASTER ON LOOM_WEAVERID = GREYRECEIVEDWEAVER.GRECDWEAVER_LEDGERID INNER JOIN LOOMMASTER_DESC ON LOOMMASTER.LOOM_ID = LOOMMASTER_DESC.LOOM_ID", "  and GREYRECEIVEDWEAVER.GRECDWEAVER_NO = " & TXTGREYRECDNO.Text.Trim & " and GREYRECEIVEDWEAVER.GRECDWEAVER_DONE=0 AND GREYRECEIVEDWEAVER.GRECDWEAVER_YEARID = " & YearId)
                Else
                    DT = objclscmn.search(" ISNULL(GREYRECEIVEDWEAVER.GRECDWEAVER_NO, 0) AS GREYRECDNO, GREYRECEIVEDWEAVER.GRECDWEAVER_DATE AS GREYRECDDATE, ISNULL(GREYRECEIVEDWEAVER_DESC.GRECDWEAVER_LOOMNO, 0) AS LOOMNO, ISNULL(GREYRECEIVEDWEAVER_DESC.GRECDWEAVER_PCS, 0) AS PCS", "", " GREYRECEIVEDWEAVER INNER JOIN GREYRECEIVEDWEAVER_DESC ON GREYRECEIVEDWEAVER.GRECDWEAVER_NO = GREYRECEIVEDWEAVER_DESC.GRECDWEAVER_NO AND GREYRECEIVEDWEAVER.GRECDWEAVER_YEARID = GREYRECEIVEDWEAVER_DESC.GRECDWEAVER_YEARID", "  and GREYRECEIVEDWEAVER.GRECDWEAVER_NO = " & TXTGREYRECDNO.Text.Trim & " AND GREYRECEIVEDWEAVER_DESC.GRECDWEAVER_PCS > 0 and GREYRECEIVEDWEAVER.GRECDWEAVER_DONE=0 AND GREYRECEIVEDWEAVER.GRECDWEAVER_YEARID = " & YearId & " ORDER BY GREYRECEIVEDWEAVER_DESC.GRECDWEAVER_LOOMNO")
                End If
                If DT.Rows.Count > 0 Then
                    Dim DV As New DataView(DT)
                    DV.Sort = "LOOMNO ASC"

                    DTGREYRECDDATE.Text = DT.Rows(0).Item("GREYRECDDATE")
                    For Each dr As DataRowView In DV
                        For I As Integer = 1 To Val(dr("PCS"))
                            GRIDGREYCHK.Rows.Add(dr("LOOMNO"), 0, 0, 0, 0, 0, "")
                        Next
                    Next
                End If
                TOTAL()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDGREYCHK_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles GRIDGREYCHK.CellValidating
        Try
            Dim colNum As Integer = GRIDGREYCHK.Columns(e.ColumnIndex).Index
            If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return

            Select Case colNum

                Case GMTRS.Index, GWT.Index, GPICK.Index, GPANNA.Index
                    Dim dDebit As Decimal
                    Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

                    If bValid Then
                        If GRIDGREYCHK.CurrentCell.Value = Nothing Then GRIDGREYCHK.CurrentCell.Value = "0.000"
                        GRIDGREYCHK.CurrentCell.Value = Convert.ToDecimal(GRIDGREYCHK.Item(colNum, e.RowIndex).Value)
                        '' everything is good
                        TOTAL()
                    Else
                        MessageBox.Show("Invalid Number Entered")
                        e.Cancel = True
                        Exit Sub
                    End If

                Case GTP.Index
                    Dim dDebit As Integer
                    Dim bValid As Boolean = Integer.TryParse(e.FormattedValue.ToString, dDebit)

                    If bValid Then
                        If GRIDGREYCHK.CurrentCell.Value = Nothing Then GRIDGREYCHK.CurrentCell.Value = "0"
                        GRIDGREYCHK.CurrentCell.Value = Convert.ToInt32(GRIDGREYCHK.Item(colNum, e.RowIndex).Value)
                        '' everything is good
                        TOTAL()
                    Else
                        MessageBox.Show("Invalid Number Entered")
                        e.Cancel = True
                        Exit Sub
                    End If


            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GreyChecking_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If ALLOWMFG = False Then Exit Sub
    End Sub
End Class