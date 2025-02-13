using System.Linq;
using UnityEngine;

public sealed class SpotStrategy : IBallColorStrategy
{
    private SpotManager spotManager;
    public SpotStrategy(SpotManager spotManager)
    {
        this.spotManager = spotManager;
    }

    public void ChangeColor(Ball ball)
    {
        var ballPosition = ball.transform.position;

        float searchRadius = spotManager.spotRadius ; 

        var spotsInRadius = spotManager.GetAllSpots()
            .Where(spot => Vector3.Distance(ballPosition, spot.position) < searchRadius)
            .ToList();
        
        if (spotsInRadius.Count > 0)
        {
            foreach (var spot in spotsInRadius)
            {
                ball.AssignSpot(spot.position);
            }

            ball.ChangeColor(spotsInRadius[0].materialEnum);
        }
    }
    


}