﻿using ClientApplicatie;
using ServerApplicatie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launcher
{
    class Program
    {
        static void Main(string[] args)
        {
            new Server();
            new Client();
            new Client();
            Console.ReadLine();
        }
    }
}
