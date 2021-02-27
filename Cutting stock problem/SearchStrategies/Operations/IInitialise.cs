namespace SearchStrategies.Operations
{
    public interface IInitialise<S,P>
    {
        S Initialise(P problem);
    }
}
