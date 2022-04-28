using Fungus;

namespace MDI.Gamekit.Interactables
{
    public class UseInteractable : IInteractable, IUseable{
        Flowchart useFlow;

        public UseInteractable(Flowchart examineFlow){
            this.useFlow = examineFlow;
        }

        public void Use(string flowBlockName){
            useFlow.ExecuteBlock(flowBlockName);
        }
    }
}