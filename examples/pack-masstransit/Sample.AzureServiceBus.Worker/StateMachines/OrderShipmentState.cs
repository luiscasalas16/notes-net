namespace Sample.Worker.StateMachines
{
    using System;
    using MassTransit;

    public class OrderShipmentState : SagaStateMachineInstance
    {
        public string CurrentState { get; set; }

        public Guid? MonitorTimeoutTokenId { get; set; }

        public Guid CorrelationId { get; set; }
    }
}
