using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    // 可序列化的条件类
    [System.Serializable]
    public class Condition
    {
        [SerializeField]
        Disjunction[] and; // 一个由"或"条件（Disjunction）构成的数组，它们之间的关系是"与"

        // 检查所有的"与"条件是否都满足
        public bool Check(IEnumerable<IPredicateEvaluator> evaluators)
        {
            foreach (Disjunction dis in and)
            {
                // 对于每个"与"条件，如果不满足，则整个Condition不满足，返回false
                if (!dis.Check(evaluators))
                {
                    return false;
                }
            }
            // 只有所有的"与"条件都满足，才返回true
            return true; 
        }

        // 可序列化的"或"条件类
        [System.Serializable]
        class Disjunction
        {
            [SerializeField]
            Predicate[] or; // 一个由谓词（Predicate）构成的数组，它们之间的关系是"或"

            // 检查所有的"或"条件，只要有一个满足，就返回true
            public bool Check(IEnumerable<IPredicateEvaluator> evaluators)
            {
                foreach (Predicate pred in or)
                {
                    // 对于每个"或"条件，如果满足，则整个Disjunction满足，返回true
                    if (pred.Check(evaluators))
                    {
                        return true; 
                    }
                }
                // 如果所有的"或"条件都不满足，返回false
                return false;
            }
        }

        // 可序列化的谓词类
        [System.Serializable]
        class Predicate
        {
            [SerializeField]
            string predicate; // 谓词的名称

            [SerializeField]
            string[] parameters; // 谓词的参数列表

            [SerializeField]
            bool negate = false; // 是否对谓词的结果取反

            // 检查谓词是否满足
            public bool Check(IEnumerable<IPredicateEvaluator> evaluators)
            {
                foreach (var evaluator in evaluators)
                {
                    // 通过评估器获取谓词的结果
                    bool? result = evaluator.Evaluate(predicate, parameters); 
                    
                    // 如果评估结果为null，则继续下一个评估器
                    if (result == null)
                    {
                        continue; 
                    }

                    // 如果谓词的结果与negate相等（例如，结果为false，negate也为false），则谓词不满足，返回false
                    if (result == negate) return false;
                }
                // 只有当所有的评估器都满足条件，才返回true
                return true; 
            }
        }
    }
}
