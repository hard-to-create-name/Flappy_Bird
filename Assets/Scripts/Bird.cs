using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour
{
    public float timer = 0;
    public int frameNumber = 10;
    public int frameCount = 0;
    public bool animation = false;// 是否播放飞行动画
    public bool canJump = false;
    void Update()
    {
        // 小鸟图片共有三帧，当游戏开始时，改变偏移量使其产生动画的效果
        if (GameManager._intance.GameState == GameManager.GAMESTATE_PLAYING)
        {
            timer += Time.deltaTime;
            if (timer >= 1.0f / frameNumber)
            {
                frameCount++;
                timer -= 1.0f / frameNumber;
                int frameIndex = frameCount % 3;
                //更新小鸟材质的偏移量 x
                this.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(0.333333f * frameIndex, 0));
            }
        }

        if (GameManager._intance.GameState == GameManager.GAMESTATE_PLAYING)
        {
            // 点击跳跃
            if (Input.GetMouseButton(0))
            {
                 //audio.play();已弃用
                GetComponent<AudioSource>().Play();//添加声音
                Vector3 vel2 = this.GetComponent<Rigidbody>().velocity;
                this.GetComponent<Rigidbody>().velocity = new Vector3(vel2.x, 5, vel2.z);
            }
        }
    }
    public void getLife()
    {
        GetComponent<Rigidbody>().useGravity = true;
        this.GetComponent<Rigidbody>().velocity = new Vector3(3, 0, 0);//初速度
    }
}
