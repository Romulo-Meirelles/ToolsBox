Imports System.Text
Imports System.Security.Cryptography
Namespace Utils
    Public Class SHA256H
        Shared Function Hash(ByVal Content As String) As String
            Using hasher As SHA256 = SHA256.Create()    ' create hash object

                ' Convert to byte array and get hash
                Dim dbytes As Byte() = hasher.ComputeHash(Encoding.UTF8.GetBytes(Content))

                ' sb to create string from bytes
                Dim sBuilder As New StringBuilder()

                ' convert byte data to hex string
                For n As Integer = 0 To dbytes.Length - 1
                    sBuilder.Append(dbytes(n).ToString("X2"))
                Next n

                Return sBuilder.ToString()
            End Using
        End Function
    End Class
End Namespace