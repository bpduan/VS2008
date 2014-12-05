using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPattern._12Proxy
{
    //继承的方式
    public class Tank2 :Tank
    {
        public override void Move()
        {
            Console.WriteLine("Tank2 运行");
            DateTime t1 = DateTime.Now;
            base.Move();
            Console.WriteLine(DateTime.Now - t1);
        }
    }
}
