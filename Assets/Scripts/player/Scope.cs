using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;
using StarterAssets;

public class Scope : MonoBehaviour
{
    [Header("References")]
    public Animator animator;
    public GameObject scopeOverlay;
    public GameObject weaponCamera;
    public CinemachineVirtualCamera vcam;
    private FirstPersonController fpController;


    [Header("Scope Settings")]
    public float scopedFOV = 10f; // Zoomed FOV
    private float normalFOV;
    private bool isScoped = false;

    [Header("Input")]
    public InputActionReference scopeAction;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip scopeInSound;
    [SerializeField] private AudioClip scopeOutSound;

    private void Start()
    {
        if (vcam == null)
        {
            Debug.LogError("Virtual Camera not assigned!");
            return;
        }

        fpController = FindObjectOfType<FirstPersonController>();

        if (fpController == null)
        {
            Debug.LogError("Scope: Could not find FirstPersonController in scene!");
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

    private void OnScoped()
    {
        scopeOverlay.SetActive(true);
        weaponCamera.SetActive(false);
        fpController.IsZoomed = true;

        if (vcam != null)
            vcam.m_Lens.FieldOfView = scopedFOV;

        if (audioSource != null && scopeInSound != null)
            audioSource.PlayOneShot(scopeInSound);
    }

    private void OnUnscoped()
    {
        scopeOverlay.SetActive(false);
        weaponCamera.SetActive(true);
        fpController.IsZoomed = false;

        if (vcam != null)
            vcam.m_Lens.FieldOfView = normalFOV;

        if (audioSource != null && scopeOutSound != null)
            audioSource.PlayOneShot(scopeOutSound);
    }

    private void Update()
    {
        // Optional debug toggle
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
