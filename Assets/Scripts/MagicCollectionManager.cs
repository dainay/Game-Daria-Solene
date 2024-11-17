using System.Collections.Generic;
using UnityEngine;

public static class MagicCollectionManager
{
    private static List<string> collectedTags = new List<string>();

    public static void AddTag(string tag)
    {
        if (!collectedTags.Contains(tag))
        {
            collectedTags.Add(tag);
            Debug.Log("Collected tag: " + tag);

            // Log the entire list of tages of colors
            Debug.Log("Current Collected Tags: " + string.Join(", ", collectedTags));
        }
    }

    public static List<string> GetCollectedTags()
    {
        return collectedTags;
    }

  
}
