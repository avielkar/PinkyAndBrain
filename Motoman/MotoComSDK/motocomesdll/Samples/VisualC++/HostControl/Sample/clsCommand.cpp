#include "StdAfx.h"
#include "clsCommand.h"

clsCommand::clsCommand(void)
{
}

clsCommand::~clsCommand(void)
{
}

CString clsCommand::esOpen()
{
	CString res = "";
	long result = OK;

	res.Append("ESOpen\r\n");
	result = ESOpen(1, "192.168.255.1", &handle);
	res.AppendFormat("res\t%X\r\n", result);
	res.AppendFormat("handle\t%d\r\n", handle);
	res.Append("\r\n");

	return res;
}

CString clsCommand::esClose()
{
	CString res = "";
	long result = OK;

	res.Append("ESClose\r\n");
	result = ESClose(handle);
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	return res;
}

CString clsCommand::esGetAlarm()
{
	CString res = "";
	long result = OK;

	//引数となる構造体の定義
	ESAlarmList alarmList;
	memset(&alarmList, 0, sizeof(ESAlarmList));

	//関数実行
	res.Append("ESGetAlarm\r\n");
	result = ESGetAlarm(handle, &alarmList);
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	//結果表示
	if (result == OK)
	{
		for (int i = 0; i < LENGTH_OF_ALARMLIST; i++)
		{
			res.AppendFormat("Alarm Cord\t%d\r\n", alarmList.data[i].alarmCode);
			res.AppendFormat("Alarm Data\t%d\r\n", alarmList.data[i].alarmData);
			res.AppendFormat("Alarm Type\t%d\r\n", alarmList.data[i].alarmType);
			res.AppendFormat("Alarm Time\t%s\r\n", alarmList.data[i].alarmTime);
			res.AppendFormat("Alarm Name\t%s\r\n", alarmList.data[i].alarmName);
			res.Append("\r\n");
		}
	}
	return res;
}

CString clsCommand::esGetAlarmEx()
{
	CString res = "";
	long result = OK;

	//引数となる構造体の定義
	ESAlarmListEx alarmListEx;
	memset(&alarmListEx, 0, sizeof(ESAlarmListEx));

	//関数実行
	res.Append("ESGetAlarmEx\r\n");
	result = ESGetAlarmEx(handle, &alarmListEx);
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	//結果表示
	if (result == OK)
	{
		for (int i = 0; i < LENGTH_OF_ALARMLIST; i++)
		{
			res.AppendFormat("Alarm Cord\t%d\r\n", alarmListEx.data[i].alarmData.alarmCode);
			res.AppendFormat("Alarm Data\t%d\r\n", alarmListEx.data[i].alarmData.alarmData);
			res.AppendFormat("Alarm Type\t%d\r\n", alarmListEx.data[i].alarmData.alarmType);
			res.AppendFormat("Alarm Time\t%s\r\n", alarmListEx.data[i].alarmData.alarmTime);
			res.AppendFormat("Alarm Name\t%s\r\n", alarmListEx.data[i].alarmData.alarmName);
			res.AppendFormat("Alarm SubCode AddInfo\t%s\r\n", alarmListEx.data[ i ].subcodeData.alarmAddInfo);
			res.AppendFormat("Alarm SubCode StrData\t%s\r\n", alarmListEx.data[ i ].subcodeData.alarmStrData);
			res.AppendFormat("Alarm SubCode HighlightData\t%s\r\n", alarmListEx.data[ i ].subcodeData.alarmHighlightData);
			res.Append("\r\n");
		}
	}
	return res;
}

CString clsCommand::esGetAlarmHist()
{
	CString res = "";
	long result = OK;

	//引数となる構造体の定義
	ESAlarmData alarmData;
	memset(&alarmData, 0, sizeof(ESAlarmData));

	//関数実行
	res.Append("ESGetAlarmHist\r\n");
	result = ESGetAlarmHist(handle, 1001, &alarmData);
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	//結果表示
	if (result == OK)
	{
		res.AppendFormat("Alarm Cord\t%d\r\n", alarmData.alarmCode);
		res.AppendFormat("Alarm Data\t%d\r\n", alarmData.alarmData);
		res.AppendFormat("Alarm Type\t%d\r\n", alarmData.alarmType);
		res.AppendFormat("Alarm Time\t%s\r\n", alarmData.alarmTime);
		res.AppendFormat("Alarm Name\t%s\r\n", alarmData.alarmName);
		res.Append("\r\n");
	}
	return res;
}

