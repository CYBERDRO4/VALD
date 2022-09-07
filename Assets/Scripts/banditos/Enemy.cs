using UnityEngine;

[RequireComponent(typeof(RemoveEnemyFromScene))]
public class Enemy : MonoBehaviour
{
    
    private Nicholas.PlayableCharacter player; // объект игрока находится по тэгу
    public EnemyType enemyType;
    private Animator animator;
    private SpriteRenderer renderer;
    private Rigidbody2D rgb2d;

    public static int  LVLSpear=1;
    public static int LVLSword=1;
    public static int LVLShield=1;


    private bool lookForward = true;
    private bool canMove = true;
    private bool canAttack = true;
    private float nextAttack = 0;
    private float shieldShockDuration;
    private float direction;
    

    [SerializeField] private int startHealth = 100;
    [SerializeField] private int health;
    [SerializeField] private int damageBySpear;
    [SerializeField] private float impulseBySpear;
    [SerializeField] private int damageBySword;
    [SerializeField] private float impulseBySword;
    [SerializeField] private int damageByShield;
    [SerializeField] private float impulseByShield;
    [SerializeField] private float shockTimeByShieldHit;
    [SerializeField] private float shockResistInTime;
    private float nextShockAvailable = 0;
    public bool shocked;
    [SerializeField] private int damageToPlayer = 15;
    [SerializeField] private float attackRate = 1.5f;
    [SerializeField] private float speedMove = 2.5f;
    [SerializeField] private float triggeredDistance = 15;

    private Collider2D collider;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Nicholas.PlayableCharacter>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        rgb2d = GetComponent<Rigidbody2D>();
        health = startHealth;
        animator.SetBool("Attack", false);
        collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        health = Mathf.Clamp(health, 0, startHealth);
        if (health == 0)
            Die();

        if (shocked && Time.time > shieldShockDuration) 
            ClearShock();
       
        direction = player.transform.position.x - transform.position.x;
        AttackWithRaycast();
        
        if (Mathf.Abs(direction) < triggeredDistance && canMove && !shocked)
        {
            animator.SetFloat("Speed", 1);
            Vector3 pos = transform.position;
            pos.x += Mathf.Sign(direction) * speedMove * Time.deltaTime;
            FlipIfPositionHasChanged();
            transform.position = pos;
        }
        else
            animator.SetFloat("Speed", 0);
        if (health < 0)
            Destroy(gameObject);
    }

    
    public void Die() {
        animator.SetBool("Die", true);
        this.enabled = false;
        collider.enabled = false;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       // if(canAttack && !shocked)
      //  animator.SetBool("Attack", true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && !shocked)
        {
            canMove = true;
            StopAttack();
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && shocked)
            StopAttack();
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            canMove = false;
        }
    }

    public void AttackWithRaycast() {
       
        Vector3 rayDirection = new Vector3(direction, 0, 0);
        Debug.DrawRay(transform.position, rayDirection, Color.red);
        if (canAttack)
        {
            FlipIfPositionHasChanged();
            RaycastHit2D[] ray = Physics2D.RaycastAll(transform.position, rayDirection, 2.55f);
            foreach (RaycastHit2D collider in ray) {
                if (collider.collider.gameObject.tag == "Player" && Time.time > nextAttack) {

                    animator.SetBool("Attack", true);
                    player.GetDamage(damageToPlayer);
                    nextAttack = Time.time + attackRate;
                }
            }
            /*
            if (ray.Length > 2 && Time.time > nextAttack)
            {
                animator.SetBool("Attack", true);
                player.GetDamage(damageToPlayer);
                nextAttack = Time.time + attackRate;
               // Debug.Log(nextAttack);
            }
            */
        }
        /*
        foreach (RaycastHit2D hit in ray) {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player") && Time.time > nextAttack)
            { 
                player.GetDamage(15);
                nextAttack = Time.time + attackRate;
                Debug.Log(nextAttack);
            }
        }
        */
    }
    /*
    public void Attack() {
        FlipIfPositionHasChanged();
        Collider2D[] colliders = Physics2D.OverlapCir`ll(transform.position, 3.5f);
        foreach (Collider2D collider in colliders) {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Player") && Time.time > nextAttack)
            {
                player.GetDamage(15);
                nextAttack = Time.time + attackRate;
            }
        }
    }
    */

   public void LVLUP()
    {

        if (LVLSword == 2)
        {
            damageBySword += 5;
        }

        if (LVLSpear == 2)
        {
            damageBySword += 5;
        }

        if (LVLShield == 2)
        {
            damageBySword += 5;
        }


        if (LVLSword == 3)
        {
            damageBySword += 5;
        }

        if (LVLSpear == 3)
        {
            damageBySword += 5;
        }

        if (LVLShield == 3)
        {
            damageBySword += 5;
        }



        if (LVLSword == 4)
        {
            damageBySword += 5;
        }

        if (LVLSpear == 4)
        {
            damageBySword += 5;
        }

        if (LVLShield == 4)
        {
            damageBySword += 5;
        }


        if (LVLSword == 5)
        {
            damageBySword += 5;
        }

        if (LVLSpear == 5)
        {
            damageBySword += 5;
        }

        if (LVLShield == 5)
        {
            damageBySword += 5;
        }


    }






    public void GetDamageByPlayer(Nicholas.Weapon playerWeapon) {
        switch (playerWeapon) {
            case Nicholas.Weapon.Spear:
                health -= damageBySpear;
                rgb2d.AddForce(new Vector2(-direction * impulseBySpear, impulseBySpear * 2), ForceMode2D.Impulse);
                Debug.Log("АУЕ");
                break;
            case Nicholas.Weapon.Sword:
                health -= damageBySword;
                rgb2d.AddForce(new Vector2(-direction * impulseBySword, impulseBySword * 0.5f), ForceMode2D.Impulse);
                break;
            case Nicholas.Weapon.Shield:
                health -= damageByShield;
                rgb2d.AddForce(new Vector2(-direction * impulseByShield, impulseByShield * 0.5f), ForceMode2D.Impulse);
                ShockMyself();
                break;
        }
    }
    private void ShockMyself() {
        if (Time.time > nextShockAvailable)
        {
            canAttack = false;
            canMove = false;
            shocked = true;
            StopAttack();
            shieldShockDuration = Time.time + shockTimeByShieldHit;
            nextShockAvailable = Time.time + shockTimeByShieldHit + shockResistInTime;
        }
    }
    private void ClearShock() {
        canAttack = true;
        canMove = true;
        shocked = false;
    }
    private void FlipIfPositionHasChanged() {
        if (player.transform.position.x < transform.position.x && lookForward)
        {
            lookForward = false;
            renderer.flipX = lookForward;
        }
        if (player.transform.position.x > transform.position.x && !lookForward)
        {
            lookForward = true;
            renderer.flipX = lookForward;
            
        }
    }
    public void StopAttack() => animator.SetBool("Attack", false);
}
public enum EnemyType {
    Силач, Ловкач, Босс
}

