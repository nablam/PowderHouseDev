using UnityEngine;

public class SubBlockVisualCtrl : MonoBehaviour
{

    public GameObject TopL;
    public GameObject TopM;
    public GameObject TopR;
    public GameObject MidL;
    public GameObject MidM;
    public GameObject MidR;
    public GameObject BotL;
    public GameObject BotM;
    public GameObject BotR;
    // Start is called before the first frame update
    void Start()
    {
        TopL.SetActive(false);
        TopM.SetActive(false);
        TopR.SetActive(false);
        MidL.SetActive(false);
        MidM.SetActive(false);
        MidR.SetActive(false);
        BotL.SetActive(false);
        BotM.SetActive(false);
        TopL.SetActive(false);
        BotR.SetActive(false);
    }

    public void TurnonTop() { TopM.SetActive(true); if (!MidM.activeSelf) MidM.SetActive(true); }
    public void TurnonBot() { BotM.SetActive(true); if (!MidM.activeSelf) MidM.SetActive(true); }
    public void TurnonRight() { MidR.SetActive(true); if (!MidM.activeSelf) MidM.SetActive(true); }
    public void TurnonLeft() { MidL.SetActive(true); if (!MidM.activeSelf) MidM.SetActive(true); }
}
