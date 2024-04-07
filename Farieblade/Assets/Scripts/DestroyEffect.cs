using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    void Start()
    {
            Invoke("Destr", 2f);
    }

    private void Destr()
    {
        Destroy(gameObject);
    }
}
