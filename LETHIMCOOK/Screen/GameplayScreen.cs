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
using System.Reflection.Metadata;
using System.ComponentModel;


namespace LETHIMCOOK.Screen
{
    public class GameplayScreen : screen
    {
        RestauarntScreen res;
        Texture2D texture;
        AnimatedTexture SpriteTexture;
        public static Player player;
        Enemy enemy;
        TiledMap _tiledMap;
        TiledMapRenderer _tiledMapRenderer;
        TiledMapObjectLayer _platformTiledObj;
        private readonly List<IEntity> _entities = new List<IEntity>();
        public readonly CollisionComponent _collisionComponent;
        Game1 game;
        public RectangleF Bounds = new RectangleF(new Vector2(750,440), new Vector2(32, 32));
        Texture2D foodTexture,crabmeat;
        Texture2D hippo,hippomeat;
        Texture2D chicken,chickenmeat;
        Texture2D rat, cheese;
        Texture2D slime, rainbowsmilemeat;
        Texture2D pinkslime, pinksmilemeat;
        Texture2D icebear , wipcream;
        //Texture2D jellyfish;
        Texture2D popup;
        Texture2D enemytex, enemytexbag;
        bool Istrue;

        Vector2 playerPos;// = new Vector2(player.Bounds.Position.X, player.Bounds.Position.Y);
        public GameplayScreen(Game1 game, EventHandler theScreenEvent ) : base(theScreenEvent)
        {
            popup = game.Content.Load<Texture2D>("popup");
            foodTexture = game.Content.Load<Texture2D>("crab");
            pinkslime = game.Content.Load<Texture2D>("pinksmaile");
            hippo = game.Content.Load<Texture2D>("hippo");
            chicken = game.Content.Load<Texture2D>("chicken");
            rat = game.Content.Load<Texture2D>("rat");
            slime = game.Content.Load<Texture2D>("slime");
            icebear = game.Content.Load<Texture2D>("icebear");
            crabmeat = game.Content.Load<Texture2D>("ingre/crabmeat");
            hippomeat = game.Content.Load<Texture2D>("ingre/hippomeat");
            chickenmeat = game.Content.Load<Texture2D>("ingre/chickenmeat");
            cheese = game.Content.Load<Texture2D>("ingre/cheese");
            rainbowsmilemeat = game.Content.Load<Texture2D>("ingre/rainbowsmilemeat");
            pinksmilemeat = game.Content.Load<Texture2D>("ingre/pinksmilemeat");
            wipcream = game.Content.Load<Texture2D>("ingre/wipcream");

            //jellyfish = game.Content.Load<Texture2D>("jellyfish");
            Game1.enemyList.Add(new Enemy("crab", foodTexture, crabmeat, new Vector2(550, 250)));
            Game1.enemyList.Add(new Enemy("pinksmaile",pinkslime, pinksmilemeat, new Vector2(200, 400)));
            Game1.enemyList.Add(new Enemy("hippo", hippo, hippomeat, new Vector2(300 + 100, 300)));
            Game1.enemyList.Add(new Enemy("chicken", chicken, chickenmeat, new Vector2(150 + 100, 150)));
            Game1.enemyList.Add(new Enemy("rat", rat, cheese, new Vector2(300 + 100, 200)));
            Game1.enemyList.Add(new Enemy("slime",slime, rainbowsmilemeat,new Vector2(380 + 100, 330)));
            Game1.enemyList.Add(new Enemy("icebear", icebear, wipcream, new Vector2(230 + 100, 260)));;
            ///Game1.BagList.Add(new Enemy("icebear", icebear, wipcream, new Vector2(230 + 100, 260))); ;
            Game1.ingredentList.Add(new Enemy(1,"crab", foodTexture, Istrue));
            Game1.ingredentList.Add(new Enemy(2,"pinksmaile", pinkslime,Istrue));
            Game1.ingredentList.Add(new Enemy(3,"hippo", hippo, Istrue));
            Game1.ingredentList.Add(new Enemy(4,"chicken", chicken, Istrue));
            Game1.ingredentList.Add(new Enemy(5,"rat", rat, Istrue));
            Game1.ingredentList.Add(new Enemy(6,"slime", slime, Istrue));
            Game1.ingredentList.Add(new Enemy(7,"icebear", icebear, Istrue)); 
            var viewportadapter = new BoxingViewportAdapter(game.Window, game.GraphicsDevice, 800, 450);
            Game1._camera = new OrthographicCamera(viewportadapter);//******//
            game._bgPosition = new Vector2(400, 225);//******//
            game._cameraPosition = new Vector2(400, 200);
            SpriteTexture = new AnimatedTexture(new Vector2(16, 16), 0, 2f,1f);
            SpriteTexture.Load(game.Content, "Player-Sheet", 5,4,10);
            player = new Player(SpriteTexture, playerPos, game, Bounds);
            player.Load(game.Content,"Sword");
            player.Load(game.Content, "Effect");
            //Load the background texture for the screen
            _collisionComponent = new CollisionComponent(new RectangleF(0, 0, 1600, 900));
            _tiledMap = game.Content.Load<TiledMap>("Tile_FrontRestaurant");
            _tiledMapRenderer = new TiledMapRenderer(game.GraphicsDevice, _tiledMap);
            //Get object layers
            foreach (TiledMapObjectLayer layer in _tiledMap.ObjectLayers)
            {
                if (layer.Name == "Tile_Wall_Frontres")
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
        RectangleF doorRec = new RectangleF(750, 400, 100, 20);
        RectangleF SeaMapRec = new RectangleF(700, 880, 100, 10);
        RectangleF CandyMapRec = new RectangleF(0, 400, 50, 100);
        RectangleF mouseRec;
        Vector2 popupPos;
        public static Vector2 mousepos;
        public static Vector2 posMouse;
        public static RectangleF mouseCheck;
        public static bool EnterDoor = false;
        public override void Update(GameTime theTime)
        {
            MouseState ms = Mouse.GetState();
            mousepos = Mouse.GetState().Position.ToVector2();
            posMouse = new Vector2(mousepos.X + (game._cameraPosition.X), mousepos.Y + (game._cameraPosition.Y));
            mouseCheck = new Rectangle((int)posMouse.X, (int)posMouse.Y, 24, 24);
            popupPos = new Vector2(700, 400);
            popupRec = new Rectangle(700,400, 100,50);
            if(mouseCheck.Intersects(popupRec) && ms.LeftButton == ButtonState.Pressed)
            {
                popupRec.X += 10;
            }
            if (!player.Bounds.Intersects(doorRec) && !player.Bounds.Intersects(CandyMapRec) && !player.Bounds.Intersects(SeaMapRec))
            {
                GameplayScreen.EnterDoor = false;
            }
            if (player.Bounds.Intersects(doorRec) && !EnterDoor)
            {
                EnterDoor = true;
                ScreenEvent.Invoke(game.RestauarntScreen, new EventArgs());
                return;
            }
            if (player.Bounds.Intersects(CandyMapRec) && !EnterDoor)
            {
                EnterDoor = true;
                ScreenEvent.Invoke(game.CandyScreen, new EventArgs());
                game._cameraPosition = new Vector2(800, 200);
                return;
            }
            if (player.Bounds.Intersects(SeaMapRec) && !EnterDoor)
            {
                EnterDoor = true;
                ScreenEvent.Invoke(game.SeaScreen, new EventArgs());
                // player.Bounds.Position = new Vector2(780, 64);
                game._cameraPosition = new Vector2(440, 0);
                return;
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
            for (int i = Game1.enemyList.Count - 1; i >= 0; i--)
            {
                Game1.enemyList[i].Update(theTime);
            }

            foreach (IEntity entity in _entities)
            {
                entity.Update(theTime);
            }
            _collisionComponent.Update(theTime);
            _tiledMapRenderer.Update(theTime);
            
            Game1._camera.LookAt(game._bgPosition + game._cameraPosition);//******//
            player.Update(theTime);
            base.Update(theTime);
        }
        Rectangle popupRec;
        public override void Draw(SpriteBatch _spriteBatch)
        {

            var transformMatrix = Game1._camera.GetViewMatrix();//******//
            _tiledMapRenderer.Draw(transformMatrix);//******//
            _spriteBatch.End();
            _spriteBatch.Begin(transformMatrix: transformMatrix, samplerState: SamplerState.PointClamp);//******//
 
            //_spriteBatch.Draw(popup,popupRec, Color.White);
            foreach (Food food in Game1.foodList)
            {
                for (int i = 0; i < Game1.foodList.Count; i++)
                {
                    Game1.foodList[i].Draw(_spriteBatch);
                }
            }
            foreach (Enemy enemy in Game1.enemyList)
            {
                for (int i = 0; i < Game1.enemyList.Count; i++)
                {
                    Game1.enemyList[i].Draw(_spriteBatch);
                }
            }
            player.Draw(_spriteBatch);
            //_spriteBatch.Draw(crabmeat,new Rectangle(700,400,32,32), Color.White);




        }


    }
}

