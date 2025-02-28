using UnityEngine;
using UnityEngine.UI;

public class PlayerProgress : MonoBehaviour
{
    public Text GreenziCoin;
    public Text Rupees;
    public Text PlayerHealth;
    public DataContainer gdata;

    // Start is called before the first frame update
    void Start()
    {
        GreenziCoin.text = gdata.gamedata.Greenzi.ToString();
        Rupees.text = gdata.gamedata.Rs.ToString();
    }

    public void EarnGreenziCoins()
    {
        gdata.gamedata.Greenzi += 20;
        GreenziCoin.text = gdata.gamedata.Greenzi.ToString();
    }
    
    public void LossGreenziCoins(int amount)
    {
        if(gdata.gamedata.Greenzi !< 10)
        {
            gdata.gamedata.Greenzi -= amount;
            GreenziCoin.text = gdata.gamedata.Greenzi.ToString();
        }
    }
    
    public void EarnRupees(int amount)
    {
        gdata.gamedata.Rs += amount;
        Rupees.text = gdata.gamedata.Rs.ToString();
    }
    
    public void LossRupees()
    {
        if(gdata.gamedata.Rs !< 200)
        {
            gdata.gamedata.Rs -= 10;
            Rupees.text = gdata.gamedata.Rs.ToString();
        }
    }
}
