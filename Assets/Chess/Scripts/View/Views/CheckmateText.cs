using Chess.Domain;
using Chess.Domain.Games;
using TMPro;
using UnityEngine;

namespace Chess.View.Views
{
    public class CheckmateText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _checkmateText;
        [SerializeField] private TMP_Text _winnerText;

        private void Start()
        {
            _checkmateText.gameObject.SetActive(false);
            _winnerText.gameObject.SetActive(false);
        }

        public void Show(PlayerColor winnerColor)
        {
            _checkmateText.gameObject.SetActive(true);
            _winnerText.gameObject.SetActive(true);
            _winnerText.text = $"{winnerColor} player won";
        }
    }
}
