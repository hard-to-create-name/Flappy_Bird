using UnityEngine;
using System.Collections;

public class FollowBird : MonoBehaviour
{
    private GameObject bird;
    private Transform birdTransform;
    void Start()
    {
        bird = GameObject.FindGameObjectWithTag("Player");
        birdTransform = bird.transform;
    }
    void Update()
    {
        Vector3 birdPos = birdTransform.position;
        float y = birdPos.y - 3.5088f;
        if (y > 2.4f)
        {
            y = 2.4f;
        }
        if (y < -2.4f)
        {
            y = -2.4f;
        }
        this.transform.position = new Vector3(birdPos.x + 1.99f, y, -10);//相机与小鸟相对静止
    }
}
