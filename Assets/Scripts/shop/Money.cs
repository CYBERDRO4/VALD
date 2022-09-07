using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public Rigidbody2D rig;
    public void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }


    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Nicholas.PlayableCharacter.moneyAmount += Random.Range(1, 5);
            Destroy(gameObject);

        }
    }
}
