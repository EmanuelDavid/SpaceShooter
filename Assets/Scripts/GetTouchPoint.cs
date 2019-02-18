using UnityEngine;
using System.Linq;

public class GetTouchPoint : MonoBehaviour
{
    private Camera _camera;
    private GameController _gameController;

    private SpaceshipController _spaceshipController;
    private ShootAreea _shootArea;

    private void Awake()
    {
        _spaceshipController = gameObject.GetComponent<SpaceshipController>();

        var cameraGameObject = GameObject.FindWithTag("MainCamera");
        if (cameraGameObject == null)
        {
            Debug.Log("Cannot find 'MainCamera' gameObject");
        }
        else
        {
            _camera = cameraGameObject.GetComponent<Camera>();
        }

        var gameController = GameObject.FindWithTag("GameController");
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' gameObject");
        }
        else
        {
            _gameController = gameController.GetComponent<GameController>();
            _shootArea = _gameController.ShootArea;
        }
    }


    void Update()
    {
        if (_gameController != null && !_gameController.IsGyroscopControlled)
        {
            // Handle screen touches.
            if (Input.touchCount > 0)
            {
                var shootAreeaPointerId = _shootArea.GetShootPointerId();
                Touch[] touches = Input.touches;

                var dragTouch = touches.FirstOrDefault(t => t.fingerId != shootAreeaPointerId && t.phase == TouchPhase.Moved);

                if (dragTouch.position.x != 0f && dragTouch.position.y != 0f && dragTouch.phase == TouchPhase.Moved)
                {
                    Vector2 touchPos = dragTouch.position;

                    Vector3 touchPosinWorldSpace = _camera.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, 5));

                    // Position the player
                    transform.position = touchPosinWorldSpace;
                }
            }
        }
    }
}
