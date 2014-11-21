using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _0_ThinkingOO
{
    public class Driver
    {
       public  string Name { get; set; }
         
       public void Drive( IVehicle v )
       {
           v.Go(new Address { Name="东北"});
       }
    }
}
