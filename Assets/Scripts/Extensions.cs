using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static float Normalize(this float val)
    {
        if (val < 0f)
        {
            return -1f;
        }
        else if (val > 0f)
        {
            return 1f;
        }

        return 0;
    }

    public static void SetAllActive(this IEnumerable<GameObject> items, bool isActive = true)
    {
        foreach (var item in items)
        {
            item.SetActive(isActive);
        }
    }
}
