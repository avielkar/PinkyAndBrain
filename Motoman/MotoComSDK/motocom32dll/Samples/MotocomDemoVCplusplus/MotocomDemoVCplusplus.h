// MotocomDemoVCplusplus.h : main header file for the MOTOCOMDEMOVCPLUSPLUS application
//

#if !defined(AFX_MOTOCOMDEMOVCPLUSPLUS_H__19947B1C_0284_482B_BF2C_A75C306DFCC7__INCLUDED_)
#define AFX_MOTOCOMDEMOVCPLUSPLUS_H__19947B1C_0284_482B_BF2C_A75C306DFCC7__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CMotocomDemoVCplusplusApp:
// See MotocomDemoVCplusplus.cpp for the implementation of this class
//

class CMotocomDemoVCplusplusApp : public CWinApp
{
public:
	CMotocomDemoVCplusplusApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CMotocomDemoVCplusplusApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CMotocomDemoVCplusplusApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_MOTOCOMDEMOVCPLUSPLUS_H__19947B1C_0284_482B_BF2C_A75C306DFCC7__INCLUDED_)