CString clsCommand::esGetAlarmHistEx()
{
	CString res = "";
	long result = OK;

	//引数となる構造体の定義
	ESAlarmDataEx alarmDataEx;
	memset(&alarmDataEx, 0, sizeof(ESAlarmDataEx));

	//関数実行
	res.Append("ESGetAlarmHist\r\n");
	result = ESGetAlarmHistEx(handle, 1001, &alarmDataEx);
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	//結果表示
	if (result == OK)
	{
		res.AppendFormat("Alarm Cord\t%d\r\n", alarmDataEx.alarmData.alarmCode);
		res.AppendFormat("Alarm Data\t%d\r\n", alarmDataEx.alarmData.alarmData);
		res.AppendFormat("Alarm Type\t%d\r\n", alarmDataEx.alarmData.alarmType);
		res.AppendFormat("Alarm Time\t%s\r\n", alarmDataEx.alarmData.alarmTime);
		res.AppendFormat("Alarm Name\t%s\r\n", alarmDataEx.alarmData.alarmName);
		res.AppendFormat("Alarm SubCode AddInfo\t%s\r\n", alarmDataEx.subcodeData.alarmAddInfo);
		res.AppendFormat("Alarm SubCode StrData\t%s\r\n", alarmDataEx.subcodeData.alarmStrData);
		res.AppendFormat("Alarm SubCode HighlightData\t%s\r\n", alarmDataEx.subcodeData.alarmHighlightData);
		res.Append("\r\n");
	}
	return res;
}

CString clsCommand::esGetStatus()
{
	CString res = "";
	long result = OK;

	//引数となる構造体の定義
	ESStatusData statusData;
	memset(&statusData, 0, sizeof(ESStatusData));

	//関数実行
	res.Append("ESGetStatus\r\n");
	result = ESGetStatus(handle, &statusData);
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	//結果表示
	if (result == OK)
	{
		res.AppendFormat("status1\t%d%d%d%d%d%d%d%d\r\n",
			(statusData.status1&128)/128, (statusData.status1&64)/64, (statusData.status1&32)/32, (statusData.status1&16)/16,
			(statusData.status1&8)/8, (statusData.status1&4)/4, (statusData.status1&2)/2, (statusData.status1&1)/1);
		res.AppendFormat("status2\t%d%d%d%d%d%d%d%d\r\n",
			(statusData.status2&128)/128, (statusData.status2&64)/64, (statusData.status2&32)/32, (statusData.status2&16)/16,
			(statusData.status2&8)/8, (statusData.status2&4)/4, (statusData.status2&2)/2, (statusData.status2&1)/1);
		res.Append("\r\n");
	}
	return res;
}

CString clsCommand::esGetJobStatus()
{
	CString res = "";
	long result = OK;

	//引数となる構造体の定義
	ESJobStatusData jobData;
	memset(&jobData, 0, sizeof(ESJobStatusData));

	//関数実行
	res.Append("ESGetJobStatus\r\n");
	result = ESGetJobStatus(handle, 1, &jobData);
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	//結果表示
	if (result == OK)
	{
		res.AppendFormat("JobName\t%s\r\n",jobData.jobName);
		res.AppendFormat("LineNo\t%d\r\n",jobData.lineNo);
		res.AppendFormat("StepNo\t%d\r\n",jobData.stepNo);
		res.AppendFormat("SpeedOverride\t%d\r\n",jobData.speedOverride);
		res.Append("\r\n");
	}
	return res;
}

CString clsCommand::esGetConfiguration()
{
	CString res = "";
	long result = OK;

	//引数となる構造体の定義
	ESConfigurationData configData;
	memset(&configData, 0, sizeof(ESConfigurationData));

	//関数実行
	res.Append("ESGetConfiguration\r\n");
	result = ESGetConfiguration(handle, 1, &configData);
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	//結果表示
	if (result == OK)
	{
		for (int i = 0; i < NUMBER_OF_AXIS; i++)
		{
			res.AppendFormat("Axis%d\t%s\r\n",i+1,configData.configurations[i]);
			res.Append("\r\n");
		}
	}

	return res;
}

CString clsCommand::esGetPosition()
{
	CString res = "";
	long result = OK;

	//引数となる構造体の定義
	ESPositionData positionData;
	memset(&positionData, 0, sizeof(ESPositionData));

	//関数実行
	res.Append("ESGetPosition\r\n");
	result = ESGetPosition(handle, 1, &positionData);
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	//結果表示
	if (result == OK)
	{
		res.AppendFormat("Data Type\t%d\r\n", positionData.dataType);
		res.AppendFormat("Fig\t%d\r\n", positionData.fig);
		res.AppendFormat("Tool No\t%d\r\n", positionData.toolNo);
		res.AppendFormat("UserFrame No\t%d\r\n", positionData.userFrameNo);
		res.AppendFormat("exFig\t%d\r\n", positionData.exFig);
		for (int i = 0; i < NUMBER_OF_AXIS; i++)
		{
			res.AppendFormat("Axis%d\t%lf\r\n",i+1,positionData.axesData.axis[i]);
			res.Append("\r\n");
		}
	}

	return res;
}

