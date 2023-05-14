using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;

namespace CuratorEsstu.JobsQuatz
{
    public class JobFactory : IJobFactory
    {
        protected readonly IServiceScopeFactory _serviceScope;
        
        public JobFactory(IServiceScopeFactory serviceScope)
        {
            _serviceScope = serviceScope;
        }
        
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            using (var scope = _serviceScope.CreateScope())
            {
                var job = scope.ServiceProvider.GetService(bundle.JobDetail.JobType) as IJob;
                return job;
            }
        }

        public void ReturnJob(IJob job)
        {
            
        }
    }
}
