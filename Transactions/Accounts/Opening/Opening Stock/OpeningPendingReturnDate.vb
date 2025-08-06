
Imports BL
Imports System.Windows.Forms
Imports System.IO
Imports System.Diagnostics

Public Class OpeningPendingReturnDate

    Public edit As Boolean
    Public TEMPRETNO As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer

    Private Sub CMBDYEING_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBDYEINGFILTER.Enter
        Try
            If CMBDYEINGFILTER.Text.Trim = "" Then fillname(CMBDYEINGFILTER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDYEING_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBDYEINGFILTER.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBDYEINGFILTER.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDYEING_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBDYEINGFILTER.Validating
        Try
            If CMBDYEINGFILTER.Text.Trim <> "" Then namevalidate(CMBDYEINGFILTER, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "PROCESSOR")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDYEING_Enter_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBDYEING.Enter
        Try
            If CMBDYEING.Text.Trim = "" Then fillname(CMBDYEING, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDYEING_KeyDown1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBDYEING.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBDYEING.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDYEING_Validating1(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBDYEING.Validating
        Try
            If CMBDYEING.Text.Trim <> "" Then namevalidate(CMBDYEING, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "PROCESSOR")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGREYQUALITY_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBGREYQUALITY.Enter
        Try
            If CMBGREYQUALITY.Text.Trim = "" Then FILLGREY(CMBGREYQUALITY, edit)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGREYQUALITY_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBGREYQUALITY.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJGREY As New SelectGreyQuality
                OBJGREY.ShowDialog()
                If OBJGREY.TEMPNAME <> "" Then CMBGREYQUALITY.Text = OBJGREY.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGREYQUALITY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBGREYQUALITY.Validating
        Try
            If CMBGREYQUALITY.Text.Trim <> "" Then GREYVALIDATE(CMBGREYQUALITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBTRANS.Enter
        Try
            If CMBTRANS.Text.Trim = "" Then fillname(CMBTRANS, edit, "AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'AND LEDGERS.ACC_TYPE= 'TRANSPORT'")
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

    Sub TOTAL()
        Try
            LBLTOTALTAKA.Text = 0.0
            LBLTOTALMTRS.Text = 0.0

            For Each ROW As DataGridViewRow In GRIDSTOCK.Rows
                If ROW.Cells(GOPRETNO.Index).Value <> Nothing Then
                    LBLTOTALTAKA.Text = Format(Val(LBLTOTALTAKA.Text) + Val(ROW.Cells(GOPRETTAKA.Index).EditedFormattedValue), "0.00")
                    LBLTOTALMTRS.Text = Format(Val(LBLTOTALMTRS.Text) + Val(ROW.Cells(GOPRETMTRS.Index).EditedFormattedValue), "0.00")
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()
        CMBDYEINGFILTER.Text = ""
        CMBOURGODOWN.Text = GETDEFAULTGODOWN()
        TXTGRNO.Clear()
        TXTINVNO.Clear()
        DTINVDATE.Clear()
        CMBNAME.Text = ""
        CMBDYEING.Text = ""
        TXTLOTNO.Clear()
        CMBGREYQUALITY.Text = ""
        TXTRETTAKA.Clear()
        TXTRETMTRS.Clear()
        TXTRETDONO.Clear()
        DTRETDODATE.Clear()
        CMBTRANS.Text = ""
    End Sub

    Private Sub OpeningPendingReturnDate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then
                Me.Close()
            ElseIf e.KeyCode = Keys.OemQuotes Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        fillGODOWN(CMBOURGODOWN, edit, " AND GODOWN_ISOUR = 'True'")
        fillname(CMBNAME, edit, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND ACC_TYPE = 'ACCOUNTS'")
        fillname(CMBDYEINGFILTER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'")
        fillname(CMBDYEING, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'")
        FILLGREY(CMBGREYQUALITY, edit)
        fillname(CMBTRANS, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'TRANSPORT'")

    End Sub

    Private Sub cmbname_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            If cmbname.Text.Trim = "" Then fillname(cmbname, edit, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBNAME.KeyDown
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

    Private Sub cmbname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            namevalidate(cmbname, CMBCODE, e, Me, TXTADD, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry debtors' ", "Sundry debtors", "ACCOUNTS")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBOURGODOWN_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBOURGODOWN.Enter
        Try
            If CMBOURGODOWN.Text.Trim = "" Then fillGODOWN(CMBOURGODOWN, edit, "AND GODOWN_ISOUR='TRUE'")
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
                OBJGODOWN.SEARCH = "AND GODOWN_ISOUR='TRUE'"
                OBJGODOWN.ShowDialog()
                If OBJGODOWN.TEMPNAME <> "" Then CMBOURGODOWN.Text = OBJGODOWN.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBOURGODOWN_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBOURGODOWN.Validating
        Try
            If CMBOURGODOWN.Text.Trim <> "" Then GODOWNVALIDATE(CMBOURGODOWN, e, Me, "AND GODOWN_ISOUR='TRUE'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()

        Try
            Dim dttable As New DataTable
            Dim OBJOPSTOCK As New ClsOpeningPendingReturnDate

            OBJOPSTOCK.alParaval.Add(TEMPRETNO)
            OBJOPSTOCK.alParaval.Add(YearId)
            dttable = OBJOPSTOCK.GETSTOCKGREY()

            If dttable.Rows.Count > 0 Then
                'ITEM GRID
                Dim RETDATE As String = ""
                For Each ROW As DataRow In dttable.Rows
                    If ROW("RETDODATE") <> "" Then RETDATE = ROW("RETDODATE")
                    GRIDSTOCK.Rows.Add(Val(ROW("OPRETNO")), ROW("GODOWN"), Val(ROW("GRNO")), Val(ROW("INVNO")), Format(Convert.ToDateTime(ROW("INVDATE")).Date, "dd/MM/yyyy"), ROW("NAME"), ROW("DYEING"), Val(ROW("LOTNO")), ROW("GREY"), Val(ROW("RETTAKA")), Format(Val(ROW("RETMTRS")), "0.00"), ROW("RETDONO"), RETDATE, ROW("TRANSPORT"))
                Next
            End If
            TOTAL()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub CMBTRANS_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBTRANS.Validated

        If CMBOURGODOWN.Text.Trim <> "" And Val(TXTGRNO.Text.Trim) > 0 And Val(TXTINVNO.Text.Trim) > 0 And DTINVDATE.Text <> "__/__/____" And CMBNAME.Text.Trim <> "" And CMBDYEING.Text.Trim <> "" And Val(TXTLOTNO.Text.Trim) > 0 And CMBGREYQUALITY.Text.Trim <> "" And Val(TXTRETTAKA.Text.Trim) > 0 And Val(TXTRETMTRS.Text.Trim) > 0 And TXTRETDONO.Text.Trim <> "" Then

            If USERADD = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(CMBOURGODOWN.Text.Trim)
            ALPARAVAL.Add(TXTGRNO.Text.Trim)
            ALPARAVAL.Add(TXTINVNO.Text.Trim)
            ALPARAVAL.Add(Format(Convert.ToDateTime(DTINVDATE.Text).Date, "MM/dd/yyyy"))
            ALPARAVAL.Add(CMBNAME.Text.Trim)
            ALPARAVAL.Add(CMBDYEING.Text.Trim)
            ALPARAVAL.Add(TXTLOTNO.Text.Trim)
            ALPARAVAL.Add(CMBGREYQUALITY.Text.Trim)
            ALPARAVAL.Add(Val(TXTRETTAKA.Text.Trim))
            ALPARAVAL.Add(Format(Val(TXTRETMTRS.Text.Trim), "0.00"))
            ALPARAVAL.Add(TXTRETDONO.Text.Trim)
            If DTRETDODATE.Text = "__/__/____" Then ALPARAVAL.Add("") Else ALPARAVAL.Add(Format(Convert.ToDateTime(DTRETDODATE.Text).Date, "yyyy-MM-dd"))
            'ALPARAVAL.Add(DTRETDODATE.Text.Trim)
            ALPARAVAL.Add(CMBTRANS.Text.Trim)

            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)


            Dim OBJOPENSTOCK As New ClsOpeningPendingReturnDate
            OBJOPENSTOCK.alParaval = ALPARAVAL

            If edit = False Then
                Dim DT As DataTable = OBJOPENSTOCK.SAVE()
            Else
                ALPARAVAL.Add(Val(TXTOPRETNO.Text))
                Dim INTRES As Integer = OBJOPENSTOCK.UPDATE()
                GRIDDOUBLECLICK = False
                edit = False
            End If

            GRIDSTOCK.RowCount = 0
            FILLGRID()
            CLEAR()
            TXTGRNO.Focus()
        Else
            MsgBox("Enter Proper Details")
        End If
    End Sub

    Private Sub OpeningStockGreywithProcessor_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'GRN'")
        USERADD = DTROW(0).Item(1)
        USEREDIT = DTROW(0).Item(2)
        USERVIEW = DTROW(0).Item(3)
        USERDELETE = DTROW(0).Item(4)

        Dim OBJSEARCH As New ClsCommon
        Dim dttable As New DataTable

        If USEREDIT = False And USERVIEW = False Then
            MsgBox("Insufficient Rights")
            Exit Sub
        End If

        FILLCMB()
        CMBOURGODOWN.Text = GETDEFAULTGODOWN()
        FILLGRID()
    End Sub

    Private Sub GRIDSTOCK_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDSTOCK.CellDoubleClick
        Try
            If e.RowIndex < 0 Then Exit Sub

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            TXTOPRETNO.Text = Val(GRIDSTOCK.Item(GOPRETNO.Index, e.RowIndex).Value)
            CMBOURGODOWN.Text = GRIDSTOCK.Item(GGODOWN.Index, e.RowIndex).Value
            TXTGRNO.Text = Val(GRIDSTOCK.Item(GOPGRNO.Index, e.RowIndex).Value)
            TXTINVNO.Text = Val(GRIDSTOCK.Item(GOPINVNO.Index, e.RowIndex).Value)
            DTINVDATE.Text = GRIDSTOCK.Item(GOPDATE.Index, e.RowIndex).Value
            CMBNAME.Text = GRIDSTOCK.Item(GNAME.Index, e.RowIndex).Value
            CMBDYEING.Text = GRIDSTOCK.Item(GOPDYEING.Index, e.RowIndex).Value
            TXTLOTNO.Text = Val(GRIDSTOCK.Item(GOPLOTNO.Index, e.RowIndex).Value)
            CMBGREYQUALITY.Text = GRIDSTOCK.Item(GOPGREYQUALITY.Index, e.RowIndex).Value
            TXTRETTAKA.Text = Val(GRIDSTOCK.Item(GOPRETTAKA.Index, e.RowIndex).Value)
            TXTRETMTRS.Text = Val(GRIDSTOCK.Item(GOPRETMTRS.Index, e.RowIndex).Value)
            TXTRETDONO.Text = GRIDSTOCK.Item(GOPRETDO.Index, e.RowIndex).Value
            DTRETDODATE.Text = GRIDSTOCK.Item(GOPRETDATE.Index, e.RowIndex).Value
            CMBTRANS.Text = GRIDSTOCK.Item(GOPTRANSPORT.Index, e.RowIndex).Value
            GRIDDOUBLECLICK = True
            edit = True
            TEMPROW = e.RowIndex
            CMBOURGODOWN.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDSTOCK_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDSTOCK.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                If GRIDSTOCK.SelectedCells.Count > 0 Then

                    If USERDELETE = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If


                    Dim TEMPMSG As Integer = MsgBox("Delete Details", MsgBoxStyle.YesNo)
                    If TEMPMSG = vbYes Then
                        Dim ALPARAVAL As New ArrayList
                        Dim OBJNO As New ClsOpeningPendingReturnDate

                        OBJNO.alParaval = ALPARAVAL
                        ALPARAVAL.Add(GRIDSTOCK.Rows(GRIDSTOCK.CurrentRow.Index).Cells(GOPRETNO.Index).Value)
                        ALPARAVAL.Add(YearId)

                        Dim INTRES As DataTable = OBJNO.DELETE()
                        GRIDSTOCK.Rows.RemoveAt(GRIDSTOCK.CurrentRow.Index)
                    End If
                    TOTAL()

                End If
            End If
        Catch ex As Exception
            Throw ex

        End Try
    End Sub

    Private Sub CMBDYEINGFILTER_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBDYEINGFILTER.Validated
        Try
            If CMBDYEINGFILTER.Text.Trim <> "" Then
                GRIDSTOCK.RowCount = 0
                Dim objclsCMST As New ClsCommonMaster
                Dim dt As DataTable = objclsCMST.search("ISNULL(GODOWN_NAME, '') AS GODOWN, ISNULL(DYEING.Acc_cmpname, '') AS DYEING, ISNULL(GREYQUALITYMASTER.GREY_NAME, '') AS GREY, ISNULL(STOCKMASTER_RETDATE.SMRETDATE_NO, 0) AS OPRETNO, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSPORT, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(STOCKMASTER_RETDATE.SMRETDATE_GRNO, '') AS GRNO, ISNULL(STOCKMASTER_RETDATE.SMRETDATE_INVNO, '') AS INVNO, ISNULL(STOCKMASTER_RETDATE.SMRETDATE_INVDATE, '') AS INVDATE, ISNULL(STOCKMASTER_RETDATE.SMRETDATE_LOTNO, '') AS LOTNO, ISNULL(STOCKMASTER_RETDATE.SMRETDATE_RETTAKA, 0) AS RETTAKA, ISNULL(STOCKMASTER_RETDATE.SMRETDATE_RETMTRS, 0) AS RETMTRS, ISNULL(STOCKMASTER_RETDATE.SMRETDATE_RETDONO, '') AS RETDONO, ISNULL(STOCKMASTER_RETDATE.SMRETDATE_RETDODATE, '') AS RETDODATE", " ", " STOCKMASTER_RETDATE INNER JOIN LEDGERS AS DYEING ON STOCKMASTER_RETDATE.SMRETDATE_DYEINGID = DYEING.Acc_id INNER JOIN GREYQUALITYMASTER ON STOCKMASTER_RETDATE.SMRETDATE_GREYID = GREYQUALITYMASTER.GREY_ID LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON STOCKMASTER_RETDATE.SMRETDATE_TRANSID = TRANSLEDGERS.Acc_id INNER JOIN GODOWNMASTER ON STOCKMASTER_RETDATE.SMRETDATE_GODOWNID = GODOWN_id INNER JOIN LEDGERS ON STOCKMASTER_RETDATE.SMRETDATE_LEDGERID = LEDGERS.Acc_id", " AND DYEING.ACC_CMPNAME = '" & CMBDYEINGFILTER.Text.Trim & "' AND STOCKMASTER_RETDATE.SMRETDATE_YEARID = " & YearId & " ORDER BY STOCKMASTER_RETDATE.SMRETDATE_NO")
                If dt.Rows.Count > 0 Then
                    For Each ROW As DataRow In dt.Rows
                        GRIDSTOCK.Rows.Add(Val(ROW("OPRETNO")), ROW("GODOWN"), Val(ROW("GRNO")), Val(ROW("INVNO")), Format(Convert.ToDateTime(ROW("INVDATE")).Date, "dd/MM/yyyy"), ROW("NAME"), ROW("DYEING"), Val(ROW("LOTNO")), ROW("GREY"), Val(ROW("RETTAKA")), Format(Val(ROW("RETMTRS")), "0.00"), ROW("RETDONO"), ROW("RETDODATE"), ROW("TRANSPORT"))
                    Next
                End If
                TOTAL()
            Else
                GRIDSTOCK.RowCount = 0
                FILLGRID()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTRETTAKA_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTRETTAKA.KeyPress, TXTRETMTRS.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub TXTLOTNO_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTLOTNO.KeyPress, TXTINVNO.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub DTINVDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DTINVDATE.Validating
        Try
            If sender.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(sender.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DTRETDODATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DTRETDODATE.Validating
        Try
            If sender.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(sender.Text, TEMP) Then
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