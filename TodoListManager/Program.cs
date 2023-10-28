using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListManager
{
    class Program
    {
        static void Main(string[] args) {
            DateTimeSelector.setDateFormat("d/M/yyyy");
            DateTimeSelector.setTimeFormat("HH:mm:ss");

            new UserInterface().Run();
        }
    }
}
