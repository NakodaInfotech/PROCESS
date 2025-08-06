
Imports BL

Public Class OpeningStockBeam

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public edit As Boolean
    Public tempMsg As Integer
    Public TEMPOPBEAMSTOCKNO As Integer

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
            LBLTOTALBEAMS.Text = 0.0
            LBLTOTALCUT.Text = 0.0
            LBLTOTALWT.Text = 0.0
            LBLTOTALWTCUT.Text = 0.0

            For Each ROW As DataGridViewRow In GRIDSTOCK.Rows
                If ROW.Cells(GOPBEAMSTOCKNO.Index).Value <> Nothing Then
                    LBLTOTALCUT.Text = Format(Val(LBLTOTALCUT.Text) + Val(ROW.Cells(GCUT.Index).EditedFormattedValue), "0.00")
                    LBLTOTALWT.Text = Format(Val(LBLTOTALWT.Text) + Val(ROW.Cells(GWT.Index).EditedFormattedValue), "0.000")
                End If
            Next
            LBLTOTALWTCUT.Text = Format(Val(LBLTOTALWT.Text) / Val(LBLTOTALCUT.Text.Trim), "0.000")
            LBLTOTALBEAMS.Text = GRIDSTOCK.RowCount
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()
        TXTOPBEAMSTOCKNO.Clear()
        CMBOURGODOWN.Text = GETDEFAULTGODOWN()
        CMBBEAMFILTER.Text = ""
        CMBSIZER.Text = ""
        CMBBEAM.Text = ""
        TXTBEAMNO.Clear()
        TXTENDS.Clear()
        TXTTAPLINE.Clear()
        TXTCUT.Clear()
        TXTWT.Clear()
        TXTWTCUT.Clear()
        TXTREMARKS.Clear()
    End Sub

    Private Sub OpeningStockBeam_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        fillGODOWN(CMBOURGODOWN, False, " AND GODOWN_ISOUR = 'True'")
        fillname(CMBSIZER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'")
        fillBEAM(CMBBEAM, edit)
        fillBEAM(CMBBEAMFILTER, edit)
    End Sub

    Private Sub CMBSIZER_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBSIZER.Enter
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

    Sub FILLGRID()
        Try

            Dim dttable As New DataTable
            Dim OBJOPSTOCK As New ClsOpeningStockBeam

            OBJOPSTOCK.alParaval.Add(0)
            OBJOPSTOCK.alParaval.Add(YearId)
            dttable = OBJOPSTOCK.GETSTOCKBEAM()

            If dttable.Rows.Count > 0 Then
                'ITEM GRID
                For Each ROW As DataRow In dttable.Rows
                    GRIDSTOCK.Rows.Add(Val(ROW("OPBEAMSTOCKNO")), ROW("SIZERNAME"), ROW("GODOWN"), ROW("BEAM"), ROW("BEAMNO"), Val(ROW("ENDS")), Val(ROW("TAPLINE")), Val(ROW("CUT")), Format(Val(ROW("WT")), "0.000"), Format(Val(ROW("WTCUT")), "0.000"), ROW("REMARKS"), ROW("DONE"))
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

    Sub calc()
        TXTWTCUT.Text = Format(Val(TXTWT.Text.Trim) / Val(TXTCUT.Text.Trim), "0.000")
    End Sub

    Private Sub TXTREMARKS_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTREMARKS.Validating

        If CMBSIZER.Text.Trim <> "" And CMBBEAM.Text.Trim <> "" And TXTBEAMNO.Text.Trim <> "" And Val(TXTTAPLINE.Text.Trim) > 0 And Val(TXTCUT.Text.Trim) > 0 And Val(TXTWT.Text.Trim) > 0 Then

            If USERADD = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(CMBSIZER.Text.Trim)
            ALPARAVAL.Add(CMBOURGODOWN.Text.Trim)
            ALPARAVAL.Add(CMBBEAM.Text.Trim)
            ALPARAVAL.Add(TXTBEAMNO.Text.Trim)
            ALPARAVAL.Add(Val(TXTENDS.Text.Trim))
            ALPARAVAL.Add(Val(TXTTAPLINE.Text.Trim))
            ALPARAVAL.Add(Val((TXTCUT.Text.Trim)))
            ALPARAVAL.Add(Format(Val(TXTWT.Text.Trim), "0.000"))
            ALPARAVAL.Add(Format(Val(TXTWTCUT.Text.Trim), "0.000"))
            ALPARAVAL.Add(TXTREMARKS.Text.Trim)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)


            Dim OBJOPENSTOCK As New ClsOpeningStockBeam
            OBJOPENSTOCK.alParaval = ALPARAVAL

            If edit = False Then
                Dim DT As DataTable = OBJOPENSTOCK.SAVE()
            Else

                ALPARAVAL.Add(Val(TXTOPBEAMSTOCKNO.Text))
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

    Private Sub OpeningStockBeam_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            If Convert.ToBoolean(GRIDSTOCK.Rows(e.RowIndex).Cells(GDONE.INDEX).VALUE) = True Then
                MsgBox("Unable to Modify, Entry Locked", MsgBoxStyle.Critical)
                Exit Sub
            End If

            TXTOPBEAMSTOCKNO.Text = GRIDSTOCK.Item(GOPBEAMSTOCKNO.Index, e.RowIndex).Value
            CMBSIZER.Text = GRIDSTOCK.Item(GSIZER.Index, e.RowIndex).Value
            CMBOURGODOWN.Text = GRIDSTOCK.Item(GGODOWN.Index, e.RowIndex).Value
            CMBBEAM.Text = GRIDSTOCK.Item(GBEAM.Index, e.RowIndex).Value
            TXTBEAMNO.Text = GRIDSTOCK.Item(GBEAMNO.Index, e.RowIndex).Value
            TXTENDS.Text = Val(GRIDSTOCK.Item(GENDS.Index, e.RowIndex).Value)
            TXTTAPLINE.Text = Val(GRIDSTOCK.Item(GTAPLINE.Index, e.RowIndex).Value)
            TXTCUT.Text = GRIDSTOCK.Item(GCUT.Index, e.RowIndex).Value
            TXTWT.Text = GRIDSTOCK.Item(GWT.Index, e.RowIndex).Value
            TXTWTCUT.Text = Val(GRIDSTOCK.Item(GWTCUT.Index, e.RowIndex).Value)
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

                    If Convert.ToBoolean(GRIDSTOCK.Rows(GRIDSTOCK.CurrentRow.Index).Cells(GDONE.Index).Value) = True Then
                        MsgBox("Unable to Modify, Entry Locked", MsgBoxStyle.Critical)
                        Exit Sub
                    End If

                    Dim TEMPMSG As Integer = MsgBox("Delete Details", MsgBoxStyle.YesNo)
                    If TEMPMSG = vbYes Then
                        Dim ALPARAVAL As New ArrayList
                        Dim OBJNO As New ClsOpeningStockBeam

                        OBJNO.alParaval = ALPARAVAL
                        ALPARAVAL.Add(GRIDSTOCK.Rows(GRIDSTOCK.CurrentRow.Index).Cells(GOPBEAMSTOCKNO.Index).Value)
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

    Private Sub TXTCUT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCUT.KeyPress, TXTTAPLINE.KeyPress
        numdotkeypress(e, TXTCUT, Me)
    End Sub

    Private Sub TXTWT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTWT.KeyPress
        numdot3(e, TXTWT, Me)
    End Sub

    Private Sub CMBBEAM_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBBEAM.Enter
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
                Dim DT As DataTable = OBJCMN.search("ISNULL(BEAM_ENDS, 0) AS ENDS, ISNULL(BEAM_TAPLINE, 0) AS TAPLINE", "", "BEAMMASTER", "AND BEAMMASTER.BEAM_NAME = '" & CMBBEAM.Text.Trim & "' AND BEAM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    If Val(TXTENDS.Text.Trim) = 0 Then TXTENDS.Text = DT.Rows(0).Item("ENDS")
                    If Val(TXTTAPLINE.Text.Trim) = 0 Then TXTTAPLINE.Text = DT.Rows(0).Item("TAPLINE")
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

    Private Sub CMBBEAMFILTER_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBBEAMFILTER.Enter
        Try
            If CMBBEAMFILTER.Text.Trim = "" Then fillBEAM(CMBBEAMFILTER, edit)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBEAMFILTER_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBBEAMFILTER.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJBEAMFILTER As New SelectBeam
                OBJBEAMFILTER.ShowDialog()
                If OBJBEAMFILTER.TEMPNAME <> "" Then CMBBEAMFILTER.Text = OBJBEAMFILTER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBEAMFILTER_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBBEAMFILTER.Validating
        Try
            If CMBBEAMFILTER.Text.Trim <> "" Then BEAMVALIDATE(CMBBEAMFILTER, e, Me)
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

    Private Sub CMBBEAMFILTER_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBBEAMFILTER.Validated
        Try
            GRIDSTOCK.RowCount = 0
            If CMBBEAMFILTER.Text.Trim <> "" Then
                Dim objclsCMST As New ClsCommonMaster
                Dim dt As DataTable = objclsCMST.search("ISNULL(STOCKMASTER_BEAM.SMBEAM_NO, 0) AS OPBEAMSTOCKNO, ISNULL(LEDGERS.Acc_cmpname, '') AS SIZERNAME, ISNULL(GODOWNMASTER.GODOWN_NAME,'') AS GODOWN, ISNULL(BEAMMASTER.BEAM_NAME, '') AS BEAM, ISNULL(STOCKMASTER_BEAM.SMBEAM_BEAMNO, '')AS BEAMNO, ISNULL(STOCKMASTER_BEAM.SMBEAM_ENDS, 0) AS ENDS, ISNULL(STOCKMASTER_BEAM.SMBEAM_TAPLINE, 0) AS TAPLINE, ISNULL(STOCKMASTER_BEAM.SMBEAM_CUT, 0) AS CUT, ISNULL(STOCKMASTER_BEAM.SMBEAM_WT, 0) AS WT, ISNULL(STOCKMASTER_BEAM.SMBEAM_WTCUT, 0) AS WTCUT, ISNULL(STOCKMASTER_BEAM.SMBEAM_REMARKS, '') AS REMARKS, STOCKMASTER_BEAM.SMBEAM_DONE AS DONE ", " ", " STOCKMASTER_BEAM INNER JOIN BEAMMASTER ON STOCKMASTER_BEAM.SMBEAM_BEAMID = BEAMMASTER.BEAM_ID AND STOCKMASTER_BEAM.SMBEAM_YEARID = BEAMMASTER.BEAM_YEARID LEFT OUTER JOIN GODOWNMASTER ON GODOWNMASTER.GODOWN_ID = STOCKMASTER_BEAM.SMBEAM_GODOWNID INNER JOIN LEDGERS ON STOCKMASTER_BEAM.SMBEAM_SIZERID = LEDGERS.Acc_id  ", " AND BEAMMASTER.BEAM_NAME = '" & CMBBEAMFILTER.Text.Trim & "' AND STOCKMASTER_BEAM.SMBEAM_YEARID = " & YearId & " ORDER BY STOCKMASTER_BEAM.SMBEAM_NO")
                If dt.Rows.Count > 0 Then
                    For Each ROW As DataRow In dt.Rows
                        GRIDSTOCK.Rows.Add(Val(ROW("OPBEAMSTOCKNO")), ROW("SIZERNAME"), ROW("GODOWN"), ROW("BEAM"), ROW("BEAMNO"), Val(ROW("ENDS")), Val(ROW("TAPLINE")), Val(ROW("CUT")), Format(Val(ROW("WT")), "0.000"), Format(Val(ROW("WTCUT")), "0.000"), ROW("REMARKS"), ROW("DONE"))
                        If Convert.ToBoolean(ROW("DONE")) = True Then GRIDSTOCK.Rows(GRIDSTOCK.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
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

    Private Sub CMBGODOWN_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBOURGODOWN.Enter
        Try
            If CMBOURGODOWN.Text.Trim = "" Then fillGODOWN(CMBOURGODOWN, edit, " AND GODOWN_ISOUR = 'True'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBOURGODOWN.Validating
        Try
            If CMBOURGODOWN.Text.Trim <> "" Then GODOWNVALIDATE(CMBOURGODOWN, e, Me, " AND GODOWN_ISOUR = 'True'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTENDS_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTENDS.KeyPress
        numkeypress(e, sender, Me)
    End Sub
End Class