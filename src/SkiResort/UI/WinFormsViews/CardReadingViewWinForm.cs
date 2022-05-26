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
    public partial class CardReadingViewWinForm : Form, ICardReadingView
    {
        public bool GetCardReadingEnabled
        {
            get { return GetCardReadingButton.Enabled; }
            set { GetCardReadingButton.Enabled = value; }
        }
        public bool GetCardReadingsEnabled
        {
            get { return GetCardReadingsButton.Enabled; }
            set { GetCardReadingsButton.Enabled = value; }
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
        public string RecordID
        {
            get { return RecordIDTextBox.Text; }
            set { RecordIDTextBox.Text = value; }
        }
        public string TurnstileID
        {
            get { return TurnstileIDTextBox.Text; }
            set { TurnstileIDTextBox.Text = value; }
        }
        public string CardID
        {
            get { return CardIDTextBox.Text; }
            set { CardIDTextBox.Text = value; }
        }
        public DateTimeOffset ReadingTime
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
        public List<BL.Models.CardReading> CardReadings
        {
            set
            {
                СardReadingsDataGridView.Rows.Clear();
                foreach (BL.Models.CardReading cardReading in value)
                {
                    string[] row = new string[4];
                    row[0] = cardReading.RecordID.ToString();
                    row[1] = cardReading.TurnstileID.ToString();
                    row[2] = cardReading.CardID.ToString();
                    row[3] = cardReading.ReadingTime.ToString();

                    СardReadingsDataGridView.Rows.Add(row);
                }
            }
        }

        public CardReadingViewWinForm()
        {
            InitializeComponent();
        }

        public event AsyncEventHandler GetCardReadingClicked;
        public event AsyncEventHandler GetCardReadingsClicked;
        public event AsyncEventHandler UpdateClicked;
        public event AsyncEventHandler AddClicked;
        public event AsyncEventHandler DeleteClicked;
        public event EventHandler CloseClicked;

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

        private void GetCardReadingButton_Click(object sender, EventArgs e)
        {
            GetCardReadingClicked?.Invoke(this, new EventArgs());
        }

        private void GetCardReadingsButton_Click(object sender, EventArgs e)
        {
            GetCardReadingsClicked?.Invoke(this, new EventArgs());
        }

        private void CardReadingViewWinForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseClicked?.Invoke(this, new EventArgs());
        }

        public void Open()
        {
            Show();
        }

    }
}
