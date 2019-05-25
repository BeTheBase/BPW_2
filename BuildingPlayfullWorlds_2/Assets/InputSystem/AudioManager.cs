using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    public Button[] ClickButtons;
    public AudioSource ASource;
    public AudioClip ButtonClip, PlayerWalk, PlayerJump;


    private Transform Player;

    public void PlayButtonSound()
    {
        ASource.PlayOneShot(ButtonClip);
    }

    public void PlayPlayerSound(string _name)
    {
        switch (_name)
        {
            case "PlayerWalk":
                ASource.PlayOneShot (PlayerWalk);
                break;
            case "PlayerJump":
                ASource.PlayOneShot(PlayerJump);
                break;
        }
    }
}
