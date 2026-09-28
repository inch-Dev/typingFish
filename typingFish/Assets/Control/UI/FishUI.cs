using TMPro;
using UnityEngine;

public class FishUI : UI
{
    public static FishUI instance;
    TextMeshProUGUI _fishCaughtTF;
    public void DisplayFishCaught(int numFish)
    {
        _fishCaughtTF.text = "FISH CAUGHT: " + numFish.ToString();
    }
    void Start()
    {
        if (instance == null)
            instance = this;

        _fishCaughtTF = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
