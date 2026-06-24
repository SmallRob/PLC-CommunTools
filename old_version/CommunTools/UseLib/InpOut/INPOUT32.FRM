VERSION 4.00
Begin VB.Form inpout32 
   Caption         =   "Form1"
   ClientHeight    =   1692
   ClientLeft      =   912
   ClientTop       =   1404
   ClientWidth     =   2400
   Height          =   2076
   Left            =   864
   LinkTopic       =   "Form1"
   ScaleHeight     =   1692
   ScaleWidth      =   2400
   Top             =   1068
   Width           =   2496
   Begin VB.TextBox Text1 
      Height          =   372
      Left            =   120
      TabIndex        =   1
      Text            =   "Text1"
      Top             =   1080
      Width           =   2052
   End
   Begin VB.CommandButton cmdWriteToPort 
      Caption         =   "Write to Port"
      Height          =   732
      Left            =   240
      TabIndex        =   0
      Top             =   120
      Width           =   1932
   End
End
Attribute VB_Name = "inpout32"
Attribute VB_Creatable = False
Attribute VB_Exposed = False
Option Explicit
Dim Value As Integer
Dim PortAddress As Integer
Private Sub cmdWriteToPort_Click()
'Write to a port.
Out PortAddress, Value
'Read back and display the result.
Text1.Text = Inp(PortAddress)
Value = Value + 1
If Value = 255 Then Value = 0
End Sub
Private Sub Form_Load()
'Test program for inpout32.dll
Value = 0
'Change PortAddress to match the port address to write to:
'(Usual parallel-port addresses are &h378, &h278, &h3BC)
PortAddress = &H378
End Sub
