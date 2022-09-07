using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public void InMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


}
