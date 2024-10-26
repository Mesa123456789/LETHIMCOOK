using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using System.Diagnostics.Eventing.Reader;

namespace CookHim
{
    public class SpriteEntity : IEntity
    {

        private readonly Game1 game;

        public int frame;
        public int framePerSec;
        public float totalElapsed;
        public float timePerFream;

        public int velocity = 4;
        public int hp;
        public Vector2 move;
        public IShapeF Bounds { get; set; }
        public CircleF attackRange;

        public bool isAttack;
        public virtual void Load(ContentManager content, string asset)
        {

        }
        public virtual void Update(GameTime gameTime)
        {

        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
        public virtual void OnCollision(CollisionEventArgs collisionInfo)
        {
        }
    }
}
