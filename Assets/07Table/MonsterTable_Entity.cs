using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����ȭ : Ŭ������ ��ü�� �ؽ�Ʈ �������� ���ڿ��� �����ؼ� ����
//C# �� ��Ģ�� �����ϰ� ��� ����� ��Ģ��� ����ϰڴ�
//��Ʈ ������ �����͸� �ɰ��� �����ϴ� ����� ����Ų��

[System.Serializable]
public class MonsterTable_Entity
{
    public int MonsterID;// �̸��� ���ƾ���
    public string MonsterName;
    public int MonsterHP;
    public int MonsterScore;
    public int MonsterProjectTile;
    public string Pattern01;
    public string Pattern02;
}
