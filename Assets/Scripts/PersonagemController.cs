using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonagemController : MonoBehaviour
{
    public Image imgVida;


    //Vida
    public int vida;
    int VidaMax;

    //Dano
    public int danoAtk;

    public GameObject goAtk;

    //Velocidade Movimentação
    public float speed;


    //Velocidade para Rotação
    public float speedRotation;

    Rigidbody rigidbody;

    Animator animator;


    public int playerNumber;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        VidaMax = vida;
    }

    // Update is called once per frame
    void Update()
    {
        Movimento();
        atacar();
    }
     

    void Movimento()
    {
        float hAxis = Input.GetAxis("HorizontalP"+playerNumber);
        float vAxis = Input.GetAxis("VerticalP"+playerNumber);

        Vector3 move = new Vector3(hAxis, 0, vAxis);
        animator.SetFloat("movimento",move.magnitude);

        rigidbody.velocity = move.normalized * speed * Time.deltaTime;

        if(move != Vector3.zero)
        {
            Quaternion rotacao = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotacao, speedRotation * Time.deltaTime);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Inimigo")
        {
            SlimeController slime = other.GetComponent<SlimeController>();
            slime.LevarDano(danoAtk);
        }
    }

    public void LevarDano(int dano)
    {
        vida -= dano;
        imgVida.fillAmount = (float)vida / (float)VidaMax;
        animator.SetTrigger("dano");
        if (vida <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    void atacar()
    {
        if (Input.GetButtonDown("Fire1P"+playerNumber))
        {
            animator.SetTrigger("atk");
        }
    }

    public void AtivarAtaque()
    {
        goAtk.SetActive(true);
    }

    public void DesativarAtaque()
    {
        goAtk.SetActive(false);
    }
}
