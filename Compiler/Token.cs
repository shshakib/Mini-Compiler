using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class Token
    {
        public int ROW;
        public int COL;
        public int BLKORD;
        public Symbol Type;
        public Char[] Name;
    }
}
