
// SampleDlg.cpp : 実装ファイル
//

#include "stdafx.h"
#include "Sample.h"
#include "SampleDlg.h"

#include "clsCommand.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CSampleDlg ダイアログ

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


// CSampleDlg メッセージ ハンドラ

BOOL CSampleDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// このダイアログのアイコンを設定します。アプリケーションのメイン ウィンドウがダイアログでない場合、
	//  Framework は、この設定を自動的に行います。
	SetIcon(m_hIcon, TRUE);			// 大きいアイコンの設定
	SetIcon(m_hIcon, FALSE);		// 小さいアイコンの設定

	// TODO: 初期化をここに追加します。

	return TRUE;  // フォーカスをコントロールに設定した場合を除き、TRUE を返します。
}

// ダイアログに最小化ボタンを追加する場合、アイコンを描画するための
//  下のコードが必要です。ドキュメント/ビュー モデルを使う MFC アプリケーションの場合、
//  これは、Framework によって自動的に設定されます。

void CSampleDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // 描画のデバイス コンテキスト

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// クライアントの四角形領域内の中央
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// アイコンの描画
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

// ユーザーが最小化したウィンドウをドラッグしているときに表示するカーソルを取得するために、
//  システムがこの関数を呼び出します。
HCURSOR CSampleDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}


void CSampleDlg::OnBnClickedBtnesgetalarm()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetAlarm();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetalarmex()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetAlarmEx();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetalarmhist()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetAlarmHist();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetalarmhistex()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetAlarmHistEx();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetstatus()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetStatus();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetjobstatus()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetJobStatus();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetconfiguration()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetConfiguration();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetposition()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetPosition();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetdeviation()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetDeviation();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgettorque()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetTorque();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetmonitoringtime()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetMonitoringTime();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetsysteminfo()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetSystemInfo();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesreadio()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esReadIO1();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtneswriteio()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esWriteIO1();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesreadregister()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esReadRegister();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtneswriteregister()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esWriteRegister();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesreadiom()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esReadIOM();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtneswriteiom()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esWriteIOM();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesreadregisterm()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esReadRegisterM();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtneswriteregisterm()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esWriteRegisterM();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetvardata()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetVarData1();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnessetvardata()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esSetVarData1();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetstrdata()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetStrData();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnessetstrdata()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esSetStrData();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetpositiondata()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetPositionData();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnessetpositiondata()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esSetPositionData();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetbpexpositiondata()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetBpexPositionData();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnessetbpexpositiondata()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esSetBpexPositionData();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetvardatamb()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetVarDataMB();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnessetvardatamb()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esSetVarDataMB();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetvardatami()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetVarDataMI();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnessetvardatami()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esSetVarDataMI();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetvardatamd()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetVarDataMD();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnessetvardatamd()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esSetVarDataMD();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetvardatamr()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetVarDataMR();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnessetvardatamr()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esSetVarDataMR();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetstrdatam()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetStrDataM();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnessetstrdatam()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esSetStrDataM();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetpositiondatam()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetPositionDataM();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnessetpositiondatam()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esSetPositionDataM();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesgetbpexpositiondatam()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esGetBpexPositionDataM();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnessetbpexpositiondatam()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esSetBpexPositionDataM();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesreset()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esReset();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnescancel()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esCancel();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtneshold()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esHold();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesservo()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esServo();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtneshlock()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esHlock();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnescycle()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esCycle();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesbdsp()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esBDSP();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesstartjob()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esStartJob();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesselectjob()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esSelectJob();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnessavefile()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esSaveFile();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesloadfile()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esLoadFile();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesdeletejob()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esDeleteJob();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnesfilelist()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esFileList();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnescartmove()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esCartMove();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}

void CSampleDlg::OnBnClickedBtnespulsemove()
{
	// TODO: ここにコントロール通知ハンドラ コードを追加します。
	CString res = "";

	//ESOpen
	res = _Command.esOpen();
	_Log.Append(res);

	res = _Command.esPulseMove();
	_Log.Append(res);

	//ESClose
	res = _Command.esClose();
	_Log.Append(res);

	//結果表示
	SetDlgItemText(IDC_EDIT1, _Log);
}
