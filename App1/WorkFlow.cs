using PartyCalculator.PartyWebServices;
using System;

namespace PartyCalculator
{
    public class User
    {
        private readonly user user;

        public User(user user)
        {
            this.user = user;
            userName = user.userName;
        }
        private string userName;
        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
            }
        }

        public user ToServiceUser()
        {
            return user;
        }
    }
    public class ExistingWorkFlow
    {
        private int workflowId;
        private string role;
        private user userName;
        public ExistingWorkFlow(int workflowId, user userName,string role)
        {
            this.workflowId = workflowId;
            this.UserName = userName;
            this.role = role;

        }
        public int WorkflowId
        {
            get
            {
                return workflowId;
            }

            set
            {
                workflowId = value;
            }
        }

        public string Role
        {
            get
            {
                return role;
            }

            set
            {
                role = value;
            }
        }

        public user UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
            }
        }

        public override string ToString()
        {
            return WorkflowId + "  " + Role + "  " + UserName.userName;
        }
    }
    public class WorkFlow
    {
        private User owner;
        private int workflowId;
        private string[] participants;

        public WorkFlow(workflow workflow)
        {
            owner = new User(workflow.owner);
            workflowId = workflow.workflowId;
            participants = null;
        }
        public User Owner
        {
            get
            {
                return owner;
            }

            set
            {
                owner = value;
            }
        }

        public int WorkflowId
        {
            get
            {
                return workflowId;
            }

            set
            {
                workflowId = value;
            }
        }

        public string[] Participants
        {
            get
            {
                return participants;
            }

            set
            {
                participants = value;
            }
        }
    }
}
