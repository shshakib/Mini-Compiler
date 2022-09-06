using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class SymbolTable
    {
        public Char[] Name;
        public Token Token;
        public int offset;
        public int func_count;
        public Symboltable_type Type;
        public Arraydescriptor arType;
        public Functiondescriptor func_dis;

        public SymbolTable(Char[] n, int o, Token t, Arraydescriptor ty, int fc, Symboltable_type st,Functiondescriptor fd)
        {
            Name = n;
            Token = t;
            offset = o;
            arType = ty;
            func_count = fc;
            Type = st;
            func_dis = fd;
            //throw new System.NotImplementedException();
        }
    }
}
