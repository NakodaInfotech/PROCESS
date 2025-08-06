
Imports BL
Imports System.Windows.Forms
Imports System.IO
Imports System.Diagnostics

Public Class OpeningStockGreywithProcessor

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public edit As Boolean
    Public tempMsg As Integer
    Public TEMPOPGREYSTOCKPROCESSORNO As Integer
    Public FRMSTRING As String

    Sub TOTAL()
        Try
            LBLTOTALTAKA.Text = 0.0
            LBLTOTALMTRS.Text = 0.0
            LBLTOTALWT.Text = 0.0
            LBLTOTALWTMTRS.Text = 0.0


            For Each ROW As DataGridViewRow In GRIDSTOCK.Rows
                If ROW.Cells(GOPGREYSTOCKPROCESSORNO.Index).Value <> Nothing Then
                    LBLTOTALTAKA.Text = Format(Val(LBLTOTALTAKA.Text) + Val(ROW.Cells(GTAKA.Index).EditedFormattedValue), "0.00")
                    LBLTOTALMTRS.Text = Format(Val(LBLTOTALMTRS.Text) + Val(ROW.Cells(GMTRS.Index).EditedFormattedValue), "0.00")
                    LBLTOTALWT.Text = Format(Val(LBLTOTALWT.Text) + Val(ROW.Cells(GWT.Index).EditedFormattedValue), "0.000")
                End If
            Next
            LBLTOTALWTMTRS.Text = Format(Val(LBLTOTALWT.Text) / Val(LBLTOTALMTRS.Text), "0.000")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()
        TXTOPGREYSTOCKPROCESSORNO.Clear()
        CMBPROCESSORFILTER.Text = ""
        CMBPROCESSOR.Text = ""
        CMBGREYQUALITY.Text = ""
        TXTLOTNO.Clear()
        TXTPCS.Clear()
        TXTMTRS.Clear()
        TXTWT.Clear()
        TXTQUALITYWT.Clear()
        TXTREMARKS.Clear()
    End Sub

    Private Sub OpeningStockGreywithProcessor_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        fillname(CMBPROCESSORFILTER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'")
        fillname(CMBPROCESSOR, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'")
        FILLGREY(CMBGREYQUALITY, edit)
    End Sub

    Sub FILLGRID()

        Try
            Dim dttable As New DataTable
            Dim OBJOPSTOCK As New ClsOpeningStockGreyProcessor

            OBJOPSTOCK.alParaval.Add(TEMPOPGREYSTOCKPROCESSORNO)
            OBJOPSTOCK.alParaval.Add(YearId)
            dttable = OBJOPSTOCK.GETSTOCKGREY()

            If dttable.Rows.Count > 0 Then
                'ITEM GRID
                For Each ROW As DataRow In dttable.Rows
                    GRIDSTOCK.Rows.Add(Val(ROW("OPGREYSTOCKPROCESSORNO")), ROW("PROCESSOR"), ROW("GREY"), ROW("LOTNO"), Val(ROW("TAKA")), Format(Val(ROW("MTRS")), "0.00"), Format(Val(ROW("WT")), "0.000"), Format(Val(ROW("WTMTR")), "0.000"), ROW("REMARKS"))
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

    Private Sub TXTREMARKS_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTREMARKS.Validating

        If CMBGREYQUALITY.Text.Trim <> "" And TXTPCS.Text.Trim <> "" And Val(TXTMTRS.Text.Trim) > 0 And Val(TXTWT.Text.Trim) > 0 Then

            If USERADD = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(CMBPROCESSOR.Text.Trim)
            ALPARAVAL.Add(CMBGREYQUALITY.Text.Trim)
            ALPARAVAL.Add(TXTLOTNO.Text.Trim)
            ALPARAVAL.Add(Val(TXTPCS.Text.Trim))
            ALPARAVAL.Add(Format(Val(TXTMTRS.Text.Trim), "0.00"))
            ALPARAVAL.Add(Format(Val(TXTWT.Text.Trim), "0.000"))
            ALPARAVAL.Add(Format(Val(TXTQUALITYWT.Text.Trim), "0.000"))
            ALPARAVAL.Add(TXTREMARKS.Text.Trim)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)


            Dim OBJOPENSTOCK As New ClsOpeningStockGreyProcessor
            OBJOPENSTOCK.alParaval = ALPARAVAL

            If edit = False Then
                Dim DT As DataTable = OBJOPENSTOCK.SAVE()
            Else
                ALPARAVAL.Add(Val(TXTOPGREYSTOCKPROCESSORNO.Text))
                Dim INTRES As Integer = OBJOPENSTOCK.UPDATE()
                GRIDDOUBLECLICK = False
                edit = False
            End If

            GRIDSTOCK.RowCount = 0
            FILLGRID()
            CLEAR()
            CMBPROCESSOR.Focus()
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
        FILLGRID()
        TOTAL()
    End Sub

    Private Sub GRIDSTOCK_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDSTOCK.CellDoubleClick
        Try
            If e.RowIndex < 0 Then Exit Sub

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            TXTOPGREYSTOCKPROCESSORNO.Text = GRIDSTOCK.Item(GOPGREYSTOCKPROCESSORNO.Index, e.RowIndex).Value
            CMBPROCESSOR.Text = GRIDSTOCK.Item(GPROCESSOR.Index, e.RowIndex).Value
            CMBGREYQUALITY.Text = GRIDSTOCK.Item(GGREY.Index, e.RowIndex).Value
            TXTLOTNO.Text = GRIDSTOCK.Item(GLOTNO.Index, e.RowIndex).Value
            TXTPCS.Text = GRIDSTOCK.Item(GTAKA.Index, e.RowIndex).Value
            TXTMTRS.Text = GRIDSTOCK.Item(GMTRS.Index, e.RowIndex).Value
            TXTWT.Text = GRIDSTOCK.Item(GWT.Index, e.RowIndex).Value
            TXTQUALITYWT.Text = Val(GRIDSTOCK.Item(GWTMTR.Index, e.RowIndex).Value)
            TXTREMARKS.Text = GRIDSTOCK.Item(GREMARKS.Index, e.RowIndex).Value

            GRIDDOUBLECLICK = True
            edit = True
            TEMPROW = e.RowIndex
            CMBPROCESSOR.Focus()

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
                        Dim OBJNO As New ClsOpeningStockGreyProcessor

                        OBJNO.alParaval = ALPARAVAL
                        ALPARAVAL.Add(GRIDSTOCK.Rows(GRIDSTOCK.CurrentRow.Index).Cells(GOPGREYSTOCKPROCESSORNO.Index).Value)
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

    Private Sub TXTTAKA_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTPCS.KeyPress
        numdotkeypress(e, TXTPCS, Me)
    End Sub

    Private Sub TXTMTRS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTMTRS.KeyPress
        numdot(e, TXTMTRS, Me)
    End Sub

    Private Sub TXTWT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTWT.KeyPress
        numdot3(e, TXTWT, Me)
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

    Private Sub CMBGREYQUALITY_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBGREYQUALITY.Validated
        calc()
    End Sub

    Private Sub CMBGREYQUALITY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBGREYQUALITY.Validating
        Try
            If CMBGREYQUALITY.Text.Trim <> "" Then GREYVALIDATE(CMBGREYQUALITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPROCESSOR_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBPROCESSOR.Enter
        Try
            If CMBPROCESSOR.Text.Trim = "" Then fillname(CMBPROCESSOR, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPROCESSOR_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBPROCESSOR.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBPROCESSOR.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPROCESSOR_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBPROCESSOR.Validating
        Try
            If CMBPROCESSOR.Text.Trim <> "" Then namevalidate(CMBPROCESSOR, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "PROCESSOR")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPROCESSORFILTER_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBPROCESSORFILTER.Enter
        Try
            If CMBPROCESSORFILTER.Text.Trim = "" Then fillname(CMBPROCESSORFILTER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPROCESSORFILTER_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBPROCESSORFILTER.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBPROCESSORFILTER.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPROCESSORFILTER_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBPROCESSORFILTER.Validating
        Try
            If CMBPROCESSORFILTER.Text.Trim <> "" Then namevalidate(CMBPROCESSORFILTER, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "PROCESSOR")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPROCESSORFILTER_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBPROCESSORFILTER.Validated
        Try
            If CMBPROCESSORFILTER.Text.Trim <> "" Then
                GRIDSTOCK.RowCount = 0
                Dim objclsCMST As New ClsCommonMaster
                Dim dt As New DataTable
                dt = objclsCMST.search("ISNULL(STOCKMASTER_GREYPROCESSOR.SMGREYPROCESSOR_NO, 0) AS OPGREYSTOCKPROCESSORNO, ISNULL(PROCESSOR.Acc_cmpname, '') AS PROCESSOR, ISNULL(GREYQUALITYMASTER.GREY_NAME, '') AS GREY, ISNULL(STOCKMASTER_GREYPROCESSOR.SMGREYPROCESSOR_LOTNO, '') AS LOTNO, ISNULL(STOCKMASTER_GREYPROCESSOR.SMGREYPROCESSOR_TAKA, 0) AS TAKA, ISNULL(STOCKMASTER_GREYPROCESSOR.SMGREYPROCESSOR_MTRS, 0) AS MTRS, ISNULL(STOCKMASTER_GREYPROCESSOR.SMGREYPROCESSOR_WT, 0) AS WT, ISNULL(STOCKMASTER_GREYPROCESSOR.SMGREYPROCESSOR_WTMTR, 0) AS WTMTR, ISNULL(STOCKMASTER_GREYPROCESSOR.SMGREYPROCESSOR_REMARKS, '') AS REMARKS", " ", " STOCKMASTER_GREYPROCESSOR INNER JOIN GREYQUALITYMASTER ON STOCKMASTER_GREYPROCESSOR.SMGREYPROCESSOR_GREYID = GREYQUALITYMASTER.GREY_ID AND  STOCKMASTER_GREYPROCESSOR.SMGREYPROCESSOR_YEARID = GREYQUALITYMASTER.GREY_YEARID INNER JOIN LEDGERS AS PROCESSOR ON STOCKMASTER_GREYPROCESSOR.SMGREYPROCESSOR_PROCESSORID = PROCESSOR.Acc_id", " AND PROCESSOR.ACC_CMPNAME = '" & CMBPROCESSORFILTER.Text.Trim & "' AND STOCKMASTER_GREYPROCESSOR.SMGREYPROCESSOR_YEARID = " & YearId & " ORDER BY STOCKMASTER_GREYPROCESSOR.SMGREYPROCESSOR_NO")
                'gridstock.DataSource = dt
                If dt.Rows.Count > 0 Then
                    For Each ROW As DataRow In dt.Rows
                        GRIDSTOCK.Rows.Add(Val(ROW("OPGREYSTOCKPROCESSORNO")), ROW("PROCESSOR"), ROW("GREY"), ROW("LOTNO"), Val(ROW("TAKA")), Format(Val(ROW("MTRS")), "0.00"), Format(Val(ROW("WT")), "0.000"), Format(Val(ROW("WTMTR")), "0.000"), ROW("REMARKS"))
                    Next
                End If
                TOTAL()
            Else
                GRIDSTOCK.RowCount = 0
                FILLGRID()
                TOTAL()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub calc()
        Try
            If CMBGREYQUALITY.Text.Trim <> "" And GRIDDOUBLECLICK = False Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("ISNULL(GREY_QUALITYWT, 0) AS QUALITYWT, ISNULL(GREY_MTRS, 0) AS MTRS", "", "GREYQUALITYMASTER", "AND GREYQUALITYMASTER.GREY_NAME = '" & CMBGREYQUALITY.Text.Trim & "' AND GREY_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    'TXTMTRS.Text = Format(Val(TXTPCS.Text.Trim) * DT.Rows(0).Item("MTRS"), "0.000")
                    TXTQUALITYWT.Text = DT.Rows(0).Item("QUALITYWT")
                End If
                TXTWT.Text = Format((Val(TXTQUALITYWT.Text.Trim) * Val(TXTMTRS.Text.Trim)) / 100, "0.000")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTTAKA_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTPCS.Validating
        calc()
    End Sub

    Private Sub TXTLOTNO_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTLOTNO.KeyPress
        numkeypress(e, TXTLOTNO, Me)
    End Sub

    Private Sub TXTMTRS_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTMTRS.Validating
        If Val(TXTMTRS.Text.Trim) > 0 Then TXTWT.Text = Format((Val(TXTQUALITYWT.Text.Trim) * Val(TXTMTRS.Text.Trim)) / 100, "0.000")
    End Sub
End Class