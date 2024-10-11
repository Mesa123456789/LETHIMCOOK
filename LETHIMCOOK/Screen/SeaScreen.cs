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
using LETHIMCOOK.Sprite;


namespace LETHIMCOOK.Screen
{
    public class SeaScreen : screen
    {
        Vector2 fishPos;
        Texture2D fishTexBag;
        string name;
        Texture2D fishTex;
        Fish fish;
        Texture2D texture;
        AnimatedTexture SpriteTexture;
        Player player;
        Vector2 playerPos = Vector2.Zero;
        TiledMap _tiledMap;
        TiledMapRenderer _tiledMapRenderer;
        TiledMapObjectLayer _platformTiledObj;
        private readonly List<IEntity> _entities = new List<IEntity>();
        public readonly CollisionComponent _collisionComponent;
        //Camera _camera;
        Game1 game;
        RectangleF Bounds = new RectangleF(new Vector2(780, 64), new Vector2(40, 60));
        Texture2D _fish, popup, gotfish, fishing;
        Texture2D salmonmeat, redfishmeat, whalemeat, greenshimpmeat, pinkfishmeat, sharkmeat, shimpmeat , unimeat;
        public static List<Fish> BigFishList = new();
        public static List<Fish> SmallFishList = new();
        private Random _random;
        public static bool _isFishing;
        public static double _fishCatchTime;
        public static double _elapsedTime;
        bool Isinteract = false;

        //Tile_FrontRestaurant Tile_Wall_Frontres
        public SeaScreen(Game1 game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            popup = game.Content.Load<Texture2D>("interact");
            _fish = game.Content.Load<Texture2D>("fish");
            fishing = game.Content.Load<Texture2D>("fishing");
            gotfish = game.Content.Load<Texture2D>("gotfish");
            //**ingre
            salmonmeat = game.Content.Load<Texture2D>("ingre/salmonmeat");
            redfishmeat = game.Content.Load<Texture2D>("ingre/redfishmeat");
            whalemeat = game.Content.Load<Texture2D>("ingre/whalemeat");
            greenshimpmeat = game.Content.Load<Texture2D>("ingre/greenshimpmeat");
            pinkfishmeat = game.Content.Load<Texture2D>("ingre/pinkfishmeat");
            sharkmeat = game.Content.Load<Texture2D>("ingre/sharkmeat");
            shimpmeat = game.Content.Load<Texture2D>("ingre/shimpmeat");
            unimeat = game.Content.Load<Texture2D>("ingre/unimeat");
            SmallFishList.Add(new Fish("fish", _fish, redfishmeat, fishPos));
            BigFishList.Add(new Fish("salmonmeat", _fish, salmonmeat, fishPos));
            BigFishList.Add(new Fish("whalemeat", _fish, whalemeat, fishPos));
            SmallFishList.Add(new Fish("greenshimpmeat", _fish, greenshimpmeat, fishPos));
            BigFishList.Add(new Fish("pinkfishmeat", _fish, pinkfishmeat, fishPos));
            BigFishList.Add(new Fish("sharkmeat", _fish, sharkmeat, fishPos));
            SmallFishList.Add(new Fish("shimpmeat", _fish, shimpmeat, fishPos));
            SmallFishList.Add(new Fish("unimeat", _fish, unimeat, fishPos));

            var viewportadapter = new BoxingViewportAdapter(game.Window, game.GraphicsDevice, 800, 450);
            Game1._camera = new OrthographicCamera(viewportadapter);//******//
            game._bgPosition = new Vector2(400, 225);//******//
            game._cameraPosition = new Vector2(440, 0);
            SpriteTexture = new AnimatedTexture(new Vector2(16, 16), 0, 2f, 1f);
            SpriteTexture.Load(game.Content, "Player-Sheet", 5, 4,10);
            player = new Player(SpriteTexture, playerPos, game, Bounds);
            player.Load(game.Content, "Sword");
            player.Load(game.Content, "Effect");
            ////
            framePerSec = 4;
            timePerFream = (float)1 / framePerSec;
            frame = 4;

            _random = new Random();
            _isFishing = false;

            //Load the background texture for the screen

            _collisionComponent = new CollisionComponent(new RectangleF(0, 0, 1600, 900));


            _tiledMap = game.Content.Load<TiledMap>("Tile_Sea");

            _tiledMapRenderer = new TiledMapRenderer(game.GraphicsDevice, _tiledMap);
            //Get object layers
            foreach (TiledMapObjectLayer layer in _tiledMap.ObjectLayers)
            {
                if (layer.Name == "Tile_Wall_Sea")
                {
                    _platformTiledObj = layer;
                }
            }
            foreach (TiledMapObject obj in _platformTiledObj.Objects)
            {
                Vector2 position = new Vector2(obj.Position.X, obj.Position.Y);
                _entities.Add(new PlatformEntity(game, new RectangleF(position, obj.Size)));
            }

            _entities.Add(player);

            foreach (IEntity entity in _entities)
            {
                _collisionComponent.Insert(entity);


            }
            this.game = game;
        }

