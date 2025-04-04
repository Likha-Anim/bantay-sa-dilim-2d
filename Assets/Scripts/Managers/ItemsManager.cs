using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class ItemsManager : Singleton<ItemsManager>
    {
        private Dictionary<string, string> _items = new Dictionary<string, string>()
        {
            { "Bolo", "(Baka may mga mabangis na hayop kaming makasalubong.)" },
            { "Arnis", "(Baka kailanganin ko ito..)" },
            { "Rosary", "(Sana ligtas at mapayapa ang aming paglakbay.)" },
            { "Santo Nino", "(Pagpalain mo kami sa aming paglalakbay.)" },
            { "Sting", "(Kung sakaling mawalan ako ng enerhiya mamaya, ito na lang.)" },
            { "Yosi", "(Hmmm... Dapat pala hindi ko na ito dinala.)" },
            { "Bawang", "(Siguro naman, wala kaming masasalubong na aswang...)" },
            { "Pizza Pocket", "(Paborito ko to... Baka gusto rin ni Maria.)" },
            { "Ube Halaya", "(Hmmm, sarap... Hatiin ko na lang ito kay Maria pagkatapos niyang magamot.)" },
        };

        public void InitializeItemsOnClick(string section)
        {
            Button[] buttons = Utilities.FindChild(section).GetComponentsInChildren<Button>();
            var nextButton = Utilities.FindChild("Overlay/Dialogue/ItemButton").GetComponent<Button>();

            foreach (var button in buttons)
            {
                ColorBlock colors = button.colors;
                colors.highlightedColor = new Color(1f, 0.84f, 0f, 0.6f);
                button.colors = colors;

                button.onClick.AddListener(() =>
                {
                    nextButton.interactable = false;
                    nextButton.gameObject.SetActive(false);
                    OnItemButtonClicked(button.gameObject);
                });
            }

            nextButton.onClick.AddListener(() =>
            {
                nextButton.interactable = false;
                nextButton.gameObject.SetActive(false);

                Utilities.FindChild("Overlay/Dialogue").gameObject.SetActive(false);
                Utilities.FindChild("Overlay/Choices").gameObject.SetActive(true);
            });
        }

        public void UninitializeItemsOnLeave(string section)
        {
            Button[] buttons = Utilities.FindChild(section).GetComponentsInChildren<Button>();
            foreach (var button in buttons)
            {
                ColorBlock colors = button.colors;
                colors.highlightedColor = Color.white;
                button.colors = colors;

                button.onClick.RemoveAllListeners();
            }
        }

        private void OnItemButtonClicked(GameObject buttonObject)
        {
            StartCoroutine(TypeDialogue(buttonObject, _items[buttonObject.name]));
        }

        public IEnumerator TypeDialogue(GameObject item, string dialogue)
        {
            Debug.Log("Clicked Item: " + item.name);
            Utilities.FindChild("Overlay/Choices").gameObject.SetActive(false);
            Utilities.FindChild("Overlay/Dialogue").gameObject.SetActive(true);

            TypingManager.Instance.InitializeTextDisplay("Dialogue");
            UIManager.Instance.ChangeSpeaker("Ibarra");
            yield return StartCoroutine(TypingManager.Instance.TypeText(dialogue));

            // add code for inventory here

            if (item.name != "Santo Nino") Destroy(item);
            var nextButton = Utilities.FindChild("Overlay/Dialogue/ItemButton").GetComponent<Button>();
            nextButton.interactable = true;
            nextButton.gameObject.SetActive(true);
        }
    }
}