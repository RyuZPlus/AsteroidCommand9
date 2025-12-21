using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Velocidad base")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Multiplicador por plataforma")]
    [SerializeField] private float pcMultiplier = 1f;
    [SerializeField] private float androidMultiplier = 1.4f;

    [Header("Propiedades de bala de jugador")]
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.25f;

    private float nextFireTime;

    private Vector2 moveInput;
    private float currentMultiplier;

    void Awake()
    {
#if UNITY_ANDROID
        currentMultiplier = androidMultiplier;
#else
        currentMultiplier = pcMultiplier;
#endif
    }

    void Update()
    {
        Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0f);

        if (movement.magnitude > 1f)
            movement.Normalize();

        transform.position += movement * moveSpeed * Time.deltaTime;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot()    
    {
        Instantiate(laserPrefab, firePoint.position, Quaternion.identity);
        Debug.Log("Pew!");
    }
}