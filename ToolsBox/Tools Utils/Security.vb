Imports System.Security.Cryptography
Imports System.Text
Namespace Utils
    Public Module Security
        Public Function AES_Decrypt(ByVal Input As String) As String
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
                Dim inputBuffer As Byte() = Convert.FromBase64String(Input)
                str = Encoding.ASCII.GetString(transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length))
                Return str
            Catch exception1 As Exception
                Return Nothing
            End Try
            Return str
        End Function
        Public Function AES_Encrypt(ByVal Input As String) As String
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
                Dim bytes As Byte() = Encoding.ASCII.GetBytes(Input)
                str = Convert.ToBase64String(transform.TransformFinalBlock(bytes, 0, bytes.Length))
                Return str
            Catch exception1 As Exception
                Return Nothing
            End Try
            Return str
        End Function

        Public Function AES_Decrypt(ByVal Input As String, ByVal Password As String, Optional Mode As CipherMode = CipherMode.ECB) As String
            Dim Pass As String = Password
            Dim str As String
            Dim managed As New RijndaelManaged
            Dim provider As New MD5CryptoServiceProvider
            Try
                Dim destinationArray As Byte() = New Byte(&H20 - 1) {}
                Dim sourceArray As Byte() = provider.ComputeHash(Encoding.ASCII.GetBytes(Pass))
                Array.Copy(sourceArray, 0, destinationArray, 0, &H10)
                Array.Copy(sourceArray, 0, destinationArray, 15, &H10)
                managed.Key = destinationArray
                managed.Mode = Mode
                Dim transform As ICryptoTransform = managed.CreateDecryptor
                Dim inputBuffer As Byte() = Convert.FromBase64String(Input)
                str = Encoding.ASCII.GetString(transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length))
                Return str
            Catch exception1 As Exception
                Return Nothing
            End Try
            Return str
        End Function
        Public Function AES_Encrypt(ByVal Input As String, ByVal Password As String, Optional Mode As CipherMode = CipherMode.ECB) As String
            Dim Pass As String = Password
            Dim str As String
            Dim managed As New RijndaelManaged
            Dim provider As New MD5CryptoServiceProvider
            Try
                Dim destinationArray As Byte() = New Byte(&H20 - 1) {}
                Dim sourceArray As Byte() = provider.ComputeHash(Encoding.ASCII.GetBytes(Pass))
                Array.Copy(sourceArray, 0, destinationArray, 0, &H10)
                Array.Copy(sourceArray, 0, destinationArray, 15, &H10)
                managed.Key = destinationArray
                managed.Mode = Mode
                Dim transform As ICryptoTransform = managed.CreateEncryptor
                Dim bytes As Byte() = Encoding.ASCII.GetBytes(Input)
                str = Convert.ToBase64String(transform.TransformFinalBlock(bytes, 0, bytes.Length))
                Return str
            Catch exception1 As Exception
                Return Nothing
            End Try
            Return str
        End Function
        Public Function AES_Encrypt_Bytes(ByVal Input As Byte()) As Byte()
            Dim Key As String = "⊟Шਟ⢢⠥ᑙᙟ❝⍽ᦳʄ⇬✏ൻ⤷ᘾ"
            Dim AES As New System.Security.Cryptography.RijndaelManaged
            Dim SHA256 As New System.Security.Cryptography.SHA256Cng
            Try
                AES.Key = SHA256.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(Key))
                AES.Mode = System.Security.Cryptography.CipherMode.ECB
                Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
                Dim Buffer As Byte() = Input
                Return DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length)
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
        Public Function AES_Decrypt_Bytes(ByVal Input As Byte()) As Byte()
            Dim Key As String = "⊟Шਟ⢢⠥ᑙᙟ❝⍽ᦳʄ⇬✏ൻ⤷ᘾ"
            Dim AES As New System.Security.Cryptography.RijndaelManaged
            Dim SHA256 As New System.Security.Cryptography.SHA256Cng
            Try
                AES.Key = SHA256.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(Key))
                AES.Mode = System.Security.Cryptography.CipherMode.ECB
                Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
                Dim Buffer As Byte() = Input
                Return DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length)
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Function AES_Encrypt_Bytes(ByVal Input As Byte(), ByVal Password As String, Optional Mode As CipherMode = CipherMode.ECB) As Byte()
            Dim Key As String = Password
            Dim AES As New System.Security.Cryptography.RijndaelManaged
            Dim SHA256 As New System.Security.Cryptography.SHA256Cng
            Try
                AES.Key = SHA256.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(Key))
                AES.Mode = Mode
                Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
                Dim Buffer As Byte() = Input
                Return DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length)
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
        Public Function AES_Decrypt_Bytes(ByVal Input As Byte(), ByVal Password As String, Optional Mode As CipherMode = CipherMode.ECB) As Byte()
            Dim Key As String = Password
            Dim AES As New System.Security.Cryptography.RijndaelManaged
            Dim SHA256 As New System.Security.Cryptography.SHA256Cng
            Try
                AES.Key = SHA256.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(Key))
                AES.Mode = Mode
                Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
                Dim Buffer As Byte() = Input
                Return DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length)
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
    End Module
End Namespace

