using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BL;
using AccessToDB;
using Microsoft.VisualStudio.Threading;
using UI.IViews;

namespace UI.WinFormsViews
{
    public partial class ProfileView : Form, IProfileView
    {

        public event AsyncEventHandler LogInClicked;
        public event AsyncEventHandler LogOutClicked;
        public event AsyncEventHandler RegisterClicked;
        public event EventHandler CloseClicked;

        public bool LogInEnabled
        {
            get { return logInButton.Enabled; }
            set {logInButton.Enabled = value; }
        }
        public bool LogOutEnabled
        {
            get { return logOutButton.Enabled; }
            set { logOutButton.Enabled = value; }
        }
        public bool RegisterEnabled
        {
            get { return registerButton.Enabled; }
            set { registerButton.Enabled = value; }
        }
        public string Email
        {
            get { return emailTextBox.Text; }
            set { emailTextBox.Text = value; }
        }
        public string Password
        {
            get { return passwordTextBox.Text; }
            set { passwordTextBox.Text = value; }
        }
        public string cardID
        {
            get { return cardIDTextBox.Text; }
            set { cardIDTextBox.Text = value; }
        }
        public ProfileView()
        {
            InitializeComponent();

        }


        private void logInButton_Click(object sender, EventArgs e)
        {
            LogInClicked?.Invoke(this, new EventArgs());
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            RegisterClicked?.Invoke(this, new EventArgs());
        }

        private void logOutButton_Click(object sender, EventArgs e)
        {
            LogOutClicked?.Invoke(this, new EventArgs());
        }

        public void Open()
        {
            base.ShowDialog();
        }

        private void ProfileView_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseClicked?.Invoke(this, new EventArgs());
        }
    }
}
