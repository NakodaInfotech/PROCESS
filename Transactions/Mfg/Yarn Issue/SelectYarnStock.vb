
Imports BL

Public Class SelectYarnStock

    Public DT As New DataTable
    Public GODOWN, FRMSTRING, PARTYNAME As String
    Public ENTRYDATE As Date
    Dim TEMPMSG As Integer

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SelectStock_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub SelectStock_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FILLGRID(" ")
    End Sub

    Sub FILLGRID(ByVal WHERE As String)
        Try
            Cursor.Current = Cursors.WaitCursor

            If GODOWN <> "" Then WHERE = WHERE & " AND GODOWN = '" & GODOWN & "'"
            WHERE = WHERE & " AND STOCKVIEW_OURGODOWN.DATE <= '" & Format(ENTRYDATE.Date, "MM/dd/yyyy") & "'"

            Dim objcmn As New ClsCommon
            Dim dt As DataTable
            If FRMSTRING = "ISSUETOADDA" Then
                dt = objcmn.search("CAST(0 AS BIT) AS CHK,  LEDGERS.Acc_cmpname AS NAME, ISNULL(MILLLEDGERS.Acc_cmpname, '') AS MILLNAME, ISNULL(QUALITYMASTER.QUALITY_NAME, '') AS QUALITY, ISNULL(YARNISSUEWARPER_DESC.YISSUEWARPER_BAGS, 0) AS BAGS, ISNULL(YARNISSUEWARPER_DESC.YISSUEWARPER_WT, 0) AS WT, YARNISSUEWARPER_DESC.YISSUEWARPER_NO AS FROMNO, YARNISSUEWARPER_DESC.YISSUEWARPER_GRIDSRNO AS FROMSRNO, 'YARNISSUEWARPER' AS FROMTYPE, ISNULL(YARNISSUEWARPER_DESC.YISSUEWARPER_FRESH, '') AS FRESH, ISNULL(YARNISSUEWARPER_DESC.YISSUEWARPER_WINDING, 0) AS WINDING, ISNULL(YARNISSUEWARPER_DESC.YISSUEWARPER_FIRKA, 0) AS FIRKA, ISNULL(YARNISSUEWARPER_DESC.YISSUEWARPER_LOTNO, '') AS LOTNO, ISNULL(COLORMASTER.COLOR_NAME, '') AS SHADE, ISNULL(YARNISSUEWARPER_DESC.YISSUEWARPER_BAGNO, '') AS BAGNO ", "", " YARNISSUEWARPER INNER JOIN YARNISSUEWARPER_DESC ON YARNISSUEWARPER.YISSUEWARPER_no = YARNISSUEWARPER_DESC.YISSUEWARPER_NO AND YARNISSUEWARPER.YISSUEWARPER_yearid = YARNISSUEWARPER_DESC.YISSUEWARPER_YEARID INNER JOIN LEDGERS ON YARNISSUEWARPER.YISSUEWARPER_ledgerid = LEDGERS.Acc_id INNER JOIN QUALITYMASTER ON YARNISSUEWARPER_DESC.YISSUEWARPER_QUALITYID = QUALITYMASTER.QUALITY_ID LEFT OUTER JOIN COLORMASTER ON YARNISSUEWARPER_DESC.YISSUEWARPER_COLORID = COLORMASTER.COLOR_ID LEFT OUTER JOIN LEDGERS AS MILLLEDGERS ON YARNISSUEWARPER_DESC.YISSUEWARPER_MILLID = MILLLEDGERS.Acc_id ", " AND ISNULL(YARNISSUEWARPER_DESC.YISSUEWARPER_WT, 0)-ISNULL(YARNISSUEWARPER_DESC.YISSUEWARPER_OUTWT, 0)>0 AND ISNULL(LEDGERS.ACC_CMPNAME,'') = '" & PARTYNAME & "' AND YARNISSUEWARPER_DESC.YISSUEWARPER_YEARID = " & YearId)
            Else
                dt = objcmn.search("CAST(0 AS BIT) AS CHK,NAME,MILLNAME,QUALITY,BAGS ,WT,NO AS FROMNO ,SRNO AS FROMSRNO,TYPE AS FROMTYPE, FRESH, WINDING, FIRKA, LOTNO, SHADE, BAGNO ", "", "STOCKVIEW_OURGODOWN", " " & WHERE & " AND YEARID = " & YearId)
            End If
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try

    End Sub

    Private Sub cmdok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try

            DT.Columns.Add("NAME")
            DT.Columns.Add("MILLNAME")
            DT.Columns.Add("QUALITY")
            DT.Columns.Add("LOTNO")
            DT.Columns.Add("SHADE")
            DT.Columns.Add("BAGS")
            DT.Columns.Add("BAGNO")
            DT.Columns.Add("GROSSWT")
            DT.Columns.Add("TAREWT")
            DT.Columns.Add("WT")
            DT.Columns.Add("FRESH")
            DT.Columns.Add("WINDING")
            DT.Columns.Add("FIRKA")
            DT.Columns.Add("FROMNO")
            DT.Columns.Add("FROMSRNO")
            DT.Columns.Add("FROMTYPE")

            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    DT.Rows.Add(dtrow("NAME"), dtrow("MILLNAME"), dtrow("QUALITY"), dtrow("LOTNO"), dtrow("SHADE"), Val(dtrow("BAGS")), dtrow("BAGNO"), Val(dtrow("WT")), 0, Val(dtrow("WT")), Val(dtrow("FRESH")), Val(dtrow("WINDING")), Val(dtrow("FIRKA")), dtrow("FROMNO"), dtrow("FROMSRNO"), dtrow("FROMTYPE"))
                End If
            Next
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKSELECTALL_CheckedChanged(sender As Object, e As EventArgs) Handles CHKSELECTALL.CheckedChanged
        Try
            If gridbilldetails.Visible = True Then
                For i As Integer = 0 To gridbill.RowCount - 1
                    Dim dtrow As DataRow = gridbill.GetDataRow(i)
                    dtrow("CHK") = CHKSELECTALL.Checked
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SelectYarnStock_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If FRMSTRING = "ISSUETOWEAVER" Then
            GSUPPLIERNAME.Visible = False
            GBAG.Visible = False
            GFIRKA.Visible = False
            GFRESH.Visible = False
            GWINDING.Visible = False
        End If
    End Sub
End Class