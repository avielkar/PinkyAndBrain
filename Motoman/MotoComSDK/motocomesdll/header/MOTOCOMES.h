// MOTOCOMES.h : MOTOCOMES.DLL �̃��C�� �w�b�_�[ �t�@�C��
//

#pragma once
#undef AFX_DATA
#define AFX_DATA AFX_EXT_DATA

#ifndef __AFXWIN_H__
	#error "PCH �ɑ΂��Ă��̃t�@�C�����C���N���[�h����O�� 'stdafx.h' ���C���N���[�h���Ă�������"
#endif

#include "resource.h"		// ���C�� �V���{��


// CMOTOCOMESApp
// ���̃N���X�̎����Ɋւ��Ă� MOTOCOMES.cpp ���Q�Ƃ��Ă��������B
//

#pragma pack(4)				//�A���C�����g�w��

class CMOTOCOMESApp : public CWinApp
{
public:
	CMOTOCOMESApp();

// �I�[�o�[���C�h
public:
	virtual BOOL InitInstance();

	DECLARE_MESSAGE_MAP()
};

//�^�C���A�E�g�����l
static const long	TIMEOUT					= 500;							//��M�^�C���A�E�g����(msec)�F�����l
static const long	RETRY					= 3;							//��M�^�C���A�E�g���g���C�񐔁F�����l

/****************�ȉ��̃T�C�Y�E�\���̂̒�`�͕ύX���Ȃ��ł�������*******************/
//�T�C�Y��`
static const short	LENGTH_OF_TIME					= 16;							//�����̃f�[�^��(ex. 2007/05/10 15:49)
static const short	LENGTH_OF_NAME					= 32;							//�����񖼏̂̃f�[�^��(�ő�32����)
static const short	LENGTH_OF_SUBCODE_ADDINFO		= 16;							//�ڍ׃f�[�^�ǉ���񕶎���(�ő�16����)
static const short	LENGTH_OF_SUBCODE_STRINGDATA	= 96;							//�ڍ׃f�[�^������(�ő�96����)
static const short	LENGTH_OF_ALARMLIST				= 4;							//�A���[�����X�g��
static const short	NUMBER_OF_AXIS					= 8;							//���{�b�g�̍ő厲��
static const short	LENGTH_OF_CONFIGNAME			= 4;							//���\�����̂̃f�[�^��
static const short	LENGTH_OF_ELAPSETIME			= 12;							//�o�ߎ��Ԃ̃f�[�^��
static const short	LENGTH_OF_SYSTEMVER				= 24;							//�V�X�e�����@�[�W�����̃f�[�^��
static const short	LENGTH_OF_ROBOTNAME				= 16;							//�@�햼�̂̃f�[�^��
static const short	LENGTH_OF_PARAMNO				= 8;							//�p�����[�^No.�̃f�[�^��
static const short	NUMBER_OF_BASE_AXIS				= 3;							//���s���̍ő厲��
static const short	NUMBER_OF_STATION_AXIS			= 6;							//�X�e�[�V�������̍ő厲��

static const short	LENGTH_OF_MULTI_1				= 474;							//1�o�C�g�T�C�Y�̕����f�[�^�ő吔
static const short	LENGTH_OF_MULTI_2				= 237;							//2�o�C�g�T�C�Y�̕����f�[�^�ő吔
static const short	LENGTH_OF_MULTI_4				= 118;							//4�o�C�g�T�C�Y�̕����f�[�^�ő吔
static const short	LENGTH_OF_MULTI_STR				= 29;							//������̕����f�[�^�ő吔
static const short	LENGTH_OF_MULTI_POS				= 9;							//�ʒu�f�[�^�̕����f�[�^�ő吔
static const short	LENGTH_OF_MULTI_BPEX			= 13;							//�x�[�X���ʒu�E�O�����ʒu�f�[�^�̕����f�[�^�ő吔
static const short	LENGTH_OF_STRING				= 16;							//������ϐ��̃f�[�^��

//�A���[���f�[�^
typedef struct {
		long alarmCode;														//�A���[���R�[�h
		long alarmData;														//�A���[���f�[�^
		long alarmType;														//�A���[���f�[�^���
		char alarmTime[LENGTH_OF_TIME+1];									//�A���[����������
		char alarmName[LENGTH_OF_NAME+1];									//�A���[�������񖼏�
} ESAlarmData;

