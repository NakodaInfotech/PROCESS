
Imports BL
Imports System.Windows.Forms
Imports System.IO

Public Class MaterialDespatch

    Dim IntResult As Integer
    Dim GRIDUPLOADDOUBLECLICK As Boolean
    Public edit As Boolean          'used for editing
    Public TEMPMATDESNO As Integer     'used for poation no while editing
    Dim TEMPUPLOADROW As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub clear()

        EP.Clear()
        TXTNAME.Clear()
        TXTMILLNAME.Clear()
        cmbtrans.Text = ""
        txtpono.Clear()
        DESPATCHDATE.Text = Mydate
        txtremarks.Clear()
        tstxtbillno.Clear()

        lbllocked.Visible = False
        PBlock.Visible = False

        GRIDMATDES.RowCount = 0

        PBSOFTCOPY.Image = Nothing
        TXTUPLOADSRNO.Text = 1
        txtuploadname.Clear()
        txtuploadremarks.Clear()
        TXTIMGPATH.Clear()
        gridupload.RowCount = 0

        cmdselectPO.Enabled = True
        GRIDUPLOADDOUBLECLICK = False

        getmaxno()

        LBLTOTALBAGS.Text = 0
        LBLTOTALWT.Text = 0

    End Sub

    Sub total()
        Try
            LBLTOTALWT.Text = 0.0
            LBLTOTALBAGS.Text = 0.0
            For Each ROW As DataGridViewRow In GRIDMATDES.Rows
                If ROW.Cells(gsrno.Index).Value <> Nothing Then
                    LBLTOTALBAGS.Text = Format(Val(LBLTOTALBAGS.Text) + Val(ROW.Cells(gBag.Index).EditedFormattedValue), "0")
                    LBLTOTALWT.Text = Format(Val(LBLTOTALWT.Text) + Val(ROW.Cells(Gwt.Index).EditedFormattedValue), "0.00")
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdclear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        clear()
        edit = False
        DESPATCHDATE.Focus()
    End Sub

    Sub getmaxno()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(MATDES_no),0) + 1 ", "MATERIALDESPATCH", " and MATDES_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTMATDESNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Function errorvalid() As Boolean
        Try
            Dim bln As Boolean = True

            If DESPATCHDATE.Text = "__/__/____" Then
                EP.SetError(DESPATCHDATE, " Please Enter Proper Date")
                bln = False
            Else
                If Not datecheck(DESPATCHDATE.Text) Then
                    EP.SetError(DESPATCHDATE, "Date not in Accounting Year")
                    bln = False
                End If
            End If

            If DESPATCHDATE.Text <> "__/__/____" Then
                If Convert.ToDateTime(DESPATCHDATE.Text).Date < podate.Value.Date Then
                    EP.SetError(DESPATCHDATE, " Please Enter Proper Date")
                    bln = False
                End If
            End If

            If cmbtrans.Text.Trim.Length = 0 Then
                EP.SetError(cmbtrans, " Please Fill Transport")
                bln = False
            End If

            If lbllocked.Visible = True Then
                EP.SetError(lbllocked, "Invoice Raised, Delete Invoice First")
                bln = False
            End If

            If GRIDMATDES.RowCount = 0 Then
                EP.SetError(TabControl1, "Fill Item Details")
                bln = False
            End If


            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable
            For Each row As DataGridViewRow In GRIDMATDES.Rows
                If Val(row.Cells(gBag.Index).Value) = 0 Then
                    EP.SetError(TXTNAME, "Bags Cannot be 0")
                    bln = False
                End If

                If Val(row.Cells(Gwt.Index).Value) = 0 Then
                    EP.SetError(TXTNAME, "Wt Cannot be 0")
                    bln = False
                End If

                DT = OBJCMN.search("(PO_BAGS - PO_OUTBAGS) AS ALLOWEDBAGS, (PO_WT - PO_OUTWT) AS ALLOWEDWT", "", " PURCHASEORDER_DESC ", " AND PO_NO = " & row.Cells(GPONO.Index).Value & " AND PO_GRIDSRNO = " & row.Cells(GPOSRNO.Index).Value & " AND PO_YEARID = " & YearId)
                If edit = False Then
                    If Val(row.Cells(gBag.Index).Value) > Val(DT.Rows(0).Item("ALLOWEDBAGS")) Then
                        EP.SetError(TXTNAME, "Bags Greater then Allowed Bags, Maximum " & Val(DT.Rows(0).Item("ALLOWEDBAGS")) & " Bags Allowed")
                        bln = False
                    End If
                    If Val(row.Cells(Gwt.Index).Value) > Val(DT.Rows(0).Item("ALLOWEDWT")) Then
                        EP.SetError(TXTNAME, "Wt Greater then Allowed Wt, Maximum " & Val(DT.Rows(0).Item("ALLOWEDWT")) & " Wt Allowed")
                        bln = False
                    End If
                Else
                    Dim DT1 As DataTable = OBJCMN.search("MATDES_BAGS AS OLDBAGS, MATDES_WT AS OLDWT ", "", " MATERIALDESPATCH_DESC ", " AND MATDES_NO = " & TEMPMATDESNO & " AND MATDES_GRIDPONO = " & row.Cells(GPONO.Index).Value & " AND MATDES_GRIDPOSRNO = " & row.Cells(GPOSRNO.Index).Value & " AND MATDES_YEARID = " & YearId)
                    If Val(row.Cells(gBag.Index).Value) > (Val(DT.Rows(0).Item("ALLOWEDBAGS")) + Val(DT1.Rows(0).Item("OLDBAGS"))) Then
                        EP.SetError(TXTNAME, "Bags Greater then Allowed Bags, Maximum " & (Val(DT.Rows(0).Item("ALLOWEDBAGS")) + Val(DT1.Rows(0).Item("OLDBAGS"))) & " Bags Allowed")
                        bln = False
                    End If
                    If Val(row.Cells(Gwt.Index).Value) > (Val(DT.Rows(0).Item("ALLOWEDWT")) + Val(DT1.Rows(0).Item("OLDWT"))) Then
                        EP.SetError(TXTNAME, "Wt Greater then Allowed Wt, Maximum " & (Val(DT.Rows(0).Item("ALLOWEDWT")) + Val(DT1.Rows(0).Item("OLDWT"))) & " Wt Allowed")
                        bln = False
                    End If
                End If
            Next



            Return bln
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Function

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

    Sub SAVEUPLOAD()
        Try
            Dim OBJPR As New ClsMaterialDespatch
            For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                Dim MS As New IO.MemoryStream
                Dim ALPARAVAL As New ArrayList
                If row.Cells(GUSRNO.Index).Value <> Nothing Then
                    ALPARAVAL.Add(TEMPMATDESNO)
                    ALPARAVAL.Add(row.Cells(GUSRNO.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUREMARKS.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUNAME.Index).Value)

                    PBSOFTCOPY.Image = row.Cells(GUIMGPATH.Index).Value
                    PBSOFTCOPY.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                    ALPARAVAL.Add(MS.ToArray)
                    ALPARAVAL.Add(YearId)

                    OBJPR.alParaval = ALPARAVAL
                    Dim INTRES As Integer = OBJPR.SAVEUPLOAD()
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

        TXTUPLOADSRNO.Text = gridupload.RowCount + 1
        txtuploadremarks.Clear()
        txtuploadname.Clear()
        PBSOFTCOPY.Image = Nothing
        TXTIMGPATH.Clear()

        txtuploadremarks.Focus()

    End Sub

    Private Sub GRIDUPLOAD_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridupload.CellDoubleClick
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

    Private Sub GRIDUPLOAD_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gridupload.KeyDown
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

    Private Sub TXTUPLOADNAME_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtuploadname.Validating
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

    Private Sub cmdok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList
            alParaval.Add(Format(Convert.ToDateTime(DESPATCHDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(TXTNAME.Text.Trim)
            alParaval.Add(TXTMILLNAME.Text.Trim)
            alParaval.Add(txtpono.Text.Trim)
            alParaval.Add(Format(podate.Value.Date, "MM/dd/yyyy"))
            alParaval.Add(cmbtrans.Text.Trim)

            alParaval.Add(Val(LBLTOTALBAGS.Text))
            alParaval.Add(Val(LBLTOTALWT.Text))

            alParaval.Add(txtremarks.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)

            Dim gridsrno As String = ""
            Dim TYPE As String = ""
            Dim QUALITY As String = ""
            Dim COUNT As String = ""
            Dim BAGS As String = ""
            Dim WT As String = ""
            Dim LRNO As String = ""
            Dim NARRATION As String = ""
            Dim DONE As String = ""
            Dim PONO As String = ""
            Dim POSRNO As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDMATDES.Rows
                If row.Cells(0).Value <> Nothing Then
                    If gridsrno = "" Then
                        gridsrno = row.Cells(gsrno.Index).Value.ToString
                        TYPE = row.Cells(GTYPE.Index).Value.ToString
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                        COUNT = Val(row.Cells(gcount.Index).Value)
                        BAGS = Val(row.Cells(gBag.Index).Value)
                        WT = Val(row.Cells(Gwt.Index).Value)
                        If row.Cells(GLRNO.Index).Value <> Nothing Then LRNO = row.Cells(GLRNO.Index).Value.ToString Else LRNO = ""
                        If row.Cells(GNarration.Index).Value <> Nothing Then NARRATION = row.Cells(GNarration.Index).Value.ToString Else NARRATION = ""
                        If row.Cells(GDONE.Index).Value = True Then
                            DONE = 1
                        Else
                            DONE = 0
                        End If
                        PONO = row.Cells(GPONO.Index).Value
                        POSRNO = row.Cells(GPOSRNO.Index).Value
                    Else
                        gridsrno = gridsrno & "|" & row.Cells(gsrno.Index).Value
                        TYPE = TYPE & "|" & row.Cells(GTYPE.Index).Value.ToString
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        COUNT = COUNT & "|" & row.Cells(gcount.Index).Value.ToString
                        BAGS = BAGS & "|" & row.Cells(gBag.Index).Value
                        WT = WT & "|" & row.Cells(Gwt.Index).Value
                        If row.Cells(GLRNO.Index).Value <> Nothing Then LRNO = LRNO & "|" & row.Cells(GLRNO.Index).Value.ToString Else LRNO = LRNO & "|" & ""
                        If row.Cells(GNarration.Index).Value <> Nothing Then NARRATION = NARRATION & "|" & row.Cells(GNarration.Index).Value.ToString Else NARRATION = NARRATION & "|" & ""
                        If row.Cells(GDONE.Index).Value = True Then
                            DONE = DONE & "|" & "1"
                        Else
                            DONE = DONE & "|" & "0"
                        End If
                        PONO = PONO & "|" & row.Cells(GPONO.Index).Value
                        POSRNO = POSRNO & "|" & row.Cells(GPOSRNO.Index).Value
                    End If
                End If
            Next

            alParaval.Add(gridsrno)
            alParaval.Add(TYPE)
            alParaval.Add(QUALITY)
            alParaval.Add(COUNT)
            alParaval.Add(BAGS)
            alParaval.Add(WT)
            alParaval.Add(LRNO)
            alParaval.Add(NARRATION)
            alParaval.Add(DONE)
            alParaval.Add(PONO)
            alParaval.Add(POSRNO)


            Dim OBJMATDES As New ClsMaterialDespatch()
            OBJMATDES.alParaval = alParaval
            If edit = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DTTABLE As DataTable = OBJMATDES.save()
                TEMPMATDESNO = DTTABLE.Rows(0).Item(0)
                MsgBox("Details Added")

            ElseIf edit = True Then
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPMATDESNO)

                IntResult = OBJMATDES.Update()
                MsgBox("Details Updated")
                edit = False

            End If

            If gridupload.RowCount > 0 Then SAVEUPLOAD()

            'clear()
            'SHOW NEXT BILL ON EDIT MODE DONT CLEAR
            Call toolnext_Click(sender, e)
            DESPATCHDATE.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub MaterialDespatch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            If errorvalid() = True Then
                Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                If tempmsg = vbYes Then cmdok_Click(sender, e)
            End If
            Me.Close()
        ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for Delete
            tstxtbillno.Focus()
            tstxtbillno.SelectAll()
        ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.D1 Then       'for Delete
            TabControl1.SelectedIndex = (0)
        ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.D2 Then       'for Delete
            TabControl1.SelectedIndex = (1)
        ElseIf e.KeyCode = Keys.OemQuotes Then
            e.SuppressKeyPress = True
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.KeyCode = Keys.Left And e.Alt = True Then
            Call toolprevious_Click(sender, e)
        ElseIf e.KeyCode = Keys.Right And e.Alt = True Then
            Call toolnext_Click(sender, e)
        ElseIf e.KeyCode = Keys.F5 Then
            GRIDMATDES.Focus()
        End If
    End Sub

    Private Sub MaterialDespatch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'MATERIAL DESPATCH'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor

            fillcmb()
            clear()

            If edit = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim OBJMATDES As New ClsMaterialDespatch()
                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(TEMPMATDESNO)
                ALPARAVAL.Add(YearId)
                OBJMATDES.alParaval = ALPARAVAL
                Dim dttable As DataTable = OBJMATDES.SELECTMATDES()

                If dttable.Rows.Count > 0 Then

                    For Each dr As DataRow In dttable.Rows

                        TXTMATDESNO.Text = TEMPMATDESNO
                        DESPATCHDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                        TXTNAME.Text = Convert.ToString(dr("NAME").ToString)
                        TXTMILLNAME.Text = Convert.ToString(dr("MILLNAME").ToString)
                        txtpono.Text = Convert.ToString(dr("PONO").ToString)
                        podate.Value = Format(Convert.ToDateTime(dr("PODATE")).Date, "dd/MM/yyyy")
                        cmbtrans.Text = dr("TRANSNAME").ToString
                        txtremarks.Text = Convert.ToString(dr("remarks").ToString)

                        'Item Grid
                        GRIDMATDES.Rows.Add(Val(dr("SRNO")), dr("TYPE").ToString, dr("QUALITY").ToString, Val(dr("COUNT")), Val(dr("BAGS")), Val(dr("WT")), dr("LRNO"), dr("NARRATION").ToString, dr("DONE"), dr("GRIDPONO"), dr("POSRNO"))
                        If Convert.ToBoolean(dr("DONE")) = True Then
                            lbllocked.Visible = True
                            PBlock.Visible = True
                        End If

                    Next
                    total()
                Else
                    edit = False
                    clear()
                End If

                'Dim OBJCMN As New ClsCommon
                'dttable = OBJCMN.search(" MATDES_GRIDSRNO AS GRIDSRNO, MATDES_REMARKS AS REMARKS, MATDES_NAME AS NAME, MATDES_IMGPATH AS IMGPATH, MATDES_NEWIMGPATH AS NEWIMGPATH", "", " MATERIALDESPATCH_UPLOAD", " AND MATDES_NO = " & TEMPMATDESNO & " AND MATDES_YEARID = " & YearId)
                'If dttable.Rows.Count > 0 Then
                '    For Each DTR As DataRow In dttable.Rows
                '        gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), DTR("IMGPATH"), DTR("NEWIMGPATH"))
                '    Next
                'End If

            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try

    End Sub

    Sub fillcmb()
        Try
            fillname(cmbtrans, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'TRANSPORT'")
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

            Dim OBJMATDES As New MaterialDespatchDetails
            OBJMATDES.MdiParent = MDIMain
            OBJMATDES.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbtrans.Enter
        Try
            If cmbtrans.Text.Trim = "" Then fillname(cmbtrans, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbtrans.Validating
        Try
            If cmbtrans.Text.Trim <> "" Then namevalidate(cmbtrans, CMBCODE, e, Me, txtadd, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'", "Sundry Creditors", "TRANSPORT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
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

    Private Sub cmdselectpo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdselectPO.Click
        Try

            If (edit = True And USEREDIT = False And USERVIEW = False) Or (edit = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If


            Dim OBJSELECTPO As New SelectPO
            If edit = True Then OBJSELECTPO.PONO = Val(txtpono.Text.Trim)
            Dim DTPO As DataTable = OBJSELECTPO.DT
            OBJSELECTPO.ShowDialog()

            If DTPO.Rows.Count > 0 Then
                GRIDMATDES.RowCount = 0

                Dim OBJCMN As New ClsCommon
                Dim DT As New DataTable

                For Each DTPOROW As DataRow In DTPO.Rows
                    DT = OBJCMN.search(" ISNULL(LEDGERS.Acc_cmpname,'') AS NAME, MILLLEDGERS.Acc_cmpname AS MILLNAME, PURCHASEORDER.PO_NO AS PONO, PURCHASEORDER.PO_DATE AS PODATE, TYPEMASTER.TYPE_name AS TYPE, QUALITYMASTER.QUALITY_NAME AS QUALITY, PURCHASEORDER_DESC.PO_count AS COUNT, PURCHASEORDER_DESC.PO_BAGS - PURCHASEORDER_DESC.PO_OUTBAGS AS BAGS, PURCHASEORDER_DESC.PO_WT - PURCHASEORDER_DESC.PO_OUTWT AS WT, PURCHASEORDER_DESC.PO_GRIDSRNO AS GRIDSRNO, ISNULL(TRANS1.Acc_cmpname, '') AS TRANS1", "", "  PURCHASEORDER INNER JOIN PURCHASEORDER_DESC ON PURCHASEORDER.PO_NO = PURCHASEORDER_DESC.PO_NO AND  PURCHASEORDER.PO_YEARID = PURCHASEORDER_DESC.PO_YEARID INNER JOIN TYPEMASTER ON PURCHASEORDER_DESC.PO_TYPEID = TYPEMASTER.TYPE_id INNER JOIN QUALITYMASTER ON PURCHASEORDER_DESC.PO_QUALITYID = QUALITYMASTER.QUALITY_ID LEFT OUTER JOIN LEDGERS ON PURCHASEORDER.PO_LEDGERID = LEDGERS.Acc_id INNER JOIN LEDGERS AS MILLLEDGERS ON PURCHASEORDER.PO_MILLID = MILLLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS TRANS1 ON PURCHASEORDER.PO_YEARID = TRANS1.Acc_yearid AND PURCHASEORDER.PO_TRANS1ID = TRANS1.Acc_id", " AND PURCHASEORDER.PO_NO = " & DTPOROW(0) & " AND PURCHASEORDER_DESC.PO_GRIDSRNO = " & DTPOROW(1) & " AND PURCHASEORDER.PO_YEARID = " & YearId)
                    For Each DTROW As DataRow In DT.Rows

                        TXTNAME.Text = DTROW("NAME")
                        TXTMILLNAME.Text = DTROW("MILLNAME")
                        txtpono.Text = DTROW("PONO")
                        podate.Value = DTROW("PODATE")
                        cmbtrans.Text = DTROW("TRANS1")

                        GRIDMATDES.Rows.Add(0, DTROW("TYPE"), DTROW("QUALITY"), Val(DTROW("COUNT")), Format(Val(DTROW("BAGS")), "0"), Format(Val(DTROW("WT")), "0.00"), "", "", 0, DTROW("PONO"), DTROW("GRIDSRNO"))
                    Next
                Next
                GRIDMATDES.FirstDisplayedScrollingRowIndex = GRIDMATDES.RowCount - 1
                total()
                getsrno(GRIDMATDES)
                GRIDMATDES.Rows(0).Cells(gBag.Index).Selected = True
                GRIDMATDES.Focus()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDMATDES.RowCount = 0
                TEMPMATDESNO = Val(tstxtbillno.Text)
                If TEMPMATDESNO > 0 Then
                    edit = True
                    MaterialDespatch_Load(sender, e)
                Else
                    clear()
                    edit = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            GRIDMATDES.RowCount = 0
LINE1:
            TEMPMATDESNO = Val(TXTMATDESNO.Text) - 1
Line2:
            If TEMPMATDESNO > 0 Then

                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" MATDES_NO ", "", "  MATERIALDESPATCH ", " AND MATDES_NO = '" & TEMPMATDESNO & "' AND MATERIALDESPATCH.MATDES_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    edit = True
                    MaterialDespatch_Load(sender, e)
                Else
                    TEMPMATDESNO = Val(TEMPMATDESNO - 1)
                    GoTo Line2
                End If
            Else
                clear()
                edit = False
            End If

            If GRIDMATDES.RowCount = 0 And TEMPMATDESNO > 1 Then
                TXTMATDESNO.Text = TEMPMATDESNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            GRIDMATDES.RowCount = 0
LINE1:
            TEMPMATDESNO = Val(TXTMATDESNO.Text) + 1
            getmaxno()
            Dim MAXNO As Integer = TXTMATDESNO.Text.Trim
            clear()
            If Val(TXTMATDESNO.Text) - 1 >= TEMPMATDESNO Then
                edit = True
                MaterialDespatch_Load(sender, e)
            Else
                clear()
                edit = False
            End If
            If GRIDMATDES.RowCount = 0 And TEMPMATDESNO < MAXNO Then
                TXTMATDESNO.Text = TEMPMATDESNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tooldelete.Click
        Try
            Call cmddelete_Click(sender, e)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Dim IntResult As Integer
        Try

            If edit = True Then
                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                If lbllocked.Visible = True Then
                    MsgBox("Unable to Delete, Checking Done / Item Used", MsgBoxStyle.Critical)
                    Exit Sub
                End If
                If MsgBox("Delete Material Despatch?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(TXTMATDESNO.Text.Trim)
                    alParaval.Add(YearId)

                    Dim CLSMATDES As New ClsMaterialDespatch()
                    CLSMATDES.alParaval = alParaval
                    IntResult = CLSMATDES.Delete()
                    MsgBox("Material Despatch Deleted")
                    clear()
                    edit = False
                End If
            Else
                MsgBox("Delete is only in Edit Mode")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub gridMATDESC_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles GRIDMATDES.CellValidating
        Try
            Dim colNum As Integer = GRIDMATDES.Columns(e.ColumnIndex).Index
            If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return

            Select Case colNum

                Case gBag.Index, Gwt.Index, gcount.Index
                    Dim dDebit As Decimal
                    Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

                    If bValid Then
                        If GRIDMATDES.CurrentCell.Value = Nothing Then GRIDMATDES.CurrentCell.Value = "0.00"
                        GRIDMATDES.CurrentCell.Value = Convert.ToDecimal(GRIDMATDES.Item(colNum, e.RowIndex).Value)
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

    Private Sub gridMATDESC_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDMATDES.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDMATDES.RowCount > 0 Then
                GRIDMATDES.Rows.RemoveAt(GRIDMATDES.CurrentRow.Index)
                getsrno(GRIDMATDES)
                total()
            ElseIf e.KeyCode = Keys.F12 And GRIDMATDES.RowCount > 0 Then
                'If GRIDMATDES.CurrentRow.Cells(GQUALITY.Index).Value <> "" Then GRIDMATDES.Rows.Add(CloneWithValues(GRIDMATDES.CurrentRow))
                'If GRIDMATDES.CurrentRow.Index > 0 Then If e.KeyCode = Keys.F12 Then If GRIDMATDES.Rows(GRIDMATDES.CurrentRow.Index - 1).Cells(GLRNO.Index).Value <> "" Then GRIDMATDES.Rows(GRIDMATDES.CurrentRow.Index).Cells(GLRNO.Index).Value = GRIDGDN.Rows(GRIDGDN.CurrentRow.Index - 1).Cells(GLRNO.Index).Value
                If GRIDMATDES.CurrentRow.Cells(GLRNO.Index).Value.ToString <> "" Then GRIDMATDES.Rows.Add(CloneWithValues(GRIDMATDES.CurrentRow))
                If GRIDMATDES.Rows(GRIDMATDES.RowCount - 1).Cells(GLRNO.Index).Value <> Nothing Then GRIDMATDES.Rows(GRIDMATDES.RowCount - 1).Cells(GLRNO.Index).Value = Val(GRIDMATDES.Rows(GRIDMATDES.RowCount - 2).Cells(GLRNO.Index).Value) + 1
                GRIDMATDES.FirstDisplayedScrollingRowIndex = GRIDMATDES.RowCount - 1
                GRIDMATDES.Rows(GRIDMATDES.RowCount - 1).Selected = True
                getsrno(GRIDMATDES)
                total()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Public Function CloneWithValues(ByVal row As DataGridViewRow) As DataGridViewRow
        CloneWithValues = CType(row.Clone(), DataGridViewRow)
        For index As Int32 = 0 To row.Cells.Count - 1
            CloneWithValues.Cells(index).Value = row.Cells(index).Value
        Next
    End Function

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

    Private Sub txtremarks_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtremarks.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJREMARKS As New SelectRemarks
                OBJREMARKS.FRMSTRING = "NARRATION"
                OBJREMARKS.ShowDialog()
                If OBJREMARKS.TEMPNAME <> "" Then txtremarks.Text = OBJREMARKS.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DESPATCHDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DESPATCHDATE.GotFocus
        DESPATCHDATE.SelectAll()
    End Sub

    Private Sub DESPATCHDATE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DESPATCHDATE.Validating
        Try
            If DESPATCHDATE.Text <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(DESPATCHDATE.Text, TEMP) Then
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