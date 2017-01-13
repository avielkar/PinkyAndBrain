// MOTOCOMES.h : MOTOCOMES.DLL のメイン ヘッダー ファイル
//

#pragma once
#undef AFX_DATA
#define AFX_DATA AFX_EXT_DATA

#ifndef __AFXWIN_H__
	#error "PCH に対してこのファイルをインクルードする前に 'stdafx.h' をインクルードしてください"
#endif

#include "resource.h"		// メイン シンボル


// CMOTOCOMESApp
// このクラスの実装に関しては MOTOCOMES.cpp を参照してください。
//

#pragma pack(4)				//アライメント指定

class CMOTOCOMESApp : public CWinApp
{
public:
	CMOTOCOMESApp();

// オーバーライド
public:
	virtual BOOL InitInstance();

	DECLARE_MESSAGE_MAP()
};

//タイムアウト初期値
static const long	TIMEOUT					= 500;							//受信タイムアウト時間(msec)：初期値
static const long	RETRY					= 3;							//受信タイムアウトリトライ回数：初期値

/****************以下のサイズ・構造体の定義は変更しないでください*******************/
//サイズ定義
static const short	LENGTH_OF_TIME					= 16;							//時刻のデータ長(ex. 2007/05/10 15:49)
static const short	LENGTH_OF_NAME					= 32;							//文字列名称のデータ長(最大32文字)
static const short	LENGTH_OF_SUBCODE_ADDINFO		= 16;							//詳細データ追加情報文字列長(最大16文字)
static const short	LENGTH_OF_SUBCODE_STRINGDATA	= 96;							//詳細データ文字列長(最大96文字)
static const short	LENGTH_OF_ALARMLIST				= 4;							//アラームリスト長
static const short	NUMBER_OF_AXIS					= 8;							//ロボットの最大軸数
static const short	LENGTH_OF_CONFIGNAME			= 4;							//軸構成名称のデータ長
static const short	LENGTH_OF_ELAPSETIME			= 12;							//経過時間のデータ長
static const short	LENGTH_OF_SYSTEMVER				= 24;							//システムヴァージョンのデータ長
static const short	LENGTH_OF_ROBOTNAME				= 16;							//機種名称のデータ長
static const short	LENGTH_OF_PARAMNO				= 8;							//パラメータNo.のデータ長
static const short	NUMBER_OF_BASE_AXIS				= 3;							//走行軸の最大軸数
static const short	NUMBER_OF_STATION_AXIS			= 6;							//ステーション軸の最大軸数

static const short	LENGTH_OF_MULTI_1				= 474;							//1バイトサイズの複数データ最大数
static const short	LENGTH_OF_MULTI_2				= 237;							//2バイトサイズの複数データ最大数
static const short	LENGTH_OF_MULTI_4				= 118;							//4バイトサイズの複数データ最大数
static const short	LENGTH_OF_MULTI_STR				= 29;							//文字列の複数データ最大数
static const short	LENGTH_OF_MULTI_POS				= 9;							//位置データの複数データ最大数
static const short	LENGTH_OF_MULTI_BPEX			= 13;							//ベース軸位置・外部軸位置データの複数データ最大数
static const short	LENGTH_OF_STRING				= 16;							//文字列変数のデータ長

//アラームデータ
typedef struct {
		long alarmCode;														//アラームコード
		long alarmData;														//アラームデータ
		long alarmType;														//アラームデータ種別
		char alarmTime[LENGTH_OF_TIME+1];									//アラーム発生時刻
		char alarmName[LENGTH_OF_NAME+1];									//アラーム文字列名称
} ESAlarmData;

//アラームサブコードデータ
typedef struct {
		char  alarmAddInfo[LENGTH_OF_SUBCODE_ADDINFO+1];					//詳細データ追加情報文字列
		char  alarmStrData[LENGTH_OF_SUBCODE_STRINGDATA+1];					//詳細データ文字列
		char  alarmHighlightData[LENGTH_OF_SUBCODE_STRINGDATA+1];			//詳細データ反転表示情報
} ESSubcodeData;

//アラームデータ(サブコード文字列対応)
typedef struct {
		ESAlarmData	alarmData;												//アラームデータ
		ESSubcodeData subcodeData;											//サブコードデータ
} ESAlarmDataEx;

//アラームリスト
typedef struct {
	ESAlarmData data[LENGTH_OF_ALARMLIST];									//アラームデータ
} ESAlarmList;

//アラームリスト(サブコード文字列対応)
typedef struct {
	ESAlarmDataEx data[LENGTH_OF_ALARMLIST];								//アラームデータ(サブコード対応)
} ESAlarmListEx;

//ステータスデータ
typedef struct {
			long status1;													//ステータスデータ1
			long status2;													//ステータスデータ2
} ESStatusData;

