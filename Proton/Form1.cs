using CefSharp;
using CefSharp.WinForms;
using Decent.Minecraft.Client.Java;
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
        bool showing = true;
        public Form1()
        {
            InitializeComponent();
        }

        ChromiumWebBrowser browser = new ChromiumWebBrowser("file://" + Program.htmlPath);
        ChromiumWebBrowser titleBar = new ChromiumWebBrowser("file://" + Program.titleBarHtml);
        private void Form1_Load(object sender, EventArgs e)
        {
            CefSharpSettings.WcfEnabled = true;
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
            ProtonEssentials proton = new ProtonEssentials(this);
            browser.JavascriptObjectRepository.Register("proton", proton, isAsync: false, options: BindingOptions.DefaultBinder);
            //Icon = Icon.ExtractAssociatedIcon(Program.iconPath);
            if (Program.isOverlay)
            {
                this.Location = new Point(0, 0);
                this.TopMost = true;
                guna2ControlBox1.Visible = false;
                guna2ControlBox2.Location = guna2ControlBox1.Location;
                KeyPreview = true;
            }
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.F))
            {
                if(showing)
                {
                    showing = false;
                    this.WindowState = FormWindowState.Minimized;
                }
                else
                {
                    showing = true;
                    this.WindowState = FormWindowState.Normal;
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
