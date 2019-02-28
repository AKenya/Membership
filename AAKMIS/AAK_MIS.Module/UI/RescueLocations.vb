Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp.DC
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation

<XafDefaultProperty("Location")> _
Public Class RescueLocations
    Inherits BaseObject
    Private fLocation As String
    Private fRescueRegion As RescueRegions
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
    Public Property Location As String
        Get
            Return fLocation
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Location", fLocation, value)
        End Set
    End Property

    <Association("RescueLocations-RescueRegions")> _
    Public Property RescueRegion() As RescueRegions
        Get
            Return fRescueRegion
        End Get
        Set(ByVal value As RescueRegions)
            SetPropertyValue("RescueRegion", fRescueRegion, value)
        End Set
    End Property

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place here your initialization code.
    End Sub
End Class
