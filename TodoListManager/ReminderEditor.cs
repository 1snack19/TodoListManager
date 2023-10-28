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

        Reminder _reminder;

        public ReminderEditor()
        {
            _reminder = new Reminder();
        }

        public ReminderEditor(Reminder reminderInput)
        {
            _reminder = reminderInput;
        }

        private void PrintPreview()
        {
            Console.Clear();
            Console.WriteLine("----Preview----");
            Console.WriteLine("Title : " + _reminder.title);
            Console.WriteLine("Datetime : " + _reminder.datetime.ToString(DateTimeSelector.dateFormat + " " + DateTimeSelector.timeFormat));

            //Console.WriteLine("Note : " +  noteInput);
            if (!String.IsNullOrEmpty(_reminder.note))
            {
                string[] sep = _reminder.note.Split('\n');

                Console.WriteLine("Note : " + sep[0]);
                for (int i = 1; i < sep.Length; i++)
                {
                    //                    Note : 
                    Console.WriteLine("       " + sep[i]);
                }
            }
            else
            {
                Console.WriteLine("Note : ");
            }

            Console.WriteLine("-------------------------------------");
            Console.Write("Press any key to return");
            Console.Read();
            _madeError = false;
        }

        private bool AskConfirm()
        {
            Console.Clear();
            Console.Write("Are you sure? (Enter -y to confirm) : ");
            var confirm = Console.ReadLine();

            return confirm == "-y";
        }

        private bool ReminderEdit()
        {
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
            }
            Console.Write("Enter an option: ");


            string input = Console.ReadLine();

            switch (input)
            {
                case "-b":
                    
                    if (AskConfirm())
                    {
                        _resultType = ResultType.Canceled;
                        Console.WriteLine("\nDone! Press any key to continue.");
                        Console.ReadKey();
                        return false;
                    }
                    _madeError = false;
                    break;

                case "-n"://edit 
                    TextPrompt noteInputPrompt = new TextPrompt();
                    noteInputPrompt.Run();
                    _reminder.note = noteInputPrompt.GetResult();
                    _madeError = false;
                    break;

                case "-t"://edit title
                    TextPrompt titleInputPrompt = new TextPrompt(true);
                    titleInputPrompt.Run();
                    _reminder.title = titleInputPrompt.GetResult();
                    _madeError = false;
                    break;

                case "-d"://edit date
                    DateTimeSelector timeSelector = new DateTimeSelector();
                    timeSelector.Run();
                    if (!timeSelector.IsCanceled())
                        _reminder.datetime = timeSelector.GetResult();
                    _madeError = false;
                    break;

                case "-c"://Confirm creation
                    if (AskConfirm())
                    {
                        _resultType = ResultType.Confirmed;
                        Console.WriteLine("\nDone! Press any key to continue.");
                        Console.ReadKey();
                        return false;
                    }
                    _madeError = false;
                    break;

                case "-p":
                    PrintPreview();
                    _madeError = false;
                    break;

                default:
                    _madeError = true;
                    break;
            }

            return true;
        }

        public void Run()
        {
            while (ReminderEdit())
            {
                Console.Clear();
            }
        }

        public Reminder GetResult()
        {
            return _reminder;
        }
        
        public ResultType GetResultType()
        {
            return _resultType;
        }
    }
}
