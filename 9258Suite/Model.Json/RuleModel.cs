using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YoYoStudio.Model;

namespace YoYoStudio.Model.Json
{
    [Serializable]
    public class Rule
    {
        public string field { get; set; }
        public string op { get; set; }
        public string value { get; set; }
        public string type { get; set; }

        public static string GetOperator(string op)
        {
            switch (op)
            {
                case "equal":
                    return " = ";
                case "notequal":
                    return " <> ";
                case "greater":
                    return " > ";
                case "greaterorequal":
                    return " >= ";
                case "less":
                    return " < ";
                case "lessorequal":
                    return " <= ";
                case "and":
                    return " AND ";
                case "or":
                    return " OR ";
                default:
                    return " ";
            }
        }
        public string GetCondition()
        {
            return "("+field + GetOperator(op) + value +")";
        }
    }
    [Serializable]
    public class RuleModel
    {
        public List<Rule> rules { get; set; }
        public List<RuleModel> groups { get; set; }
        public string op { get; set; }

        public string GetCondition()
        {
            string condition = "";
            if (rules != null)
            {
                foreach (Rule r in rules)
                {
                    if (string.IsNullOrEmpty(condition))
                    {
                        condition = r.GetCondition();
                    }
                    else
                    {
                        condition = condition + " AND " + r.GetCondition();
                    }
                }
                if (!string.IsNullOrEmpty(condition) && rules.Count >1)
                {
                    condition = "(" + condition + ")";
                }
            }

            string groupCondition = "";
            if (groups != null)
            {
                foreach (var group in groups)
                {
                    if (string.IsNullOrEmpty(groupCondition))
                    {
                        groupCondition = group.GetCondition();
                    }
                    else
                    {
                        groupCondition = groupCondition + " AND " + group.GetCondition();
                    }
                }
            }
            if (!string.IsNullOrEmpty(groupCondition))
            {
                groupCondition = "(" + groupCondition + ")";
            }
            if (string.IsNullOrEmpty(condition))
            {
                return groupCondition;
            }
            else if (string.IsNullOrEmpty(groupCondition))
            {
                return condition;
            }
            return "(" + condition + Rule.GetOperator(op) + groupCondition + ")";
        }
    }
    
}