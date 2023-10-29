using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TodoListManager
{
    class Database
    {
        public static Database Instance { get; private set; }

        public static void Init()
        {
            if (Instance != null)
                throw new Exception("Database is already initialized!");
            
            Instance = new Database();
        }

        List<Reminder> _reminders = new List<Reminder>();

        Database() { }


        public List<Reminder> GetReminders() 
        { 
            return _reminders; 
        }

        public void RemoveAt(int index)
        {
            _reminders.RemoveAt(index);
        }

        public void InsertAt(int index, Reminder item)
        {
            _reminders.Insert(index, item);
        }

        public void Add(Reminder item)
        {
            _reminders.Add(item);
        }

        //public List<Reminder> GetPage(int page, int pageSize)
        //{
        //    if (page <= 0 || pageSize  <= 0)
        //    {
        //        throw new ArgumentOutOfRangeException("Invalid page size: " + pageSize);
        //    }

        //    return new List<Reminder>();
        //}

        public void LoadFromFile()
        {

        }

        public void SaveToFile() 
        {

        }


    }
}
