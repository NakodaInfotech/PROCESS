Imports BL

Public Class OpeningStockYarnOnMachine
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public edit As Boolean
    Public tempMsg As Integer
    Public TEMPOPYARNSTOCKNO As Integer
    Public TEMPLRNO, TEMPGODRECNO As String

    Sub getsrno(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            For Each row As DataGridViewRow In grid.Rows
                row.Cells(0).Value = row.Index + 1
            Next
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub CLEAR()
        TXTOPYARNSTOCKNO.Clear()
        CMBMACHINE.Text = ""
        CMBMACHINEFILTER.Text = ""
        CMBQUALITY.Text = ""
        CMBMILLNAME.Text = ""
        CMBSHADE.Text = ""
        CMBGODOWN.Text = ""
        TXTGODRECNO.Clear()
        CMBTRANS.Text = ""
        TXTLOTNO.Clear()
        TXTWEIGHT.Clear()
        TXTBAGS.Clear()
        TXTREMARKS.Clear()

    End Sub

    Private Sub OpeningStockYarnwithSizer_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{TAB}")
            ElseIf e.KeyCode = Keys.OemQuotes Or e.KeyCode = Keys.OemPipe Then
                e.SuppressKeyPress = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        If CMBMACHINEFILTER.Text.Trim = "" Then FILLMACHINE(CMBMACHINEFILTER)
        If CMBMACHINE.Text.Trim = "" Then FILLMACHINE(CMBMACHINE)
        If CMBQUALITY.Text.Trim = "" Then fillQUALITY(CMBQUALITY, edit)
        If CMBMILLNAME.Text.Trim = "" Then fillname(CMBMILLNAME, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'ACCOUNTS' OR LEDGERS.ACC_SUBTYPE = 'MILL')")
        If CMBSHADE.Text.Trim = "" Then FILLCOLOR(CMBSHADE)
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub FILLGRID()

        Try
            Dim dttable As New DataTable

            Dim OBJOPSTOCK As New ClsOpeningStockYarnOnMachine
            OBJOPSTOCK.alParaval.Add(0)
            OBJOPSTOCK.alParaval.Add(YearId)
            dttable = OBJOPSTOCK.GETSTOCKYARN()

            If dttable.Rows.Count > 0 Then
                'ITEM GRID
                For Each ROW As DataRow In dttable.Rows
                    gridstock.Rows.Add(Val(ROW("NO")), ROW("MACHINE"), ROW("QUALITY"), ROW("MILL"), Val(ROW("LOTNO")), ROW("SHADE"), Val(ROW("BAGS")), Format(Val(ROW("WT")), "0.000"), Val(ROW("CONES")), ROW("REMARKS"), Val(ROW("RECDWT")))
                    If Val(ROW("RECDWT")) > 0 Then gridstock.Rows(gridstock.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                Next
            End If
            TOTAL()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTREMARKS_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTREMARKS.Validating
        If CMBMACHINE.Text.Trim <> "" And CMBQUALITY.Text.Trim <> "" And Val(TXTWEIGHT.Text.Trim) > 0 Then

            If USERADD = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If ClientName <> "JASHOK" Then
                If Val(TXTBAGS.Text.Trim) = 0 And Val(TXTWEIGHT.Text.Trim) = 0 And Val(TXTCONES.Text.Trim) = 0 Then
                    MsgBox("Enter Proper Details")
                    Exit Sub
                End If
            End If

            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(CMBMACHINE.Text.Trim)
            ALPARAVAL.Add(CMBQUALITY.Text.Trim)
            ALPARAVAL.Add(CMBMILLNAME.Text.Trim)
            ALPARAVAL.Add(TXTLOTNO.Text.Trim)
            ALPARAVAL.Add(CMBSHADE.Text.Trim)
            ALPARAVAL.Add(Val(TXTBAGS.Text.Trim))
            ALPARAVAL.Add(Format(Val(TXTWEIGHT.Text.Trim), "0.000"))
            ALPARAVAL.Add(Val(TXTCONES.Text.Trim))
            ALPARAVAL.Add(TXTREMARKS.Text.Trim)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)



            Dim OBJOPENSTOCK As New ClsOpeningStockYarnOnMachine
            OBJOPENSTOCK.alParaval = ALPARAVAL

            If edit = False Then
                Dim DT As DataTable = OBJOPENSTOCK.SAVE()
            Else
                ALPARAVAL.Add(Val(TXTOPYARNSTOCKNO.Text))
                    Dim INTRES As Integer = OBJOPENSTOCK.UPDATE()
                    GRIDDOUBLECLICK = False
                    edit = False
                End If

            gridstock.RowCount = 0
            FILLGRID()
            CLEAR()
            CMBMACHINE.Focus()
        Else
            MsgBox("Enter Proper Details")
        End If
    End Sub

    Private Sub OpeningStockYarnwithSizer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'OPENING'")
        USERADD = DTROW(0).Item(1)
        USEREDIT = DTROW(0).Item(2)
        USERVIEW = DTROW(0).Item(3)
        USERDELETE = DTROW(0).Item(4)

        LBLNAME.Text = "Select Machine"
        Me.Text = " Opening Stock Yarn On Machine"

        Dim OBJSEARCH As New ClsCommon
        Dim dttable As New DataTable

        If USEREDIT = False And USERVIEW = False Then
            MsgBox("Insufficient Rights")
            Exit Sub
        End If

        FILLCMB()
        FILLGRID()
    End Sub

    Private Sub gridstock_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridstock.CellDoubleClick
        Try
            If e.RowIndex < 0 Then Exit Sub

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            TXTOPYARNSTOCKNO.Text = gridstock.Item(GOPYARNSTOCKNO.Index, e.RowIndex).Value
            CMBMACHINE.Text = gridstock.Item(GMACHINE.Index, e.RowIndex).Value
            CMBQUALITY.Text = gridstock.Item(GQUALITY.Index, e.RowIndex).Value
            CMBMILLNAME.Text = gridstock.Item(GMILLNAME.Index, e.RowIndex).Value
            TXTLOTNO.Text = gridstock.Item(GLOTNO.Index, e.RowIndex).Value
            CMBSHADE.Text = gridstock.Item(GSHADE.Index, e.RowIndex).Value
            TXTBAGS.Text = Val(gridstock.Item(GBAGS.Index, e.RowIndex).Value)
            TXTWEIGHT.Text = Val(gridstock.Item(GWT.Index, e.RowIndex).Value)
            TXTCONES.Text = Val(gridstock.Item(GCONES.Index, e.RowIndex).Value)
            TXTREMARKS.Text = gridstock.Item(GREMARKS.Index, e.RowIndex).Value

            GRIDDOUBLECLICK = True
            edit = True
            TEMPROW = e.RowIndex
            CMBMACHINE.Focus()

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

                    If gridstock.Item(GRECDWT.Index, gridstock.CurrentRow.Index).Value > 0 Then
                        MsgBox("Entry Locked, it is used further", MsgBoxStyle.Critical)
                        Exit Sub
                    End If


                    Dim TEMPMSG As Integer = MsgBox("Delete Details", MsgBoxStyle.YesNo)
                    If TEMPMSG = vbYes Then
                        Dim ALPARAVAL As New ArrayList
                        Dim OBJNO As New ClsOpeningStockYarnOnMachine

                        OBJNO.alParaval = ALPARAVAL
                        ALPARAVAL.Add(gridstock.Rows(gridstock.CurrentRow.Index).Cells(GOPYARNSTOCKNO.Index).Value)
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

    Private Sub CMBMACHINEFILTER_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBMACHINEFILTER.Enter
        Try
            If CMBMACHINEFILTER.Text.Trim = "" Then FILLMACHINE(CMBMACHINEFILTER)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMACHINEFILTER_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBMACHINEFILTER.Validating
        Try
            If CMBMACHINEFILTER.Text.Trim <> "" Then MACHINEVALIDATE(CMBMACHINEFILTER, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMACHINE_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBMACHINE.Enter
        Try
            If CMBMACHINE.Text.Trim = "" Then FILLMACHINE(CMBMACHINE)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMACHINE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBMACHINE.Validating
        Try
            If CMBMACHINE.Text.Trim <> "" Then MACHINEVALIDATE(CMBMACHINE, e, Me)
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
                Dim OBJYARN As New SelectQuality
                OBJYARN.ShowDialog()
                If OBJYARN.TEMPNAME <> "" Then CMBQUALITY.Text = OBJYARN.TEMPNAME
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

    Private Sub CMBMILLNAME_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBMILLNAME.Enter
        Try
            If CMBMILLNAME.Text.Trim = "" Then fillname(CMBMILLNAME, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'MILL'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILLNAME_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBMILLNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'MILL'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then cmbcode.Text = OBJLEDGER.TEMPCODE
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSHADE_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBSHADE.Enter
        Try
            If CMBSHADE.Text.Trim = "" Then FILLCOLOR(CMBSHADE)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSHADE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBSHADE.Validating
        Try
            If CMBSHADE.Text.Trim <> "" Then COLORVALIDATE(CMBSHADE, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtbags_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTLOTNO.KeyPress
        numkeypress(e, TXTLOTNO, Me)
    End Sub

    Private Sub TXTWT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTWEIGHT.KeyPress
        numdot3(e, TXTWEIGHT, Me)
    End Sub

    Private Sub DTGODRECDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTGODRECDATE.GotFocus
        DTGODRECDATE.SelectAll()
    End Sub

    Private Sub DTLRDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTLRDATE.GotFocus
        DTLRDATE.SelectAll()
    End Sub

    Private Sub CMBMACHINEFILTER_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBMACHINEFILTER.Validated
        Try
            gridstock.RowCount = 0
            If CMBMACHINEFILTER.Text.Trim <> "" Then
                Dim objclsCMST As New ClsCommonMaster
                Dim dt As DataTable = objclsCMST.search("STOCKMASTER_YARNMACHINE.SMYARNMAC_NO AS NO, ISNULL(MACHINEMASTER.MACHINE_NAME, '') AS MACHINE, ISNULL(QUALITYMASTER.QUALITY_NAME, '') AS QUALITY, ISNULL(MILL.Acc_cmpname, '') AS MILL, ISNULL(STOCKMASTER_YARNMACHINE.SMYARNMAC_LOTNO,0) AS LOTNO,ISNULL(COLORMASTER.COLOR_NAME, '') AS SHADE, ISNULL(STOCKMASTER_YARNMACHINE.SMYARNMAC_BAGS, 0) AS BAGS, ISNULL(STOCKMASTER_YARNMACHINE.SMYARNMAC_WT, 0) AS WT, ISNULL(STOCKMASTER_YARNMACHINE.SMYARNMAC_CONES, 0) AS CONES, ISNULL(STOCKMASTER_YARNMACHINE.SMYARNMAC_REMARKS, '') AS REMARKS, ISNULL(STOCKMASTER_YARNMACHINE.SMYARNMAC_RECDWT, 0) AS RECDWT", " ", "STOCKMASTER_YARNMACHINE INNER JOIN QUALITYMASTER ON STOCKMASTER_YARNMACHINE.SMYARNMAC_QUALITYID = QUALITYMASTER.QUALITY_ID  INNER JOIN MACHINEMASTER ON STOCKMASTER_YARNMACHINE.SMYARNMAC_MACHINEID = MACHINEMASTER.MACHINE_ID LEFT OUTER JOIN LEDGERS AS MILL ON STOCKMASTER_YARNMACHINE.SMYARNMAC_MILLID = MILL.Acc_id LEFT OUTER JOIN COLORMASTER ON STOCKMASTER_YARNMACHINE.SMYARNMAC_SHADEID = COLORMASTER.COLOR_ID", " AND MACHINEMASTER.MACHINE_NAME = '" & CMBMACHINEFILTER.Text.Trim & "' AND STOCKMASTER_YARNMACHINE.SMYARNMAC_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    For Each ROW As DataRow In dt.Rows
                        gridstock.Rows.Add(Val(ROW("NO")), ROW("MACHINE"), ROW("QUALITY"), ROW("MILL"), ROW("LOTNO"), ROW("SHADE"), Val(ROW("BAGS")), Format(Val(ROW("WT")), "0.000"), Val(ROW("CONES")), ROW("REMARKS"), Val(ROW("RECDWT")))
                        If Val(ROW("RECDWT")) > 0 Then gridstock.Rows(gridstock.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                    Next
                End If
            Else
                FILLGRID()
            End If
            TOTAL()
        Catch ex As Exception
            Throw (ex)
        End Try
    End Sub

    Sub TOTAL()
        Try
            LBLTOTALBAG.Text = 0.0
            LBLTOTALWT.Text = 0.0
            LBLTOTALCONES.Text = 0
            For Each ROW As DataGridViewRow In gridstock.Rows
                If ROW.Cells(GOPYARNSTOCKNO.Index).Value <> Nothing Then
                    LBLTOTALBAG.Text = Format(Val(LBLTOTALBAG.Text) + Val(ROW.Cells(GBAGS.Index).EditedFormattedValue), "0")
                    LBLTOTALWT.Text = Format(Val(LBLTOTALWT.Text) + Val(ROW.Cells(GWT.Index).EditedFormattedValue), "0.000")
                    LBLTOTALCONES.Text = Format(Val(LBLTOTALCONES.Text) + Val(ROW.Cells(GCONES.Index).EditedFormattedValue), "0")
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class
