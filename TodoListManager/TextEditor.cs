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
        
        //public TextPrompt(string text) { _text = text;}

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

    //class Editor
    //{
    //    Buffer _buffer;
    //    Cursor _cursor;
    //    Stack<object> _history;

    //    public Editor(string text)
    //    {
    //        //var lines = File.ReadAllLines("foo.txt")
    //        //                .Where(x => x != Environment.NewLine);
    //        string[] lines = text.Split('\n');
    //        _buffer = new Buffer(lines);
    //        _cursor = new Cursor();
    //        _history = new Stack<object>();
    //    }

    //    public void Run()
    //    {
    //        while (true)
    //        {
    //            Render();
    //            HandleInput();
    //        }
    //    }

    //    private void HandleInput()
    //    {
    //        var character = Console.ReadKey();


    //        if ((ConsoleModifiers.Control & character.Modifiers) != 0 &&
    //              character.Key == ConsoleKey.Q)
    //        {
    //            Environment.Exit(0);
    //        }

    //        else if ((ConsoleModifiers.Control & character.Modifiers) != 0 &&
    //              character.Key == ConsoleKey.W)
    //        {
    //            _cursor = _cursor.Up(_buffer);
    //        }

    //        else if ((ConsoleModifiers.Control & character.Modifiers) != 0 &&
    //              character.Key == ConsoleKey.S)
    //        {
    //            _cursor = _cursor.Down(_buffer);
    //        }

    //        else if ((ConsoleModifiers.Control & character.Modifiers) != 0 &&
    //              character.Key == ConsoleKey.A)
    //        {
    //            _cursor = _cursor.Left(_buffer);
    //        }

    //        else if ((ConsoleModifiers.Control & character.Modifiers) != 0 &&
    //              character.Key == ConsoleKey.D)
    //        {
    //            _cursor = _cursor.Right(_buffer);
    //        }

    //        else if ((ConsoleModifiers.Control & character.Modifiers) != 0 &&
    //             character.Key == ConsoleKey.U)
    //        {
    //            RestoreSnapshot();
    //        }

    //        else if (character.Key == ConsoleKey.Backspace)
    //        {
    //            if (_cursor.Col > 0)
    //            {
    //                SaveSnapshot();
    //                _buffer = _buffer.Delete(_cursor.Row, _cursor.Col - 1);
    //                _cursor = _cursor.Left(_buffer);
    //            }
    //        }

    //        else if (character.Key == ConsoleKey.Enter)
    //        {
    //            SaveSnapshot();
    //            _buffer = _buffer.SplitLine(_cursor.Row, _cursor.Col);
    //            _cursor = _cursor.Down(_buffer).MoveToCol(0);
    //        }

    //        else
    //        {
    //            SaveSnapshot();
    //            _buffer = _buffer.Insert(character.KeyChar.ToString(), _cursor.Row, _cursor.Col);
    //            _cursor = _cursor.Right(_buffer);
    //        }


    //    }

    //    private void Render()
    //    {
    //        ANSI.ClearScreen();
    //        ANSI.MoveCursor(0, 0);
    //        _buffer.Render();
    //        ANSI.MoveCursor(_cursor.Row, _cursor.Col);
    //    }

    //    private void SaveSnapshot()
    //    {
    //        _history.Push(_cursor);
    //        _history.Push(_buffer);
    //    }

    //    private void RestoreSnapshot()
    //    {
    //        if (_history.Count > 0)
    //        {
    //            _buffer = (Buffer)_history.Pop();
    //            _cursor = (Cursor)_history.Pop();
    //        }
    //    }
    //}

    //class Buffer
    //{
    //    string[] _lines;

    //    public Buffer(IEnumerable<string> lines)
    //    {
    //        _lines = lines.ToArray();
    //    }

    //    public void Render()
    //    {
    //        foreach (var line in _lines)
    //        {
    //            Console.WriteLine(line);
    //        }
    //    }

    //    public int LineCount()
    //    {
    //        return _lines.Count();
    //    }

    //    public int LineLength(int row)
    //    {
    //        return _lines[row].Length;
    //    }

    //    internal Buffer Insert(string character, int row, int col)
    //    {
    //        var linesDeepCopy = _lines.Select(x => x).ToArray();
    //        linesDeepCopy[row] = linesDeepCopy[row].Insert(col, character);
    //        return new Buffer(linesDeepCopy);
    //    }

    //    internal Buffer Delete(int row, int col)
    //    {
    //        var linesDeepCopy = _lines.Select(x => x).ToArray();
    //        linesDeepCopy[row] = linesDeepCopy[row].Remove(col, 1);
    //        return new Buffer(linesDeepCopy);
    //    }

    //    internal Buffer SplitLine(int row, int col)
    //    {
    //        var linesDeepCopy = _lines.Select(x => x).ToList();

    //        var line = linesDeepCopy[row];

    //        var newLines = new[] { line.Substring(0, col), line.Substring(col, line.Length - line.Substring(0, col).Length) };

    //        linesDeepCopy[row] = newLines[0];
    //        linesDeepCopy[row + 1] = newLines[1];



    //        return new Buffer(linesDeepCopy);
    //    }
    //}

    //class Cursor
    //{
    //    public int Row { get; set; }
    //    public int Col { get; set; }


    //    public Cursor(int row = 0, int col = 0)
    //    {
    //        Row = row;
    //        Col = col;
    //    }

    //    internal Cursor Up(Buffer buffer)
    //    {
    //        return new Cursor(Row - 1, Col).Clamp(buffer);
    //    }

    //    internal Cursor Down(Buffer buffer)
    //    {
    //        return new Cursor(Row + 1, Col).Clamp(buffer);
    //    }


    //    internal Cursor Left(Buffer buffer)
    //    {
    //        return new Cursor(Row, Col - 1).Clamp(buffer);
    //    }

    //    internal Cursor Right(Buffer buffer)
    //    {
    //        return new Cursor(Row, Col + 1).Clamp(buffer);
    //    }

    //    private Cursor Clamp(Buffer buffer)
    //    {
    //        Row = Math.Min(buffer.LineCount() - 1, Math.Max(Row, 0));
    //        Col = Math.Min(buffer.LineLength(Row), Math.Max(Col, 0));
    //        return new Cursor(Row, Col);
    //    }

    //    internal Cursor MoveToCol(int col)
    //    {
    //        return new Cursor(Row, 0);
    //    }
    //}

    //class ANSI
    //{
    //    public static void ClearScreen()
    //    {
    //        Console.Clear();
    //    }

    //    public static void MoveCursor(int row, int col)
    //    {
    //        Console.CursorTop = row;
    //        Console.CursorLeft = col;
    //    }
    //}
}