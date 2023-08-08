
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    [SerializeField] private Health health = null;
    [SerializeField] private GameObject healthBarParent = null;
    [SerializeField] private Image healthBarImage = null;

    private void Start()
    {
        health.ClientOnHealthChanged += Health_ClientOnHealthChanged;
    }

    private void OnDestroy()
    {
        health.ClientOnHealthChanged -= Health_ClientOnHealthChanged;
    }

    private void Health_ClientOnHealthChanged(int currentHealth, int maxHealth)
    {
        healthBarImage.fillAmount = (float)currentHealth / maxHealth;
    }

    private void OnMouseEnter()
    {
        healthBarParent.SetActive(true);
    }

    private void OnMouseExit()
    {
        healthBarParent.SetActive(false);
    }
}
