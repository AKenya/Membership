Imports System.IO
Imports DevExpress.Xpo
Imports System.ComponentModel
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.ExpressApp.DC

Namespace WinWebSolution.Module
    <Persistent(), XafDefaultProperty("FileName")> _
    Public Class FileDataEx
        Inherits BaseObject
        Implements IFileData, IEmptyCheckable
        Public Const DefaultDirectory2 As String = "\\(Local)\mman docs\"
        Public Const DefaultDirectory As String = "D:\LicensingFiles\"
        Private Const ReadBytesSize As Integer = &H1000
        Private tempStream As Stream = Nothing
        Private tempFileName As String = String.Empty

        <Persistent("Size")> _
        Private _Size As Integer
        <PersistentAlias("_Size")> _
        Public ReadOnly Property Size() As Integer Implements IFileData.Size
            Get
                Return _Size
            End Get
        End Property
        Public Sub LoadFromStream(ByVal name As String, ByVal fs As Object)
            Throw New NotImplementedException()
        End Sub
        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub
        Public Overrides Function ToString() As String
            Return FileName
        End Function
        Private _Content() As Byte
        <Browsable(False), MemberDesignTimeVisibility(False), EditorBrowsable(EditorBrowsableState.Never)> _
        Public Property Content() As Byte()
            Get
                Return _Content
            End Get
            Set(ByVal value As Byte())
                SetPropertyValue("Content", _Content, value)
            End Set
        End Property
        <NonPersistent(), MemberDesignTimeVisibility(False)> _
        Public ReadOnly Property IsEmpty() As Boolean Implements IEmptyCheckable.IsEmpty
            Get
                Return String.IsNullOrEmpty(_FileName) AndAlso Not Exists
            End Get
        End Property
        Public Overrides Sub AfterConstruction()
            MyBase.AfterConstruction()
            If (Not Directory.Exists(DefaultDirectory)) Then
                Directory.CreateDirectory(DefaultDirectory)
            End If
        End Sub
        Private _FileName As String = String.Empty
        <Size(260)> _
        Public Property FileName() As String Implements IFileData.FileName
            Get
                Return _FileName
            End Get
            Set(ByVal value As String)
                SetPropertyValue("FileName", _FileName, value)
            End Set
        End Property
        Public ReadOnly Property RealFileName() As String
            Get

                If _FileName <> String.Empty Then
                    Return Path.Combine(DefaultDirectory, GetUniqueFileName(_FileName))
                Else
                    Return String.Empty
                End If
            End Get
        End Property
        Protected Overridable Function GetUniqueFileName(ByVal shortFileName As String) As String
            Return String.Format("{0}-{1}", Oid, shortFileName)
        End Function
        Protected Overridable Function GetShortFileName(ByVal uniqueFileName As String) As String
            Return uniqueFileName.Replace(String.Format("{0}-", Oid), String.Empty).Replace(DefaultDirectory, String.Empty)
        End Function
        Public ReadOnly Property Exists() As Boolean
            Get
                Return System.IO.File.Exists(RealFileName)
            End Get
        End Property
        Public Sub LoadFromStream(ByVal fileName As String, ByVal source As Stream) Implements IFileData.LoadFromStream
            tempStream = source
            If String.IsNullOrEmpty(tempFileName) Then
                tempFileName = RealFileName
            End If
            Me.FileName = fileName
        End Sub
        Public Sub SaveToStream(ByVal destination As Stream) Implements IFileData.SaveToStream
            Try
                Using source As Stream = System.IO.File.OpenRead(RealFileName)
                    Dim buffer(ReadBytesSize - 1) As Byte
                    Dim read As Integer = 0
                    read = source.Read(buffer, 0, buffer.Length)
                    Do While read > 0
                        destination.Write(buffer, 0, read)
                        read = source.Read(buffer, 0, buffer.Length)
                    Loop
                End Using
            Catch exc As DirectoryNotFoundException
                Throw New Exception(String.Format("Cannot access the '{0}' store because it is not available.", DefaultDirectory), exc)
            Catch exc As FileNotFoundException
                Throw New Exception(String.Format("Cannot access the '{0}' file because it is not found in the store.", FileName), exc)
            End Try
        End Sub
        Protected Overrides Sub OnSaving()
            MyBase.OnSaving()
            If tempStream IsNot Nothing AndAlso (Not String.IsNullOrEmpty(RealFileName)) Then
                Dim source As Stream = Nothing
                If TypeOf tempStream Is FileStream Then
                    Try
                        source = System.IO.File.OpenRead((CType(tempStream, FileStream)).Name)
                    Catch exc As FileNotFoundException
                        Throw New Exception(String.Format("Cannot read the '{0}' file from a temporary store.", FileName), exc)
                    End Try
                Else
                    source = tempStream
                End If
                Try
                    Using destination As Stream = System.IO.File.OpenWrite(RealFileName)
                        Dim buffer(ReadBytesSize - 1) As Byte
                        Dim read As Integer = 0
                        read = source.Read(buffer, 0, buffer.Length)
                        Do While read > 0
                            destination.Write(buffer, 0, read)
                            read = source.Read(buffer, 0, buffer.Length)
                        Loop
                        UpdateSize(destination)
                    End Using
                Catch exc As DirectoryNotFoundException
                    Throw New Exception(String.Format("Cannot access the '{0}' store because it is not available.", DefaultDirectory), exc)
                Finally
                    source.Close()
                    source = Nothing
                End Try
            End If
            If (Not String.IsNullOrEmpty(tempFileName)) Then
                Try
                    System.IO.File.Delete(tempFileName)
                    tempFileName = String.Empty
                Catch exc As DirectoryNotFoundException
                    Throw New Exception(String.Format("Cannot access the '{0}' store because it is not available.", DefaultDirectory), exc)
                End Try
            End If
        End Sub
        Protected Overrides Sub OnDeleting()
            Clear()
            MyBase.OnDeleting()
        End Sub
        Private Sub UpdateSize(ByVal stream As Stream)
            If stream Is Nothing Then
                _Size = 0
            Else
                _Size = CInt(Fix(stream.Length))
            End If
        End Sub
        Public Sub Clear() Implements IFileData.Clear
            If String.IsNullOrEmpty(tempFileName) Then
                tempFileName = RealFileName
            End If
            FileName = String.Empty
            UpdateSize(Nothing)
        End Sub
    End Class
End Namespace