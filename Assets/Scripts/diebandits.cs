using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diebandits : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {   //foreach(Collider2D i in collision)
        if (collision.gameObject.tag == "Enemy") Destroy(collision.gameObject);
    }

}
