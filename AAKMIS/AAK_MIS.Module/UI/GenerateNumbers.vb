Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports DevExpress.Xpo.DB

Public Class GenerateNumbers
    Private Shared Property con As New Constants
    Public Shared Property uow As UnitOfWork
    Public Shared Sub GetUow()
        If uow Is Nothing Then
            Dim sqlconn = MSSqlConnectionProvider.GetConnectionString(con.server, con.user, con.password, con.DB)
            Dim dl = XpoDefault.GetDataLayer(sqlconn, AutoCreateOption.DatabaseAndSchema)
            uow = New UnitOfWork(dl)
        End If
    End Sub
    Public Shared Function GetNewOrderNumber() As Integer
        Dim number As Integer = 0
        GetUow()
        Using xpc As New XPCollection(Of Order)(uow)
            For Each rec In xpc
                If number < rec.OrderNo Then
                    number = rec.OrderNo
                End If
            Next
        End Using
        number += 1
        Return number
    End Function
    Public Shared Function GetNewApplicantRefNo() As Integer
        Dim number As Integer = 0
        GetUow()
        Using xpc As New XPCollection(Of Applicants)(uow)
            For Each rec In xpc
                If number < rec.RefNo Then
                    number = rec.RefNo
                End If
            Next
        End Using
        number += 1
        Return number
    End Function
    Public Shared Function GetNewNonMemberNo() As Integer
        Dim NonMemberNo As Integer = 1
        GetUow()
        Using xpc As New XPCollection(Of NonMember)(uow)
            For Each rec In xpc
                If (rec.ModifiedOn).Month = (Today).Month And (rec.ModifiedOn).Year = (Today).Year Then
                    NonMemberNo += 1
                End If
            Next
        End Using
        Return NonMemberNo
    End Function
    Public Shared Function GetNewJobNumber() As Integer
        Dim number As Integer = 1
        GetUow()
        Dim crit As CriteriaOperator = New BinaryOperator("Checked", False, BinaryOperatorType.Equal)
        Using xpc As New XPCollection(Of RescueDetails)(uow, crit)
            For Each rec In xpc
                number += 1
            Next
        End Using
        Return number
    End Function
    Public Shared Function GetBlacklistNo() As Integer
        Dim number As Integer = 0
        GetUow()
        Using xpc As New XPCollection(Of BlacklistDetails)(uow)
            For Each rec In xpc
                If number < rec.BlacklistNo Then
                    number = rec.BlacklistNo
                End If
            Next
        End Using
        number += 1
        Return number
    End Function
    Public Shared Function GetfDeletionlistNo() As Integer
        Dim number As Integer = 0
        GetUow()
        Using xpc As New XPCollection(Of DeletionDetails)(uow)
            For Each rec In xpc
                If number < rec.DeletionlistNo Then
                    number = rec.DeletionlistNo
                End If
            Next
        End Using
        number += 1
        Return number
    End Function
    Public Shared Function GetCertDeletionlistNo() As Integer
        Dim number As Integer = 0
        GetUow()
        Using xpc As New XPCollection(Of CertificateDeletionDetails)(uow)
            For Each rec In xpc
                If number < rec.DeletionlistNo Then
                    number = rec.DeletionlistNo
                End If
            Next
        End Using
        number += 1
        Return number
    End Function
    Public Shared Function GetCardCancellationlistNo() As Integer
        Dim number As Integer = 0
        GetUow()
        Using xpc As New XPCollection(Of CardCancellationDetails)(uow)
            For Each rec In xpc
                If number < rec.CardCancellationlistNo Then
                    number = rec.CardCancellationlistNo
                End If
            Next
        End Using
        number += 1
        Return number
    End Function
    Public Shared Function GetPreIssuelistNo() As Integer
        Dim number As Integer = 0
        GetUow()
        Using xpc As New XPCollection(Of IdpPreIssueDetails)(uow)
            For Each rec In xpc
                If number < rec.PreIssuelistNo Then
                    number = rec.PreIssuelistNo
                End If
            Next
        End Using
        number += 1
        Return number
    End Function
    Public Shared Function GenIDPCancelNo() As Integer
        Dim number As Integer = 0
        GetUow()
        Using xpc As New XPCollection(Of CancelReasons)(uow)
            For Each rec In xpc
                If number < rec.IDPCancelNo Then
                    number = rec.IDPCancelNo
                End If
            Next
        End Using
        number += 1
        Return number
    End Function
    Public Shared Function GenFeedbackNo() As Integer
        Dim number As Integer = 0
        GetUow()
        Using xpc As New XPCollection(Of ComplaintRegister)(uow)
            For Each rec In xpc
                If (rec.ModifiedOn).Month = (Today).Month And (rec.ModifiedOn).Year = (Today).Year Then
                    number += 1
                End If
            Next
        End Using
        number += 1
        Return number
    End Function
    Public Shared Function GenVoucherNo() As Integer
        Dim number As Integer = 0
        GetUow()
        Using xpc As New XPCollection(Of Voucher)(uow)
            For Each rec In xpc
                If (rec.ModifiedOn).Month = (Today).Month And (rec.ModifiedOn).Year = (Today).Year Then
                    number += 1
                End If
            Next
        End Using
        number += 1
        Return number
    End Function
    Public Shared Function GetAssessmentRptNo() As Integer
        Dim number As Integer = 0
        GetUow()
        Using xpc As New XPCollection(Of Assessment)(uow)
            For Each rec In xpc
                If number < rec.ReportNo Then
                    number = rec.ReportNo
                End If
            Next
        End Using
        number += 1
        Return number
    End Function
    Public Shared Function GetAdvAssessmentRptNo() As Integer
        Dim number As Integer = 0
        GetUow()
        Using xpc As New XPCollection(Of AdvancedAssessment)(uow)
            For Each rec In xpc
                If (rec.ModifiedOn).Month = (Today).Month And (rec.ModifiedOn).Year = (Today).Year Then
                    number += 1
                End If
            Next
            number += 1
        End Using
        Return number
    End Function
    Public Shared Function GetStartingIdp() As Integer
        Dim number As Integer = 0
        GetUow()
        Using xpc As New XPCollection(Of IDP_Allocation)(uow)
            For Each rec In xpc
                If number < rec.IDPNumber Then
                    number = rec.IDPNumber
                End If
            Next
        End Using
        number += 1
        Return number
    End Function
    Public Shared Function GetStartingIdpAllocationNo(ByVal IDPStatus As Integer) As Integer
        Dim number As Integer = 0
        Dim newIdp As Integer = 0
        GetUow()
        Dim crit As CriteriaOperator = New BinaryOperator("Status", IDPStatus, BinaryOperatorType.Equal)
        Using xpc As New XPCollection(Of IDP_Allocation)(uow, crit)
            For Each rec In xpc
                If rec.IDPNumber <> 0 Then
                    If number > rec.IDPNumber Then
                        number = rec.IDPNumber
                        newIdp = If(newIdp <= number, number, number)
                    ElseIf number = 0 Then
                        number = rec.IDPNumber
                    End If
                End If
            Next
        End Using
        Return number
    End Function
    Public Shared Function GetInvoiceNo(ByVal oid As String) As Integer
        Dim number As Integer = 1
        GetUow()
        Dim crit As CriteriaOperator = New BinaryOperator("ModeOfPayment", oid, BinaryOperatorType.Equal)
        Using xpc As New XPCollection(Of Payments)(uow, crit)
            For Each rec In xpc
                If Not rec.InvoiceNo Is Nothing And rec.DatePaid.Month = DateTime.Now.Month And rec.DatePaid.Year = DateTime.Now.Year Then
                    number += 1
                End If
            Next
        End Using
        Return number
    End Function

    Public Shared Function GetTotalIdps(ByVal IDPStatus As Integer) As Integer
        Dim total As Integer = 0
        GetUow()
        Dim crit As CriteriaOperator = New BinaryOperator("Status", IDPStatus, BinaryOperatorType.Equal)
        Using xpc As New XPCollection(Of IDP_Allocation)(uow, crit)
            total = xpc.Count
        End Using
        Return total
    End Function
End Class
