using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{


    public static AudioClip blockPlaceSound1, blockPlaceSound2, platformPlaceSound, PlatformMoveSound, blockPlaceHollowSound1;
    static        AudioSource audioSrc;

    void Awake()
    {

        audioSrc = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        PlatformMoveSound = Resources.Load<AudioClip>( "click_sound" );
        blockPlaceSound1 = Resources.Load<AudioClip>( "block_impact" );
        blockPlaceHollowSound1 = Resources.Load<AudioClip>( "block_impact_hollow" );

    }

    public static void SetSound(bool state)
    {
        if (state)
        {
            audioSrc.volume = 1;
        }
        else
        {
            audioSrc.volume = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "click_sound":
                audioSrc.PlayOneShot( PlatformMoveSound );
                break;

            case "block_impact":
                audioSrc.PlayOneShot( blockPlaceSound1 );
                break;

            case "block_impact_hollow":
                audioSrc.PlayOneShot( blockPlaceHollowSound1 );
                break;
        }


    }
}
