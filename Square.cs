using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppBingo
{
    public enum Column
    {
        B = 0,
        I = 1,
        N = 2,
        G = 3,
        O = 4
    }
    public class Square
    {
        public int Id { get; set; }
        public bool Pick { get; set; }
        public Column Letter { get; set; }

        public Square() { }
        public Square(int key, bool val)
        {
            this.Id = key;
            this.Pick = val;
        }

        public Square(int key, bool val, Column column)
        {
            this.Id = key;
            this.Pick = val;
            this.Letter = column;
        }
    }
}