CString clsCommand::esGetDeviation()
{
	CString res = "";
	long result = OK;

	//引数となる構造体の定義
	ESAxisData axisData;
	memset(&axisData, 0, sizeof(ESAxisData));

	//関数実行
	res.Append("ESGetDeviation\r\n");
	result = ESGetDeviation(handle, 1, &axisData);
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	//結果表示
	if (result == OK)
	{
		for (int i = 0; i < NUMBER_OF_AXIS; i++)
		{
			res.AppendFormat("Axis%d\t%lf\r\n",i+1,axisData.axis[i]);
			res.Append("\r\n");
		}
	}

	return res;
}

CString clsCommand::esGetTorque()
{
	CString res = "";
	long result = OK;

	//引数となる構造体の定義
	ESAxisData axisData;
	memset(&axisData, 0, sizeof(ESAxisData));

	//関数実行
	res.Append("ESGetTorque\r\n");
	result = ESGetTorque(handle, 1, &axisData);
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	//結果表示
	if (result == OK)
	{
		for (int i = 0; i < NUMBER_OF_AXIS; i++)
		{
			res.AppendFormat("Axis%d\t%lf\r\n",i+1,axisData.axis[i]);
			res.Append("\r\n");
		}
	}

	return res;
}

CString clsCommand::esGetMonitoringTime()
{
	CString res = "";
	long result = OK;

	//引数となる構造体の定義
	ESMonitoringTimeData timeData;
	memset(&timeData, 0, sizeof(ESMonitoringTimeData));

	//関数実行
	res.Append("ESGetMonitoringTime\r\n");
	result = ESGetMonitoringTime(handle, 1, &timeData);
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	//結果表示
	if (result == OK)
	{
		res.AppendFormat("Start Time\t%s\r\n", timeData.startTime);
		res.AppendFormat("Elapse Time\t%s\r\n", timeData.elapseTime);
		res.Append("\r\n");
	}

	return res;
}

CString clsCommand::esGetSystemInfo()
{
	CString res = "";
	long result = OK;

	//引数となる構造体の定義
	ESSystemInfoData infoData;
	memset(&infoData, 0, sizeof(ESSystemInfoData));

	//関数実行
	res.Append("ESGetSystemInfo\r\n");
	result = ESGetSystemInfo(handle, 11, &infoData);
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	//結果表示
	if (result == OK)
	{
		res.AppendFormat("System Version\t%s\r\n", infoData.systemVersion);
		res.AppendFormat("Name\t%s\r\n", infoData.name);
		res.AppendFormat("Parameter No\t%s\r\n", infoData.parameterNo);
		res.Append("\r\n");
	}

	return res;
}

CString clsCommand::esReadIO1()
{
	CString res = "";
	long result = OK;

	//引数となる変数の定義
	short ioData = 0;

	//関数実行
	res.Append("ESReadIO1\r\n");
	result = ESReadIO1(handle, 2501, &ioData);
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	//結果表示
	if (result == OK)
	{
		res.AppendFormat("#2501X\t%d\r\n", ioData);
		res.Append("\r\n");
	}

	return res;
}

CString clsCommand::esWriteIO1()
{
	CString res = "";
	long result = OK;

	//関数実行
	res.Append("ESWriteIO1\r\n");
	result = ESWriteIO1(handle, 2501, (short)255);
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	return res;
}

CString clsCommand::esReadIO2()
{
	CString res = "";
	long result = OK;

	//引数となる変数の定義
	short ioData = 0;

	//関数実行
	res.Append("ESReadIO2\r\n");
	result = ESReadIO2(handle, 2501, &ioData);
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	//結果表示
	if (result == OK)
	{
		res.AppendFormat("#2501X\t%d\r\n", ioData);
		res.Append("\r\n");
	}

	return res;
}

CString clsCommand::esWriteIO2()
{
	CString res = "";
	long result = OK;

	//関数実行
	res.Append("ESWriteIO2\r\n");
	result = ESWriteIO2(handle, 2501, (short)255);
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	return res;
}

CString clsCommand::esReadRegister()
{
	CString res = "";
	long result = OK;

	//引数となる変数の定義
	unsigned short regData = 0;

	//関数実行
	res.Append("ESReadRegister\r\n");
	result = ESReadRegister(handle, 0+1, &regData); //RS022=0
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	//結果表示
	if (result == OK)
	{
		res.AppendFormat("R(0)\t%d\r\n", regData);
		res.Append("\r\n");
	}

	return res;
}

