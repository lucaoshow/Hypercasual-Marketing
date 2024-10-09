using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using Root.General.API;
using Root.General.Utils.Scenes;

namespace Root.General.Forms
{
    public class FormsManager : MonoBehaviour
    {
        [SerializeField] private MarketingAPI marketingApi;
        [SerializeField] private InputField nameInput;
        [SerializeField] private InputField emailInput;
        [SerializeField] private InputField interestInput;
        [SerializeField] private InputField yearInput;
        [SerializeField] private GameObject authorizationImage; 
        [SerializeField] private Text errorTextPrefab;
        [SerializeField] private GameInfo gameInfo;

        private readonly int CURRENT_YEAR = DateTime.Now.Year;

        private bool authorized = false;

        private Dictionary<InputField, Text> errors;

        void Start()
        {
            this.errors = new Dictionary<InputField, Text>();
            this.errors.Add(nameInput, null);
            this.errors.Add(emailInput, null);
            this.errors.Add(interestInput, null);
            this.errors.Add(yearInput, null);
        }

        public void OnInputEndEdit(InputField input)
        {
            if (input.text.Trim() == string.Empty)
            {
                this.errors[input] = this.CreateError(input.transform, "*Preencha este campo para enviar");
            }
            else if (input == this.emailInput && !Regex.IsMatch(input.text, "^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$"))
            {
                this.errors[input] = this.CreateError(input.transform, "*Email inválido");
            }
            else if (input == this.yearInput && (input.text.Length != 4 || Int32.Parse(input.text) < this.CURRENT_YEAR))
            {
                this.errors[input] = this.CreateError(input.transform, "*Ano inválido");
            }
        }

        public void OnInputValueChange(InputField input)
        {
            if (this.errors[input] != null)
            {
                Destroy(this.errors[input]);
                this.errors[input] = null;
            }
        }

        public void OnAuthorizationButtonClicked()
        {
            this.authorized = !this.authorized;
            this.authorizationImage.SetActive(this.authorized);
        }

        public void OnSendButtonClicked()
        {
            if (this.errors.Values.Any(t => t != null))
            {
                return;
            }

            this.gameInfo.playerName = nameInput.text;
            this.marketingApi.SendPlayerFormData(nameInput.text, emailInput.text, interestInput.text, Int32.Parse(yearInput.text), authorized);
            SceneHelper.LoadScene("StartScene");
        }

        private Text CreateError(Transform parent, string message)
        {
            Text error = Instantiate(this.errorTextPrefab, parent.position - new Vector3(0, parent.GetComponent<RectTransform>().rect.height, 0), Quaternion.identity, parent);
            error.text = message;
            return error;
        }
    }
}
