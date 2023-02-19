using GameSystem;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CameraMove cameraMove = new CameraMove();

    public void IsCamController(bool active)
    {
        cameraMove.IsCamController(active);
    }

    void Awake()
    {
        cameraMove.AwakeSettings();
    }

    private void Update()
    {
        cameraMove.CameraGiro();
    }

    void LateUpdate()
    {
        cameraMove.RefreshCam();
    }
}
