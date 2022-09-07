using UnityEngine.SceneManagement;
using UnityEngine;

public class DeathScreenBtns : MonoBehaviour
{
    private void Start()
    {
        
    }

    public void onClickRestart() {
        GameObject.Find("DeathScreen").SetActive(false);
        SceneManager.LoadScene(SaveLoad.Read());
    }
    public void onClickExit() {
        Application.Quit();
    }

}
