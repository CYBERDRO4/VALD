using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraControl : MonoBehaviour
{
    public Transform target;
    public Transform up;
    public Transform die;
    public float smoothSpeed = 0.3F;
    //public Transform targetxr;
    //public Transform targetxl;

    private Vector3 currentVelocity;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        //if (target != null)
        //{
        if ((target.position.y > transform.position.y) && (target.position.y < up.position.y))
        {
            Vector3 newPos = new Vector3(transform.position.x, target.position.y, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, newPos, ref currentVelocity, smoothSpeed);
        }


        else if (target.position.y < die.position.y)
        {
            SceneManager.LoadScene("2");
        }

                }
                //}



            }


