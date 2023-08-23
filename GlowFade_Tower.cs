using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.SceneView;

public class GlowFade_Tower : MonoBehaviour
{
    Material material;

    int fadePropertyID;
    float fadeValue;
    [SerializeField] bool R;
    [SerializeField] GameObject Circle_1, Circle_2;
    [SerializeField] float fadeSpeed;
    [SerializeField] float Top, Bottom;

    public bool UnCharge, Uncharge_Spawn;       //Uncharge는 구슬 FadeOut 시킬지 여부로 true일 경우 구슬 Fadeout 안함. -> GlowIn에서 업그레이드 될 경우 True로 변경

    [SerializeField] GameObject R_Tower, L_Tower;

    [SerializeField] GameObject Camera;
    [SerializeField] GameObject MoneyHole;

    [SerializeField] GameObject Shop, R_Ball, R_Ball_02, L_Ball, L_Ball_02;

    [SerializeField] float R_LowValue, L_LowValue, R_Length, L_Length, L_SpawnValue, R_SpawnValue;
    [SerializeField] float L_FinalPos, R_FinalPos;

    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
        fadePropertyID = Shader.PropertyToID("_DirectionalGlowFadeFade");
    }

    private void OnEnable()
    {
        material = GetComponent<SpriteRenderer>().material;
        fadePropertyID = Shader.PropertyToID("_DirectionalGlowFadeFade");
        fadeValue = material.GetFloat(fadePropertyID);
    }
    //Start함수는 딱 한번만 호출됨. 스크립트가 꺼지고 다시 켜질때마다 값 초기화줄거면 void OnEnable() 써라.

    void FixedUpdate()
    {
        if (R)  //우측 건물 철거
        {
            if (fadeValue >= Bottom)
            {
                fadeValue -= Time.deltaTime * fadeSpeed;

                float CamPos = (fadeValue - R_LowValue) * R_FinalPos / R_Length;
                CamPos = Mathf.Floor(CamPos * 100f) / 100f;


                if (fadeValue <= R_SpawnValue && !Uncharge_Spawn)       //스폰 위치 변경
                {
                    Circle_1.GetComponent<TowerSelf_Fade>().enabled = true;
                    R_Tower.GetComponent<GlowIn_Tower>().FullCharge_Spawn = false;
                    Shop.GetComponent<Shop>().R_Base = true;
                    R_Ball.GetComponent<Ball_Logic>().Base = true;
                    R_Ball_02.GetComponent<Ball_Logic>().Base = true;
                    Uncharge_Spawn = true;
                }


                if (fadeValue <= Bottom)
                {
                    //CamPos = -7;

                    MoneyHole.GetComponent<Tower_R_Money>().Cancle();    //인간 돈 생성 취소
                    MoneyHole.GetComponent<Tower_R_Money>().enabled = false;

                    //Camera.GetComponent<CameraMove>().CheckMerOn = 1;   //시점 중앙으로 이동

                    fadeValue = Bottom;
                    R_Tower.GetComponent<GlowIn_Tower>().FullCharge = false;
                    
                    L_Tower.GetComponent<GlowIn_Tower>().enabled = true;
                    if (!UnCharge)
                    {
                        //Circle_1.GetComponent<TowerSelf_Fade>().enabled = true;
                        Circle_2.GetComponent<TowerSelf_Fade>().enabled = true;
                        UnCharge = true;
                    }
                    this.enabled = false;

                    /*Shop.GetComponent<Shop>().R_Base = true;
                    R_Ball.GetComponent<Ball_Logic>().Base = true;
                    R_Ball_02.GetComponent<Ball_Logic>().Base = true;*/
                }
                Camera.GetComponent<CameraMove>().Target = CamPos;

            }
        }
        if (!R) //좌측 건물 철거
        {
            if (fadeValue >= Bottom)
            {
                fadeValue -= Time.deltaTime * fadeSpeed;

                float CamPos = (fadeValue - L_LowValue) * L_FinalPos / L_Length;
                CamPos = Mathf.Floor(CamPos * 100f) / 100f;


                if (fadeValue <= L_SpawnValue && !Uncharge_Spawn)       //스폰 위치 변경
                {
                    Circle_1.GetComponent<TowerSelf_Fade>().enabled = true;
                    L_Tower.GetComponent<GlowIn_Tower>().FullCharge_Spawn = false;
                    Shop.GetComponent<Shop>().L_Base = true;
                    L_Ball.GetComponent<Ball_Logic>().Base = true;
                    L_Ball_02.GetComponent<Ball_Logic>().Base = true;
                    Uncharge_Spawn = true;
                }


                if (fadeValue <= Bottom)    //좀비 전부 철거했을 경우
                {
                    //CamPos = 7;

                    MoneyHole.GetComponent<Tower_L_Money>().Cancle();    //좀비 돈 생성 취소
                    MoneyHole.GetComponent<Tower_L_Money>().enabled = false;

                    //Camera.GetComponent<CameraMove>().CheckMerOn = 1;   //시점 중앙으로 이동

                    fadeValue = Bottom;
                    L_Tower.GetComponent<GlowIn_Tower>().FullCharge = false;
                    R_Tower.GetComponent<GlowIn_Tower>().enabled = true;
                    if (!UnCharge)
                    {
                        //Circle_1.GetComponent<TowerSelf_Fade>().enabled = true;
                        Circle_2.GetComponent<TowerSelf_Fade>().enabled = true;
                        UnCharge = true;
                    }
                    this.enabled = false;

                    /*Shop.GetComponent<Shop>().L_Base = true;
                    L_Ball.GetComponent<Ball_Logic>().Base = true;
                    L_Ball_02.GetComponent<Ball_Logic>().Base = true;*/
                }
                Camera.GetComponent<CameraMove>().Target = CamPos;
            }
        }
        material.SetFloat(fadePropertyID, fadeValue);

    }
}
