using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListManager {
    static class Misc {

        public static string SAVE_FILE_NAME = "objects.json";
        public static bool AskConfirm() {
            Console.Clear();
            Console.Write("Are you sure? (Enter -y to confirm) : ");
            var confirm = Console.ReadLine();

            return confirm == "-y";
        }

        public static void PrintReminderPreview(Reminder r) {
            Console.Clear();
            Console.WriteLine("----Preview----");
            Console.WriteLine("Title : " + r.title);
            Console.WriteLine("Datetime : " + r.datetime.ToString(DateTimeSelector.DateFormat + " " + DateTimeSelector.TimeFormat));

            //Console.WriteLine("Note : " +  noteInput);
            if (!String.IsNullOrEmpty(r.note)) {
                string[] sep = r.note.Split('\n');

                Console.WriteLine("Note : " + sep[0]);
                for (int i = 1; i < sep.Length; i++) {
                    //                    Note : 
                    Console.WriteLine("       " + sep[i]);
                }
            } else {
                Console.WriteLine("Note : ");
            }

        }

    }
}
