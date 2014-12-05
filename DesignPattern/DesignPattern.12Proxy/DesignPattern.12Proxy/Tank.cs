using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DesignPattern._12Proxy
{
    public class Tank : DesignPattern._12Proxy.IMoveable
    {
        virtual public void Move()
        {
            Console.WriteLine("Tank1:坦克正在移动中....");
            Thread.Sleep( new Random().Next(10000));
        }
    }
}
