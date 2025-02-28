using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject MainMenuPnl, OptionPnl, CreditsPnl;
    AudioSettings settings;

    // Start is called before the first frame update
    void Start()
    {
        settings = FindAnyObjectByType<AudioSettings>();
    }

    public void NewGame()
    {
        ClickSound();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        PlayerPrefs.SetInt("isStarting", 2);
        PlayerPrefs.SetInt("Gameplay", 3);
    }

    public void ContinueGame()
    {
        ClickSound();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        PlayerPrefs.SetInt("isStarting", 2);
        PlayerPrefs.SetInt("Gameplay", 3);
    }

    public void GameSettingsPNL()
    {
        ClickSound();
        OptionPnl.SetActive(true);
        CreditsPnl.SetActive(false);
    }

    public void GameSettingSaveBTN()
    {
        ClickSound();
        //SaveAudio();
        OptionPnl.SetActive(false);
    }

    //public void ExitSettingsPNL()
    //{
    //    ClickSound();
    //    OptionPnl.SetActive(false);
    //    MainMenuPnl.SetActive(true);
    //}

    public void CreditsPNL()
    {
        ClickSound();
        OptionPnl.SetActive(false);
        CreditsPnl.SetActive(true);
    }

    public void CloseCreditsPNL()
    {
        ClickSound();
        CreditsPnl.SetActive(false);
    }

    public void ExitCreditsPNL()
    {
        ClickSound();
        CreditsPnl.SetActive(false);
    }

    public void ExitGame()
    {
        ClickSound();
        Application.Quit();
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

    void SaveAudio()
    {
        PlayerPrefs.SetFloat("SoundBar", settings.soundBar.fillAmount);
        PlayerPrefs.SetFloat("Sound", AudioManager.instance.SFXSource.volume);

        PlayerPrefs.SetFloat("MusicBar", settings.musicBar.fillAmount);
        PlayerPrefs.SetFloat("Music", AudioManager.instance.MusicSource.volume);
    }
}
