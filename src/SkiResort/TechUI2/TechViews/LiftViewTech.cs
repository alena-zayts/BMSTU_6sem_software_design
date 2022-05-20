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
    public class LiftViewTech : ILiftView
    {
        public bool GetInfoEnabled { get; set; }
        public bool GetInfosEnabled { get; set; }
        public bool UpdateEnabled { get; set; }
        public bool AddEnabled { get; set; }
        public bool DeleteEnabled { get; set; }
        public bool AddConnectedSlopeEnabled { get; set; }
        public bool DeleteConnectedSlopeEnabled { get; set; }
        public string Name
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
        public string IsOpen
        {
            get
            {
                Console.WriteLine("Введите открытось: ");
                return Console.ReadLine();
            }
            set
            {
                Console.WriteLine($"открытость: {value}");
            }
        }
        public string SeatsAmount
        {
            get
            {
                Console.WriteLine("Введите количество мест: ");
                return Console.ReadLine();
            }
            set
            {
                Console.WriteLine($"количество мест: {value}");
            }
        }
        public string QueueTime
        {
            get
            {
                Console.WriteLine("время в очереди: ");
                return Console.ReadLine();
            }
            set
            {
                Console.WriteLine($"время в очереди: {value}");
            }
        }
        public string LiftingTime
        {
            get
            {
                Console.WriteLine("время подъема: ");
                return Console.ReadLine();
            }
            set
            {
                Console.WriteLine($"время подъема: {value}");
            }
        }
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
        public List<Lift> Lifts
        {
            set
            {
                foreach (var l in value)
                    Console.WriteLine($"{l}");
            }
        }

        public event AsyncEventHandler GetInfoClicked;
        public event AsyncEventHandler GetInfosClicked;
        public event AsyncEventHandler UpdateClicked;
        public event AsyncEventHandler AddClicked;
        public event AsyncEventHandler DeleteClicked;
        public event AsyncEventHandler AddConnectedSlopeClicked;
        public event AsyncEventHandler DeleteConnectedSlopeClicked;
        public event EventHandler CloseClicked;

        public void Close()
        {
            Console.WriteLine("Выход из окна Подъемники");
        }

        public void Open()
        {
            {
                Console.WriteLine("\n\nОкно Подъемники");
                Console.WriteLine("Доступные действия:\n" +
                    "0 -- Выход");
                if (GetInfoEnabled)
                    Console.WriteLine("1 -- Получить информацию об одном подъемнике");
                if (GetInfosEnabled)
                    Console.WriteLine("2 -- Получить информацию обо всех подъемниках");
                if (UpdateEnabled)
                    Console.WriteLine("3 -- Обновить подъемник");
                if (AddEnabled)
                    Console.WriteLine("4 -- Добавить подъемник");
                if (DeleteEnabled)
                    Console.WriteLine("5 -- Удалить подъемник");
                if (AddConnectedSlopeEnabled)
                    Console.WriteLine("6 -- Добавить связь с трассой");
                if (DeleteConnectedSlopeEnabled)
                    Console.WriteLine("7 -- Удалить связь с трассой");

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
                        if (AddConnectedSlopeEnabled)
                        {
                            AddConnectedSlopeClicked.Invoke(this, new EventArgs());
                        }
                        else
                        {
                            Console.WriteLine("Недостаточно прав");
                        }
                        break;
                    case "7":
                        if (DeleteConnectedSlopeEnabled)
                        {
                            DeleteConnectedSlopeClicked.Invoke(this, new EventArgs());
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
