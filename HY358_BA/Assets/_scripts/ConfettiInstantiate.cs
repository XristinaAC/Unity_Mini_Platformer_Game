using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiInstantiate : MonoBehaviour {

    // Use this for initialization
    public GameObject ConfettiPrefab;
    public int confettiNum = 20;
    public Transform crownMiddle;
    public float radius = 5.0F;
    public float power = 10.0F;
    public Material mat;
    public Color conColor;

    private Rigidbody[] confetti;


    // Update is called once per frame
    public void ConfettiExplosion()
    {
        confetti = new Rigidbody[confettiNum];
        Rigidbody rb = ConfettiPrefab.GetComponent<Rigidbody>();
        for (int i = 0; i < confettiNum; ++i)
        {
            confetti[i] = Instantiate(rb, crownMiddle.position, Quaternion.identity) as Rigidbody;
            confetti[i].AddForce(crownMiddle.position,ForceMode.Impulse);
            mat.color = conColor;
        }
    }
}
