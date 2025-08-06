Imports BL
Imports System.Windows.Forms
Imports System.IO
Imports System.Diagnostics

Public Class OpeningStockRollsSizer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public edit As Boolean
    Public tempMsg As Integer
    Public TEMPOPROLLSTOCKSIZER As Integer
    Public FRMSTRING As String

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
            LBLTOTALROLLS.Text = 0
            LBLTOTALWT.Text = 0.0

            For Each ROW As DataGridViewRow In gridstock.Rows
                If ROW.Cells(GOPROLLSTOCKNOSIZER.Index).Value <> Nothing Then
                    LBLTOTALROLLS.Text = Format(Val(LBLTOTALROLLS.Text) + Val(ROW.Cells(GROLLS.Index).EditedFormattedValue), "0")
                    LBLTOTALWT.Text = Format(Val(LBLTOTALWT.Text) + Val(ROW.Cells(GWT.Index).EditedFormattedValue), "0.000")
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()
        TXTOPROLLSSTOCKNOSIZER.Clear()
        CMBSIZER.Text = ""
        TXTPROGRAMNO.Clear()
        CMBQUALITY.Text = ""
        CMBSIZERFILTER.Text = ""
        CMBMILL.Text = ""
        TXTTOTALENDS.Clear()
        TXTROLLS.Clear()
        TXTWT.Clear()
        TXTREMARKS.Clear()
    End Sub

    Private Sub OpeningStockRollsSizer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        fillname(CMBSIZER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'")
        fillname(CMBSIZERFILTER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'")
        fillQUALITY(CMBQUALITY, edit)
        fillname(CMBMILL, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS' AND ACC_SUBTYPE = 'MILL'")
    End Sub

    Sub FILLGRID()
        Try

            Dim dttable As New DataTable
            Dim OBJOPSTOCK As New ClsOpeningStockRollsWithSizer

            OBJOPSTOCK.alParaval.Add(0)
            OBJOPSTOCK.alParaval.Add(YearId)
            dttable = OBJOPSTOCK.GETSTOCKROLLS()

            If dttable.Rows.Count > 0 Then
                'ITEM GRID
                For Each ROW As DataRow In dttable.Rows
                    GRIDSTOCK.Rows.Add(Val(ROW("OPROLLSTOCKNOSIZER")), ROW("SIZER"), ROW("PROGRAMNO"), ROW("QUALITY"), ROW("MILL"), Val(ROW("ENDS")), Val(ROW("ROLLS")), Format(Val(ROW("WT")), "0.000"), ROW("REMARKS"), Val(ROW("OUTROLLS")), Val(ROW("OUTWT")))
                    If Val(ROW("OUTROLLS")) > 0 Or Val(ROW("OUTWT")) > 0 Then GRIDSTOCK.Rows(GRIDSTOCK.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
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
        If CMBSIZER.Text.Trim <> "" And Val(TXTPROGRAMNO.Text.Trim) > 0 And CMBQUALITY.Text.Trim <> "" And CMBMILL.Text.Trim <> "" And Val(TXTTOTALENDS.Text.Trim) > 0 And Val(TXTROLLS.Text.Trim) > 0 And Val(TXTWT.Text.Trim) > 0 Then

            If USERADD = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(CMBSIZER.Text.Trim)
            ALPARAVAL.Add(Val(TXTPROGRAMNO.Text.Trim))
            ALPARAVAL.Add(CMBQUALITY.Text.Trim)
            ALPARAVAL.Add(CMBMILL.Text.Trim)
            ALPARAVAL.Add(Val(TXTTOTALENDS.Text.Trim))
            ALPARAVAL.Add(Val(TXTROLLS.Text.Trim))
            ALPARAVAL.Add(Format(Val(TXTWT.Text.Trim), "0.000"))
            ALPARAVAL.Add(TXTREMARKS.Text.Trim)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)


            Dim OBJOPENSTOCK As New ClsOpeningStockRollsWithSizer
            OBJOPENSTOCK.alParaval = ALPARAVAL

            If edit = False Then
                Dim DT As DataTable = OBJOPENSTOCK.SAVE()
            Else

                ALPARAVAL.Add(Val(TXTOPROLLSSTOCKNOSIZER.Text))
                Dim INTRES As Integer = OBJOPENSTOCK.UPDATE()
                GRIDDOUBLECLICK = False
                edit = False
            End If

            GRIDSTOCK.RowCount = 0
            FILLGRID()
            CLEAR()
            CMBSIZER.Focus()
        Else
            MsgBox("Enter Proper Details")
        End If
    End Sub

    Private Sub OpeningStockRollsSizer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'OPENING'")
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

            If GRIDSTOCK.Item(GOUTROLLS.Index, e.RowIndex).Value > 0 Or GRIDSTOCK.Item(GOUTWT.Index, e.RowIndex).Value > 0 Then
                MsgBox("Rolls Locked, it is used further", MsgBoxStyle.Critical)
                Exit Sub
            End If

            TXTOPROLLSSTOCKNOSIZER.Text = GRIDSTOCK.Item(GOPROLLSTOCKNOSIZER.Index, e.RowIndex).Value
            CMBSIZER.Text = GRIDSTOCK.Item(GSIZER.Index, e.RowIndex).Value
            TXTPROGRAMNO.Text = GRIDSTOCK.Item(GPROGRAMNO.Index, e.RowIndex).Value
            CMBQUALITY.Text = GRIDSTOCK.Item(GQUALITY.Index, e.RowIndex).Value
            CMBMILL.Text = GRIDSTOCK.Item(GMILL.Index, e.RowIndex).Value
            TXTTOTALENDS.Text = GRIDSTOCK.Item(GENDS.Index, e.RowIndex).Value
            TXTROLLS.Text = GRIDSTOCK.Item(GROLLS.Index, e.RowIndex).Value
            TXTWT.Text = GRIDSTOCK.Item(GWT.Index, e.RowIndex).Value
            TXTREMARKS.Text = GRIDSTOCK.Item(GREMARKS.Index, e.RowIndex).Value

            GRIDDOUBLECLICK = True
            edit = True
            TEMPROW = e.RowIndex
            CMBSIZER.Focus()

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

                    If GRIDDOUBLECLICK = True Then
                        MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                        Exit Sub
                    End If

                    If GRIDSTOCK.Item(GOUTROLLS.Index, GRIDSTOCK.CurrentRow.Index).Value > 0 Or GRIDSTOCK.Item(GOUTWT.Index, GRIDSTOCK.CurrentRow.Index).Value > 0 Then
                        MsgBox("Rolls Locked, it is used further", MsgBoxStyle.Critical)
                        Exit Sub
                    End If


                    Dim TEMPMSG As Integer = MsgBox("Delete Details", MsgBoxStyle.YesNo)
                    If TEMPMSG = vbYes Then
                        Dim ALPARAVAL As New ArrayList
                        Dim OBJNO As New ClsOpeningStockRollsWithSizer

                        OBJNO.alParaval = ALPARAVAL
                        ALPARAVAL.Add(GRIDSTOCK.Rows(GRIDSTOCK.CurrentRow.Index).Cells(GOPROLLSTOCKNOSIZER.Index).Value)
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

    Private Sub TXTWT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTWT.KeyPress
        numdot3(e, TXTWT, Me)
    End Sub

    Private Sub TXTROLLS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTROLLS.KeyPress
        numkeypress(e, TXTROLLS, Me)
    End Sub

    Private Sub TXTENDS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTTOTALENDS.KeyPress
        numkeypress(e, TXTTOTALENDS, Me)
    End Sub

    Private Sub CMBSIZERFILTER_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBSIZERFILTER.Enter
        Try
            If CMBSIZERFILTER.Text.Trim = "" Then fillname(CMBSIZERFILTER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSIZERFILTER_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBSIZERFILTER.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBSIZERFILTER.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSIZERFILTER_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBSIZERFILTER.Validating
        Try
            If CMBSIZERFILTER.Text.Trim <> "" Then namevalidate(CMBSIZERFILTER, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "SIZER")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSIZER_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBSIZER.Enter
        Try
            If CMBSIZER.Text.Trim = "" Then fillname(CMBSIZER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSIZER_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBSIZER.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBSIZER.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSIZER_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBSIZER.Validating
        Try
            If CMBSIZER.Text.Trim <> "" Then namevalidate(CMBSIZER, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "SIZER")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBQUALITY.Enter
        Try
            If CMBQUALITY.Text.Trim = "" Then fillQUALITY(CMBQUALITY, edit)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBQUALITY.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJQUALITY As New SelectQuality
                OBJQUALITY.ShowDialog()
                If OBJQUALITY.TEMPNAME <> "" Then CMBQUALITY.Text = OBJQUALITY.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBQUALITY.Validating
        Try
            If CMBQUALITY.Text.Trim <> "" Then QUALITYVALIDATE(CMBQUALITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILL_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBMILL.Enter
        Try
            If CMBMILL.Text.Trim = "" Then fillname(CMBMILL, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS' and LEDGERS.ACC_SUBTYPE = 'MILL'")
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
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' and LEDGERS.ACC_SUBTYPE = 'MILL'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then cmbcode.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then CMBMILL.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILL_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBMILL.Validating
        Try
            If CMBMILL.Text.Trim <> "" Then namevalidate(CMBMILL, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' and LEDGERS.ACC_SUBTYPE = 'MILL'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "MILL")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSIZERFILTER_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBSIZERFILTER.Validated
        Try
            GRIDSTOCK.RowCount = 0
            If CMBSIZERFILTER.Text.Trim <> "" Then
                GRIDSTOCK.RowCount = 0
                Dim objclsCMST As New ClsCommonMaster
                Dim dt As New DataTable
                dt = objclsCMST.search("ISNULL(STOCKMASTER_ROLLSSIZER.SMROLLSSIZER_NO, 0) AS OPROLLSTOCKNOSIZER, ISNULL(SIZER.Acc_cmpname, '') AS SIZER, ISNULL(QUALITYMASTER.QUALITY_NAME, '') AS QUALITY, ISNULL(MILL.Acc_cmpname, '') AS MILL, ISNULL(STOCKMASTER_ROLLSSIZER.SMROLLSSIZER_ENDS, 0) AS ENDS, ISNULL(STOCKMASTER_ROLLSSIZER.SMROLLSSIZER_ROLLS, 0) AS ROLLS, ISNULL(STOCKMASTER_ROLLSSIZER.SMROLLSSIZER_WT, 0) AS WT, ISNULL(STOCKMASTER_ROLLSSIZER.SMROLLSSIZER_REMARKS, '') AS REMARKS, ISNULL(STOCKMASTER_ROLLSSIZER.SMROLLSSIZER_OUTROLLS, 0) AS OUTROLLS, ISNULL(STOCKMASTER_ROLLSSIZER.SMROLLSSIZER_OUTWT, 0) AS OUTWT, STOCKMASTER_ROLLSSIZER.SMROLLSSIZER_PROGRAMNO AS PROGRAMNO", " ", " STOCKMASTER_ROLLSSIZER INNER JOIN LEDGERS AS SIZER ON STOCKMASTER_ROLLSSIZER.SMROLLSSIZER_SIZERID = SIZER.Acc_id INNER JOIN QUALITYMASTER ON STOCKMASTER_ROLLSSIZER.SMROLLSSIZER_QUALITYID = QUALITYMASTER.QUALITY_ID INNER JOIN LEDGERS AS MILL ON STOCKMASTER_ROLLSSIZER.SMROLLSSIZER_MILLID = MILL.Acc_id", " AND SIZER.ACC_CMPNAME = '" & CMBSIZERFILTER.Text.Trim & "' AND STOCKMASTER_ROLLSSIZER.SMROLLSSIZER_YEARID = " & YearId & " ORDER BY STOCKMASTER_ROLLSSIZER.SMROLLSSIZER_NO")
                'gridstock.DataSource = dt
                If dt.Rows.Count > 0 Then
                    For Each ROW As DataRow In dt.Rows
                        GRIDSTOCK.Rows.Add(Val(ROW("OPROLLSTOCKNOSIZER")), ROW("SIZER"), ROW("PROGRAMNO"), ROW("QUALITY"), ROW("MILL"), Val(ROW("ENDS")), Val(ROW("ROLLS")), Format(Val(ROW("WT")), "0.000"), ROW("REMARKS"), Val(ROW("OUTROLLS")), Val(ROW("OUTWT")))
                        If Val(ROW("OUTROLLS")) > 0 Or Val(ROW("OUTWT")) > 0 Then GRIDSTOCK.Rows(GRIDSTOCK.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                    Next
                End If
            Else
                FILLGRID()
            End If
            TOTAL()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTPROGRAMNO_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TXTPROGRAMNO.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                Dim OBJPROG As New SelectProgram
                Dim DT As DataTable = OBJPROG.DT
                OBJPROG.ShowDialog()
                If DT.Rows.Count > 0 Then
                    TXTPROGRAMNO.Text = Val(DT.Rows(0).Item("PROGRAMNO"))
                    TXTTOTALENDS.Text = Val(DT.Rows(0).Item("TOTALENDS"))
                    CMBQUALITY.Text = DT.Rows(0).Item("QUALITY")
                    CMBMILL.Text = DT.Rows(0).Item("MILLNAME")
                    TXTROLLS.Text = Val(DT.Rows(0).Item("ROLLS"))
                    TXTWT.Focus()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class