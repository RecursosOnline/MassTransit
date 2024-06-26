﻿namespace MassTransit.Tests.SagaStateMachineTests.Automatonymous
{
    using System;
    using System.Threading.Tasks;
    using NUnit.Framework;


    [TestFixture]
    public class Serializing_a_state_instance
    {
        [Test]
        public async Task Should_properly_handle_the_state_property()
        {
            var instance = new Instance();
            var machine = new InstanceStateMachine();

            await machine.RaiseEvent(instance, machine.Thing, new Data { Condition = true });
            Assert.That(instance.CurrentState, Is.EqualTo(machine.True));

            var serializer = new JsonStateSerializer<InstanceStateMachine, Instance>(machine);

            string body = serializer.Serialize(instance);

            Console.WriteLine("Body: {0}", body);
            var reInstance = serializer.Deserialize<Instance>(body);

            Assert.That(reInstance.CurrentState, Is.EqualTo(machine.True));
        }


        class Instance :
            SagaStateMachineInstance
        {
            public State CurrentState { get; set; }
            public Guid CorrelationId { get; set; }
        }


        class InstanceStateMachine :
            MassTransitStateMachine<Instance>
        {
            public InstanceStateMachine()
            {
                During(Initial,
                    When(Thing, context => context.Data.Condition)
                        .TransitionTo(True),
                    When(Thing, context => !context.Data.Condition)
                        .TransitionTo(False));
            }

            public State True { get; private set; }
            public State False { get; private set; }

            public Event<Data> Thing { get; private set; }
        }


        class Data
        {
            public bool Condition { get; set; }
        }
    }
}
