using Blog_Recovery_Tool.Properties;
using Google.Apis.Blogger.v3;
using Google.Apis.Blogger.v3.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blog_Recovery_Tool
{
    public partial class Form1 : Form
    {
        public static string SessionId = new Random().Next(0, 99999).ToString()+ new Random().Next(0, 99999).GetHashCode().ToString();
        BloggerService MyBlog = null;
        public Form1()
        {
            this.Icon = Resources.icon;
            InitializeComponent();
        }

        private static string title="";
        private static string content="";
        private void LoadPage(string url)
        {
            var Post = new Parser(url);
            title = Post.Title();
            content = Post.Main();
        }
        private void btnPost_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            var id = txtID.Text;
            if (btnPost.Text != "Upload Post")
            {
                LoadPage(txtUrl.Text);


                lblTitle.Text = title;
                webBrowser1.DocumentText = content;
                btnPost.Text = "Upload Post";
                btnPost.ForeColor = Color.Green;
            }
            else
            {
                Post(content, title, id);
                btnPost.Text = "Load Post";
                btnPost.ForeColor = SystemColors.ControlText;
            }
            Cursor.Current = Cursors.Default;
        }

        private void txtUrl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
                btnPost_Click(sender, e);
        }
        private void btnImport_Click(object sender, EventArgs e)
        {            
            txtUrl.Text = "(LIST)";
            txtUrl.Enabled = false;
            var list = Dialog.GetList();
            foreach(string page in list)
            {
                var id = txtID.Text;
                LoadPage(page);
                Post(content, title, id);
            }
            btnImport.Enabled = true;
            btnPost.Enabled = true;
        }
        private void Post(string content, string title, string id)
        {

            MyBlog = Auth.AuthenticateOauth();
            if (MyBlog != null)
            {
                var post = new Post { Content = content, Title = title };
                var result = MyBlog.Posts.Insert(post, id).Execute();
                txtUrl.Text = "";
                txtUrl.Enabled = true;
                txtOut.Text = result.Url ?? "" + Environment.NewLine;
                txtID_TextChanged(null, null);
                txtUrl_TextChanged(null, null);
                webBrowser1.DocumentText = "";
                lblTitle.Text = "";
            }         
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {
            if(txtID.Text.Length != 0)
            {
                btnImport.Enabled = true;
            }
            else
            {
                btnImport.Enabled = false;
            }
        }

        private void txtUrl_TextChanged(object sender, EventArgs e)
        {
            if(txtUrl.Text.Length != 0)
            {
                btnPost.Enabled = true;
            }
            else
            {
                btnPost.Enabled = false;
            }
        }
    }
}
