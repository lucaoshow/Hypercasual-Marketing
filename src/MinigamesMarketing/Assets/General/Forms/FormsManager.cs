using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using Root.General.API;

namespace Root.General.Forms
{
    public class FormsManager : MonoBehaviour
    {
        [SerializeField]
        private InputField nameInput;
        [SerializeField]
        private InputField emailInput;
        [SerializeField]
        private InputField interestInput;
        [SerializeField]
        private InputField yearInput;
        [SerializeField]
        private Button authorizationButton;
        [SerializeField]
        private Button sendButton;
        [SerializeField]
        private Text errorTextPrefab;

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
                this.errors[input] = this.CreateError(input.transform, "*Email inv�lido");
            }
            else if (input == this.yearInput && (input.text.Length != 4 || Int32.Parse(input.text) < this.CURRENT_YEAR))
            {
                this.errors[input] = this.CreateError(input.transform, "*Ano inv�lido");
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
        }

        public void OnSendButtonClicked()
        {
            if (this.errors.Values.Any(t => t != null))
            {
                return;
            }
            MarketingAPI.Instance.SendPlayerFormData(nameInput.text, emailInput.text, interestInput.text, Int32.Parse(yearInput.text), authorized);
        }

        private Text CreateError(Transform parent, string message)
        {
            Text error = Instantiate(this.errorTextPrefab, parent.position - new Vector3(0, parent.GetComponent<RectTransform>().rect.height, 0), Quaternion.identity, parent);
            error.text = message;
            return error;
        }
    }
}
