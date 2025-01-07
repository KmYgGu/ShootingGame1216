using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//직렬화 : 클래스의 객체를 텍스트 파일인지 문자열을 변형해서 저장
//C# 언어를 규칙을 무시하고 모든 언어의 규칙대로 사용하겠다
//비트 단위로 데이터를 쪼개서 저장하는 방식을 가르킨다

[System.Serializable]
public class MonsterTable_Entity
{
    public int MonsterID;// 이름이 같아야함
    public string MonsterName;
    public int MonsterHP;
    public int MonsterScore;
    public int MonsterProjectTile;
    public string Pattern01;
    public string Pattern02;
}
