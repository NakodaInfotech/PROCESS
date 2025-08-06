Imports BL
Imports System.Windows.Forms
Imports System.IO

Public Class GreyRecdFromProcessing

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK, GRIDUPLOADDOUBLECLICK As Boolean
    Dim TEMPROW, TEMPUPLOADROW, PURREGID As Integer
    Public EDIT As Boolean
    Public TEMPGREYRECNO As Integer
    Dim TEMPMSG As Integer

    Private Sub CMDEXIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub CMDCLEAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDCLEAR.Click
        CLEAR()
        EDIT = False
        CMBNAME.Focus()
    End Sub

    Sub CLEAR()

        CMBLOTNO.DataSource = Nothing
        TXTGREYRECDNO.Clear()
        DTGREYRECDDATE.Text = Mydate
        cmbname.Text = ""
        CMBNAME.Text = ""
        CMBGODOWN.Text = GETDEFAULTGODOWN()
        TXTCHALLANNO.Clear()
        DTCHALLANDATE.Text = Mydate
        TXTREMARKS.Clear()

        TXTSRNO.Clear()
        CMBQUALITY.Text = ""
        CMBLOTNO.Text = ""
        TXTBALENO.Clear()
        TXTTAKA.Clear()
        TXTMTRS.Clear()
        TXTNARR.Clear()
        TXTBALMTRS.Clear()
        TXTBALTAKA.Clear()
        CMBLOTNO.Text = ""


        EP.Clear()
        lbllocked.Visible = False
        PBlock.Visible = False

        GRIDGREY.RowCount = 0

        GETMAX_GREYREC_NO()
        LBLTOTALMTRS.Text = 0.0
        LBLTOTALTAKA.Text = 0.0


        GRIDDOUBLECLICK = False
        GRIDUPLOADDOUBLECLICK = False


        TabControl1.SelectedIndex = 0

        PBSOFTCOPY.Image = Nothing
        TXTUPLOADSRNO.Clear()
        txtuploadname.Clear()
        txtuploadremarks.Clear()
        TXTIMGPATH.Clear()
        gridupload.RowCount = 0

        If GRIDGREY.RowCount > 0 Then
            TXTSRNO.Text = Val(GRIDGREY.Rows(GRIDGREY.RowCount - 1).Cells(0).Value) + 1
        Else
            TXTSRNO.Text = 1
        End If

        If gridupload.RowCount > 0 Then
            TXTUPLOADSRNO.Text = Val(gridupload.Rows(gridupload.RowCount - 1).Cells(0).Value) + 1
        Else
            TXTUPLOADSRNO.Text = 1
        End If
    End Sub

    Sub TOTAL()
        Try
            LBLTOTALMTRS.Text = 0.0
            LBLTOTALTAKA.Text = 0.0
            For Each ROW As DataGridViewRow In GRIDGREY.Rows
                If ROW.Cells(GSRNO.Index).Value <> Nothing Then
                    LBLTOTALMTRS.Text = Format(Val(LBLTOTALMTRS.Text) + Val(ROW.Cells(GMTRS.Index).EditedFormattedValue), "0.00")
                    LBLTOTALTAKA.Text = Format(Val(LBLTOTALTAKA.Text) + Val(ROW.Cells(GTAKA.Index).EditedFormattedValue), "0.00")
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETMAX_GREYREC_NO()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax("ISNULL(MAX(GRECDPROCESSOR_NO),0)+1", "GREYRECEIVEDPROCESSING", "AND GRECDPROCESSOR_YEARID=" & YearId)
        If DTTABLE.Rows.Count > 0 Then
            TXTGREYRECDNO.Text = DTTABLE.Rows(0).Item(0)
        End If
    End Sub

    Private Sub GreyRecdFromProcessing_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'")
        If CMBGODOWN.Text.Trim = "" Then fillGODOWN(CMBGODOWN, EDIT, " AND GODOWN_ISOUR='TRUE'")
        If CMBQUALITY.Text = "" Then FILLGREY(CMBQUALITY, EDIT)

        If CMBLOTNO.Text.Trim = "" Then
            Dim OBJCMN As New ClsCommonMaster
            Dim dt As DataTable
            dt = OBJCMN.search("ISNULL(GREYISSUETOPROCESS_DESC.GREYISSUE_LOTNO, '') AS LOTNO", "", " GREYISSUETOPROCESS INNER JOIN GREYISSUETOPROCESS_DESC ON GREYISSUETOPROCESS.GREYISSUE_NO = GREYISSUETOPROCESS_DESC.GREYISSUE_NO AND GREYISSUETOPROCESS.GREYISSUE_YEARID = GREYISSUETOPROCESS_DESC.GREYISSUE_YEARID INNER JOIN LEDGERS ON GREYISSUETOPROCESS.GREYISSUE_LEDGERID = LEDGERS.Acc_id", " AND LEDGERS.ACC_CMPNAME= '" & CMBNAME.Text.Trim & "' AND GREYISSUETOPROCESS_DESC.GREYISSUE_YEARID = " & YearId)
            If dt.Rows.Count > 0 Then
                dt.DefaultView.Sort = "GREYISSUE_LOTNO"
                CMBLOTNO.DataSource = dt
                CMBLOTNO.DisplayMember = "GREYISSUE_LOTNO"
                If EDIT = False Then CMBLOTNO.Text = ""
            End If
            CMBLOTNO.SelectAll()
        End If

    End Sub

    Private Sub GreyRecdFromProcessing_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
                Dim OBJGREYREC As New ClsGreyReceivedFromProcessing

                OBJGREYREC.alParaval.Add(TEMPGREYRECNO)
                OBJGREYREC.alParaval.Add(YearId)
                dttable = OBJGREYREC.SELECTGREY()

                If dttable.Rows.Count > 0 Then
                    CMBNAME.Focus()

                    TXTGREYRECDNO.Text = TEMPGREYRECNO
                    DTGREYRECDDATE.Text = dttable.Rows(0).Item("RECDATE")
                    CMBNAME.Text = dttable.Rows(0).Item("NAME").ToString
                    CMBGODOWN.Text = dttable.Rows(0).Item("GODOWN").ToString
                    TXTCHALLANNO.Text = dttable.Rows(0).Item("CHALLANNO").ToString
                    DTCHALLANDATE.Text = dttable.Rows(0).Item("CHALLANDATE")
                    TXTREMARKS.Text = dttable.Rows(0).Item("REMARKS").ToString

                    'ITEM GRID
                    For Each ROW As DataRow In dttable.Rows
                        GRIDGREY.Rows.Add(Val(ROW("GRIDSRNO")), ROW("LOTNO"), ROW("GREYQUALITY"), ROW("BALENO"), 0, 0, Val(ROW("TAKA")), Format(Val(ROW("MTRS")), "0.00"), ROW("NARR"), Val(ROW("FROMNO")), Val(ROW("FROMSRNO")))
                    Next

                    'UPLOAD(GRID)
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.search(" GREYRECEIVEDPROCESSING_UPLOAD.GRECDPROCESSOR_SRNO AS GRIDSRNO, GREYRECEIVEDPROCESSING_UPLOAD.GRECDPROCESSOR_REMARKS AS REMARKS, GREYRECEIVEDPROCESSING_UPLOAD.GRECDPROCESSOR_NAME AS NAME, GREYRECEIVEDPROCESSING_UPLOAD.GRECDPROCESSOR_PHOTO AS IMGPATH ", "", " GREYRECEIVEDPROCESSING_UPLOAD ", " AND GREYRECEIVEDPROCESSING_UPLOAD.GRECDPROCESSOR_NO = " & TEMPGREYRECNO & " AND GRECDPROCESSOR_YEARID = " & YearId & " ORDER BY GREYRECEIVEDPROCESSING_UPLOAD.GRECDPROCESSOR_SRNO")
                    If DT.Rows.Count > 0 Then
                        For Each DTR As DataRow In DT.Rows
                            gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                        Next
                    End If

                    TOTAL()
                    getsrno(GRIDGREY)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        Try
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim IntResult As Integer
            Dim alParaval As New ArrayList

            alParaval.Add(Format(Convert.ToDateTime(DTGREYRECDDATE.Text.Trim).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBNAME.Text.Trim)
            alParaval.Add(CMBGODOWN.Text.Trim)
            alParaval.Add(TXTCHALLANNO.Text.Trim)
            alParaval.Add(DTCHALLANDATE.Text.Trim)
            alParaval.Add(Val(LBLTOTALTAKA.Text.Trim))
            alParaval.Add(Val(LBLTOTALMTRS.Text.Trim))
            alParaval.Add(TXTREMARKS.Text.Trim)


            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)

            Dim SRNO As String = ""
            Dim QUALITY As String = ""
            Dim LOTNO As String = ""
            Dim BALENO As String = ""
            Dim TAKA As String = ""
            Dim MTRS As String = ""
            Dim NARR As String = ""
            Dim FROMNO As String = ""
            Dim FROMSRNO As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDGREY.Rows
                If row.Cells(GSRNO.Index).Value <> Nothing Then
                    If SRNO = "" Then
                        SRNO = Val(row.Cells(GSRNO.Index).Value)
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                        LOTNO = row.Cells(GLOTNO.Index).Value.ToString
                        BALENO = row.Cells(GBALENO.Index).Value.ToString
                        TAKA = Val(row.Cells(GTAKA.Index).Value)
                        MTRS = Format(Val(row.Cells(GMTRS.Index).Value), "0.00")
                        NARR = row.Cells(GNARR.Index).Value.ToString
                        FROMNO = Val(row.Cells(GFROMNO.Index).Value)
                        FROMSRNO = Val(row.Cells(GFROMSRNO.Index).Value)

                    Else

                        SRNO = SRNO & "|" & row.Cells(GSRNO.Index).Value
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        LOTNO = LOTNO & "|" & row.Cells(GLOTNO.Index).Value.ToString
                        BALENO = BALENO & "|" & row.Cells(GBALENO.Index).Value.ToString
                        TAKA = TAKA & "|" & Val(row.Cells(GTAKA.Index).Value)
                        MTRS = MTRS & "|" & Format(Val(row.Cells(GMTRS.Index).Value), "0.00")
                        NARR = NARR & "|" & row.Cells(GNARR.Index).Value
                        FROMNO = FROMNO & "|" & Val(row.Cells(GFROMNO.Index).Value)
                        FROMSRNO = FROMSRNO & "|" & Val(row.Cells(GFROMSRNO.Index).Value)

                    End If
                End If
            Next

            alParaval.Add(SRNO)
            alParaval.Add(QUALITY)
            alParaval.Add(LOTNO)
            alParaval.Add(BALENO)
            alParaval.Add(TAKA)
            alParaval.Add(MTRS)
            alParaval.Add(NARR)
            alParaval.Add(FROMNO)
            alParaval.Add(FROMSRNO)





            Dim OBJGREYREC As New ClsGreyReceivedFromProcessing
            OBJGREYREC.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DT As DataTable = OBJGREYREC.save()
                TEMPGREYRECNO = DT.Rows(0).Item(0)
                MsgBox("Details Added")

            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPGREYRECNO)
                IntResult = OBJGREYREC.Update()
                EDIT = False
                MsgBox("Details Updated")

            End If

            If gridupload.RowCount > 0 Then SAVEUPLOAD()
            'CLEAR()
            'SHOW NEXT BILL ON EDIT MODE DONT CLEAR
            Call toolnext_Click(sender, e)
            DTGREYRECDDATE.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub SAVEUPLOAD()

        Try
            Dim OBJGREYREC As New ClsGreyReceivedFromProcessing


            For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                Dim MS As New IO.MemoryStream
                Dim ALPARAVAL As New ArrayList
                If row.Cells(GUSRNO.Index).Value <> Nothing Then
                    ALPARAVAL.Add(TEMPGREYRECNO)
                    ALPARAVAL.Add(row.Cells(GUSRNO.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUREMARKS.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUNAME.Index).Value)

                    PBSOFTCOPY.Image = row.Cells(GUIMGPATH.Index).Value
                    PBSOFTCOPY.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                    ALPARAVAL.Add(MS.ToArray)
                    ALPARAVAL.Add(YearId)

                    OBJGREYREC.alParaval = ALPARAVAL
                    Dim INTRES As Integer = OBJGREYREC.SAVEUPLOAD()
                End If
            Next


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function CloneWithValues(ByVal row As DataGridViewRow) As DataGridViewRow
        CloneWithValues = CType(row.Clone(), DataGridViewRow)
        For index As Int32 = 0 To row.Cells.Count - 1
            CloneWithValues.Cells(index).Value = row.Cells(index).Value
        Next
    End Function

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


        If DTGREYRECDDATE.Text = "__/__/____" Then
            EP.SetError(DTGREYRECDDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(DTGREYRECDDATE.Text) Then
                EP.SetError(DTGREYRECDDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        If cmbname.Text.Trim.Length = 0 Then
            EP.SetError(CMBNAME, "Please Fill Processor Name")
            bln = False
        End If

        If CMBGODOWN.Text.Trim.Length = 0 Then
            EP.SetError(CMBGODOWN, " Please Fill Our Godown Name ")
            bln = False
        End If

        If TXTCHALLANNO.Text.Trim.Length = 0 Then
            EP.SetError(TXTCHALLANNO, " Please Enter Challan No.")
            bln = False
        End If

        If lbllocked.Visible = True Then
            EP.SetError(lbllocked, "Unable to Modify, entry Locked")
            bln = False
        End If

        If DTGREYRECDDATE.Text.Trim <> "__/__/____" And DTCHALLANDATE.Text.Trim <> "__/__/____" Then
            If Convert.ToDateTime(DTGREYRECDDATE.Text).Date > Convert.ToDateTime(DTCHALLANDATE.Text).Date Then
                EP.SetError(DTCHALLANDATE, " Please Enter Proper Challan Date")
                bln = False
            End If
        End If

        If GRIDGREY.RowCount = 0 Then
            EP.SetError(TXTNARR, "Enter Proper Details")
            bln = False
        End If

        For Each row As DataGridViewRow In GRIDGREY.Rows

            If Val(row.Cells(GTAKA.Index).Value) = 0 Then
                EP.SetError(TXTNARR, "Taka Cannot be 0")
                bln = False
            End If

            If Val(row.Cells(GMTRS.Index).Value) = 0 Then
                EP.SetError(TXTNARR, "Mtrs Cannot be 0")
                bln = False
            End If

            'Dim OBJCMN As New ClsCommon
            'Dim DT As New DataTable
            'DT = OBJCMN.search("(GREYISSUE_TAKA - GREYISSUE_OUTTAKA) AS ALLOWEDTAKA, (GREYISSUE_MTRS - GREYISSUE_OUTMTRS) AS ALLOWEDMTRS ", "", " GREYISSUETOPROCESS_DESC ", " AND GREYISSUE_NO = " & row.Cells(GFROMNO.Index).Value & " AND GREYISSUE_GRIDSRNO = " & row.Cells(GFROMSRNO.Index).Value & " AND GREYISSUE_YEARID = " & YearId)
            'If EDIT = False Then
            '    If Val(row.Cells(GTAKA.Index).Value) > Val(DT.Rows(0).Item("ALLOWEDTAKA")) Then
            '        EP.SetError(CMDNARR, "Taka Greater then Allowed Taka, Maximum " & Val(DT.Rows(0).Item("ALLOWEDTAKA")) & " Taka Allowed")
            '        bln = False
            '    ElseIf Val(row.Cells(GMTRS.Index).Value) > Val(DT.Rows(0).Item("ALLOWEDMTRS")) Then
            '        EP.SetError(CMDNARR, "Meters Greater then Allowed Meters, Maximum " & Val(DT.Rows(0).Item("ALLOWEDMTRS")) & " Meters Allowed")
            '        bln = False
            '    End If
            'Else
            '    Dim DT1 As DataTable = OBJCMN.search("GRECDPROCESSOR_TAKA AS OLDTAKA, GRECDPROCESSOR_MTRS AS OLDMTRS ", "", " GREYRECEIVEDPROCESSING_DESC ", " AND GRECDPROCESSOR_NO = " & TEMPGREYRECNO & " AND GRECDPROCESSOR_FROMNO = " & row.Cells(GFROMNO.Index).Value & " AND GRECDPROCESSOR_FROMSRNO = " & row.Cells(GFROMSRNO.Index).Value & " AND GRECDPROCESSOR_YEARID = " & YearId)
            '    If Val(row.Cells(GTAKA.Index).Value) > (Val(DT.Rows(0).Item("ALLOWEDTAKA")) + Val(DT1.Rows(0).Item("OLDTAKA"))) Then
            '        EP.SetError(CMDNARR, "Taka Greater then Allowed Taka, Maximum " & (Val(DT.Rows(0).Item("ALLOWEDTAKA")) + Val(DT1.Rows(0).Item("OLDTAKA"))) & " Taka Allowed")
            '        bln = False
            '    ElseIf Val(row.Cells(GMTRS.Index).Value) > (Val(DT.Rows(0).Item("ALLOWEDMTRS")) + Val(DT1.Rows(0).Item("OLDMTRS"))) Then
            '        EP.SetError(CMDNARR, "Meters Greater then Allowed Meters, Maximum " & (Val(DT.Rows(0).Item("ALLOWEDMTRS")) + Val(DT1.Rows(0).Item("OLDMTRS"))) & " Meters Allowed")
            '        bln = False
            '    End If
            'End If


        Next

        Return bln
    End Function

    Private Sub DTGREYRECDDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTGREYRECDDATE.GotFocus
        DTGREYRECDDATE.Select(0, 0)
    End Sub

    Sub getsrno(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            For Each row As DataGridViewRow In grid.Rows
                row.Cells(0).Value = row.Index + 1
            Next

            If GRIDGREY.RowCount > 0 Then
                TXTSRNO.Text = Val(GRIDGREY.Rows(GRIDGREY.RowCount - 1).Cells(0).Value) + 1
            Else
                TXTSRNO.Text = 1
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBNAME.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "PROCESSOR")
            If CMBNAME.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search("ISNULL(GREYISSUETOPROCESS_DESC.GREYISSUE_LOTNO, '') AS LOTNO", "", " GREYISSUETOPROCESS INNER JOIN GREYISSUETOPROCESS_DESC ON GREYISSUETOPROCESS.GREYISSUE_NO = GREYISSUETOPROCESS_DESC.GREYISSUE_NO AND GREYISSUETOPROCESS.GREYISSUE_YEARID = GREYISSUETOPROCESS_DESC.GREYISSUE_YEARID INNER JOIN LEDGERS ON GREYISSUETOPROCESS.GREYISSUE_LEDGERID = LEDGERS.Acc_id", " AND LEDGERS.ACC_CMPNAME= '" & CMBNAME.Text.Trim & "' AND GREYISSUETOPROCESS_DESC.GREYISSUE_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "LOTNO"
                    CMBLOTNO.DataSource = dt
                    CMBLOTNO.DisplayMember = "LOTNO"
                    If EDIT = False Then CMBLOTNO.Text = ""
                End If
                CMBLOTNO.SelectAll()
            End If
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

    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            GRIDGREY.RowCount = 0
LINE1:
            TEMPGREYRECNO = Val(TXTGREYRECDNO.Text) - 1
Line2:
            If TEMPGREYRECNO > 0 Then

                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" GRECDPROCESSOR_NO ", "", "  GREYRECEIVEDPROCESSING", " AND GRECDPROCESSOR_NO = '" & TEMPGREYRECNO & "' AND GREYRECEIVEDPROCESSING.GRECDPROCESSOR_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    EDIT = True
                    GreyRecdFromProcessing_Load(sender, e)
                Else
                    TEMPGREYRECNO = Val(TEMPGREYRECNO - 1)
                    GoTo Line2
                End If
            Else
                CLEAR()
                EDIT = False
            End If

            If GRIDGREY.RowCount = 0 And TEMPGREYRECNO > 1 Then
                TXTGREYRECDNO.Text = TEMPGREYRECNO
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
            TEMPGREYRECNO = Val(TXTGREYRECDNO.Text) + 1
            GETMAX_GREYREC_NO()
            Dim MAXNO As Integer = TXTGREYRECDNO.Text.Trim
            CLEAR()
            If Val(TXTGREYRECDNO.Text) - 1 >= TEMPGREYRECNO Then
                EDIT = True
                GreyRecdFromProcessing_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDGREY.RowCount = 0 And TEMPGREYRECNO < MAXNO Then
                TXTGREYRECDNO.Text = TEMPGREYRECNO
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
                GRIDGREY.RowCount = 0
                TEMPGREYRECNO = Val(tstxtbillno.Text)
                If TEMPGREYRECNO > 0 Then
                    EDIT = True
                    GreyRecdFromProcessing_Load(sender, e)
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

    Private Sub GRIDGREY_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDGREY.CellDoubleClick
        Try
            If e.RowIndex = -1 Then Exit Sub

            If e.RowIndex >= 0 And GRIDGREY.Item(GSRNO.Index, e.RowIndex).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                TXTSRNO.Text = GRIDGREY.Item(GSRNO.Index, e.RowIndex).Value
                CMBQUALITY.Text = GRIDGREY.Item(GQUALITY.Index, e.RowIndex).Value
                CMBLOTNO.Text = GRIDGREY.Item(GLOTNO.Index, e.RowIndex).Value
                TXTBALENO.Text = GRIDGREY.Item(GBALENO.Index, e.RowIndex).Value
                TXTBALTAKA.Text = Val(GRIDGREY.Item(GBALTAKA.Index, e.RowIndex).Value)
                TXTBALMTRS.Text = Val(GRIDGREY.Item(GBALMTRS.Index, e.RowIndex).Value)
                TXTTAKA.Text = Val(GRIDGREY.Item(GTAKA.Index, e.RowIndex).Value)
                TXTMTRS.Text = Val(GRIDGREY.Item(GMTRS.Index, e.RowIndex).Value)
                TXTNARR.Text = GRIDGREY.Item(GNARR.Index, e.RowIndex).Value

                TEMPROW = e.RowIndex
                CMBLOTNO.Focus()

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

            ElseIf e.KeyCode = Keys.F12 And GRIDGREY.RowCount > 0 Then
                Dim TEMPBALTAKA As New Integer
                Dim TEMPBALMTRS As New Double

                TEMPBALTAKA = (GRIDGREY.Rows(GRIDGREY.RowCount - 1).Cells(GBALTAKA.Index).Value) - (GRIDGREY.Rows(GRIDGREY.RowCount - 1).Cells(GTAKA.Index).Value)
                TEMPBALMTRS = (GRIDGREY.Rows(GRIDGREY.RowCount - 1).Cells(GBALMTRS.Index).Value) - (GRIDGREY.Rows(GRIDGREY.RowCount - 1).Cells(GMTRS.Index).Value)

                If TEMPBALTAKA > (GRIDGREY.CurrentRow.Cells(GTAKA.Index).Value) And TEMPBALMTRS > (GRIDGREY.CurrentRow.Cells(GMTRS.Index).Value) Then
                    GRIDGREY.Rows.Add(CloneWithValues(GRIDGREY.CurrentRow))
                Else

                    '(GRIDGREY.Rows(GRIDGREY.CurrentRow )+1).Cells(GBALTAKA.Index).Value = TEMPBALTAKA
                    'GRIDGREY.Rows(GRIDGREY.RowCount + 1).Cells(GBALMTRS.Index - 1).Value = (GRIDGREY.Rows(GRIDGREY.RowCount - 1).Cells(GBALMTRS.Index).Value) - (GRIDGREY.Rows(GRIDGREY.RowCount - 1).Cells(GMTRS.Index).Value)
                    If TEMPBALTAKA = 0 And TEMPBALMTRS = 0 Then
                        MsgBox("No More Stock for Selected Lot No.")
                        Exit Sub
                    Else
                        GRIDGREY.Rows.Add(GRIDGREY.Rows(GRIDGREY.RowCount - 1).Cells(GSRNO.Index).Value, GRIDGREY.Rows(GRIDGREY.RowCount - 1).Cells(GLOTNO.Index).Value, GRIDGREY.Rows(GRIDGREY.RowCount - 1).Cells(GQUALITY.Index).Value, GRIDGREY.Rows(GRIDGREY.RowCount - 1).Cells(GBALENO.Index).Value, ((GRIDGREY.Rows(GRIDGREY.RowCount - 2).Cells(GBALTAKA.Index).Value) - (GRIDGREY.Rows(GRIDGREY.RowCount - 2).Cells(GTAKA.Index).Value)), ((GRIDGREY.Rows(GRIDGREY.RowCount - 2).Cells(GBALMTRS.Index).Value) - (GRIDGREY.Rows(GRIDGREY.RowCount - 2).Cells(GMTRS.Index).Value)), TEMPBALTAKA, TEMPBALMTRS, "")
                        'GRIDGREY.Rows.Insert(GRIDGREY.RowCount, "", "", "", "", "", "", TEMPBALTAKA, TEMPBALMTRS, "")
                        'GRIDGREY.Rows(GRIDGREY.RowCount - 1).Cells(GTAKA.Index).Value = TEMPBALTAKA
                        'GRIDGREY.Rows(GRIDGREY.RowCount + 1).Cells(GMTRS.Index).Value = TEMPBALMTRS

                        'GRIDGREY.Rows(GRIDGREY.CurrentRow + 1).Cells(GMTRS.Index).Value = TEMPBALMTRS
                        'GRIDGREY.CurrentRow.Cells(GTAKA.Index).Value = TEMPBALMTRS
                    End If


                End If


                'If GRIDGREY.CurrentRow.Cells(GBALENO.Index).Value <> "" And (Val(GRIDGREY.CurrentRow.Cells(GBALTAKA.Index).Value - GRIDGREY.CurrentRow.Cells(GTAKA.Index).Value) > 0 Or Val(GRIDGREY.CurrentRow.Cells(GBALMTRS.Index).Value - GRIDGREY.CurrentRow.Cells(GMTRS.Index).Value) > 0) Then GRIDGREY.Rows.Add(CloneWithValues(GRIDGREY.CurrentRow))
                'If GRIDGREY.CurrentRow.Cells(GBALENO.Index).Value <> Nothing And ((Val(GRIDGREY.CurrentRow.Cells(GBALTAKA.Index).Value) - Val(GRIDGREY.CurrentRow.Cells(GTAKA.Index).Value) > 0) Or (Format(Val(GRIDGREY.CurrentRow.Cells(GBALMTRS.Index).Value) - Val(GRIDGREY.CurrentRow.Cells(GMTRS.Index).Value), "0.00")) > 0) Then GRIDGREY.Rows.Add(CloneWithValues(GRIDGREY.CurrentRow))


                'If GRIDGREY.Rows(GRIDGREY.RowCount - 1).Cells(GBALENO.Index).Value <> Nothing Then
                GRIDGREY.Rows(GRIDGREY.RowCount - 1).Cells(GBALENO.Index).Value = Val(GRIDGREY.Rows(GRIDGREY.RowCount - 2).Cells(GBALENO.Index).Value) + 1
                GRIDGREY.Rows(GRIDGREY.RowCount - 1).Cells(GBALTAKA.Index).Value = (GRIDGREY.Rows(GRIDGREY.RowCount - 2).Cells(GBALTAKA.Index).Value) - (GRIDGREY.Rows(GRIDGREY.RowCount - 2).Cells(GTAKA.Index).Value)
                GRIDGREY.Rows(GRIDGREY.RowCount - 1).Cells(GBALMTRS.Index).Value = (GRIDGREY.Rows(GRIDGREY.RowCount - 2).Cells(GBALMTRS.Index).Value) - (GRIDGREY.Rows(GRIDGREY.RowCount - 2).Cells(GMTRS.Index).Value)
                'End If

                GRIDGREY.FirstDisplayedScrollingRowIndex = GRIDGREY.RowCount - 1
                GRIDGREY.Rows(GRIDGREY.RowCount - 1).Selected = True
                getsrno(GRIDGREY)
                TOTAL()
            End If



        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Call CMDOK_Click(sender, e)
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

                TEMPMSG = MsgBox("Delete Entry?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbYes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(TXTGREYRECDNO.Text.Trim)
                    alParaval.Add(YearId)

                    Dim OBJDEL As New ClsGreyReceivedFromProcessing
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

    Sub FILLGRID()
        Try
            If GRIDDOUBLECLICK = False Then
                GRIDGREY.Rows.Add(Val(TXTSRNO.Text.Trim), CMBLOTNO.Text.Trim, CMBQUALITY.Text.Trim, TXTBALENO.Text.Trim, Format(Val(TXTBALTAKA.Text.Trim), "0"), Format(Val(TXTBALMTRS.Text.Trim), "0.00"), Format(Val(TXTTAKA.Text.Trim), "0"), Format(Val(TXTMTRS.Text.Trim), "0.00"), TXTNARR.Text.Trim, Val(TXTFROMNO.Text.Trim), Val(TXTFROMSRNO.Text.Trim))
            Else
                GRIDGREY.Item(GSRNO.Index, TEMPROW).Value = TXTSRNO.Text.Trim
                GRIDGREY.Item(GQUALITY.Index, TEMPROW).Value = CMBQUALITY.Text.Trim
                GRIDGREY.Item(GLOTNO.Index, TEMPROW).Value = CMBLOTNO.Text.Trim
                GRIDGREY.Item(GBALENO.Index, TEMPROW).Value = TXTBALENO.Text.Trim
                GRIDGREY.Item(GBALTAKA.Index, TEMPROW).Value = Format(Val(TXTBALTAKA.Text.Trim), "0")
                GRIDGREY.Item(GBALMTRS.Index, TEMPROW).Value = Format(Val(TXTBALMTRS.Text.Trim), "0.00")
                GRIDGREY.Item(GTAKA.Index, TEMPROW).Value = Format(Val(TXTTAKA.Text.Trim), "0")
                GRIDGREY.Item(GMTRS.Index, TEMPROW).Value = Format(Val(TXTMTRS.Text.Trim), "0.00")
                GRIDGREY.Item(GNARR.Index, TEMPROW).Value = TXTNARR.Text.Trim
                GRIDGREY.Item(GFROMNO.Index, TEMPROW).Value = TXTFROMNO.Text.Trim
                GRIDGREY.Item(GFROMSRNO.Index, TEMPROW).Value = TXTFROMSRNO.Text.Trim

                GRIDDOUBLECLICK = False
            End If
            CMBQUALITY.Text = ""
            'CMBLOTNO.Text = ""
            TXTBALENO.Clear()
            TXTBALTAKA.Clear()
            TXTBALMTRS.Clear()
            TXTTAKA.Clear()
            TXTMTRS.Clear()
            TXTNARR.Clear()
            TXTFROMNO.Clear()
            TXTFROMSRNO.Clear()
            getsrno(GRIDGREY)
            TOTAL()
            CMBLOTNO.Focus()
            If GRIDGREY.RowCount > 0 Then TXTSRNO.Text = Val(GRIDGREY.RowCount) + 1 Else TXTSRNO.Text = 1
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTNARR_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTNARR.Validating
        ' If CMBQUALITY.Text.Trim <> "" And CMBLOTNO.Text.Trim <> "" And TXTBALENO.Text.Trim <> "" And Val(TXTTAKA.Text.Trim) > 0 And Val(TXTMTRS.Text.Trim) > 0 Then FILLGRID() Else MsgBox("Please Enter proper details")
        If CMBQUALITY.Text.Trim <> "" And CMBLOTNO.Text.Trim <> "" And TXTBALENO.Text.Trim <> "" And (Val(TXTTAKA.Text.Trim) <= Val(TXTBALTAKA.Text.Trim)) And (Val(TXTMTRS.Text.Trim) <= Val(TXTBALMTRS.Text.Trim)) Then FILLGRID() Else MsgBox("Please Enter proper details")

    End Sub

    Private Sub TXTTAKA_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTTAKA.KeyPress
        numkeypress(e, TXTTAKA, Me)
    End Sub

    Private Sub TXTMTRS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTMTRS.KeyPress
        numdotkeypress(e, TXTMTRS, Me)
    End Sub

    Private Sub CMBQUALITY_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBQUALITY.Enter
        Try
            If CMBQUALITY.Text.Trim = "" Then FILLGREY(CMBQUALITY, EDIT)
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
                OBJGREY.ShowDialog()
                If OBJGREY.TEMPNAME <> "" Then CMBQUALITY.Text = OBJGREY.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBQUALITY.Validating
        Try
            If CMBQUALITY.Text.Trim <> "" Then GREYVALIDATE(CMBQUALITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim OBJGREY As New GreyRecdFromProcessingDetails
            OBJGREY.MdiParent = MDIMain
            OBJGREY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub CMBNAME_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBNAME.Validated
        If CMBNAME.Text.Trim <> "" Then
            Dim OBJCMN As New ClsCommonMaster
            Dim dt As DataTable
            dt = OBJCMN.search("ISNULL(GREYISSUETOPROCESS_DESC.GREYISSUE_LOTNO, '') AS LOTNO", "", " GREYISSUETOPROCESS INNER JOIN GREYISSUETOPROCESS_DESC ON GREYISSUETOPROCESS.GREYISSUE_NO = GREYISSUETOPROCESS_DESC.GREYISSUE_NO AND GREYISSUETOPROCESS.GREYISSUE_YEARID = GREYISSUETOPROCESS_DESC.GREYISSUE_YEARID INNER JOIN LEDGERS ON GREYISSUETOPROCESS.GREYISSUE_LEDGERID = LEDGERS.Acc_id", " AND LEDGERS.ACC_CMPNAME= '" & CMBNAME.Text.Trim & "' AND GREYISSUETOPROCESS_DESC.GREYISSUE_YEARID = " & YearId)
            If dt.Rows.Count > 0 Then
                dt.DefaultView.Sort = "LOTNO"
                CMBLOTNO.DataSource = dt
                CMBLOTNO.DisplayMember = "LOTNO"
                If EDIT = False Then CMBLOTNO.Text = ""
            End If
            CMBLOTNO.SelectAll()
        End If
    End Sub

    Private Sub CMBLOTNO_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBLOTNO.Validated
        Try
            If GRIDDOUBLECLICK = False Then
                If CMBLOTNO.Text.Trim <> "" Then
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.search("ISNULL((GREYISSUETOPROCESS_DESC.GREYISSUE_TAKA - GREYISSUETOPROCESS_DESC.GREYISSUE_OUTTAKA), 0) AS BALTAKA, ISNULL((GREYISSUETOPROCESS_DESC.GREYISSUE_MTRS - GREYISSUETOPROCESS_DESC.GREYISSUE_OUTMTRS), 0) AS BALMTRS, ISNULL(GREYISSUETOPROCESS_DESC.GREYISSUE_NO, 0) AS FROMNO, ISNULL(GREYISSUETOPROCESS_DESC.GREYISSUE_GRIDSRNO, 0) AS FROMSRNO", "", "GREYISSUETOPROCESS INNER JOIN GREYISSUETOPROCESS_DESC ON GREYISSUETOPROCESS.GREYISSUE_NO = GREYISSUETOPROCESS_DESC.GREYISSUE_NO AND GREYISSUETOPROCESS.GREYISSUE_YEARID = GREYISSUETOPROCESS_DESC.GREYISSUE_YEARID", "AND GREYISSUETOPROCESS_DESC.GREYISSUE_LOTNO = '" & CMBLOTNO.Text.Trim & "' AND GREYISSUETOPROCESS_DESC.GREYISSUE_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        TXTBALTAKA.Text = DT.Rows(0).Item("BALTAKA")
                        TXTBALMTRS.Text = DT.Rows(0).Item("BALMTRS")
                        TXTFROMNO.Text = DT.Rows(0).Item("FROMNO")
                        TXTFROMSRNO.Text = DT.Rows(0).Item("FROMSRNO")
                    End If
                End If
            End If

            ' If GRIDGREY.RowCount > 0 Then TXTBALTAKA.Text = (GRIDGREY.Rows(GRIDGREY.RowCount - 1).Cells(GBALTAKA.Index).Value) - (GRIDGREY.Rows(GRIDGREY.RowCount - 1).Cells(GTAKA.Index).Value)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTTAKA_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTTAKA.Validating
        'If Val(TXTTAKA.Text.Trim) > Val(TXTBALTAKA.Text.Trim) Or Val(TXTTAKA.Text.Trim) <= 0 Then
        '    MsgBox("Please check Taka Quantity")
        '    e.Cancel = True
        'End If
    End Sub


    Private Sub TXTMTRS_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTMTRS.Validating
        'If Val(TXTMTRS.Text.Trim) > Val(TXTBALMTRS.Text.Trim) Or Val(TXTMTRS.Text.Trim) <= 0 Then
        '    MsgBox("Please check Meter Quantity")
        '    e.Cancel = True
        'End If
    End Sub

    Private Sub DTGREYRECDDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DTGREYRECDDATE.Validating
        Try
            If DTGREYRECDDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(DTGREYRECDDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DTCHALLANDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DTCHALLANDATE.Validating
        Try
            If DTCHALLANDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(DTCHALLANDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DTCHALLANDATE_GotFocus(sender As Object, e As EventArgs) Handles DTCHALLANDATE.GotFocus
        DTCHALLANDATE.Select(0, 0)
    End Sub
End Class