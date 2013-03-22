Class Algebra

	'Author: Yves Vander Haeghen (Yves.VanderHaeghen@UGent.be)
	'Version: 1.0
	'VersionDate": 13 june 2003


	'Class of helper functions for simple algebra operations on 1 and 2 dimensional single arrays
	'Although speed is not essential, we try to avoid recreating and reallocating output arrays 
	'on every call as this could slow things down a lot. This means that usually the output arrays MUST
	'be allocated and passed to the functions, except when they are passed on by reference.
	'All matrices are supposedly ordered ROW x COLUMN

	'August 2003: Added non-linear optimization (Nelder-Mead simplex algorithm)

	Enum NormOrder As Integer
		AbsoluteValue = 1
		Euclidean = 2
		Max = 16
	End Enum

	'Defines for NMS algorithm
	Private Const NMSMAX = 30000
	Private Const NMSTINY = 0.000000001
	Private Const NMSTOL = 0.0000025 'Machine precision?

	'Helper function for NMS algorithm
	Private Shared Sub Swap(ByRef sA As Single, ByRef sB As Single)
		Dim sTemp As Single
		sTemp = sA
		sA = sB
		sB = sTemp
	End Sub

	'Prototype for the function to be optimized
	Delegate Function SolveNonLinearError(ByVal sX() As Single) As Single

	Public Shared Function SolveNonLinear(ByVal sX(,) As Single, _
	  ByVal sY() As Single, _
	  ByVal lNrIterations As Long, _
	  ByVal ErrorFunction As SolveNonLinearError) As Boolean
		'Minimize a function of iNrDim dimensions using the Nelder-Mead
		'simplex algorythm (NMS). sX is a (iNrDim + 1) by iNrDim matrix
		'initialized with a starting simplex. sY is a iNrDim vector with
		'function values at the simplex points.
		Dim iNrDims As Integer
		Dim iNrPts As Integer, iLo As Integer, iHi As Integer
		Dim i As Integer, j As Integer, iNHi As Integer
		Dim sSum() As Single, sYSave As Single, sYTry As Single
		Dim sRTol As Single, iDisplayCounter As Integer

		SolveNonLinear = False

		iNrDims = sX.GetUpperBound(1) + 1
		iNrPts = iNrDims + 1
		ReDim sSum(iNrDims - 1)
		lNrIterations = 0
		iDisplayCounter = 0
		Sum(sX, sSum)
		Do
			'Rank vertices of simplex by function value
			iLo = 0
			If sY(0) > sY(1) Then
				iNHi = 1
				iHi = 0
			Else
				iNHi = 0
				iHi = 1
			End If

			For i = 0 To iNrPts - 1
				If sY(i) <= sY(iLo) Then iLo = i
				If sY(i) > sY(iHi) Then
					iNHi = iHi
					iHi = i
				ElseIf (sY(i) > sY(iNHi)) And (i <> iHi) Then
					iNHi = i
				End If
			Next i

			'TEST
			'Debug.Print "Highest vertex: " & iHi & ", next " & iNHi
			'Debug.Print "Lowest vertex: " & iLo
			iDisplayCounter = iDisplayCounter + 1
			sRTol = 2.0# * Math.Abs(sY(iHi) - sY(iLo)) / (Math.Abs(sY(iHi)) + Math.Abs(sY(iLo)) + NMSTINY)
			If iDisplayCounter Mod 200 = 0 Then
				Console.WriteLine("Iteration " & lNrIterations & ", best solution has error: " & sY(iLo))
			End If

			'Convergence criterium
			If sRTol < NMSTOL Then
				Swap(sY(0), sY(iLo))
				For i = 0 To iNrDims - 1
					Swap(sX(0, i), sX(iLo, i))
				Next i

				Dim sCoef(iNrDims - 1) As Single
				Console.WriteLine("Convergence after " & lNrIterations & " iterations, with error " & sY(iLo))
				GetMatrixRow(sX, sCoef, iLo)
				Console.WriteLine("Parameters are " & ToString(sCoef))

				Exit Do
			End If

			If lNrIterations > NMSMAX Then
				'Do not raise error, result is useful most of the time!
				'NMSErrorType = NMSTooManyIterations
				'GoTo ErrorHandler
				Exit Function
			End If
			lNrIterations = lNrIterations + 2

			sYTry = SolveNonLinearAdjustSimplex(sX, sY, sSum, iNrDims, iHi, -1.0#, ErrorFunction)

			If sYTry < sY(iLo) Then
				sYTry = SolveNonLinearAdjustSimplex(sX, sY, sSum, iNrDims, iHi, 2.0#, ErrorFunction)
			ElseIf sYTry > sY(iNHi) Then
				sYSave = sY(iHi)
				sYTry = SolveNonLinearAdjustSimplex(sX, sY, sSum, iNrDims, iHi, 0.5, ErrorFunction)
				If sYTry >= sYSave Then
					For i = 0 To iNrPts - 1
						If i <> iLo Then
							For j = 0 To iNrDims - 1
								sX(i, j) = 0.5 * (sX(i, j) + sX(iLo, j))
								sSum(j) = sX(i, j)
							Next j
							sY(i) = ErrorFunction(sSum)
						End If
					Next i
					lNrIterations = lNrIterations + iNrDims
					Sum(sX, sSum)
				Else
					lNrIterations = lNrIterations - 1
				End If
			End If
		Loop While True
		SolveNonLinear = True
	End Function

	Private Shared Function SolveNonLinearAdjustSimplex(ByVal sX(,) As Single, _
	 ByVal sY() As Single, _
	 ByVal sSum() As Single, _
	 ByVal iNrDims As Integer, _
	 ByVal iHi As Integer, _
	 ByVal sFactor As Single, _
	 ByVal ErrorFunction As SolveNonLinearError) As Single
		Dim i As Integer, sFactor1 As Single, sFactor2 As Single
		Dim sYTry As Single, sXTry(iNrDims - 1) As Single

		'Debug.Print "Try adjustment simplex with factor " & sFactor
		sFactor1 = (1.0# - sFactor) / iNrDims
		sFactor2 = sFactor1 - sFactor
		For i = 0 To iNrDims - 1
			sXTry(i) = sSum(i) * sFactor1 - sX(iHi, i) * sFactor2
		Next i
		sYTry = ErrorFunction(sXTry)
		'Console.WriteLine("Proposed vertex " & ToString(sXTry) & "Value " & sYTry)

		If sYTry < sY(iHi) Then
			sY(iHi) = sYTry
			For i = 0 To iNrDims - 1
				sSum(i) = sSum(i) + sXTry(i) - sX(iHi, i)
				sX(iHi, i) = sXTry(i)
			Next i
			'DisplayMatrix "New simplex", sX()
		Else
			'Debug.Print "Vertex rejected"
		End If
		SolveNonLinearAdjustSimplex = sYTry
	End Function

	Public Shared Sub SolveNonLinearTest(ByVal iNrDims As Integer)

		Dim sCoef() As Single, iVertexNr As Integer
		Dim sSimplex(,) As Single, iNrVertices As Integer
		Dim sSimplexVal() As Single, lNrIterations As Long
		Dim i As Integer

		Randomize()
		iNrVertices = iNrDims + 1
		ReDim sCoef(iNrDims - 1)
		ReDim sSimplex(iNrVertices - 1, iNrDims - 1)
		ReDim sSimplexVal(iNrVertices - 1)
		For iVertexNr = 0 To iNrVertices - 1
			For i = 0 To iNrDims - 1
				If iVertexNr > 0 And i = iVertexNr - 1 Then
					sCoef(i) = 1.0 * Rnd()
				Else
					sCoef(i) = 0.0#
				End If
			Next i

			'Put in simplex and compute function value
			SetMatrixRow(sSimplex, sCoef, iVertexNr)
			sSimplexVal(iVertexNr) = NelderMeadMinimizeFunction(sCoef)

		Next iVertexNr

		'Optimize
		SolveNonLinear(sSimplex, sSimplexVal, lNrIterations, AddressOf NelderMeadMinimizeFunction) ' SolveNonLinearTestError)
	End Sub

	Public Shared Function NelderMeadMinimizeFunction(ByVal sCoef() As Single) As Single

		'should attempt to minimize the value of 2x + 3 = 0, thus x should be -1.5
		Return Math.Abs(2 * sCoef(0) + 3)

	End Function


	Public Shared Function SolveNonLinearTestError(ByVal sCoef() As Single) As Single
		'Return the error
		Dim i, iNrDims As Integer, sError As Single = 100
		iNrDims = sCoef.GetUpperBound(0) + 1

		For i = 0 To iNrDims - 1
			If i Mod 2 = 0 Then
				sError += sCoef(i) * i
			Else
				sError -= sCoef(i) * i
			End If
		Next i
		Return Math.Abs(sError)
	End Function

	Public Overloads Shared Function Solve(ByVal sA(,) As Single, ByVal sX(,) As Single, ByVal sY(,) As Single) As Boolean
		'Solve A.X = Y, FOR every column of Y!!!
		'This is useful because we only have to decompose A once, 
		'and then use this decomposition to compute X = inv(A).Y for every column of Y
		'The results are stored in the corresponding columns of X
		'See overloaded Solve for general explanation about the solver.
		Dim sU(,) As Single, sW() As Single, sV(,) As Single, i As Integer
		Dim strError As String

		If SVDDecomposition(sA, sU, sW, sV, strError) = False Then
			MsgBox("Algebra.Solve: SVD gives error '" & strError & "'", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
			Return False
		End If

		SVDRemoveSingularValues(sW, 0.0001)

		'Run though every column of sY, compute the result, and store it in the corresponding column of sX.
		Dim iNrEquationSets As Integer = sY.GetUpperBound(1) + 1
		Dim iNrVariables As Integer = sA.GetUpperBound(1) + 1
		Dim iNrEquationsPerSet As Integer = sA.GetUpperBound(0) + 1
		Dim sXCol(iNrVariables - 1), sYCol(iNrEquationsPerSet - 1) As Single
		For i = 0 To iNrEquationSets - 1
			GetMatrixColumn(sY, sYCol, i)
			Solve(sA, sXCol, sYCol)
			SetMatrixColumn(sX, sXCol, i)
		Next
	End Function

	Public Overloads Shared Function Solve(ByVal sA(,) As Single, ByVal sX() As Single, ByVal sY() As Single) As Boolean
		'Solve the set of linear equations represented by A.x = y.
		'The number of equations can be larger than the number of variables (overdetermined):
		'i.e. the number of rows in A > number of cols in A. In that case the solution is 
		'a solution in the least-squares sense.
		'This routine uses singular value decomposition, translated from "Numerical recipes in C"
		Dim sU(,) As Single, sW() As Single, sV(,) As Single
		Dim strError As String

		Console.WriteLine("Solving linear set of equations A.x = y with A" & _
		  vbNewLine & Algebra.ToString(sA) & _
		  vbNewLine & "y" & _
		  vbNewLine & Algebra.ToString(sY))

		If SVDDecomposition(sA, sU, sW, sV, strError) = False Then
			MsgBox("Algebra.Solve: SVD gives error '" & strError & "'", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
			Return False
		End If

		SVDRemoveSingularValues(sW, 0.0001)

		'Compute pseudo-inverse multiplied with sY
		SVDInvert(sU, sW, sV, sY, sX)
		Return True
	End Function

	Private Shared Sub SVDRemoveSingularValues(ByVal sW() As Single, ByVal sThresholdFactor As Single)
		'Set singular values to zero by compairing them to
		'the highest value in w. 
		Dim iNrVariables As Integer = sW.GetUpperBound(0) + 1
		Dim i As Integer, sWMax As Single = 0.0

		For i = 0 To iNrVariables - 1
			If sW(i) > sWMax Then sWMax = sW(i)
		Next i
		Dim sThreshold As Single = sThresholdFactor * sWMax
		For i = 0 To iNrVariables - 1
			If sW(i) < sThreshold Then sW(i) = 0.0
		Next i
	End Sub

	Private Shared Sub SVDInvert(ByVal sU(,) As Single, _
	  ByVal sW() As Single, _
	  ByVal sV(,) As Single, _
	  ByVal sY() As Single, _
	  ByVal sX() As Single)
		'Computes Y = inv(A).Y using the SVD decomposition of A = U.W.Vt
		Dim jj, j, i, m, n As Integer
		Dim s As Single

		m = sU.GetUpperBound(0) + 1
		n = sU.GetUpperBound(1) + 1

		Dim tmp(n - 1) As Single
		For j = 1 To n
			s = 0.0
			If sW(j - 1) <> 0.0 Then
				For i = 1 To m
					s = s + sU(i - 1, j - 1) * sY(i - 1)
				Next i
				s = s / sW(j - 1)
			End If

			tmp(j - 1) = s
		Next j

		For j = 1 To n
			s = 0.0
			For jj = 1 To n
				s = s + sV(j - 1, jj - 1) * tmp(jj - 1)
			Next jj
			sX(j - 1) = s
		Next j
	End Sub

	Private Shared Function SVDDecomposition(ByVal sA(,) As Single, _
	  ByRef sU(,) As Single, _
	  ByRef sW() As Single, _
	  ByRef sV(,) As Single, _
	  ByVal strError As String) As Boolean

		'Compute the singular value decomposition of
		'an m sx n matrix A: A = U.W.Vt
		'None of the byref matrices must be allocated here.
		'If something goes wrong it returns false with a message in strError
		Dim Flag As Boolean, i As Integer, its As Integer
		Dim j As Integer, jj As Integer, k As Integer
		Dim l As Integer, nm As Integer
		Dim c As Single, f As Single, h As Single, s As Single
		Dim sX As Single, sY As Single, sz As Single, rv1() As Single
		Dim anorm As Single, g As Single, hhscale As Single
		'Extra variables for VBasic.
		Dim sTemp1 As Single, n As Integer, m As Integer

		m = sA.GetUpperBound(0) + 1
		n = sA.GetUpperBound(1) + 1

		If m < n Then
			strError = "Not enough rows in A (underdetermined system)"
			Return False
		End If

		ReDim sU(m - 1, n - 1)
		ReDim sW(n - 1)
		ReDim sV(n - 1, n - 1)
		ReDim rv1(n - 1)

		'Copy the matrix A in U.
        Array.Copy(sA, sU, sA.Length)

		'Householder reduction to bidiagonal form
		anorm = 0.0#
		For i = 1 To n
			l = i + 1
			rv1(i - 1) = hhscale * g
			g = 0.0#
			s = 0.0#
			hhscale = 0.0#
			If i <= m Then
				For k = i To m
					hhscale = hhscale + Math.Abs(sU(k - 1, i - 1))
				Next k

				If hhscale <> 0.0# Then
					For k = i To m
						sU(k - 1, i - 1) = sU(k - 1, i - 1) / hhscale
						s = s + sU(k - 1, i - 1) * sU(k - 1, i - 1)
					Next k

					f = sU(i - 1, i - 1)
					If f >= 0 Then
						g = -Math.Sqrt(s)
					Else
						g = Math.Sqrt(s)
					End If

					h = f * g - s
					sU(i - 1, i - 1) = f - g
					If i <> n Then
						For j = l To n
							s = 0.0#
							For k = i To m
								s = s + sU(k - 1, i - 1) * sU(k - 1, j - 1)
							Next k
							f = s / h
							For k = i To m
								sU(k - 1, j - 1) = sU(k - 1, j - 1) + f * sU(k - 1, i - 1)
							Next k
						Next j
					End If

					For k = i To m
						sU(k - 1, i - 1) = sU(k - 1, i - 1) * hhscale
					Next k

				End If
			End If

			sW(i - 1) = hhscale * g
			g = 0.0#
			s = 0.0#
			hhscale = 0.0#
			If i <= m And i <> n Then
				For k = l To n
					hhscale = hhscale + Math.Abs(sU(i - 1, k - 1))
				Next k

				If hhscale <> 0.0# Then
					For k = l To n
						sU(i - 1, k - 1) = sU(i - 1, k - 1) / hhscale
						s = s + sU(i - 1, k - 1) * sU(i - 1, k - 1)
					Next k

					f = sU(i - 1, l - 1)
					If f >= 0 Then
						g = -Math.Sqrt(s)
					Else
						g = Math.Sqrt(s)
					End If
					h = f * g - s
					sU(i - 1, l - 1) = f - g

					For k = l To n
						rv1(k - 1) = sU(i - 1, k - 1) / h
					Next k

					If i <> m Then
						For j = l To m
							s = 0.0#
							For k = l To n
								s = s + sU(j - 1, k - 1) * sU(i - 1, k - 1)
							Next k

							For k = l To n
								sU(j - 1, k - 1) = sU(j - 1, k - 1) + s * rv1(k - 1)
							Next k
						Next j
					End If

					For k = l To n
						sU(i - 1, k - 1) = sU(i - 1, k - 1) * hhscale
					Next k

				End If
			End If

			sTemp1 = Math.Abs(sW(i - 1)) + Math.Abs(rv1(i - 1))
			If anorm < sTemp1 Then anorm = sTemp1
		Next i
		'Call DisplayMatrix("Bidiagonal form", a())

		'Accumulation of right-hand transformations
		For i = n To 1 Step -1
			If i < n Then
				If g <> 0.0# Then
					For j = l To n
						sV(j - 1, i - 1) = (sU(i - 1, j - 1) / sU(i - 1, l - 1)) / g
					Next j

					For j = l To n
						s = 0.0#
						For k = l To n
							s = s + sU(i - 1, k - 1) * sV(k - 1, j - 1)
						Next k

						For k = l To n
							sV(k - 1, j - 1) = sV(k - 1, j - 1) + s * sV(k - 1, i - 1)
						Next k
					Next j
				End If

				For j = l To n
					sV(i - 1, j - 1) = 0.0#
					sV(j - 1, i - 1) = 0.0#
				Next j

			End If

			sV(i - 1, i - 1) = 1.0#
			g = rv1(i - 1)
			l = i
		Next i

		'Accumulation of left-hand transformations
		For i = n To 1 Step -1
			l = i + 1
			g = sW(i - 1)
			If i < n Then
				For j = l To n
					sU(i - 1, j - 1) = 0.0#
				Next j
			End If

			If g <> 0.0# Then
				g = 1.0# / g
				If i <> n Then
					For j = l To n
						s = 0.0#
						For k = l To m
							s = s + sU(k - 1, i - 1) * sU(k - 1, j - 1)
						Next k

						f = (s / sU(i - 1, i - 1)) * g
						For k = i To m
							sU(k - 1, j - 1) = sU(k - 1, j - 1) + f * sU(k - 1, i - 1)
						Next k
					Next j
				End If

				For j = i To m
					sU(j - 1, i - 1) = sU(j - 1, i - 1) * g
				Next j
			Else
				For j = i To m
					sU(j - 1, i - 1) = 0.0#
				Next j
			End If

			sU(i - 1, i - 1) = sU(i - 1, i - 1) + 1.0#
		Next i

		'Diagonalization of the bidiagonal form (QR algorythm)
		For k = n To 1 Step -1
			For its = 1 To 30
				'Debug.Print "Iteration " & its
				Flag = True
				For l = k To 1 Step -1
					nm = l - 1
					If Math.Abs(rv1(l - 1)) + anorm = anorm Then
						Flag = False
						Exit For
					End If

					If Math.Abs(sW(nm - 1)) + anorm = anorm Then
						Exit For
					End If
				Next l

				If Flag = True Then
					c = 0.0#
					s = 1.0#
					For i = l To k
						f = s * rv1(i - 1)
						If (Math.Abs(f) + anorm) <> anorm Then
							g = sW(i - 1)
							h = Pythagoras(f, g)
							sW(i - 1) = h
							h = 1.0# / h
							c = g * h
							s = (-f * h)
							For j = 1 To m
								sY = sU(j - 1, nm - 1)
								sz = sU(j - 1, i - 1)
								sU(j - 1, nm - 1) = sY * c + sz * s
								sU(j - 1, i - 1) = sz * c - sY * s
							Next j
						End If
					Next i
				End If
				sz = sW(k - 1)

				'Test for convergence
				If l = k Then
					If sz < 0.0# Then
						sW(k - 1) = -sz
						For j = 1 To n
							sV(j - 1, k - 1) = -sV(j - 1, k - 1)
						Next j
					End If
					Exit For
				End If

				If its = 30 Then
					strError = "Too many iterations"
					Return False
				End If

				sX = sW(l - 1)
				nm = k - 1
				sY = sW(nm - 1)
				g = rv1(nm - 1)
				h = rv1(k - 1)
				f = ((sY - sz) * (sY + sz) + (g - h) * (g + h)) / (2.0# * h * sY)
				g = Pythagoras(f, 1.0#)
				If f > 0.0# Then
					f = ((sX - sz) * (sX + sz) + h * ((sY / (f + Math.Abs(g))) - h)) / sX
				Else
					f = ((sX - sz) * (sX + sz) + h * ((sY / (f - Math.Abs(g))) - h)) / sX
				End If

				c = 1.0#
				s = 1.0#
				For j = l To nm
					i = j + 1
					g = rv1(i - 1)
					sY = sW(i - 1)
					h = s * g
					g = c * g
					sz = Pythagoras(f, h)
					rv1(j - 1) = sz
					c = f / sz
					s = h / sz
					f = sX * c + g * s
					g = g * c - sX * s
					h = sY * s
					sY = sY * c
					For jj = 1 To n
						sX = sV(jj - 1, j - 1)
						sz = sV(jj - 1, i - 1)
						sV(jj - 1, j - 1) = sX * c + sz * s
						sV(jj - 1, i - 1) = sz * c - sX * s
					Next jj
					sz = Pythagoras(f, h)
					sW(j - 1) = sz
					If sz <> 0.0# Then
						sz = 1.0# / sz
						c = f * sz
						s = h * sz
					End If
					f = c * g + s * sY
					sX = c * sY - s * g
					For jj = 1 To m
						sY = sU(jj - 1, j - 1)
						sz = sU(jj - 1, i - 1)
						sU(jj - 1, j - 1) = sY * c + sz * s
						sU(jj - 1, i - 1) = sz * c - sY * s
					Next jj
				Next j
				rv1(l - 1) = 0.0#
				rv1(k - 1) = f
				sW(k - 1) = sX
			Next its
		Next k
		Return True
	End Function

	Private Shared Function Pythagoras(ByVal a As Single, ByVal b As Single) As Single
		Dim at As Single, bt As Single, ct As Single

		at = Math.Abs(a)
		bt = Math.Abs(b)
		If at > bt Then
			ct = bt / at
			Pythagoras = at * Math.Sqrt(1.0# + ct * ct)
		Else
			If bt = 0.0# Then
				'Means a is also 0
				Pythagoras = 0.0#
			Else
				ct = at / bt
				Pythagoras = bt * Math.Sqrt(1.0# + ct * ct)
			End If
		End If
	End Function

	Public Overloads Shared Sub Add(ByVal sV1() As Single, ByVal sV2() As Single, ByVal sR() As Single)
		Dim i, iHiCol As Integer
		iHiCol = sV1.GetUpperBound(0)
		For i = 0 To iHiCol
			sR(i) = sV1(i) + sV2(i)
		Next
	End Sub

	Public Overloads Shared Sub Add(ByVal sM1(,) As Single, ByVal sM2(,) As Single, ByVal sMR(,) As Single)
		Dim i, j, iHiRow, iHiCol As Integer
		GetBounds(sM1, iHiRow, iHiCol)
		For j = 0 To iHiCol
			For i = 0 To iHiRow
				sMR(i, j) = sM1(i, j) + sM2(i, j)
			Next i
		Next j
	End Sub

	Public Overloads Shared Sub Subtract(ByVal sV1() As Single, ByVal sV2() As Single, ByVal sR() As Single)
		Dim i As Integer, iHiCol As Integer
		iHiCol = sV1.GetUpperBound(0)
		For i = 0 To iHiCol
			sR(i) = sV1(i) - sV2(i)
		Next
	End Sub

	Public Overloads Shared Sub Subtract(ByVal sM1(,) As Single, ByVal sM2(,) As Single, ByVal sMR(,) As Single)
		Dim i, j, iHiRow, iHiCol As Integer
		GetBounds(sM1, iHiRow, iHiCol)
		For j = 0 To iHiCol
			For i = 0 To iHiRow
				sMR(i, j) = sM1(i, j) - sM2(i, j)
			Next i
		Next j
	End Sub

	Public Overloads Shared Function Norm(ByVal sV1() As Single) As Single
		Return Norm(sV1, NormOrder.Euclidean)
	End Function

	Public Overloads Shared Function Norm(ByVal sV1() As Single, ByVal iOrder As NormOrder) As Single
		'Compute norm of given order
		Dim i As Integer, sNorm As Single = 0.0, iHiCol As Integer
		iHiCol = sV1.GetUpperBound(0)
		Select Case iOrder
			Case NormOrder.AbsoluteValue
				For i = 0 To iHiCol
					sNorm += Math.Abs(sV1(i))
				Next
			Case NormOrder.Euclidean
				For i = 0 To iHiCol
					sNorm += sV1(i) ^ 2
				Next
				sNorm = sNorm ^ 0.5
			Case NormOrder.Max
				sNorm = 0
				For i = 0 To iHiCol
					Dim sTemp As Single = Math.Abs(sV1(i))
					If sTemp > sNorm Then sNorm = sTemp
				Next
		End Select
		Return sNorm
	End Function

	Public Overloads Shared Sub Mean(ByVal sM(,) As Single, ByVal sV() As Single)
		'Compute columnwise mean
		Dim i, iHiCol As Integer
		iHiCol = sV.GetUpperBound(0)
		Sum(sM, sV)
		For i = 0 To iHiCol
			sV(i) = sV(i) / (iHiCol + 1)
		Next i
	End Sub

	Public Overloads Shared Function Mean(ByVal sV() As Single) As Single
		'Compute average of a vector
		Dim sMean As Single
		sMean = Sum(sV)
		sMean /= sV.GetLength(0)
		Return sMean
	End Function

	Public Overloads Shared Sub Sum(ByVal sM(,) As Single, ByVal sV() As Single)
		'Compute columnwise sum of matrix
		Dim i, j, iHiRow, iHiCol As Integer
		GetBounds(sM, iHiRow, iHiCol)

		For j = 0 To iHiCol
			sV(j) = 0.0
			For i = 0 To iHiRow
				sV(j) += sM(i, j)
			Next i
		Next j
	End Sub

	Public Overloads Shared Function Sum(ByVal sV() As Single) As Single
		'Compute sum of elements of vector
		Dim sSum As Single = 0
		Dim i, iHiCol As Integer
		iHiCol = sV.GetUpperBound(0)

		For i = 0 To iHiCol
			sSum = sSum + sV(i)
		Next i
		Return sSum
	End Function

	Public Overloads Shared Function Max(ByVal sV() As Single, ByRef iPos As Integer) As Single
		'Find max of a vector
		Dim i As Integer, sMax As Single = 0.0
		Dim iHiCol As Integer
		iHiCol = sV.GetUpperBound(0)

		For i = 0 To iHiCol
			If sV(i) > sMax Then
				iPos = i
				sMax = sV(i)
			End If
		Next i
		Return sMax
	End Function

	Public Overloads Shared Function Max(ByVal sV() As Single) As Single
		Dim iPos As Integer
		Return Max(sV, iPos)
	End Function

	Public Overloads Shared Function Max(ByVal sM(,) As Single) As Single
		'Find max of a matrix
		Dim i, j As Integer
		Return Max(sM, i, j)
	End Function

	Public Overloads Shared Function Max(ByVal sM(,) As Single, ByRef iCol As Integer, ByRef iRow As Integer) As Single
		'Find max of a matrix
		Dim sMAx As Single = 0
		Dim i, j, iHiRow, iHiCol As Integer
		GetBounds(sM, iHiRow, iHiCol)
		For j = 0 To iHiCol
			For i = 0 To iHiRow
				If sM(i, j) > sMAx Then
					iCol = j
					iRow = i
					sMAx = sM(i, j)
				End If
			Next i
		Next j
		Return sMAx
	End Function

	Public Overloads Shared Function Scale(ByVal sX As Single, ByVal sOffset As Single, ByVal sScale As Single) As Single
		'Scale a scalar with an offset. For vectors and matrices this would lead to too many 
		'different versions, so use Subtract to have an offset.
		Return (sX - sOffset) * sScale
	End Function

	Public Overloads Shared Sub Scale(ByVal sScale As Single, _
	 ByVal sV2() As Single, _
	 ByVal sY() As Single)
		'Scale elements of vector V2 using the scalar sScale
		Dim i As Integer, iHiRow As Integer
		iHiRow = UBound(sV2)
		For i = 0 To iHiRow
			sY(i) = sScale * sV2(i)
		Next i
	End Sub

	Public Overloads Shared Sub Scale(ByVal sV1() As Single, _
	 ByVal sV2() As Single, _
	 ByVal sY() As Single)
		'Scale elements of vector V2 using the elements of V1
		Dim i As Integer, iHiRow As Integer
		iHiRow = UBound(sV2)
		For i = 0 To iHiRow
			sY(i) = sV1(i) * sV2(i)
		Next i
	End Sub

	Public Overloads Shared Sub Scale(ByVal sScale As Single, _
	  ByVal sB(,) As Single, _
	  ByVal sY(,) As Single)
		'Scale elements of matrix sB using  sScale
		Dim i, j, iHiRow, iHiCol As Integer
		GetBounds(sB, iHiRow, iHiCol)
		For i = 0 To iHiRow
			For j = 0 To iHiCol
				sY(i, j) = sScale * sB(i, j)
			Next j
		Next i
	End Sub

	Public Overloads Shared Sub Scale(ByVal sA(,) As Single, _
	  ByVal sB(,) As Single, _
	  ByVal sY(,) As Single)
		'Scale elements of matrix sB using the corresponding elements of sA
		Dim i, j, iHiRow, iHiCol As Integer
		GetBounds(sB, iHiRow, iHiCol)
		For i = 0 To iHiRow
			For j = 0 To iHiCol
				sY(i, j) = sA(i, j) * sB(i, j)
			Next j
		Next i
	End Sub

	Public Overloads Shared Sub Scale(ByVal sRowScales() As Single, _
	 ByVal sB(,) As Single, _
	 ByVal sY(,) As Single)
		'Scale elements of matrix sB using the corresponding elements of sRowScales, per ROW
		Dim i, j, iHiRow, iHiCol As Integer
		GetBounds(sB, iHiRow, iHiCol)
		For i = 0 To iHiRow
			For j = 0 To iHiCol
				sY(i, j) = sRowScales(i) * sB(i, j)
			Next j
		Next i
	End Sub

	Public Overloads Shared Sub Scale(ByVal sB(,) As Single, _
	 ByVal sColScales() As Single, _
	 ByVal sY(,) As Single)
		'Scale elements of matrix sB using the corresponding elements of sColScales, per col
		Dim i, j, iHiRow, iHiCol As Integer
		GetBounds(sB, iHiRow, iHiCol)
		For i = 0 To iHiRow
			For j = 0 To iHiCol
				sY(i, j) = sColScales(j) * sB(i, j)
			Next j
		Next i
	End Sub

	Public Overloads Shared Sub Product(ByVal sA(,) As Single, _
	  ByVal sB(,) As Single, _
	  ByVal sC(,) As Single)
		'Compute A * B and store in C. 
		'Raise a fatal run-time error if any errors (no return value)!
		Dim i, j, k, iAHiRow, iAHiCol As Integer
		GetBounds(sA, iAHiRow, iAHiCol)
		Dim iBHiRow, iBHiCol As Integer
		GetBounds(sB, iBHiRow, iBHiCol)
		Dim iCHiRow, iCHiCol As Integer
		GetBounds(sC, iCHiRow, iCHiCol)

		If (((iAHiCol) <> (iBHiRow)) Or _
		 ((iAHiRow) <> (iCHiRow)) Or _
		 ((iBHiCol) <> (iCHiCol))) Then
			MsgBox("Algebra.Product: Incompatible matrix dimensions", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical)
		End If

		For i = 0 To iCHiRow
			For j = 0 To iCHiCol
				sC(i, j) = 0.0
				For k = 0 To iAHiCol
					sC(i, j) += sA(i, k) * sB(k, j)
				Next k
			Next j
		Next i
	End Sub

	Public Overloads Shared Function Product(ByVal sV1() As Single, ByVal sV2() As Single) As Single
		'Return the scalar product of two vectors.
		Dim i As Integer, iHiRow As Integer, sResult As Single

		iHiRow = UBound(sV1)
		For i = 0 To iHiRow
			sResult = sResult + sV1(i) * sV2(i)
		Next i
		Return sResult
	End Function

	Public Overloads Shared Sub Product(ByVal sM() As Single, _
	   ByVal sX() As Single, _
	   ByVal sY(,) As Single)
		'Multiply a vector times a vector (Y = M.Y), by interpreting the vector M as a columnmatrix,
		'and X as a rowmatrix. Result is a matrix
		Dim sA(0, sM.GetUpperBound(0)) As Single, sB(sX.GetUpperBound(0), 0) As Single

		SetMatrixColumn(sA, sM, 0)
		SetMatrixRow(sB, sX, 0)
		Product(sA, sB, sY)
	End Sub

	Public Overloads Shared Sub Product(ByVal sM(,) As Single, _
	   ByVal sX() As Single, _
	   ByVal sY() As Single)
		'Multiply a matrix times a vector (y = M.x), by interpreting the vector X as a columnmatrix.
		Dim sB(sX.GetUpperBound(0), 0), sC(sM.GetUpperBound(0), 0) As Single

		SetMatrixColumn(sB, sX, 0)
		Product(sM, sB, sC)
		GetMatrixColumn(sC, sY, 0)
	End Sub

	Public Overloads Shared Sub Product(ByVal sX() As Single, _
	   ByVal sM(,) As Single, _
	   ByVal sY() As Single)
		'Multiply a vector with a matrix (y = x.M), by interpreting the vector X as a rowmatrix.
		Dim iHiCol As Integer = sX.GetUpperBound(0)
		Dim sB(0, iHiCol), sC(0, iHiCol) As Single

		SetMatrixRow(sB, sX, 0)
		Product(sM, sB, sC)
		GetMatrixRow(sC, sY, 0)
	End Sub

	Public Shared Sub SubMatrix(ByVal sA(,) As Single, _
	   ByVal sB(,) As Single, _
	   ByVal iRow As Integer, _
	   ByVal iCol As Integer)
		'Extract submatrix of the dimensions of B using row and col
		'as start values in sA. sA and sB can be mixed one and zero-
		'based, but iRow and iCol are interpreted according to sA
		Dim i, j, iHiRow, iHiCol As Integer
		GetBounds(sB, iHiRow, iHiCol)
		For i = 0 To iHiRow
			For j = 0 To iHiCol
				sB(i, j) = sA(i + iRow, j + iCol)
			Next j
		Next i
	End Sub

	Public Overloads Shared Sub GetMatrixColumn(ByVal sM(,) As Single, _
	   ByVal sV() As Single, _
	   ByVal iCol As Integer)
		GetMatrixColumn(sM, sV, iCol, 0)
	End Sub

	Public Overloads Shared Sub GetMatrixColumn(ByVal sM(,) As Single, _
	   ByVal sV() As Single, _
	   ByVal iCol As Integer, _
	   ByVal iStartRow As Integer)
		'Fill vector with matrix col
		Dim i, iHiCol As Integer
		iHiCol = sV.GetUpperBound(0)
		For i = 0 To iHiCol
			sV(i) = sM(i + iStartRow, iCol)
		Next i
	End Sub

	Public Overloads Shared Sub GetMatrixRow(ByVal sM(,) As Single, _
	 ByVal sV() As Single, _
	 ByVal iRow As Integer)
		GetMatrixRow(sM, sV, iRow, 0)
	End Sub

	Public Overloads Shared Sub GetMatrixRow(ByVal sM(,) As Single, _
	   ByVal sV() As Single, _
	   ByVal iRow As Integer, _
	   ByVal iStartCol As Integer)
		'Fill vector with matrix row. 
		Dim i, iHiCol As Integer
		iHiCol = sV.GetUpperBound(0)
		For i = 0 To iHiCol
			sV(i) = sM(iRow, i + iStartCol)
		Next i
	End Sub

	Public Overloads Shared Sub SetMatrixColumn(ByVal sM(,) As Single, _
	  ByVal sV() As Single, _
	  ByVal iCol As Integer, _
	  ByVal iStartRow As Integer)
		'Fill matrix col with vector
		Dim i, iHiCol As Integer
		iHiCol = sV.GetUpperBound(0)
		For i = 0 To iHiCol
			sM(i + iStartRow, iCol) = sV(i)
		Next i
	End Sub

	Public Overloads Shared Sub SetMatrixColumn(ByVal sM(,) As Single, _
	   ByVal sV() As Single, _
	   ByVal iCol As Integer)
		SetMatrixColumn(sM, sV, iCol, 0)
	End Sub

	Public Overloads Shared Sub SetMatrixRow(ByVal sM(,) As Single, _
	 ByVal sV() As Single, _
	 ByVal iRow As Integer)
		SetMatrixRow(sM, sV, iRow, 0)
	End Sub

	Public Overloads Shared Sub SetMatrixRow(ByVal sM(,) As Single, _
	   ByVal sV() As Single, _
	   ByVal iRow As Integer, _
	   ByVal iStartCol As Integer)
		Dim i, iHiCol As Integer
		iHiCol = sV.GetUpperBound(0)
		For i = 0 To iHiCol
			sM(iRow, i + iStartCol) = sV(i)
		Next i
	End Sub

	Public Shared Sub MatrixToVector(ByVal sM(,) As Single, _
	   ByVal sV() As Single)
		'Put all elements of a matrix into a vector
		Dim i, j, iHiRow, iHiCol, k As Integer

		GetBounds(sM, iHiRow, iHiCol)
		k = 0
		For i = 0 To iHiRow
			For j = 0 To iHiCol
				sV(k) = sM(i, j)
				k += 1
			Next
		Next
	End Sub

	Public Shared Sub VectorToMatrix(ByVal sV() As Single, _
	   ByVal sM(,) As Single)
		'Put all elements of a vector into a vector. Use the shape of the matrix
		Dim i, j, iHiRow, iHiCol, k As Integer

		GetBounds(sM, iHiRow, iHiCol)
		k = 0
		For i = 0 To iHiRow
			For j = 0 To iHiCol
				sM(i, j) = sV(k)
				k += 1
			Next
		Next
	End Sub

	Public Shared Sub Transpose(ByVal sA(,) As Single, ByVal sAt(,) As Single)
		'Transpose matrix A and put result in At. Output has
		'same base as input. Input arguments must be different!
		Dim i, j, iHiRow, iHiCol As Integer
		GetBounds(sA, iHiRow, iHiCol)

		For i = 0 To iHiRow
			For j = 0 To iHiCol
				sAt(j, i) = sA(i, j)
			Next j
		Next i
	End Sub

	Public Shared Function Load(ByVal strFile As String, ByRef sM(,) As Single) As Boolean
		'Read a tex file with a matrix or vector stored separated by spaces and
		'newlines. sM will be redimensioned as necessary and must be
		'a dynamic array. Redimensioning can only affect the last dimension!
		'When a vector is read in the matrix will be of size n x 1, and can easily 
		'be converted to a vector
		Dim iNrCols As Integer
        Dim iRowNr As Integer, iColNr As Integer
		Dim sMt(,) As Single, strText As String, strTextItems() As String

		Try
			FileOpen(5, strFile, OpenMode.Input, OpenAccess.Read)
		Catch e As Exception
			MsgBox("Algebra.Load:" & e.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.Critical)
			Return False
		End Try

		iRowNr = 0
		iNrCols = 0
		Do While Not EOF(5)
			'Read first line to count number of columns
			strText = Trim(LineInput(5))

			If strText.Length > 0 Then
				strText = strText.Replace("  ", " ") 'Make sure no 2 spaces are in the string ...
				strText = strText.Replace("   ", " ") 'Make sure no 3 spaces are in the string ...
				strTextItems = strText.Split()

				'Redimension the array if the nr of cols is known, i.e. after
				'reading the first line.
				If iRowNr = 0 Then
					iNrCols = (strTextItems.GetUpperBound(0) + 1)
					ReDim sMt(iNrCols - 1, 0)
				Else
					ReDim Preserve sMt(iNrCols - 1, iRowNr)
				End If

				'Read values into transposed matrix
				For iColNr = 0 To iNrCols - 1
					'sMt(iColNr, iRowNr) = CSng(strTextItems(iColNr))
					sMt(iColNr, iRowNr) = Val(strTextItems(iColNr))
				Next
				iRowNr += 1
			End If
		Loop

		'close file
		FileClose(5)

		'Transpose matrix to output format
		ReDim sM(iRowNr - 1, iNrCols - 1)
		Transpose(sMt, sM)
		Return True
	End Function

	Public Overloads Shared Sub Save(ByVal strFile As String, _
	  ByVal sM(,) As Single)
		Save(strFile, sM, 16, 2)
	End Sub

	Public Overloads Shared Sub Save(ByVal strFile As String, _
	 ByVal sM(,) As Single, _
	 ByVal iPrecBeforeDec As Integer, _
	 ByVal iPrecAfterDec As Integer)
		'Save a matrix to file.
		Dim strF As String
		Dim i, j, iHiRow, iHiCol As Integer

		If iPrecAfterDec = -1 Then
			strF = "0."
		Else
			For i = 1 To iPrecAfterDec
				strF = strF & "0"
			Next
			strF = strF & "."
		End If

		For i = 1 To iPrecBeforeDec
			strF = strF & "#"
		Next

		If System.IO.File.Exists(strFile) Then System.IO.File.Delete(strFile)

		Try
			FileOpen(5, strFile, OpenMode.Output, OpenAccess.Write)
			GetBounds(sM, iHiRow, iHiCol)
			For i = 0 To iHiRow
				For j = 0 To iHiCol - 1
					Print(5, Format(sM(i, j), strF), SPC(1))
				Next j
				PrintLine(5, SPC(1), Format(sM(i, iHiCol), strF))
			Next i
			FileClose(5)
		Catch e As Exception
			MsgBox("Algebra.Save (file = " & strFile & "):" & e.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.Critical)
		End Try
	End Sub

	Private Shared Sub GetBounds(ByVal sM(,) As Single, _
	  ByRef iHiRow As Integer, _
	  ByRef iHiCol As Integer)
		iHiRow = sM.GetUpperBound(0)
		iHiCol = sM.GetUpperBound(1)
	End Sub

	Public Overloads Shared Function ToString(ByVal sM(,) As Single) As String
		Dim strText As String = vbNewLine
		Dim i, j, iHiRow, iHiCol As Integer
		GetBounds(sM, iHiRow, iHiCol)
		For i = 0 To iHiRow
			For j = 0 To iHiCol - 1
				strText = strText & sM(i, j).ToString & " "
			Next j
			strText = strText & sM(i, iHiCol).ToString & vbNewLine
		Next i
		Return strText
	End Function

	Public Overloads Shared Function ToString(ByVal sV() As Single) As String
		Dim strText As String
		Dim i, iHiCol As Integer
		iHiCol = sV.GetUpperBound(0)
		For i = 0 To iHiCol - 1
			strText = strText & sV(i).ToString & " "
		Next i
		strText = vbNewLine & strText & sV(iHiCol).ToString
		Return strText
	End Function

End Class

#Region "Test Class for Nelder Mead"
'Public Class Color
'	'Author: Yves Vander Haeghen (Yves.VanderHaeghen@UGent.be)
'	'Version: 1.0
'	'VersionDate": 13 june 2003

'	'MS surely fucked up its implementation of color and color transforms!!!!
'	'Some methods are not static and need the class to be instantiated. 

'	'Enums used as array indices to make code more readable
'	Public Enum RGB
'		red = 0
'		green = 1
'		blue = 2
'	End Enum

'	Public Enum CIEXYZ
'		X = 0
'		Y = 1
'		Z = 2
'	End Enum

'	Public Enum CIELab
'		L = 0
'		a = 1
'		b = 2
'	End Enum

'	Public Enum WhitePoint
'		CIED65 = 0
'		CIED55 = 1
'		CIEA = 2
'		CIEC = 3
'		CIED93 = 4
'		CIED50 = 5
'		HDTVD65 = 6
'	End Enum

'	Public Enum ColorSpace
'		GenericRGB = 0
'		GenericGammaRGB
'		sRGB
'		sGammaRGB
'		CIEXYZ
'		CIELab
'	End Enum

'	Public Enum ColorSpaceTransform
'		'They use the number of terms as ID, so we can decude them from the 
'		'transformation matrix by counting the rows
'		Linear = 3
'		NonLinear6Term = 6
'		NonLinear8Term = 8
'		NonLinear9Term = 9
'		NonLinear11Term = 11
'		NonLinear14Term = 14
'		NonLinear20Term = 20
'	End Enum

'	'The Gamma LUT, Rec709 transforms and white point matrices
'	'Class variables
'	Private sWhite(6, 2), sGammaLut(255), sXYZToSRGB(2, 2), sSRGBtoXYZ(2, 2) As Single

'	'Some shared variables
'	Private Shared sColorSpaceTransformDesignMatrix(,), sColorSpaceTransformAimCIELabColors(,) As Single
'	Private Shared iColorSpaceTransformMetricPower As Integer = 1

'	Public Sub New()
'		'Object constructor
'		MyBase.new()

'		'Initalise the LUTs and whitepoint arrays
'		Dim iPixel As Integer, sScaledPixel As Single

'		'Whitepoints.
'		sWhite(WhitePoint.HDTVD65, CIEXYZ.X) = 94.825
'		sWhite(WhitePoint.HDTVD65, CIEXYZ.Y) = 100.0# 'Poynton
'		sWhite(WhitePoint.HDTVD65, CIEXYZ.Z) = 107.391

'		sWhite(WhitePoint.CIED65, CIEXYZ.X) = 95.017
'		sWhite(WhitePoint.CIED65, CIEXYZ.Y) = 100.0# 'Wyszecki
'		sWhite(WhitePoint.CIED65, CIEXYZ.Z) = 108.813

'		sWhite(WhitePoint.CIED55, CIEXYZ.X) = 95.642
'		sWhite(WhitePoint.CIED55, CIEXYZ.Y) = 100.0# 'Wyszecki
'		sWhite(WhitePoint.CIED55, CIEXYZ.Z) = 92.085

'		sWhite(WhitePoint.CIEA, CIEXYZ.X) = 109.828
'		sWhite(WhitePoint.CIEA, CIEXYZ.Y) = 100.0# 'Wyszecki
'		sWhite(WhitePoint.CIEA, CIEXYZ.Z) = 35.547

'		sWhite(WhitePoint.CIEC, CIEXYZ.X) = 98.041
'		sWhite(WhitePoint.CIEC, CIEXYZ.Y) = 100.0# 'Wyszecki
'		sWhite(WhitePoint.CIEC, CIEXYZ.Z) = 118.103

'		sWhite(WhitePoint.CIED50, CIEXYZ.X) = 96.3963
'		sWhite(WhitePoint.CIED50, CIEXYZ.Y) = 100.0# 'Wyszecki
'		sWhite(WhitePoint.CIED50, CIEXYZ.Z) = 82.4145

'		'Transform 3x3 from XYZ to sRGB using Rec 709 primaries
'		sXYZToSRGB(0, 0) = 3.240479 / 100.0#
'		sXYZToSRGB(1, 0) = -0.969256 / 100.0#
'		sXYZToSRGB(2, 0) = 0.055648 / 100.0#
'		sXYZToSRGB(0, 1) = -1.53715 / 100.0#
'		sXYZToSRGB(1, 1) = 1.875992 / 100.0#
'		sXYZToSRGB(2, 1) = -0.204043 / 100.0#
'		sXYZToSRGB(0, 2) = -0.498535 / 100.0#
'		sXYZToSRGB(1, 2) = 0.041556 / 100.0#
'		sXYZToSRGB(2, 2) = 1.057311 / 100.0#

'		'Transform 3x3 from sRGB to XYZ using Rec 709 primaries
'		sSRGBtoXYZ(0, 0) = 0.412453 * 100.0#
'		sSRGBtoXYZ(1, 0) = 0.212671 * 100.0#
'		sSRGBtoXYZ(2, 0) = 0.019334 * 100.0#
'		sSRGBtoXYZ(0, 1) = 0.35758 * 100.0#
'		sSRGBtoXYZ(1, 1) = 0.71516 * 100.0#
'		sSRGBtoXYZ(2, 1) = 0.119193 * 100.0#
'		sSRGBtoXYZ(0, 2) = 0.180423 * 100.0#
'		sSRGBtoXYZ(1, 2) = 0.072169 * 100.0#
'		sSRGBtoXYZ(2, 2) = 0.950227 * 100.0#

'		'Rec709 Gamma function LUT
'		'from non-linear GammaSRGB [0 255] to linear SRGB [0.0 1.0]
'		For iPixel = 0 To 255
'			sScaledPixel = iPixel / 255.0#
'			If sScaledPixel <= 0.081 Then
'				sGammaLut(iPixel) = sScaledPixel / 4.5
'			Else
'				sGammaLut(iPixel) = ((sScaledPixel + 0.099) / 1.099) ^ (1 / 0.45)
'			End If
'		Next iPixel
'	End Sub

'	Public Shared Function RGBName(ByVal i As Integer)
'		'Return the name associated with a color component
'		Select Case i
'			Case RGB.blue
'				Return "Blue"
'			Case RGB.green
'				Return "Green"
'			Case RGB.red
'				Return "Red"
'		End Select
'	End Function

'	Public Shared Function CIEXYZName(ByVal i As Integer)
'		'Return the name associated with a color  component 
'		Select Case i
'			Case CIEXYZ.X
'				Return "X"
'			Case CIEXYZ.Y
'				Return "Y"
'			Case CIEXYZ.Z
'				Return "Z"
'		End Select
'	End Function

'	Public Overloads Shared Sub RGBTosRGB(ByVal sRGB() As Single, _
'			   ByVal sSRGB() As Single, _
'			   ByVal sTransform(,) As Single)
'		'Transform linear RGB using a polynomial mapping technique.
'		'RGB both in [0.0 1.0] range. Convenience function, it just makes code more readable.
'		TransformColor(sRGB, sSRGB, sTransform)
'	End Sub

'	Public Overloads Shared Sub RGBTosRGB(ByVal sRGB(,) As Single, _
'			   ByVal sSRGB(,) As Single, _
'			   ByVal sTransform(,) As Single)
'		'Transform linear RGB using a polynomial mapping technique.
'		'RGB both in [0.0 1.0] range. Convenience function, it just makes code more readable.
'		TransformColor(sRGB, sSRGB, sTransform)
'	End Sub

'	Public Overloads Shared Sub TransformColor(ByVal sC1(,) As Single, _
'		  ByVal sC2(,) As Single, _
'		  ByVal sTransform(,) As Single)
'		Dim sC1Row(2), sC2Row(2) As Single
'		Dim iHiRow, i As Integer
'		iHiRow = sC1.GetUpperBound(0)
'		For i = 0 To iHiRow
'			Algebra.GetMatrixRow(sC1, sC1Row, i)
'			TransformColor(sC1Row, sC2Row, sTransform)
'			Algebra.SetMatrixRow(sC2, sC2Row, i)
'		Next
'	End Sub

'	Public Overloads Shared Sub TransformColor(ByVal sC1() As Single, _
'				 ByVal sC2() As Single, _
'				 ByVal sTransform(,) As Single)
'		'Transform linear tristimulus sC1 to sC2 using a polynomial mapping technique.
'		'Both tristimuli in [0.0 1.0] range
'		Dim iNrTerms As Integer = sTransform.GetUpperBound(1) + 1
'		Dim sCNL(iNrTerms - 1) As Single

'		'Compute color N-tuple 
'		ColorTripletToNTuple(sC1, sCNL, iNrTerms)

'		'Left multiply color N-tuple with transform
'		Algebra.Product(sTransform, sCNL, sC2)
'	End Sub

'	Public Overloads Sub XYZToCIELab(ByVal sXYZ() As Single, _
'		  ByVal sLab() As Single)
'		XYZToCIELab(sXYZ, sLab, WhitePoint.CIED65)
'	End Sub

'	Public Overloads Sub XYZToCIELab(ByVal sXYZ() As Single, _
'			ByVal sLab() As Single, _
'			ByVal iWhite As Integer)
'		'XYZ to CIELab transform.
'		Dim sY As Single

'		sY = sXYZ(CIEXYZ.Y) / sWhite(iWhite, CIEXYZ.Y)
'		sLab(CIELab.L) = 116 * CIEf(sY) - 16.0#
'		sLab(CIELab.a) = 500 * (CIEf(sXYZ(CIEXYZ.X) / sWhite(iWhite, CIEXYZ.X)) - CIEf(sY))
'		sLab(CIELab.b) = 200 * (CIEf(sY) - CIEf(sXYZ(CIEXYZ.Z) / sWhite(iWhite, CIEXYZ.Z)))
'	End Sub

'	Public Overloads Sub CIELabToXYZ(ByVal sLab() As Single, _
'			  ByVal sXYZ() As Single)
'		CIELabToXYZ(sLab, sXYZ, WhitePoint.CIED65)
'	End Sub

'	Public Overloads Sub CIELabToXYZ(ByVal sLab() As Single, _
'			ByVal sXYZ() As Single, _
'			ByVal iWhite As Integer)
'		'CIELab to XYZ transform.
'		Dim sTemp As Single, i As Integer

'		sTemp = (sLab(0) + 16.0#) / 116.0#
'		sXYZ(CIEXYZ.Y) = CIEfInverse(sTemp) * sWhite(iWhite, CIEXYZ.Y)
'		sXYZ(CIEXYZ.X) = CIEfInverse(sLab(1) / 500.0# + sTemp) * sWhite(iWhite, CIEXYZ.X)
'		sXYZ(CIEXYZ.Z) = CIEfInverse(-sLab(2) / 200.0# + sTemp) * sWhite(iWhite, CIEXYZ.Z)
'		For i = 0 To 2
'			If sXYZ(i) < 0 Then sXYZ(i) = 0
'		Next i
'	End Sub

'	Public Shared Function CIELabTodE(ByVal sLab1() As Single, _
'			  ByVal sLab2() As Single)
'		Dim sDeltaLab(2) As Single
'		Algebra.Subtract(sLab1, sLab2, sDeltaLab)
'		Return Algebra.Norm(sDeltaLab)
'	End Function

'	Public Shared Function CIELabTodC(ByVal sLab1() As Single, _
'			 ByVal sLab2() As Single)
'		'Use only chromatic info, not luminance
'		CIELabTodC = ((sLab1(CIELab.a) - sLab2(CIELab.a)) ^ 2 + (sLab1(CIELab.b) - sLab2(CIELab.b)) ^ 2) ^ 0.5
'	End Function

'	Public Sub CIELabTosRGB(ByVal sLab() As Single, _
'		  ByVal sRGB() As Single)
'		'Input must have D65 whitepoint!
'		Dim sXYZ(2) As Single
'		CIELabToXYZ(sLab, sXYZ, WhitePoint.CIED65)
'		XYZToSRGB(sXYZ, sRGB)
'	End Sub

'	Public Sub sRGBToCIELab(ByVal sRGB() As Single, _
'		  ByVal sLab() As Single)
'		'Output has D65 whitepoint!
'		Dim sXYZ(2) As Single
'		sRGBtoXYZ(sRGB, sXYZ)
'		XYZToCIELab(sXYZ, sLab, WhitePoint.CIED65)
'	End Sub

'	Public Overloads Function sRGBTodE(ByVal sSRGB1() As Single, ByVal sSRGB2() As Single) As Single
'		'Output has D65 whitepoint!
'		Dim sLab1(2), sLab2(2) As Single
'		sRGBToCIELab(sSRGB1, sLab1)
'		sRGBToCIELab(sSRGB2, sLab2)
'		Return CIELabTodE(sLab1, sLab2)
'	End Function

'	Public Overloads Sub sRGBTodE(ByVal sSRGB1(,) As Single, ByVal sSRGB2(,) As Single, ByVal sdE() As Single)
'		'Output has D65 whitepoint!
'		Dim i As Integer, iMax As Integer = sSRGB1.GetUpperBound(0)

'		For i = 0 To iMax
'			Dim sRGB1(2), sRGB2(2) As Single

'			Algebra.GetMatrixRow(sSRGB1, sRGB1, i)
'			Algebra.GetMatrixRow(sSRGB2, sRGB2, i)
'			sdE(i) = sRGBTodE(sRGB1, sRGB2)
'		Next
'	End Sub

'	Public Overloads Function XYZTodE(ByVal sXYZ1() As Single, _
'		   ByVal sXYZ2() As Single) As Single
'		Return XYZTodE(sXYZ1, sXYZ2, WhitePoint.CIED65)
'	End Function

'	Public Overloads Function XYZTodE(ByVal sXYZ1() As Single, _
'		  ByVal sXYZ2() As Single, _
'		  ByVal iWhite As Integer) As Single
'		'Compute dE from two XYZ tristimulus values. We take some shortcuts here for speed
'		Dim sDLab(2) As Single

'		sDLab(CIELab.L) = 116 * (CIEf(sXYZ1(CIEXYZ.Y) / sWhite(iWhite, CIEXYZ.Y)) - _
'		  CIEf(sXYZ2(CIEXYZ.Y) / sWhite(iWhite, CIEXYZ.Y)))
'		sDLab(CIELab.a) = 500 * (CIEf(sXYZ1(CIEXYZ.X) / sWhite(iWhite, CIEXYZ.X)) - _
'		  CIEf(sXYZ1(CIEXYZ.Y) / sWhite(iWhite, CIEXYZ.Y)) - _
'		  CIEf(sXYZ2(CIEXYZ.X) / sWhite(iWhite, CIEXYZ.X)) + _
'		  CIEf(sXYZ2(CIEXYZ.Y) / sWhite(iWhite, CIEXYZ.Y)))
'		sDLab(CIELab.b) = 200 * (CIEf(sXYZ1(CIEXYZ.Y) / sWhite(iWhite, CIEXYZ.Y)) - _
'		  CIEf(sXYZ1(CIEXYZ.Z) / sWhite(iWhite, CIEXYZ.Z)) - _
'		  CIEf(sXYZ2(CIEXYZ.Y) / sWhite(iWhite, CIEXYZ.Y)) + _
'		  CIEf(sXYZ2(CIEXYZ.Z) / sWhite(iWhite, CIEXYZ.Z)))
'		Return Algebra.Norm(sDLab)
'	End Function

'	Private Function CIEf(ByVal sValue As Single) As Single
'		'CIE f function for the computation of CIE Lab values

'		If sValue >= 0.008856 Then
'			CIEf = sValue ^ 0.3333333333
'		Else
'			CIEf = 7.787 * sValue + 0.1379310344828
'		End If
'	End Function

'	Private Function CIEfInverse(ByVal sValue As Single) As Single
'		'CIE f function for the computation of CIE Lab values

'		If sValue >= 0.206892706482 Then
'			CIEfInverse = sValue ^ 3
'			'Debug.Print "CIE f inverse of " & sValue & ": " & CIEfInverse
'		Else
'			CIEfInverse = (sValue - 0.1379310344828) / 7.787
'			'Debug.Print "CIE f inverse (linear part) of " & sValue & ": " & CIEfInverse
'		End If
'	End Function

'	Public Sub XYZToSRGB(ByVal sXYZ() As Single, ByVal sRGB() As Single)
'		'Transform CIE XYZ to RGB using the Rec 709 primaries (sRGB space)
'		'RGB in [0.0 1.0] range, XYZ in [0.0 1xx.x] range
'		Algebra.Product(sXYZToSRGB, sXYZ, sRGB)
'	End Sub

'	Public Sub sRGBtoXYZ(ByVal sRGB() As Single, ByVal sXYZ() As Single)
'		'Transform RGB to XYZ using the Rec 709 primaries (sRGB space)
'		'RGB in [0.0 1.0] range, XYZ in [0.0 1xx.X] range
'		Algebra.Product(sSRGBtoXYZ, sRGB, sXYZ)
'	End Sub

'	Public Sub XYZToxyY(ByVal sXYZ() As Single, ByVal sXYY() As Single)
'		Dim sSum As Single

'		sSum = sXYZ(CIEXYZ.X) + sXYZ(CIEXYZ.Y) + sXYZ(CIEXYZ.Z)
'		sXYY(0) = sXYZ(CIEXYZ.X) / sSum
'		sXYY(1) = sXYZ(CIEXYZ.Y) / sSum
'		sXYY(2) = sXYZ(CIEXYZ.Y)
'	End Sub

'	Public Shared Function GammaCorrection(ByVal sIn As Single) As Single
'		'Apply gamma correction from input range [0.0 1.0] to [0.0 1.0]
'		'Do clipping
'		If sIn >= 1.0# Then
'			GammaCorrection = 1.0#
'		ElseIf sIn < 0.0# Then
'			GammaCorrection = 0.0#
'		ElseIf sIn <= 0.018 Then
'			GammaCorrection = 4.5 * sIn
'		Else
'			GammaCorrection = 1.099 * sIn ^ 0.45 - 0.099
'		End If
'	End Function

'	Public Shared Function InverseGammaCorrection(ByVal sIn As Single) As Single
'		'Apply gamma correction from input range [0.0 1.0] to [0.0 1.0]
'		If sIn <= 0.081 Then
'			InverseGammaCorrection = sIn / 4.5
'		Else
'			InverseGammaCorrection = ((sIn + 0.099) / 1.099) ^ 2.22222222
'		End If
'	End Function

'	Public Shared Sub Windowing(ByVal sRGB() As Single, _
'		   ByVal sOffsetRGB() As Single, _
'		   ByVal sScaleRGB() As Single, _
'		   ByVal sWindowedRGB() As Single)
'		'Apply subwindow selection to a RGB value: sWRGB = (sRGB - sOffsetRGB) .* sScaleRGB
'		sWindowedRGB(RGB.red) = Algebra.Scale(sRGB(RGB.red), sOffsetRGB(RGB.red), sScaleRGB(RGB.red))
'		sWindowedRGB(RGB.green) = Algebra.Scale(sRGB(RGB.green), sOffsetRGB(RGB.green), sScaleRGB(RGB.green))
'		sWindowedRGB(RGB.blue) = Algebra.Scale(sRGB(RGB.blue), sOffsetRGB(RGB.blue), sScaleRGB(RGB.blue))
'	End Sub

'	Public Shared Sub Windowing2(ByVal sRGB() As Single, _
'			ByVal sLowRGB() As Single, _
'			ByVal sHighRGB() As Single, _
'			ByVal sWindowedRGB() As Single)
'		'Apply subwindow selection to a RGB value: sWRGB = (sRGB - sLowRGB) .* (1 / (sHigh - sLowRGB))
'		'Use only for small groups of values, otherwise precompute the scale and use Windowing
'		sWindowedRGB(RGB.red) = Algebra.Scale(sRGB(RGB.red), sLowRGB(RGB.red), 1 / (sHighRGB(RGB.red) - sLowRGB(RGB.red)))
'		sWindowedRGB(RGB.green) = Algebra.Scale(sRGB(RGB.green), sLowRGB(RGB.green), 1 / (sHighRGB(RGB.green) - sLowRGB(RGB.green)))
'		sWindowedRGB(RGB.blue) = Algebra.Scale(sRGB(RGB.blue), sLowRGB(RGB.blue), 1 / (sHighRGB(RGB.blue) - sLowRGB(RGB.blue)))
'	End Sub

'	Public Shared Sub RGBToGammaRGB(ByVal sRGB() As Single, ByVal byGammaRGB() As Byte)
'		'Gamma correction. Input RGB in [0.0 1.0], output RGB [0 255].
'		byGammaRGB(RGB.red) = CByte(GammaCorrection(sRGB(RGB.red)) * 255)
'		byGammaRGB(RGB.green) = CByte(GammaCorrection(sRGB(RGB.green)) * 255)
'		byGammaRGB(RGB.blue) = CByte(GammaCorrection(sRGB(RGB.blue)) * 255)
'	End Sub

'	Public Shared Sub RGBToGammaRGB(ByVal sRGB() As Single, ByVal sGammaRGB() As Single)
'		'Gamma correction. Input RGB in [0.0 1.0], output RGB [0 255].
'		sGammaRGB(RGB.red) = GammaCorrection(sRGB(RGB.red)) * 255.0
'		sGammaRGB(RGB.green) = GammaCorrection(sRGB(RGB.green)) * 255.0
'		sGammaRGB(RGB.blue) = GammaCorrection(sRGB(RGB.blue)) * 255.0
'	End Sub

'	Public Overloads Shared Sub GammaRGBToRGB(ByVal sGammaRGB() As Single, ByVal sRGB() As Single)
'		'Inverse gamma correction, single precision input.
'		'Output RGB in [0.0 1.0], input RGB [0.0 255.0].
'		'Input maybe same array as output.
'		sRGB(RGB.red) = InverseGammaCorrection(sGammaRGB(RGB.red) / 255.0#)
'		sRGB(RGB.green) = InverseGammaCorrection(sGammaRGB(RGB.green) / 255.0#)
'		sRGB(RGB.blue) = InverseGammaCorrection(sGammaRGB(RGB.blue) / 255)
'	End Sub

'	Public Overloads Shared Sub GammaRGBToRGB(ByVal sGammaRGB() As Single, ByVal sRGB() As Single, ByVal sLUT(,) As Single)
'		'Inverse gamma correction, single precision input, but with a LUT BEFORE the gamma-correction
'		'Output RGB in [0.0 1.0], input RGB [0.0 255.0].
'		'Input maybe same array as output.
'		sRGB(RGB.red) = InverseGammaCorrection(sLUT(CInt(sGammaRGB(RGB.red)), RGB.red) / 255.0#)
'		sRGB(RGB.green) = InverseGammaCorrection(sLUT(CInt(sGammaRGB(RGB.green)), RGB.green) / 255.0#)
'		sRGB(RGB.blue) = InverseGammaCorrection(sLUT(CInt(sGammaRGB(RGB.blue)), RGB.blue) / 255)
'	End Sub

'	Public Overloads Shared Sub GammaRGBToRGB(ByVal byGammaRGB() As Byte, ByVal sRGB() As Single, ByVal sLUT(,) As Single)
'		'Inverse gamma correction, single precision input, but with a LUT BEFORE the gamma-correction
'		'Output RGB in [0.0 1.0], input RGB [0.0 255.0].
'		'Input maybe same array as output.
'		sRGB(RGB.red) = InverseGammaCorrection(sLUT(byGammaRGB(RGB.red), RGB.red) / 255.0#)
'		sRGB(RGB.green) = InverseGammaCorrection(sLUT(byGammaRGB(RGB.green), RGB.green) / 255.0#)
'		sRGB(RGB.blue) = InverseGammaCorrection(sLUT(byGammaRGB(RGB.blue), RGB.blue) / 255)
'	End Sub

'	Public Overloads Sub GammaRGBToRGBbyLUT(ByVal byGammaRGB() As Byte, ByVal sRGB() As Single)
'		'Inverse gamma correction, byte input. Faster with LUT, but not shared!
'		'Output RGB in [0.0 1.0], input RGB [0 255].
'		sRGB(RGB.red) = sGammaLut(byGammaRGB(RGB.red))
'		sRGB(RGB.green) = sGammaLut(byGammaRGB(RGB.green))
'		sRGB(RGB.blue) = sGammaLut(byGammaRGB(RGB.blue))
'	End Sub

'	Public Overloads Sub GammaRGBToRGBbyLUT(ByVal byGammaRGB() As Byte, ByVal sRGB() As Single, ByVal sLUT(,) As Single)
'		'Inverse gamma correction, byte input. Faster with LUT, but not shared!
'		'Output RGB in [0.0 1.0], input RGB [0 255].
'		sRGB(RGB.red) = sGammaLut(sLUT(byGammaRGB(RGB.red), RGB.red))
'		sRGB(RGB.green) = sGammaLut(sLUT(byGammaRGB(RGB.green), RGB.green))
'		sRGB(RGB.blue) = sGammaLut(sLUT(byGammaRGB(RGB.blue), RGB.blue))
'	End Sub

'	Public Overloads Shared Function ToString(ByVal sColor() As Single) As String
'		Return ToString(sColor, 3)
'	End Function

'	Public Overloads Shared Function ToString(ByVal sColor() As Single, _
'			   ByVal iPrecision As Integer) As String
'		'Transform a 3 element color to a string for screen output
'		Dim strFormat As String, i As Integer

'		If iPrecision > 0 Then
'			strFormat = "##0."
'			For i = 1 To iPrecision
'				strFormat = strFormat & "#"
'			Next i
'		Else
'			strFormat = "#"
'		End If
'		Return sColor(0).ToString(strFormat) & " " & sColor(1).ToString(strFormat) & " " & sColor(2).ToString(strFormat)
'	End Function

'	Public Overloads Shared Function ToString(ByVal byColor() As Byte) As String
'		'Transform a 3 element color to a string for screen output
'		Return byColor(0) & " " & byColor(1) & " " & byColor(2)
'	End Function

'	Public Shared Sub ComputeColorSpaceTransform(ByVal sC1(,) As Single, _
'				ByVal sC2(,) As Single, _
'				ByRef sTransform(,) As Single, _
'				ByVal iTransformType As ColorSpaceTransform)
'		'Compute the polynomial transforms from R3 to R3, both representing LINEAR color spaces.
'		'This is based on a set of N color triplets in sC1, which have to be mapped to N color triplets in sC2.
'		'This means both sC1 and sC2 are N x 3 matrices. 
'		'This also means there are actually 3 transforms to be computed, i.e. one for every color coordinate.
'		'The passed on iTransformType is equal to the number of terms in the transform (3 for linear,etc..), 
'		'and can be termed the order of the transform.
'		'The resulting polynomial transform  is stored in a 3 x M matrix, which can be right multiplied with
'		'a color N-tuple to obtain the desired color tristimulus value.
'		'NOTE: Usually the color transform is used to transform from RGB to sRGB or XYZ
'		Dim iNrColors, i As Integer

'		iNrColors = sC1.GetUpperBound(0) + 1
'		Console.WriteLine("Computing " & iTransformType & _
'		  "-term transform using " & iNrColors & " tristimulus values")

'		'Compute sA using the input color triplets
'		Dim sA(,) As Single
'		ColorTripletToNTuple(sC1, sA, iTransformType)
'		Console.WriteLine("Design matrix is " & vbNewLine & Algebra.ToString(sA))

'		'Solve the 3 sets of linear equations (they are linear in their unknowns, even if they use
'		'coefficient which are non-linear functions of the input color triplets!)
'		'The resulting matrix needs to be transposed to be consistent with the fixed linear transform
'		'matrices already used in the color class (they all use Tf.X = Y, NOT X.Tf = Y)
'		Dim sX(iTransformType - 1, 2) As Single
'		Algebra.Solve(sA, sX, sC2)

'		ReDim sTransform(2, iTransformType - 1)
'		Algebra.Transpose(sX, sTransform)
'	End Sub

'	Public Sub ImproveRGBTosRGBTransform(ByVal sInputColors(,) As Single, _
'			  ByVal sAimColors(,) As Single, _
'			  ByRef sTransform(,) As Single, _
'			  ByVal iMetricPower As Integer)
'		'Improve the RGB to sRGB color space transform using non-linear optimization
'		'IF necessary create other function to improve other types of transforms ....
'		'Because we use delegates we need to set some global variables ...

'		'The transform has 3 x iNrTerms parameters (or dimensions)
'		'as the R,G and B transforms are no longer computed
'		'independently as in the SVD method. The coefficients
'		'must be in a big vector scoef which is filled before each
'		'run
'		Dim iNrColors As Integer = sInputColors.GetUpperBound(0) + 1
'		Dim iNrTerms As Integer = sTransform.GetUpperBound(1) + 1
'		Dim iNrDims = 3 * iNrTerms
'		Dim sCoef(iNrDims - 1) As Single
'		Dim i As Integer, objColor As Color

'		Console.WriteLine("Improving " & iNrTerms & _
'		  "-term transform using " & iNrColors & " tristimulus values")

'		iColorSpaceTransformMetricPower = iMetricPower
'		Console.WriteLine("Using " & iColorSpaceTransformMetricPower & "-metric in CIE Lab space")

'		'Copy the colors. Transform them to CIELab to speed up things
'		ReDim sColorSpaceTransformAimCIELabColors(sAimColors.GetUpperBound(0), sAimColors.GetUpperBound(1))
'		For i = 0 To iNrColors - 1
'			Dim ssRGB(2), sLab(2) As Single
'			Algebra.GetMatrixRow(sAimColors, ssRGB, i)
'			sRGBToCIELab(ssRGB, sLab)
'			Algebra.SetMatrixRow(sColorSpaceTransformAimCIELabColors, sLab, i)
'		Next

'		'Compute the designmatrix using the input color triplets
'		ColorTripletToNTuple(sInputColors, sColorSpaceTransformDesignMatrix, iNrTerms)
'		'Console.WriteLine("Design matrix is " & vbNewLine & Algebra.ToString(sColorSpaceTransformDesignMatrix))

'		'Initialize a simplex using the initial coefficient values
'		'This simplex kind of defines the initial search space, so
'		'this is important. One of the vertices is the SVD solution.
'		'One coefficient of the other vertices is perturbed by
'		'a multiplication by a factor between 0.5 and 1.5
'		Randomize()
'		Dim iNrVertices As Integer = iNrDims + 1
'		Dim sSimplex(iNrVertices - 1, iNrDims - 1) As Single
'		Dim sSimplexVal(iNrVertices - 1) As Single

'		For i = 0 To iNrVertices - 1
'			Algebra.MatrixToVector(sTransform, sCoef)
'			If i > 0 Then
'				sCoef(i - 1) = sCoef(i - 1) * (Rnd() * 5 + 0.5)
'			End If

'			'Put in simplex and compute function value
'			Algebra.SetMatrixRow(sSimplex, sCoef, i)
'			sSimplexVal(i) = RGBTosRGBTransformError(sCoef)

'		Next i

'		'Optimize
'		Console.WriteLine("Current transform: " & Algebra.ToString(sTransform))
'		Console.WriteLine("Error for current transform: " & sSimplexVal(0))
'		Algebra.SolveNonLinear(sSimplex, sSimplexVal, 32768, AddressOf Me.RGBTosRGBTransformError)

'		'Copy result back to transform
'		Algebra.GetMatrixRow(sSimplex, sCoef, 0)
'		Algebra.VectorToMatrix(sCoef, sTransform)

'		Console.WriteLine("New transform: " & Algebra.ToString(sTransform))
'		Console.WriteLine("Error for new transform: " & sSimplexVal(0))
'	End Sub

'	Public Function RGBTosRGBTransformError(ByVal sCoef() As Single) As Single
'		'Return the error to be minimized. Uses global variables!!!!!
'		'Any value for the errormetric above 3 means max-metric!
'		Dim iNrColors As Integer = Me.sColorSpaceTransformAimCIELabColors.GetUpperBound(0) + 1
'		Dim iNrTerms As Integer = Me.sColorSpaceTransformDesignMatrix.GetUpperBound(1) + 1
'		Dim sError As Single

'		Dim i As Integer, j As Integer, k As Integer
'		Dim iCoefNr As Integer
'		Dim ssRGB(2), sLab(2), sAimLab(2) As Single

'		sError = 0
'		For i = 0 To iNrColors - 1
'			iCoefNr = 0
'			For j = 0 To 2
'				sAimLab(j) = Me.sColorSpaceTransformAimCIELabColors(i, j)
'				ssRGB(j) = 0
'				For k = 0 To iNrTerms - 1
'					ssRGB(j) = ssRGB(j) + sCoef(iCoefNr) * Me.sColorSpaceTransformDesignMatrix(i, k)
'					iCoefNr = iCoefNr + 1
'				Next k
'			Next j

'			sRGBToCIELab(ssRGB, sLab)
'			If iColorSpaceTransformMetricPower < 4 Then
'				sError += Math.Pow(CIELabTodE(sAimLab, sLab), iColorSpaceTransformMetricPower)
'			Else
'				sError = Math.Max(sError, CIELabTodE(sAimLab, sLab))
'			End If
'		Next i

'		If iColorSpaceTransformMetricPower < 4 Then
'			sError = Math.Pow(sError / iNrColors, 1 / iColorSpaceTransformMetricPower)
'		End If
'		Return sError
'	End Function

'	Private Overloads Shared Sub ColorTripletToNTuple(ByVal sCList(,) As Single, _
'				 ByRef sCNLList(,) As Single, _
'				 ByVal iNrTerms As Integer)
'		'Computes N-tuples for several color triplets at once.
'		Dim iNrColors As Integer = sCList.GetUpperBound(0) + 1
'		Dim sC(2), sCNL(iNrTerms - 1) As Single, i As Integer
'		ReDim sCNLList(iNrColors - 1, iNrTerms - 1)

'		For i = 0 To iNrColors - 1
'			Algebra.GetMatrixRow(sCList, sC, i)
'			ColorTripletToNTuple(sC, sCNL, iNrTerms)

'			'Copy color N-tuple to sA
'			Algebra.SetMatrixRow(sCNLList, sCNL, i)
'		Next
'	End Sub

'	Private Overloads Shared Sub ColorTripletToNTuple(ByVal sC() As Single, _
'				 ByVal sCNL() As Single, _
'				 ByVal iNrTerms As Integer)
'		'Compute a color N-tuple using higher order and cross terms of the input color triplet.
'		'N depends on the number of elements in sCNL.
'		'This N-tuple can then be multiplied by a transform matrix representing the corresponding 
'		'polynomial transform to obtain a new color triplet.
'		Dim i

'		'All base function sets are at least linear in RGB
'		For i = RGB.red To RGB.blue
'			sCNL(i) = sC(i)
'		Next i

'		Select Case iNrTerms
'			Case ColorSpaceTransform.Linear
'				'Linear transform
'			Case ColorSpaceTransform.NonLinear20Term
'				' 20 terms:
'				' tf = a0 + a1*X + a2*y + a3*z + a4*X*y + a5*y*z + a6*z*X + a7*X^2 + a8*y^2 + a9*z^2 + a10 * X*y*z
'				'      + a11*X^3 + a12*y^3 + a13*z^3 + a14*X*y^2 + a15*X^2*y + a16*y*z^2 + a17*y^2*z + a18*z*X^2
'				'      + a19*z^2*X
'				sCNL(3) = sCNL(0) * sCNL(1)
'				sCNL(4) = sCNL(2) * sCNL(1)
'				sCNL(5) = sCNL(0) * sCNL(2)

'				sCNL(6) = sCNL(0) * sCNL(0)
'				sCNL(7) = sCNL(1) * sCNL(1)
'				sCNL(8) = sCNL(2) * sCNL(2)

'				sCNL(9) = sCNL(0) * sCNL(1) * sCNL(2)
'				sCNL(10) = sCNL(6) * sCNL(0)
'				sCNL(11) = sCNL(7) * sCNL(1)
'				sCNL(12) = sCNL(8) * sCNL(2)

'				sCNL(13) = sCNL(3) * sCNL(1)
'				sCNL(14) = sCNL(3) * sCNL(0)
'				sCNL(15) = sCNL(4) * sCNL(2)
'				sCNL(16) = sCNL(4) * sCNL(1)
'				sCNL(17) = sCNL(5) * sCNL(0)
'				sCNL(18) = sCNL(5) * sCNL(2)
'				sCNL(19) = 1.0#
'			Case ColorSpaceTransform.NonLinear14Term
'				' 14 terms:
'				' tf = a13 + a0*X + a1*y + a2*z + a3*X*y + a4*y*z + a5*z*X +
'				'      a6*X^2 + a7*y^2 + a8*z^2 + a9 * X*y*z
'				'      + a10*X^3 + a11*y^3 + a12*z^3
'				sCNL(3) = sCNL(0) * sCNL(1)
'				sCNL(4) = sCNL(2) * sCNL(1)
'				sCNL(5) = sCNL(0) * sCNL(2)

'				sCNL(6) = sCNL(0) * sCNL(0)
'				sCNL(7) = sCNL(1) * sCNL(1)
'				sCNL(8) = sCNL(2) * sCNL(2)

'				sCNL(9) = sCNL(0) * sCNL(1) * sCNL(2)
'				sCNL(10) = sCNL(6) * sCNL(0)
'				sCNL(11) = sCNL(7) * sCNL(1)
'				sCNL(12) = sCNL(8) * sCNL(2)

'				sCNL(13) = 1.0
'			Case ColorSpaceTransform.NonLinear11Term
'				' 11 terms:
'				' tf = a10 + a0*X + a1*y + a2*z + a3*X*y + a4*y*z + a5*z*X +
'				'      a6*X^2 + a7*y^2 + a8*z^2 + a9*X*y*z
'				sCNL(3) = sCNL(0) * sCNL(1)
'				sCNL(4) = sCNL(2) * sCNL(1)
'				sCNL(5) = sCNL(0) * sCNL(2)

'				sCNL(6) = sCNL(0) * sCNL(0)
'				sCNL(7) = sCNL(1) * sCNL(1)
'				sCNL(8) = sCNL(2) * sCNL(2)

'				sCNL(9) = sCNL(0) * sCNL(1) * sCNL(2)
'				sCNL(10) = 1.0#
'			Case ColorSpaceTransform.NonLinear6Term
'				' 6 terms:
'				' tf = a0*X + a1*y + a2*z + a3*X*y + a4*y*z + a5*z*X
'				sCNL(3) = sCNL(0) * sCNL(1)
'				sCNL(4) = sCNL(2) * sCNL(1)
'				sCNL(5) = sCNL(0) * sCNL(2)
'			Case ColorSpaceTransform.NonLinear8Term
'				' 8 terms:
'				' tf = a7 + a0*X + a1*y + a2*z + a3*X*y +
'				'      a4*y*z + a5*z*X + a6*X*y*z
'				sCNL(3) = sCNL(0) * sCNL(1)
'				sCNL(4) = sCNL(2) * sCNL(1)
'				sCNL(5) = sCNL(0) * sCNL(2)

'				sCNL(6) = sCNL(0) * sCNL(1) * sCNL(2)
'				sCNL(7) = 1.0
'			Case ColorSpaceTransform.NonLinear9Term
'				' 9 terms:
'				' tf = a0*X + a1*y + a2*z + a3*X*y + a4*y*z + a5*z*X +
'				'      a6*X^2 + a7*y^2 + a8*z^2
'				sCNL(3) = sCNL(0) * sCNL(1)
'				sCNL(4) = sCNL(2) * sCNL(1)
'				sCNL(5) = sCNL(0) * sCNL(2)

'				sCNL(6) = sCNL(0) * sCNL(0)
'				sCNL(7) = sCNL(1) * sCNL(1)
'				sCNL(8) = sCNL(2) * sCNL(2)
'			Case Else
'		End Select

'		'Console.WriteLine("Converted color triplet:" & _
'		'  vbNewLine & Algebra.ToString(sC) & _
'		'  "to " & _
'		'  vbNewLine & Algebra.ToString(sCNL))
'	End Sub

'	'------------------- Some functions for use with Windows GDI+ Get and SetPixel ----------------------

'	Public Shared Sub GammaRGBToLUTGamma(ByVal sGammaRGB() As Single, _
'				ByVal sGammaLUTRGB() As Single, _
'				ByVal sLUT(,) As Single)
'		sGammaLUTRGB(RGB.red) = sLUT(CInt(sGammaRGB(0)), RGB.red)
'		sGammaLUTRGB(RGB.green) = sLUT(CInt(sGammaRGB(0)), RGB.green)
'		sGammaLUTRGB(RGB.blue) = sLUT(CInt(sGammaRGB(0)), RGB.blue)
'	End Sub

'	Public Overloads Shared Function GammaRGBToColor(ByVal sGammaRGB() As Single) As System.Drawing.Color
'		'Return a color structure from gamma-corrected RGB
'		Dim objColor As System.Drawing.Color
'		objColor = System.Drawing.Color.FromArgb(CInt(sGammaRGB(RGB.red)), CInt(sGammaRGB(RGB.green)), CInt(sGammaRGB(RGB.blue)))
'	End Function

'	Public Overloads Shared Function GammaRGBToColor(ByVal byGammaRGB() As Byte) As System.Drawing.Color
'		'Return a color structure from gamma-corrected RGB
'		Return System.Drawing.Color.FromArgb(byGammaRGB(RGB.red), byGammaRGB(RGB.green), byGammaRGB(RGB.blue))
'	End Function

'	Public Shared Function RGBToColor(ByVal sRGB() As Single) As System.Drawing.Color
'		'Return a color structure from LINEAR RGB
'		Dim iRGB(2) As Byte
'		RGBToGammaRGB(sRGB, iRGB)
'		Return System.Drawing.Color.FromArgb(iRGB(RGB.red), iRGB(RGB.green), iRGB(RGB.blue))
'	End Function

'	Public Overloads Shared Sub ColorToRGB(ByVal objColor As System.Drawing.Color, ByVal sRGB() As Single)
'		sRGB(RGB.red) = objColor.R
'		sRGB(RGB.green) = objColor.G
'		sRGB(RGB.blue) = objColor.B
'		GammaRGBToRGB(sRGB, sRGB)
'	End Sub

'	Public Overloads Shared Sub ColorToRGB(ByVal objColor As System.Drawing.Color, ByVal sLUT(,) As Single, ByVal sRGB() As Single)
'		sRGB(RGB.red) = sLUT(objColor.R, RGB.red)
'		sRGB(RGB.green) = sLUT(objColor.G, RGB.green)
'		sRGB(RGB.blue) = sLUT(objColor.B, RGB.blue)
'		GammaRGBToRGB(sRGB, sRGB)
'	End Sub

'	Public Overloads Shared Function RGBColorTosRGBColor(ByVal objColor As System.Drawing.Color, ByVal sTransform(,) As Single) As System.Drawing.Color
'		'Conveniece function that tranforms an color struct containing gamma-corrected
'		'RGB values to sRGB values
'		Dim sRGB(2), sSRGB(2) As Single

'		ColorToRGB(objColor, sRGB)
'		TransformColor(sRGB, sSRGB, sTransform)
'		Return RGBToColor(sSRGB)
'	End Function

'	Public Overloads Shared Function RGBColorTosRGBColor(ByVal objColor As System.Drawing.Color, ByVal sTransform(,) As Single, ByVal sLUT(,) As Single) As System.Drawing.Color
'		'Conveniece function that tranforms a color struct containing gamma-corrected
'		'RGB values to sRGB values
'		Dim sRGB(2), sSRGB(2) As Single

'		ColorToRGB(objColor, sLUT, sRGB)
'		TransformColor(sRGB, sSRGB, sTransform)
'		Return RGBToColor(sSRGB)
'	End Function

'	'------------------- Some functions for use with unmanaged code in C# ----------------------
'	'                    These bypass the color structure and workt with bytes

'	Public Overloads Shared Sub GammaRGBToGammasRGB(ByVal byGammaRGB() As Byte, ByVal sLUT(,) As Single, ByVal sTransform(,) As Single)
'		'Conveniece function that tranforms 3 bytes containing gamma-corrected 
'		'RGB values to sRGB values
'		Dim sRGB(2), sSRGB(2) As Single

'		GammaRGBToRGB(byGammaRGB, sRGB, sLUT)
'		TransformColor(sRGB, sSRGB, sTransform)
'		RGBToGammaRGB(sSRGB, byGammaRGB)
'	End Sub

'End Class

#End Region
