using UnityEngine;
using Cinemachine;
public class ControleCamera : MonoBehaviour
{
    private bool podeMover;
    private CinemachineBrain cameraBrain;
    void Start()
    {
        cameraBrain = GetComponent<CinemachineBrain>();
    }

    void Update()
    {
        MovimentoMouse();
        CameraPodeMover();
    }

    void CameraPodeMover()
    {
        cameraBrain.enabled = podeMover;
    }
    void MovimentoMouse()
    {
        podeMover = Input.GetMouseButton(1);
    }
    void MovimentoTouch()
    {
        //TODO: MOVIMENTO DO TOUCH: não lembro a sintaxe, vou ter que dar uma estudada para lembrar
    }
}
