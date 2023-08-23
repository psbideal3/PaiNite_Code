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
    public bool FullCharge, FullCharge_Spawn;         //FullCharge�� ������ ���׷��̵� �Ǿ����� ����, False�� ��쿡�� ���� Glow�̸� �������� True�� �ٲ���� �Ѵ�.

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
    //Start�Լ��� �� �ѹ��� ȣ���. ��ũ��Ʈ�� ������ �ٽ� ���������� �� �ʱ�ȭ�ٰŸ� void OnEnable() ���.

    void FixedUpdate()
    {
        if (R)       //������ ���
        {
            if (fadeValue <= Top)
            {
                fadeValue += Time.deltaTime * fadeSpeed;

                float CamPos = (fadeValue - R_LowValue) * R_FinalPos / R_Length;
                CamPos = Mathf.Floor(CamPos * 100f) / 100f;


                if (fadeValue >= R_SpawnValue && !FullCharge_Spawn)             //���� ��ġ ����
                {
                    Circle_1.GetComponent<TowerSelf_Glow>().enabled = true;
                    R_Tower.GetComponent<GlowFade_Tower>().Uncharge_Spawn = false;
                    Shop.GetComponent<Shop>().R_Base = false;
                    R_Ball.GetComponent<Ball_Logic>().Base = false;
                    R_Ball_02.GetComponent<Ball_Logic>().Base = false;
                    FullCharge_Spawn = true;
                }


                if (fadeValue >= Top)       //�����
                {
                    fadeValue = Top;
                    CamPos = R_FinalPos;
                    if (!FullCharge)
                    {
                        //Circle_1.GetComponent<TowerSelf_Glow>().enabled = true;   //�ȵǸ� �ּ� ����
                        Circle_2.GetComponent<TowerSelf_Glow>().enabled = true;
                        R_Tower.GetComponent<GlowFade_Tower>().UnCharge = false;
                        

                        MoneyHole.GetComponent<R_ConquerPrize>().enabled = true;    //������
                    }
                    FullCharge = true;
                    this.enabled = false;

                    //MoneyHole.GetComponent<R_ConquerPrize>().enabled = true;    //������
                    MoneyHole.GetComponent<Tower_R_Money>().enabled = true;     //�� ���� ON

                    /*Shop.GetComponent<Shop>().R_Base = false;
                    R_Ball.GetComponent<Ball_Logic>().Base = false;
                    R_Ball_02.GetComponent<Ball_Logic>().Base = false;*/
                }
                Camera.GetComponent<CameraMove>().Target = CamPos;
            }
            material.SetFloat(fadePropertyID, fadeValue);
        }
        if (!R)     //������ ���
        {
            if (fadeValue <= Top)
            {
                fadeValue += Time.deltaTime * fadeSpeed;

                float CamPos = (fadeValue - L_LowValue) * L_FinalPos / L_Length;
                CamPos = Mathf.Floor(CamPos * 100f) / 100f;


                if (fadeValue >= L_SpawnValue && !FullCharge_Spawn)             //���� ��ġ ����
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
                        //Circle_1.GetComponent<TowerSelf_Glow>().enabled = true;   //�ȵǸ� �ּ� ����
                        Circle_2.GetComponent<TowerSelf_Glow>().enabled = true;
                        L_Tower.GetComponent<GlowFade_Tower>().UnCharge = false;
                        //L_Tower.GetComponent<GlowFade_Tower>().Uncharge_Spawn = false;

                        MoneyHole.GetComponent<L_ConquerPrize>().enabled = true;    //������
                    }
                    FullCharge = true;
                    this.enabled = false;

                    //MoneyHole.GetComponent<L_ConquerPrize>().enabled = true;    //������
                    MoneyHole.GetComponent<Tower_L_Money>().enabled = true;     //�� ���� ON

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
