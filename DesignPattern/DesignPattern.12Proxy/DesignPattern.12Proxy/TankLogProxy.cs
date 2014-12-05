using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPattern._12Proxy
{
    public class TankLogProxy:IMoveable
    {
        public void Move()
        {
            Console.WriteLine("日志代理：Begining");
            IMoveable obj =new Tank();
            obj.Move();
            Console.WriteLine("日志代理：End");
        }
    }
}
