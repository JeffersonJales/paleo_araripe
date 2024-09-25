using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SobrescreverDadosFerramenta))]
public class EditorSobrescreverDadosFerramenta : Editor
{
    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();
        SobrescreverDadosFerramenta componenteAlvo = (SobrescreverDadosFerramenta)target;

        if (componenteAlvo.novaFerramenta != null)
        {

            EditorGUILayout.LabelField("Propriedades da nova ferramenta", EditorStyles.boldLabel);
            GUILayout.Space(5);

            Editor editor = CreateEditor(componenteAlvo.novaFerramenta);
            editor.OnInspectorGUI();

            GUILayout.Space(25);

            if (GUILayout.Button("Aplicar Sobrescrita"))
                componenteAlvo.aplicarSobrescrita();

            GUILayout.Space(10);

            if (GUILayout.Button("Reverter para Original"))
                componenteAlvo.reverterModificacoes();

            GUILayout.Space(10);

            if (GUILayout.Button("Salvar Ferramenta Nova"))
            {
                string path = EditorUtility.SaveFilePanelInProject("Salvar Ferramenta", "NovaFerramenta", "asset", "Escolha onde salvar o ScriptableObject");

                if (!string.IsNullOrEmpty(path))
                    componenteAlvo.salvarFerramentaNova(path);
            }

        }
    }

}
