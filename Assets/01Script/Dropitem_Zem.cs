using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Dropitem_Zem : MonoBehaviour, IPickuped
{
    public delegate void PickupZem();
    public static event PickupZem OnPickupJam;

    private Rigidbody2D rig;

    private bool isSetTarget = false;
    private GameObject target;
    private float pickupTimeper;

    private void Awake()
    {
        if(TryGetComponent<CircleCollider2D>(out CircleCollider2D col))
        {
            col.radius = 0.2f;
            col.isTrigger = true;
        }

        if(TryGetComponent<Rigidbody2D>(out rig))
        {
            rig.gravityScale = 1f;

            Vector2 InitVelocity = Vector2.zero;
            InitVelocity.x = Random.Range(-0.5f, 0.5f);
            InitVelocity.y = Random.Range(3f, 4f);
            rig.AddForce(InitVelocity, ForceMode2D.Impulse);
        }
    }

    private void Update()
    {
        if(isSetTarget && target.activeSelf)//해당 오브젝트가 활성화 되어있는지
        {
            pickupTimeper += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, target.transform.position,pickupTimeper/2f);
            //2초 내에 목적지에 도착하겠다는 선형 보간의 코드

            if(pickupTimeper / 2f > 1.0f)//목표위치에 도달
            {
                OnPickupJam?.Invoke();
                Destroy(gameObject);
            }
        }
    }

    public void OnPickUp(GameObject picker)
    {
        rig.gravityScale = 0f;
        rig.velocity = Vector2.zero;
        isSetTarget = true;
        target = picker;
        pickupTimeper = 0f;
    }
}
