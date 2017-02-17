using System;
using System.Collections.Generic;
using System.Text;

namespace MotocomdotNetWrapper
{
    public class CRobPosVar
    {
        public CRobPosVar():this(PosVarType.XYZ)
        {
        }

        public CRobPosVar(int saxis,int laxis,int uaxis,int raxis, int baxis,int taxis,int e7axis,int e8axis,short toolno)
        {
            DataType = PosVarType.Pulse;
            SAxis = saxis;
            LAxis = laxis;
            UAxis = uaxis;
            RAxis = raxis;
            BAxis = baxis;
            TAxis = taxis;
            E7Axis = e7axis;
            E8Axis = e8axis;
            ToolNo = toolno;
        }

        public CRobPosVar(FrameType frame,double x,double y,double z, double rx,double ry,double rz,short formcode,short toolno)
        {
            DataType = PosVarType.XYZ;
            Frame = frame;
            X = x;
            Y = y;
            Z = z;
            Rx = rx;
            Ry = ry;
            Rz = rz;
            Formcode = formcode;
            ToolNo = toolno;
        }

        public CRobPosVar(PosVarType datatype)
        {
            DataType=datatype;
            if (datatype == PosVarType.Pulse)
            {
                SAxis = 0;
                LAxis = 0;
                UAxis = 0;
                RAxis = 0;
                BAxis = 0;
                TAxis = 0;
                E7Axis = 0;
                E8Axis = 0;
                ToolNo = 0;
            }
            else
            {
                Frame = FrameType.Robot;
                X = 0.0;
                Y = 0.0;
                Z = 0.0;
                Rx = 0.0;
                Ry = 0.0;
                Rz = 0.0;
                Formcode = 0;
                ToolNo = 0;
            }
        }
        
        public CRobPosVar(double[] HostGetVarDataArray)
        {
            HostGetVarDataArray.CopyTo(m_HostGetVarDataArray,0);
        }


        public PosVarType DataType
        {
            get { return (PosVarType)m_HostGetVarDataArray[0]; }
            set { m_HostGetVarDataArray[0] = (double)value; }
        }

        public int SAxis
        {
            get { return (int)m_HostGetVarDataArray[1]; }
            set { m_HostGetVarDataArray[1] = (double)value; }
        }

        public FrameType Frame
        {
            get { return (FrameType)m_HostGetVarDataArray[1]; }
            set { m_HostGetVarDataArray[1] = (double)value; }
        }

        public int LAxis
        {
            get { return (int)m_HostGetVarDataArray[2]; }
            set { m_HostGetVarDataArray[2] = (double)value; }
        }

        public double X
        {
            get { return m_HostGetVarDataArray[2]; }
            set { m_HostGetVarDataArray[2] = value; }
        }

        public int UAxis
        {
            get { return (int)m_HostGetVarDataArray[3]; }
            set { m_HostGetVarDataArray[3] = (double)value; }
        }

        public double Y
        {
            get { return m_HostGetVarDataArray[3]; }
            set { m_HostGetVarDataArray[3] = value; }
        }

        public int RAxis
        {
            get { return (int)m_HostGetVarDataArray[4]; }
            set { m_HostGetVarDataArray[4] = (double)value; }
        }

        public double Z
        {
            get { return m_HostGetVarDataArray[4]; }
            set { m_HostGetVarDataArray[4] = value; }
        }

        public int BAxis
        {
            get { return (int)m_HostGetVarDataArray[5]; }
            set { m_HostGetVarDataArray[5] = (double)value; }
        }

        public double Rx
        {
            get { return m_HostGetVarDataArray[5]; }
            set { m_HostGetVarDataArray[5] = value; }
        }

        public int TAxis
        {
            get { return (int)m_HostGetVarDataArray[6]; }
            set { m_HostGetVarDataArray[6] = (double)value; }
        }

        public double Ry
        {
            get { return m_HostGetVarDataArray[6]; }
            set { m_HostGetVarDataArray[6] = value; }
        }

        public int E7Axis
        {
            get { return (int)m_HostGetVarDataArray[7]; }
            set { m_HostGetVarDataArray[7] = (double)value; }
        }

        public double Rz
        {
            get { return m_HostGetVarDataArray[7]; }
            set { m_HostGetVarDataArray[7] = value; }
        }

        public int E8Axis
        {
            get { return (int)m_HostGetVarDataArray[8]; }
            set { m_HostGetVarDataArray[8] = (double)value; }
        }

        public short Formcode
        {
            get { return (short)m_HostGetVarDataArray[8]; }
            set { m_HostGetVarDataArray[8] = (double)value; }
        }

        public short ToolNo
        {
            get { return (short)m_HostGetVarDataArray[9]; }
            set { m_HostGetVarDataArray[9] = (double)value; }
        }


        double[] m_HostGetVarDataArray = new double[12];

        public double[] HostGetVarDataArray
        {
            get
            {

                return m_HostGetVarDataArray;
            }

            set
            {
                m_HostGetVarDataArray = value;
            }
        }
    }
}
