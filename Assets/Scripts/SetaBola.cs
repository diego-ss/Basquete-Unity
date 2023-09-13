using UnityEngine;
using UnityEngine.UI;

public class SetaBola : MonoBehaviour
{
    public Transform alvo;
    public Image seta;

    public static bool alvoInvisivel = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!alvoInvisivel)
            SeguirBola();
        
        VisualizaSeta();
    }

    void SeguirBola()
    {
        if (!alvo) return;

        Vector2 aux = seta.rectTransform.position;
        aux.x = alvo.position.x;
        seta.rectTransform.position = aux;
    }

    void VisualizaSeta()
    {
        seta.enabled = alvoInvisivel;
    }
}
