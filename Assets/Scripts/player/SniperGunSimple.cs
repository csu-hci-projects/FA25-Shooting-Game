using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class SniperGunSimple : MonoBehaviour
{
    [Header("Required")]
    [SerializeField] public Camera playerCamera;
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


    private float nextFireTime = 0f;

    // Input System
    private PlayerInput playerInput;
    private InputAction fireAction;

    void Awake()
    {
        
    }

    void Update()
    {
        if (playerCamera == null) return;

        if (shotChanceText != null)
        shotChanceText.text = $"Shot Chance: {(shotChance * 100f).ToString("F0")}%";

        // Left mouse click using new Input System
        if (Mouse.current.leftButton.wasPressedThisFrame && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireCooldown;
            Shoot();
        }
    }

    void Shoot()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {

            EnemyHitReactSimple react = hit.collider.GetComponentInParent<EnemyHitReactSimple>();
            if (react != null)
            {
                bool actualHit = Random.value <= shotChance;

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
}
