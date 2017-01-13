// MotocomLoadLibrary.h : main header file for the MOTOCOMLOADLIBRARY application
//

#if !defined(AFX_MOTOCOMLOADLIBRARY_H__C2CC7957_18A7_4BC0_ABB5_1E2C559B0E4A__INCLUDED_)
#define AFX_MOTOCOMLOADLIBRARY_H__C2CC7957_18A7_4BC0_ABB5_1E2C559B0E4A__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CMotocomLoadLibraryApp:
// See MotocomLoadLibrary.cpp for the implementation of this class
//

class CMotocomLoadLibraryApp : public CWinApp
{
public:
	CMotocomLoadLibraryApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CMotocomLoadLibraryApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CMotocomLoadLibraryApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_MOTOCOMLOADLIBRARY_H__C2CC7957_18A7_4BC0_ABB5_1E2C559B0E4A__INCLUDED_)
