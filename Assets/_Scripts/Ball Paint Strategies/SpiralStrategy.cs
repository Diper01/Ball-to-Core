using UnityEngine;

public sealed class SpiralStrategy : IBallColorStrategy  //ToDo make it work again
{
    public void ChangeColor(Ball ball)
    {
        int spiralIndex = Mathf.FloorToInt((ball.theta / Mathf.PI + ball.y * 3) * 2) % 4;
        if (spiralIndex == 0 || spiralIndex == 1)
        {
           // ball.ChangeColor(Color.blue);
        }
    }
}