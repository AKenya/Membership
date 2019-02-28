Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp.DC
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation

<XafDefaultProperty("JobTitle")> _
Public Class JobTitle
    Inherits BaseObject
    Private fJobTitle As String
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
        ' This constructor is used when an object is loaded from a persistent storage.
        ' Do not place any code here or place it only when the IsLoading property is false:
        ' if (!IsLoading){
        '   It is now OK to place your initialization code here.
        ' }
        ' or as an alternative, move your initialization code into the AfterConstruction method.
    End Sub
    <RuleRequiredField()> _
<RuleUniqueValue("", DefaultContexts.Save)> _
    Public Property JobTitle() As String
        Get
            Return fJobTitle
        End Get
        Set(ByVal value As String)
            SetPropertyValue("JobTitle", fJobTitle, value)
        End Set
    End Property
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place here your initialization code.
    End Sub
End Class
