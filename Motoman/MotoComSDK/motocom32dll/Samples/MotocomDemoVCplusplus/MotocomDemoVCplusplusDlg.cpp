// MotocomDemoVCplusplusDlg.cpp : implementation file
//

#include "stdafx.h"
#include "MotocomDemoVCplusplus.h"
#include "MotocomDemoVCplusplusDlg.h"
#include "Motocom.h"


#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CMotocomDemoVCplusplusDlg dialog

CMotocomDemoVCplusplusDlg::CMotocomDemoVCplusplusDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CMotocomDemoVCplusplusDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CMotocomDemoVCplusplusDlg)
		// NOTE: the ClassWizard will add member initialization here
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CMotocomDemoVCplusplusDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CMotocomDemoVCplusplusDlg)
		// NOTE: the ClassWizard will add DDX and DDV calls here
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CMotocomDemoVCplusplusDlg, CDialog)
	//{{AFX_MSG_MAP(CMotocomDemoVCplusplusDlg)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_COM, OnCom)
	ON_BN_CLICKED(IDC_ETHER, OnEther)
	ON_BN_CLICKED(IDC_HOSTGETVAR, OnHostgetvar)
	ON_BN_CLICKED(IDC_HOSTPUTVAR, OnHostputvar)
	ON_BN_CLICKED(IDC_DCIGETPOS, OnDcigetpos)
	ON_BN_CLICKED(IDC_DCISETPOS, OnDcisetpos)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CMotocomDemoVCplusplusDlg message handlers

BOOL CMotocomDemoVCplusplusDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon
	
	GetCurrentDirectory(255,m_strCurDir);

	//set initial user interface
	((CButton*)GetDlgItem(IDC_COM))->SetCheck(1);
	((CButton*)GetDlgItem(IDC_BSC))->SetCheck(1);
	OnCom();
	GetDlgItem(IDC_COMPORT)->SetWindowText("1");
	GetDlgItem(IDC_IP)->SetWindowText("192.168.10.1");
	GetDlgItem(IDC_HOSTVAL)->SetWindowText("0");
	GetDlgItem(IDC_DCIVAL)->SetWindowText("0");



	return TRUE;  // return TRUE  unless you set the focus to a control
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CMotocomDemoVCplusplusDlg::OnPaint() 
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, (WPARAM) dc.GetSafeHdc(), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

// The system calls this to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CMotocomDemoVCplusplusDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}

void CMotocomDemoVCplusplusDlg::OnCancel() 
{
	// TODO: Add extra cleanup here
	
	CDialog::OnCancel();
}

void CMotocomDemoVCplusplusDlg::OnCom() 
{
	GetDlgItem(IDC_IP)->EnableWindow(FALSE);
	GetDlgItem(IDC_ESERVER)->EnableWindow(FALSE);
	GetDlgItem(IDC_BSC)->EnableWindow(FALSE);
	GetDlgItem(IDC_COMPORT)->EnableWindow(TRUE);
}

void CMotocomDemoVCplusplusDlg::OnEther() 
{
	GetDlgItem(IDC_IP)->EnableWindow(TRUE);
	GetDlgItem(IDC_ESERVER)->EnableWindow(TRUE);
	GetDlgItem(IDC_BSC)->EnableWindow(TRUE);
	GetDlgItem(IDC_COMPORT)->EnableWindow(FALSE);
}

void CMotocomDemoVCplusplusDlg::OnHostgetvar() 
{
	int ret;
	ret=DoHost(0);
}

void CMotocomDemoVCplusplusDlg::OnHostputvar() 
{
	int ret;
	ret=DoHost(1);	
}

void CMotocomDemoVCplusplusDlg::OnDcigetpos() 
{
	int ret;
	ret=DoDCI(0);		
}

void CMotocomDemoVCplusplusDlg::OnDcisetpos() 
{
	int ret;
	ret=DoDCI(1);			
}

