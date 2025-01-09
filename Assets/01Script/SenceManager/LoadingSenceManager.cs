using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ������ �÷��� : �ε��� ���ؼ� ���� �ٲ� �� ������ ��������� ��
public class LoadingSenceManager : MonoBehaviour
{
    [SerializeField] Image loadingBar;
    private AsyncOperation asyncScene;
    private float timeC;

    private void Awake()
    {
        //Debug.Log(PlayerPrefs.GetString(SAVE_Type.SAVE_SceneName.ToString()) + "�� ���� �ε��� �����Դϴ�");
        loadingBar.fillAmount = 0.0f;

        StartCoroutine("LoadAsyncSence");
    }

    IEnumerator LoadAsyncSence()
    {
        yield return new WaitForSeconds(3f); //�뷮�� ���� ������ �Ѿ�� ������ ������ ��������

        //�񵿱� �ε�
        asyncScene = SceneManager.LoadSceneAsync(PlayerPrefs.GetString(SAVE_Type.SAVE_SceneName.ToString()));
        asyncScene.allowSceneActivation = false;

        timeC = 0.0f;//�ε� �ϴ� �ð��� ����

        while (!asyncScene.isDone)
        {
            timeC += Time.deltaTime;

            if(asyncScene.progress >= 0.9f)// �ε��� �Ϸ��ϰ� ���� ������ �Ѿ�� ���ؼ� ��ٸ��� 10%�� ������
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, 1f, timeC);
                // ����ũ �ε�
                if(loadingBar.fillAmount > 0.99f)
                    asyncScene.allowSceneActivation = true;// �� �κ��� ������ ���������� �ѱ�� �ڵ�
            }
            else// ���� �ε� ������
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, asyncScene.progress, timeC);
                if(loadingBar.fillAmount >= asyncScene.progress)
                    timeC = 0.0f;//�� ����Ǵ� ���߰� �ϱ�
            }

            yield return null; //�������� ���α׷��� �ѱ��
        }
        //yield return null; //�������� ���α׷��� �ѱ��
    }
}
