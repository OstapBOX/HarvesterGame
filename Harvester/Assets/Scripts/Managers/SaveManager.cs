using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour {
    public static SaveManager instance { get; private set; }

    //What we want to save
    public int currentHarvester;
    public bool[] harvestersUnlocked = new bool[17] { true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };


    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
        Load();
    }

    public void Load() {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerDataStorage data = (PlayerDataStorage)bf.Deserialize(file);

            currentHarvester = data.currentHarvester;
            harvestersUnlocked = data.harvestersUnlocked;

            if (data.harvestersUnlocked == null) {
                harvestersUnlocked = new bool[17] { true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
            }

            file.Close();
        }
    }

    public void Save() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerDataStorage data = new PlayerDataStorage();


        data.currentHarvester = currentHarvester;
        data.harvestersUnlocked = harvestersUnlocked;

        bf.Serialize(file, data);
        file.Close();
    }


}

[Serializable]
class PlayerDataStorage {
    public int currentHarvester;
    public bool[] harvestersUnlocked;
}
