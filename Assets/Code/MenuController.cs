using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement; // Si tu veux changer de scène

public class MenuController : MonoBehaviour
{
    [Header("Menu Items (Text)")]
    // Tableau de textes (TextMeshProUGUI) représentant les options
    public TextMeshProUGUI[] menuOptions;

    [Header("Highlight Colors")]
    // Couleurs pour l'option sélectionnée et non sélectionnée
    public Color normalColor = Color.white;
    public Color selectedColor = Color.yellow;

    // Indice de l'option actuellement sélectionnée
    private int currentSelection = 0;

    [Header("Joystick Settings")]
    // Seuil de détection pour éviter de bouger à la moindre variation
    public float moveThreshold = 0.5f;
    // Délai minimal entre deux changements de sélection
    public float moveCooldown = 0.3f;
    private bool canMove = true;

    void Start()
    {
        // Mettre en évidence l'option initiale
        UpdateSelection();
    }

    void Update()
    {
        // Vérifie que l'instance ArduinoLink est disponible
        if (ArduinoLink.instance == null) return;

        float vertical = ArduinoLink.instance.joyY;
        bool isPressed = ArduinoLink.instance.joyButton;

        // Navigation verticale : haut/bas
        if (canMove && Mathf.Abs(vertical) > moveThreshold)
        {
            if (vertical > 0)
                currentSelection--;
            else
                currentSelection++;

            // Boucler la sélection si on dépasse le début ou la fin
            if (currentSelection < 0)
                currentSelection = menuOptions.Length - 1;
            else if (currentSelection >= menuOptions.Length)
                currentSelection = 0;

            // Met à jour l'affichage
            UpdateSelection();

            // Lance un cooldown pour éviter de changer de sélection trop vite
            StartCoroutine(MoveCooldown());
        }

        // Vérifie si on appuie sur le bouton du joystick pour sélectionner
        if (isPressed)
        {
            SelectOption(currentSelection);
        }
    }

    IEnumerator MoveCooldown()
    {
        canMove = false;
        yield return new WaitForSeconds(moveCooldown);
        canMove = true;
    }

    // Met à jour les couleurs pour indiquer quelle option est sélectionnée
    void UpdateSelection()
    {
        for (int i = 0; i < menuOptions.Length; i++)
        {
            if (i == currentSelection)
                menuOptions[i].color = selectedColor;
            else
                menuOptions[i].color = normalColor;
        }
    }

    // Exécute l'action liée à l'option sélectionnée
    void SelectOption(int index)
    {
        Debug.Log("Option sélectionnée : " + index);

        switch (index)
        {
            case 0:
                // Par exemple, lancer une scène de jeu
                SceneManager.LoadScene("GameScene");
                break;

            case 1:
                // Ouvrir un autre menu (Options), etc.
                // SceneManager.LoadScene("OptionsScene");
                Debug.Log("Options sélectionnées !");
                break;

            case 2:
                // Quitter le jeu
                Application.Quit();
                Debug.Log("Quitter le jeu");
                break;

            default:
                Debug.LogWarning("Aucune action définie pour cette option !");
                break;
        }
    }
}