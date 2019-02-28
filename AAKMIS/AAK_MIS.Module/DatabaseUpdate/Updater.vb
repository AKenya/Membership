Imports System.Linq
Imports DevExpress.ExpressApp
Imports DevExpress.Data.Filtering
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.ExpressApp.Security

Public Class Updater
    Inherits ModuleUpdater
    Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
        MyBase.New(objectSpace, currentDBVersion)
    End Sub
    ' Override to specify the database update code which should be performed after the database schema is updated (http://documentation.devexpress.com/#Xaf/DevExpressExpressAppUpdatingModuleUpdater_UpdateDatabaseAfterUpdateSchematopic).
    Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
        MyBase.UpdateDatabaseAfterUpdateSchema()
        ' ...
        ' If a user named 'Sam' does not exist in the database, create this user
        Dim user1 As User = ObjectSpace.FindObject(Of User)(New BinaryOperator("UserName", "Sam"))
        If user1 Is Nothing Then
            user1 = ObjectSpace.CreateObject(Of User)()
            user1.UserName = "Sam"
            user1.FirstName = "Sam"
            ' Set a password if the standard authentication type is used
            user1.SetPassword("Sam")
        End If
        ' If a user named 'John' does not exist in the database, create this user
        Dim user2 As User = ObjectSpace.FindObject(Of User)(New BinaryOperator("UserName", "John"))
        If user2 Is Nothing Then
            user2 = ObjectSpace.CreateObject(Of User)()
            user2.UserName = "John"
            user2.FirstName = "John"
            ' Set a password if the standard authentication type is used
            user2.SetPassword("John")
        End If
        ' If a role with the Administrators name does not exist in the database, create this role
        Dim adminRole As Role = _
        ObjectSpace.FindObject(Of Role)(New BinaryOperator("Name", "Administrators "))
        If adminRole Is Nothing Then
            adminRole = ObjectSpace.CreateObject(Of Role)()
            adminRole.Name = "Administrators "
        End If
        ' If a role with the Users name does not exist in the database, create this role
        Dim userRole As Role = ObjectSpace.FindObject(Of Role)(New BinaryOperator("Name", "Users"))
        If userRole Is Nothing Then
            userRole = ObjectSpace.CreateObject(Of Role)()
            userRole.Name = "Users"
        End If
        ' Delete all permissions assigned to the Administrators and Users roles
        Do While adminRole.PersistentPermissions.Count > 0
            ObjectSpace.Delete(adminRole.PersistentPermissions(0))
        Loop
        Do While userRole.PersistentPermissions.Count > 0
            ObjectSpace.Delete(userRole.PersistentPermissions(0))
        Loop
        ' Allow full access to all objects to the Administrators role
        adminRole.AddPermission(New ObjectAccessPermission(GetType(Object), ObjectAccess.AllAccess))
        ' Allow editing the Application Model to the Administrators role
        adminRole.AddPermission(New EditModelPermission(ModelAccessModifier.Allow))
        ' Save the Administrators role to the database
        adminRole.Save()
        ' Allow full access to all objects to the Users role
        userRole.AddPermission(New ObjectAccessPermission(GetType(Object), ObjectAccess.AllAccess))
        ' Deny change access to the User type objects to the Users role
        userRole.AddPermission(New ObjectAccessPermission(GetType(User), _
           ObjectAccess.ChangeAccess, ObjectAccessModifier.Deny))
        ' Deny full access to the User type objects to the Users role
        userRole.AddPermission(New ObjectAccessPermission(GetType(Role), _
           ObjectAccess.AllAccess, ObjectAccessModifier.Deny))
        ' Deny editing the Application Model to the Users role
        userRole.AddPermission(New EditModelPermission(ModelAccessModifier.Deny))
        ' Save the Users role to the database
        userRole.Save()
        ' Add the Administrators role to the user1
        user1.Roles.Add(adminRole)
        ' Add the Users role to the user2
        user2.Roles.Add(userRole)
        ' Save the users to the database
        user1.Save()
        user2.Save()

        '//Autogenerated codes in devexpress 13.
        'Example:
        'Dim name As String = "MyName"
        'Dim theObject As DomainObject1 = ObjectSpace.FindObject(Of DomainObject1)(CriteriaOperator.Parse("Name=?", name))
        'If (theObject Is Nothing) Then
        '    theObject = ObjectSpace.CreateObject(Of DomainObject1)()
        '    theObject.Name = name
        'End If
    End Sub

    ' Override to perform the required changes with the database structure before the database schema is updated (http://documentation.devexpress.com/#Xaf/DevExpressExpressAppUpdatingModuleUpdater_UpdateDatabaseBeforeUpdateSchematopic).
    Public Overrides Sub UpdateDatabaseBeforeUpdateSchema()
        MyBase.UpdateDatabaseBeforeUpdateSchema()
    End Sub

End Class