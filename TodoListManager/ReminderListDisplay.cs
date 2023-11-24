using System;
using System.Collections.Generic;

namespace TodoListManager
{
    class ReminderListDisplay : InteractiveMenu
    {

        private void DisplayReminders()
        {
            var rlist = Database.Instance.GetReminders();
            if (rlist.Count < 1)
            {
                Console.WriteLine("         Empty         ");
                return;
            }
            
            int i = 1;
            foreach (Reminder r in rlist)
            {
                Console.WriteLine(i + "." + r.ToString() + "\n");
                i++;
            }
        }

        protected override void PrintMenu() {
            Console.WriteLine("-----Reminder List-----\n");
            DisplayReminders();
            Console.WriteLine("\n-----------------------");
            Console.WriteLine("e N = Edit the N'th item");
            Console.WriteLine("d N = Delete the N'th item");
            Console.WriteLine("p N = Preview the N'th item");
            Console.WriteLine("b = Go back");
        }

        protected override void ProcessInput(string input) {
            string[] args = input.Split(' ');
            if (args.Length <= 0) {
                return;
            }

            List<Reminder> reminders = Database.Instance.GetReminders();

            if (args.Length == 1) {
                switch (args[0]) {
                    case "b":
                        PlanExit();
                        return;
                    default:
                        PlanError("(INVALID OPTION)");
                        return;
                }
            } else if (args.Length == 2) {
                int value;
                if (!int.TryParse(args[1], out value)) {
                    PlanError("(INVALID VALUE)");
                    return;
                }
                switch (args[0]) {
                    case "e":
                        value -= 1;
                        reminders = Database.Instance.GetReminders();
                        if (value > reminders.Count - 1 || value < 0) {
                            PlanError("(ITEM DOES NOT EXIST)");
                            return;
                        }

                        Reminder toEdit = (Reminder)reminders[value].Clone();

                        ReminderEditor editor = new ReminderEditor(toEdit);//bring up the editing menu
                        editor.Run();
                        if (editor.GetResultType() == ReminderEditor.ResultType.Confirmed) {
                            Database.Instance.ReplaceAt(value, editor.GetResult());
                            Database.Instance.Save();
                        }

                        break;
                    case "d":
                        value -= 1;
                        if (value > reminders.Count - 1 || value < 0) {
                            PlanError("(ITEM DOES NOT EXIST)");
                            return;
                        }

                        if (Misc.AskConfirm()) {
                            Database.Instance.RemoveAt(value);
                        }
                        break;
                    case "p":
                        value -= 1;
                        if (value > reminders.Count - 1 || value < 0) {
                            PlanError("(ITEM DOES NOT EXIST)");
                            return;
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
