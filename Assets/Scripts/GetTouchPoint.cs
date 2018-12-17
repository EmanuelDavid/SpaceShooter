using UnityEngine;
public class GetTouchPoint : MonoBehaviour
{
    public Camera camera;

    void Update()
    {
        // Handle screen touches.
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Move the player if the screen has the finger moving.
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 touchPos = touch.position;

                Vector3 touchPosinWorldSpace = camera.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, 5));

                // Position the player
                transform.position = touchPosinWorldSpace;
            }
        }
    }
}
