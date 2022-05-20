using Microsoft.VisualStudio.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.IViews;

namespace UI.TechViews
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
            throw new NotImplementedException();
        }

        public void Open()
        {
            Console.WriteLine("\n\nAvailable actions:\n" +
                "0 -- Exit\n" +
                "1 -- Profile\n" +
                "2 -- Slopes\n" +
                "3 -- Lifts\n");
            if (MessageEnabled)
                Console.WriteLine("4 -- Messages\n");
            if (UserEnabled)
                Console.WriteLine("5 -- Users\n");
            if (CardEnabled)
                Console.WriteLine("6 -- Cards\n");
            if (TurnstileEnabled)
                Console.WriteLine("7 -- Turnstiles\n");
            if (CardReadingEnabled)
                Console.WriteLine("8 -- CardReadings\n\n\n");
            string? commandString = Console.ReadLine();
            try
            {
                int command = Int32.Parse(commandString);
                switch (command)
                {
                    case 0: CloseClicked.Invoke(this, new EventArgs()); break;
                    case 1: ProfileClicked.Invoke(this, new EventArgs()); break;
                    case 2: SlopeClicked.Invoke(this, new EventArgs()); break;
                    case 3: LiftClicked.Invoke(this, new EventArgs()); break;
                    case 4: MessageClicked.Invoke(this, new EventArgs()); break;
                    case 5: UserClicked.Invoke(this, new EventArgs()); break;
                    case 6: CardClicked.Invoke(this, new EventArgs()); break;
                    case 7: TurnstileClicked.Invoke(this, new EventArgs()); break;
                    case 8: CardReadingClicked.Invoke(this, new EventArgs()); break;
                    default:
                        Console.WriteLine("Недопустимая команда");
                        Open();
                        break;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Недопустимая команда");
            }
        }

        public void Refresh()
        {
            Open();
        }
    }
}
