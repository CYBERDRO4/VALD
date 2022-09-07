using UnityEngine;
using System;
using Nicholas;

public class BossStats : MonoBehaviour
{
    public string bossName;
    public int maxHealth;
    public int health;
    public int damage;

    private void Start()
    {
        health = maxHealth;
    }

    public void GetDamageByPlayer(Weapon weapon) {
        switch (weapon) {
            case Weapon.Spear:
                health -= damage;
                health = Mathf.Clamp(health, 0, maxHealth);
                return;
            case Weapon.Sword:
                health -= Convert.ToInt32(damage * 1.5);
                health = Mathf.Clamp(health, 0, maxHealth);
                return;
            case Weapon.Shield:
                health -= Convert.ToInt32(damage / 3);
                health = Mathf.Clamp(health, 0, maxHealth);
                return;
        }

    }

}
