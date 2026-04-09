Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports Newtonsoft.Json
Imports QRCoder

Namespace Utils
    Public Module Pix

        Enum Method
            CELULAR
            CPF_CNPJ
            EMAIL
            LEATORIA
            AUTOMATICO
            NONE
        End Enum

#Region "IMAGE GENERATOR"
        Public Function PixGerarQRCodeImage(Chave As String, Optional Nome As String = "N", Optional Cidade As String = "C", Optional Identificacao As String = "***", Optional Valor As String = "", Optional Comentario As String = "", Optional ForcarMetodo As Method = Method.AUTOMATICO, Optional QRSize As Int32 = 10) As Image
            Dim payload As String = PixGerarPayloadPix(Chave, Nome, Cidade, Identificacao, Valor, Comentario, ForcarMetodo)

            Dim qrGenerator As New QRCodeGenerator()
            Dim qrData As QRCodeData = qrGenerator.CreateQrCode(payload.Trim(), QRCodeGenerator.ECCLevel.Default)
            Dim qrCode As New QRCode(qrData)

            Return qrCode.GetGraphic(QRSize)
        End Function
        Public Function PixGerarQRCodeImage(Chave As String, Optional Nome As String = "N", Optional Cidade As String = "C", Optional Identificacao As String = "***", Optional Valor As Double = 0, Optional Comentario As String = "", Optional ForcarMetodo As Method = Method.AUTOMATICO, Optional QRSize As Int32 = 10) As Image
            Dim Vlr As String = Valor.ToString("F2").Replace(",", ".")
            Dim payload As String = PixGerarPayloadPix(Chave, Nome, Cidade, Identificacao, Valor, Comentario, ForcarMetodo)

            Dim qrGenerator As New QRCodeGenerator()
            Dim qrData As QRCodeData = qrGenerator.CreateQrCode(payload.Trim(), QRCodeGenerator.ECCLevel.Default)
            Dim qrCode As New QRCode(qrData)

            Return qrCode.GetGraphic(QRSize)
        End Function
        Public Function PixGerarQRCodeImage(PixPayload As PixPayload, Optional QRSize As Int32 = 10) As Image
            Dim payload As String = PixGerarPayloadPix(PixPayload.PixKey, PixPayload.Nome, PixPayload.Cidade, PixPayload.TxId, PixPayload.Valor, PixPayload.Comentario, PixPayload.Metodo)

            Dim qrGenerator As New QRCodeGenerator()
            Dim qrData As QRCodeData = qrGenerator.CreateQrCode(payload.Trim(), QRCodeGenerator.ECCLevel.Default)
            Dim qrCode As New QRCode(qrData)

            Return qrCode.GetGraphic(QRSize)
        End Function
#End Region

#Region "QRCODE GENERATOR"
        Public Function PixGerarQRCode(Chave As String, Optional Nome As String = "N", Optional Cidade As String = "C", Optional Identificacao As String = "***", Optional Valor As String = "", Optional Comentario As String = "", Optional ForcarMetodo As Method = Method.AUTOMATICO) As String
            Return PixGerarPayloadPix(Chave, Nome, Cidade, Identificacao, Valor, Comentario, ForcarMetodo)
        End Function
        Public Function PixGerarQRCode(Chave As String, Optional Nome As String = "N", Optional Cidade As String = "C", Optional Identificacao As String = "***", Optional Valor As Double = 0, Optional Comentario As String = "", Optional ForcarMetodo As Method = Method.AUTOMATICO) As String
            Return PixGerarPayloadPix(Chave, Nome, Cidade, Identificacao, Valor.ToString("0.00", Globalization.CultureInfo.InvariantCulture), Comentario, ForcarMetodo)
        End Function
        Public Function PixGerarQRCode(PixPayload As PixPayload) As String
            Return PixGerarPayloadPix(PixPayload.PixKey, PixPayload.Nome, PixPayload.Cidade, PixPayload.TxId, PixPayload.Valor, PixPayload.Comentario, PixPayload.Metodo)
        End Function
#End Region

