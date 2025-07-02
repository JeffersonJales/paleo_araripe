using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaleoAraripe
{
    /// <summary>
    /// Classe responsável por configurar o material do cubo de acordo com as preferências de destaque de cor e aresta.
    /// </summary>
    public class ConfiguracaoMaterialCubo : MonoBehaviour
    {
        [SerializeField] private Material normal;
        [SerializeField] private Material cor;
        [SerializeField] private Material aresta;
        [SerializeField] private Material arestaECor;

        void Start ()
        {
            bool arestaDestacada = PlayerPrefs.GetInt("ArestaDestacada")==1;
            bool corDestacada = PlayerPrefs.GetInt("BlocoDestacado")==1;
            if (!arestaDestacada && !corDestacada)
            {
                GetComponent<MeshRenderer>().material = normal;
            }
            else if (!arestaDestacada && corDestacada)
            {
                GetComponent<MeshRenderer>().material = cor;
            }
            else if (arestaDestacada && !corDestacada)
            {
                GetComponent<MeshRenderer>().material = aresta;
            }
            else if (arestaDestacada && corDestacada)
            {
                GetComponent<MeshRenderer>().material = arestaECor;
            }
        }
    }
}
