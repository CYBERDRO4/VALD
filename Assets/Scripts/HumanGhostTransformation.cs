using UnityEngine;
namespace Nicholas

    /**
     * Последнее редактирование: 17.05.2019
     * V1.2.5
     */

{   [RequireComponent(typeof(PlayableCharacter))]
    public class HumanGhostTransformation : MonoBehaviour {

        public static bool canToTransform = true;
        private static PlayerMode mode = PlayerMode.Human;
        private Animator animator;
        public GameObject[] normalBg;
        public GameObject[] ghostBg;
        private GameObject canToTransformUI;
        private PlayableCharacter player;
        public static float speedInGhostMode = 12;
        public static int jumpForceInGhostMode = 35;

        public void SetCanToTransform(bool arg) => canToTransform = arg;
        public bool GetCanToTransform() { return canToTransform; }

        public static PlayerMode getMode() {return mode; }
        public static void setMode(PlayerMode modeArg) { mode = modeArg;}
        private void Start()
        {
            animator = GetComponent<Animator>();
            player = GetComponent<PlayableCharacter>();
            normalBg = GameObject.FindGameObjectsWithTag("NormBg");
            ghostBg = GameObject.FindGameObjectsWithTag("GhostBg");
            canToTransformUI = GameObject.Find("CanToTransformUI");

            switch (mode) {
                case PlayerMode.Human:
                    foreach (GameObject i in ghostBg)
                        i.SetActive(false);
                    break;
                case PlayerMode.Ghost:
                    foreach (GameObject i in normalBg)
                        i.SetActive(false);
                    break;
            }

        }

        private void Update()
        {
            switch (mode)
            {
                case PlayerMode.Ghost:
                    player.SetSpeedWithoutWeapon(speedInGhostMode);
                    player.SetJumpForce(35);
                    break;
                case PlayerMode.Human:
                    player.SetSpeedWithoutWeapon(8);
                    player.SetJumpForce(25);
                    break;        
            }


            if (!animator.GetBool("Ghost") && mode == PlayerMode.Ghost)
                animator.SetBool("Ghost", true);
            else if (animator.GetBool("Ghost") && mode == PlayerMode.Human)
                animator.SetBool("Ghost", false);

            if (Input.GetKeyDown(KeyCode.T))
                Transform();
            if (canToTransform == false && canToTransformUI.active == true)
                canToTransformUI.SetActive(false);
            else if (canToTransform == true && canToTransformUI.active == false)
                canToTransformUI.SetActive(true);
        }
        public void Transform()
        {
            if (canToTransform && Input.GetAxis("Horizontal") == 0)
                {
                    switch (mode)
                    {
                        case PlayerMode.Human:
                        animator.SetBool("Ghost", true);
                        foreach (GameObject bgObject in normalBg)
                                bgObject.SetActive(false);
                            foreach (GameObject bgObject in ghostBg)
                                bgObject.SetActive(true);  
                        mode = PlayerMode.Ghost;
                        animator.SetInteger("Weapon", 0);

                        
                            return;

                        case PlayerMode.Ghost:
                        animator.SetBool("Ghost", false);
                        foreach (GameObject bgObject in normalBg)
                                bgObject.SetActive(true);
                            foreach (GameObject bgObject in ghostBg)
                                bgObject.SetActive(false);
                        mode = PlayerMode.Human;

                        player.SetSpeedWithoutWeapon(8);
                        player.SetJumpForce(25);
                        return;


                    }
                }
            }
            public void Transform(PlayMode mode)
        {

        }

    }
        public enum PlayerMode { Human, Ghost, None // только для проверки на след.ур 
    }
}
