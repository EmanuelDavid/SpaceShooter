using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GetTouchPoint : MonoBehaviour
{
    public Camera camera;
    public ShootAreea shootArea;

    void Update()
    {
        // Handle screen touches.
        if (Input.touchCount > 0)
        {
            var shootAreeaPointerId = shootArea.GetShootPointerId();
            Touch[] touches = Input.touches;

            var dragTouch = touches.FirstOrDefault(t => t.fingerId != shootAreeaPointerId  && t.phase == TouchPhase.Moved);

            if (dragTouch.position.x != 0f && dragTouch.position.y != 0f && dragTouch.phase == TouchPhase.Moved)
            {
                Vector2 touchPos = dragTouch.position;

                Vector3 touchPosinWorldSpace = camera.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, 5));

                // Position the player
                transform.position = touchPosinWorldSpace;
            }
        }
    }
}
