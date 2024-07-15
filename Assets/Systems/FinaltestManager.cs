using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class FinaltestManager : MonoBehaviour
{
    public TextMeshProUGUI finalTestText;
    public Button nextButton;
    public Button startTestButton; // Referência ao botão de iniciar o teste final

    private Queue<string> finaltestQueue;
    private string[] finalTestMessages = new string[]
    {
        "Ótimo!\nVocê concluiu com sucesso o treinamento.",
        "Agora iremos para o teste final, ele irá nos dizer se você está preparado para trabalhar conosco.",
        "Lembrando que você deverá obter nota acima de 100 pontos para ser contratado(a).",
        "Preparado?\nBoa sorte!"
    };

    private int currentStep = 0;

     private void Start()
    {
        finaltestQueue = new Queue<string>(finalTestMessages);
        DisplayNextInstruction();

        nextButton.onClick.AddListener(DisplayNextInstruction);
        startTestButton.onClick.AddListener(StartFinalTest);
        startTestButton.gameObject.SetActive(false); // Esconde o botão de iniciar o trabalho inicialmente
    }


    private void DisplayNextInstruction()
    {
        if (finaltestQueue.Count > 0)
        {
            finalTestText.text = finaltestQueue.Dequeue();
            currentStep++;
        }
        else
        {
            nextButton.gameObject.SetActive(false);
            startTestButton.gameObject.SetActive(true); // Mostra o botão de iniciar o teste quando as instruções terminarem
        }
    }



    // Método para iniciar o teste final
    public void StartFinalTest()
    {
        SceneManager.LoadScene("Level 5"); // Substitua "FinalTestScene" pelo nome da sua cena de teste final
    }
}
