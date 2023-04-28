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

    public static float DistanceEx(this BoxCollider2D coll1, CircleCollider2D coll2) => Vector2.Distance(coll1.transform.position, coll2.transform.position);
}
