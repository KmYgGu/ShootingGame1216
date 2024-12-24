using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertLine : MonoBehaviour
{
    [SerializeField] private GameObject prefabMeteo;
    private Animator anims;
    private Animator Anims
    {
        get
        {
            if(anims == null)
                anims = GetComponent<Animator>();
            return anims;
        }
    }
    //월드에 라인이 처음 생성이 됬을때
    public void SpawnedLine()
    {
        //Anims.SetTrigger("Spawn");    //추후에 폭탄 기능에 사용될 예정
        Invoke("SpawnMetro", 2.0f);

    }

    private void SpawnMetro()
    {
        Vector3 spawnPos = transform.position;
        spawnPos.y = 8.0f;
        GameObject obj = Instantiate(prefabMeteo, spawnPos, Quaternion.identity);
        if (obj.TryGetComponent<MetaoLight>(out MetaoLight metorite))
        {
            metorite.InitMetro();
            Destroy(gameObject);

        }
    }
}
