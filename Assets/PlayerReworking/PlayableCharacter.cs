using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Bosses;
namespace Nicholas
{
    [RequireComponent(typeof(HumanGhostTransformation))]
    public class PlayableCharacter : MonoBehaviour
    {
       // public int currentScene;

        public static int moneyAmount;

        private static int maxHealth = 100;
        public static float health = maxHealth;
        public float speed;
        public static float speedWithoutWeapon = 5;
        public static float speedWithSpear = 5;
        public static float speedWithSword = 5;
        public static float speedWithShield = 3;
        public static float spearAttackDistance = 4;
        public static float swordAttackDistance = 3;
        public static float shieldAttackDistance = 2;
        public static int jumpForce = 25;
        private bool isGrounded;
            public Text hpBar;
        public Text moneyUI;
        public bool canMove = true;
        public bool canJump;
        public bool canAttack = true;
        private bool lookForward = true;
        private float nextShieldShock;
        private Animator animator;
        private Rigidbody2D rgb2d;
        private SpriteRenderer renderer;

        public Weapon weapon;
        private HumanGhostTransformation mode;
        private GameObject deathScreen;


        //public void SetHealth(int healthArg) { health = healthArg; }
        public void SetSpeedWithoutWeapon(float speed) { speedWithoutWeapon = speed; }
        public void SetSpeedWithSpear(float speed) { speedWithSpear = speed; }
        public void SetSpeedWithSword(float speed) { speedWithSword = speed; }
        public void SetSpeedWithShield(float speed) { speedWithShield = speed;}
        public float GetSpeedWithoutWeapon() { return speedWithoutWeapon; }
        public float GetSpeedWithSpear() { return speedWithSpear; }
        public float GetSpeedWithSword() { return speedWithSword; }
        public float GetSpeedWithShield() { return speedWithShield; }
        public void SetJumpForce(int force) { jumpForce = force; }
        public int GetJumpForce() { return jumpForce; }



        private void Awake()
        {
            hpBar = GameObject.Find("HPSTAT").GetComponent<Text>();
            animator = GetComponent<Animator>();
            rgb2d = GetComponent<Rigidbody2D>();
            renderer = GetComponentInChildren<SpriteRenderer>();
            mode = GetComponent<HumanGhostTransformation>();
            moneyUI = GameObject.Find("Money").GetComponent<Text>();
           
        }

        private void Start()
        {
            //animator = GetComponent<Animator>();
            //rgb2d = GetComponent<Rigidbody2D>();
            //renderer = GetComponentInChildren<SpriteRenderer>();
            //mode = GetComponent<HumanGhostTransformation>();
            //hpBar = GameObject.Find("HPSTAT").GetComponent<Text>();
            InvokeRepeating("ClearAttack", 0.15f, 0.3f);
            moneyAmount = SaveLoad.ReadMoneyAmount();
            deathScreen = GameObject.FindGameObjectWithTag("Death");
            if (deathScreen != null)
                deathScreen.SetActive(false);

            // moneyAmount = PlayerPrefs.GetInt("MoneyAmount");//lj,fdbk
            // moneyUI = GameObject.Find("Money").GetComponent<Text>();
        }
      
        public void GetDamage(int damageValue)
        {
          
                if (weapon == Weapon.Shield)
                    health -= Mathf.Floor(damageValue * 0.33f);
                else
                    health -= damageValue;
        }
        public void Death()
        {
            deathScreen.SetActive(true);
            Destroy(gameObject);
        }

        private void ShowHPInUI()
        {
            health = Mathf.Clamp(health, 0, maxHealth);
            hpBar.text = health.ToString();
            if (health <= 0)
                Death();
           
        }
       
