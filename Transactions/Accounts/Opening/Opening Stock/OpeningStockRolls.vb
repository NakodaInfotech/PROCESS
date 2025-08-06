Imports BL
Imports System.Windows.Forms
Imports System.IO
Imports System.Diagnostics

Public Class OpeningStockRolls

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public edit As Boolean
    Public tempMsg As Integer
    Public TEMPOPROLLSTOCKNO As Integer
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
                If ROW.Cells(GOPROLLSTOCKNO.Index).Value <> Nothing Then
                    LBLTOTALROLLS.Text = Format(Val(LBLTOTALROLLS.Text) + Val(ROW.Cells(GROLLS.Index).EditedFormattedValue), "0")
                    LBLTOTALWT.Text = Format(Val(LBLTOTALWT.Text) + Val(ROW.Cells(GWT.Index).EditedFormattedValue), "0.000")
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()
        TXTOPROLLSSTOCKNO.Clear()
        CMBNAME.Text = ""
        TXTPROGRAMNO.Clear()
        CMBOURGODOWN.Text = GETDEFAULTGODOWN()
        CMBQUALITY.Text = ""
        CMBQUALITYFILTER.Text = ""
        CMBMILL.Text = ""
        TXTTOTALENDS.Clear()
        TXTROLLS.Clear()
        TXTWT.Clear()
        TXTREMARKS.Clear()
    End Sub

    Private Sub OpeningStockRolls_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        fillGODOWN(CMBOURGODOWN, edit, "AND GODOWN_ISOUR='TRUE'")
        fillQUALITY(CMBQUALITY, edit)
        fillname(CMBNAME, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WARPER'")
        fillname(CMBMILL, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS' AND ACC_SUBTYPE = 'MILL'")
    End Sub

    Private Sub CMBNAME_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WARPER'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBNAME.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WARPER'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WARPER'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "WARPER")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try

            Dim dttable As New DataTable
            Dim OBJOPSTOCK As New ClsOpeningStockRolls

            OBJOPSTOCK.alParaval.Add(0)
            OBJOPSTOCK.alParaval.Add(YearId)
            dttable = OBJOPSTOCK.GETSTOCKROLLS()

            If dttable.Rows.Count > 0 Then
                'ITEM GRID
                For Each ROW As DataRow In dttable.Rows
                    GRIDSTOCK.Rows.Add(Val(ROW("OPROLLSTOCKNO")), ROW("GODOWN"), ROW("WARPERNAME"), ROW("PROGRAMNO"), ROW("QUALITY"), ROW("MILL"), Val(ROW("ENDS")), Val(ROW("ROLLS")), Format(Val(ROW("WT")), "0.000"), ROW("REMARKS"), Val(ROW("OUTROLLS")), Val(ROW("OUTWT")))
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

        If CMBOURGODOWN.Text.Trim <> "" And CMBNAME.Text.Trim <> "" And Val(TXTPROGRAMNO.Text.Trim) > 0 And CMBQUALITY.Text.Trim <> "" And CMBMILL.Text.Trim <> "" And Val(TXTTOTALENDS.Text.Trim) > 0 And Val(TXTROLLS.Text.Trim) > 0 And Val(TXTWT.Text.Trim) > 0 Then

            If USERADD = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(CMBOURGODOWN.Text.Trim)
            ALPARAVAL.Add(CMBNAME.Text.Trim)
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


            Dim OBJOPENSTOCK As New ClsOpeningStockRolls
            OBJOPENSTOCK.alParaval = ALPARAVAL

            If edit = False Then
                Dim DT As DataTable = OBJOPENSTOCK.SAVE()
            Else

                ALPARAVAL.Add(Val(TXTOPROLLSSTOCKNO.Text))
                Dim INTRES As Integer = OBJOPENSTOCK.UPDATE()
                GRIDDOUBLECLICK = False
                edit = False
            End If

            GRIDSTOCK.RowCount = 0
            FILLGRID()
            CLEAR()
            CMBOURGODOWN.Focus()
        Else
            MsgBox("Enter Proper Details")
        End If
    End Sub

    Private Sub OpeningStockRolls_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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
        CMBOURGODOWN.Text = GETDEFAULTGODOWN()
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


            TXTOPROLLSSTOCKNO.Text = GRIDSTOCK.Item(GOPROLLSTOCKNO.Index, e.RowIndex).Value
            CMBOURGODOWN.Text = GRIDSTOCK.Item(GGODOWN.Index, e.RowIndex).Value
            CMBNAME.Text = GRIDSTOCK.Item(GNAME.Index, e.RowIndex).Value
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
            CMBNAME.Focus()

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
                        Dim OBJNO As New ClsOpeningStockRolls

                        OBJNO.alParaval = ALPARAVAL
                        ALPARAVAL.Add(GRIDSTOCK.Rows(GRIDSTOCK.CurrentRow.Index).Cells(GOPROLLSTOCKNO.Index).Value)
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

    Private Sub CMBQUALITYFILTER_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBQUALITYFILTER.Enter
        Try
            If CMBQUALITYFILTER.Text.Trim = "" Then fillQUALITY(CMBQUALITYFILTER, edit)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITYFILTER_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBQUALITYFILTER.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJQUALITYFILTER As New SelectQuality
                OBJQUALITYFILTER.ShowDialog()
                If OBJQUALITYFILTER.TEMPNAME <> "" Then CMBQUALITYFILTER.Text = OBJQUALITYFILTER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITYFILTER_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBQUALITYFILTER.Validating
        Try
            If CMBQUALITYFILTER.Text.Trim <> "" Then QUALITYVALIDATE(CMBQUALITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITYFILTER_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBQUALITYFILTER.Validated
        Try
            GRIDSTOCK.RowCount = 0
            If CMBQUALITYFILTER.Text.Trim <> "" Then
                GRIDSTOCK.RowCount = 0
                Dim objclsCMST As New ClsCommonMaster
                Dim dt As New DataTable
                dt = objclsCMST.search("ISNULL(STOCKMASTER_ROLLS.SMROLLS_NO, 0) AS OPROLLSTOCKNO, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN, LEDGERS.Acc_cmpname AS WARPERNAME, STOCKMASTER_ROLLS.SMROLLS_PROGRAMNO AS PROGRAMNO, ISNULL(QUALITYMASTER.QUALITY_NAME, '') AS QUALITY, ISNULL(MILL.Acc_cmpname, '') AS MILL, ISNULL(STOCKMASTER_ROLLS.SMROLLS_ENDS, 0) AS ENDS, ISNULL(STOCKMASTER_ROLLS.SMROLLS_ROLLS, 0) AS ROLLS, ISNULL(STOCKMASTER_ROLLS.SMROLLS_WT, 0) AS WT, ISNULL(STOCKMASTER_ROLLS.SMROLLS_REMARKS, '') AS REMARKS, ISNULL(STOCKMASTER_ROLLS.SMROLLS_OUTROLLS, 0) AS OUTROLLS, ISNULL(STOCKMASTER_ROLLS.SMROLLS_OUTWT, 0) AS OUTWT", " ", " STOCKMASTER_ROLLS INNER JOIN GODOWNMASTER ON STOCKMASTER_ROLLS.SMROLLS_GODOWNID = GODOWNMASTER.GODOWN_ID INNER JOIN QUALITYMASTER ON STOCKMASTER_ROLLS.SMROLLS_QUALITYID = QUALITYMASTER.QUALITY_ID INNER JOIN LEDGERS AS MILL ON STOCKMASTER_ROLLS.SMROLLS_MILLID = MILL.Acc_id INNER JOIN LEDGERS ON STOCKMASTER_ROLLS.SMROLLS_WARPERID = LEDGERS.Acc_id", " AND QUALITYMASTER.QUALITY_NAME = '" & CMBQUALITYFILTER.Text.Trim & "' AND STOCKMASTER_ROLLS.SMROLLS_YEARID = " & YearId & " ORDER BY STOCKMASTER_ROLLS.SMROLLS_NO")
                If dt.Rows.Count > 0 Then
                    For Each ROW As DataRow In dt.Rows
                        GRIDSTOCK.Rows.Add(Val(ROW("OPROLLSTOCKNO")), ROW("GODOWN"), ROW("WARPERNAME"), ROW("PROGRAMNO"), ROW("QUALITY"), ROW("MILL"), Val(ROW("ENDS")), Val(ROW("ROLLS")), Format(Val(ROW("WT")), "0.000"), ROW("REMARKS"), Val(ROW("OUTROLLS")), Val(ROW("OUTWT")))
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

    Private Sub CMBOURGODOWN_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBOURGODOWN.Enter
        Try
            If CMBOURGODOWN.Text.Trim <> "" Then fillGODOWN(CMBOURGODOWN, edit, "AND GODOWN_ISOUR='TRUE'")
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

    Private Sub TXTPROGRAMNO_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TXTPROGRAMNO.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                If CMBNAME.Text.Trim = "" Then
                    MsgBox("Select Warper First", MsgBoxStyle.Critical)
                    CMBNAME.Focus()
                    Exit Sub
                End If

                Dim OBJPROG As New SelectProgram
                Dim DT As DataTable = OBJPROG.DT
                OBJPROG.WARPERNAME = CMBNAME.Text.Trim
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