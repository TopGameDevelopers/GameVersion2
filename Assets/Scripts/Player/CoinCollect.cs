using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class CoinCollect : MonoBehaviour
    {
        public static int CoinCount;
        private Text _coinCounter;

        private void Start()
        {
            _coinCounter = GetComponent<Text>();
            CoinCount = 0;
        }

        private void Update()
        {
            _coinCounter.text = $"✘{CoinCount}";
        }
    }
}