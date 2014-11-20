using System;
namespace _19_Observer
{
    public interface IWakenUpListener
    {
        void ActionToWakeUp(WakenUpEvent e);
    }
}
