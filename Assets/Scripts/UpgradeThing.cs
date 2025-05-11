using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeThing : MonoBehaviour
{
    public int upgradeNumber;

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descText;
    public Button selectButton;

    //sprites
    public Sprite common;
    public Sprite rare;
    public Sprite epic;
    public Sprite legendary;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selectButton.interactable = false;
        selectButton.onClick.AddListener(SelectUpgrade);
    }

    void Update()
    {
        if (GameManager.shortTimer >= 1){
            selectButton.interactable = true;
            GameManager.shortTimer = 1;
        }
        else {
            selectButton.interactable = false;
        }
    }

    void LateUpdate()
    {
        if (GameManager.upgradePause){

            //print("option " + upgradeNumber + " should have " + GameManager.options[upgradeNumber].upgradeName);
            titleText.text = GameManager.options[upgradeNumber].upgradeName;
            descText.text = GameManager.options[upgradeNumber].description;

            if (GameManager.options[upgradeNumber].specificCondition.Equals("common")){
                gameObject.GetComponent<Image>().sprite = common;
            }
            else if (GameManager.options[upgradeNumber].specificCondition.Equals("rare")){
                gameObject.GetComponent<Image>().sprite = rare;
            }
            else if (GameManager.options[upgradeNumber].specificCondition.Equals("epic")){
                gameObject.GetComponent<Image>().sprite = epic;
            }
            else {
                gameObject.GetComponent<Image>().sprite = legendary;
            }
        }
    }

    public void SelectUpgrade(){

        GameManager.damageAdd += GameManager.options[upgradeNumber].damageAdd;
        GameManager.damageMult *= GameManager.options[upgradeNumber].damageMult;

        Player.levHealth += GameManager.options[upgradeNumber].healthAdd;
        Player.nicHealth += GameManager.options[upgradeNumber].healthAdd;
        Player.someHealth += GameManager.options[upgradeNumber].healthAdd;
        Player.tiHealth += GameManager.options[upgradeNumber].healthAdd;
        Player.rainHealth += GameManager.options[upgradeNumber].healthAdd;
        Player.remeHealth += GameManager.options[upgradeNumber].healthAdd;

        GameManager.upgradeTimer = GameManager.upgradeTimerBase;
        GameManager.upgradePause = false;

        //special upgrades
        if (GameManager.options[upgradeNumber].specificCondition.Equals("turretspecial")){
            GameManager.turretSpecial = true;
            GameManager.specialList[0].chosen = true;
        }
        else if (GameManager.options[upgradeNumber].specificCondition.Equals("megapunch")){
            GameManager.megaPunch = true;
            GameManager.specialList[1].chosen = true;
        }
        else if (GameManager.options[upgradeNumber].specificCondition.Equals("globalpassive")){
            GameManager.globalPassive = true;
            GameManager.specialList[2].chosen = true;
        }
        else if (GameManager.options[upgradeNumber].specificCondition.Equals("mcsyndrome")){
            GameManager.mcSyndrome = true;
            GameManager.specialList[3].chosen = true;
        }
        else if (GameManager.options[upgradeNumber].specificCondition.Equals("catchtherat")){
            GameManager.catchTheRat = true;
            GameManager.specialList[4].chosen = true;
        }
        else if (GameManager.options[upgradeNumber].specificCondition.Equals("tastetherainbow")){
            GameManager.tasteTheRainbow = true;
            GameManager.specialList[5].chosen = true;
        }
        else if (GameManager.options[upgradeNumber].specificCondition.Equals("bubblehunter")){
            GameManager.bubbleHunter = true;
            GameManager.specialList[6].chosen = true;
        }

    }
}
