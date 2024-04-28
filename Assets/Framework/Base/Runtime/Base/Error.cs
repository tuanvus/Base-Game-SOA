using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LTA.Error
{
    public class ErrorHelper
    {
        public static void ErrorHandle(ErrorException error)
        {
            Debug.LogError(error.Message);
        }
    }
    public enum Severity
    {
        Critical,
        Major,
        Minior,
        Low
    }

    public enum ScopeType
    {
        Framework,
        Project,
        Data
    }
    [System.Serializable]
    public class ProblemData
    {

        public Severity severity;
        public ScopeType type;
        public string message = null;
        public IReason reason;

        public ProblemData(Severity severity, ScopeType type)
        {
            this.severity = severity;
            this.type = type;
        }
        //public override string ToString()
        //{
        //    return JsonUtility.ToJson(this);
        //}

        //public override bool Equals(object obj)
        //{
        //    return base.Equals(obj);
        //}

        //public override int GetHashCode()
        //{
        //    return base.GetHashCode();
        //}
        //public static bool operator == (ProblemData info1, ProblemData info2)
        //{
        //    if (info2 is null) return info1 is null;
        //    if (info1.reason == null) return info2.reason == null;
        //    return info1.name == info2.name && info1.level == info2.level;
        //}

        //public static bool operator !=(ProblemData info1, ProblemData info2)
        //{
        //    if (info2 is null) return !(info1 is null);
        //    return info1.name != info2.name || info1.level != info2.level;
        //}
    }

    public interface IReason
    {
        GameObject EntityReason { get; }
        MonoBehaviour ComponentReason { get; }
        object DataReason { get; }
    }

    public interface ISolveProblem
    {
        bool OnSolveProblem(ProblemData problemData,bool test = false);
    }

    public class SolutionHelper
    {

        public static bool SolveProblem(ProblemData problemData)
        {
            Debug.LogError("Error " + problemData.ToString() + " " + problemData.message);
            return false;
        }
    }
}
