using UnityEngine;

namespace Nicholas
{


      //Триггер-активатор диалога  

    public class DialogueControl : MonoBehaviour
    {
        public GameObject dialogueManager;
        private DialogueScript dialogue;

        private void Start()
        {
            dialogue = dialogueManager.GetComponent<DialogueScript>();
        }

        private void Update()
        {
            if (dialogue.isDialogActivated && Input.GetKeyDown(KeyCode.X))
                dialogue.ShowNextPhrase();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Debug.Log("Триггер");
                if (!dialogue.isDialogueReaded())
                    dialogue.StartDialogue();
            }
        }

    }
}

