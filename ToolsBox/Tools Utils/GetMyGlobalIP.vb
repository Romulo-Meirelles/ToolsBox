Imports System.Net

Namespace Utils
    Public Module GetMyGlobalIP
        Public Function GetMyIP_Global() As String
            Dim IP As New WebClient
            Return IP.DownloadString("https://api.my-ip.io/ip")
        End Function
    End Module
End Namespace
