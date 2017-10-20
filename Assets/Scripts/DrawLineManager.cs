using UnityEngine;

public class DrawLineManager : MonoBehaviour
{
    public GameObject linePrefab;
    public Material lMat;

    public Color lineColor;
    public float lineWidth = 0.1f;

    public SteamVR_TrackedObject trackedObj;

    private MeshLineRenderer currLine;
    private int numClicks = 0;

    void Update()
    {
        print(trackedObj.transform.forward);

        SteamVR_Controller.Device device = SteamVR_Controller.Input((int) trackedObj.index);
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            currLine = Instantiate(linePrefab).GetComponent<MeshLineRenderer>();
            currLine.lmat = new Material(lMat);
            currLine.lmat.color = lineColor;
            currLine.SetWidth(lineWidth);
            numClicks = 0;
        }
        else if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            currLine.AddPoint(trackedObj.transform.position + trackedObj.transform.forward * 0.1f + trackedObj.transform.TransformDirection(Vector3.down) * 0.02f, trackedObj.transform.forward);
            numClicks++;
        }
        else if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            currLine.GetComponent<MeshCollider>().sharedMesh = currLine.GetComponent<MeshFilter>().mesh;

            numClicks = 0;
            currLine = null;
        }
    }
}
