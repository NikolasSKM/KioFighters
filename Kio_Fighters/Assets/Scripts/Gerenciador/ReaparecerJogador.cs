using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaparecerJogador : MonoBehaviour
{

    public GameObject[] jogador;
    public List<Transform> charactersTransform;

    // Start is called before the first frame update
    void Awake()
    {
        if (charactersTransform == null)
            charactersTransform = new List<Transform>();

        int index = FindObjectOfType<GerenciadorJogo>().jogadorIndex;
        int indexPlayerDois = FindObjectOfType<GerenciadorJogo>().jogadorDoisIndex;
        //int index = 0;

        GameObject obj = Instantiate(jogador[index], transform.position, transform.rotation);
        charactersTransform.Add(obj.transform);

        obj = Instantiate(jogador[indexPlayerDois], transform.position + new Vector3(18, transform.position.y), transform.rotation);

        Vector3 theScale = obj.transform.localScale;
        theScale.x *= -1;
        obj.transform.localScale = theScale;

        charactersTransform.Add(obj.transform);

        //obj = Instantiate(jogador[2], transform.position, transform.rotation);
        //charactersTransform.Add(obj.transform);

        FindObjectOfType<ControleCamera>().alvos = charactersTransform.ToArray();
    }
}
