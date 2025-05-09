using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeThing : MonoBehaviour
{
    public int upgradeNumber;

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descText;
    public Button selectButton;

    void Awake()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selectButton.onClick.AddListener(SelectUpgrade);
    }

    void LateUpdate()
    {
        if (GameManager.upgradePause){
            //print("option " + upgradeNumber + " should have " + GameManager.options[upgradeNumber].upgradeName);
            titleText.text = GameManager.options[upgradeNumber].upgradeName;
            descText.text = GameManager.options[upgradeNumber].description;
        }
    }

    public void SelectUpgrade(){

        GameManager.damageAdd += GameManager.options[upgradeNumber].damageAdd;
        GameManager.damageMult += GameManager.options[upgradeNumber].damageMult;

        Player.levHealth += GameManager.options[upgradeNumber].healthAdd;
        Player.nicHealth += GameManager.options[upgradeNumber].healthAdd;
        Player.someHealth += GameManager.options[upgradeNumber].healthAdd;
        Player.tiHealth += GameManager.options[upgradeNumber].healthAdd;
        Player.rainHealth += GameManager.options[upgradeNumber].healthAdd;
        Player.remeHealth += GameManager.options[upgradeNumber].healthAdd;

        GameManager.upgradeTimer = 20;
        GameManager.upgradePause = false;

    }
}