#Region "TOOLS"
        Public Function DeserializeQRCode(EMV As String) As PixEmvPayload

            Dim pos As Integer = 0
            Dim pix As New PixEmvPayload()

            While pos < EMV.Length
                Dim tag As String = EMV.Substring(pos, 2)
                Dim len As Integer = Integer.Parse(EMV.Substring(pos + 2, 2))
                Dim value As String = EMV.Substring(pos + 4, len)

                Select Case tag
                    Case "00"
                        pix.PayloadFormatIndicator = value

                    Case "26"
                        pix.MerchantAccountInfo = ParseMerchantAccount(value)

                    Case "52"
                        pix.MerchantCategoryCode = value

                    Case "53"
                        pix.TransactionCurrency = value

                    Case "54"
                        pix.TransactionAmount = Decimal.Parse(value.Replace(".", ","))

                    Case "58"
                        pix.CountryCode = value

                    Case "59"
                        pix.MerchantName = value

                    Case "60"
                        pix.MerchantCity = value.Trim()

                    Case "62"
                        pix.AdditionalData = ParseAdditionalData(value)

                    Case "63"
                        pix.CRC = value
                End Select

                pos += 4 + len
            End While

            Return pix

        End Function
        Public Function ExtractQRCodeToJson(QRCode As PixEmvPayload) As String
            Return JsonConvert.SerializeObject(QRCode, Formatting.Indented)
        End Function
        Public Function ExtractQRCodeToJson(QRCode As String) As String
            Dim pixObj = DeserializeQRCode(QRCode)
            Return JsonConvert.SerializeObject(pixObj, Formatting.Indented)
        End Function
#End Region

#Region "CLASSES"
        Public Class PixPayload
            Public Property PixKey As String
            Public Property Nome As String = "N"
            Public Property Cidade As String = "C"
            Public Property TxId As String = "***"
            Public Property Valor As String = "0"
            Public Property Comentario As String = ""
            Public Property Metodo As Method = Method.AUTOMATICO
        End Class
        Public Class PixEmvPayload

            Public Property PayloadFormatIndicator As String          ' 00
            Public Property MerchantAccountInfo As MerchantAccount    ' 26
            Public Property MerchantCategoryCode As String            ' 52
            Public Property TransactionCurrency As String             ' 53
            Public Property TransactionAmount As Decimal?             ' 54
            Public Property CountryCode As String                      ' 58
            Public Property MerchantName As String                     ' 59
            Public Property MerchantCity As String                     ' 60
            Public Property AdditionalData As AdditionalDataTemplate  ' 62
            Public Property CRC As String                              ' 63

        End Class
        Public Class MerchantAccount
            Public Property GUI As String
            Public Property PixKey As String
            Public Property Message As String   ' 02 ← descrição
        End Class
        Public Class AdditionalDataTemplate
            Public Property TxId As String      ' 05
            Public Property Purpose As String   ' 08 (opcional)
        End Class
