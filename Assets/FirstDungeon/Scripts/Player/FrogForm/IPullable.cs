using UnityEngine;

public interface IPullable
{
    Transform PullTransform { get; }


    void Pull(Vector2 frogPosition, float speed, float offset)
    {
        
    }

    public void CancelPulling()
    {
        
    }
}
