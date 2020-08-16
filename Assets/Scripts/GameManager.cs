using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour {
    private static GameManager gm;

    public bool imRich;
    public bool imPoor;

    public int[] ConsumeTimesCount = new int[13];

    public float ConsumemoneyCount;

    public static GameManager getGM{
        get{
            return gm;
        }
    }

    public float HP = 20000;
    public float Mood = 100;
    public float moodMultiplier=100;//买到好东西的时候回到100
    void Awake () {
        gm = this;
        GameObject.DontDestroyOnLoad(gameObject);
    }
    public void Init()
    {
        imPoor = false;
        imRich = false;
        HP = 20000;
        Mood = 100;
    }

    public void addHP(float count){
        HP = HP + count;
        if(HP>200000000f) HP=200000000f;
    }
    public void reduceHP(float count){
        HP = HP - count;
    }
    public void addMood(float count){
        Mood+=count;
        if(Mood>100) Mood=100;
    }
    public void reduceMood(float count){
        Mood=Mood - count*moodMultiplier/100;
    }
    
}