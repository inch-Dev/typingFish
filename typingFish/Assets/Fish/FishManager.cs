using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class FishManager : MonoBehaviour
{
    public static FishManager instance;

    string[] fishGuids;

    [SerializeField] List<Fish> allFish;

    [SerializeField] List<Fish> wildFish;

    [SerializeField] List<Fish> caughtFish;


    public Fish GetRandomFish()
    {
        return GetRandomFish(allFish);
    }

    public Fish GetRandomFish(bool hasCaught)
    {
        if(hasCaught)
        {
            return GetRandomFish(caughtFish);
        }

        else
        {
            return GetRandomFish(wildFish);
        }
    }

    public Fish GetRandomFish(List<Fish> fishList)
    {
		int index = Random.Range(0, fishList.Count);

		return fishList[index];
	}

    public void SpawnFish()
    {

    }


    public void SpawnFish(Fish fish)
    {

    }


    public void CatchFish(Fish fish)
    {
        wildFish.Remove(fish);

        if (!caughtFish.Contains(fish))
            caughtFish.Add(fish);
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

		fishGuids = AssetDatabase.FindAssets("t:Fish");

		foreach (string guid in fishGuids)
		{
			string path = AssetDatabase.GUIDToAssetPath(guid);
			Fish newFish = AssetDatabase.LoadAssetAtPath(path, typeof(Fish)) as Fish;

			allFish.Add(newFish);

            if (newFish.fishData.isCaught)
                caughtFish.Add(newFish);
            else
                wildFish.Add(newFish);
		}
	}
}
