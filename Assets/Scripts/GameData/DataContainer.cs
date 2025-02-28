using UnityEngine;

[CreateAssetMenu(fileName ="DataContainer", menuName ="Dev/GameData")]

public class DataContainer : ScriptableObject
{
   public GameData gamedata;

    private void OnEnable()
    {
        //gamedata = (GameData)ES3.Load("Gamedata");
        gamedata = (GameData)ES3.Load("gameData");
    }

    private void OnDisable()
    {
        ES3.Save("gameData", gamedata);
    }
}
