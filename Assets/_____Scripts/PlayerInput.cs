using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float playerSpeed = 5f;
    public GameObject attackPrefab;

    // Update is called once per frame
    void Update()
    {
        // Handle player movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * playerSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World);

        // Handle player attack
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        // Spawn attack prefab or perform attack logic here
        // For simplicity, let's just print a message to the console
        Debug.Log("Player attacks!");
    }
}