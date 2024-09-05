using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject otherTarget;
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            transform.position = new Vector3(player.transform.position.x,
                                            player.transform.position.y, -10);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if(otherTarget != null){
                Vector2 pos = Vector2.Lerp(player.transform.position,
                            otherTarget.transform.position, 0.5f);
                transform.position = new Vector3(pos.x, pos.y, -10);
            } else{
                transform.position = new Vector3(player.transform.position.x,
                    player.transform.position.y, -10);
            }
        }
    }
}
