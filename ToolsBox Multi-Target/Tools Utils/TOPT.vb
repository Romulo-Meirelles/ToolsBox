Imports System.Security.Cryptography
Imports System.Text
Namespace Utils
    Public Module AuthTOTP
        Public Function ComputeTOTP(SecretBase As String, Optional TimeCorrection As Long = 0) As String
            Try
                Dim TOTP As New TOPT(SecretBase, TimeCorrection)
                Return TOTP.ComputeTotp
            Catch ex As Exception
                Console.WriteLine(ex.Message)
                Return Nothing
            End Try
        End Function
    End Module

    Friend Class TOPT
        Private Property SecretBase_ As String
        Private Property TimeCorrectionSeconds_ As Long
        Sub New(SecretBase As String, Optional TimeCorrection As Long = 0)
            SecretBase_ = SecretBase
            TimeCorrectionSeconds_ = TimeCorrection
        End Sub
        Friend Function ComputeTotp() As String
            ' 1. Decode secret Base32
            Dim secretBytes As Byte() = Base32Decode(SecretBase_.ToUpper())

            ' 2. Get current Unix time
            Dim unixTime As Long = CLng((DateTime.UtcNow - New DateTime(1970, 1, 1)).TotalSeconds) + TimeCorrectionSeconds_

            ' 3. Calculate time step (30 sec)
            Dim timeStep As Long = unixTime \ 30
            Dim timestepBytes As Byte() = BitConverter.GetBytes(timeStep)
            If BitConverter.IsLittleEndian Then
                Array.Reverse(timestepBytes)
            End If

            ' 4. Apply HMAC-SHA1
            Dim hmac = New HMACSHA1(secretBytes)
            Dim hash = hmac.ComputeHash(timestepBytes)

            ' 5. Dynamic truncation
            Dim offset = hash(hash.Length - 1) And &HF
            Dim binaryCode = ((hash(offset) And &H7F) << 24) Or
                             ((hash(offset + 1) And &HFF) << 16) Or
                             ((hash(offset + 2) And &HFF) << 8) Or
                             (hash(offset + 3) And &HFF)

            ' 6. Generate 6-digit code
            Dim otp = binaryCode Mod 1000000
            Return otp.ToString("D6")
        End Function

        ' Base32 decode (sem usar libs externas)
        Private Function Base32Decode(base32 As String) As Byte()
            Const alphabet As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567"
            Dim clean = base32.Replace("=", "").ToUpperInvariant()
            Dim buffer As Integer = 0
            Dim bitsLeft As Integer = 0
            Dim result As New List(Of Byte)

            For Each c As Char In clean
                If Not alphabet.Contains(c) Then Continue For
                buffer = (buffer << 5) Or alphabet.IndexOf(c)
                bitsLeft += 5
                If bitsLeft >= 8 Then
                    result.Add(CByte((buffer >> (bitsLeft - 8)) And &HFF))
                    bitsLeft -= 8
                End If
            Next

            Return result.ToArray()
        End Function
    End Class


End Namespace

