using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NVorbis.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net.Mime;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;




namespace Pokemon
{
    class Arcanine
    {
        private Texture2D _Wtexture, _Otexture, _blastTexture, _blastImpact, _crunch, _flamethrower, _howl;
        private Rectangle _Olocation, _Wlocation, _blastLocation, _enemyHitbox, _impactLocation, _crunchLocation, _flameLocation, _howlLocation;
        private bool _wild, _canAct, _blastHit, _attackBoost;
        private string _move1, _move2, _move3, _move4, _name;
        private int _speed, _health, _defense, _attack, _sAttack, _sDefense, _move1_PP, _move2_PP, _move3_PP, _move4_PP;
        private float _battle_time, _frame_time, _textTime, _blastInterval, _alpha, _howlEffect;
        private int _healtCurrent;
        public enum Move
        {
            flamethrower, crunch, fireblast, howl, none
        }
        public enum Text
        {
            flamethrower, crunch, fireblast, howl, none
        }
        private Move _currentMove;
        private Text _currentText;
        public Arcanine(Texture2D Wtexture, Texture2D Otexture, Texture2D BlastTexture, Texture2D BlastImpact, Texture2D Crunch, Texture2D Flamethrower, Texture2D Howl, Rectangle Olocation, Rectangle Wlocation)
        {
            _wild = true;
            _canAct = true;
            _blastHit = false;
            _attackBoost = false;
            _Wtexture = Wtexture;
            _Otexture = Otexture;
            _blastTexture = BlastTexture;
            _blastImpact = BlastImpact;
            _Wlocation = Wlocation;
            _Olocation = Olocation;
            _crunch = Crunch;
            _flamethrower = Flamethrower;
            _howl = Howl;
            _move1 = "Flamethrower";
            _move2 = "Crunch";
            _move3 = "Fire Blast";
            _move4 = "Howl";
            _name = "ARCANINE";
            _move1_PP = 15;
            _move2_PP = 15;
            _move3_PP = 5;
            _move4_PP = 40;
            _blastInterval = 0.05f;
            if (_wild)
            {
                _blastLocation = new Rectangle(400, 180, 300, 200);
                _flameLocation = new Rectangle(400, 180, 300, 200);
                _howlLocation = new Rectangle(470, 80, 150, 250);
            }
            _enemyHitbox = new Rectangle(120, 283, 200, 200);
            _crunchLocation = new Rectangle(120, 283, 400, 300);
            Random stats = new Random();
            _speed = stats.Next(80, 111);
            _health = stats.Next(80, 101);
            _healtCurrent = _health;
            _defense = stats.Next(70, 91);
            _attack = stats.Next(100, 121);
            _sAttack = stats.Next(90, 111);
            _sDefense = stats.Next(70, 91);
            _howlEffect = (float)(_attack * 0.67);
            _currentMove = Move.none;
        }




