using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MotocomdotNetWrapper;
using System.Collections;
using System.Diagnostics;

namespace MotocomDemoCSharp
{
    public partial class Form1 : Form
    {
        CYasnac rc1;
        bool bTest=false;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ArrayList JobList = new ArrayList();
            listBox1.Items.Clear();
            if (rc1.GetJobList(JobList) > 0)
            {
                for (int i = 0; i < JobList.Count; i++)
                {
                    listBox1.Items.Add(JobList[i].ToString());
                }
            }
        }

        void rc1_StatusChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = rc1.IsStep;
            checkBox2.Checked = rc1.Is1Cycle;
            checkBox3.Checked = rc1.IsAuto;
            checkBox4.Checked = rc1.IsOperating;
            checkBox5.Checked = rc1.IsSafeSpeed;
            checkBox6.Checked = rc1.IsTeach;
            checkBox7.Checked = rc1.IsPlay;
            checkBox8.Checked = rc1.IsCommandRemote;

            checkBox9.Checked = rc1.IsPlaybackBoxHold;
            checkBox10.Checked = rc1.IsPPHold;
            checkBox11.Checked = rc1.IsExternalHold;
            checkBox12.Checked = rc1.IsCommandHold;
            checkBox13.Checked = rc1.IsAlarm;
            checkBox14.Checked = rc1.IsError;
            checkBox15.Checked = rc1.IsServoOn;

            listBox2.Items.Clear();

