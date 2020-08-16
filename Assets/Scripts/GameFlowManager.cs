using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour
{

    public string WinSceneName;


    public string loseSceneName;

    

    GameManager m_gm;
    string m_SceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
        m_gm = GameManager.getGM;

    }

    // Update is called once per frame
    void Update()
    {

        if (m_gm.HP <=0)
        {
            m_gm.imPoor = true;
        }
        if (m_gm.imRich)
        {
            EndGame(true);
        }
        if (m_gm.imPoor)
        {
            EndGame(false);
        }
    }
    void EndGame(bool isWin)
    {
        
        if (isWin)
        {
            SceneManager.LoadScene( WinSceneName);

        }
        else
        {
            SceneManager.LoadScene (loseSceneName);

        }

    }
}
