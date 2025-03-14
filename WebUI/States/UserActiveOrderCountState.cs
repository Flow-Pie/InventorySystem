namespace WebUI.States
{
    public class UserActiveOrderCountState
    {
        public int ActiveOrderCount { get; private set; }

        public void SetActiveOrderCount(int count)
        {
            ActiveOrderCount = count;
        }
    }
}