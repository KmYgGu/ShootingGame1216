using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingSenceManager : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log(PlayerPrefs.GetString(SAVE_Type.SAVE_SceneName.ToString()) + "이 씬을 로딩할 예정입니다");
    }
}
