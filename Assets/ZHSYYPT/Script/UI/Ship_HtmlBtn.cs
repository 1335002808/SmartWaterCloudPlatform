using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship_HtmlBtn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        Application.ExternalCall("Ship_Name", this.transform.name);
    }
}
