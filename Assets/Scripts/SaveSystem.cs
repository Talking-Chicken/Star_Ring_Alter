using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void saveDialogueNodesCount(Codex codex)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/DialogueNodesCount.codex";
        FileStream stream = new FileStream(path, FileMode.Create);

        CodexData data = new CodexData(codex);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static CodexData loadCodex()
    {
        string path = Application.persistentDataPath + "/DialogueNodesCount.codex";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            CodexData data = formatter.Deserialize(stream) as CodexData;
            stream.Close();

            return data;
        } else
        {
            Debug.LogError("Save File not fount in " + path);
            return null;
        }
    }
}
