using UnityEngine;
using UnityEngine.UI;
namespace Bosses
{
    [RequireComponent(typeof(Boss))]
    public class BossUI : MonoBehaviour
    {
        private BossStats boss;
        private Slider hpbar;
        private Text bossName;


        private void Start()
        {
            boss = GetComponent<BossStats>();
            hpbar = GameObject.Find("BossHP").GetComponent<Slider>();
            bossName = GameObject.Find("BossName").GetComponent<Text>();
            hpbar.maxValue = boss.maxHealth;
            hpbar.value = boss.health;
            bossName.text = boss.bossName;
        }
        private void Update()
        {
            if (boss != null)
                hpbar.value = boss.health;
            else
                Destroy(GameObject.Find("BossCanvas"));
        }
    }
}
