// MotocomDemoVCplusplusDlg.h : header file
//

#if !defined(AFX_MOTOCOMDEMOVCPLUSPLUSDLG_H__103F7813_2DA5_467C_9204_9D6064566F47__INCLUDED_)
#define AFX_MOTOCOMDEMOVCPLUSPLUSDLG_H__103F7813_2DA5_467C_9204_9D6064566F47__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CMotocomDemoVCplusplusDlg dialog

class CMotocomDemoVCplusplusDlg : public CDialog
{
// Construction
public:
	CMotocomDemoVCplusplusDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CMotocomDemoVCplusplusDlg)
	enum { IDD = IDD_MOTOCOMDEMOVCPLUSPLUS_DIALOG };
		// NOTE: the ClassWizard will add data members here
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CMotocomDemoVCplusplusDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	//{{AFX_MSG(CMotocomDemoVCplusplusDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	virtual void OnCancel();
	afx_msg void OnCom();
	afx_msg void OnEther();
	afx_msg void OnHostgetvar();
	afx_msg void OnHostputvar();
	afx_msg void OnDcigetpos();
	afx_msg void OnDcisetpos();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
private:
	char m_strCurDir[255];
	short DoDCI(short function_id);
	short DoHost (short function_id);
	short m_nCid;
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_MOTOCOMDEMOVCPLUSPLUSDLG_H__103F7813_2DA5_467C_9204_9D6064566F47__INCLUDED_)
