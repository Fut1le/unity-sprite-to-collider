using UnityEngine;
using System.Collections.Generic; // Добавлено для использования List<>

[RequireComponent(typeof(PolygonCollider2D))]
public class SpriteToCollider : MonoBehaviour
{
    public float simplificationTolerance = 0.5f;  // Толерантность упрощения пути

    void Start()
    {
        var collider = GetComponent<PolygonCollider2D>();
        var sprite = GetComponent<SpriteRenderer>().sprite;
        
        if(sprite == null)
        {
            Debug.LogWarning("SpriteToCollider: Не найден спрайт на объекте " + gameObject.name);
            return;
        }

        // Очищаем предыдущие точки коллайдера
        collider.pathCount = 0;
        
        // Создаем коллайдер на основе спрайта
        if (sprite != null)
        {
            collider.pathCount = sprite.GetPhysicsShapeCount();

            List<Vector2> path = new List<Vector2>();
            for (int i = 0; i < collider.pathCount; i++)
            {
                sprite.GetPhysicsShape(i, path);
                // Упрощаем путь перед его устанавкой.
                List<Vector2> simplifiedPath = SimplifyPath(path, simplificationTolerance);
                collider.SetPath(i, simplifiedPath);
            }
        }
        else
        {
            Debug.LogError("SpriteToCollider: Не удалось получить спрайт для создания коллайдера.");
        }
    }

    // Вставьте свой алгоритм упрощения пути сюда
    List<Vector2> SimplifyPath(List<Vector2> originalPath, float tolerance)
    {
        // Это простая реализация, которая не использует tolerance.
        // Должна быть заменена на настоящий алгоритм упрощения, если вам нужна точность.

        List<Vector2> simplified = new List<Vector2>();
        for (int i = 0; i < originalPath.Count; i += 2) // Пропускаем каждую вторую точку (очень базовое 'упрощение')
        {
            simplified.Add(originalPath[i]);
        }

        return simplified;
    }
}
