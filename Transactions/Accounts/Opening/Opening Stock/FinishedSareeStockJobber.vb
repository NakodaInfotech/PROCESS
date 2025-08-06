
Imports BL

Public Class FinishedSareeStockJobber

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public edit As Boolean
    Public tempMsg As Integer
    Public TEMPFINISHEDSAREESTOCK As Integer

    Sub TOTAL()
        Try
            LBLTOTALPCS.Text = 0.0
            LBLTOTALMTRS.Text = 0.0

            For Each ROW As DataGridViewRow In GRIDSTOCK.Rows
                If ROW.Cells(GOPFINISHEDSAREESTOCK.Index).Value <> Nothing Then
                    LBLTOTALPCS.Text = Format(Val(LBLTOTALPCS.Text) + Val(ROW.Cells(GPCS.Index).EditedFormattedValue), "0.00")
                    LBLTOTALMTRS.Text = Format(Val(LBLTOTALMTRS.Text) + Val(ROW.Cells(GMTRS.Index).EditedFormattedValue), "0.00")
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()
        TXTFINISHEDSAREESTOCK.Clear()
        CMBMERCHANTFILTER.Text = ""
        CMBJOBBER.Text = ""
        CMBMERCHANT.Text = ""
        TXTLOTNO.Clear()
        TXTBALENO.Clear()
        TXTPCS.Clear()
        TXTCUT.Clear()
        TXTMTRS.Clear()
        TXTREMARKS.Clear()
    End Sub

    Private Sub FinishedSareeStockJobber_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        FILLGREY(CMBMERCHANTFILTER, edit, " AND GREY_TYPE = 'SAREE'")
        FILLGREY(CMBMERCHANT, edit, " AND GREY_TYPE = 'SAREE'")
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
            If CMBJOBBER.Text.Trim <> "" Then namevalidate(CMBJOBBER, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'JOBBER'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "PROCESSOR")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()

        Try
            Dim dttable As New DataTable
            Dim OBJOPGREY As New ClsFinishedSareeStockJobber

            OBJOPGREY.alParaval.Add(TEMPFINISHEDSAREESTOCK)
            OBJOPGREY.alParaval.Add(YearId)
            dttable = OBJOPGREY.GETSTOCKMERCHANT()

            If dttable.Rows.Count > 0 Then
                'ITEM GRID
                For Each ROW As DataRow In dttable.Rows
                    GRIDSTOCK.Rows.Add(Val(ROW("FINISHEDSAREESTOCK")), ROW("JOBBER"), ROW("MERCHANT"), Val(ROW("LOTNO")), ROW("BALENO"), Val(ROW("PCS")), Format(Val(ROW("CUT")), "0.00"), Format(Val(ROW("MTRS")), "0.00"), ROW("REMARKS"), Val(ROW("OUTPCS")), Val(ROW("OUTMTRS")))
                    If Val(ROW("OUTPCS")) > 0 Then GRIDSTOCK.Rows(GRIDSTOCK.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
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
                If (GRIDDOUBLECLICK = False And LCase(CMBJOBBER.Text.Trim) = LCase(ROW.Cells(GJOBBER.Index).Value) And LCase(TXTLOTNO.Text.Trim) = LCase(ROW.Cells(GLOTNO.Index).Value)) Then
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

        If CMBMERCHANT.Text.Trim <> "" And CMBJOBBER.Text.Trim <> "" And Val(TXTLOTNO.Text.Trim) > 0 And Val(TXTPCS.Text.Trim) > 0 And Val(TXTCUT.Text.Trim) > 0 And Val(TXTMTRS.Text.Trim) > 0 Then

            If USERADD = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If Not CHECKDUPLICATE() Then
                MsgBox("Lot No For Selected Jobber Already Exists", MsgBoxStyle.Critical)
                Exit Sub
            End If


            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(CMBJOBBER.Text.Trim)
            ALPARAVAL.Add(CMBMERCHANT.Text.Trim)
            ALPARAVAL.Add(Val(TXTLOTNO.Text.Trim))
            ALPARAVAL.Add(TXTBALENO.Text.Trim)
            ALPARAVAL.Add(Val(TXTPCS.Text.Trim))
            ALPARAVAL.Add(Format(Val(TXTCUT.Text.Trim), "0.00"))
            ALPARAVAL.Add(Format(Val(TXTMTRS.Text.Trim), "0.00"))
            ALPARAVAL.Add(TXTREMARKS.Text.Trim)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)


            Dim OBJFINISHEDSAREESTOCK As New ClsFinishedSareeStockJobber
            OBJFINISHEDSAREESTOCK.alParaval = ALPARAVAL

            If edit = False Then
                Dim DT As DataTable = OBJFINISHEDSAREESTOCK.SAVE()
            Else
                ALPARAVAL.Add(Val(TXTFINISHEDSAREESTOCK.Text))
                Dim INTRES As Integer = OBJFINISHEDSAREESTOCK.UPDATE()
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

    Private Sub FinishedSareeStockJobber_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            If Val(GRIDSTOCK.Item(GOUTPCS.Index, e.RowIndex).Value) > 0 Then
                MsgBox("Entry Locked", MsgBoxStyle.Critical)
                Exit Sub
            End If

            TXTFINISHEDSAREESTOCK.Text = GRIDSTOCK.Item(GOPFINISHEDSAREESTOCK.Index, e.RowIndex).Value
            CMBJOBBER.Text = GRIDSTOCK.Item(GJOBBER.Index, e.RowIndex).Value
            CMBMERCHANT.Text = GRIDSTOCK.Item(GGREY.Index, e.RowIndex).Value
            TXTLOTNO.Text = Val(GRIDSTOCK.Item(GLOTNO.Index, e.RowIndex).Value)
            TXTBALENO.Text = GRIDSTOCK.Item(GBALENO.Index, e.RowIndex).Value
            TXTPCS.Text = Val(GRIDSTOCK.Item(GPCS.Index, e.RowIndex).Value)
            TXTCUT.Text = Val(GRIDSTOCK.Item(GCUT.Index, e.RowIndex).Value)
            TXTMTRS.Text = Val(GRIDSTOCK.Item(GMTRS.Index, e.RowIndex).Value)
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

                    If Val(GRIDSTOCK.Item(GOUTPCS.Index, GRIDSTOCK.CurrentRow.Index).Value) > 0 Then
                        MsgBox("Bale Locked", MsgBoxStyle.Critical)
                        Exit Sub
                    End If

                    Dim TEMPMSG As Integer = MsgBox("Delete Details", MsgBoxStyle.YesNo)
                    If TEMPMSG = vbYes Then
                        Dim ALPARAVAL As New ArrayList
                        Dim OBJNO As New ClsFinishedSareeStockJobber

                        OBJNO.alParaval = ALPARAVAL
                        ALPARAVAL.Add(GRIDSTOCK.Rows(GRIDSTOCK.CurrentRow.Index).Cells(GOPFINISHEDSAREESTOCK.Index).Value)
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

    Private Sub TXTPCS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTPCS.KeyPress, TXTLOTNO.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTMTRS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTMTRS.KeyPress, TXTCUT.KeyPress
        numdot(e, sender, Me)
    End Sub

    Private Sub CMBMERCHANTFILTER_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBMERCHANTFILTER.Enter
        Try
            If CMBMERCHANTFILTER.Text.Trim = "" Then FILLGREY(CMBMERCHANTFILTER, edit, " AND GREY_TYPE = 'SAREE'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMERCHANTFILTER_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBMERCHANTFILTER.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJGREY As New SelectGreyQuality
                OBJGREY.WHERECLAUSE = " AND GREY_TYPE ='SAREE'"
                OBJGREY.ShowDialog()
                If OBJGREY.TEMPNAME <> "" Then CMBMERCHANTFILTER.Text = OBJGREY.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMERCHANTFILTER_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBMERCHANTFILTER.Validating
        Try
            If CMBMERCHANTFILTER.Text.Trim <> "" Then GREYVALIDATE(CMBMERCHANTFILTER, e, Me, " AND GREY_TYPE = 'SAREE'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMERCHANT_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBMERCHANT.Enter
        Try
            If CMBMERCHANT.Text.Trim = "" Then FILLGREY(CMBMERCHANT, edit, " AND GREY_TYPE ='SAREE'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMERCHANT_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBMERCHANT.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJGREY As New SelectGreyQuality
                OBJGREY.WHERECLAUSE = " AND GREY_TYPE ='SAREE'"
                OBJGREY.ShowDialog()
                If OBJGREY.TEMPNAME <> "" Then CMBMERCHANT.Text = OBJGREY.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMERCHANT_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBMERCHANT.Validating
        Try
            If CMBMERCHANT.Text.Trim <> "" Then GREYVALIDATE(CMBMERCHANT, e, Me, " AND GREY_TYPE = 'SAREE'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMERCHANTFILTER_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBMERCHANTFILTER.Validated
        Try
            If CMBMERCHANTFILTER.Text.Trim <> "" Then
                GRIDSTOCK.RowCount = 0
                Dim objclsCMST As New ClsCommonMaster
                Dim dt As New DataTable
                dt = objclsCMST.search("ISNULL(STOCKMASTER_SAREEJOBBER.SMSAREEJOBBER_NO, 0) AS FINISHEDSAREESTOCK,ISNULL(LEDGERS.ACC_CMPNAME,'') AS JOBBER, ISNULL(GREYQUALITYMASTER.GREY_NAME, '') AS MERCHANT, ISNULL(STOCKMASTER_SAREEJOBBER.SMSAREEJOBBER_BALENO, 0) AS LOTNO, ISNULL(STOCKMASTER_SAREEJOBBER.SMSAREEJOBBER_BALENO, '') AS BALENO, ISNULL(STOCKMASTER_SAREEJOBBER.SMSAREEJOBBER_PCS, 0) AS PCS, ISNULL(STOCKMASTER_SAREEJOBBER.SMSAREEJOBBER_CUT, 0) AS CUT, ISNULL(STOCKMASTER_SAREEJOBBER.SMSAREEJOBBER_MTRS, 0) AS MTRS, ISNULL(STOCKMASTER_SAREEJOBBER.SMSAREEJOBBER_REMARKS, '') AS REMARKS, ISNULL(STOCKMASTER_SAREEJOBBER.SMSAREEJOBBER_OUTPCS, 0) AS OUTPCS, ISNULL(STOCKMASTER_SAREEJOBBER.SMSAREEJOBBER_OUTMTRS, 0) AS OUTMTRS", " ", " STOCKMASTER_SAREEJOBBER INNER JOIN LEDGERS ON SMSAREEJOBBER_LEDGERID = LEDGERS.ACC_ID INNER JOIN GREYQUALITYMASTER ON STOCKMASTER_SAREEJOBBER.SMSAREEJOBBER_MERCHANTID = GREYQUALITYMASTER.GREY_ID", " AND GREYQUALITYMASTER.GREY_NAME = '" & CMBMERCHANTFILTER.Text.Trim & "' AND STOCKMASTER_SAREEJOBBER.SMSAREEJOBBER_YEARID = " & YearId & " ORDER BY STOCKMASTER_SAREEJOBBER.SMSAREEJOBBER_NO")
                If dt.Rows.Count > 0 Then
                    For Each ROW As DataRow In dt.Rows
                        GRIDSTOCK.Rows.Add(Val(ROW("FINISHEDSAREESTOCK")), ROW("JOBBER"), ROW("MERCHANT"), Val(ROW("LOTNO")), ROW("BALENO"), Val(ROW("PCS")), Format(Val(ROW("CUT")), "0.00"), Format(Val(ROW("MTRS")), "0.00"), ROW("REMARKS"), Val(ROW("OUTPCS")), Val(ROW("OUTMTRS")))
                        If Val(ROW("OUTPCS")) > 0 Then GRIDSTOCK.Rows(GRIDSTOCK.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
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

    Sub CALC()
        Try
            If Val(TXTCUT.Text.Trim) > 0 And Val(TXTPCS.Text.Trim) > 0 Then TXTMTRS.Text = Format(Val(TXTPCS.Text.Trim) * Val(TXTCUT.Text.Trim), "0.00")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TXTCUT_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTCUT.Validated, TXTPCS.Validated
        CALC()
    End Sub
End Class