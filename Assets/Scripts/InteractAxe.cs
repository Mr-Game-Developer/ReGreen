using UnityEngine;

public class InteractAxe : MonoBehaviour
{
    public GameObject Axe;
    public GameObject ChoppingUIBTN;
    Player_TreeChopping Interaction;
    InteractFire Extinguisher;

    private void Start()
    {
        Interaction = GetComponent<Player_TreeChopping>();
        Extinguisher = GetComponent<InteractFire>();
    }

    public void AxeInteract()
    {
        if(!Extinguisher.WaterExtingusiher.activeSelf)
        {
            Axe.SetActive(true);
            ChoppingUIBTN.SetActive(true);
        }
    }
}
