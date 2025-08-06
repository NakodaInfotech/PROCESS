
Imports BL
Imports System.Windows.Forms
Imports System.Net.Mail
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Web
Imports CrystalDecisions.Shared
Imports System.Windows.Forms.PrintDialog
Imports WAProAPI
Imports Newtonsoft.Json


Module Functions

#Region "PRINTING"

    Sub serverprop(ByVal fromno As Integer, ByVal tono As Integer, ByVal REGISTERNAME As String, ByVal FRMSTRING As String, Optional ByVal NOOFCOPIES As Integer = 1, Optional ByVal PRINTSETTING As Object = Nothing)

        For I As Integer = fromno To tono
            Dim OBJCMN As New ClsCommon
            Dim DTYARNGREY As New DataTable
            Dim OBJINV As New SODesign
            Dim RPT As New Object
            If FRMSTRING = "INVOICE" Then
                DTYARNGREY = OBJCMN.search("ISNULL(INVOICEMASTER.INVOICE_TYPE,'GREY') AS INVTYPE, ISNULL(INVOICEMASTER.INVOICE_SCREENTYPE,'TOTAL GST') AS INVSCREENTYPE", "", "INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID ", " AND INVOICE_NO = " & Val(I) & " AND REGISTER_NAME = '" & REGISTERNAME & "' AND INVOICE_YEARID = " & YearId)
                If DTYARNGREY.Rows.Count > 0 Then
                    If DTYARNGREY.Rows(0).Item("INVTYPE") = "GREY" Then
                        If DTYARNGREY.Rows(0).Item("INVSCREENTYPE") = "LINE GST" Then RPT = New InvoiceReport_COMMON Else RPT = New InvoiceReport_TOTALGST
                    Else
                        If DTYARNGREY.Rows(0).Item("INVSCREENTYPE") = "LINE GST" Then RPT = New InvoiceReport_Yarn Else RPT = New InvoiceReport_YarnTOTALGST
                    End If
                    RPT.RecordSelectionFormula = " {INVOICEMASTER.INVOICE_no}=" & Val(I) & "  and {REGISTERMASTER.REGISTER_NAME}= '" & REGISTERNAME & "' and {INVOICEMASTER.INVOICE_yearid}=" & YearId
                    If ClientName = "NIRMALA" Or ClientName = "YESHA" Then RPT.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                End If

            ElseIf FRMSTRING = "CHALLAN" Then
                DTYARNGREY = OBJCMN.search("ISNULL(CHALLANMASTER.CHALLAN_TYPE,'GREY') AS CHALLANTYPE", "", "CHALLANMASTER ", " AND CHALLAN_NO = " & Val(I) & " AND CHALLAN_YEARID = " & YearId)
                If DTYARNGREY.Rows.Count > 0 Then
                    If DTYARNGREY.Rows(0).Item("CHALLANTYPE") = "GREY" Then
                        If ClientName = "SASHWINKUMAR" Then
                            RPT = New ChallanReport_SASHWIN
                        Else
                            RPT = New ChallanReport
                        End If
                    Else
                        RPT = New ChallanReport_Yarn
                    End If
                    RPT.RecordSelectionFormula = " {CHALLANMASTER.CHALLAN_no}=" & Val(I) & "  and {CHALLANMASTER.CHALLAN_yearid}=" & YearId
                End If
            ElseIf FRMSTRING = "PROGRAMREPORT" Then
                RPT = New ProgramReport
                RPT.RecordSelectionFormula = " {PROGRAMMASTER.PROGRAM_NO}=" & Val(I)
            ElseIf FRMSTRING = "SALEORDER" Then
                RPT = New SOReport
                RPT.RecordSelectionFormula = " {SALEORDER.SO_NO}=" & Val(I) & "  and {SALEORDER.SO_yearid}=" & YearId
            ElseIf FRMSTRING = "YARNISSUEWARPER" Then
                RPT = New YarnIssueWarperReport
                RPT.RecordSelectionFormula = " {YARNISSUEWARPER.YISSUEWARPER_NO}=" & Val(I) & "  and {YARNISSUEWARPER.YISSUEWARPER_YEARID}=" & YearId
            ElseIf FRMSTRING = "YARNISSUESIZER" Then
                RPT = New YarnIssueSizerReport
                RPT.RecordSelectionFormula = " {YARNISSUESIZER.YISSUESIZER_NO}=" & Val(I) & "  and {YARNISSUESIZER.YISSUESIZER_YEARID}=" & YearId
            ElseIf FRMSTRING = "YARNISSUEWEAVER" Then
                RPT = New YarnIssueWeaverReport
                RPT.RecordSelectionFormula = " {YARNISSUEWEAVER.YISSUEWEAVER_NO}=" & Val(I) & "  and {YARNISSUEWEAVER.YISSUEWEAVER_YEARID}=" & YearId
            ElseIf FRMSTRING = "YARNISSUEDYEING" Then
                RPT = New YarnIssueDyeingReport
                RPT.RecordSelectionFormula = " {YARNISSUEDYEING.YISSUEDYEING_NO}=" & Val(I) & "  and {YARNISSUEDYEING.YISSUEDYEING_YEARID}=" & YearId
            ElseIf FRMSTRING = "BEAMISSUEBEAMNO" Then
                RPT = New BeamIssueReport
                RPT.RecordSelectionFormula = " {BEAMISSUEWEAVER.BEAMISSUE_NO}=" & Val(I) & "  and {BEAMISSUEWEAVER.BEAMISSUE_YEARID}=" & YearId
            ElseIf FRMSTRING = "DOREPORT" Then
                RPT = New DeliveryReport
                RPT.RecordSelectionFormula = " {DELIVERYORDER.DO_NO}=" & Val(I) & "  and {DELIVERYORDER.DO_yearid}=" & YearId
            End If




            'Dim RPTCHALLAN As New ChallanReport

            '**************** SET SERVER ************************
            Dim crtableLogonInfo As New TableLogOnInfo
            Dim crConnecttionInfo As New ConnectionInfo
            Dim crTables As Tables
            Dim crTable As Table

            With crConnecttionInfo
                .ServerName = SERVERNAME
                .DatabaseName = DatabaseName
                .UserID = DBUSERNAME
                .Password = Dbpassword
                .IntegratedSecurity = Dbsecurity
            End With

            crTables = RPT.Database.Tables
            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            OBJINV.MdiParent = MDIMain

            RPT.PrintOptions.PrinterName = PRINTSETTING.PrinterSettings.PrinterName
            RPT.PrintToPrinter(Val(PRINTSETTING.PrinterSettings.Copies), True, 0, 0)
            
        Next
    End Sub

    Sub ALLCHQPRINTING(ByVal FROMNO As Integer, ByVal TONO As Integer, ByVal REGNAME As String)

        For I As Integer = FROMNO To TONO
            Dim OBJCHQ As New payment_advice
            Dim RPTCHQ As New ChqPayment
            Dim RPTCHQ_SASHWINKUMAR As New ChqPayment_SASHWINKUMAR

            '**************** SET SERVER ************************
            Dim crtableLogonInfo As New TableLogOnInfo
            Dim crConnecttionInfo As New ConnectionInfo
            Dim crTables As Tables
            Dim crTable As Table

            With crConnecttionInfo
                .ServerName = SERVERNAME
                .DatabaseName = DatabaseName
                .UserID = DBUSERNAME
                .Password = Dbpassword
                .IntegratedSecurity = Dbsecurity
            End With

            If ClientName = "SASHWINKUMAR" Then
                crTables = RPTCHQ_SASHWINKUMAR.Database.Tables
            Else
                crTables = RPTCHQ.Database.Tables
            End If
            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            RPTCHQ.RecordSelectionFormula = " {PAYMENTMASTER.PAYMENT_NO}= " & I & " and {REGISTERMASTER.REGISTER_NAME} = '" & REGNAME & "' and {PAYMENTMASTER.PAYMENT_YEARID} = " & YearId
            RPTCHQ.PrintToPrinter(1, True, 0, 0)

        Next
    End Sub

#End Region

    Sub VIEWFORM(ByVal TYPE As String, ByVal EDIT As Boolean, ByVal BILLNO As Integer, ByVal REGTYPE As String)
        Try
            If TYPE = "PURCHASE" Then

                Dim OBJPURCHASE As New PurchaseMaster
                OBJPURCHASE.MdiParent = MDIMain
                OBJPURCHASE.EDIT = EDIT
                OBJPURCHASE.TEMPBILLNO = BILLNO
                OBJPURCHASE.TEMPREGNAME = REGTYPE
                OBJPURCHASE.SELECTEDREG = REGTYPE
                OBJPURCHASE.Show()

            ElseIf TYPE = "SALE" Then

                Dim OBJSALE As New InvoiceMaster
                OBJSALE.MdiParent = MDIMain
                OBJSALE.edit = EDIT
                OBJSALE.TEMPINVOICENO = BILLNO
                OBJSALE.TEMPREGNAME = REGTYPE
                OBJSALE.Show()

            ElseIf TYPE = "PAYMENT" Then

                Dim OBJPAYMENT As New PaymentMaster
                OBJPAYMENT.MdiParent = MDIMain
                OBJPAYMENT.edit = EDIT
                OBJPAYMENT.TEMPPAYMENTNO = BILLNO
                OBJPAYMENT.TEMPREGNAME = REGTYPE
                OBJPAYMENT.Show()

            ElseIf TYPE = "RECEIPT" Then

                Dim OBJREC As New Receipt
                OBJREC.MdiParent = MDIMain
                OBJREC.edit = EDIT
                OBJREC.TEMPRECEIPTNO = BILLNO
                OBJREC.TEMPREGNAME = REGTYPE
                OBJREC.Show()

            ElseIf TYPE = "JOURNAL" Then

                Dim OBJJV As New journal
                OBJJV.MdiParent = MDIMain
                OBJJV.edit = EDIT
                OBJJV.tempjvno = BILLNO
                OBJJV.TEMPREGNAME = REGTYPE
                OBJJV.Show()

            ElseIf TYPE = "DEBITNOTE" Then

                Dim OBJDN As New DebitNote
                OBJDN.MdiParent = MDIMain
                OBJDN.edit = EDIT
                OBJDN.TEMPDNNO = BILLNO
                OBJDN.TEMPREGNAME = REGTYPE
                OBJDN.Show()

            ElseIf TYPE = "CREDITNOTE" Then

                Dim OBJCN As New CREDITNOTE
                OBJCN.MdiParent = MDIMain
                OBJCN.edit = EDIT
                OBJCN.TEMPCNNO = BILLNO
                OBJCN.TEMPREGNAME = REGTYPE
                OBJCN.Show()

            ElseIf TYPE = "CONTRA" Then

                Dim OBJCON As New CONTRA
                OBJCON.MdiParent = MDIMain
                OBJCON.edit = EDIT
                OBJCON.tempcontrano = BILLNO
                OBJCON.TEMPREGNAME = REGTYPE
                OBJCON.Show()

            ElseIf TYPE = "EXPENSE" Then

                Dim OBJEXP As New ExpenseVoucher
                OBJEXP.MdiParent = MDIMain
                OBJEXP.edit = EDIT
                OBJEXP.TEMPEXPNO = BILLNO
                OBJEXP.TEMPREGNAME = REGTYPE
                OBJEXP.FRMSTRING = "NONPURCHASE"
                OBJEXP.Show()

            ElseIf TYPE = "SALE RETURN" Then

                Dim OBJSALRET As New SaleReturn
                OBJSALRET.MdiParent = MDIMain
                OBJSALRET.edit = EDIT
                OBJSALRET.TEMPSALERETNO = BILLNO
                OBJSALRET.TEMPREGNAME = REGTYPE
                OBJSALRET.Show()

            ElseIf TYPE = "PUR RETURN" Then

                Dim OBJPURRET As New PurchaseReturn
                OBJPURRET.MdiParent = MDIMain
                OBJPURRET.EDIT = EDIT
                OBJPURRET.TEMPBILLNO = BILLNO
                OBJPURRET.TEMPREGNAME = REGTYPE
                OBJPURRET.Show()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function ErrHandle(ByVal Errcode As Integer) As Boolean
        Dim bln As Boolean = False
        If Errcode = -675406840 Then
            MsgBox("Check Internet Connection")
            bln = True
        End If
        Return bln
    End Function

    Public Sub pcase(ByRef txt As Object)
        txt.Text = StrConv(txt.Text, VbStrConv.ProperCase)
    End Sub

    Public Sub uppercase(ByRef txt As Object)
        txt.Text = StrConv(txt.Text, VbStrConv.Uppercase)
    End Sub

    Public Sub lowercase(ByRef txt As Object)
        txt.Text = StrConv(txt.Text, VbStrConv.Lowercase)
    End Sub

    Public Sub GETMAXSERIES(ByVal TXTSERIES As TextBox)
        Try
            Dim DTTABLE As DataTable = getmax(" ISNULL(MAX(SERIES),0) + 1 ", " OUTWARDSERIES ", " AND YEARID = " & YearId)
            If DTTABLE.Rows.Count > 0 Then TXTSERIES.Text = DTTABLE.Rows(0).Item(0)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


