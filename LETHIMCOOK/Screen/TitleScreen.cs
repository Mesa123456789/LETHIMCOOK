using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using MonoGame.Extended.Input;
namespace LETHIMCOOK.Screen
{
    public class TitleScreen : screen
    {
        Texture2D menuTexture;
        Game1 game;
        Texture2D startgame, menugame;
        public TitleScreen(Game1 game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            //Load the background texture for the screen
            startgame = game.Content.Load<Texture2D>("menugame (1)");
            menugame = game.Content.Load<Texture2D>("menugame (2)");
            this.game = game;
        }
        bool oncursor = false, showSetting = false;
        bool oncursor1 = false;
        bool oncursor2 = false;
        bool oncursor3 = false;
        bool oncursor4 = false;
        bool noSound = false;
        bool noMusic = false;
        MouseState msPre, ms;
        public override void Update(GameTime theTime)
        {
            ms = Mouse.GetState();
            Rectangle mouseRec = new Rectangle(ms.X,ms.Y,30,30);
            Rectangle startgame = new Rectangle(282, 272, 238, 89);
            Rectangle soundbutton = new Rectangle(292,384, 30, 30);
            Rectangle ask = new Rectangle(357,387, 30, 30);
            Rectangle setting = new Rectangle(425,387, 30,30);
            Rectangle outgame = new Rectangle(490, 387, 30, 30);

            //***
            Rectangle sound = new Rectangle(341,212, 30, 30);
            Rectangle music = new Rectangle(341,259, 30, 30);
            if (mouseRec.Intersects(sound) && ms.LeftButton == ButtonState.Released && msPre.LeftButton == ButtonState.Pressed && showSetting)
            {
                noSound = true;
            }
            if (mouseRec.Intersects(sound) && ms.LeftButton == ButtonState.Pressed && msPre.LeftButton == ButtonState.Released && showSetting)
            {
                noSound = false;
            }
            if (mouseRec.Intersects(music) && ms.LeftButton == ButtonState.Released && msPre.LeftButton == ButtonState.Pressed && showSetting)
            {
                noMusic = true;
            }
            if (mouseRec.Intersects(music) && ms.LeftButton == ButtonState.Pressed && msPre.LeftButton == ButtonState.Pressed && showSetting)
            {
                noMusic = false;
            }
            msPre = ms;

            if (mouseRec.Intersects(startgame))
            {
                oncursor = true;
                if (ms.LeftButton == ButtonState.Pressed && !showSetting)
                {
                    ScreenEvent.Invoke(game.GameplayScreen, new EventArgs());
                    return;
                }
            }
            else
            {
                oncursor= false;
            }
            if (mouseRec.Intersects(soundbutton))
            {
                oncursor1 = true;
                if (ms.LeftButton == ButtonState.Pressed)
                {
                    showSetting = true;
                }
            }
            else{ oncursor1 = false;}
            if (mouseRec.Intersects(ask))
            {
                oncursor2 = true;
            }
            else { oncursor2 = false; }
            if (mouseRec.Intersects(setting))
            {
                oncursor3 = true;

            }
            else { oncursor3 = false; }
            if (mouseRec.Intersects(outgame))
            {
                oncursor4 = true;
                if (ms.LeftButton == ButtonState.Pressed && !showSetting)
                {
                    game.Exit();
                }
            }
            else { oncursor4 = false; }
            if (showSetting == true)
            {
                oncursor = false;
                oncursor1 = false;
                oncursor2 = false;
                oncursor3 = false;
                oncursor4 = false;
            }
            base.Update(theTime);
        }
        public override void Draw(SpriteBatch theBatch)
        {
            theBatch.Draw(startgame, Vector2.Zero, Color.White);
            theBatch.Draw(menugame, new Vector2(282, 272), new Rectangle(96, 194, 238, 89), Color.White);
            if (oncursor)
            {
                theBatch.Draw(menugame, new Vector2(282, 272), new Rectangle(96, 74, 238, 89), Color.White);
            }
            theBatch.Draw(menugame, new Vector2(278,373), new Rectangle(57,303, 50, 48), Color.White);
            if (oncursor1)
            {
                theBatch.Draw(menugame, new Vector2(279, 373), new Rectangle(58, 367, 50,48), Color.White);
            }
            theBatch.Draw(menugame, new Vector2(278 + 66, 373), new Rectangle(123, 303, 50, 48), Color.White);
            if (oncursor2)
            {
                theBatch.Draw(menugame, new Vector2(279 + 66, 373), new Rectangle(124, 367, 50, 48), Color.White);
            }
            theBatch.Draw(menugame, new Vector2(278 + 66 + 66, 373), new Rectangle(123 + 66, 303, 50, 48), Color.White);
            if (oncursor3)
            {
                theBatch.Draw(menugame, new Vector2(276 + 66 + 66, 373), new Rectangle(121 + 66, 367, 50, 48), Color.White);
            }
            theBatch.Draw(menugame, new Vector2(278 + 66 + 66 + 66, 373), new Rectangle(123 + 66 +66, 303, 50, 48), Color.White);
            if (oncursor4)
            {
                theBatch.Draw(menugame, new Vector2(274 + 66 + 66 + 66, 373), new Rectangle(119 + 66 + 66, 367, 50, 48), Color.White);
            }
            if (showSetting)
            {
                theBatch.Draw(menugame, new Vector2(286,120), new Rectangle(387,79,232,200), Color.White);
            }
            if (noSound)
            {
                theBatch.Draw(menugame, new Vector2(338, 208), new Rectangle(413,311, 17,17), Color.White);
            }
            if (noMusic)
            {
                theBatch.Draw(menugame, new Vector2(338, 258), new Rectangle(413, 311, 17, 17), Color.White);
            }
            base.Draw(theBatch);
        }
    }
}