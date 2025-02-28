using UnityEngine;
using UnityEngine.UI;

public class PauseUISettingSound : MonoBehaviour
{
    public Image SoundBar, MusicBar;

    private void Start()
    {
        AudioManager.instance.MusicSource = GameObject.FindWithTag("Music").GetComponent<AudioSource>();
        AudioManager.instance.SFXSource = GameObject.FindWithTag("SFX").GetComponent<AudioSource>();

        if (PlayerPrefs.GetInt("SettingChanged") == 1)
        {
            Debug.Log("SettingChanged");
            LoadSound();
            LoadMusic();
        }

        AudioManager.instance.PlayMusic("Theme");
    }

    // for Music and All Sounds On and Off
    public void SoundIncreaseButton()
    {
        ButtonClickSound();
        if(AudioManager.instance.SFXSource.volume != 1)
        {
            SoundBar.fillAmount += 0.10f;
            AudioManager.instance.SFXSource.volume += 0.10f;
            AudioListener.volume += 0.10f;
        }
        
        AudioManager.instance.SFXSource.mute = false;

        PlayerPrefs.SetInt("SettingChanged", 1);
        PlayerPrefs.SetFloat("SoundBar", SoundBar.fillAmount);
        PlayerPrefs.SetFloat("Sound", AudioManager.instance.SFXSource.volume);
    }

    public void SoundDecreaseButton()
    {
        ButtonClickSound();
        if (SoundBar.fillAmount == 0f || AudioManager.instance.SFXSource.volume == 0f)
        {
            AudioManager.instance.SFXSource.mute = true;
        }
        AudioListener.volume -= 0.10f;
        SoundBar.fillAmount -= 0.10f;
        
        AudioManager.instance.SFXSource.volume -= 0.10f;

        PlayerPrefs.SetInt("SettingChanged", 1);
        PlayerPrefs.SetFloat("SoundBar", SoundBar.fillAmount);
        PlayerPrefs.SetFloat("Sound", AudioManager.instance.SFXSource.volume);
    }

    public void MusicIncreaseButton()
    {
        ButtonClickSound();
        if(AudioManager.instance.MusicSource.volume != 1)
        {
            MusicBar.fillAmount += 0.10f;
            AudioManager.instance.MusicSource.volume += 0.10f;
        }
        
        AudioManager.instance.MusicSource.mute = false;

        PlayerPrefs.SetInt("SettingChanged", 1);
        PlayerPrefs.SetFloat("MusicBar", MusicBar.fillAmount);
        PlayerPrefs.SetFloat("Music", AudioManager.instance.MusicSource.volume);
    }

    public void MusicDecreaseButton()
    {
        ButtonClickSound();
        if (MusicBar.fillAmount == 0f || AudioManager.instance.MusicSource.volume == 0f)
        {
            AudioManager.instance.MusicSource.mute = true;
        }
        MusicBar.fillAmount -= 0.10f;
        AudioManager.instance.MusicSource.volume -= 0.10f;

        PlayerPrefs.SetInt("SettingChanged", 1);
        PlayerPrefs.SetFloat("MusicBar", MusicBar.fillAmount);
        PlayerPrefs.SetFloat("Music", AudioManager.instance.MusicSource.volume);
    }

    void LoadSound()
    {
        SoundBar.fillAmount = PlayerPrefs.GetFloat("SoundBar");
        AudioManager.instance.SFXSource.volume = PlayerPrefs.GetFloat("Sound");
    }
    void LoadMusic()
    {
        MusicBar.fillAmount = PlayerPrefs.GetFloat("MusicBar");
        AudioManager.instance.MusicSource.volume = PlayerPrefs.GetFloat("Music");
    }

    void ButtonClickSound()
    {
        AudioManager.instance.PlaySFX("Click");
    }
}
