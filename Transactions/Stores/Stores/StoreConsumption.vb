
Imports BL

Public Class StoreConsumption

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public EDIT As Boolean
    Public TEMPCONSUMPTIONNO As Integer
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        Try
            clear()
            EDIT = False
            CONSUMPTIONDATE.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub clear()
        Try
            CONSUMPTIONDATE.Text = Mydate
            tstxtbillno.Clear()

            CMBNAME.Text = ""
            CMBTRANS.Text = ""
            CMBITEMNAME.Text = ""
            TXTQTY.Clear()
            GRIDCONSUMPTION.RowCount = 0
            EP.Clear()
            txtremarks.Clear()

            getmax_BILL_no()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub getmax_BILL_no()
        Try
            Dim DTTABLE As DataTable = getmax(" isnull(max(STORE_NO),0) + 1 ", "  STORECONSUMPTION ", " AND STORE_YEARID = " & YearId)
            If DTTABLE.Rows.Count > 0 Then TXTCONSUMPTIONNO.Text = DTTABLE.Rows(0).Item(0)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub STORECONSUMPTION_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        Try
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS' AND (ACC_SUBTYPE = 'SIZER' OR ACC_SUBTYPE = 'WEAVER' OR ACC_SUBTYPE = 'WARPER') ")
            If CMBGODOWN.Text.Trim = "" Then fillGODOWN(CMBGODOWN, False, " AND GODOWN_ISOUR = 'True'")
            If CMBTRANS.Text.Trim = "" Then fillname(CMBTRANS, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'TRANSPORT'")
            If CMBITEMNAME.Text.Trim = "" Then FILLSTOREITEMNAME(CMBITEMNAME)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub STORECONSUMPTION_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'STORES'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor

            FILLCMB()
            clear()

            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim OBJCONSUME As New ClsStoreConsumption
                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(TEMPCONSUMPTIONNO)
                ALPARAVAL.Add(YearId)
                OBJCONSUME.alParaval = ALPARAVAL
                Dim dttable As DataTable = OBJCONSUME.SELECTCONSUMPTION()

                If dttable.Rows.Count > 0 Then
                    For Each dr As DataRow In dttable.Rows

                        TXTCONSUMPTIONNO.Text = TEMPCONSUMPTIONNO
                        CONSUMPTIONDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")

                        CMBNAME.Text = Convert.ToString(dr("NAME").ToString)
                        CMBTRANS.Text = dr("TRANSNAME").ToString

                        txtremarks.Text = Convert.ToString(dr("REMARKS").ToString)

                        GRIDCONSUMPTION.Rows.Add(dr("ITEMNAME"), Val(dr("QTY")))
                    Next
                    total()
                Else
                    EDIT = False
                    clear()
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

            alParaval.Add(Format(Convert.ToDateTime(CONSUMPTIONDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBNAME.SelectedValue)
            alParaval.Add(CMBGODOWN.SelectedValue)
            alParaval.Add(CMBTRANS.SelectedValue)
            alParaval.Add(Val(LBLTOTALQTY.Text.Trim))
            alParaval.Add(txtremarks.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)


            Dim ITEMNAME As String = ""
            Dim QTY As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDCONSUMPTION.Rows
                If row.Cells(0).Value <> Nothing Then
                    If ITEMNAME = "" Then
                        ITEMNAME = row.Cells(GITEMNAME.Index).Value.ToString
                        QTY = Val(row.Cells(GQTY.Index).Value)
                    Else
                        ITEMNAME = ITEMNAME & "|" & row.Cells(GITEMNAME.Index).Value
                        QTY = QTY & "|" & Val(row.Cells(GQTY.Index).Value)
                    End If
                End If
            Next

            alParaval.Add(ITEMNAME)
            alParaval.Add(QTY)

            Dim OBJCONSUME As New ClsStoreConsumption
            OBJCONSUME.alParaval = alParaval
            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DTTABLE As DataTable = OBJCONSUME.SAVE()
                TEMPCONSUMPTIONNO = DTTABLE.Rows(0).Item(0)
                MessageBox.Show("Details Added")

            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPCONSUMPTIONNO)
                IntResult = OBJCONSUME.UPDATE()
                MessageBox.Show("Details Updated")
                EDIT = False
            End If

            clear()
            CONSUMPTIONDATE.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try

    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True

        If CONSUMPTIONDATE.Text = "__/__/____" Then
            EP.SetError(CONSUMPTIONDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(CONSUMPTIONDATE.Text) Then
                EP.SetError(CONSUMPTIONDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        If CMBNAME.Text.Trim.Length = 0 And CMBGODOWN.Text.Trim.Length = 0 Then
            EP.SetError(CMBNAME, " Please Fill Supplier / Godown Name ")
            bln = False
        End If

        If CMBNAME.Text.Trim.Length <> 0 And CMBGODOWN.Text.Trim.Length <> 0 Then
            EP.SetError(CMBNAME, " Please Fill Either Supplier / Godown Name, Both are not allowed ")
            bln = False
        End If

        If GRIDCONSUMPTION.RowCount = 0 Then
            EP.SetError(CMBNAME, " Please Fill Item Details")
            bln = False
        End If

        Return bln
    End Function

    Private Sub cmbtrans_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBTRANS.Enter
        Try
            If CMBTRANS.Text.Trim = "" Then fillname(CMBTRANS, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBTRANS.KeyDown
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

    Private Sub cmbtrans_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBTRANS.Validating
        Try
            If CMBTRANS.Text.Trim <> "" Then namevalidate(CMBTRANS, cmbcode, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT' ", "Sundry Creditors", "TRANSPORT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS' AND (ACC_SUBTYPE = 'SIZER' OR ACC_SUBTYPE = 'WEAVER' OR ACC_SUBTYPE = 'WARPER') ")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBNAME.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS' AND (ACC_SUBTYPE = 'SIZER' OR ACC_SUBTYPE = 'WEAVER' OR ACC_SUBTYPE = 'WARPER') "
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, cmbcode, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS' AND (ACC_SUBTYPE = 'SIZER' OR ACC_SUBTYPE = 'WEAVER' OR ACC_SUBTYPE = 'WARPER') ")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
LINE1:
            TEMPCONSUMPTIONNO = Val(TXTCONSUMPTIONNO.Text) - 1
Line2:
            If TEMPCONSUMPTIONNO > 0 Then

                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" STORE_NO ", "", "  STORECONSUMPTION ", " AND STORE_NO = '" & TEMPCONSUMPTIONNO & "' AND STORECONSUMPTION.STORE_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    EDIT = True
                    STORECONSUMPTION_Load(sender, e)
                Else
                    TEMPCONSUMPTIONNO = Val(TEMPCONSUMPTIONNO - 1)
                    GoTo Line2
                End If
            Else
                clear()
                EDIT = False
            End If

            If Val(TXTQTY.Text.Trim) = 0 And TEMPCONSUMPTIONNO > 1 Then
                TXTCONSUMPTIONNO.Text = TEMPCONSUMPTIONNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
LINE1:
            TEMPCONSUMPTIONNO = Val(TXTCONSUMPTIONNO.Text) + 1
            getmax_BILL_no()
            Dim MAXNO As Integer = TXTCONSUMPTIONNO.Text.Trim
            clear()
            If Val(TXTCONSUMPTIONNO.Text) - 1 >= TEMPCONSUMPTIONNO Then
                EDIT = True
                STORECONSUMPTION_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
            If Val(TXTQTY.Text.Trim) = 0 And TEMPCONSUMPTIONNO < MAXNO Then
                TXTCONSUMPTIONNO.Text = TEMPCONSUMPTIONNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                TEMPCONSUMPTIONNO = Val(tstxtbillno.Text)
                If TEMPCONSUMPTIONNO > 0 Then
                    EDIT = True
                    STORECONSUMPTION_Load(sender, e)
                Else
                    clear()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Dim OBJDTLS As New StoreConsumptionDetails
            OBJDTLS.MdiParent = MDIMain
            OBJDTLS.Show()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Call cmdok_Click(sender, e)
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Try
            If EDIT = True Then
                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If MsgBox("Delete Entry?", MsgBoxStyle.YesNo) = vbYes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(TXTCONSUMPTIONNO.Text.Trim)
                    alParaval.Add(YearId)

                    Dim ClsDO As New ClsStoreConsumption
                    ClsDO.alParaval = alParaval
                    Dim IntResult As Integer = ClsDO.DELETE()
                    MsgBox("Entry Deleted Successfully")
                    clear()
                    EDIT = False
                End If
            Else
                MsgBox("Delete is only in Edit Mode")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tooldelete.Click
        Call cmddelete_Click(sender, e)
    End Sub

    Private Sub CONSUMPTIONDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CONSUMPTIONDATE.GotFocus
        CONSUMPTIONDATE.SelectAll()
    End Sub

    Private Sub CONSUMPTIONDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CONSUMPTIONDATE.Validating
        Try
            If CONSUMPTIONDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(CONSUMPTIONDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTQTY_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTQTY.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Sub total()
        Try
            LBLTOTALQTY.Text = 0.0
            For Each ROW As DataGridViewRow In GRIDCONSUMPTION.Rows
                If ROW.Cells(GITEMNAME.Index).Value <> Nothing Then
                    LBLTOTALQTY.Text += Val(ROW.Cells(GQTY.Index).EditedFormattedValue)
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try
            If GRIDDOUBLECLICK = False Then
                GRIDCONSUMPTION.Rows.Add(CMBITEMNAME.Text.Trim, Val(TXTQTY.Text.Trim))
            ElseIf GRIDDOUBLECLICK = True Then
                GRIDCONSUMPTION.Item(GITEMNAME.Index, TEMPROW).Value = CMBITEMNAME.Text.Trim
                GRIDCONSUMPTION.Item(GQTY.Index, TEMPROW).Value = Val(TXTQTY.Text.Trim)
                GRIDDOUBLECLICK = False
            End If

            total()

            GRIDCONSUMPTION.FirstDisplayedScrollingRowIndex = GRIDCONSUMPTION.RowCount - 1

            CMBITEMNAME.Focus()
            CMBITEMNAME.Text = ""
            TXTQTY.Clear()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTQTY_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTQTY.Validated
        Try
            If CMBITEMNAME.Text.Trim <> "" And Val(TXTQTY.Text.Trim) > 0 Then
                If Not CHECKQTY() Then
                    MsgBox("Enter Proper Details", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    FILLGRID()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function CHECKQTY() As Boolean
        Try
            Dim BLN As Boolean = True
            Dim STOCK As Double = 0
            Dim OBJCMN As New ClsCommonMaster
            Dim dt As DataTable

            'STOCK VALIDATION
            STOCK = 0
            dt = OBJCMN.search(" ISNULL(SUM(STORESTOCKVIEW.QTY),0) AS QTY ", "", " STORESTOCKVIEW ", " AND STORESTOCKVIEW.ITEMNAME = '" & CMBITEMNAME.Text.Trim & "' AND STORESTOCKVIEW.CMPID = " & CmpId & " AND STORESTOCKVIEW.YEARID = " & YearId)
            If dt.Rows.Count > 0 Then STOCK = Val(dt.Rows(0).Item("QTY"))

            'IF ENTRY IS IN EDIT MODE ADD ALREADY SAVED(SUBTRACTED) QTY AND THEN CHECK...
            If EDIT = True Then
                Dim OBJSTORE As New ClsStoreConsumption
                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(TEMPCONSUMPTIONNO)
                ALPARAVAL.Add(YearId)
                OBJSTORE.alParaval = ALPARAVAL
                Dim dttable As DataTable = OBJSTORE.SELECTCONSUMPTION()
                If dttable.Rows.Count > 0 Then STOCK += Val(dttable.Rows(0).Item("QTY"))
            Else
                If STOCK = 0 Then
                    MsgBox("Stock not Found")
                    BLN = False
                End If
            End If

            If Val(TXTQTY.Text.Trim) > STOCK Then
                MsgBox("Quantity cannot be more than stock quantity")
                BLN = False
            End If

            Return BLN
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub GRIDCONSUMPTION_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDCONSUMPTION.CellDoubleClick
        Try
            If GRIDCONSUMPTION.CurrentRow.Index >= 0 And GRIDCONSUMPTION.Item(GITEMNAME.Index, GRIDCONSUMPTION.CurrentRow.Index).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                CMBITEMNAME.Text = GRIDCONSUMPTION.Item(GITEMNAME.Index, GRIDCONSUMPTION.CurrentRow.Index).Value.ToString
                TXTQTY.Text = GRIDCONSUMPTION.Item(GQTY.Index, GRIDCONSUMPTION.CurrentRow.Index).Value.ToString

                TEMPROW = GRIDCONSUMPTION.CurrentRow.Index
                CMBITEMNAME.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDCONSUMPTION_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDCONSUMPTION.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDCONSUMPTION.RowCount > 0 Then
                'dont allow user if any of the grid line is in edit mode.....
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block
                GRIDCONSUMPTION.Rows.RemoveAt(GRIDCONSUMPTION.CurrentRow.Index)
                total()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBITEMNAME.Enter
        Try
            If CMBITEMNAME.Text.Trim = "" Then FILLSTOREITEMNAME(CMBITEMNAME)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBITEMNAME.Validating
        Try
            If CMBITEMNAME.Text.Trim <> "" Then STOREITEMVALIDATE(CMBITEMNAME, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBGODOWN.Enter
        Try
            If CMBGODOWN.Text.Trim = "" Then fillGODOWN(CMBGODOWN, EDIT, " AND GODOWN_ISOUR = 'True'")
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
            If CMBGODOWN.Text.Trim <> "" Then GODOWNVALIDATE(CMBGODOWN, e, Me, " AND GODOWN_ISOUR = 'True'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class