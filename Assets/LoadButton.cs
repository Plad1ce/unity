using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadButton : MonoBehaviour
{
    public void LoadGame()
    {
        // Загружаем данные игрока
        PlayerData data = SaveSystem.LoadPlayer();

        if (data != null)
        {
            // Сохраняем данные в статические переменные, чтобы перенести их в сцену2
            TempPlayerData.PlayerHealth = data.PlayerHealth;
            TempPlayerData.Position = new Vector3(data.position[0], data.position[1], data.position[2]);

            // Загружаем сцену2
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            Debug.LogWarning("Сохранение не найдено!");
        }
    }
}