CString clsCommand::esWriteRegister()
{
	CString res = "";
	long result = OK;

	//関数実行
	res.Append("ESWriteRegister\r\n");
	result = ESWriteRegister(handle, 0+1, 65535); //RS022=0
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	return res;
}

CString clsCommand::esReadIOM()
{
	CString res = "";
	long result = OK;

	//引数となる構造体の定義
	ESMultiByteData ioData;
	memset(&ioData, 0, sizeof(ESMultiByteData));

	//関数実行
	res.Append("ESReadIOM\r\n");
	result = ESReadIOM(handle, 2501, 2, &ioData);
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	//結果表示
	if (result == OK)
	{
		for(int i = 0; i < 2; i++)
		{
			res.AppendFormat("#250%dX\t%d\r\n",i, (int)ioData.data[i]);
		}
	}

	return res;
}

CString clsCommand::esWriteIOM()
{
	CString res = "";
	long result = OK;

	//引数となる構造体の定義
	ESMultiByteData ioData;
	memset(&ioData, 0, sizeof(ESMultiByteData));
	ioData.data[0] = 1;
	ioData.data[1] = 2;

	//関数実行
	res.Append("ESWriteIOM\r\n");
	result = ESWriteIOM(handle, 2501, 2, ioData);
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	return res;
}

CString clsCommand::esReadRegisterM()
{
	CString res = "";
	long result = OK;

	//引数となる構造体の定義
	ESMultiUShortData regData;
	memset(&regData, 0, sizeof(ESMultiUShortData));

	//関数実行
	res.Append("ESReadRegisterM\r\n");
	result = ESReadRegisterM(handle, 0+1, 2, &regData); //RS022=0
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	//結果表示
	if (result == OK)
	{
		for(int i = 0; i < 2; i++)
		{
			res.AppendFormat("R(%d)\t%d\r\n",i, regData.data[i]);
		}
	}

	return res;
}

CString clsCommand::esWriteRegisterM()
{
	CString res = "";
	long result = OK;

	//引数となる構造体の定義
	ESMultiUShortData regData;
	memset(&regData, 0, sizeof(ESMultiUShortData));
	regData.data[0] = 1;
	regData.data[1] = 65535;

	//関数実行
	res.Append("ESWriteRegisterM\r\n");
	result = ESWriteRegisterM(handle, 0+1, 2, regData); //RS022=0
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	return res;
}

CString clsCommand::esGetVarData1()
{
	CString res = "";
	long result = OK;

	//引数となる変数の定義
	double data = 0;

	//関数実行
	res.Append("ESGetVarData1\r\n");
	result = ESGetVarData1(handle, 1, 0+1, &data); //RS022=0, B変数
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	//結果表示
	if (result == OK)
	{
		res.AppendFormat("B(0)\t%lf\r\n", data);
		res.Append("\r\n");
	}

	return res;
}

CString clsCommand::esSetVarData1()
{
	CString res = "";
	long result = OK;

	//関数実行
	res.Append("ESSetVarData1\r\n");
	result = ESSetVarData1(handle, 1, 0+1, 1); //RS022=0, B変数
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	return res;
}

CString clsCommand::esGetVarData2()
{
	CString res = "";
	long result = OK;
	//引数となる変数の定義
	double data = 0;

	//関数実行
	res.Append("ESGetVarData2\r\n");
	result = ESGetVarData2(handle, 1, 0+1, &data); //RS022=0, B変数
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	//結果表示
	if (result == OK)
	{
		res.AppendFormat("B(0)\t%d\r\n", data);
		res.Append("\r\n");
	}

	return res;
}

CString clsCommand::esSetVarData2()
{
	CString res = "";
	long result = OK;

	//関数実行
	res.Append("ESSetVarData2\r\n");
	result = ESSetVarData2(handle, 1, 0+1, 1); //RS022=0, B変数
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	return res;
}

CString clsCommand::esGetStrData()
{
	CString res = "";
	long result = OK;

	//引数となる変数の定義
	char data[LENGTH_OF_STRING + 1];

	//関数実行
	res.Append("ESGetStrData\r\n");
	result = ESGetStrData(handle, 0+1, data); //RS022=0
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	//結果表示
	if (result == OK)
	{
		res.AppendFormat("S(0)\t%s\r\n", data);
		res.Append("\r\n");
	}

	return res;
}

CString clsCommand::esSetStrData()
{
	CString res = "";
	long result = OK;

	//関数実行
	res.Append("ESSetStrData\r\n");
	result = ESSetStrData(handle, 0+1, "test"); //RS022=0
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	return res;
}

