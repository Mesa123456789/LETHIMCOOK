using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

using MonoGame.Extended.Collisions;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Tiled;
using MonoGame.Extended;
using MonoGame.Extended.Timers;
using MonoGame.Extended.ViewportAdapters;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;
using LETHIMCOOK.Screen;

namespace LETHIMCOOK.Sprite
{
    public class Food : Sprite
    {
        public Vector2 foodPosition;
        public RectangleF foodBox;
        public Texture2D foodTexture;
        public Texture2D foodTexBag;
        public int getFood;
        public static bool OntableAble;
        public Player player;
        Game1 game;
        public RectangleF Bounds;
        AnimatedTexture SpriteTexture;
        Vector2 playerPos;
        public string name;
        public bool istrue;
        public int id;

        public Food(int id,string name, Texture2D foodTexture, bool Istrue)
        {
            this.id = id;
            this.name = name;
            this.foodTexture = foodTexture;
            istrue = Istrue;
        }
        public Food(string name,Texture2D foodTexture, Texture2D foodTexBag ,Vector2 foodPosition)
        {
            this.name = name;
            this.foodTexture = foodTexture;
            this.foodTexBag = foodTexBag;
            this.foodPosition = foodPosition;
            foodBox = new RectangleF((int)foodPosition.X, (int)foodPosition.Y, 50, 50);
            OntableAble = false;
            player = new Player(SpriteTexture, playerPos, game, Bounds);

        }

        public override void Update(GameTime gameTime)
        {
            MouseState ms = Mouse.GetState();
            if (foodBox.Intersects(GameplayScreen.player.Bounds) && !OntableAble)
            {
                if (ms.LeftButton == ButtonState.Pressed)
                {
                    OnCollision();
                }
            }
            foodBox = new RectangleF((int)foodPosition.X, (int)foodPosition.Y, 50, 50);
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(foodTexture, foodPosition, Color.White);
        }
        public override void DrawBag(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(foodTexBag, foodPosition, Color.White);
        }
        public virtual void OnCollision()
        {
            OntableAble = true;
            Game1.BagList.Add(this);
            Game1.IsPopUp = true;
            foreach (Food food in Game1.foodList)
            {
                Game1.foodList.Remove(this);
                break;
            }
        }


    }
}
