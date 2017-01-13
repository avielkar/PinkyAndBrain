
// SampleDlg.cpp : �����t�@�C��
//

#include "stdafx.h"
#include "Sample.h"
#include "SampleDlg.h"

#include "clsCommand.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CSampleDlg �_�C�A���O

clsCommand _Command;
CString _Log = "";

CSampleDlg::CSampleDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CSampleDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CSampleDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CSampleDlg, CDialog)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	//}}AFX_MSG_MAP
	ON_BN_CLICKED(IDC_BTNESGETALARM, &CSampleDlg::OnBnClickedBtnesgetalarm)
	ON_BN_CLICKED(IDC_BTNESGETALARMEX, &CSampleDlg::OnBnClickedBtnesgetalarmex)
	ON_BN_CLICKED(IDC_BTNESGETALARMHIST, &CSampleDlg::OnBnClickedBtnesgetalarmhist)
	ON_BN_CLICKED(IDC_BTNESGETALARMHISTEX, &CSampleDlg::OnBnClickedBtnesgetalarmhistex)
	ON_BN_CLICKED(IDC_BTNESGETJOBSTATUS, &CSampleDlg::OnBnClickedBtnesgetjobstatus)
	ON_BN_CLICKED(IDC_BTNESGETSTATUS, &CSampleDlg::OnBnClickedBtnesgetstatus)
	ON_BN_CLICKED(IDC_BTNESGETCONFIGURATION, &CSampleDlg::OnBnClickedBtnesgetconfiguration)
	ON_BN_CLICKED(IDC_BTNESGETPOSITION, &CSampleDlg::OnBnClickedBtnesgetposition)
	ON_BN_CLICKED(IDC_BTNESGETDEVIATION, &CSampleDlg::OnBnClickedBtnesgetdeviation)
	ON_BN_CLICKED(IDC_BTNESGETTORQUE, &CSampleDlg::OnBnClickedBtnesgettorque)
	ON_BN_CLICKED(IDC_BTNESGETMONITORINGTIME, &CSampleDlg::OnBnClickedBtnesgetmonitoringtime)
	ON_BN_CLICKED(IDC_BTNESGETSYSTEMINFO, &CSampleDlg::OnBnClickedBtnesgetsysteminfo)
	ON_BN_CLICKED(IDC_BTNESREADIO, &CSampleDlg::OnBnClickedBtnesreadio)
	ON_BN_CLICKED(IDC_BTNESWRITEIO, &CSampleDlg::OnBnClickedBtneswriteio)
	ON_BN_CLICKED(IDC_BTNESREADREGISTER, &CSampleDlg::OnBnClickedBtnesreadregister)
	ON_BN_CLICKED(IDC_BTNESWRITEREGISTER, &CSampleDlg::OnBnClickedBtneswriteregister)
	ON_BN_CLICKED(IDC_BTNESREADIOM, &CSampleDlg::OnBnClickedBtnesreadiom)
	ON_BN_CLICKED(IDC_BTNESWRITEIOM, &CSampleDlg::OnBnClickedBtneswriteiom)
	ON_BN_CLICKED(IDC_BTNESREADREGISTERM, &CSampleDlg::OnBnClickedBtnesreadregisterm)
	ON_BN_CLICKED(IDC_BTNESWRITEREGISTERM, &CSampleDlg::OnBnClickedBtneswriteregisterm)
	ON_BN_CLICKED(IDC_BTNESGETVARDATA, &CSampleDlg::OnBnClickedBtnesgetvardata)
	ON_BN_CLICKED(IDC_BTNESSETVARDATA, &CSampleDlg::OnBnClickedBtnessetvardata)
	ON_BN_CLICKED(IDC_BTNESGETSTRDATA, &CSampleDlg::OnBnClickedBtnesgetstrdata)
	ON_BN_CLICKED(IDC_BTNESSETSTRDATA, &CSampleDlg::OnBnClickedBtnessetstrdata)
	ON_BN_CLICKED(IDC_BTNESGETPOSITIONDATA, &CSampleDlg::OnBnClickedBtnesgetpositiondata)
	ON_BN_CLICKED(IDC_BTNESSETPOSITIONDATA, &CSampleDlg::OnBnClickedBtnessetpositiondata)
	ON_BN_CLICKED(IDC_BTNESGETBPEXPOSITIONDATA, &CSampleDlg::OnBnClickedBtnesgetbpexpositiondata)
	ON_BN_CLICKED(IDC_BTNESSETBPEXPOSITIONDATA, &CSampleDlg::OnBnClickedBtnessetbpexpositiondata)
	ON_BN_CLICKED(IDC_BTNESGETVARDATAMB, &CSampleDlg::OnBnClickedBtnesgetvardatamb)
	ON_BN_CLICKED(IDC_BTNESSETVARDATAMB, &CSampleDlg::OnBnClickedBtnessetvardatamb)
	ON_BN_CLICKED(IDC_BTNESGETVARDATAMI, &CSampleDlg::OnBnClickedBtnesgetvardatami)
	ON_BN_CLICKED(IDC_BTNESSETVARDATAMI, &CSampleDlg::OnBnClickedBtnessetvardatami)
	ON_BN_CLICKED(IDC_BTNESGETVARDATAMD, &CSampleDlg::OnBnClickedBtnesgetvardatamd)
	ON_BN_CLICKED(IDC_BTNESSETVARDATAMD, &CSampleDlg::OnBnClickedBtnessetvardatamd)
	ON_BN_CLICKED(IDC_BTNESGETVARDATAMR, &CSampleDlg::OnBnClickedBtnesgetvardatamr)
	ON_BN_CLICKED(IDC_BTNESSETVARDATAMR, &CSampleDlg::OnBnClickedBtnessetvardatamr)
	ON_BN_CLICKED(IDC_BTNESGETSTRDATAM, &CSampleDlg::OnBnClickedBtnesgetstrdatam)
	ON_BN_CLICKED(IDC_BTNESSETSTRDATAM, &CSampleDlg::OnBnClickedBtnessetstrdatam)
	ON_BN_CLICKED(IDC_BTNESGETPOSITIONDATAM, &CSampleDlg::OnBnClickedBtnesgetpositiondatam)
	ON_BN_CLICKED(IDC_BTNESSETPOSITIONDATAM, &CSampleDlg::OnBnClickedBtnessetpositiondatam)
	ON_BN_CLICKED(IDC_BTNESGETBPEXPOSITIONDATAM, &CSampleDlg::OnBnClickedBtnesgetbpexpositiondatam)
	ON_BN_CLICKED(IDC_BTNESSETBPEXPOSITIONDATAM, &CSampleDlg::OnBnClickedBtnessetbpexpositiondatam)
	ON_BN_CLICKED(IDC_BTNESRESET, &CSampleDlg::OnBnClickedBtnesreset)
	ON_BN_CLICKED(IDC_BTNESCANCEL, &CSampleDlg::OnBnClickedBtnescancel)
	ON_BN_CLICKED(IDC_BTNESHOLD, &CSampleDlg::OnBnClickedBtneshold)
	ON_BN_CLICKED(IDC_BTNESSERVO, &CSampleDlg::OnBnClickedBtnesservo)
	ON_BN_CLICKED(IDC_BTNESHLOCK, &CSampleDlg::OnBnClickedBtneshlock)
	ON_BN_CLICKED(IDC_BTNESCYCLE, &CSampleDlg::OnBnClickedBtnescycle)
	ON_BN_CLICKED(IDC_BTNESBDSP, &CSampleDlg::OnBnClickedBtnesbdsp)
	ON_BN_CLICKED(IDC_BTNESSTARTJOB, &CSampleDlg::OnBnClickedBtnesstartjob)
	ON_BN_CLICKED(IDC_BTNESSELECTJOB, &CSampleDlg::OnBnClickedBtnesselectjob)
	ON_BN_CLICKED(IDC_BTNESSAVEFILE, &CSampleDlg::OnBnClickedBtnessavefile)
	ON_BN_CLICKED(IDC_BTNESLOADFILE, &CSampleDlg::OnBnClickedBtnesloadfile)
	ON_BN_CLICKED(IDC_BTNESDELETEJOB, &CSampleDlg::OnBnClickedBtnesdeletejob)
	ON_BN_CLICKED(IDC_BTNESFILELIST, &CSampleDlg::OnBnClickedBtnesfilelist)
	ON_BN_CLICKED(IDC_BTNESCARTMOVE, &CSampleDlg::OnBnClickedBtnescartmove)
	ON_BN_CLICKED(IDC_BTNESPULSEMOVE, &CSampleDlg::OnBnClickedBtnespulsemove)
