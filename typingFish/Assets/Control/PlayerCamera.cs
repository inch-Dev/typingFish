using UnityEngine;

public class PlayerCamera : MonoBehaviour, IStateable
{
    public void HandleState()
    {
        switch (GameManager.instance.GetState())
        {
            case GameState.TYPING:
                isFollowingPlayer = false;
                break;
            case GameState.FISHING:
                isFollowingPlayer = true;
                break;
        }

    }

    bool isFollowingPlayer = false;
    [SerializeField] GameObject hook;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void FixedUpdate()
	{
        if (isFollowingPlayer)
            FollowPlayer();
	}

	void FollowPlayer()
    {
        transform.position = new Vector3(hook.transform.position.x, hook.transform.position.y, -10f);
	}
}
