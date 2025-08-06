Imports BL
Imports System.Windows.Forms
Imports System.IO
Imports System.Diagnostics

Public Class OpeningStockBeamwithWeaverLoomWise

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public edit As Boolean
    Public tempMsg As Integer
    Public TEMPOPBEAMSTOCKWEAVER As Integer
    Public FRMSTRING As String

    Sub TOTAL()
        Try
            LBLTOTALCUT.Text = 0.0
            LBLTOTALWT.Text = 0.0

            For Each ROW As DataGridViewRow In GRIDSTOCK.Rows
                If ROW.Cells(GOPBEAMSTOCKWEAVER.Index).Value <> Nothing Then
                    LBLTOTALCUT.Text = Format(Val(LBLTOTALCUT.Text) + Val(ROW.Cells(GCUT.Index).EditedFormattedValue), "0.00")
                    LBLTOTALWT.Text = Format(Val(LBLTOTALWT.Text) + Val(ROW.Cells(GWT.Index).EditedFormattedValue), "0.000")
                End If
            Next
            LBLTOTALWTCUT.Text = Format(Val(LBLTOTALWT.Text) / Val(LBLTOTALCUT.Text.Trim), "0.000")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()
        TXTOPBEAMSTOCKWEAVER.Clear()
        'CMBWEAVER.Text = ""
        CMBBEAM.Text = ""
        TXTBEAMNO.Clear()
        CMBLOOMNO.Text = ""
        CMBLOOMNO.DataSource = Nothing
        TXTENDS.Clear()
        TXTTAPLINE.Clear()
        TXTCUT.Clear()
        TXTWT.Clear()
        TXTWTCUT.Clear()
        TXTREMARKS.Clear()
    End Sub

    Private Sub BeamStockwithWeaver_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        fillBEAM(CMBBEAM, edit)
        fillname(CMBWEAVERFILTER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'")
        fillname(CMBWEAVER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'")
    End Sub

    Sub FILLGRID()

        Try
            Dim dttable As New DataTable
            Dim OBJOPSTOCK As New ClsOpeningStockBeamWeaver

            OBJOPSTOCK.alParaval.Add(TEMPOPBEAMSTOCKWEAVER)
            OBJOPSTOCK.alParaval.Add(YearId)
            dttable = OBJOPSTOCK.GETSTOCKBEAM()

            If dttable.Rows.Count > 0 Then
                'ITEM GRID
                For Each ROW As DataRow In dttable.Rows
                    GRIDSTOCK.Rows.Add(Val(ROW("OPBEAMSTOCKNOWEAVER")), ROW("WEAVER"), ROW("BEAM"), ROW("BEAMNO"), ROW("LOOMNO"), Val(ROW("ENDS")), Val(ROW("TAPLINE")), Val(ROW("CUT")), Format(Val(ROW("WT")), "0.000"), Format(Val(ROW("WTCUT")), "0.000"), ROW("REMARKS"), ROW("OUTCUT"))
                    If Val(ROW("OUTCUT")) > 0 Or Convert.ToBoolean(ROW("DONE")) = True Then GRIDSTOCK.Rows(GRIDSTOCK.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
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

    Sub calc()
        If Val(TXTWTCUT.Text.Trim) = 0 Then
            TXTWTCUT.Text = Format(Val(TXTWT.Text.Trim) / Val(TXTCUT.Text.Trim), "0.000")
        Else
            TXTWT.Text = Format(Val(TXTWTCUT.Text.Trim) * Val(TXTCUT.Text.Trim), "0.000")
        End If
    End Sub

    Private Sub TXTREMARKS_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTREMARKS.Validating

        If CMBBEAM.Text.Trim <> "" And TXTBEAMNO.Text.Trim <> "" And Val(TXTCUT.Text.Trim) > 0 And Val(TXTWT.Text.Trim) > 0 And Val(TXTTAPLINE.Text.Trim) > 0 Then

            If USERADD = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(CMBWEAVER.Text.Trim)
            ALPARAVAL.Add(CMBBEAM.Text.Trim)
            ALPARAVAL.Add(TXTBEAMNO.Text.Trim)
            ALPARAVAL.Add(Val(CMBLOOMNO.Text.Trim))
            ALPARAVAL.Add(Val(TXTENDS.Text.Trim))
            ALPARAVAL.Add(Val(TXTTAPLINE.Text.Trim))
            ALPARAVAL.Add(Val((TXTCUT.Text.Trim)))
            ALPARAVAL.Add(Format(Val(TXTWT.Text.Trim), "0.000"))
            ALPARAVAL.Add(Format(Val(TXTWTCUT.Text.Trim), "0.000"))
            ALPARAVAL.Add(TXTREMARKS.Text.Trim)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)


            Dim OBJOPENSTOCK As New ClsOpeningStockBeamWeaver
            OBJOPENSTOCK.alParaval = ALPARAVAL

            If edit = False Then
                Dim DT As DataTable = OBJOPENSTOCK.SAVE()
            Else
                ALPARAVAL.Add(Val(TXTOPBEAMSTOCKWEAVER.Text))
                Dim INTRES As Integer = OBJOPENSTOCK.UPDATE()
                GRIDDOUBLECLICK = False
                edit = False
            End If

            GRIDSTOCK.RowCount = 0
            If CMBWEAVERFILTER.Text.Trim = "" Then FILLGRID() Else CMBWEAVERFILTER_Validated(sender, e)
            CLEAR()
            CMBBEAM.Focus()
        Else
            MsgBox("Enter Proper Details")
        End If
    End Sub

    Private Sub BeamStockwithWeaver_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            If Val(GRIDSTOCK.Rows(e.RowIndex).Cells(GOUTCUT.Index).Value) > 0 Then
                MsgBox("Unable to Modify, Entry Locked", MsgBoxStyle.Critical)
                Exit Sub
            End If

            TXTOPBEAMSTOCKWEAVER.Text = GRIDSTOCK.Item(GOPBEAMSTOCKWEAVER.Index, e.RowIndex).Value
            CMBWEAVER.Text = GRIDSTOCK.Item(GWEAVER.Index, e.RowIndex).Value
            CMBBEAM.Text = GRIDSTOCK.Item(GBEAM.Index, e.RowIndex).Value
            TXTBEAMNO.Text = GRIDSTOCK.Item(GBEAMNO.Index, e.RowIndex).Value
            CMBLOOMNO.Text = Val(GRIDSTOCK.Item(GLOOMNO.Index, e.RowIndex).Value)
            TXTENDS.Text = Val(GRIDSTOCK.Item(GENDS.Index, e.RowIndex).Value)
            TXTCUT.Text = GRIDSTOCK.Item(GCUT.Index, e.RowIndex).Value
            TXTWT.Text = GRIDSTOCK.Item(GWT.Index, e.RowIndex).Value
            TXTWTCUT.Text = Val(GRIDSTOCK.Item(GWTCUT.Index, e.RowIndex).Value)
            TXTREMARKS.Text = GRIDSTOCK.Item(GREMARKS.Index, e.RowIndex).Value

            GRIDDOUBLECLICK = True
            edit = True
            TEMPROW = e.RowIndex
            CMBWEAVER.Focus()

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

                    If Val(GRIDSTOCK.Rows(GRIDSTOCK.CurrentRow.Index).Cells(GOUTCUT.Index).Value) > 0 Then
                        MsgBox("Unable to Delete, Entry Locked", MsgBoxStyle.Critical)
                        Exit Sub
                    End If


                    Dim TEMPMSG As Integer = MsgBox("Delete Details", MsgBoxStyle.YesNo)
                    If TEMPMSG = vbYes Then
                        Dim ALPARAVAL As New ArrayList
                        Dim OBJNO As New ClsOpeningStockBeamWeaver

                        OBJNO.alParaval = ALPARAVAL
                        ALPARAVAL.Add(GRIDSTOCK.Rows(GRIDSTOCK.CurrentRow.Index).Cells(GOPBEAMSTOCKWEAVER.Index).Value)
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

    Private Sub TXTCUT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCUT.KeyPress
        numdotkeypress(e, TXTCUT, Me)
    End Sub

    Private Sub TXTWT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTWT.KeyPress
        numdot3(e, TXTWT, Me)
    End Sub

    Private Sub CMBWEAVERFILTER_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBWEAVERFILTER.Enter
        Try
            If CMBWEAVERFILTER.Text.Trim = "" Then fillname(CMBWEAVERFILTER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWEAVERFILTER_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBWEAVERFILTER.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBWEAVERFILTER.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWEAVERFILTER_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBWEAVERFILTER.Validating
        Try
            If CMBWEAVERFILTER.Text.Trim <> "" Then namevalidate(CMBWEAVERFILTER, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "WEAVER")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWEAVER_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBWEAVER.Enter
        Try
            If CMBWEAVER.Text.Trim = "" Then fillname(CMBWEAVER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWEAVER_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBWEAVER.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBWEAVER.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWEAVER_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBWEAVER.Validated
        Try
            If CMBWEAVER.Text.Trim <> "" Then FILLLOOMNO(CMBLOOMNO, CMBWEAVER.Text.Trim, edit, "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWEAVER_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBWEAVER.Validating
        Try
            If CMBWEAVER.Text.Trim <> "" Then namevalidate(CMBWEAVER, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "WEAVER")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBEAM_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBBEAM.Enter
        Try
            If CMBBEAM.Text.Trim = "" Then fillBEAM(CMBBEAM, edit)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBEAM_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBBEAM.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJBEAM As New SelectBeam
                OBJBEAM.ShowDialog()
                If OBJBEAM.TEMPNAME <> "" Then CMBBEAM.Text = OBJBEAM.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBEAM_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBBEAM.Validated
        Try
            If CMBBEAM.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("ISNULL(BEAM_ENDS, 0) AS ENDS, ISNULL(BEAM_TAPLINE, 0) AS TAPLINE, ISNULL(BEAM_WTMTRS,0) AS WTMtRS", "", "BEAMMASTER", "AND BEAMMASTER.BEAM_NAME = '" & CMBBEAM.Text.Trim & "' AND BEAM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    If Val(TXTENDS.Text.Trim) = 0 Then TXTENDS.Text = DT.Rows(0).Item("ENDS")
                    TXTTAPLINE.Text = DT.Rows(0).Item("TAPLINE")
                    TXTWTCUT.Text = Val(DT.Rows(0).Item("WTMTRS"))
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBEAM_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBBEAM.Validating
        Try
            If CMBBEAM.Text.Trim <> "" Then BEAMVALIDATE(CMBBEAM, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCUT_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTCUT.Validating
        calc()
    End Sub

    Private Sub TXTWT_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTWT.Validating
        calc()
    End Sub

    Private Sub CMBWEAVERFILTER_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBWEAVERFILTER.Validated
        Try
            If CMBWEAVERFILTER.Text.Trim <> "" Then
                GRIDSTOCK.RowCount = 0
                Dim objclsCMST As New ClsCommonMaster
                Dim dt As New DataTable
                dt = objclsCMST.search("ISNULL(STOCKMASTER_BEAMWEAVER.SMBEAMWEAVER_NO, 0) AS OPBEAMSTOCKWEAVER, ISNULL(STOCKMASTER_BEAMWEAVER.SMBEAMWEAVER_LOOMNO, 0) AS LOOMNO, ISNULL(WEAVER.Acc_cmpname, '') AS WEAVER, ISNULL(BEAMMASTER.BEAM_NAME, '') AS BEAM, ISNULL(STOCKMASTER_BEAMWEAVER.SMBEAMWEAVER_ENDS, 0) AS ENDS, ISNULL(BEAMMASTER.BEAM_TAPLINE, 0) AS TAPLINE, ISNULL(STOCKMASTER_BEAMWEAVER.SMBEAMWEAVER_BEAMNO, '') AS BEAMNO, ISNULL(STOCKMASTER_BEAMWEAVER.SMBEAMWEAVER_CUT, 0) AS CUT, ISNULL(STOCKMASTER_BEAMWEAVER.SMBEAMWEAVER_WT, 0) AS WT, ISNULL(STOCKMASTER_BEAMWEAVER.SMBEAMWEAVER_WTCUT, 0) AS WTCUT, ISNULL(STOCKMASTER_BEAMWEAVER.SMBEAMWEAVER_REMARKS, '') AS REMARKS", " ", " STOCKMASTER_BEAMWEAVER INNER JOIN BEAMMASTER ON STOCKMASTER_BEAMWEAVER.SMBEAMWEAVER_BEAMID = BEAMMASTER.BEAM_ID AND STOCKMASTER_BEAMWEAVER.SMBEAMWEAVER_YEARID = BEAMMASTER.BEAM_YEARID INNER JOIN LEDGERS AS WEAVER ON STOCKMASTER_BEAMWEAVER.SMBEAMWEAVER_WEAVERID = WEAVER.Acc_id", " AND WEAVER.ACC_CMPNAME = '" & CMBWEAVERFILTER.Text.Trim & "' AND STOCKMASTER_BEAMWEAVER.SMBEAMWEAVER_YEARID = " & YearId & " ORDER BY STOCKMASTER_BEAMWEAVER.SMBEAMWEAVER_NO")
                'gridstock.DataSource = dt
                If dt.Rows.Count > 0 Then
                    For Each ROW As DataRow In dt.Rows
                        GRIDSTOCK.Rows.Add(Val(ROW("OPBEAMSTOCKWEAVER")), ROW("WEAVER"), ROW("BEAM"), ROW("BEAMNO"), ROW("LOOMNO"), Val(ROW("ENDS")), Val(ROW("TAPLINE")), Val(ROW("CUT")), Format(Val(ROW("WT")), "0.000"), Format(Val(ROW("WTCUT")), "0.000"), ROW("REMARKS"))
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

    Private Sub CMBLOOMNO_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBLOOMNO.Enter
        If CMBWEAVER.Text.Trim <> "" Then FILLLOOMNO(CMBLOOMNO, CMBWEAVER.Text.Trim, edit, "")
    End Sub

    Private Sub CMBLOOMNO_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBLOOMNO.Validating
        Try
            If CMBLOOMNO.Text.Trim <> "" Then LOOMVALIDATE(CMBLOOMNO, CMBWEAVER.Text.Trim, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTENDS_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTENDS.KeyPress
        numkeypress(e, sender, Me)
    End Sub
End Class