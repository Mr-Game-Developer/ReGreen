using UnityEngine;
using UnityEngine.UI;

public class PauseUI_to_UIScene : MonoBehaviour
{
    public GameObject pausePNL;
    public GameObject controlsUI;
    public GameObject touchPad, dialougeSubtitles, optionPausePNL;
    public GameObject dialougeSpeaker;
    public Text pointsVictory, cuttedTreesVictory, plantTreesVictory;
    public Text pointsLoss, cuttedTreesLoss, plantTreesLoss;
    public GameObject VictoryPanel, LossPanel;
    public DataContainer gdata;
    public GameObject HUD1, HUD2;


    // For Pause the Game
    public void GamePlaytoPausePNL()
    {
        ClickSound();
        Time.timeScale = 0;
        //for(int i = 0; i < GameplayUI.Length; i++)
        //{
        //    GameplayUI[i].SetActive(false);
        //}

        
        touchPad.SetActive(false);
        dialougeSubtitles.SetActive(false);
        dialougeSpeaker.SetActive(false);

        pausePNL.SetActive(true);
        AudioManager.instance.MusicSource.mute = false;
    }

    // Resume, Restart and Exit Game
    public void ResumeGameplayBTN()
    {
        Time.timeScale = 1;
        pausePNL.SetActive(false);
        optionPausePNL.SetActive(false);
        ClickSound();
        controlsUI.SetActive(true);
        touchPad.SetActive(true);
        dialougeSubtitles.SetActive(true);
        dialougeSpeaker.SetActive(true);

        AudioManager.instance.MusicSource.mute = true;
    }

    public void RestartGameplayBTN()
    {
        PlayerPrefs.SetInt("ReGameplay", 5);
        Time.timeScale = 1;
        ClickSound();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void PauseOptionBTN()
    {
        pausePNL.SetActive(false);
        optionPausePNL.SetActive(true);
    }

    public void SaveSoundOption()
    {
        pausePNL.SetActive(true);
        optionPausePNL.SetActive(false);
    }

    public void Victory()
    {
        pointsVictory.text = gdata.gamedata.Greenzi.ToString();
        cuttedTreesVictory.text = PlayerPrefs.GetInt("NoOfCuttedTrees").ToString();
        plantTreesVictory.text = PlayerPrefs.GetInt("NoOfPlantedTrees").ToString();

        controlsUI.SetActive(false);
        touchPad.SetActive(false);
        dialougeSubtitles.SetActive(false);
        dialougeSpeaker.SetActive(false);
        VictoryPanel.SetActive(true);
    }

    public void VictoryHomeBTN()
    {
        PlayerPrefs.SetInt("Menu", 4);
        Time.timeScale = 1;
        AudioManager.instance.MusicSource.mute = false;
        ClickSound();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void Loss()
    {
        pointsLoss.text = gdata.gamedata.Greenzi.ToString();
        cuttedTreesLoss.text = PlayerPrefs.GetInt("NoOfCuttedTrees").ToString();
        plantTreesLoss.text = PlayerPrefs.GetInt("NoOfPlantedTrees").ToString();

        controlsUI.SetActive(false);
        touchPad.SetActive(false);
        dialougeSubtitles.SetActive(false);
        dialougeSpeaker.SetActive(false);
        LossPanel.SetActive(true);
    }

    public void LossRetryBTN()
    {
        PlayerPrefs.SetInt("ReGameplay", 5);
        Time.timeScale = 1;
        ClickSound();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void ExitGameOrHomeBTN()
    {
        PlayerPrefs.SetInt("Menu", 4);
        Time.timeScale = 1;
        AudioManager.instance.MusicSource.mute = false;
        ClickSound();
        Destroy(HUD1);
        Destroy(HUD2);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("isStarting", 0);
        PlayerPrefs.SetInt("Menu", 0);
        PlayerPrefs.SetInt("Gameplay", 0);
        PlayerPrefs.SetInt("ReGameplay", 0);
    }

    public void ClickSound()
    {
        AudioManager.instance.PlaySFX("Click");
    }
}
