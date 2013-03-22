Friend Module FluidProperties

#Region "Fluid Data Arrays"
    'C2*** Number of fluids
    Private Const NSECC As Integer = 2
    'C2*** Arrays for viscosity coefficient data
    Private B(NSECC) As Double, C(NSECC) As Double, D(NSECC) As Double, E(NSECC) As Double
    Private A1(NSECC) As Double, A2(NSECC) As Double, A3(NSECC) As Double, A4(NSECC) As Double, A5(NSECC) As Double, A6(NSECC) As Double, A7(NSECC) As Double
    Private A8(NSECC) As Double, A9(NSECC) As Double, A10(NSECC) As Double, A11(NSECC) As Double, A12(NSECC) As Double, A13(NSECC) As Double, A14(NSECC) As Double
    Private A15(NSECC) As Double, A16(NSECC) As Double, A17(NSECC) As Double, A18(NSECC) As Double, A19(NSECC) As Double, A20(NSECC) As Double, A21(NSECC) As Double, A22(NSECC) As Double
    Private A23(NSECC) As Double, A24(NSECC) As Double, A25(NSECC) As Double, A26(NSECC) As Double, A27(NSECC) As Double, A28(NSECC) As Double, A29(NSECC) As Double, A30(NSECC) As Double
    Private A31(NSECC) As Double, A32(NSECC) As Double, A33(NSECC) As Double, A34(NSECC) As Double, A35(NSECC) As Double
    'C2*** Arrays for conductivity coefficient data
    Private B0(NSECC) As Double, B1(NSECC) As Double, B2(NSECC) As Double, B3(NSECC) As Double, B4(NSECC) As Double, B5(NSECC) As Double, B6(NSECC) As Double, B7(NSECC) As Double
    Private B8(NSECC) As Double, B9(NSECC) As Double, B10(NSECC) As Double, B11(NSECC) As Double, B12(NSECC) As Double, B13(NSECC) As Double, B14(NSECC) As Double
    Private B15(NSECC) As Double, B16(NSECC) As Double, B17(NSECC) As Double, B18(NSECC) As Double, B19(NSECC) As Double, B20(NSECC) As Double
    'C2*** Arrays for density coefficient data
    Private BD(NSECC) As Double, CD(NSECC) As Double, DD(NSECC) As Double, ED(NSECC) As Double, FD(NSECC) As Double, GD(NSECC) As Double, HD(NSECC) As Double
    Private C1(NSECC) As Double, C2(NSECC) As Double, C3(NSECC) As Double, C4(NSECC) As Double, C5(NSECC) As Double, C6(NSECC) As Double, C7(NSECC) As Double
    Private C8(NSECC) As Double, C9(NSECC) As Double, C10(NSECC) As Double, C11(NSECC) As Double, C12(NSECC) As Double, C13(NSECC) As Double, C14(NSECC) As Double
    Private C15(NSECC) As Double, C16(NSECC) As Double, C17(NSECC) As Double, C18(NSECC) As Double, C19(NSECC) As Double, C20(NSECC) As Double, C21(NSECC) As Double, C22(NSECC) As Double
    Private C23(NSECC) As Double, C24(NSECC) As Double, C25(NSECC) As Double, C26(NSECC) As Double, C27(NSECC) As Double, C28(NSECC) As Double, C29(NSECC) As Double, C30(NSECC) As Double
    Private C31(NSECC) As Double, C32(NSECC) As Double, C33(NSECC) As Double, C34(NSECC) As Double, C35(NSECC) As Double
    'C2*** Arrays for specific heat coefficient data
    Private D0(NSECC) As Double, D1(NSECC) As Double, D2(NSECC) As Double, D3(NSECC) As Double, D4(NSECC) As Double, D5(NSECC) As Double, D6(NSECC) As Double, D7(NSECC) As Double
    Private D8(NSECC) As Double, D9(NSECC) As Double, D10(NSECC) As Double, D11(NSECC) As Double, D12(NSECC) As Double, D13(NSECC) As Double, D14(NSECC) As Double
    Private D15(NSECC) As Double, D16(NSECC) As Double, D17(NSECC) As Double, D18(NSECC) As Double, D19(NSECC) As Double, D20(NSECC) As Double, D21(NSECC) As Double, D22(NSECC) As Double
    Private D23(NSECC) As Double, D24(NSECC) As Double, D25(NSECC) As Double, D26(NSECC) As Double, D27(NSECC) As Double, D28(NSECC) As Double, D29(NSECC) As Double, D30(NSECC) As Double
    Private D31(NSECC) As Double, D32(NSECC) As Double, D33(NSECC) As Double, D34(NSECC) As Double, D35(NSECC) As Double
#End Region

