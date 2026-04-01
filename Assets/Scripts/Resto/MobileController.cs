using System;
using TMPro;
using UnityEngine;

public class MobileController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI textField;
    private float initialTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        if(Input.touchCount > 0 )
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
                initialTime = Time.realtimeSinceStartup;
            
            if(Input.GetTouch(0).phase == TouchPhase.Moved)
                textField.text = "Moving";
            
            if(Input.GetTouch(0).phase == TouchPhase.Stationary)
                textField.text = "Static";
        
            if(Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                float finalTime = Time.realtimeSinceStartup - initialTime;
                textField.text = Input.GetTouch(0).position.ToString() + "\n" + finalTime;
            }

        }
    }
}
