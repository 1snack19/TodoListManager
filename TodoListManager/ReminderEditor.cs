using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace TodoListManager
{
    

    class ReminderEditor
    {
        public enum ResultType
        {
            Confirmed,
            Canceled,
        };

        ResultType _resultType = ResultType.Canceled;

        bool _madeError = false;

        Reminder _editingReminder;

        public ReminderEditor()
        {
            _editingReminder = new Reminder();
        }

        public ReminderEditor(Reminder reminderInput)
        {
            _editingReminder = reminderInput;
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("-----Reminder Editor Menu-----");
                Console.WriteLine("-t = Title of the List");
                Console.WriteLine("-n = Note the List");
                Console.WriteLine("-d = Date");
                Console.WriteLine("-p = Preview");
                Console.WriteLine("-c = Confirm Editing");
                Console.WriteLine("-b = Cancel Editing");
                if (_madeError)
                {
                    Console.WriteLine("(INVALID OPTION)");
                    _madeError = false;
                }
                Console.Write("Enter an option: ");


                string input = Console.ReadLine();

                switch (input)
                {
                    case "-b":
                        if (Misc.AskConfirm())
                        {
                            _resultType = ResultType.Canceled;
                            Console.WriteLine("\nDone! Press any key to continue.");
                            Console.ReadKey();
                            return;
                        }
                        break;

                    case "-n":
                        TextPrompt noteInputPrompt = new TextPrompt();
                        noteInputPrompt.Run();
                        _editingReminder.note = noteInputPrompt.GetResult();
                        break;

                    case "-t":
                        TextPrompt titleInputPrompt = new TextPrompt(true);
                        titleInputPrompt.Run();
                        _editingReminder.title = titleInputPrompt.GetResult();
                        break;

                    case "-d":
                        DateTimeSelector timeSelector = new DateTimeSelector();
                        timeSelector.Run();
                        if (!timeSelector.IsCanceled())
                            _editingReminder.datetime = timeSelector.GetResult();
                        break;

                    case "-c":
                        if (Misc.AskConfirm())
                        {
                            _resultType = ResultType.Confirmed;
                            Console.WriteLine("\nDone! Press any key to continue.");
                            Console.ReadKey();
                            return;
                        }
                        break;

                    case "-p":
                        Misc.PrintReminderPreview(_editingReminder);
                        Console.WriteLine("-------------------------------------");
                        Console.Write("Press enter to return\n");
                        Console.ReadLine();
                        break;
                    default:
                        _madeError = true;
                        break;
                }
            }
        }

        public Reminder GetResult()
        {
            return _editingReminder;
        }
        
        public ResultType GetResultType()
        {
            return _resultType;
        }
    }
}
