using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MyThread
{
    public class Children:MyThread
    {
        public override void Run()
        {
            Thread.Sleep(5000);
            Console.WriteLine("小孩醒过来");
        }
    }
}
