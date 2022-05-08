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
    public partial class CardViewWinForm : Form, ICardView
    {
        public bool GetCardEnabled
        {
            get { return GetCardButton.Enabled; }
            set { GetCardButton.Enabled = value; }
        }
        public bool GetCardsEnabled
        {
            get { return GetCardsButton.Enabled; }
            set { GetCardsButton.Enabled = value; }
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
        public string CardID
        {
            get { return CardIDTextBox.Text; }
            set { CardIDTextBox.Text = value; }
        }
        string ICardView.Type
        {
            get { return TypeTextBox.Text; }
            set { TypeTextBox.Text = value; }
        }
        public DateTimeOffset ActivationTime
        {
            get
            {
                DateTimeOffset date = DatePicker.Value;
                DateTimeOffset time = TimePicker.Value;
                DateTimeOffset dateTime = new(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second, new TimeSpan());
                return dateTime;
            }
            set
            {
                DatePicker.Value = value.DateTime;
                TimePicker.Value = value.DateTime;
            }
        }
        public List<Card> Cards
        {
            set
            {
                СardsDataGridView.Rows.Clear();
                foreach (Card card in value)    
                {
                    string[] row = new string[3];
                    row[0] = card.CardID.ToString();
                    row[1] = card.Type.ToString();
                    row[2] = card.ActivationTime.ToString();

                    СardsDataGridView.Rows.Add(row);
                }
            }
        }

        public CardViewWinForm()
        {
            InitializeComponent();
        }

        public event AsyncEventHandler GetCardClicked;
        public event AsyncEventHandler GetCardsClicked;
        public event AsyncEventHandler UpdateClicked;
        public event AsyncEventHandler AddClicked;
        public event AsyncEventHandler DeleteClicked;
        public event EventHandler CloseClicked;

        private void GetCardButton_Click(object sender, EventArgs e)
        {
            GetCardClicked?.Invoke(this, new EventArgs());
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

        private void GetCardsButton_Click(object sender, EventArgs e)
        {
            GetCardsClicked?.Invoke(this, new EventArgs());
        }

        private void CardViewWinForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseClicked?.Invoke(this, new EventArgs());
        }

        public void Open()
        {
            Show();
        }
    }
}
