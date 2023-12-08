using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject popUpPanel;
    public GameObject repeatPanel;
    public GameObject numbers;
    public GameObject level;
    public GameObject castlehealthtext;
    public GameObject castlehealthbar;
    public GameObject attackbutton;
    public Button playButton;
    public Button stopButton;
    public GameObject barrelGenerator;
 

    private void Start()
    {
        playButton.onClick.AddListener(StartGame);
        stopButton.onClick.AddListener(QuitGame);
        numbers.SetActive(false);
        level.SetActive(false);
        castlehealthtext.SetActive(false);
        castlehealthbar.SetActive(false);
        attackbutton.SetActive(false);
        barrelGenerator.SetActive(false);
        repeatPanel.SetActive(false);
        
    }

    private bool isBarrelGeneratorActive = false;

    

    public void StartGame()
    {
        Debug.Log("Game Starting...");
        popUpPanel.SetActive(false);
        repeatPanel.SetActive(false);
        numbers.SetActive(true);
        castlehealthtext.SetActive(true);
        castlehealthbar.SetActive(true);
        attackbutton.SetActive(true);
        level.SetActive(true);
        StartCoroutine(ActivateBarrelGenerator());
    }

    public IEnumerator ActivateBarrelGenerator()
    {
        while (true)
        {
            if (!isBarrelGeneratorActive)
            {
                isBarrelGeneratorActive = true;
                yield return new WaitForSeconds(3f);

                if (barrelGenerator != null)
                {
                    barrelGenerator.SetActive(true);
                }

                isBarrelGeneratorActive = false;
            }

            yield return null;
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit the game...");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


public void ShowRepeatPanel()
{
   repeatPanel.SetActive(false);
   popUpPanel.SetActive(true);
}

}








