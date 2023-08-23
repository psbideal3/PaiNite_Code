using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelf_Fade : MonoBehaviour
{
    Material material;

    int fadePropertyID;
    float fadeValue;
    [SerializeField] float fadeSpeed;

    void Start()
    {
        //Get material reference
        material = GetComponent<SpriteRenderer>().material;
        fadePropertyID = Shader.PropertyToID("_FullGlowDissolveFade");

        //Set fade value to zero at start.
    }

    private void OnEnable()
    {
        fadeValue = 1;
    }
    //Start함수는 딱 한번만 호출됨. 스크립트가 꺼지고 다시 켜질때마다 값 초기화줄거면 void OnEnable() 써라.

    void FixedUpdate()
    {
        //Update while fade value is less than 1.
        if (fadeValue > 0)
        {
            //Increase fade value over time.
            fadeValue -= Time.deltaTime * fadeSpeed;
            if (fadeValue < 0)
            {
                fadeValue = 0;
                this.enabled = false;
            }
            //Update value in material.
        }
        material.SetFloat(fadePropertyID, fadeValue);
    }
}
