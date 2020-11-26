public interface IInputControlled
{
    InputProvider InputProvider { get; }
    bool Permission { get; }
}