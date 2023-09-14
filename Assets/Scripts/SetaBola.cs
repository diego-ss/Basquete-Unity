using UnityEngine;
using UnityEngine.UI;

public class SetaBola : MonoBehaviour
{
    private Transform alvo;
    public Image seta;

    public static bool alvoInvisivel = false;

    // Start is called before the first frame update
    void Start()
    {
        alvo = GameObject.FindGameObjectWithTag("Bola").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.bolaEmJogo && alvo == null)
            alvo = GameObject.FindGameObjectWithTag("Bola").transform;

        if (alvoInvisivel)
            SeguirBola();
        
        VisualizaSeta();
    }

    void SeguirBola()
    {
        Vector2 aux = seta.rectTransform.position;
        aux.x = alvo.position.x;
        seta.rectTransform.position = aux;
    }

    void VisualizaSeta()
    {
        seta.enabled = alvoInvisivel;
    }
}
