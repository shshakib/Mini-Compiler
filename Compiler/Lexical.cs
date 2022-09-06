using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Compiler
{
    public class Lexical
    {
        public Form2 fe = new Form2();
        public Form2 fc = new Form2();
        public String InText;
        public int pointer = -1;
        private Char LastChar = '\0';
        private int RowNi = 0;
        private int ColNo = 0;
        private int BlockOrder = 0;

        public Lexical(String s)
        {
            InText = s;
            //pointer = -1;
            //throw new System.NotImplementedException();
        }

        public Token lexer()
        {
            //Symbol LexiconType;
            Char NextChar;
            Char[] NextWord = new Char[80];
            int State, Length;
            //Char LastChar='\0';
            //int RowNo = 0, ColNo = 0;
            State = 0;
            Length = 0;
            while (pointer < InText.Length)
            {
                if (!(LastChar == '\0')) { NextChar = LastChar; LastChar = '\0'; }
                else if (pointer < InText.Length - 1)
                {
                    NextChar = InText[++pointer];
                    ColNo++;
                }
                else
                {
                    NextChar = ' ';
                    pointer++;
                }
                NextWord[Length++] = NextChar;
                switch (State)
                {
                    case 0: if (NextChar == '\r')
                        {
                            State = 1;
                            break;
                        }
                        //else ColNo++;
                        if (NextChar == ' ') Length = 0;
                        else if ((NextChar <= 'z' && NextChar >= 'a') ||
                                (NextChar <= 'Z' && NextChar >= 'A'))
                        {
                            State = 2;
                            LastChar = NextChar;
                            Length--;
                        }
                        else if (NextChar <= '9' && NextChar >= '0')
                        {
                            State = 80;
                            LastChar = NextChar;
                            Length--;
                        }
                        else if (NextChar == '(') State = 82;
                        else if (NextChar == ')') State = 83;
                        else if (NextChar == ',') State = 84;
                        else if (NextChar == ':') State = 85;
                        else if (NextChar == ';') State = 86;
                        else if (NextChar == '[') State = 87;
                        else if (NextChar == ']') State = 88;
                        else if (NextChar == '.') State = 89;
                        else if (NextChar == '+') State = 90;
                        else if (NextChar == '-') State = 91;
                        else if (NextChar == '=') State = 92;
                        else if (NextChar == '<') State = 93;
                        else if (NextChar == '>') State = 94;
                        else if (NextChar == '*') State = 95;
                        else if (NextChar == '/') State = 96;
                        else error(RowNi, ColNo);
                        break;
                    case 1: if (NextChar == '\n')
                        {
                            RowNi++;
                            ColNo = 0;
                            Length = 0;
                            for (int i = 0; i < 80; i++)
                                NextWord[i] = '\0';
                            State = 0;
                        }
                        break;
                    case 2: switch (NextChar)
                        {
                            case 'p': State = 3;
                                break;
                            case 'P': State = 3;
                                break;
                            case 'v': State = 4;
                                break;
                            case 'V': State = 4;
                                break;
                            case 'a': State = 5;
                                break;
                            case 'A': State = 5;
                                break;
                            case 'o': State = 6;
                                break;
                            case 'O': State = 6;
                                break;
                            case 'i': State = 7;
                                break;
                            case 'I': State = 7;
                                break;
                            case 'r': State = 8;
                                break;
                            case 'R': State = 8;
                                break;
                            case 'f': State = 9;
                                break;
                            case 'F': State = 9;
                                break;
                            case 'b': State = 10;
                                break;
                            case 'B': State = 10;
                                break;
                            case 'e': State = 11;
                                break;
                            case 'E': State = 11;
                                break;
                            case 't': State = 12;
                                break;
                            case 'T': State = 12;
                                break;
                            case 'w': State = 13;
                                break;
                            case 'W': State = 13;
                                break;
                            case 'd': State = 14;
                                break;
                            case 'D': State = 14;
                                break;
                            case 'n': State = 15;
                                break;
                            case 'N': State = 15;
                                break;
                            case 'm': State = 16;
                                break;
                            case 'M': State = 16;
                                break;
                            default: State = 17;
                                break;
                        }
                        break;
                    case 3: if (NextChar == 'r' | NextChar == 'R') State = 18;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 4: if (NextChar == 'a' | NextChar == 'A') State = 19;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 5: if (NextChar == 'r' | NextChar == 'R') State = 20;
                        else if (NextChar == 'n' | NextChar == 'N') State = 21;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 6: if (NextChar == 'f' | NextChar == 'F') State = 22;
                        else if (NextChar == 'r' | NextChar == 'R') State = 23;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 7: if (NextChar == 'n' | NextChar == 'N') State = 24;
                        else if (NextChar == 'f' | NextChar == 'F') State = 25;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 8: if (NextChar == 'e' | NextChar == 'E') State = 26;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 9: if (NextChar == 'u' | NextChar == 'U') State = 27;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 10: if (NextChar == 'e' | NextChar == 'E') State = 28;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 11: if (NextChar == 'n' | NextChar == 'N') State = 29;
                        else if (NextChar == 'l' | NextChar == 'L') State = 30;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 12: if (NextChar == 'h' | NextChar == 'H') State = 31;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 13: if (NextChar == 'h' | NextChar == 'H') State = 32;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 14: if (NextChar == 'o' | NextChar == 'O') State = 33;
                        else if (NextChar == 'i' | NextChar == 'I') State = 34;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 15: if (NextChar == 'o' | NextChar == 'O') State = 35;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 16: if (NextChar == 'o' | NextChar == 'O') State = 36;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 17: if (NextChar == ' ' | NextChar == '(' | NextChar == ')'
                               | NextChar == ',' | NextChar == ':' | NextChar == ';'
                               | NextChar == '[' | NextChar == ']' | NextChar == '.'
                               | NextChar == '+' | NextChar == '-' | NextChar == '='
                               | NextChar == '<' | NextChar == '>' | NextChar == '*'
                               | NextChar == '/' | NextChar == '\r')
                        {
                            LastChar = NextChar;
                            Length--;
                            NextWord[Length] = '\0';
                            return MakeToken(Symbol.S_id, BlockOrder, ColNo, NextWord, RowNi);
                        }
                        else State = 17;
                        break;
                    case 18: if (NextChar == 'o' | NextChar == 'O') State = 37;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 19: if (NextChar == 'r' | NextChar == 'R') State = 38;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 20: if (NextChar == 'r' | NextChar == 'R') State = 39;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 21: if (NextChar == 'd' | NextChar == 'D') State = 40;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 22: if (NextChar == ' ' | NextChar == '(' | NextChar == ')'
                               | NextChar == ',' | NextChar == ':' | NextChar == ';'
                               | NextChar == '[' | NextChar == ']' | NextChar == '.'
                               | NextChar == '+' | NextChar == '-' | NextChar == '='
                               | NextChar == '<' | NextChar == '>' | NextChar == '*'
                               | NextChar == '/' | NextChar == '\r')
                        {
                            LastChar = NextChar;
                            Length--;
                            NextWord[Length] = '\0';
                            return MakeToken(Symbol.S_of, BlockOrder, ColNo, NextWord, RowNi);
                        }
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 23: if (NextChar == ' ' | NextChar == '(' | NextChar == ')'
                               | NextChar == ',' | NextChar == ':' | NextChar == ';'
                               | NextChar == '[' | NextChar == ']' | NextChar == '.'
                               | NextChar == '+' | NextChar == '-' | NextChar == '='
                               | NextChar == '<' | NextChar == '>' | NextChar == '*'
                               | NextChar == '/' | NextChar == '\r')
                        {
                            LastChar = NextChar;
                            Length--;
                            NextWord[Length] = '\0';
                            return MakeToken(Symbol.S_or, BlockOrder, ColNo, NextWord, RowNi);
                        }
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 24: if (NextChar == 't' | NextChar == 'T') State = 41;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 25: if (NextChar == ' ' | NextChar == '(' | NextChar == ')'
                               | NextChar == ',' | NextChar == ':' | NextChar == ';'
                               | NextChar == '[' | NextChar == ']' | NextChar == '.'
                               | NextChar == '+' | NextChar == '-' | NextChar == '='
                               | NextChar == '<' | NextChar == '>' | NextChar == '*'
                               | NextChar == '/' | NextChar == '\r')
                        {
                            LastChar = NextChar;
                            Length--;
                            NextWord[Length] = '\0';
                            return MakeToken(Symbol.S_if, BlockOrder, ColNo, NextWord, RowNi);
                        }
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 26: if (NextChar == 'a' | NextChar == 'A') State = 42;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 27: if (NextChar == 'n' | NextChar == 'N') State = 43;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 28: if (NextChar == 'g' | NextChar == 'G') State = 44;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 29: if (NextChar == 'd' | NextChar == 'D') State = 45;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 30: if (NextChar == 's' | NextChar == 'S') State = 46;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 31: if (NextChar == 'e' | NextChar == 'E') State = 47;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 32: if (NextChar == 'i' | NextChar == 'I') State = 48;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 33: if (NextChar == ' ' | NextChar == '(' | NextChar == ')'
                               | NextChar == ',' | NextChar == ':' | NextChar == ';'
                               | NextChar == '[' | NextChar == ']' | NextChar == '.'
                               | NextChar == '+' | NextChar == '-' | NextChar == '='
                               | NextChar == '<' | NextChar == '>' | NextChar == '*'
                               | NextChar == '/' | NextChar == '\r')
                        {
                            LastChar = NextChar;
                            Length--;
                            NextWord[Length] = '\0';
                            return MakeToken(Symbol.S_do, BlockOrder, ColNo, NextWord, RowNi);
                        }
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 34: if (NextChar == 'v' | NextChar == 'V') State = 49;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 35: if (NextChar == 't' | NextChar == 'T') State = 50;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 36: if (NextChar == 'd' | NextChar == 'D') State = 51;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 37: if (NextChar == 'g' | NextChar == 'G') State = 52;
                        else if (NextChar == 'c' | NextChar == 'C') State = 53;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 38: if (NextChar == ' ' | NextChar == '(' | NextChar == ')'
                               | NextChar == ',' | NextChar == ':' | NextChar == ';'
                               | NextChar == '[' | NextChar == ']' | NextChar == '.'
                               | NextChar == '+' | NextChar == '-' | NextChar == '='
                               | NextChar == '<' | NextChar == '>' | NextChar == '*'
                               | NextChar == '/' | NextChar == '\r')
                        {
                            LastChar = NextChar;
                            Length--;
                            NextWord[Length] = '\0';
                            return MakeToken(Symbol.S_var, BlockOrder, ColNo, NextWord, RowNi);
                        }
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 39: if (NextChar == 'a' | NextChar == 'A') State = 54;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 40: if (NextChar == ' ' | NextChar == '(' | NextChar == ')'
                               | NextChar == ',' | NextChar == ':' | NextChar == ';'
                               | NextChar == '[' | NextChar == ']' | NextChar == '.'
                               | NextChar == '+' | NextChar == '-' | NextChar == '='
                               | NextChar == '<' | NextChar == '>' | NextChar == '*'
                               | NextChar == '/' | NextChar == '\r')
                        {
                            LastChar = NextChar;
                            Length--;
                            NextWord[Length] = '\0';
                            return MakeToken(Symbol.S_and, BlockOrder, ColNo, NextWord, RowNi);
                        }
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 41: if (NextChar == 'e' | NextChar == 'E') State = 55;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 42: if (NextChar == 'l' | NextChar == 'L') State = 56;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 43: if (NextChar == 'c' | NextChar == 'C') State = 57;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 44: if (NextChar == 'i' | NextChar == 'I') State = 58;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 45: if (NextChar == ' ' | NextChar == '(' | NextChar == ')'
                               | NextChar == ',' | NextChar == ':' | NextChar == ';'
                               | NextChar == '[' | NextChar == ']' | NextChar == '.'
                               | NextChar == '+' | NextChar == '-' | NextChar == '='
                               | NextChar == '<' | NextChar == '>' | NextChar == '*'
                               | NextChar == '/' | NextChar == '\r')
                        {
                            LastChar = NextChar;
                            Length--;
                            NextWord[Length] = '\0';
                            //BlockOrder--;
                            return MakeToken(Symbol.S_end, BlockOrder, ColNo, NextWord, RowNi);
                        }
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 46: if (NextChar == 'e' | NextChar == 'E') State = 59;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 47: if (NextChar == 'n' | NextChar == 'N') State = 60;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 48: if (NextChar == 'l' | NextChar == 'L') State = 61;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 49: if (NextChar == ' ' | NextChar == '(' | NextChar == ')'
                               | NextChar == ',' | NextChar == ':' | NextChar == ';'
                               | NextChar == '[' | NextChar == ']' | NextChar == '.'
                               | NextChar == '+' | NextChar == '-' | NextChar == '='
                               | NextChar == '<' | NextChar == '>' | NextChar == '*'
                               | NextChar == '/' | NextChar == '\r')
                        {
                            LastChar = NextChar;
                            Length--;
                            NextWord[Length] = '\0';
                            return MakeToken(Symbol.S_div, BlockOrder, ColNo, NextWord, RowNi);
                        }
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 50: if (NextChar == ' ' | NextChar == '(' | NextChar == ')'
                               | NextChar == ',' | NextChar == ':' | NextChar == ';'
                               | NextChar == '[' | NextChar == ']' | NextChar == '.'
                               | NextChar == '+' | NextChar == '-' | NextChar == '='
                               | NextChar == '<' | NextChar == '>' | NextChar == '*'
                               | NextChar == '/' | NextChar == '\r')
                        {
                            LastChar = NextChar;
                            Length--;
                            NextWord[Length] = '\0';
                            return MakeToken(Symbol.S_not, BlockOrder, ColNo, NextWord, RowNi);
                        }
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 51: if (NextChar == ' ' | NextChar == '(' | NextChar == ')'
                               | NextChar == ',' | NextChar == ':' | NextChar == ';'
                               | NextChar == '[' | NextChar == ']' | NextChar == '.'
                               | NextChar == '+' | NextChar == '-' | NextChar == '='
                               | NextChar == '<' | NextChar == '>' | NextChar == '*'
                               | NextChar == '/' | NextChar == '\r')
                        {
                            LastChar = NextChar;
                            Length--;
                            NextWord[Length] = '\0';
                            return MakeToken(Symbol.S_mod, BlockOrder, ColNo, NextWord, RowNi);
                        }
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 52: if (NextChar == 'r' | NextChar == 'R') State = 62;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 53: if (NextChar == 'e' | NextChar == 'E') State = 63;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 54: if (NextChar == 'y' | NextChar == 'Y') State = 64;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 55: if (NextChar == 'g' | NextChar == 'G') State = 65;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 56: if (NextChar == ' ' | NextChar == '(' | NextChar == ')'
                               | NextChar == ',' | NextChar == ':' | NextChar == ';'
                               | NextChar == '[' | NextChar == ']' | NextChar == '.'
                               | NextChar == '+' | NextChar == '-' | NextChar == '='
                               | NextChar == '<' | NextChar == '>' | NextChar == '*'
                               | NextChar == '/' | NextChar == '\r')
                        {
                            LastChar = NextChar;
                            Length--;
                            NextWord[Length] = '\0';
                            return MakeToken(Symbol.S_real, BlockOrder, ColNo, NextWord, RowNi);
                        }
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 57: if (NextChar == 't' | NextChar == 'T') State = 66;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 58: if (NextChar == 'n' | NextChar == 'N') State = 67;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 59: if (NextChar == ' ' | NextChar == '(' | NextChar == ')'
                               | NextChar == ',' | NextChar == ':' | NextChar == ';'
                               | NextChar == '[' | NextChar == ']' | NextChar == '.'
                               | NextChar == '+' | NextChar == '-' | NextChar == '='
                               | NextChar == '<' | NextChar == '>' | NextChar == '*'
                               | NextChar == '/' | NextChar == '\r')
                        {
                            LastChar = NextChar;
                            Length--;
                            NextWord[Length] = '\0';
                            return MakeToken(Symbol.S_else, BlockOrder, ColNo, NextWord, RowNi);
                        }
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 60: if (NextChar == ' ' | NextChar == '(' | NextChar == ')'
                               | NextChar == ',' | NextChar == ':' | NextChar == ';'
                               | NextChar == '[' | NextChar == ']' | NextChar == '.'
                               | NextChar == '+' | NextChar == '-' | NextChar == '='
                               | NextChar == '<' | NextChar == '>' | NextChar == '*'
                               | NextChar == '/' | NextChar == '\r')
                        {
                            LastChar = NextChar;
                            Length--;
                            NextWord[Length] = '\0';
                            return MakeToken(Symbol.S_then, BlockOrder, ColNo, NextWord, RowNi);
                        }
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 61: if (NextChar == 'e' | NextChar == 'E') State = 68;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 62: if (NextChar == 'a' | NextChar == 'A') State = 69;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 63: if (NextChar == 'd' | NextChar == 'D') State = 70;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 64: if (NextChar == ' ' | NextChar == '(' | NextChar == ')'
                               | NextChar == ',' | NextChar == ':' | NextChar == ';'
                               | NextChar == '[' | NextChar == ']' | NextChar == '.'
                               | NextChar == '+' | NextChar == '-' | NextChar == '='
                               | NextChar == '<' | NextChar == '>' | NextChar == '*'
                               | NextChar == '/' | NextChar == '\r')
                        {
                            LastChar = NextChar;
                            Length--;
                            NextWord[Length] = '\0';
                            return MakeToken(Symbol.S_array, BlockOrder, ColNo, NextWord, RowNi);
                        }
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 65: if (NextChar == 'e' | NextChar == 'E') State = 71;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 66: if (NextChar == 'i' | NextChar == 'I') State = 72;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 67: if (NextChar == ' ' | NextChar == '(' | NextChar == ')'
                               | NextChar == ',' | NextChar == ':' | NextChar == ';'
                               | NextChar == '[' | NextChar == ']' | NextChar == '.'
                               | NextChar == '+' | NextChar == '-' | NextChar == '='
                               | NextChar == '<' | NextChar == '>' | NextChar == '*'
                               | NextChar == '/' | NextChar == '\r')
                        {
                            LastChar = NextChar;
                            Length--;
                            NextWord[Length] = '\0';
                            return MakeToken(Symbol.S_begin, BlockOrder, ColNo, NextWord, RowNi);
                        }
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 68: if (NextChar == ' ' | NextChar == '(' | NextChar == ')'
                               | NextChar == ',' | NextChar == ':' | NextChar == ';'
                               | NextChar == '[' | NextChar == ']' | NextChar == '.'
                               | NextChar == '+' | NextChar == '-' | NextChar == '='
                               | NextChar == '<' | NextChar == '>' | NextChar == '*'
                               | NextChar == '/' | NextChar == '\r')
                        {
                            LastChar = NextChar;
                            Length--;
                            NextWord[Length] = '\0';
                            return MakeToken(Symbol.S_while, BlockOrder, ColNo, NextWord, RowNi);
                        }
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 69: if (NextChar == 'm' | NextChar == 'M') State = 73;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 70: if (NextChar == 'u' | NextChar == 'U') State = 74;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 71: if (NextChar == 'r' | NextChar == 'R') State = 75;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 72: if (NextChar == 'o' | NextChar == 'O') State = 76;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 73: if (NextChar == ' ' | NextChar == '(' | NextChar == ')'
                               | NextChar == ',' | NextChar == ':' | NextChar == ';'
                               | NextChar == '[' | NextChar == ']' | NextChar == '.'
                               | NextChar == '+' | NextChar == '-' | NextChar == '='
                               | NextChar == '<' | NextChar == '>' | NextChar == '*'
                               | NextChar == '/' | NextChar == '\r')
                        {
                            LastChar = NextChar;
                            Length--;
                            NextWord[Length] = '\0';
                            return MakeToken(Symbol.S_program, BlockOrder, ColNo, NextWord, RowNi);
                        }
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 74: if (NextChar == 'r' | NextChar == 'R') State = 77;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 75: if (NextChar == ' ' | NextChar == '(' | NextChar == ')'
                               | NextChar == ',' | NextChar == ':' | NextChar == ';'
                               | NextChar == '[' | NextChar == ']' | NextChar == '.'
                               | NextChar == '+' | NextChar == '-' | NextChar == '='
                               | NextChar == '<' | NextChar == '>' | NextChar == '*'
                               | NextChar == '/' | NextChar == '\r')
                        {
                            LastChar = NextChar;
                            Length--;
                            NextWord[Length] = '\0';
                            return MakeToken(Symbol.S_integer, BlockOrder, ColNo, NextWord, RowNi);
                        }
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 76: if (NextChar == 'n' | NextChar == 'N') State = 78;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 77: if (NextChar == 'e' | NextChar == 'E') State = 79;
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 78: if (NextChar == ' ' | NextChar == '(' | NextChar == ')'
                               | NextChar == ',' | NextChar == ':' | NextChar == ';'
                               | NextChar == '[' | NextChar == ']' | NextChar == '.'
                               | NextChar == '+' | NextChar == '-' | NextChar == '='
                               | NextChar == '<' | NextChar == '>' | NextChar == '*'
                               | NextChar == '/' | NextChar == '\r')
                        {
                            LastChar = NextChar;
                            Length--;
                            NextWord[Length] = '\0';
                            BlockOrder++;
                            return MakeToken(Symbol.S_function, BlockOrder, ColNo, NextWord, RowNi);
                        }
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 79: if (NextChar == ' ' | NextChar == '(' | NextChar == ')'
                               | NextChar == ',' | NextChar == ':' | NextChar == ';'
                               | NextChar == '[' | NextChar == ']' | NextChar == '.'
                               | NextChar == '+' | NextChar == '-' | NextChar == '='
                               | NextChar == '<' | NextChar == '>' | NextChar == '*'
                               | NextChar == '/' | NextChar == '\r')
                        {
                            LastChar = NextChar;
                            Length--;
                            NextWord[Length] = '\0';
                            BlockOrder++;
                            return MakeToken(Symbol.S_procedure, BlockOrder, ColNo, NextWord, RowNi);
                        }
                        else
                        {
                            State = 17;
                            LastChar = NextChar;
                            Length--;
                        }
                        break;
                    case 80: if (NextChar <= '9' && NextChar >= '0') State = 80;
                        else if (NextChar == '.') State = 81;
                        else
                        {
                            LastChar = NextChar;
                            Length--;
                            NextWord[Length] = '\0';
                            return MakeToken(Symbol.S_digit, BlockOrder, ColNo, NextWord, RowNi);
                        }
                        break;
                    case 81: if (NextChar <= '9' && NextChar >= '0') State = 81;
                        else
                        {
                            LastChar = NextChar;
                            Length--;
                            NextWord[Length] = '\0';
                            return MakeToken(Symbol.S_digit, BlockOrder, ColNo, NextWord, RowNi);
                        }
                        break;
                    case 82: LastChar = NextChar;
                        Length--;
                        NextWord[Length] = '\0';
                        return MakeToken(Symbol.S_popen, BlockOrder, ColNo, NextWord, RowNi);
                    //break;
                    case 83: LastChar = NextChar;
                        Length--;
                        NextWord[Length] = '\0';
                        return MakeToken(Symbol.S_pclose, BlockOrder, ColNo, NextWord, RowNi);
                    //break;
                    case 84: LastChar = NextChar;
                        Length--;
                        NextWord[Length] = '\0';
                        return MakeToken(Symbol.S_cama, BlockOrder, ColNo, NextWord, RowNi);
                    //break;
                    case 85: if (NextChar != '=')
                        {
                            LastChar = NextChar;
                            Length--;
                            NextWord[Length] = '\0';
                            return MakeToken(Symbol.S_ddot, BlockOrder, ColNo, NextWord, RowNi);
                        }
                        else return MakeToken(Symbol.S_assign, BlockOrder, ColNo, NextWord, RowNi);
                    //break;
                    case 86: LastChar = NextChar;
                        Length--;
                        NextWord[Length] = '\0';
                        return MakeToken(Symbol.S_semicolon, BlockOrder, ColNo, NextWord, RowNi);
                    //break;
                    case 87: LastChar = NextChar;
                        Length--;
                        NextWord[Length] = '\0';
                        return MakeToken(Symbol.S_brakopen, BlockOrder, ColNo, NextWord, RowNi);
                    //break;
                    case 88: LastChar = NextChar;
                        Length--;
                        NextWord[Length] = '\0';
                        return MakeToken(Symbol.S_brakclose, BlockOrder, ColNo, NextWord, RowNi);
                    //break;
                    case 89: if (NextChar == '.') return MakeToken(Symbol.S_twodot, BlockOrder, ColNo, NextWord, RowNi);
                        else
                        {
                            LastChar = NextChar;
                            Length--;
                            NextWord[Length] = '\0';
                            return MakeToken(Symbol.S_dot, BlockOrder, ColNo, NextWord, RowNi);
                        }
                    //break;
                    case 90: LastChar = NextChar;
                        Length--;
                        NextWord[Length] = '\0';
                        return MakeToken(Symbol.S_add, BlockOrder, ColNo, NextWord, RowNi);
                    //break;
                    case 91: LastChar = NextChar;
                        Length--;
                        NextWord[Length] = '\0';
                        return MakeToken(Symbol.S_sub, BlockOrder, ColNo, NextWord, RowNi);
                    //break;
                    case 92: LastChar = NextChar;
                        Length--;
                        NextWord[Length] = '\0';
                        return MakeToken(Symbol.S_equal, BlockOrder, ColNo, NextWord, RowNi);
                    //break;
                    case 93: if (NextChar == '>') return MakeToken(Symbol.S_notequal, BlockOrder, ColNo, NextWord, RowNi);
                        else if (NextChar == '=') return MakeToken(Symbol.S_seq, BlockOrder, ColNo, NextWord, RowNi);
                        else
                        {
                            LastChar = NextChar;
                            Length--;
                            NextWord[Length] = '\0';
                            return MakeToken(Symbol.S_shorter, BlockOrder, ColNo, NextWord, RowNi);
                        }
                    //break;
                    case 94: if (NextChar == '=') return MakeToken(Symbol.S_geq, BlockOrder, ColNo, NextWord, RowNi);
                        else
                        {
                            LastChar = NextChar;
                            Length--;
                            NextWord[Length] = '\0';
                            return MakeToken(Symbol.S_greater, BlockOrder, ColNo, NextWord, RowNi);
                        }
                    //break;
                    case 95: LastChar = NextChar;
                        Length--;
                        NextWord[Length] = '\0';
                        return MakeToken(Symbol.S_mul, BlockOrder, ColNo, NextWord, RowNi);
                    //break;
                    case 96: LastChar = NextChar;
                        Length--;
                        NextWord[Length] = '\0';
                        return MakeToken(Symbol.S_realdiv, BlockOrder, ColNo, NextWord, RowNi);
                    //break;
                }
            }
            if (LastChar == '\0') return MakeToken(Symbol.S_finallprogram, BlockOrder, ColNo, NextWord, RowNi);
            else if ((LastChar <= 'z' && LastChar >= 'a') ||
                                (LastChar <= 'Z' && LastChar >= 'A')) return MakeToken(Symbol.S_id, BlockOrder, ColNo, NextWord, RowNi);
            else if (LastChar <= '9' && LastChar >= '0') return MakeToken(Symbol.S_digit, BlockOrder, ColNo, NextWord, RowNi);
            else if (LastChar == '(') return MakeToken(Symbol.S_popen, BlockOrder, ColNo, NextWord, RowNi);
            else if (LastChar == ')') return MakeToken(Symbol.S_pclose, BlockOrder, ColNo, NextWord, RowNi);
            else if (LastChar == ',') return MakeToken(Symbol.S_cama, BlockOrder, ColNo, NextWord, RowNi);
            else if (LastChar == ':') return MakeToken(Symbol.S_ddot, BlockOrder, ColNo, NextWord, RowNi);
            else if (LastChar == ';') return MakeToken(Symbol.S_semicolon, BlockOrder, ColNo, NextWord, RowNi);
            else if (LastChar == '[') return MakeToken(Symbol.S_brakopen, BlockOrder, ColNo, NextWord, RowNi);
            else if (LastChar == ']') return MakeToken(Symbol.S_brakclose, BlockOrder, ColNo, NextWord, RowNi);
            else if (LastChar == '.') return MakeToken(Symbol.S_dot, BlockOrder, ColNo, NextWord, RowNi);
            else if (LastChar == '+') return MakeToken(Symbol.S_add, BlockOrder, ColNo, NextWord, RowNi);
            else if (LastChar == '-') return MakeToken(Symbol.S_sub, BlockOrder, ColNo, NextWord, RowNi);
            else if (LastChar == '=') return MakeToken(Symbol.S_equal, BlockOrder, ColNo, NextWord, RowNi);
            else if (LastChar == '<') return MakeToken(Symbol.S_shorter, BlockOrder, ColNo, NextWord, RowNi);
            else if (LastChar == '>') return MakeToken(Symbol.S_greater, BlockOrder, ColNo, NextWord, RowNi);
            else if (LastChar == '*') return MakeToken(Symbol.S_mul, BlockOrder, ColNo, NextWord, RowNi);
            else if (LastChar == '/') return MakeToken(Symbol.S_realdiv, BlockOrder, ColNo, NextWord, RowNi);
            else
            {
                error(RowNi, ColNo);
                return MakeToken(Symbol.S_finallprogram, BlockOrder, ColNo, NextWord, RowNi);
            }


            //throw new System.NotImplementedException();
        }
 
        public Token MakeToken(Symbol type, int blkord, int col, Char[] name, int row)
        {
            Token tk = new Token();
            tk.BLKORD = blkord;
            tk.COL = col - 1;
            tk.Name = name;
            tk.ROW = row;
            tk.Type = type;
            return tk;
            //throw new System.NotImplementedException();
        }

        public void error(int row, int col)
        {
            int col2;
            col2 = col - 1;
            fe.Visible = true;
            fe.textBox2.Text = fe.textBox2.Text + "\r\n" + "Lexical Error in row :" + row + " col :" + col2;
            //MessageBox.Show("Lexical Error in row :" + row + " col :" + col2);
            //throw new System.NotImplementedException();
        }
 
    }
}
