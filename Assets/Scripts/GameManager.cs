using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public bool bolaEmJogo = false;
    public int numeroDeBolas;
    public GameObject bolaPrefab;
    private Transform limiteSuperior, limiteInferior, limiteEsquerda, limiteDireita;
    private bool jogoExecutando = true, win = false, lose = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(gameObject);

        SceneManager.sceneLoaded += Carregar;
    }

    void Carregar(Scene cena, LoadSceneMode modo)
    {
        limiteSuperior = GameObject.FindGameObjectWithTag("LimiteSuperior").transform;
        limiteInferior = GameObject.FindGameObjectWithTag("LimiteInferior").transform;
        limiteEsquerda = GameObject.FindGameObjectWithTag("LimiteEsquerda").transform;
        limiteDireita   = GameObject.FindGameObjectWithTag("LimiteDireita").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        bolaEmJogo = true;
        numeroDeBolas = 4;
    }

    public void GerarBola()
    {
        Instantiate(bolaPrefab, 
            new Vector2(
                Random.Range(limiteEsquerda.position.x, limiteDireita.position.x),
                Random.Range(limiteSuperior.position.x, limiteInferior.position.y)), 
            Quaternion.identity);

        bolaEmJogo = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
