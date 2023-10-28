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
        Exit,
    }

    class Reminder
    {
        public Reminder()
        {
            note = "Empty";
            title = "Untitled";
            datetime = DateTime.Now;
        }
        public string note;
        public string title;
        public DateTime datetime;
    }

    class UserInterface
    {

        Reminder _reminderInput;

        List<Reminder> _reminders;

        public UserInterface()
        {
            _reminders = new List<Reminder>();
            _reminderInput = new Reminder();
        }

        bool _madeError = false;

        ConsoleState _state = ConsoleState.Main;
        
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
        private void MainUI()
        {
            Console.WriteLine("----Main Menu----");
            Console.WriteLine("-c = Create a reminder");
            Console.WriteLine("-q = Quit the program");
            Console.Write("Enter an option: ");
            string input = Console.ReadLine();
            switch (input)
            {
                case "-c":
                    _state = ConsoleState.Creating;
                    break;
                case "-q":
                    _state = ConsoleState.Exit;
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
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
                    _state = ConsoleState.Main;
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

                case "-c"://Confirm creation
                    Console.Clear();
                    Console.Write("Are you sure? (Press y to confirm) : ");
                    var confirm = Console.ReadKey(true);
                    if (confirm.Key == ConsoleKey.Y)
                    {
                        _reminders.Add(_reminderInput);
                        _reminderInput = new Reminder();
                        Console.WriteLine("\nDone! Press any key to continue.");
                        Console.ReadKey();
                    }
                    _madeError = false;
                    break;

                case "-p":
                    PrintPreview();
                    _madeError = false;
                    break;

                default:
                    _madeError = true;
                    break;
            }
        }
        public void Run()
        {
            Console.WriteLine("-----Todo list manager-----");
            bool running = true;
            while (running)
            {

                Console.Clear();
                switch (_state)
                {
                    case ConsoleState.Main:
                        MainUI();
                        break;
                    case ConsoleState.Creating:
                        ReminderCreation();
                        break;
                    case ConsoleState.Exit:
                        Console.WriteLine("Exit!");
                        running = false;
                        break;
                }
            }

        }
    }

}
