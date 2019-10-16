using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WOWAutoFishing
{
    public class AutoFishing
    {
        #region 必要引用
        [DllImport("user32.dll")]
        private static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);//导入激活指定窗口的方法
        [DllImport("user32.dll")]
        private static extern void SetCursorPos(int x, int y);//导入移动鼠标的方法
        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);//导入移动鼠标的按键方法
        //移动鼠标 
        const int MOUSEEVENTF_MOVE = 0x0001;
        //模拟鼠标左键按下 
        const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        //模拟鼠标左键抬起 
        const int MOUSEEVENTF_LEFTUP = 0x0004;
        //模拟鼠标右键按下 
        const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        //模拟鼠标右键抬起 
        const int MOUSEEVENTF_RIGHTUP = 0x0010;
        //模拟鼠标中键按下 
        const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        //模拟鼠标中键抬起 
        const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        //标示是否采用绝对坐标 
        const int MOUSEEVENTF_ABSOLUTE = 0x8000;
        #endregion

        #region 带鼠标截屏必要引用
        private const Int32 CURSOR_SHOWING = 0x00000001;
        [DllImport("user32.dll")]
        static extern bool GetCursorInfo(out CURSORINFO pci);

        [StructLayout(LayoutKind.Sequential)]
        private struct CURSORINFO
        {
            public Int32 cbSize;
            public Int32 flags;
            public IntPtr hCursor;
            public System.Drawing.Point ptScreenPos;
        }
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point pt);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool BRePaint);
        #endregion

        #region 全局参数
        //string log_path = @"C:\Users\DELL\Documents\Visual Studio 2008\Projects\Happy_Fishing\Logs.txt"; 
        private Bitmap baseCursorImage = (Bitmap)Image.FromFile(@".\base_cursor.png");
        //Bitmap base_giant_float_buff_image = (Bitmap)Image.FromFile(@".\giant_float_buff.jpg");
        #endregion

        //屏幕上方预留出的高度
        private int reservedHeight = 200;

        public int ScreenWidth { get; set; }

        //屏幕高度
        public int ScreenHeight { get; set; }

        public int xStepLength = 20;   //定义横向的步长

        public int yStepLength = 25;   //定义纵向的步长

        public int moveStepTime = 10;  //定义鼠标移动的时间间隔

        public string MacroKey { get; set; }

        public int MacroWaitTime { get; set; }

        public string Key { get; set; }

        public event EventHandler<SendKeyEventArgs> SendKey;

        public void MainAutoFishingThread()
        {
            //初始化鱼漂位置变量
            Point cursorPosition = new Point(0, 0);

            while (true)
            {
                Thread.Sleep(500);

                if (!string.IsNullOrEmpty(MacroKey))
                {
                    SendKey?.Invoke(this, new SendKeyEventArgs(MacroKey));

                    Thread.Sleep(MacroWaitTime);
                }

                //发送按键开始钓鱼
                //SendKeys.Send(keyTextBox.Text);
                SendKey?.Invoke(this, new SendKeyEventArgs(Key));

                //等待鱼漂稳定
                Thread.Sleep(2000);

                //如果当前鼠标就在鱼漂位置，则不进行查找
                if (!IsFishingCursor())
                {
                    //每次寻找3次鱼漂
                    for (int i = 0; i < 3; i++)
                    {
                        cursorPosition = GetFishingCursorPosition();

                        if (cursorPosition.X != 0 && cursorPosition.Y != 0)
                            break;
                    }

                    //找了3次还没有，跳出本次循环
                    if (cursorPosition.X == 0 && cursorPosition.Y == 0)
                        continue;
                }

                //找到了之后，将鱼漂移动到最左上位置，方便截图
                Point bufPoint = MoveToFishingLeftTop(cursorPosition);

                //找到了之后，快速监听鼠标向右100向下100的范围内，是否有纯白色出现（水花判断）
                int waterListenAllTime = 0;   //监听水花总时长
                int waterListenSleep = 10;  //监听水花每次Sleep时长
                int waterListenMax = 20000; //监听水花最大时长 25秒
                int imgWidth = 100;
                int imgHeight = 100;

                while (waterListenAllTime <= waterListenMax)
                {
                    //创建图像，用于保存截图
                    Image img = new Bitmap(imgWidth, imgHeight);
                    Graphics imgGraphics = Graphics.FromImage(img);
                    //设置截屏区域
                    //开始位置为鼠标位置，截取高宽为100的图片
                    imgGraphics.CopyFromScreen
                        (
                        bufPoint,
                        new Point(0, 0),
                        new Size(imgWidth, imgHeight)
                        );

                    //判断是否有水花
                    Bitmap bitmap = (Bitmap)img;

                    bool isFind = false;
                    int findTimes = 0;
                    int findTimesMax = 5;

                    for (int i = 0; i < imgWidth; i++)
                    {
                        if (isFind)
                            break;

                        for (int n = 0; n < imgHeight; n++)
                        {
                            var pixel = bitmap.GetPixel(i, n);

                            if (pixel.R == 255 & pixel.B == 255 && pixel.G == 255)
                            {
                                findTimes++;

                                if (findTimes >= findTimesMax)
                                {
                                    isFind = true;
                                    break;
                                }
                            }
                        }
                    }

                    //查询到了水花
                    if (isFind)
                    {
                        Thread.Sleep(750);//在鱼上钩后先等鱼漂不再晃动

                        SetCursorPos(cursorPosition.X, cursorPosition.Y); //重新移动鼠标到鱼漂位置

                        mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);

                        Thread.Sleep(500);

                        //将鼠标位置向左上角移动（10，10），以防上一次鼠标点击时没有自动拾取物品
                        SetCursorPos(cursorPosition.X - 10, cursorPosition.Y - 10);

                        mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0); 
                    }

                    //释放
                    bitmap.Dispose();
                    imgGraphics.Dispose();
                    img.Dispose();

                    if (isFind)
                        break;

                    waterListenAllTime += waterListenSleep;
                    Thread.Sleep(waterListenSleep);
                }
            }
        }

        private Point MoveToFishingLeftTop(Point point)
        {
            int xPosition = 0;
            int yPosition = 0;

            //先移动到最左边
            for (int x = 1; true; x++)
            {
                SetCursorPos(point.X - x, point.Y);

                //一直移动到不是鱼漂样式为止
                if (!IsFishingCursor())
                {
                    xPosition = point.X - x;

                    break;
                }
            }

            //再移动到最上方
            for (int y = 1; true; y++)
            {
                SetCursorPos(xPosition, point.Y - y);

                if (!IsFishingCursor())
                {
                    yPosition = point.Y - y;

                    break;
                }
            }

            //重新移动
            SetCursorPos(xPosition, yPosition);

            return new Point(xPosition, yPosition);
        }

        //获取鱼漂位置
        private Point GetFishingCursorPosition()
        {
            Point cursorPosition = new Point(0, 0); //初始化找到的鱼漂的位置
            bool isFind = false;

            int xStartPosition = ScreenWidth / 2 / 2 / 2;   //定义横向开始的位置
            int xStopPosition = ScreenWidth - xStartPosition;//定义横向结束的位置
            int yStopPosition = Int32.Parse(Math.Round(ScreenHeight / 1.5, 0).ToString());           //定义纵向结束的位置

            for (int y = reservedHeight; y <= yStopPosition; y += yStepLength)
            {
                if (isFind)
                    break;

                for (int x = xStartPosition; x <= xStopPosition; x += xStepLength)
                {
                    //移动鼠标到指定位置
                    SetCursorPos(x, y);

                    if (IsFishingCursor())
                    {
                        cursorPosition.X = x;
                        cursorPosition.Y = y;
                        isFind = true;
                        break;
                    }

                    Thread.Sleep(moveStepTime);
                }
            }

            return cursorPosition;
        }

        //判断鼠标样式是否是放到了鱼漂上的样式
        private bool IsFishingCursor()
        {
            //获取出当前鼠标的样式，并转为图片
            CURSORINFO pci;
            pci.cbSize = Marshal.SizeOf(typeof(CURSORINFO));

            GetCursorInfo(out pci);
            Cursor cur = new Cursor(pci.hCursor);
            Image img = new Bitmap(cur.Size.Width, cur.Size.Height);
            Graphics gc = Graphics.FromImage(img);
            cur.Draw(gc, new Rectangle(0, 0, cur.Size.Width, cur.Size.Height));

            bool res = ImageWhetherMatch(baseCursorImage, (Bitmap)img);

            gc.Dispose();
            img.Dispose();

            return res;
        }

        //对比两张图片是否一致
        private bool ImageWhetherMatch(Bitmap base_image, Bitmap target_image)
        {
            bool image_whether_match = true;

            #region 获取基准图片和目标图片的宽与高            
            int base_image_width = base_image.Width;
            int base_image_height = base_image.Height;
            int target_image_width = target_image.Width;
            int target_image_height = target_image.Height;
            #endregion

            #region [ 重要 ]核心对比代码
            if (target_image_width != base_image_width || target_image_height != base_image_height)//首先对比大小，大小不一致就不必继续判断
                image_whether_match = false;

            else for (int w = 0; w < base_image_width; w++)
                {
                    for (int h = 0; h < base_image_height; h++)//其次按照像素点一一对比
                    {
                        if (target_image.GetPixel(w, h) != base_image.GetPixel(w, h))
                        {
                            image_whether_match = false;
                            break;
                        }
                    }

                    if (!image_whether_match)
                        break;
                }
            #endregion

            return image_whether_match;
        }
    }
}
