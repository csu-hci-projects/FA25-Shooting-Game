using UnityEngine;
using System.Collections;

public class EnemyHitReactSimple : MonoBehaviour
{
    public Color hitColor = Color.red;
    public float flashDuration = 0.3f;

    Renderer[] rends;
    Color[] originalColors;

    void Awake()
    {
        // gather all renderers on this object and children so whole enemy flashes
        rends = GetComponentsInChildren<Renderer>();

        // create instance materials to avoid changing shared materials at runtime
        originalColors = new Color[rends.Length];
        for (int i = 0; i < rends.Length; i++)
        {
            // make a unique material instance so we don't recolor the shared material
            Material inst = new Material(rends[i].material);
            rends[i].material = inst;
            originalColors[i] = inst.color;
        }
    }

    // Public method sniper script calls
    public void HitReact()
    {
        StopAllCoroutines();
        StartCoroutine(FlashCoroutine());
    }

    IEnumerator FlashCoroutine()
    {
        // set hit color
        for (int i = 0; i < rends.Length; i++)
        {
            if (rends[i] != null)
                rends[i].material.color = hitColor;
        }

        yield return new WaitForSeconds(flashDuration);

        // revert to original
        for (int i = 0; i < rends.Length; i++)
        {
            if (rends[i] != null)
                rends[i].material.color = originalColors[i];
        }
    }
}
