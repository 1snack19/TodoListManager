using System;

namespace TodoListManager
{

    class Program
    {

        static void Main(string[] args) {
            Database.Init();
            Database.Instance.Load();
            Misc.SetDateFormat("d/M/yyyy");
            Misc.SetTimeFormat("HH:mm:ss");


            try {
                new DashBoard().Run();
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
            }

            Database.Instance.Save();
        }

    }
}
