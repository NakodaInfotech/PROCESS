
Imports BL

Public Class FinishedBaleStockJobber

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
        TXTFINISHEDSAREESTOCK.Clear()
        CMBGREYFILTER.Text = ""
        CMBJOBBER.Text = ""
        CMBGREYQUALITY.Text = ""
        TXTLOTNO.Clear()
        TXTBALENO.Clear()
        TXTTAKA.Clear()
        TXTMTRS.Clear()
        TXTREMARKS.Clear()
    End Sub

    Private Sub FinishedStockSareeJobber_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        fillname(CMBJOBBER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'JOBBER'")
        FILLGREY(CMBGREYFILTER, edit, " AND GREY_TYPE = 'FINISHED'")
        FILLGREY(CMBGREYQUALITY, edit, " AND GREY_TYPE = 'FINISHED'")
    End Sub

    Private Sub CMBJOBBER_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBJOBBER.Enter
        Try
            If CMBJOBBER.Text.Trim = "" Then fillname(CMBJOBBER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'JOBBER'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBJOBBER_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBJOBBER.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'JOBBER'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBJOBBER.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBJOBBER_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBJOBBER.Validating
        Try
            If CMBJOBBER.Text.Trim <> "" Then namevalidate(CMBJOBBER, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'JOBBER'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "JOBBER")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()

        Try
            Dim dttable As New DataTable
            Dim OBJOPGREY As New ClsFinishedBaleStockJobber

            OBJOPGREY.alParaval.Add(TEMPFINISHEDBALESTOCK)
            OBJOPGREY.alParaval.Add(YearId)
            dttable = OBJOPGREY.GETSTOCKGREY()

            If dttable.Rows.Count > 0 Then
                'ITEM GRID
                For Each ROW As DataRow In dttable.Rows
                    GRIDSTOCK.Rows.Add(Val(ROW("FINISHEDGREYSTOCK")), ROW("JOBBER"), ROW("GREY"), ROW("LOTNO"), ROW("BALENO"), Val(ROW("TAKA")), Format(Val(ROW("MTRS")), "0.00"), ROW("REMARKS"), ROW("DONE"))
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
                If (GRIDDOUBLECLICK = False And LCase(CMBJOBBER.Text.Trim) = LCase(ROW.Cells(GJOBBER.Index).Value) And Val(TXTLOTNO.Text.Trim) = Val(ROW.Cells(GLOTNO.Index).Value) And LCase(TXTBALENO.Text.Trim) = LCase(ROW.Cells(GBALENO.Index).Value)) Then
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

        If CMBJOBBER.Text.Trim <> "" And CMBGREYQUALITY.Text.Trim <> "" And Val(TXTLOTNO.Text.Trim) > 0 And Val(TXTTAKA.Text.Trim) > 0 And Val(TXTMTRS.Text.Trim) > 0 Then

            If USERADD = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If Not CHECKDUPLICATE() Then
                MsgBox("Bale No Already Exists", MsgBoxStyle.Critical)
                Exit Sub
            End If


            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(CMBJOBBER.Text.Trim)
            ALPARAVAL.Add(CMBGREYQUALITY.Text.Trim)
            ALPARAVAL.Add(TXTLOTNO.Text.Trim)
            ALPARAVAL.Add(TXTBALENO.Text.Trim)
            ALPARAVAL.Add(Val(TXTTAKA.Text.Trim))
            ALPARAVAL.Add(Format(Val(TXTMTRS.Text.Trim), "0.00"))
            ALPARAVAL.Add(TXTREMARKS.Text.Trim)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)


            Dim OBJFINGREYSTOCK As New ClsFinishedBaleStockJobber
            OBJFINGREYSTOCK.alParaval = ALPARAVAL

            If edit = False Then
                Dim DT As DataTable = OBJFINGREYSTOCK.SAVE()
            Else
                ALPARAVAL.Add(Val(TXTFINISHEDSAREESTOCK.Text))
                Dim INTRES As Integer = OBJFINGREYSTOCK.UPDATE()
                GRIDDOUBLECLICK = False
                edit = False
            End If

            GRIDSTOCK.RowCount = 0
            FILLGRID()
            CLEAR()
            CMBJOBBER.Focus()
        Else
            MsgBox("Enter Proper Details")
        End If
    End Sub

    Private Sub FinishedStockSareeJobber_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
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
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDSTOCK_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDSTOCK.CellDoubleClick
        Try
            If e.RowIndex < 0 Then Exit Sub

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If Convert.ToBoolean(GRIDSTOCK.Item(GDONE.Index, e.RowIndex).Value) = True Then
                MsgBox("Lot No Locked", MsgBoxStyle.Critical)
                Exit Sub
            End If

            TXTFINISHEDSAREESTOCK.Text = GRIDSTOCK.Item(GOPFINISHEDGREYSTOCK.Index, e.RowIndex).Value
            CMBJOBBER.Text = GRIDSTOCK.Item(GJOBBER.Index, e.RowIndex).Value
            CMBGREYQUALITY.Text = GRIDSTOCK.Item(GGREY.Index, e.RowIndex).Value
            TXTLOTNO.Text = GRIDSTOCK.Item(GLOTNO.Index, e.RowIndex).Value
            TXTBALENO.Text = GRIDSTOCK.Item(GBALENO.Index, e.RowIndex).Value
            TXTTAKA.Text = GRIDSTOCK.Item(GTAKA.Index, e.RowIndex).Value
            TXTMTRS.Text = GRIDSTOCK.Item(GMTRS.Index, e.RowIndex).Value
            TXTREMARKS.Text = GRIDSTOCK.Item(GREMARKS.Index, e.RowIndex).Value

            GRIDDOUBLECLICK = True
            edit = True
            TEMPROW = e.RowIndex
            CMBJOBBER.Focus()

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

                    If Convert.ToBoolean(GRIDSTOCK.CurrentRow.Cells(GDONE.Index).Value) = True Then
                        MsgBox("Lot No Locked", MsgBoxStyle.Critical)
                        Exit Sub
                    End If

                    Dim TEMPMSG As Integer = MsgBox("Delete Details", MsgBoxStyle.YesNo)
                    If TEMPMSG = vbYes Then
                        Dim ALPARAVAL As New ArrayList
                        Dim OBJNO As New ClsFinishedBaleStockJobber

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
                dt = objclsCMST.search("ISNULL(STOCKMASTER_BALEJOBBER.SMBALEJOBBER_NO, 0) AS FINISHEDGREYSTOCK,ISNULL(LEDGERS.ACC_CMPNAME,'') AS JOBBER, ISNULL(GREYQUALITYMASTER.GREY_NAME, '') AS GREY, ISNULL(STOCKMASTER_BALEJOBBER.SMBALEJOBBER_LOTNO, '') AS LOTNO, ISNULL(STOCKMASTER_BALEJOBBER.SMBALEJOBBER_BALENO, '') AS BALENO, ISNULL(STOCKMASTER_BALEJOBBER.SMBALEJOBBER_TAKA, 0) AS TAKA, ISNULL(STOCKMASTER_BALEJOBBER.SMBALEJOBBER_MTRS, 0) AS MTRS, ISNULL(STOCKMASTER_BALEJOBBER.SMBALEJOBBER_REMARKS, '') AS REMARKS, ISNULL(STOCKMASTER_BALEJOBBER.SMBALEJOBBER_DONE, 0) AS DONE", " ", " STOCKMASTER_BALEJOBBER INNER JOIN LEDGERS ON SMBALEJOBBER_LEDGERID = LEDGERS.ACC_ID INNER JOIN GREYQUALITYMASTER ON STOCKMASTER_BALEJOBBER.SMBALEJOBBER_GREYID = GREYQUALITYMASTER.GREY_ID", " AND GREYQUALITYMASTER.GREY_NAME = '" & CMBGREYFILTER.Text.Trim & "' AND STOCKMASTER_BALEJOBBER.SMBALEJOBBER_YEARID = " & YearId & " ORDER BY STOCKMASTER_BALEJOBBER.SMBALEJOBBER_NO")
                If dt.Rows.Count > 0 Then
                    For Each ROW As DataRow In dt.Rows
                        GRIDSTOCK.Rows.Add(Val(ROW("FINISHEDGREYSTOCK")), ROW("JOBBER"), ROW("GREY"), ROW("LOTNO"), ROW("BALENO"), Val(ROW("TAKA")), Format(Val(ROW("MTRS")), "0.00"), ROW("REMARKS"), ROW("DONE"))
                        If Convert.ToBoolean(ROW("DONE")) = True Then GRIDSTOCK.Rows(GRIDSTOCK.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
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