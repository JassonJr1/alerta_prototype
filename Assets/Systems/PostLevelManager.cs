using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PostLevelManager : MonoBehaviour
{
    public TextMeshProUGUI postLevelText;
    public Button nextButton;
    public Button startTestButton; // Referência ao botão de iniciar o teste
    
    private Queue<string> postlevelQueue;
    private string[] postLevelMessages = new string[]
    {
        "Agora vamos iniciar o seu treinamento, o qual será muito parecido com essa tarefa de teste. A diferença é que, a partir de agora, você receberá feedbacks para os seus acertos e erros.",
        "Lembre-se de que identificar e-mails de phishing é crucial para manter a segurança da nossa empresa.",
        "Vamos começar o seu treinamento. Agora, valendo pontos!",
        "De início, vamos apresentar o conceito de phishing.",
        "Phishing é uma técnica utilizada por cibercriminosos, para convencer pessoas a fornecerem os seus dados pessoais ou realizarem alguma ação em benefício do atacante.",
        "A principal forma de realizar o phishing consiste em enviar um e-mail que se pareça com o de uma organização ou pessoa legítima, utilizando informações falsas, que possam atrair a atenção do destinatário do e-mail.",
        "Agora que você sabe o que é phishing, podemos começar a te mostrar na prática os e-mails típicos desse tipo de ataque. Atenção aos detalhes.\nBoa sorte!"
    };

    private int currentStep = 0;

private void Start()
    {
        postlevelQueue = new Queue<string>(postLevelMessages);
        DisplayNextInstruction();

        nextButton.onClick.AddListener(DisplayNextInstruction);
        startTestButton.onClick.AddListener(StartTest);
        startTestButton.gameObject.SetActive(false); // Esconde o botão de iniciar o trabalho inicialmente
    }


    private void DisplayNextInstruction()
    {
        if (postlevelQueue.Count > 0)
        {
            postLevelText.text = postlevelQueue.Dequeue();
            currentStep++;
        }
        else
        {
            nextButton.gameObject.SetActive(false);
            startTestButton.gameObject.SetActive(true); // Mostra o botão de iniciar o teste quando as instruções terminarem
        }
    }

    private void StartTest()
    {
        SceneManager.LoadScene("Level 1"); // Substitua "Level 0" pelo nome da sua cena de teste
    }
}

