
Imports BL
Imports System.IO
Imports System.ComponentModel

Public Class GreyQualityMaster

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public FRMSTRING As String
    Public EDIT As Boolean
    Public TEMPQUALITYNAME As String
    Public TEMPQUALITYCODE As String
    Public TEMPQUALITYID As Integer

    Dim GRIDDOUBLECLICK, GRIDWEFTDOUBLECLICK, GRIDBEAMDOUBLECLICK As Boolean
    Dim TEMPROW, TEMPWEFTROW, TEMPBEAMROW As Integer
    Dim DT_WEFTDETAILS As New DataTable

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub GreyQualiyMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Sub clear()
        Try
            TXTGREYNAME.Clear()
            TXTCODE.Clear()
            TXTRATE.Clear()
            TXTHSNCODE.Clear()
            CMBGREYQUALITY.Text = ""
            CMBBEAMNAME.Text = ""
            TXTENDS.Clear()
            TXTTL.Clear()
            TXTBEAMWT.Clear()
            CMBQUALITY.Text = ""
            TXTWTMTRS.Clear()
            TXTREEDSPACE.Clear()
            TXTREED.Clear()
            TXTMTRS.Clear()
            TXTQUALITYWT.Clear()
            TXTREMARKS.Clear()
            TXTPHOTOIMGPATH.Clear()
            PBPHOTO.Image = Nothing

            GRIDWEFTCHANGE.RowCount = 0
            GRIDWEFT.RowCount = 0
            GRIDBEAM.RowCount = 0
            TXTWEFTSRNO.Text = GRIDWEFTCHANGE.RowCount + 1
            CMBWEFTCHANGE.Text = ""
            TXTSRNO.Text = GRIDWEFT.RowCount + 1
            CMBGRIDQUALITY.Text = ""
            CMBSHADE.Text = ""
            TXTPICK.Clear()
            TXTGRIDWT.Clear()
            TXTTOTALWT.Clear()


            DT_WEFTDETAILS.Reset()
            DT_WEFTDETAILS.Columns.Add("SRNO")
            DT_WEFTDETAILS.Columns.Add("GRIDQUALITY")
            DT_WEFTDETAILS.Columns.Add("SHADE")
            DT_WEFTDETAILS.Columns.Add("PICK")
            DT_WEFTDETAILS.Columns.Add("GRIDWT")
            DT_WEFTDETAILS.Columns.Add("WEFTSRNO")

            TXTBEAMSRNO.Text = GRIDBEAM.RowCount + 1
            CMBGRIDBEAM.Text = ""
            TXTGRIDENDS.Clear()
            TXTGRIDTL.Clear()
            TXTGRIDBEAMWT.Clear()
            TXTTOTALGRIDBEAMENDS.Clear()
            TXTTOTALGRIDBEAMWT.Clear()


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        If CMBQUALITY.Text = "" Then fillQUALITY(CMBQUALITY, EDIT)
        If CMBGRIDQUALITY.Text = "" Then fillQUALITY(CMBGRIDQUALITY, EDIT)
        If CMBWEFTCHANGE.Text = "" Then FILLCOLOR(CMBWEFTCHANGE)
        If CMBSHADE.Text = "" Then FILLCOLOR(CMBSHADE)

        If CMBGREYQUALITY.Text = "" Then FILLGREY(CMBGREYQUALITY, EDIT)
        If CMBBEAMNAME.Text = "" Then fillBEAM(CMBBEAMNAME, EDIT)
        If CMBGRIDBEAM.Text = "" Then fillBEAM(CMBGRIDBEAM, EDIT)

    End Sub

    Sub TOTAL()
        Try
            TXTQUALITYWT.Text = 0.0
            TXTTOTALWT.Text = 0.0
            TXTTOTALGRIDBEAMWT.Text = 0.0
            TXTTOTALGRIDBEAMENDS.Text = 0.0


            For Each ROW As DataGridViewRow In GRIDWEFT.Rows
                TXTTOTALWT.Text = Format(Val(TXTTOTALWT.Text.Trim) + Val(ROW.Cells(GWTPER.Index).Value), "0.000")
            Next
            For Each ROW As DataGridViewRow In GRIDBEAM.Rows
                TXTTOTALGRIDBEAMENDS.Text = Format(Val(TXTTOTALGRIDBEAMENDS.Text.Trim) + Val(ROW.Cells(BENDS.Index).Value), "0.000")
                TXTTOTALGRIDBEAMWT.Text = Format(Val(TXTTOTALGRIDBEAMWT.Text.Trim) + Val(ROW.Cells(BBEAMWT.Index).Value), "0.000")
            Next
            TXTQUALITYWT.Text = Format(Val(TXTTOTALGRIDBEAMWT.Text) + Val(TXTWTMTRS.Text), "0.000")


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GreyQualiyMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'GREYQUALITY MASTER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            FILLCMB()
            clear()
            TXTGREYNAME.Text = TEMPQUALITYNAME
            CMBTYPE.SelectedIndex = 0

            If MULTIYARN = True Then
                LBLWEFTQUALITY.Visible = False
                CMBQUALITY.Visible = False
                LBLWTMTRS.Visible = False
                TXTWTMTRS.Visible = False
                GPGRID.Visible = True
            End If

            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim objCommon As New ClsCommonMaster
                Dim dttable As DataTable = objCommon.search("  ISNULL(GREYQUALITYMASTER.GREY_ID, 0) AS GREYID,ISNULL(GREYQUALITYMASTER.GREY_TYPE, 'GREY') AS [TYPE], ISNULL(GREYQUALITYMASTER.GREY_NAME, '') AS GREYNAME, ISNULL(GREYQUALITYMASTER.GREY_CODE, '') AS GREYCODE, ISNULL(GREYQUALITYMASTER.GREY_RATE, 0) AS RATE, ISNULL(EFFECTIVEQUALITY.GREY_NAME, '') AS EFFECTIVEQUALITY, ISNULL(BEAMMASTER.BEAM_NAME, '') AS BEAMNAME, ISNULL(QUALITYMASTER.QUALITY_NAME, '') AS QUALITY, ISNULL(GREYQUALITYMASTER.GREY_WTMTRS, 0) AS WTMTRS, ISNULL(GREYQUALITYMASTER.GREY_REEDSPACE, 0) AS REEDSPACE, ISNULL(GREYQUALITYMASTER.GREY_REED, 0) AS REED, ISNULL(GREYQUALITYMASTER.GREY_MTRS, 0) AS MTRS, ISNULL(GREYQUALITYMASTER.GREY_QUALITYWT, 0) AS QUALITYWT, ISNULL(GREYQUALITYMASTER.GREY_REMARKS, '') AS REMARKS, GREYQUALITYMASTER.GREY_IMAGE AS IMGPATH, ISNULL(BEAMMASTER.BEAM_ENDS, 0) AS ENDS, ISNULL(BEAMMASTER.BEAM_TAPLINE, 0) AS TAPLINE, ISNULL(BEAMMASTER.BEAM_WTMTRS, 0) AS BEAMWTMTRS, ISNULL(BEAMMASTER.BEAM_TOTALENDS, 0) AS TOTALENDS, ISNULL(BEAMMASTER.BEAM_TOTALWT, 0) AS TOTALWT, ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(GREYQUALITYMASTER_WEFTCHANGE.GREY_SRNO, 0) AS SRNO, ISNULL(COLORMASTER.COLOR_NAME, '') AS WEFTCHANGE ", "", " COLORMASTER RIGHT OUTER JOIN GREYQUALITYMASTER_WEFTCHANGE ON COLORMASTER.COLOR_ID = GREYQUALITYMASTER_WEFTCHANGE.GREY_WEFTCHANGEID RIGHT OUTER JOIN GREYQUALITYMASTER ON GREYQUALITYMASTER_WEFTCHANGE.GREY_ID = GREYQUALITYMASTER.GREY_ID LEFT OUTER JOIN QUALITYMASTER ON GREYQUALITYMASTER.GREY_WEFTQUALITYID = QUALITYMASTER.QUALITY_ID LEFT OUTER JOIN GREYQUALITYMASTER AS EFFECTIVEQUALITY ON GREYQUALITYMASTER.GREY_EFFECTQUALITYID = EFFECTIVEQUALITY.GREY_ID AND GREYQUALITYMASTER.GREY_YEARID = EFFECTIVEQUALITY.GREY_YEARID LEFT OUTER JOIN BEAMMASTER ON GREYQUALITYMASTER.GREY_BEAMID = BEAMMASTER.BEAM_ID LEFT OUTER JOIN HSNMASTER ON GREYQUALITYMASTER.GREY_HSNCODEID = HSNMASTER.HSN_ID ", " and GREYQUALITYMASTER.GREY_ID = '" & TEMPQUALITYID & "' and GREYQUALITYMASTER.GREY_yearid = " & YearId)
                If dttable.Rows.Count > 0 Then
                    For Each ROW As DataRow In dttable.Rows

                        TEMPQUALITYID = ROW("GREYID")
                        TEMPQUALITYNAME = ROW("GREYNAME")
                        TEMPQUALITYCODE = ROW("GREYCODE")

                        CMBTYPE.Text = ROW("TYPE")
                        CMBTYPE.Enabled = False


                        TXTGREYNAME.Text = ROW("GREYNAME")
                        TXTCODE.Text = ROW("GREYCODE")
                        TXTRATE.Text = Val(ROW("RATE"))
                        TXTHSNCODE.Text = ROW("HSNCODE")
                        CMBGREYQUALITY.Text = ROW("EFFECTIVEQUALITY")
                        CMBBEAMNAME.Text = ROW("BEAMNAME")
                        TXTTL.Text = ROW("TAPLINE")

                        If MULTIYARN = True Then
                            TXTENDS.Text = ROW("TOTALENDS")
                            TXTBEAMWT.Text = ROW("TOTALWT")
                        Else
                            TXTENDS.Text = ROW("ENDS")
                            TXTBEAMWT.Text = ROW("BEAMWTMTRS")
                        End If



                        CMBQUALITY.Text = ROW("QUALITY")
                        TXTWTMTRS.Text = ROW("WTMTRS")
                        TXTREEDSPACE.Text = ROW("REEDSPACE")
                        TXTREED.Text = ROW("REED")
                        TXTMTRS.Text = ROW("MTRS")
                        TXTQUALITYWT.Text = ROW("QUALITYWT")
                        TXTREMARKS.Text = ROW("REMARKS")

                        If IsDBNull(dttable.Rows(0).Item("IMGPATH")) = False Then
                            PBPHOTO.Image = Image.FromStream(New IO.MemoryStream(DirectCast(dttable.Rows(0).Item("IMGPATH"), Byte())))
                            TXTPHOTOIMGPATH.Text = dttable.Rows(0).Item("IMGPATH").ToString
                        Else
                            PBPHOTO.Image = Nothing
                        End If

                        If ROW("WEFTCHANGE") <> "" Then GRIDWEFTCHANGE.Rows.Add(Val(ROW("SRNO")), ROW("WEFTCHANGE"))

                    Next
                End If

                Dim OBJCMN As New ClsCommon
                dttable = OBJCMN.search(" ISNULL(GREYQUALITYMASTER_QUALITYDESC.GREY_SRNO, 0) AS SRNO, ISNULL(QUALITYMASTER.QUALITY_NAME, '') AS GRIDQUALITY, ISNULL(COLORMASTER.COLOR_NAME, '') AS SHADE,  ISNULL(GREYQUALITYMASTER_QUALITYDESC.GREY_PICK, 0) AS PICK, ISNULL(GREYQUALITYMASTER_QUALITYDESC.GREY_GRIDWT, 0) AS GRIDWT, ISNULL(GREYQUALITYMASTER_QUALITYDESC.GREY_WEFTSRNO, 0) AS WEFTSRNO ", "", " GREYQUALITYMASTER_QUALITYDESC INNER JOIN QUALITYMASTER ON GREYQUALITYMASTER_QUALITYDESC.GREY_GRIDQUALITYID = QUALITYMASTER.QUALITY_ID LEFT OUTER JOIN COLORMASTER ON GREYQUALITYMASTER_QUALITYDESC.GREY_SHADEID = COLORMASTER.COLOR_ID ", " AND GREYQUALITYMASTER_QUALITYDESC.GREY_ID = " & TEMPQUALITYID & " AND GREYQUALITYMASTER_QUALITYDESC.GREY_YEARID = " & YearId & " ORDER BY SRNO, WEFTSRNO")
                If dttable.Rows.Count > 0 Then
                    For Each DR As DataRow In dttable.Rows
                        DT_WEFTDETAILS.Rows.Add(Val(DR("SRNO")), DR("GRIDQUALITY"), DR("SHADE"), Format(DR("PICK"), "0.00"), Format(DR("GRIDWT"), "0.000"), Val(DR("WEFTSRNO")))
                    Next
                End If

                dttable = OBJCMN.search("ISNULL(GREYQUALITYMASTER_BEAMDETAILS.GREY_BEAMSRNO, '') AS BEAMSRNO, ISNULL(BEAMMASTER.BEAM_NAME, '') AS GRIDBEAMNAME, ISNULL(BEAMMASTER.BEAM_TOTALENDS, 0) AS BENDS, ISNULL(BEAMMASTER.BEAM_TAPLINE, 0) AS BTL, ISNULL(BEAMMASTER.BEAM_TOTALWT, 0) AS BEAMWT", "", " GREYQUALITYMASTER_BEAMDETAILS RIGHT OUTER JOIN BEAMMASTER ON GREYQUALITYMASTER_BEAMDETAILS.GREY_BEAMID = BEAMMASTER.BEAM_ID ", " AND GREYQUALITYMASTER_BEAMDETAILS.GREY_ID = " & TEMPQUALITYID & " AND GREYQUALITYMASTER_BEAMDETAILS.GREY_YEARID = " & YearId & " ORDER BY BEAMSRNO")
                If dttable.Rows.Count > 0 Then
                    For Each DR As DataRow In dttable.Rows
                        GRIDBEAM.Rows.Add(Val(DR("BEAMSRNO")), DR("GRIDBEAMNAME"), DR("BENDS"), DR("BTL"), DR("BEAMWT"))
                    Next
                End If
            End If
            TOTAL()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTGREYNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTGREYNAME.Validating
        Try
            If TXTGREYNAME.Text.Trim <> "" Then
                uppercase(TXTGREYNAME)
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                If (EDIT = False) Or (EDIT = True And LCase(TXTGREYNAME.Text) <> LCase(TEMPQUALITYNAME)) Then
                    dt = OBJCMN.search("GREY_NAME", "", "GREYQUALITYMASTER", " and GREY_NAME = '" & TXTGREYNAME.Text.Trim & "' And GREY_yearid = " & YearId)
                    If dt.Rows.Count > 0 Then
                        MsgBox("Grey Quality Already Exists", MsgBoxStyle.Critical, "PROCESS")
                        e.Cancel = True
                    End If
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True
        If TXTGREYNAME.Text.Trim.Length = 0 Then
            EP.SetError(TXTGREYNAME, "Fill Grey Name")
            bln = False
        End If

        If TXTHSNCODE.Text.Trim.Length = 0 Then
            EP.SetError(TXTHSNCODE, "Fill HSN Code")
            bln = False
        End If

        'IF TYPE = FINISHED OR TYPE = SAREE
        If CMBTYPE.Text.Trim <> "GREY" Then
            CMBGREYQUALITY.Text = TXTGREYNAME.Text.Trim
        End If

        'THIS IS NOT MANDATE AS PER NIMESH BHAI, COZ WHEN WE PURCHASE GREY DIRECTLY FROM OUTSIDE WE DONT KNOW THESE DETAISL
        'If CMBTYPE.Text.Trim = "GREY" Then
        '    If CMBBEAMNAME.Text.Trim.Length = 0 Then
        '        EP.SetError(CMBBEAMNAME, "Fill Beam Name")
        '        bln = False
        '    End If

        '    If CMBQUALITY.Text.Trim.Length = 0 Then
        '        EP.SetError(CMBQUALITY, "Fill Yarn Quality Name")
        '        bln = False
        '    End If

        '    If TXTWTMTRS.Text.Trim.Length = 0 Then
        '        EP.SetError(TXTWTMTRS, "Fill Wt./100 Mtrs.")
        '        bln = False
        '    End If

        '    If TXTMTRS.Text.Trim.Length = 0 Then
        '        EP.SetError(TXTMTRS, "Fill Approx. Mtrs.")
        '        bln = False
        '    End If
        'End If


        'THIS IS NOT MANDATE COZ WHEN WE PURCHASE GREY DIRECTLY FROM OUTSIDE WE DONT KNOW THESE DETAISL
        'If MULTIYARN = True And CMBTYPE.Text = "GREY" Then
        '    If GRIDWEFTCHANGE.RowCount = 0 Then
        '        EP.SetError(TXTGREYNAME, "Enter Weft Change Details")
        '        bln = False
        '    End If
        '    If GRIDWEFT.RowCount = 0 Then
        '        EP.SetError(TXTGREYNAME, "Enter Weft Details")
        '        bln = False
        '    End If
        'End If

        Return bln
    End Function

    Private Sub CMDPHOTOUPLOAD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDPHOTOUPLOAD.Click
        OpenFileDialog1.Filter = "Pictures (*.bmp;*.jpeg;*.png)|*.bmp;*.jpg;*.png"
        OpenFileDialog1.ShowDialog()
        TXTPHOTOIMGPATH.Text = OpenFileDialog1.FileName
        On Error Resume Next
        If TXTPHOTOIMGPATH.Text.Trim.Length <> 0 Then PBPHOTO.ImageLocation = TXTPHOTOIMGPATH.Text.Trim
    End Sub

    Private Sub CMDPHOTOREMOVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDPHOTOREMOVE.Click
        Try
            PBPHOTO.Image = Nothing
            TXTPHOTOIMGPATH.Clear()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDPHOTOVIEW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDPHOTOVIEW.Click
        Try
            If TXTPHOTOIMGPATH.Text.Trim <> "" Then
                If Path.GetExtension(TXTPHOTOIMGPATH.Text.Trim) = ".pdf" Then
                    System.Diagnostics.Process.Start(TXTPHOTOIMGPATH.Text.Trim)
                Else
                    Dim objVIEW As New ViewImage
                    objVIEW.pbsoftcopy.Image = PBPHOTO.Image
                    objVIEW.ShowDialog()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try

            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim IntResult As Integer
            Dim alParaval As New ArrayList

            alParaval.Add(CMBTYPE.Text.Trim)
            alParaval.Add(TXTGREYNAME.Text.Trim)
            alParaval.Add(TXTCODE.Text.Trim)
            alParaval.Add(Val(TXTRATE.Text.Trim))
            alParaval.Add(TXTHSNCODE.Text.Trim)

            If CMBGREYQUALITY.Text.Trim = "" Then
                alParaval.Add(TXTGREYNAME.Text.Trim)
            Else
                alParaval.Add(CMBGREYQUALITY.Text.Trim)
            End If

            alParaval.Add(CMBBEAMNAME.Text.Trim)
            alParaval.Add(CMBQUALITY.Text.Trim)
            alParaval.Add(Format(Val(TXTWTMTRS.Text.Trim), "0.000"))
            alParaval.Add(Format(Val(TXTREEDSPACE.Text.Trim), "0"))
            alParaval.Add(TXTREED.Text.Trim)
            alParaval.Add(Format(Val(TXTMTRS.Text.Trim), "0.00"))
            alParaval.Add(Format(Val(TXTQUALITYWT.Text.Trim), "0.000"))
            alParaval.Add(TXTREMARKS.Text.Trim)

            If PBPHOTO.Image IsNot Nothing Then
                Dim MS As New IO.MemoryStream
                PBPHOTO.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                alParaval.Add(MS.ToArray)
            Else
                alParaval.Add(DBNull.Value)
            End If

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)


            Dim WEFTSRNO As String = ""
            Dim WEFTCHANGE As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDWEFTCHANGE.Rows
                If row.Cells(0).Value <> Nothing Then
                    If WEFTSRNO = "" Then
                        WEFTSRNO = Val(row.Cells(ESRNO.Index).Value)
                        WEFTCHANGE = row.Cells(EWEFTCHANGE.Index).Value.ToString
                    Else
                        WEFTSRNO = WEFTSRNO & "|" & Val(row.Cells(ESRNO.Index).Value)
                        WEFTCHANGE = WEFTCHANGE & "|" & row.Cells(EWEFTCHANGE.Index).Value.ToString
                    End If
                End If
            Next



            alParaval.Add(WEFTSRNO)
            alParaval.Add(WEFTCHANGE)


            Dim SRNO As String = ""
            Dim GRIDYARNQUALITY As String = ""
            Dim SHADE As String = ""
            Dim PICK As String = ""
            Dim GRIDWT As String = ""
            Dim WEFTGRIDNO As String = ""

            For i As Integer = 0 To DT_WEFTDETAILS.Rows.Count - 1
                If DT_WEFTDETAILS.Rows(i).Item(0) <> Nothing Then
                    If SRNO = "" Then
                        SRNO = Val(DT_WEFTDETAILS.Rows(i).Item("SRNO"))
                        GRIDYARNQUALITY = DT_WEFTDETAILS.Rows(i).Item("GRIDQUALITY")
                        SHADE = DT_WEFTDETAILS.Rows(i).Item("SHADE")
                        PICK = Val(DT_WEFTDETAILS.Rows(i).Item("PICK"))
                        GRIDWT = Val(DT_WEFTDETAILS.Rows(i).Item("GRIDWT"))
                        WEFTGRIDNO = Val(DT_WEFTDETAILS.Rows(i).Item("WEFTSRNO"))
                    Else
                        SRNO = SRNO & "|" & Val(DT_WEFTDETAILS.Rows(i).Item("SRNO"))
                        GRIDYARNQUALITY = GRIDYARNQUALITY & "|" & DT_WEFTDETAILS.Rows(i).Item("GRIDQUALITY")
                        SHADE = SHADE & "|" & DT_WEFTDETAILS.Rows(i).Item("SHADE")
                        PICK = PICK & "|" & Val(DT_WEFTDETAILS.Rows(i).Item("PICK"))
                        GRIDWT = GRIDWT & "|" & Val(DT_WEFTDETAILS.Rows(i).Item("GRIDWT"))
                        WEFTGRIDNO = WEFTGRIDNO & "|" & Val(DT_WEFTDETAILS.Rows(i).Item("WEFTSRNO"))
                    End If
                End If
            Next

            Dim BEAMSRNO As String = ""
            Dim GRIDBEAMNAME As String = ""
            For Each row As Windows.Forms.DataGridViewRow In GRIDBEAM.Rows
                If row.Cells(0).Value <> Nothing Then
                    If BEAMSRNO = "" Then
                        BEAMSRNO = Val(row.Cells(BSRNO.Index).Value)
                        GRIDBEAMNAME = row.Cells(BBEAMNAME.Index).Value.ToString
                    Else
                        BEAMSRNO = BEAMSRNO & "|" & Val(row.Cells(BSRNO.Index).Value)
                        GRIDBEAMNAME = GRIDBEAMNAME & "|" & row.Cells(BBEAMNAME.Index).Value.ToString
                    End If
                End If
            Next

            alParaval.Add(SRNO)
            alParaval.Add(GRIDYARNQUALITY)
            alParaval.Add(SHADE)
            alParaval.Add(PICK)
            alParaval.Add(GRIDWT)
            alParaval.Add(WEFTGRIDNO)

            alParaval.Add(BEAMSRNO)
            alParaval.Add(GRIDBEAMNAME)
            alParaval.Add(TXTTOTALGRIDBEAMENDS.Text.Trim)
            alParaval.Add(TXTTOTALGRIDBEAMWT.Text.Trim)


            Dim objclsGreyQualityMaster As New ClsGreyQualityMaster
            objclsGreyQualityMaster.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                IntResult = objclsGreyQualityMaster.SAVE()
                MsgBox("Details Added")
            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPQUALITYID)
                IntResult = objclsGreyQualityMaster.UPDATE()
                MsgBox("Details Updated")

            End If
            EDIT = False

            clear()
            TXTGREYNAME.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBGREYQUALITY_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBGREYQUALITY.Enter
        Try
            If CMBGREYQUALITY.Text.Trim = "" Then FILLGREY(CMBGREYQUALITY, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGREYQUALITY_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBGREYQUALITY.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJGODOWN As New SelectGreyQuality
                OBJGODOWN.ShowDialog()
                If OBJGODOWN.TEMPNAME <> "" Then CMBGREYQUALITY.Text = OBJGODOWN.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGREYQUALITY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBGREYQUALITY.Validating
        Try
            If CMBGREYQUALITY.Text.Trim <> "" Then GREYVALIDATE(CMBGREYQUALITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBEAMNAME_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBBEAMNAME.Enter
        Try
            If CMBBEAMNAME.Text.Trim = "" Then fillBEAM(CMBBEAMNAME, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBEAMNAME_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBBEAMNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJBEAM As New SelectBeam
                OBJBEAM.ShowDialog()
                If OBJBEAM.TEMPNAME <> "" Then CMBBEAMNAME.Text = OBJBEAM.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBEAMNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBBEAMNAME.Validating
        Try
            If CMBBEAMNAME.Text.Trim <> "" Then BEAMVALIDATE(CMBBEAMNAME, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBEAMNAME_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBBEAMNAME.Validated
        Try
            If CMBBEAMNAME.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("ISNULL(BEAM_ENDS, 0) As ENDS, ISNULL(BEAM_TAPLINE, 0) As TAPLINE, ISNULL(BEAM_WTMTRS, 0) As BEAMWT, ISNULL(BEAM_TOTALENDS, 0) As TOTALENDS, ISNULL(BEAM_TOTALWT, 0) AS TOTALWT", "", "BEAMMASTER", "And BEAMMASTER.BEAM_NAME = '" & CMBBEAMNAME.Text.Trim & "' AND BEAM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TXTTL.Text = DT.Rows(0).Item("TAPLINE")
                    If MULTIYARN = True Then
                        TXTENDS.Text = Val(DT.Rows(0).Item("TOTALENDS"))
                        TXTBEAMWT.Text = Val(DT.Rows(0).Item("TOTALWT"))
                    Else
                        TXTENDS.Text = Val(DT.Rows(0).Item("ENDS"))
                        TXTBEAMWT.Text = Val(DT.Rows(0).Item("BEAMWT"))
                    End If
                End If
                TOTAL()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBQUALITY.Enter
        Try
            If CMBQUALITY.Text.Trim = "" Then fillQUALITY(CMBQUALITY, EDIT)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBQUALITY.Validating
        Try
            If CMBQUALITY.Text.Trim <> "" Then QUALITYVALIDATE(CMBQUALITY, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBQUALITY.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.F1 Then
                Dim OBJQUALITY As New SelectQuality
                OBJQUALITY.FRMSTRING = "QUALITY"
                OBJQUALITY.ShowDialog()
                If OBJQUALITY.TEMPNAME <> "" Then CMBQUALITY.Text = OBJQUALITY.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWEFTCHANGE_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBWEFTCHANGE.Enter
        Try
            If CMBWEFTCHANGE.Text.Trim = "" Then FILLCOLOR(CMBWEFTCHANGE)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBWEFTCHANGE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBWEFTCHANGE.Validating
        Try
            If CMBWEFTCHANGE.Text.Trim <> "" Then COLORVALIDATE(CMBWEFTCHANGE, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBGRIDQUALITY_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBGRIDQUALITY.Enter
        Try
            If CMBGRIDQUALITY.Text.Trim = "" Then fillQUALITY(CMBGRIDQUALITY, EDIT)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBGRIDQUALITY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBGRIDQUALITY.Validating
        Try
            If CMBGRIDQUALITY.Text.Trim <> "" Then QUALITYVALIDATE(CMBGRIDQUALITY, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBGRIDQUALITY_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBGRIDQUALITY.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.F1 Then
                Dim OBJQUALITY As New SelectQuality
                OBJQUALITY.FRMSTRING = "QUALITY"
                OBJQUALITY.ShowDialog()
                If OBJQUALITY.TEMPNAME <> "" Then CMBGRIDQUALITY.Text = OBJQUALITY.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTWTMTRS_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTWTMTRS.Validating, TXTBEAMWT.Validating
        TOTAL()
    End Sub

    Private Sub CMDCLEAR_Click(sender As Object, e As EventArgs) Handles CMDCLEAR.Click
        Try
            clear()
            EDIT = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTMTRS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTMTRS.KeyPress, TXTWTMTRS.KeyPress, TXTPICK.KeyPress, TXTRATE.KeyPress, TXTGRIDWT.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub CMBTYPE_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBTYPE.Validated
        Try
            'If CMBTYPE.Text.Trim = "GREY" Then
            '    GPDETAILS.Visible = True
            '    GPDETAILS.Enabled = True
            '    If MULTIYARN = True Then
            '        GPQUALITY.Visible = False
            '        GPGRID.Visible = True
            '    End If
            'Else
            '    GPDETAILS.Visible = False
            '    GPDETAILS.Enabled = False
            '    GPGRID.Visible = False
            'End If
            CMBTYPE.Enabled = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Try
            If USERDELETE = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If EDIT = False Then Exit Sub
            If MsgBox("Delete Grey Quality?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            Dim OBJGREY As New ClsGreyQualityMaster
            OBJGREY.alParaval.Add(TEMPQUALITYID)
            OBJGREY.alParaval.Add(YearId)

            Dim DT As DataTable = OBJGREY.DELETE
            If DT.Rows.Count > 0 Then
                MsgBox(DT.Rows(0).Item(0))
                clear()
                EDIT = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTHSNCODE_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TXTHSNCODE.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectHSN
                OBJLEDGER.STRSEARCH = " AND HSN_TYPE='GOODS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then TXTHSNCODE.Text = OBJLEDGER.TEMPCODE
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWEFTCHANGE_Validated(sender As Object, e As EventArgs) Handles CMBWEFTCHANGE.Validated
        Try
            If CMBWEFTCHANGE.Text.Trim <> "" Then
                If Not CHECKWEFTCHANGE() Then
                    MsgBox("Matching already Present in Grid below ")
                    Exit Sub
                End If

                FILLGRID()
                CMBWEFTCHANGE.Text = ""
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function CHECKWEFTCHANGE() As Boolean
        Try
            Dim bln As Boolean = True
            For Each ROW As DataGridViewRow In GRIDWEFTCHANGE.Rows
                If (GRIDDOUBLECLICK = True And TEMPROW <> ROW.Index) Or GRIDDOUBLECLICK = False Then
                    If CMBWEFTCHANGE.Text.Trim = ROW.Cells(EWEFTCHANGE.Index).Value Then bln = False
                End If
            Next
            Return bln
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Sub FILLGRID()
        If GRIDDOUBLECLICK = False Then
            GRIDWEFTCHANGE.Rows.Add(TXTWEFTSRNO.Text, CMBWEFTCHANGE.Text.Trim)
            getsrno(GRIDWEFTCHANGE)

        ElseIf GRIDDOUBLECLICK = True Then
            GRIDWEFTCHANGE.Item(ESRNO.Index, TEMPROW).Value = TXTSRNO.Text
            GRIDWEFTCHANGE.Item(EWEFTCHANGE.Index, TEMPROW).Value = CMBWEFTCHANGE.Text.Trim
            GRIDDOUBLECLICK = False
        End If
        GRIDWEFTCHANGE.FirstDisplayedScrollingRowIndex = GRIDWEFTCHANGE.RowCount - 1

        GRIDWEFTCHANGE.Rows(GRIDWEFTCHANGE.RowCount - 1).Selected = True
        GRIDWEFTCHANGE.CurrentCell = GRIDWEFTCHANGE.Item(0, GRIDWEFTCHANGE.RowCount - 1)
        TXTWEFTSRNO.Text = GRIDWEFTCHANGE.RowCount + 1

        GRIDWEFT.RowCount = 0
        TXTSRNO.Text = GRIDWEFT.RowCount + 1

        CMBWEFTCHANGE.Text = ""
        CMBGRIDQUALITY.Focus()

    End Sub

    Sub FILLGRIDBEAM()
        If GRIDBEAMDOUBLECLICK = False Then
            GRIDBEAM.Rows.Add(TXTBEAMSRNO.Text, CMBGRIDBEAM.Text.Trim, Val(TXTGRIDENDS.Text.Trim), TXTGRIDTL.Text.Trim, Format(Val(TXTGRIDBEAMWT.Text.Trim), "0.000"))
            getsrno(GRIDBEAM)

        ElseIf GRIDBEAMDOUBLECLICK = True Then
            GRIDBEAM.Item(BSRNO.Index, TEMPBEAMROW).Value = TXTBEAMSRNO.Text.Trim
            GRIDBEAM.Item(BBEAMNAME.Index, TEMPBEAMROW).Value = CMBGRIDBEAM.Text.Trim
            GRIDBEAM.Item(BENDS.Index, TEMPBEAMROW).Value = Val(TXTGRIDENDS.Text.Trim)
            GRIDBEAM.Item(BTL.Index, TEMPBEAMROW).Value = TXTGRIDTL.Text.Trim
            GRIDBEAM.Item(BBEAMWT.Index, TEMPBEAMROW).Value = Format(Val(TXTGRIDBEAMWT.Text.Trim), "0.000")

            GRIDBEAMDOUBLECLICK = False

        End If
        TOTAL()
        GRIDBEAM.FirstDisplayedScrollingRowIndex = GRIDBEAM.RowCount - 1


        TXTBEAMSRNO.Text = GRIDBEAM.RowCount + 1
        CMBGRIDBEAM.Text = ""
        TXTGRIDENDS.Clear()
        TXTGRIDTL.Clear()
        TXTGRIDBEAMWT.Clear()
        CMBGRIDBEAM.Focus()

    End Sub

    Sub FILLWEFTGRID()
        If GRIDWEFTDOUBLECLICK = False Then
            GRIDWEFT.Rows.Add(TXTSRNO.Text, CMBGRIDQUALITY.Text.Trim, CMBSHADE.Text.Trim, Val(TXTPICK.Text.Trim), Val(TXTGRIDWT.Text.Trim), GRIDWEFTCHANGE.Rows(GRIDWEFTCHANGE.CurrentRow.Index).Cells(GSRNO.Index).Value)
            DT_WEFTDETAILS.Rows.Add(Val(TXTSRNO.Text), CMBGRIDQUALITY.Text.Trim, CMBSHADE.Text.Trim, Val(TXTPICK.Text.Trim), Val(TXTGRIDWT.Text), GRIDWEFTCHANGE.Rows(GRIDWEFTCHANGE.CurrentRow.Index).Cells(0).Value)
            getsrno(GRIDWEFT)

        ElseIf GRIDWEFTDOUBLECLICK = True Then
            For I As Integer = 0 To DT_WEFTDETAILS.Rows.Count - 1
                If GRIDWEFT.Item(GSRNO.Index, TEMPWEFTROW).Value = DT_WEFTDETAILS.Rows(I).Item("SRNO") And GRIDWEFT.Item(GWEFTSRNO.Index, TEMPWEFTROW).Value = DT_WEFTDETAILS.Rows(I).Item("WEFTSRNO") Then
                    DT_WEFTDETAILS.Rows(I).Item("GRIDQUALITY") = CMBGRIDQUALITY.Text
                    DT_WEFTDETAILS.Rows(I).Item("SHADE") = CMBSHADE.Text.Trim
                    DT_WEFTDETAILS.Rows(I).Item("PICK") = Val(TXTPICK.Text.Trim)
                    DT_WEFTDETAILS.Rows(I).Item("GRIDWT") = Val(TXTGRIDWT.Text.Trim)
                End If
            Next
LINE1:
            GRIDWEFT.Item(GYARNQUALITY.Index, TEMPWEFTROW).Value = CMBGRIDQUALITY.Text.Trim
            GRIDWEFT.Item(GSHADE.Index, TEMPWEFTROW).Value = CMBSHADE.Text.Trim
            GRIDWEFT.Item(GPICK.Index, TEMPWEFTROW).Value = Val(TXTPICK.Text.Trim)
            GRIDWEFT.Item(GWTPER.Index, TEMPWEFTROW).Value = Val(TXTGRIDWT.Text.Trim)

            GRIDWEFTDOUBLECLICK = False
        End If
        TXTSRNO.Text = GRIDWEFT.RowCount + 1
        CMBGRIDQUALITY.Text = ""
        CMBSHADE.Text = ""
        TXTPICK.Clear()
        TXTGRIDWT.Clear()

        CMBGRIDQUALITY.Focus()
    End Sub

    Private Sub GRIDWEFTCHANGE_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDWEFTCHANGE.CellClick
        Try
            If GRIDWEFTCHANGE.Rows.Count > 0 Then
                GRIDWEFT.RowCount = 0
                GRIDWEFTDOUBLECLICK = False
                For i As Integer = 0 To DT_WEFTDETAILS.Rows.Count - 1
                    If DT_WEFTDETAILS.Rows(i).Item("WEFTSRNO") = GRIDWEFTCHANGE.Rows(GRIDWEFTCHANGE.CurrentRow.Index).Cells(0).Value Then
                        GRIDWEFT.Rows.Add(DT_WEFTDETAILS.Rows(i).Item("SRNO"), DT_WEFTDETAILS.Rows(i).Item("GRIDQUALITY"), DT_WEFTDETAILS.Rows(i).Item("SHADE"), DT_WEFTDETAILS.Rows(i).Item("PICK"), DT_WEFTDETAILS.Rows(i).Item("GRIDWT"), DT_WEFTDETAILS.Rows(i).Item("WEFTSRNO"))
                    End If
                Next
                TOTAL()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDWEFTCHANGE_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDWEFTCHANGE.CellDoubleClick
        Try
            If e.RowIndex >= 0 And GRIDWEFTCHANGE.Item(EWEFTCHANGE.Index, e.RowIndex).Value <> Nothing Then
                GRIDDOUBLECLICK = True
                TEMPROW = e.RowIndex
                TXTSRNO.Text = GRIDWEFTCHANGE.Item(ESRNO.Index, e.RowIndex).Value
                CMBWEFTCHANGE.Text = GRIDWEFTCHANGE.Item(EWEFTCHANGE.Index, e.RowIndex).Value
                CMBWEFTCHANGE.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDWEFTCHANGE_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDWEFTCHANGE.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
LINE1:
                For I As Integer = 0 To DT_WEFTDETAILS.Rows.Count - 1
                    If GRIDWEFTCHANGE.Rows(GRIDWEFTCHANGE.CurrentRow.Index).Cells(ESRNO.Index).Value = Val(DT_WEFTDETAILS.Rows(I).Item("WEFTSRNO")) Then
                        DT_WEFTDETAILS.Rows.RemoveAt(I)
                        GoTo LINE1
                    End If
                Next
                For I As Integer = 0 To DT_WEFTDETAILS.Rows.Count - 1
                    If GRIDWEFTCHANGE.Rows(GRIDWEFTCHANGE.CurrentRow.Index).Cells(ESRNO.Index).Value < Val(DT_WEFTDETAILS.Rows(I).Item("WEFTSRNO")) Then
                        DT_WEFTDETAILS.Rows(I).Item("WEFTSRNO") = Val(DT_WEFTDETAILS.Rows(I).Item("WEFTSRNO")) - 1
                    End If
                Next
                GRIDWEFTCHANGE.Rows.RemoveAt(GRIDWEFTCHANGE.CurrentRow.Index)
                GRIDWEFT.RowCount = 0

                getsrno(GRIDWEFTCHANGE)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTGRIDWT_Validated(sender As Object, e As EventArgs) Handles TXTGRIDWT.Validated
        Try
            If CMBGRIDQUALITY.Text.Trim <> "" And GRIDWEFTCHANGE.RowCount > 0 And Val(TXTGRIDWT.Text.Trim) > 0 Then
                FILLWEFTGRID()
                TOTAL()
                CMBGRIDQUALITY.Text = ""
                CMBSHADE.Text = ""
                TXTGRIDWT.Clear()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDWEFT_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDWEFT.CellDoubleClick
        Try
            If e.RowIndex >= 0 And GRIDWEFT.Item(GYARNQUALITY.Index, e.RowIndex).Value <> Nothing Then
                GRIDWEFTDOUBLECLICK = True
                TEMPWEFTROW = e.RowIndex
                TXTSRNO.Text = Val(GRIDWEFT.Item(GSRNO.Index, e.RowIndex).Value)
                CMBGRIDQUALITY.Text = GRIDWEFT.Item(GYARNQUALITY.Index, e.RowIndex).Value
                CMBSHADE.Text = GRIDWEFT.Item(GSHADE.Index, e.RowIndex).Value
                TXTPICK.Text = Val(GRIDWEFT.Item(GPICK.Index, e.RowIndex).Value)
                TXTGRIDWT.Text = Val(GRIDWEFT.Item(GWTPER.Index, e.RowIndex).Value)

                CMBGRIDQUALITY.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDWEFT_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDWEFT.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                Dim del As Boolean = False
                If GRIDWEFT.RowCount > 0 Then
                    Dim row As Integer = GRIDWEFTCHANGE.Rows(GRIDWEFTCHANGE.CurrentRow.Index).Cells(ESRNO.Index).Value
                    For I As Integer = 0 To DT_WEFTDETAILS.Rows.Count - 1
                        If GRIDWEFTCHANGE.Rows(GRIDWEFTCHANGE.CurrentRow.Index).Cells(ESRNO.Index).Value = Val(DT_WEFTDETAILS.Rows(I).Item("WEFTSRNO")) And GRIDWEFT.Rows(GRIDWEFT.CurrentRow.Index).Cells(GSRNO.Index).Value = Val(DT_WEFTDETAILS.Rows(I).Item("SRNO")) Then
                            If del = False Then
                                DT_WEFTDETAILS.Rows.RemoveAt(I)
                                GRIDWEFT.Rows.RemoveAt(GRIDWEFT.CurrentRow.Index)
                                del = True
                                GoTo line1
                            End If
                        End If
                    Next
line1:
                    For I As Integer = 0 To DT_WEFTDETAILS.Rows.Count - 1
                        If GRIDWEFTCHANGE.Rows(GRIDWEFTCHANGE.CurrentRow.Index).Cells(ESRNO.Index).Value = Val(DT_WEFTDETAILS.Rows(I).Item("WEFTSRNO")) And del = True And row < Val(DT_WEFTDETAILS.Rows(I).Item(GSRNO.Index)) Then
                            DT_WEFTDETAILS.Rows(I).Item("SRNO") = Val(DT_WEFTDETAILS.Rows(I).Item("SRNO")) - 1
                        End If
                    Next
                    getsrno(GRIDWEFT)
                    TOTAL()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDBEAM_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDBEAM.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDBEAM.RowCount > 0 Then
                GRIDBEAM.Rows.RemoveAt(GRIDBEAM.CurrentRow.Index)
                getsrno(GRIDBEAM)
                TOTAL()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub getsrno(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            For Each row As DataGridViewRow In grid.Rows
                row.Cells(0).Value = row.Index + 1
            Next
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTREEDSPACE_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTREEDSPACE.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTPICK_Validated(sender As Object, e As EventArgs) Handles TXTPICK.Validated, CMBGRIDQUALITY.Validated, TXTREEDSPACE.Validated
        Try
            CALC()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CALC()
        Try
            If GRIDBEAM.RowCount = 0 Then Exit Sub
            If CMBGRIDQUALITY.Text.Trim <> "" And Val(TXTPICK.Text.Trim) > 0 And Val(TXTREEDSPACE.Text.Trim) > 0 And Val(GRIDBEAM.Rows(0).Cells(BTL.Index).Value) > 0 And Val(TXTGRIDWT.Text.Trim) = 0 Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("ISNULL(QUALITY_DENIER,0) AS DENIER", "", " QUALITYMASTER ", " AND QUALITY_NAME = '" & CMBGRIDQUALITY.Text.Trim & "' AND QUALITY_YEARID = " & YearId)

                'WE HAVE NOT USED TL FOR HARIA, WE HAVE USED 100 AS TL
                If ClientName = "HARIA" Then
                    TXTGRIDWT.Text = Format((Val(TXTREEDSPACE.Text.Trim) * Val(TXTPICK.Text.Trim) * 100 * Val(DT.Rows(0).Item("DENIER"))) / 9000000, "0.000")
                Else
                    TXTGRIDWT.Text = Format((Val(TXTREEDSPACE.Text.Trim) * Val(TXTPICK.Text.Trim) * Val(GRIDBEAM.Rows(0).Cells(BTL.Index).Value) * Val(DT.Rows(0).Item("DENIER"))) / 9000000, "0.000")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGRIDBEAM_Validated(sender As Object, e As EventArgs) Handles CMBGRIDBEAM.Validated
        Try
            If CMBGRIDBEAM.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" ISNULL(BEAMMASTER.BEAM_TOTALENDS, 0) As ENDS, ISNULL(BEAMMASTER.BEAM_TAPLINE, 0) As TAPLINE, ISNULL(BEAMMASTER.BEAM_TOTALWT, 0) As BEAMWT ", "", "BEAMMASTER", "And BEAMMASTER.BEAM_NAME = '" & CMBGRIDBEAM.Text.Trim & "' AND BEAM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TXTGRIDENDS.Text = Val(DT.Rows(0).Item("ENDS"))
                    TXTGRIDTL.Text = Val(DT.Rows(0).Item("TAPLINE"))
                    TXTGRIDBEAMWT.Text = Val(DT.Rows(0).Item("BEAMWT"))
                End If
                FILLGRIDBEAM()
                TOTAL()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGRIDBEAM_Validating(sender As Object, e As CancelEventArgs) Handles CMBGRIDBEAM.Validating
        Try
            If CMBGRIDBEAM.Text.Trim <> "" Then BEAMVALIDATE(CMBGRIDBEAM, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGRIDBEAM_Enter(sender As Object, e As EventArgs) Handles CMBGRIDBEAM.Enter
        Try
            If CMBGRIDBEAM.Text.Trim = "" Then fillBEAM(CMBGRIDBEAM, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDBEAM_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDBEAM.CellDoubleClick
        Try
            If e.RowIndex >= 0 And GRIDBEAM.Item(BBEAMNAME.Index, e.RowIndex).Value <> Nothing Then
                GRIDBEAMDOUBLECLICK = True
                TEMPBEAMROW = e.RowIndex
                TXTBEAMSRNO.Text = Val(GRIDBEAM.Item(BSRNO.Index, e.RowIndex).Value)
                CMBGRIDBEAM.Text = GRIDBEAM.Item(BBEAMNAME.Index, e.RowIndex).Value
                TXTGRIDENDS.Text = GRIDBEAM.Item(BENDS.Index, e.RowIndex).Value
                TXTGRIDTL.Text = Val(GRIDBEAM.Item(BTL.Index, e.RowIndex).Value)
                TXTGRIDBEAMWT.Text = Val(GRIDBEAM.Item(BBEAMWT.Index, e.RowIndex).Value)
                CMBGRIDBEAM.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


End Class