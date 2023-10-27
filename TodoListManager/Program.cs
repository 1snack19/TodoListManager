﻿using System;
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

        static ConsoleState state = ConsoleState.Main;
        public static void MainUI()
        {
            Console.WriteLine("----Main Menu----");
            //Console.WriteLine("-c = Create a reminder");
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
            //Console.WriteLine("-b = Back");
            //Console.WriteLine("-n = Note");
            Console.Write("Enter an option: ");
            string input = Console.ReadLine();
            switch (input)
            {
                case "-b":
                    state = ConsoleState.Main;
                    break;
                case "-n":
                    Console.Clear();
                    Console.WriteLine("----Enter a text----");
                    noteInput = ReadMultipleLines();
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
