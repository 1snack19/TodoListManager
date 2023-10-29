using Newtonsoft.Json;
using System;
using System.IO;
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
            Database.Init();
            Database.Instance.Load();
            DateTimeSelector.SetDateFormat("d/M/yyyy");
            DateTimeSelector.SetTimeFormat("HH:mm:ss");

            new UserInterface().Run();

            Database.Instance.Save();

        }
    }
}
