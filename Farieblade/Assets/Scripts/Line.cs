using UnityEngine;

public class Line : MonoBehaviour
{
    private LineRenderer lineRenderer;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        lineRenderer.SetPosition(1, Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
