using Fungus;
using UnityEngine;

namespace MDI.Gamekit.Interactables
{
    public class ExamineInteractable : IInteractable, IExaminable
    {
        Flowchart examineFlow;

        public ExamineInteractable(Flowchart examineFlow)
        {
            this.examineFlow = examineFlow;
        }

        public void Examine(string flowBlockName)
        {
            examineFlow.ExecuteBlock(flowBlockName);
        }
    }
}