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
            if (impactPrefab != null)
            {
                GameObject impact = Instantiate(impactPrefab, hit.point + hit.normal * 0.01f, Quaternion.LookRotation(hit.normal));
                Destroy(impact, impactLifetime);
            }

            EnemyHitReactSimple react = hit.collider.GetComponentInParent<EnemyHitReactSimple>();
            if (react != null)
            {
                react.HitReact();
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
