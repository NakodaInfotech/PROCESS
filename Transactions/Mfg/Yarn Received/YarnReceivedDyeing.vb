
Imports BL
Imports System.Windows.Forms
Imports System.IO

Public Class YarnReceivedDyeing

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDRECDDOUBLECLICK, GRIDUPLOADDOUBLECLICK As Boolean
    Dim TEMPROW, TEMPUPLOADROW As Integer
    Public EDIT As Boolean
    Public TEMPRECDNO As Integer

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        CLEAR()
        EDIT = False
        CMBOURGODOWN.Focus()
    End Sub

    Sub CLEAR()

        CMBOURGODOWN.Text = GETDEFAULTGODOWN()
        cmbname.Text = ""
        cmbtrans.Text = ""
        TXTVEHICLENO.Clear()
        TXTCHALLANNO.Clear()

        DTRECDDATE.Text = Mydate
        txtremarks.Clear()
        LBLTOTALBAGS.Text = 0
        LBLBAGNOCOUNT.Text = 0
        LBLTOTALCONES.Text = 0
        LBLTOTALGROSSWT.Text = 0.0
        LBLTOTALTAREWT.Text = 0.0
        LBLTOTALWT.Text = 0.0
        
        EP.Clear()
        'lbllocked.Visible = False
        'PBlock.Visible = False

        txtremarks.Clear()

        TXTSRNO.Text = 1
        CMBQUALITY.Text = ""
        CMBMILL.Text = ""
        TXTLOTNO.Clear()
        CMBCOLOR.Text = ""
        TXTBAGS.Clear()
        TXTBAGNO.Clear()
        TXTCONES.Clear()
        TXTGROSSWT.Clear()
        TXTTAREWT.Clear()
        TXTWT.Clear()
        TXTNARR.Clear()

        GRIDRECD.RowCount = 0
        GETMAX_RECD_NO()

        GRIDRECDDOUBLECLICK = False
        GRIDUPLOADDOUBLECLICK = False


        PBSOFTCOPY.Image = Nothing
        txtuploadname.Clear()
        txtuploadremarks.Clear()
        TXTIMGPATH.Clear()
        gridupload.RowCount = 0
        TXTUPLOADSRNO.Text = 1

    End Sub

    Sub GETMAX_RECD_NO()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax("ISNULL(MAX(YRECDDYEING_NO),0)+ 1", "YARNRECEIVEDDYEING", "AND YRECDDYEING_YEARID = " & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTRECDNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub YARNRECEIVEDDYEING_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
        If cmbname.Text.Trim = "" Then fillname(cmbname, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'") ''''TYPE = PROCESSOR
        If CMBOURGODOWN.Text.Trim = "" Then fillGODOWN(CMBOURGODOWN, EDIT, " AND GODOWN_ISOUR = 'True'")
        If cmbtrans.Text.Trim = "" Then fillname(cmbtrans, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'TRANSPORT'")
        If CMBQUALITY.Text.Trim = "" Then fillQUALITY(CMBQUALITY, EDIT)
        If CMBCOLOR.Text.Trim = "" Then FILLCOLOR(CMBCOLOR)
    End Sub

    Private Sub YARNRECEIVEDDYEING_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'MFG'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)


            FILLCMB()
            CLEAR()
            CMBOURGODOWN.Text = GETDEFAULTGODOWN()

            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim dttable As New DataTable


                Dim objclsDO As New ClsYarnRecdFromDyeing
                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(TEMPRECDNO)
                ALPARAVAL.Add(YearId)
                objclsDO.alParaval = ALPARAVAL
                dttable = objclsDO.SELECTYARNRECD()

                If dttable.Rows.Count > 0 Then

                    For Each dr As DataRow In dttable.Rows

                        TXTRECDNO.Text = TEMPRECDNO
                        DTRECDDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                        CMBOURGODOWN.Text = Convert.ToString(dr("GODOWN").ToString)
                        cmbname.Text = Convert.ToString(dr("NAME").ToString)
                        cmbtrans.Text = Convert.ToString(dr("TRANSNAME").ToString)
                        TXTVEHICLENO.Text = Convert.ToString(dr("VEHICLENO").ToString)
                        TXTCHALLANNO.Text = Convert.ToString(dr("CHALLANNO").ToString)

                        txtremarks.Text = Convert.ToString(dr("REMARKS").ToString)

                        GRIDRECD.Rows.Add(Val(dr("SRNO")), dr("QUALITY").ToString, dr("MILLNAME").ToString, dr("LOTNO").ToString, dr("SHADE").ToString, Val(dr("BAGS")), dr("BAGNO"), Val(dr("CONES")), Format(Val(dr("GROSSWT")), "0.00"), Format(Val(dr("TAREWT")), "0.00"), Format(Val(dr("WT")), "0.00"), dr("NARRATION").ToString, Val(dr("OUTWT")))

                        If Val(dr("OUTWT")) > 0 Then
                            lbllocked.Visible = True
                            PBlock.Visible = True
                        End If

                    Next
                    TOTAL()
                Else
                    EDIT = False
                    CLEAR()
                End If

                Dim OBJCMN As New ClsCommon
                dttable = OBJCMN.search(" YRECDDYEING_SRNO AS GRIDSRNO, YRECDDYEING_REMARKS AS REMARKS, YRECDDYEING_NAME AS NAME, YRECDDYEING_PHOTO AS IMGPATH ", "", " YARNRECEIVEDDYEING_UPLOAD", " AND YRECDDYEING_NO = " & TEMPRECDNO & " AND YRECDDYEING_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    For Each DTR As DataRow In dttable.Rows
                        gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                    Next
                End If

            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
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

            alParaval.Add(Format(Convert.ToDateTime(DTRECDDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBOURGODOWN.Text.Trim)
            alParaval.Add(cmbname.Text.Trim)
            alParaval.Add(cmbtrans.Text.Trim)
            alParaval.Add(TXTVEHICLENO.Text.Trim)
            alParaval.Add(TXTCHALLANNO.Text.Trim)
            alParaval.Add(Val(LBLTOTALBAGS.Text.Trim))
            alParaval.Add(Val(LBLBAGNOCOUNT.Text.Trim))
            alParaval.Add(Val(LBLTOTALCONES.Text.Trim))
            alParaval.Add(Val(LBLTOTALGROSSWT.Text.Trim))
            alParaval.Add(Val(LBLTOTALTAREWT.Text.Trim))
            alParaval.Add(Val(LBLTOTALWT.Text.Trim))
            alParaval.Add(txtremarks.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)


            Dim gridsrno As String = ""
            Dim QUALITY As String = ""
            Dim MILLNAME As String = ""
            Dim LOTNO As String = ""
            Dim SHADE As String = ""
            Dim BAGS As String = ""
            Dim BAGNO As String = ""
            Dim CONES As String = ""
            Dim GROSSWT As String = ""
            Dim TAREWT As String = ""
            Dim WT As String = ""
            Dim NARRATION As String = ""
            Dim OUTWT As String = ""


            For Each row As Windows.Forms.DataGridViewRow In GRIDRECD.Rows
                If row.Cells(0).Value <> Nothing Then
                    If gridsrno = "" Then
                        gridsrno = row.Cells(gsrno.Index).Value.ToString
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                        MILLNAME = row.Cells(GMILLNAME.Index).Value.ToString
                        LOTNO = row.Cells(GLOTNO.Index).Value.ToString
                        SHADE = row.Cells(GSHADE.Index).Value.ToString
                        BAGS = Val(row.Cells(GBAGS.Index).Value)
                        BAGNO = row.Cells(GBAGNO.Index).Value
                        CONES = Val(row.Cells(GCONES.Index).Value)
                        GROSSWT = row.Cells(GGROSSWT.Index).Value
                        TAREWT = row.Cells(GTAREWT.Index).Value
                        WT = Val(row.Cells(Gwt.Index).Value)
                        If row.Cells(GNARRATION.Index).Value <> Nothing Then NARRATION = row.Cells(GNARRATION.Index).Value.ToString Else NARRATION = ""
                        OUTWT = Val(row.Cells(GOUTWT.Index).Value)
                    Else
                        gridsrno = gridsrno & "|" & row.Cells(gsrno.Index).Value
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        MILLNAME = MILLNAME & "|" & row.Cells(GMILLNAME.Index).Value.ToString
                        LOTNO = LOTNO & "|" & row.Cells(GLOTNO.Index).Value.ToString
                        SHADE = SHADE & "|" & row.Cells(GSHADE.Index).Value.ToString
                        BAGS = BAGS & "|" & Val(row.Cells(GBAGS.Index).Value)
                        BAGNO = BAGNO & "|" & row.Cells(GBAGNO.Index).Value
                        CONES = CONES & "|" & Val(row.Cells(GCONES.Index).Value)
                        GROSSWT = GROSSWT & "|" & row.Cells(GGROSSWT.Index).Value
                        TAREWT = TAREWT & "|" & row.Cells(GTAREWT.Index).Value
                        WT = WT & "|" & Val(row.Cells(Gwt.Index).Value)
                        If row.Cells(GNARRATION.Index).Value <> Nothing Then NARRATION = NARRATION & "|" & row.Cells(GNARRATION.Index).Value.ToString Else NARRATION = NARRATION & "|" & ""
                        OUTWT = OUTWT & "|" & Val(row.Cells(GOUTWT.Index).Value)
                    End If
                End If
            Next

            alParaval.Add(gridsrno)
            alParaval.Add(QUALITY)
            alParaval.Add(MILLNAME)
            alParaval.Add(LOTNO)
            alParaval.Add(SHADE)
            alParaval.Add(BAGS)
            alParaval.Add(BAGNO)
            alParaval.Add(CONES)
            alParaval.Add(GROSSWT)
            alParaval.Add(TAREWT)
            alParaval.Add(WT)
            alParaval.Add(NARRATION)
            alParaval.Add(OUTWT)

            Dim OBJDYEING As New ClsYarnRecdFromDyeing
            OBJDYEING.alParaval = alParaval
            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DTTABLE As DataTable = OBJDYEING.SAVE()
                TEMPRECDNO = DTTABLE.Rows(0).Item(0)
                MessageBox.Show("Details Added")

            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPRECDNO)
                IntResult = OBJDYEING.UPDATE()
                MessageBox.Show("Details Updated")
                EDIT = False
            End If

            'PRINTREPORT()
            If gridupload.RowCount > 0 Then SAVEUPLOAD()

            'CLEAR()
            'SHOW NEXT BILL ON EDIT MODE DONT CLEAR
            Call toolnext_Click(sender, e)
            CMBOURGODOWN.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True


        If DTRECDDATE.Text = "__/__/____" Then
            EP.SetError(DTRECDDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(DTRECDDATE.Text) Then
                EP.SetError(DTRECDDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        If cmbname.Text.Trim.Length = 0 Then
            EP.SetError(cmbname, "Please Fill Dyeing Name")
            bln = False
        End If

        If cmbtrans.Text.Trim.Length = 0 Then
            EP.SetError(cmbtrans, " Please Fill Transport Name ")
            bln = False
        End If

        If CMBOURGODOWN.Text.Trim.Length = 0 Then
            EP.SetError(CMBOURGODOWN, " Please Fill Godown ")
            bln = False
        End If

        If GRIDRECD.RowCount = 0 Then
            EP.SetError(cmbtrans, "Select Stock")
            bln = False
        End If

        For Each row As DataGridViewRow In GRIDRECD.Rows
            If Val(row.Cells(Gwt.Index).Value) = 0 Then
                EP.SetError(TabPage1, "Wt Cannot be 0")
                bln = False
            End If
        Next

        'DONE TEMPORARILY
        If lbllocked.Visible = True Then
            EP.SetError(lbllocked, "Unable to Update, Entry Locked")
            bln = False
        End If


        Return bln
    End Function

    Sub fillgrid()

        GRIDRECD.Enabled = True

        If GRIDRECDDOUBLECLICK = False Then
            GRIDRECD.Rows.Add(Val(TXTSRNO.Text.Trim), CMBQUALITY.Text.Trim, CMBMILL.Text.Trim, TXTLOTNO.Text.Trim, CMBCOLOR.Text.Trim, Val(TXTBAGS.Text.Trim), TXTBAGNO.Text.Trim, Val(TXTCONES.Text.Trim), Format(Val(TXTGROSSWT.Text.Trim), "0.00"), Format(Val(TXTTAREWT.Text.Trim), "0.00"), Format(Val(TXTWT.Text.Trim), "0.00"), TXTNARR.Text.Trim, 0)
            getsrno(GRIDRECD)
        ElseIf GRIDRECDDOUBLECLICK = True Then
            GRIDRECD.Item(gsrno.Index, TEMPROW).Value = Val(TXTSRNO.Text.Trim)
            GRIDRECD.Item(GQUALITY.Index, TEMPROW).Value = CMBQUALITY.Text.Trim
            GRIDRECD.Item(GMILLNAME.Index, TEMPROW).Value = CMBMILL.Text.Trim
            GRIDRECD.Item(GLOTNO.Index, TEMPROW).Value = TXTLOTNO.Text.Trim
            GRIDRECD.Item(GSHADE.Index, TEMPROW).Value = CMBCOLOR.Text.Trim
            GRIDRECD.Item(GBAGS.Index, TEMPROW).Value = Val(TXTBAGS.Text.Trim)
            GRIDRECD.Item(GBAGNO.Index, TEMPROW).Value = TXTBAGNO.Text.Trim
            GRIDRECD.Item(GCONES.Index, TEMPROW).Value = Val(TXTCONES.Text.Trim)
            GRIDRECD.Item(GGROSSWT.Index, TEMPROW).Value = Format(Val(TXTGROSSWT.Text.Trim), "0.00")
            GRIDRECD.Item(GTAREWT.Index, TEMPROW).Value = Format(Val(TXTTAREWT.Text.Trim), "0.00")
            GRIDRECD.Item(Gwt.Index, TEMPROW).Value = Format(Val(TXTWT.Text.Trim), "0.00")
            GRIDRECD.Item(GNARRATION.Index, TEMPROW).Value = TXTNARR.Text.Trim
            GRIDRECDDOUBLECLICK = False
        End If

        total()

        GRIDRECD.FirstDisplayedScrollingRowIndex = GRIDRECD.RowCount - 1

        TXTSRNO.Text = Val(GRIDRECD.RowCount) + 1

        'BY DEFAULT GET NEXT BAGNO
        If Val(TXTBAGNO.Text.Trim) > 0 Then TXTBAGNO.Text = Val(TXTBAGNO.Text.Trim) + 1
        TXTBAGS.Clear()
        TXTCONES.Clear()
        TXTGROSSWT.Clear()
        TXTTAREWT.Clear()
        TXTWT.Clear()
        TXTNARR.Clear()
        CMBQUALITY.Focus()

    End Sub

    Private Sub gridRECD_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDRECD.CellDoubleClick
        EDITROW()
    End Sub

    Sub EDITROW()
        Try
            If GRIDRECD.CurrentRow.Index >= 0 And GRIDRECD.Item(gsrno.Index, GRIDRECD.CurrentRow.Index).Value <> Nothing Then

                GRIDRECDDOUBLECLICK = True
                TXTSRNO.Text = GRIDRECD.Item(gsrno.Index, GRIDRECD.CurrentRow.Index).Value.ToString
                CMBQUALITY.Text = GRIDRECD.Item(GQUALITY.Index, GRIDRECD.CurrentRow.Index).Value.ToString
                CMBMILL.Text = GRIDRECD.Item(GMILLNAME.Index, GRIDRECD.CurrentRow.Index).Value.ToString
                TXTLOTNO.Text = GRIDRECD.Item(GLOTNO.Index, GRIDRECD.CurrentRow.Index).Value.ToString
                CMBCOLOR.Text = GRIDRECD.Item(GSHADE.Index, GRIDRECD.CurrentRow.Index).Value.ToString
                TXTBAGS.Text = Val(GRIDRECD.Item(GBAGS.Index, GRIDRECD.CurrentRow.Index).Value)
                TXTBAGNO.Text = GRIDRECD.Item(GBAGNO.Index, GRIDRECD.CurrentRow.Index).Value.ToString
                TXTCONES.Text = Val(GRIDRECD.Item(GCONES.Index, GRIDRECD.CurrentRow.Index).Value)
                TXTGROSSWT.Text = Val(GRIDRECD.Item(GGROSSWT.Index, GRIDRECD.CurrentRow.Index).Value)
                TXTTAREWT.Text = Val(GRIDRECD.Item(GTAREWT.Index, GRIDRECD.CurrentRow.Index).Value)
                TXTWT.Text = Val(GRIDRECD.Item(Gwt.Index, GRIDRECD.CurrentRow.Index).Value)
                TXTNARR.Text = GRIDRECD.Item(GNARRATION.Index, GRIDRECD.CurrentRow.Index).Value.ToString

                TEMPROW = GRIDRECD.CurrentRow.Index
                TXTSRNO.Focus()
            End If
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

    Sub SAVEUPLOAD()
        Try

            Dim OBJDYEING As New ClsYarnRecdFromDyeing
            For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                Dim MS As New IO.MemoryStream
                Dim ALPARAVAL As New ArrayList
                If row.Cells(GUSRNO.Index).Value <> Nothing Then
                    ALPARAVAL.Add(TEMPRECDNO)
                    ALPARAVAL.Add(row.Cells(GUSRNO.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUREMARKS.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUNAME.Index).Value)

                    PBSOFTCOPY.Image = row.Cells(GUIMGPATH.Index).Value
                    PBSOFTCOPY.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                    ALPARAVAL.Add(MS.ToArray)
                    ALPARAVAL.Add(YearId)

                    OBJDYEING.alParaval = ALPARAVAL
                    Dim INTRES As Integer = OBJDYEING.SAVEUPLOAD()
                End If
            Next


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBOURGODOWN_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBOURGODOWN.Enter
        Try
            If CMBOURGODOWN.Text.Trim = "" Then fillGODOWN(CMBOURGODOWN, EDIT, " AND GODOWN_ISOUR = 'True'")
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
            If CMBOURGODOWN.Text.Trim <> "" Then GODOWNVALIDATE(CMBOURGODOWN, e, Me, " AND GODOWN_ISOUR = 'True'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbname.Enter
        Try
            If cmbname.Text.Trim = "" Then fillname(cmbname, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'") ''''TYPE = PROCESSOR
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbname.Validating
        Try
            If cmbname.Text.Trim <> "" Then namevalidate(cmbname, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "PROCESSOR")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbname.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then cmbname.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbtrans.Enter
        Try
            If cmbtrans.Text.Trim = "" Then fillname(cmbtrans, EDIT, "AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'AND LEDGERS.ACC_TYPE= 'TRANSPORT'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbtrans.Validating
        Try
            If cmbtrans.Text.Trim <> "" Then namevalidate(cmbtrans, cmbcode, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'", "Sundry Creditors", "TRANSPORT")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbtrans.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then cmbtrans.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
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

    Sub TOTAL()
        Try
            LBLTOTALBAGS.Text = 0
            LBLTOTALCONES.Text = 0
            LBLTOTALGROSSWT.Text = 0.0
            LBLTOTALTAREWT.Text = 0.0
            LBLTOTALWT.Text = 0.0
            For Each ROW As DataGridViewRow In GRIDRECD.Rows
                If ROW.Cells(gsrno.Index).Value <> Nothing Then
                    LBLTOTALBAGS.Text = Format(Val(LBLTOTALBAGS.Text) + Val(ROW.Cells(GBAGS.Index).EditedFormattedValue), "0")
                    LBLTOTALCONES.Text = Format(Val(LBLTOTALCONES.Text) + Val(ROW.Cells(GCONES.Index).EditedFormattedValue), "0")
                    LBLTOTALGROSSWT.Text = Format(Val(LBLTOTALGROSSWT.Text) + Val(ROW.Cells(GGROSSWT.Index).EditedFormattedValue), "0.00")
                    LBLTOTALTAREWT.Text = Format(Val(LBLTOTALTAREWT.Text) + Val(ROW.Cells(GTAREWT.Index).EditedFormattedValue), "0.00")
                    LBLTOTALWT.Text = Format(Val(LBLTOTALWT.Text) + Val(ROW.Cells(Gwt.Index).EditedFormattedValue), "0.000")
                End If
            Next
            BAGNOCOUNT()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub BAGNOCOUNT()
        Try
            LBLBAGNOCOUNT.Text = 0
            Dim dic As New Dictionary(Of String, Integer)()
            Dim cellValue As String
            For i = 0 To GRIDRECD.Rows.Count - 1
                If Not GRIDRECD.Rows(i).IsNewRow Then
                    cellValue = GRIDRECD(GBAGNO.Index, i).EditedFormattedValue.ToString()
                    If cellValue <> "" Then
                        If Not dic.ContainsKey(cellValue) Then
                            dic.Add(cellValue, 1)
                        Else
                            dic(cellValue) += 1
                        End If
                    End If
                End If
            Next
            LBLBAGNOCOUNT.Text = Val(dic.Count)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            GRIDRECD.RowCount = 0
LINE1:
            TEMPRECDNO = Val(TXTRECDNO.Text) - 1
Line2:
            If TEMPRECDNO > 0 Then

                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("YRECDDYEING_NO ", "", "  YARNRECEIVEDDYEING ", " AND YRECDDYEING_NO = " & Val(TEMPRECDNO) & " AND YARNRECEIVEDDYEING.YRECDDYEING_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    EDIT = True
                    YARNRECEIVEDDYEING_Load(sender, e)
                Else
                    TEMPRECDNO = Val(TEMPRECDNO - 1)
                    GoTo Line2
                End If
            Else
                CLEAR()
                EDIT = False
            End If

            If GRIDRECD.RowCount = 0 And TEMPRECDNO > 1 Then
                TXTRECDNO.Text = TEMPRECDNO
                GoTo LINE1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            GRIDRECD.RowCount = 0
LINE1:
            TEMPRECDNO = Val(TXTRECDNO.Text) + 1
            GETMAX_RECD_NO()
            Dim MAXNO As Integer = TXTRECDNO.Text.Trim
            CLEAR()
            If Val(TXTRECDNO.Text) - 1 >= TEMPRECDNO Then
                EDIT = True
                YARNRECEIVEDDYEING_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDRECD.RowCount = 0 And TEMPRECDNO < MAXNO Then
                TXTRECDNO.Text = TEMPRECDNO
                GoTo LINE1
            End If
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
                GRIDRECD.RowCount = 0
                TEMPRECDNO = Val(tstxtbillno.Text)
                If TEMPRECDNO > 0 Then
                    EDIT = True
                    YARNRECEIVEDDYEING_Load(sender, e)
                Else
                    CLEAR()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridupload_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridupload.CellDoubleClick
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

    Private Sub gridupload_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gridupload.KeyDown
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

    Private Sub gridupload_RowEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridupload.RowEnter
        Try
            If e.RowIndex >= 0 Then PBSOFTCOPY.Image = gridupload.Rows(e.RowIndex).Cells(GUIMGPATH.Index).Value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDRECD_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDRECD.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDRECD.RowCount > 0 Then
                'dont allow user if any of the grid line is in edit mode.....
                'cmbitemname.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDRECDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block
                GRIDRECD.Rows.RemoveAt(GRIDRECD.CurrentRow.Index)
                getsrno(GRIDRECD)
                TOTAL()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim OBJWARPER As New YarnReceivedDyeingDetails
            OBJWARPER.MdiParent = MDIMain
            OBJWARPER.Show()
        Catch EX As Exception
            Throw EX
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
                    MsgBox("Unable to Delete, Item Used", MsgBoxStyle.Critical)
                    Exit Sub
                End If
                If MsgBox("Delete Yarn Isuue?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(TEMPRECDNO)
                    alParaval.Add(YearId)

                    Dim ClsDO As New ClsYarnRecdFromDyeing
                    ClsDO.alParaval = alParaval
                    IntResult = ClsDO.DELETE()

                    MsgBox("Yarn Issue Deleted")
                    CLEAR()
                    EDIT = False
                End If
            Else
                MsgBox("Delete is only in Edit Mode")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DTISSUEDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTRECDDATE.GotFocus
        DTRECDDATE.SelectAll()
    End Sub

    Private Sub DTISSUEDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DTRECDDATE.Validating
        Try
            If DTRECDDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(DTRECDDATE.Text, TEMP) Then
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
            If CMBMILL.Text.Trim = "" Then
                fillname(CMBMILL, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'MILL'")
            End If
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
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'MILL'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBMILL.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILL_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBMILL.Validating
        Try
            If CMBMILL.Text.Trim <> "" Then namevalidate(CMBMILL, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' and LEDGERS.ACC_SUBTYPE = 'MILL'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "MILL")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbQUALITY_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBQUALITY.Enter
        Try
            If CMBQUALITY.Text.Trim = "" Then fillQUALITY(CMBQUALITY, EDIT)
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

    Private Sub cmbQUALITY_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBQUALITY.Validating
        Try
            If CMBQUALITY.Text.Trim <> "" Then QUALITYVALIDATE(CMBQUALITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSHADE_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBCOLOR.Enter
        Try
            If CMBCOLOR.Text.Trim = "" Then FILLCOLOR(CMBCOLOR)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCOLOR_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBCOLOR.Validating
        Try
            If CMBCOLOR.Text.Trim <> "" Then COLORVALIDATE(CMBCOLOR, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTWT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTWT.KeyPress, TXTGROSSWT.KeyPress, TXTTAREWT.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub TXTNARR_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTNARR.Validated
        Try
            If CMBQUALITY.Text.Trim <> "" And Val(TXTWT.Text.Trim) > 0 And TXTLOTNO.Text.Trim <> "" Then
                fillgrid()
            ElseIf CMBQUALITY.Text.Trim = "" Then
                MsgBox("Enter Quality", MsgBoxStyle.Critical)
                CMBQUALITY.Focus()
                Exit Sub

            ElseIf TXTLOTNO.Text.Trim = "" Then
                MsgBox("Enter Lot No", MsgBoxStyle.Critical)
                TXTLOTNO.Focus()
                Exit Sub

            ElseIf Val(TXTWT.Text.Trim) <= 0 Then
                MsgBox("Enter Wt", MsgBoxStyle.Critical)
                TXTWT.Focus()
                Exit Sub
            
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCONES_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTCONES.KeyPress, TXTBAGS.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTCONES_Validated(sender As Object, e As EventArgs) Handles TXTCONES.Validated, TXTGROSSWT.Validated
        CALC()
    End Sub

    Sub CALC()
        Try
            TXTTAREWT.Text = 0.0
            TXTWT.Text = 0.0
            TXTTAREWT.Text = Format(((0.05 * Val(TXTCONES.Text.Trim)) + 0.18), "0.00")
            TXTWT.Text = Format(Val(TXTGROSSWT.Text.Trim) - Val(TXTTAREWT.Text.Trim), "0.00")
            TOTAL()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class