using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListManager
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("---Todo list manager---");
            bool running = true;
            while (running) {
                Console.Write("Choose an option: ");
                string option = Console.ReadLine();
                switch (option)
                {
                    case "-q":
                        Console.WriteLine("Quitting!");
                        running = false;
                        break;
                }
            }

        }
    }
}
