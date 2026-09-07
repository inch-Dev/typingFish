using TMPro;
using UnityEngine;

public class SessionUI : MonoBehaviour
{
    [HideInInspector] public static SessionUI instance;
    [SerializeField] TextMeshProUGUI sessionTF;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
            instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayTime(float secondsElapsed)
    {
        sessionTF.text = System.TimeSpan.FromSeconds(secondsElapsed).ToString("mm':'ss");
    }
}
