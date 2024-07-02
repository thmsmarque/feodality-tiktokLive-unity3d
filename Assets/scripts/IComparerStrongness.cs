using System;
using System.Collections.Generic;

public class IComparerStrongness : IComparer<VillagerScript>
{
    public int Compare(VillagerScript x, VillagerScript y)
    {
        if (x.getStrongness() == y.getStrongness())
        {
            return 0;
        }
        else
        {
            if (x.getStrongness() > y.getStrongness())
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

    }
}