using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;

public class EmailManager : MonoBehaviour
{
    public PhaseConfig currentPhaseConfig;

    [System.Serializable]
    public class Email
    {
        public string Remetente;
        public string Conteudo;
        public bool IsPhishing;
        public bool IsMarked;
        public bool IsMarkedCorrectly;
        public int attempts;
        public bool IsPhishingMarked;
        public bool IsRead;
    }

    public GameObject emailPrefab;
    public Transform emailListContainer;
    public GameObject fullEmailPanel;
    public TextMeshProUGUI fullEmailRemetente;
    public TextMeshProUGUI fullEmailConteudo;
    public Button phishingButton;
    public Button legitimateButton;
    public TextMeshProUGUI scoreText;
    public Button finishPhaseButton;
    public GameObject endPhasePanel;
    public TextMeshProUGUI endPhaseScoreText;
    public TextMeshProUGUI retryMessageText;
    public Button nextPhaseButton;

    private List<Email> emails = new List<Email>();
    private Email currentEmail;
    private int score = 0;
    private Color originalPhishingColor;
    private Color originalLegitimateColor;
    private List<string> consequences = new List<string>();

    [Header("Popup Settings")]
    public GameObject popupPanel;
    public TextMeshProUGUI popupText;
    public Button closeButton;
    public bool showCloseButton = true;
    public float autoCloseDelay = 5f;

    [Header("Scenes")]
    
    public string hiredScene = "HiredScene";
    public string retryScene = "RetryScene";

    void Start()
    {
        LoadPhaseData();
        DisplayEmails();
        fullEmailPanel.SetActive(false);
        endPhasePanel.SetActive(false);
        popupPanel.SetActive(false);

        phishingButton.onClick.AddListener(MarkAsPhishing);
        legitimateButton.onClick.AddListener(MarkAsLegitimate);
        finishPhaseButton.onClick.AddListener(FinishPhase);
        nextPhaseButton.onClick.AddListener(NextPhase);
        nextPhaseButton.interactable = false;

        originalPhishingColor = phishingButton.GetComponent<Image>().color;
        originalLegitimateColor = legitimateButton.GetComponent<Image>().color;

        closeButton.onClick.AddListener(ClosePopup);

        UpdateScoreText();
    }

    private void LoadPhaseData()
    {
        foreach (var emailData in currentPhaseConfig.emails)
        {
            emails.Add(new Email
            {
                Remetente = emailData.Remetente,
                Conteudo = emailData.Conteudo,
                IsPhishing = emailData.IsPhishing,
                IsMarked = false,
                attempts = 2,
                IsRead = false
            });
        }
    }

    private void DisplayEmails()
    {
        foreach (Email email in emails)
        {
            GameObject emailItem = Instantiate(emailPrefab, emailListContainer);
            EmailItemScript emailScript = emailItem.GetComponent<EmailItemScript>();
            emailScript.SetEmailDetails(email.Remetente, email.Conteudo, this, email.IsRead);
        }
    }

    public void DisplayFullEmail(string remetente, string conteudo)
    {
        fullEmailRemetente.text = remetente;
        fullEmailConteudo.text = conteudo;
        fullEmailPanel.SetActive(true);

        currentEmail = emails.Find(e => e.Remetente == remetente && e.Conteudo == conteudo);
        currentEmail.IsRead = true;

        UpdateEmailItemUI(currentEmail);

        phishingButton.GetComponent<Image>().color = originalPhishingColor;
        legitimateButton.GetComponent<Image>().color = originalLegitimateColor;

        if (currentEmail.IsMarked)
        {
            if (currentEmail.IsPhishingMarked)
            {
                HighlightButton(phishingButton);
            }
            else
            {
                HighlightButton(legitimateButton);
            }
        }
    }

    private void UpdateEmailItemUI(Email email)
    {
        var emailItemScripts = emailListContainer.GetComponentsInChildren<EmailItemScript>();
        foreach (var emailItemScript in emailItemScripts)
        {
            if (emailItemScript.remetenteText.text == email.Remetente && emailItemScript.conteudoText.text == email.Conteudo)
            {
                emailItemScript.UpdateReadStatus(email.IsRead);
                break;
            }
        }
    }

