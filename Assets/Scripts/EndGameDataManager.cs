using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameDataManager : MonoBehaviour
{
    Text hp;
    // Start is called before the first frame update
    void Start()
    {
        hp = GetComponent<Text>();
        if (GameManager.getGM.imRich) {
            hp.text = ("你还剩" + GameManager.getGM.HP + "钱");
        }

        else
        {
            hp.text = ("你穷死了！！");

        }
        

        

    }

    
   
}
