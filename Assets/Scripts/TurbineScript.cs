using UnityEngine;

public class TurbineScript : MonoBehaviour
{
    public float turbineSpeed = 2f; 

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 1f * turbineSpeed, 0f);
    }
}
