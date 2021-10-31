using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    public GameObject uiCanvas;
    public Button inventory;
    public Button attributes;
    public GameObject dice;
    public GameObject inventoryPanel;
    public GameObject attributesPanel;
    public SpriteRenderer diceRenderer;
    public Button diceButton;
    public GameObject shopCanvas;

    private bool inventoryActive = false;
    private bool attributesActive = false;

    // On start, assign GameObjects and components to variables
    private void Start()
    {
        inventory = uiCanvas.transform.Find("Inventory Button").GetComponent<Button>();
        attributes = uiCanvas.transform.Find("Attributes Button").GetComponent<Button>();

        dice = uiCanvas.transform.Find("Dice Button").gameObject.transform.GetChild(2).gameObject;
        diceRenderer = dice.GetComponent<SpriteRenderer>();
        inventoryPanel = uiCanvas.transform.Find("Inventory Panel").gameObject;
        attributesPanel = uiCanvas.transform.Find("Attributes Panel").gameObject;
        diceButton = GameObject.Find("Dice Button").GetComponent<Button>();

        inventoryPanel.SetActive(false);
        attributesPanel.SetActive(false);

        inventory.onClick.AddListener(inventoryClicked);
        attributes.onClick.AddListener(attributesClicked);
        diceButton.onClick.AddListener(GameObject.Find("AI Controller").GetComponent<AiController>().RollForPlayer);

        shopCanvas = GameObject.Find("Shop Canvas");
        shopCanvas.GetComponent<Canvas>().enabled = false;
    }

    // Methods to Show and Hide the various panels and UI elemnts.
    public void inventoryClicked()
    {
        inventoryActive = !inventoryActive;
        attributesActive = false;
        inventoryPanel.SetActive(inventoryActive);
        attributesPanel.SetActive(false);
    }

    private void attributesClicked()
    {
        attributesActive = !attributesActive;
        inventoryActive = false;
        inventoryPanel.SetActive(inventoryActive);
        attributesPanel.SetActive(attributesActive);
    }

    public void diceShow()
    {
        diceRenderer.enabled = true;
    }

    public void diceHide()
    {
        diceRenderer.enabled = false;
    }
}