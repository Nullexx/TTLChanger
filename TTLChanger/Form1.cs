using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;

namespace TTLChanger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Follow these steps: \n1. Enter a new TTL value. \n2. Click the Change Regedit button to change the registry value. \n3. Click the Update Regedit button to save the changes. \nWaThe computer will restart, so close your applications in advance.");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            
            
            label3.Text += Environment.UserName;
            label2.Text += Registry.GetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters", "DefaultTTL", "DefaultTTL"); ;
            MessageBox.Show("Follow these steps: \n1. Enter a new TTL value. \n2. Click the Change Regedit button to change the registry value. \n3. Click the Update Regedit button to save the changes. \nWaThe computer will restart, so close your applications in advance.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int TTLValue = Convert.ToInt32(textBox1.Text);
                CreateREGFile(TTLValue);
            }
            catch {
                MessageBox.Show("Enter a numeric value!");
            }
            
        }
        private void CreateREGFile(int value) {
            string Version = "Windows Registry Editor Version 5.00";
            string TCPIPPath = "[HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters]";
            string TTL = "\"DefaultTTL\"" + "=" + "\""+value+"\"";
            string TCPIP6Path = "[HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\TCPIP6\\Parameters]";
            string ALLTEXT = Version + "\n" + TCPIPPath + "\n" + TTL + "\n" + TCPIP6Path + "\n" + TTL;

            SaveREGFile(ALLTEXT);
        }

        private void SaveREGFile(string text) {
            string path = "C:\\Users\\"+Environment.UserName+"\\ttl.reg";

            using (StreamWriter s = new StreamWriter(path, false)) {
                s.WriteLineAsync(text);
            }
            StartChanges(path);
        }

        private void StartChanges(string path) {
            MessageBox.Show("Click \"Yes\" to change Register!");
            Process.Start(path);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The computer will be rebooted now!\nClose your applications and Click \"OK\" to continue!");
            Process.Start("shutdown", "/s /t 0");
        }
    }
}
