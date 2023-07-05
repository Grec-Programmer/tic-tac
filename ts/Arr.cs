using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ts
{
    internal class Arr
    {
        static char[,] arr = null;

        private Arr() {}
        static public char[,] getArr()
        {
            if(arr==null) arr = new char[3, 3];
            return arr;
        }
        static public char getArrChar(int x, int y)
        {
            return arr[x,y];
        }
        static public void setArr(int x, int y, char c)
        {
            if (arr == null) arr = new char[3, 3];
            arr[x,y] = c;
        }
        static Form1 form1 = new Form1();
        public static Form1 GetForm()
        {
            if(form1 == null) form1 = new Form1();
            return form1;
        }
    }
}
