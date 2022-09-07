using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class ShopControl : MonoBehaviour
{
   
   int priceSpear=5;
    int priceSword = 5;
    int priceShield = 5;

    public Text moneyAmountText;

    public Text PriceSpear;
    public Text PriceSword;
    public Text PriceShield;


    int moneyamount = Nicholas.PlayableCharacter.moneyAmount;

    int lvlsword=Enemy.LVLSword;
    int lvlspear = Enemy.LVLSpear;
    int lvlshield = Enemy.LVLShield;

  
    public Button Shield;
    public Button Spear;
    public Button Sword;



    void Start()
    {
        //moneyamount = PlayerPrefs.GetInt("MoneyAmount");

        }


    void Update()
    {

        moneyAmountText.text = Nicholas.PlayableCharacter.moneyAmount.ToString() ;
        PriceSpear.text = priceSpear.ToString() + " монет";
        PriceSword.text = priceSword.ToString() + " монет";
        PriceShield.text = priceShield.ToString() + " монет";

    }

    public void LvlSpear()
    {
        Debug.Log(Nicholas.PlayableCharacter.moneyAmount);

        if (Nicholas.PlayableCharacter.moneyAmount >= priceSpear)
        {
            Nicholas.PlayableCharacter.moneyAmount -= priceSpear;
            priceSpear += 5;
          Enemy.LVLSpear += 1;
            PriceSpear.text = priceSpear.ToString() + " монет";
            Debug.Log(lvlspear);
            Debug.Log(Nicholas.PlayableCharacter.moneyAmount);
            Debug.Log(priceSpear);
            Debug.Log("//////////////");
        }
        


    }

    public void LvlShield()
    {
        Debug.Log(Nicholas.PlayableCharacter.moneyAmount);

        if (Nicholas.PlayableCharacter.moneyAmount >= priceShield)
        {

            Nicholas.PlayableCharacter.moneyAmount -= priceShield;
            priceShield += 5;
            Enemy.LVLShield+= 1;
            PriceShield.text = priceShield.ToString() + " монет";
            Debug.Log(lvlshield);
            Debug.Log(Nicholas.PlayableCharacter.moneyAmount);
            Debug.Log(priceShield);
        }
       


    }

    public void LvlSword()
    {
        Debug.Log(Nicholas.PlayableCharacter.moneyAmount);
        if (Nicholas.PlayableCharacter.moneyAmount >= priceSword)
        {

            Nicholas.PlayableCharacter.moneyAmount -= priceSword;
            priceSword += 5;
            Enemy.LVLSword += 1;
            PriceSword.text = priceSword.ToString() + " монет";
            Debug.Log(lvlsword);
            Debug.Log(Nicholas.PlayableCharacter.moneyAmount);
            Debug.Log(priceSword);
        }
       

    }





    public void exitShop()
    {

        PlayerPrefs.SetInt("moneyAmount", moneyamount);
        SceneManager.LoadScene("1");
    }

}