        RectangleF mouseRec;
        RectangleF FrontRec;
        RectangleF popupRec,SeaRec;
        public static Vector2 mousepos;
        public static Vector2 posMouse;
        public static RectangleF mouseCheck;
        bool popUpfish;
        bool getfish = false ;
        public override void Update(GameTime theTime)
        {
            MouseState ms = Mouse.GetState();
            mousepos = Mouse.GetState().Position.ToVector2();
            posMouse = new Vector2(mousepos.X + (game._cameraPosition.X), mousepos.Y + (game._cameraPosition.Y));
            mouseCheck = new Rectangle((int)posMouse.X, (int)posMouse.Y, 24, 24);
            popupRec = new RectangleF(840, 700, 140, 50);
            SeaRec = new RectangleF(400, 750, 1000, 200);
            FrontRec = new RectangleF(740, 0, 100, 10);
            if (player.Bounds.Intersects(FrontRec) && !GameplayScreen.EnterDoor)
            {
                ScreenEvent.Invoke(game.GameplayScreen, new EventArgs());
                game._cameraPosition = new Vector2(400, 478);
                GameplayScreen.player.Bounds.Position = new Vector2(765, 880);
                GameplayScreen.EnterDoor = true;
                return;
            }
            if (!player.Bounds.Intersects(FrontRec))
            {
                GameplayScreen.EnterDoor = false;
            }
            //if (mouseCheck.Intersects(popupRec) && ms.LeftButton == ButtonState.Pressed)
            //{
            //    popupRec.X += 10;
            //}
            if (player.Bounds.Intersects(popupRec))
            {
                Isinteract = true;
            }
            else
            {
                Isinteract = false;
            }
            if (player.Bounds.Intersects(popupRec) && mouseCheck.Intersects(SeaRec) && ms.LeftButton == ButtonState.Pressed && !_isFishing)
            {
                _isFishing = true;
                _fishCatchTime = _random.Next(2, 5); // Random time between 2 to 5 seconds
                _elapsedTime = 0;
            }
            else if (ms.LeftButton == ButtonState.Released)
            {
                _isFishing = false;
            }

            if (_isFishing)
            {
                _elapsedTime += theTime.ElapsedGameTime.TotalSeconds;

                if (_elapsedTime >= _fishCatchTime)
                {
                    _isFishing = false;
                    bool isBigFish = _random.Next(0, 2) == 0; 
                    if (isBigFish)
                    {
                        int bigFishIndex = _random.Next(0,BigFishList.Count);
                        var caughtFish = BigFishList[bigFishIndex];
                        Food.OntableAble = true;
                        Game1.BagList.Add(caughtFish);
                        Game1.IsPopUp = true;
                        Console.WriteLine("Big Fish Caught!");
                    }
                    else
                    {
                        int smallFishIndex = _random.Next(0,SmallFishList.Count);
                        var caughtFish = SmallFishList[smallFishIndex];
                        Food.OntableAble = true;
                        Game1.BagList.Add(caughtFish);
                        Game1.IsPopUp = true;
                        Console.WriteLine("Small Fish Caught!");
                    }
                    getfish = true;
                }

            }
            else
            {
                getfish = false;
            }
            for (int i = Game1.enemyList.Count - 1; i >= 0; i--)
            {
                player.Attack(Game1.enemyList[i]);
            }
            for (int i = 0; i < Game1.BagList.Count; i++)
            {
                Game1.BagList[i].Update(theTime);
            }
            for (int i = Game1.foodList.Count - 1; i >= 0; i--)
            {
                Game1.foodList[i].Update(theTime);
            }
            for (int i = BigFishList.Count - 1; i >= 0; i--)
            {
                BigFishList[i].Update(theTime);
            }
            for (int i = SmallFishList.Count - 1; i >= 0; i--)
            {
                SmallFishList[i].Update(theTime);
            }

            foreach (IEntity entity in _entities)
            {
                entity.Update(theTime);
            }
            _collisionComponent.Update(theTime);
            _tiledMapRenderer.Update(theTime);
            //Console.WriteLine("Fish Caught!");
            Game1._camera.LookAt(game._bgPosition + game._cameraPosition);//******//
            player.Update(theTime);
            UpdateFream((float)theTime.ElapsedGameTime.TotalSeconds);
            base.Update(theTime);
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {

            var transformMatrix = Game1._camera.GetViewMatrix();//******//
            _tiledMapRenderer.Draw(transformMatrix);//******//
            _spriteBatch.End();
            _spriteBatch.Begin(transformMatrix: transformMatrix, samplerState: SamplerState.PointClamp);//******//

            if (Isinteract == true)
            {
                _spriteBatch.Draw(popup, new Rectangle(840, 700, 140, 50), Color.White);
            }
            //foreach (IEntity entity in _entities)
            //{
            //    entity.Draw(_spriteBatch);
            //}
            foreach (Food food in Game1.foodList)
            {
                for (int i = 0; i < Game1.foodList.Count; i++)
                {
                    Game1.foodList[i].Draw(_spriteBatch);
                }
            }
            foreach (Fish fish in BigFishList)
            {
                for (int i = 0; i < BigFishList.Count; i++)
                {
                    BigFishList[i].Draw(_spriteBatch);
                }
            }
            foreach (Fish fish in SmallFishList)
            {
                for (int i = 0; i < SmallFishList.Count; i++)
                {
                    SmallFishList[i].Draw(_spriteBatch);
                }
            }
            player.Draw(_spriteBatch);
            if(_isFishing == true)
            {
                _spriteBatch.Draw(fishing, new Vector2(player.Bounds.Position.X, player.Bounds.Position.Y + 40), new Rectangle(32 * frame, 0, 32, 150), Color.White, 0.0f, new Vector2(0,0), 1.0f, SpriteEffects.None, 0.0f);
            }
            if (getfish == true)
            {
                _spriteBatch.Draw(gotfish,new Rectangle((int)player.Bounds.Position.X, (int)player.Bounds.Position.Y - 20, 32,32),Color.White);
            }
  
            


        }
        public int frame;
        public int framePerSec;
        public float totalElapsed;
        public float timePerFream;
        void UpdateFream(float elapsed)
        {
            totalElapsed += elapsed;
            if (totalElapsed > timePerFream)
            {
                frame = (frame + 1) % 4;
                totalElapsed -= timePerFream;
            }
        }

    }
}

