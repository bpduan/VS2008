using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPattern._12Proxy
{
    //聚合的方法
    class TankTimeProxy:IMoveable
    {
        public void Move()
        {
            Tank tank1 = new Tank();
            Console.WriteLine("时间代理:Begining");
            DateTime t1 = DateTime.Now;
            tank1.Move();
            Console.WriteLine("时间代理:End  用时："+(DateTime.Now - t1));
        }
    }
}
