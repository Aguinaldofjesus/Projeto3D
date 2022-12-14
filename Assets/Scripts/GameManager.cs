using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //Prefab Slime
    public GameObject prefabSlime;

    //Spawn
    public Transform[] Spawns;
    public float TempoSpawn;
    float contadorTempoSpawn;

    //Pega o Player
    public Transform[] players;

    //Gravidade do Jogo
    public Vector3 Gravidade;

    public GameObject MainCamera;

    public GameObject CameraP1;
    public GameObject CameraP2;

    public GameObject vcamP1;
    public GameObject vcamP2;
    public GameObject vcamGroup;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = Gravidade;
    }

    // Update is called once per frame
    void Update()
    {
        Spawnar();
        AtualizarCameras();
    }

    void Spawnar()
    {
        contadorTempoSpawn += Time.deltaTime;
        if(contadorTempoSpawn > TempoSpawn)
        {
            int sorteio = Random.Range(0, Spawns.Length);
            Instantiate(prefabSlime,
                Spawns[sorteio].position,
                prefabSlime.transform.rotation);
            contadorTempoSpawn = 0;
        }
    }

    public Vector3 GetPlayerProximo(Vector3 posInimigo)
    {
        float menorDistancia = float.MaxValue;
        Vector3 playerProx = Vector3.zero;
        for(int i = 0; i < players.Length; i++)
        {

            if(players[i].gameObject.activeSelf == true)
            {
                float distancia = Vector3.Distance(posInimigo, players[i].transform.position);
                if (menorDistancia > distancia)
                {
                    menorDistancia = distancia;
                    playerProx = players[i].transform.position;
                }
            }
           
        }
        return playerProx;
    }

    void AtualizarCameras()
    {
        float distancia = Vector3.Distance(players[0].position,players[1].position);
        if (distancia >= 20)
        {
            if (MainCamera.activeSelf)
            {
                MainCamera.SetActive(false);
                vcamGroup.SetActive(false);

                CameraP1.SetActive(true);
                CameraP2.SetActive(true);

            }
        }
        else
        {
            if (!MainCamera.activeSelf)
            {
                MainCamera.SetActive(true);
                vcamGroup.SetActive(true);

                CameraP1.SetActive(false);
                CameraP2.SetActive(false);

            }
        }
    }
}