            if (rc1.IsAlarm || rc1.IsError)
            {
                ArrayList AlarmList = new ArrayList();
                CErrorData ErrorData = new CErrorData();

                if (rc1.GetAlarm(ErrorData, AlarmList) > 0)
                {
                    for (int i = 0; i < AlarmList.Count; i++)
                    {
                        listBox2.Items.Add(((CAlarmHistoryItem)AlarmList[i]).Code.ToString() + "-" + ((CAlarmHistoryItem)AlarmList[i]).Subcode.ToString() + ":" + ((CAlarmHistoryItem)AlarmList[i]).Message);
                    }
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedItem != null)
                    rc1.DeleteJob(listBox1.SelectedItem.ToString());
                textBox4.Text = "OK";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                rc1.WritePositionVariable(66, new CRobPosVar(FrameType.Robot,11.1,12.3,13.3,14.4,15.5,16.6,(new CConfiguration(false,false,false,false,false,false)).Formcode,1));
                textBox3.Text = "OK";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (rc1 != null)
            {
                rc1.AutoStatusUpdate = false;               
                rc1 = null;
            }
            try
            {
                rc1 = new CYasnac(textBox1.Text.ToString(), Application.StartupPath);
                rc1.StatusChanged += new EventHandler(rc1_StatusChanged); // Register Eventhandler for status change
                rc1.AutoStatusUpdate = true;         
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedItem != null)
                {
                    if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                        rc1.ReadFile(listBox1.SelectedItem.ToString(), folderBrowserDialog1.SelectedPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    rc1.WriteFile(openFileDialog1.FileName);
                    textBox4.Text = "OK";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

  

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                CSimpleTypeVarList simpleVar = new CSimpleTypeVarList((VarType)listVarType.SelectedIndex, short.Parse(textNumber.Text), short.Parse(textIndex.Text));
                rc1.ReadSimpleTypeVariable(simpleVar);
                textBox3.Clear();
                for (int i = 0; i < short.Parse(textNumber.Text); i++)
                {
                    textBox3.Text = textBox3.Text + listVarType.Items[(int)simpleVar.VarType] + "\t" + (short.Parse(textIndex.Text) + i).ToString() + "\t" + simpleVar.VarListArray[i].ToString() + "\r\n";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
 
        }


        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                double[] values = new double[short.Parse(textNumber.Text)];
                for (int i = 0; i < short.Parse(textNumber.Text); i++)
                {
                    values[i] = double.Parse(textValue.Text);
                }
                CSimpleTypeVarList simpleVar = new CSimpleTypeVarList((VarType)listVarType.SelectedIndex, short.Parse(textNumber.Text), short.Parse(textIndex.Text),values);
                rc1.WriteSimpleTypeVariable(simpleVar);
                textBox3.Text = "OK";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            CRobPosVar posVar = new CRobPosVar();

            try
            {
                rc1.ReadPositionVariable(66, posVar);
                if (posVar.DataType == PosVarType.XYZ)
                    textBox3.Text = "X-Value:\t" + posVar.X.ToString();
                else
                    textBox3.Text = "S-Value:\t" + posVar.SAxis.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                rc1.ResetAlarm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

 

        private void button7_Click(object sender, EventArgs e)
        {
            bTest = !bTest;
            if (!bTest)
                return;

            button7.Text = "Stop Test";
            do
            {
                try
                {
                    Stopwatch watch = new Stopwatch();
                    short d1 = -1, d2 = -1;
                    watch.Start();
                    rc1.UpdateStatus(ref d1, ref d2);
                    rc1.ReadSimpleTypeVariable(new CSimpleTypeVarList(VarType.Double, 1, 71));
                    rc1.ReadSimpleTypeVariable(new CSimpleTypeVarList(VarType.Double, 1, 72));
                    rc1.ReadSimpleTypeVariable(new CSimpleTypeVarList(VarType.Double, 1, 73));
                    rc1.WriteSimpleTypeVariable(new CSimpleTypeVarList(VarType.Double, 1, 70, new double[] { 80}));
                    rc1.WritePositionVariable(60, new CRobPosVar(new double[] { 1.0, 5.0, 11.2, 12.2, 13.3, 14.4, 15.5, 16.6, 0.0, 4.0, 0.0, 0.0 }));
                    rc1.WritePositionVariable(61, new CRobPosVar(new double[] { 1.0, 5.0, 11.2, 12.2, 13.3, 14.4, 15.5, 16.6, 0.0, 4.0, 0.0, 0.0 }));
                    rc1.WritePositionVariable(62, new CRobPosVar(new double[] { 1.0, 5.0, 11.2, 12.2, 13.3, 14.4, 15.5, 16.6, 0.0, 4.0, 0.0, 0.0 }));
                    rc1.WritePositionVariable(63, new CRobPosVar(new double[] { 1.0, 5.0, 11.2, 12.2, 13.3, 14.4, 15.5, 16.6, 0.0, 4.0, 0.0, 0.0 }));
                    rc1.WritePositionVariable(64, new CRobPosVar(new double[] { 1.0, 5.0, 11.2, 12.2, 13.3, 14.4, 15.5, 16.6, 0.0, 4.0, 0.0, 0.0 }));
                    rc1.WritePositionVariable(65, new CRobPosVar(new double[] { 1.0, 5.0, 11.2, 12.2, 13.3, 14.4, 15.5, 16.6, 0.0, 4.0, 0.0, 0.0 }));
                    
                    watch.Stop();
                    //MessageBox.Show("Time: " + watch.ElapsedMilliseconds);
                    Debug.WriteLine(watch.ElapsedMilliseconds.ToString());
                    Application.DoEvents();
                }
                catch
                {
                    Debug.WriteLine("Übertragungsfehler !");
                }
            }
            while (bTest);
            button7.Text = "Start Test";

        }

        private void button12_Click(object sender, EventArgs e)
        {         
            try
            {
                if (listBox1.SelectedItem != null)
                {
                    if (MessageBox.Show("Are you sure to start specified job ?","Confirm action",MessageBoxButtons.YesNo)==DialogResult.Yes)
                        rc1.StartJob(listBox1.SelectedItem.ToString());
                }
                else
                    MessageBox.Show("No job selected !");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                textBox4.Text = "Task: " + textTaskNo.Text + "\t" + "Current Job:" + "\t" + rc1.GetCurrentJob(short.Parse(textTaskNo.Text)) + "\r\n\t" + "Current Line:" + "\t" + rc1.GetCurrentLine(short.Parse(textTaskNo.Text));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {          
            listVarType.Items.AddRange(new string[]{"Byte(B)","Integer(I)","Double(D)","Real(R)"});
            listVarType.SelectedIndex = 0;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedItem != null)
                {
                    rc1.SetCurrentJob(short.Parse(textTaskNo.Text), listBox1.SelectedItem.ToString(), short.Parse(textLineNumber.Text));
                    textBox4.Text = "OK";
                }
                else
                    textBox4.Text = "No job selected !";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
                bool iovalue=rc1.ReadSingleIO(int.Parse(textAddress.Text));
                textBox7.Text ="Address:" + "\t" + textAddress.Text + "\t" + iovalue.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            try
            {
                short[] iovalues=rc1.ReadIOGroups(int.Parse(textAddress.Text),short.Parse(textIONumber.Text));
                for (int i = 0; i < short.Parse(textIONumber.Text); i++)
                {
                    textBox7.Text = textBox7.Text + "Address:" + "\t" + (int.Parse(textAddress.Text) + i*10).ToString() + "\t" + iovalues[i].ToString() + "\r\n";
                }
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message);
            }        
        }

        private void button15_Click(object sender, EventArgs e)
        {
           try
            {
                rc1.WriteSingleIO(int.Parse(textAddress.Text),short.Parse(textIOValue.Text)==0 ? false:true);
                textBox7.Text = "Ok";
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message);
            }        
        }

        private void button18_Click(object sender, EventArgs e)
        {
            try
            {
                short[] ioValues = new short[32];
                for (int i = 0; i < short.Parse(textIONumber.Text); i++)
                {
                    ioValues[i] = short.Parse(textIOValue.Text);
                }
                rc1.WriteIOGroups(int.Parse(textAddress.Text), short.Parse(textIONumber.Text),ioValues);
                textBox7.Text = "OK";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }        
        }

        private void button19_Click(object sender, EventArgs e)
        {
            try
            {
                rc1.SetPlayMode();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }        
        }

        private void button20_Click(object sender, EventArgs e)
        {
            try
            {
                rc1.SetTeachMode();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }  
        }

        private void button21_Click(object sender, EventArgs e)
        {
            try
            {
                rc1.SetServoOn();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }  
        }

        private void button22_Click(object sender, EventArgs e)
        {
            try
            {
                rc1.SetServoOff();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }  
        }

        private void button23_Click(object sender, EventArgs e)
        {
            try
            {
                rc1.SetHoldOn();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }  
        }

        private void button24_Click(object sender, EventArgs e)
        {
            try
            {
                rc1.SetHoldOff();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }  
        }

        private void button25_Click(object sender, EventArgs e)
        {
            try
            {
                rc1.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }  
        }
    }
}