using TMPro;
using UnityEngine;

public class SessionUI : UI
{
    [HideInInspector] public static SessionUI instance;
    [SerializeField] TextMeshProUGUI sessionTF;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

	// Update is called once per frame
	void Update()
    {
		if (instance == null)
			instance = this;
	}

    public void DisplayTime(float secondsElapsed)
    {
        sessionTF.text = System.TimeSpan.FromSeconds(secondsElapsed).ToString("mm':'ss");
    }
}
