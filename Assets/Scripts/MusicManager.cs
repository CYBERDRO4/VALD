using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource normalMusic;
    public AudioSource ghostMusic;
    public bool canPlay;

    public void switchBackgroundMusic(Nicholas.PlayerMode mode) {
        switch (mode) {
            case Nicholas.PlayerMode.Ghost:
                break;
            case Nicholas.PlayerMode.Human:
                break;
        }
    }


}
