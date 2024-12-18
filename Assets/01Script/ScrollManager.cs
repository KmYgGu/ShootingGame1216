using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollManager : MonoBehaviour
{
    List<IBackGrondScroller> scrollObjects;
    [SerializeField] private float scrollSpeed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        scrollObjects = InterFaceFinder.FindObjectOfInterface<IBackGrondScroller>();

       // SetScrollSpeed(2.5f);// 추후 게임매니저에서 관리하도록 변경
    }

    // Update is called once per frame
    void Update()
    {
     foreach(var scroll in scrollObjects)
        {
            if(scroll != null)
            {
                scroll.Scroll(Time.deltaTime);
            }
        }
    }

    public void SetScrollSpeed(float newSpeed)
    {
        foreach(var scroll in scrollObjects)
        {
            if(scroll is IBackGrondScroller verticalScroll)
            {
                verticalScroll.SetScrollSpeed(newSpeed);
            }
        }
    }
}
