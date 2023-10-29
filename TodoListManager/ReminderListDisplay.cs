using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListManager
{
    class ReminderListDisplay
    {

        bool _madeError = false;
        string errorMessage = "(INVALID OPTION)";

        private void DisplayReminders()
        {
            int i = 1;
            foreach (Reminder r in Database.Instance.GetReminders())
            {
                Console.WriteLine(i + "." + r.ToString() + "\n");
                i++;
            }
        }

        private void PlanError(string message) {
            _madeError = true;
            errorMessage = message;
        }

        private void TryPrintingError() {
            if (_madeError) {
                Console.WriteLine(errorMessage);
                _madeError = false;
            }
        }

        public void Run()
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine("-----Reminder List-----\n");
                DisplayReminders();
                Console.WriteLine("\n-----------------------");
                Console.WriteLine("-e N = Edit the N'th item");
                Console.WriteLine("-d N = Delete the N'th item");
                Console.WriteLine("-p N = Preview the N'th item");
                Console.WriteLine("-b = Go back");

                TryPrintingError();

                Console.Write("Enter an option: ");

                string input = Console.ReadLine().Trim();

                string[] args = input.Split(' ');
                if (args.Length <= 0) {
                    continue;
                }

                List<Reminder> reminders = Database.Instance.GetReminders();

                if (args.Length == 1) {
                    switch (args[0]) {
                        case "-b":
                            return;
                        default:
                            PlanError("(INVALID OPTION)");
                            continue;
                    } 
                } else if (args.Length == 2) {
                    int value;
                    if (!int.TryParse(args[1], out value)) {
                        PlanError("(INVALID VALUE)");
                        continue;
                    }
                    switch (args[0]) {
                        case "-e":
                            value -= 1;
                            reminders = Database.Instance.GetReminders();
                            if (value > reminders.Count - 1 || value < 0) {
                                PlanError("(ITEM DOES NOT EXIST)");
                                continue;
                            }

                            Reminder toEdit = (Reminder)reminders[value].Clone();

                            ReminderEditor editor = new ReminderEditor(toEdit);//bring up the editing menu
                            editor.Run();
                            if (editor.GetResultType() == ReminderEditor.ResultType.Confirmed) {
                                Database.Instance.ReplaceAt(value, editor.GetResult());
                                Database.Instance.Save();
                            }

                            break;
                        case "-d":
                            value -= 1;
                            if (value > reminders.Count - 1 || value < 0) {
                                PlanError("(ITEM DOES NOT EXIST)");
                                continue;
                            }

                            if (Misc.AskConfirm()) {
                                Database.Instance.RemoveAt(value);
                            }
                            break;
                        case "-p":
                            value -= 1;
                            if (value > reminders.Count - 1 || value < 0) {
                                PlanError("(ITEM DOES NOT EXIST)");
                                continue;
                            }

                            Misc.PrintReminderPreview(reminders[value]);
                            Console.WriteLine("-------------------------------------");
                            Console.Write("Press enter to return\n");
                            Console.ReadLine();
                            break;
                        default:
                            PlanError("(INVALID OPTION)");
                            break;
                    }
                }
            }
        }

    }
}
