using UnityEngine;

public enum WordDifficulty
{
    NULL = -1,
    EASY,
    MEDIUM,
    HARD,
}

[CreateAssetMenu(fileName = "Word", menuName = "ScriptableObjects/Word", order = 1)]
public class Word : ScriptableObject
{
    public string value;
    public WordDifficulty difficulty;
    public bool hasLearned = false;
    [HideInInspector] public float accuracy = 0.0f;
    [HideInInspector] public float speed = 0.0f;

    [HideInInspector] public int timesTyped = 0;
    [HideInInspector] public int timesEncountered = 0;

    public void UpdateStats()
    {
        accuracy = timesTyped / timesEncountered;
        speed = speed / timesEncountered;
    }

}
