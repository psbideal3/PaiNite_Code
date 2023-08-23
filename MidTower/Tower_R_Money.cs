using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_R_Money : MonoBehaviour
{
    [SerializeField] GameObject Money;
    [SerializeField] int Time;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        InvokeRepeating("Spawn_Money", 4, Time);
    }

    void Spawn_Money()
    {
        Instantiate(Money, transform.position, transform.rotation);
    }

    public void Cancle()
    {
        CancelInvoke("Spawn_Money");
    }
}
