
Imports BL

Public Class GSTTaxFilter

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GSTTaxFilter_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.Alt = True And e.KeyCode = Windows.Forms.Keys.X) Or (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                Me.Close()
            ElseIf e.KeyCode = Keys.OemQuotes Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf (e.Alt = True And e.KeyCode = Windows.Forms.Keys.S) Then
                cmdshow_Click(sender, e)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdshow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdshow.Click
        Try

            If RBGSTSUMMARY.Checked = True Then

                If MsgBox("Wish To Mail Directly?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Dim OBJRPT As New clsReportDesigner("GST Tax Summary", System.AppDomain.CurrentDomain.BaseDirectory & "GST Tax Summary.xlsx", 2)
                    If chkdate.CheckState = CheckState.Checked Then
                        OBJRPT.GSTSUMMARYNEW_EXCEL(CmpId, YearId, dtfrom.Value.Date, dtto.Value.Date)
                    Else
                        OBJRPT.GSTSUMMARYNEW_EXCEL(CmpId, YearId, AccFrom, AccTo)
                    End If
                    Exit Sub
                Else
                    Dim OBJRPT As New clsReportDesigner("GST Tax Summary", System.AppDomain.CurrentDomain.BaseDirectory & "GST Tax Summary.xlsx", 0)
                    If chkdate.CheckState = CheckState.Checked Then
                        OBJRPT.GSTSUMMARYNEW_EXCEL(CmpId, YearId, dtfrom.Value.Date, dtto.Value.Date)
                    Else
                        OBJRPT.GSTSUMMARYNEW_EXCEL(CmpId, YearId, AccFrom, AccTo)
                    End If

                    'MAIL EXCEL AS ATTACHMENTS
                    Dim TEMPATTACHMENT As String = System.AppDomain.CurrentDomain.BaseDirectory & "GST Tax Summary.xlsx"
                    Dim objmail As New SendMail
                    objmail.attachment = TEMPATTACHMENT
                    objmail.subject = "GST Tax Symmary"
                    objmail.Show()
                    objmail.BringToFront()
                    Windows.Forms.Cursor.Current = Cursors.Arrow

                    Exit Sub
                End If

            ElseIf RBGSTSALEDETAILS.Checked = True Then

                If MsgBox("Wish To Mail Directly?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Dim OBJRPT As New clsReportDesigner("B2B", System.AppDomain.CurrentDomain.BaseDirectory & "B2B.xlsx", 2)
                    Dim OBJRPTB2CL As New clsReportDesigner("B2CL", System.AppDomain.CurrentDomain.BaseDirectory & "B2CL.xlsx", 2)
                    Dim OBJRPTB2CS As New clsReportDesigner("B2CS", System.AppDomain.CurrentDomain.BaseDirectory & "B2CS.xlsx", 2)
                    Dim OBJRPTCDNR As New clsReportDesigner("CDNR", System.AppDomain.CurrentDomain.BaseDirectory & "CDNR.xlsx", 2)
                    Dim OBJRPTCDNUR As New clsReportDesigner("CDNUR", System.AppDomain.CurrentDomain.BaseDirectory & "CDNUR.xlsx", 2)
                    Dim OBJRPTHSNB2B As New clsReportDesigner("HSNB2B", System.AppDomain.CurrentDomain.BaseDirectory & "HSNB2B.xlsx", 2)
                    Dim OBJRPTHSNB2C As New clsReportDesigner("HSNB2C", System.AppDomain.CurrentDomain.BaseDirectory & "HSNB2C.xlsx", 2)
                    Dim OBJRPTDOCS As New clsReportDesigner("DOCS", System.AppDomain.CurrentDomain.BaseDirectory & "DOCS.xlsx", 2)

                    If chkdate.CheckState = CheckState.Checked Then
                        OBJRPT.GSTB2B_EXCEL(CmpId, YearId, dtfrom.Value.Date, dtto.Value.Date)
                        OBJRPTB2CL.GSTB2CL_EXCEL(CmpId, YearId, dtfrom.Value.Date, dtto.Value.Date)
                        OBJRPTB2CS.GSTB2CS_EXCEL(CmpId, YearId, dtfrom.Value.Date, dtto.Value.Date)
                        OBJRPTCDNR.GSTCDNR_EXCEL(CmpId, YearId, dtfrom.Value.Date, dtto.Value.Date, CMBREGISTER.Text.Trim)
                        OBJRPTCDNUR.GSTCDNUR_EXCEL(CmpId, YearId, dtfrom.Value.Date, dtto.Value.Date, CMBREGISTER.Text.Trim)
                        OBJRPTHSNB2B.GSTHSNB2B_EXCEL(CmpId, YearId, dtfrom.Value.Date, dtto.Value.Date)
                        OBJRPTHSNB2C.GSTHSNB2C_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), INVOICESCREENTYPE, ClientName, CMBREGISTER.Text.Trim)
                        OBJRPTDOCS.GSTDOCS_EXCEL(CmpId, YearId, dtfrom.Value.Date, dtto.Value.Date)
                    Else
                        OBJRPT.GSTB2B_EXCEL(CmpId, YearId, AccFrom, AccTo)
                        OBJRPTB2CL.GSTB2CL_EXCEL(CmpId, YearId, AccFrom, AccTo)
                        OBJRPTB2CS.GSTB2CS_EXCEL(CmpId, YearId, AccFrom, AccTo)
                        OBJRPTCDNR.GSTCDNR_EXCEL(CmpId, YearId, AccFrom, AccTo, CMBREGISTER.Text.Trim)
                        OBJRPTCDNUR.GSTCDNUR_EXCEL(CmpId, YearId, AccFrom, AccTo, CMBREGISTER.Text.Trim)
                        OBJRPTHSNB2B.GSTHSNB2B_EXCEL(CmpId, YearId, AccFrom, AccTo)
                        OBJRPTHSNB2C.GSTHSNB2C_EXCEL(CmpId, YearId, AccFrom, AccTo, INVOICESCREENTYPE, ClientName, CMBREGISTER.Text.Trim)
                        OBJRPTDOCS.GSTDOCS_EXCEL(CmpId, YearId, AccFrom, AccTo)
                    End If

                    Exit Sub
                Else
                    Dim OBJRPT As New clsReportDesigner("B2B", System.AppDomain.CurrentDomain.BaseDirectory & "B2B.xlsx", 0)
                    Dim OBJRPTB2CL As New clsReportDesigner("B2CL", System.AppDomain.CurrentDomain.BaseDirectory & "B2CL.xlsx", 0)
                    Dim OBJRPTB2CS As New clsReportDesigner("B2CS", System.AppDomain.CurrentDomain.BaseDirectory & "B2CS.xlsx", 0)
                    Dim OBJRPTCDNR As New clsReportDesigner("CDNR", System.AppDomain.CurrentDomain.BaseDirectory & "CDNR.xlsx", 0)
                    Dim OBJRPTCDNUR As New clsReportDesigner("CDNUR", System.AppDomain.CurrentDomain.BaseDirectory & "CDNUR.xlsx", 0)
                    Dim OBJRPTHSNB2B As New clsReportDesigner("HSNB2B", System.AppDomain.CurrentDomain.BaseDirectory & "HSNB2B.xlsx", 0)
                    Dim OBJRPTHSNB2C As New clsReportDesigner("HSNB2C", System.AppDomain.CurrentDomain.BaseDirectory & "HSNB2C.xlsx", 0)
                    Dim OBJRPTDOCS As New clsReportDesigner("DOCS", System.AppDomain.CurrentDomain.BaseDirectory & "DOCS.xlsx", 0)
                    If chkdate.CheckState = CheckState.Checked Then
                        OBJRPT.GSTB2B_EXCEL(CmpId, YearId, dtfrom.Value.Date, dtto.Value.Date)
                        OBJRPTB2CL.GSTB2CL_EXCEL(CmpId, YearId, dtfrom.Value.Date, dtto.Value.Date)
                        OBJRPTB2CS.GSTB2CS_EXCEL(CmpId, YearId, dtfrom.Value.Date, dtto.Value.Date)
                        OBJRPTCDNR.GSTCDNR_EXCEL(CmpId, YearId, dtfrom.Value.Date, dtto.Value.Date, CMBREGISTER.Text.Trim)
                        OBJRPTCDNUR.GSTCDNUR_EXCEL(CmpId, YearId, dtfrom.Value.Date, dtto.Value.Date, CMBREGISTER.Text.Trim)
                        OBJRPTHSNB2B.GSTHSNB2B_EXCEL(CmpId, YearId, dtfrom.Value.Date, dtto.Value.Date)
                        OBJRPTHSNB2C.GSTHSNB2C_EXCEL(CmpId, YearId, Convert.ToDateTime(dtfrom.Text), Convert.ToDateTime(dtto.Text), INVOICESCREENTYPE, ClientName, CMBREGISTER.Text.Trim)
                        OBJRPTDOCS.GSTDOCS_EXCEL(CmpId, YearId, dtfrom.Value.Date, dtto.Value.Date)
                    Else
                        OBJRPT.GSTB2B_EXCEL(CmpId, YearId, AccFrom, AccTo)
                        OBJRPTB2CL.GSTB2CL_EXCEL(CmpId, YearId, AccFrom, AccTo)
                        OBJRPTB2CS.GSTB2CS_EXCEL(CmpId, YearId, AccFrom, AccTo)
                        OBJRPTCDNR.GSTCDNR_EXCEL(CmpId, YearId, AccFrom, AccTo, CMBREGISTER.Text.Trim)
                        OBJRPTCDNUR.GSTCDNUR_EXCEL(CmpId, YearId, AccFrom, AccTo, CMBREGISTER.Text.Trim)
                        OBJRPTHSNB2B.GSTHSNB2B_EXCEL(CmpId, YearId, AccFrom, AccTo)
                        OBJRPTHSNB2C.GSTHSNB2C_EXCEL(CmpId, YearId, AccFrom, AccTo, INVOICESCREENTYPE, ClientName, CMBREGISTER.Text.Trim)
                        OBJRPTDOCS.GSTDOCS_EXCEL(CmpId, YearId, AccFrom, AccTo)
                    End If

                    'MAIL EXCEL AS ATTACHMENTS

                    Dim objmail As New SendMail
                    objmail.ALATTACHMENT.Add(System.AppDomain.CurrentDomain.BaseDirectory & "B2B.xlsx")
                    objmail.ALATTACHMENT.Add(System.AppDomain.CurrentDomain.BaseDirectory & "B2CL.xlsx")
                    objmail.ALATTACHMENT.Add(System.AppDomain.CurrentDomain.BaseDirectory & "B2CS.xlsx")
                    objmail.ALATTACHMENT.Add(System.AppDomain.CurrentDomain.BaseDirectory & "HSNB2B.xlsx")
                    objmail.ALATTACHMENT.Add(System.AppDomain.CurrentDomain.BaseDirectory & "HSNB2C.xlsx")
                    objmail.ALATTACHMENT.Add(System.AppDomain.CurrentDomain.BaseDirectory & "CDNR.xlsx")
                    objmail.ALATTACHMENT.Add(System.AppDomain.CurrentDomain.BaseDirectory & "CDNUR.xlsx")
                    objmail.ALATTACHMENT.Add(System.AppDomain.CurrentDomain.BaseDirectory & "DOCS.xlsx")
                    objmail.subject = "GSTR1 Details"
                    objmail.Show()
                    objmail.BringToFront()
                    Windows.Forms.Cursor.Current = Cursors.Arrow

                    Exit Sub
                End If

            ElseIf RBGSTPURCHASEEXCEL.Checked = True Then

                If MsgBox("Wish To Mail Directly?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Dim OBJRPT As New clsReportDesigner("GST Purchase Details", System.AppDomain.CurrentDomain.BaseDirectory & "GST Purchase Details.xlsx", 2)
                    OBJRPT.CLIENTNAME = ClientName
                    If chkdate.CheckState = CheckState.Checked Then
                        OBJRPT.GSTPURCHASEDETAILS_EXCEL(CmpId, YearId, dtfrom.Value.Date, dtto.Value.Date)
                    Else
                        OBJRPT.GSTPURCHASEDETAILS_EXCEL(CmpId, YearId, AccFrom, AccTo)
                    End If
                    Exit Sub
                Else
                    Dim OBJRPT As New clsReportDesigner("GST Purchase Details", System.AppDomain.CurrentDomain.BaseDirectory & "GST Purchase Details.xlsx", 0)
                    OBJRPT.CLIENTNAME = ClientName
                    If chkdate.CheckState = CheckState.Checked Then
                        OBJRPT.GSTPURCHASEDETAILS_EXCEL(CmpId, YearId, dtfrom.Value.Date, dtto.Value.Date)
                    Else
                        OBJRPT.GSTPURCHASEDETAILS_EXCEL(CmpId, YearId, AccFrom, AccTo)
                    End If

                    'MAIL EXCEL AS ATTACHMENTS
                    Dim TEMPATTACHMENT As String = System.AppDomain.CurrentDomain.BaseDirectory & "GST Purchase Details.xlsx"
                    Dim objmail As New SendMail
                    objmail.attachment = TEMPATTACHMENT
                    objmail.subject = "GST Tax Symmary"
                    objmail.Show()
                    objmail.BringToFront()
                    Windows.Forms.Cursor.Current = Cursors.Arrow

                    Exit Sub
                End If

            ElseIf RBGSTPURHSNSUMM.Checked = True Then

                If MsgBox("Wish To Mail Directly?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Dim OBJRPT As New clsReportDesigner("GST Purchase HSN Summary", System.AppDomain.CurrentDomain.BaseDirectory & "GST Purchase HSN Summary.xlsx", 2)
                    If chkdate.CheckState = CheckState.Checked Then
                        OBJRPT.GSTPURHSNSUMM_EXCEL(CmpId, YearId, dtfrom.Value.Date, dtto.Value.Date, INVOICESCREENTYPE, CMBREGISTER.Text.Trim)
                    Else
                        OBJRPT.GSTPURHSNSUMM_EXCEL(CmpId, YearId, AccFrom, AccTo, INVOICESCREENTYPE, CMBREGISTER.Text.Trim)
                    End If
                    Exit Sub
                Else
                    Dim OBJRPT As New clsReportDesigner("GST Purchase HSN Summary", System.AppDomain.CurrentDomain.BaseDirectory & "GST Purchase HSN Summary.xlsx", 0)
                    If chkdate.CheckState = CheckState.Checked Then
                        OBJRPT.GSTPURHSNSUMM_EXCEL(CmpId, YearId, dtfrom.Value.Date, dtto.Value.Date, INVOICESCREENTYPE, CMBREGISTER.Text.Trim)
                    Else
                        OBJRPT.GSTPURHSNSUMM_EXCEL(CmpId, YearId, AccFrom, AccTo, INVOICESCREENTYPE, CMBREGISTER.Text.Trim)
                    End If

                    'MAIL EXCEL AS ATTACHMENTS
                    Dim TEMPATTACHMENT As String = System.AppDomain.CurrentDomain.BaseDirectory & "GST Purchase HSN Summary.xlsx"
                    Dim objmail As New SendMail
                    objmail.attachment = TEMPATTACHMENT
                    objmail.Show()
                    objmail.BringToFront()
                    Windows.Forms.Cursor.Current = Cursors.Arrow

                    Exit Sub
                End If

            ElseIf RBGSTHSNDETAILS.Checked = True Then

                Dim OBJHSN As New HSNWiseDetails
                OBJHSN.FRMSTRING = "SALE"
                If chkdate.CheckState = CheckState.Checked Then OBJHSN.WHERECLAUSE = OBJHSN.WHERECLAUSE & " And HSNSUMMARY.Date >='" & Format(dtfrom.Value.Date, "MM/dd/yyyy") & "' AND HSNSUMMARY.DATE <= '" & Format(dtto.Value.Date, "MM/dd/yyyy") & "'"
                OBJHSN.MdiParent = MDIMain
                OBJHSN.Show()

            ElseIf RBGSTHSNPURDETAILS.Checked = True Then

                Dim OBJHSN As New HSNWiseDetails
                OBJHSN.FRMSTRING = "PURCHASE"
                If chkdate.CheckState = CheckState.Checked Then OBJHSN.WHERECLAUSE = OBJHSN.WHERECLAUSE & " AND HSNPURSUMMARY.DATE >='" & Format(dtfrom.Value.Date, "MM/dd/yyyy") & "' AND HSNPURSUMMARY.DATE <= '" & Format(dtto.Value.Date, "MM/dd/yyyy") & "'"
                OBJHSN.MdiParent = MDIMain
                OBJHSN.Show()

            ElseIf RBGSTOUTWAREDSERIES.Checked = True Then

                Dim OBJGST As New GSTOutwardSeriesReport
                If chkdate.CheckState = CheckState.Checked Then OBJGST.WHERECLAUSE = OBJGST.WHERECLAUSE & " And OUTWARDSERIESDETAILS.Date >='" & Format(dtfrom.Value.Date, "MM/dd/yyyy") & "' AND OUTWARDSERIESDETAILS.DATE <= '" & Format(dtto.Value.Date, "MM/dd/yyyy") & "'"
                OBJGST.MdiParent = MDIMain
                OBJGST.Show()

            ElseIf RBGSTSALEHSNSUMM.Checked = True Then

                If MsgBox("Wish To Mail Directly?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Dim OBJRPT As New clsReportDesigner("GST Sale HSN Summary", System.AppDomain.CurrentDomain.BaseDirectory & "GST Sale HSN Summary.xlsx", 2)
                    If chkdate.CheckState = CheckState.Checked Then
                        OBJRPT.GSTSALEHSNSUMM_EXCEL(CmpId, YearId, dtfrom.Value.Date, dtto.Value.Date, INVOICESCREENTYPE, CMBREGISTER.Text.Trim)
                    Else
                        OBJRPT.GSTSALEHSNSUMM_EXCEL(CmpId, YearId, AccFrom, AccTo, INVOICESCREENTYPE, CMBREGISTER.Text.Trim)
                    End If
                    Exit Sub
                Else
                    Dim OBJRPT As New clsReportDesigner("GST Sale HSN Summary", System.AppDomain.CurrentDomain.BaseDirectory & "GST Sale HSN Summary.xlsx", 0)
                    If chkdate.CheckState = CheckState.Checked Then
                        OBJRPT.GSTSALEHSNSUMM_EXCEL(CmpId, YearId, dtfrom.Value.Date, dtto.Value.Date, INVOICESCREENTYPE, CMBREGISTER.Text.Trim)
                    Else
                        OBJRPT.GSTSALEHSNSUMM_EXCEL(CmpId, YearId, AccFrom, AccTo, INVOICESCREENTYPE, CMBREGISTER.Text.Trim)
                    End If

                    'MAIL EXCEL AS ATTACHMENTS
                    Dim TEMPATTACHMENT As String = System.AppDomain.CurrentDomain.BaseDirectory & "GST Sale HSN Summary.xlsx"
                    Dim objmail As New SendMail
                    objmail.attachment = TEMPATTACHMENT
                    objmail.Show()
                    objmail.BringToFront()
                    Windows.Forms.Cursor.Current = Cursors.Arrow

                    Exit Sub
                End If

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GSTTaxFilter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            fillregister(CMBREGISTER, " AND (REGISTER_TYPE ='SALE' OR REGISTER_TYPE = 'PURCHASE')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


End Class