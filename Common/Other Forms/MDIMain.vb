
Imports BL
Imports System.Windows.Forms
Imports WAProAPI

Public Class MDIMain

    Private Sub MDIMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If MsgBox("Wish To Exit?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then e.Cancel = True Else End
    End Sub

    Private Sub MDIMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Alt = True And e.Control = True And e.KeyCode = Keys.W Then
                Dim OBJWARP As New WarpRegister
                OBJWARP.MdiParent = Me
                OBJWARP.Show()
            ElseIf e.Alt = True And e.Control = True And e.KeyCode = Keys.E Then
                Dim OBJWEFT As New WeftRegister
                OBJWEFT.MdiParent = Me
                OBJWEFT.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MDIMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = CmpName & " (" & AccFrom & " - " & AccTo & ")"
        GETCONN()
        fillYEAR()
        SETENABILITY()

        'GET COMPANY'S DATA FOR VALIDATIONS OF EWB AND GST
        Dim OBJCMN As New ClsCommon
        Dim DT As DataTable = OBJCMN.search("ISNULL(CMP_EWBUSER,'') AS EWBUSER, ISNULL(CMP_EWBPASS,'') AS EWBPASS, ISNULL(CMP_GSTIN,'') AS CMPGSTIN, ISNULL(CMP_ZIPCODE,'') AS CMPPINCODE, ISNULL(CITY_NAME,'') AS CITYNAME, CAST(STATE_NAME AS VARCHAR(5)) AS STATENAME, CAST(STATE_REMARK AS VARCHAR(5)) AS STATECODE, ISNULL(NOOFEWAYBILLS,0) AS EWAYCOUNTER, ISNULL(CMP_DIVISIONOFF,'') AS CMPTYPE", "", " STATEMASTER INNER JOIN CMPMASTER ON STATE_ID = CMP_STATEID LEFT OUTER JOIN CITYMASTER ON CMP_FROMCITYID = CITY_ID LEFT OUTER JOIN EWAYCOUNTER ON CMP_ID = CMPID", " AND CMP_ID = " & CmpId)
        If DT.Rows.Count > 0 Then
            CMPEWBUSER = DT.Rows(0).Item("EWBUSER")
            CMPEWBPASS = DT.Rows(0).Item("EWBPASS")
            CMPGSTIN = DT.Rows(0).Item("CMPGSTIN")
            CMPPINCODE = DT.Rows(0).Item("CMPPINCODE")
            CMPCITYNAME = DT.Rows(0).Item("CITYNAME")
            CMPSTATENAME = DT.Rows(0).Item("STATENAME")
            CMPSTATECODE = DT.Rows(0).Item("STATECODE")
            CMPTYPE = DT.Rows(0).Item("CMPTYPE")

            DT = OBJCMN.search("ISNULL(SUM(NOOFEWAYBILLS),0) AS EWAYCOUNTER", "", " EWAYCOUNTER ", " AND CMPID = " & CmpId)
            CMPEWAYCOUNTER = Val(DT.Rows(0).Item("EWAYCOUNTER"))
            DT = OBJCMN.search("ISNULL(MAX(DATE), GETDATE()) AS EWAYEXPDATE", "", " EWAYCOUNTER ", " AND CMPID = " & CmpId)
            EWAYEXPDATE = Convert.ToDateTime(DT.Rows(0).Item("EWAYEXPDATE")).Date.AddYears(1)

            DT = OBJCMN.search("ISNULL(SUM(NOOFEINVOICE),0) AS EINVOICECOUNTER", "", " EINVOICECOUNTER ", " AND CMPID = " & CmpId)
            CMPEINVOICECOUNTER = Val(DT.Rows(0).Item("EINVOICECOUNTER"))
            DT = OBJCMN.search("ISNULL(MAX(DATE), GETDATE()) AS EINVOICEEXPDATE", "", " EINVOICECOUNTER ", " AND CMPID = " & CmpId)
            EINVOICEEXPDATE = Convert.ToDateTime(DT.Rows(0).Item("EINVOICEEXPDATE")).Date.AddYears(1)

        End If




        If ALLOWWHATSAPP = True Then
            Dim BASEURL As String = GETWHATSAPPBASEURL()
            If BASEURL <> "" Then
                APIMethods.BaseURL = BASEURL
            Else
                MsgBox("Whastapp Base URL Is Missing", MsgBoxStyle.Critical)
            End If
        End If


        'DONE BY GULKIT COZ THEY HAVE XP OPERATING SYSTEM AND IT GIVES ISSUE
        'WE HAVE GIVEN ONLY TRADING NOT MFG
        If ClientName = "STC" Then
            DBUSERNAME = ""
            Dbpassword = ""
            Dbsecurity = True
            ALLOWMFG = False
        End If

        If ClientName = "MASHOK" Then ALLOWSMS = True


    End Sub

    Sub SETENABILITY()
        Try

            If UserName = "Admin" Then
                RECODATA_MASTER.Enabled = True
                CMP_MASTER.Enabled = True
                YEAR_MASTER.Enabled = True
                ADMIN_MASTER.Enabled = True
                MERGELEDGER.Enabled = True
                MERGEPARAMETER_MENU.Enabled = True
                USERTRANSFER.Enabled = True
                YEARTRANSFER_MENU.Enabled = True
                STOCKTRANSFER_MENU.Enabled = True
                BLOCKUSER_MENU.Enabled = True
                PURCHASECONFIG_MASTER.Enabled = True
                PURRETURNCONFIG_MASTER.Enabled = True
                MATCHING_MASTER.Enabled = True
                MATCHINGADD.Enabled = True
                MATCHINGEDIT.Enabled = True
            Else
                'ONLY TO CHANGE PASSWORD
                ADMIN_MASTER.Enabled = True
                USERADD.Enabled = False
                USEREDIT.Enabled = True
            End If


            If ClientName = "SHREEJI" Then
                BEAMUPLOAD_MASTER.Visible = True
            End If


            For Each DTROW As DataRow In USERRIGHTS.Rows

                'MASTERS
                If DTROW(0).ToString = "GROUP MASTER" Then
                    If DTROW(3).ToString = True Then GROUP_MASTER.Visible = True Else GROUP_MASTER.Visible = False
                    If DTROW(1).ToString = True Then
                        GROUP_MASTER.Enabled = True
                        GROUPADD.Enabled = True
                    Else
                        GROUPADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(4) = True) Then
                        GROUP_MASTER.Enabled = True
                        GROUPEDIT.Enabled = True
                    Else
                        GROUPEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "ACCOUNTS MASTER" Then
                    If DTROW(3).ToString = True Then ACC_MASTER.Visible = True Else ACC_MASTER.Visible = False
                    If DTROW(1).ToString = True Then
                        ACC_MASTER.Enabled = True
                        ACCADD.Enabled = True
                        HSN_MASTER.Enabled = True
                        HSNADD.Enabled = True
                    Else
                        ACCADD.Enabled = False
                        HSNADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        ACC_MASTER.Enabled = True
                        ACCEDIT.Enabled = True
                        HSN_MASTER.Enabled = True
                        HSNEDIT.Enabled = True
                    Else
                        ACCEDIT.Enabled = False
                        HSNEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "DELIVERY AT MASTER" Then
                    If DTROW(3).ToString = True Then DELIVERYAT_MASTER.Visible = True Else DELIVERYAT_MASTER.Visible = False
                    If DTROW(1).ToString = True Then
                        DELIVERYAT_MASTER.Enabled = True
                        DELIVERYATADD.Enabled = True
                    Else
                        DELIVERYATADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        DELIVERYAT_MASTER.Enabled = True
                        DELIVERYATEDIT.Enabled = True
                    Else
                        DELIVERYATEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "REGISTER MASTER" Then
                    If DTROW(3).ToString = True Then REG_MASTER.Visible = True Else REG_MASTER.Visible = False
                    If DTROW(1).ToString = True Or (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        REG_MASTER.Enabled = True
                    End If

                ElseIf DTROW(0).ToString = "NARRATION MASTER" Then
                    If DTROW(3).ToString = True Then NARRATION_MASTER.Visible = True Else NARRATION_MASTER.Visible = False
                    If DTROW(1).ToString = True Then
                        NARRATION_MASTER.Enabled = True
                        NARRATIONADD.Enabled = True
                    Else
                        NARRATIONADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        NARRATION_MASTER.Enabled = True
                        NARRATIONEDIT.Enabled = True
                    Else
                        NARRATIONEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "PARTYBANK MASTER" Then
                    If DTROW(3).ToString = True Then PARTYBANK_MASTER.Visible = True Else PARTYBANK_MASTER.Visible = False
                    If DTROW(1).ToString = True Then
                        PARTYBANK_MASTER.Enabled = True
                        PARTYBANKADD.Enabled = True
                    Else
                        PARTYBANKADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        PARTYBANK_MASTER.Enabled = True
                        PARTYBANKEDIT.Enabled = True
                        PARTYBANKEDIT.Visible = True
                    Else
                        PARTYBANKEDIT.Enabled = False
                        PARTYBANKEDIT.Visible = False
                    End If

                ElseIf DTROW(0).ToString = "TYPE MASTER" Then
                    If DTROW(3).ToString = True Then TYPE_MASTER.Visible = True Else TYPE_MASTER.Visible = False
                    If DTROW(1).ToString = True Then
                        TYPE_MASTER.Enabled = True
                        TYPEADD.Enabled = True
                    Else
                        TYPEADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        TYPE_MASTER.Enabled = True
                    Else
                        TYPEEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "STORE ITEM MASTER" Then
                    If DTROW(3).ToString = True Then STORESITEM_MASTER.Visible = True Else STORESITEM_MASTER.Visible = False
                    If DTROW(1).ToString = True Then
                        STORESITEM_MASTER.Enabled = True
                        STORESITEMADD.Enabled = True
                        COLOR_MASTER.Enabled = True
                        COLORADD.Enabled = True
                    Else
                        STORESITEMADD.Enabled = False
                        COLORADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        STORESITEM_MASTER.Enabled = True
                        STORESITEMEDIT.Enabled = True
                        COLOR_MASTER.Enabled = True
                        COLOREDIT.Enabled = True
                    Else
                        STORESITEMEDIT.Enabled = False
                        COLOREDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "QUALITY MASTER" Then
                    If DTROW(3).ToString = True Then QUALITY_MASTER.Visible = True Else QUALITY_MASTER.Visible = False
                    If DTROW(1).ToString = True Then
                        QUALITY_MASTER.Enabled = True
                        QUALITYADD.Enabled = True
                        MACHINE_MASTER.Enabled = True
                        MACHINEADD.Enabled = True
                    Else
                        QUALITYADD.Enabled = False
                        MACHINEADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        QUALITY_MASTER.Enabled = True
                        QUALITYEDIT.Enabled = True
                        MACHINE_MASTER.Enabled = True
                        MACHINEEDIT.Enabled = True
                    Else
                        QUALITYEDIT.Enabled = False
                        MACHINEEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "GREYQUALITY MASTER" Then
                    If DTROW(3).ToString = True Then GREYQUALITY_MASTER.Visible = True Else GREYQUALITY_MASTER.Visible = False
                    If DTROW(1).ToString = True Then
                        GREYQUALITY_MASTER.Enabled = True
                        PIECETYPE_MASTER.Enabled = True
                        GREYQUALITYADD.Enabled = True
                        PIECETYPEADD.Enabled = True
                    Else
                        GREYQUALITYADD.Enabled = False
                        PIECETYPEADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        GREYQUALITY_MASTER.Enabled = True
                        PIECETYPE_MASTER.Enabled = True
                        GREYQUALITYEDIT.Enabled = True
                        PIECETYPEEDIT.Enabled = True
                    Else
                        GREYQUALITYEDIT.Enabled = False
                        PIECETYPEEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "BEAM MASTER" Then
                    If DTROW(3).ToString = True Then BEAM_MASTER.Visible = True Else BEAM_MASTER.Visible = False
                    If DTROW(1).ToString = True Then
                        BEAM_MASTER.Enabled = True
                        BEAMADD.Enabled = True
                    Else
                        BEAMADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        BEAM_MASTER.Enabled = True
                        BEAMEDIT.Enabled = True
                    Else
                        BEAMEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "LOOM MASTER" Then
                    If DTROW(3).ToString = True Then LOOM_MASTER.Visible = True Else LOOM_MASTER.Visible = False
                    If DTROW(1).ToString = True Then
                        LOOM_MASTER.Enabled = True
                    Else
                        LOOM_MASTER.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        LOOM_MASTER.Enabled = True
                    Else
                        LOOM_MASTER.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "TAX MASTER" Then
                    If DTROW(3).ToString = True Then TAX_MASTER.Visible = True Else TAX_MASTER.Visible = False
                    If DTROW(1).ToString = True Then
                        TAX_MASTER.Enabled = True
                        TAXADD.Enabled = True
                    Else
                        TAXADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        TAX_MASTER.Enabled = True
                        TAXEDIT.Enabled = True
                    Else
                        TAXEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "AREA MASTER" Then
                    If DTROW(3).ToString = True Then AREA_MASTER.Visible = True Else AREA_MASTER.Visible = False
                    If DTROW(1).ToString = True Then
                        AREA_MASTER.Enabled = True
                        AREAADD.Enabled = True
                    Else
                        AREAADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        AREA_MASTER.Enabled = True
                        AREAEDIT.Enabled = True
                    Else
                        AREAEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "CITY MASTER" Then
                    If DTROW(3).ToString = True Then CITY_MASTER.Visible = True Else CITY_MASTER.Visible = False
                    If DTROW(1).ToString = True Then
                        CITY_MASTER.Enabled = True
                        CITYADD.Enabled = True
                    Else
                        CITYADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        CITY_MASTER.Enabled = True
                        CITYEDIT.Enabled = True
                    Else
                        CITYEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "STATE MASTER" Then
                    If DTROW(3).ToString = True Then STATE_MASTER.Visible = True Else STATE_MASTER.Visible = False
                    If DTROW(1).ToString = True Then
                        STATE_MASTER.Enabled = True
                        STATEADD.Enabled = True
                    Else
                        STATEADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        STATE_MASTER.Enabled = True
                        STATEEDIT.Enabled = True
                    Else
                        STATEEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "COUNTRY MASTER" Then
                    If DTROW(3).ToString = True Then COUNTRY_MASTER.Visible = True Else COUNTRY_MASTER.Visible = False
                    If DTROW(1).ToString = True Then
                        COUNTRY_MASTER.Enabled = True
                        COUNTRYADD.Enabled = True
                    Else
                        COUNTRYADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        COUNTRY_MASTER.Enabled = True
                        COUNTRYEDIT.Enabled = True
                    Else
                        COUNTRYEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "DISTRICT MASTER" Then
                    If DTROW(3).ToString = True Then DISTRICT_MASTER.Visible = True Else DISTRICT_MASTER.Visible = False
                    If DTROW(1).ToString = True Then
                        DISTRICT_MASTER.Enabled = True
                        DISTRICTADD.Enabled = True
                    Else
                        DISTRICTADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        DISTRICT_MASTER.Enabled = True
                        DISTRICTEDIT.Enabled = True
                    Else
                        DISTRICTEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "VIA MASTER" Then
                    If DTROW(3).ToString = True Then VIA_MASTER.Visible = True Else VIA_MASTER.Visible = False
                    If DTROW(1).ToString = True Then
                        VIA_MASTER.Enabled = True
                        VIAADD.Enabled = True
                    Else
                        VIAADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        VIA_MASTER.Enabled = True
                        VIAEDIT.Enabled = True
                    Else
                        VIAEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "GODOWN MASTER" Then
                    If DTROW(3).ToString = True Then GODOWN_MASTER.Visible = True Else GODOWN_MASTER.Visible = False
                    If DTROW(1).ToString = True Then
                        GODOWN_MASTER.Enabled = True
                        GODOWNADD.Enabled = True
                    Else
                        GODOWNADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        GODOWN_MASTER.Enabled = True
                        GODOWNEDIT.Enabled = True
                    Else
                        GODOWNEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "OPENING" Then
                    If DTROW(3).ToString = True Then
                        OPENINGSTOCK_MENU.Visible = True
                        OPENINGSTORE_MENU.Visible = True
                        OPENINGACC_MENU.Visible = True
                        OPENINGSTOCKVALUE.Enabled = True
                    Else
                        OPENINGSTOCK_MENU.Visible = False
                        OPENINGSTORE_MENU.Visible = False
                        OPENINGACC_MENU.Visible = False
                        OPENINGSTOCKVALUE.Enabled = False
                    End If
                    If DTROW(1).ToString = True Or (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        OPENINGBILL_MASTER.Enabled = True
                        OPENINGBILLINT_MASTER.Enabled = True
                        OPENINGDRCR.Enabled = True
                        OPENINGBALANCE.Enabled = True

                        OPSO_MASTER.Enabled = True
                        OPSOADD.Enabled = True
                        OPSOEDIT.Enabled = True

                        OPPO_MASTER.Enabled = True
                        OPPOADD.Enabled = True
                        OPPOEDIT.Enabled = True

                        OPSTOCK_YARN.Enabled = True
                        OPSTOCK_YARNGODOWN.Enabled = True
                        OPSTOCK_WARPERYARN.Enabled = True
                        OPSTOCK_SIZERYARN.Enabled = True
                        OPSTOCK_WEAVERYARN.Enabled = True
                        OPSTOCK_DYEINGYARN.Enabled = True
                        OPSTOCK_JOBBERYARN.Enabled = True
                        OPSTOCK_MACHINEYARN.Enabled = True
                        OPSTOCK_ROLLS.Enabled = True
                        OPSTOCK_ROLLSSIZER.Enabled = True
                        OPSTOCK_BEAM.Enabled = True
                        OPSTOCK_BEAMGODOWN.Enabled = True
                        OPSTOCK_BEAMWEAVER.Enabled = True
                        OPSTOCK_CUTWEAVER.Enabled = True
                        OPSTOCK_GREY.Enabled = True
                        OPSTOCK_GREYPROCESSOR.Enabled = True
                        OPSTOCK_RETDATE.Enabled = True
                        OPSTOCK_FINISHEDBALE.Enabled = True
                        OPSTOCK_FINISHEDBALEPROCESSOR.Enabled = True
                        OPSTOCK_FINISHEDBALEJOBBER.Enabled = True
                        OPSTOCK_FINISHEDSAREEJOBBER.Enabled = True
                        OPSTOCK_FINISHEDSAREE.Enabled = True

                        OPSTOCK_PIPEGODOWN.Enabled = True
                        OPSTOCK_PIPEPARTY.Enabled = True
                        OPSTOCK_STORES.Enabled = True

                    End If

                    'PURCHASE
                ElseIf DTROW(0).ToString = "PURCHASE ORDER" Then
                    If DTROW(3).ToString = True Then
                        PO_MASTER.Visible = True
                        PO_TOOL.Visible = True
                        PO_TOOLSTRIP.Visible = True
                    Else
                        PO_MASTER.Visible = False
                        PO_TOOL.Visible = False
                        PO_TOOLSTRIP.Visible = False
                    End If
                    If DTROW(1).ToString = True Then
                        PO_MASTER.Enabled = True
                        POADD.Enabled = True
                        POADD.Visible = True
                        PO_TOOL.Enabled = True
                    Else
                        POADD.Enabled = False
                        POADD.Visible = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        PO_MASTER.Enabled = True
                        POEDIT.Enabled = True
                        POEDIT.Visible = True
                        PO_TOOL.Enabled = True
                    Else
                        POEDIT.Enabled = False
                        POEDIT.Visible = False
                    End If



                ElseIf DTROW(0).ToString = "MATERIAL DESPATCH" Then
                    If DTROW(3).ToString = True Then
                        MATDES_MASTER.Visible = True
                        PO_TOOL.Visible = True
                        PO_TOOLSTRIP.Visible = True
                    Else
                        PO_MASTER.Visible = False
                        PO_TOOL.Visible = False
                        PO_TOOLSTRIP.Visible = False
                    End If
                    If DTROW(1).ToString = True Then
                        MATDES_MASTER.Enabled = True
                        MATDESADD.Enabled = True
                    Else
                        MATDESADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        MATDES_MASTER.Enabled = True
                        MATDESEDIT.Enabled = True
                    Else
                        MATDESEDIT.Enabled = False
                    End If


                ElseIf DTROW(0).ToString = "GRN" Then
                    If DTROW(1).ToString = True Then
                        MATDES_MASTER.Enabled = True
                        DO_MASTER.Enabled = True
                        DO_TOOL.Enabled = True
                        DOADD.Enabled = True
                        DOADD.Visible = True
                        MATDESADD.Enabled = True
                        MATDESADD.Visible = True
                        GRN_MASTER.Enabled = True
                        GRNADD.Enabled = True
                        GRNADD.Visible = True
                        GRN_TOOL.Enabled = True
                        LIFTING_MASTER.Enabled = True
                    Else
                        GRNADD.Enabled = False
                        GRNADD.Visible = False
                        MATDESADD.Enabled = False
                        MATDESADD.Visible = False
                        DOADD.Enabled = False
                        DOADD.Visible = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        MATDES_MASTER.Enabled = True
                        DO_MASTER.Enabled = True
                        DO_TOOL.Enabled = True
                        GRN_MASTER.Enabled = True
                        GRNEDIT.Enabled = True
                        GRNEDIT.Visible = True
                        GRN_TOOL.Enabled = True
                        MATDESEDIT.Enabled = True
                        MATDESEDIT.Visible = True
                        DOEDIT.Enabled = True
                        DOEDIT.Visible = True
                        LIFTING_MASTER.Enabled = True
                    Else
                        GRNEDIT.Enabled = False
                        GRNEDIT.Visible = False
                        MATDESEDIT.Enabled = False
                        MATDESEDIT.Visible = False
                        DOEDIT.Enabled = False
                        DOEDIT.Visible = False
                    End If


                ElseIf DTROW(0).ToString = "PURCHASE INVOICE" Then
                    If DTROW(1).ToString = True Then
                        PURINV_MASTER.Enabled = True
                        PURCHASE_TOOL.Enabled = True
                        PURINVADD.Enabled = True
                        PURINVADD.Visible = True
                    Else
                        PURINVADD.Enabled = False
                        PURINVADD.Visible = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        PURINV_MASTER.Enabled = True
                        PURCHASE_TOOL.Enabled = True
                        PURINVEDIT.Enabled = True
                        PURINVEDIT.Visible = True
                    Else
                        PURINVEDIT.Enabled = False
                        PURINVEDIT.Visible = False
                    End If


                ElseIf DTROW(0).ToString = "PURCHASE RETURN" Then
                    If DTROW(1).ToString = True Then
                        PURRET_MASTER.Enabled = True
                        PURRETADD.Enabled = True
                        PURRETADD.Visible = True
                    Else
                        PURRETADD.Enabled = False
                        PURRETADD.Visible = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        PURRET_MASTER.Enabled = True
                        PURRETEDIT.Enabled = True
                        PURRETEDIT.Visible = True
                    Else
                        PURRETEDIT.Enabled = False
                        PURRETEDIT.Visible = False
                    End If


                    'MFG
                ElseIf DTROW(0).ToString = "MFG" Then
                    If DTROW(1).ToString = True Then
                        MFGTOOLSTRIP_MENU.Visible = True
                        PROGRAM_MASTER.Enabled = True
                        YARNISSUEWARPER_MASTER.Enabled = True
                        YARNISSUEADDA_MASTER.Enabled = True
                        YARNISSUESIZER_MASTER.Enabled = True
                        YARNISSUEWEAVER_MASTER.Enabled = True
                        YARNISSUEMAC_MASTER.Enabled = True
                        YARNISSUEDYEING_MASTER.Enabled = True

                        YARNISSUEWARPER_TOOL.Enabled = True
                        YARNISSUEADDA_TOOL.Enabled = True
                        YARNISSUESIZER_TOOL.Enabled = True
                        YARNISSUEWEAVER_TOOL.Enabled = True
                        YARNISSUEWINDING_TOOL.Enabled = True


                        YARNRECEIVEDFROMDYEING_MASTER.Enabled = True
                        YARNRECEIVEDMACHINE_MASTER.Enabled = True


                        YARNRETURNWARPER_MASTER.Enabled = True
                        YARNRETURNADDA_MASTER.Enabled = True
                        YARNRETURNSIZER_MASTER.Enabled = True
                        YARNRETURNWEAVER_MASTER.Enabled = True
                        YARNRETURNDYEING_MASTER.Enabled = True
                        YARNRETURNJOBBER_MASTER.Enabled = True
                        YARNRETURNMACHINE_MASTER.Enabled = True

                        ROLLSRECD_MASTER.Enabled = True
                        ROLLSISSUE_MASTER.Enabled = True
                        ROLLSRETURN_MASTER.Enabled = True

                        BEAMRECWARPER_MASTER.Enabled = True
                        BEAMREC_MASTER.Enabled = True
                        BEAMISSUE_MASTER.Enabled = True
                        BEAMRETURN_MASTER.Enabled = True

                        BEAMUPLOAD_MASTER.Enabled = True
                        GREYRECWEAVER_MASTER.Enabled = True
                        GREYRECWEAVERSUMM_MASTER.Enabled = True
                        GREYCHECKING_MASTER.Enabled = True
                        GREYCHECKINGADD.Enabled = True

                        BALERETURNPROCESSOR_MASTER.Enabled = True
                        BALERETURNPROCESSORADD.Enabled = True

                        BALERECD_MASTER.Enabled = True
                        BALERECDADD.Enabled = True

                        BALEOPEN_MASTER.Enabled = True
                        BALEOPENADD.Enabled = True

                        YARNISSUEWINDING_MASTER.Enabled = True
                        YARNRECWINDING_MASTER.Enabled = True

                        WARPERYARNWASTAGE_MASTER.Enabled = True
                        SIZERYARNWASTAGE_MASTER.Enabled = True
                        WEAVERYARNWASTAGE_MASTER.Enabled = True
                        DYEINGYARNWASTAGE_MASTER.Enabled = True
                        GODOWNYARNWASTAGE_MASTER.Enabled = True
                        JOBBERYARNWASTAGE_MASTER.Enabled = True
                        MACHINEYARNWASTAGE_MASTER.Enabled = True

                        SIZERBEAMWASTAGE_MASTER.Enabled = True
                        WEAVERBEAMWASTAGE_MASTER.Enabled = True
                        GODOWNBEAMWASTAGE_MASTER.Enabled = True

                        GODOWNGREYWASTAGE_MASTER.Enabled = True

                        PROGRAMADD.Enabled = True

                        YARNISSUEWARPERADD.Enabled = True
                        YARNISSUEADDAADD.Enabled = True
                        YARNISSUESIZERADD.Enabled = True
                        YARNISSUEWEAVERADD.Enabled = True
                        YARNISSUEMACADD.Enabled = True
                        YARNISSUEDYEINGADD.Enabled = True
                        YARNRECDDYEINGADD.Enabled = True
                        YARNRECEIVEDMACHINEADD.Enabled = True

                        YARNRETURNWARPERADD.Enabled = True
                        YARNRETURNADDAADD.Enabled = True
                        YARNRETURNSIZERADD.Enabled = True
                        YARNRETURNWEAVERADD.Enabled = True
                        YARNRETURNDYEINGADD.Enabled = True
                        YARNRETURNJOBBERADD.Enabled = True
                        YARNRETURNMACHINEADD.Enabled = True

                        ROLLSISSUEADD.Enabled = True
                        ROLLSRECDADD.Enabled = True
                        ROLLSRETURNADD.Enabled = True

                        BEAMRECWARPERADD.Enabled = True
                        BEAMRECADD.Enabled = True
                        BEAMISSUEADD.Enabled = True
                        BEAMRETURNADD.Enabled = True

                        BEAMUPLOADADD.Enabled = True
                        GREYRECWEAVERADD.Enabled = True
                        GREYRECWEAVERSUMMADD.Enabled = True

                        YARNISSUEWINDINGADD.Enabled = True
                        YARNRECWINDINGADD.Enabled = True

                        WARPERYARNWASTAGEADD.Enabled = True
                        SIZERYARNWASTAGEADD.Enabled = True
                        WEAVERYARNWASTAGEADD.Enabled = True
                        DYEINGYARNWASTAGEADD.Enabled = True
                        GODOWNYARNWASTAGEADD.Enabled = True
                        JOBBERYARNWASTAGEADD.Enabled = True
                        MACHINEYARNWASTAGEADD.Enabled = True

                        SIZERBEAMWASTAGEADD.Enabled = True
                        WEAVERBEAMWASTAGEADD.Enabled = True
                        GODOWNBEAMWASTAGEADD.Enabled = True

                        GODOWNGREYWASTAGEADD.Enabled = True
                    Else
                        MFGTOOLSTRIP_MENU.Visible = False

                        PROGRAMADD.Enabled = False
                        YARNISSUEWARPERADD.Enabled = False
                        YARNISSUEADDAADD.Enabled = False
                        YARNISSUESIZERADD.Enabled = False
                        YARNISSUEWEAVERADD.Enabled = False
                        YARNISSUEMACADD.Enabled = False
                        YARNISSUEDYEINGADD.Enabled = False
                        YARNRECDDYEINGADD.Enabled = False
                        YARNRECEIVEDMACHINEADD.Enabled = False

                        YARNRETURNWARPERADD.Enabled = False
                        YARNRETURNADDAADD.Enabled = False
                        YARNRETURNSIZERADD.Enabled = False
                        YARNRETURNWEAVERADD.Enabled = False
                        YARNRETURNDYEINGADD.Enabled = False
                        YARNRETURNJOBBERADD.Enabled = False
                        YARNRETURNMACHINEADD.Enabled = False

                        ROLLSISSUEADD.Enabled = False
                        ROLLSRECDADD.Enabled = False
                        ROLLSRETURNADD.Enabled = False

                        BEAMRECWARPERADD.Enabled = False
                        BEAMRECADD.Enabled = False
                        BEAMISSUEADD.Enabled = False
                        BEAMRETURNADD.Enabled = False

                        BEAMUPLOADADD.Enabled = False
                        GREYRECWEAVERADD.Enabled = False
                        GREYRECWEAVERSUMMADD.Enabled = False
                        GREYCHECKINGADD.Enabled = False

                        BALERETURNPROCESSORADD.Enabled = False

                        BALERECDADD.Enabled = False
                        BALEOPENADD.Enabled = False

                        YARNISSUEWINDINGADD.Enabled = False
                        YARNRECWINDINGADD.Enabled = False

                        WARPERYARNWASTAGEADD.Enabled = False
                        SIZERYARNWASTAGEADD.Enabled = False
                        WEAVERYARNWASTAGEADD.Enabled = False
                        DYEINGYARNWASTAGEADD.Enabled = False
                        GODOWNYARNWASTAGEADD.Enabled = False
                        JOBBERYARNWASTAGEADD.Enabled = False
                        MACHINEYARNWASTAGEADD.Enabled = False

                        SIZERBEAMWASTAGEADD.Enabled = False
                        WEAVERBEAMWASTAGEADD.Enabled = False
                        GODOWNBEAMWASTAGEADD.Enabled = False

                        GODOWNGREYWASTAGEADD.Enabled = False
                    End If

                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then

                        PROGRAM_MASTER.Enabled = True
                        YARNISSUEWARPER_MASTER.Enabled = True
                        YARNISSUEADDA_MASTER.Enabled = True
                        YARNISSUESIZER_MASTER.Enabled = True
                        YARNISSUEWEAVER_MASTER.Enabled = True
                        YARNISSUEMAC_MASTER.Enabled = True
                        YARNISSUEDYEING_MASTER.Enabled = True
                        YARNRECEIVEDFROMDYEING_MASTER.Enabled = True
                        YARNRECEIVEDMACHINE_MASTER.Enabled = True

                        YARNRETURNWARPER_MASTER.Enabled = True
                        YARNRETURNADDA_MASTER.Enabled = True
                        YARNRETURNSIZER_MASTER.Enabled = True
                        YARNRETURNWEAVER_MASTER.Enabled = True
                        YARNRETURNDYEING_MASTER.Enabled = True
                        YARNRETURNJOBBER_MASTER.Enabled = True
                        YARNRETURNMACHINE_MASTER.Enabled = True

                        ROLLSRECD_MASTER.Enabled = True
                        ROLLSISSUE_MASTER.Enabled = True
                        ROLLSRETURN_MASTER.Enabled = True

                        BEAMRECWARPER_MASTER.Enabled = True
                        BEAMREC_MASTER.Enabled = True
                        BEAMISSUE_MASTER.Enabled = True
                        BEAMRETURN_MASTER.Enabled = True

                        BEAMUPLOAD_MASTER.Enabled = True
                        GREYRECWEAVER_MASTER.Enabled = True
                        GREYRECWEAVERSUMM_MASTER.Enabled = True
                        GREYCHECKING_MASTER.Enabled = True
                        GREYCHECKINGEDIT.Enabled = True

                        BALERETURNPROCESSOR_MASTER.Enabled = True
                        BALERETURNPROCESSOREDIT.Enabled = True


                        BALERECD_MASTER.Enabled = True
                        BALERECDEDIT.Enabled = True

                        BALEOPEN_MASTER.Enabled = True
                        BALEOPENEDIT.Enabled = True


                        YARNISSUEWINDING_MASTER.Enabled = True
                        YARNRECWINDING_MASTER.Enabled = True

                        WARPERYARNWASTAGE_MASTER.Enabled = True
                        SIZERYARNWASTAGE_MASTER.Enabled = True
                        WEAVERYARNWASTAGE_MASTER.Enabled = True
                        DYEINGYARNWASTAGE_MASTER.Enabled = True
                        GODOWNYARNWASTAGE_MASTER.Enabled = True
                        JOBBERYARNWASTAGE_MASTER.Enabled = True
                        MACHINEYARNWASTAGE_MASTER.Enabled = True

                        SIZERBEAMWASTAGE_MASTER.Enabled = True
                        WEAVERBEAMWASTAGE_MASTER.Enabled = True
                        GODOWNBEAMWASTAGE_MASTER.Enabled = True

                        GODOWNGREYWASTAGE_MASTER.Enabled = True

                        PROGRAMEDIT.Enabled = True

                        YARNISSUEWARPEREDIT.Enabled = True
                        YARNISSUEADDAEDIT.Enabled = True
                        YARNISSUESIZEREDIT.Enabled = True
                        YARNISSUEWEAVEREDIT.Enabled = True
                        YARNISSUEMACEDIT.Enabled = True
                        YARNISSUEDYEINGEDIT.Enabled = True
                        YARNRECDDYEINGEDIT.Enabled = True
                        YARNRECEIVEDMACHINEEDIT.Enabled = True

                        YARNRETURNWARPEREDIT.Enabled = True
                        YARNRETURNADDAEDIT.Enabled = True
                        YARNRETURNSIZEREDIT.Enabled = True
                        YARNRETURNWEAVEREDIT.Enabled = True
                        YARNRETURNDYEINGEDIT.Enabled = True
                        YARNRETURNJOBBEREDIT.Enabled = True
                        YARNRETURNMACHINEEDIT.Enabled = True

                        ROLLSRECDEDIT.Enabled = True
                        ROLLSISSUEEDIT.Enabled = True
                        ROLLSRETURNEDIT.Enabled = True

                        BEAMRECWARPEREDIT.Enabled = True
                        BEAMRECEDIT.Enabled = True
                        BEAMISSUEEDIT.Enabled = True
                        BEAMRETURNEDIT.Enabled = True

                        BEAMUPLOADEDIT.Enabled = True
                        GREYRECWEAVEREDIT.Enabled = True
                        GREYRECWEAVERSUMMEDIT.Enabled = True
                        GREYCHECKINGEDIT.Enabled = True

                        YARNISSUEWINDINGEDIT.Enabled = True
                        YARNRECWINDINEDIT.Enabled = True

                        WARPERYARNWASTAGEEDIT.Enabled = True
                        SIZERYARNWASTAGEEDIT.Enabled = True
                        WEAVERYARNWASTAGEEDIT.Enabled = True
                        DYEINGYARNWASTAGEEDIT.Enabled = True
                        GODOWNYARNWASTAGEEDIT.Enabled = True
                        JOBBERYARNWASTAGEEDIT.Enabled = True
                        MACHINEYARNWASTAGEEDIT.Enabled = True

                        SIZERBEAMWASTAGEEDIT.Enabled = True
                        WEAVERBEAMWASTAGEEDIT.Enabled = True
                        GODOWNBEAMWASTAGEEDIT.Enabled = True

                        GODOWNGREYWASTAGEEDIT.Enabled = True
                    Else
                        PROGRAMEDIT.Enabled = False
                        YARNISSUEWARPEREDIT.Enabled = False
                        YARNISSUEADDAEDIT.Enabled = False
                        YARNISSUESIZEREDIT.Enabled = False
                        YARNISSUEWEAVEREDIT.Enabled = False
                        YARNISSUEMACEDIT.Enabled = False
                        YARNISSUEDYEINGEDIT.Enabled = False
                        YARNRECDDYEINGEDIT.Enabled = False
                        YARNRECEIVEDMACHINEEDIT.Enabled = False

                        YARNRETURNWARPEREDIT.Enabled = False
                        YARNRETURNADDAEDIT.Enabled = False
                        YARNRETURNSIZEREDIT.Enabled = False
                        YARNRETURNWEAVEREDIT.Enabled = False
                        YARNRETURNDYEINGEDIT.Enabled = False
                        YARNRETURNJOBBEREDIT.Enabled = False
                        YARNRETURNMACHINEEDIT.Enabled = False

                        ROLLSRECDEDIT.Enabled = False
                        ROLLSISSUEEDIT.Enabled = False
                        ROLLSRETURNEDIT.Enabled = False

                        BEAMRECWARPEREDIT.Enabled = False
                        BEAMRECEDIT.Enabled = False
                        BEAMISSUEEDIT.Enabled = False
                        BEAMRETURNEDIT.Enabled = False

                        BEAMUPLOADEDIT.Enabled = False
                        GREYRECWEAVEREDIT.Enabled = False
                        GREYRECWEAVERSUMMEDIT.Enabled = False
                        GREYCHECKINGEDIT.Enabled = False

                        BALERETURNPROCESSOREDIT.Enabled = True

                        BALERECDEDIT.Enabled = False
                        BALEOPENEDIT.Enabled = False

                        YARNISSUEWINDINGEDIT.Enabled = False
                        YARNRECWINDINEDIT.Enabled = False

                        WARPERYARNWASTAGEEDIT.Enabled = False
                        SIZERYARNWASTAGEEDIT.Enabled = False
                        WEAVERYARNWASTAGEEDIT.Enabled = False
                        DYEINGYARNWASTAGEEDIT.Enabled = False
                        GODOWNYARNWASTAGEEDIT.Enabled = False
                        JOBBERYARNWASTAGEEDIT.Enabled = False
                        MACHINEYARNWASTAGEEDIT.Enabled = False

                        SIZERBEAMWASTAGEEDIT.Enabled = False
                        WEAVERBEAMWASTAGEEDIT.Enabled = False
                        GODOWNBEAMWASTAGEEDIT.Enabled = False

                        GODOWNGREYWASTAGEEDIT.Enabled = False
                    End If


                ElseIf DTROW(0).ToString = "JOB OUT" Then

                    If DTROW(1).ToString = True Then
                        SAREEJOBOUT_MASTER.Enabled = True
                        SAREEJOBOUTADD.Enabled = True
                    Else
                        SAREEJOBOUTADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        SAREEJOBOUT_MASTER.Enabled = True
                        SAREEJOBOUTEDIT.Enabled = True
                    Else
                        SAREEJOBOUTEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "JOB IN" Then

                    If DTROW(1).ToString = True Then
                        SAREEJOBIN_MASTER.Enabled = True
                        SAREEJOBINADD.Enabled = True
                    Else
                        SAREEJOBINADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        SAREEJOBIN_MASTER.Enabled = True
                        SAREEJOBINEDIT.Enabled = True
                    Else
                        SAREEJOBINEDIT.Enabled = False
                    End If

                    'SALE
                ElseIf DTROW(0).ToString = "SALE ORDER" Then
                    If DTROW(1).ToString = True Then
                        SO_MASTER.Enabled = True
                        SOADD.Enabled = True
                        SO_TOOL.Enabled = True
                    Else
                        SOADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        SO_MASTER.Enabled = True
                        SOEDIT.Enabled = True
                        SO_TOOL.Enabled = True
                    Else
                        SOEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "PACKING SLIP" Then
                    If DTROW(1).ToString = True Then
                        PS_MASTER.Enabled = True
                        PSADD.Enabled = True
                    Else
                        PSADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        PS_MASTER.Enabled = True
                        PSEDIT.Enabled = True
                    Else
                        PSEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "GDN" Then
                    If DTROW(1).ToString = True Then
                        CHALLAN_MASTER.Enabled = True
                        CHALLANFINISHED_MASTER.Enabled = True
                        GREYRETURN_MASTER.Enabled = True
                        GREYRETURN_TOOL.Enabled = True
                        CHALLANADD.Enabled = True
                        CHALLANFINISHEDADD.Enabled = True
                        CHALLAN_TOOL.Enabled = True
                        GREYRETURNADD.Enabled = True
                    Else
                        CHALLANADD.Enabled = False
                        CHALLANFINISHEDADD.Enabled = False
                        GREYRETURNADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        CHALLAN_MASTER.Enabled = True
                        CHALLANFINISHED_MASTER.Enabled = True
                        GREYRETURN_MASTER.Enabled = True
                        GREYRETURN_TOOL.Enabled = True
                        CHALLANEDIT.Enabled = True
                        CHALLANFINISHEDEDIT.Enabled = True
                        CHALLAN_TOOL.Enabled = True
                        GREYRETURNEDIT.Enabled = True
                    Else
                        CHALLANEDIT.Enabled = False
                        CHALLANFINISHEDEDIT.Enabled = False
                        GREYRETURNEDIT.Enabled = False
                    End If


                ElseIf DTROW(0).ToString = "SALE INVOICE" Then
                    If DTROW(1).ToString = True Then
                        SALE_MASTER.Enabled = True
                        SALE_TOOL.Enabled = True
                        FORMENTRY_MASTER.Enabled = True
                        FORMDETAILS.Enabled = True
                        FORMSUMMARY.Enabled = True
                        CFORMAPPLICATION.Enabled = True
                        FORMREPORTS.Enabled = True
                        SALEADD.Enabled = True
                    Else
                        SALEADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        SALE_MASTER.Enabled = True
                        SALE_TOOL.Enabled = True
                        FORMENTRY_MASTER.Enabled = True
                        FORMDETAILS.Enabled = True
                        FORMSUMMARY.Enabled = True
                        CFORMAPPLICATION.Enabled = True
                        FORMREPORTS.Enabled = True
                        SALEEDIT.Enabled = True
                    Else
                        SALEEDIT.Enabled = False
                    End If


                ElseIf DTROW(0).ToString = "SALE RETURN" Then
                    If DTROW(1).ToString = True Then
                        SALERET_MASTER.Enabled = True
                        SALERETADD.Enabled = True
                    Else
                        SALERETADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        SALERET_MASTER.Enabled = True
                        SALERETEDIT.Enabled = True
                    Else
                        SALERETEDIT.Enabled = False
                    End If

                    'ACCOUNTS
                ElseIf DTROW(0).ToString = "PAYMENT" Then
                    If DTROW(1).ToString = True Then
                        PAY_MASTER.Enabled = True
                        PAYMENT_TOOL.Enabled = True
                        PAYADD.Enabled = True
                    Else
                        PAYADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        PAY_MASTER.Enabled = True
                        PAYMENT_TOOL.Enabled = True
                        PAYEDIT.Enabled = True
                    Else
                        PAYEDIT.Enabled = False
                    End If


                ElseIf DTROW(0).ToString = "RECEIPT" Then
                    If DTROW(1).ToString = True Then
                        REC_MASTER.Enabled = True
                        RECEIPT_TOOL.Enabled = True
                        RECADD.Enabled = True
                    Else
                        RECADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        REC_MASTER.Enabled = True
                        RECEIPT_TOOL.Enabled = True
                        RECEDIT.Enabled = True
                    Else
                        RECEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "CONTRA VOUCHER" Then
                    If DTROW(1).ToString = True Then
                        CONTRA_MASTER.Enabled = True
                        CONTRA_TOOL.Enabled = True
                        CONTRAADD.Enabled = True
                    Else
                        CONTRAADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        CONTRA_MASTER.Enabled = True
                        CONTRA_TOOL.Enabled = True
                        CONTRAEDIT.Enabled = True
                    Else
                        CONTRAEDIT.Enabled = False
                    End If


                ElseIf DTROW(0).ToString = "JOURNAL VOUCHER" Then
                    If DTROW(1).ToString = True Then
                        JV_MASTER.Enabled = True
                        JV_TOOL.Enabled = True
                        JVADD.Enabled = True
                    Else
                        JVADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        JV_MASTER.Enabled = True
                        JV_TOOL.Enabled = True
                        JVEDIT.Enabled = True
                    Else
                        JVEDIT.Enabled = False
                    End If


                ElseIf DTROW(0).ToString = "DEBIT NOTE" Then
                    If DTROW(1).ToString = True Then
                        DEBIT_MASTER.Enabled = True
                        DEBITADD.Enabled = True
                    Else
                        DEBITADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        DEBIT_MASTER.Enabled = True
                        DEBITEDIT.Enabled = True
                    Else
                        DEBITEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "CREDIT NOTE" Then
                    If DTROW(1).ToString = True Then
                        CREDIT_MASTER.Enabled = True
                        CREDITADD.Enabled = True
                    Else
                        CREDITADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        CREDIT_MASTER.Enabled = True
                        CREDITEDIT.Enabled = True
                    Else
                        CREDITEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "VOUCHER ENTRY" Then
                    If DTROW(1).ToString = True Then
                        VOUCHER_MASTER.Enabled = True
                        VOUCHERADD.Enabled = True
                    Else
                        VOUCHERADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        VOUCHER_MASTER.Enabled = True
                        VOUCHEREDIT.Enabled = True
                    Else
                        VOUCHEREDIT.Enabled = False
                    End If


                    'STORES
                ElseIf DTROW(0).ToString = "STORES" Then
                    If DTROW(1).ToString = True Then
                        STORESITEM_MASTER.Enabled = True
                        STORESITEMADD.Enabled = True
                        PIPEINWARD_MASTER.Enabled = True
                        PIPETRANSFER_MASTER.Enabled = True
                        PIPEDESTROYED_MASTER.Enabled = True
                        PIPEINWARDADD.Enabled = True
                        PIPEOUTWARDADD.Enabled = True
                        PIPEDESTROYEDADD.Enabled = True
                        STORESINWARD_MASTER.Enabled = True
                        STORESINWARDADD.Enabled = True
                    Else
                        STORESITEMADD.Enabled = False
                        PIPEOUTWARDADD.Enabled = False
                        PIPEINWARDADD.Enabled = False
                        PIPEDESTROYEDADD.Enabled = False
                        STORESINWARDADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        STORESITEM_MASTER.Enabled = True
                        STORESITEMEDIT.Enabled = True
                        PIPEINWARD_MASTER.Enabled = True
                        PIPETRANSFER_MASTER.Enabled = True
                        PIPEDESTROYED_MASTER.Enabled = True
                        PIPEINWARDEDIT.Enabled = True
                        PIPEOUTWARDEDIT.Enabled = True
                        PIPEDESTROYEDEDIT.Enabled = True
                        STORESINWARD_MASTER.Enabled = True
                        STORESINWARDEDIT.Enabled = True
                    Else
                        STORESITEMEDIT.Enabled = False
                        PIPEINWARDEDIT.Enabled = False
                        PIPEOUTWARDEDIT.Enabled = False
                        PIPEDESTROYEDEDIT.Enabled = False
                        STORESINWARDEDIT.Enabled = False
                    End If



                    'REPORTS
                ElseIf DTROW(0).ToString = "PURCHASE REPORTS" Then
                    If (DTROW(1) = True) Or (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        PUR_REPORTS.Enabled = True
                    End If


                ElseIf DTROW(0).ToString = "SALE REPORTS" Then
                    If (DTROW(1) = True) Or (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        SALE_REPORTS.Enabled = True
                    End If


                ElseIf DTROW(0).ToString = "STOCK REPORTS" Then
                    If (DTROW(1) = True) Or (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        STOCKS.Enabled = True
                    End If


                ElseIf DTROW(0).ToString = "ACCOUNT REPORTS" Then
                    If (DTROW(1) = True) Or (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        ACC_REPORTS.Enabled = True
                    End If


                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillYEAR()
        Try
            Dim objcmn As New ClsCommon
            Dim dt As DataTable
            Dim whereclause As String = ""
            'If UserName <> "Admin" Then
            '    dt = objcmn.search(" distinct user_YEARid", "", "UserMaster", " and User_Name = '" & UserName & "' and user_cmpid = " & CmpId)
            '    For Each DTROW As DataRow In dt.Rows
            '        If whereclause = "" Then
            '            whereclause = " AND YEAR_ID IN (" & DTROW(0)
            '        Else
            '            whereclause = whereclause & "," & DTROW(0)
            '        End If
            '    Next
            '    whereclause = whereclause & ")"
            'End If

            dt = objcmn.search(" distinct user_YEARid", "", "UserMaster", " and User_Name = '" & UserName & "' and user_cmpid = " & CmpId)
            For Each DTROW As DataRow In dt.Rows
                If whereclause = "" Then
                    whereclause = " AND YEAR_ID IN (" & DTROW(0)
                Else
                    whereclause = whereclause & "," & DTROW(0)
                End If
            Next
            whereclause = whereclause & ")"

            dt = objcmn.search("CONVERT(char(11), year_startdate , 6) + '   ---   ' + CONVERT(char(11), year_enddate , 6) ", "", "YearMaster INNER JOIN cmpmaster on cmp_id = year_cmpid", whereclause & " order BY YEAR_ID")
            For Each DTROW As DataRow In dt.Rows
                cmbselectcmp.DropDownItems.Add(DTROW(0))
            Next
            cmbselectcmp.Text = CmpName
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewGroupToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GROUPADD.Click
        Try
            Dim objGroupMaster As New GroupMaster
            objGroupMaster.MdiParent = Me
            objGroupMaster.Show()
            objGroupMaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub AddNewCityToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CITYADD.Click
        Try
            Dim objcitymaster As New citymaster
            objcitymaster.MdiParent = Me
            objcitymaster.frmstring = "CITYMASTER"
            objcitymaster.Show()
            objcitymaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub AddNewStateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles STATEADD.Click
        Try
            Dim objcitymaster As New citymaster
            objcitymaster.MdiParent = Me
            objcitymaster.frmstring = "STATEMASTER"
            objcitymaster.Show()
            objcitymaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub AddNewCountryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles COUNTRYADD.Click
        Try
            Dim objcitymaster As New citymaster
            objcitymaster.MdiParent = Me
            objcitymaster.frmstring = "COUNTRYMASTER"
            objcitymaster.Show()
            objcitymaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub AddNewAreaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AREAADD.Click
        Try
            Dim objcitymaster As New citymaster
            objcitymaster.MdiParent = Me
            objcitymaster.frmstring = "AREAMASTER"
            objcitymaster.Show()
            objcitymaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try

    End Sub

    Private Sub AddNewAccountsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ACCADD.Click
        Try
            Dim objAccountMaster As New AccountsMaster
            objAccountMaster.MdiParent = Me
            objAccountMaster.frmstring = "ACCOUNTS"
            objAccountMaster.Show()
            objAccountMaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub EditExistingGroupToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GROUPEDIT.Click
        Try
            Dim objGroupDetails As New GroupDetails
            objGroupDetails.MdiParent = Me
            objGroupDetails.Show()
            objGroupDetails.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub EditExistingAccoutsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ACCEDIT.Click
        Try
            Dim objAccountDetails As New AccountsDetails
            objAccountDetails.MdiParent = Me
            objAccountDetails.frmstring = "ACCOUNTS"
            objAccountDetails.Show()
            objAccountDetails.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub EditExistingAreaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AREAEDIT.Click
        Try
            Dim objCityDetails As New CityDetails
            objCityDetails.MdiParent = Me
            objCityDetails.frmstring = "AREAMASTER"
            objCityDetails.Show()
            objCityDetails.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub EditExistingCityToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CITYEDIT.Click
        Try
            Dim objCityDetails As New CityDetails
            objCityDetails.MdiParent = Me
            objCityDetails.frmstring = "CITYMASTER"
            objCityDetails.Show()
            objCityDetails.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub EditExistingStateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles STATEEDIT.Click
        Try
            Dim objCityDetails As New CityDetails
            objCityDetails.MdiParent = Me
            objCityDetails.frmstring = "STATEMASTER"
            objCityDetails.Show()
            objCityDetails.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub EditExistingCountryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles COUNTRYEDIT.Click
        Try
            Dim objCityDetails As New CityDetails
            objCityDetails.MdiParent = Me
            objCityDetails.frmstring = "COUNTRYMASTER"
            objCityDetails.Show()
            objCityDetails.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub addUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles USERADD.Click
        Try
            Dim objuser As New UserMaster
            objuser.MdiParent = Me
            objuser.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub editUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles USEREDIT.Click
        Try
            Dim objuser As New UserDetails
            objuser.MdiParent = Me
            objuser.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub opencmp(ByVal CMP As String)
        Try

            Dim objcmn As New ClsCommon
            Dim DT As DataTable

            DT = objcmn.search("CMPMASTER.CMP_NAME, YEARMASTER.YEAR_DBNAME, CMPMASTER.CMP_ID, YEARMASTER.YEAR_STARTDATE, YEARMASTER.YEAR_ENDDATE, YEARMASTER.YEAR_ID", "", " CMPMASTER INNER JOIN YEARMASTER ON YEARMASTER.YEAR_CMPID = CMPMASTER.CMP_ID", " AND CMPMASTER.CMP_NAME = '" & CMP & "'")
            CmpName = DT.Rows(0).Item(0).ToString
            DBName = DT.Rows(0).Item(1).ToString
            CmpId = DT.Rows(0).Item(2).ToString
            AccFrom = DT.Rows(0).Item(3)
            AccTo = DT.Rows(0).Item(4)
            YearId = DT.Rows(0).Item(5).ToString
            Cmppassword.cmdback.Visible = False
            Cmppassword.lblretype.Visible = False
            Cmppassword.txtretypepassword.Visible = False
            Cmppassword.cmdnext.Text = "&Ok"
            Cmppassword.ShowDialog()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub AddNewRegisterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddNewRegisterToolStripMenuItem.Click
        Try
            Dim objregistermaster As New RegisterMaster
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "PURCHASE"
            objregistermaster.Show()
            objregistermaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ToolStripMenuItem14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem14.Click
        Try
            Dim objregistermaster As New RegisterMaster
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "SALE"
            objregistermaster.Show()
            objregistermaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ToolStripMenuItem17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem17.Click
        Try
            Dim objregistermaster As New RegisterMaster
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "JOURNAL"
            objregistermaster.Show()
            objregistermaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ToolStripMenuItem19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem19.Click
        Try
            Dim objregistermaster As New RegisterMaster
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "CONTRA"
            objregistermaster.Show()
            objregistermaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ToolStripMenuItem21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem21.Click
        Try
            Dim objregistermaster As New RegisterMaster
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "PAYMENT"
            objregistermaster.Show()
            objregistermaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ToolStripMenuItem23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem23.Click
        Try
            Dim objregistermaster As New RegisterMaster
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "RECEIPT"
            objregistermaster.Show()
            objregistermaster.BringToFront()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TAXADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TAXADD.Click
        Try
            Dim OBJTAX As New Taxmaster
            OBJTAX.MdiParent = Me
            OBJTAX.Show()
            OBJTAX.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TAXEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TAXEDIT.Click
        Try
            Dim OBJTAXDETAILS As New TaxDetails
            OBJTAXDETAILS.MdiParent = Me
            OBJTAXDETAILS.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub AddNewExpenseRegisterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddNewExpenseRegisterToolStripMenuItem.Click
        Try
            Dim objregistermaster As New RegisterMaster
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "EXPENSE"
            objregistermaster.Show()
            objregistermaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub AddNewRegisterToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddNewRegisterToolStripMenuItem1.Click
        Try
            Dim objregistermaster As New RegisterMaster
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "CREDITNOTE"
            objregistermaster.Show()
            objregistermaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub AddNewRegisterToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddNewRegisterToolStripMenuItem2.Click
        Try
            Dim objregistermaster As New RegisterMaster
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "DEBITNOTE"
            objregistermaster.Show()
            objregistermaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub QUALITYADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QUALITYADD.Click
        Try
            Dim objCategory As New QualityMaster
            objCategory.MdiParent = Me
            objCategory.Show()
            objCategory.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ChangeCompany_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeCompany.Click
        Try
            'close all child forms
            Dim frm As Form
            For Each frm In MdiChildren
                frm.Close()
            Next

            Me.Dispose()
            Cmpdetails.Show()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ChangeUserToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeUserToolStripMenuItem.Click
        Try
            'close all child forms
            Dim frm As Form
            For Each frm In MdiChildren
                frm.Close()
            Next

            Me.Dispose()
            Login.Show()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMPEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMPEDIT.Click
        Try
            Cmpmaster.edit = True
            Cmpmaster.TEMPCMPNAME = CmpName
            Cmpmaster.ShowDialog()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMPADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMPADD.Click
        Try
            Dim obj As New Cmpmaster
            obj.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub GRNADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GRNADD.Click
        Try
            Dim OBJGRN As New GRN
            OBJGRN.MdiParent = Me
            OBJGRN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRNEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GRNEDIT.Click
        Try
            Dim OBJGRN As New GRNDetails
            OBJGRN.MdiParent = Me
            OBJGRN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRN_TOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GRN_TOOL.Click
        Try
            Dim OBJGRN As New GRN
            OBJGRN.MdiParent = Me
            OBJGRN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub POADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles POADD.Click
        Try
            Dim ObjPurchaseOrder As New PurchaseOrder
            ObjPurchaseOrder.MdiParent = Me
            ObjPurchaseOrder.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub POEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles POEDIT.Click
        Try
            Dim ObjPurchaseOrder As New PurchaseOrderDetails
            ObjPurchaseOrder.MdiParent = Me
            ObjPurchaseOrder.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PURRETADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PURRETADD.Click
        Try
            'Dim OBJPUR As New SelectPurRetScreen
            'OBJPUR.FRMSTRING = "ADDPURRETURN"
            'OBJPUR.ShowDialog()
            Dim OBJPUR As New PurchaseReturn
            OBJPUR.MdiParent = Me
            OBJPUR.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PURRETEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PURRETEDIT.Click
        Try
            'Dim OBJPUR As New SelectPurRetScreen
            'OBJPUR.FRMSTRING = "EDITPURRETURN"
            'OBJPUR.ShowDialog()
            Dim OBJPUR As New PurchaseReturnDetails
            OBJPUR.MdiParent = Me
            OBJPUR.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PURINVADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PURINVADD.Click
        Try
            'Dim OBJPUR As New SelectPurScreen
            'OBJPUR.FRMSTRING = "ADDPUR"
            'OBJPUR.ShowDialog()
            Dim OBJPUR As New PurchaseMaster
            OBJPUR.MdiParent = Me
            OBJPUR.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PURINVEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PURINVEDIT.Click
        Try
            'Dim OBJPUR As New SelectPurScreen
            'OBJPUR.FRMSTRING = "EDITPUR"
            'OBJPUR.ShowDialog()
            Dim OBJPUR As New PurchaseInvoiceDetails
            OBJPUR.MdiParent = Me
            OBJPUR.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BackupCompany_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BackupCompany.Click
        Try
            backup()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub backup()
        Try
            'TAKE BACKUP
            Dim TEMPMSG As Integer = MsgBox("Create Backup?", MsgBoxStyle.YesNo)
            If TEMPMSG = vbYes Then

                'CHECKING FOR BACKUP FOLDER
                If FileIO.FileSystem.DirectoryExists("C:\PROCESSBACKUP") = False Then FileIO.FileSystem.CreateDirectory("C:\PROCESSBACKUP")

                'IF SAME DATE'S BACKUP EXIST THEN DELETE IT THEN RECREATE IT
                If FileIO.FileSystem.FileExists("C:\PROCESSBACKUP\BACKUP " & Now.Day & "-" & Now.Month & "-" & Now.Year & ".bak") Then FileIO.FileSystem.DeleteFile("D:\PROCESSBACKUP\BACKUP " & Now.Day & "-" & Now.Month & "-" & Now.Year & ".bak")

                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.Execute_Any_String(" backup database PROCESS to disk='C:\PROCESSBACKUP\BACKUP " & Now.Day & "-" & Now.Month & "-" & Now.Year & ".bak'", "", "")
                MsgBox("Backup Completed")
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PURCHASE_TOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PURCHASE_TOOL.Click
        Try
            'Dim OBJPUR As New SelectPurScreen
            'OBJPUR.FRMSTRING = "ADDPUR"
            'OBJPUR.ShowDialog()
            Dim OBJPUR As New PurchaseMaster
            OBJPUR.MdiParent = Me
            OBJPUR.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MERGELEDGER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MERGELEDGER.Click
        Try
            Dim OBJMERGE As New MergeLedger
            OBJMERGE.MdiParent = Me
            OBJMERGE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MergeParameterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MERGEPARAMETER_MENU.Click
        Try
            Dim OBJmerger As New MergeParameter
            OBJmerger.MdiParent = Me
            OBJmerger.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub NARRATIONADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NARRATIONADD.Click
        Try
            Dim objCategory As New CategoryMaster
            objCategory.frmString = "NARRATION"
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub PARTYBANKADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PARTYBANKADD.Click
        Try
            Dim objCategory As New CategoryMaster
            objCategory.frmString = "PARTYBANK"
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub NARRATIONEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NARRATIONEDIT.Click
        Try
            Dim objCategory As New CategoryDetails
            objCategory.frmstring = "NARRATION"
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub PARTYBANKEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PARTYBANKEDIT.Click
        Try
            Dim objCategory As New CategoryDetails
            objCategory.frmstring = "PARTYBANK"
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub QUALITYEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QUALITYEDIT.Click
        Try
            Dim objCategory As New QualityDetails
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub PO_TOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PO_TOOL.Click
        Try
            Dim ObjPurchaseOrder As New PurchaseOrder
            ObjPurchaseOrder.MdiParent = Me
            ObjPurchaseOrder.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewDistrictToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DISTRICTADD.Click
        Try
            Dim objCategory As New CategoryMaster
            objCategory.frmString = "DISTRICT"
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub AddNewViaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VIAADD.Click
        Try
            Dim objCategory As New CategoryMaster
            objCategory.frmString = "VIA"
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub EditExistingDistrictToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DISTRICTEDIT.Click
        Try
            Dim objCategory As New CategoryDetails
            objCategory.frmstring = "DISTRICT"
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub EditExistingViaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VIAEDIT.Click
        Try
            Dim objCategory As New CategoryDetails
            objCategory.frmstring = "VIA"
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub AddNewTypeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TYPEADD.Click
        Try
            Dim objCategory As New TypeMaster
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub EditExisitngTypeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TYPEEDIT.Click
        Try
            Dim objCategory As New TypeDetails
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub DefaultRegisterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DefaultRegisterToolStripMenuItem.Click
        Try
            Dim objCategory As New DefaultRegister
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub InHouseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPSTOCK_YARN.Click
        Try
            Dim OBJOPSTOCKYARN As New OpeningStockYarnWareHouse
            OBJOPSTOCKYARN.MdiParent = Me
            OBJOPSTOCKYARN.Show()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub JobberToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPSTOCK_BEAM.Click
        Try
            Dim OBJOPSTOCKBEAM As New OpeningStockBeam
            OBJOPSTOCKBEAM.MdiParent = Me
            OBJOPSTOCKBEAM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewOrderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DOADD.Click
        Try
            Dim objDO As New DeliveryOrder
            objDO.MdiParent = Me
            objDO.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub EditExistingOrderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DOEDIT.Click
        Try
            Dim objDO As New DeliveryOrderDetails
            objDO.MdiParent = Me
            objDO.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub MaterialDespatch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MATDESADD.Click
        Try
            Dim OBJMATDES As New MaterialDespatch
            OBJMATDES.MdiParent = Me
            OBJMATDES.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MATDESEDIT.Click
        Try
            Dim OBJMATDES As New MaterialDespatchDetails
            OBJMATDES.MdiParent = Me
            OBJMATDES.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub DO_TOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DO_TOOL.Click
        Try
            Dim OBJDO As New DeliveryOrder
            OBJDO.MdiParent = Me
            OBJDO.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub GODOWNADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GODOWNADD.Click
        Try
            Dim OBJGODOWN As New GodownMaster
            OBJGODOWN.MdiParent = Me
            OBJGODOWN.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub GODOWNEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GODOWNEDIT.Click
        Try
            Dim OBJGODOWN As New GodownDetails
            OBJGODOWN.MdiParent = Me
            OBJGODOWN.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub PurchaseGRNToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurchaseGRNToolStripMenuItem.Click
        Try
            Dim OBJGRNFILTR As New GRNFilter
            OBJGRNFILTR.MdiParent = Me
            OBJGRNFILTR.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub PurchaseOrderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurchaseOrderToolStripMenuItem.Click
        Try
            Dim OBJPURINV As New POFilter
            OBJPURINV.MdiParent = Me
            OBJPURINV.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddBeamToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BEAMADD.Click
        Try
            Dim OBJBEAMMASTER As New BeamMaster
            OBJBEAMMASTER.MdiParent = Me
            OBJBEAMMASTER.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingBeamToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BEAMEDIT.Click
        Try
            Dim OBJBEAM As New BeamDetails
            OBJBEAM.MdiParent = Me
            OBJBEAM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewGreyQualityToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GREYQUALITYADD.Click
        Try
            Dim OBJGREY As New GreyQualityMaster
            OBJGREY.MdiParent = Me
            OBJGREY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingGreyQualityToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GREYQUALITYEDIT.Click
        Try
            Dim OBJGREYQUALITY As New GreyQualityDetails
            OBJGREYQUALITY.MdiParent = Me
            OBJGREYQUALITY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNISSUESIZERADD.Click
        Try
            Dim OBJISSUE As New YarnIssue
            OBJISSUE.FRMSTRING = "ISSUETOSIZER"
            OBJISSUE.MdiParent = Me
            OBJISSUE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewBeamRecFromSizerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BEAMRECADD.Click
        Try
            Dim OBJBEAMREC As New BeamRecd
            OBJBEAMREC.MdiParent = Me
            OBJBEAMREC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingBeamRecFromSizerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BEAMRECEDIT.Click
        Try
            Dim OBJBEAMREC As New BeamRecdDetails
            OBJBEAMREC.MdiParent = Me
            OBJBEAMREC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingIssueToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNISSUESIZEREDIT.Click
        Try
            Dim OBJISSUEDETAILS As New YarnIssueDetails
            OBJISSUEDETAILS.MdiParent = Me
            OBJISSUEDETAILS.FRMSTRING = "ISSUETOSIZER"
            OBJISSUEDETAILS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNISSUEWEAVERADD.Click
        Try
            Dim OBJISSUE As New YarnIssue
            OBJISSUE.FRMSTRING = "ISSUETOWEAVER"
            OBJISSUE.MdiParent = Me
            OBJISSUE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNISSUEWEAVEREDIT.Click
        Try
            Dim OBJISSUE As New YarnIssueDetails
            OBJISSUE.FRMSTRING = "ISSUETOWEAVER"
            OBJISSUE.MdiParent = Me
            OBJISSUE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BEAMISSUEADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BEAMISSUEADD.Click
        Try
            Dim OBJBEAM As New BeamIssue
            OBJBEAM.MdiParent = Me
            OBJBEAM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BEAMISSUEEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BEAMISSUEEDIT.Click
        Try
            Dim OBJBEAM As New BeamIssueDetails
            OBJBEAM.MdiParent = Me
            OBJBEAM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GREYRECWEAVERADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GREYRECWEAVERADD.Click
        Try
            If MULTIYARN = False Then
                Dim OBJGREY As New GreyRecdFromWeaver
                OBJGREY.MdiParent = Me
                OBJGREY.Show()
            Else
                Dim OBJGREY As New GreyRecdWeaverWeftChange
                OBJGREY.MdiParent = Me
                OBJGREY.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GREYRECWEAVEREDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GREYRECWEAVEREDIT.Click
        Try
            If MULTIYARN = False Then
                Dim OBJGREY As New GreyRecdFromWeaverDetails
                OBJGREY.MdiParent = Me
                OBJGREY.Show()
            Else
                Dim OBJGREY As New GreyRecdWeaverWeftChangeDetails
                OBJGREY.MdiParent = Me
                OBJGREY.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewEntryToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHALLANADD.Click
        Try
            Dim OBJCHALLAN As New Challan
            OBJCHALLAN.MdiParent = Me
            OBJCHALLAN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripMenuItem2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHALLANEDIT.Click
        Try
            Dim OBJCHALLAN As New ChallanDetails
            OBJCHALLAN.MdiParent = Me
            OBJCHALLAN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpeningStockBeamWithWeaverToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPSTOCK_BEAMWEAVER.Click
        Try
            Dim OBJBEAMWEAVER As New OpeningStockBeamwithWeaverLoomWise
            OBJBEAMWEAVER.MdiParent = Me
            OBJBEAMWEAVER.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpeningStockGreyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPSTOCK_GREY.Click
        Try
            Dim OBJOPGREY As New OpeningStockGrey
            OBJOPGREY.MdiParent = Me
            OBJOPGREY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OPSTOCK_GREYPROCESSOR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPSTOCK_GREYPROCESSOR.Click
        Try
            Dim OBJOPGREYPROCESSOR As New OpeningStockGreywithProcessor
            OBJOPGREYPROCESSOR.MdiParent = Me
            OBJOPGREYPROCESSOR.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SALE_TOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SALE_TOOL.Click
        Try
            Dim ObjSaleInvoice As New InvoiceMaster
            ObjSaleInvoice.MdiParent = Me
            ObjSaleInvoice.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SO_TOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SO_TOOL.Click
        Try
            Dim ObjSaleOrder As New SaleOrder
            ObjSaleOrder.MdiParent = Me
            ObjSaleOrder.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PAYMENT_TOOL.Click
        Try
            Dim OBJPAY As New PaymentMaster
            OBJPAY.MdiParent = Me
            OBJPAY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RECEIPT_TOOL.Click
        Try
            Dim OBJREC As New Receipt
            OBJREC.MdiParent = Me
            OBJREC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CONTRA_TOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CONTRA_TOOL.Click
        Try
            Dim OBJCONRA As New CONTRA
            OBJCONRA.MdiParent = Me
            OBJCONRA.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub JV_TOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JV_TOOL.Click
        Try
            Dim OBJJV As New journal
            OBJJV.MdiParent = Me
            OBJJV.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SOADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SOADD.Click
        Try
            Dim ObjSaleOrder As New SaleOrder
            ObjSaleOrder.MdiParent = Me
            ObjSaleOrder.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SOEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SOEDIT.Click
        Try
            Dim ObjSaleOrder As New SaleOrderDetails
            ObjSaleOrder.MdiParent = Me
            ObjSaleOrder.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SALEADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SALEADD.Click
        Try
            Dim ObjSaleInvoice As New InvoiceMaster
            ObjSaleInvoice.MdiParent = Me
            ObjSaleInvoice.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SALEEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SALEEDIT.Click
        Try
            Dim ObjSaleInvoice As New InvoiceDetails
            ObjSaleInvoice.MdiParent = Me
            ObjSaleInvoice.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BLOCKUSER_MENU_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BLOCKUSER_MENU.Click
        Try
            'Dim OBJUSER As New BlockUser
            'OBJUSER.MdiParent = Me
            'OBJUSER.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YEARTRANSFER_MENU_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YEARTRANSFER_MENU.Click
        Try
            Dim OBJYEAR As New YearTransfer
            OBJYEAR.MdiParent = Me
            OBJYEAR.FRMSTRING = "YEARTRANSFER"
            OBJYEAR.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHALLAN_TOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHALLAN_TOOL.Click
        Try
            Dim OBJGDN As New Challan
            OBJGDN.MdiParent = Me
            OBJGDN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OPENINGBILL_MASTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPENINGBILL_MASTER.Click
        Try
            Dim OBJOP As New OpeningBills
            OBJOP.FRMSTRING = "OPENINGBILLS"
            OBJOP.MdiParent = Me
            OBJOP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PAYADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PAYADD.Click
        Try
            Dim OBJPAY As New PaymentMaster
            OBJPAY.MdiParent = Me
            OBJPAY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PAYEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PAYEDIT.Click
        Try
            Dim OBJPAY As New PaymentDetails
            OBJPAY.MdiParent = Me
            OBJPAY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RECADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RECADD.Click
        Try
            Dim OBJREC As New Receipt
            OBJREC.MdiParent = Me
            OBJREC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RECEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RECEDIT.Click
        Try
            Dim OBJREC As New ReceiptDetails
            OBJREC.MdiParent = Me
            OBJREC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CONTRAADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CONTRAADD.Click
        Try
            Dim OBJCON As New CONTRA
            OBJCON.MdiParent = Me
            OBJCON.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CONTRAEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CONTRAEDIT.Click
        Try
            Dim OBJCON As New ContraDetails
            OBJCON.MdiParent = Me
            OBJCON.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub JVEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JVADD.Click
        Try
            Dim OBJJV As New journal
            OBJJV.MdiParent = Me
            OBJJV.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingJournalEntryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JVEDIT.Click
        Try
            Dim OBJJV As New JournalDetails
            OBJJV.MdiParent = Me
            OBJJV.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CREDITADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CREDITADD.Click
        Try
            Dim OBJCN As New CREDITNOTE
            OBJCN.MdiParent = Me
            OBJCN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CREDITEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CREDITEDIT.Click
        Try
            Dim OBJCN As New CreditNoteDetails
            OBJCN.MdiParent = Me
            OBJCN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DEBITADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DEBITADD.Click
        Try
            Dim OBJDN As New DebitNote
            OBJDN.MdiParent = Me
            OBJDN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DEBITEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DEBITEDIT.Click
        Try
            Dim OBJDN As New DebitNoteDetails
            OBJDN.MdiParent = Me
            OBJDN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub VOUCHERADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VOUCHERADD.Click
        Try
            Dim OBJEXP As New ExpenseVoucher
            OBJEXP.MdiParent = Me
            OBJEXP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub VOUCHEREDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VOUCHEREDIT.Click
        Try
            Dim OBJEXP As New ExpenseVoucherDetails
            OBJEXP.MdiParent = Me
            OBJEXP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaleInvoiceFilterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaleInvoiceFilterToolStripMenuItem.Click
        Try
            Dim OBJSALE As New SaleInvoiceFilter
            OBJSALE.MdiParent = Me
            OBJSALE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RegisterWisePurchaseSummaryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RegisterWisePurchaseSummaryToolStripMenuItem.Click
        Try
            Dim OBJFILTER As New filter
            OBJFILTER.frmstring = "REGISTERPURCHASESUMMARY"
            OBJFILTER.MdiParent = Me
            OBJFILTER.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RegiserWiseSaleSummaryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RegiserWiseSaleSummaryToolStripMenuItem.Click
        Try
            Dim OBJFILTER As New filter
            OBJFILTER.frmstring = "REGISTERSALESUMMARY"
            OBJFILTER.MdiParent = Me
            OBJFILTER.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DeleteLogsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteLogsToolStripMenuItem.Click
        Try
            Dim OBJDELETE As New DeleteDetails
            OBJDELETE.MdiParent = Me
            OBJDELETE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UpdateLogsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdateLogsToolStripMenuItem.Click
        Try
            Dim OBJLOGS As New UpdateDetails
            OBJLOGS.MdiParent = Me
            OBJLOGS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PurchaseRegisterToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurchaseRegisterToolStripMenuItem2.Click
        Try
            Dim objpurreg As New PurchaseRegister
            objpurreg.MdiParent = Me
            objpurreg.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaleRegisterToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaleRegisterToolStripMenuItem2.Click
        Try
            Dim objsalereg As New SaleRegister
            objsalereg.MdiParent = Me
            objsalereg.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub JournalRegisterToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JournalRegisterToolStripMenuItem2.Click
        Try
            Dim OBJJVREG As New JournalRegister
            OBJJVREG.MdiParent = Me
            OBJJVREG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ContraRegisterToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContraRegisterToolStripMenuItem2.Click
        Try
            Dim OBJCONTRAREG As New ContraRegister
            OBJCONTRAREG.MdiParent = Me
            OBJCONTRAREG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DebitNoteRegisterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DebitNoteRegisterToolStripMenuItem.Click
        Try
            Dim OBJDNREG As New DNRegister
            OBJDNREG.MdiParent = Me
            OBJDNREG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CreditNoteRegisterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreditNoteRegisterToolStripMenuItem.Click
        Try
            Dim OBJCNREG As New CNRegister
            OBJCNREG.MdiParent = Me
            OBJCNREG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BankBookToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BankBookToolStripMenuItem.Click
        Try
            Dim OBJBANKREG As New BankRegister
            OBJBANKREG.MdiParent = Me
            OBJBANKREG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CashBookToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CashBookToolStripMenuItem.Click
        Try
            Dim OBJCASHREG As New cashregister1
            OBJCASHREG.MdiParent = Me
            OBJCASHREG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AdvancesSettlementToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdvancesSettlementToolStripMenuItem.Click
        Try
            Dim OBJADV As New Adv_Receivable_settlement
            OBJADV.flag_adv_settlement = True
            OBJADV.MdiParent = Me
            OBJADV.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ReceivableSettlementToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReceivableSettlementToolStripMenuItem.Click
        Try
            Dim OBJADV As New Adv_Receivable_settlement
            OBJADV.flag_Rec_settlement = True
            OBJADV.MdiParent = Me
            OBJADV.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ReceivableOutstandingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReceivableOutstandingToolStripMenuItem.Click
        Try
            Dim OBJOUT As New OutstandingFilter
            OBJOUT.FRMSTRING = "RECOUTSTANDING"
            OBJOUT.MdiParent = Me
            OBJOUT.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PayableOutstandingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PayableOutstandingToolStripMenuItem.Click
        Try
            Dim OBJOUT As New OutstandingFilter
            OBJOUT.FRMSTRING = "PAYOUTSTANDING"
            OBJOUT.MdiParent = Me
            OBJOUT.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DayBookToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DayBookToolStripMenuItem.Click
        Try
            Dim OBJDAYBOOK As New DayBook
            OBJDAYBOOK.MdiParent = Me
            OBJDAYBOOK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LedgerOnScreenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LedgerOnScreenToolStripMenuItem.Click
        Try
            Dim objledger As New LedgerSummary
            objledger.MdiParent = Me
            objledger.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LedgerBookToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LedgerBookToolStripMenuItem1.Click
        Try
            Dim objledgerbook As New RegisterDetails
            objledgerbook.MdiParent = Me
            objledgerbook.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LedgerBillWiseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LedgerBillWiseToolStripMenuItem.Click
        Try
            Dim OBJBILL As New LedgerBillwise
            OBJBILL.MdiParent = Me
            OBJBILL.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OutstandingFilterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OutstandingFilterToolStripMenuItem.Click
        Try
            Dim OBJOP As New OutstandingFilter
            OBJOP.MdiParent = Me
            OBJOP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OutstandingGridReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OutstandingGridReportToolStripMenuItem.Click
        Try
            Dim OBJOUT As New OutstandingReport
            OBJOUT.MdiParent = Me
            OBJOUT.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LedgersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LedgersToolStripMenuItem.Click
        Try
            Dim OBJLEDGER As New LedgerFilter
            OBJLEDGER.MdiParent = Me
            OBJLEDGER.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PurchaseTaxRegisterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurchaseTaxRegisterToolStripMenuItem.Click
        Try
            Dim OBJTAX As New TaxFilter
            OBJTAX.MdiParent = Me
            OBJTAX.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GroupSummaryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupSummaryToolStripMenuItem.Click
        Try
            Dim OBJGROUP As New GroupRegister
            OBJGROUP.MdiParent = Me
            OBJGROUP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TrialBalanceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrialBalanceToolStripMenuItem.Click
        Try
            Dim OBJTB As New TB
            OBJTB.MdiParent = Me
            OBJTB.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ProfitLossToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProfitLossToolStripMenuItem.Click
        Try
            Dim objpl As New PL
            objpl.MdiParent = Me
            objpl.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BalanceSheetToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BalanceSheetToolStripMenuItem.Click
        Try
            If ClientName = "JASHOK" Then
                'Dim OBJBALANCESHEET As New BSDrCr
                Dim OBJBALANCESHEET As New BS
                OBJBALANCESHEET.MdiParent = Me
                OBJBALANCESHEET.Show()
            Else
                Dim OBJBALANCESHEET As New BS
                OBJBALANCESHEET.MdiParent = Me
                OBJBALANCESHEET.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub FORMSUMMARY_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FORMSUMMARY.Click
        Try
            Dim OBJCFORM As New CFormSummary
            OBJCFORM.MdiParent = Me
            OBJCFORM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub FORMDETAILS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FORMDETAILS.Click
        Try
            Dim OBJCFORM As New CFormEntry
            OBJCFORM.MdiParent = Me
            OBJCFORM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub FORMREPORTS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FORMREPORTS.Click
        Try
            Dim OBJCFORM As New FormFilter
            OBJCFORM.MdiParent = Me
            OBJCFORM.frmstring = "CFORMFILTER"
            OBJCFORM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CFormApplicationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CFORMAPPLICATION.Click
        Try
            Dim OBJCFORM As New FormFilter
            OBJCFORM.frmstring = "CFORMAPPLICATION"
            OBJCFORM.MdiParent = Me
            OBJCFORM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TDSToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TDSToolStripMenuItem.Click
        Try
            Dim OBJTDS As New TDS
            OBJTDS.MdiParent = Me
            OBJTDS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TDSCHALLAN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TDSCHALLAN.Click
        Try
            Dim OBJTDS As New TDSChallan
            OBJTDS.MdiParent = Me
            OBJTDS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub InterestToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InterestToolStripMenuItem.Click
        Try
            Dim OBJINTCALC As New InterestCalc
            OBJINTCALC.MdiParent = Me
            OBJINTCALC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub IntrestCalculatorSummaryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IntrestCalculatorSummaryToolStripMenuItem.Click
        Try
            Dim OBJINTCALC As New InterestCalc_Summary
            OBJINTCALC.MdiParent = Me
            OBJINTCALC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpeningStockYarnWithSizerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPSTOCK_SIZERYARN.Click
        Try
            Dim OBJCFORM As New OpeningStockYarnwithSizer
            OBJCFORM.FRMSTRING = "YARNSIZER"
            OBJCFORM.MdiParent = Me
            OBJCFORM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpeningStockYarnWithWeaverToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPSTOCK_WEAVERYARN.Click
        Try
            Dim OBJCFORM As New OpeningStockYarnwithSizer
            OBJCFORM.FRMSTRING = "YARNWEAVER"
            OBJCFORM.MdiParent = Me
            OBJCFORM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ROLLSRECDADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ROLLSRECDADD.Click
        Try
            Dim OBJROLLRECD As New RollsRecd
            OBJROLLRECD.MdiParent = Me
            OBJROLLRECD.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ROLLSRECDEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ROLLSRECDEDIT.Click
        Try
            Dim OBJROLLRECD As New RollsRecdDetails
            OBJROLLRECD.MdiParent = Me
            OBJROLLRECD.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ROLLSISSUEADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ROLLSISSUEADD.Click
        Try
            Dim OBJROLLISSUE As New RollsIssue
            OBJROLLISSUE.MdiParent = Me
            OBJROLLISSUE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ROLLSISSUEEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ROLLSISSUEEDIT.Click
        Try
            Dim OBJROLLISSUE As New RollIssueDetails
            OBJROLLISSUE.MdiParent = Me
            OBJROLLISSUE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNISSUEWARPERADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNISSUEWARPERADD.Click
        Try
            Dim OBJISSUE As New YarnIssue
            OBJISSUE.FRMSTRING = "ISSUETOWARPER"
            OBJISSUE.MdiParent = Me
            OBJISSUE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNISSUEWARPEREDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNISSUEWARPEREDIT.Click
        Try
            Dim OBJISSUE As New YarnIssueDetails
            OBJISSUE.FRMSTRING = "ISSUETOWARPER"
            OBJISSUE.MdiParent = Me
            OBJISSUE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PROGRAMADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PROGRAMADD.Click
        Try
            Dim OBJPROGRAM As New ProgramMaster
            OBJPROGRAM.MdiParent = Me
            OBJPROGRAM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PROGRAMEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PROGRAMEDIT.Click
        Try
            Dim OBJPROGRAM As New ProgramDetails
            OBJPROGRAM.MdiParent = Me
            OBJPROGRAM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OPSTOCK_WARPERYARN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPSTOCK_WARPERYARN.Click
        Try
            Dim OBJCFORM As New OpeningStockYarnwithSizer
            OBJCFORM.FRMSTRING = "YARNWARPER"
            OBJCFORM.MdiParent = Me
            OBJCFORM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OPSTOCK_YARNGODOWN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPSTOCK_YARNGODOWN.Click
        Try
            Dim OBJOPSTOCKYARN As New OpeningStockYarnGodown
            OBJOPSTOCKYARN.MdiParent = Me
            OBJOPSTOCKYARN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewEntryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNRETURNWARPERADD.Click
        Try
            Dim OBJRETURNWARPER As New YarnReturn
            OBJRETURNWARPER.FRMSTRING = "RETURNFROMWARPER"
            OBJRETURNWARPER.MdiParent = Me
            OBJRETURNWARPER.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewEntryToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNRETURNSIZERADD.Click
        Try
            Dim OBJRETURNSIZER As New YarnReturn
            OBJRETURNSIZER.FRMSTRING = "RETURNFROMSIZER"
            OBJRETURNSIZER.MdiParent = Me
            OBJRETURNSIZER.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewEnterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNRETURNWEAVERADD.Click
        Try
            Dim OBJRETURNWEAVER As New YarnReturn
            OBJRETURNWEAVER.FRMSTRING = "RETURNFROMWEAVER"
            OBJRETURNWEAVER.MdiParent = Me
            OBJRETURNWEAVER.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingReturnToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNRETURNWARPEREDIT.Click
        Try
            Dim OBJISSUE As New YarnReturnDetails
            OBJISSUE.FRMSTRING = "RETURNFROMWARPER"
            OBJISSUE.MdiParent = Me
            OBJISSUE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingReturnToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNRETURNSIZEREDIT.Click
        Try
            Dim OBJISSUE As New YarnReturnDetails
            OBJISSUE.FRMSTRING = "RETURNFROMSIZER"
            OBJISSUE.MdiParent = Me
            OBJISSUE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingReturnToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNRETURNWEAVEREDIT.Click
        Try
            Dim OBJISSUE As New YarnReturnDetails
            OBJISSUE.FRMSTRING = "RETURNFROMWEAVER"
            OBJISSUE.MdiParent = Me
            OBJISSUE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewBeamReturnToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BEAMRETURNADD.Click
        Try
            Dim OBJBEAMRETURN As New BeamReturn
            OBJBEAMRETURN.MdiParent = Me
            OBJBEAMRETURN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingBeamReturnToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BEAMRETURNEDIT.Click
        Try
            Dim OBJBEAMRET As New BeamReturnDetails
            OBJBEAMRET.MdiParent = Me
            OBJBEAMRET.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpeningStockRollsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPSTOCK_ROLLS.Click
        Try
            Dim OBJOPROLLS As New OpeningStockRolls
            OBJOPROLLS.MdiParent = Me
            OBJOPROLLS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewRollsReturnToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ROLLSRETURNADD.Click
        Try
            Dim OBJROLLSRET As New RollsReturn
            OBJROLLSRET.MdiParent = Me
            OBJROLLSRET.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingRollsReturnToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ROLLSRETURNEDIT.Click
        Try
            Dim OBJROLLSRETDET As New RollsReturnDetails
            OBJROLLSRETDET.MdiParent = Me
            OBJROLLSRETDET.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpeningStockRollsWithSizerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPSTOCK_ROLLSSIZER.Click
        Try
            Dim OBJOPROLLSSIZER As New OpeningStockRollsSizer
            OBJOPROLLSSIZER.MdiParent = Me
            OBJOPROLLSSIZER.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub FINSISHEDToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPSTOCK_FINISHEDBALE.Click
        Try
            Dim OBJFINGREY As New FinishedBaleStockGodown
            OBJFINGREY.MdiParent = Me
            OBJFINGREY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub JobOutNewEntryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNISSUEWINDINGADD.Click
        Try
            Dim OBJJOBOUT As New JobOut
            OBJJOBOUT.MdiParent = Me
            OBJJOBOUT.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewJobInToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNRECWINDINGADD.Click
        Try
            Dim OBJJOBIN As New JobIn
            OBJJOBIN.MdiParent = Me
            OBJJOBIN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingJobOutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNISSUEWINDINGEDIT.Click
        Try
            Dim OBJJOBOUT As New JobOutDetails
            OBJJOBOUT.MdiParent = Me
            OBJJOBOUT.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingJobInToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNRECWINDINEDIT.Click
        Try
            Dim OBJJOBIN As New JobInDetails
            OBJJOBIN.MdiParent = Me
            OBJJOBIN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewEntryToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DELIVERYATADD.Click
        Try
            Dim OBJDEL As New DeliveryAtMaster
            OBJDEL.MdiParent = Me
            OBJDEL.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub ToolStripMenuItem2_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DELIVERYATEDIT.Click
        Try
            Dim OBJDEL As New DeliveryAtDetails
            OBJDEL.MdiParent = Me
            OBJDEL.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewEntryToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHALLANFINISHEDADD.Click
        Try
            Dim OBJCHALLAN As New Challan_Finished
            OBJCHALLAN.MdiParent = Me
            OBJCHALLAN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHALLANFINISHEDEDIT.Click
        Try
            Dim OBJCHALLAN As New Challan_Finished_Details
            OBJCHALLAN.MdiParent = Me
            OBJCHALLAN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LOOM_MASTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LOOM_MASTER.Click
        Try
            Dim OBJLOOM As New LoomMaster
            OBJLOOM.MdiParent = Me
            OBJLOOM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LIFTING_MASTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LIFTING_MASTER.Click
        Try
            Dim OBJLIFT As New LiftingDetails
            OBJLIFT.MdiParent = Me
            OBJLIFT.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GREYCHECKINGADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GREYCHECKINGADD.Click
        Try
            Dim OBJGREYCHK As New GreyChecking
            OBJGREYCHK.MdiParent = Me
            OBJGREYCHK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GREYCHECKINGEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GREYCHECKINGEDIT.Click
        Try
            Dim OBJGREYCHK As New GreyCheckingDetails
            OBJGREYCHK.MdiParent = Me
            OBJGREYCHK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub AddNewGoodsReturnToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GREYRETURNADD.Click
        Try
            Dim OBJGR As New GoodsReturn
            OBJGR.MdiParent = Me
            OBJGR.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingGoodsReturnToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GREYRETURNEDIT.Click
        Try
            Dim OBJGRDTLS As New GoodsReturnDetails
            OBJGRDTLS.MdiParent = Me
            OBJGRDTLS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OPSTOCK_FINISHEDBALEPROCESSOR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPSTOCK_FINISHEDBALEPROCESSOR.Click
        Try
            Dim OBJFINBALE As New FinishedBaleStockProcessor
            OBJFINBALE.MdiParent = Me
            OBJFINBALE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewOpeningSaleOrderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPSOADD.Click
        Try
            Dim OBJOPSO As New OpeningSaleOrder
            OBJOPSO.MdiParent = Me
            OBJOPSO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingOpeningSaleOrderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPSOEDIT.Click
        Try
            Dim OBJOPSO As New OpeningSaleOrderDetails
            OBJOPSO.MdiParent = Me
            OBJOPSO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewOpeningPurchaseOrderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPPOADD.Click
        Try
            Dim OBJOPPO As New OpeningPurchaseOrder
            OBJOPPO.MdiParent = Me
            OBJOPPO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingOpeningPurchaseOrderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPPOEDIT.Click
        Try
            Dim OBJOPPO As New OpeningPurchaseOrderDetails
            OBJOPPO.MdiParent = Me
            OBJOPPO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub WARPERYARNWASTAGEADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WARPERYARNWASTAGEADD.Click
        Try
            Dim OBJWASTAGE As New YarnWastage
            OBJWASTAGE.MdiParent = Me
            OBJWASTAGE.FRMSTRING = "WASTAGEWARPER"
            OBJWASTAGE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SIZERYARNWASTAGEADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SIZERYARNWASTAGEADD.Click
        Try
            Dim OBJWASTAGE As New YarnWastage
            OBJWASTAGE.MdiParent = Me
            OBJWASTAGE.FRMSTRING = "WASTAGESIZER"
            OBJWASTAGE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub WEAVERYARNWASTAGEADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WEAVERYARNWASTAGEADD.Click
        Try
            Dim OBJWASTAGE As New YarnWastage
            OBJWASTAGE.MdiParent = Me
            OBJWASTAGE.FRMSTRING = "WASTAGEWEAVER"
            OBJWASTAGE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub JOBBERYARNWASTAGEADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JOBBERYARNWASTAGEADD.Click
        Try
            Dim OBJWASTAGE As New YarnWastage
            OBJWASTAGE.MdiParent = Me
            OBJWASTAGE.FRMSTRING = "WASTAGEJOBBER"
            OBJWASTAGE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GODOWNYARNWASTAGEADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GODOWNYARNWASTAGEADD.Click
        Try
            Dim OBJWASTAGE As New YarnWastage
            OBJWASTAGE.MdiParent = Me
            OBJWASTAGE.FRMSTRING = "WASTAGEGODOWN"
            OBJWASTAGE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub WEAVERBEAMWASTAGEADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WEAVERBEAMWASTAGEADD.Click
        Try
            Dim OBJWASTAGE As New BeamWastage
            OBJWASTAGE.MdiParent = Me
            OBJWASTAGE.FRMSTRING = "WASTAGEWEAVER"
            OBJWASTAGE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GODOWNBEAMWASTAGEADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GODOWNBEAMWASTAGEADD.Click
        Try
            Dim OBJWASTAGE As New BeamWastage
            OBJWASTAGE.MdiParent = Me
            OBJWASTAGE.FRMSTRING = "WASTAGEGODOWN"
            OBJWASTAGE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GODOWNGREYWASTAGEADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GODOWNGREYWASTAGEADD.Click
        Try
            Dim OBJWASTAGE As New GreyWastage
            OBJWASTAGE.MdiParent = Me
            OBJWASTAGE.FRMSTRING = "GODOWNWASTAGE"
            OBJWASTAGE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub WARPERYARNWASTAGEEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WARPERYARNWASTAGEEDIT.Click
        Try
            Dim OBJWASTAGE As New YarnWastageDetails
            OBJWASTAGE.MdiParent = Me
            OBJWASTAGE.FRMSTRING = "WASTAGEWARPER"
            OBJWASTAGE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SIZERYARNWASTAGEEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SIZERYARNWASTAGEEDIT.Click
        Try
            Dim OBJWASTAGE As New YarnWastageDetails
            OBJWASTAGE.MdiParent = Me
            OBJWASTAGE.FRMSTRING = "WASTAGESIZER"
            OBJWASTAGE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub WEAVERYARNWASTAGEEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WEAVERYARNWASTAGEEDIT.Click
        Try
            Dim OBJWASTAGE As New YarnWastageDetails
            OBJWASTAGE.MdiParent = Me
            OBJWASTAGE.FRMSTRING = "WASTAGEWEAVER"
            OBJWASTAGE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub JOBBERYARNWASTAGEEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JOBBERYARNWASTAGEEDIT.Click
        Try
            Dim OBJWASTAGE As New YarnWastageDetails
            OBJWASTAGE.MdiParent = Me
            OBJWASTAGE.FRMSTRING = "WASTAGEJOBBER"
            OBJWASTAGE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GODOWNYARNWASTAGEEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GODOWNYARNWASTAGEEDIT.Click
        Try
            Dim OBJWASTAGE As New YarnWastageDetails
            OBJWASTAGE.MdiParent = Me
            OBJWASTAGE.FRMSTRING = "WASTAGEGODOWN"
            OBJWASTAGE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub WEAVERBEAMWASTAGEEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WEAVERBEAMWASTAGEEDIT.Click
        Try
            Dim OBJWASTAGE As New BeamWastageDetails
            OBJWASTAGE.MdiParent = Me
            OBJWASTAGE.FRMSTRING = "WEAVERWASTAGE"
            OBJWASTAGE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GODOWNBEAMWASTAGEEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GODOWNBEAMWASTAGEEDIT.Click
        Try
            Dim OBJWASTAGE As New BeamWastageDetails
            OBJWASTAGE.MdiParent = Me
            OBJWASTAGE.FRMSTRING = "GODOWNWASTAGE"
            OBJWASTAGE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GODOWNGREYWASTAGEEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GODOWNGREYWASTAGEEDIT.Click
        Try
            Dim OBJWASTAGE As New GreyWastageDetails
            OBJWASTAGE.MdiParent = Me
            OBJWASTAGE.FRMSTRING = "GODOWNWASTAGE"
            OBJWASTAGE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OPSTOCK_PIPEGODOWN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPSTOCK_PIPEGODOWN.Click
        Try
            Dim OBJOPPIPE As New OpeningPipes
            OBJOPPIPE.MdiParent = Me
            OBJOPPIPE.FRMSTRING = "STOREGODOWN"
            OBJOPPIPE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OPSTOCK_PIPEPARTY_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPSTOCK_PIPEPARTY.Click
        Try
            Dim OBJOPPIPE As New OpeningPipes
            OBJOPPIPE.MdiParent = Me
            OBJOPPIPE.FRMSTRING = "STORE"
            OBJOPPIPE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BALERECDADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BALERECDADD.Click
        Try
            Dim OBJBALERECD As New BaleAtProcessing
            OBJBALERECD.MdiParent = Me
            OBJBALERECD.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BALERECDEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BALERECDEDIT.Click
        Try
            Dim OBJBALERECD As New BaleAtProcessingDetails
            OBJBALERECD.MdiParent = Me
            OBJBALERECD.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNRETURNJOBBERADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNRETURNJOBBERADD.Click
        Try
            Dim OBJRETURNWEAVER As New YarnReturn
            OBJRETURNWEAVER.FRMSTRING = "RETURNFROMJOBBER"
            OBJRETURNWEAVER.MdiParent = Me
            OBJRETURNWEAVER.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNRETURNJOBBEREDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNRETURNJOBBEREDIT.Click
        Try
            Dim OBJISSUE As New YarnReturnDetails
            OBJISSUE.FRMSTRING = "RETURNFROMJOBBER"
            OBJISSUE.MdiParent = Me
            OBJISSUE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub STORESITEMADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles STORESITEMADD.Click
        Try
            Dim OBJITEM As New StoreItemMaster
            OBJITEM.MdiParent = Me
            OBJITEM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub STORESITEMEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles STORESITEMEDIT.Click
        Try
            Dim OBJITEM As New StoreItemDetails
            OBJITEM.MdiParent = Me
            OBJITEM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub STOREINWARDADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles STORESINWARDADD.Click
        Try
            Dim OBJSTORE As New StoreInward
            OBJSTORE.MdiParent = Me
            OBJSTORE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub STOREINWARDEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles STORESINWARDEDIT.Click
        Try
            Dim OBJSTORE As New StoreInwardDetails
            OBJSTORE.MdiParent = Me
            OBJSTORE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PIPEINWARDADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PIPEINWARDADD.Click
        Try
            Dim OBJSTORE As New PipeInward
            OBJSTORE.MdiParent = Me
            OBJSTORE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PIPEINWARDEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PIPEINWARDEDIT.Click
        Try
            Dim OBJSTORE As New PipeInwardDetails
            OBJSTORE.MdiParent = Me
            OBJSTORE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PIPEDESTROYEDADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PIPEDESTROYEDADD.Click
        Try
            Dim OBJSTORE As New PipeDestroyed
            OBJSTORE.MdiParent = Me
            OBJSTORE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PIPEDESTROYEDEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PIPEDESTROYEDEDIT.Click
        Try
            Dim OBJSTORE As New PipeDestroyedDetails
            OBJSTORE.MdiParent = Me
            OBJSTORE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PSADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PSADD.Click
        Try
            Dim OBJPS As New PackingSlip
            OBJPS.MdiParent = Me
            OBJPS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PSEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PSEDIT.Click
        Try
            Dim OBJPS As New PackingSlipDetails
            OBJPS.MdiParent = Me
            OBJPS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SAREEJOBOUTADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SAREEJOBOUTADD.Click
        Try
            Dim OBJJO As New SareeJobOut
            OBJJO.MdiParent = Me
            OBJJO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SAREEJOBOUTEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SAREEJOBOUTEDIT.Click
        Try
            Dim OBJJO As New SareeJobOutDetails
            OBJJO.MdiParent = Me
            OBJJO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SAREEJOBINADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SAREEJOBINADD.Click
        Try
            Dim OBJJI As New SareeJobIn
            OBJJI.MdiParent = Me
            OBJJI.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SAREEJOBINEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SAREEJOBINEDIT.Click
        Try
            Dim OBJJI As New SareeJobInDetails
            OBJJI.MdiParent = Me
            OBJJI.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OPSTOCK_FINISHEDBALEJOBBER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPSTOCK_FINISHEDBALEJOBBER.Click
        Try
            Dim OBJFINBALE As New FinishedBaleStockJobber
            OBJFINBALE.MdiParent = Me
            OBJFINBALE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OPSTOCK_FINISHEDSAREEJOBBER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPSTOCK_FINISHEDSAREEJOBBER.Click
        Try
            Dim OBJFINSAREE As New FinishedSareeStockJobber
            OBJFINSAREE.MdiParent = Me
            OBJFINSAREE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OPSTOCK_FINISHEDSAREE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPSTOCK_FINISHEDSAREE.Click
        Try
            Dim OBJFINSAREE As New FinishedSareeStockGodown
            OBJFINSAREE.MdiParent = Me
            OBJFINSAREE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PROGFILTER_MASTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PROGFILTER_MASTER.Click
        Try
            Dim OBJPROG As New ProgramFilter
            OBJPROG.MdiParent = Me
            OBJPROG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YarnIssueToWarperFilterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YarnIssueToWarperFilterToolStripMenuItem.Click
        Try
            Dim OBJISSUE As New YarnIssueFilter
            OBJISSUE.FRMSTRING = "ISSUETOWARPER"
            OBJISSUE.MdiParent = Me
            OBJISSUE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YarnIssueToSizerFilterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YarnIssueToSizerFilterToolStripMenuItem.Click
        Try
            Dim OBJISSUE As New YarnIssueFilter
            OBJISSUE.FRMSTRING = "ISSUETOSIZER"
            OBJISSUE.MdiParent = Me
            OBJISSUE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YarnIssueToWeaverFilterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YarnIssueToWeaverFilterToolStripMenuItem.Click
        Try
            Dim OBJISSUE As New YarnIssueFilter
            OBJISSUE.FRMSTRING = "ISSUETOWEAVER"
            OBJISSUE.MdiParent = Me
            OBJISSUE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RollsRecdFilterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RollsRecdFilterToolStripMenuItem.Click
        Try
            Dim OBJROLLS As New RollsReceivedFilter
            OBJROLLS.MdiParent = Me
            OBJROLLS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RollsIssuedFilterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RollsIssuedFilterToolStripMenuItem.Click
        Try
            Dim OBJROLLS As New RollsIssueFilter
            OBJROLLS.MdiParent = Me
            OBJROLLS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BeamRecdFilterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BeamRecdFilterToolStripMenuItem.Click
        Try
            Dim OBJBEAM As New BeamReceivedFilter
            OBJBEAM.MdiParent = Me
            OBJBEAM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BeamIssuedFilterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BeamIssuedFilterToolStripMenuItem.Click
        Try
            Dim OBJBEAM As New BeamIssueFilter
            OBJBEAM.MdiParent = Me
            OBJBEAM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GreyRecdFilterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GreyRecdFilterToolStripMenuItem.Click
        Try
            Dim OBJGREY As New GreyRecdFromWeaverFilter
            OBJGREY.MdiParent = Me
            OBJGREY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BaleRToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BaleRToolStripMenuItem.Click
        Try
            Dim OBJBALE As New JobOutFilter
            OBJBALE.MdiParent = Me
            OBJBALE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PURCHASECONFIG_MASTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PURCHASECONFIG_MASTER.Click
        Try
            Dim OBJPUR As New PurchaseConfig
            OBJPUR.MdiParent = Me
            OBJPUR.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YarnLedgerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNSTOCK_FILTER.Click
        Try
            Dim OBJYARN As New YarnStockFilter
            OBJYARN.MdiParent = Me
            OBJYARN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNSTOCKSIZER_FILTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNSTOCKSIZER_FILTER.Click
        Try
            Dim OBJSTK As New SizerYarnStockFilter
            OBJSTK.MdiParent = Me
            OBJSTK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DOFilterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DOFilterToolStripMenuItem.Click
        Try
            Dim OBJGRN As New DOFilter
            OBJGRN.MdiParent = Me
            OBJGRN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OPSTOCK_STOREGODOWN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPSTOCK_STOREGODOWN.Click
        Try
            Dim OBJOPSTORE As New OpeningStoreStock
            OBJOPSTORE.MdiParent = Me
            OBJOPSTORE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OPSTOCK_STOREPARTY_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPSTOCK_STOREPARTY.Click
        Try
            Dim OBJOPJOBBER As New OpeningStoreStockJobber
            OBJOPJOBBER.MdiParent = Me
            OBJOPJOBBER.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OPENINGBALANCE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPENINGBALANCE.Click
        Try
            Dim OBJOP As New OpeningBalance
            OBJOP.MdiParent = Me
            OBJOP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNSTOCKWAREHOUSE_FILTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNSTOCKWAREHOUSE_FILTER.Click
        Try
            Dim OBJYARN As New YarnStockWareHouseFilter
            OBJYARN.MdiParent = Me
            OBJYARN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNSTOCKWEAVER_FILTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNSTOCKWEAVER_FILTER.Click
        Try
            Dim OBJSTK As New WeaverYarnStockFilter
            OBJSTK.MdiParent = Me
            OBJSTK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BEAMSTOCKWEAVER_FILTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BEAMSTOCKWEAVER_FILTER.Click
        Try
            Dim OBJSTK As New WeaverBeamStockFilter
            OBJSTK.MdiParent = Me
            OBJSTK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BEAMSTOCKSIZER_FILTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BEAMSTOCKSIZER_FILTER.Click
        Try
            Dim OBJSTK As New SizerBeamStockFilter
            OBJSTK.MdiParent = Me
            OBJSTK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PENDINGCONE_MASTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PENDINGCONE_MASTER.Click
        Try
            Dim OBJCONES As New PendingConeDetails
            OBJCONES.MdiParent = Me
            OBJCONES.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DOGODOWNCHGS_MASTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DOGODOWNCHGS_MASTER.Click
        Try
            Dim OBJCHGS As New DOGodownChargesDetails
            OBJCHGS.MdiParent = Me
            OBJCHGS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PENDINGLOTNO_MASTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PENDINGLOTNO_MASTER.Click
        Try
            Dim OBJLOT As New PendingLotno
            OBJLOT.MdiParent = Me
            OBJLOT.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PENDINGRETURNDATE_MASTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PENDINGRETURNDATE_MASTER.Click
        Try
            Dim OBJRDATE As New PendingReturnDate
            OBJRDATE.MdiParent = Me
            OBJRDATE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PENDINGRETURNDONO_MASTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PENDINGRETURNDONO_MASTER.Click
        Try
            Dim OBJRDONO As New PendingReturnDONo
            OBJRDONO.MdiParent = Me
            OBJRDONO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SAREELOTDONE_MASTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SAREELOTDONE_MASTER.Click
        Try
            Dim OBJLOTDONE As New SareeLotDone
            OBJLOTDONE.MdiParent = Me
            OBJLOTDONE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GREYSTOCK_FILTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GREYSTOCK_FILTER.Click
        Try
            Dim OBJGREY As New GreyStockFilter
            OBJGREY.MdiParent = Me
            OBJGREY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GREYSTOCKWITHDYEING_FILTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GREYSTOCKWITHDYEING_FILTER.Click
        Try
            Dim OBJGREY As New GreyStockWithDyeingFilter
            OBJGREY.MdiParent = Me
            OBJGREY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaleOrderFilterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaleOrderFilterToolStripMenuItem.Click
        Try
            Dim OBJSO As New SOFilter
            OBJSO.MdiParent = Me
            OBJSO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GREYRETURN_TOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GREYRETURN_TOOL.Click
        Try
            If ClientName <> "SASHWINKUMAR" Then
                Dim OBJRET As New SaleReturn
                OBJRET.MdiParent = Me
                OBJRET.Show()
            Else
                Dim OBJRET As New GoodsReturn
                OBJRET.MdiParent = Me
                OBJRET.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MISREPORT_FILTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MISREPORT_FILTER.Click
        Try
            Dim OBJMIS As New MISFilter
            OBJMIS.MdiParent = Me
            OBJMIS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripMenuItem2_Click_3(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        Try
            Dim OBJCOMP As New ComplaintRegister
            OBJCOMP.MdiParent = Me
            OBJCOMP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingComplaintToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditExistingComplaintToolStripMenuItem.Click
        Try
            Dim OBJCOMP As New ComplaintRegisterDetails
            OBJCOMP.MdiParent = Me
            OBJCOMP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNSTOCKWARPER_FILTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNSTOCKWARPER_FILTER.Click
        Try
            Dim OBJSTK As New WarperYarnStockFilter
            OBJSTK.MdiParent = Me
            OBJSTK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PENDINGGR_MASTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PENDINGGR_MASTER.Click
        Try
            Dim OBJGR As New PendingGRReport
            OBJGR.MdiParent = Me
            OBJGR.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LOCKPENINGBEAMS_MASTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LOCKPENINGBEAMS_MASTER.Click
        Try
            Dim OBJBEAMS As New LockPendingBeams
            OBJBEAMS.MdiParent = Me
            OBJBEAMS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GoodsReturnFilterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GoodsReturnFilterToolStripMenuItem.Click
        Try
            Dim OBJGR As New GRFilter
            OBJGR.MdiParent = Me
            OBJGR.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PURRETURNCONFIG_MASTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PURRETURNCONFIG_MASTER.Click
        Try
            Dim OBJPUR As New PurchaseReturnConfig
            OBJPUR.MdiParent = Me
            OBJPUR.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PURRETURNREGISTERADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PURRETURNREGISTERADD.Click
        Try
            Dim objregistermaster As New RegisterMaster
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "PURCHASERETURN"
            objregistermaster.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SALERETURNREGISTERADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SALERETURNREGISTERADD.Click
        Try
            Dim objregistermaster As New RegisterMaster
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "SALERETURN"
            objregistermaster.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SALERETADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SALERETADD.Click
        Try
            Dim OBJSALRET As New SaleReturn
            OBJSALRET.MdiParent = Me
            OBJSALRET.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SALERETEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SALERETEDIT.Click
        Try
            Dim OBJSALRET As New SaleReturnDetails
            OBJSALRET.MdiParent = Me
            OBJSALRET.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PurchaseReturnRegisterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurchaseReturnRegisterToolStripMenuItem.Click
        Try
            Dim objpurreg As New PurchaseReturnRegister
            objpurreg.MdiParent = Me
            objpurreg.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaleReturnRegisterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaleReturnRegisterToolStripMenuItem.Click
        Try
            Dim objsalereg As New SaleReturnRegister
            objsalereg.MdiParent = Me
            objsalereg.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BALERETURNPROCESSORADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BALERETURNPROCESSORADD.Click
        Try
            Dim OBJBALERET As New BaleReturnfromProcessing
            OBJBALERET.MdiParent = Me
            OBJBALERET.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BALERETURNPROCESSOREDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BALERETURNPROCESSOREDIT.Click
        Try
            Dim OBJBALERET As New BaleReturnfromProcessingDetails
            OBJBALERET.MdiParent = Me
            OBJBALERET.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PurchaseReturnFilterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurchaseReturnFilterToolStripMenuItem.Click
        Try
            Dim OBJPURRETFILTER As New PurchaseReturnFilter
            OBJPURRETFILTER.MdiParent = Me
            OBJPURRETFILTER.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaleInvoiceReturnFilterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaleInvoiceReturnFilterToolStripMenuItem.Click
        Try
            Dim OBJSALRETFILTER As New SaleReturnFilter
            OBJSALRETFILTER.MdiParent = Me
            OBJSALRETFILTER.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub AddNewToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BALEOPENADD.Click
        Try
            Dim OBJBO As New BaleOpen
            OBJBO.MdiParent = Me
            OBJBO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripMenuItem4_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BALEOPENEDIT.Click
        Try
            Dim OBJBO As New BaleOpenDetails
            OBJBO.MdiParent = Me
            OBJBO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OPSTOCK_BEAMGODOWN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPSTOCK_BEAMGODOWN.Click
        Try
            Dim OBJOPSTOCKBEAM As New OpeningStockBeamGodown
            OBJOPSTOCKBEAM.MdiParent = Me
            OBJOPSTOCKBEAM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BALESTOCK_FILTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BALESTOCK_FILTER.Click
        Try
            Dim OBJBALE As New BaleStockFilter
            OBJBALE.MdiParent = Me
            OBJBALE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BALESTOCKWITHDYEING_FILTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BALESTOCKWITHDYEING_FILTER.Click
        Try
            Dim OBJBALE As New BaleStockDyeingFilter
            OBJBALE.MdiParent = Me
            OBJBALE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub BeamWastageInhouseFilterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BeamWastageInhouseFilterToolStripMenuItem1.Click
        Try
            Dim OBJBALE As New BeamWastageInhouseFilter
            OBJBALE.MdiParent = Me
            OBJBALE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GreyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GreyToolStripMenuItem.Click
        Try
            Dim OBJBALE As New GreyWastageFilter
            OBJBALE.MdiParent = Me
            OBJBALE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub WeaverWiseToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WeaverWiseToolStripMenuItem1.Click
        Try
            Dim objregistermaster As New YarnWastageFilter
            objregistermaster.MdiParent = Me
            objregistermaster.FRMSTRING = "WASTAGEWEAVER"
            objregistermaster.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BaleOpenFilterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BaleOpenFilterToolStripMenuItem.Click
        Try
            Dim OBJBO As New BaleOpenFilter
            OBJBO.MdiParent = Me
            OBJBO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SizerWiseToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SizerWiseToolStripMenuItem.Click
        Try
            Dim objregistermaster As New YarnWastageFilter
            objregistermaster.MdiParent = Me
            objregistermaster.FRMSTRING = "WASTAGESIZER"
            objregistermaster.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub WarperWiseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WarperWiseToolStripMenuItem.Click
        Try
            Dim objregistermaster As New YarnWastageFilter
            objregistermaster.MdiParent = Me
            objregistermaster.FRMSTRING = "WASTAGEWARPER"
            objregistermaster.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub WeaverWiseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WeaverWiseToolStripMenuItem.Click
        Try
            Dim objregistermaster As New BeamWastageWeaverFilter
            objregistermaster.MdiParent = Me
            'objregistermaster.FRMSTRING = "WASTAGEWARPER"
            objregistermaster.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BeamWastageInhouseFilterToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BeamWastageInhouseFilterToolStripMenuItem1.Click
        Try
            Dim objregistermaster As New BeamWastageInhouseFilter
            objregistermaster.MdiParent = Me
            'objregistermaster.FRMSTRING = "WASTAGEWARPER"
            objregistermaster.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BaleReturnFilterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BaleReturnFilterToolStripMenuItem.Click
        Try
            Dim OBJBR As New BaleReturnFilter
            OBJBR.MdiParent = Me
            OBJBR.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RollReturnFilterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RollReturnFilterToolStripMenuItem.Click
        Try
            Dim OBJROLLS As New RollsReturnFilter
            OBJROLLS.MdiParent = Me
            OBJROLLS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BeamReturnFilterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BeamReturnFilterToolStripMenuItem.Click
        Try
            Dim OBJBEAM As New BeamReturnFilter
            OBJBEAM.MdiParent = Me
            OBJBEAM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YarnReturnToSizerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YarnReturnToSizerToolStripMenuItem.Click
        Try
            Dim OBJSIZER As New YarnReturnFilter
            OBJSIZER.FRMSTRING = "RETURNTOSIZER"
            OBJSIZER.MdiParent = Me
            OBJSIZER.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YarnReturnToWarperToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YarnReturnToWarperToolStripMenuItem.Click
        Try
            Dim OBJWARPER As New YarnReturnFilter
            OBJWARPER.FRMSTRING = "RETURNTOWARPER"
            OBJWARPER.MdiParent = Me
            OBJWARPER.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YarnReturnToWeaverToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YarnReturnToWeaverToolStripMenuItem.Click
        Try
            Dim OBJWEAVER As New YarnReturnFilter
            OBJWEAVER.FRMSTRING = "RETURNTOWEAVER"
            OBJWEAVER.MdiParent = Me
            OBJWEAVER.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YarnReturnToJobberToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YarnReturnToJobberToolStripMenuItem.Click
        Try
            Dim OBJJOBBER As New YarnReturnFilter
            OBJJOBBER.FRMSTRING = "RETURNTOJOBBER"
            OBJJOBBER.MdiParent = Me
            OBJJOBBER.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PurchaseInvoiceYarnToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurchaseInvoiceYarnToolStripMenuItem.Click
        Try
            Dim OBJPUR As New PurchaseInvoiceFilter
            OBJPUR.FRMSTRING = "PURCHASEYARN"
            OBJPUR.MdiParent = Me
            OBJPUR.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PurchaseInvoiceGreyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurchaseInvoiceGreyToolStripMenuItem.Click
        Try
            Dim OBJPUR As New PurchaseInvoiceFilter
            OBJPUR.FRMSTRING = "PURCHASEGREY"
            OBJPUR.MdiParent = Me
            OBJPUR.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub JobOutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JobOutToolStripMenuItem.Click
        Try
            Dim OBJJOB As New JobOutFilter
            OBJJOB.MdiParent = Me
            OBJJOB.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PipeInwardFilterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PipeInwardFilterToolStripMenuItem.Click
        Try
            Dim OBJPI As New PipeInwardFilter
            OBJPI.MdiParent = Me
            OBJPI.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PurchaseInvoiceOtherToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurchaseInvoiceOtherToolStripMenuItem.Click
        Try
            Dim OBJPUR As New PurchaseInvoiceFilter
            OBJPUR.FRMSTRING = "OTHERPURCHASE"
            OBJPUR.MdiParent = Me
            OBJPUR.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PipeDestroyedFilterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PipeDestroyedFilterToolStripMenuItem.Click
        Try
            Dim OBJPD As New PipeDestroyedFilter
            OBJPD.MdiParent = Me
            OBJPD.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub StoreInwardFilterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StoreInwardFilterToolStripMenuItem.Click
        Try
            Dim OBJSI As New StoreInwardFilter
            OBJSI.MdiParent = Me
            OBJSI.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub STOCKTRANSFER_MENU_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles STOCKTRANSFER_MENU.Click
        Try
            Dim OBJYEAR As New YearTransfer
            OBJYEAR.MdiParent = Me
            OBJYEAR.FRMSTRING = "STOCKTRANSFER"
            OBJYEAR.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub USERTRANSFER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles USERTRANSFER.Click
        Try
            Dim OBJYEAR As New YearTransfer
            OBJYEAR.FRMSTRING = "USERTRANSFER"
            OBJYEAR.MdiParent = Me
            OBJYEAR.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OPENINGSTOCKVALUE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPENINGSTOCKVALUE.Click
        Try
            Dim OBJOP As New OpeningClosingStock
            OBJOP.MdiParent = Me
            OBJOP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub HSNADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HSNADD.Click
        Try
            Dim OBJHSN As New HSNMaster
            OBJHSN.MdiParent = Me
            OBJHSN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub HSNEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HSNEDIT.Click
        Try
            Dim OBJHSN As New HSNDetails
            OBJHSN.MdiParent = Me
            OBJHSN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpeningStockPendingRetDateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPSTOCK_RETDATE.Click
        Try
            Dim OBJOP As New OpeningPendingReturnDate
            OBJOP.MdiParent = Me
            OBJOP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNISSUEDYEINGADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNISSUEDYEINGADD.Click
        Try
            Dim OBJISSUE As New YarnIssue
            OBJISSUE.FRMSTRING = "ISSUETODYEING"
            OBJISSUE.MdiParent = Me
            OBJISSUE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNISSUEDYEINGEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNISSUEDYEINGEDIT.Click
        Try
            Dim OBJISSUE As New YarnIssueDetails
            OBJISSUE.FRMSTRING = "ISSUETODYEING"
            OBJISSUE.MdiParent = Me
            OBJISSUE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewColorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles COLORADD.Click
        Try
            Dim OBJCOLOR As New ColorMaster
            OBJCOLOR.MdiParent = Me
            OBJCOLOR.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub COLOREDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles COLOREDIT.Click
        Try
            Dim OBJCOLOR As New ColorDetails
            OBJCOLOR.MdiParent = Me
            OBJCOLOR.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OPSTOCK_DYEINGYARN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPSTOCK_DYEINGYARN.Click
        Try
            Dim OBJCFORM As New OpeningStockYarnwithSizer
            OBJCFORM.FRMSTRING = "YARNDYEING"
            OBJCFORM.MdiParent = Me
            OBJCFORM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewEntryToolStripMenuItem_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNRECDDYEINGADD.Click
        Try
            Dim OBJCFORM As New YarnReceivedDyeing
            OBJCFORM.MdiParent = Me
            OBJCFORM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNRECD_EDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNRECDDYEINGEDIT.Click
        Try
            Dim OBJCFORM As New YarnReceivedDyeingDetails
            OBJCFORM.MdiParent = Me
            OBJCFORM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YarnIssueToDyeingFilterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YarnIssueToDyeingFilterToolStripMenuItem.Click
        Try
            Dim OBJISSUE As New YarnIssueFilter
            OBJISSUE.FRMSTRING = "ISSUETODYEING"
            OBJISSUE.MdiParent = Me
            OBJISSUE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YarnStockWithDyeingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YarnStockWithDyeingToolStripMenuItem.Click
        Try
            Dim OBJSTK As New DyeingYarnStockFilter
            OBJSTK.MdiParent = Me
            OBJSTK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SIZERBEAMWASTAGEADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SIZERBEAMWASTAGEADD.Click
        Try
            Dim OBJWASTAGE As New BeamWastage
            OBJWASTAGE.MdiParent = Me
            OBJWASTAGE.FRMSTRING = "WASTAGESIZER"
            OBJWASTAGE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SIZERBEAMWASTAGEEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SIZERBEAMWASTAGEEDIT.Click
        Try
            Dim OBJWASTAGE As New BeamWastageDetails
            OBJWASTAGE.MdiParent = Me
            OBJWASTAGE.FRMSTRING = "SIZERWASTAGE"
            OBJWASTAGE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GSTTAXREGISTER_MASTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GSTTAXREGISTER_MASTER.Click
        Try
            Dim OBJGST As New GSTTaxFilter
            OBJGST.MdiParent = Me
            OBJGST.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNRETURNDYEINGADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNRETURNDYEINGADD.Click
        Try
            Dim OBJRETURNWEAVER As New YarnReturn
            OBJRETURNWEAVER.FRMSTRING = "RETURNFROMDYEING"
            OBJRETURNWEAVER.MdiParent = Me
            OBJRETURNWEAVER.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNRETURNDYEINGEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YARNRETURNDYEINGEDIT.Click
        Try
            Dim OBJISSUE As New YarnReturnDetails
            OBJISSUE.FRMSTRING = "RETURNFROMDYEING"
            OBJISSUE.MdiParent = Me
            OBJISSUE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OPSTOCK_CUTWEAVER_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OPSTOCK_CUTWEAVER.Click
        Try
            Dim OBJCUTWEAVER As New OpeningStockBeamCutWithWeaver
            OBJCUTWEAVER.MdiParent = Me
            OBJCUTWEAVER.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MATCHINGADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MATCHINGADD.Click
        Try
            Dim OBJMATCH As New ManualMatching
            OBJMATCH.MdiParent = Me
            OBJMATCH.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MATCHINGEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MATCHINGEDIT.Click
        Try
            Dim OBJMATCH As New ManualMatchingDetails
            OBJMATCH.MdiParent = Me
            OBJMATCH.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PIPEOUTWARDADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PIPEOUTWARDADD.Click
        Try
            Dim OBJpipe As New PipeOutward
            OBJpipe.MdiParent = Me
            OBJpipe.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PIPEOUTWARDEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PIPEOUTWARDEDIT.Click
        Try
            Dim OBJpipe As New PipeOutwardDetails
            OBJpipe.MdiParent = Me
            OBJpipe.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRDYEINGUPDATE_MASTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GRDYEINGUPDATE_MASTER.Click
        Try
            Dim OBJpipe As New GRDyeingUpdate
            OBJpipe.MdiParent = Me
            OBJpipe.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MDIMain_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            If MULTIYARN = True Then
                BEAMRECWARPER_MASTER.Visible = True
                ROLLSRECD_MASTER.Visible = False
                ROLLSISSUE_MASTER.Visible = False
                TOOLSTRIPROLLS.Visible = False
            End If

            If ClientName <> "SASHWINKUMAR" Then
                GREYRETURN_TOOL.Text = "Sale Return"
            End If

            If ClientName = "SHREEJI" Then
                DO_TOOL.Visible = False
                DO_TOOLSTRIP.Visible = False
                DO_MASTER.Visible = False
                DO_MASTER_TOOLSTRIP.Visible = False
                BEAMUPLOAD_MASTER.Visible = True

                BEAMREC_MASTER.Visible = False '(BEAMREC FROM SIZER)
                GREYRECWEAVERSUMM_MASTER.Visible = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LOOMWISECUT_MENU_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LOOMWISECUT_MENU.Click
        Try
            Dim OBJCUT As New LoomWiseCutBalance
            OBJCUT.MdiParent = Me
            OBJCUT.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PendingInvoiceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PendingInvoiceToolStripMenuItem.Click
        Try
            Dim OBJPENCHALLAN As New PendingChallanForInvoice
            OBJPENCHALLAN.MdiParent = Me
            OBJPENCHALLAN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TDSDeductedNotDedictedReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TDSDeductedNotDedictedReportToolStripMenuItem.Click
        Try
            Dim OBJTDS As New TDSDeductedReport
            OBJTDS.MdiParent = Me
            OBJTDS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem3.Click
        Try
            Dim OBJTDS As New SendSMS
            OBJTDS.MdiParent = Me
            OBJTDS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OutstandingReminderSMSToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OutstandingReminderSMSToolStripMenuItem.Click
        Try
            Dim OBJTDS As New OutstandingReminderSMS
            OBJTDS.MdiParent = Me
            OBJTDS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PendingDOInwardsDetailsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PendingDOInwardsDetailsToolStripMenuItem.Click
        Try
            Dim OBJTDS As New PendingDOInwardsDetails
            OBJTDS.MdiParent = Me
            OBJTDS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PendingPurchaseInvoiceDetailsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PendingPurchaseInvoiceDetailsToolStripMenuItem.Click
        Try
            Dim OBJTDS As New PendingPurchaseInvoiceDetails_MASHOK
            OBJTDS.MdiParent = Me
            OBJTDS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GREYRECDWEAVERSUMMADD_Click(sender As Object, e As EventArgs) Handles GREYRECWEAVERSUMMADD.Click
        Try
            Dim OBJRET As New GreyRecdWeaverSummary
            OBJRET.MdiParent = Me
            OBJRET.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GREYRECDWEAVERSUMMEDIT_Click(sender As Object, e As EventArgs) Handles GREYRECWEAVERSUMMEDIT.Click
        Try
            Dim OBJRET As New GreyRecdWeaverSummaryDetails
            OBJRET.MdiParent = Me
            OBJRET.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MACHINEADD_Click(sender As Object, e As EventArgs) Handles MACHINEADD.Click
        Try
            Dim OBJMACHINE As New MachineMaster
            OBJMACHINE.MdiParent = Me
            OBJMACHINE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MACHINEEDIT_Click(sender As Object, e As EventArgs) Handles MACHINEEDIT.Click
        Try
            Dim OBJMACHINE As New MachineDetails
            OBJMACHINE.MdiParent = Me
            OBJMACHINE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNISSUEMACADD_Click(sender As Object, e As EventArgs) Handles YARNISSUEMACADD.Click
        Try
            Dim OBJYARNISSUE As New YarnIssueMachine
            OBJYARNISSUE.MdiParent = Me
            OBJYARNISSUE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNISSUEMACEDIT_Click(sender As Object, e As EventArgs) Handles YARNISSUEMACEDIT.Click
        Try
            Dim OBJYARNISSUE As New YarnIssueMachineDetails
            OBJYARNISSUE.MdiParent = Me
            OBJYARNISSUE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNRECEIVEDMACHINEADD_Click(sender As Object, e As EventArgs) Handles YARNRECEIVEDMACHINEADD.Click
        Try
            Dim OBJYARNREC As New YarnReceivedMachine
            OBJYARNREC.MdiParent = Me
            OBJYARNREC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNRECEIVEDMACHINEEDIT_Click(sender As Object, e As EventArgs) Handles YARNRECEIVEDMACHINEEDIT.Click
        Try
            Dim OBJYARNREC As New YarnReceivedMachineDetails
            OBJYARNREC.MdiParent = Me
            OBJYARNREC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNRETURNMACHINEADD_Click(sender As Object, e As EventArgs) Handles YARNRETURNMACHINEADD.Click
        Try
            Dim OBJRETURNMACHINE As New YarnReturn
            OBJRETURNMACHINE.FRMSTRING = "RETURNFROMMACHINE"
            OBJRETURNMACHINE.MdiParent = Me
            OBJRETURNMACHINE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNRETURNMACHINEEDIT_Click(sender As Object, e As EventArgs) Handles YARNRETURNMACHINEEDIT.Click
        Try
            Dim OBJRETURNMACHINE As New YarnReturnDetails
            OBJRETURNMACHINE.FRMSTRING = "RETURNFROMMACHINE"
            OBJRETURNMACHINE.MdiParent = Me
            OBJRETURNMACHINE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OPSTOCK_MACHINEYARN_Click(sender As Object, e As EventArgs) Handles OPSTOCK_MACHINEYARN.Click
        Try
            Dim OBJYARNMACHINE As New OpeningStockYarnOnMachine
            OBJYARNMACHINE.MdiParent = Me
            OBJYARNMACHINE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNSTOCKMACHINE_FILTER_Click(sender As Object, e As EventArgs) Handles YARNSTOCKMACHINE_FILTER.Click
        Try
            Dim OBJSTK As New MachineYarnStockFilter
            OBJSTK.MdiParent = Me
            OBJSTK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MACHINEYARNWASTAGEEDIT_Click(sender As Object, e As EventArgs) Handles MACHINEYARNWASTAGEEDIT.Click
        Try
            Dim OBJWASTAGE As New YarnWastageDetails
            OBJWASTAGE.MdiParent = Me
            OBJWASTAGE.FRMSTRING = "WASTAGEMACHINE"
            OBJWASTAGE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MACHINEYARNWASTAGEADD_Click(sender As Object, e As EventArgs) Handles MACHINEYARNWASTAGEADD.Click
        Try
            Dim OBJWASTAGE As New YarnWastage
            OBJWASTAGE.MdiParent = Me
            OBJWASTAGE.FRMSTRING = "WASTAGEMACHINE"
            OBJWASTAGE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BEAMRECWARPERADD_Click(sender As Object, e As EventArgs) Handles BEAMRECWARPERADD.Click
        Try
            Dim OBJBEAMREC As New BeamRecdWarper
            OBJBEAMREC.MdiParent = Me
            OBJBEAMREC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BEAMRECWARPEREDIT_Click(sender As Object, e As EventArgs) Handles BEAMRECWARPEREDIT.Click
        Try
            Dim OBJBEAMREC As New BeamRecdWarperDetails
            OBJBEAMREC.MdiParent = Me
            OBJBEAMREC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DYEINGYARNWASTAGEADD_Click(sender As Object, e As EventArgs) Handles DYEINGYARNWASTAGEADD.Click
        Try
            Dim OBJWASTAGE As New YarnWastage
            OBJWASTAGE.MdiParent = Me
            OBJWASTAGE.FRMSTRING = "WASTAGEDYEING"
            OBJWASTAGE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DYEINGYARNWASTAGEEDIT_Click(sender As Object, e As EventArgs) Handles DYEINGYARNWASTAGEEDIT.Click
        Try
            Dim OBJWASTAGE As New YarnWastageDetails
            OBJWASTAGE.MdiParent = Me
            OBJWASTAGE.FRMSTRING = "WASTAGEDYEING"
            OBJWASTAGE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UploadAccountsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UploadAccountsToolStripMenuItem.Click
        Try
            If InputBox("Enter Master Password") <> "Infosys@123" Then Exit Sub

            '************************************ LEDGER UPLOAD ****************************
            'upload the files data
            ''Reading from Excel Woorkbook
            Dim cPart As Microsoft.Office.Interop.Excel.Range
            Dim oExcel As Microsoft.Office.Interop.Excel.Application = CreateObject("Excel.Application")
            Dim oBook As Microsoft.Office.Interop.Excel.Workbook = oExcel.Workbooks.Open("D:\" & InputBox("Enter File Name").ToString.Trim, , False)
            Dim oSheet As New Microsoft.Office.Interop.Excel.Worksheet
            oSheet = oBook.Worksheets("Sheet1")

            'GRID
            Dim ADDITEM As Boolean = True
            Dim TEMPITEMNAME As String = ""

            Dim DTSAVE As New System.Data.DataTable
            DTSAVE.Columns.Add("CODE")
            DTSAVE.Columns.Add("COMPANYNAME")
            DTSAVE.Columns.Add("ADD1")
            DTSAVE.Columns.Add("ADD2")
            DTSAVE.Columns.Add("ADDRESS")
            DTSAVE.Columns.Add("CITYNAME")
            DTSAVE.Columns.Add("PINNO")
            DTSAVE.Columns.Add("STATE")
            DTSAVE.Columns.Add("COUNTRY")
            DTSAVE.Columns.Add("PHONENO")
            DTSAVE.Columns.Add("MOBILENO")
            DTSAVE.Columns.Add("GSTIN")
            DTSAVE.Columns.Add("GROUPNAME")
            DTSAVE.Columns.Add("PANNO")
            DTSAVE.Columns.Add("BROKER")
            DTSAVE.Columns.Add("TRANSPORT")
            DTSAVE.Columns.Add("EMAIL")
            DTSAVE.Columns.Add("CRDAYS")
            DTSAVE.Columns.Add("SALESMAN")
            DTSAVE.Columns.Add("TDSPER")
            DTSAVE.Columns.Add("TDSSECTION")
            DTSAVE.Columns.Add("CMPNONCMP")
            DTSAVE.Columns.Add("DISCOUNT")
            DTSAVE.Columns.Add("CASHDISC")
            DTSAVE.Columns.Add("COMMISSION")
            DTSAVE.Columns.Add("SHIPPINGADD")

            Dim ARR As New ArrayList
            Dim COLIND As Integer = 0
            Dim DTROWSAVE As System.Data.DataRow = DTSAVE.NewRow()

            Dim FROMROWNO As Integer = Val(InputBox("Enter Start Row No"))
            Dim TOROWNO As Integer = Val(InputBox("Enter End Row No"))

            For I As Integer = FROMROWNO To TOROWNO

                If IsDBNull(oSheet.Range("A" & I.ToString).Text) = False Then
                    DTROWSAVE("CODE") = oSheet.Range("A" & I.ToString).Text
                Else
                    DTROWSAVE("CODE") = ""
                End If

                If IsDBNull(oSheet.Range("B" & I.ToString).Text) = False Then
                    DTROWSAVE("COMPANYNAME") = oSheet.Range("B" & I.ToString).Text
                Else
                    DTROWSAVE("COMPANYNAME") = ""
                End If

                If IsDBNull(oSheet.Range("C" & I.ToString).Text) = False Then
                    DTROWSAVE("ADD1") = oSheet.Range("C" & I.ToString).Text
                Else
                    DTROWSAVE("ADD1") = ""
                End If

                If IsDBNull(oSheet.Range("D" & I.ToString).Text) = False Then
                    DTROWSAVE("ADD2") = oSheet.Range("D" & I.ToString).Text
                Else
                    DTROWSAVE("ADD2") = ""
                End If

                If IsDBNull(oSheet.Range("E" & I.ToString).Text) = False Then
                    DTROWSAVE("ADDRESS") = oSheet.Range("E" & I.ToString).Text
                Else
                    DTROWSAVE("ADDRESS") = ""
                End If

                If IsDBNull(oSheet.Range("F" & I.ToString).Text) = False Then
                    DTROWSAVE("CITYNAME") = oSheet.Range("F" & I.ToString).Text
                Else
                    DTROWSAVE("CITYNAME") = ""
                End If

                If IsDBNull(oSheet.Range("G" & I.ToString).Text) = False Then
                    DTROWSAVE("PINNO") = oSheet.Range("G" & I.ToString).Text
                Else
                    DTROWSAVE("PINNO") = 0
                End If

                If IsDBNull(oSheet.Range("H" & I.ToString).Text) = False Then
                    DTROWSAVE("STATE") = oSheet.Range("H" & I.ToString).Text
                Else
                    DTROWSAVE("STATE") = ""
                End If

                If IsDBNull(oSheet.Range("I" & I.ToString).Text) = False Then
                    DTROWSAVE("COUNTRY") = oSheet.Range("I" & I.ToString).Text
                Else
                    DTROWSAVE("COUNTRY") = ""
                End If

                If IsDBNull(oSheet.Range("J" & I.ToString).Text) = False Then
                    DTROWSAVE("PHONENO") = oSheet.Range("J" & I.ToString).Text
                Else
                    DTROWSAVE("PHONENO") = ""
                End If

                If IsDBNull(oSheet.Range("K" & I.ToString).Text) = False Then
                    DTROWSAVE("MOBILENO") = oSheet.Range("K" & I.ToString).Text
                Else
                    DTROWSAVE("MOBILENO") = 0
                End If


                If IsDBNull(oSheet.Range("L" & I.ToString).Text) = False Then
                    DTROWSAVE("GSTIN") = oSheet.Range("L" & I.ToString).Text
                Else
                    DTROWSAVE("GSTIN") = ""
                End If

                If IsDBNull(oSheet.Range("M" & I.ToString).Text) = False Then
                    DTROWSAVE("GROUPNAME") = oSheet.Range("M" & I.ToString).Text
                Else
                    DTROWSAVE("GROUPNAME") = ""
                End If

                If IsDBNull(oSheet.Range("N" & I.ToString).Text) = False Then
                    DTROWSAVE("PANNO") = oSheet.Range("N" & I.ToString).Text
                Else
                    DTROWSAVE("PANNO") = ""
                End If

                If IsDBNull(oSheet.Range("O" & I.ToString).Text) = False Then
                    DTROWSAVE("BROKER") = oSheet.Range("O" & I.ToString).Text
                Else
                    DTROWSAVE("BROKER") = ""
                End If

                If IsDBNull(oSheet.Range("P" & I.ToString).Text) = False Then
                    DTROWSAVE("TRANSPORT") = oSheet.Range("P" & I.ToString).Text
                Else
                    DTROWSAVE("TRANSPORT") = ""
                End If

                If IsDBNull(oSheet.Range("Q" & I.ToString).Text) = False Then
                    DTROWSAVE("EMAIL") = oSheet.Range("Q" & I.ToString).Text
                Else
                    DTROWSAVE("EMAIL") = ""
                End If

                If IsDBNull(oSheet.Range("R" & I.ToString).Text) = False Then
                    DTROWSAVE("CRDAYS") = oSheet.Range("R" & I.ToString).Text
                Else
                    DTROWSAVE("CRDAYS") = ""
                End If

                If IsDBNull(oSheet.Range("S" & I.ToString).Text) = False Then
                    DTROWSAVE("SALESMAN") = oSheet.Range("S" & I.ToString).Text
                Else
                    DTROWSAVE("SALESMAN") = ""
                End If

                If IsDBNull(oSheet.Range("T" & I.ToString).Text) = False Then
                    DTROWSAVE("TDSPER") = oSheet.Range("T" & I.ToString).Text
                Else
                    DTROWSAVE("TDSPER") = ""
                End If

                If IsDBNull(oSheet.Range("U" & I.ToString).Text) = False Then
                    DTROWSAVE("TDSSECTION") = oSheet.Range("U" & I.ToString).Text
                Else
                    DTROWSAVE("TDSSECTION") = ""
                End If

                If IsDBNull(oSheet.Range("V" & I.ToString).Text) = False Then
                    DTROWSAVE("CMPNONCMP") = oSheet.Range("V" & I.ToString).Text
                Else
                    DTROWSAVE("CMPNONCMP") = ""
                End If

                If IsDBNull(oSheet.Range("W" & I.ToString).Text) = False Then
                    DTROWSAVE("DISCOUNT") = oSheet.Range("W" & I.ToString).Text
                Else
                    DTROWSAVE("DISCOUNT") = ""
                End If

                If IsDBNull(oSheet.Range("X" & I.ToString).Text) = False Then
                    DTROWSAVE("CASHDISC") = oSheet.Range("X" & I.ToString).Text
                Else
                    DTROWSAVE("CASHDISC") = ""
                End If

                If IsDBNull(oSheet.Range("Y" & I.ToString).Text) = False Then
                    DTROWSAVE("COMMISSION") = oSheet.Range("Y" & I.ToString).Text
                Else
                    DTROWSAVE("COMMISSION") = ""
                End If

                If IsDBNull(oSheet.Range("Z" & I.ToString).Text) = False Then
                    DTROWSAVE("SHIPPINGADD") = oSheet.Range("Z" & I.ToString).Text
                Else
                    DTROWSAVE("SHIPPINGADD") = ""
                End If




                Dim ALPARAVAL As New ArrayList
                Dim OBJCMN As New ClsCommon
                Dim DTTABLE As DataTable = OBJCMN.search("CITY_ID AS CITYID", "", "CITYMASTER ", "AND CITY_NAME = '" & DTROWSAVE("CITYNAME") & "' AND CITY_YEARID = " & YearId)
                If DTTABLE.Rows.Count = 0 Then
                    'ADD NEW CITYNAME
                    Dim objyearmaster As New ClsYearMaster
                    objyearmaster.savecity(DTROWSAVE("CITYNAME"), CmpId, Locationid, Userid, YearId, " and city_name = '" & DTROWSAVE("CITYNAME") & "' AND CITY_CMPID = " & CmpId & " AND CITY_LOCATIONID = " & Locationid & " AND CITY_YEARID = " & YearId)
                End If


                DTTABLE = OBJCMN.search("STATE_ID AS STATEID", "", "STATEMASTER ", "AND STATE_NAME = '" & DTROWSAVE("STATE") & "' AND STATE_YEARID = " & YearId)
                If DTTABLE.Rows.Count = 0 Then
                    'ADD NEW STATE
                    Dim objyearmaster As New ClsYearMaster
                    objyearmaster.savestate(DTROWSAVE("STATE"), CmpId, Locationid, Userid, YearId, " and STATE_name = '" & DTROWSAVE("STATE") & "' AND STATE_YEARID = " & YearId)
                End If


                DTTABLE = OBJCMN.search("COUNTRY_ID AS COUNTRYID", "", "COUNTRYMASTER ", "AND COUNTRY_NAME = '" & DTROWSAVE("COUNTRY") & "' AND COUNTRY_YEARID = " & YearId)
                If DTTABLE.Rows.Count = 0 Then
                    'ADD NEW COUNTRY
                    Dim objyearmaster As New ClsYearMaster
                    objyearmaster.savecountry(DTROWSAVE("COUNTRY"), CmpId, Locationid, Userid, YearId, " and COUNTRY_name = '" & DTROWSAVE("COUNTRY") & "' AND COUNTRY_YEARID = " & YearId)
                End If


                'check whether ITEMNAME is already present or not
                DTTABLE = OBJCMN.search("ACC_CMPNAME AS COMPANYNAME", "", "LEDGERS ", " AND ACC_CMPNAME = '" & DTROWSAVE("COMPANYNAME") & "' AND ACC_YEARID = " & YearId)
                If DTTABLE.Rows.Count > 0 Then GoTo SKIPLINE



                'ADD IN ACCOUNTSMASTER
                ALPARAVAL.Clear()
                Dim OBJSM As New ClsAccountsMaster

                ALPARAVAL.Add(DTROWSAVE("COMPANYNAME"))
                ALPARAVAL.Add("")   'NAME
                ALPARAVAL.Add(DTROWSAVE("GROUPNAME"))
                ALPARAVAL.Add(0)    'OPBAL
                ALPARAVAL.Add("Cr.")
                ALPARAVAL.Add(0)    'INTPER
                ALPARAVAL.Add(0)    'PROFITPER
                ALPARAVAL.Add(DTROWSAVE("ADD1"))
                ALPARAVAL.Add(DTROWSAVE("ADD2"))
                ALPARAVAL.Add("")   'AREA
                ALPARAVAL.Add("")   'STD
                ALPARAVAL.Add(DTROWSAVE("CITYNAME"))
                ALPARAVAL.Add(DTROWSAVE("PINNO"))
                ALPARAVAL.Add(DTROWSAVE("STATE"))
                ALPARAVAL.Add(DTROWSAVE("COUNTRY"))
                ALPARAVAL.Add(Val(DTROWSAVE("CRDAYS")))
                ALPARAVAL.Add(0)    'CRLIMIT
                ALPARAVAL.Add("")   'RESI
                ALPARAVAL.Add("")   'ALT
                ALPARAVAL.Add(DTROWSAVE("PHONENO"))
                ALPARAVAL.Add(DTROWSAVE("MOBILENO"))
                ALPARAVAL.Add("")   'BOSS MOBILE
                ALPARAVAL.Add("")   'FAX
                ALPARAVAL.Add("")   'WEBSITE
                ALPARAVAL.Add(DTROWSAVE("EMAIL"))   'EMAIL

                ALPARAVAL.Add("")   'STREET

                ALPARAVAL.Add(0)   'CHKBANK
                ALPARAVAL.Add("")   'PARTYBANK
                ALPARAVAL.Add("")   'ACCNO
                ALPARAVAL.Add("")   'IFSC
                ALPARAVAL.Add("")   'BRANCH

                ALPARAVAL.Add(0)   'CHKBANK1
                ALPARAVAL.Add("")   'PARTYBANK1
                ALPARAVAL.Add("")   'ACCNO1
                ALPARAVAL.Add("")   'IFSC1
                ALPARAVAL.Add("")   'BRANCH1



                ALPARAVAL.Add(DTROWSAVE("TRANSPORT"))   'TRANS
                ALPARAVAL.Add(DTROWSAVE("BROKER"))   'AGENT
                ALPARAVAL.Add(Val(DTROWSAVE("COMMISSION")))    'AGENTCOM
                ALPARAVAL.Add(Val(DTROWSAVE("DISCOUNT")))    'DISC
                ALPARAVAL.Add(0)    'KMS

                ALPARAVAL.Add(DTROWSAVE("PANNO"))   'PAN
                ALPARAVAL.Add("")   'EXISE
                ALPARAVAL.Add("")   'LABDMAR
                ALPARAVAL.Add("")   'DIVISION
                ALPARAVAL.Add("")   'CST
                ALPARAVAL.Add("")   'TIN
                ALPARAVAL.Add("")   'ST
                ALPARAVAL.Add("")   'DISTRICT
                ALPARAVAL.Add("")   'VIA
                ALPARAVAL.Add("")   'VAT
                ALPARAVAL.Add(DTROWSAVE("GSTIN"))
                ALPARAVAL.Add("")   'REGISTER
                ALPARAVAL.Add(DTROWSAVE("ADDRESS"))
                ALPARAVAL.Add(DTROWSAVE("SHIPPINGADD"))   'SHIPADD
                ALPARAVAL.Add("")   'CALCON
                ALPARAVAL.Add("")   'ADDLESS
                ALPARAVAL.Add("")   'REMARKS
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(Locationid)
                ALPARAVAL.Add(Userid)
                ALPARAVAL.Add(YearId)
                ALPARAVAL.Add(0)    'TRANSFER
                ALPARAVAL.Add(DTROWSAVE("CODE"))
                ALPARAVAL.Add("")    'HINDINAME


                'TDS
                '*******************************
                ALPARAVAL.Add(0)    'ISTDS
                ALPARAVAL.Add("")   'DEDUCTEETYPER
                ALPARAVAL.Add(0)    'ISLOWER

                ALPARAVAL.Add(DTROWSAVE("TDSSECTION"))   'SECTION
                ALPARAVAL.Add("")   'TDSFORM
                ALPARAVAL.Add(Val(0))   'TDSRATE
                ALPARAVAL.Add(Val(DTROWSAVE("TDSPER")))    'TDSPER
                ALPARAVAL.Add(0) 'SURCHARGE
                ALPARAVAL.Add(0) 'LIMIT
                '*******************************

                ALPARAVAL.Add(0)    'TDSAC
                ALPARAVAL.Add(DTROWSAVE("CMPNONCMP"))   'NATUREOFPAY
                ALPARAVAL.Add("ACCOUNTS")   'TYPE
                ALPARAVAL.Add("ACCOUNTS")   'SUBTYPE
                ALPARAVAL.Add("")   'FORMTYPE


                OBJSM.alParaval = ALPARAVAL
                Dim INTRES As Integer = OBJSM.save()

                DTROWSAVE = DTSAVE.NewRow()

SKIPLINE:
            Next

            oBook.Close()

            Exit Sub

            '************************************ END OF CODE FOR LEDGER UPLOAD ****************************



        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNISSUESIZER_TOOL_Click(sender As Object, e As EventArgs) Handles YARNISSUESIZER_TOOL.Click
        Try
            Dim OBJISSUE As New YarnIssue
            OBJISSUE.FRMSTRING = "ISSUETOSIZER"
            OBJISSUE.MdiParent = Me
            OBJISSUE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNISSUEWEAVER_TOOL_Click(sender As Object, e As EventArgs) Handles YARNISSUEWEAVER_TOOL.Click
        Try
            Dim OBJISSUE As New YarnIssue
            OBJISSUE.FRMSTRING = "ISSUETOWEAVER"
            OBJISSUE.MdiParent = Me
            OBJISSUE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNISSUEWARPER_TOOL_Click(sender As Object, e As EventArgs) Handles YARNISSUEWARPER_TOOL.Click
        Try
            Dim OBJISSUE As New YarnIssue
            OBJISSUE.FRMSTRING = "ISSUETOWARPER"
            OBJISSUE.MdiParent = Me
            OBJISSUE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OPSTOCK_WINDINGYARN_Click(sender As Object, e As EventArgs) Handles OPSTOCK_JOBBERYARN.Click
        Try
            Dim OBJCFORM As New OpeningStockYarnwithSizer
            OBJCFORM.FRMSTRING = "YARNJOBBER"
            OBJCFORM.MdiParent = Me
            OBJCFORM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNISSUEWINDING_TOOL_Click(sender As Object, e As EventArgs) Handles YARNISSUEWINDING_TOOL.Click
        Try
            Dim OBJJOBOUT As New JobOut
            OBJJOBOUT.MdiParent = Me
            OBJJOBOUT.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNISSUEADDA_TOOL_Click(sender As Object, e As EventArgs) Handles YARNISSUEADDA_TOOL.Click
        Try
            Dim OBJISSUE As New YarnIssue
            OBJISSUE.FRMSTRING = "ISSUETOADDA"
            OBJISSUE.MdiParent = Me
            OBJISSUE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNISSUEADDAADD_Click(sender As Object, e As EventArgs) Handles YARNISSUEADDAADD.Click
        Try
            Dim OBJISSUE As New YarnIssue
            OBJISSUE.FRMSTRING = "ISSUETOADDA"
            OBJISSUE.MdiParent = Me
            OBJISSUE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNISSUEADDAEDIT_Click(sender As Object, e As EventArgs) Handles YARNISSUEADDAEDIT.Click
        Try
            Dim OBJISSUE As New YarnIssueDetails
            OBJISSUE.FRMSTRING = "ISSUETOADDA"
            OBJISSUE.MdiParent = Me
            OBJISSUE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNRETURNADDAADD_Click(sender As Object, e As EventArgs) Handles YARNRETURNADDAADD.Click
        Try
            Dim OBJRETURNWARPER As New YarnReturn
            OBJRETURNWARPER.FRMSTRING = "RETURNFROMADDA"
            OBJRETURNWARPER.MdiParent = Me
            OBJRETURNWARPER.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YARNRETURNADDAEDIT_Click(sender As Object, e As EventArgs) Handles YARNRETURNADDAEDIT.Click
        Try
            Dim OBJISSUE As New YarnReturnDetails
            OBJISSUE.FRMSTRING = "RETURNFROMADDA"
            OBJISSUE.MdiParent = Me
            OBJISSUE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TERMSANDCONDITIONS_Click(sender As Object, e As EventArgs) Handles TERMSANDCONDITIONS.Click
        Try
            Dim OBJOPPO As New TermsAndConditions
            OBJOPPO.MdiParent = Me
            OBJOPPO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UPLOADSIGN_Click(sender As Object, e As EventArgs) Handles UPLOADSIGN.Click
        Try
            Dim OBJOPPO As New UploadSign
            OBJOPPO.MdiParent = Me
            OBJOPPO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BEAMUPLOADADD_Click(sender As Object, e As EventArgs) Handles BEAMUPLOADADD.Click
        Try
            Dim OBJBEAMUPLOAD As New BeamUpload
            OBJBEAMUPLOAD.MdiParent = Me
            OBJBEAMUPLOAD.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BEAMUPLOADEDIT_Click(sender As Object, e As EventArgs) Handles BEAMUPLOADEDIT.Click
        Try
            Dim OBJBEAMUPLOAD As New BeamUploadDetails
            OBJBEAMUPLOAD.MdiParent = Me
            OBJBEAMUPLOAD.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PIECETYPEADD_Click(sender As Object, e As EventArgs) Handles PIECETYPEADD.Click
        Try
            Dim objCategory As New CategoryMaster
            objCategory.frmString = "PIECE TYPE"
            objCategory.MdiParent = Me
            objCategory.Show()
            objCategory.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub PIECETYPEEDIT_Click(sender As Object, e As EventArgs) Handles PIECETYPEEDIT.Click
        Try
            Dim objCategoryDetails As New CategoryDetails
            objCategoryDetails.MdiParent = Me
            objCategoryDetails.frmstring = "PIECE TYPE"
            objCategoryDetails.Show()
            objCategoryDetails.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub LiftingDetailsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LiftingDetailsToolStripMenuItem.Click
        Try
            Dim OBJLIFT As New LiftingDetails
            OBJLIFT.MdiParent = Me
            OBJLIFT.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PendingLotNoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PendingLotNoToolStripMenuItem.Click
        Try
            Dim OBJLOT As New PendingLotno
            OBJLOT.MdiParent = Me
            OBJLOT.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PendingReturnTakaDateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PendingReturnTakaDateToolStripMenuItem.Click
        Try
            Dim OBJRDATE As New PendingReturnDate
            OBJRDATE.MdiParent = Me
            OBJRDATE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PendingReturnDONoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PendingReturnDONoToolStripMenuItem.Click
        Try
            Dim OBJRDONO As New PendingReturnDONo
            OBJRDONO.MdiParent = Me
            OBJRDONO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LockPendingBeamsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LockPendingBeamsToolStripMenuItem.Click
        Try
            Dim OBJBEAMS As New LockPendingBeams
            OBJBEAMS.MdiParent = Me
            OBJBEAMS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RECODATA_MASTER_Click(sender As Object, e As EventArgs) Handles RECODATA_MASTER.Click
        Try
            Dim OBJRECO As New ReconcileData
            OBJRECO.MdiParent = Me
            OBJRECO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Try
            Dim OBJGREY As New OpeningBankReco
            OBJGREY.MdiParent = Me
            OBJGREY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub WhatsappRegistrationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WhatsappRegistrationToolStripMenuItem.Click
        Try
            Dim OBJRECO As New WhatsappRegistration
            OBJRECO.MdiParent = Me
            OBJRECO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
