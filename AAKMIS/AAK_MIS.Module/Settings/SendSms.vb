Imports System.Net
Imports System.Text
Imports System.IO

Public Class SendSms
    Public Shared Function SendSMS(msisdn As [String], text As [String]) As Integer
        Dim webTarget As String = "http://api.sms.bambika.co.ke:8555/?target=AAKENYA&login=Aakenya&pass=4516ec9f2bdfaac579161caf3f60daf2"
        Dim webPost As String = "&msisdn={0}&text={1}"
        Dim userName As String = "Aakenya"
        Dim password As String = "4516ec9f2bdfaac579161caf3f60daf2"
        Dim url As String = [String].Format(webTarget)
        Dim req As HttpWebRequest = DirectCast(WebRequest.Create(url), HttpWebRequest)
        req.Method = "POST"
        req.ContentType = "application/x-www-form-urlencoded"
        Dim postData As Byte() = Encoding.ASCII.GetBytes([String].Format(webPost, msisdn, text))
        req.ContentLength = postData.Length
        Dim authInfo As String = Convert.ToString(userName & Convert.ToString(":")) & password
        authInfo = Convert.ToBase64String(Encoding.[Default].GetBytes(authInfo))
        req.Headers("Authorization") = Convert.ToString("Basic ") & authInfo
        Dim PostStream As Stream = req.GetRequestStream()
        PostStream.Write(postData, 0, postData.Length)
        Dim res As HttpWebResponse = DirectCast(req.GetResponse(), HttpWebResponse)
    End Function
End Class
