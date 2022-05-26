using Microsoft.VisualStudio.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.IViews;

namespace TechUI.TechViews
{
    public class ProfileViewTech : IProfileView
    {
        public bool LogInEnabled { get; set; }
        public bool LogOutEnabled { get; set; }
        public bool RegisterEnabled { get; set; }
        public string Email 
        {
            get
            {
                Console.WriteLine("Введите email: ");
                return Console.ReadLine();
            }
            set
            {
                Console.WriteLine($"email: {value}");
            }
        }
        public string Password
        {
            get
            {
                Console.WriteLine("Введите пароль: ");
                return Console.ReadLine();
            }
            set
            {
                Console.WriteLine($"пароль: {value}");
            }
        }
        public string cardID
        {
            get
            {
                Console.WriteLine("Введите номер карты: ");
                return Console.ReadLine();
            }
            set
            {
                Console.WriteLine($"номер карты: {value}");
            }
        }

        public event AsyncEventHandler LogInClicked;
        public event AsyncEventHandler LogOutClicked;
        public event AsyncEventHandler RegisterClicked;
        public event EventHandler CloseClicked;

        public void Close()
        {
            Console.WriteLine("Выход из окна Профиль");
        }

        public void Open()
        {
            Console.WriteLine("\n\nОкно Профиль");
            Console.WriteLine("Доступные действия:\n" +
                "0 -- Выход из окна))");
            if (LogInEnabled)
                Console.WriteLine("1 -- Вход");
            if (LogOutEnabled)
                Console.WriteLine("2 -- Выход из профиля");
            if (RegisterEnabled)
                Console.WriteLine("3 -- Регистрация");

            Console.WriteLine("Введите команду: ");
            string? commandString = Console.ReadLine();

            switch (commandString)
            {
                case "0": 
                    CloseClicked.Invoke(this, new EventArgs());
                    return;
                case "1":
                    if (LogInEnabled)
                    {
                        LogInClicked.Invoke(this, new EventArgs()); 
                    }
                    else
                    {
                        Console.WriteLine("Недостаточно прав");
                    }
                    break;
                case "2":
                    if (LogOutEnabled)
                    {
                        LogOutClicked.Invoke(this, new EventArgs());
                    }
                    else
                    {
                        Console.WriteLine("Недостаточно прав");
                    }
                    break;
                case "3":
                    if (RegisterEnabled)
                    {
                        RegisterClicked.Invoke(this, new EventArgs());
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

        public void Refresh()
        {
            return;
        }
    }
}
