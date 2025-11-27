using UnityEngine;
using System.Collections;
using TMPro;
using NUnit.Framework; // only if using TextMeshPro

public class endturn : MonoBehaviour
{
    public Color hitColor = Color.red;
    public Color missColor = Color.blue;
    public float flashDuration = 0.3f;

    public float textHeight = 2f;
    public float textLifetime = 1.0f;

    public bool endturn_triggered = false;
    public bool endturn_confirmed = false;

    Renderer[] rends;
    Color[] originalColors;

    void Awake()
    {
        rends = GetComponentsInChildren<Renderer>();

        originalColors = new Color[rends.Length];
        for (int i = 0; i < rends.Length; i++)
        {
            Material inst = new Material(rends[i].material);
            rends[i].material = inst;
            originalColors[i] = inst.color;
        }
    }

    // now takes bool
    public void HitReact(bool isHit)
    {
        isHit= endturn_triggered? false : true;
        StopAllCoroutines();
        StartCoroutine(FlashCoroutine(isHit));
        SpawnFloatingText(isHit ? "End turn? Shoot again to confirm" : "Confirm!", isHit);
        endturn_confirmed = endturn_triggered&&(!isHit) ? true : false;
        Debug.Log($"End turn confirmed: {endturn_confirmed} and triggered: {endturn_triggered} and isHit: {isHit}");
    
    }

    IEnumerator FlashCoroutine(bool isHit)
    {
        Color useColor = isHit ? hitColor : missColor;

        // set colored flash
        for (int i = 0; i < rends.Length; i++)
            if (rends[i] != null)
                rends[i].material.color = useColor;

        yield return new WaitForSeconds(flashDuration);

        // revert color
        for (int i = 0; i < rends.Length; i++)
            if (rends[i] != null)
                rends[i].material.color = originalColors[i];
    }

    void SpawnFloatingText(string msg, bool isHit)
    {
        // Create a simple 3D TextMesh
        GameObject go = new GameObject("EndTurn");
        go.transform.position = transform.position + Vector3.up * textHeight;

        TextMesh tm = go.AddComponent<TextMesh>();
        tm.text = msg;
        tm.fontSize = 64;
        tm.characterSize = 0.1f;
        tm.color = isHit ? Color.red : Color.blue;
        tm.alignment = TextAlignment.Center;

        // face the player always
        go.transform.forward = Camera.main.transform.forward;

        Destroy(go, textLifetime);
    }


}
