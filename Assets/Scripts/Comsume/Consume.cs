using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consume : MonoBehaviour
{
    public enum ConsumeType
    {
        固定消费点,
        随机消费点
    }
    public int ID;//消费点id
    public string _name;//名字
    public string _assestsName;//资源名字
    public float refreshTime;//刷新间隔
    float tempTime;
    public float moneyReduce;//消耗财富
    public float moodAdd;//心情增长

    public ConsumeType type;
    private Collider2D coll;
    private SpriteRenderer thisSprite;
    bool isBuy = false;
    private void Start() {
        thisSprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        tempTime=refreshTime;
    }
    private void Update() {
        if(isBuy)
        {
            refreshTime-=Time.deltaTime;
            if(refreshTime<0)
                Refresh();
        }
    }
    private void OnCollisionEnter(Collision other) {
        if(other.collider.CompareTag("Player"))
        {
            if(GameManager.getGM.HP>=moneyReduce)
            {
                succesBuy();
                //触发对话  成功购买
                Debug.Log("成功购买");
            }
            else
            {
                //触发对话  钱不够
            }
        }
    }
    void succesBuy()
    {
        isBuy = true;
        coll.enabled = false;
        thisSprite.enabled = false;
        GameManager.getGM.reduceHP(moneyReduce);
        GameManager.getGM.addMood(moodAdd);
        GameManager.getGM.ConsumeTimesCount[ID]++;
        GameManager.getGM.ConsumemoneyCount += moneyReduce;
    }
    void Refresh()
    {
        if(type == ConsumeType.随机消费点)
        {
            //随机刷新
            Vector2 randPos = new Vector2(Random.Range(-1776,1776),Random.Range(-1008,1008));
            transform.position = randPos;
        }
        isBuy = false;
        coll.enabled = false;
        thisSprite.enabled = false;
    }
}
