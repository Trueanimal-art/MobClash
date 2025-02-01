using UnityEngine;

public class MobController : MonoBehaviour
{
    public int multiplier = 1;
    public float baseSpeed = 2f;
    private float currentSpeed;
    private Rigidbody2D rb;

    // Ссылка на базу
    private BaseController targetBase;

    private bool isAttackingBase = false;  // Флаг для отслеживания атаки базы
    private bool isCloned = false;  // Флаг для предотвращения клонирования моба

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Initialize();
    }

    public void Initialize()
    {
        if (rb == null) return;

        currentSpeed = baseSpeed * multiplier;

        // Сбрасываем угловую скорость и поворот моба
        rb.angularVelocity = 0f;
        rb.rotation = 0f;

        // Отключаем гравитацию (если вдруг включена)
        rb.gravityScale = 0f;

        // Сбрасываем любую силу, которая могла остаться
        rb.velocity = Vector2.zero;

        UpdateSpeed();
    }

    public void Multiply(int factor)
    {
        multiplier *= factor;
        currentSpeed = baseSpeed * multiplier;
        UpdateSpeed();
    }

    private void UpdateSpeed()
    {
        if (rb != null)
        {
            rb.velocity = new Vector2(currentSpeed, 0); // Движение строго по оси X
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Gate"))
        {
            Multiply(2);
            Debug.Log($"Моб прошел через ворота. Новый множитель: {multiplier}");
        }

        // Если моб столкнулся с базой
        if (other.CompareTag("Base"))
        {
            if (isCloned) return;  // Если это клон, не создаем нового моба
            isCloned = true;  // Отметим, что это клон

            // Останавливаем движение моба
            isAttackingBase = true;
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;  // Делаем моба кинематическим, чтобы он не дергался

            AttackBase();
        }
    }

    // Метод для установки цели - базы
    public void SetTargetBase(BaseController baseController)
    {
        targetBase = baseController;
    }

    // Метод для движения к базе
    void Update()
    {
        if (targetBase != null && !isAttackingBase)
        {
            MoveTowardsBase(targetBase.transform.position);
        }
    }

    // Логика движения к базе
    private void MoveTowardsBase(Vector3 basePosition)
    {
        Vector3 direction = basePosition - transform.position;
        transform.position += direction.normalized * currentSpeed * Time.deltaTime;

        // Если в радиусе атаки, начинаем атаковать базу
        float attackRange = 1.5f;  // Радиус атаки
        if (Vector3.Distance(transform.position, basePosition) <= attackRange)
        {
            AttackBase();
        }
    }

    // Логика атаки базы
    private void AttackBase()
    {
        if (targetBase != null)
        {
            targetBase.TakeDamage(1f);  // Наносим урон базе
            Debug.Log("Моб атакует базу!");
        }
    }

    // Метод для установки скорости
    public void SetSpeed(float speed)
    {
        currentSpeed = speed;
        UpdateSpeed();
    }

    // Метод для получения текущей скорости
    public float GetSpeed()
    {
        return currentSpeed;
    }
}
