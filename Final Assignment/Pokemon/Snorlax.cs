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
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    class Snorlax
    {
        private Texture2D _texture, _hyperTexture, _impactTexture, _defenseTexture;
        private Rectangle _location, _hyper_Location, _impact_Location, _enemy_Location, _enemy_Hitbox;
        private bool _hyper_hit, _canAct, _defenseBoost;
        private string _move1, _move2, _move3, _move4, _move_type, _name;
        private int _speed, _health, _defense, _attack, _sDefense, _move1_PP, _move2_PP, _move3_PP, _move4_PP, _healthCurrent;
        private float _battle_time, _frame_time, _hyper_interval, _alpha, _text_time, _defenseCurlEffect;
        public enum Move
        {
            defenseCurl, bodyPress, hyperBeam, headbutt, none
        }
        public enum Text
        {
            defenseCurl, bodyPress, hyperBeam, headbutt, none
        }
        private Move _currentMove;
        private Text _text;
        public Snorlax(Texture2D texture, Texture2D hyperTexture, Texture2D impactTexture, Texture2D defenseCurl, Rectangle location)
        {
            _texture = texture;
            _hyperTexture = hyperTexture;
            _impactTexture = impactTexture;
            _defenseTexture = defenseCurl;
            _location = location;
            _move1 = "Headbutt";
            _move2 = "Body Press";
            _move3 = "Hyper Beam";
            _move4 = "Defense Curl";
            _move_type = "Normal";
            _name = "SNORLAX";
            _move1_PP = 15;
            _move2_PP = 10;
            _move3_PP = 5;
            _move4_PP = 30;
            _hyper_interval = 0.03f;
            Random stats = new Random();
            _speed = stats.Next(20, 41);
            _health = stats.Next(150, 171);
            _defense = stats.Next(50, 81);
            _attack = stats.Next(100, 121);
            _sDefense = stats.Next(100, 121);
            _healthCurrent = _health;
            _hyper_Location = new Rectangle(300, 300, 150, 300);
            _enemy_Location = new Rectangle(610, 90, 300, 300);
            _enemy_Hitbox = new Rectangle(800, 90, 200, 200);
            _hyper_hit = false;
            _canAct = true;
            _defenseBoost = false;
            _currentMove = Move.none;
            _text = Text.none;
            _defenseCurlEffect = (float)(_defense * 0.67);
        }

        public void Update(GameTime gametime)
        {
            if (_currentMove == Move.headbutt)
            {
                _canAct = false;
                _text = Text.headbutt;
                _text_time += (float)gametime.ElapsedGameTime.TotalSeconds;
                _battle_time += (float)gametime.ElapsedGameTime.TotalSeconds;
                _frame_time += (float)gametime.ElapsedGameTime.TotalSeconds;
                if (_frame_time <= 0.5f)
                {
                    _location.X += 15;
                    _location.Y -= 10;
                }
                else if (_frame_time <= 1f)
                {
                    _location.X -= 15;
                    _location.Y += 10;
                }
                else if (_battle_time >= 3f && _text_time >= 3)
                {
                    _currentMove = Move.none;
                    _text = Text.none;
                    _location.X = 120;
                    _location.Y = 283;
                    _battle_time = 0;
                    _frame_time = 0;
                    _text_time = 0;
                    _canAct = true;
                }
            }
            if (_currentMove == Move.bodyPress)
            {
                _canAct = false;
                _text = Text.bodyPress;
                _text_time += (float)gametime.ElapsedGameTime.TotalSeconds;
                _battle_time += (float)gametime.ElapsedGameTime.TotalSeconds;
                _frame_time += (float)gametime.ElapsedGameTime.TotalSeconds;
                if (_frame_time <= 0.5f)
                {
                    _location.Y -= 20;
                }
                else if (_frame_time <= 0.7f)
                {
                    _location.X = 610;
                }
                else if (_frame_time <= 1f)
                {
                    _location.Y += 20;
                }
                else if (_frame_time >= 1.3 && _text_time >= 3)
                {
                    _currentMove = Move.none;
                    _location.X = 120;
                    _location.Y = 283;
                    _currentMove = Move.none;
                    _text = Text.none;
                    _battle_time = 0;
                    _frame_time = 0;
                    _text_time = 0;
                    _canAct = true;
                }
            }
            if (_currentMove == Move.hyperBeam)
            {
                _canAct = false;
                _text = Text.hyperBeam;
                _text_time += (float)gametime.ElapsedGameTime.TotalSeconds;
                _battle_time += (float)gametime.ElapsedGameTime.TotalSeconds;
                _frame_time += (float)gametime.ElapsedGameTime.TotalSeconds;
                if (!_hyper_hit && _frame_time >= _hyper_interval)
                {
                    _hyper_Location.Width += 10;
                    _hyper_Location.Y -= 3;
                    _frame_time = 0;
                }
                if (_hyper_Location.Intersects(_enemy_Hitbox))
                {
                    _hyper_hit = true;
                    _impact_Location = new Rectangle(_enemy_Location.X, _enemy_Location.Y, 300, 300);
                }
                if (_battle_time >= 1.5 && _text_time >= 3)
                {
                    _currentMove = Move.none;
                    _text = Text.none;
                    _frame_time = 0;
                    _battle_time = 0;
                    _text_time = 0;
                    _hyper_hit = false;
                    _hyper_Location.Width = 150;
                    _hyper_Location.Y = 300;
                    _canAct = true;
                }
            }
            if (_currentMove == Move.defenseCurl)
            {
                _canAct = false;
                _text = Text.defenseCurl;
                _text_time += (float)gametime.ElapsedGameTime.TotalSeconds;
                _battle_time += (float)gametime.ElapsedGameTime.TotalSeconds;
                if (!_defenseBoost)
                {
                    _defense += (int)_defenseCurlEffect;
                    _defenseBoost = true;
                }
                if (_battle_time >= 2 && _text_time >= 3)
                {
                    _currentMove = Move.none;
                    _text = Text.none;
                    _battle_time = 0;
                    _text_time = 0;
                    _canAct = true;
                    _defenseBoost = false;
                }
            }
            if (_healthCurrent == 0)
            {
                _battle_time += (float)gametime.ElapsedGameTime.TotalSeconds;
                if (_battle_time >= 1)
                    _location.Y += 10;
                if (_battle_time >= 5)
                {
                    _location.Y = 283;
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
            get { return _text; }
            set { _text = value; }
        }
        public bool CanAct
        {
            get { return _canAct; }
            set { _canAct = value; }
        }
        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }
        public int Speed
        {
            get { return _speed; }
        }
        public int Attack
        {
            get { return _attack; }
        }
        public int Defense
        {
            get { return _defense; }
        }
        public int SDefense
        {
            get { return _sDefense; }
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
        public string MoveType
        {
            get { return _move_type; }
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
        public float TextTime
        {
            get { return _text_time; }
        }
        public string Name
        {
            get { return _name; }
        }
        public int HealthCurrent
        {
            get { return _healthCurrent; }
            set { _healthCurrent = value; }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (_currentMove == Move.hyperBeam)
            {
                spriteBatch.Draw(_hyperTexture, _hyper_Location, Color.White);
                if (_hyper_hit)
                {
                    spriteBatch.Draw(_impactTexture, _impact_Location, Color.White);
                }
            }
            spriteBatch.Draw(_texture, _location, Color.White);
            if (_currentMove == Move.defenseCurl)
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
                spriteBatch.Draw(_defenseTexture, _location, Color.White * _alpha);
            }
        }
    }
}