//�A���[���T�u�R�[�h�f�[�^
typedef struct {
		char  alarmAddInfo[LENGTH_OF_SUBCODE_ADDINFO+1];					//�ڍ׃f�[�^�ǉ���񕶎���
		char  alarmStrData[LENGTH_OF_SUBCODE_STRINGDATA+1];					//�ڍ׃f�[�^������
		char  alarmHighlightData[LENGTH_OF_SUBCODE_STRINGDATA+1];			//�ڍ׃f�[�^���]�\�����
} ESSubcodeData;

//�A���[���f�[�^(�T�u�R�[�h������Ή�)
typedef struct {
		ESAlarmData	alarmData;												//�A���[���f�[�^
		ESSubcodeData subcodeData;											//�T�u�R�[�h�f�[�^
} ESAlarmDataEx;

//�A���[�����X�g
typedef struct {
	ESAlarmData data[LENGTH_OF_ALARMLIST];									//�A���[���f�[�^
} ESAlarmList;

//�A���[�����X�g(�T�u�R�[�h������Ή�)
typedef struct {
	ESAlarmDataEx data[LENGTH_OF_ALARMLIST];								//�A���[���f�[�^(�T�u�R�[�h�Ή�)
} ESAlarmListEx;

//�X�e�[�^�X�f�[�^
typedef struct {
			long status1;													//�X�e�[�^�X�f�[�^1
			long status2;													//�X�e�[�^�X�f�[�^2
} ESStatusData;

//�W���u�X�e�[�^�X�f�[�^
typedef struct {
			char jobName[LENGTH_OF_NAME+1];									//�W���u����
			long lineNo;													//���C���ԍ�
			long stepNo;													//�X�e�b�v�ԍ�
			long speedOverride;												//���x�I�[�o�[���C�h�l
} ESJobStatusData;

//���\���f�[�^
typedef struct {
			char configurations[NUMBER_OF_AXIS][LENGTH_OF_CONFIGNAME+1];	//������(SLURBT,XYZRxRyRz)
} ESConfigurationData;

//���f�[�^
typedef struct {
			double axis [NUMBER_OF_AXIS];									//���f�[�^
} ESAxisData;

//���{�b�g�ʒu�f�[�^
typedef struct {
			long dataType;													//�f�[�^�^�C�v(�p���X�l/����l)
			long fig;														//�`��
			long toolNo;													//�c�[���ԍ�
			long userFrameNo;												//���[�U���W�ԍ�
			long exFig;														//�g���`��
			ESAxisData axesData;											//���f�[�^
} ESPositionData;

//�x�[�X�ʒu/�O�����ʒu�f�[�^
typedef struct {
			long dataType;													//�f�[�^�^�C�v(�p���X�l/����l)
			ESAxisData axesData;											//���f�[�^
} ESBpexPositionData;

//�Ǘ����ԃf�[�^
typedef struct {
			char startTime[LENGTH_OF_TIME+1];								//�ғ��J�n����
			char elapseTime[LENGTH_OF_ELAPSETIME+1];						//�o�ߎ���
} ESMonitoringTimeData;

//�V�X�e�����f�[�^
typedef struct {
			char systemVersion[LENGTH_OF_SYSTEMVER+1];						//�V�X�e���\�t�g�E�F�A�o�[�W����
			char name[LENGTH_OF_ROBOTNAME+1];								//�@�햼��/�p�r����
			char parameterNo[LENGTH_OF_PARAMNO+1];							//�p�����[�^No.
} ESSystemInfoData;

//�ړ����f�[�^
typedef struct
{
			long  	robotNo;												//���{�b�g�ԍ�
			long  	stationNo;												//�X�e�[�V�����ԍ�
			long  	speedType;												//���x�敪�w��
			double  speed;													//���x�w��
} ESMoveData;

//���{�b�g�̖ڕW�ʒu�f�[�^�i�������W�j
typedef ESPositionData ESCartPosData;										//���{�b�g�̖ڕW�ʒu�f�[�^�i����l�j

//���{�b�g�̖ڕW�ʒu�f�[�^�i�p���X�j
typedef ESAxisData ESPulsePosData;											//���{�b�g�̖ڕW�ʒu�f�[�^�i�p���X�l�j

//�x�[�X�̖ڕW�ʒu�f�[�^
typedef struct {
			double axis[NUMBER_OF_BASE_AXIS];								//�x�[�X�̖ڕW�ʒu�f�[�^
} ESBaseData;

