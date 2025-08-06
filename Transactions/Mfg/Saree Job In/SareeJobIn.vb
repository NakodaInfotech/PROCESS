
Imports BL
Imports System.IO

Public Class SareeJobIn

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK, GRIDUPLOADDOUBLECLICK As Boolean
    Dim TEMPROW, TEMPUPLOADROW, TEMPJOBBERLOTNO As Integer
    Public EDIT As Boolean
    Public TEMPJINO As Integer
    Dim TEMPMSG As Integer

    Private Sub cmdexit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub cmdclear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMDCLEAR.Click
        CLEAR()
        EDIT = False
        CMBNAME.Focus()
    End Sub

    Sub CLEAR()

        TXTJINO.Clear()
        DTJIDATE.Text = Mydate

        CMBNAME.Text = ""
        CMBNAME.Enabled = True
        CMBQUALITY.Text = ""
        CMBQUALITY.Enabled = True
        TXTREMARKS.Clear()
        TXTTOTALCUT.Clear()
        TXTJOBBERLOTNO.Clear()

        EP.Clear()
        lbllocked.Visible = False
        PBlock.Visible = False


        TXTSRNO.Text = 1
        CMBLOTNO.Items.Clear()
        CMBLOTNO.Text = ""
        TXTQUALITY.Clear()
        TXTPCS.Clear()
        TXTCUT.Clear()
        TXTMTRS.Clear()
        TXTNARR.Clear()
        GRIDJI.RowCount = 0

        GETMAX_JI_NO()

        TXTBALMTRS.Clear()
        TXTTOTALPCS.Text = 0

        LBLTOTALMTRS.Text = 0.0

        GRIDDOUBLECLICK = False
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

    Sub TOTAL()
        Try
            TXTTOTALPCS.Text = 0
            LBLTOTALMTRS.Text = 0.0
            TXTTOTALCUT.Text = 0

            For Each ROW As DataGridViewRow In GRIDJI.Rows
                If ROW.Cells(GSRNO.Index).Value <> Nothing Then
                    LBLTOTALMTRS.Text = Format(Val(LBLTOTALMTRS.Text) + Val(ROW.Cells(GMTRS.Index).EditedFormattedValue), "0.00")
                    TXTTOTALCUT.Text = Format(Val(TXTTOTALCUT.Text.Trim) + Val(ROW.Cells(GCUT.Index).Value), "0.00")
                    TXTTOTALPCS.Text = Format(Val(LBLTOTALMTRS.Text) / Val(TXTTOTALCUT.Text.Trim), "0")
                End If
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETMAX_JI_NO()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax("ISNULL(MAX(JI_NO),0)+1", "SAREEJOBIN", "AND JI_YEARID=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTJINO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub SareeJobIn_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'JOBBER'") ''''TYPE = JOBBER
        If CMBQUALITY.Text.Trim = "" Then FILLGREY(CMBQUALITY, EDIT, " AND GREY_TYPE ='FINISHED'")
    End Sub

    Private Sub SareeJobIn_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'JOB IN'")
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
                Dim OBJJO As New ClsSareeJobIn

                OBJJO.alParaval.Add(TEMPJINO)
                OBJJO.alParaval.Add(YearId)
                dttable = OBJJO.SELECTJI()

                If dttable.Rows.Count > 0 Then
                    TXTJINO.Text = TEMPJINO
                    DTJIDATE.Text = dttable.Rows(0).Item("DATE")

                    CMBNAME.Text = dttable.Rows(0).Item("NAME").ToString
                    CMBNAME.Enabled = False
                    FILLLOTNO()

                    TXTJOBBERLOTNO.Text = Val(dttable.Rows(0).Item("JOBBERLOTNO"))
                    TEMPJOBBERLOTNO = Val(dttable.Rows(0).Item("JOBBERLOTNO"))
                    CMBQUALITY.Text = dttable.Rows(0).Item("MERCHANTNAME").ToString

                    TXTREMARKS.Text = dttable.Rows(0).Item("REMARKS").ToString

                    'OUTPCS AND OUTMTRS ARE KEPT IN MASTER TABLE INSTAED OF DESC TABLE
                    If Val(dttable.Rows(0).Item("OUTPCS")) > 0 Then
                        lbllocked.Visible = True
                        PBlock.Visible = True
                    End If



                    'ITEM GRID
                    For Each ROW As DataRow In dttable.Rows
                        GRIDJI.Rows.Add(Val(ROW("SRNO")), ROW("LOTNO").ToString, ROW("GREYQUALITY").ToString, Val(ROW("PCS")), Val(ROW("CUT")), Format(Val(ROW("MTRS")), "0.00"), ROW("NARRATION"), ROW("DONE"), ROW("LOTCOMPLETE"))
                        If Convert.ToBoolean(ROW("DONE")) = True Or Convert.ToBoolean(ROW("LOTCOMPLETE")) = True Then
                            GRIDJI.Rows(GRIDJI.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                            lbllocked.Visible = True
                            PBlock.Visible = True
                        End If
                    Next


                    'UPLOAD(GRID)
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.search(" SAREEJOBIN_UPLOAD.JI_SRNO AS GRIDSRNO, SAREEJOBIN_UPLOAD.JI_REMARKS AS REMARKS, SAREEJOBIN_UPLOAD.JI_NAME AS NAME, SAREEJOBIN_UPLOAD.JI_PHOTO AS IMGPATH ", "", " SAREEJOBIN_UPLOAD ", " AND SAREEJOBIN_UPLOAD.JI_NO = " & TEMPJINO & " AND JI_YEARID = " & YearId & " ORDER BY SAREEJOBIN_UPLOAD.JI_SRNO")
                    If DT.Rows.Count > 0 Then
                        For Each DTR As DataRow In DT.Rows
                            gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                        Next
                    End If

                    TOTAL()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        Try
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim IntResult As Integer
            Dim alParaval As New ArrayList

            alParaval.Add(Format(Convert.ToDateTime(DTJIDATE.Text.Trim).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBNAME.Text.Trim)
            alParaval.Add(CMBQUALITY.Text.Trim)
            alParaval.Add(Val(TXTJOBBERLOTNO.Text.Trim))

            alParaval.Add(TXTREMARKS.Text.Trim)

            alParaval.Add(Val(TXTTOTALPCS.Text.Trim))
            alParaval.Add(Val(TXTTOTALCUT.Text.Trim))
            alParaval.Add(Val(LBLTOTALMTRS.Text.Trim))

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)

            Dim SRNO As String = ""
            Dim LOTNO As String = ""
            Dim QUALITY As String = ""
            Dim PCS As String = ""
            Dim CUT As String = ""
            Dim MTRS As String = ""
            Dim NARR As String = ""


            For Each row As Windows.Forms.DataGridViewRow In GRIDJI.Rows
                If row.Cells(GSRNO.Index).Value <> Nothing Then
                    If SRNO = "" Then
                        SRNO = Val(row.Cells(GSRNO.Index).Value)
                        LOTNO = Val(row.Cells(GLOTNO.Index).Value)
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                        PCS = Format(Val(row.Cells(GPCS.Index).Value), "0")
                        CUT = Format(Val(row.Cells(GCUT.Index).Value), "0.00")
                        MTRS = Format(Val(row.Cells(GMTRS.Index).Value), "0.00")
                        If row.Cells(GNARR.Index).Value <> Nothing Then NARR = row.Cells(GNARR.Index).Value.ToString Else NARR = ""

                    Else

                        SRNO = SRNO & "|" & row.Cells(GSRNO.Index).Value
                        LOTNO = LOTNO & "|" & Val(row.Cells(GLOTNO.Index).Value)
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        PCS = PCS & "|" & Format(Val(row.Cells(GPCS.Index).Value), "0")
                        CUT = CUT & "|" & Format(Val(row.Cells(GCUT.Index).Value), "0.00")
                        MTRS = MTRS & "|" & Format(Val(row.Cells(GMTRS.Index).Value), "0.00")
                        If row.Cells(GNARR.Index).Value <> Nothing Then NARR = NARR & "|" & row.Cells(GNARR.Index).Value.ToString Else NARR = NARR & "|" & ""

                    End If
                End If
            Next

            alParaval.Add(SRNO)
            alParaval.Add(LOTNO)
            alParaval.Add(QUALITY)
            alParaval.Add(PCS)
            alParaval.Add(CUT)
            alParaval.Add(MTRS)
            alParaval.Add(NARR)

            Dim OBJJO As New ClsSareeJobIn
            OBJJO.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DT As DataTable = OBJJO.SAVE()
                TEMPJINO = DT.Rows(0).Item(0)
                MsgBox("Details Added")

            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPJINO)
                IntResult = OBJJO.UPDATE()
                EDIT = False
                MsgBox("Details Updated")

            End If

            If gridupload.RowCount > 0 Then SAVEUPLOAD()
            'CLEAR()
            'SHOW NEXT BILL ON EDIT MODE DONT CLEAR
            Call toolnext_Click(sender, e)
            DTJIDATE.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub SAVEUPLOAD()

        Try
            Dim OBJBEAMISSUE As New ClsSareeJobIn


            For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                Dim MS As New IO.MemoryStream
                Dim ALPARAVAL As New ArrayList
                If row.Cells(GUSRNO.Index).Value <> Nothing Then
                    ALPARAVAL.Add(TEMPJINO)
                    ALPARAVAL.Add(row.Cells(GUSRNO.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUREMARKS.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUNAME.Index).Value)

                    PBSOFTCOPY.Image = row.Cells(GUIMGPATH.Index).Value
                    PBSOFTCOPY.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                    ALPARAVAL.Add(MS.ToArray)
                    ALPARAVAL.Add(YearId)

                    OBJBEAMISSUE.alParaval = ALPARAVAL
                    Dim INTRES As Integer = OBJBEAMISSUE.SAVEUPLOAD()
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

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True


        If DTJIDATE.Text = "__/__/____" Then
            EP.SetError(DTJIDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(DTJIDATE.Text) Then
                EP.SetError(DTJIDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        If CMBNAME.Text.Trim.Length = 0 Then
            EP.SetError(CMBNAME, "Please Fill Jobber Name")
            bln = False
        End If

        If CMBQUALITY.Text.Trim.Length = 0 Then
            EP.SetError(CMBQUALITY, "Please Fill Merchant Name")
            bln = False
        End If

        If Val(TXTJOBBERLOTNO.Text.Trim) = 0 Then
            EP.SetError(TXTJOBBERLOTNO, "Please Enter Jobber's Lot No")
            bln = False
        End If

        If lbllocked.Visible = True Then
            EP.SetError(lbllocked, "Unable to Modify, entry Locked")
            bln = False
        End If

        If GRIDJI.RowCount = 0 Then
            EP.SetError(CMBNAME, "Select Item Details")
            bln = False
        End If
        Return bln
    End Function

    Private Sub DTJIDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTJIDATE.GotFocus
        DTJIDATE.SelectAll()
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

    Private Sub cmbname_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'JOBBER'") ''''TYPE = JOBBER
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBNAME.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'JOBBER'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBNAME.Validated
        Try
            If CMBNAME.Text.Trim <> "" Then FILLLOTNO()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLLOTNO()
        Try
            CMBLOTNO.Items.Clear()
            CMBLOTNO.Text = ""
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.search("DISTINCT LOTNO ", "", " JOBBERPENDINGLOT AS A ", " AND NOT EXISTS (SELECT * FROM SAREELOTDONE AS B INNER JOIN LEDGERS ON B.LOTDONE_LEDGERID = LEDGERS.ACC_ID WHERE A.NAME = LEDGERS.Acc_cmpname AND A.YEARID = LOTDONE_YEARID AND A.LOTNO = B.LOTDONE_LOTNO) AND NAME = '" & CMBNAME.Text.Trim & "' AND YEARID = " & YearId)
            If DT.Rows.Count > 0 Then
                For Each DTROW As DataRow In DT.Rows
                    CMBLOTNO.Items.Add(DTROW("LOTNO"))
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'JOBBER'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "JOBBER")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            GRIDJI.RowCount = 0
LINE1:
            TEMPJINO = Val(TXTJINO.Text) - 1
