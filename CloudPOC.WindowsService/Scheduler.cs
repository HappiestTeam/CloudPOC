using System;
using System.Threading;
using System.Diagnostics;

namespace Service.Scheduler
{
    public class Scheduler
    {
        #region Private Data

        bool blnScheduleForTaskRunCount = false;
        private Timer timerInstance;
        private bool TaskInProgress = false;
        object objLock;

        #endregion Private Data

        #region Scheduler

        public Scheduler()
        {
            InitialiseVariables();
            InitializeTimer();
        }

        private void InitialiseVariables()
        {
            objLock = new object();
        }

        private void InitializeTimer()
        {
            try
            {
                long threadInterval = 0;
                if ((threadInterval = ConfigManager.PingInterval) > 0)                
                    timerInstance = new Timer(new TimerCallback(OnTimer),null,threadInterval,threadInterval);  												
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private void OnTimer(object obj)
        {
            try
            {
                CheckAndInitiateTask();                
            }
            catch (Exception e)
            {
                // do nothing
            }
        }

        private void CheckAndInitiateTask()
        {
            long threadInterval = 0;
            string startTimeFromConfig = "";
            string scheduledStartTime = "";

            if ((threadInterval = ConfigManager.PingInterval) > 0)
            {										
                startTimeFromConfig = ConfigManager.StartTime;
                if (startTimeFromConfig.Trim().Length > 0)
                    scheduledStartTime = ComputeScheduleTime(startTimeFromConfig, ConfigManager.RetryInterval);

                if (scheduledStartTime.Trim().Length > 0)
                {
                    if (this.CheckScheduleForTaskRun(scheduledStartTime))
                    {
                        if (!GetTaskInProgress())
                        {
                            Thread thd = new Thread(new ThreadStart(InitiateTask));
                            thd.Start();
                        }
                    }
                }
            }
        }

        private string ComputeScheduleTime(string strSchedTime, int nRetryVal)
        {
            string strComputeTime = strSchedTime;

            try
            {
                DateTime dt = Convert.ToDateTime(strSchedTime);
                long diff = CommonUtil.GetCurrentISTDateTime().Ticks - dt.Ticks;
                TimeSpan ts = CommonUtil.GetCurrentISTDateTime().Subtract(dt);
                long nHours = ts.Hours + 1;
                long diffCompare = 1800000000 + (36000000000 * (nHours - 1));
                
                if ((diff > diffCompare) && (nHours >= 1))
                {
                    int nComputedRetryIntreval = ComputeRetryIntreval(Convert.ToInt32(nHours), nRetryVal);
                    DateTime dt2 = dt.AddHours(nComputedRetryIntreval); 
                    strComputeTime = dt2.ToString("HH:mm");
                }
                else if (nHours > 1)
                {
                    int nComputedRetryIntreval = ComputeRetryIntreval(Convert.ToInt32(nHours - 1), nRetryVal);
                    DateTime dt2 = dt.AddHours(nComputedRetryIntreval); 
                    strComputeTime = dt2.ToString("HH:mm");
                }
                else
                {
                    strComputeTime = strSchedTime;
                }

                return strComputeTime;
            }
            catch 
            { 
                strComputeTime = strSchedTime; 
                return strComputeTime; 
            }
            finally
            {
                strSchedTime = null;
                strComputeTime = null;
            }
        }

        private int ComputeRetryIntreval(int nNumHoursPassed, int nSleepIntreval)
        {
            int retVal = nSleepIntreval;
            try
            {
                int nVal = ((nNumHoursPassed / nSleepIntreval) * nSleepIntreval);
                if (nVal == 0)
                {
                    retVal = nSleepIntreval;
                }
                else
                {
                    int nDiffVal = nNumHoursPassed - nVal;
                    if (nDiffVal > 0)
                    {
                        retVal = (((nNumHoursPassed / nSleepIntreval) + 1) * nSleepIntreval);
                    }
                    else if (nDiffVal == 0)
                    {
                        retVal = nNumHoursPassed;
                    }
                }
            }
            catch (Exception e)
            {
                // do nothing
            }

            return retVal;
        }

        private bool CheckScheduleForTaskRun(string strTime)
        {            	
            lock (objLock)
            {
                try
                {
                    bool retVal = false;

                    DateTime dt = Convert.ToDateTime(strTime);
                    TimeSpan ts = dt.Subtract(CommonUtil.GetCurrentISTDateTime());
                    long TimediffMin = ts.Minutes;
                    long TimediffHour = ts.Hours;
                    long TimediffSecond = ts.Seconds;

                    if ((TimediffMin == 0) && (TimediffHour == 0) && (TimediffSecond < 0) && (!blnScheduleForTaskRunCount))
                    {
                        blnScheduleForTaskRunCount = true;
                        retVal = true;
                    }
                    else if ((TimediffMin < 0) && (TimediffMin >= -2) && (TimediffHour == 0) && (TimediffSecond < 0) && (blnScheduleForTaskRunCount == true))
                    {
                        blnScheduleForTaskRunCount = false;
                    }

                    return retVal;
                }
                catch (Exception e)
                {
                    return false;
                }
                finally
                {
                    strTime = null;
                }
            }
        }

        private bool GetTaskInProgress()
        {
            try
            {
                bool retval = false;
                lock (objLock)
                {
                    retval = this.TaskInProgress;
                }

                return retval;
            }
            catch (Exception e)
            {
                throw (new Exception("Exception raised in GetTaskInProgress: " + e.Message + "] [Stack Trace: " + e.StackTrace));
            }
        }

        private void SetTaskInProgress(bool blnVal)
        {
            try
            {
                lock (objLock)
                {
                    this.TaskInProgress = blnVal;
                }
            }
            catch (Exception e)
            {
                throw (new Exception("Exception raised in SetStartTime: " + e.Message + "] [Stack Trace: " + e.StackTrace));
            }
        }

        #endregion Scheduler

        #region InitiateTask
        private void InitiateTask()
        {
            String LogPath = ConfigManager.LogFile;
            try
            {
                
                this.SetTaskInProgress(true);

                // Invoke the method
                ProcessData.PullDataFromQueue();
            }
            catch (Exception e)
            {
                CommonUtil.WriteLog(LogPath, DateTime.Now + " Exception ERROR: " + e.InnerException);
                CommonUtil.WriteLog(LogPath, DateTime.Now + " Exception ERROR: " + e.StackTrace);
                CommonUtil.WriteLog(LogPath, DateTime.Now + " Exception ERROR: " + e.Message);
            }
            finally
            {
                this.SetTaskInProgress(false);
            }
        }
        #endregion 
    }
}
