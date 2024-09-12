using UnityEngine;
using UnityEngine.UI;

public class AtualizarValorSlider : MonoBehaviour
{
    private Slider sliderUi;
    public Slider SliderUi => sliderUi;

    [Range(0f, 1f)]
    [SerializeField] public float valorInicial = 0f;

    public void Awake()
    {
        sliderUi = GetComponent<Slider>();    
        sliderUi.value = valorInicial; 
    }

    public void atualizarValorSlider(float valor) {
        sliderUi.value = valor;
    }
    public void atualizarValorSlider(float valorA, float valorB) {
        sliderUi.value = valorA / valorB;
    }
    public void atualizarValorSlider(int valorA, int valorB) {
        sliderUi.value = (float)valorA / valorB;
    }
}
