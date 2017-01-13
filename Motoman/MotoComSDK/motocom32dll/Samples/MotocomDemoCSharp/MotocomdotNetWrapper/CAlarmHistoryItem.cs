using System;
using System.Collections.Generic;
using System.Text;

namespace MotocomdotNetWrapper
{
    public class CAlarmHistoryItem:IComparable
    {
        public short Code = -1;
        public short Subcode = -1;
        public string SubcodeText = "";
        public string Message = "";
        public string Group = "";
        public string Axis = "";
        public string Job = "";
        public string Line = "";
        public string Step = "";
        public string Type = "";

        private string[] AlarmParam;
        public DateTime Date;
        public CAlarmHistoryItem(short _AlarmNo, short _AlarmSubNo, string _AlarmMsg)
        {
            Code = _AlarmNo;
            Subcode = _AlarmSubNo;
            SubcodeText = Subcode.ToString();
            Message = _AlarmMsg;
        }

        public CAlarmHistoryItem(string AlarmHistoryItemString)
        {
            ParseAlarmHistoryItem(AlarmHistoryItemString);
        }

        public CAlarmHistoryItem()
        {

        }

        public void ParseAlarmHistoryItem(string AlarmHistoryItemString)
        {
            AlarmParam=AlarmHistoryItemString.Split(',');
            Code = short.Parse(AlarmParam[0]);
            Message = AlarmParam[1];
            if (AlarmParam[2].Length == 0)
                SubcodeText = AlarmParam[3];
            else
            {
                Group = AlarmParam[2];
                Axis = AlarmParam[4];
            }
            Job = AlarmParam[5];
            Line = AlarmParam[6];
            Step= AlarmParam[7];
            Date = new DateTime(int.Parse(AlarmParam[8].Substring(0, 4)), int.Parse(AlarmParam[8].Substring(5, 2)), int.Parse(AlarmParam[8].Substring(8, 2)),int.Parse(AlarmParam[8].Substring(11, 2)), int.Parse(AlarmParam[8].Substring(14, 2)), 0);
            Type= AlarmParam[9].Substring(3);





        }

        //implement IComparable
        public int CompareTo(object obj)
        {
            // prüfen, ob der Parameter ein null-Verweis ist   
            if (obj == null) return 1;
            // prüfen, ob beide Typen gleich sind   
            if (obj.GetType() != this.GetType())
                throw new ArgumentException("Ungültiger Vergleich");
            // Vergleich der beiden Objekte   
            CAlarmHistoryItem val = (CAlarmHistoryItem)obj;
            return this.Date.CompareTo(val.Date);
        }

    }
}
