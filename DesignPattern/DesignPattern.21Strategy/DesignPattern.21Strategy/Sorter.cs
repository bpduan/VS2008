using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPattern._21Strategy
{
    public class Sorter
    {
        public static void Sort(int[] a)
        {
            for (int i = a.Length; i > 0;i-- )
            {
                for (int j = 0; j < i - 1;j++ )
                {
                    if (a[j]>a[j+1])
                    {
                        swap(a, j, j + 1);
                    }
                }
            }
        }

        private static void swap(int[] a, int x, int y)
        {
            int temp = a[x];
            a[x] = a[y];
            a[y] = temp;
        }

        public static void Print(int[] a)
        {
            foreach (var m in a)
            {
                Console.Write(m+" ");
            }
        }

        internal static void Sort(Cat[] a)
        {
            for (int i = a.Length; i > 0; i--)
            {
                for (int j = 0; j < i - 1; j++)
                {
                    if (a[j].Height > a[j + 1].Height)
                    {
                        swap(a, j, j + 1);
                    }
                }
            }
            
        }

        private static void swap(Cat[] a, int x, int y)
        {
            Cat temp = a[x];
            a[x] = a[y];
            a[y] = temp;
        }

        internal static void Print(Cat[] cats)
        {
            for (int i = 0; i < cats.Length;i++ )
            {
                Console.Write(cats[i]);
            }
            Console.WriteLine();
        }
    }
}
