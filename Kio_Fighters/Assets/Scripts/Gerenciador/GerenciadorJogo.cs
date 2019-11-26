using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorJogo : MonoBehaviour
{
    public int jogadorIndex;
    public int jogadorDoisIndex;
    private GerenciadorJogo gerenciadorJogo;

    // Start is called before the first frame update
    void Awake()
    {
        if (gerenciadorJogo == null)
        {
            gerenciadorJogo = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
