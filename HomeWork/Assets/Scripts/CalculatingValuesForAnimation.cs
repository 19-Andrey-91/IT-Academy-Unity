public class CalculatingValuesForAnimation
{ 
    public int CalculateAnimationMove(float axis)
    {
        int direction = 0;
        
        if (axis > 0)
        {
            direction = 1;
        }
        else if (axis < 0) 
        {
            direction = -1;
        }
        
        return direction;
    }
}
