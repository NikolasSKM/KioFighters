using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelecaoJogador : MonoBehaviour
{
    public List<GameObject> jogadores;
    public List<Color> colors;

    private Color corPadrao;
    private int playerUmIndex;
    private int playerDoisIndex;
    private AudioSource audioS;

    void Start()
    {
        playerUmIndex = 0;
        playerDoisIndex = jogadores.Count - 1;

        audioS = GetComponent<AudioSource>();
        corPadrao = new Color(179, 179, 179);

        colors = new List<Color>();
        colors.Add(Color.green);
        colors.Add(Color.red);
        colors.Add(Color.magenta);
        colors.Add(Color.cyan);

    }

    void Update()
    {
        RestaurarEstadoInicial();
        TocarAudioSelecaoJogador();
        TrocarIndiceSelecaoJogador();
        SelecionarJogador(playerUmIndex);
        SelecionarJogador(playerDoisIndex);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            FindObjectOfType<GerenciadorJogo>().jogadorIndex = playerUmIndex;
            FindObjectOfType<GerenciadorJogo>().jogadorDoisIndex = playerDoisIndex;
            SceneManager.LoadScene("Cena1");
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void TrocarIndiceSelecaoJogador()
    {

        int ultimoIndice = (jogadores.Count - 1);
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (--playerUmIndex < 0)
                playerUmIndex = ultimoIndice;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (++playerUmIndex > ultimoIndice)
                playerUmIndex = 0;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (--playerDoisIndex < 0)
                playerDoisIndex = ultimoIndice;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (++playerDoisIndex > ultimoIndice)
                playerDoisIndex = 0;
        }
    }

    void TocarAudioSelecaoJogador()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)
            || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            AudioJogador();
        }
    }

    void SelecionarJogador(int playerIndex)
    {

        if (jogadores == null)
            return;

        GameObject character = jogadores[playerIndex];
        Image img = character.GetComponentInChildren<Image>();
        Animator anim = character.GetComponentInChildren<Animator>();

        img.color = this.colors[playerIndex];
        anim.SetBool("jogadorSelecionado", true);

    }

    void RestaurarEstadoInicial()
    {
        if (jogadores == null)
            return;

        foreach (GameObject character in jogadores)
        {
            Image img = character.GetComponentInChildren<Image>();
            Animator anim = character.GetComponentInChildren<Animator>();

            img.color = corPadrao;
            anim.SetBool("jogadorSelecionado", false);
            Debug.Log("vixci");
        }
    }

    void AudioJogador()
    {
        if (!audioS.isPlaying)
        {
            audioS.Play();
        }
    }
}
