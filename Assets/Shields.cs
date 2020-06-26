using UnityEngine.SceneManagement;

internal class Shields
{
    public int maxHealth = 100;
    public int currentHealth;
    public int damage = 10;

    public void TakeDamage()
    {
        currentHealth = maxHealth - damage;
    }

    public void GainHealth()
    {
        currentHealth = currentHealth + 5;
    }

    public void Death()
    {
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(0);
            currentHealth = 100;
        }
        else
        {
            //Do nothing
        }
    }
}