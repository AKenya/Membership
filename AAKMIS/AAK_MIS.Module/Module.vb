Imports System.Linq
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Updating

Public NotInheritable Class [AAK_MISModule]
    ' You can override various virtual methods and handle corresponding events to manage various aspects of your XAF application UI and behavior.
    Inherits ModuleBase 'http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppModuleBasetopic
    Public Sub New()
        InitializeComponent()
    End Sub

    Public Overrides Function GetModuleUpdaters(ByVal objectSpace As IObjectSpace, ByVal versionFromDB As Version) As IEnumerable(Of ModuleUpdater)
        Dim updater As ModuleUpdater = New Updater(objectSpace, versionFromDB)
        Return New ModuleUpdater() {updater}
    End Function

    ' Override to customize information on a particular class or property, before it is loaded to the Application Model.
    'Public Overrides Sub CustomizeTypesInfo(ByVal typesInfo As ITypesInfo) ' http://documentation.devexpress.com/Xaf/CustomDocument3224.aspx
    '    MyBase.CustomizeTypesInfo(typesInfo)
    '    Example:
    '    Dim theTypeInfo As ITypeInfo = typesInfo.FindTypeInfo(GetType(DCBaseObject))
    '    If theTypeInfo IsNot Nothing Then
    '        Dim memberName As String = "MyCustomMember"
    '        Dim theMemberInfo As IMemberInfo = theTypeInfo.FindMember(memberName)
    '        If theMemberInfo Is Nothing Then
    '            theMemberInfo = theTypeInfo.CreateMember(memberName, GetType(String))
    '        End If
    '        theTypeInfo.AddAttribute(New VisibleInDetailViewAttribute(False))
    '    End If
    'End Sub
    
    ' You can define a fully custom Application Model element or extend an existing one (http://documentation.devexpress.com/#Xaf/CustomDocument3169).
    'Public Overrides Sub ExtendModelInterfaces(ByVal extenders As ModelInterfaceExtenders)
    '    MyBase.ExtendModelInterfaces(extenders)
    '    Example:
    '    extenders.Add(Of IModelApplication, IModelMyModelExtension)()
    'End Sub
End Class
' Declaration of a custom Application Model element extension to keep your custom settings (http://documentation.devexpress.com/#Xaf/CustomDocument2579).
' Example:
'Public Interface IModelMyModelExtension
'    Inherits IModelNode
'    Property MyCustomProperty() As String
'End Interface
