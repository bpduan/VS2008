using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyThread
{
    class Program
    {
        static void Main(string[] args)
        {
            Children c = new Children();
            c.Start();
            Console.ReadLine();
        }
    }
}
