using System;
using System.Collections.Generic;

public class IComparerEfficacity : IComparer<VillagerScript>
{
    public int Compare(VillagerScript x, VillagerScript y)
    {
        if(x.getEfficacity() == y.getEfficacity())
        {
            return 0;
        }else
        {
            if(x.getEfficacity() > y.getEfficacity())
            {
                return 1;
            }else
            {
                return -1;
            }
        }
        
    }
}