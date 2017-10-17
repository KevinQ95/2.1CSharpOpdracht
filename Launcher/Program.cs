using ClientApplicatie;
using ServerApplicatie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ClientForm;
using System.Windows.Forms;


namespace Launcher
{
    class Program
    {
        static void Main(string[] args)
        {
            new Thread(() => new Server()).Start();

            Application.Run(new MultiFormContext(new Form1(), new Form1()));

        }

        public Program()
        {
           
        }
    }

    public class MultiFormContext : ApplicationContext
    {
        private int openForms;
        public MultiFormContext(params Form[] forms)
        {
            openForms = forms.Length;

            foreach (var form in forms)
            {
                form.FormClosed += (s, args) =>
                {
                    //When we have closed the last of the "starting" forms, 
                    //end the program.
                    if (Interlocked.Decrement(ref openForms) == 0)
                        ExitThread();
                };

                form.Show();
            }
        }
    }
}
