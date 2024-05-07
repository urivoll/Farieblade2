using UnityEngine;

public class DestroyEffect2 : MonoBehaviour
{
    [SerializeField] private float duration;
    void Start()
    {
        Destroy(gameObject, duration);
    }
}
