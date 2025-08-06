
Imports System.ComponentModel
Imports BL

Public Class YarnReceivedMachine
    Public EDIT As Boolean          'used for editing
    Dim IntResult As Integer
    Dim GRIDDOUBLECLICK As Boolean
    Dim GRIDUPLOADDOUBLECLICK As Boolean
    Public TEMPMACRECNO As Integer          'used for editing
    Dim TEMPROW As Integer
    Dim TEMPUPLOADROW As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim TEMPMSG As Integer
    Dim TEMPMTRS As Double = 0.0
    Dim PARTYCHALLANNO As String

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

    Private Sub CMBMACHINE_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBMACHINE.Enter
        Try
            If CMBMACHINE.Text.Trim = "" Then FILLMACHINE(CMBMACHINE)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMACHINE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBMACHINE.Validating
        Try
            If CMBMACHINE.Text.Trim <> "" Then MACHINEVALIDATE(CMBMACHINE, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub clear()

        CMBOURGODOWN.Text = GETDEFAULTGODOWN()
        EP.Clear()
        TXTSRNO.Text = 1
        CMBQUALITY.Text = ""
        TXTLOTNO.Clear()
        CMBCOLOR.Text = ""
        CMBMACHINE.Text = ""
        CMBMACHINE.Enabled = True
        TXTLABOURNAME.Clear()

        TXTBAGNO.Clear()
        TXTBAGS.Clear()
        TXTGROSSWT.Clear()
        TXTTAREWT.Clear()
        TXTNETTWT.Clear()
        CMBISSUENO.Items.Clear()
        CMBISSUENO.Text = ""
        CMBISSUENO.Enabled = True
        RECEIVEDDATE.Text = Mydate
        tstxtbillno.Clear()
        txtremarks.Clear()
        TXTBALWT.Clear()
        TXTACTUALISSUEWT.Clear()
        TXTRUNNINGBAL.Clear()
        TXTFROMNO.Clear()
        TXTFROMTYPE.Clear()
        TXTFROMSRNO.Clear()

        txtuploadsrno.Text = 1
        txtuploadremarks.Clear()
        txtimgpath.Clear()
        TXTNEWIMGPATH.Clear()
        TXTFILENAME.Clear()
        PBSoftCopy.ImageLocation = ""
        gridupload.RowCount = 0


        lbllocked.Visible = False
        PBlock.Visible = False


        LBLTOTALCONES.Text = 0.0
        LBLTOTALTAREWT.Text = 0.0
        LBLTOTALGROSSWT.Text = 0.0
        LBLTOTALNETTWT.Text = 0.0


        GRIDYARN.RowCount = 0
        GRIDYARNCOMP.RowCount = 0

        GRIDDOUBLECLICK = False
        GRIDUPLOADDOUBLECLICK = False
        getmaxno()

    End Sub

    Sub total()
        Try
            LBLTOTALCONES.Text = 0.0
            LBLTOTALGROSSWT.Text = 0.0
            LBLTOTALTAREWT.Text = 0.0
            LBLTOTALNETTWT.Text = 0.0



            For Each ROW As DataGridViewRow In GRIDYARN.Rows
                If ROW.Cells(gsrno.Index).Value <> Nothing Then
                    LBLTOTALCONES.Text = Format(Val(LBLTOTALCONES.Text) + Val(ROW.Cells(GBAGS.Index).EditedFormattedValue), "0.00")
                    LBLTOTALNETTWT.Text = Format(Val(LBLTOTALNETTWT.Text) + Val(ROW.Cells(GNETTWT.Index).EditedFormattedValue), "0.00")
                    LBLTOTALTAREWT.Text = Format(Val(LBLTOTALTAREWT.Text) + Val(ROW.Cells(GTAREWT.Index).EditedFormattedValue), "0.00")
                    LBLTOTALGROSSWT.Text = Format(Val(LBLTOTALGROSSWT.Text) + Val(ROW.Cells(GGROSSWT.Index).EditedFormattedValue), "0.00")

                    'ROW.Cells(GTAREWT.Index).Value = ((0.05 * ROW.Cells(GBAGS.Index).EditedFormattedValue) + 0.18)
                    'ROW.Cells(GNETTWT.Index).Value = ROW.Cells(GGROSSWT.Index).EditedFormattedValue - ROW.Cells(GTAREWT.Index).EditedFormattedValue
                End If
            Next
            TXTRUNNINGBAL.Text = Format(Val(TXTBALWT.Text.Trim) - Val(LBLTOTALNETTWT.Text.Trim), "0.00")


            'FILL GRIDCOMPOSITION
            Dim DONE As Boolean = False
            GRIDYARNCOMP.RowCount = 0

            For Each ROW As DataGridViewRow In GRIDYARN.Rows

                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" ISNULL(QUALITYMASTER.QUALITY_NAME, '') AS YARNCOMPOSITION, ISNULL(QUALITYMASTER_COMPOSITION.QUALITY_PER, 0) AS PER ", "", " QUALITYMASTER INNER JOIN QUALITYMASTER_COMPOSITION ON QUALITYMASTER.QUALITY_ID = QUALITYMASTER_COMPOSITION.QUALITY_YARNQUALITYID AND QUALITYMASTER.QUALITY_YEARID = QUALITYMASTER_COMPOSITION.QUALITY_YEARID INNER JOIN QUALITYMASTER AS MAINQUALITYMASTER ON QUALITYMASTER_COMPOSITION.QUALITY_ID = MAINQUALITYMASTER.QUALITY_ID ", " AND MAINQUALITYMASTER.QUALITY_NAME = '" & ROW.Cells(GYARNQUALITY.Index).Value & "' AND QUALITYMASTER_COMPOSITION.QUALITY_YEARID = " & YearId)
                If DT.Rows.Count = 0 Then Exit For

                For Each DTROW As DataRow In DT.Rows
                    If GRIDYARNCOMP.RowCount = 0 Then
                        GRIDYARNCOMP.Rows.Add(DTROW("YARNCOMPOSITION"), Format((Val(ROW.Cells(GNETTWT.Index).EditedFormattedValue) * Val(DTROW("PER"))) / 100, "0.000"))
                    Else
                        For Each COMPROW As DataGridViewRow In GRIDYARNCOMP.Rows
                            If COMPROW.Cells(CYARNQUALITY.Index).Value = DTROW("YARNCOMPOSITION") Then
                                COMPROW.Cells(CWT.Index).Value = Format(Val(COMPROW.Cells(CWT.Index).Value) + ((Val(ROW.Cells(GNETTWT.Index).EditedFormattedValue) * Val(DTROW("PER"))) / 100), "0.000")
                                DONE = True
                            End If
                        Next
                        If DONE = False Then GRIDYARNCOMP.Rows.Add(DTROW("YARNCOMPOSITION"), Format((Val(ROW.Cells(GNETTWT.Index).EditedFormattedValue) * Val(DTROW("PER"))) / 100, "0.000"))
                    End If
                Next

            Next


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdclear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        clear()
        EDIT = False
        CMBOURGODOWN.Focus()
    End Sub

    Sub getmaxno()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(YRECEIVEDMAC_NO),0) + 1 ", "YARNRECEIVEDMACHINE", " and YRECEIVEDMAC_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTRECNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub


    Function errorvalid() As Boolean
        Try
            Dim bln As Boolean = True

            If CMBOURGODOWN.Text.Trim.Length = 0 Then
                EP.SetError(CMBOURGODOWN, " Please Fill Godown")
                bln = False
            End If

            If CMBMACHINE.Text.Trim.Length = 0 Then
                EP.SetError(CMBMACHINE, " Select Machine Name")
                bln = False
            End If

            If Val(CMBISSUENO.Text.Trim) = 0 Then
                EP.SetError(CMBISSUENO, " Please Select Issue No")
                bln = False
            End If

            If GRIDYARN.RowCount = 0 Then
                EP.SetError(TabControl1, "Select Stock")
                bln = False
            End If


            If RECEIVEDDATE.Text = "__/__/____" Then
                EP.SetError(RECEIVEDDATE, " Please Enter Proper Date")
                bln = False
            Else
                If Not datecheck(RECEIVEDDATE.Text) Then
                    EP.SetError(RECEIVEDDATE, "Date not in Accounting Year")
                    bln = False
                End If
            End If


            'CHECKING WHETHER THE RECD YARN IS THE BYPRODUCT OF THE ISSUED YARN OR NOT
            'FIRST CHECK THE STOCK OF THE YARNCOMPOSITION ON THE SAME MACHINE NO
            Dim STOCKNOTPROPER As Boolean = False
            Dim BALANCEWT As Double = 0.0
            If ClientName <> "NIRMALA" Then
                For Each ROW As DataGridViewRow In GRIDYARNCOMP.Rows
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.search("ROUND(SUM(WT -  RECD),2) AS BALWT", "", " MACHINEYARNSTOCKREGISTER ", " AND QUALITY = '" & ROW.Cells(CYARNQUALITY.Index).Value & "' AND MACHINENAME = '" & CMBMACHINE.Text.Trim & "' AND YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        BALANCEWT = Format(Val(DT.Rows(0).Item("BALWT")), "0.00")

                        If EDIT = True Then
                            DT = OBJCMN.search("ISNULL(YRECEIVEDMAC_WT,0) AS WT", "", "YARNRECEIVEDMACHINE_COMPOSITION INNER JOIN QUALITYMASTER ON YRECEIVEDMAC_QUALITYID = QUALITY_ID", " AND QUALITY_NAME = '" & ROW.Cells(CYARNQUALITY.Index).Value & "' AND YRECEIVEDMAC_NO = " & Val(TEMPMACRECNO) & " AND YRECEIVEDMAC_YEARID = " & YearId)
                            If DT.Rows.Count > 0 Then BALANCEWT = BALANCEWT + Format(Val(DT.Rows(0).Item("WT")), "0.00")
                        End If

                        If Val(ROW.Cells(CWT.Index).Value) > Val(BALANCEWT) Then
                            STOCKNOTPROPER = True
                            Exit For
                        End If
                    Else
                        STOCKNOTPROPER = True
                        Exit For
                    End If
                    BALANCEWT = 0
                Next
                If STOCKNOTPROPER = True Then
                    bln = False
                    EP.SetError(CMBMACHINE, "Stock Not Present on Machine")
                End If
            End If



            Return bln
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Function


    Private Sub cmdok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        Try
            Cursor.Current = Cursors.WaitCursor
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList

            alParaval.Add(Format(Convert.ToDateTime(RECEIVEDDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBOURGODOWN.Text.Trim)
            alParaval.Add(CMBMACHINE.Text.Trim)
            alParaval.Add(CMBISSUENO.Text.Trim)
            alParaval.Add(TXTTYPE.Text.Trim)
            alParaval.Add(TXTLABOURNAME.Text.Trim)
            alParaval.Add(TXTBALWT.Text.Trim)
            alParaval.Add(TXTACTUALISSUEWT.Text.Trim)
            alParaval.Add(Val(LBLTOTALCONES.Text))
            alParaval.Add(Val(LBLTOTALGROSSWT.Text))
            alParaval.Add(Val(LBLTOTALTAREWT.Text))
            alParaval.Add(Val(LBLTOTALNETTWT.Text))
            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)


            Dim gridsrno As String = ""
            Dim QUALITY As String = ""
            Dim LOTNO As String = ""
            Dim SHADE As String = ""
            Dim BAGNO As String = ""
            Dim CONES As String = ""
            Dim GROSSWT As String = ""
            Dim TAREWT As String = ""
            Dim NETTWT As String = ""
            Dim OUTWT As String = ""
            Dim FROMNO As String = ""
            Dim FROMSRNO As String = ""
            Dim FROMTYPE As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDYARN.Rows
                If row.Cells(0).Value <> Nothing Then
                    If gridsrno = "" Then
                        gridsrno = row.Cells(gsrno.Index).Value.ToString
                        QUALITY = row.Cells(GYARNQUALITY.Index).Value.ToString
                        LOTNO = Val(row.Cells(GLOTNO.Index).Value)
                        SHADE = row.Cells(GSHADE.Index).Value.ToString
                        BAGNO = Val(row.Cells(GBAGNO.Index).Value)
                        CONES = Val(row.Cells(GBAGS.Index).Value.ToString)
                        GROSSWT = Val(row.Cells(GGROSSWT.Index).Value)
                        TAREWT = Val(row.Cells(GTAREWT.Index).Value)
                        NETTWT = Val(row.Cells(GNETTWT.Index).Value)
                        OUTWT = Val(row.Cells(GOUTWT.Index).Value)
                        FROMNO = row.Cells(GFROMNO.Index).Value.ToString
                        FROMSRNO = row.Cells(GFROMSRNO.Index).Value.ToString
                        FROMTYPE = row.Cells(GFROMTYPE.Index).Value.ToString
                    Else
                        gridsrno = gridsrno & "|" & row.Cells(gsrno.Index).Value
                        QUALITY = QUALITY & "|" & row.Cells(GYARNQUALITY.Index).Value.ToString
                        LOTNO = LOTNO & "|" & Val(row.Cells(GLOTNO.Index).Value)
                        SHADE = SHADE & "|" & row.Cells(GSHADE.Index).Value.ToString
                        BAGNO = BAGNO & "|" & Val(row.Cells(GBAGNO.Index).Value)
                        CONES = CONES & "|" & Val(row.Cells(GBAGS.Index).Value)
                        GROSSWT = GROSSWT & "|" & Val(row.Cells(GGROSSWT.Index).Value)
                        TAREWT = TAREWT & "|" & Val(row.Cells(GTAREWT.Index).Value)
                        NETTWT = NETTWT & "|" & Val(row.Cells(GNETTWT.Index).Value)
                        OUTWT = OUTWT & "|" & Val(row.Cells(GOUTWT.Index).Value)
                        FROMNO = FROMNO & "|" & row.Cells(GFROMNO.Index).Value.ToString
                        FROMSRNO = FROMSRNO & "|" & row.Cells(GFROMSRNO.Index).Value.ToString
                        FROMTYPE = FROMTYPE & "|" & row.Cells(GFROMTYPE.Index).Value.ToString

                    End If
                End If
            Next

            alParaval.Add(gridsrno)
            alParaval.Add(QUALITY)
            alParaval.Add(LOTNO)
            alParaval.Add(SHADE)
            alParaval.Add(BAGNO)
            alParaval.Add(CONES)
            alParaval.Add(GROSSWT)
            alParaval.Add(TAREWT)
            alParaval.Add(NETTWT)
            alParaval.Add(OUTWT)
            alParaval.Add(FROMNO)
            alParaval.Add(FROMSRNO)
            alParaval.Add(FROMTYPE)


            Dim COMPQUALITY As String = ""
            Dim COMPWT As String = ""
            For Each row As Windows.Forms.DataGridViewRow In GRIDYARNCOMP.Rows
                If row.Cells(0).Value <> Nothing Then
                    If COMPQUALITY = "" Then
                        COMPQUALITY = row.Cells(CYARNQUALITY.Index).Value.ToString
                        COMPWT = Val(row.Cells(CWT.Index).Value)
                    Else
                        COMPQUALITY = COMPQUALITY & "|" & row.Cells(CYARNQUALITY.Index).Value.ToString
                        COMPWT = COMPWT & "|" & Val(row.Cells(CWT.Index).Value)
                    End If
                End If
            Next

            alParaval.Add(COMPQUALITY)
            alParaval.Add(COMPWT)


            Dim OBJYISSUE As New ClsYarnReceivedMachine()
            OBJYISSUE.alParaval = alParaval
            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DTTABLE As DataTable = OBJYISSUE.SAVE()
                MsgBox("Details Added")
                TXTRECNO.Text = DTTABLE.Rows(0).Item(0)
                PRINTREPORT()

            ElseIf EDIT = True Then
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                alParaval.Add(TEMPMACRECNO)
                IntResult = OBJYISSUE.UPDATE()
                MsgBox("Details Updated")
                PRINTREPORT()

                If gridupload.RowCount > 0 Then SAVEUPLOAD()
                EDIT = False
            End If

            clear()
            RECEIVEDDATE.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub YarnReceivedMachine_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If errorvalid() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then cmdok_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.Alt = True And (e.KeyCode = Windows.Forms.Keys.D1) Then
                TabControl1.Focus()
                TabControl1.SelectedIndex = (0)
            ElseIf e.Alt = True And (e.KeyCode = Windows.Forms.Keys.D2) Then
                TabControl1.SelectedIndex = (1)
            ElseIf e.KeyCode = Keys.Oemcomma Then
                e.SuppressKeyPress = True
            ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
                Call OpenToolStripButton_Click(sender, e)
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for billno foucs
                tstxtbillno.Focus()
                tstxtbillno.SelectAll()
            ElseIf e.Alt = True And e.KeyCode = Keys.Left Then
                toolprevious_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.Right Then
                toolnext_Click(sender, e)
            ElseIf e.KeyCode = Keys.F5 Then     'grid focus
                GRIDYARN.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YarnReceivedMachine_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'MFG'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor

            fillcmb()
            clear()

            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim objYRECEIVEDMAC As New ClsYarnReceivedMachine()
                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(TEMPMACRECNO)
                ALPARAVAL.Add(YearId)
                objYRECEIVEDMAC.alParaval = ALPARAVAL
                Dim dttable As DataTable = objYRECEIVEDMAC.SELECTYARNRECMAC()

                If dttable.Rows.Count > 0 Then
                    For Each dr As DataRow In dttable.Rows
                        TXTRECNO.Text = TEMPMACRECNO
                        RECEIVEDDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                        CMBOURGODOWN.Text = Convert.ToString(dr("GODOWN").ToString)
                        CMBMACHINE.Text = Convert.ToString(dr("MACHINENAME").ToString)
                        CMBISSUENO.Text = Val(dr("ISSUENO"))
                        TXTTYPE.Text = dr("FROMTYPE")

                        TXTLABOURNAME.Text = dr("LABOURNAME")

                        TXTBALWT.Text = Val(dr("BALWT"))
                        TXTACTUALISSUEWT.Text = Val(dr("ACTAULISSUEWT"))
                        txtremarks.Text = dr("REMARKS")


                        GRIDYARN.Rows.Add(dr("SRNO").ToString, dr("QUALITY").ToString, dr("LOTNO"), dr("SHADE").ToString, dr("BAGNO"), Format(dr("CONES"), "0"), Format(dr("GROSSWT"), "0.00"), Format(dr("TAREWT"), "0.00"), Format(dr("NETTWT"), "0.00"), Format(dr("OUTWT"), "0.00"), Val(dr("FROMNO")), Val(dr("FROMSRNO")), dr("FROMTYPE"))

                        If Val(dr("OUTWT")) > 0 Then
                            GRIDYARN.Rows(GRIDYARN.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                            lbllocked.Visible = True
                            PBlock.Visible = True
                        End If


                    Next

                    Dim OBJCMN As New ClsCommon
                    dttable = OBJCMN.search(" YARNRECEIVEDMACHINE_UPLOAD.YRECEIVEDMAC_NO AS GRIDSRNO, YARNRECEIVEDMACHINE_UPLOAD.YRECEIVEDMAC_REMARKS AS REMARKS, YARNRECEIVEDMACHINE_UPLOAD.YRECEIVEDMAC_NAME AS NAME, YARNRECEIVEDMACHINE_UPLOAD.YRECEIVEDMAC_PHOTO AS IMGPATH ", "", " YARNRECEIVEDMACHINE_UPLOAD ", " AND YARNRECEIVEDMACHINE_UPLOAD.YRECEIVEDMAC_NO = " & TEMPMACRECNO & " AND YRECEIVEDMAC_YEARID = " & YearId & " ORDER BY YARNRECEIVEDMACHINE_UPLOAD.YRECEIVEDMAC_SRNO")
                    If dttable.Rows.Count > 0 Then
                        For Each DTR As DataRow In dttable.Rows
                            gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                        Next
                    End If

                    total()
                    GRIDYARN.FirstDisplayedScrollingRowIndex = GRIDYARN.RowCount - 1
                Else
                    EDIT = False
                    clear()
                End If
            End If

        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub fillcmb()
        Try
            If CMBOURGODOWN.Text.Trim = "" Then fillGODOWN(CMBOURGODOWN, EDIT, " AND GODOWN_ISOUR = 'True'")
            If CMBMACHINE.Text.Trim = "" Then FILLMACHINE(CMBMACHINE)
            If CMBQUALITY.Text.Trim = "" Then fillQUALITY(CMBQUALITY, EDIT)
            If CMBCOLOR.Text.Trim = "" Then FILLCOLOR(CMBCOLOR)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Dim OBJYISSUE As New YarnReceivedMachineDetails
            OBJYISSUE.MdiParent = MDIMain
            OBJYISSUE.Show()
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

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDYARN.RowCount = 0
                TEMPMACRECNO = Val(tstxtbillno.Text)
                If TEMPMACRECNO > 0 Then
                    EDIT = True
                    YarnReceivedMachine_Load(sender, e)
                Else
                    clear()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub toolprevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Cursor.Current = Cursors.WaitCursor
            GRIDYARN.RowCount = 0
LINE1:
            TEMPMACRECNO = Val(TXTRECNO.Text) - 1
            If TEMPMACRECNO > 0 Then
                EDIT = True
                YarnReceivedMachine_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
            If GRIDYARN.RowCount = 0 And TEMPMACRECNO > 1 Then
                TXTRECNO.Text = TEMPMACRECNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
LINE1:
            TEMPMACRECNO = Val(TXTRECNO.Text) + 1
            getmaxno()
            Dim MAXNO As Integer = TXTRECNO.Text.Trim
            clear()
            If Val(TXTRECNO.Text) - 1 >= TEMPMACRECNO Then
                EDIT = True
                YarnReceivedMachine_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
            If GRIDYARN.RowCount = 0 And TEMPMACRECNO < MAXNO Then
                TXTRECNO.Text = TEMPMACRECNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub GRIDYRECEIVEDMAC_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles GRIDYARN.CellValidating
        Try
            Dim colNum As Integer = GRIDYARN.Columns(e.ColumnIndex).Index
            If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return

            Select Case colNum

                Case GBAGS.Index
                    Dim dDebit As Decimal
                    Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

                    If bValid Then
                        If GRIDYARN.CurrentCell.Value = Nothing Then GRIDYARN.CurrentCell.Value = "0.00"
                        GRIDYARN.CurrentCell.Value = Convert.ToDecimal(GRIDYARN.Item(colNum, e.RowIndex).Value)
                        '' everything is good
                        total()
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

    Private Sub GRIDYRECEIVEDMAC_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDYARN.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDYARN.RowCount > 0 Then
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                If lbllocked.Visible = True Then
                    MessageBox.Show("Do not delete the Line, this will create Stock Problem")
                    Exit Sub
                End If
                GRIDYARN.Rows.RemoveAt(GRIDYARN.CurrentRow.Index)
                getsrno(GRIDYARN)
                total()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then PRINTREPORT()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub


    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Try
            If EDIT = True Then

                Dim TEMPMSG As Integer = MsgBox("Wish to Delete Yarn Receive?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbNo Then Exit Sub

                Dim ALPARAVAL As New ArrayList
                Dim OBJYISSUE As New ClsYarnReceivedMachine

                ALPARAVAL.Add(TEMPMACRECNO)
                ALPARAVAL.Add(YearId)
                ALPARAVAL.Add(CmpId)
                OBJYISSUE.alParaval = ALPARAVAL
                Dim INTRES As Integer = OBJYISSUE.Delete()
                MsgBox("Yarn Received Deleted Succesfully")
                clear()
                EDIT = False
                CMBOURGODOWN.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tooldelete.Click
        Call cmddelete_Click(sender, e)
    End Sub

    Private Sub RECEIVEDDATE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles RECEIVEDDATE.Validating
        Try
            If RECEIVEDDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(RECEIVEDDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Try
            cmdok_Click(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PRINTREPORT()
        Try

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMACHINE_Validated(sender As Object, e As EventArgs) Handles CMBMACHINE.Validated
        Try
            If EDIT = False And CMBMACHINE.Text.Trim <> "" Then
                CMBISSUENO.Items.Clear()
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("*", "", " (SELECT YARNISSUEMACHINE.YISSUEMAC_NO AS ISSUENO FROM YARNISSUEMACHINE INNER JOIN MACHINEMASTER ON YARNISSUEMACHINE.YISSUEMAC_MACHINEID = MACHINEMASTER.MACHINE_ID WHERE ROUND(YISSUEMAC_TOTALWT - YISSUEMAC_RECDWT,2) > 0  AND MACHINEMASTER.MACHINE_NAME = '" & CMBMACHINE.Text.Trim & "' AND YARNISSUEMACHINE.YISSUEMAC_YEARID = " & YearId & " UNION ALL SELECT STOCKMASTER_YARNMACHINE.SMYARNMAC_NO AS ISSUENO FROM STOCKMASTER_YARNMACHINE INNER JOIN MACHINEMASTER ON STOCKMASTER_YARNMACHINE.SMYARNMAC_MACHINEID = MACHINEMASTER.MACHINE_ID WHERE ROUND(SMYARNMAC_WT - SMYARNMAC_RECDWT,2) > 0  AND MACHINEMASTER.MACHINE_NAME = '" & CMBMACHINE.Text.Trim & "' AND STOCKMASTER_YARNMACHINE.SMYARNMAC_YEARID = " & YearId & ") AS T", "")
                If DT.Rows.Count > 0 Then
                    For Each DTROW As DataRow In DT.Rows
                        CMBISSUENO.Items.Add(DTROW("ISSUENO"))
                    Next
                    CMBMACHINE.Enabled = False
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid()

        GRIDYARN.Enabled = True

        If GRIDDOUBLECLICK = False Then
            GRIDYARN.Rows.Add(Val(TXTSRNO.Text.Trim), CMBQUALITY.Text.Trim, TXTLOTNO.Text.Trim, CMBCOLOR.Text.Trim, TXTBAGNO.Text.Trim, TXTBAGS.Text.Trim, Format(Val(TXTGROSSWT.Text.Trim), "0.00"), Format(Val(TXTTAREWT.Text.Trim), "0.00"), Format(Val(TXTNETTWT.Text.Trim), "0.00"), 0, Val(CMBISSUENO.Text.Trim), 0, TXTTYPE.Text.Trim)
            getsrno(GRIDYARN)
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDYARN.Item(gsrno.Index, TEMPROW).Value = Val(TXTSRNO.Text.Trim)
            GRIDYARN.Item(GYARNQUALITY.Index, TEMPROW).Value = CMBQUALITY.Text.Trim
            GRIDYARN.Item(GLOTNO.Index, TEMPROW).Value = TXTLOTNO.Text.Trim
            GRIDYARN.Item(GSHADE.Index, TEMPROW).Value = CMBCOLOR.Text.Trim
            GRIDYARN.Item(GBAGNO.Index, TEMPROW).Value = TXTBAGNO.Text.Trim
            GRIDYARN.Item(GBAGS.Index, TEMPROW).Value = TXTBAGS.Text.Trim
            GRIDYARN.Item(GGROSSWT.Index, TEMPROW).Value = Format(Val(TXTGROSSWT.Text.Trim), "0.00")
            GRIDYARN.Item(GTAREWT.Index, TEMPROW).Value = Format(Val(TXTTAREWT.Text.Trim), "0.00")
            GRIDYARN.Item(GNETTWT.Index, TEMPROW).Value = Format(Val(TXTNETTWT.Text.Trim), "0.00")

            GRIDDOUBLECLICK = False
        End If

        total()

        GRIDYARN.FirstDisplayedScrollingRowIndex = GRIDYARN.RowCount - 1

        TXTSRNO.Text = Val(GRIDYARN.RowCount) + 1
        'BY DEFAULT GET NEXT BAGNO
        If Val(TXTBAGNO.Text.Trim) > 0 Then TXTBAGNO.Text = Val(TXTBAGNO.Text.Trim) + 1
        TXTBAGS.Clear()
        TXTGROSSWT.Clear()
        TXTTAREWT.Clear()
        TXTNETTWT.Clear()
        CMBQUALITY.Focus()

    End Sub

    Private Sub GRIDYARN_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDYARN.CellDoubleClick
        EDITROW()
    End Sub

    Sub EDITROW()
        Try
            If GRIDYARN.CurrentRow.Index >= 0 And GRIDYARN.Item(gsrno.Index, GRIDYARN.CurrentRow.Index).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                TXTSRNO.Text = GRIDYARN.Item(gsrno.Index, GRIDYARN.CurrentRow.Index).Value.ToString
                CMBQUALITY.Text = GRIDYARN.Item(GYARNQUALITY.Index, GRIDYARN.CurrentRow.Index).Value.ToString
                TXTLOTNO.Text = GRIDYARN.Item(GLOTNO.Index, GRIDYARN.CurrentRow.Index).Value.ToString
                CMBCOLOR.Text = GRIDYARN.Item(GSHADE.Index, GRIDYARN.CurrentRow.Index).Value.ToString
                TXTBAGNO.Text = GRIDYARN.Item(GBAGNO.Index, GRIDYARN.CurrentRow.Index).Value.ToString
                TXTBAGS.Text = GRIDYARN.Item(GBAGS.Index, GRIDYARN.CurrentRow.Index).Value.ToString
                TXTGROSSWT.Text = GRIDYARN.Item(GGROSSWT.Index, GRIDYARN.CurrentRow.Index).Value.ToString
                TXTTAREWT.Text = GRIDYARN.Item(GTAREWT.Index, GRIDYARN.CurrentRow.Index).Value.ToString
                TXTNETTWT.Text = GRIDYARN.Item(GNETTWT.Index, GRIDYARN.CurrentRow.Index).Value.ToString

                TEMPROW = GRIDYARN.CurrentRow.Index
                TXTSRNO.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLUPLOAD()

        If GRIDUPLOADDOUBLECLICK = False Then
            gridupload.Rows.Add(Val(txtuploadsrno.Text.Trim), txtuploadremarks.Text.Trim, txtuploadname.Text.Trim, PBSoftCopy.Image)
            getsrno(gridupload)
        ElseIf GRIDUPLOADDOUBLECLICK = True Then

            gridupload.Item(GUSRNO.Index, TEMPUPLOADROW).Value = txtuploadsrno.Text.Trim
            gridupload.Item(GUREMARKS.Index, TEMPUPLOADROW).Value = txtuploadremarks.Text.Trim
            gridupload.Item(GUNAME.Index, TEMPUPLOADROW).Value = txtuploadname.Text.Trim
            gridupload.Item(GUIMGPATH.Index, TEMPUPLOADROW).Value = PBSoftCopy.Image

            GRIDUPLOADDOUBLECLICK = False

        End If
        gridupload.FirstDisplayedScrollingRowIndex = gridupload.RowCount - 1

        txtuploadsrno.Clear()
        txtuploadremarks.Clear()
        txtuploadname.Clear()
        PBSoftCopy.Image = Nothing
        txtimgpath.Clear()

        txtuploadremarks.Focus()
    End Sub

    Private Sub TXTNETTWT_Validated(sender As Object, e As EventArgs) Handles TXTNETTWT.Validated
        Try
            If CMBQUALITY.Text.Trim <> "" And Val(TXTGROSSWT.Text.Trim) > 0 And Val(TXTBAGS.Text.Trim) > 0 And Val(CMBISSUENO.Text.Trim) <> 0 Then fillgrid() Else MsgBox("Enter Proper Details", MsgBoxStyle.Critical)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBQUALITY.Enter
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

    Private Sub CMBQUALITY_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBQUALITY.Validating
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

    Private Sub TXTCONES_Validated(sender As Object, e As EventArgs) Handles TXTBAGS.Validated, TXTGROSSWT.Validated
        CALC()
    End Sub

    Sub CALC()
        Try
            TXTTAREWT.Text = 0.0
            TXTNETTWT.Text = 0.0
            'TXTTAREWT.Text = Format(((0.05 * Val(TXTCONES.Text.Trim)) + 0.18), "0.00")
            TXTNETTWT.Text = Format(Val(TXTGROSSWT.Text.Trim) - Val(TXTTAREWT.Text.Trim), "0.00")
            total()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBISSUENO_KeyDown(sender As Object, e As KeyEventArgs) Handles CMBISSUENO.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJSELECTISSUENO As New SelectIssueNo
                OBJSELECTISSUENO.MACHINENAME = CMBMACHINE.Text.Trim
                OBJSELECTISSUENO.ShowDialog()
                If OBJSELECTISSUENO.TEMPISSUENO <> "" Then CMBISSUENO.Text = OBJSELECTISSUENO.TEMPISSUENO
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBISSUENO_Validating(sender As Object, e As CancelEventArgs) Handles CMBISSUENO.Validating
        Try
            If Val(CMBISSUENO.Text) > 0 Then

                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" SUM(BALANCEWT) AS BALANCEWT, SUM(OUTWT) AS OUTWT, TYPE ", "", " (SELECT (YARNISSUEMACHINE.YISSUEMAC_TOTALWT - YARNISSUEMACHINE.YISSUEMAC_RECDWT) AS BALANCEWT, YARNISSUEMACHINE.YISSUEMAC_TOTALWT AS OUTWT, 'YARNISSUEMACHINE' AS TYPE FROM YARNISSUEMACHINE INNER JOIN MACHINEMASTER ON YARNISSUEMACHINE.YISSUEMAC_MACHINEID = MACHINEMASTER.MACHINE_ID  WHERE MACHINEMASTER.MACHINE_NAME='" & CMBMACHINE.Text.Trim & "' AND ROUND((YARNISSUEMACHINE.YISSUEMAC_TOTALWT - YARNISSUEMACHINE.YISSUEMAC_RECDWT),2) > 0  AND YARNISSUEMACHINE.YISSUEMAC_NO = " & Val(CMBISSUENO.Text.Trim) & " AND YARNISSUEMACHINE.YISSUEMAC_YEARID = " & YearId & " UNION ALL SELECT (STOCKMASTER_YARNMACHINE.SMYARNMAC_WT - STOCKMASTER_YARNMACHINE.SMYARNMAC_RECDWT) AS BALANCEWT, STOCKMASTER_YARNMACHINE.SMYARNMAC_WT AS OUTWT, 'OPENING' AS TYPE FROM STOCKMASTER_YARNMACHINE INNER JOIN MACHINEMASTER ON STOCKMASTER_YARNMACHINE.SMYARNMAC_MACHINEID = MACHINEMASTER.MACHINE_ID  WHERE MACHINEMASTER.MACHINE_NAME='" & CMBMACHINE.Text.Trim & "' AND ROUND((STOCKMASTER_YARNMACHINE.SMYARNMAC_WT - STOCKMASTER_YARNMACHINE.SMYARNMAC_RECDWT),2) > 0  AND STOCKMASTER_YARNMACHINE.SMYARNMAC_NO = " & Val(CMBISSUENO.Text.Trim) & " AND STOCKMASTER_YARNMACHINE.SMYARNMAC_YEARID = " & YearId & ") AS T", " GROUP BY TYPE HAVING ISNULL(SUM(BALANCEWT),0) > 0")
                If DT.Rows.Count > 0 Then
                    If DT.Rows(0).Item("BALANCEWT") > 0 Then

                        CMBISSUENO.Enabled = False
                        TXTTYPE.Text = DT.Rows(0).Item("TYPE")
                        TXTBALWT.Text = Format(Val(DT.Rows(0).Item("BALANCEWT")), "0.00")
                        TXTACTUALISSUEWT.Text = Format(Val(DT.Rows(0).Item("OUTWT")), "0.00")

                    Else
                        MsgBox("Challan Already Cleared", MsgBoxStyle.Critical)
                        e.Cancel = True
                        CMBISSUENO.Text = ""
                        Exit Sub
                    End If
                Else
                    MsgBox("Invalid Challan No !", MsgBoxStyle.Critical)
                    e.Cancel = True
                    CMBISSUENO.Text = ""
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SAVEUPLOAD()
        Try

            Dim OBJDYEING As New ClsYarnRecdFromDyeing
            For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                Dim MS As New IO.MemoryStream
                Dim ALPARAVAL As New ArrayList
                If row.Cells(GUSRNO.Index).Value <> Nothing Then
                    ALPARAVAL.Add(TEMPMACRECNO)
                    ALPARAVAL.Add(row.Cells(GUSRNO.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUREMARKS.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUNAME.Index).Value)

                    PBSoftCopy.Image = row.Cells(GUIMGPATH.Index).Value
                    PBSoftCopy.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
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

    Private Sub gridupload_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridupload.CellDoubleClick
        Try
            If e.RowIndex = -1 Then Exit Sub

            If e.RowIndex >= 0 And gridupload.Item(GUSRNO.Index, e.RowIndex).Value <> Nothing Then

                GRIDUPLOADDOUBLECLICK = True
                txtuploadsrno.Text = gridupload.Item(GUSRNO.Index, e.RowIndex).Value
                txtuploadremarks.Text = gridupload.Item(GUREMARKS.Index, e.RowIndex).Value
                txtuploadname.Text = gridupload.Item(GUNAME.Index, e.RowIndex).Value
                PBSoftCopy.Image = gridupload.Item(GUIMGPATH.Index, e.RowIndex).Value

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
            If txtuploadremarks.Text.Trim <> "" And txtuploadname.Text.Trim <> "" And PBSoftCopy.ImageLocation <> "" Then
                FILLUPLOAD()
            Else
                MsgBox("Enter Proper Details")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTUPLOADSRNO_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtuploadsrno.GotFocus
        If GRIDUPLOADDOUBLECLICK = False Then
            If gridupload.RowCount > 0 Then
                txtuploadsrno.Text = Val(gridupload.Rows(gridupload.RowCount - 1).Cells(0).Value) + 1
            Else
                txtuploadsrno.Text = 1
            End If
        End If
    End Sub

    Private Sub CMDUPLOAD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDUPLOAD.Click
        OpenFileDialog1.Filter = "Pictures (*.bmp;*.jpeg;*.png)|*.bmp;*.jpg;*.png"
        OpenFileDialog1.ShowDialog()
        txtimgpath.Text = OpenFileDialog1.FileName
        On Error Resume Next
        If txtimgpath.Text.Trim.Length <> 0 Then PBSoftCopy.ImageLocation = txtimgpath.Text.Trim
    End Sub

    Private Sub CMDREMOVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDREMOVE.Click
        Try
            PBSoftCopy.Image = Nothing
            txtimgpath.Clear()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDVIEW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDVIEW.Click
        Try
            If gridupload.SelectedRows.Count > 0 Then
                Dim objVIEW As New ViewImage
                objVIEW.pbsoftcopy.Image = PBSoftCopy.Image
                objVIEW.ShowDialog()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridupload_RowEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridupload.RowEnter
        Try
            If e.RowIndex >= 0 Then PBSoftCopy.Image = gridupload.Rows(e.RowIndex).Cells(GUIMGPATH.Index).Value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCONES_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTBAGS.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTGROSSWT_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTGROSSWT.KeyPress, TXTTAREWT.KeyPress, TXTNETTWT.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

End Class