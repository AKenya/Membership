Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.ExpressApp.DC

<XafDefaultProperty("RescueType")> _
Public Class RescueType
    Inherits BaseObject
    Private fRescueType As String
    Private fCategory As Categories
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
    Public Property RescueType As String
        Get
            Return fRescueType
        End Get
        Set(ByVal value As String)
            SetPropertyValue("RescueType", fRescueType, value)
        End Set
    End Property
    Public Property Category() As Categories
        Get
            Return fCategory
        End Get
        Set(ByVal value As Categories)
            SetPropertyValue("Category", fCategory, value)
        End Set
    End Property
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place here your initialization code.
    End Sub
    Public Enum Categories
        Member
        NonMember
    End Enum
End Class
