using UnityEngine;
using UnityEngine.InputSystem;

public enum WeaponMode
{
    Single,
    Double
}

public class PlayerController : MonoBehaviour
{
    [Header("Base Speed")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Platform multiplier")]
    [SerializeField] private float pcMultiplier = 1f;
    [SerializeField] private float androidMultiplier = 1.4f;

    [Header("Shoot")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.25f;
    [SerializeField] private BulletPool bulletPool;

    [Header("Weapon Level")]
    [SerializeField] private int weaponLevel = 1;
    [SerializeField] private int maxWeaponLevel = 3;

    [SerializeField] private Color[] bulletColors;

    private float nextFireTime;

    private Vector2 moveInput;
    private float currentMultiplier;

    [Header("Weapon Mode")]
    [SerializeField] private WeaponMode weaponMode = WeaponMode.Single;

    [SerializeField] private Transform firePointLeft;
    [SerializeField] private Transform firePointRight;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();

#if UNITY_ANDROID
        currentMultiplier = androidMultiplier;
#else
        currentMultiplier = pcMultiplier;
#endif
        animator.SetInteger("WeaponMode", (int)weaponMode);
        UpdateFirePoints();
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
        if (weaponMode == WeaponMode.Single)
        {
            ShootFromPoint(firePoint);
        }
        else if (weaponMode == WeaponMode.Double)
        {
            ShootFromPoint(firePointLeft);
            ShootFromPoint(firePointRight);
        }

        Debug.Log("Pew!");
    }

    private void ShootFromPoint(Transform point)
    {
        GameObject bullet = bulletPool.GetBullet();

        if (bullet != null)
        {
            bullet.transform.position = point.position;
            bullet.transform.rotation = Quaternion.identity;

            LaserBullet laser = bullet.GetComponent<LaserBullet>();
            if (laser != null)
            {
                if (weaponMode == WeaponMode.Double)
                {
                    laser.SetLevel(weaponLevel);
                }
                else
                {
                    laser.SetLevel(1);
                }
            }

            bullet.SetActive(true);
        }
    }
    private void UpdateFirePoints()
    {
        if (weaponMode == WeaponMode.Single)
        {
            firePoint.gameObject.SetActive(true);
            firePointLeft.gameObject.SetActive(false);
            firePointRight.gameObject.SetActive(false);
        }
        else if (weaponMode == WeaponMode.Double)
        {
            firePoint.gameObject.SetActive(false);
            firePointLeft.gameObject.SetActive(true);
            firePointRight.gameObject.SetActive(true);
        }
    }
    private void UpgradeWeapon()
    {
        if (weaponLevel < maxWeaponLevel)
        {
            weaponLevel++;
            Debug.Log("Weapon Level Up: " + weaponLevel);
        }
    }
    public void ActivateDoubleShot()
    {
        if (weaponMode == WeaponMode.Single)
        {
            Debug.Log("Double Shot Enabled");
            weaponMode = WeaponMode.Double;
            animator.SetInteger("WeaponMode", (int)weaponMode);
            UpdateFirePoints();
        }
        else
        {
            UpgradeWeapon();
        }
    }
    public void DeactivateDoubleShot()
    {
        Debug.Log("Double Shot Disable");
        weaponMode = WeaponMode.Single;
        animator.SetInteger("WeaponMode", (int)weaponMode);
        UpdateFirePoints();
    }

}