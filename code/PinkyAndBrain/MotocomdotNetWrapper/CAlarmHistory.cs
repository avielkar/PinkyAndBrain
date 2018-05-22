using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

namespace MotocomdotNetWrapper
{
    public class CAlarmHistory
    {
        string m_strFilename;
        string[] m_strAlarmType = { @"///MAJOR", @"///MINOR", @"///IO_SYS",@"///IO_USR",@"///OFFLINE",@"///END" };
        public ArrayList AlarmHistoryItems = new ArrayList();
        public CAlarmHistory(string _Filename)
        {
            m_strFilename=_Filename;
            ReadAlarmFile();
        }        
        public CAlarmHistory()
        {

        }
        public void UpdateAlarmFile(string _Filename)
        {
            m_strFilename=_Filename;
            ReadAlarmFile();

        }
        public void ReadAlarmFile()
        {
            //Read alarm file
            string strContent="";
            StreamReader srAlarmFile = new StreamReader(m_strFilename, System.Text.Encoding.ASCII);
            strContent = srAlarmFile.ReadToEnd();
            srAlarmFile.Close();

            //split in lines
            string[] strSep={"\r\n"};
            string[] strArr=strContent.Split(strSep,StringSplitOptions.None);

            //separate alarm history items
            int i=0,AlarmType=-1;
            string AlarmDataItem="";
            int AlarmSubLine = 0;
            int ParseResult=-1;
            AlarmHistoryItems.Clear();

            for (int j = 0; j < strArr.Length; j++)
            {
                if (strArr[j].Contains(m_strAlarmType[i]))
                {
                    AlarmType = i;
                    i++;
                }
                else if (strArr[j].Length == 0)
                    continue;
                else if (int.TryParse(strArr[j], out ParseResult))
                    continue;
                else if (strArr[j].Substring(0, 2) == @"//")
                    continue;
                else
                {
                    strArr[j] = CheckLineForInternalComma(strArr[j]);
                    if (AlarmSubLine == 0)
                    {
                        AlarmDataItem = strArr[j];
                        AlarmSubLine = 1;
                    }
                    else if (AlarmSubLine == 1)
                    {
                        AlarmDataItem = AlarmDataItem + strArr[j];
                        AlarmSubLine = 2;
                    }
                    else if (AlarmSubLine == 2)
                    {
                        AlarmDataItem = AlarmDataItem + "," + strArr[j] + "," + m_strAlarmType[AlarmType];
                        AlarmSubLine = 0;
                        CAlarmHistoryItem almData = new CAlarmHistoryItem(AlarmDataItem);
                        AlarmHistoryItems.Add(almData);
                    }
                }
            }
            AlarmHistoryItems.Sort();
            AlarmHistoryItems.Reverse();
        }
        string CheckLineForInternalComma(string AlarmHistoryLine)
        {
            string newString=AlarmHistoryLine;

            // Define a regular expression for internal commas.
            Regex rx = new Regex(@"(\[[^\]]*\,[^\[]*\])",RegexOptions.Compiled | RegexOptions.IgnoreCase);

            // Find matches.
            MatchCollection matches = rx.Matches(AlarmHistoryLine);

            // Report the number of matches found.
            //Console.WriteLine("{0} matches found in:\n   {1}",matches.Count,AlarmHistoryLine);

            // Report on each match.
            foreach (Match match in matches)
            {
                GroupCollection groups = match.Groups;
                newString = newString.Substring(0, groups[0].Index) + groups[0].Value.Replace(',', ';') + newString.Substring(groups[0].Index + groups[0].Value.Length);
                //Console.WriteLine("'{0}' repeated at positions {1}",groups[0].Value,groups[0].Index);
            }

            return newString;
        }

    }

 
}