END_MESSAGE_MAP()


// CSampleDlg ���b�Z�[�W �n���h��

BOOL CSampleDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// ���̃_�C�A���O�̃A�C�R����ݒ肵�܂��B�A�v���P�[�V�����̃��C�� �E�B���h�E���_�C�A���O�łȂ��ꍇ�A
	//  Framework �́A���̐ݒ�������I�ɍs���܂��B
	SetIcon(m_hIcon, TRUE);			// �傫���A�C�R���̐ݒ�
	SetIcon(m_hIcon, FALSE);		// �������A�C�R���̐ݒ�

	// TODO: �������������ɒǉ����܂��B

	return TRUE;  // �t�H�[�J�X���R���g���[���ɐݒ肵���ꍇ�������ATRUE ��Ԃ��܂��B
}

// �_�C�A���O�ɍŏ����{�^����ǉ�����ꍇ�A�A�C�R����`�悷�邽�߂�
//  ���̃R�[�h���K�v�ł��B�h�L�������g/�r���[ ���f�����g�� MFC �A�v���P�[�V�����̏ꍇ�A
//  ����́AFramework �ɂ���Ď����I�ɐݒ肳��܂��B

void CSampleDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // �`��̃f�o�C�X �R���e�L�X�g

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// �N���C�A���g�̎l�p�`�̈���̒���
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// �A�C�R���̕`��
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

