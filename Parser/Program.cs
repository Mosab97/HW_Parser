using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser parser = new Parser(@"D:\file.txt");
            parser.expr();
            //0Console.WriteLine(text);
    }
    }
    class Parser
    {

        public StreamWriter w { get; set; }

        public static char lookahead { get; set; }
        public int count { get; set; } = 0;
        public string text { get; set; }

        public Parser(string path)
        {
            w= new StreamWriter(@"D:\fileWrite.txt");


            text = File.ReadAllText(path);
            lookahead = text[0];
            //w.Write("hi there");
            //w.Close();
        }
        public void expr()
        {
            term();
            while (true)
            {
                if (lookahead == '+')
                {
                    match('+'); term();w.Write('+');
                }
                else if (lookahead == '-')
                {
                    match('-'); term(); w.Write('-');
                }
                else
                {
                    w.Close();
                    return;
                }

            }
        }
       


        void term()
        {
            factor(lookahead);
            R2();
        

        }
        void R2()
        {
            if (lookahead == '*')
            {
                match('*'); factor(lookahead); w.Write('*');
            }
            else if (lookahead == '/')
            {
                match('/'); term(); w.Write('/');
            }
            else
            {
                return;
            }
        }

        private void factor(char t)
        {
            if (t == '(') expr();
            num();
        }
        void num()
        {

            if (Char.IsDigit(lookahead))
            {
                w.Write(lookahead); match(lookahead);
            }
            else
            {
                Console.WriteLine("Syntax Error");
                w.Close();
            }
        }

        void match(char t)
        {
            if (lookahead == t)
            {
                if(count < text.Length-1) count++;

                lookahead = text[count];
            }
            else
            {
                Console.WriteLine("Syntax Error");
                w.Close();
            }

        }
    }


}
