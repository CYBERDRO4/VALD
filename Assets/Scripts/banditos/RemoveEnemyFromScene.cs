using UnityEngine;

public class RemoveEnemyFromScene : MonoBehaviour
{
    public GameObject ob;
    public void RemoveEnemyGameobjectFromScene() {
       
        Destroy(gameObject);
        Instantiate(ob, transform.position, Quaternion.identity);
    }
}
