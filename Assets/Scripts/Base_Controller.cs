using UnityEngine;

public class BaseController : MonoBehaviour
{
    public float health = 100f;  // Здоровье базы
    public float damagePerHit = 10f; // Урон, который база наносит за время
    private bool isDestroyed = false; // Флаг, чтобы не разрушать базу несколько раз

    // Ссылка на Collider базы для проверки попадания в радиус
    private Collider baseCollider;

    void Start()
    {
        baseCollider = GetComponent<Collider>(); // Получаем Collider базы
    }

    void Update()
    {
        // Если здоровье базы ниже нуля и она ещё не разрушена
        if (health <= 0 && !isDestroyed)
        {
            DestroyBase();
        }
    }

    // Метод для получения урона от мобов
    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"База получила урон. Осталось здоровья: {health}");
    }

    // Метод для разрушения базы
    private void DestroyBase()
    {
        isDestroyed = true;
        // Здесь можно добавить анимацию разрушения, например, вспышку или падение объекта
        Destroy(gameObject);  // Уничтожаем объект базы
        Debug.Log("База уничтожена!");
    }

    // Метод, который проверяет, вошли ли мобы в радиус базы
    private void OnTriggerEnter(Collider other)
    {
        // Если объект, попавший в радиус, является мобом
        if (other.CompareTag("Mob"))
        {
            MobController mob = other.GetComponent<MobController>();
            if (mob != null)
            {
                // Моб атакует базу, когда входит в радиус
                mob.SetTargetBase(this); // Устанавливаем цель для моба
            }
        }
    }
}
