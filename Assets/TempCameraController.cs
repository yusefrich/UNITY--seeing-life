using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCameraController : MonoBehaviour
{
    public GameObject player;
    private bool followPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayer)
        {
            transform.position = new Vector3(
                player.transform.position.x,
                transform.position.y,
                transform.position.z);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            followPlayer = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            followPlayer = true;
        }
    }
}
