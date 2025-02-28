using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    // UI elements
    public Image soundBar;
    public Image musicBar;

    // Audio manager instance
    private AudioManager audioManager;

    private void Start()
    {
        // Get the audio manager instance
        audioManager = AudioManager.instance;

        // Initialize audio sources
        audioManager.MusicSource = GameObject.FindWithTag("Music").GetComponent<AudioSource>();
        audioManager.SFXSource = GameObject.FindWithTag("SFX").GetComponent<AudioSource>();

        // Load saved audio settings
        LoadAudioSettings();

        // Play music
        audioManager.PlayMusic("Theme");
    }

    // Load saved audio settings
    private void LoadAudioSettings()
    {
        // Check if settings have been changed
        if (PlayerPrefs.GetInt("SettingChanged") == 1)
        {
            // Load sound and music settings
            LoadSoundSettings();
            LoadMusicSettings();
        }
    }

    // Load sound settings
    private void LoadSoundSettings()
    {
        // Get saved sound volume and fill amount
        float soundVolume = PlayerPrefs.GetFloat("Sound");
        float soundFillAmount = PlayerPrefs.GetFloat("SoundBar");

        // Set sound volume and fill amount
        audioManager.SFXSource.volume = soundVolume;
        soundBar.fillAmount = soundFillAmount;
    }

    // Load music settings
    private void LoadMusicSettings()
    {
        // Get saved music volume and fill amount
        float musicVolume = PlayerPrefs.GetFloat("Music");
        float musicFillAmount = PlayerPrefs.GetFloat("MusicBar");

        // Set music volume and fill amount
        audioManager.MusicSource.volume = musicVolume;
        musicBar.fillAmount = musicFillAmount;
    }

    // Increase sound volume
    public void IncreaseSoundVolume()
    {
        // Play button click sound
        ButtonClickSound();

        // Increase sound volume and fill amount
        audioManager.SFXSource.volume = Mathf.Min(audioManager.SFXSource.volume + 0.1f, 1f);
        soundBar.fillAmount = Mathf.Min(soundBar.fillAmount + 0.1f, 1f);

        // Save sound settings
        SaveSoundSettings();
    }

    // Decrease sound volume
    public void DecreaseSoundVolume()
    {
        // Play button click sound
        ButtonClickSound();

        // Decrease sound volume and fill amount
        audioManager.SFXSource.volume = Mathf.Max(audioManager.SFXSource.volume - 0.1f, 0f);
        soundBar.fillAmount = Mathf.Max(soundBar.fillAmount - 0.1f, 0f);

        // Save sound settings
        SaveSoundSettings();
    }

    // Increase music volume
    public void IncreaseMusicVolume()
    {
        // Play button click sound
        ButtonClickSound();

        // Increase music volume and fill amount
        audioManager.MusicSource.volume = Mathf.Min(audioManager.MusicSource.volume + 0.1f, 1f);
        musicBar.fillAmount = Mathf.Min(musicBar.fillAmount + 0.1f, 1f);

        // Save music settings
        SaveMusicSettings();
    }

    // Decrease music volume
    public void DecreaseMusicVolume()
    {
        // Play button click sound
        ButtonClickSound();

        // Decrease music volume and fill amount
        audioManager.MusicSource.volume = Mathf.Max(audioManager.MusicSource.volume - 0.1f, 0f);
        musicBar.fillAmount = Mathf.Max(musicBar.fillAmount - 0.1f, 0f);

        // Save music settings
        SaveMusicSettings();
    }

    // Save sound settings
    private void SaveSoundSettings()
    {
        // Save sound volume and fill amount
        PlayerPrefs.SetFloat("Sound", audioManager.SFXSource.volume);
        PlayerPrefs.SetFloat("SoundBar", soundBar.fillAmount);

        // Set settings changed flag
        PlayerPrefs.SetInt("SettingChanged", 1);
    }

    // Save music settings
    private void SaveMusicSettings()
    {
        // Save music volume and fill amount
        PlayerPrefs.SetFloat("Music", audioManager.MusicSource.volume);
        PlayerPrefs.SetFloat("MusicBar", musicBar.fillAmount);

        // Set settings changed flag
        PlayerPrefs.SetInt("SettingChanged", 1);
    }

    // Play button click sound
    private void ButtonClickSound()
    {
        audioManager.PlaySFX("Click");
    }
}
