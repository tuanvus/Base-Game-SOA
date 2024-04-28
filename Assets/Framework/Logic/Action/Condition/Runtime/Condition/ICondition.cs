using System;
namespace LTA.Condition
{
    public interface ICondition
    {
        bool IsSuitable { get; }
        Action<ICondition> SuitableCondition { set; }
    }
}