        public void Update(GameTime gametime)
        {
            if (_currentMove == Move.flamethrower && _wild)
            {
                _canAct = false;
                _currentText = Text.flamethrower;
                _textTime += (float)gametime.ElapsedGameTime.TotalSeconds;
                _battle_time += (float)gametime.ElapsedGameTime.TotalSeconds;
                if (_battle_time <= 1)
                {
                    _flameLocation.Width += 5;
                    _flameLocation.Height += 2;
                    _flameLocation.X -= 5;
                }
                else if (_battle_time <= 2)
                {
                    _flameLocation.Width -= 6;
                    _flameLocation.X -= 5;
                    _flameLocation.Y += 8;
                }
                if (_battle_time >= 2)
                {
                    _currentText = Text.none;
                    _currentMove = Move.none;
                    _canAct = true;
                    _textTime = 0;
                    _battle_time = 0;
                    _flameLocation.Width = 300;
                    _flameLocation.Height = 200;
                    _flameLocation.X = 400;
                    _flameLocation.Y = 180;
                }
            }
            if (_currentMove == Move.crunch && _wild)
            {
                _canAct = false;
                _currentText = Text.crunch;
                _textTime += (float)gametime.ElapsedGameTime.TotalSeconds;
                _battle_time += (float)gametime.ElapsedGameTime.TotalSeconds;
                if (_battle_time >= 3 && _textTime >= 3)
                {
                    _canAct = true;
                    _currentText = Text.none;
                    _currentMove = Move.none;
                    _battle_time = 0;
                    _textTime = 0;
                }
            }
            if (_currentMove == Move.fireblast && _wild)
            {
                _canAct = false;
                _currentText = Text.fireblast;
                _textTime += (float)gametime.ElapsedGameTime.TotalSeconds;
                _battle_time += (float)gametime.ElapsedGameTime.TotalSeconds;
                _frame_time += (float)gametime.ElapsedGameTime.TotalSeconds;
                if (!_blastHit && _frame_time >= _blastInterval)
                {
                    _blastLocation.Width += 5;
                    _blastLocation.Height += 1;
                    _blastLocation.X -= 5;
                    _frame_time = 0;
                }
                if (_blastLocation.Intersects(_enemyHitbox))
                {
                    _blastHit = true;
                    _blastLocation.Width -= 5;
                    _blastLocation.Y += 5;
                    _impactLocation = new Rectangle(_enemyHitbox.X, _enemyHitbox.Y, 400, 400);
                }
                if (_battle_time >= 3 && _textTime >= 3)
                {
                    _currentMove = Move.none;
                    _currentText = Text.none;
                    _canAct = true;
                    _blastHit = false;
                    _textTime = 0;
                    _battle_time = 0;
                    _frame_time = 0;
                    _blastLocation = new Rectangle(400, 180, 300, 200);
                }
            }
            if (_currentMove == Move.howl)
            {
                _canAct = false;
                _currentText = Text.howl;
                _textTime += (float)gametime.ElapsedGameTime.TotalSeconds;
                _battle_time += (float)gametime.ElapsedGameTime.TotalSeconds;
                if (!_attackBoost)
                {
                    _attack += (int)_howlEffect;
                    _attackBoost = true;
                }
                if (_battle_time >= 2 && _textTime >= 3)
                {
                    _currentMove = Move.none;
                    _currentText = Text.none;
                    _battle_time = 0;
                    _textTime = 0;
                    _canAct = true;
                    _attackBoost = false;
                }
            }
            if (_healtCurrent == 0)
            {
                _battle_time += (float)gametime.ElapsedGameTime.TotalSeconds;
                if (_battle_time >= 1)
                    _Wlocation.Y += 10;
                if (_battle_time >= 5)
                {
                    _Wlocation.Y = 90;
                    _battle_time = 0;
                }
            }
        }
        public Move CurrentMove
        {
            get { return _currentMove; }
            set { _currentMove = value; }
        }
        public Text CurrentText
        {
            get { return _currentText; }
            set { _currentText = value; }
        }
        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }
        public int HealthCurrent
        {
            get { return _healtCurrent; }
            set { _healtCurrent = value; }
        }
        public int Speed
        {
            get { return _speed; }
        }
        public int Defense
        {
            get { return _defense; }
        }
        public int SDefense
        {
            get { return _sDefense; }
        }
        public int SAttack
        {
            get { return _sAttack; }
        }
        public int Attack
        {
            get { return _attack; }
        }
        public string Move1
        {
            get { return _move1; }
        }
        public string Move2
        {
            get { return _move2; }
        }
        public string Move3
        {
            get { return _move3; }
        }
        public string Move4
        {
            get { return _move4; }
        }
        public int Move1PP
        {
            get { return _move1_PP; }
            set { _move1_PP = value; }
        }
        public int Move2PP
        {
            get { return _move2_PP; }
            set { _move2_PP = value; }
        }
        public int Move3PP
        {
            get { return _move3_PP; }
            set { _move3_PP = value; }
        }
        public int Move4PP
        {
            get { return _move4_PP; }
            set { _move4_PP = value; }
        }
        public string Name
        {
            get { return _name; }
        }
        public bool CanAct
        {
            get { return _canAct; }
            set { _canAct = value; }
        }
        public float TextTime
        {
            get { return _textTime; }
        }
        public bool Wild
        {
            get { return _wild; }
            set { _wild = value; }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (_wild)
            {
                spriteBatch.Draw(_Wtexture, _Wlocation, Color.White);
                if (_currentMove == Move.flamethrower)
                {
                    spriteBatch.Draw(_flamethrower, _flameLocation, Color.White);
                }
                if (_currentMove == Move.crunch)
                {
                    _alpha = 0f;
                    if (_battle_time <= 0.5f)
                        _alpha = _battle_time / 0.5f;
                    else if (_battle_time <= 1.5f)
                        _alpha = 1;
                    else if (_battle_time <= 2f)
                        _alpha = 1f - ((_battle_time - 1.5f) / 0.5f);
                    else
                        _alpha = 0;
                    spriteBatch.Draw(_crunch, _crunchLocation, Color.White * _alpha);
                }
                if (_currentMove == Move.fireblast)
                {
                    spriteBatch.Draw(_blastTexture, _blastLocation, Color.White);
                    if (_blastHit && _battle_time <= 3)
                        spriteBatch.Draw(_blastImpact, _impactLocation, Color.White);
                }
                if (_currentMove == Move.howl)
                {
                    _alpha = 0f;
                    if (_battle_time <= 0.5f)
                        _alpha = _battle_time / 0.5f;
                    else if (_battle_time <= 1.5f)
                        _alpha = 1;
                    else if (_battle_time <= 2f)
                        _alpha = 1f - ((_battle_time - 1.5f) / 0.5f);
                    else
                        _alpha = 0;
                    spriteBatch.Draw(_howl, _howlLocation, null, Color.White * _alpha, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
                }
            }
            else
            {
                spriteBatch.Draw(_Otexture, _Olocation, Color.White);
            }
        }
    }
}