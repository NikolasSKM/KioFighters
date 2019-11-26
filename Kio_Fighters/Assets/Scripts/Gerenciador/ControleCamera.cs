using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleCamera : MonoBehaviour
{
    public float tempoFoco = 0.2f;              // Tempo aproximado para a câmera focar novamente.
    public float espacoBordaTela = 2f;          // Espaço entre o alvo mais superior / inferior e a borda da tela.
    public float tamanhoMin = 6.5f;             // O menor tamanho ortográfico da câmera.
    /*[HideInInspector]*/ public Transform[] alvos; // Todos os alvos que a camera precisa incluir.

    private Camera camera;                      // Usado para referenciar a camera
    private float velocidadeZoom;               // Velocidade de referência para o amortecimento suave do tamanho ortográfico da camera.
    private Vector3 velocidadeMovimento;        // Velocidade de referência para o amortecimento suave da posição.
    private Vector3 posicaoDesejada;            // A posição em que a câmera está se movendo.

    // Start is called before the first frame update
    void Awake()
    {
        camera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move(); // Move a camera para a posição desejada.
        Zoom(); // Altera o tamanho base da camera.
        
    }

    private void Move()
    {
        EncontrePosMedia(); // Encontre a posição média dos alvos.

        transform.position = Vector3.SmoothDamp(transform.position, posicaoDesejada, ref velocidadeMovimento, tempoFoco); //Transição suave para essa posição.
    }

    private void EncontrePosMedia()
    {
        Vector3 posicaoMedia = new Vector3();
        int numAlvos = 0;

        for (int i = 0; i < alvos.Length; i++)  // Atravesse todos os alvos e adicione suas posições.
        {
            if (alvos[i] == null || !alvos[i].gameObject.activeSelf)    // Se o alvo não estiver ativo, prossiga para o próximo.
                continue;

            posicaoMedia += alvos[i].position; // Adicione à média e aumente o número de destinos na média.
            numAlvos++;
        }

        if (numAlvos > 0)   // Se houver metas, divida a soma das posições pelo número delas para encontrar a média.
            posicaoMedia /= numAlvos;

        posicaoMedia.z = transform.position.z;  // Mantenha o mesmo valor y.

        posicaoDesejada = posicaoMedia; // A posição desejada é a posição média;
    }

    private void Zoom()
    {
        float tamRequerido = EncontreTamRequerido();    // Encontre o tamanho necessário com base na posição desejada e faça a transição suave para esse tamanho.

        if (tamRequerido > 6)
            tamRequerido = 6;

        camera.orthographicSize = Mathf.SmoothDamp(camera.orthographicSize, tamRequerido, ref velocidadeZoom, tempoFoco);
    }

    private float EncontreTamRequerido()
    {
        Vector3 posLocalDesejado = transform.InverseTransformPoint(posicaoDesejada); // Encontre a posição em que o equipamento da câmera está se movendo no espaço local.

        float tamanho = 0f; // Inicie o cálculo do tamanho da câmera em zero.

        for (int i = 0; i < alvos.Length; i++)  // Passa por todos os alvos ...
        {
            if (alvos[i] == null || !alvos[i].gameObject.activeSelf)
                continue;

            Vector3 posLocalAlvo = transform.InverseTransformPoint(alvos[i].position);  // Caso contrário, encontre a posição do alvo no espaço local da câmera.

            Vector3 posDesejadaAlvo = posLocalAlvo - posLocalDesejado;  // Encontre a posição do alvo a partir da posição desejada no espaço local da câmera.

            tamanho = Mathf.Max(tamanho, Mathf.Abs(posDesejadaAlvo.y)); // Escolha o maior entre o tamanho atual e a distância do tanque 'para cima' ou 'para baixo' da câmera.

            tamanho = Mathf.Max(tamanho, Mathf.Abs(posDesejadaAlvo.x) / camera.aspect); // Escolha o maior entre o tamanho atual e o tamanho calculado com base no tanque à esquerda ou à direita da câmera.
        }

        tamanho += espacoBordaTela; // Adicione o buffer de borda ao tamanho.

        tamanho = Mathf.Max(tamanho, tamanhoMin);   // Verifique se o tamanho da câmera não está abaixo do mínimo.

        return tamanho;
    }

    public void DefinirTamanhoPosicaoInicial()
    {
        EncontrePosMedia(); // Encontre a posição desejada.

        transform.position = posicaoDesejada;   // Defina a posição da câmera para a posição desejada sem amortecer.

        camera.orthographicSize = EncontreTamRequerido();   // Encontre e defina o tamanho necessário da câmera.
    }
}
