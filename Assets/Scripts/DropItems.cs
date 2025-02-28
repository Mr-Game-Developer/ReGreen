using UnityEngine;

public class DropItems : MonoBehaviour
{
    public GameObject Axe;
    public GameObject FireExtinguisher;

    public void ItemDisable()
    {
        Axe.SetActive(false);
        FireExtinguisher.SetActive(false);
    }

    public void AxeEnable()
    {
        Axe.SetActive(true);
    }

    public void FireExtinguisherEnable()
    {
        FireExtinguisher.SetActive(true);
    }
}
