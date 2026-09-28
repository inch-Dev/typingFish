using System.Collections;
using System.Transactions;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [HideInInspector] public static ScoreManager instance;
    int _score;
    int _fishCaught = 0;

    public void CatchFish()
    {
        _fishCaught++;
        ScoreUI.instance.DisplayFishCaught(_fishCaught);
    }

    float _currentScoredWordDifficultyMult;
    public void SetScoredWordDifficulty(WordDifficulty difficulty)
    {
        switch(difficulty)
        {
            case WordDifficulty.EASY:
                _currentScoredWordDifficultyMult = 1f;
                break;
            case WordDifficulty.MEDIUM:
                _currentScoredWordDifficultyMult = 1.5f;
                break;
            case WordDifficulty.HARD:
                _currentScoredWordDifficultyMult = 2.5f;
                break;
        }
    }
    float _currentFishCaughtSizeScore;

    public void SetFishCaughtSize(FishSize size)
    {
        switch (size)
        {
            case FishSize.SMALL:
                _currentFishCaughtSizeScore = 500;
                break;
            case FishSize.MEDIUM:
                _currentFishCaughtSizeScore = 250;
                break;
            case FishSize.LARGE:
                _currentFishCaughtSizeScore = 100;
                break;
        }
    }



    public float CalculateScoreIncrease()
    {
        float scoreIncrease;

        scoreIncrease = _currentFishCaughtSizeScore * _currentScoredWordDifficultyMult;

        return scoreIncrease;
    }

    private void Start()
    {
        if (instance == null)
            instance = this;
    }

    void Update()
    {
        if(_currentScoredWordDifficultyMult != 0 && _currentFishCaughtSizeScore != 0)
        {
            _score += (int) CalculateScoreIncrease();
            ScoreUI.instance.DisplayScore(_score);
            _currentScoredWordDifficultyMult = 0;
            _currentFishCaughtSizeScore = 0;
        }
    }
}
