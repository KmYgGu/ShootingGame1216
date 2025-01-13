using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 게임 전반적인 흐름을 관리, (싱글 톤)
// 1. 게임의 시작, 중지,종료
// 2. 사운드 관리자
// 3. 동적로딩 : 에셋 로딩
// 4. 씬 관리 : 씬 병경 관리.
// 5. 입력시스템

public class GameManager : SingleTon<GameManager>
{
    private PlayerController pc;
    private ScrollManager scrollManager;
    private IinputHandle inputHandle;
    private EnemySpwonManager enemySpawnManager;
    private MetaoSpwonManager meteoManager;
    private ScoreManager scoreManager;

    public ScoreManager GetScoreManager => scoreManager;
    

    private void Start()
    {
        LoadSenceInit();// 임시로 작성 : 추후 씬변경 로직을 완료후 수정
        StartCoroutine(GameStart());// 임시...
    }


    //씬이 변경이 되어 로딩이 끝났을 때
    //한번씩 호출이 되도록
    private void LoadSenceInit()
    {
        pc = FindAnyObjectByType<PlayerController>();
        scrollManager = FindAnyObjectByType<ScrollManager>();
        inputHandle = GetComponent<KeyBordInputHandle>();
        enemySpawnManager = FindAnyObjectByType<EnemySpwonManager>();
        meteoManager = FindAnyObjectByType<MetaoSpwonManager>();
        scoreManager = FindAnyObjectByType<ScoreManager>();
    }

    private void Update()
    {
        if(inputHandle != null)
            pc?.CustomUpdate(inputHandle.Getinput());

    }

    //게임이 시작이 될때 처리되어야하는 일련의 로직들을
    //순서에 맞춰서 수행해주는 역할
    IEnumerator GameStart()
    {
        yield return null;
        scoreManager?.InitScoreReset();
        yield return new WaitForSeconds(1f);
        pc?.StartGame();
        scrollManager?.SetScrollSpeed(2.5f);

        yield return new WaitForSeconds(1f);
        enemySpawnManager.InitSpawnManager();

        yield return new WaitForSeconds(3f);
        meteoManager.StartSpawnMeteo();
    }
}
