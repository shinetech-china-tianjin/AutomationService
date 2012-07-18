using Common.Logging;

namespace AutoDial
{
    public class Util
    {
        private static ILog _logger;
        public static ILog Logger {
            get { return _logger ?? (_logger = LogManager.GetLogger(typeof (Program))); }
        }
    }
}