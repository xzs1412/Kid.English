using Abp.Dependency;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Exceptions;
using Abp.Events.Bus.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kid.English
{
    /// <summary>
    /// 异常会触发的事件(包括BackgroundJob)
    /// </summary>
    public class MyExceptionHandler : IEventHandler<AbpHandledExceptionData>,
        ITransientDependency
    {
        public void HandleEvent(AbpHandledExceptionData eventData)
        {
            //TODO: Check eventData.Exception!
        }
    }
}