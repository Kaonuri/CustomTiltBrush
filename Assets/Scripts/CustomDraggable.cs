using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomDraggable : MonoBehaviour
{
    public SteamVR_TrackedObject trackedObj;

    public Transform minBound;

    public bool fixX;
    public bool fixY;
    public Transform thumb;
    bool dragging;

    void FixedUpdate()
    {
        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            dragging = false;
            Ray ray = new Ray(trackedObj.transform.position, trackedObj.transform.forward);
            RaycastHit hit;
            if (GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity))
            {
                dragging = true;
            }
        }

        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger)) dragging = false;
        if (dragging && device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            Ray ray = new Ray(trackedObj.transform.position, trackedObj.transform.forward);
            RaycastHit hit;
            if (GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity))
            {
                var point = hit.point;                
                SetThumbPosition(point);
                SendMessage("OnDrag", Vector3.one - (thumb.localPosition - minBound.localPosition) / GetComponent<BoxCollider>().size.x);
            }
        }
    }

    void SetDragPoint(Vector3 point)
    {
        point = (Vector3.one - point) * GetComponent<Collider>().bounds.size.x + GetComponent<Collider>().bounds.min;
        SetThumbPosition(point);
    }

    void SetThumbPosition(Vector3 point)
    {
        Vector3 temp = thumb.localPosition;
        thumb.position = point;
        thumb.localPosition = new Vector3(fixX ? temp.x : thumb.localPosition.x, fixY ? temp.y : thumb.localPosition.y, thumb.position.z - 1);
    }
}
