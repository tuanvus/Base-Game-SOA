
using System;
using System.Runtime.Serialization;
using UnityEngine;


namespace LTA.Error
{
    public class WarningException : Exception
    {
        ProblemData problemData;

        protected virtual ProblemData DefaultProblemData
        {
            get
            {
                return new ProblemData(Severity.Major, ScopeType.Framework);
            }
        }

        public ProblemData ProblemData
        {
            set
            {
                problemData = value;
            }
            get
            {
                if (problemData == null)
                    problemData = DefaultProblemData;
                return problemData;
            }
        }

        public override string Message
        {
            get
            {

                return ProblemData.message;
            }
        }


        public virtual void SetMessage(string message)
        {
            ProblemData.message = message;
        }

        protected WarningException()
        {

        }

        public WarningException(string message)
        {
            SetMessage(message);
        }

        protected WarningException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }

    public class ErrorException : WarningException
    {

        protected override ProblemData DefaultProblemData
        {
            get
            {
                return new ProblemData(Severity.Critical, ScopeType.Framework);
            }
        }

        public override string Message
        {
            get
            {

                return ProblemData.message;
            }
        }


        public override void SetMessage(string message)
        {
            base.SetMessage(message);
            ErrorHelper.ErrorHandle(this);
        }

        protected ErrorException()
        {
            
        }

        public ErrorException(string message)
        {
            SetMessage(message);
        }

        protected ErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }

    public class NullReferenceException<T> : ErrorException
    {
        public NullReferenceException(T variable)
        {
            SetMessage(Init("variable with type is " + typeof(T).FullName, typeof(T).FullName));
        }


        protected string Init(string propertyName, string valueName)
        {
            return String.Format("{0} is null.Please Assigned to {0} by {1} In Your Project", propertyName, valueName);
        }

        protected override ProblemData DefaultProblemData
        {
            get
            {
                return new ProblemData(Severity.Critical, ScopeType.Project);
            }
        }

    }
}