    private void MarkAsPhishing()
    {
        if (currentEmail != null)
        {
            if (currentEmail.attempts > 0)
            {
                currentEmail.attempts--;
                currentEmail.IsMarked = true;
                currentEmail.IsPhishingMarked = true;

                if (currentEmail.IsPhishing)
                {
                    score += 10;
                    currentEmail.IsMarkedCorrectly = true;
                    ShowPopup("Você acertou! Este e-mail é um phishing.", true);
                }
                else
                {
                    score += 0;
                    currentEmail.IsMarkedCorrectly = false;
                    ShowPopup("Você errou! Este e-mail não é um phishing.", false);
                }
                UpdateScoreText();
                HighlightButton(phishingButton);
            }
            else
            {
                Debug.Log("Tentativas esgotadas para este email.");
            }
        }
    }

    private void MarkAsLegitimate()
    {
        if (currentEmail != null)
        {
            if (currentEmail.attempts > 0)
            {
                currentEmail.attempts--;
                currentEmail.IsMarked = true;
                currentEmail.IsPhishingMarked = false;

                if (!currentEmail.IsPhishing)
                {
                    score += 10;
                    currentEmail.IsMarkedCorrectly = true;
                    ShowPopup("Você acertou! Este e-mail é legítimo.", true);
                }
                else
                {
                    score += 0;
                    currentEmail.IsMarkedCorrectly = false;
                    ShowPopup("Você errou! Este e-mail é um phishing.", false);
                }
                UpdateScoreText();
                HighlightButton(legitimateButton);
            }
            else
            {
                Debug.Log("Tentativas esgotadas para este email.");
            }
        }
    }

    private void HighlightButton(Button button)
    {
        phishingButton.GetComponent<Image>().color = originalPhishingColor;
        legitimateButton.GetComponent<Image>().color = originalLegitimateColor;

        button.GetComponent<Image>().color = Color.yellow;
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Pontuação: " + score;
    }

    private void FinishPhase()
    {
        endPhaseScoreText.text = "Pontuação Final: " + score;
        

        int correctEmailCount = emails.Count(email => email.IsMarkedCorrectly);
        
        // Check if it's the final phase (Level 5)
        if (SceneManager.GetActiveScene().name == "Level 5")
    {
        if (score >= 100)
        {
            Invoke("LoadHiredScene", 5f); // Carrega a cena de contratação após 2 segundos
        }
        else
        {
            Invoke("LoadRetryScene", 5f); // Carrega a cena de tentar novamente após 2 segundos
        }
    }
        else
        {
            if (correctEmailCount >= 2)
            {
                nextPhaseButton.interactable = true;
                retryMessageText.gameObject.SetActive(false);
            }
            else
            {
                retryMessageText.gameObject.SetActive(true);
                retryMessageText.text = "Você não acertou pelo menos dois e-mails. Por favor, tente novamente.";
                nextPhaseButton.interactable = false;
                Invoke("RetryPhase", 5f);
            }
        }

        endPhasePanel.SetActive(true);
        SaveResultsToFile();
    }

    private void LoadHiredScene()
{
    SceneManager.LoadScene(hiredScene); // Carrega a cena de contratação
}

private void LoadRetryScene()
{
    SceneManager.LoadScene(retryScene); // Carrega a cena de tentar novamente
}

    private void RetryPhase()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void NextPhase()
    {
        int nextPhaseIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextPhaseIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextPhaseIndex);
        }
        else
        {
            Debug.Log("Última fase completada!");
        }
    }

    private void SaveResultsToFile()
    {
        string filePath = Application.dataPath + "/EmailResults.txt";
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine("Resultados da Fase:");
            foreach (var email in emails)
            {
                writer.WriteLine("Remetente: " + email.Remetente);
                writer.WriteLine("Conteúdo: " + email.Conteudo);
                writer.WriteLine("É Phishing: " + email.IsPhishing);
                writer.WriteLine("Marcado como Phishing: " + email.IsPhishingMarked);
                writer.WriteLine("Correto: " + email.IsMarkedCorrectly);
                writer.WriteLine("Tentativas restantes: " + email.attempts);
                writer.WriteLine("Lido: " + email.IsRead);
                writer.WriteLine();
            }
            writer.WriteLine("Pontuação Final: " + score);
            writer.WriteLine("=====================================");
        }

        Debug.Log("Resultados salvos em " + filePath);
    }

    private void ShowPopup(string message, bool showCloseButton)
    {
        popupText.text = message;
        popupPanel.SetActive(true);

        closeButton.gameObject.SetActive(true);

        if (!showCloseButton)
        {
            Invoke("ClosePopup", autoCloseDelay);
        }
    }

    private void ClosePopup()
    {
        popupPanel.SetActive(false);
    }
}









