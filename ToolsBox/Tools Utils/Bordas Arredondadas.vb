Namespace Utils
    Public Module Bordas_Arredondadas
        Private Declare Function CreateRoundRectRgn Lib "Gdi32" (ByVal X1 As Integer, ByVal Y1 As Integer, ByVal X2 As Integer, ByVal Y2 As Integer, ByVal X3 As Integer, ByVal Y3 As Integer) As Integer

        'DECLARAÇÃO REGIONHANDLE PARA BORDAS
        Friend regionHandle As IntPtr

        'FUNÇÃO QUE CRIA BORDAS NOS OBJETOS, MODIFIQUE O 20, 20 PARA O TAMANHO DAS BORDAS.
        Public Sub CarregaBordas(ByVal Componente As Object, Optional ByVal Width As Integer = 20, Optional ByVal Height As Integer = 20)
            regionHandle = New IntPtr(CreateRoundRectRgn(0, 0, Componente.Width, Componente.Height, Width, Height))
            Componente.Region = Region.FromHrgn(regionHandle)
            Componente.Region.ReleaseHrgn(regionHandle)
        End Sub
    End Module
End Namespace
