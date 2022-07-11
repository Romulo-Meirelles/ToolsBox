Imports System.IO
Imports System.Text

Namespace Utils
    Public Class Files
        Public Function FileReplaceBytes(ByVal Path As String, ByVal OldValue As Byte(), ByVal NewValue As Byte()) As Boolean
            Try
                Dim FileLoaded As String = File.ReadAllText(Path, Encoding.Default) : Threading.Thread.Sleep(100)
                Dim SearchString As String = Encoding.Default.GetString(OldValue) : Threading.Thread.Sleep(100)
                Dim ReplaceString As String = Encoding.Default.GetString(NewValue) : Threading.Thread.Sleep(100)

                If Not FileLoaded.Contains(SearchString) Then
                    Return False
                End If

                Dim NewFile As String = FileLoaded.Replace(SearchString, ReplaceString) : Threading.Thread.Sleep(100)
                File.WriteAllBytes(Path, Encoding.Default.GetBytes(NewFile))

                If NewFile.Contains(ReplaceString) Then
                    Return True
                Else
                    Return False
                End If
				
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)
                Return False
            End Try
        End Function
        Public Function FileReplaceString(ByVal Path As String, ByVal OldValueHex As String, ByVal NewValueHex As String) As Boolean
            Try
                Dim FileLoaded As String = File.ReadAllText(Path, Encoding.Default) : Threading.Thread.Sleep(100)
                Dim SearchString As String = HexToString(OldValueHex) : Threading.Thread.Sleep(100)
                Dim ReplaceString As String = String.Empty
                Dim Tratar As String = String.Empty

                If Not FileLoaded.Contains(SearchString) Then
                    Return False
                End If

                If NewValueHex Is Nothing Then
                    For i = 0 To OldValueHex.Length - 1
                        Tratar += "0"
                    Next
                    ReplaceString = HexToString(Tratar)
                ElseIf NewValueHex.ToLower = "null" Then
                    For i = 0 To OldValueHex.Length - 1
                        Tratar += "0"
                    Next
                    ReplaceString = HexToString(Tratar)
                ElseIf NewValueHex.ToLower = "" Then
                    For i = 0 To OldValueHex.Length - 1
                        Tratar += "0"
                    Next
                    ReplaceString = HexToString(Tratar)
                Else
                    ReplaceString = HexToString(NewValueHex) : Threading.Thread.Sleep(100)
                End If

                Dim NewFile As String = FileLoaded.Replace(SearchString, ReplaceString) : Threading.Thread.Sleep(100)
                File.WriteAllBytes(Path, Encoding.Default.GetBytes(NewFile))

                If NewFile.Contains(ReplaceString) Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)
                Return False
            End Try
        End Function
        Private Function StringToHex(ByVal Strings As String) As String
            Try
                StringToHex = Nothing
                For i As Integer = 0 To Strings.Length - 1
                    StringToHex &= Asc(Strings.Substring(i, 1)).ToString("x").ToUpper
                Next
                Return StringToHex.ToString
            Catch EX As Exception
                Return EX.Message
            End Try
        End Function
        Private Function HexToString(ByVal Hex As String) As String
            Try
                Dim A As String = Hex.Replace(" ", "").Replace(".", "00").Replace("$", "24").Replace("?", "00")
                Dim text As New System.Text.StringBuilder(Hex.Length \ 2)
                For i As Integer = 0 To A.Length - 2 Step 2
                    text.Append(Chr(Convert.ToByte(A.Substring(i, 2), 16)))
                Next
                Return text.ToString
            Catch EX As Exception
                Return EX.Message
            End Try
        End Function
    End Class
End Namespace
