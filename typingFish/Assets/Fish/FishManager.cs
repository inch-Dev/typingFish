using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class FishManager : MonoBehaviour, IStateable
{
    //Toggle off Fish Collision
    public void HandleState()
    {
        switch(GameManager.instance.GetState())
        {
            case GameState.CASTING:
                //Spawn a bunch of fish
                isSpawning = true;
                break;
            case GameState.FISHING:
                isSpawning = true;
                break;
            case GameState.PAUSED:
            case GameState.SESSION_OVER:
                isSpawning = false;
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

    [SerializeField] Vector2 spawnSideDistances;
    bool isSpawning = false;
    [SerializeField] float spawnTimeIntervalSeconds;
    float spawnTimeElapsedSeconds;
    [SerializeField] Vector2 verticalSpawnOffsetRange;

    Fish catchingFish;

    public Fish GetCatchingFish() { return catchingFish; }
    public void SetCatchingFish(Fish fish)
    { 
        catchingFish = fish;
        if(fish != null)
            fish.Catching();
    }

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
        //Get Size Prefab
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

        //Get Random Side && Orient Movement 
        int randomSide = Random.Range(0, 2);
        float spawnX = 0;
        Vector2 moveDirection = Vector2.zero;

        switch(randomSide)
        {
            case 0:
                spawnX = spawnSideDistances.x;
                moveDirection = Vector2.right;

                break;
            case 1:
                spawnX = spawnSideDistances.y;
                moveDirection = Vector2.left;
                break;
        }

        //Spawn At Position
        float spawnOffset = Random.Range(verticalSpawnOffsetRange.x, verticalSpawnOffsetRange.y);
        GameObject newFish =GameObject.Instantiate(prefab, new Vector3(spawnX, Hook.instance.transform.position.y + spawnOffset,0), Quaternion.identity);
        Fish fish = newFish.GetComponent<Fish>();
        fish.fishData = fishData;

        
        //Ignore Collision with Other Fish
        foreach(Fish spawnedFish in spawnedFish)
        {
            Physics2D.IgnoreCollision(fish.collider, spawnedFish.collider);
        }



        fish.moveDirection = moveDirection;
        if (moveDirection == Vector2.left)
            fish.GetSpriteRenderer().flipX = true;
        fish.SetMove(true);

        switch (GameManager.instance.GetState())
        {
            case GameState.CASTING:
            case GameState.FISHING:
                fish.collider.enabled = true;
                break;
            default:
                fish.collider.enabled = false;
                break;
        }     

        spawnedFish.Add(fish);
    }


    public void CatchFish()
    {
        CatchFish(catchingFish);
        SetCatchingFish(null);
    }

    public void CatchFish(Fish fish)
    {
        //Debug.Log("Caught!");
        wildFishData.Remove(fish.fishData);

        if (!caughtFishData.Contains(fish.fishData))
            caughtFishData.Add(fish.fishData);

        spawnedFish.Remove(fish);
        fish.Caught();
        SpawnFish();
    }

    public void MissFish()
    {
        MissFish(catchingFish);
        SetCatchingFish(null);
    }

    public void MissFish(Fish fish)
    {
        //Debug.Log("Missed!");
        spawnedFish.Remove(fish);
        fish.Missed();
        SpawnFish();
    }

    public void RemoveFish(Fish fish)
    {
        if (spawnedFish.Contains(fish))
        {
            spawnedFish.Remove(fish);
			Destroy(fish.gameObject);
		}

        else
        {
            Debug.Log("Not contained");
        }
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

    private void FixedUpdate()
    {
        if(isSpawning)
        {
            spawnTimeElapsedSeconds += Time.fixedDeltaTime;

            if(spawnTimeElapsedSeconds >= spawnTimeIntervalSeconds)
            {
                SpawnFish();
                spawnTimeElapsedSeconds = 0;
            }
        }
    }
}
