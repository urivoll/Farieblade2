using UnityEngine;

public class DestroyEffect2 : MonoBehaviour
{
    [SerializeField] private float duration;
    void Start()
    {
            Invoke("Destr", duration);
    }

    private void Destr()
    {
        Destroy(gameObject);
    }
}
