using System.Collections.Generic;
using PartyCalculator.PartyWebServices;

namespace PartyCalculator
{
    public interface IPartyCalculatorService
    {
        void AddExpenseToWorkFlow(int workflowId, expense expense);
        void AddUserToWorkflow(user user, int workflowIdValue);
        void EndWorkflow(int workflowId);
        IEnumerable<Expense> GetAllExpenses(int workflowIdValue);
        void StartWorkflow(int value, user user);
        IEnumerable<WorkFlow> GetWorkFlows();

        int? GetWorkFlowId();
    }
}