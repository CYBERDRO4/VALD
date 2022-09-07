using UnityEngine;
using Nicholas;
public class BulletScript : MonoBehaviour
{
    private PlayableCharacter player;
    private Rigidbody2D rgb2d;
    private float aliveTime = 4.25f;
    private float destroyTime; 

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayableCharacter>();
        rgb2d = GetComponent<Rigidbody2D>();
        rgb2d.AddForce(new Vector2(Random.Range(-10, 10) * 2, Random.Range(2, 10) * 3), ForceMode2D.Impulse);
        destroyTime = Time.time + aliveTime;

    }
    void GiveDamage() {
        player.GetDamage(2);
        Destroy();
      }

    private void Destroy() { Destroy(gameObject); }

    private void Update()
    {
        if (Time.time > destroyTime)
            Destroy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
            GiveDamage();
    }

}
