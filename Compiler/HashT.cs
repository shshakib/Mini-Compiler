using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
//using Compiler;

namespace Compiler
{
    public class HashT
    {
        private const int size=256;

        static Arraydescriptor a = new Arraydescriptor();
        static Token t = new Token();
        static Symboltable_type s = new Symboltable_type();
        static Char[] ch ={ '&' };
        static SymbolTable b = new SymbolTable(ch, -5, t, a, -1, s, null);
        Buket n = new Buket(ch, b);
        
        Buket[] table = new Buket[size];

        public HashT()
        {
            for (int i = 0; i < size; i++)
            {
                table[i] = n;
            }
            //throw new System.NotImplementedException();
        }

        public int hash()
        {
            int h = 0;
            for (int i = 0; i < size; i++)
                if (table[i] == n)
                {
                    h = i;
                    break;
                }
            return h;
            //throw new System.NotImplementedException();
        }

        public void insert(Char[] S_, SymbolTable b,Form2 fe)
        {
            SymbolTable ob = LookUp(S_);
            if (ob == null)
            {
                int index = hash();
                table[index] = new Buket(S_, b);
            }
            else
            {
                if (ob.func_count == b.func_count)
                {
                    String s = new String(S_);
                    fe.textBox2.Text = fe.textBox2.Text + "\r\n" + "Double declaration of " + s;
                    //MessageBox.Show("double declaration of " + s);
                }
            }
            //throw new System.NotImplementedException();
        }

        public SymbolTable LookUp(Char[] S_)
        {
            Boolean a = false;
            for (int i = 0; i < size; i++)
            {
                int j = 0;
                a = true;
                while (j < S_.Length)
                {
                    if (table[i].Key[j] != S_[j])
                    {
                        a = false;
                        break;
                        //return table[i].Binding;
                    }
                    j++;
                }
                if (a == true) return table[i].Binding;
            }
            return null;
            
            //int index = hash(S_) % size;
            //Buket b;
            //for (b = table[index]; b != null; b = b.Next)
            //    if (b.Key == S_ ) return b.Binding;
            //return null;
            //throw new System.NotImplementedException();
        }

        public void pop(Char[] S_)
        {
            for (int i = 0; i < size; i++)
            {
                if (table[i] != null & table[i].Key == S_)
                {
                    table[i] = n;
                }
            }

            //int index = hash(S_) % size;
            //table[index] = table[index].Next;
            //throw new System.NotImplementedException();
        }

        //public void Update(Char[] S_, SymbolTable b)
        //{
            //SymbolTable f = LookUp(S_);
            //pop(S_);
            //insert(S_, b);
            //throw new System.NotImplementedException();
        //}
    }
}
