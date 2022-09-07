using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nicholas
{
    public class NextLevelScript : MonoBehaviour
    {
        [Header("Индекс следующей сцены. Не путать с названием")]
        [SerializeField] private int nextSceneIndex;
        [Header("Можно ли будет трансформироваться из человека/призрака в человека/призрака в след.сцене")]
        [SerializeField] private bool canTransformInNextScene;
        [Header("Спавн на след.сцене будет в том же состоянии, что присутствует во время перехода")]
        [SerializeField] private bool noChanges; // Сохранять ли состояние с предыдущего уровня
        [Header("Необходимое состояние Человек/Призрак для перехода на след.уровень. Если особых условий нет, то оставить NONE")]
        [SerializeField] private PlayerMode stateToNextLevel; // Необходимое состояние, чтобы попасть на новый уровень
        [Header("Если не выбрано noChanges, то на след.уровне спавн будет в этом состояннии")]
        [SerializeField] private PlayerMode nextLevelRespawnMode;
        [Header("Нужно ли сначала убить босса прежде чем перейти на след.уровень.")]
        [SerializeField] private bool nextLevelAfterBossFall;
        [Header("Объект босса уровня")]
        [SerializeField] private GameObject boss;
        private HumanGhostTransformation transformation; 


        private void Start()
        {
           transformation = GameObject.FindGameObjectWithTag("Player").GetComponent<HumanGhostTransformation>();
        }

        public void goToNextScene()
        {
            if (nextLevelAfterBossFall)
            {
                if (boss == null)
                {
                    if (stateToNextLevel != PlayerMode.None && HumanGhostTransformation.getMode() != stateToNextLevel)
                        SceneManager.LoadScene(SaveLoad.Read());
                    else
                    {
                        if (!noChanges)
                            HumanGhostTransformation.setMode(nextLevelRespawnMode);
                        transformation.SetCanToTransform(canTransformInNextScene);
                        SceneManager.LoadScene(nextSceneIndex);
                        SaveLoad.Write(nextSceneIndex, HumanGhostTransformation.getMode(), PlayableCharacter.health, PlayableCharacter.moneyAmount);
                    }
                }
            }
            else
            {
                if (stateToNextLevel != PlayerMode.None && HumanGhostTransformation.getMode() != stateToNextLevel)
                    SceneManager.LoadScene(SaveLoad.Read());
                else
                {
                    if (!noChanges)
                        HumanGhostTransformation.setMode(nextLevelRespawnMode);
                    transformation.SetCanToTransform(canTransformInNextScene);
                    SceneManager.LoadScene(nextSceneIndex);
                    SaveLoad.Write(nextSceneIndex, HumanGhostTransformation.getMode(), PlayableCharacter.health, PlayableCharacter.moneyAmount);
                }
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
                goToNextScene();
        }
    }
}

