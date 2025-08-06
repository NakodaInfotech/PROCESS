
Imports BL

Public Class OpeningPipes

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public edit As Boolean
    Public FRMSTRING As String

    Sub TOTAL()
        Try
            LBLTOTALQTY.Text = 0.0
            For Each ROW As DataGridViewRow In gridstock.Rows
                If ROW.Cells(GOPSTOCKNO.Index).Value <> Nothing Then
                    LBLTOTALQTY.Text = Format(Val(LBLTOTALQTY.Text) + Val(ROW.Cells(GQTY.Index).EditedFormattedValue), "0")
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function CHECKGRID() As Boolean
        Dim bln As Boolean = True
        For Each ROW As DataGridViewRow In gridstock.Rows
            If GRIDDOUBLECLICK = False Or (GRIDDOUBLECLICK = True And TEMPROW <> ROW.Index) Then
                If (FRMSTRING = "STORE" And ROW.Cells(GNAME.Index).Value = CMBNAME.Text.Trim) Or (FRMSTRING = "STOREGODOWN" And ROW.Cells(GGODOWN.Index).Value = CMBOURGODOWN.Text.Trim) Then
                    EP.SetError(TXTQTY, " Name Already Present in Grid Below")
                    bln = False
                End If
            End If
        Next
        Return bln
    End Function

    Sub CLEAR()
        CMBNAME.Text = ""
        CMBOURGODOWN.Text = GETDEFAULTGODOWN()
        TXTOPSTOCKNO.Clear()
        TXTQTY.Clear()
    End Sub

    Private Sub OpeningPipes_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        If FRMSTRING = "STOREGODOWN" Then fillGODOWN(CMBOURGODOWN, edit, "AND GODOWN_ISOUR='TRUE'")
        If FRMSTRING = "STORE" Then fillname(CMBNAME, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS' AND (ACC_SUBTYPE = 'SIZER' OR ACC_SUBTYPE = 'WEAVER') ")
    End Sub

    Sub FILLGRID()
        Try
            Dim dttable As New DataTable
            Dim OBJOPSTOCK As New ClsOpeningPipes

            OBJOPSTOCK.alParaval.Add(0)
            OBJOPSTOCK.alParaval.Add(YearId)
            If FRMSTRING = "STOREGODOWN" Then
                dttable = OBJOPSTOCK.GETSTOCKGODOWN()
            Else
                dttable = OBJOPSTOCK.GETSTOCK()
            End If

            If dttable.Rows.Count > 0 Then
                'ITEM GRID
                For Each ROW As DataRow In dttable.Rows
                    gridstock.Rows.Add(Val(ROW("OPSTOCKNO")), ROW("GODOWN"), ROW("NAME"), Val(ROW("QTY")), Val(ROW("OUTQTY")))
                    If Val(ROW("OUTQTY")) > 0 Then gridstock.Rows(gridstock.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
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

    Private Sub TXTQTY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTQTY.Validating

        If FRMSTRING = "STOREGODOWN" Then
            If CMBOURGODOWN.Text.Trim = "" Or Val(TXTQTY.Text.Trim) = 0 Then
                MsgBox("Enter Proper Details")
                Exit Sub
            End If
        ElseIf FRMSTRING = "STORE" Then
            If CMBNAME.Text.Trim = "" Or Val(TXTQTY.Text.Trim) = 0 Then
                MsgBox("Enter Proper Details")
                Exit Sub
            End If
        End If

        If USERADD = False Then
            MsgBox("Insufficient Rights")
            Exit Sub
        End If

        EP.Clear()
        If Not CHECKGRID() Then
            Exit Sub
        End If

        Dim ALPARAVAL As New ArrayList

        ALPARAVAL.Add(CMBOURGODOWN.Text.Trim)
        ALPARAVAL.Add(CMBNAME.Text.Trim)
        ALPARAVAL.Add(Val(TXTQTY.Text.Trim))
        ALPARAVAL.Add(CmpId)
        ALPARAVAL.Add(Userid)
        ALPARAVAL.Add(YearId)


        Dim OBJOPENSTOCK As New ClsOpeningPipes
        OBJOPENSTOCK.alParaval = ALPARAVAL

        If edit = False Then
            Dim DT As DataTable = OBJOPENSTOCK.SAVE()
        Else
            ALPARAVAL.Add(Val(TXTOPSTOCKNO.Text))
            Dim INTRES As Integer = OBJOPENSTOCK.UPDATE()
            GRIDDOUBLECLICK = False
            edit = False
        End If

        gridstock.RowCount = 0
        FILLGRID()
        CLEAR()

    End Sub

    Private Sub OpeningPipes_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

        If FRMSTRING = "STOREGODOWN" Then
            GGODOWN.Visible = True
            GNAME.Visible = False
            CMBOURGODOWN.Visible = True
            CMBNAME.Visible = False
        End If

        CLEAR()
        CMBOURGODOWN.Text = GETDEFAULTGODOWN()
        FILLCMB()
        FILLGRID()
        TOTAL()
    End Sub

    Private Sub gridstock_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridstock.CellDoubleClick
        Try
            If e.RowIndex < 0 Then Exit Sub

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If gridstock.Item(GOUTQTY.Index, e.RowIndex).Value > 0 Then
                MsgBox("Pipe Locked, it is used further", MsgBoxStyle.Critical)
                Exit Sub
            End If

            TXTOPSTOCKNO.Text = gridstock.Item(GOPSTOCKNO.Index, e.RowIndex).Value
            CMBOURGODOWN.Text = gridstock.Item(GGODOWN.Index, e.RowIndex).Value
            CMBNAME.Text = gridstock.Item(GNAME.Index, e.RowIndex).Value
            TXTQTY.Text = Val(gridstock.Item(GQTY.Index, e.RowIndex).Value)

            GRIDDOUBLECLICK = True
            edit = True
            TEMPROW = e.RowIndex
            CMBNAME.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridstock_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gridstock.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                If gridstock.SelectedCells.Count > 0 Then

                    If USERDELETE = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If

                    If GRIDDOUBLECLICK = True Then
                        MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                        Exit Sub
                    End If

                    If gridstock.Item(GOUTQTY.Index, gridstock.CurrentRow.Index).Value > 0 Then
                        MsgBox("Pipe Locked, it is used further", MsgBoxStyle.Critical)
                        Exit Sub
                    End If


                    Dim TEMPMSG As Integer = MsgBox("Delete Details", MsgBoxStyle.YesNo)
                    If TEMPMSG = vbYes Then
                        Dim ALPARAVAL As New ArrayList
                        Dim OBJNO As New ClsOpeningPipes

                        OBJNO.alParaval = ALPARAVAL
                        ALPARAVAL.Add(gridstock.CurrentRow.Cells(GOPSTOCKNO.Index).Value)
                        ALPARAVAL.Add(YearId)

                        Dim INTRES As DataTable = OBJNO.DELETE()
                        gridstock.Rows.RemoveAt(gridstock.CurrentRow.Index)
                    End If
                    TOTAL()

                End If
            End If
        Catch ex As Exception
            Throw ex

        End Try
    End Sub

    Private Sub CMBNAME_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS' and (LEDGERS.ACC_SUBTYPE = 'SIZER' OR  LEDGERS.ACC_SUBTYPE = 'WEAVER')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' and (LEDGERS.ACC_SUBTYPE = 'SIZER' OR  LEDGERS.ACC_SUBTYPE = 'WEAVER')"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then cmbcode.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then CMBNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' and (LEDGERS.ACC_SUBTYPE = 'SIZER' OR  LEDGERS.ACC_SUBTYPE = 'WEAVER')", "SUNDRY CREDITORS", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
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

    Private Sub TXTQTY_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTQTY.KeyPress
        numkeypress(e, TXTQTY, Me)
    End Sub

End Class