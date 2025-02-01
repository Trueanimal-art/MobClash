using UnityEngine;

public class SpawnPointPositioner : MonoBehaviour
{
    void Start()
    {
        Camera mainCamera = Camera.main;

        // Определяем нижнюю часть экрана
        Vector3 bottomScreenPosition = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0f, mainCamera.nearClipPlane + 1));
        
        // Устанавливаем точку спауна в эту позицию
        transform.position = new Vector3(bottomScreenPosition.x, bottomScreenPosition.y, 0);
    }
}
