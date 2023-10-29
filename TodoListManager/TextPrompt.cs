using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TodoListManager
{

    class TextPrompt
    {
        bool _singleline = false;
        string _text;
        public TextPrompt() { }
        

        public TextPrompt(bool singleline) { 
            _singleline = singleline;
        }

        public void Run()
        {
            Console.Clear();
            if (_singleline)
            {
                Console.WriteLine("-----Enter a text(Single line only)-----");
                _text = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("-----Enter a text-----");
                string line;
                while (!String.IsNullOrEmpty(line = Console.ReadLine()))
                {
                    _text += line + "\n";
                }
            }
        }

        public string GetResult() { 
            return _text; 
        }
    }

    
}