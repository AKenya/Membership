Imports DevExpress.ExpressApp
Imports DevExpress.Data.Filtering
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Utils
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Base.Security
Imports DevExpress.Xpo
Imports DevExpress.Xpo.DB
Imports DevExpress.ExpressApp.SystemModule
Imports System.Net.Mail
Imports System.Net
Imports System.Data.SqlClient

Public Class MyController
    Inherits DevExpress.ExpressApp.ViewController
    Private ReadOnly setAppStatusItem As ChoiceActionItem
    Private Shared Property con As New Constants
    Public Shared Property uow As UnitOfWork
    Private controller As NewObjectViewController
    Public Shared Sub GetUow()
        If uow Is Nothing Then
            Dim sqlconn = MSSqlConnectionProvider.GetConnectionString(con.server, con.user, con.password, con.DB)
            Dim dl As IDataLayer = XpoDefault.GetDataLayer(sqlconn, AutoCreateOption.DatabaseAndSchema)
            uow = New UnitOfWork(dl)
        End If
    End Sub
    Public Sub New()
        MyBase.New()
        InitializeComponent()
        RegisterActions(components)
        ChangeCState.Items.Clear()
        setAppStatusItem = _
        New ChoiceActionItem( _
        CaptionHelper.GetMemberCaption(GetType(Order), "Status"), Nothing)
        ChangeCState.Items.Add(setAppStatusItem)
        FillItemWithEnumValues(setAppStatusItem, GetType(Status))
        ChangeState.Items.Clear()
        setAppStatusItem = _
        New ChoiceActionItem( _
        CaptionHelper.GetMemberCaption(GetType(Applicants), "Status"), Nothing)
        ChangeState.Items.Add(setAppStatusItem)
        FillItemWithEnumValues(setAppStatusItem, GetType(Status))
    End Sub
    Private Sub MemberRegistration_Activated(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Activated
        UnBlacklist.Active.SetItemValue("SecurityAllowance", False)
        BlacklistP.Active.SetItemValue("SecurityAllowance", False)
        CloseMon.Active.SetItemValue("SecurityAllowance", False)
        AssignCard.Active.SetItemValue("SecurityAllowance", False)
        AllowCardRequest.Active.SetItemValue("SecurityAllowance", False)
        RequestCard.Active.SetItemValue("SecurityAllowance", False)
        Try
            Dim usr As User = SecuritySystem.CurrentUser
            For Each role In usr.Roles
                If String.Compare(role.Name, "ADMINISTRATORS", False) = 0 Then
                    UnBlacklist.Active.SetItemValue("SecurityAllowance", True)
                    BlacklistP.Active.SetItemValue("SecurityAllowance", True)
                    CloseMon.Active.SetItemValue("SecurityAllowance", True)
                    AssignCard.Active.SetItemValue("SecurityAllowance", True)
                    AllowCardRequest.Active.SetItemValue("SecurityAllowance", True)
                    RequestCard.Active.SetItemValue("SecurityAllowance", True)
                End If
                If String.Compare(role.Name, "MEMBERSHIP ADMIN", False) = 0 Then
                    AssignCard.Active.SetItemValue("SecurityAllowance", True)
                    AllowCardRequest.Active.SetItemValue("SecurityAllowance", True)
                    RequestCard.Active.SetItemValue("SecurityAllowance", True)
                End If
                If String.Compare(role.Name, "SCHEME ADMIN", False) = 0 Then
                    AssignCard.Active.SetItemValue("SecurityAllowance", True)
                End If
                If String.Compare(role.Name, "MANAGER", False) = 0 Then
                    UnBlacklist.Active.SetItemValue("SecurityAllowance", True)
                    BlacklistP.Active.SetItemValue("SecurityAllowance", True)
                End If
                If String.Compare(role.Name, "RESCUE", False) = 0 Then
                    CloseMon.Active.SetItemValue("SecurityAllowance", True)
                End If
                If String.Compare(role.Name, "SCHEME MANAGER", False) = 0 Then
                    AssignCard.Active.SetItemValue("SecurityAllowance", True)
                End If
                If String.Compare(role.Name, "CARDS OFFICER", False) = 0 Then
                    AssignCard.Active.SetItemValue("SecurityAllowance", True)
                End If
                If String.Compare(role.Name, "CUSTOMER SERVICE SUPER", False) = 0 Then
                    AssignCard.Active.SetItemValue("SecurityAllowance", True)
                    RequestCard.Active.SetItemValue("SecurityAllowance", True)
                End If
            Next
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub BlacklistP_CustomizePopupWindowParams(sender As Object, e As CustomizePopupWindowParamsEventArgs) Handles BlacklistP.CustomizePopupWindowParams
        Dim task = CType(View.CurrentObject, MemberRegistration)
        View.ObjectSpace.SetModified(task)
        Dim objectSpace = Application.CreateObjectSpace()
        Dim sm = objectSpace.CreateObject(Of BlacklistDetails)()
        e.View = Application.CreateDetailView(objectSpace, sm)
        sm.MemberNo = task.MemberNumber
    End Sub
  Protected Overrides Sub OnFrameAssigned()
        MyBase.OnFrameAssigned()
    End Sub

    Protected Overrides Sub OnActivated()
        MyBase.OnActivated()
        controller = Frame.GetController(Of NewObjectViewController)()
        If controller IsNot Nothing Then
            AddHandler controller.ObjectCreating, AddressOf controller_ObjectCreating
        End If
        Dim linkcontroller As LinkUnlinkController = Frame.GetController(Of LinkUnlinkController)()
        If linkcontroller IsNot Nothing Then
            linkcontroller.Active("Deactivation in code") = Not (View.ObjectTypeInfo.Type Is GetType(Payments) AndAlso TypeOf View Is ListView)
            linkcontroller.Active("UnlinkDeactivation in code") = Not (View.ObjectTypeInfo.Type Is GetType(IDP_Details) AndAlso SecuritySystem.CurrentUserName <> "Sam" AndAlso TypeOf View Is ListView)
            linkcontroller.Active("DeactivateRescueLink in code") = Not (View.ObjectTypeInfo.Type Is GetType(RescueDetails) AndAlso SecuritySystem.CurrentUserName <> "Sam" AndAlso TypeOf View Is ListView)
            linkcontroller.Active("DeactivateMemberLink in code") = Not (View.ObjectTypeInfo.Type Is GetType(MemberRegistration) AndAlso SecuritySystem.CurrentUserName <> "Sam" AndAlso TypeOf View Is ListView)
            linkcontroller.Active("DeactivateVoucherLink in code") = Not (View.ObjectTypeInfo.Type Is GetType(Voucher) AndAlso SecuritySystem.CurrentUserName <> "Sam" AndAlso TypeOf View Is ListView)
        End If
    End Sub

    Protected Overrides Sub OnViewControlsCreated()
        MyBase.OnViewControlsCreated()
    End Sub

    Protected Overrides Sub OnDeactivated()
        If controller IsNot Nothing Then
            RemoveHandler controller.ObjectCreating, AddressOf controller_ObjectCreating
        End If
        MyBase.OnDeactivated()
    End Sub

    Private Sub UnBlacklist_Execute(sender As System.Object, e As SimpleActionExecuteEventArgs) Handles UnBlacklist.Execute
        Dim objectsToProcess As New ArrayList(e.SelectedObjects)
        If e.SelectedObjects.Count > 0 Then
            For Each obj As Object In objectsToProcess
                Dim objInNewObjectSpace = CType(ObjectSpace.GetObject(obj), MemberRegistration)
                objInNewObjectSpace.Blacklisted = False
                objInNewObjectSpace.BlacklistReason = Nothing
                objInNewObjectSpace.Save()
            Next obj
        End If
        If TypeOf View Is DetailView AndAlso (CType(View, DetailView)).ViewEditMode = _
            ViewEditMode.View Then
            View.ObjectSpace.CommitChanges()
            View.ObjectSpace.Refresh()
        End If
        If TypeOf View Is ListView Then
            ObjectSpace.CommitChanges()
            View.ObjectSpace.Refresh()
        End If
    End Sub

    Private Sub BlacklistP_Execute(sender As System.Object, e As PopupWindowShowActionExecuteEventArgs) Handles BlacklistP.Execute
        Dim task = CType(View.CurrentObject, MemberRegistration)
        View.ObjectSpace.SetModified(task)
        For Each Blacklist As BlacklistDetails In e.PopupWindow.View.SelectedObjects
            Try
                If Blacklist.Reason Is Nothing = False Then
                    If task.BlacklistReason Is Nothing Then
                        task.BlacklistReason = Blacklist.Reason
                        task.Blacklisted = True
                        task.Save()
                    End If
                    Blacklist.MemberNo = task.MemberNumber
                    Blacklist.Save()
                End If
            Catch ex As Exception
                Throw New InvalidProgramException("A problem was detected during the blacklisting process")
            End Try
        Next
        If TypeOf View Is DetailView AndAlso (CType(View, DetailView)).ViewEditMode = _
            ViewEditMode.View Then
            View.ObjectSpace.CommitChanges()
        End If
        If TypeOf View Is ListView Then
            ObjectSpace.CommitChanges()
            View.ObjectSpace.Refresh()
        End If
    End Sub

    Private Sub IDP_Allocation_Activated(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Activated
        ReceiveToStore.Active.SetItemValue("SecurityAllowance", False)
        AllocateToBranch.Active.SetItemValue("SecurityAllowance", False)
        MarkMissing.Active.SetItemValue("SecurityAllowance", False)
        PreIssueIDP.Active.SetItemValue("SecurityAllowance", False)
        Try
            Dim usr As User = SecuritySystem.CurrentUser
            For Each role As IRole In usr.Roles
                If String.Compare(role.Name, "ADMINISTRATORS", False) = 0 Then
                    ReceiveToStore.Active.SetItemValue("SecurityAllowance", True)
                    AllocateToBranch.Active.SetItemValue("SecurityAllowance", True)
                    MarkMissing.Active.SetItemValue("SecurityAllowance", True)
                    PreIssueIDP.Active.SetItemValue("SecurityAllowance", True)
                End If
                If String.Compare(role.Name, "LICENSING ADMINISTRATOR", False) = 0 Then
                    ReceiveToStore.Active.SetItemValue("SecurityAllowance", True)
                    AllocateToBranch.Active.SetItemValue("SecurityAllowance", True)
                    MarkMissing.Active.SetItemValue("SecurityAllowance", True)
                    PreIssueIDP.Active.SetItemValue("SecurityAllowance", True)
                End If
            Next

        Catch ex As Exception
            Throw New ApplicationException("You are not authorised to access this object")
        End Try
    End Sub
    Private Sub CancelIdp_CustomizePopupWindowParams(sender As Object, e As CustomizePopupWindowParamsEventArgs) Handles CancelIDP.CustomizePopupWindowParams
        Dim task = CType(View.CurrentObject, IDP_Allocation)
        View.ObjectSpace.SetModified(task)
        Dim objectSpace = Application.CreateObjectSpace()
        Dim sm = objectSpace.CreateObject(Of CancelReasons)()
        e.View = Application.CreateDetailView(objectSpace, sm)
        sm.IDPNumber = task.IDPNumber
    End Sub

    Private Sub CancelIdp_Execute(sender As Object, e As PopupWindowShowActionExecuteEventArgs) Handles CancelIDP.Execute
        Dim task = CType(View.CurrentObject, IDP_Allocation)
        View.ObjectSpace.SetModified(task)
        For Each CancelReason As CancelReasons In e.PopupWindow.View.SelectedObjects
            If CancelReason.Reason Is Nothing = False And CancelReason.File Is Nothing = False Then
                If task.CancelReason Is Nothing Then
                    task.CancelReason = CancelReason.Reason
                    task.Status = IDP_Allocation.IdpState.Cancelled
                    task.Save()
                End If
                CancelReason.IDPNumber = task.IDPNumber
                CancelReason.Save()
                View.ObjectSpace.CommitChanges()
            End If
            View.ObjectSpace.Refresh()
            Exit For
        Next
    End Sub

    Private Sub ReceiveToStore_CustomizePopupWindowParams(sender As Object, e As CustomizePopupWindowParamsEventArgs) Handles ReceiveToStore.CustomizePopupWindowParams
        Dim RTS = CType(View.CurrentObject, IDP_Allocation)
        View.ObjectSpace.SetModified(RTS)
        Dim objectSpace = Application.CreateObjectSpace()
        Dim Rm = objectSpace.CreateObject(Of ReceiveIdps)()
        e.View = Application.CreateDetailView(objectSpace, Rm)
    End Sub

    Private Sub AllocateToBranch_CustomizePopupWindowParams(sender As Object, e As CustomizePopupWindowParamsEventArgs) Handles AllocateToBranch.CustomizePopupWindowParams
        Dim IBA = CType(View.CurrentObject, IDP_Allocation)
        View.ObjectSpace.SetModified(IBA)
        Dim objectSpace = Application.CreateObjectSpace()
        Dim RmIBA = objectSpace.CreateObject(Of IDPBranch_Allocation)()
        e.View = Application.CreateDetailView(objectSpace, RmIBA)
    End Sub

    Private Sub MarkMissing_Execute(sender As Object, e As SimpleActionExecuteEventArgs) Handles MarkMissing.Execute
        Dim objectsToProcess As New ArrayList(e.SelectedObjects)
        If e.SelectedObjects.Count > 0 Then
            For Each obj As Object In objectsToProcess
                Dim objInNewObjectSpace = CType(ObjectSpace.GetObject(obj), IDP_Allocation)
                objInNewObjectSpace.Status = IDP_Allocation.IdpState.Missing
                objInNewObjectSpace.Save()
            Next obj
        End If
        If TypeOf View Is DetailView AndAlso (CType(View, DetailView)).ViewEditMode = _
            ViewEditMode.View Then
            View.ObjectSpace.CommitChanges()
            View.ObjectSpace.Refresh()
        End If
        If TypeOf View Is ListView Then
            ObjectSpace.CommitChanges()
            View.ObjectSpace.Refresh()
        End If
    End Sub

    Private Sub ChangeCState_Execute(ByVal sender As System.Object, ByVal e As SingleChoiceActionExecuteEventArgs) Handles ChangeCState.Execute
        Dim objectSpace As IObjectSpace = If( _
        TypeOf View Is ListView, Application.CreateObjectSpace(), View.ObjectSpace)
        Dim objectsToProcess As New ArrayList(e.SelectedObjects)
        If e.SelectedChoiceActionItem.ParentItem Is setAppStatusItem Then
            For Each obj As Object In objectsToProcess
                Dim objInNewObjectSpace As Order = CType( _
                objectSpace.GetObject(obj), Order)
                objInNewObjectSpace.Status = CType(e.SelectedChoiceActionItem.Data, Status)
                objInNewObjectSpace.Status = Order.OrderStatus.Pending
            Next obj
        End If
        If TypeOf View Is DetailView AndAlso (CType(View, DetailView)).ViewEditMode = _
        ViewEditMode.View Then
            objectSpace.CommitChanges()
        End If
        If TypeOf View Is ListView Then
            objectSpace.CommitChanges()
            View.ObjectSpace.Refresh()
        End If
    End Sub

    Private Shared Sub FillItemWithEnumValues(ByVal parentItem As ChoiceActionItem, ByVal enumType As Type)
        For Each current As Object In System.Enum.GetValues(enumType)
            Dim ed As EnumDescriptor = New EnumDescriptor(enumType)
            Dim item As ChoiceActionItem = New ChoiceActionItem(ed.GetCaption(current), current) With {.ImageName = ImageLoader.Instance.GetEnumValueImageName(current)}
            parentItem.Items.Add(item)
        Next current
    End Sub
    Public Enum Status
        <ImageName("State_Priority_Low")> _
        Available = 0
        <ImageName("State_Priority_Normal")> _
        Engaged = 1
        <ImageName("State_Priority_High")> _
        Disqualified = 2
    End Enum

    Private Sub ChangeState_Execute(ByVal sender As Object, ByVal e As SingleChoiceActionExecuteEventArgs) Handles ChangeState.Execute
        Dim objectSpace As IObjectSpace = If( _
        TypeOf View Is ListView, Application.CreateObjectSpace(), View.ObjectSpace)
        Dim objectsToProcess As New ArrayList(e.SelectedObjects)
        If e.SelectedChoiceActionItem.ParentItem Is setAppStatusItem Then
            For Each obj As Object In objectsToProcess
                Dim objInNewObjectSpace As Applicants = CType( _
                objectSpace.GetObject(obj), Applicants)
                objInNewObjectSpace.Status = CType(e.SelectedChoiceActionItem.Data, Status)
                objInNewObjectSpace.Order = Nothing
            Next obj
        End If
        If TypeOf View Is DetailView AndAlso (CType(View, DetailView)).ViewEditMode = _
        ViewEditMode.View Then
            objectSpace.CommitChanges()
        End If
        If TypeOf View Is ListView Then
            objectSpace.CommitChanges()
            View.ObjectSpace.Refresh()
        End If

    End Sub

    Private Sub CloseMon_Execute(sender As Object, e As SimpleActionExecuteEventArgs) Handles CloseMon.Execute
        Dim Mon As String = ""
        If DatePart(DateInterval.Month, Today) < 10 Then
            Mon = String.Format("0{0}", DatePart(DateInterval.Month, Today))
        Else
            Mon = DatePart(DateInterval.Month, Today)
        End If
        If (View.ObjectTypeInfo.Type Is GetType(RescueDetails)) Then
            Try
                Dim sqlHandler As New SQLdb
                Dim StrQ1 As String = [String].Format("UPDATE RescuePeriod SET RescueMonth='{0}',RescueYear='{1}',ResetDate=CONVERT(DATETIME,'{2}',104)", Mon, DatePart(DateInterval.Year, Today), Date.Now)
                sqlHandler.SendNonQuery(CType("UPDATE RescueDetails SET Checked = 'True' WHERE Checked <>'True'", String))
                sqlHandler.SendNonQuery(StrQ1)
            Catch ex As Exception
                Throw
            End Try
        End If
    End Sub

    Private Sub FeedBack_CustomizePopupWindowParams(sender As Object, e As CustomizePopupWindowParamsEventArgs) Handles FeedBack.CustomizePopupWindowParams
        Dim task = CType(View.CurrentObject, MemberRegistration)
        View.ObjectSpace.SetModified(task)
        Dim objectSpace = Application.CreateObjectSpace()
        Dim sm = objectSpace.CreateObject(Of ComplaintRegister)()
        e.View = Application.CreateDetailView(objectSpace, sm)
        sm.MemberNo = task.MemberNumber
    End Sub

    Private Sub FeedBack_Execute(sender As Object, e As PopupWindowShowActionExecuteEventArgs) Handles FeedBack.Execute
        For Each Feedback As ComplaintRegister In e.PopupWindow.View.SelectedObjects
            Dim task = CType(View.CurrentObject, MemberRegistration)
            View.ObjectSpace.SetModified(task)
            If Feedback.Comments Is Nothing = False Then
                Feedback.MemberNo = task.MemberNumber
                Feedback.Save()
            End If
        Next
        If TypeOf View Is DetailView AndAlso (CType(View, DetailView)).ViewEditMode = _
            ViewEditMode.View Then
            View.ObjectSpace.CommitChanges()
        End If
        If TypeOf View Is ListView Then
            ObjectSpace.CommitChanges()
            View.ObjectSpace.Refresh()
        End If
    End Sub

    Private Sub TakeAction_CustomizePopupWindowParams(sender As Object, e As CustomizePopupWindowParamsEventArgs) Handles TakeAction.CustomizePopupWindowParams
        Dim task = CType(View.CurrentObject, ComplaintRegister)
        View.ObjectSpace.SetModified(task)
        Dim objectSpace = Application.CreateObjectSpace()
        Dim sm = objectSpace.CreateObject(Of Action)()
        e.View = Application.CreateDetailView(objectSpace, sm)
    End Sub

    Private Sub TakeAction_Execute(sender As Object, e As PopupWindowShowActionExecuteEventArgs) Handles TakeAction.Execute
        Dim task = CType(View.CurrentObject, ComplaintRegister)
        View.ObjectSpace.SetModified(task)
        For Each action As Action In e.PopupWindow.View.SelectedObjects
            If action.ActionTaken Is Nothing = False Then
                If task.ActionTaken Is Nothing Then
                    task.ActionTaken = action.ActionTaken
                    task.ActionTime = action.ModifiedOn
                    task.Save()
                Else
                    task.ActionTaken = String.Format("{0} {1}", task.ActionTaken, action.ActionTaken)
                    task.ActionTime = action.ModifiedOn
                    task.Save()
                End If
            End If
        Next
        If TypeOf View Is DetailView AndAlso (CType(View, DetailView)).ViewEditMode = _
            ViewEditMode.View Then
            View.ObjectSpace.CommitChanges()
        End If
        If TypeOf View Is ListView Then
            ObjectSpace.CommitChanges()
            View.ObjectSpace.Refresh()
        End If
    End Sub

    Private Sub Close_Execute(sender As Object, e As SimpleActionExecuteEventArgs) Handles CloseF.Execute
        Dim objectsToProcess As New ArrayList(e.SelectedObjects)
        If e.SelectedObjects.Count > 0 Then
            For Each obj As Object In objectsToProcess
                Dim objInNewObjectSpace = CType(ObjectSpace.GetObject(obj), ComplaintRegister)
                If objInNewObjectSpace.ActionTaken = Nothing And String.Compare(objInNewObjectSpace.Type.Name, "COMPLAINT", False) = 0 Then
                    Throw New InvalidProgramException("Please take action to close this complaint")
                Else
                    objInNewObjectSpace.Status = ComplaintRegister.FeedStatus.Closed
                    If objInNewObjectSpace.ActionTime = Nothing Then
                        objInNewObjectSpace.ActionTime = Now
                    End If
                    objInNewObjectSpace.Save()
                End If
            Next obj
        End If
        If TypeOf View Is DetailView AndAlso (CType(View, DetailView)).ViewEditMode = _
            ViewEditMode.View Then
            View.ObjectSpace.CommitChanges()
            View.ObjectSpace.Refresh()
        End If
        If TypeOf View Is ListView Then
            ObjectSpace.CommitChanges()
            View.ObjectSpace.Refresh()
        End If
    End Sub

    Private Sub AssignCard_CustomizePopupWindowParams(sender As Object, e As CustomizePopupWindowParamsEventArgs) Handles AssignCard.CustomizePopupWindowParams
        Try
            Dim task = CType(View.CurrentObject, MemberRegistration)
            View.ObjectSpace.SetModified(task)
            Dim objectSpace = Application.CreateObjectSpace()
            Dim sm = objectSpace.CreateObject(Of CardsManager)()
            e.View = Application.CreateDetailView(objectSpace, sm)
            sm.MemberNo = task.MemberNumber
            sm.Branch = task.Branch.BranchName
            sm.RequestedBy = task.RequestedBy
            sm.MemberName = task.MemberName
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub AssignCard_Execute(sender As System.Object, e As PopupWindowShowActionExecuteEventArgs) Handles AssignCard.Execute
        Dim task = CType(View.CurrentObject, MemberRegistration)
        View.ObjectSpace.SetModified(task)
        For Each Card As CardsManager In e.PopupWindow.View.SelectedObjects
            Try
                If task.CardSerialNo Is Nothing = False Or task.CardSerialNo <> "" Then
                    task.CardSerialNo = String.Format("{0}; {1}", task.CardSerialNo, Card.SerialNo)
                    task.Save()
                Else
                    task.CardSerialNo = String.Format("{0}", Card.SerialNo)
                    task.Save()
                End If
                If TypeOf View Is DetailView AndAlso (CType(View, DetailView)).ViewEditMode = ViewEditMode.View Then
                    View.ObjectSpace.CommitChanges()
                End If
                If TypeOf View Is ListView Then
                    ObjectSpace.CommitChanges()
                    View.ObjectSpace.Refresh()
                End If
            Catch ex As Exception
                Throw
            End Try
        Next
    End Sub

    Private Sub controller_ObjectCreating(ByVal sender As Object, ByVal e As ObjectCreatingEventArgs)
        If View.ObjectTypeInfo.Type Is GetType(IDP_Details) Then
            Try
                Dim task As String = (CType(View.ObjectSpace.Owner, DetailView).CurrentObject).ToString
                View.ObjectSpace.SetModified(task)
                Dim crit As CriteriaOperator = New BinaryOperator("MemberNumber", task, BinaryOperatorType.Equal)
                Dim CritMember = ObjectSpace.FindObject(Of MemberRegistration)(crit)
                Dim MaxIDPNo As Integer = CritMember.Category.MaxIDPs
                Dim count As Integer = 0
                If CritMember.MembershipStatus = MemberRegistration.MemStatus.Valid Then
                    If TypeOf View Is ListView Then
                        Try
                            Dim crit1 As CriteriaOperator = New BinaryOperator("IDPExpiryDate", Today, BinaryOperatorType.GreaterOrEqual)
                            Dim crit3 As CriteriaOperator = New BinaryOperator("MemberNumber", CritMember, BinaryOperatorType.Equal)
                            count = View.ObjectSpace.GetObjectsCount(GetType(IDP_Details), (New BinaryOperator(crit1, crit3, BinaryOperatorType.BitwiseAnd)))
                        Catch ex As Exception
                            Throw ex
                        End Try
                    Else
                        Try
                            Dim crit1 As CriteriaOperator = New BinaryOperator("IDPExpiryDate", Today, BinaryOperatorType.GreaterOrEqual)
                            Dim crit3 As CriteriaOperator = New BinaryOperator("MemberNumber", CritMember, BinaryOperatorType.Equal)
                            count = View.ObjectSpace.GetObjectsCount(GetType(IDP_Details), (New BinaryOperator(crit1, crit3, BinaryOperatorType.BitwiseAnd)))
                        Catch ex As Exception
                            Throw ex
                        End Try
                    End If
                    If count >= MaxIDPNo Then
                        e.Cancel = True
                        Throw New InvalidProgramException("Cannot add another IDP. The member has another valid IDP")
                    End If
                Else
                    e.Cancel = True
                    Throw New InvalidProgramException("Please subscribe this membership to issue an IDP")
                End If
            Catch ex As Exception
                Throw
            End Try

        ElseIf View.ObjectTypeInfo.Type Is GetType(Vehicles) Then
            Try
                Dim task As String = (CType(View.ObjectSpace.Owner, DetailView).CurrentObject).ToString
                View.ObjectSpace.SetModified(task)
                Dim crit As CriteriaOperator = New BinaryOperator("MemberNumber", task, BinaryOperatorType.Equal)
                Dim CritMember = ObjectSpace.FindObject(Of MemberRegistration)(crit)
                Dim count As Integer = 0
                If CritMember Is Nothing = False Then
                    Dim CarNo As Integer = CritMember.Category.MaxVehicles
                    count = If(TypeOf View Is ListView, (CType(View, ListView)).CollectionSource.GetCount(), View.ObjectSpace.GetObjectsCount(GetType(Vehicles), Nothing))
                    If count >= CarNo Then
                        e.Cancel = True
                        Throw New InvalidProgramException("Cannot add another vehicle. Maximum allowed vehicles count reached.")
                    End If
                End If
            Catch ex As Exception

            End Try

        ElseIf View.ObjectTypeInfo.Type Is GetType(Dependants) Then
            Try
                Dim task As String = (CType(View.ObjectSpace.Owner, DetailView).CurrentObject).ToString
                View.ObjectSpace.SetModified(task)
                Dim crit As CriteriaOperator = New BinaryOperator("MemberNumber", task, BinaryOperatorType.Equal)
                Dim CritMember = ObjectSpace.FindObject(Of MemberRegistration)(crit)
                Dim count As Integer = 0
                If CritMember Is Nothing = False Then
                    Dim DepNo As Integer = CritMember.Category.NoOfDependants
                    count = If(TypeOf View Is ListView, (CType(View, ListView)).CollectionSource.GetCount(), View.ObjectSpace.GetObjectsCount(GetType(Dependants), Nothing))
                    If count >= DepNo Then
                        e.Cancel = True
                        Throw New InvalidProgramException("Cannot add more dependants. Maximum allowed dependants count reached.")
                    End If
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Br As Guid
    Private Sub SchemeFilter_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Try
            If (TypeOf View Is ListView) Then
                Dim u As Guid = SecuritySystem.CurrentUserId
                Dim usr As IUserWithRoles = SecuritySystem.CurrentUser
                Dim sqlconn As String = Constants.ConnectionString
                Dim connection As New SqlConnection(sqlconn)
                Dim cmd As New SqlCommand() With {.CommandText = String.Format("SELECT Person.Branch FROM Person where Person.Oid = '{0}'", u.ToString), .Connection = connection}
                connection.Open()
                Dim reader = cmd.ExecuteReader()
                If reader.Read Then
                    Try
                        Br = reader(0)
                    Catch ex As Exception
                        Throw
                    End Try
                End If
                reader.Close()
                For Each role In usr.Roles
                    If String.Compare(role.Name, "SCHEME", False) = 0 Or String.Compare(role.Name, "Scheme", False) = 0 Then
                        If (View.ObjectTypeInfo.Type Is GetType(MemberRegistration)) Then
                            CType(View, ListView).CollectionSource.Criteria("Filter1") = New BinaryOperator("Branch.Oid", Br, BinaryOperatorType.Equal)
                        End If
                        If View.ObjectTypeInfo.Type Is GetType(Branch) Then
                            CType(View, ListView).CollectionSource.Criteria("Filter2") = New BinaryOperator("Oid", Br, BinaryOperatorType.Equal)
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub ProcessO_CustomizePopupWindowParams(ByVal sender As Object, ByVal e As CustomizePopupWindowParamsEventArgs) Handles ProcessO.CustomizePopupWindowParams
        Dim objectSpace As IObjectSpace = Application.CreateObjectSpace()
        e.View = Application.CreateListView(Application.FindListViewId(GetType(Applicants)), _
        New CollectionSource(objectSpace, GetType(Applicants)), True)
    End Sub

    Private Sub ProcessO_Execute(ByVal sender As Object, ByVal e As PopupWindowShowActionExecuteEventArgs) Handles ProcessO.Execute
        Dim task As Order = CType(View.CurrentObject, Order)
        View.ObjectSpace.SetModified(task)
        For Each Appl As Applicants In e.PopupWindow.View.SelectedObjects
            Dim objInNewObjectSpace As Applicants = CType( _
                ObjectSpace.GetObject(Appl), Applicants)
            If Appl.Status = Applicants.AppStatus.Available And Appl.Age >= task.MinAge And Appl.Age <= task.MaxAge And Appl.DrivingExperience >= task.Experience Then
                objInNewObjectSpace.Order = task
                objInNewObjectSpace.Status = Applicants.AppStatus.Engaged
                task.Status = Order.OrderStatus.Processed
                Appl.Save()
                task.Save()
            End If
        Next
        If TypeOf View Is DetailView AndAlso (CType(View, DetailView)).ViewEditMode = _
            ViewEditMode.View Then
            View.ObjectSpace.CommitChanges()
            View.ObjectSpace.Refresh()
        End If

    End Sub

    Private Sub PreIssueIDP_CustomizePopupWindowParams(sender As Object, e As CustomizePopupWindowParamsEventArgs) Handles PreIssueIDP.CustomizePopupWindowParams
        Dim task = CType(View.CurrentObject, IDP_Details)
        View.ObjectSpace.SetModified(task)
        Dim objectSpace = Application.CreateObjectSpace()
        Dim sm = objectSpace.CreateObject(Of IdpPreIssueDetails)()
        e.View = Application.CreateDetailView(objectSpace, sm)
        sm.IDPNo = task.IDPNumber.IDPNumber
    End Sub

    Private Sub PreIssueIDP_Execute(sender As System.Object, e As PopupWindowShowActionExecuteEventArgs) Handles PreIssueIDP.Execute
        Dim task = CType(View.CurrentObject, IDP_Details)
        View.ObjectSpace.SetModified(task)
        For Each PreIssue As IdpPreIssueDetails In e.PopupWindow.View.SelectedObjects
            Try
                If PreIssue.Reason Is Nothing = False Then
                    If task.CancelReason Is Nothing Then
                        task.CancelReason = PreIssue.Reason
                        task.IDPExpiryDate = DateAdd(DateInterval.Day, -1, Date.Today)
                    End If
                    task.Save()
                End If
            Catch ex As Exception
                Throw New InvalidProgramException("A problem was detected during the IDP pre-issue process")
            End Try
        Next
        If TypeOf View Is DetailView AndAlso (CType(View, DetailView)).ViewEditMode = _
            ViewEditMode.View Then
            View.ObjectSpace.CommitChanges()
        End If
        If TypeOf View Is ListView Then
            ObjectSpace.CommitChanges()
        End If
    End Sub

    Private Sub DeleteCert_CustomizePopupWindowParams(sender As Object, e As CustomizePopupWindowParamsEventArgs) Handles DeleteCert.CustomizePopupWindowParams
        Dim task = CType(View.CurrentObject, DriverCertification)
        View.ObjectSpace.SetModified(task)
        Dim objectSpace = Application.CreateObjectSpace()
        Dim sm = objectSpace.CreateObject(Of CertificateDeletionDetails)()
        e.View = Application.CreateDetailView(objectSpace, sm)
        sm.CertNo = task.CertificateNo
    End Sub

    Private Sub DeleteCert_Execute(sender As System.Object, e As PopupWindowShowActionExecuteEventArgs) Handles DeleteCert.Execute
        Dim task = CType(View.CurrentObject, DriverCertification)
        View.ObjectSpace.SetModified(task)
        For Each DeleteCert As CertificateDeletionDetails In e.PopupWindow.View.SelectedObjects
            Try
                If DeleteCert.Reason Is Nothing = False Then
                    If task.DeletionReason Is Nothing Then
                        task.DeletionReason = DeleteCert.Reason
                        task.Delete()
                    End If
                    DeleteCert.CertNo = task.CertificateNo
                    DeleteCert.Save()
                End If
            Catch ex As Exception
                Throw New InvalidProgramException("A problem was detected during the certificate deletion process")
            End Try
        Next
        If TypeOf View Is DetailView AndAlso (CType(View, DetailView)).ViewEditMode = _
            ViewEditMode.View Then
            View.ObjectSpace.CommitChanges()
        End If
        If TypeOf View Is ListView Then
            ObjectSpace.CommitChanges()
        End If
    End Sub

    Private Sub AllowCardRequest_CustomizePopupWindowParams(sender As Object, e As CustomizePopupWindowParamsEventArgs) Handles AllowCardRequest.CustomizePopupWindowParams
        Dim task = CType(View.CurrentObject, MemberRegistration)
        View.ObjectSpace.SetModified(task)
        Dim objectSpace = Application.CreateObjectSpace()
        Dim sm = objectSpace.CreateObject(Of CardCancellationDetails)()
        e.View = Application.CreateDetailView(objectSpace, sm)
        sm.MemberNo = task.MemberNumber
        sm.SerialNo = task.CardSerialNo
    End Sub

    Private Sub AllowCardRequest_Execute(sender As System.Object, e As PopupWindowShowActionExecuteEventArgs) Handles AllowCardRequest.Execute
        Dim objectsToProcess As New ArrayList(e.SelectedObjects)
        If e.SelectedObjects.Count > 0 Then
            For Each obj As Object In objectsToProcess
                Dim objInNewObjectSpace = CType(ObjectSpace.GetObject(obj), MemberRegistration)
                objInNewObjectSpace.CardPrintTime = Nothing
                objInNewObjectSpace.RequestedBy = Nothing
                objInNewObjectSpace.Save()
            Next obj
        End If
        If TypeOf View Is DetailView AndAlso (CType(View, DetailView)).ViewEditMode = ViewEditMode.View Then
            View.ObjectSpace.CommitChanges()
            View.ObjectSpace.Refresh()
        End If
        If TypeOf View Is ListView Then
            ObjectSpace.CommitChanges()
            View.ObjectSpace.Refresh()
        End If
    End Sub

    Private Sub RequestCard_Execute(sender As System.Object, e As SimpleActionExecuteEventArgs) Handles RequestCard.Execute
        Dim objectsToProcess As New ArrayList(e.SelectedObjects)
        If e.SelectedObjects.Count > 0 Then
            For Each obj As Object In objectsToProcess
                Dim objInNewObjectSpace = CType(ObjectSpace.GetObject(obj), MemberRegistration)
                objInNewObjectSpace.CardPrintTime = "Requested"
                objInNewObjectSpace.RequestedBy = SecuritySystem.CurrentUser.ToString
                If objInNewObjectSpace.Category.IsScheme = True Then
                    objInNewObjectSpace.SchemeVehicle = objInNewObjectSpace.VehicleNumber
                End If
                objInNewObjectSpace.Save()
            Next obj
        End If
        If TypeOf View Is DetailView AndAlso (CType(View, DetailView)).ViewEditMode = _
            ViewEditMode.View Then
            View.ObjectSpace.CommitChanges()
            View.ObjectSpace.Refresh()
        End If
        If TypeOf View Is ListView Then
            ObjectSpace.CommitChanges()
            View.ObjectSpace.Refresh()
        End If
    End Sub
    Private Sub GenerateVoucher_CustomizePopupWindowParams(sender As Object, e As DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventArgs) Handles GenerateVoucher.CustomizePopupWindowParams
        Dim taskb = CType(View.CurrentObject, MemberRegistration)
        View.ObjectSpace.SetModified(taskb)
        Dim objectSpace = Application.CreateObjectSpace()
        Dim sm = objectSpace.CreateObject(Of Voucher)()
        e.View = Application.CreateDetailView(objectSpace, sm)
        sm.MemberNo = sm.Session.GetObjectByKey(Of MemberRegistration)(taskb.Oid)
    End Sub
End Class
