Option Strict Off
Option Explicit On
Module MotoCom32
	' // MotoCom.h
	'public const DECSPCEX = __declspec(dllexport )
	
	'#ifdef __cplusplus
	'extern "C"
	'{
	'#endif
	' ////////////////////////// Open ///////////
	Public Const PACKETCOM As Short = (1)
	Public Const PACKETLPT As Short = (2)
	Public Const PACKETHSLSERVER As Short = (3)
	Public Const PACKETHSLCLIENT As Short = (4)
	
	Public Const PACKETETHERNET As Short = (16)
	
	Public Const PACKETESERVER As Short = (256)
	
	Public Const PACKETESERVER_NO_LINGER As Short = (4096)
	
	' /****************************************************/
	' /****************   BSC     *************************/
	' /****************************************************/
	' /* bsc3.h */
	' /*  "COM1:9600,E,8,1" */
	Declare Function BscCancel Lib "MotoCom32"  Alias "_BscCancel@4"(ByVal nCid As Short) As Short
	Declare Function BscChangeTask Lib "MotoCom32"  Alias "_BscChangeTask@8"(ByVal nCid As Short, ByVal task As Short) As Short
	Declare Function BscClose Lib "MotoCom32"  Alias "_BscClose@4"(ByVal nCid As Short) As Short
	'Declare Function BscCloseAll Lib "MotoCom32" Alias "_BscCloseAll@4" (ByVal nCiddummy%) As Integer
	Declare Function BscCommand Lib "MotoCom32"  Alias "_BscCommand@16"(ByVal nCid As Short, ByVal h_buf As String, ByVal d_buf As String, ByVal fforever As Short) As Short
	Declare Function BscConnect Lib "MotoCom32"  Alias "_BscConnect@4"(ByVal nCid As Short) As Short
	Declare Function BscContinueJob Lib "MotoCom32"  Alias "_BscContinueJob@4"(ByVal nCid As Short) As Short
	Declare Function BscConvertJobP2R Lib "MotoCom32"  Alias "_BscConvertJobP2R@12"(ByVal nCid As Short, ByVal name_v As String, ByVal framename As String) As Short
	Declare Function BscConvertJobR2P Lib "MotoCom32"  Alias "_BscConvertJobR2P@16"(ByVal nCid As Short, ByVal name_v As String, ByVal cv_type As Short, ByVal var_no As String) As Short
	Declare Function BscDCIGetPos Lib "MotoCom32"  Alias "_BscDCIGetPos@16"(ByVal nCid As Short, ByRef type_v As Short, ByRef rconf As Short, ByRef p As Double) As Short
	Declare Function BscDCIGetPos2 Lib "MotoCom32"  Alias "_BscDCIGetPos2@20"(ByVal nCid As Short, ByRef type_v As Short, ByRef rconf As Short, ByRef p As Double, ByRef axisNum As Short) As Short
	Declare Function BscDCIGetVarData Lib "MotoCom32"  Alias "_BscDCIGetVarData@20"(ByVal nCid As Short, ByRef type_v As Short, ByRef rconf As Short, ByRef p As Double, ByVal s As String) As Short
	Declare Function BscDCIGetVarDataEx Lib "MotoCom32"  Alias "_BscDCIGetVarDataEx@24"(ByVal nCid As Short, ByRef type_v As Short, ByRef rconf As Integer, ByRef p As Double, ByVal s As String, ByRef axisNum As Short) As Short
	Declare Function BscDCILoadSave Lib "MotoCom32"  Alias "_BscDCILoadSave@8"(ByVal nCid As Short, ByVal timec As Short) As Short
	Declare Function BscDCILoadSaveOnce Lib "MotoCom32"  Alias "_BscDCILoadSaveOnce@4"(ByVal nCid As Short) As Short
	Declare Function BscDCIPutPos Lib "MotoCom32"  Alias "_BscDCIPutPos@16"(ByVal nCid As Short, ByVal type_v As Short, ByVal rconf As Short, ByRef p As Double) As Short
	Declare Function BscDCIPutPos2 Lib "MotoCom32"  Alias "_BscDCIPutPos2@20"(ByVal nCid As Short, ByVal type_v As Short, ByVal rconf As Short, ByRef p As Double, ByVal axisNum As Short) As Short
	Declare Function BscDCIPutVarData Lib "MotoCom32"  Alias "_BscDCIPutVarData@20"(ByVal nCid As Short, ByVal type_v As Short, ByVal rconf As Short, ByRef p As Double, ByVal s As String) As Short
	Declare Function BscDCIPutVarDataEx Lib "MotoCom32"  Alias "_BscDCIPutVarDataEx@24"(ByVal nCid As Short, ByVal type_v As Short, ByVal rconf As Integer, ByRef p As Double, ByVal s As String, ByVal axisNum As Short) As Short
	'Declare Function BscDelay Lib "MotoCom32" Alias "_BscDelay@8" (ByVal nCid%, ByVal a&) As Integer
	Declare Function BscDeleteJob Lib "MotoCom32"  Alias "_BscDeleteJob@4"(ByVal nCid As Short) As Short
	Declare Function BscDisConnect Lib "MotoCom32"  Alias "_BscDisConnect@4"(ByVal nCid As Short) As Short
	Declare Function BscDiskFreeSizeGet Lib "MotoCom32"  Alias "_BscDiskFreeSizeGet@12"(ByVal nCid As Short, ByVal dno As Short, ByRef dsize As Integer) As Short
	Declare Function BscDownLoad Lib "MotoCom32"  Alias "_BscDownLoad@8"(ByVal nCid As Short, ByVal fname As String) As Short
	Declare Function BscDownLoadEx Lib "MotoCom32"  Alias "_BscDownLoadEx@16"(ByVal nCid As Short, ByVal fname As String, ByVal srcPath As String, ByVal nFlg As Boolean) As Short
	Declare Function BscEnforcedClose Lib "MotoCom32"  Alias "_BscEnforcedClose@4"(ByVal nCid As Short) As Short
	Declare Function BscFindFirst Lib "MotoCom32"  Alias "_BscFindFirst@12"(ByVal nCid As Short, ByVal fname As String, ByVal size As Short) As Short
	Declare Function BscFindFirstMaster Lib "MotoCom32"  Alias "_BscFindFirstMaster@12"(ByVal nCid As Short, ByVal fname As String, ByVal size As Short) As Short
	Declare Function BscFindNext Lib "MotoCom32"  Alias "_BscFindNext@12"(ByVal nCid As Short, ByVal fname As String, ByVal size As Short) As Short
	Declare Function BscFindNextMaster Lib "MotoCom32"  Alias "_BscFindNextMaster@12"(ByVal nCid As Short, ByVal fname As String, ByVal size As Short) As Short
	'Declare Function BscGetAbso Lib "MotoCom32" Alias "_BscGetAbso@12" (ByVal nCid%, ByVal axisno%, abso&) As Integer
	Declare Function BscGetCartPos Lib "MotoCom32"  Alias "_BscGetCartPos@24"(ByVal nCid As Short, ByVal framename As String, ByVal isex As Short, ByRef rconf As Integer, ByRef toolno As Short, ByRef p As Double) As Short
	Declare Function BscGetCtrlGroup Lib "MotoCom32"  Alias "_BscGetCtrlGroup@12"(ByVal nCid As Short, ByRef groupinf As Short, ByRef taskinf As Short) As Short
	Declare Function BscGetCtrlGroupDx Lib "MotoCom32"  Alias "_BscGetCtrlGroupDx@16"(ByVal nCid As Short, ByRef robtask As Integer, ByRef stattask As Integer, ByRef taskinf As Short) As Short
	Declare Function BscGetCtrlGroupXrc Lib "MotoCom32"  Alias "_BscGetCtrlGroupXrc@16"(ByVal nCid As Short, ByRef robtask As Short, ByRef stattask As Short, ByRef taskinf As Short) As Short
	Declare Function BscGetError Lib "MotoCom32"  Alias "_BscGetError@4"(ByVal nCid As Short) As Short
	Declare Function BscGetError2 Lib "MotoCom32"  Alias "_BscGetError2@4"(ByVal nCid As Short) As Short
	Declare Function BscGetFirstAlarm Lib "MotoCom32"  Alias "_BscGetFirstAlarm@8"(ByVal nCid As Short, ByRef data_v As Short) As Short
	Declare Function BscGetFirstAlarmS Lib "MotoCom32"  Alias "_BscGetFirstAlarmS@12"(ByVal nCid As Short, ByRef data_v As Short, ByVal s As String) As Short
	Declare Function BscGetNextAlarm Lib "MotoCom32"  Alias "_BscGetNextAlarm@8"(ByVal nCid As Short, ByRef data_v As Short) As Short
	Declare Function BscGetNextAlarmS Lib "MotoCom32"  Alias "_BscGetNextAlarmS@12"(ByVal nCid As Short, ByRef data_v As Short, ByVal s As String) As Short
	Declare Function BscGetPulsePos Lib "MotoCom32"  Alias "_BscGetPulsePos@8"(ByVal nCid As Short, ByRef p As Double) As Short
	Declare Function BscGets Lib "MotoCom32"  Alias "_BscGets@16"(ByVal nCid As Short, ByVal bufptr As String, ByVal bsize As Short, ByRef plengets As Short) As Short
	Declare Function BscGetStatus Lib "MotoCom32"  Alias "_BscGetStatus@12"(ByVal nCid As Short, ByRef d1 As Short, ByRef d2 As Short) As Short
	Declare Function BscGetUFrame Lib "MotoCom32"  Alias "_BscGetUFrame@12"(ByVal nCid As Short, ByVal ufname As String, ByRef p As Double) As Short
	Declare Function BscGetVarData Lib "MotoCom32"  Alias "_BscGetVarData@16"(ByVal nCid As Short, ByVal type_v As Short, ByVal varno As Short, ByRef pos As Double) As Short
	Declare Function BscGetVarData2 Lib "MotoCom32"  Alias "_BscGetVarData2@16"(ByVal nCid As Short, ByVal type_v As Short, ByVal varno As Short, ByRef pos As Double) As Short
	Declare Function BscGetVarDataEx Lib "MotoCom32"  Alias "_BscGetVarDataEx@24"(ByVal nCid As Short, ByVal type_v As Short, ByVal varno As Short, ByRef pos As Double, ByVal s As String, ByRef axisNum As Short) As Short
	Declare Function BscHoldOff Lib "MotoCom32"  Alias "_BscHoldOff@4"(ByVal nCid As Short) As Short
	Declare Function BscHoldOn Lib "MotoCom32"  Alias "_BscHoldOn@4"(ByVal nCid As Short) As Short
    Declare Function BscHostGetVarData Lib "MotoCom32" Alias "_BscHostGetVarData@20" (ByVal nCid As Short, ByVal type_v As Short, ByVal varno As Short, ByRef pos As Double, ByVal s As String) As Short
	Declare Function BscHostGetVarDataM Lib "MotoCom32"  Alias "_BscHostGetVarDataM@20"(ByVal nCid As Short, ByVal type_v As Short, ByVal varno As Short, ByVal num As Short, ByRef pos As Double) As Short
	Declare Function BscHostPutVarData Lib "MotoCom32"  Alias "_BscHostPutVarData@20"(ByVal nCid As Short, ByVal type_v As Short, ByVal varno As Short, ByRef dat As Double, ByVal s As String) As Short
	Declare Function BscHostPutVarDataM Lib "MotoCom32"  Alias "_BscHostPutVarDataM@20"(ByVal nCid As Short, ByVal type_v As Short, ByVal varno As Short, ByVal num As Short, ByRef dat As Double) As Short
	Declare Function BscImov Lib "MotoCom32"  Alias "_BscImov@28"(ByVal nCid As Short, ByVal vtype As String, ByVal spd As Double, ByVal framename As String, ByVal toolno As Short, ByRef p As Double) As Short
	Declare Function BscImovEx Lib "MotoCom32"  Alias "_BscImovEx@32"(ByVal nCid As Short, ByVal vtype As String, ByVal spd As Double, ByVal framename As String, ByVal toolno As Short, ByRef p As Double, ByVal axisNum As Short) As Short
	Declare Function BscInBytes Lib "MotoCom32"  Alias "_BscInBytes@4"(ByVal nCid As Short) As Short
	Declare Function BscIsAlarm Lib "MotoCom32"  Alias "_BscIsAlarm@4"(ByVal nCid As Short) As Short
	Declare Function BscIsCtrlGroup Lib "MotoCom32"  Alias "_BscIsCtrlGroup@4"(ByVal nCid As Short) As Short
	Declare Function BscIsCtrlGroupDx Lib "MotoCom32"  Alias "_BscIsCtrlGroupDx@12"(ByVal nCid As Short, ByRef robtask As Integer, ByRef stattask As Integer) As Short
	Declare Function BscIsCtrlGroupXrc Lib "MotoCom32"  Alias "_BscIsCtrlGroupXrc@12"(ByVal nCid As Short, ByRef robtask As Short, ByRef stattask As Short) As Short
	Declare Function BscIsCycle Lib "MotoCom32"  Alias "_BscIsCycle@4"(ByVal nCid As Short) As Short
	Declare Function BscIsError Lib "MotoCom32"  Alias "_BscIsError@4"(ByVal nCid As Short) As Short
	Declare Function BscIsErrorCode Lib "MotoCom32"  Alias "_BscIsErrorCode@4"(ByVal nCid As Short) As Short
	Declare Function BscIsHold Lib "MotoCom32"  Alias "_BscIsHold@4"(ByVal nCid As Short) As Short
	Declare Function BscIsJobLine Lib "MotoCom32"  Alias "_BscIsJobLine@4"(ByVal nCid As Short) As Short
	Declare Function BscIsJobName Lib "MotoCom32"  Alias "_BscIsJobName@12"(ByVal nCid As Short, ByVal jobname As String, ByVal size As Short) As Short
	Declare Function BscIsJobStep Lib "MotoCom32"  Alias "_BscIsJobStep@4"(ByVal nCid As Short) As Short
	Declare Function BscIsLoc Lib "MotoCom32"  Alias "_BscIsLoc@16"(ByVal nCid As Short, ByVal ispulse As Short, ByRef rconf As Short, ByRef p As Double) As Short
	Declare Function BscIsPlayMode Lib "MotoCom32"  Alias "_BscIsPlayMode@4"(ByVal nCid As Short) As Short
	Declare Function BscIsRemoteMode Lib "MotoCom32"  Alias "_BscIsRemoteMode@4"(ByVal nCid As Short) As Short
	Declare Function BscIsRobotPos Lib "MotoCom32"  Alias "_BscIsRobotPos@24"(ByVal nCid As Short, ByVal framename As String, ByVal isex As Short, ByRef rconf As Short, ByRef toolno As Short, ByRef p As Double) As Short
	Declare Function BscIsServo Lib "MotoCom32"  Alias "_BscIsServo@4"(ByVal nCid As Short) As Short
	Declare Function BscIsTaskInf Lib "MotoCom32"  Alias "_BscIsTaskInf@4"(ByVal nCid As Short) As Short
	Declare Function BscIsTaskInfDx Lib "MotoCom32"  Alias "_BscIsTaskInfDx@4"(ByVal nCid As Short) As Short
	Declare Function BscIsTaskInfXrc Lib "MotoCom32"  Alias "_BscIsTaskInfXrc@4"(ByVal nCid As Short) As Short
	Declare Function BscIsTeachMode Lib "MotoCom32"  Alias "_BscIsTeachMode@4"(ByVal nCid As Short) As Short
	Declare Function BscJobWait Lib "MotoCom32"  Alias "_BscJobWait@8"(ByVal nCid As Short, ByVal time As Short) As Short
	Declare Function BscMDSP Lib "MotoCom32"  Alias "_BscMDSP@8"(ByVal nCid As Short, ByVal ptr As String) As Short
	Declare Function BscMov Lib "MotoCom32"  Alias "_BscMov@36"(ByVal nCid As Short, ByVal movtype As String, ByVal vtype As String, ByVal spd As Double, ByVal framename As String, ByVal rconf As Short, ByVal toolno As Short, ByRef p As Double) As Short
	Declare Function BscMovEx Lib "MotoCom32"  Alias "_BscMovEx@40"(ByVal nCid As Short, ByVal movtype As String, ByVal vtype As String, ByVal spd As Double, ByVal framename As String, ByVal rconf As Integer, ByVal toolno As Short, ByRef p As Double, ByVal axisNum As Short) As Short
	Declare Function BscMovj Lib "MotoCom32"  Alias "_BscMovj@28"(ByVal nCid As Short, ByVal spd As Double, ByVal framename As String, ByVal rconf As Short, ByVal toolno As Short, ByRef p As Double) As Short
	Declare Function BscMovjEx Lib "MotoCom32"  Alias "_BscMovjEx@32"(ByVal nCid As Short, ByVal spd As Double, ByVal framename As String, ByVal rconf As Integer, ByVal toolno As Short, ByRef p As Double, ByVal axisNum As Short) As Short
	Declare Function BscMovl Lib "MotoCom32"  Alias "_BscMovl@32"(ByVal nCid As Short, ByVal vtype As String, ByVal spd As Double, ByVal framename As String, ByVal rconf As Short, ByVal toolno As Short, ByRef p As Double) As Short
	Declare Function BscMovlEx Lib "MotoCom32"  Alias "_BscMovlEx@36"(ByVal nCid As Short, ByVal vtype As String, ByVal spd As Double, ByVal framename As String, ByVal rconf As Integer, ByVal toolno As Short, ByRef p As Double, ByVal axisNum As Short) As Short
	Declare Function BscOpen Lib "MotoCom32"  Alias "_BscOpen@8"(ByVal path As String, ByVal mode As Short) As Short
	Declare Function BscOPLock Lib "MotoCom32"  Alias "_BscOPLock@4"(ByVal nCid As Short) As Short
	Declare Function BscOPUnLock Lib "MotoCom32"  Alias "_BscOPUnLock@4"(ByVal nCid As Short) As Short
	Declare Function BscOutBytes Lib "MotoCom32"  Alias "_BscOutBytes@4"(ByVal nCid As Short) As Short
	Declare Function BscPMov Lib "MotoCom32"  Alias "_BscPMov@28"(ByVal nCid As Short, ByVal movtype As String, ByVal vtype As String, ByVal spd As Double, ByVal toolno As Short, ByRef p As Double) As Short
	Declare Function BscPMovEx Lib "MotoCom32"  Alias "_BscPMovEx@32"(ByVal nCid As Short, ByVal movtype As String, ByVal vtype As String, ByVal spd As Double, ByVal toolno As Short, ByRef p As Double, ByVal axisNum As Short) As Short
	Declare Function BscPMovj Lib "MotoCom32"  Alias "_BscPMovj@20"(ByVal nCid As Short, ByVal spd As Double, ByVal toolno As Short, ByRef p As Double) As Short
	Declare Function BscPMovjEx Lib "MotoCom32"  Alias "_BscPMovjEx@24"(ByVal nCid As Short, ByVal spd As Double, ByVal toolno As Short, ByRef p As Double, ByVal axisNum As Short) As Short
	Declare Function BscPMovl Lib "MotoCom32"  Alias "_BscPMovl@24"(ByVal nCid As Short, ByVal vtype As String, ByVal spd As Double, ByVal toolno As Short, ByRef p As Double) As Short
	Declare Function BscPMovlEx Lib "MotoCom32"  Alias "_BscPMovlEx@28"(ByVal nCid As Short, ByVal vtype As String, ByVal spd As Double, ByVal toolno As Short, ByRef p As Double, ByVal axisNum As Short) As Short
	Declare Function BscPuts Lib "MotoCom32"  Alias "_BscPuts@12"(ByVal nCid As Short, ByVal bufptr As String, ByVal length As Short) As Short
	Declare Function BscPutUFrame Lib "MotoCom32"  Alias "_BscPutUFrame@12"(ByVal nCid As Short, ByVal ufname As String, ByRef p As Double) As Short
	Declare Function BscPutVarData Lib "MotoCom32"  Alias "_BscPutVarData@16"(ByVal nCid As Short, ByVal type_v As Short, ByVal varno As Short, ByRef dat As Double) As Short
	Declare Function BscPutVarData2 Lib "MotoCom32"  Alias "_BscPutVarData2@20"(ByVal nCid As Short, ByVal type_v As Short, ByVal varno As Short, ByRef dat As Double, ByVal axisNum As Short) As Short
	Declare Function BscPutVarDataEx Lib "MotoCom32"  Alias "_BscPutVarDataEx@24"(ByVal nCid As Short, ByVal type_v As Short, ByVal varno As Short, ByRef dat As Double, ByVal s As String, ByVal axisNum As Short) As Short
	Declare Function BscReadAlarmS Lib "MotoCom32"  Alias "_BscReadAlarmS@12"(ByVal nCid As Short, ByRef data_v As Short, ByVal s As String) As Short
	Declare Function BscReadIO Lib "MotoCom32"  Alias "_BscReadIO@16"(ByVal nCid As Short, ByVal startadd As Short, ByVal ionum As Short, ByRef stat As Short) As Short
	Declare Function BscReadIO2 Lib "MotoCom32"  Alias "_BscReadIO2@16"(ByVal nCid As Short, ByVal startadd As Integer, ByVal ionum As Short, ByRef stat As Short) As Short
	Declare Function BscReConnect Lib "MotoCom32"  Alias "_BscReConnect@4"(ByVal nCid As Short) As Short
	Declare Function BscReset Lib "MotoCom32"  Alias "_BscReset@4"(ByVal nCid As Short) As Short
	Declare Function BscReStartJob Lib "MotoCom32"  Alias "_BscReStartJob@4"(ByVal nCid As Short) As Short
	Declare Function BscSelectJob Lib "MotoCom32"  Alias "_BscSelectJob@8"(ByVal nCid As Short, ByVal name_v As String) As Short
	Declare Function BscSelectMode Lib "MotoCom32"  Alias "_BscSelectMode@8"(ByVal nCid As Short, ByVal mode As Short) As Short
	Declare Function BscSelLoopCycle Lib "MotoCom32"  Alias "_BscSelLoopCycle@4"(ByVal nCid As Short) As Short
	Declare Function BscSelOneCycle Lib "MotoCom32"  Alias "_BscSelOneCycle@4"(ByVal nCid As Short) As Short
	Declare Function BscSelStepCycle Lib "MotoCom32"  Alias "_BscSelStepCycle@4"(ByVal nCid As Short) As Short
	Declare Function BscServoOff Lib "MotoCom32"  Alias "_BscServoOff@4"(ByVal nCid As Short) As Short
	Declare Function BscServoOn Lib "MotoCom32"  Alias "_BscServoOn@4"(ByVal nCid As Short) As Short
	'Declare Function BscSetAbso Lib "MotoCom32" Alias "_BscSetAbso@12" (ByVal nCid%, ByVal axisno%, ByVal abso&) As Integer
	Declare Function BscSetBreak Lib "MotoCom32"  Alias "_BscSetBreak@8"(ByVal nCid As Short, ByVal flg As Short) As Short
	Declare Function BscSetCom Lib "MotoCom32"  Alias "_BscSetCom@24"(ByVal nCid As Short, ByVal port As Short, ByVal baud As Integer, ByVal parity As Short, ByVal clen As Short, ByVal stp As Short) As Short
	Declare Function BscSetCondBSC Lib "MotoCom32"  Alias "_BscSetCondBSC@20"(ByVal nCid As Short, ByVal timerA As Short, ByVal timerB As Short, ByVal rtyR As Short, ByVal rtyW As Short) As Short
	Declare Function BscSetCtrlGroup Lib "MotoCom32"  Alias "_BscSetCtrlGroup@8"(ByVal nCid As Short, ByVal groupno As Short) As Short
	Declare Function BscSetCtrlGroupDx Lib "MotoCom32"  Alias "_BscSetCtrlGroupDx@12"(ByVal nCid As Short, ByVal groupno1 As Integer, ByVal groupno2 As Integer) As Short
	Declare Function BscSetCtrlGroupXrc Lib "MotoCom32"  Alias "_BscSetCtrlGroupXrc@12"(ByVal nCid As Short, ByVal groupno1 As Short, ByVal groupno2 As Short) As Short
	Declare Function BscSetEServer Lib "MotoCom32"  Alias "_BscSetEServer@8"(ByVal nCid As Short, ByVal IPaddr As String) As Short
	Declare Function BscSetEServerMode Lib "MotoCom32"  Alias "_BscSetEServerMode@12"(ByVal nCid As Short, ByVal IPaddr As String, ByVal mode As Short) As Short
	Declare Function BscSetEther Lib "MotoCom32"  Alias "_BscSetEther@16"(ByVal nCid As Short, ByVal IPaddr As String, ByVal mode As Short, ByVal hWnd As Integer) As Short
	'Declare Function BscSetHSL Lib "MotoCom32" Alias "_BscSetHSL@12" (ByVal nCid%, ByVal strName$, ByVal port%) As Integer
	Declare Function BscSetLineNumber Lib "MotoCom32"  Alias "_BscSetLineNumber@8"(ByVal nCid As Short, ByVal line_v As Short) As Short
	Declare Function BscSetMasterJob Lib "MotoCom32"  Alias "_BscSetMasterJob@4"(ByVal nCid As Short) As Short
	Declare Function BscStartJob Lib "MotoCom32"  Alias "_BscStartJob@4"(ByVal nCid As Short) As Short
	Declare Function BscStatus Lib "MotoCom32"  Alias "_BscStatus@20"(ByVal nCid As Short, ByVal hpt As String, ByVal dpt As String, ByVal sz As Short, ByVal rbuf As String) As Short
	Declare Function BscUpLoad Lib "MotoCom32"  Alias "_BscUpLoad@8"(ByVal nCid As Short, ByVal fname As String) As Short
	Declare Function BscUpLoadEx Lib "MotoCom32"  Alias "_BscUpLoadEx@16"(ByVal nCid As Short, ByVal fname As String, ByVal desPath As String, ByVal nFlg As Boolean) As Short
	'Declare Function BscUpLoadTRQ_HM Lib "MotoCom32" Alias "_BscUpLoadTRQ_HM@16" (ByVal nCid%, ByVal fname$, ByVal nType, ByVal nNo) As Integer
	'Declare Function BscUpLoadTRQ Lib "MotoCom32" Alias "_BscUpLoadTRQ@8" (ByVal nCid%, ByVal fname$) As Integer
	Declare Function BscWriteIO Lib "MotoCom32"  Alias "_BscWriteIO@16"(ByVal nCid As Short, ByVal startadd As Short, ByVal ionum As Short, ByRef stat As Short) As Short
	Declare Function BscWriteIO2 Lib "MotoCom32"  Alias "_BscWriteIO2@16"(ByVal nCid As Short, ByVal startadd As Integer, ByVal ionum As Short, ByRef stat As Short) As Short
	
	' /****************************************************/
	' /****************   FC1,FC2 *************************/
	' /****************************************************/
	' /* FCWIN.H */
	' /*************** Slave Command /I am FC1. ***********************/
	' /* DOS  COM1:96,E,8,1 */
	' /* Win FC1 COM1:4800,E,8,1 */
	' /* Win FC2 COM1:19200,E,8,1 */
	Declare Function FcClose Lib "MotoCom32"  Alias "_FcClose@4"(ByVal nCid As Short) As Short
	Declare Function FcConnect Lib "MotoCom32"  Alias "_FcConnect@4"(ByVal nCid As Short) As Short
	Declare Function FcDisConnect Lib "MotoCom32"  Alias "_FcDisConnect@4"(ByVal nCid As Short) As Short
	Declare Function FcGetCom Lib "MotoCom32"  Alias "_FcGetCom@12"(ByVal nCid As Short, ByVal buf As String, ByVal size As Short) As Short
	Declare Function FcGetData Lib "MotoCom32"  Alias "_FcGetData@12"(ByVal nCid As Short, ByVal buf As String, ByVal size As Short) As Short
	Declare Function FcGetPath Lib "MotoCom32"  Alias "_FcGetPath@12"(ByVal nCid As Short, ByVal buf As String, ByVal size As Short) As Short
	Declare Function FcOpen Lib "MotoCom32"  Alias "_FcOpen@8"(ByVal path As String, ByVal mode As Short) As Short
	Declare Function FcProc Lib "MotoCom32"  Alias "_FcProc@8"(ByVal nCid As Short, ByVal isremove As Short) As Short
	Declare Function FcScan Lib "MotoCom32"  Alias "_FcScan@4"(ByVal nCid As Short) As Short
	Declare Function FcSetBreak Lib "MotoCom32"  Alias "_FcSetBreak@8"(ByVal nCid As Short, ByVal flg As Short) As Short
	Declare Function FcSetCom Lib "MotoCom32"  Alias "_FcSetCom@24"(ByVal nCid As Short, ByVal port As Short, ByVal baud As Integer, ByVal parity As Short, ByVal clen As Short, ByVal stp As Short) As Short
	Declare Function FcSetHSL Lib "MotoCom32"  Alias "_FcSetHSL@12"(ByVal nCid As Short, ByVal strName As String, ByVal port As Short) As Short
	
	' /*************** Master Command /I am ERC.***********************/
	Declare Function FcAutoDeleteSet Lib "MotoCom32"  Alias "_FcAutoDeleteSet@8"(ByVal nCid As Short, ByVal flg As Short) As Short
	Declare Function FcBye Lib "MotoCom32"  Alias "_FcBye@4"(ByVal nCid As Short) As Short
	Declare Function FcChangeDir Lib "MotoCom32"  Alias "_FcChangeDir@8"(ByVal nCid As Short, ByVal dname As String) As Short
	Declare Function FcChangeDrive Lib "MotoCom32"  Alias "_FcChangeDrive@8"(ByVal nCid As Short, ByVal type_v As String) As Short
	Declare Function FcDiskSize Lib "MotoCom32"  Alias "_FcDiskSize@12"(ByVal nCid As Short, ByVal disksize As String, ByVal size As Short) As Short
	Declare Function FcDiskSizeSet Lib "MotoCom32"  Alias "_FcDiskSizeSet@8"(ByVal nCid As Short, ByVal flg As Short) As Short
	Declare Function FcDownload Lib "MotoCom32"  Alias "_FcDownload@8"(ByVal nCid As Short, ByVal fname As String) As Short
	Declare Function FcFileSize Lib "MotoCom32"  Alias "_FcFileSize@12"(ByVal nCid As Short, ByVal fname As String, ByRef size As Integer) As Short
	Declare Function FcFindCurrentDir Lib "MotoCom32"  Alias "_FcFindCurrentDir@12"(ByVal nCid As Short, ByVal dname As String, ByVal size As Short) As Short
	Declare Function FcFindFirst Lib "MotoCom32"  Alias "_FcFindFirst@12"(ByVal nCid As Short, ByVal fname As String, ByVal size As Short) As Short
	Declare Function FcFindFirstDir Lib "MotoCom32"  Alias "_FcFindFirstDir@12"(ByVal nCid As Short, ByVal dname As String, ByVal size As Short) As Short
	Declare Function FcFindNext Lib "MotoCom32"  Alias "_FcFindNext@12"(ByVal nCid As Short, ByVal fname As String, ByVal size As Short) As Short
	Declare Function FcFindNextDir Lib "MotoCom32"  Alias "_FcFindNextDir@12"(ByVal nCid As Short, ByVal dname As String, ByVal size As Short) As Short
	Declare Function FcIsDiskSize Lib "MotoCom32"  Alias "_FcIsDiskSize@4"(ByVal nCid As Short) As Short
	Declare Function FcLogin Lib "MotoCom32"  Alias "_FcLogin@4"(ByVal nCid As Short) As Short
	Declare Function FcLogout Lib "MotoCom32"  Alias "_FcLogout@4"(ByVal nCid As Short) As Short
	Declare Function FcMakeDir Lib "MotoCom32"  Alias "_FcMakeDir@8"(ByVal nCid As Short, ByVal dname As String) As Short
	Declare Function FcRemove Lib "MotoCom32"  Alias "_FcRemove@8"(ByVal nCid As Short, ByVal fname As String) As Short
	Declare Function FcRename Lib "MotoCom32"  Alias "_FcRename@12"(ByVal nCid As Short, ByVal oldname As String, ByVal newname As String) As Short
	Declare Function FcUpload Lib "MotoCom32"  Alias "_FcUpload@8"(ByVal nCid As Short, ByVal fname As String) As Short
	
	
	'#ifdef __cplusplus
	'}
	'#endif
End Module