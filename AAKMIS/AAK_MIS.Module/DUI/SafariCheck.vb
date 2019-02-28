Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.Validation

Public Class SafariCheck
    Inherits SafariAssessment
    Private fDrivingLicence, fVehicleInsurance, fVehicleInspection, fFuel, fEngineOil, fWater, fBatteryWater, fBrakeFluid, _
        fWheelBrace, fJack, fFanBelt, fFireExtinguisher, fFirstAid, fWheelSpanner, fHazardTriangles, fSeatBelt, fSeatAdjustment, _
        fIgnition, fHorn, fMirrors, fIndicators, fBrakes, fWipers, fworkingLights, fSteering, fInstrument, fRimCondtion, fTires, _
        fCarCleanliness, fAllGlasses As AdvancedPerformance
    Private VarTotal As Double
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property DrivingLicence() As AdvancedPerformance
        Get
            Return fDrivingLicence
        End Get
        Set(ByVal value As AdvancedPerformance)
            SetPropertyValue("DrivingLicence", fDrivingLicence, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property VehicleInsurance() As AdvancedPerformance
        Get
            Return fVehicleInsurance
        End Get
        Set(ByVal value As AdvancedPerformance)
            SetPropertyValue("VehicleInsurance", fVehicleInsurance, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property VehicleInspection() As AdvancedPerformance
        Get
            Return fVehicleInspection
        End Get
        Set(ByVal value As AdvancedPerformance)
            SetPropertyValue("VehicleInspection", fVehicleInspection, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property Fuel() As AdvancedPerformance
        Get
            Return fFuel
        End Get
        Set(ByVal value As AdvancedPerformance)
            SetPropertyValue("Fuel", fFuel, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property EngineOil() As AdvancedPerformance
        Get
            Return fEngineOil
        End Get
        Set(ByVal value As AdvancedPerformance)
            SetPropertyValue("EngineOil", fEngineOil, value)
        End Set
    End Property
    <VisibleInListView(False)> _
<RuleRequiredField()> _
    Public Property Water() As AdvancedPerformance
        Get
            Return fWater
        End Get
        Set(ByVal value As AdvancedPerformance)
            SetPropertyValue("Water", fWater, value)
        End Set
    End Property
    <VisibleInListView(False)> _
<RuleRequiredField()> _
    Public Property BatteryWater() As AdvancedPerformance
        Get
            Return fBatteryWater
        End Get
        Set(ByVal value As AdvancedPerformance)
            SetPropertyValue("BatteryWater", fBatteryWater, value)
        End Set
    End Property
    <VisibleInListView(False)> _
<RuleRequiredField()> _
    Public Property BrakeAndClutchFluid() As AdvancedPerformance
        Get
            Return fBrakeFluid
        End Get
        Set(ByVal value As AdvancedPerformance)
            SetPropertyValue("BrakeClutchFluid", fBrakeFluid, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property WheelBrace() As AdvancedPerformance
        Get
            Return fWheelBrace
        End Get
        Set(ByVal value As AdvancedPerformance)
            SetPropertyValue("WheelBrace", fWheelBrace, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property Jack() As AdvancedPerformance
        Get
            Return fJack
        End Get
        Set(ByVal value As AdvancedPerformance)
            SetPropertyValue("Jack", fJack, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property FanBelt() As AdvancedPerformance
        Get
            Return fFanBelt
        End Get
        Set(ByVal value As AdvancedPerformance)
            SetPropertyValue("FanBelt", fFanBelt, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property FireExtinguisher() As AdvancedPerformance
        Get
            Return fFireExtinguisher
        End Get
        Set(ByVal value As AdvancedPerformance)
            SetPropertyValue("FireExtinguiher", fFireExtinguisher, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property FirstAidKit() As AdvancedPerformance
        Get
            Return fFirstAid
        End Get
        Set(ByVal value As AdvancedPerformance)
            SetPropertyValue("FirstAidKit", fFirstAid, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property WheelSpanner() As AdvancedPerformance
        Get
            Return fWheelSpanner
        End Get
        Set(ByVal value As AdvancedPerformance)
            SetPropertyValue("WheelSpanner", fWheelSpanner, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property HazardTriangles() As AdvancedPerformance
        Get
            Return fHazardTriangles
        End Get
        Set(ByVal value As AdvancedPerformance)
            SetPropertyValue("HazardTriangles", fHazardTriangles, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property SeatBelts() As AdvancedPerformance
        Get
            Return fSeatBelt
        End Get
        Set(ByVal value As AdvancedPerformance)
            SetPropertyValue("SeatBelt", fSeatBelt, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property SeatAdjustment() As AdvancedPerformance
        Get
            Return fSeatAdjustment
        End Get
        Set(ByVal value As AdvancedPerformance)
            SetPropertyValue("SeatAdjustment", fSeatAdjustment, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property IgnitionAndCharging() As AdvancedPerformance
        Get
            Return fIgnition
        End Get
        Set(ByVal value As AdvancedPerformance)
            SetPropertyValue("IgnitionAndCharging", fIgnition, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property Horn() As AdvancedPerformance
        Get
            Return fHorn
        End Get
        Set(ByVal value As AdvancedPerformance)
            SetPropertyValue("Horn", fHorn, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property Mirrors() As AdvancedPerformance
        Get
            Return fMirrors
        End Get
        Set(ByVal value As AdvancedPerformance)
            SetPropertyValue("Mirrors", fMirrors, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property Indicators() As AdvancedPerformance
        Get
            Return fIndicators
        End Get
        Set(ByVal value As AdvancedPerformance)
            SetPropertyValue("DrivingLicence", fIndicators, value)
        End Set
    End Property
    <VisibleInListView(False)> _
<RuleRequiredField()> _
    Public Property Brakes() As AdvancedPerformance
        Get
            Return fBrakes
        End Get
        Set(ByVal value As AdvancedPerformance)
            SetPropertyValue("Brakes", fBrakes, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property Wipers() As AdvancedPerformance
        Get
            Return fWipers
        End Get
        Set(ByVal value As AdvancedPerformance)
            SetPropertyValue("Wipers", fWipers, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property WorkingLights() As AdvancedPerformance
        Get
            Return fworkingLights
        End Get
        Set(ByVal value As AdvancedPerformance)
            SetPropertyValue("WorkingLights", fworkingLights, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property SteeringSystem() As AdvancedPerformance
        Get
            Return fSteering
        End Get
        Set(ByVal value As AdvancedPerformance)
            SetPropertyValue("SteeringSystem", fSteering, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property Instrument() As AdvancedPerformance
        Get
            Return fInstrument
        End Get
        Set(ByVal value As AdvancedPerformance)
            SetPropertyValue("Instrument", fInstrument, value)
        End Set
    End Property
    <VisibleInListView(False)> _
<RuleRequiredField()> _
    Public Property RimCondition() As AdvancedPerformance
        Get
            Return fRimCondtion
        End Get
        Set(ByVal value As AdvancedPerformance)
            SetPropertyValue("RimCondition", fRimCondtion, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property TiresAndPressure() As AdvancedPerformance
        Get
            Return fTires
        End Get
        Set(ByVal value As AdvancedPerformance)
            SetPropertyValue("TiresAndPressure", fTires, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property CarCleanliness() As AdvancedPerformance
        Get
            Return fCarCleanliness
        End Get
        Set(ByVal value As AdvancedPerformance)
            SetPropertyValue("CarCleanliness", fCarCleanliness, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property AllCleanGlasses() As AdvancedPerformance
        Get
            Return fAllGlasses
        End Get
        Set(ByVal value As AdvancedPerformance)
            SetPropertyValue("AllCleanGlasses", fAllGlasses, value)
        End Set
    End Property
    Protected Overrides Sub OnSaving()
        Try
            Dim NewVarOutOf As Int16 = 460
            'Dim properties As PropertyInfo() = GetType(SafariCheck).GetProperties()
            'For Each [property] As PropertyInfo In properties
            If DrivingLicence.Name = "N/A" Then
                NewVarOutOf -= 1
            End If
            If VehicleInsurance.Name = "N/A" Then
                NewVarOutOf -= 1
            End If
            If VehicleInspection.Name = "N/A" Then
                NewVarOutOf -= 1
            End If
            If Fuel.Name = "N/A" Then
                NewVarOutOf -= 1
            End If
            If EngineOil.Name = "N/A" Then
                NewVarOutOf -= 1
            End If
            If Water.Name = "N/A" Then
                NewVarOutOf -= 1
            End If
            If BatteryWater.Name = "N/A" Then
                NewVarOutOf -= 1
            End If
            If BrakeAndClutchFluid.Name = "N/A" Then
                NewVarOutOf -= 1
            End If
            If WheelBrace.Name = "N/A" Then
                NewVarOutOf -= 1
            End If
            If Jack.Name = "N/A" Then
                NewVarOutOf -= 1
            End If
            If FanBelt.Name = "N/A" Then
                NewVarOutOf -= 1
            End If
            If FireExtinguisher.Name = "N/A" Then
                NewVarOutOf -= 1
            End If
            If FirstAidKit.Name = "N/A" Then
                NewVarOutOf -= 1
            End If
            If WheelSpanner.Name = "N/A" Then
                NewVarOutOf -= 1
            End If
            If HazardTriangles.Name = "N/A" Then
                NewVarOutOf -= 1
            End If
            If SeatBelts.Name = "N/A" Then
                NewVarOutOf -= 1
            End If
            If SeatAdjustment.Name = "N/A" Then
                NewVarOutOf -= 1
            End If
            If IgnitionAndCharging.Name = "N/A" Then
                NewVarOutOf -= 1
            End If
            If Horn.Name = "N/A" Then
                NewVarOutOf -= 1
            End If
            If Mirrors.Name = "N/A" Then
                NewVarOutOf -= 1
            End If
            If Indicators.Name = "N/A" Then
                NewVarOutOf -= 1
            End If
            If Brakes.Name = "N/A" Then
                NewVarOutOf -= 1
            End If
            If Wipers.Name = "N/A" Then
                NewVarOutOf -= 1
            End If
            If WorkingLights.Name = "N/A" Then
                NewVarOutOf -= 1
            End If
            If SteeringSystem.Name = "N/A" Then
                NewVarOutOf -= 1
            End If
            If Instrument.Name = "N/A" Then
                NewVarOutOf -= 1
            End If
            If RimCondition.Name = "N/A" Then
                NewVarOutOf -= 1
            End If
            If TiresAndPressure.Name = "N/A" Then
                NewVarOutOf -= 1
            End If
            If CarCleanliness.Name = "N/A" Then
                NewVarOutOf -= 1
            End If
            If AllCleanGlasses.Name = "N/A" Then
                NewVarOutOf -= 1
            End If
            'Next
            VarTotal = DrivingLicence.Points + VehicleInsurance.Points + VehicleInspection.Points + Fuel.Points _
                     + EngineOil.Points + Water.Points + BatteryWater.Points + BrakeAndClutchFluid.Points + WheelBrace.Points _
                     + Jack.Points + FanBelt.Points + FireExtinguisher.Points + FirstAidKit.Points + WheelSpanner.Points _
                     + HazardTriangles.Points + SeatBelts.Points + SeatAdjustment.Points + IgnitionAndCharging.Points _
                     + Horn.Points + Mirrors.Points + Indicators.Points + Brakes.Points + Wipers.Points + WorkingLights.Points _
                     + SteeringSystem.Points + Instrument.Points + RimCondition.Points + TiresAndPressure.Points + _
                       CarCleanliness.Points + AllCleanGlasses.Points + _
                   (BSB + BDL + VCA + VCB + VCC + VCD + VCE + VCF + VCG + VCH + VCI + VCJ + VCK + VCL + VCM + VCN + _
                   TDA + TDB + TDC + TDD + TDE + TDF + TDG + TDH + TDI + TDJ + _
                   BRA + BRB + BRC + BRD + BRE + BRF + BRG + BRH + BRI + BRJ + BRK + BRL + BRM + BRN + BRO + BRP + BRQ + _
                   MRA + MRB + MRC + MRD + MRE + MRF + MRG + MRH + ULA + ULB + _
                   AWA + AWB + AWC + AWD + HCA + HCB + HCC)
            Score = (String.Format("{0}{1}", Math.Round(VarTotal * 100 / NewVarOutOf), "%"))
        Catch ex As Exception
            Throw
        End Try
        MyBase.OnSaving()
    End Sub
End Class