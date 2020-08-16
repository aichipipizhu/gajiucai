using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider hpSlider;
    public Slider moodSlider;
    
    void Start()
    {
        GameManager.getGM.Init();
        StartCoroutine("MoodAdd");
        StartCoroutine("HpAdd");
    }

    void Update()
    {
        //moodSlider.value=GameManager.getGM.Mood/100f;
        //hpSlider.value=GameManager.getGM.HP/20000f;
    }

    IEnumerator HpAdd()
    {
        while(GameManager.getGM.HP>=0)
        {
            yield return new WaitForSeconds(1);
            GameManager.getGM.addHP(1000000);
           // Debug.Log(GameManager.getGM.HP);
        }
        yield return null;
    }

    IEnumerator MoodAdd()//开始游戏调用
    {
        while(GameManager.getGM.Mood>=0)
        {
            GameManager.getGM.reduceMood(2.0f);
            GameManager.getGM.moodMultiplier-=2.0f;
            yield return new WaitForSeconds(1);
        }
        yield return null;
    }
    
}
