Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.ExpressApp.DC

<XafDefaultProperty("RegionName")> _
Public Class RescueRegions
    Inherits BaseObject
    Private fRegionName As String
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    <RuleRequiredField()> _
    <RuleUniqueValue("", DefaultContexts.Save)> _
    Public Property RegionName As String
        Get
            Return fRegionName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("RegionName", fRegionName, value)
        End Set
    End Property
    <Association("RescueLocations-RescueRegions")> _
    Public ReadOnly Property RescueLocation() As XPCollection(Of RescueLocations)
        Get
            Return (GetCollection(Of RescueLocations)("RescueLocation"))
        End Get
    End Property
    <Association("RescueDetails-RescueRegionsFrom", GetType(RescueDetails))> _
    Public ReadOnly RescueDetailsFrom() As RescueDetails
    <Association("RescueDetails-RescueRegions", GetType(RescueDetails))> _
    Public ReadOnly RescueDetailsTo() As RescueDetails
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place here your initialization code.
    End Sub
End Class
