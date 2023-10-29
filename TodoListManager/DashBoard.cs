using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListManager {
    enum ContinueType {
        Quit,
        SwitchMenu,
    }

    class DashBoard {
        

        public void Run() {

            while (true) {
                Thread displayThread = new Thread(DisplayLoop);
                displayThread.Start();

                ContinueType ctype = ContinueType.Quit;

                bool run = true;
                
                while (run) {//Wait for a keypress
                    if (Console.KeyAvailable) {
                        ConsoleKeyInfo key = Console.ReadKey();
                        if (key.Key == ConsoleKey.S || key.Key == ConsoleKey.Q) {
                            if (key.Key == ConsoleKey.S) {
                                ctype = ContinueType.SwitchMenu;
                            }
                            run = false;
                        }
                    }
                }

                displayThread.Abort();
                Console.Clear();

                if (ctype == ContinueType.Quit) {
                    break;
                } else if (ctype == ContinueType.SwitchMenu) {
                    new ReminderManager().Run();
                }
            }
        }

        public void DisplayLoop() {

            while (true) {
                Console.Clear();
                Misc.BigHeaderPrint("Dashboard");
                Console.WriteLine("* Press Q to exit the program. Press S to switch over to the manager menu.\n");
                if (Database.Instance.Count() > 0) {
                    int i = 1;
                    foreach (Reminder r in Database.Instance.GetReminders()) {
                        Console.WriteLine(i + "." + r.ToString() + "\n");
                        i++;
                    }
                    Thread.Sleep(500);
                } else {
                    Console.WriteLine("\n");
                    Console.WriteLine("Your list is empty! Switch over to the manager menu to create some!");
                    Console.WriteLine("\n");
                    while (true);
                }
                
            }
        }
    }
}