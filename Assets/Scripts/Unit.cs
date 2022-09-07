using UnityEngine;


//  Класс-родитель для скриптов игрока.
public class Unit : MonoBehaviour
{
    public static bool canMove = true;

    public void changeState() {
        canMove = !canMove;
      }
}