#Region "IN WORDS FUNCTION"

    Function CurrencyToWord(ByVal Num As Decimal) As String

        'I have created this function for converting amount in indian rupees (INR). 
        'You can manipulate as you wish like decimal setting, Doller (any currency) Prefix.

        Dim strNum As String
        Dim strNumDec As String
        Dim StrWord As String
        strNum = Num

        If InStr(1, strNum, ".") <> 0 Then
            strNumDec = Mid(strNum, InStr(1, strNum, ".") + 1)

            If Len(strNumDec) = 1 Then
                strNumDec = strNumDec + "0"
            End If
            If Len(strNumDec) > 2 Then
                strNumDec = Mid(strNumDec, 1, 2)
            End If

            strNum = Mid(strNum, 1, InStr(1, strNum, ".") - 1)
            StrWord = IIf(CDbl(strNum) = 1, " Rupee ", " Rupees ") + NumToWord(CDbl(strNum)) + IIf(CDbl(strNumDec) > 0, " and Paise" + cWord3(CDbl(strNumDec)), "")
        Else
            StrWord = IIf(CDbl(strNum) = 1, " Rupee ", " Rupees ") + NumToWord(CDbl(strNum))
        End If
        CurrencyToWord = StrWord & " Only"
        Return CurrencyToWord

    End Function

    Function NumToWord(ByVal Num As Decimal) As String

        'I divided this function in two part.
        '1. Three or less digit number.
        '2. more than three digit number.
        Dim strNum As String
        Dim StrWord As String
        strNum = Num

        If Len(strNum) <= 3 Then
            StrWord = cWord3(CDbl(strNum))
        Else
            StrWord = cWordG3(CDbl(Mid(strNum, 1, Len(strNum) - 3))) + " " + cWord3(CDbl(Mid(strNum, Len(strNum) - 2)))
        End If
        NumToWord = StrWord

    End Function

    Function cWordG3(ByVal Num As Decimal) As String

        '2. more than three digit number.
        Dim strNum As String = ""
        Dim StrWord As String = ""
        Dim readNum As String = ""
        strNum = Num
        If Len(strNum) Mod 2 <> 0 Then
            readNum = CDbl(Mid(strNum, 1, 1))
            If readNum <> "0" Then
                StrWord = retWord(readNum)
                readNum = CDbl("1" + strReplicate("0", Len(strNum) - 1) + "000")
                StrWord = StrWord + " " + retWord(readNum)
            End If
            strNum = Mid(strNum, 2)
        End If
        While Not Len(strNum) = 0
            readNum = CDbl(Mid(strNum, 1, 2))
            If readNum <> "0" Then
                StrWord = StrWord + " " + cWord3(readNum)
                readNum = CDbl("1" + strReplicate("0", Len(strNum) - 2) + "000")
                StrWord = StrWord + " " + retWord(readNum)
            End If
            strNum = Mid(strNum, 3)
        End While
        cWordG3 = StrWord
        Return cWordG3

    End Function

    Function strReplicate(ByVal str As String, ByVal intD As Integer) As String

        'This fucntion padded "0" after the number to evaluate hundred, thousand and on....
        'using this function you can replicate any Charactor with given string.
        strReplicate = ""
        For i As Integer = 1 To intD
            strReplicate = strReplicate + str
        Next
        Return strReplicate

    End Function

    Function cWord3(ByVal Num As Decimal) As String

        '1. Three or less digit number.
        Dim strNum As String = ""
        Dim StrWord As String = ""
        Dim readNum As String = ""
        If Num < 0 Then Num = Num * -1
        strNum = Num

        If Len(strNum) = 3 Then
            readNum = CDbl(Mid(strNum, 1, 1))
            StrWord = retWord(readNum) + " Hundred"
            strNum = Mid(strNum, 2, Len(strNum))
        End If

        If Len(strNum) <= 2 Then
            If CDbl(strNum) >= 0 And CDbl(strNum) <= 20 Then
                StrWord = StrWord + " " + retWord(CDbl(strNum))
            Else
                StrWord = StrWord + " " + retWord(CDbl(Mid(strNum, 1, 1) + "0")) + " " + retWord(CDbl(Mid(strNum, 2, 1)))
            End If
        End If

        strNum = CStr(Num)
        cWord3 = StrWord
        Return cWord3

    End Function

    Function retWord(ByVal Num As Decimal) As String
        'This two dimensional array store the primary word convertion of number.
        retWord = ""
        Dim ArrWordList(,) As Object = {{0, ""}, {1, "One"}, {2, "Two"}, {3, "Three"}, {4, "Four"}, _
                                        {5, "Five"}, {6, "Six"}, {7, "Seven"}, {8, "Eight"}, {9, "Nine"}, _
                                        {10, "Ten"}, {11, "Eleven"}, {12, "Twelve"}, {13, "Thirteen"}, {14, "Fourteen"}, _
                                        {15, "Fifteen"}, {16, "Sixteen"}, {17, "Seventeen"}, {18, "Eighteen"}, {19, "Nineteen"}, _
                                        {20, "Twenty"}, {30, "Thirty"}, {40, "Forty"}, {50, "Fifty"}, {60, "Sixty"}, _
                                        {70, "Seventy"}, {80, "Eighty"}, {90, "Ninety"}, {100, "Hundred"}, {1000, "Thousand"}, _
                                        {100000, "Lakh"}, {10000000, "Crore"}}

        For i As Integer = 0 To UBound(ArrWordList)
            If Num = ArrWordList(i, 0) Then
                retWord = ArrWordList(i, 1)
                Exit For
            End If
        Next
        Return retWord

    End Function

