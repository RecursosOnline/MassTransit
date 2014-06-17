// Copyright 2007-2014 Chris Patterson, Dru Sellers, Travis Smith, et. al.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace MassTransit.Transports.RabbitMq
{
    using System.Collections.Generic;


    public class RabbitMqReceiveConsumerSettings :
        ReceiveConsumerSettings
    {
        public RabbitMqReceiveConsumerSettings()
        {
            QueueArguments = new Dictionary<string, object>();
            ExchangeArguments = new Dictionary<string, object>();
            ExchangeType = RabbitMQ.Client.ExchangeType.Fanout;
        }

        public string QueueName { get; set; }
        public string ExchangeName { get; set; }
        public ushort PrefetchCount { get; set; }
        public bool Durable { get; set; }
        public bool Exclusive { get; set; }
        public bool AutoDelete { get; set; }
        public IDictionary<string, object> QueueArguments { get; private set; }
        public IDictionary<string, object> ExchangeArguments { get; private set; }
        public bool PurgeOnReceive { get; set; }
        public string ExchangeType { get; set; }
    }
}