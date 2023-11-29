using System;
using System.Threading;

namespace TodoListManager {
    
    class DashBoard : InteractiveMenu {

        EventWaitHandle _displayUpdateFence;

        public DashBoard() {
            _displayUpdateFence = new EventWaitHandle(false, EventResetMode.AutoReset);
        }

        public override void Run() {

            while (true) {
                Console.CursorVisible = false;
                Thread updateThread = new Thread(UpdateLoop);
                Thread displayThread = new Thread(DisplayLoop);

                displayThread.Start();
                updateThread.Start();

                bool run = true;

                InteractiveMenu nextMenu = null;

                void _PlanNextMenu(InteractiveMenu menu = null)
                {
                    run = false;
                    nextMenu = menu;
                }
                
                while (run) {//Wait for a keypress
                    if (Console.KeyAvailable) {
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        switch (key.Key)
                        {
                            case ConsoleKey.S:
                                _PlanNextMenu(new ReminderManager());
                                break;
                            case ConsoleKey.Q:
                                _PlanNextMenu();
                                break;
                            case ConsoleKey.D:
                                _PlanNextMenu(new QuickDeleteMenu());
                                break;
                        }
                    }
                }

                displayThread.Abort();
                updateThread.Abort();
                Console.Clear();
                Console.CursorVisible = true;

                if (nextMenu != null) 
                    nextMenu.Run();
                else
                    break;
                
            }
        }

        private void UpdateLoop() {
            while (true) {

                bool changed = false;
                foreach (Reminder r in Database.Instance.GetReminders()) {
                    if (r.datetime < DateTime.Now && !r.due) {
                        r.due = true;
                        changed = true;
                    }
                }

                if (changed) {
                    _displayUpdateFence.Set();
                }

                Thread.Sleep(10);
            }
        }

        private void DisplayLoop() {
            while (true) {
                Console.Clear();
                Misc.BigHeaderPrint("Dashboard");
                Console.WriteLine("* Press Q to exit the program. ");
                Console.WriteLine("* Press S to switch over to the manager menu.");
                Console.WriteLine("* Press D to quick delete items.");
                Console.WriteLine("");
                if (Database.Instance.Count() > 0) {
                    int i = 1;
                    foreach (Reminder r in Database.Instance.GetReminders()) {
                        if (r.due) {

                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.ForegroundColor = ConsoleColor.White;

                            Console.WriteLine($"{r} [DUE]");

                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.Green;

                            if (!String.IsNullOrEmpty(r.note)) {
                                Console.WriteLine("Note:  " + r.note);
                            } else {
                                Console.WriteLine("Note:  (Empty)");
                            }

                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.White;
                        } else {
                            Console.WriteLine(r.ToString());
                        }
                        i++;
                    }
                    _displayUpdateFence.WaitOne();
                } else {
                    Console.WriteLine("\nThe list is empty! Switch over to the manager menu to create some!\n\n");
                    while (true) { 
                        Thread.Sleep(1000); 
                    }
                }
            }
        }

        protected override void PrintMenu() { }
        protected override void ProcessInput(string input) { }
    }
}