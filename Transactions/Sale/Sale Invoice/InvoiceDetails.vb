
Imports BL
Imports DevExpress.XtraGrid.Views.Grid

Public Class InvoiceDetails

    Dim SALEREGID As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim DTMAIL As New DataTable
    Dim DTWHATSAPP As New DataTable

    Private Sub InvoiceDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.N And e.Alt = True Then
                showform(False, 0)
            ElseIf e.KeyCode = Keys.F2 Then
                TXTFROM.Focus()
            ElseIf e.KeyCode = Keys.P And e.Alt = True Then
                PrintToolStripButton_Click(sender, e)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid()
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim objclsCMST As New ClsCommonMaster
            'Dim dt As DataTable = objclsCMST.search(" CAST(0 AS BIT) AS CHK, INVOICEMASTER.INVOICE_NO AS SRNO, INVOIC963.*EMASTER.INVOICE_DATE AS DATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(LEDGERS.ACC_GSTIN,'') AS GSTIN, ISNULL(INVOICE_CHALLANNO,'') AS CHALLANNO, ISNULL(INVOICE_EWAYBILLNO,'') AS EWAYBILLNO,  ISNULL(INVOICEMASTER.INVOICE_TOTALPCS, 0) AS PCS, ISNULL(INVOICEMASTER.INVOICE_TOTALMTRS, 0) AS MTRS, ISNULL(INVOICEMASTER.INVOICE_LRNO,'') AS LRNO, ISNULL(INVOICEMASTER.INVOICE_GRNO, '') AS GRNO, ISNULL(INVOICEMASTER.INVOICE_GRPCS, 0) AS GRPCS, ISNULL(INVOICEMASTER.INVOICE_GRMTRS, 0) AS GRMTRS, ISNULL(INVOICEMASTER.INVOICE_GRSHORTAGE, 0) AS SHORTAGE, CASE WHEN ISNULL(INVOICEMASTER.INVOICE_SCREENTYPE, 'LINE GST') = 'LINE GST' THEN ISNULL(INVOICEMASTER.INVOICE_TOTALTAXABLEAMT, 0) ELSE ISNULL(INVOICEMASTER.INVOICE_SUBTOTAL, 0) END AS TAXABLEAMT, ISNULL(INVOICEMASTER.INVOICE_TOTALCGSTAMT, 0) AS CGSTAMT, ISNULL(INVOICEMASTER.INVOICE_TOTALSGSTAMT, 0) AS SGSTAMT, ISNULL(INVOICEMASTER.INVOICE_TOTALIGSTAMT, 0) AS IGSTAMT, ISNULL(INVOICEMASTER.INVOICE_GRANDTOTAL, 0) AS AMOUNT, ISNULL(INVOICEMASTER.INVOICE_REMARKS, '') AS REMARKS, ISNULL(INVOICEMASTER.INVOICE_DISPUTE, 0) AS DISPUTED, ISNULL(INVOICEMASTER.INVOICE_CHECKED, 0) AS CHECKED, ISNULL(INVOICEMASTER.INVOICE_TOTALWITHGST, 0) AS TOTALWITHGST, ISNULL(INVOICEMASTER.INVOICE_APPLYTCS, 0) AS APPLYTCS, ISNULL(INVOICEMASTER.INVOICE_TCSPER, 0) AS TCSPER, ISNULL(INVOICEMASTER.INVOICE_TCSAMT, 0) AS TCSAMT, ISNULL(AGENTLEDGERS.ACC_CMPNAME,'') AS AGENTNAME, ISNULL(INVOICEMASTER.INVOICE_FOLD,'') AS FOLD, ISNULL(INVOICEMASTER.INVOICE_ORDERDISC,0) AS DISCOUNT, ISNULL(INVOICEMASTER.INVOICE_RETURN,0) AS RETURNAMT, ISNULL(INVOICEMASTER.INVOICE_AMTREC,0) AS AMTREC, ISNULL(INVOICEMASTER.INVOICE_BALANCE,0) AS BALANCE ", " ", " INVOICEMASTER INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id INNER JOIN REGISTERMASTER ON INVOICEMASTER.INVOICE_REGISTERID = REGISTERMASTER.register_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON INVOICEMASTER.INVOICE_AGENTID = AGENTLEDGERS.ACC_ID", " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND (INVOICEMASTER.INVOICE_YEARID = '" & YearId & "')  ORDER BY INVOICEMASTER.INVOICE_NO")
            Dim dt As DataTable = objclsCMST.search(" CAST(0 AS BIT) AS CHK, INVOICEMASTER.INVOICE_NO AS SRNO, INVOICEMASTER.INVOICE_DATE AS DATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(LEDGERS.ACC_GSTIN, '') AS GSTIN, ISNULL(INVOICEMASTER.INVOICE_CHALLANNO, '') AS CHALLANNO, ISNULL(INVOICEMASTER.INVOICE_EWAYBILLNO, '') AS EWAYBILLNO, ISNULL(INVOICEMASTER.INVOICE_TOTALPCS, 0) AS PCS, ISNULL(INVOICEMASTER.INVOICE_TOTALMTRS, 0) AS MTRS, ISNULL(INVOICEMASTER.INVOICE_LRNO, '') AS LRNO, ISNULL(INVOICEMASTER.INVOICE_GRNO, '') AS GRNO, ISNULL(INVOICEMASTER.INVOICE_GRPCS, 0) AS GRPCS, ISNULL(INVOICEMASTER.INVOICE_GRMTRS, 0) AS GRMTRS, ISNULL(INVOICEMASTER.INVOICE_GRSHORTAGE, 0) AS SHORTAGE, CASE WHEN ISNULL(INVOICEMASTER.INVOICE_SCREENTYPE, 'LINE GST') = 'LINE GST' THEN ISNULL(INVOICEMASTER.INVOICE_TOTALTAXABLEAMT, 0) ELSE ISNULL(INVOICEMASTER.INVOICE_SUBTOTAL, 0) END AS TAXABLEAMT, ISNULL(INVOICEMASTER.INVOICE_TOTALCGSTAMT, 0) AS CGSTAMT, ISNULL(INVOICEMASTER.INVOICE_TOTALSGSTAMT, 0) AS SGSTAMT, ISNULL(INVOICEMASTER.INVOICE_TOTALIGSTAMT, 0) AS IGSTAMT, ISNULL(INVOICEMASTER.INVOICE_GRANDTOTAL, 0) AS AMOUNT, ISNULL(INVOICEMASTER.INVOICE_REMARKS, '') AS REMARKS, ISNULL(INVOICEMASTER.INVOICE_DISPUTE, 0) AS DISPUTED, ISNULL(INVOICEMASTER.INVOICE_CHECKED, 0) AS CHECKED, ISNULL(INVOICEMASTER.INVOICE_TOTALWITHGST, 0) AS TOTALWITHGST, ISNULL(INVOICEMASTER.INVOICE_APPLYTCS, 0) AS APPLYTCS, ISNULL(INVOICEMASTER.INVOICE_TCSPER, 0) AS TCSPER, ISNULL(INVOICEMASTER.INVOICE_TCSAMT, 0) AS TCSAMT, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS AGENTNAME, ISNULL(INVOICEMASTER.INVOICE_FOLD, '') AS FOLD, ISNULL(INVOICEMASTER.INVOICE_ORDERDISC, 0) AS DISCOUNT, ISNULL(INVOICEMASTER.INVOICE_RETURN, 0) AS RETURNAMT, ISNULL(INVOICEMASTER.INVOICE_AMTREC, 0) AS AMTREC, ISNULL(INVOICEMASTER.INVOICE_BALANCE, 0) AS BALANCE, ISNULL(INVOICEMASTER.INVOICE_INITIALS, '') AS PRINTINITIALS, ISNULL(LEDGERS.ACC_MOBILE, '') AS PARTYWHATSAPP, ISNULL(AGENTLEDGERS.ACC_MOBILE, '') AS AGENTWHATSAPP, ISNULL(INVOICEMASTER.INVOICE_GRANDTOTAL, 0) AS GRANDTOTAL ", " ", " INVOICEMASTER INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id INNER JOIN REGISTERMASTER ON INVOICEMASTER.INVOICE_REGISTERID = REGISTERMASTER.register_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON INVOICEMASTER.INVOICE_AGENTID = AGENTLEDGERS.ACC_ID", " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND (INVOICEMASTER.INVOICE_YEARID = '" & YearId & "')  ORDER BY INVOICEMASTER.INVOICE_NO")

            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal billno As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim OBJBILL As New InvoiceMaster
                OBJBILL.MdiParent = MDIMain
                OBJBILL.edit = editval
                OBJBILL.tempinvoiceno = billno
                OBJBILL.TEMPREGNAME = cmbregister.Text.Trim
                OBJBILL.Show()
                '  Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Try
            showform(False, 0)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbregister_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbregister.Enter
        Try
            If cmbregister.Text.Trim = "" Then fillregister(cmbregister, " and register_type = 'SALE'")

            Dim clscommon As New ClsCommon
            Dim dt As DataTable
            dt = clscommon.search(" register_name,register_id", "", " RegisterMaster ", " and register_default = 'True' and register_type = 'SALE' and register_cmpid = " & CmpId & " and register_locationid = " & Locationid & " and register_yearid = " & YearId)
            If dt.Rows.Count > 0 Then
                cmbregister.Text = dt.Rows(0).Item(0).ToString
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbregister_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbregister.Validating
        Try
            If cmbregister.Text.Trim.Length > 0 Then
                cmbregister.Text = UCase(cmbregister.Text)
                Dim clscommon As New ClsCommon
                Dim dt As DataTable
                dt = clscommon.search(" register_id ", "", " RegisterMaster ", " and register_name ='" & cmbregister.Text.Trim & "' and register_type = 'SALE' and register_cmpid = " & CmpId & " and register_locationid = " & Locationid & " and register_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    SALEREGID = dt.Rows(0).Item(0)
                    fillgrid()
                Else
                    MsgBox("Register Not Present, Add New from Master Module")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        Me.Close()
    End Sub

    Private Sub CMDOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridINVOICE_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            If Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Then Exit Sub
            If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                MsgBox("Enter Proper Invoice Nos", MsgBoxStyle.Critical)
                Exit Sub
            End If
            If MsgBox("Wish to Print Invoice from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings
            serverprop(Val(TXTFROM.Text.Trim), Val(TXTTO.Text.Trim), cmbregister.Text.Trim, "INVOICE", Val(TXTCOPIES.Text.Trim), PRINTDIALOG)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub InvoiceDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'SALE INVOICE'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            DTMAIL.Columns.Add("INVNO")
            DTMAIL.Columns.Add("REGID")
            DTMAIL.Columns.Add("REGNAME")
            DTMAIL.Columns.Add("PRINTINITIALS")
            DTMAIL.Columns.Add("DATE")
            DTMAIL.Columns.Add("NAME")
            DTMAIL.Columns.Add("PARTYEMAILID")
            DTMAIL.Columns.Add("AGENTNAME")
            DTMAIL.Columns.Add("AGENTEMAILID")
            DTMAIL.Columns.Add("GRANDTOTAL")
            DTMAIL.Columns.Add("SUBJECT")
            DTMAIL.Columns.Add("ATTACHMENT")
            DTMAIL.Columns.Add("FILENAME")

            DTWHATSAPP.Columns.Add("INVNO")
            DTWHATSAPP.Columns.Add("REGID")
            DTWHATSAPP.Columns.Add("REGNAME")
            DTWHATSAPP.Columns.Add("PRINTINITIALS")
            DTWHATSAPP.Columns.Add("DATE")
            DTWHATSAPP.Columns.Add("NAME")
            DTWHATSAPP.Columns.Add("PARTYWHATSAPP")
            DTWHATSAPP.Columns.Add("AGENTNAME")
            DTWHATSAPP.Columns.Add("AGENTWHATSAPP")
            DTWHATSAPP.Columns.Add("GRANDTOTAL")
            DTWHATSAPP.Columns.Add("SUBJECT")
            DTWHATSAPP.Columns.Add("ATTACHMENT")
            DTWHATSAPP.Columns.Add("FILENAME")

            fillregister(cmbregister, " and register_name ='" & cmbregister.Text.Trim & "' and register_type = 'SALE' and register_cmpid = " & CmpId & " and register_locationid = " & Locationid & " and register_yearid = " & YearId)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gridbill.RowStyle
        Try
            If e.RowHandle >= 0 Then
                Dim View As GridView = sender
                If View.GetRowCellDisplayText(e.RowHandle, View.Columns("CHECKED")) = "Checked" Then
                    e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Bold)
                    e.Appearance.BackColor = Color.LightGreen
                ElseIf View.GetRowCellDisplayText(e.RowHandle, View.Columns("DISPUTED")) = "Checked" Then
                    e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Bold)
                    e.Appearance.BackColor = Color.Yellow
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLGRIDDETAILS_Click(sender As Object, e As EventArgs) Handles TOOLGRIDDETAILS.Click
        Try
            Dim OBJINV As New InvoiceGridDetails
            OBJINV.MdiParent = MDIMain
            OBJINV.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            Dim DT As New DataTable
            Dim OBJCMN As New ClsCommon
            If (Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Or Val(TXTCOPIES.Text.Trim) = 0) AndAlso gridbill.SelectedRowsCount = 0 Then Exit Sub
            'IF WE HAVE SELECTED FROM AND TO THEN WORK WITH THE CURRENT CODE ELSE GO FOR SELECTED ENTRIES CODE
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper Invoice Nos", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    If MsgBox("Wish to Whatsapp Invoice from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    SERVERPROPDIRECT(False, True)
                End If
            Else
                If MsgBox("Wish to Whatsapp Selected Invoice ?", MsgBoxStyle.YesNo) = vbYes Then
                    CMDOK.Focus()
                    SERVERPROPSELECTED(False, True)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub SERVERPROPDIRECT(Optional ByVal INVOICEMAIL As Boolean = False, Optional ByVal WHATSAPP As Boolean = False)
        Try
            Dim ALATTACHMENT As New ArrayList
            Dim FILENAME As New ArrayList

            If INVOICEMAIL = False And WHATSAPP = False Then
                If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings Else Exit Sub
            End If
            For I As Integer = Val(TXTFROM.Text.Trim) To Val(TXTTO.Text.Trim)
                Dim OBJINVOICE As New SaleDesign
                OBJINVOICE.MdiParent = MDIMain
                OBJINVOICE.DIRECTPRINT = True
                OBJINVOICE.FRMSTRING = "INVOICE"
                OBJINVOICE.DIRECTMAIL = INVOICEMAIL
                OBJINVOICE.DIRECTWHATSAPP = WHATSAPP

                OBJINVOICE.INVOICECOPYNAME = TOOLCMBINVCOPY.Text.Trim
                If (ClientName = "SOFTAS" Or ClientName = "MANS") And TOOLCMBINVCOPY.Text = "OFFICE COPY" Then OBJINVOICE.INVOICECOPYNAME = "AGENT COPY"
                'If (ClientName = "RMANILAL" Or ClientName = "YUMILONE" Or ClientName = "REVAANT" Or ClientName = "TARUN" Or ClientName = "SHANTI" Or ClientName = "KUNAL" Or ClientName = "VALIANT") And TOOLCMBINVCOPY.Text = "DUPLICATE COPY" Then OBJINVOICE.INVOICECOPYNAME = "AGENT COPY"
                'If ClientName = "ALENCOT" And TOOLCMBINVCOPY.Text = "DUPLICATE COPY" Then OBJINVOICE.INVOICECOPYNAME = "REVISED COPY"
                'If ClientName = "GELATO" And TOOLCMBINVCOPY.Text = "TRANSPORT COPY" Then OBJINVOICE.INVOICECOPYNAME = "DUPLICATE FOR TRANSPORT"

                If TOOLCMBINVCOPY.Text = "TRANSPORT COPY" Then OBJINVOICE.INVOICETRANS = True
                If TOOLCMBINVCOPY.Text = "RETAIL COPY (A5)" Then OBJINVOICE.INVOICERETAIL = True
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("ISNULL(STATE_REMARK,'') AS STATECODE", "", " INVOICEMASTER INNER JOIN LEDGERS ON INVOICE_LEDGERID = LEDGERS.ACC_ID LEFT OUTER JOIN STATEMASTER ON LEDGERS.ACC_STATEID = STATE_ID INNER JOIN REGISTERMASTER ON REGISTER_ID = INVOICEMASTER.INVOICE_REGISTERID ", " AND INVOICEMASTER.INVOICE_NO = " & Val(I) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICEMASTER.INVOICE_YEARID = " & YearId)
                If DT.Rows.Count > 0 AndAlso DT.Rows(0).Item("STATECODE") <> CMPSTATECODE Then OBJINVOICE.IGSTFORMAT = True
                OBJINVOICE.registername = cmbregister.Text.Trim
                OBJINVOICE.PRINTSETTING = PRINTDIALOG
                OBJINVOICE.INVNO = Val(I)
                OBJINVOICE.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
                OBJINVOICE.BLANKPAPER = CHKBLANKPAPER.Checked
                OBJINVOICE.Show()
                OBJINVOICE.Close()
                ALATTACHMENT.Add(Application.StartupPath & "\INVOICE_" & I & ".pdf")
                FILENAME.Add("INVOICE_" & I & ".pdf")
                DT = OBJCMN.Execute_Any_String("UPDATE INVOICEMASTER SET INVOICE_SENDWHATSAPP = 1 FROM InvoiceMaster INNER JOIN REGISTERMASTER On INVOICEMASTER.INVOICE_REGISTERID = REGISTERMASTER.register_id WHERE INVOICE_NO = " & I & " AND REGISTER_NAME '" & cmbregister.Text.Trim & "'  AND INVOICE_YEARID = " & YearId, "", "")
            Next

            If INVOICEMAIL Then
                Dim OBJMAIL As New SendMail
                OBJMAIL.ALATTACHMENT = ALATTACHMENT
                OBJMAIL.subject = "Invoice"
                OBJMAIL.ShowDialog()
            End If



            If WHATSAPP = True Then
                If DTWHATSAPP.Rows.Count = 0 Then Exit Sub
                Dim OBJWHATSAPP As New SendMultipleWhatsapp
                OBJWHATSAPP.PATH = ALATTACHMENT
                OBJWHATSAPP.FILENAME = FILENAME
                OBJWHATSAPP.DT = DTWHATSAPP
                OBJWHATSAPP.ShowDialog()
            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SERVERPROPSELECTED(Optional ByVal INVOICEMAIL As Boolean = False, Optional ByVal WHATSAPP As Boolean = False)
        Try

            Dim ALATTACHMENT As New ArrayList
            Dim FILENAME As New ArrayList
            DTMAIL.Rows.Clear()
            DTWHATSAPP.Rows.Clear()


            If INVOICEMAIL = False And WHATSAPP = False Then
                If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings Else Exit Sub
            End If
            'Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()
            For I As Integer = 0 To Val(gridbill.RowCount - 1)
                Dim ROW As DataRow = gridbill.GetDataRow(I)
                If ROW("CHK") = True Then
                    Dim OBJINVOICE As New SaleDesign
                    OBJINVOICE.MdiParent = MDIMain
                    OBJINVOICE.DIRECTPRINT = True
                    OBJINVOICE.FRMSTRING = "INVOICE"
                    OBJINVOICE.DIRECTMAIL = INVOICEMAIL
                    OBJINVOICE.DIRECTWHATSAPP = WHATSAPP
                    If TOOLCMBINVCOPY.Text = "TRANSPORT COPY" Then OBJINVOICE.INVOICETRANS = True
                    If TOOLCMBINVCOPY.Text = "RETAIL COPY (A5)" Then OBJINVOICE.INVOICERETAIL = True

                    OBJINVOICE.INVOICECOPYNAME = TOOLCMBINVCOPY.Text.Trim
                    If (ClientName = "SOFTAS" Or ClientName = "MANS") And TOOLCMBINVCOPY.Text = "OFFICE COPY" Then OBJINVOICE.INVOICECOPYNAME = "AGENT COPY"
                    If (ClientName = "RMANILAL" Or ClientName = "YUMILONE" Or ClientName = "REVAANT" Or ClientName = "TARUN" Or ClientName = "SHANTI" Or ClientName = "KUNAL" Or ClientName = "VALIANT") And TOOLCMBINVCOPY.Text = "DUPLICATE COPY" Then OBJINVOICE.INVOICECOPYNAME = "AGENT COPY"
                    If ClientName = "ALENCOT" And TOOLCMBINVCOPY.Text = "DUPLICATE COPY" Then OBJINVOICE.INVOICECOPYNAME = "REVISED COPY"
                    If ClientName = "GELATO" And TOOLCMBINVCOPY.Text = "TRANSPORT COPY" Then OBJINVOICE.INVOICECOPYNAME = "DUPLICATE FOR TRANSPORT"

                    OBJINVOICE.PARTYNAME = ROW("NAME")
                    OBJINVOICE.AGENTNAME = ROW("AGENTNAME")
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.search("ISNULL(STATE_REMARK,'') AS STATECODE, ISNULL(REGISTERMASTER.REGISTER_ID,0) AS REGID", "", " INVOICEMASTER INNER JOIN LEDGERS ON INVOICE_LEDGERID = LEDGERS.ACC_ID LEFT OUTER JOIN STATEMASTER ON LEDGERS.ACC_STATEID = STATE_ID INNER JOIN REGISTERMASTER ON REGISTER_ID = INVOICEMASTER.INVOICE_REGISTERID ", " AND INVOICEMASTER.INVOICE_NO = " & Val(ROW("SRNO")) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICEMASTER.INVOICE_YEARID = " & YearId)
                    If DT.Rows.Count > 0 AndAlso DT.Rows(0).Item("STATECODE") <> CMPSTATECODE Then OBJINVOICE.IGSTFORMAT = True
                    OBJINVOICE.registername = cmbregister.Text.Trim
                    OBJINVOICE.PRINTSETTING = PRINTDIALOG
                    OBJINVOICE.INVNO = Val(ROW("SRNO"))
                    OBJINVOICE.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
                    OBJINVOICE.BLANKPAPER = CHKBLANKPAPER.Checked
                    OBJINVOICE.Show()
                    OBJINVOICE.Close()
                    ALATTACHMENT.Add(Application.StartupPath & "\" & ROW("NAME") & "INVOICE_" & Val(ROW("SRNO")) & ".pdf")
                    FILENAME.Add(ROW("NAME") & "INVOICE_" & Val(ROW("SRNO")) & ".pdf")

                    'ADDINT IN DTEMAIL
                    'DTMAIL.Rows.Add(ROW("SRNO"), DT.Rows(0).Item("REGID"), cmbregister.Text.Trim, ROW("PRINTINITIALS"), ROW("DATE"), ROW("NAME"), ROW("PARTYMAIL"), ROW("AGENTNAME"), ROW("AGENTMAIL"), Val(ROW("GRANDTOTAL")), UCase(CmpName) & " - Invoice No. " & ROW("PRINTINITIALS") & " Dated " & ROW("DATE"), Application.StartupPath & "\" & ROW("NAME") & "INVOICE_" & Val(ROW("SRNO")) & ".pdf", ROW("NAME") & "INVOICE_" & Val(ROW("SRNO")) & ".pdf")

                    'ADDING IN DTWHATSAPP
                   If ClientName = "MAHAVIRPOLYCOT" Then ROW("AGENTWHATSAPP") = ""
                    DTWHATSAPP.Rows.Add(ROW("SRNO"), DT.Rows(0).Item("REGID"), cmbregister.Text.Trim, ROW("PRINTINITIALS"), ROW("DATE"), ROW("NAME"), ROW("PARTYWHATSAPP"), ROW("AGENTNAME"), ROW("AGENTWHATSAPP"), Val(ROW("GRANDTOTAL")), UCase(CmpName) & " - Invoice No. " & ROW("PRINTINITIALS") & " Dated " & ROW("DATE"), Application.StartupPath & "\" & ROW("NAME") & "INVOICE_" & Val(ROW("SRNO")) & ".pdf", ROW("NAME") & "INVOICE_" & Val(ROW("SRNO")) & ".pdf")

                    DT = OBJCMN.Execute_Any_String("UPDATE INVOICEMASTER SET INVOICE_SENDWHATSAPP = 1 FROM InvoiceMaster INNER JOIN REGISTERMASTER On INVOICEMASTER.INVOICE_REGISTERID = REGISTERMASTER.register_id WHERE INVOICE_NO = " & Val(ROW("SRNO")) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "'  AND INVOICE_YEARID = " & YearId, "", "")

                End If
            Next

            If INVOICEMAIL Then
                If DTMAIL.Rows.Count = 0 Then Exit Sub
                Dim OBJEMAIL As New SendMultipleMail
                OBJEMAIL.FORMTYPE = "INVOICE"
                OBJEMAIL.DT = DTMAIL
                OBJEMAIL.ShowDialog()
                Exit Sub
            End If

            'If INVOICEMAIL Then
            '    Dim OBJMAIL As New SendMail
            '    OBJMAIL.ALATTACHMENT = ALATTACHMENT
            '    OBJMAIL.subject = "Invoice"
            '    OBJMAIL.ShowDialog()
            'End If

            If WHATSAPP = True Then
                If DTWHATSAPP.Rows.Count = 0 Then Exit Sub
                Dim OBJWHATSAPP As New SendMultipleWhatsapp
                OBJWHATSAPP.PATH = ALATTACHMENT
                OBJWHATSAPP.FILENAME = FILENAME
                OBJWHATSAPP.DT = DTWHATSAPP
                OBJWHATSAPP.ShowDialog()
            End If


            'FOR MERGING MULTIPLE PDF
            'Dim pdfReaderList As List(Of PdfReader) = New List(Of PdfReader)()
            'For i As Integer = 0 To ALATTACHMENT.Count - 1
            '    Dim pdfReader As PdfReader = New PdfReader(ALATTACHMENT(i).ToString)
            '    pdfReaderList.Add(pdfReader)
            'Next

            'Dim document As Document = New Document(PageSize.A4, 0, 0, 0, 0)
            'Dim writer As PdfWriter = PdfWriter.GetInstance(document, New FileStream("D:  \OutPut.pdf", FileMode.Create))
            'document.Open()
            'For Each reader As PdfReader In pdfReaderList
            '    For i As Integer = 1 To reader.NumberOfPages
            '        Dim page As PdfImportedPage = writer.GetImportedPage(reader, i)
            '        document.Add(iTextSharp.text.Image.GetInstance(page))
            '    Next
            'Next
            'document.Close()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTFROM_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTFROM.Validated
        If TXTFROM.Text.Trim <> "" Then TXTTO.Focus()
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Try
            Dim PATH As String = "D:\Sale Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                proc.Kill()
            Next
            opti.SheetName = "Sale Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Sale Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCOPIES_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTCOPIES.Validating
        If Val(TXTCOPIES.Text.Trim) <= 0 Then TXTCOPIES.Text = 1
    End Sub
End Class