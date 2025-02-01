using UnityEngine;

public class CannonController : MonoBehaviour
{
    private Camera mainCamera;

    [Header("Settings")]
    public GameObject unitPrefab;      // Префаб юнита (моба)
    public Transform spawnPoint;       // Точка спауна для мобов
    public float fireRate = 0.5f;      // Интервал между выстрелами (в секундах)
    public float holdingFireRate = 0.2f; // Интервал между выстрелами при зажатой кнопке

    private float nextFireTime = 0f;   // Время следующего выстрела
    private bool isShooting = false;   // Флаг для предотвращения повторных выстрелов

    void Start()
    {
        mainCamera = Camera.main;
        ValidateReferences();
    }

    void Update()
    {
        MoveCannon();
        HandleShooting();
    }

    private void HandleShooting()
    {
        // Используем единый таймер для всех случаев
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            // Выстрел при зажатой кнопке
            SpawnUnit();
            nextFireTime = Time.time + holdingFireRate; // Используем holdingFireRate для быстрого выстрела при удерживании
        }
        // Если кнопка мыши отпущена, сбрасываем флаг
        else if (!Input.GetMouseButton(0))
        {
            isShooting = false; 
        }
    }

    void MoveCannon()
    {
        if (mainCamera == null) return;

        Vector3 targetPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = 0;

        float clampedX = ClampCannonPosition(targetPosition.x);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    private float ClampCannonPosition(float targetX)
    {
        float screenMinX = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        float screenMaxX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        float cannonWidth = 0f;

        // Проверка на наличие компонента SpriteRenderer
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            cannonWidth = spriteRenderer.bounds.size.x;
        }

        return Mathf.Clamp(targetX, screenMinX + cannonWidth / 2, screenMaxX - cannonWidth / 2);
    }

    void SpawnUnit()
    {
        if (unitPrefab != null && spawnPoint != null)
        {
            // Создаем нового моба
            GameObject mob = Instantiate(unitPrefab, spawnPoint.position, Quaternion.identity);

            // Применяем множитель и начальную скорость
            MobController mobController = mob.GetComponent<MobController>();
            if (mobController != null)
            {
                mobController.Initialize(); // Убедитесь, что этот метод существует в MobController
            }
            else
            {
                Debug.LogError("MobController не найден в префабе юнита!");
            }

            Debug.Log($"Моб создан: {mob.name} в позиции {spawnPoint.position}");
        }
        else
        {
            Debug.LogError("UnitPrefab или SpawnPoint не назначены в CannonController!");
        }
    }

    private void ValidateReferences()
    {
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera не найдена! Убедитесь, что у вас есть камера с тегом 'MainCamera'.");
        }

        if (unitPrefab == null)
        {
            Debug.LogError("UnitPrefab не назначен! Укажите префаб юнита в инспекторе.");
        }

        if (spawnPoint == null)
        {
            Debug.LogError("SpawnPoint не назначен! Укажите точку спауна в инспекторе.");
        }
    }
}
