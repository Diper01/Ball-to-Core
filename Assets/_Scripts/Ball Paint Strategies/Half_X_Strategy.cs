using UnityEngine;


public class Half_X_Strategy : IBallColorStrategy //ToDo make it work again
{
    public void ChangeColor(Ball ball)
    {
        
        if (ball.x < 0)
        {
           // ball.ChangeColor(Color.red);
        }
    }
}