#Region "Initialization Routines"
    Private Sub InitArrayOfSingles(ByVal A() As Single, ByVal ParamArray vals() As Object)
        Dim VALUE As Object
        Dim count As Integer
        count = LBound(A)
        For Each VALUE In vals
            A(count + 1) = VALUE
            count = count + 1
        Next VALUE
    End Sub

    Private Sub InitArrayOfDoubles(ByVal A() As Double, ByVal ParamArray vals() As Object)
        Dim VALUE As Object
        Dim count As Integer
        count = LBound(A)
        For Each VALUE In vals
            A(count + 1) = VALUE
            count = count + 1
        Next VALUE
    End Sub

    Private Sub InitArrayOfIntegers(ByVal A() As Integer, ByVal ParamArray vals() As Object)
        Dim VALUE As Object
        Dim count As Integer
        count = LBound(A)
        For Each VALUE In vals
            A(count + 1) = VALUE
            count = count + 1
        Next VALUE
    End Sub

    Sub InitUTILSECC()
        'C2*** Viscosity coefficient data
        Call InitArrayOfDoubles(B, -6.95056, -0.943399)
        Call InitArrayOfDoubles(C, 163.912, 173.981)
        Call InitArrayOfDoubles(D, -0.00225036, -0.00306936)
        Call InitArrayOfDoubles(E, -0.00566376, -0.00902707)
        Call InitArrayOfDoubles(A1, 0.0366108, 0.00108897)
        Call InitArrayOfDoubles(A2, -0.0129957, 0.00357364)
        Call InitArrayOfDoubles(A3, 0.00175258, -0.00043797)
        Call InitArrayOfDoubles(A4, 0.000406074, -0.000180156)
        Call InitArrayOfDoubles(A5, 0.00151259, 0.00205603)
        Call InitArrayOfDoubles(A6, -0.000011799, -0.000000758126)
        Call InitArrayOfDoubles(A7, -0.0000326055, -0.0000784913)
        Call InitArrayOfDoubles(A8, -0.0000937391, -0.0000134743)
        Call InitArrayOfDoubles(A9, -0.0000412939, 0.0000253759)
        Call InitArrayOfDoubles(A10, 0.000000115629, 0.0000000612614)
        Call InitArrayOfDoubles(A11, 0.000000406194, 0.00000129791)
        Call InitArrayOfDoubles(A12, 0.000000531125, -0.000000299907)
        Call InitArrayOfDoubles(A13, 0.00000206139, 0.00000117963)
        Call InitArrayOfDoubles(A14, 0.00000130468, -0.000000602621)
        Call InitArrayOfDoubles(A15, -0.000000000459729, -0.000000000534869)
        Call InitArrayOfDoubles(A16, -0.00000000380332, 0.000000000542488)
        Call InitArrayOfDoubles(A17, -0.00000000923278, 0.00000000711767)
        Call InitArrayOfDoubles(A18, -0.00000000424388, -0.0000000069959)
        Call InitArrayOfDoubles(A19, -0.0000000192148, 0.00000000532905)
        Call InitArrayOfDoubles(A20, -0.0000000232591, -0.0000000282225)
        Call InitArrayOfDoubles(A21, -0.000000000000178126, 0.000000000000576279)
        Call InitArrayOfDoubles(A22, 0.000000000014605, 0.0000000000110551)
        Call InitArrayOfDoubles(A23, 0.0000000000340806, -0.00000000004129)
        Call InitArrayOfDoubles(A24, 0.0000000000778992, -0.000000000022358)
        Call InitArrayOfDoubles(A25, 0.000000000127753, -0.0000000000408549)
        Call InitArrayOfDoubles(A26, 0.000000000140797, 0.000000000289252)
        Call InitArrayOfDoubles(A27, 0.000000000031736, -0.0000000000301727)
        Call InitArrayOfDoubles(A28, 0.00000000000000442725, 0.00000000000000501319)
        Call InitArrayOfDoubles(A29, -0.000000000000024498, -0.0000000000000400588)
        Call InitArrayOfDoubles(A30, -0.0000000000000481202, 0.0000000000000195942)
        Call InitArrayOfDoubles(A31, -0.00000000000012404, 0.000000000000241919)
        Call InitArrayOfDoubles(A32, -0.000000000000251696, -0.000000000000202376)
        Call InitArrayOfDoubles(A33, -0.000000000000295491, 0.000000000000287234)
        Call InitArrayOfDoubles(A34, -0.000000000000382364, -0.00000000000110334)
        Call InitArrayOfDoubles(A35, -0.000000000000095889, 0.00000000000030704)

        'C2*** Conductivity coefficient data
        Call InitArrayOfDoubles(B0, 0.568724, 0.568732)
        Call InitArrayOfDoubles(B1, 0.00182466, 0.00181676)
        Call InitArrayOfDoubles(B2, -0.006221, -0.00611616)
        Call InitArrayOfDoubles(B3, -0.0000131792, -0.0000101895)
        Call InitArrayOfDoubles(B4, -0.00000507752, -0.0000048098)
        Call InitArrayOfDoubles(B5, 0.0000641775, 0.0000682912)
        Call InitArrayOfDoubles(B6, -0.0000000303392, -0.0000000321451)
        Call InitArrayOfDoubles(B7, -0.000000817547, -0.000000840983)
        Call InitArrayOfDoubles(B8, -0.000000346639, -0.000000316404)
        Call InitArrayOfDoubles(B9, -0.0000000577491, -0.0000000556239)
        Call InitArrayOfDoubles(B10, 0.000000000121555, 0.000000000105263)
        Call InitArrayOfDoubles(B11, 0.00000000631964, 0.00000000675588)
        Call InitArrayOfDoubles(B12, 0.000000000657355, 0.000000000749031)
        Call InitArrayOfDoubles(B13, 0.00000000407955, 0.00000000382458)
        Call InitArrayOfDoubles(B14, 0.00000000219313, 0.00000000201274)
        Call InitArrayOfDoubles(B15, -0.000000000000311629, -0.000000000000198081)
        Call InitArrayOfDoubles(B16, -0.00000000000057424, -0.000000000000736164)
        Call InitArrayOfDoubles(B17, -0.00000000000405273, -0.00000000000473463)
        Call InitArrayOfDoubles(B18, -0.00000000001912, -0.0000000000221541)
        Call InitArrayOfDoubles(B19, -0.0000000000111308, -0.000000000010024)
        Call InitArrayOfDoubles(B20, -0.0000000000136301, -0.0000000000126727)

        'C2*** Density coefficient data
        Call InitArrayOfDoubles(BD, 1025.36, 1025.33)
        Call InitArrayOfDoubles(CD, 348.195, 348.196)
        Call InitArrayOfDoubles(DD, 0.0139658, 0.00418383)
        Call InitArrayOfDoubles(ED, -0.0525256, -0.00799824)
        Call InitArrayOfDoubles(FD, 2.30014, 1.66945)
        Call InitArrayOfDoubles(GD, -0.00294967, -0.00844228)
        Call InitArrayOfDoubles(HD, 0.110511, 0.827781)
        Call InitArrayOfDoubles(C1, -0.134213, 0.951519)
        Call InitArrayOfDoubles(C2, -0.507492, -0.299951)
        Call InitArrayOfDoubles(C3, 0.0342052, -0.0143923)
        Call InitArrayOfDoubles(C4, -0.0255121, -0.0195601)
        Call InitArrayOfDoubles(C5, 0.0232799, 0.0162627)
        Call InitArrayOfDoubles(C6, 0.00016157, 0.000069527)
        Call InitArrayOfDoubles(C7, -0.0030165, -0.00122186)
        Call InitArrayOfDoubles(C8, 0.0000587673, 0.00104618)
        Call InitArrayOfDoubles(C9, 0.000403944, 0.000674293)
        Call InitArrayOfDoubles(C10, 0.0000000810322, -0.000000658244)
        Call InitArrayOfDoubles(C11, 0.000113668, 0.0000328315)
        Call InitArrayOfDoubles(C12, -0.0000139294, -0.00000586954)
        Call InitArrayOfDoubles(C13, 0.00000198906, -0.0000191972)
        Call InitArrayOfDoubles(C14, 0.00000867231, -0.0000171165)
        Call InitArrayOfDoubles(C15, -0.00000000339459, 0.00000000287607)
        Call InitArrayOfDoubles(C16, 0.0000000496386, 0.0000000285855)
        Call InitArrayOfDoubles(C17, 0.000000138696, 0.000000103903)
        Call InitArrayOfDoubles(C18, -0.00000175018, -0.000000449538)
        Call InitArrayOfDoubles(C19, -0.0000000506644, 0.000000249831)
        Call InitArrayOfDoubles(C20, -0.000000519621, 0.000000155223)
        Call InitArrayOfDoubles(C21, 0.0000000000179511, -0.000000000000922601)
        Call InitArrayOfDoubles(C22, 0.0000000000730338, -0.0000000000726616)
        Call InitArrayOfDoubles(C23, -0.00000000147862, -0.000000000340197)
        Call InitArrayOfDoubles(C24, 0.00000000138577, -0.000000000830652)
        Call InitArrayOfDoubles(C25, -0.000000000828419, -0.00000000177803)
        Call InitArrayOfDoubles(C26, 0.00000000614393, -0.000000000422094)
        Call InitArrayOfDoubles(C27, 0.0000000129467, 0.0000000031225)
        Call InitArrayOfDoubles(C28, -0.0000000000000584431, -0.0000000000000104417)
        Call InitArrayOfDoubles(C29, 0.0000000000000438148, 0.0000000000000435702)
        Call InitArrayOfDoubles(C30, -0.000000000000711065, 0.000000000000481215)
        Call InitArrayOfDoubles(C31, 0.00000000000969321, 0.00000000000115885)
        Call InitArrayOfDoubles(C32, -0.0000000000143639, 0.00000000000254618)
        Call InitArrayOfDoubles(C33, 0.00000000000273885, 0.00000000000498945)
        Call InitArrayOfDoubles(C34, -0.0000000000168499, -0.000000000000508205)
        Call InitArrayOfDoubles(C35, -0.0000000000392875, -0.00000000000870531)

        'C2*** Specific heat coefficient data
        Call InitArrayOfDoubles(D0, 4.21464, 4.21469)
        Call InitArrayOfDoubles(D1, -0.00292342, -0.00289105)
        Call InitArrayOfDoubles(D2, -0.0225631, -0.0379582)
        Call InitArrayOfDoubles(D3, 0.000554484, 0.000585034)
        Call InitArrayOfDoubles(D4, 0.0000887834, 0.0000835502)
        Call InitArrayOfDoubles(D5, 0.000902108, 0.00157346)
        Call InitArrayOfDoubles(D6, -0.00000141107, -0.00000118943)
        Call InitArrayOfDoubles(D7, -0.0000374691, -0.0000593093)
        Call InitArrayOfDoubles(D8, -0.0000240183, -0.0000264535)
        Call InitArrayOfDoubles(D9, -0.00000665052, -0.00000659344)
        Call InitArrayOfDoubles(D10, 0.0000000141562, 0.0000000100889)
        Call InitArrayOfDoubles(D11, 0.000000815618, 0.00000121165)
        Call InitArrayOfDoubles(D12, 0.0000000627311, 0.0000000594668)
        Call InitArrayOfDoubles(D13, 0.000000580654, 0.000000664327)
        Call InitArrayOfDoubles(D14, 0.000000202008, 0.000000204251)
        Call InitArrayOfDoubles(D15, -0.0000000000797226, -0.0000000000437913)
        Call InitArrayOfDoubles(D16, -0.000000000403659, -0.000000000344571)
        Call InitArrayOfDoubles(D17, -0.00000000105033, -0.00000000108344)
        Call InitArrayOfDoubles(D18, -0.0000000104993, -0.0000000140108)
        Call InitArrayOfDoubles(D19, -0.00000000312716, -0.00000000313974)
        Call InitArrayOfDoubles(D20, -0.00000000749134, -0.00000000899339)
        Call InitArrayOfDoubles(D21, 0.000000000000217114, 0.000000000000072736)
        Call InitArrayOfDoubles(D22, 0.00000000000143055, 0.00000000000102028)
        Call InitArrayOfDoubles(D23, 0.00000000000374195, 0.00000000000371761)
        Call InitArrayOfDoubles(D24, 0.00000000000802307, 0.00000000000863592)
        Call InitArrayOfDoubles(D25, 0.000000000024479, 0.000000000024077)
        Call InitArrayOfDoubles(D26, 0.0000000000491625, 0.0000000000625157)
        Call InitArrayOfDoubles(D27, 0.0000000000734995, 0.0000000000858223)
        Call InitArrayOfDoubles(D28, -0.000000000000000185973, 5.07277E-18)
        Call InitArrayOfDoubles(D29, -0.00000000000000207296, -0.00000000000000105783)
        Call InitArrayOfDoubles(D30, -0.00000000000000590016, -0.0000000000000053735)
        Call InitArrayOfDoubles(D31, -0.0000000000000116874, -0.0000000000000128654)
        Call InitArrayOfDoubles(D32, -0.0000000000000238409, -0.0000000000000257684)
        Call InitArrayOfDoubles(D33, -0.0000000000000766383, -0.0000000000000736722)
        Call InitArrayOfDoubles(D34, -0.000000000000128749, -0.000000000000175126)
        Call InitArrayOfDoubles(D35, -0.000000000000215016, -0.000000000000216781)
    End Sub
