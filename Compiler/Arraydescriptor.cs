using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class Arraydescriptor
    {
        private Symboltable_type Element_Type;
        public int Lower_bound;
        public int Upper_bound;
        public int valid = 0;

        public void Arraydescriptor1(Symboltable_type E, int L, int U,int valid1)
        {
            Element_Type = E;
            Lower_bound = L;
            Upper_bound = U;
            valid = valid1;
            //throw new System.NotImplementedException();
        }
    }
}
