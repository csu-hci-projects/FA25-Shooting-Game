using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class Scope : MonoBehaviour
{
    public Animator animator;
    private bool isScoped = false;
    public GameObject scopeOverlay;
    public GameObject weaponCamera;

    // THIS is the real camera controller
    public CinemachineVirtualCamera vcam;

    public float scopedFOV = 10f; // Zoomed FOV
    private float normalFOV;

    public InputActionReference scopeAction;

    private void Start()
    {
        if (vcam == null)
        {
            Debug.LogError("Virtual Camera not assigned!");
            return;
        }

        normalFOV = vcam.m_Lens.FieldOfView;
        Debug.Log($"Initial Cinemachine FOV: {normalFOV}");
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

        vcam.m_Lens.FieldOfView = normalFOV;
        Debug.Log($"Unscoped: Cinemachine FOV set to {vcam.m_Lens.FieldOfView}");
    }

    void OnScoped()
    {
        scopeOverlay.SetActive(true);
        weaponCamera.SetActive(false);

        vcam.m_Lens.FieldOfView = scopedFOV;
        Debug.Log($"Scoped: Cinemachine FOV set to {vcam.m_Lens.FieldOfView}");
    }

    private void Update()
    {
        // optional debug toggle
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isScoped = !isScoped;

            if (isScoped)
                OnScoped();
            else
                OnUnscoped();
        }
    }
}
