public interface IGameEventListener<in TArg>
{
    public void OnEventRaised(TArg argument);    
}