Imports System.Security.Cryptography
Imports System.Text
Namespace Utils
    Public Module Security
        Public Function AES_Encrypt(ByVal Input As String) As String
            Dim Pass As String = "Ɇ♍ᐋ⩮☾Ⓝⵗ⋩ǁ−⋗ᙔᔳથ╪⇈"

            Using sha256 = System.Security.Cryptography.SHA256.Create(),
          aes = System.Security.Cryptography.Aes.Create()

                ' chave 32 bytes
                aes.Key = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Pass))
                aes.Mode = System.Security.Cryptography.CipherMode.CBC
                aes.Padding = System.Security.Cryptography.PaddingMode.PKCS7
                aes.GenerateIV()

                Dim inputBytes = System.Text.Encoding.UTF8.GetBytes(Input)

                Using encryptor = aes.CreateEncryptor()
                    Dim cipher = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length)

                    ' [ IV | CIPHER ]
                    Dim result(aes.IV.Length + cipher.Length - 1) As Byte
                    Buffer.BlockCopy(aes.IV, 0, result, 0, aes.IV.Length)
                    Buffer.BlockCopy(cipher, 0, result, aes.IV.Length, cipher.Length)

                    Return Convert.ToBase64String(result)
                End Using
            End Using
        End Function
        Public Function AES_Decrypt(ByVal Input As String) As String
            Dim Pass As String = "Ɇ♍ᐋ⩮☾Ⓝⵗ⋩ǁ−⋗ᙔᔳથ╪⇈"

            Dim allBytes = Convert.FromBase64String(Input)

            Using sha256 = System.Security.Cryptography.SHA256.Create(),
          aes = System.Security.Cryptography.Aes.Create()

                aes.Key = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Pass))
                aes.Mode = System.Security.Cryptography.CipherMode.CBC
                aes.Padding = System.Security.Cryptography.PaddingMode.PKCS7

                ' IV = primeiros 16 bytes
                Dim iv(15) As Byte
                Buffer.BlockCopy(allBytes, 0, iv, 0, iv.Length)
                aes.IV = iv

                ' dados criptografados
                Dim cipherLen = allBytes.Length - iv.Length
                Dim cipher(cipherLen - 1) As Byte
                Buffer.BlockCopy(allBytes, iv.Length, cipher, 0, cipherLen)

                Using decryptor = aes.CreateDecryptor()
                    Dim plainBytes = decryptor.TransformFinalBlock(cipher, 0, cipher.Length)
                    Return System.Text.Encoding.UTF8.GetString(plainBytes)
                End Using
            End Using
        End Function


        Public Function AES_Encrypt(ByVal Input As String, Password As String) As String

            Using sha256 = System.Security.Cryptography.SHA256.Create(),
          aes = System.Security.Cryptography.Aes.Create()

                ' chave 32 bytes
                aes.Key = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password))
                aes.Mode = System.Security.Cryptography.CipherMode.CBC
                aes.Padding = System.Security.Cryptography.PaddingMode.PKCS7
                aes.GenerateIV()

                Dim inputBytes = System.Text.Encoding.UTF8.GetBytes(Input)

                Using encryptor = aes.CreateEncryptor()
                    Dim cipher = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length)

                    ' [ IV | CIPHER ]
                    Dim result(aes.IV.Length + cipher.Length - 1) As Byte
                    Buffer.BlockCopy(aes.IV, 0, result, 0, aes.IV.Length)
                    Buffer.BlockCopy(cipher, 0, result, aes.IV.Length, cipher.Length)

                    Return Convert.ToBase64String(result)
                End Using
            End Using
        End Function

        Public Function AES_Decrypt(ByVal Input As String, Password As String) As String

            Dim allBytes = Convert.FromBase64String(Input)

            Using sha256 = System.Security.Cryptography.SHA256.Create(),
          aes = System.Security.Cryptography.Aes.Create()

                aes.Key = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password))
                aes.Mode = System.Security.Cryptography.CipherMode.CBC
                aes.Padding = System.Security.Cryptography.PaddingMode.PKCS7

                ' IV = primeiros 16 bytes
                Dim iv(15) As Byte
                Buffer.BlockCopy(allBytes, 0, iv, 0, iv.Length)
                aes.IV = iv

                ' dados criptografados
                Dim cipherLen = allBytes.Length - iv.Length
                Dim cipher(cipherLen - 1) As Byte
                Buffer.BlockCopy(allBytes, iv.Length, cipher, 0, cipherLen)

                Using decryptor = aes.CreateDecryptor()
                    Dim plainBytes = decryptor.TransformFinalBlock(cipher, 0, cipher.Length)
                    Return System.Text.Encoding.UTF8.GetString(plainBytes)
                End Using
            End Using
        End Function
        Public Function AES_Encrypt_Bytes(ByVal Input As Byte()) As Byte()
            Dim Key As String = "⊟Шਟ⢢⠥ᑙᙟ❝⍽ᦳʄ⇬✏ൻ⤷ᘾ"

            Using sha256 = System.Security.Cryptography.SHA256.Create(),
          aes = System.Security.Cryptography.Aes.Create()

                aes.Key = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Key))
                aes.Mode = System.Security.Cryptography.CipherMode.CBC
                aes.Padding = System.Security.Cryptography.PaddingMode.PKCS7
                aes.GenerateIV() ' obrigatório

                Using encryptor = aes.CreateEncryptor()
                    Dim encrypted = encryptor.TransformFinalBlock(Input, 0, Input.Length)

                    ' concatena IV + dados (padrão antigo e correto)
                    Dim result(aes.IV.Length + encrypted.Length - 1) As Byte
                    Buffer.BlockCopy(aes.IV, 0, result, 0, aes.IV.Length)
                    Buffer.BlockCopy(encrypted, 0, result, aes.IV.Length, encrypted.Length)

                    Return result
                End Using
            End Using
        End Function

        Public Function AES_Decrypt_Bytes(ByVal Input As Byte()) As Byte()
            Dim Key As String = "⊟Шਟ⢢⠥ᑙᙟ❝⍽ᦳʄ⇬✏ൻ⤷ᘾ"

            Using sha256 As System.Security.Cryptography.SHA256 = System.Security.Cryptography.SHA256.Create(),
          aes As System.Security.Cryptography.Aes = System.Security.Cryptography.Aes.Create()

                ' chave (32 bytes)
                aes.Key = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Key))

                aes.Mode = System.Security.Cryptography.CipherMode.CBC
                aes.Padding = System.Security.Cryptography.PaddingMode.PKCS7

                ' IV explícito (primeiros 16 bytes)
                Dim iv(15) As Byte
                Buffer.BlockCopy(Input, 0, iv, 0, iv.Length)
                aes.IV = iv

                ' ciphertext (resto)
                Dim cipherLen As Integer = Input.Length - iv.Length
                Dim cipher(cipherLen - 1) As Byte
                Buffer.BlockCopy(Input, iv.Length, cipher, 0, cipherLen)

                Using decryptor As System.Security.Cryptography.ICryptoTransform = aes.CreateDecryptor()
                    Return decryptor.TransformFinalBlock(cipher, 0, cipher.Length)
                End Using
            End Using
        End Function
        Public Function AES_Encrypt_Bytes(ByVal Input As Byte(), Password As String) As Byte()


            Using sha256 = System.Security.Cryptography.SHA256.Create(),
          aes = System.Security.Cryptography.Aes.Create()

                aes.Key = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password))
                aes.Mode = System.Security.Cryptography.CipherMode.CBC
                aes.Padding = System.Security.Cryptography.PaddingMode.PKCS7
                aes.GenerateIV() ' obrigatório

                Using encryptor = aes.CreateEncryptor()
                    Dim encrypted = encryptor.TransformFinalBlock(Input, 0, Input.Length)

                    ' concatena IV + dados (padrão antigo e correto)
                    Dim result(aes.IV.Length + encrypted.Length - 1) As Byte
                    Buffer.BlockCopy(aes.IV, 0, result, 0, aes.IV.Length)
                    Buffer.BlockCopy(encrypted, 0, result, aes.IV.Length, encrypted.Length)

                    Return result
                End Using
            End Using
        End Function

        Public Function AES_Decrypt_Bytes(ByVal Input As Byte(), Password As String) As Byte()

            Using sha256 As System.Security.Cryptography.SHA256 = System.Security.Cryptography.SHA256.Create(),
          aes As System.Security.Cryptography.Aes = System.Security.Cryptography.Aes.Create()

                ' chave (32 bytes)
                aes.Key = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password))

                aes.Mode = System.Security.Cryptography.CipherMode.CBC
                aes.Padding = System.Security.Cryptography.PaddingMode.PKCS7

                ' IV explícito (primeiros 16 bytes)
                Dim iv(15) As Byte
                Buffer.BlockCopy(Input, 0, iv, 0, iv.Length)
                aes.IV = iv

                ' ciphertext (resto)
                Dim cipherLen As Integer = Input.Length - iv.Length
                Dim cipher(cipherLen - 1) As Byte
                Buffer.BlockCopy(Input, iv.Length, cipher, 0, cipherLen)

                Using decryptor As System.Security.Cryptography.ICryptoTransform = aes.CreateDecryptor()
                    Return decryptor.TransformFinalBlock(cipher, 0, cipher.Length)
                End Using
            End Using
        End Function
    End Module
End Namespace

