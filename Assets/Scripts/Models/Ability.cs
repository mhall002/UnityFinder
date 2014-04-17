using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    public class Ability
    {
        public string Name;
        public int UsageLimit;
        public LimitType UsageLimitType;
        public int Uses;
        public string Description;
    }

    public enum LimitType
    {
        Daily
    }
}
