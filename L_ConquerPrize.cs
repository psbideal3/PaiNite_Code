using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_ConquerPrize : MonoBehaviour
{
    [SerializeField] GameObject Money;

    private void OnEnable()
    {
        SpawnMoney();
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnMoney()
    {
        for (int i = 0; i < 10; i++)
        {
            Instantiate(Money, transform.position, transform.rotation);
        }
    }
}
