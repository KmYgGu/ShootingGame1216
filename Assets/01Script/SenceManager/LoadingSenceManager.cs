using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 가비지 컬렉터 : 로딩을 통해서 씬을 바꿀 때 정리가 어느정도는 됨
public class LoadingSenceManager : MonoBehaviour
{
    [SerializeField] Image loadingBar;
    private AsyncOperation asyncScene;
    private float timeC;

    private void Awake()
    {
        //Debug.Log(PlayerPrefs.GetString(SAVE_Type.SAVE_SceneName.ToString()) + "이 씬을 로딩할 예정입니다");
        loadingBar.fillAmount = 0.0f;

        StartCoroutine("LoadAsyncSence");
    }

    IEnumerator LoadAsyncSence()
    {
        yield return new WaitForSeconds(3f); //용량이 적어 빠르게 넘어가기 때문에 강제로 딜레이줌

        //비동기 로딩
        asyncScene = SceneManager.LoadSceneAsync(PlayerPrefs.GetString(SAVE_Type.SAVE_SceneName.ToString()));
        asyncScene.allowSceneActivation = false;

        timeC = 0.0f;//로딩 하는 시간을 누적

        while (!asyncScene.isDone)
        {
            timeC += Time.deltaTime;

            if(asyncScene.progress >= 0.9f)// 로딩을 완료하고 다음 씬으로 넘어가기 위해서 기다리는 10%를 남겨줌
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, 1f, timeC);
                // 페이크 로딩
                if(loadingBar.fillAmount > 0.99f)
                    asyncScene.allowSceneActivation = true;// 이 부분이 실제로 다음씬으로 넘기는 코드
            }
            else// 실제 로딩 진행중
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, asyncScene.progress, timeC);
                if(loadingBar.fillAmount >= asyncScene.progress)
                    timeC = 0.0f;//쭉 진행되다 멈추게 하기
            }

            yield return null; //한프레임 프로그래스 넘기기
        }
        //yield return null; //한프레임 프로그래스 넘기기
    }
}
