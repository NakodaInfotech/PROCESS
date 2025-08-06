
Imports System.ComponentModel
Imports BL

Public Class OpeningStockYarnWareHouse

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public edit As Boolean
    Public tempMsg As Integer
    Public TEMPOPYARNSTOCKNO As Integer
    Public TEMPLRNO, TEMPGODRECNO As String

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

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True

        'OLD DO CAN BE CREAETED IN NEW ACC YEAR
        'If DTGODRECDATE.Text = "__/__/____" Then
        '    EP.SetError(CMBQUALITYFILTER, " Please Enter Proper Godown Recd. Date")
        '    bln = False
        'Else
        '    If Not datecheck(DTGODRECDATE.Text) Then
        '        EP.SetError(CMBQUALITYFILTER, "Godown Recd. Date not in Accounting Year")
        '        bln = False
        '    End If
        'End If

        'If DTLRDATE.Text <> "__/__/____" Then
        '    If Not datecheck(DTLRDATE.Text) Then
        '        EP.SetError(CMBQUALITYFILTER, "LR Date not in Accounting Year")
        '        bln = False
        '    End If
        'End If


        Return bln
    End Function

    Sub CLEAR()
        TXTOPYARNSTOCKNO.Clear()
        CMBQUALITYFILTER.Text = ""
        CMBQUALITY.Text = ""
        CMBSUPPLIER.Text = ""
        CMBMILLNAME.Text = ""
        CMBGODOWN.Text = ""
        TXTGODRECNO.Clear()
        CMBTRANS.Text = ""
        txtbags.Clear()
        TXTWT.Clear()
        TXTLOTNO.Clear()
        CMBSHADE.Text = ""
        TXTCONES.Clear()
        TXTLRNO.Clear()
        TXTREMARKS.Clear()
    End Sub

    Private Sub OpeningStock_Yarn_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        fillQUALITY(CMBQUALITYFILTER, edit)
        fillQUALITY(CMBQUALITY, edit)
        fillname(CMBSUPPLIER, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'ACCOUNTS' OR LEDGERS.ACC_SUBTYPE = 'MILL')")
        fillname(CMBMILLNAME, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS' AND ACC_SUBTYPE = 'MILL'")
        fillGODOWN(CMBGODOWN, edit, "AND GODOWN_ISOUR='FALSE'")
        fillname(CMBTRANS, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'TRANSPORT'")
        FILLCOLOR(CMBSHADE)
    End Sub

    Sub FILLGRID()
        Try
            Dim dttable As New DataTable
            Dim OBJOPSTOCK As New ClsOpeningStockYarnWareHouse

            OBJOPSTOCK.alParaval.Add(0)
            OBJOPSTOCK.alParaval.Add(YearId)
            dttable = OBJOPSTOCK.GETSTOCKYARN()

            If dttable.Rows.Count > 0 Then
                'ITEM GRID
                For Each ROW As DataRow In dttable.Rows
                    gridstock.Rows.Add(Val(ROW("OPYARNSTOCKNO")), ROW("QUALITY"), ROW("SUPPLIER"), ROW("MILLNAME"), ROW("GODOWN"), ROW("GODRECNO"), ROW("GODRECDATE"), ROW("TRANSPORT"), ROW("BAGS"), ROW("WT"), ROW("LOTNO"), ROW("COLOR"), ROW("CONES"), ROW("LRNO"), Format(Convert.ToDateTime(ROW("LRDATE")).Date, "dd/MM/yyyy"), ROW("LIFTINGDATE"), ROW("REMARKS"), ROW("OUTBAGS"), ROW("OUTWT"), ROW("OUTCONES"))
                    If Val(ROW("OUTBAGS")) > 0 Or Val(ROW("OUTWT")) > 0 Then gridstock.Rows(gridstock.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
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

        If CMBQUALITY.Text.Trim <> "" And CMBSUPPLIER.Text.Trim <> "" And CMBMILLNAME.Text.Trim <> "" And CMBGODOWN.Text.Trim <> "" And TXTLRNO.Text.Trim <> "" And DTLRDATE.Text <> "__/__/____" And Val(txtbags.Text.Trim) > 0 And Val(TXTWT.Text.Trim) > 0 Then

            'If ClientName <> "JASHOK" Then
            '    If Val(TXTCONES.Text.Trim) = 0 Then
            '        MsgBox("Enter Proper Details")
            '        Exit Sub
            '    End If
            'End If


            If USERADD = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(CMBQUALITY.Text.Trim)
            ALPARAVAL.Add(CMBSUPPLIER.Text.Trim)
            ALPARAVAL.Add(CMBMILLNAME.Text.Trim)
            ALPARAVAL.Add(CMBGODOWN.Text.Trim)
            ALPARAVAL.Add(TXTGODRECNO.Text.Trim)
            ALPARAVAL.Add(DTGODRECDATE.Text.Trim)
            ALPARAVAL.Add(CMBTRANS.Text.Trim)
            ALPARAVAL.Add(Val(txtbags.Text.Trim))
            ALPARAVAL.Add(Format(Val(TXTWT.Text.Trim), "0.000"))
            ALPARAVAL.Add(TXTLOTNO.Text.Trim)
            ALPARAVAL.Add(CMBSHADE.Text.Trim)
            ALPARAVAL.Add(Val(TXTCONES.Text.Trim))
            ALPARAVAL.Add(TXTLRNO.Text.Trim)
            ALPARAVAL.Add(Format(Convert.ToDateTime(DTLRDATE.Text.Trim).Date, "MM/dd/yyyy"))
            ALPARAVAL.Add(DTLIFTINGDATE.Text.Trim)
            ALPARAVAL.Add(TXTREMARKS.Text.Trim)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)


            Dim OBJOPENSTOCK As New ClsOpeningStockYarnWareHouse
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
            CMBQUALITY.Focus()
        Else
            MsgBox("Enter Proper Details")
        End If
    End Sub

    Private Sub OpeningStockYarn_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

    Private Sub gridstock_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridstock.CellDoubleClick
        Try
            If e.RowIndex < 0 Then Exit Sub

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If UserName <> "Admin" Then
                If gridstock.Item(GOUTBAGS.Index, e.RowIndex).Value > 0 Or gridstock.Item(GOUTWT.Index, e.RowIndex).Value > 0 Then
                    MsgBox("Yarn Locked, it is used further", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End If

            TXTOPYARNSTOCKNO.Text = gridstock.Item(GOPYARNSTOCKNO.Index, e.RowIndex).Value
            CMBQUALITY.Text = gridstock.Item(GQUALITY.Index, e.RowIndex).Value
            CMBSUPPLIER.Text = gridstock.Item(GSUPPLIERNAME.Index, e.RowIndex).Value
            CMBMILLNAME.Text = gridstock.Item(GMILLNAME.Index, e.RowIndex).Value
            CMBGODOWN.Text = gridstock.Item(GGODOWN.Index, e.RowIndex).Value
            TXTGODRECNO.Text = gridstock.Item(GGODRECNO.Index, e.RowIndex).Value
            TEMPGODRECNO = gridstock.Item(GGODRECNO.Index, e.RowIndex).Value
            DTGODRECDATE.Text = gridstock.Item(GGODRECDATE.Index, e.RowIndex).Value
            CMBTRANS.Text = gridstock.Item(GTRANSPORT.Index, e.RowIndex).Value
            txtbags.Text = Val(gridstock.Item(GBAGS.Index, e.RowIndex).Value)
            TXTWT.Text = Val(gridstock.Item(GWT.Index, e.RowIndex).Value)
            TXTLOTNO.Text = gridstock.Item(GLOTNO.Index, e.RowIndex).Value
            CMBSHADE.Text = gridstock.Item(GCOLOR.Index, e.RowIndex).Value
            TXTCONES.Text = Val(gridstock.Item(GCONES.Index, e.RowIndex).Value)
            TXTLRNO.Text = gridstock.Item(GLRNO.Index, e.RowIndex).Value
            TEMPLRNO = gridstock.Item(GLRNO.Index, e.RowIndex).Value
            DTLRDATE.Text = gridstock.Item(GLRDATE.Index, e.RowIndex).Value
            DTLIFTINGDATE.Text = gridstock.Item(GLIFTINGDATE.Index, e.RowIndex).Value
            TXTREMARKS.Text = gridstock.Item(GREMARKS.Index, e.RowIndex).Value

            GRIDDOUBLECLICK = True
            edit = True
            TEMPROW = e.RowIndex
            CMBQUALITY.Focus()

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

                    If gridstock.Item(GOUTBAGS.Index, gridstock.CurrentRow.Index).Value > 0 Or gridstock.Item(GOUTWT.Index, gridstock.CurrentRow.Index).Value > 0 Then
                        MsgBox("Yarn Locked, it is used further", MsgBoxStyle.Critical)
                        Exit Sub
                    End If

                    Dim TEMPMSG As Integer = MsgBox("Delete Details", MsgBoxStyle.YesNo)
                    If TEMPMSG = vbYes Then
                        Dim ALPARAVAL As New ArrayList
                        Dim OBJNO As New ClsOpeningStockYarnWareHouse

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

    Private Sub CMBSUPPLIER_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBSUPPLIER.Enter
        Try
            If CMBSUPPLIER.Text.Trim = "" Then fillname(CMBSUPPLIER, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS' and (LEDGERS.ACC_SUBTYPE = 'ACCOUNTS' OR LEDGERS.ACC_SUBTYPE = 'MILL')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSUPPLIER_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBSUPPLIER.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' and (LEDGERS.ACC_SUBTYPE = 'ACCOUNTS' OR LEDGERS.ACC_SUBTYPE = 'MILL')"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then cmbcode.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then CMBSUPPLIER.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSUPPLIER_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBSUPPLIER.Validating
        Try
            If CMBSUPPLIER.Text.Trim <> "" Then namevalidate(CMBSUPPLIER, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' and (LEDGERS.ACC_SUBTYPE = 'ACCOUNTS' OR LEDGERS.ACC_SUBTYPE = 'MILL')", "SUNDRY CREDITORS", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILLNAME_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBMILLNAME.Enter
        Try
            If CMBMILLNAME.Text.Trim = "" Then fillname(CMBMILLNAME, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS' and LEDGERS.ACC_SUBTYPE = 'MILL'")
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
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' and LEDGERS.ACC_SUBTYPE = 'MILL'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then cmbcode.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then CMBMILLNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILLNAME_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBMILLNAME.Validating
        Try
            If CMBMILLNAME.Text.Trim <> "" Then namevalidate(CMBMILLNAME, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' and LEDGERS.ACC_SUBTYPE = 'MILL'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "MILL")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBGODOWN.Enter
        Try
            If CMBGODOWN.Text.Trim <> "" Then fillGODOWN(CMBGODOWN, edit, " AND GODOWN_ISOUR='FALSE'")
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
                OBJGODOWN.SEARCH = "AND GODOWN_ISOUR='FALSE'"
                OBJGODOWN.ShowDialog()
                If OBJGODOWN.TEMPNAME <> "" Then CMBGODOWN.Text = OBJGODOWN.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBGODOWN.Validating
        Try
            If CMBGODOWN.Text.Trim <> "" Then GODOWNVALIDATE(CMBGODOWN, e, Me, "AND GODOWN_ISOUR='FALSE'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBTRANS.Enter
        Try
            If CMBTRANS.Text.Trim = "" Then fillname(CMBTRANS, edit, "AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'AND LEDGERS.ACC_TYPE= 'TRANSPORT'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBTRANS.Validating
        Try
            If CMBTRANS.Text.Trim <> "" Then namevalidate(CMBTRANS, cmbcode, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'", "Sundry Creditors", "TRANSPORT")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBTRANS.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBTRANS.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSHADE_Enter(sender As Object, e As EventArgs) Handles CMBSHADE.Enter
        Try
            If CMBSHADE.Text.Trim = "" Then FILLCOLOR(CMBSHADE)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub txtbags_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtbags.KeyPress
        numkeypress(e, txtbags, Me)
    End Sub

    Private Sub TXTWT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTWT.KeyPress
        numdot3(e, TXTWT, Me)
    End Sub

    Private Sub DTGODRECDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTGODRECDATE.GotFocus
        DTGODRECDATE.SelectAll()
    End Sub

    Private Sub DTLRDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTLRDATE.GotFocus
        DTLRDATE.SelectAll()
    End Sub

    Private Sub CMBQUALITYFILTER_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBQUALITYFILTER.Validated
        Try
            gridstock.RowCount = 0
            If CMBQUALITYFILTER.Text.Trim <> "" Then
                Dim objclsCMST As New ClsCommonMaster
                Dim dt As New DataTable
                dt = objclsCMST.search("ISNULL(STOCKMASTER.SM_NO, 0) AS OPYARNSTOCKNO, ISNULL(QUALITYMASTER.QUALITY_NAME, '') AS QUALITY, ISNULL(SUPPLIER.Acc_cmpname, '') AS SUPPLIER, ISNULL(MILL.Acc_cmpname, '') AS MILLNAME, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN, ISNULL(STOCKMASTER.SM_GODRECNO, '') AS GODRECNO, ISNULL(STOCKMASTER.SM_GODRECDATE, '') AS GODRECDATE, ISNULL(TRANSPORT.Acc_cmpname, '') AS TRANSPORT, ISNULL(STOCKMASTER.SM_BAGS, 0) AS BAGS, ISNULL(STOCKMASTER.SM_WT, 0) AS WT, ISNULL(STOCKMASTER.SM_LRNO, '') AS LRNO, ISNULL(COLORMASTER.COLOR_NAME,'') AS COLOR, ISNULL(STOCKMASTER.SM_LRDATE, '') AS LRDATE, ISNULL(STOCKMASTER.SM_REMARKS, '') AS REMARKS, ISNULL(STOCKMASTER.SM_LOTNO, '') AS LOTNO, ISNULL(COLORMASTER.COLOR_NAME,'') AS COLOR, ISNULL(STOCKMASTER.SM_CONES, 0) AS CONES, STOCKMASTER.SM_LIFTINGDATE AS LIFTINGDATE, ISNULL(STOCKMASTER.SM_OUTBAGS, 0) AS OUTBAGS, ISNULL(STOCKMASTER.SM_OUTWT, 0) AS OUTWT, ISNULL(STOCKMASTER.SM_OUTCONES, 0) AS OUTCONES ", " ", " STOCKMASTER INNER JOIN QUALITYMASTER ON STOCKMASTER.SM_QUALITYID = QUALITYMASTER.QUALITY_ID INNER JOIN LEDGERS AS MILL ON STOCKMASTER.SM_MILLID = MILL.Acc_id INNER JOIN GODOWNMASTER ON STOCKMASTER.SM_GODOWNID = GODOWNMASTER.GODOWN_ID INNER JOIN LEDGERS AS SUPPLIER ON STOCKMASTER.SM_LEDGERID = SUPPLIER.Acc_id LEFT OUTER JOIN LEDGERS AS TRANSPORT ON STOCKMASTER.SM_TRANSID = TRANSPORT.Acc_id LEFT OUTER JOIN COLORMASTER ON COLOR_ID = STOCKMASTER.SM_SHADEID ", " AND QUALITYMASTER.QUALITY_NAME = '" & CMBQUALITYFILTER.Text.Trim & "' AND STOCKMASTER.SM_YEARID = " & YearId & " ORDER BY STOCKMASTER.SM_NO")
                If dt.Rows.Count > 0 Then
                    For Each ROW As DataRow In dt.Rows
                        gridstock.Rows.Add(Val(ROW("OPYARNSTOCKNO")), ROW("QUALITY"), ROW("SUPPLIER"), ROW("MILLNAME"), ROW("GODOWN"), ROW("GODRECNO"), ROW("GODRECDATE"), ROW("TRANSPORT"), ROW("BAGS"), ROW("WT"), ROW("LOTNO"), ROW("COLOR"), ROW("CONES"), ROW("LRNO"), ROW("LRDATE"), ROW("LIFTINGDATE"), ROW("REMARKS"), ROW("OUTBAGS"), ROW("OUTWT"), ROW("OUTCONES"))
                        If Val(ROW("OUTBAGS")) > 0 Or Val(ROW("OUTWT")) > 0 Then gridstock.Rows(gridstock.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
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

    Private Sub TXTLRNO_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTLRNO.Validated
        LRNODUPLICATION()
    End Sub

    Sub LRNODUPLICATION()
        Try
            If CMBTRANS.Text <> "" And edit = True And GRIDDOUBLECLICK = True And (TXTLRNO.Text.Trim <> TEMPLRNO) Then
                Dim OBJCMN As New ClsCommon
                Dim dttable As DataTable = OBJCMN.search("  ISNULL(STOCKMASTER.SM_LRNO, '') AS LRNO ", "", " STOCKMASTER INNER JOIN LEDGERS AS TRANSPORT ON STOCKMASTER.SM_TRANSPORTID = TRANSPORT.Acc_id AND STOCKMASTER.SM_YEARID = TRANSPORT.Acc_yearid ", " AND STOCKMASTER.SM_LRNO = '" & TXTLRNO.Text.Trim & "' AND TRANSPORT.ACC_CMPNAME = '" & CMBTRANS.Text.Trim & "' AND STOCKMASTER.SM_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    MsgBox("LR No Already Exists!")
                    TXTLRNO.Focus()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTGODRECNO_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTGODRECNO.Validated
        RECNODUPLICATION()
    End Sub

    Sub RECNODUPLICATION()
        Try
            If CMBGODOWN.Text <> "" And edit = True And GRIDDOUBLECLICK = True And (TXTGODRECNO.Text.Trim <> TEMPGODRECNO) Then
                Dim OBJCMN As New ClsCommon
                Dim dttable As DataTable = OBJCMN.search(" ISNULL(STOCKMASTER.SM_GODRECNO,'') AS GOERECNO ", "", "  STOCKMASTER INNER JOIN GODOWNMASTER ON STOCKMASTER.SM_GODOWNID = GODOWNMASTER.GODOWN_ID", " AND STOCKMASTER.SM_GODRECNO = '" & TXTGODRECNO.Text.Trim & "' AND GODOWNMASTER.GODOWN_NAME = '" & CMBGODOWN.Text.Trim & "' AND STOCKMASTER.SM_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    MsgBox("Godown Rec No Already Present !")
                    TXTGODRECNO.Focus()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
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
                Dim OBJYARN As New SelectQuality
                OBJYARN.ShowDialog()
                If OBJYARN.TEMPNAME <> "" Then CMBQUALITYFILTER.Text = OBJYARN.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITYFILTER_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBQUALITYFILTER.Validating
        Try
            If CMBQUALITYFILTER.Text.Trim <> "" Then QUALITYVALIDATE(CMBQUALITYFILTER, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCONES_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCONES.KeyPress
        numkeypress(e, TXTCONES, Me)
    End Sub

    Private Sub OpeningStockYarnWareHouse_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If ClientName = "JASHOK" Then
            TXTCONES.BackColor = Color.White
            TXTLRNO.BackColor = Color.White
            DTLRDATE.BackColor = Color.White
        End If
    End Sub

    Private Sub CMBSHADE_Validating(sender As Object, e As CancelEventArgs) Handles CMBSHADE.Validating
        Try
            If CMBSHADE.Text.Trim <> "" Then COLORVALIDATE(CMBSHADE, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
End Class