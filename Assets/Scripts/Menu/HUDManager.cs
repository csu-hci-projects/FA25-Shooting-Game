using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI playerHealthText;
    [SerializeField] private TextMeshProUGUI enemyHealthText;
    [SerializeField] private TextMeshProUGUI actionsText;

    void Update()
    {
        if (playerHealthText != null)
            playerHealthText.text = $"Player HP: {Admin.Players_health[0]}";

        if (enemyHealthText != null)
            enemyHealthText.text = $"Enemy HP: {Admin.Players_health[1]}";

        if (actionsText != null)
        {
            actionsText.text = $"Actions Left: {Admin.ActionsLeft() / 10}";
        }
    }
}
