using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPattern._21Strategy
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] nn = { 5, 4, 8, 6, 9, 10 };

            Sorter.Sort(nn);
            Sorter.Print(nn);

            Console.WriteLine("");

            Cat[] cats = { new Cat{Weight=5,Height=5}, 
                            new Cat{Weight=3,Height=3},
                            new Cat{Weight=1,Height=1}
                        };
            Sorter.Sort(cats);
            Sorter.Print(cats);


            Console.ReadLine();

        }
    }
}
