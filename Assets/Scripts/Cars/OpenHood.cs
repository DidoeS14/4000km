using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenHood : MonoBehaviour
{
    [SerializeField, Range(1,5)]    public int hoodLevel = 1;
    [SerializeField] SpriteRenderer renderer;
    [Header("The second sprite should be the opened hood")]
    [Tooltip("The second sprite should be the opened hood")]
    [SerializeField] Sprite[] sprites;

    [SerializeField] private ParticleSystem smoke;
    [Header("For Debugging")]
    public bool toMuchDamage = false;

    private bool open = false;

    // Update is called once per frame
    private void Awake()
    {
        if(smoke != null)
        {
            smoke.Stop();
        }
    }
    void Update()
    {
        OpenClose(open,hoodLevel);
        CarFixed(toMuchDamage);
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if(Input.GetKey(KeyCode.E))
            {
                open = true;
                PlayerAniationOn();
                if (toMuchDamage && smoke != null)
                {
                    smoke.Play();
                }
            }
            else if(Input.anyKey)
            {
                open = false;
                PlayerAniationOff();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            open = false;
            PlayerAniationOff();
            if (toMuchDamage && smoke != null)
            {
                smoke.Stop();
            }
        }            
    }
    private void OpenClose(bool open, int level)
    {
        
        if (open)
        {
            switch (level)
            {
                case 1:
                    renderer.sprite = sprites[1];
                    break;
                case 2:
                    renderer.sprite = sprites[3];
                    break;
                case 3:
                    renderer.sprite = sprites[5];
                    break;
                case 4:
                    renderer.sprite = sprites[7];
                    break;
                case 5:
                    renderer.sprite = sprites[9];
                    break;
            }
        }
        else
        {
            switch (level)
            {
                case 1:
                    renderer.sprite = sprites[0];
                    break;
                case 2:
                    renderer.sprite = sprites[2];
                    break;
                case 3:
                    renderer.sprite = sprites[4];
                    break;
                case 4:
                    renderer.sprite = sprites[6];
                    break;
                case 5:
                    renderer.sprite = sprites[8];
                    break;
            }
        }
    }
    private void PlayerAniationOn()
    {
        GameObject player = GameObject.FindWithTag("Player");
        var playerScript = player.GetComponent<PlayerMovement>();
        playerScript.Fixing();
    }
    private void PlayerAniationOff()
    {
        GameObject player = GameObject.FindWithTag("Player");
        var playerScript = player.GetComponent<PlayerMovement>();
        playerScript.DoneFixing();
    }
    private void CarFixed(bool bad)
    {
        if (!bad && smoke != null)
        {
            smoke.Stop();
        }
    }
}
