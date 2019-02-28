Partial Class MyController

	<System.Diagnostics.DebuggerNonUserCode()> _
	Public Sub New(ByVal Container As System.ComponentModel.IContainer)
		MyClass.New()

		'Required for Windows.Forms Class Composition Designer support
		Container.Add(Me)

	End Sub

	'Component overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> _
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing AndAlso components IsNot Nothing Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

	'Required by the Component Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Component Designer
	'It can be modified using the Component Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.RequestCard = New DevExpress.ExpressApp.Actions.SimpleAction(Me.components)
        Me.UnBlacklist = New DevExpress.ExpressApp.Actions.SimpleAction(Me.components)
        Me.BlacklistP = New DevExpress.ExpressApp.Actions.PopupWindowShowAction(Me.components)
        Me.CancelIDP = New DevExpress.ExpressApp.Actions.PopupWindowShowAction(Me.components)
        Me.ReceiveToStore = New DevExpress.ExpressApp.Actions.PopupWindowShowAction(Me.components)
        Me.AllocateToBranch = New DevExpress.ExpressApp.Actions.PopupWindowShowAction(Me.components)
        Me.MarkMissing = New DevExpress.ExpressApp.Actions.SimpleAction(Me.components)
        Me.ChangeCState = New DevExpress.ExpressApp.Actions.SingleChoiceAction(Me.components)
        Me.ChangeState = New DevExpress.ExpressApp.Actions.SingleChoiceAction(Me.components)
        Me.CloseMon = New DevExpress.ExpressApp.Actions.SimpleAction(Me.components)
        Me.FeedBack = New DevExpress.ExpressApp.Actions.PopupWindowShowAction(Me.components)
        Me.CloseF = New DevExpress.ExpressApp.Actions.SimpleAction(Me.components)
        Me.TakeAction = New DevExpress.ExpressApp.Actions.PopupWindowShowAction(Me.components)
        Me.Escalate = New DevExpress.ExpressApp.Actions.SimpleAction(Me.components)
        Me.AssignCard = New DevExpress.ExpressApp.Actions.PopupWindowShowAction(Me.components)
        Me.ProcessO = New DevExpress.ExpressApp.Actions.PopupWindowShowAction(Me.components)
        Me.PreIssueIDP = New DevExpress.ExpressApp.Actions.PopupWindowShowAction(Me.components)
        Me.DeleteCert = New DevExpress.ExpressApp.Actions.PopupWindowShowAction(Me.components)
        Me.AllowCardRequest = New DevExpress.ExpressApp.Actions.PopupWindowShowAction(Me.components)
        Me.GenerateVoucher = New DevExpress.ExpressApp.Actions.PopupWindowShowAction(Me.components)
        '
        'RequestCard
        '
        Me.RequestCard.Caption = "Request Card"
        Me.RequestCard.ConfirmationMessage = "Do you want to request a plastic card for this member?"
        Me.RequestCard.Id = "RequestCard"
        Me.RequestCard.ImageName = Nothing
        Me.RequestCard.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject
        Me.RequestCard.Shortcut = Nothing
        Me.RequestCard.Tag = Nothing
        Me.RequestCard.TargetObjectsCriteria = "ExpiryDate>'@CurrentDate' And CardPrintTime IS Null And Category<>'DSC' And Categ" & _
    "ory<>'VAL'"
        Me.RequestCard.TargetObjectType = GetType(AAK_MIS.[Module].MemberRegistration)
        Me.RequestCard.TargetViewId = Nothing
        Me.RequestCard.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView
        Me.RequestCard.ToolTip = Nothing
        Me.RequestCard.TypeOfView = GetType(DevExpress.ExpressApp.DetailView)
        '
        'UnBlacklist
        '
        Me.UnBlacklist.Caption = "Un Blacklist"
        Me.UnBlacklist.ConfirmationMessage = "Are you sure you want to unblacklist the member?"
        Me.UnBlacklist.Id = "UnBlacklist"
        Me.UnBlacklist.ImageName = Nothing
        Me.UnBlacklist.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject
        Me.UnBlacklist.Shortcut = Nothing
        Me.UnBlacklist.Tag = Nothing
        Me.UnBlacklist.TargetObjectsCriteria = "Blacklisted=True"
        Me.UnBlacklist.TargetObjectType = GetType(AAK_MIS.[Module].MemberRegistration)
        Me.UnBlacklist.TargetViewId = Nothing
        Me.UnBlacklist.TargetViewType = DevExpress.ExpressApp.ViewType.ListView
        Me.UnBlacklist.ToolTip = Nothing
        Me.UnBlacklist.TypeOfView = GetType(DevExpress.ExpressApp.ListView)
        '
        'BlacklistP
        '
        Me.BlacklistP.AcceptButtonCaption = Nothing
        Me.BlacklistP.CancelButtonCaption = Nothing
        Me.BlacklistP.Caption = "Blacklist"
        Me.BlacklistP.ConfirmationMessage = Nothing
        Me.BlacklistP.Id = "BlacklistP"
        Me.BlacklistP.ImageName = Nothing
        Me.BlacklistP.IsSizeable = False
        Me.BlacklistP.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject
        Me.BlacklistP.Shortcut = Nothing
        Me.BlacklistP.Tag = Nothing
        Me.BlacklistP.TargetObjectsCriteria = "Blacklisted=False"
        Me.BlacklistP.TargetObjectType = GetType(AAK_MIS.[Module].MemberRegistration)
        Me.BlacklistP.TargetViewId = Nothing
        Me.BlacklistP.TargetViewType = DevExpress.ExpressApp.ViewType.ListView
        Me.BlacklistP.ToolTip = Nothing
        Me.BlacklistP.TypeOfView = GetType(DevExpress.ExpressApp.ListView)
        '
        'CancelIDP
        '
        Me.CancelIDP.AcceptButtonCaption = Nothing
        Me.CancelIDP.CancelButtonCaption = Nothing
        Me.CancelIDP.Caption = "Cancel IDP"
        Me.CancelIDP.ConfirmationMessage = Nothing
        Me.CancelIDP.Id = "CancelIDP"
        Me.CancelIDP.ImageName = Nothing
        Me.CancelIDP.IsSizeable = False
        Me.CancelIDP.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject
        Me.CancelIDP.Shortcut = Nothing
        Me.CancelIDP.Tag = Nothing
        Me.CancelIDP.TargetObjectsCriteria = "Status='UnIssued'"
        Me.CancelIDP.TargetObjectType = GetType(AAK_MIS.[Module].IDP_Allocation)
        Me.CancelIDP.TargetViewId = Nothing
        Me.CancelIDP.TargetViewType = DevExpress.ExpressApp.ViewType.ListView
        Me.CancelIDP.ToolTip = Nothing
        Me.CancelIDP.TypeOfView = GetType(DevExpress.ExpressApp.ListView)
        '
        'ReceiveToStore
        '
        Me.ReceiveToStore.AcceptButtonCaption = ""
        Me.ReceiveToStore.CancelButtonCaption = Nothing
        Me.ReceiveToStore.Caption = "Receive To Store"
        Me.ReceiveToStore.ConfirmationMessage = Nothing
        Me.ReceiveToStore.Id = "ReceiveToStore"
        Me.ReceiveToStore.ImageName = Nothing
        Me.ReceiveToStore.Shortcut = Nothing
        Me.ReceiveToStore.Tag = Nothing
        Me.ReceiveToStore.TargetObjectsCriteria = Nothing
        Me.ReceiveToStore.TargetObjectType = GetType(AAK_MIS.[Module].IDP_Allocation)
        Me.ReceiveToStore.TargetViewId = Nothing
        Me.ReceiveToStore.TargetViewType = DevExpress.ExpressApp.ViewType.ListView
        Me.ReceiveToStore.ToolTip = Nothing
        Me.ReceiveToStore.TypeOfView = GetType(DevExpress.ExpressApp.ListView)
        '
        'AllocateToBranch
        '
        Me.AllocateToBranch.AcceptButtonCaption = Nothing
        Me.AllocateToBranch.CancelButtonCaption = Nothing
        Me.AllocateToBranch.Caption = "Allocate To Branch"
        Me.AllocateToBranch.ConfirmationMessage = Nothing
        Me.AllocateToBranch.Id = "AllocateToBranch"
        Me.AllocateToBranch.ImageName = Nothing
        Me.AllocateToBranch.Shortcut = Nothing
        Me.AllocateToBranch.Tag = Nothing
        Me.AllocateToBranch.TargetObjectsCriteria = Nothing
        Me.AllocateToBranch.TargetObjectType = GetType(AAK_MIS.[Module].IDP_Allocation)
        Me.AllocateToBranch.TargetViewId = Nothing
        Me.AllocateToBranch.TargetViewType = DevExpress.ExpressApp.ViewType.ListView
        Me.AllocateToBranch.ToolTip = Nothing
        Me.AllocateToBranch.TypeOfView = GetType(DevExpress.ExpressApp.ListView)
        '
        'MarkMissing
        '
        Me.MarkMissing.Caption = "Mark Missing"
        Me.MarkMissing.ConfirmationMessage = Nothing
        Me.MarkMissing.Id = "MarkMissing"
        Me.MarkMissing.ImageName = Nothing
        Me.MarkMissing.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject
        Me.MarkMissing.Shortcut = Nothing
        Me.MarkMissing.Tag = Nothing
        Me.MarkMissing.TargetObjectsCriteria = "Status='UnIssued'"
        Me.MarkMissing.TargetObjectType = GetType(AAK_MIS.[Module].IDP_Allocation)
        Me.MarkMissing.TargetViewId = Nothing
        Me.MarkMissing.TargetViewType = DevExpress.ExpressApp.ViewType.ListView
        Me.MarkMissing.ToolTip = Nothing
        Me.MarkMissing.TypeOfView = GetType(DevExpress.ExpressApp.ListView)
        '
        'ChangeCState
        '
        Me.ChangeCState.Caption = "Change Status"
        Me.ChangeCState.ConfirmationMessage = Nothing
        Me.ChangeCState.Id = "ChangeCState"
        Me.ChangeCState.ImageName = Nothing
        Me.ChangeCState.ItemType = DevExpress.ExpressApp.Actions.SingleChoiceActionItemType.ItemIsOperation
        Me.ChangeCState.Shortcut = Nothing
        Me.ChangeCState.Tag = Nothing
        Me.ChangeCState.TargetObjectsCriteria = Nothing
        Me.ChangeCState.TargetObjectType = GetType(AAK_MIS.[Module].Order)
        Me.ChangeCState.TargetViewId = Nothing
        Me.ChangeCState.ToolTip = Nothing
        Me.ChangeCState.TypeOfView = Nothing
        '
        'ChangeState
        '
        Me.ChangeState.Caption = "Change State"
        Me.ChangeState.ConfirmationMessage = "You are about to change the applicants status"
        Me.ChangeState.Id = "ChangeState"
        Me.ChangeState.ImageName = Nothing
        Me.ChangeState.ItemType = DevExpress.ExpressApp.Actions.SingleChoiceActionItemType.ItemIsOperation
        Me.ChangeState.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireMultipleObjects
        Me.ChangeState.Shortcut = Nothing
        Me.ChangeState.Tag = Nothing
        Me.ChangeState.TargetObjectsCriteria = Nothing
        Me.ChangeState.TargetObjectType = GetType(AAK_MIS.[Module].Applicants)
        Me.ChangeState.TargetViewId = Nothing
        Me.ChangeState.ToolTip = Nothing
        Me.ChangeState.TypeOfView = Nothing
        '
        'CloseMon
        '
        Me.CloseMon.ActionMeaning = DevExpress.ExpressApp.Actions.ActionMeaning.Accept
        Me.CloseMon.Caption = "Close Month"
        Me.CloseMon.ConfirmationMessage = "Closing month will reset the Rescue Jobs, Do you want to proceed?"
        Me.CloseMon.Id = "CloseMon"
        Me.CloseMon.ImageName = Nothing
        Me.CloseMon.Shortcut = Nothing
        Me.CloseMon.Tag = Nothing
        Me.CloseMon.TargetObjectsCriteria = Nothing
        Me.CloseMon.TargetObjectType = GetType(AAK_MIS.[Module].RescueDetails)
        Me.CloseMon.TargetViewId = Nothing
        Me.CloseMon.TargetViewType = DevExpress.ExpressApp.ViewType.ListView
        Me.CloseMon.ToolTip = Nothing
        Me.CloseMon.TypeOfView = GetType(DevExpress.ExpressApp.ListView)
        '
        'FeedBack
        '
        Me.FeedBack.AcceptButtonCaption = Nothing
        Me.FeedBack.CancelButtonCaption = Nothing
        Me.FeedBack.Caption = "Feed Back"
        Me.FeedBack.ConfirmationMessage = Nothing
        Me.FeedBack.Id = "FeedBack"
        Me.FeedBack.ImageName = Nothing
        Me.FeedBack.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject
        Me.FeedBack.Shortcut = Nothing
        Me.FeedBack.Tag = Nothing
        Me.FeedBack.TargetObjectsCriteria = Nothing
        Me.FeedBack.TargetObjectType = GetType(AAK_MIS.[Module].MemberRegistration)
        Me.FeedBack.TargetViewId = Nothing
        Me.FeedBack.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView
        Me.FeedBack.ToolTip = Nothing
        Me.FeedBack.TypeOfView = GetType(DevExpress.ExpressApp.DetailView)
        '
        'CloseF
        '
        Me.CloseF.Caption = "Close"
        Me.CloseF.ConfirmationMessage = Nothing
        Me.CloseF.Id = "CloseF"
        Me.CloseF.ImageName = Nothing
        Me.CloseF.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject
        Me.CloseF.Shortcut = Nothing
        Me.CloseF.Tag = Nothing
        Me.CloseF.TargetObjectsCriteria = "Status='Pending' Or Status='Escalated'"
        Me.CloseF.TargetObjectType = GetType(AAK_MIS.[Module].ComplaintRegister)
        Me.CloseF.TargetViewId = Nothing
        Me.CloseF.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView
        Me.CloseF.ToolTip = Nothing
        Me.CloseF.TypeOfView = GetType(DevExpress.ExpressApp.DetailView)
        '
        'TakeAction
        '
        Me.TakeAction.AcceptButtonCaption = Nothing
        Me.TakeAction.CancelButtonCaption = Nothing
        Me.TakeAction.Caption = "Take Action"
        Me.TakeAction.ConfirmationMessage = Nothing
        Me.TakeAction.Id = "TakeAction"
        Me.TakeAction.ImageName = Nothing
        Me.TakeAction.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject
        Me.TakeAction.Shortcut = Nothing
        Me.TakeAction.Tag = Nothing
        Me.TakeAction.TargetObjectsCriteria = "Status<>'Closed'"
        Me.TakeAction.TargetObjectType = GetType(AAK_MIS.[Module].ComplaintRegister)
        Me.TakeAction.TargetViewId = Nothing
        Me.TakeAction.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView
        Me.TakeAction.ToolTip = Nothing
        Me.TakeAction.TypeOfView = GetType(DevExpress.ExpressApp.DetailView)
        '
        'Escalate
        '
        Me.Escalate.Caption = "Escalate"
        Me.Escalate.ConfirmationMessage = Nothing
        Me.Escalate.Id = "Escalate"
        Me.Escalate.ImageName = Nothing
        Me.Escalate.Shortcut = Nothing
        Me.Escalate.Tag = Nothing
        Me.Escalate.TargetObjectsCriteria = "Status<>'Closed' And Status<>'Escalated'"
        Me.Escalate.TargetObjectType = GetType(AAK_MIS.[Module].ComplaintRegister)
        Me.Escalate.TargetViewId = Nothing
        Me.Escalate.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView
        Me.Escalate.ToolTip = Nothing
        Me.Escalate.TypeOfView = GetType(DevExpress.ExpressApp.DetailView)
        '
        'AssignCard
        '
        Me.AssignCard.AcceptButtonCaption = Nothing
        Me.AssignCard.CancelButtonCaption = Nothing
        Me.AssignCard.Caption = "Assign Card"
        Me.AssignCard.ConfirmationMessage = Nothing
        Me.AssignCard.Id = "AssignCard"
        Me.AssignCard.ImageName = Nothing
        Me.AssignCard.IsSizeable = False
        Me.AssignCard.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject
        Me.AssignCard.Shortcut = Nothing
        Me.AssignCard.Tag = Nothing
        Me.AssignCard.TargetObjectsCriteria = "MembershipStatus='Valid'"
        Me.AssignCard.TargetObjectType = GetType(AAK_MIS.[Module].MemberRegistration)
        Me.AssignCard.TargetViewId = Nothing
        Me.AssignCard.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView
        Me.AssignCard.ToolTip = Nothing
        Me.AssignCard.TypeOfView = GetType(DevExpress.ExpressApp.DetailView)
        '
        'ProcessO
        '
        Me.ProcessO.AcceptButtonCaption = Nothing
        Me.ProcessO.CancelButtonCaption = Nothing
        Me.ProcessO.Caption = "Process Order"
        Me.ProcessO.ConfirmationMessage = Nothing
        Me.ProcessO.Id = "ProcessO"
        Me.ProcessO.ImageName = Nothing
        Me.ProcessO.Shortcut = Nothing
        Me.ProcessO.Tag = Nothing
        Me.ProcessO.TargetObjectsCriteria = "[Status]<>'Processed'"
        Me.ProcessO.TargetObjectType = GetType(AAK_MIS.[Module].Order)
        Me.ProcessO.TargetViewId = Nothing
        Me.ProcessO.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView
        Me.ProcessO.ToolTip = Nothing
        Me.ProcessO.TypeOfView = GetType(DevExpress.ExpressApp.DetailView)
        '
        'PreIssueIDP
        '
        Me.PreIssueIDP.AcceptButtonCaption = Nothing
        Me.PreIssueIDP.CancelButtonCaption = Nothing
        Me.PreIssueIDP.Caption = "Pre-Issue IDP"
        Me.PreIssueIDP.ConfirmationMessage = Nothing
        Me.PreIssueIDP.Id = "PreIssueIDP"
        Me.PreIssueIDP.ImageName = Nothing
        Me.PreIssueIDP.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject
        Me.PreIssueIDP.Shortcut = Nothing
        Me.PreIssueIDP.Tag = Nothing
        Me.PreIssueIDP.TargetObjectsCriteria = "IDPExpiryDate>='@CurrentDate'"
        Me.PreIssueIDP.TargetObjectType = GetType(AAK_MIS.[Module].IDP_Details)
        Me.PreIssueIDP.TargetViewId = Nothing
        Me.PreIssueIDP.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView
        Me.PreIssueIDP.ToolTip = Nothing
        Me.PreIssueIDP.TypeOfView = GetType(DevExpress.ExpressApp.DetailView)
        '
        'DeleteCert
        '
        Me.DeleteCert.AcceptButtonCaption = Nothing
        Me.DeleteCert.CancelButtonCaption = Nothing
        Me.DeleteCert.Caption = "Cancel Certificate"
        Me.DeleteCert.ConfirmationMessage = Nothing
        Me.DeleteCert.Id = "DeleteCert"
        Me.DeleteCert.ImageName = Nothing
        Me.DeleteCert.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject
        Me.DeleteCert.Shortcut = Nothing
        Me.DeleteCert.Tag = Nothing
        Me.DeleteCert.TargetObjectsCriteria = Nothing
        Me.DeleteCert.TargetObjectType = GetType(AAK_MIS.[Module].DriverCertification)
        Me.DeleteCert.TargetViewId = Nothing
        Me.DeleteCert.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView
        Me.DeleteCert.ToolTip = Nothing
        Me.DeleteCert.TypeOfView = GetType(DevExpress.ExpressApp.DetailView)
        '
        'AllowCardRequest
        '
        Me.AllowCardRequest.AcceptButtonCaption = Nothing
        Me.AllowCardRequest.CancelButtonCaption = Nothing
        Me.AllowCardRequest.Caption = "Allow Card Request"
        Me.AllowCardRequest.ConfirmationMessage = "Do you want to allow plastic card Re-Request for this member?"
        Me.AllowCardRequest.Id = "AllowCardRequest"
        Me.AllowCardRequest.ImageName = Nothing
        Me.AllowCardRequest.Shortcut = Nothing
        Me.AllowCardRequest.Tag = Nothing
        Me.AllowCardRequest.TargetObjectsCriteria = "CardPrintTime IS Not Null"
        Me.AllowCardRequest.TargetObjectType = GetType(AAK_MIS.[Module].MemberRegistration)
        Me.AllowCardRequest.TargetViewId = Nothing
        Me.AllowCardRequest.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView
        Me.AllowCardRequest.ToolTip = Nothing
        Me.AllowCardRequest.TypeOfView = GetType(DevExpress.ExpressApp.DetailView)
        '
        'GenerateVoucher
        '
        Me.GenerateVoucher.AcceptButtonCaption = Nothing
        Me.GenerateVoucher.CancelButtonCaption = Nothing
        Me.GenerateVoucher.Caption = "Generate Voucher"
        Me.GenerateVoucher.ConfirmationMessage = Nothing
        Me.GenerateVoucher.Id = "GenerateVoucher"
        Me.GenerateVoucher.ImageName = Nothing
        Me.GenerateVoucher.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject
        Me.GenerateVoucher.Shortcut = Nothing
        Me.GenerateVoucher.Tag = Nothing
        Me.GenerateVoucher.TargetObjectsCriteria = "HasPreInsValuation=='True' Or HasFullMechanical=='True' Or HasFreeWheelAlignment=" & _
    "='True'"
        Me.GenerateVoucher.TargetObjectType = GetType(AAK_MIS.[Module].MemberRegistration)
        Me.GenerateVoucher.TargetViewId = Nothing
        Me.GenerateVoucher.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView
        Me.GenerateVoucher.ToolTip = Nothing
        Me.GenerateVoucher.TypeOfView = GetType(DevExpress.ExpressApp.DetailView)
        '
        'MyController
        '

    End Sub
    Friend WithEvents UnBlacklist As DevExpress.ExpressApp.Actions.SimpleAction
    Friend WithEvents BlacklistP As DevExpress.ExpressApp.Actions.PopupWindowShowAction
    Friend WithEvents CancelIDP As DevExpress.ExpressApp.Actions.PopupWindowShowAction
    Friend WithEvents ReceiveToStore As DevExpress.ExpressApp.Actions.PopupWindowShowAction
    Friend WithEvents AllocateToBranch As DevExpress.ExpressApp.Actions.PopupWindowShowAction
    Friend WithEvents MarkMissing As DevExpress.ExpressApp.Actions.SimpleAction
    Friend WithEvents ChangeCState As DevExpress.ExpressApp.Actions.SingleChoiceAction
    Friend WithEvents ChangeState As DevExpress.ExpressApp.Actions.SingleChoiceAction
    Private WithEvents CloseMon As DevExpress.ExpressApp.Actions.SimpleAction
    Friend WithEvents FeedBack As DevExpress.ExpressApp.Actions.PopupWindowShowAction
    Friend WithEvents CloseF As DevExpress.ExpressApp.Actions.SimpleAction
    Friend WithEvents TakeAction As DevExpress.ExpressApp.Actions.PopupWindowShowAction
    Friend WithEvents Escalate As DevExpress.ExpressApp.Actions.SimpleAction
    Friend WithEvents AssignCard As DevExpress.ExpressApp.Actions.PopupWindowShowAction
    Friend WithEvents ProcessO As DevExpress.ExpressApp.Actions.PopupWindowShowAction
    Friend WithEvents RequestCard As DevExpress.ExpressApp.Actions.SimpleAction
    Friend WithEvents PreIssueIDP As DevExpress.ExpressApp.Actions.PopupWindowShowAction
    Friend WithEvents DeleteCert As DevExpress.ExpressApp.Actions.PopupWindowShowAction
    Friend WithEvents AllowCardRequest As DevExpress.ExpressApp.Actions.PopupWindowShowAction
    Friend WithEvents GenerateVoucher As DevExpress.ExpressApp.Actions.PopupWindowShowAction

End Class
