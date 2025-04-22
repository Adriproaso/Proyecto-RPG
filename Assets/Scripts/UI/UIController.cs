using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject uiContainerMenu = null;
    [SerializeField] GameObject uiContainerStats = null;
    [SerializeField] GameObject uiContainerInventory = null;
    [SerializeField] GameObject uiContainerQuests = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiContainerStats.SetActive(false);
        uiContainerInventory.SetActive(false);
        uiContainerQuests.SetActive(false);
        uiContainerMenu.SetActive(false);
    }

    public void ToggleMenu()
    {      
        uiContainerStats.SetActive(false);
        uiContainerQuests.SetActive(false);
        uiContainerInventory.SetActive(false);
        uiContainerMenu.SetActive(!uiContainerMenu.activeSelf);
        uiContainerMenu.GetComponent<MenuController>().CloseMenu();
    }

    public void ToggleInventory()
    {
        uiContainerStats.SetActive(false);
        uiContainerQuests.SetActive(false);
        uiContainerInventory.SetActive(!uiContainerInventory.activeSelf);
        uiContainerMenu.SetActive(false);
    }

    public void ToggleQuests()
    {
        uiContainerStats.SetActive(false);
        uiContainerQuests.SetActive(!uiContainerQuests.activeSelf);
        uiContainerInventory.SetActive(false);
        uiContainerMenu.SetActive(false);
    }

    public void ToggleStats()
    {
        uiContainerStats.SetActive(!uiContainerStats.activeSelf);
        uiContainerQuests.SetActive(false);
        uiContainerInventory.SetActive(false);
        uiContainerMenu.SetActive(false);
    }

    public void ReturnToGame()
    {
        uiContainerStats.SetActive(false);
        uiContainerQuests.SetActive(false);
        uiContainerInventory.SetActive(false);
        uiContainerMenu.SetActive(false);
    }
}
