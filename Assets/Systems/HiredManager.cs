using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class HiredManager : MonoBehaviour
{
    public TextMeshProUGUI hiredText;
    public Button nextButton;
    public Button startWorkButton;
    
    private Queue<string> HiredQueue;
    private string[] hiredMessages = new string[]
    {
        "Parabéns!\nVocê foi aprovado em nosso processo seletivo.",
        "Agora você faz parte da equipe da JR Company.",
        "Entraremos em contato para continuarmos com sua contratação.\nSeja bem-vindo ao time!"
    };

    private int currentStep = 0;

    private void Start()
    {
        HiredQueue = new Queue<string>(hiredMessages);
        DisplayNextInstruction();

        nextButton.onClick.AddListener(DisplayNextInstruction);
        startWorkButton.onClick.AddListener(StartWork);
        startWorkButton.gameObject.SetActive(false); // Esconde o botão de iniciar o trabalho inicialmente
    }

    private void DisplayNextInstruction()
    {
        if (HiredQueue.Count > 0)
        {
            hiredText.text = HiredQueue.Dequeue();
            currentStep++;
        }
        else
        {
            nextButton.gameObject.SetActive(false);
            startWorkButton.gameObject.SetActive(true); // Mostra o botão de iniciar o teste quando as instruções terminarem
            Debug.Log("Todas as mensagens de contratação foram exibidas.");
        }
    }

    // Método para iniciar o trabalho
    private void StartWork()
    {
        SceneManager.LoadScene("MainMenu"); // Substitua "WorkScene" pelo nome da sua cena de trabalho
    }
}

