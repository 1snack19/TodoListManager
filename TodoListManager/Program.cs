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

        static string noteInput;
        static string titleInput;

        static ConsoleState state = ConsoleState.Main;
        public static void MainUI()
        {
            Console.WriteLine("----Main Menu----");
            Console.WriteLine("-c = Create a reminder");
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

        public static string ReadMultipleLines() {
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
            Console.WriteLine("-b = Back");
            Console.WriteLine("-n = Note");
            Console.Write("Enter an option: ");
            string input = Console.ReadLine();
            switch (input)
            {
                case "-b":
                    state = ConsoleState.Main;
                    break;

                case "-n"://edit 
                    Console.Clear();
                    Console.WriteLine("----Enter a text----");
                    noteInput = ReadMultipleLines();
                    break;

                case "-t"://edit title
                    Console.Clear();
                    Console.WriteLine("---Enter a title(Single line only)---");
                    titleInput = Console.ReadLine();
                    break;

                case "-d"://edit date
                    //Get Date input here
                    break;

                case "-p"://preview
                    Console.Clear();
                    Console.WriteLine("----Preview----");
                    Console.WriteLine("Title : " + titleInput);

                    
                    if (!String.IsNullOrEmpty(noteInput))
                    {
                        string[] sep = noteInput.Split('\n');
                        Console.WriteLine("Note : " + sep[0]);
                        for (int i = 0; i < sep.Length; i++)
                        {//                    Note : 
                            Console.WriteLine("       " + sep[i]);
                        }
                    } else {
                        Console.WriteLine("Note : ");
                    }
                    
                    Console.Write("\n\n\n\nPress any key to return");
                    Console.Read();
                    break;

                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("---Todo list manager---");
            bool running = true;
            while (running)
            {
                switch (state)
                {
                    case ConsoleState.Main:
                        MainUI();
                        break;
                    case ConsoleState.Creating:
                        ReminderCreation();
                        break;
                }
                Console.Clear();
            }

        }
    }
}

//Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);
//Console.CursorVisible = false;
//Console.Write('*');

//var random = new Random();

//while (true)
//{
//    if (Console.KeyAvailable)
//    {
//        var key = Console.ReadKey(true);

//        switch (key.Key)
//        {
//            case ConsoleKey.UpArrow:
//                if (Console.CursorTop > 0)
//                {
//                    Console.SetCursorPosition(Console.CursorLeft - 1,
//                        Console.CursorTop - 1);
//                    Console.Write('*');
//                }
//                break;
//            case ConsoleKey.DownArrow:
//                if (Console.CursorTop < Console.BufferHeight)
//                {
//                    Console.SetCursorPosition(Console.CursorLeft - 1,
//                        Console.CursorTop + 1);
//                    Console.Write('*');
//                }
//                break;
//            case ConsoleKey.LeftArrow:
//                if (Console.CursorLeft > 1)
//                {
//                    Console.SetCursorPosition(Console.CursorLeft - 2,
//                        Console.CursorTop);
//                    Console.Write('*');
//                }
//                break;
//            case ConsoleKey.RightArrow:
//                if (Console.CursorLeft < Console.WindowWidth - 1)
//                {
//                    Console.Write('*');
//                }
//                break;
//        }
//    }

//    // This method should be called on every iteration, 
//    // and the iterations should not wait for a key to be pressed
//    // Instead of Frame.Update(), change the foreground color every three seconds  
//    if (DateTime.Now.Second % 3 == 0)
//        Console.ForegroundColor = (ConsoleColor)random.Next(0, 16);
//}