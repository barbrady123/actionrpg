using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

    public static T ChooseRandomElement<T>(this IEnumerable<T> items) => items.Skip(Random.Range(0, items.Count())).First();

    public static void SetAlpha(this Image img, float alpha) => img.color = new Color(img.color.r, img.color.g, img.color.b, alpha);

    public static bool IsPlayerTag(this string val) => val == Global.Tags.Player;

    public static bool IsEnemyTag(this string val) => val == Global.Tags.Enemy;

    public static bool Intersects2d(this Collider2D val, CircleCollider2D other)
    {
        /*
        var obj1BoundsMin = new Vector3(val.center.x - (val.size.x / 2), val.center.y - (val.size.y / 2));
        var obj1BoundsMax = new Vector3(val.center.x + (val.size.x / 2), val.center.y + (val.size.x / 2));
        var obj2BoundsMin = new Vector3(other.offset.x - (other.size.x / 2), other.offset.y - (other.size.y / 2));
        var obj2BoundsMax = new Vector3(other.offset.x + (other.size.x / 2), other.offset.y + (other.size.x / 2));

        return obj1BoundsMin.x <= obj2BoundsMax.x && obj1BoundsMax.x >= obj2BoundsMin.x &&
               obj1BoundsMin.y <= obj2BoundsMax.y && obj1BoundsMax.y >= obj2BoundsMin.y;
        */
        return true;
    }
}
