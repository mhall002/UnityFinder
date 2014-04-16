using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    class Ability
    {
        public string Name;
        public int UsageLimit;
        public LimitType UsageLimitType;
        public int Uses;
        public string Description;
    }

    enum LimitType
    {
        Daily
    }
}
