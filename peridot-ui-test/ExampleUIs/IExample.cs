using Microsoft.Xna.Framework.Graphics;

public interface IExample
{ 
    void Initialize(SpriteFont font);
    IUIElement GetRootElement();
}