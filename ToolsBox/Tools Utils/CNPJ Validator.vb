Namespace Utils
    Public Module CNPJ

        Public Function ValidarCNPJ(CNPJ As String) As Boolean

            For Each c As Char In "()[]\;,<>/""&*=%`~{}|^$-_."" """
                CNPJ = CNPJ.Replace(c.ToString(), "")
            Next

            CNPJ = New String(CNPJ.Where(AddressOf Char.IsDigit).ToArray())

            If CNPJ.Length <> 14 OrElse CNPJ.Distinct().Count() = 1 Then
                Return False
            End If

            Dim pesos1() As Integer = {5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2}
            Dim pesos2() As Integer = {6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2}

            Dim soma As Integer = 0
            For i As Integer = 0 To 11
                soma += CInt(CNPJ(i).ToString()) * pesos1(i)
            Next

            Dim resto As Integer = soma Mod 11
            Dim digito1 As Integer = If(resto < 2, 0, 11 - resto)
            If digito1 <> CInt(CNPJ(12).ToString()) Then Return False

            soma = 0
            For i As Integer = 0 To 12
                soma += CInt(CNPJ(i).ToString()) * pesos2(i)
            Next

            resto = soma Mod 11
            Dim digito2 As Integer = If(resto < 2, 0, 11 - resto)
            Return digito2 = CInt(CNPJ(13).ToString())
        End Function

    End Module
End Namespace

