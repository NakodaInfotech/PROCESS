
Imports BL

Public Class YarnIssueGridDetails

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public FRMSTRING As String

    Private Sub CMDEXIT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub YarnIssueGridDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Or (e.KeyCode = Keys.X And e.Alt = True) Then
                Me.Close()
            ElseIf e.KeyCode = Keys.N And e.Control = True Then
                showform(False, 0)
            ElseIf e.KeyCode = Keys.O And e.Alt = True Then
                CMDEDIT_Click(sender, e)
            ElseIf e.KeyCode = Keys.P And e.Alt = True Then
                TOOLEXCEL_Click(sender, e)
            ElseIf e.KeyCode = Keys.R And e.Alt = True Then
                TOOLREFRESH_Click(sender, e)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub YarnIssueGridDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'MFG'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            If FRMSTRING = "ISSUETOWARPER" Then
                Me.Text = "Yarn Issue To Warper Details"
            ElseIf FRMSTRING = "ISSUETOSIZER" Then
                Me.Text = "Yarn Issue To Sizer Details"
            ElseIf FRMSTRING = "ISSUETOWEAVER" Then
                Me.Text = "Yarn Issue To Weaver Details"
            ElseIf FRMSTRING = "ISSUETODYEING" Then
                Me.Text = "Yarn Issue To Dyeing Details"
            End If

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            fillgrid()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid()
        Try
            Dim DT As New DataTable
            If FRMSTRING = "ISSUETOWARPER" Then
                Dim OBJISS As New ClsYarnIssueToWarper
                OBJISS.alParaval.Add(0)
                OBJISS.alParaval.Add(YearId)
                DT = OBJISS.selectYARNISSUE
            ElseIf FRMSTRING = "ISSUETOADDA" Then
                Dim OBJISS As New ClsYarnIssueToAdda
                OBJISS.alParaval.Add(0)
                OBJISS.alParaval.Add(YearId)
                DT = OBJISS.selectYARNISSUE
            ElseIf FRMSTRING = "ISSUETOSIZER" Then
                Dim OBJISS As New ClsYarnIssueToSizer
                OBJISS.alParaval.Add(0)
                OBJISS.alParaval.Add(YearId)
                DT = OBJISS.selectYARNISSUE
            ElseIf FRMSTRING = "ISSUETOWEAVER" Then
                Dim OBJISS As New ClsYarnIssueToWeaver
                OBJISS.alParaval.Add(0)
                OBJISS.alParaval.Add(YearId)
                DT = OBJISS.selectYARNISSUE
            ElseIf FRMSTRING = "ISSUETODYEING" Then
                Dim OBJISS As New ClsYarnIssueToDyeing
                OBJISS.alParaval.Add(0)
                OBJISS.alParaval.Add(YearId)
                DT = OBJISS.selectYARNISSUE
            End If
            gridbilldetails.DataSource = DT
            If DT.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal ISSUENO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim objDO As New YarnIssue
                objDO.MdiParent = MDIMain
                objDO.EDIT = editval
                objDO.TEMPISSUENO = ISSUENO
                objDO.FRMSTRING = FRMSTRING
                objDO.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDADD.Click
        Try
            If USERADD = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            showform(False, 0)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbilldetails_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbilldetails.DoubleClick
        Try

            showform(True, gridbill.GetFocusedRowCellValue("YISSUENO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEDIT.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("YISSUENO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLEXCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLEXCEL.Click
        Try
            If FRMSTRING = "ISSUETOWARPER" Then
                Dim PATH As String = Application.StartupPath & "\Yarn Issue To Warper Details.XLS"
                Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
                opti.ShowGridLines = True

                For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                    proc.Kill()
                Next
                opti.SheetName = "Yarn Issue To Warper Details"
                gridbill.ExportToXls(PATH, opti)
                EXCELCMPHEADER(PATH, "Yarn Issue To Warper Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)

            ElseIf FRMSTRING = "ISSUETOADDA" Then
                Dim PATH As String = Application.StartupPath & "\Yarn Issue To ADDA Details.XLS"
                Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
                opti.ShowGridLines = True

                For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                    proc.Kill()
                Next
                opti.SheetName = "Yarn Issue To ADDA Details"
                gridbill.ExportToXls(PATH, opti)
                EXCELCMPHEADER(PATH, "Yarn Issue To ADDA Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)

            ElseIf FRMSTRING = "ISSUETOSIZER" Then
                Dim PATH As String = Application.StartupPath & "\Yarn Issue To Sizer Details.XLS"
                Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
                opti.ShowGridLines = True

                For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                    proc.Kill()
                Next
                opti.SheetName = "Yarn Issue To Sizer Details"
                gridbill.ExportToXls(PATH, opti)
                EXCELCMPHEADER(PATH, "Yarn Issue To Sizer Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)

            ElseIf FRMSTRING = "ISSUETOWEAVER" Then

                Dim PATH As String = Application.StartupPath & "\Yarn Issue To Weaver Details.XLS"
                Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
                opti.ShowGridLines = True

                For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                    proc.Kill()
                Next
                opti.SheetName = "Yarn Issue To Weaver Details"
                gridbill.ExportToXls(PATH, opti)
                EXCELCMPHEADER(PATH, "Yarn Issue To Weaver Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)

            ElseIf FRMSTRING = "ISSUETODYEING" Then

                Dim PATH As String = Application.StartupPath & "\Yarn Issue To Dyeing Details.XLS"
                Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
                opti.ShowGridLines = True

                For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                    proc.Kill()
                Next
                opti.SheetName = "Yarn Issue To Dyeing Details"
                gridbill.ExportToXls(PATH, opti)
                EXCELCMPHEADER(PATH, "Yarn Issue To Dyeing Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLREFRESH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLREFRESH.Click
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            PRINTREPORT()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PRINTREPORT()
        Try
            If Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Then Exit Sub
            If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                MsgBox("Enter Proper Yarn Issue Nos", MsgBoxStyle.Critical)
                Exit Sub
            End If
            If MsgBox("Wish to Yarn Issue from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings

            If FRMSTRING = "ISSUETOWARPER" Then
                serverprop(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), "", "YARNISSUEWARPER", Val(TXTCOPIES.Text.Trim), PRINTDIALOG)
            ElseIf FRMSTRING = "ISSUETOADDA" Then
                serverprop(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), "", "YARNISSUEADDA", Val(TXTCOPIES.Text.Trim), PRINTDIALOG)
            ElseIf FRMSTRING = "ISSUETOSIZER" Then
                serverprop(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), "", "YARNISSUESIZER", Val(TXTCOPIES.Text.Trim), PRINTDIALOG)
            ElseIf FRMSTRING = "ISSUETOWEAVER" Then
                serverprop(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), "", "YARNISSUEWEAVER", Val(TXTCOPIES.Text.Trim), PRINTDIALOG)
            ElseIf FRMSTRING = "ISSUETODYEING" Then
                serverprop(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), "", "YARNISSUEDYEING", Val(TXTCOPIES.Text.Trim), PRINTDIALOG)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTFROM_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTFROM.Validated
        If TXTFROM.Text.Trim <> "" Then TXTTO.Focus()
    End Sub

    Private Sub TXTCOPIES_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTCOPIES.Validating
        If Val(TXTCOPIES.Text.Trim) <= 0 Then TXTCOPIES.Text = 1
    End Sub

End Class