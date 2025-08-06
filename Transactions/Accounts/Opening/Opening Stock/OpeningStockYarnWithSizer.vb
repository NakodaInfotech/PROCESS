
Imports BL

Public Class OpeningStockYarnwithSizer

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public edit As Boolean
    Public tempMsg As Integer
    Public TEMPOPYARNSTOCKNO As Integer
    Public FRMSTRING As String
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
        CMBWEAVER.Text = ""
        CMBWEAVERFILTER.Text = ""
        CMBQUALITY.Text = ""
        CMBSUPPLIER.Text = ""
        CMBMILLNAME.Text = ""
        CMBGODOWN.Text = ""
        TXTGODRECNO.Clear()
        CMBTRANS.Text = ""
        txtbags.Clear()
        TXTWT.Clear()
        TXTFRESH.Clear()
        TXTWINDING.Clear()
        TXTFIRKA.Clear()
        TXTNALI.Clear()
        TXTLRNO.Clear()
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
        If FRMSTRING = "YARNWARPER" Then
            If CMBWEAVERFILTER.Text.Trim = "" Then fillname(CMBWEAVERFILTER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'WARPER' OR LEDGERS.ACC_SUBTYPE = 'SIZER')")
            If CMBWEAVER.Text.Trim = "" Then fillname(CMBWEAVER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'WARPER' OR LEDGERS.ACC_SUBTYPE = 'SIZER')")
        ElseIf FRMSTRING = "YARNWEAVER" Then
            If CMBWEAVERFILTER.Text.Trim = "" Then fillname(CMBWEAVERFILTER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'")
            If CMBWEAVER.Text.Trim = "" Then fillname(CMBWEAVER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'")
        ElseIf FRMSTRING = "YARNDYEING" Then
            If CMBWEAVERFILTER.Text.Trim = "" Then fillname(CMBWEAVERFILTER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'")
            If CMBWEAVER.Text.Trim = "" Then fillname(CMBWEAVER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'")
        ElseIf FRMSTRING = "YARNJOBBER" Then
            If CMBWEAVERFILTER.Text.Trim = "" Then fillname(CMBWEAVERFILTER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'JOBBER'")
            If CMBWEAVER.Text.Trim = "" Then fillname(CMBWEAVER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'JOBBER'")
        ElseIf FRMSTRING = "YARNSIZER" Then
            If CMBWEAVERFILTER.Text.Trim = "" Then fillname(CMBWEAVERFILTER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'")
            If CMBWEAVER.Text.Trim = "" Then fillname(CMBWEAVER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'")
        End If
        If CMBQUALITY.Text.Trim = "" Then fillQUALITY(CMBQUALITY, edit)
        If CMBSUPPLIER.Text.Trim = "" Then fillname(CMBSUPPLIER, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'ACCOUNTS' OR LEDGERS.ACC_SUBTYPE = 'MILL')")
        If CMBMILLNAME.Text.Trim = "" Then fillname(CMBMILLNAME, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'MILL'")
        If CMBGODOWN.Text.Trim = "" Then fillGODOWN(CMBGODOWN, edit, "")
        If CMBTRANS.Text.Trim = "" Then fillname(CMBTRANS, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'TRANSPORT'")
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub FILLGRID()

        Try
            Dim dttable As New DataTable
            If FRMSTRING = "YARNWARPER" Then
                Dim OBJOPSTOCK As New ClsOpeningStockYarnWithWarper

                OBJOPSTOCK.alParaval.Add(0)
                OBJOPSTOCK.alParaval.Add(YearId)
                dttable = OBJOPSTOCK.GETSTOCKYARN()

            ElseIf FRMSTRING = "YARNWEAVER" Then
                Dim OBJOPSTOCK As New ClsOpeningStockYarnwithWeaver

                OBJOPSTOCK.alParaval.Add(0)
                OBJOPSTOCK.alParaval.Add(YearId)
                dttable = OBJOPSTOCK.GETSTOCKYARN()

            ElseIf FRMSTRING = "YARNSIZER" Then
                Dim OBJOPSTOCK As New ClsOpeningStockYarnWithSizer

                OBJOPSTOCK.alParaval.Add(0)
                OBJOPSTOCK.alParaval.Add(YearId)
                dttable = OBJOPSTOCK.GETSTOCKYARN()

            ElseIf FRMSTRING = "YARNDYEING" Then
                Dim OBJOPSTOCK As New ClsOpeningStockYarnWithDyeing

                OBJOPSTOCK.alParaval.Add(0)
                OBJOPSTOCK.alParaval.Add(YearId)
                dttable = OBJOPSTOCK.GETSTOCKYARN()

            ElseIf FRMSTRING = "YARNJOBBER" Then
                Dim OBJOPSTOCK As New ClsOpeningStockYarnWithJobber

                OBJOPSTOCK.alParaval.Add(0)
                OBJOPSTOCK.alParaval.Add(YearId)
                dttable = OBJOPSTOCK.GETSTOCKYARN()

            End If
            If dttable.Rows.Count > 0 Then
                'ITEM GRID
                For Each ROW As DataRow In dttable.Rows
                    gridstock.Rows.Add(Val(ROW("NO")), ROW("NAME"), ROW("QUALITY"), ROW("SUPPLIER"), ROW("MILL"), ROW("GODOWN"), ROW("GODRECNO"), ROW("GODRECDATE"), ROW("TRANSPORT"), Val(ROW("BAGS")), Format(Val(ROW("WT")), "0.000"), Val(ROW("FRESH")), Val(ROW("WINDING")), Val(ROW("FIRKA")), Val(ROW("NALI")), ROW("LRNO"), ROW("LRDATE"), ROW("REMARKS"))
                Next
            End If
            TOTAL()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTREMARKS_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTREMARKS.Validating
        If CMBWEAVER.Text.Trim <> "" And CMBQUALITY.Text.Trim <> "" And Val(TXTWT.Text.Trim) > 0 Then

            If USERADD = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If ClientName <> "SHREEJI" Then
                If Val(TXTFRESH.Text.Trim) = 0 And Val(TXTWINDING.Text.Trim) = 0 And Val(TXTFIRKA.Text.Trim) = 0 And Val(TXTNALI.Text.Trim) = 0 Then
                    MsgBox("Enter Proper Details")
                    Exit Sub
                End If
            End If

            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(CMBWEAVER.Text.Trim)
            ALPARAVAL.Add(CMBQUALITY.Text.Trim)
            ALPARAVAL.Add(CMBSUPPLIER.Text.Trim)
            ALPARAVAL.Add(CMBMILLNAME.Text.Trim)
            ALPARAVAL.Add(CMBGODOWN.Text.Trim)
            ALPARAVAL.Add(TXTGODRECNO.Text.Trim)
            ALPARAVAL.Add(DTGODRECDATE.Text.Trim)
            ALPARAVAL.Add(CMBTRANS.Text.Trim)
            ALPARAVAL.Add(Val(txtbags.Text.Trim))
            ALPARAVAL.Add(Format(Val(TXTWT.Text.Trim), "0.000"))
            ALPARAVAL.Add(Val(TXTFRESH.Text.Trim))
            ALPARAVAL.Add(Val(TXTWINDING.Text.Trim))
            ALPARAVAL.Add(Val(TXTFIRKA.Text.Trim))
            ALPARAVAL.Add(Val(TXTNALI.Text.Trim))
            ALPARAVAL.Add(TXTLRNO.Text.Trim)
            ALPARAVAL.Add(DTLRDATE.Text.Trim)
            ALPARAVAL.Add(TXTREMARKS.Text.Trim)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            If FRMSTRING = "YARNWARPER" Then
                Dim OBJOPENSTOCK As New ClsOpeningStockYarnWithWarper
                OBJOPENSTOCK.alParaval = ALPARAVAL

                If edit = False Then
                    Dim DT As DataTable = OBJOPENSTOCK.SAVE()
                Else
                    ALPARAVAL.Add(Val(TXTOPYARNSTOCKNO.Text))
                    Dim INTRES As Integer = OBJOPENSTOCK.UPDATE()
                    GRIDDOUBLECLICK = False
                    edit = False
                End If

            ElseIf FRMSTRING = "YARNWEAVER" Then
                Dim OBJOPENSTOCK As New ClsOpeningStockYarnwithWeaver
                OBJOPENSTOCK.alParaval = ALPARAVAL

                If edit = False Then
                    Dim DT As DataTable = OBJOPENSTOCK.SAVE()
                Else
                    ALPARAVAL.Add(Val(TXTOPYARNSTOCKNO.Text))
                    Dim INTRES As Integer = OBJOPENSTOCK.UPDATE()
                    GRIDDOUBLECLICK = False
                    edit = False
                End If

            ElseIf FRMSTRING = "YARNSIZER" Then
                Dim OBJOPENSTOCK As New ClsOpeningStockYarnWithSizer
                OBJOPENSTOCK.alParaval = ALPARAVAL

                If edit = False Then
                    Dim DT As DataTable = OBJOPENSTOCK.SAVE()
                Else
                    ALPARAVAL.Add(Val(TXTOPYARNSTOCKNO.Text))
                    Dim INTRES As Integer = OBJOPENSTOCK.UPDATE()
                    GRIDDOUBLECLICK = False
                    edit = False
                End If

            ElseIf FRMSTRING = "YARNDYEING" Then
                Dim OBJOPENSTOCK As New ClsOpeningStockYarnWithDyeing
                OBJOPENSTOCK.alParaval = ALPARAVAL

                If edit = False Then
                    Dim DT As DataTable = OBJOPENSTOCK.SAVE()
                Else
                    ALPARAVAL.Add(Val(TXTOPYARNSTOCKNO.Text))
                    Dim INTRES As Integer = OBJOPENSTOCK.UPDATE()
                    GRIDDOUBLECLICK = False
                    edit = False
                End If

            ElseIf FRMSTRING = "YARNJOBBER" Then
                Dim OBJOPENSTOCK As New ClsOpeningStockYarnWithJobber
                OBJOPENSTOCK.alParaval = ALPARAVAL

                If edit = False Then
                    Dim DT As DataTable = OBJOPENSTOCK.SAVE()
                Else
                    ALPARAVAL.Add(Val(TXTOPYARNSTOCKNO.Text))
                    Dim INTRES As Integer = OBJOPENSTOCK.UPDATE()
                    GRIDDOUBLECLICK = False
                    edit = False
                End If
            End If
            gridstock.RowCount = 0
            FILLGRID()
            CLEAR()
            CMBWEAVER.Focus()
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

        If FRMSTRING = "YARNWARPER" Then
            LBLNAME.Text = "Select Warper"
            Me.Text = " Opening Stock yarn With Warper "
        ElseIf FRMSTRING = "YARNWEAVER" Then
            LBLNAME.Text = "Select Weaver"
            Me.Text = " Opening Stock yarn With Weaver "
        ElseIf FRMSTRING = "YARNSIZER" Then
            LBLNAME.Text = "Select Sizer"
            Me.Text = " Opening Stock yarn With Sizer "
        ElseIf FRMSTRING = "YARNDYEING" Then
            LBLNAME.Text = "Select Dyeing"
            Me.Text = " Opening Stock yarn With Dyeing"
        ElseIf FRMSTRING = "YARNJOBBER" Then
            LBLNAME.Text = "Select Jobber"
            Me.Text = " Opening Stock yarn With Jobber"
        End If

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
            CMBWEAVER.Text = gridstock.Item(GWEAVER.Index, e.RowIndex).Value
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
            TXTFRESH.Text = Val(gridstock.Item(GFRESH.Index, e.RowIndex).Value)
            TXTWINDING.Text = Val(gridstock.Item(GWINDING.Index, e.RowIndex).Value)
            TXTFIRKA.Text = Val(gridstock.Item(GFIRKA.Index, e.RowIndex).Value)
            TXTNALI.Text = Val(gridstock.Item(GNALI.Index, e.RowIndex).Value)
            TXTLRNO.Text = gridstock.Item(GLRNO.Index, e.RowIndex).Value
            TEMPLRNO = gridstock.Item(GLRNO.Index, e.RowIndex).Value
            DTLRDATE.Text = gridstock.Item(GLRDATE.Index, e.RowIndex).Value
            TXTREMARKS.Text = gridstock.Item(GREMARKS.Index, e.RowIndex).Value

            GRIDDOUBLECLICK = True
            edit = True
            TEMPROW = e.RowIndex
            CMBWEAVER.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridstock_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gridstock.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                If gridstock.SelectedCells.Count > 0 Then
                    Dim TEMP_NO As Integer
                    TEMP_NO = (gridstock.Rows(gridstock.CurrentRow.Index).Cells(GOPYARNSTOCKNO.Index).Value)

                    If USERDELETE = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If

                    If FRMSTRING = "YARNWARPER" Then
                        Dim ALPARAVAL As New ArrayList
                        Dim OBJNO As New ClsOpeningStockYarnWithWarper

                        OBJNO.alParaval = ALPARAVAL
                        ALPARAVAL.Add(TEMP_NO)
                        ALPARAVAL.Add(YearId)

                        Dim TEMPMSG As Integer = MsgBox("Delete Details", MsgBoxStyle.YesNo)

                        If TEMPMSG = vbYes Then
                            Dim INTRES As DataTable = OBJNO.DELETE()
                            gridstock.Rows.RemoveAt(gridstock.CurrentRow.Index)
                        End If

                    ElseIf FRMSTRING = "YARNWEAVER" Then
                        Dim ALPARAVAL As New ArrayList
                        Dim OBJNO As New ClsOpeningStockYarnwithWeaver

                        OBJNO.alParaval = ALPARAVAL
                        ALPARAVAL.Add(TEMP_NO)
                        ALPARAVAL.Add(YearId)

                        Dim TEMPMSG As Integer = MsgBox("Delete Details", MsgBoxStyle.YesNo)

                        If TEMPMSG = vbYes Then
                            Dim INTRES As DataTable = OBJNO.DELETE()
                            gridstock.Rows.RemoveAt(gridstock.CurrentRow.Index)
                        End If

                    ElseIf FRMSTRING = "YARNSIZER" Then
                        Dim ALPARAVAL As New ArrayList
                        Dim OBJNO As New ClsOpeningStockYarnWithSizer

                        OBJNO.alParaval = ALPARAVAL
                        ALPARAVAL.Add(TEMP_NO)
                        ALPARAVAL.Add(YearId)

                        Dim TEMPMSG As Integer = MsgBox("Delete Details", MsgBoxStyle.YesNo)

                        If TEMPMSG = vbYes Then
                            Dim INTRES As DataTable = OBJNO.DELETE()
                            gridstock.Rows.RemoveAt(gridstock.CurrentRow.Index)
                        End If

                    ElseIf FRMSTRING = "YARNDYEING" Then
                        Dim ALPARAVAL As New ArrayList
                        Dim OBJNO As New ClsOpeningStockYarnWithDyeing

                        OBJNO.alParaval = ALPARAVAL
                        ALPARAVAL.Add(TEMP_NO)
                        ALPARAVAL.Add(YearId)

                        Dim TEMPMSG As Integer = MsgBox("Delete Details", MsgBoxStyle.YesNo)

                        If TEMPMSG = vbYes Then
                            Dim INTRES As DataTable = OBJNO.DELETE()
                            gridstock.Rows.RemoveAt(gridstock.CurrentRow.Index)
                        End If

                    ElseIf FRMSTRING = "YARNJOBBER" Then
                        Dim ALPARAVAL As New ArrayList
                        Dim OBJNO As New ClsOpeningStockYarnWithJobber

                        OBJNO.alParaval = ALPARAVAL
                        ALPARAVAL.Add(TEMP_NO)
                        ALPARAVAL.Add(YearId)

                        Dim TEMPMSG As Integer = MsgBox("Delete Details", MsgBoxStyle.YesNo)

                        If TEMPMSG = vbYes Then
                            Dim INTRES As DataTable = OBJNO.DELETE()
                            gridstock.Rows.RemoveAt(gridstock.CurrentRow.Index)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWEAVERFILTER_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBWEAVERFILTER.Enter
        Try
            If FRMSTRING = "YARNWARPER" Then
                If CMBWEAVERFILTER.Text.Trim = "" Then fillname(CMBWEAVERFILTER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'WARPER' OR LEDGERS.ACC_SUBTYPE = 'SIZER')")
            ElseIf FRMSTRING = "YARNWEAVER" Then
                If CMBWEAVERFILTER.Text.Trim = "" Then fillname(CMBWEAVERFILTER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'")
            ElseIf FRMSTRING = "YARNSIZER" Then
                If CMBWEAVERFILTER.Text.Trim = "" Then fillname(CMBWEAVERFILTER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'")
            ElseIf FRMSTRING = "YARNDYEING" Then
                If CMBWEAVERFILTER.Text.Trim = "" Then fillname(CMBWEAVERFILTER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'")
            ElseIf FRMSTRING = "YARNJOBBER" Then
                If CMBWEAVERFILTER.Text.Trim = "" Then fillname(CMBWEAVERFILTER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'JOBBER'")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWEAVERFILTER_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBWEAVERFILTER.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                If FRMSTRING = "YARNWARPER" Then
                    OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'WARPER' OR LEDGERS.ACC_SUBTYPE = 'SIZER')"
                ElseIf FRMSTRING = "YARNWEAVER" Then
                    OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'"
                ElseIf FRMSTRING = "YARNSIZER" Then
                    OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'"
                ElseIf FRMSTRING = "YARNDYEING" Then
                    OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'"
                ElseIf FRMSTRING = "YARNJOBBER" Then
                    OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'JOBBER'"
                End If
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBWEAVERFILTER.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWEAVERFILTER_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBWEAVERFILTER.Validating
        Try
            If FRMSTRING = "YARNWARPER" Then
                If CMBWEAVERFILTER.Text.Trim <> "" Then namevalidate(CMBWEAVERFILTER, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'WARPER' OR LEDGERS.ACC_SUBTYPE = 'SIZER')", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "WARPER")
            ElseIf FRMSTRING = "YARNWEAVER" Then
                If CMBWEAVERFILTER.Text.Trim <> "" Then namevalidate(CMBWEAVERFILTER, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "WEAVER")
            ElseIf FRMSTRING = "YARNSIZER" Then
                If CMBWEAVERFILTER.Text.Trim <> "" Then namevalidate(CMBWEAVERFILTER, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "SIZER")
            ElseIf FRMSTRING = "YARNDYEING" Then
                If CMBWEAVERFILTER.Text.Trim <> "" Then namevalidate(CMBWEAVERFILTER, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "PROCESSOR")
            ElseIf FRMSTRING = "YARNJOBBER" Then
                If CMBWEAVERFILTER.Text.Trim <> "" Then namevalidate(CMBWEAVERFILTER, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'JOBBER'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "JOBBER")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWEAVER_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBWEAVER.Enter
        Try
            If FRMSTRING = "YARNWARPER" Then
                If CMBWEAVER.Text.Trim = "" Then fillname(CMBWEAVER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'WARPER' OR LEDGERS.ACC_SUBTYPE = 'SIZER')")
            ElseIf FRMSTRING = "YARNWEAVER" Then
                If CMBWEAVER.Text.Trim = "" Then fillname(CMBWEAVER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'")
            ElseIf FRMSTRING = "YARNSIZER" Then
                If CMBWEAVER.Text.Trim = "" Then fillname(CMBWEAVER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'")
            ElseIf FRMSTRING = "YARNDYEING" Then
                If CMBWEAVER.Text.Trim = "" Then fillname(CMBWEAVER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'")
            ElseIf FRMSTRING = "YARNJOBBER" Then
                If CMBWEAVER.Text.Trim = "" Then fillname(CMBWEAVER, edit, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'JOBBER'")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWEAVER_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBWEAVER.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                If FRMSTRING = "YARNWARPER" Then
                    OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'WARPER' OR LEDGERS.ACC_SUBTYPE = 'SIZER')"
                ElseIf FRMSTRING = "YARNWEAVER" Then
                    OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'"
                ElseIf FRMSTRING = "YARNSIZER" Then
                    OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'"
                ElseIf FRMSTRING = "YARNDYEING" Then
                    OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'"
                ElseIf FRMSTRING = "YARNJOBBER" Then
                    OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'JOBBER'"
                End If
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBWEAVER.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWEAVER_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBWEAVER.Validating
        Try
            If FRMSTRING = "YARNWARPER" Then
                If CMBWEAVER.Text.Trim <> "" Then namevalidate(CMBWEAVER, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'WARPER' OR LEDGERS.ACC_SUBTYPE = 'SIZER')", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "WARPER")
            ElseIf FRMSTRING = "YARNWEAVER" Then
                If CMBWEAVER.Text.Trim <> "" Then namevalidate(CMBWEAVER, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'WEAVER'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "WEAVER")
            ElseIf FRMSTRING = "YARNSIZER" Then
                If CMBWEAVER.Text.Trim <> "" Then namevalidate(CMBWEAVER, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'SIZER'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "SIZER")
            ElseIf FRMSTRING = "YARNDYEING" Then
                If CMBWEAVER.Text.Trim <> "" Then namevalidate(CMBWEAVER, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'PROCESSOR'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "PROCESSOR")
            ElseIf FRMSTRING = "YARNJOBBER" Then
                If CMBWEAVER.Text.Trim <> "" Then namevalidate(CMBWEAVER, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'JOBBER'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "PROCESSOR")
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
            If CMBSUPPLIER.Text.Trim = "" Then fillname(CMBSUPPLIER, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'ACCOUNTS' OR LEDGERS.ACC_SUBTYPE = 'MILL')")
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
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'ACCOUNTS' OR LEDGERS.ACC_SUBTYPE = 'MILL')"
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
            If CMBSUPPLIER.Text.Trim <> "" Then namevalidate(CMBSUPPLIER, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND (LEDGERS.ACC_SUBTYPE = 'ACCOUNTS' OR LEDGERS.ACC_SUBTYPE = 'MILL')", "SUNDRY CREDITORS", "ACCOUNTS")
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
                If OBJLEDGER.TEMPNAME <> "" Then CMBMILLNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILLNAME_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBMILLNAME.Validating
        Try
            If CMBMILLNAME.Text.Trim <> "" Then namevalidate(CMBMILLNAME, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_SUBTYPE = 'MILL'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", "MILL")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBGODOWN.Enter
        Try
            If CMBGODOWN.Text.Trim <> "" Then fillGODOWN(CMBGODOWN, edit, "")
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
                OBJGODOWN.SEARCH = " "
                OBJGODOWN.ShowDialog()
                If OBJGODOWN.TEMPNAME <> "" Then CMBGODOWN.Text = OBJGODOWN.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBGODOWN.Validating
        Try
            If CMBGODOWN.Text.Trim <> "" Then GODOWNVALIDATE(CMBGODOWN, e, Me, "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBTRANS.Enter
        Try
            If cmbtrans.Text.Trim = "" Then fillname(cmbtrans, EDIT, "AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'AND LEDGERS.ACC_TYPE= 'TRANSPORT'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBTRANS.Validating
        Try
            If cmbtrans.Text.Trim <> "" Then namevalidate(cmbtrans, cmbcode, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'", "Sundry Creditors", "TRANSPORT")
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
                If OBJLEDGER.TEMPNAME <> "" Then cmbtrans.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
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

    Private Sub CMBWEAVERFILTER_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBWEAVERFILTER.Validated
        Try
            gridstock.RowCount = 0
            If CMBWEAVERFILTER.Text.Trim <> "" Then
                Dim objclsCMST As New ClsCommonMaster
                Dim dt As New DataTable
                If FRMSTRING = "YARNWARPER" Then
                    dt = objclsCMST.search("  STOCKMASTER_YARNWARPER.SMYARNWARPER_NO AS NO, ISNULL(WARPER.Acc_CMPNAME, '') AS SIZER, ISNULL(QUALITYMASTER.QUALITY_NAME, '')  AS QUALITY, ISNULL(SUPPLIER.Acc_cmpname, '') AS SUPPLIER, ISNULL(MILL.Acc_cmpname, '') AS MILL, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN, ISNULL(STOCKMASTER_YARNWARPER.SMYARNWARPER_GODRECNO, '') AS GODRECNO, STOCKMASTER_YARNWARPER.SMYARNWARPER_GODRECDATE AS GODRECDATE, ISNULL(TRANSPORT.Acc_cmpname, '') AS TRANSPORT, ISNULL(STOCKMASTER_YARNWARPER.SMYARNWARPER_BAGS, 0) AS BAGS, ISNULL(STOCKMASTER_YARNWARPER.SMYARNWARPER_WT, 0) AS WT, ISNULL(STOCKMASTER_YARNWARPER.SMYARNWARPER_FRESH, 0) AS FRESH, ISNULL(STOCKMASTER_YARNWARPER.SMYARNWARPER_WINDING, 0) AS WINDING, ISNULL(STOCKMASTER_YARNWARPER.SMYARNWARPER_FIRKA, 0) AS FIRKA, ISNULL(STOCKMASTER_YARNWARPER.SMYARNWARPER_NALI, 0) AS NALI, ISNULL(STOCKMASTER_YARNWARPER.SMYARNWARPER_LRNO, '') AS LRNO, STOCKMASTER_YARNWARPER.SMYARNWARPER_LRDATE AS LRDATE, ISNULL(STOCKMASTER_YARNWARPER.SMYARNWARPER_REMARKS, '') AS REMARKS ", " ", "   STOCKMASTER_YARNWARPER INNER JOIN QUALITYMASTER ON STOCKMASTER_YARNWARPER.SMYARNWARPER_QUALITYID = QUALITYMASTER.QUALITY_ID LEFT OUTER JOIN LEDGERS AS TRANSPORT ON STOCKMASTER_YARNWARPER.SMYARNWARPER_TRANSPORTID = TRANSPORT.Acc_id LEFT OUTER JOIN LEDGERS AS SUPPLIER ON STOCKMASTER_YARNWARPER.SMYARNWARPER_SUPPLIERID = SUPPLIER.Acc_id LEFT OUTER JOIN LEDGERS AS MILL ON STOCKMASTER_YARNWARPER.SMYARNWARPER_MILLID = MILL.Acc_id INNER JOIN LEDGERS AS WARPER ON STOCKMASTER_YARNWARPER.SMYARNWARPER_WARPERID = WARPER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON STOCKMASTER_YARNWARPER.SMYARNWARPER_GODOWNID = GODOWNMASTER.GODOWN_ID", " AND WARPER.ACC_CMPNAME = '" & CMBWEAVERFILTER.Text.Trim & "' AND STOCKMASTER_YARNWARPER.SMYARNWARPER_YEARID = " & YearId & " ORDER BY STOCKMASTER_YARNWARPER.SMYARNWARPER_NO")
                ElseIf FRMSTRING = "YARNSIZER" Then
                    dt = objclsCMST.search("  STOCKMASTER_YARNSIZER.SMYARNSIZER_NO AS NO, ISNULL(SIZER.Acc_CMPNAME, '') AS SIZER, ISNULL(QUALITYMASTER.QUALITY_NAME, '')  AS QUALITY, ISNULL(SUPPLIER.Acc_cmpname, '') AS SUPPLIER, ISNULL(MILL.Acc_cmpname, '') AS MILL, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN, ISNULL(STOCKMASTER_YARNSIZER.SMYARNSIZER_GODRECNO, '') AS GODRECNO, STOCKMASTER_YARNSIZER.SMYARNSIZER_GODRECDATE AS GODRECDATE, ISNULL(TRANSPORT.Acc_cmpname, '') AS TRANSPORT, ISNULL(STOCKMASTER_YARNSIZER.SMYARNSIZER_BAGS, 0) AS BAGS, ISNULL(STOCKMASTER_YARNSIZER.SMYARNSIZER_WT, 0) AS WT, ISNULL(STOCKMASTER_YARNSIZER.SMYARNSIZER_FRESH, 0) AS FRESH, ISNULL(STOCKMASTER_YARNSIZER.SMYARNSIZER_WINDING, 0) AS WINDING, ISNULL(STOCKMASTER_YARNSIZER.SMYARNSIZER_FIRKA, 0) AS FIRKA, ISNULL(STOCKMASTER_YARNSIZER.SMYARNSIZER_NALI, 0) AS NALI, ISNULL(STOCKMASTER_YARNSIZER.SMYARNSIZER_LRNO, '') AS LRNO, STOCKMASTER_YARNSIZER.SMYARNSIZER_LRDATE AS LRDATE, ISNULL(STOCKMASTER_YARNSIZER.SMYARNSIZER_REMARKS, '') AS REMARKS ", " ", "   STOCKMASTER_YARNSIZER INNER JOIN QUALITYMASTER ON STOCKMASTER_YARNSIZER.SMYARNSIZER_QUALITYID = QUALITYMASTER.QUALITY_ID LEFT OUTER JOIN LEDGERS AS TRANSPORT ON STOCKMASTER_YARNSIZER.SMYARNSIZER_TRANSPORTID = TRANSPORT.Acc_id LEFT OUTER JOIN LEDGERS AS SUPPLIER ON STOCKMASTER_YARNSIZER.SMYARNSIZER_SUPPLIERID = SUPPLIER.Acc_id LEFT OUTER JOIN LEDGERS AS MILL ON STOCKMASTER_YARNSIZER.SMYARNSIZER_MILLID = MILL.Acc_id INNER JOIN LEDGERS AS SIZER ON STOCKMASTER_YARNSIZER.SMYARNSIZER_SIZERID = SIZER.Acc_id LEFT OUTER JOIN GODOWNMASTER ON STOCKMASTER_YARNSIZER.SMYARNSIZER_GODOWNID = GODOWNMASTER.GODOWN_ID", " AND SIZER.ACC_CMPNAME = '" & CMBWEAVERFILTER.Text.Trim & "' AND STOCKMASTER_YARNSIZER.SMYARNSIZER_YEARID = " & YearId & " ORDER BY STOCKMASTER_YARNSIZER.SMYARNSIZER_NO")
                ElseIf FRMSTRING = "YARNWEAVER" Then
                    dt = objclsCMST.search("  STOCKMASTER_YARNWEAVER.SMYARNWEAVER_NO AS NO, ISNULL(WEAVER.Acc_cmpname, '') AS SIZER, ISNULL(QUALITYMASTER.QUALITY_NAME, '') AS QUALITY, ISNULL(SUPPLIER.Acc_cmpname, '') AS SUPPLIER, ISNULL(MILL.Acc_cmpname, '') AS MILL, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN, ISNULL(STOCKMASTER_YARNWEAVER.SMYARNWEAVER_GODRECNO, '') AS GODRECNO, STOCKMASTER_YARNWEAVER.SMYARNWEAVER_GODRECDATE AS GODRECDATE, ISNULL(TRANSPORT.Acc_cmpname, '') AS TRANSPORT, ISNULL(STOCKMASTER_YARNWEAVER.SMYARNWEAVER_BAGS, 0) AS BAGS, ISNULL(STOCKMASTER_YARNWEAVER.SMYARNWEAVER_WT, 0) AS WT, ISNULL(STOCKMASTER_YARNWEAVER.SMYARNWEAVER_FRESH, 0) AS FRESH, ISNULL(STOCKMASTER_YARNWEAVER.SMYARNWEAVER_WINDING, 0) AS WINDING, ISNULL(STOCKMASTER_YARNWEAVER.SMYARNWEAVER_FIRKA, 0) AS FIRKA, ISNULL(STOCKMASTER_YARNWEAVER.SMYARNWEAVER_NALI, 0) AS NALI, ISNULL(STOCKMASTER_YARNWEAVER.SMYARNWEAVER_LRNO, '') AS LRNO, STOCKMASTER_YARNWEAVER.SMYARNWEAVER_LRDATE AS LRDATE, ISNULL(STOCKMASTER_YARNWEAVER.SMYARNWEAVER_REMARKS, '') AS REMARKS", " ", "   STOCKMASTER_YARNWEAVER INNER JOIN QUALITYMASTER ON STOCKMASTER_YARNWEAVER.SMYARNWEAVER_QUALITYID = QUALITYMASTER.QUALITY_ID LEFT OUTER JOIN GODOWNMASTER ON STOCKMASTER_YARNWEAVER.SMYARNWEAVER_GODOWNID = GODOWNMASTER.GODOWN_ID INNER JOIN LEDGERS AS WEAVER ON STOCKMASTER_YARNWEAVER.SMYARNWEAVER_WEAVERID = WEAVER.Acc_id LEFT OUTER JOIN LEDGERS AS MILL ON STOCKMASTER_YARNWEAVER.SMYARNWEAVER_MILLID = MILL.Acc_id LEFT OUTER JOIN LEDGERS AS TRANSPORT ON STOCKMASTER_YARNWEAVER.SMYARNWEAVER_TRANSPORTID = TRANSPORT.Acc_id LEFT OUTER JOIN LEDGERS AS SUPPLIER ON STOCKMASTER_YARNWEAVER.SMYARNWEAVER_SUPPLIERID = SUPPLIER.Acc_id", " AND WEAVER.ACC_CMPNAME = '" & CMBWEAVERFILTER.Text.Trim & "' AND STOCKMASTER_YARNWEAVER.SMYARNWEAVER_YEARID = " & YearId)
                ElseIf FRMSTRING = "YARNDYEING" Then
                    dt = objclsCMST.search("  STOCKMASTER_YARNDYEING.SMYARNDYEING_NO AS NO, ISNULL(DYEING.Acc_cmpname, '') AS SIZER, ISNULL(QUALITYMASTER.QUALITY_NAME, '') AS QUALITY, ISNULL(SUPPLIER.Acc_cmpname, '') AS SUPPLIER, ISNULL(MILL.Acc_cmpname, '') AS MILL, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN, ISNULL(STOCKMASTER_YARNDYEING.SMYARNDYEING_GODRECNO, '') AS GODRECNO, STOCKMASTER_YARNDYEING.SMYARNDYEING_GODRECDATE AS GODRECDATE, ISNULL(TRANSPORT.Acc_cmpname, '') AS TRANSPORT, ISNULL(STOCKMASTER_YARNDYEING.SMYARNDYEING_BAGS, 0) AS BAGS, ISNULL(STOCKMASTER_YARNDYEING.SMYARNDYEING_WT, 0) AS WT, ISNULL(STOCKMASTER_YARNDYEING.SMYARNDYEING_FRESH, 0) AS FRESH, ISNULL(STOCKMASTER_YARNDYEING.SMYARNDYEING_WINDING, 0) AS WINDING, ISNULL(STOCKMASTER_YARNDYEING.SMYARNDYEING_FIRKA, 0) AS FIRKA, ISNULL(STOCKMASTER_YARNDYEING.SMYARNDYEING_NALI, 0) AS NALI, ISNULL(STOCKMASTER_YARNDYEING.SMYARNDYEING_LRNO, '') AS LRNO, STOCKMASTER_YARNDYEING.SMYARNDYEING_LRDATE AS LRDATE, ISNULL(STOCKMASTER_YARNDYEING.SMYARNDYEING_REMARKS, '') AS REMARKS", " ", "   STOCKMASTER_YARNDYEING INNER JOIN QUALITYMASTER ON STOCKMASTER_YARNDYEING.SMYARNDYEING_QUALITYID = QUALITYMASTER.QUALITY_ID LEFT OUTER JOIN GODOWNMASTER ON STOCKMASTER_YARNDYEING.SMYARNDYEING_GODOWNID = GODOWNMASTER.GODOWN_ID INNER JOIN LEDGERS AS DYEING ON STOCKMASTER_YARNDYEING.SMYARNDYEING_DYEINGID = DYEING.Acc_id LEFT OUTER JOIN LEDGERS AS MILL ON STOCKMASTER_YARNDYEING.SMYARNDYEING_MILLID = MILL.Acc_id LEFT OUTER JOIN LEDGERS AS TRANSPORT ON STOCKMASTER_YARNDYEING.SMYARNDYEING_TRANSPORTID = TRANSPORT.Acc_id LEFT OUTER JOIN LEDGERS AS SUPPLIER ON STOCKMASTER_YARNDYEING.SMYARNDYEING_SUPPLIERID = SUPPLIER.Acc_id", " AND DYEING.ACC_CMPNAME = '" & CMBWEAVERFILTER.Text.Trim & "' AND STOCKMASTER_YARNDYEING.SMYARNDYEING_YEARID = " & YearId)
                ElseIf FRMSTRING = "YARNJOBBER" Then
                    dt = objclsCMST.search("  STOCKMASTER_YARNJOBBER.SMYARNJOBBER_NO AS NO, ISNULL(JOBBER.Acc_cmpname, '') AS SIZER, ISNULL(QUALITYMASTER.QUALITY_NAME, '') AS QUALITY, ISNULL(SUPPLIER.Acc_cmpname, '') AS SUPPLIER, ISNULL(MILL.Acc_cmpname, '') AS MILL, ISNULL(GODOWNMASTER.GODOWN_NAME, '') AS GODOWN, ISNULL(STOCKMASTER_YARNJOBBER.SMYARNJOBBER_GODRECNO, '') AS GODRECNO, STOCKMASTER_YARNJOBBER.SMYARNJOBBER_GODRECDATE AS GODRECDATE, ISNULL(TRANSPORT.Acc_cmpname, '') AS TRANSPORT, ISNULL(STOCKMASTER_YARNJOBBER.SMYARNJOBBER_BAGS, 0) AS BAGS, ISNULL(STOCKMASTER_YARNJOBBER.SMYARNJOBBER_WT, 0) AS WT, ISNULL(STOCKMASTER_YARNJOBBER.SMYARNJOBBER_FRESH, 0) AS FRESH, ISNULL(STOCKMASTER_YARNJOBBER.SMYARNJOBBER_WINDING, 0) AS WINDING, ISNULL(STOCKMASTER_YARNJOBBER.SMYARNJOBBER_FIRKA, 0) AS FIRKA, ISNULL(STOCKMASTER_YARNJOBBER.SMYARNJOBBER_NALI, 0) AS NALI, ISNULL(STOCKMASTER_YARNJOBBER.SMYARNJOBBER_LRNO, '') AS LRNO, STOCKMASTER_YARNJOBBER.SMYARNJOBBER_LRDATE AS LRDATE, ISNULL(STOCKMASTER_YARNJOBBER.SMYARNJOBBER_REMARKS, '') AS REMARKS", " ", "   STOCKMASTER_YARNJOBBER INNER JOIN QUALITYMASTER ON STOCKMASTER_YARNJOBBER.SMYARNJOBBER_QUALITYID = QUALITYMASTER.QUALITY_ID LEFT OUTER JOIN GODOWNMASTER ON STOCKMASTER_YARNJOBBER.SMYARNJOBBER_GODOWNID = GODOWNMASTER.GODOWN_ID INNER JOIN LEDGERS AS JOBBER ON STOCKMASTER_YARNJOBBER.SMYARNJOBBER_JOBBERID = JOBBER.Acc_id LEFT OUTER JOIN LEDGERS AS MILL ON STOCKMASTER_YARNJOBBER.SMYARNJOBBER_MILLID = MILL.Acc_id LEFT OUTER JOIN LEDGERS AS TRANSPORT ON STOCKMASTER_YARNJOBBER.SMYARNJOBBER_TRANSPORTID = TRANSPORT.Acc_id LEFT OUTER JOIN LEDGERS AS SUPPLIER ON STOCKMASTER_YARNJOBBER.SMYARNJOBBER_SUPPLIERID = SUPPLIER.Acc_id", " AND JOBBER.ACC_CMPNAME = '" & CMBWEAVERFILTER.Text.Trim & "' AND STOCKMASTER_YARNJOBBER.SMYARNJOBBER_YEARID = " & YearId)
                End If
                'gridstock.DataSource = dt
                If dt.Rows.Count > 0 Then
                    For Each ROW As DataRow In dt.Rows
                        gridstock.Rows.Add(Val(ROW("NO")), ROW("SIZER"), ROW("QUALITY"), ROW("SUPPLIER"), ROW("MILL"), ROW("GODOWN"), ROW("GODRECNO"), ROW("GODRECDATE"), ROW("TRANSPORT"), Val(ROW("BAGS")), Format(Val(ROW("WT")), "0.000"), Val(ROW("FRESH")), Val(ROW("WINDING")), Val(ROW("FIRKA")), Val(ROW("NALI")), ROW("LRNO"), ROW("LRDATE"), ROW("REMARKS"))
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
            LBLTOTALFRESH.Text = 0
            LBLTOTALWINDING.Text = 0
            LBLTOTALFIRKA.Text = 0
            LBLTOTALNALI.Text = 0

            For Each ROW As DataGridViewRow In gridstock.Rows
                If ROW.Cells(GOPYARNSTOCKNO.Index).Value <> Nothing Then
                    LBLTOTALBAG.Text = Format(Val(LBLTOTALBAG.Text) + Val(ROW.Cells(GBAGS.Index).EditedFormattedValue), "0")
                    LBLTOTALWT.Text = Format(Val(LBLTOTALWT.Text) + Val(ROW.Cells(GWT.Index).EditedFormattedValue), "0.000")
                    LBLTOTALFRESH.Text = Format(Val(LBLTOTALFRESH.Text) + Val(ROW.Cells(GFRESH.Index).EditedFormattedValue), "0")
                    LBLTOTALWINDING.Text = Format(Val(LBLTOTALWINDING.Text) + Val(ROW.Cells(GWINDING.Index).EditedFormattedValue), "0")
                    LBLTOTALFIRKA.Text = Format(Val(LBLTOTALFIRKA.Text) + Val(ROW.Cells(GFIRKA.Index).EditedFormattedValue), "0")
                    LBLTOTALNALI.Text = Format(Val(LBLTOTALNALI.Text) + Val(ROW.Cells(GNALI.Index).EditedFormattedValue), "0")
                End If
            Next
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
                Dim DTTABLE As New DataTable
                If FRMSTRING = "YARNWARPER" Then
                    DTTABLE = OBJCMN.search("  ISNULL(STOCKMASTER_YARNWARPER.SMYARNWARPER_LRNO, '') AS LRNO ", "", " STOCKMASTER_YARNWARPER INNER JOIN LEDGERS AS TRANSPORT ON STOCKMASTER_YARNWARPER.SMYARNWARPER_TRANSPORTID = TRANSPORT.Acc_id AND STOCKMASTER_YARNWARPER.SMYARNWARPER_YEARID = TRANSPORT.Acc_yearid ", " AND STOCKMASTER_YARNWARPER.SMYARNWARPER_LRNO = '" & TXTLRNO.Text.Trim & "' AND TRANSPORT.ACC_CMPNAME = '" & CMBTRANS.Text.Trim & "' AND STOCKMASTER_YARNWARPER.SMYARNWARPER_YEARID = " & YearId)
                ElseIf FRMSTRING = "YARNWEAVER" Then
                    DTTABLE = OBJCMN.search("  ISNULL(STOCKMASTER_YARNWEAVER.SMYARNWEAVER_LRNO, '') AS LRNO ", "", " STOCKMASTER_YARNWEAVER INNER JOIN LEDGERS AS TRANSPORT ON STOCKMASTER_YARNWEAVER.SMYARNWEAVER_TRANSPORTID = TRANSPORT.Acc_id AND STOCKMASTER_YARNWEAVER.SMYARNWEAVER_YEARID = TRANSPORT.Acc_yearid ", " AND STOCKMASTER_YARNWEAVER.SMYARNWEAVER_LRNO = '" & TXTLRNO.Text.Trim & "' AND TRANSPORT.ACC_CMPNAME = '" & CMBTRANS.Text.Trim & "' AND STOCKMASTER_YARNWEAVER.SMYARNWEAVER_YEARID = " & YearId)
                ElseIf FRMSTRING = "YARNSIZER" Then
                    DTTABLE = OBJCMN.search("  ISNULL(STOCKMASTER_YARNSIZER.SMYARNSIZER_LRNO, '') AS LRNO ", "", " STOCKMASTER_YARNSIZER INNER JOIN LEDGERS AS TRANSPORT ON STOCKMASTER_YARNSIZER.SMYARNSIZER_TRANSPORTID = TRANSPORT.Acc_id AND STOCKMASTER_YARNSIZER.SMYARNSIZER_YEARID = TRANSPORT.Acc_yearid ", " AND STOCKMASTER_YARNSIZER.SMYARNSIZER_LRNO = '" & TXTLRNO.Text.Trim & "' AND TRANSPORT.ACC_CMPNAME = '" & CMBTRANS.Text.Trim & "' AND STOCKMASTER_YARNSIZER.SMYARNSIZER_YEARID = " & YearId)
                ElseIf FRMSTRING = "YARNDYEING" Then
                    DTTABLE = OBJCMN.search("  ISNULL(STOCKMASTER_YARNDYEING.SMYARNDYEING_LRNO, '') AS LRNO ", "", " STOCKMASTER_YARNDYEING INNER JOIN LEDGERS AS TRANSPORT ON STOCKMASTER_YARNDYEING.SMYARNDYEING_TRANSPORTID = TRANSPORT.Acc_id AND STOCKMASTER_YARNDYEING.SMYARNDYEING_YEARID = TRANSPORT.Acc_yearid ", " AND STOCKMASTER_YARNDYEING.SMYARNDYEING_LRNO = '" & TXTLRNO.Text.Trim & "' AND TRANSPORT.ACC_CMPNAME = '" & CMBTRANS.Text.Trim & "' AND STOCKMASTER_YARNDYEING.SMYARNDYEING_YEARID = " & YearId)
                ElseIf FRMSTRING = "YARNJOBBER" Then
                    DTTABLE = OBJCMN.search("  ISNULL(STOCKMASTER_YARNJOBBER.SMYARNJOBBER_LRNO, '') AS LRNO ", "", " STOCKMASTER_YARNJOBBER INNER JOIN LEDGERS AS TRANSPORT ON STOCKMASTER_YARNJOBBER.SMYARNJOBBER_TRANSPORTID = TRANSPORT.Acc_id AND STOCKMASTER_YARNJOBBER.SMYARNJOBBER_YEARID = TRANSPORT.Acc_yearid ", " AND STOCKMASTER_YARNJOBBER.SMYARNJOBBER_LRNO = '" & TXTLRNO.Text.Trim & "' AND TRANSPORT.ACC_CMPNAME = '" & CMBTRANS.Text.Trim & "' AND STOCKMASTER_YARNJOBBER.SMYARNJOBBER_YEARID = " & YearId)
                End If
                If DTTABLE.Rows.Count > 0 Then
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
                Dim DTTABLE As New DataTable
                If FRMSTRING = "YARNWARPER" Then
                    DTTABLE = OBJCMN.search(" ISNULL(STOCKMASTER_YARNWARPER.SMYARNWARPER_GODRECNO,'') AS GOERECNO ", "", "  STOCKMASTER_YARNWARPER INNER JOIN GODOWNMASTER ON STOCKMASTER_YARNWARPER.SMYARNWARPER_GODOWNID = GODOWNMASTER.GODOWN_ID", " AND STOCKMASTER_YARNWARPER.SMYARNWARPER_GODRECNO = '" & TXTGODRECNO.Text.Trim & "' AND GODOWNMASTER.GODOWN_NAME = '" & CMBGODOWN.Text.Trim & "' AND STOCKMASTER_YARNWARPER.SMYARNWARPER_YEARID = " & YearId)
                ElseIf FRMSTRING = "YARNWEAVER" Then
                    DTTABLE = OBJCMN.search(" ISNULL(STOCKMASTER_YARNWEAVER.SMYARNWEAVER_GODRECNO,'') AS GOERECNO ", "", "  STOCKMASTER_YARNWEAVER INNER JOIN GODOWNMASTER ON STOCKMASTER_YARNWEAVER.SMYARNWEAVER_GODOWNID = GODOWNMASTER.GODOWN_ID", " AND STOCKMASTER_YARNWEAVER.SMYARNWEAVER_GODRECNO = '" & TXTGODRECNO.Text.Trim & "' AND GODOWNMASTER.GODOWN_NAME = '" & CMBGODOWN.Text.Trim & "' AND STOCKMASTER_YARNWEAVER.SMYARNWEAVER_YEARID = " & YearId)
                ElseIf FRMSTRING = "YARNSIZER" Then
                    DTTABLE = OBJCMN.search(" ISNULL(STOCKMASTER_YARNSIZER.SMYARNSIZER_GODRECNO,'') AS GOERECNO ", "", "  STOCKMASTER_YARNSIZER INNER JOIN GODOWNMASTER ON STOCKMASTER_YARNSIZER.SMYARNSIZER_GODOWNID = GODOWNMASTER.GODOWN_ID", " AND STOCKMASTER_YARNSIZER.SMYARNSIZER_GODRECNO = '" & TXTGODRECNO.Text.Trim & "' AND GODOWNMASTER.GODOWN_NAME = '" & CMBGODOWN.Text.Trim & "' AND STOCKMASTER_YARNSIZER.SMYARNSIZER_YEARID = " & YearId)
                ElseIf FRMSTRING = "YARNDYEING" Then
                    DTTABLE = OBJCMN.search(" ISNULL(STOCKMASTER_YARNDYEING.SMYARNDYEING_GODRECNO,'') AS GOERECNO ", "", "  STOCKMASTER_YARNDYEING INNER JOIN GODOWNMASTER ON STOCKMASTER_YARNDYEING.SMYARNDYEING_GODOWNID = GODOWNMASTER.GODOWN_ID", " AND STOCKMASTER_YARNDYEING.SMYARNDYEING_GODRECNO = '" & TXTGODRECNO.Text.Trim & "' AND GODOWNMASTER.GODOWN_NAME = '" & CMBGODOWN.Text.Trim & "' AND STOCKMASTER_YARNDYEING.SMYARNDYEING_YEARID = " & YearId)
                ElseIf FRMSTRING = "YARNJOBBER" Then
                    DTTABLE = OBJCMN.search(" ISNULL(STOCKMASTER_YARNJOBBER.SMYARNJOBBER_GODRECNO,'') AS GOERECNO ", "", "  STOCKMASTER_YARNJOBBER INNER JOIN GODOWNMASTER ON STOCKMASTER_YARNJOBBER.SMYARNJOBBER_GODOWNID = GODOWNMASTER.GODOWN_ID", " AND STOCKMASTER_YARNJOBBER.SMYARNJOBBER_GODRECNO = '" & TXTGODRECNO.Text.Trim & "' AND GODOWNMASTER.GODOWN_NAME = '" & CMBGODOWN.Text.Trim & "' AND STOCKMASTER_YARNJOBBER.SMYARNJOBBER_YEARID = " & YearId)
                End If
                If DTTABLE.Rows.Count > 0 Then
                    MsgBox("Godown Rec No Already Present !")
                    TXTGODRECNO.Focus()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpeningStockYarnwithSizer_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If ClientName = "SHREEJI" Then
            TXTFRESH.BackColor = Color.White
            TXTWINDING.BackColor = Color.White
            TXTFIRKA.BackColor = Color.White
            TXTNALI.BackColor = Color.White
        End If
    End Sub
End Class