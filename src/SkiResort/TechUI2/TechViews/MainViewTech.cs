using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.IViews;
using Microsoft.VisualStudio.Threading;

namespace TechUI.TechViews
{
    public class MainViewTech : IMainView
    {
        public bool MessageEnabled { get; set; }
        public bool UserEnabled { get; set; }
        public bool TurnstileEnabled { get; set; }
        public bool CardReadingEnabled { get; set; }
        public bool CardEnabled { get; set; }

        public event EventHandler ProfileClicked;
        public event EventHandler LiftClicked;
        public event EventHandler SlopeClicked;
        public event EventHandler MessageClicked;
        public event EventHandler UserClicked;
        public event EventHandler TurnstileClicked;
        public event EventHandler CardReadingClicked;
        public event EventHandler CardClicked;
        public event AsyncEventHandler CloseClicked;

        public void Close()
        {
            Console.WriteLine("Дотвидания");
        }

        public void Open()
        {
            Console.WriteLine("\n\nГлавное окно");

            Console.WriteLine("Доступные окна:\n" +
                "0 -- Выход\n" +
                "1 -- Профиль\n" +
                "2 -- Трассы\n" +
                "3 -- Подъемники");
            if (MessageEnabled)
                Console.WriteLine("4 -- Сообщения");
            if (UserEnabled)
                Console.WriteLine("5 ----- Пользователи");
            if (CardEnabled)
                Console.WriteLine("6 ----- Карты");
            if (TurnstileEnabled)
                Console.WriteLine("7 ----- Турникеты");
            if (CardReadingEnabled)
                Console.WriteLine("8 ----- Считывания\n");

            Console.WriteLine("Введите номер окна: ");
            string? commandString = Console.ReadLine();

            switch (commandString)
            {
                case "0": CloseClicked.Invoke(this, new EventArgs()); return;
                case "1": ProfileClicked.Invoke(this, new EventArgs()); break;
                case "2": SlopeClicked.Invoke(this, new EventArgs()); break;
                case "3": LiftClicked.Invoke(this, new EventArgs()); break;
                case "4": MessageClicked.Invoke(this, new EventArgs()); break;
                case "5": UserClicked.Invoke(this, new EventArgs()); break;
                case "6": CardClicked.Invoke(this, new EventArgs()); break;
                case "7": TurnstileClicked.Invoke(this, new EventArgs()); break;
                case "8": CardReadingClicked.Invoke(this, new EventArgs()); break;
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
