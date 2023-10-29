using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TodoListManager
{


    class UserInterface
    {

        List<Reminder> _reminders;

        public UserInterface()
        {
            _reminders = new List<Reminder>();
        }

        bool _madeError = false;

        public void Run()
        { 
            while (true)
            {
                Console.Clear();
                Console.WriteLine("-----Todo list manager-----");
                Console.WriteLine("----Main Menu----");
                Console.WriteLine("-c = Create a reminder");
                Console.WriteLine("-l = Show reminder list");
                Console.WriteLine("-q = Quit the program");

                if (_madeError )
                {
                    Console.WriteLine("(INVALID OPTION)");
                    _madeError = false;
                }

                Console.Write("Enter an option: ");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "-c":
                        ReminderEditor editor = new ReminderEditor();
                        editor.Run();
                        if (editor.GetResultType() == ReminderEditor.ResultType.Confirmed)
                        {
                            Database.Instance.Add(editor.GetResult());
                            Console.WriteLine("Confirmed");
                        }
                        break;
                    case "-q":
                        Console.WriteLine("Exit!");
                        Console.ReadKey();
                        Environment.Exit(0);
                        break;
                    case "-l":
                        ReminderListDisplay displayer = new ReminderListDisplay();
                        displayer.Run();
                        break;
                    default:
                        _madeError = true;
                        break;
                }
            }

        }
    }

}
