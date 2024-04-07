using UnityEngine;

public class HideEffect : MonoBehaviour
{
    void Start()
    {
        Invoke("Destr", 2f);
    }

    private void Destr()
    {
        gameObject.SetActive(false);
    }
}
