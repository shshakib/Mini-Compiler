using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class Buket
    {
        public Char[] Key = new Char[80];
        public SymbolTable Binding;
        //public Buket Next;

        public Buket(Char[] K, SymbolTable b)
        {
            Key = K;
            Binding = b;
            //Next = n;
            //throw new System.NotImplementedException();
        }
    }
}
