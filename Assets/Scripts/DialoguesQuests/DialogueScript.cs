using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Nicholas
{
    public class DialogueScript : MonoBehaviour
    {
        [SerializeField] private string dialogueFilePath;

        [SerializeField] private GameObject dialoguePanel;
        [SerializeField] private Text nameUI;
        [SerializeField] private Text textUI;
        [SerializeField] private bool haveEvent = false;
        [SerializeField] EventType eventType;
        private PlayableCharacter playerUnit;
        private string text = null; //Временное хранилище всей каши - в эту пременную записываются всё из .txt файла для последующей сортировки
        int phrasesCount; // Количество фраз диалога
        private List<Phrase> dialogue; // Содержит все реплики диалога
        private bool isReaded = false;
        public bool isDialogActivated;
        private int currentPage = 0;



        private void Start()
        {
            playerUnit = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayableCharacter>();
        }

        void Load(string path)
        {
            if (File.Exists(path))
            {
                dialogue = new List<Phrase>();
                NameTextSeparator separator = new NameTextSeparator('#'); // Пора сортировать эту кучу
                text = File.ReadAllText(path);
                string[] phrases = text.Split('\n');
                phrasesCount = phrases.Length;
                for (int i = 0; i < phrasesCount; i++)
                    dialogue.Add(separator.SeparateForPhrase(phrases[i]));
            }
        }

        public bool isDialogueReaded() { return isReaded; }

        public void StartDialogue()
        {
            Load(dialogueFilePath);

            ShowPhrase(0);
            dialoguePanel.SetActive(true);
            isDialogActivated = true;
            playerUnit.canMove = false;

        }
        public void ShowPhrase(int index) {
            if (index < phrasesCount)
            {
                nameUI.text = dialogue[index].name;
                textUI.text = dialogue[index].phrase;
            }
            else
            {
                Exit();
            }
        }
        public void ShowNextPhrase()
        {
            if (currentPage < phrasesCount)
            {
                nameUI.text = dialogue[currentPage].name;
                textUI.text = dialogue[currentPage].phrase;
                currentPage++;
            }
            else
            {
                Exit();
            }
        }
        public void Exit()
        {
            dialoguePanel.SetActive(false);
            dialogue = null;
            currentPage = 0;
            isDialogActivated = false;

            playerUnit.canMove = true;
            if (!isReaded)
                isReaded = 1 > 0; // Глэк

        }
    }

    public class NameTextSeparator
    {
        /**
     * Класс, позволяющий преобразовать строку вида (имя:#фраза)
     * в объект класса Phrase, использующийся в диалогах
     */
        private char separatorChar;
        public NameTextSeparator(char separatorChar) { this.separatorChar = separatorChar; }
        //Возвращает все, что находится до условного разделителя (имя:#фраза)
        public string getName(string src)
        {
            string res;
            int indexOfSeparator = src.IndexOf(separatorChar);
            res = src.Substring(0, src.Length - (src.Length - indexOfSeparator));
            return res;
        }
        //Возвращает все, что находится после условного разделителя (имя:#фраза)
        public string getText(string src)
        {
            string res;
            int indexOfSeparator = src.IndexOf(separatorChar) + 1;
            res = src.Substring(indexOfSeparator, src.Length - indexOfSeparator);
            return res;
        }
        // Создание новой фразы диалога напрямую
        public Phrase SeparateForPhrase(string src)
        {
            return new Phrase(getName(src), getText(src));
        }
    }

    public class Phrase
    {
        public string name;
        public string phrase;
        public Phrase(string name, string phrase)
        {
            this.name = name; this.phrase = phrase;
        }
    }

    
}
