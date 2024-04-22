namespace RabbitMQ.EventBus.Helper
{
    public class EnumEventBusErrorMessage
    {
        public EnumEventBusErrorMessage(string message)
        {
            this.message = message;
        }

        public string message { get; set; }

        public static EnumEventBusErrorMessage ConnectionProblem = new EnumEventBusErrorMessage("Connection error.");
        public static EnumEventBusErrorMessage ConnectionFail = new EnumEventBusErrorMessage("The connection can't be created.");
        public static EnumEventBusErrorMessage ConnectionBlocked = new EnumEventBusErrorMessage("Blocked connection, trying to reconnect.");
        public static EnumEventBusErrorMessage ConnectionClosed = new EnumEventBusErrorMessage("Connection finished, trying to reconnect");
        public static EnumEventBusErrorMessage NoValidConnectionToCreateModel = new EnumEventBusErrorMessage("No valid connection found.");
        public static EnumEventBusErrorMessage TheResourcesUsedDeleted = new EnumEventBusErrorMessage("The resources used has been deleted");
    }
}