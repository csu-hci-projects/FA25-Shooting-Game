using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scope : MonoBehaviour
{
    public Animator animator;
    private bool isScoped = false;
    public GameObject scopeOverlay;
    public GameObject weaponCamera;
    public Camera mainCamera;
    public float scopedFOV = .15f;
    private float normalFOV;


    // Reference to the Input Action
    public InputActionReference scopeAction;

    private void OnEnable()
    {
        // Subscribe to the performed event
        scopeAction.action.performed += OnScopePerformed;
        scopeAction.action.Enable();
    }

    private void OnDisable()
    {
        // Unsubscribe to avoid memory leaks
        scopeAction.action.performed -= OnScopePerformed;
        scopeAction.action.Disable();
    }

    private void OnScopePerformed(InputAction.CallbackContext context)
    {
        isScoped = !isScoped;
        animator.SetBool("IsScoped", isScoped);

        if (isScoped)
        {
            StartCoroutine(OnScoped());
        }
        else
        {
            OnUnscoped();
        }
    }

    void OnUnscoped()
    {
        scopeOverlay.SetActive(false);
        weaponCamera.SetActive(true);

        mainCamera.fieldOfView = normalFOV;
    }

    IEnumerator OnScoped()
    {
        yield return new WaitForSeconds(.15f);
        scopeOverlay.SetActive(true);
        weaponCamera.SetActive(false);

        normalFOV = mainCamera.fieldOfView;
        mainCamera.fieldOfView = scopedFOV;
    }

}
