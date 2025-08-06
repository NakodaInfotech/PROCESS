
Imports BL
Imports System.IO

Public Class SareeJobOut

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK, GRIDUPLOADDOUBLECLICK As Boolean
    Dim TEMPROW, TEMPUPLOADROW As Integer
    Public EDIT As Boolean
    Public TEMPJONO As Integer
    Dim TEMPMSG As Integer

    Private Sub cmdexit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdclear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        CLEAR()
        EDIT = False
        CMBDELIVERYAT.Focus()
    End Sub

    Sub CLEAR()

        TXTSERIES.Clear()
        CMBTYPE.SelectedIndex = 0
        TXTJONO.Clear()
        DTJODATE.Text = Mydate

        CMBNAME.Text = ""
        CMBNAME.Enabled = True
        CMBDELIVERYAT.Text = ""
        CMBDELIVERYAT.Enabled = True
        CMBOURGODOWN.Text = ""
        CMBOURGODOWN.Enabled = True

        CMBTRANS.Text = ""
        TXTLRNO.Clear()
        DTLRDATE.Clear()

        txtremarks.Clear()

        EP.Clear()
        lbllocked.Visible = False
        PBlock.Visible = False


        GRIDJO.RowCount = 0

        GETMAX_JO_NO()

        LBLTOTALBALES.Text = 0.0
        LBLTOTALPCS.Text = 0
        LBLTOTALMTRS.Text = 0.0


        GRIDDOUBLECLICK = False
        GRIDUPLOADDOUBLECLICK = False
        CMDSELECTSTOCK.Enabled = True

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
            LBLTOTALBALES.Text = 0
            LBLTOTALPCS.Text = 0
            LBLTOTALMTRS.Text = 0.0
            For Each ROW As DataGridViewRow In GRIDJO.Rows
                If ROW.Cells(GSRNO.Index).Value <> Nothing Then
                    LBLTOTALPCS.Text = Format(Val(LBLTOTALPCS.Text) + Val(ROW.Cells(GPCS.Index).EditedFormattedValue), "0")
                    LBLTOTALMTRS.Text = Format(Val(LBLTOTALMTRS.Text) + Val(ROW.Cells(GMTRS.Index).EditedFormattedValue), "0.00")
                    If Val(ROW.Cells(GBALENO.Index).Value) > 0 Then LBLTOTALBALES.Text = Val(LBLTOTALBALES.Text) + 1
                End If
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETMAX_JO_NO()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax("ISNULL(MAX(JO_NO),0)+1", "SAREEJOBOUT", "AND JO_YEARID=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTJONO.Text = DTTABLE.Rows(0).Item(0)
        GETMAXSERIES(TXTSERIES)
    End Sub

    Private Sub SareeJobOut_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        If CMBDELIVERYAT.Text.Trim = "" Then fillname(CMBDELIVERYAT, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND ACC_SUBTYPE = 'PROCESSOR'")
        If CMBOURGODOWN.Text.Trim = "" Then fillGODOWN(CMBOURGODOWN, EDIT, " AND GODOWN_ISOUR='TRUE'")
        If CMBTRANS.Text = "" Then fillname(CMBTRANS, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'TRANSPORT'")
    End Sub

    Private Sub SareeJobOut_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'JOB OUT'")
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
                Dim OBJJO As New ClsSareeJobOut

                OBJJO.alParaval.Add(TEMPJONO)
                OBJJO.alParaval.Add(YearId)
                dttable = OBJJO.SELECTJO()

                If dttable.Rows.Count > 0 Then
                    CMBNAME.Focus()

                    TXTSERIES.Text = Val(dttable.Rows(0).Item("SERIES"))
                    TXTJONO.Text = TEMPJONO
                    DTJODATE.Text = dttable.Rows(0).Item("DATE")
                    CMBTYPE.Text = dttable.Rows(0).Item("TYPE").ToString

                    CMBNAME.Text = dttable.Rows(0).Item("NAME").ToString
                    CMBDELIVERYAT.Text = dttable.Rows(0).Item("DELIVERYAT").ToString
                    CMBOURGODOWN.Text = dttable.Rows(0).Item("GODOWN").ToString
                    CMBTRANS.Text = dttable.Rows(0).Item("TRANS").ToString
                    TXTLRNO.Text = dttable.Rows(0).Item("LRNO").ToString
                    If dttable.Rows(0).Item("LRDATE") <> "" Then DTLRDATE.Text = Format(Convert.ToDateTime(dttable.Rows(0).Item("LRDATE")).Date, "dd/MM/yyyy")

                    txtremarks.Text = dttable.Rows(0).Item("REMARKS").ToString

                    'ITEM GRID
                    For Each ROW As DataRow In dttable.Rows
                        GRIDJO.Rows.Add(Val(ROW("SRNO")), ROW("GREYQUALITY").ToString, ROW("LOTNO").ToString, ROW("BALENO"), Val(ROW("PCS")), Format(Val(ROW("MTRS")), "0.00"), ROW("NARRATION"), ROW("FROMNO"), ROW("FROMSRNO"), ROW("GRIDTYPE"), ROW("DONE"))
                        If Convert.ToBoolean(ROW("DONE")) = True Then
                            GRIDJO.Rows(GRIDJO.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                            lbllocked.Visible = True
                            PBlock.Visible = True
                        End If
                    Next


                    'UPLOAD(GRID)
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.search(" SAREEJOBOUT_UPLOAD.JO_SRNO AS GRIDSRNO, SAREEJOBOUT_UPLOAD.JO_REMARKS AS REMARKS, SAREEJOBOUT_UPLOAD.JO_NAME AS NAME, SAREEJOBOUT_UPLOAD.JO_PHOTO AS IMGPATH ", "", " SAREEJOBOUT_UPLOAD ", " AND SAREEJOBOUT_UPLOAD.JO_NO = " & TEMPJONO & " AND JO_YEARID = " & YearId & " ORDER BY SAREEJOBOUT_UPLOAD.JO_SRNO")
                    If DT.Rows.Count > 0 Then
                        For Each DTR As DataRow In DT.Rows
                            gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                        Next
                    End If

                    CMDSELECTSTOCK.Enabled = False
                    CMBNAME.Enabled = False
                    CMBDELIVERYAT.Enabled = False
                    CMBOURGODOWN.Enabled = False
                    TOTAL()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim IntResult As Integer
            Dim alParaval As New ArrayList

            alParaval.Add(Format(Convert.ToDateTime(DTJODATE.Text.Trim).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBTYPE.Text.Trim)

            alParaval.Add(CMBNAME.Text.Trim)
            alParaval.Add(CMBDELIVERYAT.Text.Trim)
            alParaval.Add(CMBOURGODOWN.Text.Trim)
            alParaval.Add(CMBTRANS.Text.Trim)
            alParaval.Add(TXTLRNO.Text.Trim)
            If DTLRDATE.Text <> "__/__/____" Then alParaval.Add(Format(Convert.ToDateTime(DTLRDATE.Text).Date, "MM/dd/yyyy")) Else alParaval.Add("")
            alParaval.Add(txtremarks.Text.Trim)

            alParaval.Add(Val(LBLTOTALBALES.Text.Trim))
            alParaval.Add(Val(LBLTOTALPCS.Text.Trim))
            alParaval.Add(Val(LBLTOTALMTRS.Text.Trim))

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)

            Dim SRNO As String = ""
            Dim QUALITY As String = ""
            Dim LOTNO As String = ""
            Dim BALENO As String = ""
            Dim PCS As String = ""
            Dim MTRS As String = ""
            Dim NARR As String = ""
            Dim FROMNO As String = ""
            Dim FROMSRNO As String = ""
            Dim GRIDTYPE As String = ""
            Dim COMPLETED As String = ""


            For Each row As Windows.Forms.DataGridViewRow In GRIDJO.Rows
                If row.Cells(GSRNO.Index).Value <> Nothing Then
                    If SRNO = "" Then
                        SRNO = Val(row.Cells(GSRNO.Index).Value)
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                        LOTNO = row.Cells(GLOTNO.Index).Value.ToString
                        BALENO = row.Cells(GBALENO.Index).Value.ToString
                        PCS = Format(Val(row.Cells(GPCS.Index).Value), "0")
                        MTRS = Format(Val(row.Cells(GMTRS.Index).Value), "0.00")
                        If row.Cells(GNARR.Index).Value <> Nothing Then NARR = row.Cells(GNARR.Index).Value.ToString Else NARR = ""
                        FROMNO = row.Cells(GFROMNO.Index).Value.ToString
                        FROMSRNO = row.Cells(GFROMSRNO.Index).Value.ToString
                        GRIDTYPE = row.Cells(GTYPE.Index).Value.ToString
                        COMPLETED = row.Cells(GCOMPLETED.Index).Value

                    Else

                        SRNO = SRNO & "|" & row.Cells(GSRNO.Index).Value
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        LOTNO = LOTNO & "|" & row.Cells(GLOTNO.Index).Value.ToString
                        BALENO = BALENO & "|" & row.Cells(GBALENO.Index).Value.ToString
                        PCS = PCS & "|" & Format(Val(row.Cells(GPCS.Index).Value), "0")
                        MTRS = MTRS & "|" & Format(Val(row.Cells(GMTRS.Index).Value), "0.00")
                        If row.Cells(GNARR.Index).Value <> Nothing Then NARR = NARR & "|" & row.Cells(GNARR.Index).Value.ToString Else NARR = NARR & "|" & ""
                        FROMNO = FROMNO & "|" & row.Cells(GFROMNO.Index).Value.ToString
                        FROMSRNO = FROMSRNO & "|" & row.Cells(GFROMSRNO.Index).Value.ToString
                        GRIDTYPE = GRIDTYPE & "|" & row.Cells(GTYPE.Index).Value.ToString
                        COMPLETED = COMPLETED & "|" & row.Cells(GCOMPLETED.Index).Value.ToString

                    End If
                End If
            Next

            alParaval.Add(SRNO)
            alParaval.Add(QUALITY)
            alParaval.Add(LOTNO)
            alParaval.Add(BALENO)
            alParaval.Add(PCS)
            alParaval.Add(MTRS)
            alParaval.Add(NARR)
            alParaval.Add(FROMNO)
            alParaval.Add(FROMSRNO)
            alParaval.Add(GRIDTYPE)
            alParaval.Add(COMPLETED)

            Dim OBJJO As New ClsSareeJobOut
            OBJJO.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DT As DataTable = OBJJO.SAVE()
                TEMPJONO = DT.Rows(0).Item(0)
                MsgBox("Details Added")

            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPJONO)
                IntResult = OBJJO.Update()
                EDIT = False
                MsgBox("Details Updated")

            End If
            PRINTREPORT(TEMPJONO)

            If gridupload.RowCount > 0 Then SAVEUPLOAD()
            'CLEAR()
            'SHOW NEXT BILL ON EDIT MODE DONT CLEAR
            Call toolnext_Click(sender, e)
            DTJODATE.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub SAVEUPLOAD()

        Try
            Dim OBJBEAMISSUE As New ClsSareeJobOut


            For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                Dim MS As New IO.MemoryStream
                Dim ALPARAVAL As New ArrayList
                If row.Cells(GUSRNO.Index).Value <> Nothing Then
                    ALPARAVAL.Add(TEMPJONO)
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


        If DTJODATE.Text = "__/__/____" Then
            EP.SetError(DTJODATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(DTJODATE.Text) Then
                EP.SetError(DTJODATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        If CMBNAME.Text.Trim.Length = 0 Then
            EP.SetError(CMBNAME, "Please Fill Jobber Name")
            bln = False
        End If

        If CMBTRANS.Text.Trim.Length = 0 Then
            EP.SetError(CMBTRANS, " Please Select Transport")
            bln = False
        End If

        If lbllocked.Visible = True Then
            EP.SetError(lbllocked, "Unable to Modify, entry Locked")
            bln = False
        End If

        If GRIDJO.RowCount = 0 Then
            EP.SetError(CMBTRANS, "Select Stock")
            bln = False
        End If
        Return bln
    End Function

    Private Sub DTJODATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        DTJODATE.SelectAll()
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

    Private Sub CMBOURGODOWN_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBOURGODOWN.Enter
        Try
            If CMBOURGODOWN.Text.Trim <> "" Then fillGODOWN(CMBOURGODOWN, EDIT, " AND GODOWN_ISOUR='TRUE'")
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
                OBJGODOWN.SEARCH = " AND GODOWN_ISOUR = 'True'"
                OBJGODOWN.ShowDialog()
                If OBJGODOWN.TEMPNAME <> "" Then CMBOURGODOWN.Text = OBJGODOWN.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBOURGODOWN_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBOURGODOWN.Validating
        Try
            If CMBOURGODOWN.Text.Trim <> "" Then GODOWNVALIDATE(CMBOURGODOWN, e, Me, " AND GODOWN_ISOUR='TRUE'")
        Catch ex As Exception
            Throw ex
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

    Private Sub cmbname_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, CMBCODE, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'JOBBER'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "JOBBER")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBTRANS.Enter
        Try
            If CMBTRANS.Text.Trim = "" Then fillname(CMBTRANS, EDIT, "AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE= 'TRANSPORT'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBTRANS.Validating
        Try
            If CMBTRANS.Text.Trim <> "" Then namevalidate(CMBTRANS, CMBCODE, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'", "Sundry Creditors", "TRANSPORT")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBTRANS.KeyDown
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

    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            GRIDJO.RowCount = 0
LINE1:
            TEMPJONO = Val(TXTJONO.Text) - 1
Line2:
            If TEMPJONO > 0 Then

                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" JO_NO ", "", "  SAREEJOBOUT", " AND JO_NO = '" & TEMPJONO & "' AND SAREEJOBOUT.JO_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    EDIT = True
                    SareeJobOut_Load(sender, e)
                Else
                    TEMPJONO = Val(TEMPJONO - 1)
                    GoTo Line2
                End If
            Else
                CLEAR()
                EDIT = False
            End If

            If GRIDJO.RowCount = 0 And TEMPJONO > 1 Then
                TXTJONO.Text = TEMPJONO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            GRIDJO.RowCount = 0
LINE1:
            TEMPJONO = Val(TXTJONO.Text) + 1
            GETMAX_JO_NO()
            Dim MAXNO As Integer = TXTJONO.Text.Trim
            CLEAR()
            If Val(TXTJONO.Text) - 1 >= TEMPJONO Then
                EDIT = True
                SareeJobOut_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDJO.RowCount = 0 And TEMPJONO < MAXNO Then
                TXTJONO.Text = TEMPJONO
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
                GRIDJO.RowCount = 0
                TEMPJONO = Val(tstxtbillno.Text)
                If TEMPJONO > 0 Then
                    EDIT = True
                    SareeJobOut_Load(sender, e)
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

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Dim IntResult As Integer
        Try
            If EDIT = True Then

                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If lbllocked.Visible = True Then
                    MsgBox("Unable to Delete, Job Out Locked", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                If MsgBox("Delete Job Out?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(TEMPJONO)
                    alParaval.Add(YearId)

                    Dim OBJSO As New ClsSareeJobOut()
                    OBJSO.alParaval = alParaval
                    IntResult = OBJSO.DELETE()
                    MsgBox("Job Out Deleted")
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

    Private Sub CMDSELECTSTOCK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSELECTSTOCK.Click
        Try
            If CMBTYPE.Text.Trim = "FINISHED" Then
                If CMBDELIVERYAT.Text.Trim <> "" Then

                    Dim OBJSELECTSTOCK As New SelectBaleStock
                    OBJSELECTSTOCK.TEMPGODOWNNAME = CMBOURGODOWN.Text.Trim
                    OBJSELECTSTOCK.PROCESSORNAME = CMBDELIVERYAT.Text.Trim
                    Dim DTBEAMSTOCK As DataTable = OBJSELECTSTOCK.DT
                    OBJSELECTSTOCK.ShowDialog()
                    If DTBEAMSTOCK.Rows.Count > 0 Then
                        For Each ROW As DataRow In DTBEAMSTOCK.Rows
                            GRIDJO.Rows.Add(0, ROW("GREYQUALITY"), ROW("LOTNO"), ROW("BALENO"), Val(ROW("PCS")), Format(Val(ROW("MTRS")), "0.000"), "", ROW("FROMNO"), ROW("FROMSRNO"), ROW("TYPE"), 0)
                        Next
                        CMDSELECTSTOCK.Enabled = False
                        TOTAL()
                        getsrno(GRIDJO)
                    End If
                Else
                    MsgBox("Select Processor Name")
                    CMBDELIVERYAT.Focus()
                End If

            Else
                'FETCH GREYSTOCK FROM OUR GODOWN
                If CMBOURGODOWN.Text.Trim <> "" Then

                    Dim OBJSELECTSTOCK As New SelectGreyStock
                    OBJSELECTSTOCK.TEMPGODOWNNAME = CMBOURGODOWN.Text.Trim
                    Dim DTBEAMSTOCK As DataTable = OBJSELECTSTOCK.DT
                    OBJSELECTSTOCK.ShowDialog()
                    If DTBEAMSTOCK.Rows.Count > 0 Then
                        For Each ROW As DataRow In DTBEAMSTOCK.Rows
                            GRIDJO.Rows.Add(0, ROW("GREYQUALITY"), 0, 0, Val(ROW("PCS")), Format(Val(ROW("MTRS")), "0.00"), 0, 0, "", 0)
                        Next
                        CMDSELECTSTOCK.Enabled = False
                        TOTAL()
                        getsrno(GRIDJO)
                    End If
                Else
                    MsgBox("Select Godown Name")
                    CMBNAME.Focus()
                End If

            End If
            TOTAL()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDJO_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDJO.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDJO.RowCount > 0 Then
                'dont allow user if any of the grid line is in edit mode.....
                'cmbitemname.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block
                GRIDJO.Rows.RemoveAt(GRIDJO.CurrentRow.Index)
                getsrno(GRIDJO)
                TOTAL()

            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then PRINTREPORT(TEMPJONO)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PRINTREPORT(JONO As Integer)
        Try
            If MsgBox("Wish to Print Job Out?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            Dim OBJJOB As New JobOutDesign
            OBJJOB.MdiParent = MDIMain
            OBJJOB.FRMSTRING = "JOBOUT"
            OBJJOB.WHERECLAUSE = "{SAREEJOBOUT.JO_NO} = " & JONO & " AND {SAREEJOBOUT.JO_YEARID} = " & YearId
            OBJJOB.Show()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDJO_CellValidating(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles GRIDJO.CellValidating
        Try
            'Dim colNum As Integer = GRIDJO.Columns(e.ColumnIndex).Index
            'If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return

            'Select Case colNum

            '    Case GMTRS.Index, GPCS.Index ', gcount.Index
            '        Dim dDebit As Decimal
            '        Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

            '        If bValid Then
            '            If GRIDJO.CurrentCell.Value = Nothing Then GRIDJO.CurrentCell.Value = "0.000"
            '            GRIDJO.CurrentCell.Value = Convert.ToDecimal(GRIDJO.Item(colNum, e.RowIndex).Value)
            '            '' everything is good
            '            TOTAL()
            '        Else
            '            MessageBox.Show("Invalid Number Entered")
            '            e.Cancel = True
            '            Exit Sub
            '        End If

            'End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim OBJJO As New SareeJobOutDetails
            OBJJO.MdiParent = MDIMain
            OBJJO.Show()
        Catch EX As Exception
            Throw EX
        End Try
    End Sub

    Private Sub CMBDELIVERYAT_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBDELIVERYAT.Enter
        Try
            If CMBDELIVERYAT.Text.Trim = "" Then fillname(CMBDELIVERYAT, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND ACC_SUBTYPE = 'PROCESSOR'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDELIVERYAT_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBDELIVERYAT.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBDELIVERYAT.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDELIVERYAT_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBDELIVERYAT.Validating
        Try
            If CMBDELIVERYAT.Text.Trim <> "" Then namevalidate(CMBDELIVERYAT, cmbcode, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "PROCESSOR")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class