#End Region

    Sub FILLAREA(ByRef cmbname As ComboBox)
        Try
            If cmbname.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable

                dt = OBJCMN.search(" AREA_name ", "", " AREAMaster", " and AREA_cmpid=" & CmpId & " AND AREA_LOCATIONID = " & Locationid & " AND AREA_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "AREA_name"
                    cmbname.DataSource = dt
                    cmbname.DisplayMember = "AREA_name"
                    cmbname.Text = ""
                End If
                cmbname.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub AREAVALIDATE(ByRef CMBAREA As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBAREA.Text.Trim <> "" Then
                uppercase(CMBAREA)
                Dim OBJCMN As New ClsCommonMaster
                Dim objyearmaster As New ClsYearMaster
                Dim dt As DataTable
                dt = OBJCMN.search("AREA_name", "", "AREAMaster", " and AREA_name = '" & CMBAREA.Text.Trim & "' AND AREA_CMPID = " & CmpId & " AND AREA_LOCATIONID = " & Locationid & " AND AREA_YEARID = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBAREA.Text.Trim
                    Dim tempmsg As Integer = MsgBox("AREA not present, Add New?", MsgBoxStyle.YesNo, " ")
                    If tempmsg = vbYes Then
                        CMBAREA.Text = a
                        Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'AREA MASTER'")
                        If DTROW(0).Item(1) = False Then
                            MsgBox("Insufficient Rights")
                            Exit Sub
                        End If
                        objyearmaster.savearea(CMBAREA.Text.Trim, CmpId, Locationid, Userid, YearId, " and AREA_name = '" & CMBAREA.Text.Trim & "' AND AREA_CMPID = " & CmpId & " AND AREA_LOCATIONID = " & Locationid & " AND AREA_YEARID = " & YearId)
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub fillCOUNTRY(ByRef cmbname As ComboBox)
        Try
            If cmbname.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable

                dt = OBJCMN.search(" COUNTRY_name ", "", " COUNTRYMaster", " and COUNTRY_cmpid=" & CmpId & " AND COUNTRY_LOCATIONID = " & Locationid & " AND COUNTRY_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "COUNTRY_name"
                    cmbname.DataSource = dt
                    cmbname.DisplayMember = "COUNTRY_name"
                    cmbname.Text = ""
                End If
                cmbname.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub COUNTRYVALIDATE(ByRef CMBCOUNTRY As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBCOUNTRY.Text.Trim <> "" Then
                uppercase(CMBCOUNTRY)
                Dim OBJCMN As New ClsCommonMaster
                Dim objyearmaster As New ClsYearMaster
                Dim dt As DataTable
                dt = OBJCMN.search("COUNTRY_name", "", "COUNTRYMaster", " and COUNTRY_name = '" & CMBCOUNTRY.Text.Trim & "' AND COUNTRY_CMPID = " & CmpId & " AND COUNTRY_LOCATIONID = " & Locationid & " AND COUNTRY_YEARID = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBCOUNTRY.Text.Trim
                    Dim tempmsg As Integer = MsgBox("COUNTRY not present, Add New?", MsgBoxStyle.YesNo, " ")
                    If tempmsg = vbYes Then
                        CMBCOUNTRY.Text = a
                        Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'COUNTRY MASTER'")
                        If DTROW(0).Item(1) = False Then
                            MsgBox("Insufficient Rights")
                            Exit Sub
                        End If
                        objyearmaster.savecountry(CMBCOUNTRY.Text.Trim, CmpId, Locationid, Userid, YearId, " and COUNTRY_name = '" & CMBCOUNTRY.Text.Trim & "' AND COUNTRY_CMPID = " & CmpId & " AND COUNTRY_LOCATIONID = " & Locationid & " AND COUNTRY_YEARID = " & YearId)
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub DELIVAERYATVALIDATE(ByRef CMBDELIVERYAT As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBDELIVERYAT.Text.Trim <> "" Then
                uppercase(CMBDELIVERYAT)
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search("   DELIVERYAT_NAME", "", "  DELIVERYATMASTER ", " and  DELIVERYAT_NAME = '" & CMBDELIVERYAT.Text.Trim & "' and DELIVERYAT_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBDELIVERYAT.Text.Trim
                    Dim tempmsg As Integer = MsgBox("Name not present, Add New?", MsgBoxStyle.YesNo, CmpName)
                    If tempmsg = vbYes Then
                        CMBDELIVERYAT.Text = a
                        Dim OBJDELIVERY As New DeliveryAtMaster
                        OBJDELIVERY.TEMPDELAT = CMBDELIVERYAT.Text.Trim()

                        OBJDELIVERY.ShowDialog()
                        dt = OBJCMN.search("   DELIVERYAT_NAME", "", "  DELIVERYATMASTER ", " and  DELIVERYAT_NAME = '" & CMBDELIVERYAT.Text.Trim & "' and DELIVERYAT_Yearid = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = CMBDELIVERYAT.DataSource
                            If CMBDELIVERYAT.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(CMBDELIVERYAT.Text.Trim)
                                    CMBDELIVERYAT.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub HSNITEMDESCVALIDATE(ByRef CMBHSNCODE As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBHSNCODE.Text.Trim <> "" Then
                uppercase(CMBHSNCODE)
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search("   ISNULL(HSN_CODE, '') AS HSNCODE", "", "  HSNMASTER ", "and  HSN_CODE = '" & CMBHSNCODE.Text.Trim & "' and HSN_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBHSNCODE.Text.Trim
                    Dim tempmsg As Integer = MsgBox("HSN/SAC Code Not present, Add New?", MsgBoxStyle.YesNo, CmpName)
                    If tempmsg = vbYes Then
                        CMBHSNCODE.Text = a
                        Dim OBJDELIVERY As New HSNMaster
                        OBJDELIVERY.TEMPHSNCODE = CMBHSNCODE.Text.Trim()

                        OBJDELIVERY.ShowDialog()
                        dt = OBJCMN.search("   ISNULL(HSN_CODE, '') AS HSNCODE", "", "  HSNMASTER ", " and  HSN_CODE = '" & CMBHSNCODE.Text.Trim & "' and HSN_Yearid = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = CMBHSNCODE.DataSource
                            If CMBHSNCODE.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(CMBHSNCODE.Text.Trim)
                                    CMBHSNCODE.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub fillSTATE(ByRef cmbname As ComboBox)
        Try
            If cmbname.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable

                dt = OBJCMN.search(" STATE_name ", "", " STATEMaster", " and STATE_cmpid=" & CmpId & " AND STATE_LOCATIONID = " & Locationid & " AND STATE_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "STATE_name"
                    cmbname.DataSource = dt
                    cmbname.DisplayMember = "STATE_name"
                    cmbname.Text = ""
                End If
                cmbname.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub STATEVALIDATE(ByRef CMBSTATE As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBSTATE.Text.Trim <> "" Then
                uppercase(CMBSTATE)
                Dim OBJCMN As New ClsCommonMaster
                Dim objyearmaster As New ClsYearMaster
                Dim dt As DataTable
                dt = OBJCMN.search("STATE_name", "", "STATEMaster", " and STATE_name = '" & CMBSTATE.Text.Trim & "' AND STATE_CMPID = " & CmpId & " AND STATE_LOCATIONID = " & Locationid & " AND STATE_YEARID = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBSTATE.Text.Trim
                    Dim tempmsg As Integer = MsgBox("STATE not present, Add New?", MsgBoxStyle.YesNo, " ")
                    If tempmsg = vbYes Then
                        CMBSTATE.Text = a
                        Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'STATE MASTER'")
                        If DTROW(0).Item(1) = False Then
                            MsgBox("Insufficient Rights")
                            Exit Sub
                        End If
                        objyearmaster.savestate(CMBSTATE.Text.Trim, CmpId, Locationid, Userid, YearId, " and STATE_name = '" & CMBSTATE.Text.Trim & "' AND STATE_CMPID = " & CmpId & " AND STATE_LOCATIONID = " & Locationid & " AND STATE_YEARID = " & YearId)
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Function setcolor(ByVal no As Integer) As String
        Try
            Dim str As String = String.Empty
            Dim remainder As Integer
            remainder = no Mod 30
            Select Case remainder
                Case 0
                    str = "Turquoise"
                Case 1
                    str = "LightGreen"
                Case 2
                    str = "LightSkyBlue"
                Case 3
                    str = "Lavender"
                Case 4
                    str = "Plum"
                Case 5
                    str = "Pink"
                Case 6
                    str = "LightCyan"
                Case 7
                    str = "Gold"
                Case 8
                    str = "Silver"
                Case 9
                    str = "Khaki"
                Case 10
                    str = "LIGHTCORAL"
                Case 11
                    str = "MISTYROSE"
                Case 12
                    str = "LIGHTSALMON"
                Case 13
                    str = "SEASHELL"
                Case 14
                    str = "PEACHPUFF"
                Case 15
                    str = "CORNSILK"
                Case 16
                    str = "YELLOWGREEN"
                Case 17
                    str = "HotPink"
                Case 18
                    str = "HONEYDEW"
                Case 19
                    str = "LAVENDER"
                Case 20
                    str = "THISTLE"
                Case 21
                    str = "PINK"
                Case 22
                    str = "GREENYELLOW"
                Case 23
                    str = "SkyBlue"
                Case 24
                    str = "LightCyan"
                Case 25
                    str = "Lime"
                Case 26
                    str = "Wheat"
                Case 27
                    str = "Cornsilk"
                Case 28
                    str = "DarkOrange"
                Case 29
                    str = "PaleVioletRed"
                Case 30
                    str = "LIGHTPINK"

            End Select
            Return str
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Function

    Sub fillform(ByRef CHKFORM As CheckedListBox, ByRef edit As Boolean, Optional ByVal WHERECLAUSE As String = "")
        Try
            If CHKFORM.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable

                dt = OBJCMN.search(" form_name ", "", " FORMTYPE", WHERECLAUSE)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "FORM_name"
                    CHKFORM.DataSource = dt
                    CHKFORM.DisplayMember = "FORM_name"
                    If edit = False Then CHKFORM.Text = ""
                End If
                ''CHKFORM.SelectedIndex = 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLFORMTYPE(ByRef CMBFORM As ComboBox, ByRef edit As Boolean, Optional ByVal WHERECLAUSE As String = "")
        Try
            If CMBFORM.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable

                dt = OBJCMN.search(" form_name ", "", " FORMTYPE", WHERECLAUSE)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "FORM_name"
                    CMBFORM.DataSource = dt
                    CMBFORM.DisplayMember = "FORM_name"
                    If edit = False Then CMBFORM.Text = ""
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub filltax(ByRef cmbtax As ComboBox, ByRef edit As Boolean)
        Try
            If cmbtax.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable

                dt = OBJCMN.search(" tax_name ", "", " TaxMaster", " and TAX_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "tax_name"
                    cmbtax.DataSource = dt
                    cmbtax.DisplayMember = "tax_name"
                    If edit = False Then cmbtax.Text = ""
                End If
                cmbtax.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillper(ByRef cmbper As ComboBox, ByRef edit As Boolean)
        Try
            If cmbper.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable

                dt = OBJCMN.search(" per_AMT ", "", " PERMaster", " and PER_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "PER_AMT"
                    cmbper.DataSource = dt
                    cmbper.DisplayMember = "PER_AMT"
                    If edit = False Then cmbper.Text = ""
                End If
                cmbper.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ALPHEBETKEYPRESS(ByVal han As KeyPressEventArgs, ByVal sen As Control, ByVal frm As System.Windows.Forms.Form)
        If (AscW(han.KeyChar) >= 65 And AscW(han.KeyChar) <= 90) Or (AscW(han.KeyChar) >= 97 And AscW(han.KeyChar) <= 122) Or AscW(han.KeyChar) = 8 Then
            han.KeyChar = han.KeyChar
        Else
            han.KeyChar = ""
        End If
        If AscW(han.KeyChar) = Keys.Escape Then
            frm.Close()
        End If
    End Sub

    Sub numdot3(ByVal han As KeyPressEventArgs, ByVal txt As Object, ByVal frm As System.Windows.Forms.Form)
        Dim mypos As Integer

        mypos = InStr(1, txt.Text, ".")

        If AscW(han.KeyChar) > 47 And AscW(han.KeyChar) < 58 Or AscW(han.KeyChar) = 8 Or AscW(han.KeyChar) = 46 Then
            han.KeyChar = han.KeyChar
        Else
            han.KeyChar = ""
        End If


        If AscW(han.KeyChar) > 47 And AscW(han.KeyChar) < 58 And mypos <> "0" Then
            If txt.SelectionStart = mypos + 3 Then
                han.KeyChar = ""
            End If
        End If

        If txt.SelectionStart >= mypos Then
            txt.SelectionLength = 1
            han.KeyChar = han.KeyChar
        End If

        If AscW(han.KeyChar) = 46 Then

            'test = True
            mypos = InStr(1, txt.Text, ".")
            If mypos <> "0" Then txt.SelectionStart = mypos
            If mypos = 0 Then
                han.KeyChar = han.KeyChar
            Else
                han.KeyChar = ""
            End If

        End If

        If AscW(han.KeyChar) = Keys.Escape Then
            frm.Close()
        End If
    End Sub

    Sub numdot(ByVal han As KeyPressEventArgs, ByVal txt As TextBox, ByVal frm As System.Windows.Forms.Form)
        Dim mypos As Integer

        mypos = InStr(1, txt.Text, ".")

        If AscW(han.KeyChar) > 47 And AscW(han.KeyChar) < 58 Or AscW(han.KeyChar) = 8 Or AscW(han.KeyChar) = 46 Then
            han.KeyChar = han.KeyChar
        Else
            han.KeyChar = ""
        End If


        If AscW(han.KeyChar) > 47 And AscW(han.KeyChar) < 58 And mypos <> "0" Then
            If txt.SelectionStart = mypos + 2 Then
                han.KeyChar = ""
            End If
        End If

        If txt.SelectionStart >= mypos Then
            txt.SelectionLength = 1
            han.KeyChar = han.KeyChar
        End If

        If AscW(han.KeyChar) = 46 Then

            'test = True
            mypos = InStr(1, txt.Text, ".")
            If mypos <> "0" Then txt.SelectionStart = mypos
            If mypos = 0 Then
                han.KeyChar = han.KeyChar
            Else
                han.KeyChar = ""
            End If

        End If

        If AscW(han.KeyChar) = Keys.Escape Then
            frm.Close()
        End If
    End Sub

    Sub numdotkeypress(ByVal han As KeyPressEventArgs, ByVal sen As Object, ByVal frm As System.Windows.Forms.Form)
        Dim mypos As Integer

        If AscW(han.KeyChar) >= 48 And AscW(han.KeyChar) <= 57 Or AscW(han.KeyChar) = 8 Then
            han.KeyChar = han.KeyChar
        ElseIf AscW(han.KeyChar) = 46 Then
            mypos = InStr(1, sen.Text, ".")
            If mypos = 0 Then
                han.KeyChar = han.KeyChar
            Else
                han.KeyChar = ""
            End If
        Else
            han.KeyChar = ""
        End If

        If AscW(han.KeyChar) = Keys.Escape Then
            frm.Close()
        End If
    End Sub

    Sub numkeypress(ByVal han As KeyPressEventArgs, ByVal sen As Object, ByVal frm As System.Windows.Forms.Form)

        If AscW(han.KeyChar) >= 48 And AscW(han.KeyChar) <= 57 Or AscW(han.KeyChar) = 8 Then
            han.KeyChar = han.KeyChar
        Else
            han.KeyChar = ""
        End If

        If AscW(han.KeyChar) = Keys.Escape Then
            frm.Close()
        End If
    End Sub

    Function getmax(ByVal fldname As String, ByVal tbname As String, Optional ByVal whereclause As String = "") As DataTable
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim DTTABLE As DataTable

            Dim OBJCMN As New ClsCommon()
            DTTABLE = OBJCMN.GETMAXNO(fldname, tbname, whereclause)

            Return DTTABLE
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Function

    Function getfirstdate(ByVal cmpid As Integer, Optional ByVal monthname As String = "", Optional ByVal monthno As Integer = 0) As Date
        Try
            Dim objcmn As New ClsCommon
            Dim ddate As Date
            If monthname <> "" And monthno = 0 Then
                If monthname = "April" Then monthno = 4
                If monthname = "May" Then monthno = 5
                If monthname = "June" Then monthno = 6
                If monthname = "July" Then monthno = 7
                If monthname = "August" Then monthno = 8
                If monthname = "September" Then monthno = 9
                If monthname = "October" Then monthno = 10
                If monthname = "November" Then monthno = 11
                If monthname = "December" Then monthno = 12
                If monthname = "January" Then monthno = 1
                If monthname = "February" Then monthno = 2
                If monthname = "March" Then monthno = 3

                If monthno < 4 Then
                    ddate = (objcmn.getfirstdate(Convert.ToDateTime((monthno & "/01/" & Year(AccTo)))))
                Else
                    ddate = (objcmn.getfirstdate(Convert.ToDateTime((monthno & "/01/" & Year(AccFrom)))))
                End If
            End If
            Return ddate
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function getlastdate(ByVal cmpid As Integer, Optional ByVal monthname As String = "", Optional ByVal monthno As Integer = 0) As Date
        Try
            Dim objcmn As New ClsCommon
            Dim ddate As Date
            If monthname <> "" And monthno = 0 Then
                If monthname = "April" Then monthno = 4
                If monthname = "May" Then monthno = 5
                If monthname = "June" Then monthno = 6
                If monthname = "July" Then monthno = 7
                If monthname = "August" Then monthno = 8
                If monthname = "September" Then monthno = 9
                If monthname = "October" Then monthno = 10
                If monthname = "November" Then monthno = 11
                If monthname = "December" Then monthno = 12
                If monthname = "January" Then monthno = 1
                If monthname = "February" Then monthno = 2
                If monthname = "March" Then monthno = 3

                If monthno < 4 Then
                    ddate = (objcmn.getlastdate(Convert.ToDateTime((monthno & "/01/" & Year(AccTo)))))
                Else
                    ddate = (objcmn.getlastdate(Convert.ToDateTime((monthno & "/01/" & Year(AccFrom)))))
                End If
            End If
            Return ddate
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function datecheck(ByVal dateval As Date) As Boolean
        Dim bln As Boolean = True
        If dateval.Date > AccTo Or dateval.Date < AccFrom Then
            bln = False
        End If
        Return bln
    End Function

    Sub enterkeypress(ByVal han As KeyPressEventArgs, ByVal frm As System.Windows.Forms.Form)
        If AscW(han.KeyChar) = 13 Then
            SendKeys.Send("{Tab}")
            han.KeyChar = ""
        End If
    End Sub

    Sub fillACCCODE(ByRef CMBCODE As ComboBox, Optional ByVal CONDITION As String = "")
        Try
            Dim OBJCMN As New ClsCommon
            Dim dt As DataTable

            dt = OBJCMN.search(" DISTINCT ACC_CODE ", "", " LEDGERS INNER JOIN GROUPMASTER ON GROUP_ID = ACC_GROUPID AND GROUP_CMPID = ACC_CMPID AND GROUP_LOCATIONID = ACC_LOCATIONID AND GROUP_YEARID = ACC_YEARID ", " and ACC_cmpid=" & CmpId & " AND ACC_LOCATIONID = " & Locationid & " AND ACC_YEARID = " & YearId & CONDITION)
            If dt.Rows.Count > 0 Then
                dt.DefaultView.Sort = "ACC_CODE"
                CMBCODE.DataSource = dt
                CMBCODE.DisplayMember = "ACC_CODE"
                CMBCODE.Text = ""
            End If
            CMBCODE.SelectAll()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillname(ByRef cmbname As ComboBox, ByRef edit As Boolean, ByVal CONDITION As String)
        Try
            Cursor.Current = Cursors.WaitCursor
            If cmbname.Text.Trim = "" Then
                uppercase(cmbname)
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable

                dt = OBJCMN.search(" LEDGERS.ACC_cmpname, LEDGERS.ACC_ID ", "", "LEDGERS INNER JOIN GROUPMASTER ON GROUP_ID = ACC_GROUPID AND GROUP_CMPID = ACC_CMPID AND GROUP_LOCATIONID = ACC_LOCATIONID AND GROUP_YEARID = ACC_YEARID ", " AND ISNULL(ACC_BLOCKED,'FALSE') = 'FALSE' and LEDGERS.ACC_Yearid=" & YearId & CONDITION)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "ACC_cmpname"
                    cmbname.DataSource = dt
                    cmbname.DisplayMember = "ACC_cmpname"
                    cmbname.ValueMember = "ACC_ID"
                    cmbname.Text = ""
                End If
                cmbname.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub fillledger(ByRef cmbname As ComboBox, ByRef edit As Boolean, ByVal WHERECLAUSE As String)
        Try
            Cursor.Current = Cursors.WaitCursor
            If cmbname.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable

                dt = OBJCMN.search(" acc_cmpname, ACC_ID ", "", "LEDGERS INNER JOIN GROUPMASTER ON GROUP_ID = ACC_GROUPID AND GROUP_CMPID = ACC_CMPID AND GROUP_LOCATIONID = ACC_LOCATIONID AND GROUP_YEARID = ACC_YEARID", " AND ISNULL(ACC_BLOCKED,'FALSE') = 'FALSE' AND ACC_CMPID = " & CmpId & " AND ACC_LOCATIONID = " & Locationid & " AND ACC_YEARID = " & YearId & WHERECLAUSE)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "ACC_cmpname"
                    cmbname.DataSource = dt
                    cmbname.DisplayMember = "ACC_cmpname"
                    cmbname.ValueMember = "ACC_ID"
                    If edit = False Then cmbname.Text = ""
                End If
                cmbname.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub fillQUALITY(ByRef CMBQUALITY As ComboBox, ByRef edit As Boolean)
        Try
            If CMBQUALITY.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable

                dt = OBJCMN.search(" QUALITY_name, QUALITY_ID ", "", " QUALITYMaster", " and QUALITY_cmpid=" & CmpId & " AND QUALITY_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "QUALITY_name"
                    CMBQUALITY.DataSource = dt
                    CMBQUALITY.DisplayMember = "QUALITY_name"
                    CMBQUALITY.ValueMember = "QUALITY_ID"
                    CMBQUALITY.Text = ""
                End If
                CMBQUALITY.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLYARNCOMPOSITION(ByRef CMBYARNCOMPOSITION As ComboBox, ByRef edit As Boolean)
        Try
            If CMBYARNCOMPOSITION.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable

                dt = OBJCMN.search(" QUALITY_name, QUALITY_ID ", "", " QUALITYMaster", " and QUALITY_cmpid=" & CmpId & " AND QUALITY_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "QUALITY_name"
                    CMBYARNCOMPOSITION.DataSource = dt
                    CMBYARNCOMPOSITION.DisplayMember = "QUALITY_name"
                    CMBYARNCOMPOSITION.ValueMember = "QUALITY_ID"
                    If edit = False Then CMBYARNCOMPOSITION.Text = ""
                End If
                CMBYARNCOMPOSITION.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLSACCODE(ByRef CMBSACCODE As ComboBox, ByRef edit As Boolean)
        Try
            If CMBSACCODE.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable = OBJCMN.search(" HSN_ITEMDESC ", "", " HSNMASTER ", " AND HSN_TYPE = 'Services' and HSN_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "HSN_ITEMDESC"
                    CMBSACCODE.DataSource = dt
                    CMBSACCODE.DisplayMember = "HSN_ITEMDESC"
                    If edit = False Then CMBSACCODE.Text = ""
                End If
                CMBSACCODE.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLSTOREITEMNAME(ByRef CMBSTOREITEM As ComboBox)
        Try
            If CMBSTOREITEM.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable

                dt = OBJCMN.search(" STOREITEM_name, STOREITEM_ID ", "", " STOREITEMMaster", " AND STOREITEM_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "STOREITEM_name"
                    CMBSTOREITEM.DataSource = dt
                    CMBSTOREITEM.DisplayMember = "STOREITEM_name"
                    CMBSTOREITEM.ValueMember = "STOREITEM_ID"
                    CMBSTOREITEM.Text = ""
                End If
                CMBSTOREITEM.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub STOREITEMVALIDATE(ByRef CMBSTOREITEM As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form, Optional ByRef unit As String = "", Optional ByRef ProcessNAME As String = "")
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBSTOREITEM.Text.Trim <> "" Then
                uppercase(CMBSTOREITEM)
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search("   STOREITEMMASTER.STOREITEM_name AS STOREITEM", "", "  STOREITEMMASTER ", " and  STOREITEMMASTER.STOREITEM_name = '" & CMBSTOREITEM.Text.Trim & "' and STOREITEMMASTER.STOREITEM_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBSTOREITEM.Text.Trim
                    Dim tempmsg As Integer = MsgBox("STOREITEM not present, Add New?", MsgBoxStyle.YesNo, CmpName)
                    If tempmsg = vbYes Then
                        CMBSTOREITEM.Text = a
                        Dim objSTOREITEMmaster As New StoreItemMaster
                        objSTOREITEMmaster.TEMPNAME = CMBSTOREITEM.Text.Trim()

                        objSTOREITEMmaster.ShowDialog()
                        dt = OBJCMN.search("   STOREITEMMASTER.STOREITEM_name AS STOREITEM ", "", "  STOREITEMMASTER ", " and  STOREITEMMASTER.STOREITEM_name = '" & CMBSTOREITEM.Text.Trim & "' and STOREITEMMASTER.STOREITEM_Yearid = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = CMBSTOREITEM.DataSource
                            If CMBSTOREITEM.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(CMBSTOREITEM.Text.Trim)
                                    CMBSTOREITEM.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub fillCATEGORY(ByRef CMBCATEGORY As ComboBox, ByRef edit As Boolean)
        Try
            If CMBCATEGORY.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable

                dt = OBJCMN.search(" CATEGORY_name ", "", " CATEGORYMaster", " and CATEGORY_cmpid=" & CmpId & " AND CATEGORY_LOCATIONID = " & Locationid & " AND CATEGORY_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "CATEGORY_name"
                    CMBCATEGORY.DataSource = dt
                    CMBCATEGORY.DisplayMember = "CATEGORY_name"
                    If edit = False Then CMBCATEGORY.Text = ""
                End If
                CMBCATEGORY.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillCITY(ByRef CMBCITY As ComboBox, ByRef edit As Boolean)
        Try
            If CMBCITY.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable

                dt = OBJCMN.search(" CITY_name ", "", " CITYMaster", " and CITY_cmpid=" & CmpId & " AND CITY_LOCATIONID = " & Locationid & " AND CITY_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "CITY_name"
                    CMBCITY.DataSource = dt
                    CMBCITY.DisplayMember = "CITY_name"
                    If edit = False Then CMBCITY.Text = ""
                End If
                CMBCITY.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillregister(ByRef cmbregister As ComboBox, ByVal condition As String)
        Try
            If cmbregister.Text.Trim = "" Then

                Dim OBJCMN As New ClsCommon
                Dim dt As DataTable
                dt = OBJCMN.search(" Register_name ", "", "RegisterMaster ", condition & " and Register_cmpid=" & CmpId & " and REGISTER_LOCATIONID = " & Locationid & " AND REGISTER_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "Register_name"
                    cmbregister.DataSource = dt
                    cmbregister.DisplayMember = "Register_name"
                    cmbregister.Text = ""
                End If
                cmbregister.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub namevalidate(ByRef cmbname As ComboBox, ByRef CMBACCCODE As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form, ByRef txtadd As System.Windows.Forms.TextBox, ByVal WHERECLAUSE As String, Optional ByVal GROUPNAME As String = "", Optional ByVal TYPE As String = "ACCOUNTS", Optional ByRef TRANSNAME As String = "", Optional ByRef AGENTNAME As String = "", Optional ByRef SUBTYPE As String = "", Optional ByRef WHATSAPPNO As String = "")
        Try
            Cursor.Current = Cursors.WaitCursor
            If cmbname.Text.Trim <> "" Then
                uppercase(cmbname)
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search("LEDGERS.acc_add, isnull(LEDGERS.ACC_CODE,'') as CODE,LEDGERS_1.ACC_CMPNAME AS TRANSNAME,LEDGERS_2.ACC_CMPNAME AS AGENTNAME, ISNULL(LEDGERS.ACC_MOBILE,'') AS WHATSAPPNO ", "", "    LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN LEDGERS AS LEDGERS_1 ON LEDGERS.ACC_TRANSID = LEDGERS_1.Acc_id LEFT OUTER JOIN LEDGERS AS LEDGERS_2 ON LEDGERS.ACC_AGENTID = LEDGERS_2.Acc_id ", " and LEDGERS.acc_cmpname = '" & cmbname.Text.Trim & "' and LEDGERS.acc_YEARid = " & YearId & WHERECLAUSE)
                If dt.Rows.Count = 0 Then
                    Dim a As String = cmbname.Text.Trim
                    Dim tempmsg As Integer = MsgBox("Ledger not present, Add New?", MsgBoxStyle.YesNo, "PROCESS")
                    If tempmsg = vbYes Then
                        cmbname.Text = a
                        Dim objVendormaster As New AccountsMaster
                        objVendormaster.frmstring = "ACCOUNTS"
                        objVendormaster.tempAccountsName = cmbname.Text.Trim()
                        objVendormaster.TEMPGROUPNAME = GROUPNAME
                        objVendormaster.tempTYPE = TYPE
                        objVendormaster.TEMPSUBTYPE = SUBTYPE

                        objVendormaster.ShowDialog()
                        dt = OBJCMN.search("LEDGERS.acc_add, isnull(LEDGERS.ACC_CODE,'') as CODE,LEDGERS_1.ACC_CMPNAME AS TRANSNAME,LEDGERS_2.ACC_CMPNAME AS AGENTNAME, ISNULL(LEDGERS.ACC_MOBILE,'') AS WHATSAPPNO ", "", "    LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_cmpid = GROUPMASTER.group_cmpid AND LEDGERS.Acc_locationid = GROUPMASTER.group_locationid AND LEDGERS.Acc_yearid = GROUPMASTER.group_yearid AND LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN LEDGERS AS LEDGERS_1 ON LEDGERS.ACC_TRANSID = LEDGERS_1.Acc_id AND LEDGERS.Acc_cmpid = LEDGERS_1.Acc_cmpid AND LEDGERS.Acc_locationid = LEDGERS_1.Acc_locationid AND LEDGERS.Acc_yearid = LEDGERS_1.Acc_yearid LEFT OUTER JOIN LEDGERS AS LEDGERS_2 ON LEDGERS.ACC_AGENTID = LEDGERS_2.Acc_id AND LEDGERS.Acc_cmpid = LEDGERS_2.Acc_cmpid AND LEDGERS.Acc_locationid = LEDGERS_2.Acc_locationid AND LEDGERS.Acc_yearid = LEDGERS_2.Acc_yearid ", " and LEDGERS.acc_cmpname = '" & cmbname.Text.Trim & "' and LEDGERS.acc_cmpid = " & CmpId & " and LEDGERS.acc_LOCATIONid = " & Locationid & " and LEDGERS.acc_YEARid = " & YearId & WHERECLAUSE)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = cmbname.DataSource
                            If cmbname.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(cmbname.Text.Trim)
                                    cmbname.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                        Exit Sub
                    End If
                Else
                    txtadd.Text = dt.Rows(0).Item(0).ToString
                    If TRANSNAME = "" Then TRANSNAME = dt.Rows(0).Item(2).ToString
                    If AGENTNAME = "" Then AGENTNAME = dt.Rows(0).Item(3).ToString
                    If WHATSAPPNO = "" Then WHATSAPPNO = dt.Rows(0).Item("WHATSAPPNO")
                End If
            End If
        Catch ex As Exception
            GoTo line1
            Throw ex
        End Try
    End Sub

    Sub ledgervalidate(ByRef cmbname As ComboBox, ByVal CMBACCCODE As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form, ByRef txtadd As System.Windows.Forms.TextBox, ByVal WHERECLAUSE As String, Optional ByVal GROUPNAME As String = "")
        Try
            Cursor.Current = Cursors.WaitCursor
            If cmbname.Text.Trim <> "" Then
                uppercase(cmbname)
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search("acc_add, isnull( ACC_CODE,''), REGISTER_NAME AS REGISTERNAME", "", " LEDGERS INNER JOIN GROUPMASTER ON GROUPMASTER.group_id = LEDGERS.Acc_groupid AND GROUPMASTER.group_cmpid = LEDGERS.Acc_cmpid AND GROUPMASTER.group_locationid = LEDGERS.Acc_locationid AND GROUPMASTER.group_yearid = LEDGERS.Acc_yearid LEFT OUTER JOIN REGISTERMASTER ON LEDGERS.ACC_REGISTERID = REGISTERMASTER.register_id AND LEDGERS.Acc_cmpid = REGISTERMASTER.register_cmpid AND LEDGERS.Acc_locationid = REGISTERMASTER.register_locationid AND LEDGERS.Acc_yearid = REGISTERMASTER.register_yearid ", " and acc_cmpname = '" & cmbname.Text.Trim & "' AND ACC_CMPID = " & CmpId & " AND ACC_LOCATIONID = " & Locationid & " AND ACC_YEARID = " & YearId & WHERECLAUSE)
                If dt.Rows.Count = 0 Then
                    Dim a As String = cmbname.Text.Trim
                    Dim tempmsg As Integer = MsgBox("Account not present, Add New?", MsgBoxStyle.YesNo, "PROCESS")
                    If tempmsg = vbYes Then
                        cmbname.Text = a
                        Dim objVendormaster As New AccountsMaster
                        objVendormaster.frmstring = "ACCOUNTS"
                        objVendormaster.tempAccountsName = cmbname.Text.Trim()
                        objVendormaster.TEMPGROUPNAME = GROUPNAME
                        objVendormaster.ShowDialog()
                        dt = OBJCMN.search("acc_add, REGISTER_NAME AS REGISTERNAME", "", " LEDGERS INNER JOIN GROUPMASTER ON GROUPMASTER.group_id = LEDGERS.Acc_groupid AND GROUPMASTER.group_cmpid = LEDGERS.Acc_cmpid AND GROUPMASTER.group_locationid = LEDGERS.Acc_locationid AND GROUPMASTER.group_yearid = LEDGERS.Acc_yearid LEFT OUTER JOIN REGISTERMASTER ON LEDGERS.ACC_REGISTERID = REGISTERMASTER.register_id AND LEDGERS.Acc_cmpid = REGISTERMASTER.register_cmpid AND LEDGERS.Acc_locationid = REGISTERMASTER.register_locationid AND LEDGERS.Acc_yearid = REGISTERMASTER.register_yearid ", " and acc_cmpname = '" & cmbname.Text.Trim & "' AND ACC_CMPID = " & CmpId & " AND ACC_LOCATIONID = " & Locationid & " AND ACC_YEARID = " & YearId & WHERECLAUSE)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As DataTable
                            dt1 = cmbname.DataSource
                            If cmbname.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(cmbname.Text.Trim)
                                    cmbname.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                    End If
                Else
                    txtadd.Text = dt.Rows(0).Item(0).ToString
                    CMBACCCODE.Text = dt.Rows(0).Item(1)
                End If
            End If
        Catch ex As Exception
            GoTo line1
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub FORMVALIDATE(ByRef cmbform As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try
            Cursor.Current = Cursors.WaitCursor
            If cmbform.Text.Trim <> "" Then
                uppercase(cmbform)
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search("FORM_NAME", "", "FORMTYPE", " and FORM_NAME = '" & cmbform.Text.Trim & "'")
                If dt.Rows.Count = 0 Then
                    Dim a As String = cmbform.Text.Trim
                    Dim tempmsg As Integer = MsgBox("FORM not present, Add New?", MsgBoxStyle.YesNo, "PROCESS")
                    If tempmsg = vbYes Then
                        cmbform.Text = a
                        Dim OBJFORM As New citymaster
                        OBJFORM.frmstring = "FORMTYPE"
                        OBJFORM.txtname.Text = cmbform.Text.Trim()
                        OBJFORM.ShowDialog()
                        dt = OBJCMN.search("FORM_name", "", "FORMTYPE", " and FORM_name = '" & cmbform.Text.Trim & "'")
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = cmbform.DataSource
                            If cmbform.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(cmbform.Text.Trim)
                                    cmbform.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub TAXvalidate(ByRef CMBTAX As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBTAX.Text.Trim <> "" Then
                uppercase(CMBTAX)
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search("TAX_NAME", "", "TAXMaster", " and TAX_NAME = '" & CMBTAX.Text.Trim & "' and TAX_cmpid = " & CmpId & " and TAX_Locationid = " & Locationid & " and TAX_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBTAX.Text.Trim
                    Dim tempmsg As Integer = MsgBox("TAX not present, Add New?", MsgBoxStyle.YesNo, "PROCESS")
                    If tempmsg = vbYes Then
                        CMBTAX.Text = a
                        Dim OBJTAX As New Taxmaster
                        OBJTAX.txtname.Text = CMBTAX.Text.Trim()
                        OBJTAX.ShowDialog()
                        dt = OBJCMN.search("TAX_name", "", "TAXMaster", " and TAX_name = '" & CMBTAX.Text.Trim & "' and TAX_cmpid = " & CmpId & " and TAX_Locationid = " & Locationid & " and TAX_Yearid = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = CMBTAX.DataSource
                            If CMBTAX.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(CMBTAX.Text.Trim)
                                    CMBTAX.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub FILLNATURE(ByRef CMBNATURE As ComboBox)
        Try
            Dim OBJCMN As New ClsCommon
            Dim dt As DataTable

            dt = OBJCMN.search(" PAY_name ", "", " NATUREOFPAYMENTMaster", "")
            If dt.Rows.Count > 0 Then
                dt.DefaultView.Sort = "PAY_name"
                CMBNATURE.DataSource = dt
                CMBNATURE.DisplayMember = "PAY_name"
                CMBNATURE.Text = ""
            End If
            CMBNATURE.SelectAll()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub NATUREVALIDATE(ByRef CMBNATURE As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBNATURE.Text.Trim <> "" Then
                uppercase(CMBNATURE)
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search("PAY_id", "", "NATUREOFPAYMENTMASTER", " and PAY_NAME = '" & CMBNATURE.Text.Trim & "'")
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("NATURE OF PAYMENT not present, Add New?", MsgBoxStyle.YesNo, "PROCESS")
                    If tempmsg = vbYes Then
                        Dim alParaval As New ArrayList

                        alParaval.Add(CMBNATURE.Text.Trim)
                        alParaval.Add("")
                        alParaval.Add(CmpId)
                        alParaval.Add(Locationid)
                        alParaval.Add(Userid)
                        alParaval.Add(YearId)
                        alParaval.Add(0)

                        Dim OBJNATUREOFPAYMENT As New ClsNatureOfPayment
                        OBJNATUREOFPAYMENT.alParaval = alParaval
                        Dim IntResult As Integer = OBJNATUREOFPAYMENT.SAVE()
                    Else
                        CMBNATURE.Focus()
                        CMBNATURE.SelectAll()
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub fillDEDUCTEETYPE(ByRef cmbDEDUCTEE As ComboBox)
        Try
            Dim OBJCMN As New ClsCommon
            Dim dt As DataTable

            dt = OBJCMN.search(" DEDUCTEETYPE_name ", "", " DEDUCTEETYPEMaster", "")
            If dt.Rows.Count > 0 Then
                dt.DefaultView.Sort = "DEDUCTEETYPE_name"
                cmbDEDUCTEE.DataSource = dt
                cmbDEDUCTEE.DisplayMember = "DEDUCTEETYPE_name"
                cmbDEDUCTEE.Text = ""
            End If
            cmbDEDUCTEE.SelectAll()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub DEDUCTEETYPEVALIDATE(ByRef CMBDEDUCTEETYPE As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBDEDUCTEETYPE.Text.Trim <> "" Then
                uppercase(CMBDEDUCTEETYPE)
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search("DEDUCTEETYPE_id", "", "DEDUCTEETYPEMASTER", " and DEDUCTEETYPE_NAME = '" & CMBDEDUCTEETYPE.Text.Trim & "'")
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("DEDUCTEETYPE not present, Add New?", MsgBoxStyle.YesNo, "PROCESS")
                    If tempmsg = vbYes Then
                        Dim alParaval As New ArrayList

                        alParaval.Add(CMBDEDUCTEETYPE.Text.Trim)
                        alParaval.Add("")
                        alParaval.Add(CmpId)
                        alParaval.Add(Locationid)
                        alParaval.Add(Userid)
                        alParaval.Add(YearId)
                        alParaval.Add(0)

                        Dim objclsDEDUCTEETYPE As New ClsDeducteetypeMaster
                        objclsDEDUCTEETYPE.alParaval = alParaval
                        Dim IntResult As Integer = objclsDEDUCTEETYPE.SAVE()
                    Else
                        CMBDEDUCTEETYPE.Focus()
                        CMBDEDUCTEETYPE.SelectAll()
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub QUALITYVALIDATE(ByRef CMBQUALITY As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form, Optional ByRef unit As String = "", Optional ByRef ProcessNAME As String = "")
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBQUALITY.Text.Trim <> "" Then
                uppercase(CMBQUALITY)
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search("   QUALITYMASTER.QUALITY_name AS QUALITY", "", "  QUALITYMASTER ", " and  QUALITYMASTER.QUALITY_name = '" & CMBQUALITY.Text.Trim & "' and QUALITYMASTER.QUALITY_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBQUALITY.Text.Trim
                    Dim tempmsg As Integer = MsgBox("Quality not present, Add New?", MsgBoxStyle.YesNo, CmpName)
                    If tempmsg = vbYes Then
                        CMBQUALITY.Text = a
                        Dim objqualitymaster As New QualityMaster
                        objqualitymaster.tempQualityName = CMBQUALITY.Text.Trim()

                        objqualitymaster.ShowDialog()
                        dt = OBJCMN.search("   QUALITYMASTER.QUALITY_name AS QUALITY ", "", "  QUALITYMASTER ", " and  QUALITYMASTER.QUALITY_name = '" & CMBQUALITY.Text.Trim & "' and QUALITYMASTER.QUALITY_Yearid = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = CMBQUALITY.DataSource
                            If CMBQUALITY.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(CMBQUALITY.Text.Trim)
                                    CMBQUALITY.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub
    Sub YARNCOMPOSITIONVALIDATE(ByRef CMBYARNCOMPOSITION As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form, Optional ByRef unit As String = "", Optional ByRef ProcessNAME As String = "")
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBYARNCOMPOSITION.Text.Trim <> "" Then
                uppercase(CMBYARNCOMPOSITION)
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search("   QUALITYMASTER.QUALITY_name AS QUALITY", "", "  QUALITYMASTER ", " and  QUALITYMASTER.QUALITY_name = '" & CMBYARNCOMPOSITION.Text.Trim & "' and QUALITYMASTER.QUALITY_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBYARNCOMPOSITION.Text.Trim
                    Dim tempmsg As Integer = MsgBox("Quality not present, Add New?", MsgBoxStyle.YesNo, CmpName)
                    If tempmsg = vbYes Then
                        CMBYARNCOMPOSITION.Text = a
                        Dim objqualitymaster As New QualityMaster
                        objqualitymaster.tempQualityName = CMBYARNCOMPOSITION.Text.Trim()

                        objqualitymaster.ShowDialog()
                        dt = OBJCMN.search("   QUALITYMASTER.QUALITY_name AS QUALITY ", "", "  QUALITYMASTER ", " and  QUALITYMASTER.QUALITY_name = '" & CMBYARNCOMPOSITION.Text.Trim & "' and QUALITYMASTER.QUALITY_Yearid = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = CMBYARNCOMPOSITION.DataSource
                            If CMBYARNCOMPOSITION.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(CMBYARNCOMPOSITION.Text.Trim)
                                    CMBYARNCOMPOSITION.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub PERVALIDATE(ByRef CMBPER As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBPER.Text.Trim <> "" Then
                uppercase(CMBPER)
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search("PER_id", "", "PERMaster", " and PER_AMT = '" & CMBPER.Text.Trim & "' and PER_YEARid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("Per not present, Add New?", MsgBoxStyle.YesNo, "PROCESS")
                    If tempmsg = vbYes Then
                        Dim alParaval As New ArrayList

                        alParaval.Add(CMBPER.Text.Trim)
                        alParaval.Add("")
                        alParaval.Add(CmpId)
                        alParaval.Add(Locationid)
                        alParaval.Add(Userid)
                        alParaval.Add(YearId)
                        alParaval.Add(0)

                        Dim objclsCITY As New ClsCityMaster
                        objclsCITY.alParaval = alParaval
                        Dim IntResult As Integer = objclsCITY.SAVEPER()
                    Else
                        CMBPER.Focus()
                        CMBPER.SelectAll()
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub FILLCOLOR(ByRef CMBCOLOR As ComboBox)
        Try
            If CMBCOLOR.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable

                dt = OBJCMN.search(" COLOR_name, COLOR_ID ", "", " COLORMaster", " AND COLOR_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "COLOR_name"
                    CMBCOLOR.DataSource = dt
                    CMBCOLOR.DisplayMember = "COLOR_name"
                    CMBCOLOR.ValueMember = "COLOR_ID"
                End If
                CMBCOLOR.SelectAll()
                CMBCOLOR.Text = ""
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub COLORVALIDATE(ByRef CMBCOLOR As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBCOLOR.Text.Trim <> "" Then
                uppercase(CMBCOLOR)
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search("   COLORMASTER.COLOR_name AS COLOR", "", "  COLORMASTER ", " and  COLORMASTER.COLOR_name = '" & CMBCOLOR.Text.Trim & "' and COLORMASTER.COLOR_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBCOLOR.Text.Trim
                    Dim tempmsg As Integer = MsgBox(" Shade not present, Add New?", MsgBoxStyle.YesNo, CmpName)
                    If tempmsg = vbYes Then
                        CMBCOLOR.Text = a
                        Dim objCOLORMASTER As New ColorMaster
                        objCOLORMASTER.TEMPNAME = CMBCOLOR.Text.Trim()

                        objCOLORMASTER.ShowDialog()
                        dt = OBJCMN.search(" COLORMASTER.COLOR_name AS COLOR ", "", "  COLORMASTER ", " and  COLORMASTER.COLOR_name = '" & CMBCOLOR.Text.Trim & "' and COLORMASTER.COLOR_Yearid = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = CMBCOLOR.DataSource
                            If CMBCOLOR.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(CMBCOLOR.Text.Trim)
                                    CMBCOLOR.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub fillPIECETYPE(ByRef CMBPIECETYPE As ComboBox)
        Try
            If CMBPIECETYPE.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search(" PIECETYPE_NAME ", "", " PIECETYPEMaster ", " And PIECETYPE_cmpid=" & CmpId & " And PIECETYPE_locationid = " & Locationid & " And PIECETYPE_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "PIECETYPE_NAME"
                    CMBPIECETYPE.DataSource = dt
                    CMBPIECETYPE.DisplayMember = "PIECETYPE_NAME"
                    CMBPIECETYPE.Text = ""
                End If
                CMBPIECETYPE.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PIECETYPEvalidate(ByRef cmbPIECETYPE As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try

            If cmbPIECETYPE.Text.Trim <> "" Then
                uppercase(cmbPIECETYPE)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search(" PIECETYPE_NAME ", "", "PIECETYPEMaster", " and PIECETYPE_NAME = '" & cmbPIECETYPE.Text.Trim & "' and PIECETYPE_cmpid = " & CmpId & " and PIECETYPE_locationid = " & Locationid & " and PIECETYPE_yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("PIECETYPE not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        Dim alParaval As New ArrayList

                        alParaval.Add(UCase(cmbPIECETYPE.Text.Trim))
                        alParaval.Add("")
                        alParaval.Add(CmpId)
                        alParaval.Add(Locationid)
                        alParaval.Add(Userid)
                        alParaval.Add(YearId)
                        alParaval.Add(0)

                        Dim objPIECETYPE As New ClsPieceTypeMaster
                        objPIECETYPE.alParaval = alParaval
                        Dim IntResult As Integer = objPIECETYPE.save()
                        'e.Cancel = True
                    Else
                        cmbPIECETYPE.Focus()
                        cmbPIECETYPE.SelectAll()
                        e.Cancel = True
                    End If
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CITYVALIDATE(ByRef CMBCITY As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBCITY.Text.Trim <> "" Then
                uppercase(CMBCITY)
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search("CITY_id", "", "CITYMaster", " and CITY_NAME = '" & CMBCITY.Text.Trim & "' and CITY_cmpid = " & CmpId & " and CITY_LOCATIONid = " & Locationid & " and CITY_YEARid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("CITY not present, Add New?", MsgBoxStyle.YesNo, "PROCESS")
                    If tempmsg = vbYes Then
                        Dim alParaval As New ArrayList

                        Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'CITY MASTER'")
                        If DTROW(0).Item(1) = False Then
                            MsgBox("Insufficient Rights")
                            Exit Sub
                        End If

                        alParaval.Add(CMBCITY.Text.Trim)
                        alParaval.Add("")
                        alParaval.Add(CmpId)
                        alParaval.Add(Locationid)
                        alParaval.Add(Userid)
                        alParaval.Add(YearId)
                        alParaval.Add(0)

                        Dim objclsCITY As New ClsCityMaster
                        objclsCITY.alParaval = alParaval
                        Dim IntResult As Integer = objclsCITY.save()
                    Else
                        CMBCITY.Focus()
                        CMBCITY.SelectAll()
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub fillunit(ByRef cmbunit As ComboBox)
        Try
            Cursor.Current = Cursors.WaitCursor
            If cmbunit.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search(" unit_abbr ", "", " UnitMaster ", " and unit_cmpid=" & CmpId & " and unit_Locationid=" & Locationid & " and unit_Yearid=" & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "unit_abbr"
                    cmbunit.DataSource = dt
                    cmbunit.DisplayMember = "unit_abbr"
                    cmbunit.Text = ""
                End If
                cmbunit.SelectAll()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub unitvalidate(ByRef cmbunit As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try

            Cursor.Current = Cursors.WaitCursor
            If cmbunit.Text.Trim <> "" Then
                lowercase(cmbunit)
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search(" unit_abbr ", "", "UnitMaster", " and unit_abbr = '" & cmbunit.Text.Trim & "' and unit_cmpid = " & CmpId & " and unit_Locationid = " & Locationid & " and unit_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = cmbunit.Text.Trim
                    Dim tempmsg As Integer = MsgBox("Unit not present, Add New?", MsgBoxStyle.YesNo, "PROCESS")
                    If tempmsg = vbYes Then
                        cmbunit.Text = a
                        Dim objunitmaster As New UnitMaster
                        objunitmaster.frmString = "UNIT"
                        objunitmaster.txtabbr.Text = cmbunit.Text.Trim()
                        objunitmaster.ShowDialog()
                        dt = OBJCMN.search(" unit_abbr ", "", "UnitMaster", " and unit_abbr = '" & cmbunit.Text.Trim & "' and unit_cmpid = " & CmpId & " and unit_Locationid = " & Locationid & " and unit_Yearid = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = cmbunit.DataSource
                            If cmbunit.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(cmbunit.Text.Trim)
                                    cmbunit.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                    End If
                End If

            End If
        Catch ex As Exception
            GoTo line1
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub ACCCODEVALIDATE(ByRef CMBCODE As ComboBox, ByVal CMBACCNAME As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form, ByRef TXTADD As System.Windows.Forms.TextBox, Optional ByVal WHERECLAUSE As String = "", Optional ByVal GROUPNAME As String = "")
        Try
            If CMBCODE.Text.Trim <> "" Then
                uppercase(CMBCODE)
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search("acc_CMPNAME, ACC_ADD", "", "Ledgers inner join groupmaster on groupmaster.group_id = ledgers.acc_groupid and groupmaster.group_cmpid = ledgers.acc_cmpid and groupmaster.group_locationid = ledgers.acc_locationid and groupmaster.group_yearid = ledgers.acc_yearid", " and acc_cODE = '" & CMBCODE.Text.Trim & "' and acc_cmpid = " & CmpId & " and acc_LOCATIONid = " & Locationid & " and acc_YEARid = " & YearId & WHERECLAUSE)
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("Ledger not present, Add New?", MsgBoxStyle.YesNo, "")
                    If tempmsg = vbYes Then
                        Dim objVendormaster As New AccountsMaster
                        objVendormaster.frmstring = "ACCOUNTS"
                        objVendormaster.tempAccountsCode = CMBCODE.Text.Trim()
                        objVendormaster.TEMPGROUPNAME = GROUPNAME
                        objVendormaster.ShowDialog()
                        dt = OBJCMN.search("ACC_CODE", "", "Ledgers inner join groupmaster on groupmaster.group_id = ledgers.acc_groupid and groupmaster.group_cmpid = ledgers.acc_cmpid and groupmaster.group_locationid = ledgers.acc_locationid and groupmaster.group_yearid = ledgers.acc_yearid", " and acc_cODE = '" & CMBCODE.Text.Trim & "' and acc_cmpid = " & CmpId & " and acc_LOCATIONid = " & Locationid & " and acc_YEARid = " & YearId & WHERECLAUSE)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As DataTable
                            Dim a As String = CMBCODE.Text.Trim
                            dt1 = CMBCODE.DataSource
                            If CMBCODE.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(CMBCODE.Text.Trim)
                                    CMBCODE.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                        Exit Sub
                    End If
                Else
                    CMBACCNAME.Text = dt.Rows(0).Item(0)
                    TXTADD.Text = dt.Rows(0).Item(1)
                End If
            End If
        Catch ex As Exception
            GoTo line1
            Throw ex
        End Try
    End Sub

    Sub fillGODOWN(ByRef CMBGODOWN As ComboBox, ByRef edit As Boolean, Optional ByVal WHERECLAUSE As String = "")
        Try
            If CMBGODOWN.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search(" GODOWN_name, GODOWN_ID ", "", " GODOWNMaster", WHERECLAUSE & " AND GODOWN_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "GODOWN_name"
                    CMBGODOWN.DataSource = dt
                    CMBGODOWN.DisplayMember = "GODOWN_name"
                    CMBGODOWN.ValueMember = "GODOWN_ID"
                    If edit = False Then CMBGODOWN.Text = ""
                End If
                CMBGODOWN.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLDELIVERYAT(ByRef CMBDELIVERYAT As ComboBox, ByRef EDIT As Boolean)
        Try
            If CMBDELIVERYAT.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search("  DELIVERYAT_NAME ", "", " DELIVERYATMASTER", " and DELIVERYAT_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "DELIVERYAT_NAME"
                    CMBDELIVERYAT.DataSource = dt
                    CMBDELIVERYAT.DisplayMember = "DELIVERYAT_NAME"
                    If EDIT = False Then CMBDELIVERYAT.Text = ""
                End If
                CMBDELIVERYAT.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillBEAM(ByRef CMBBEAM As ComboBox, ByRef edit As Boolean)
        Try
            If CMBBEAM.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search(" BEAM_name ", "", " BEAMMaster", " AND BEAM_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "BEAM_name"
                    CMBBEAM.DataSource = dt
                    CMBBEAM.DisplayMember = "BEAM_name"
                    CMBBEAM.Text = ""
                End If
                CMBBEAM.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub BEAMVALIDATE(ByRef CMBBEAM As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBBEAM.Text.Trim <> "" Then
                uppercase(CMBBEAM)
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search("BEAM_NAME", "", "BEAMMASTER", " and BEAM_NAME = '" & CMBBEAM.Text.Trim & "' and BEAM_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBBEAM.Text.Trim
                    Dim tempmsg As Integer = MsgBox("Beam Name not present, Add New?", MsgBoxStyle.YesNo, "PROCESS")
                    If tempmsg = vbYes Then
                        CMBBEAM.Text = a
                        Dim OBJBEAM As New BeamMaster
                        OBJBEAM.TEMPBEAMNAME = CMBBEAM.Text.Trim()
                        OBJBEAM.ShowDialog()
                        dt = OBJCMN.search("BEAM_name", "", "BEAMMaster", " and BEAM_name = '" & CMBBEAM.Text.Trim & "' and BEAM_Yearid = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = CMBBEAM.DataSource
                            If CMBBEAM.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(CMBBEAM.Text.Trim)
                                    CMBBEAM.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub GODOWNVALIDATE(ByRef CMBGODOWN As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form, Optional ByVal WHERECLAUSE As String = "")
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBGODOWN.Text.Trim <> "" Then
                uppercase(CMBGODOWN)
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search("GODOWN_NAME", "", "GODOWNMaster", WHERECLAUSE & " and GODOWN_NAME = '" & CMBGODOWN.Text.Trim & "' and GODOWN_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBGODOWN.Text.Trim
                    Dim tempmsg As Integer = MsgBox("GODOWN not present, Add New?", MsgBoxStyle.YesNo, "PROCESS")
                    If tempmsg = vbYes Then
                        CMBGODOWN.Text = a
                        Dim OBJGODOWN As New GodownMaster
                        OBJGODOWN.TEMPGODOWN = CMBGODOWN.Text.Trim()
                        OBJGODOWN.ShowDialog()
                        dt = OBJCMN.search("GODOWN_name", "", "GODOWNMaster", WHERECLAUSE & " and GODOWN_name = '" & CMBGODOWN.Text.Trim & "' and GODOWN_Yearid = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = CMBGODOWN.DataSource
                            If CMBGODOWN.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(CMBGODOWN.Text.Trim)
                                    CMBGODOWN.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub FILLLOOMNO(ByRef CMBLOOMNO As ComboBox, ByVal WEAVERNAME As String, ByRef edit As Boolean, Optional ByVal WHERECLAUSE As String = "")
        Try
            If CMBLOOMNO.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable = OBJCMN.search("  LOOMMASTER_DESC.LOOM_NO AS LOOMNO ", "", "  LOOMMASTER INNER JOIN LOOMMASTER_DESC ON LOOMMASTER.LOOM_ID = LOOMMASTER_DESC.LOOM_ID INNER JOIN LEDGERS ON LOOMMASTER.LOOM_WEAVERID = LEDGERS.Acc_id", WHERECLAUSE & " AND LEDGERS.ACC_CMPNAME = '" & WEAVERNAME & "' AND LOOM_YEARID = " & YearId & " ORDER BY CAST(LOOM_NO AS INT)")
                If dt.Rows.Count > 0 Then
                    CMBLOOMNO.DataSource = dt
                    CMBLOOMNO.DisplayMember = "LOOMNO"
                    CMBLOOMNO.Text = ""
                End If
                CMBLOOMNO.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub LOOMVALIDATE(ByRef CMBLOOMNO As ComboBox, ByVal WEAVERNAME As String, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form, Optional ByVal WHERECLAUSE As String = "")
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBLOOMNO.Text.Trim <> "" Then
                uppercase(CMBLOOMNO)
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable = OBJCMN.search("  LOOMMASTER_DESC.LOOM_NO AS LOOMNO ", "", "  LOOMMASTER INNER JOIN LOOMMASTER_DESC ON LOOMMASTER.LOOM_ID = LOOMMASTER_DESC.LOOM_ID INNER JOIN LEDGERS ON LOOMMASTER.LOOM_WEAVERID = LEDGERS.Acc_id", WHERECLAUSE & " AND LEDGERS.ACC_CMPNAME = '" & WEAVERNAME & "' AND LOOM_NO = '" & CMBLOOMNO.Text.Trim & "' AND LOOM_YEARID = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBLOOMNO.Text.Trim
                    Dim tempmsg As Integer = MsgBox("Loom No not present for selected Weaver, Add New?", MsgBoxStyle.YesNo, "PROCESS")
                    If tempmsg = vbYes Then
                        CMBLOOMNO.Text = a
                        Dim OBJBEAM As New LoomMaster
                        OBJBEAM.WEAVERNAME = WEAVERNAME
                        OBJBEAM.ShowDialog()
                        dt = OBJCMN.search("  LOOMMASTER_DESC.LOOM_NO AS LOOMNO ", "", "  LOOMMASTER INNER JOIN LOOMMASTER_DESC ON LOOMMASTER.LOOM_ID = LOOMMASTER_DESC.LOOM_ID INNER JOIN LEDGERS ON LOOMMASTER.LOOM_WEAVERID = LEDGERS.Acc_id", WHERECLAUSE & " AND LEDGERS.ACC_CMPNAME = '" & WEAVERNAME & "' AND LOOM_NO = '" & CMBLOOMNO.Text.Trim & "' AND LOOM_YEARID = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As DataTable = CMBLOOMNO.DataSource
                            If CMBLOOMNO.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(CMBLOOMNO.Text.Trim)
                                    CMBLOOMNO.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub FILLGREY(ByRef CMBGREY As ComboBox, ByRef edit As Boolean, Optional ByVal WHERECLAUSE As String = "")
        Try
            If CMBGREY.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search(" GREY_name ", "", " GREYQUALITYMASTER ", WHERECLAUSE & " AND GREY_YEARID = " & YearId)
                CMBGREY.DataSource = dt
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "GREY_name"
                    CMBGREY.DisplayMember = "GREY_name"
                    If edit = False Then CMBGREY.Text = ""
                End If
                CMBGREY.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GREYVALIDATE(ByRef CMBGREY As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form, Optional ByVal WHERECLAUSE As String = "")
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBGREY.Text.Trim <> "" Then
                uppercase(CMBGREY)
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search("GREY_NAME", "", "GREYQUALITYMASTER", WHERECLAUSE & " and GREY_NAME = '" & CMBGREY.Text.Trim & "' and GREY_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBGREY.Text.Trim
                    Dim tempmsg As Integer = MsgBox("Item Name not present, Add New?", MsgBoxStyle.YesNo, "PROCESS")
                    If tempmsg = vbYes Then
                        CMBGREY.Text = a
                        Dim OBJBEAM As New GreyQualityMaster
                        OBJBEAM.TEMPQUALITYNAME = CMBGREY.Text.Trim()
                        OBJBEAM.ShowDialog()
                        dt = OBJCMN.search("GREY_name", "", "GREYQUALITYMASTER", WHERECLAUSE & " and GREY_name = '" & CMBGREY.Text.Trim & "' and GREY_Yearid = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = CMBGREY.DataSource
                            If CMBGREY.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(CMBGREY.Text.Trim)
                                    CMBGREY.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub FILLBANK(ByRef CMBBANK As ComboBox)
        Try
            If CMBBANK.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search(" TDSCHA_BANKNAME ", "", " TDSCHALLAN ", " and TDSCHA_CMPID=" & CmpId & " and TDSCHA_LOCATIONID = " & Locationid & " and TDSCHA_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "TDSCHA_BANKNAME"
                    CMBBANK.DataSource = dt
                    CMBBANK.DisplayMember = "TDSCHA_BANKNAME"
                    CMBBANK.Text = ""
                End If
                CMBBANK.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillTYPE(ByRef CMBTYPE As ComboBox)
        Try
            If CMBTYPE.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search(" TYPE_NAME ", "", " TYPEMaster ", " and TYPE_cmpid=" & CmpId & " and TYPE_locationid = " & Locationid & " and TYPE_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "TYPE_NAME"
                    CMBTYPE.DataSource = dt
                    CMBTYPE.DisplayMember = "TYPE_NAME"
                    CMBTYPE.Text = ""
                End If
                CMBTYPE.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillDISTRICT(ByRef CMBDISTRICT As ComboBox)
        Try
            If CMBDISTRICT.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search(" DISTRICT_NAME ", "", " DISTRICTMaster ", " and DISTRICT_cmpid=" & CmpId & " and DISTRICT_locationid = " & Locationid & " and DISTRICT_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "DISTRICT_NAME"
                    CMBDISTRICT.DataSource = dt
                    CMBDISTRICT.DisplayMember = "DISTRICT_NAME"
                    CMBDISTRICT.Text = ""
                End If
                CMBDISTRICT.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillVIA(ByRef CMBVIA As ComboBox)
        Try
            If CMBVIA.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search(" VIA_NAME ", "", " VIAMaster ", " and VIA_cmpid=" & CmpId & " and VIA_locationid = " & Locationid & " and VIA_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "VIA_NAME"
                    CMBVIA.DataSource = dt
                    CMBVIA.DisplayMember = "VIA_NAME"
                    CMBVIA.Text = ""
                End If
                CMBVIA.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillPARTYBANK(ByRef CMBPARTYBANK As ComboBox, ByRef edit As Boolean)
        Try
            If CMBPARTYBANK.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                Dim WHERECLAUSE As String = ""
                dt = OBJCMN.search(" PARTYBANK_name ", "", " PARTYBANKMaster", WHERECLAUSE & " and PARTYBANK_cmpid=" & CmpId & " AND PARTYBANK_Locationid=" & Locationid & " AND PARTYBANK_Yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "PARTYBANK_name"
                    CMBPARTYBANK.DataSource = dt
                    CMBPARTYBANK.DisplayMember = "PARTYBANK_name"
                    If edit = False Then CMBPARTYBANK.Text = ""
                End If
                CMBPARTYBANK.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub TYPEvalidate(ByRef cmbTYPE As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try

            If cmbTYPE.Text.Trim <> "" Then
                uppercase(cmbTYPE)
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search(" TYPE_NAME ", "", "TYPEMaster", " and TYPE_NAME = '" & cmbTYPE.Text.Trim & "' and TYPE_cmpid = " & CmpId & " and TYPE_locationid = " & Locationid & " and TYPE_yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("TYPE not present, Add New?", MsgBoxStyle.YesNo, "PROCESS")
                    If tempmsg = vbYes Then
                        Dim alParaval As New ArrayList

                        Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'TYPE MASTER'")
                        If DTROW(0).Item(1) = False Then
                            MsgBox("Insufficient Rights")
                            Exit Sub
                        End If

                        alParaval.Add(cmbTYPE.Text.Trim)
                        alParaval.Add("")
                        alParaval.Add(CmpId)
                        alParaval.Add(Locationid)
                        alParaval.Add(Userid)
                        alParaval.Add(YearId)
                        alParaval.Add(0)

                        Dim objPIECETYPE As New ClsTypeMaster
                        objPIECETYPE.alParaval = alParaval
                        Dim IntResult As Integer = objPIECETYPE.save()
                        'e.Cancel = True
                    Else
                        cmbTYPE.Focus()
                        cmbTYPE.SelectAll()
                        e.Cancel = True
                    End If
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub DISTRICTvalidate(ByRef cmbDISTRICT As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try

            If cmbDISTRICT.Text.Trim <> "" Then
                uppercase(cmbDISTRICT)
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search(" DISTRICT_NAME ", "", "DISTRICTMaster", " and DISTRICT_NAME = '" & cmbDISTRICT.Text.Trim & "' and DISTRICT_cmpid = " & CmpId & " and DISTRICT_locationid = " & Locationid & " and DISTRICT_yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("DISTRICT not present, Add New?", MsgBoxStyle.YesNo, "PROCESS")
                    If tempmsg = vbYes Then
                        Dim alParaval As New ArrayList

                        Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'DISTRICT MASTER'")
                        If DTROW(0).Item(1) = False Then
                            MsgBox("Insufficient Rights")
                            Exit Sub
                        End If

                        alParaval.Add(cmbDISTRICT.Text.Trim)
                        alParaval.Add("")
                        alParaval.Add(CmpId)
                        alParaval.Add(Locationid)
                        alParaval.Add(Userid)
                        alParaval.Add(YearId)
                        alParaval.Add(0)

                        Dim objPIECETYPE As New ClsDistrictMaster
                        objPIECETYPE.alParaval = alParaval
                        Dim IntResult As Integer = objPIECETYPE.save()
                        'e.Cancel = True
                    Else
                        cmbDISTRICT.Focus()
                        cmbDISTRICT.SelectAll()
                        e.Cancel = True
                    End If
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub VIAvalidate(ByRef cmbVIA As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try

            If cmbVIA.Text.Trim <> "" Then
                uppercase(cmbVIA)
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search(" VIA_NAME ", "", "VIAMaster", " and VIA_NAME = '" & cmbVIA.Text.Trim & "' and VIA_cmpid = " & CmpId & " and VIA_locationid = " & Locationid & " and VIA_yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("VIA not present, Add New?", MsgBoxStyle.YesNo, "PROCESS")
                    If tempmsg = vbYes Then
                        Dim alParaval As New ArrayList

                        Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'VIA MASTER'")
                        If DTROW(0).Item(1) = False Then
                            MsgBox("Insufficient Rights")
                            Exit Sub
                        End If

                        alParaval.Add(cmbVIA.Text.Trim)
                        alParaval.Add("")
                        alParaval.Add(CmpId)
                        alParaval.Add(Locationid)
                        alParaval.Add(Userid)
                        alParaval.Add(YearId)
                        alParaval.Add(0)

                        Dim objPIECETYPE As New ClsDistrictMaster
                        objPIECETYPE.alParaval = alParaval
                        Dim IntResult As Integer = objPIECETYPE.saveVIA()
                        'e.Cancel = True
                    Else
                        cmbVIA.Focus()
                        cmbVIA.SelectAll()
                        e.Cancel = True
                    End If
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PARTYBANKvalidate(ByRef CMBPARTYBANK As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try

            If CMBPARTYBANK.Text.Trim <> "" Then
                uppercase(CMBPARTYBANK)
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search(" PARTYBANK_name ", "", "PARTYBANKMaster", " and PARTYBANK_name = '" & CMBPARTYBANK.Text.Trim & "' and PARTYBANK_cmpid = " & CmpId & " and PARTYBANK_locationid = " & Locationid & " and PARTYBANK_yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("PARTYBANK Name not present, Add New?", MsgBoxStyle.YesNo, "PROCESS")
                    If tempmsg = vbYes Then

                        Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'PARTYBANK MASTER'")
                        If DTROW(0).Item(1) = False Then
                            MsgBox("Insufficient Rights")
                            Exit Sub
                        End If

                        Dim alParaval As New ArrayList

                        alParaval.Add(CMBPARTYBANK.Text.Trim)
                        alParaval.Add("")
                        alParaval.Add(CmpId)
                        alParaval.Add(Locationid)
                        alParaval.Add(Userid)
                        alParaval.Add(YearId)
                        alParaval.Add(0)

                        Dim objPIECETYPE As New ClsPARTYBANKMaster
                        objPIECETYPE.alParaval = alParaval
                        Dim IntResult As Integer = objPIECETYPE.save()
                        'e.Cancel = True
                    Else
                        CMBPARTYBANK.Focus()
                        CMBPARTYBANK.SelectAll()
                        e.Cancel = True
                    End If
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillUSER(ByRef CMBUSER As ComboBox, Optional ByVal CONDITION As String = "")
        Try
            Dim OBJCMN As New ClsCommon
            Dim dt As DataTable

            dt = OBJCMN.search(" DISTINCT User_Name as [UserName]", "", "USERMASTER", " and USERMASTER.USER_cmpid= " & CmpId & " ORDER BY USER_NAME ")
            If dt.Rows.Count > 0 Then
                dt.DefaultView.Sort = "USERNAME"
                CMBUSER.DataSource = dt
                CMBUSER.DisplayMember = "USERNAME"
                CMBUSER.Text = ""
            End If
            CMBUSER.SelectAll()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function GETDEFAULTGODOWN() As String
        Try
            Dim clscommon As New ClsCommon
            Dim dt As DataTable
            dt = clscommon.search(" GODOWN_NAME AS GODOWNNAME ", "", " GODOWNMASTER ", " and GODOWN_ISDEFAULT = 'True' and GODOWN_YEARID = " & YearId)
            If dt.Rows.Count > 0 Then Return dt.Rows(0).Item("GODOWNNAME") Else Return ""
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Sub FILLMACHINE(ByRef CMBMACHINE As ComboBox)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBMACHINE.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable

                dt = objclscommon.search(" MACHINE_ID AS ID , MACHINE_NAME AS NAME ", "", " MACHINEMASTER ", " And MACHINE_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "NAME"
                    CMBMACHINE.DataSource = dt
                    CMBMACHINE.DisplayMember = "NAME"
                    CMBMACHINE.ValueMember = "ID"
                    CMBMACHINE.SelectedItem = Nothing
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub MACHINEVALIDATE(ByRef CMBMACHINE As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try

            If CMBMACHINE.Text.Trim <> "" Then
                uppercase(CMBMACHINE)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search(" MACHINE_NAME ", "", "MACHINEMASTER", " and MACHINE_NAME = '" & CMBMACHINE.Text.Trim & "' and MACHINE_yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("Machine not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        Dim a As String = CMBMACHINE.Text.Trim
                        Dim alParaval As New ArrayList

                        alParaval.Add(CMBMACHINE.Text.Trim)
                        alParaval.Add("")
                        alParaval.Add(CmpId)
                        alParaval.Add(Userid)
                        alParaval.Add(YearId)


                        Dim objRACK As New ClsMachineMaster
                        objRACK.alParaval = alParaval
                        Dim IntResult As Integer = objRACK.SAVE()


                        dt = objclscommon.search(" MACHINE_ID AS ID, MACHINE_NAME AS NAME ", "", "MACHINEMASTER", " and MACHINE_NAME = '" & CMBMACHINE.Text.Trim & "' and MACHINE_yearid = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = CMBMACHINE.DataSource
                            If CMBMACHINE.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(dt.Rows(0).Item("ID"), CMBMACHINE.Text.Trim)
                                    CMBMACHINE.Text = a
                                End If
                            End If
                        End If

                    Else
                        CMBMACHINE.Focus()
                        CMBMACHINE.SelectAll()
                        e.Cancel = True
                    End If
                End If

            End If
        Catch ex As Exception
            GoTo line1
            Throw ex
        End Try
    End Sub


#Region "FUNCTION FOR EMAIL"

    Sub sendemail(ByVal toMail As String, ByVal tempattachment As String, ByVal mailbody As String, ByVal subject As String, Optional ByVal ALATTACHMENT As ArrayList = Nothing, Optional ByVal NOOFATTACHMENTS As Integer = 0, Optional ByVal TEMPATTACHMENT1 As String = "", Optional ByVal TEMPATTACHMENT2 As String = "", Optional ByVal TEMPATTACHMENT3 As String = "", Optional ByVal TEMPATTACHMENT4 As String = "", Optional ByVal TEMPATTACHMENT5 As String = "", Optional ByVal TEMPATTACHMENT6 As String = "")

        'Dim mailBody As String
        Try
            Cursor.Current = Cursors.WaitCursor

            'create the mail message
            Dim mail As New MailMessage
            Dim MAILATTACHMENT As Attachment

            'set the addresses
            'mail.From = New MailAddress("siddhivinayaksynthetics@gmail.com", CmpName)
            'mail.From = New MailAddress("gulkitjain@gmail.com", "TexPro V1.0")

            mail.To.Add(toMail)

            '''' GIVING ISSUE IN DIRECT MULTIPLE PRINT IN INVOICE.
            ''set the content
            'mail.Subject = subject
            'mail.Body = mailbody
            'mail.IsBodyHtml = True
            'MAILATTACHMENT = New Attachment(tempattachment)
            'mail.Attachments.Add(MAILATTACHMENT)

            'If TEMPATTACHMENT1 <> "" Then
            '    MAILATTACHMENT = New Attachment(TEMPATTACHMENT1)
            '    mail.Attachments.Add(MAILATTACHMENT)
            'End If

            'If TEMPATTACHMENT2 <> "" Then
            '    MAILATTACHMENT = New Attachment(TEMPATTACHMENT2)
            '    mail.Attachments.Add(MAILATTACHMENT)
            'End If

            'If TEMPATTACHMENT3 <> "" Then
            '    MAILATTACHMENT = New Attachment(TEMPATTACHMENT3)
            '    mail.Attachments.Add(MAILATTACHMENT)
            'End If

            'If TEMPATTACHMENT4 <> "" Then
            '    MAILATTACHMENT = New Attachment(TEMPATTACHMENT4)
            '    mail.Attachments.Add(MAILATTACHMENT)
            'End If


            'If TEMPATTACHMENT5 <> "" Then
            '    MAILATTACHMENT = New Attachment(TEMPATTACHMENT5)
            '    mail.Attachments.Add(MAILATTACHMENT)
            'End If

            'If TEMPATTACHMENT6 <> "" Then
            '    MAILATTACHMENT = New Attachment(TEMPATTACHMENT6)
            '    mail.Attachments.Add(MAILATTACHMENT)
            'End If

            'set the content
            mail.Subject = subject
            mail.Body = mailbody
            mail.IsBodyHtml = True
            If NOOFATTACHMENTS <= 1 Then
                MAILATTACHMENT = New Attachment(tempattachment)
                mail.Attachments.Add(MAILATTACHMENT)
            Else
                For I As Integer = 0 To NOOFATTACHMENTS - 1
                    MAILATTACHMENT = New Attachment(ALATTACHMENT(I))
                    mail.Attachments.Add(MAILATTACHMENT)
                Next
            End If


            'send the message
            Dim smtp As New SmtpClient

            'set username and password
            Dim nc As New System.Net.NetworkCredential


            'GET SMTP, EMAILADD AND PASSWORD FROM USERMASTER
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.search("USER_SMTP AS SMTP, USER_SMTPEMAIL AS EMAIL, USER_SMTPPASS AS PASS", "", " USERMASTER", " AND USER_NAME = '" & UserName & "' and USER_CMPID = " & CmpId)
            If DT.Rows.Count > 0 Then
                If DT.Rows(0).Item("SMTP") = "" Then smtp.Host = "smtp.gmail.com" Else smtp.Host = DT.Rows(0).Item("SMTP")
                'smtp.Port = (25)
                smtp.Port = (587)


                smtp.EnableSsl = True
                mail.From = New MailAddress(DT.Rows(0).Item("EMAIL"), CmpName)
                nc.UserName = DT.Rows(0).Item("EMAIL")
                nc.Password = DT.Rows(0).Item("PASS") '"qhokuzymfmcxtoge"

            Else

                smtp.Host = "smtp.gmail.com"
                'smtp.Port = (25)
                smtp.Port = (587)
                smtp.EnableSsl = True

                mail.From = New MailAddress("noreply.textrade@gmail.com", CmpName)
                nc.UserName = "noreply.textrade@gmail.com"
                nc.Password = "qhokuzymfmcxtoge"

            End If


            'smtp.Timeout = 20000
            smtp.Timeout = 50000

            smtp.Credentials = nc

            smtp.Send(mail)
            mail.Dispose()

        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

#End Region

    Function SENDMSG(ByVal msg As String, ByVal MOBILENO As String) As String
        'Try
        '    Dim objSMS As New routesmsdll.SMS
        '    If MOBILENO <> "" Then objSMS.MobileNo = MOBILENO

        '    Dim OBJCMN As New ClsCommon
        '    Dim DT As New DataTable
        '    'If GUESTCATEGORY <> "" Then
        '    '    'SEND SAME MAIL TO ALL THE GUEST IN THIS CATEGORY
        '    '    DT = OBJCMN.search("ISNULL(GUEST_MOBILENO,'') AS MOBILENO ", "", " GUESTMASTER INNER JOIN GUESTCATEGORYMASTER ON GUEST_GUESTCATEGORYID = CATEGORY_ID", " AND ISNULL(GUEST_MOBILENO,'') <> ''  AND GUEST_YEARID = " & YearId)
        '    '    If DT.Rows.Count > 0 Then
        '    '        For Each DTROW As DataRow In DT.Rows
        '    '            If objSMS.MobileNo = "" Then
        '    '                objSMS.MobileNo = DTROW("MOBILENO")
        '    '            Else
        '    '                objSMS.MobileNo = objSMS.MobileNo & "," & DTROW("MOBILENO")
        '    '            End If
        '    '        Next
        '    '    End If
        '    'End If

        '    'If GROUPDEP <> "" Then
        '    '    'SEND SAME MAIL TO ALL THE GUEST IN THIS GROUPDEP
        '    '    DT = OBJCMN.search("ISNULL(GUEST_MOBILENO,'') AS MOBILENO ", "", " GROUPDEPARTURE INNER JOIN HOLIDAYPACKAGEMASTER ON GROUPDEPARTURE.GROUPDEP_NO = HOLIDAYPACKAGEMASTER.BOOKING_GROUPDEPARTID AND GROUPDEPARTURE.GROUPDEP_YEARID = HOLIDAYPACKAGEMASTER.BOOKING_YEARID INNER JOIN GUESTMASTER ON HOLIDAYPACKAGEMASTER.BOOKING_GUESTID = GUESTMASTER.GUEST_ID ", " AND ISNULL(GUEST_MOBILENO,'') <> '' AND GROUPDEP_NAME = '" & GROUPDEP & "' AND BOOKING_YEARID = " & YearId)
        '    '    If DT.Rows.Count > 0 Then
        '    '        For Each DTROW As DataRow In DT.Rows
        '    '            If objSMS.MobileNo = "" Then
        '    '                objSMS.MobileNo = DTROW("MOBILENO")
        '    '            Else
        '    '                objSMS.MobileNo = objSMS.MobileNo & "," & DTROW("MOBILENO")
        '    '            End If
        '    '        Next
        '    '    End If
        '    'End If


        '    'If ClientName = "RAMKRISHNA" Then
        '    '    objSMS.UserName = "nako-ramkrishna"
        '    '    objSMS.Password = "shreeram"
        '    '    objSMS.Sender = "SRKTRV"
        '    'ElseIf ClientName = "KHANNA" Then
        '    '    objSMS.UserName = "nako-khanna"
        '    '    objSMS.Password = "khanna12"
        '    '    objSMS.Sender = "KHANNA"
        '    'ElseIf ClientName = "APSARA" Then
        '    '    objSMS.UserName = "nako-apsara"
        '    '    objSMS.Password = "apsara12"
        '    '    objSMS.Sender = "APSARA"
        '    'ElseIf ClientName = "KPNT" Then
        '    '    objSMS.UserName = "nako-national"
        '    '    objSMS.Password = "national"
        '    '    objSMS.Sender = "NATTRV"
        '    'End If


        '    objSMS.Message = msg
        '    objSMS.IpAddress = "103.16.101.52"
        '    objSMS.dlr = 1
        '    objSMS.MessageType = routesmsdll.MESSAGE_TYPE.mTEXT
        '    Dim response As String = objSMS.sendMessage()
        '    Return (response.ToString.Substring(0, 4))

        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Function

    Function checkrowlinedel(ByVal gridsrno As Integer, ByVal txtno As TextBox) As Boolean
        Dim bln As Boolean = True
        If gridsrno = Val(txtno.Text.Trim) Then
            bln = False
        End If
        Return bln
    End Function

    Sub commakeypress(ByVal han As KeyPressEventArgs, ByVal sen As Control, ByVal frm As System.Windows.Forms.Form)
        If AscW(han.KeyChar) = 44 Then
            han.KeyChar = ""
        End If
    End Sub

#Region "WHATSAPP"

    Function CHECKWHASTAPPEXP() As Boolean
        Dim BLN As Boolean = True
        If Now.Date > WHATSAPPEXPDATE Then
            BLN = False
        End If
        Return BLN
    End Function

    Function GETWHATSAPPBASEURL() As String
        Dim WHATSAPPBASEURL As String = ""

        'READ BASEURL FROM C DRIVE
        If File.Exists("C:\WHATSAPPBASEURL.txt") Then
            Dim oRead As System.IO.StreamReader = File.OpenText("C:\WHATSAPPBASEURL.txt")
            WHATSAPPBASEURL = oRead.ReadToEnd
        End If
        Return WHATSAPPBASEURL
    End Function

    Async Function SENDWHATSAPPATTACHMENT(WHATSAPPNO As String, PATH As String, FILENAME As String) As Threading.Tasks.Task(Of String)
        Dim RESPONSE As String = ""
        Dim waMediaMsgBody As SendMediaMsgJson = New SendMediaMsgJson()
        Dim Attachment As String = Convert.ToBase64String(File.ReadAllBytes(PATH))
        Dim AttachmentFileName As String = FILENAME
        waMediaMsgBody.base64data = Attachment
        waMediaMsgBody.mimeType = MimeMapping.GetMimeMapping(AttachmentFileName)
        'waMediaMsgBody.caption = "APIMethod SendMediaMessage from CISPLWhatsAppAPI.dll"
        waMediaMsgBody.filename = AttachmentFileName
        Dim txnResp As TxnRespWithSendMessageDtls = Await APIMethods.SendMediaMessageAsync(WHATSAPPNO, waMediaMsgBody)
        RESPONSE = JsonConvert.SerializeObject(txnResp, Formatting.Indented)

        Return RESPONSE
    End Function

    Async Function SENDWHATSAPPMESSAGE(WHATSAPPNO As String, TEXTMESSAGE As String) As Threading.Tasks.Task(Of String)
        Dim RESPONSE As String = ""
        Dim Body As SendTextMsgJson = New SendTextMsgJson()
        Body.text = TEXTMESSAGE
        Dim txnResp As TxnRespWithSendMessageDtls = Await APIMethods.SendTextMessageAsync(WHATSAPPNO, Body)
        RESPONSE = JsonConvert.SerializeObject(txnResp, Formatting.Indented)
        Return RESPONSE
    End Function

    Async Function CHECKMOBILECONNECTSTATUS() As Threading.Tasks.Task(Of String)
        Dim RESPONSE As String = ""
        Dim txnResp As TxnRespWithConnectionState = Await APIMethods.GetConnectionStateAsync()
        RESPONSE = JsonConvert.SerializeObject(txnResp, Formatting.Indented)
        Return RESPONSE
    End Function

#End Region



End Module
