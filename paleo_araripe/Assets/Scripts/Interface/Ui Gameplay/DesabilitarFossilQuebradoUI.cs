using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaleoAraripe
{
    public class DesabilitarFossilQuebradoUI : MonoBehaviour
    {
        public void DisableUIFeedback()
        {
            gameObject.SetActive(false);
        }
    }
}
