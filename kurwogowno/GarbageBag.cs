namespace kurwogowno;

public static class GarbageBag
{
    public static long CurrentTime() {
        return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
    }
}