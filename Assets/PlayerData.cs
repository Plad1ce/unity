using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int PlayerHealth;
    public float[] position;

    public PlayerData(PlayerHealth player)
    {
        PlayerHealth = player.currentHealth;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
