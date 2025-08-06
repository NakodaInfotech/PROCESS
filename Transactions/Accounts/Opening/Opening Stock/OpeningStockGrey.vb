
Imports System.ComponentModel
Imports BL

Public Class OpeningStockGrey
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public edit As Boolean
    Public tempMsg As Integer
    Public TEMPOPGREYSTOCKNO As Integer
    Public FRMSTRING As String

    Sub TOTAL()
        Try
            LBLTOTALTAKA.Text = 0.0
            LBLTOTALMTRS.Text = 0.0
            LBLTOTALWT.Text = 0.0
            LBLTOTALWTMTRS.Text = 0.0


            For Each ROW As DataGridViewRow In GRIDSTOCK.Rows
                If ROW.Cells(GOPGREYSTOCKNO.Index).Value <> Nothing Then
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
        TXTOPGREYSTOCKNO.Clear()
        CMBOURGODOWN.Text = GETDEFAULTGODOWN()
        CMBGREYQUALITYFILTER.Text = ""
        CMBGREYQUALITY.Text = ""
        CMBSHADE.Text = ""
        TXTTAKANO.Clear()
        TXTPCS.Clear()
        TXTMTRS.Clear()
        TXTWT.Clear()
        TXTQUALITYWT.Clear()
        TXTREMARKS.Clear()
    End Sub

    Private Sub OpeningStockGrey_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        FILLGREY(CMBGREYQUALITYFILTER, edit)
        FILLGREY(CMBGREYQUALITYFILTER, edit)
        FILLCOLOR(CMBSHADE)
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
            Dim OBJOPSTOCK As New ClsOpeningStockGrey

            OBJOPSTOCK.alParaval.Add(0)
            OBJOPSTOCK.alParaval.Add(YearId)
            dttable = OBJOPSTOCK.GETSTOCKGREY()

            If dttable.Rows.Count > 0 Then
                'ITEM GRID
                For Each ROW As DataRow In dttable.Rows
                    GRIDSTOCK.Rows.Add(Val(ROW("OPGREYSTOCKNO")), ROW("GODOWN"), ROW("GREY"), ROW("SHADE"), ROW("TAKANO"), Val(ROW("TAKA")), Format(Val(ROW("MTRS")), "0.00"), Format(Val(ROW("WT")), "0.000"), Format(Val(ROW("WTMTR")), "0.000"), ROW("REMARKS"), Val(ROW("OUTPCS")), Val(ROW("OUTMTRS")))
                    If Val(ROW("OUTPCS")) > 0 Or Val(ROW("OUTMTRS")) > 0 Then GRIDSTOCK.Rows(GRIDSTOCK.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
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
        Try
            If CMBGREYQUALITY.Text.Trim <> "" And Val(TXTPCS.Text.Trim) > 0 And Val(TXTMTRS.Text.Trim) > 0 And Val(TXTWT.Text.Trim) > 0 Then

                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim ALPARAVAL As New ArrayList

                ALPARAVAL.Add(CMBOURGODOWN.Text.Trim)
                ALPARAVAL.Add(CMBGREYQUALITY.Text.Trim)
                ALPARAVAL.Add(CMBSHADE.Text.Trim)
                ALPARAVAL.Add(TXTTAKANO.Text.Trim)
                ALPARAVAL.Add(Val(TXTPCS.Text.Trim))
                ALPARAVAL.Add(Format(Val(TXTMTRS.Text.Trim), "0.00"))
                ALPARAVAL.Add(Format(Val(TXTWT.Text.Trim), "0.000"))
                ALPARAVAL.Add(Format(Val(TXTQUALITYWT.Text.Trim), "0.000"))
                ALPARAVAL.Add(TXTREMARKS.Text.Trim)
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(Userid)
                ALPARAVAL.Add(YearId)


                Dim OBJOPENSTOCK As New ClsOpeningStockGrey
                OBJOPENSTOCK.alParaval = ALPARAVAL

                If edit = False Then
                    Dim DT As DataTable = OBJOPENSTOCK.SAVE()
                Else
                    ALPARAVAL.Add(Val(TXTOPGREYSTOCKNO.Text))
                    Dim INTRES As Integer = OBJOPENSTOCK.UPDATE()
                    GRIDDOUBLECLICK = False
                    edit = False
                End If

                GRIDSTOCK.RowCount = 0
                FILLGRID()
                CLEAR()
                CMBGREYQUALITY.Focus()
                TOTAL()

            Else
                MsgBox("Enter Proper Details")
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub OpeningStockGrey_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
        CLEAR()
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

            If GRIDSTOCK.Item(GOUTPCS.Index, e.RowIndex).Value > 0 Or GRIDSTOCK.Item(GOUTMTRS.Index, e.RowIndex).Value > 0 Then
                MsgBox("Yarn Locked, it is used further", MsgBoxStyle.Critical)
                Exit Sub
            End If


            TXTOPGREYSTOCKNO.Text = GRIDSTOCK.Item(GOPGREYSTOCKNO.Index, e.RowIndex).Value
            CMBOURGODOWN.Text = GRIDSTOCK.Item(GGODOWN.Index, e.RowIndex).Value
            CMBGREYQUALITY.Text = GRIDSTOCK.Item(GGREY.Index, e.RowIndex).Value
            CMBSHADE.Text = GRIDSTOCK.Item(GSHADE.Index, e.RowIndex).Value
            TXTTAKANO.Text = GRIDSTOCK.Item(GTAKANO.Index, e.RowIndex).Value
            TXTPCS.Text = GRIDSTOCK.Item(GTAKA.Index, e.RowIndex).Value
            TXTMTRS.Text = GRIDSTOCK.Item(GMTRS.Index, e.RowIndex).Value
            TXTWT.Text = GRIDSTOCK.Item(GWT.Index, e.RowIndex).Value
            TXTQUALITYWT.Text = Val(GRIDSTOCK.Item(GWTMTR.Index, e.RowIndex).Value)
            TXTREMARKS.Text = GRIDSTOCK.Item(GREMARKS.Index, e.RowIndex).Value

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

                    If GRIDSTOCK.Item(GOUTPCS.Index, GRIDSTOCK.CurrentRow.Index).Value > 0 Or GRIDSTOCK.Item(GOUTMTRS.Index, GRIDSTOCK.CurrentRow.Index).Value > 0 Then
                        MsgBox("Yarn Locked, it is used further", MsgBoxStyle.Critical)
                        Exit Sub
                    End If

                    Dim TEMPMSG As Integer = MsgBox("Delete Details", MsgBoxStyle.YesNo)
                    If TEMPMSG = vbYes Then
                        Dim ALPARAVAL As New ArrayList
                        Dim OBJNO As New ClsOpeningStockGrey

                        OBJNO.alParaval = ALPARAVAL
                        ALPARAVAL.Add(GRIDSTOCK.Rows(GRIDSTOCK.CurrentRow.Index).Cells(GOPGREYSTOCKNO.Index).Value.ToString)
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

    Private Sub TXTTAKA_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTPCS.KeyPress, TXTMTRS.KeyPress, TXTWT.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub CMBGREYQUALITYFILTER_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBGREYQUALITYFILTER.Enter
        Try
            If CMBGREYQUALITYFILTER.Text.Trim = "" Then FILLGREY(CMBGREYQUALITYFILTER, edit)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGREYQUALITYFILTER_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBGREYQUALITYFILTER.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJGREY As New SelectGreyQuality
                OBJGREY.ShowDialog()
                If OBJGREY.TEMPNAME <> "" Then CMBGREYQUALITYFILTER.Text = OBJGREY.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGREYQUALITYFILTER_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBGREYQUALITYFILTER.Validating
        Try
            If CMBGREYQUALITYFILTER.Text.Trim <> "" Then GREYVALIDATE(CMBGREYQUALITYFILTER, e, Me)
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
                If OBJGREY.TEMPNAME <> "" Then CMBGREYQUALITYFILTER.Text = OBJGREY.TEMPNAME
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

    Private Sub TXTMTRS_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTMTRS.Validating
        If Val(TXTMTRS.Text.Trim) > 0 Then TXTWT.Text = Format((Val(TXTQUALITYWT.Text.Trim) * Val(TXTMTRS.Text.Trim)) / 100, "0.000")
    End Sub

    Private Sub CMBGREYQUALITYFILTER_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBGREYQUALITYFILTER.Validated
        Try
            If CMBGREYQUALITYFILTER.Text.Trim <> "" Then
                GRIDSTOCK.RowCount = 0
                Dim objclsCMST As New ClsCommonMaster
                Dim dt As DataTable = objclsCMST.search("ISNULL(STOCKMASTER_GREY.SMGREY_NO, 0) AS OPGREYSTOCKNO, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN, ISNULL(GREYQUALITYMASTER.GREY_NAME, '') AS GREY, ISNULL(STOCKMASTER_GREY.SMGREY_TAKA, 0) AS TAKA, ISNULL(STOCKMASTER_GREY.SMGREY_MTRS, 0) AS MTRS, ISNULL(STOCKMASTER_GREY.SMGREY_WT, 0) AS WT, ISNULL(STOCKMASTER_GREY.SMGREY_WTMTR, 0) AS WTMTR, ISNULL(STOCKMASTER_GREY.SMGREY_REMARKS, '') AS REMARKS, ISNULL(STOCKMASTER_GREY.SMGREY_TAKANO, 0) AS TAKANO, ISNULL(STOCKMASTER_GREY.SMGREY_OUTPCS, 0) AS OUTPCS, ISNULL(STOCKMASTER_GREY.SMGREY_OUTMTRS, 0) AS OUTMTRS, ISNULL(COLORMASTER.COLOR_NAME, '') AS SHADE", " ", " STOCKMASTER_GREY INNER JOIN GREYQUALITYMASTER ON STOCKMASTER_GREY.SMGREY_GREYID = GREYQUALITYMASTER.GREY_ID AND STOCKMASTER_GREY.SMGREY_YEARID = GREYQUALITYMASTER.GREY_YEARID INNER JOIN GODOWNMASTER ON STOCKMASTER_GREY.SMGREY_GODOWNID = GODOWNMASTER.GODOWN_ID LEFT OUTER JOIN COLORMASTER ON STOCKMASTER_GREY.SMGREY_SHADEID = COLORMASTER.COLOR_ID", " AND GREYQUALITYMASTER.GREY_NAME = '" & CMBGREYQUALITYFILTER.Text.Trim & "' AND STOCKMASTER_GREY.SMGREY_YEARID = " & YearId & " ORDER BY STOCKMASTER_GREY.SMGREY_NO")
                If dt.Rows.Count > 0 Then
                    For Each ROW As DataRow In dt.Rows
                        GRIDSTOCK.Rows.Add(Val(ROW("OPGREYSTOCKNO")), ROW("GODOWN"), ROW("GREY"), ROW("SHADE"), Val(ROW("TAKANO")), Val(ROW("TAKA")), Format(Val(ROW("MTRS")), "0.00"), Format(Val(ROW("WT")), "0.000"), Format(Val(ROW("WTMTR")), "0.000"), ROW("REMARKS"), Val(ROW("OUTPCS")), Val(ROW("OUTMTRS")))
                        If Val(ROW("OUTPCS")) > 0 Or Val(ROW("OUTMTRS")) > 0 Then GRIDSTOCK.Rows(GRIDSTOCK.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
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
                    TXTMTRS.Text = Format(Val(TXTPCS.Text.Trim) * DT.Rows(0).Item("MTRS"), "0.000")
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

    Private Sub CMBSHADE_Enter(sender As Object, e As EventArgs) Handles CMBSHADE.Enter
        Try
            If CMBSHADE.Text.Trim = "" Then FILLCOLOR(CMBSHADE)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSHADE_Validating(sender As Object, e As CancelEventArgs) Handles CMBSHADE.Validating
        Try
            If CMBSHADE.Text.Trim <> "" Then COLORVALIDATE(CMBSHADE, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class