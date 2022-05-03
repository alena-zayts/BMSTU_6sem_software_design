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

namespace AuthorizationComponent
{
    public partial class ProfileView : Form, IProfileView
    {
        private Facade _facade;

        public event AsyncEventHandler LogInClicked;
        public event AsyncEventHandler LogOutClicked;
        public event AsyncEventHandler RegisterClicked;

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
        public string Password { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string cardID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ProfileView()
        {
            InitializeComponent();

        }


        private void logInButton_Click(object sender, EventArgs e)
        {
            string email = emailTextBox.Text;
            string password = passwordTextBox.Text;
            string cardID = cardIDTextBox.Text;

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


    }
}
