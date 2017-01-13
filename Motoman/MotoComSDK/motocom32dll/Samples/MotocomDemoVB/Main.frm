VERSION 5.00
Begin VB.Form Main 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "MotocomDemoVB"
   ClientHeight    =   7500
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   5865
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   ScaleHeight     =   7500
   ScaleWidth      =   5865
   StartUpPosition =   2  'CenterScreen
   Begin VB.Frame Frame2 
      Caption         =   "Host mode Job"
      Height          =   2370
      Left            =   360
      TabIndex        =   20
      Top             =   4440
      Width           =   2340
      Begin VB.CommandButton cmdHost 
         Caption         =   "Upload File"
         Height          =   450
         Index           =   2
         Left            =   300
         TabIndex        =   23
         Top             =   360
         Width           =   1665
      End
      Begin VB.CommandButton cmdHost 
         Caption         =   "Download File"
         Height          =   435
         Index           =   3
         Left            =   315
         TabIndex        =   22
         Top             =   1005
         Width           =   1635
      End
      Begin VB.TextBox Text2 
         Height          =   330
         Left            =   315
         TabIndex        =   21
         Text            =   "TEST.JBI"
         Top             =   1725
         Width           =   1635
      End
   End
   Begin VB.Frame Frame1 
      Caption         =   "DCI mode job"
      Height          =   2370
      Left            =   2955
      TabIndex        =   16
      Top             =   4440
      Width           =   2490
      Begin VB.CommandButton cmdDCI 
         Caption         =   "Wait for robot"
         Height          =   420
         Index           =   2
         Left            =   450
         TabIndex        =   19
         Top             =   375
         Width           =   1635
      End
      Begin VB.CommandButton cmdDCI 
         Caption         =   "Stop"
         Enabled         =   0   'False
         Height          =   420
         Index           =   3
         Left            =   480
         TabIndex        =   18
         Top             =   1020
         Width           =   1635
      End
      Begin VB.TextBox Text1 
         Height          =   330
         Left            =   465
         TabIndex        =   17
         Text            =   "0"
         Top             =   1740
         Width           =   1635
      End
   End
   Begin VB.CommandButton cmdExit 
      Caption         =   "Exit"
      Height          =   360
      Left            =   2955
      TabIndex        =   3
      Top             =   6960
      Width           =   2490
   End
   Begin VB.Frame fraDCI 
      Caption         =   "DCI mode variable"
      Height          =   2370
      Left            =   2955
      TabIndex        =   2
      Top             =   1875
      Width           =   2490
      Begin VB.TextBox txtDCI 
         Height          =   330
         Left            =   465
         TabIndex        =   9
         Text            =   "0"
         Top             =   1740
         Width           =   1635
      End
      Begin VB.CommandButton cmdDCI 
         Caption         =   "SETPOS B-VAR"
         Height          =   420
         Index           =   1
         Left            =   465
         TabIndex        =   7
         Top             =   1020
         Width           =   1635
      End
      Begin VB.CommandButton cmdDCI 
         Caption         =   "GETPOS B-VAR"
         Height          =   420
         Index           =   0
         Left            =   450
         TabIndex        =   6
         Top             =   375
         Width           =   1635
      End
   End
   Begin VB.Frame fraHost 
      Caption         =   "Host mode Variable"
      Height          =   2370
      Left            =   345
      TabIndex        =   1
      Top             =   1890
      Width           =   2340
      Begin VB.TextBox txtHost 
         Height          =   330
         Left            =   315
         TabIndex        =   8
         Text            =   "0"
         Top             =   1725
         Width           =   1635
      End
      Begin VB.CommandButton cmdHost 
         Caption         =   "Write B000"
         Height          =   435
         Index           =   1
         Left            =   315
         TabIndex        =   5
         Top             =   1005
         Width           =   1635
      End
      Begin VB.CommandButton cmdHost 
         Caption         =   "Read B000"
         Height          =   450
         Index           =   0
         Left            =   300
         TabIndex        =   4
         Top             =   360
         Width           =   1665
      End
   End
   Begin VB.Frame fraConnection 
      Caption         =   "Connection"
      Height          =   1572
      Left            =   345
      TabIndex        =   0
      Top             =   120
      Width           =   5130
      Begin VB.Frame fraEthernetMode 
         BorderStyle     =   0  'None
         Caption         =   "Frame4"
         Height          =   492
         Left            =   1800
         TabIndex        =   24
         Top             =   960
         Width           =   2772
         Begin VB.OptionButton optMode 
            Caption         =   "EServer"
            Height          =   252
            Index           =   0
            Left            =   240
            TabIndex        =   26
            Top             =   0
            Width           =   972
         End
         Begin VB.OptionButton optMode 
            Caption         =   "BSC"
            Height          =   252
            Index           =   1
            Left            =   1440
            TabIndex        =   25
            Top             =   0
            Value           =   -1  'True
            Width           =   972
         End
      End
      Begin VB.TextBox txtIP 
         Height          =   315
         Left            =   3270
         TabIndex        =   15
         Text            =   "192.168.10.1"
         Top             =   540
         Width           =   1260
      End
      Begin VB.TextBox txtPort 
         Height          =   285
         Left            =   3270
         TabIndex        =   14
         Text            =   "1"
         Top             =   168
         Width           =   735
      End
      Begin VB.OptionButton optConnect 
         Caption         =   "Ethernet"
         Height          =   315
         Index           =   1
         Left            =   315
         TabIndex        =   11
         Top             =   540
         Width           =   1350
      End
      Begin VB.OptionButton optConnect 
         Caption         =   "Serial"
         Height          =   315
         Index           =   0
         Left            =   315
         TabIndex        =   10
         Top             =   180
         Width           =   1350
      End
      Begin VB.Label lblConnect 
         Caption         =   "IP adress"
         Height          =   300
         Index           =   1
         Left            =   1980
         TabIndex        =   13
         Top             =   588
         Width           =   972
      End
      Begin VB.Label lblConnect 
         Caption         =   "Com-Port"
         Height          =   300
         Index           =   0
         Left            =   1992
         TabIndex        =   12
         Top             =   216
         Width           =   780
      End
   End
