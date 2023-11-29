using System;

namespace TodoListManager
{
    class ReminderManager : InteractiveMenu
    {

        protected override void PrintMenu() {
            Misc.HeaderPrint("Reminder manager");
            Console.WriteLine("c = Create a reminder");
            Console.WriteLine("l = Show reminder list");
            Console.WriteLine("e = Exit the menu");
        }

        protected override void ProcessInput(string input) {
            switch (input) {
                case "c":
                    ReminderEditor editor = new ReminderEditor();
                    editor.Run();
                    if (editor.GetResultType() == ReminderEditor.ResultType.Confirmed) {
                        Database.Instance.Add(editor.GetResult());
                        Database.Instance.Save();
                    }
                    break;
                case "e":
                    PlanExit();
                    break;
                case "l":
                    ReminderListDisplay displayer = new ReminderListDisplay();
                    displayer.Run();
                    break;
                default:
                    PlanError("(INVALID OPTION)");
                    break;
            }
        }
    }

}
