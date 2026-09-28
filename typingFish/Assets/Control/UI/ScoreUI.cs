using TMPro;
using UnityEngine;

public class ScoreUI : UI
{
    public static ScoreUI instance;
    [SerializeField] TextMeshProUGUI _scoreTF;
    public void DisplayScore(float score)
    {
        _scoreTF.text = "SCORE: " + score.ToString();
    }
    [SerializeField] TextMeshProUGUI _fishCaughtTF;
    public void DisplayFishCaught(int numFish)
    {
        _fishCaughtTF.text = "FISH CAUGHT: " + numFish.ToString();
    }
    void Start()
    {
        if (instance == null)
            instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
