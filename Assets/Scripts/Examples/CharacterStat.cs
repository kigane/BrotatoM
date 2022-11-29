using System;
using System.Collections.Generic;
using System.Collections.ObjectModel; // for ReadOnlyCollection

public enum StatModType
{
    FLAT = 100,
    PERCENT_ADD = 200,
    PERCENT_MULT = 300
}

public class CharacterStat
{
    public float BaseValue;
    public float Value
    {
        get
        {
            // 增加modifiers，或修改基础值都需要重新计算。
            if (isDirty || lastBaseValue != BaseValue)
            {
                lastBaseValue = BaseValue;
                mValue = CalculateFinalValue();
                isDirty = false;
            }
            return mValue;
        }
    }

    private bool isDirty = true;
    private float lastBaseValue = float.MinValue;
    private float mValue;
    private readonly List<StatModifier> statModifiers;
    // 让statModifiers暴露出去，以便可以让玩家看到modifiers。
    // 但是不应该改变statModifiers的访问级别，因为statModifiers只应该由该类修改
    // 因此使用ReadOnlyCollection
    public readonly ReadOnlyCollection<StatModifier> StatModifiers;

    public CharacterStat(float baseValue)
    {
        BaseValue = baseValue;
        statModifiers = new();
        StatModifiers = statModifiers.AsReadOnly(); // ReadOnlyCollection
    }

    public void AddModifier(StatModifier mod)
    {
        isDirty = true;
        statModifiers.Add(mod);
        statModifiers.Sort(CompareModifierOrder);
    }

    private int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        if (a.Order < b.Order)
            return -1;
        else if (a.Order > b.Order)
            return 1;
        else
            return 0;
    }

    public bool RemoveModifier(StatModifier mod)
    {
        if (statModifiers.Remove(mod))
        {
            isDirty = true;
            return true;
        }
        return false;
    }

    private float CalculateFinalValue()
    {
        float finalValue = BaseValue;
        float sumPercentAdd = 0;

        for (int i = 0; i < statModifiers.Count; i++)
        {
            StatModifier mod = statModifiers[i];

            if (mod.Type == StatModType.FLAT)
            {
                finalValue += mod.Value;
            }
            else if (mod.Type == StatModType.PERCENT_ADD)
            {
                sumPercentAdd += mod.Value;

                if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PERCENT_ADD)
                {
                    finalValue *= 1 + sumPercentAdd;
                    sumPercentAdd = 0; // 感觉没必要
                }
            }
            else if (mod.Type == StatModType.PERCENT_MULT)
            {
                finalValue *= 1 + mod.Value;
            }
        }
        return (float)Math.Round(finalValue, 4);
    }

    public bool RemoveAllModifiersFromSource(object source)
    {
        bool didRemove = false;

        for (int i = statModifiers.Count - 1; i >= 0; i--)
        {
            if (statModifiers[i].Source == source)
            {
                isDirty = true;
                didRemove = true;
                statModifiers.RemoveAt(i);
            }
        }
        return didRemove;
    }
}

public class StatModifier
{
    public readonly float Value;
    public readonly StatModType Type;
    public readonly int Order;
    public readonly object Source; // 可以实现去除一个道具的所有增益的效果

    public StatModifier(float value, StatModType type, int order, object source)
    {
        Value = value;
        Type = type;
        Order = order;
        Source = source;
    }

    // 提供两参数的构造器
    public StatModifier(float value, StatModType type) : this(value, type, (int)type, null) { }

    // 提供三参数的构造器
    public StatModifier(float value, StatModType type, object source) : this(value, type, (int)type, source) { }
}

public class Character
{
    public CharacterStat Strength { get; }
}

public class Item
{
    public void Equip(Character c)
    {
        c.Strength.AddModifier(new StatModifier(10, StatModType.FLAT, this));
        c.Strength.AddModifier(new StatModifier(0.1f, StatModType.PERCENT_MULT, this));
    }
}