Line2:
            If TEMPJINO > 0 Then

                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" JI_NO ", "", "  SAREEJOBIN", " AND JI_NO = '" & TEMPJINO & "' AND SAREEJOBIN.JI_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    EDIT = True
                    SareeJobIn_Load(sender, e)
                Else
                    TEMPJINO = Val(TEMPJINO - 1)
                    GoTo Line2
                End If
            Else
                CLEAR()
                EDIT = False
            End If

            If GRIDJI.RowCount = 0 And TEMPJINO > 1 Then
                TXTJINO.Text = TEMPJINO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            GRIDJI.RowCount = 0
LINE1:
            TEMPJINO = Val(TXTJINO.Text) + 1
            GETMAX_JI_NO()
            Dim MAXNO As Integer = TXTJINO.Text.Trim
            CLEAR()
            If Val(TXTJINO.Text) - 1 >= TEMPJINO Then
                EDIT = True
                SareeJobIn_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDJI.RowCount = 0 And TEMPJINO < MAXNO Then
                TXTJINO.Text = TEMPJINO
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
                GRIDJI.RowCount = 0
                TEMPJINO = Val(tstxtbillno.Text)
                If TEMPJINO > 0 Then
                    EDIT = True
                    SareeJobIn_Load(sender, e)
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
        Call cmdok_Click(sender, e)
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tooldelete.Click
        Call cmddelete_Click(sender, e)
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDDELETE.Click
        Dim IntResult As Integer
        Try
            If EDIT = True Then

                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If lbllocked.Visible = True Then
                    MsgBox("Unable to Delete, Job In Locked", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                If MsgBox("Delete Job Out?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(TEMPJINO)
                    alParaval.Add(YearId)

                    Dim OBJSO As New ClsSareeJobIn()
                    OBJSO.alParaval = alParaval
                    IntResult = OBJSO.DELETE()
                    MsgBox("Job In Deleted")
                    CLEAR()
                    EDIT = False
                    DTJIDATE.Focus()
                End If
            Else
                MsgBox("Delete is only in Edit Mode")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub GRIDJI_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDJI.CellDoubleClick
        Try
            If e.RowIndex >= 0 And GRIDJI.Item(GSRNO.Index, e.RowIndex).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                TXTSRNO.Text = GRIDJI.Item(GSRNO.Index, e.RowIndex).Value.ToString
                CMBLOTNO.Text = GRIDJI.Item(GLOTNO.Index, e.RowIndex).Value.ToString
                TXTQUALITY.Text = GRIDJI.Item(GQUALITY.Index, e.RowIndex).Value.ToString
                TXTPCS.Text = Val(GRIDJI.Item(GPCS.Index, e.RowIndex).Value)
                TXTCUT.Text = Val(GRIDJI.Item(GCUT.Index, e.RowIndex).Value)
                TXTMTRS.Text = Val(GRIDJI.Item(GMTRS.Index, e.RowIndex).Value)
                TXTNARR.Text = GRIDJI.Item(GNARR.Index, e.RowIndex).Value.ToString

                TEMPROW = e.RowIndex
                TXTPCS.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDJI_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDJI.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDJI.RowCount > 0 Then
                'dont allow user if any of the grid line is in edit mode.....
                'cmbitemname.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block
                GRIDJI.Rows.RemoveAt(GRIDJI.CurrentRow.Index)
                getsrno(GRIDJI)
                TOTAL()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim OBJJO As New SareeJobInDetails
            OBJJO.MdiParent = MDIMain
            OBJJO.Show()
        Catch EX As Exception
            Throw EX
        End Try
    End Sub

    Private Sub CMBQUALITY_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBQUALITY.Enter
        Try
            If CMBQUALITY.Text.Trim = "" Then FILLGREY(CMBQUALITY, EDIT, " AND GREY_TYPE ='FINISHED'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBQUALITY.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJGREY As New SelectGreyQuality
                OBJGREY.WHERECLAUSE = " AND GREY_TYPE ='FINISHED'"
                OBJGREY.ShowDialog()
                If OBJGREY.TEMPNAME <> "" Then CMBQUALITY.Text = OBJGREY.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBQUALITY.Validating
        Try
            If CMBQUALITY.Text.Trim <> "" Then GREYVALIDATE(CMBQUALITY, e, Me, " AND GREY_TYPE = 'FINISHED'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBLOTNO_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBLOTNO.Validating
        Try
            If CMBLOTNO.Text.Trim <> "" Then
                'GET BALMTRS  AND QUALITY FROM VIEW
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("QUALITY, SUM(MTRS) AS MTRS", "", " JOBBERPENDINGLOT AS A ", " AND NOT EXISTS (SELECT * FROM SAREELOTDONE AS B INNER JOIN LEDGERS ON B.LOTDONE_LEDGERID = LEDGERS.ACC_ID WHERE A.NAME = LEDGERS.Acc_cmpname AND A.YEARID = LOTDONE_YEARID AND A.LOTNO = B.LOTDONE_LOTNO AND B.LOTDONE_YEARID = " & YearId & ") AND NAME = '" & CMBNAME.Text.Trim & "' AND LOTNO = " & Val(CMBLOTNO.Text.Trim) & " AND YEARID = " & YearId & " GROUP BY QUALITY")
                If DT.Rows.Count > 0 Then
                    TXTBALMTRS.Text = Format(Val(DT.Rows(0).Item("MTRS")), "0.00")
                    TXTQUALITY.Text = DT.Rows(0).Item("QUALITY")
                Else
                    MsgBox("Invalid Lot no Entered", MsgBoxStyle.Critical)
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try
            If GRIDDOUBLECLICK = False Then
                GRIDJI.Rows.Add(Val(TXTSRNO.Text.Trim), CMBLOTNO.Text.Trim, TXTQUALITY.Text.Trim, Format(Val(TXTPCS.Text.Trim), "0"), Format(Val(TXTCUT.Text.Trim), "0.00"), Format(Val(TXTMTRS.Text.Trim), "0.00"), TXTNARR.Text.Trim, 0, 0)
                getsrno(GRIDJI)
            ElseIf GRIDDOUBLECLICK = True Then
                GRIDJI.Item(GSRNO.Index, TEMPROW).Value = Val(TXTSRNO.Text.Trim)
                GRIDJI.Item(GLOTNO.Index, TEMPROW).Value = CMBLOTNO.Text.Trim
                GRIDJI.Item(GQUALITY.Index, TEMPROW).Value = TXTQUALITY.Text.Trim
                GRIDJI.Item(GPCS.Index, TEMPROW).Value = Format(Val(TXTPCS.Text.Trim), "0")
                GRIDJI.Item(GCUT.Index, TEMPROW).Value = Format(Val(TXTCUT.Text.Trim), "0.00")
                GRIDJI.Item(GMTRS.Index, TEMPROW).Value = Format(Val(TXTMTRS.Text.Trim), "0.00")
                GRIDJI.Item(GNARR.Index, TEMPROW).Value = TXTNARR.Text.Trim()
                GRIDDOUBLECLICK = False
            End If
            TOTAL()
            GRIDJI.FirstDisplayedScrollingRowIndex = GRIDJI.RowCount - 1

            TXTSRNO.Clear()
            CMBLOTNO.Text = ""
            TXTQUALITY.Clear()
            TXTPCS.Clear()
            TXTCUT.Clear()
            TXTMTRS.Clear()
            TXTNARR.Clear()

            If GRIDJI.RowCount > 0 Then TXTSRNO.Text = Val(GRIDJI.Rows(GRIDJI.RowCount - 1).Cells(0).Value) + 1 Else TXTSRNO.Text = 1
            CMBLOTNO.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function CHECKGRID() As Boolean
        Dim bln As Boolean = True
        For Each ROW As DataGridViewRow In GRIDJI.Rows
            If GRIDDOUBLECLICK = False Or (GRIDDOUBLECLICK = True And TEMPROW <> ROW.Index) Then
                If (ROW.Cells(GLOTNO.Index).Value = CMBLOTNO.Text.Trim) Then
                    bln = False
                End If
            End If
        Next
        Return bln
    End Function

    Private Sub TXTNARR_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTNARR.Validated
        Try
            If CMBLOTNO.Text.Trim <> "" And TXTQUALITY.Text.Trim <> "" And Val(TXTPCS.Text.Trim) > 0 And Val(TXTCUT.Text.Trim) > 0 And Val(TXTMTRS.Text.Trim) > 0 Then
                If Not CHECKGRID() Then
                    MsgBox("Lot No Already Selected in Grid Below", MsgBoxStyle.Critical)
                    Exit Sub
                End If
                FILLGRID()
            Else
                MsgBox("Enter Valid Details", MsgBoxStyle.Critical)
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCUT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCUT.KeyPress, TXTMTRS.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub TXTPCS_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTPCS.KeyPress, CMBLOTNO.KeyPress, TXTJOBBERLOTNO.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Sub CALC()
        Try
            TXTMTRS.Text = Format(Val(TXTPCS.Text.Trim) * Val(TXTCUT.Text.Trim), "0.00")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTPCS_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTPCS.Validated, TXTCUT.Validated
        CALC()
    End Sub

    Private Sub TXTJOBBERLOTNO_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTJOBBERLOTNO.Validating
        Try
            'ASK NIMESH BHAI DO WE NEED TO VALIDATE OR NOT
            'CHECK FOR DUPLICATION
            'If EDIT = False Or (EDIT = True And TEMPJOBBERLOTNO <> Val(TXTJOBBERLOTNO.Text.Trim)) Then
            '    Dim OBJCMN As New ClsCommon
            '    Dim DT As DataTable = OBJCMN.search("JI_NO AS JINO", "", " SAREEJOBIN ", " AND JI_JOBBERLOTNO = " & Val(TXTJOBBERLOTNO.Text.Trim) & " AND JI_YEARID = " & YearId)
            '    If DT.Rows.Count > 0 Then
            '        MsgBox("Lot No already Present", MsgBoxStyle.Critical)
            '        e.Cancel = True
            '        Exit Sub
            '    End If
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDLOTDONE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDLOTDONE.Click
        Try
            Dim OBJLOTDONE As New SareeLotDone
            OBJLOTDONE.MdiParent = MDIMain
            OBJLOTDONE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class