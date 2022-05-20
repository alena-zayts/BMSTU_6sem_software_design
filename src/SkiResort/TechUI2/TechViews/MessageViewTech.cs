using BL.Models;
using Microsoft.VisualStudio.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.IViews;

namespace TechUI.TechViews
{
    public class MessageViewTech : IMessageView
    {
        public bool GetMessageEnabled { get; set; }
        public bool GetMessagesEnabled { get; set; }
        public bool MarkCheckedEnabled { get; set; }
        public bool SendEnabled { get; set; }
        public bool UpdateEnabled { get; set; }
        public bool DeleteEnabled { get; set; }
        public string MessageID
        {
            get
            {
                Console.WriteLine("Введите ID сообщения: ");
                return Console.ReadLine();
            }
            set
            {
                Console.WriteLine($"ID сообщения: {value}");
            }
        }
        public string MessageText
        {
            get
            {
                Console.WriteLine("Введите текст сообщения: ");
                return Console.ReadLine();
            }
            set
            {
                Console.WriteLine($"текст сообщения: {value}");
            }
        }
        public string SenderID
        {
            get
            {
                Console.WriteLine("Введите ID отправителя: ");
                return Console.ReadLine();
            }
            set
            {
                Console.WriteLine($"ID отправителя: {value}");
            } 
        }
        public string CheckedByID
        {
            get
            {
                Console.WriteLine("Введите ID проверившего: ");
                return Console.ReadLine();
            }
            set
            {
                Console.WriteLine($"ID проверившего: {value}");
            }
        }
        public List<Message> Messages
        {
            set
            {
                foreach (var message in value)
                    Console.WriteLine($"{message}");
            }
        }

        public event AsyncEventHandler GetMessageClicked;
        public event AsyncEventHandler GetMessagesClicked;
        public event AsyncEventHandler MarkCheckedClicked;
        public event AsyncEventHandler SendClicked;
        public event AsyncEventHandler DeleteClicked;
        public event AsyncEventHandler UpdateClicked;
        public event EventHandler CloseClicked;

        public void Close()
        {
            Console.WriteLine("Выход из окна Сообщения");
        }

        public void Open()
        {
            {
                Console.WriteLine("\n\nОкно Сообщения");
                Console.WriteLine("Доступные действия:\n" +
                    "0 -- Выход");
                if (GetMessageEnabled)
                    Console.WriteLine("1 -- Получить информацию об одном сообщении");
                if (GetMessagesEnabled)
                    Console.WriteLine("2 -- Получить информацию обо всех сообщениях");
                if (MarkCheckedEnabled)
                    Console.WriteLine("3 -- Отметить сообщение прочитанным");
                if (SendEnabled)
                    Console.WriteLine("4 -- Отправить сообщение");
                if (DeleteEnabled)
                    Console.WriteLine("5 -- Удалить сообщение");
                if (UpdateEnabled)
                    Console.WriteLine("6 -- Обновить сообщение");

                Console.WriteLine("Введите команду: ");
                string? commandString = Console.ReadLine();

                switch (commandString)
                {
                    case "0": CloseClicked.Invoke(this, new EventArgs()); return;
                    case "1":
                        if (GetMessageEnabled)
                        {
                            GetMessageClicked.Invoke(this, new EventArgs());
                        }
                        else
                        {
                            Console.WriteLine("Недостаточно прав");
                        }
                        break;
                    case "2":
                        if (GetMessagesEnabled)
                        {
                            GetMessagesClicked.Invoke(this, new EventArgs());
                        }
                        else
                        {
                            Console.WriteLine("Недостаточно прав");
                        }
                        break;
                    case "3":
                        if (MarkCheckedEnabled)
                        {
                            MarkCheckedClicked.Invoke(this, new EventArgs());
                        }
                        else
                        {
                            Console.WriteLine("Недостаточно прав");
                        }
                        break;
                    case "4":
                        if (SendEnabled)
                        {
                            SendClicked.Invoke(this, new EventArgs());
                        }
                        else
                        {
                            Console.WriteLine("Недостаточно прав");
                        }
                        break;
                    case "5":
                        if (DeleteEnabled)
                        {
                            DeleteClicked.Invoke(this, new EventArgs());
                        }
                        else
                        {
                            Console.WriteLine("Недостаточно прав");
                        }
                        break;
                    case "6":
                        if (UpdateEnabled)
                        {
                            UpdateClicked.Invoke(this, new EventArgs());
                        }
                        else
                        {
                            Console.WriteLine("Недостаточно прав");
                        }
                        break;
                    default:
                        Console.WriteLine("Неизвестная команда");
                        break;
                }
                Open();
            }
        }

        public void Refresh()
        {
            return;
        }
    }
}