// ���[�U�[���ŏ��������E�B���h�E���h���b�O���Ă���Ƃ��ɕ\������J�[�\�����擾���邽�߂ɁA
//  �V�X�e�������̊֐����Ăяo���܂��B
HCURSOR CSampleDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}


void CSampleDlg::OnBnClickedBtnesgetalarm()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetAlarm();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetalarmex()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetAlarmEx();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetalarmhist()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetAlarmHist();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetalarmhistex()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetAlarmHistEx();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetstatus()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetStatus();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetjobstatus()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetJobStatus();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetconfiguration()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetConfiguration();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetposition()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetPosition();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetdeviation()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetDeviation();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgettorque()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetTorque();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetmonitoringtime()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetMonitoringTime();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetsysteminfo()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetSystemInfo();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesreadio()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esReadIO1();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtneswriteio()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esWriteIO1();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesreadregister()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esReadRegister();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtneswriteregister()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esWriteRegister();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesreadiom()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esReadIOM();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtneswriteiom()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esWriteIOM();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesreadregisterm()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esReadRegisterM();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtneswriteregisterm()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esWriteRegisterM();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetvardata()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetVarData1();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnessetvardata()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esSetVarData1();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetstrdata()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetStrData();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnessetstrdata()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esSetStrData();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetpositiondata()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetPositionData();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnessetpositiondata()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esSetPositionData();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetbpexpositiondata()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetBpexPositionData();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnessetbpexpositiondata()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esSetBpexPositionData();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetvardatamb()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetVarDataMB();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnessetvardatamb()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esSetVarDataMB();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetvardatami()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetVarDataMI();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnessetvardatami()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esSetVarDataMI();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetvardatamd()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetVarDataMD();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnessetvardatamd()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esSetVarDataMD();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetvardatamr()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetVarDataMR();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnessetvardatamr()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esSetVarDataMR();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetstrdatam()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetStrDataM();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnessetstrdatam()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esSetStrDataM();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetpositiondatam()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetPositionDataM();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnessetpositiondatam()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esSetPositionDataM();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetbpexpositiondatam()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetBpexPositionDataM();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnessetbpexpositiondatam()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esSetBpexPositionDataM();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesreset()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esReset();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnescancel()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esCancel();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtneshold()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esHold();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesservo()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esServo();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtneshlock()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esHlock();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnescycle()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esCycle();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesbdsp()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esBDSP();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesstartjob()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esStartJob();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesselectjob()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esSelectJob();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnessavefile()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esSaveFile();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesloadfile()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esLoadFile();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesdeletejob()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esDeleteJob();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesfilelist()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esFileList();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnescartmove()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esCartMove();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnespulsemove()
{
	// TODO: �����ɃR���g���[���ʒm�n���h�� �R�[�h��ǉ����܂��B
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esPulseMove();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//���ʕ\��
	SetDlgItemText(IDC_EDIT1, _Log);
}
