using UnityEngine;
using UnityEngine.UI;

public class Mine : MonoBehaviour
{
    public string Genus1;
    public string NumberOfGenus1;
    public string Genus2;
    public string NumberOfGenus2;
    public string Genus3;
    public string NumberOfGenus3;
    public GameObject InfoWindow;
    public GameObject MineWindow;
    public Text Type1;
    public Text Type2;
    public Text Type3;
    public Text Number1;
    public Text Number2;
    public Text Number3;
    public Button AddButton;
    public Text TextInfo;
    bool set_active;
    string output;
    bool isShowInfo;
    bool inCollider;
    // Start is called before the first frame update
    void Start()
    {
        MineWindow.SetActive(false);
        set_active = false;
        isShowInfo = false;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        inCollider = true;
    }
    private void but1()
    {
        if (Inventory.inventory.ContainsKey(Genus1) == false)
        {
            Inventory.inventory.Add(Genus1, int.Parse(NumberOfGenus1));
        }
        else
        {
            Inventory.inventory[Genus1] += int.Parse(NumberOfGenus1);
        }
        NumberOfGenus1 = "0";
        if (Inventory.inventory.ContainsKey(Genus2) == false)
        {
            Inventory.inventory.Add(Genus2, int.Parse(NumberOfGenus2));
        }
        else
        {
            Inventory.inventory[Genus2] += int.Parse(NumberOfGenus2);
        }
        NumberOfGenus2 = "0";
        if (Inventory.inventory.ContainsKey(Genus3) == false)
        {
            Inventory.inventory.Add(Genus3, int.Parse(NumberOfGenus3));
        }
        else
        {
            Inventory.inventory[Genus3] += int.Parse(NumberOfGenus3);
        }
        NumberOfGenus3 = "0";
        AddButton.onClick.RemoveListener(but1);
        output = "";
        foreach (var item in Inventory.inventory)
        {
            output += ($"{item.Key}: {item.Value} ");
        }
        TextInfo.text = output;
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        MineWindow.SetActive(false);
        set_active = false;
        AddButton.onClick.RemoveListener(but1);
        inCollider = false;
        isShowInfo = false;

    }


    // Update is called once per frame
    void Update()
    {
        if (inCollider == true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (isShowInfo == false)
<<<<<<< Updated upstream
                {
=======
                {
>>>>>>> Stashed changes
                    InfoWindow.SetActive(false);
                    MineWindow.SetActive(true);
                    isShowInfo = true;
                    set_active = true;
                    Type1.text = Genus1;
                    Type2.text = Genus2;
                    Type3.text = Genus3;
                    Number1.text = NumberOfGenus1;
                    Number2.text = NumberOfGenus2;
                    Number3.text = NumberOfGenus3;
                    AddButton.onClick.AddListener(but1);


                }
                else
                {
                    MineWindow.SetActive(false);
                    isShowInfo = false;

                }
            }
        }
        if (set_active == true)
        {
            Type1.text = Genus1;
            Type2.text = Genus2;
            Type3.text = Genus3;
            Number1.text = NumberOfGenus1;
            Number2.text = NumberOfGenus2;
            Number3.text = NumberOfGenus3;
        }
    }
}