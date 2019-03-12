using UnityEngine;
using UnityEngine.UI;

namespace Assests.Sources.Store
{
    public abstract class DetailWindowModel : MonoBehaviour
    {
        [SerializeField]
        protected Text _name;

        [SerializeField]
        protected Text _description;

        public void InitializeModel(string name,
            string description)
        {
            _name.text = name;
            _description.text = description;
        }

        private void OnDisable()
        {
            Destroy(gameObject);
        }
    }
}