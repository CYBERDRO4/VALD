using UnityEngine;
using Nicholas;

namespace Bosses
{
    [RequireComponent(typeof(BossUI))]
    public class Boss : MonoBehaviour
    {
        public int damage;
        public bool canUseShield;
        public bool canUseRangeAttack;
        public float meleeAttackDelay;
        public float RangeAttackDelay;
        public float ShieldDelay;
        public BossStage stage;
        private PlayableCharacter player;
        private Transform playerPos;
        private Animator animator;
        private BossStats stats;

        private Collider2D collider;

        [SerializeField] private GameObject rangeAttackBullet;
        [SerializeField] private GameObject specialEffectForMeleeAttack;
        [SerializeField] private GameObject specialEffectForRangeAttack;
        [SerializeField] private GameObject laser;


        private float portalLiveTime;
        private float nextportalShow;

        private float nextRange;
        private float nextMelee;
       

        private void Start()
        {
            stats = GetComponent<BossStats>();
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayableCharacter>();
            playerPos = GameObject.FindGameObjectWithTag("Player").transform;
            animator = GetComponent<Animator>();
            nextRange = RangeAttackDelay;
            collider = GetComponent<Collider2D>();
        }
        private void Update()
        {
          
            CheckPlayer();
            CheckStage();
            CheckHealth();

        }



        public void MeleeAttack()
        {
            if (Time.time > nextMelee)
            {
                animator.SetBool("MeleeAttack", true);
               
                nextMelee = Time.time + meleeAttackDelay;
            }
        }
        public void RangeAttack() {


            if (canUseRangeAttack && Time.time > nextRange && stage > 0)
            {
               
                switch (stage) {
                    case BossStage.Second:
                        animator.SetBool("RangeAttack", true);
                        nextRange = Time.time + RangeAttackDelay;
                        break;

                    case BossStage.Rage:
                        animator.SetBool("RangeAttack", true);
                        nextRange = Time.time + RangeAttackDelay - 1.5f;
                        break;
                }
            }
        }

        public void DamageToPlayer() {

            if(playerPos.position.y < -2f){

                switch (stage) {
                    case BossStage.First:
                        player.GetDamage(damage);
                        return;
                    case BossStage.Second:
                        player.GetDamage((int)Mathf.Ceil(damage * 1.25f));
                        return;
                    case BossStage.Rage:
                        player.GetDamage((int)Mathf.Ceil(damage * 1.75f));
                        return;
                }
            }
        }
        public void UseShield(Weapon weaponToBrake) { }

        public void CheckStage() {
            if (stats.health / stats.maxHealth < 0.75f && stats.health / stats.maxHealth > 0.33f)
                stage = BossStage.Second;
            else if (stats.health / stats.maxHealth < 0.33f)
                stage = BossStage.Rage;

        }

        public void CheckHealth()
        {
            if (stats.health <= 0)
                Die();
        }

        public void SpawnFireballs()
        {
            switch (stage) {
                case BossStage.Second:
                    for(int i = 0; i < 7; i++)
                        Instantiate(rangeAttackBullet, transform.position, Quaternion.identity);
                    return;
                case BossStage.Rage:
                    for (int i = 0; i < 12; i++)
                        Instantiate(rangeAttackBullet, transform.position, Quaternion.identity);
                    return;
            }
      
        }
        public void SpawnFireballs(int count) {
            for (int i = 0; i < count; i++)
                Instantiate(rangeAttackBullet, transform.position, Quaternion.identity);
          }


        public void Die() {
            animator.SetBool("Die", true);
            collider.enabled = false;
            GetComponent<BossUI>().enabled = false;
            this.enabled = false;

         }

        public void RemoveBoss() {
            SpawnFireballs(15);
            Destroy(gameObject);
          }

        public void ShowRangeAttackEffect()
        {
            specialEffectForRangeAttack.SetActive(true);
        }
        public void HideRangeAttackEffect()
        {
            specialEffectForRangeAttack.SetActive(false);
        }

        public void DisableMeleeAttackAnimation()
        {
            animator.SetBool("MeleeAttack", false);
        }
        public void DisableRangeAttackAnimation()
        {
            animator.SetBool("RangeAttack", false);
        }
        public void DisableShieldAnimation()
        {
            animator.SetBool("Die", true);
        }


        private void CheckPlayer()
        {
            Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, 5);
            foreach (Collider2D hit in col)
            {
                if (hit.gameObject.tag == "Player" && playerPos.transform.position.y < -2.5f)
                {
                    MeleeAttack();
                    return;
                }

            }
            RangeAttack();
        }



        public enum BossStage
        {
            First, Second, Rage
        }
    }
}
