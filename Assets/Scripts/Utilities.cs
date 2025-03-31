using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public static class Utilities
{
    // Fade function for images that manipulates the alpha value
    public static IEnumerator FadeImage(Image image, float startAlpha, float endAlpha, float duration)
    {
        Color color = image.color;
        color.a = startAlpha;
        image.color = color;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            image.color = color;
            yield return null;
        }

        color.a = endAlpha;
        image.color = color;
    }

    // Function to find a child whether active or inactive
    // by hierarchy name (transform.Find) or child name (recursive search)
    public static Transform FindChild(string childName)
    {
        // Requires the parent to be an active object in order to work
        Transform parent = GameObject.Find("MainView")?.transform;
        if (parent == null)
        {
            Debug.LogError("MainView is null");
            return null;
        }

        // Check if childName is a hierarchy path or a child name
        return (childName.Contains("/")) ? parent.Find(childName) : FindChildRecursive(parent, childName);
    }

    // Recursive function to find a child by name
    private static Transform FindChildRecursive(Transform parent, string childName)
    {
        foreach (Transform child in parent)
        {
            if (child.name == childName)
                return child;

            Transform found = FindChildRecursive(child, childName);
            if (found != null)
                return found;
        }

        return null;
    }
}