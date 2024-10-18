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
using System.Diagnostics;


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
        Texture2D _fish, popup,popup2, gotfish, fishing;
        Texture2D salmonmeat, redfishmeat, whalemeat, greenshimpmeat, pinkfishmeat, sharkmeat, shimpmeat , unimeat;
        Texture2D dowfin, normalfish, octopus, salmon, shark, shimai, whale;
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
            RestauarntScreen.IsCooking = false;
            popup = game.Content.Load<Texture2D>("interact");
            popup2 = game.Content.Load<Texture2D>("popup");
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
            unimeat = game.Content.Load<Texture2D>("ingre/unimeat");
            //dowfin, normalfish, octopus, salmon, shark, shimai, whale;
            dowfin = game.Content.Load<Texture2D>("_fish/dowfin");
            normalfish = game.Content.Load<Texture2D>("_fish/normalfish");
            octopus = game.Content.Load<Texture2D>("_fish/octopus");
            salmon = game.Content.Load<Texture2D>("_fish/salmon");
            shark = game.Content.Load<Texture2D>("_fish/shark");
            shimai = game.Content.Load<Texture2D>("_fish/shimai");
            whale = game.Content.Load<Texture2D>("_fish/whale");


            SmallFishList.Add(new Fish(15, "redfish", _fish, redfishmeat, fishPos));
            BigFishList.Add(new Fish(16,"salmon", salmon, salmonmeat, fishPos));
            BigFishList.Add(new Fish(17,"whalemeat", whale, whalemeat, fishPos));
            SmallFishList.Add(new Fish(18,"greenshimpmeat", _fish, greenshimpmeat, fishPos));
            BigFishList.Add(new Fish(19,"pinkfishmeat", dowfin, pinkfishmeat, fishPos));
            BigFishList.Add(new Fish(20,"sharkmeat", shark, sharkmeat, fishPos));
            SmallFishList.Add(new Fish(21,"shimpmeat", _fish, greenshimpmeat, fishPos));
            SmallFishList.Add(new Fish(22,"unimeat", shimai, unimeat, fishPos));
            //***new
            SmallFishList.Add(new Fish(15, "shimai", shimai, shimai, fishPos));
            BigFishList.Add(new Fish(15, "octopus", octopus, octopus, fishPos));
            ////***สุตรโกง
            Game1.BagList.Add(new Fish(15, "redfish", salmon, redfishmeat, fishPos));
            Game1.BagList.Add(new Fish(16, "salmon", salmon, salmonmeat, fishPos));
            Game1.BagList.Add(new Fish(17, "whalemeat", whale, whalemeat, fishPos));
            Game1.BagList.Add(new Fish(18, "greenshimpmeat", _fish, greenshimpmeat, fishPos));
            Game1.BagList.Add(new Fish(19, "pinkfishmeat", dowfin, pinkfishmeat, fishPos));
            Game1.BagList.Add(new Fish(20, "sharkmeat", shark, sharkmeat, fishPos));
            Game1.BagList.Add(new Fish(21, "shimpmeat", _fish, greenshimpmeat, fishPos));
            Game1.BagList.Add(new Fish(22, "unimeat", shimai, unimeat, fishPos));
            Game1.BagList.Add(new Fish(22, "shimai", shimai, shimai, fishPos));
            Game1.BagList.Add(new Fish(22, "octopus", octopus, octopus, fishPos));


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
                        getfish = true;
                        Game1.IsPopUp = true;
                        Console.WriteLine("Big Fish Caught!");
                    }
                    else
                    {
                        int smallFishIndex = _random.Next(0,SmallFishList.Count);
                        var caughtFish = SmallFishList[smallFishIndex];
                        Food.OntableAble = true;
                        getfish = true;
                        Game1.BagList.Add(caughtFish);
                        Game1.IsPopUp = true;
                        Console.WriteLine("Small Fish Caught!");
                    }
                    
                }

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

            foreach (Food food in Game1.BagList)
            {
                if (getfish == true)
                {
                    for (int i = 0; i < Game1.BagList.Count; i++)
                    {
                        _spriteBatch.Draw(popup2, new Rectangle((int)player.Bounds.Position.X-10, (int)player.Bounds.Position.Y - 42,40,40) , Color.White);
                        _spriteBatch.Draw(Game1.BagList[i].foodTexture, new Rectangle((int)player.Bounds.Position.X-5, (int)player.Bounds.Position.Y-40, 32, 32), Color.White);
                    }
                }
                CountTime(250);
            }




        }
        int countPopUp;
        public void CountTime(int timePopup)
        {
            countPopUp += 1;
            {
                if (countPopUp > timePopup)
                {
                    countPopUp = 0;
                    getfish = false;

                }
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

