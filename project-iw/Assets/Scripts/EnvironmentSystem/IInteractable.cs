namespace CodeNameIW.EnvironmentSystem
{
    public interface IInteractable
    {
        public int InteractionID { get; }
        
        public void Interact();
        public void Disconnect();
    }
}