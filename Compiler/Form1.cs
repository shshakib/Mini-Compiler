using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;

namespace Compiler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InText = textBox1.Text;
            pointer = -1;
        }

        //public Form2 fe = new Form2();
        bool dirty = false;
        string fname = "";
        private String InText;
        private int pointer;
        private Char LastChar = '\0';
        private int RowNi = 0;
        private int ColNo = 0;

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Cut();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (File.Exists(openFileDialog1.FileName))
            {
                fname = openFileDialog1.FileName;
                StreamReader sr = new StreamReader(fname);
                textBox1.Text = sr.ReadToEnd();
                dirty = false;
                sr.Close();
                Compiler.Form1.ActiveForm.Text = "Compiler - " + fname;
            }	
        }

        private void savedata()
        {
            if (fname == "")
            {
                //saveFileDialog1.Filter = "Text Files|*.txt";
                DialogResult res = saveFileDialog1.ShowDialog();
                if (res == DialogResult.Cancel)
                {
                    return;
                }
                fname = saveFileDialog1.FileName;
                MessageBox.Show(fname);
            }
            StreamWriter sw = new StreamWriter(fname);
            sw.WriteLine(textBox1.Text);
            sw.Flush();
            sw.Close();
            dirty = false;
            Compiler.Form1.ActiveForm.Text = "Compiler - " + fname;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savedata();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            fname = saveFileDialog1.FileName;
            savedata();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDocument pd1 = new PrintDocument();
            pd1.DocumentName = fname;
            printDialog1.Document = pd1;
            printDialog1.AllowSomePages = true;
            printDialog1.AllowPrintToFile = true;
            printDialog1.ShowDialog();
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //PrintDocument pd1 = new PrintDocument();
            //pd1.DocumentName = fname;
            //printPreviewDialog1.Document = pd1;
            //printPreviewDialog1.ShowDialog();
        }

        private void checkdirty()
        {
            if (dirty)
            {
                DialogResult click = MessageBox.Show(this, "Do You wish to save this Document?", "Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (click == DialogResult.Yes)
                {
                    savedata();
                    textBox1.Text = "";
                    fname = "";
                    dirty = false;
                }
                if (click == DialogResult.No)
                {
                    textBox1.Text = "";
                    fname = "";
                    dirty = false;
                }
            }
            else
            {
                textBox1.Text = "";
                fname = "";
            }
        }
		
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkdirty();
            this.Dispose();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkdirty();
            Compiler.Form1.ActiveForm.Text = "Compiler - NEW CAMPILER.CMP";
        }

        //private void cutToolStripMenuItem_Click_1(object sender, EventArgs e)
        //{
            //textBox1.Cut();
            //dirty = true;
        //} 

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Paste();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {

            fontDialog1.ShowColor = true;
            fontDialog1.ShowDialog();
            textBox1.Font = fontDialog1.Font;
            textBox1.ForeColor = fontDialog1.Color;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about nf = new about();
            nf.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //int i=0;
            dirty = true;
            //textBox1.GetPositionFromCharIndex(i);
            //toolStripStatusLabel1.Text = i.ToString();
        }
        Enum Stop;
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Lexical temp = new Lexical(textBox1.Text);
            Syntax_Semantic s_s = new Syntax_Semantic(temp);

            //InText = textBox1.Text;
            //pointer = -1;
            
            
            s_s.Program(Stop);
            
            //Token tok;
            //int i=0;
            //while (pointer < InText.Length)
            //{
            //    i=0;
            //    tok = lexer();
            //    label10.Text = tok.COL.ToString();
            //    label9.Text = tok.ROW.ToString();
            //    label8.Text = tok.Type.ToString();
            //    label7.Text = tok.BLKORD.ToString();
            //    label6.Text = "";
            //    while (i < tok.Name.Length)
            //    {
            //        label6.Text += tok.Name[i];
            //        i++;
            //    }
            //    MessageBox.Show("next");
            //}
        }


        //public Symbol IsKeyWord(Char[] Key)
        //{
        //    //Key = new Char[20];
        //    int i = 0;
        //    Symbol KeyTable;
        //    for (i = 0; KeyTable[i] && Key == KeyTable; i++) ;
        //    if (KeyTable[i]) return KeyTable[i];
        //    //throw new System.NotImplementedException();
        //}

        //public Form1 global1 = new Form1();
    }
}