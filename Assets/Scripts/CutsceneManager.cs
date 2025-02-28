using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    public PlayableDirector cutsceneDirector;
    public GameObject gameplayUI;
    public GameObject player;

    void Start()
    {
        cutsceneDirector.stopped += OnCutsceneEnded;
        StartCutscene();
    }

    void StartCutscene()
    {
        gameplayUI.SetActive(false);
        player.GetComponent<CharacterController>().enabled = false; // Disable player controls
        cutsceneDirector.Play();
    }

    void OnCutsceneEnded(PlayableDirector director)
    {
        gameplayUI.SetActive(true);
        player.GetComponent<CharacterController>().enabled = true; // Enable player controls
    }
}
