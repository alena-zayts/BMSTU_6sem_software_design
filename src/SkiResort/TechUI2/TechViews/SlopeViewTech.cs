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
    public class SlopeViewTech : ISlopeView
    {
        public bool GetInfoEnabled { get; set; }
        public bool GetInfosEnabled { get; set; }
        public bool UpdateEnabled { get; set; }
        public bool AddEnabled { get; set; }
        public bool DeleteEnabled { get; set; }
        public bool AddConnectedLiftEnabled { get; set; }
        public bool DeleteConnectedLiftEnabled { get; set; }

        public string SlopeName
        {
            get
            {
                Console.WriteLine("Введите название трассы: ");
                return Console.ReadLine();
            }
            set
            {
                Console.WriteLine($"название трассы: {value}");
            }
        }
        public string IsOpen
        {
            get
            {
                Console.WriteLine("Введите открытость: ");
                return Console.ReadLine();
            }
            set
            {
                Console.WriteLine($"открытость: {value}");
            }
        }
        public string DifficultyLevel
        {
            get
            {
                Console.WriteLine("Введите уровень сложности: ");
                return Console.ReadLine();
            }
            set
            {
                Console.WriteLine($"уровень сложности: {value}");
            }
        }
        public string LiftName
        {
            get
            {
                Console.WriteLine("Введите название подъемника: ");
                return Console.ReadLine();
            }
            set
            {
                Console.WriteLine($"название подъемника: {value}");
            }
        }
        public List<Slope> Slopes
        {
            set
            {
                foreach (Slope s in value)
                {
                    Console.WriteLine($"{s}");
                }
            }
        }

        public event AsyncEventHandler GetInfoClicked;
        public event AsyncEventHandler GetInfosClicked;
        public event AsyncEventHandler UpdateClicked;
        public event AsyncEventHandler AddClicked;
        public event AsyncEventHandler DeleteClicked;
        public event AsyncEventHandler AddConnectedLiftClicked;
        public event AsyncEventHandler DeleteConnectedLiftClicked;
        public event EventHandler CloseClicked;

        public void Close()
        {
            Console.WriteLine("Выход из окна Трассы");
        }

        public void Open()
        {
            {
                Console.WriteLine("\n\nОкно Трассы");
                Console.WriteLine("Доступные действия:\n" +
                    "0 -- Выход");
                if (GetInfoEnabled)
                    Console.WriteLine("1 -- Получить информацию об одной трассе");
                if (GetInfosEnabled)
                    Console.WriteLine("2 -- Получить информацию обо всех трассах");
                if (UpdateEnabled)
                    Console.WriteLine("3 -- Обновить трассу");
                if (AddEnabled)
                    Console.WriteLine("4 -- Добавить трассу");
                if (DeleteEnabled)
                    Console.WriteLine("5 -- Удалить трассу");
                if (AddConnectedLiftEnabled)
                    Console.WriteLine("6 -- Добавить связь с подъемником");
                if (DeleteConnectedLiftEnabled)
                    Console.WriteLine("7 -- Удалить связь с подъемником");

                Console.WriteLine("Введите команду: ");
                string? commandString = Console.ReadLine();

                switch (commandString)
                {
                    case "0": CloseClicked.Invoke(this, new EventArgs()); return;
                    case "1":
                        if (GetInfoEnabled)
                        {
                            GetInfoClicked.Invoke(this, new EventArgs());
                        }
                        else
                        {
                            Console.WriteLine("Недостаточно прав");
                        }
                        break;
                    case "2":
                        if (GetInfosEnabled)
                        {
                            GetInfosClicked.Invoke(this, new EventArgs());
                        }
                        else
                        {
                            Console.WriteLine("Недостаточно прав");
                        }
                        break;
                    case "3":
                        if (UpdateEnabled)
                        {
                            UpdateClicked.Invoke(this, new EventArgs());
                        }
                        else
                        {
                            Console.WriteLine("Недостаточно прав");
                        }
                        break;
                    case "4":
                        if (AddEnabled)
                        {
                            AddClicked.Invoke(this, new EventArgs());
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
                        if (AddConnectedLiftEnabled)
                        {
                            AddConnectedLiftClicked.Invoke(this, new EventArgs());
                        }
                        else
                        {
                            Console.WriteLine("Недостаточно прав");
                        }
                        break;
                    case "7":
                        if (DeleteConnectedLiftEnabled)
                        {
                             DeleteConnectedLiftClicked.Invoke(this, new EventArgs());
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
