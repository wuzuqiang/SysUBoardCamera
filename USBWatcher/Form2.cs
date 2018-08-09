﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EZUSB;
using Splash.IO.PORTS;
using DirectShowLib;

namespace Splash
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        MyUsbWatcherAboutCameraOper watcher = new MyUsbWatcherAboutCameraOper();
        private void Form2_Load(object sender, EventArgs e)
        {
            //SetText(DateTime.Now + "：Test is can Success??");
            string strResult = watcher.AddWatcher();
            //MessageBox.Show(strResult);
            watcher.eventCameraInsert += Watcher_eventCameraInsert1;
            watcher.eventCameraRemove += Watcher_eventCameraRemove;
        }

        private void Watcher_eventCameraRemove(string cameraName)
        {
            SetText(DateTime.Now + "：");
            SetText("移除摄像头：" + cameraName);
        }

        private void Watcher_eventCameraInsert1(string cameraName)
        {
            SetText(DateTime.Now + "：");
            SetText("插入摄像头：" + cameraName);
        }

        private void Watcher_eventCameraInsert(DsDevice[] VideoInputDevices)
        { 
            SetText(DateTime.Now + "：\n");
            foreach (DsDevice ds in VideoInputDevices)
            {
                SetText(ds.Name + "\r\n");
            }
        }

        private void Watcher_eventUsbInsert(List<USBControllerDevice> listUSBControllerDev)
        {
            throw new NotImplementedException();
        }

        int i = 0;
        private void Watcher_eventUsbChanged(List<USBControllerDevice> listUSBControllerDev)
        {
            foreach (USBControllerDevice Device in listUSBControllerDev)
            {
                this.SetText(i++ + "：");
                this.SetText("\tAntecedent：" + Device.Antecedent + "\r\n");
                this.SetText("\tDependent：" + Device.Dependent + "\r\n");
            }
        }

        // 对 Windows 窗体控件进行线程安全调用
        private void SetText(String text)
        {
            text = text + "\n";
            if (this.textBox1.InvokeRequired)
            {
                this.textBox1.BeginInvoke(new Action<String>((msg) =>
                {
                    this.textBox1.AppendText(msg);
                }), text);
            }
            else
            {
                this.textBox1.AppendText(text);
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            watcher.DelWatcher();
        }
    }
}
