using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class InstructionsManager : MonoBehaviour
{
    public TextMeshProUGUI instructionsText;
    public Button nextButton;
    public Button startTestButton; // Botão para iniciar o teste

    // Lista de imagens para cada passo
    public RawImage[] instructionImages;

    private Queue<string> instructionQueue;
    private string[] instructions = new string[]
    {
        "Olá, eu sou o Matheus, gerente do setor de RH da empresa JR Company, que atua no segmento de criação de sistemas. Bem-vindo ao nosso processo seletivo para assistente de segurança!",
        "Para você ser contratado(a), deverá obter nota acima de 100 pontos em uma avaliação da sua capacidade de reconhecer e-mails com tentativas de phishing, algo que explicarei melhor mais adiante.",
        "Como sabemos que muitos ainda não conhecem como reconhecer esse tipo de e-mail, forneceremos um breve treinamento para você. Só depois você será avaliado em definitivo.",
        "Vamos entender agora a sua tarefa.",
        "1. Examine cada e-mail recebido na sua caixa de entrada.",
        "2. Use os botões para marcar se o e-mail é phishing ou legítimo. No treino, você terá 2 tentativas para acertar a classificação correta do e-mail.",
        "3. Para ajudá-lo a entender o contexto relacionado a cada e-mail, você terá acesso aos cartões de informações dos donos dos e-mails. Use esses dados para lhe auxiliar.",
        "4. Você ganhará pontos ao classificar corretamente os e-mails não ganhará nada se errar.",
        "Antes de iniciarmos o seu treinamento, queremos identificar o que você já conhece sobre o assunto. Portanto, vamos começar com um teste. Ele não valerá pontos."
    };

    private int currentStep = 0;

    private void Start()
    {
        instructionQueue = new Queue<string>(instructions);
        DisplayNextInstruction();

        nextButton.onClick.AddListener(DisplayNextInstruction);
        startTestButton.onClick.AddListener(StartTest); // Adiciona o listener para o botão de iniciar o teste
        startTestButton.gameObject.SetActive(false); // Esconde o botão de iniciar o teste inicialmente
    }

    private void DisplayNextInstruction()
    {
        if (instructionQueue.Count > 0)
        {
            instructionsText.text = instructionQueue.Dequeue();
            UpdateInstructionImage(currentStep);
            currentStep++;
        }
        else
        {
            nextButton.gameObject.SetActive(false);
            startTestButton.gameObject.SetActive(true); // Mostra o botão de iniciar o teste quando as instruções terminarem
            instructionsText.text = "Essas foram as instruções. Agora clique em começar.";
            
        }
    }

    private void UpdateInstructionImage(int step)
    {
        HideAllInstructionImages();
        if (step < instructionImages.Length)
        {
            instructionImages[step].gameObject.SetActive(true);
        }
    }

    private void HideAllInstructionImages()
    {
        foreach (var image in instructionImages)
        {
            image.gameObject.SetActive(false);
        }
    }

    private void StartTest()
    {
        SceneManager.LoadScene("Level 0"); // Substitua "TestScene" pelo nome da sua cena de teste
    }
}






