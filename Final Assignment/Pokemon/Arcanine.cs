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
        private Texture2D _Wtexture, _Otexture;
        private Rectangle _Olocation, _Wlocation;
        private bool _wild;
        private string _move1, _move2, _move3, _move4, _name;
        private int _speed, _health, _defense, _attack, _sAttack, _sDefense, _move1_PP, _move2_PP, _move3_PP, _move4_PP;
        private float _battle_time, _frame_time, _alpha;
        private int _healtCurrent;
        public enum Move
        {
            defenseCurl, bodyPress, hyperBeam, headbutt, none
        }
        private Move _currentMove;
        public Arcanine(Texture2D Wtexture, Texture2D Otexture, Rectangle Olocation, Rectangle Wlocation)
        {
            _Wtexture = Wtexture;
            _Otexture = Otexture;
            _Wlocation = Wlocation;
            _Olocation = Olocation;
            _move1 = "Flamethrower";
            _move2 = "Crunch";
            _move3 = "Fire Blast";
            _move4 = "Howl";
            _name = "ARCANINE";
            _move1_PP = 15;
            _move2_PP = 15;
            _move3_PP = 5;
            _move4_PP = 40;
            Random stats = new Random();
            _speed = stats.Next(80, 111);
            _health = stats.Next(80, 101);
            _healtCurrent = _health;
            _defense = stats.Next(70, 91);
            _attack = stats.Next(100, 121);
            _sAttack = stats.Next(90, 111);
            _sDefense = stats.Next(70, 91);
            _currentMove = Move.none;
            _wild = true;
        }

        public void Update(GameTime gametime)
        {
        }
        public Move CurrentMove
        {
            get { return _currentMove; }
            set { _currentMove = value; }
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
        public void Draw(SpriteBatch spriteBatch)
        {
            if (_wild)
            {
                spriteBatch.Draw(_Wtexture, _Wlocation, Color.White);
            }
            else
            {
                spriteBatch.Draw(_Otexture, _Olocation, Color.White);
            }
        }
    }
}
