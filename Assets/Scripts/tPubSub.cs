using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TwitchLib.Unity;
using TwitchLib.PubSub;
using TwitchLib.PubSub.Events;
using System;

public class tPubSub : MonoBehaviour
{
    // External Object References
    [Header("External Object References")]
    [SerializeField] private PubSub pubSub;
    [SerializeField] private Spawner spawner;
    [SerializeField] private GameObject audienceCamBrain;
    [SerializeField] private GameObject audienceCanvas;

    [SerializeField] private bossBattle bossbattle;

    // Instance Variables
    private GameObject clone;
    private GameObject audienceCam;


    void Start()
    {
        // Create new instance of PubSub Client
        pubSub = new PubSub();
        
        // Run the application in bg at all times ('Preferences' option doesn't always work)
        Application.runInBackground = true;

        // Set Connect Event
        pubSub.OnPubSubServiceConnected += OnPubSubServiceConnected;

        // Set Chatter Events
        pubSub.OnWhisper += OnWhisper;
        pubSub.OnRewardRedeemed += OnRewardRedeemed;
        pubSub.OnBitsReceived += OnBitsReceived;
        pubSub.OnChannelSubscription += OnChannelSubscription;

        // Set Error Handling Events
        pubSub.OnListenResponse += OnListenResponse;
        pubSub.OnPubSubServiceError += OnPubSubServiceError;

        // Connect PubSub to channel
        pubSub.Connect();
    }

    // PubSub Connected
    private void OnPubSubServiceConnected(object sender, EventArgs e)
    {
       // Listen for Events
        pubSub.ListenToWhispers(Secrets.channel_id); 
        pubSub.ListenToRewards(Secrets.channel_id); // CHANNEL ID
        pubSub.ListenToBitsEvents(Secrets.channel_id);
        
        // Send Topics
        pubSub.SendTopics(Secrets.client_access_token); // OAUTH TOKEN
        
        // Connection Successful
		Debug.Log("PubSub Service Connected");
    }

	private void OnWhisper(object sender, TwitchLib.PubSub.Events.OnWhisperArgs e)
	{
	    // Debug.Log($"{e.Whisper.Data}");
	    // Bits Logic Here
	}


    // Channel Point Rewards
    private void OnRewardRedeemed(object sender, OnRewardRedeemedArgs e)
    {
        Debug.Log("Reward Redeemed:" + e);

        // Reward title must EXACTLY MATCH on Twitch Dashboard
        switch(e.RewardTitle)
        {
            // --- Audience Redeems ---
            case "Spawn Generic Avatar":
                if(e.Status == "UNFULFILLED")
                {
                    // spawn in an audience member
                    // e.DisplayName = username of redeem requester
                    // option 1 : no message attached
                    // option 2 : message attached
                    // kind of moot since twitch will force the user to create input; refactor
                    if (e.Message == null)
                    {
                        spawner.spawnAudience(e.DisplayName);
                    }
                    else
                    {
                        spawner.spawnAudience(e.DisplayName, e.Message);
                    }
                }
                break;
            case "Spawn Custom Avatar":
                if(e.Status == "UNFULFILLED")
                {
                    // spawn in custom audience member
                    spawner.spawnCustomAudience(e.DisplayName, e.Message);
                }
                break;
        
            case "Look At Me!":
                if(e.Status == "UNFULFILLED") // add check here or internally to make sure the redeem corresponds to an actual object in game
                {
                    // object1.transform.parent = object2.transform -> force an object to be a child of another object
                    // ^ for future implementations
                    StartCoroutine(activateAudienceMemberCam(e.DisplayName));
                }
                break;
            
            // Pubsub reset functionality / testing
            case "Pubsub Reset Test":
                if(e.Status == "UNFULFILLED") // add check here or internally to make sure the redeem corresponds to an actual object in game
                {
                    Debug.Log("TwitchLib Reset Test Successful");
                }
                break;
            
            // --- Boss Battle Redeems --- 
            case "Boss Attack : Light":
                if(e.Status == "UNFULFILLED")
                {
                    bossbattle.audienceAttack(e.DisplayName, "attack1");
                }
                break;
            case "Boss Attack : Medium":
                if(e.Status == "UNFULFILLED")
                {
                    bossbattle.audienceAttack(e.DisplayName, "attack2");
                }
                break;
            case "Boss Attack : Heavy":
                if(e.Status == "UNFULFILLED")
                {
                    bossbattle.audienceAttack(e.DisplayName, "attack3");
                }
                break;
            case "Boss Attack : Ultra":
                if(e.Status == "UNFULFILLED")
                {
                    bossbattle.audienceAttack(e.DisplayName, "attack4");
                }
                break;
        }
    }

