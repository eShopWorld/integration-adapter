using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace EDAIntegrationAdapter.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEventHandler
    {
        /// <summary>
        /// 
        /// </summary>
        string EventName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string Execute();
    }

    /// <summary>
    /// 
    /// </summary>
    public class EventHandlerFactory
    {
        private readonly ConcurrentDictionary<string, IEventHandler> _handlers;

        /// <summary>
        /// 
        /// </summary>
        public EventHandlerFactory()
        {
            _handlers = new ConcurrentDictionary<string, IEventHandler>();    
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventHandler"></param>
        public void Register(IEventHandler eventHandler)
        {
            if (!_handlers.TryAdd(eventHandler.EventName, eventHandler))
            {
                //todo blow up
            }
        }

        public void Get(string eventType)
        {
            if(!_handlers.TryGetValue(eventType, out var handler))
            {

            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class WebhookController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Post(JObject payload)
        {
            Request.Headers.TryGetValue(Constants.Headers.EswEventType, out var values);
            if (string.IsNullOrWhiteSpace(values))
            {
                throw new ArgumentNullException(Constants.Headers.EswEventType);
            }

            switch (Constants.Headers.EswEventType)
            {
                //to do something 
            }

            return Accepted();
        }

        /// <summary>
        /// Magic String Management
        /// </summary>
        public struct Constants
        {
            /// <summary>
            /// 
            /// </summary>
            public struct Headers
            {
                /// <summary>
                /// The public esw event type
                /// </summary>
                public const string EswEventType = "Esw-Event-Type";

                /// <summary>
                /// Default correlation passed around services for tracking
                /// </summary>
                public const string EswCorrelationId = "Esw-Delivery"; 

                /// <summary>
                /// Should be used to extend the timeout of the http client duration. If not set, it should default to defaults
                /// </summary>
                public const string RequestDuration = "Expected-Request-Duration";

                /// <summary>
                /// Standard Correlation Id passed around
                /// </summary>
                public const string CorrelationId = "X-Correlation-Id";
            }
        }
    }
}