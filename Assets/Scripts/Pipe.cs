using UnityEngine;
using System.Collections;

public class Pipe : MonoBehaviour
{
    void Start()
    {
        RandomGeneratePosition();
    }
    public void RandomGeneratePosition()
    {
        // 在一定范围内随机y轴上一个坐标位置
        float pos_y = Random.Range(-0.4f, -0.1f);
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, pos_y, this.transform.localPosition.z);
    }
    void OnTriggerExit(Collider other)//触发器触发，加分数
    {//OnTriggerEnter OnTriggerStay OnTiggerExit
        if (other.tag == "Player")//
        {
            GetComponent<AudioSource>().Play();//添加声音
            GameManager._intance.score++;//分数加1
        }
    }
    void OnGUI()
    {
        GUILayout.Label("Score:" + GameManager._intance.score);//在左上角显示分数
    }
}
