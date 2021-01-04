using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        public Form1()
        {
            InitializeComponent();
        }

        ChromiumWebBrowser browser = new ChromiumWebBrowser("file://" + Program.htmlPath);
        private void Form1_Load(object sender, EventArgs e)
        {
            CefSettings settings = new CefSettings();
            Cef.EnableHighDPISupport();
            browser.Dock = DockStyle.Fill;
            this.Controls.Add(browser);
            browser.TitleChanged += Browser_TitleChanged;
            this.Text = browser.Text;
            ProtonMenuHandler handler = new ProtonMenuHandler();
            browser.MenuHandler = handler;
            browser.AddressChanged += Browser_AddressChanged;
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
    }
}
