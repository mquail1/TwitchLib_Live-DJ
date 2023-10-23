using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class bossBattle : MonoBehaviour
{
    // External References
    [Header("GameObject References")]
    [SerializeField] private GameObject bossObject;
    [SerializeField] private Slider bossHealth; //bossHealth.value = value of bar
    [SerializeField] private Animator bossAnimator;
    [SerializeField] private GameObject bossUI; // canvas UI object
    [SerializeField] private TextMeshProUGUI battleTicker; // displays actions via text onscreen

    // Cinemachine Setup
    [Header("Camera References")]
    [SerializeField] private CamController camController; 
    [SerializeField] private GameObject deathCam;

    // Battle / Attack Settings
    [Header("Attack Settings")]
    [SerializeField] public int hitHP1Lower;
    [SerializeField] public int hitHP1Upper;
    [SerializeField] public int hitHP2Lower;
    [SerializeField] public int hitHP2Upper;  
    [SerializeField] public int hitHP3Lower;
    [SerializeField] public int hitHP3Upper;
    [SerializeField] public int hitHP4Lower;
    [SerializeField] public int hitHP4Upper;
    [SerializeField] private int healSmallLower;
    [SerializeField] private int healSmallUpper;
    [SerializeField] private int healLargeLower;
    [SerializeField] private int healLargeUpper;

    // GUI Button Settings
    [Header("GUI Button Settings")]
    [SerializeField] private int GUIButtonWidth;
    [SerializeField] private int GUIButtonHeight;
    [SerializeField] private int xOffset;
    [SerializeField] private int yOffset;

    // Smoothing Parameters
    [Header("Smoothing Settings")]
    [SerializeField] private float bossHealthSmoothingVelocity;
    [SerializeField] private float tickerTextSmoothingVelocity;

    [Header("Fade Out Reference")]
    [SerializeField] private GameObject fadeToBlack;

    // Instance Variables
    private int randomAnimation = 0;

    // Smoothing Instance Variables
    private float bossHealthTarget = 0;
    private float bossHealthCurrent = 0;
    private float textAlphaTarget = 0;
    private float textAlphaCurrent = 0;
    private Color newColor;

    // Perform some check for "Environment 2" to trigger -> spawn boss (SetActive())

    // Start is called before the first frame update
    void Start()
    {
        // set Boss Health smoothing value to its max value
        bossHealthTarget = bossHealth.maxValue;
        // set battleticker color to new constructor
        newColor = new Color(0f, 190f, 228f, 255f);
        // set text Alpha to target value
        textAlphaTarget = battleTicker.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        // check for HP hitting 0
        if (bossHealth.value <= 100)
        {
            // save death state
            triggerDeathSave();
        }

        if(bossHealth.value != bossHealthTarget)
        {
            // Smooth damp that shit
            bossHealthCurrent = Mathf.SmoothDamp(bossHealth.value, bossHealthTarget, ref bossHealthSmoothingVelocity, 100 * Time.deltaTime);
            bossHealth.value = bossHealthCurrent;
        }

        // {text}.Color.a = value where 0 is transparent and 255 is opaque
        // can't set that stupid fucker manually, gotta call a constructor I guess. boooo
        if(textAlphaTarget != battleTicker.color.a)
        {
            textAlphaCurrent = Mathf.SmoothDamp(battleTicker.color.a, textAlphaTarget, ref tickerTextSmoothingVelocity, 100 * Time.deltaTime);
            // Call a constructor because text.color.a is read-only like a little whiney babey
            newColor = new Color(0f, 190f, 228f, textAlphaCurrent);
            battleTicker.color = newColor;
        }

        // The below checks fuck up the smoothing
        // Ideally you would want to reset the velocity more efficiently and outside of Update()

        /*
        // Clamp the smoothing velocity once it gets close enough to zero (or shoots way the hell out of bounds)
        if ((bossHealthSmoothingVelocity < 0 && bossHealthSmoothingVelocity > -1) || (bossHealthSmoothingVelocity < 1 && bossHealthSmoothingVelocity > 0) || bossHealthSmoothingVelocity > 95)
        {
            // set back to inspector value? or to 0?
            bossHealthSmoothingVelocity = 0;
        }

        // Clamp the smoothing velocity once it gets close enough to zero (or shoots way the hell out of bounds)
        if ((tickerTextSmoothingVelocity < 0 && tickerTextSmoothingVelocity > -1) || (tickerTextSmoothingVelocity < 1 && tickerTextSmoothingVelocity > 0) || tickerTextSmoothingVelocity > 95)
        {
            // set back to inspector value? or to 0?
            tickerTextSmoothingVelocity = 0;
        }
        */
    }

    public void audienceAttack(string username, string damageType)
    {
        switch(damageType)
        {
            case "attack1":
                // deal HP1 damage to health bar
                float hit1 = Random.Range(hitHP1Lower, hitHP1Upper);
                bossHealthTarget -= hit1;
                // trigger knockback
                triggerKnockbackAnimation();
                // display attack information
                StartCoroutine(displayAttackInfo(hit1,username));
                break;
            case "attack2":
                // deal HP2 damage to health bar
                float hit2 = Random.Range(hitHP2Lower, hitHP2Upper);
                bossHealthTarget -= hit2;
                triggerKnockbackAnimation();
                StartCoroutine(displayAttackInfo(hit2, username));
                break;
            case "attack3":
                // deal HP3 damage to health bar
                float hit3 = Random.Range(hitHP3Lower, hitHP3Upper);
                bossHealthTarget -= hit3;
                triggerKnockbackAnimation();
                StartCoroutine(displayAttackInfo(hit3, username));
                break;
            case "attack4":
                // deal HP4 damage to health bar
                float hit4 = Random.Range(hitHP4Lower, hitHP4Upper);
                bossHealthTarget -= hit4;
                triggerKnockbackAnimation();
                StartCoroutine(displayAttackInfo(hit4, username));
                break;
        }
    }

    private void playRandomAnimation()
    {
        // picks one of 4 animations at random
        randomAnimation = Random.Range(1,5);
        // Debug
        Debug.Log("Random Number: " + randomAnimation);
        switch(randomAnimation.ToString())
        {
            case "1":
                bossAnimator.SetTrigger("point");
                break;
            case "2":
                bossAnimator.SetTrigger("stab");
                break;
            case "3":
                bossAnimator.SetTrigger("sweep");
                break;
            case "4":
                bossAnimator.SetTrigger("hands");
                break;
        }
    }

    // why did I make this? dunno but here we are
    void triggerKnockbackAnimation()
    {
        // trigger knockback animation to play
        bossAnimator.SetTrigger("knockback");
    }

    void triggerDeathSave()
    {
        // automatically heal boss
        // play random animation and set health bar to appropriate value
        playRandomAnimation();
        float hSmall = Random.Range(healSmallLower, healSmallUpper);
        bossHealthTarget += hSmall;
        // kinda sorta "lerping" the battle ticker but not really (see Update())
        StartCoroutine(displayHealInfo(hSmall));
    }

    // Buttons to control boss healing
    void OnGUI()
    {
        if( GUI.Button(new Rect(6*xOffset,6*yOffset,GUIButtonWidth,GUIButtonHeight), "sH") )
        {
            // play random animation and set health bar to appropriate value
            playRandomAnimation();
            float healSmall = Random.Range(healSmallLower, healSmallUpper);
            bossHealthTarget += healSmall;
            // kinda sorta "lerping" the battle ticker but not really (see Update())
            StartCoroutine(displayHealInfo(healSmall));
        }

        if( GUI.Button(new Rect(7*xOffset,7*yOffset,GUIButtonWidth,GUIButtonHeight), "lH") )
        {
            playRandomAnimation();
            float healLarge = Random.Range(healLargeLower, healLargeUpper);
            bossHealthTarget += healLarge;
            StartCoroutine(displayHealInfo(healLarge));
        }


        if( GUI.Button(new Rect(8*xOffset,8*yOffset,GUIButtonWidth,GUIButtonHeight), "END") )
        {
            // End Battle
            StartCoroutine(triggerDeath());   
        }

    }

    // displaying the battle ticker + something kind of like a Lerp but not really
    private IEnumerator displayHealInfo(float healAmt)
    {
        textAlphaTarget = 255;
        // "Skeith has healed itself for {#} HP!"
        battleTicker.text = "Skeith has healed itself for " + healAmt.ToString() + " HP!";
        // wait for a few seconds
        yield return new WaitForSeconds(8);
        // set battleTicker target alpha to 0
        textAlphaTarget = 0;
        yield break;
    }

    private IEnumerator displayAttackInfo(float attackAmt, string userN)
    {
        textAlphaTarget = 255;
        // "{Username} has done {#} damage to Skeith"
        battleTicker.text = userN + " has done " + attackAmt.ToString() + " damage to Skeith!";
        // wait for a few seconds
        yield return new WaitForSeconds(8);
        // set battleTicker target alpha to 0
        textAlphaTarget = 0;
        yield break;
    }

    private IEnumerator triggerDeath()
    {
        // turn on death cam
        camController.setCams(deathCam);

        // turn off boss UI
        bossUI.SetActive(false);

        yield return new WaitForSeconds(15);

        // set isDead animation bool to true
        bossAnimator.SetBool("isDead", true);

        // Trigger boss death animation
        bossAnimator.SetTrigger("death");

        yield return new WaitForSeconds(35);

        // turn off boss object
        bossObject.SetActive(false);

        yield break;
    }

}