#End Region

        Private Function PixGerarPayloadPix(Chave As String, Optional Nome As String = "N", Optional Cidade As String = "C", Optional Identificacao As String = "***", Optional Valor As String = "", Optional Comentario As String = "", Optional ForcarMetodo As Method = Method.AUTOMATICO) As String

            Nome = LimparTextoCompleto(Nome)
            Cidade = LimparTextoCompleto(Cidade)
            Identificacao = LimparTextoEstritoRemoveEspaco(Identificacao)
            Comentario = LimparTextoCompleto(Comentario)

            Select Case ForcarMetodo
                Case Method.AUTOMATICO

                    If Chave.Contains("@") Then 'Chave Email

                        Chave = Chave.ToLower

                    ElseIf Regex.IsMatch(Chave, "^[A-Za-z0-9]{8}-") Then 'Chava Aleatoria
                        For Each c As Char In "()[]\;,<>/""&*=%`~{}|^$"
                            Chave = Chave.Replace(c.ToString(), "")
                        Next


                    ElseIf ValidarCPF(Chave) = True Then 'Chave CPF
                        For Each c As Char In "()[]\;,<>/""&*=%`~{}|^$-_."" """
                            Chave = Chave.Replace(c.ToString(), "")
                        Next

                    ElseIf ValidarCNPJ(Chave) = True Then 'Chave CNPJ
                        For Each c As Char In "()[]\;,<>/""&*=%`~{}|^$-_."" """
                            Chave = Chave.Replace(c.ToString(), "")
                        Next

                    Else 'Chave Telefone

                        For Each c As Char In "()[]\;,<>/""&*=%`~{}|^$-_."" """
                            Chave = Chave.Replace(c.ToString(), "")
                        Next

                        If String.IsNullOrWhiteSpace(Chave) Then
                            Chave = " (Invalid Pix) "
                        End If

                        Chave = Chave.Trim()

                        ' Remove espaços e caracteres invisíveis
                        Chave = Chave.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "")

                        ' Caso 1: já começa com +55
                        If Chave.StartsWith("+55") Then
                            ' ok, já está no padrão
                            ' Caso 2: começa com 55
                        ElseIf Chave.StartsWith("55") Then
                            Chave = "+" & Chave
                            ' Caso 3: começa com número normal (DDD + número)
                        ElseIf IsNumeric(Chave) Then
                            Chave = "+55" & Chave
                        Else
                            Chave = " (Invalid Pix) "
                        End If

                        ' Remove qualquer coisa que não seja + ou número
                        For Each c As Char In Chave
                            If Not (Char.IsDigit(c) OrElse c = "+"c) Then
                                Chave = " (Invalid Pix) "
                            End If
                        Next

                        ' Limite Pix (máx 15 caracteres)
                        If Chave.Length > 15 Then
                            Chave = Chave.Substring(0, 15)
                        End If

                    End If

                Case Method.CELULAR
                    For Each c As Char In "()[]\;,<>/""&*=%`~{}|^$-_."" """
                        Chave = Chave.Replace(c.ToString(), "")
                    Next

                    If String.IsNullOrWhiteSpace(Chave) Then
                        Chave = " (Invalid Pix) "
                    End If

                    Chave = Chave.Trim()

                    ' Remove espaços e caracteres invisíveis
                    Chave = Chave.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "")

                    ' Caso 1: já começa com +55
                    If Chave.StartsWith("+55") Then
                        ' ok, já está no padrão
                        ' Caso 2: começa com 55
                    ElseIf Chave.StartsWith("55") Then
                        Chave = "+" & Chave
                        ' Caso 3: começa com número normal (DDD + número)
                    ElseIf IsNumeric(Chave) Then
                        Chave = "+55" & Chave
                    Else
                        Chave = " (Invalid Pix) "
                    End If

                    ' Remove qualquer coisa que não seja + ou número
                    For Each c As Char In Chave
                        If Not (Char.IsDigit(c) OrElse c = "+"c) Then
                            Chave = " (Invalid Pix) "
                        End If
                    Next

                    ' Limite Pix (máx 15 caracteres)
                    If Chave.Length > 15 Then
                        Chave = Chave.Substring(0, 15)
                    End If

                Case Method.CPF_CNPJ
                    For Each c As Char In "()[]\;,<>/""&*=%`~{}|^$-_."" """
                        Chave = Chave.Replace(c.ToString(), "")
                    Next
                    If ValidarCPF(Chave) = False Then
                        If ValidarCNPJ(Chave) = False Then
                            Chave = "InvalidPix"
                        End If
                    End If

                Case Method.EMAIL
                    Chave = Chave.ToLower
                    If Not Chave.Contains("@") Then
                        Chave = "InvalidPix"
                    End If

                Case Method.LEATORIA
                    For Each c As Char In "()[]\;,<>/""&*=%`~{}|^$"
                        Chave = Chave.Replace(c.ToString(), "")
                    Next

                Case Method.NONE

            End Select


            ' Padroniza entrada
            Nome = Nome.Trim().ToUpper().PadRight(25).Substring(0, 25)
            Cidade = Cidade.Trim().ToUpper().PadRight(15).Substring(0, 15)

            ' Elementos do payload
            Dim payload As New StringBuilder()

            payload.Append("000201") ' Payload format
            Dim merchantInfo As New StringBuilder()

            merchantInfo.Append("00").Append("br.gov.bcb.pix".Length.ToString("D2")).Append("br.gov.bcb.pix")
            merchantInfo.Append("01").Append(Chave.Length.ToString("D2")).Append(Chave)

            ' >>> MENSAGEM TEM QUE ENTRAR AQUI <<<
            If Not String.IsNullOrWhiteSpace(Comentario) Then
                Dim msg As String = Comentario.Trim()
                merchantInfo.Append("02").Append(msg.Length.ToString("D2")).Append(msg)
            End If

            Dim campo26 = merchantInfo.ToString()
            payload.Append("26").Append(campo26.Length.ToString("D2")).Append(campo26)

            payload.Append("52040000") ' Merchant category code
            payload.Append("5303986")  ' Currency: 986 = BRL

            If Not String.IsNullOrWhiteSpace(Valor) Then
                Valor = Valor.Replace(",", ".")

                Dim v As Decimal
                If Decimal.TryParse(Valor, Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture, v) Then
                    ' CORTA para 2 casas, não arredonda
                    v = Math.Truncate(v * 100D) / 100D

                    If v > 0D Then
                        Valor = v.ToString("0.00", Globalization.CultureInfo.InvariantCulture)
                        payload.Append("54").Append(Valor.Length.ToString("D2")).Append(Valor)
                    End If
                End If
            End If

            payload.Append("5802BR") ' País
            Nome = Nome.Trim().ToUpper()
            If Nome.Length > 25 Then
                Nome = Nome.Substring(0, 25)
            End If
            payload.Append("59").Append(Nome.Trim().Length.ToString("D2")).Append(Nome.Trim()) ' Nome
            payload.Append("60").Append(Cidade.Trim().Length.ToString("D2")).Append(Cidade.Trim()) ' Cidade

            If Not Identificacao = "" Or Identificacao = "***" Then
                Identificacao = Identificacao.Replace(" ", "")
                For Each c As Char In "()[]\;,<>/""&=%`~{}|^$-_."" """
                    Identificacao = Identificacao.Replace(c.ToString(), "")
                Next
            Else
                Identificacao = "***"
            End If


            Dim campo62 As New StringBuilder()
            campo62.Append("05").Append(Identificacao.Length.ToString("D2")).Append(Identificacao)

            payload.Append("62").Append(campo62.Length.ToString("D2")).Append(campo62.ToString())

            ' Gera CRC16
            payload.Append("6304")
            Dim crc = CalcularCRC16(payload.ToString())
            payload.Append(crc)

            Return payload.ToString()
        End Function
        Private Function ParseMerchantAccount(data As String) As MerchantAccount

            Dim pos As Integer = 0
            Dim acc As New MerchantAccount()

            While pos < data.Length
                Dim tag = data.Substring(pos, 2)
                Dim len = Integer.Parse(data.Substring(pos + 2, 2))
                Dim value = data.Substring(pos + 4, len)

                Select Case tag
                    Case "00"
                        acc.GUI = value
                    Case "01"
                        acc.PixKey = value
                    Case "02"
                        acc.Message = value
                End Select

                pos += 4 + len
            End While

            Return acc

        End Function
        Private Function ParseAdditionalData(data As String) As AdditionalDataTemplate

            Dim pos As Integer = 0
            Dim ad As New AdditionalDataTemplate()

            While pos < data.Length
                Dim tag = data.Substring(pos, 2)
                Dim len = Integer.Parse(data.Substring(pos + 2, 2))
                Dim value = data.Substring(pos + 4, len)

                Select Case tag
                    Case "05"
                        ad.TxId = value
                    Case "08"
                        ad.Purpose = value
                End Select

                pos += 4 + len
            End While

            Return ad

        End Function
        Private Function CalcularCRC16(payload As String) As String
            Dim polinomio As UShort = &H1021
            Dim resultado As UShort = &HFFFF

            Dim bytes() As Byte = Encoding.ASCII.GetBytes(payload)

            For Each b In bytes
                resultado = CUInt(resultado Xor (CUInt(b) << 8))

                For i = 0 To 7
                    If (resultado And &H8000) <> 0 Then
                        resultado = CUInt((resultado << 1) Xor polinomio)
                    Else
                        resultado <<= 1
                    End If
                    resultado = CUInt(resultado And &HFFFF)
                Next
            Next

            Return resultado.ToString("X4")
        End Function
        Private Function RemoverAcentos(texto As String) As String
            If String.IsNullOrEmpty(texto) Then
                Return texto
            End If

            Dim textoNormalizado As String = texto.Normalize(NormalizationForm.FormD)
            Dim sb As New StringBuilder()

            For Each c As Char In textoNormalizado
                If CharUnicodeInfo.GetUnicodeCategory(c) <> UnicodeCategory.NonSpacingMark Then
                    sb.Append(c)
                End If
            Next

            Return sb.ToString().Normalize(NormalizationForm.FormC)
        End Function
        Private Function LimparTextoEstritoRemoveEspaco(texto As String) As String
            If String.IsNullOrWhiteSpace(texto) Then
                Return String.Empty
            End If

            ' Remove acentos
            Dim normalizado As String = texto.Normalize(NormalizationForm.FormD)
            Dim sb As New StringBuilder()

            For Each c As Char In normalizado
                If CharUnicodeInfo.GetUnicodeCategory(c) <> UnicodeCategory.NonSpacingMark Then
                    sb.Append(c)
                End If
            Next

            Dim semAcento As String = sb.ToString().Normalize(NormalizationForm.FormC)

            ' Remove tudo que não for letra ou número (inclusive espaço)
            Dim limpo As String = Regex.Replace(semAcento, "[^a-zA-Z0-9]", "")

            Return limpo
        End Function
        Public Function LimparTextoCompleto(texto As String) As String
            If String.IsNullOrWhiteSpace(texto) Then
                Return texto
            End If

            ' 1 - Remove acentos
            Dim normalizado As String = texto.Normalize(NormalizationForm.FormD)
            Dim sb As New StringBuilder()

            For Each c As Char In normalizado
                If CharUnicodeInfo.GetUnicodeCategory(c) <> UnicodeCategory.NonSpacingMark Then
                    sb.Append(c)
                End If
            Next

            Dim semAcento As String = sb.ToString().Normalize(NormalizationForm.FormC)

            ' 2 - Remove tudo que não for letra, número ou espaço
            Dim limpo As String = Regex.Replace(semAcento, "[^a-zA-Z0-9 ]", "")

            Return limpo
        End Function
    End Module
End Namespace
