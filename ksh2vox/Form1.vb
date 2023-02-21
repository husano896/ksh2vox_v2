Imports Microsoft.VisualBasic.FileIO
Imports System.Collections.ObjectModel
Public Class Form1
    Dim mystery_number As UShort = 6
    Dim Laser_convert As String = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmno"
    Dim FX_convert As String = "11STIFUHQADXVB11111"
    Dim BEAT_INFO As New ArrayList
    Dim BPM_INFO As New ArrayList
    Dim TILT_INFO As New ArrayList
    '#LYRIC INFO
    Dim END_POSITION As String
    '#TAB EFFECT INFO
    '#FXBUTTON EFFECT INFO
    Dim TRACK1_START As New ArrayList
    Dim TRACK2_START As New ArrayList
    Dim TRACK3_START As New ArrayList
    Dim TRACK4_START As New ArrayList
    Dim TRACK5_START As New ArrayList
    Dim TRACK6_START As New ArrayList
    Dim TRACK7_START As New ArrayList
    Dim TRACK8_START As New ArrayList
    Dim Temp_Data_ArrayList As New ArrayList

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        BEAT_INFO.Clear()
        BPM_INFO.Clear()
        TILT_INFO.Clear()
        END_POSITION = ""
        TRACK1_START.Clear()
        TRACK2_START.Clear()
        TRACK3_START.Clear()
        TRACK4_START.Clear()
        TRACK5_START.Clear()
        TRACK6_START.Clear()
        TRACK7_START.Clear()
        TRACK8_START.Clear()
        Temp_Data_ArrayList.Clear()
        TILT_INFO.Add("001,01,00" & vbTab & "0")
        'Chart Data
        '0 = Peak / 1 = ? / 2 = LPF / 3 = ? / 4 = HPF / 5 = Bitcrusher
        Dim Current_Filtertype As Integer
        Dim Current_Position(3) As Integer
        Dim Current_Beat(2) As Integer
        Dim Current_LongJudge() As Integer = {0, 0, 0, 0, 0, 0, 0, 0}    'ABCDLR
        Dim Secret_Number() As Integer = {0, 0}
        Dim Last_Slam() As Integer = {0, 0}
        Dim Debug_Laser() As Boolean = {False, False}
        Current_Position(1) = 1
        Dim note_size, note_spilt_size, measure_maxsize As Integer
        'Read Data
        Dim temp_string As String = ""
        Dim temp_string2 As String = ""
        Dim temp_arraystring As String()
        Dim temp_trackstring1 As String = ""
        Dim temp_trackstring2 As String = ""
        Dim temp_trackstring3 As String = ""
        Dim temp_trackstring4 As String = ""
        Dim temp_trackstring5 As String = ""
        Dim temp_trackstring6 As String = ""
        Dim temp_trackstring7 As String = ""
        Dim temp_trackstring8 As String = ""
        Dim value As String = ""
        Dim MySF As IO.StreamReader = New IO.StreamReader(TextBox1.Text, System.Text.Encoding.Default)
        Do While Not MySF.EndOfStream
            value = MySF.ReadLine() '讀取單行
            If (value = "--") Then
                'Save Data to each track
                If (Current_Position(0) > 0) Then
                    For i = 0 To Temp_Data_ArrayList.Count - 1
                        If (Current_Beat(0) > 0 And Current_Beat(1) > 0) Then
                            measure_maxsize = 192 / Current_Beat(1) * Current_Beat(0)
                            note_spilt_size = measure_maxsize \ note_size
                        End If
                        temp_string = Temp_Data_ArrayList.Item(i)
                        temp_string2 = Current_Position(0).ToString.PadLeft(3, "0") & "," & Current_Position(1).ToString.PadLeft(2, "0") & "," & Current_Position(2).ToString.PadLeft(2, "0")
                        If (temp_string.Contains("|")) Then  'is a note?
                            'BTA
                            If (temp_string(0) = "1") Then 'BT-A 1 = Chip 2 = Long
                                TRACK3_START.Add(temp_string2 & vbTab & "0" & vbTab & "255")
                            ElseIf (temp_string(0) = "2") Then
                                If (Current_LongJudge(0) = 0) Then
                                    temp_trackstring1 = temp_string2 & vbTab
                                End If
                                Current_LongJudge(0) += note_spilt_size

                            ElseIf (Current_LongJudge(0) > 0 And temp_string(0) = "0") Then
                                TRACK3_START.Add(temp_trackstring1 & Current_LongJudge(0) & vbTab & "3")
                                Current_LongJudge(0) = 0
                            End If
                            'BTB
                            If (temp_string(1) = "1") Then 'BT-B 1 = Chip 2 = Long
                                TRACK4_START.Add(temp_string2 & vbTab & "0" & vbTab & "255")
                            ElseIf (temp_string(1) = "2") Then
                                If (Current_LongJudge(1) = 0) Then
                                    temp_trackstring2 = temp_string2 & vbTab
                                End If
                                Current_LongJudge(1) += note_spilt_size

                            ElseIf (Current_LongJudge(1) > 0 And temp_string(1) = "0") Then
                                TRACK4_START.Add(temp_trackstring2 & Current_LongJudge(1) & vbTab & "3")
                                Current_LongJudge(1) = 0
                            End If

                            'BTC
                            If (temp_string(2) = "1") Then 'BT-C 1 = Chip 2 = Long
                                TRACK5_START.Add(temp_string2 & vbTab & "0" & vbTab & "255")
                            ElseIf (temp_string(2) = "2") Then
                                If (Current_LongJudge(2) = 0) Then
                                    temp_trackstring3 = temp_string2 & vbTab
                                End If
                                Current_LongJudge(2) += note_spilt_size

                            ElseIf (Current_LongJudge(2) > 0 And temp_string(2) = "0") Then
                                TRACK5_START.Add(temp_trackstring3 & Current_LongJudge(2) & vbTab & "3")
                                Current_LongJudge(2) = 0
                            End If
                            'BTD
                            If (temp_string(3) = "1") Then 'BT-D 1 = Chip 2 = Long
                                TRACK6_START.Add(temp_string2 & vbTab & "0" & vbTab & "255")
                            ElseIf (temp_string(3) = "2") Then
                                If (Current_LongJudge(3) = 0) Then
                                    temp_trackstring4 = temp_string2 & vbTab
                                End If
                                Current_LongJudge(3) += note_spilt_size

                            ElseIf (Current_LongJudge(3) > 0 And temp_string(3) = "0") Then
                                TRACK6_START.Add(temp_trackstring4 & Current_LongJudge(3) & vbTab & "3")
                                Current_LongJudge(3) = 0
                            End If
                            'FX-L
                            If Not (temp_string(5) = "0") Then 'FX-L 1 = Long 2 = Chip
                                If (temp_string(5) = "2") Then
                                    TRACK2_START.Add(temp_string2 & vbTab & "0" & vbTab & "255")
                                Else
                                    If (Current_LongJudge(4) = 0) Then
                                        temp_trackstring5 = temp_string2 & vbTab & "lg" & vbTab & FX_convert.IndexOf(temp_string(5).ToString)
                                    End If
                                    Current_LongJudge(4) += note_spilt_size
                                End If
                            Else
                                If (Current_LongJudge(4) > 0) Then
                                    temp_trackstring5 = temp_trackstring5.Replace("lg", Current_LongJudge(4).ToString)
                                    TRACK2_START.Add(temp_trackstring5)
                                    Current_LongJudge(4) = 0
                                End If
                            End If
                            'FX-R
                            If Not (temp_string(6) = "0") Then 'FX-L 1 = Long 2 = Chip
                                If (temp_string(6) = "2") Then
                                    TRACK7_START.Add(temp_string2 & vbTab & "0" & vbTab & "255")
                                Else
                                    If (Current_LongJudge(5) = 0) Then
                                        temp_trackstring6 = temp_string2 & vbTab & "lg" & vbTab & FX_convert.IndexOf(temp_string(6).ToString)
                                    End If
                                    Current_LongJudge(5) += note_spilt_size
                                End If
                            Else
                                If (Current_LongJudge(5) > 0) Then
                                    temp_trackstring6 = temp_trackstring6.Replace("lg", Current_LongJudge(5).ToString)
                                    TRACK7_START.Add(temp_trackstring6)
                                    Current_LongJudge(5) = 0
                                End If
                            End If
                            'DAMN U LASER
                            'L-Laser
                            If Not (temp_string(8) = "-") Then
                                If (temp_string(8) = ":") Then
                                    Secret_Number(0) += note_spilt_size
                                    Last_Slam(0) -= note_spilt_size
                                Else

                                    Secret_Number(0) += note_spilt_size
                                    If (Secret_Number(0) = 6) Then 'slam?

                                        If (Current_Position(2) - mystery_number >= 0) Then
                                            If Current_Position(2) Mod 12 = 6 Then
                                                temp_trackstring7 = Current_Position(0).ToString.PadLeft(3, "0") & "," & Current_Position(1).ToString.PadLeft(2, "0") & "," & (Current_Position(2) - mystery_number).ToString.PadLeft(2, "0")
                                            Else
                                                temp_trackstring7 = Current_Position(0).ToString.PadLeft(3, "0") & "," & Current_Position(1).ToString.PadLeft(2, "0") & "," & Current_Position(2).ToString.PadLeft(2, "0")
                                            End If
                                        Else
                                            temp_trackstring7 = Current_Position(0).ToString.PadLeft(3, "0") & "," & Current_Position(1).ToString.PadLeft(2, "0") & "," & Current_Position(2).ToString.PadLeft(2, "0")
                                        End If

                                            Last_Slam(0) = 6
                                            If (Int((Laser_convert.IndexOf(temp_string(8))) / 50 * 127) = Current_LongJudge(6) - 1) Then
                                                If (Debug_Laser(0) = True) Then 'same for more than 2 time?
                                                    temp_trackstring7 &= vbTab & Int((Laser_convert.IndexOf(temp_string(8))) / 50 * 127)
                                                Else
                                                    If (Int((Laser_convert.IndexOf(temp_string(8))) / 50 * 127) >= 126) Then
                                                        temp_trackstring7 &= vbTab & "126"
                                                    Else
                                                        temp_trackstring7 &= vbTab & Int((Laser_convert.IndexOf(temp_string(8))) / 50 * 127) + 1
                                                    End If
                                                End If
                                                Debug_Laser(0) = Not Debug_Laser(0)

                                            Else
                                                temp_trackstring7 &= vbTab & Int((Laser_convert.IndexOf(temp_string(8))) / 50 * 127)
                                                Debug_Laser(0) = False
                                            End If

                                            Secret_Number(0) = 0
                                        Else
                                            temp_trackstring7 = Current_Position(0).ToString.PadLeft(3, "0") & "," & Current_Position(1).ToString.PadLeft(2, "0") & "," & Current_Position(2).ToString.PadLeft(2, "0")
                                            If (Int((Laser_convert.IndexOf(temp_string(8))) / 50 * 127) = Current_LongJudge(6) - 1) Then
                                                If (Debug_Laser(0) = True) Then 'same for more than 2 time?
                                                    temp_trackstring7 &= vbTab & Int((Laser_convert.IndexOf(temp_string(8))) / 50 * 127)
                                                Else
                                                    If (Int((Laser_convert.IndexOf(temp_string(8))) / 50 * 127) >= 126) Then
                                                        temp_trackstring7 &= vbTab & "126"
                                                    Else
                                                        temp_trackstring7 &= vbTab & Int((Laser_convert.IndexOf(temp_string(8))) / 50 * 127) + 1
                                                    End If
                                                End If
                                                Debug_Laser(0) = Not Debug_Laser(0)
                                            Else
                                                temp_trackstring7 &= vbTab & Int((Laser_convert.IndexOf(temp_string(8))) / 50 * 127)
                                                Debug_Laser(0) = False
                                            End If
                                        End If

                                    Secret_Number(0) = 0
                                    If (Current_LongJudge(6) = 0) Then   'start mark?
                                        temp_trackstring7 &= vbTab & "1"
                                        Current_LongJudge(6) = 1
                                    Else
                                        temp_trackstring7 &= vbTab & "0"
                                    End If
                                    'rotate not done
                                    temp_trackstring7 &= vbTab & "0"
                                    'Filter type
                                    temp_trackstring7 &= vbTab & Current_Filtertype

                                    TRACK1_START.Add(temp_trackstring7)
                                    Current_LongJudge(6) = Int((Laser_convert.IndexOf(temp_string(8))) / 50 * 127) + 1
                                End If
                            Else
                                If (Current_LongJudge(6) > 0) Then
                                    temp_trackstring7 = TRACK1_START.Item(TRACK1_START.Count - 1)
                                    temp_trackstring7 = temp_trackstring7.Remove(9)
                                    If (Current_LongJudge(6) = 1) Then
                                        temp_trackstring7 &= vbTab & "1" & vbTab & "2" & vbTab & "0" & vbTab & Current_Filtertype
                                    ElseIf (Current_LongJudge(6) = 128) Then
                                        temp_trackstring7 &= vbTab & "126" & vbTab & "2" & vbTab & "0" & vbTab & Current_Filtertype
                                    Else
                                        temp_trackstring7 &= vbTab & (Current_LongJudge(6) + 1) & vbTab & "2" & vbTab & "0" & vbTab & Current_Filtertype
                                    End If
                                    TRACK1_START.RemoveAt(TRACK1_START.Count - 1)   'Remove and readd
                                    TRACK1_START.Add(temp_trackstring7)
                                    Current_LongJudge(6) = 0
                                    Secret_Number(1) = 0
                                End If

                            End If
                            'R-Laser
                            If Not (temp_string(9) = "-") Then
                                If (temp_string(9) = ":") Then
                                    Secret_Number(1) += note_spilt_size
                                    Last_Slam(1) -= note_spilt_size
                                Else

                                    Secret_Number(1) += note_spilt_size

                                    If (Secret_Number(1) = 6) Then 'slam?
                                        If (Current_Position(2) - mystery_number >= 0) Then
                                            If Current_Position(2) Mod 12 = 6 Then
                                                temp_trackstring8 = Current_Position(0).ToString.PadLeft(3, "0") & "," & Current_Position(1).ToString.PadLeft(2, "0") & "," & (Current_Position(2) - mystery_number).ToString.PadLeft(2, "0")
                                            Else
                                                temp_trackstring8 = Current_Position(0).ToString.PadLeft(3, "0") & "," & Current_Position(1).ToString.PadLeft(2, "0") & "," & Current_Position(2).ToString.PadLeft(2, "0")
                                            End If
                                        Else
                                            temp_trackstring8 = Current_Position(0).ToString.PadLeft(3, "0") & "," & Current_Position(1).ToString.PadLeft(2, "0") & "," & Current_Position(2).ToString.PadLeft(2, "0")
                                        End If
                                        Last_Slam(1) = 6
                                        If (Int((Laser_convert.IndexOf(temp_string(9))) / 50 * 127) = Current_LongJudge(7) - 1) Then
                                            If (Debug_Laser(1) = True) Then 'same for more than 2 time?
                                                temp_trackstring8 &= vbTab & Int((Laser_convert.IndexOf(temp_string(9))) / 50 * 127)
                                            Else
                                                If (Int((Laser_convert.IndexOf(temp_string(9))) / 50 * 127) >= 126) Then
                                                    temp_trackstring8 &= vbTab & "126"
                                                Else
                                                    temp_trackstring8 &= vbTab & Int((Laser_convert.IndexOf(temp_string(9))) / 50 * 127) + 1
                                                End If
                                            End If
                                            Debug_Laser(1) = Not Debug_Laser(1)
                                        Else
                                            temp_trackstring8 &= vbTab & Int((Laser_convert.IndexOf(temp_string(9))) / 50 * 127)
                                            Debug_Laser(1) = False
                                        End If
                                        Secret_Number(1) = 0
                                    Else
                                        temp_trackstring8 = Current_Position(0).ToString.PadLeft(3, "0") & "," & Current_Position(1).ToString.PadLeft(2, "0") & "," & Current_Position(2).ToString.PadLeft(2, "0")
                                        If (Int((Laser_convert.IndexOf(temp_string(9))) / 50 * 127) = Current_LongJudge(7) - 1) Then
                                            If (Debug_Laser(1) = True) Then 'same for more than 2 time?
                                                temp_trackstring8 &= vbTab & Int((Laser_convert.IndexOf(temp_string(9))) / 50 * 127)
                                            Else
                                                If (Int((Laser_convert.IndexOf(temp_string(9))) / 50 * 127) >= 126) Then
                                                    temp_trackstring8 &= vbTab & "126"
                                                Else
                                                    temp_trackstring8 &= vbTab & Int((Laser_convert.IndexOf(temp_string(9))) / 50 * 127) + 1
                                                End If
                                            End If
                                            Debug_Laser(1) = Not Debug_Laser(1)
                                        Else
                                            temp_trackstring8 &= vbTab & Int((Laser_convert.IndexOf(temp_string(9))) / 50 * 127)
                                            Debug_Laser(1) = False
                                        End If
                                    End If
                                    ' MsgBox(temp_trackstring8 & "," & Secret_Number(1))
                                    Secret_Number(1) = 0
                                    If (Current_LongJudge(7) = 0) Then   'start mark?
                                        temp_trackstring8 &= vbTab & "1"
                                    Else
                                        temp_trackstring8 &= vbTab & "0"
                                    End If
                                    'rotate not done
                                    temp_trackstring8 &= vbTab & "0"
                                    'Filter type
                                    temp_trackstring8 &= vbTab & Current_Filtertype

                                    TRACK8_START.Add(temp_trackstring8)
                                    Current_LongJudge(7) = Int((Laser_convert.IndexOf(temp_string(9))) / 50 * 127) + 1
                                End If
                            Else
                                    'Make a end mark
                                    If (Current_LongJudge(7) > 0) Then
                                        temp_trackstring8 = TRACK8_START.Item(TRACK8_START.Count - 1)
                                        temp_trackstring8 = temp_trackstring8.Remove(9)
                                        If (Current_LongJudge(7) = 1) Then
                                            temp_trackstring8 &= vbTab & "1" & vbTab & "2" & vbTab & "0" & vbTab & Current_Filtertype
                                        ElseIf (Current_LongJudge(7) = 128) Then
                                            temp_trackstring8 &= vbTab & "126" & vbTab & "2" & vbTab & "0" & vbTab & Current_Filtertype
                                        Else
                                            temp_trackstring8 &= vbTab & (Current_LongJudge(7) + 1) & vbTab & "2" & vbTab & "0" & vbTab & Current_Filtertype
                                        End If
                                        TRACK8_START.RemoveAt(TRACK8_START.Count - 1)   'Remove and readd
                                        TRACK8_START.Add(temp_trackstring8)
                                        Current_LongJudge(7) = 0
                                        Secret_Number(1) = 0
                                    End If

                            End If
                            Current_Position(2) += note_spilt_size
                            'Move to next position
                            If (Current_Position(2) >= (192 / Current_Beat(1))) Then
                                Current_Position(1) += Current_Position(2) / (192 / Current_Beat(1))
                                Current_Position(2) = Current_Position(2) Mod (192 / Current_Beat(1))
                            End If
                        ElseIf (temp_string.StartsWith("beat=")) Then   'Beat Change?
                            temp_string = temp_string.Replace("beat=", "")
                            temp_arraystring = temp_string.Split("/")
                            temp_string = temp_string2 & vbTab & temp_arraystring(0) & vbTab & temp_arraystring(1)
                            Current_Beat(0) = temp_arraystring(0)
                            Current_Beat(1) = temp_arraystring(1)
                            BEAT_INFO.Add(temp_string)
                        ElseIf (temp_string.StartsWith("t=")) Then
                            temp_string = temp_string.Replace("t=", "")
                            Dim BPM As Double
                            BPM = temp_string
                            temp_string = temp_string2 & vbTab & Format(BPM, "0.0000") & vbTab & "4"
                            BPM_INFO.Add(temp_string)
                        ElseIf (temp_string.StartsWith("filtertype=")) Then
                            If (temp_string.Contains("fx;bitc")) Then
                                Current_Filtertype = 5
                            ElseIf (temp_string.Contains("hpf1")) Then
                                Current_Filtertype = 4
                            ElseIf (temp_string.Contains("lpf1")) Then
                                Current_Filtertype = 2
                            ElseIf (temp_string.Contains("peak")) Then
                                Current_Filtertype = 0
                            End If
                        ElseIf (temp_string.StartsWith("tilt=")) Then
                            If (temp_string.Contains("keep_")) Then
                                temp_string = temp_string2 & vbTab & "2"
                            ElseIf (temp_string.Contains("big")) Then
                                temp_string = temp_string2 & vbTab & "1"
                            Else
                                temp_string = temp_string2 & vbTab & "0"
                            End If
                            TILT_INFO.Add(temp_string)
                        End If
                    Next
                    note_size = 0
                    Temp_Data_ArrayList.Clear()
                    Current_Position(1) = 1
                    Current_Position(2) = 0
                    Secret_Number(0) = 0
                    Secret_Number(1) = 0
                End If
                Current_Position(0) += 1
            ElseIf (Current_Position(0) = 0) Then ' ksh chart init
                If (value.StartsWith("t=")) Then
                    If Not (value.Contains("-")) Then   'BPM changing chart?
                        temp_string = value.Replace("t=", "001,01,00" & vbTab)
                        temp_string = temp_string & vbTab & "4"
                        BPM_INFO.Add(temp_string)
                    End If
                ElseIf (value.StartsWith("filtertype=")) Then
                    If (value.Contains("fx;bitc")) Then
                        Current_Filtertype = 5
                    ElseIf (value.Contains("hpf1")) Then
                        Current_Filtertype = 4
                    ElseIf (value.Contains("lpf1")) Then
                        Current_Filtertype = 2
                    ElseIf (value.Contains("peak")) Then
                        Current_Filtertype = 0
                    End If
                End If
            Else
                'Chart Load started
                Temp_Data_ArrayList.Add(value)
                If (value.Contains("|")) Then
                    note_size += 1
                End If
            End If
        Loop
        MySF.Dispose()
        temp_string2 = (Current_Position(0) + 2).ToString.PadLeft(3, "0") & ",01,00"
        END_POSITION = temp_string2

        'Start to save data

        My.Computer.FileSystem.WriteAllText(TextBox1.Text & ".vox", "", False)
        'HEADER
        temp_string = "//====================================" & vbCrLf & "// SOUND VOLTEX OUTPUT TEXT FILE" & vbCrLf & "//====================================" & vbCrLf & vbCrLf
        My.Computer.FileSystem.WriteAllText(TextBox1.Text & ".vox", temp_string, True)
        'VERSION
        temp_string = "#FORMAT VERSION" & vbCrLf & "5" & vbCrLf & "#END" & vbCrLf & vbCrLf
        My.Computer.FileSystem.WriteAllText(TextBox1.Text & ".vox", temp_string, True)
        'BEAT INFO
        temp_string = "#BEAT INFO" & vbCrLf
        For i = 0 To BEAT_INFO.Count - 1
            temp_string = temp_string & BEAT_INFO.Item(i) & vbCrLf
        Next
        temp_string = temp_string & "#END" & vbCrLf & vbCrLf
        My.Computer.FileSystem.WriteAllText(TextBox1.Text & ".vox", temp_string, True)
        'BPM INFO
        temp_string = "#BPM INFO" & vbCrLf
        For i = 0 To BPM_INFO.Count - 1
            temp_string = temp_string & BPM_INFO.Item(i) & vbCrLf
        Next
        temp_string = temp_string & "#END" & vbCrLf & vbCrLf
        My.Computer.FileSystem.WriteAllText(TextBox1.Text & ".vox", temp_string, True)
        'TILT MODE INFO
        temp_string = "#TILT MODE INFO" & vbCrLf
        For i = 0 To TILT_INFO.Count - 1
            temp_string = temp_string & TILT_INFO.Item(i) & vbCrLf
        Next

        temp_string = temp_string & "#END" & vbCrLf & vbCrLf
        My.Computer.FileSystem.WriteAllText(TextBox1.Text & ".vox", temp_string, True)
        'LYRIC INFO(Really need?)
        temp_string = "#LYRIC INFO" & vbCrLf & "#END" & vbCrLf & vbCrLf
        My.Computer.FileSystem.WriteAllText(TextBox1.Text & ".vox", temp_string, True)
        'END POSITION
        temp_string = "#END POSITION" & vbCrLf & END_POSITION & vbCrLf & "#END" & vbCrLf & vbCrLf
        My.Computer.FileSystem.WriteAllText(TextBox1.Text & ".vox", temp_string, True)
        'TAB EFFECT INFO
        'Still don't know how to use it -_-
        temp_string = "#TAB EFFECT INFO" & vbCrLf
        temp_string &= "1," & vbTab & "90.00," & vbTab & "400.00," & vbTab & "18000.00," & vbTab & "0.70" & vbCrLf
        temp_string &= "1," & vbTab & "90.00," & vbTab & "600.00," & vbTab & "15000.00," & vbTab & "5.00" & vbCrLf
        temp_string &= "2," & vbTab & "90.00," & vbTab & "40.00," & vbTab & "5000.00," & vbTab & "0.70" & vbCrLf
        temp_string &= "2," & vbTab & "90.00," & vbTab & "40.00," & vbTab & "2000.00," & vbTab & "3.00" & vbCrLf
        temp_string &= "3," & vbTab & "100.00," & vbTab & "30" & vbCrLf
        temp_string &= "#END" & vbCrLf & vbCrLf
        My.Computer.FileSystem.WriteAllText(TextBox1.Text & ".vox", temp_string, True)
        'FXBUTTON EFFECT INFO
        'also don't know how to use it -_-
        temp_string = "#FXBUTTON EFFECT INFO" & vbCrLf
        temp_string &= "1," & vbTab & "4," & vbTab & "95.00," & vbTab & "2.00," & vbTab & "1.00," & vbTab & "0.85," & vbTab & "0.15" & vbCrLf
        temp_string &= "1," & vbTab & "8," & vbTab & "95.00," & vbTab & "2.00," & vbTab & "1.00," & vbTab & "0.75," & vbTab & "0.1" & vbCrLf
        temp_string &= "2," & vbTab & "98.00," & vbTab & "8," & vbTab & "1.00" & vbCrLf
        temp_string &= "3," & vbTab & "75.00," & vbTab & "2.00," & vbTab & "0.50," & vbTab & "90," & vbTab & "2.00" & vbCrLf
        temp_string &= "1," & vbTab & "16," & vbTab & "95.00," & vbTab & "2.00," & vbTab & "1.00," & vbTab & "0.87," & vbTab & "0.13" & vbCrLf
        temp_string &= "2," & vbTab & "98.00," & vbTab & "4," & vbTab & "1.00" & vbCrLf
        temp_string &= "1," & vbTab & "4," & vbTab & "100.00," & vbTab & "4.00," & vbTab & "0.60," & vbTab & "1.00," & vbTab & "0.85" & vbCrLf
        temp_string &= "4," & vbTab & "100.00," & vbTab & "8.00," & vbTab & "0.40" & vbCrLf
        temp_string &= "5," & vbTab & "90.00," & vbTab & "1.00," & vbTab & "45," & vbTab & "50," & vbTab & "60" & vbCrLf
        temp_string &= "6," & vbTab & "0," & vbTab & "3," & vbTab & "80.00," & vbTab & "500.00," & vbTab & "18000.00," & vbTab & "4.00," & vbTab & "1.40" & vbCrLf
        temp_string &= "1," & vbTab & "6," & vbTab & "95.00," & vbTab & "2.00," & vbTab & "1.00," & vbTab & "0.85," & vbTab & "0.15" & vbCrLf
        temp_string &= "7," & vbTab & "100.00," & vbTab & "12" & vbCrLf
        temp_string &= "#END" & vbCrLf & vbCrLf
        My.Computer.FileSystem.WriteAllText(TextBox1.Text & ".vox", temp_string, True)
        'TRACK INFO
        temp_string = "//====================================" & vbCrLf & "// TRACK INFO" & vbCrLf & "//====================================" & vbCrLf & vbCrLf
        My.Computer.FileSystem.WriteAllText(TextBox1.Text & ".vox", temp_string, True)
        'TRACK1
        temp_string = "#TRACK1" & vbCrLf
        For i = 0 To TRACK1_START.Count - 1
            temp_string = temp_string & TRACK1_START.Item(i) & vbCrLf
        Next
        temp_string = temp_string & "#END" & vbCrLf & vbCrLf & "//====================================" & vbCrLf & vbCrLf
        My.Computer.FileSystem.WriteAllText(TextBox1.Text & ".vox", temp_string, True)
        'TRACK2
        temp_string = "#TRACK2" & vbCrLf
        For i = 0 To TRACK2_START.Count - 1
            temp_string = temp_string & TRACK2_START.Item(i) & vbCrLf
        Next
        temp_string = temp_string & "#END" & vbCrLf & vbCrLf & "//====================================" & vbCrLf & vbCrLf
        My.Computer.FileSystem.WriteAllText(TextBox1.Text & ".vox", temp_string, True)
        'TRACK3
        temp_string = "#TRACK3" & vbCrLf
        For i = 0 To TRACK3_START.Count - 1
            temp_string = temp_string & TRACK3_START.Item(i) & vbCrLf
        Next
        temp_string = temp_string & "#END" & vbCrLf & vbCrLf & "//====================================" & vbCrLf & vbCrLf
        My.Computer.FileSystem.WriteAllText(TextBox1.Text & ".vox", temp_string, True)
        'TRACK4
        temp_string = "#TRACK4" & vbCrLf
        For i = 0 To TRACK4_START.Count - 1
            temp_string = temp_string & TRACK4_START.Item(i) & vbCrLf
        Next
        temp_string = temp_string & "#END" & vbCrLf & vbCrLf & "//====================================" & vbCrLf & vbCrLf
        My.Computer.FileSystem.WriteAllText(TextBox1.Text & ".vox", temp_string, True)
        'TRACK5
        temp_string = "#TRACK5" & vbCrLf
        For i = 0 To TRACK5_START.Count - 1
            temp_string = temp_string & TRACK5_START.Item(i) & vbCrLf
        Next
        temp_string = temp_string & "#END" & vbCrLf & vbCrLf & "//====================================" & vbCrLf & vbCrLf
        My.Computer.FileSystem.WriteAllText(TextBox1.Text & ".vox", temp_string, True)
        'TRACK6
        temp_string = "#TRACK6" & vbCrLf
        For i = 0 To TRACK6_START.Count - 1
            temp_string = temp_string & TRACK6_START.Item(i) & vbCrLf
        Next
        temp_string = temp_string & "#END" & vbCrLf & vbCrLf & "//====================================" & vbCrLf & vbCrLf
        My.Computer.FileSystem.WriteAllText(TextBox1.Text & ".vox", temp_string, True)
        'TRACK7
        temp_string = "#TRACK7" & vbCrLf
        For i = 0 To TRACK7_START.Count - 1
            temp_string = temp_string & TRACK7_START.Item(i) & vbCrLf
        Next
        temp_string = temp_string & "#END" & vbCrLf & vbCrLf & "//====================================" & vbCrLf & vbCrLf
        My.Computer.FileSystem.WriteAllText(TextBox1.Text & ".vox", temp_string, True)
        'TRACK8
        temp_string = "#TRACK8" & vbCrLf
        For i = 0 To TRACK8_START.Count - 1
            temp_string = temp_string & TRACK8_START.Item(i) & vbCrLf
        Next
        temp_string = temp_string & "#END" & vbCrLf & vbCrLf & "//====================================" & vbCrLf & vbCrLf
        My.Computer.FileSystem.WriteAllText(TextBox1.Text & ".vox", temp_string, True)
        MsgBox("Done :)", MsgBoxStyle.Information)
    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        TextBox1.Text = OpenFileDialog1.FileName
    End Sub
End Class