#End Region

#Region "Secondary Properties"
#Region "Property routine descriptions"
    'Public Function SECCVISC(ByVal CONCENT As Single, ByVal TEMP As Single, ByVal ISECC As Integer)
    'C * PURPOSE:  Calculate the dynamic viscosity of secondary coolants
    'C * CONCENT  : solution concentration                                (%)
    'C * TEMP     : Fluid temperature                                     (C)
    'C * ISECC    : selected secondary coolant                            (-)
    'C * SECCVISC: Dynamic viscosity                        (N/m^2.s or Pa.s)

    'Public Function SECCCOND(ByVal CONCENT As Single, ByVal TEMP As Single, ByVal ISECC As Integer)
    'C * PURPOSE:  Calculate the Thermal conductivity of secondary coolants
    'C * CONCENT  : solution concentration                                (%)
    'C * TEMP     : Fluid temperature                                     (C)
    'C * ISECC    : selected secondary coolant                            (-)
    'C * SECCCOND: Thermal conductivity                               (W/m.K)

    'Public Function SECCDENS(ByVal CONCENT As Single, ByVal TEMP As Single, ByVal ISECC As Integer)
    'C * PURPOSE:  Calculate the density of secondary coolants
    'C * CONCENT  : solution concentration                                (%)
    'C * TEMP     : Fluid temperature                                     (C)
    'C * ISECC    : selected secondary coolant                            (-)
    'C * SECCDENS: Density                                           (kg/m^3)

    'Public Function SECCSPHT(ByVal CONCENT As Single, ByVal TEMP As Single, ByVal ISECC As Integer)
    'C * PURPOSE:  Calculate the specific heat of secondary coolants
    'C * CONCENT  : solution concentration                                (%)
    'C * TEMP     : Fluid temperature                                     (C)
    'C * ISECC    : selected secondary coolant                            (-)
    'C * SECCSPHT: specific heat                                     (J/kg.K)
