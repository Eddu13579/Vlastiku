using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    GameObject playerToFollow;

    float minDistanceToPlayer = -5;
    float distanceToPlayer = -10;
    float maxDistanceToPlayer = -20;

    private void Start()
    {
        var worldSpaceHeight = this.GetComponent<Camera>().orthographicSize * 2;
        var worldSpaceWidth = worldSpaceHeight * this.GetComponent<Camera>().aspect;
    }

    void Update()
    {
        distanceToPlayer += Input.mouseScrollDelta.y;
        if (distanceToPlayer > minDistanceToPlayer)
        {
            distanceToPlayer = minDistanceToPlayer;
        } else if (distanceToPlayer < maxDistanceToPlayer)
        {
            distanceToPlayer = maxDistanceToPlayer;
        }       
    }

    void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerToFollow.transform.position.x, playerToFollow.transform.position.y, distanceToPlayer), Time.deltaTime * 1000);
    }
}
