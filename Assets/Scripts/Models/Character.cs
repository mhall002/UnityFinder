using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Assets.Scripts.Models
{
    public class Character : Entity, INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;

		private int hp;
        public int HP
		{
			set
			{
				hp = HP;
				OnPropertyChanged("HP");
			}
			get
			{
				return hp;
			}
		}
		private int xp;
		public int XP
		{
			set
			{
				xp = XP;
				OnPropertyChanged("XP");
			}
			get
			{
				return xp;
			}
		}
		private int level;
        public int Level
		{
			set
			{
				xp = XP;
				OnPropertyChanged("Level");
			}
			get
			{
				return level;
			}
		}
		private Dictionary<StatName, int> Stats = new Dictionary<StatName, int>();

		public int GetStat(StatName statName)
		{
			return Stats[statName];
		}

		public void SetStat(StatName stat, int value)
		{
			Stats[stat] = value;
			OnPropertyChanged("Stat[" + stat.ToString() + "]");
		}

        public List<State> States;
        public List<Ability> Abilities;

		protected void OnPropertyChanged(string name)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(name));
			}
		}
    }

	public enum StatName
	{
		Strength = 0,
		Dexterity = 1,
		Constitution = 2,
		Intelligence = 3,
		Wisdom = 4,
		Charisma = 5,
		STR = 6,
		DEX = 7,
		CON = 8,
		INT = 9,
		WIS = 10,
		CHA = 11,
		Acrobatics = 12,
		Bluff = 13,
		Climb = 14,
		Diplomacy = 15,
		DisableDevice = 16,
		Heal = 17,
		Arcana = 18,
		Dungeoneering = 19,
		Geography = 20,
		History = 21,
		Local = 22,
		Nature = 23,
		Religion = 24,
		Perception = 25,
		Ride = 26,
		SenseMotive = 27,
		SpellCraft = 28,
		Stealth = 29,
		Swim = 30,
		FortSave = 31,
		ReflexSave = 32,
		WillSave = 33,
		ClassAttack = 34,
		SkillRanks = 35,
		Initiative = 36,
		MeleeAttack = 37,
		RangedAttack = 38,
		WeaponAttack1 = 39,
		WeaponAttack2 = 40,
		ArmorClass = 41
	}

	public enum StatType
	{
		AbilityScore,
		AbilityMod,
		Skill,
		Saves,
		ClassFeature,
		Combat
	}
}