CString clsCommand::esGetPositionData()
{
	CString res = "";
	long result = OK;

	//引数となる構造体の定義
	ESPositionData positionData;
	memset(&positionData, 0, sizeof(ESPositionData));

	//関数実行
	res.Append("ESGetPositionData\r\n");
	result = ESGetPositionData(handle, 0+1, &positionData); //RS022=0
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	//結果表示
	if (result == OK)
	{
		res.AppendFormat("Data Type\t%d\r\n", positionData.dataType);
		res.AppendFormat("Fig\t%d\r\n", positionData.fig);
		res.AppendFormat("Tool No\t%d\r\n", positionData.toolNo);
		res.AppendFormat("UserFrame No\t%d\r\n", positionData.userFrameNo);
		res.AppendFormat("exFig\t%d\r\n", positionData.exFig);
		for (int i = 0; i < NUMBER_OF_AXIS; i++)
		{
			res.AppendFormat("Axis%d\t%lf\r\n",i+1,positionData.axesData.axis[i]);
		}
	}

	return res;
}

CString clsCommand::esSetPositionData()
{
	CString res = "";
	long result = OK;

	//引数となる構造体の定義
	ESPositionData positionData;
	memset(&positionData, 0, sizeof(ESPositionData));
	positionData.dataType = 16; //ベース座標値
	for (int i = 0; i < 6; i++)
	{
		positionData.axesData.axis[i] = i + 1;
	}

	//関数実行
	res.Append("ESSetPositionData\r\n");
	result = ESSetPositionData(handle, 0+1, positionData); //RS022=0
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	return res;
}

CString clsCommand::esGetBpexPositionData()
{
	CString res = "";
	long result = OK;

	//引数となる構造体の定義
	ESBpexPositionData positionData;
	memset(&positionData, 0, sizeof(ESBpexPositionData));

	//関数実行
	res.Append("ESGetBpexPositionData\r\n");
	result = ESGetBpexPositionData(handle, 1, 0+1, &positionData); //RS022=0, BP変数
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	//結果表示
	if (result == OK)
	{
		res.AppendFormat("Data Type\t%d\r\n", positionData.dataType);
		for (int i = 0; i < NUMBER_OF_AXIS; i++)
		{
			res.AppendFormat("Axis%d\t%lf\r\n",i+1,positionData.axesData.axis[i]);
		}
	}

	return res;
}

CString clsCommand::esSetBpexPositionData()
{
	CString res = "";
	long result = OK;

	//引数となる構造体の定義
	ESBpexPositionData positionData;
	memset(&positionData, 0, sizeof(ESBpexPositionData));
	positionData.dataType = 16; //ベース座標値
	for (int i = 0; i < 3; i++)
	{
		positionData.axesData.axis[i] = i + 1;
	}

	//関数実行
	res.Append("ESSetBpexPositionData\r\n");
	result = ESSetBpexPositionData(handle, 1, 0+1, positionData); //RS022=0, BP変数
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	return res;
}

CString clsCommand::esGetVarDataMB()
{
	CString res = "";
	long result = OK;

	//引数となる構造体の定義
	ESMultiByteData varData;
	memset(&varData, 0, sizeof(ESMultiByteData));

	//関数実行
	res.Append("ESGetVarDataMB\r\n");
	result = ESGetVarDataMB(handle, 0+1, 2, &varData); //RS022=0
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	//結果表示
	if (result == OK)
	{
		for(int i = 0; i < 2; i++)
		{
			res.AppendFormat("B(%d)\t%d\r\n",i, (int)(unsigned char)varData.data[i]);
		}
	}

	return res;
}

CString clsCommand::esSetVarDataMB()
{
	CString res = "";
	long result = OK;

	//引数となる構造体の定義
	ESMultiByteData varData;
	memset(&varData, 0, sizeof(ESMultiByteData));
	varData.data[0] = 1;
	varData.data[1] = (char)255;

	//関数実行
	res.Append("ESSetVarDataMB\r\n");
	result = ESSetVarDataMB(handle, 0+1, 2, varData); //RS022=0
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	return res;
}

CString clsCommand::esGetVarDataMI()
{
	CString res = "";
	long result = OK;

	//引数となる構造体の定義
	ESMultiShortData varData;
	memset(&varData, 0, sizeof(ESMultiShortData));

	//関数実行
	res.Append("ESGetVarDataMI\r\n");
	result = ESGetVarDataMI(handle, 0+1, 2, &varData); //RS022=0
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	//結果表示
	if (result == OK)
	{
		for(int i = 0; i < 2; i++)
		{
			res.AppendFormat("I(%d)\t%d\r\n",i, varData.data[i]);
		}
	}

	return res;
}