//�X�e�[�V�����̖ڕW�ʒu�f�[�^
typedef struct {
			double axis[NUMBER_OF_STATION_AXIS];							//�X�e�[�V�����̖ڕW�ʒu�f�[�^
} ESStationData;

//�ړ����߃f�[�^�i�������W�j
typedef struct {
			ESMoveData		moveData;										//�ړ����f�[�^
			ESCartPosData	robotPos;										//���{�b�g�̖ڕW�ʒu�f�[�^
			ESBaseData		basePos;										//�x�[�X�̖ڕW�ʒu�f�[�^
			ESStationData	stationPos;										//�X�e�[�V�����̖ڕW�ʒu�f�[�^
}ESCartMoveData;

//�ړ����߃f�[�^�i�p���X�j
typedef struct {
			ESMoveData		moveData;										//�ړ����f�[�^
			ESPulsePosData	robotPos;										//���{�b�g�̖ڕW�ʒu�f�[�^
			ESBaseData		basePos;										//�x�[�X�̖ڕW�ʒu�f�[�^
			ESStationData	stationPos;										//�X�e�[�V�����̖ڕW�ʒu�f�[�^
			long			toolNo;											//�c�[���ԍ�
}ESPulseMoveData;

//1�o�C�g�̕����f�[�^
typedef struct {
			char data[LENGTH_OF_MULTI_1];
}ESMultiByteData;

//2�o�C�g�̕����f�[�^
typedef struct {
			short data[LENGTH_OF_MULTI_2];
}ESMultiShortData;

//2�o�C�g�̕����f�[�^�iUnsigned�j
typedef struct {
			unsigned short data[LENGTH_OF_MULTI_2];
}ESMultiUShortData;

//LONG�̕����f�[�^
typedef struct {
			long data[LENGTH_OF_MULTI_4];
}ESMultiLongData;

//DOUBLE�̕����f�[�^
typedef struct {
			double data[LENGTH_OF_MULTI_4];
}ESMultiRealData;

//������̕����f�[�^
typedef struct {
			char data[LENGTH_OF_MULTI_STR][LENGTH_OF_STRING+1];
}ESMultiStrData;

//ESPositionData�̕����f�[�^
typedef struct {
			ESPositionData data[LENGTH_OF_MULTI_POS];
}ESMultiPositionData;

//ESBpexPositionData�̕����f�[�^
typedef struct {
			ESBpexPositionData data[LENGTH_OF_MULTI_BPEX];
}ESMultiBpexPositionData;

#define STDCALL __stdcall

/*�R�}���h*/
//���̑��̊֐�
LONG	STDCALL	ESOpen(long controllerType, char *ipAddress, HANDLE *handle);						//�ڑ�
LONG	STDCALL	ESClose(HANDLE handle);																//�ؒf
LONG	STDCALL ESSetTimeOut(HANDLE handle, long timeOut, long retry);								//�^�C���A�E�g�E���g���C�ݒ�

//���[�h�E�Ď��n
LONG	STDCALL	ESGetAlarm(HANDLE handle, ESAlarmList *alarmList);									//�������A���[���ǂݏo��
LONG	STDCALL	ESGetAlarmHist(HANDLE handle, long alarmHistNo, ESAlarmData *alarmData);			//�A���[������ǂݏo��
LONG	STDCALL	ESGetAlarmEx(HANDLE handle, ESAlarmListEx *alarmList);								//�������A���[���ǂݏo��(�T�u�R�[�h������Ή�)
LONG	STDCALL	ESGetAlarmHistEx(HANDLE handle, long alarmHistNo, ESAlarmDataEx *alarmData);		//�A���[������ǂݏo��(�T�u�R�[�h������Ή�)
LONG	STDCALL	ESGetStatus(HANDLE handle, ESStatusData *statusData);								//�X�e�[�^�X�ǂݏo��
LONG	STDCALL	ESGetJobStatus(HANDLE handle, long taskNo, ESJobStatusData *jobStatusData);			//���s�W���u���ǂݏo��
LONG	STDCALL	ESGetConfiguration(HANDLE handle, long ctrlGrp, ESConfigurationData *configData);	//���\���ǂݏo��
LONG	STDCALL	ESGetPosition(HANDLE handle, long ctrlGrp, ESPositionData *positionData);			//���{�b�g�ʒu�̓ǂݏo��
LONG	STDCALL	ESGetDeviation(HANDLE handle, long ctrlGrp, ESAxisData *deviationData);				//�e���ʒu�΍��ǂݏo��
LONG	STDCALL	ESGetTorque(HANDLE handle, long ctrlGrp, ESAxisData *torqueData);					//�e���g���N�̓ǂݏo��
LONG	STDCALL ESGetMonitoringTime(HANDLE handle, long timeType, ESMonitoringTimeData *timeData);	//�Ǘ����ԓǂݏo��
LONG	STDCALL ESGetSystemInfo(HANDLE handle, long systemType, ESSystemInfoData *infoData);		//�V�X�e�����ǂݏo��

