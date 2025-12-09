using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Globalization;
using System;

public class EnemeyGunSimple : MonoBehaviour
{
    [Header("Required")]
    [SerializeField] public GameObject player;
      [SerializeField] public Transform player_transform;
    [SerializeField] public Transform muzzle;
    [SerializeField] public GameObject impactPrefab;

    [Header("Settings")]
    [SerializeField] public float range = 200f;
    [SerializeField] public float fireCooldown = 0.8f;
    [SerializeField] public float muzzleFlashDuration = 0.06f;
    [SerializeField] public float impactLifetime = 5f;

    [Header("UI")]
    [SerializeField] private TMPro.TextMeshProUGUI shotChanceText;
    [SerializeField, Range(0f, 1f)] private float shotChance = 0.01f; // 1% default

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip fireSound;

   


    private float nextFireTime = 0f;

    // Input System
    private PlayerInput playerInput;
    private InputAction fireAction;



    void Awake()
    {
        muzzle = this.transform;
    }
    void OnEnable()
    {
        if (player == null) return;

       Debug.Log("im enabled and shooting");

        shotChance = cal_shotChance();

         Debug.Log("shoot c "+shotChance);


        // Left mouse click using new Input System
     
     
            Shoot();
        
           this.enabled = false;
        
    }

    void Update()
    {
    

    }

    void Shoot()
    {
        if (audioSource != null && fireSound != null)
            audioSource.PlayOneShot(fireSound);
        Vector3 direction = (player_transform.position - transform.position).normalized;
        Ray ray = new Ray(transform.position, direction);
        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            //Debug.Log($"SniperGunSimple: Hit {hit.collider.gameObject.name} at {hit.point}");
            PlayerHitReactSimple react = hit.collider.GetComponentInParent<PlayerHitReactSimple>();



            if (react != null)
            {
                bool actualHit = UnityEngine.Random.value <=shotChance;

                if (actualHit)
                {
                    if (impactPrefab != null)
                    {
                        GameObject impact = Instantiate(impactPrefab, hit.point + hit.normal * 0.01f, Quaternion.LookRotation(hit.normal));
                        Destroy(impact, impactLifetime);
                    }
                    react.HitReact(true);  // red flash + HIT text
                }
                else
                {
                    react.HitReact(false); // blue flash + MISS text
                }
            }
        }

        if (muzzle != null)
            StartCoroutine(TemporaryMuzzleLight(muzzle.position, muzzleFlashDuration));

        Debug.DrawRay(ray.origin, ray.direction * range, Color.yellow, 0.5f);
    }

    IEnumerator TemporaryMuzzleLight(Vector3 position, float duration)
    {
        GameObject go = new GameObject("MuzzleFlashLight");
        go.transform.position = position;
        Light l = go.AddComponent<Light>();
        l.type = LightType.Point;
        l.range = 5f;
        l.intensity = 5f;

        yield return new WaitForSeconds(duration);

        Destroy(go);
    }
        float cal_shotChance()
    {
        //for now this is just the base number of map size
        int base_num = 253;
          float win_con = 0.1f;
        shotChance = (float)(Admin.Players_scores[1]*.001) / (base_num*win_con*Admin.enemys.Count);

        return shotChance;
    }
}
