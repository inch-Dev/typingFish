using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class FishManager : MonoBehaviour, IStateable
{
    //Toggle off Fish Collision
    public void HandleState()
    {
        switch(GameManager.instance.GetState())
        {
            case GameState.FISHING:
                SpawnFish();
                break;
            default:
                break;
        }
    }
    public static FishManager instance;

    string[] fishGuids;

    [SerializeField] GameObject smallFishPF;
    [SerializeField] GameObject mediumFishPF;
    [SerializeField] GameObject largeFishPF;

    [SerializeField] List<FishData> allFish;

    [SerializeField] List<FishData> wildFish;

    [SerializeField] List<FishData> caughtFish;

    [SerializeField] List<Fish> spawnedFish;


    public FishData GetRandomFishData()
    {
        return GetRandomFishData(allFish);
    }

    public FishData GetRandomFishData(bool hasCaught)
    {
        if(hasCaught)
        {
            return GetRandomFishData(caughtFish);
        }

        else
        {
            return GetRandomFishData(wildFish);
        }
    }

    public FishData GetRandomFishData(List<FishData> fishList)
    {
		int index = Random.Range(0, fishList.Count);

		return fishList[index];
	}

    public void SpawnFish()
    {
        FishData newFish = GetRandomFishData();

        SpawnFish(newFish);
    }


    public void SpawnFish(FishData fishData)
    {
        //Get size prefab
        GameObject prefab = null;

        switch(fishData.fishSize)
        {
            case FishSize.SMALL:
                prefab = smallFishPF;
                break;
            case FishSize.MEDIUM:
                prefab = mediumFishPF;
                break;
            case FishSize.LARGE:
                prefab = largeFishPF;
                break;
        }

        //Need Fish Spawning Zone
        GameObject.Instantiate(prefab, new Vector3(0,-5,0), Quaternion.identity);
        Fish fish = prefab.GetComponent<Fish>();
        fish.fishData = fishData;

        switch (GameManager.instance.GetState())
        {
            case GameState.FISHING:
                fish.collider.enabled = true;
                Debug.Log("Setting collider on");
                break;
            default:
                fish.collider.enabled = false;
                Debug.Log("Setting collider off");
                break;
        }     

        spawnedFish.Add(prefab.GetComponent<Fish>());
    }


    public void CatchFish(Fish fish)
    {
        wildFish.Remove(fish.fishData);

        if (!caughtFish.Contains(fish.fishData))
            caughtFish.Add(fish.fishData);
    }

    void ClearFish()
    {
        allFish.Clear();
        wildFish.Clear();
        caughtFish.Clear();
    }

    void InitFish()
    {
        ClearFish();

		fishGuids = AssetDatabase.FindAssets("t:FishData");

		foreach (string guid in fishGuids)
		{
			string path = AssetDatabase.GUIDToAssetPath(guid);
			FishData newFishData = AssetDatabase.LoadAssetAtPath(path, typeof(FishData)) as FishData;

			allFish.Add(newFishData);

            if (newFishData.isCaught)
                caughtFish.Add(newFishData);
            else
                wildFish.Add(newFishData);
		}
	}

	private void Start()
	{
        if (instance == null)
            instance = this;

        InitFish();
	}
}
