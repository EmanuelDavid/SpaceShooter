using UnityEngine;
using UnityEngine.EventSystems;

public class ShootAreea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool touched;
    private int pointerID;
    private bool canFire;

    void Awake()
    {
        touched = false;
        pointerID = int.MaxValue;
    }

    public void OnPointerDown(PointerEventData data)
    {
        if (!touched)
        {
            touched = true;
            pointerID = data.pointerId;
            canFire = true;

        }
    }

    public void OnPointerUp(PointerEventData data)
    {
        if (data.pointerId == pointerID)
        {
            canFire = false;
            touched = false;
            pointerID = int.MaxValue;
        }
    }

    public bool CanFire()
    {
        return canFire;
    }

    public int GetShootPointerId()
    {
        return pointerID;
    }
}
