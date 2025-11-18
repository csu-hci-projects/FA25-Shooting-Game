using System.Data.Common;
using UnityEngine;

public class cursor_ : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    bool isbound = false;
    void Start()
    {
        Cursor.visible = false;      
    }
    
        private void OnEnable()
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0,15,0);

        


    }

    // Update is called once per frame
    void Update()
    {

        Vector2 pos = Input.mousePosition;
        
    
        float posx = (float)(pos.x -461);
        float posz = (float)((pos.y - 189) * (-1));

        if (posz <= -14)
        {
            isbound = false;
            posz = -14;
        }
        if (posz >= 16)
        {
            isbound = false;
            Debug.Log("mousez bound " + posz);
            posz = 16;
        }
        
         if (posx <= -40)
        {
            isbound = false;
            posx = -40;
        }
        if (posx >= 40)
        {
            Debug.Log("mousez bound " + posz);
             posx = 40;
             isbound = false;
        }


    if (isbound){
        Vector3 pos_ = new Vector3((posz) , 8, posx);
        gameObject.GetComponent<RectTransform>().anchoredPosition3D = pos_;
        }
        else
        {
            isbound = true;
        }
        
    }
}