short CMotocomDemoVCplusplusDlg::DoHost(short function_id)
{
	int ret;
	char* path;
	short open_mode;

	CString strPort;
	short port;
	DWORD baud;
	short parity;
	short clen;
	short stp;

	char strIPaddr[20];
	short ether_mode;
	HWND hWnd;

	short type;
	short varno;
	double vardata[11];
	CString strVal;


	//******************************************************************************************
	//***************** Connect to XRC *********************************************************

	if (((CButton*)GetDlgItem(IDC_COM))->GetCheck()==1)
	//serial communication	
	{
		path=m_strCurDir;									//working directory
		open_mode=1;										//serial mode

		//step 1: get a hardware key handle
		m_nCid=BscOpen(path,open_mode);

		if (m_nCid>=0)
		{
			GetDlgItem(IDC_COMPORT)->GetWindowText(strPort);		//read value from user interface
			sscanf(strPort,"%d",&port);
			baud=9600;										//9600 baud
			parity=2;										//even parity
			clen=8;											//8 data bits
			stp=0;											//1 stop bit
			
			//step 2: setup com port
			ret=BscSetCom(m_nCid,port,baud,parity,clen,stp);
			
			if (ret==1)
			{
				//step 3: Establish a connection
				ret=BscConnect(m_nCid);
				if (ret==1)
				{
					//...
				}
				else
				{
					ret=BscClose(m_nCid);
					m_nCid=-1;
					AfxMessageBox("Error establish connection !");
				}
			}
			else
			{
				ret=BscClose(m_nCid);
				m_nCid=-1;
				AfxMessageBox("Error setting up com port !");
			}
		}
		else
		{
			AfxMessageBox("Hardware Key Error !");
		}
	}
	else
	//ethernet communication
	{
		path=m_strCurDir;									//working directory
		if (((CButton*)GetDlgItem(IDC_ESERVER))->GetCheck()==1)
			open_mode=256;										//eserver mode
		else
			open_mode=16;										//bsc mode
			
		
		//step 1: get a hardware key handle
		m_nCid=BscOpen(path,open_mode);

		if (m_nCid>=0)
		{
			GetDlgItem(IDC_IP)->GetWindowText(strIPaddr,20);//read value from user interface
			ether_mode=0;									//for host function client-mode is neccessary
			hWnd=m_hWnd;									//handle of dialog window

			//step 2: setup ethernet
			if (((CButton*)GetDlgItem(IDC_ESERVER))->GetCheck()==1)
				ret=BscSetEServer(m_nCid,strIPaddr);
			else
				ret=BscSetEther(m_nCid,strIPaddr,ether_mode,hWnd);

			if (ret==1)
			{
				//step 3: Establish a connection
				ret=BscConnect(m_nCid);
				if (ret==1)
				{
					//...
				}
				else
				{
					ret=BscClose(m_nCid);
					m_nCid=-1;
					AfxMessageBox("Error establish connection !");
				}
			}
			else
			{
				ret=BscClose(m_nCid);
				m_nCid=-1;
				AfxMessageBox("Error setting up ethernet !");
			}
		}
		else
		{
			AfxMessageBox("Hardware Key Error !");
		}
	}
	


	//******************************************************************************************
	//***************** Transmit Data **********************************************************	
	
	//step 4: access robot control
	if (m_nCid>=0)
	{
		if (function_id==0)
		{
			//read value of B000 variable
			type=0;												//Byte variable
			varno=0;											//No. 0 / B000
			ret=BscGetVarData(m_nCid,type,varno,vardata);
			if (ret==0)
			{
				//adjust user interface
				strVal.Format("%f",vardata[0]);
				GetDlgItem(IDC_HOSTVAL)->SetWindowText(strVal);	//display value
				AfxMessageBox("Ok, variable was received from robot !");
			}
			else
			{
				AfxMessageBox("Error reading variable !");
			}
		}
		else if (function_id==1)
		{
			//write value of B000 variable
			type=0;												//Byte variable
			varno=0;											//No. 0 / B000
			GetDlgItem(IDC_HOSTVAL)->GetWindowText(strVal);		//read value from user interface
			vardata[0]=atof(strVal);
			ret=BscPutVarData(m_nCid,type,varno,vardata);
			if (ret==0)
			{
				AfxMessageBox("Ok, variable was sent to robot !");
			}
			else
			{
				AfxMessageBox("Error writing variable !");
			}
		}
		//step 5: if work is done disconnect
		ret=BscDisConnect(m_nCid);
		//step 6: free handle
		ret=BscClose(m_nCid);
	}
	return (0);
}

