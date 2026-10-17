using TMPro;
using UnityEngine;

public class SessionUI : UI
{
    [HideInInspector] public static SessionUI instance;
    [SerializeField] TextMeshProUGUI sessionTF;
    
    void Start()
    {
		if (instance == null)
			instance = this;
	}

    public void DisplayTime(float secondsElapsed)
    {
        sessionTF.text = System.TimeSpan.FromSeconds(secondsElapsed).ToString("mm':'ss");
    }
}
