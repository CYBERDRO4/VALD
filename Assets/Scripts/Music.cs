using Nicholas;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource source;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        switch (HumanGhostTransformation.getMode()) {
            case PlayerMode.Ghost:
                source.pitch = 0.15f;
                break;
            case PlayerMode.Human:
                source.pitch = 1;
                break;
        }
    }
}
