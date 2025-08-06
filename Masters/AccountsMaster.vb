
Imports BL
Imports System.Windows.Forms

Public Class AccountsMaster

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public frmstring As String        'Used from Displaying Customer, Vendor, Employee Master
    Public edit As Boolean
    Public tempAccountsName As String
    Public TEMPGROUPNAME As String
    Public tempAccountsCode As String
    Public tempTYPE As String = "ACCOUNTS"
    Public TEMPSUBTYPE As String = "ACCOUNTS"
    Public tempISAGENT As String = ""
    Public tempISTRANS As String = ""
    Dim tempAccountId As Integer

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub AccountsMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Sub FILLCMPNAME()
        Try
            Dim OBJCMN As New ClsCommonMaster
            Dim dt As DataTable

            If frmstring = "CUSTOMER" Then

                dt = OBJCMN.search("customer_cmpname", "", " CUSTOMERMASTER ", " and CUSTOMER_cmpid = " & CmpId & " and CUSTOMER_locationid = " & Locationid & " and CUSTOMER_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "customer_cmpname"
                    cmbcmpname.DataSource = dt
                    cmbcmpname.DisplayMember = "customer_cmpname"
                    cmbcmpname.Text = tempAccountsName
                End If

            ElseIf frmstring = "VENDOR" Then

                dt = OBJCMN.search("VENDOR_cmpname", "", " VENDORMaster ", " and VENDOR_cmpid = " & CmpId & " and VENDOR_locationid = " & Locationid & " and VENDOR_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "VENDOR_cmpname"
                    cmbcmpname.DataSource = dt
                    cmbcmpname.DisplayMember = "VENDOR_cmpname"
                    cmbcmpname.Text = tempAccountsName
                End If

            ElseIf frmstring = "ACCOUNTS" Then

                dt = OBJCMN.search("acc_cmpname", "", " ACCOUNTSMaster ", " and acc_cmpid = " & CmpId & " and acc_locationid = " & Locationid & " and acc_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "acc_cmpname"
                    cmbcmpname.DataSource = dt
                    cmbcmpname.DisplayMember = "acc_cmpname"
                    cmbcmpname.Text = tempAccountsName
                End If

            ElseIf frmstring = "EMPLOYEE" Then

                dt = OBJCMN.search("EMP_cmpname", "", " EMPLOYEEMaster ", " and EMP_cmpid = " & CmpId & " and EMP_locationid = " & Locationid & " and EMP_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "EMP_cmpname"
                    cmbcmpname.DataSource = dt
                    cmbcmpname.DisplayMember = "EMP_cmpname"
                    cmbcmpname.Text = tempAccountsName
                End If

            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub FILLCMPCODE()
        Try
            Dim OBJCMN As New ClsCommonMaster
            Dim dt As DataTable

            If frmstring = "CUSTOMER" Then

                dt = OBJCMN.search("customer_CODE", "", " CUSTOMERMASTER ", " and CUSTOMER_cmpid = " & CmpId & " and CUSTOMER_locationid = " & Locationid & " and CUSTOMER_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "customer_CODE"
                    CMBCODE.DataSource = dt
                    CMBCODE.DisplayMember = "customer_CODE"
                    CMBCODE.Text = tempAccountsCode
                End If

            ElseIf frmstring = "VENDOR" Then

                dt = OBJCMN.search("VENDOR_CODE", "", " VENDORMaster ", " and VENDOR_cmpid = " & CmpId & " and VENDOR_locationid = " & Locationid & " and VENDOR_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "VENDOR_CODE"
                    CMBCODE.DataSource = dt
                    CMBCODE.DisplayMember = "VENDOR_CODE"
                    CMBCODE.Text = tempAccountsCode
                End If

            ElseIf frmstring = "ACCOUNTS" Then

                dt = OBJCMN.search("acc_CODE", "", " ACCOUNTSMaster ", " and acc_cmpid = " & CmpId & " and acc_locationid = " & Locationid & " and acc_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "acc_CODE"
                    CMBCODE.DataSource = dt
                    CMBCODE.DisplayMember = "acc_CODE"
                    CMBCODE.Text = tempAccountsCode
                End If

            ElseIf frmstring = "EMPLOYEE" Then

                dt = OBJCMN.search("EMP_CODE", "", " EMPLOYEEMaster ", " and EMP_cmpid = " & CmpId & " and EMP_locationid = " & Locationid & " and EMP_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "EMP_CODE"
                    CMBCODE.DataSource = dt
                    CMBCODE.DisplayMember = "EMP_CODE"
                    CMBCODE.Text = tempAccountsCode
                End If

            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub fillcmb()

        Dim OBJCMN As New ClsCommonMaster
        Dim dt As DataTable

        dt = OBJCMN.search("area_name", "", "AreaMaster", " and area_cmpid =" & CmpId & " and area_Locationid =" & Locationid & " and area_Yearid =" & YearId)
        If dt.Rows.Count > 0 Then
            dt.DefaultView.Sort = "area_name"
            cmbarea.DataSource = dt
            cmbarea.DisplayMember = "area_name"
            cmbarea.Text = ""
        End If

        dt = OBJCMN.search("street_name", "", "STREETMaster", " and street_cmpid =" & CmpId & " and street_Locationid =" & Locationid & " and street_Yearid =" & YearId)
        If dt.Rows.Count > 0 Then
            dt.DefaultView.Sort = "street_name"
            CMBSTREET.DataSource = dt
            CMBSTREET.DisplayMember = "street_name"
            CMBSTREET.Text = ""
        End If

        dt = OBJCMN.search("city_name", "", "CityMaster", " and city_cmpid = " & CmpId & " and city_Locationid = " & Locationid & " and city_yearid = " & YearId)
        If dt.Rows.Count > 0 Then
            dt.DefaultView.Sort = "City_name"
            cmbcity.DataSource = dt
            cmbcity.DisplayMember = "city_name"
            cmbcity.Text = ""
        End If

        dt = OBJCMN.search("country_name", "", "CountryMaster", " and country_cmpid = " & CmpId & " and country_Locationid = " & Locationid & " and country_Yearid = " & YearId)
        If dt.Rows.Count > 0 Then
            dt.DefaultView.Sort = "country_name"
            cmbcountry.DataSource = dt
            cmbcountry.DisplayMember = "country_name"
            cmbcountry.Text = ""
        End If

        dt = OBJCMN.search("group_name", "", "GroupMaster", " and group_cmpid = " & CmpId & " and group_Locationid = " & Locationid & " and group_Yearid = " & YearId)
        If dt.Rows.Count > 0 Then
            dt.DefaultView.Sort = "Group_name"
            cmbgroup.DataSource = dt
            cmbgroup.DisplayMember = "group_name"
            cmbgroup.Text = ""
        End If

        dt = OBJCMN.search("state_name", "", "StateMaster", " and state_cmpid = " & CmpId & " and state_Locationid = " & Locationid & " and state_yearid = " & YearId)
        If dt.Rows.Count > 0 Then
            dt.DefaultView.Sort = "state_name"
            cmbstate.DataSource = dt
            cmbstate.DisplayMember = "state_name"
            cmbstate.Text = ""
        End If
        If CMBTRANS.Text.Trim = "" Then fillname(CMBTRANS, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS'  AND LEDGERS.ACC_TYPE='TRANSPORT'")

        If CMBDISTRICT.Text = "" Then fillDISTRICT(CMBDISTRICT)
        If CMBVIA.Text = "" Then fillVIA(CMBVIA)

        If CMBPARTYBANK.Text = "" Then fillPARTYBANK(CMBPARTYBANK, edit)
        If CMBPARTYBANK.Text = "" Then fillPARTYBANK(CMBPARTYBANK1, edit)

        fillform(CHKFORMBOX, edit)


    End Sub

    Private Sub AccountsMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'ACCOUNTS MASTER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            fillcmb()
            FILLCMPNAME()
            FILLCMPCODE()
            cmbgroup.Text = TEMPGROUPNAME
            CMBTYPE.Text = tempTYPE

            If TEMPSUBTYPE <> "" Then CMBSUBTYPE.Text = TEMPSUBTYPE Else CMBSUBTYPE.SelectedIndex = 0


            Dim dttable As New DataTable
            Dim objCommon As New ClsCommonMaster

            Me.Text = "Accounts Master"
            Dim OBJACC As New ClsAccountsMaster
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(tempAccountsName)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(YearId)

            OBJACC.alParaval = ALPARAVAL
            If edit = True Then dttable = OBJACC.GETACCOUNTS


            If edit = True Then
                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                If dttable.Rows.Count > 0 Then
                    tempAccountId = Val(dttable.Rows(0).Item("ACCID"))
                    cmbcmpname.Text = dttable.Rows(0).Item("CMPNAME").ToString
                    txtname.Text = dttable.Rows(0).Item("NAME").ToString
                    cmbgroup.Text = dttable.Rows(0).Item("GROUPNAME").ToString

                    'FILLING REGISTER
                    If cmbgroup.Text.Trim <> "" Then
                        'CHECK GROUP_SECONDARY
                        Dim OBJCMN As New ClsCommon
                        Dim DT As DataTable = OBJCMN.search(" GROUP_SECONDARY ", "", " GROUPMASTER ", " AND GROUP_NAME = '" & cmbgroup.Text.Trim & "' AND GROUP_CMPID = " & CmpId & " AND GROUP_LOCATIONID = " & Locationid & " AND GROUP_YEARID = " & YearId)
                        If DT.Rows.Count > 0 Then
                            TXTPROFITPER.ReadOnly = True

                            If DT.Rows(0).Item(0) = "Sundry Creditors" Then
                                fillregister(cmbregister, " and register_type = 'PURCHASE'")
                            ElseIf DT.Rows(0).Item(0) = "Sundry Debtors" Then
                                fillregister(cmbregister, " and register_type = 'SALE'")
                            ElseIf DT.Rows(0).Item(0) = "Capital A/C" Then
                                TXTPROFITPER.ReadOnly = False
                            End If
                        End If
                    End If


                    txtopbal.Text = Val(dttable.Rows(0).Item("OPBAL").ToString)
                    cmbdrcrrs.Text = dttable.Rows(0).Item("DRCR").ToString
                    TXTINTPER.Text = Val(dttable.Rows(0).Item("INTPER"))
                    TXTPROFITPER.Text = Val(dttable.Rows(0).Item("PROFITPER"))

                    CMBDEDUCTEETYPE.Text = dttable.Rows(0).Item("DEDUCTEETYPE")

                    txtadd1.Text = dttable.Rows(0).Item("ADD1").ToString
                    txtadd2.Text = dttable.Rows(0).Item("ADD2").ToString
                    cmbarea.Text = dttable.Rows(0).Item("AREA").ToString
                    CMBSTREET.Text = dttable.Rows(0).Item("STREET").ToString
                    cmbcity.Text = dttable.Rows(0).Item("CITY").ToString
                    cmbstate.Text = dttable.Rows(0).Item("STATE").ToString
                    txtzipcode.Text = dttable.Rows(0).Item("ZIPCODE").ToString
                    cmbcountry.Text = dttable.Rows(0).Item("COUNTRY").ToString
                    txtstd.Text = dttable.Rows(0).Item("STD").ToString
                    txtresino.Text = dttable.Rows(0).Item("RESINO").ToString
                    txtaltno.Text = dttable.Rows(0).Item("ALTNO").ToString
                    txttel1.Text = dttable.Rows(0).Item("PHONE").ToString
                    txtmobile.Text = dttable.Rows(0).Item("MOBILE").ToString
                    TXTBOSSMOBILE.Text = dttable.Rows(0).Item("BOSSMOBILENO").ToString
                    txtfax.Text = dttable.Rows(0).Item("FAX").ToString
                    txtwebsite.Text = dttable.Rows(0).Item("WEBSITE").ToString
                    cmbemail.Text = dttable.Rows(0).Item("EMAIL").ToString
                    CMBTRANS.Text = dttable.Rows(0).Item("TRANS").ToString
                    CMBAGENT.Text = dttable.Rows(0).Item("AGENT").ToString
                    TXTAGENTCOMM.Text = dttable.Rows(0).Item("AGENTCOMM").ToString
                    TXTDISC.Text = dttable.Rows(0).Item("DISC").ToString
                    TXTKMS.Text = Val(dttable.Rows(0).Item("KMS"))
                    txtcrdays.Text = Val(dttable.Rows(0).Item("CRDAYS").ToString)
                    txtcrlimit.Text = Val(dttable.Rows(0).Item("CRLIMIT").ToString)
                    txtpanno.Text = dttable.Rows(0).Item("PANNO").ToString
                    txtexciseno.Text = dttable.Rows(0).Item("EXCISENO").ToString
                    TXTLANDMARK.Text = dttable.Rows(0).Item("RANGE").ToString
                    txtdivision.Text = dttable.Rows(0).Item("DIVISION").ToString
                    txtcstno.Text = dttable.Rows(0).Item("CSTNO").ToString
                    txttinno.Text = dttable.Rows(0).Item("TINNO").ToString
                    txtstno.Text = dttable.Rows(0).Item("STNO").ToString
                    CMBDISTRICT.Text = dttable.Rows(0).Item("DISTRICT").ToString
                    CMBVIA.Text = dttable.Rows(0).Item("VIA").ToString
                    txtvatno.Text = dttable.Rows(0).Item("VATNO").ToString
                    TXTGSTIN.Text = dttable.Rows(0).Item("GSTIN").ToString
                    cmbregister.Text = dttable.Rows(0).Item("REGISTERNAME").ToString
                    txtadd.Text = dttable.Rows(0).Item("ADD").ToString
                    txtshipadd.Text = dttable.Rows(0).Item("SHIPPINGADD").ToString
                    CMBCALC.Text = dttable.Rows(0).Item("CALC")
                    CMBADDLESS.Text = dttable.Rows(0).Item("ADDLESS").ToString
                    txtremarks.Text = dttable.Rows(0).Item("REMARKS").ToString
                    CMBCODE.Text = dttable.Rows(0).Item("CODE").ToString
                    tempAccountsCode = dttable.Rows(0).Item("CODE").ToString
                    TXTHINDINAME.Text = dttable.Rows(0).Item("HINDINAME")
                    CHKBLOCKED.Checked = Convert.ToBoolean(dttable.Rows(0).Item("BLOCKED"))


                    CHKBANK.Checked = Convert.ToBoolean(dttable.Rows(0).Item("ISBANK"))
                    CMBPARTYBANK.Text = dttable.Rows(0).Item("BANK").ToString
                    TXTACCNO.Text = dttable.Rows(0).Item("ACCOUNTNO").ToString
                    TXTIFSC.Text = dttable.Rows(0).Item("IFSC").ToString
                    TXTBRANCH.Text = dttable.Rows(0).Item("BRANCH").ToString


                    CHKBANK1.Checked = Convert.ToBoolean(dttable.Rows(0).Item("ISBANK1"))
                    CMBPARTYBANK1.Text = dttable.Rows(0).Item("BANK1").ToString
                    TXTACCNO1.Text = dttable.Rows(0).Item("ACCOUNTNO1").ToString
                    TXTIFSC1.Text = dttable.Rows(0).Item("IFSC1").ToString
                    TXTBRANCH1.Text = dttable.Rows(0).Item("BRANCH1").ToString


                    If Val(dttable.Rows(0).Item("TDSPER")) > 0 Then
                        CHKTDSAPP.CheckState = CheckState.Checked
                        If dttable.Rows(0).Item("ISLOWER") = True Then
                            CMBISLOWER.Text = "Yes"
                            CMBSECTION.Enabled = True
                            CMBTDSFORM.Enabled = True
                        Else
                            CMBISLOWER.Text = "No"
                        End If
                        CMBSECTION.Text = dttable.Rows(0).Item("SECTION")
                        CMBTDSFORM.Text = dttable.Rows(0).Item("TDSFORM")

                        TXTTDSRATE.Text = Format(Val(dttable.Rows(0).Item("TDSRATE")), "0.00")
                        TXTTDSPER.Text = Format(Val(dttable.Rows(0).Item("TDSPER")), "0.00")
                        If CMBSECTION.Text.Trim = "197" Then
                            TXTTDSRATE.Enabled = True
                        Else
                            TXTTDSRATE.Enabled = False
                        End If

                        If dttable.Rows(0).Item("SURCHARGE") = True Then
                            CMBSURCHARGE.Text = "Yes"
                        Else
                            CMBSURCHARGE.Text = "No"
                        End If
                        TXTLIMIT.Text = dttable.Rows(0).Item("LIMIT")
                    Else
                        CHKTDSAPP.CheckState = CheckState.Unchecked
                    End If

                    CHKTDS.Checked = Convert.ToBoolean(dttable.Rows(0).Item("TDSAC"))
                    CMBNATURE.Text = dttable.Rows(0).Item("NATURE")

                    CMBTYPE.Text = dttable.Rows(0).Item("TYPE")
                    CMBSUBTYPE.Text = dttable.Rows(0).Item("SUBTYPE")


                    If Convert.ToBoolean(dttable.Rows(0).Item("LOCKED")) = True Then cmbcmpname.Enabled = False

                End If

                'Dim OBJCOMMON As New ClsCommon
                dttable = objCommon.search(" ISNULL(FORMTYPE.FORM_NAME, '') AS FORMNAME", "", " ACCOUNTSMASTER_FORMTYPE INNER JOIN LEDGERS ON ACCOUNTSMASTER_FORMTYPE.Acc_id = LEDGERS.Acc_id AND ACCOUNTSMASTER_FORMTYPE.Acc_yearid = LEDGERS.Acc_yearid LEFT OUTER JOIN FORMTYPE ON ACCOUNTSMASTER_FORMTYPE.Acc_formid = FORMTYPE.FORM_ID", " AND LEDGERS.Acc_cmpname = '" & cmbcmpname.Text & "' AND ACCOUNTSMASTER_FORMTYPE.Acc_yearid = " & YearId)
                If dttable.Rows.Count > 0 Then
                    For Each ROW As DataRow In dttable.Rows
                        For I As Integer = 0 To CHKFORMBOX.Items.Count
                            Dim DTR As DataRowView = CHKFORMBOX.Items(I)
                            If ROW("FORMNAME") = DTR.Item(0) Then
                                CHKFORMBOX.SetItemCheckState(I, CheckState.Checked)
                            End If
                        Next
                    Next
                End If

            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbregister_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbregister.Enter
        Try
            If cmbregister.Text.Trim = "" Then
                'CHECK GROUP_SECONDARY
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" GROUP_SECONDARY ", "", " GROUPMASTER ", " AND GROUP_NAME = '" & cmbgroup.Text.Trim & "' AND GROUP_CMPID = " & CmpId & " AND GROUP_LOCATIONID = " & Locationid & " AND GROUP_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    If DT.Rows(0).Item(0) = "Sundry Creditors" Then
                        fillregister(cmbregister, " and register_type = 'PURCHASE'")
                    ElseIf DT.Rows(0).Item(0) = "Sundry Debtors" Then
                        fillregister(cmbregister, " and register_type = 'SALE'")
                    End If
                End If
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbregister_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbregister.Validating
        Try
            If cmbregister.Text.Trim.Length > 0 Then
                'clear()
                cmbregister.Text = UCase(cmbregister.Text)
                'CHECK GROUP_SECONDARY
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" GROUP_SECONDARY ", "", " GROUPMASTER ", " AND GROUP_NAME = '" & cmbgroup.Text.Trim & "' AND GROUP_CMPID = " & CmpId & " AND GROUP_LOCATIONID = " & Locationid & " AND GROUP_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    If DT.Rows(0).Item(0) = "Sundry Creditors" Then
                        DT = OBJCMN.search(" register_abbr, register_initials, register_id", "", " RegisterMaster", " and register_name ='" & cmbregister.Text.Trim & "' and register_type = 'PURCHASE' and register_cmpid = " & CmpId & " AND REGISTER_LOCATIONID = " & Locationid & " AND REGISTER_YEARID = " & YearId)
                    ElseIf DT.Rows(0).Item(0) = "Sundry Debtors" Then
                        DT = OBJCMN.search(" register_abbr, register_initials, register_id", "", " RegisterMaster", " and register_name ='" & cmbregister.Text.Trim & "' and register_type = 'SALE' and register_cmpid = " & CmpId & " AND REGISTER_LOCATIONID = " & Locationid & " AND REGISTER_YEARID = " & YearId)
                    End If
                    If DT.Rows.Count = 0 Then
                        MsgBox("Register Not Present, Add New from Master Module")
                        cmbregister.Text = ""
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub add()
        txtadd.Clear()
        If txtadd1.Text.Trim <> "" Then txtadd.Text = txtadd.Text & txtadd1.Text.Trim & vbNewLine
        If txtadd2.Text.Trim <> "" Then txtadd.Text = txtadd.Text & txtadd2.Text.Trim & vbNewLine

        If cmbarea.Text.Trim <> "" Then txtadd.Text = txtadd.Text & "" & cmbarea.Text.Trim

        txtadd.Text = txtadd.Text & "" & cmbcity.Text.Trim

        If txtzipcode.Text.Trim <> "" Then
            txtadd.Text = txtadd.Text & " - " & txtzipcode.Text.Trim & vbNewLine
        Else
            txtadd.Text = txtadd.Text & vbNewLine
        End If

        If cmbstate.Text.Trim <> "" Then
            txtadd.Text = txtadd.Text & "" & cmbstate.Text.Trim
        Else
            txtadd.Text = txtadd.Text & vbNewLine
        End If

        If cmbcountry.Text.Trim <> "" Then
            txtadd.Text = txtadd.Text & " " & cmbcountry.Text.Trim
        End If
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            Ep.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim IntResult As Integer
            Dim alParaval As New ArrayList
            alParaval.Add(UCase(cmbcmpname.Text.Trim))
            alParaval.Add(txtname.Text.Trim)
            alParaval.Add(cmbgroup.Text.Trim)
            alParaval.Add(txtopbal.Text.Trim)
            alParaval.Add(cmbdrcrrs.Text.Trim)

            alParaval.Add(Val(TXTINTPER.Text.Trim))
            alParaval.Add(Val(TXTPROFITPER.Text.Trim))

            alParaval.Add(txtadd1.Text.Trim)
            alParaval.Add(txtadd2.Text.Trim)
            alParaval.Add(cmbarea.Text.Trim)
            alParaval.Add(txtstd.Text.Trim)
            alParaval.Add(cmbcity.Text.Trim)
            alParaval.Add(txtzipcode.Text.Trim)
            alParaval.Add(cmbstate.Text.Trim)
            alParaval.Add(cmbcountry.Text.Trim)
            alParaval.Add(txtcrdays.Text.Trim)
            alParaval.Add(txtcrlimit.Text.Trim)
            alParaval.Add(txtresino.Text.Trim)
            alParaval.Add(txtaltno.Text.Trim)
            alParaval.Add(txttel1.Text.Trim)
            alParaval.Add(txtmobile.Text.Trim)
            alParaval.Add(TXTBOSSMOBILE.Text.Trim)
            alParaval.Add(txtfax.Text.Trim)
            alParaval.Add(txtwebsite.Text.Trim)
            alParaval.Add(cmbemail.Text.Trim)

            alParaval.Add(CMBSTREET.Text.Trim)

            alParaval.Add(CHKBANK.CheckState)
            alParaval.Add(CMBPARTYBANK.Text.Trim)
            alParaval.Add(TXTACCNO.Text.Trim)
            alParaval.Add(TXTIFSC.Text.Trim)
            alParaval.Add(TXTBRANCH.Text.Trim)

            alParaval.Add(CHKBANK1.CheckState)
            alParaval.Add(CMBPARTYBANK1.Text.Trim)
            alParaval.Add(TXTACCNO1.Text.Trim)
            alParaval.Add(TXTIFSC1.Text.Trim)
            alParaval.Add(TXTBRANCH1.Text.Trim)


            alParaval.Add(CMBTRANS.Text.Trim)
            alParaval.Add(CMBAGENT.Text.Trim)
            alParaval.Add(Val(TXTAGENTCOMM.Text.Trim))
            alParaval.Add(Val(TXTDISC.Text.Trim))
            alParaval.Add(Val(TXTKMS.Text.Trim))

            alParaval.Add(txtpanno.Text.Trim)
            alParaval.Add(txtexciseno.Text.Trim)
            alParaval.Add(TXTLANDMARK.Text.Trim)
            alParaval.Add(txtdivision.Text.Trim)
            alParaval.Add(txtcstno.Text.Trim)
            alParaval.Add(txttinno.Text.Trim)
            alParaval.Add(txtstno.Text.Trim)
            alParaval.Add(CMBDISTRICT.Text.Trim)
            alParaval.Add(CMBVIA.Text.Trim)
            alParaval.Add(txtvatno.Text.Trim)
            alParaval.Add(TXTGSTIN.Text.Trim)
            alParaval.Add(cmbregister.Text.Trim)
            alParaval.Add(txtadd.Text.Trim)
            alParaval.Add(txtshipadd.Text.Trim)
            alParaval.Add(CMBCALC.Text.Trim)
            alParaval.Add(CMBADDLESS.Text.Trim)
            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)
            alParaval.Add(CMBCODE.Text.Trim)
            alParaval.Add(TXTHINDINAME.Text.Trim)
            alParaval.Add(CHKBLOCKED.CheckState)

            'TDS
            '*******************************
            alParaval.Add(CHKTDSAPP.CheckState)
            alParaval.Add(CMBDEDUCTEETYPE.Text.Trim)

            If CMBISLOWER.Text.Trim = "Yes" Then
                alParaval.Add(1)
            Else
                alParaval.Add(0)
            End If

            alParaval.Add(CMBSECTION.Text.Trim)
            alParaval.Add(CMBTDSFORM.Text.Trim)
            alParaval.Add(Val(TXTTDSRATE.Text.Trim))
            alParaval.Add(Val(TXTTDSPER.Text.Trim))

            If CMBSURCHARGE.Text.Trim = "Yes" Then
                alParaval.Add(1)
            Else
                alParaval.Add(0)
            End If
            alParaval.Add(Val(TXTLIMIT.Text.Trim))
            '*******************************



            alParaval.Add(CHKTDS.CheckState)
            alParaval.Add(CMBNATURE.Text.Trim)
            alParaval.Add(CMBTYPE.Text.Trim)
            alParaval.Add(CMBSUBTYPE.Text.Trim)


            'ADDING FORMTYPE
            Dim FORMTYPE As String = ""
            For Each DTROW As DataRowView In CHKFORMBOX.CheckedItems
                If FORMTYPE = "" Then
                    FORMTYPE = DTROW.Item(0)
                Else
                    FORMTYPE = FORMTYPE & "|" & DTROW.Item(0)
                End If
            Next
            alParaval.Add(FORMTYPE)


            Dim objAccountsMaster As New ClsAccountsMaster
            objAccountsMaster.alParaval = alParaval
            objAccountsMaster.frmstring = frmstring

            If edit = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                IntResult = objAccountsMaster.save()
                MsgBox("Details Added")
            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(tempAccountId)
                IntResult = objAccountsMaster.update()
                MsgBox("Details Updated")
            End If

            clear()
            cmbcmpname.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTINTPER_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTINTPER.KeyPress, TXTPROFITPER.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub TXTINTPER_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTINTPER.Validating
        Try
            If Val(TXTINTPER.Text.Trim) > 100 Then
                e.Cancel = True
                MsgBox("% Cannot be Greater then 100", MsgBoxStyle.Critical)
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTPROFITPER_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTPROFITPER.Validating
        Try
            If Val(TXTPROFITPER.Text.Trim) > 100 Then
                e.Cancel = True
                MsgBox("% Cannot be Greater then 100", MsgBoxStyle.Critical)
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub clear()

        'TDS
        CHKTDS.CheckState = CheckState.Unchecked
        CMBNATURE.Text = ""

        CHKTDSAPP.CheckState = CheckState.Unchecked
        GROUPTDS.Enabled = False
        CMBDEDUCTEETYPE.Text = ""
        CMBISLOWER.SelectedIndex = 0
        CMBSECTION.SelectedIndex = 0
        CMBTDSFORM.SelectedIndex = 0
        TXTTDSRATE.Clear()
        TXTTDSPER.Clear()
        CMBSURCHARGE.SelectedIndex = 0
        TXTTDSRATE.Enabled = False
        CMBSECTION.Enabled = False
        CMBTDSFORM.Enabled = False

        TXTINTPER.Clear()
        TXTPROFITPER.Clear()
        TXTPROFITPER.ReadOnly = True

        txtadd.Clear()
        txtadd1.Clear()
        txtadd2.Clear()
        txtaltno.Clear()
        cmbcmpname.Text = ""
        cmbcmpname.Enabled = True
        txtcrdays.Clear()
        txtcrlimit.Clear()
        txtcstno.Clear()
        txtdivision.Clear()

        cmbemail.Text = ""
        CMBTRANS.Text = ""
        CMBAGENT.Text = ""
        TXTAGENTCOMM.Clear()

        txtexciseno.Clear()
        txtfax.Clear()
        TXTGSTIN.Clear()
        txtmobile.Clear()
        TXTBOSSMOBILE.Clear()
        txtname.Clear()
        txtopbal.Clear()
        txtpanno.Clear()
        TXTLANDMARK.Clear()
        txtremarks.Clear()
        txtresino.Clear()
        txtshipadd.Clear()
        txtstd.Clear()
        txtstno.Clear()
        txttel1.Clear()
        txttinno.Clear()
        txtvatno.Clear()
        txtwebsite.Clear()
        txtzipcode.Clear()
        cmbarea.Text = ""
        CMBSTREET.Text = ""
        TXTKMS.Clear()

        CHKBANK.CheckState = CheckState.Checked
        TXTBANK.Clear()
        CMBPARTYBANK.Text = ""
        TXTACCNO.Clear()
        TXTIFSC.Clear()
        TXTBRANCH.Clear()


        CHKBANK1.CheckState = CheckState.Unchecked
        CMBPARTYBANK1.Text = ""
        TXTACCNO1.Clear()
        TXTIFSC1.Clear()
        TXTBRANCH1.Clear()


        cmbcity.Text = ""
        cmbcountry.Text = ""
        cmbdrcrrs.Text = ""
        cmbgroup.Text = ""
        cmbstate.Text = ""
        CMBCODE.Text = ""
        tempAccountsName = ""
        cmbcmpname.Text = ""
        CMBDISTRICT.Text = ""
        CMBVIA.Text = ""
        edit = False

        TXTHINDINAME.Clear()
        CHKBLOCKED.CheckState = CheckState.Unchecked

        'cmbregister.DataSource = Nothing

        If frmstring = "CUSTOMER" Then
            Me.Text = "Customer Master"
            cmbgroup.Text = "Sundry Debtors"
        ElseIf frmstring = "VENDOR" Then
            Me.Text = "Supplier Master"
            cmbgroup.Text = "Sundry Creditors"
        ElseIf frmstring = "ACCOUNTS" Then
            Me.Text = "Accounts Master"
        ElseIf frmstring = "EMPLOYEE" Then
            Me.Text = "Employee Master"
        End If
    End Sub

    Function CHECKDUPLICATE() As Boolean
        Try
            Dim BLN As Boolean = True
            uppercase(cmbcmpname)
            'CMBCODE.Text = cmbcmpname.Text.Trim
            Dim OBJCMN As New ClsCommonMaster
            Dim dt As DataTable
            If (edit = False) Or (edit = True And LCase(cmbcmpname.Text) <> LCase(tempAccountsName)) Then
                dt = OBJCMN.search("ACC_CMPNAME", "", " LEDGERS", " AND ACC_CMPNAME = '" & cmbcmpname.Text.Trim & "' and LEDGERS.acc_cmpid = " & CmpId & " AND LEDGERS.ACC_LOCATIONID = " & Locationid & " AND LEDGERS.ACC_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    MsgBox("Name Already Exists", MsgBoxStyle.Critical, "EASYBIZ")
                    BLN = False
                End If
            End If
            Return BLN
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True
        If cmbcmpname.Text.Trim.Length = 0 Then
            Ep.SetError(cmbcmpname, "Fill Company Name")
            bln = False
        End If

        If Not CHECKDUPLICATE() Then
            Ep.SetError(cmbcmpname, "Name Already Exists")
            bln = False
        End If

        If CMBTYPE.Text.Trim.Length = 0 Then
            Ep.SetError(CMBTYPE, "Select Type")
            bln = False
        End If

        If CHKTDS.CheckState = CheckState.Unchecked Then CMBNATURE.Text = ""

        If CHKTDS.CheckState = CheckState.Checked And CMBNATURE.Text.Trim = "" Then
            Ep.SetError(CMBNATURE, "Select Nature Of Payment")
            bln = False
        End If



        If CMBCODE.Text.Trim.Length = 0 Then
            Ep.SetError(CMBCODE, "Fill Company Code")
            bln = False
        End If

        If cmbgroup.Text.Trim.Length = 0 Then
            Ep.SetError(cmbgroup, "Select Group")
            bln = False
        End If

        If CMBTDSFORM.Text.Trim = "TDS" And Val(TXTTDSPER.Text.Trim) = 0 Then
            Ep.SetError(TXTTDSPER, "Enter TDS%")
            bln = False
        End If

        If CMBTDSFORM.Text.Trim <> "TDS" And Val(TXTTDSPER.Text.Trim) > 0 Then
            Ep.SetError(TXTTDSPER, "Remove TDS")
            bln = False
        End If

        If CMBTRANS.Text.Trim = cmbcmpname.Text.Trim Then
            Ep.SetError(CMBTRANS, "Transport Name cannot be same as Company Name")
            bln = False
        End If

        If CMBAGENT.Text.Trim = cmbcmpname.Text.Trim Then
            Ep.SetError(CMBAGENT, "Agent Name cannot be same as Company Name")
            bln = False
        End If


        'IF GROUPNAME = CREDITORS OR DEBTORS THEN ONLY
        Dim OBJCMN As New ClsCommon
        Dim DT As DataTable = OBJCMN.search("GROUP_SECONDARY AS GROUPNAME", "", " GROUPMASTER", " AND GROUP_NAME ='" & cmbgroup.Text.Trim & "' AND GROUP_CMPID =  " & CmpId & " AND GROUP_LOCATIONID = " & Locationid & " AND GROUP_YEARID = " & YearId)
        If DT.Rows.Count > 0 Then
            If (DT.Rows(0).Item(0) = "Sundry Creditors" Or DT.Rows(0).Item(0) = "Sundry Debtors") And CMBTYPE.Text.Trim = "ACCOUNTS" And CMBSUBTYPE.Text.Trim = "ACCOUNTS" Then
                If txtzipcode.Text.Trim.Length = 0 Then
                    Ep.SetError(txtzipcode, "Fill Pin-Code")
                    bln = False
                End If

                If cmbstate.Text.Trim.Length = 0 Then
                    Ep.SetError(cmbstate, "Select State")
                    bln = False
                End If

            End If

            If DT.Rows(0).Item(0) = "Sundry Debtors" Then
                If Val(TXTKMS.Text.Trim) = 0 Then
                    Ep.SetError(TXTKMS, "Enter Kilometers")
                    bln = False
                End If
            End If

        End If


        If txtpanno.Text.Trim <> "" Then

            If CMBDEDUCTEETYPE.Text.Trim = "" Then
                Ep.SetError(txtpanno, "Select Company Type")
                bln = False
            End If

            If txtpanno.Text.Trim.Length <> 10 Then
                Ep.SetError(txtpanno, "Insert Proper PAN No")
                bln = False
            Else
                'validating PAN NO
                For x = 1 To Len(txtpanno.Text.Trim)
                    If x <= 5 Or x = 10 Then
                        If Asc(txtpanno.Text.Substring(x - 1, 1)) < 65 Or Asc(txtpanno.Text.Substring(x - 1, 1)) > 90 Then
                            Ep.SetError(txtpanno, "Insert Proper PAN No")
                            bln = False
                        End If
                    Else
                        If Asc(txtpanno.Text.Substring(x - 1, 1)) < 48 Or Asc(txtpanno.Text.Substring(x - 1, 1)) > 57 Then
                            Ep.SetError(txtpanno, "Insert Proper PAN No")
                            bln = False
                        End If
                    End If
                    'CHECKING 4TH ALPHABET WITH DEDUCTEETYPE
                    If x = 4 Then
                        If CMBDEDUCTEETYPE.Text.Trim = "Individual" Then
                            If txtpanno.Text.Substring(x - 1, 1) <> "P" Then
                                Ep.SetError(txtpanno, "Insert Proper PAN No")
                                bln = False
                            End If
                        ElseIf CMBDEDUCTEETYPE.Text.Trim = "Firm" Then
                            If txtpanno.Text.Substring(x - 1, 1) <> "F" Then
                                Ep.SetError(txtpanno, "Insert Proper PAN No")
                                bln = False
                            End If
                        ElseIf CMBDEDUCTEETYPE.Text = "Company" Then
                            If txtpanno.Text.Substring(x - 1, 1) <> "C" Then
                                Ep.SetError(txtpanno, "Insert Proper PAN No")
                                bln = False
                            End If
                        ElseIf CMBDEDUCTEETYPE.Text = "HUF" Then
                            If txtpanno.Text.Substring(x - 1, 1) <> "H" Then
                                Ep.SetError(txtpanno, "Insert Proper PAN No")
                                bln = False
                            End If
                        ElseIf CMBDEDUCTEETYPE.Text = "Association of Persons" Then
                            If txtpanno.Text.Substring(x - 1, 1) <> "A" Then
                                Ep.SetError(txtpanno, "Insert Proper PAN No")
                                bln = False
                            End If
                        ElseIf CMBDEDUCTEETYPE.Text = "Association of Trust" Then
                            If txtpanno.Text.Substring(x - 1, 1) <> "T" Then
                                Ep.SetError(txtpanno, "Insert Proper PAN No")
                                bln = False
                            End If
                        ElseIf CMBDEDUCTEETYPE.Text = "Body Of Individuals" Then
                            If txtpanno.Text.Substring(x - 1, 1) <> "B" Then
                                Ep.SetError(txtpanno, "Insert Proper PAN No")
                                bln = False
                            End If
                        ElseIf CMBDEDUCTEETYPE.Text = "Local Authority" Then
                            If txtpanno.Text.Substring(x - 1, 1) <> "L" Then
                                Ep.SetError(txtpanno, "Insert Proper PAN No")
                                bln = False
                            End If
                        ElseIf CMBDEDUCTEETYPE.Text = "Government" Then
                            If txtpanno.Text.Substring(x - 1, 1) <> "G" Then
                                Ep.SetError(txtpanno, "Insert Proper PAN No")
                                bln = False
                            End If
                        End If
                    End If
                Next x
            End If
        End If


        'CHECKING 1ST TWO ALPHABETS WITH STATE
        If cmbstate.Text.Trim <> "" And TXTGSTIN.Text.Trim <> "" Then
            DT = OBJCMN.search(" cast(state_remark as varchar(5)) AS STATECODE ", "", " STATEMASTER", " AND state_name = '" & cmbstate.Text.Trim & "' and STATE_YEARID = " & YearId)
            If DT.Rows(0).Item("STATECODE") <> TXTGSTIN.Text.Substring(0, 2) Then
                Ep.SetError(TXTGSTIN, "State Code does not match with GST No")
                bln = False
            End If
        End If


        If TXTGSTIN.Text.Trim.Length > 0 Then
            If txtpanno.Text.Trim = "" Then
                Ep.SetError(txtpanno, "Enter Pan No")
                bln = False
            End If

            If txtpanno.Text.Trim.Length <> 10 Then
                Ep.SetError(txtpanno, "Insert Proper PAN No")
                bln = False
            End If

            'CHECKING 2ND TO 11TH ALPHABETS WITH PANNO
            If TXTGSTIN.Text.Trim.Length = 15 Then
                If txtpanno.Text.Trim <> TXTGSTIN.Text.Substring(2, 10) Then
                    Ep.SetError(txtpanno, "Enter Proper PAN Details")
                    bln = False
                End If
            End If

            If TXTGSTIN.Text.Trim.Length <> 15 Then
                Ep.SetError(TXTGSTIN, "Enter Proper GST No")
                bln = False
            End If
        End If

        Return bln
    End Function

    Private Sub cmbgroup_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbgroup.GotFocus
        Try
            If cmbgroup.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search("group_name", "", "GroupMaster", " and group_cmpid = " & CmpId & " and group_Locationid = " & Locationid & " and group_Yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "Group_name"
                    cmbgroup.DataSource = dt
                    cmbgroup.DisplayMember = "group_name"
                    cmbgroup.Text = ""
                End If
                cmbgroup.SelectAll()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbcity_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbcity.GotFocus
        Try
            If cmbcity.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search("city_name", "", "CityMaster", " and city_cmpid = " & CmpId & " and city_Locationid = " & Locationid & " and city_Yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "City_name"
                    cmbcity.DataSource = dt
                    cmbcity.DisplayMember = "city_name"
                    cmbcity.Text = ""
                End If
                cmbcity.SelectAll()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbcity_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbcity.Validating
        Try
            If cmbcity.Text.Trim <> "" Then
                uppercase(cmbcity)
                Dim OBJCMN As New ClsCommonMaster
                Dim objyearmaster As New ClsYearMaster
                Dim dt As DataTable
                dt = OBJCMN.search("city_name", "", "CityMaster", " and city_name = '" & cmbcity.Text.Trim & "' and city_cmpid = " & CmpId & " and city_Locationid = " & Locationid & " and city_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = cmbcity.Text.Trim
                    Dim tempmsg As Integer = MsgBox("City not present, Add New?", MsgBoxStyle.YesNo, "PROCESS")
                    If tempmsg = vbYes Then
                        Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'CITY MASTER'")
                        If DTROW(0).Item(1) = False Then
                            MsgBox("Insufficient Rights")
                            Exit Sub
                        End If
                        cmbcity.Text = a
                        objyearmaster.savecity(cmbcity.Text.Trim, CmpId, Locationid, Userid, YearId, " and city_name = '" & cmbcity.Text.Trim & "' and city_cmpid = " & CmpId & " and city_Locationid = " & Locationid & " and city_Yearid = " & YearId)
                        Dim dt1 As New DataTable
                        dt1 = cmbcity.DataSource
                        If cmbcity.DataSource <> Nothing Then
line1:
                            If dt1.Rows.Count > 0 Then
                                dt1.Rows.Add(cmbcity.Text)
                                cmbcity.Text = a
                            End If
                        End If
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbstate_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbstate.GotFocus
        Try
            If cmbstate.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search("state_name", "", "StateMaster", " and state_cmpid = " & CmpId & " and state_Locationid = " & Locationid & " and state_Yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "state_name"
                    cmbstate.DataSource = dt
                    cmbstate.DisplayMember = "state_name"
                    cmbstate.Text = ""
                End If
                cmbstate.SelectAll()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbstate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbstate.Validating
        Try
            If cmbstate.Text.Trim <> "" Then

                uppercase(cmbstate)
                Dim OBJCMN As New ClsCommonMaster
                Dim objyearmaster As New ClsYearMaster
                Dim dt As DataTable
                dt = OBJCMN.search("state_name", "", "StateMaster", " and state_name = '" & cmbstate.Text.Trim & "' and state_cmpid = " & CmpId & " and state_Locationid = " & Locationid & " and state_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = cmbstate.Text.Trim
                    Dim tempmsg As Integer = MsgBox("State not present, Add New?", MsgBoxStyle.YesNo, "PROCESS")
                    If tempmsg = vbYes Then
                        Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'STATE MASTER'")
                        If DTROW(0).Item(1) = False Then
                            MsgBox("Insufficient Rights")
                            Exit Sub
                        End If
                        cmbstate.Text = a
                        objyearmaster.savestate(cmbstate.Text.Trim, CmpId, Locationid, Userid, YearId, " and state_name = '" & cmbstate.Text.Trim & "' and state_cmpid = " & CmpId & " and state_Locationid = " & Locationid & " and state_Yearid = " & YearId)
                        Dim dt1 As New DataTable
                        dt1 = cmbstate.DataSource
                        If cmbstate.DataSource <> Nothing Then
line1:
                            If dt1.Rows.Count > 0 Then
                                dt1.Rows.Add(cmbstate.Text)
                                cmbstate.Text = a
                            End If
                        End If
                    Else
                        e.Cancel = True
                    End If
                End If

            End If
        Catch ex As Exception
            GoTo line1
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbcountry_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbcountry.GotFocus
        Try
            If cmbcountry.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search("country_name", "", "CountryMaster", " and country_cmpid = " & CmpId & " and country_Locationid = " & Locationid & " and country_Yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "country_name"
                    cmbcountry.DataSource = dt
                    cmbcountry.DisplayMember = "country_name"
                    cmbcountry.Text = ""
                End If
                cmbcountry.SelectAll()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbcountry_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbcountry.Validating
        Try
            If cmbcountry.Text.Trim <> "" Then
                uppercase(cmbcountry)
                Dim OBJCMN As New ClsCommonMaster
                Dim objyearmaster As New ClsYearMaster
                Dim dt As DataTable
                dt = OBJCMN.search("Country_name", "", "CountryMaster", " and Country_name = '" & cmbcountry.Text.Trim & "' and country_cmpid = " & CmpId & " and country_Locationid = " & Locationid & " and country_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = cmbcountry.Text.Trim
                    Dim tempmsg As Integer = MsgBox("Country not present, Add New?", MsgBoxStyle.YesNo, "PROCESS")
                    If tempmsg = vbYes Then
                        Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'COUNTRY MASTER'")
                        If DTROW(0).Item(1) = False Then
                            MsgBox("Insufficient Rights")
                            Exit Sub
                        End If
                        cmbcountry.Text = a
                        objyearmaster.savecountry(cmbcountry.Text.Trim, CmpId, Locationid, Userid, YearId, " and Country_name = '" & cmbcountry.Text.Trim & "' and country_cmpid = " & CmpId & " and country_Locationid = " & Locationid & " and country_Yearid = " & YearId)
                        Dim dt1 As New DataTable
                        dt1 = cmbcountry.DataSource
                        If cmbcountry.DataSource <> Nothing Then
                            If dt1.Rows.Count > 0 Then
Line1:
                                dt1.Rows.Add(cmbcountry.Text)
                                cmbcountry.Text = a
                            End If
                        End If
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    'Private Sub txtcmpname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
    '    Try

    '        UPPERCASE(cmbcmpname)
    '        If (edit = False) Or (edit = True And tempAccountsName <> cmbcmpname.Text.Trim) Then
    '            'for search
    '            Dim OBJCMN As New ClsCommonMaster
    '            Dim dt As New DataTable
    '            If frmstring = "CUSTOMER" Then
    '                dt = OBJCMN.search("customer_cmpname", "", "CustomerMaster", " and customer_cmpname = '" & cmbcmpname.Text.Trim & "' and customer_cmpid =" & CmpId & " and customer_Locationid =" & Locationid & " and customer_Yearid =" & YearId)
    '            ElseIf frmstring = "VENDOR" Then
    '                dt = OBJCMN.search("Vendor_cmpname", "", "VendorMaster", " and Vendor_cmpname = '" & cmbcmpname.Text.Trim & "' and Vendor_cmpid = " & CmpId & " and Vendor_Locationid = " & Locationid & " and Vendor_Yearid = " & YearId)
    '            ElseIf frmstring = "ACCOUNTS" Then
    '                dt = OBJCMN.search("acc_cmpname", "", "AccountsMaster", " and acc_cmpname = '" & cmbcmpname.Text.Trim & "' and acc_cmpid = " & CmpId & " and acc_Locationid = " & Locationid & " and acc_Yearid = " & YearId)
    '            ElseIf frmstring = "EMPLOYEE" Then
    '                dt = OBJCMN.search("Emp_cmpname", "", "EmployeeMaster", " and Emp_cmpname = '" & cmbcmpname.Text.Trim & "' and Emp_cmpid = " & CmpId & " and Emp_Locationid = " & Locationid & " and Emp_Yearid = " & YearId)
    '            End If
    '            If dt.Rows.Count > 0 Then
    '                MsgBox("Company Name Already Exists", MsgBoxStyle.Critical, "PROCESS")
    '                e.Cancel = True
    '            End If
    '        End If
    '    Catch ex As Exception
    '        If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
    '    End Try
    'End Sub

    Private Sub cmbarea_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbarea.GotFocus
        Try
            If cmbarea.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search("area_name", "", "AreaMaster", " and area_cmpid = " & CmpId & " and area_Locationid = " & Locationid & " and area_Yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "area_name"
                    cmbarea.DataSource = dt
                    cmbarea.DisplayMember = "area_name"
                    cmbarea.Text = ""
                End If
                cmbarea.SelectAll()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbstreet_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBSTREET.GotFocus
        Try
            If CMBSTREET.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search("street_name", "", "streetMaster", " and street_Yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "street_name"
                    CMBSTREET.DataSource = dt
                    CMBSTREET.DisplayMember = "street_name"
                    CMBSTREET.Text = ""
                End If
                CMBSTREET.SelectAll()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbarea_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbarea.Validating
        Try
            If cmbarea.Text.Trim <> "" Then
                uppercase(cmbarea)
                Dim OBJCMN As New ClsCommonMaster
                Dim objyearmaster As New ClsYearMaster
                Dim dt As DataTable
                dt = OBJCMN.search("area_name", "", "areaMaster", " and area_name = '" & cmbarea.Text.Trim & "' and area_cmpid = " & CmpId & " and area_Locationid = " & Locationid & " and area_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = cmbarea.Text.Trim
                    Dim tempmsg As Integer = MsgBox("area not present, Add New?", MsgBoxStyle.YesNo, "PROCESS")
                    If tempmsg = vbYes Then
                        Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'AREA MASTER'")
                        If DTROW(0).Item(1) = False Then
                            MsgBox("Insufficient Rights")
                            Exit Sub
                        End If
                        cmbarea.Text = a
                        objyearmaster.savearea(cmbarea.Text.Trim, CmpId, Locationid, Userid, YearId, " and area_name = '" & cmbarea.Text.Trim & "' and area_cmpid = " & CmpId & " and area_Locationid = " & Locationid & " and area_Yearid = " & YearId)
                        Dim dt1 As New DataTable
                        dt1 = cmbarea.DataSource
                        If cmbarea.DataSource <> Nothing Then
line1:
                            If dt1.Rows.Count > 0 Then
                                dt1.Rows.Add(cmbarea.Text)
                                cmbarea.Text = a
                            End If
                        End If
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbstreet_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBSTREET.Validating
        Try
            If CMBSTREET.Text.Trim <> "" Then
                uppercase(CMBSTREET)
                Dim OBJCMN As New ClsCommonMaster
                Dim objyearmaster As New ClsYearMaster
                Dim dt As DataTable
                dt = OBJCMN.search("street_name", "", "streetMaster", " and street_name = '" & CMBSTREET.Text.Trim & "' and street_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBSTREET.Text.Trim
                    Dim tempmsg As Integer = MsgBox("Street not present, Add New?", MsgBoxStyle.YesNo, "PROCESS")
                    If tempmsg = vbYes Then
                        Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'AREA MASTER'")
                        If DTROW(0).Item(1) = False Then
                            MsgBox("Insufficient Rights")
                            Exit Sub
                        End If
                        CMBSTREET.Text = a
                        objyearmaster.savestreet(CMBSTREET.Text.Trim, CmpId, Locationid, Userid, YearId, " and street_name = '" & CMBSTREET.Text.Trim & "' and street_Yearid = " & YearId)
                        Dim dt1 As New DataTable
                        dt1 = CMBSTREET.DataSource
                        If CMBSTREET.DataSource <> Nothing Then
line1:
                            If dt1.Rows.Count > 0 Then
                                dt1.Rows.Add(CMBSTREET.Text)
                                CMBSTREET.Text = a
                            End If
                        End If
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbgroup_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbgroup.Validated
        Try
            If cmbgroup.Text.Trim <> "" Then
                'CHECK GROUP_SECONDARY
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" GROUP_SECONDARY ", "", " GROUPMASTER ", " AND GROUP_NAME = '" & cmbgroup.Text.Trim & "' AND GROUP_CMPID = " & CmpId & " AND GROUP_LOCATIONID = " & Locationid & " AND GROUP_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    cmbregister.Text = ""
                    TXTPROFITPER.ReadOnly = True
                    If DT.Rows(0).Item(0) = "Sundry Creditors" Then
                        fillregister(cmbregister, " and register_type = 'PURCHASE'")
                    ElseIf DT.Rows(0).Item(0) = "Sundry Debtors" Then
                        fillregister(cmbregister, " and register_type = 'SALE'")
                    ElseIf DT.Rows(0).Item(0) = "Capital A/C" Then
                        TXTPROFITPER.ReadOnly = False
                    Else
                        cmbregister.DataSource = Nothing
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbgroup_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbgroup.Validating
        Try
            If cmbgroup.Text.Trim <> "" Then
                uppercase(cmbgroup)
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search("group_name", "", "groupMaster", " and group_name = '" & cmbgroup.Text.Trim & "' and group_cmpid = " & CmpId & " and group_Locationid = " & Locationid & " and group_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = cmbgroup.Text.Trim
                    Dim tempmsg As Integer = MsgBox("Group not present, Add New?", MsgBoxStyle.YesNo, "PROCESS")
                    If tempmsg = vbYes Then
                        cmbgroup.Text = a
                        Dim objgroupmaster As New GroupMaster
                        objgroupmaster.txtname.Text = cmbgroup.Text.Trim()
                        objgroupmaster.ShowDialog()
                        dt = OBJCMN.search("group_name", "", "groupMaster", " and group_name = '" & cmbgroup.Text.Trim & "' and group_cmpid = " & CmpId & " and group_Locationid = " & Locationid & " and group_Yearid = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As DataTable
                            dt1 = cmbgroup.DataSource
                            If cmbgroup.DataSource <> Nothing Then
line1:
                                dt1.Rows.Add(cmbgroup.Text)
                                cmbgroup.Text = a
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
        End Try
    End Sub

    Private Sub txtname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtname.Validating
        pcase(txtname)
    End Sub

    Private Sub txtadd1_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtadd1.Validating
        pcase(txtadd1)
    End Sub

    Private Sub txtadd2_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtadd2.Validating
        pcase(txtadd2)
    End Sub

    Private Sub txtzipcode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtzipcode.KeyPress
        numdotkeypress(e, txtzipcode, Me)
    End Sub

    Private Sub txtcrdays_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcrdays.KeyPress, TXTKMS.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub txtcrlimit_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcrlimit.KeyPress
        numdotkeypress(e, txtcrlimit, Me)
    End Sub

    Private Sub txtresino_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtresino.KeyPress
        numdotkeypress(e, txtresino, Me)
    End Sub

    Private Sub txtaltno_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtaltno.KeyPress
        numdotkeypress(e, txtaltno, Me)
    End Sub

    Private Sub txttel1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txttel1.KeyPress
        'numdotkeypress(e, txttel1, Me)
    End Sub

    Private Sub txtmobile_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtmobile.KeyPress
        numdotkeypress(e, txtmobile, Me)
    End Sub

    Private Sub txtfax_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtfax.KeyPress
        numdotkeypress(e, txtfax, Me)
    End Sub

    Private Sub cmbcmpname_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbcmpname.Enter
        Try
            If cmbcmpname.Text.Trim = "" Then
                FILLCMPNAME()
                cmbcmpname.SelectAll()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbcmpname_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbcmpname.KeyDown
        If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
        If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True
    End Sub

    Private Sub cmbcmpname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbcmpname.Validating
        Try
            If cmbcmpname.Text.Trim <> "" Then
                uppercase(cmbcmpname)
                If CMBCODE.Text.Trim = "" Then CMBCODE.Text = cmbcmpname.Text.Trim
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                If (edit = False) Or (edit = True And LCase(cmbcmpname.Text) <> LCase(tempAccountsName)) Then
                    dt = OBJCMN.search("ACC_CMPNAME", "", " LEDGERS", " AND ACC_CMPNAME = '" & cmbcmpname.Text.Trim & "' and LEDGERS.acc_cmpid = " & CmpId & " AND LEDGERS.ACC_LOCATIONID = " & Locationid & " AND LEDGERS.ACC_YEARID = " & YearId)
                    If dt.Rows.Count > 0 Then
                        MsgBox("Name Already Exists", MsgBoxStyle.Critical, "PROCESS")
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Function DELETEERRORVALID() As Boolean
        Try
            Dim BLN As Boolean = True
            Dim bln_4_case As Boolean = True
            Select Case tempAccountsName
                Case "Transport Charges"
                    ' Str = "Turquoise"
                    bln_4_case = False
                Case "Other Charges"
                    bln_4_case = False
                Case "PURCHASE"
                    bln_4_case = False
                Case "SALE"
                    bln_4_case = False
                Case "T.D.S."
                    bln_4_case = False
                Case "Cash In Hand"
                    bln_4_case = False
                Case "Petty Cash"
                    bln_4_case = False
                Case "Round Off"
                    bln_4_case = False
                Case "Discount Recd"
                    bln_4_case = False
                Case "Commission Recd"
                    bln_4_case = False
                Case "Discount Given"
                    bln_4_case = False
                Case "Commission A/C"
                    bln_4_case = False
                Case "Profit & Loss A/C"
                    bln_4_case = False
                Case "Opening A/C"
                    bln_4_case = False
            End Select

            If bln_4_case = False Then
                Ep.SetError(cmbcmpname, "Build In Ledger Cannot Delete")
                BLN = False
            End If
            Return BLN
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Try
            If edit = True Then

                Ep.Clear()
                If Not DELETEERRORVALID() Then
                    Exit Sub
                End If

                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If MsgBox("Wish to Delete Ledger?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

                Dim ALPARAVAL As New ArrayList

                ALPARAVAL.Add(tempAccountsName)
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(Locationid)
                ALPARAVAL.Add(YearId)

                Dim OBJACC As New ClsAccountsMaster
                OBJACC.alParaval = ALPARAVAL
                OBJACC.frmstring = frmstring
                Dim DT As DataTable = OBJACC.DELETE
                If DT.Rows.Count > 0 Then
                    MsgBox(DT.Rows(0).Item(0))
                    clear()
                    edit = False
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCODE_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBCODE.Enter
        Try
            If CMBCODE.Text.Trim = "" Then
                FILLCMPCODE()
                CMBCODE.SelectAll()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBCODE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBCODE.Validating
        Try
            If CMBCODE.Text.Trim <> "" Then
                uppercase(CMBCODE)
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                If (edit = False) Or (edit = True And LCase(CMBCODE.Text) <> LCase(tempAccountsCode)) Then
                    dt = OBJCMN.search("ACC_CODE", "", " LEDGERS", " AND ACC_CODE = '" & CMBCODE.Text.Trim & "' and LEDGERS.acc_cmpid = " & CmpId & " AND LEDGERS.ACC_LOCATIONID = " & Locationid & " AND LEDGERS.ACC_YEARID = " & YearId)
                    If dt.Rows.Count > 0 Then
                        MsgBox("Code Already Exists", MsgBoxStyle.Critical, "PROCESS")
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub chkcopy_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkcopy.CheckedChanged
        Try
            txtadd.Clear()
            If chkcopy.CheckState = CheckState.Checked Then
                If txtadd1.Text.Trim <> "" Then txtadd.Text = txtadd.Text & txtadd1.Text.Trim & "," & vbNewLine
                If CMBSTREET.Text.Trim <> "" Then txtadd.Text = txtadd.Text & CMBSTREET.Text.Trim & ", "
                If cmbarea.Text.Trim <> "" Then txtadd.Text = txtadd.Text & "" & cmbarea.Text.Trim & vbNewLine
                If TXTLANDMARK.Text.Trim <> "" Then txtadd.Text = txtadd.Text & "" & TXTLANDMARK.Text.Trim & vbNewLine
                If cmbcity.Text.Trim <> "" Then txtadd.Text = txtadd.Text & cmbcity.Text.Trim
                If txtzipcode.Text.Trim <> "" Then
                    txtadd.Text = txtadd.Text & " - " & txtzipcode.Text.Trim & "," & vbNewLine
                Else
                    txtadd.Text = txtadd.Text & vbNewLine
                End If
                If CMBVIA.Text.Trim <> "" Then txtadd.Text = txtadd.Text & CMBVIA.Text.Trim & ", "
                If CMBDISTRICT.Text.Trim <> "" Then txtadd.Text = txtadd.Text & CMBDISTRICT.Text.Trim & vbNewLine

                If cmbstate.Text.Trim <> "" Then
                    txtadd.Text = txtadd.Text & "" & cmbstate.Text.Trim & ","
                Else
                    txtadd.Text = txtadd.Text & vbNewLine
                End If

                If cmbcountry.Text.Trim <> "" Then
                    txtadd.Text = txtadd.Text & " " & cmbcountry.Text.Trim & "."
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDBILL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDBILL.Click
        Try
            If edit = True Then
                Dim OBJOPENING As New OpeningBills
                OBJOPENING.MdiParent = MDIMain
                OBJOPENING.TEMPNAME = cmbcmpname.Text.Trim
                OBJOPENING.FRMSTRING = "OPENINGBILLS"
                OBJOPENING.Show()
            Else
                MsgBox("Entry Allowed in Edit mode only", MsgBoxStyle.Critical)
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKTDSAPP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHKTDSAPP.CheckedChanged
        Try
            If cmbgroup.Text.Trim <> "" Then
                Dim objcmn As New ClsCommon
                Dim DT As DataTable = objcmn.search(" GROUP_SECONDARY", "", " GROUPMASTER", " AND GROUP_NAME = '" & cmbgroup.Text.Trim & "' AND GROUP_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    If DT.Rows(0).Item(0) = "Sundry Creditors" Or DT.Rows(0).Item(0) = "Sundry Debtors" Or DT.Rows(0).Item(0) = "Provisions" Or DT.Rows(0).Item(0) = "Loans & Advances" Or DT.Rows(0).Item(0) = "Loans" Then
                        GROUPTDS.Enabled = CHKTDSAPP.CheckState
                    Else
                        MsgBox("Not Applicable for this Group")
                        CHKTDSAPP.CheckState = CheckState.Unchecked
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBISLOWER_Validating(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBISLOWER.Validating
        Try
            CMBSECTION.Text = ""
            If CMBISLOWER.Text = "Yes" Then
                CMBSECTION.Enabled = True
                CMBSECTION.Focus()
            Else
                CMBSECTION.Enabled = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSECTION_Validating(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBSECTION.Validating
        Try
            TXTTDSRATE.Clear()
            If CMBSECTION.Text = "197" Then
                TXTTDSRATE.Enabled = True
                TXTTDSRATE.Focus()
            Else
                TXTTDSRATE.Enabled = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTTDSRATE_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTTDSRATE.KeyPress
        numdotkeypress(e, TXTTDSRATE, Me)
    End Sub

    Private Sub TXTLIMIT_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTLIMIT.KeyPress
        numdotkeypress(e, TXTLIMIT, Me)
    End Sub

    Private Sub CMBNATURE_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBNATURE.Enter
        Try
            If CMBNATURE.Text.Trim = "" Then FILLNATURE(CMBNATURE)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNATURE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNATURE.Validating
        Try
            If CMBNATURE.Text.Trim <> "" Then NATUREVALIDATE(CMBNATURE, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBTRANS.Enter
        Try
            If CMBTRANS.Text.Trim = "" Then fillname(CMBTRANS, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND  LEDGERS.ACC_TYPE='TRANSPORT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
    Private Sub cmbtrans_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBTRANS.Validating
        Try
            If CMBTRANS.Text.Trim <> "" Then namevalidate(CMBTRANS, cmbhotelcode, e, Me, TXTHOTELADD, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS'", "SUNDRY CREDITORS", "TRANSPORT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
    Private Sub cmbagent_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBAGENT.Enter
        Try
            If CMBAGENT.Text.Trim = "" Then fillname(CMBAGENT, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='AGENT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
    Private Sub cmbagent_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBAGENT.Validating
        Try
            If CMBAGENT.Text.Trim <> "" Then namevalidate(CMBAGENT, cmbhotelcode, e, Me, TXTHOTELADD, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS'", "SUNDRY CREDITORS", "AGENT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTTDSPER_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTTDSPER.KeyPress
        numdotkeypress(e, TXTTDSPER, Me)
    End Sub

    Private Sub TXTAGENTCOMM_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTAGENTCOMM.KeyPress
        numdotkeypress(e, TXTAGENTCOMM, Me)
    End Sub

    Private Sub CMBDISTRICT_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBDISTRICT.Enter
        Try
            If CMBDISTRICT.Text.Trim = "" Then fillDISTRICT(CMBDISTRICT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDISTRICT_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBDISTRICT.Validating
        Try
            If CMBDISTRICT.Text.Trim <> "" Then DISTRICTvalidate(CMBDISTRICT, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBVIA_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBVIA.Enter
        Try
            If CMBVIA.Text.Trim = "" Then fillVIA(CMBVIA)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBVIA_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBVIA.Validating
        Try
            If CMBVIA.Text.Trim <> "" Then VIAvalidate(CMBVIA, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYBANK_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBPARTYBANK.Enter
        Try
            If CMBPARTYBANK.Text.Trim = "" Then fillPARTYBANK(CMBPARTYBANK, edit)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYBANK_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBPARTYBANK.Validating
        Try
            If CMBPARTYBANK.Text.Trim <> "" Then PARTYBANKvalidate(CMBPARTYBANK, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYBANK1_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBPARTYBANK1.Enter
        Try
            If CMBPARTYBANK1.Text.Trim = "" Then fillPARTYBANK(CMBPARTYBANK1, edit)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYBANK1_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBPARTYBANK1.Validating
        Try
            If CMBPARTYBANK1.Text.Trim <> "" Then PARTYBANKvalidate(CMBPARTYBANK1, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKBANK1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHKBANK1.CheckedChanged
        Try
            If CHKBANK1.CheckState = CheckState.Checked Then
                CHKBANK.CheckState = CheckState.Unchecked
                CMBPARTYBANK.BackColor = Color.White
                CMBPARTYBANK1.BackColor = Color.Linen
                TXTACCNO.BackColor = Color.White
                TXTACCNO1.BackColor = Color.Linen
                TXTIFSC.BackColor = Color.White
                TXTIFSC1.BackColor = Color.Linen
                TXTBRANCH.BackColor = Color.White
                TXTBRANCH1.BackColor = Color.Linen
            Else
                CHKBANK.CheckState = CheckState.Checked
                CMBPARTYBANK1.BackColor = Color.White
                CMBPARTYBANK.BackColor = Color.Linen
                TXTACCNO1.BackColor = Color.White
                TXTACCNO.BackColor = Color.Linen
                TXTIFSC1.BackColor = Color.White
                TXTIFSC.BackColor = Color.Linen
                TXTBRANCH1.BackColor = Color.White
                TXTBRANCH.BackColor = Color.Linen
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKBANK_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHKBANK.CheckedChanged
        Try
            If CHKBANK.CheckState = CheckState.Checked Then
                CHKBANK1.CheckState = CheckState.Unchecked
                CMBPARTYBANK1.BackColor = Color.White
                CMBPARTYBANK.BackColor = Color.Linen
                TXTACCNO1.BackColor = Color.White
                TXTACCNO.BackColor = Color.Linen
                TXTIFSC1.BackColor = Color.White
                TXTIFSC.BackColor = Color.Linen
                TXTBRANCH1.BackColor = Color.White
                TXTBRANCH.BackColor = Color.Linen
            Else
                CHKBANK1.CheckState = CheckState.Checked
                CMBPARTYBANK.BackColor = Color.White
                CMBPARTYBANK1.BackColor = Color.Linen
                TXTACCNO.BackColor = Color.White
                TXTACCNO1.BackColor = Color.Linen
                TXTIFSC.BackColor = Color.White
                TXTIFSC1.BackColor = Color.Linen
                TXTBRANCH.BackColor = Color.White
                TXTBRANCH1.BackColor = Color.Linen
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDHINDI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDHINDI.Click
        Try
            Dim OBJHINDI As New HindiTyping
            OBJHINDI.TEMPHINDINAME = TXTHINDINAME.Text.Trim
            OBJHINDI.ShowDialog()
            TXTHINDINAME.Text = OBJHINDI.TXTNAME.Text.Trim
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class