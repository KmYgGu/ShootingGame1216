using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class ShootingGame : ScriptableObject
{
	//저장하는 데이터를 엑셀의 형식으로 관리해주는 방식

	//첫 번째 시트의 이름으로 리스트를 만든다
	public List<MonsterTable_Entity> MonsterTable; // Replace 'EntityType' to an actual type that is serializable.
}
