using UnityEngine;

public class MouseButtonDown : MonoBehaviour
{
    [SerializeField] private Camera _inputCamera;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = _inputCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                var unitProperties = hit.collider.GetComponent<UnitProperties>();

                if (unitProperties != null)
                    print("go");
            }
        }
    }
}