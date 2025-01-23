using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static SaveSystem;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance { get; private set; }

    [System.Serializable]
    public class SaveData
    {
        public int currentGeneration;
        public List<RagdollData> ragdollList;
    }

    [System.Serializable]
    public class RagdollData
    {
        public string prefabName;
        public List<MovementArticulation[]> movementCycles;

        public RagdollData(Ragdoll ragdoll)
        {
            prefabName = ragdoll.prefabsRagdoll.name;
            movementCycles = new List<MovementArticulation[]>
            {
                ragdoll.leftShoulder_MovementCycle,
                ragdoll.leftUpArm_MovementCycle,
                ragdoll.leftDownArm_MovementCycle,
                ragdoll.rightShoulder_MovementCycle,
                ragdoll.rightUpArm_MovementCycle,
                ragdoll.rightDownArm_MovementCycle,
                ragdoll.leftArticulation_MovementCycle,
                ragdoll.leftUpLeg_MovementCycle,
                ragdoll.leftDownLeg_MovementCycle,
                ragdoll.rightArticulation_MovementCycle,
                ragdoll.rightUpLeg_MovementCycle,
                ragdoll.rightDownLeg_MovementCycle,
                ragdoll.head_MovementCycle
            };
        }

        public Ragdoll ToRagdoll(GameObject prefab)
        {
            Ragdoll ragdoll = new Ragdoll(prefab);
            ragdoll.leftShoulder_MovementCycle = movementCycles[0];
            ragdoll.leftUpArm_MovementCycle = movementCycles[1];
            ragdoll.leftDownArm_MovementCycle = movementCycles[2];
            ragdoll.rightShoulder_MovementCycle = movementCycles[3];
            ragdoll.rightUpArm_MovementCycle = movementCycles[4];
            ragdoll.rightDownArm_MovementCycle = movementCycles[5];
            ragdoll.leftArticulation_MovementCycle = movementCycles[6];
            ragdoll.leftUpLeg_MovementCycle = movementCycles[7];
            ragdoll.leftDownLeg_MovementCycle = movementCycles[8];
            ragdoll.rightArticulation_MovementCycle = movementCycles[9];
            ragdoll.rightUpLeg_MovementCycle = movementCycles[10];
            ragdoll.rightDownLeg_MovementCycle = movementCycles[11];
            ragdoll.head_MovementCycle = movementCycles[12];
            return ragdoll;
        }
    }

    private string saveFilePath;

    private void Awake()
    {
        // Singleton pour garantir une instance unique
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Persiste entre les scènes

        saveFilePath = Path.Combine(Application.persistentDataPath, "saveData.json");
        LoadOnStart();
    }

    public void Save(int currentGeneration, List<Ragdoll> ragdolls)
    {
        SaveData saveData = new SaveData
        {
            currentGeneration = currentGeneration,
            ragdollList = new List<RagdollData>()
        };

        foreach (var ragdoll in ragdolls)
        {
            saveData.ragdollList.Add(new RagdollData(ragdoll));
        }

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(saveFilePath, json);
        Debug.Log($"Data saved to {saveFilePath}");
    }

    public void Save(int currentGeneration, List<RagdollDepartureArrival> ragdollList)
    {
        SaveData saveData = new SaveData
        {
            currentGeneration = currentGeneration,
            ragdollList = new List<RagdollData>()
        };

        foreach (var ragdoll in ragdollList)
        {
            saveData.ragdollList.Add(new RagdollData(ragdoll.ragdoll));
        }

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(saveFilePath, json);
        Debug.Log($"Data saved to {saveFilePath}");
    }


    public (int, List<Ragdoll>) Load(Dictionary<string, GameObject> prefabDictionary)
    {
        if (!File.Exists(saveFilePath))
        {
            Debug.LogWarning("Save file not found!");
            return (0, new List<Ragdoll>());
        }

        string json = File.ReadAllText(saveFilePath);
        SaveData saveData = JsonUtility.FromJson<SaveData>(json);

        List<Ragdoll> loadedRagdolls = new List<Ragdoll>();
        foreach (var ragdollData in saveData.ragdollList)
        {
            if (prefabDictionary.TryGetValue(ragdollData.prefabName, out var prefab))
            {
                loadedRagdolls.Add(ragdollData.ToRagdoll(prefab));
            }
            else
            {
                Debug.LogWarning($"Prefab {ragdollData.prefabName} not found in dictionary!");
            }
        }

        return (saveData.currentGeneration, loadedRagdolls);
    }

    public void LoadOnStart()
    {
        if (File.Exists(saveFilePath))
        {
            Debug.Log("Save file found. Loading game data...");
            // Place code here to initialize your game state with the loaded data.
        }
        else
        {
            Debug.Log("No save file found. Starting with default values.");
        }
    }

    public void ResetSave()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Save file deleted. Progress has been reset.");
        }
        else
        {
            Debug.LogWarning("No save file to delete.");
        }
    }
}
