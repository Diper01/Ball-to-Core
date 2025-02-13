using UnityEngine;

public class Half_Y_Strategy : IBallColorStrategy  //ToDo make it work again
{
    public void ChangeColor(Ball ball)
    {
        if (ball.y < 0)
        {
           // ball.ChangeColor(Color.red);
        }
    }
}