using System.Collections;
using UnityEngine;

public class InteractFire : MonoBehaviour/*, IInteractable*/
{
    public ParticleSystem Fire, Water;
    public AudioSource fireSound;
    public GameObject WaterExtingusiher;
    InteractAxe Axe;

    private void Start()
    {
        Axe = GetComponent<InteractAxe>();
        PlayerPrefs.SetInt("Fire", 1);
    }
    
    void FireExtinguisher()
    {
        Fire.Stop();
        fireSound.Stop();
        Invoke("RemoveExtinguisher", 2f);
    }

    void RemoveExtinguisher()
    {
        WaterExtingusiher.SetActive(false);
    }

    public void InteractWithFire()
    {
        if (WaterExtingusiher.activeSelf)
        {
            Water.Play();
            PlayerPrefs.SetInt("Fire", 0);
            Invoke("FireExtinguisher", 0.1f);
        }
    }

    public void InteractWithExtinguisher()
    {
        if (Axe.Axe.activeSelf)
        {
            Axe.Axe.SetActive(false);
            WaterExtingusiher.SetActive(true);
            PlayerPrefs.SetInt("Extinguisher", 1);
        }
    }
}
