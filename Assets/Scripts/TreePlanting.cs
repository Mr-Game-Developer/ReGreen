using StarterAssets;
using System.Collections;
using UnityEngine;

public class TreePlanting : MonoBehaviour
{
    [SerializeField] private GameObject[] treePrefab; // Prefab of the tree to be planted
    ThirdPersonController tpController;
    Player_TreeChopping tpcTree;
    EnvironmentalChanges _changes;
    DropItems _dropItems;
    PlayerProgress _progress;
    InteractAxe _interactionAxe;
    InteractFire _interactionExtinguisher;
    [HideInInspector] public bool isValidLocation;
    public ParticleSystem PlantationEffect;
    public float treeSpacing = 2f; // Distance between trees

    private void Start()
    {
        tpController = FindAnyObjectByType<ThirdPersonController>();
        tpcTree = FindAnyObjectByType<Player_TreeChopping>();
        _changes = FindAnyObjectByType<EnvironmentalChanges>();
        _progress = FindAnyObjectByType<PlayerProgress>();
        _dropItems = FindAnyObjectByType<DropItems>();
        _interactionAxe = FindAnyObjectByType<InteractAxe>();
        _interactionExtinguisher = FindAnyObjectByType<InteractFire>();
        tpcTree._animIDPlant = Animator.StringToHash("Plant");
    }

    // Method to be called when the raycast button is pressed
    public void OnPressPlant()
    {
        GameObject area = GameObject.FindWithTag("HitArea");
        Ray ray = new Ray(area.transform.position, new Vector3(0f, -1f, 0f)); // Diagonal forward direction
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // Check if the hit object is the ground
            if (hit.collider.CompareTag("ForestGround") && tpController.Grounded)
            {
                isValidLocation = true;

                // Check if there is already a tree within the tree spacing distance
                Collider[] hitColliders = Physics.OverlapSphere(hit.point, treeSpacing);
                foreach (var hitCollider in hitColliders)
                {
                    if (hitCollider.CompareTag("Tree"))
                    {
                        isValidLocation = false;
                        break;
                    }
                }

                // Store the last valid hit point for drawing Gizmos
                if (isValidLocation)
                {
                    _dropItems.ItemDisable();
                    StartCoroutine(PlantTree(hit.point));
                    tpcTree.animator.SetBool(tpcTree._animIDPlant, true);
                    Invoke("PlantAnim", 2f);
                }
                else
                {
                    Debug.Log("Not Valid Area");
                }
            }
        }
        else
        {
            Debug.Log("Not Valid Area");
        }
    }

    // Method to plant a tree at a specific position
    IEnumerator PlantTree(Vector3 position)
    {
        ParticleSystem Effect = Instantiate(PlantationEffect, position, Quaternion.identity);
        yield return new WaitForSeconds(20);
        // Instantiate tree prefab at the given position
        GameObject tree = Instantiate(treePrefab[Random.Range(0, treePrefab.Length)], position, Quaternion.identity);
        Destroy(Effect.gameObject);
        tree.transform.parent = transform; // Parent the tree to this object
        tree.tag = "Tree";
        _changes.PlantTree();
        _progress.EarnGreenziCoins();
        _progress.LossRupees();

        if(_interactionAxe.ChoppingUIBTN.activeSelf)
        {
            _dropItems.AxeEnable();
        }
        else if (_interactionExtinguisher.WaterExtingusiher.activeSelf)
        {
            _dropItems.FireExtinguisherEnable();
        }
    }

    void PlantAnim()
    {
        if (tpcTree.animator.GetBool(tpcTree._animIDPlant) == true)
        {
            tpcTree.animator.SetBool(tpcTree._animIDPlant, false);
        }

        tpController.enabled = true;
    }
}