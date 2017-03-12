using System;
using System.Collections.Generic;
using System.Linq;
using PartyCalculator.PartyWebServices;

namespace PartyCalculator
{
    internal class LocalPartyCalculatorService : IPartyCalculatorService
    {
        private static readonly Random Random = new Random();
        private static readonly IList<WorkFlow> WorkFlows = new List<WorkFlow>();
        private readonly IDictionary<int, IEnumerable<expense>> _workflowExpenses = new Dictionary<int, IEnumerable<expense>>();

        public void AddExpenseToWorkFlow(int workflowId, expense expense)
        {
            if (_workflowExpenses.ContainsKey(workflowId))
            {
                _workflowExpenses[workflowId].ToList().Add(expense);
            }
            _workflowExpenses.Add(workflowId,new List<expense>() {expense});
        }

        public void AddUserToWorkflow(user user, int workflowIdValue)
        {
            var partcipants = WorkFlows.FirstOrDefault(w => w.WorkflowId.Equals(workflowIdValue)).Participants;
            if(partcipants == null) partcipants = new string[0];
            var oldParticipant = partcipants;
            partcipants = new string[partcipants.Length + 1];
            oldParticipant.CopyTo(partcipants,0);
            partcipants.ToList().Add(user.userName);
        }

        public void EndWorkflow(int workflowId)
        {
            WorkFlows.Remove(WorkFlows.FirstOrDefault(w => w.WorkflowId.Equals(workflowId)));
        }

        public void GetAllExpenses(int workflowIdValue)
        {
            throw new System.NotImplementedException();
        }

        public void StartWorkflow(int value, user user)
        {
            var serviceWorkflow = new workflow();
            serviceWorkflow.owner = user;
            serviceWorkflow.workflowId = value;
            WorkFlows.Add(new WorkFlow(serviceWorkflow));
        }

        public IEnumerable<WorkFlow> GetWorkFlows()
        {
            return WorkFlows;
        }

        IEnumerable<Expense> IPartyCalculatorService.GetAllExpenses(int workflowIdValue)
        {
            return _workflowExpenses[workflowIdValue].Select(e=>new Expense(e.user.userName,e.expenseType,e.description,e.amount));
        }

        public int? GetWorkFlowId()
        {
            return Random.Next();
        }
    }
}