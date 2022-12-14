using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeController : MonoBehaviour
{
    //Pega GameManager
    GameManager GM;

    //Movimentação
    Vector3 alvo;
    NavMeshAgent agente;

    //Vida Inimigo
    public int vida;

    Rigidbody rb;
    Animator animator;

    public GameObject Ataque;

    //Dano
    public int danoAtk;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        agente = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        alvo = GM.GetPlayerProximo(gameObject.transform.position);
        animator = GetComponent<Animator>();
        agente.destination = alvo;
    }

    // Update is called once per frame
    void Update()
    {
       
       
        Movimentação();
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.tag == "Player")
        {
            PersonagemController personagem = other.GetComponent<PersonagemController>();
            personagem.LevarDano(danoAtk);
        }
    }

    public void LevarDano(int dano)
    {
        vida -= dano;
        animator.SetTrigger("dano");
        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Movimentação()
    {
        alvo = GM.GetPlayerProximo(gameObject.transform.position);

        float distancia = Vector3.Distance(transform.position, alvo);
        if (distancia >= 2)
        {
            agente.SetDestination(alvo);
           
        } else if (distancia < 2 )
        {
            agente.velocity = Vector3.zero;
            animator.SetTrigger("atk");
        }
        animator.SetFloat("movimento", agente.velocity.magnitude);
    }

    void AtivarAtaque()
    {
        Ataque.SetActive(true);
    }

    void DesativarAtaque()
    {
        Ataque.SetActive(false);
    }
}
