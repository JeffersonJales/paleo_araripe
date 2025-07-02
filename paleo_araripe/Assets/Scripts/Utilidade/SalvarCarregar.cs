using UnityEngine;

namespace PaleoAraripe {
    /// <summary>
    /// Classe singleton responsável por salvar e carregar o progresso do jogador, incluindo níveis completos e fósseis encontrados.
    /// </summary>
    public class SalvarCarregar : Singleton<SalvarCarregar>
    {
        [SerializeField] private ConfiguracoesSO configuracoes;
        private const int TOTAL_NIVEIS = 5;
        private const int TOTAL_FOSSEIS = 20;
        private const string KEY_NIVEIS = "NiveisCompletos";
        private const string KEY_FOSSEIS = "FosseisEncontrados";

        private void Start()
        {
            Carregar(out bool[] niveis, out bool[] fosseis);
            //PlayerPrefs.DeleteAll();
        }
        // Salva o progresso dos níveis e fósseis
        public void Salvar(bool[] niveisCompletos, bool[] fosseisEncontrados)
        {
            PlayerPrefs.SetString(KEY_NIVEIS, BoolArrayToString(niveisCompletos));
            PlayerPrefs.SetString(KEY_FOSSEIS, BoolArrayToString(fosseisEncontrados));
            PlayerPrefs.Save();
        }

        // Carrega o progresso dos níveis e fósseis
        public void Carregar(out bool[] niveisCompletos, out bool[] fosseisEncontrados)
        {
            niveisCompletos = StringToBoolArray(PlayerPrefs.GetString(KEY_NIVEIS, new string('0', TOTAL_NIVEIS)), TOTAL_NIVEIS);
            fosseisEncontrados = StringToBoolArray(PlayerPrefs.GetString(KEY_FOSSEIS, new string('0', TOTAL_FOSSEIS)), TOTAL_FOSSEIS);
        }

        // Marca um nível como completo e salva
        public void MarcarNivelCompleto(int indiceNivel)
        {
            Carregar(out bool[] niveis, out bool[] fosseis);
            if (indiceNivel >= 0 && indiceNivel < TOTAL_NIVEIS)
            {
                niveis[indiceNivel] = true;
                Salvar(niveis, fosseis);
            }
        }

        // Marca um fóssil como encontrado e salva
        public void MarcarFossilEncontrado(int indiceFossil)
        {
            Carregar(out bool[] niveis, out bool[] fosseis);
            if (indiceFossil >= 0 && indiceFossil < TOTAL_FOSSEIS)
            {
                fosseis[indiceFossil] = true;
                Salvar(niveis, fosseis);
            }
        }

        // Utilitários para converter bool[] para string e vice-versa
        private string BoolArrayToString(bool[] array)
        {
            char[] chars = new char[array.Length];
            for (int i = 0; i < array.Length; i++)
                chars[i] = array[i] ? '1' : '0';
            return new string(chars);
        }

        private bool[] StringToBoolArray(string str, int tamanho)
        {
            bool[] array = new bool[tamanho];
            for (int i = 0; i < tamanho && i < str.Length; i++)
                array[i] = str[i] == '1';
            return array;
        }
    }
}
