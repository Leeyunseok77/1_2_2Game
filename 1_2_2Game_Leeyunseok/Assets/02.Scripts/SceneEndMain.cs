using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEndMain : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Main"); // 넘어가고 싶은 씬의 이름을 적어주세요오
    }
}
