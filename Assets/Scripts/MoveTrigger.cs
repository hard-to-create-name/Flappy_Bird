using UnityEngine;
using System.Collections;

public class MoveTrigger : MonoBehaviour
{
    public Transform currentBg;
    public Pipe pipe1;
    public Pipe pipe2;
    public void OnTriggerEnter(Collider other)
    {
        print("OnTriggerEnter");
        if (other.tag == "Player")
        {
            Transform firstBg = GameManager._intance.firstBg;
            // 移动
            currentBg.position = new Vector3(firstBg.position.x + 10, currentBg.position.y, currentBg.position.z);
            GameManager._intance.firstBg = currentBg;
            // 管道获得随机新坐标
            pipe1.RandomGeneratePosition();
            pipe2.RandomGeneratePosition();
        }
    }
}
