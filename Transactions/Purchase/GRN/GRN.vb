
Imports BL
Imports System.ComponentModel

Public Class GRN

    Dim IntResult As Integer
    Dim GRIDDOUBLECLICK As Boolean
    Dim GRIDUPLOADDOUBLECLICK As Boolean
    Public edit As Boolean          'used for editing
    Public TEMPGRNNO As Integer     'used for poation no while editing
    Dim TEMPROW, TEMPUPLOADROW As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim TEMPMSG As Integer
    Dim TEMPTRANS, TEMPLRNO As String
    Dim TEMPFORM As String
    Dim TEMPBILLNO As String

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub clear()

        CMBTYPE.Enabled = True
        CMBTYPE.SelectedIndex = 0

        EP.Clear()
        cmbname.Enabled = True
        cmbname.Text = ""
        TXTBILLNO.Clear()
        CMBBROKER.Text = ""
        cmbGodown.Text = ""
        cmbtrans.Text = ""
        CMBINVNAME.Text = ""

        txtchallan.Enabled = True
        CHALLANDATE.Enabled = True

        CHALLANDATE.Text = Mydate
        txtchallan.Clear()
        CMBMILL.Text = ""
        txtpono.Clear()
        TXTPOSRNO.Clear()
        TXTPOTYPE.Clear()
        podate.Value = Mydate
        CMBTYPE.Text = ""
        BTNLRNO.Text = "LR No."

        txtadd.Clear()
        GRNDATE.Text = Mydate
        CHKPAID.CheckState = CheckState.Unchecked
        CHKPENDINGREPORTS.CheckState = CheckState.Checked
        If ClientName = "MASHOK" Then txtremarks.Text = "DO NO -" & vbCrLf & "DO DATE -" Else txtremarks.Clear()
        tstxtbillno.Clear()
        txtuploadsrno.Clear()
        txtuploadname.Clear()
        txtuploadremarks.Clear()
        gridupload.RowCount = 0
        txtimgpath.Clear()
        TXTNEWIMGPATH.Clear()
        TXTFILENAME.Clear()
        PBSoftCopy.ImageLocation = ""

        For I As Integer = 0 To CHKFORMBOX.Items.Count - 1
            Dim DTR As DataRowView = CHKFORMBOX.Items(I)
            CHKFORMBOX.SetItemCheckState(I, CheckState.Unchecked)
        Next

        lbllocked.Visible = False
        PBlock.Visible = False

        'clearing itemgrid textboxes and combos
        txtsrno.Clear()
        CMBQUALITY.Text = ""
        txtcount.Clear()
        txtbags.Clear()
        TXTWT.Clear()
        TXTGRIDLOTNO.Clear()
        CMBSHADE.Text = ""
        TXTCONES.Clear()
        txtlrno.Clear()
        LRDATE.Text = Mydate
        TXTNARR.Clear()

        GRIDGRN.RowCount = 0
        gridupload.RowCount = 0


        CMBPROCESSOR.Text = ""
        TXTLOTNO.Clear()
        CHECKDATE.Value = Mydate
        TXTREJPCS.Clear()
        TXTREJMTRS.Clear()
        TXTOPSHORTMTRS.Clear()
        TXTSHORTPCS.Clear()
        TXTSHORTMTRS.Clear()
        TXTACCEPTPCS.Clear()
        TXTACCEPTMTRS.Clear()
        GPGREY.Visible = False

        cmdselectPO.Enabled = True
        podate.Enabled = True
        GRIDDOUBLECLICK = False
        GRIDUPLOADDOUBLECLICK = False

        getmaxno()

        LBLTOTALBAGS.Text = 0
        LBLTOTALWT.Text = 0
        LBLTOTALCONES.Text = 0

        txtsrno.Text = 1
        txtuploadsrno.Text = 1

    End Sub

    Sub total()
        Try
            LBLTOTALWT.Text = 0.0
            LBLTOTALBAGS.Text = 0.0
            LBLTOTALCONES.Text = 0.0
            For Each ROW As DataGridViewRow In GRIDGRN.Rows
                If ROW.Cells(gsrno.Index).Value <> Nothing Then
                    LBLTOTALBAGS.Text = Format(Val(LBLTOTALBAGS.Text) + Val(ROW.Cells(gBag.Index).EditedFormattedValue), "0")
                    LBLTOTALWT.Text = Format(Val(LBLTOTALWT.Text) + Val(ROW.Cells(Gwt.Index).EditedFormattedValue), "0.00")
                    LBLTOTALCONES.Text = Format(Val(LBLTOTALCONES.Text) + Val(ROW.Cells(GCONE.Index).EditedFormattedValue), "0")
                End If
            Next
            CALCACCEPTMTRS()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdclear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        clear()
        edit = False
        CMBTYPE.Focus()


    End Sub

    Sub getmaxno()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(grn_no),0) + 1 ", "GRN", " and grn_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then txtgrnno.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub txtuploadsrno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtuploadsrno.KeyPress
        enterkeypress(e, Me)
    End Sub

    Function errorvalid() As Boolean
        Try
            Dim bln As Boolean = True

            If GRNDATE.Text = "__/__/____" Then
                EP.SetError(GRNDATE, " Please Enter Proper Date")
                bln = False
            Else
                If Not datecheck(GRNDATE.Text) Then
                    EP.SetError(GRNDATE, "Date not in Accounting Year")
                    bln = False
                End If
            End If

            If Val(txtpono.Text.Trim) > 0 Then
                If GRNDATE.Text.Trim <> "__/__/____" Then
                    If Convert.ToDateTime(GRNDATE.Text).Date < podate.Value.Date Then
                        EP.SetError(GRNDATE, " Please Enter Proper PO Date")
                        bln = False
                    End If
                End If
            End If


            If cmbname.Text.Trim.Length = 0 Then
                EP.SetError(cmbname, " Please Fill Company Name ")
                bln = False
            End If

            If cmbGodown.Text.Trim.Length = 0 Then
                EP.SetError(cmbGodown, " Please Select Godown")
                bln = False
            End If

            If CMBTYPE.Text.Trim <> "FINISHED" And CMBTYPE.Text.Trim <> "GREY" Then
                If CMBMILL.Text.Trim.Length = 0 Then
                    EP.SetError(CMBMILL, " Please Fill Mill Name")
                    bln = False
                End If
            End If

            'If ClientName <> "JASHOK" Then
            '    If cmbtrans.Text.Trim.Length = 0 Then
            '        EP.SetError(cmbtrans, " Please Fill Transport")
            '        bln = False
            '    End If
            'End If

            If CMBTYPE.Text.Trim = "" Then
                EP.SetError(CMBTYPE, "Select Type")
                bln = False
            End If


            'DONE TEMPORARILY
            'If lbllocked.Visible = True Then
            '    EP.SetError(lbllocked, "Invoice Raised, Delete Invoice First")
            '    bln = False
            'End If

            If GRIDGRN.RowCount = 0 Then
                EP.SetError(TabControl1, "Fill Item Details")
                bln = False
            End If

            'Dim FORMTYPE As String = ""
            'For Each DTROW As DataRowView In CHKFORMBOX.CheckedItems
            '    FORMTYPE = DTROW.Item(0)
            'Next

            'If FORMTYPE = Nothing Then
            '    EP.SetError(CHKFORMBOX, "Pls Select Form Type")
            '    bln = False
            'End If

            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable

            'If ClientName = "MSWEAVING" Then
            '    DT = OBJCMN.search("CITY_NAME AS CITYNAME ", "", " LEDGERS INNER JOIN CITYMASTER ON CITY_ID = ACC_CITYID ", " AND ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND CITY_NAME <> 'Mumbai'")
            '    If DT.Rows.Count > 0 Then
            '        If txtchallan.Text.Trim.Length = 0 Then
            '            EP.SetError(txtchallan, " Please Fill God Rec No")
            '            bln = False
            '        End If
            '    End If
            'End If


            For Each row As DataGridViewRow In GRIDGRN.Rows
                If cmbtrans.Text <> "" And edit = False And row.Cells(GLRNO.Index).Value.ToString <> "" Then
                    Dim dttable As DataTable = OBJCMN.search(" ISNULL(GRN_DESC.GRN_LRNO,'') AS LRNO ", "", " GRN INNER JOIN GRN_DESC ON GRN.grn_no = GRN_DESC.GRN_NO AND GRN.grn_cmpid = GRN_DESC.GRN_CMPID AND GRN.grn_locationid = GRN_DESC.GRN_LOCATIONID AND GRN.grn_yearid = GRN_DESC.GRN_YEARID LEFT OUTER JOIN LEDGERS ON GRN.grn_yearid = LEDGERS.Acc_yearid AND GRN.grn_transledgerid = LEDGERS.Acc_id ", " AND GRN_LRNO = '" & row.Cells(GLRNO.Index).Value & "' AND ISNULL(LEDGERS.ACC_CMPNAME,'') = '" & cmbtrans.Text.Trim & "' AND GRN_DESC.GRN_YEARID = " & YearId)
                    If dttable.Rows.Count > 0 Then
                        EP.SetError(cmbtrans, "LR No Already Exists!")
                        bln = False
                    End If
                End If

                If Val(row.Cells(Gwt.Index).Value) = 0 Or Val(row.Cells(gBag.Index).Value) = 0 Or row.Cells(GLRNO.Index).Value.ToString = "" Then
                    EP.SetError(TabControl1, "Bags / Wt / LR No Cannot be kept Blank")
                    bln = False
                End If

                'If ClientName <> "JASHOK" Then
                '    If Val(row.Cells(GCONE.Index).Value) = 0 Then
                '        EP.SetError(TabControl1, "Cones Cannot be kept Blank")
                '        bln = False
                '    End If
                'End If

                If GRNDATE.Text.Trim <> "__/__/____" And CMBTYPE.Text.Trim <> "FINISHED" Then
                    If Convert.ToDateTime(GRNDATE.Text).Date < Convert.ToDateTime(row.Cells(GLRDATE.Index).Value).Date Then
                        EP.SetError(GRNDATE, " Please Enter Proper LR Date")
                        bln = False
                    End If
                End If

                If Val(row.Cells(GPONO.Index).Value) > 0 And Val(row.Cells(GPOSRNO.Index).Value) > 0 Then
                    'If ClientName = "JASHOK" Then
                    '    'DT = OBJCMN.search("(PO_BAGS-PO_OUTBAGS) AS ALLOWEDBAGS, (PO_WT-PO_OUTWT) AS ALLOWEDWT", "", " PURCHASEORDER_DESC ", " AND PO_NO = " & row.Cells(GPONO.Index).Value & " AND PO_GRIDSRNO = " & row.Cells(GPOSRNO.Index).Value & " AND PO_YEARID = " & YearId)
                    '    DT = OBJCMN.search("*", "", "(SELECT (OPPO_BAGS-OPPO_OUTBAGS) AS ALLOWEDBAGS, (OPPO_WT-OPPO_OUTWT) AS ALLOWEDWT FROM OPENINGPURCHASEORDER_DESC WHERE OPPO_NO = " & row.Cells(GPONO.Index).Value & " AND OPPO_GRIDSRNO = " & row.Cells(GPOSRNO.Index).Value & " AND '" & row.Cells(GTYPE.Index).Value & "' = 'OPENING' AND OPPO_YEARID = " & YearId & " UNION ALL SELECT (PO_BAGS-PO_OUTBAGS) AS ALLOWEDBAGS, (PO_WT-PO_OUTWT) AS ALLOWEDWT FROM PURCHASEORDER_DESC WHERE PO_NO = " & row.Cells(GPONO.Index).Value & " AND PO_GRIDSRNO = " & row.Cells(GPOSRNO.Index).Value & " AND '" & row.Cells(GTYPE.Index).Value & "' = 'PO' " & " AND PO_YEARID = " & YearId & ") AS T", "")
                    'Else
                    '    DT = OBJCMN.search("(MATDES_BAGS) AS ALLOWEDBAGS, (MATDES_WT) AS ALLOWEDWT", "", " MATERIALDESPATCH_DESC ", " AND MATDES_NO = " & row.Cells(GPONO.Index).Value & " AND MATDES_SRNO = " & row.Cells(GPOSRNO.Index).Value & " AND MATDES_YEARID = " & YearId)
                    'End If

                    DT = OBJCMN.search("*", "", "(SELECT (OPPO_BAGS-OPPO_OUTBAGS) AS ALLOWEDBAGS, (OPPO_WT-OPPO_OUTWT) AS ALLOWEDWT FROM OPENINGPURCHASEORDER_DESC WHERE OPPO_NO = " & row.Cells(GPONO.Index).Value & " AND OPPO_GRIDSRNO = " & row.Cells(GPOSRNO.Index).Value & " AND '" & row.Cells(GTYPE.Index).Value & "' = 'OPENING' AND OPPO_YEARID = " & YearId & " UNION ALL SELECT (PO_BAGS-PO_OUTBAGS) AS ALLOWEDBAGS, (PO_WT-PO_OUTWT) AS ALLOWEDWT FROM PURCHASEORDER_DESC WHERE PO_NO = " & row.Cells(GPONO.Index).Value & " AND PO_GRIDSRNO = " & row.Cells(GPOSRNO.Index).Value & " AND '" & row.Cells(GTYPE.Index).Value & "' = 'PO' " & " AND PO_YEARID = " & YearId & ") AS T", "")

                    If edit = False Then
                        If Val(row.Cells(gBag.Index).Value) > Val(DT.Rows(0).Item("ALLOWEDBAGS")) Then
                            EP.SetError(cmbname, "Bags not matched with Allowed Bags, Maximum " & Val(DT.Rows(0).Item("ALLOWEDBAGS")) & " Bags Allowed")
                            bln = False
                        End If
                        If Val(row.Cells(Gwt.Index).Value) > Val(DT.Rows(0).Item("ALLOWEDWT")) Then
                            EP.SetError(cmbname, "Wt not matched with Allowed Wt, Maximum " & Val(DT.Rows(0).Item("ALLOWEDWT")) & " Wt Allowed")
                            bln = False
                        End If
                    Else
                        Dim DT1 As DataTable = OBJCMN.search("ISNULL(GRN_BAGS,0) AS OLDBAGS, GRN_WT AS OLDWT ", "", " GRN_DESC ", " AND GRN_NO = " & TEMPGRNNO & " AND GRN_PONO = " & row.Cells(GPONO.Index).Value & " AND GRN_POSRNO = " & row.Cells(GPOSRNO.Index).Value & " AND GRN_YEARID = " & YearId)
                        Dim TEMPOLDBAGS, TEMPOLDWT As Double
                        If DT1.Rows.Count > 0 Then
                            TEMPOLDBAGS = Val(DT1.Rows(0).Item("OLDBAGS"))
                            TEMPOLDWT = Val(DT1.Rows(0).Item("OLDWT"))
                        End If
                        If Val(row.Cells(gBag.Index).Value) > (Val(DT.Rows(0).Item("ALLOWEDBAGS")) + Val(TEMPOLDBAGS)) Then
                            EP.SetError(cmbname, "Bags Greater then Allowed Bags, Maximum " & (Val(DT.Rows(0).Item("ALLOWEDBAGS")) + Val(DT1.Rows(0).Item("OLDBAGS"))) & " Bags Allowed")
                            bln = False
                        End If
                        If Val(row.Cells(Gwt.Index).Value) > (Val(DT.Rows(0).Item("ALLOWEDWT")) + Val(TEMPOLDWT)) Then
                            EP.SetError(cmbname, "Wt Greater then Allowed Wt, Maximum " & (Val(DT.Rows(0).Item("ALLOWEDWT")) + Val(DT1.Rows(0).Item("OLDWT"))) & " Wt Allowed")
                            bln = False
                        End If
                    End If
                End If
            Next

            If (cmbtrans.Text <> "" And txtchallan.Text <> "" And edit = False) Or (cmbtrans.Text <> TEMPTRANS And txtchallan.Text <> "" And edit = True) Then
                ' Dim OBJCMN As New ClsCommon
                Dim dttable As DataTable = OBJCMN.search(" ISNULL(GRN.grn_challanno, '') AS RECNO ", "", " GRN LEFT OUTER JOIN LEDGERS ON GRN.grn_yearid = LEDGERS.Acc_yearid AND GRN.grn_transledgerid = LEDGERS.Acc_id ", " AND LEDGERS.ACC_CMPNAME = '" & cmbtrans.Text.Trim & "' AND GRN.GRN_CHALLANNO = '" & txtchallan.Text.Trim & "' AND GRN.GRN_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    EP.SetError(txtchallan, "Godown Rec No already Present !")
                    bln = False
                End If
            End If

            If CMBTYPE.Text = "GREY" Then
                If Not datecheck(CHECKDATE.Value) Then
                    EP.SetError(CHECKDATE, "Date Not in Accounting Year")
                    bln = False
                End If
            End If

            Return bln
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Function

    Private Sub cmdok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            Cursor.Current = Cursors.WaitCursor
            EP.Clear()
            total()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList
            alParaval.Add(Format(Convert.ToDateTime(GRNDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBTYPE.Text.Trim)
            alParaval.Add(cmbname.Text.Trim)
            alParaval.Add(TXTBILLNO.Text.Trim)
            alParaval.Add(Val(txtpono.Text.Trim))
            alParaval.Add(Format(podate.Value.Date, "MM/dd/yyyy"))
            alParaval.Add(TXTPOTYPE.Text.Trim)
            alParaval.Add(txtchallan.Text.Trim)
            alParaval.Add(CHALLANDATE.Text)

            alParaval.Add(cmbGodown.Text.Trim)
            alParaval.Add(CMBBROKER.Text.Trim)
            alParaval.Add(cmbtrans.Text.Trim)
            alParaval.Add(CMBMILL.Text.Trim)
            alParaval.Add(CMBINVNAME.Text.Trim)
            alParaval.Add(Val(LBLTOTALBAGS.Text))
            alParaval.Add(Val(LBLTOTALWT.Text))
            alParaval.Add(Val(LBLTOTALCONES.Text))


            alParaval.Add(CMBPROCESSOR.Text.Trim)
            alParaval.Add(Val(TXTLOTNO.Text.Trim))
            alParaval.Add(Format(CHECKDATE.Value.Date, "MM/dd/yyyy"))
            alParaval.Add(Val(TXTREJPCS.Text))
            alParaval.Add(Val(TXTREJMTRS.Text))
            alParaval.Add(Val(TXTOPSHORTMTRS.Text))
            alParaval.Add(Val(TXTSHORTPCS.Text))
            alParaval.Add(Val(TXTSHORTMTRS.Text))
            alParaval.Add(Val(TXTACCEPTPCS.Text))
            alParaval.Add(Val(TXTACCEPTMTRS.Text))

            alParaval.Add(txtremarks.Text.Trim)

            If CHKPAID.Checked = True Then
                alParaval.Add(1)
            Else
                alParaval.Add(0)
            End If
            If CHKPENDINGREPORTS.Checked = True Then
                alParaval.Add(1)
            Else

                alParaval.Add(0)
            End If
            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)


            'ADDING FORMTYPE
            Dim FORMTYPE As String = ""
            For Each DTROW As DataRowView In CHKFORMBOX.CheckedItems
                If FORMTYPE = "" Then
                    FORMTYPE = DTROW.Item(0)
                Else
                    FORMTYPE = FORMTYPE & "|" & DTROW.Item(0)
                End If
            Next
            alParaval.Add(FORMTYPE)

            Dim gridsrno As String = ""
            Dim QUALITY As String = ""
            Dim COUNT As String = ""
            Dim BAGS As String = ""
            Dim WT As String = ""
            Dim GRIDLOTNO As String = ""
            Dim SHADE As String = ""
            Dim CONES As String = ""
            Dim LRNO As String = ""
            Dim GRIDLRDATE As String = ""
            Dim NARRATION As String = ""
            Dim DONE As String = ""
            Dim OUTBAGS As String = ""
            Dim OUTWT As String = ""
            Dim PONO As String = ""
            Dim POSRNO As String = ""
            Dim GRIDTYPE As String = ""
            Dim PURBAGS As String = ""
            Dim PURWT As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDGRN.Rows
                If row.Cells(0).Value <> Nothing Then
                    If gridsrno = "" Then
                        gridsrno = row.Cells(gsrno.Index).Value.ToString
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                        COUNT = Val(row.Cells(gcount.Index).Value)
                        BAGS = Val(row.Cells(gBag.Index).Value)
                        WT = Val(row.Cells(Gwt.Index).Value)
                        GRIDLOTNO = row.Cells(GLOTNO.Index).Value
                        SHADE = row.Cells(GSHADE.Index).Value.ToString
                        CONES = Val(row.Cells(GCONE.Index).Value)
                        If row.Cells(GLRNO.Index).Value <> Nothing Then LRNO = row.Cells(GLRNO.Index).Value.ToString Else LRNO = ""
                        GRIDLRDATE = Format(Convert.ToDateTime(row.Cells(GLRDATE.Index).Value).Date, "MM/dd/yyyy")
                        If row.Cells(GNarration.Index).Value <> Nothing Then NARRATION = row.Cells(GNarration.Index).Value.ToString Else NARRATION = ""
                        If row.Cells(GDONE.Index).Value = True Then
                            DONE = 1
                        Else
                            DONE = 0
                        End If
                        OUTBAGS = Val(row.Cells(GOUTBAGS.Index).Value)
                        OUTWT = Val(row.Cells(GOUTWT.Index).Value)
                        PONO = row.Cells(GPONO.Index).Value
                        POSRNO = row.Cells(GPOSRNO.Index).Value
                        GRIDTYPE = row.Cells(GTYPE.Index).Value
                        PURBAGS = Val(row.Cells(GPURBAGS.Index).Value)
                        PURWT = Val(row.Cells(GPURWT.Index).Value)

                    Else
                        gridsrno = gridsrno & "|" & row.Cells(gsrno.Index).Value
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        COUNT = COUNT & "|" & row.Cells(gcount.Index).Value.ToString
                        BAGS = BAGS & "|" & row.Cells(gBag.Index).Value
                        WT = WT & "|" & row.Cells(Gwt.Index).Value
                        GRIDLOTNO = GRIDLOTNO & "|" & row.Cells(GLOTNO.Index).Value
                        SHADE = SHADE & "|" & row.Cells(GSHADE.Index).Value.ToString
                        CONES = CONES & "|" & row.Cells(GCONE.Index).Value
                        If row.Cells(GLRNO.Index).Value <> Nothing Then LRNO = LRNO & "|" & row.Cells(GLRNO.Index).Value.ToString Else LRNO = LRNO & "|" & ""
                        GRIDLRDATE = GRIDLRDATE & "|" & Format(Convert.ToDateTime(row.Cells(GLRDATE.Index).Value).Date, "MM/dd/yyyy")
                        If row.Cells(GNarration.Index).Value <> Nothing Then NARRATION = NARRATION & "|" & row.Cells(GNarration.Index).Value.ToString Else NARRATION = NARRATION & "|" & ""
                        If row.Cells(GDONE.Index).Value = True Then
                            DONE = DONE & "|" & "1"
                        Else
                            DONE = DONE & "|" & "0"
                        End If
                        OUTBAGS = OUTBAGS & "|" & Val(row.Cells(GOUTBAGS.Index).Value)
                        OUTWT = OUTWT & "|" & Val(row.Cells(GOUTWT.Index).Value)
                        PONO = PONO & "|" & row.Cells(GPONO.Index).Value
                        POSRNO = POSRNO & "|" & row.Cells(GPOSRNO.Index).Value
                        GRIDTYPE = GRIDTYPE & "|" & row.Cells(GTYPE.Index).Value
                        PURBAGS = PURBAGS & "|" & Val(row.Cells(GPURBAGS.Index).Value)
                        PURWT = PURWT & "|" & Val(row.Cells(GPURWT.Index).Value)
                    End If
                End If
            Next

            alParaval.Add(gridsrno)
            alParaval.Add(QUALITY)
            alParaval.Add(COUNT)
            alParaval.Add(BAGS)
            alParaval.Add(WT)
            alParaval.Add(GRIDLOTNO)
            alParaval.Add(SHADE)
            alParaval.Add(CONES)
            alParaval.Add(LRNO)
            alParaval.Add(GRIDLRDATE)
            alParaval.Add(NARRATION)
            alParaval.Add(DONE)
            alParaval.Add(OUTBAGS)
            alParaval.Add(OUTWT)
            alParaval.Add(PONO)
            alParaval.Add(POSRNO)
            alParaval.Add(GRIDTYPE)
            alParaval.Add(PURBAGS)
            alParaval.Add(PURWT)

            Dim griduploadsrno As String = ""
            Dim imgpath As String = ""
            Dim uploadremarks As String = ""
            Dim name As String = ""
            Dim NEWIMGPATH As String = ""
            Dim FILENAME As String = ""

            'Saving Upload Grid
            For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                If row.Cells(0).Value <> Nothing Then
                    If griduploadsrno = "" Then
                        griduploadsrno = row.Cells(0).Value.ToString
                        uploadremarks = row.Cells(1).Value.ToString
                        name = row.Cells(2).Value.ToString
                        imgpath = row.Cells(3).Value.ToString
                        NEWIMGPATH = row.Cells(GNEWIMGPATH.Index).Value.ToString

                    Else
                        griduploadsrno = griduploadsrno & "|" & row.Cells(0).Value.ToString
                        uploadremarks = uploadremarks & "|" & row.Cells(1).Value.ToString
                        name = name & "|" & row.Cells(2).Value.ToString
                        imgpath = imgpath & "|" & row.Cells(3).Value.ToString
                        NEWIMGPATH = NEWIMGPATH & "|" & row.Cells(GNEWIMGPATH.Index).Value.ToString

                    End If
                End If
            Next

            alParaval.Add(griduploadsrno)
            alParaval.Add(uploadremarks)
            alParaval.Add(name)
            alParaval.Add(imgpath)
            alParaval.Add(NEWIMGPATH)
            alParaval.Add(FILENAME)
            alParaval.Add(ClientName)


            Dim OBJCLSGRN As New ClsGrn()
            OBJCLSGRN.alParaval = alParaval
            If edit = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DTTABLE As DataTable = OBJCLSGRN.SAVE()
                If DTTABLE.Rows(0).Item(0) <> 0 Then txtgrnno.Text = DTTABLE.Rows(0).Item(0)
                MsgBox("Details Added")
            ElseIf edit = True Then
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPGRNNO)

                IntResult = OBJCLSGRN.UPDATE()
                MsgBox("Details Updated")
            End If

            edit = False
            'clear()
            'SHOW NEXT BILL ON EDIT MODE DONT CLEAR
            Call toolnext_Click(sender, e)
            CMBTYPE.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub GRN_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
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
            GRIDGRN.Focus()
        End If
    End Sub

    Private Sub GRN_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'GRN'")
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

                Dim objclsGRN As New ClsGrn()
                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(TEMPGRNNO)
                ALPARAVAL.Add(YearId)
                objclsGRN.alParaval = ALPARAVAL
                Dim dttable As DataTable = objclsGRN.selectGRN()

                If dttable.Rows.Count > 0 Then

                    For Each dr As DataRow In dttable.Rows

                        txtgrnno.Text = TEMPGRNNO
                        GRNDATE.Text = Format(Convert.ToDateTime(dr("GRNDATE")).Date, "dd/MM/yyyy")

                        CMBTYPE.Text = Convert.ToString(dr("TYPE"))
                        CMBTYPE.Enabled = False

                        HIDEVIEW()
                        CMBQUALITY.Text = ""

                        cmbname.Text = Convert.ToString(dr("NAME").ToString)
                        cmdselectPO.Enabled = True

                        TXTBILLNO.Text = Convert.ToString(dr("BILLNO").ToString)
                        TEMPBILLNO = Convert.ToString(dr("BILLNO").ToString)

                        txtpono.Text = Val(dr("PONO"))
                        TXTPOSRNO.Text = Val(dr("POSRNO"))
                        TXTPOTYPE.Text = Convert.ToString(dr("POTYPE").ToString)
                        podate.Value = Format(Convert.ToDateTime(dr("PODATE")).Date, "dd/MM/yyyy")

                        txtchallan.Text = Convert.ToString(dr("CHALLANNO").ToString)
                        CHALLANDATE.Text = dr("CHALLANDATE")
                        cmbGodown.Text = Convert.ToString(dr("GODOWN").ToString)
                        CMBBROKER.Text = Convert.ToString(dr("BROKER").ToString)
                        cmbtrans.Text = dr("TRANSNAME").ToString
                        TEMPTRANS = dr("TRANSNAME").ToString

                        CMBMILL.Text = dr("MILLNAME").ToString
                        CMBINVNAME.Text = dr("INVNAME").ToString

                        CMBPROCESSOR.Text = dr("PROCESSOR").ToString
                        TXTLOTNO.Text = Val(dr("LOTNO"))
                        CHECKDATE.Value = Format(Convert.ToDateTime(dr("CHECKDATE")).Date, "dd/MM/yyyy")
                        TXTREJPCS.Text = Val(dr("REJPCS"))
                        TXTREJMTRS.Text = Val(dr("REJMTRS"))
                        TXTOPSHORTMTRS.Text = Val(dr("OPSHORTMTRS"))
                        TXTSHORTPCS.Text = Val(dr("SHORTPCS"))
                        TXTSHORTMTRS.Text = Val(dr("SHORTMTRS"))
                        TXTACCEPTPCS.Text = Val(dr("ACCEPTPCS"))
                        TXTACCEPTMTRS.Text = Val(dr("ACCEPTMTRS"))

                        txtremarks.Text = Convert.ToString(dr("remarks").ToString)
                        If Convert.ToBoolean(dr("PAID")) = True Then CHKPAID.CheckState = CheckState.Checked
                        If Convert.ToBoolean(dr("PENREPORT")) = True Then CHKPENDINGREPORTS.Checked = True Else CHKPENDINGREPORTS.Checked = False


                        'Item Grid
                        GRIDGRN.Rows.Add(dr("GRIDSRNO").ToString, dr("QUALITY").ToString, Val(dr("COUNT")), Val(dr("BAGS")), Val(dr("WT")), dr("GRIDLOTNO"), dr("SHADE").ToString, Val(dr("CONES")), dr("LRNO").ToString, Format(Convert.ToDateTime(dr("LRDATE")).Date, "dd/MM/yyyy"), dr("NARRATION").ToString, dr("GRIDDONE"), Val(dr("OUTBAGS")), Val(dr("OUTWT")), dr("PONO"), dr("POSRNO"), dr("GRIDTYPE"), Val(dr("PURBAGS")), Val(dr("PURWT")))

                        If Val(dr("OUTBAGS")) > 0 Or Val(dr("OUTWT")) > 0 Then
                            GRIDGRN.Rows(GRIDGRN.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                            lbllocked.Visible = True
                            PBlock.Visible = True
                        End If

                        If Val(dr("PURBAGS")) > 0 Or Val(dr("PURWT")) > 0 Then
                            GRIDGRN.Rows(GRIDGRN.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                            lbllocked.Visible = True
                            PBlock.Visible = True
                        End If

                        If Convert.ToBoolean(dr("DONE")) = True Then
                            lbllocked.Visible = True
                            PBlock.Visible = True
                        End If

                    Next

                    total()
                    cmbname.Focus()
                Else
                    edit = False
                    clear()
                    CMBTYPE.Focus()
                End If

                Dim OBJCMN As New ClsCommon
                dttable = OBJCMN.search(" GRN_GRIDSRNO AS GRIDSRNO, GRN_REMARKS AS REMARKS, GRN_NAME AS NAME, GRN_IMGPATH AS IMGPATH, GRN_NEWIMGPATH AS NEWIMGPATH", "", " GRN_UPLOAD", " AND GRN_NO = " & TEMPGRNNO & " AND GRN_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    For Each DTR As DataRow In dttable.Rows
                        gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), DTR("IMGPATH"), DTR("NEWIMGPATH"))
                    Next
                End If

                Dim OBJCOMMON As New ClsCommon
                dttable = OBJCOMMON.search(" ISNULL(FORMTYPE.FORM_NAME, '') AS FORMNAME", "", " GRN_FORMTYPE LEFT OUTER JOIN FORMTYPE ON GRN_FORMTYPE.GRN_FORMID = FORMTYPE.FORM_ID ", " AND GRN_FORMTYPE.GRN_NO = " & TEMPGRNNO & " AND GRN_FORMTYPE.GRN_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    For Each ROW As DataRow In dttable.Rows
                        For I As Integer = 0 To CHKFORMBOX.Items.Count - 1
                            Dim DTR As DataRowView = CHKFORMBOX.Items(I)
                            If ROW("FORMNAME") = DTR.Item(0) Then
                                CHKFORMBOX.SetItemCheckState(I, CheckState.Checked)
                            End If
                        Next
                    Next
                End If


                For Each DTROW1 As DataRowView In CHKFORMBOX.CheckedItems
                    TEMPFORM = DTROW1.Item(0)
                    dttable = OBJCOMMON.search(" ISNULL(FORMTYPE.FORM_ISLR, 0) AS ISLR", "", " FORMTYPE ", " AND FORMTYPE.FORM_NAME = '" & DTROW1.Item(0) & "'")
                    If Convert.ToBoolean(dttable.Rows(0).Item(0)) = True Then
                        If ClientName = "JASHOK" Then BTNLRNO.Text = "LR / DO No." Else BTNLRNO.Text = "LR No."
                    Else
                        If ClientName = "JASHOK" Then BTNLRNO.Text = "LR / DO No." Else BTNLRNO.Text = "DO No."
                    End If
                Next


                If Val(txtpono.Text.Trim) = 0 Then
                    cmdselectPO.Enabled = False
                    cmbname.Enabled = True
                Else
                    cmdselectPO.Enabled = True
                    cmbname.Enabled = False
                End If

            End If

            If GRIDGRN.RowCount > 0 Then
                txtsrno.Text = Val(GRIDGRN.Rows(GRIDGRN.RowCount - 1).Cells(0).Value) + 1
            Else
                txtsrno.Text = 1
            End If

            If ClientName = "MASHOK" Then
                CHKPENDINGREPORTS.Visible = True
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try

    End Sub

    Sub fillcmb()
        Try
            fillname(cmbname, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'  AND (LEDGERS.ACC_SUBTYPE = 'ACCOUNTS'OR LEDGERS.ACC_SUBTYPE = 'MILL')")
            fillname(CMBMILL, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'MILL'")
            If CMBTYPE.Text.Trim = "YARN" And ClientName <> "SHREEJI" Then fillGODOWN(cmbGodown, edit, " AND GODOWN_ISOUR = 'False' ") Else fillGODOWN(cmbGodown, edit, " AND GODOWN_ISOUR = 'True' ")

            fillname(CMBBROKER, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'AGENT'")
            fillname(cmbtrans, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'TRANSPORT'")
            fillname(CMBPROCESSOR, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'PROCESSOR'")
            fillQUALITY(CMBQUALITY, edit)
            FILLCOLOR(CMBSHADE)
            fillform(CHKFORMBOX, edit)
            If CMBINVNAME.Text.Trim = "" Then fillname(CMBINVNAME, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY DEBTORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_ENTER(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbGodown.Enter
        Try
            If cmbGodown.Text.Trim = "" Then
                If CMBTYPE.Text = "YARN" And ClientName <> "SHREEJI" Then
                    fillGODOWN(cmbGodown, edit, " AND GODOWN_ISOUR = 'False'")
                Else
                    fillGODOWN(cmbGodown, edit, " AND GODOWN_ISOUR = 'True'")
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbGodown_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbGodown.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJGODOWN As New SelectGodown
                OBJGODOWN.FRMSTRING = "GODOWN"
                If CMBTYPE.Text = "YARN" And ClientName <> "SHREEJI" Then
                    OBJGODOWN.SEARCH = " AND GODOWN_ISOUR = 'False'"
                Else
                    OBJGODOWN.SEARCH = " AND GODOWN_ISOUR = 'True'"
                End If
                OBJGODOWN.ShowDialog()
                If OBJGODOWN.TEMPNAME <> "" Then cmbGodown.Text = OBJGODOWN.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbGodown.Validating
        Try
            If cmbGodown.Text.Trim <> "" Then
                If CMBTYPE.Text.Trim = "YARN" And ClientName <> "SHREEJI" Then
                    GODOWNVALIDATE(cmbGodown, e, Me, " AND GODOWN_ISOUR = 'False'")
                Else
                    GODOWNVALIDATE(cmbGodown, e, Me, " AND GODOWN_ISOUR = 'True'")
                End If
            End If
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

            Dim objgrndetails As New GRNDetails
            objgrndetails.MdiParent = MDIMain
            objgrndetails.Show()
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
            RECNODUPLICATION()
            LRNODUPLICATION()
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

    Sub uploadgetsrno(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            'If edit = False Then
            Dim i As Integer = 0
            For Each row As DataGridViewRow In grid.Rows
                If row.Visible = True Then
                    row.Cells(GGRIDUPLOADSRNO.Index).Value = i + 1
                    i = i + 1
                End If
            Next
            'End If
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


            Dim DTPO As New DataTable

            If CMBTYPE.Text.Trim = "YARN" Then
                If CMBMILL.Text.Trim = "" Then
                    MsgBox("Enter Mill Name")
                    CMBMILL.Focus()
                    Exit Sub
                End If
            Else
                If cmbname.Text.Trim = "" Then
                    MsgBox("Enter Party Name")
                    cmbname.Focus()
                    Exit Sub
                End If
            End If

            Dim OBJSELECTPO As New SelectPO
            OBJSELECTPO.MILLNAME = CMBMILL.Text.Trim
            If ClientName <> "JASHOK" Then OBJSELECTPO.PARTYNAME = cmbname.Text.Trim
            OBJSELECTPO.TYPE = CMBTYPE.Text.Trim
            DTPO = OBJSELECTPO.DT
            OBJSELECTPO.ShowDialog()


            Dim i As Integer = 0
            If DTPO.Rows.Count > 0 Then

                Dim OBJCMN As New ClsCommon

                For Each DTROW As DataRow In DTPO.Rows
                    Dim DT As New DataTable
                    If CMBTYPE.Text = "YARN" Then
                        DT = OBJCMN.search("*", "", " (SELECT ISNULL(LEDGERS.Acc_cmpname,'') AS NAME, MILLLEDGERS.Acc_cmpname AS MILLNAME, OPENINGPURCHASEORDER.OPPO_NO AS PONO, OPENINGPURCHASEORDER.OPPO_DATE AS PODATE, QUALITYMASTER.QUALITY_NAME AS QUALITY, OPENINGPURCHASEORDER_DESC.OPPO_count AS COUNT, OPENINGPURCHASEORDER_DESC.OPPO_BAGS - OPENINGPURCHASEORDER_DESC.OPPO_OUTBAGS AS BAGS, OPENINGPURCHASEORDER_DESC.OPPO_WT - OPENINGPURCHASEORDER_DESC.OPPO_OUTWT AS WT, OPENINGPURCHASEORDER_DESC.OPPO_GRIDSRNO AS GRIDSRNO, ISNULL(TRANS1.Acc_cmpname, '') AS TRANS, ISNULL(BROKERLEDGER.Acc_cmpname, '') AS BROKER, 'OPENING' AS TYPE, OPPO_TYPE AS POTYPE ,'' AS INVNAME FROM OPENINGPURCHASEORDER INNER JOIN OPENINGPURCHASEORDER_DESC ON OPENINGPURCHASEORDER.OPPO_NO = OPENINGPURCHASEORDER_DESC.OPPO_NO AND  OPENINGPURCHASEORDER.OPPO_YEARID = OPENINGPURCHASEORDER_DESC.OPPO_YEARID INNER JOIN QUALITYMASTER ON OPENINGPURCHASEORDER_DESC.OPPO_QUALITYID = QUALITYMASTER.QUALITY_ID LEFT OUTER JOIN LEDGERS ON OPENINGPURCHASEORDER.OPPO_LEDGERID = LEDGERS.Acc_id INNER JOIN LEDGERS AS MILLLEDGERS ON OPENINGPURCHASEORDER.OPPO_MILLID = MILLLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS BROKERLEDGER ON OPENINGPURCHASEORDER.OPPO_BROKERID = BROKERLEDGER.Acc_id  LEFT OUTER JOIN LEDGERS AS TRANS1 ON OPENINGPURCHASEORDER.OPPO_YEARID = TRANS1.Acc_yearid AND OPENINGPURCHASEORDER.OPPO_TRANS1ID = TRANS1.Acc_id WHERE OPENINGPURCHASEORDER.OPPO_YEARID = " & YearId & " UNION ALL SELECT ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, MILLLEDGERS.Acc_cmpname AS MILLNAME, PURCHASEORDER.PO_NO AS PONO, PURCHASEORDER.PO_DATE AS PODATE, QUALITYMASTER.QUALITY_NAME AS QUALITY,  PURCHASEORDER_DESC.PO_count AS COUNT, PURCHASEORDER_DESC.PO_BAGS - PURCHASEORDER_DESC.PO_OUTBAGS AS BAGS, PURCHASEORDER_DESC.PO_WT - PURCHASEORDER_DESC.PO_OUTWT AS WT, PURCHASEORDER_DESC.PO_GRIDSRNO AS GRIDSRNO, ISNULL(TRANS1.Acc_cmpname, '') AS TRANS, ISNULL(BROKERLEDGER.Acc_cmpname, '') AS BROKER, 'PO' AS TYPE, PURCHASEORDER.PO_TYPE AS POTYPE, ISNULL(INVLEDGERS.Acc_cmpname, '') AS INVNAME FROM PURCHASEORDER INNER JOIN PURCHASEORDER_DESC ON PURCHASEORDER.PO_NO = PURCHASEORDER_DESC.PO_NO AND PURCHASEORDER.PO_YEARID = PURCHASEORDER_DESC.PO_YEARID INNER JOIN QUALITYMASTER ON PURCHASEORDER_DESC.PO_QUALITYID = QUALITYMASTER.QUALITY_ID LEFT OUTER JOIN LEDGERS ON PURCHASEORDER.PO_LEDGERID = LEDGERS.Acc_id INNER JOIN LEDGERS AS MILLLEDGERS ON PURCHASEORDER.PO_MILLID = MILLLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS INVLEDGERS ON PURCHASEORDER.PO_INVLEDGERID = INVLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS BROKERLEDGER ON PURCHASEORDER.PO_BROKERID = BROKERLEDGER.Acc_id LEFT OUTER JOIN LEDGERS AS TRANS1 ON PURCHASEORDER.PO_YEARID = TRANS1.Acc_yearid AND PURCHASEORDER.PO_TRANS1ID = TRANS1.Acc_id WHERE PURCHASEORDER.PO_YEARID = " & YearId & ") AS T ", " AND T.PONO = " & Val(DTROW(0)) & " AND T.GRIDSRNO = " & Val(DTROW(1)) & " AND T.TYPE = '" & DTROW(2) & "'")
                    Else
                        DT = OBJCMN.search("*", "", " (SELECT ISNULL(LEDGERS.Acc_cmpname,'') AS NAME, MILLLEDGERS.Acc_cmpname AS MILLNAME, OPENINGPURCHASEORDER.OPPO_NO AS PONO, OPENINGPURCHASEORDER.OPPO_DATE AS PODATE, GREYQUALITYMASTER.GREY_NAME AS QUALITY, OPENINGPURCHASEORDER_DESC.OPPO_count AS COUNT, OPENINGPURCHASEORDER_DESC.OPPO_BAGS - OPENINGPURCHASEORDER_DESC.OPPO_OUTBAGS AS BAGS, OPENINGPURCHASEORDER_DESC.OPPO_WT - OPENINGPURCHASEORDER_DESC.OPPO_OUTWT AS WT, OPENINGPURCHASEORDER_DESC.OPPO_GRIDSRNO AS GRIDSRNO, ISNULL(TRANS1.Acc_cmpname, '') AS TRANS, ISNULL(BROKERLEDGER.Acc_cmpname, '') AS BROKER, 'OPENING' AS TYPE, OPPO_TYPE AS POTYPE,'' AS INVNAME FROM OPENINGPURCHASEORDER INNER JOIN OPENINGPURCHASEORDER_DESC ON OPENINGPURCHASEORDER.OPPO_NO = OPENINGPURCHASEORDER_DESC.OPPO_NO AND  OPENINGPURCHASEORDER.OPPO_YEARID = OPENINGPURCHASEORDER_DESC.OPPO_YEARID INNER JOIN GREYQUALITYMASTER ON OPENINGPURCHASEORDER_DESC.OPPO_QUALITYID = GREYQUALITYMASTER.GREY_ID LEFT OUTER JOIN LEDGERS ON OPENINGPURCHASEORDER.OPPO_LEDGERID = LEDGERS.Acc_id INNER JOIN LEDGERS AS MILLLEDGERS ON OPENINGPURCHASEORDER.OPPO_MILLID = MILLLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS BROKERLEDGER ON OPENINGPURCHASEORDER.OPPO_BROKERID = BROKERLEDGER.Acc_id  LEFT OUTER JOIN LEDGERS AS TRANS1 ON OPENINGPURCHASEORDER.OPPO_YEARID = TRANS1.Acc_yearid AND OPENINGPURCHASEORDER.OPPO_TRANS1ID = TRANS1.Acc_id WHERE OPENINGPURCHASEORDER.OPPO_YEARID = " & YearId & " UNION ALL  SELECT ISNULL(LEDGERS.Acc_cmpname,'') AS NAME, MILLLEDGERS.Acc_cmpname AS MILLNAME, PURCHASEORDER.PO_NO AS PONO, PURCHASEORDER.PO_DATE AS PODATE, GREYQUALITYMASTER.GREY_NAME AS QUALITY, PURCHASEORDER_DESC.PO_count AS COUNT, PURCHASEORDER_DESC.PO_BAGS - PURCHASEORDER_DESC.PO_OUTBAGS AS BAGS, PURCHASEORDER_DESC.PO_WT - PURCHASEORDER_DESC.PO_OUTWT AS WT, PURCHASEORDER_DESC.PO_GRIDSRNO AS GRIDSRNO, ISNULL(TRANS1.Acc_cmpname, '') AS TRANS, ISNULL(BROKERLEDGER.Acc_cmpname, '') AS BROKER, 'PO' AS TYPE, PO_TYPE AS POTYPE , ISNULL(INVLEDGERS.Acc_cmpname, '') AS INVNAME FROM PURCHASEORDER INNER JOIN PURCHASEORDER_DESC ON PURCHASEORDER.PO_NO = PURCHASEORDER_DESC.PO_NO AND  PURCHASEORDER.PO_YEARID = PURCHASEORDER_DESC.PO_YEARID INNER JOIN GREYQUALITYMASTER ON PURCHASEORDER_DESC.PO_QUALITYID = GREYQUALITYMASTER.GREY_ID LEFT OUTER JOIN LEDGERS ON PURCHASEORDER.PO_LEDGERID = LEDGERS.Acc_id INNER JOIN LEDGERS AS MILLLEDGERS ON PURCHASEORDER.PO_MILLID = MILLLEDGERS.Acc_id  LEFT OUTER JOIN LEDGERS AS INVLEDGERS ON PURCHASEORDER.PO_INVLEDGERID = INVLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS BROKERLEDGER ON PURCHASEORDER.PO_BROKERID = BROKERLEDGER.Acc_id  LEFT OUTER JOIN LEDGERS AS TRANS1 ON PURCHASEORDER.PO_YEARID = TRANS1.Acc_yearid AND PURCHASEORDER.PO_TRANS1ID = TRANS1.Acc_id WHERE PURCHASEORDER.PO_YEARID = " & YearId & ") AS T", " AND T.PONO = " & Val(DTROW(0)) & " AND T.GRIDSRNO = " & Val(DTROW(1)) & " AND T.TYPE = '" & DTROW(2) & "'")
                    End If
                    If DT.Rows.Count > 0 Then

                        cmbname.Text = DT.Rows(0).Item("NAME")
                        cmbname.Enabled = True
                        txtpono.Text = DT.Rows(0).Item("PONO")
                        TXTPOSRNO.Text = DT.Rows(0).Item("GRIDSRNO")
                        TXTPOTYPE.Text = DT.Rows(0).Item("TYPE")
                        podate.Text = DT.Rows(0).Item("PODATE")
                        cmbtrans.Text = DT.Rows(0).Item("TRANS")
                        CMBMILL.Text = DT.Rows(0).Item("MILLNAME")
                        CMBPROCESSOR.Text = DT.Rows(0).Item("MILLNAME")
                        CMBBROKER.Text = DT.Rows(0).Item("BROKER")
                        CMBINVNAME.Text = DT.Rows(0).Item("INVNAME")

                        'ITEM GRID
                        For Each ROW As DataRow In DT.Rows
                            GRIDGRN.Rows.Add(0, ROW("QUALITY"), Val(ROW("COUNT")), Val(ROW("BAGS")), Val(ROW("WT")), "", "", 0, "", Format(Convert.ToDateTime(ROW("PODATE")).Date, "dd/MM/yyyy"), "", 0, 0, 0, Val(ROW("PONO")), Val(ROW("GRIDSRNO")), ROW("TYPE"), 0, 0)
                        Next

                        total()
                        getsrno(GRIDGRN)
                        cmbname.Focus()
                        podate.Enabled = False
                        cmdselectPO.Enabled = False
                    End If
                Next
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDGRN.RowCount = 0
                TEMPGRNNO = Val(tstxtbillno.Text)
                If TEMPGRNNO > 0 Then
                    edit = True
                    GRN_Load(sender, e)
                Else
                    clear()
                    edit = False
                    CMBTYPE.Focus()
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub fillgrid()

        GRIDGRN.Enabled = True

        If GRIDDOUBLECLICK = False Then
            GRIDGRN.Rows.Add(Val(txtsrno.Text.Trim), CMBQUALITY.Text.Trim, Val(txtcount.Text.Trim), Format(Val(txtbags.Text.Trim), "0"), Format(Val(TXTWT.Text.Trim), "0.00"), TXTGRIDLOTNO.Text.Trim, CMBSHADE.Text.Trim, Val(TXTCONES.Text.Trim), txtlrno.Text.Trim, Format(Convert.ToDateTime(LRDATE.Text).Date, "dd/MM/yyyy"), TXTNARR.Text.Trim, 0, 0, 0, Val(txtpono.Text.Trim), Val(TXTPOSRNO.Text.Trim), TXTPOTYPE.Text.Trim, 0, 0)
            getsrno(GRIDGRN)
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDGRN.Item(gsrno.Index, TEMPROW).Value = Val(txtsrno.Text.Trim)
            GRIDGRN.Item(GQUALITY.Index, TEMPROW).Value = CMBQUALITY.Text.Trim
            GRIDGRN.Item(gcount.Index, TEMPROW).Value = Val(txtcount.Text.Trim)
            GRIDGRN.Item(gBag.Index, TEMPROW).Value = Format(Val(txtbags.Text.Trim), "0")
            GRIDGRN.Item(Gwt.Index, TEMPROW).Value = Format(Val(TXTWT.Text.Trim), "0.00")
            GRIDGRN.Item(GLOTNO.Index, TEMPROW).Value = TXTGRIDLOTNO.Text.Trim
            GRIDGRN.Item(GSHADE.Index, TEMPROW).Value = CMBSHADE.Text.Trim
            GRIDGRN.Item(GCONE.Index, TEMPROW).Value = Format(Val(TXTCONES.Text.Trim), "0")
            GRIDGRN.Item(GLRNO.Index, TEMPROW).Value = txtlrno.Text.Trim
            GRIDGRN.Item(GLRDATE.Index, TEMPROW).Value = Format(Convert.ToDateTime(LRDATE.Text).Date, "dd/MM/yyyy")
            GRIDGRN.Item(GNarration.Index, TEMPROW).Value = TXTNARR.Text.Trim
            GRIDDOUBLECLICK = False
        End If

        total()

        GRIDGRN.FirstDisplayedScrollingRowIndex = GRIDGRN.RowCount - 1

        If GRIDGRN.RowCount > 0 Then
            txtsrno.Text = Val(GRIDGRN.Rows(GRIDGRN.RowCount - 1).Cells(0).Value) + 1
        Else
            txtsrno.Text = 1
        End If


        If ClientName <> "HARIA" Then
            CMBQUALITY.Text = ""
            txtbags.Clear()
        End If
        txtcount.Clear()
        TXTWT.Clear()
        TXTGRIDLOTNO.Clear()
        CMBSHADE.Text = ""
        TXTCONES.Clear()
        txtlrno.Clear()
        LRDATE.Text = GRNDATE.Text
        TXTNARR.Clear()
        If CMBTYPE.Text = "FINISHED" Then CMBQUALITY.Focus() Else GRIDGRN.Focus()
        If ClientName = "HARIA" Then CMBQUALITY.Focus()

    End Sub

    Sub fillgridscan()
        Try
            If GRIDUPLOADDOUBLECLICK = False Then

                gridupload.Rows.Add(Val(txtuploadsrno.Text.Trim), txtuploadremarks.Text.Trim, txtuploadname.Text.Trim, txtimgpath.Text.Trim, TXTNEWIMGPATH.Text.Trim, TXTFILENAME.Text.Trim)
                uploadgetsrno(gridupload)

            ElseIf GRIDUPLOADDOUBLECLICK = True Then

                gridupload.Item(0, TEMPUPLOADROW).Value = txtuploadsrno.Text.Trim
                gridupload.Item(1, TEMPUPLOADROW).Value = txtuploadremarks.Text.Trim
                gridupload.Item(2, TEMPUPLOADROW).Value = txtuploadname.Text.Trim
                gridupload.Item(3, TEMPUPLOADROW).Value = txtimgpath.Text.Trim
                gridupload.Item(GNEWIMGPATH.Index, TEMPUPLOADROW).Value = TXTNEWIMGPATH.Text.Trim
                gridupload.Item(GFILENAME.Index, TEMPUPLOADROW).Value = TXTFILENAME.Text.Trim

                GRIDUPLOADDOUBLECLICK = False

            End If
            gridupload.FirstDisplayedScrollingRowIndex = gridupload.RowCount - 1
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdupload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdupload.Click

        If (edit = True And USEREDIT = False And USERVIEW = False) Or (edit = False And USERADD = False) Then
            MsgBox("Insufficient Rights")
            Exit Sub
        End If

        OpenFileDialog1.Filter = "Pictures (*.bmp;*.jpeg;*.png;*.pdf)|*.bmp;*.jpg;*.png;*.pdf"
        OpenFileDialog1.ShowDialog()

        OpenFileDialog1.AddExtension = True
        TXTFILENAME.Text = OpenFileDialog1.SafeFileName
        txtimgpath.Text = OpenFileDialog1.FileName
        TXTNEWIMGPATH.Text = Application.StartupPath & "\UPLOADDOCS\" & txtgrnno.Text.Trim & txtuploadsrno.Text.Trim & TXTFILENAME.Text.Trim
        On Error Resume Next

        If txtimgpath.Text.Trim.Length <> 0 Then
            PBSoftCopy.ImageLocation = txtimgpath.Text.Trim
            PBSoftCopy.Load(txtimgpath.Text.Trim)
            txtuploadsrno.Focus()
        End If
    End Sub

    Private Sub txtuploadname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtuploadname.Validating
        Try
            If txtimgpath.Text.Trim <> "" And txtuploadname.Text.Trim <> "" Then
                fillgridscan()
                txtuploadremarks.Clear()
                txtuploadname.Clear()
                txtimgpath.Clear()
                PBSoftCopy.ImageLocation = ""
                txtuploadsrno.Focus()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub gridupload_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridupload.CellDoubleClick
        Try
            If gridupload.Rows(e.RowIndex).Cells(GGRIDUPLOADSRNO.Index).Value <> Nothing Then
                GRIDUPLOADDOUBLECLICK = True
                TEMPUPLOADROW = e.RowIndex
                txtuploadsrno.Text = gridupload.Rows(e.RowIndex).Cells(GGRIDUPLOADSRNO.Index).Value
                txtuploadremarks.Text = gridupload.Rows(e.RowIndex).Cells(GREMARKS.Index).Value
                txtuploadname.Text = gridupload.Rows(e.RowIndex).Cells(GNAME.Index).Value
                txtimgpath.Text = gridupload.Rows(e.RowIndex).Cells(GIMGPATH.Index).Value
                TXTNEWIMGPATH.Text = gridupload.Rows(e.RowIndex).Cells(GNEWIMGPATH.Index).Value
                TXTFILENAME.Text = gridupload.Rows(e.RowIndex).Cells(GFILENAME.Index).Value
                txtuploadsrno.Focus()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridupload_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gridupload.KeyDown
        If e.KeyCode = Keys.Delete And gridupload.RowCount > 0 Then
            Dim TEMPMSG As Integer = MsgBox("This Will Delete File, Wish to Proceed?", MsgBoxStyle.YesNo)
            If TEMPMSG = vbYes Then
                If FileIO.FileSystem.FileExists(gridupload.Rows(gridupload.CurrentRow.Index).Cells(GNEWIMGPATH.Index).Value) Then FileIO.FileSystem.DeleteFile(gridupload.Rows(gridupload.CurrentRow.Index).Cells(GNEWIMGPATH.Index).Value)
                gridupload.Rows.RemoveAt(gridupload.CurrentRow.Index)
                uploadgetsrno(gridupload)
            End If
        End If
    End Sub

    Private Sub gridupload_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridupload.RowEnter
        Try
            If gridupload.RowCount > 0 Then
                If Not FileIO.FileSystem.FileExists(gridupload.Rows(e.RowIndex).Cells(GNEWIMGPATH.Index).Value) Then
                    PBSoftCopy.ImageLocation = gridupload.Rows(e.RowIndex).Cells(GIMGPATH.Index).Value
                Else
                    PBSoftCopy.ImageLocation = gridupload.Rows(e.RowIndex).Cells(GNEWIMGPATH.Index).Value
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub txtuploadsrno_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtuploadsrno.GotFocus
        If GRIDUPLOADDOUBLECLICK = False Then
            If gridupload.RowCount > 0 Then
                txtuploadsrno.Text = Val(gridupload.Rows(gridupload.RowCount - 1).Cells(GGRIDUPLOADSRNO.Index).Value) + 1
            Else
                txtuploadsrno.Text = 1
            End If
        End If
    End Sub

    Sub EDITROW()
        Try
            If GRIDGRN.CurrentRow.Index >= 0 And GRIDGRN.Item(gsrno.Index, GRIDGRN.CurrentRow.Index).Value <> Nothing Then

                'DONE TEMPORARILY
                'If Convert.ToDouble(GRIDGRN.Rows(GRIDGRN.CurrentRow.Index).Cells(GOUTBAGS.Index).Value) > 0 Then
                '    MsgBox("Item Locked", MsgBoxStyle.Critical)
                '    Exit Sub
                'End If

                'If Convert.ToDouble(GRIDGRN.Rows(GRIDGRN.CurrentRow.Index).Cells(GPURBAGS.Index).Value) > 0 Then
                '    MsgBox("Item Locked", MsgBoxStyle.Critical)
                '    Exit Sub
                'End If

                GRIDDOUBLECLICK = True
                txtsrno.Text = GRIDGRN.Item(gsrno.Index, GRIDGRN.CurrentRow.Index).Value.ToString
                CMBQUALITY.Text = GRIDGRN.Item(GQUALITY.Index, GRIDGRN.CurrentRow.Index).Value.ToString
                txtcount.Text = GRIDGRN.Item(gcount.Index, GRIDGRN.CurrentRow.Index).Value.ToString
                txtbags.Text = GRIDGRN.Item(gBag.Index, GRIDGRN.CurrentRow.Index).Value.ToString
                TXTWT.Text = GRIDGRN.Item(Gwt.Index, GRIDGRN.CurrentRow.Index).Value.ToString
                TXTGRIDLOTNO.Text = GRIDGRN.Item(GLOTNO.Index, GRIDGRN.CurrentRow.Index).Value.ToString
                CMBSHADE.Text = GRIDGRN.Item(GSHADE.Index, GRIDGRN.CurrentRow.Index).Value.ToString
                TXTCONES.Text = GRIDGRN.Item(GCONE.Index, GRIDGRN.CurrentRow.Index).Value.ToString
                txtlrno.Text = GRIDGRN.Item(GLRNO.Index, GRIDGRN.CurrentRow.Index).Value.ToString
                TEMPLRNO = GRIDGRN.Item(GLRNO.Index, GRIDGRN.CurrentRow.Index).Value.ToString
                LRDATE.Text = Format(Convert.ToDateTime(GRIDGRN.Item(GLRDATE.Index, GRIDGRN.CurrentRow.Index).Value).Date, "dd/MM/yyyy")
                TXTNARR.Text = GRIDGRN.Item(GNarration.Index, GRIDGRN.CurrentRow.Index).Value.ToString

                TEMPROW = GRIDGRN.CurrentRow.Index
                txtsrno.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridgrn_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDGRN.CellDoubleClick
        EDITROW()
    End Sub

    Private Sub toolprevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            GRIDGRN.RowCount = 0
LINE1:
            TEMPGRNNO = Val(txtgrnno.Text) - 1
Line2:
            If TEMPGRNNO > 0 Then

                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" GRN_NO ", "", "  GRN ", " AND GRN_NO = '" & TEMPGRNNO & "' AND GRN.GRN_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    edit = True
                    GRN_Load(sender, e)
                Else
                    TEMPGRNNO = Val(TEMPGRNNO - 1)
                    GoTo Line2
                End If
            Else
                clear()
                edit = False
                CMBTYPE.Focus()
            End If

            If GRIDGRN.RowCount = 0 And TEMPGRNNO > 1 Then
                txtgrnno.Text = TEMPGRNNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            GRIDGRN.RowCount = 0
LINE1:
            TEMPGRNNO = Val(txtgrnno.Text) + 1
            getmaxno()
            Dim MAXNO As Integer = txtgrnno.Text.Trim
            clear()
            If Val(txtgrnno.Text) - 1 >= TEMPGRNNO Then
                edit = True
                GRN_Load(sender, e)
            Else
                clear()
                edit = False
                CMBTYPE.Focus()
            End If
            If GRIDGRN.RowCount = 0 And TEMPGRNNO < MAXNO Then
                txtgrnno.Text = TEMPGRNNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub txtbags_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtbags.KeyPress, TXTREJPCS.KeyPress, TXTSHORTPCS.KeyPress, txtcount.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tooldelete.Click
        Try
            Call cmddelete_Click(sender, e)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub podate_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles podate.Validating
        If Not datecheck(podate.Value) Then
            MsgBox("Date Not in Current Accounting Year")
            e.Cancel = True
        End If
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
                    MsgBox("Unable to Delete, GRN Used", MsgBoxStyle.Critical)
                    Exit Sub
                End If
                TEMPMSG = MsgBox("Delete GRN?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbYes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(txtgrnno.Text.Trim)
                    alParaval.Add(YearId)
                    alParaval.Add(ClientName)

                    Dim Clsgrn As New ClsGrn()
                    Clsgrn.alParaval = alParaval
                    IntResult = Clsgrn.Delete()
                    MsgBox("GRN Deleted")
                    clear()
                    edit = False
                    CMBTYPE.Focus()
                End If
            Else
                MsgBox("Delete is only in Edit Mode")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub gridgrn_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles GRIDGRN.CellValidating
        Try
            Dim colNum As Integer = GRIDGRN.Columns(e.ColumnIndex).Index
            If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return

            Select Case colNum

                Case gBag.Index, Gwt.Index, gcount.Index
                    Dim dDebit As Decimal
                    Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

                    If bValid Then
                        If GRIDGRN.CurrentCell.Value = Nothing Then GRIDGRN.CurrentCell.Value = "0.00"
                        GRIDGRN.CurrentCell.Value = Convert.ToDecimal(GRIDGRN.Item(colNum, e.RowIndex).Value)
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

    Private Sub gridgrn_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDGRN.KeyDown

        Try
            If e.KeyCode = Keys.Delete And GRIDGRN.RowCount > 0 Then
                'dont allow user if any of the grid line is in edit mode.....
                'cmbitemname.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If

                If lbllocked.Visible = True Then
                    MessageBox.Show("Do not delete the Line, this will create Stock Problem")
                    Exit Sub
                End If

                'end of block
                GRIDGRN.Rows.RemoveAt(GRIDGRN.CurrentRow.Index)
                getsrno(GRIDGRN)
                total()
            ElseIf e.KeyCode = Keys.F5 Then
                EDITROW()
            ElseIf e.KeyCode = Keys.F12 And GRIDGRN.RowCount > 0 Then
                'If GRIDGRN.CurrentRow.Cells(GQUALITY.Index).Value <> "" Then GRIDGRN.Rows.Add(CloneWithValues(GRIDGRN.CurrentRow))
                'If GRIDGRN.CurrentRow.Index > 0 Then If e.KeyCode = Keys.F12 Then If GRIDGRN.Rows(GRIDGRN.CurrentRow.Index - 1).Cells(GLRNO.Index).Value <> "" Then GRIDGRN.Rows(GRIDGRN.CurrentRow.Index).Cells(GLRNO.Index).Value = GRIDGDN.Rows(GRIDGDN.CurrentRow.Index - 1).Cells(GLRNO.Index).Value
                If GRIDGRN.CurrentRow.Cells(GLRNO.Index).Value.ToString <> "" Then GRIDGRN.Rows.Add(CloneWithValues(GRIDGRN.CurrentRow))
                If GRIDGRN.Rows(GRIDGRN.RowCount - 1).Cells(GLRNO.Index).Value <> Nothing Then GRIDGRN.Rows(GRIDGRN.RowCount - 1).Cells(GLRNO.Index).Value = Val(GRIDGRN.Rows(GRIDGRN.RowCount - 2).Cells(GLRNO.Index).Value) + 1
                GRIDGRN.FirstDisplayedScrollingRowIndex = GRIDGRN.RowCount - 1
                GRIDGRN.Rows(GRIDGRN.RowCount - 1).Selected = True
                getsrno(GRIDGRN)
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

    Private Sub CMBBROKER_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBBROKER.Enter
        Try
            If CMBBROKER.Text.Trim = "" Then fillname(CMBBROKER, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'AGENT'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBROKER_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBBROKER.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'AGENT'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPAGENT <> "" Then CMBBROKER.Text = OBJLEDGER.TEMPAGENT
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBROKER_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBBROKER.Validating
        Try
            If CMBBROKER.Text.Trim <> "" Then namevalidate(CMBBROKER, CMBCODE, e, Me, txtadd, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'AGENT'", "Sundry Creditors", "AGENT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTWT_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTWT.KeyPress, TXTREJMTRS.KeyPress, TXTOPSHORTMTRS.KeyPress, TXTSHORTMTRS.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub cmbname_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbname.Enter
        Try
            If cmbname.Text.Trim = "" Then fillname(cmbname, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'ACCOUNTS'OR LEDGERS.ACC_SUBTYPE = 'MILL')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbname.Validating
        Try
            If cmbname.Text.Trim <> "" Then namevalidate(cmbname, CMBCODE, e, Me, txtadd, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'ACCOUNTS'OR LEDGERS.ACC_SUBTYPE = 'MILL')", "Sundry Creditors", "ACCOUNTS", cmbtrans.Text, CMBBROKER.Text)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPROCESSOR_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBPROCESSOR.Enter
        Try
            If CMBPROCESSOR.Text.Trim = "" Then fillname(CMBPROCESSOR, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPROCESSOR_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBPROCESSOR.Validating
        Try
            If CMBPROCESSOR.Text.Trim <> "" Then namevalidate(CMBPROCESSOR, CMBCODE, e, Me, txtadd, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'", "Sundry Creditors", "ACCOUNTS", "", "", "PROCESSOR")
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

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        'Try
        '    If edit = True Then
        '        Dim OBJGRN As New GRNDesign
        '        OBJGRN.GRNNO = TEMPGRNNO
        '        OBJGRN.FRMSTRING = "GRN"
        '        OBJGRN.MdiParent = MDIMain
        '        OBJGRN.selfor_GRN = "{GRN.GRN_NO}=" & TEMPGRNNO & " AND {GRN.GRN_TYPE} = '" & cmbtype.Text.Trim & "' and {GRN.GRN_cmpid}=" & CmpId & " and {GRN.GRN_locationid}=" & Locationid & " and {GRN.GRN_yearid}=" & YearId
        '        OBJGRN.vendorname = cmbname.Text.Trim
        '        OBJGRN.Show()
        '    End If
        'Catch ex As Exception
        '    If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        'End Try
    End Sub

    Private Sub cmbname_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbname.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'ACCOUNTS'OR LEDGERS.ACC_SUBTYPE = 'MILL')"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then cmbname.Text = OBJLEDGER.TEMPNAME
                If OBJLEDGER.TEMPAGENT <> "" Then CMBBROKER.Text = OBJLEDGER.TEMPAGENT
            End If
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

    Function CHECKGRID() As Boolean
        Try
            Dim bln As Boolean = True
            For Each ROW As DataGridViewRow In GRIDGRN.Rows
                If (GRIDDOUBLECLICK = False And LCase(txtlrno.Text.Trim) = LCase(ROW.Cells(GLRNO.Index).Value)) Or (GRIDDOUBLECLICK = True And TEMPROW <> ROW.Index And LCase(txtlrno.Text.Trim) = LCase(ROW.Cells(GLRNO.Index).Value)) Then
                    bln = False
                    Exit For
                End If
            Next
            Return bln
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub TXTNARR_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTNARR.Validated
        Try
            If CMBQUALITY.Text.Trim <> "" And Val(txtbags.Text.Trim) > 0 And Val(TXTWT.Text.Trim) > 0 And LRDATE.Text <> "__/__/____" Then
                If CMBTYPE.Text.Trim <> "FINISHED" And txtlrno.Text.Trim = "" Then
                    MsgBox("Enter LR No.", MsgBoxStyle.Critical)
                    Exit Sub
                End If
                'If ClientName <> "JASHOK" Then
                '    If Val(TXTCONES.Text.Trim) = 0 Then
                '        MsgBox("Enter Cones", MsgBoxStyle.Critical)
                '        TXTCONES.Focus()
                '        Exit Sub
                '    End If
                '    If cmbtrans.Text.Trim = "" Then
                '        MsgBox("Enter Transport", MsgBoxStyle.Critical)
                '        cmbtrans.Focus()
                '        Exit Sub
                '    End If
                'End If


                If CMBTYPE.Text.Trim <> "FINISHED" And Not CHECKGRID() Then
                    MsgBox("LR No Already Present Below", MsgBoxStyle.Critical)
                    Exit Sub
                End If
                fillgrid()
            ElseIf CMBQUALITY.Text.Trim = "" Then
                MsgBox("Enter Quality", MsgBoxStyle.Critical)
                CMBQUALITY.Focus()
                Exit Sub
            ElseIf Val(txtbags.Text.Trim) = 0 Then
                MsgBox("Enter Bags", MsgBoxStyle.Critical)
                txtbags.Focus()
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

    Private Sub cmbQUALITY_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBQUALITY.Enter
        Try
            If CMBQUALITY.Text.Trim = "" Then
                If CMBTYPE.Text.Trim = "YARN" Then
                    fillQUALITY(CMBQUALITY, edit)
                Else
                    FILLGREY(CMBQUALITY, edit, " AND GREY_TYPE = '" & CMBTYPE.Text.Trim & "'")
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBQUALITY.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                If CMBTYPE.Text = "YARN" Then
                    Dim OBJROLLS As New SelectQuality
                    OBJROLLS.ShowDialog()
                    If OBJROLLS.TEMPNAME <> "" Then CMBQUALITY.Text = OBJROLLS.TEMPNAME
                Else
                    Dim OBJROLLS As New SelectGreyQuality
                    OBJROLLS.ShowDialog()
                    If OBJROLLS.TEMPNAME <> "" Then CMBQUALITY.Text = OBJROLLS.TEMPNAME
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbQUALITY_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBQUALITY.Validating
        Try
            If CMBQUALITY.Text.Trim <> "" Then
                If CMBTYPE.Text = "YARN" Then
                    QUALITYVALIDATE(CMBQUALITY, e, Me)
                Else
                    GREYVALIDATE(CMBQUALITY, e, Me, " AND GREY_TYPE = '" & CMBTYPE.Text.Trim & "'")
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBTYPE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBTYPE.Validating
        HIDEVIEW()
    End Sub

    Sub HIDEVIEW()
        Try
            If CMBTYPE.Text.Trim <> "" Then
                If CMBTYPE.Text = "YARN" Then
                    LBLMILL.Text = "Mill Name"
                    BTNBAGS.Text = "Bags"
                    BTNWT.Text = "Wt"
                    BTNCONE.Text = "Cones"
                    BTNCONE.Visible = True
                    TXTCONES.Visible = True
                    GCONE.Visible = True
                    GLRNO.Width = 80
                    BTNLRNO.Width = 80
                    txtlrno.Width = 80
                    BTNLRNO.Left = BTNCONE.Left + BTNCONE.Width
                    txtlrno.Left = TXTCONES.Left + TXTCONES.Width
                    BTNLRNO.Text = "LR/DO No"
                    BTNLRDATE.Text = "LR/DO Date"
                    BTNLRDATE.Visible = True
                    LRDATE.Visible = True
                    GLRDATE.Visible = True
                    BTNNARRATION.Width = 190
                    TXTNARR.Width = 190
                    BTNNARRATION.Left = BTNLRDATE.Left + BTNLRDATE.Width
                    TXTNARR.Left = LRDATE.Left + LRDATE.Width
                    GNarration.Width = 190
                    fillname(CMBMILL, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'MILL'")
                    fillQUALITY(CMBQUALITY, edit)
                    fillGODOWN(cmbGodown, edit, " AND GODOWN_ISOUR = 'False' ")
                    CMBMILL.BackColor = Color.LemonChiffon
                    GPGREY.Visible = False

                ElseIf CMBTYPE.Text = "FINISHED" Then
                    LBLMILL.Text = "Dyeing Name"
                    BTNBAGS.Text = "Taka"
                    BTNWT.Text = "Mtrs"
                    BTNCONE.Text = "Lot No"
                    BTNCONE.Visible = True
                    TXTCONES.Visible = True
                    GCONE.Visible = True
                    GLRNO.Width = 80
                    BTNLRNO.Width = 80
                    txtlrno.Width = 80
                    BTNLRNO.Left = BTNCONE.Left + BTNCONE.Width
                    txtlrno.Left = TXTCONES.Left + TXTCONES.Width
                    If ClientName = "HARIA" Then BTNLRNO.Text = "Taka No" Else BTNLRNO.Text = "Bale No"
                    BTNLRDATE.Visible = False
                    LRDATE.Visible = False
                    GLRDATE.Visible = False
                    BTNNARRATION.Width = 280
                    TXTNARR.Width = 280
                    GNarration.Width = 280
                    BTNNARRATION.Left = BTNLRNO.Left + BTNLRNO.Width
                    TXTNARR.Left = txtlrno.Left + txtlrno.Width
                    fillname(CMBMILL, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'")
                    FILLGREY(CMBQUALITY, edit, " AND GREY_TYPE = '" & CMBTYPE.Text.Trim & "'")
                    fillGODOWN(cmbGodown, edit, " AND GODOWN_ISOUR = 'True' ")
                    cmbGodown.Text = GETDEFAULTGODOWN()
                    CMBMILL.BackColor = Color.White
                    GPGREY.Visible = False

                Else
                    LBLMILL.Text = "Delivery At"
                    BTNBAGS.Text = "Pcs"
                    BTNWT.Text = "Mtrs"
                    BTNCONE.Text = "Cones"
                    BTNCONE.Visible = False
                    TXTCONES.Visible = False
                    GCONE.Visible = False
                    GLRNO.Width = 135
                    BTNLRNO.Width = 135
                    txtlrno.Width = 135
                    BTNLRNO.Left = BTNSHADE.Left + BTNSHADE.Width
                    txtlrno.Left = CMBSHADE.Left + CMBSHADE.Width
                    If ClientName = "HARIA" Then BTNLRNO.Text = "Taka No" Else BTNLRNO.Text = "Challan No"
                    BTNLRDATE.Text = "Challan Date"
                    BTNLRDATE.Visible = True
                    LRDATE.Visible = True
                    GLRDATE.Visible = True
                    BTNNARRATION.Width = 190
                    TXTNARR.Width = 190
                    GNarration.Width = 190
                    BTNLRDATE.Left = BTNLRNO.Left + BTNLRNO.Width
                    LRDATE.Left = txtlrno.Left + txtlrno.Width
                    BTNNARRATION.Left = BTNLRDATE.Left + BTNLRDATE.Width
                    TXTNARR.Left = LRDATE.Left + LRDATE.Width
                    fillname(CMBMILL, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'")
                    FILLGREY(CMBQUALITY, edit, " AND GREY_TYPE = '" & CMBTYPE.Text.Trim & "'")
                    fillGODOWN(cmbGodown, edit, " AND GODOWN_ISOUR = 'True' ")
                    cmbGodown.Text = GETDEFAULTGODOWN()
                    CMBMILL.BackColor = Color.LemonChiffon
                    GPGREY.Visible = True
                End If
                CMBTYPE.Enabled = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILL_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBMILL.Enter
        Try
            If CMBMILL.Text.Trim = "" Then
                If CMBTYPE.Text.Trim = "YARN" Then
                    fillname(CMBMILL, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'MILL'")
                Else
                    fillname(CMBMILL, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'")
                End If
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
                If CMBTYPE.Text = "YARN" Then
                    OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'MILL'"
                Else
                    OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'"
                End If
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBMILL.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILL_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBMILL.Validating
        Try
            If CMBMILL.Text.Trim <> "" Then
                If CMBTYPE.Text = "YARN" Then
                    namevalidate(CMBMILL, CMBCODE, e, Me, txtadd, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' and LEDGERS.ACC_SUBTYPE = 'MILL'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "MILL")
                Else
                    namevalidate(CMBMILL, CMBCODE, e, Me, txtadd, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'  AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'", "Sundry Creditors", "PROCESSOR")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub LRNODUPLICATION()
        Try
            'If cmbtrans.Text <> "" And txtlrno.Text <> "" Then
            '    Dim OBJCMN As New ClsCommon
            '    Dim dttable As DataTable = OBJCMN.search(" ISNULL(GRN_DESC.GRN_LRNO,'') AS LRNO ", "", " GRN INNER JOIN GRN_DESC ON GRN.grn_no = GRN_DESC.GRN_NO AND GRN.grn_cmpid = GRN_DESC.GRN_CMPID AND GRN.grn_locationid = GRN_DESC.GRN_LOCATIONID AND GRN.grn_yearid = GRN_DESC.GRN_YEARID LEFT OUTER JOIN LEDGERS ON GRN.grn_yearid = LEDGERS.Acc_yearid AND GRN.grn_transledgerid = LEDGERS.Acc_id ", " AND GRN_LRNO = '" & txtlrno.Text.Trim & "' AND LEDGERS.ACC_CMPNAME = '" & cmbtrans.Text.Trim & "' AND GRN_DESC.GRN_YEARID = " & YearId)
            '    If dttable.Rows.Count > 0 Then
            '        MsgBox("LR No Already Exists!")
            '        txtlrno.Focus()
            '    End If
            'End If


            If cmbtrans.Text <> "" And edit = True And GRIDDOUBLECLICK = True And (txtlrno.Text.Trim <> TEMPLRNO) Then
                Dim OBJCMN As New ClsCommon
                Dim dttable As DataTable = OBJCMN.search(" ISNULL(GRN_DESC.GRN_LRNO,'') AS LRNO ", "", " GRN INNER JOIN GRN_DESC ON GRN.grn_no = GRN_DESC.GRN_NO AND GRN.grn_yearid = GRN_DESC.GRN_YEARID LEFT OUTER JOIN LEDGERS ON GRN.grn_transledgerid = LEDGERS.Acc_id ", " AND GRN_LRNO = '" & txtlrno.Text.Trim & "' AND LEDGERS.ACC_CMPNAME = '" & cmbtrans.Text.Trim & "' AND GRN_DESC.GRN_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    MsgBox("LR No Already Exists!")
                    txtlrno.Focus()
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtlrno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtlrno.Validating
        LRNODUPLICATION()
    End Sub

    Private Sub cmbname_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbname.Validated
        'THIS IS DONE BCOZ CMBMILL IS NOT ONLY MILL PROCESSEOR ARE ALSO OPENED THERE
        'If cmbname.Text <> "" And CMBMILL.Text.Trim = "" And edit = False Then CMBMILL.Text = cmbname.Text
    End Sub

    Sub RECNODUPLICATION()
        Try
            If cmbtrans.Text <> "" And txtchallan.Text <> "" And edit = False Then
                Dim OBJCMN As New ClsCommon
                Dim dttable As DataTable = OBJCMN.search(" ISNULL(GRN.grn_challanno, '') AS RECNO ", "", " GRN LEFT OUTER JOIN LEDGERS ON GRN.grn_yearid = LEDGERS.Acc_yearid AND GRN.grn_transledgerid = LEDGERS.Acc_id ", " AND LEDGERS.ACC_CMPNAME = '" & cmbtrans.Text.Trim & "' AND GRN.GRN_CHALLANNO = '" & txtchallan.Text.Trim & "' AND GRN.GRN_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    MsgBox("Godown Rec No Already Present !", MsgBoxStyle.Critical)
                    txtchallan.Focus()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtchallan_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtchallan.Validating
        RECNODUPLICATION()
    End Sub

    Private Sub CHKFORMBOX_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHKFORMBOX.SelectedValueChanged
        For Each DTROW1 As DataRowView In CHKFORMBOX.CheckedItems
            TEMPFORM = DTROW1.Item(0)
            Dim OBJCOMMON As New ClsCommon
            Dim dttable As DataTable = OBJCOMMON.search(" ISNULL(FORMTYPE.FORM_ISLR, 0) AS ISLR", "", " FORMTYPE ", " AND FORMTYPE.FORM_NAME = '" & DTROW1.Item(0) & "'")
            If Convert.ToBoolean(dttable.Rows(0).Item(0)) = True Then
                BTNLRNO.Text = "LR No."
            Else
                BTNLRNO.Text = "DO No."
            End If
        Next
    End Sub

    Private Sub GRNDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles GRNDATE.GotFocus
        GRNDATE.Select(0, 0)
    End Sub

    Private Sub GRNDATE_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles GRNDATE.Validated
        Try
            If ClientName <> "SASHWINKUMAR" And GRNDATE.Text.Trim <> "__/__/____" Then
                LRDATE.Text = GRNDATE.Text
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRNDATE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles GRNDATE.Validating
        Try
            If GRNDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(GRNDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LRDATE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles LRDATE.Validating
        Try
            If LRDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(LRDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTRATE_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTRATE.KeyPress
        numdotkeypress(e, TXTRATE, Me)
    End Sub

    Private Sub TXTBILLNO_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTBILLNO.Validating
        Try
            If TXTBILLNO.Text.Trim = "" Then Exit Sub
            If edit = False Or (edit = True And LCase(TEMPBILLNO) <> LCase(TXTBILLNO.Text.Trim)) Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" GRN_NO AS GRNNO", "", " GRN ", " AND GRN_BILLNO = '" & TXTBILLNO.Text.Trim & "' AND GRN_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    MsgBox("Bill No Already Present in GRN No " & DT.Rows(0).Item(0), MsgBoxStyle.Critical)
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHALLANDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CHALLANDATE.GotFocus
        CHALLANDATE.Select(0, 0)
    End Sub

    Private Sub CHALLANDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CHALLANDATE.Validating
        Try
            If CHALLANDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(CHALLANDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCONES_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCONES.KeyPress
        numkeypress(e, TXTCONES, Me)
    End Sub

    Private Sub GRN_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            txtchallan.Visible = False
            LBLRECNO.Visible = False
            CHALLANDATE.Visible = False
            LBLRECDATE.Visible = False
            If ClientName <> "JASHOK" Then CMBMILL.TabStop = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDCONES_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDCONES.Click
        Try
            Dim OBJCONES As New PendingConeDetails
            OBJCONES.MdiParent = MDIMain
            OBJCONES.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTREJPCS_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTREJPCS.Validated, TXTREJMTRS.Validated, TXTSHORTPCS.Validated, TXTSHORTMTRS.Validated, TXTOPSHORTMTRS.Validated
        CALCACCEPTMTRS()
    End Sub

    Sub CALCACCEPTMTRS()
        Try
            TXTACCEPTPCS.Text = Format(Val(LBLTOTALBAGS.Text) - Val(TXTREJPCS.Text) - Val(TXTSHORTPCS.Text), "0")
            TXTACCEPTMTRS.Text = Format(Val(LBLTOTALWT.Text) - Val(TXTREJMTRS.Text) - Val(TXTOPSHORTMTRS.Text) - Val(TXTSHORTMTRS.Text), "0.00")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBINVNAME_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBINVNAME.Enter
        Try
            If CMBINVNAME.Text.Trim = "" Then fillname(CMBINVNAME, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbINVname_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBINVNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry debtors' AND ACC_SUBTYPE = 'ACCOUNTS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then cmbname.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbINVname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBINVNAME.Validating
        Try
            namevalidate(CMBINVNAME, CMBCODE, e, Me, txtadd, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry debtors' ", "Sundry debtors", "ACCOUNTS")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try

    End Sub

    Private Sub CMBSHADE_Enter(sender As Object, e As EventArgs) Handles CMBSHADE.Enter
        Try
            If CMBSHADE.Text.Trim = "" Then FILLCOLOR(CMBSHADE)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBSHADE_Validating(sender As Object, e As CancelEventArgs) Handles CMBSHADE.Validating
        Try
            If CMBSHADE.Text.Trim <> "" Then COLORVALIDATE(CMBSHADE, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub LRDATE_GotFocus(sender As Object, e As EventArgs) Handles LRDATE.GotFocus
        LRDATE.Select(0, 0)
    End Sub

End Class