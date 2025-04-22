using UnityEngine;

namespace RPG.Core
{
    public interface IPredicateEvaluate
    {
        bool? Evaluate(string predicate, string[] parameters);
    }
}
