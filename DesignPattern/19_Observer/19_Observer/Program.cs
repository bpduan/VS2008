using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _19_Observer
{
    class Program
    {
        static void Main(string[] args)
        {
            Dad dad=new Dad();
            GrandFather grandfather = new GrandFather();

            Children child = new Children();
            child.AddWakenUplistener(dad);
            child.AddWakenUplistener(grandfather);
            child.Start();

            Console.ReadLine();
        }
    }
}
