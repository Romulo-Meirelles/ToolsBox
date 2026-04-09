Namespace Utils
    Public Module CPF

        Public Function ValidarCPF(CPF As String) As Boolean

            For Each c As Char In "()[]\;,<>/""&*=%`~{}|^$-_."" """
                CPF = CPF.Replace(c.ToString(), "")
            Next

            CPF = New String(CPF.Where(AddressOf Char.IsDigit).ToArray())

            If CPF.Length <> 11 OrElse CPF.Distinct().Count() = 1 Then
                Return False
            End If

            Dim soma As Integer = 0
            For i As Integer = 0 To 8
                soma += CInt(CPF(i).ToString()) * (10 - i)
            Next

            Dim resto As Integer = (soma * 10) Mod 11
            If resto = 10 Then resto = 0
            If resto <> CInt(CPF(9).ToString()) Then Return False

            soma = 0
            For i As Integer = 0 To 9
                soma += CInt(CPF(i).ToString()) * (11 - i)
            Next

            resto = (soma * 10) Mod 11
            If resto = 10 Then resto = 0
            Return resto = CInt(CPF(10).ToString())
        End Function
    End Module
End Namespace