//I/O���[�h�E���C�g�n
LONG	STDCALL	ESReadIO1(HANDLE handle, long ioNumber, short *ioData);								//IO�f�[�^�̓ǂݏo��
LONG	STDCALL	ESWriteIO1(HANDLE handle, long ioNumber, short ioData);								//IO�f�[�^�̏�������
LONG	STDCALL	ESReadIO2(HANDLE handle, long ioNumber, short *ioData);								//IO�f�[�^�̓ǂݏo���i1�o�C�g���o�͋֎~�d�l�j
LONG	STDCALL	ESWriteIO2(HANDLE handle, long ioNumber, short ioData);								//IO�f�[�^�̏������݁i1�o�C�g���o�͋֎~�d�l�j
LONG	STDCALL	ESReadRegister(HANDLE handle, long regNumber, unsigned short *regData);				//���W�X�^�f�[�^�̓ǂݏo��
LONG	STDCALL	ESWriteRegister(HANDLE handle, long regNumber, unsigned short regData);				//���W�X�^�f�[�^�̏�������
LONG	STDCALL	ESReadIOM(HANDLE handle, long ioNumber, long number, ESMultiByteData *ioData);		//IO�f�[�^�̓ǂݏo��(Multi)
LONG	STDCALL	ESWriteIOM(HANDLE handle, long ioNumber, long number, ESMultiByteData ioData);		//IO�f�[�^�̏�������(Multi)
LONG	STDCALL	ESReadRegisterM(HANDLE handle, long regNumber, long number,							//���W�X�^�f�[�^�̓ǂݏo��(Multi)
								ESMultiUShortData *regData);
LONG	STDCALL	ESWriteRegisterM(HANDLE handle, long regNumber, long number,						//���W�X�^�f�[�^�̏�������(Multi)
								 ESMultiUShortData regData);

//���[�h�E�f�[�^�A�N�Z�X�n���ҏW�n
LONG	STDCALL	ESGetVarData1(HANDLE handle, long type, long number, double *data);					//�ϐ��̓ǂݏo��
LONG	STDCALL	ESSetVarData1(HANDLE handle, long type, long number, double data);					//�ϐ��̏�������
LONG	STDCALL	ESGetVarData2(HANDLE handle, long type, long number, double *data);					//�ϐ��̓ǂݏo���i1�o�C�g���o�͋֎~�d�l�j
LONG	STDCALL	ESSetVarData2(HANDLE handle, long type, long number, double data);					//�ϐ��̏������݁i1�o�C�g���o�͋֎~�d�l�j
LONG	STDCALL	ESGetStrData(HANDLE handle, long number, char *cp);									//�����^�ϐ�(S)�̓ǂݏo��
LONG	STDCALL	ESSetStrData(HANDLE handle, long number, char *cp);									//�����^�ϐ�(S)�̏�������
LONG	STDCALL	ESGetPositionData(HANDLE handle, long number, ESPositionData *positionData);		//���{�b�g�ʒu�^�ϐ�(P)�̓ǂݏo��
LONG	STDCALL	ESSetPositionData(HANDLE handle, long number, ESPositionData positionData);			//���{�b�g�ʒu�^�ϐ�(P)�̏�������
LONG	STDCALL	ESGetBpexPositionData(HANDLE handle, long type, long number,						//�x�[�X�ʒu�^�ϐ�(BP)�E�O�����ʒu�^�ϐ�(EX)�̓ǂݏo��
									  ESBpexPositionData *positionData);
LONG	STDCALL	ESSetBpexPositionData(HANDLE handle, long type, long number,						//�x�[�X�ʒu�^�ϐ�(BP)�E�O�����ʒu�^�ϐ�(EX)�̏�������
									  ESBpexPositionData positionData);