End
Attribute VB_Name = "Main"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private m_nCid As Integer
Private bDCI2 As Boolean

Private Sub cmdDCI_Click(Index As Integer)
    Select Case Index
        Case 0
            DoDCI 0
        Case 1
            DoDCI 1
        Case 2
            DoDCI 2
        Case 3
            DoDCI 3
            
    End Select
End Sub

Private Sub cmdExit_Click()
    Unload Me
End Sub

Private Sub cmdHost_Click(Index As Integer)
    Select Case Index
        Case 0
            DoHost 0
        Case 1
            DoHost 1
        Case 2
            DoHost 2
        Case 3
            DoHost 3
    End Select
End Sub



Private Sub Form_Load()
    optConnect(1).Value = True
End Sub



Private Sub optConnect_Click(Index As Integer)
    Select Case Index
        Case 0
            txtPort.Enabled = True
            txtIP.Enabled = False
            optMode(0).Enabled = False
            optMode(1).Enabled = False
        Case 1
            txtPort.Enabled = False
            txtIP.Enabled = True
            optMode(0).Enabled = True
            optMode(1).Enabled = True
    End Select
End Sub


Private Function DoHost(function_id As Integer)
    Dim ret As Long
    Dim path As String
    Dim open_mode As Integer

    Dim strPort As String
    Dim port As Integer
    Dim baud As Long
    Dim parity As Integer
    Dim clen As Integer
    Dim stp As Integer

    Dim strIPaddr As String
    Dim ether_mode As Integer
    Dim hWnd As Long

    Dim type_ As Integer
    Dim varno As Integer
    Dim vardata(0 To 10) As Double
    Dim strVal As String
    
    '******************************************************************************************
    '***************** Connect to Yasnac *********************************************************
    If optConnect(0).Value = True Then
        'serial communication
        path = App.path                                     'working directory
        open_mode = 1                                       'serial mode

        'step 1: get a hardware key handle
        m_nCid = BscOpen(path, open_mode)

        If m_nCid >= 0 Then
                     
            port = CInt(txtPort.Text)                      'read value from user interface
            baud = 9600                                    '9600 baud
            parity = 2                                     'even parity
            clen = 8                                       '8 data bits
            stp = 0                                        '1 stop bit
            
            'step 2: setup com port
            ret = BscSetCom(m_nCid, port, baud, parity, clen, stp)
            
            If ret = 1 Then
                'step 3: Establish a connection
                ret = BscConnect(m_nCid)
                If ret = 1 Then
                    '...
                Else
                    ret = BscClose(m_nCid)
                    m_nCid = -1
                    MsgBox ("Error establish connection !")
                End If
            Else
                ret = BscClose(m_nCid)
                m_nCid = -1
                MsgBox ("Error setting up com port !")
            End If
        Else
            MsgBox ("Hardware Key Error !")
        End If
    Else
        'ethernet communication
        path = App.path                                    'working directory
        If optMode(0).Value = True Then
            open_mode = 256                                     'ethernet E-Server mode
        Else
            open_mode = 16                                     'ethernet BSC mode
        End If
        
        'step 1: get a hardware key handle
        m_nCid = BscOpen(path, open_mode)

        If m_nCid >= 0 Then
            strIPaddr = txtIP.Text                         'read value from user interface
            ether_mode = 0                                 'for host function client-mode is neccessary
            hWnd = Me.hWnd                                 'handle of dialog window

            'step 2: setup ethernet
            If optMode(0).Value = True Then
                ret = BscSetEServer(m_nCid, strIPaddr)
            Else
                ret = BscSetEther(m_nCid, strIPaddr, ether_mode, hWnd)
            End If

            If ret = 1 Then
                'step 3: Establish a connection
                ret = BscConnect(m_nCid)
                If ret = 1 Then
                    '...
                Else
                    ret = BscClose(m_nCid)
                    m_nCid = -1
                    MsgBox ("Error establish connection !")
                End If
            Else
                ret = BscClose(m_nCid)
                m_nCid = -1
                MsgBox ("Error setting up ethernet !")
            End If
        Else
            MsgBox ("Hardware Key Error !")
        End If
    End If
    
    
    '******************************************************************************************
    '***************** Transmit Data **********************************************************
    'step 4: access robot control
    If m_nCid >= 0 Then
        If function_id = 0 Then
            'read value of B000 variable
            type_ = 0                                           'Byte variable
            varno = 0                                           'No. 0 / B000
            ret = BscGetVarData(m_nCid, type_, varno, vardata(0))
            If ret = 0 Then
                'adjust user interface
                txtHost.Text = CStr(vardata(0))
                MsgBox ("Ok, variable was received from robot !")
            Else
                MsgBox ("Error reading variable !")
            End If
        ElseIf function_id = 1 Then
            'write value of B000 variable
            type_ = 0                                           'Byte variable
            varno = 0                                           'No. 0 / B000
            vardata(0) = CDbl(txtHost.Text)                     'read value from user interface
            ret = BscPutVarData(m_nCid, type_, varno, vardata(0))
            If ret = 0 Then
                MsgBox ("Ok, variable was sent to robot !")
            Else
                MsgBox ("Error writing variable !")
            End If
         ElseIf function_id = 2 Then
            'get file from robot
            ret = BscUpLoad(m_nCid, UCase$(CStr(Text2.Text)))
            If ret = 0 Then
                MsgBox ("Ok, file was uploaded from robot !")
            Else
                MsgBox ("Error uploading file !")
            End If
        ElseIf function_id = 3 Then
            'transfer file to robot
            ret = BscDownLoad(m_nCid, UCase$(CStr(Text2.Text)))
            If ret = 0 Then
                MsgBox ("Ok, file was downloaded to robot !")
            Else
                MsgBox ("Error downloading file !")
            End If
        End If
        
        'step 5: if work is done disconnect
        ret = BscDisConnect(m_nCid)
        'step 6: free handle
        ret = BscClose(m_nCid)
    End If