    // Bits Event
    private void OnBitsReceived(object sender, OnBitsReceivedArgs e)
    {
        // check how many bits were received
        
        // between 1 and 100 bits
        if (e.BitsUsed > 0 && e.BitsUsed < 100)
        {
            spawner.spawnBits("bit1");
        }

        // between 100 and 1000 bits
        if (e.BitsUsed >= 100 && e.BitsUsed < 1000)
        {
            spawner.spawnBits("bit100");
        }

        if (e.BitsUsed >= 1000 && e.BitsUsed < 10000)
        {
            spawner.spawnBits("bit1000");
        }

        else if (e.BitsUsed >= 10000)
        {
            spawner.spawnBits("bit10000");
        }

        // spawn appropriate bits object via spawner
    }

    // Channel Subscription Event
    private void OnChannelSubscription(object sender, OnChannelSubscriptionArgs e)
    {}

    // Listening Failed
    private void OnListenResponse(object sender, OnListenResponseArgs e)
    {
        if(!e.Successful)
        {
            Debug.Log("Failed to listen! Response: " + e.Response.Error);
            Debug.Log("Topic: " + e.Topic);
        }
    }

    // PubSub Service Connection Error
    private void OnPubSubServiceError(object sender, OnPubSubServiceErrorArgs e)
    {
        Debug.Log("Error: " + e.Exception.Message);
    }

    // IEnum to activate PiP Audience Member Cam for 20 seconds
    private IEnumerator activateAudienceMemberCam(string thisName)
    {
        // traverse the audience array
        foreach(GameObject obj in spawner.audienceArray)
        {
            // if the object in the audience array matches the username who redeemed
            if (obj.name == thisName)
            {
                // store this object
                clone = obj;
            }
        }

        // if no username was found that matches
        if (clone == null)
        {
            Debug.Log("No matching username found in array.");
            yield break;
        }

        // get the audienceVC child in the clone we just grabbed
        audienceCam = GetChildWithName(clone, "audienceVC");

        // if the function didn't find audienceVC object
        if (audienceCam == null)
        {
            Debug.Log("GetChildWithName() returned null for " + clone);
        }

        // Activate audienceCam
        audienceCam.SetActive(true);

        // Activate audienceCam Cinemachine Brain controller
        audienceCamBrain.SetActive(true);
        
        // Activate audienceCam UI Text
        audienceCanvas.SetActive(true);
        
        // Wait for 20 seconds
        yield return new WaitForSeconds(20);

        // Deactivate audienceCam
        audienceCam.SetActive(false);

        // De-activate audienceCam Cinemachine Brain controller
        audienceCamBrain.SetActive(false);        

        // Deactivate audienceCam UI Text
        audienceCanvas.SetActive(false);

        // set clone back to null
        clone = null;
            
        // exit coroutine
        yield break;
    }

    // Method that retrieves a child with a specific name from a specific game object
    GameObject GetChildWithName(GameObject obj, string name) 
    {
        Transform trans = obj.transform;
        Transform childTrans = trans. Find(name);
        if (childTrans != null) 
        {
            return childTrans.gameObject;
        } 
        
        else 
        {
            return null;
        }
    }

    // Generic version of GetChildWithName
    private T GetChildComponentByName<T>(string name) where T : Component {
        foreach (T component in GetComponentsInChildren<T>(true)) {
            if (component.gameObject.name == name) {
                return component;
            }
        }
        return null;
    }
}