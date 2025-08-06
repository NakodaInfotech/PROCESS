
Imports BL

Public Class JobIn

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK, GRIDUPLOADDOUBLECLICK As Boolean
    Dim TEMPROW, TEMPUPLOADROW As Integer
    Public EDIT As Boolean
    Public TEMPJOBNO As Integer

    Private Sub cmdexit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdclear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        CLEAR()
        EDIT = False
        CMBNAME.Focus()
    End Sub

    Sub CLEAR()

        CMBTYPE.SelectedIndex = 0
        CMBTYPE.Enabled = True

        LBL.Text = "Godown Name"
        CMBNAME.Text = ""
        CMBNAME.DataSource = Nothing
        CMBNAME.Enabled = True

        DTJOBINDATE.Text = Mydate
        CMBJOBBERNAME.Text = ""
        cmbtrans.Text = ""
        TXTVEHICLENO.Clear()

        txtremarks.Clear()

        LBLTOTALBAGS.Text = 0
        LBLTOTALWT.Text = 0.0
        LBLTOTALWINDING.Text = 0
        LBLTOTALFIRKA.Text = 0
        LBLTOTALNALI.Text = 0

        TXTSRNO.Text = 1
        CMBQUALITY.Text = ""
        TXTLOTNO.Clear()
        CMBCOLOR.Text = ""
        TXTBAGS.Clear()
        TXTBAGNO.Clear()
        TXTCONES.Clear()
        TXTGROSSWT.Clear()
        TXTTAREWT.Clear()
        TXTWT.Clear()
        TXTWINDING.Clear()
        TXTFIRKA.Clear()
        TXTNALI.Clear()
        TXTNARR.Clear()
        TXTOUTWT.Clear()


        EP.Clear()
        lbllocked.Visible = False
        PBlock.Visible = False

        GRIDISSUE.RowCount = 0
        GETMAX_JOB_NO()

        GRIDDOUBLECLICK = False
        GRIDUPLOADDOUBLECLICK = False
        CMDSELECTJOB.Enabled = True

        TabControl1.SelectedIndex = 0

        PBSOFTCOPY.Image = Nothing
        TXTUPLOADSRNO.Clear()
        txtuploadname.Clear()
        txtuploadremarks.Clear()
        TXTIMGPATH.Clear()
        gridupload.RowCount = 0

        If gridupload.RowCount > 0 Then TXTUPLOADSRNO.Text = Val(gridupload.Rows(gridupload.RowCount - 1).Cells(0).Value) + 1 Else TXTUPLOADSRNO.Text = 1
    End Sub

    Sub GETMAX_JOB_NO()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax("ISNULL(MAX(JI_NO),0)+ 1", "JOBIN", "AND JI_YEARID = " & YearId)
        If DTTABLE.Rows.Count > 0 Then
            TXTJOBNO.Text = DTTABLE.Rows(0).Item(0)
        End If
    End Sub

    Private Sub JobIn_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        If CMBJOBBERNAME.Text.Trim = "" Then fillname(CMBJOBBERNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'JOBBER'")
        If cmbtrans.Text.Trim = "" Then fillname(cmbtrans, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'TRANSPORT'")
        If CMBQUALITY.Text.Trim = "" Then fillQUALITY(CMBQUALITY, EDIT)
        If CMBCOLOR.Text.Trim = "" Then FILLCOLOR(CMBCOLOR)
        'If CMBNAME.Text.Trim = "" Then fillGODOWN(CMBNAME, EDIT, " AND GODOWN_ISOUR = 'True'")

    End Sub

    Private Sub JobIn_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'MFG'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            FILLCMB()
            CLEAR()

            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim dttable As New DataTable
                Dim objclsDO As New ClsJobIn
                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(TEMPJOBNO)
                ALPARAVAL.Add(YearId)
                objclsDO.alParaval = ALPARAVAL
                dttable = objclsDO.selectJOBIN()

                If dttable.Rows.Count > 0 Then

                    For Each dr As DataRow In dttable.Rows

                        TXTJOBNO.Text = TEMPJOBNO
                        DTJOBINDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                        CMBTYPE.Text = Convert.ToString(dr("NAMETYPE").ToString)
                        CMBTYPE.Enabled = False
                        FILLNAMETYPE()
                        CMBNAME.Text = Convert.ToString(dr("NAME").ToString)
                        CMBNAME.Enabled = False
                        CMBJOBBERNAME.Text = Convert.ToString(dr("JOBBERNAME").ToString)
                        cmbtrans.Text = Convert.ToString(dr("TRANSNAME").ToString)
                        TXTVEHICLENO.Text = Convert.ToString(dr("VEHICLENO").ToString)

                        txtremarks.Text = Convert.ToString(dr("remarks").ToString)

                        'Item Grid
                        GRIDISSUE.Rows.Add(dr("SRNO"), dr("QUALITY").ToString, dr("LOTNO").ToString, dr("SHADE").ToString, Val(dr("BAGS")), dr("BAGNO"), Val(dr("CONES")), Format(Val(dr("GROSSWT")), "0.00"), Format(Val(dr("TAREWT")), "0.00"), Format(Val(dr("WT")), "0.000"), Val(dr("WINDING")), Val(dr("FIRKA")), Val(dr("NALI")), dr("NARR").ToString, Val(dr("OUTWT")))
                        If Val(dr("OUTWT")) > 0 Then
                            lbllocked.Visible = True
                            PBlock.Visible = True
                        End If

                    Next
                    CMDSELECTJOB.Enabled = False
                    TOTAL()
                Else
                    EDIT = False
                    CLEAR()
                End If

                Dim OBJCMN As New ClsCommon
                dttable = OBJCMN.search(" JI_SRNO AS GRIDSRNO, JI_REMARKS AS REMARKS, JI_NAME AS NAME, JI_PHOTO AS IMGPATH ", "", " JOBIN_UPLOAD", " AND JI_NO = " & TEMPJOBNO & " AND JI_YEARID = " & YearId)
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

    Private Sub cmdok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Dim IntResult As Integer
        Try
            Cursor.Current = Cursors.WaitCursor
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList

            alParaval.Add(Format(Convert.ToDateTime(DTJOBINDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBTYPE.Text.Trim)
            alParaval.Add(CMBNAME.Text.Trim)
            alParaval.Add(CMBJOBBERNAME.Text.Trim)
            alParaval.Add(cmbtrans.Text.Trim)
            alParaval.Add(TXTVEHICLENO.Text.Trim)
            alParaval.Add(Val(LBLTOTALBAGS.Text.Trim))
            alParaval.Add(Val(LBLTOTALWT.Text.Trim))
            alParaval.Add(Val(LBLTOTALWINDING.Text.Trim))
            alParaval.Add(Val(LBLTOTALFIRKA.Text.Trim))
            alParaval.Add(Val(LBLTOTALNALI.Text.Trim))
            alParaval.Add(txtremarks.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)


            Dim gridsrno As String = ""
            Dim QUALITY As String = ""
            Dim LOTNO As String = ""
            Dim SHADE As String = ""
            Dim BAGS As String = ""
            Dim BAGNO As String = ""
            Dim CONES As String = ""
            Dim GROSSWT As String = ""
            Dim TAREWT As String = ""
            Dim WT As String = ""
            Dim WINDING As String = ""
            Dim FIRKA As String = ""
            Dim NALI As String = ""
            Dim NARRATION As String = ""
            Dim OUTWT As String = ""



            For Each row As Windows.Forms.DataGridViewRow In GRIDISSUE.Rows
                If row.Cells(0).Value <> Nothing Then
                    If gridsrno = "" Then
                        gridsrno = row.Cells(gsrno.Index).Value.ToString
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                        LOTNO = row.Cells(GLOTNO.Index).Value.ToString
                        SHADE = row.Cells(GSHADE.Index).Value.ToString
                        BAGS = Val(row.Cells(GBAGS.Index).Value)
                        BAGNO = row.Cells(GBAGNO.Index).Value
                        CONES = Val(row.Cells(GCONES.Index).Value)
                        GROSSWT = row.Cells(GGROSSWT.Index).Value
                        TAREWT = row.Cells(GTAREWT.Index).Value
                        WT = Val(row.Cells(Gwt.Index).Value)
                        WINDING = Val(row.Cells(GWINDING.Index).Value)
                        FIRKA = Val(row.Cells(GFIRKA.Index).Value)
                        NALI = Val(row.Cells(gnali.Index).Value)
                        If row.Cells(GNARRATION.Index).Value <> Nothing Then NARRATION = row.Cells(GNARRATION.Index).Value.ToString Else NARRATION = ""
                        OUTWT = Val(row.Cells(GOUTWT.Index).Value)


                    Else
                        gridsrno = gridsrno & "|" & row.Cells(gsrno.Index).Value
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        LOTNO = LOTNO & "|" & row.Cells(GLOTNO.Index).Value.ToString
                        SHADE = SHADE & "|" & row.Cells(GSHADE.Index).Value.ToString
                        BAGS = BAGS & "|" & Val(row.Cells(GBAGS.Index).Value)
                        BAGNO = BAGNO & "|" & row.Cells(GBAGNO.Index).Value
                        CONES = CONES & "|" & Val(row.Cells(GCONES.Index).Value)
                        GROSSWT = GROSSWT & "|" & row.Cells(GGROSSWT.Index).Value
                        TAREWT = TAREWT & "|" & row.Cells(GTAREWT.Index).Value
                        WT = WT & "|" & Val(row.Cells(Gwt.Index).Value)
                        WINDING = WINDING & "|" & Val(row.Cells(GWINDING.Index).Value)
                        FIRKA = FIRKA & "|" & Val(row.Cells(GFIRKA.Index).Value)
                        NALI = NALI & "|" & Val(row.Cells(gnali.Index).Value)
                        If row.Cells(GNARRATION.Index).Value <> Nothing Then NARRATION = NARRATION & "|" & row.Cells(GNARRATION.Index).Value.ToString Else NARRATION = NARRATION & "|" & ""
                        OUTWT = OUTWT & "|" & Val(row.Cells(GOUTWT.Index).Value)

                    End If
                End If
            Next

            alParaval.Add(gridsrno)
            alParaval.Add(QUALITY)
            alParaval.Add(LOTNO)
            alParaval.Add(SHADE)
            alParaval.Add(BAGS)
            alParaval.Add(BAGNO)
            alParaval.Add(CONES)
            alParaval.Add(GROSSWT)
            alParaval.Add(TAREWT)
            alParaval.Add(WT)
            alParaval.Add(WINDING)
            alParaval.Add(FIRKA)
            alParaval.Add(NALI)
            alParaval.Add(OUTWT)
            alParaval.Add(NARRATION)


            Dim OBJWARPER As New ClsJobIn
            OBJWARPER.alParaval = alParaval
            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DTTABLE As DataTable = OBJWARPER.SAVE()
                TEMPJOBNO = DTTABLE.Rows(0).Item(0)
                MessageBox.Show("Details Added")

            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPJOBNO)
                IntResult = OBJWARPER.Update()
                MessageBox.Show("Details Updated")
                EDIT = False
            End If

            'PRINTREPORT(TEMPISSUENO)
            If gridupload.RowCount > 0 Then SAVEUPLOAD()

            CLEAR()
            'SHOW NEXT BILL ON EDIT MODE DONT CLEAR
            Call toolnext_Click(sender, e)
            CMBNAME.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True


        If DTJOBINDATE.Text = "__/__/____" Then
            EP.SetError(DTJOBINDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(DTJOBINDATE.Text) Then
                EP.SetError(DTJOBINDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        If CMBJOBBERNAME.Text.Trim.Length = 0 Then
            EP.SetError(CMBJOBBERNAME, "Please Fill Jobber Name")
            bln = False
        End If

        If CMBNAME.Text.Trim.Length = 0 Then
            EP.SetError(CMBNAME, " Please Enter Proper Details ")
            bln = False
        End If

        If GRIDISSUE.RowCount = 0 Then
            EP.SetError(CMBJOBBERNAME, "Select Stock")
            bln = False
        End If

        If cmbtrans.Text.Trim.Length = 0 Then
            EP.SetError(cmbtrans, " Please Fill Transport Name ")
            bln = False
        End If


        'DONE TEMPORARILY
        'If lbllocked.Visible = True Then
        '    EP.SetError(lbllocked, "Rec/Pay/TDS Made, Delete Rec/Pay/TDS First")
        '    bln = False
        'End If


        For Each row As DataGridViewRow In GRIDISSUE.Rows
            If Val(row.Cells(Gwt.Index).Value) = 0 Then
                EP.SetError(CMBJOBBERNAME, "Wt Cannot be 0")
                bln = False
            End If

            'If Val(row.Cells(GFRESH.Index).Value) = 0 And Val(row.Cells(GWINDING.Index).Value) = 0 And Val(row.Cells(GFIRKA.Index).Value) = 0 Then
            '    EP.SetError(CMBJOBBERNAME, "Fresh / Winding / Firka Cannot be 0")
            '    bln = False
            'End If
        Next


        'CHECKING STOCK
        'If GRIDISSUE.RowCount > 0 Then
        '    For Each row As DataGridViewRow In GRIDISSUE.Rows
        '        If Val(row.Cells(Gwt.Index).Value) > 0 Then

        '            Dim BALWT, BALWINDING, BALFIRKA As Integer

        '            Dim OBJCMN As New ClsCommonMaster
        '            'Dim dt As DataTable = OBJCMN.search(" ISNULL((JOBOUT_DESC.JO_WT - JOBOUT_DESC.JO_OUTWT), 0) AS WT,  ISNULL((JOBOUT_DESC.JO_WINDING - JOBOUT_DESC.JO_OUTWINDING), 0) AS WINDING, ISNULL((JOBOUT_DESC.JO_FIRKA - JOBOUT_DESC.JO_OUTFIRKA), 0) AS FIRKA ", "", "   JOBOUT_DESC ", " AND JO_NO = " & Val(row.Cells(GFROMNO.Index).Value) & " AND JO_GRIDSRNO = " & Val(row.Cells(GFROMSRNO.Index).Value) & "  AND JOBOUT_DESC.JO_Yearid = " & YearId)
        '            Dim dt As DataTable = OBJCMN.search(" ISNULL((JOBOUT_DESC.JO_WT - JOBOUT_DESC.JO_OUTWT), 0) AS WT,  ISNULL((JOBOUT_DESC.JO_WINDING), 0) AS WINDING, ISNULL((JOBOUT_DESC.JO_FIRKA), 0) AS FIRKA ", "", "   JOBOUT_DESC ", "AND JOBOUT_DESC.JO_Yearid = " & YearId)
        '            If dt.Rows.Count <= 0 Then GoTo LINE1
        '            BALWT = dt.Rows(0).Item("WT")
        '            BALWINDING = dt.Rows(0).Item("WINDING")
        '            BALFIRKA = dt.Rows(0).Item("FIRKA")

        '            If EDIT = True Then

        '                Dim DT1 As New DataTable
        '                DT1 = OBJCMN.search(" ISNULL(JOBIN_DESC.JI_WT, 0) AS WT,  ISNULL(JOBIN_DESC.JI_WINDING, 0) AS WINDING, ISNULL(JOBIN_DESC.JI_FIRKA, 0) AS FIRKA ", "", "   JOBIN_DESC ", "AND JOBIN_DESC.JI_Yearid = " & YearId)

        '                If DT1.Rows.Count > 0 Then
        '                    BALWT = BALWT + Val(DT1.Rows(0).Item("WT"))
        '                    BALWINDING = BALWINDING + Val(DT1.Rows(0).Item("WINDING"))
        '                    BALFIRKA = BALFIRKA + Val(DT1.Rows(0).Item("FIRKA"))
        '                End If
        '            End If

        '                    If Val(row.Cells(Gwt.Index).Value) > Format(Val(BALWT), "0.00") Then
        'LINE1:
        '                        EP.SetError(LBLTOTALWT, "Wt Not Present only " & Val(BALWT) & " Wt Allowed")
        '                        GRIDISSUE.CurrentRow.DefaultCellStyle.BackColor = Drawing.Color.Yellow
        '                        bln = False
        '                    End If


        '                    If Val(row.Cells(GWINDING.Index).Value) > 0 Then
        '                        If Val(row.Cells(GWINDING.Index).Value) > Format(Val(BALWINDING), "0") Then
        '                            EP.SetError(LBLTOTALWINDING, "Winding Cones Not Present only " & Val(BALWINDING) & " Cones Allowed")
        '                            GRIDISSUE.CurrentRow.DefaultCellStyle.BackColor = Drawing.Color.Yellow
        '                            bln = False
        '                        End If
        '                    End If

        '                    If Val(row.Cells(GFIRKA.Index).Value) > 0 Then
        '                        If Val(row.Cells(GFIRKA.Index).Value) > Format(Val(BALFIRKA), "0") Then
        '                            EP.SetError(LBLTOTALFIRKA, "Firka Cones Not Present only " & Val(BALFIRKA) & " Cones Allowed")
        '                            GRIDISSUE.CurrentRow.DefaultCellStyle.BackColor = Drawing.Color.Yellow
        '                            bln = False
        '                        End If
        '                    End If

        'End If
        '    Next
        'End If


        Return bln
    End Function

    Sub fillgrid()

        GRIDISSUE.Enabled = True

        If GRIDDOUBLECLICK = False Then
            GRIDISSUE.Rows.Add(Val(TXTSRNO.Text.Trim), CMBQUALITY.Text.Trim, TXTLOTNO.Text.Trim, CMBCOLOR.Text.Trim, Val(TXTBAGS.Text.Trim), TXTBAGNO.Text.Trim, Val(TXTCONES.Text.Trim), Format(Val(TXTGROSSWT.Text.Trim), "0.00"), Format(Val(TXTTAREWT.Text.Trim), "0.00"), Format(Val(TXTWT.Text.Trim), "0.00"), Val(TXTWINDING.Text.Trim), Val(TXTFIRKA.Text.Trim), Val(TXTNALI.Text.Trim), TXTNARR.Text.Trim, Format(Val(TXTOUTWT.Text.Trim), "0.00"), 0)
            getsrno(GRIDISSUE)
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDISSUE.Item(gsrno.Index, TEMPROW).Value = Val(TXTSRNO.Text.Trim)
            GRIDISSUE.Item(GQUALITY.Index, TEMPROW).Value = CMBQUALITY.Text.Trim
            GRIDISSUE.Item(GLOTNO.Index, TEMPROW).Value = TXTLOTNO.Text.Trim
            GRIDISSUE.Item(GSHADE.Index, TEMPROW).Value = CMBCOLOR.Text.Trim
            GRIDISSUE.Item(GBAGNO.Index, TEMPROW).Value = TXTBAGNO.Text.Trim
            GRIDISSUE.Item(GCONES.Index, TEMPROW).Value = Val(TXTCONES.Text.Trim)
            GRIDISSUE.Item(GGROSSWT.Index, TEMPROW).Value = Format(Val(TXTGROSSWT.Text.Trim), "0.00")
            GRIDISSUE.Item(GTAREWT.Index, TEMPROW).Value = Format(Val(TXTTAREWT.Text.Trim), "0.00")
            GRIDISSUE.Item(Gwt.Index, TEMPROW).Value = Format(Val(TXTWT.Text.Trim), "0.00")
            GRIDISSUE.Item(GWINDING.Index, TEMPROW).Value = Val(TXTWINDING.Text.Trim)
            GRIDISSUE.Item(GFIRKA.Index, TEMPROW).Value = Val(TXTFIRKA.Text.Trim)
            GRIDISSUE.Item(gnali.Index, TEMPROW).Value = Val(TXTNALI.Text.Trim)
            GRIDISSUE.Item(GNARRATION.Index, TEMPROW).Value = TXTNARR.Text.Trim
            GRIDISSUE.Item(GOUTWT.Index, TEMPROW).Value = Format(Val(TXTOUTWT.Text.Trim), "0.00")
            GRIDDOUBLECLICK = False
        End If

        TOTAL()

        GRIDISSUE.FirstDisplayedScrollingRowIndex = GRIDISSUE.RowCount - 1

        TXTSRNO.Text = Val(GRIDISSUE.RowCount) + 1

        'BY DEFAULT GET NEXT BAGNO
        If Val(TXTBAGNO.Text.Trim) > 0 Then TXTBAGNO.Text = Val(TXTBAGNO.Text.Trim) + 1
        TXTBAGS.Clear()
        TXTCONES.Clear()
        TXTGROSSWT.Clear()
        TXTTAREWT.Clear()
        TXTWT.Clear()
        TXTNARR.Clear()
        CMBQUALITY.Focus()
        TXTOUTWT.Clear()

    End Sub

    Private Sub GRIDISSUE_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDISSUE.CellDoubleClick
        EDITROW()
    End Sub

    Sub EDITROW()
        Try
            If GRIDISSUE.CurrentRow.Index >= 0 And GRIDISSUE.Item(gsrno.Index, GRIDISSUE.CurrentRow.Index).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                TXTSRNO.Text = GRIDISSUE.Item(gsrno.Index, GRIDISSUE.CurrentRow.Index).Value.ToString
                CMBQUALITY.Text = GRIDISSUE.Item(GQUALITY.Index, GRIDISSUE.CurrentRow.Index).Value.ToString
                TXTLOTNO.Text = GRIDISSUE.Item(GLOTNO.Index, GRIDISSUE.CurrentRow.Index).Value.ToString
                CMBCOLOR.Text = GRIDISSUE.Item(GSHADE.Index, GRIDISSUE.CurrentRow.Index).Value.ToString
                TXTBAGS.Text = Val(GRIDISSUE.Item(GBAGS.Index, GRIDISSUE.CurrentRow.Index).Value)
                TXTBAGNO.Text = GRIDISSUE.Item(GBAGNO.Index, GRIDISSUE.CurrentRow.Index).Value.ToString
                TXTCONES.Text = Val(GRIDISSUE.Item(GCONES.Index, GRIDISSUE.CurrentRow.Index).Value)
                TXTGROSSWT.Text = Val(GRIDISSUE.Item(GGROSSWT.Index, GRIDISSUE.CurrentRow.Index).Value)
                TXTTAREWT.Text = Val(GRIDISSUE.Item(GTAREWT.Index, GRIDISSUE.CurrentRow.Index).Value)
                TXTWT.Text = Val(GRIDISSUE.Item(Gwt.Index, GRIDISSUE.CurrentRow.Index).Value)
                TXTWINDING.Text = Val(GRIDISSUE.Item(GWINDING.Index, GRIDISSUE.CurrentRow.Index).Value)
                TXTFIRKA.Text = Val(GRIDISSUE.Item(GFIRKA.Index, GRIDISSUE.CurrentRow.Index).Value)
                TXTNALI.Text = Val(GRIDISSUE.Item(gnali.Index, GRIDISSUE.CurrentRow.Index).Value)
                TXTNARR.Text = GRIDISSUE.Item(GNARRATION.Index, GRIDISSUE.CurrentRow.Index).Value.ToString
                TXTOUTWT.Text = Val(GRIDISSUE.Item(GOUTWT.Index, GRIDISSUE.CurrentRow.Index).Value)

                TEMPROW = GRIDISSUE.CurrentRow.Index
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

            Dim OBJDO As New ClsJobIn
            For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                Dim MS As New IO.MemoryStream
                Dim ALPARAVAL As New ArrayList
                If row.Cells(GUSRNO.Index).Value <> Nothing Then
                    ALPARAVAL.Add(TEMPJOBNO)
                    ALPARAVAL.Add(row.Cells(GUSRNO.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUREMARKS.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUNAME.Index).Value)

                    PBSOFTCOPY.Image = row.Cells(GUIMGPATH.Index).Value
                    PBSOFTCOPY.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                    ALPARAVAL.Add(MS.ToArray)
                    ALPARAVAL.Add(YearId)

                    OBJDO.alParaval = ALPARAVAL
                    Dim INTRES As Integer = OBJDO.SAVEUPLOAD()
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBJOBBERNAME_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBJOBBERNAME.Enter
        Try
            If CMBJOBBERNAME.Text.Trim = "" Then fillname(CMBJOBBERNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'JOBBER'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBJOBBERNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBJOBBERNAME.Validating
        Try
            If CMBJOBBERNAME.Text.Trim <> "" Then namevalidate(CMBJOBBERNAME, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'JOBBER'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "JOBBER")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBJOBBERNAME_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBJOBBERNAME.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'JOBBER'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBJOBBERNAME.Text = OBJLEDGER.TEMPNAME
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

    Private Sub CMBCOLOR_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBCOLOR.Enter
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
            LBLTOTALWT.Text = 0.0
            LBLTOTALWINDING.Text = 0.0
            LBLTOTALFIRKA.Text = 0.0
            LBLTOTALNALI.Text = 0.0

            For Each ROW As DataGridViewRow In GRIDISSUE.Rows
                If ROW.Cells(gsrno.Index).Value <> Nothing Then
                    LBLTOTALBAGS.Text = Format(Val(LBLTOTALBAGS.Text) + Val(ROW.Cells(GBAGS.Index).EditedFormattedValue), "0")
                    LBLTOTALWT.Text = Format(Val(LBLTOTALWT.Text) + Val(ROW.Cells(Gwt.Index).EditedFormattedValue), "0.000")
                    LBLTOTALWINDING.Text = Format(Val(LBLTOTALWINDING.Text) + Val(ROW.Cells(GWINDING.Index).EditedFormattedValue), "0")
                    LBLTOTALFIRKA.Text = Format(Val(LBLTOTALFIRKA.Text) + Val(ROW.Cells(GFIRKA.Index).EditedFormattedValue), "0")
                    LBLTOTALNALI.Text = Format(Val(LBLTOTALNALI.Text) + Val(ROW.Cells(gnali.Index).EditedFormattedValue), "0")

                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
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

    Private Sub TXTCONES_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTCONES.KeyPress, TXTBAGS.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTCONES_Validated(sender As Object, e As EventArgs) Handles TXTCONES.Validated, TXTGROSSWT.Validated
        CALC()
    End Sub

    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            GRIDISSUE.RowCount = 0
LINE1:
            TEMPJOBNO = Val(TXTJOBNO.Text) - 1
Line2:
            If TEMPJOBNO > 0 Then

                Dim OBJCMN As New ClsCommon
                Dim DT As New DataTable
                DT = OBJCMN.search("JI_NO ", "", "  JOBIN ", " AND JI_NO = '" & TEMPJOBNO & "' AND JOBIN.JI_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    EDIT = True
                    JobIn_Load(sender, e)
                Else
                    TEMPJOBNO = Val(TEMPJOBNO - 1)
                    GoTo Line2
                End If
            Else
                CLEAR()
                EDIT = False
            End If

            If GRIDISSUE.RowCount = 0 And TEMPJOBNO > 1 Then
                TXTJOBNO.Text = TEMPJOBNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            GRIDISSUE.RowCount = 0
LINE1:
            TEMPJOBNO = Val(TXTJOBNO.Text) + 1
            GETMAX_JOB_NO()
            Dim MAXNO As Integer = TXTJOBNO.Text.Trim
            CLEAR()
            If Val(TXTJOBNO.Text) - 1 >= TEMPJOBNO Then
                EDIT = True
                JobIn_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDISSUE.RowCount = 0 And TEMPJOBNO < MAXNO Then
                TXTJOBNO.Text = TEMPJOBNO
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
                GRIDISSUE.RowCount = 0
                TEMPJOBNO = Val(tstxtbillno.Text)
                If TEMPJOBNO > 0 Then
                    EDIT = True
                    JobIn_Load(sender, e)
                Else
                    CLEAR()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
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

    Private Sub GRIDISSUE_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDISSUE.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDISSUE.RowCount > 0 Then
                'dont allow user if any of the grid line is in edit mode.....
                'cmbitemname.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block
                GRIDISSUE.Rows.RemoveAt(GRIDISSUE.CurrentRow.Index)
                getsrno(GRIDISSUE)
                TOTAL()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim OBJjobin As New JobInDetails
            OBJjobin.MdiParent = MDIMain
            OBJjobin.Show()
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
                    MsgBox("Unable to Delete, Entry Locked", MsgBoxStyle.Critical)
                    Exit Sub
                End If
                If MsgBox("Delete Entry?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub


                Dim alParaval As New ArrayList
                alParaval.Add(TEMPJOBNO)
                alParaval.Add(YearId)

                Dim ClsJI As New ClsJobIn
                ClsJI.alParaval = alParaval
                IntResult = ClsJI.Delete()


                MsgBox("Job In Deleted")
                CLEAR()
                EDIT = False

            Else
                MsgBox("Delete is only in Edit Mode")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub DTJOBINDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTJOBINDATE.GotFocus
        DTJOBINDATE.SelectAll()
    End Sub

    Private Sub CMDSELECTJOB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSELECTJOB.Click
        Try

            If (EDIT = True And USEREDIT = False And USERVIEW = False) Or (EDIT = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If CMBJOBBERNAME.Text.Trim <> "" Then

                Dim OBJSELECTSTOCK As New SelectJobOut
                OBJSELECTSTOCK.TEMPJOBBERNAME = CMBJOBBERNAME.Text.Trim
                Dim DTSTOCK As DataTable = OBJSELECTSTOCK.DT
                OBJSELECTSTOCK.ShowDialog()
                If DTSTOCK.Rows.Count > 0 Then
                    For Each ROW As DataRow In DTSTOCK.Rows
                        GRIDISSUE.Rows.Add(0, ROW("QUALITY").ToString, ROW("LOTNO"), ROW("SHADE").ToString, 0, ROW("BAGNO"), Val(ROW("CONES")), Format(Val(ROW("GROSSWT")), "0.00"), Format(Val(ROW("TAREWT")), "0.00"), Format(Val(ROW("WT")), "0.0000"), Format(Val(ROW("WINDING")), "0"), Format(Val(ROW("FIRKA")), "0"), 0, "", 0)
                    Next
                    TOTAL()
                    getsrno(GRIDISSUE)
                    CMDSELECTJOB.Enabled = False
                End If
            Else
                MsgBox("Select Jobber Name")
                CMBJOBBERNAME.Focus()
            End If
            TOTAL()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub GRIDISSUE_CellValidating(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles GRIDISSUE.CellValidating
        Try
            Dim colNum As Integer = GRIDISSUE.Columns(e.ColumnIndex).Index
            If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return

            Select Case colNum

                Case Gwt.Index ', gcount.Index
                    Dim dDebit As Decimal
                    Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

                    If bValid Then
                        If GRIDISSUE.CurrentCell.Value = Nothing Then GRIDISSUE.CurrentCell.Value = "0.000"
                        GRIDISSUE.CurrentCell.Value = Convert.ToDecimal(GRIDISSUE.Item(colNum, e.RowIndex).Value)
                        '' everything is good
                        TOTAL()
                    Else
                        MessageBox.Show("Invalid Number Entered")
                        e.Cancel = True
                        Exit Sub
                    End If

                Case GWINDING.Index, GFIRKA.Index, gnali.Index
                    Dim dDebit As Integer
                    Dim bValid As Boolean = Integer.TryParse(e.FormattedValue.ToString, dDebit)

                    If bValid Then
                        If GRIDISSUE.CurrentCell.Value = Nothing Then GRIDISSUE.CurrentCell.Value = "0"
                        GRIDISSUE.CurrentCell.Value = Convert.ToInt32(GRIDISSUE.Item(colNum, e.RowIndex).Value)
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

    Sub FILLNAMETYPE()
        Try
            CMBNAME.Text = ""
            CMBNAME.DataSource = Nothing
            CMBNAME.Enabled = True

            If CMBTYPE.Text = "SELF" Then
                LBL.Text = "Godown Name"
                fillGODOWN(CMBNAME, EDIT, " AND GODOWN_ISOUR = 'True'")
            ElseIf CMBTYPE.Text = "WARPER" Then
                LBL.Text = "Warper Name"
                fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WARPER'") ''''TYPE = WARPER
            ElseIf CMBTYPE.Text = "SIZER" Then
                LBL.Text = "Sizer Name"
                fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'") ''''TYPE = SIZER
            ElseIf CMBTYPE.Text = "WEAVER" Then
                LBL.Text = "Weaver Name"
                fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'") ''''TYPE = WEAVER
            ElseIf CMBTYPE.Text = "PROCESSOR" Then
                LBL.Text = "Processor Name"
                fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'") ''''TYPE = processor
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTYPE_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBTYPE.Validated
        Try
            FILLNAMETYPE()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBNAME.Validated
        If CMBNAME.Text.Trim <> "" Then CMBNAME.Enabled = False
    End Sub

    Private Sub DTJOBINDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DTJOBINDATE.Validating
        Try
            If DTJOBINDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(DTJOBINDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTNARR_Validated(sender As Object, e As EventArgs) Handles TXTNARR.Validated
        Try
            If CMBQUALITY.Text.Trim <> "" And Val(TXTWT.Text.Trim) > 0 And Val(TXTBAGS.Text.Trim) > 0 Then
                fillgrid()
            Else
                MsgBox("Enter Proper Details", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class