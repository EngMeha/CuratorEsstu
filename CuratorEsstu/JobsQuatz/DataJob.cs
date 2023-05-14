using InformationParser;
using Quartz;

namespace CuratorEsstu.JobsQuatz
{
    public class DataJob : IJob
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public DataJob(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            using(var scope = _serviceScopeFactory.CreateScope())
            {
                var parserWorker = scope.ServiceProvider.GetService<ParserWorker>();

                await parserWorker.Worker();
            }
        }
    }
}
