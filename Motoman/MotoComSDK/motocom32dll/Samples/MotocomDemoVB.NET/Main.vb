Option Strict Off
Option Explicit On
Friend Class Main_Renamed
	Inherits System.Windows.Forms.Form
#Region "Windows Form Designer generated code "
	Public Sub New()
		MyBase.New()
		If m_vb6FormDefInstance Is Nothing Then
			If m_InitializingDefInstance Then
				m_vb6FormDefInstance = Me
			Else
				Try 
					'For the start-up form, the first instance created is the default instance.
					If System.Reflection.Assembly.GetExecutingAssembly.EntryPoint.DeclaringType Is Me.GetType Then
						m_vb6FormDefInstance = Me
					End If
				Catch
				End Try
			End If
		End If
		'This call is required by the Windows Form Designer.
		InitializeComponent()
	End Sub
	'Form overrides dispose to clean up the component list.
	Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer
	Public ToolTip1 As System.Windows.Forms.ToolTip
	Public WithEvents _cmdHost_2 As System.Windows.Forms.Button
	Public WithEvents _cmdHost_3 As System.Windows.Forms.Button
	Public WithEvents Text2 As System.Windows.Forms.TextBox
	Public WithEvents Frame2 As System.Windows.Forms.GroupBox
	Public WithEvents _cmdDCI_2 As System.Windows.Forms.Button
	Public WithEvents _cmdDCI_3 As System.Windows.Forms.Button
	Public WithEvents Text1 As System.Windows.Forms.TextBox
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	Public WithEvents cmdExit As System.Windows.Forms.Button
	Public WithEvents txtDCI As System.Windows.Forms.TextBox
	Public WithEvents _cmdDCI_1 As System.Windows.Forms.Button
	Public WithEvents _cmdDCI_0 As System.Windows.Forms.Button
	Public WithEvents fraDCI As System.Windows.Forms.GroupBox
	Public WithEvents txtHost As System.Windows.Forms.TextBox
	Public WithEvents _cmdHost_1 As System.Windows.Forms.Button
	Public WithEvents _cmdHost_0 As System.Windows.Forms.Button
	Public WithEvents fraHost As System.Windows.Forms.GroupBox
	Public WithEvents _optMode_0 As System.Windows.Forms.RadioButton
	Public WithEvents _optMode_1 As System.Windows.Forms.RadioButton
	Public WithEvents fraEthernetMode As System.Windows.Forms.Panel
	Public WithEvents txtIP As System.Windows.Forms.TextBox
	Public WithEvents txtPort As System.Windows.Forms.TextBox
	Public WithEvents _optConnect_1 As System.Windows.Forms.RadioButton
	Public WithEvents _optConnect_0 As System.Windows.Forms.RadioButton
	Public WithEvents _lblConnect_1 As System.Windows.Forms.Label
	Public WithEvents _lblConnect_0 As System.Windows.Forms.Label
	Public WithEvents fraConnection As System.Windows.Forms.GroupBox
	Public WithEvents cmdDCI As Microsoft.VisualBasic.Compatibility.VB6.ButtonArray
	Public WithEvents cmdHost As Microsoft.VisualBasic.Compatibility.VB6.ButtonArray
	Public WithEvents lblConnect As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
	Public WithEvents optConnect As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	Public WithEvents optMode As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Frame2 = New System.Windows.Forms.GroupBox
        Me._cmdHost_2 = New System.Windows.Forms.Button
        Me._cmdHost_3 = New System.Windows.Forms.Button
        Me.Text2 = New System.Windows.Forms.TextBox
        Me.Frame1 = New System.Windows.Forms.GroupBox
        Me._cmdDCI_2 = New System.Windows.Forms.Button
        Me._cmdDCI_3 = New System.Windows.Forms.Button
        Me.Text1 = New System.Windows.Forms.TextBox
        Me.cmdExit = New System.Windows.Forms.Button
        Me.fraDCI = New System.Windows.Forms.GroupBox
        Me.txtDCI = New System.Windows.Forms.TextBox
        Me._cmdDCI_1 = New System.Windows.Forms.Button
        Me._cmdDCI_0 = New System.Windows.Forms.Button
        Me.fraHost = New System.Windows.Forms.GroupBox
        Me.txtHost = New System.Windows.Forms.TextBox
        Me._cmdHost_1 = New System.Windows.Forms.Button
        Me._cmdHost_0 = New System.Windows.Forms.Button
        Me.fraConnection = New System.Windows.Forms.GroupBox
        Me.fraEthernetMode = New System.Windows.Forms.Panel
        Me._optMode_0 = New System.Windows.Forms.RadioButton
        Me._optMode_1 = New System.Windows.Forms.RadioButton
        Me.txtIP = New System.Windows.Forms.TextBox
        Me.txtPort = New System.Windows.Forms.TextBox
        Me._optConnect_1 = New System.Windows.Forms.RadioButton
        Me._optConnect_0 = New System.Windows.Forms.RadioButton
        Me._lblConnect_1 = New System.Windows.Forms.Label
        Me._lblConnect_0 = New System.Windows.Forms.Label
        Me.cmdDCI = New Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(Me.components)
        Me.cmdHost = New Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(Me.components)
        Me.lblConnect = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.optConnect = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optMode = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.fraDCI.SuspendLayout()
        Me.fraHost.SuspendLayout()
        Me.fraConnection.SuspendLayout()
        Me.fraEthernetMode.SuspendLayout()
        CType(Me.cmdDCI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdHost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblConnect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optConnect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optMode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me._cmdHost_2)
        Me.Frame2.Controls.Add(Me._cmdHost_3)
        Me.Frame2.Controls.Add(Me.Text2)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(24, 296)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(156, 158)
        Me.Frame2.TabIndex = 20
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Host mode Job"
        '
        '_cmdHost_2
        '
        Me._cmdHost_2.BackColor = System.Drawing.SystemColors.Control
        Me._cmdHost_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdHost_2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cmdHost_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdHost.SetIndex(Me._cmdHost_2, CType(2, Short))
        Me._cmdHost_2.Location = New System.Drawing.Point(20, 24)
        Me._cmdHost_2.Name = "_cmdHost_2"
        Me._cmdHost_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdHost_2.Size = New System.Drawing.Size(111, 30)
        Me._cmdHost_2.TabIndex = 23
        Me._cmdHost_2.Text = "Upload File"
        Me._cmdHost_2.UseVisualStyleBackColor = False
        '
        '_cmdHost_3
        '
        Me._cmdHost_3.BackColor = System.Drawing.SystemColors.Control
        Me._cmdHost_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdHost_3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cmdHost_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdHost.SetIndex(Me._cmdHost_3, CType(3, Short))
        Me._cmdHost_3.Location = New System.Drawing.Point(21, 67)
        Me._cmdHost_3.Name = "_cmdHost_3"
        Me._cmdHost_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdHost_3.Size = New System.Drawing.Size(109, 29)
        Me._cmdHost_3.TabIndex = 22
        Me._cmdHost_3.Text = "Download File"
        Me._cmdHost_3.UseVisualStyleBackColor = False
        '
        'Text2
        '
        Me.Text2.AcceptsReturn = True
        Me.Text2.BackColor = System.Drawing.SystemColors.Window
        Me.Text2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.Text2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Text2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Text2.Location = New System.Drawing.Point(21, 115)
        Me.Text2.MaxLength = 0
        Me.Text2.Name = "Text2"
        Me.Text2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text2.Size = New System.Drawing.Size(109, 22)
        Me.Text2.TabIndex = 21
        Me.Text2.Text = "TEST.JBI"
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._cmdDCI_2)
        Me.Frame1.Controls.Add(Me._cmdDCI_3)
        Me.Frame1.Controls.Add(Me.Text1)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(197, 296)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(166, 158)
        Me.Frame1.TabIndex = 16
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "DCI mode job"
        '
        '_cmdDCI_2
        '
        Me._cmdDCI_2.BackColor = System.Drawing.SystemColors.Control
        Me._cmdDCI_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdDCI_2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cmdDCI_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDCI.SetIndex(Me._cmdDCI_2, CType(2, Short))
        Me._cmdDCI_2.Location = New System.Drawing.Point(30, 25)
        Me._cmdDCI_2.Name = "_cmdDCI_2"
        Me._cmdDCI_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdDCI_2.Size = New System.Drawing.Size(109, 28)
        Me._cmdDCI_2.TabIndex = 19
        Me._cmdDCI_2.Text = "Wait for robot"
        Me._cmdDCI_2.UseVisualStyleBackColor = False
        '
        '_cmdDCI_3
        '
        Me._cmdDCI_3.BackColor = System.Drawing.SystemColors.Control
        Me._cmdDCI_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdDCI_3.Enabled = False
        Me._cmdDCI_3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cmdDCI_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDCI.SetIndex(Me._cmdDCI_3, CType(3, Short))
        Me._cmdDCI_3.Location = New System.Drawing.Point(32, 68)
        Me._cmdDCI_3.Name = "_cmdDCI_3"
        Me._cmdDCI_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdDCI_3.Size = New System.Drawing.Size(109, 28)
        Me._cmdDCI_3.TabIndex = 18
        Me._cmdDCI_3.Text = "Stop"
        Me._cmdDCI_3.UseVisualStyleBackColor = False
        '
        'Text1
        '
        Me.Text1.AcceptsReturn = True
        Me.Text1.BackColor = System.Drawing.SystemColors.Window
        Me.Text1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.Text1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Text1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Text1.Location = New System.Drawing.Point(31, 116)
        Me.Text1.MaxLength = 0
        Me.Text1.Name = "Text1"
        Me.Text1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text1.Size = New System.Drawing.Size(109, 22)
        Me.Text1.TabIndex = 17
        Me.Text1.Text = "0"
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(197, 464)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(166, 24)
        Me.cmdExit.TabIndex = 3
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = False
        '
        'fraDCI
        '
        Me.fraDCI.BackColor = System.Drawing.SystemColors.Control
        Me.fraDCI.Controls.Add(Me.txtDCI)
        Me.fraDCI.Controls.Add(Me._cmdDCI_1)
        Me.fraDCI.Controls.Add(Me._cmdDCI_0)
        Me.fraDCI.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraDCI.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraDCI.Location = New System.Drawing.Point(197, 125)
        Me.fraDCI.Name = "fraDCI"
        Me.fraDCI.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraDCI.Size = New System.Drawing.Size(166, 158)
        Me.fraDCI.TabIndex = 2
        Me.fraDCI.TabStop = False
        Me.fraDCI.Text = "DCI mode variable"
        '
        'txtDCI
        '
        Me.txtDCI.AcceptsReturn = True
        Me.txtDCI.BackColor = System.Drawing.SystemColors.Window
        Me.txtDCI.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDCI.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDCI.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDCI.Location = New System.Drawing.Point(31, 116)
        Me.txtDCI.MaxLength = 0
        Me.txtDCI.Name = "txtDCI"
        Me.txtDCI.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDCI.Size = New System.Drawing.Size(109, 22)
        Me.txtDCI.TabIndex = 9
        Me.txtDCI.Text = "0"
        '
        '_cmdDCI_1
        '
        Me._cmdDCI_1.BackColor = System.Drawing.SystemColors.Control
        Me._cmdDCI_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdDCI_1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cmdDCI_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDCI.SetIndex(Me._cmdDCI_1, CType(1, Short))
        Me._cmdDCI_1.Location = New System.Drawing.Point(31, 68)
        Me._cmdDCI_1.Name = "_cmdDCI_1"
        Me._cmdDCI_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdDCI_1.Size = New System.Drawing.Size(109, 28)
        Me._cmdDCI_1.TabIndex = 7
        Me._cmdDCI_1.Text = "SETPOS B-VAR"
        Me._cmdDCI_1.UseVisualStyleBackColor = False
        '
        '_cmdDCI_0
        '
        Me._cmdDCI_0.BackColor = System.Drawing.SystemColors.Control
        Me._cmdDCI_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdDCI_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cmdDCI_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDCI.SetIndex(Me._cmdDCI_0, CType(0, Short))
        Me._cmdDCI_0.Location = New System.Drawing.Point(30, 25)
        Me._cmdDCI_0.Name = "_cmdDCI_0"
        Me._cmdDCI_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdDCI_0.Size = New System.Drawing.Size(109, 28)
        Me._cmdDCI_0.TabIndex = 6
        Me._cmdDCI_0.Text = "GETPOS B-VAR"
        Me._cmdDCI_0.UseVisualStyleBackColor = False
        '
        'fraHost
        '
        Me.fraHost.BackColor = System.Drawing.SystemColors.Control
        Me.fraHost.Controls.Add(Me.txtHost)
        Me.fraHost.Controls.Add(Me._cmdHost_1)
        Me.fraHost.Controls.Add(Me._cmdHost_0)
        Me.fraHost.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraHost.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraHost.Location = New System.Drawing.Point(23, 126)
        Me.fraHost.Name = "fraHost"
        Me.fraHost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraHost.Size = New System.Drawing.Size(156, 158)
        Me.fraHost.TabIndex = 1
        Me.fraHost.TabStop = False
        Me.fraHost.Text = "Host mode Variable"
        '
        'txtHost
        '
        Me.txtHost.AcceptsReturn = True
        Me.txtHost.BackColor = System.Drawing.SystemColors.Window
        Me.txtHost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtHost.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHost.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtHost.Location = New System.Drawing.Point(21, 115)
        Me.txtHost.MaxLength = 0
        Me.txtHost.Name = "txtHost"
        Me.txtHost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtHost.Size = New System.Drawing.Size(109, 22)
        Me.txtHost.TabIndex = 8
        Me.txtHost.Text = "0"
        '
        '_cmdHost_1
        '
        Me._cmdHost_1.BackColor = System.Drawing.SystemColors.Control
        Me._cmdHost_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdHost_1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cmdHost_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdHost.SetIndex(Me._cmdHost_1, CType(1, Short))
        Me._cmdHost_1.Location = New System.Drawing.Point(21, 67)
        Me._cmdHost_1.Name = "_cmdHost_1"
        Me._cmdHost_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdHost_1.Size = New System.Drawing.Size(109, 29)
        Me._cmdHost_1.TabIndex = 5
        Me._cmdHost_1.Text = "Write B000"
        Me._cmdHost_1.UseVisualStyleBackColor = False
        '
        '_cmdHost_0
        '
        Me._cmdHost_0.BackColor = System.Drawing.SystemColors.Control
        Me._cmdHost_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdHost_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cmdHost_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdHost.SetIndex(Me._cmdHost_0, CType(0, Short))
        Me._cmdHost_0.Location = New System.Drawing.Point(20, 24)
        Me._cmdHost_0.Name = "_cmdHost_0"
        Me._cmdHost_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdHost_0.Size = New System.Drawing.Size(111, 30)
        Me._cmdHost_0.TabIndex = 4
        Me._cmdHost_0.Text = "Read B000"
        Me._cmdHost_0.UseVisualStyleBackColor = False
        '
        'fraConnection
        '
        Me.fraConnection.BackColor = System.Drawing.SystemColors.Control
        Me.fraConnection.Controls.Add(Me.fraEthernetMode)
        Me.fraConnection.Controls.Add(Me.txtIP)
        Me.fraConnection.Controls.Add(Me.txtPort)
        Me.fraConnection.Controls.Add(Me._optConnect_1)
        Me.fraConnection.Controls.Add(Me._optConnect_0)
        Me.fraConnection.Controls.Add(Me._lblConnect_1)
        Me.fraConnection.Controls.Add(Me._lblConnect_0)
        Me.fraConnection.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraConnection.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraConnection.Location = New System.Drawing.Point(23, 8)
        Me.fraConnection.Name = "fraConnection"
        Me.fraConnection.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraConnection.Size = New System.Drawing.Size(342, 105)
        Me.fraConnection.TabIndex = 0
        Me.fraConnection.TabStop = False
        Me.fraConnection.Text = "Connection"
        '
        'fraEthernetMode
        '
        Me.fraEthernetMode.BackColor = System.Drawing.SystemColors.Control
        Me.fraEthernetMode.Controls.Add(Me._optMode_0)
        Me.fraEthernetMode.Controls.Add(Me._optMode_1)
        Me.fraEthernetMode.Cursor = System.Windows.Forms.Cursors.Default
        Me.fraEthernetMode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraEthernetMode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraEthernetMode.Location = New System.Drawing.Point(120, 64)
        Me.fraEthernetMode.Name = "fraEthernetMode"
        Me.fraEthernetMode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraEthernetMode.Size = New System.Drawing.Size(185, 33)
        Me.fraEthernetMode.TabIndex = 24
        Me.fraEthernetMode.Text = "Frame4"
        '
        '_optMode_0
        '
        Me._optMode_0.BackColor = System.Drawing.SystemColors.Control
        Me._optMode_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optMode_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optMode_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optMode.SetIndex(Me._optMode_0, CType(0, Short))
        Me._optMode_0.Location = New System.Drawing.Point(16, 0)
        Me._optMode_0.Name = "_optMode_0"
        Me._optMode_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optMode_0.Size = New System.Drawing.Size(65, 17)
        Me._optMode_0.TabIndex = 26
        Me._optMode_0.TabStop = True
        Me._optMode_0.Text = "EServer"
        Me._optMode_0.UseVisualStyleBackColor = False
        '
        '_optMode_1
        '
        Me._optMode_1.BackColor = System.Drawing.SystemColors.Control
        Me._optMode_1.Checked = True
        Me._optMode_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optMode_1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optMode_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optMode.SetIndex(Me._optMode_1, CType(1, Short))
        Me._optMode_1.Location = New System.Drawing.Point(96, 0)
        Me._optMode_1.Name = "_optMode_1"
        Me._optMode_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optMode_1.Size = New System.Drawing.Size(65, 17)
        Me._optMode_1.TabIndex = 25
        Me._optMode_1.TabStop = True
        Me._optMode_1.Text = "BSC"
        Me._optMode_1.UseVisualStyleBackColor = False
        '
        'txtIP
        '
        Me.txtIP.AcceptsReturn = True
        Me.txtIP.BackColor = System.Drawing.SystemColors.Window
        Me.txtIP.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIP.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIP.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIP.Location = New System.Drawing.Point(218, 36)
        Me.txtIP.MaxLength = 0
        Me.txtIP.Name = "txtIP"
        Me.txtIP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIP.Size = New System.Drawing.Size(84, 21)
        Me.txtIP.TabIndex = 15
        Me.txtIP.Text = "192.168.10.1"
        '
        'txtPort
        '
        Me.txtPort.AcceptsReturn = True
        Me.txtPort.BackColor = System.Drawing.SystemColors.Window
        Me.txtPort.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPort.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPort.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPort.Location = New System.Drawing.Point(218, 12)
        Me.txtPort.MaxLength = 0
        Me.txtPort.Name = "txtPort"
        Me.txtPort.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPort.Size = New System.Drawing.Size(49, 19)
        Me.txtPort.TabIndex = 14
        Me.txtPort.Text = "1"
        '
        '_optConnect_1
        '
        Me._optConnect_1.BackColor = System.Drawing.SystemColors.Control
        Me._optConnect_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optConnect_1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optConnect_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optConnect.SetIndex(Me._optConnect_1, CType(1, Short))
        Me._optConnect_1.Location = New System.Drawing.Point(21, 36)
        Me._optConnect_1.Name = "_optConnect_1"
        Me._optConnect_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optConnect_1.Size = New System.Drawing.Size(90, 21)
        Me._optConnect_1.TabIndex = 11
        Me._optConnect_1.TabStop = True
        Me._optConnect_1.Text = "Ethernet"
        Me._optConnect_1.UseVisualStyleBackColor = False
        '
        '_optConnect_0
        '
        Me._optConnect_0.BackColor = System.Drawing.SystemColors.Control
        Me._optConnect_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optConnect_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optConnect_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optConnect.SetIndex(Me._optConnect_0, CType(0, Short))
        Me._optConnect_0.Location = New System.Drawing.Point(21, 12)
        Me._optConnect_0.Name = "_optConnect_0"
        Me._optConnect_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optConnect_0.Size = New System.Drawing.Size(90, 21)
        Me._optConnect_0.TabIndex = 10
        Me._optConnect_0.TabStop = True
        Me._optConnect_0.Text = "Serial"
        Me._optConnect_0.UseVisualStyleBackColor = False
        '
        '_lblConnect_1
        '
        Me._lblConnect_1.BackColor = System.Drawing.SystemColors.Control
        Me._lblConnect_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblConnect_1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblConnect_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblConnect.SetIndex(Me._lblConnect_1, CType(1, Short))
        Me._lblConnect_1.Location = New System.Drawing.Point(132, 40)
        Me._lblConnect_1.Name = "_lblConnect_1"
        Me._lblConnect_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblConnect_1.Size = New System.Drawing.Size(65, 20)
        Me._lblConnect_1.TabIndex = 13
        Me._lblConnect_1.Text = "IP adress"
        '
        '_lblConnect_0
        '
        Me._lblConnect_0.BackColor = System.Drawing.SystemColors.Control
        Me._lblConnect_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblConnect_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblConnect_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblConnect.SetIndex(Me._lblConnect_0, CType(0, Short))
        Me._lblConnect_0.Location = New System.Drawing.Point(133, 15)
        Me._lblConnect_0.Name = "_lblConnect_0"
        Me._lblConnect_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblConnect_0.Size = New System.Drawing.Size(52, 20)
        Me._lblConnect_0.TabIndex = 12
        Me._lblConnect_0.Text = "Com-Port"
        '
        'cmdDCI
        '
        '
        'cmdHost
        '
        '
        'optConnect
        '
        '
        'Main_Renamed
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(391, 500)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.fraDCI)
        Me.Controls.Add(Me.fraHost)
        Me.Controls.Add(Me.fraConnection)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.Name = "Main_Renamed"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MotocomDemoVB"
        Me.Frame2.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        Me.fraDCI.ResumeLayout(False)
        Me.fraHost.ResumeLayout(False)
        Me.fraConnection.ResumeLayout(False)
        Me.fraEthernetMode.ResumeLayout(False)
        CType(Me.cmdDCI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdHost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblConnect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optConnect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optMode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region 
#Region "Upgrade Support "
	Private Shared m_vb6FormDefInstance As Main_Renamed
	Private Shared m_InitializingDefInstance As Boolean
	Public Shared Property DefInstance() As Main_Renamed
		Get
			If m_vb6FormDefInstance Is Nothing OrElse m_vb6FormDefInstance.IsDisposed Then
				m_InitializingDefInstance = True
				m_vb6FormDefInstance = New Main_Renamed()
				m_InitializingDefInstance = False
			End If
			DefInstance = m_vb6FormDefInstance
		End Get
		Set
			m_vb6FormDefInstance = Value
		End Set
	End Property
#End Region 
	
	Private m_nCid As Short
	Private bDCI2 As Boolean
	
	Private Sub cmdDCI_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDCI.Click
		Dim Index As Short = cmdDCI.GetIndex(eventSender)
		Select Case Index
			Case 0
				DoDCI(0)
			Case 1
				DoDCI(1)
			Case 2
				DoDCI(2)
			Case 3
				DoDCI(3)
				
		End Select
	End Sub
	
	Private Sub cmdExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdExit.Click
		Me.Close()
	End Sub
	
	Private Sub cmdHost_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdHost.Click
		Dim Index As Short = cmdHost.GetIndex(eventSender)
		Select Case Index
			Case 0
				DoHost(0)
			Case 1
				DoHost(1)
			Case 2
				DoHost(2)
			Case 3
				DoHost(3)
		End Select
	End Sub
	
	
	
	Private Sub Main_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		optConnect(1).Checked = True
	End Sub
	
	
	
	'UPGRADE_WARNING: Event optConnect.CheckedChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"'
	Private Sub optConnect_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optConnect.CheckedChanged
		If eventSender.Checked Then
			Dim Index As Short = optConnect.GetIndex(eventSender)
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
		End If
	End Sub
	
	
	Private Function DoHost(ByRef function_id As Short) As Object
		Dim ret As Integer
		Dim path As String
		Dim open_mode As Short
		
        Dim port As Short
		Dim baud As Integer
		Dim parity As Short
		Dim clen As Short
		Dim stp As Short
		
		Dim strIPaddr As String
		Dim ether_mode As Short
		Dim hWnd As Integer
		
		Dim type_ As Short
		Dim varno As Short
		Dim vardata(10) As Double

		'******************************************************************************************
		'***************** Connect to Yasnac *********************************************************
		If optConnect(0).Checked = True Then
			'serial communication
			path = VB6.GetPath 'working directory
			open_mode = 1 'serial mode
			
			'step 1: get a hardware key handle
			m_nCid = BscOpen(path, open_mode)
			
			If m_nCid >= 0 Then
				
				port = CShort(txtPort.Text) 'read value from user interface
				baud = 9600 '9600 baud
				parity = 2 'even parity
				clen = 8 '8 data bits
				stp = 0 '1 stop bit
				
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
						MsgBox("Error establish connection !")
					End If
				Else
					ret = BscClose(m_nCid)
					m_nCid = -1
					MsgBox("Error setting up com port !")
				End If
			Else
				MsgBox("Hardware Key Error !")
			End If
		Else
			'ethernet communication
			path = VB6.GetPath 'working directory
			If optMode(0).Checked = True Then
				open_mode = 256 'ethernet E-Server mode
			Else
				open_mode = 16 'ethernet BSC mode
			End If
			
			'step 1: get a hardware key handle
			m_nCid = BscOpen(path, open_mode)
			
			If m_nCid >= 0 Then
				strIPaddr = txtIP.Text 'read value from user interface
				ether_mode = 0 'for host function client-mode is neccessary
				hWnd = Me.Handle.ToInt32 'handle of dialog window
				
				'step 2: setup ethernet
				If optMode(0).Checked = True Then
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
						MsgBox("Error establish connection !")
					End If
				Else
					ret = BscClose(m_nCid)
					m_nCid = -1
					MsgBox("Error setting up ethernet !")
				End If
			Else
				MsgBox("Hardware Key Error !")
			End If
		End If
		
		
		'******************************************************************************************
		'***************** Transmit Data **********************************************************
		'step 4: access robot control
		If m_nCid >= 0 Then
			If function_id = 0 Then
				'read value of B000 variable
				type_ = 0 'Byte variable
				varno = 0 'No. 0 / B000
                ret = BscGetVarData(m_nCid, type_, varno, vardata(0))
                If ret = 0 Then
                    'adjust user interface
                    txtHost.Text = CStr(vardata(0))
                    MsgBox("Ok, variable was received from robot !")
                Else
                    MsgBox("Error reading variable !")
                End If
			ElseIf function_id = 1 Then 
				'write value of B000 variable
				type_ = 0 'Byte variable
				varno = 0 'No. 0 / B000
				vardata(0) = CDbl(txtHost.Text) 'read value from user interface
				ret = BscPutVarData(m_nCid, type_, varno, vardata(0))
				If ret = 0 Then
					MsgBox("Ok, variable was sent to robot !")
				Else
					MsgBox("Error writing variable !")
				End If
			ElseIf function_id = 2 Then 
				'get file from robot
				ret = BscUpLoad(m_nCid, UCase(CStr(Text2.Text)))
				If ret = 0 Then
					MsgBox("Ok, file was uploaded from robot !")
				Else
					MsgBox("Error uploading file !")
				End If
			ElseIf function_id = 3 Then 
				'transfer file to robot
				ret = BscDownLoad(m_nCid, UCase(CStr(Text2.Text)))
				If ret = 0 Then
					MsgBox("Ok, file was downloaded to robot !")
				Else
					MsgBox("Error downloading file !")
				End If
			End If
			
			'step 5: if work is done disconnect
			ret = BscDisConnect(m_nCid)
			'step 6: free handle
			ret = BscClose(m_nCid)
		End If
	End Function
	
	Private Function DoDCI(ByRef function_id As Short) As Object
		Dim ret As Integer
		Dim path As String
		Dim open_mode As Short
		
        Dim port As Short
		Dim baud As Integer
		Dim parity As Short
		Dim clen As Short
		Dim stp As Short
		
		Dim strIPaddr As String
		Dim ether_mode As Short
		Dim hWnd As Integer
		
		Dim type_ As Short
		Dim rconf As Short
		Dim vardata(10) As Double

		If function_id = 3 Then
			bDCI2 = False
            Exit Function
        End If
		
		
		'******************************************************************************************
		'***************** Connect to Yasnac *********************************************************
		If optConnect(0).Checked = True Then
			'serial communication
			path = VB6.GetPath 'working directory
			open_mode = 1 'serial mode
			
			'step 1: get a hardware key handle
			m_nCid = BscOpen(path, open_mode)
			
			If m_nCid >= 0 Then
				port = CShort(txtPort.Text) 'read value from user interface
				baud = 9600 '9600 baud
				parity = 2 'even parity
				clen = 8 '8 data bits
				stp = 0 '1 stop bit
				
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
						MsgBox("Error establish connection !")
					End If
				Else
					ret = BscClose(m_nCid)
					m_nCid = -1
					MsgBox("Error setting up com port !")
				End If
			Else
				MsgBox("Hardware Key Error !")
			End If
		Else
			'ethernet communication
			path = VB6.GetPath 'working directory
			open_mode = 16 'ethernet mode
			
			'step 1: get a hardware key handle
			m_nCid = BscOpen(path, open_mode)
			
			If m_nCid >= 0 Then
				strIPaddr = txtIP.Text 'read value from user interface
				ether_mode = 1 'for dci function server-mode is neccessary
				hWnd = Me.Handle.ToInt32 'handle of dialog window
				
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
						MsgBox("Error establish connection !")
					End If
				Else
					ret = BscClose(m_nCid)
					m_nCid = -1
					MsgBox("Error setting up ethernet !")
				End If
			Else
				MsgBox("Hardware Key Error !")
			End If
		End If
		
		
		'******************************************************************************************
		'***************** Transmit Data **********************************************************
		'step 4: access robot control
		If m_nCid >= 0 Then
			Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
			If function_id = 0 Then
                'get Variable, SAVEV on robot side
                'corresponding job on controller is
                'NOP
                'SAVE B000
                'END
                cmdDCI(0).Enabled = False
				ret = BscDCIGetPos(m_nCid, type_, rconf, vardata(0))
				cmdDCI(0).Enabled = True
				Me.Cursor = System.Windows.Forms.Cursors.Default
				If ret <> -1 Then
					'adjust user interface
					txtDCI.Text = CStr(vardata(0)) 'display value
					MsgBox("Ok, variable was received from robot by DCI !")
				Else
					MsgBox("Error reading variable !")
				End If
			ElseIf function_id = 1 Then 
                'put B-variable, LOADV on robot side
                'corresponding job on controller is
                'NOP
                'LOADV B000
                'END
                type_ = 1 'Byte variable
				rconf = 0 'Form data
				vardata(0) = CDbl(txtDCI.Text) 'read value from user interface
				cmdDCI(1).Enabled = False
				ret = BscDCIPutPos(m_nCid, type_, rconf, vardata(0))
				cmdDCI(1).Enabled = True
				Me.Cursor = System.Windows.Forms.Cursors.Default
				If ret = 0 Then
					MsgBox("Ok, variable was sent to robot by DCI !")
				Else
					MsgBox("Error writing variable !")
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
                    System.Windows.Forms.Application.DoEvents()
                Loop Until ret <> -1 Or bDCI2 = False
                Me.Cursor = System.Windows.Forms.Cursors.Default
                If bDCI2 <> False Then
                    'second send requested job
                    ret = BscDCILoadSaveOnce(m_nCid)
                    If ret <> -1 Then
                        MsgBox("Ok, job was sent to robot by DCI !")
                    Else
                        MsgBox("Error sending job !")
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

End Class