//ジョブステータスデータ
typedef struct {
			char jobName[LENGTH_OF_NAME+1];									//ジョブ名称
			long lineNo;													//ライン番号
			long stepNo;													//ステップ番号
			long speedOverride;												//速度オーバーライド値
} ESJobStatusData;

//軸構成データ
typedef struct {
			char configurations[NUMBER_OF_AXIS][LENGTH_OF_CONFIGNAME+1];	//軸名称(SLURBT,XYZRxRyRz)
} ESConfigurationData;

//軸データ
typedef struct {
			double axis [NUMBER_OF_AXIS];									//軸データ
} ESAxisData;

//ロボット位置データ
typedef struct {
			long dataType;													//データタイプ(パルス値/直交値)
			long fig;														//形態
			long toolNo;													//ツール番号
			long userFrameNo;												//ユーザ座標番号
			long exFig;														//拡張形態
			ESAxisData axesData;											//軸データ
} ESPositionData;

//ベース位置/外部軸位置データ
typedef struct {
			long dataType;													//データタイプ(パルス値/直交値)
			ESAxisData axesData;											//軸データ
} ESBpexPositionData;

//管理時間データ
typedef struct {
			char startTime[LENGTH_OF_TIME+1];								//稼働開始時刻
			char elapseTime[LENGTH_OF_ELAPSETIME+1];						//経過時間
} ESMonitoringTimeData;

//システム情報データ
typedef struct {
			char systemVersion[LENGTH_OF_SYSTEMVER+1];						//システムソフトウェアバージョン
			char name[LENGTH_OF_ROBOTNAME+1];								//機種名称/用途名称
			char parameterNo[LENGTH_OF_PARAMNO+1];							//パラメータNo.
} ESSystemInfoData;

//移動情報データ
typedef struct
{
			long  	robotNo;												//ロボット番号
			long  	stationNo;												//ステーション番号
			long  	speedType;												//速度区分指定
			double  speed;													//速度指定
} ESMoveData;

//ロボットの目標位置データ（直交座標）
typedef ESPositionData ESCartPosData;										//ロボットの目標位置データ（直交値）

//ロボットの目標位置データ（パルス）
typedef ESAxisData ESPulsePosData;											//ロボットの目標位置データ（パルス値）

//ベースの目標位置データ
typedef struct {
			double axis[NUMBER_OF_BASE_AXIS];								//ベースの目標位置データ
} ESBaseData;

//ステーションの目標位置データ
typedef struct {
			double axis[NUMBER_OF_STATION_AXIS];							//ステーションの目標位置データ
} ESStationData;

//移動命令データ（直交座標）
typedef struct {
			ESMoveData		moveData;										//移動情報データ
			ESCartPosData	robotPos;										//ロボットの目標位置データ
			ESBaseData		basePos;										//ベースの目標位置データ
			ESStationData	stationPos;										//ステーションの目標位置データ
}ESCartMoveData;

//移動命令データ（パルス）
typedef struct {
			ESMoveData		moveData;										//移動情報データ
			ESPulsePosData	robotPos;										//ロボットの目標位置データ
			ESBaseData		basePos;										//ベースの目標位置データ
			ESStationData	stationPos;										//ステーションの目標位置データ
			long			toolNo;											//ツール番号
}ESPulseMoveData;

//1バイトの複数データ
typedef struct {
			char data[LENGTH_OF_MULTI_1];
}ESMultiByteData;

//2バイトの複数データ
typedef struct {
			short data[LENGTH_OF_MULTI_2];
}ESMultiShortData;

//2バイトの複数データ（Unsigned）
typedef struct {
			unsigned short data[LENGTH_OF_MULTI_2];
}ESMultiUShortData;

//LONGの複数データ
typedef struct {
			long data[LENGTH_OF_MULTI_4];
}ESMultiLongData;

//DOUBLEの複数データ
typedef struct {
			double data[LENGTH_OF_MULTI_4];
}ESMultiRealData;

//文字列の複数データ
typedef struct {
			char data[LENGTH_OF_MULTI_STR][LENGTH_OF_STRING+1];
}ESMultiStrData;

//ESPositionDataの複数データ
typedef struct {
			ESPositionData data[LENGTH_OF_MULTI_POS];
}ESMultiPositionData;

//ESBpexPositionDataの複数データ
typedef struct {
			ESBpexPositionData data[LENGTH_OF_MULTI_BPEX];
}ESMultiBpexPositionData;

#define STDCALL __stdcall

/*コマンド*/
//その他の関数
LONG	STDCALL	ESOpen(long controllerType, char *ipAddress, HANDLE *handle);						//接続
LONG	STDCALL	ESClose(HANDLE handle);																//切断
LONG	STDCALL ESSetTimeOut(HANDLE handle, long timeOut, long retry);								//タイムアウト・リトライ設定