CString clsCommand::esSetVarDataMI()
{
	CString res = "";
	long result = OK;

	//引数となる構造体の定義
	ESMultiShortData varData;
	memset(&varData, 0, sizeof(ESMultiShortData));
	varData.data[0] = 1;
	varData.data[1] = -1;

	//関数実行
	res.Append("ESSetVarDataMI\r\n");
	result = ESSetVarDataMI(handle, 0+1, 2, varData); //RS022=0
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	return res;
}

CString clsCommand::esGetVarDataMD()
{
	CString res = "";
	long result = OK;

	//引数となる構造体の定義
	ESMultiLongData varData;
	memset(&varData, 0, sizeof(ESMultiLongData));

	//関数実行
	res.Append("ESGetVarDataMD\r\n");
	result = ESGetVarDataMD(handle, 0+1, 2, &varData); //RS022=0
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	//結果表示
	if (result == OK)
	{
		for(int i = 0; i < 2; i++)
		{
			res.AppendFormat("D(%d)\t%d\r\n",i, varData.data[i]);
		}
	}

	return res;
}

CString clsCommand::esSetVarDataMD()
{
	CString res = "";
	long result = OK;

	//引数となる構造体の定義
	ESMultiLongData varData;
	memset(&varData, 0, sizeof(ESMultiLongData));
	varData.data[0] = 1;
	varData.data[1] = -1;

	//関数実行
	res.Append("ESSetVarDataMD\r\n");
	result = ESSetVarDataMD(handle, 0+1, 2, varData); //RS022=0
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	return res;
}

CString clsCommand::esGetVarDataMR()
{
	CString res = "";
	long result = OK;

	//引数となる構造体の定義
	ESMultiRealData varData;
	memset(&varData, 0, sizeof(ESMultiRealData));

	//関数実行
	res.Append("ESGetVarDataMR\r\n");
	result = ESGetVarDataMR(handle, 0+1, 2, &varData); //RS022=0
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	//結果表示
	if (result == OK)
	{
		for(int i = 0; i < 2; i++)
		{
			res.AppendFormat("R(%d)\t%lf\r\n",i, varData.data[i]);
		}
	}

	return res;
}

CString clsCommand::esSetVarDataMR()
{
	CString res = "";
	long result = OK;

	//引数となる構造体の定義
	ESMultiRealData varData;
	memset(&varData, 0, sizeof(ESMultiRealData));
	varData.data[0] = 1.1;
	varData.data[1] = -1.1;

	//関数実行
	res.Append("ESSetVarDataMR\r\n");
	result = ESSetVarDataMR(handle, 0+1, 2, varData); //RS022=0
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	return res;
}

CString clsCommand::esGetStrDataM()
{
	CString res = "";
	long result = OK;

	//引数となる構造体の定義
	ESMultiStrData varData;
	memset(&varData, 0, sizeof(ESMultiStrData));

	//関数実行
	res.Append("ESGetStrDataM\r\n");
	result = ESGetStrDataM(handle, 0+1, 2, &varData); //RS022=0
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	//結果表示
	if (result == OK)
	{
		for(int i = 0; i < 2; i++)
		{
			res.AppendFormat("S(%d)\t%s\r\n",i, varData.data[i]);
		}
	}

	return res;
}

CString clsCommand::esSetStrDataM()
{
	CString res = "";
	long result = OK;

	//引数となる構造体の定義
	ESMultiStrData varData;
	memset(&varData, 0, sizeof(ESMultiStrData));
	strcpy_s(varData.data[0], LENGTH_OF_STRING, "test1");
	strcpy_s(varData.data[1], LENGTH_OF_STRING, "test2");

	//関数実行
	res.Append("ESSetStrDataM\r\n");
	result = ESSetStrDataM(handle, 0+1, 2, varData); //RS022=0
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	return res;
}

CString clsCommand::esGetPositionDataM()
{
	CString res = "";
	long result = OK;

	//引数となる構造体の定義
	ESMultiPositionData positionData;
	memset(&positionData, 0, sizeof(ESMultiPositionData));

	//関数実行
	res.Append("ESGetPositionDataM\r\n");
	result = ESGetPositionDataM(handle, 0+1, 2, &positionData); //RS022=0
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	//結果表示
	if (result == OK)
	{
		for(int i = 0; i < 2; i++)
		{
			res.AppendFormat("Data Type\t%d\r\n", positionData.data[i].dataType);
			res.AppendFormat("Fig\t%d\r\n", positionData.data[i].fig);
			res.AppendFormat("Tool No\t%d\r\n", positionData.data[i].toolNo);
			res.AppendFormat("UserFrame No\t%d\r\n", positionData.data[i].userFrameNo);
			res.AppendFormat("exFig\t%d\r\n", positionData.data[i].exFig);
			for (int j = 0; j < NUMBER_OF_AXIS; j++)
			{
				res.AppendFormat("Axis%d\t%lf\r\n",j+1,positionData.data[i].axesData.axis[j]);
			}
		}
	}

	return res;
}

