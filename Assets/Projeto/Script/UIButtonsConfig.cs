using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIButtonsConfig : MonoBehaviour
{
    static public UnityAction acitionGetGema;
    [SerializeField] Canvas PanelMessage;
    [SerializeField] Button buttonExit;
    [SerializeField] Button buttonPegar;

    void Start()
    {
        SetButtonsActions();
    }

    void PanelObjetos()
    {
        if (acitionGetGema != null) acitionGetGema();
        else PanelMessage.enabled = true;
    }

    void SetButtonsActions()
    {
        buttonPegar.onClick.AddListener(delegate { PanelObjetos(); });
        buttonExit.onClick.AddListener(delegate { ExitGame(); });
    }

    void ExitGame()
    {
        Debug.Log("ExitGame");
    }
}
