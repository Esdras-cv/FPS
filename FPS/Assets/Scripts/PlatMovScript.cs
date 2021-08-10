using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatMovScript : MonoBehaviour
{
    public float velocidade = 3f;
    public Transform posFinal;

    private Vector3 posInicial;
    private Vector3 destinoAtual;
    private bool voltando;
    // Start is called before the first frame update
    void Start()
    {
        posInicial = transform.position;
        destinoAtual = posFinal.position;
        voltando = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float movimento = velocidade * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, destinoAtual, movimento);
        float distDestino = Vector3.Distance(transform.position, destinoAtual);

        if(distDestino <= 0.1f)
        {
            if(voltando)
            {
                voltando = false;
                destinoAtual = posFinal.position;
            }
            else
            {
                voltando = true;
                destinoAtual = posInicial;
            }
        }
    }
}
