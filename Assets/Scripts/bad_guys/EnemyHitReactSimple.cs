using UnityEngine;
using System.Collections;
using TMPro; // only if using TextMeshPro

public class EnemyHitReactSimple : MonoBehaviour
{
    public Color hitColor = Color.red;
    public Color missColor = Color.blue;
    public float flashDuration = 0.3f;

    public float textHeight = 2f;
    public float textLifetime = 1.0f;

    public bool boss = false;

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

       
        //no actions left means you cant hit anything
        isHit= Admin.Action()?isHit:false;

        

        StopAllCoroutines();
        StartCoroutine(FlashCoroutine(isHit));
        SpawnFloatingText(isHit ? "HIT!" : "MISS!", isHit);
        //kill enemy

        if (isHit)
        {
            //Admin._ene_killed++;
            //Debug.Log("Enemy killed "+Admin._ene_killed);
            Admin._ply_actions =   Admin._ply_actions+11;
            Debug.Log("updated actions"+Admin._ply_actions);

            // If this is the boss, set health to 0
            if (transform.parent.name == "Main_Enemy")
            {
                Admin.Players_health[1] = 0;
                Debug.Log("Boss hit! Health set to 0");
            }

            Admin.enemys.Remove(transform.parent.gameObject);
            Destroy(transform.parent.gameObject);
        }
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
        GameObject go = new GameObject("HitText");
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
