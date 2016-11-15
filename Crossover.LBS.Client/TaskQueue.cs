using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crossover.LBS.Client
{
    public class TaskQueue
    {
        private readonly BackgroundWorker _processor = new BackgroundWorker();

        private ConcurrentQueue<Action> Tasks { get; set; }

        public TaskQueue()
        {
            Tasks = new ConcurrentQueue<Action>();
            _processor.WorkerSupportsCancellation = true;
            _processor.DoWork += (s, args) =>
            {
                while (!_processor.CancellationPending)
                {
                    Action task = null;
                    if (this.Tasks.TryDequeue(out task))
                    {
                        task();
                    }
                }
            };

            _processor.RunWorkerAsync();
        }

        public void ScheduleTask(Action task)
        {
            this.Tasks.Enqueue(task);
        }
    }
}
