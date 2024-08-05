using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private Vector3 offset;

    [SerializeField] private float speedZ;
    [SerializeField] private float speedX;
    [SerializeField] private float[] lines = new float[3];

    private Player player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        if (player.live)
        {
            Vector3 targetPosition = target.position;

            targetPosition += offset;
            targetPosition.x = transform.position.x;
            targetPosition.y = transform.position.y;

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speedZ);

            targetPosition = transform.position;
            targetPosition.x = lines[player.currentLine];
            targetPosition.y = transform.position.y;

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speedX);
        }
    }
}
