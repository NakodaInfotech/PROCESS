
Imports BL

Public Class YarnWastageDetails

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public FRMSTRING As String

    Private Sub CMDEXIT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub YarnISSUETOWARPERDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub YarnISSUETOWARPERDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'MFG'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)


            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If FRMSTRING = "WASTAGEWARPER" Then
                Me.Text = "Yarn Wastage From Warper"
            ElseIf FRMSTRING = "WASTAGESIZER" Then
                Me.Text = "Yarn Wastage From Sizer"
            ElseIf FRMSTRING = "WASTAGEWEAVER" Then
                Me.Text = "Yarn Wastage From Weaver"
            ElseIf FRMSTRING = "WASTAGEDYEING" Then
                Me.Text = "Yarn Wastage From Dyeing"
            ElseIf FRMSTRING = "WASTAGEGODOWN" Then
                Me.Text = "Yarn Wastage at Godown"
                GGODOWN.Visible = True
                GNAME.Visible = False
            ElseIf FRMSTRING = "WASTAGEJOBBER" Then
                Me.Text = "Yarn Wastage From Jobber"
            ElseIf FRMSTRING = "WASTAGEMACHINE" Then
                Me.Text = "Yarn Wastage From Machine"
            End If

            fillgrid()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid()
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As New DataTable
            If FRMSTRING = "WASTAGEWARPER" Then
                dt = objclsCMST.search(" YARNWASTAGEWARPER.YWASWARPER_NO AS SRNO, YARNWASTAGEWARPER.YWASWARPER_DATE AS DATE, LEDGERS.Acc_cmpname AS NAME, '' AS GODOWN, YARNWASTAGEWARPER_DESC.YWASWARPER_TYPE AS TYPE, QUALITYMASTER.QUALITY_NAME AS QUALITY, ISNULL(MILLLEDGERS.Acc_cmpname, '') AS MILLNAME, ISNULL(YARNWASTAGEWARPER_DESC.YWASWARPER_TAKA,0) AS TAKA, ISNULL(YARNWASTAGEWARPER_DESC.YWASWARPER_STOCKWT,0) AS STOCKWT, ISNULL(YARNWASTAGEWARPER_DESC.YWASWARPER_ACTUALWT,0) AS ACTUALWT, YARNWASTAGEWARPER_DESC.YWASWARPER_WT AS WT, YARNWASTAGEWARPER_DESC.YWASWARPER_NARRATION AS NARRATION ", "", " YARNWASTAGEWARPER INNER JOIN LEDGERS ON YARNWASTAGEWARPER.YWASWARPER_LEDGERID = LEDGERS.Acc_id INNER JOIN YARNWASTAGEWARPER_DESC ON YARNWASTAGEWARPER.YWASWARPER_NO = YARNWASTAGEWARPER_DESC.YWASWARPER_NO AND YARNWASTAGEWARPER.YWASWARPER_YEARID = YARNWASTAGEWARPER_DESC.YWASWARPER_YEARID INNER JOIN QUALITYMASTER ON YARNWASTAGEWARPER_DESC.YWASWARPER_QUALITYID = QUALITYMASTER.QUALITY_ID LEFT OUTER JOIN LEDGERS AS MILLLEDGERS ON YARNWASTAGEWARPER_DESC.YWASWARPER_MILLID = MILLLEDGERS.Acc_id ", " AND dbo.YARNWASTAGEWARPER.YWASWARPER_yearid=" & YearId & " ORDER by dbo.YARNWASTAGEWARPER.YWASWARPER_NO ")
            ElseIf FRMSTRING = "WASTAGESIZER" Then
                dt = objclsCMST.search(" YARNWASTAGESIZER.YWASSIZER_NO AS SRNO, YARNWASTAGESIZER.YWASSIZER_DATE AS DATE, LEDGERS.Acc_cmpname AS NAME, '' AS GODOWN, YARNWASTAGESIZER_DESC.YWASSIZER_TYPE AS TYPE, QUALITYMASTER.QUALITY_NAME AS QUALITY, ISNULL(MILLLEDGERS.Acc_cmpname, '') AS MILLNAME, ISNULL(YARNWASTAGESIZER_DESC.YWASSIZER_TAKA,0) AS TAKA, ISNULL(YARNWASTAGESIZER_DESC.YWASSIZER_STOCKWT,0) AS STOCKWT, ISNULL(YARNWASTAGESIZER_DESC.YWASSIZER_ACTUALWT,0) AS ACTUALWT, YARNWASTAGESIZER_DESC.YWASSIZER_WT AS WT, YARNWASTAGESIZER_DESC.YWASSIZER_NARRATION AS NARRATION ", "", " YARNWASTAGESIZER INNER JOIN LEDGERS ON YARNWASTAGESIZER.YWASSIZER_LEDGERID = LEDGERS.Acc_id INNER JOIN YARNWASTAGESIZER_DESC ON YARNWASTAGESIZER.YWASSIZER_NO = YARNWASTAGESIZER_DESC.YWASSIZER_NO AND YARNWASTAGESIZER.YWASSIZER_YEARID = YARNWASTAGESIZER_DESC.YWASSIZER_YEARID INNER JOIN QUALITYMASTER ON YARNWASTAGESIZER_DESC.YWASSIZER_QUALITYID = QUALITYMASTER.QUALITY_ID LEFT OUTER JOIN LEDGERS AS MILLLEDGERS ON YARNWASTAGESIZER_DESC.YWASSIZER_MILLID = MILLLEDGERS.Acc_id ", " AND dbo.YARNWASTAGESIZER.YWASSIZER_yearid=" & YearId & " ORDER by dbo.YARNWASTAGESIZER.YWASSIZER_NO ")
            ElseIf FRMSTRING = "WASTAGEWEAVER" Then
                dt = objclsCMST.search(" YARNWASTAGEWEAVER.YWASWEAVER_NO AS SRNO, YARNWASTAGEWEAVER.YWASWEAVER_DATE AS DATE, LEDGERS.Acc_cmpname AS NAME, '' AS GODOWN, YARNWASTAGEWEAVER_DESC.YWASWEAVER_TYPE AS TYPE, QUALITYMASTER.QUALITY_NAME AS QUALITY, ISNULL(MILLLEDGERS.Acc_cmpname, '') AS MILLNAME, ISNULL(YARNWASTAGEWEAVER_DESC.YWASWEAVER_TAKA,0) AS TAKA, ISNULL(YARNWASTAGEWEAVER_DESC.YWASWEAVER_STOCKWT,0) AS STOCKWT, ISNULL(YARNWASTAGEWEAVER_DESC.YWASWEAVER_ACTUALWT,0) AS ACTUALWT, YARNWASTAGEWEAVER_DESC.YWASWEAVER_WT AS WT, YARNWASTAGEWEAVER_DESC.YWASWEAVER_NARRATION AS NARRATION ", "", " YARNWASTAGEWEAVER INNER JOIN LEDGERS ON YARNWASTAGEWEAVER.YWASWEAVER_LEDGERID = LEDGERS.Acc_id INNER JOIN YARNWASTAGEWEAVER_DESC ON YARNWASTAGEWEAVER.YWASWEAVER_NO = YARNWASTAGEWEAVER_DESC.YWASWEAVER_NO AND YARNWASTAGEWEAVER.YWASWEAVER_YEARID = YARNWASTAGEWEAVER_DESC.YWASWEAVER_YEARID INNER JOIN QUALITYMASTER ON YARNWASTAGEWEAVER_DESC.YWASWEAVER_QUALITYID = QUALITYMASTER.QUALITY_ID LEFT OUTER JOIN LEDGERS AS MILLLEDGERS ON YARNWASTAGEWEAVER_DESC.YWASWEAVER_MILLID = MILLLEDGERS.Acc_id ", " AND dbo.YARNWASTAGEWEAVER.YWASWEAVER_yearid=" & YearId & " ORDER by dbo.YARNWASTAGEWEAVER.YWASWEAVER_NO ")
            ElseIf FRMSTRING = "WASTAGEDYEING" Then
                dt = objclsCMST.search(" YARNWASTAGEDYEING.YWASDYEING_NO AS SRNO, YARNWASTAGEDYEING.YWASDYEING_DATE AS DATE, LEDGERS.Acc_cmpname AS NAME, '' AS GODOWN, YARNWASTAGEDYEING_DESC.YWASDYEING_TYPE AS TYPE, QUALITYMASTER.QUALITY_NAME AS QUALITY, ISNULL(MILLLEDGERS.Acc_cmpname, '') AS MILLNAME, ISNULL(YARNWASTAGEDYEING_DESC.YWASDYEING_TAKA,0) AS TAKA, ISNULL(YARNWASTAGEDYEING_DESC.YWASDYEING_STOCKWT,0) AS STOCKWT, ISNULL(YARNWASTAGEDYEING_DESC.YWASDYEING_ACTUALWT,0) AS ACTUALWT, YARNWASTAGEDYEING_DESC.YWASDYEING_WT AS WT, YARNWASTAGEDYEING_DESC.YWASDYEING_NARRATION AS NARRATION ", "", " YARNWASTAGEDYEING INNER JOIN LEDGERS ON YARNWASTAGEDYEING.YWASDYEING_LEDGERID = LEDGERS.Acc_id INNER JOIN YARNWASTAGEDYEING_DESC ON YARNWASTAGEDYEING.YWASDYEING_NO = YARNWASTAGEDYEING_DESC.YWASDYEING_NO AND YARNWASTAGEDYEING.YWASDYEING_YEARID = YARNWASTAGEDYEING_DESC.YWASDYEING_YEARID INNER JOIN QUALITYMASTER ON YARNWASTAGEDYEING_DESC.YWASDYEING_QUALITYID = QUALITYMASTER.QUALITY_ID LEFT OUTER JOIN LEDGERS AS MILLLEDGERS ON YARNWASTAGEDYEING_DESC.YWASDYEING_MILLID = MILLLEDGERS.Acc_id ", " AND dbo.YARNWASTAGEDYEING.YWASDYEING_yearid=" & YearId & " ORDER by dbo.YARNWASTAGEDYEING.YWASDYEING_NO ")
            ElseIf FRMSTRING = "WASTAGEGODOWN" Then
                dt = objclsCMST.search(" YARNWASTAGEGODOWN.YWASGODOWN_NO AS SRNO, YARNWASTAGEGODOWN.YWASGODOWN_DATE AS DATE, LEDGERS.Acc_cmpname AS NAME, '' AS GODOWN, YARNWASTAGEGODOWN_DESC.YWASGODOWN_TYPE AS TYPE, QUALITYMASTER.QUALITY_NAME AS QUALITY, ISNULL(MILLLEDGERS.Acc_cmpname, '') AS MILLNAME, ISNULL(YARNWASTAGEGODOWN_DESC.YWASGODOWN_TAKA,0) AS TAKA, ISNULL(YARNWASTAGEGODOWN_DESC.YWASGODOWN_STOCKWT,0) AS STOCKWT, ISNULL(YARNWASTAGEGODOWN_DESC.YWASGODOWN_ACTUALWT,0) AS ACTUALWT, ISNULL(YARNWASTAGEGODOWN_DESC.YWASGODOWN_WT,0) AS WT, YARNWASTAGEGODOWN_DESC.YWASGODOWN_NARRATION AS NARRATION ", "", " YARNWASTAGEGODOWN INNER JOIN LEDGERS ON YARNWASTAGEGODOWN.YWASGODOWN_LEDGERID = LEDGERS.Acc_id INNER JOIN YARNWASTAGEGODOWN_DESC ON YARNWASTAGEGODOWN.YWASGODOWN_NO = YARNWASTAGEGODOWN_DESC.YWASGODOWN_NO AND YARNWASTAGEGODOWN.YWASGODOWN_YEARID = YARNWASTAGEGODOWN_DESC.YWASGODOWN_YEARID INNER JOIN QUALITYMASTER ON YARNWASTAGEGODOWN_DESC.YWASGODOWN_QUALITYID = QUALITYMASTER.QUALITY_ID LEFT OUTER JOIN LEDGERS AS MILLLEDGERS ON YARNWASTAGEGODOWN_DESC.YWASGODOWN_MILLID = MILLLEDGERS.Acc_id ", " AND dbo.YARNWASTAGEGODOWN.YWASGODOWN_yearid=" & YearId & " ORDER by dbo.YARNWASTAGEGODOWN.YWASGODOWN_NO ")
            ElseIf FRMSTRING = "WASTAGEJOBBER" Then
                dt = objclsCMST.search(" YARNWASTAGEJOBBER.YWASJOBBER_NO AS SRNO, YARNWASTAGEJOBBER.YWASJOBBER_DATE AS DATE, LEDGERS.Acc_cmpname AS NAME, '' AS GODOWN, YARNWASTAGEJOBBER_DESC.YWASJOBBER_TYPE AS TYPE, QUALITYMASTER.QUALITY_NAME AS QUALITY, ISNULL(MILLLEDGERS.Acc_cmpname, '') AS MILLNAME, ISNULL(YARNWASTAGEJOBBER_DESC.YWASJOBBER_TAKA,0) AS TAKA, ISNULL(YARNWASTAGEJOBBER_DESC.YWASJOBBER_STOCKWT,0) AS STOCKWT, ISNULL(YARNWASTAGEJOBBER_DESC.YWASJOBBER_ACTUALWT,0) AS ACTUALWT, YARNWASTAGEJOBBER_DESC.YWASJOBBER_WT AS WT, YARNWASTAGEJOBBER_DESC.YWASJOBBER_NARRATION AS NARRATION ", "", " YARNWASTAGEJOBBER INNER JOIN LEDGERS ON YARNWASTAGEJOBBER.YWASJOBBER_LEDGERID = LEDGERS.Acc_id INNER JOIN YARNWASTAGEJOBBER_DESC ON YARNWASTAGEJOBBER.YWASJOBBER_NO = YARNWASTAGEJOBBER_DESC.YWASJOBBER_NO AND YARNWASTAGEJOBBER.YWASJOBBER_YEARID = YARNWASTAGEJOBBER_DESC.YWASJOBBER_YEARID INNER JOIN QUALITYMASTER ON YARNWASTAGEJOBBER_DESC.YWASJOBBER_QUALITYID = QUALITYMASTER.QUALITY_ID LEFT OUTER JOIN LEDGERS AS MILLLEDGERS ON YARNWASTAGEJOBBER_DESC.YWASJOBBER_MILLID = MILLLEDGERS.Acc_id ", " AND dbo.YARNWASTAGEJOBBER.YWASJOBBER_yearid=" & YearId & " ORDER by dbo.YARNWASTAGEJOBBER.YWASJOBBER_NO ")
            ElseIf FRMSTRING = "WASTAGEMACHINE" Then
                dt = objclsCMST.search(" YARNWASTAGEMACHINE.YWASMACHINE_NO AS SRNO, YARNWASTAGEMACHINE.YWASMACHINE_DATE AS DATE, LEDGERS.Acc_cmpname AS NAME, '' AS GODOWN, YARNWASTAGEMACHINE_DESC.YWASMACHINE_TYPE AS TYPE, QUALITYMASTER.QUALITY_NAME AS QUALITY, ISNULL(MILLLEDGERS.Acc_cmpname, '') AS MILLNAME, ISNULL(YARNWASTAGEMACHINE_DESC.YWASMACHINE_TAKA,0) AS TAKA, ISNULL(YARNWASTAGEMACHINE_DESC.YWASMACHINE_STOCKWT,0) AS STOCKWT, ISNULL(YARNWASTAGEMACHINE_DESC.YWASMACHINE_ACTUALWT,0) AS ACTUALWT, YARNWASTAGEMACHINE_DESC.YWASMACHINE_WT AS WT, YARNWASTAGEMACHINE_DESC.YWASMACHINE_NARRATION AS NARRATION ", "", " YARNWASTAGEMACHINE INNER JOIN LEDGERS ON YARNWASTAGEMACHINE.YWASMACHINE_LEDGERID = LEDGERS.Acc_id INNER JOIN YARNWASTAGEMACHINE_DESC ON YARNWASTAGEMACHINE.YWASMACHINE_NO = YARNWASTAGEMACHINE_DESC.YWASMACHINE_NO AND YARNWASTAGEMACHINE.YWASMACHINE_YEARID = YARNWASTAGEMACHINE_DESC.YWASMACHINE_YEARID INNER JOIN QUALITYMASTER ON YARNWASTAGEMACHINE_DESC.YWASMACHINE_QUALITYID = QUALITYMASTER.QUALITY_ID LEFT OUTER JOIN LEDGERS AS MILLLEDGERS ON YARNWASTAGEMACHINE_DESC.YWASMACHINE_MILLID = MILLLEDGERS.Acc_id ", " AND dbo.YARNWASTAGEMACHINE.YWASMACHINE_yearid=" & YearId & " ORDER by dbo.YARNWASTAGEMACHINE.YWASMACHINE_NO ")
            End If
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal WASTAGENO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim objDO As New YarnWastage
                objDO.MdiParent = MDIMain
                objDO.EDIT = editval
                objDO.TEMPWASTAGENO = WASTAGENO
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
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEDIT.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLEXCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLEXCEL.Click
        Try
            If FRMSTRING = "WASTAGEWARPER" Then
                Dim PATH As String = Application.StartupPath & "\Wastage From Warper Details.XLS"
                Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
                opti.ShowGridLines = True

                For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                    proc.Kill()
                Next
                opti.SheetName = "Wastage From Warper Details"
                gridbill.ExportToXls(PATH, opti)
                EXCELCMPHEADER(PATH, "Wastage From Warper Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)

            ElseIf FRMSTRING = "WASTAGESIZER" Then
                Dim PATH As String = Application.StartupPath & "\Wastage From Sizer Details.XLS"
                Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
                opti.ShowGridLines = True

                For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                    proc.Kill()
                Next
                opti.SheetName = "Wastage From Sizer Details"
                gridbill.ExportToXls(PATH, opti)
                EXCELCMPHEADER(PATH, "Wastage From Sizer Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)

            ElseIf FRMSTRING = "WASTAGEWEAVER" Then

                Dim PATH As String = Application.StartupPath & "\Wastage From Weaver Details.XLS"
                Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
                opti.ShowGridLines = True

                For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                    proc.Kill()
                Next
                opti.SheetName = "Wastage From Weaver Details"
                gridbill.ExportToXls(PATH, opti)
                EXCELCMPHEADER(PATH, "Wastage From Weaver Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)

            ElseIf FRMSTRING = "WASTAGEDYEING" Then

                Dim PATH As String = Application.StartupPath & "\Wastage From Dyeing Details.XLS"
                Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
                opti.ShowGridLines = True

                For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                    proc.Kill()
                Next
                opti.SheetName = "Wastage From Dyeing Details"
                gridbill.ExportToXls(PATH, opti)
                EXCELCMPHEADER(PATH, "Wastage From Dyeing Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)

            ElseIf FRMSTRING = "WASTAGEGODOWN" Then

                Dim PATH As String = Application.StartupPath & "\Wastage From Godown Details.XLS"
                Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
                opti.ShowGridLines = True

                For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                    proc.Kill()
                Next
                opti.SheetName = "Wastage From Godown Details"
                gridbill.ExportToXls(PATH, opti)
                EXCELCMPHEADER(PATH, "Wastage From Godown Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)

            ElseIf FRMSTRING = "WASTAGEJOBBER" Then

                Dim PATH As String = Application.StartupPath & "\Wastage From Jobber Details.XLS"
                Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
                opti.ShowGridLines = True

                For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                    proc.Kill()
                Next
                opti.SheetName = "Wastage From Jobber Details"
                gridbill.ExportToXls(PATH, opti)
                EXCELCMPHEADER(PATH, "Wastage From Jobber Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)

            ElseIf FRMSTRING = "WASTAGEMACHINE" Then

                Dim PATH As String = Application.StartupPath & "\Wastage From Machine Details.XLS"
                Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
                opti.ShowGridLines = True

                For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                    proc.Kill()
                Next
                opti.SheetName = "Wastage From Machine Details"
                gridbill.ExportToXls(PATH, opti)
                EXCELCMPHEADER(PATH, "Wastage From Machine Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLREFRESH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLREFRESH.Click
        Try
            fillgrid
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class