LONG	STDCALL	ESGetVarDataMB(HANDLE handle, long varNo, long number, ESMultiByteData *varData);	//B�ϐ��̓ǂݏo��(Multi)
LONG	STDCALL	ESSetVarDataMB(HANDLE handle, long varNo, long number, ESMultiByteData varData);	//B�ϐ��̏�������(Multi)
LONG	STDCALL	ESGetVarDataMI(HANDLE handle, long varNo, long number, ESMultiShortData *varData);	//I�ϐ��̓ǂݏo��(Multi)
LONG	STDCALL	ESSetVarDataMI(HANDLE handle, long varNo, long number, ESMultiShortData varData);	//I�ϐ��̏�������(Multi)	
LONG	STDCALL	ESGetVarDataMD(HANDLE handle, long varNo, long number, ESMultiLongData *varData);	//D�ϐ��̓ǂݏo��(Multi)
LONG	STDCALL	ESSetVarDataMD(HANDLE handle, long varNo, long number, ESMultiLongData varData);	//D�ϐ��̏�������(Multi)
LONG	STDCALL	ESGetVarDataMR(HANDLE handle, long varNo, long number, ESMultiRealData *varData);	//R�ϐ��̓ǂݏo��(Multi)
LONG	STDCALL	ESSetVarDataMR(HANDLE handle, long varNo, long number, ESMultiRealData varData);	//R�ϐ��̏�������(Multi)
LONG	STDCALL	ESGetStrDataM(HANDLE handle, long varNo, long number, ESMultiStrData *varData);		//S�ϐ��̓ǂݏo��(Multi)
LONG	STDCALL	ESSetStrDataM(HANDLE handle, long varNo, long number, ESMultiStrData varData);		//S�ϐ��̏�������(Multi)
LONG	STDCALL	ESGetPositionDataM(HANDLE handle, long varNo, long number,							//P�ϐ��̓ǂݏo��(Multi)
								   ESMultiPositionData *positionData);
LONG	STDCALL	ESSetPositionDataM(HANDLE handle, long varNo, long number,							//P�ϐ��̏�������(Multi)
								   ESMultiPositionData positionData);
LONG	STDCALL	ESGetBpexPositionDataM(HANDLE handle, long type, long varNo, long number,			//BP�ϐ��EEX�ϐ��̓ǂݏo��(Multi)
									   ESMultiBpexPositionData *positionData);
LONG	STDCALL	ESSetBpexPositionDataM(HANDLE handle, long type, long varNo, long number,			//BP�ϐ��EEX�ϐ��̏�������(Multi)
									   ESMultiBpexPositionData positionData);

//����n
LONG	STDCALL	ESReset(HANDLE handle);																//���Z�b�g
LONG	STDCALL	ESCancel(HANDLE handle);															//�L�����Z��
LONG	STDCALL	ESHold(HANDLE handle, long onOff);													//�z�[���h
LONG	STDCALL	ESServo(HANDLE handle, long onOff);													//�T�[�{�I��
LONG	STDCALL	ESHlock(HANDLE handle, long onOff);													//PP��IO�̑���n�M���̃C���^���b�N
LONG	STDCALL	ESCycle(HANDLE handle, long cycle);													//�X�e�b�v/�T�C�N��/�A������
LONG	STDCALL	ESBDSP(HANDLE handle, char *message);												//�y���_���g�ւ̕�����\��

//�N���n
LONG	STDCALL	ESStartJob(HANDLE handle);															//�W���u�X�^�[�g
LONG	STDCALL ESCartMove(HANDLE handle, long moveType, ESCartMoveData moveData);					//�ړ����߁i�������W�j
LONG	STDCALL ESPulseMove(HANDLE handle, long moveType, ESPulseMoveData moveData);				//�ړ����߁i�p���X�j

//�W���u�I���n
LONG	STDCALL	ESSelectJob(HANDLE handle, long jobType, long lineNo, char *jobName);				//�W���u�I��

//�t�@�C������n
LONG	STDCALL	ESSaveFile(HANDLE handle, char *savePath, char *fileName);							//�t�@�C���Z�[�u
LONG	STDCALL	ESLoadFile(HANDLE handle, char *filePath);											//�t�@�C�����[�h
LONG	STDCALL	ESDeleteJob(HANDLE handle, char *jobName);											//�W���u�폜
LONG	STDCALL	ESFileListFirst(HANDLE handle, long fileType, char *fileName);						//�t�@�C�����X�g�̍X�V���擪��ǂݍ���
LONG	STDCALL	ESFileListNext(HANDLE handle, char *fileName);										//�t�@�C�����X�g��ǂݍ���
#undef AFX_DATA
#define AFX_DATA
