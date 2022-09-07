using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private SaveLoad loader;

    public void OnClickStart()
    {
        SaveLoad.Write(1, Nicholas.PlayerMode.Human, 100, 0);
       // SceneManager.LoadScene(1);
        Application.LoadLevel(1);
    }
    public void OnClickExit()
    {
        Application.Quit();

    }

    public void OnClickContinue() {
        loader = new SaveLoad();
        int readResult = SaveLoad.Read();
        if (readResult != 0)
            SceneManager.LoadScene(readResult);
        else
            return;
     }



}
