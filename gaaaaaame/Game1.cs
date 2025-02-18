using System.Net.Mime;
using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace gaaaaaame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D ballTex;
    private Vector2 ballPos;
    private float ballSPD;
    private float CTRLRDeadZone;
    

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        ballPos = new Vector2(_graphics.PreferredBackBufferWidth / 2,
                              _graphics.PreferredBackBufferHeight / 2);

        ballSPD = 100f;
        CTRLRDeadZone = 4096;
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        ballTex = Content.Load<Texture2D>("ball");
    }

    protected override void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        // TODO: Add your update logic here
        
        float UPDBallSPD = ballSPD * dt;
        var kstate = Keyboard.GetState();
        var jstate = Joystick.GetState((int) PlayerIndex.One); // checks the state of controller dex 1

        if (kstate.IsKeyDown(Keys.Up) || kstate.IsKeyDown(Keys.W))
        {
            ballPos.Y -= UPDBallSPD;
        }
        
        if (kstate.IsKeyDown(Keys.Down) || kstate.IsKeyDown(Keys.S))
        {
            ballPos.Y += UPDBallSPD;
        }
        
        if (kstate.IsKeyDown(Keys.Left) || kstate.IsKeyDown(Keys.A))
        {
            ballPos.X -= UPDBallSPD;
        }
        
        if (kstate.IsKeyDown(Keys.Right) || kstate.IsKeyDown(Keys.D))
        {
            ballPos.X += UPDBallSPD;
        }

        if (Joystick.LastConnectedIndex > -1) // checks if any controllers are connected
        {
            if (jstate.Axes[1] < -CTRLRDeadZone)
            {
                ballPos.Y -= UPDBallSPD;
            }
            
            if (jstate.Axes[1] > CTRLRDeadZone)
            {
                ballPos.Y += UPDBallSPD;
            }
            
            if (jstate.Axes[0] < -CTRLRDeadZone)
            {
                ballPos.X -= UPDBallSPD;
            }
            
            if (jstate.Axes[0] < CTRLRDeadZone)
            {
                ballPos.X += UPDBallSPD;
            }
        } //joystick handling

        if (ballPos.X > _graphics.PreferredBackBufferWidth - ballTex.Width / 2)
        {
            ballPos.X = _graphics.PreferredBackBufferWidth - ballTex.Width / 2;
        }
        else if (ballPos.X < ballTex.Width / 2)
        {
            ballPos.X = ballTex.Width / 2;
        }
        
        if (ballPos.Y > _graphics.PreferredBackBufferHeight - ballTex.Height / 2)
        {
            ballPos.Y = _graphics.PreferredBackBufferHeight - ballTex.Height / 2;
        }
        else if (ballPos.Y < ballTex.Height / 2)
        {
            ballPos.Y = ballTex.Height / 2;
        }
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        _spriteBatch.Draw(ballTex, ballPos, null, Color.White, 0f,
                    new Vector2(ballTex.Width / 2, ballTex.Height / 2), Vector2.One, 
                    SpriteEffects.None, 0f);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}