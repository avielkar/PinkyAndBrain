
// SampleDlg.h : �w�b�_�[ �t�@�C��
//

#pragma once


// CSampleDlg �_�C�A���O
class CSampleDlg : public CDialog
{
// �R���X�g���N�V����
public:
	CSampleDlg(CWnd* pParent = NULL);	// �W���R���X�g���N�^

// �_�C�A���O �f�[�^
	enum { IDD = IDD_SAMPLE_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV �T�|�[�g


// ����
protected:
	HICON m_hIcon;

	// �������ꂽ�A���b�Z�[�W���蓖�Ċ֐�
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedBtnesgetalarm();
	afx_msg void OnBnClickedBtnesgetalarmex();
	afx_msg void OnBnClickedBtnesgetalarmhist();
	afx_msg void OnBnClickedBtnesgetalarmhistex();
	afx_msg void OnBnClickedBtnesgetjobstatus();
	afx_msg void OnBnClickedBtnesgetstatus();
	afx_msg void OnBnClickedBtnesgetconfiguration();
	afx_msg void OnBnClickedBtnesgetposition();
	afx_msg void OnBnClickedBtnesgetdeviation();
	afx_msg void OnBnClickedBtnesgettorque();
	afx_msg void OnBnClickedBtnesgetmonitoringtime();
	afx_msg void OnBnClickedBtnesgetsysteminfo();
	afx_msg void OnBnClickedBtnesreadio();
	afx_msg void OnBnClickedBtneswriteio();
	afx_msg void OnBnClickedBtnesreadregister();
	afx_msg void OnBnClickedBtneswriteregister();
	afx_msg void OnBnClickedBtnesreadiom();
	afx_msg void OnBnClickedBtneswriteiom();
	afx_msg void OnBnClickedBtnesreadregisterm();
	afx_msg void OnBnClickedBtneswriteregisterm();
	afx_msg void OnBnClickedBtnesgetvardata();
	afx_msg void OnBnClickedBtnessetvardata();
	afx_msg void OnBnClickedBtnesgetstrdata();
	afx_msg void OnBnClickedBtnessetstrdata();
	afx_msg void OnBnClickedBtnesgetpositiondata();
	afx_msg void OnBnClickedBtnessetpositiondata();
	afx_msg void OnBnClickedBtnesgetbpexpositiondata();
	afx_msg void OnBnClickedBtnessetbpexpositiondata();
	afx_msg void OnBnClickedBtnesgetvardatamb();
	afx_msg void OnBnClickedBtnessetvardatamb();
	afx_msg void OnBnClickedBtnesgetvardatami();
	afx_msg void OnBnClickedBtnessetvardatami();
	afx_msg void OnBnClickedBtnesgetvardatamd();
	afx_msg void OnBnClickedBtnessetvardatamd();
	afx_msg void OnBnClickedBtnesgetvardatamr();
	afx_msg void OnBnClickedBtnessetvardatamr();
	afx_msg void OnBnClickedBtnesgetstrdatam();
	afx_msg void OnBnClickedBtnessetstrdatam();
	afx_msg void OnBnClickedBtnesgetpositiondatam();
	afx_msg void OnBnClickedBtnessetpositiondatam();
	afx_msg void OnBnClickedBtnesgetbpexpositiondatam();
	afx_msg void OnBnClickedBtnessetbpexpositiondatam();
	afx_msg void OnBnClickedBtnesreset();
	afx_msg void OnBnClickedBtnescancel();
	afx_msg void OnBnClickedBtneshold();
	afx_msg void OnBnClickedBtnesservo();
	afx_msg void OnBnClickedBtneshlock();
	afx_msg void OnBnClickedBtnescycle();
	afx_msg void OnBnClickedBtnesbdsp();
	afx_msg void OnBnClickedBtnesstartjob();
	afx_msg void OnBnClickedBtnesselectjob();
	afx_msg void OnBnClickedBtnessavefile();
	afx_msg void OnBnClickedBtnesloadfile();
	afx_msg void OnBnClickedBtnesdeletejob();
	afx_msg void OnBnClickedBtnesfilelist();
	afx_msg void OnBnClickedBtnescartmove();
	afx_msg void OnBnClickedBtnespulsemove();
};
