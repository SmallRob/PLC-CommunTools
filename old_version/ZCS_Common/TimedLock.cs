using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZCS_Common
{
    /// <summary>
    /// 抽象类：资源锁
    /// </summary>
    public struct TimedLock : IDisposable
    {
        public static TimedLock Lock(object o)
        {
            return Lock(o, TimeSpan.FromSeconds(10));
        }

        public static TimedLock Lock(object o, TimeSpan timeout)
        {
            var tl = new TimedLock(o);
            if (!Monitor.TryEnter(o, timeout))
            {
#if DEBUG
                GC.SuppressFinalize(tl.leakDetector);
#endif
                throw new LockTimeoutException();
            }

            return tl;
        }

        private TimedLock(object o)
        {
            target = o;
#if DEBUG
            leakDetector = new Sentinel();
#endif
        }
        private object target;

        public void Dispose()
        {
            Monitor.Exit(target);

#if DEBUG
            GC.SuppressFinalize(leakDetector);
#endif
        }

#if DEBUG

        private class Sentinel
        {
            ~Sentinel()
            {
                Debug.Fail("Undisposed lock");
            }
        }
        private Sentinel leakDetector;
#endif

    }
    public class LockTimeoutException : ApplicationException
    {
        public LockTimeoutException()
          : base("Timeout waiting for lock")
        {
        }
    }
}
