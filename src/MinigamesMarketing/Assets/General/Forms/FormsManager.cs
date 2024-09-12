using Root.General.API;
using System;
using UnityEngine;
using UnityEngine.UI;

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

        private bool authorized = false;
        
        public void OnAuthorizationButtonClicked()
        {
            this.authorized = !this.authorized;
        }

        public void OnSendButtonClicked()
        {
            string auth = this.authorized ? "true" : "false";
            MarketingAPI.Instance.SendData($"{{\"name\": \"{nameInput.text}\",\"email\": \"{emailInput.text}\",\"interest_area\": \"{interestInput.text}\", \"graduation_year\": {Int32.Parse(yearInput.text)}, \"email_authorization\": {auth}}}");
        }
    }
}
