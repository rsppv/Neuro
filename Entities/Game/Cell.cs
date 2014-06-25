using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Game
{
    public class Cell
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public int Value { get; set; }
        private int[] Ends = new int[4];

        
        public Cell()
        {
            Row = 0;
            Col = 0;
            Value = 0;
        }

        public Cell(int x, int y, int value)
        {
            Row = x;
            Col = y;
            Value = value;
        }

        
    }
}
