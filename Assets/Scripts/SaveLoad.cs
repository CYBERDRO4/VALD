using UnityEngine;
using System;
using System.IO;

public class SaveLoad : MonoBehaviour
{
    private static string savedGamePath = Application.dataPath + "/SaveFileFolder/" + "SaveFile.txt";
    private Nicholas.HumanGhostTransformation transformation;

    public static void Write(int sceneNumber, Nicholas.PlayerMode mode, float health, int money) {
        if (!File.Exists(savedGamePath))
        {
            switch (mode)
            {
                case Nicholas.PlayerMode.Human:
                    string result = sceneNumber.ToString() + "\nHuman\n" + health.ToString() + "\n" + money.ToString();
                    File.WriteAllText(savedGamePath, result, System.Text.Encoding.UTF8);
                    return;
                case Nicholas.PlayerMode.Ghost:
                    string result1 = sceneNumber.ToString() + "\nGhost\n" + health.ToString() + "\n" + money.ToString();
                    File.WriteAllText(savedGamePath, result1, System.Text.Encoding.UTF8);
                    return;

            }
        }
        else
        {
            File.Delete(savedGamePath);
            Write(sceneNumber, mode, health, money);
        }
         
    }
    public static int ReadMoneyAmount()
    {
        if (File.Exists(savedGamePath))
        {
            string[] res = File.ReadAllLines(savedGamePath);
            return Convert.ToInt32(res[3]);
        }
        else
            return 0;
    }
    public static int Read() {
        if (File.Exists(savedGamePath))
        {
            string[] saveData = File.ReadAllLines(savedGamePath);
            switch (saveData[1]) {
                case "Human":
                    Nicholas.HumanGhostTransformation.setMode(Nicholas.PlayerMode.Human);
                    Nicholas.PlayableCharacter.health = Convert.ToInt32(saveData[2]);
                    Nicholas.PlayableCharacter.moneyAmount = Convert.ToInt32(saveData[3]);
                    return Convert.ToInt32(saveData[0]);
                case "Ghost":
                    Nicholas.HumanGhostTransformation.setMode(Nicholas.PlayerMode.Ghost);
                    return Convert.ToInt32(saveData[0]);
            }
          
        }
        return 0;

    }

}
