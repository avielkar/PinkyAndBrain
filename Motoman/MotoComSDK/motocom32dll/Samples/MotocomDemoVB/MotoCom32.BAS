Attribute VB_Name = "MotoCom32"
' // MotoCom.h
'public const DECSPCEX = __declspec(dllexport )

'#ifdef __cplusplus
'extern "C"
'{
'#endif
' ////////////////////////// Open ///////////
Public Const PACKETCOM = (1)
Public Const PACKETLPT = (2)
Public Const PACKETHSLSERVER = (3)
Public Const PACKETHSLCLIENT = (4)

Public Const PACKETETHERNET = (16)

Public Const PACKETESERVER = (256)

Public Const PACKETESERVER_NO_LINGER = (4096)

' /****************************************************/
' /****************   BSC     *************************/
' /****************************************************/
' /* bsc3.h */
' /*  "COM1:9600,E,8,1" */
Declare Function BscCancel Lib "MotoCom32" Alias "_BscCancel@4" (ByVal nCid%) As Integer
Declare Function BscChangeTask Lib "MotoCom32" Alias "_BscChangeTask@8" (ByVal nCid%, ByVal task%) As Integer
Declare Function BscClose Lib "MotoCom32" Alias "_BscClose@4" (ByVal nCid%) As Integer
'Declare Function BscCloseAll Lib "MotoCom32" Alias "_BscCloseAll@4" (ByVal nCiddummy%) As Integer
Declare Function BscCommand Lib "MotoCom32" Alias "_BscCommand@16" (ByVal nCid%, ByVal h_buf$, ByVal d_buf$, ByVal fforever%) As Integer
Declare Function BscConnect Lib "MotoCom32" Alias "_BscConnect@4" (ByVal nCid%) As Integer
Declare Function BscContinueJob Lib "MotoCom32" Alias "_BscContinueJob@4" (ByVal nCid%) As Integer
Declare Function BscConvertJobP2R Lib "MotoCom32" Alias "_BscConvertJobP2R@12" (ByVal nCid%, ByVal name_v$, ByVal framename$) As Integer
Declare Function BscConvertJobR2P Lib "MotoCom32" Alias "_BscConvertJobR2P@16" (ByVal nCid%, ByVal name_v$, ByVal cv_type%, ByVal var_no$) As Integer
Declare Function BscDCIGetPos Lib "MotoCom32" Alias "_BscDCIGetPos@16" (ByVal nCid%, type_v%, rconf%, p#) As Integer
Declare Function BscDCIGetPos2 Lib "MotoCom32" Alias "_BscDCIGetPos2@20" (ByVal nCid%, type_v%, rconf%, p#, axisNum%) As Integer
Declare Function BscDCIGetVarData Lib "MotoCom32" Alias "_BscDCIGetVarData@20" (ByVal nCid%, type_v%, rconf%, p#, ByVal s$) As Integer
Declare Function BscDCIGetVarDataEx Lib "MotoCom32" Alias "_BscDCIGetVarDataEx@24" (ByVal nCid%, type_v%, rconf&, p#, ByVal s$, axisNum%) As Integer
Declare Function BscDCILoadSave Lib "MotoCom32" Alias "_BscDCILoadSave@8" (ByVal nCid%, ByVal timec%) As Integer
Declare Function BscDCILoadSaveOnce Lib "MotoCom32" Alias "_BscDCILoadSaveOnce@4" (ByVal nCid%) As Integer
Declare Function BscDCIPutPos Lib "MotoCom32" Alias "_BscDCIPutPos@16" (ByVal nCid%, ByVal type_v%, ByVal rconf%, p#) As Integer
Declare Function BscDCIPutPos2 Lib "MotoCom32" Alias "_BscDCIPutPos2@20" (ByVal nCid%, ByVal type_v%, ByVal rconf%, p#, ByVal axisNum%) As Integer
Declare Function BscDCIPutVarData Lib "MotoCom32" Alias "_BscDCIPutVarData@20" (ByVal nCid%, ByVal type_v%, ByVal rconf%, p#, ByVal s$) As Integer
Declare Function BscDCIPutVarDataEx Lib "MotoCom32" Alias "_BscDCIPutVarDataEx@24" (ByVal nCid%, ByVal type_v%, ByVal rconf&, p#, ByVal s$, ByVal axisNum%) As Integer
'Declare Function BscDelay Lib "MotoCom32" Alias "_BscDelay@8" (ByVal nCid%, ByVal a&) As Integer
Declare Function BscDeleteJob Lib "MotoCom32" Alias "_BscDeleteJob@4" (ByVal nCid%) As Integer
Declare Function BscDisConnect Lib "MotoCom32" Alias "_BscDisConnect@4" (ByVal nCid%) As Integer
Declare Function BscDiskFreeSizeGet Lib "MotoCom32" Alias "_BscDiskFreeSizeGet@12" (ByVal nCid%, ByVal dno%, dsize&) As Integer
Declare Function BscDownLoad Lib "MotoCom32" Alias "_BscDownLoad@8" (ByVal nCid%, ByVal fname$) As Integer
Declare Function BscDownLoadEx Lib "MotoCom32" Alias "_BscDownLoadEx@16" (ByVal nCid%, ByVal fname$, ByVal srcPath$, ByVal nFlg As Boolean) As Integer
Declare Function BscEnforcedClose Lib "MotoCom32" Alias "_BscEnforcedClose@4" (ByVal nCid%) As Integer
Declare Function BscFindFirst Lib "MotoCom32" Alias "_BscFindFirst@12" (ByVal nCid%, ByVal fname$, ByVal size%) As Integer
Declare Function BscFindFirstMaster Lib "MotoCom32" Alias "_BscFindFirstMaster@12" (ByVal nCid%, ByVal fname$, ByVal size%) As Integer
Declare Function BscFindNext Lib "MotoCom32" Alias "_BscFindNext@12" (ByVal nCid%, ByVal fname$, ByVal size%) As Integer
Declare Function BscFindNextMaster Lib "MotoCom32" Alias "_BscFindNextMaster@12" (ByVal nCid%, ByVal fname$, ByVal size%) As Integer
'Declare Function BscGetAbso Lib "MotoCom32" Alias "_BscGetAbso@12" (ByVal nCid%, ByVal axisno%, abso&) As Integer
Declare Function BscGetCartPos Lib "MotoCom32" Alias "_BscGetCartPos@24" (ByVal nCid%, ByVal framename$, ByVal isex%, rconf&, toolno%, p#) As Integer
Declare Function BscGetCtrlGroup Lib "MotoCom32" Alias "_BscGetCtrlGroup@12" (ByVal nCid%, groupinf%, taskinf%) As Integer
Declare Function BscGetCtrlGroupDx Lib "MotoCom32" Alias "_BscGetCtrlGroupDx@16" (ByVal nCid%, robtask&, stattask&, taskinf%) As Integer
Declare Function BscGetCtrlGroupXrc Lib "MotoCom32" Alias "_BscGetCtrlGroupXrc@16" (ByVal nCid%, robtask%, stattask%, taskinf%) As Integer
Declare Function BscGetError Lib "MotoCom32" Alias "_BscGetError@4" (ByVal nCid%) As Integer
Declare Function BscGetError2 Lib "MotoCom32" Alias "_BscGetError2@4" (ByVal nCid%) As Integer
Declare Function BscGetFirstAlarm Lib "MotoCom32" Alias "_BscGetFirstAlarm@8" (ByVal nCid%, data_v%) As Integer
Declare Function BscGetFirstAlarmS Lib "MotoCom32" Alias "_BscGetFirstAlarmS@12" (ByVal nCid%, data_v%, ByVal s$) As Integer
Declare Function BscGetNextAlarm Lib "MotoCom32" Alias "_BscGetNextAlarm@8" (ByVal nCid%, data_v%) As Integer
Declare Function BscGetNextAlarmS Lib "MotoCom32" Alias "_BscGetNextAlarmS@12" (ByVal nCid%, data_v%, ByVal s$) As Integer
Declare Function BscGetPulsePos Lib "MotoCom32" Alias "_BscGetPulsePos@8" (ByVal nCid%, p#) As Integer
Declare Function BscGets Lib "MotoCom32" Alias "_BscGets@16" (ByVal nCid%, ByVal bufptr$, ByVal bsize%, plengets%) As Integer
Declare Function BscGetStatus Lib "MotoCom32" Alias "_BscGetStatus@12" (ByVal nCid%, d1%, d2%) As Integer
Declare Function BscGetUFrame Lib "MotoCom32" Alias "_BscGetUFrame@12" (ByVal nCid%, ByVal ufname$, p#) As Integer
Declare Function BscGetVarData Lib "MotoCom32" Alias "_BscGetVarData@16" (ByVal nCid%, ByVal type_v%, ByVal varno%, pos#) As Integer
Declare Function BscGetVarData2 Lib "MotoCom32" Alias "_BscGetVarData2@16" (ByVal nCid%, ByVal type_v%, ByVal varno%, pos#) As Integer
Declare Function BscGetVarDataEx Lib "MotoCom32" Alias "_BscGetVarDataEx@24" (ByVal nCid%, ByVal type_v%, ByVal varno%, pos#, ByVal s$, axisNum%) As Integer
Declare Function BscHoldOff Lib "MotoCom32" Alias "_BscHoldOff@4" (ByVal nCid%) As Integer
Declare Function BscHoldOn Lib "MotoCom32" Alias "_BscHoldOn@4" (ByVal nCid%) As Integer
Declare Function BscHostGetVarData Lib "MotoCom32" Alias "_BscHostGetVarData@20" (ByVal nCid%, ByVal type_v%, ByVal varno%, pos#, ByVal s$) As Integer
Declare Function BscHostGetVarDataM Lib "MotoCom32" Alias "_BscHostGetVarDataM@20" (ByVal nCid%, ByVal type_v%, ByVal varno%, ByVal num%, pos#) As Integer
Declare Function BscHostPutVarData Lib "MotoCom32" Alias "_BscHostPutVarData@20" (ByVal nCid%, ByVal type_v%, ByVal varno%, dat#, ByVal s$) As Integer
Declare Function BscHostPutVarDataM Lib "MotoCom32" Alias "_BscHostPutVarDataM@20" (ByVal nCid%, ByVal type_v%, ByVal varno%, ByVal num%, dat#) As Integer
Declare Function BscImov Lib "MotoCom32" Alias "_BscImov@28" (ByVal nCid%, ByVal vtype$, ByVal spd#, ByVal framename$, ByVal toolno%, p#) As Integer
Declare Function BscImovEx Lib "MotoCom32" Alias "_BscImovEx@32" (ByVal nCid%, ByVal vtype$, ByVal spd#, ByVal framename$, ByVal toolno%, p#, ByVal axisNum%) As Integer
Declare Function BscImovEx2 Lib "MotoCom32" Alias "_BscImovEx2@36" (ByVal nCid%, ByVal ctype%, ByVal vtype$, ByVal spd#, ByVal framename$, ByVal toolno%, p#, ByVal axisNum%) As Integer
Declare Function BscInBytes Lib "MotoCom32" Alias "_BscInBytes@4" (ByVal nCid%) As Integer
Declare Function BscIsAlarm Lib "MotoCom32" Alias "_BscIsAlarm@4" (ByVal nCid%) As Integer
Declare Function BscIsCtrlGroup Lib "MotoCom32" Alias "_BscIsCtrlGroup@4" (ByVal nCid%) As Integer
Declare Function BscIsCtrlGroupDx Lib "MotoCom32" Alias "_BscIsCtrlGroupDx@12" (ByVal nCid%, robtask&, stattask&) As Integer
Declare Function BscIsCtrlGroupXrc Lib "MotoCom32" Alias "_BscIsCtrlGroupXrc@12" (ByVal nCid%, robtask%, stattask%) As Integer
Declare Function BscIsCycle Lib "MotoCom32" Alias "_BscIsCycle@4" (ByVal nCid%) As Integer
Declare Function BscIsError Lib "MotoCom32" Alias "_BscIsError@4" (ByVal nCid%) As Integer
Declare Function BscIsErrorCode Lib "MotoCom32" Alias "_BscIsErrorCode@4" (ByVal nCid%) As Integer
Declare Function BscIsHold Lib "MotoCom32" Alias "_BscIsHold@4" (ByVal nCid%) As Integer
Declare Function BscIsJobLine Lib "MotoCom32" Alias "_BscIsJobLine@4" (ByVal nCid%) As Integer
Declare Function BscIsJobName Lib "MotoCom32" Alias "_BscIsJobName@12" (ByVal nCid%, ByVal jobname$, ByVal size%) As Integer
Declare Function BscIsJobStep Lib "MotoCom32" Alias "_BscIsJobStep@4" (ByVal nCid%) As Integer
Declare Function BscIsLoc Lib "MotoCom32" Alias "_BscIsLoc@16" (ByVal nCid%, ByVal ispulse%, rconf%, p#) As Integer
Declare Function BscIsPlayMode Lib "MotoCom32" Alias "_BscIsPlayMode@4" (ByVal nCid%) As Integer
Declare Function BscIsRemoteMode Lib "MotoCom32" Alias "_BscIsRemoteMode@4" (ByVal nCid%) As Integer
Declare Function BscIsRobotPos Lib "MotoCom32" Alias "_BscIsRobotPos@24" (ByVal nCid%, ByVal framename$, ByVal isex%, rconf%, toolno%, p#) As Integer
Declare Function BscIsServo Lib "MotoCom32" Alias "_BscIsServo@4" (ByVal nCid%) As Integer
Declare Function BscIsTaskInf Lib "MotoCom32" Alias "_BscIsTaskInf@4" (ByVal nCid%) As Integer
Declare Function BscIsTaskInfDx Lib "MotoCom32" Alias "_BscIsTaskInfDx@4" (ByVal nCid%) As Integer
Declare Function BscIsTaskInfXrc Lib "MotoCom32" Alias "_BscIsTaskInfXrc@4" (ByVal nCid%) As Integer
Declare Function BscIsTeachMode Lib "MotoCom32" Alias "_BscIsTeachMode@4" (ByVal nCid%) As Integer
Declare Function BscJobWait Lib "MotoCom32" Alias "_BscJobWait@8" (ByVal nCid%, ByVal time%) As Integer
Declare Function BscMDSP Lib "MotoCom32" Alias "_BscMDSP@8" (ByVal nCid%, ByVal ptr$) As Integer
Declare Function BscMov Lib "MotoCom32" Alias "_BscMov@36" (ByVal nCid%, ByVal movtype$, ByVal vtype$, ByVal spd#, ByVal framename$, ByVal rconf%, ByVal toolno%, p#) As Integer
Declare Function BscMovEx Lib "MotoCom32" Alias "_BscMovEx@40" (ByVal nCid%, ByVal movtype$, ByVal vtype$, ByVal spd#, ByVal framename$, ByVal rconf&, ByVal toolno%, p#, ByVal axisNum%) As Integer
Declare Function BscMovEx2 Lib "MotoCom32" Alias "_BscMovEx2@44" (ByVal nCid%, ByVal ctype%, ByVal movtype$, ByVal vtype$, ByVal spd#, ByVal framename$, ByVal rconf&, ByVal toolno%, p#, ByVal axisNum%) As Integer
Declare Function BscMovj Lib "MotoCom32" Alias "_BscMovj@28" (ByVal nCid%, ByVal spd#, ByVal framename$, ByVal rconf%, ByVal toolno%, p#) As Integer
Declare Function BscMovjEx Lib "MotoCom32" Alias "_BscMovjEx@32" (ByVal nCid%, ByVal spd#, ByVal framename$, ByVal rconf&, ByVal toolno%, p#, ByVal axisNum%) As Integer
Declare Function BscMovl Lib "MotoCom32" Alias "_BscMovl@32" (ByVal nCid%, ByVal vtype$, ByVal spd#, ByVal framename$, ByVal rconf%, ByVal toolno%, p#) As Integer
Declare Function BscMovlEx Lib "MotoCom32" Alias "_BscMovlEx@36" (ByVal nCid%, ByVal vtype$, ByVal spd#, ByVal framename$, ByVal rconf&, ByVal toolno%, p#, ByVal axisNum%) As Integer
Declare Function BscOpen Lib "MotoCom32" Alias "_BscOpen@8" (ByVal path$, ByVal mode%) As Integer
Declare Function BscOPLock Lib "MotoCom32" Alias "_BscOPLock@4" (ByVal nCid%) As Integer
Declare Function BscOPUnLock Lib "MotoCom32" Alias "_BscOPUnLock@4" (ByVal nCid%) As Integer
Declare Function BscOutBytes Lib "MotoCom32" Alias "_BscOutBytes@4" (ByVal nCid%) As Integer
Declare Function BscPMov Lib "MotoCom32" Alias "_BscPMov@28" (ByVal nCid%, ByVal movtype$, ByVal vtype$, ByVal spd#, ByVal toolno%, p#) As Integer
Declare Function BscPMovEx Lib "MotoCom32" Alias "_BscPMovEx@32" (ByVal nCid%, ByVal movtype$, ByVal vtype$, ByVal spd#, ByVal toolno%, p#, ByVal axisNum%) As Integer
Declare Function BscPMovj Lib "MotoCom32" Alias "_BscPMovj@20" (ByVal nCid%, ByVal spd#, ByVal toolno%, p#) As Integer
Declare Function BscPMovjEx Lib "MotoCom32" Alias "_BscPMovjEx@24" (ByVal nCid%, ByVal spd#, ByVal toolno%, p#, ByVal axisNum%) As Integer
Declare Function BscPMovl Lib "MotoCom32" Alias "_BscPMovl@24" (ByVal nCid%, ByVal vtype$, ByVal spd#, ByVal toolno%, p#) As Integer
Declare Function BscPMovlEx Lib "MotoCom32" Alias "_BscPMovlEx@28" (ByVal nCid%, ByVal vtype$, ByVal spd#, ByVal toolno%, p#, ByVal axisNum%) As Integer
Declare Function BscPuts Lib "MotoCom32" Alias "_BscPuts@12" (ByVal nCid%, ByVal bufptr$, ByVal length%) As Integer
Declare Function BscPutUFrame Lib "MotoCom32" Alias "_BscPutUFrame@12" (ByVal nCid%, ByVal ufname$, p#) As Integer
Declare Function BscPutUFrameEx2 Lib "MotoCom32" Alias "_BscPutUFrameEx2@16" (ByVal nCid%, ByVal ctype%, ByVal ufname$, p#) As Integer
Declare Function BscPutVarData Lib "MotoCom32" Alias "_BscPutVarData@16" (ByVal nCid%, ByVal type_v%, ByVal varno%, dat#) As Integer
Declare Function BscPutVarData2 Lib "MotoCom32" Alias "_BscPutVarData2@20" (ByVal nCid%, ByVal type_v%, ByVal varno%, dat#, ByVal axisNum%) As Integer
Declare Function BscPutVarDataEx Lib "MotoCom32" Alias "_BscPutVarDataEx@24" (ByVal nCid%, ByVal type_v%, ByVal varno%, dat#, ByVal s$, ByVal axisNum%) As Integer
Declare Function BscReadAlarmS Lib "MotoCom32" Alias "_BscReadAlarmS@12" (ByVal nCid%, data_v%, ByVal s$) As Integer
Declare Function BscReadIO Lib "MotoCom32" Alias "_BscReadIO@16" (ByVal nCid%, ByVal startadd%, ByVal ionum%, stat%) As Integer
Declare Function BscReadIO2 Lib "MotoCom32" Alias "_BscReadIO2@16" (ByVal nCid%, ByVal startadd&, ByVal ionum%, stat%) As Integer
Declare Function BscReConnect Lib "MotoCom32" Alias "_BscReConnect@4" (ByVal nCid%) As Integer
Declare Function BscReset Lib "MotoCom32" Alias "_BscReset@4" (ByVal nCid%) As Integer
Declare Function BscReStartJob Lib "MotoCom32" Alias "_BscReStartJob@4" (ByVal nCid%) As Integer
Declare Function BscSelectJob Lib "MotoCom32" Alias "_BscSelectJob@8" (ByVal nCid%, ByVal name_v$) As Integer
Declare Function BscSelectMode Lib "MotoCom32" Alias "_BscSelectMode@8" (ByVal nCid%, ByVal mode%) As Integer
Declare Function BscSelLoopCycle Lib "MotoCom32" Alias "_BscSelLoopCycle@4" (ByVal nCid%) As Integer
Declare Function BscSelOneCycle Lib "MotoCom32" Alias "_BscSelOneCycle@4" (ByVal nCid%) As Integer
Declare Function BscSelStepCycle Lib "MotoCom32" Alias "_BscSelStepCycle@4" (ByVal nCid%) As Integer
Declare Function BscServoOff Lib "MotoCom32" Alias "_BscServoOff@4" (ByVal nCid%) As Integer
Declare Function BscServoOn Lib "MotoCom32" Alias "_BscServoOn@4" (ByVal nCid%) As Integer
'Declare Function BscSetAbso Lib "MotoCom32" Alias "_BscSetAbso@12" (ByVal nCid%, ByVal axisno%, ByVal abso&) As Integer
Declare Function BscSetBreak Lib "MotoCom32" Alias "_BscSetBreak@8" (ByVal nCid%, ByVal flg%) As Integer
Declare Function BscSetCom Lib "MotoCom32" Alias "_BscSetCom@24" (ByVal nCid%, ByVal port%, ByVal baud&, ByVal parity%, ByVal clen%, ByVal stp%) As Integer
Declare Function BscSetCondBSC Lib "MotoCom32" Alias "_BscSetCondBSC@20" (ByVal nCid%, ByVal timerA%, ByVal timerB%, ByVal rtyR%, ByVal rtyW%) As Integer
Declare Function BscSetCtrlGroup Lib "MotoCom32" Alias "_BscSetCtrlGroup@8" (ByVal nCid%, ByVal groupno%) As Integer
Declare Function BscSetCtrlGroupDx Lib "MotoCom32" Alias "_BscSetCtrlGroupDx@12" (ByVal nCid%, ByVal groupno1&, ByVal groupno2&) As Integer
Declare Function BscSetCtrlGroupXrc Lib "MotoCom32" Alias "_BscSetCtrlGroupXrc@12" (ByVal nCid%, ByVal groupno1%, ByVal groupno2%) As Integer
Declare Function BscSetEServer Lib "MotoCom32" Alias "_BscSetEServer@8" (ByVal nCid%, ByVal IPaddr$) As Integer
Declare Function BscSetEServerMode Lib "MotoCom32" Alias "_BscSetEServerMode@12" (ByVal nCid%, ByVal IPaddr$, ByVal Mode%) As Integer
Declare Function BscSetEther Lib "MotoCom32" Alias "_BscSetEther@16" (ByVal nCid%, ByVal IPaddr$, ByVal mode%, ByVal hWnd&) As Integer
'Declare Function BscSetHSL Lib "MotoCom32" Alias "_BscSetHSL@12" (ByVal nCid%, ByVal strName$, ByVal port%) As Integer
Declare Function BscSetLineNumber Lib "MotoCom32" Alias "_BscSetLineNumber@8" (ByVal nCid%, ByVal line_v%) As Integer
Declare Function BscSetMasterJob Lib "MotoCom32" Alias "_BscSetMasterJob@4" (ByVal nCid%) As Integer
Declare Function BscStartJob Lib "MotoCom32" Alias "_BscStartJob@4" (ByVal nCid%) As Integer
Declare Function BscStatus Lib "MotoCom32" Alias "_BscStatus@20" (ByVal nCid%, ByVal hpt$, ByVal dpt$, ByVal sz%, ByVal rbuf$) As Integer
Declare Function BscUpLoad Lib "MotoCom32" Alias "_BscUpLoad@8" (ByVal nCid%, ByVal fname$) As Integer
Declare Function BscUpLoadEx Lib "MotoCom32" Alias "_BscUpLoadEx@16" (ByVal nCid%, ByVal fname$, ByVal desPath$, ByVal nFlg As Boolean) As Integer
'Declare Function BscUpLoadTRQ_HM Lib "MotoCom32" Alias "_BscUpLoadTRQ_HM@16" (ByVal nCid%, ByVal fname$, ByVal nType, ByVal nNo) As Integer
'Declare Function BscUpLoadTRQ Lib "MotoCom32" Alias "_BscUpLoadTRQ@8" (ByVal nCid%, ByVal fname$) As Integer
Declare Function BscWriteIO Lib "MotoCom32" Alias "_BscWriteIO@16" (ByVal nCid%, ByVal startadd%, ByVal ionum%, stat%) As Integer
Declare Function BscWriteIO2 Lib "MotoCom32" Alias "_BscWriteIO2@16" (ByVal nCid%, ByVal startadd&, ByVal ionum%, stat%) As Integer

' /****************************************************/
' /****************   FC1,FC2 *************************/
' /****************************************************/
' /* FCWIN.H */
' /*************** Slave Command /I am FC1. ***********************/
' /* DOS  COM1:96,E,8,1 */
' /* Win FC1 COM1:4800,E,8,1 */
' /* Win FC2 COM1:19200,E,8,1 */
Declare Function FcClose Lib "MotoCom32" Alias "_FcClose@4" (ByVal nCid%) As Integer
Declare Function FcConnect Lib "MotoCom32" Alias "_FcConnect@4" (ByVal nCid%) As Integer
Declare Function FcDisConnect Lib "MotoCom32" Alias "_FcDisConnect@4" (ByVal nCid%) As Integer
Declare Function FcGetCom Lib "MotoCom32" Alias "_FcGetCom@12" (ByVal nCid%, ByVal buf$, ByVal size%) As Integer
Declare Function FcGetData Lib "MotoCom32" Alias "_FcGetData@12" (ByVal nCid%, ByVal buf$, ByVal size%) As Integer
Declare Function FcGetPath Lib "MotoCom32" Alias "_FcGetPath@12" (ByVal nCid%, ByVal buf$, ByVal size%) As Integer
Declare Function FcOpen Lib "MotoCom32" Alias "_FcOpen@8" (ByVal path$, ByVal mode%) As Integer
Declare Function FcProc Lib "MotoCom32" Alias "_FcProc@8" (ByVal nCid%, ByVal isremove%) As Integer
Declare Function FcScan Lib "MotoCom32" Alias "_FcScan@4" (ByVal nCid%) As Integer
Declare Function FcSetBreak Lib "MotoCom32" Alias "_FcSetBreak@8" (ByVal nCid%, ByVal flg%) As Integer
Declare Function FcSetCom Lib "MotoCom32" Alias "_FcSetCom@24" (ByVal nCid%, ByVal port%, ByVal baud&, ByVal parity%, ByVal clen%, ByVal stp%) As Integer
Declare Function FcSetHSL Lib "MotoCom32" Alias "_FcSetHSL@12" (ByVal nCid%, ByVal strName$, ByVal port%) As Integer

' /*************** Master Command /I am ERC.***********************/
Declare Function FcAutoDeleteSet Lib "MotoCom32" Alias "_FcAutoDeleteSet@8" (ByVal nCid%, ByVal flg%) As Integer
Declare Function FcBye Lib "MotoCom32" Alias "_FcBye@4" (ByVal nCid%) As Integer
Declare Function FcChangeDir Lib "MotoCom32" Alias "_FcChangeDir@8" (ByVal nCid%, ByVal dname$) As Integer
Declare Function FcChangeDrive Lib "MotoCom32" Alias "_FcChangeDrive@8" (ByVal nCid%, ByVal type_v$) As Integer
Declare Function FcDiskSize Lib "MotoCom32" Alias "_FcDiskSize@12" (ByVal nCid%, ByVal disksize$, ByVal size%) As Integer
Declare Function FcDiskSizeSet Lib "MotoCom32" Alias "_FcDiskSizeSet@8" (ByVal nCid%, ByVal flg%) As Integer
Declare Function FcDownload Lib "MotoCom32" Alias "_FcDownload@8" (ByVal nCid%, ByVal fname$) As Integer
Declare Function FcFileSize Lib "MotoCom32" Alias "_FcFileSize@12" (ByVal nCid%, ByVal fname$, size&) As Integer
Declare Function FcFindCurrentDir Lib "MotoCom32" Alias "_FcFindCurrentDir@12" (ByVal nCid%, ByVal dname$, ByVal size%) As Integer
Declare Function FcFindFirst Lib "MotoCom32" Alias "_FcFindFirst@12" (ByVal nCid%, ByVal fname$, ByVal size%) As Integer
Declare Function FcFindFirstDir Lib "MotoCom32" Alias "_FcFindFirstDir@12" (ByVal nCid%, ByVal dname$, ByVal size%) As Integer
Declare Function FcFindNext Lib "MotoCom32" Alias "_FcFindNext@12" (ByVal nCid%, ByVal fname$, ByVal size%) As Integer
Declare Function FcFindNextDir Lib "MotoCom32" Alias "_FcFindNextDir@12" (ByVal nCid%, ByVal dname$, ByVal size%) As Integer
Declare Function FcIsDiskSize Lib "MotoCom32" Alias "_FcIsDiskSize@4" (ByVal nCid%) As Integer
Declare Function FcLogin Lib "MotoCom32" Alias "_FcLogin@4" (ByVal nCid%) As Integer
Declare Function FcLogout Lib "MotoCom32" Alias "_FcLogout@4" (ByVal nCid%) As Integer
Declare Function FcMakeDir Lib "MotoCom32" Alias "_FcMakeDir@8" (ByVal nCid%, ByVal dname$) As Integer
Declare Function FcRemove Lib "MotoCom32" Alias "_FcRemove@8" (ByVal nCid%, ByVal fname$) As Integer
Declare Function FcRename Lib "MotoCom32" Alias "_FcRename@12" (ByVal nCid%, ByVal oldname$, ByVal newname$) As Integer
Declare Function FcUpload Lib "MotoCom32" Alias "_FcUpload@8" (ByVal nCid%, ByVal fname$) As Integer


'#ifdef __cplusplus
'}
'#endif
