using System;
using System.Collections.Generic;
using System.Linq;

namespace PaleoAraripe
{
    public class ReproduzirSomDestruicaoBlocos
    {
        private const int MAXIMO_AUDIOS_POR_INTERACAO = 4;
        public void TocarAudioBlocos(List<BlocoGenerico> blocos, int maximoSons = MAXIMO_AUDIOS_POR_INTERACAO)
        {
            List<SOSonsBlocosAoSeremDestruidos> sons = new List<SOSonsBlocosAoSeremDestruidos>();

            foreach (BlocoGenerico bloco in blocos)
            {
                sons.Add(bloco.SonsBloco);
            }

            if (sons.Count <= maximoSons) {
                ReproduzirSfxs(sons);
                return;
            }

            FiltrarSfxs(sons, maximoSons);
        }

        private void FiltrarSfxs(List<SOSonsBlocosAoSeremDestruidos> sons, int qtdMaximaSons)
        {
            List<SOSonsBlocosAoSeremDestruidos> listaFiltrada = new List<SOSonsBlocosAoSeremDestruidos>();

            // Filtrar por tipo / Prioridade
            var porTipo = sons.GroupBy(a => a.tipoSonoro).ToDictionary(g => g.Key, g => g.OrderByDescending(a => a.prioridade).ToList());
            
            foreach (var grupo in porTipo.Values)
            {
                if (grupo.Count > 0)
                    listaFiltrada.Add(grupo[0]);
            }

            if (listaFiltrada.Count > qtdMaximaSons)
            {
                ReproduzirSfxs(listaFiltrada);
                return;
            }

            // Restando ordenado por prioridade
            var restantes = sons.Except(listaFiltrada).OrderByDescending(a => a.prioridade).ToList();
            foreach (var audio in restantes)
            {
                if (listaFiltrada.Count >= qtdMaximaSons)
                    break;

                listaFiltrada.Add(audio);
            }

            ReproduzirSfxs(listaFiltrada);
        }

        private void ReproduzirSfxs(List<SOSonsBlocosAoSeremDestruidos> sons)
        {
            Random random = new Random();   
            foreach(SOSonsBlocosAoSeremDestruidos som in sons)
            {
                UtilitarioAudio.TocarSFX(som.audios[random.Next(som.audios.Count)], som.volumeBase);
            }
        }

    }
}
