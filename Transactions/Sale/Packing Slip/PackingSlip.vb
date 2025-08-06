
Imports BL

Public Class PackingSlip

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public EDIT As Boolean
    Public TEMPPSNO As Integer
    Dim DTPCS As New DataTable
    Dim MANUALPSNO As Boolean

    Private Sub cmdexit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdclear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        CLEAR()
        EDIT = False
        CMBNAME.Focus()
    End Sub

    Sub CLEAR()
        DTPCS.Rows.Clear()

        TXTPSNO.Clear()
        If MANUALPSNO = True Then
            TXTPSNO.ReadOnly = False
            TXTPSNO.BackColor = Color.LemonChiffon
        Else
            TXTPSNO.ReadOnly = True
            TXTPSNO.BackColor = Color.Linen
        End If

        If ClientName <> "STC" Then DTPSDATE.Text = Mydate
        CMBOURGODOWN.Text = GETDEFAULTGODOWN()
        CMBFROMCITY.Text = ""
        CMBTOCITY.Text = ""
        CMBNAME.Text = ""
        CMBFROMNAME.Text = ""
        CMBPACKERNAME.Text = ""
        CMBQUALITY.Text = ""
        TXTSTOCKTAKA.Clear()
        TXTSTOCKMTRS.Clear()
        CMBTRANS.Text = ""

        TXTCHALLANNO.Clear()
        DTCHALLANDATE.Clear()
        TXTLRNO.Clear()
        LRDATE.Clear()

        TXTREMARKS.Clear()
        If ClientName <> "STC" Then TXTPCS.Text = 1
        TXTMTRS.Clear()
        TXTWT.Clear()
        TXTTP.Clear()
        GRIDPS.RowCount = 0

        tstxtbillno.Clear()

        EP.Clear()
        lbllocked.Visible = False
        PBlock.Visible = False

        TXTREMARKS.Clear()


        GETMAX_PACKINGSLIP_NO()

        LBLTOTALTAKA.Text = 0
        LBLTOTALTP.Text = 0
        LBLAVGWT.Text = 0
        LBLTOTALMTRS.Text = 0.0


        GRIDDOUBLECLICK = False
        TXTSRNO.Text = 1

    End Sub

    Sub TOTAL()
        Try
            LBLTOTALTAKA.Text = 0
            LBLTOTALTP.Text = 0
            LBLAVGWT.Text = 0
            LBLTOTALMTRS.Text = 0.0
            Dim TEMPMTRSFORAVGROW As Double = 0.0
            For Each ROW As DataGridViewRow In GRIDPS.Rows
                LBLTOTALMTRS.Text = Format(Val(LBLTOTALMTRS.Text) + Val(ROW.Cells(GMTRS.Index).EditedFormattedValue), "0.00")
                LBLAVGWT.Text += Val(ROW.Cells(GWT.Index).Value)
                LBLTOTALTAKA.Text = Format(Val(LBLTOTALTAKA.Text) + Val(ROW.Cells(GPCS.Index).EditedFormattedValue), "0")
                If Val(ROW.Cells(GWT.Index).Value) > 0 Then TEMPMTRSFORAVGROW += Val(ROW.Cells(GMTRS.Index).Value)
                If Val(ROW.Cells(GTP.Index).Value) > 0 Then LBLTOTALTP.Text += 1
            Next

            LBLAVGWT.Text = Format((Val(Val(LBLAVGWT.Text) / TEMPMTRSFORAVGROW)) * 100, "0.00")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETMAX_PACKINGSLIP_NO()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax("ISNULL(MAX(PS_NO),0)+1", "PACKINGSLIP", "AND PS_YEARID=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTPSNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub PackingSlip_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        If CMBFROMCITY.Text = "" Then fillCITY(CMBFROMCITY, EDIT)
        If CMBTOCITY.Text = "" Then fillCITY(CMBTOCITY, EDIT)
        If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "and (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS') AND ACC_TYPE = 'ACCOUNTS' AND ACC_SUBTYPE ='ACCOUNTS'")
        If CMBFROMNAME.Text.Trim = "" Then fillname(CMBFROMNAME, EDIT, "and (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS') AND ACC_TYPE = 'ACCOUNTS' AND ACC_SUBTYPE ='ACCOUNTS'")
        If CMBPACKERNAME.Text.Trim = "" Then fillname(CMBPACKERNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND ACC_SUBTYPE = 'ACCOUNTS'")
        If CMBTRANS.Text = "" Then fillname(CMBTRANS, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'TRANSPORT'")

        'DO NOT FILL ALL QUALITY FILL ONLY THOSE WHICH ARE PRESENT IN GREYSTOCK
        'If CMBQUALITY.Text.Trim = "" Then FILLGREY(CMBQUALITY, EDIT)
        FILLGREYSTOCKQUALITY()
    End Sub

    Sub FILLGREYSTOCKQUALITY()
        Try
            If CMBQUALITY.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search(" EFFECTGREYQUALITY ", "", " GREYSTOCK ", " AND YEARID = " & YearId & " GROUP BY EFFECTGREYQUALITY HAVING SUM(MTRS) > 0")
                CMBQUALITY.DataSource = dt
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "EFFECTGREYQUALITY"
                    CMBQUALITY.DisplayMember = "EFFECTGREYQUALITY"
                    If EDIT = False Then CMBQUALITY.Text = ""
                End If
                CMBQUALITY.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PackingSlip_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'PACKING SLIP'")
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
                Dim OBJPS As New ClsPackingSlip

                OBJPS.alParaval.Add(TEMPPSNO)
                OBJPS.alParaval.Add(YearId)
                dttable = OBJPS.SELECTPS()

                If dttable.Rows.Count > 0 Then
                    CMBNAME.Focus()

                    TXTPSNO.Text = TEMPPSNO
                    TXTPSNO.ReadOnly = True
                    TXTPSNO.BackColor = Color.Linen

                    DTPSDATE.Text = dttable.Rows(0).Item("PSDATE")

                    TXTCHALLANNO.Text = dttable.Rows(0).Item("CHALLANNO")
                    If dttable.Rows(0).Item("CHALLANDATE") <> "" Then DTCHALLANDATE.Text = Format(Convert.ToDateTime(dttable.Rows(0).Item("CHALLANDATE")).Date, "dd/MM/yyyy")
                    TXTLRNO.Text = dttable.Rows(0).Item("LRNO")
                    If dttable.Rows(0).Item("LRDATE") <> "" Then LRDATE.Text = Format(Convert.ToDateTime(dttable.Rows(0).Item("LRDATE")).Date, "dd/MM/yyyy")

                    CMBOURGODOWN.Text = dttable.Rows(0).Item("GODOWN").ToString
                    CMBFROMCITY.Text = dttable.Rows(0).Item("FROMCITY").ToString
                    CMBTOCITY.Text = dttable.Rows(0).Item("TOCITY").ToString

                    CMBNAME.Text = dttable.Rows(0).Item("NAME").ToString
                    CMBFROMNAME.Text = dttable.Rows(0).Item("FROMNAME").ToString
                    CMBPACKERNAME.Text = dttable.Rows(0).Item("PACKERNAME").ToString
                    CMBQUALITY.Text = dttable.Rows(0).Item("QUALITY").ToString

                    CMBTRANS.Text = dttable.Rows(0).Item("TRANSNAME").ToString
                    TXTREMARKS.Text = dttable.Rows(0).Item("REMARKS").ToString


                    'ITEM GRID
                    For Each ROW As DataRow In dttable.Rows
                        GRIDPS.Rows.Add(Val(ROW("GRIDSRNO")), Format(Val(ROW("TAKA")), "0"), Format(Val(ROW("MTRS")), "0.00"), Val(ROW("WT")), Val(ROW("TP")))
                    Next

                    If Convert.ToBoolean(dttable.Rows(0).Item("DONE")) = True Then
                        lbllocked.Visible = True
                        PBlock.Visible = True
                    End If

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

            If TXTPSNO.ReadOnly = False Then
                alParaval.Add(Val(TXTPSNO.Text.Trim))
            Else
                alParaval.Add(0)
            End If
            alParaval.Add(Format(Convert.ToDateTime(DTPSDATE.Text.Trim).Date, "MM/dd/yyyy"))
            alParaval.Add(TXTLRNO.Text.Trim)
            If LRDATE.Text <> "__/__/____" Then alParaval.Add(LRDATE.Text.Trim) Else alParaval.Add("")
            alParaval.Add(CMBOURGODOWN.Text.Trim)
            alParaval.Add(CMBFROMCITY.Text.Trim)
            alParaval.Add(CMBTOCITY.Text.Trim)
            alParaval.Add(CMBNAME.Text.Trim)
            alParaval.Add(CMBFROMNAME.Text.Trim)
            alParaval.Add(CMBPACKERNAME.Text.Trim)
            alParaval.Add(CMBQUALITY.Text.Trim)
            alParaval.Add(CMBTRANS.Text.Trim)


            alParaval.Add(Val(LBLTOTALTAKA.Text.Trim))
            alParaval.Add(Val(LBLTOTALMTRS.Text.Trim))
            alParaval.Add(Val(LBLAVGWT.Text.Trim))
            alParaval.Add(Val(LBLTOTALTP.Text.Trim))
            alParaval.Add(TXTREMARKS.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)

            Dim SRNO As String = ""
            Dim TAKA As String = ""
            Dim MTRS As String = ""
            Dim WT As String = ""
            Dim TP As String = ""


            For Each row As Windows.Forms.DataGridViewRow In GRIDPS.Rows
                If row.Cells(GSRNO.Index).Value <> Nothing Then
                    If SRNO = "" Then
                        SRNO = Val(row.Cells(GSRNO.Index).Value)
                        TAKA = Format(Val(row.Cells(GPCS.Index).Value), "0")
                        MTRS = Format(Val(row.Cells(GMTRS.Index).Value), "0.00")
                        WT = Format(Val(row.Cells(GWT.Index).Value), "0.00")
                        TP = Format(Val(row.Cells(GTP.Index).Value), "0")
                    Else

                        SRNO = SRNO & "|" & row.Cells(GSRNO.Index).Value
                        TAKA = TAKA & "|" & row.Cells(GPCS.Index).Value
                        MTRS = MTRS & "|" & Format(Val(row.Cells(GMTRS.Index).Value), "0.00")
                        WT = WT & "|" & Format(Val(row.Cells(GWT.Index).Value), "0.00")
                        TP = TP & "|" & Format(Val(row.Cells(GTP.Index).Value), "0")
                    End If
                End If
            Next

            alParaval.Add(SRNO)
            alParaval.Add(TAKA)
            alParaval.Add(MTRS)
            alParaval.Add(WT)
            alParaval.Add(TP)

            Dim OBJPS As New ClsPackingSlip
            OBJPS.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DT As DataTable = OBJPS.SAVE()
                TEMPPSNO = DT.Rows(0).Item(0)
                MsgBox("Details Added")

            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPPSNO)
                IntResult = OBJPS.UPDATE()
                EDIT = False
                MsgBox("Details Updated")

            End If

            'CLEAR()
            'SHOW NEXT BILL ON EDIT MODE DONT CLEAR
            Call toolnext_Click(sender, e)
            DTPSDATE.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True


        If DTPSDATE.Text = "__/__/____" Then
            EP.SetError(DTPSDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(DTPSDATE.Text) Then
                EP.SetError(DTPSDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        If ClientName <> "STC" Then
            If CMBNAME.Text.Trim.Length = 0 Then
                EP.SetError(CMBNAME, "Please Fill Name")
                bln = False
            End If
        End If

        If Val(TXTPSNO.Text.Trim) = 0 Then
            EP.SetError(TXTPSNO, "Enter Packing Slip No")
            bln = False
        End If

        If CMBOURGODOWN.Text.Trim.Length = 0 Then
            EP.SetError(CMBOURGODOWN, " Please Fill Our Godown Name ")
            bln = False
        End If

        If CMBQUALITY.Text.Trim.Length = 0 Then
            EP.SetError(CMBQUALITY, " Please Select Quality")
            bln = False
        End If

        If ClientName <> "JASHOK" Then
            If CMBTRANS.Text.Trim.Length = 0 Then
                EP.SetError(CMBTRANS, " Please Select Transport")
                bln = False
            End If
        End If

        Dim OBJCMN As New ClsCommon
        If MANUALPSNO = True Then
            If TXTPSNO.Text <> "" And CMBNAME.Text.Trim <> "" And EDIT = False Then
                Dim dttable As DataTable = OBJCMN.search(" ISNULL(PS_NO, '') AS PSNO", "", " PACKINGSLIP ", "  AND PS_NO = " & Val(TXTPSNO.Text.Trim) & " AND PS_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    EP.SetError(TXTPSNO, "Packing Slip No Already Exist")
                    bln = False
                End If
            End If
        End If

        If lbllocked.Visible = True Then
            EP.SetError(lbllocked, "Unable to Modify, Entry Locked")
            bln = False
        End If

        If GRIDPS.RowCount = 0 Then
            EP.SetError(CMBTRANS, "Select Stock")
            bln = False
        End If

        For Each row As DataGridViewRow In GRIDPS.Rows
            If Val(row.Cells(GMTRS.Index).Value) = 0 Then
                EP.SetError(CMBTRANS, "Mtrs Cannot be 0")
                bln = False
                Exit For
            End If
        Next


        'STOCK VALIDATE FROM GREYSTOCK
        Dim BALPCS As Double = 0
        Dim BALMTRS As Double = 0
        Dim DT As DataTable = OBJCMN.search("ISNULL(SUM(PCS),0) AS PCS, ISNULL(SUM(MTRS),0) AS MTRS", "", "GREYSTOCK", " AND GODOWN = '" & CMBOURGODOWN.Text.Trim & "' AND EFFECTGREYQUALITY = '" & CMBQUALITY.Text.Trim & "' AND YEARID = " & YearId)
        If DT.Rows.Count > 0 Then
            BALPCS = Val(DT.Rows(0).Item("PCS"))
            BALMTRS = Val(DT.Rows(0).Item("MTRS"))
        End If
        If EDIT = True Then
            DT = OBJCMN.search("PS_TOTALMTRS AS MTRS", "", "PACKINGSLIP INNER JOIN GREYQUALITYMASTER ON GREY_ID  = PS_QUALITYID  ", " AND GREY_NAME = '" & CMBQUALITY.Text.Trim & "' AND PS_NO = " & TEMPPSNO & " AND PS_YEARID = " & YearId)
            If DT.Rows.Count > 0 Then
                BALMTRS = BALMTRS + Val(DT.Rows(0).Item("MTRS"))
            End If
        End If
        If Val(LBLTOTALMTRS.Text.Trim) > BALMTRS Then
            EP.SetError(TXTSTOCKMTRS, "Stock Not Present, Only " & Val(BALMTRS) & " allowed")
            bln = False
        End If

        Return bln
    End Function

    Private Sub DTPSDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTPSDATE.GotFocus
        DTPSDATE.SelectAll()
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
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, " and (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS') AND ACC_TYPE = 'ACCOUNTS' AND ACC_SUBTYPE = 'ACCOUNTS' ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBNAME.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS') AND LEDGERS.ACC_TYPE='ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'ACCOUNTS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, cmbcode, e, Me, TXTADD, "AND (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS') AND LEDGERS.ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'ACCOUNTS'", "SUNDRY DEBTORS", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBFROMNAME_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBFROMNAME.Enter
        Try
            If CMBFROMNAME.Text.Trim = "" Then fillname(CMBFROMNAME, EDIT, " and (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS') AND ACC_TYPE = 'ACCOUNTS' AND ACC_SUBTYPE = 'ACCOUNTS' ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBFROMNAME_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBFROMNAME.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS') AND LEDGERS.ACC_TYPE='ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'ACCOUNTS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBFROMNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBFROMNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBFROMNAME.Validating
        Try
            If CMBFROMNAME.Text.Trim <> "" Then namevalidate(CMBFROMNAME, cmbcode, e, Me, TXTADD, "AND (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS') AND LEDGERS.ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'ACCOUNTS'", "SUNDRY DEBTORS", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBTRANS.Enter
        Try
            If CMBTRANS.Text.Trim = "" Then fillname(CMBTRANS, EDIT, "AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'AND LEDGERS.ACC_TYPE= 'TRANSPORT'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBTRANS.Validating
        Try
            If CMBTRANS.Text.Trim <> "" Then namevalidate(CMBTRANS, cmbcode, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'", "Sundry Creditors", "TRANSPORT")
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
            GRIDPS.RowCount = 0
LINE1:
            TEMPPSNO = Val(TXTPSNO.Text) - 1
Line2:
            If TEMPPSNO > 0 Then

                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" PS_NO ", "", "  PACKINGSLIP", " AND PS_NO = " & TEMPPSNO & " AND PACKINGSLIP.PS_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    EDIT = True
                    PackingSlip_Load(sender, e)
                Else
                    TEMPPSNO = Val(TEMPPSNO - 1)
                    GoTo Line2
                End If
            Else
                CLEAR()
                EDIT = False
            End If

            If GRIDPS.RowCount = 0 And TEMPPSNO > 1 Then
                TXTPSNO.Text = TEMPPSNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            GRIDPS.RowCount = 0
LINE1:
            TEMPPSNO = Val(TXTPSNO.Text) + 1
            GETMAX_PACKINGSLIP_NO()
            Dim MAXNO As Integer = TXTPSNO.Text.Trim
            CLEAR()
            If Val(TXTPSNO.Text) - 1 >= TEMPPSNO Then
                EDIT = True
                PackingSlip_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDPS.RowCount = 0 And TEMPPSNO < MAXNO Then
                TXTPSNO.Text = TEMPPSNO
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
                GRIDPS.RowCount = 0
                TEMPPSNO = Val(tstxtbillno.Text)
                If TEMPPSNO > 0 Then
                    EDIT = True
                    PackingSlip_Load(sender, e)
                Else
                    CLEAR()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
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
                    MsgBox("Unable to Delete, PackingSlip Locked", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                If MsgBox("Delete PackingSlip?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(TEMPPSNO)
                    alParaval.Add(YearId)

                    Dim OBJSO As New ClsPackingSlip()
                    OBJSO.alParaval = alParaval
                    IntResult = OBJSO.DELETE()
                    MsgBox("PackingSlip Deleted")
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

    Private Sub GRIDPS_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDPS.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDPS.RowCount > 0 Then
                'dont allow user if any of the grid line is in edit mode.....
                'cmbitemname.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block
                GRIDPS.Rows.RemoveAt(GRIDPS.CurrentRow.Index)
                getsrno(GRIDPS)
                TOTAL()


            ElseIf e.KeyCode = Keys.F8 Then
                'FILLPCSDETAILS()
                Dim OBJPCS As New PcsWiseDetails
                OBJPCS.DT = DTPCS
                OBJPCS.FROMNO = Val(TXTPSNO.Text.Trim)
                OBJPCS.MAINLINENO = GRIDPS.CurrentRow.Cells(GSRNO.Index).Value
                OBJPCS.ShowDialog()

                ''GET TOTAL MTRS AND ADD IN GRID
                If DTPCS.Rows.Count > 0 Then
                    GRIDPS.Rows(GRIDPS.CurrentRow.Index).Cells(GMTRS.Index).Value = 0
                    For Each DTROW As DataRow In DTPCS.Rows
                        If DTROW("MAINLINENO") = GRIDPS.CurrentRow.Cells(GSRNO.Index).Value Then
                            Dim RESULT() As DataRow = DTPCS.Select("MAINLINENO = " & GRIDPS.CurrentRow.Cells(GSRNO.Index).Value)
                            GRIDPS.Rows(GRIDPS.CurrentRow.Index).Cells(GMTRS.Index).Value = Format(Val(GRIDPS.Rows(GRIDPS.CurrentRow.Index).Cells(GMTRS.Index).Value) + Val(DTROW("MTRS")), "0.000")
                            GRIDPS.Rows(GRIDPS.CurrentRow.Index).Cells(GWT.Index).Value = RESULT.Length
                        End If
                    Next
                    getsrno(GRIDPS)
                    TOTAL()
                End If
            End If


        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub FILLPCSDETAILS()

        Dim OBJPCS As New PcsWiseDetails
        OBJPCS.DT = DTPCS
        OBJPCS.MAINLINENO = Val(TXTSRNO.Text.Trim)
        OBJPCS.ShowDialog()


        ''GET TOTAL MTRS AND ADD IN GRID
        If DTPCS.Rows.Count > 0 Then
            TXTMTRS.Text = 0.0
            For Each DTROW As DataRow In DTPCS.Rows
                If DTROW("MAINLINENO") = Val(TXTSRNO.Text.Trim) Then
                    Dim RESULT() As DataRow = DTPCS.Select("MAINLINENO = " & Val(TXTSRNO.Text.Trim))
                    TXTMTRS.Text = Format(Val(TXTMTRS.Text) + Val(DTROW("MTRS")), "0.000")
                    TXTWT.Text = RESULT.Length
                End If
            Next
            getsrno(GRIDPS)
            TOTAL()
        End If
        'DTPCS.Rows.Clear()
    End Sub

    Private Sub GRIDPS_CellValidating(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles GRIDPS.CellValidating
        Try
            Dim colNum As Integer = GRIDPS.Columns(e.ColumnIndex).Index
            If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return

            Select Case colNum

                Case GMTRS.Index, GWT.Index ', gcount.Index
                    Dim dDebit As Decimal
                    Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

                    If bValid Then
                        If GRIDPS.CurrentCell.Value = Nothing Then GRIDPS.CurrentCell.Value = "0.000"
                        GRIDPS.CurrentCell.Value = Convert.ToDecimal(GRIDPS.Item(colNum, e.RowIndex).Value)
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

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim OBJPS As New PackingSlipDetails
            OBJPS.MdiParent = MDIMain
            OBJPS.Show()
        Catch EX As Exception
            Throw EX
        End Try
    End Sub

    Private Sub CMBPACKERNAME_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBPACKERNAME.Enter
        Try
            If CMBPACKERNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE ='ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPACKERNAME_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBPACKERNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'ACCOUNTS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBPACKERNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPACKERNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBPACKERNAME.Validating
        Try
            If CMBPACKERNAME.Text.Trim <> "" Then namevalidate(CMBPACKERNAME, cmbcode, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'  AND LEDGERS.ACC_SUBTYPE = 'ACCOUNTS'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTMTRS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTMTRS.KeyPress, TXTWT.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub TXTTP_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTTP.KeyPress, TXTPCS.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTTP_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTTP.Validating
        Try
            If ClientName = "JASHOK" Then
                If GRIDDOUBLECLICK = False And GRIDPS.RowCount = 20 Then
                    MsgBox("Only 20 Entries allowed", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End If
            If Val(TXTMTRS.Text) > 0 And Val(TXTPCS.Text.Trim) > 0 Then FILLGRID() Else MsgBox("Enter Proper Details !")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()

        GRIDPS.Enabled = True

        If GRIDDOUBLECLICK = False Then
            GRIDPS.Rows.Add(Val(TXTSRNO.Text.Trim), Val(TXTPCS.Text.Trim), Format(Val(TXTMTRS.Text.Trim), "0.00"), Format(Val(TXTWT.Text.Trim), "0.00"), Format(Val(TXTTP.Text.Trim), "0"))
            getsrno(GRIDPS)
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDPS.Item(GSRNO.Index, TEMPROW).Value = Val(TXTSRNO.Text.Trim)
            GRIDPS.Item(GPCS.Index, TEMPROW).Value = Val(TXTPCS.Text.Trim)
            GRIDPS.Item(GMTRS.Index, TEMPROW).Value = Format(Val(TXTMTRS.Text.Trim), "0.00")
            GRIDPS.Item(GWT.Index, TEMPROW).Value = Format(Val(TXTWT.Text.Trim), "0.00")
            GRIDPS.Item(GTP.Index, TEMPROW).Value = Format(Val(TXTTP.Text.Trim), "0")
            GRIDDOUBLECLICK = False
        End If
        TOTAL()
        GRIDPS.FirstDisplayedScrollingRowIndex = GRIDPS.RowCount - 1

        TXTSRNO.Clear()
        If ClientName <> "STC" Then TXTPCS.Text = 1
        TXTTP.Clear()
        TXTWT.Clear()
        TXTMTRS.Clear()
        TXTMTRS.Focus()
        If GRIDPS.RowCount > 0 Then TXTSRNO.Text = Val(GRIDPS.Rows(GRIDPS.RowCount - 1).Cells(0).Value) + 1 Else TXTSRNO.Text = 1

    End Sub

    Private Sub GRIDPS_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDPS.CellDoubleClick
        If e.RowIndex >= 0 And GRIDPS.Item(GSRNO.Index, e.RowIndex).Value <> Nothing Then

            GRIDDOUBLECLICK = True
            TXTSRNO.Text = GRIDPS.Item(GSRNO.Index, e.RowIndex).Value.ToString
            TXTPCS.Text = Val(GRIDPS.Item(GPCS.Index, e.RowIndex).Value)
            TXTMTRS.Text = Val(GRIDPS.Item(GMTRS.Index, e.RowIndex).Value)
            TXTWT.Text = Val(GRIDPS.Item(GWT.Index, e.RowIndex).Value)
            TXTTP.Text = Val(GRIDPS.Item(GTP.Index, e.RowIndex).Value)

            TEMPROW = e.RowIndex
            TXTPCS.Focus()
        End If
    End Sub

    Private Sub CMBQUALITY_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBQUALITY.Enter
        Try
            If CMBQUALITY.Text.Trim = "" Then FILLGREYSTOCKQUALITY()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBQUALITY.Validated
        Try
            TXTSTOCKTAKA.Clear()
            TXTSTOCKMTRS.Clear()

            If CMBQUALITY.Text.Trim <> "" Then
                'GET GREYQUALITY STOCK
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("ISNULL(SUM(PCS),0) AS PCS, ISNULL(SUM(MTRS),0) AS MTRS ", "", " GREYSTOCK ", " AND GODOWN = '" & CMBOURGODOWN.Text.Trim & "' AND EFFECTGREYQUALITY = '" & CMBQUALITY.Text.Trim & "' AND YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TXTSTOCKTAKA.Text = Val(DT.Rows(0).Item("PCS"))
                    TXTSTOCKMTRS.Text = Format(Val(DT.Rows(0).Item("MTRS")), "0.00")
                End If
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

    Private Sub TXTSRNO_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTSRNO.GotFocus
        If GRIDDOUBLECLICK = False Then
            If GRIDPS.RowCount > 0 Then
                TXTSRNO.Text = Val(GRIDPS.Rows(GRIDPS.RowCount - 1).Cells(0).Value) + 1
            Else
                TXTSRNO.Text = 1
            End If
        End If
    End Sub

    Private Sub CMBFROMCITY_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBFROMCITY.Enter
        Try
            If CMBFROMCITY.Text = "" Then fillCITY(CMBFROMCITY, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBFROMCITY_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBFROMCITY.Validating
        Try
            If CMBFROMCITY.Text.Trim <> "" Then CITYVALIDATE(CMBFROMCITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTOCITY_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBTOCITY.Enter
        Try
            If CMBTOCITY.Text.Trim = "" Then fillCITY(CMBTOCITY, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTOCITY_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBTOCITY.Validating
        Try
            If CMBTOCITY.Text.Trim <> "" Then CITYVALIDATE(CMBTOCITY, e, Me)
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

    Sub PRINTREPORT()
        Try
            If MsgBox("Wish to Print Packing Slip?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            Dim OBJPC As New ChallanDesign
            OBJPC.WHERECLAUSE = "{PACKINGSLIP.PS_NO} = " & TEMPPSNO & " AND {PACKINGSLIP.PS_YEARID} = " & YearId
            OBJPC.FRMSTRING = "PACKINGSLIP"
            OBJPC.MdiParent = MDIMain
            OBJPC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PackingSlip_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If ClientName = "STC" Then
            MANUALPSNO = True
            TXTLRNO.ReadOnly = False
            LRDATE.Enabled = True
            TXTLRNO.TabStop = True
            LRDATE.TabStop = True
            TXTPSNO.ReadOnly = False
            TXTPSNO.BackColor = Color.LemonChiffon
        End If

        If ClientName = "NIRMALA" Then
            MANUALPSNO = True
            TXTPSNO.ReadOnly = False
            TXTPSNO.BackColor = Color.LemonChiffon
        End If
    End Sub

    Private Sub DTPSDATE_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTPSDATE.Validated
        Try
            If ClientName = "STC" And DTPSDATE.Text.Trim <> "__/__/____" Then
                LRDATE.Text = DTPSDATE.Text
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DTPSDATE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DTPSDATE.Validating
        Try
            If DTPSDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(DTPSDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTPSNO_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTPSNO.KeyPress
        numkeypress(e, TXTPSNO, Me)
    End Sub

    Private Sub TXTPSNO_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTPSNO.Validating
        Try
            If (Val(TXTPSNO.Text.Trim) <> 0 And EDIT = False) Or (EDIT = True And TEMPPSNO <> Val(TXTPSNO.Text.Trim)) Then
                Dim OBJCMN As New ClsCommon
                Dim dttable As DataTable = OBJCMN.search(" ISNULL(PS_NO,0)  AS PSNO", "", " PACKINGSLIP ", "  AND PS_NO=" & Val(TXTPSNO.Text.Trim) & " AND PS_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    MsgBox("Packing Slip No Already Exist")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class