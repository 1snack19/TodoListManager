using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TodoListManager
{
    
    enum ConsoleState
    {
        Main,
        Creating,
    }


    internal class Program
    {

        static ConsoleState state = ConsoleState.Main;
        public static void MainUI()
        {
            Console.WriteLine("----Main Menu----");
            //commm
            Console.Write("Enter an option: ");
            string input = Console.ReadLine();
            switch (input)
            {
                case "-c":
                    state = ConsoleState.Creating;
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }

        public static string GetTextInput() {
            string res = "";
            string line;
            while (!String.IsNullOrWhiteSpace(line = Console.ReadLine()))
            {
                res += line + "\n";
            }

            return res;
        }

        public static void ReminderCreation() {
            Console.WriteLine("----Reminder Creation Menu----");
            Console.Write("Enter an option: ");
            string input = Console.ReadLine();
            switch (input)
            {
                case "-b":
                    state = ConsoleState.Main;
                    break;
                case "-n":
                    
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }

        public static void Main(string[] args)
        {
            //Console.WriteLine("---Todo list manager---");
            //bool running = true;
            //while (running) {
            //    switch (state)
            //    {
            //        case ConsoleState.Main:
            //            MainUI();
            //            break;
            //        case ConsoleState.Creating:
            //            ReminderCreation();
            //            break;
            //    }
            //    Console.Clear();
            //}
            Console.WriteLine("Enter a text: ");
            Console.WriteLine(GetTextInput());
        }
    }
}
