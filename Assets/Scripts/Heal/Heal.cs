using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{



 


    private void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Nicholas.PlayableCharacter.health += Random.Range(15, 35);
            Destroy(gameObject);

        }
    }


}
