using UnityEngine;
using UnityEngine.UI;

public class appleHealth : MonoBehaviour
{ 
    public DataContainer gdata;
    EnvironmentalChanges heart;

    private void Start()
    {
        heart = FindAnyObjectByType<EnvironmentalChanges>();
    }

    public void Eat()
    {
        if(gdata.gamedata.PlayerHealth < 100)
        {
            gdata.gamedata.PlayerHealth += 1;
            heart.health.text = gdata.gamedata.PlayerHealth.ToString();
        }
        Destroy(gameObject);
    }
}