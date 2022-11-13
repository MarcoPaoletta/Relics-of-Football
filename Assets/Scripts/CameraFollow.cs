using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 1.85f, -10);
    public int smoothFactor = 7;

    Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        ClampPosition();
    }

    void ClampPosition()
    {
        transform.position = new Vector3(Mathf.Max(0, transform.position.x), Mathf.Clamp(target.position.y, 0, 3), transform.position.z);
    }

    void FixedUpdate()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        Vector3 targetPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }
}
