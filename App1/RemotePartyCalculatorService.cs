using System.Collections.Generic;
using System.Linq;
using PartyCalculator.PartyWebServices;

namespace PartyCalculator
{
    internal class RemotePartyCalculatorService : IPartyCalculatorService
    {
        private readonly PartyCalculatorServiceClient partyCalculatorServiceClient = new PartyCalculatorServiceClient();
        public void AddExpenseToWorkFlow(int workflowId, expense expense)
        {
            var task = partyCalculatorServiceClient.addExpenseToWorkflowAsync(workflowId, expense);
            var r = task.Result;
        }

        public void AddUserToWorkflow(user user, int workflowIdValue)
        {
            var task = partyCalculatorServiceClient.addUserToWorkflowAsync(user, workflowIdValue);
            var r = task.Result;
        }

        public void EndWorkflow(int workflowId)
        {
            var task = partyCalculatorServiceClient.endWorkflowAsync(workflowId);
            var r = task.Result;
        }
        public void StartWorkflow(int value, user user)
        {
            var task = partyCalculatorServiceClient.startWorkflowAsync(value,user);
            var result =  task.Result;
        }

        public IEnumerable<WorkFlow> GetWorkFlows()
        {
            var task = partyCalculatorServiceClient.getWorkflowsAsync();
            return task.Result.@return.ToList().Select(wf=>new WorkFlow(wf));
        }

        IEnumerable<Expense> IPartyCalculatorService.GetAllExpenses(int workflowIdValue)
        {
            var task = partyCalculatorServiceClient.getAllExpensesAsync(workflowIdValue);
            return task.Result.@return.ToList().Select(exp=>new Expense(exp.user.userName,exp.expenseType,exp.description,exp.amount));
        }

        public int? GetWorkFlowId()
        {
            var task = partyCalculatorServiceClient.generateWorkflowIdAsync();
            return task.Result.@return;
        }
    }
}