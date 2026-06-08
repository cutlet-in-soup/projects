using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Wafanda_calculator
{
    public partial class Form1 : Form
    {
        private Point startPoint;
        private Point endPoint;
        private bool isStartPointSet = false;
        private bool isEndPointSet = false;
        private Point currentMousePosition;

        private LowLevelKeyboardProc _procKeyboard;
        private IntPtr _hookIDKeyboard = IntPtr.Zero;
        private Form overlayForm;
        private Label overlayLabelLPix;
        private Label overlayLabelAngle;

        public Form1()
        {
            InitializeComponent();
            pictureBox.BackColor = Color.Transparent;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.BackColor = Color.FromArgb(128, Color.White);

            _procKeyboard = HookCallbackKeyboard;
            _hookIDKeyboard = SetHook(_procKeyboard);

            Timer timer1 = new Timer();
            timer1.Interval = 100; // Интервал в миллисекундах
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();

            this.FormClosing += (s, e) =>
            {
                UnhookWindowsHookEx(_hookIDKeyboard);
            };

            // Создаем наложение (overlay) форму
            CreateOverlayForm();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            currentMousePosition = Cursor.Position;
            UpdateOverlay();
        }

        private void SetStartPoint()
        {
            startPoint = pictureBox.PointToClient(currentMousePosition);
            isStartPointSet = true;
            isEndPointSet = false;
            pictureBox.Invalidate();
        }

        private void SetEndPoint()
        {
            endPoint = pictureBox.PointToClient(currentMousePosition);
            isEndPointSet = true;
            pictureBox.Invalidate();
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (isStartPointSet)
            {
                e.Graphics.DrawEllipse(Pens.Green, startPoint.X - 6, startPoint.Y - 6, 12, 12);
            }
            if (isStartPointSet && isEndPointSet)
            {
                e.Graphics.DrawLine(Pens.Red, startPoint, endPoint);
                e.Graphics.DrawEllipse(Pens.Green, endPoint.X - 10, endPoint.Y - 10, 20, 20);

                double deltaX = endPoint.X - startPoint.X;
                double deltaY = endPoint.Y - startPoint.Y;
                double angleInRadians = Math.Atan2(deltaY, deltaX);
                double angleInDegrees = angleInRadians * (180 / Math.PI);
                if (angleInDegrees < 0)
                {
                    angleInDegrees += 360;
                }
                angleInDegrees += 90;
                if (angleInDegrees > 360)
                {
                    angleInDegrees -= 360;
                }
                double distanceInPixels = Math.Sqrt(Math.Pow(endPoint.X - startPoint.X, 2) + Math.Pow(endPoint.Y - startPoint.Y, 2));
                double distanceInMeters = ((double)distance_map.Value / 111) * distanceInPixels;
                distanceInMeters = Math.Ceiling(distanceInMeters);
                distance_label.Text = $"{distanceInMeters} м";
                LPix.Text = $"{distanceInPixels:F2} px";
                angleLabel.Text = $"{angleInDegrees:F1} angle";

                // Обновляем наложение (overlay) текст
                UpdateOverlay();
            }
        }

        private void CreateOverlayForm()
        {
            overlayForm = new Form();
            overlayForm.FormBorderStyle = FormBorderStyle.None;
            overlayForm.BackColor = Color.Black;
            overlayForm.Opacity = 0.5;
            overlayForm.TopMost = true;
            overlayForm.StartPosition = FormStartPosition.Manual;
            overlayForm.Size = new Size(200, 100);
            overlayForm.ShowInTaskbar = false;

            overlayLabelLPix = new Label();
            overlayLabelLPix.ForeColor = Color.White;
            overlayLabelLPix.Dock = DockStyle.Top;
            overlayLabelLPix.TextAlign = ContentAlignment.MiddleCenter;

            overlayLabelAngle = new Label();
            overlayLabelAngle.ForeColor = Color.White;
            overlayLabelAngle.Dock = DockStyle.Bottom;
            overlayLabelAngle.TextAlign = ContentAlignment.MiddleCenter;

            overlayForm.Controls.Add(overlayLabelLPix);
            overlayForm.Controls.Add(overlayLabelAngle);

            overlayForm.Show();
        }

        private void UpdateOverlay()
        {
            overlayLabelLPix.Text = distance_label.Text;
            overlayLabelAngle.Text = angleLabel.Text;
            overlayForm.Location = new Point(Screen.PrimaryScreen.WorkingArea.Right - overlayForm.Width - 10, 10);
        }

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private IntPtr HookCallbackKeyboard(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN))
            {
                int vkCode = Marshal.ReadInt32(lParam);
                if (vkCode == (int)Keys.H)
                {
                    SetStartPoint();
                }
                else if (vkCode == (int)Keys.J)
                {
                    SetEndPoint();
                }
            }
            return CallNextHookEx(_hookIDKeyboard, nCode, wParam, lParam);
        }

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_SYSKEYDOWN = 0x0104;

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
