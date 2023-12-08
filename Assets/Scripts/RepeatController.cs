using UnityEngine;
using UnityEngine.UI;

public class RepeatController : MonoBehaviour
{
    public GameObject popUppanel;
    public GameObject Diepanel;
    public Button repeatbutton;
    public Button showPanelbutton;

    private bool clicked = false;
    private GameController gameController;
 

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        repeatbutton.onClick.AddListener(OnRepeatButtonClick);
        showPanelbutton.onClick.AddListener(OnShowPanelButtonClick);
    }

    void Update()
    {
        if (popUppanel.activeSelf && clicked)
        {
            popUppanel.SetActive(true);
            clicked = false;
        }
    }

    void OnRepeatButtonClick()
    {
        gameController.StartGame();
       
    }

    void OnShowPanelButtonClick()
    {
        gameController.ShowRepeatPanel();
    }
}
