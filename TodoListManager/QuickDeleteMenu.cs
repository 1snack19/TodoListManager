using System;

namespace TodoListManager
{
    class QuickDeleteMenu : InteractiveMenu
    {
        bool _listEmpty = false;

        protected override void PrintMenu()
        {
            var rlist = Database.Instance.GetReminders();
            if (rlist.Count <= 0) {
                Console.WriteLine("Your list is empty! There's nothing to delete!");
                Console.Write("Enter anything to exit");
                _askForInputString = "";
                _listEmpty = true;
                return;
            }

            Console.WriteLine("* Choose an item to delete(Enter q to exit)\n");
            int i = 1;
            foreach (var r in rlist)
            {
                Console.Write(i + ". " + r.title + " ");
                if (r.due)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("[DUE]");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor= ConsoleColor.White;
                }
                Console.WriteLine("\n");
                i++;
            }

        }

        protected override void ProcessInput(string input)
        {
            if (_listEmpty || input.Equals("q")) {
                PlanExit();
                return;
            }

            var rlist = Database.Instance.GetReminders();

            int value;
            if (!int.TryParse(input, out value))
            {
                PlanError("(INVALID VALUE)");
                return;
            }

            value--;
            if (value > rlist.Count - 1 || value < 0)
            {
                PlanError("(ITEM DOES NOT EXIST)");
                return;
            }

            
            Database.Instance.RemoveAt(value);
            Database.Instance.Save();
        }
    }
}
