using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneButton : MonoBehaviour
{
    public string sceneName = "";
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(LoadTargetScene);
    }

    // Update is called once per frame
    void Update()
    {


    }



    void LoadTargetScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
