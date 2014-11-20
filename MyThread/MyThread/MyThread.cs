using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MyThread
{
    public abstract class MyThread
    {
        Thread _thread = null;
        public abstract void Run();
        public void Start()
        {
            if (_thread==null)
            {
                _thread = new Thread(Run);
            }
            _thread.Start();
        }
    }
}
