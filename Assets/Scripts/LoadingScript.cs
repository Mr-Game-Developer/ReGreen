using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour
{
    public Image Loadder;
    int starting = 0;

    private void Start()
    {
        int Menu = PlayerPrefs.GetInt("Menu");
        int ReGreenGameplay = PlayerPrefs.GetInt("Gameplay");
        int RestartGameplay = PlayerPrefs.GetInt("ReGameplay");

        starting = PlayerPrefs.GetInt("isStarting", starting);

        if (PlayerPrefs.GetInt("isStarting") <= 1)
        {
            SceneLoad(1);
            starting++;
            PlayerPrefs.SetInt("isStarting", starting);
        }
        else if (RestartGameplay == 5)
        {
            SceneLoad(2);
        }
        else if (ReGreenGameplay == 3)
        {
            SceneLoad(2);
        }
        else if (Menu == 4)
        {
            SceneLoad(1);
        }
    }

    public void SceneLoad(int SceneNum)
    {
        StartCoroutine(AsyncLoadScene(SceneNum));
    }

    IEnumerator AsyncLoadScene(int Scene)
    {
        AsyncOperation Operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(Scene);
        while (!Operation.isDone)
        {
            //yield return new WaitForSeconds(0.01f);
            Loadder.fillAmount += Operation.progress;
            PlayerPrefs.SetInt("Gameplay", 0);
            PlayerPrefs.SetInt("ReGameplay", 0);
            PlayerPrefs.SetInt("Menu", 0);
            yield return null;
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("isStarting", 0);
        PlayerPrefs.SetInt("Menu", 0);
        PlayerPrefs.SetInt("Gameplay", 0);
        PlayerPrefs.SetInt("ReGameplay", 0);
    }
}