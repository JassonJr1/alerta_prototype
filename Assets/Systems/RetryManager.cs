using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class RetryManager : MonoBehaviour
{
    public TextMeshProUGUI retryText;
    public Button nextButton;
    public Button retryButton; // Referência ao botão de tentar novamente
    
    private Queue<string> retryQueue;
    private string[] retryMessages = new string[]
    {
        "Infelizmente, você não atingiu a pontuação necessária para ser contratado.",
        "Mas não desanime! Você pode tentar novamente depois.",
        "Estaremos de portas abertas para receber você novamente."
    };

    private int currentStep = 0;

    private void Start()
    {
        retryQueue = new Queue<string>(retryMessages);
        DisplayNextInstruction();

        nextButton.onClick.AddListener(DisplayNextInstruction);
        retryButton.onClick.AddListener(Retry);
        retryButton.gameObject.SetActive(false); // Esconde o botão de iniciar o trabalho inicialmente
    }

private void DisplayNextInstruction()
    {
        if (retryQueue.Count > 0)
        {
            retryText.text = retryQueue.Dequeue();
            currentStep++;
        }
        else
        {
            nextButton.gameObject.SetActive(false);
            retryButton.gameObject.SetActive(true); // Mostra o botão de iniciar o teste quando as instruções terminarem
            Debug.Log("Todas as mensagens de tentar novamente foram exibidas.");
        }
    }

    // Método para tentar novamente
    public void Retry()
    {
        SceneManager.LoadScene("MainMenu"); // Substitua "TrainingScene" pelo nome da sua cena de treinamento ou teste
    }
}
