using UnityEngine;
using GameSystem;
using GameInterfaces;

public class PlayerController : MonoBehaviour
{
    public PlayerBase plBase = new PlayerBase();
    public static Transform transformPlayer;
    public static Transform transformHand;
    public static Transform transformHandParent;
    public static Transform transformGema;
    public static bool getAction;
    [SerializeField] Transform transformHandParentRef;
    [SerializeField] Transform transformHandRef;

    void ConfigCollectReferences()
    {
        transformHandParent = transformHandParentRef;
        transformPlayer = transform;
        transformHand = transformHandRef;
    }

    void Start()
    {
        ConfigCollectReferences();
        plBase.StartSettings(GetComponent<CameraController>().cameraMove);
    }

    void Update() 
    { 
        if (!getAction)
        {
            plBase.MovePlayer(); 
            plBase.Mov(); 
        }
    }
}
