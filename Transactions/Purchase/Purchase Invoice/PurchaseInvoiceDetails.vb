
Imports BL
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid

Public Class PurchaseInvoiceDetails

    Dim PURCHASEREGID As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim DTMAIL As New DataTable
    Dim DTWHATSAPP As New DataTable

    Private Sub PURCHASEMASTERDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Or (e.KeyCode = Keys.X And e.Alt = True) Then
                Me.Close()
            ElseIf e.KeyCode = Keys.N And e.Alt = True Then
                showform(False, 0, "")
            ElseIf e.KeyCode = Keys.O And e.Alt = True Then
                CMDOK_Click(sender, e)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid(ByVal tepmcondition)
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search(" CAST(0 AS BIT) AS CHK, PURCHASEMASTER.BILL_NO AS SRNO, PURCHASEMASTER.BILL_PARTYBILLDATE AS Date, LEDGERS.Acc_cmpname AS PARTYNAME, ISNULL(LEDGERS.ACC_GSTIN, '') AS GSTIN, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS BROKERNAME, ISNULL(PURCHASEMASTER.BILL_PARTYBILLNO, '') AS PARTYBILLNO, ISNULL(PURCHASEMASTER.BILL_RCM, 0) AS RCM, ISNULL(PURCHASEMASTER.BILL_PENREPORT, 0) AS PENDINGREPORT, ISNULL(PURCHASEMASTER.BILL_EWAYBILLNO, '') AS EWAYBILLNO, ISNULL(MILLLEDGERS.Acc_cmpname, '') AS MILLNAME, ISNULL(PURCHASEMASTER.BILL_TOTALBAGS, 0) AS TOTALBAGS, ISNULL(PURCHASEMASTER.BILL_TOTALWT, 0) AS TOTALWT, ISNULL(PURCHASEMASTER.BILL_SUBTOTAL, 0) AS TAXABLEAMT, ISNULL(PURCHASEMASTER.BILL_CGSTAMT, 0) AS CGSTAMT, ISNULL(PURCHASEMASTER.BILL_SGSTAMT, 0) AS SGSTAMT, ISNULL(PURCHASEMASTER.BILL_IGSTAMT, 0) AS IGSTAMT, ISNULL(PURCHASEMASTER.BILL_CHECKED, 0) AS CHECKED, ISNULL(PURCHASEMASTER.BILL_DISPUTE, 0) AS DISPUTED, ISNULL(PURCHASEMASTER.BILL_REMARKS, '') AS REMARKS, ISNULL(PURCHASEMASTER.BILL_GRANDTOTAL, 0) AS GRANDTOTAL, ISNULL(PURCHASEMASTER.BILL_RETURN, 0) AS PURRETURN, ISNULL(PURCHASEMASTER.BILL_AMTPAID, 0) AS AMTPAID, ISNULL(PURCHASEMASTER.BILL_BALANCE, 0) AS BALANCE, PURCHASEMASTER.BILL_PURTYPE AS PURTYPE, PURCHASEMASTER.BILL_CHALLANNO AS GRNNO, ISNULL(PURCHASEMASTER.BILL_TOTALWITHGST, 0) AS TOTALWITHGST, ISNULL(PURCHASEMASTER.BILL_APPLYTCS, 0) AS APPLYTCS, ISNULL(PURCHASEMASTER.BILL_TCSPER, 0) AS TCSPER, ISNULL(PURCHASEMASTER.BILL_TCSAMT, 0) AS TCSAMT, ISNULL(LEDGERS.Acc_mobile, '') AS PARTYWHATSAPP, ISNULL(AGENTLEDGERS.Acc_mobile, '') AS AGENTWHATSAPP, ISNULL(PURCHASEMASTER.BILL_INITIALS, '') AS PRINTINITIALS  ", " ", "  PURCHASEMASTER INNER JOIN LEDGERS ON PURCHASEMASTER.BILL_LEDGERID = LEDGERS.Acc_id INNER JOIN REGISTERMASTER ON PURCHASEMASTER.BILL_REGISTERID = REGISTERMASTER.register_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON PURCHASEMASTER.BILL_AGENTID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS MILLLEDGERS ON PURCHASEMASTER.BILL_MILLID = MILLLEDGERS.Acc_id  ", tepmcondition)
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal billno As Integer, ByVal PURTYPE As String)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim OBJBILL As New PurchaseMaster
                OBJBILL.MdiParent = MDIMain
                OBJBILL.EDIT = editval
                OBJBILL.TEMPBILLNO = billno
                OBJBILL.PURTYPE = PURTYPE
                OBJBILL.SELECTEDREG = cmbregister.Text.Trim
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
            showform(False, 0, "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbregister_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbregister.Enter
        Try
            If cmbregister.Text.Trim = "" Then fillregister(cmbregister, " and register_type = 'PURCHASE'")

            Dim clscommon As New ClsCommon
            Dim dt As DataTable
            dt = clscommon.search(" register_name,register_id", "", " RegisterMaster ", " and register_default = 'True' and register_type = 'PURCHASE' and register_cmpid = " & CmpId & " and register_locationid = " & Locationid & " and register_yearid = " & YearId)
            If dt.Rows.Count > 0 Then
                cmbregister.Text = dt.Rows(0).Item(0).ToString
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbregister_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbregister.Validating
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'PURCHASE INVOICE'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            If cmbregister.Text.Trim.Length > 0 Then
                cmbregister.Text = UCase(cmbregister.Text)
                Dim clscommon As New ClsCommon
                Dim dt As DataTable
                dt = clscommon.search(" register_id ", "", " RegisterMaster ", " and register_name ='" & cmbregister.Text.Trim & "' and register_type = 'PURCHASE' and register_cmpid = " & CmpId & " and register_locationid = " & Locationid & " and register_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    PURCHASEREGID = dt.Rows(0).Item(0)
                    fillgrid(" AND PURCHASEMASTER.BILL_yearid = " & YearId & " AND PURCHASEMASTER.BILL_registerid = " & PURCHASEREGID & " order by dbo.PURCHASEMASTER.BILL_no ")
                Else
                    MsgBox("Register Not Present, Add New from Master Module")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub CMDOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"), gridbill.GetFocusedRowCellValue("PURTYPE"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"), gridbill.GetFocusedRowCellValue("PURTYPE"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub TOOLGRIDDETAILS_Click(sender As Object, e As EventArgs) Handles TOOLGRIDDETAILS.Click
        Try
            Dim OBJPUR As New PurchaseInvoiceGridDetails
            OBJPUR.MdiParent = MDIMain
            OBJPUR.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try

            Dim PATH As String = Application.StartupPath & "\Purchase Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True

            For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                proc.Kill()
            Next
            opti.SheetName = "Purchase Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Purchase Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PurchaseInvoiceDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'PURCHASE INVOICE'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            DTMAIL.Columns.Add("BILLNO")
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

            DTWHATSAPP.Columns.Add("BILLNO")
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

            fillregister(cmbregister, " and register_type = 'PURCHASE'")
            fillgrid(" AND PURCHASEMASTER.BILL_yearid = " & YearId & " AND REGISTERMASTER.REGISTER_NAME = '" & cmbregister.Text.Trim & "' order by dbo.PURCHASEMASTER.BILL_no ")
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
                    cmdok.Focus()
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
                Dim OBJINVOICE As New PurchaseInvoiceDesign
                OBJINVOICE.MdiParent = MDIMain
                OBJINVOICE.DIRECTPRINT = True
                OBJINVOICE.FRMSTRING = "PURBILL"
                OBJINVOICE.DIRECTMAIL = INVOICEMAIL
                OBJINVOICE.DIRECTWHATSAPP = WHATSAPP

                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("ISNULL(STATE_REMARK,'') AS STATECODE", "", " INVOICEMASTER INNER JOIN LEDGERS ON INVOICE_LEDGERID = LEDGERS.ACC_ID LEFT OUTER JOIN STATEMASTER ON LEDGERS.ACC_STATEID = STATE_ID INNER JOIN REGISTERMASTER ON REGISTER_ID = INVOICEMASTER.INVOICE_REGISTERID ", " AND INVOICEMASTER.INVOICE_NO = " & Val(I) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICEMASTER.INVOICE_YEARID = " & YearId)
                OBJINVOICE.registername = cmbregister.Text.Trim
                OBJINVOICE.PRINTSETTING = PRINTDIALOG
                OBJINVOICE.BILLNO = Val(I)
                OBJINVOICE.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
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
                    Dim OBJINVOICE As New PurchaseInvoiceDesign
                    OBJINVOICE.MdiParent = MDIMain
                    OBJINVOICE.DIRECTPRINT = True
                    OBJINVOICE.FRMSTRING = "PURBILL"
                    OBJINVOICE.DIRECTMAIL = INVOICEMAIL
                    OBJINVOICE.DIRECTWHATSAPP = WHATSAPP

                    OBJINVOICE.PARTYNAME = ROW("PARTYNAME")
                    OBJINVOICE.AGENTNAME = ROW("BROKERNAME")
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.search("ISNULL(REGISTERMASTER.REGISTER_ID,0) AS REGID", "", " PURCHASEMASTER INNER JOIN LEDGERS ON BILL_LEDGERID = LEDGERS.ACC_ID  INNER JOIN REGISTERMASTER ON REGISTER_ID = PURCHASEMASTER.BILL_REGISTERID ", " AND PURCHASEMASTER.BILL_NO = " & Val(ROW("SRNO")) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND PURCHASEMASTER.BILL_YEARID = " & YearId)
                    OBJINVOICE.registername = cmbregister.Text.Trim
                    OBJINVOICE.PRINTSETTING = PRINTDIALOG
                    OBJINVOICE.BILLNO = Val(ROW("SRNO"))
                    OBJINVOICE.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
                    OBJINVOICE.Show()
                    OBJINVOICE.Close()
                    ALATTACHMENT.Add(Application.StartupPath & "\" & ROW("PARTYNAME") & "_PURCHASEINVOICE_NO-" & Val(ROW("SRNO")) & ".pdf")
                    FILENAME.Add(ROW("PARTYNAME") & "_PURCHASEINVOICE_NO-" & Val(ROW("SRNO")) & ".pdf")

                    'ADDINT IN DTEMAIL
                    'DTMAIL.Rows.Add(ROW("SRNO"), DT.Rows(0).Item("REGID"), cmbregister.Text.Trim, ROW("PRINTINITIALS"), ROW("DATE"), ROW("NAME"), ROW("PARTYMAIL"), ROW("AGENTNAME"), ROW("AGENTMAIL"), Val(ROW("GRANDTOTAL")), UCase(CmpName) & " - Invoice No. " & ROW("PRINTINITIALS") & " Dated " & ROW("DATE"), Application.StartupPath & "\" & ROW("NAME") & "INVOICE_" & Val(ROW("SRNO")) & ".pdf", ROW("NAME") & "INVOICE_" & Val(ROW("SRNO")) & ".pdf")

                    'ADDING IN DTWHATSAPP
                    If ClientName = "MAHAVIRPOLYCOT" Then ROW("AGENTWHATSAPP") = ""
                    DTWHATSAPP.Rows.Add(ROW("SRNO"), DT.Rows(0).Item("REGID"), cmbregister.Text.Trim, ROW("PRINTINITIALS"), ROW("DATE"), ROW("PARTYNAME"), ROW("PARTYWHATSAPP"), ROW("BROKERNAME"), ROW("AGENTWHATSAPP"), Val(ROW("GRANDTOTAL")), UCase(CmpName) & " - Purchase Invoice No. " & ROW("PRINTINITIALS") & " Dated " & ROW("DATE"), Application.StartupPath & "\" & ROW("PARTYNAME") & "_PURCHASEINVOICE_NO-" & Val(ROW("SRNO")) & ".pdf", ROW("PARTYNAME") & "_PURCHASEINVOICE_NO-" & Val(ROW("SRNO")) & ".pdf")

                    DT = OBJCMN.Execute_Any_String("UPDATE PURCHASEMASTER SET BILL_SENDWHATSAPP = 1 FROM PURCHASEMASTER INNER JOIN REGISTERMASTER On PURCHASEMASTER.BILL_REGISTERID = REGISTERMASTER.register_id WHERE BILL_NO = " & Val(ROW("SRNO")) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "'  AND BILL_YEARID = " & YearId, "", "")

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

End Class