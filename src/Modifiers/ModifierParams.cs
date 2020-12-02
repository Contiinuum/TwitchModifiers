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
            public bool showOnScoreOverlay;
            public bool countdownEnabled;
            public float cooldownBetweenModifiers;
            public int maxActiveModifiers;
            public bool showModStatus;

            public General(bool _countdownEnabled, bool _showOnScoreOverlay, float _cooldownBetweenModifiers, bool _enableTwitchModifiers, int _maxActiveModifiers, bool _showModStatus)
            {
                enableTwitchModifiers = _enableTwitchModifiers;
                countdownEnabled = _countdownEnabled;
                cooldownBetweenModifiers = _cooldownBetweenModifiers;
                maxActiveModifiers = _maxActiveModifiers;
                showModStatus = _showModStatus;
                showOnScoreOverlay = _showOnScoreOverlay;
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

        public struct BetterMelees
        {
            public bool enabled;
            public float duration;
            public float cooldown;

            public BetterMelees(bool _enabled, float _duration, float _cooldown)
            {
                enabled = _enabled;
                duration = _duration;
                cooldown = _cooldown;
            }
        }
        public struct Wobble
        {
            public bool enabled;
            public float duration;
            public float cooldown;
            public float minSpeed;
            public float maxSpeed;

            public Wobble(bool _enabled, float _duration, float _cooldown, float _minSpeed, float _maxSpeed)
            {
                duration = _duration;
                cooldown = _cooldown;
                enabled = _enabled;
                minSpeed = _minSpeed;
                maxSpeed = _maxSpeed;
            }
        }
        public struct RandomColors
        {
            public bool enabled;
            public float duration;
            public float cooldown;
            public RandomColors(bool _enabled, float _duration, float _cooldown)
            {
                enabled = _enabled;
                duration = _duration;
                cooldown = _cooldown;
                
            }
        }
        public struct ColorSwap
        {
            public bool enabled;
            public float duration;
            public float cooldown;
            public ColorSwap(bool _enabled, float _duration, float _cooldown)
            {
                enabled = _enabled;
                duration = _duration;
                cooldown = _cooldown;

            }
        }

        public struct RandomOffset
        {
            public bool enabled;
            public float duration;
            public float cooldown;
            public float minOffsetX;
            public float maxOffsetX;
            public float minOffsetY;
            public float maxOffsetY;

            public RandomOffset(bool _enabled, float _duration, float _cooldown, float _minoffsetX, float _maxOffsetX, float _minOffsetY, float _maxOffsetY)
            {
                enabled = _enabled;
                duration = _duration;
                cooldown = _cooldown;
                minOffsetX = _minoffsetX;
                maxOffsetX = _maxOffsetX;
                minOffsetY = _minOffsetY;
                maxOffsetY = _maxOffsetY;
            }
        }

        public struct Scale
        {
            public bool enabled;
            public float duration;
            public float cooldown;
            public float minScale;
            public float maxScale;

            public Scale(bool _enabled, float _duration, float _cooldown, float _minScale, float _maxScale)
            {
                enabled = _enabled;
                duration = _duration;
                cooldown = _cooldown;
                minScale = _minScale;
                maxScale = _maxScale;
            }
        }

        public struct Wooble
        {
            public bool enabled;
            public float duration;
            public float cooldown;

            public Wooble(bool _enabled, float _duration, float _cooldown)
            {
                enabled = _enabled;
                duration = _duration;
                cooldown = _cooldown;
            }
        }
    }

}
