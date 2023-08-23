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

    public bool UnCharge, Uncharge_Spawn;       //Uncharge�� ���� FadeOut ��ų�� ���η� true�� ��� ���� Fadeout ����. -> GlowIn���� ���׷��̵� �� ��� True�� ����

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
    //Start�Լ��� �� �ѹ��� ȣ���. ��ũ��Ʈ�� ������ �ٽ� ���������� �� �ʱ�ȭ�ٰŸ� void OnEnable() ���.

    void FixedUpdate()
    {
        if (R)  //���� �ǹ� ö��
        {
            if (fadeValue >= Bottom)
            {
                fadeValue -= Time.deltaTime * fadeSpeed;

                float CamPos = (fadeValue - R_LowValue) * R_FinalPos / R_Length;
                CamPos = Mathf.Floor(CamPos * 100f) / 100f;


                if (fadeValue <= R_SpawnValue && !Uncharge_Spawn)       //���� ��ġ ����
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

                    MoneyHole.GetComponent<Tower_R_Money>().Cancle();    //�ΰ� �� ���� ���
                    MoneyHole.GetComponent<Tower_R_Money>().enabled = false;

                    //Camera.GetComponent<CameraMove>().CheckMerOn = 1;   //���� �߾����� �̵�

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
        if (!R) //���� �ǹ� ö��
        {
            if (fadeValue >= Bottom)
            {
                fadeValue -= Time.deltaTime * fadeSpeed;

                float CamPos = (fadeValue - L_LowValue) * L_FinalPos / L_Length;
                CamPos = Mathf.Floor(CamPos * 100f) / 100f;


                if (fadeValue <= L_SpawnValue && !Uncharge_Spawn)       //���� ��ġ ����
                {
                    Circle_1.GetComponent<TowerSelf_Fade>().enabled = true;
                    L_Tower.GetComponent<GlowIn_Tower>().FullCharge_Spawn = false;
                    Shop.GetComponent<Shop>().L_Base = true;
                    L_Ball.GetComponent<Ball_Logic>().Base = true;
                    L_Ball_02.GetComponent<Ball_Logic>().Base = true;
                    Uncharge_Spawn = true;
                }


                if (fadeValue <= Bottom)    //���� ���� ö������ ���
                {
                    //CamPos = 7;

                    MoneyHole.GetComponent<Tower_L_Money>().Cancle();    //���� �� ���� ���
                    MoneyHole.GetComponent<Tower_L_Money>().enabled = false;

                    //Camera.GetComponent<CameraMove>().CheckMerOn = 1;   //���� �߾����� �̵�

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
