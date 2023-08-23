using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidTower : MonoBehaviour
{
    [SerializeField] GameObject R_MidTower;
    [SerializeField] GameObject L_MidTower;
    public bool L_On, R_On;
    [SerializeField] bool targetOn;
    [SerializeField] float size;

    Material R_Mat, L_Mat;
    int fadePropertyID;
    float R_Value, L_Value;

    [SerializeField] Collider2D[] hit2;
    [SerializeField] float L_Limit, R_Limit;


    void Start()
    {
        InvokeRepeating("Check", 0f, 0.25f);

        R_Mat = R_MidTower.GetComponent<SpriteRenderer>().material;
        L_Mat = L_MidTower.GetComponent<SpriteRenderer>().material;
        fadePropertyID = Shader.PropertyToID("_DirectionalGlowFadeFade");
    }

    void Update()
    {
        TowerChange();
    }

    void Check()
    {
        hit2 = Physics2D.OverlapBoxAll(new Vector3(0, -2.5f, 0), new Vector2(size, 2), 0, LayerMask.GetMask("L", "R"));

        if (hit2.Length > 0)
        {
            targetOn = true;
            L_On = false;
            R_On = false;
            for (int i = 0; i < hit2.Length; i++)
            {
                if (hit2[i].tag == "L")
                {
                    L_On = true;
                }
                if (hit2[i].tag == "R")
                {
                    R_On = true;
                }
            }
        }
    }


    void TowerChange()
    {
        if (targetOn)
        {
            if (R_On && L_On)
            {
                Stop();
            }
            if (!R_On && L_On)
            {
                L_Upgrade();
            }
            if (R_On && !L_On)
            {
                R_Upgrade();
            }
        }
    }

    void Stop()
    {
        R_MidTower.GetComponent<GlowFade_Tower>().enabled = false;
        R_MidTower.GetComponent<GlowIn_Tower>().enabled = false;
        L_MidTower.GetComponent<GlowFade_Tower>().enabled = false;
        L_MidTower.GetComponent<GlowIn_Tower>().enabled = false;
    }

    void R_Upgrade()
    {
        R_TowerCheck();
    }
    void L_Upgrade()
    {
        L_TowerCheck();
    }

    void R_TowerCheck() //인간이 점령할 경우
    {
        L_Value = L_Mat.GetFloat(fadePropertyID);
        if (L_Value > L_Limit) //좀비가 어느정도 점거하였을 경우
        {
            L_MidTower.GetComponent<GlowIn_Tower>().enabled = false;
            L_MidTower.GetComponent<GlowFade_Tower>().enabled = true;
        }
        else
        {
            R_MidTower.GetComponent<GlowIn_Tower>().enabled = true;
        }
    }

    void L_TowerCheck() //좀비가 점령할 경우
    {
        R_Value = R_Mat.GetFloat(fadePropertyID);
        if (R_Value > R_Limit)  //인간이 어느정도 점거하였을 경우
        {
            R_MidTower.GetComponent<GlowFade_Tower>().enabled = true;
            R_MidTower.GetComponent<GlowIn_Tower>().enabled = false;
        }
        else
        {
            L_MidTower.GetComponent<GlowIn_Tower>().enabled = true;
        }
    }
}
