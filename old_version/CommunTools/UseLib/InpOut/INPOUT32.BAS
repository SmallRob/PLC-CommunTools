Attribute VB_Name = "inpout"

'Inp and Out declarations for direct port I/O 
'in 32-bit Visual Basic 4 programs.

Public Declare Function Inp Lib "inpout32.dll" _
Alias "Inp32" (ByVal PortAddress As Integer) As Integer
Public Declare Sub Out Lib "inpout32.dll" _
Alias "Out32" (ByVal PortAddress As Integer, ByVal Value As Integer)
