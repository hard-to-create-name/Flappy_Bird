using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PipeUpOrDown : MonoBehaviour {
    public AudioSource hitMusic;
    public AudioSource dieMusic;
    public Text scoreUIText;

    void OnCollisionEnter(Collision other)
    {
        print("OnCollisionEnter");
        if (other.gameObject.tag == "Player")
        {
            hitMusic.Play();
            dieMusic.Play();//发出声音
            GameManager._intance.GameState = GameManager.GAMESTATE_END;//撞击之后发出声音并转换为游戏结束状态
        }
    }
}
