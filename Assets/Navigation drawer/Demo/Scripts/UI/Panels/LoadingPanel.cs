using UnityEngine;
using UnityEngine.UI;

public enum ELoading
{
    LoadData,
    UnloadData
}

namespace Loading
{
    public class LoadingPanel : Singleton<LoadingPanel>
    {
        #region FIELDS

        [SerializeField]
        private GameObject _root = default;

        [SerializeField]
        private Text _txtLoading = default;

        [SerializeField]
        private Slider _slider = default;

        private const float SPEED = 0.1f;

        #endregion

        #region PROPERTIES

        public bool Load { get; private set; }

        #endregion

        #region UNITY_METHODS

        private void Update()
        {
            if (!Load) return;

            if (_slider.value < 1.0f)
            {
                _slider.value = _slider.value + Time.deltaTime * SPEED;
            }
            else
            {
                _slider.value = 0.0f;
            }
        }

        #endregion

        #region PUBLIC_METHODS

        public void LoadingStart(ELoading state)
        {
            _root.SetActive(true);
            Load = true;

            switch (state)
            {
                case ELoading.LoadData:
                    _txtLoading.text = "LOADING DATA ...";
                    break;
                case ELoading.UnloadData:
                    _txtLoading.text = "UNLOADING DATA ...";
                    break;
                default:
                    break;
            }
        }

        public void LoadingStop()
        {
            _slider.value = 0.0f;
            Load = false;
            _root.SetActive(false);
        }

        #endregion
    }
}