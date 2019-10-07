using System.Collections.Generic;

public class BHG_TaskSystem
{


    private List<BHG_Task> taskList;

    private BHG_Task _curTask = null;
    public BHG_TaskSystem()
    {
        taskList = new List<BHG_Task>();
    }


    public BHG_Task getCurTak()
    {

        return _curTask;
    }
    public BHG_Task RequestNextTask()
    {
        if (taskList.Count > 0)
        {
            BHG_Task task = taskList[0];
            _curTask = task;
            taskList.RemoveAt(0);
            return task;
        }
        else
        {
            // No tasks are available
            return null;
        }
    }

    public void AddTask(BHG_Task task)
    {
        taskList.Add(task);
    }

}
