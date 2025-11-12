using System.Data.Common;
using UnityEngine;

public class cursor_ : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = false;      
    }
    
        private void OnEnable()
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition3D =Vector3.zero ;

    }

    // Update is called once per frame
    void Update()
    {

        Vector2 pos = Input.mousePosition;
        
    
        float posx = (float)(pos.x -461);
        float posz = (float)((pos.y - 189) * (-1));

        if (posz <= -14)
        {
            
            posz = -14;
        }
        if (posz >= 16)
        {
            Debug.Log("mousez bound " + posz);
            posz = 16;
        }
        
         if (posx <= -40)
        {
            
            posx = -40;
        }
        if (posx >= 40)
        {
            Debug.Log("mousez bound " + posz);
             posx = 40;
        }

        Vector3 pos_ = new Vector3((posz) , 8, posx);
        
        gameObject.GetComponent<RectTransform>().anchoredPosition3D = pos_;
        
        
        
    }
}
