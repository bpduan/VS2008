using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _19_Observer
{
    public class GrandFather : IWakenUpListener
    {
        public void ActionToWakeUp(WakenUpEvent e)
        {
            Console.WriteLine("爷爷：孩子在" + e.Location + "醒了过来，赶快过去【抱抱】他,醒过来的时间是" + e.Time);
        }
    }
}
