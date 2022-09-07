using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHeal : MonoBehaviour
{
    int a;
    public GameObject obj;

    public GameObject boss;

    public GameObject[] spawn = new GameObject[5];

     

   public float h =  Nicholas.PlayableCharacter.health;
    public void Update()
    {
        if (Nicholas.PlayableCharacter.health <= 50f)
        {
            Invoke("Shit", 2f);
            Debug.Log("Open");
        }
        else if (Nicholas.PlayableCharacter.health == 100f)
        {
            CancelInvoke();
            Debug.Log("Close");
        }
    
    }


    public void Shit()
    {
        GameObject[] mas = GameObject.FindGameObjectsWithTag("Heal");

        a = Random.Range(0, 5);

        if (mas.Length == 0) {
            Debug.Log("Cgfdy");
            switch (a)
            {
                case 0:
                    Instantiate(obj, spawn[0].transform.position, transform.rotation, spawn[0].transform);
                    Debug.Log("Cgfdy 0");
                    break;
                case 1:
                    Instantiate(obj, spawn[1].transform.position, transform.rotation, spawn[1].transform);
                    Debug.Log("Cgfdy 1");
                    break;
                case 2:
                    Instantiate(obj, spawn[2].transform.position, transform.rotation, spawn[2].transform);
                        Debug.Log("Cgfdy 2");
                    break;
                case 3:
                    Instantiate(obj, spawn[3].transform.position, transform.rotation, spawn[3].transform);
                    Debug.Log("Cgfdy 3");
                    break;
                case 4:
                    Instantiate(obj, spawn[4].transform.position, transform.rotation, spawn[4].transform);
                    Debug.Log("Cgfdy");
                    break;
            }
        }
    }
   
}
