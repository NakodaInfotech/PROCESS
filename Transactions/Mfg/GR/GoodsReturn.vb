
Imports BL

Public Class GoodsReturn

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public EDIT As Boolean
    Public TEMPGRNO As Integer
    Dim DT As New DataTable

    Private Sub cmdexit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdclear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        CLEAR()
        EDIT = False
        cmbname.Focus()
    End Sub

    Sub CLEAR()
        DT.Rows.Clear()

        TXTGRNO.Clear()
        DTGRDATE.Text = Mydate
        CMBOURGODOWN.Text = GETDEFAULTGODOWN()
        CMBNAME.Text = ""
        TXTBUYERNAME.Clear()
        CMBLOTNO.DataSource = Nothing
        CMBLOTNO.Items.Clear()
        CMBLOTNO.Text = ""
        CHKDYEING.CheckState = CheckState.Unchecked
        cmbtrans.Text = ""
        TXTGREYQUALITY.Clear()
        TXTCHALLANTAKA.Clear()
        TXTCHALLANMTRS.Clear()
        TXTRETURNTAKA.Clear()
        TXTRETURNMTRS.Clear()
        TXTRETURNDONO.Clear()
        DODATE.Clear()
        TXTDAMARAGE.Clear()
        DTRETURNDATE.Clear()
        TXTSHORTAGE.Clear()
        LBLSHORTAGEPER.Text = "0 %"
        TXTNETTTAKA.Clear()
        TXTNETTMTRS.Clear()
        TXTREMARKS.Clear()
        tstxtbillno.Clear()
        CMDPENDINGGR.Enabled = True
        CMDPENDINGDATE.Enabled = True

        EP.Clear()
        GETMAX_GR_NO()
    End Sub

    Sub TOTAL()
        Try
            TXTNETTTAKA.Text = Val(TXTCHALLANTAKA.Text.Trim) - Val(TXTRETURNTAKA.Text.Trim)
            TXTSHORTAGE.Text = Format((Val(TXTCHALLANMTRS.Text.Trim) - (Val(TXTNETTMTRS.Text.Trim) + Val(TXTRETURNMTRS.Text.Trim))), "0.00")
            If Val(TXTSHORTAGE.Text.Trim) > 0 Then LBLSHORTAGEPER.Text = Format((Val(TXTSHORTAGE.Text.Trim) / (Val(TXTCHALLANMTRS.Text.Trim) - Val(TXTRETURNMTRS.Text.Trim))) * 100, "0.00") & " %"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETMAX_GR_NO()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax("ISNULL(MAX(GR_NO),0)+1", "GOODSRETURNMASTER", "AND GR_YEARID=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTGRNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub GoodsReturn_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        If CMBOURGODOWN.Text.Trim = "" Then fillGODOWN(CMBOURGODOWN, EDIT, " AND GODOWN_ISOUR='TRUE'")
        If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND ACC_SUBTYPE = 'PROCESSOR'")
        If cmbtrans.Text = "" Then fillname(cmbtrans, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'TRANSPORT'")
    End Sub

    Private Sub GoodsReturn_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'GDN'")
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
                Dim OBJGR As New ClsGoodsReturn

                OBJGR.alParaval.Add(TEMPGRNO)
                OBJGR.alParaval.Add(YearId)
                dttable = OBJGR.SELECTGR()

                If dttable.Rows.Count > 0 Then

                    CMDPENDINGGR.Enabled = False

                    TXTGRNO.Text = TEMPGRNO
                    DTGRDATE.Text = dttable.Rows(0).Item("GRDATE")
                    CMBOURGODOWN.Text = dttable.Rows(0).Item("GODOWN").ToString
                    CMBNAME.Text = dttable.Rows(0).Item("NAME").ToString
                    TXTBUYERNAME.Text = dttable.Rows(0).Item("BUYERNAME").ToString
                    CMBLOTNO.DataSource = Nothing
                    CMBLOTNO.Enabled = False
                    CMBLOTNO.Items.Add(dttable.Rows(0).Item("LOTNO"))

                    CMBLOTNO.Text = dttable.Rows(0).Item("LOTNO")

                    If Convert.ToBoolean(dttable.Rows(0).Item("FORDYEING")) = False Then CHKDYEING.Checked = False Else CHKDYEING.Checked = True
                    cmbtrans.Text = dttable.Rows(0).Item("TRANSPORT").ToString

                    TXTGREYQUALITY.Text = dttable.Rows(0).Item("GREYQUALITY").ToString
                    TXTCHALLANTAKA.Text = Val(dttable.Rows(0).Item("CHALLANTAKA"))
                    TXTCHALLANMTRS.Text = Val(dttable.Rows(0).Item("CHALLANMTRS"))
                    TXTRETURNTAKA.Text = Val(dttable.Rows(0).Item("RETURNTAKA"))
                    TXTRETURNMTRS.Text = Val(dttable.Rows(0).Item("RETURNMTRS"))
                    TXTRETURNDONO.Text = dttable.Rows(0).Item("RETURNDONO").ToString
                    If dttable.Rows(0).Item("DODATE") <> "01/01/1900" Then DODATE.Text = dttable.Rows(0).Item("DODATE")

                    TXTDAMARAGE.Text = Val(dttable.Rows(0).Item("DAMARAGE"))
                    If dttable.Rows(0).Item("RETURNDATE") <> "01/01/1900" Then DTRETURNDATE.Text = dttable.Rows(0).Item("RETURNDATE")

                    TXTSHORTAGE.Text = Val(dttable.Rows(0).Item("SHORTAGE"))
                    TXTNETTTAKA.Text = Val(dttable.Rows(0).Item("NETTTAKA"))
                    TXTNETTMTRS.Text = Val(dttable.Rows(0).Item("NETTMTRS"))
                    TXTREMARKS.Text = dttable.Rows(0).Item("REMARKS").ToString

                    TOTAL()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim IntResult As Integer
            Dim alParaval As New ArrayList


            alParaval.Add(Format(Convert.ToDateTime(DTGRDATE.Text.Trim).Date, "MM/dd/yyyy"))
            alParaval.Add(TXTBUYERNAME.Text.Trim)
            alParaval.Add(CMBOURGODOWN.Text.Trim)
            alParaval.Add(CMBNAME.Text.Trim)
            alParaval.Add(Val(CMBLOTNO.Text.Trim))

            If CHKDYEING.CheckState = CheckState.Checked Then alParaval.Add(1) Else alParaval.Add(0)

            alParaval.Add(cmbtrans.Text.Trim)
            alParaval.Add(TXTGREYQUALITY.Text.Trim)
            alParaval.Add(Val(TXTCHALLANTAKA.Text.Trim))
            alParaval.Add(Val(TXTCHALLANMTRS.Text.Trim))
            alParaval.Add(Val(TXTRETURNTAKA.Text.Trim))
            alParaval.Add(Val(TXTRETURNMTRS.Text.Trim))
            alParaval.Add(TXTRETURNDONO.Text.Trim)
            If DODATE.Text = "__/__/____" Then alParaval.Add("") Else alParaval.Add(Format(Convert.ToDateTime(DODATE.Text).Date, "MM/dd/yyyy"))

            alParaval.Add(Val(TXTDAMARAGE.Text.Trim))
            If DTRETURNDATE.Text = "__/__/____" Then alParaval.Add("") Else alParaval.Add(Format(Convert.ToDateTime(DTRETURNDATE.Text).Date, "MM/dd/yyyy"))

            alParaval.Add(Val(TXTSHORTAGE.Text.Trim))
            alParaval.Add(Val(TXTNETTTAKA.Text.Trim))
            alParaval.Add(Val(TXTNETTMTRS.Text.Trim))
            alParaval.Add(TXTREMARKS.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)

            Dim OBJGR As New ClsGoodsReturn
            OBJGR.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DT As DataTable = OBJGR.SAVE()
                TEMPGRNO = DT.Rows(0).Item(0)
                MsgBox("Details Added")

            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPGRNO)
                IntResult = OBJGR.UPDATE()
                EDIT = False
                MsgBox("Details Updated")

            End If

            'IF INVOICE IS CREATED THEN AUTO SAVE THE INVOICE
            'FOR THIS WE NEED CHALLANNO FIRST AND WITH RESPECT TO THAT CHALLAN WE WILL UPDATE INVOICE
            Dim OBJCMN As New ClsCommon
            Dim DTCHALLAN As DataTable = OBJCMN.search("CHALLAN_NO AS CHALLANNO ", "", " CHALLANMASTER LEFT OUTER JOIN LEDGERS ON CHALLAN_DELIVERYID = ACC_ID", " AND CHALLAN_LOTNO = " & Val(CMBLOTNO.Text.Trim) & " AND ISNULL(LEDGERS.ACC_CMPNAME,'') = '" & CMBNAME.Text.Trim & "' AND CHALLAN_YEARID = " & YearId)
            If DTCHALLAN.Rows.Count > 0 Then
                DT = OBJCMN.search("INVOICE_NO AS INVNO, REGISTER_NAME AS REGNAME", "", "INVOICEMASTER_DESC INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID ", " AND INVOICE_FROMNO = " & Val(DTCHALLAN.Rows(0).Item("CHALLANNO")) & " AND INVOICE_GRIDTYPE = 'CHALLAN' AND INVOICE_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    Dim OBJINV As New InvoiceMaster
                    OBJINV.edit = True
                    OBJINV.TEMPINVOICENO = Val(DT.Rows(0).Item("INVNO"))
                    OBJINV.TEMPREGNAME = DT.Rows(0).Item("REGNAME")
                    OBJINV.AUTOSAVEFROMGR = True
                    OBJINV.Show()

                    'UPDATE REPORTS IN SALE INVOICE
                    If ClientName <> "SASHWINKUMAR" Then DT = OBJCMN.Execute_Any_String("UPDATE INVOICEMASTER SET INVOICE_GRNO = " & Val(TEMPGRNO) & ", INVOICE_GRPCS = " & Val(TXTRETURNTAKA.Text.Trim) & ", INVOICE_GRMTRS = " & Val(TXTRETURNMTRS.Text.Trim) & ", INVOICE_GRSHORTAGE = " & Val(TXTSHORTAGE.Text.Trim) & " FROM INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID WHERE INVOICE_NO = " & Val(DT.Rows(0).Item("INVNO")) & " AND REGISTER_NAME = '" & DT.Rows(0).Item("REGNAME") & "' AND INVOICE_YEARID = " & YearId, "", "")
                End If
            End If


            'CLEAR()
            'SHOW NEXT BILL ON EDIT MODE DONT CLEAR
            Call toolnext_Click(sender, e)
            DTGRDATE.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True

        If DTGRDATE.Text = "__/__/____" Then
            EP.SetError(DTGRDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(DTGRDATE.Text) Then
                EP.SetError(DTGRDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        'DYEING IS NOT MANDATORY
        'If cmbname.Text.Trim.Length = 0 Then
        '    EP.SetError(cmbname, "Please Fill Name")
        '    bln = False
        'End If

        If CMBLOTNO.Text.Trim.Length = 0 Then
            EP.SetError(CMBLOTNO, "Please Select Lot No")
            bln = False
        End If

        If CMBOURGODOWN.Text.Trim.Length = 0 Then
            EP.SetError(CMBOURGODOWN, " Please Fill Our Godown Name ")
            bln = False
        End If

        If Val(TXTRETURNTAKA.Text) > Val(TXTCHALLANTAKA.Text) Then
            EP.SetError(TXTRETURNTAKA, " Return Taka Cannot be Greater than Challan Taka")
            bln = False
        End If

        If Val(TXTRETURNMTRS.Text) > Val(TXTCHALLANMTRS.Text) Then
            EP.SetError(TXTRETURNMTRS, " Return Mtrs. Cannot be Greater than Challan Mtrs.")
            bln = False
        End If

        If Val(TXTSHORTAGE.Text) > (Val(TXTCHALLANMTRS.Text) - Val(TXTRETURNMTRS.Text)) Then
            EP.SetError(TXTSHORTAGE, " Incorrect Shortage Value Entered")
            bln = False
        End If

        Return bln
    End Function

    Private Sub DTGRDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTGRDATE.GotFocus
        DTGRDATE.SelectAll()
    End Sub

    Private Sub DODATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DODATE.GotFocus
        DODATE.SelectAll()
    End Sub

    Private Sub DTRETURNDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTRETURNDATE.GotFocus
        DTRETURNDATE.SelectAll()
    End Sub

    Private Sub CMBOURGODOWN_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBOURGODOWN.Enter
        Try
            If CMBOURGODOWN.Text.Trim = "" Then fillGODOWN(CMBOURGODOWN, EDIT, " AND GODOWN_ISOUR='TRUE'")
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

    Private Sub CMBNAME_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE ='PROCESSOR'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, cmbcode, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "PROCESSOR")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTRETURNTAKA_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTRETURNTAKA.KeyPress
        numkeypress(e, TXTRETURNTAKA, Me)
    End Sub

    Private Sub TXTRETURNMTRS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTRETURNMTRS.KeyPress, TXTNETTMTRS.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub TXTCHALLANTAKA_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTCHALLANTAKA.Validating, TXTCHALLANMTRS.Validating, TXTRETURNTAKA.Validating, TXTRETURNMTRS.Validating, TXTSHORTAGE.Validating, TXTNETTTAKA.Validating, TXTNETTMTRS.Validating, TXTNETTMTRS.Validating
        TOTAL()
    End Sub

    Private Sub CMBNAME_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBNAME.Validated
        'If EDIT = False Then
        '    If CMBNAME.Text.Trim <> "" Then
        '        Dim OBJCMN As New ClsCommonMaster
        '        Dim dt As DataTable
        '        'dt = OBJCMN.search("DISTINCT ISNULL(CHALLANMASTER.CHALLAN_LOTNO, 0) AS LOTNO", "", "CHALLANMASTER INNER JOIN LEDGERS AS DELIVERYAT ON CHALLANMASTER.CHALLAN_DELIVERYID = DELIVERYAT.Acc_id", " AND DELIVERYAT .ACC_CMPNAME= '" & CMBNAME.Text.Trim & "' AND CHALLANMASTER.CHALLAN_YEARID = " & YearId)
        '        dt = OBJCMN.search("DISTINCT ISNULL(CHALLANMASTER.CHALLAN_LOTNO, 0) AS LOTNO", "", "CHALLANMASTER INNER JOIN LEDGERS AS DELIVERYAT ON CHALLANMASTER.CHALLAN_DELIVERYID = DELIVERYAT.Acc_id", " AND DELIVERYAT .ACC_CMPNAME= '" & CMBNAME.Text.Trim & "' AND CHALLANMASTER.CHALLAN_YEARID = " & YearId & " AND CHALLAN_LOTNO NOT IN (SELECT ISNULL(GOODSRETURNMASTER.GR_LOTNO, 0) AS LOTNO FROM  GOODSRETURNMASTER INNER JOIN LEDGERS ON GOODSRETURNMASTER.GR_DYEINGID = LEDGERS.Acc_id WHERE LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND GOODSRETURNMASTER.GR_YEARID = " & YearId & ")")
        '        If dt.Rows.Count > 0 Then
        '            dt.DefaultView.Sort = "LOTNO"
        '            CMBLOTNO.DataSource = dt
        '            CMBLOTNO.DisplayMember = "LOTNO"
        '            If EDIT = False Then CMBLOTNO.Text = ""
        '        End If
        '        CMBLOTNO.SelectAll()
        '    End If
        'End If
    End Sub

    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try

LINE1:
            TEMPGRNO = Val(TXTGRNO.Text) - 1
Line2:
            If TEMPGRNO > 0 Then

                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" GR_NO ", "", "  GOODSRETURNMASTER", " AND GR_NO = " & TEMPGRNO & " AND GOODSRETURNMASTER.GR_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    EDIT = True
                    GoodsReturn_Load(sender, e)
                Else
                    TEMPGRNO = Val(TEMPGRNO - 1)
                    GoTo Line2
                End If
            Else
                CLEAR()
                EDIT = False
            End If

            If TEMPGRNO > 1 Then
                TXTGRNO.Text = TEMPGRNO
                'GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            TXTGREYQUALITY.Clear()
LINE1:
            TEMPGRNO = Val(TXTGRNO.Text) + 1
            GETMAX_GR_NO()
            Dim MAXNO As Integer = TXTGRNO.Text.Trim
            CLEAR()
            If Val(TXTGRNO.Text) - 1 >= TEMPGRNO Then
                EDIT = True
                GoodsReturn_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If TXTGREYQUALITY.Text.Trim = "" And TEMPGRNO < MAXNO Then
                TXTGRNO.Text = TEMPGRNO
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
                TEMPGRNO = Val(tstxtbillno.Text)
                If TEMPGRNO > 0 Then
                    EDIT = True
                    GoodsReturn_Load(sender, e)
                Else
                    CLEAR()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim OBJGR As New GoodsReturnDetails
            OBJGR.MdiParent = MDIMain
            OBJGR.Show()
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
        Try
            If USERDELETE = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim OBJGRDEL As New ClsGoodsReturn
            Dim TEMPMSG As Integer = MsgBox("Wish To Delete?", MsgBoxStyle.YesNo)
            If TEMPMSG = vbYes Then

                Dim alParaval As New ArrayList
                alParaval.Add(TEMPGRNO)
                alParaval.Add(YearId)
                OBJGRDEL.alParaval = alParaval
                Dim INTRES As Integer = OBJGRDEL.Delete()
                MsgBox("Goods Return Deleted Successfully")
                CLEAR()
                EDIT = False

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBLOTNO_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBLOTNO.Validated
        Try
            If CMBLOTNO.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                'Dim DT As DataTable = OBJCMN.search("ISNULL((CHALLANMASTER_DESC.CHALLAN_TAKA - CHALLANMASTER_DESC.CHALLAN_OUTTAKA), 0) AS BALTAKA, ISNULL((CHALLANMASTER_DESC.CHALLAN_MTRS - CHALLANMASTER_DESC.CHALLAN_OUTMTRS), 0) AS BALMTRS, ISNULL(CHALLANMASTER_DESC.CHALLAN_NO, 0) AS FROMNO, ISNULL(CHALLANMASTER_DESC.CHALLAN_GRIDSRNO, 0) AS FROMSRNO", "", "CHALLANMASTER INNER JOIN CHALLANMASTER_DESC ON CHALLANMASTER.CHALLAN_NO = CHALLANMASTER_DESC.CHALLAN_NO AND CHALLANMASTER.CHALLAN_YEARID = CHALLANMASTER_DESC.CHALLAN_YEARID", "AND CHALLANMASTER.CHALLAN_LOTNO = '" & CMBLOTNO.Text.Trim & "' AND CHALLANMASTER.CHALLAN_YEARID = " & YearId)
                Dim DT As DataTable = OBJCMN.search("SUM(CHALLANMASTER.CHALLAN_TOTALTAKA) AS TAKA, SUM(CHALLANMASTER.CHALLAN_TOTALMTRS) AS MTRS, ISNULL(GREYQUALITYMASTER.GREY_NAME, '') AS GREY, ISNULL(CHALLANMASTER.CHALLAN_FORDYEING, 0) AS FORDYEING", "", "CHALLANMASTER INNER JOIN CHALLANMASTER_DESC ON CHALLANMASTER.CHALLAN_NO = CHALLANMASTER_DESC.CHALLAN_NO AND CHALLANMASTER.CHALLAN_YEARID = CHALLANMASTER_DESC.CHALLAN_YEARID INNER JOIN GREYQUALITYMASTER ON CHALLANMASTER_DESC.CHALLAN_QUALITYID = GREYQUALITYMASTER.GREY_ID INNER JOIN LEDGERS ON CHALLANMASTER.CHALLAN_DELIVERYID = LEDGERS.Acc_id", "AND CHALLANMASTER.CHALLAN_LOTNO = '" & CMBLOTNO.Text.Trim & "' AND LEDGERS.ACC_CMPNAME= '" & CMBNAME.Text.Trim & "' AND CHALLANMASTER.CHALLAN_YEARID = " & YearId & " GROUP BY ISNULL(GREYQUALITYMASTER.GREY_NAME, ''), ISNULL(CHALLANMASTER.CHALLAN_FORDYEING, 0)")

                If DT.Rows.Count > 0 Then
                    TXTCHALLANTAKA.Text = DT.Rows(0).Item("TAKA")
                    TXTCHALLANMTRS.Text = DT.Rows(0).Item("MTRS")
                    TXTGREYQUALITY.Text = DT.Rows(0).Item("GREY")
                    If Convert.ToBoolean(DT.Rows(0).Item("FORDYEING")) = False Then CHKDYEING.Checked = False Else CHKDYEING.Checked = True
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDPENDINGGR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDPENDINGGR.Click
        Try

            If (EDIT = True And USEREDIT = False And USERVIEW = False) Or (EDIT = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If


            Dim OBJPENDINGGR As New PendingGoodsReturn
            OBJPENDINGGR.ShowDialog()
            Dim DTTABLE As DataTable = OBJPENDINGGR.DT
            If DTTABLE.Rows.Count = 0 Then Exit Sub

            Dim OBJCMN As New ClsCommon
            'Dim DT As DataTable = OBJCMN.search("ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN, ISNULL(DELIVERYAT.Acc_cmpname, '') AS DELAT, ISNULL(CHALLANMASTER.CHALLAN_LOTNO, 0) AS LOTNO, ISNULL(CHALLANMASTER.CHALLAN_FORDYEING, 0) AS FORDYEING, ISNULL(GREYQUALITYMASTER.GREY_NAME, '') AS QUALITY, SUM(CHALLANMASTER_DESC.CHALLAN_TAKA) AS TAKA, SUM(CHALLANMASTER_DESC.CHALLAN_MTRS) AS MTRS", "", "CHALLANMASTER INNER JOIN CHALLANMASTER_DESC ON CHALLANMASTER.CHALLAN_NO = CHALLANMASTER_DESC.CHALLAN_NO AND CHALLANMASTER.CHALLAN_YEARID = CHALLANMASTER_DESC.CHALLAN_YEARID INNER JOIN GREYQUALITYMASTER ON CHALLANMASTER_DESC.CHALLAN_QUALITYID = GREYQUALITYMASTER.GREY_ID INNER JOIN GODOWNMASTER ON CHALLANMASTER.CHALLAN_GODOWNID = GODOWNMASTER.GODOWN_ID LEFT OUTER JOIN LEDGERS AS DELIVERYAT ON CHALLANMASTER.CHALLAN_DELIVERYID = DELIVERYAT.Acc_id", "AND CHALLANMASTER.CHALLAN_LOTNO = " & DTTABLE.Rows(0).Item("LOTNO") & " AND ISNULL(DELIVERYAT.ACC_CMPNAME,'')= '" & DTTABLE.Rows(0).Item("DELAT") & "' AND CHALLANMASTER.CHALLAN_YEARID= " & YearId & " GROUP BY ISNULL(DELIVERYAT.Acc_cmpname, ''), ISNULL(GREYQUALITYMASTER.GREY_NAME, ''), ISNULL(CHALLANMASTER.CHALLAN_LOTNO, 0),ISNULL(GODOWNMASTER.GODOWN_NAME, ''), ISNULL(CHALLANMASTER.CHALLAN_FORDYEING, 0)")
            Dim DT As DataTable = OBJCMN.search("*", "", "(SELECT ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN, ISNULL(DELIVERYAT.Acc_cmpname, '') AS DELAT, ISNULL(CHALLANMASTER.CHALLAN_LOTNO, 0) AS LOTNO, ISNULL(CHALLANMASTER.CHALLAN_FORDYEING, 0) AS FORDYEING, ISNULL(GREYQUALITYMASTER.GREY_NAME, '') AS QUALITY, SUM(CHALLANMASTER_DESC.CHALLAN_TAKA) AS TAKA, SUM(CHALLANMASTER_DESC.CHALLAN_MTRS) AS MTRS FROM CHALLANMASTER INNER JOIN CHALLANMASTER_DESC ON CHALLANMASTER.CHALLAN_NO = CHALLANMASTER_DESC.CHALLAN_NO AND CHALLANMASTER.CHALLAN_YEARID = CHALLANMASTER_DESC.CHALLAN_YEARID INNER JOIN GREYQUALITYMASTER ON CHALLANMASTER_DESC.CHALLAN_QUALITYID = GREYQUALITYMASTER.GREY_ID INNER JOIN GODOWNMASTER ON CHALLANMASTER.CHALLAN_GODOWNID = GODOWNMASTER.GODOWN_ID LEFT OUTER JOIN LEDGERS AS DELIVERYAT ON CHALLANMASTER.CHALLAN_DELIVERYID = DELIVERYAT.Acc_id WHERE CHALLANMASTER.CHALLAN_LOTNO = " & DTTABLE.Rows(0).Item("LOTNO") & " AND ISNULL(DELIVERYAT.ACC_CMPNAME,'')= '" & DTTABLE.Rows(0).Item("DELAT") & "' AND CHALLANMASTER.CHALLAN_YEARID= " & YearId & " GROUP BY ISNULL(DELIVERYAT.Acc_cmpname, ''), ISNULL(GREYQUALITYMASTER.GREY_NAME, ''), ISNULL(CHALLANMASTER.CHALLAN_LOTNO, 0),ISNULL(GODOWNMASTER.GODOWN_NAME, ''), ISNULL(CHALLANMASTER.CHALLAN_FORDYEING, 0) UNION ALL SELECT '' AS GODOWN, ISNULL(DELIVERYAT.Acc_cmpname, '') AS DELAT, ISNULL(STOCKMASTER_GREYPROCESSOR.SMGREYPROCESSOR_LOTNO, 0) AS LOTNO, 0 AS FORDYEING, ISNULL(GREYQUALITYMASTER.GREY_NAME, '') AS QUALITY, SUM(STOCKMASTER_GREYPROCESSOR.SMGREYPROCESSOR_TAKA) AS TAKA, SUM(STOCKMASTER_GREYPROCESSOR.SMGREYPROCESSOR_MTRS) AS MTRS FROM STOCKMASTER_GREYPROCESSOR INNER JOIN GREYQUALITYMASTER ON STOCKMASTER_GREYPROCESSOR.SMGREYPROCESSOR_GREYID = GREYQUALITYMASTER.GREY_ID LEFT OUTER JOIN LEDGERS AS DELIVERYAT ON STOCKMASTER_GREYPROCESSOR.SMGREYPROCESSOR_PROCESSORID = DELIVERYAT.Acc_id WHERE STOCKMASTER_GREYPROCESSOR.SMGREYPROCESSOR_LOTNO = " & DTTABLE.Rows(0).Item("LOTNO") & " AND ISNULL(DELIVERYAT.ACC_CMPNAME,'')= '" & DTTABLE.Rows(0).Item("DELAT") & "' AND STOCKMASTER_GREYPROCESSOR.SMGREYPROCESSOR_YEARID= " & YearId & " GROUP BY ISNULL(DELIVERYAT.Acc_cmpname, ''), ISNULL(GREYQUALITYMASTER.GREY_NAME, ''), ISNULL(STOCKMASTER_GREYPROCESSOR.SMGREYPROCESSOR_LOTNO, 0)) AS T", "")
            If DT.Rows.Count > 0 Then
                CMDPENDINGGR.Enabled = False

                CMBOURGODOWN.Text = DT.Rows(0).Item("GODOWN")
                CMBNAME.Text = DT.Rows(0).Item("DELAT")
                TXTBUYERNAME.Text = DTTABLE.Rows(0).Item("NAME")

                CMBLOTNO.DataSource = Nothing
                CMBLOTNO.Enabled = False
                CMBLOTNO.Items.Add(DT.Rows(0).Item("LOTNO"))
                CMBLOTNO.Text = DT.Rows(0).Item("LOTNO")

                If Convert.ToBoolean(DT.Rows(0).Item("FORDYEING")) = False Then CHKDYEING.Checked = False Else CHKDYEING.Checked = True

                TXTGREYQUALITY.Text = DT.Rows(0).Item("QUALITY")
                TXTCHALLANTAKA.Text = DT.Rows(0).Item("TAKA")
                TXTCHALLANMTRS.Text = DT.Rows(0).Item("MTRS")

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDPENDINGDATE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDPENDINGDATE.Click
        Try
            Dim DTTABLE As New DataTable
            Dim OBJRDATE As New PendingReturnDate
            OBJRDATE.ShowDialog()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DODATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DODATE.Validating
        Try
            If DODATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(DODATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DTRETURNDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DTRETURNDATE.Validating
        Try
            If DTRETURNDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(DTRETURNDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDPENDINGDONO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDPENDINGDONO.Click
        Try
            Dim OBJRDATE As New PendingReturnDONo
            OBJRDATE.ShowDialog()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GoodsReturn_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If ClientName = "NIRMALA" Then CHKDYEING.Enabled = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class