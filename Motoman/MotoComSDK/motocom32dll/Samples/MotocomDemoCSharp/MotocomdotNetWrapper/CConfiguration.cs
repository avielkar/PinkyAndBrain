using System;
using System.Collections.Generic;
using System.Text;

namespace MotocomdotNetWrapper
{
    public class CConfiguration
    {
        public CConfiguration(short formcode)
        {
            m_Formcode = formcode;
        }

        public CConfiguration(bool flip,bool elbowabove,bool frontside,bool rbelow180,bool tbelow180,bool sbelow180)
        {
            Flip = flip;
            ElbowAbove = elbowabove;
            FrontSide = frontside;
            Rbelow180 = rbelow180;
            Tbelow180 = tbelow180;
            Sbelow180 = sbelow180;
        }

        int m_Formcode = 0;

        public short Formcode
        {
            get { return (short)m_Formcode; }
            set { m_Formcode = (int)value; }
        }

        public bool Flip
        {
            get { return (m_Formcode & (1 << 0)) > 0 ? true : false; }
            set { m_Formcode = value ? m_Formcode | (1 << 0):m_Formcode & ~(1 << 0) ; }
        }
        public bool ElbowAbove
        {
            get { return (m_Formcode & (1 << 1)) > 0 ? true : false; }
            set { m_Formcode = value ? m_Formcode | (1 << 1) : m_Formcode & ~(1 << 1); }
        }
        public bool FrontSide
        {
            get { return (m_Formcode & (1 << 2)) > 0 ? true : false; }
            set { m_Formcode = value ? m_Formcode | (1 << 2) : m_Formcode & ~(1 << 2); }
        }
        public bool Rbelow180
        {
            get { return (m_Formcode & (1 << 3)) > 0 ? true : false; }
            set { m_Formcode = value ? m_Formcode | (1 << 3) : m_Formcode & ~(1 << 3); }
        }
        public bool Tbelow180
        {
            get { return (m_Formcode & (1 << 4)) > 0 ? true : false; }
            set { m_Formcode = value ? m_Formcode | (1 << 4) : m_Formcode & ~(1 << 4); }
        }
        public bool Sbelow180
        {
            get { return (m_Formcode & (1 << 5)) > 0 ? true : false; }
            set { m_Formcode = value ? m_Formcode | (1 << 5) : m_Formcode & ~(1 << 5); }
        }        

    }
}
