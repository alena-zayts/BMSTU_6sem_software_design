using BL.Models;
using Microsoft.VisualStudio.Threading;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.IViews;

namespace UI.WinFormsViews
{
    public partial class UserViewWinForm : Form, IUserView
    {
        public bool GetUserEnabled
        {
            get { return GetUserButton.Enabled; }
            set { GetUserButton.Enabled = value; }
        }
        public bool GetUsersEnabled
        {
            get { return GetUsersButton.Enabled; }
            set { GetUsersButton.Enabled = value; }
        }

        public bool UpdateEnabled
        {
            get { return UpdateButton.Enabled; }
            set { UpdateButton.Enabled = value; }
        }
        public bool AddEnabled
        {
            get { return AddButton.Enabled; }
            set { AddButton.Enabled = value; }
        }
        public bool DeleteEnabled
        {
            get { return DeleteButton.Enabled; }
            set { DeleteButton.Enabled = value; }
        }
        public string UserID
        {
            get { return UserIDTextBox.Text; }
            set { UserIDTextBox.Text = value; }
        }
        public string UserEmail
        {
            get { return EmailTextBox.Text; }
            set { EmailTextBox.Text = value; }
        }
        public string Password
        {
            get { return PasswordTextBox.Text; }
            set { PasswordTextBox.Text = value; }
        }
        public string Permissions
        {
            get { return PermissionsTextBox.Text; }
            set { PermissionsTextBox.Text = value; }
        }
        public string CardID
        {
            get { return CardIDTextBox.Text; }
            set { CardIDTextBox.Text = value; }
        }
        public List<User> Users
        {
            set
            {
                UsersDataGridView.Rows.Clear();
                foreach (User user in value)  
                {
                    string[] row = new string[5];
                    row[0] = user.UserID.ToString();
                    row[4] = user.CardID.ToString();
                    row[1] = user.UserEmail.ToString();
                    row[2] = user.Password.ToString();
                    row[3] = user.Permissions.ToString();

                    UsersDataGridView.Rows.Add(row);
                }
            }
        }

        public UserViewWinForm()
        {
            InitializeComponent();
        }

        public event AsyncEventHandler GetUserClicked;
        public event AsyncEventHandler GetUsersClicked;
        public event AsyncEventHandler UpdateClicked;
        public event AsyncEventHandler AddClicked;
        public event AsyncEventHandler DeleteClicked;
        public event EventHandler CloseClicked;

        private void GetUserButton_Click(object sender, EventArgs e)
        {
            GetUserClicked?.Invoke(this, new EventArgs());
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            UpdateClicked?.Invoke(this, new EventArgs());
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            AddClicked?.Invoke(this, new EventArgs());
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DeleteClicked?.Invoke(this, new EventArgs());
        }

        private void GetUsersButton_Click(object sender, EventArgs e)
        {
            GetUsersClicked?.Invoke(this, new EventArgs());
        }

        public void Open()
        {
            Show();
        }

        private void UserViewWinForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseClicked?.Invoke(this, new EventArgs());
        }
    }
}
