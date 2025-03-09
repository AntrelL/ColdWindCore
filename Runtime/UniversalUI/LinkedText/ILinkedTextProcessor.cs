namespace ColdWind.Core.UniversalUI
{
    public interface ILinkedTextProcessor
    {
        void OnEventCalled(ILinkedTextData linkedTextData);
    }

    public interface ILinkedTextProcessor<T>
    {
        void OnEventCalled(T value, ILinkedTextData linkedTextData);
    }
}
