using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    [System.Serializable]
    public class Condition
    {
        [SerializeField]
        Disjunction[] and; // 与条件的数组

        public bool Check(IEnumerable<IPredicateEvaluator> evaluators)
        {
            foreach (Disjunction dis in and)
            {
                if (!dis.Check(evaluators))
                {
                    return false; // 如果有任何一个与条件不满足，返回false
                }
            }
            return true; // 所有与条件都满足，返回true
        }

        [System.Serializable]
        class Disjunction
        {
            [SerializeField]
            Predicate[] or; // 或条件的数组

            public bool Check(IEnumerable<IPredicateEvaluator> evaluators)
            {
                foreach (Predicate pred in or)
                {
                    if (pred.Check(evaluators))
                    {
                        return true; // 如果有任何一个或条件满足，返回true
                    }
                }
                return false; // 所有或条件都不满足，返回false
            }
        }

        [System.Serializable]
        class Predicate
        {
            [SerializeField]
            string predicate; // 谓词名称
            [SerializeField]
            string[] parameters; // 谓词参数
            [SerializeField]
            bool negate = false; // 是否取反

            public bool Check(IEnumerable<IPredicateEvaluator> evaluators)
            {
                foreach (var evaluator in evaluators)
                {
                    bool? result = evaluator.Evaluate(predicate, parameters); // 评估谓词结果
                    if (result == null)
                    {
                        continue; // 如果结果为null，继续下一个评估器
                    }

                    if (result == negate) return false; // 根据取反情况判断谓词是否满足条件
                }
                return true; // 所有评估器都满足条件，返回true
            }
        }
    }
}