CString clsCommand::esSetPositionDataM()
{
	CString res = "";
	long result = OK;

	//引数となる構造体の定義
	ESMultiPositionData positionData;
	memset(&positionData, 0, sizeof(ESMultiPositionData));
	for (int i = 0; i < 2; i++)
	{
		positionData.data[i].dataType = 16; //ベース座標値
		for (int j = 0; j < 6; j++)
		{
			positionData.data[i].axesData.axis[j] = (i + 1) * 10 + j + 1;
		}
	}

	//関数実行
	res.Append("ESSetPositionDataM\r\n");
	result = ESSetPositionDataM(handle, 0+1, 2, positionData); //RS022=0
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	return res;
}

CString clsCommand::esGetBpexPositionDataM()
{
	CString res = "";
	long result = OK;

	//引数となる構造体の定義
	ESMultiBpexPositionData positionData;
	memset(&positionData, 0, sizeof(ESMultiBpexPositionData));

	//関数実行
	res.Append("ESGetBpexPositionDataM\r\n");
	result = ESGetBpexPositionDataM(handle, 1, 0+1, 2, &positionData); //RS022=0, BP変数
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	//結果表示
	if (result == OK)
	{
		for(int i = 0; i < 2; i++)
		{
			res.AppendFormat("Data Type\t%d\r\n", positionData.data[i].dataType);
			for (int j = 0; j < NUMBER_OF_AXIS; j++)
			{
				res.AppendFormat("Axis%d\t%lf\r\n",j+1,positionData.data[i].axesData.axis[j]);
			}
		}
	}

	return res;
}

CString clsCommand::esSetBpexPositionDataM()
{
	CString res = "";
	long result = OK;

	//引数となる構造体の定義
	ESMultiBpexPositionData positionData;
	memset(&positionData, 0, sizeof(ESMultiBpexPositionData));
	for (int i = 0; i < 2; i++)
	{
		positionData.data[i].dataType = 16; //ベース座標値
		for (int j = 0; j < 3; j++)
		{
			positionData.data[i].axesData.axis[j] = (i + 1) * 10 + j + 1;
		}
	}

	//関数実行
	res.Append("ESSetBpexPositionDataM\r\n");
	result = ESSetBpexPositionDataM(handle, 1, 0+1, 2, positionData); //RS022=0
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	return res;
}

CString clsCommand::esReset()
{
	CString res = "";
	long result = OK;

	//関数実行
	res.Append("ESReset\r\n");
	result = ESReset(handle);
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	return res;
}

CString clsCommand::esCancel()
{
	CString res = "";
	long result = OK;

	//関数実行
	res.Append("ESCancel\r\n");
	result = ESCancel(handle);
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	return res;
}

CString clsCommand::esHold()
{
	CString res = "";
	long result = OK;

	//関数実行
	res.Append("ESHold\r\n");
	result = ESHold(handle, 1); //Hold ON
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	return res;
}

CString clsCommand::esServo()
{
	CString res = "";
	long result = OK;

	//関数実行
	res.Append("ESServo\r\n");
	result = ESServo(handle, 1); //Servo ON
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	return res;
}

CString clsCommand::esHlock()
{
	CString res = "";
	long result = OK;

	//関数実行
	res.Append("ESHlock\r\n");
	result = ESHlock(handle, 1); //Hlock ON
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	return res;
}

CString clsCommand::esCycle()
{
	CString res = "";
	long result = OK;

	//関数実行
	res.Append("ESCycle\r\n");
	result = ESCycle(handle, 1); //Cycle Step
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	return res;
}

CString clsCommand::esBDSP()
{
	CString res = "";
	long result = OK;

	//関数実行
	res.Append("ESBDSP\r\n");
	result = ESBDSP(handle, "test");
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	return res;
}

CString clsCommand::esStartJob()
{
	CString res = "";
	long result = OK;

	//関数実行
	res.Append("ESStartJob\r\n");
	result = ESStartJob(handle);
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	return res;
}

CString clsCommand::esSelectJob()
{
	CString res = "";
	long result = OK;

	//関数実行
	res.Append("ESSelectJob\r\n");
	result = ESSelectJob(handle, 1, 1, "TEST");
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	return res;
}

