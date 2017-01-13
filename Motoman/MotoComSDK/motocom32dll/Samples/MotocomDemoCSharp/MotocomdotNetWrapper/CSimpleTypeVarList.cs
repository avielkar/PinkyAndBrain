using System;
using System.Collections.Generic;
using System.Text;

namespace MotocomdotNetWrapper
{
    public class CSimpleTypeVarList
    {
        public CSimpleTypeVarList()
        {
        }

        public CSimpleTypeVarList(VarType vartype,short listsize,short startindex)
        {
            VarType = vartype;
            ListSize = listsize;
            StartIndex = startindex;
        }

        public CSimpleTypeVarList(VarType vartype,short listsize,short startindex,double[] varlistarray)
        {
            VarType=vartype;
            ListSize = listsize;
            StartIndex = startindex;
            varlistarray.CopyTo(VarListArray,0);
        }


        VarType m_VarType;
        public VarType VarType {
            get
            {
                return m_VarType;
            }
            set
            {
                if ((int)value < 0 || (int)value >3 )
                    throw new Exception("Invalid variable type !");              
                m_VarType=value;
            }
        }

        short m_ListSize=1;
        public short ListSize
        {
            get
            {
                return m_ListSize;
            }
            set
            {
                m_ListSize=value;
                if (value > 15 || value <1)
                    throw new ArgumentOutOfRangeException("Invalid ListSize: 0<ListSize<=15 !");
            }
        }

        short m_StartIndex = 0;
        public short StartIndex
        {
            get
            {
                return m_StartIndex;
            }
            set
            {
                m_StartIndex = value;
                //todo:check variable limits
            }
        }

        public double[] VarListArray = new double[15];
        public double this[int index]
        {
            get
            {
                return VarListArray[index];
            }
            set
            {
                if (index > m_ListSize - 1)
                    throw new ArgumentOutOfRangeException("Index bigger than ListSize !");
                VarListArray[index] = value;
            }
        }
    }
}
