﻿// Copyright 2007-2014 Chris Patterson, Dru Sellers, Travis Smith, et. al.
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
namespace MassTransit.SubscriptionBuilders
{
    using Pipeline;
    using Policies;
    using SubscriptionConnectors;
    using Subscriptions;


    public class InstanceSubscriptionBuilder :
        SubscriptionBuilder
    {
        readonly InstanceConnector _connector;
        readonly object _instance;
        readonly ReferenceFactory _referenceFactory;
        readonly IRetryPolicy _retryPolicy;

        public InstanceSubscriptionBuilder(object instance, IRetryPolicy retryPolicy, ReferenceFactory referenceFactory)
        {
            _instance = instance;
            _referenceFactory = referenceFactory;
            _retryPolicy = retryPolicy;

            _connector = InstanceConnectorCache.GetInstanceConnector(instance.GetType());
        }

        public ISubscriptionReference Subscribe(IConsumePipe pipe)
        {
            ConnectHandle handle = _connector.Connect(pipe, _instance, _retryPolicy);

            return _referenceFactory(handle);
        }
    }
}