using StarterAssets;
using System.Collections;
using UnityEngine;

public class InteractSolar_Turbine : MonoBehaviour
{
    public GameObject SolarPanel, WindTurbine;
    public GameObject BuildAreaBTN;
    public GameObject Hammer;
    public GameObject HomeSolarLight, HomeTurbineLight;
    public ParticleSystem SolarParticle, TurbineParticle;
    Player_TreeChopping tpcSolar_Turbine;
    ThirdPersonController tpController;
    InteractAxe Axe;
    public float SpaceBetween = 5f;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Build", 0);
        tpController = FindAnyObjectByType<ThirdPersonController>();
        tpcSolar_Turbine = FindAnyObjectByType<Player_TreeChopping>();
        Axe = FindAnyObjectByType<InteractAxe>();
        tpcSolar_Turbine._animIDBuild = Animator.StringToHash("Build");
    }

    public void Solar_Turbine()
    {
        PlayerPrefs.SetInt("Build", 1);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("SolarArea"))
        {
            BuildAreaBTN.SetActive(true);
            if(PlayerPrefs.GetInt("Build") == 1 || Input.GetKey(KeyCode.T))
            {
                GameObject area = GameObject.FindWithTag("HitArea");
                Ray ray = new Ray(area.transform.position, new Vector3(0f, -1f, 0f)); // Diagonal forward direction
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    // Check if the hit object is the ground
                    if (hit.collider.CompareTag("ForestGround") && tpController.Grounded)
                    {
                        bool isValidLocation = true;

                        // Check if there is already a tree within the tree spacing distance
                        Collider[] hitColliders = Physics.OverlapSphere(hit.point, SpaceBetween);
                        foreach (var hitCollider in hitColliders)
                        {
                            if (hitCollider.CompareTag("Solar"))
                            {
                                isValidLocation = false;
                                break;
                            }
                        }

                        // Store the last valid hit point for drawing Gizmos
                        if (isValidLocation)
                        {
                            PlayerPrefs.SetInt("Build", 0);
                            Hammer.SetActive(true);
                            StartCoroutine(BuildSolar(hit.point));
                            tpcSolar_Turbine.animator.SetBool(tpcSolar_Turbine._animIDBuild, true);
                            Invoke("BuildAnim", 2f);
                        }
                        else
                        {
                            Debug.Log("Not Valid Area");
                        }
                    }
                }
            }
        }

        if (other.CompareTag("TurbineArea"))
        {
            BuildAreaBTN.SetActive(true);
            if (PlayerPrefs.GetInt("Build") == 1)
            {
                GameObject area = GameObject.FindWithTag("HitArea");
                Ray ray = new Ray(area.transform.position, new Vector3(0f, -1f, 0f)); // Diagonal forward direction
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    // Check if the hit object is the ground
                    if (hit.collider.CompareTag("ForestGround") && tpController.Grounded)
                    {
                        bool isValidLocation = true;

                        // Check if there is already a tree within the tree spacing distance
                        Collider[] hitColliders = Physics.OverlapSphere(hit.point, SpaceBetween);
                        foreach (var hitCollider in hitColliders)
                        {
                            if (hitCollider.CompareTag("Turbine"))
                            {
                                isValidLocation = false;
                                break;
                            }
                        }

                        // Store the last valid hit point for drawing Gizmos
                        if (isValidLocation)
                        {
                            PlayerPrefs.SetInt("Build", 0);
                            StartCoroutine(BuildTurbine(hit.point));
                            tpcSolar_Turbine.animator.SetBool(tpcSolar_Turbine._animIDBuild, true);
                            Invoke("BuildAnim", 2f);
                        }
                        else
                        {
                            Debug.Log("Not Valid Area");
                        }
                    }
                }
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if(collider.CompareTag("SolarArea") || collider.CompareTag("TurbineArea"))
        {
            BuildAreaBTN.SetActive(false);
        }
    }

    IEnumerator BuildSolar(Vector3 position)
    {
        Axe.Axe.SetActive(false);
        ParticleSystem Effect = Instantiate(SolarParticle, position, Quaternion.identity);
        yield return new WaitForSeconds(20);
        // Instantiate Solar prefab at the given position
        GameObject solarPanel = Instantiate(SolarPanel, position, Quaternion.Euler(0f, 145f, 0f));
        solarPanel.tag = "Solar";
        HomeSolarLight.SetActive(true);
        Destroy(Effect.gameObject);
    }
    
    IEnumerator BuildTurbine(Vector3 position)
    {
        Axe.Axe.SetActive(false);
        Axe.ChoppingUIBTN.SetActive(false);
        ParticleSystem Effect = Instantiate(TurbineParticle, position, Quaternion.identity);
        yield return new WaitForSeconds(20);
        // Instantiate Solar prefab at the given position
        GameObject windTurbine = Instantiate(WindTurbine, position + new Vector3(0f, 4.5f, 0f), Quaternion.Euler(-90f, 0f, 0f));
        windTurbine.tag = "Turbine";
        HomeTurbineLight.SetActive(true);
        Destroy(Effect.gameObject);
    }

    void BuildAnim()
    {
        if (tpcSolar_Turbine.animator.GetBool(tpcSolar_Turbine._animIDBuild) == true)
        {
            tpcSolar_Turbine.animator.SetBool(tpcSolar_Turbine._animIDBuild, false);
        }
        
        tpController.enabled = true;
        Hammer.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("NoOfSolars", 0);
        PlayerPrefs.SetInt("NoOfTurbine", 0);
    }
}
