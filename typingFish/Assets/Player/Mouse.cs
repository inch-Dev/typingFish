using UnityEngine;

public class Mouse : MonoBehaviour, IStateable
{
    public static Mouse instance;
    [SerializeField] float moveSpeed;   
    public void HandleState() { }

    void Start()
    {
        if (instance == null)
            instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        FollowMouse();
    }

    void FollowMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = new Vector3(mousePosition.x, mousePosition.y, 0f);
        transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        Debug.Log("kdjf");
	}
}