#End Region
    Public Function SECCVISC(ByVal CONCENT As Single, ByVal TEMP As Single, ByVal ISECC As Integer)
        'C***********************************************************************
        'C *
        'C * PURPOSE:  Calculate the dynamic viscosity of secondary coolants
        'C *
        'C***********************************************************************
        'C * INPUTS
        'C * ======
        'C * CONCENT  : solution concentration                                (%)
        'C * TEMP     : Fluid temperature                                     (C)
        'C * ISECC    : selected secondary coolant                            (-)
        'C *
        'C * OUTPUT
        'C * ======
        'C * SECCVISC: Dynamic viscosity                        (N/m^2.s or Pa.s)
        'C **********************************************************************

        'C2*** Arguments
        Dim LNVISC As Single
        'C2*** Temporary variables
        Dim T2 As Single, T3 As Single, T4 As Single, T5 As Single, T6 As Single, T7 As Single
        Dim C2 As Single, C3 As Single, C4 As Single, C5 As Single, C6 As Single, C7 As Single
        'C1*** Clamp the concentration within 0-100 bounds
        If (CONCENT > 100.0#) Then
            CONCENT = 100.0#
        ElseIf (CONCENT < 0.0#) Then
            CONCENT = 0.0#
        End If

        'C2*** Calculate temporary variables used for higher terms  in polynomial
        T2 = TEMP * TEMP
        T3 = T2 * TEMP
        T4 = T3 * TEMP
        T5 = T4 * TEMP
        T6 = T5 * TEMP
        T7 = T6 * TEMP
        C2 = CONCENT * CONCENT
        C3 = C2 * CONCENT
        C4 = C3 * CONCENT
        C5 = C4 * CONCENT
        C6 = C5 * CONCENT
        C7 = C6 * CONCENT
        'C1*** Calculate natural log of viscosity (mPa.s). The function is a
        'C1*** combination of an exponential function and 7th order polynomial
        LNVISC = B(ISECC) + C(ISECC) * Math.Exp(D(ISECC) * (Math.Abs(100.0# - CONCENT)) ^ 1.1) * _
         Math.Exp(E(ISECC) * (TEMP + 273.15) ^ 1.1) + A1(ISECC) * TEMP + _
         A2(ISECC) * CONCENT + A3(ISECC) * CONCENT * TEMP + A4(ISECC) * T2 + _
         A5(ISECC) * C2 + A6(ISECC) * T3 + A7(ISECC) * C3 + A8(ISECC) * TEMP * C2 + _
         A9(ISECC) * T2 * CONCENT + A10(ISECC) * T4 + A11(ISECC) * C4 + _
         A12(ISECC) * T3 * CONCENT + A13(ISECC) * TEMP * C3 + A14(ISECC) * T2 * C2 + _
         A15(ISECC) * T5 + A16(ISECC) * T4 * CONCENT + A17(ISECC) * T3 * C2 + _
         A18(ISECC) * C5 + A19(ISECC) * T2 * C3 + A20(ISECC) * TEMP * C4 + _
         A21(ISECC) * T6 + A22(ISECC) * T5 * CONCENT + A23(ISECC) * T4 * C2 + _
         A24(ISECC) * T3 * C3 + A25(ISECC) * T2 * C4 + A26(ISECC) * TEMP * C5 + _
         A27(ISECC) * C6 + A28(ISECC) * T7 + A29(ISECC) * T6 * CONCENT + _
         A30(ISECC) * T5 * C2 + A31(ISECC) * T4 * C3 + A32(ISECC) * T3 * C4 + _
         A33(ISECC) * T2 * C5 + A34(ISECC) * TEMP * C6 + A35(ISECC) * C7
        'C2*** Final value is exponent and divided by 1000 to change from
        'C2*** mPa.s to Pa.s
        SECCVISC = 0.001 * Math.Exp(LNVISC)
    End Function
    Public Function SECCCOND(ByVal CONCENT As Single, ByVal TEMP As Single, ByVal ISECC As Integer)
        'C***********************************************************************
        'C *
        'C * PURPOSE:  Calculate the Thermal conductivity of secondary coolants
        'C *
        'C***********************************************************************
        'C * INPUTS
        'C * ======
        'C * CONCENT  : solution concentration                                (%)
        'C * TEMP     : Fluid temperature                                     (C)
        'C * ISECC    : selected secondary coolant                            (-)
        'C *
        'C * OUTPUT
        'C * ======
        'C * SECCCOND: Thermal conductivity                               (W/m.K)
        'C **********************************************************************
        'C

        'C2*** Arguments

        'C2*** Temporary variables
        Dim T2 As Single, T3 As Single, T4 As Single, T5 As Single ', T6 As Single, T7 As Single
        Dim C2 As Single, C3 As Single, C4 As Single, C5 As Single ', C6 As Single, C7 As Single
        'C2*** Clamp the concentration within 0-100 bounds

        If (CONCENT > 100.0#) Then
            CONCENT = 100.0#
        ElseIf (CONCENT < 0.0#) Then
            CONCENT = 0.0#
        End If
        'C2*** Calculate temporary variables used for higher terms  in polynomial
        T2 = TEMP * TEMP
        T3 = T2 * TEMP
        T4 = T3 * TEMP
        T5 = T4 * TEMP
        C2 = CONCENT * CONCENT
        C3 = C2 * CONCENT
        C4 = C3 * CONCENT
        C5 = C4 * CONCENT
        'C1*** Calculate thermal conductivity in W/m.K. using 5th order polynomial
        SECCCOND = B0(ISECC) + B1(ISECC) * TEMP + _
         B2(ISECC) * CONCENT + B3(ISECC) * CONCENT * TEMP + B4(ISECC) * T2 + _
         B5(ISECC) * C2 + B6(ISECC) * T3 + B7(ISECC) * C3 + B8(ISECC) * TEMP * C2 + _
         B9(ISECC) * T2 * CONCENT + B10(ISECC) * T4 + B11(ISECC) * C4 + _
         B12(ISECC) * T3 * CONCENT + B13(ISECC) * TEMP * C3 + B14(ISECC) * T2 * C2 + _
         B15(ISECC) * T5 + B16(ISECC) * T4 * CONCENT + B17(ISECC) * T3 * C2 + _
         B18(ISECC) * C5 + B19(ISECC) * T2 * C3 + B20(ISECC) * TEMP * C4
    End Function
    Public Function SECCDENS(ByVal CONCENT As Single, ByVal TEMP As Single, ByVal ISECC As Integer)

        'C
        'C***********************************************************************
        'C *
        'C * PURPOSE:  Calculate the density of secondary coolants
        'C *
        'C***********************************************************************
        'C * INPUTS
        'C * ======
        'C * CONCENT  : solution concentration                                (%)
        'C * TEMP     : Fluid temperature                                     (C)
        'C * ISECC    : selected secondary coolant                            (-)
        'C *
        'C * OUTPUT
        'C * ======
        'C * SECCDENS: Density                                           (kg/m^3)
        'C **********************************************************************

        'C2*** Arguments

        'C2*** Temporary variables
        Dim T2 As Single, T3 As Single, T4 As Single, T5 As Single, T6 As Single, T7 As Single
        Dim CN2 As Single, CN3 As Single, CN4 As Single, CN5 As Single, CN6 As Single, CN7 As Single
        'C1*** Clamp the concentration within 0-100 bounds
        If (CONCENT > 100.0#) Then
            CONCENT = 100.0#
        ElseIf (CONCENT < 0.0#) Then
            CONCENT = 0.0#
        End If
        'C2*** Calculate temporary variables used for higher terms  in polynomial
        T2 = TEMP * TEMP
        T3 = T2 * TEMP
        T4 = T3 * TEMP
        T5 = T4 * TEMP
        T6 = T5 * TEMP
        T7 = T6 * TEMP
        CN2 = CONCENT * CONCENT
        CN3 = CN2 * CONCENT
        CN4 = CN3 * CONCENT
        CN5 = CN4 * CONCENT
        CN6 = CN5 * CONCENT
        CN7 = CN6 * CONCENT
        'C1*** Calculate the density in kg/m^3. The function is a
        'C1*** combination of a trignometrical function and 7th order polynomial
        SECCDENS = BD(ISECC) + CD(ISECC) * Math.Cos(DD(ISECC) * TEMP + ED(ISECC) * CONCENT + _
        FD(ISECC)) * Math.Sin(GD(ISECC) * TEMP + HD(ISECC)) + C1(ISECC) * TEMP + _
         C2(ISECC) * CONCENT + C3(ISECC) * CONCENT * TEMP + C4(ISECC) * T2 + _
         C5(ISECC) * CN2 + C6(ISECC) * T3 + C7(ISECC) * CN3 + C8(ISECC) * TEMP * CN2 + _
         C9(ISECC) * T2 * CONCENT + C10(ISECC) * T4 + C11(ISECC) * CN4 + _
         C12(ISECC) * T3 * CONCENT + C13(ISECC) * TEMP * CN3 + C14(ISECC) * T2 * CN2 + _
         C15(ISECC) * T5 + C16(ISECC) * T4 * CONCENT + C17(ISECC) * T3 * CN2 + _
         C18(ISECC) * CN5 + C19(ISECC) * T2 * CN3 + C20(ISECC) * TEMP * CN4 + _
         C21(ISECC) * T6 + C22(ISECC) * T5 * CONCENT + C23(ISECC) * T4 * CN2 + _
         C24(ISECC) * T3 * CN3 + C25(ISECC) * T2 * CN4 + C26(ISECC) * TEMP * CN5 + _
         C27(ISECC) * CN6 + C28(ISECC) * T7 + C29(ISECC) * T6 * CONCENT + _
         C30(ISECC) * T5 * CN2 + C31(ISECC) * T4 * CN3 + C32(ISECC) * T3 * CN4 + _
         C33(ISECC) * T2 * CN5 + C34(ISECC) * TEMP * CN6 + C35(ISECC) * CN7

    End Function
    Public Function SECCSPHT(ByVal CONCENT As Single, ByVal TEMP As Single, ByVal ISECC As Integer)

        'C***********************************************************************
        'C *
        'C * PURPOSE:  Calculate the specific heat of secondary coolants
        'C *
        'C***********************************************************************
        'C * INPUTS
        'C * ======
        'C * CONCENT  : solution concentration                                (%)
        'C * TEMP     : Fluid temperature                                     (C)
        'C * ISECC    : selected secondary coolant                            (-)
        'C *
        'C * OUTPUT
        'C * ======
        'C * SECCSPHT: specific heat                                     (J/kg.K)
        'C **********************************************************************

        'C2*** Arguments
        'Dim LNVISC As Single

        'C2*** Temporary variables
        Dim T2 As Single, T3 As Single, T4 As Single, T5 As Single, T6 As Single, T7 As Single
        Dim C2 As Single, C3 As Single, C4 As Single, C5 As Single, C6 As Single, C7 As Single
        'C1*** Clamp the concentration within 0-100 bounds
        If (CONCENT > 100.0#) Then
            CONCENT = 100.0#
        ElseIf (CONCENT < 0.0#) Then
            CONCENT = 0.0#
        End If
        'C2*** Calculate temporary variables used for higher terms  in polynomial
        T2 = TEMP * TEMP
        T3 = T2 * TEMP
        T4 = T3 * TEMP
        T5 = T4 * TEMP
        T6 = T5 * TEMP
        T7 = T6 * TEMP
        C2 = CONCENT * CONCENT
        C3 = C2 * CONCENT
        C4 = C3 * CONCENT
        C5 = C4 * CONCENT
        C6 = C5 * CONCENT
        C7 = C6 * CONCENT
        'C1*** Calculate the specific heat in KJ/kg.K.The function is a
        'C1*** 7th order polynomial
        SECCSPHT = D0(ISECC) + D1(ISECC) * TEMP + _
         D2(ISECC) * CONCENT + D3(ISECC) * CONCENT * TEMP + D4(ISECC) * T2 + _
         D5(ISECC) * C2 + D6(ISECC) * T3 + D7(ISECC) * C3 + D8(ISECC) * TEMP * C2 + _
         D9(ISECC) * T2 * CONCENT + D10(ISECC) * T4 + D11(ISECC) * C4 + _
         D12(ISECC) * T3 * CONCENT + D13(ISECC) * TEMP * C3 + D14(ISECC) * T2 * C2 + _
         D15(ISECC) * T5 + D16(ISECC) * T4 * CONCENT + D17(ISECC) * T3 * C2 + _
         D18(ISECC) * C5 + D19(ISECC) * T2 * C3 + D20(ISECC) * TEMP * C4 + _
         D21(ISECC) * T6 + D22(ISECC) * T5 * CONCENT + D23(ISECC) * T4 * C2 + _
         D24(ISECC) * T3 * C3 + D25(ISECC) * T2 * C4 + D26(ISECC) * TEMP * C5 + _
         D27(ISECC) * C6 + D28(ISECC) * T7 + D29(ISECC) * T6 * CONCENT + _
         D30(ISECC) * T5 * C2 + D31(ISECC) * T4 * C3 + D32(ISECC) * T3 * C4 + _
         D33(ISECC) * T2 * C5 + D34(ISECC) * TEMP * C6 + D35(ISECC) * C7 _
  'c1*** Change from KJ/kg.K to J/kg.K
        SECCSPHT = SECCSPHT * 1000.0#
    End Function
#End Region

#Region "Water Properties"
#Region "Property routine descriptions"
    'Public Function TSATS(ByVal PKPA)
    '    '  Saturation temp. of steam (C) as a function of pressure (KPA)

    'Public Function PSATS(ByVal TC)
    '    '  Saturation pressure of steam (KPA) as a function of temperature (C)

    'Public Function VSATS(ByVal PKPA, ByVal TC)
    '    '  Sat. specific volume of steam (M3/KG) given sat. T (C) and P (KPA)

    'Public Function VSATW(ByVal TC)
    '    '  Sat. specific volume of water (M3/KG) given sat. T (C)

    'Public Function HSATW(ByVal TC)
    '    '  Sat. enthalpy of liquid water (KJ/KG) given sat. T (C)

    'Public Function hfg(ByVal TC)
    '    '  Latent heat of vaporization of water (KJ/KG) given sat. T (C)

    'Public Function HSATS(ByVal TC)
    '    '  Enthalpy of saturated steam (KJ/KG) given sat. T (C)

    'Public Function SSATW(ByVal TC)
    '    '  Sat. entropy of liquid water [KJ/(KG K)] given sat. T (C)

    'Public Function SSATS(ByVal TC)
    '    '  Entropy of saturated steam [KJ/(KG K)] given sat. T (C)

    'Public Function VS(ByVal PKPA, ByVal TC)
    '    '  Specific volume of superheated steam (M3/KG) given P (KPA) and T (C)

    'Public Function HS(ByVal PKPA, ByVal TC)
    '    '  Enthalpy of superheated steam (KJ/KG) given P (KPA) and T (C)

    'Public Function SS(ByVal PKPA, ByVal TC)
    '    '  Entropy of superheated steam [KJ/(KG K)] given P (KPA) and T (C)

    'Public Function TPSS(ByVal P, ByVal S)
    '    '  Temperature (C) of steam,  given P (KPA) and S [KJ/(KG K)]

    'Public Function CPS(ByVal T)
    '    '  Determine specific heat of steam, Cp, (KJ/Kg/K) given Temp. (C)

    'Public Function CVS(ByVal V, ByVal T)
    '    '  Calculate Cv (KJ/Kg/K) given V (m3/kg) and T (C)

    'Public Function VISSV(ByVal P)
    '    '  Calculates the dynamic viscosity (kg/m-s) of saturated vapor given the pressure (kPa). 

    'Public Function VISSPH(ByVal T)
    '    '  Calculates the dynamic viscosity (kg/m-s) of superheated steam given the temperature (C).  

    'Public Function STEAMK(ByVal T)
    '    '  Calculates thermal conductivity of superheated steam (KW/m-C) given the temperature (C). 

    'Public Function WRHO(ByVal TW)
    '    '  Density of water at 1 atm., WRHO (kg/m3) given temp. (C).

    'Public Function WMU(ByVal TW)
    '    '  Viscosity of water at 1 atm.,  WMU (kg/meter-sec) given temp (C); for centipoise, multiply by 1000.

    'Public Function WK(ByVal TW)
    '    '  Thermal conductivity of water at 1 atm., TW in (C), WK in [kW/(m K)].

    'Public Function WCP(ByVal TW)
    '    '  Specific heat of water at 1 atm., TW in (C), WCP in (J/g-C) or (kJ/kg-C).
#End Region
    Public Function TSATS(ByVal PKPA)

        ' ----------------------------------------------------------------------

        '  Saturation temp. of steam (C) as a function of pressure (KPA)

        '***********************************************************************
        Dim A1 As Double, B1 As Single, C1 As Double, TCONV As Single
        Dim A2 As Double, B2 As Double, C2 As Double, PCONV As Single, P As Single
        A1 = 42.6776
        B1 = -3892.7
        C1 = -9.48654
        TCONV = -273.15
        A2 = -387.592
        B2 = -12587.5
        C2 = -15.2578
        PCONV = 0.001

        P = PKPA * PCONV
        If (P < 12.33) Then
            TSATS = TCONV + A1 + B1 / (Math.Log(P) + C1)
        Else
            TSATS = TCONV + A2 + B2 / (Math.Log(P) + C2)
        End If

        'Return
    End Function
    Public Function PSATS(ByVal TC)

        ' ----------------------------------------------------------------------

        '  Saturation pressure of steam (KPA) as a function of temperature (C)

        '***********************************************************************
        Dim A0 As Single, A1 As Double, A2 As Double, A3 As Single, A4 As Single
        Dim A5 As Single, A6 As Double, A7 As Double, A8 As Double, A9 As Double
        Dim A10 As Double, A11 As Double, TCONV As Single, PCONV As Integer, T As Single
        Dim PLOG As Single

        A0 = 10.4592
        A1 = -0.00404897
        A2 = -0.000041752
        A3 = 0.00000036851
        A4 = -0.0000000010152
        A5 = 0.00000000000086531
        A6 = 0.000000000000000903668
        A7 = -1.9969E-18
        A8 = 7.79287E-22
        A9 = 1.91482E-25
        A10 = -3968.06
        A11 = 39.5735
        TCONV = 273.15
        PCONV = 1000

        T = TC + TCONV
        PLOG = A0 + T * A1 + (T ^ 2) * A2 + (T ^ 3) * A3 + (T ^ 4) * A4 + (T ^ 5) * A5 + (T ^ 6) * A6 + (T ^ 7) * A7 + (T ^ 8) * A8 + (T ^ 9) * A9 + A10 / (T - A11)
        PSATS = PCONV * Math.Exp(PLOG)

        'Return
    End Function
    Public Function VSATS(ByVal PKPA, ByVal TC)

        ' ----------------------------------------------------------------------

        '  Sat. specific volume of steam (M3/KG) given sat. T (C) and P (KPA)

        '***********************************************************************
        Dim A As Integer, B As Double, C As Double, D As Double, E1 As Double
        Dim E2 As Double, E3 As Double, E4 As Double, E5 As Double, Y As Double
        Dim TCR As Single, PCR As Single, VCR As Single, TCNV As Single, PCNV As Single
        Dim TR As Double
        A = 1
        B = 1.6351057
        C = 52.584599
        D = -44.694653
        E1 = -8.9751114
        E2 = -0.4384553
        E3 = -19.179576
        E4 = 36.765319
        E5 = -19.462437
        TCR = 647.3
        PCR = 22.089
        VCR = 0.003155
        TCNV = 273.15
        PCNV = 0.001

        TR = (TCR - TC - TCNV) / TCR
        Y = A + B * TR ^ (1 / 3) + C * TR ^ (5 / 6) + D * TR ^ 0.875
        Y = Y + TR * E1 + (TR ^ 2) * E2 + (TR ^ 3) * E3 + (TR ^ 4) * E4 + (TR ^ 5) * E5
        VSATS = Y * PCR * VCR / (PKPA * PCNV)

        'Return
    End Function
    Public Function VSATW(ByVal TC)

        ' ----------------------------------------------------------------------

        '  Sat. specific volume of water (M3/KG) given sat. T (C)

        '***********************************************************************
        Dim A As Integer, B As Double, C As Double, D As Double, E1 As Double
        Dim E2 As Double, E3 As Double, E4 As Double, E5 As Double
        Dim TCR As Single, VCR As Single, TCNV As Single, TR As Single, Y As Double

        A = 1
        B = -1.9153882
        C = 12.015186
        D = -7.8464025
        E1 = -3.8886414
        E2 = 2.0582238
        E3 = -2.0829991
        E4 = 0.82180004
        E5 = 0.47549742
        TCR = 647.3
        VCR = 0.003155
        TCNV = 273.15

        TR = (TCR - TC - TCNV) / TCR
        Y = A + B * TR ^ (1 / 3) + C * TR ^ (5 / 6) + D * TR ^ 0.875
        Y = Y + TR * E1 + (TR ^ 2) * E2 + (TR ^ 3) * E3 + (TR ^ 4) * E4 + (TR ^ 5) * E5
        VSATW = Y * VCR

        'Return
    End Function
    Public Function HSATW(ByVal TC)

        ' ----------------------------------------------------------------------

        '  Sat. enthalpy of liquid water (KJ/KG) given sat. T (C)

        '***********************************************************************
        Dim E11 As Double, E21 As Double, E31 As Double, HFCR As Single, E41 As Double
        Dim E51 As Double, E61 As Double, TCNV As Single, E71 As Double, A2 As Double
        Dim E12 As Double, E22 As Double, E32 As Double, E42 As Double, E52 As Double
        Dim E62 As Double, E72 As Double, A3 As Single, B3 As Double, C3 As Double
        Dim D3 As Double, E13 As Double, E23 As Double, TCR As Single
        Dim TK As Double, TR As Double, Y As Double

        E11 = 624.698837
        E21 = -2343.85369
        E31 = -9508.12101
        HFCR = 2099.3
        E41 = 71628.7928
        E51 = -163535.221
        E61 = 166531.093
        TCNV = 273.15
        E71 = -64785.4585
        A2 = 0.8839230108
        E12 = -2.67172935
        E22 = 6.22640035
        E32 = -13.1789573
        E42 = -1.91322436
        E52 = 68.7937653
        E62 = -124.819906
        E72 = 72.1435404
        A3 = 1.0#
        B3 = -0.441057805
        C3 = -5.52255517
        D3 = 6.43994847
        E13 = -1.64578795
        E23 = -1.30574143
        TCR = 647.3

        TK = TC + TCNV
        TR = (TCR - TK) / TCR
        If (TK < 300) Then
            Y = TR * E11 + (TR ^ 2) * E21 + (TR ^ 3) * E31 + (TR ^ 4) * E41 + (TR ^ 5) * E51 + (TR ^ 6) * E61 + (TR ^ 7) * E71
        Else
            If (TK < 600) Then
                Y = TR * E12 + (TR ^ 2) * E22 + (TR ^ 3) * E32 + (TR ^ 4) * E42 + (TR ^ 5) * E52 + (TR ^ 6) * E62 + (TR ^ 7) * E72
                Y = Y + A2
            Else
                Y = A3 + B3 * TR ^ (1 / 3) + C3 * TR ^ (5 / 6) + D3 * TR ^ 0.875 + TR * E13 + (TR ^ 2) * E23
            End If
        End If

        HSATW = Y * HFCR

        'Return
    End Function
    Public Function hfg(ByVal TC)

        ' ----------------------------------------------------------------------

        '  Latent heat of vaporization of water (KJ/KG) given sat. T (C)

        '***********************************************************************
        Dim E1 As Double, E2 As Single, E3 As Double, E4 As Single, E5 As Double
        Dim B As Single, C As Single, D As Double, HFGTP As Single, TCR As Single
        Dim TCNV As Single, TR As Double, Y As Double

        E1 = -3.87446
        E2 = 2.94553
        E3 = -8.06395
        E4 = 11.5633
        E5 = -6.02884
        B = 0.779221
        C = 4.62668
        D = -1.07931
        HFGTP = 2500.9
        TCR = 647.3
        TCNV = 273.15

        If (TC < 0) Then TC = 0
        TR = (TCR - TC - TCNV) / TCR
        If (TR < 0) Then
            hfg = 0
            Return hfg
        End If
        Y = B * TR ^ (1 / 3) + C * TR ^ (5 / 6) + D * TR ^ 0.875
        Y = Y + TR * E1 + (TR ^ 2) * E2 + (TR ^ 3) * E3 + (TR ^ 4) * E4 + (TR ^ 5) * E5
        hfg = Y * HFGTP

        'Return
    End Function
    Public Function HSATS(ByVal TC)

        ' ----------------------------------------------------------------------

        '  Enthalpy of saturated steam (KJ/KG) given sat. T (C)

        '***********************************************************************
        Dim E1 As Double, E2 As Double, E3 As Double, E4 As Double
        Dim E5 As Double, B As Double, C As Double, D As Double
        Dim A As Integer, TCR As Single, HCR As Single, TCNV As Single
        Dim TR As Double, Y As Double

        E1 = -4.81351884
        E2 = 2.69411792
        E3 = -7.39064542
        E4 = 10.4961689
        E5 = -5.46840036
        B = 0.457874342
        C = 5.08441288
        D = -1.48513244
        A = 1
        TCR = 647.3
        HCR = 2099.3
        TCNV = 273.15

        TR = (TCR - TC - TCNV) / TCR
        Y = A + B * TR ^ (1 / 3) + C * TR ^ (5 / 6) + D * TR ^ 0.875
        Y = Y + TR * E1 + (TR ^ 2) * E2 + (TR ^ 3) * E3 + (TR ^ 4) * E4 + (TR ^ 5) * E5
        HSATS = Y * HCR

        'Return
    End Function
    Public Function SSATW(ByVal TC)

        '---------------------------------------------------------------------

        '  Sat. entropy of liquid water [KJ/(KG K)] given sat. T (C)

        '***********************************************************************
        Dim E11 As Double, E21 As Double, E31 As Double, SCR As Single, E41 As Double
        Dim E51 As Double, E61 As Double, TCNV As Single, E71 As Double, A2 As Double
        Dim E12 As Double, TCR As Single, E22 As Double, E32 As Single, E42 As Double
        Dim E52 As Double, E62 As Double, E72 As Double, A3 As Single, B3 As Double
        Dim C3 As Double, D3 As Double, E13 As Double, E23 As Double
        Dim TK As Double, TR As Double, Y As Double

        E11 = -1836.92956
        E21 = 14706.6352
        E31 = -43146.6046
        SCR = 4.4289
        E41 = 48606.6733
        E51 = 7997.5096
        E61 = -58333.9887
        TCNV = 273.15
        E71 = 33140.0718
        A2 = 0.912762917
        E12 = -1.75702956
        TCR = 647.3
        E22 = 1.68754095
        E32 = 5.82215341
        E42 = -63.3354786
        E52 = 188.076546
        E62 = -252.344531
        E72 = 128.058531
        A3 = 1
        B3 = -0.32481765
        C3 = -2.990556709
        D3 = 3.23419
        E13 = -0.678067859
        E23 = -1.91910364

        TK = TC + TCNV
        TR = (TCR - TK) / TCR
        If (TK < 300) Then
            Y = TR * E11 + (TR ^ 2) * E21 + (TR ^ 3) * E31 + (TR ^ 4) * E41 + (TR ^ 5) * E51 + (TR ^ 6) * E61 + (TR ^ 7) * E71
        Else
            If (TK < 600) Then
                Y = TR * E12 + (TR ^ 2) * E22 + (TR ^ 3) * E32 + (TR ^ 4) * E42 + (TR ^ 5) * E52 + (TR ^ 6) * E62 + (TR ^ 7) * E72
                Y = Y + A2
            Else
                Y = A3 + B3 * TR ^ (1 / 3) + C3 * TR ^ (5 / 6) + D3 * TR ^ 0.875 + TR * E13 + (TR ^ 2) * E23
            End If
        End If
        SSATW = Y * SCR

        'Return
    End Function
    Public Function SSATS(ByVal TC)

        ' ----------------------------------------------------------------------

        '  Entropy of saturated steam [KJ/(KG K)] given sat. T (C)

        '***********************************************************************
        Dim E1 As Double, E2 As Double, E3 As Double, E4 As Double, E5 As Double
        Dim A As Single, B As Single, C As Double, D As Double, TCR As Single, SCR As Single
        Dim TCNV As Single, TR As Double, Y As Double

        E1 = -4.34839
        E2 = 1.34672
        E3 = 1.75261
        E4 = -6.22295
        E5 = 9.99004
        A = 1
        B = 0.377391
        C = -2.78368
        D = 6.93135
        TCR = 647.3
        SCR = 4.4289
        TCNV = 273.15

        TR = (TCR - TC - TCNV) / TCR
        Y = A + B * TR ^ (1 / 3) + C * TR ^ (5 / 6) + D * TR ^ 0.875
        Y = Y + TR * E1 + (TR ^ 2) * E2 + (TR ^ 3) * E3 + (TR ^ 4) * E4 + (TR ^ 5) * E5
        SSATS = Y * SCR

        'Return
    End Function
    Public Function VS(ByVal PKPA, ByVal TC)

        ' ----------------------------------------------------------------------

        '  Specific volume of superheated steam (M3/KG) given P (KPA) and T (C)

        '***********************************************************************
        Dim R As Single, B1 As Single, B2 As Single, B3 As Single, EM As Single
        Dim A0 As Double, A1 As Double, A2 As Double, TCNV As Single, A3 As Single
        Dim C1 As Single, C2 As Single, C3 As Single, PCNV As Single, C4 As Single
        Dim C5 As Single, C6 As Single, P As Double, T As Double, TS As Double

        R = 0.000461631
        B1 = 0.0527993
        B2 = 0.00375928
        B3 = 0.022
        EM = 40
        A0 = -3.741378
        A1 = -0.0047838281
        A2 = 0.000015923434
        TCNV = 273.15
        A3 = 10
        C1 = 42.6776
        C2 = -3892.7
        C3 = -9.48654
        PCNV = 0.001
        C4 = -387.592
        C5 = -12587.5
        C6 = -15.2578

        P = PKPA * PCNV
        T = TC + TCNV
        TS = C1 + C2 / (Math.Log(P) + C3)
        If (P >= 12.33) Then TS = C4 + C5 / (Math.Log(P) + C6)

        VS = R * T / P - B1 * Math.Exp(-B2 * T) + (B3 - Math.Exp(A0 + TS * A1 + (TS ^ 2) * A2)) / (A3 * P) * Math.Exp((TS - T) / EM)

        'Return
    End Function
    Public Function HS(ByVal PKPA, ByVal TC)

        ' ----------------------------------------------------------------------

        '  Enthalpy of superheated steam (KJ/KG) given P (KPA) and T (C)

        '***********************************************************************
        Dim B11 As Single, B12 As Double, B13 As Single, B21 As Single, B22 As Double
        Dim B23 As Double, B31 As Double, B32 As Double, B33 As Double, B41 As Single
        Dim B42 As Double, B43 As Double, B44 As Double, B45 As Double, EM As Single
        Dim C1 As Single, C2 As Single, C3 As Single, PCNV As Single, C4 As Single
        Dim C5 As Single, C6 As Single, TCNV As Single, P As Double, T As Double, TS As Double

        B11 = 2041.21
        B12 = -40.4002
        B13 = -0.48095
        B21 = 1.610693
        B22 = 0.05472051
        B23 = 0.0007517537
        B31 = 0.0003383117
        B32 = -0.00001975736
        B33 = -0.000000287409
        B41 = 1707.82
        B42 = -16.99419
        B43 = 0.062746295
        B44 = -0.00010284259
        B45 = 0.000000064561298
        EM = 45
        C1 = 42.6776
        C2 = -3892.7
        C3 = -9.48654
        PCNV = 0.001
        C4 = -387.592
        C5 = -12587.5
        C6 = -15.2578
        TCNV = 273.15

        P = PKPA * PCNV
        T = TC + TCNV
        TS = C1 + C2 / (Math.Log(P) + C3)
        If (P >= 12.33) Then TS = C4 + C5 / (Math.Log(P) + C6)

        Dim A0 As Double, A1 As Double, A2 As Double, A3 As Double

        A0 = B11 + P * B12 + (P ^ 2) * B13
        A1 = B21 + P * B22 + (P ^ 2) * B23
        A2 = B31 + P * B32 + (P ^ 2) * B33
        A3 = B41 + TS * B42 + (TS ^ 2) * B43 + (TS ^ 3) * B44 + (TS ^ 4) * B45

        HS = A0 + T * A1 + (T ^ 2) * A2 - A3 * Math.Exp((TS - T) / EM)

        'Return
    End Function
    Public Function SS(ByVal PKPA, ByVal TC)

        ' ----------------------------------------------------------------------

        '  Entropy of superheated steam [KJ/(KG K)] given P (KPA) and T (C)

        '***********************************************************************
        Dim A0 As Double, A1 As Double, A2 As Double, A3 As Double, A4 As Double
        Dim B1 As Double, B2 As Single, B3 As Single, C0 As Double, C1 As Double
        Dim C2 As Double, C3 As Double, EM As Single, C4 As Double, E1 As Single
        Dim E2 As Single, E3 As Single, E4 As Single, E5 As Single, E6 As Double, E7 As Single
        Dim TCNV As Single, P As Double, T As Double, TS As Double

        A0 = 4.6162961
        A1 = 0.01039008
        A2 = -0.000009873085
        A3 = 0.00000000543411
        A4 = -0.000000000001170465
        B1 = -0.4650306
        B2 = 0.001
        B3 = 10
        C0 = 1.777804
        C1 = -0.01802468
        C2 = 0.00006854459
        C3 = -0.0000001184434
        EM = 85
        C4 = 0.00000000008142201
        E1 = 42.6776
        E2 = -3892.7
        E3 = -9.48654
        E4 = -387.592
        E5 = -12587.5
        E6 = -15.2578
        E7 = 273.15

        P = PKPA * B2
        T = TC + TCNV
        TS = E1 + E2 / (Math.Log(P) + E3)
        If (P >= 12.33) Then TS = E4 + E5 / (Math.Log(P) + E6)

        SS = A0 + T * A1 + (T ^ 2) * A2 + (T ^ 3) * A3 + (T ^ 4) * A4 + B1 * Math.Log(B2 + P * B3) - Math.Exp((TS - T) / EM) * (C0 + TS * C1 + (TS ^ 2) * C2 + (TS ^ 3) * C3 + (TS ^ 4) * C4)

        'Return
    End Function
    Public Function TPSS(ByVal P, ByVal S)

        ' ----------------------------------------------------------------------

        '  Temperature (C) of steam,  given P (KPA) and S [KJ/(KG K)]

        '***********************************************************************
        Dim E1 As Single, E2 As Single, E3 As Single, PCNV As Single, i As Integer
        Dim E4 As Single, E5 As Single, E6 As Single, TABS As Single, T0 As Single
        Dim SO As Double, TA As Double, SA As Double, T As Double, j As Integer

        E1 = 42.6776
        E2 = -3892.7
        E3 = -9.48654
        PCNV = 0.001
        E4 = -387.592
        E5 = -12587.5
        E6 = -15.2578
        TABS = 273.15

        '  compare input entropy with saturation value

        T0 = E1 - TABS + E2 / (Math.Log(P * PCNV) + E3)
        If (P >= 12330) Then T0 = E4 - TABS + E5 / (Math.Log(P * PCNV) + E6)
        SO = SSATS(T0)
        If (SO >= S) Then
            TPSS = T0

        Else

            '  Initial guess TA is based on assumption of constant specific heat.
            '  Subsequent approximations made by interpolation.

            TA = (T0 + TABS) * (1 + (S - SO) / CPS(T0)) - TABS
            SA = SS(P, TA)
            i = 0
            j = 0
            'T = TA + (T0 - TA) * (S - SA) / (SO - SA)
            Do 'Until (Abs(T - TA) < 0.05) Or j = 0

                'For i = 1 To 10
                T = TA + (T0 - TA) * (S - SA) / (SO - SA)
                'If (Abs(T - TA) < 0.05) Then
                ' i = 10
                'End If
                T0 = TA
                SO = SA
                TA = T
                SA = SS(P, TA)
                i = 1 + i
                'T = TA + (T0 - TA) * (S - SA) / (SO - SA)
                'Next i
                If i >= 10 Then
                    PublicData.ShowSevereError("WARNING! Function TPSS Fails to converge!")
                    j = 1
                End If
            Loop Until ((T - TA) < 0.1) And ((T - TA) > -0.1) Or j = 1
            TPSS = T

        End If
    End Function
    Public Function CPS(ByVal T)

        ' ----------------------------------------------------------------------

        '       Determine specific heat of steam, Cp, (KJ/Kg/K) given Temp. (C)

        '       Specific heat equation from "Fundamentals of Classical
        '       Thermodynamics-SI Version" by Van Wylen and Sonntag
        '       Table A.9, pg. 683.

        '       Valid for T between 300-3500 K   max error = .43%

        '***********************************************************************
        Dim C1 As Single, C2 As Single, C3 As Single, C4 As Single, E1 As Single, E2 As Single
        Dim TK As Double, T1 As Double
        C1 = 143.05
        C2 = -183.54
        C3 = 82.751
        C4 = -3.6989
        E1 = 0.25
        E2 = 0.5

        TK = T + 273.15
        T1 = TK / 100
        CPS = (C1 + C2 * T1 ^ E1 + C3 * T1 ^ E2 + C4 * T1) / 18.015
        If (TK < 300 Or TK > 3500) Then
            PublicData.ShowSevereError("WARNING! Function CPS: T out of range!")
        End If

        'Return
    End Function
    Public Function CVS(ByVal V, ByVal T)

        ' ----------------------------------------------------------------------

        '       Calculate Cv (KJ/Kg/K) given V (m3/kg) and T (C)

        '***********************************************************************

        Dim A(7) As Double, TC As Single, TFR As Single, B1 As Double, VE As Double, TR As Double

        TC = 1165.11
        TFR = 459.67
        B1 = 0.0063101
        A(1) = 0.99204818
        A(2) = -33.137211
        A(3) = 416.29663
        A(4) = 0.185053
        A(5) = 5.475
        A(6) = -2590.5815
        A(7) = 113.95968

        TR = 1.8 * T + 32 + TFR
        VE = (V - B1) / 0.062428
        CVS = A(1) + A(2) / TR ^ (0.5) + A(3) / TR - A(4) * A(5) ^ 2 * TR / TC ^ 2 * Math.Exp(-A(5) * TR / TC) * (A(6) / VE + A(7) / VE ^ 2)
        CVS = CVS * 4.1868

        'Return
    End Function
    Public Function VISSV(ByVal P)

        ' ----------------------------------------------------------------------
        '
        '  Calculates the dynamic viscosity (kg/m-s) of saturated
        '  vapor given the pressure (kPa).  Correlation obtained from
        '  a curve fit of data from 'Heat Transfer' by Alan J. Chapman, 1974.

        '***********************************************************************
        Dim C1 As Single, C2 As Single, C3 As Double, C4 As Double
        Dim psi As Double
        C1 = 0.0314
        C2 = 0.000029675
        C3 = -0.0000000160583
        C4 = 0.000000000003768986

        '       Covert pressure from kPa to psi

        psi = P / 6.894757
        VISSV = C1 + C2 * psi + C3 * psi ^ 2 + C4 * psi ^ 3

        '       Convert viscosity from lbm/ft-hr to kg/m-s

        VISSV = VISSV * 0.00041338

        'Return
    End Function
    Public Function VISSPH(ByVal T)

        ' ----------------------------------------------------------------------

        '   Calculates the dynamic viscosity (kg/m-s) of superheated
        '   steam given the temperature (C).  The correlation is obtained
        '   from a curve fit at atmospheric pressure from 'Heat Transfer'
        '   by Alan J. Chapman, 1974. (Note: there is little  variation in
        '   viscosity at higher pressures.)

        '***********************************************************************
        Dim C1 As Double, C2 As Double, C3 As Double, C4 As Double
        Dim TF As Double
        C1 = 0.0183161
        C2 = 0.000057067
        C3 = -0.0000000142253
        C4 = 0.000000000007241555

        '     Convert temperature from C to F

        TF = T * 1.8 + 32
        VISSPH = C1 + C2 * TF + C3 * TF ^ 2 + C4 * TF ^ 3

        '     Convert viscosity from lbm/ft-hr to kg/m-s

        VISSPH = VISSPH * 0.00041338

        'Return
    End Function
    Public Function STEAMK(ByVal T)

        ' ----------------------------------------------------------------------

        '     Calculates thermal conductivity of superheated steam (KW/m-C)
        '     given the temperature (C).  Curve fit from data in 'Heat Transfer'
        '     by Alan J. Chapman, 1974.

        '***********************************************************************
        Dim C1 As Single, C2 As Double, C3 As Double, TF As Double

        C1 = 0.824272
        C2 = 0.00254627
        C3 = 0.00000009848539

        '       Convert temperature from C to F

        TF = T * 1.8 + 32
        STEAMK = (C1 + C2 * TF + C3 * TF ^ 2) * 0.01

        '       Convert K from Btu/hr-ft-F to kW/m-C

        STEAMK = STEAMK * 0.0017308

        'Return
    End Function
    Public Function WRHO(ByVal TW)

        ' ----------------------------------------------------------------------

        '  Density eq. for water at 1 atm., from CRC Handbook of Chem. & Phys.,
        '   61st Edition (1980-1981), p. F-6.  Density (kg/m3) given temp. (C).

        '***********************************************************************
        Dim AR0 As Double, Ar1 As Double, AR2 As Double, AR6 As Double
        Dim AR3 As Double, Ar4 As Double, AR5 As Double

        AR0 = 999.83952
        Ar1 = 16.945176
        AR2 = -0.0079870401
        AR6 = 0.01687985
        AR3 = -0.000046170461
        Ar4 = 0.00000010556302
        AR5 = -0.00000000028054253

        WRHO = (AR0 + TW * Ar1 + (TW ^ 2) * AR2 + (TW ^ 3) * AR3 + (TW ^ 4) * Ar4 + (TW ^ 5) * AR5) / (1 + AR6 * TW)

        'Return
    End Function
    Public Function WMU(ByVal TW)

        ' ----------------------------------------------------------------------

        '  Viscosity equations for water at 1 atm., from CRC Handbook (op.cit.),
        '   page F-51.  WMU in kg/meter-sec; for centipoise, multiply by 1000.
        '    For temps > 100 C, fit to data from Karlekar & Desmond (saturated).

        '***********************************************************************
        Dim AM0 As Single, AM1 As Single, AM2 As Single, AM3 As Single, AM4 As Single
        Dim AM5 As Single, AM6 As Single, AM7 As Single, AM8 As Single, A10 As Single
        Dim A11 As Single, A12 As Single, A13 As Single
        AM0 = -3.30233
        AM1 = 1301
        AM2 = 998.333
        AM3 = 8.1855
        AM4 = 0.00585
        AM5 = 1.002
        AM6 = -1.3272
        AM7 = -0.001053
        AM8 = 105
        A10 = 0.68714
        A11 = -0.0059231
        A12 = 0.000021249
        A13 = -0.0000000269575

        WMU = AM5 * 10 ^ ((TW - 20) * (AM6 + (TW - 20) * AM7) / (TW + AM8))
        If (TW < 20) Then
            WMU = 10 ^ (AM0 + AM1 / (AM2 + (TW - 20) * (AM3 + AM4 * (TW - 20)))) * 100
        End If
        If (TW > 100) Then WMU = A10 + TW * A11 + (TW ^ 2) * A12 + (TW ^ 3) * A13
        WMU = 0.001 * WMU

        'Return
    End Function
    Public Function WK(ByVal TW)

        ' ----------------------------------------------------------------------

        '  Thermal conductivity equation from linear least-squares fit to data
        '   in CRC Handbook (op.cit.), page E-11; temps. from 270 K to 620 K.
        '    Temperature in Celsius, WK in [kW/(m K)].  Values at one atmosphere
        '    for T from 0 to 100 C, at saturation for T above 100.

        '***********************************************************************
        Dim AK0 As Single, AK1 As Double, AK2 As Double, AK3 As Double, AK4 As Double

        AK0 = 0.560101
        AK1 = 0.00211703
        AK2 = -0.0000105172
        AK3 = 0.00000001497323
        AK4 = -0.0000000000148553

        WK = 0.001 * (AK0 + TW * AK1 + (TW ^ 2) * AK2 + (TW ^ 3) * AK3 + (TW ^ 4) * AK4)

        'Return
    End Function
    Public Function WCP(ByVal TW)

        ' ----------------------------------------------------------------------

        '  Specific heat of water at 1 atmosphere, 0 to 100 C.  Equation from
        '    linear least-squares regression of data from CRC Handbook (op.cit.)
        '    page D-174; in J/g-C (or kJ/kg-C).
        '    For temps > 100, fit to data from Karlekar & Desmond (saturated).

        '***********************************************************************
        Dim ACP0 As Single, ACP1 As Double, ACP2 As Double, ACP3 As Single, ACP4 As Double
        Dim ACP5 As Single, ACP6 As Single, ACP7 As Single, ACP8 As Double

        ACP0 = 4.21534
        ACP1 = -0.00287819
        ACP2 = 0.000074729
        ACP3 = -0.000000779624
        ACP4 = 0.000000003220424
        ACP5 = 2.9735
        ACP6 = 0.023049
        ACP7 = -0.00013953
        ACP8 = 0.0000003092474

        WCP = ACP0 + TW * ACP1 + (TW ^ 2) * ACP2 + (TW ^ 3) * ACP3 + (TW ^ 4) * ACP4
        If (TW > 100) Then WCP = ACP5 + TW * ACP6 + (TW ^ 2) * ACP7 + (TW ^ 3) * ACP8

    End Function
#End Region

End Module


Friend Module PsychProperties

    Friend Function Atmpress(ByVal elevinft As Double) As Double
        Atmpress = 14.696 * (1 - 0.0000068753 * elevinft) ^ 5.2559
    End Function

    Friend Function HumRat(ByVal db As Double, ByVal wb As Double, ByVal elevinft As Double) As Double
        Dim p As Double, pws As Double, wsat As Double
        p = Atmpress(elevinft)
        pws = pwsat(wb)
        wsat = (pws * 0.62198) / (p - pws)
        HumRat = ((1093 - 0.556 * wb) * wsat - 0.24 * (db - wb)) / (1093 + 0.444 * db - wb)
    End Function

    Friend Function RelHum(ByVal db As Double, ByVal wb As Double, ByVal elevinft As Double) As Double
        Dim HumRatRH As Double, press As Double, pw As Double, pws As Double
        HumRatRH = HumRat(db, wb, elevinft)
        press = Atmpress(elevinft)
        pw = press / (1 + (0.62198 / HumRatRH))
        pws = pwsat(db)
        RelHum = pw / pws
    End Function

    Friend Function pwsat(ByVal db As Double) As Double
        Dim RT As Double, c8 As Double, c9 As Double, c10 As Double, c11 As Double, c12 As Double, c13 As Double
        RT = db + 459.67
        c8 = -10440.397 / RT
        c9 = -11.29465
        c10 = -0.027022355 * RT
        c11 = 0.00001289036 * RT ^ 2
        c12 = -0.000000002478068 * RT ^ 3
        c13 = 6.5459673 * Math.Log(RT)
        pwsat = Math.Exp(c8 + c9 + c10 + c11 + c12 + c13)
    End Function

	''' <summary>
	''' Calculate the air enthalpy given dry bulb and wet bulb temperature and pressure at atmospheric pressure
	''' </summary>
	''' <param name="TDB">dry bulb temperature (C)</param>
	''' <param name="Twb">wet bulb temperature (C)</param>
	''' <returns>enthapy(KJ/kg)</returns>
	''' <remarks></remarks>
	Public Function H(ByVal TDB As Single, ByVal Twb As Single) As Single

		'variables
		Dim ALNPSTAR As Double
		Dim PSTAR As Double	'PSTAR=water vapor saturation pressure at wet bulb temperature (Pa)
		Dim WS As Double 'WS=saturation humidity ratio at wet bulb temperature
		Dim W As Double	'W=humidity ratio at dry bulb temperature

		'parameters
		Const Patm As Single = 101325 'atmospheric pressure (Pa)

		'calculations
		ALNPSTAR = -5800.2206 / (Twb + 273.15) + 1.3914993 - 0.048640239 * (Twb + 273.15) + 0.000041764768 * (Twb + 273.15) ^ 2 - 0.000000014452093 * (Twb + 273.15) ^ 3 + 6.5459673 * Math.Log(Twb + 273.15)
		PSTAR = Math.Exp(ALNPSTAR)
		WS = 0.62198 * PSTAR / (Patm - PSTAR)
		W = ((2501 - 2.381 * Twb) * WS - 1.006 * (TDB - Twb)) / (2501 + 1.805 * TDB - 4.186 * Twb)
		H = 1.006 * TDB + W * (2501 + 1.805 * TDB)

	End Function

	''' <summary>
	''' Calculate saturated enthalpy(KJ/Kg)
	''' </summary>
	''' <param name="T">Temperature</param>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Function HSAT(ByVal T As Single) As Single

		Dim ALNP As Double, P As Double, omega As Double
		ALNP = -10440.4 / (T * 9 / 5 + 32 + 459.67) - 11.2946669 - 0.02700133 * (T * 9 / 5 + 32 + 459.67) + 0.00001289706 * (T * 9 / 5 + 32 + 459.67) ^ 2 - 0.000000002478068 * (T * 9 / 5 + 32 + 459.67) ^ 3 + 6.5459673 * Math.Log(T * 9 / 5 + 32 + 459.67)
		'PRESSURE IN IP UNIT (lb/in^2)
		P = Math.Exp(ALNP)
		'CONVERT PRESSURE TO SI UNIT (KPa)
		P = P * 6.895
		'HUMIDITY RATIO
		omega = 0.62198 * P / (101.325 - P)
		'SATURATED ENTHALPY IN SI UNIT (KJ/KG)
		HSAT = 1.006 * T + omega * (2501 + 1.805 * T)

	End Function

End Module