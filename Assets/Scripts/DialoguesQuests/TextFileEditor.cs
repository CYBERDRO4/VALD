using System.IO;
using UnityEditor;
using UnityEngine;
namespace Nicholas
{
    public class TextFileEditor : MonoBehaviour
    {
        public string fileName = "default";

        [TextArea(1, 10)] [SerializeField] private string text;

        public void SaveDialogueFile()
        {
            string path = Application.dataPath + "/Resources/" + "Dialogues/" + "RU/" + fileName + ".txt";
            if (!File.Exists(path) && text.Length > 0)
            {
                File.WriteAllText(path, text);
                Debug.Log("Файл создан");
            }
            else
                Debug.Log("Файл с таким названием уже существует");
        }


    }


    [CustomEditor(typeof(TextFileEditor))]
    class TextInput : Editor
    {
#if UNITY_EDITOR
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            GUILayout.Label("Юзай шаблон типа\nName:# text");
            TextFileEditor editor = (TextFileEditor)target;
            GUILayout.Space(35);
            if (GUILayout.Button("Save file", GUILayout.Height(25)))
                editor.SaveDialogueFile();
        }

#endif
    }
}
