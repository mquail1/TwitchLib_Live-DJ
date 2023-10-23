using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script that stores the static Audience in an array and destroys one at random when actual player objects spawn in

public class AudienceArray : MonoBehaviour
{
    [Header("Arrays")]
    [SerializeField] private List<GameObject> staticAudienceArray;
    [SerializeField] private Spawner spawner;

    // 164 memebers in staticAudienceArray
    // (2 additional slots for buffer purposes)

    // Random number between 1 and 164

    // Instance Variables
    private GameObject destroyObject;
    private int arrayCheck = 0;
    private int randomNumber = 0;

    // Start is called before the first frame update
    void Start(){}

    // Update is called once per frame
    void Update()
    {
        // check for a player spawning into the player array
        if (spawner.arrayCounter > arrayCheck && staticAudienceArray.Count > 0)
        {
            destroyAudience();
        }
    }

    void destroyAudience()
    {
        // Generate a random index number
        randomNumber = Random.Range(0, staticAudienceArray.Count);

        // Debug
        Debug.Log("Current Array Count: " + staticAudienceArray.Count);

        // Debug
        Debug.Log("Random Number: " + randomNumber);
        
        // Choose an audience member at random
        destroyObject = staticAudienceArray[randomNumber];

        // Debug
        Debug.Log("Removing the following object: " + destroyObject);

        // Destroy audience member
        Destroy(destroyObject);

        // Debug
        Debug.Log("Removing the following index in array: " + randomNumber);

        // Remove element in list
        staticAudienceArray.RemoveAt(randomNumber);

        // Debug
        Debug.Log("Array Count after destruction: " + staticAudienceArray.Count);
        
        // increment arrayCheck by 1
        arrayCheck++;
    }
}
