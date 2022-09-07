using UnityEngine;

public class CutsceneActivation : MonoBehaviour
{
    public GameObject cutsceneObject;
    public bool activateScriptAfterCutscene;
    [SerializeField] private MonoBehaviour script;
    public float duration;
    private float deactivationTime;


    private Nicholas.PlayableCharacter player;

    private void Awake()
    {
        cutsceneObject.SetActive(true);
       // this.enabled = true;

    }


    private void OnEnable()
    {
        deactivationTime = Time.time + duration;
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Nicholas.PlayableCharacter>();
       
        if(player != null)
        player.canMove = false;

    }

    private void Update()
    {
        switch (activateScriptAfterCutscene) {
            case false:
        if (Time.time > deactivationTime)
        {
          
            cutsceneObject.SetActive(false);
            Destroy(cutsceneObject);
                    if(player != null)
            player.canMove = true;    
                }
                break;
            case true:
                if (Time.time > deactivationTime)
                {
                    cutsceneObject.SetActive(false);
                   
                    Destroy(cutsceneObject);
                    script.enabled = true;
                    if(player != null)
                    player.canMove = true;
                }
                break;
                
    }
    }

}
