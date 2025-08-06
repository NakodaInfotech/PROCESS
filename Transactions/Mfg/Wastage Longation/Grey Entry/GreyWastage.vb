
Imports BL

Public Class GreyWastage

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK, GRIDUPLOADDOUBLECLICK As Boolean
    Dim TEMPROW, TEMPUPLOADROW As Integer
    Public EDIT As Boolean
    Public TEMPGWASTAGENO As Integer
    Public FRMSTRING As String

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        Try
            CLEAR()
            EDIT = False
            CMBOURGODOWN.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()
        Try
            DTWASTAGE.Text = Mydate
            TXTREMARKS.Clear()

            LBLTOTALMTRS.Text = 0.0

            TXTSRNO.Text = 1
            CMBTYPE.SelectedItem = Nothing
            CMBGREYQUALITY.Text = ""
            TXTTAKANO.Clear()
            TXTPCS.Clear()
            TXTMTRS.Clear()
            TXTNARR.Clear()

            EP.Clear()
            lbllocked.Visible = False
            PBlock.Visible = False

            GRIDWASTAGE.RowCount = 0
            GETMAX_WASTAGE_NO()

            GRIDDOUBLECLICK = False
            GRIDUPLOADDOUBLECLICK = False

            TabControl1.SelectedIndex = 0

            PBSOFTCOPY.Image = Nothing
            TXTUPLOADSRNO.Text = 1
            txtuploadname.Clear()
            txtuploadremarks.Clear()
            TXTIMGPATH.Clear()
            gridupload.RowCount = 0
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETMAX_WASTAGE_NO()
        Try
            Dim DTTABLE As New DataTable
            DTTABLE = getmax(" ISNULL(MAX(GWASTAGE_NO),0)+ 1 ", " GREYWASTAGE ", " AND GWASTAGE_YEARID = " & YearId)
            If DTTABLE.Rows.Count > 0 Then TXTGWASTAGENO.Text = DTTABLE.Rows(0).Item(0)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GreyWastage_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
                TSTXTBILLNO.Focus()
                TSTXTBILLNO.SelectAll()
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
            Throw ex
        Finally
            Cursor.Current = Cursors.WaitCursor
        End Try
    End Sub

    Sub FILLCMB()
        Try
            If CMBOURGODOWN.Text.Trim = "" Then fillGODOWN(CMBOURGODOWN, EDIT, " AND GODOWN_ISOUR = 'True'")
            FILLGREYSTOCKQUALITY()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGREYSTOCKQUALITY()
        Try
            If CMBGREYQUALITY.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search(" EFFECTGREYQUALITY ", "", " GREYSTOCK ", " AND YEARID = " & YearId & " GROUP BY EFFECTGREYQUALITY HAVING SUM(MTRS) > 0 ")
                CMBGREYQUALITY.DataSource = dt
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "EFFECTGREYQUALITY"
                    CMBGREYQUALITY.DisplayMember = "EFFECTGREYQUALITY"
                    If EDIT = False Then CMBGREYQUALITY.Text = ""
                End If
                CMBGREYQUALITY.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GreyWastage_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
                Dim OBJWAS As New ClsGreyWastage
                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(TEMPGWASTAGENO)
                ALPARAVAL.Add(YearId)
                OBJWAS.alParaval = ALPARAVAL
                dttable = OBJWAS.SELECTGREYWASTAGE()

                If dttable.Rows.Count > 0 Then
                    For Each dr As DataRow In dttable.Rows

                        TXTGWASTAGENO.Text = TEMPGWASTAGENO
                        DTWASTAGE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                        CMBOURGODOWN.Text = Convert.ToString(dr("GODOWN").ToString)
                        TXTREMARKS.Text = Convert.ToString(dr("REMARKS").ToString)

                        'Item Grid
                        GRIDWASTAGE.Rows.Add(dr("SRNO"), dr("TYPE"), dr("QUALITY").ToString, dr("TAKANO"), Format(Val(dr("PCS")), "0.00"), Format(Val(dr("MTRS")), "0.00"), dr("NARRATION").ToString, Val(dr("FROMNO")), Val(dr("FROMSRNO")), dr("FROMTYPE"), dr("DONE"), Val(dr("OUTPCS")), Val(dr("OUTMTRS")))


                        If Val(dr("OUTPCS")) > 0 Or Val(dr("OUTMTRS")) > 0 Then
                            GRIDWASTAGE.Rows(GRIDWASTAGE.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                            lbllocked.Visible = True
                            PBlock.Visible = True
                        End If

                    Next
                    TOTAL()
                    getsrno(GRIDWASTAGE)
                    If GRIDWASTAGE.RowCount > 0 Then TXTSRNO.Text = Val(GRIDWASTAGE.RowCount) + 1 Else TXTSRNO.Text = 1


                    Dim OBJCMN As New ClsCommon
                    dttable = OBJCMN.search(" GWASTAGE_SRNO AS GRIDSRNO, GWASTAGE_REMARKS AS REMARKS, GWASTAGE_NAME AS NAME, GWASTAGE_PHOTO AS IMGPATH ", "", " GREYWASTAGE_UPLOAD", " AND GWASTAGE_NO = " & TEMPGWASTAGENO & " AND GWASTAGE_YEARID = " & YearId)
                    If dttable.Rows.Count > 0 Then
                        For Each DTR As DataRow In dttable.Rows
                            gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                        Next
                    End If
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

            alParaval.Add(Format(Convert.ToDateTime(DTWASTAGE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBOURGODOWN.Text.Trim)
            alParaval.Add(Val(LBLTOTALPCS.Text.Trim))
            alParaval.Add(Val(LBLTOTALMTRS.Text.Trim))
            alParaval.Add(TXTREMARKS.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)


            Dim GRIDSRNO As String = ""
            Dim TYPE As String = ""
            Dim QUALITY As String = ""
            Dim TAKANO As String = ""
            Dim PCS As String = ""
            Dim MTRS As String = ""
            Dim NARRATION As String = ""
            Dim FROMNO As String = ""
            Dim FROMSRNO As String = ""
            Dim FROMTYPE As String = ""
            Dim DONE As String = ""
            Dim OUTPCS As String = ""
            Dim OUTMTRS As String = ""



            For Each row As Windows.Forms.DataGridViewRow In GRIDWASTAGE.Rows
                If row.Cells(0).Value <> Nothing Then
                    If GRIDSRNO = "" Then
                        GRIDSRNO = row.Cells(GSRNO.Index).Value.ToString
                        TYPE = row.Cells(GTYPE.Index).Value.ToString
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                        TAKANO = Val(row.Cells(GTAKANO.Index).Value)
                        PCS = Val(row.Cells(GPCS.Index).Value)
                        MTRS = Val(row.Cells(GMTRS.Index).Value)
                        NARRATION = row.Cells(GNARRATION.Index).Value.ToString
                        FROMNO = Val(row.Cells(GFROMNO.Index).Value)
                        FROMSRNO = Val(row.Cells(GFROMSRNO.Index).Value)
                        FROMTYPE = row.Cells(GFROMTYPE.Index).Value
                        If Convert.ToBoolean(row.Cells(GDONE.Index).Value) = True Then DONE = "1" Else DONE = "0"
                        OUTPCS = Val(row.Cells(GOUTPCS.Index).Value)
                        OUTMTRS = Val(row.Cells(GOUTMTRS.Index).Value)
                    Else
                        GRIDSRNO = GRIDSRNO & "|" & row.Cells(GSRNO.Index).Value.ToString
                        TYPE = TYPE & "|" & row.Cells(GTYPE.Index).Value
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        TAKANO = TAKANO & "|" & Val(row.Cells(GTAKANO.Index).Value)
                        PCS = PCS & "|" & Val(row.Cells(GPCS.Index).Value)
                        MTRS = MTRS & "|" & Val(row.Cells(GMTRS.Index).Value)
                        NARRATION = NARRATION & "|" & row.Cells(GNARRATION.Index).Value.ToString
                        FROMNO = FROMNO & "|" & Val(row.Cells(GFROMNO.Index).Value)
                        FROMSRNO = FROMSRNO & "|" & Val(row.Cells(GFROMSRNO.Index).Value)
                        FROMTYPE = FROMTYPE & "|" & row.Cells(GFROMTYPE.Index).Value
                        If Convert.ToBoolean(row.Cells(GDONE.Index).Value) = True Then DONE = DONE & "|" & "1" Else DONE = DONE & "|" & "0"
                        OUTPCS = OUTPCS & "|" & Val(row.Cells(GOUTPCS.Index).Value)
                        OUTMTRS = OUTMTRS & "|" & Val(row.Cells(GOUTMTRS.Index).Value)
                    End If
                End If
            Next

            alParaval.Add(GRIDSRNO)
            alParaval.Add(TYPE)
            alParaval.Add(QUALITY)
            alParaval.Add(TAKANO)
            alParaval.Add(PCS)
            alParaval.Add(MTRS)
            alParaval.Add(NARRATION)
            alParaval.Add(FROMNO)
            alParaval.Add(FROMSRNO)
            alParaval.Add(FROMTYPE)
            alParaval.Add(DONE)
            alParaval.Add(OUTPCS)
            alParaval.Add(OUTMTRS)


            Dim OBJGW As New ClsGreyWastage
            OBJGW.alParaval = alParaval
            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DTTABLE As DataTable = OBJGW.SAVE()
                TEMPGWASTAGENO = DTTABLE.Rows(0).Item(0)
                MessageBox.Show("Details Added")

            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPGWASTAGENO)
                IntResult = OBJGW.UPDATE()
                MessageBox.Show("Details Updated")
                EDIT = False
            End If


            'PRINTREPORT(TEMPISSUENO)
            If gridupload.RowCount > 0 Then SAVEUPLOAD()

            CLEAR()
            CMBOURGODOWN.Focus()

        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Function errorvalid() As Boolean
        Try
            Dim bln As Boolean = True

            If DTWASTAGE.Text = "__/__/____" Then
                EP.SetError(DTWASTAGE, " Please Enter Proper Date")
                bln = False
            Else
                If Not datecheck(DTWASTAGE.Text) Then
                    EP.SetError(DTWASTAGE, "Date not in Accounting Year")
                    bln = False
                End If
            End If


            If CMBOURGODOWN.Text.Trim.Length = 0 Then
                EP.SetError(CMBOURGODOWN, "Please Select Godown Name")
                bln = False
            End If


            If GRIDWASTAGE.RowCount = 0 Then
                EP.SetError(TXTNARR, "Enter Yarn Details")
                bln = False
            End If


            If lbllocked.Visible = True Then
                EP.SetError(lbllocked, "Entry Locked")
                bln = False
            End If

            Return bln
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Sub FILLUPLOAD()
        Try
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
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SAVEUPLOAD()
        Try
            Dim OBJGW As New ClsGreyWastage
            For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                Dim MS As New IO.MemoryStream
                Dim ALPARAVAL As New ArrayList
                If row.Cells(GUSRNO.Index).Value <> Nothing Then
                    ALPARAVAL.Add(TEMPGWASTAGENO)
                    ALPARAVAL.Add(row.Cells(GUSRNO.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUREMARKS.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUNAME.Index).Value)

                    PBSOFTCOPY.Image = row.Cells(GUIMGPATH.Index).Value
                    PBSOFTCOPY.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                    ALPARAVAL.Add(MS.ToArray)
                    ALPARAVAL.Add(YearId)

                    OBJGW.alParaval = ALPARAVAL
                    Dim INTRES As Integer = OBJGW.SAVEUPLOAD()
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

    Sub getsrno(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            For Each row As DataGridViewRow In grid.Rows
                row.Cells(0).Value = row.Index + 1
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub TOTAL()
        Try
            LBLTOTALPCS.Text = 0.0
            LBLTOTALMTRS.Text = 0.0
            For Each ROW As DataGridViewRow In GRIDWASTAGE.Rows
                If ROW.Cells(GSRNO.Index).Value <> Nothing Then
                    LBLTOTALPCS.Text = Format(Val(LBLTOTALPCS.Text) + Val(ROW.Cells(GPCS.Index).EditedFormattedValue), "0.00")
                    LBLTOTALMTRS.Text = Format(Val(LBLTOTALMTRS.Text) + Val(ROW.Cells(GMTRS.Index).EditedFormattedValue), "0.00")
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            GRIDWASTAGE.RowCount = 0
LINE1:
            TEMPGWASTAGENO = Val(TXTGWASTAGENO.Text) - 1
Line2:
            If TEMPGWASTAGENO > 0 Then

                Dim OBJCMN As New ClsCommon
                Dim DT As New DataTable
                DT = OBJCMN.search("GWASTAGE_NO ", "", " GREYWASTAGE ", " AND GWASTAGE_NO = '" & TEMPGWASTAGENO & "' AND GREYWASTAGE.GWASTAGE_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    EDIT = True
                    GreyWastage_Load(sender, e)
                Else
                    TEMPGWASTAGENO = Val(TEMPGWASTAGENO - 1)
                    GoTo Line2
                End If
            Else
                CLEAR()
                EDIT = False
            End If

            If GRIDWASTAGE.RowCount = 0 And TEMPGWASTAGENO > 1 Then
                TXTGWASTAGENO.Text = TEMPGWASTAGENO
                GoTo LINE1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            GRIDWASTAGE.RowCount = 0
LINE1:
            TEMPGWASTAGENO = Val(TXTGWASTAGENO.Text) + 1
            GETMAX_WASTAGE_NO()
            Dim MAXNO As Integer = TXTGWASTAGENO.Text.Trim
            CLEAR()
            If Val(TXTGWASTAGENO.Text) - 1 >= TEMPGWASTAGENO Then
                EDIT = True
                GreyWastage_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDWASTAGE.RowCount = 0 And TEMPGWASTAGENO < MAXNO Then
                TXTGWASTAGENO.Text = TEMPGWASTAGENO
                GoTo LINE1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TSTXTBILLNO.KeyPress
        numkeypress(e, TSTXTBILLNO, Me)
    End Sub

    Private Sub tstxtbillno_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSTXTBILLNO.Validated
        Try
            If Val(TSTXTBILLNO.Text.Trim) > 0 Then
                GRIDWASTAGE.RowCount = 0
                TEMPGWASTAGENO = Val(TSTXTBILLNO.Text)
                If TEMPGWASTAGENO > 0 Then
                    EDIT = True
                    GreyWastage_Load(sender, e)
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

    Private Sub GRIDWASTAGE_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDWASTAGE.CellDoubleClick
        Try
            If e.RowIndex = -1 Then Exit Sub

            If e.RowIndex >= 0 And GRIDWASTAGE.Item(GSRNO.Index, e.RowIndex).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                TXTSRNO.Text = GRIDWASTAGE.Item(GSRNO.Index, e.RowIndex).Value
                CMBTYPE.Text = GRIDWASTAGE.Item(GTYPE.Index, e.RowIndex).Value
                CMBGREYQUALITY.Text = GRIDWASTAGE.Item(GQUALITY.Index, e.RowIndex).Value
                TXTTAKANO.Text = GRIDWASTAGE.Item(GTAKANO.Index, e.RowIndex).Value
                TXTPCS.Text = Format(Val(GRIDWASTAGE.Item(GPCS.Index, e.RowIndex).Value), "0.00")
                TXTMTRS.Text = Format(Val(GRIDWASTAGE.Item(GMTRS.Index, e.RowIndex).Value), "0.00")
                TXTNARR.Text = GRIDWASTAGE.Item(GNARRATION.Index, e.RowIndex).Value

                TEMPROW = e.RowIndex
                CMBTYPE.Focus()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDWASTAGE_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDWASTAGE.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDWASTAGE.RowCount > 0 Then
                'dont allow user if any of the grid line is in edit mode.....
                'cmbitemname.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block
                GRIDWASTAGE.Rows.RemoveAt(GRIDWASTAGE.CurrentRow.Index)
                getsrno(GRIDWASTAGE)
                TOTAL()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim OBJBEAM As New GreyWastageDetails
            OBJBEAM.MdiParent = MDIMain
            OBJBEAM.FRMSTRING = FRMSTRING
            OBJBEAM.Show()
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
                    MsgBox("Unable to Delete, Checking Done / Item Used", MsgBoxStyle.Critical)
                    Exit Sub
                End If
                If MsgBox("Delete Grey Wastage Entry ?", MsgBoxStyle.YesNo) = vbYes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(TEMPGWASTAGENO)
                    alParaval.Add(YearId)

                    Dim ClsDO As New ClsGreyWastage
                    ClsDO.alParaval = alParaval
                    IntResult = ClsDO.Delete()

                    MsgBox("Grey Wastage Entry Deleted")
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

    Private Sub DTWASTAGE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTWASTAGE.GotFocus
        DTWASTAGE.SelectAll()
    End Sub

    Private Sub TXTMTRS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTMTRS.KeyPress, TXTPCS.KeyPress
        numdotkeypress(e, TXTMTRS, Me)
    End Sub

    Private Sub TXTNARR_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTNARR.Validating
        Try
            If CMBTYPE.Text.Trim <> "" And CMBGREYQUALITY.Text.Trim <> "" And (Val(TXTPCS.Text.Trim) > 0 Or Val(TXTMTRS.Text.Trim) > 0) Then
                FILLGRID()
            Else
                MsgBox("Please Enter proper details")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSELECTSTOCK_Click(sender As Object, e As EventArgs) Handles CMDSELECTSTOCK.Click
        Try
            If CMBOURGODOWN.Text.Trim = "" Then
                MsgBox("Please Select Godown First")
                CMBOURGODOWN.Focus()
                Exit Sub
            End If

            If DTWASTAGE.Text = "__/__/____" Then
                MsgBox("Please Select Date First")
                DTWASTAGE.Focus()
                Exit Sub
            End If

            If (EDIT = True And USEREDIT = False And USERVIEW = False) Or (EDIT = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Cursor.Current = Cursors.WaitCursor

            Dim DTTABLE As New DataTable

            If MULTIYARN = False Then
                Dim OBJSELECTYARN As New SelectGRNforDO
                OBJSELECTYARN.GODOWN = CMBOURGODOWN.Text.Trim
                OBJSELECTYARN.DODATE = Convert.ToDateTime(DTWASTAGE.Text).Date
                OBJSELECTYARN.ShowDialog()
                DTTABLE = OBJSELECTYARN.DT1
            Else
                Dim OBJSELECTTAKANO As New SelectTakaNoStock
                OBJSELECTTAKANO.GODOWN = CMBOURGODOWN.Text.Trim
                OBJSELECTTAKANO.DODATE = Convert.ToDateTime(DTWASTAGE.Text).Date
                OBJSELECTTAKANO.ShowDialog()
                DTTABLE = OBJSELECTTAKANO.DT1
            End If

            Dim i As Integer = 0
            If DTTABLE.Rows.Count > 0 Then
                For Each dr As DataRow In DTTABLE.Rows
                    GRIDWASTAGE.Rows.Add(dr("SRNO"), "WASTAGE", dr("QUALITY").ToString, dr("TAKANO"), Format(Val(dr("PCS")), "0.00"), Format(Val(dr("MTRS")), "0.00"), "", Val(dr("NO")), Val(dr("SRNO")), dr("GRIDTYPE"), 0, 0, 0)
                Next
                GRIDWASTAGE.FirstDisplayedScrollingRowIndex = GRIDWASTAGE.RowCount - 1
                getsrno(GRIDWASTAGE)
            End If

            TOTAL()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Function CHECKGRID() As Boolean
    '    Try
    '        Dim BLN As Boolean = True
    '        For Each ROW As DataGridViewRow In GRIDWASTAGE.Rows
    '            If ROW.Cells(GBEAMNAME.Index).Value = CMBGREYQUALITY.Text.Trim And CMBBEAMNO.Text.Trim = ROW.Cells(GBEAMNO.Index).Value Then
    '                If GRIDDOUBLECLICK = False Or (GRIDDOUBLECLICK = True And ROW.Index <> TEMPROW) Then
    '                    EP.SetError(TXTNARR, "Beam No already present in Grid Below")
    '                    BLN = False
    '                End If
    '            End If
    '        Next
    '        Return BLN
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    Sub FILLGRID()
        Try
            If GRIDDOUBLECLICK = False Then
                GRIDWASTAGE.Rows.Add(Val(TXTSRNO.Text.Trim), CMBTYPE.Text.Trim, CMBGREYQUALITY.Text.Trim, TXTTAKANO.Text.Trim, Format(Val(TXTPCS.Text.Trim), "0.00"), Format(Val(TXTMTRS.Text.Trim), "0.00"), TXTNARR.Text.Trim, 0, 0, "", 0, 0, 0)
            Else
                GRIDWASTAGE.Item(GSRNO.Index, TEMPROW).Value = TXTSRNO.Text.Trim
                GRIDWASTAGE.Item(GTYPE.Index, TEMPROW).Value = CMBTYPE.Text.Trim
                GRIDWASTAGE.Item(GQUALITY.Index, TEMPROW).Value = CMBGREYQUALITY.Text.Trim
                GRIDWASTAGE.Item(GTAKANO.Index, TEMPROW).Value = TXTTAKANO.Text.Trim
                GRIDWASTAGE.Item(GPCS.Index, TEMPROW).Value = Format(Val(TXTPCS.Text.Trim), "0.00")
                GRIDWASTAGE.Item(GMTRS.Index, TEMPROW).Value = Format(Val(TXTMTRS.Text.Trim), "0.00")
                GRIDWASTAGE.Item(GNARRATION.Index, TEMPROW).Value = TXTNARR.Text.Trim

                GRIDDOUBLECLICK = False
            End If
            CMBTYPE.SelectedItem = Nothing
            CMBGREYQUALITY.Text = ""
            TXTTAKANO.Clear()
            TXTPCS.Clear()
            TXTMTRS.Clear()
            TXTNARR.Clear()
            getsrno(GRIDWASTAGE)
            TOTAL()
            CMBTYPE.Focus()
            If GRIDWASTAGE.RowCount > 0 Then TXTSRNO.Text = Val(GRIDWASTAGE.RowCount) + 1 Else TXTSRNO.Text = 1
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DTWASTAGE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DTWASTAGE.Validating
        Try
            If DTWASTAGE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(DTWASTAGE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGREYQUALITY_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBGREYQUALITY.Enter
        Try
            If CMBGREYQUALITY.Text.Trim = "" Then FILLGREYSTOCKQUALITY()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGREYQUALITY_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBGREYQUALITY.Validating
        Try
            If CMBGREYQUALITY.Text.Trim <> "" Then GREYVALIDATE(CMBGREYQUALITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GreyWastage_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If ALLOWMFG = False Then Exit Sub
    End Sub
End Class