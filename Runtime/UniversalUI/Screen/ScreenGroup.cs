using ColdWind.Core.AdvancedDebugModule;
using ColdWind.Core.ModularCompositeRoot;
using System.Collections.Generic;

namespace ColdWind.Core.UniversalUI
{
    public class ScreenGroup : MonoScript<bool, Screen[]>
    {
        private List<Screen> _screens;

        protected override void Constructor(bool displayed, params Screen[] screens)
        {
            _screens = new List<Screen>(screens);
            SetScreensState(displayed, true);
        }

        public bool Displayed { get; private set; }

        public IReadOnlyList<IReadOnlyScreen> Screens => _screens;

        public void Show(bool force = false) => SetScreensState(true, force);

        public void Hide(bool force = false) => SetScreensState(false, force);

        public void SwitchTo(ScreenGroup screenGroup, bool force = false)
        {
            Hide(force);
            screenGroup.Show(force);
        }

        public void Add(Screen screen)
        {
            screen.SetState(Displayed);

            if (_screens.Contains(screen))
            {
                AdvancedDebug.LogError<ScreenGroup>(
                    "The screen group already contains this screen");

                return;
            }

            _screens.Add(screen);
        }

        public Screen Remove(IReadOnlyScreen readOnlyScreen)
        {
            Screen screen;

            if (TryFindScreen(readOnlyScreen, out screen) == false)
            {
                AdvancedDebug.LogError<ScreenGroup>(
                    "This screen cannot be removed because it is not contained in this group");

                return null;
            }

            _screens.Remove(screen);
            return screen;
        }

        public bool Contains(IReadOnlyScreen readOnlyScreen) =>
            TryFindScreen(readOnlyScreen, out _);

        private bool TryFindScreen(IReadOnlyScreen readOnlyScreen, out Screen screen)
        {
            for (int i = 0; i < Screens.Count; i++)
            {
                if (Screens[i] == readOnlyScreen)
                {
                    screen = _screens[i];
                    return true;
                }
            }

            screen = null;
            return false;
        }

        private void SetScreensState(bool displayed, bool force)
        {
            foreach (var screen in _screens)
            {
                if (displayed)
                    screen.Show(force);
                else
                    screen.Hide(force);
            }

            Displayed = displayed;
        }
    }
}
