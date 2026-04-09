Imports System.Net
Imports System.Net.Http
Imports System.Threading.Tasks
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Namespace Utils

    Public Module CotacaoUOL

        Public Async Function UOLGetRawJsonCotationAsync(Currency_ As Currency_World, Optional BidValue As Boolean = False, Optional AskValue As Boolean = False, Optional MaxBid As Boolean = False, Optional MinBid As Boolean = False, Optional VariationBid As Boolean = False, Optional VariationPercentBid As Boolean = False, Optional OpenBidValue As Boolean = False, Optional Date_ As Boolean = False, Optional Size As Integer = 1) As Task(Of String)
            Try
                Dim Cotacao As New UOL
                Return Await Cotacao.GetRawJsonCotationAsync(Currency_, BidValue, AskValue, MaxBid, MinBid, VariationBid, VariationPercentBid, OpenBidValue, Date_, Size)
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try
        End Function
        Public Async Function UOLCotationAsync(Currency_ As Currency_World, Optional BidValue As Boolean = False, Optional AskValue As Boolean = False, Optional MaxBid As Boolean = False, Optional MinBid As Boolean = False, Optional VariationBid As Boolean = False, Optional VariationPercentBid As Boolean = False, Optional OpenBidValue As Boolean = False, Optional Date_ As Boolean = False, Optional Size As Integer = 1) As Task(Of CotacaoResponse)
            Try
                Dim Cotacao As New UOL
                Return Await Cotacao.CotationAsync(Currency_, BidValue, AskValue, MaxBid, MinBid, VariationBid, VariationPercentBid, OpenBidValue, Date_, Size)
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try
        End Function
        Public Enum Currency_World
            Dolar = 1
            Euro = 5
            Libra = 7
            India = 57
            Japao = 9
            Argentina = 11
            Canada = 27
            China = 35
            Suica = 117
            Russia = 109
            Noruega = 89
        End Enum

        Public Class CotacaoResponse
            <JsonProperty("prev")>
            Public Property Prev As String

            <JsonProperty("next")>
            Public Property [Next] As String

            <JsonProperty("docs")>
            Public Property Docs As New List(Of CotacaoItem)
            Public Class CotacaoItem
                <JsonProperty("bidvalue")>
                Public Property BidValue As Decimal

                <JsonProperty("askvalue")>
                Public Property AskValue As Decimal

                <JsonProperty("maxbid")>
                Public Property MaxBid As Decimal

                <JsonProperty("minbid")>
                Public Property MinBid As Decimal

                <JsonProperty("variationbid")>
                Public Property VariationBid As Decimal

                <JsonProperty("variationpercentbid")>
                Public Property VariationPercentBid As Decimal

                <JsonProperty("openbidvalue")>
                Public Property OpenBidValue As Decimal

                <JsonProperty("date")>
                Public Property [Date] As String
            End Class
        End Class
    End Module
    Friend Class UOL

        Private ReadOnly _http As HttpClient
        Private ReadOnly Site As String = "https://api.cotacoes.uol.com/currency/intraday/list/paged/?"
        Public Sub New()
            _http = New HttpClient()
            _http.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/124.0.0.0 Safari/537.36")
            _http.DefaultRequestHeaders.Add("Accept", "application/json, text/plain, */*")
            _http.DefaultRequestHeaders.Add("Referer", "https://economia.uol.com.br/")
        End Sub


        Protected Friend Async Function GetRawJsonCotationAsync(Currency_ As Currency_World, Optional BidValue As Boolean = False, Optional AskValue As Boolean = False, Optional MaxBid As Boolean = False, Optional MinBid As Boolean = False, Optional VariationBid As Boolean = False, Optional VariationPercentBid As Boolean = False, Optional OpenBidValue As Boolean = False, Optional Date_ As Boolean = False, Optional Size As Integer = 1) As Task(Of String)

            Dim Currency_Now As String = String.Empty

            Select Case Currency_
                Case Currency_World.Dolar
                    Currency_Now = "1"
            End Select

            Dim Lista As New List(Of String)

            If BidValue = True Then
                Lista.Add("bidvalue")
            End If

            If AskValue = True Then
                Lista.Add("askvalue")
            End If

            If MaxBid = True Then
                Lista.Add("maxbid")
            End If

            If MinBid = True Then
                Lista.Add("minbid")
            End If

            If VariationBid = True Then
                Lista.Add("variationbid")
            End If

            If VariationPercentBid = True Then
                Lista.Add("variationpercentbid")
            End If

            If OpenBidValue = True Then
                Lista.Add("openbidvalue")
            End If

            If Date_ = True Then
                Lista.Add("date")
            End If

            Dim Complemento As String = ""

            For I = 0 To Lista.Count - 1
                If I = Lista.Count Then
                    Complemento += Lista.Item(I).ToString
                Else
                    Complemento += Lista.Item(I).ToString & ","
                End If
            Next

            Dim Payload As String = Site & "format=JSON&fields=" & Complemento & "&currency=" & Currency_ & "&size=" & Size

            Dim response = Await _http.GetAsync(Payload)
            Dim content = Await response.Content.ReadAsStringAsync()


            If Not response.IsSuccessStatusCode Then
                Throw New Exception($"Erro HTTP {response.StatusCode}: {content}")
            End If

            Return content
        End Function

        Protected Friend Async Function CotationAsync(Currency_ As Currency_World, Optional BidValue As Boolean = False, Optional AskValue As Boolean = False, Optional MaxBid As Boolean = False, Optional MinBid As Boolean = False, Optional VariationBid As Boolean = False, Optional VariationPercentBid As Boolean = False, Optional OpenBidValue As Boolean = False, Optional Date_ As Boolean = False, Optional Size As Integer = 1) As Task(Of CotacaoResponse)

            Dim Currency_Now As String = String.Empty

            Select Case Currency_
                Case Currency_World.Dolar
                    Currency_Now = "1"
            End Select

            Dim Lista As New List(Of String)

            If BidValue = True Then
                Lista.Add("bidvalue")
            End If

            If AskValue = True Then
                Lista.Add("askvalue")
            End If

            If MaxBid = True Then
                Lista.Add("maxbid")
            End If

            If MinBid = True Then
                Lista.Add("minbid")
            End If

            If VariationBid = True Then
                Lista.Add("variationbid")
            End If

            If VariationPercentBid = True Then
                Lista.Add("variationpercentbid")
            End If

            If OpenBidValue = True Then
                Lista.Add("openbidvalue")
            End If

            If Date_ = True Then
                Lista.Add("date")
            End If

            Dim Complemento As String = ""

            For I = 0 To Lista.Count - 1
                If I = Lista.Count Then
                    Complemento += Lista.Item(I).ToString
                Else
                    Complemento += Lista.Item(I).ToString & ","
                End If
            Next

            Dim Payload As String = Site & "format=JSON&fields=" & Complemento & "&currency=" & Currency_ & "&size=" & Size

            Dim response = Await _http.GetAsync(Payload)
            Dim content = Await response.Content.ReadAsStringAsync()

            Dim Resultado As CotacaoResponse = JsonConvert.DeserializeObject(Of CotacaoResponse)(content)

            If Not response.IsSuccessStatusCode Then
                Throw New Exception($"Erro HTTP {response.StatusCode}: {content}")
            End If

            Return Resultado
        End Function


    End Class
End Namespace



