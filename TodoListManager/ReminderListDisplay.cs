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

        private void DisplayReminders()
        {
            int i = 1;
            foreach (Reminder r in Database.Instance.GetReminders())
            {
                Console.WriteLine(i + "." + r.ToString());
                i++;
            }
        }

        public void Run()
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine("-----Reminder List-----");
                Console.WriteLine();
                DisplayReminders();
                Console.WriteLine();
                Console.WriteLine("-----------------------");
                Console.WriteLine("-e N = Edit the N'th item");
                Console.WriteLine("-d N = Delete the N'th item");
                Console.WriteLine("-b = Go back");
                if (_madeError ) {
                    Console.WriteLine("(INVALID OPTION)");
                    _madeError = false;
                }
                Console.Write("Enter an option: ");

                string input = Console.ReadLine().Trim();
                if (input.Length <= 0)
                {
                    _madeError = true;
                    continue;
                }

                string[] args = input.Split(' ');

                switch (input)
                {
                    case "-b":
                        return;
                    case "-e":

                    default:
                        _madeError = true;
                        break;
                }
            }
        }

    }
}
