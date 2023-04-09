using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace ksh2vox_v2
{
    public partial class Form1 : Form
    {
        string Laser_convert = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmno";
        string FX_convert = "--STIFUHQADXVB-";
        string[] difficulty_string = { "light", "challenge", "extended", "infinite" };
        string[] difficulty_db = { "novice", "advanced", "exhaust", "infinite" };

        string[] TAB_EFFECT_INFO =
        {
            "1,	90.00,	400.00,	18000.00,	0.70",
            "1,	90.00,	600.00,	15000.00,	5.00",
            "2,	90.00,	40.00,	5000.00,	0.70",
            "2,	90.00,	40.00,	2000.00,	3.00",
            "3,	100.00,	30"
        };
        string[] FXBUTTON_EFFECT =
        {
            "1,	4,	95.00,	2.00,	1.00,	0.85,	0.15",
            "1,	8,	95.00,	2.00,	1.00,	0.75,	0.1",
            "2,	98.00,	8,	1.00",
            "3,	75.00,	2.00,	0.50,	90,	2.00",
            "1,	16,	95.00,	2.00,	1.00,	0.87,	0.13",
            "2,	98.00,	4,	1.00",
            "1,	4,	100.00,	4.00,	0.60,	1.00,	0.85",
            "4,	100.00,	8.00,	0.40",
            "5,	90.00,	1.00,	45,	50,	60",
            "6,	0,	3,	80.00,	500.00,	18000.00,	4.00,	1.40",
            "1,	6,	95.00,	2.00,	1.00,	0.85,	0.15",
            "7,	100.00,	12"
        };

        string[] TAB_PARAM_ASSIGN_INFO =
        {
            "0,	0,	0.00,	0.00",
            "0,	0,	0.00,	0.00",
            "1,	0,	0.00,	0.00",
            "1,	0,	0.00,	0.00",
            "2,	0,	0.00,	0.00",
            "2,	0,	0.00,	0.00",
            "3,	0,	0.00,	0.00",
            "3,	0,	0.00,	0.00",
            "4,	0,	0.00,	0.00",
            "4,	0,	0.00,	0.00",
            "5,	0,	0.00,	0.00",
            "5,	0,	0.00,	0.00",
            "6,	0,	0.00,	0.00",
            "6,	0,	0.00,	0.00",
            "7,	0,	0.00,	0.00",
            "7,	0,	0.00,	0.00",
            "8,	0,	0.00,	0.00",
            "8,	0,	0.00,	0.00",
            "9,	0,	0.00,	0.00",
            "9,	0,	0.00,	0.00",
            "10,	0,	0.00,	0.00",
            "10,	0,	0.00,	0.00",
            "11,	0,	0.00,	0.00",
            "11,	0,	0.00,	0.00"
        };

        string null_effect = "0,	0,	0,	0,	0,	0,	0";
        string title="";
        string artist="";
        int bpm_max=0;
        int bpm_min=0;
        short volume = 100;
        short genre = 32;
        string effect="";
        string illustrator="";
        int difficulty=0;
        short level=1;
        Boolean newsupport160 = false;
        short []background = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 19, 27, 29, 30, 31, 34, 36, 38, 39, 40, 41, 42 };

        List<List<string>> Data_BEAT_INFO = new List<List<string>>();
        List<List<string>> Data_BPM_INFO = new List<List<string>>();
        List<List<string>> Data_TILT_MODE_INFO = new List<List<string>>();
        string Data_END_POSITION = "";
        List<List<string>> Data_TAB_EFFECT_INFO = new List<List<string>>();
        List<List<string>> Data_FXBUTTON_EFFECT_INFO = new List<List<string>>();
        List<List<string>> Data_TAB_PARAM_ASSIGN_INFO = new List<List<string>>();
        List<List<string>> Data_REVERB_EFFECT_PARAM = new List<List<string>>();
        List<List<string>> Data_TRACK1 = new List<List<string>>();
        List<List<string>> Data_TRACK2 = new List<List<string>>();
        List<List<string>> Data_TRACK3 = new List<List<string>>();
        List<List<string>> Data_TRACK4 = new List<List<string>>();
        List<List<string>> Data_TRACK5 = new List<List<string>>();
        List<List<string>> Data_TRACK6 = new List<List<string>>();
        List<List<string>> Data_TRACK7 = new List<List<string>>();
        List<List<string>> Data_TRACK8 = new List<List<string>>();
        List<List<string>> TRACK_AUTO_TAB = new List<List<string>>();
        List<List<string>> Data_SPCONTROLER = new List<List<string>>();
        List<List<string>> Data_SPCONTROLER_T = new List<List<string>>();
        List<List<string>> Data_SPCONTROLER_B = new List<List<string>>();
        List<List<string>> Data_SPCONTROLER_TILT = new List<List<string>>();
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 1;
            metadata_change();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }
        private void openFileDialog1_FileOk(object sender, EventArgs e)
        {
            Data_BEAT_INFO.Clear();
            Data_BPM_INFO.Clear();
            Data_TILT_MODE_INFO.Clear();
            Data_END_POSITION = "";
            Data_TAB_EFFECT_INFO.Clear();
            Data_FXBUTTON_EFFECT_INFO.Clear();
            Data_TAB_PARAM_ASSIGN_INFO.Clear();
            Data_REVERB_EFFECT_PARAM.Clear();
            Data_TRACK1.Clear();
            Data_TRACK2.Clear();
            Data_TRACK3.Clear();
            Data_TRACK4.Clear();
            Data_TRACK5.Clear();
            Data_TRACK6.Clear();
            Data_TRACK7.Clear();
            Data_TRACK8.Clear();
            TRACK_AUTO_TAB.Clear();
            Data_SPCONTROLER.Clear();
            Data_SPCONTROLER_T.Clear();
            Data_SPCONTROLER_B.Clear();
            Data_SPCONTROLER_TILT.Clear();
            Load_Chart(openFileDialog1.FileName);
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                String FilePath = folderBrowserDialog1.SelectedPath + "\\00" + (comboBox2.SelectedIndex + 2).ToString() + "_" + (numericUpDown1.Value).ToString("0000") + 
                    "_" + textBox2.Text + "_" + (difficulty+1).ToString() + difficulty_db[difficulty][0] + ".vox";
                using (StreamWriter File = new StreamWriter(FilePath))
                {
                    String Buffer;
                    int i, j;
                    File.WriteLine("//====================================");
                    File.WriteLine("// SOUND VOLTEX OUTPUT TEXT FILE");
                    File.WriteLine("//====================================\r\n");
                    File.WriteLine("#FORMAT VERSION");
                    File.WriteLine(comboBox2.SelectedIndex == 0 ? "5" : "8");
                    File.WriteLine("#END\r\n");

                    File.WriteLine("#BEAT INFO");
                    for (i = 0; i < Data_BEAT_INFO.Count; i++)
                    {
                        Buffer = "";
                        for (j = 0; j < Data_BEAT_INFO[i].Count; j++)
                        {
                            Buffer += Data_BEAT_INFO[i][j];
                            if (j + 1 < Data_BEAT_INFO[i].Count)
                                Buffer += '\t';
                        }
                        File.WriteLine(Buffer);
                    }
                    File.WriteLine("#END\r\n");

                    File.WriteLine("#BPM INFO");
                    for (i = 0; i < Data_BPM_INFO.Count; i++)
                    {
                        Buffer = "";
                        for (j = 0; j < Data_BPM_INFO[i].Count; j++)
                        {
                            Buffer += Data_BPM_INFO[i][j];
                            if (j + 1 < Data_BPM_INFO[i].Count)
                                Buffer += '\t';
                        }
                        File.WriteLine(Buffer);
                    }
                    File.WriteLine("#END\r\n");

                    File.WriteLine("#TILT MODE INFO");
                    for (i = 0; i < Data_TILT_MODE_INFO.Count; i++)
                    {
                        Buffer = "";
                        for (j = 0; j < Data_TILT_MODE_INFO[i].Count; j++)
                        {
                           Buffer += Data_TILT_MODE_INFO[i][j];
                           if (j + 1 < Data_TILT_MODE_INFO[i].Count)
                                Buffer += '\t';
                        }
                        File.WriteLine(Buffer);
                    }
                    File.WriteLine("#END\r\n");

                    //Is it necessary?
                    if (comboBox2.SelectedIndex == 1) File.WriteLine("#LYRIC INFO\r\n#END\r\n");

                    File.WriteLine("#END POSITION");
                    File.WriteLine(Data_END_POSITION);
                    File.WriteLine("#END\r\n");

                    File.WriteLine("#TAB EFFECT INFO");
                    for (i = 0; i < TAB_EFFECT_INFO.Count(); i++)
                    {
                        File.WriteLine(TAB_EFFECT_INFO[i]);
                    }
                    File.WriteLine("#END\r\n");

                    File.WriteLine("#FXBUTTON EFFECT INFO");
                    for (i =0; i < FXBUTTON_EFFECT.Count();i++)
                    {
                        File.WriteLine(FXBUTTON_EFFECT[i]);
                        if (comboBox2.SelectedIndex == 1) File.WriteLine(null_effect+"\r\n");
                    }
                    File.WriteLine("#END\r\n");

                    if (comboBox2.SelectedIndex == 1)
                    { 
                        File.WriteLine("#TAB PARAM ASSIGN INFO");
                        for (i = 0; i < TAB_PARAM_ASSIGN_INFO.Count(); i++)
                        {
                            File.WriteLine(TAB_PARAM_ASSIGN_INFO[i]);
                       }
                        File.WriteLine("#END\r\n");

                        File.WriteLine("#REVERB EFFECT PARAM");
                        File.WriteLine("#END\r\n");
                    }

                    File.WriteLine("//====================================");
                    File.WriteLine("// TRACK INFO");
                    File.WriteLine("//====================================\r\n");

                    File.WriteLine("#TRACK1");
                    for (i = 0; i < Data_TRACK1.Count; i++)
                    {
                        Buffer = "";
                        for (j = 0; j < Data_TRACK1[i].Count; j++)
                        {
                            Buffer += Data_TRACK1[i][j];
                            if (j + 1 < Data_TRACK1[i].Count)
                                Buffer += '\t';
                        }
                        File.WriteLine(Buffer);
                    }
                    File.WriteLine("#END\r\n");

                    File.WriteLine("//====================================\r\n");

                    File.WriteLine("#TRACK2");
                    for (i = 0; i < Data_TRACK2.Count; i++)
                    {
                        Buffer = "";
                        for (j = 0; j < Data_TRACK2[i].Count; j++)
                        {
                            Buffer += Data_TRACK2[i][j];
                            if (j + 1 < Data_TRACK2[i].Count)
                                Buffer += '\t';
                        }
                        File.WriteLine(Buffer);
                    }
                    File.WriteLine("#END\r\n");

                    File.WriteLine("//====================================\r\n");

                    File.WriteLine("#TRACK3");
                    for (i = 0;i<Data_TRACK3.Count;i++)
                    {
                        Buffer = "";
                        for (j=0;j<Data_TRACK3[i].Count;j++)
                        {
                            Buffer += Data_TRACK3[i][j];
                            if (j + 1 < Data_TRACK3[i].Count)
                                Buffer += '\t';
                        }
                        File.WriteLine(Buffer);
                    }
                    File.WriteLine("#END\r\n");

                    File.WriteLine("//====================================\r\n");

                    File.WriteLine("#TRACK4");
                    for (i = 0; i < Data_TRACK4.Count; i++)
                    {
                        Buffer = "";
                        for (j = 0; j < Data_TRACK4[i].Count; j++)
                        {
                            Buffer += Data_TRACK4[i][j];
                            if (j + 1 < Data_TRACK4[i].Count)
                                Buffer += '\t';
                        }
                        File.WriteLine(Buffer);
                    }
                    File.WriteLine("#END\r\n");

                    File.WriteLine("//====================================\r\n");

                    File.WriteLine("#TRACK5");
                    for (i = 0; i < Data_TRACK5.Count; i++)
                    {
                        Buffer = "";
                        for (j = 0; j < Data_TRACK5[i].Count; j++)
                        {
                            Buffer += Data_TRACK5[i][j];
                            if (j + 1 < Data_TRACK5[i].Count)
                                Buffer += '\t';
                        }
                        File.WriteLine(Buffer);
                    }
                    File.WriteLine("#END\r\n");

                    File.WriteLine("//====================================\r\n");

                    File.WriteLine("#TRACK6");
                    for (i = 0; i < Data_TRACK6.Count; i++)
                    {
                        Buffer = "";
                        for (j = 0; j < Data_TRACK6[i].Count; j++)
                        {
                            Buffer += Data_TRACK6[i][j];
                            if (j + 1 < Data_TRACK6[i].Count)
                                Buffer += '\t';
                        }
                        File.WriteLine(Buffer);
                    }
                    File.WriteLine("#END\r\n");

                    File.WriteLine("//====================================\r\n");

                    File.WriteLine("#TRACK7");
                    for (i = 0; i < Data_TRACK7.Count; i++)
                    {
                        Buffer = "";
                        for (j = 0; j < Data_TRACK7[i].Count; j++)
                        {
                            Buffer += Data_TRACK7[i][j];
                            if (j + 1 < Data_TRACK7[i].Count)
                                Buffer += '\t';
                        }
                        File.WriteLine(Buffer);
                    }
                    File.WriteLine("#END\r\n");

                    File.WriteLine("//====================================\r\n");

                    File.WriteLine("#TRACK8");
                    for (i = 0; i < Data_TRACK8.Count; i++)
                    {
                        Buffer = "";
                        for (j = 0; j < Data_TRACK8[i].Count; j++)
                        {
                            Buffer += Data_TRACK8[i][j];
                            if (j + 1 < Data_TRACK8[i].Count)
                                Buffer += '\t';
                        }
                        File.WriteLine(Buffer);
                    }
                    File.WriteLine("#END");

                    if (comboBox2.SelectedIndex == 1)
                    {
                        File.WriteLine("#TRACK AUTO TAB");
                        File.WriteLine("#END\r\n");

                        File.WriteLine("//====================================\r\n\r\n");

                        File.WriteLine("//====================================");
                        File.WriteLine("// SPCONTROLER INFO");
                        File.WriteLine("//====================================\r\n");
                        File.WriteLine("#SPCONTROLER");
                        for (i = 0; i < Data_SPCONTROLER.Count; i++)
                        {
                            Buffer = "";
                            for (j = 0; j < Data_SPCONTROLER[i].Count; j++)
                            {
                                Buffer += Data_SPCONTROLER[i][j];
                                if (j + 1 < Data_SPCONTROLER[i].Count)
                                    Buffer += '\t';
                            }
                            File.WriteLine(Buffer);
                        }
                        for (i = 0; i < Data_SPCONTROLER_T.Count; i++)
                        {
                            Buffer = "";
                            for (j = 0; j < Data_SPCONTROLER_T[i].Count; j++)
                            {
                                Buffer += Data_SPCONTROLER_T[i][j];
                                if (j + 1 < Data_SPCONTROLER_T[i].Count)
                                    Buffer += '\t';
                            }
                            File.WriteLine(Buffer);
                        }
                        for (i = 0; i < Data_SPCONTROLER_B.Count; i++)
                        {
                            Buffer = "";
                            for (j = 0; j < Data_SPCONTROLER_B[i].Count; j++)
                            {
                                Buffer += Data_SPCONTROLER_B[i][j];
                                if (j + 1 < Data_SPCONTROLER_B[i].Count)
                                    Buffer += '\t';
                            }
                            File.WriteLine(Buffer);
                        }
                        File.WriteLine("#END\r\n");
                    }
                    File.WriteLine("//====================================");
                }
            }
        }
        public Boolean Load_Chart(String FilePath)
        {
            
            string buffer, handle;
            string[] handle_array;
            List<string> Data_buffer = new List<string>();
            int[] position = { 0, 1, 0 };
            short data_size = 0;
            short []beat = {4,4};
            short tilt_mode = 0;
            int[] last_laser_inverval = { 0, 0 };
            short laser_type = 0;
            string[] laser2x = { "1", "1" };
            int[] last_laser = { -1, -1 };
            int[] last_longbt = { 0, 0 ,0 ,0};
            int[] last_longfx = { 0,0 };
            int[] last_fxtype = { 0, 0 };
            int[] zoom_freeze = { 0, 0 ,0};
            short[] slam_rotate = { 0, 0 };
            Boolean[] zoomed = {false,false };
            // Read the file and display it line by line.
            System.IO.StreamReader file = new System.IO.StreamReader(FilePath);
            if (comboBox2.SelectedIndex == 1)
            {
                Data_SPCONTROLER.Add(new List<string> { "001,01,00", "Realize" ,"3" ,"0", "36.12", "60.12", "110.12", "0.00" });
                Data_SPCONTROLER.Add(new List<string> { "001,01,00", "Realize", "4", "0", "0.62","0.72","1.03","0.00" });
                Data_SPCONTROLER.Add(new List<string> { "001,01,00", "AIRL_ScaX", "1", "0", "0.00", "1.00", "0.00", "0.00" });
                Data_SPCONTROLER.Add(new List<string> { "001,01,00", "AIRR_ScaX", "1", "0", "0.00", "2.00", "0.00", "0.00" });
            }
            Data_TILT_MODE_INFO.Add(new List<string> { "001,01,00", "0" });
            while ((buffer = file.ReadLine()) != null)
            {
                if (buffer == "--")
                {
                    for (int i = 0; i < Data_buffer.Count;i++)
                    {
                        handle = Data_buffer[i];
                        if (handle.Contains("beat="))
                        {
                            List<string> Save_DataBuffer = new List<string>();
                            handle_array = handle.Split('=');
                            handle_array = handle_array[1].Split('/');
                            beat[0] = short.Parse(handle_array[0]);
                            beat[1] = short.Parse(handle_array[1]);
                            Save_DataBuffer.Add(position_string(position));
                            Save_DataBuffer.Add(beat[0].ToString());
                            Save_DataBuffer.Add(beat[1].ToString());
                            Data_BEAT_INFO.Add(Save_DataBuffer);  
                        }
                        else if (handle.Contains("tilt="))
                        {
                            List<string> Save_DataBuffer = new List<string>();
                            Save_DataBuffer.Add(position_string(position));
                            handle_array = handle.Split('=');/*
                            if (handle_array[1].Equals("zero"))
                            {
                                if (tilt_mode != 3)
                                {
                                    Save_DataBuffer.Add("0");
                                    Data_TILT_MODE_INFO.Add(Save_DataBuffer);
                                    Save_DataBuffer.Add("0");
                                    Save_DataBuffer.Add("0");
                                }
                                tilt_mode = 3;
                            }*/
                            if (handle_array[1].Equals("normal"))
                            {
                                Save_DataBuffer.Add("0");
                                tilt_mode = 0;
                            }
                            else if (handle_array[1].Equals("bigger") || handle_array[1].Equals("biggest"))
                            {
                                Save_DataBuffer.Add("1");
                                tilt_mode = 1;
                            }
                            else if (handle_array[1].Contains("keep_"))
                            {
                                Save_DataBuffer.Add("2");
                                tilt_mode = 2;
                            }
                            else
                            {
                                Save_DataBuffer.Add("0");
                                tilt_mode = 0;
                            }
                            Data_TILT_MODE_INFO.Add(Save_DataBuffer);
                        }
                        else if (handle.Contains("t="))
                        {
                            List<string> Save_DataBuffer = new List<string>();
                            handle_array = handle.Split('=');

                            Save_DataBuffer.Add(position_string(position));
                            Save_DataBuffer.Add(String.Format("{0:N4}", double.Parse(handle_array[1])));
                            Save_DataBuffer.Add("4");
                            Data_BPM_INFO.Add(Save_DataBuffer);
                        }
                        else if (handle.Contains("filtertype="))
                        {
                            List<string> Save_DataBuffer = new List<string>();
                            handle_array = handle.Split('=');
                            //0 = Peak / 1 = ? / 2 = LPF / 3 = ? / 4 = HPF / 5 = Bitcrusher
                            if (handle_array[1].Contains("peak"))
                                laser_type = 0;
                            else if (handle_array[1].Contains("lpf1"))
                                laser_type = 2;
                            else if (handle_array[1].Contains("hpf1"))
                                laser_type = 4;
                            else if (handle_array[1].Contains("bitc"))
                                laser_type = 5;
                            else if (comboBox2.SelectedIndex == 1) laser_type = 6;
                        }
                        else if (handle.Contains("zoom_top="))
                        {
                            List<string> Save_DataBuffer = new List<string>();
                            List<string> Save_DataBuffer2 = new List<string>();
                            handle_array = handle.Split('=');

                            Save_DataBuffer.Add(position_string(position));
                            Save_DataBuffer.Add("CAM_RotX");
                            Save_DataBuffer.Add("2");
                            Save_DataBuffer.Add("0");
                            Save_DataBuffer.Add(String.Format("{0:N2}", double.Parse(handle_array[1]) / 150.0));
                            Save_DataBuffer.Add(String.Format("{0:N2}", double.Parse(handle_array[1]) / 150.0));

                            Save_DataBuffer.Add("0.0");
                            Save_DataBuffer.Add("0.0");
                            if (zoomed[0] == false)
                            {
                                if (!(position[0] == 1 && position[1] == 1 && position[2] == 0))
                                {
                                    Save_DataBuffer2.Add("001,01,00");
                                    Save_DataBuffer2.Add("CAM_RotX");
                                    Save_DataBuffer2.Add("2");
                                    Save_DataBuffer2.Add(zoom_freeze[0].ToString());
                                    Save_DataBuffer2.Add(String.Format("{0:N2}", double.Parse(handle_array[1]) / 150.0));
                                    Save_DataBuffer2.Add(String.Format("{0:N2}", double.Parse(handle_array[1]) / 150.0));

                                    Data_SPCONTROLER_T.Add(Save_DataBuffer2);
                                }
                            }
                            else
                            {
                                Data_SPCONTROLER_T[Data_SPCONTROLER_T.Count - 1][3] = zoom_freeze[0].ToString();
                                Data_SPCONTROLER_T[Data_SPCONTROLER_T.Count - 1][5] = Save_DataBuffer[4];
                            }
                            Data_SPCONTROLER_T.Add(Save_DataBuffer);
                            zoomed[0] = true;
                            zoom_freeze[0] = 0;
                        }
                        else if (handle.Contains("zoom_bottom="))//Radi == Bottom * -1
                        {
                            List<string> Save_DataBuffer = new List<string>();
                            List<string> Save_DataBuffer2 = new List<string>();
                            handle_array = handle.Split('=');

                            Save_DataBuffer.Add(position_string(position));
                            Save_DataBuffer.Add("CAM_Radi");
                            Save_DataBuffer.Add("2");
                            Save_DataBuffer.Add("0");
                            Save_DataBuffer.Add(String.Format("{0:N2}", double.Parse(handle_array[1]) / -150.0));
                            Save_DataBuffer.Add(String.Format("{0:N2}", double.Parse(handle_array[1]) / -150.0));

                            Save_DataBuffer.Add("0.0");
                            Save_DataBuffer.Add("0.0");
                            if (zoomed[1] == false)
                            {
                                if (!(position[0] == 1 && position[1] == 1 && position[2] == 0))
                                {
                                    Save_DataBuffer2.Add("001,01,00");
                                    Save_DataBuffer2.Add("CAM_Radi");
                                    Save_DataBuffer2.Add("2");
                                    Save_DataBuffer2.Add(zoom_freeze[0].ToString());
                                    Save_DataBuffer2.Add(String.Format("{0:N2}", double.Parse(handle_array[1]) / 150.0));
                                    Save_DataBuffer2.Add(String.Format("{0:N2}", double.Parse(handle_array[1]) / 150.0));

                                    Data_SPCONTROLER_T.Add(Save_DataBuffer2);
                                }
                            }
                            else
                            {
                                Data_SPCONTROLER_B[Data_SPCONTROLER_B.Count - 1][3] = zoom_freeze[1].ToString();
                                Data_SPCONTROLER_B[Data_SPCONTROLER_B.Count - 1][5] = Save_DataBuffer[4];
                            }
                            Data_SPCONTROLER_B.Add(Save_DataBuffer);
                            zoomed[1] = true;
                            zoom_freeze[1] = 0;
                        }
                        else if (handle.Equals("laserrange_l=2x"))
                        {
                            laser2x[0] = "2";
                        }
                        else if (handle.Equals("laserrange_r=2x"))
                        {
                            laser2x[1] = "2";
                        }
                        //Ver 160~ Support
                        else if (handle.Contains("fx-l="))
                        {
                            handle_array = handle.Split('=');
                            last_fxtype[0] = 255;
                            if (handle_array[1].Equals("Retrigger;8"))
                                last_fxtype[0] = 2;
                            if (handle_array[1].Equals("Retrigger;16"))
                                last_fxtype[0] = 3;
                            if (handle_array[1].Equals("Gate;16"))
                                last_fxtype[0] = 4;
                            if (handle_array[1].Equals("Flanger"))
                                last_fxtype[0] = 5;
                            if (handle_array[1].Equals("Retrigger;32"))
                                last_fxtype[0] = 6;
                            if (handle_array[1].Equals("Gate;8"))
                                last_fxtype[0] = 7;
                            if (handle_array[1].Contains("Echo;4"))
                                last_fxtype[0] = 8;
                            if (handle_array[1].Contains("TapeStop"))
                                last_fxtype[0] = 9;
                            if (handle_array[1].Contains("SideChain"))
                                last_fxtype[0] = 10;
                            if (handle_array[1].Contains("Wobble"))
                                last_fxtype[0] = 11;
                            if (handle_array[1].Equals("Retrigger;12"))
                                last_fxtype[0] = 12;
                            if (handle_array[1].Contains("BitCrusher"))
                                last_fxtype[0] = 13;
                        }
                        else if (handle.Contains("fx-r="))
                        {
                            handle_array = handle.Split('=');
                            last_fxtype[1] = 255;
                            if (handle_array[1].Equals("Retrigger;8"))
                                last_fxtype[1] = 2;
                            if (handle_array[1].Equals("Retrigger;16"))
                                last_fxtype[1] = 3;
                            if (handle_array[1].Equals("Gate;16"))
                                last_fxtype[1] = 4;
                            if (handle_array[1].Equals("Flanger"))
                                last_fxtype[1] = 5;
                            if (handle_array[1].Equals("Retrigger;32"))
                                last_fxtype[1] = 6;
                            if (handle_array[1].Equals("Gate;8"))
                                last_fxtype[1] = 7;
                            if (handle_array[1].Contains("Echo;4"))
                                last_fxtype[1] = 8;
                            if (handle_array[1].Contains("TapeStop"))
                                last_fxtype[1] = 9;
                            if (handle_array[1].Contains("SideChain"))
                                last_fxtype[1] = 10;
                            if (handle_array[1].Contains("Wobble"))
                                last_fxtype[1] = 11;
                            if (handle_array[1].Equals("Retrigger;12"))
                                last_fxtype[1] = 12;
                            if (handle_array[1].Contains("BitCrusher"))
                                last_fxtype[1] = 13;
                        }
                        else if (handle[4] == '|' && handle[7] == '|')   //is a vaild data?
                        {
                            switch (handle[0])  //BT-A
                            {
                                case '0':
                                {
                                    if (last_longbt[0] > 0)
                                    {
                                        Data_TRACK3[Data_TRACK3.Count-1].Add(last_longbt[0].ToString());
                                        Data_TRACK3[Data_TRACK3.Count-1].Add("3");
                                        last_longbt[0] = 0;
                                    }
                                    break;
                                }
                                case '1':
                                {
                                    List<string> Save_DataBuffer = new List<string>();
                                    Save_DataBuffer.Add(position_string(position));
                                    Save_DataBuffer.Add("0");
                                    Save_DataBuffer.Add("255");
                                    Data_TRACK3.Add(Save_DataBuffer);
                                    
                                    break;
                                }
                                case '2':
                                {
                                    List<string> Save_DataBuffer = new List<string>();
                                    if (last_longbt[0] == 0)
                                    {
                                        Save_DataBuffer.Add(position_string(position));
                                        Data_TRACK3.Add(Save_DataBuffer);
                                    }
                                    last_longbt[0] += (48*beat[0]) / data_size;
                                    break;
                                }
                            }
                            if ((handle.Count() > 10) && (handle[10] == '@'))
                            {
                                switch (handle[11])
                                {
                                    case '>':
                                    {
                                            handle_array = handle.Split('>');
                                            slam_rotate[0] = 1;
                                            slam_rotate[1] = 5;
                                            break;
                                        }
                                    case '<':
                                        {
                                            handle_array = handle.Split('<');
                                            slam_rotate[0] = 2;
                                            slam_rotate[1] = 5;
                                            break;
                                        }
                                    case ')':
                                        {
                                            handle_array = handle.Split(')');
                                            slam_rotate[0] = 1;
                                            if (handle_array[1] == "48")
                                                slam_rotate[1] = 2;
                                            else if (handle_array[1] == "96")
                                                slam_rotate[1] = 3;
                                            else slam_rotate[1] = 1;

                                            break;
                                        }
                                    case '(':
                                        {
                                            handle_array = handle.Split('(');
                                            slam_rotate[0] = 2;

                                            if (handle_array[1] == "48")
                                                slam_rotate[1] = 2;
                                            else if (handle_array[1] == "96")
                                                slam_rotate[1] = 3;
                                            else slam_rotate[1] = 1;
                                            break;
                                        }
                                }

                            }
                            switch (handle[1])  //BT-B
                            {
                                case '0':
                                {
                                    if (last_longbt[1] > 0)
                                    {
                                        Data_TRACK4[Data_TRACK4.Count - 1].Add(last_longbt[1].ToString());
                                        Data_TRACK4[Data_TRACK4.Count - 1].Add("3");
                                        last_longbt[1] = 0;
                                    }
                                    break;
                                }
                                case '1':
                                {
                                    List<string> Save_DataBuffer = new List<string>();
                                    Save_DataBuffer.Add(position_string(position));
                                    Save_DataBuffer.Add("0");
                                    Save_DataBuffer.Add("255");
                                    Data_TRACK4.Add(Save_DataBuffer);
                                    
                                    break;
                                }
                                case '2':
                                {
                                    List<string> Save_DataBuffer = new List<string>();
                                    if (last_longbt[1] == 0)
                                    {
                                        Save_DataBuffer.Add(position_string(position));
                                        Data_TRACK4.Add(Save_DataBuffer);
                                    }
                                    last_longbt[1] += (48*beat[0]) / data_size;
                                    break;
                                }
                            }
                            switch (handle[2])  //BT-C
                            {
                                case '0':
                                {
                                    if (last_longbt[2] > 0)
                                    {
                                        Data_TRACK5[Data_TRACK5.Count - 1].Add(last_longbt[2].ToString());
                                        Data_TRACK5[Data_TRACK5.Count - 1].Add("3");
                                        last_longbt[2] = 0;
                                    }
                                    break;
                                }
                                case '1':
                                {
                                    List<string> Save_DataBuffer = new List<string>();
                                    Save_DataBuffer.Add(position_string(position));
                                    Save_DataBuffer.Add("0");
                                    Save_DataBuffer.Add("255");
                                    Data_TRACK5.Add(Save_DataBuffer);
                                    
                                    break;
                                }
                                case '2':
                                {
                                    List<string> Save_DataBuffer = new List<string>();
                                    if (last_longbt[2] == 0)
                                    {
                                        Save_DataBuffer.Add(position_string(position));
                                        Data_TRACK5.Add(Save_DataBuffer);
                                    }
                                    last_longbt[2] += (48*beat[0]) / data_size;
                                    break;
                                }
                            }
                            switch (handle[3])  //BT-D
                            {
                                case '0':
                                {
                                    if (last_longbt[3] > 0)
                                    {
                                        Data_TRACK6[Data_TRACK6.Count - 1].Add(last_longbt[3].ToString());
                                        Data_TRACK6[Data_TRACK6.Count - 1].Add("3");
                                        last_longbt[3] = 0;
                                    }
                                    break;
                                }
                                case '1':
                                {
                                    List<string> Save_DataBuffer = new List<string>();
                                    Save_DataBuffer.Add(position_string(position));
                                    Save_DataBuffer.Add("0");
                                    Save_DataBuffer.Add("255");
                                    Data_TRACK6.Add(Save_DataBuffer);
                                    break;
                                }
                                case '2':
                                {
                                    List<string> Save_DataBuffer = new List<string>();
                                    if (last_longbt[3] == 0)
                                    {
                                        Save_DataBuffer.Add(position_string(position));
                                        Data_TRACK6.Add(Save_DataBuffer);
                                    }
                                    last_longbt[3] += (48 * beat[0]) / data_size;
                                    break;
                                }
                            }

                            switch (handle[5])  //FX-L
                            {
                                case '0':
                                {
                                    List<string> Save_DataBuffer = new List<string>();
                                    if (last_longfx[0] > 0)
                                    {
                                        Data_TRACK2[Data_TRACK2.Count - 1].Add(last_longfx[0].ToString());
                                        Data_TRACK2[Data_TRACK2.Count - 1].Add(last_fxtype[0].ToString());
                                        last_longfx[0] = 0;
                                    }
                                    break;
                                }
                                case '1':
                                    {
                                        List<string> Save_DataBuffer = new List<string>();
                                        if (last_longfx[0] == 0)
                                        {
                                            Save_DataBuffer.Add(position_string(position));
                                            Data_TRACK2.Add(Save_DataBuffer);
                                        }
                                        last_longfx[0] += (48 * beat[0]) / data_size;
                                        break;
                                    }
                                case '2':
                                {
                                    List<string> Save_DataBuffer = new List<string>();
                                    Save_DataBuffer.Add(position_string(position));
                                    Save_DataBuffer.Add("0");
                                    Save_DataBuffer.Add("255");
                                    Data_TRACK2.Add(Save_DataBuffer);
                                    break;
                                }
                                default:
                                {
                                    List<string> Save_DataBuffer = new List<string>();
                                    int j = (checkBox2.Checked == false) ? FX_convert.IndexOf(handle[5]) : -1;
                                    if (last_longfx[0] == 0)
                                    {
                                        Save_DataBuffer.Add(position_string(position));
                                        Data_TRACK2.Add(Save_DataBuffer);
                                    }
                                    last_longfx[0] += (48 * beat[0]) / data_size;
                                    last_fxtype[0] = (j != -1) ? j: 254;
                                    break;
                                }
                            }
                            switch (handle[6])  //FX-r
                            {
                                case '0':
                                {
                                    List<string> Save_DataBuffer = new List<string>();
                                    if (last_longfx[1] > 0)
                                    {
                                        Data_TRACK7[Data_TRACK7.Count - 1].Add(last_longfx[1].ToString());
                                        Data_TRACK7[Data_TRACK7.Count - 1].Add(last_fxtype[1].ToString());
                                        last_longfx[1] = 0;
                                    }
                                    break;
                                 }
                                 case '1':
                                 {
                                        List<string> Save_DataBuffer = new List<string>();
                                        if (last_longfx[1] == 0)
                                        {
                                            Save_DataBuffer.Add(position_string(position));
                                            Data_TRACK7.Add(Save_DataBuffer);
                                        }
                                        last_longfx[1] += (48 * beat[0]) / data_size;
                                        break;
                                    }
                                 case '2':
                                 {
                                    List<string> Save_DataBuffer = new List<string>();
                                    Save_DataBuffer.Add(position_string(position));
                                    Save_DataBuffer.Add("0");
                                    Save_DataBuffer.Add("255");
                                    Data_TRACK7.Add(Save_DataBuffer);
                                    break;
                                 }
                                 default:
                                 {
                                    List<string> Save_DataBuffer = new List<string>();
                                    int j = (checkBox2.Checked == false) ? FX_convert.IndexOf(handle[6]) : -1;
                                    if (last_longfx[1] == 0)
                                    {
                                        Save_DataBuffer.Add(position_string(position));
                                        Data_TRACK7.Add(Save_DataBuffer);
                                    }
                                    last_longfx[1] += (48 * beat[0]) / data_size;
                                    last_fxtype[1] = (j != -1) ? j : 254;
                                    break;
                                 }
                            }
                            if (handle[8] == '-')
                            {
                                if (last_laser[0] != -1) Data_TRACK1[Data_TRACK1.Count - 1][2] = "2";
                                last_laser[0] = -1;
                                laser2x[0] = "1";
                                last_laser_inverval[0] = 0;
                            }
                            else
                            {
                                if (handle[8] != ':')
                                { 
                                    List<string> Save_DataBuffer = new List<string>();

                                    if (last_laser[0] != -1 && last_laser_inverval[0] + ((48 * beat[0]) / data_size) == 6)
                                    {
                                        Save_DataBuffer.Add(Data_TRACK1[Data_TRACK1.Count - 1][0]);
                                        last_laser_inverval[0] = -999;
                                    }
                                    else
                                    {
                                        Save_DataBuffer.Add(position_string(position));
                                        last_laser_inverval[0] = 0;
                                    }
                                    Save_DataBuffer.Add((Laser_convert.IndexOf(handle[8]) * 127 / 50).ToString());
                                    Save_DataBuffer.Add((last_laser[0] == -1) ? "1" : "0");
                                    if (last_laser_inverval[0] == -999)
                                    {
                                        int k = Laser_convert.IndexOf(handle[8]) * 127 / 50;
                                        if ((slam_rotate[0] == 1 && k > last_laser[0]) || (slam_rotate[0] == 2 && k < last_laser[0]))
                                        {
                                            Data_TRACK1[Data_TRACK1.Count - 1][3] = slam_rotate[1].ToString();
                                            slam_rotate[0] = 0;
                                        }
                                    }
                                    Save_DataBuffer.Add("0");    //Rotate

                                    Save_DataBuffer.Add(laser_type.ToString());
                                    if (comboBox2.SelectedIndex == 1) Save_DataBuffer.Add(laser2x[0]);
                                    last_laser[0] = Laser_convert.IndexOf(handle[8]) * 127 / 50;
                                    Data_TRACK1.Add(Save_DataBuffer);
                                }
                                else last_laser_inverval[0] += (48 * beat[0]) / data_size;
                            }
                            if (handle[9] == '-')
                            {
                                if (last_laser[1] != -1) Data_TRACK8[Data_TRACK8.Count - 1][2] = "2";
                                last_laser[1] = -1;
                                laser2x[1] = "1";
                                last_laser_inverval[1] = 0;
                                
                            }
                            else
                            {
                                
                                if (handle[9] != ':')
                                {
                                    List<string> Save_DataBuffer = new List<string>();

                                    if (last_laser[1] != -1 && last_laser_inverval[1] + ((48 * beat[0]) / data_size) == 6)
                                    {
                                        Save_DataBuffer.Add(Data_TRACK8[Data_TRACK8.Count - 1][0]);
                                        last_laser_inverval[1] = -999;
                                    }
                                    else
                                    {
                                        Save_DataBuffer.Add(position_string(position));
                                        last_laser_inverval[1] = 0;
                                    }
                                    Save_DataBuffer.Add((Laser_convert.IndexOf(handle[9]) * 127 / 50).ToString());
                                    Save_DataBuffer.Add((last_laser[1] == -1) ? "1" : "0");

                                    if (last_laser_inverval[1] == -999)
                                    {
                                        int k = Laser_convert.IndexOf(handle[9]) * 127 / 50;
                                        if ((slam_rotate[0] == 1 && k > last_laser[1]) || (slam_rotate[0] == 2 && k < last_laser[1]))
                                        {
                                            Data_TRACK8[Data_TRACK8.Count - 1][3] = slam_rotate[1].ToString();
                                            slam_rotate[0] = 0;
                                        }
                                    }
                                    Save_DataBuffer.Add("0");    //Rotate

                                    Save_DataBuffer.Add(laser_type.ToString());
                                    if (comboBox2.SelectedIndex == 1) Save_DataBuffer.Add(laser2x[1]);

                                    last_laser[1] = Laser_convert.IndexOf(handle[9]) * 127 / 50;
                                    Data_TRACK8.Add(Save_DataBuffer);
                                }
                                else last_laser_inverval[1] += (48 * beat[0]) / data_size;
                            }

                            zoom_freeze[0] += (48 * beat[0]) / data_size;
                            zoom_freeze[1] += (48 * beat[0]) / data_size;
                            if (tilt_mode == 3) zoom_freeze[2] += (48 * beat[0]) / data_size;
                            position[2] += (48 * beat[0]) / data_size;
                            while (position[2] >= (192 / beat[1]))
                            {
                                position[1] += 1;
                                position[2] -= (192 / beat[1]);
                            }
                        }
                    }

                    position[0] += 1;
                    position[1] = 1;
                    position[2] = 0;

                    data_size = 0;
                    Data_buffer.Clear();
                }
                else if (position[0] == 0)
                {
                    string[] parameter = buffer.Split('=');
                    if (parameter[0] == "title") title = parameter[1];
                    else if (parameter[0] == "artist") artist = parameter[1];
                    else if (parameter[0] == "effect") effect = parameter[1];
                    else if (parameter[0] == "illustrator") illustrator = parameter[1];
                    else if (parameter[0] == "ver")
                    {
                        if (Convert.ToInt32 (parameter[1][1]) >= 6 || Convert.ToInt32(parameter[1][0]) >= 2)
                        {
                            newsupport160 = true;
                        }
                    }
                    else if (parameter[0] == "difficulty")
                        difficulty = Array.IndexOf(difficulty_string, parameter[1]);
                    else if (parameter[0] == "level")
                    {
                        level = short.Parse(parameter[1]);
                        if (level > 16)
                        {
                            MessageBox.Show("Difficulty can only ranged from 1~16.", "Info", MessageBoxButtons.OK,MessageBoxIcon.Information);
                            level = 16;
                        }
                    }
                    else if (parameter[0] == "t")
                    {
                        if (parameter[1].Contains("-"))
                        {
                            string[] t = parameter[1].Split('-');
                            bpm_min = Convert.ToInt32(double.Parse(t[0]) * 100);
                            bpm_max = Convert.ToInt32(double.Parse(t[1]) * 100);
                        }
                        else
                        {
                            bpm_min = bpm_max = Convert.ToInt32(double.Parse(parameter[1]) * 100);
                        }
                    }
                }
                else
                {
                    Data_buffer.Add(buffer);
                    if (buffer.Count() > 7 && buffer[4] == '|' && buffer[7] == '|')   //is a vaild data?
                        data_size++;
                }
            }
            if (Data_BPM_INFO.Count == 0)
            {
                List<string> other_shit = new List<string>();
                other_shit.Add("001,01,00");
                other_shit.Add(String.Format("{0:N4}", double.Parse((bpm_max/100.0).ToString())));
                other_shit.Add("4");
                Data_BPM_INFO.Add(other_shit);
            }
            if (Data_TILT_MODE_INFO.Count == 0)
            {
                List<string> other_shit = new List<string>();
                other_shit.Add("001,01,00");
                other_shit.Add("0");
                Data_TILT_MODE_INFO.Add(other_shit);
            }
            position[0] += 1;
            if (Data_SPCONTROLER_T.Count == 0)
            {
                List<string> other_shit = new List<string>();
                other_shit.Add("001,01,00");
                other_shit.Add("CAM_RotX");
                other_shit.Add("2");
                other_shit.Add(((192/beat[1])*beat[0]*position[0]).ToString());
                other_shit.Add("0.00");
                other_shit.Add("0.00");
                other_shit.Add("0.00");
                other_shit.Add("0.00");
                Data_SPCONTROLER_T.Add(other_shit);
            }
            else
            {
                Data_SPCONTROLER_T[Data_SPCONTROLER_T.Count - 1][3] = (zoom_freeze[0] + (192 / beat[1]) * beat[0]).ToString();
            }
            if (Data_SPCONTROLER_B.Count == 0)
            {
                List<string> other_shit = new List<string>();
                other_shit.Add("001,01,00");
                other_shit.Add("CAM_Radi");
                other_shit.Add("2");
                other_shit.Add(((192 / beat[1]) * beat[0] * position[0]).ToString());
                other_shit.Add("0.00");
                other_shit.Add("0.00");
                other_shit.Add("0.00");
                other_shit.Add("0.00");
                Data_SPCONTROLER_B.Add(other_shit);
            }
            else
            {
                Data_SPCONTROLER_B[Data_SPCONTROLER_B.Count - 1][3] = (zoom_freeze[1] + (192 / beat[1]) * beat[0]).ToString();
            }
            Data_END_POSITION = position_string(position);
            file.Close();
            checkBox1.Checked = false;
            metadata_change();

            return true;
        }

        public void metadata_change()
        {
            String temp = "";
            if (checkBox1.Checked == false)
            {
                //HEADER

                temp = Header(1) + "<music id=\"" + (numericUpDown1.Value).ToString() + "\">" + "\r\n";
                temp += Header(2) + "<info>" + "\r\n";
                temp += Header(3) + "<label>" + (numericUpDown1.Value).ToString() + "</label>" + "\r\n";
                temp += Header(3) + "<title_name>" + title + "</title_name>" + "\r\n";
                temp += Header(3) + "<title_yomigana>" + "convert" + "</title_yomigana>" + "\r\n";
                temp += Header(3) + "<artist_name>" + artist + "</artist_name>" + "\r\n";
                temp += Header(3) + "<artist_yomigana>" + "convert" + "</artist_yomigana>" + "\r\n";
                temp += Header(3) + "<ascii>" + textBox2.Text + "</ascii>" + "\r\n";
                if (comboBox2.SelectedIndex == 1)
                {
                    temp += Header(3) + "<bpm_max __type=\"u32\">" + bpm_max.ToString() + "</bpm_max>" + "\r\n";
                    temp += Header(3) + "<bpm_min __type=\"u32\">" + bpm_min.ToString() + "</bpm_min>" + "\r\n";
                }
                else
                {
                    temp += Header(3) + "<bpm_max __type=\"u16\">" + bpm_max.ToString() + "</bpm_max>" + "\r\n";
                    temp += Header(3) + "<bpm_min __type=\"u16\">" + bpm_min.ToString() + "</bpm_min>" + "\r\n";
                }
                temp += Header(3) + "<distribution_date __type=\"u32\">" + String.Format("{0:yyyyMMdd}", DateTime.Now) + "</distribution_date>" + "\r\n";
                temp += Header(3) + "<volume __type=\"u16\">" + volume.ToString() + "</volume>" + "\r\n";
                temp += Header(3) + "<bg_no __type=\"u16\">" + background[comboBox1.SelectedIndex] + "</bg_no>" + "\r\n";
                temp += Header(3) + "<genre __type=\"u8\">" + genre.ToString() + "</genre>" + "\r\n";
                temp += Header(3) + "<is_fixed __type=\"u8\">1</is_fixed>" + "\r\n";
                temp += Header(3) + "<version __type=\"u8\">" + (comboBox2.SelectedIndex+2).ToString() + "</version>" + "\r\n";
                temp += Header(3) + "<demo_pri __type=\"s8\">0</demo_pri>" + "\r\n";
                if (comboBox2.SelectedIndex == 1) temp += Header(3) + "<inf_ver __type=\"u8\">" + (comboBox2.SelectedIndex + 2).ToString() + "</inf_ver>" + "\r\n";
                temp += Header(2) + "</info>" + "\r\n";
                //HEADER
            }
            //DIFFICULTY
            if (checkBox1.Checked == false) temp += Header(2) + "<difficulty>" + "\r\n";
            for (int j = ((checkBox1.Checked == true) ? difficulty : 0); j < 4; j++)
            {
                temp += Header(3) + "<" + difficulty_db[j] + ">" + "\r\n";
                temp += Header(4) + "<difnum __type=\"u8\">" + ((difficulty == j) ? level.ToString() : "0") + "</difnum>" + "\r\n";
                temp += Header(4) + "<illustrator>" + ((difficulty == j) ? illustrator : "dummy") + "</illustrator>" + "\r\n";
                temp += Header(4) + "<effected_by>" + ((difficulty == j) ? effect : "dummy") + "</effected_by>" + "\r\n";
                temp += Header(4) + "<price __type=\"s32\">-1</price>" + "\r\n";
                temp += Header(4) + "<limited __type=\"u8\">3</limited>" + "\r\n";
                temp += Header(3) + "</" + difficulty_db[j] + ">" + "\r\n";
                if (checkBox1.Checked == true) break;
            }
            if (checkBox1.Checked == false)
            {
                temp += Header(2) + "</difficulty>" + "\r\n";
                temp += Header(1) + "</music>" + "\r\n";
            }
            //DIFFICULTY
            textBox1.Text = temp;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (textBox1.Text != "")Clipboard.SetText(textBox1.Text);
        }
        public string Header(short j )
        {
            String dividen = "  ";
            String temp = "";
            for (int i = 0; i < j; i++)
                temp += dividen;

            return temp;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            metadata_change();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            metadata_change();
        }

        public string position_string(int []i)
        {
            return i[0].ToString("000") + "," + i[1].ToString("00") + "," + i[2].ToString("00");
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            metadata_change();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            metadata_change();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            metadata_change();
            button2.Enabled = false;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            button2.Enabled = false;
        }
    }
}
