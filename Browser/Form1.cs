using System;
using System.Drawing;
using System.Windows.Forms;

namespace Browser
{
    public partial class MainForm : Form
    {
        int count = 1;

        public MainForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Form load event handler, calls LoadFrom to load the initial page.
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadPage(addressBar.Text);
        }

        /// <summary>
        /// TabPage selecting event handler, if the selected tab is a tabAdd, 
        /// a new tab is created with a WebBrowser inside
        /// and inserted to the list of tabs.
        /// </summary>
        private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            int addIdx = tabControl.TabCount - 1;
            if (tabControl.SelectedIndex == addIdx)
            {
                WebBrowser wb = new WebBrowser();

                wb.Dock = DockStyle.Fill;
                wb.MinimumSize = new Size(20, 20);
                wb.Name = "webBrowser" + (++count).ToString();
                wb.TabIndex = count;
                wb.Url = new Uri("http://google.com");
                wb.Tag = "http://google.com";
                wb.Navigated += new WebBrowserNavigatedEventHandler(webBrowser_Navigated);

                TabPage tp = new TabPage();
                tp.Controls.Add(wb);
                tp.Name = "tabPage" + count.ToString();
                tp.Size = new Size(974, 551);
                tp.Text = "Page " + count.ToString() + "        ";
                tp.UseVisualStyleBackColor = true;
                tp.Tag = "http://google.com";
                tabControl.TabPages.Insert(addIdx, tp);

                tabControl.SelectedIndex = addIdx;
            }
        }

        /// <summary>
        /// Selected tab index changed event handler, the current address is entered
        /// in the address bar, which is contained in the tag of the selected tab.
        /// </summary>
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            addressBar.Text = tabControl.SelectedTab.Tag.ToString();
        }

        /// <summary>
        /// Address bar key press event handler, if enter is pressed - 
        /// the page is loaded upon request in the address bar.
        /// </summary>
        private void addressBar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                LoadPage("https://www.google.co.in/search?q=" + addressBar.Text);
            }
        }

        /// <summary>
        /// Web browser navigated event handler, changes the tag of the selected tab 
        /// to the current address and call LoadLink method.
        /// </summary>
        private void webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            string url = (tabControl.SelectedTab.Controls[0] as WebBrowser).Url.ToString();
            tabControl.SelectedTab.Tag = url;
            LoadLink(url);
        }

        /// <summary>
        /// Method that loads the page in a web browser.
        /// </summary>
        /// <param name="link">Link to download</param>
        private void LoadPage(string link)
        {
            tabControl.SelectedTab.Tag = link;
            (tabControl.SelectedTab.Controls[0] as WebBrowser).Navigate(link);
            LoadLink(link);
        }

        /// <summary>
        /// Method that display the current address in the address bar 
        /// and call LoadLink method.
        /// </summary>
        /// <param name="link">Link to download</param>
        private void LoadLink(string link)
        {
            addressBar.Text = link;
        }
    }
}
