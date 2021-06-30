using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformPlayer : MonoBehaviour
{
    public GameObject player;
    public GameObject playerFat;
    public bool cooldownOn;
    public bool PlayerActive;
    public bool PlayerFatActive;
    

    // Start is called before the first frame update
    void Start()
    {
        cooldownOn = false;
        player.SetActive(true);
        playerFat.SetActive(false);
        PlayerActive = true;
        PlayerFatActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && cooldownOn == false) // player presses E, bouncy mode on for 4s
        {
            player.SetActive (false);
            playerFat.SetActive (true);
            PlayerActive = false;
            PlayerFatActive = true;
            playerFat.transform.position = player.transform.position;
            playerFat.transform.rotation = player.transform.rotation;
            cooldownOn = true;
            StartCoroutine(playerFatCooldown());
            StartCoroutine(playerFatCountdown());
        }
            
    }

    IEnumerator playerFatCountdown()
    {
        yield return new WaitForSeconds(4);
        playerFat.SetActive(false);
        player.SetActive(true);
        PlayerActive = true;
        PlayerFatActive = false;
        player.transform.position = playerFat.transform.position;
        player.transform.rotation = playerFat.transform.rotation;
    }

    IEnumerator playerFatCooldown()
    {
        yield return new WaitForSeconds(9);
        cooldownOn = false;
    }
}
