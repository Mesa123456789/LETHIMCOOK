
using LETHIMCOOK.Screen;
using LETHIMCOOK.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collections;
using MonoGame.Extended.Timers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LETHIMCOOK
{
    public class Game1 : Game
    {

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public GameplayScreen GameplayScreen;
        public CandyScreen CandyScreen;
        public SeaScreen SeaScreen;

        public RestauarntScreen RestauarntScreen;
        public TitleScreen TitleScreen;
        public screen mCurrentScreen;
        public Vector2 _bgPosition;
        Player player;
        public static Vector2 move;
        public static OrthographicCamera _camera;
        public Vector2 _cameraPosition;

        ///****/
        public Texture2D book;
        Texture2D ui;
        public Texture2D uiHeart;
        Texture2D inventory;
        Texture2D popup;
        Texture2D FridgeUi;
        Texture2D bookUi;
        public Texture2D QuestUI;
        public Texture2D Quest;
        public RectangleF questboxRec;
        public RectangleF bagRec;
        public Texture2D bag;
        Texture2D interact , craft;

        public static List<Food> BagList = new List<Food>();
        public static List<Food> foodList = new();
        public static List<Enemy> enemyList = new();
        public static List<Food> CraftList = new List<Food>();
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 450;
            _graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
        }
        AnimatedTexture SpriteTexture;
        Vector2 playerPos;
        Game1 game;
        public RectangleF Bounds = new RectangleF(new Vector2(750, 440), new Vector2(40, 60));
        protected override void Initialize()
        {
           // player = new Player(SpriteTexture, playerPos, game, Bounds);
            //camera_ = new Camera_1();
            // TODO: Add your initialization logic here
            base.Initialize();
        }
        public static int currentHeart;

        protected override void LoadContent()
        {
            SpriteTexture = new AnimatedTexture(new Vector2(0, 0), 0, 2f, 1f);
            SpriteTexture.Load(Content, "PlayerIdel", 5, 4, 5);
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ui = Content.Load<Texture2D>("ui");
            uiHeart = Content.Load<Texture2D>("uiHeart");
            interact = Content.Load<Texture2D>("interact");
            book = Content.Load<Texture2D>("book");
            Quest = Content.Load<Texture2D>("Quest");
            bag = Content.Load<Texture2D>("bag");
            FridgeUi = Content.Load<Texture2D>("FridgeUI");
            bookUi = Content.Load<Texture2D>("BookUI");
            QuestUI = Content.Load<Texture2D>("QuestUI");
            inventory = Content.Load<Texture2D>("inventory");
            popup = Content.Load<Texture2D>("popup");
            craft = Content.Load<Texture2D>("craft");

            ///***///
            TitleScreen = new TitleScreen(this, new EventHandler(GameplayScreenEvent));
            RestauarntScreen = new RestauarntScreen(this, new EventHandler(GameplayScreenEvent));
            CandyScreen = new CandyScreen(this, new EventHandler(GameplayScreenEvent));
            SeaScreen = new SeaScreen(this, new EventHandler(GameplayScreenEvent));
            GameplayScreen = new GameplayScreen(this, new EventHandler(GameplayScreenEvent));
            mCurrentScreen = GameplayScreen;
            currentHeart = CandyScreen.uiHeart.Width - 10;
        }
        public RectangleF bookRec;
        public RectangleF mouseRec;
        public RectangleF XboxQ;
        public RectangleF xBox;
        bool OnCursor1;
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
           Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
            UpDateUI();
            //player.Update(gameTime);
            mCurrentScreen.Update(gameTime);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            mCurrentScreen.Draw(_spriteBatch);
            _spriteBatch.End();
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            DrawUiGameplay(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        public void GameplayScreenEvent(object obj, EventArgs e)
        {
            mCurrentScreen = (screen)obj;
        }
        public void UpdateCamera(Vector2 move)
        {
            _camera.LookAt(_bgPosition + _cameraPosition);//******//
            _cameraPosition += move;
        }
        public float GetCameraPosX()
        {
            return _cameraPosition.X;
        }
        public float GetCameraPosY()
        {
            return _cameraPosition.Y;
        }
        bool OncursorXBOX = false;
        bool OncursorxboxQ = false;
        bool closeXBox = false;
        bool closeXBoxQuest = false;
        public bool ShowInventory = false;
        public static bool IsPopUp = false;
        public static RectangleF doorRec = new RectangleF(120, 40, 200, 20);
        bool IssendMenuInterect = false;
        bool IsInterect = false;
        public static bool Ontable = false;
        bool GotMenu = false;
        bool Crafting = false;
        int MenuPopup;
        bool IsFrigeInterect = false;
        bool sendingMenu = false;
        public void UpdateUIRest(Player player , GameTime gameTime)
        {
            MouseState ms = Mouse.GetState();
            mouseRec = new RectangleF(ms.X, ms.Y, 50, 50);
            RectangleF FrigeRec = new RectangleF(348, 120, 40, 80);
            RectangleF tableBox = new RectangleF(450, 150, 130, 20);
            RectangleF sendMenu = new RectangleF(600, 240, 40, 30);
            RectangleF equal = new RectangleF(345, 140, 120, 50);
            if (player.Bounds.Intersects(FrigeRec))
            {
                IsFrigeInterect = true;
                if (mouseRec.Intersects(FrigeRec) && ms.LeftButton == ButtonState.Pressed)
                {
                    openFridgeUI = true;
                }
            }
            else { IsFrigeInterect = false; openFridgeUI = false; }
            if (player.Bounds.Intersects(tableBox))
            {
                IsInterect = true;
                if (ms.LeftButton == ButtonState.Pressed && mouseRec.Intersects(tableBox))
                {
                    Ontable = true;
                }
            }
            else
            {
                IsInterect = false;
                Ontable = false;
            }
            if (player.Bounds.Intersects(equal) && Ontable)
            {
                Crafting = true;
            }
            else
            {
                Crafting = false;
            }
            for (int i = 0; i < Game1.BagList.Count; i++)
            {
                Game1.BagList[i].Update(gameTime);
                if (mouseRec.Intersects(Game1.BagList[i].foodBox) && ms.LeftButton == ButtonState.Pressed && Ontable)
                {
                    Game1.CraftList.Add(Game1.BagList[i]);
                    Game1.BagList.RemoveAt(i);
                    break;
                }
            }
            if (player.Bounds.Intersects(sendMenu))
            {
                IssendMenuInterect = true;
                //if (mouseBox.Intersects(sendMenu) && ms.LeftButton == ButtonState.Pressed && GotMenu == true)
                //{
                //    sendingMenu = false;
                //}
            }
            else
            {
                IssendMenuInterect = false;
            }
        }
        public void DrawUIRest(SpriteBatch _spriteBatch)
        {

            //if (sendingMenu == true && GotMenu == true && food.getFood == 2)
            //{
            //    _spriteBatch.Draw(uni, new Rectangle((int)player.CharPosition.X, (int)player.CharPosition.Y + 13, 32, 32), Color.White);
            //}
            if (openFridgeUI == true)
            {
                _spriteBatch.Draw(FridgeUi, new Vector2(0, 0), Color.White);
            }
            if (Ontable == true)
            {
                _spriteBatch.Draw(craft, new Vector2(215, 60), Color.White);
                _spriteBatch.Draw(inventory, new Vector2(129, 220), Color.White);
                if (!GotMenu)
                {
                    for (int i = 0; i < Game1.CraftList.Count; i++)
                    {
                        _spriteBatch.Draw(Game1.CraftList[i].foodTexture, new Vector2(285 + i * 68, 98), new Rectangle(0, 0, 32, 32), Color.White);
                    }
                }
                for (int i = 0; i < Game1.BagList.Count; i++)
                {
                    _spriteBatch.Draw(Game1.BagList[i].foodTexture, new Vector2(160 + i * 52, 250), new Rectangle(0, 0, 32, 32), Color.White);
                }
            }

            //if (Crafting == true && Ontable)// && food.getFood == 2)
            //{
            //    _spriteBatch.Draw(QuestUI, new Vector2(720, 320) , Color.White);
            //    //_spriteBatch.Draw(menuBG, new Rectangle(650, 230, 300, 300), Color.White);
            //    ///_spriteBatch.Draw(uni, new Rectangle(733, 330, 128, 128), Color.White);
            //    GotMenu = true;
            //}
            ////_spriteBatch.Draw(QuestUI, new Vector2(720, 320), new Rectangle(725, 133, 146, 190), Color.White,rotationMenuBG, Vector2.Zero, 1f, 0, 1);
            ////if (MenuPopup == 1 && !FinsihCooking)
            ////{
            ////    if (Crafting == true && food.getFood == 2)
            ////    {
            ////        _spriteBatch.Draw(QuestUI, new Vector2(720, 320), new Rectangle(725, 133, 146, 190), Color.White,
            ////            rotationMenuBG, Vector2.Zero, 1f, 0, 1);
            ////        _spriteBatch.Draw(menuBG, new Rectangle(650, 230, 300, 300), Color.White);
            ////        _spriteBatch.Draw(uni, new Rectangle(733, 330, 128, 128), Color.White);
            ////        GotMenu = true;
            ////    }

            ////    CountTime(200);
            ////}
        }
        public void UpDateUI()
        {
            MouseState ms = Mouse.GetState();
            mouseRec = new RectangleF(ms.X, ms.Y, 50, 50);
            bagRec = new Rectangle(750, 25, 25, 20);
            bookRec = new RectangleF(755, 100, 25, 15);
            questboxRec = new Rectangle(750, 150, 25, 20);//680, 30
            xBox = new Rectangle(650, 75, 30, 30);
            XboxQ = new Rectangle(680, 30, 30, 30);
            if (mouseRec.Intersects(bookRec) && ms.LeftButton == ButtonState.Pressed)
            {
                closeXBoxQuest = false;
                closeXBox = false;
                openbookUI = true;
                openQuestUI = false;
                ShowInventory = false;
            }
            else { OnCursor1 = false; }
            if (mouseRec.Intersects(questboxRec) && ms.LeftButton == ButtonState.Pressed)
            {
                closeXBox = false;
                openQuestUI = true;
                ShowInventory = false;
                openbookUI = false;
            }
            else { OnCursor2 = false; }
            if (mouseRec.Intersects(bagRec))
            {
                OnCursor = true;
                if (ms.LeftButton == ButtonState.Pressed && mouseRec.Intersects(bagRec))
                {
                    closeXBox = false;
                    ShowInventory = true;
                    openQuestUI = false;
                    openbookUI = false;
                }
            }
            else { OnCursor = false; }
            if (mouseRec.Intersects(xBox) && ms.LeftButton == ButtonState.Pressed)
            {
                closeXBox = true;
                openbookUI = false;
                ShowInventory = false;
                openQuestUI = false;
            }
            if (mouseRec.Intersects(XboxQ) && ms.LeftButton == ButtonState.Pressed)
            {
                closeXBoxQuest = true;
                closeXBox = false;
                openbookUI = false;
                ShowInventory = false;
                openQuestUI = false;
            }
            if (mouseRec.Intersects(xBox)) { OnCursorXBOX = true; }
            else { OnCursorXBOX = false; }
            if (mouseRec.Intersects(XboxQ)) { OncursorxboxQ = true; }
            else { OncursorxboxQ = false; }
            if (mouseRec.Intersects(bookRec)) { OnCursor1 = true; }
            else { OnCursor1 = false; }
            if (mouseRec.Intersects(questboxRec)) { OnCursor2 = true; }
            else { OnCursor2 = false; }
            if (ShowInventory) { OnCursor1 = false; OnCursor2 = false; }
            if (openbookUI) { OnCursor = false; OnCursor2 = false; }
            if (openQuestUI) { OnCursor = false; OnCursor1 = false; }
            if (currentHeart < 60) { color = Color.Red; }
            else { color = Color.White; }

        }

        Color color = Color.White;
        bool OnCursor = false;
        bool OnCursor2 = false;
        bool OnCursorXBOX = false;
        bool openbookUI = false;
        bool openQuestUI = false;
        bool openFridgeUI = false;
        int mouse_state = 1;
        public void DrawUiGameplay(SpriteBatch _spriteBatch)
        {
            foreach (Food food in BagList)
            {
                if (IsPopUp == true)
                {
                    for (int i = 0; i < BagList.Count; i++)
                    {
                        _spriteBatch.Draw(popup, new Vector2(635, 170), Color.White);
                        _spriteBatch.Draw(BagList[i].foodTexture, new Vector2(653, 180),new Rectangle(0, 0, 32,32), Color.White);
                    }
                    CountTime(100);
                }
            }
            if (openQuestUI == true)
            {
                _spriteBatch.Draw(QuestUI, new Vector2(0, 0), new Rectangle(0, 0, 700, 400), Color.White);
                if (closeXBox == false)
                {
                    _spriteBatch.Draw(QuestUI, new Vector2(655, 35), new Rectangle(745 + 64, 81, 64, 40), Color.White);
                }
                if (OnCursorXBOX == true)
                {
                    _spriteBatch.Draw(QuestUI, new Vector2(655, 35), new Rectangle(745 , 81, 64, 40), Color.White);
                }
            }
            if (openbookUI == true)
            {
                _spriteBatch.Draw(bookUi, new Vector2(150, 0), new Rectangle(153, 0, 800, 500), Color.White);
                if (OncursorxboxQ == false)
                {
                    _spriteBatch.Draw(QuestUI, new Vector2(683, 20), new Rectangle(745 + 64, 81, 64, 40), Color.White);
                }
                if (OncursorxboxQ == true)
                {
                    _spriteBatch.Draw(QuestUI, new Vector2(683, 20), new Rectangle(745, 81, 64, 40), Color.White);
                }
            }
            if (ShowInventory == true)
            {
                _spriteBatch.Draw(inventory, new Vector2(123, 125), Color.White);
                for (int i = 0; i < BagList.Count; i++)
                {
                    _spriteBatch.Draw(BagList[i].foodTexture, new Vector2(153 + i * 53, 156),new Rectangle(0, 0, 32, 32), Color.White);
                }
                if (closeXBox == false)
                {
                    _spriteBatch.Draw(QuestUI, new Vector2(650, 60), new Rectangle(745 + 64, 81, 64, 40), Color.White);
                }
                if (OnCursorXBOX == true)
                {
                    _spriteBatch.Draw(QuestUI, new Vector2(650, 60), new Rectangle(745, 81, 64, 40), Color.White);
                }
            }
            _spriteBatch.Draw(bag, new Vector2(735, 15), new Rectangle(64, 0, 48, 44), Color.White);
            if (OnCursor == true || ShowInventory)
            {
                _spriteBatch.Draw(bag, new Vector2(735 - 2, 15), new Rectangle(1, 0, 63, 44), Color.White);
            }
            _spriteBatch.Draw(book, new Vector2(735, 65), new Rectangle(64, 0, 48, 44), Color.White);
            if (OnCursor1 == true || openbookUI)
            {
                _spriteBatch.Draw(book, new Vector2(735-2, 65), new Rectangle(1, 0, 63, 44), Color.White);
            }
            _spriteBatch.Draw(Quest, new Vector2(735, 115), new Rectangle(64, 0, 48, 44), Color.White);
            if (OnCursor2 == true || openQuestUI == true)
            {
                _spriteBatch.Draw(Quest, new Vector2(735-2, 115), new Rectangle(1, 0, 63, 44), Color.White);
            }
            _spriteBatch.Draw(uiHeart, new Vector2(110, 22),
new Rectangle(0, 0, currentHeart + 10, 18), color);
            _spriteBatch.Draw(ui, new Vector2(6, 6), Color.White);
            SpriteTexture.DrawFrame(_spriteBatch, new Vector2(23, 25), 1);

        }

        public int countPopUp;
        public void CountTime(int timePopup)
        {
            countPopUp += 1;
            {
                if (countPopUp > timePopup)
                {
                    countPopUp = 0;
                    IsPopUp = false;
                    //ShowInventory = false;

                }
            }
        }



    }
}
