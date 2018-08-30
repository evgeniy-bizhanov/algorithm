using System;
using System.ComponentModel;

namespace Algorithms
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public partial class MilestoneAttribute : Attribute
    {
        public string Description
        {
            get
            {
                return description;
            }
        }

        private string description;

        public Milestones Milestone
        {
            get
            {
                return milestone;
            }
        }

        private Milestones milestone;

        [DefaultValue(false)]
        public bool WriteTimestampToConsole { get; set; } = false;

        [DefaultValue(true)]
        public bool WaitInput { get; set; } = true;

        public MilestoneAttribute(string description, Milestones milestone, params string[] args)
        {
            this.description = description;
            this.milestone = milestone;
        }
    }

    partial class MilestoneAttribute
    {
        public enum Milestones
        {
            Input,
            Output
        }
    }
}
