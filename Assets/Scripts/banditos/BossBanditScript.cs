using UnityEngine;

public class BossBanditScript : MonoBehaviour
{
    [SerializeField] private float attackRate;
    [SerializeField] private float runSpeed;
    [SerializeField] private int damage;
    [SerializeField] private GameObject finalCutsceneObject;
    private float nextAttack;

    private Nicholas.PlayableCharacter player;
    private Transform playerPos;
    private Animator animator;
    private SpriteRenderer renderer;
    private BossStats stats;
    private bool lookForward = true;
    private float direction;
    private bool canMove;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Nicholas.PlayableCharacter>();
        stats = GetComponent<BossStats>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckPlayer();
        flipSprite();
        TowardsPlayer();
        CheckHp();
    }

    public void flipSprite()
    {
        if (player.transform.position.x > transform.position.x && !lookForward)
        {
            renderer.flipX = true;
            lookForward = true;
        }
        else if (player.transform.position.x < transform.position.x && lookForward) {
            renderer.flipX = false;
            lookForward = false;
        }
            
    }

    public void TowardsPlayer() {
        if (canMove)
        {
            direction = player.transform.position.x - transform.position.x;
            Vector3 pos = transform.position;
            pos.x += Mathf.Sign(direction) * runSpeed * Time.deltaTime;
            transform.position = pos;
        }
    }

    public void CheckPlayer() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1);
        foreach (Collider2D col in colliders) {
            if (col.gameObject.tag == "Player") {
                Attack();
                return;
            }
        }
        canMove = true;
    }

    public void RemoveBossFormScene()
    {
        Destroy(gameObject);
    }

    public void CheckHp() {
        if (stats.health <= 0)
            Die();
    }
    public void Die()
    {
        canMove = false;
        animator.SetBool("Die", true);
        this.enabled = false;
    }
    public void ShowFinalCutscene()
    {
        finalCutsceneObject.SetActive(true);
    }

    public void Attack() {
        canMove = false;
        if (Time.time > nextAttack && !animator.GetBool("Attack")) {
            animator.SetBool("Attack", true);
            nextAttack = Time.time + attackRate;
        }
    }

    public void DamageToPlayer()
    {
        player.GetDamage(Random.Range(damage / 2, damage * 2));
    }

    public void DisableAttackAnimation()// Только для использования с анимациями
    {
        animator.SetBool("Attack", false);
    }
   

}
