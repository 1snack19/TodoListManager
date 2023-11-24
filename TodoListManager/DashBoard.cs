using System;
using System.Threading;

namespace TodoListManager {
    
    class DashBoard {

        enum ContinueType {
            Quit,
            SwitchMenu,
        }


        EventWaitHandle _displayUpdateFence;

        public DashBoard() {
            _displayUpdateFence = new EventWaitHandle(false, EventResetMode.AutoReset);
        }

        public void Run() {

            while (true) {
                Console.CursorVisible = false;
                Thread updateThread = new Thread(UpdateLoop);
                Thread displayThread = new Thread(DisplayLoop);
                displayThread.Start();
                updateThread.Start();

                ContinueType ctype = ContinueType.Quit;

                bool run = true;
                
                while (run) {//Wait for a keypress
                    if (Console.KeyAvailable) {
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        if (key.Key == ConsoleKey.S || key.Key == ConsoleKey.Q) {
                            if (key.Key == ConsoleKey.S) {
                                ctype = ContinueType.SwitchMenu;
                            }
                            run = false;
                        }
                    }
                }

                displayThread.Abort();
                updateThread.Abort();
                Console.Clear();
                Console.CursorVisible = true;
                if (ctype == ContinueType.Quit) {
                    break;
                } else if (ctype == ContinueType.SwitchMenu) {
                    new ReminderManager().Run();
                }
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
                Console.WriteLine("* Press Q to exit the program. Press S to switch over to the manager menu.\n");
                if (Database.Instance.Count() > 0) {
                    int i = 1;
                    foreach (Reminder r in Database.Instance.GetReminders()) {
                        if (r.due) {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine($"{r} [DUE]");

                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.White;
                        } else {
                            Console.WriteLine(r.ToString());
                        }
                        i++;
                    }
                    _displayUpdateFence.WaitOne();
                } else {
                    Console.WriteLine("\n");
                    Console.WriteLine("Your list is empty! Switch over to the manager menu to create some!");
                    Console.WriteLine("\n\n");
                    while (true) { 
                        Thread.Sleep(1000); 
                    }
                }
            }
        }
    }
}