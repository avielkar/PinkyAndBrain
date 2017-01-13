#pragma once
#include "MOTOCOMES.h"

class clsCommand
{
public:
	clsCommand(void);
	~clsCommand(void);

	#define OK			0x0000			//正常処理

	//ハンドル
	HANDLE handle;

	#pragma region 実行コマンド
	CString esOpen();
	CString esClose();
    CString esGetAlarm();
    CString esGetAlarmEx();
	CString esGetAlarmHist();
	CString esGetAlarmHistEx();
	CString esGetStatus();
	CString esGetJobStatus();
	CString esGetConfiguration();
	CString esGetPosition();
	CString esGetDeviation();
	CString esGetTorque();
	CString esGetMonitoringTime();
	CString esGetSystemInfo();
	CString esReadIO1();
	CString esWriteIO1();
	CString esReadIO2();
	CString esWriteIO2();
	CString esReadRegister();
	CString esWriteRegister();
	CString esReadIOM();
	CString esWriteIOM();
	CString esReadRegisterM();
	CString esWriteRegisterM();
	CString esGetVarData1();
	CString esSetVarData1();
	CString esGetVarData2();
	CString esSetVarData2();
	CString esGetStrData();
	CString esSetStrData();
	CString esGetPositionData();
	CString esSetPositionData();
	CString esGetBpexPositionData();
	CString esSetBpexPositionData();
	CString esGetVarDataMB();
	CString esSetVarDataMB();
	CString esGetVarDataMI();
	CString esSetVarDataMI();
	CString esGetVarDataMD();
	CString esSetVarDataMD();
	CString esGetVarDataMR();
	CString esSetVarDataMR();
	CString esGetStrDataM();
	CString esSetStrDataM();
	CString esGetPositionDataM();
	CString esSetPositionDataM();
	CString esGetBpexPositionDataM();
	CString esSetBpexPositionDataM();
	CString esReset();
	CString esCancel();
	CString esHold();
	CString esServo();
	CString esHlock();
	CString esCycle();
	CString esBDSP();
	CString esStartJob();
	CString esSelectJob();
	CString esSaveFile();
	CString esLoadFile();
	CString esDeleteJob();
	long esFileListFirst(CString *res);
	long esFileListNext(CString *res);

	CString esFileList();
	CString esCartMove();
	CString esPulseMove();
	#pragma endregion
};
