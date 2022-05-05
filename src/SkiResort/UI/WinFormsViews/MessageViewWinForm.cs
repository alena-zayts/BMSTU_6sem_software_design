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
    public partial class MessageViewWinForm : Form, IMessageView
    {
        public bool GetMessageEnabled
        {
            get { return GetMessageButton.Enabled; }
            set { GetMessageButton.Enabled = value; }
        }
        public bool GetMessagesEnabled
        {
            get { return GetMessagesButton.Enabled; }
            set { GetMessagesButton.Enabled = value; }
        }
        public bool MarkCheckedEnabled
        {
            get { return MarkCheckedButton.Enabled; }
            set { MarkCheckedButton.Enabled = value; }
        }
        public bool SendEnabled
        {
            get { return SendButton.Enabled; }
            set { SendButton.Enabled = value; }
        }
        public bool UpdateEnabled
        {
            get { return UpdateButton.Enabled; }
            set { UpdateButton.Enabled = value; }
        }
        public bool DeleteEnabled
        {
            get { return DeleteButton.Enabled; }
            set { DeleteButton.Enabled = value; }
        }
        public string MessageID
        {
            get { return MessageIDTextBox.Text; }
            set { MessageIDTextBox.Text = value; }
        }
        public string SenderID
        {
            get { return SenderIDTextBox.Text; }
            set { SenderIDTextBox.Text = value; }
        }
        public string CheckedByID
        {
            get { return CheckedByIDTextBox.Text; }
            set { CheckedByIDTextBox.Text = value; }
        }
        public List<BL.Models.Message> Messages
        {
            set
            {
                MessagesDataGridView.Rows.Clear();
                foreach (BL.Models.Message message in value)
                {
                    string[] row = new string[4];
                    row[0] = message.MessageID.ToString();
                    row[1] = message.SenderID.ToString();
                    if (message.SenderID != BL.Models.Message.MessageCheckedByNobody)
                    {
                        row[2] = message.CheckedByID.ToString();
                    }
                    else
                    {
                        row[2] = "-";
                    }
                    
                    row[3] = message.Text;



                    MessagesDataGridView.Rows.Add(row);
                }
            }
        }

        public MessageViewWinForm()
        {
            InitializeComponent();
        }

        public event AsyncEventHandler GetMessageClicked;
        public event AsyncEventHandler GetMessagesClicked;
        public event AsyncEventHandler MarkCheckedClicked;
        public event AsyncEventHandler SendClicked;
        public event AsyncEventHandler DeleteClicked;
        public event AsyncEventHandler UpdateClicked;
        public event EventHandler CloseClicked;

        private void GetMessagesButton_Click(object sender, EventArgs e)
        {
            GetMessagesClicked?.Invoke(this, new EventArgs());
        }

        private void MarkCheckedButton_Click(object sender, EventArgs e)
        {
            MarkCheckedClicked?.Invoke(this, new EventArgs());
        }

        private void GetMessageButton_Click(object sender, EventArgs e)
        {
            GetMessageClicked?.Invoke(this, new EventArgs());
        }
        private void SendButton_Click(object sender, EventArgs e)
        {
            SendClicked?.Invoke(this, new EventArgs());
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            UpdateClicked?.Invoke(this, new EventArgs());
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DeleteClicked?.Invoke(this, new EventArgs());
        }

        public void Open()
        {
            base.ShowDialog();
        }

        private void MessageViewWinForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseClicked?.Invoke(this, new EventArgs());
        }


    }
}
