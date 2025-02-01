using UnityEngine;
using UnityEngine.UI;  // Для работы с UI элементами

public class BaseController : MonoBehaviour
{
    public float maxHealth = 100f;  // Максимальное здоровье базы
    public float currentHealth;     // Текущее здоровье базы
    public Slider healthSlider;     // Ссылка на UI Slider для отображения здоровья

    void Start()
    {
        currentHealth = maxHealth;
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;  // Устанавливаем начальное значение
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // Ограничиваем здоровье, чтобы оно не стало меньше 0
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        // Обновляем значение слайдера
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        // Дополнительно: если здоровье базы дошло до нуля, можем уничтожить базу
        if (currentHealth <= 0)
        {
            DestroyBase();
        }
    }

    void DestroyBase()
    {
        // Логика уничтожения базы (например, анимация разрушения, звуки и т.д.)
        Debug.Log("База уничтожена!");
        Destroy(gameObject);  // Уничтожаем объект базы
    }
}
