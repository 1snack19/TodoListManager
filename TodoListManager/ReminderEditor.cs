using System;

namespace TodoListManager
{
    class ReminderEditor : InteractiveMenu
    {
        public enum ResultType
        {
            Confirmed,
            Canceled,
        };

        bool _changed = false;

        ResultType _resultType = ResultType.Canceled;


        Reminder _editingReminder;

        public ReminderEditor()
        {
            _editingReminder = new Reminder();
        }

        public ReminderEditor(Reminder reminderInput)
        {
            _editingReminder = reminderInput;
            _editingReminder.due = false;
        }

        public Reminder GetResult()
        {
            return _editingReminder;
        }
        
        public ResultType GetResultType()
        {
            return _resultType;
        }

        protected override void PrintMenu() {
            Misc.HeaderPrint("Reminder Editor Menu");
            Console.WriteLine("t = Set title");
            Console.WriteLine("n = Set note");
            Console.WriteLine("d = Set due date");
            Console.WriteLine("p = Preview");
            Console.WriteLine("c = Confirm editing");
            Console.WriteLine("b = Cancel editing");
        }

        protected override void ProcessInput(string input) {
            switch (input) {
                case "b":
                    if (!_changed) {//Skip confirmation
                        PlanExit();
                        return;
                    }

                    if (Misc.AskConfirm()) {
                        _resultType = ResultType.Canceled;
                        Console.WriteLine("\nDone! Press any key to continue.");
                        Console.ReadKey();
                        PlanExit();
                        return;
                    }
                    break;
                case "n":
                    TextPrompt noteInputPrompt = new TextPrompt();
                    noteInputPrompt.Run();
                    if (noteInputPrompt.GetResultType() == TextPrompt.ResultType.Confirmed) {
                        _editingReminder.note = noteInputPrompt.GetResult();
                        _changed = true;
                    }
                    break;

                case "t":
                    TextPrompt titleInputPrompt = new TextPrompt(true);
                    titleInputPrompt.Run();
                    if (titleInputPrompt.GetResultType() == TextPrompt.ResultType.Confirmed) {
                        _editingReminder.title = titleInputPrompt.GetResult();
                        _changed = true;
                    }
                    break;

                case "d":
                    DateTimeSelector timeSelector = new DateTimeSelector();
                    timeSelector.Run();
                    if (!timeSelector.IsCanceled()) {
                        _editingReminder.datetime = timeSelector.GetResult();
                        _changed = true;
                    }
                    break;

                case "c":
                    if (Misc.AskConfirm()) {
                        _resultType = ResultType.Confirmed;
                        Console.WriteLine("\nDone! Press any key to continue.");
                        Console.ReadKey();
                        PlanExit();
                        return;
                    }
                    break;

                case "p":
                    Misc.PrintReminderPreview(_editingReminder);
                    Console.WriteLine("-------------------------------------");
                    Console.Write("Press enter to return\n");
                    Console.ReadLine();
                    break;
                default:
                    PlanError("(INVALID INPUT)");
                    break;
            }
        }
    }
}
