using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudicaModding
{
    public struct ModifierParams
    {
        public struct Default
        {
            public string name;
            public float duration;
            public float cooldown;
            public bool active;
            public string user;

            public Default(string _name, string _user)
            {
                name = _name;
                user = _user;
                duration = 0;
                cooldown = 0;
                active = false;
            }
        }

        public struct General
        {
            public bool enableTwitchModifiers;
            public bool countdownEnabled;
            public float cooldownBetweenModifiers;

            public General(bool _countdownEnabled, float _cooldownBetweenModifiers, bool _enableTwitchModifiers)
            {
                enableTwitchModifiers = _enableTwitchModifiers;
                countdownEnabled = _countdownEnabled;
                cooldownBetweenModifiers = _cooldownBetweenModifiers;
            }
        }

        public struct Speed
        {
            public bool enabled;
            public float duration;
            public float cooldown;
            public float minSpeed;
            public float maxSpeed;          

            public Speed(bool _enabled, float _duration, float _cooldown, float _minSpeed, float _maxSpeed)
            {
                maxSpeed = _maxSpeed;
                minSpeed = _minSpeed;
                duration = _duration;
                cooldown = _cooldown;
                enabled = _enabled;
            }
        }

        public struct AimAssist
        {
            public bool enabled;
            public float duration;
            public float cooldown;
            public float minAimAssist;
                               
            public AimAssist(bool _enabled, float _duration, float _cooldown, float _minAllowedAimAssist)
            {
                minAimAssist = _minAllowedAimAssist;
                duration = _duration;
                cooldown = _cooldown;
                enabled = _enabled;
            }
        }

        public struct Psychedelia
        {
            public bool enabled;
            public float duration;
            public float cooldown;
            public float minPsychadeliaSpeed;
            public float maxPsychadeliaSpeed;

            public Psychedelia(bool _enabled, float _duration, float _cooldown, float _minPsychadeliaSpeed, float _maxPsychadeliaSpeed)
            {
                duration = _duration;
                cooldown = _cooldown;
                maxPsychadeliaSpeed = _maxPsychadeliaSpeed;
                minPsychadeliaSpeed = _minPsychadeliaSpeed;
                enabled = _enabled;
            }
        }
         
        public struct Mines
        {
            public bool enabled;
            public float duration;
            public float cooldown;           

            public Mines(bool _enabled, float _duration, float _cooldown)
            {
                duration = _duration;
                cooldown = _cooldown;
                enabled = _enabled;
            }
        }
        public struct InvisGuns
        {
            public bool enabled;
            public float duration;
            public float cooldown;
           
            public InvisGuns(bool _enabled, float _duration, float _cooldown)
            {
                duration = _duration;
                cooldown = _cooldown;
                enabled = _enabled;
            }
        }

        public struct Particles
        {
            public bool enabled;
            public float duration;
            public float cooldown;          
            public float minParticles;
            public float maxParticles;

            public Particles(bool _enabled, float _duration, float _cooldown, float _minParticles, float _maxParticles)
            {
                duration = _duration;
                cooldown = _cooldown;
                minParticles = _minParticles;
                maxParticles = _maxParticles;
                enabled = _enabled;
            }
        }

        public struct ZOffset
        {
            public bool enabled;
            public float duration;
            public float cooldown;           
            public float minZOffset;
            public float maxZOffset;

            public ZOffset(bool _enabled, float _duration, float _cooldown, float _minZOffset, float _maxZOffset)
            {
                duration = _duration;
                cooldown = _cooldown;
                minZOffset = _minZOffset;
                maxZOffset = _maxZOffset;
                enabled = _enabled;
            }
        }

        public struct Shift
        {
            public bool enabled;
            public float duration;
            public float cooldown;
            public float minShift;
            public float maxShift;

            public Shift(bool _enabled, float _duration, float _cooldown, float _minShift, float _maxShift)
            {
                enabled = _enabled;
                duration = _duration;
                cooldown = _cooldown;
                minShift = _minShift;
                maxShift = _maxShift;
            }
        }
        public struct Wobble
        {
            public bool enabled;
            public float duration;
            public float cooldown;        

            public Wobble(bool _enabled, float _duration, float _cooldown)
            {
                duration = _duration;
                cooldown = _cooldown;
                enabled = _enabled;
            }
        }
        public struct RandomColors
        {
            public float duration;
            public float cooldown;
            public bool enabled;

            public RandomColors(float _duration, float _cooldown, bool _enabled)
            {
                duration = _duration;
                cooldown = _cooldown;
                enabled = _enabled;
            }
        }
    }

}
