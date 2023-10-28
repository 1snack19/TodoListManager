using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TodoListManager
{

    enum ConsoleState
    {
        Main,
        Creating,
        Exit,
    }

    class Reminder
    {
        public Reminder()
        {
            note = "Empty";
            title = "Untitled";
            datetime = DateTime.Now;
        }
        public string note;
        public string title;
        public DateTime datetime;
    }

    class UserInterface
    {

        List<Reminder> _reminders;

        public UserInterface()
        {
            _reminders = new List<Reminder>();
        }

        bool _madeError = false;

        ConsoleState _state = ConsoleState.Main;
        
        private void MainUI()
        {
            Console.WriteLine("----Main Menu----");
            Console.WriteLine("-c = Create a reminder");
            Console.WriteLine("-q = Quit the program");
            Console.Write("Enter an option: ");
            string input = Console.ReadLine();
            switch (input)
            {
                case "-c":
                    _state = ConsoleState.Creating;
                    break;
                case "-q":
                    _state = ConsoleState.Exit;
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
        public void Run()
        {
            Console.WriteLine("-----Todo list manager-----");
            bool running = true;
            while (running)
            {
                Console.Clear();
                switch (_state)
                {
                    case ConsoleState.Main:
                        MainUI();
                        break;
                    case ConsoleState.Creating:
                        ReminderEditor editor = new ReminderEditor();
                        editor.Run();
                        if (editor.GetResultType() == ReminderEditor.ResultType.Confirmed)
                        {
                            _reminders.Add(editor.GetResult());
                        }
                        break;
                    case ConsoleState.Exit:
                        Console.WriteLine("Exit!");
                        running = false;
                        break;
                }
            }

        }
    }

}
