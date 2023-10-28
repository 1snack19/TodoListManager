using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListManager
{
    class ReminderEditor
    {

        bool _madeError = false;

        Reminder _reminderInput;

        public ReminderEditor()
        {
            _reminderInput = new Reminder();
        }

        public ReminderEditor(Reminder reminderInput)
        {
            _reminderInput = reminderInput;
        }

        private void PrintPreview()
        {
            Console.Clear();
            Console.WriteLine("----Preview----");
            Console.WriteLine("Title : " + _reminderInput.title);
            Console.WriteLine("Datetime : " + _reminderInput.datetime.ToString(DateTimeSelector.dateFormat + " " + DateTimeSelector.timeFormat));

            //Console.WriteLine("Note : " +  noteInput);
            if (!String.IsNullOrEmpty(_reminderInput.note))
            {
                string[] sep = _reminderInput.note.Split('\n');

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

        private void ReminderCreation()
        {
            Console.WriteLine("-----Reminder Creation Menu-----");
            Console.WriteLine("-t = Title of the List");
            Console.WriteLine("-n = Note the List");
            Console.WriteLine("-d = Date");
            Console.WriteLine("-p = Preview");
            Console.WriteLine("-c = Confirm Creation");
            Console.WriteLine("-b = Back");
            if (_madeError)
            {
                Console.WriteLine("(INVALID OPTION)");
            }
            Console.Write("Enter an option: ");


            string input = Console.ReadLine();
            switch (input)
            {
                case "-b":
                    _madeError = false;
                    break;

                case "-n"://edit 
                    TextPrompt noteInputPrompt = new TextPrompt();
                    noteInputPrompt.Run();
                    _reminderInput.note = noteInputPrompt.GetResult();
                    _madeError = false;
                    break;

                case "-t"://edit title
                    TextPrompt titleInputPrompt = new TextPrompt(true);
                    titleInputPrompt.Run();
                    _reminderInput.title = titleInputPrompt.GetResult();
                    _madeError = false;
                    break;

                case "-d"://edit date
                    DateTimeSelector timeSelector = new DateTimeSelector();
                    timeSelector.Run();
                    if (!timeSelector.IsCanceled())
                        _reminderInput.datetime = timeSelector.GetResult();
                    _madeError = false;
                    break;

                //case "-c"://Confirm creation
                //    Console.Clear();
                //    Console.Write("Are you sure? (Press y to confirm) : ");
                //    var confirm = Console.ReadKey(true);
                //    if (confirm.Key == ConsoleKey.Y)
                //    {
                //        _reminders.Add(_reminderInput);
                //        _reminderInput = new Reminder();
                //        Console.WriteLine("\nDone! Press any key to continue.");
                //        Console.ReadKey();
                //    }
                //    _madeError = false;
                //    break;

                case "-p":
                    PrintPreview();
                    _madeError = false;
                    break;

                default:
                    _madeError = true;
                    break;
            }
        }
    }
}
