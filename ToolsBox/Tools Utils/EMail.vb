Imports System.Net.Mail

Namespace Utils
    Public Class EMails
        Public Property DefaultCredentials As Boolean = False
        Public Property Port As Integer = 587
        Public Property SLL As Boolean = True
        Public Property IsBodyHtml As Boolean = False
        Private UserName As String
        Private UserPassword As String
        Private UserSMTP As String
        Sub New(ByVal Name As String, ByVal Password As String, Optional ByVal SMTP As String = "")

            Dim Server As String
            If SMTP = "" Then
                If Name.Contains("@gmail.com") Then
                    Server = "smtp.gmail.com"
                ElseIf Name.Contains("@live.com") Then
                    Server = "SMTP.office365.com"
                ElseIf Name.Contains("@hotmail.com") Then
                    Server = "SMTP.office365.com"
                ElseIf Name.Contains("@outlook.com") Or Name.Contains("@outlook.com.br") Then
                    Server = "SMTP.office365.com"
                ElseIf Name.Contains("@yahoo.com") Or Name.Contains("@yahoo.com.br") Then
                    Server = "smtp.mail.yahoo.com"
                ElseIf Name.Contains("@ig.com") Or Name.Contains("@ig.com.br") Then
                    Server = "smtp.ig.com.br"
                End If
            Else
                Server = SMTP
            End If

            UserName = Name
            UserPassword = Password
            UserSMTP = Server
        End Sub
        Public Function Send(ByVal Title As String, ByVal Message As String, ByVal Too As String) As Boolean
            Try
                Dim Smtp_Server As New SmtpClient
                Dim e_mail As New MailMessage()
                Smtp_Server.UseDefaultCredentials = DefaultCredentials
                Smtp_Server.Credentials = New Net.NetworkCredential(UserName, UserPassword)
                Smtp_Server.Port = Port
                Smtp_Server.EnableSsl = SLL
                Smtp_Server.Host = UserSMTP

                e_mail = New MailMessage()
                e_mail.From = New MailAddress(UserName)
                e_mail.To.Add(Too)
                e_mail.Subject = Title
                e_mail.IsBodyHtml = False
                e_mail.Body = Message
                Smtp_Server.Send(e_mail)
                Return True
            Catch ex As Exception
                Return False
            End Try
            Return False
        End Function
    End Class
End Namespace
