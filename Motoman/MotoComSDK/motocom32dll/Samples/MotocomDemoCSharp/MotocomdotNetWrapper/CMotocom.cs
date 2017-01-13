using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace MotocomdotNetWrapper
{
    class CMotocom
    {
        public const int MaxJobNameLength = 32;

        #region PInvokes

        [DllImport("MotoCom32.dll", EntryPoint = "_BscCancel@4")]
        public static extern short BscCancel(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscChangeTask@8")]
        public static extern short BscChangeTask(short nCid, short task);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscClose@4")]
        public static extern short BscClose(short nCid);

        // Declare Function BscCloseAll Lib "MotoCom32" Alias "_BscCloseAll@4" (ByVal nCiddummy%) As Integer
        [DllImport("MotoCom32.dll", EntryPoint = "_BscCommand@16")]
        public static extern short BscCommand(short nCid, StringBuilder h_buf, StringBuilder d_buf, short fforever);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscConnect@4")]
        public static extern short BscConnect(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscContinueJob@4")]
        public static extern short BscContinueJob(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscConvertJobP2R@12")]
        public static extern short BscConvertJobP2R(short nCid, StringBuilder name_v, StringBuilder framename);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscConvertJobR2P@16")]
        public static extern short BscConvertJobR2P(short nCid, StringBuilder name_v, short cv_type, StringBuilder var_no);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscDCIGetPos@16")]
        public static extern short BscDCIGetPos(short nCid, ref short type_v, ref short rconf, ref double p);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscDCIGetPos2@20")]
        public static extern short BscDCIGetPos2(short nCid, ref short type_v, ref short rconf, ref double p, ref short axisNum);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscDCIGetVarData@20")]
        public static extern short BscDCIGetVarData(short nCid, ref short type_v, ref short rconf, ref double p, StringBuilder s);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscDCIGetVarDataEx@24")]
        public static extern short BscDCIGetVarDataEx(short nCid, ref short type_v, ref int rconf, ref double p, StringBuilder s, ref short axisNum);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscDCILoadSave@8")]
        public static extern short BscDCILoadSave(short nCid, short timec);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscDCILoadSaveOnce@4")]
        public static extern short BscDCILoadSaveOnce(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscDCIPutPos@16")]
        public static extern short BscDCIPutPos(short nCid, short type_v, short rconf, ref double p);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscDCIPutPos2@20")]
        public static extern short BscDCIPutPos2(short nCid, short type_v, short rconf, ref double p, short axisNum);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscDCIPutVarData@20")]
        public static extern short BscDCIPutVarData(short nCid, short type_v, short rconf, ref double p, StringBuilder s);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscDCIPutVarDataEx@24")]
        public static extern short BscDCIPutVarDataEx(short nCid, short type_v, int rconf, ref double p, StringBuilder s, short axisNum);

        // Declare Function BscDelay Lib "MotoCom32" Alias "_BscDelay@8" (ByVal nCid%, ByVal a&) As Integer
        [DllImport("MotoCom32.dll", EntryPoint = "_BscDeleteJob@4")]
        public static extern short BscDeleteJob(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscDisConnect@4")]
        public static extern short BscDisConnect(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscDiskFreeSizeGet@12")]
        public static extern short BscDiskFreeSizeGet(short nCid, short dno, ref int dsize);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscDownLoad@8")]
        public static extern short BscDownLoad(short nCid, StringBuilder fname);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscDownLoadEx@16")]
        public static extern short BscDownLoadEx(short nCid, StringBuilder fname, StringBuilder srcPath, bool nFlg);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscEnforcedClose@4")]
        public static extern short BscEnforcedClose(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscFindFirst@12")]
        public static extern short BscFindFirst(short nCid, StringBuilder fname, short size);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscFindFirstMaster@12")]
        public static extern short BscFindFirstMaster(short nCid, StringBuilder fname, short size);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscFindNext@12")]
        public static extern short BscFindNext(short nCid, StringBuilder fname, short size);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscFindNextMaster@12")]
        public static extern short BscFindNextMaster(short nCid, StringBuilder fname, short size);

        // Declare Function BscGetAbso Lib "MotoCom32" Alias "_BscGetAbso@12" (ByVal nCid%, ByVal axisno%, abso&) As Integer
        [DllImport("MotoCom32.dll", EntryPoint = "_BscGetCartPos@24")]
        public static extern short BscGetCartPos(short nCid, StringBuilder framename, short isex, ref int rconf, ref short toolno, ref double p);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscGetCtrlGroup@12")]
        public static extern short BscGetCtrlGroup(short nCid, ref short groupinf, ref short taskinf);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscGetCtrlGroupDx@16")]
        public static extern short BscGetCtrlGroupDx(short nCid, ref int robtask, ref int stattask, ref short taskinf);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscGetCtrlGroupXrc@16")]
        public static extern short BscGetCtrlGroupXrc(short nCid, ref short robtask, ref short stattask, ref short taskinf);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscGetError@4")]
        public static extern short BscGetError(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscGetError2@4")]
        public static extern short BscGetError2(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscGetFirstAlarm@8")]
        public static extern short BscGetFirstAlarm(short nCid, ref short data_v);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscGetFirstAlarmS@12")]
        public static extern short BscGetFirstAlarmS(short nCid, ref short data_v, StringBuilder s);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscGetNextAlarm@8")]
        public static extern short BscGetNextAlarm(short nCid, ref short data_v);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscGetNextAlarmS@12")]
        public static extern short BscGetNextAlarmS(short nCid, ref short data_v, StringBuilder s);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscGetPulsePos@8")]
        public static extern short BscGetPulsePos(short nCid, ref double p);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscGets@16")]
        public static extern short BscGets(short nCid, StringBuilder bufptr, short bsize, ref short plengets);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscGetStatus@12")]
        public static extern short BscGetStatus(short nCid, ref short d1, ref short d2);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscGetUFrame@12")]
        public static extern short BscGetUFrame(short nCid, StringBuilder ufname, ref double p);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscGetVarData@16")]
        public static extern short BscGetVarData(short nCid, short type_v, short varno, ref double pos);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscGetVarData2@16")]
        public static extern short BscGetVarData2(short nCid, short type_v, short varno, ref double pos);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscGetVarDataEx@24")]
        public static extern short BscGetVarDataEx(short nCid, short type_v, short varno, ref double pos, StringBuilder s, ref short axisNum);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscHoldOff@4")]
        public static extern short BscHoldOff(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscHoldOn@4")]
        public static extern short BscHoldOn(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscHostGetVarData@20")]
        public static extern short BscHostGetVarData(short nCid, short type_v, short varno, ref double pos, StringBuilder s);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscHostGetVarDataM@20")]
        public static extern short BscHostGetVarDataM(short nCid, short type_v, short varno, short num, ref double pos);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscHostPutVarData@20")]
        public static extern short BscHostPutVarData(short nCid, short type_v, short varno, ref double dat, StringBuilder s);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscHostPutVarDataM@20")]
        public static extern short BscHostPutVarDataM(short nCid, short type_v, short varno, short num, ref double dat);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscImov@28")]
        public static extern short BscImov(short nCid, StringBuilder vtype, double spd, StringBuilder framename, short toolno, ref double p);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscImovEx@32")]
        public static extern short BscImovEx(short nCid, StringBuilder vtype, double spd, StringBuilder framename, short toolno, ref double p, short axisNum);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscInBytes@4")]
        public static extern short BscInBytes(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscIsAlarm@4")]
        public static extern short BscIsAlarm(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscIsCtrlGroup@4")]
        public static extern short BscIsCtrlGroup(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscIsCtrlGroupDx@12")]
        public static extern short BscIsCtrlGroupDx(short nCid, ref int robtask, ref int stattask);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscIsCtrlGroupXrc@12")]
        public static extern short BscIsCtrlGroupXrc(short nCid, ref short robtask, ref short stattask);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscIsCycle@4")]
        public static extern short BscIsCycle(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscIsError@4")]
        public static extern short BscIsError(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscIsErrorCode@4")]
        public static extern short BscIsErrorCode(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscIsHold@4")]
        public static extern short BscIsHold(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscIsJobLine@4")]
        public static extern short BscIsJobLine(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscIsJobName@12")]
        public static extern short BscIsJobName(short nCid, StringBuilder jobname, short size);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscIsJobStep@4")]
        public static extern short BscIsJobStep(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscIsLoc@16")]
        public static extern short BscIsLoc(short nCid, short ispulse, ref short rconf, ref double p);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscIsPlayMode@4")]
        public static extern short BscIsPlayMode(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscIsRemoteMode@4")]
        public static extern short BscIsRemoteMode(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscIsRobotPos@24")]
        public static extern short BscIsRobotPos(short nCid, StringBuilder framename, short isex, ref short rconf, ref short toolno, ref double p);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscIsServo@4")]
        public static extern short BscIsServo(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscIsTaskInf@4")]
        public static extern short BscIsTaskInf(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscIsTaskInfDx@4")]
        public static extern short BscIsTaskInfDx(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscIsTaskInfXrc@4")]
        public static extern short BscIsTaskInfXrc(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscIsTeachMode@4")]
        public static extern short BscIsTeachMode(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscJobWait@8")]
        public static extern short BscJobWait(short nCid, short time);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscMDSP@8")]
        public static extern short BscMDSP(short nCid, StringBuilder ptr);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscMov@36")]
        public static extern short BscMov(short nCid, StringBuilder movtype, StringBuilder vtype, double spd, StringBuilder framename, short rconf, short toolno, ref double p);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscMovEx@40")]
        public static extern short BscMovEx(short nCid, StringBuilder movtype, StringBuilder vtype, double spd, StringBuilder framename, int rconf, short toolno, ref double p, short axisNum);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscMovj@28")]
        public static extern short BscMovj(short nCid, double spd, StringBuilder framename, short rconf, short toolno, ref double p);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscMovjEx@32")]
        public static extern short BscMovjEx(short nCid, double spd, StringBuilder framename, int rconf, short toolno, ref double p, short axisNum);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscMovl@32")]
        public static extern short BscMovl(short nCid, StringBuilder vtype, double spd, StringBuilder framename, short rconf, short toolno, ref double p);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscMovlEx@36")]
        public static extern short BscMovlEx(short nCid, StringBuilder vtype, double spd, StringBuilder framename, int rconf, short toolno, ref double p, short axisNum);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscOpen@8")]
        public static extern short BscOpen(string path, short mode);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscOPLock@4")]
        public static extern short BscOPLock(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscOPUnLock@4")]
        public static extern short BscOPUnLock(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscOutBytes@4")]
        public static extern short BscOutBytes(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscPMov@28")]
        public static extern short BscPMov(short nCid, StringBuilder movtype, StringBuilder vtype, double spd, short toolno, ref double p);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscPMovEx@32")]
        public static extern short BscPMovEx(short nCid, StringBuilder movtype, StringBuilder vtype, double spd, short toolno, ref double p, short axisNum);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscPMovj@20")]
        public static extern short BscPMovj(short nCid, double spd, short toolno, ref double p);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscPMovjEx@24")]
        public static extern short BscPMovjEx(short nCid, double spd, short toolno, ref double p, short axisNum);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscPMovl@24")]
        public static extern short BscPMovl(short nCid, StringBuilder vtype, double spd, short toolno, ref double p);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscPMovlEx@28")]
        public static extern short BscPMovlEx(short nCid, StringBuilder vtype, double spd, short toolno, ref double p, short axisNum);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscPuts@12")]
        public static extern short BscPuts(short nCid, StringBuilder bufptr, short length);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscPutUFrame@12")]
        public static extern short BscPutUFrame(short nCid, StringBuilder ufname, ref double p);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscPutVarData@16")]
        public static extern short BscPutVarData(short nCid, short type_v, short varno, ref double dat);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscPutVarData2@20")]
        public static extern short BscPutVarData2(short nCid, short type_v, short varno, ref double dat, short axisNum);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscPutVarDataEx@24")]
        public static extern short BscPutVarDataEx(short nCid, short type_v, short varno, ref double dat, StringBuilder s, short axisNum);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscReadAlarmS@12")]
        public static extern short BscReadAlarmS(short nCid, ref short data_v, StringBuilder s);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscReadIO@16")]
        public static extern short BscReadIO(short nCid, short startadd, short ionum, ref short stat);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscReadIO2@16")]
        public static extern short BscReadIO2(short nCid, int startadd, short ionum, ref short stat);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscReConnect@4")]
        public static extern short BscReConnect(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscReset@4")]
        public static extern short BscReset(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscReStartJob@4")]
        public static extern short BscReStartJob(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscSelectJob@8")]
        public static extern short BscSelectJob(short nCid, string name_v);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscSelectMode@8")]
        public static extern short BscSelectMode(short nCid, short mode);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscSelLoopCycle@4")]
        public static extern short BscSelLoopCycle(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscSelOneCycle@4")]
        public static extern short BscSelOneCycle(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscSelStepCycle@4")]
        public static extern short BscSelStepCycle(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscServoOff@4")]
        public static extern short BscServoOff(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscServoOn@4")]
        public static extern short BscServoOn(short nCid);

        // Declare Function BscSetAbso Lib "MotoCom32" Alias "_BscSetAbso@12" (ByVal nCid%, ByVal axisno%, ByVal abso&) As Integer
        [DllImport("MotoCom32.dll", EntryPoint = "_BscSetBreak@8")]
        public static extern short BscSetBreak(short nCid, short flg);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscSetCom@24")]
        public static extern short BscSetCom(short nCid, short port, int baud, short parity, short clen, short stp);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscSetCondBSC@20")]
        public static extern short BscSetCondBSC(short nCid, short timerA, short timerB, short rtyR, short rtyW);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscSetCtrlGroup@8")]
        public static extern short BscSetCtrlGroup(short nCid, short groupno);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscSetCtrlGroupDx@12")]
        public static extern short BscSetCtrlGroupDx(short nCid, int groupno1, int groupno2);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscSetCtrlGroupXrc@12")]
        public static extern short BscSetCtrlGroupXrc(short nCid, short groupno1, short groupno2);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscSetEServer@8")]
        public static extern short BscSetEServer(short nCid, string IPaddr);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscSetEServerMode@12")]
        public static extern short BscSetEServerMode(short nCid, StringBuilder IPaddr, short mode);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscSetEther@16")]
        public static extern short BscSetEther(short nCid, StringBuilder IPaddr, short mode, int hWnd);

        // Declare Function BscSetHSL Lib "MotoCom32" Alias "_BscSetHSL@12" (ByVal nCid%, ByVal strName$, ByVal port%) As Integer
        [DllImport("MotoCom32.dll", EntryPoint = "_BscSetLineNumber@8")]
        public static extern short BscSetLineNumber(short nCid, short line_v);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscSetMasterJob@4")]
        public static extern short BscSetMasterJob(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscStartJob@4")]
        public static extern short BscStartJob(short nCid);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscStatus@20")]
        public static extern short BscStatus(short nCid, StringBuilder hpt, StringBuilder dpt, short sz, StringBuilder rbuf);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscUpLoad@8")]
        public static extern short BscUpLoad(short nCid, StringBuilder fname);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscUpLoadEx@16")]
        public static extern short BscUpLoadEx(short nCid, StringBuilder fname, StringBuilder desPath, bool nFlg);

        // Declare Function BscUpLoadTRQ_HM Lib "MotoCom32" Alias "_BscUpLoadTRQ_HM@16" (ByVal nCid%, ByVal fname$, ByVal nType, ByVal nNo) As Integer
        // Declare Function BscUpLoadTRQ Lib "MotoCom32" Alias "_BscUpLoadTRQ@8" (ByVal nCid%, ByVal fname$) As Integer
        [DllImport("MotoCom32.dll", EntryPoint = "_BscWriteIO@16")]
        public static extern short BscWriteIO(short nCid, short startadd, short ionum, ref short stat);

        [DllImport("MotoCom32.dll", EntryPoint = "_BscWriteIO2@16")]
        public static extern short BscWriteIO2(short nCid, int startadd, short ionum, ref short stat);

        #endregion
    }
}
