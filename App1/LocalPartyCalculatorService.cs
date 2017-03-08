using System;
using System.Collections.Generic;
using System.Linq;
using PartyCalculator.PartyWebServices;

namespace PartyCalculator
{
    class LocalPartyCalculatorService : IPartyCalculatorService
    {
        private static Random random = new Random();
        private static IList<WorkFlow> workFlows = new List<WorkFlow>();
        private IDictionary<int, IEnumerable<expense>> workflowExpenses = new Dictionary<int, IEnumerable<expense>>();
        public void AddExpenseToWorkFlow(int workflowId, expense expense)
        {
            if (workflowExpenses.ContainsKey(workflowId))
            {
                workflowExpenses[workflowId].ToList().Add(expense);
            }
            workflowExpenses.Add(workflowId,new List<expense>() {expense});
        }

        public void AddUserToWorkflow(user user, int workflowIdValue)
        {
            var partcipants = workFlows.FirstOrDefault(w => w.WorkflowId.Equals(workflowIdValue)).Participants;
            if(partcipants == null) partcipants = new string[0];
            var oldParticipant = partcipants;
            partcipants = new string[partcipants.Length + 1];
            oldParticipant.CopyTo(partcipants,0);
            partcipants.ToList().Add(user.userName);
        }

        public void EndWorkflow(int workflowId)
        {
            workFlows.Remove(workFlows.FirstOrDefault(w => w.WorkflowId.Equals(workflowId)));
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
            workFlows.Add(new WorkFlow(serviceWorkflow));
        }

        public IEnumerable<WorkFlow> GetWorkFlows()
        {
            return workFlows;
        }

        IEnumerable<Expense> IPartyCalculatorService.GetAllExpenses(int workflowIdValue)
        {
            return workflowExpenses[workflowIdValue].Select(e=>new Expense(e.user.userName,e.expenseType,e.description,e.amount));
        }

        public int? GetWorkFlowId()
        {
            return random.Next();
        }
    }
}