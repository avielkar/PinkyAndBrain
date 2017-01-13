// MotocomLoadLibraryDlg.h : header file
//

#if !defined(AFX_MOTOCOMLOADLIBRARYDLG_H__62F9785B_3D10_41D8_8529_41638B9C9FE0__INCLUDED_)
#define AFX_MOTOCOMLOADLIBRARYDLG_H__62F9785B_3D10_41D8_8529_41638B9C9FE0__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CMotocomLoadLibraryDlg dialog

class CMotocomLoadLibraryDlg : public CDialog
{
// Construction
public:
	CMotocomLoadLibraryDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CMotocomLoadLibraryDlg)
	enum { IDD = IDD_MOTOCOMLOADLIBRARY_DIALOG };
		// NOTE: the ClassWizard will add data members here
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CMotocomLoadLibraryDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	//{{AFX_MSG(CMotocomLoadLibraryDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnSendbytevar();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_MOTOCOMLOADLIBRARYDLG_H__62F9785B_3D10_41D8_8529_41638B9C9FE0__INCLUDED_)
