using UnityEngine;

public class DrawLineManager : MonoBehaviour
{
    public GameObject linePrefab;

    public SteamVR_TrackedObject trackedObj;

    private MeshLineRenderer currLine;

    private int numClicks = 0;

    void Update()
    {
        SteamVR_Controller.Device device = SteamVR_Controller.Input((int) trackedObj.index);
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            currLine = Instantiate(linePrefab).GetComponent<MeshLineRenderer>();
            currLine.SetWidth(0.1f);
            numClicks = 0;
        }
        else if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            currLine.AddPoint(trackedObj.transform.position);
            numClicks++;
        }
        else if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            numClicks = 0;
            currLine = null;
        }

        if (currLine != null)
        {
            currLine.lmat.color = ColorManager.Instance.GetCurrentColor();
        }
    }
}
