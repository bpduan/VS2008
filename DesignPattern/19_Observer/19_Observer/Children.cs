using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace _19_Observer
{
    public class Children:MyThread
    {
        private List<IWakenUpListener> _listener = new List<IWakenUpListener>();

        public void AddWakenUplistener(IWakenUpListener  L)
        {
            _listener.Add(L);
        }

        void WakeUp()
        {
            foreach (IWakenUpListener L in _listener)
            {
                L.ActionToWakeUp(new WakenUpEvent
                {
                    Time = DateTime.Now,
                    Location = "����",
                    Source = this
                });
            }
        }
        public override void Run()
        {
            Thread.Sleep(5000);
            Console.WriteLine("�ѹ���");
            WakeUp(); //���ѹ��������顣 
        }
    }
}