//リード・監視系
LONG	STDCALL	ESGetAlarm(HANDLE handle, ESAlarmList *alarmList);									//発生中アラーム読み出し
LONG	STDCALL	ESGetAlarmHist(HANDLE handle, long alarmHistNo, ESAlarmData *alarmData);			//アラーム履歴読み出し
LONG	STDCALL	ESGetAlarmEx(HANDLE handle, ESAlarmListEx *alarmList);								//発生中アラーム読み出し(サブコード文字列対応)
LONG	STDCALL	ESGetAlarmHistEx(HANDLE handle, long alarmHistNo, ESAlarmDataEx *alarmData);		//アラーム履歴読み出し(サブコード文字列対応)
LONG	STDCALL	ESGetStatus(HANDLE handle, ESStatusData *statusData);								//ステータス読み出し
LONG	STDCALL	ESGetJobStatus(HANDLE handle, long taskNo, ESJobStatusData *jobStatusData);			//実行ジョブ情報読み出し
LONG	STDCALL	ESGetConfiguration(HANDLE handle, long ctrlGrp, ESConfigurationData *configData);	//軸構成読み出し
LONG	STDCALL	ESGetPosition(HANDLE handle, long ctrlGrp, ESPositionData *positionData);			//ロボット位置の読み出し
LONG	STDCALL	ESGetDeviation(HANDLE handle, long ctrlGrp, ESAxisData *deviationData);				//各軸位置偏差読み出し
LONG	STDCALL	ESGetTorque(HANDLE handle, long ctrlGrp, ESAxisData *torqueData);					//各軸トルクの読み出し
LONG	STDCALL ESGetMonitoringTime(HANDLE handle, long timeType, ESMonitoringTimeData *timeData);	//管理時間読み出し
LONG	STDCALL ESGetSystemInfo(HANDLE handle, long systemType, ESSystemInfoData *infoData);		//システム情報読み出し

//I/Oリード・ライト系
LONG	STDCALL	ESReadIO1(HANDLE handle, long ioNumber, short *ioData);								//IOデータの読み出し
LONG	STDCALL	ESWriteIO1(HANDLE handle, long ioNumber, short ioData);								//IOデータの書き込み
LONG	STDCALL	ESReadIO2(HANDLE handle, long ioNumber, short *ioData);								//IOデータの読み出し（1バイト入出力禁止仕様）
LONG	STDCALL	ESWriteIO2(HANDLE handle, long ioNumber, short ioData);								//IOデータの書き込み（1バイト入出力禁止仕様）
LONG	STDCALL	ESReadRegister(HANDLE handle, long regNumber, unsigned short *regData);				//レジスタデータの読み出し
LONG	STDCALL	ESWriteRegister(HANDLE handle, long regNumber, unsigned short regData);				//レジスタデータの書き込み
LONG	STDCALL	ESReadIOM(HANDLE handle, long ioNumber, long number, ESMultiByteData *ioData);		//IOデータの読み出し(Multi)
LONG	STDCALL	ESWriteIOM(HANDLE handle, long ioNumber, long number, ESMultiByteData ioData);		//IOデータの書き込み(Multi)
LONG	STDCALL	ESReadRegisterM(HANDLE handle, long regNumber, long number,							//レジスタデータの読み出し(Multi)
								ESMultiUShortData *regData);
LONG	STDCALL	ESWriteRegisterM(HANDLE handle, long regNumber, long number,						//レジスタデータの書き込み(Multi)
								 ESMultiUShortData regData);

//リード・データアクセス系＆編集系
LONG	STDCALL	ESGetVarData1(HANDLE handle, long type, long number, double *data);					//変数の読み出し
LONG	STDCALL	ESSetVarData1(HANDLE handle, long type, long number, double data);					//変数の書き込み
LONG	STDCALL	ESGetVarData2(HANDLE handle, long type, long number, double *data);					//変数の読み出し（1バイト入出力禁止仕様）
LONG	STDCALL	ESSetVarData2(HANDLE handle, long type, long number, double data);					//変数の書き込み（1バイト入出力禁止仕様）
LONG	STDCALL	ESGetStrData(HANDLE handle, long number, char *cp);									//文字型変数(S)の読み出し
LONG	STDCALL	ESSetStrData(HANDLE handle, long number, char *cp);									//文字型変数(S)の書き込み
LONG	STDCALL	ESGetPositionData(HANDLE handle, long number, ESPositionData *positionData);		//ロボット位置型変数(P)の読み出し
LONG	STDCALL	ESSetPositionData(HANDLE handle, long number, ESPositionData positionData);			//ロボット位置型変数(P)の書き込み
LONG	STDCALL	ESGetBpexPositionData(HANDLE handle, long type, long number,						//ベース位置型変数(BP)・外部軸位置型変数(EX)の読み出し
									  ESBpexPositionData *positionData);
