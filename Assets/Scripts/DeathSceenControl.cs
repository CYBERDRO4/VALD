using UnityEngine;



public class DeathSceenControl : MonoBehaviour
{
    [SerializeField] private GameObject textObj;
    [SerializeField] private GameObject buttonsObj;

    public void ShowText()
    {
        textObj.SetActive(true);
    }
    public void ShowButtons() {
        buttonsObj.SetActive(true);
    }


}
