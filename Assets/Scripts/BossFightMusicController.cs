using UnityEngine;
using Nicholas;

public class BossFightMusicController : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioSource screams;
 

    public float pitch = 1;

    private void Update()
    {
        switch (HumanGhostTransformation.getMode()) {
            case PlayerMode.Ghost:
                pitch = 0.15f;
                screams.volume = 0.5f;
                break;
            case PlayerMode.Human:
                pitch = 1;
                screams.volume = 0.15f;
                break;
        }
        backgroundMusic.pitch = pitch;
    }








}
