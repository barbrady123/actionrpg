public static class Extensions
{
    public static float Normalize(this float val)
    {
        if (val < 0f)
        {
            return -1f;
        }
        else if (val > 0f)
        {
            return 1f;
        }

        return 0;
    }
}
