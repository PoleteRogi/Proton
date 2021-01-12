using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proton
{
    public partial class Form1 : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        public Form1()
        {
            InitializeComponent();
        }

        ChromiumWebBrowser browser = new ChromiumWebBrowser("file://" + Program.htmlPath);
        ChromiumWebBrowser titleBar = new ChromiumWebBrowser("file://" + Program.titleBarHtml);
        private void Form1_Load(object sender, EventArgs e)
        {
            CefSettings settings = new CefSettings();
            Cef.EnableHighDPISupport();
            browser.Dock = DockStyle.Fill;
            guna2Panel2.Controls.Add(browser);
            browser.TitleChanged += Browser_TitleChanged;
            this.Text = browser.Text;
            ProtonMenuHandler handler = new ProtonMenuHandler();
            browser.MenuHandler = handler;
            browser.AddressChanged += Browser_AddressChanged;
            if(Program.executesJs)
            {
                StartJSHandler jshandler = new StartJSHandler();
                jshandler.startScript = Program.jsExecuted;
                browser.RenderProcessMessageHandler = jshandler;
            }
            browser.LoadingStateChanged += Browser_LoadingStateChanged;
            titleBar.ExecuteScriptAsyncWhenPageLoaded("document.body.style.backgroundColor = 'rgb(64, 64, 64)';");
            titleBar.ExecuteScriptAsyncWhenPageLoaded("document.body.style.color = 'white';");
            titleBar.ExecuteScriptAsyncWhenPageLoaded("document.body.style.overflow = 'hidden';");
            titleBar.ExecuteScriptAsyncWhenPageLoaded("document.body.style.marginTop = '5px';");
            titleBar.AllowDrop = false;
            guna2Panel3.Controls.Add(titleBar);
            titleBar.MenuHandler = handler;
            Icon = Icon.ExtractAssociatedIcon(Program.iconPath);
        }

        private void TitleBar_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void TitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void Browser_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            DirectoryInfo tempDirInfo = new DirectoryInfo(Path.GetDirectoryName(Program.htmlPath) + @"\GPUCache\");
            tempDirInfo.Attributes = FileAttributes.Hidden;
        }

        private void Browser_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            //browser.Load("file://" + Environment.CurrentDirectory + "/index.html");
            //MessageBox.Show("WebPage changed");
        }

        private void Browser_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            this.Text = e.Title;
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
