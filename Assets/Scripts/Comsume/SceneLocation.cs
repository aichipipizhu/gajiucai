using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SceneLocation : MonoBehaviour
{
    public enum SceneType
    {
        售楼中心,
        证券交易所
    } 

    public SceneType type;
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.CompareTag("Player"))
        {
            Debug.Log("碰到");
            if(type==SceneType.售楼中心)
            {
                if(GameManager.getGM.HP>=1000000f)
                    Win();
                else
                    GameManager.getGM.reduceMood(10f);
            }
            else if(type==SceneType.证券交易所)
            {
                StartCoroutine("Investing");
            }
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
        if(other.collider.CompareTag("Player"))
        {
            if(type==SceneType.证券交易所)
            {
                StopCoroutine("Investing");
            }
        }
    }
    IEnumerator Investing()//炒股
    {
        while(true)
        {
            int rand = Random.Range(0,10);//左闭右开
            if(rand<7)//0123456  70%
            {
                GameManager.getGM.addHP(300);
            }
            else if(rand<8)//7  10%
            {
                GameManager.getGM.addHP(1500);
            }
            else//89    20%
            {
                GameManager.getGM.reduceHP(2500);
            }
            Debug.Log(GameManager.getGM.HP);
            yield return new WaitForSeconds(1);
        }
    }
    void Win()
    {
        GameManager.getGM.imRich = true;

        Debug.Log("胜利");
        //那不可能赢
    }

}
