using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DudleJump : MonoBehaviour
{
    public Transform NextLVLy;
    public Transform Player;



    public void NextLVLP()
    {
        SceneManager.LoadScene(2);

    }


    private void Update()
    {
        if (Player.position.x > NextLVLy.position.x && Player.position.y < NextLVLy.position.y) NextLVLP();
    }
}
