using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PhaseConfig", menuName = "Game/PhaseConfig")]
public class PhaseConfig : ScriptableObject
{
    [System.Serializable]
    public class EmailData
    {
        public string Remetente;

        [TextArea(3, 10)]
        public string Conteudo;
        public bool IsPhishing;
    }

    public List<EmailData> emails;
    public int requiredScoreToAdvance; // Pontuação necessária para avançar
}

