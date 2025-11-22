using UnityEngine;
using UnityEngine.InputSystem;

public class Scope : MonoBehaviour
{
    public Animator animator;
    private bool isScoped = false;
    public GameObject scopeOverlay;
    public GameObject weaponCamera;
    public Camera mainCamera;
    public float scopedFOV = 10f; // Zoomed FOV
    public float normalFOV = 60f; // Default FOV

    public InputActionReference scopeAction;

    private void Start()
    {
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not assigned!");
            return;
        }
        // Set normalFOV to the initial camera FOV
        normalFOV = mainCamera.fieldOfView;
        Debug.Log($"Initial FOV: {normalFOV}");
    }

    private void OnEnable()
    {
        scopeAction.action.performed += OnScopePerformed;
        scopeAction.action.Enable();
    }

    private void OnDisable()
    {
        scopeAction.action.performed -= OnScopePerformed;
        scopeAction.action.Disable();
    }

    private void OnScopePerformed(InputAction.CallbackContext context)
    {
        isScoped = !isScoped;
        animator.SetBool("IsScoped", isScoped);
        Debug.Log($"Scoped state toggled: {isScoped}");

        if (isScoped)
            OnScoped();
        else
            OnUnscoped();
    }

    void OnUnscoped()
    {
        scopeOverlay.SetActive(false);
        weaponCamera.SetActive(true);
        mainCamera.fieldOfView = normalFOV; // Instant reset
        Debug.Log($"Unscoped: FOV set to {mainCamera.fieldOfView}");
    }

    void OnScoped()
    {
        scopeOverlay.SetActive(true);
        weaponCamera.SetActive(false);
        mainCamera.fieldOfView = scopedFOV; // Instant zoom
        Debug.Log($"Scoped: FOV set to {mainCamera.fieldOfView}");
    }

    private void Update()
    {
        // Optional: toggle zoom with Mouse2 (right-click)
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            isScoped = !isScoped;
            if (isScoped)
                OnScoped();
            else
                OnUnscoped();
        }
    }
}
