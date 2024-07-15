using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmailItemScript : MonoBehaviour
{
    public TextMeshProUGUI remetenteText;
    public TextMeshProUGUI conteudoText;
    private EmailManager emailManager;
    private Image background;

    private void Awake()
    {
        background = GetComponent<Image>();
    }

    public void SetEmailDetails(string remetente, string conteudo, EmailManager manager, bool isRead)
    {
        remetenteText.text = remetente;
        conteudoText.text = conteudo;
        emailManager = manager;
        GetComponent<Button>().onClick.AddListener(OnEmailClick);
        UpdateReadStatus(isRead);
    }

    private void OnEmailClick()
    {
        emailManager.DisplayFullEmail(remetenteText.text, conteudoText.text);
    }

    public void UpdateReadStatus(bool isRead)
    {
        if (isRead)
        {
            background.color = Color.green;
        }
        else
        {
            background.color = Color.red;
        }
    }
}



