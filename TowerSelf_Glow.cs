using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelf_Glow : MonoBehaviour
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
        fadeValue = 0;
    }
    //Start�Լ��� �� �ѹ��� ȣ���. ��ũ��Ʈ�� ������ �ٽ� ���������� �� �ʱ�ȭ�ٰŸ� void OnEnable() ���.

    void FixedUpdate()
    {
        if (fadeValue < 1)
        {
            fadeValue += Time.deltaTime * fadeSpeed;
            if (fadeValue > 1)
            {
                fadeValue = 1;
                this.enabled = false;
            }
        }
        material.SetFloat(fadePropertyID, fadeValue);
    }
}
