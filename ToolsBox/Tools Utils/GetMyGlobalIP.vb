Imports System.Net
Imports System.Threading.Tasks

Namespace Utils
    Public Module GetMyGlobalIP
        Public Async Function GetMyIP_GlobalAsync() As Task(Of String)
            Using client As New Net.Http.HttpClient()
                Return Await client.GetStringAsync("https://api.my-ip.io/v1/ip")
            End Using
        End Function
        Public Async Function GetMyIP_Globa_CompletelAsync() As Task(Of String)
            Using client As New Net.Http.HttpClient()
                Return Await client.GetStringAsync("https://api.my-ip.io/v2/ip")
            End Using
        End Function
    End Module
End Namespace

