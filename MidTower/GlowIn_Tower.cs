using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.SceneView;

public class GlowIn_Tower : MonoBehaviour
{
    Material material;

    int fadePropertyID, fadePropertyID_2;
    float fadeValue;
    [SerializeField] bool R;
    [SerializeField] GameObject Circle_1, Circle_2;
    [SerializeField] float fadeSpeed;
    public bool FullCharge, FullCharge_Spawn;         //FullCharge는 완전히 업그레이드 되었는지 여부, False일 경우에만 구슬 Glow이며 마지막에 True로 바꿔줘야 한다.

    [SerializeField] float Top, Bottom;
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
        if (R)       //우측일 경우
        {
            if (fadeValue <= Top)
            {
                fadeValue += Time.deltaTime * fadeSpeed;

                float CamPos = (fadeValue - R_LowValue) * R_FinalPos / R_Length;
                CamPos = Mathf.Floor(CamPos * 100f) / 100f;


                if (fadeValue >= R_SpawnValue && !FullCharge_Spawn)             //스폰 위치 변경
                {
                    Circle_1.GetComponent<TowerSelf_Glow>().enabled = true;
                    R_Tower.GetComponent<GlowFade_Tower>().Uncharge_Spawn = false;
                    Shop.GetComponent<Shop>().R_Base = false;
                    R_Ball.GetComponent<Ball_Logic>().Base = false;
                    R_Ball_02.GetComponent<Ball_Logic>().Base = false;
                    FullCharge_Spawn = true;
                }


                if (fadeValue >= Top)       //완충시
                {
                    fadeValue = Top;
                    CamPos = R_FinalPos;
                    if (!FullCharge)
                    {
                        //Circle_1.GetComponent<TowerSelf_Glow>().enabled = true;   //안되면 주석 해제
                        Circle_2.GetComponent<TowerSelf_Glow>().enabled = true;
                        R_Tower.GetComponent<GlowFade_Tower>().UnCharge = false;
                        

                        MoneyHole.GetComponent<R_ConquerPrize>().enabled = true;    //정복금
                    }
                    FullCharge = true;
                    this.enabled = false;

                    //MoneyHole.GetComponent<R_ConquerPrize>().enabled = true;    //정복금
                    MoneyHole.GetComponent<Tower_R_Money>().enabled = true;     //돈 스폰 ON

                    /*Shop.GetComponent<Shop>().R_Base = false;
                    R_Ball.GetComponent<Ball_Logic>().Base = false;
                    R_Ball_02.GetComponent<Ball_Logic>().Base = false;*/
                }
                Camera.GetComponent<CameraMove>().Target = CamPos;
            }
            material.SetFloat(fadePropertyID, fadeValue);
        }
        if (!R)     //좌측일 경우
        {
            if (fadeValue <= Top)
            {
                fadeValue += Time.deltaTime * fadeSpeed;

                float CamPos = (fadeValue - L_LowValue) * L_FinalPos / L_Length;
                CamPos = Mathf.Floor(CamPos * 100f) / 100f;


                if (fadeValue >= L_SpawnValue && !FullCharge_Spawn)             //스폰 위치 변경
                {
                    Circle_1.GetComponent<TowerSelf_Glow>().enabled = true;
                    L_Tower.GetComponent<GlowFade_Tower>().Uncharge_Spawn = false;
                    Shop.GetComponent<Shop>().L_Base = false;
                    L_Ball.GetComponent<Ball_Logic>().Base = false;
                    L_Ball_02.GetComponent<Ball_Logic>().Base = false;
                    FullCharge_Spawn = true;
                }


                if (fadeValue >= Top)
                {
                    fadeValue = Top;
                    CamPos = L_FinalPos;
                    if (!FullCharge)
                    {
                        //Circle_1.GetComponent<TowerSelf_Glow>().enabled = true;   //안되면 주석 해제
                        Circle_2.GetComponent<TowerSelf_Glow>().enabled = true;
                        L_Tower.GetComponent<GlowFade_Tower>().UnCharge = false;
                        //L_Tower.GetComponent<GlowFade_Tower>().Uncharge_Spawn = false;

                        MoneyHole.GetComponent<L_ConquerPrize>().enabled = true;    //정복금
                    }
                    FullCharge = true;
                    this.enabled = false;

                    //MoneyHole.GetComponent<L_ConquerPrize>().enabled = true;    //정복금
                    MoneyHole.GetComponent<Tower_L_Money>().enabled = true;     //돈 스폰 ON

                    /*Shop.GetComponent<Shop>().L_Base = false;
                    L_Ball.GetComponent<Ball_Logic>().Base = false;
                    L_Ball_02.GetComponent<Ball_Logic>().Base = false;*/
                }
                Camera.GetComponent<CameraMove>().Target = CamPos;
            }
            material.SetFloat(fadePropertyID, fadeValue);
        }

    }
}
