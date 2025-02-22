namespace ColdWind.Core.GeneralControlWindow.Editor
{
    public abstract class Tab
    {
        public abstract string Name { get; }

        public abstract void Draw();

        public virtual void Initialize() { }
    }
}
