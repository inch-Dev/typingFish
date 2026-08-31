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

    [SerializeField] List<FishData> allFishData;

    [SerializeField] List<FishData> wildFishData;

    [SerializeField] List<FishData> caughtFishData;

    [SerializeField] List<Fish> spawnedFish;

    Fish catchingFish;
    public Fish GetCatchingFish() { return catchingFish; }
    public void SetCatchingFish(Fish fish) { catchingFish = fish; }

    public FishData GetRandomFishData()
    {
        return GetRandomFishData(allFishData);
    }

    public FishData GetRandomFishData(bool hasCaught)
    {
        if(hasCaught)
        {
            return GetRandomFishData(caughtFishData);
        }

        else
        {
            return GetRandomFishData(wildFishData);
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
                //Debug.Log("Setting collider on");
                break;
            default:
                fish.collider.enabled = false;
                //Debug.Log("Setting collider off");
                break;
        }     

        spawnedFish.Add(prefab.GetComponent<Fish>());
    }


    public void CatchFish()
    {
        CatchFish(catchingFish);
        SetCatchingFish(null);
    }

    public void CatchFish(Fish fish)
    {
        Debug.Log("Caught!");
        wildFishData.Remove(fish.fishData);

        if (!caughtFishData.Contains(fish.fishData))
            caughtFishData.Add(fish.fishData);

        spawnedFish.Remove(fish);
        Destroy(fish.gameObject);
    }

    public void MissFish()
    {
        MissFish(catchingFish);
        SetCatchingFish(null);
    }

    public void MissFish(Fish fish)
    {
        Debug.Log("Missed!");
        spawnedFish.Remove(fish);
        Destroy(fish.gameObject);
    }

    void ClearFish()
    {
        allFishData.Clear();
        wildFishData.Clear();
        caughtFishData.Clear();
        SetCatchingFish(null);
    }

    void InitFish()
    {
        ClearFish();

		fishGuids = AssetDatabase.FindAssets("t:FishData");

		foreach (string guid in fishGuids)
		{
			string path = AssetDatabase.GUIDToAssetPath(guid);
			FishData newFishData = AssetDatabase.LoadAssetAtPath(path, typeof(FishData)) as FishData;

			allFishData.Add(newFishData);

            if (newFishData.isCaught)
                caughtFishData.Add(newFishData);
            else
                wildFishData.Add(newFishData);
		}
	}

	private void Start()
	{
        if (instance == null)
            instance = this;

        InitFish();
	}
}
