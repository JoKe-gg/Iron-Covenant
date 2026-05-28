using UnityEngine;

public class AttractObjects : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Gem gem))
        {
            gem.SetAttraction(transform);
        }
    }
}