LONG	STDCALL	ESSetBpexPositionData(HANDLE handle, long type, long number,						//ベース位置型変数(BP)・外部軸位置型変数(EX)の書き込み
									  ESBpexPositionData positionData);
LONG	STDCALL	ESGetVarDataMB(HANDLE handle, long varNo, long number, ESMultiByteData *varData);	//B変数の読み出し(Multi)
LONG	STDCALL	ESSetVarDataMB(HANDLE handle, long varNo, long number, ESMultiByteData varData);	//B変数の書き込み(Multi)
LONG	STDCALL	ESGetVarDataMI(HANDLE handle, long varNo, long number, ESMultiShortData *varData);	//I変数の読み出し(Multi)
LONG	STDCALL	ESSetVarDataMI(HANDLE handle, long varNo, long number, ESMultiShortData varData);	//I変数の書き込み(Multi)	
LONG	STDCALL	ESGetVarDataMD(HANDLE handle, long varNo, long number, ESMultiLongData *varData);	//D変数の読み出し(Multi)
LONG	STDCALL	ESSetVarDataMD(HANDLE handle, long varNo, long number, ESMultiLongData varData);	//D変数の書き込み(Multi)
LONG	STDCALL	ESGetVarDataMR(HANDLE handle, long varNo, long number, ESMultiRealData *varData);	//R変数の読み出し(Multi)
LONG	STDCALL	ESSetVarDataMR(HANDLE handle, long varNo, long number, ESMultiRealData varData);	//R変数の書き込み(Multi)
LONG	STDCALL	ESGetStrDataM(HANDLE handle, long varNo, long number, ESMultiStrData *varData);		//S変数の読み出し(Multi)
LONG	STDCALL	ESSetStrDataM(HANDLE handle, long varNo, long number, ESMultiStrData varData);		//S変数の書き込み(Multi)
LONG	STDCALL	ESGetPositionDataM(HANDLE handle, long varNo, long number,							//P変数の読み出し(Multi)
								   ESMultiPositionData *positionData);
LONG	STDCALL	ESSetPositionDataM(HANDLE handle, long varNo, long number,							//P変数の書き込み(Multi)
								   ESMultiPositionData positionData);
LONG	STDCALL	ESGetBpexPositionDataM(HANDLE handle, long type, long varNo, long number,			//BP変数・EX変数の読み出し(Multi)
									   ESMultiBpexPositionData *positionData);
LONG	STDCALL	ESSetBpexPositionDataM(HANDLE handle, long type, long varNo, long number,			//BP変数・EX変数の書き込み(Multi)
									   ESMultiBpexPositionData positionData);

//操作系
LONG	STDCALL	ESReset(HANDLE handle);																//リセット
LONG	STDCALL	ESCancel(HANDLE handle);															//キャンセル
LONG	STDCALL	ESHold(HANDLE handle, long onOff);													//ホールド
LONG	STDCALL	ESServo(HANDLE handle, long onOff);													//サーボオン
LONG	STDCALL	ESHlock(HANDLE handle, long onOff);													//PPとIOの操作系信号のインタロック
LONG	STDCALL	ESCycle(HANDLE handle, long cycle);													//ステップ/サイクル/連続自動
LONG	STDCALL	ESBDSP(HANDLE handle, char *message);												//ペンダントへの文字列表示

//起動系
LONG	STDCALL	ESStartJob(HANDLE handle);															//ジョブスタート
LONG	STDCALL ESCartMove(HANDLE handle, long moveType, ESCartMoveData moveData);					//移動命令（直交座標）
LONG	STDCALL ESPulseMove(HANDLE handle, long moveType, ESPulseMoveData moveData);				//移動命令（パルス）

//ジョブ選択系
LONG	STDCALL	ESSelectJob(HANDLE handle, long jobType, long lineNo, char *jobName);				//ジョブ選択

//ファイル制御系
LONG	STDCALL	ESSaveFile(HANDLE handle, char *savePath, char *fileName);							//ファイルセーブ
LONG	STDCALL	ESLoadFile(HANDLE handle, char *filePath);											//ファイルロード
LONG	STDCALL	ESDeleteJob(HANDLE handle, char *jobName);											//ジョブ削除
LONG	STDCALL	ESFileListFirst(HANDLE handle, long fileType, char *fileName);						//ファイルリストの更新し先頭を読み込む
LONG	STDCALL	ESFileListNext(HANDLE handle, char *fileName);										//ファイルリストを読み込む
#undef AFX_DATA
#define AFX_DATA
