
Imports BL

Public Class FinishedBaleStockGodown

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public edit As Boolean
    Public tempMsg As Integer
    Public TEMPFINISHEDBALESTOCK As Integer

    Sub TOTAL()
        Try
            LBLTOTALTAKA.Text = 0.0
            LBLTOTALMTRS.Text = 0.0

            For Each ROW As DataGridViewRow In GRIDSTOCK.Rows
                If ROW.Cells(GOPFINISHEDGREYSTOCK.Index).Value <> Nothing Then
                    LBLTOTALTAKA.Text = Format(Val(LBLTOTALTAKA.Text) + Val(ROW.Cells(GTAKA.Index).EditedFormattedValue), "0.00")
                    LBLTOTALMTRS.Text = Format(Val(LBLTOTALMTRS.Text) + Val(ROW.Cells(GMTRS.Index).EditedFormattedValue), "0.00")
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()
        TXTFINISHEDBALESTOCK.Clear()
        CMBGREYFILTER.Text = ""
        CMBPROCESSOR.Text = ""
        CMBGODOWN.Text = GETDEFAULTGODOWN()
        CMBGREYQUALITY.Text = ""
        TXTLOTNO.Clear()
        TXTBALENO.Clear()
        TXTTAKA.Clear()
        TXTMTRS.Clear()
        TXTREMARKS.Clear()
    End Sub

    Private Sub FinsishedGreyStockInGodown_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        fillname(CMBPROCESSOR, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'")
        FILLGREY(CMBGREYFILTER, edit, " AND GREY_TYPE = 'FINISHED'")
        FILLGREY(CMBGREYQUALITY, edit, " AND GREY_TYPE = 'FINISHED'")
        fillGODOWN(CMBGODOWN, edit, "AND GODOWN_ISOUR='TRUE'")
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

    Sub FILLGRID()

        Try
            Dim dttable As New DataTable
            Dim OBJOPGREY As New ClsFinishedBaleStockGodown

            OBJOPGREY.alParaval.Add(TEMPFINISHEDBALESTOCK)
            OBJOPGREY.alParaval.Add(YearId)
            dttable = OBJOPGREY.GETSTOCKGREY()

            If dttable.Rows.Count > 0 Then
                'ITEM GRID
                For Each ROW As DataRow In dttable.Rows
                    GRIDSTOCK.Rows.Add(Val(ROW("FINISHEDGREYSTOCK")), ROW("PROCESSOR"), ROW("GODOWN"), ROW("GREY"), ROW("LOTNO"), ROW("BALENO"), Val(ROW("TAKA")), Format(Val(ROW("MTRS")), "0.00"), ROW("REMARKS"), ROW("DONE"))
                    If Convert.ToBoolean(ROW("DONE")) = True Then GRIDSTOCK.Rows(GRIDSTOCK.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
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

    Function CHECKDUPLICATE() As Boolean
        Try
            Dim BLN As Boolean = True
            For Each ROW As DataGridViewRow In GRIDSTOCK.Rows
                If (GRIDDOUBLECLICK = False And LCase(CMBPROCESSOR.Text.Trim) = LCase(ROW.Cells(GPROCESSOR.Index).Value) And Val(TXTLOTNO.Text.Trim) = Val(ROW.Cells(GLOTNO.Index).Value) And LCase(TXTBALENO.Text.Trim) = LCase(ROW.Cells(GBALENO.Index).Value)) Then
                    BLN = False
                    Exit For
                End If
            Next
            Return BLN
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub TXTREMARKS_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTREMARKS.Validating

        If CMBGODOWN.Text.Trim <> "" And CMBGREYQUALITY.Text.Trim <> "" And TXTLOTNO.Text.Trim <> "" And TXTBALENO.Text.Trim <> "" And Val(TXTTAKA.Text.Trim) > 0 And Val(TXTMTRS.Text.Trim) > 0 Then

            If USERADD = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If Not CHECKDUPLICATE Then
                MsgBox("Bale No Already Exists", MsgBoxStyle.Critical)
                Exit Sub
            End If


            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(CMBPROCESSOR.Text.Trim)
            ALPARAVAL.Add(CMBGODOWN.Text.Trim)
            ALPARAVAL.Add(CMBGREYQUALITY.Text.Trim)
            ALPARAVAL.Add(TXTLOTNO.Text.Trim)
            ALPARAVAL.Add(TXTBALENO.Text.Trim)
            ALPARAVAL.Add(Val(TXTTAKA.Text.Trim))
            ALPARAVAL.Add(Format(Val(TXTMTRS.Text.Trim), "0.00"))
            ALPARAVAL.Add(TXTREMARKS.Text.Trim)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)


            Dim OBJFINGREYSTOCK As New ClsFinishedBaleStockGodown
            OBJFINGREYSTOCK.alParaval = ALPARAVAL

            If edit = False Then
                Dim DT As DataTable = OBJFINGREYSTOCK.SAVE()
            Else
                ALPARAVAL.Add(Val(TXTFINISHEDBALESTOCK.Text))
                Dim INTRES As Integer = OBJFINGREYSTOCK.UPDATE()
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

    Private Sub FinsishedGreyStockInGodown_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
        CLEAR()
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

            If Convert.ToBoolean(GRIDSTOCK.Item(GDONE.Index, e.RowIndex).Value) = True Then
                MsgBox("Bale Locked", MsgBoxStyle.Critical)
                Exit Sub
            End If

            TXTFINISHEDBALESTOCK.Text = GRIDSTOCK.Item(GOPFINISHEDGREYSTOCK.Index, e.RowIndex).Value
            CMBPROCESSOR.Text = GRIDSTOCK.Item(GPROCESSOR.Index, e.RowIndex).Value
            CMBGODOWN.Text = GRIDSTOCK.Item(GGODOWN.Index, e.RowIndex).Value
            CMBGREYQUALITY.Text = GRIDSTOCK.Item(GGREY.Index, e.RowIndex).Value
            TXTLOTNO.Text = GRIDSTOCK.Item(GLOTNO.Index, e.RowIndex).Value
            TXTBALENO.Text = GRIDSTOCK.Item(GBALENO.Index, e.RowIndex).Value
            TXTTAKA.Text = GRIDSTOCK.Item(GTAKA.Index, e.RowIndex).Value
            TXTMTRS.Text = GRIDSTOCK.Item(GMTRS.Index, e.RowIndex).Value
            TXTREMARKS.Text = GRIDSTOCK.Item(GREMARKS.Index, e.RowIndex).Value

            GRIDDOUBLECLICK = True
            edit = True
            TEMPROW = e.RowIndex
            CMBGODOWN.Focus()

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

                    If Convert.ToBoolean(GRIDSTOCK.Item(GDONE.Index, GRIDSTOCK.CurrentRow.Index).Value) = True Then
                        MsgBox("Bale Locked", MsgBoxStyle.Critical)
                        Exit Sub
                    End If


                    Dim TEMPMSG As Integer = MsgBox("Delete Details", MsgBoxStyle.YesNo)
                    If TEMPMSG = vbYes Then
                        Dim ALPARAVAL As New ArrayList
                        Dim OBJNO As New ClsFinishedBaleStockGodown

                        OBJNO.alParaval = ALPARAVAL
                        ALPARAVAL.Add(GRIDSTOCK.Rows(GRIDSTOCK.CurrentRow.Index).Cells(GOPFINISHEDGREYSTOCK.Index).Value)
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

    Private Sub TXTTAKA_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTTAKA.KeyPress, TXTLOTNO.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTMTRS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTMTRS.KeyPress
        numdot(e, TXTMTRS, Me)
    End Sub

    Private Sub CMBGREYFILTER_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBGREYFILTER.Enter
        Try
            If CMBGREYFILTER.Text.Trim = "" Then FILLGREY(CMBGREYFILTER, edit, " AND GREY_TYPE = 'FINISHED'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGREYFILTER_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBGREYFILTER.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJGREY As New SelectGreyQuality
                OBJGREY.WHERECLAUSE = " AND GREY_TYPE ='FINISHED'"
                OBJGREY.ShowDialog()
                If OBJGREY.TEMPNAME <> "" Then CMBGREYFILTER.Text = OBJGREY.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGREYFILTER_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBGREYFILTER.Validating
        Try
            If CMBGREYFILTER.Text.Trim <> "" Then GREYVALIDATE(CMBGREYFILTER, e, Me, " AND GREY_TYPE = 'FINISHED'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBGODOWN.Enter
        Try
            If CMBGODOWN.Text.Trim = "" Then fillGODOWN(CMBGODOWN, edit, "AND GODOWN_ISOUR='TRUE'")
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
                OBJGODOWN.SEARCH = "AND GODOWN_ISOUR='TRUE'"
                OBJGODOWN.ShowDialog()
                If OBJGODOWN.TEMPNAME <> "" Then CMBGODOWN.Text = OBJGODOWN.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBGODOWN.Validating
        Try
            If CMBGODOWN.Text.Trim <> "" Then GODOWNVALIDATE(CMBGODOWN, e, Me, "AND GODOWN_ISOUR='TRUE'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGREYQUALITY_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBGREYQUALITY.Enter
        Try
            If CMBGREYQUALITY.Text.Trim = "" Then FILLGREY(CMBGREYQUALITY, edit, " AND GREY_TYPE ='FINISHED'")
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
                OBJGREY.WHERECLAUSE = " AND GREY_TYPE ='FINISHED'"
                OBJGREY.ShowDialog()
                If OBJGREY.TEMPNAME <> "" Then CMBGREYQUALITY.Text = OBJGREY.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGREYQUALITY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBGREYQUALITY.Validating
        Try
            If CMBGREYQUALITY.Text.Trim <> "" Then GREYVALIDATE(CMBGREYQUALITY, e, Me, " AND GREY_TYPE = 'FINISHED'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGREYFILTER_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBGREYFILTER.Validated
        Try
            If CMBGREYFILTER.Text.Trim <> "" Then
                GRIDSTOCK.RowCount = 0
                Dim objclsCMST As New ClsCommonMaster
                Dim dt As New DataTable
                dt = objclsCMST.search("ISNULL(STOCKMASTER_BALEFINISHED.SMBALEFINISHED_NO, 0) AS FINISHEDGREYSTOCK,ISNULL(LEDGERS.ACC_CMPNAME,'') AS PROCESSOR, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN, ISNULL(GREYQUALITYMASTER.GREY_NAME, '') AS GREY, ISNULL(STOCKMASTER_BALEFINISHED.SMBALEFINISHED_LOTNO, '') AS LOTNO, ISNULL(STOCKMASTER_BALEFINISHED.SMBALEFINISHED_BALENO, '') AS BALENO, ISNULL(STOCKMASTER_BALEFINISHED.SMBALEFINISHED_TAKA, 0) AS TAKA, ISNULL(STOCKMASTER_BALEFINISHED.SMBALEFINISHED_MTRS, 0) AS MTRS, ISNULL(STOCKMASTER_BALEFINISHED.SMBALEFINISHED_REMARKS, '') AS REMARKS, ISNULL(STOCKMASTER_BALEFINISHED.SMBALEFINISHED_DONE, 0) AS DONE", " ", " STOCKMASTER_BALEFINISHED LEFT OUTER JOIN LEDGERS ON SMBALEFINISHED_LEDGERID = LEDGERS.ACC_ID LEFT OUTER JOIN GODOWNMASTER ON STOCKMASTER_BALEFINISHED.SMBALEFINISHED_GODOWNID = GODOWNMASTER.GODOWN_ID INNER JOIN GREYQUALITYMASTER ON STOCKMASTER_BALEFINISHED.SMBALEFINISHED_GREYID = GREYQUALITYMASTER.GREY_ID", " AND GREYQUALITYMASTER.GREY_NAME = '" & CMBGREYFILTER.Text.Trim & "' AND STOCKMASTER_BALEFINISHED.SMBALEFINISHED_YEARID = " & YearId & " ORDER BY STOCKMASTER_BALEFINISHED.SMBALEFINISHED_NO")
                If dt.Rows.Count > 0 Then
                    For Each ROW As DataRow In dt.Rows
                        GRIDSTOCK.Rows.Add(Val(ROW("FINISHEDGREYSTOCK")), ROW("PROCESSOR"), ROW("GODOWN"), ROW("GREY"), ROW("LOTNO"), ROW("BALENO"), Val(ROW("TAKA")), Format(Val(ROW("MTRS")), "0.00"), ROW("REMARKS"), ROW("DONE"))
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
End Class