CString clsCommand::esSaveFile()
{
	CString res = "";
	long result = OK;

	//引数となる変数の定義
	CString savePath = "C:\\TEMP";
	CString fileName = "TEST.JBI";

	//関数実行
	res.Append("ESFileSave\r\n");
	result = ESSaveFile(handle, savePath.GetBuffer(), fileName.GetBuffer());
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	return res;
}

CString clsCommand::esLoadFile()
{
	CString res = "";
	long result = OK;

	//引数となる変数の定義
	CString filePath = "C:\\TEMP\\TEST.JBI";

	//関数実行
	res.Append("ESFileLoad\r\n");
	result = ESLoadFile(handle, filePath.GetBuffer());
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	return res;
}

CString clsCommand::esDeleteJob()
{
	CString res = "";
	long result = OK;

	//引数となる変数の定義
	CString fileName = "TEST.JBI";

	//関数実行
	res.Append("ESDeleteJob\r\n");
	result = ESDeleteJob(handle, fileName.GetBuffer());
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	return res;
}

long clsCommand::esFileListFirst(CString *res)
{
	long result = OK;

	//引数となる変数定義
	char fileName[LENGTH_OF_NAME + 5];

	//関数実行
	res->Append("ESFileListFirst\r\n");
	result = ESFileListFirst(handle, 1, &fileName[0]);
	res->AppendFormat("res\t%X\r\n", result);
	if (result == OK)
	{
		res->AppendFormat("%s\r\n",fileName);
	}
	res->Append("\r\n");

	return result;
}

long clsCommand::esFileListNext(CString *res)
{
	long result = OK;

	//引数となる変数定義
	char fileName[LENGTH_OF_NAME + 5];

	//関数実行
	result = ESFileListNext(handle, &fileName[0]);
	if (result == OK)
	{
		res->Append("ESFileListNext\r\n");
		res->AppendFormat("res\t%X\r\n", result);
		res->AppendFormat("%s\r\n",fileName);
		res->Append("\r\n");
	}

	return result;
}

CString clsCommand::esFileList()
{
	CString res = "";
	int result = OK;

	//esFileListFirst
	result = esFileListFirst(&res);
	if (result != OK)
	{
		return res;
	}

	//esFileListNext
	for (;;)
	{
		result = esFileListNext(&res);

		if(result != OK)
		{
			return res;
		}
	}
}

CString clsCommand::esCartMove()
{
	CString res = "";
	long result = OK;

	//引数となる構造体の定義
	long moveType;
	ESCartMoveData moveData;
	memset(&moveData, 0, sizeof(ESCartMoveData));

	moveType = 1;						// MOVJ
	moveData.moveData.robotNo = 1;
	moveData.moveData.stationNo = 0;
	moveData.moveData.speedType = 0;	// %
	moveData.moveData.speed = 25;
	moveData.robotPos.dataType = 16;	// ベース座標
	moveData.robotPos.fig = 0;
	moveData.robotPos.toolNo = 0;
	moveData.robotPos.userFrameNo = 0;
	moveData.robotPos.exFig = 0;
	for(int i = 0; i < 8; i++)
	{
		moveData.robotPos.axesData.axis[i] = i + 1;
	}
	moveData.robotPos.axesData.axis[6] = 0;
	moveData.robotPos.axesData.axis[7] = 0;
	for(int i = 0; i < 3; i++)
	{
		moveData.basePos.axis[i] = i + 1;
	}
	for(int i = 0; i < 6; i++)
	{
		moveData.stationPos.axis[i] = i + 1;
	}

	//関数実行
	res.Append("ESCartMove\r\n");
	result = ESCartMove(handle, moveType, moveData);
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	return res;
}

CString clsCommand::esPulseMove()
{
	CString res = "";
	long result = OK;

	//引数となる構造体の定義
	long moveType;
	ESPulseMoveData moveData;
	memset(&moveData, 0, sizeof(ESPulseMoveData));

	moveType = 2;						// MOVL

	moveData.moveData.robotNo = 0;
	moveData.moveData.stationNo = 1;
	moveData.moveData.speedType = 1;	// V
	moveData.moveData.speed = 4500;
	for(int i = 0; i < 8; i++)
	{
		moveData.robotPos.axis[i] = i + 1;
	}
	moveData.robotPos.axis[6] = 0;
	moveData.robotPos.axis[7] = 0;
	for(int i = 0; i < 3; i++)
	{
		moveData.basePos.axis[i] = i + 1;
	}
	for(int i = 0; i < 6; i++)
	{
		moveData.stationPos.axis[i] = i + 1;
	}
	moveData.toolNo = 0;

	//関数実行
	res.Append("ESPulseMove\r\n");
	result = ESPulseMove(handle, moveType, moveData);
	res.AppendFormat("res\t%X\r\n", result);
	res.Append("\r\n");

	return res;
}