short CMotocomDemoVCplusplusDlg::DoDCI(short function_id)
{
	int ret;
	char* path;
	short open_mode;

	CString strPort;
	short port;
	DWORD baud;
	short parity;
	short clen;
	short stp;

	char strIPaddr[20];
	short ether_mode;
	HWND hWnd;

	short type;
	short rconf;
	double vardata[11];
	CString strVal;


	//******************************************************************************************
	//***************** Connect to XRC *********************************************************

	if (((CButton*)GetDlgItem(IDC_COM))->GetCheck()==1)
	//serial communication	
	{
		path=m_strCurDir;									//working directory
		open_mode=1;										//serial mode

		//step 1: get a hardware key handle
		m_nCid=BscOpen(path,open_mode);

		if (m_nCid>=0)
		{
			GetDlgItem(IDC_COMPORT)->GetWindowText(strPort);		//read value from user interface
			sscanf(strPort,"%d",&port);
			baud=9600;										//9600 baud
			parity=2;										//even parity
			clen=8;											//8 data bits
			stp=0;											//1 stop bit
			
			//step 2: setup com port
			ret=BscSetCom(m_nCid,port,baud,parity,clen,stp);
			
			if (ret==1)
			{
				//step 3: Establish a connection
				ret=BscConnect(m_nCid);
				if (ret==1)
				{
					//...
				}
				else
				{
					ret=BscClose(m_nCid);
					m_nCid=-1;
					AfxMessageBox("Error establish connection !");
				}
			}
			else
			{
				ret=BscClose(m_nCid);
				m_nCid=-1;
				AfxMessageBox("Error setting up com port !");
			}
		}
		else
		{
			AfxMessageBox("Hardware Key Error !");
		}
	}
	else
	//ethernet communication
	{
		path=m_strCurDir;									//working directory
		open_mode=16;										//ethernet mode
		
		//step 1: get a hardware key handle
		m_nCid=BscOpen(path,open_mode);

		if (m_nCid>=0)
		{
			GetDlgItem(IDC_IP)->GetWindowText(strIPaddr,20);//read value from user interface
			ether_mode=1;									//for dci function server-mode is neccessary
			hWnd=m_hWnd;									//handle of dialog window

			//step 2: setup ethernet
			ret=BscSetEther(m_nCid,strIPaddr,ether_mode,hWnd);

			if (ret==1)
			{
				//step 3: Establish a connection
				ret=BscConnect(m_nCid);
				if (ret==1)
				{
					//...
				}
				else
				{
					ret=BscClose(m_nCid);
					m_nCid=-1;
					AfxMessageBox("Error establish connection !");
				}
			}
			else
			{
				ret=BscClose(m_nCid);
				m_nCid=-1;
				AfxMessageBox("Error setting up ethernet !");
			}
		}
		else
		{
			AfxMessageBox("Hardware Key Error !");
		}
	}
	


	//******************************************************************************************
	//***************** Transmit Data **********************************************************	
	
	//step 4: access robot control
	if (m_nCid>=0)
	{
		if (function_id==0)
		{
            //get Variable, SAVEV on robot side
            //corresponding job on controller is
            //NOP
            //SAVE B000
            //END
			GetDlgItem(IDC_DCIGETPOS)->EnableWindow(FALSE);
			ret=BscDCIGetPos(m_nCid,(unsigned short*)&type,(unsigned short*)&rconf,vardata);
			GetDlgItem(IDC_DCIGETPOS)->EnableWindow(TRUE);
			if (ret!=-1)
			{
				//adjust user interface
				strVal.Format("%f",vardata[0]);
				GetDlgItem(IDC_DCIVAL)->SetWindowText(strVal);	//display value
				AfxMessageBox("Ok, variable was received from robot by DCI !");
			}
			else
			{
				AfxMessageBox("Error reading variable !");
			}
		}
		else if (function_id==1)
		{
            //put B-variable, LOADV on robot side
            //corresponding job on controller is
            //NOP
            //LOADV B000
            //END
			type=1;												//Byte variable
			rconf=0;											//Form data
			GetDlgItem(IDC_DCIVAL)->GetWindowText(strVal);		//read value from user interface
			vardata[0]=atof(strVal);
			GetDlgItem(IDC_DCISETPOS)->EnableWindow(FALSE);
			ret=BscDCIPutPos(m_nCid,type,rconf,vardata);
			GetDlgItem(IDC_DCISETPOS)->EnableWindow(TRUE);
			if (ret==0)
			{
				AfxMessageBox("Ok, variable was sent to robot by DCI !");
			}
			else
			{
				AfxMessageBox("Error writing variable !");
			}
		}
		//step 5: if work is done disconnect
		ret=BscDisConnect(m_nCid);
		//step 6: free handle
		ret=BscClose(m_nCid);
	}
	return (0);
}