        private void Update()
        {
            GroundCheck();
            ShowHPInUI();
            moneyUI.text = moneyAmount.ToString();

            if (Input.GetAxis("Horizontal") == 0)
                animator.SetFloat("Speed", 0);
            else
                Run();
            if (Input.GetKeyDown(KeyCode.Alpha1))
                ChangeWeapon(1);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                ChangeWeapon(2);
            if (Input.GetKeyDown(KeyCode.Alpha3))
                ChangeWeapon(3);
            if (Input.GetKeyDown(KeyCode.Alpha0))
                ChangeWeapon(0);
            if (Input.GetKeyDown(KeyCode.Mouse0))
                Attack();
            if (Input.GetKeyDown(KeyCode.Space))
                Jump();
            switch (animator.GetInteger("Weapon"))
            {
                case 0:
                    weapon = Weapon.NoWeapon;
                    speed = speedWithoutWeapon;
                    canJump = true;
                    break;
                case 1:
                    weapon = Weapon.Spear;
                    speed = speedWithSpear;
                    canJump = false;
                    break;
                case 2:
                    weapon = Weapon.Sword;
                    speed = speedWithSword;
                    canJump = true;
                    break;
                case 3:
                    weapon = Weapon.Shield;
                    speed = speedWithShield;
                    canJump = false;
                    break;
            }
        }


        public void ClearAttack()
        {
            animator.SetBool("Attack", false);
            canAttack = true;
        }
        public void Run()
        {
            if (canMove)
            {
                if (Input.GetAxis("Horizontal") < 0 && lookForward)
                {
                    renderer.flipX = lookForward;
                    lookForward = false;
                }
                if (Input.GetAxis("Horizontal") > 0 && !lookForward)
                {
                    renderer.flipX = lookForward;
                    lookForward = true;
                }
                if (Input.GetAxis("Horizontal") < 0)
                    animator.SetFloat("Speed", -Input.GetAxis("Horizontal"));
                else
                    animator.SetFloat("Speed", Input.GetAxis("Horizontal"));

                if (Input.GetAxis("Horizontal") != 0)
                {
                    Vector3 direction = Input.GetAxis("Horizontal") * transform.right;
                    transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
                }

            }
        }
        public void Jump()
        {
            if (canMove && isGrounded && canJump)
                rgb2d.AddForce(new Vector2(Input.GetAxis("Horizontal") * 0.25f, jumpForce), ForceMode2D.Impulse);
               
        }
        public void Attack()
        {
            if (canMove && canAttack && weapon > 0)
            {
                animator.SetBool("Attack", true);
                canAttack = false;
                switch (lookForward)
                {
                    case true:
                        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, transform.right, spearAttackDistance);
                        foreach (RaycastHit2D ray in hit)
                        {
                            switch (ray.collider.tag)
                            {
                                case "Enemy":
                                    ray.collider.gameObject.GetComponent<Enemy>().GetDamageByPlayer(weapon);
                                    return;
                                case "Boss":
                                    ray.collider.gameObject.GetComponent<BossStats>().GetDamageByPlayer(weapon);
                                    return;
                            }
                        }
                        return;
                    case false:
                        RaycastHit2D[] hit1 = Physics2D.RaycastAll(transform.position, -transform.right, spearAttackDistance);
                        foreach (RaycastHit2D ray in hit1) {
                            switch (ray.collider.tag) {
                                case "Enemy":
                                    ray.collider.gameObject.GetComponent<Enemy>().GetDamageByPlayer(weapon);
                                    return;
                                case "Boss":
                                    ray.collider.gameObject.GetComponent<BossStats>().GetDamageByPlayer(weapon);
                                    return;
                            }
                        }
                        return;
                }
            }
        }

        public void ChangeWeapon(int weaponIndex)
        {
            if (canMove && isGrounded && HumanGhostTransformation.getMode() == PlayerMode.Human)
                animator.SetInteger("Weapon", weaponIndex);
        }

        public void GroundCheck()
        {
            RaycastHit2D[] ray = Physics2D.RaycastAll(transform.position, -transform.up, 1.5f); // Пускаем лучи добра себе под ноги
            Debug.DrawRay(transform.position, -transform.up);
            int groundCheckObjectsCount = 0;
            foreach(RaycastHit2D collider in ray)
            {
                if (collider.collider.gameObject.tag == "Other" || collider.collider.gameObject.tag == "Platform")
                    groundCheckObjectsCount++;
            }
            isGrounded = groundCheckObjectsCount > 0;
            animator.SetBool("Grounded", isGrounded);
            
        }


    }

    public enum Weapon
    {
        NoWeapon, Spear, Sword, Shield
    }
}
