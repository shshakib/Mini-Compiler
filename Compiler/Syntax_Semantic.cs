using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Compiler
{
    public class Syntax_Semantic
    {
        private Token CurrentToken;
        private Token LastToken;
        public HashT Symboltable = new HashT();
        public int offset = 0;
        Lexical temp_temp;
        String Code;
        int codecount = 0;
        int Lablecont = 0;

        public Syntax_Semantic(Lexical te)
        {
            temp_temp = te;
            //throw new System.NotImplementedException();
        }
        //private Enum globaltemp;

        public void Program(Enum Stop)
        {
            //temp = new Form1();
            NextSymbol();
            Expect(Symbol.S_program, Stop);
            Expect(Symbol.S_id, Stop);
            Expect(Symbol.S_popen, Stop);
            Identifier_list1(Stop);
            Expect(Symbol.S_pclose, Stop);
            Expect(Symbol.S_semicolon, Stop);
            Declarations(Stop);
            Subprogram_declarations(Stop);
            Compound_statement(Stop);
            Expect(Symbol.S_dot, Stop);
            temp_temp.fc.textBox2.Text = Code;
            temp_temp.fc.Text = "Code";
            temp_temp.fc.Visible = true;
            //throw new System.NotImplementedException();
        }

        public void Identifier_list1(Enum Stop)
        {
            Expect(Symbol.S_id, Stop);
            //Expect(Symbol.S_cama, Stop);
            Identifier_list2(Stop);
            //throw new System.NotImplementedException();
        }

        public void Identifier_list2(Enum Stop)
        {
            if (CurrentToken.Type == Symbol.S_cama)
            {
                Expect(Symbol.S_cama, Stop);
                Expect(Symbol.S_id, Stop);
                Identifier_list2(Stop);
            }
            //throw new System.NotImplementedException();
        }

        public void Identifier_list3(Enum Stop, Stack<Token> mystack)
        {
            mystack.Push(CurrentToken);
            Expect(Symbol.S_id, Stop);
            while (CurrentToken.Type == Symbol.S_cama)
            {
                NextSymbol();
                mystack.Push(CurrentToken);
                Expect(Symbol.S_id, Stop);
            }
            //throw new System.NotImplementedException();
        }

        public void Declarations(Enum Stop)
        {
            Stack<Token> mystack = new Stack<Token>();
            Symbol_class kind = new Symbol_class();
            Arraydescriptor ar = new Arraydescriptor();

            if (CurrentToken.Type == Symbol.S_var)
            {
                Expect(Symbol.S_var, Stop);
                Identifier_list3(Stop, mystack);//call ref OK
                int func_count = CurrentToken.BLKORD;
                Expect(Symbol.S_ddot, Stop);
                Type(Stop, kind, ar);//call ref OK
                if (ar.valid == 0)
                {
                    while (mystack.Count != 0)
                    {
                        Token temp;
                        SymbolTable ob;
                        temp = mystack.Pop();
                        if (kind.Symbol == Symboltable_type.Integer)
                        {
                            ob = new SymbolTable(temp.Name, offset, temp, null, func_count, Symboltable_type.Integer,null);
                        }
                        else
                        {
                            ob = new SymbolTable(temp.Name, offset, temp, null, func_count, Symboltable_type.Real,null);
                        }
                        Symboltable.insert(temp.Name, ob,temp_temp.fe);
                        offset++; offset++;
                    }
                }
                else
                {
                    while (mystack.Count != 0)
                    {
                        Token temp;
                        SymbolTable ob;
                        temp = mystack.Pop();
                        if (kind.Symbol == Symboltable_type.Integer)
                        {
                            ob = new SymbolTable(temp.Name, offset, temp, ar, func_count, Symboltable_type.Integer,null);
                        }
                        else
                        {
                            ob = new SymbolTable(temp.Name, offset, temp, ar, func_count, Symboltable_type.Real,null);
                        }
                        Symboltable.insert(temp.Name, ob,temp_temp.fe);
                        offset = (ar.Upper_bound - ar.Lower_bound + 1) * 2;
                    }
                }
                Expect(Symbol.S_semicolon, Stop);
            }
            //mystack.Clear();
            if (CurrentToken.Type == Symbol.S_var)
            {
                Declarations(Stop);
            }
            //throw new System.NotImplementedException();
        }

        public void Type(Enum Stop,Symbol_class s,Arraydescriptor ar)
        {
            Symbol_class type1 = new Symbol_class();
            Symbol_class type2 = new Symbol_class();

            if (CurrentToken.Type == Symbol.S_array)
            {
                Int32 u = 0, l = 0;
                Expect(Symbol.S_array, Stop);
                Expect(Symbol.S_brakopen, Stop);
                
                Num(Stop, l, type1);//call ref
                String s1 = new String(CurrentToken.Name);
                if (type1.Symbol == Symboltable_type.Integer)
                {
                    l = Convert.ToInt32(s1);
                }
                else l = 0;
                Expect(Symbol.S_digit, Stop);
                
                Expect(Symbol.S_twodot, Stop);
                
                Num(Stop, u, type2);//call ref
                String s2 = new String(CurrentToken.Name);
                if (type2.Symbol == Symboltable_type.Integer)
                {
                    u = Convert.ToInt32(s2);
                }
                else u = 0;
                Expect(Symbol.S_digit, Stop);
                if (type1.Symbol != Symboltable_type.Integer | type2.Symbol != Symboltable_type.Integer) error("Invalid array type");
                else if (l > u) error("Invalid array bound");
                Expect(Symbol.S_brakclose, Stop);
                Expect(Symbol.S_of, Stop);
                Standard_type(Stop, s);//call ref
                ar.Arraydescriptor1(s.Symbol, l, u, 1);
            }
            else
            {
                Standard_type(Stop, s);//call ref OK
                ar.Arraydescriptor1(Symboltable_type.Function_int, 0, 0, 0);
            }
            //throw new System.NotImplementedException();
        }
        
        public void Standard_type(Enum Stop,Symbol_class s)
        {
            if (CurrentToken.Type == Symbol.S_integer)
            {
                Expect(Symbol.S_integer, Stop);
                s.Symbol = Symboltable_type.Integer;
            }
            else
            {
                Expect(Symbol.S_real, Stop);
                s.Symbol = Symboltable_type.Real;
            }
            //throw new System.NotImplementedException();
        }
        
        public void Subprogram_declarations(Enum Stop)
        {
            if (CurrentToken.Type == Symbol.S_function | CurrentToken.Type == Symbol.S_procedure)
            {
                Subprogram_declaration(Stop);
                Expect(Symbol.S_semicolon,Stop);
                Subprogram_declarations(Stop);
            }
            //throw new System.NotImplementedException();
        }

        public void Subprogram_declaration(Enum Stop)
        {
            Subprogram_head(Stop);
            Declarations(Stop);
            Compound_statement(Stop);
            //throw new System.NotImplementedException();
        }

        public void Subprogram_head(Enum Stop)
        {
            Functiondescriptor myarray = new Functiondescriptor();
            if (CurrentToken.Type == Symbol.S_procedure)
            {
                Expect(Symbol.S_procedure, Stop);
                Token c;
                c = CurrentToken;
                Expect(Symbol.S_id, Stop);
                Arguments(Stop,myarray);
                SymbolTable ob = new SymbolTable(c.Name, -1, c, null,CurrentToken.BLKORD,Symboltable_type.Procedure,myarray);//procedure
                Symboltable.insert(c.Name, ob,temp_temp.fe);
                Expect(Symbol.S_semicolon, Stop);
            }
            else
            {
                Expect(Symbol.S_function, Stop);
                Token c;
                Symbol_class s = new Symbol_class();

                c = CurrentToken;
                Expect(Symbol.S_id, Stop);
                Arguments(Stop,myarray);
                Expect(Symbol.S_ddot, Stop);
                Standard_type(Stop, s);//call ref OK
                if (s.Symbol == Symboltable_type.Integer)
                {
                    SymbolTable ob = new SymbolTable(c.Name, -2, c, null,CurrentToken.BLKORD,Symboltable_type.Function_int,myarray);//function int
                    Symboltable.insert(c.Name, ob,temp_temp.fe);
                }
                else
                {
                    SymbolTable ob = new SymbolTable(c.Name, -2, c, null,CurrentToken.BLKORD,Symboltable_type.Function_real,myarray);//function real
                    Symboltable.insert(c.Name, ob,temp_temp.fe);
                }
                Expect(Symbol.S_semicolon,Stop);
            }
            //throw new System.NotImplementedException();
        }

        public void Arguments(Enum Stop,Functiondescriptor myarray)
        {
            if (CurrentToken.Type == Symbol.S_popen)
            {
                Expect(Symbol.S_popen, Stop);
                Parameter_list(Stop,myarray);
                Expect(Symbol.S_pclose, Stop);
            }
            //throw new System.NotImplementedException();
        }

        public void Parameter_list(Enum Stop,Functiondescriptor myarray)
        {
            Stack<Token> mystack=new Stack<Token>();
            Stack<Token> mystack2=new Stack<Token>();

            Identifier_list3(Stop, mystack);

            while (mystack.Count != 0)
            {
                Token ss = mystack.Pop();
                myarray.argument.Add(ss);
                mystack2.Push(ss);
            }
            //myarray.argument. = mystack.ToArray();
            
            Expect(Symbol.S_ddot, Stop);
            Arraydescriptor ar = new Arraydescriptor();
            Symbol_class s = new Symbol_class();
            Token tok;
            
            tok = CurrentToken;
            Type(Stop, s, ar);
            int func_count = CurrentToken.BLKORD;

            if (ar.valid == 0)
            {
                while (mystack2.Count != 0)
                {
                    Token temp;
                    SymbolTable ob;
                    temp = mystack2.Pop();
                    if (s.Symbol == Symboltable_type.Integer)
                    {
                        ob = new SymbolTable(temp.Name, offset, temp, null, func_count, Symboltable_type.Integer,null);
                    }
                    else
                    {
                        ob = new SymbolTable(temp.Name, offset, temp, null, func_count, Symboltable_type.Real,null);
                    }
                    Symboltable.insert(temp.Name, ob,temp_temp.fe);
                    offset++; offset++;
                }
            }
            else
            {
                while (mystack2.Count != 0)
                {
                    Token temp;
                    SymbolTable ob;
                    temp = mystack2.Pop();
                    if (s.Symbol == Symboltable_type.Integer)
                    {
                        ob = new SymbolTable(temp.Name, offset, temp, ar, func_count, Symboltable_type.Integer,null);
                    }
                    else
                    {
                        ob = new SymbolTable(temp.Name, offset, temp, ar, func_count, Symboltable_type.Real,null);
                    }
                    Symboltable.insert(temp.Name, ob,temp_temp.fe);
                    offset = (ar.Upper_bound - ar.Lower_bound + 1) * 2;
                }
            }

            Parameter_list2(Stop,myarray);
            //throw new System.NotImplementedException();
        }

        public void Parameter_list2(Enum Stop,Functiondescriptor myarray)
        {
            if (CurrentToken.Type == Symbol.S_semicolon)
            {
                Expect(Symbol.S_semicolon, Stop);
                Parameter_list(Stop,myarray);
            }
            //throw new System.NotImplementedException();
        }

        public void Compound_statement(Enum Stop)
        {
            Expect(Symbol.S_begin, Stop);
            Optional_statements(Stop);
            Expect(Symbol.S_end, Stop);
            
            //throw new System.NotImplementedException();
        }

        public void Optional_statements(Enum Stop)
        {
            if (CurrentToken.Type != Symbol.S_end)
            {
                Statement_list1(Stop);
            }
            //throw new System.NotImplementedException();
        }

        public void Statement_list1(Enum Stop)
        {
            Statement1(Stop);
            Statement_list2(Stop);
            //throw new System.NotImplementedException();
        }

        public void Statement_list2(Enum Stop)
        {
            if (CurrentToken.Type == Symbol.S_semicolon)
            {
                Expect(Symbol.S_semicolon, Stop);
                Statement_list1(Stop);
            }
            //throw new System.NotImplementedException();
        }

        public void Statement1(Enum Stop)
        {
            Symbol_class tp = new Symbol_class();
            Code_String codst = new Code_String();

            if (CurrentToken.Type == Symbol.S_id)
            {
                SymbolTable ob = Symboltable.LookUp(CurrentToken.Name);
                Expect(Symbol.S_id, Stop);
                if (ob == null) error("Unknown id");
                if (CurrentToken.Type == Symbol.S_assign & ob != null)
                {
                    Statement2(Stop, tp, ob);//call ref
                    if (tp.Symbol != ob.Type) { error("Type not match"); }
                }
                Statement2(Stop, tp,ob);//call ref
            }
            else if (CurrentToken.Type == Symbol.S_begin)
            {
                Compound_statement(Stop);
            }
            else if (CurrentToken.Type == Symbol.S_if)
            {
                Expect(Symbol.S_if, Stop);
                Expression1(Stop, tp, codst);
                Code = Code + "L" + Lablecont + "\r\n";
                Expect(Symbol.S_then, Stop);
                Statement_list1(Stop);
                Expect(Symbol.S_else, Stop);
                Code = Code + "L" + Lablecont + " : \r\n";
                Lablecont++;
                Statement_list1(Stop);
            }
            else if (CurrentToken.Type == Symbol.S_while)
            {
                Expect(Symbol.S_while, Stop);
                Code = Code + "L" + Lablecont + ": \r\n";
                Expression1(Stop, tp, codst);
                Expect(Symbol.S_do, Stop);
                Lablecont++;
                Code = Code + "L" + Lablecont + "\r\n";
                Statement1(Stop);
                int lbtemp = Lablecont - 1;
                Code = Code + "Jmp L" + lbtemp + "\r\n";
                Code = Code + "L" + Lablecont + " : \r\n";
                Lablecont++;
            }
            //throw new System.NotImplementedException();
        }

        public void Statement2(Enum Stop,Symbol_class tp,SymbolTable ob)
        {
            //Symbol tp=Symbol.S_assign;
            Code_String codst = new Code_String();
            Code_String codst1 = new Code_String();

            if (CurrentToken.Type == Symbol.S_assign)
            {
                Expect(Symbol.S_assign, Stop);
                Expression1(Stop, tp, codst);//call ref

                Code = Code + "Mov ";
                for (int i = 0; i < ob.Name.Length; i++)
                {
                    if (ob.Name[i] == '\0')
                    {
                        break;
                    }
                    Code = Code + ob.Name[i];
                }
                Code = Code + ",";
                for (int i = 0; i < codst.codest.Length; i++)
                {
                    if (codst.codest[i] == '\0')
                    {
                        break;
                    }
                    Code = Code + codst.codest[i];
                }
                Code = Code + "\r\n";

            }
            else if (CurrentToken.Type == Symbol.S_brakopen)
            {
                Expect(Symbol.S_brakopen, Stop);

                String s2="1";
                int test = 0;
                Lexical temp2 = new Lexical(temp_temp.InText);
                temp2.pointer = temp_temp.pointer-1;
                Token te = temp2.lexer();
                //te = temp2.lexer();
                if (CurrentToken.Type == Symbol.S_digit & te.Type == Symbol.S_brakclose)
                {
                    String s = new String(CurrentToken.Name);
                    s2 = s;
                    test = 1;
                }
                temp2 = null;
                Expression1(Stop, tp, codst);//call ref
                if (tp.Symbol != Symboltable_type.Integer)
                {
                    error("Array bound type must be integer");//array[ type ]
                }
                else if (test == 1)
                {
                    if (ob != null)
                    {
                        if (ob.arType != null)
                        {
                            if (Convert.ToInt32(s2) > ob.arType.Upper_bound | Convert.ToInt32(s2) < ob.arType.Lower_bound)
                            {
                                error("Array bound is not valid");
                            }
                        }
                    }
                }
                Expect(Symbol.S_brakclose, Stop);
                Expect(Symbol.S_assign, Stop);
                Expression1(Stop, tp, codst1);//call ref
            }
            else if (CurrentToken.Type == Symbol.S_popen)
            {
                Stack<Symboltable_type> mystack = new Stack<Symboltable_type>();
                //Functiondescriptor myarray = new Functiondescriptor();
                Expect(Symbol.S_popen, Stop);
                Expression_list1(Stop,mystack);
                //myarray.argument[1]. = mystack.ToArray();
                int i=0;
                Stack<SymbolTable> mystack2 = new Stack<SymbolTable>();
                //while (ob.func_dis.argument.Length != i)
                //{
                //    mystack2.Push(Symboltable.LookUp(ob.func_dis.argument[i].Name));
                //    i++;
                //}
                //i = 0;
                if (ob != null)
                {
                    if (ob.func_dis != null)
                    {
                        while (ob.func_dis.argument.Count != i)
                        {
                            Token tp1 = null;
                            tp1 = ob.func_dis.argument[i];
                            i++;
                            mystack2.Push(Symboltable.LookUp(tp1.Name));
                        }
                        if (mystack.Count != mystack2.Count)
                        {
                            error("Number of argument not match");
                        }
                        else
                        {
                            while (mystack2.Count != 0)
                            {
                                Symboltable_type a = mystack2.Pop().Type;
                                Symboltable_type b = mystack.Pop();
                                if (a != b)
                                {
                                    error("Type not match");
                                }
                            }
                        }
                    }
                }
                //while (mystack.Count != 0)
                //{

                //}
                Expect(Symbol.S_pclose, Stop);
            }
            //throw new System.NotImplementedException();
        }

        public void Expression_list1(Enum Stop,Stack<Symboltable_type> mystack)
        {
            Symbol_class tp = new Symbol_class();
            Code_String codst = new Code_String();

            Expression1(Stop, tp, codst);//call ref
            mystack.Push(tp.Symbol);
            Expression_list2(Stop, mystack);
            //throw new System.NotImplementedException();
        }

        public void Expression_list2(Enum Stop, Stack<Symboltable_type> mystack)
        {
            if (CurrentToken.Type == Symbol.S_cama)
            {
                Expect(Symbol.S_cama, Stop);
                Expression_list1(Stop,mystack);
            }
            //throw new System.NotImplementedException();
        }

        public void Expression1(Enum Stop,Symbol_class tp, Code_String codst)
        {
            Symbol_class tp1 = new Symbol_class();
            Symbol_class tp2 = new Symbol_class();
            //Code_String codst = new Code_String();

            Simple_expression1(Stop, tp1, codst);//cal ref
            Expression2(Stop,tp1, codst);//not call ref
            tp.Symbol = tp1.Symbol;
            //if (tp1.Symbol != tp2.Symbol) error("type missmatch");
            //tp.Symbol = tp1.Symbol;
            //throw new System.NotImplementedException();
        }

        public void Expression2(Enum Stop,Symbol_class tp,Code_String codst)
        {
            Symbol_class tp2 = new Symbol_class();
            Code_String codst1 = new Code_String();

            if (CurrentToken.Type == Symbol.S_equal | CurrentToken.Type == Symbol.S_notequal |
                CurrentToken.Type == Symbol.S_shorter | CurrentToken.Type == Symbol.S_seq |
                CurrentToken.Type == Symbol.S_greater | CurrentToken.Type == Symbol.S_geq)
            {
                if (CurrentToken.Type == Symbol.S_equal)
                {
                    Expect(Symbol.S_equal, Stop);
                    Simple_expression1(Stop, tp2, codst1);
                    Code = Code + "Cmp ";// +"Data" + codecount + ",";
                    for (int i = 0; i < codst.codest.Length; i++)
                    {
                        if (codst.codest[i] == '\0')
                        {
                            break;
                        }
                        Code = Code + codst.codest[i];
                    }
                    Code = Code + ",";
                    for (int i = 0; i < codst1.codest.Length; i++)
                    {
                        if (codst1.codest[i] == '\0')
                        {
                            break;
                        }
                        Code = Code + codst1.codest[i];
                    }
                    Code = Code + "\r\n";

                    Code = Code + "JNE ";
                }
                else if (CurrentToken.Type == Symbol.S_notequal)
                {
                    Expect(Symbol.S_notequal, Stop);
                    Simple_expression1(Stop, tp2, codst1);
                    Code = Code + "Cmp ";// +"Data" + codecount + ",";
                    for (int i = 0; i < codst.codest.Length; i++)
                    {
                        if (codst.codest[i] == '\0')
                        {
                            break;
                        }
                        Code = Code + codst.codest[i];
                    }
                    Code = Code + ",";
                    for (int i = 0; i < codst1.codest.Length; i++)
                    {
                        if (codst1.codest[i] == '\0')
                        {
                            break;
                        }
                        Code = Code + codst1.codest[i];
                    }
                    Code = Code + "\r\n";
                    Code = Code + "JE ";
                }

                else if (CurrentToken.Type == Symbol.S_shorter)
                {
                    Expect(Symbol.S_shorter, Stop);
                    Simple_expression1(Stop, tp2, codst1);
                    Code = Code + "Cmp ";// +"Data" + codecount + ",";
                    for (int i = 0; i < codst.codest.Length; i++)
                    {
                        if (codst.codest[i] == '\0')
                        {
                            break;
                        }
                        Code = Code + codst.codest[i];
                    }
                    Code = Code + ",";
                    for (int i = 0; i < codst1.codest.Length; i++)
                    {
                        if (codst1.codest[i] == '\0')
                        {
                            break;
                        }
                        Code = Code + codst1.codest[i];
                    }
                    Code = Code + "\r\n";
                    Code = Code + "JGE ";
                }


                else if (CurrentToken.Type == Symbol.S_seq)
                {
                    Expect(Symbol.S_seq, Stop);
                    Simple_expression1(Stop, tp2, codst1);
                    Code = Code + "Cmp ";// +"Data" + codecount + ",";
                    for (int i = 0; i < codst.codest.Length; i++)
                    {
                        if (codst.codest[i] == '\0')
                        {
                            break;
                        }
                        Code = Code + codst.codest[i];
                    }
                    Code = Code + ",";
                    for (int i = 0; i < codst1.codest.Length; i++)
                    {
                        if (codst1.codest[i] == '\0')
                        {
                            break;
                        }
                        Code = Code + codst1.codest[i];
                    }
                    Code = Code + "\r\n";
                    Code = Code + "JG ";
                }

                else if (CurrentToken.Type == Symbol.S_greater)
                {
                    Expect(Symbol.S_greater, Stop);
                    Simple_expression1(Stop, tp2, codst1);
                    Code = Code + "Cmp ";// +"Data" + codecount + ",";
                    for (int i = 0; i < codst.codest.Length; i++)
                    {
                        if (codst.codest[i] == '\0')
                        {
                            break;
                        }
                        Code = Code + codst.codest[i];
                    }
                    Code = Code + ",";
                    for (int i = 0; i < codst1.codest.Length; i++)
                    {
                        if (codst1.codest[i] == '\0')
                        {
                            break;
                        }
                        Code = Code + codst1.codest[i];
                    }
                    Code = Code + "\r\n";
                    Code = Code + "JSE ";
                }

                else if (CurrentToken.Type == Symbol.S_geq)
                {
                    Expect(Symbol.S_geq, Stop);
                    Simple_expression1(Stop, tp2, codst1);
                    Code = Code + "Cmp ";// +"Data" + codecount + ",";
                    for (int i = 0; i < codst.codest.Length; i++)
                    {
                        if (codst.codest[i] == '\0')
                        {
                            break;
                        }
                        Code = Code + codst.codest[i];
                    }
                    Code = Code + ",";
                    for (int i = 0; i < codst1.codest.Length; i++)
                    {
                        if (codst1.codest[i] == '\0')
                        {
                            break;
                        }
                        Code = Code + codst1.codest[i];
                    }
                    Code = Code + "\r\n";
                    Code = Code + "JS ";
                }
                if (tp.Symbol != tp2.Symbol)
                {
                    error("Type not match");//type missmatch
                }            
            }
            //throw new System.NotImplementedException();
        }

        public void Simple_expression1(Enum Stop,Symbol_class tp,Code_String codst)
        {
            Symbol_class tp1 = new Symbol_class();

            Sign(Stop);
            Term1(Stop, tp, codst);//call ref
            if (CurrentToken.Type == Symbol.S_add | CurrentToken.Type == Symbol.S_sub |
                CurrentToken.Type == Symbol.S_or)
            {

                Simple_expression2(Stop, tp1, codst);//call ref
                if (tp1.Symbol != tp.Symbol) error("Type not match");
            }
            //throw new System.NotImplementedException();
        }

        public void Simple_expression2(Enum Stop, Symbol_class tp,Code_String codst)
        {
            Symbol_class tp2 = new Symbol_class();
            Code_String codst2 = new Code_String();
            //Code_String codst3 = new Code_String();

            if (CurrentToken.Type == Symbol.S_add | CurrentToken.Type == Symbol.S_sub |
                CurrentToken.Type == Symbol.S_or)
            {
                if (CurrentToken.Type == Symbol.S_add)
                {
                    Expect(Symbol.S_add, Stop);
                    Term1(Stop, tp, codst2);//call ref
                    Simple_expression2(Stop, tp2, codst);//call ref
                    Code = Code + "Mov " + "Data" + codecount + ",";
                    for (int i = 0; i < codst2.codest.Length; i++)
                    {
                        if (codst2.codest[i] == '\0')
                        {
                            break;
                        }
                        Code = Code + codst2.codest[i];
                    }
                    Code = Code + "\r\n";
                    Code = Code + "Add " + "Data" + codecount + ",";
                    for (int i = 0; i < codst.codest.Length; i++)
                    {
                        if (codst.codest[i] == '\0')
                        {
                            break;
                        }
                        Code = Code + codst.codest[i];
                    }                    
                    Code = Code + "\r\n";
                    codst.codest = "Data" + codecount;
                    codecount++;
                                       
                }
                else if (CurrentToken.Type == Symbol.S_sub)
                {
                    Expect(Symbol.S_sub, Stop);
                    Term1(Stop, tp, codst2);//call ref
                    Simple_expression2(Stop, tp2, codst);//call ref
                    Code = Code + "Mov " + "Data" + codecount + ",";
                    for (int i = 0; i < codst2.codest.Length; i++)
                    {
                        if (codst2.codest[i] == '\0')
                        {
                            break;
                        }
                        Code = Code + codst2.codest[i];
                    }
                    Code = Code + "\r\n";
                    Code = Code + "Sub " + "Data" + codecount + ",";
                    for (int i = 0; i < codst.codest.Length; i++)
                    {
                        if (codst.codest[i] == '\0')
                        {
                            break;
                        }
                        Code = Code + codst.codest[i];
                    }
                    Code = Code + "\r\n";
                    codst.codest = "Data" + codecount;
                    codecount++;
                    
                }
                else if (CurrentToken.Type == Symbol.S_or)
                {
                    Expect(Symbol.S_or, Stop);
                    Term1(Stop, tp, codst2);//call ref
                    Simple_expression2(Stop, tp2, codst);//call ref
                    Code = Code + "Mov " + "Data" + codecount + ",";
                    for (int i = 0; i < codst2.codest.Length; i++)
                    {
                        if (codst2.codest[i] == '\0')
                        {
                            break;
                        }
                        Code = Code + codst2.codest[i];
                    }
                    Code = Code + "\r\n";
                    Code = Code + "Or " + "Data" + codecount + ",";
                    for (int i = 0; i < codst.codest.Length; i++)
                    {
                        if (codst.codest[i] == '\0')
                        {
                            break;
                        }
                        Code = Code + codst.codest[i];
                    }
                    Code = Code + "\r\n";
                    codst.codest = "Data" + codecount;
                    codecount++;
                    
                }                
            }
            //throw new System.NotImplementedException();
        }

        public void Term1(Enum Stop,Symbol_class tp,Code_String codst)
        {
            Symbol_class tp1 = new Symbol_class();
            Symbol_class tp2 = new Symbol_class();
            

            if (CurrentToken.Type == Symbol.S_id | CurrentToken.Type == Symbol.S_digit)
            {
                Code_String codst1=new Code_String();
                codst1.codest = new String(CurrentToken.Name);
                codst.codest = codst1.codest;
            }
            Factor1(Stop, tp1, codst);//call ref
            if (CurrentToken.Type == Symbol.S_mul | CurrentToken.Type == Symbol.S_realdiv |
                CurrentToken.Type == Symbol.S_div | CurrentToken.Type == Symbol.S_mod |
                CurrentToken.Type == Symbol.S_and)
            {
                Term2(Stop, tp2, codst);//call ref
                if (tp1.Symbol != tp2.Symbol) error("Type not match");
            }
            tp.Symbol = tp1.Symbol;
            //throw new System.NotImplementedException();
        }

        public void Term2(Enum Stop,Symbol_class tp,Code_String codst)
        {
            Code_String codst2 = new Code_String();
            
            if (CurrentToken.Type == Symbol.S_mul | CurrentToken.Type == Symbol.S_realdiv |
                CurrentToken.Type == Symbol.S_div | CurrentToken.Type == Symbol.S_mod |
                CurrentToken.Type == Symbol.S_and)
            {
                if (CurrentToken.Type == Symbol.S_mul)
                {
                    Expect(Symbol.S_mul, Stop);
                    Term1(Stop, tp, codst2);//call ref
                    
                    Code = Code + "Mov " + "Data" + codecount + ",";
                    for (int i = 0; i < codst.codest.Length; i++)
                    {
                        if (codst.codest[i] == '\0')
                        {
                            break;
                        }
                        Code = Code + codst.codest[i];
                    }
                    Code = Code + "\r\n";
                    Code = Code + "Mul " + "Data" + codecount + ",";
                    for (int i = 0; i < codst2.codest.Length; i++)
                    {
                        if (codst2.codest[i] == '\0')
                        {
                            break;
                        }
                        Code = Code + codst2.codest[i];
                    }
                    Code = Code + "\r\n";
                    codst.codest = "Data" + codecount;                
                    codecount++;
                }
                else if (CurrentToken.Type == Symbol.S_realdiv)
                {
                    Expect(Symbol.S_realdiv, Stop);
                    Term1(Stop, tp, codst2);//call ref
                    Code = Code + "Mov " + "Data" + codecount + ",";
                    for (int i = 0; i < codst.codest.Length; i++)
                    {
                        if (codst.codest[i] == '\0')
                        {
                            break;
                        }
                        Code = Code + codst.codest[i];
                    }
                    Code = Code + "\r\n";
                    Code = Code + "Div " + "Data" + codecount + ",";
                    for (int i = 0; i < codst2.codest.Length; i++)
                    {
                        if (codst2.codest[i] == '\0')
                        {
                            break;
                        }
                        Code = Code + codst2.codest[i];
                    }
                    Code = Code + "\r\n";
                    codst.codest = "Data" + codecount;
                    codecount++;
                }
                else if (CurrentToken.Type == Symbol.S_div)
                {
                    Expect(Symbol.S_div, Stop);
                    Term1(Stop, tp, codst2);//call ref
                    Code = Code + "Mov " + "Data" + codecount + ",";
                    for (int i = 0; i < codst.codest.Length; i++)
                    {
                        if (codst.codest[i] == '\0')
                        {
                            break;
                        }
                        Code = Code + codst.codest[i];
                    }
                    Code = Code + "\r\n";
                    Code = Code + "Div " + "Data" + codecount + ",";
                    for (int i = 0; i < codst2.codest.Length; i++)
                    {
                        if (codst2.codest[i] == '\0')
                        {
                            break;
                        }
                        Code = Code + codst2.codest[i];
                    }
                    Code = Code + "\r\n";
                    codst.codest = "Data" + codecount;
                    codecount++;
                }
                else if (CurrentToken.Type == Symbol.S_mod)
                {
                    Expect(Symbol.S_mod, Stop);
                    Term1(Stop, tp, codst2);//call ref
                    Code = Code + "Mov " + "Data" + codecount + ",";
                    for (int i = 0; i < codst.codest.Length; i++)
                    {
                        if (codst.codest[i] == '\0')
                        {
                            break;
                        }
                        Code = Code + codst.codest[i];
                    }
                    Code = Code + "\r\n";
                    Code = Code + "Mod " + "Data" + codecount + ",";
                    for (int i = 0; i < codst2.codest.Length; i++)
                    {
                        if (codst2.codest[i] == '\0')
                        {
                            break;
                        }
                        Code = Code + codst2.codest[i];
                    }
                    Code = Code + "\r\n";
                    codst.codest = "Data" + codecount;
                    codecount++;
                }
                else if (CurrentToken.Type == Symbol.S_and)
                {
                    Expect(Symbol.S_and, Stop);
                    Term1(Stop, tp, codst2);//call ref
                    Code = Code + "Mov " + "Data" + codecount + ",";
                    for (int i = 0; i < codst.codest.Length; i++)
                    {
                        if (codst.codest[i] == '\0')
                        {
                            break;
                        }
                        Code = Code + codst.codest[i];
                    }
                    Code = Code + "\r\n";
                    Code = Code + "And " + "Data" + codecount + ",";
                    for (int i = 0; i < codst2.codest.Length; i++)
                    {
                        if (codst2.codest[i] == '\0')
                        {
                            break;
                        }
                        Code = Code + codst2.codest[i];
                    }
                    Code = Code + "\r\n";
                    codst.codest = "Data" + codecount;
                    codecount++;
                }
                
            }
            //throw new System.NotImplementedException();
        }

        public void Factor1(Enum Stop,Symbol_class tp,Code_String codst)
        {
            if (CurrentToken.Type == Symbol.S_id)
            {
                SymbolTable ob = Symboltable.LookUp(CurrentToken.Name);
                Expect(Symbol.S_id, Stop);
                if (ob == null) error("Unknown id");
                else tp.Symbol = ob.Type;
                Factor2(Stop);
            }
            else if (CurrentToken.Type == Symbol.S_digit)
            {
                Num(Stop,tp);//call ref
            }
            else if (CurrentToken.Type == Symbol.S_popen)
            {
                //Code_String codst = new Code_String();
                Expect(Symbol.S_popen, Stop);
                Expression1(Stop, tp, codst);
                Expect(Symbol.S_pclose, Stop);
            }
            else
            {
                Expect(Symbol.S_not, Stop);
                Factor1(Stop, tp, codst);//call ref
            }
            //throw new System.NotImplementedException();
        }

        public void Factor2(Enum Stop)
        {
            if (CurrentToken.Type == Symbol.S_popen)
            {
                Stack<Symboltable_type> mystack=new Stack<Symboltable_type>();
                Expect(Symbol.S_popen, Stop);
                Expression_list1(Stop,mystack);
                Expect(Symbol.S_pclose, Stop);
            }
            //throw new System.NotImplementedException();
        }

        public void Sign(Enum Stop)
        {
            if (CurrentToken.Type == Symbol.S_add | CurrentToken.Type == Symbol.S_sub)
            {
                if (CurrentToken.Type == Symbol.S_add)
                    Expect(Symbol.S_add, Stop);
                else
                {
                    Expect(Symbol.S_sub, Stop);
                }
            }
            //throw new System.NotImplementedException();
        }

        public void Num(Enum Stop,Int32 value,Symbol_class type)
        {
            type.Symbol = Symboltable_type.Integer;
            for (int i = 0; i < CurrentToken.Name.Length & CurrentToken.Name[i] != '\0'; i++)
            {
                if (CurrentToken.Name[i] == '.')
                {
                    type.Symbol = Symboltable_type.Real;
                    //value = Convert.ToInt32(CurrentToken.Name);
                }
            }
            //Optional_exponent(Stop);
            //throw new System.NotImplementedException();
        }

        public void Num(Enum Stop, Symbol_class type)
        {
            type.Symbol = Symboltable_type.Integer;
            for (int i = 0; i < CurrentToken.Name.Length & CurrentToken.Name[i]!='\0'; i++)
            {
                if (CurrentToken.Name[i] == '.')
                {
                    type.Symbol = Symboltable_type.Real;
                }
            }
            Expect(Symbol.S_digit, Stop);
            //Optional_exponent(Stop);
            //throw new System.NotImplementedException();
        }

        //public void Optional_exponent(Enum Stop)
        //{
        //    if (CurrentToken.Type == Symbol.S_add | CurrentToken.Type == Symbol.S_sub)
        //    {
        //        if (CurrentToken.Type == Symbol.S_add)
        //            Expect(Symbol.S_add, Stop);
        //        else Expect(Symbol.S_sub, Stop);
        //    }
        //    //throw new System.NotImplementedException();
        //}

        public void error(String s)
        {
            temp_temp.fe.Visible = true;
            temp_temp.fe.textBox2.Text = temp_temp.fe.textBox2.Text + "\r\n" + s + " Row :" + LastToken.ROW.ToString() + " Col :" + LastToken.COL.ToString();
            //MessageBox.Show(s + " Row :" + LastToken.ROW.ToString() + " Col :" + LastToken.COL.ToString());
        }

        public void error1(String s)
        {
            temp_temp.fe.Visible = true;
            temp_temp.fe.textBox2.Text = temp_temp.fe.textBox2.Text + "\r\n" + s + " Row :" + CurrentToken.ROW.ToString() + " Col :" + CurrentToken.COL.ToString();
            //MessageBox.Show(s + "Row :" + CurrentToken.ROW.ToString() + " Col :" + CurrentToken.COL.ToString());
            
        }

        public void Expect(Symbol S, Enum Stop)
        {
            if (CurrentToken.Type == S) NextSymbol();
            else
            {
                
                error1("Syntax Error : "+ S.ToString() +" Expected");
                if (CurrentToken.Type != Symbol.S_dot)
                {
                    Lexical temp2 = new Lexical(temp_temp.InText);
                    temp2.pointer = temp_temp.pointer-1;
                    Token te=new Token();
                    for (int i = 0; i < 5 & te.Type != Symbol.S_dot;i++ ){
                        te = temp2.lexer();
                        if (te.Type == S)
                        {
                            int s = i+1;
                            while (s >= 0)
                            {
                                NextSymbol();
                                s--;
                            }
                            break;
                        }
                    }
                }
            }
            //throw new System.NotImplementedException();
        }
        
        public void NextSymbol()
        {
            LastToken = CurrentToken;
            CurrentToken = temp_temp.lexer();
            //throw new System.NotImplementedException();
        }
    }
}
