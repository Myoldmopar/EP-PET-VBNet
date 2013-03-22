Module RefrigerantProperties

#Region "Refrigerant Data Arrays"
    Private INDENT(12) As Integer
    Private AL(12) As Double, BL(12) As Double, CL(12) As Double, DL(12) As Double, EL(12) As Double, FL(12) As Double, GL(12) As Double, TCR(12) As Double, ATF(12) As Double
    Private A(12) As Double, B(12) As Double, C(12) As Double, D(12) As Double, E(12) As Double, F(12) As Double, ATFP(12) As Double
    Private X(12) As Double, Y(12) As Double
    Private AA(12) As Double, BB(12) As Double, CC(12) As Double, DD(12) As Double, FF(12) As Double, ATFPPP(12) As Double
    Private R(12) As Double, BV(12) As Double, A2(12) As Double, B2(12) As Double, C2(12) As Double, A3(12) As Double, B3(12) As Double, C3(12) As Double, A4(12) As Double, B4(12) As Double, C4(12) As Double, A5(12) As Double, B5(12) As Double, C5(12) As Double, A6(12) As Double, B6(12) As Double, C6(12) As Double, AK(12) As Double, AV(12) As Double, CP(12) As Double, TCRP(12) As Double, ATFPP(12) As Double
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

    Sub InitREFRIGPR()
        Call InitArrayOfIntegers(INDENT, 11, 12, 13, 14, 21, 22, 23, 113, 114, 500, 502, 318)
        Call InitArrayOfDoubles(AL, 34.57, 34.84, 36.06996, 39.06, 116.37962, 32.76, 32.7758, 122.872, 36.32, 31.0#, 35.0#, 38.7)
        Call InitArrayOfDoubles(BL, 57.63811, 53.341187, 54.395124, 69.568489, -0.03106808, 54.634409, 63.37784, -0.0128, 61.146414, 43.562, 53.48437, 70.858318)
        Call InitArrayOfDoubles(CL, 43.6322, 0.0#, 0.0#, 4.5866114, -0.0000501, 36.74892, -25.30533, 0.0000636, 0.0#, 74.709, 63.86417, 23.609759)
        Call InitArrayOfDoubles(DL, -42.82356, 18.69137, 8.512776, 36.1716662, 0.0#, -22.2925657, 144.16182, 0.0#, 16.418015, -87.583, -70.08066, 15.989182)
        Call InitArrayOfDoubles(EL, 36.70663, 0.0#, 0.0#, -8.058986, 0.0#, 20.4732886, -106.1328, 0.0#, 0.0#, 56.483, 48.47901, -8.9243856)
        Call InitArrayOfDoubles(FL, 0.0#, 21.98396, 25.879906, 0.0#, 0.0#, 0.0#, 0.0#, 0.0#, 17.476838, 0.0#, 0.0#, 0.0#)
        Call InitArrayOfDoubles(GL, 0.0#, -3.150994, 9.589006, 0.0#, 0.0#, 0.0#, 0.0#, 0.0#, 1.119828, 0.0#, 0.0#, 0.0#)
        Call InitArrayOfDoubles(TCR, 848.07, 693.3, 543.6, 409.5, 812.9, 664.5, 538.33, 877.0#, 753.95, 681.59, 639.56, 699.27)
        Call InitArrayOfDoubles(ATF, 459.67, 459.7, 459.69, 459.69, 459.6, 459.69, 459.69, 459.6, 459.69, 459.69, 459.67, 459.69)

        Call InitArrayOfDoubles(A, 42.14702865, 39.88381727, 25.967975, 20.71545389, 42.7908, 29.35754453, 328.90853, 33.0655, 27.071306, 17.780935, 10.644955, 15.63242)
        Call InitArrayOfDoubles(B, -4344.343807, -3436.632228, -2709.538217, -2467.505285, -4261.34, -3845.193152, -7952.76913, -4330.98, -5113.7021, -3422.69717, -3671.153813, -4301.063)
        Call InitArrayOfDoubles(C, -12.84596753, -12.47152228, -7.17234391, -4.69017025, -13.0295, -7.86103122, -144.5142304, -9.2635, -6.3086761, -3.63691, -0.369835, -2.128401)
        Call InitArrayOfDoubles(D, 0.0040083725, 0.00473044244, 0.002545154, 0.00064798076, 0.0039851, 0.002190939, 0.24211502, 0.0020539, 0.0006913003, 0.00050272207, -0.001746352, -0.00119759)
        Call InitArrayOfDoubles(E, 0.0313605356, 0.0#, 0.280301091, 0.770707795, 0.0#, 0.445746703, -0.00021280665, 0.0#, 0.78142111, 0.4629401, 0.8161139, 0.6625898)
        Call InitArrayOfDoubles(F, 862.07, 0.0#, 546.0#, 424.0#, 0.0#, 686.1, 0.00000009434955, 0.0#, 768.35, 695.57, 654.0#, 714.0#)
        Call InitArrayOfDoubles(ATFP, 459.67, 459.7, 459.67, 459.69, 459.6, 459.69, 459.69, 459.6, 459.69, 459.67, 459.67, 459.69)

        Call InitArrayOfDoubles(R, 0.078117, 0.088734, 0.102728, 0.1219336, 0.10427, 0.124098, 0.15327, 0.05728, 0.062780807, 0.10805, 0.096125, 0.053645698)
        Call InitArrayOfDoubles(BV, 0.0019, 0.0065093886, 0.0048, 0.0015, 0.0#, 0.002, 0.00125, 0.0#, 0.005914907, 0.006034229, 0.00167, 0.0060114165)
        Call InitArrayOfDoubles(A2, -3.126759, -3.40972713, -3.083417, -2.162959, -7.316, -4.353547, -4.679499, -4.035, -2.3856704, -4.549888, -3.2613344, -1.8947274)
        Call InitArrayOfDoubles(B2, 0.001318523, 0.00159434848, 0.002341695, 0.002135114, 0.0046421, 0.002407252, 0.003472778, 0.002618, 0.0010801207, 0.002308415, 0.0020576287, 0.00098484745)
        Call InitArrayOfDoubles(C2, -35.76999, -56.7627671, -18.212643, -18.941131, 0.0#, -44.066868, -159.775232, 0.0#, -6.5643648, -92.90748, -24.24879, -28.542156)
        Call InitArrayOfDoubles(A3, -0.025341, 0.0602394465, 0.058854, 0.004404057, -0.20382376, -0.017464, 0.012475, -0.0214, 0.034055687, 0.08660634, 0.034866748, 0.026479892)
        Call InitArrayOfDoubles(B3, 0.00004875121, -0.0000187961843, -0.00005671268, 0.00001282818, 0.0003593, 0.0000762789, 0.00007733388, 0.00005, -0.0000053336494, -0.00003141665, -0.0000086791313, -0.000006862101)
        Call InitArrayOfDoubles(C3, 1.220367, 1.31139908, 0.571958, 0.539776, 0.0#, 1.483763, 5.941212, 0.0#, 0.16366057, 2.742282, 0.33274779, 0.66384636)
        Call InitArrayOfDoubles(A4, 0.001687277, -0.00054873701, -0.001026061, 0.0001921072, 0.0#, 0.002310142, 0.002068042, 0.0#, -0.0003857481, -0.0008726016, -0.00085765677, -0.00024565234)
        Call InitArrayOfDoubles(B4, -0.000001805062, 0.0#, 0.000001338679, -0.0000003918263, 0.0#, -0.000003605723, -0.000003684238, 0.0#, 0.0#, 0.0#, 0.00000070240549, 0.0#)
        Call InitArrayOfDoubles(C4, 0.0#, 0.0#, 0.0#, 0.0#, 0.0#, 0.0#, 0.0#, 0.0#, 0.0#, 0.0#, 0.022412368, 0.0#)
        Call InitArrayOfDoubles(A5, -0.0000235893, 0.0#, 0.000005290649, -0.000004481049, 0.0#, -0.00003724044, -0.00003868546, 0.0#, 0.0000016017659, -0.000001375958, 0.0000088368967, 0.00000060887086)
        Call InitArrayOfDoubles(B5, 0.00000002448303, 0.000000003468834, -0.000000007395111, 0.000000009062318, 0.0#, 0.00000005355465, 0.00000006455643, 0.0#, 0.00000000062632341, 0.00000000914957, -0.0000000079168095, 0.0000000008269634)
        Call InitArrayOfDoubles(C5, -0.0001478379, -0.0000254390678, -0.00003874233, -0.00004836678, 0.0#, -0.0001845051, -0.0007394214, 0.0#, -0.000010165314, -0.0002102661, -0.00037167231, -0.00003849145)
        Call InitArrayOfDoubles(A6, 105750400.0#, 0.0#, 73786010.0#, 58388230.0#, 0.0#, 136338700.0#, 75023570.0#, 0.0#, 0.0#, 0.0#, -38257766.0#, 0.0#)
        Call InitArrayOfDoubles(B6, -94721.03, 0.0#, -74355.65, -92639.23, 0.0#, -167261.2, -111420.2, 0.0#, 0.0#, 0.0#, 55816.094, 0.0#)
        Call InitArrayOfDoubles(C6, 0.0#, 0.0#, 0.0#, 0.0#, 0.0#, 0.0#, 0.0#, 0.0#, 0.0#, 0.0#, 1537837700.0#, 0.0#)
        Call InitArrayOfDoubles(AK, 4.5, 5.475, 4.0#, 4.0#, 0.0#, 4.2, 5.5, 0.0#, 3.0#, 5.475, 4.2, 5.0#)
        Call InitArrayOfDoubles(AV, 580.0#, 0.0#, 625.0#, 661.199997, 0.0#, 548.2, 520.0#, 0.0#, 0.0#, 0.0#, 609.0#, 0.0#)
        Call InitArrayOfDoubles(CP, 0.0#, 0.0#, 0.0#, 0.0#, 0.0#, 0.0#, 0.0#, 0.0#, 0.0#, 0.0#, 0.0000007, 0.0#)
        Call InitArrayOfDoubles(TCRP, 848.07, 693.3, 543.6, 409.5, 812.9, 664.5, 538.33, 877.0#, 753.95, 681.59, 639.56, 699.27)
        Call InitArrayOfDoubles(ATFPP, 459.67, 459.7, 459.67, 459.69, 459.69, 459.69, 459.69, 459.69, 459.69, 459.69, 459.67, 459.69)

        '"a" AND "b" IN EQUATION OF STATE ARE CALLED "AV" AND "BV"
        '"C'" IS CALLED "CP".
        '"K" IS CALLED "AK".

        Call InitArrayOfDoubles(AA, 0.023815, 0.0080945, 0.01602, 0.0300559282, 0.0427, 0.02812836, 0.07628087, 0.07963, 0.0175, 0.026803537, 0.020419, 0.0225178157)
        Call InitArrayOfDoubles(BB, 0.0002798823, 0.000332662, 0.0002823, 0.00023704335, 0.00014, 0.0002255408, -0.000007561805, 0.0001159, 0.000349, 0.00028373408, 0.0002996802, 0.000369907814)
        Call InitArrayOfDoubles(CC, -0.0000002123734, -0.0000002413896, -0.0000001159, -0.0000000285660077, 0.0#, -0.00000006509607, 0.00000039065696, 0.0#, -0.000000167, -0.000000097167893, -0.0000001409043, -0.000000164842522)
        Call InitArrayOfDoubles(DD, 0.00000000005999018, 0.0000000000672363, 0.0#, -0.0000000000295338805, 0.0#, 0.0#, -0.0000000002454905, 0.0#, 0.0#, 0.0#, 0.00000000002210861, 0.00000000002152780846)
        Call InitArrayOfDoubles(FF, -336.80703, 0.0#, 0.0#, 0.0#, 0.0#, 257.341, 0.0#, 0.0#, 0.0#, 0.0#, 0.0#, 0.0#)
        Call InitArrayOfDoubles(ATFPPP, 459.67, 459.7, 459.69, 459.69, 459.6, 459.69, 459.69, 459.6, 459.69, 459.69, 459.67, 459.69)

        'LOWERCASE LETTERS "a,b,c,d,f" IN HEAT CAPACITY EQUATION
        'ARE CALLED "AA,BB,CC,DD,FF".

        Call InitArrayOfDoubles(X, 50.5418, 39.556551, 20.911, 86.102162, 0.0#, 62.4009, 0.0#, 25.198, 25.3396621, 46.4734, 35.308, 12.19214242)
        Call InitArrayOfDoubles(Y, -0.0918395, -0.016537936, -0.05676, 0.36172528, 0.0#, -0.0453335, 0.0#, -0.40552, -0.11513718, -0.09012707564, -0.07444, -0.16828871)

    End Sub
#End Region

#Region "Refrigerant Properties"
#Region "Property routine descriptions"
    '    Public Function PSAT(ByVal TCELS As Single, ByVal IR As Integer)
    '        '  Given Tsat (Celsius), calculate Psat (kPa)

    '    Public Function VGAS(ByVal PKPA As Single, ByVal TCELS As Single, ByVal RTOL As Single, ByVal IR As Integer)
    '        '  Given P (kPa) and T (Celsius), calculate V (m3/kg)

    '    Public Function HGAS(ByVal PKPA As Single, ByVal VM3KG As Single, ByVal TCELS As Single, ByVal IR As Integer)
    '        '    Units are kPa, m3/kg, !elsius; Hgas in kJ/kg.

    'TPH?

    '    Public Function DHLAT(ByVal PSATSI As Single, ByVal VSATSI As Single, ByVal TSATSI As Single, ByVal IR As Integer)
    '        '  Latent heat of vaporization.  Constants are for R-11.

    '    Public Function RHOLIQ(ByVal TCEL As Single, ByVal IR As Integer)

    '    Public Function TSAT(ByVal PKPA, ByVal RTOL, ByVal IR)
    '        '   Given vapor Psat (kPa), calculate Tsat (Celsius)

    'TVH?

    '    Public Function PGAS(ByVal VM3KG, ByVal TCELS, ByVal IR)
    '        '  Given V (m3/kg) and T (Celsius), calculate P (kPa)
#End Region
    Public Function PSAT(ByVal TCELS As Single, ByVal IR As Integer)

        ' ----------------------------------------------------------------------
        '
        '  Given Tsat (Celsius), calculate Psat (kPa)
        '
        '  Conversion factors from psia to kPa, and from Celsius to Kelvin:
        '  P(kPa) = 6.89492 * P(psia)
        '  T(Celsius) = (T(Rankine) / 1.8) - 273.15
        '  Constants for R-11 (ASHRAE Trans. 1974,vol.80,part 2, page 158)
        '
        '***********************************************************************
        '
        Const PCONV As Single = 6.89476
        Const TCONV1 As Single = 273.15
        Const TCONV2 As Single = 1.8
        Dim T As Single, CLOG As Single

        T = (TCELS + TCONV1) * TCONV2
        CLOG = Math.Log(Math.Abs(F(IR) - T)) / Math.Log(10)
        PSAT = PCONV * 10.0# ^ (A(IR) + B(IR) / T + C(IR) * (Math.Log(T) / Math.Log(10)) + D(IR) * T + E(IR) * (F(IR) - T) / T * CLOG)

    End Function
    Public Function VGAS(ByVal PKPA As Single, ByVal TCELS As Single, ByVal RTOL As Single, ByVal IR As Integer)

        ' ----------------------------------------------------------------------
        '
        '  Given P (kPa) and T (Celsius), calculate V (m3/kg)
        '
        '  Conversion factors:
        'T(Celsius) = T(Rankine) / 1.8 - 273.15
        'P(kPa) = 6.89492 * P(psia)
        'v(m3 / kg) = v(cu.FT / lbm) / 16.018
        '
        '***********************************************************************

        Dim X11(3) As Double, X22(3) As Double, X33(3) As Double, X44(3) As Double
        Dim P As Single, T As Single, E As Single, T1 As Single, T2 As Single, T3 As Single, T4 As Single, T5 As Single
        Dim ICOR As Integer, V As Single, VINV0 As Single, i As Single, VINV As Single, FN As Single, FV As Single
        Const VCONV As Double = 0.062427961
        Const TCONV1 As Single = 273.15
        Const TCONV2 As Single = 1.8
        Const PCONV As Single = 0.145036

        X11(1) = -0.016739
        X11(2) = -0.0227961
        X11(3) = -0.038263
        X22(1) = -0.0000148616
        X22(2) = -0.00000968957
        X22(3) = -0.000009922
        X33(1) = 3.3399
        X33(2) = 4.0473
        X33(3) = 7.7941
        X44(1) = -299.608
        X44(2) = -294.012
        X44(3) = -579.584

        P = PKPA * PCONV
        T = (TCELS + TCONV1) * TCONV2
        E = Math.Exp(-AK(IR) * T / TCRP(IR))
        T1 = R(IR) * T
        T2 = A2(IR) + B2(IR) * T + C2(IR) * E
        T3 = A3(IR) + B3(IR) * T + C3(IR) * E
        T4 = A4(IR) + B4(IR) * T + C4(IR) * E
        T5 = A5(IR) + B5(IR) * T + C5(IR) * E
        '!---
        '!---Initial guess:  The correlation for R-11 from 0.4 to 575 psia,
        '!---and from Tsat to 1059.67R;  S.D.=0.001659 in range 647 d.f.
        '!---                The correlation for R-12 from 0.4 to 575 psia,
        '!---and from Tsat to 1059.67R;  S.D.=0.002143 in range 647 d.f.
        '!---                The correlation for R-500 from 0.4 to 575 psia,
        '!---and from Tsat to 1059.67R;  S.D.=0.002562 in range 647 d.f.
        '!---
        ICOR = IR
        If (IR > 2) Then ICOR = 3
        V = T1 / P + X22(ICOR) * P + X11(ICOR) + (X33(ICOR) + X44(ICOR) / T1) / T1
        VINV0 = 1.0# / (V - BV(IR))
        '!---
        For i = 1 To 1000
            VINV = VINV0
            FN = ((((T5 * VINV + T4) * VINV + T3) * VINV + T2) * VINV + T1) * VINV - P
            FV = (((5.0# * T5 * VINV + 4.0# * T4) * VINV + 3.0# * T3) * VINV + 2.0# * T2) * VINV + T1
            VINV0 = VINV - FN / FV
            If (Math.Abs(VINV0 - VINV) <= RTOL * VINV0) Then GoTo 20
        Next i

        ' WRITE(1,1)P,T
        ' 1     FORMAT(' WARNING--VGAS(P,T) UNCONVERGED AFTER 30 ITERATIONS'/'   P=',G11.4,'  T=',G11.4)
20:     VGAS = (1.0# / VINV0 + BV(IR)) * VCONV

    End Function
    Public Function HGAS(ByVal PKPA As Single, ByVal VM3KG As Single, ByVal TCELS As Single, ByVal IR As Integer)
        ' ----------------------------------------------------------------------
        '
        '    Units are kPa, m3/kg, Celsius; Hgas in kJ/kg.
        '
        '    CAUTION: in converting Hgas from Btu/lbm to kJ/kg, it's left with
        '          English reference point, not converted to SI reference point.
        '
        '***********************************************************************
        Dim FA3 As Single, FA4 As Single, FA5 As Single, FC3 As Single, FC5 As Single, FB As Single, FC As Single, FD As Single
        Dim P As Single, T As Single, V As Single, VI As Single, CK As Single

        Const AJ As Single = 0.185053
        Const PCNV As Single = 0.14503768
        Const TCNV1 As Single = 273.15
        Const TCNV2 As Single = 1.8
        Const VCNV As Single = 16.01891
        Const HCNV As Single = 2.326

        FA3 = 0.5 * A3(IR)
        FA4 = (1.0# / 3.0#) * A4(IR)
        FA5 = 0.25 * A5(IR)
        FC3 = 0.5 * C3(IR)
        FC5 = 0.25 * C5(IR)
        FB = 0.5 * BB(IR)
        FC = (1.0# / 3.0#) * CC(IR)
        FD = 0.25 * DD(IR)

        P = PKPA * PCNV
        T = (TCELS + TCNV1) * TCNV2
        V = VM3KG * VCNV

        '---------eq. from ASHRAE Trans vol 80 part 2 1974 pp 158-169;
        '---------  consts have been divided by 2,3, or 4 to simplefy eq.
        VI = 1.0# / (V - BV(IR))
        CK = AK(IR) * T / TCRP(IR)
        HGAS = X(IR) - FF(IR) / T + (((FD * T + FC) * T + FB) * T + AA(IR)) * T + AJ * (P * V + (((FA5 * VI + FA4) * VI + FA3) * VI + A2(IR)) * VI + (1.0# + CK) * Math.Exp(-CK) * (((FC5 * VI + C4(IR)) * VI + FC3) * VI + C2(IR)) * VI)
        HGAS = HGAS * HCNV

    End Function
    Public Function TPH(ByVal PKPA As Single, ByVal HKJKG As Single, ByVal RTOL As Single, ByVal IR As Integer)
        Dim X1(3) As Double, X2(3) As Double, X3(3) As Double, X4(3) As Double, X5(3) As Double, X6(3) As Double
        Dim FKEC3 As Single, FKEC5 As Single
        Dim P As Single, H As Single, PLOG As Single, ICOR As Integer, TS As Single, VS As Single, HS As Single, DH As Single, TSEU As Single
        Dim T0 As Single, T01 As Single, T02 As Single, T As Single, TCEL As Single, VM3KG As Single, V As Single, VI As Single, FN As Single, DFNDT As Single
        Dim i As Integer

        Const AJ As Double = 0.185053
        Const PCNV As Double = 0.14503768
        Const TCNV1 As Single = 273.15
        Const TCNV2 As Single = 1.8
        Const VCNV As Single = 16.01891
        Const HCNV As Single = 2.326
        Const TCONV3 As Single = 459.67

        X1(1) = 7.93703
        X1(2) = 8.2267
        X1(3) = 7.06948
        X2(1) = -0.0207321
        X2(2) = -0.027269
        X2(3) = -0.0217131
        X3(1) = 0.000074643
        X3(2) = 0.000090386
        X3(3) = 0.0000567
        X4(1) = -0.265717
        X4(2) = -0.268452
        X4(3) = -0.24733
        X5(1) = -0.36136
        X5(2) = -0.42599
        X5(3) = -0.37881
        X6(1) = 0.0043632
        X6(2) = 0.0043449
        X6(3) = 0.0031129

        FKEC3 = 0.5 * C3(IR)
        FKEC5 = 0.25 * C5(IR)

        P = PKPA * PCNV
        H = HKJKG / HCNV
        PLOG = Math.Log(P) / Math.Log(10)
        '---
        '---Initial guess:  The correlation for R-11 from 0.4 to 575 psia,
        '---and from Tsat to 1059.67R;  S.D.=0.8744 in range 250 d.f.
        '---                The correlation for R-12 from 0.4 to 575 psia,
        '---and from Tsat to 1059.67R;  S.D.=1.436 in range 250 d.f.
        '---                The correlation for R-500 from 0.4 to 575 psia,
        '---and from Tsat to 1059.67R;  S.D.=1.542 in range 250 d.f.
        '---
        ICOR = IR
        If (IR > 2) Then ICOR = 3
        TS = TSAT(PKPA, RTOL, IR)
        VS = VGAS(PKPA, TS, RTOL, IR)
        HS = HGAS(PKPA, VS, TS, IR) / HCNV
        If (H > HS) Then GoTo 5
        TPH = TS / TCNV2 - TCNV1

        Return TPH
5:      DH = H - HS
        TSEU = (TS + TCNV1) * TCNV2
        T01 = (X1(ICOR) + DH * (X2(ICOR) + DH * X3(ICOR)))
        T02 = PLOG * (X4(ICOR) + PLOG * (X5(ICOR) + DH * X6(ICOR)))
        T0 = TSEU + DH * (T01 + T02)

        For i = 1 To 1000
            T = T0
            TS = (TSAT(PKPA, RTOL, IR) + TCNV1) * TCNV2
            If (T < TS) Then T = TS
            TCEL = T / TCNV2 - TCNV1
            VM3KG = VGAS(PKPA, TCEL, RTOL, IR)
            V = VM3KG * VCNV
            VI = 1.0# / (V - BV(IR))
            FN = HGAS(PKPA, VM3KG, TCEL, IR) / HCNV - H
            DFNDT = FF(IR) / T ^ 2 + ((DD(IR) * T + CC(IR)) * T + BB(IR)) * T + AA(IR) - AJ * (AK(IR) / TCRP(IR)) ^ 2 * T * Math.Exp(-AK(IR) * T / TCRP(IR)) * (((FKEC5 * VI + C4(IR)) * VI + FKEC3) * VI + C2(IR)) * VI  'error
            T0 = T - FN / DFNDT
            '  note rel. error tolerance is applied to Fahrenheit temp.
            If (Math.Abs(T - T0) < (RTOL * Math.Abs(T0 - TCONV3) + 0.001)) Then GoTo 20
        Next i

        '       WRITE(1,1)P,H
        '1     FORMAT(' WARNING--TPH(P,H) FAILS TO CONVERGE IN 30 ITERATIONS'/'   P= ',G11.4,' H= ',G11.4)
20:     TPH = T0 / TCNV2 - TCNV1

    End Function
    Public Function DHLAT(ByVal PSATSI As Single, ByVal VSATSI As Single, ByVal TSATSI As Single, ByVal IR As Integer)
        ' ----------------------------------------------------------------------
        '
        '  Latent heat of vaporization.  Constants are for R-11.
        '
        '***********************************************************************
        Dim PSAT As Single, TSAT As Single, VSAT As Single, Tinv As Single, FLOG As Single

        Const LN10 As Double = 2.302585093
        Const LOG10E As Double = 0.4342944819
        Const AJ As Single = 0.185053
        Const PCNV As Double = 0.14503768
        Const TCNV1 As Single = 273.15
        Const TCNV2 As Single = 1.8
        Const VCNV As Single = 16.01891
        Const HCNV As Single = 2.326


        PSAT = PSATSI * PCNV
        TSAT = (TSATSI + TCNV1) * TCNV2
        VSAT = VSATSI * VCNV
        Tinv = 1 / TSAT
        FLOG = 0
        If (F(IR) <= 0) Then GoTo 5
        FLOG = Math.Log(F(IR) - TSAT) / Math.Log(10)
5:      DHLAT = HCNV * (VSAT - 1 / RHOLIQ(TSATSI, IR)) * AJ * TSAT * (PSAT * LN10 * (D(IR) + Tinv * (-B(IR) * Tinv + C(IR) / LN10 - E(IR) * (LOG10E + Tinv * F(IR) * FLOG))))

    End Function
    Public Function RHOLIQ(ByVal TCEL As Single, ByVal IR As Integer)

        Dim T As Single, TF As Single
        Const E1 As Single = 0.3333333333
        Const E2 As Single = 0.6666666667
        Const E3 As Single = 1.333333333
        Const TCNV1 As Single = 273.15
        Const TCNV2 As Single = 1.8
        Const RHOCNV As Single = 16.01846326


        T = (TCEL + TCNV1) * TCNV2
        TF = 1 - T / TCR(IR)

        If TF < 0 Then
            TF = 0 'If Temperature>TCR,let TF=0
        End If
        RHOLIQ = AL(IR) + BL(IR) * TF ^ E1 + CL(IR) * TF ^ E2 + DL(IR) * TF + EL(IR) * TF ^ E3 + FL(IR) * (TF) ^ 0.5 + GL(IR) * TF * TF
        RHOLIQ = RHOLIQ * RHOCNV

    End Function
    Public Function TSAT(ByVal PKPA, ByVal RTOL, ByVal IR)
        ' ----------------------------------------------------------------------
        '
        '   Given vapor Psat (kPa), calculate Tsat (Celsius)
        '
        '***********************************************************************
        Dim AA(3) As Double, BB(3) As Double, CC(3) As Double, DD(3) As Double, EE(3) As Double, FF(3) As Double
        Dim ICOR As Integer, i As Integer, P As Single, PLOG As Single, T0 As Single, TG As Single, CLOG As Single, FN As Single
        Dim FT As Single

        Const CNVLOG As Single = 0.43429448
        Const PCONV As Single = 0.145036
        Const TCONV1 As Single = 273.15
        Const TCONV2 As Single = 1.8
        Const TCONV3 As Single = 459.67

        AA(1) = 428.544
        AA(2) = 350.466
        AA(3) = 348.394
        BB(1) = 0.084447
        BB(2) = 0.079999
        BB(3) = 0.119359
        CC(1) = -0.000037483
        CC(2) = -0.000039864
        CC(3) = -0.000060199
        DD(1) = 69.5313
        DD(2) = 57.5202
        DD(3) = 47.9408
        EE(1) = 12.896
        EE(2) = 10.4648
        EE(3) = 18.4989
        FF(1) = 3.79718
        FF(2) = 3.18191
        FF(3) = 0

        '---The following correlation for Tsat(P) is used as initial guess.
        '---The correlation for R-11 was derived for P between 0.2 and 600.
        '---psia, and T between -76.F and 383.F;  S.D.=0.123 in range (45df)
        '---The correlation for R-12 was derived for T between -140.F and
        '---210.F; S.D.=0.063 in range (170df)
        '---The correlation for R-500 was derived for T between -80.F and
        '---200.F; S.D.=0.094 in range (136df)
        ICOR = IR
        If (IR > 2) Then ICOR = 3
        P = PKPA * PCONV
        PLOG = Math.Log(P) / Math.Log(10)
        T0 = AA(ICOR) + P * (BB(ICOR) + P * CC(ICOR)) + PLOG * (DD(ICOR) + PLOG * (EE(ICOR) + PLOG * FF(ICOR)))

        For i = 1 To 1000
            TG = T0
            CLOG = 0.0#
            If (F(IR) <= 0.0#) Then GoTo 5
            CLOG = Math.Log(F(IR) - TG) / Math.Log(10)
5:          FN = A(IR) + B(IR) / TG + C(IR) * (Math.Log(TG) / Math.Log(10)) + D(IR) * TG + E(IR) * (F(IR) - TG) / TG * CLOG - PLOG
            FT = -B(IR) / TG ^ 2 + CNVLOG * C(IR) / TG + D(IR) - E(IR) * (CNVLOG / TG + F(IR) * CLOG / TG ^ 2)
            T0 = TG - FN / FT
            '  note rel. error tolerance is applied to Fahrenheit temp.
            If (Math.Abs(T0 - TG) < (RTOL * Math.Abs(T0 - TCONV3) + 0.001)) Then GoTo 20
        Next i

        '       WRITE(1,1)P,T0
        ' 1     FORMAT(' WARNING--TSAT(P) FAILS TO CONVERGE IN 30 ITERATIONS'/'   P =',G11.4,'  T=?',G11.4)
20:     TSAT = (T0 / TCONV2) - TCONV1
    End Function
    Public Function TVH(ByVal VM3KG As Single, ByVal HKJKG As Single, ByVal RTOL As Single, ByVal IR As Integer)
        Dim X1(3) As Double, X2(3) As Double, X3(3) As Double, X4(3) As Double, X5(3) As Double, X6(3) As Double, X7(3) As Double, X8(3) As Double, X9(3) As Double, X10(3) As Double
        Dim ICOR As Integer, i As Integer, V As Single, H As Single, VLOG As Single, T0 As Single, T As Single
        Dim TCEL As Single, PKPA As Single, VI As Single, FN As Single, DFNDT As Single
        Dim FAKEC3 As Single, FAKEC5 As Single

        Const AJ As Single = 0.185053
        Const TCNV1 As Single = 273.15
        Const TCNV2 As Single = 1.8
        Const VCNV As Single = 16.01891
        Const HCNV As Single = 2.326
        Const TCONV3 As Single = 459.67
        X1(1) = -443.043
        X1(2) = -281.582
        X1(3) = -315.191
        X2(1) = 13.0283
        X2(2) = 12.3854
        X2(3) = 11.0026
        X3(1) = -0.0405824
        X3(2) = -0.0430709
        X3(3) = -0.0336587
        X4(1) = 0.0000787
        X4(2) = 0.000090069
        X4(3) = 0.0000566285
        X5(1) = -40.165
        X5(2) = -34.544
        X5(3) = -37.647
        X6(1) = 10.437
        X6(2) = 9.153
        X6(3) = 9.886
        X7(1) = -1.2046
        X7(2) = -1.0396
        X7(3) = -1.0856
        X8(1) = 0.29215
        X8(2) = 0.28148
        X8(3) = 0.24745
        X9(1) = -0.026045
        X9(2) = -0.02607
        X9(3) = -0.02337
        X10(1) = 0.00067734
        X10(2) = -0.0007153
        X10(3) = -0.00050261

        FAKEC3 = 0.5 * C3(IR)
        FAKEC5 = 0.25 * C5(IR)

        ICOR = IR
        If (IR > 2) Then ICOR = 3
        H = HKJKG / HCNV
        V = VM3KG * VCNV
        VLOG = Math.Log(V) / Math.Log(10)

        '---Initial guess:  The correlation for R-11 from 0.4 to 575 psia,
        '---and from Tsat to 1059.67R;  S.D.=0.3627 in range 246 d.f.
        '---                The correlation for R-12 from 0.4 to 575 psia,
        '---and from Tsat to 1059.67R;  S.D.=0.7989 in range 246 d.f.
        '---                The correlation for R-500 from 0.4 to 575 psia,
        '---and from Tsat to 1059.67R;  S.D.=0.8470 in range 246 d.f.

        T0 = X1(ICOR) + H * (X2(ICOR) + H * (X3(ICOR) + H * X4(ICOR))) + VLOG * ((X5(ICOR) + VLOG * (X6(ICOR) + VLOG * X7(ICOR))) + H * (X8(ICOR) + VLOG * X9(ICOR) + H * X10(ICOR)))

        For i = 1 To 30
            T = T0
            TCEL = T / TCNV2 - TCNV1
            PKPA = PGAS(VM3KG, TCEL, IR)

            VI = 1.0# / (V - BB(IR))
            FN = HGAS(PKPA, VM3KG, TCEL, IR) / HCNV - H
            DFNDT = FF(IR) / T ^ 2 + ((DD(IR) * T + CC(IR)) * T + BV(IR)) * T + AA(IR) - AJ * (AK(IR) / TCRP(IR)) ^ 2 * T * Math.Exp(-AK(IR) * T / TCRP(IR)) * (((FAKEC5 * VI + C4(IR)) * VI + FAKEC3) * VI + C2(IR)) * VI
            T0 = T - FN / DFNDT
            '  note rel. error tolerance is applied to Fahrenheit temp.
            If (Math.Abs(T - T0) < (RTOL * Math.Abs(T0 - TCONV3) + 0.001)) Then GoTo 20
        Next i

        '        WRITE(1,1)V,H
        '  1     FORMAT(' WARNING--TVH(V,H) FAILS TO CONVERGE IN 30 ITERATIONS'/'   V= ',G11.4,' H= ',G11.4)
20:     TVH = T0 / TCNV2 - TCNV1

    End Function
    Public Function PGAS(ByVal VM3KG, ByVal TCELS, ByVal IR)

        ' ----------------------------------------------------------------------
        '
        '  Given V (m3/kg) and T (Celsius), calculate P (kPa)
        '
        '  Conversion factors:
        'T(Celsius) = T(Rankine) / 1.8 - 273.15
        'P(kPa) = 6.89492 * P(psia)
        'V(m3 / kg) = V(cu.FT / lbm) / 16.018
        '
        '***********************************************************************
        Dim T As Single, V As Single, E As Single, T1 As Single, T2 As Single, T3 As Single, T4 As Single, T5 As Single, VINV As Single, P As Single
        Const VCONV As Single = 16.01846326
        Const TCONV1 As Single = 273.15
        Const TCONV2 As Single = 1.8
        Const PCONV As Single = 6.89492

        V = VM3KG * VCONV
        T = (TCELS + TCONV1) * TCONV2
        E = Math.Exp(-AK(IR) * T / TCRP(IR))
        T1 = R(IR) * T
        T2 = A2(IR) + B2(IR) * T + C2(IR) * E
        T3 = A3(IR) + B3(IR) * T + C3(IR) * E
        T4 = A4(IR) + B4(IR) * T + C4(IR) * E
        T5 = A5(IR) + B5(IR) * T + C5(IR) * E

        VINV = 1.0# / (V - BV(IR))
        P = ((((T5 * VINV + T4) * VINV + T3) * VINV + T2) * VINV + T1) * VINV
        PGAS = P * PCONV
        Return PGAS
        End

    End Function
#End Region

End Module
