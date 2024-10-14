using LETHIMCOOK.Screen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Timers;
using System;


namespace LETHIMCOOK.Sprite
{
    public class Fish : Food
    {
        Vector2 fishPos;
        Texture2D fishTexBag;
        Texture2D fishTex;

        private Random _random;
        public static bool _isFishing;
        public static double _fishCatchTime;
        public static double _elapsedTime;


        public Fish(int id, string name, Texture2D fishTex, bool Istrue) : base(id, name, fishTex, Istrue)
        {
            this.id = id;
            this.name = name;
            this.fishTex = fishTex;
            istrue = Istrue;
        }
        public Fish(int id, string name, Texture2D fishTex, Texture2D fishTexBag, Vector2 fishPos) : base(name, fishTex, fishTexBag, fishPos)
        {
            this.id = id;
            this.name = name;
            this.fishTex = fishTex;
            this.fishPos = fishPos;
            this.fishTexBag = fishTexBag;

            //framePerSec = 7;
            //timePerFream = (float)1 / framePerSec;
            //frame = 0;
            _random = new Random();
            _isFishing = false;
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

            //if (mouseState.LeftButton == ButtonState.Pressed && !_isFishing)
            //{
            //    _isFishing = true;
            //    Console.WriteLine("Fish Caught!");
            //    //_fishCatchTime = _random.Next(2, 5); // Random time between 2 to 5 seconds
            //    //_elapsedTime = 0;
            //}

            //if (_isFishing)
            //{
            //    _elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

            //    if (_elapsedTime >= _fishCatchTime)
            //    {
            //        _isFishing = false;
            //        //OnCollision();
            //        Console.WriteLine("Fish Caught!");
            //    }
            //}

        }


        public override void OnCollision()
        {
            OntableAble = true;
            Game1.BagList.Add(this);
            Game1.IsPopUp = true;
            //foreach (Fish fish in SeaScreen.FishList)
            //{
            //    SeaScreen.FishList.Remove(this);
            //    break;
            //}

        }
        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(fishTex, fishPos, new Rectangle(0, 0, 32, 32), Color.White, 0.0f, new Vector2(16, 16), 2.0f, SpriteEffects.None, 0.0f);
        }
        public override void DrawBag(SpriteBatch batch)
        {
            batch.Draw(fishTexBag, fishPos, new Rectangle(0, 0, 32, 32), Color.White);
        }
    }
}
