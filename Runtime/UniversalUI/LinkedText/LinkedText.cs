using ColdWind.Core.EventLinks;

namespace ColdWind.Core.UniversalUI
{
    public class LinkedText
    {
        private BaseLinkedText _baseLinkedText;
        private IEventLink _eventLink;
        private ILinkedTextProcessor _linkedTextProcessor;

        public LinkedText(
            BaseLinkedText baseLinkedText, 
            IEventLink eventLink, 
            ILinkedTextProcessor linkedTextProcessor)
        {
            _baseLinkedText = baseLinkedText;
            _eventLink = eventLink;
            _linkedTextProcessor = linkedTextProcessor;

            _baseLinkedText.Construct(new LinkPair(
                () => _eventLink.Called += OnEventCalled, 
                () => _eventLink.Called -= OnEventCalled));
        }

        private void OnEventCalled() => _linkedTextProcessor.OnEventCalled(_baseLinkedText);
    }

    public class LinkedText<T>
    {
        private BaseLinkedText _baseLinkedText;
        private IEventLink<T> _eventLink;
        private ILinkedTextProcessor<T> _linkedTextProcessor;

        public LinkedText(
            BaseLinkedText baseLinkedText,
            IEventLink<T> eventLink,
            ILinkedTextProcessor<T> linkedTextProcessor)
        {
            _baseLinkedText = baseLinkedText;
            _eventLink = eventLink;
            _linkedTextProcessor = linkedTextProcessor;

            _baseLinkedText.Construct(new LinkPair(
                () => _eventLink.Called += OnEventCalled,
                () => _eventLink.Called -= OnEventCalled));
        }

        private void OnEventCalled(T value) =>
            _linkedTextProcessor.OnEventCalled(value, _baseLinkedText);
    }
}