End Function

Private Function DoDCI(function_id As Integer)
    Dim ret As Long
    Dim path As String
    Dim open_mode As Integer

    Dim strPort As String
    Dim port As Integer
    Dim baud As Long
    Dim parity As Integer
    Dim clen As Integer
    Dim stp As Integer

    Dim strIPaddr As String
    Dim ether_mode As Integer
    Dim hWnd As Long

    Dim type_ As Integer
    Dim rconf As Integer
    Dim vardata(0 To 10) As Double
    Dim strVal As String
    
    If function_id = 3 Then
        bDCI2 = False
        Exit Function
    End If


    '******************************************************************************************
    '***************** Connect to Yasnac *********************************************************
    If optConnect(0).Value = True Then
    'serial communication
        path = App.path                                    'working directory
        open_mode = 1                                      'serial mode

        'step 1: get a hardware key handle
        m_nCid = BscOpen(path, open_mode)

        If m_nCid >= 0 Then
            port = CInt(txtPort.Text)                      'read value from user interface
            baud = 9600                                    '9600 baud
            parity = 2                                     'even parity
            clen = 8                                       '8 data bits
            stp = 0                                        '1 stop bit
            
            'step 2: setup com port
            ret = BscSetCom(m_nCid, port, baud, parity, clen, stp)
            
            If ret = 1 Then
                'step 3: Establish a connection
                ret = BscConnect(m_nCid)
                If ret = 1 Then
                    '...
                Else
                    ret = BscClose(m_nCid)
                    m_nCid = -1
                    MsgBox ("Error establish connection !")
                End If
            Else
                ret = BscClose(m_nCid)
                m_nCid = -1
                MsgBox ("Error setting up com port !")
            End If
        Else
            MsgBox ("Hardware Key Error !")
        End If
    Else
        'ethernet communication
        path = App.path                                    'working directory
        open_mode = 16                                     'ethernet mode
        
        'step 1: get a hardware key handle
        m_nCid = BscOpen(path, open_mode)

        If m_nCid >= 0 Then
            strIPaddr = txtIP.Text                         'read value from user interface
            ether_mode = 1                                 'for dci function server-mode is neccessary
            hWnd = Me.hWnd                                 'handle of dialog window

            'step 2: setup ethernet
            ret = BscSetEther(m_nCid, strIPaddr, ether_mode, hWnd)

            If ret = 1 Then
                'step 3: Establish a connection
                ret = BscConnect(m_nCid)
                If ret = 1 Then
                    '...
                Else
                    ret = BscClose(m_nCid)
                    m_nCid = -1
                    MsgBox ("Error establish connection !")
                End If
            Else
                ret = BscClose(m_nCid)
                m_nCid = -1
                MsgBox ("Error setting up ethernet !")
            End If
        Else
            MsgBox ("Hardware Key Error !")
        End If
    End If


    '******************************************************************************************
    '***************** Transmit Data **********************************************************
    'step 4: access robot control
    If m_nCid >= 0 Then
        Me.MousePointer = vbHourglass
        If function_id = 0 Then
            'get Variable, SAVEV on robot side
            'corresponding job on controller is
            'NOP
            'SAVE B000
            'END
            cmdDCI(0).Enabled = False
            ret = BscDCIGetPos(m_nCid, type_, rconf, vardata(0))
            cmdDCI(0).Enabled = True
            Me.MousePointer = vbDefault
            If ret <> -1 Then
                'adjust user interface
                txtDCI.Text = CStr(vardata(0))                  'display value
                MsgBox ("Ok, variable was received from robot by DCI !")
            Else
                MsgBox ("Error reading variable !")
            End If
        ElseIf function_id = 1 Then
            'put B-variable, LOADV on robot side
            'corresponding job on controller is
            'NOP
            'LOADV B000
            'END
            type_ = 1                                           'Byte variable
            rconf = 0                                           'Form data
            vardata(0) = CDbl(txtDCI.Text)                      'read value from user interface
            cmdDCI(1).Enabled = False
            ret = BscDCIPutPos(m_nCid, type_, rconf, vardata(0))
            cmdDCI(1).Enabled = True
            Me.MousePointer = vbDefault
            If ret = 0 Then
                MsgBox ("Ok, variable was sent to robot by DCI !")
            Else
                MsgBox ("Error writing variable !")
            End If
        ElseIf function_id = 2 Then
            'job loading with LOADJ, triggered by SAVEV
            'corresponding job on controller is
            'NOP
            'SAVEV B000
            'LOADJ JOB:TEST JBI
            'End
            cmdDCI(2).Enabled = False
            cmdDCI(3).Enabled = True
            
            bDCI2 = True
            Do
                'first get variable by SAVEV instruction on robot side
                ret = BscDCIGetPos(m_nCid, type_, rconf, vardata(0))
                DoEvents
            Loop Until ret <> -1 Or bDCI2 = False
            Me.MousePointer = vbDefault
            If bDCI2 <> False Then
                'second send requested job
                ret = BscDCILoadSaveOnce(m_nCid)
                If ret <> -1 Then
                    MsgBox ("Ok, job was sent to robot by DCI !")
                Else
                    MsgBox ("Error sending job !")
                End If
            End If
            
            cmdDCI(3).Enabled = False
            cmdDCI(2).Enabled = True
        End If
        
        'step 5: if work is done disconnect
        ret = BscDisConnect(m_nCid)
        'step 6: free handle
        ret = BscClose(m_nCid)
    End If
End Function




