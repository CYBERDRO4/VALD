using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameControlScript : MonoBehaviour
{

    public Text moneyText;
    public static int moneyAmount;
    public GameObject Shop;


    void Start()
    {
      
    }


    void Update()
    {
        moneyText.text = moneyAmount.ToString();

    }


    public void ToShop()
    {
        Shop.SetActive(true);
    }

    public void BackShop()
    {
        Shop.SetActive(false);
    }

}
