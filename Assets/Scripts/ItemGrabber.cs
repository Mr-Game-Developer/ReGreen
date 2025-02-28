using StarterAssets;
using System.Collections;
using UnityEngine;

public class ItemGrabber : MonoBehaviour
{
    // The distance from the player that the item can be grabbed
    public float grabDistance = 2.0f;

    // The player's hand game object
    public GameObject hand;
    public GameObject Bin;

    // The item that is currently being held
    private GameObject heldItem;

    Player_TreeChopping Interaction;
    CharacterController thirdPersonController;
    ThirdPersonController tpController;

    public GameObject PickHand;
    public GameObject ThrowHand;

    bool GrabingItem = false;
    bool ThrowingItem = false;

    private void Start()
    {
        Interaction = GetComponent<Player_TreeChopping>();
        thirdPersonController = GetComponentInChildren<CharacterController>();
        tpController = GetComponent<ThirdPersonController>();
        Interaction._animIDPickUp = Animator.StringToHash("PickUp");
        Interaction._animIDThrow = Animator.StringToHash("Throw");
    }

    public void Grab()
    {
        GrabingItem = true;
    }

    public void Throw()
    {
        ThrowingItem = true;
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject item;

        if (other.tag == "Item")
        {
            // Get the item that was hit
            item = other.transform.gameObject;

            PickHand.SetActive(true);

            // Check if the item can be grabbed
            if (item.GetComponent<GrabableItem>() != null && GrabingItem == true/*Input.GetMouseButtonDown(0)*/)
            {
                // Grab the item
                Interaction.tpController.enabled = false;
                thirdPersonController.enabled = false;
                StartCoroutine(GrabItem(item));
                animationGrab();
                GrabingItem = false;
            }
        }

        // Check if the player is trying to drop the item
        if (/*Input.GetMouseButtonDown(1)*/ ThrowingItem == true && heldItem != null && other.CompareTag("WasteBin"))
        {
            // Drop the item
            StartCoroutine(DropItem());
            animationThrow();
            ThrowingItem= false;
        }

        if (heldItem == null)
        {
            ThrowHand.SetActive(false);
        }
        else if (heldItem != null)
        {
            ThrowHand.SetActive(true);
            PickHand.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Item")
        {
            PickHand.SetActive(false);
        }
    }

    IEnumerator GrabItem(GameObject item)
    {
        Debug.Log("Grab");
        yield return new WaitForSeconds(0.5f);
        // Set the held item
        heldItem = item;

        // Parent the item to the hand
        item.transform.parent = hand.transform;

        // Set the item's position and rotation to match the hand
        item.transform.localPosition = new Vector3(40f, 5f, 0);
        item.transform.localRotation = Quaternion.Euler(0f ,0f ,90f);
        item.GetComponent<Rigidbody>().isKinematic = true;
    }

    IEnumerator DropItem()
    {
        Interaction.tpController.enabled = false;
        thirdPersonController.enabled = false;

        yield return new WaitForSeconds(2.5f); // slight delay to ensure hand is open

        // Calculate the direction of the throw based on the camera's forward direction
        //Vector3 throwDirection = Camera.main.transform.forward + Camera.main.transform.up;
        Debug.Log("Throw");
        heldItem.GetComponent<Rigidbody>().isKinematic = false;
        //heldItem.GetComponent<Rigidbody>().AddForce(throwDirection * 5f, ForceMode.Impulse);

        // Unparent the item from the hand
        heldItem.transform.parent = Bin.transform;
        heldItem.transform.position = new Vector3(-17.27f, 0.2f, 84.18f) /*null*/;

        // Set the held item to null
        heldItem = null;
        //Controllers();
    }

    void animationGrab()
    {
        Interaction.animator.SetBool(Interaction._animIDPickUp, true);
        Invoke("anim", 1f);
    }
    
    void animationThrow()
    {
        Interaction.animator.SetBool(Interaction._animIDThrow, true);
        Invoke("anim", 1f);
    }

    void anim()
    {
        if (Interaction.animator.GetBool(Interaction._animIDPickUp) == true)
        {
            Interaction.animator.SetBool(Interaction._animIDPickUp, false);
        }
        
        if (Interaction.animator.GetBool(Interaction._animIDThrow) == true)
        {
            Interaction.animator.SetBool(Interaction._animIDThrow, false);
        }

        Invoke("Controllers", 0.5f);
    }

    void Controllers()
    {
        Interaction.tpController.enabled = true;
        thirdPersonController.enabled = true;
    }
}