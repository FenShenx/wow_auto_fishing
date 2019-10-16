using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WOWAutoFishing
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        private static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);//导入激活指定窗口的方法

        //主线程
        private Thread mainThread { get; set; }

        //屏幕宽度
        private int screenWidth { get; set; }

        //屏幕高度
        private int screenHeight { get; set; }

        //wow进程的名字
        private string wowName = "Wow";

        public Form1()
        {
            InitializeComponent();

            Control.CheckForIllegalCrossThreadCalls = false;

            Rectangle rec = Screen.GetWorkingArea(this);

            screenWidth = rec.Width;
            screenHeight = rec.Height;
        }

        private void Start()
        {
            if (string.IsNullOrEmpty(keyTextBox.Text))
            {
                MessageBox.Show("请输入快捷键");

                return;
            }

            if (keyTextBox.Text.Length != 1)
            {
                MessageBox.Show("快捷键只能为一个按键");

                return;
            }

            if ("qwertyuiopasdfghjklzxcvbnm".IndexOf(keyTextBox.Text.ToLower()) < 0)
            {
                MessageBox.Show("快捷键只能为a-z");

                return;
            }

            if (string.IsNullOrEmpty(xStepTextBox.Text))
            {
                MessageBox.Show("请输入横向步长");

                return;
            }

            if (string.IsNullOrEmpty(yStepTextBox.Text))
            {
                MessageBox.Show("请输入纵向步长");

                return;
            }

            if (string.IsNullOrEmpty(moveTimeTextBox.Text))
            {
                MessageBox.Show("请输入移动时间间隔");

                return;
            }

            if (!string.IsNullOrEmpty(macroKeyTextBox.Text))
            {
                if (macroKeyTextBox.Text.Length != 1)
                {
                    MessageBox.Show("宏的快捷键只能设置一个");

                    return;
                }

                if ("qwertyuiopasdfghjklzxcvbnm".IndexOf(macroKeyTextBox.Text.ToLower()) < 0)
                {
                    MessageBox.Show("宏的快捷键只能为a-z");

                    return;
                }
            }

            keyTextBox.Enabled = false;
            startButton.Enabled = false;
            stopButton.Enabled = true;
            xStepTextBox.Enabled = false;
            yStepTextBox.Enabled = false;
            moveTimeTextBox.Enabled = false;
            macroKeyTextBox.Enabled = false;
            macroTimeTextBox.Enabled = false;

            //获取Wow进程
            Process[] temp = Process.GetProcessesByName(wowName);

            if (temp.Length > 0)
            {
                IntPtr process_handler = temp[0].MainWindowHandle;
                SwitchToThisWindow(process_handler, true);//激活指定进程的窗口
            }
            else
            {
                MessageBox.Show("魔兽世界未打开！");
                return;
            }

            AutoFishing autoFishing = new AutoFishing();
            autoFishing.ScreenWidth = screenWidth;
            autoFishing.ScreenHeight = screenHeight;
            autoFishing.SendKey += SendKeyEvent;
            autoFishing.Key = keyTextBox.Text;
            autoFishing.xStepLength = Int32.Parse(xStepTextBox.Text);
            autoFishing.yStepLength = Int32.Parse(yStepTextBox.Text);
            autoFishing.moveStepTime = Int32.Parse(moveTimeTextBox.Text);
            autoFishing.MacroKey = macroKeyTextBox.Text;
            autoFishing.MacroWaitTime = Int32.Parse(macroTimeTextBox.Text) * 1000;  //转为毫秒

            mainThread = new Thread(new ThreadStart(autoFishing.MainAutoFishingThread));
            mainThread.IsBackground = true;
            mainThread.Start();
        }

        //发送按键的委托
        private delegate void SendKeyEventDelegate(object sender, SendKeyEventArgs e);

        //执行发送按键
        private void SendKeyEvent(object sender, SendKeyEventArgs e)
        {
            if (this.InvokeRequired)
            {
                SendKeyEventDelegate sendKeyEventDelegate = SendKeyEvent;
                object[] o = new object[] { sender, e };
                this.Invoke(sendKeyEventDelegate, o);
            }
            else
            {
                SendKeys.Send(e.Key);
            }
        }

        private void Stop()
        {
            mainThread.Abort();
            mainThread = null;

            keyTextBox.Enabled = true;
            startButton.Enabled = true;
            stopButton.Enabled = false;

            xStepTextBox.Enabled = true;
            yStepTextBox.Enabled = true;
            moveTimeTextBox.Enabled = true;

            macroKeyTextBox.Enabled = true;
            macroTimeTextBox.Enabled = true;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            Stop();
        }
    }
}
