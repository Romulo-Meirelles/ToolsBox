Imports System.Security.Cryptography
Imports System.Text
Namespace Utils
    Public Class Security
        Shared Function AES_Decrypt(ByVal input As String) As String
            Dim Pass As String = "Ɇ♍ᐋ⩮☾Ⓝⵗ⋩ǁ−⋗ᙔᔳથ╪⇈"
            Dim str As String
            Dim managed As New RijndaelManaged
            Dim provider As New MD5CryptoServiceProvider
            Try
                Dim destinationArray As Byte() = New Byte(&H20 - 1) {}
                Dim sourceArray As Byte() = provider.ComputeHash(Encoding.ASCII.GetBytes(Pass))
                Array.Copy(sourceArray, 0, destinationArray, 0, &H10)
                Array.Copy(sourceArray, 0, destinationArray, 15, &H10)
                managed.Key = destinationArray
                managed.Mode = CipherMode.ECB
                Dim transform As ICryptoTransform = managed.CreateDecryptor
                Dim inputBuffer As Byte() = Convert.FromBase64String(input)
                str = Encoding.ASCII.GetString(transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length))
                Return str
            Catch exception1 As Exception
                Return Nothing
            End Try
            Return str
        End Function
        Shared Function AES_Encrypt(ByVal input As String) As String
            Dim Pass As String = "Ɇ♍ᐋ⩮☾Ⓝⵗ⋩ǁ−⋗ᙔᔳથ╪⇈"
            Dim str As String
            Dim managed As New RijndaelManaged
            Dim provider As New MD5CryptoServiceProvider
            Try
                Dim destinationArray As Byte() = New Byte(&H20 - 1) {}
                Dim sourceArray As Byte() = provider.ComputeHash(Encoding.ASCII.GetBytes(Pass))
                Array.Copy(sourceArray, 0, destinationArray, 0, &H10)
                Array.Copy(sourceArray, 0, destinationArray, 15, &H10)
                managed.Key = destinationArray
                managed.Mode = CipherMode.ECB
                Dim transform As ICryptoTransform = managed.CreateEncryptor
                Dim bytes As Byte() = Encoding.ASCII.GetBytes(input)
                str = Convert.ToBase64String(transform.TransformFinalBlock(bytes, 0, bytes.Length))
                Return str
            Catch exception1 As Exception
                Return Nothing
            End Try
            Return str
        End Function
        Shared Function AES_Encrypt_Bytes(ByVal input As Byte()) As Byte()
            Dim Key As String = "⊟Шਟ⢢⠥ᑙᙟ❝⍽ᦳʄ⇬✏ൻ⤷ᘾ"
            Dim AES As New System.Security.Cryptography.RijndaelManaged
            Dim SHA256 As New System.Security.Cryptography.SHA256Cng
            Try
                AES.Key = SHA256.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(Key))
                AES.Mode = System.Security.Cryptography.CipherMode.ECB
                Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
                Dim Buffer As Byte() = input
                Return DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length)
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
        Shared Function AES_Decrypt_Bytes(ByVal input As Byte()) As Byte()
            Dim Key As String = "⊟Шਟ⢢⠥ᑙᙟ❝⍽ᦳʄ⇬✏ൻ⤷ᘾ"
            Dim AES As New System.Security.Cryptography.RijndaelManaged
            Dim SHA256 As New System.Security.Cryptography.SHA256Cng
            Try
                AES.Key = SHA256.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(Key))
                AES.Mode = System.Security.Cryptography.CipherMode.ECB
                Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
                Dim Buffer As Byte() = input
                Return DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length)
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
    End Class
End Namespace
